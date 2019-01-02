using DataAccess.DbEntities;
using DataAccess.DbEntities.AccountingNew;
using DataAccess.DbEntities.Marketing;
using DataAccess.DbEntities.OnlyForDT;
using DataAccess.DbEntities.Project;
using DataAccess.DbEntities.Store;
using HumanitarianAssistance.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Entities
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //DataTransfer
        public DbSet<EmployeeDetailDT> EmployeeDetailDT { get; set; }
       


        public DbSet<AccountNoteDetail> AccountNoteDetail { get; set; }

        public DbSet<Permissions> Permissions { get; set; }

        public DbSet<DesignationDetail> DesignationDetail { get; set; }

        public DbSet<DistrictDetail> DistrictDetail { get; set; }
        public DbSet<EmailType> EmailType { get; set; }
        public DbSet<EmailSettingDetail> EmailSettingDetail { get; set; }

        public DbSet<EmployeeAnalyticalDetail> EmployeeAnalyticalDetail { get; set; }

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

        public DbSet<VoucherDocumentDetail> VoucherDocumentDetail { get; set; }

        public DbSet<Department> Department { get; set; }
        public DbSet<PermissionsInRoles> PermissionsInRoles { get; set; }
        public DbSet<CurrencyDetails> CurrencyDetails { get; set; }
        //public DbSet<ChartAccountDetail> ChartAccountDetail { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<VoucherType> VoucherType { get; set; }
        //public DbSet<VoucherTransactionDetails> VoucherTransactionDetails { get; set; }
        public DbSet<VoucherTransactions> VoucherTransactions { get; set; }
        public DbSet<AnalyticalDetail> AnalyticalDetail { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<ProjectDetails> ProjectDetails { get; set; }
        //public DbSet<ProjectBudget> ProjectBudget { get; set; }
        public DbSet<ProjectBudgetLine> ProjectBudgetLine { get; set; }
        public DbSet<BudgetReceivable> BudgetReceivable { get; set; }
        public DbSet<BudgetReceivedAmount> BudgetReceivedAmount { get; set; }
        //public DbSet<BudgetPayable> BudgetPayable { get; set; }
        //public DbSet<BudgetPayableAmount> BudgetPayableAmount { get; set; }
        public DbSet<CodeType> CodeType { get; set; }
        public DbSet<ProvinceDetails> ProvinceDetails { get; set; }
        public DbSet<CountryDetails> CountryDetails { get; set; }
        public DbSet<NationalityDetails> NationalityDetails { get; set; }
        public DbSet<QualificationDetails> QualificationDetails { get; set; }
        public DbSet<ProfessionDetails> ProfessionDetails { get; set; }
        public DbSet<JobHiringDetails> JobHiringDetails { get; set; }
        //public DbSet<BudgetLineEmployees> BudgetLineEmployees { get; set; }
        public DbSet<InterviewScheduleDetails> InterviewScheduleDetails { get; set; }
        public DbSet<EmployeeSalaryDetails> EmployeeSalaryDetails { get; set; }
        public DbSet<LeaveReasonDetail> LeaveReasonDetail { get; set; }
        public DbSet<AssignLeaveToEmployee> AssignLeaveToEmployee { get; set; }
        public DbSet<FinancialYearDetail> FinancialYearDetail { get; set; }
        public DbSet<TaskMaster> TaskMaster { get; set; }
        public DbSet<ActivityMaster> ActivityMaster { get; set; }
        //public DbSet<ProjectDocument> ProjectDocument { get; set; }
        public DbSet<BudgetLineType> BudgetLineType { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<EmployeeContractType> EmployeeContractType { get; set; }
        public DbSet<AssignActivity> AssignActivity { get; set; }
        public DbSet<AssignActivityApproveBy> AssignActivityApproveBy { get; set; }
        public DbSet<AssignActivityFeedback> AssignActivityFeedback { get; set; }
        public DbSet<EmployeeProfessionalDetail> EmployeeProfessionalDetail { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendance { get; set; }
        public DbSet<EmployeeApplyLeave> EmployeeApplyLeave { get; set; }
        //public DbSet<EmployeeHealthDetail> EmployeeHealthDetail { get; set; }
        public DbSet<NotesMaster> NotesMaster { get; set; }
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
        public DbSet<CategoryPopulator> CategoryPopulator { get; set; }
        public DbSet<LoggerDetails> LoggerDetails { get; set; }

        // Store
        public DbSet<StoreInventory> StoreInventories { get; set; }
        public DbSet<StoreInventoryItem> InventoryItems { get; set; }
        public DbSet<StoreItemPurchase> StoreItemPurchases { get; set; }
        public DbSet<StorePurchaseOrder> StorePurchaseOrders { get; set; }
        public DbSet<ItemPurchaseDocument> ItemPurchaseDocuments { get; set; }
        public DbSet<PurchaseVehicle> PurchaseVehicles { get; set; }
        public DbSet<MotorFuel> VehicleFuel { get; set; }
        public DbSet<VehicleLocation> VehicleLocations { get; set; }
        public DbSet<VehicleMileage> VehicleMileages { get; set; }
        public DbSet<PurchaseGenerator> PurchaseGenerators { get; set; }
        public DbSet<MotorMaintenance> MotorMaintenances { get; set; }
        public DbSet<MotorSparePart> MotorSpareParts { get; set; }
        //

        public DbSet<EmployeePayrollForMonth> EmployeePayrollForMonth { get; set; }
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
        public DbSet<EmployeePayrollDetailTest> EmployeePayrollDetailTest { get; set; }//to be removed after data transfer
        public DbSet<PensionPaymentHistory> PensionPaymentHistory { get; set; }
        public DbSet<PayrollAccountHead> PayrollAccountHead { get; set; }
        public DbSet<EmployeePayrollAccountHead> EmployeePayrollAccountHead { get; set; }
        public DbSet<EmployeeSalaryPaymentHistory> EmployeeSalaryPaymentHistory { get; set; }
        public DbSet<AccountLevel> AccountLevel { get; set; }
        public DbSet<EmployeeLanguages> EmployeeLanguages { get; set; }
        public DbSet<AccountHeadType> AccountHeadType { get; set; }
        public DbSet<PaymentTypes> PaymentTypes { get; set; }
        public DbSet<PriorityOtherDetail> PriorityOtherDetail { get; set; }
        public DbSet<ApplicationPages> ApplicationPages { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<CEFeasibilityExpertOtherDetail> CEFeasibilityExpertOtherDetail { get; set; }
        public DbSet<CEAgeGroupDetail> CEAgeGroupDetail { get; set; }
        public DbSet<CEOccupationDetail> CEOccupationDetail { get; set; }

        public DbSet<CEAssumptionDetail> CEAssumptionDetail { get; set; }
        public DbSet<DonorEligibilityCriteria> DonorEligibilityCriteria { get; set; }

        



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
        public DbSet<ProjectCommunication> ProjectCommunication { get; set; }
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



        #endregion

        #region Marketing
        public DbSet<UnitRate> UnitRates { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Quality> Qualities { get; set; }
        public DbSet<ContractDetails> ContractDetails { get; set; }
        public DbSet<JobDetails> JobDetails { get; set; }
        public DbSet<JobPhase> JobPhases { get; set; }
        public DbSet<JobPriceDetails> JobPriceDetails { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<MediaCategory> MediaCategories { get; set; }
        public DbSet<Medium> Mediums { get; set; }
        public DbSet<Nature> Natures { get; set; }
        public DbSet<TimeCategory> TimeCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        #endregion


        #region "Accounting New"
        public DbSet<AccountFilterType> AccountFilterType { get; set; }
        public DbSet<ChartOfAccountNew> ChartOfAccountNew { get; set; }
        #endregion




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionsInRoles>().HasKey(s => new { s.RoleId, s.PermissionId });
           // modelBuilder.Entity<RolePermissions>().HasKey(s => new { s.RoleId});

            modelBuilder.Entity<VoucherTransactions>().HasOne(x => x.ChartOfAccountDetail).WithMany(b => b.VoucherTransactionsList);
            modelBuilder.Entity<VoucherTransactions>().HasOne(p => p.VoucherDetails).WithMany(b => b.VoucherTransactionDetails);

            //modelBuilder.Entity<ChartOfAccountNew>().HasMany(x => x.CreditAccountlist);
            //modelBuilder.Entity<ChartOfAccountNew>().HasMany(x => x.DebitAccountlist);




            //Global filter on table
            modelBuilder.Entity<JobHiringDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeSalaryDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeDocumentDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeHistoryDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeProfessionalDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<PayrollMonthlyHourDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeAttendance>().HasQueryFilter(x => x.IsDeleted == false);
            //modelBuilder.Entity<EmployeeHealthDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeApplyLeave>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<JobGrade>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<InterviewScheduleDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<QualificationDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<HolidayDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<FinancialYearDetail>().HasQueryFilter(x => x.IsDeleted == false);
            //modelBuilder.Entity<SalaryHeadDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeePayroll>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ProjectDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ExchangeRate>().HasIndex(e => e.Date);

            //modelBuilder.Entity<ChartAccountDetail>().HasIndex(e => e.AccountCode).IsUnique();
            modelBuilder.Entity<VoucherDetail>().HasIndex(e => e.VoucherNo).IsUnique();
            modelBuilder.Entity<VoucherTransactions>().HasIndex(e => e.TransactionId).IsUnique();
            modelBuilder.Entity<VoucherTransactions>().HasIndex(e => new { e.TransactionDate, e.ChartOfAccountNewId });
            modelBuilder.Entity<FinancialProjectDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ProvinceMultiSelect>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<SecurityConsiderationMultiSelect>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<DistrictMultiSelect>().HasQueryFilter(x => x.IsDeleted == false);



            #region "Seed Data"

            modelBuilder.Entity<AccountLevel>().HasData(
                new AccountLevel { AccountLevelId = 1, AccountLevelName = "Main Level Accounts" },
                new AccountLevel { AccountLevelId = 2, AccountLevelName = "Control Level Accounts" },
                new AccountLevel { AccountLevelId = 3, AccountLevelName = "Sub Level Accounts" },
                new AccountLevel { AccountLevelId = 4, AccountLevelName = "Input Level Accounts" }
            );

            modelBuilder.Entity<PayrollAccountHead>().HasData(
                new PayrollAccountHead { AccountNo = null, IsDeleted = false, PayrollHeadId = 1, Description = null, PayrollHeadName = "Net Salary", PayrollHeadTypeId = 3, TransactionTypeId = 1 },
                new PayrollAccountHead { AccountNo = null, IsDeleted = false, PayrollHeadId = 2, Description = null, PayrollHeadName = "Advance Deduction", PayrollHeadTypeId = 2, TransactionTypeId = 1 },
                new PayrollAccountHead { AccountNo = null, IsDeleted = false, PayrollHeadId = 3, Description = null, PayrollHeadName = "Salary Tax", PayrollHeadTypeId = 2, TransactionTypeId = 1 },
                new PayrollAccountHead { AccountNo = null, IsDeleted = true, PayrollHeadId = 4, Description = null, PayrollHeadName = "Gross Salary", PayrollHeadTypeId = 3, TransactionTypeId = 2 },
                new PayrollAccountHead { AccountNo = null, IsDeleted = false, PayrollHeadId = 5, Description = null, PayrollHeadName = "Pension", PayrollHeadTypeId = 2, TransactionTypeId = 1 }
            );

            modelBuilder.Entity<CurrencyDetails>().HasData(
                new CurrencyDetails { CurrencyId = 1, CurrencyName = "Afghanistan", CurrencyCode = "AFG", IsDeleted = false, Status = false, SalaryTaxFlag = true },
                new CurrencyDetails { CurrencyId = 2, CurrencyName = "European Curency", CurrencyCode = "EUR", IsDeleted = false, Status = false, SalaryTaxFlag = false },
                new CurrencyDetails { CurrencyId = 3, CurrencyName = "Pakistani Rupees", CurrencyCode = "PKR", IsDeleted = false, Status = true, SalaryTaxFlag = false }, //base currency :  Status = true
                new CurrencyDetails { CurrencyId = 4, CurrencyName = "US Dollars", CurrencyCode = "USD", IsDeleted = false, Status = false, SalaryTaxFlag = false }
            );

            modelBuilder.Entity<ReceiptType>().HasData(
               new ReceiptType { IsDeleted = false, ReceiptTypeId = 1, ReceiptTypeName = "Purchased" },
               new ReceiptType { IsDeleted = false, ReceiptTypeId = 2, ReceiptTypeName = "Transfers" },
               new ReceiptType { IsDeleted = false, ReceiptTypeId = 3, ReceiptTypeName = "Donation" },
               new ReceiptType { IsDeleted = false, ReceiptTypeId = 4, ReceiptTypeName = "Take Over" },
               new ReceiptType { IsDeleted = false, ReceiptTypeId = 5, ReceiptTypeName = "Loan" },
               new ReceiptType { IsDeleted = false, ReceiptTypeId = 6, ReceiptTypeName = "Return" },
               new ReceiptType { IsDeleted = false, ReceiptTypeId = 7, ReceiptTypeName = "Other" }
            );

            modelBuilder.Entity<StatusAtTimeOfIssue>().HasData(
               new StatusAtTimeOfIssue { IsDeleted = false, StatusAtTimeOfIssueId = 1, StatusName = "New" },
               new StatusAtTimeOfIssue { IsDeleted = false, StatusAtTimeOfIssueId = 2, StatusName = "Useable" },
               new StatusAtTimeOfIssue { IsDeleted = false, StatusAtTimeOfIssueId = 3, StatusName = "To Repair" },
               new StatusAtTimeOfIssue { IsDeleted = false, StatusAtTimeOfIssueId = 4, StatusName = "Damage" },
               new StatusAtTimeOfIssue { IsDeleted = false, StatusAtTimeOfIssueId = 5, StatusName = "Sold" },
               new StatusAtTimeOfIssue { IsDeleted = false, StatusAtTimeOfIssueId = 6, StatusName = "Stolen" },
               new StatusAtTimeOfIssue { IsDeleted = false, StatusAtTimeOfIssueId = 7, StatusName = "Handover" },
               new StatusAtTimeOfIssue { IsDeleted = false, StatusAtTimeOfIssueId = 8, StatusName = "Demolished" },
               new StatusAtTimeOfIssue { IsDeleted = false, StatusAtTimeOfIssueId = 9, StatusName = "Broken" }
            );

            modelBuilder.Entity<AccountHeadType>().HasData(
               new AccountHeadType { AccountHeadTypeId = 1, AccountHeadTypeName = "Assets", IsDeleted = false },
               new AccountHeadType { AccountHeadTypeId = 2, AccountHeadTypeName = "Liabilities", IsDeleted = false },
               new AccountHeadType { AccountHeadTypeId = 3, AccountHeadTypeName = "Donors Equity", IsDeleted = false },
               new AccountHeadType { AccountHeadTypeId = 4, AccountHeadTypeName = "Income", IsDeleted = false },
               new AccountHeadType { AccountHeadTypeId = 5, AccountHeadTypeName = "Expense", IsDeleted = false }
            );

            modelBuilder.Entity<EmployeeContractType>().HasData(
               new EmployeeContractType { EmployeeContractTypeId = 1, EmployeeContractTypeName = "Probationary" },
               new EmployeeContractType { EmployeeContractTypeId = 2, EmployeeContractTypeName = "PartTime" },
               new EmployeeContractType { EmployeeContractTypeId = 3, EmployeeContractTypeName = "Permanent" }
            );

            modelBuilder.Entity<EmailType>().HasData(
                new EmailType { IsDeleted = false, EmailTypeId = 1, EmailTypeName = "General" },
                new EmailType { IsDeleted = false, EmailTypeId = 2, EmailTypeName = "Bidding Panel" }
            );

            modelBuilder.Entity<CountryDetails>().HasData(
                new CountryDetails { IsDeleted = false, CountryId = 1, CountryName = "Afghanistan" },
                new CountryDetails { IsDeleted = false, CountryId = 2, CountryName = "United States" }
            );

            modelBuilder.Entity<ProvinceDetails>().HasData(
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 1, ProvinceName = "Badghis" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 2, ProvinceName = "Baghlan" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 3, ProvinceName = "Balkh" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 4, ProvinceName = "Bamyan" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 5, ProvinceName = "Daykundi" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 6, ProvinceName = "Farah" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 7, ProvinceName = "Faryab" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 8, ProvinceName = "Ghazni" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 9, ProvinceName = "Ghor" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 10, ProvinceName = "Helmand" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 11, ProvinceName = "Herat" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 12, ProvinceName = "Jowzjan" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 13, ProvinceName = "Kabul" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 14, ProvinceName = "Kandahar" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 15, ProvinceName = "Kapisa" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 16, ProvinceName = "Khost" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 17, ProvinceName = "Kunar" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 18, ProvinceName = "Kunduz" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 19, ProvinceName = "Laghman" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 20, ProvinceName = "Logar" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 21, ProvinceName = "Maidan Wardak" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 22, ProvinceName = "Nangarhar" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 23, ProvinceName = "Nimruz" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 24, ProvinceName = "Nuristan" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 25, ProvinceName = "Paktia" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 26, ProvinceName = "Paktika" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 27, ProvinceName = "Panjshir" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 28, ProvinceName = "Parwan" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 29, ProvinceName = "Samangan" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 30, ProvinceName = "Sar-e Pol" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 31, ProvinceName = "Takhar" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 32, ProvinceName = "Urozgan" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 33, ProvinceName = "Zabul" },
               new ProvinceDetails { IsDeleted = false, CountryId = 1, ProvinceId = 34, ProvinceName = "Alabama" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 35, ProvinceName = "Alaska" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 36, ProvinceName = "Arizona" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 37, ProvinceName = "Arkansas" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 38, ProvinceName = "California" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 39, ProvinceName = "Colorado" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 40, ProvinceName = "Connecticut" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 41, ProvinceName = "Delaware" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 42, ProvinceName = "Florida" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 43, ProvinceName = "Georgia" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 44, ProvinceName = "Hawaii" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 45, ProvinceName = "Idaho" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 46, ProvinceName = "Illinois" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 47, ProvinceName = "Indiana" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 48, ProvinceName = "Iowa" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 49, ProvinceName = "Kansas" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 50, ProvinceName = "Kentucky" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 51, ProvinceName = "Louisiana" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 52, ProvinceName = "Maine" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 53, ProvinceName = "Maryland" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 54, ProvinceName = "Massachusetts" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 55, ProvinceName = "Michigan" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 56, ProvinceName = "Minnesota" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 57, ProvinceName = "Mississippi" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 58, ProvinceName = "Missouri" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 59, ProvinceName = "Montana" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 60, ProvinceName = "Nebraska" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 61, ProvinceName = "Nevada" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 62, ProvinceName = "New Hampshire" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 63, ProvinceName = "New Jersey" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 64, ProvinceName = "New Mexico" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 65, ProvinceName = "New York" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 66, ProvinceName = "North Carolina" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 67, ProvinceName = "North Dakota" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 68, ProvinceName = "Ohio" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 69, ProvinceName = "Oklahoma" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 70, ProvinceName = "Oregon" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 71, ProvinceName = "Pennsylvania" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 72, ProvinceName = "Rhode Island" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 73, ProvinceName = "South Carolina" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 74, ProvinceName = "South Dakota" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 75, ProvinceName = "Tennessee" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 76, ProvinceName = "Texas" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 77, ProvinceName = "Utah" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 78, ProvinceName = "Vermont" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 79, ProvinceName = "Virginia" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 80, ProvinceName = "Washington" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 81, ProvinceName = "West Virginia" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 82, ProvinceName = "Wisconsin" },
               new ProvinceDetails { IsDeleted = false, CountryId = 2, ProvinceId = 83, ProvinceName = "Wyoming" }
            );

            modelBuilder.Entity<VoucherType>().HasData(
                 new VoucherType { VoucherTypeId = 1, VoucherTypeName = "Adjustment" },
                 new VoucherType { VoucherTypeId = 2, VoucherTypeName = "Journal" }
            );

            modelBuilder.Entity<EmployeeType>().HasData(
                 new EmployeeType { EmployeeTypeId = 1, EmployeeTypeName = "Prospective", IsDeleted = false },
                 new EmployeeType { EmployeeTypeId = 2, EmployeeTypeName = "Active", IsDeleted = false },
                 new EmployeeType { EmployeeTypeId = 3, EmployeeTypeName = "Terminated", IsDeleted = false }
            );

            modelBuilder.Entity<OfficeDetail>().HasData(
                 new OfficeDetail { OfficeId = 1, OfficeCode = "A0001", OfficeKey = "AF", OfficeName = "Afghanistan", IsDeleted = false }
            );

            modelBuilder.Entity<Department>().HasData(
                 new Department { DepartmentId = 1, DepartmentName = "Administration", OfficeId = 1, IsDeleted = false }
            );

            modelBuilder.Entity<StrengthConsiderationDetail>().HasData(
                 new StrengthConsiderationDetail { StrengthConsiderationId = 1, StrengthConsiderationName = "Gender Friendly", IsDeleted = false },
                 new StrengthConsiderationDetail { StrengthConsiderationId = 2, StrengthConsiderationName = "Not Gender Friendly", IsDeleted = false },
                 new StrengthConsiderationDetail { StrengthConsiderationId = 3, StrengthConsiderationName = "Not Applicable", IsDeleted = false }
            );

            modelBuilder.Entity<GenderConsiderationDetail>().HasData(
                 new GenderConsiderationDetail { GenderConsiderationId = 1, GenderConsiderationName = "50 % F - 50 % M Excellent", IsDeleted = false },
                 new GenderConsiderationDetail { GenderConsiderationId = 2, GenderConsiderationName = "40 % F - 60 % M Very Good", IsDeleted = false },
                 new GenderConsiderationDetail { GenderConsiderationId = 3, GenderConsiderationName = "30 % F - 70 % M Good", IsDeleted = false },
                 new GenderConsiderationDetail { GenderConsiderationId = 4, GenderConsiderationName = "25 % F - 75 % M Poor", IsDeleted = false },
                 new GenderConsiderationDetail { GenderConsiderationId = 5, GenderConsiderationName = "20 % F - 80 % M Poor", IsDeleted = false },
                 new GenderConsiderationDetail { GenderConsiderationId = 6, GenderConsiderationName = "10 % F - 90 % M Poor", IsDeleted = false },
                 new GenderConsiderationDetail { GenderConsiderationId = 7, GenderConsiderationName = "5 % F - 95 % M Poor", IsDeleted = false },
                 new GenderConsiderationDetail { GenderConsiderationId = 8, GenderConsiderationName = "0 % F - 100 % M Poor", IsDeleted = false }
            );

            modelBuilder.Entity<SecurityDetail>().HasData(
                 new SecurityDetail { SecurityId = 1, SecurityName = "Insecure", IsDeleted = false },
                 new SecurityDetail { SecurityId = 2, SecurityName = "Partially Insecure", IsDeleted = false },
                 new SecurityDetail { SecurityId = 3, SecurityName = "Secure (Green Area)", IsDeleted = false }
            );

            modelBuilder.Entity<SecurityConsiderationDetail>().HasData(
                 new SecurityConsiderationDetail { SecurityConsiderationId = 1, SecurityConsiderationName = "Project Staff Cannot Visit Project Site", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 2, SecurityConsiderationName = "Beneficiaries cannot be reached", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 3, SecurityConsiderationName = "Resources cannot be deployed", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 4, SecurityConsiderationName = "Threat exit for future (Highly)", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 5, SecurityConsiderationName = "Project staff access the are partially", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 6, SecurityConsiderationName = "Bonfires can be reached partially", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 7, SecurityConsiderationName = "Resources can be deployed partially", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 8, SecurityConsiderationName = "Future Threats exits", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 9, SecurityConsiderationName = "No barrier for staff to access the area", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 10, SecurityConsiderationName = "No obstacle for deploying Resources & office", IsDeleted = false },
                 new SecurityConsiderationDetail { SecurityConsiderationId = 11, SecurityConsiderationName = "Future Threats expected", IsDeleted = false }
            );

            modelBuilder.Entity<SalaryHeadDetails>().HasData(
                 new SalaryHeadDetails { SalaryHeadId = 1, HeadName = "Tr Allowance", Description = "Tr Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 2, HeadName = "Food Allowance", Description = "Food Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 3, HeadName = "Fine Deduction", Description = "Fine Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 4, HeadName = "Capacity Building Deduction", Description = "Capacity Building Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 5, HeadName = "Security Deduction", Description = "Security Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 6, HeadName = "Other Allowance", Description = "Other Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 7, HeadName = "Other Deduction", Description = "Other Deduction", HeadTypeId = 2, TransactionTypeId = 1, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 8, HeadName = "Medical Allowance", Description = "Medical Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 9, HeadName = "Other1Allowance", Description = "Other1Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 10, HeadName = "Other2Allowance", Description = "Other2Allowance", HeadTypeId = 1, TransactionTypeId = 2, IsDeleted = false },
                 new SalaryHeadDetails { SalaryHeadId = 11, HeadName = "Basic Pay (In hours)", Description = "Basic Pay (In hours)", HeadTypeId = 3, TransactionTypeId = 2, IsDeleted = false });


            modelBuilder.Entity<CodeType>().HasData(
                 new CodeType { CodeTypeId = 1, CodeTypeName = "Organizations" },
                 new CodeType { CodeTypeId = 2, CodeTypeName = "Suppliers" },
                 new CodeType { CodeTypeId = 3, CodeTypeName = "Repair Shops" },
                 new CodeType { CodeTypeId = 4, CodeTypeName = "Individual/Others" },
                 new CodeType { CodeTypeId = 5, CodeTypeName = "Locations/Stores" }
            );

            modelBuilder.Entity<LanguageDetail>().HasData(
                new LanguageDetail { IsDeleted = false, LanguageId = 1, LanguageName = "Arabic" },
                new LanguageDetail { IsDeleted = false, LanguageId = 2, LanguageName = "Dari" },
                new LanguageDetail { IsDeleted = false, LanguageId = 3, LanguageName = "English" },
                new LanguageDetail { IsDeleted = false, LanguageId = 4, LanguageName = "French" },
                new LanguageDetail { IsDeleted = false, LanguageId = 5, LanguageName = "German" },
                new LanguageDetail { IsDeleted = false, LanguageId = 6, LanguageName = "Pashto" },
                new LanguageDetail { IsDeleted = false, LanguageId = 7, LanguageName = "Russian" },
                new LanguageDetail { IsDeleted = false, LanguageId = 8, LanguageName = "Turkish" },
                new LanguageDetail { IsDeleted = false, LanguageId = 9, LanguageName = "Turkmani" },
                new LanguageDetail { IsDeleted = false, LanguageId = 10, LanguageName = "Urdu" },
                new LanguageDetail { IsDeleted = false, LanguageId = 11, LanguageName = "Uzbek" }
            );

            modelBuilder.Entity<AccountFilterType>().HasData(
              new AccountFilterType { IsDeleted = false, AccountFilterTypeId = 1, AccountFilterTypeName = "Inventory Account" },
              new AccountFilterType { IsDeleted = false, AccountFilterTypeId = 2, AccountFilterTypeName = "Salary Account" }
          );

            modelBuilder.Entity<ProjectPhaseDetails>().HasData(
              new ProjectPhaseDetails { IsDeleted = false, ProjectPhaseDetailsId = 1, ProjectPhase = "Data Entry" }
          );

            modelBuilder.Entity<ActivityType>().HasData(
              new ActivityType { IsDeleted = false, ActivityTypeId = 1, ActivityName = "Broadcasting" },
              new ActivityType { IsDeleted = false, ActivityTypeId = 2, ActivityName = "Production" }
          );

            modelBuilder.Entity<ApplicationPages>().HasData(
                new ApplicationPages { IsDeleted = false, PageId = 1,  PageName = "Users", ModuleId = 1, ModuleName = "Users"},
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
                new ApplicationPages { IsDeleted = false, PageId = 22, PageName = "Vouchers", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 23, PageName = "Journal", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 24, PageName = "LedgerStatement", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 25, PageName = "BudgetBalance", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 26, PageName = "TrialBalance", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 27, PageName = "FinancialReport", ModuleId = 3, ModuleName = "Accounting" },
                new ApplicationPages { IsDeleted = false, PageId = 28, PageName = "CategoryPopulator", ModuleId = 3, ModuleName = "Accounting" },
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
                new ApplicationPages { IsDeleted = false, PageId = 48, PageName = "DepreciationReport", ModuleId = 5, ModuleName = "Store" }
                );
            #endregion




            base.OnModelCreating(modelBuilder);
        }


    }
}
