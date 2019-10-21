using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;

namespace HumanitarianAssistance.Service.interfaces.AccountingNew
{
    public interface IAccountBalance
    {
        Task<APIResponse> GetNoteBalancesByHeadType(int headTypeId, int toCurrency, DateTime reportDate);
        Task<APIResponse> GetNoteBalancesByHeadType(int headTypeId, int toCurrencyId, DateTime reportStartDate, DateTime reportEndDate);

        Task<APIResponse> GetAccountBalancesById(List<long> accountIds, int toCurrencyId, DateTime reportDate);
        Task<APIResponse> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId, DateTime reportDate);
        Task<APIResponse> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId, DateTime reportStartDate, DateTime reportEndDate);
        Task<APIResponse> GetExchangeGainLossReport(ExchangeGainLossFilterModel exchangeGainLossFilterModel);
        Task<APIResponse> GetAccountBalancesById(List<long?> accountIds, DateTime transactionExchangeDate, int toCurrencyId, DateTime reportStartDate, DateTime reportEndDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList);
        Task<APIResponse> GetAccountBalancesById(List<long?> accountIds, int toCurrencyId, DateTime reportStartDate, DateTime reportEndDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList);

        Task<APIResponse> SaveGainLossAccountList(List<long> accountIds);
        Task<APIResponse> GetExchangeGainLossFilterAccountList();

        Task<APIResponse> GetDetailOfNotes(DetailOfNotesFilterModel model);

    }
}
