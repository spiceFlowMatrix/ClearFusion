using DataAccess.DbEntities;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanitarianAssistance.Service.APIResponses
{
    public class APIResponse
    {
        public APIResponse()
        {
            data = new data();
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public data data { get; set; }
        public LoggerDetailsModel LoggerDetailsModel { get; set; }
    }

    public class LoggerDetailsModel
    {
        public int LoggerDetailsId { get; set; }
        public int NotificationId { get; set; }
        public bool IsRead { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string LoggedDetail { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Parent
    {
        public dynamic LoggerDetailsModel { get; set; }
    }

    public class data
    {
        public string AspNetUserId { get; set; }
        public RoleViewModel RoleData { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
        public string RoleName { get; set; }
        public double CurrenctExchangeRate { get; set; }
        public double TotalExpenditure { get; set; }

        public double TotalRecivable { get; set; }
        public double TotalPayable { get; set; }
        public double Balance { get; set; }


        public double TotalIncome { get; set; }
        public int TotalPresentDays { get; set; }
        public int TotalAbsentDays { get; set; }

        public bool AttendanceStatus { get; set; }

        public int? OfficeId { get; set; }

        public int? TotalEmployees { get; set; }
        public double? TotalGrossSalary { get; set; }
        public double? TotalDeductions { get; set; }
        public double? TotalAllowances { get; set; }
        //public List<IGrouping<int, CategoryPopulator>> CategoryPopulatorLst { get; set; }
        public List<CategoryPopulator> CategoryPopulatorLst { get; set; }
        public List<int> UserOfficeList { get; set; }
        public List<ExitInterviewModel> ExitInterviewList { get; set; }
        public List<InterviewDetailModel> InterviewDetailList { get; set; }
        public List<AdvanceModel> AdvanceList { get; set; }
        public List<InterviewTechnicalQuestions> InterviewTechnicalQuestionsList { get; set; }
        public List<EmployeeDetailList> EmployeeDetailListData { get; set; }
        public List<EmployeeAppraisalDetailsModel> EmployeeAppraisalDetailsModelLst { get; set; }
        public List<IGrouping<int, EmployeeAppraisalDetailsModel>> EmployeeEvaluationDetailsModelLst { get; set; }
        public List<AppraisalGeneralQuestions> AppraisalList { get; set; }
        public EmployeeTaxReport EmployeeTaxReport { get; set; }
        public List<EmployeeSalarySlipModel> EmployeeSalarySlipModelList { get; set; }
        public List<EmployeeContractModel> EmployeeContractModellst { get; set; }
        public ContractTypeContent ContractTypeContentList { get; set; }
        public List<EmployeeContractType> EmployeeContractTypeList { get; set; }
        public List<EmployeePensionRateModel> EmployeePensionRateList { get; set; }
        public List<BudgetLineEmployees> GetAllEmployeesInBudgetLine { get; set; }
        public List<EmployeeProjectModel> EmployeeProjectList { get; set; }
        public List<EmployeeSummaryDetails> EmployeeSummaryDetailsList { get; set; }
        public ExchangeRate ExchangeRateLists { get; set; }

        public AccountOpendingAndClosingBL AccountOpendingAndClosingBL { get; set; }
        //List Response result
        public List<Roles> RoleList { get; set; }
        //Get All Office
        public IList<OfficeDetailModel> OfficeDetailsList { get; set; }
        public IList<DepartmentModel> Departments { get; set; }
        public IList<UserDetailsModel> UserDetailsList { get; set; }
        public UserDetailsModel UserDetails { get; set; }
        public IList<PermissionsInRolesModel> PermissionsInRolesList { get; set; }
        public IList<PermissionsModel> PermissionsList { get; set; }
        public IList<CurrencyModel> CurrencyList { get; set; }
        public IList<JournalDetailModel> JournalDetailList { get; set; }
        public IList<EmailSettingModel> EmailSettingList { get; set; }
        public IList<EmailTypeModel> EmailTypeList { get; set; }
        public IList<ChartAccountDetailModel> ChartAccountList { get; set; }
        public IList<AccountLevelModel> AccountLevelList { get; set; }
        public IList<AccountTypeModel> AccountTypeList { get; set; }
        public IList<VoucherDetailModel> VoucherDetailList { get; set; }
        public IList<VoucherTypeModel> VoucherTypeList { get; set; }
        public IList<VoucherDocumentDetailModel> VoucherDocumentDetailList { get; set; }

        public IList<JournalVoucherViewModel> JournalVoucherViewList { get; set; }
        public IList<AccountDetailModel> AccountDetailList { get; set; }
        public IList<VoucherTransactionModel> VoucherTransactionList { get; set; }
        public IList<LedgerModel> LedgerList { get; set; }
        public IList<LedgerModel> TrailBlanceList { get; set; }

        public IList<ExchangeRateModel> ExchangeRateList { get; set; }
        public IList<EmployeeDetailModel> EmployeeDetailList { get; set; }
        public IList<ProjectBudget> ProjectBudgetList { get; set; }
        public IList<EmployeeDetailsAllModel> ActiveEmployeeDetailsList { get; set; }
        public IList<EmployeeDetailsAllModel> ProspectiveEmployeeDetailsList { get; set; }
        public IList<EmployeeDetailsAllModel> TerminatedEmployeeDetailsList { get; set; }
        public IList<EmployeeDetailsAllModel> EmployeeDetailsList { get; set; }
        public IList<JobHiringDetailsModel> JobHiringDetailsList { get; set; }
        public IList<ProfessionModel> ProfessionList { get; set; }
        public IList<DesignationModel> DesignationList { get; set; }
        public IList<CountryDetailsModel> CountryDetailsList { get; set; }
        public IList<ProvinceDetailsModel> ProvinceDetailsList { get; set; }
        public IList<NationalityDetailsModel> NationalityDetailsList { get; set; }
        public IList<QualificationDetailsModel> QualificationDetailsList { get; set; }
        public IList<InterViewRoundModel> InterviewRoundList { get; set; }
        public IList<InterviewScheduleForProspectiveEmployeeModel> ISFPEmployeeList { get; set; }
        public IList<ScheduleCandidateModel> ScheduleCandidateList { get; set; }

        public IList<ProjectDetails> ProjectDetailList { get; set; }

        public IList<InterviewFeedbackDetailsModel> InterviewFeedbackDetailsList { get; set; }
        public IList<EmployeeSalaryDetailsModel> EmployeeSalaryDetailsList { get; set; }
        public IList<LeaveReasonDetailModel> LeaveReasonList { get; set; }
        public IList<FinancialYearDetailModel> FinancialYearDetailList { get; set; }
        public IList<TaskMasterModel> TaskMasterList { get; set; }
        public IList<ActivityMasterModel> ActivityMasterList { get; set; }
        public IList<ProjectBudgetLineModel> ProjectBudgetLineList { get; set; }
        public IList<BudgetLineTypeModel> BudgetLineTypeList { get; set; }
        public IList<EmployeeTypeModel> EmployeeTypeList { get; set; }
        public IList<AssignActivityModel> AssignActivityList { get; set; }

        public IList<BudgetReceivableModel> BudgetReceivableList { get; set; }

        public IList<BudgetReceivedAmountModel> BudgetReceivedAmountList { get; set; }
        public IList<EmployeeDocumentDetailModel> EmployeeDocumentList { get; set; }
        public IList<EmployeeHistoryDetailModel> EmployeeHistoryDetailList { get; set; }
        public IList<PayrollMonthlyHourDetailModel> PayrollMonthlyHourList { get; set; }
        public IList<AssignLeaveToEmployeeModel> AssignLeaveToEmployeeList { get; set; }

        public IList<BudgetPayableModel> BudgetPayableList { get; set; }
        public IList<BudgetPayableAmountModel> BudgetPaidAmountList { get; set; }
        public IList<EmployeeProfessionalDetailModel> EmployeeProfessionalList { get; set; }
        public IList<EmployeeAttendanceModel> EmployeeAttendanceList { get; set; }
        public IList<DisplayEmployeeAttendanceModel> DisEmployeeAttendanceList { get; set; }
        public IList<EmployeeHealthInformationModel> EmployeeHealthInfoList { get; set; }

        public ProjectBudgetLinesModel ProjectBudgetLinesModel { get; set; }
        public IList<ProjectDocumentModel> ProjectDocumentList { get; set; }
        public IList<EmployeeApplyLeaveModel> EmployeeApplyLeaveList { get; set; }
        public IList<NotesMasterModel> NotesDetailsList { get; set; }
        public BalanceSheet BalanceSheet { get; set; }
        public IList<CurrentFinancialYear> CurrentFinancialYearList { get; set; }

        public List<DetailsOfNotesModel> DetailsOfNotesList { get; set; }

        //Alpit
        public IList<ScheduleCandidateModel> ScheduledProspectiveEmployee { get; set; }
        public IList<JobGradeModel> JobGradeList { get; set; }

        public IList<InterviewScheduleModel> InterviewApprovedEmployeeList { get; set; }
        //public IList<InterviewScheduleModel> InterviewScheduleList { get; set; }

        public IList<InterviewScheduleModel> InterviewScheduleGeneralAssemblylist { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleDirectorlist { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleGeneralAdminlist { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleFieldOfficelist { get; set; }

        public IList<EmployeeMonthlyPayrollModel> EmployeeMonthlyPayrolllist { get; set; }

        public IList<HolidayDetails> HolidayDetailsList { get; set; }

        public IList<ApplyLeaveModel> ApplyLeaveList { get; set; }
        public IList<MonthlyEmployeeAttendanceModel> MonthlyEmployeeAttendanceList { get; set; }
        public IList<RepeatWeeklyDay> HolidayWeeklyDetailsList { get; set; }

        public IList<SalaryHeadModel> SalaryHeadList { get; set; }



        public IList<EmployeePayrollModel> EmployeePayrollList { get; set; }
        public EmployeePensionModel EmployeePensionModel { get; set; }


    }

    public class Roles
    {
        public string RoleName { get; set; }
        public string Id { get; set; }
        public IList<PermissionsModel> PermissionsList { get; set; }
    }
}
