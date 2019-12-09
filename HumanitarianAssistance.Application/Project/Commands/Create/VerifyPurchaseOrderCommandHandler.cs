using System;
using HumanitarianAssistance.Common.Enums;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using HumanitarianAssistance.Application.Accounting.Commands.Create;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class VerifyPurchaseOrderCommandHandler : IRequestHandler<VerifyPurchaseOrderCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProjectServices _iProjectServices;

        public VerifyPurchaseOrderCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IProjectServices iProjectServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(VerifyPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var _logisticReq = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted== false && x.LogisticRequestsId == request.RequestId);
                if(_logisticReq == null) {
                    throw new Exception("Request not found!");
                }
                AddVoucherDetailCommand model = new AddVoucherDetailCommand(){
                    ChequeNo = "",
                    CurrencyId= _logisticReq.CurrencyId,
                    Description= request.VoucherDescription,
                    JournalCode= request.Journal,
                    OfficeId= _logisticReq.OfficeId,
                    TimezoneOffset= -330,
                    VoucherDate= _logisticReq.PurchaseDate ?? throw new Exception("Purchase not submitted!"),
                    VoucherTypeId= 2
                };
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch(Exception ex) 
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}