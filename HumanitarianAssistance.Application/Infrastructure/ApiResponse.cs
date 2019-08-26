using System;
using System.Collections.Generic;
using System.Text;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Domain.Entities.Store;

namespace HumanitarianAssistance.Application.Infrastructure
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            data = new Data();
            ItemAmount = new ItemAmount();
            CommonId = new CommonId();
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Data data { get; set; }
        public dynamic ResponseData { get; set; }
        public LoggerDetailsModel LoggerDetailsModel { get; set; }
        public ItemAmount ItemAmount { get; set; }
        public CommonId CommonId { get; set; }

    }
    public class Data
    {

        // login
        public string AspNetUserId { get; set; }
        public string Token { get; set; }
        public string JobCode { get; set; }
        public IList<string> Roles { get; set; }
        public List<RolePermissionModel> RolePermissionModelList { get; set; }
        public IList<ApproveRejectPermissionModel> ApproveRejectPermissionsInRole { get; set; }
        public IList<AgreeDisagreePermissionModel> AgreeDisagreePermissionsInRole { get; set; }
        public IList<OrderSchedulePermissionModel> OrderSchedulePermissionsInRole { get; set; }
        public List<int> UserOfficeList { get; set; }

        public ChartOfAccountNew ChartOfAccountNewDetail { get; set; }
        public VoucherDetailEntityModel VoucherDetailEntity { get; set; }
        public bool IsVoucherVerified { get; set; }
        public List<ChartOfAccountNew> AllAccountList { get; set; }
        public List<VoucherTransactionsModel> VoucherTransactions { get; set; }
        public IList<VoucherDetailModel> VoucherDetailList { get; set; }
        public int TotalCount { get; set; }
        public bool IsExchangeRateVerified { get; set; }
        public List<ExchangeRateDetailViewModel> ExchangeRateDetailViewModelList { get; set; }
        public List<ChartOfAccountNew> MainLevelAccountList { get; set; }
        public dynamic ExchangeRateVerificationList { get; set; }
        public VoucherDetailModel VoucherDetail { get; set; }
        public long TotalExchangeRateCount { get; set; }
        public ClientDetailModel clientDetailsById { get; set; }
        public int jobListTotalCount { get; set; }
        public ClientDetails clientDetails { get; set; }
        public ICollection<ClientDetails> ClientDetails { get; set; }
        public ICollection<Category> Categories { get; set; }
        // public GainLossVoucherList GainLossVoucherDetail { get; set; }
        public dynamic GainLossVoucherList { get; set; }
        public dynamic AccountBalances { get; set; }
        public dynamic GainLossVoucherDetail { get; set; }
        // public List<NoteAccountBalances> NoteAccountBalances { get; set; }
        public dynamic NoteAccountBalances { get; set; }
        public dynamic GainLossSelectedAccounts { get; set; }
        public dynamic TrialBalanceList { get; set; }
        public dynamic VoucherSummaryTransactionList { get; set; }
        public List<Roles> RoleList { get; set; }
        public IList<UserDetailsModel> UserDetailsList { get; set; }
        public ICollection<UserDetails> UserDetailList { get; set; }
        public UserDetailsModel UserDetails { get; set; }
        public IList<PermissionsInRolesModel> PermissionsInRolesList { get; set; }
        public IList<PermissionsModel> PermissionsList { get; set; }
        public IList<VoucherTypeModel> VoucherTypeList { get; set; }
        public IList<AccountDetailModel> AccountDetailList { get; set; }
        public List<LoggerModel> LoggerDetailsModelList { get; set; }
        public string VoucherReferenceNo { get; set; }
        public long VoucherNo { get; set; }
        public List<AccountFilterType> AllAccountFilterList { get; set; }
        public List<ApplicationPages> ApplicationPagesList { get; set; }
        public IList<RolePermissionViewModel> PermissionsInRole { get; set; }

        //Code
        public IList<OfficeDetailModel> OfficeDetailsList { get; set; }
        public IList<CurrencyModel> CurrencyList { get; set; }
        public IList<JournalDetailModel> JournalDetailList { get; set; }
        public IList<EmailSettingModel> EmailSettingList { get; set; }
        public IList<EmailTypeModel> EmailTypeList { get; set; }
        public List<AccountType> AccountTypeList { get; set; }
        public IList<DesignationModel> DesignationList { get; set; }
        public EmployeeAppraisalDetailsModel EmployeeAppraisalDetailsModel { get; set; }
        public List<AppraisalGeneralQuestions> AppraisalList { get; set; }
        public List<LanguageDetail> LanguageDetail { get; set; }




        // Marketing
        public byte[] pdf { get; set; }
        public dynamic contractDetailsModel { get; set; }
        public ICollection<JobPriceDetails> JobPriceDetails { get; set; }
        public JobPriceModel JobPriceDetail { get; set; }
        public ContractDetails contractDetails { get; set; }
        public ICollection<ContractDetails> ContractDetails { get; set; }
        public ActivityType activityById { get; set; }
        public ICollection<ActivityType> ActivityTypes { get; set; }
        public ICollection<LanguageDetail> Languages { get; set; }
        public ICollection<MediaCategory> MediaCategories { get; set; }
        public JobPhase phaseById { get; set; }
        public Channel channelById { get; set; }
        public ICollection<Channel> Channels { get; set; }
        public JobDetails JobDetailModel { get; set; }
        public ICollection<JobDetails> JobDetails { get; set; }
        public JobDetailsModel JobDetail { get; set; }
        public InvoiceModel invoiceDetails { get; set; }
        public List<JobDetailsModel> JobPriceDetailList { get; set; }
        public List<JobDetailsModel> JobDetailsModel { get; set; }
        public ICollection<Medium> Mediums { get; set; }
        public ICollection<JobPhase> JobPhases { get; set; }
        public UnitRate unitRateDetailsById { get; set; }
        public dynamic unitRateDetails { get; set; }
        public ICollection<Nature> Natures { get; set; }
        public ICollection<Quality> Qualities { get; set; }
        public List<UnitRateDetailsModel> UnitRateDetails { get; set; }
        public ICollection<TimeCategory> TimeCategories { get; set; }
        public List<ContractByClient> ContractByClientList { get; set; }
        public ICollection<CurrencyDetails> Currencies { get; set; }
        public ICollection<Producer> Producers { get; set; }
        public MediaCategory mediaCategoryById { get; set; }
        public Medium mediumById { get; set; }
        public Nature natureById { get; set; }
        public Quality qualityById { get; set; }
        public TimeCategory timeCatergoryById { get; set; }
        public UnitRate UnitRateByActivityId { get; set; }
        public UnitRateDetailsModel rateDetailsById { get; set; }
        public ICollection<UnitRate> UnitRates { get; set; }
        public PolicyDetail policyDetails { get; set; }
        public List<PolicyModel> policyList { get; set; }
        public List<PolicyModel> policyFilterList { get; set; }
        public PolicyModel policyDetailsById { get; set; }
        public List<PolicyScheduleModel> policySchedulesByDateList { get; set; }
        public PolicyTimeSchedule policyTimeScheduleDetails { get; set; }
        public List<PolicyTimeScheduleModel> policySchedulesByTimeList { get; set; }
        public PolicyTimeScheduleModel policyTimeDetailsById { get; set; }
        public PolicyDaySchedule policyDayScheduleDetails { get; set; }
        public Producer producerById { get; set; }
        public List<PolicyModel> PolicyFilteredList { get; set; }
        public ICollection<ProjectDetailModel> ProjectDetailModel { get; set; }
        public List<ScheduleDetailModel> scheduleDetailsList { get; set; }
        public List<string> RepeatDays { get; set; }
        public SchedulerModel scheduleDetailsModel { get; set; }
        public ICollection<ChannelModel> ChannelList { get; set; }
        public ScheduleDetails scheduleDetails { get; set; }
        public List<SchedulerModel> SchedulerList { get; internal set; }
        public ScheduleDetails schedulerDetails { get; set; }
        public PlayoutMinutes playoutMinutesDetails { get; set; }


        //Project
        public ICollection<DonorDetail> DonorDetail { get; set; }
        public DonorDetail DonorDetailById { get; set; }
        public ICollection<SectorDetails> sectorDetails { get; set; }
        public ICollection<ProgramDetail> programDetails { get; set; }
        public ProgramDetail ProgramDetail { get; set; }
        public ICollection<AreaDetail> AreaDetail { get; set; }
        public ICollection<GenderConsiderationDetail> GenderConsiderationDetail { get; set; }
        public ICollection<StrengthConsiderationDetail> StrengthConsiderationDetail { get; set; }
        public ICollection<SecurityDetail> SecurityDetail { get; set; }
        public ICollection<SecurityConsiderationDetail> SecurityConsiderationDetail { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
        //public ICollection<ProjectDetailModel> ProjectDetailModel { get; set; }
        public ProjectDetailModel ProjectDetailModel1 { get; set; }
        public ProjectOtherDetail OtherProjectDetailById { get; set; }
        public ProjectProgram projectProgram { get; set; }
        public ProjectArea projectArea { get; set; }
        public ProjectSector projectSector { get; set; }
        public bool ProjectWinLoss { get; set; }
        public IList<ProvinceDetailModel> ProvinceDetailsList { get; set; }
        public List<int?> CountryMultiSelectById { get; set; }
        public List<int> ProvinceMultiSelectById { get; set; }
        public List<long> DistrictMultiSelectById { get; set; }
        public List<DistrictDetail> Districtlist { get; set; }
        public List<long> SecurityConsiderationMultiSelectById { get; set; }






        //Project
        public IList<ProjectHiringCandidateDetailModel> ProjectHiringCandidateDetailModel { get; set; }
        public List<EmployeeDetailListModel> EmployeeDetailListData { get; set; }
        public IList<ProjectHiringRequestModel> ProjectHiringRequestModel { get; set; }
        public List<OpportunityControlViewModel> OpportunityControlList { get; set; }
        public List<LogisticsControlViewModel> LogisticsControlList { get; set; }
        public List<ActivitiesControlViewModel> ActivitiesControlList { get; set; }
        public List<ProjectActivityPermissionModel> ProjectActivityPermissionList { get; set; }
        public List<HiringControlViewModel> HiringControlList { get; set; }
        public SectorDetails SectorDetails { get; set; }
        public ICollection<ProjectJobDetail> ProjectJobDetail { get; set; }
        public IList<ProjectBudgetLineDetailsModel> ProjectBudgetLineDetailList { get; set; }
        public ICollection<ProjectSubActivityListModel> ProjectSubActivityListModel { get; set; }
        public ProjectActivityModel ProjectActivityModel { get; set; }
        public ProjectActivityDetail ProjectActivityDetail { get; set; }
        public ProjectActivityModel ProjectActivityDetails { get; set; }
        public ICollection<ProjectActivityExtensions> ProjectActivityExtensionsList { get; set; }
        public ICollection<ProjectActivityModel> ProjectActivityList { get; set; }
        public List<ProjectMonitoringViewModel> ProjectMonitoring { get; set; }
        public ProjectMonitoringViewModel ProjectMonitoringModel { get; set; }
        public EligibilityCriteriaDetail eligibilityCriteriaDetail { get; set; }
        public List<ProjectProposalAmountSummary> ProjectProposalAmountSummary { get; set; }
        public ProjectIndicatorModel ProjectIndicatorList { get; set; }
        public EditIndicatorModel IndicatorModel { get; set; }
        public ProjectIndicatorViewModel ProjectIndicator { get; set; }
        public List<IndicatorQuestions> Questions { get; set; }
        public List<SPProjectProposalReportModel> ProjectProposalReportList { get; set; }


        //Web Link Url 05082019
        public string SignedUrl { get; set; }
        public WinProjectDetails WinProjectDetails { get; set; }
        public ApproveProjectDetails ApproveProjectDetails { get; set; }
        public ProjectProposalDetail ProjectProposalDetail { get; set; }
        public ProjectCashFlowModel ProjectCashFlowModel { get; set; }
        public BudgetLineBreakdownModel BudgetLineBreakdownModel { get; set; }
        public ActivityDocumentDetailModel activityDocumnentDetail { get; set; }
        public List<ActivityDocumentDetailModel> ActivityDocumentDetailModel { get; set; }
        public ProjectActivityStatusModel ProjectActivityStatusModel { get; set; }
        public IList<ProjectBudgetLineDetailsModel> ProjectBudgetLineDetailByBudgetId { get; set; }
        public IList<ProjectBudgetLineDetailsModel> ProjectBudgetLineList { get; set; }
        public IList<TransactionBudgetModel> TransactionBudgetModelList { get; set; }
        public List<ProjectJobDetailModel> ProjectJobDetailModel { get; set; }
        public ProjectJobDetailModel ProjectJobModel { get; set; }
        public ICollection<PriorityOtherDetail> PriorityOtherDetail { get; set; }
        public ICollection<CEFeasibilityExpertOtherDetail> FeasibilityExpertOtherDetail { get; set; }
        public ICollection<CEAgeGroupDetail> CEAgeGroupDetail { get; set; }
        public ICollection<CEAssumptionDetail> CEAssumptionDetail { get; set; }
        public ICollection<DonorEligibilityCriteria> DonorEligibilityCriteria { get; set; }
        public bool IsApprovedCriteriaEvaluation { get; set; }
        public CriteriaEveluationModel CriteriaEveluationModel { get; set; }

        public ProjectProposalModel ProjectProposalModel { get; set; }

        #region "proposal web link" 
        public string ProposalWebLink { get; set; }
        public string EDIWebLink { get; set; }
        public string BudgetWebLink { get; set; }
        public string ConceptWebLink { get; set; }
        public string PresentationWebLink { get; set; }

        public string ProposalWebLinkExtType { get; set; }
        public string EDIWebLinkExtType { get; set; }
        public string BudgetWebLinkExtType { get; set; }
        public string ConceptWebLinkExtType { get; set; }
        public string PresentationWebLinkExtType { get; set; }


        #endregion


        #region "Code"
        public IList<ProjectDetail> ProjectDetailList { get; set; }
        public IList<ProfessionModel> ProfessionList { get; set; }
        public IList<CountryDetailsModel> CountryDetailsList { get; set; }
        public IList<QualificationDetailsModel> QualificationDetailsList { get; set; }
        public IList<LeaveReasonDetailModel> LeaveReasonList { get; set; }
        public IList<FinancialYearDetailModel> FinancialYearDetailList { get; set; }
        public IList<EmployeeTypeModel> EmployeeTypeList { get; set; }
        public IList<BudgetLineTypeModel> BudgetLineTypeList { get; set; }
        public IList<CurrentFinancialYearModel> CurrentFinancialYearList { get; set; }
        public IList<DepartmentModel> Departments { get; set; }
        public IList<SalaryHeadModel> SalaryHeadList { get; set; }
        public List<EmployeePensionRateModel> EmployeePensionRateList { get; set; }
        public List<EmployeeAppraisalDetailsModel> EmployeeAppraisalDetailsModelLst { get; set; }
        public List<InterviewTechnicalQuestionsModel> InterviewTechnicalQuestionsList { get; set; }
        public List<ExitInterviewModel> ExitInterviewList { get; set; }
        public SalaryTaxReportContent SalaryTaxReportContentDetails { get; set; }
        public List<AdvancesHistoryModel> AdvanceHistory { get; set; }
        public ICollection<PayrollAccountHead> PayrollAccountHead { get; set; }
        public long? PensionDebitAccountId { get; set; }
        public List<AttendanceGroupMasterModel> AttendanceGroupMasterList { get; set; }
        #endregion



        // Store
        public string ItemGroupCode { get; set; }
        public string StoreSourceCode { get; set; }
        public List<StoreInventoryModel> InventoryList { get; set; }
        public List<StoreInventoryItemModel> InventoryItemList { get; set; }
        public List<InventoryItemTypeModel> InventoryItemTypeList { get; set; }
        public List<StoreItemPurchaseViewModel> StoreItemsPurchaseViewList { get; set; }
        public List<StoreItemGroupModel> storeItemGroupList { get; set; }
        public ICollection<PaymentTypes> PaymentTypesList { get; set; }
        public List<StoreSourceCodeDetailModel> SourceCodeDatalist { get; set; }
        public List<ItemSpecificationDetailModel> ItemSpecificationDetailList { get; set; }

        //Store
        public List<ItemOrderModel> ItemOrderModelList { get; set; }
        public List<PurchaseUnitType> PurchaseUnitTypeList { get; set; }
        public List<ProcurmentSummaryModel> ProcurmentSummaryModelList { get; set; }
        public List<DepreciationReportModel> DepreciationReportList { get; set; }
        public UpdatePurchaseInvoiceModel UpdatePurchaseInvoiceModel { get; set; }
        public List<ItemSpecificationMasterModel> ItemSpecificationMasterList { get; set; }
        public List<StatusAtTimeOfIssue> StatusAtTimeOfIssueList { get; set; }
        public List<ReceiptType> ReceiptTypeList { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryItemCode { get; set; }
        public List<CodeType> SourceCodeTypelist { get; set; }

        #region "HR"
        //HR
        public List<EmployeeHistoryOutsideOrganizationModel> EmployeeHistoryOutsideOrganizationList { get; set; }
        public IList<PayrollMonthlyHourDetailModel> PayrollMonthlyHourList { get; set; }
        public IList<DisplayEmployeeAttendanceModel> DisEmployeeAttendanceList { get; set; }
        public IList<EmployeeAttendanceModel> EmployeeAttendanceList { get; set; }
        public bool AttendanceStatus { get; set; }
        public IList<MonthlyEmployeeAttendanceModel> MonthlyEmployeeAttendanceList { get; set; }
        public IList<AssignLeaveToEmployeeModel> AssignLeaveToEmployeeList { get; set; }
        public IList<EmployeeApplyLeaveModel> EmployeeApplyLeaveList { get; set; }
        public List<EmployeeMonthlyPayrollModel> EmployeeMonthlyPayrollApprovedList { get; set; }
        public IList<EmployeeMonthlyPayrollModel> EmployeeMonthlyPayrolllist { get; set; }
        public EmployeePensionModel EmployeePensionModel {get; set;}
        public List<SalaryTaxReportModel> SalaryTaxReportModelList { get; set; }
        public List<PensionPaymentModel> PensionPayment { get; set; }
        public List<PensionPaymentHistoryModel> PensionPaymentHistory { get; set; }
        public IList<EmployeePayrollModel> EmployeePayrollList { get; set; }
        public IList<EmployeePayrollAccountModel> EmployeePayrollAccountHeadList { get; set; }
        public bool isSalaryHeadSaved { get; set; }
        public bool isPayrollHeadSaved { get; set; }
        public IList<RepeatWeeklyDay> HolidayWeeklyDetailsList { get; set; }
        public List<AdvanceModel> AdvanceList { get; set; }
        public EmployeeTaxReportModel EmployeeTaxReport { get; set; }
        public List<EmployeeContractModel> EmployeeContractDetails { get; set; }
        public ContractTypeContent ContractTypeContentList { get; set; }
        public List<EmployeeContractType> EmployeeContractTypeList { get; set; }
       public List<EmployeeSummaryDetailModel> EmployeeSummaryDetailsList { get; set; }
       public IList<ApplyLeaveModel> ApplyLeaveList { get; set; }
        public int? TotalEmployees { get; set; }
        public double? TotalGrossSalary { get; set; }
        public double? TotalDeductions { get; set; }
        public double? TotalAllowances { get; set; }
        public List<EmployeeHealthQuestion> EmployeeHealthQuestionList { get; set; }

        public IList<InterviewScheduleModel> InterviewApprovedEmployeeList { get; set; }

        //HRJobs
        public IList<JobHiringDetailsModel> JobHiringDetailsList { get; set; }
        public IList<JobGradeModel> JobGradeList { get; set; }

        //Interview
        public List<InterviewDetailModel> InterviewDetailList { get; set; }

        //holidays
        public IList<HolidayDetails> HolidayDetailsList { get; set; }
        public List<EmployeeRelativeInfoModel> EmployeeRelativeInfoList { get; set; }
        public List<EmployeeOtherSkillsModel> EmployeeOtherSkillsList { get; set; }
        public List<EmployeeSalaryBudgetModel> EmployeeSalaryBudgetList { get; set; }
        public List<EmployeeEducationsModel> EmployeeEducationsList { get; set; }

        public List<EmployeeSalaryAnalyticalInfoModel> EmployeeSalaryAnalyticalInfoList { get; set; }
        public EmployeeHealthInformationModel EmployeeHealthInfo { get; set; }
        public List<EmployeeLanguages> EmployeeLanguagesList { get; set; }

        public IList<EmployeeDetailModel> EmployeeDetailList { get; set; }
        public IList<EmployeeDocumentDetailModel> EmployeeDocumentList { get; set; }
        public IList<EmployeeHistoryDetailModel> EmployeeHistoryDetailList { get; set; }
        public IList<EmployeeProfessionalDetailModel> EmployeeProfessionalList { get; set; }

        public IList<EmployeeDetailsAllModel> EmployeeDetailsList { get; set; }
        public IList<EmployeeHealthInformationModel> EmployeeHealthInfoList { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleGeneralAssemblylist { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleDirectorlist { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleGeneralAdminlist { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleFieldOfficelist { get; set; }

        public List<PayrollHeadModel> PayrollHeadModelList { get; set; }
        public List<ExchangeGainLossReportViewModel> ExchangeGainLossReportList { get; set; }

        #endregion

        //File Managment

        public List<FileListModel> DocumentFileList { get; set; }

        //Accounting
        public AccountOpendingAndClosingBL AccountOpendingAndClosingBL { get; set; }
        public IList<LedgerModel> LedgerList { get; set; }
        public IList<LedgerReportViewModel> ledgerReportFinal { get; set; }
        public IList<JournalVoucherViewModel> JournalVoucherViewList { get; set; }
        public IList<JournalReportViewModel> JournalReportList { get; set; }
        public List<DetailOfNotesSummaryModel> DetailsOfNotesFinalList { get; set; }
    }


    public class ItemAmount
    {
        public int ProcuredAmount { get; set; }
        public int SpentAmount { get; set; }
        public int CurrentAmount { get; set; }
    }

    public class LoggerDetailsModel
    {
        public int LoggerDetailsId { get; set; }
        public int NotificationId { get; set; }
        public bool IsRead { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string LoggedDetail { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string NotificationPath { get; set; }
    }

    //Get common Id for Project
    public class CommonId
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public long LongId { get; set; }
    }
    //get Approval Value
}
