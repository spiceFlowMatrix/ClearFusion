using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetEmployeeSalaryVoucherQueryHandler : IRequestHandler<GetEmployeeSalaryVoucherQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeSalaryVoucherQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeSalaryVoucherQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            VoucherDetail voucherDetail = new VoucherDetail();

            try
            {
                EmployeeSalaryPaymentHistory employeeSalaryPaymentHistory = await _dbContext.EmployeeSalaryPaymentHistory
                                                                                        .FirstOrDefaultAsync(x => x.IsDeleted == false
                                                                                            && x.IsSalaryReverse == false
                                                                                            && x.EmployeeId == request.EmployeeId && x.Year == request.Year
                                                                                            && x.Month == request.Month);

                if (employeeSalaryPaymentHistory != null)
                {
                    voucherDetail = await _dbContext.VoucherDetail.FirstOrDefaultAsync(x => x.VoucherNo == employeeSalaryPaymentHistory.VoucherNo);
                }

                response.data.VoucherNo = voucherDetail.VoucherNo;
                response.data.VoucherReferenceNo = voucherDetail.ReferenceNo;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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