using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IVoucherDetail
    {
        Task<APIResponse> GetAllVoucherDetails();

        Task<APIResponse> GetAllVoucherDetailsByFilter(VoucherFilterModel filterModel);

        Task<APIResponse> GetAllVoucherType();

        Task<APIResponse> AddVoucherDetail(VoucherDetailModel model);

        Task<APIResponse> EditVoucherDetail(VoucherDetailModel model);
        Task<APIResponse> DeleteVoucherDetail(int VoucherNo, string ModifiedById);

        Task<APIResponse> GetJouranlVoucherDetails();
        Task<APIResponse> GetAllAccountCode();
        Task<APIResponse> GetAllVoucherTransactionDetailByVoucherNo(int VoucherNo);
        //Task<APIResponse> AddVoucherTransactionDetail(List<VoucherTransactionModel> model, string userId);
        Task<APIResponse> AddVoucherTransactionDetail(VoucherTransactionModel model);

        Task<APIResponse> EditVoucherTransactionDetail(VoucherTransactionModel model);
        Task<APIResponse> GetAllVoucherDocumentDetailByVoucherNo(int VoucherNo);
        Task<APIResponse> AddVoucherDocumentDetail(VoucherDocumentDetailModel model);
        Task<APIResponse> DeleteVoucherDocumentDetail(int DocumentId, string ModifiedById);
        Task<APIResponse> GetAllLedgerDetails();
        Task<APIResponse> GetTrailBlanceDetails();
        Task<APIResponse> GetTrailBlanceDetailsByCondition(LedgerModels model);
        //Task<APIResponse> GetTrailBlanceDetailsByCondition(int? OfficeId = null, DateTime? Fromdate = null, DateTime? Todate = null, int? CurrencyId = null, int? RecordType = null);
        Task<APIResponse> GetAllLedgerDetailsByCondition(LedgerModels model);
        //Task<APIResponse> GetJouranlVoucherDetailsByCondition(int? CurrencyId = 2, DateTime? fromdate = null, DateTime? todate = null, int? officeid = null, int? RecordType = 1);
        Task<APIResponse> GetJouranlVoucherDetailsByCondition(JournalViewModel model);

        Task<APIResponse> GetAllVoucherTransactionDetailByBudgetLine(long projectId, long budgetLineId);
        Task<APIResponse> GetProjectAndBudgetLine();
        Task<APIResponse> AddNotesDetails(NotesMasterModel model);
        Task<APIResponse> EditNotesDetails(NotesMasterModel model);
        Task<APIResponse> GetAllNotesDetails();
        Task<APIResponse> GetBlanceSheetDetails(int? financialyearid, int? currencyid, int? financialreporttype);
        Task<APIResponse> GetDetailsOfNotes(int? financialyearid, int? currencyid);

        Task<APIResponse> GetDetailsOfNotesReportData(int? accountType, int? financialyearid, int? currencyid);

        Task<APIResponse> DeleteVoucherTransactionDetail(int transactionId, string modifiedById);
        Task<APIResponse> GetAllUserNotifications(string userid);

        Task<APIResponse> AddCategoryPopulator(CategoryPopulatorModel model, string UserId);
        Task<APIResponse> EditCategoryPopulator(CategoryPopulatorModel model, string UserId);

        Task<APIResponse> DeleteCategoryPopulator(int categoryPopulatorId, string modifiedById);

        Task<APIResponse> GetAllCategoryPopulator();
    }
}
