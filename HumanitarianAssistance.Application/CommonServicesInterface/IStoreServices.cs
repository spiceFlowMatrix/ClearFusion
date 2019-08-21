using System.Collections.Generic;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IStoreServices
    {
        ApiResponse AddEditTransactionList(AddEditTransactionModel voucherTransactions, string userId);
        Task<ApiResponse> AddVoucherNewDetail(VoucherDetailModel model);
        bool CheckExchangeRateIsPresent(List<int> currencyList, List<ExchangeRateDetail> exchangeRates);
    }
}