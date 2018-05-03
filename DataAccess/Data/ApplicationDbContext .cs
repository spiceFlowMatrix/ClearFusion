using DataAccess.DbEntities;
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
        public DbSet<AccountNoteDetail> AccountNoteDetail { get; set; }
        //public DbSet<DataAccess.DbEntities.AdvanceDetail> AdvanceDetail { get; set; }

        public DbSet<Permissions> Permissions { get; set; }

        public DbSet<DesignationDetail> DesignationDetail { get; set; }

        public DbSet<DistrictDetail> DistrictDetail { get; set; }
        public DbSet<EmailType> EmailType { get; set; }
        public DbSet<EmailSettingDetail> EmailSettingDetail { get; set; }

		public DbSet<EmployeeAnalyticalDetail> EmployeeAnalyticalDetail { get; set; }

		public DbSet<EmployeeDetail> EmployeeDetail { get; set; }

        public DbSet<EmployeeDocumentDetail> EmployeeDocumentDetail { get; set; }
		public DbSet<EmployeePaymentTypes> EmployeePaymentTypes { get; set; }
		//public DbSet<EmployeeSalary> EmployeeSalary { get; set; }

		public DbSet<EmployeeHistoryDetail> EmployeeHistoryDetail { get; set; }
		
		public DbSet<EmployeeMonthlyPayroll> EmployeeMonthlyPayroll { get; set; }
		

		//public DbSet<testtable> testtable { get; set; }

		//public DbSet<EmployeeLeaveDetail> EmployeeLeaveDetail { get; set; }

		//public DbSet<EmployeePayrollDetail> EmployeePayrollDetail { get; set; }

		//public DbSet<EmployeeSalaryBudgetDetail> EmployeeSalaryBudgetDetail { get; set; }

		//public DbSet<ExchangeRateDetail> ExchangeRateDetail { get; set; }

		//public DbSet<ExpendableStoreItemDetail> ExpendableStoreItemDetail { get; set; }

		//public DbSet<EXRate_> EXRate_ { get; set; }

		//public DbSet<FuelGeneratorDetail> FuelGeneratorDetail { get; set; }

		//public DbSet<FuelTransportDetail> FuelTransportDetail { get; set; }

		//public DbSet<HiringCandidateDetail> HiringCandidateDetail { get; set; }

		//public DbSet<HiringDetail> HiringDetail { get; set; }

		//public DbSet<InterviewHiringCandidateDetail> InterviewHiringCandidateDetail { get; set; }

		//public DbSet<ItemCodeDemo> ItemCodeDemo { get; set; }

		//public DbSet<JobEntryCategoryDetail> JobEntryCategoryDetail { get; set; }

		public DbSet<JournalDetail> JournalDetail { get; set; }

        //public DbSet<LogisticProjectDetail> LogisticProjectDetail { get; set; }

        //public DbSet<ModulePermissionDetail> ModulePermissionDetail { get; set; }

        //public DbSet<MonthlyExchangeRateDetail> MonthlyExchangeRateDetail { get; set; }

        //public DbSet<NewExpendable> NewExpendable { get; set; }

        //public DbSet<NonExpendableAdditionDetail> NonExpendableAdditionDetail { get; set; }

        //public DbSet<NonExpendableStoreItemDetail> NonExpendableStoreItemDetail { get; set; }

        //public DbSet<NonExpendableStoreItemDetailBackup> NonExpendableStoreItemDetailBackup { get; set; }

        public DbSet<OfficeDetail> OfficeDetail { get; set; }
        //public DbSet<OfficePermissionDetail> OfficePermissionDetail { get; set; }

        //public DbSet<OLDExchangeRateDetail> OLDExchangeRateDetail { get; set; }

        //public DbSet<OpportunityDetail> OpportunityDetail { get; set; }
        //public DbSet<PayrollEmployeeAnalyticalDetail> PayrollEmployeeAnalyticalDetail { get; set; }

        public DbSet<PayrollMonthlyHourDetail> PayrollMonthlyHourDetail { get; set; }

        //public DbSet<PayrollValidateDetail> PayrollValidateDetail { get; set; }

        //public DbSet<PMUActivityDetail> PMUActivityDetail { get; set; }

       // public DbSet<PMUActivityDocumentDetail> PMUActivityDocumentDetail { get; set; }

        //public DbSet<PMUBeneficiaryDetail> PMUBeneficiaryDetail { get; set; }

        //public DbSet<PMUMonitoringDetail> PMUMonitoringDetail { get; set; }

        //public DbSet<PMUProjectDetail> PMUProjectDetail { get; set; }

        //public DbSet<PMUProjectDocumentDetail> PMUProjectDocumentDetail { get; set; }

        //public DbSet<PMUProjectPermissionDetail> PMUProjectPermissionDetail { get; set; }
        //public DbSet<PMUQuestionDetail> PMUQuestionDetail { get; set; }

        //public DbSet<ProductionRateDetail> ProductionRateDetail { get; set; }

        //public DbSet<ProjectTransactionDetail> ProjectTransactionDetail { get; set; }

        //public DbSet<ProjectTransactionDetail1> ProjectTransactionDetail1 { get; set; }

        //public DbSet<ProjectTransactionItemDetail> ProjectTransactionItemDetail { get; set; }

        //public DbSet<ProspectiveEmployeeDetail> ProspectiveEmployeeDetail { get; set; }

        //public DbSet<PurchaseBiddingDetail> PurchaseBiddingDetail { get; set; }

        //public DbSet<PurchaseComparativeDetail> PurchaseComparativeDetail { get; set; }

        //public DbSet<PurchaseRequestDetail> PurchaseRequestDetail { get; set; }

        //public DbSet<StoreDocumentDetail> StoreDocumentDetail { get; set; }

        //public DbSet<StoreItemDetail> StoreItemDetail { get; set; }

        //public DbSet<StoreMasterItemDetail> StoreMasterItemDetail { get; set; }

        //public DbSet<StoreSourceCodeDetail> StoreSourceCodeDetail { get; set; }

       // public DbSet<StoreVoucherDetail> StoreVoucherDetail { get; set; }

        //public DbSet<TempStoreDocumentDetail> TempStoreDocumentDetail { get; set; }

        public DbSet<UserDetails> UserDetails { get; set; }

        //public DbSet<UserTokenCaches> UserTokenCaches { get; set; }

        public DbSet<VoucherDetail> VoucherDetail { get; set; }

        public DbSet<VoucherDocumentDetail> VoucherDocumentDetail { get; set; }

        //public DbSet<VoucherSettingDetail> VoucherSettingDetail { get; set; }

        public DbSet<Department> Department { get; set; }
        public DbSet<PermissionsInRoles> PermissionsInRoles { get; set; }
        public DbSet<CurrencyDetails> CurrencyDetails { get; set; }
        public DbSet<AccountLevel> AccountLevel { get; set; }
        public DbSet<ChartAccountDetail> ChartAccountDetail { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<VoucherType> VoucherType { get; set; }
        public DbSet<VoucherTransactionDetails> VoucherTransactionDetails { get; set; }
        public DbSet<AnalyticalType> AnalyticalType { get; set; }
        public DbSet<AnalyticalDetail> AnalyticalDetail { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
		public DbSet<ProjectDetails> ProjectDetails { get; set; }
		public DbSet<ProjectBudget> ProjectBudget { get; set; }
		public DbSet<ProjectBudgetLine> ProjectBudgetLine { get; set; }
		public DbSet<BudgetReceivable> BudgetReceivable { get; set; }
		public DbSet<BudgetReceivedAmount> BudgetReceivedAmount { get; set; }
		public DbSet<BudgetPayable> BudgetPayable { get; set; }
		public DbSet<BudgetPayableAmount> BudgetPayableAmount { get; set; }		
        public DbSet<CodeType> CodeType { get; set; }
        public DbSet<ProvinceDetails> ProvinceDetails { get; set; }
        public DbSet<CountryDetails> CountryDetails { get; set; }
        public DbSet<NationalityDetails> NationalityDetails { get; set; }
        public DbSet<QualificationDetails> QualificationDetails { get; set; }
        public DbSet<ProfessionDetails> ProfessionDetails { get; set; }
        public DbSet<JobHiringDetails> JobHiringDetails { get; set; }
		//public DbSet<InterviewRoundTypeMaster> InterviewRoundTypeMaster { get; set; }
		public DbSet<BudgetLineEmployees> BudgetLineEmployees { get; set; }
		public DbSet<InterviewScheduleDetails> InterviewScheduleDetails { get; set; }
        //public DbSet<InterviewFeedbackDetails> InterviewFeedbackDetails { get; set; }
        public DbSet<EmployeeSalaryDetails> EmployeeSalaryDetails { get; set; }
        public DbSet<LeaveReasonDetail> LeaveReasonDetail { get; set; }
        public DbSet<AssignLeaveToEmployee> AssignLeaveToEmployee { get; set; }
        public DbSet<FinancialYearDetail> FinancialYearDetail { get; set; }
        public DbSet<TaskMaster> TaskMaster { get; set; }
        public DbSet<ActivityMaster> ActivityMaster { get; set; }
		public DbSet<ProjectDocument> ProjectDocument { get; set; }
        public DbSet<BudgetLineType> BudgetLineType { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
		public DbSet<EmployeeContractType> EmployeeContractType { get; set; }
		public DbSet<AssignActivity> AssignActivity { get; set; }
        public DbSet<AssignActivityApproveBy> AssignActivityApproveBy { get; set; }
        public DbSet<AssignActivityFeedback> AssignActivityFeedback { get; set; }
        public DbSet<EmployeeProfessionalDetail> EmployeeProfessionalDetail { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendance { get; set; }
        public DbSet<EmployeeApplyLeave> EmployeeApplyLeave { get; set; }
        public DbSet<EmployeeHealthDetail> EmployeeHealthDetail { get; set; }
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
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionsInRoles>().HasKey(s => new { s.RoleId, s.PermissionId });            
            modelBuilder.Entity<VoucherTransactionDetails>().HasOne(x => x.CreditAccountDetails).WithMany(b => b.CreditAccountlist);
            modelBuilder.Entity<VoucherTransactionDetails>().HasOne(x => x.DebitAccountDetails).WithMany(b => b.DebitAccountlist);
            modelBuilder.Entity<VoucherTransactionDetails>().HasOne(p => p.VoucherDetails).WithMany(b => b.VoucherTransactionDetails);
            modelBuilder.Entity<ExchangeRate>().HasOne(x => x.CurrencyFrom).WithMany(b => b.ExchangeRateListFrom);
            modelBuilder.Entity<ExchangeRate>().HasOne(x => x.CurrencyTo).WithMany(b => b.ExchangeRateListTo);
			//modelBuilder.Entity<EmployeePaymentType>().HasIndex(b => b.EmployeeID).IsUnique(false);
			//modelBuilder.Entity<EmployeePaymentType>().HasIndex(b => b.EmployeeID).IsUnique(false);

			//Non Clustered Index
			modelBuilder.Entity<BudgetLineEmployees>().HasIndex(t => new { t.OfficeId, t.ProjectId, t.BudgetLineId, t.IsActive });


			//Global filter on table
			modelBuilder.Entity<JobHiringDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeSalaryDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeDocumentDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeHistoryDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeProfessionalDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<PayrollMonthlyHourDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeAttendance>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeHealthDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeeApplyLeave>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<JobGrade>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<InterviewScheduleDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<QualificationDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<HolidayDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<FinancialYearDetail>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<SalaryHeadDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeePayroll>().HasQueryFilter(x => x.IsDeleted == false);
            base.OnModelCreating(modelBuilder);
        }


    }
}
