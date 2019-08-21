using AutoMapper;
using HumanitarianAssistance.Application.Marketing.Commands.Common;
using HumanitarianAssistance.Application.Marketing.Commands.Create;
using HumanitarianAssistance.Application.Marketing.Commands.Update;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Marketing;

namespace HumanitarianAssistance.WebApi.Infrastructure
{
    public class MarketingMapper : Profile
    {
        public MarketingMapper()
        {
            CreateMap<ClientDetails, AddClientDetailsCommand>().ReverseMap();

            CreateMap<Quality, AddEditQualityCommand>().ReverseMap();

            CreateMap<TimeCategory, AddEditTimeCategoryCommand>().ReverseMap();

            CreateMap<Category, AddCategoryCommand>().ReverseMap();

            CreateMap<LanguageDetail, AddLanguageCommand>().ReverseMap();

            CreateMap<Category, EditCategoryCommand>().ReverseMap();

            CreateMap<ClientDetails, EditClientDetailsCommand>().ReverseMap();

            CreateMap<ActivityType, AddEditActivityTypeCommand>().ReverseMap();

            CreateMap<Channel, AddEditChannelCommand>().ReverseMap();

            CreateMap<ContractDetails, AddEditContractDetailCommand>().ReverseMap();

            CreateMap<JobDetails, AddEditJobDetailCommand>().ReverseMap();

            CreateMap<JobPriceDetails, JobPriceDetailsModel>().ReverseMap();

            CreateMap<JobPriceDetails, JobPriceDetailsModel>().ReverseMap();

            CreateMap<MediaCategory, AddEditMediaCategoryCommand>().ReverseMap();

            CreateMap<Medium, AddEditMediumCommand>().ReverseMap();

            CreateMap<Nature, AddEditNatureCommand>().ReverseMap();

            CreateMap<JobPhase, AddEditPhaseCommand>().ReverseMap();

            CreateMap<PolicyDetail, AddEditPolicyCommand>().ReverseMap();

            CreateMap<PolicyOrderSchedule, AddEditPolicyOrderScheduleCommand>().ReverseMap();

            CreateMap<PolicyDaySchedule, AddEditPolicyRepeatDaysCommand>().ReverseMap();

            CreateMap<PolicyScheduleModel, PolicySchedule>().ReverseMap();

            CreateMap<PolicyTimeSchedule, AddEditPolicyTimeScheduleCommand>().ReverseMap();

            CreateMap<Producer, AddEditProducerCommand>().ReverseMap();

            CreateMap<Quality, AddEditQualityCommand>().ReverseMap();

            CreateMap<ScheduleDetails, AddEditScheduleCommand>().ReverseMap();

            CreateMap<UnitRate, AddEditUnitRateCommand>().ReverseMap();
        }
    }
}
