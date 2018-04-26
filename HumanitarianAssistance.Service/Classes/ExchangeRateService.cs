using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class ExchangeRateService : IExchangeRate
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public ExchangeRateService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> AddExchangeRate(ExchangeRateModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                ExchangeRate obj = _mapper.Map<ExchangeRate>(model);
                await _uow.ExchangeRateRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditExchangeRate(ExchangeRateModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var exchangerateinfo = await _uow.ExchangeRateRepository.FindAsync(x => x.ExchangeRateId == model.ExchangeRateId && x.IsDeleted == false);
                exchangerateinfo.ModifiedById = model.ModifiedById;
                exchangerateinfo.ModifiedDate = DateTime.UtcNow;
                exchangerateinfo.Date = model.Date;
                exchangerateinfo.FromCurrency = model.FromCurrency;
                exchangerateinfo.ToCurrency = model.ToCurrency;
                exchangerateinfo.Rate = model.Rate;                
                await _uow.ExchangeRateRepository.UpdateAsyn(exchangerateinfo);
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

        public async Task<APIResponse> GetAllExchangeRate()
        {
            APIResponse response = new APIResponse();
            try
            {
                var exchangeratelist = await _uow.GetDbContext().ExchangeRates.Include(x => x.CurrencyFrom).Include(y => y.CurrencyTo).Where(x=>x.IsDeleted == false)
                    .Select(x => new ExchangeRateModel
                    {
                        ExchangeRateId = x.ExchangeRateId,
                        Date = x.Date.Date.ToLocalTime(),						
                        FromCurrencyName = x.CurrencyFrom.CurrencyCode,
                        ToCurrencyName = x.CurrencyTo.CurrencyCode,
                        Rate = x.Rate,
                        FromCurrency = x.FromCurrency,
                        ToCurrency = x.ToCurrency
                    }).ToListAsync();

                response.data.ExchangeRateList = exchangeratelist;
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

        public async Task<APIResponse> GetExchangeRateByDate(int currencyFromCode, int currenctToCode,DateTime?date)
        {
            APIResponse response = new APIResponse();
            try
            {
                var exchangerate = await Task.Run(() => 
                    _uow.GetDbContext().
ExchangeRates.Include(x => x.CurrencyFrom).Include(y => y.CurrencyTo).Where(x => x.ToCurrency == currenctToCode && x.FromCurrency == currencyFromCode && x.IsDeleted == false).FirstOrDefault()
                );


                response.data.CurrenctExchangeRate = exchangerate.Rate;
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
