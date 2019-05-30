using DataAccess.DbEntities;
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
        Task<APIResponse> GetAllVouchersByOfficeId(int officeId);
        Task<APIResponse> GetAllVoucherDetailsByFilter(VoucherFilterModel filterModel);

        Task<APIResponse> GetAllVoucherType();

        Task<APIResponse> AddVoucherDetail(VoucherDetailModel model);

        Task<APIResponse> EditVoucherDetail(VoucherDetailModel model);
        Task<APIResponse> DeleteVoucherDetail(int VoucherNo, string ModifiedById);

        Task<APIResponse> GetJouranlVoucherDetails();
        Task<APIResponse> GetAllAccountCode();
		Task<APIResponse> GetAllControlLevelAccountCode();

		Task<APIResponse> GetAllVoucherTransactionDetailByVoucherNo(int VoucherNo);
        //Task<APIResponse> AddVoucherTransactionDetail(List<VoucherTransactionModel> model, string userId);
        Task<APIResponse> AddVoucherTransactionDetail(List<VoucherTransactionModel> model, string UserId);

        Task<APIResponse> EditVoucherTransactionDetail(VoucherTransactionModel model);
        //Task<APIResponse> GetAllVoucherDocumentDetailByVoucherNo(int VoucherNo);
        // Task<APIResponse> AddVoucherDocumentDetail(VoucherDocumentDetailModel model);
        //Task<APIResponse> DeleteVoucherDocumentDetail(int DocumentId, string ModifiedById);	
        Task<APIResponse> GetTrailBlanceDetailsByCondition(LedgerModels model);
        //Task<APIResponse> GetTrailBlanceDetailsByCondition(int? OfficeId = null, DateTime? Fromdate = null, DateTime? Todate = null, int? CurrencyId = null, int? RecordType = null);
        Task<APIResponse> GetAllLedgerDetailsByCondition(LedgerModels model);
        //Task<APIResponse> GetJouranlVoucherDetailsByCondition(int? CurrencyId = 2, DateTime? fromdate = null, DateTime? todate = null, int? officeid = null, int? RecordType = 1);
        Task<APIResponse> GetJouranlVoucherDetailsByCondition(JournalViewModel model);

        Task<APIResponse> GetAllVoucherTransactionDetailByBudgetLine(long projectId, long budgetLineId);
       // Task<APIResponse> GetProjectAndBudgetLine();
        Task<APIResponse> AddNotesDetails(NotesMasterModel model);
        Task<APIResponse> EditNotesDetails(NotesMasterModel model);
        Task<APIResponse> GetAllNotesDetails();
		//Task<APIResponse> GetBlanceSheetDetails(int? financialyearid, int? currencyid, int? financialreporttype);
		Task<APIResponse> GetBlanceSheetDetails(FinancialReportModel model);
		//Task<APIResponse> GetDetailsOfNotes(int? GetDetailsOfNotes, int? financialyearid, int? currencyid);

		//Task<APIResponse> GetDetailsOfNotesReportData(int? financialyearid, int? currencyid);

        Task<APIResponse> DeleteVoucherTransactionDetail(int transactionId, string modifiedById);
        Task<APIResponse> GetAllUserNotifications(string userid);

        Task<APIResponse> AddCategoryPopulator(CategoryPopulatorModel model, string UserId);
        Task<APIResponse> EditCategoryPopulator(CategoryPopulatorModel model, string UserId);

        Task<APIResponse> DeleteCategoryPopulator(int categoryPopulatorId, string modifiedById);

        Task<APIResponse> GetAllCategoryPopulator();
		//Task<APIResponse> GetExchangeGainOrLossAmount(ExchangeGainOrLossFilterModel model);
        Task<APIResponse> GetAllVoucherByJouranlId(JournalVoucherFilterModel JournalVoucherFilter);
        Task<APIResponse> GetLevelFourAccountCode();
        Task<APIResponse> AddExchangeGainLossVoucher(ExchangeGainLossVoucher model);
        Task<APIResponse> GetExchangeGainLossVoucherList(int OfficeId);
        Task<APIResponse> DeleteExchangeGainLossVoucher(long VoucherNo, string UserId);
        //Task<APIResponse> GetAllAccountCodeByVoucherNo(ExchangeGainOrLossFilterModel ExchangeGainOrLossFilter);    
        Task<APIResponse> GetAllInputLevelAccountCode();
        Task<APIResponse> GenerateSalaryVoucher(EmployeeSalaryVoucherModel EmployeeSalaryVoucher);
        Task<APIResponse> GetEmployeeSalaryVoucher(int EmployeeId, int Month, int Year);
        Task<APIResponse> ReverseEmployeeSalaryVoucher(long VoucherNo, string UserId);
        Task<APIResponse> DisapproveEmployeeApprovedSalary(DisapprovePayrollModel model, string UserId);
        Task<APIResponse> GetVoucherDetailByVoucherNo(long VoucherNo);
        Task<APIResponse> DeleteVoucherTransactions(int VoucherId, string modifiedById);
        Task<APIResponse> AddVoucherTransactionConvertedToExchangeRate(VoucherTransactionModel model, List<ExchangeRate> exchangeRate);
    }
}
