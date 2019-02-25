using AutoMapper;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Marketing;
using DataAccess.DbEntities.Project;
using DataAccess.DbEntities.Store;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using HumanitarianAssistance.ViewModels.Models.Project;
using HumanitarianAssistance.ViewModels.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebAPI
{
  public class AutoMapperProfile : Profile
  {
    

    public AutoMapperProfile()
   : this("MyProfile")
    {
    }
    protected AutoMapperProfile(string profileName) : base(profileName)
    {
      CreateMap<Permissions, PermissionsModel>().ReverseMap();
      CreateMap<OfficeDetail, OfficeDetailModel>().ReverseMap();
      CreateMap<UserDetails, UserDetailsModel>().ForMember(x=>x.OfficeId, opt=> opt.Ignore());
      CreateMap<Department, DepartmentModel>().ReverseMap();
      CreateMap<PermissionsInRoles, PermissionsInRolesModel>().ReverseMap();
      CreateMap<CurrencyDetails, CurrencyModel>().ReverseMap();
      CreateMap<JournalDetail, JournalDetailModel>().ReverseMap();
      CreateMap<EmailSettingDetail, EmailSettingModel>().ReverseMap();
      //CreateMap<ChartAccountDetail, ChartAccountDetailModel>().ReverseMap();
      CreateMap<VoucherDetail, VoucherDetailModel>().ReverseMap();
      CreateMap<VoucherTransactionDetails, VoucherTransactionModel>().ReverseMap();
      CreateMap<VoucherDocumentDetail, VoucherDocumentDetailModel>().ReverseMap();
      CreateMap<ExchangeRate, ExchangeRateModel>().ReverseMap();
      CreateMap<EmployeeDetail, EmployeeDetailModel>().ReverseMap();
      CreateMap<DesignationDetail, DesignationModel>().ReverseMap();
      //CreateMap<ProjectBudget, ProjectBudgetModel>().ReverseMap();
      CreateMap<JobHiringDetails, JobHiringDetailsModel>().ReverseMap();
      CreateMap<ProfessionDetails, ProfessionModel>().ReverseMap();
      CreateMap<InterviewScheduleDetails, InterviewScheduleModel>().ReverseMap();
      CreateMap<EmployeeSalaryDetails, EmployeeSalaryDetailsModel>().ReverseMap();
      CreateMap<TaskMaster, TaskMasterModel>().ReverseMap();
      CreateMap<ActivityMaster, ActivityMasterModel>().ReverseMap();
      CreateMap<ProjectBudgetLine, ProjectBudgetLineModel>().ReverseMap();
      CreateMap<ProjectDetails, ProjectDetailModel>().ReverseMap();
      CreateMap<BudgetLineType, BudgetLineTypeModel>().ReverseMap();
      CreateMap<AssignActivity, AssignActivityModel>().ReverseMap();
      CreateMap<AssignActivityApproveBy, AssignActivityApproveByModel>().ReverseMap();
      CreateMap<BudgetReceivable, BudgetReceivableModel>().ReverseMap();
      CreateMap<BudgetReceivable, BudgetReceivableModel>().ReverseMap();
      CreateMap<BudgetReceivedAmount, BudgetReceivedAmountModel>().ReverseMap();
      CreateMap<EmployeeDocumentDetail, EmployeeDocumentDetailModel>().ReverseMap();
      CreateMap<EmployeeHistoryDetail, EmployeeHistoryDetailModel>().ReverseMap();
      CreateMap<LeaveReasonDetail, LeaveReasonDetailModel>().ReverseMap();
      CreateMap<EmployeeProfessionalDetail, EmployeeProfessionalDetailModel>().ReverseMap();
      CreateMap<PayrollMonthlyHourDetail, PayrollMonthlyHourDetailModel>().ReverseMap();
      CreateMap<AssignLeaveToEmployee, AssignLeaveToEmployeeModel>().ReverseMap();
      CreateMap<FinancialYearDetail, FinancialYearDetailModel>().ReverseMap();
      //CreateMap<BudgetPayable, BudgetPayableModel>().ReverseMap();
      //CreateMap<BudgetPayableAmount, BudgetPayableAmountModel>().ReverseMap();
      CreateMap<EmployeeAttendance, EmployeeAttendanceModel>().ReverseMap();
      CreateMap<EmployeeHealthDetail, EmployeeHealthInformationModel>().ReverseMap();
      CreateMap<EmployeeApplyLeave, EmployeeApplyLeaveModel>().ReverseMap();
      CreateMap<NotesMaster, NotesMasterModel>().ReverseMap();
      CreateMap<QualificationDetails, QualificationDetailsModel>().ReverseMap();
      CreateMap<HolidayDetails, HolidayDetailsModel>().ReverseMap();
      CreateMap<JobGrade, JobGradeModel>().ReverseMap();
      CreateMap<SalaryHeadDetails, SalaryHeadModel>().ReverseMap();
      CreateMap<EmployeePensionRate, EmployeePensionRateModel>().ReverseMap();
      CreateMap<ContractTypeContent, ContractTypeModel>().ReverseMap();
      CreateMap<AppraisalGeneralQuestions, AppraisalQuestionModel>().ReverseMap();
      CreateMap<EmployeeAppraisalDetails, EmployeeAppraisalDetailsModel>().ReverseMap();
      CreateMap<EmployeeAppraisalQuestions, EmployeeAppraisalQuestionModel>().ReverseMap();
      CreateMap<Advances, AdvancesModel>().ReverseMap();
      CreateMap<InterviewDetails, InterviewDetailModel>().ReverseMap();
      CreateMap<InterviewTechnicalQuestion, InterviewTechQuesModel>().ReverseMap();
      CreateMap<ExistInterviewDetails, ExitInterviewModel>().ReverseMap();
      CreateMap<RatingBasedCriteriaModel, RatingBasedCriteria>().ReverseMap();
      CreateMap<CategoryPopulator, CategoryPopulatorModel>().ReverseMap();
      CreateMap<LoggerDetails, LoggerDetailsModel>().ReverseMap();
      CreateMap<StoreInventory, StoreInventoryModel>().ReverseMap();
      CreateMap<StoreInventoryItem, StoreInventoryItemModel>().ReverseMap();
      CreateMap<InventoryItemType, InventoryItemTypeModel>().ReverseMap();
      CreateMap<StoreItemPurchase, ItemPurchaseModel>().ReverseMap();
      CreateMap<StorePurchaseOrder, ItemOrderModel>().ReverseMap();
      CreateMap<ItemSpecificationDetails, ItemSpecificationDetailModel>().ReverseMap();
      CreateMap<ItemSpecificationMaster, ItemSpecificationMasterModel>().ReverseMap();
      CreateMap<EmployeeHistoryOutsideOrganization, EmployeeHistoryOutsideOrganizationModel>().ReverseMap();
      CreateMap<EmployeeHistoryOutsideCountry, EmployeeHistoryOutsideOrganizationModel>().ReverseMap();
      CreateMap<EmployeeRelativeInfo, EmployeeRelativeInfoModel>().ReverseMap();
      CreateMap<EmployeeInfoReferences, EmployeeRelativeInfoModel>().ReverseMap();
      CreateMap<EmployeeOtherSkills, EmployeeOtherSkillsModel>().ReverseMap();
      CreateMap<EmployeeSalaryBudget, EmployeeSalaryBudgetModel>().ReverseMap();
      CreateMap<EmployeeEducations, EmployeeEducationsModel>().ReverseMap();
      CreateMap<EmployeeSalaryAnalyticalInfo, EmployeeSalaryAnalyticalInfoModel>().ReverseMap();
      CreateMap<EmployeeHealthInfo, EmployeeHealthInformationModel>().ReverseMap();
      CreateMap<VoucherTransactions, VoucherTransactionModel>().ReverseMap();
      CreateMap<PayrollAccountHead, PayrollHeadModel>().ReverseMap();
      CreateMap<StoreSourceCodeDetail, StoreSourceCodeDetailModel>().ReverseMap();
      CreateMap<DonorModel, DonorDetail>().ReverseMap();
      CreateMap<SectorModel, SectorDetails>().ReverseMap();
      CreateMap<ProgramModel, ProgramDetail>().ReverseMap();
      CreateMap<AreaModel, AreaDetail>().ReverseMap();
      CreateMap<ProjectDetailNewModel, ProjectDetail>().ReverseMap();
      CreateMap<ProjectAssignToModel, ProjectAssignTo>().ReverseMap();
      CreateMap<ProjectProgramModel, ProgramDetail>().ReverseMap();
      CreateMap<ProjectSectorModel, ProjectSector>().ReverseMap();
      CreateMap<JobDetailsModel, JobDetails>().ReverseMap();
      CreateMap<ActivityTypeModel, ActivityType>().ReverseMap();
      CreateMap<JobPhaseModel, JobPhase>().ReverseMap();
      CreateMap<JobPriceDetailsModel, JobPriceDetails>().ReverseMap();
      CreateMap<LanguageModel, Language>().ReverseMap();
      CreateMap<MediaCategoryModel, MediaCategory>().ReverseMap();
      CreateMap<MediumModel, Medium>().ReverseMap();
      CreateMap<ContractDetailsModel, ContractDetails>().ReverseMap();
      CreateMap<NatureModel, Nature>().ReverseMap();
      CreateMap<QualityModel, Quality>().ReverseMap();
      CreateMap<UnitRateModel, UnitRate>().ReverseMap();
      CreateMap<TimeCategoryModel, TimeCategory>().ReverseMap();
      CreateMap<ProjectPhaseTimeModel, ProjectPhaseTime>().ReverseMap();
      CreateMap<ClientDetailModel, ClientDetails>().ReverseMap();
      CreateMap<CategoryModel, Category>().ReverseMap();
      CreateMap<ProducerModel, Producer>().ReverseMap();
      CreateMap<PolicyModel, PolicyDetail>().ReverseMap();
      CreateMap<ApproveProjectDetailModel, ApproveProjectDetails>().ReverseMap();
      CreateMap<WinApprovalProjectModel, WinProjectDetails>().ReverseMap();
      CreateMap<ProjectJobDetailModel, ProjectJobDetail>().ReverseMap();
      CreateMap<ProjectBudgetLineDetailModel, ProjectBudgetLineDetail>().ReverseMap();
      CreateMap<VoucherDetail, VoucherDetailEntityModel>().ReverseMap();
      CreateMap<PolicyTimeSchedule, PolicyTimeScheduleModel>().ReverseMap();
      CreateMap<PolicyDaySchedule, PolicyTimeModel>().ReverseMap();
      CreateMap<PolicyScheduleModel, PolicySchedule>().ReverseMap();
    }
  }
}


