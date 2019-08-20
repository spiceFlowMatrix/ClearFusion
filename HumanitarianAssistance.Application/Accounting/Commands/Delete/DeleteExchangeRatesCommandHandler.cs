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
                    throw new Exception("Exchange Rate Date can't be null");
                }

                var exchangeRateVerification = await _dbContext.ExchangeRateVerifications
                                                               .FirstOrDefaultAsync(x => x.IsDeleted == false && 
                                                                                         x.Date.Date == command.ExchangeRateDate.Date);

                if (exchangeRateVerification != null)
                {
                    exchangeRateVerification.IsDeleted = true;
                    exchangeRateVerification.ModifiedDate = DateTime.UtcNow;
                    exchangeRateVerification.ModifiedById = command.ModifiedById;
                    _dbContext.Update(exchangeRateVerification);
                    _dbContext.SaveChanges();
                }

                List<ExchangeRateDetail> exchangeRateList = await _dbContext.ExchangeRateDetail.Where(x => x.IsDeleted == false && x.Date.Date == command.ExchangeRateDate).ToListAsync();

                if (exchangeRateList.Any())
                {
                    foreach (ExchangeRateDetail exchangeRate in exchangeRateList)
                    {
                        exchangeRate.IsDeleted = true;
                        exchangeRate.ModifiedById = command.ModifiedById;
                        exchangeRate.ModifiedDate = DateTime.UtcNow;
                    }
                    _dbContext.UpdateRange(exchangeRateList);
                    _dbContext.SaveChanges();
                }

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