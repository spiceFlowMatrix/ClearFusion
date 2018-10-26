using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IEmployeeDetail
    {
        Task<APIResponse> GetAllEmployeeHistoryOutsideOrganization(int EmployeeId);
        Task<APIResponse> AddEmployeeHistoryOutsideOrganization(EmployeeHistoryOutsideOrganizationModel model, string UserId);
        Task<APIResponse> EditEmployeeHistoryOutsideOrganization(EmployeeHistoryOutsideOrganizationModel model, string UserId);
        Task<APIResponse> DeleteEmployeeHistoryOutsideOrganization(EmployeeHistoryOutsideOrganizationModel model, string UserId);
        Task<APIResponse> GetAllEmployeeHistoryOutsideCountry(int EmployeeId);
        Task<APIResponse> AddEmployeeHistoryOutsideCountry(EmployeeHistoryOutsideOrganizationModel model, string UserId);
        Task<APIResponse> EditEmployeeHistoryOutsideCountry(EmployeeHistoryOutsideOrganizationModel model, string UserId);
        Task<APIResponse> DeleteEmployeeHistoryOutsideCountry(EmployeeHistoryOutsideOrganizationModel model, string UserId);
        Task<APIResponse> GetAllEmployeeRelativeInformation(int EmployeeId);
        Task<APIResponse> AddEmployeeRelativeInformation(EmployeeRelativeInfoModel model, string UserId);
        Task<APIResponse> EditEmployeeRelativeInformation(EmployeeRelativeInfoModel model, string UserId);
        Task<APIResponse> DeleteEmployeeRelativeInformation(EmployeeRelativeInfoModel model, string UserId);
        Task<APIResponse> GetAllEmployeeInfoReferences(int EmployeeId);
        Task<APIResponse> AddEmployeeInfoReferences(EmployeeRelativeInfoModel model, string UserId);
        Task<APIResponse> EditEmployeeInfoReferences(EmployeeRelativeInfoModel model, string UserId);
        Task<APIResponse> DeleteEmployeeInfoReferences(EmployeeRelativeInfoModel model, string UserId);
        Task<APIResponse> GetAllEmployeeOtherSkills(int EmployeeId);
        Task<APIResponse> AddEmployeeOtherSkills(EmployeeOtherSkillsModel model, string UserId);
        Task<APIResponse> EditEmployeeOtherSkills(EmployeeOtherSkillsModel model, string UserId);
        Task<APIResponse> DeleteEmployeeOtherSkills(EmployeeOtherSkillsModel model, string UserId);
        Task<APIResponse> GetAllEmployeeSalaryBudgets(int EmployeeId);
        Task<APIResponse> AddEmployeeSalaryBudgets(EmployeeSalaryBudgetModel model, string UserId);
        Task<APIResponse> EditEmployeeSalaryBudgets(EmployeeSalaryBudgetModel model, string UserId);
        Task<APIResponse> DeleteEmployeeSalaryBudgets(EmployeeSalaryBudgetModel model, string UserId);
        Task<APIResponse> GetAllEmployeeEducations(int EmployeeId);
        Task<APIResponse> AddEmployeeEducations(EmployeeEducationsModel model, string UserId);
        Task<APIResponse> EditEmployeeEducations(EmployeeEducationsModel model, string UserId);
        Task<APIResponse> DeleteEmployeeEducations(EmployeeEducationsModel model, string UserId);
        Task<APIResponse> GetAllEmployeeSalaryAnalyticalInfo(int EmployeeId);
        Task<APIResponse> AddEmployeeSalaryAnalyticalInfo(EmployeeSalaryAnalyticalInfoModel model, string UserId);
        Task<APIResponse> EditEmployeeSalaryAnalyticalInfo(EmployeeSalaryAnalyticalInfoModel model, string UserId);
        Task<APIResponse> DeleteEmployeeSalaryAnalyticalInfo(EmployeeSalaryAnalyticalInfoModel model, string UserId);


        Task<APIResponse> GetAllEmployeeHealthInfo(int EmployeeId);
        Task<APIResponse> AddEmployeeHealthInfo(EmployeeHealthInformationModel model, string UserId);
        Task<APIResponse> EditEmployeeHealthInfo(EmployeeHealthInformationModel model, string UserId);

        Task<APIResponse> GetEmployeeHealthQuestion(int EmployeeId);
        Task<APIResponse> AddEmployeeHealthQuestion(EmployeeHealthQuestion model, string UserId);
        Task<APIResponse> EditEmployeeHealthQuestion(EmployeeHealthQuestion model, string UserId);
        Task<APIResponse> DeleteEmployeeHealthQuestion(EmployeeHealthQuestion model, string UserId);

        Task<APIResponse> GetAllEmployeeLanguages(int EmployeeId);
        Task<APIResponse> AddEmployeeLanguages(EmployeeLanguages model, string UserId);
        Task<APIResponse> EditEmployeeLanguages(EmployeeLanguages model, string UserId);
        Task<APIResponse> RemoveEmployeeLanguages(EmployeeLanguages model, string UserId);



    }
}
