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
    public class GetFilteredPurchaseListQueryHandler : IRequestHandler<GetFilteredPurchaseListQuery, List<PurchaseListModel>>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        
        public GetFilteredPurchaseListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<PurchaseListModel>> Handle(GetFilteredPurchaseListQuery request, CancellationToken cancellationToken)
        {
            List<PurchaseListModel> model = new List<PurchaseListModel>();
            
            try
            {
                model = await _dbContext.StoreItemPurchases
                                  // .Include(x=> x.ProjectDetail.)
                                   //.ThenInclude(x=> x.Job)
                                  .Include(x=> x.PurchaseOrders)
                                  .ThenInclude(x=> x.EmployeeDetail)
                                  .Include(x=> x.StoreInventoryItem)
                                  .Include(x=> x.EmployeeDetail)
                                  .Include(x=> x.ProjectDetail)
                                  .Where(x=> x.IsDeleted== false &&
                                        x.AssetTypeId== request.InventoryTypeId &&
                                        x.ReceiptTypeId == request.ReceiptTypeId &&
                                        x.OfficeId == request.OfficeId &&
                                        x.Currency == request.CurrencyId &&
                                        x.ProjectId==null ? true : x.ProjectId==request.ProjectId
                                       // x.JobId == null ? true : x.JobId =
                                        )
                                  .Select(x=> new PurchaseListModel 
                                  {
                                      PurchaseId = x.PurchaseId,
                                      PurchaseDate= x.PurchaseDate,
                                      CurrencyId= x.Currency,
                                      ItemId = x.StoreInventoryItem.ItemId,
                                      ItemName= x.StoreInventoryItem.ItemCode +"-"+x.StoreInventoryItem.ItemName,
                                      EmployeeName= x.EmployeeDetail.EmployeeCode+"-"+x.EmployeeDetail.EmployeeName,
                                      ProjectId= x.ProjectId,
                                      ProjectName= x.ProjectDetail == null ? "" : x.ProjectDetail.ProjectCode+"-"+x.ProjectDetail.ProjectName,
                                      OriginalCost= x.UnitCost * x.Quantity,
                                      DepreciatedCost= (x.UnitCost * x.Quantity)-(Math.Ceiling(DateTime.Now.Date.Subtract(x.PurchaseDate).TotalDays) * x.Quantity * x.DepreciationRate * x.UnitCost / 100),
                                      ProcurementList= x.PurchaseOrders.Select(z=> new ProcurementListModel 
                                                       {
                                                           IssueId= z.OrderId,
                                                           EmployeeName= z.EmployeeDetail.EmployeeCode +"-"+z.EmployeeDetail.EmployeeName,
                                                           IssueDate= z.IssueDate,
                                                           MustReturn= z.MustReturn,
                                                           ProcuredAmount= z.IssuedQuantity,
                                                           Returned= z.Returned,
                                                           ReturnedOn= z.ReturnedDate
                                                       }).ToList()

                                  })
                                  .Skip(request.pageSize.Value * request.pageIndex.Value)
                                  .Take(request.pageSize.Value)
                                  .ToListAsync();

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
            catch(Exception ex)
            {
                throw ex;
            }

            return model;
        }
    }
}