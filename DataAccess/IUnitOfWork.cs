using DataAccess.DbEntities;
using DataAccess.DbEntities.AccountingNew;
using DataAccess.DbEntities.Marketing;
using DataAccess.DbEntities.Project;
using DataAccess.DbEntities.Store;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<AccountNoteDetail> AccountNoteDetailRepository { get; }
        IGenericRepository<Permissions> PermissionRepository { get; }
        IGenericRepository<OfficeDetail> OfficeDetailRepository { get; }
        IGenericRepository<UserDetails> UserDetailsRepository { get; }
        IGenericRepository<Department> DepartmentRepository { get; }
        IGenericRepository<PermissionsInRoles> PermissionsInRolesRepository { get; }
        IGenericRepository<CurrencyDetails> CurrencyDetailsRepository { get; }
        IGenericRepository<JournalDetail> JournalDetailRepository { get; }
        IGenericRepository<EmailType> EmailTypeRepository { get; }
        IGenericRepository<EmailSettingDetail> EmailSettingDetailRepository { get; }
        IGenericRepository<AccountType> AccountTypeRepository { get; }
        IGenericRepository<AccountLevel> AccountLevelRepository { get; }
        IGenericRepository<ChartAccountDetail> ChartAccountDetailRepository { get; }
        IGenericRepository<VoucherType> VoucherTypeRepository { get; }
        IGenericRepository<VoucherDetail> VoucherDetailRepository { get; }
        IGenericRepository<VoucherDocumentDetail> VoucherDocumentDetailRepository { get; }
        IGenericRepository<VoucherTransactions> VoucherTransactionsRepository { get; }
        IGenericRepository<AnalyticalType> AnalyticalTypeRepository { get; }
        IGenericRepository<AnalyticalDetail> AnalyticalDetailRepository { get; }
        IGenericRepository<ExchangeRate> ExchangeRateRepository { get; }
        IGenericRepository<StoreSourceCodeDetail> StoreSourceCodeRepository { get; }
        IGenericRepository<EmployeeDetail> EmployeeDetailRepository { get; }
        IGenericRepository<DesignationDetail> DesignationDetailRepository { get; }
        //IGenericRepository<ProjectBudget> ProjectBudgetRepository { get; }
        IGenericRepository<ProjectDetails> ProjectDetailRepository { get; }

        IGenericRepository<JobHiringDetails> JobHiringDetailsRepository { get; }
        IGenericRepository<ProfessionDetails> ProfessionDetailsRepository { get; }
        IGenericRepository<CountryDetails> CountryDetailsRepository { get; }
        IGenericRepository<ProvinceDetails> ProvinceDetailsRepository { get; }
        IGenericRepository<NationalityDetails> NationalityDetailsRepository { get; }
        IGenericRepository<QualificationDetails> QualificationDetailsRepository { get; }
        IGenericRepository<InterviewRoundTypeMaster> InterviewRoundTypeMasterRepository { get; }
        IGenericRepository<InterviewScheduleDetails> InterviewScheduleDetailsRepository { get; }
        IGenericRepository<InterviewFeedbackDetails> InterviewFeedbackDetailsRepository { get; }
        IGenericRepository<EmployeeSalaryDetails> EmployeeSalaryDetailsRepository { get; }
        IGenericRepository<LeaveReasonDetail> LeaveReasonDetailRepository { get; }
        IGenericRepository<AssignLeaveToEmployee> AssignLeaveToEmployeeRepository { get; }
        IGenericRepository<FinancialYearDetail> FinancialYearDetailRepository { get; }
        IGenericRepository<TaskMaster> TaskMasterRepository { get; }
        IGenericRepository<ActivityMaster> ActivityMasterRepository { get; }
        IGenericRepository<ProjectBudgetLine> ProjectBudgetLineRepository { get; }
        IGenericRepository<EmployeeType> EmployeeTypeRepository { get; }
        IGenericRepository<AssignActivity> AssignActivityRepository { get; }
        IGenericRepository<AssignActivityApproveBy> AssignActivityApproveByRepository { get; }
        IGenericRepository<AssignActivityFeedback> AssignActivityFeedbackRepository { get; }
        IGenericRepository<EmployeeDocumentDetail> EmployeeDocumentDetailRepository { get; }
        IGenericRepository<EmployeeHistoryDetail> EmployeeHistoryDetailRepository { get; }
        IGenericRepository<EmployeeProfessionalDetail> EmployeeProfessionalDetailRepository { get; }
        IGenericRepository<PayrollMonthlyHourDetail> PayrollMonthlyHourDetailRepository { get; }
        IGenericRepository<EmployeeAttendance> EmployeeAttendanceRepository { get; }
        IGenericRepository<EmployeeHealthDetail> EmployeeHealthDetailRepository { get; }
        IGenericRepository<EmployeeApplyLeave> EmployeeApplyLeaveRepository { get; }
        //IGenericRepository<ProjectDocument> ProjectDocumentRepository { get; }
        IGenericRepository<NotesMaster> NotesMasterRepository { get; }
        IGenericRepository<JobGrade> JobGradeRepository { get; }
        IGenericRepository<HolidayDetails> HolidayDetailsRepository { get; }
        IGenericRepository<HolidayWeeklyDetails> HolidayWeeklyDetailRepository { get; }
        IGenericRepository<SalaryHeadDetails> SalaryHeadDetailsRepository { get; }
        IGenericRepository<EmployeePayroll> EmployeePayrollRepository { get; }
        IGenericRepository<EmployeePaymentTypes> EmployeePaymentTypeRepository { get; }
        IGenericRepository<EmployeeMonthlyPayroll> EmployeeMonthlyPayrollRepository { get; }
        //IGenericRepository<BudgetLineEmployees> BudgetLineEmployeesRepository { get; }
        IGenericRepository<EmployeePensionRate> EmployeePensionRateRepository { get; }
        IGenericRepository<EmployeeContractType> EmployeeContractTypeRepository { get; }
        IGenericRepository<ContractTypeContent> ContractTypeContentRepository { get; }
        IGenericRepository<AppraisalGeneralQuestions> AppraisalGeneralQuestionsRepository { get; }
        IGenericRepository<EmployeeAppraisalDetails> EmployeeAppraisalDetailsRepository { get; }
        IGenericRepository<EmployeeAppraisalQuestions> EmployeeAppraisalQuestionsRepository { get; }
        IGenericRepository<EmployeeEvaluation> EmployeeEvaluationRepository { get; }
        IGenericRepository<InterviewTechnicalQuestions> InterviewTechnicalQuestionsRepository { get; }
        IGenericRepository<Advances> AdvancesRepository { get; }

        IGenericRepository<InterviewDetails> InterviewDetailsRepository { get; }
        IGenericRepository<InterviewLanguages> InterviewLanguagesRepository { get; }
        IGenericRepository<InterviewTechnicalQuestion> InterviewTechnicalQuestionRepository { get; }
        IGenericRepository<InterviewTrainings> InterviewTrainingsRepository { get; }
        IGenericRepository<TechnicalQuestion> TechnicalQuestionRepository { get; }
        IGenericRepository<ExistInterviewDetails> ExistInterviewDetailsRepository { get; }
        IGenericRepository<UserDetailOffices> UserOfficesRepository { get; }
        IGenericRepository<StrongandWeakPoints> StrongandWeakPointsRepository { get; }
        IGenericRepository<EmployeeEvaluationTraining> EmployeeEvaluationTrainingRepository { get; }
        IGenericRepository<LoggerDetails> LoggerDetailsRepository { get; }
        IGenericRepository<EmployeeAppraisalTeamMember> EmployeeAppraisalTeamMemberRepository { get; }
        IGenericRepository<RatingBasedCriteria> RatingBasedCriteriaRepository { get; }
        IGenericRepository<CategoryPopulator> CategoryPopulatorRepository { get; }

        // Store repositories
        IGenericRepository<StoreInventory> StoreInventoryRepository { get; }
        IGenericRepository<StoreInventoryItem> StoreInventoryItemRepository { get; }
        IGenericRepository<StoreItemPurchase> StoreItemPurchaseRepository { get; }
        IGenericRepository<ItemPurchaseDocument> ItemPurchaseDocumentRepository { get; }
        IGenericRepository<PurchaseVehicle> PurchaseVehicleRepository { get; }
        IGenericRepository<StorePurchaseOrder> PurchaseOrderRepository { get; }
        IGenericRepository<PurchaseOrderDocument> PurchaseOrderDocumentRepository { get; }
        IGenericRepository<MotorFuel> StoreFuelRepository { get; }
        IGenericRepository<VehicleLocation> VehicleLocationRepository { get; }
        IGenericRepository<VehicleMileage> VehicleMileageRepository { get; }
        IGenericRepository<PurchaseGenerator> PurchaseGeneratorRepository { get; }
        IGenericRepository<MotorMaintenance> MotorMaintenanceRepository { get; }
        IGenericRepository<MotorSparePart> MotorSparePartsRepository { get; }
        IGenericRepository<InventoryItemType> InventoryItemTypeRepository { get; }
        IGenericRepository<PurchaseUnitType> PurchaseUnitTypeRepository { get; }
        IGenericRepository<EmployeePayrollForMonth> EmployeePayrollForMonthRepository { get; }
        IGenericRepository<EmployeePayrollMonth> EmployeePayrollMonthRepository { get; }
        IGenericRepository<EmployeeContract> EmployeeContractRepository { get; }
        IGenericRepository<SalaryTaxReportContent> SalaryTaxReportContentRepository { get; }
        IGenericRepository<ItemSpecificationMaster> ItemSpecificationMasterRepository { get; }
        IGenericRepository<ItemSpecificationDetails> ItemSpecificationDetailsRepository { get; }
        IGenericRepository<StatusAtTimeOfIssue> StatusAtTimeOfIssueRepository { get; }
        IGenericRepository<ReceiptType> ReceiptTypeRepository { get; }


        IGenericRepository<EmployeeHistoryOutsideOrganization> EmployeeHistoryOutsideOrganizationRepository { get; }
        IGenericRepository<EmployeeHistoryOutsideCountry> EmployeeHistoryOutsideCountryRepository { get; }
        IGenericRepository<EmployeeRelativeInfo> EmployeeRelativeInfoRepository { get; }
        IGenericRepository<EmployeeInfoReferences> EmployeeInfoReferencesRepository { get; }
        IGenericRepository<EmployeeOtherSkills> EmployeeOtherSkillsRepository { get; }
        IGenericRepository<EmployeeSalaryBudget> EmployeeSalaryBudgetRepository { get; }
        IGenericRepository<EmployeeEducations> EmployeeEducationsRepository { get; }
        IGenericRepository<EmployeeSalaryAnalyticalInfo> EmployeeSalaryAnalyticalInfoRepository { get; }

        IGenericRepository<EmployeeHealthInfo> EmployeeHealthInfoRepository { get; }
        IGenericRepository<EmployeeHealthQuestion> EmployeeHealthQuestionRepository { get; }
        IGenericRepository<EmployeeMonthlyAttendance> EmployeeMonthlyAttendanceRepository { get; }
        IGenericRepository<PensionPaymentHistory> PensionPaymentHistoryRepository { get; }
        IGenericRepository<PayrollAccountHead> PayrollAccountHeadRepository { get; }
        IGenericRepository<EmployeePayrollAccountHead> EmployeePayrollAccountHeadRepository { get; }
        IGenericRepository<ExchangeRateDetail> ExchangeRateDetailRepository { get; }

        #region Project
        IGenericRepository<DonorDetail> DonorDetailRepository { get; }
        IGenericRepository<SectorDetails> SectorDetailsRepository { get; }
        IGenericRepository<ProgramDetail> ProgramDetailRepository { get; }
        IGenericRepository<AreaDetail> AreaDetailRepository { get; }
        IGenericRepository<DistrictDetail> DistrictDetailRepository { get; }
        IGenericRepository<SecurityDetail> SecurityDetailRepository { get; }
        IGenericRepository<GenderConsiderationDetail> GenderConsiderationRepository { get; }
        IGenericRepository<StrengthConsiderationDetail> StrengthConsiderationRepository { get; }
        IGenericRepository<SecurityConsiderationDetail> SecurityConsiderationDetailRepository { get; }
        IGenericRepository<ProjectDetail> ProjectDetailNewRepository { get; }
        IGenericRepository<ProjectPhaseDetails> ProjectPhaseDetailsRepository { get; }
        IGenericRepository<ProjectOtherDetail> ProjectOtherDetailRepository { get; }
        
        IGenericRepository<ProjectAssignTo> ProjectAssignToRepository { get; }
        IGenericRepository<ProjectProgram> ProjectProgramRepository { get; }
        IGenericRepository<ProjectArea> ProjectAreaRepository { get; }
        IGenericRepository<ProjectSector> ProjectSectorRepository { get; }
        IGenericRepository<EmployeeSalaryPaymentHistory> EmployeeSalaryPaymentHistoryRepository { get; }
        IGenericRepository<EmployeeLanguages> EmployeeLanguagesRepository { get; }
        IGenericRepository<ProjectPhaseTime> ProjectPhaseTimeRepository { get; }
        IGenericRepository<ProjectCommunication> ProjectCommunicationRepository { get; }
        IGenericRepository<ProjectCommunicationAttachment> ProjectCommunicationAttachmentRepository { get; }
        IGenericRepository<ApproveProjectDetails> ApproveProjectDetailsRepository { get; }
        IGenericRepository<WinProjectDetails> WinProjectDetailsRepository { get; }
        IGenericRepository<ProjectProposalDetail> ProjectProposalDetailRepository { get; }
        IGenericRepository<DonorCriteriaDetails> DonorCriteriaDetailsRepository { get; }
        IGenericRepository<PurposeofInitiativeCriteria> PurposeofInitiativeCriteriaRepository { get; }
        IGenericRepository<EligibilityCriteriaDetail> EligibilityCriteriaDetailRepository { get; }
        IGenericRepository<FeasibilityCriteriaDetail> FeasibilityCriteriaDetailRepository { get; }
        IGenericRepository<PriorityCriteriaDetail> PriorityCriteriaDetailRepository { get; }
        IGenericRepository<FinancialCriteriaDetail> FinancialCriteriaDetailRepository { get; }
        IGenericRepository<RiskCriteriaDetail> RiskCriteriaDetailRepository { get; }
        
        #endregion

        #region Marketing
        IGenericRepository<UnitRate> UnitRateRepository { get; }
        IGenericRepository<ActivityType> ActivityTypeRepository { get; }
        IGenericRepository<ContractDetails> ContractDetailsRepository { get; }
        IGenericRepository<JobDetails> JobDetailsRepository { get; }
        IGenericRepository<JobPhase> JobPhaseRepository { get; }
        IGenericRepository<JobPriceDetails> JobPriceDetailsRepository { get; }
        IGenericRepository<Language> LanguageRepository { get; }
        IGenericRepository<MediaCategory> MediaCategoryRepository { get; }
        IGenericRepository<Medium> MediumRepository { get; }
        IGenericRepository<Quality> QualityRepository { get; }
        IGenericRepository<Nature> NatureRepository { get; }
        IGenericRepository<TimeCategory> TimeCategoryRepository { get; }
        IGenericRepository<ClientDetails> ClientDetailsRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        #endregion

        #region "Accounting New"
        IGenericRepository<AccountFilterType> AccountFilterTypeRepository { get; }
        IGenericRepository<ChartOfAccountNew> ChartOfAccountNewRepository { get; }

        #endregion


        void Save();
        Task<int> SaveAsync();
        ApplicationDbContext GetDbContext();

    }
}

