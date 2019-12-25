using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
                                      .Select(y=> new  {
                                         Id= y.OrderId,
                                         PurchaseId = y.PurchaseId,
                                         MustReturn = y.MustReturn,
                                         Status = statusList.FirstOrDefault(x=> x.StatusAtTimeOfIssueId == y.StatusAtTimeOfIssue) != null ? 
                                                  statusList.FirstOrDefault(x=> x.StatusAtTimeOfIssueId == y.StatusAtTimeOfIssue).StatusName : null,
                                        Date = y.IssueDate,
                                        ItemCode = y.StoreInventoryItem != null? y.StoreInventoryItem.ItemCode : null,
                                        Voucher = y.VoucherDetail != null ? y.VoucherDetail.ReferenceNo : null,
                                        IssuedToEmployee = y.EmployeeDetail != null ? (y.EmployeeDetail.EmployeeCode +"-"+ y.EmployeeDetail.EmployeeName) : null,
                                        StartingBalance = y.StoreItemPurchase.Quantity,
                                        CurrentBalance =

                                      });

                if(query != null)
                {
                    var result = 

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}