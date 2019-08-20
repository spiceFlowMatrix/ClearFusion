using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditPaymentTypesCommandHandler : IRequestHandler<EditPaymentTypesCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditPaymentTypesCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditPaymentTypesCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    //Get Payment Type Record based on payment id
                    PaymentTypes storePaymentTypes = await _dbContext.PaymentTypes.FirstOrDefaultAsync(x => x.IsDeleted == false && x.PaymentId == request.PaymentId);

                    if (storePaymentTypes != null)
                    {
                        storePaymentTypes.ModifiedById = request.ModifiedById;
                        storePaymentTypes.ModifiedDate = request.ModifiedDate;
                        storePaymentTypes.Name = request.Name;
                        storePaymentTypes.ChartOfAccountNewId = request.ChartOfAccountNewId;
                    }

                    //update PaymentType Record
                    await _dbContext.SaveChangesAsync(); 
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
