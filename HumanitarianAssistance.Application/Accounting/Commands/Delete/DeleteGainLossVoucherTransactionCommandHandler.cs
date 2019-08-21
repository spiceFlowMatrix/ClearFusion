using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Delete
{
    public class DeleteGainLossVoucherTransactionCommandHandler : IRequestHandler<DeleteGainLossVoucherTransactionCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAccountingServices _iAccountingServices;

        public DeleteGainLossVoucherTransactionCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IAccountingServices iAccountingServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iAccountingServices = iAccountingServices;
        }

        /// <summary>
        ///  Delete Gain Loss Voucher-Transaction
        /// </summary>
        /// <param name="voucherId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Handle(DeleteGainLossVoucherTransactionCommand command, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (command.VoucherNo != 0)
                {

                    var voucherResponse = await _iAccountingServices.DeleteVoucher(command.VoucherNo);
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
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}