using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetVoucherDetailByVoucherNoQueryHandler : IRequestHandler<GetVoucherDetailByVoucherNoQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetVoucherDetailByVoucherNoQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetVoucherDetailByVoucherNoQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var voucherDetail = await _dbContext.VoucherDetail
                                              .Include(o => o.OfficeDetails)
                                              .Include(x=> x.VoucherTransactionDetails)
                                              .Include(j => j.JournalDetails)
                                              .Include(c => c.CurrencyDetail)
                                              .Include(f => f.FinancialYearDetails)
                                              .Include(v=> v.VoucherTypes)
                                              .FirstOrDefaultAsync(v => v.IsDeleted == false && v.VoucherNo == request.VoucherId);

                if (voucherDetail != null)
                {
                    VoucherDetailModel obj = new VoucherDetailModel();

                    obj.VoucherNo = voucherDetail.VoucherNo;
                    obj.CurrencyCode = voucherDetail.CurrencyDetail?.CurrencyCode ?? null;
                    obj.CurrencyId = voucherDetail.CurrencyDetail?.CurrencyId ?? 0;
                    obj.VoucherDate = voucherDetail.VoucherDate;
                    obj.ChequeNo = voucherDetail.ChequeNo;
                    obj.ReferenceNo = voucherDetail.ReferenceNo;
                    obj.Description = voucherDetail.Description;
                    obj.JournalName = voucherDetail.JournalDetails?.JournalName ?? null;
                    obj.JournalCode = voucherDetail.JournalDetails?.JournalCode ?? null;
                    obj.VoucherTypeId = voucherDetail.VoucherTypeId;
                    obj.VoucherTypeName = voucherDetail.VoucherTypes.VoucherTypeName;
                    obj.OfficeId = voucherDetail.OfficeId;
                    obj.ProjectId = voucherDetail.ProjectId;
                    obj.BudgetLineId = voucherDetail.BudgetLineId;
                    obj.OfficeName = voucherDetail.OfficeDetails?.OfficeName ?? null;
                    obj.FinancialYearId = voucherDetail.FinancialYearId;
                    obj.FinancialYearName = voucherDetail.FinancialYearDetails?.FinancialYearName ?? null;
                    obj.IsVoucherVerified = voucherDetail.IsVoucherVerified;
                    obj.IsExchangeGainLossVoucher = voucherDetail.IsExchangeGainLossVoucher;
                    obj.OperationalType = voucherDetail.OperationalType;
                    obj.TotalCredit = Math.Round((double)voucherDetail.VoucherTransactionDetails.Where(t=> t.IsDeleted == false).Select(x=> x.Credit).DefaultIfEmpty(0).Sum(), 2);
                    obj.TotalDebit =  Math.Round((double)voucherDetail.VoucherTransactionDetails.Where(t=> t.IsDeleted == false).Select(x=> x.Debit).DefaultIfEmpty(0).Sum(),2);

                    if(obj.OperationalType == (int)OperationType.Store || obj.OperationalType == (int)OperationType.Logistics)
                    {
                        ProjectLogisticRequests logisticData = await _dbContext.ProjectLogisticRequests
                                                                         .FirstOrDefaultAsync(x=> x.IsDeleted == false && 
                                                                         x.VoucherNo == obj.VoucherNo);

                        
                        
                        if(logisticData != null)
                        {
                            StoreItemPurchase storePurchase = await _dbContext.StoreItemPurchases
                                                                          .FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                                                                          x.LogisticRequestId == logisticData.LogisticRequestsId);

                            if(storePurchase != null) 
                            {
                                var approvedUser = await _dbContext.UserDetails.FirstOrDefaultAsync(x=>x.IsDeleted==false && x.AspNetUserId == storePurchase.CreatedById);
                                obj.PurchaseOrderModel.ApprovedBy= approvedUser.FirstName + ' ' + approvedUser.LastName;
                                obj.PurchaseOrderModel.ApprovedOn = storePurchase.CreatedDate.Value.ToShortDateString();
                            }
                            else
                            {
                                obj.PurchaseOrderModel.ApprovedBy= "";
                                obj.PurchaseOrderModel.ApprovedOn = "";
                            }

                            obj.PurchaseOrderModel.ProjectId = logisticData.ProjectId;
                            obj.PurchaseOrderModel.Code = "REQ"+ logisticData.LogisticRequestsId.ToString();
                            obj.PurchaseOrderModel.PurchaseOrderId = logisticData.LogisticRequestsId;
                            obj.PurchaseOrderModel.Description= logisticData.Description;
                        }
                    }

                    response.data.VoucherDetail = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.VoucherNotPresent;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}