using AutoMapper;
using DataAccess.DbEntities;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.ViewModels.Models;
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
      CreateMap<UserDetails, UserDetailsModel>().ReverseMap();
      CreateMap<Department, DepartmentModel>().ReverseMap();
      CreateMap<PermissionsInRoles, PermissionsInRolesModel>().ReverseMap();
      CreateMap<CurrencyDetails, CurrencyModel>().ReverseMap();
      CreateMap<JournalDetail, JournalDetailModel>().ReverseMap();
      CreateMap<EmailSettingDetail, EmailSettingModel>().ReverseMap();
      CreateMap<ChartAccountDetail, ChartAccountDetailModel>().ReverseMap();
      CreateMap<VoucherDetail, VoucherDetailModel>().ReverseMap();
      CreateMap<VoucherTransactionDetails, VoucherTransactionModel>().ReverseMap();
      CreateMap<VoucherDocumentDetail, VoucherDocumentDetailModel>().ReverseMap();
      CreateMap<ExchangeRate, ExchangeRateModel>().ReverseMap();
      CreateMap<EmployeeDetail, EmployeeDetailModel>().ReverseMap();
      CreateMap<DesignationDetail, DesignationModel>().ReverseMap();
      CreateMap<ProjectBudget, ProjectBudgetModel>().ReverseMap();
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
      CreateMap<BudgetPayable, BudgetPayableModel>().ReverseMap();
      CreateMap<BudgetPayableAmount, BudgetPayableAmountModel>().ReverseMap();
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
    }
  }
}
