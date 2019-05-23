using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Service.interfaces
{
   public interface IAccountRecords
    {

        Task<APIResponse> GetBalanceSheet();
        Task<APIResponse> GetVoucherSummaryList(VoucherSummaryFilterModel voucherSummaryFilter);
        Task<APIResponse> GetVoucherTransactionList(TransactionFilterModel model);
    }
}
