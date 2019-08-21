using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetExchangeRatesDetailQueryHandler : IRequestHandler<GetExchangeRatesDetailQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetExchangeRatesDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetExchangeRatesDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response.data.IsExchangeRateVerified = _dbContext.ExchangeRateVerifications.FirstOrDefault(x => x.IsDeleted == false && x.Date.Date == request.ExchangeRateDate.Date).IsVerified;

                response.data.ExchangeRateDetailViewModelList = await _dbContext.ExchangeRateDetail
                                                                         .Where(x => x.IsDeleted == false && x.Date.Date == request.ExchangeRateDate.Date && x.OfficeId == request.OfficeId)
                                                                         .Select(x => new ExchangeRateDetailViewModel
                                                                         {
                                                                             ExchangeRateId = x.ExchangeRateId,
                                                                             FromCurrency = x.FromCurrency,
                                                                             Rate = (double)Math.Round(x.Rate, 4),
                                                                             ToCurrency = x.ToCurrency
                                                                         }).OrderBy(x => x.FromCurrency).ThenBy(x => x.ToCurrency).ToListAsync();

                response.StatusCode = StaticResource.successStatusCode;
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