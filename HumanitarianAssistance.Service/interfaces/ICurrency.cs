using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface ICurrency
    {
        Task<APIResponse> AddCurrency(CurrencyModel model);
        Task<APIResponse> EditCurrency(CurrencyModel model);
        Task<APIResponse> GetAllCurrency();
        Task<APIResponse> GetCurrencyByCurrencyCode(string CurrencyCode);
    }
}
