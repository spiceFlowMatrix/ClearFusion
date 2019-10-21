using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class CheckExchangeRatesExistQueryHandler : IRequestHandler<CheckExchangeRatesExistQuery, ApiResponse>
    {
         private readonly HumanitarianAssistanceDbContext _dbContext;
         public CheckExchangeRatesExistQueryHandler(HumanitarianAssistanceDbContext dbContext)
         {
            _dbContext= dbContext;
         }

        public async Task<ApiResponse> Handle(CheckExchangeRatesExistQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                bool exchangeRateExists = await _dbContext.ExchangeRateDetail.AnyAsync(x=> !x.IsDeleted && x.Date.Date == request.ExchangeRateDate.Date && x.OfficeId == request.OfficeId);
               
                response.ResponseData = exchangeRateExists;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch(Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = exception.Message;
            }

            return response;
        }
    }
}
