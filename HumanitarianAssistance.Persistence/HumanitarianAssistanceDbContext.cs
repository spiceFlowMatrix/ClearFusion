using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.ErrorLog;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Domain.Entities.Store;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
namespace HumanitarianAssistance.Persistence
{
    public class HumanitarianAssistanceDbContext : IdentityDbContext<AppUser>
    {
        public HumanitarianAssistanceDbContext(DbContextOptions<HumanitarianAssistanceDbContext> options) : base(options)
        {

        }

        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<DesignationDetail> DesignationDetail { get; set; }
        public DbSet<DistrictDetail> DistrictDetail { get; set; }
        public DbSet<EmailType> EmailType { get; set; }
        public DbSet<EmailSettingDetail> EmailSettingDetail { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetail { get; set; }
        public DbSet<EmployeeDocumentDetail> EmployeeDocumentDetail { get; set; }
        public DbSet<EmployeePaymentTypes> EmployeePaymentTypes { get; set; }

        public DbSet<EmployeeHistoryDetail> EmployeeHistoryDetail { get; set; }

        public DbSet<EmployeeMonthlyPayroll> EmployeeMonthlyPayroll { get; set; }

        public DbSet<JournalDetail> JournalDetail { get; set; }

        public DbSet<OfficeDetail> OfficeDetail { get; set; }

        public DbSet<PayrollMonthlyHourDetail> PayrollMonthlyHourDetail { get; set; }

        public DbSet<UserDetails> UserDetails { get; set; }

        public DbSet<VoucherDetail> VoucherDetail { get; set; }

        public DbSet<Department> Department { get; set; }
        public DbSet<PermissionsInRoles> PermissionsInRoles { get; set; }
        public DbSet<CurrencyDetails> CurrencyDetails { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<VoucherType> VoucherType { get; set; }
        public DbSet<VoucherTransactions> VoucherTransactions { get; set; }
        public DbSet<CodeType> CodeType { get; set; }
        public DbSet<ProvinceDetails> ProvinceDetails { get; set; }
        public DbSet<CountryDetails> CountryDetails { get; set; }
        public DbSet<QualificationDetails> QualificationDetails { get; set; }
        public DbSet<ProfessionDetails> ProfessionDetails { get; set; }
        public DbSet<JobHiringDetails> JobHiringDetails { get; set; }
        public DbSet<InterviewScheduleDetails> InterviewScheduleDetails { get; set; }
        public DbSet<LeaveReasonDetail> LeaveReasonDetail { get; set; }
        public DbSet<AssignLeaveToEmployee> AssignLeaveToEmployee { get; set; }
        public DbSet<FinancialYearDetail> FinancialYearDetail { get; set; }
        public DbSet<BudgetLineType> BudgetLineType { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<EmployeeContractType> EmployeeContractType { get; set; }
        public DbSet<EmployeeProfessionalDetail> EmployeeProfessionalDetail { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendance { get; set; }
        public DbSet<EmployeeApplyLeave> EmployeeApplyLeave { get; set; }
        public DbSet<JobGrade> JobGrade { get; set; }
        public DbSet<HolidayDetails> HolidayDetails { get; set; }
        public DbSet<HolidayWeeklyDetails> HolidayWeeklyDetails { get; set; }
        public DbSet<SalaryHeadDetails> SalaryHeadDetails { get; set; }
        public DbSet<EmployeePayroll> EmployeePayroll { get; set; }
        public DbSet<EmployeePensionRate> EmployeePensionRate { get; set; }
        public DbSet<ContractTypeContent> ContractTypeContent { get; set; }
        public DbSet<AppraisalGeneralQuestions> AppraisalGeneralQuestions { get; set; }
        public DbSet<EmployeeAppraisalDetails> EmployeeAppraisalDetails { get; set; }
        public DbSet<EmployeeAppraisalQuestions> EmployeeAppraisalQuestions { get; set; }
        public DbSet<EmployeeEvaluation> EmployeeEvaluation { get; set; }
        public DbSet<InterviewTechnicalQuestions> InterviewTechnicalQuestions { get; set; }
        public DbSet<Advances> Advances { get; set; }

        public DbSet<InterviewDetails> InterviewDetails { get; set; }
        public DbSet<InterviewLanguages> InterviewLanguages { get; set; }
        public DbSet<InterviewTechnicalQuestion> InterviewTechnicalQuestion { get; set; }
        public DbSet<InterviewTrainings> InterviewTrainings { get; set; }
        public DbSet<TechnicalQuestion> TechnicalQuestion { get; set; }

        public DbSet<ExistInterviewDetails> ExistInterviewDetails { get; set; }

        public DbSet<UserDetailOffices> UserDetailOffices { get; set; }

        public DbSet<StrongandWeakPoints> StrongandWeakPoints { get; set; }
        public DbSet<EmployeeEvaluationTraining> EmployeeEvaluationTraining { get; set; }
        public DbSet<EmployeeAppraisalTeamMember> EmployeeAppraisalTeamMember { get; set; }
        public DbSet<RatingBasedCriteria> RatingBasedCriteria { get; set; }
        public DbSet<LoggerDetails> LoggerDetails { get; set; }

        // Store
        public DbSet<StoreInventory> StoreInventories { get; set; }
        public DbSet<StoreInventoryItem> InventoryItems { get; set; }
        public DbSet<StoreItemPurchase> StoreItemPurchases { get; set; }
        public DbSet<StorePurchaseOrder> StorePurchaseOrders { get; set; }
        public DbSet<StoreItemGroup> StoreItemGroups { get; set; }

        public DbSet<EmployeePayrollMonth> EmployeePayrollMonth { get; set; }
        public DbSet<EmployeeContract> EmployeeContract { get; set; }
        public DbSet<SalaryTaxReportContent> SalaryTaxReportContent { get; set; }
        public DbSet<ItemSpecificationMaster> ItemSpecificationMaster { get; set; }
        public DbSet<ItemSpecificationDetails> ItemSpecificationDetails { get; set; }
        public DbSet<StatusAtTimeOfIssue> StatusAtTimeOfIssue { get; set; }
        public DbSet<ReceiptType> ReceiptType { get; set; }

        public DbSet<EmployeeHistoryOutsideOrganization> EmployeeHistoryOutsideOrganization { get; set; }
        public DbSet<EmployeeHistoryOutsideCountry> EmployeeHistoryOutsideCountry { get; set; }
        public DbSet<EmployeeRelativeInfo> EmployeeRelativeInfo { get; set; }
        public DbSet<EmployeeInfoReferences> EmployeeInfoReferences { get; set; }
        public DbSet<EmployeeOtherSkills> EmployeeOtherSkills { get; set; }
        public DbSet<EmployeeSalaryBudget> EmployeeSalaryBudget { get; set; }
        public DbSet<EmployeeEducations> EmployeeEducations { get; set; }
        public DbSet<EmployeeSalaryAnalyticalInfo> EmployeeSalaryAnalyticalInfo { get; set; }
        public DbSet<EmployeeHealthInfo> EmployeeHealthInfo { get; set; }
        public DbSet<EmployeeHealthQuestion> EmployeeHealthQuestion { get; set; }
        public DbSet<EmployeeMonthlyAttendance> EmployeeMonthlyAttendance { get; set; }
        public DbSet<PensionPaymentHistory> PensionPaymentHistory { get; set; }
        public DbSet<PayrollAccountHead> PayrollAccountHead { get; set; }
        public DbSet<EmployeePayrollAccountHead> EmployeePayrollAccountHead { get; set; }
        public DbSet<EmployeeSalaryPaymentHistory> EmployeeSalaryPaymentHistory { get; set; }
        public DbSet<EmployeeLanguages> EmployeeLanguages { get; set; }
        public DbSet<AccountHeadType> AccountHeadType { get; set; }
        public DbSet<PaymentTypes> PaymentTypes { get; set; }
        public DbSet<PriorityOtherDetail> PriorityOtherDetail { get; set; }
        public DbSet<ApplicationPages> ApplicationPages { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<CEFeasibilityExpertOtherDetail> CEFeasibilityExpertOtherDetail { get; set; }
        public DbSet<CEAgeGroupDetail> CEAgeGroupDetail { get; set; }

        public DbSet<CEAssumptionDetail> CEAssumptionDetail { get; set; }
        public DbSet<DonorEligibilityCriteria> DonorEligibilityCriteria { get; set; }
        public DbSet<ApproveRejectPermission> ApproveRejectPermission { get; set; }
        public DbSet<AgreeDisagreePermission> AgreeDisagreePermission { get; set; }
        public DbSet<OrderSchedulePermission> OrderSchedulePermission { get; set; }
        public DbSet<HRJobInterviewers> HRJobInterviewers { get; set; }
        public DbSet<ExchangeRateVerification> ExchangeRateVerifications { get; set; }
        public DbSet<DocumentFileDetail> DocumentFileDetail { get; set; }
        public DbSet<AttendanceGroupMaster> AttendanceGroupMaster { get; set; }

        //created by arjun singh
        public DbSet<ChatDetail> ChatDetail { get; set; }
        public DbSet<EntitySourceDocumentDetail> EntitySourceDocumentDetails { get; set; }
        public DbSet<InventoryItemType> InventoryItemType { get; set; }


        #region Project
        public DbSet<DonorDetail> DonorDetail { get; set; }
        public DbSet<SectorDetails> SectorDetails { get; set; }
        public DbSet<ProgramDetail> ProgramDetail { get; set; }
        public DbSet<AreaDetail> AreaDetail { get; set; }
        public DbSet<StrengthConsiderationDetail> StrengthConsiderationDetail { get; set; }
        public DbSet<GenderConsiderationDetail> GenderConsiderationDetail { get; set; }
        public DbSet<SecurityDetail> SecurityDetail { get; set; }
        public DbSet<SecurityConsiderationDetail> SecurityConsiderationDetail { get; set; }
        public DbSet<ProjectDetail> ProjectDetail { get; set; }
        public DbSet<ProjectPhaseDetails> ProjectPhaseDetails { get; set; }
        public DbSet<ProjectAssignTo> ProjectAssignTo { get; set; }
        public DbSet<ProjectProgram> ProjectProgram { get; set; }

        public DbSet<ProjectArea> ProjectArea { get; set; }
        public DbSet<ProjectOtherDetail> ProjectOtherDetail { get; set; }
        public DbSet<ProjectSector> ProjectSector { get; set; }
        public DbSet<ClientDetails> ClientDetails { get; set; }

        public DbSet<ProjectPhaseTime> ProjectPhaseTime { get; set; }
        public DbSet<ProjectCommunicationAttachment> ProjectCommunicationAttachment { get; set; }
        public DbSet<StoreSourceCodeDetail> StoreSourceCodeDetail { get; set; }
        public DbSet<ExchangeRateDetail> ExchangeRateDetail { get; set; }
        public DbSet<LanguageDetail> LanguageDetail { get; set; }
        public DbSet<WinProjectDetails> WinProjectDetails { get; set; }
        public DbSet<ApproveProjectDetails> ApproveProjectDetails { get; set; }
        public DbSet<ProjectProposalDetail> ProjectProposalDetail { get; set; }
        public DbSet<DonorCriteriaDetails> DonorCriteriaDetail { get; set; }
        public DbSet<PurposeofInitiativeCriteria> PurposeofInitiativeCriteria { get; set; }
        public DbSet<EligibilityCriteriaDetail> EligibilityCriteriaDetail { get; set; }
        public DbSet<FeasibilityCriteriaDetail> FeasibilityCriteriaDetail { get; set; }
        public DbSet<PriorityCriteriaDetail> PriorityCriteriaDetail { get; set; }
        public DbSet<FinancialCriteriaDetail> FinancialCriteriaDetail { get; set; }
        public DbSet<RiskCriteriaDetail> RiskCriteriaDetail { get; set; }
        public DbSet<TargetBeneficiaryDetail> TargetBeneficiaryDetail { get; set; }
        public DbSet<FinancialProjectDetail> FinancialProjectDetail { get; set; }
        public DbSet<SecurityConsiderationMultiSelect> SecurityConsiderationMultiSelect { get; set; }
        public DbSet<DistrictMultiSelect> DistrictMultiSelect { get; set; }

        public DbSet<ProvinceMultiSelect> ProvinceMultiSelect { get; set; }

        public DbSet<ProjectBudgetLineDetail> ProjectBudgetLineDetail { get; set; }
        public DbSet<ProjectJobDetail> ProjectJobDetail { get; set; }
        public DbSet<ActivityStatusDetail> ActivityStatusDetail { get; set; }
        public DbSet<ProjectActivityDetail> ProjectActivityDetail { get; set; }
        public DbSet<ActivityDocumentsDetail> ActivityDocumentsDetail { get; set; }

        public DbSet<ProjectIndicatorQuestions> ProjectIndicatorQuestions { get; set; }
        public DbSet<ProjectIndicators> ProjectIndicators { get; set; }
        public DbSet<ProjectMonitoringReviewDetail> ProjectMonitoringReviewDetail { get; set; }
        public DbSet<ProjectMonitoringIndicatorQuestions> ProjectMonitoringIndicatorQuestions { get; set; }
        public DbSet<ProjectMonitoringIndicatorDetail> ProjectMonitoringIndicatorDetail { get; set; }
        public DbSet<ProjectActivityProvinceDetail> ProjectActivityProvinceDetail { get; set; }
        public DbSet<ProjectActivityExtensions> ProjectActivityExtensions { get; set; }
        public DbSet<PensionDebitAccountMaster> PensionDebitAccountMaster { get; set; }

        public DbSet<ProjectOpportunityControl> ProjectOpportunityControl { get; set; }
        public DbSet<ProjectLogisticsControl> ProjectLogisticsControl { get; set; }
        public DbSet<ProjectActivitiesControl> ProjectActivitiesControl { get; set; }
        public DbSet<ProjectHiringControl> ProjectHiringControl { get; set; }

        public DbSet<ProjectHiringRequestDetail> ProjectHiringRequestDetail { get; set; }
        public DbSet<HiringRequestCandidates> HiringRequestCandidates { get; set; }
        public DbSet<CountryMultiSelectDetails> CountryMultiSelectDetails { get; set; }

        #endregion

        #region Marketing
        public DbSet<InvoiceGeneration> InvoiceGeneration { get; set; }
        public DbSet<InvoiceApproval> InvoiceApproval { get; set; }
        public DbSet<PlayoutMinutes> PlayoutMinutes { get; set; }
        public DbSet<ScheduleDetails> ScheduleDetails { get; set; }
        public DbSet<PolicyTimeSchedule> PolicyTimeSchedules { get; set; }
        public DbSet<PolicyDaySchedule> PolicyDaySchedules { get; set; }
        public DbSet<PolicySchedule> PolicySchedules { get; set; }
        public DbSet<UnitRate> UnitRates { get; set; }
        public DbSet<PolicyDetail> PolicyDetails { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Quality> Qualities { get; set; }
        public DbSet<ContractDetails> ContractDetails { get; set; }
        public DbSet<JobDetails> JobDetails { get; set; }
        public DbSet<JobPhase> JobPhases { get; set; }
        public DbSet<JobPriceDetails> JobPriceDetails { get; set; }
        public DbSet<MediaCategory> MediaCategories { get; set; }
        public DbSet<Medium> Mediums { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<Nature> Natures { get; set; }
        public DbSet<TimeCategory> TimeCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PolicyOrderSchedule> PolicyOrderSchedules { get; set; }
        #endregion


        #region "Accounting New"
        public DbSet<AccountFilterType> AccountFilterType { get; set; }
        public DbSet<ChartOfAccountNew> ChartOfAccountNew { get; set; }
        public DbSet<GainLossSelectedAccounts> GainLossSelectedAccounts { get; set; }

        #endregion

        //Added by Saksham
        public DbSet<PurchaseUnitType> PurchaseUnitType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionsInRoles>().HasKey(s => new { s.RoleId, s.PermissionId });

            modelBuilder.Entity<VoucherTransactions>().HasOne(x => x.ChartOfAccountDetail).WithMany(b => b.VoucherTransactionsList);
            modelBuilder.Entity<VoucherTransactions>().HasOne(p => p.VoucherDetails).WithMany(b => b.VoucherTransactionDetails);
            modelBuilder.Entity<ProjectActivityDetail>().HasMany(g => g.ProjectActivityProvinceDetail);

            //Global filter on table
            modelBuilder.Entity<JobHiringDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeSalaryDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeDocumentDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeHistoryDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeProfessionalDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<PayrollMonthlyHourDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeAttendance>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeApplyLeave>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<JobGrade>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<InterviewScheduleDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<QualificationDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<HolidayDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<FinancialYearDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeePayroll>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ProjectDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ExchangeRate>().HasIndex(e => e.Date);
            modelBuilder.Entity<VoucherDetail>().HasIndex(e => e.VoucherNo).IsUnique();
            modelBuilder.Entity<VoucherTransactions>().HasIndex(e => e.TransactionId).IsUnique();
            modelBuilder.Entity<VoucherTransactions>().HasIndex(e => new { e.TransactionDate, e.ChartOfAccountNewId });
            modelBuilder.Entity<FinancialProjectDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ProvinceMultiSelect>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<SecurityConsiderationMultiSelect>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<DistrictMultiSelect>().HasQueryFilter(x => x.IsDeleted == false);



            #region "Seed Data"

            // Seed data for the Application Pages
            modelBuilder.Entity<ApplicationPages>().HasData(
                new ApplicationPages { IsDeleted = false, PageId = 1, PageName = "Users", ModuleId = 1, ModuleName = "Users" },
                new ApplicationPages { IsDeleted = false, PageId = 2, PageName = "ChartOfAccount", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 3, PageName = "JournalCodes", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 4, PageName = "CurrencyCodes", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 5, PageName = "OfficeCodes", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 6, PageName = "FinancialYear", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 7, PageName = "PensionRate", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 8, PageName = "EmployeeContract", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 9, PageName = "AppraisalQuestions", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 10, PageName = "TechnicalQuestions", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 11, PageName = "EmailSettings", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 12, PageName = "ExchangeRate", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 13, PageName = "LeaveReason", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 14, PageName = "Profession", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 15, PageName = "Department", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 16, PageName = "Qualification", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 17, PageName = "Designation", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 18, PageName = "JobGrade", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 19, PageName = "SalaryHead", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 20, PageName = "SalaryTaxReportContent", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 21, PageName = "SetPayrollAccount", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = true, PageId = 22, PageName = "Vouchers", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 23, PageName = "Journal", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 24, PageName = "LedgerStatement", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 25, PageName = "BudgetBalance", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 26, PageName = "TrialBalance", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 27, PageName = "FinancialReport", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = true, PageId = 28, PageName = "CategoryPopulator", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 29, PageName = "ExchangeGainLoss", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 30, PageName = "GainLossTransaction", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 31, PageName = "PensionPayments", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 32, PageName = "Employees", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 33, PageName = "PayrollDailyHours", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 34, PageName = "Holidays", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 35, PageName = "Attendance", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 36, PageName = "ApproveLeave", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 37, PageName = "MonthlyPayrollRegister", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 38, PageName = "Jobs", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 39, PageName = "Interview", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 40, PageName = "EmployeeAppraisal", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 41, PageName = "Advances", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 42, PageName = "Summary", ModuleId = 4, ModuleName = "HR" },
                new ApplicationPages { IsDeleted = false, PageId = 43, PageName = "Categories", ModuleId = 5, ModuleName = "Store" },
                new ApplicationPages { IsDeleted = false, PageId = 44, PageName = "StoreSourceCodes", ModuleId = 5, ModuleName = "Store" },
                new ApplicationPages { IsDeleted = false, PageId = 45, PageName = "PaymentTypes", ModuleId = 5, ModuleName = "Store" },
                new ApplicationPages { IsDeleted = false, PageId = 46, PageName = "Store", ModuleId = 5, ModuleName = "Store" },
                new ApplicationPages { IsDeleted = false, PageId = 47, PageName = "ProcurementSummary", ModuleId = 5, ModuleName = "Store" },
                new ApplicationPages { IsDeleted = false, PageId = 48, PageName = "DepreciationReport", ModuleId = 5, ModuleName = "Store" },
                new ApplicationPages { IsDeleted = false, PageId = 49, PageName = "TimeCategory", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 50, PageName = "Quality", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 51, PageName = "Phase", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 52, PageName = "Nature", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 53, PageName = "Medium", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 54, PageName = "MediaCategory", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 55, PageName = "ActivityType", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 56, PageName = "Assets", ModuleId = 7, ModuleName = "AccountingNew" },
                new ApplicationPages { IsDeleted = false, PageId = 57, PageName = "Liabilities", ModuleId = 7, ModuleName = "AccountingNew" },
                new ApplicationPages { IsDeleted = false, PageId = 58, PageName = "Income", ModuleId = 7, ModuleName = "AccountingNew" },
                new ApplicationPages { IsDeleted = false, PageId = 59, PageName = "Expense", ModuleId = 7, ModuleName = "AccountingNew" },
                new ApplicationPages { IsDeleted = false, PageId = 60, PageName = "BalanceSheet", ModuleId = 7, ModuleName = "AccountingNew" },
                new ApplicationPages { IsDeleted = false, PageId = 61, PageName = "IncomeExpenseReport", ModuleId = 7, ModuleName = "AccountingNew" },
                new ApplicationPages { IsDeleted = false, PageId = 62, PageName = "Vouchers", ModuleId = 7, ModuleName = "AccountingNew" },
                new ApplicationPages { IsDeleted = false, PageId = 63, PageName = "Clients", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 64, PageName = "UnitRates", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 65, PageName = "Jobs", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 66, PageName = "Contracts", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 67, PageName = "MyProjects", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 68, PageName = "Donors", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 69, PageName = "ProjectDetails", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 70, PageName = "Proposal", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 71, PageName = "CriteriaEvaluation", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 72, PageName = "Producer", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 73, PageName = "Policy", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 74, PageName = "ProjectJobs", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 75, PageName = "ProjectActivities", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 76, PageName = "Channel", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 77, PageName = "Scheduler", ModuleId = 6, ModuleName = "Marketing" },
                new ApplicationPages { IsDeleted = false, PageId = 78, PageName = "ProjectDashboard", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 79, PageName = "ProjectCashFlow", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 80, PageName = "ProjectBudgetLine", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 81, PageName = "BroadCastPolicy", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 82, PageName = "ProposalReport", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 83, PageName = "ProjectIndicators", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 84, PageName = "ProjectPeople", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 85, PageName = "VoucherSummaryReport", ModuleId = 7, ModuleName = "AccountingNew" },
                new ApplicationPages { IsDeleted = false, PageId = 86, PageName = "HiringRequests", ModuleId = 8, ModuleName = "Projects" },
                new ApplicationPages { IsDeleted = false, PageId = 87, PageName = "PensionDebitAccount", ModuleId = 2, ModuleName = "Code" },
                new ApplicationPages { IsDeleted = false, PageId = 88, PageName = "AttendanceGroupMaster", ModuleId = 2, ModuleName = "Code" }
            );


            #endregion
            //static class is created by the name ApplyAllConfiguration in Extention Folder
            // modelBuilder.ApplyAllConfigurations();
            base.OnModelCreating(modelBuilder);
        }


    }
}
