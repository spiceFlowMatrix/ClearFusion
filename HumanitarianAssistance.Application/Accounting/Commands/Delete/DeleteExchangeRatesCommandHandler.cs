using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Delete
{
    public class DeleteExchangeRatesCommandHandler : IRequestHandler<DeleteExchangeRatesCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public DeleteExchangeRatesCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteExchangeRatesCommand command, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (command.ExchangeRateDate == null) {
                    throw new Exception("Please select Exchange Rate Date");
                }

                var exchangeRateVerification = await _dbContext.ExchangeRateVerifications
                                                               .FirstOrDefaultAsync(x => x.IsDeleted == false && 
                                                                                         x.Date.Date == command.ExchangeRateDate.Date);

                var exchangeRateDetail =  await _dbContext.ExchangeRateDetail
                                                          .Where(x => x.IsDeleted == false && 
                                                                               x.Date.Date == command.ExchangeRateDate.Date).ToListAsync();

                if(exchangeRateDetail.Any())
                {
                    exchangeRateDetail.ForEach(x=> { 
                        x.IsDeleted = true;
                        x.ModifiedById = command.ModifiedById;
                        x.ModifiedDate = DateTime.UtcNow;
                    });

                    _dbContext.ExchangeRateDetail.UpdateRange(exchangeRateDetail);
                }                                                           

                if (exchangeRateVerification != null)
                {
                    exchangeRateVerification.IsDeleted = true;
                    exchangeRateVerification.ModifiedDate = DateTime.UtcNow;
                    exchangeRateVerification.ModifiedById = command.ModifiedById;
                    _dbContext.Update(exchangeRateVerification);
                }

                await _dbContext.SaveChangesAsync();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Exchange rates deleted successfully";
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