using AutoMapper;
using HumanitarianAssistance.Application.Configuration.Commands.Create;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.WebApi.Infrastructure
{

    public class ConfigurationMapper : Profile
    {
        public ConfigurationMapper()
        {
            // Mapping
            CreateMap<SalaryHeadDetails, AddSalaryHeadCommand>().ReverseMap();
            CreateMap<JobGrade, AddJobGradeDetailCommand>().ReverseMap();
            CreateMap<DesignationDetail, AddDesignationCommand>().ReverseMap();
            CreateMap<ProfessionDetails, AddProfessionCommand>().ReverseMap();
            CreateMap<LeaveReasonDetail, AddLeaveReasonDetailCommand>().ReverseMap();
            CreateMap<Department, AddDepartmentCommand>().ReverseMap();
            CreateMap<QualificationDetails, AddQualificationDetailsCommand>().ReverseMap();
            CreateMap<AppraisalGeneralQuestions, AddAppraisalQuestionCommand>().ReverseMap();
            CreateMap<EmailSettingDetail, EmailSettingDetail>().ReverseMap();
            CreateMap<EmailSettingDetail, AddEmailSettingCommand>().ReverseMap();
            CreateMap<ContractTypeContent, AddContractContentCommand>().ReverseMap();
        }
    }
}