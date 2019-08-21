using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Common
{
    public class ReverseEmployeeSalaryVoucherCommandHandler : IRequestHandler<ReverseEmployeeSalaryVoucherCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IAccountingServices _iAccountingServices;

        public ReverseEmployeeSalaryVoucherCommandHandler(HumanitarianAssistanceDbContext dbContext, IAccountingServices iAccountingServices)
        {
            _dbContext = dbContext;
            _iAccountingServices = iAccountingServices;
        }

        public async Task<ApiResponse> Handle(ReverseEmployeeSalaryVoucherCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            VoucherDetail voucherDetail = new VoucherDetail();

            try
            {
                if (await _iAccountingServices.ReverseEmployeeSalaryVoucher(request))
                {
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWentWrong; ;
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