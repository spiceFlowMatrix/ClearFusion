using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class SaveExchangeRatesForAllOfficesCommandHandler : IRequestHandler<SaveExchangeRatesForAllOfficesCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public SaveExchangeRatesForAllOfficesCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(SaveExchangeRatesForAllOfficesCommand officeExchangeRateViewModel, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            List<ExchangeRateDetail> exchangeRateList = new List<ExchangeRateDetail>();

            try
            {
                if (officeExchangeRateViewModel.SaveForAllOffices)
                {

                    exchangeRateList = await _dbContext.ExchangeRateDetail.Where(x => x.IsDeleted == false && x.Date.Date == officeExchangeRateViewModel.GenerateExchangeRateModel.FirstOrDefault().Date.Date).ToListAsync();

                    if (exchangeRateList.Any())
                    {
                        foreach (ExchangeRateDetail item in exchangeRateList)
                        {
                            item.Rate = (decimal)officeExchangeRateViewModel.GenerateExchangeRateModel.FirstOrDefault(x => x.ToCurrencyId == item.ToCurrency && x.FromCurrencyId == item.FromCurrency).Rate;
                        }

                        _dbContext.ExchangeRateDetail.UpdateRange(exchangeRateList);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    exchangeRateList = await _dbContext.ExchangeRateDetail.Where(x => x.IsDeleted == false && x.Date.Date == officeExchangeRateViewModel.GenerateExchangeRateModel.FirstOrDefault().Date.Date && x.OfficeId == officeExchangeRateViewModel.OfficeId).ToListAsync();

                    if (exchangeRateList.Any())
                    {
                        foreach (ExchangeRateDetail item in exchangeRateList)
                        {
                            item.Rate = (decimal)officeExchangeRateViewModel.GenerateExchangeRateModel.FirstOrDefault(x => x.ToCurrencyId == item.ToCurrency && x.FromCurrencyId == item.FromCurrency).Rate;
                        }

                        _dbContext.ExchangeRateDetail.UpdateRange(exchangeRateList);
                        _dbContext.SaveChanges();
                    }
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Exchange rates updated successfully!!!";
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