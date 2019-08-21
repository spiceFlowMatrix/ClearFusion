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

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePaymentTypesCommandHandler : IRequestHandler<DeletePaymentTypesCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeletePaymentTypesCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeletePaymentTypesCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.PaymentId != 0)
                {
                    //Get Payment Type Record based on payment id
                    PaymentTypes storePaymentTypes = await _dbContext.PaymentTypes.FirstOrDefaultAsync(x => x.IsDeleted == false && x.PaymentId == request.PaymentId);

                    if (storePaymentTypes != null)
                    {
                        storePaymentTypes.ModifiedById = request.ModifiedById;
                        storePaymentTypes.ModifiedDate = request.ModifiedDate;
                        storePaymentTypes.IsDeleted = true;
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
