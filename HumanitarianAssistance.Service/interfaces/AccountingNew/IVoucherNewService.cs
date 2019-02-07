using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.AccountingNew
{
    public interface IVoucherNewService
    {
        Task<APIResponse> GetAllNewVoucherList(VoucherNewFilterModel voucherNewFilterModel);
        Task<APIResponse> GetVoucherDetailByVoucherNo(long id);
        Task<APIResponse> AddVoucherNewDetail(VoucherDetailModel model);
        Task<APIResponse> EditVoucherNewDetail(VoucherDetailModel model);
        Task<APIResponse> GetAllTransactionsByVoucherId(long VoucherNo);
        Task<APIResponse> EditTransactionDetail(VoucherTransactionsModel voucherTransactions, string UserId);
        Task<APIResponse> DeleteTransactionById(long transactionId);
        Task<APIResponse> AddTransactionDetail(List<VoucherTransactionsModel> voucherTransactionsList, string userId);
        APIResponse AddEditTransactionList(AddEditTransactionModel voucherTransactions, string userId);
        Task<APIResponse> VerifyVoucher(long voucherId, string userId);
        Task<APIResponse> CreateGainLossTransaction(ExchangeGainLossVoucherDetails transactionDetail, string userId);
    }
}
