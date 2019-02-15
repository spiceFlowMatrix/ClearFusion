using System;
using System.Collections.Generic;
using System.Text;
using HumanitarianAssistance.Entities.Models;
using HumanitarianAssistance.Entities;
using DataAccess.DbEntities;
using System.Threading.Tasks;
using DataAccess.DbEntities.Store;
using DataAccess.DbEntities.Project;
using DataAccess.DbEntities.Marketing;
using DataAccess.DbEntities.AccountingNew;
using DataAccess.DbEntities.ErrorLog;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _mschaContext;
        private IGenericRepository<AccountNoteDetail> _accountNoteDetail;
        private IGenericRepository<Permissions> _permissinsRepository;
        private IGenericRepository<OfficeDetail> _officedetailsRepository;
        private IGenericRepository<UserDetails> _userdetailsRepository;
        private IGenericRepository<Department> _deparmentRepository;
        private IGenericRepository<PermissionsInRoles> _permissionInRolesRepository;
        private IGenericRepository<CurrencyDetails> _currencydetailsRepository;
        private IGenericRepository<JournalDetail> _journalDetailRepository;
        private IGenericRepository<EmailType> _emailtypeRepository;
        private IGenericRepository<EmailSettingDetail> _emailsettingdetailRepository;
        private IGenericRepository<AccountType> _accounttypeRepository;
        private IGenericRepository<AccountLevel> _accountlevelRepository;
        //private IGenericRepository<ChartAccountDetail> _chartaccountdetailRepository;
        private IGenericRepository<VoucherType> _vouchertypeRepository;
        private IGenericRepository<VoucherDetail> _voucherdetailsRepository;
        private IGenericRepository<VoucherDocumentDetail> _voucherdocumentdetailRepository;
        //private IGenericRepository<VoucherTransactionDetails> _vouchertransactiondetailsRepository;
        private IGenericRepository<VoucherTransactions> _voucherTransactionsRepository;
        private IGenericRepository<AnalyticalType> _analyticaltypeRepository;
        private IGenericRepository<AnalyticalDetail> _analyticaldetailRepository;
        private IGenericRepository<ExchangeRate> _exchangerateRepository;
        private IGenericRepository<StoreSourceCodeDetail> _storesourcecodeRepository;
        private IGenericRepository<EmployeeDetail> _employeedetailRepository;
        private IGenericRepository<DesignationDetail> _designationdetailRepository;
        //private IGenericRepository<ProjectBudget> _projectBudgetRepository;
        private IGenericRepository<ProjectDetails> _projectDetailRepository;
        private IGenericRepository<ApproveProjectDetails> _approveProjectDetailsRepository;
        private IGenericRepository<WinProjectDetails> _winroveProjectDetailsRepository;

        private IGenericRepository<JobHiringDetails> _jobhiringdetailsRepository;
        private IGenericRepository<ProfessionDetails> _professiondetailsRepository;
        private IGenericRepository<CountryDetails> _countrydetailsRepository;
        private IGenericRepository<ProvinceDetails> _provincedetailsRepository;
        private IGenericRepository<NationalityDetails> _nationalitydetailsRepository;
        private IGenericRepository<QualificationDetails> _qualificationdetailsRepository;
        private IGenericRepository<InterviewRoundTypeMaster> _interviewroundtypemasterRepository;
        private IGenericRepository<InterviewScheduleDetails> _interviewscheduledetailsRepository;
        private IGenericRepository<InterviewFeedbackDetails> _interviewfeedbackdetailsRepository;
        private IGenericRepository<EmployeeSalaryDetails> _employeesalarydetailsRepository;
        private IGenericRepository<LeaveReasonDetail> _leavereasondetailRepository;
        private IGenericRepository<AssignLeaveToEmployee> _assignleavetoemployeeRepository;
        private IGenericRepository<FinancialYearDetail> _financialyeardetailRepository;
        private IGenericRepository<TaskMaster> _taskmasterRepository;
        private IGenericRepository<ActivityMaster> _activitymasterRepository;
        private IGenericRepository<ProjectBudgetLine> _projectbudgetlineRepository;
        private IGenericRepository<EmployeeType> _employeetypeRepository;
        private IGenericRepository<AssignActivity> _assignactivityRepository;
        private IGenericRepository<AssignActivityApproveBy> _assignactivityapprovebyRepository;
        private IGenericRepository<AssignActivityFeedback> _assignactivityfeedbackRepository;
        private IGenericRepository<EmployeeDocumentDetail> _employeedocumentdetailReposiotry;
        private IGenericRepository<EmployeeHistoryDetail> _employeehistorydetailReposiotry;
        private IGenericRepository<EmployeeProfessionalDetail> _employeeprofessionaldetailRepository;
        private IGenericRepository<PayrollMonthlyHourDetail> _payrollmonthlyhourdetailRepository;
        private IGenericRepository<EmployeeAttendance> _employeeattendanceRepository;
        private IGenericRepository<EmployeeHealthDetail> _employeehealthdetailRepository;
        private IGenericRepository<EmployeeApplyLeave> _employeeapplyleaveRepository;
        //private IGenericRepository<ProjectDocument> _projectdocumentRepository;
        private IGenericRepository<NotesMaster> _notesmasterRepository;
        private IGenericRepository<JobGrade> _jobgradeRepository;
        private IGenericRepository<HolidayDetails> _holidaydetailsRepository;
        private IGenericRepository<HolidayWeeklyDetails> _holidayweeklydetailRepository;
        private IGenericRepository<SalaryHeadDetails> _salaryheaddetailsRepository;
        private IGenericRepository<EmployeePayroll> _employeepayrollRepository;
        private IGenericRepository<EmployeePaymentTypes> _employeePaymentTypeRepository;
        private IGenericRepository<EmployeeMonthlyPayroll> _employeeMonthlyPayrollRepository;
        //private IGenericRepository<BudgetLineEmployees> _budgetLineEmployeesRepository;
        private IGenericRepository<EmployeePensionRate> _employeePensionRateRepository;
        private IGenericRepository<EmployeeContractType> _employeeContractTypeRepository;
        private IGenericRepository<ContractTypeContent> _contractTypeContentRepository;
        private IGenericRepository<AppraisalGeneralQuestions> _appraisalGeneralQuestionsRepository;
        private IGenericRepository<EmployeeAppraisalDetails> _employeeAppraisalDetailsRepository;
        private IGenericRepository<EmployeeAppraisalQuestions> _employeeAppraisalQuestionsRepository;
        private IGenericRepository<EmployeeEvaluation> _employeeEvaluationRepository;
        private IGenericRepository<InterviewTechnicalQuestions> _interviewTechnicalQuestionsRepository;
        private IGenericRepository<Advances> _advancesRepository;
        private IGenericRepository<InterviewDetails> _interviewDetailsRepository;
        private IGenericRepository<InterviewLanguages> _interviewLanguagesRepository;
        private IGenericRepository<InterviewTechnicalQuestion> _interviewTechnicalQuestionRepository;
        private IGenericRepository<InterviewTrainings> _interviewTrainingsRepository;
        private IGenericRepository<TechnicalQuestion> _technicalQuestionRepository;
        private IGenericRepository<ExistInterviewDetails> _existInterviewDetailsRepository;
        private IGenericRepository<UserDetailOffices> _userOfficesRepository;
        private IGenericRepository<StrongandWeakPoints> _strongandWeakPointsRepository;
        private IGenericRepository<EmployeeEvaluationTraining> _employeeEvaluationTrainingRepository;
        private IGenericRepository<LoggerDetails> _loggerDetailsRepository;
        private IGenericRepository<EmployeeAppraisalTeamMember> _employeeAppraisalTeamMemberRepository;
        private IGenericRepository<RatingBasedCriteria> _ratingBasedCriteriaRepository;
        private IGenericRepository<CategoryPopulator> _categoryPopulatorRepository;
        private IGenericRepository<EmployeePayrollForMonth> _employeePayrollForMonthRepository;

        // Store repos
        private IGenericRepository<StoreInventory> _storeInventoryRepository;
        private IGenericRepository<StoreInventoryItem> _storeInventoryItemRepository;
        private IGenericRepository<StoreItemPurchase> _storeItemPurchaseRepository;
        private IGenericRepository<ItemPurchaseDocument> _itemPurchaseDocumentRepository;
        private IGenericRepository<PurchaseVehicle> _purchaseVehicleRepository;
        private IGenericRepository<StorePurchaseOrder> _purchaseOrderRepository;
        private IGenericRepository<PurchaseOrderDocument> _purchaseOrderDocumentRepository;
        private IGenericRepository<MotorFuel> _storeFuelRepository;
        private IGenericRepository<VehicleLocation> _vehicleLocationRepository;
        private IGenericRepository<VehicleMileage> _vehicleMileageRepository;
        private IGenericRepository<PurchaseGenerator> _purchaseGeneratorRepository;
        private IGenericRepository<MotorMaintenance> _motorMaintenanceRepository;
        private IGenericRepository<MotorSparePart> _motorSparePartsRepository;
        private IGenericRepository<InventoryItemType> _inventoryItemTypeRepository;
        private IGenericRepository<PurchaseUnitType> _purchaseUnitTypeRepository;
        private IGenericRepository<EmployeePayrollMonth> _employeePayrollMonthRepository;

        private IGenericRepository<EmployeeContract> _employeeContractRepository;

        private IGenericRepository<SalaryTaxReportContent> _salaryTaxReportContentRepository;
        private IGenericRepository<ItemSpecificationMaster> _itemSpecificationMasterRepository;

        private IGenericRepository<ItemSpecificationDetails> _itemSpecificationDetailsRepository;
        private IGenericRepository<StatusAtTimeOfIssue> _statusAtTimeOfIssueRepository;
        private IGenericRepository<ReceiptType> _receiptTypeRepository;

        private IGenericRepository<EmployeeHistoryOutsideOrganization> _employeeHistoryOutsideOrganizationRepository;
        private IGenericRepository<EmployeeHistoryOutsideCountry> _employeeHistoryOutsideCountryRepository;
        private IGenericRepository<EmployeeRelativeInfo> _employeeRelativeInfoRepository;
        private IGenericRepository<EmployeeInfoReferences> _employeeInfoReferencesRepository;
        private IGenericRepository<EmployeeOtherSkills> _employeeOtherSkillsRepository;
        private IGenericRepository<EmployeeSalaryBudget> _employeeSalaryBudgetRepository;
        private IGenericRepository<EmployeeEducations> _employeeEducationsRepository;
        private IGenericRepository<EmployeeSalaryAnalyticalInfo> _employeeSalaryAnalyticalInfoRepository;

        private IGenericRepository<EmployeeHealthInfo> _employeeHealthInfoRepository;
        private IGenericRepository<EmployeeHealthQuestion> _employeeHealthQuestionRepository;
        private IGenericRepository<EmployeeMonthlyAttendance> _employeeMonthlyAttendanceRepository;
        private IGenericRepository<PensionPaymentHistory> _pensionPaymentHistoryRepository;
        private IGenericRepository<PayrollAccountHead> _payrollAccountHeadRepository;
        private IGenericRepository<EmployeePayrollAccountHead> _employeePayrollAccountHeadRepository;
        private IGenericRepository<DonorDetail> _donorDetailRepository;
        private IGenericRepository<SectorDetails> _sectorDetailsRepository;
        private IGenericRepository<ProgramDetail> _programDetailRepository;
        private IGenericRepository<AreaDetail> _areaDetailRepository;
        private IGenericRepository<DistrictDetail> _districtDetailRepository;
        private IGenericRepository<StrengthConsiderationDetail> _strengthConsiderationRepository;
        private IGenericRepository<GenderConsiderationDetail> _genderConsiderationRepository;
        private IGenericRepository<SecurityDetail> _securityDetailRepository;
        private IGenericRepository<SecurityConsiderationDetail> _securityConsiderationDetailRepository;
        private IGenericRepository<ProjectDetail> _projectDetailNewRepository;
        private IGenericRepository<ProjectPhaseDetails> _projectPhaseDetailsRepository;
        private IGenericRepository<ProjectOtherDetail> _projectOtherDetailRepository;
        
        private IGenericRepository<ProjectAssignTo> _projectAssignToRepository;
        private IGenericRepository<ProjectProgram> _projectProgramRepository;
        private IGenericRepository<ProjectArea> _projectAreaRepository;
        private IGenericRepository<ProjectSector> _projectSectorRepository;
        private IGenericRepository<EmployeeSalaryPaymentHistory> _employeeSalaryPaymentHistoryRepository;
        private IGenericRepository<EmployeeLanguages> _employeeLanguagesRepository;
        private IGenericRepository<ExchangeRateDetail> _exchangeRateDetaileRepository;
        private IGenericRepository<ProjectPhaseTime> _projectPhaseTimeRepository;
        private IGenericRepository<ProjectCommunication> _projectCommunicationRepository;
        private IGenericRepository<ProjectCommunicationAttachment> _projectCommunicationAttachmentRepository;
        private IGenericRepository<ProjectProposalDetail> _projectProposalDetailRepository;
        private IGenericRepository<DonorCriteriaDetails> _donorCriteriaDetailsRepository;
        private IGenericRepository<PurposeofInitiativeCriteria> _purposeofInitiativeCriteriaRepository;
        private IGenericRepository<EligibilityCriteriaDetail> _eligibilityCriteriaDetailRepository;
        private IGenericRepository<FeasibilityCriteriaDetail> _feasibilityCriteriaDetailRepository;
        private IGenericRepository<PriorityCriteriaDetail> _priorityCriteriaDetailRepository;
        private IGenericRepository<FinancialCriteriaDetail> _financialCriteriaDetailRepository;
        private IGenericRepository<RiskCriteriaDetail> _riskCriteriaDetailRepository;
        private IGenericRepository<TargetBeneficiaryDetail> _targetBeneficiaryDetailRepository;
        private IGenericRepository<FinancialProjectDetail> _financialProjectDetailRepository;
        private IGenericRepository<PriorityOtherDetail> _priorityOtherDetailRepository;
        private IGenericRepository<CEFeasibilityExpertOtherDetail> _feasibilit5yexpertDetailRepository;
        private IGenericRepository<CEAgeGroupDetail> _ageGroupOtherDetailRepository;
        private IGenericRepository<CEOccupationDetail> _occupationOtherDetailRepository;
        private IGenericRepository<CEAssumptionDetail> _assumptionOtherDetailRepository;
        private IGenericRepository<DonorEligibilityCriteria> _donorEligibilityCriteria;

        private IGenericRepository<ProvinceMultiSelect> _provinceMultiSelectRepository;
        private IGenericRepository<DistrictMultiSelect> _districtMultiSelectRepository;
        private IGenericRepository<SecurityConsiderationMultiSelect> _securityConsiderationMultiSelectRepository;







        //Marketing
        private IGenericRepository<ActivityType> _activityTypeRepository;
        private IGenericRepository<ContractDetails> _contractDetailsRepository;
        private IGenericRepository<JobDetails> _jobDetailsRepository;
        private IGenericRepository<JobPhase> _jobPhaseRepository;
        private IGenericRepository<Producer> _producerRepository;
        private IGenericRepository<JobPriceDetails> _jobPriceDetailsRepository;
        private IGenericRepository<LanguageDetail> _languageRepository;
        private IGenericRepository<MediaCategory> _mediaCategoryRepository;
        private IGenericRepository<Medium> _mediumRepository;
        private IGenericRepository<Quality> _qualityRepository;
        private IGenericRepository<Nature> _natureRepository;
        private IGenericRepository<TimeCategory> _timeCategoryRepository;
        private IGenericRepository<ClientDetails> _ClientDetailsRepository;
        private IGenericRepository<UnitRate> _unitRateRepository;
        private IGenericRepository<Category> _categoryRepository;
        private IGenericRepository<PaymentTypes> _paymentTypesRepository;
        private IGenericRepository<PolicyDetail> _policyRepository;
        private IGenericRepository<ProjectBudgetLineDetail> _budgetLineDetailRepository;
        private IGenericRepository<ProjectJobDetail> _projectJobDetailRepository;

        #region "new Accounting"
        private IGenericRepository<AccountFilterType> _accountFilterTypeRepository;
        private IGenericRepository<ChartOfAccountNew> _chartOfAccountNewRepository;
        private IGenericRepository<GainLossSelectedAccounts> _gainLossSelectedAccountsRepositoryRepository;

        #endregion


        private IGenericRepository<Errorlog> _errorlogRepository { get; set; }


        public UnitOfWork(ApplicationDbContext mschaContext)
        {
            _mschaContext = mschaContext;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _mschaContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public IGenericRepository<GainLossSelectedAccounts> GainLossSelectedAccountsRepository
        {
            get
            {
                return _gainLossSelectedAccountsRepositoryRepository =
                    _gainLossSelectedAccountsRepositoryRepository ?? new GenericRepository<GainLossSelectedAccounts>(_mschaContext);
            }
        }

        public IGenericRepository<ChartOfAccountNew> ChartOfAccountNewRepository
        {
            get
            {
                return _chartOfAccountNewRepository =
                    _chartOfAccountNewRepository ?? new GenericRepository<ChartOfAccountNew>(_mschaContext);
            }
        }

        public IGenericRepository<AccountFilterType> AccountFilterTypeRepository
        {
            get
            {
                return _accountFilterTypeRepository =
                    _accountFilterTypeRepository ?? new GenericRepository<AccountFilterType>(_mschaContext);
            }
        }


        public IGenericRepository<ProjectDetail> ProjectDetailNewRepository
        {
            get
            {
                return _projectDetailNewRepository =
                    _projectDetailNewRepository ?? new GenericRepository<ProjectDetail>(_mschaContext);
            }
        }


        public IGenericRepository<EmployeeHealthQuestion> EmployeeHealthQuestionRepository
        {
            get
            {
                return _employeeHealthQuestionRepository =
                    _employeeHealthQuestionRepository ?? new GenericRepository<EmployeeHealthQuestion>(_mschaContext);
            }
        }


        public IGenericRepository<EmployeeHealthInfo> EmployeeHealthInfoRepository
        {
            get
            {
                return _employeeHealthInfoRepository =
                    _employeeHealthInfoRepository ?? new GenericRepository<EmployeeHealthInfo>(_mschaContext);
            }
        }


        public IGenericRepository<EmployeeSalaryAnalyticalInfo> EmployeeSalaryAnalyticalInfoRepository
        {
            get
            {
                return _employeeSalaryAnalyticalInfoRepository =
                    _employeeSalaryAnalyticalInfoRepository ?? new GenericRepository<EmployeeSalaryAnalyticalInfo>(_mschaContext);
            }
        }

        public IGenericRepository<ReceiptType> ReceiptTypeRepository
        {
            get
            {
                return _receiptTypeRepository =
                    _receiptTypeRepository ?? new GenericRepository<ReceiptType>(_mschaContext);
            }
        }

        public IGenericRepository<StatusAtTimeOfIssue> StatusAtTimeOfIssueRepository
        {
            get
            {
                return _statusAtTimeOfIssueRepository =
                    _statusAtTimeOfIssueRepository ?? new GenericRepository<StatusAtTimeOfIssue>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeEducations> EmployeeEducationsRepository
        {
            get
            {
                return _employeeEducationsRepository =
                    _employeeEducationsRepository ?? new GenericRepository<EmployeeEducations>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeSalaryBudget> EmployeeSalaryBudgetRepository
        {
            get
            {
                return _employeeSalaryBudgetRepository =
                    _employeeSalaryBudgetRepository ?? new GenericRepository<EmployeeSalaryBudget>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeOtherSkills> EmployeeOtherSkillsRepository
        {
            get
            {
                return _employeeOtherSkillsRepository =
                    _employeeOtherSkillsRepository ?? new GenericRepository<EmployeeOtherSkills>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeInfoReferences> EmployeeInfoReferencesRepository
        {
            get
            {
                return _employeeInfoReferencesRepository =
                    _employeeInfoReferencesRepository ?? new GenericRepository<EmployeeInfoReferences>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeRelativeInfo> EmployeeRelativeInfoRepository
        {
            get
            {
                return _employeeRelativeInfoRepository =
                    _employeeRelativeInfoRepository ?? new GenericRepository<EmployeeRelativeInfo>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeHistoryOutsideCountry> EmployeeHistoryOutsideCountryRepository
        {
            get
            {
                return _employeeHistoryOutsideCountryRepository =
                    _employeeHistoryOutsideCountryRepository ?? new GenericRepository<EmployeeHistoryOutsideCountry>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeHistoryOutsideOrganization> EmployeeHistoryOutsideOrganizationRepository
        {
            get
            {
                return _employeeHistoryOutsideOrganizationRepository =
                    _employeeHistoryOutsideOrganizationRepository ?? new GenericRepository<EmployeeHistoryOutsideOrganization>(_mschaContext);
            }
        }

        public IGenericRepository<ItemSpecificationMaster> ItemSpecificationMasterRepository
        {
            get
            {
                return _itemSpecificationMasterRepository =
                    _itemSpecificationMasterRepository ?? new GenericRepository<ItemSpecificationMaster>(_mschaContext);
            }
        }

        public IGenericRepository<ItemSpecificationDetails> ItemSpecificationDetailsRepository
        {
            get
            {
                return _itemSpecificationDetailsRepository =
                    _itemSpecificationDetailsRepository ?? new GenericRepository<ItemSpecificationDetails>(_mschaContext);
            }
        }

        public IGenericRepository<SalaryTaxReportContent> SalaryTaxReportContentRepository
        {
            get
            {
                return _salaryTaxReportContentRepository =
                    _salaryTaxReportContentRepository ?? new GenericRepository<SalaryTaxReportContent>(_mschaContext);
            }
        }
        public IGenericRepository<EmployeeContract> EmployeeContractRepository
        {
            get
            {
                return _employeeContractRepository =
                    _employeeContractRepository ?? new GenericRepository<EmployeeContract>(_mschaContext);
            }
        }
        public IGenericRepository<EmployeePayrollMonth> EmployeePayrollMonthRepository
        {
            get
            {
                return _employeePayrollMonthRepository =
                    _employeePayrollMonthRepository ?? new GenericRepository<EmployeePayrollMonth>(_mschaContext);
            }
        }
        public IGenericRepository<EmployeePayrollForMonth> EmployeePayrollForMonthRepository
        {
            get
            {
                return _employeePayrollForMonthRepository =
                    _employeePayrollForMonthRepository ?? new GenericRepository<EmployeePayrollForMonth>(_mschaContext);
            }
        }

        // Store
        public IGenericRepository<PurchaseUnitType> PurchaseUnitTypeRepository
        {
            get
            {
                return _purchaseUnitTypeRepository =
                    _purchaseUnitTypeRepository ?? new GenericRepository<PurchaseUnitType>(_mschaContext);
            }
        }
        public IGenericRepository<InventoryItemType> InventoryItemTypeRepository
        {
            get
            {
                return _inventoryItemTypeRepository =
                    _inventoryItemTypeRepository ?? new GenericRepository<InventoryItemType>(_mschaContext);
            }
        }

        public IGenericRepository<StoreInventory> StoreInventoryRepository
        {
            get
            {
                return _storeInventoryRepository =
                    _storeInventoryRepository ?? new GenericRepository<StoreInventory>(_mschaContext);
            }
        }

        public IGenericRepository<StoreInventoryItem> StoreInventoryItemRepository
        {
            get
            {
                return _storeInventoryItemRepository =
                    _storeInventoryItemRepository ?? new GenericRepository<StoreInventoryItem>(_mschaContext);
            }
        }

        public IGenericRepository<StoreItemPurchase> StoreItemPurchaseRepository
        {
            get
            {
                return _storeItemPurchaseRepository =
                    _storeItemPurchaseRepository ?? new GenericRepository<StoreItemPurchase>(_mschaContext);
            }
        }

        public IGenericRepository<ItemPurchaseDocument> ItemPurchaseDocumentRepository
        {
            get
            {
                return _itemPurchaseDocumentRepository = _itemPurchaseDocumentRepository ??
                                                         new GenericRepository<ItemPurchaseDocument>(_mschaContext);
            }
        }

        public IGenericRepository<PurchaseVehicle> PurchaseVehicleRepository
        {
            get
            {
                return _purchaseVehicleRepository =
                    _purchaseVehicleRepository ?? new GenericRepository<PurchaseVehicle>(_mschaContext);
            }
        }

        public IGenericRepository<StorePurchaseOrder> PurchaseOrderRepository
        {
            get
            {
                return _purchaseOrderRepository =
                    _purchaseOrderRepository ?? new GenericRepository<StorePurchaseOrder>(_mschaContext);
            }
        }

        public IGenericRepository<PurchaseOrderDocument> PurchaseOrderDocumentRepository
        {
            get
            {
                return _purchaseOrderDocumentRepository = _purchaseOrderDocumentRepository ??
                                                          new GenericRepository<PurchaseOrderDocument>(_mschaContext);
            }
        }

        public IGenericRepository<MotorFuel> StoreFuelRepository
        {
            get
            {
                return _storeFuelRepository = _storeFuelRepository ?? new GenericRepository<MotorFuel>(_mschaContext);
            }
        }

        public IGenericRepository<VehicleLocation> VehicleLocationRepository
        {
            get
            {
                return _vehicleLocationRepository =
                    _vehicleLocationRepository ?? new GenericRepository<VehicleLocation>(_mschaContext);
            }
        }

        public IGenericRepository<VehicleMileage> VehicleMileageRepository
        {
            get
            {
                return _vehicleMileageRepository =
                    _vehicleMileageRepository ?? new GenericRepository<VehicleMileage>(_mschaContext);
            }
        }

        public IGenericRepository<PurchaseGenerator> PurchaseGeneratorRepository
        {
            get
            {
                return _purchaseGeneratorRepository =
                    _purchaseGeneratorRepository ?? new GenericRepository<PurchaseGenerator>(_mschaContext);
            }
        }

        public IGenericRepository<MotorMaintenance> MotorMaintenanceRepository
        {
            get
            {
                return _motorMaintenanceRepository =
                    _motorMaintenanceRepository ?? new GenericRepository<MotorMaintenance>(_mschaContext);
            }
        }

        public IGenericRepository<MotorSparePart> MotorSparePartsRepository
        {
            get
            {
                return _motorSparePartsRepository =
                    _motorSparePartsRepository ?? new GenericRepository<MotorSparePart>(_mschaContext);
            }
        }
        //

        public IGenericRepository<CategoryPopulator> CategoryPopulatorRepository
        {
            get
            {
                return _categoryPopulatorRepository = _categoryPopulatorRepository ?? new GenericRepository<CategoryPopulator>(_mschaContext);
            }
        }
        public IGenericRepository<RatingBasedCriteria> RatingBasedCriteriaRepository
        {
            get
            {
                return _ratingBasedCriteriaRepository = _ratingBasedCriteriaRepository ?? new GenericRepository<RatingBasedCriteria>(_mschaContext);
            }
        }
        public IGenericRepository<EmployeeAppraisalTeamMember> EmployeeAppraisalTeamMemberRepository
        {
            get
            {
                return _employeeAppraisalTeamMemberRepository = _employeeAppraisalTeamMemberRepository ?? new GenericRepository<EmployeeAppraisalTeamMember>(_mschaContext);
            }
        }
        public IGenericRepository<EmployeeEvaluationTraining> EmployeeEvaluationTrainingRepository
        {
            get
            {
                return _employeeEvaluationTrainingRepository = _employeeEvaluationTrainingRepository ?? new GenericRepository<EmployeeEvaluationTraining>(_mschaContext);
            }
        }

        public IGenericRepository<LoggerDetails> LoggerDetailsRepository
        {
            get
            {
                return _loggerDetailsRepository = _loggerDetailsRepository ?? new GenericRepository<LoggerDetails>(_mschaContext);
            }
        }

        public IGenericRepository<StrongandWeakPoints> StrongandWeakPointsRepository
        {
            get
            {
                return _strongandWeakPointsRepository = _strongandWeakPointsRepository ?? new GenericRepository<StrongandWeakPoints>(_mschaContext);
            }
        }

        public IGenericRepository<UserDetailOffices> UserOfficesRepository
        {
            get
            {
                return _userOfficesRepository = _userOfficesRepository ?? new GenericRepository<UserDetailOffices>(_mschaContext);
            }
        }

        public IGenericRepository<ExistInterviewDetails> ExistInterviewDetailsRepository
        {
            get
            {
                return _existInterviewDetailsRepository = _existInterviewDetailsRepository ?? new GenericRepository<ExistInterviewDetails>(_mschaContext);
            }
        }

        public IGenericRepository<InterviewDetails> InterviewDetailsRepository
        {
            get
            {
                return _interviewDetailsRepository = _interviewDetailsRepository ?? new GenericRepository<InterviewDetails>(_mschaContext);
            }
        }
        public IGenericRepository<InterviewLanguages> InterviewLanguagesRepository
        {
            get
            {
                return _interviewLanguagesRepository = _interviewLanguagesRepository ?? new GenericRepository<InterviewLanguages>(_mschaContext);
            }
        }
        public IGenericRepository<InterviewTechnicalQuestion> InterviewTechnicalQuestionRepository
        {
            get
            {
                return _interviewTechnicalQuestionRepository = _interviewTechnicalQuestionRepository ?? new GenericRepository<InterviewTechnicalQuestion>(_mschaContext);
            }
        }
        public IGenericRepository<InterviewTrainings> InterviewTrainingsRepository
        {
            get
            {
                return _interviewTrainingsRepository = _interviewTrainingsRepository ?? new GenericRepository<InterviewTrainings>(_mschaContext);
            }
        }
        public IGenericRepository<TechnicalQuestion> TechnicalQuestionRepository
        {
            get
            {
                return _technicalQuestionRepository = _technicalQuestionRepository ?? new GenericRepository<TechnicalQuestion>(_mschaContext);
            }
        }
        public IGenericRepository<Advances> AdvancesRepository
        {
            get
            {
                return _advancesRepository = _advancesRepository ?? new GenericRepository<Advances>(_mschaContext);
            }
        }
        public IGenericRepository<InterviewTechnicalQuestions> InterviewTechnicalQuestionsRepository
        {
            get
            {
                return _interviewTechnicalQuestionsRepository = _interviewTechnicalQuestionsRepository ?? new GenericRepository<InterviewTechnicalQuestions>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeEvaluation> EmployeeEvaluationRepository
        {
            get
            {
                return _employeeEvaluationRepository = _employeeEvaluationRepository ?? new GenericRepository<EmployeeEvaluation>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeAppraisalQuestions> EmployeeAppraisalQuestionsRepository
        {
            get
            {
                return _employeeAppraisalQuestionsRepository = _employeeAppraisalQuestionsRepository ?? new GenericRepository<EmployeeAppraisalQuestions>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeAppraisalDetails> EmployeeAppraisalDetailsRepository
        {
            get
            {
                return _employeeAppraisalDetailsRepository = _employeeAppraisalDetailsRepository ?? new GenericRepository<EmployeeAppraisalDetails>(_mschaContext);
            }
        }

        public IGenericRepository<AppraisalGeneralQuestions> AppraisalGeneralQuestionsRepository
        {
            get
            {
                return _appraisalGeneralQuestionsRepository = _appraisalGeneralQuestionsRepository ?? new GenericRepository<AppraisalGeneralQuestions>(_mschaContext);
            }
        }

        public IGenericRepository<ContractTypeContent> ContractTypeContentRepository
        {
            get
            {
                return _contractTypeContentRepository = _contractTypeContentRepository ?? new GenericRepository<ContractTypeContent>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeContractType> EmployeeContractTypeRepository
        {
            get
            {
                return _employeeContractTypeRepository = _employeeContractTypeRepository ?? new GenericRepository<EmployeeContractType>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeePensionRate> EmployeePensionRateRepository
        {
            get
            {
                return _employeePensionRateRepository = _employeePensionRateRepository ?? new GenericRepository<EmployeePensionRate>(_mschaContext);
            }
        }
        //public IGenericRepository<BudgetLineEmployees> BudgetLineEmployeesRepository
        //{
        //    get
        //    {
        //        return _budgetLineEmployeesRepository = _budgetLineEmployeesRepository ?? new GenericRepository<BudgetLineEmployees>(_mschaContext);
        //    }
        //}
        public IGenericRepository<EmployeeMonthlyPayroll> EmployeeMonthlyPayrollRepository
        {
            get
            {
                return _employeeMonthlyPayrollRepository = _employeeMonthlyPayrollRepository ?? new GenericRepository<EmployeeMonthlyPayroll>(_mschaContext);
            }
        }
        public IGenericRepository<EmployeePaymentTypes> EmployeePaymentTypeRepository
        {
            get
            {
                return _employeePaymentTypeRepository = _employeePaymentTypeRepository ?? new GenericRepository<EmployeePaymentTypes>(_mschaContext);
            }
        }
        public IGenericRepository<ProjectDetails> ProjectDetailRepository
        {
            get
            {
                return _projectDetailRepository = _projectDetailRepository ?? new GenericRepository<ProjectDetails>(_mschaContext);
            }
        }

        //public IGenericRepository<ProjectBudget> ProjectBudgetRepository
        //{
        //    get
        //    {
        //        return _projectBudgetRepository = _projectBudgetRepository ?? new GenericRepository<ProjectBudget>(_mschaContext);
        //    }
        //}
        public IGenericRepository<OfficeDetail> OfficeDetailRepository
        {
            get
            {
                return _officedetailsRepository = _officedetailsRepository ?? new GenericRepository<OfficeDetail>(_mschaContext);
            }
        }
        public IGenericRepository<Permissions> PermissionRepository
        {
            get
            {
                return _permissinsRepository = _permissinsRepository ?? new GenericRepository<Permissions>(_mschaContext);
            }
        }
        public IGenericRepository<AccountNoteDetail> AccountNoteDetailRepository
        {
            get
            {
                return _accountNoteDetail = _accountNoteDetail ?? new GenericRepository<AccountNoteDetail>(_mschaContext);
            }
        }
        public IGenericRepository<PolicyDetail> PolicyRepository
        {
            get
            {
                return _policyRepository = _policyRepository ?? new GenericRepository<PolicyDetail>(_mschaContext);
            }
        }

        public IGenericRepository<UserDetails> UserDetailsRepository
        {
            get
            {
                return _userdetailsRepository = _userdetailsRepository ?? new GenericRepository<UserDetails>(_mschaContext);
            }
        }

        public IGenericRepository<Department> DepartmentRepository
        {
            get
            {
                return _deparmentRepository = _deparmentRepository ?? new GenericRepository<Department>(_mschaContext);
            }
        }

        public IGenericRepository<PermissionsInRoles> PermissionsInRolesRepository
        {
            get
            {
                return _permissionInRolesRepository = _permissionInRolesRepository ?? new GenericRepository<PermissionsInRoles>(_mschaContext);
            }
        }

        public IGenericRepository<CurrencyDetails> CurrencyDetailsRepository
        {
            get
            {
                return _currencydetailsRepository = _currencydetailsRepository ?? new GenericRepository<CurrencyDetails>(_mschaContext);
            }
        }

        public IGenericRepository<JournalDetail> JournalDetailRepository
        {
            get
            {
                return _journalDetailRepository = _journalDetailRepository ?? new GenericRepository<JournalDetail>(_mschaContext);
            }
        }
        public IGenericRepository<EmailType> EmailTypeRepository
        {
            get
            {
                return _emailtypeRepository = _emailtypeRepository ?? new GenericRepository<EmailType>(_mschaContext);
            }
        }

        public IGenericRepository<EmailSettingDetail> EmailSettingDetailRepository
        {
            get
            {
                return _emailsettingdetailRepository = _emailsettingdetailRepository ?? new GenericRepository<EmailSettingDetail>(_mschaContext);
            }
        }

        public IGenericRepository<AccountType> AccountTypeRepository
        {
            get
            {
                return _accounttypeRepository = _accounttypeRepository ?? new GenericRepository<AccountType>(_mschaContext);
            }
        }

        public IGenericRepository<AccountLevel> AccountLevelRepository
        {
            get
            {
                return _accountlevelRepository = _accountlevelRepository ?? new GenericRepository<AccountLevel>(_mschaContext);
            }
        }

        //public IGenericRepository<ChartAccountDetail> ChartAccountDetailRepository
        //{
        //    get
        //    {
        //        return _chartaccountdetailRepository = _chartaccountdetailRepository ?? new GenericRepository<ChartAccountDetail>(_mschaContext);
        //    }
        //}

        public IGenericRepository<VoucherType> VoucherTypeRepository
        {
            get
            {
                return _vouchertypeRepository = _vouchertypeRepository ?? new GenericRepository<VoucherType>(_mschaContext);
            }
        }

        public IGenericRepository<VoucherDetail> VoucherDetailRepository
        {
            get
            {
                return _voucherdetailsRepository = _voucherdetailsRepository ?? new GenericRepository<VoucherDetail>(_mschaContext);
            }
        }

        public IGenericRepository<VoucherDocumentDetail> VoucherDocumentDetailRepository
        {
            get
            {
                return _voucherdocumentdetailRepository = _voucherdocumentdetailRepository ?? new GenericRepository<VoucherDocumentDetail>(_mschaContext);
            }
        }

        //public IGenericRepository<VoucherTransactionDetails> VoucherTransactionDetailsRepository
        //{
        //    get
        //    {
        //        return _vouchertransactiondetailsRepository = _vouchertransactiondetailsRepository ?? new GenericRepository<VoucherTransactionDetails>(_mschaContext);
        //    }
        //}

        public IGenericRepository<VoucherTransactions> VoucherTransactionsRepository
        {
            get
            {
                return _voucherTransactionsRepository = _voucherTransactionsRepository ?? new GenericRepository<VoucherTransactions>(_mschaContext);
            }
        }

        public IGenericRepository<AnalyticalType> AnalyticalTypeRepository
        {
            get
            {
                return _analyticaltypeRepository = _analyticaltypeRepository ?? new GenericRepository<AnalyticalType>(_mschaContext);
            }
        }

        public IGenericRepository<AnalyticalDetail> AnalyticalDetailRepository
        {
            get
            {
                return _analyticaldetailRepository = _analyticaldetailRepository ?? new GenericRepository<AnalyticalDetail>(_mschaContext);
            }
        }

        public IGenericRepository<ExchangeRate> ExchangeRateRepository
        {
            get
            {
                return _exchangerateRepository = _exchangerateRepository ?? new GenericRepository<ExchangeRate>(_mschaContext);
            }
        }

        public IGenericRepository<StoreSourceCodeDetail> StoreSourceCodeRepository
        {
            get
            {
                return _storesourcecodeRepository = _storesourcecodeRepository ?? new GenericRepository<StoreSourceCodeDetail>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeDetail> EmployeeDetailRepository
        {
            get
            {
                return _employeedetailRepository = _employeedetailRepository ?? new GenericRepository<EmployeeDetail>(_mschaContext);
            }
        }

        public IGenericRepository<DesignationDetail> DesignationDetailRepository
        {
            get
            {
                return _designationdetailRepository = _designationdetailRepository ?? new GenericRepository<DesignationDetail>(_mschaContext);
            }
        }

        public IGenericRepository<JobHiringDetails> JobHiringDetailsRepository
        {
            get
            {
                return _jobhiringdetailsRepository = _jobhiringdetailsRepository ?? new GenericRepository<JobHiringDetails>(_mschaContext);
            }
        }

        public IGenericRepository<ProfessionDetails> ProfessionDetailsRepository
        {
            get
            {
                return _professiondetailsRepository = _professiondetailsRepository ?? new GenericRepository<ProfessionDetails>(_mschaContext);
            }
        }

        public IGenericRepository<CountryDetails> CountryDetailsRepository
        {
            get
            {
                return _countrydetailsRepository = _countrydetailsRepository ?? new GenericRepository<CountryDetails>(_mschaContext);
            }
        }

        public IGenericRepository<ProvinceDetails> ProvinceDetailsRepository
        {
            get
            {
                return _provincedetailsRepository = _provincedetailsRepository ?? new GenericRepository<ProvinceDetails>(_mschaContext);
            }
        }

        public IGenericRepository<NationalityDetails> NationalityDetailsRepository
        {
            get
            {
                return _nationalitydetailsRepository = _nationalitydetailsRepository ?? new GenericRepository<NationalityDetails>(_mschaContext);
            }
        }

        public IGenericRepository<QualificationDetails> QualificationDetailsRepository
        {
            get
            {
                return _qualificationdetailsRepository = _qualificationdetailsRepository ?? new GenericRepository<QualificationDetails>(_mschaContext);
            }
        }

        public IGenericRepository<InterviewRoundTypeMaster> InterviewRoundTypeMasterRepository
        {
            get
            {
                return _interviewroundtypemasterRepository = _interviewroundtypemasterRepository ?? new GenericRepository<InterviewRoundTypeMaster>(_mschaContext);
            }
        }

        public IGenericRepository<InterviewScheduleDetails> InterviewScheduleDetailsRepository
        {
            get
            {
                return _interviewscheduledetailsRepository = _interviewscheduledetailsRepository ?? new GenericRepository<InterviewScheduleDetails>(_mschaContext);
            }
        }

        public IGenericRepository<InterviewFeedbackDetails> InterviewFeedbackDetailsRepository
        {
            get
            {
                return _interviewfeedbackdetailsRepository = _interviewfeedbackdetailsRepository ?? new GenericRepository<InterviewFeedbackDetails>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeSalaryDetails> EmployeeSalaryDetailsRepository
        {
            get
            {
                return _employeesalarydetailsRepository = _employeesalarydetailsRepository ?? new GenericRepository<EmployeeSalaryDetails>(_mschaContext);
            }
        }

        public IGenericRepository<LeaveReasonDetail> LeaveReasonDetailRepository
        {
            get
            {
                return _leavereasondetailRepository = _leavereasondetailRepository ?? new GenericRepository<LeaveReasonDetail>(_mschaContext);
            }
        }

        public IGenericRepository<AssignLeaveToEmployee> AssignLeaveToEmployeeRepository
        {
            get
            {
                return _assignleavetoemployeeRepository = _assignleavetoemployeeRepository ?? new GenericRepository<AssignLeaveToEmployee>(_mschaContext);
            }
        }

        public IGenericRepository<FinancialYearDetail> FinancialYearDetailRepository
        {
            get
            {
                return _financialyeardetailRepository = _financialyeardetailRepository ?? new GenericRepository<FinancialYearDetail>(_mschaContext);
            }
        }

        public IGenericRepository<TaskMaster> TaskMasterRepository
        {
            get
            {
                return _taskmasterRepository = _taskmasterRepository ?? new GenericRepository<TaskMaster>(_mschaContext);
            }
        }

        public IGenericRepository<ActivityMaster> ActivityMasterRepository
        {
            get
            {
                return _activitymasterRepository = _activitymasterRepository ?? new GenericRepository<ActivityMaster>(_mschaContext);
            }
        }

        public IGenericRepository<ProjectBudgetLine> ProjectBudgetLineRepository
        {
            get
            {
                return _projectbudgetlineRepository = _projectbudgetlineRepository ?? new GenericRepository<ProjectBudgetLine>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeType> EmployeeTypeRepository
        {
            get
            {
                return _employeetypeRepository = _employeetypeRepository ?? new GenericRepository<EmployeeType>(_mschaContext);
            }
        }

        public IGenericRepository<AssignActivity> AssignActivityRepository
        {
            get
            {
                return _assignactivityRepository = _assignactivityRepository ?? new GenericRepository<AssignActivity>(_mschaContext);
            }
        }

        public IGenericRepository<AssignActivityApproveBy> AssignActivityApproveByRepository
        {
            get
            {
                return _assignactivityapprovebyRepository = _assignactivityapprovebyRepository ?? new GenericRepository<AssignActivityApproveBy>(_mschaContext);
            }
        }

        public IGenericRepository<AssignActivityFeedback> AssignActivityFeedbackRepository
        {
            get
            {
                return _assignactivityfeedbackRepository = _assignactivityfeedbackRepository ?? new GenericRepository<AssignActivityFeedback>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeDocumentDetail> EmployeeDocumentDetailRepository
        {
            get
            {
                return _employeedocumentdetailReposiotry = _employeedocumentdetailReposiotry ?? new GenericRepository<EmployeeDocumentDetail>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeHistoryDetail> EmployeeHistoryDetailRepository
        {
            get
            {
                return _employeehistorydetailReposiotry = _employeehistorydetailReposiotry ?? new GenericRepository<EmployeeHistoryDetail>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeProfessionalDetail> EmployeeProfessionalDetailRepository
        {
            get
            {
                return _employeeprofessionaldetailRepository = _employeeprofessionaldetailRepository ?? new GenericRepository<EmployeeProfessionalDetail>(_mschaContext);
            }
        }

        public IGenericRepository<PayrollMonthlyHourDetail> PayrollMonthlyHourDetailRepository
        {
            get
            {
                return _payrollmonthlyhourdetailRepository = _payrollmonthlyhourdetailRepository ?? new GenericRepository<PayrollMonthlyHourDetail>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeAttendance> EmployeeAttendanceRepository
        {
            get
            {
                return _employeeattendanceRepository = _employeeattendanceRepository ?? new GenericRepository<EmployeeAttendance>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeHealthDetail> EmployeeHealthDetailRepository
        {
            get
            {
                return _employeehealthdetailRepository = _employeehealthdetailRepository ?? new GenericRepository<EmployeeHealthDetail>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeApplyLeave> EmployeeApplyLeaveRepository
        {
            get
            {
                return _employeeapplyleaveRepository = _employeeapplyleaveRepository ?? new GenericRepository<EmployeeApplyLeave>(_mschaContext);
            }
        }

        //public IGenericRepository<ProjectDocument> ProjectDocumentRepository
        //{
        //    get
        //    {
        //        return _projectdocumentRepository = _projectdocumentRepository ?? new GenericRepository<ProjectDocument>(_mschaContext);
        //    }
        //}

        public IGenericRepository<NotesMaster> NotesMasterRepository
        {
            get
            {
                return _notesmasterRepository = _notesmasterRepository ?? new GenericRepository<NotesMaster>(_mschaContext);
            }
        }

        public IGenericRepository<JobGrade> JobGradeRepository
        {
            get
            {
                return _jobgradeRepository = _jobgradeRepository ?? new GenericRepository<JobGrade>(_mschaContext);
            }
        }

        public IGenericRepository<HolidayDetails> HolidayDetailsRepository
        {
            get
            {
                return _holidaydetailsRepository = _holidaydetailsRepository ?? new GenericRepository<HolidayDetails>(_mschaContext);
            }
        }

        public IGenericRepository<HolidayWeeklyDetails> HolidayWeeklyDetailRepository
        {
            get
            {
                return _holidayweeklydetailRepository = _holidayweeklydetailRepository ?? new GenericRepository<HolidayWeeklyDetails>(_mschaContext);
            }
        }

        public IGenericRepository<SalaryHeadDetails> SalaryHeadDetailsRepository
        {
            get
            {
                return _salaryheaddetailsRepository = _salaryheaddetailsRepository ?? new GenericRepository<SalaryHeadDetails>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeePayroll> EmployeePayrollRepository
        {
            get
            {
                return _employeepayrollRepository = _employeepayrollRepository ?? new GenericRepository<EmployeePayroll>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeMonthlyAttendance> EmployeeMonthlyAttendanceRepository
        {
            get
            {
                return _employeeMonthlyAttendanceRepository = _employeeMonthlyAttendanceRepository ?? new GenericRepository<EmployeeMonthlyAttendance>(_mschaContext);
            }
        }

        public IGenericRepository<PensionPaymentHistory> PensionPaymentHistoryRepository
        {
            get
            {
                return _pensionPaymentHistoryRepository = _pensionPaymentHistoryRepository ?? new GenericRepository<PensionPaymentHistory>(_mschaContext);
            }
        }

        public IGenericRepository<PayrollAccountHead> PayrollAccountHeadRepository
        {
            get
            {
                return _payrollAccountHeadRepository = _payrollAccountHeadRepository ?? new GenericRepository<PayrollAccountHead>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeePayrollAccountHead> EmployeePayrollAccountHeadRepository
        {
            get
            {
                return _employeePayrollAccountHeadRepository = _employeePayrollAccountHeadRepository ?? new GenericRepository<EmployeePayrollAccountHead>(_mschaContext);
            }
        }

        public IGenericRepository<DonorDetail> DonorDetailRepository
        {
            get
            {
                return _donorDetailRepository = _donorDetailRepository ?? new GenericRepository<DonorDetail>(_mschaContext);
            }
        }

        public IGenericRepository<SectorDetails> SectorDetailsRepository
        {
            get
            {
                return _sectorDetailsRepository = _sectorDetailsRepository ?? new GenericRepository<SectorDetails>(_mschaContext);
            }
        }
        public IGenericRepository<ProgramDetail> ProgramDetailRepository
        {
            get
            {
                return _programDetailRepository = _programDetailRepository ?? new GenericRepository<ProgramDetail>(_mschaContext);
            }
        }
        public IGenericRepository<AreaDetail> AreaDetailRepository
        {
            get
            {
                return _areaDetailRepository = _areaDetailRepository ?? new GenericRepository<AreaDetail>(_mschaContext);
            }
        }
        public IGenericRepository<DistrictDetail> DistrictDetailRepository
        {
            get
            {
                return _districtDetailRepository = _districtDetailRepository ?? new GenericRepository<DistrictDetail>(_mschaContext);
            }
        }
        public IGenericRepository<GenderConsiderationDetail> GenderConsiderationRepository
        {
            get
            {
                return _genderConsiderationRepository = _genderConsiderationRepository ?? new GenericRepository<GenderConsiderationDetail>(_mschaContext);
            }
        }
        public IGenericRepository<SecurityDetail> SecurityDetailRepository
        {
            get
            {
                return _securityDetailRepository = _securityDetailRepository ?? new GenericRepository<SecurityDetail>(_mschaContext);
            }
        }
        public IGenericRepository<SecurityConsiderationDetail> SecurityConsiderationDetailRepository
        {
            get
            {
                return _securityConsiderationDetailRepository = _securityConsiderationDetailRepository ?? new GenericRepository<SecurityConsiderationDetail>(_mschaContext);
            }
        }
        public IGenericRepository<StrengthConsiderationDetail> StrengthConsiderationRepository
        {
            get
            {
                return _strengthConsiderationRepository = _strengthConsiderationRepository ?? new GenericRepository<StrengthConsiderationDetail>(_mschaContext);
            }
        }
        public IGenericRepository<ProjectPhaseDetails> ProjectPhaseDetailsRepository
        {
            get
            {
                return _projectPhaseDetailsRepository = _projectPhaseDetailsRepository ?? new GenericRepository<ProjectPhaseDetails>(_mschaContext);
            }
        }
        public IGenericRepository<ProjectOtherDetail> ProjectOtherDetailRepository
        {
            get
            {
                return _projectOtherDetailRepository = _projectOtherDetailRepository ?? new GenericRepository<ProjectOtherDetail>(_mschaContext);
            }
        }


        
        public IGenericRepository<ProjectAssignTo> ProjectAssignToRepository
        {
            get
            {
                return _projectAssignToRepository = _projectAssignToRepository ?? new GenericRepository<ProjectAssignTo>(_mschaContext);
            }
        }
        public IGenericRepository<ProjectProgram> ProjectProgramRepository
        {
            get
            {
                return _projectProgramRepository = _projectProgramRepository ?? new GenericRepository<ProjectProgram>(_mschaContext);
            }
        }
        public IGenericRepository<ProjectArea> ProjectAreaRepository
        {
            get
            {
                return _projectAreaRepository = _projectAreaRepository ?? new GenericRepository<ProjectArea>(_mschaContext);
            }
        }
        public IGenericRepository<ProjectSector> ProjectSectorRepository
        {
            get
            {
                return _projectSectorRepository = _projectSectorRepository ?? new GenericRepository<ProjectSector>(_mschaContext);
            }
        }


        public IGenericRepository<EmployeeSalaryPaymentHistory> EmployeeSalaryPaymentHistoryRepository
        {
            get
            {
                return _employeeSalaryPaymentHistoryRepository = _employeeSalaryPaymentHistoryRepository ?? new GenericRepository<EmployeeSalaryPaymentHistory>(_mschaContext);
            }
        }

        public IGenericRepository<EmployeeLanguages> EmployeeLanguagesRepository
        {
            get
            {
                return _employeeLanguagesRepository = _employeeLanguagesRepository ?? new GenericRepository<EmployeeLanguages>(_mschaContext);
            }
        }

        public IGenericRepository<ActivityType> ActivityTypeRepository
        {
            get
            {
                return _activityTypeRepository = _activityTypeRepository ?? new GenericRepository<ActivityType>(_mschaContext);
            }
        }

        public IGenericRepository<ContractDetails> ContractDetailsRepository
        {
            get
            {
                return _contractDetailsRepository = _contractDetailsRepository ?? new GenericRepository<ContractDetails>(_mschaContext);
            }
        }

        public IGenericRepository<JobDetails> JobDetailsRepository
        {
            get
            {
                return _jobDetailsRepository = _jobDetailsRepository ?? new GenericRepository<JobDetails>(_mschaContext);
            }
        }

        public IGenericRepository<JobPhase> JobPhaseRepository
        {
            get
            {
                return _jobPhaseRepository = _jobPhaseRepository ?? new GenericRepository<JobPhase>(_mschaContext);
            }
        }
        
         public IGenericRepository<Producer> ProducerRepository
        {
            get
            {
                return _producerRepository = _producerRepository ?? new GenericRepository<Producer>(_mschaContext);
            }
        }

        public IGenericRepository<JobPriceDetails> JobPriceDetailsRepository
        {
            get
            {
                return _jobPriceDetailsRepository = _jobPriceDetailsRepository ?? new GenericRepository<JobPriceDetails>(_mschaContext);
            }
        }

        public IGenericRepository<LanguageDetail> LanguageRepository
        {
            get
            {
                return _languageRepository = _languageRepository ?? new GenericRepository<LanguageDetail>(_mschaContext);
            }
        }

        public IGenericRepository<MediaCategory> MediaCategoryRepository
        {
            get
            {
                return _mediaCategoryRepository = _mediaCategoryRepository ?? new GenericRepository<MediaCategory>(_mschaContext);
            }
        }

        public IGenericRepository<Medium> MediumRepository
        {
            get
            {
                return _mediumRepository = _mediumRepository ?? new GenericRepository<Medium>(_mschaContext);
            }
        }

        public IGenericRepository<Quality> QualityRepository
        {
            get
            {
                return _qualityRepository = _qualityRepository ?? new GenericRepository<Quality>(_mschaContext);
            }
        }

        public IGenericRepository<UnitRate> UnitRateRepository
        {
            get
            {
                return _unitRateRepository = _unitRateRepository ?? new GenericRepository<UnitRate>(_mschaContext);
            }
        }

        public IGenericRepository<Nature> NatureRepository
        {
            get
            {
                return _natureRepository = _natureRepository ?? new GenericRepository<Nature>(_mschaContext);
            }
        }

        public IGenericRepository<TimeCategory> TimeCategoryRepository
        {
            get
            {
                return _timeCategoryRepository = _timeCategoryRepository ?? new GenericRepository<TimeCategory>(_mschaContext);
            }
        }
        //Client Detail Repository
        public IGenericRepository<ClientDetails> ClientDetailsRepository
        {
            get
            {
                return _ClientDetailsRepository = _ClientDetailsRepository ?? new GenericRepository<ClientDetails>(_mschaContext);
            }
        }
        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepository = _categoryRepository ?? new GenericRepository<Category>(_mschaContext);
            }
        }

        public IGenericRepository<ProjectPhaseTime> ProjectPhaseTimeRepository
        {
            get
            {
                return _projectPhaseTimeRepository = _projectPhaseTimeRepository ?? new GenericRepository<ProjectPhaseTime>(_mschaContext);
            }
        }
        public IGenericRepository<ProjectCommunication> ProjectCommunicationRepository
        {
            get
            {
                return _projectCommunicationRepository = _projectCommunicationRepository ?? new GenericRepository<ProjectCommunication>(_mschaContext);
            }
        }
        public IGenericRepository<ProjectCommunicationAttachment> ProjectCommunicationAttachmentRepository
        {
            get
            {
                return _projectCommunicationAttachmentRepository = _projectCommunicationAttachmentRepository ?? new GenericRepository<ProjectCommunicationAttachment>(_mschaContext);
            }
        }


        public IGenericRepository<ExchangeRateDetail> ExchangeRateDetailRepository
        {
            get
            {
                return _exchangeRateDetaileRepository = _exchangeRateDetaileRepository ?? new GenericRepository<ExchangeRateDetail>(_mschaContext);
            }
        }

        public IGenericRepository<ApproveProjectDetails> ApproveProjectDetailsRepository
        {
            get
            {
                return _approveProjectDetailsRepository =
                    _approveProjectDetailsRepository ?? new GenericRepository<ApproveProjectDetails>(_mschaContext);
            }
        }

        public IGenericRepository<WinProjectDetails> WinProjectDetailsRepository
        {
            get
            {
                return _winroveProjectDetailsRepository =
                    _winroveProjectDetailsRepository ?? new GenericRepository<WinProjectDetails>(_mschaContext);
            }
        }
        public IGenericRepository<ProjectProposalDetail> ProjectProposalDetailRepository
        {
            get
            {
                return _projectProposalDetailRepository =
                    _projectProposalDetailRepository ?? new GenericRepository<ProjectProposalDetail>(_mschaContext);
            }
        }
        public IGenericRepository<DonorCriteriaDetails> DonorCriteriaDetailsRepository
        {
            get
            {
                return _donorCriteriaDetailsRepository =
                    _donorCriteriaDetailsRepository ?? new GenericRepository<DonorCriteriaDetails>(_mschaContext);
            }
        }

        public IGenericRepository<PaymentTypes> PaymentTypesRepository
        {
            get
            {
                return _paymentTypesRepository = _paymentTypesRepository ?? new GenericRepository<PaymentTypes>(_mschaContext);
            }
        }


        public IGenericRepository<PurposeofInitiativeCriteria> PurposeofInitiativeCriteriaRepository
        {
            get
            {
                return _purposeofInitiativeCriteriaRepository =
                    _purposeofInitiativeCriteriaRepository ?? new GenericRepository<PurposeofInitiativeCriteria>(_mschaContext);
            }
        }
        public IGenericRepository<EligibilityCriteriaDetail> EligibilityCriteriaDetailRepository
        {
            get
            {
                return _eligibilityCriteriaDetailRepository =
                    _eligibilityCriteriaDetailRepository ?? new GenericRepository<EligibilityCriteriaDetail>(_mschaContext);
            }
        }
        public IGenericRepository<FeasibilityCriteriaDetail> FeasibilityCriteriaDetailRepository
        {
            get
            {
                return _feasibilityCriteriaDetailRepository =
                    _feasibilityCriteriaDetailRepository ?? new GenericRepository<FeasibilityCriteriaDetail>(_mschaContext);
            }
        }
        public IGenericRepository<PriorityCriteriaDetail> PriorityCriteriaDetailRepository
        {
            get
            {
                return _priorityCriteriaDetailRepository =
                    _priorityCriteriaDetailRepository ?? new GenericRepository<PriorityCriteriaDetail>(_mschaContext);
            }
        }
        public IGenericRepository<FinancialCriteriaDetail> FinancialCriteriaDetailRepository
        {
            get
            {
                return _financialCriteriaDetailRepository =
                    _financialCriteriaDetailRepository ?? new GenericRepository<FinancialCriteriaDetail>(_mschaContext);
            }
        }
        public IGenericRepository<RiskCriteriaDetail> RiskCriteriaDetailRepository
        {
            get
            {
                return _riskCriteriaDetailRepository =
                    _riskCriteriaDetailRepository ?? new GenericRepository<RiskCriteriaDetail>(_mschaContext);
            }
        }
        public IGenericRepository<TargetBeneficiaryDetail> TargetBeneficiaryDetailRepository
        {
            get
            {
                return _targetBeneficiaryDetailRepository =
                    _targetBeneficiaryDetailRepository ?? new GenericRepository<TargetBeneficiaryDetail>(_mschaContext);
            }
        }

        public IGenericRepository<FinancialProjectDetail> FinancialProjectDetailRepository
        {
            get
            {
                return _financialProjectDetailRepository =
                    _financialProjectDetailRepository ?? new GenericRepository<FinancialProjectDetail>(_mschaContext);
            }
        }

        public IGenericRepository<PriorityOtherDetail> PriorityOtherDetailRepository
        {
            get
            {
                return _priorityOtherDetailRepository =
                    _priorityOtherDetailRepository ?? new GenericRepository<PriorityOtherDetail>(_mschaContext);
            }
        }

        public IGenericRepository<CEFeasibilityExpertOtherDetail> CEFeasibilityExpertOtherDetail
        {
            get
            {
                return _feasibilit5yexpertDetailRepository =
                    _feasibilit5yexpertDetailRepository ?? new GenericRepository<CEFeasibilityExpertOtherDetail>(_mschaContext);
            }
        }

        public IGenericRepository<CEAgeGroupDetail> CEAgeGroupDetail
        {
            get
            {
                return _ageGroupOtherDetailRepository =
                    _ageGroupOtherDetailRepository ?? new GenericRepository<CEAgeGroupDetail>(_mschaContext);
            }
        }

        public IGenericRepository<CEOccupationDetail> CEOccupationDetail
        {
            get
            {
                return _occupationOtherDetailRepository =
                    _occupationOtherDetailRepository ?? new GenericRepository<CEOccupationDetail>(_mschaContext);
            }
        }

        public IGenericRepository<CEAssumptionDetail> CEAssumptionDetail
        {
            get
            {
                return _assumptionOtherDetailRepository =
                    _assumptionOtherDetailRepository ?? new GenericRepository<CEAssumptionDetail>(_mschaContext);
            }
        }

        public IGenericRepository<DonorEligibilityCriteria> DonorEligibilityCriteriaRepository
        {
            get
            {
                return _donorEligibilityCriteria =
                    _donorEligibilityCriteria ?? new GenericRepository<DonorEligibilityCriteria>(_mschaContext);
            }
        }

        public IGenericRepository<ProvinceMultiSelect> ProvinceMultiSelectRepository
        {
            get
            {
                return _provinceMultiSelectRepository =
                    _provinceMultiSelectRepository ?? new GenericRepository<ProvinceMultiSelect>(_mschaContext);
            }
        }
        public IGenericRepository<DistrictMultiSelect> DistrictMultiSelectRepository
        {
            get
            {
                return _districtMultiSelectRepository =
                    _districtMultiSelectRepository ?? new GenericRepository<DistrictMultiSelect>(_mschaContext);
            }
        }
        public IGenericRepository<SecurityConsiderationMultiSelect> SecurityConsiderationMultiSelectRepository
        {
            get
            {
                return _securityConsiderationMultiSelectRepository =
                    _securityConsiderationMultiSelectRepository ?? new GenericRepository<SecurityConsiderationMultiSelect>(_mschaContext);
            }
        }

        public IGenericRepository<Errorlog> ErrorlogRepository
        {
            get
            {
                return _errorlogRepository =
                    _errorlogRepository ?? new GenericRepository<Errorlog>(_mschaContext);
            }
        }

        public IGenericRepository<ProjectBudgetLineDetail> ProjectBudgetLineDetailRepository
        {
            get
            {
                return _budgetLineDetailRepository = _budgetLineDetailRepository ?? new GenericRepository<ProjectBudgetLineDetail>(_mschaContext);
            }
        }

        public IGenericRepository<ProjectJobDetail> ProjectJobDetailRepository
        {
            get
            {
                return _projectJobDetailRepository = _projectJobDetailRepository ?? new GenericRepository<ProjectJobDetail>(_mschaContext);
            }
        }


        public void Save()
        {
            _mschaContext.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _mschaContext.SaveChangesAsync();
        }
        public ApplicationDbContext GetDbContext()
        {

            return this._mschaContext;
        }
    }
}
