using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IExchangeRate
    {
        Task<APIResponse> AddExchangeRate(ExchangeRateModel model);
        Task<APIResponse> EditExchangeRate(ExchangeRateModel model);
        Task<APIResponse> GetAllExchangeRate();
        //Task<APIResponse> GetExchangeRateByDate(int currencyFromCode, int currenctToCode, DateTime? date);
    }
}
