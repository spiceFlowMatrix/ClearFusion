using AutoMapper;
using HumanitarianAssistance.Application.Configuration.Commands.Create;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Delete;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.WebApi.Infrastructure
{
    public class HRMapper : Profile
    {
        public HRMapper()
        {
            CreateMap<EmployeeHealthInfo,EmployeeHealthInformationModel>().ReverseMap();
            CreateMap<AddEmployeeContractCommand,EmployeeContract>().ReverseMap();
            CreateMap<AddAdvanceCommand,Advances>().ReverseMap();
            CreateMap<AddNewEmployeeCommand, EmployeeDetail>().ReverseMap();
            CreateMap<EmployeeProfessionalDetailModel, EmployeeProfessionalDetail>().ReverseMap();
            CreateMap<AddEmployeeHistoryCommand, EmployeeHistoryDetail>().ReverseMap();
            CreateMap<AddLeaveToEmployeeCommand, AssignLeaveToEmployee>().ReverseMap();
            CreateMap<AddEmployeeHealthInfoCommand, EmployeeHealthInfo>().ReverseMap();
            CreateMap<EmployeeHealthQuestion,EmployeeHealthInformationModel>().ReverseMap();
            CreateMap<AddEmployeeEducationsCommand, EmployeeEducations>().ReverseMap();
            CreateMap<AddEmployeeHistoryOutsideOrganizationCommand, EmployeeHistoryOutsideOrganization>().ReverseMap();
            CreateMap<AddEmployeeHistoryOutsideCountryCommand, EmployeeHistoryOutsideCountry>().ReverseMap();
            CreateMap<AddEmployeeRelativeInformationCommand, EmployeeInfoReferences>().ReverseMap();
            CreateMap<AddEmployeeInfoReferencesCommand, EmployeeRelativeInfo>().ReverseMap();
            CreateMap<AddEmployeeOtherSkillsCommand, EmployeeOtherSkills>().ReverseMap();
            CreateMap<AddEmployeeSalaryBudgetsCommand, EmployeeSalaryBudget>().ReverseMap();
            CreateMap<EditSalaryAnalyticalInfoCommand, EmployeeSalaryAnalyticalInfo>().ReverseMap();
            CreateMap<EmployeeAttendanceModel, EmployeeAttendance>().ReverseMap();
            CreateMap<JobHiringDetails, JobHiringDetailsModel>().ReverseMap();
            CreateMap<AddInterviewDetailsCommand, InterviewDetails>().ReverseMap();
            CreateMap<AddExitInterviewCommand, ExistInterviewDetails>().ReverseMap();
            CreateMap<AddEmployeeAppraisalCommand, EmployeeAppraisalDetails>().ReverseMap();
            CreateMap<AddJobHiringDetailCommand, JobHiringDetails>().ReverseMap();
            CreateMap<ExistInterviewDetails, ExitInterviewModel>().ReverseMap();
            CreateMap<RatingBasedCriteriaModel, RatingBasedCriteria>().ReverseMap();
            CreateMap<AssignLeaveToEmployeeModel, AssignLeaveToEmployee>().ReverseMap();
            CreateMap<EditEmployeeEducationsCommand, EmployeeEducations>().ReverseMap();
            CreateMap<EditEmployeeHistoryOutsideOrganizationCommand, EmployeeHistoryOutsideOrganization>().ReverseMap();
            CreateMap<EditEmployeeHistoryOutsideCountryCommand, EmployeeHistoryOutsideCountry>().ReverseMap();
            CreateMap<EditEmployeeRelativeInformationCommand, EmployeeRelativeInfo>().ReverseMap();
            CreateMap<EditEmployeeInfoReferencesCommand, EmployeeInfoReferences>().ReverseMap();
            CreateMap<EditEmployeeOtherSkillsCommand, EmployeeOtherSkills>().ReverseMap();
            CreateMap<AddEmployeeRelativeInformationCommand, EmployeeRelativeInfo>().ReverseMap();
            CreateMap<EditEmployeeRelativeInformationCommand, EmployeeRelativeInfo>().ReverseMap();
            CreateMap<EditEmployeeHealthInfoCommand, EmployeeHealthInfo>().ReverseMap();
            CreateMap<AddEmployeeInfoReferencesCommand, EmployeeInfoReferences>().ReverseMap();
            CreateMap<EditEmployeeSalaryBudgetsCommand, EmployeeSalaryBudget>().ReverseMap();
        }
    }
}
