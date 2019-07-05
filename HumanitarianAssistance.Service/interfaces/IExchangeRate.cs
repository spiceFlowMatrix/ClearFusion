using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
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
        Task<APIResponse> GetExchangeGainOrLossAmount(ExchangeGainOrLossFilterModel model);
        Task<APIResponse> GetExchangeGainOrLossTransactionAmount(ExchangeGainOrLossTransactionFilterModel model);
        Task<APIResponse> GenerateExchangeRates(List<GenerateExchangeRateViewModel> GenerateExchangeRateModel, string userId);
        APIResponse GetSavedExchangeRates(ExchangeRateVerificationFilter filter);
        Task<APIResponse> GetExchangeRatesDetail(ExchangeRateDetailModel exchangeRateDetailModel);
        Task<APIResponse> SaveExchangeRatesForOffice(OfficeExchangeRateViewModel officeExchangeRateViewModel, string userId);
        Task<APIResponse> VerifyExchangeRates(DateTime ExchangeRateDate, string userId);
        Task<APIResponse> DeleteExchangeRates(DateTime ExchangeRateDate, string userId);
    }
}
