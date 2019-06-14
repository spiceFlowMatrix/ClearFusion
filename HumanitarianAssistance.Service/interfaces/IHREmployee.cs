using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IHREmployee
    {
        Task<APIResponse> AddNewEmployee(EmployeeDetailModel model);
        Task<APIResponse> EditEmployeeDetail(EmployeeDetailModel model);
        Task<APIResponse> GetAllEmployeeDetail();
        Task<APIResponse> AddJobHiringDetail(JobHiringDetailsModel model);
        Task<APIResponse> EditJobHiringDetail(JobHiringDetailsModel model);
        Task<APIResponse> GetAllJobHiringDetails(int OfficeId);
        Task<APIResponse> AddInterviewScheduleDetails(List<InterviewScheduleModel> model, string CreatedById);
        //Task<APIResponse> GetAllProspectiveEmployee();
        Task<APIResponse> GetEmployeeDetailsByEmployeeId(int EmployeeId);
        Task<APIResponse> GetEmployeeGeneralInformationById(int employeeid);
        //Task<APIResponse> AddInterviewFeedbackDetails(InterviewFeedbackDetailsModel model);
        //Task<APIResponse> GetAllInterviewFeedbackByScheduleId(int? ScheduleId);
        Task<APIResponse> AddEmployeeSalaryDetail(List<EmployeePayrollModel> model, string userid);
        Task<APIResponse> EditEmployeeSalaryDetail(List<EmployeePayrollModel> model, string userid);
        Task<APIResponse> GetEmployeeSalaryDetailsByEmployeeId(int EmployeeId);
        Task<APIResponse> AddDocumentDetail(EmployeeDocumentDetailModel model);
        Task<APIResponse> DeleteDocumentDetail(int documentid, string UserId);
        Task<APIResponse> GetAllDocumentDetailByEmployeeId(int EmployeeId);
        Task<APIResponse> AddEmployeeHistoryDetail(EmployeeHistoryDetailModel model);
        Task<APIResponse> EditEmployeeHistoryDetail(EmployeeHistoryDetailModel model);
        Task<APIResponse> GetAllEmployeeHistoryByEmployeeId(int EmployeeId);
        Task<APIResponse> DeleteEmployeeHistoryDetail(int HistoryId, string UserId);
        Task<APIResponse> AddEmployeeProfessionalDetail(EmployeeProfessionalDetailModel model);
        Task<APIResponse> EditEmployeeProfessionalDetail(EmployeeProfessionalDetailModel model);

        Task<APIResponse> AddPayrollMonthlyHourDetail(PayrollMonthlyHourDetailModel model);
        Task<APIResponse> EditPayrollMonthlyHourDetail(PayrollMonthlyHourDetailModel model);
        Task<APIResponse> GetAllPayrollMonthlyHourDetail();
        Task<APIResponse> GetAllEmployeeAssignLeave(int EmployeeId);
        Task<APIResponse> AssignLeaveToEmployeeDetail(AssignLeaveToEmployeeModel model);
        Task<APIResponse> EditAssignLeaveToEmployee(AssignLeaveToEmployeeModel model);
        Task<APIResponse> AddEmployeeAttendanceDetails(List<EmployeeAttendanceModel> modellist, string UserId);
        Task<APIResponse> GetEmployeeProfessionalDetail(int EmployeeId);
        Task<APIResponse> GetAllEmployeeDetail(int EmployeeType, int officeid);
        Task<APIResponse> GetEmployeeAttendanceDetails(EmployeeAttendanceFilterModel employeeFilter);
        //Task<APIResponse> AddEmployeeHealthDetail(EmployeeHealthInformationModel model);
        //Task<APIResponse> EditEmployeeHealthDetail(EmployeeHealthInformationModel model);
        Task<APIResponse> GetAllEmployeeHealthDetailByEmployeeId(int employeeid);
        Task<APIResponse> ChangeEmployeeImage(ChangeEmployeeImage model);
        //Task<APIResponse> GetAllActiveEmployeeForAttendance();
        Task<APIResponse> GetAllEmployeesAttendanceByDate(EmployeeAttendanceFilterViewModel model);
        Task<APIResponse> EditEmployeeAttendanceByDate(EmployeeAttendanceModel model, string userid);

        //Alpit
        Task<APIResponse> GetProspectiveEmployeesByProfessionId(int ProfessionId);
        Task<APIResponse> GetAllScheduledProspectiveEmployee();

        Task<APIResponse> GetAllJobGrade();
        Task<APIResponse> GetAllScheduledEmployeeList();
        Task<APIResponse> GetAllApprovedEmployeeList();
        Task<APIResponse> AddEmployeeApplyLeaveDetail(List<EmployeeApplyLeaveModel> model, string userid);

        Task<APIResponse> GetEmployeeApplyLeaveDetailById(int employeeid);

        Task<APIResponse> InterviewApprovals(List<InterviewScheduleModel> model, int approvalId, string userid);

        Task<APIResponse> GetAllEmployeeApplyLeaveList(int officeid);
        //Task<APIResponse> ApproveEmployeeLeave(int applyleaveid, string userid);
        Task<APIResponse> ApproveEmployeeLeave(List<ApproveLeaveModel> model, string userid);
        Task<APIResponse> RejectEmployeeLeave(List<ApproveLeaveModel> model, string userid);
        Task<APIResponse> DeleteApplyEmployeeLeave(int applyleaveid, string userid);

        Task<APIResponse> GetAllEmployeeMonthlyPayrollList(int officeid, int currencyid, int month, int year, int paymentType);
        Task<APIResponse> GetAllEmployeeMonthlyPayrollListApproved(int officeid, int currencyid, int month, int year, int paymentType);


        Task<APIResponse> AddHolidayDetails(HolidayDetailsModel model);
        Task<APIResponse> EditHolidayDetails(HolidayDetailsModel model);
        Task<APIResponse> GetAllHolidayDetails(int officeid);


        Task<APIResponse> AddJobGradeDetail(JobGradeModel model);
        Task<APIResponse> EditJobGradeDetail(JobGradeModel model);
        Task<APIResponse> EditEmployeeAssignLeave(EditAssignLeaveToEmployeeModel model);

        Task<APIResponse> DeleteHolidayDetails(long holidayId, string userid);
        Task<APIResponse> GetAllDisableCalanderDate(int employeeid, int OfficeId);

        //Task<APIResponse> MonthlyEmployeeAttendanceReport(int employeeid, int year, int month, int OfficeId);
        Task<APIResponse> MonthlyEmployeeAttendanceReport(MonthlyEmployeeAttendanceReportModel model);

        Task<APIResponse> GetAllDateforDisableCalenderDate(int OfficeId);

        Task<APIResponse> GetAllHolidayWeeklyDetails(int officeid);

        Task<APIResponse> OnPostExport();
        Task<APIResponse> EmployeesSalarySummary(EmployeeSummaryModel model);
        Task<APIResponse> EmployeePaymentTypeReport(List<EmployeePaymentTypeModel> model, string userid);
        Task<APIResponse> EmployeePaymentTypeReportForSaveOnly(List<EmployeePaymentTypeModel> model, string userid);
        //Task<APIResponse> RemoveApprovedList(RemoveApprovedEmployee model, string userid);
        Task<APIResponse> RemoveApprovedList(List<EmployeePaymentTypeModel> model, string userid);
        Task<APIResponse> EmployeePensionReport(PensionModel model);
        Task<APIResponse> GetAllEmployeeProjects(int EmployeeId);
        Task<APIResponse> AssignEmployeeProjectPercentage(List<EmployeeProjectModel> model, string userid);
        Task<APIResponse> GetExchangeRate(ExchangeRateModel model);
        Task<APIResponse> GetAllEmployeeContractType();
        Task<APIResponse> SaveContractContent(ContractTypeModel model, string userid);
        Task<APIResponse> GetAllContractTypeContent(int OfficeId, int EmployeeContractTypeId);
        Task<APIResponse> GetSelectedEmployeeContract(int OfficeId, int ProjectId, int BudgetLineId, int EmployeeId);
        Task<APIResponse> GetSelectedEmployeeContractByEmployeeId(int EmployeeId);

        Task<APIResponse> GetEmployeeSalaryDetails(int OfficeId, int year, int month, int EmployeeId);
        Task<APIResponse> EmployeeTaxCalculation(SalaryTaxViewModel model);
        Task<APIResponse> EmployeeSalaryTaxDetails(SalaryTaxModel model);
        Task<APIResponse> GetAllAdvancesByOfficeId(int OfficeId, int month, int year);
        Task<APIResponse> EditAdvances(AdvancesModel model, string UserId);
        Task<APIResponse> AddAdvances(AdvancesModel model, string UserId);
        Task<APIResponse> ApproveAdvances(AdvancesModel model, string UserId);
        Task<APIResponse> RejectAdvances(AdvancesModel model, string UserId);
        Task<APIResponse> AddInterviewDetails(InterviewDetailModel model, string UserId);
        Task<APIResponse> EditInterviewDetails(InterviewDetailModel model, string UserId);
        Task<APIResponse> GetAllInterviewDetails();
        Task<APIResponse> AddTechnicalQuestions(InterviewTechnicalQuestionsModel model, string UserId);
        Task<APIResponse> AddEmployeeContractDetails(EmployeeContract model, string UserId);
        Task<APIResponse> RemoveEmployeeContractDetails(int employeeContractId, string UserId);

    }
}
