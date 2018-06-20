using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
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
        Task<APIResponse> AddSalaryHead(SalaryHeadModel model);
        Task<APIResponse> EditSalaryHead(SalaryHeadModel model);
        Task<APIResponse> GetAllSalaryHead();
        Task<APIResponse> GetAllPensionRate();
        Task<APIResponse> AddPensionRate(EmployeePensionRateModel model, string UserId);
        Task<APIResponse> EditPensionRate(EmployeePensionRateModel model, string UserId);
        Task<APIResponse> AddAppraisalQuestion(AppraisalQuestionModel model, string UserId);
        Task<APIResponse> EditAppraisalQuestion(AppraisalQuestionModel model, string UserId);
        Task<APIResponse> GetAppraisalQuestions(int OfficeId);
        Task<APIResponse> AddEmployeeAppraisalDetails(EmployeeAppraisalDetailsModel model, string UserId);
        Task<APIResponse> EditEmployeeAppraisalDetails(EmployeeAppraisalDetailsModel model, string UserId);
        Task<APIResponse> GetAllEmployeeAppraisalDetails(int OfficeId);
        Task<APIResponse> AddEmployeeAppraisalMoreDetails(EmployeeAppraisalDetailsModel model, string UserId);
        Task<APIResponse> EditEmployeeAppraisalMoreDetails(EmployeeAppraisalDetailsModel model, string UserId);
        Task<APIResponse> GetEmployeeDetailByOfficeId(int OfficeId);
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
		Task<APIResponse> GetAllEmployeeAppraisalDetailsByEmployeeId(int EmployeeId);

	}
}
