using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class AddVoucherDetailCommandHandler : IRequestHandler<AddVoucherDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAccountingServices _iAccountingServices;

        public AddVoucherDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IAccountingServices iAccountingServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iAccountingServices = iAccountingServices;
        }

        public async Task<ApiResponse> Handle(AddVoucherDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                // Common Function to Add/Update Transaction
                VoucherDetailEntityModel voucherDetail = await _iAccountingServices.AddVoucherDetail(request);

                response.data.VoucherDetailEntity = voucherDetail;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;

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