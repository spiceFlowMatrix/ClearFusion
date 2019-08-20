using AutoMapper;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Models;
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
        }
    }
}
