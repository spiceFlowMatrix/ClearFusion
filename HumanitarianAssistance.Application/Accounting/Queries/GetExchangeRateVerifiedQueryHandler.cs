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
    public class GetExchangeRateVerifiedQueryHandler : IRequestHandler<GetExchangeRateVerifiedQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetExchangeRateVerifiedQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetExchangeRateVerifiedQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                var IsExchangeRateVerified = await (from ex in _dbContext.ExchangeRateVerifications
                                              .Where(x => x.IsDeleted == false && x.Date.Date == request.ExchangeRateDate.Date && x.IsVerified == true)
                                                    join exv in _dbContext.ExchangeRateDetail on ex.Date.Date equals exv.Date.Date
                                                    select new 
                                                    {
                                                        IsVerified = ex.IsVerified
                                                    }
                                               ).FirstOrDefaultAsync();

                response.ResponseData = IsExchangeRateVerified;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = exception.Message;
            }

            return response;
        }
    }
}