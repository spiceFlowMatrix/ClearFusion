using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
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
                                              .Include(j => j.JournalDetails)
                                              .Include(c => c.CurrencyDetail)
                                              .Include(f => f.FinancialYearDetails)
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
                    obj.OfficeId = voucherDetail.OfficeId;
                    obj.ProjectId = voucherDetail.ProjectId;
                    obj.BudgetLineId = voucherDetail.BudgetLineId;
                    obj.OfficeName = voucherDetail.OfficeDetails?.OfficeName ?? null;
                    obj.FinancialYearId = voucherDetail.FinancialYearId;
                    obj.FinancialYearName = voucherDetail.FinancialYearDetails?.FinancialYearName ?? null;
                    obj.IsVoucherVerified = voucherDetail.IsVoucherVerified;
                    obj.IsExchangeGainLossVoucher = voucherDetail.IsExchangeGainLossVoucher;

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