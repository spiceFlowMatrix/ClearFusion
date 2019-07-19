using DataAccess.DbEntities;
using DataAccess.DbEntities.Marketing;
using DataAccess.DbEntities.Project;
using DataAccess.DbEntities.Store;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Project;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using HumanitarianAssistance.ViewModels.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.DbEntities.AccountingNew;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using HumanitarianAssistance.Entities.Models;
using HumanitarianAssistance.ViewModels.SPModels;
using HumanitarianAssistance.ViewModels.Models.Common;

namespace HumanitarianAssistance.Service.APIResponses
{
    public class APIResponse
    {
        public APIResponse()
        {
            data = new data();
            ItemAmount = new ItemAmount();
            CommonId = new CommonId();
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public data data { get; set; }
        public Object ResponseData { get; set; }
        public LoggerDetailsModel LoggerDetailsModel { get; set; }
        public ItemAmount ItemAmount { get; set; }
        public CommonId CommonId { get; set; }

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

    public class Parent
    {
        public dynamic LoggerDetailsModel { get; set; }
    }

    public class data
    {
        public List<string> RepeatDays { get; set; }
        public byte[] pdf { get; set; }
        public string AspNetUserId { get; set; }
        public RoleViewModel RoleData { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
        public string RoleName { get; set; }
        public double? CurrenctExchangeRate { get; set; }
        public double TotalExpenditure { get; set; }

        public double TotalRecivable { get; set; }
        public double TotalPayable { get; set; }
        public double Balance { get; set; }
        public long TotalExchangeRateCount { get; set; }

        public double TotalIncome { get; set; }
        public int TotalPresentDays { get; set; }
        public int TotalAbsentDays { get; set; }

        public bool AttendanceStatus { get; set; }

        public int? OfficeId { get; set; }
        public string VoucherReferenceNo { get; set; }
        public long VoucherNo { get; set; }

        public int? TotalEmployees { get; set; }
        public double? TotalGrossSalary { get; set; }
        public double? TotalDeductions { get; set; }
        public double? TotalAllowances { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryItemCode { get; set; }
        public string StoreSourceCode { get; set; }
        public bool isSalaryHeadSaved { get; set; }
        public bool isPayrollHeadSaved { get; set; }
        public string ItemGroupCode { get; set; }
        public string JobCode { get; set; }
        public long? PensionDebitAccountId { get; set; }
        public VoucherTransactionModel VoucherTransactionModel { get; set; }
        public List<VoucherTransactionModel> VoucherTransactionModelList { get; set; }
        public List<EmployeeSalaryAnalyticalInfoModel> EmployeeSalaryAnalyticalInfoList { get; set; }
        public List<ItemSpecificationMasterModel> ItemSpecificationMasterList { get; set; }
        public List<ItemSpecificationDetailModel> ItemSpecificationDetailList { get; set; }
        public UpdatePurchaseInvoiceModel UpdatePurchaseInvoiceModel { get; set; }
        public List<EmployeeEducationsModel> EmployeeEducationsList { get; set; }
        public List<EmployeeSalaryBudgetModel> EmployeeSalaryBudgetList { get; set; }
        public List<EmployeeOtherSkillsModel> EmployeeOtherSkillsList { get; set; }
        public List<EmployeeRelativeInfoModel> EmployeeRelativeInfoList { get; set; }
        public List<EmployeeHistoryOutsideOrganizationModel> EmployeeHistoryOutsideOrganizationList { get; set; }
        //public ExchangeGainOrLossModel ExchangeGainOrLossModel { get; set; }
        public List<TransactionsModel> ExchangeGainOrLossModel { get; set; }
        public List<SalaryTaxReportModel> SalaryTaxReportModelList { get; set; }
        public List<ProcurmentSummaryModel> ProcurmentSummaryModelList { get; set; }
        public List<ItemOrderModel> ItemOrderModelList { get; set; }
        //public List<IGrouping<int, CategoryPopulator>> CategoryPopulatorLst { get; set; }
        public List<CategoryPopulator> CategoryPopulatorLst { get; set; }
        public ICollection<VoucherDetail> VouchersList { get; set; }
        public List<int> UserOfficeList { get; set; }
        public List<ExitInterviewModel> ExitInterviewList { get; set; }
        public List<InterviewDetailModel> InterviewDetailList { get; set; }
        public List<LoggerModel> LoggerDetailsModelList { get; set; }
        public List<AdvanceModel> AdvanceList { get; set; }
        public List<InterviewTechnicalQuestions> InterviewTechnicalQuestionsList { get; set; }
        public List<EmployeeDetailList> EmployeeDetailListData { get; set; }
        public List<EmployeeAppraisalDetailsModel> EmployeeAppraisalDetailsModelLst { get; set; }
        public EmployeeAppraisalDetailsModel EmployeeAppraisalDetailsModel { get; set; }
        public List<IGrouping<int, EmployeeAppraisalDetailsModel>> EmployeeEvaluationDetailsModelLst { get; set; }
        public List<AppraisalGeneralQuestions> AppraisalList { get; set; }
        public EmployeeTaxReport EmployeeTaxReport { get; set; }
        public List<EmployeeSalarySlipModel> EmployeeSalarySlipModelList { get; set; }
        public List<EmployeeContractModel> EmployeeContractModellst { get; set; }
        public List<EmployeeContractModel> EmployeeContractDetails { get; set; }
        public ContractTypeContent ContractTypeContentList { get; set; }
        public List<EmployeeContractType> EmployeeContractTypeList { get; set; }
        public List<EmployeePensionRateModel> EmployeePensionRateList { get; set; }
        //public List<BudgetLineEmployees> GetAllEmployeesInBudgetLine { get; set; }
        public List<EmployeeProjectModel> EmployeeProjectList { get; set; }
        public List<EmployeeSummaryDetails> EmployeeSummaryDetailsList { get; set; }
        public ExchangeRateDetail ExchangeRateLists { get; set; }
        public List<ExchangeRate> ExchangeRates { get; set; }
        public AccountOpendingAndClosingBL AccountOpendingAndClosingBL { get; set; }
        //List Response result
        public List<Roles> RoleList { get; set; }
        //Get All Office
        public IList<OfficeDetailModel> OfficeDetailsList { get; set; }
        public IList<DepartmentModel> Departments { get; set; }
        public IList<UserDetailsModel> UserDetailsList { get; set; }
        public ICollection<UserDetails> UserDetailList { get; set; }
        public UserDetailsModel UserDetails { get; set; }
        public IList<PermissionsInRolesModel> PermissionsInRolesList { get; set; }
        public IList<PermissionsModel> PermissionsList { get; set; }
        public IList<CurrencyModel> CurrencyList { get; set; }
        public IList<JournalDetailModel> JournalDetailList { get; set; }
        public IList<EmailSettingModel> EmailSettingList { get; set; }
        public IList<EmailTypeModel> EmailTypeList { get; set; }
        public IList<ChartAccountDetailModel> ChartAccountList { get; set; }
        public IList<AccountLevelModel> AccountLevelList { get; set; }
        public List<AccountType> AccountTypeList { get; set; }
        public IList<VoucherDetailModel> VoucherDetailList { get; set; }
        public VoucherDetailModel VoucherDetail { get; set; }
        public IList<VoucherTypeModel> VoucherTypeList { get; set; }
        public IList<VoucherDocumentDetailModel> VoucherDocumentDetailList { get; set; }

        public IList<JournalVoucherViewModel> JournalVoucherViewList { get; set; }
        public IList<JournalReportViewModel> JournalReportList { get; set; }
        public IList<AccountDetailModel> AccountDetailList { get; set; }
        public IList<VoucherTransactionModel> VoucherTransactionList { get; set; }
        public IList<LedgerModel> LedgerList { get; set; }
        public IList<LedgerReportViewModel> ledgerReportFinal { get; set; }
        public IList<LedgerModel> TrialBalanceList { get; set; }

        public IList<ExchangeRateModel> ExchangeRateList { get; set; }
        public IList<ExchangeRateDetail> ExchangeRateDetailList { get; set; }
        public IList<EmployeeDetailModel> EmployeeDetailList { get; set; }
        //public IList<ProjectBudget> ProjectBudgetList { get; set; }
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

        public IList<ProjectDetail> ProjectDetailList { get; set; }

        public IList<InterviewFeedbackDetailsModel> InterviewFeedbackDetailsList { get; set; }
        public IList<EmployeeSalaryDetailsModel> EmployeeSalaryDetailsList { get; set; }
        public IList<LeaveReasonDetailModel> LeaveReasonList { get; set; }
        public IList<FinancialYearDetailModel> FinancialYearDetailList { get; set; }
        public IList<TaskMasterModel> TaskMasterList { get; set; }
        public IList<ActivityMasterModel> ActivityMasterList { get; set; }
        public IList<ProjectBudgetLineDetailModel> ProjectBudgetLineList { get; set; }
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
        public EmployeeHealthInformationModel EmployeeHealthInfo { get; set; }
        public List<EmployeeHealthQuestion> EmployeeHealthQuestionList { get; set; }
        public List<EmployeeLanguages> EmployeeLanguagesList { get; set; }


        public ProjectBudgetLinesModel ProjectBudgetLinesModel { get; set; }
        public IList<ProjectDocumentModel> ProjectDocumentList { get; set; }
        public IList<EmployeeApplyLeaveModel> EmployeeApplyLeaveList { get; set; }
        public IList<NotesMasterModel> NotesDetailsList { get; set; }
        public BalanceSheet BalanceSheet { get; set; }
        public IList<CurrentFinancialYear> CurrentFinancialYearList { get; set; }

        public List<DetailsOfNotesFinalModel> DetailsOfNotesFinalList { get; set; }

        public IList<ScheduleCandidateModel> ScheduledProspectiveEmployee { get; set; }
        public IList<JobGradeModel> JobGradeList { get; set; }

        public IList<InterviewScheduleModel> InterviewApprovedEmployeeList { get; set; }
        //public IList<InterviewScheduleModel> InterviewScheduleList { get; set; }

        public IList<InterviewScheduleModel> InterviewScheduleGeneralAssemblylist { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleDirectorlist { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleGeneralAdminlist { get; set; }
        public IList<InterviewScheduleModel> InterviewScheduleFieldOfficelist { get; set; }
        public IList<RolePermissionViewModel> PermissionsInRole { get; set; }
        public IList<AgreeDisagreePermissionModel> AgreeDisagreePermissionsInRole { get; set; }
        public IList<OrderSchedulePermissionModel> OrderSchedulePermissionsInRole { get; set; }
        public IList<ApproveRejectPermissionModel> ApproveRejectPermissionsInRole { get; set; }
        public IList<EmployeeMonthlyPayrollModel> EmployeeMonthlyPayrolllist { get; set; }

        public IList<HolidayDetails> HolidayDetailsList { get; set; }

        public IList<ApplyLeaveModel> ApplyLeaveList { get; set; }
        public IList<MonthlyEmployeeAttendanceModel> MonthlyEmployeeAttendanceList { get; set; }
        public IList<RepeatWeeklyDay> HolidayWeeklyDetailsList { get; set; }

        public IList<SalaryHeadModel> SalaryHeadList { get; set; }



        public IList<EmployeePayrollModel> EmployeePayrollList { get; set; }
        public IList<EmployeePayrollAccountModel> EmployeePayrollAccountHeadList { get; set; }
        public EmployeePensionModel EmployeePensionModel { get; set; }

        // Store classes
        public List<StoreInventoryModel> InventoryList { get; set; }
        public List<StoreInventoryItemModel> InventoryItemList { get; set; }
        public List<InventoryItemTypeModel> InventoryItemTypeList { get; set; }
        public List<StoreItemPurchaseViewModel> StoreItemsPurchaseViewList { get; set; }

        public List<PurchaseUnitType> PurchaseUnitTypeList { get; set; }
        public List<DepreciationReportModel> DepreciationReportList { get; set; }
        public List<StatusAtTimeOfIssue> StatusAtTimeOfIssueList { get; set; }
        public List<ReceiptType> ReceiptTypeList { get; set; }
        public List<EmployeeMonthlyPayrollModelApproved> EmployeeMonthlyPayrollApprovedList { get; set; }
        public List<StoreItemGroupModel> storeItemGroupList { get; set; }


        public int notificationIsReadCount { get; set; }
        public SalaryTaxReportContent SalaryTaxReportContentDetails { get; set; }
        public List<AdvancesHistoryModel> AdvanceHistory { get; set; }
        public List<PensionPaymentModel> PensionPayment { get; set; }
        public List<PensionPaymentHistoryModel> PensionPaymentHistory { get; set; }
        public List<PayrollHeadModel> PayrollHeadModelList { get; set; }
        public ICollection<PayrollAccountHead> PayrollAccountHead { get; set; }
        public ICollection<DonorDetail> DonorDetail { get; set; }
        public ICollection<ClientDetails> ClientDetails { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<SectorDetails> sectorDetails { get; set; }
        public ICollection<ProgramDetail> programDetails { get; set; }
        public ProgramDetail ProgramDetail { get; set; }
        public ICollection<AreaDetail> AreaDetail { get; set; }
        //public ICollection<DistrictDetail> DistrictDetail { get; }
        public int TotalCount { get; set; }
        public List<DistrictDetail> Districtlist { get; set; }
        public List<ProvinceDetails> Provincelist { get; set; }
        public List<CountryDetails> Countrylist { get; set; }
        public ICollection<StrengthConsiderationDetail> StrengthConsiderationDetail { get; set; }
        public ICollection<GenderConsiderationDetail> GenderConsiderationDetail { get; set; }
        public ICollection<SecurityDetail> SecurityDetail { get; set; }
        public ICollection<SecurityConsiderationDetail> SecurityConsiderationDetail { get; set; }

        public ICollection<ProjectDetailNewModel> ProjectDetailModel { get; set; }
        public ProjectDetailNewModel ProjectDetailModel1 { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
        public bool ProjectWinLoss { get; set; }
        public DonorDetail DonorDetailById { get; set; }
        public ProjectOtherDetail OtherProjectDetailById { get; set; }
        public SecurityConsiderationMultiSelect MultiSecurityConsiderationById { get; set; }
        public List<long> SecurityConsiderationMultiSelectById { get; set; }
        public List<int> ProvinceMultiSelectById { get; set; }
        public List<long> DistrictMultiSelectById { get; set; }
        public EditIndicatorModel IndicatorModel { get; set; }


        public List<ProjectCommunicationModel> ProjectCommunicationModel { get; set; }
        public ProjectProgram projectProgram { get; set; }
        public ProjectArea projectArea { get; set; }
        public ProjectSector projectSector { get; set; }
        public ProjectSectorModel ProjectSectorModel { get; set; }
        public SectorDetails SectorDetails { get; set; }


        public ProjectProposalModel ProjectProposalModel { get; set; }
        public ProjectProposalDetail ProjectProposalDetail { get; set; }
        public ApproveProjectDetails ApproveProjectDetails { get; set; }
        public WinProjectDetails WinProjectDetails { get; set; }

        public EligibilityCriteriaDetail eligibilityCriteriaDetail { get; set; }
        public CriteriaEveluationModel CriteriaEveluationModel { get; set; }
        public List<ApplicationPages> ApplicationPagesList { get; set; }
        public List<UserRolePermissionsModel> UserRolePermissions { get; set; }
        public List<RolePermissionModel> RolePermissionModelList { get; set; }


        public ICollection<PriorityOtherDetail> PriorityOtherDetail { get; set; }
        public ICollection<CEFeasibilityExpertOtherDetail> FeasibilityExpertOtherDetail { get; set; }
        public ICollection<CEAgeGroupDetail> CEAgeGroupDetail { get; set; }
        public ICollection<CEOccupationDetail> CEOccupationDetail { get; set; }
        public ICollection<CEAssumptionDetail> CEAssumptionDetail { get; set; }

        public ICollection<DonorEligibilityCriteria> DonorEligibilityCriteria { get; set; }
        public IList<GainLossSelectedAccounts> GainLossSelectedAccounts { get; set; }

        public ICollection<ProjectJobDetail> ProjectJobDetail { get; set; }
        public ProjectJobDetailModel ProjectJobModel { get; set; }
        public List<ProjectJobDetailModel> ProjectJobDetailModel { get; set; }
        public IList<ProjectBudgetLineDetailModel> ProjectBudgetLineDetailList { get; set; }
        public IList<ProjectBudgetLineDetailModel> ProjectBudgetLineDetailByBudgetId { get; set; }
        public IList<TransactionBudgetModel> TransactionBudgetModelList { get; set; }
        public ProjectActivityDetail ProjectActivityLst { get; set; }

        public ICollection<ProjectActivityModel> ProjectActivityList { get; set; }
        public ProjectActivityModel ProjectActivityDetails { get; set; }
        public ICollection<ProjectSubActivityListModel> ProjectSubActivityListModel { get; set; }
        public  ProjectSubActivityListModel ProjectSubActivityList { get; set; }
        public ProjectActivityStatusModel ProjectActivityStatusModel { get; set; }
        public List<ExchangeRateVerificationViewModel> ExchangeRateVerificationList { get; set; }
        public List<ExchangeRateDetailViewModel> ExchangeRateDetailViewModelList { get; set; }
        public ActivityDocumentsDetailModel activityDocumnentDetail { get; set; }
        public List<ActivityDocumentDetailModel> ActivityDocumentDetailModel { get; set; }
        public List<AttendanceGroupMasterModel> AttendanceGroupMasterList { get; set; }

        public ProjectCashFlowModel ProjectCashFlowModel { get; set; }
        public List<BudgetLineBreakdownListModel> BudgetLineBreakdownList { get; set; }
        public BudgetLineBreakdownModel BudgetLineBreakdownModel { get; set; }
        public ProjectIndicatorViewModel ProjectIndicator { get; set; }
        public List<IndicatorQuestions> Questions { get; set; }
        public IList<ProjectHiringRequestModel> ProjectHiringRequestModel { get; set; }
        
        public IList<ProjectHiringCandidateDetailModel> ProjectHiringCandidateDetailModel { get; set; }



        #region Marketing    
        public PolicyTimeSchedule policyTimeScheduleDetails { get; set; }
        public PolicyDaySchedule policyDayScheduleDetails { get; set; }
        public List<PolicyTimeScheduleModel> policySchedulesByTimeList { get; set; }
        public List<PolicyScheduleModel> policySchedulesByDateList { get; set; }
        public ICollection<JobDetails> JobDetails { get; set; }
        public JobDetails JobDetailModel { get; set; }
        public ICollection<Quality> Qualities { get; set; }
        public ICollection<CurrencyDetails> Currencies { get; set; }
        public ICollection<ContractDetails> ContractDetails { get; set; }
        public ContractDetails contractDetails { get; set; }
        public ContractDetailsModel contractDetailsModel { get; set; }
        public ICollection<LanguageDetail> Languages { get; set; }
        public ICollection<Medium> Mediums { get; set; }
        public ICollection<Nature> Natures { get; set; }
        public ICollection<JobPhase> JobPhases { get; set; }
        public ICollection<Producer> Producers { get; set; }
        public ICollection<Channel> Channels { get; set; }
        public ICollection<ChannelModel> ChannelList { get; set; }
        public ICollection<UnitRate> UnitRates { get; set; }
        public UnitRate UnitRateByActivityId { get; set; }
        public ICollection<JobPriceDetails> JobPriceDetails { get; set; }
        public ICollection<ActivityType> ActivityTypes { get; set; }
        public ICollection<MediaCategory> MediaCategories { get; set; }
        public ICollection<TimeCategory> TimeCategories { get; set; }
        public List<JobDetailsModel> JobDetailsModel { get; set; }
        public List<PolicyModel> policyList { get; set; }
        public List<ScheduleTimeModel> scheduleTimeList { get; set; }
        public ScheduleDetails scheduleDetails { get; set; }
        public PlayoutMinutes playoutMinutesDetails { get; set; }
        public SchedulerModel scheduleDetailsModel { get; set; }
        public List<ScheduleDetails> scheduleDetailsLists { get; set; }
        public List<ScheduleDetailModel> scheduleDetailsList { get; set; }
        public int jobListTotalCount { get; set; }
        public List<UnitRateDetailsModel> UnitRateDetails { get; set; }
        public List<ContractByClient> ContractByClientList { get; set; }
        public UnitRateModel unitRateDetails { get; set; }
        public UnitRate unitRateDetailsById { get; set; }
        public IQueryable<UnitRateDetailsModel> rateDetails { get; set; }
        public UnitRateDetailsModel rateDetailsById { get; set; }
        public IQueryable<ClientDetailModel> clientDetailsModel { get; set; }
        public ClientDetails clientDetails { get; set; }
        public ClientDetailModel clientDetailsById { get; set; }
        public JobDetailsModel JobDetail { get; set; }
        public JobPriceModel JobPriceDetail { get; set; }
        public List<JobDetailsModel> JobPriceDetailList { get; set; }
        public List<PolicyModel> PolicyFilteredList { get; set; }
        public MediaCategory mediaCategoryById { get; set; }
        public Medium mediumById { get; set; }
        public Nature natureById { get; set; }
        public Quality qualityById { get; set; }
        public JobPhase phaseById { get; set; }
        public Producer producerById { get; set; }
        public Channel channelById { get; set; }
        public ActivityType activityById { get; set; }
        public PolicyDetail policyDetails { get; set; }
        public ScheduleDetails schedulerDetails { get; set; }
        public List<PolicyModel> policyFilterList { get; set; }
        public PolicyModel policyDetailsById { get; set; }
        public PolicyTimeScheduleModel policyTimeDetailsById { get; set; }
        public TimeCategory timeCatergoryById { get; set; }
        #endregion

        public List<LanguageDetail> LanguageDetail { get; set; }
        public List<CodeType> SourceCodeTypelist { get; set; }
        public List<StoreSourceCodeDetailModel> SourceCodeDatalist { get; set; }
        public ICollection<PaymentTypes> PaymentTypesList { get; set; }
        public Dictionary<string, List<string>> Permissions { get; set; }
        public ProjectMonitoringViewModel ProjectMonitoringModel { get; set; }
        public List<SPVoucherSummaryReportModel> VoucherSummaryList { get; set; }
        public List<VoucherSummaryTransactionModel> VoucherSummaryTransactionList{ get; set; }

        #region "Accounting New"
        public List<ChartOfAccountNew> AllAccountList { get; set; }
        public List<ChartOfAccountNew> MainLevelAccountList { get; set; }
        public List<ChartOfAccountNew> ControlLevelAccountList { get; set; }
        public List<ChartOfAccountNew> SubLevelAccountList { get; set; }
        public List<ChartOfAccountNew> InputLevelAccountList { get; set; }
        public List<AccountFilterType> AllAccountFilterList { get; set; }

        public ChartOfAccountNew ChartOfAccountNewDetail { get; set; }
        public List<VoucherTransactionsModel> VoucherTransactions { get; set; }
        public List<AccountBalance> AccountBalanceList { get; internal set; }
        public InvoiceModel invoiceDetails { get; set; }
        public List<SchedulerModel> SchedulerList { get; internal set; }

        public List<AccountBalance> AccountBalances { get; set; }
        public List<NoteAccountBalances> NoteAccountBalances { get; set; }
        public List<ExchangeGainLossReportViewModel> ExchangeGainLossReportList { get; set; }
        public bool IsVoucherVerified { get; set; }
        public bool IsExchangeRateVerified { get; set; }

        public VoucherDetailEntityModel VoucherDetailEntity { get; set; }
        public List<GainLossVoucherList> GainLossVoucherList { get; set; }
        public GainLossVoucherList GainLossVoucherDetail { get; set; }
        public List<ProjectMonitoringViewModel> ProjectMonitoring { get; set; }
        public ProjectActivityDetail ProjectActivityDetail { get; set; }
        public ProjectActivityModel ProjectActivityModel { get; set; }

        #endregion


        #region "Web link URl"
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

        public DateTime DateTime { get; set; }
        public bool ImplementationStatus { get; set; }
        public bool MonitoringStatus { get; set; }
        public string SignedUrl { get; set; }
        public string TrainingLink { get; set; }
        public List<SPProjectProposalReportModel> ProjectProposalReportList { get; set; }
        public List<ProjectProposalAmountSummary> ProjectProposalAmountSummary { get; set; }
        public ProjectIndicatorModel ProjectIndicatorList { get; set; }

        public ICollection<ProjectActivityExtensions> ProjectActivityExtensionsList { get; set; }

        public List<OpportunityControlViewModel> OpportunityControlList { get; set; }
        public List<LogisticsControlViewModel> LogisticsControlList { get; set; }
        public List<ActivitiesControlViewModel> ActivitiesControlList { get; set; }
        public List<HiringControlViewModel> HiringControlList { get; set; }
        public List<ProjectActivityPermissionModel> ProjectActivityPermissionList { get; set; }
        public List<FileListModel> DocumentFileList { get; set; }
    }


    public class NoteAccountBalances
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public int NoteHeadId { get; set; }
        public string NoteHeadName { get; set; }
        public List<AccountBalance> AccountBalances { get; set; }
    }

    public class AccountBalance
    {
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCode { get; set; }
    }

    public class Roles
    {
        public string RoleName { get; set; }
        public string Id { get; set; }
        public IList<PermissionsModel> PermissionsList { get; set; }
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
