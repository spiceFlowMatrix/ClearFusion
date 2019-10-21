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

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddPaymentTypesCommandHandler : IRequestHandler<AddPaymentTypesCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddPaymentTypesCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddPaymentTypesCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    PaymentTypes storePaymentTypes = new PaymentTypes();

                    storePaymentTypes.IsDeleted = false;
                    storePaymentTypes.CreatedById = request.CreatedById;
                    storePaymentTypes.CreatedDate = request.CreatedDate;
                    storePaymentTypes.Name = request.Name;
                    storePaymentTypes.ChartOfAccountNewId = request.ChartOfAccountNewId;

                    await _dbContext.PaymentTypes.AddAsync(storePaymentTypes);
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
