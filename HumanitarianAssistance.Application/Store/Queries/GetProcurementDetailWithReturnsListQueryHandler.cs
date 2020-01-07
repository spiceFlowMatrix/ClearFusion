using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetProcurementDetailWithReturnsListQueryHandler: IRequestHandler<GetProcurementDetailWithReturnsListQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetProcurementDetailWithReturnsListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetProcurementDetailWithReturnsListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response  = new Dictionary<string, object>();

            try
            {

                var statusList = await _dbContext.StatusAtTimeOfIssue.Where(x=> x.IsDeleted == false).ToListAsync();
                var query = await _dbContext.StorePurchaseOrders
                                      .Include(x=> x.EmployeeDetail)
                                      .Include(x=> x.VoucherDetail)
                                      .Include(x=> x.ReturnProcurementDetailList)
                                      .Include(x=> x.StoreItemPurchase)
                                      .ThenInclude(x=> x.StoreInventoryItem)
                                      .Where(x=> x.IsDeleted == false && x.OrderId == request.Id)
                                      .Select(y=> new ProcurementControlPanelModel {
                                         Id= y.OrderId,
                                         PurchaseId = y.PurchaseId,
                                         MustReturn = y.MustReturn,
                                         Status = statusList.FirstOrDefault(x=> x.StatusAtTimeOfIssueId == y.StatusAtTimeOfIssue) != null ? 
                                                  statusList.FirstOrDefault(x=> x.StatusAtTimeOfIssueId == y.StatusAtTimeOfIssue).StatusName : null,
                                        Date = y.IssueDate.ToShortDateString(),
                                        ItemCode = y.StoreInventoryItem != null? y.StoreInventoryItem.ItemCode : null,
                                        VoucherNo = y.VoucherDetail != null ? y.VoucherDetail.ReferenceNo : null,
                                        EmployeeName = y.EmployeeDetail != null ? (y.EmployeeDetail.EmployeeCode +"-"+ y.EmployeeDetail.EmployeeName) : null,
                                        StartingBalance = y.IssuedQuantity,
                                        CurrentBalance= y.IssuedQuantity- (y.ReturnProcurementDetailList.Any()? y.ReturnProcurementDetailList
                                                                                              .Where(x=> x.IsDeleted == false)
                                                                       .Select(x=> x.ReturnedQuantity).DefaultIfEmpty(0).Sum() : 0),
                                        ProjectId = y.Project
                                      }).FirstOrDefaultAsync();

                var purchases = await _dbContext.StoreItemPurchases
                                                .Include(x=> x.PurchaseOrders)
                                                .Include(x=> x.ReturnProcurementDetailList)
                                                .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.PurchaseId == query.PurchaseId);

                if(query != null)
                {

                    // int procured = (purchases.PurchaseOrders.Any() ? 
                    //                                             purchases.PurchaseOrders.Where(x=> x.IsDeleted == false)
                    //                                             .Select(x=> x.IssuedQuantity)
                    //                                             .DefaultIfEmpty(0)
                    //                                             .Sum() : 0);

                    // int returns = (query.ReturnProcurementDetailList.Any()?
                    //                         purchases.ReturnProcurementDetailList.Where(x=> x.IsDeleted == false)
                    //                                                             .Select(x=> x.ReturnedQuantity).DefaultIfEmpty(0).Sum() : 0);

                    // query.CurrentBalance = query.StartingBalance - returns;
                    
                    var project = await _dbContext.ProjectDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ProjectId == query.ProjectId);
                    query.ProjectName = project != null ? (project.ProjectCode + "-" +project.ProjectName) : null;

                    query.ProcurementReturnList = purchases.ReturnProcurementDetailList.Where(x=> x.IsDeleted == false && x.ProcurementId == request.Id)
                    .Select(x=> new ProcurementReturnModel 
                    {
                        Date = x.ReturnedDate.ToShortDateString(),
                        Id= x.Id,
                        ReturnedQuantity= x.ReturnedQuantity
                    }).ToList();
                }

                response.Add("ProcurementDetail", query);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}