using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{

    public class AddExchangeRateCommandHandler : IRequestHandler<AddExchangeRateCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public AddExchangeRateCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddExchangeRateCommand exchangeRateModel, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (exchangeRateModel.GenerateExchangeRateModel.Any())
                    {
                        List<ExchangeRateDetail> exchangeRateDetails = new List<ExchangeRateDetail>();

                        var exchangeRateDates = _dbContext.ExchangeRateVerifications.Where(x => x.IsDeleted == false && x.Date.Date == exchangeRateModel.GenerateExchangeRateModel.FirstOrDefault().Date.Date).ToList();
                        List<CurrencyDetails> currencyDetails = _dbContext.CurrencyDetails.Where(x => x.IsDeleted == false).ToList();

                        if (exchangeRateDates.Any())
                        {
                            throw new Exception("Exchange rate already exists for the selected date");

                        }

                        GetSystemGeneratedExchangeRates(exchangeRateModel.GenerateExchangeRateModel, ref exchangeRateDetails, currencyDetails, exchangeRateModel.CreatedById);

                        List<int> OfficeIds = await _dbContext.OfficeDetail.Where(x => x.IsDeleted == false).Select(x => x.OfficeId).ToListAsync();

                        List<ExchangeRateDetail> exchangeRatesForAllOffices = new List<ExchangeRateDetail>();

                        foreach (int officeId in OfficeIds)
                        {
                            foreach (ExchangeRateDetail item in exchangeRateDetails)
                            {
                                ExchangeRateDetail exchangeRateDetail = new ExchangeRateDetail();
                                exchangeRateDetail.CreatedById = item.CreatedById;
                                exchangeRateDetail.CreatedDate = item.CreatedDate;
                                exchangeRateDetail.Date = item.Date;
                                exchangeRateDetail.FromCurrency = item.FromCurrency;
                                exchangeRateDetail.ToCurrency = item.ToCurrency;
                                exchangeRateDetail.IsDeleted = item.IsDeleted;
                                exchangeRateDetail.OfficeId = officeId;
                                exchangeRateDetail.Rate = item.Rate;

                                exchangeRatesForAllOffices.Add(exchangeRateDetail);

                            }
                            exchangeRatesForAllOffices.AddRange(exchangeRateDetails);
                        }

                        await _dbContext.ExchangeRateDetail.AddRangeAsync(exchangeRatesForAllOffices);
                        await _dbContext.SaveChangesAsync();

                        ExchangeRateVerification exchangeRateVerification = new ExchangeRateVerification();
                        exchangeRateVerification.IsDeleted = false;
                        exchangeRateVerification.CreatedById = exchangeRateModel.CreatedById;
                        exchangeRateVerification.CreatedDate = DateTime.UtcNow;
                        exchangeRateVerification.Date = exchangeRateModel.GenerateExchangeRateModel.FirstOrDefault().Date;
                        exchangeRateVerification.IsVerified = false;

                        _dbContext.ExchangeRateVerifications.Add(exchangeRateVerification);
                        await _dbContext.SaveChangesAsync();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Exchange rates generated successfully!!!";
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }


            return response;
        }

        public void GetSystemGeneratedExchangeRates(List<GenerateExchangeRateModel> exchangeRateList, ref List<ExchangeRateDetail> exchangeRateDetails, List<CurrencyDetails> currencyDetails, string userId)
        {
            try
            {
                int baseCurrenyId = exchangeRateList.FirstOrDefault().FromCurrencyId;

                foreach (GenerateExchangeRateModel item in exchangeRateList)
                {
                    exchangeRateDetails.Add(AddExchangeRateAfterManipulation(item, userId));

                    if (item.ToCurrencyId != baseCurrenyId)
                    {
                        exchangeRateDetails.Add(AddReverseExchangeRateAfterManipulation(item, userId));
                    }
                }

                if (currencyDetails.Any())
                {
                    //to currency
                    for (int i = 0; i < currencyDetails.Count; i++)
                    {
                        //from currency
                        for (int j = 0; j < currencyDetails.Count; j++)
                        {
                            //skipping base currency from conversion present in fromcurrency and tocurrency
                            if (currencyDetails[i].CurrencyId != baseCurrenyId && currencyDetails[j].CurrencyId != baseCurrenyId)
                            {
                                GenerateExchangeRateModel generateExchangeRate = new GenerateExchangeRateModel();
                                generateExchangeRate.ToCurrencyId = currencyDetails[i].CurrencyId;
                                generateExchangeRate.FromCurrencyId = currencyDetails[j].CurrencyId;
                                generateExchangeRate.Date = exchangeRateList.FirstOrDefault().Date;
                                generateExchangeRate.Rate = (double)(exchangeRateDetails.FirstOrDefault(x => x.ToCurrency == generateExchangeRate.ToCurrencyId && x.FromCurrency == baseCurrenyId).Rate /
                                                            exchangeRateDetails.FirstOrDefault(y => y.FromCurrency == baseCurrenyId && y.ToCurrency == generateExchangeRate.FromCurrencyId).Rate);

                                exchangeRateDetails.Add(AddExchangeRateAfterManipulation(generateExchangeRate, userId));
                            }
                        }

                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public ExchangeRateDetail AddExchangeRateAfterManipulation(GenerateExchangeRateModel exchangeRateViewModel, string userId)
        {
            ExchangeRateDetail exchangeRate = new ExchangeRateDetail();
            exchangeRate.CreatedById = userId;
            exchangeRate.CreatedDate = DateTime.UtcNow;
            exchangeRate.Date = exchangeRateViewModel.Date;
            exchangeRate.FromCurrency = exchangeRateViewModel.FromCurrencyId;
            exchangeRate.ToCurrency = exchangeRateViewModel.ToCurrencyId;
            exchangeRate.Rate = (decimal)exchangeRateViewModel.Rate;
            exchangeRate.IsDeleted = false;

            return exchangeRate;
        }

        public ExchangeRateDetail AddReverseExchangeRateAfterManipulation(GenerateExchangeRateModel GenerateExchangeRate, string userId)
        {
            ExchangeRateDetail exchangeRate = new ExchangeRateDetail();
            exchangeRate.CreatedById = userId;
            exchangeRate.CreatedDate = DateTime.UtcNow;
            exchangeRate.Date = GenerateExchangeRate.Date;
            exchangeRate.ToCurrency = GenerateExchangeRate.FromCurrencyId;
            exchangeRate.FromCurrency = GenerateExchangeRate.ToCurrencyId;
            exchangeRate.Rate = 1 / (decimal)GenerateExchangeRate.Rate;
            exchangeRate.IsDeleted = false;

            return exchangeRate;
        }
    }
}