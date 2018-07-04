using DataAccess.DbEntities;
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
        public DbSet<ChartAccountDetail> ChartAccountDetail { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<VoucherType> VoucherType { get; set; }
        public DbSet<VoucherTransactionDetails> VoucherTransactionDetails { get; set; }
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
        public DbSet<BudgetLineEmployees> BudgetLineEmployees { get; set; }
        public DbSet<InterviewScheduleDetails> InterviewScheduleDetails { get; set; }
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

		public DbSet<ItemSpecificationMaster> ItemSpecificationMaster { get; set; }
		public DbSet<ItemSpecificationDetails> ItemSpecificationDetails { get; set; }
		public DbSet<EmployeeHistoryOutsideOrganization> EmployeeHistoryOutsideOrganization { get; set; }
		public DbSet<EmployeeHistoryOutsideCountry> EmployeeHistoryOutsideCountry { get; set; }
		public DbSet<EmployeeRelativeInfo> EmployeeRelativeInfo { get; set; }
		public DbSet<EmployeeInfoReferences> EmployeeInfoReferences { get; set; }
		public DbSet<EmployeeOtherSkills> EmployeeOtherSkills { get; set; }
		public DbSet<EmployeeSalaryBudget> EmployeeSalaryBudget { get; set; }
		public DbSet<EmployeeEducations> EmployeeEducations { get; set; }		

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
            //modelBuilder.Entity<SalaryHeadDetails>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<EmployeePayroll>().HasQueryFilter(x => x.IsDeleted == false);
            base.OnModelCreating(modelBuilder);
        }


    }
}
