using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredPurchaseListQueryHandler : IRequestHandler<GetFilteredPurchaseListQuery, StorePurchaseListModel>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetFilteredPurchaseListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StorePurchaseListModel> Handle(GetFilteredPurchaseListQuery request, CancellationToken cancellationToken)
        {
            StorePurchaseListModel model = new StorePurchaseListModel();

            try
            {
                var query = _dbContext.StoreItemPurchases
                                   .Include(x => x.ProjectDetail)
                                   .ThenInclude(x => x.ProjectJobDetail)
                                  .Include(x => x.PurchaseOrders)
                                  .ThenInclude(x => x.EmployeeDetail)
                                  .Include(x => x.StoreInventoryItem)
                                  .ThenInclude(x => x.StoreItemGroup)
                                  .Include(x => x.StoreInventoryItem)
                                  .ThenInclude(x => x.Inventory)
                                  .Include(x => x.EmployeeDetail)
                                  .Include(x => x.ProjectDetail)
                                  .Where(x => x.IsDeleted == false &&
                                        x.StoreInventoryItem.Inventory.AssetType == request.InventoryTypeId &&
                                        x.ReceiptTypeId == request.ReceiptTypeId &&
                                        x.OfficeId == request.OfficeId &&
                                        x.Currency == request.CurrencyId);

                if (request.InventoryId != 0)
                {
                    query= query.Where(x => x.StoreInventoryItem.Inventory.InventoryId == request.InventoryId);
                }

                if (request.ProjectId != 0)
                {
                   query= query.Where(x => x.ProjectId == request.ProjectId);
                }

                if (request.JobId != 0)
                {
                   query= query.Where(x => x.ProjectDetail.ProjectJobDetail.Select(z => z.ProjectJobId).Contains(request.JobId));
                }

                if (request.PurchaseStartDate != null)
                {
                   query= query.Where(x => x.PurchaseDate.Date >= request.PurchaseStartDate);
                }

                if (request.PurchaseEndDate != null)
                {
                   query= query.Where(x => x.PurchaseDate.Date <= request.PurchaseEndDate);
                }

                if (request.ItemGroupId != 0)
                {
                   query= query.Where(x => x.StoreInventoryItem.StoreItemGroup.ItemGroupId == request.ItemGroupId);
                }

                if (request.ItemId != 0)
                {
                   query= query.Where(x => x.StoreInventoryItem.ItemId == request.ItemId);
                }

                var queryResult = query.Select(x => new PurchaseListModel
                {
                    PurchaseId = x.PurchaseId,
                    PurchaseDate = x.PurchaseDate.ToShortDateString(),
                    CurrencyId = x.Currency,
                    PurchasedQuantity= x.Quantity,
                    ItemId = x.StoreInventoryItem != null ? x.StoreInventoryItem.ItemId : 0,
                    ItemName = x.StoreInventoryItem != null ? (x.StoreInventoryItem.ItemCode + "-" + x.StoreInventoryItem.ItemName) : "",
                    EmployeeName = x.EmployeeDetail != null ? x.EmployeeDetail.EmployeeCode + "-" + x.EmployeeDetail.EmployeeName : "",
                    ProjectId = x.ProjectId,
                    ProjectName = x.ProjectDetail == null ? "" : x.ProjectDetail.ProjectCode + "-" + x.ProjectDetail.ProjectName,
                    OriginalCost = x.UnitCost * x.Quantity,
                    DepreciatedCost = (x.UnitCost * x.Quantity) - (Math.Ceiling(DateTime.Now.Date.Subtract(x.PurchaseDate).TotalDays) * x.Quantity * x.DepreciationRate * x.UnitCost / 100),
                    ProcurementList = x.PurchaseOrders.Where(p=> !p.IsDeleted).Select(z => new ProcurementListModel
                    {
                        OrderId = z.OrderId,
                        EmployeeName = z.EmployeeDetail.EmployeeCode + "-" + z.EmployeeDetail.EmployeeName,
                        IssueDate = z.IssueDate,
                        MustReturn = z.MustReturn,
                        ProcuredAmount = z.IssuedQuantity,
                        Returned = z.Returned,
                        ReturnedOn = z.ReturnedDate
                    }).Where(y => request.IssueStartDate == null ? true : y.IssueDate >= request.IssueStartDate &&
                     request.IssueEndDate == null ? true : y.IssueDate <= request.IssueEndDate).ToList()
                }).AsQueryable();

                model.RecordCount = await queryResult.CountAsync();
                model.PurchaseList = await queryResult.Skip(request.pageIndex.Value * request.pageSize.Value).Take(request.pageSize.Value).ToListAsync();

                // List<ExchangeRateDetail> exchangeRateList= await _dbContext.ExchangeRateDetail
                //                                                      .Where(x=> x.IsDeleted== false &&
                //                                                      model.Select(y=> y.PurchaseDate.Date).Contains(x.Date.Date)).ToListAsync();

                // foreach(var item in model)
                // {
                //     ExchangeRateDetail exchangeRate= exchangeRateList.FirstOrDefault(x=> x.Date.Date == item.PurchaseDate.Date &&
                //                                                      x.FromCurrency== item.CurrencyId && x.ToCurrency == request.CurrencyId);

                //     if(exchangeRate== null)
                //     {
                //         throw new Exception(string.Format(StaticResource.ExchangeRateNotPresent, item.PurchaseDate.Date.ToShortDateString()));
                //     }

                //     item.OriginalCost = item.OriginalCost * (double)exchangeRate.Rate;
                //     item.DepreciatedCost = item.DepreciatedCost * (double)exchangeRate.Rate;
                // }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }
    }
}