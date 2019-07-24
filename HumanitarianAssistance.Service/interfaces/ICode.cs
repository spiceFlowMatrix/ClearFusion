using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface ICode
    {
        Task<APIResponse> GetAllCountry();
        Task<APIResponse> GetAllProvinceDetails(int CountryId);
        Task<APIResponse> GetAllNationality();
        Task<APIResponse> GetAllQualification();
        Task<APIResponse> GetAllInterviewRoundList();
        Task<APIResponse> AddLeaveReasonDetail(LeaveReasonDetailModel model);
        Task<APIResponse> GetAllLeaveReasonList();
        Task<APIResponse> AddFinancialYearDetail(FinancialYearDetailModel model);
        Task<APIResponse> GetAllFinancialYearDetail();
        Task<APIResponse> AddBudgetLineType(BudgetLineTypeModel model);
        Task<APIResponse> GetBudgetLineTypes();

        Task<APIResponse> GetAllEmployeeType();
        Task<APIResponse> EditLeaveReasonDetail(LeaveReasonDetailModel model);
        Task<APIResponse> GetDepartmentsByOfficeId(int OfficeId);
        Task<APIResponse> EditFinancialYearDetail(FinancialYearDetailModel model);
        Task<APIResponse> GetCurrentFinancialYear();

        Task<APIResponse> AddDepartmentDetails(DepartmentModel model);
        Task<APIResponse> EditDepartmentDetails(DepartmentModel model);
        Task<APIResponse> GetAllDepartment();

        Task<APIResponse> AddQualificationDetails(QualificationDetailsModel model);
        Task<APIResponse> EditQualifactionDetails(QualificationDetailsModel model);
        Task<APIResponse> DeleteQualificationDetails(QualificationDetailsModel model);

        Task<APIResponse> AddSalaryHead(SalaryHeadModel model);
        Task<APIResponse> EditSalaryHead(SalaryHeadModel model);
        Task<APIResponse> DeleteSalaryHead(SalaryHeadModel model);
        Task<APIResponse> GetAllSalaryHead();
        Task<APIResponse> GetAllPensionRate();
        Task<APIResponse> AddPensionRate(EmployeePensionRateModel model, string UserId);
        Task<APIResponse> EditPensionRate(EmployeePensionRateModel model, string UserId);
        Task<APIResponse> AddAppraisalQuestion(AppraisalQuestionModel model, string UserId);
        Task<APIResponse> EditAppraisalQuestion(AppraisalQuestionModel model, string UserId);
        Task<APIResponse> GetAppraisalQuestions();
        Task<APIResponse> AddEmployeeAppraisalDetails(EmployeeAppraisalDetailsModel model, string UserId);
        Task<APIResponse> EditEmployeeAppraisalDetails(EmployeeAppraisalDetailsModel model, string UserId);
        Task<APIResponse> GetAllEmployeeAppraisalDetails(int OfficeId);
        Task<APIResponse> AddEmployeeAppraisalMoreDetails(EmployeeAppraisalDetailsModel model, string UserId);
        Task<APIResponse> EditEmployeeAppraisalMoreDetails(EmployeeAppraisalDetailsModel model, string UserId);
        Task<APIResponse> GetEmployeeDetailByOfficeId(int OfficeId);
        Task<APIResponse> GetAllEmployeeList();
        Task<APIResponse> GetEmployeeDetailByEmployeeId(int EmployeeId);
        Task<APIResponse> GetAllEmployeeAppraisalMoreDetails(int OfficeId);
        Task<APIResponse> AddInterviewTechnicalQuestions(InterviewTechnicalQuestions model, string UserId);
        Task<APIResponse> EditInterviewTechnicalQuestions(InterviewTechnicalQuestions model, string UserId);
        Task<APIResponse> GetAllInterviewTechnicalQuestionsByOfficeId(int OfficeId);
        Task<APIResponse> AddExitInterview(ExitInterviewModel model, string UserId);
        Task<APIResponse> EditExitInterview(ExitInterviewModel model, string UserId);
        Task<APIResponse> GetAllExitInterview();
        Task<APIResponse> ApproveEmployeeAppraisalRequest(int EmployeeAppraisalDetailsId, string UserId);
        Task<APIResponse> RejectEmployeeAppraisalRequest(int EmployeeAppraisalDetailsId, string UserId);
        Task<APIResponse> ApproveEmployeeEvaluationRequest(int EmployeeEvaluationId, string UserId);
        Task<APIResponse> ApproveEmployeeInterviewRequest(int InterviewDetailsId, string UserId);
        Task<APIResponse> RejectEmployeeEvaluationRequest(int EmployeeEvaluationId, string UserId);
        Task<APIResponse> RejectEmployeeInterviewRequest(int InterviewDetailsId, string UserId);

        Task<APIResponse> DeleteExitInterview(int existInterviewDetailsId, string UserId);
        Task<APIResponse> GetAllEmployeeAppraisalDetailsByEmployeeId(int EmployeeId, DateTime CurrentAppraisalDate);

        Task<APIResponse> GetSalaryTaxReportContentDetails(int officeId);
        Task<APIResponse> AddSalaryTaxReportContentDetails(SalaryTaxReportContent model, string UserId);
        Task<APIResponse> EditSalaryTaxReportContentDetails(SalaryTaxReportContent model, string UserId);
        Task<APIResponse> GetEmployeeAdvanceHistoryDetail(long AdvanceID);
        Task<APIResponse> AddPayrollAccountHead(PayrollHeadModel model);
        Task<APIResponse> GetAllPayrollHead();
        Task<APIResponse> UpdatePayrollAccountHead(PayrollHeadModel model);
        Task<APIResponse> DeletePayrollAccountHead(PayrollHeadModel model);
        Task<APIResponse> GetAllDistrictDetailByProvinceId(List<int?> ProvinceId);
        Task<APIResponse> UpdatePayrollAccountHeadAllEmployees(List<PayrollHeadModel> model, string UserId);
        Task<APIResponse> GetApplicationPages();
        Task<APIResponse> AddEditPensionDebitAccount(long accountId, string userId);
        Task<APIResponse> GetPensionDebitAccount();

        #region Attendance Groups
        Task<APIResponse> GetAttendanceGroups();
        Task<APIResponse> AddAttendanceGroups(AttendanceGroupMasterModel model, string userId);
        Task<APIResponse> EditAttendanceGroups(AttendanceGroupMasterModel model, string userId);
        #endregion

        #region Language
        Task<APIResponse> EditLanguage(LanguageModel model, string UserId);
        Task<APIResponse> AddLanguage(LanguageModel model, string UserId);
        Task<APIResponse> DeleteLanguage(LanguageModel model, string UserId);
        Task<APIResponse> GetAllLanguage();
        #endregion
    }
}
