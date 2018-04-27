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

	}
}
