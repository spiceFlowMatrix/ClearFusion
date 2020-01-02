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
    public class GetProcurementDetailsByProcurementIdQueryHandler : IRequestHandler<GetProcurementDetailsByProcurementIdQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetProcurementDetailsByProcurementIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetProcurementDetailsByProcurementIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                var result = await _dbContext.StorePurchaseOrders
                                        .Include(x=> x.ReturnProcurementDetailList)
                                        .Include(x=> x.EmployeeDetail)
                                        .ThenInclude(x=> x.EmployeeProfessionalDetail)
                                        .Where(x => x.OrderId == request.Id)
                                       .Select(x => new ProcurementDetailModel
                                       {
                                          OrderId= x.OrderId,
                                          PurchaseId= x.PurchaseId,
                                          ItemId= x.InventoryItem,
                                          IssuedQuantity= x.IssuedQuantity,
                                          MustReturn= x.MustReturn,
                                          IssuedToEmployeeId= x.IssuedToEmployeeId,
                                          IssueDate= x.IssueDate,
                                          VoucherNo= x.IssueVoucher,
                                          ProjectId = x.Project,
                                          IssedToLocation= Convert.ToInt64(x.IssedToLocation),
                                          StatusAtTimeOfIssue= x.StatusAtTimeOfIssue,
                                          OfficeId = x.EmployeeDetail.EmployeeProfessionalDetail.OfficeId
                                       }).FirstOrDefaultAsync();

                var purchase = await _dbContext.StoreItemPurchases
                                         .Include(x=> x.PurchaseOrders)
                                         .FirstOrDefaultAsync(x=> x.PurchaseId == result.PurchaseId);

                 result.RemainingQuantity =  purchase != null?  purchase.PurchaseOrders.Where(x=> x.IsDeleted == false)
                                                                                      .Select(x=> x.IssuedQuantity).DefaultIfEmpty(0).Sum(): 0;                
                response.Add("Procurement", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}