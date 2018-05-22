using DataAccess.DbEntities;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUnitOfWork
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
        IGenericRepository<VoucherTransactionDetails> VoucherTransactionDetailsRepository { get; }
        IGenericRepository<AnalyticalType> AnalyticalTypeRepository { get; }
        IGenericRepository<AnalyticalDetail> AnalyticalDetailRepository { get; }
        IGenericRepository<ExchangeRate> ExchangeRateRepository { get; }
        IGenericRepository<StoreSourceCodeDetail> StoreSourceCodeRepository { get; }
        IGenericRepository<EmployeeDetail> EmployeeDetailRepository { get; }
        IGenericRepository<DesignationDetail> DesignationDetailRepository { get; }
        IGenericRepository<ProjectBudget> ProjectBudgetRepository { get; }
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
        IGenericRepository<ProjectDocument> ProjectDocumentRepository { get; }
        IGenericRepository<NotesMaster> NotesMasterRepository { get; }
        IGenericRepository<JobGrade> JobGradeRepository { get; }
        IGenericRepository<HolidayDetails> HolidayDetailsRepository { get; }
        IGenericRepository<HolidayWeeklyDetails> HolidayWeeklyDetailRepository { get; }
        IGenericRepository<SalaryHeadDetails> SalaryHeadDetailsRepository { get; }
        IGenericRepository<EmployeePayroll> EmployeePayrollRepository { get; }
        IGenericRepository<EmployeePaymentTypes> EmployeePaymentTypeRepository { get; }
        IGenericRepository<EmployeeMonthlyPayroll> EmployeeMonthlyPayrollRepository { get; }
        IGenericRepository<BudgetLineEmployees> BudgetLineEmployeesRepository { get; }
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

		void Save();
        Task<int> SaveAsync();
        ApplicationDbContext GetDbContext();

    }
}

