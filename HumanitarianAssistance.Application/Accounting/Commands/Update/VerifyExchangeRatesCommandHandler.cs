using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class VerifyExchangeRatesCommandHandler: IRequestHandler<VerifyExchangeRatesCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public VerifyExchangeRatesCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(VerifyExchangeRatesCommand command, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            List<ExchangeRateDetail> exchangeRateList = new List<ExchangeRateDetail>();

            try
            {
                if (command.ExchangeRateDate == null) {
                    throw new Exception("Exchange Rate Date can't be null");
                }

                var exchangeRateVerification = await _dbContext.ExchangeRateVerifications
                                                               .FirstOrDefaultAsync(x => x.IsDeleted == false && 
                                                                                         x.Date.Date == command.ExchangeRateDate.Date);
                
                exchangeRateVerification.IsVerified = true;
                exchangeRateVerification.ModifiedDate = DateTime.UtcNow;
                exchangeRateVerification.ModifiedById = command.ModifiedById;
                
                _dbContext.Update(exchangeRateVerification);
                _dbContext.SaveChanges();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Exchange rates verified successfully";
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