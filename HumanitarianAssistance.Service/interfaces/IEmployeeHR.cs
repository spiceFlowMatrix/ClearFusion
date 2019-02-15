using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IEmployeeHR
    {
        Task<APIResponse> AddEmployeeAttendanceDetails(List<EmployeeAttendanceModel> modellist, string UserId);
        Task<APIResponse> GetAllEmployeeMonthlyPayrollListApproved(int officeid, int month, int year, int paymentType);
        //Task<APIResponse> GetAllEmployeeMonthlyPayrollList(int officeid, int currencyid, int month, int year, int paymentType);
        Task<APIResponse> GetAllPayrollMonthlyHourDetail(PayrollHourFilterModel model);
        Task<APIResponse> AddPayrollMonthlyHourDetail(PayrollMonthlyHourDetailModel model);
        Task<APIResponse> EditPayrollMonthlyHourDetail(PayrollMonthlyHourDetailModel model);
        Task<APIResponse> GetAllEmployeeMonthlyPayrollList(int officeid,int currencyid, int month, int year, int paymentType);
        Task<APIResponse> EmployeePaymentTypeReportForSaveOnly(List<EmployeePaymentTypeModel> model, string userid);
        Task<APIResponse> EmployeePaymentTypeReport(List<EmployeePaymentTypeModel> model, string userid);
        Task<APIResponse> EmployeePensionReport(PensionReportModel model);
        Task<APIResponse> GetAllEmployeePension(int OfficeId);
        Task<APIResponse> AddEmployeeLeaveDetails(List<AssignLeaveToEmployeeModel> model);

        string TransferDataForAttendance();
		Task<string> TransferDataForVoucherTransaction2008();

		Task<APIResponse> EmployeeSalaryTaxDetails(SalaryTaxModel model);
        Task<APIResponse> GetEmployeePensionHistoryDetail(int EmployeeId, int OfficeId);
        Task<APIResponse> EditEmployeeSalaryAccountDetail(List<EmployeePayrollAccountModel> model, string userid);
        Task<APIResponse> GetPrimarySalaryHeads(int EmployeeId);
        Task<APIResponse> GetAllLanguages();
        Task<APIResponse> GetJobCode(int officeId);
        string TransformExchangeRatesToFromCurrency();

        string OfficeHours(int iYear);
    }
}
