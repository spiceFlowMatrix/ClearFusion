using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditPurposeofInitiativeCriteriaCommandHandler : IRequestHandler<AddEditPurposeofInitiativeCriteriaCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditPurposeofInitiativeCriteriaCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditPurposeofInitiativeCriteriaCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            PurposeofInitiativeCriteria _detail = new PurposeofInitiativeCriteria();
            try
            {
                _detail = await _dbContext.PurposeofInitiativeCriteria.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                         x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new PurposeofInitiativeCriteria
                    {

                        Awareness = request.Awareness,
                        Infrastructure = request.Infrastructure,
                        CapacityBuilding = request.CapacityBuilding,
                        IncomeGeneration = request.IncomeGeneration,
                        Mobilization = request.Mobilization,
                        PeaceBuilding = request.PeaceBuilding,
                        SocialProtection = request.SocialProtection,
                        SustainableLivelihood = request.SustainableLivelihood,
                        Advocacy = request.Advocacy,
                        Literacy = request.Literacy,
                        EducationCapacityBuilding = request.EducationCapacityBuilding,
                        SchoolUpgrading = request.SchoolUpgrading,
                        EducationInEmergency = request.EducationInEmergency,
                        OnlineEducation = request.OnlineEducation,
                        CommunityBasedEducation = request.CommunityBasedEducation,
                        AcceleratedLearningProgram = request.AcceleratedLearningProgram,
                        PrimaryHealthServices = request.PrimaryHealthServices,
                        ReproductiveHealth = request.ReproductiveHealth,
                        Immunization = request.Immunization,
                        InfantandYoungChildFeeding = request.InfantandYoungChildFeeding,
                        Nutrition = request.Nutrition,
                        CommunicableDisease = request.CommunityBasedEducation,
                        Hygiene = request.Hygiene,
                        EnvironmentalHealth = request.EnvironmentalHealth,
                        MentalHealthandDisabilityService = request.MentalHealthandDisabilityService,
                        HealthCapacityBuilding = request.HealthCapacityBuilding,
                        Telemedicine = request.Telemedicine,
                        MitigationProjects = request.MitigationProjects,
                        WaterSupply = request.WaterSupply,
                        Sanitation = request.Sanitation,
                        DisasterRiskHygiene = request.DisasterRiskHygiene,
                        DisasterCapacityBuilding = request.DisasterCapacityBuilding,
                        EmergencyResponse = request.EmergencyResponse,
                        RenewableEnergy = request.RenewableEnergy,
                        Shelter = request.Shelter,
                        NaturalResourceManagement = request.NaturalResourceManagement,
                        AggriculutreCapacityBuilding = request.AggriculutreCapacityBuilding,
                        LivestockManagement = request.LivestockManagement,
                        FoodSecurity = request.FoodSecurity,
                        ResearchandPublication = request.ResearchandPublication,
                        Horticulture = request.Horticulture,
                        Irrigation = request.Irrigation,
                        Livelihood = request.Livelihood,
                        ValueChain = request.ValueChain,
                        Children = request.Children,
                        Disabled = request.Disabled,
                        IDPs = request.IDPs,
                        Returnees = request.Returnees,
                        Kuchis = request.Kuchis,
                        Widows = request.Widows,
                        Women = request.Women,
                        Youth = request.Youth,
                        Men = request.Men,
                        ProjectId = request.ProjectId,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow,
                    };
                    await _dbContext.PurposeofInitiativeCriteria.AddAsync(_detail);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {

                    _detail.Awareness = request.Awareness;
                    _detail.Infrastructure = request.Infrastructure;
                    _detail.CapacityBuilding = request.CapacityBuilding;
                    _detail.IncomeGeneration = request.IncomeGeneration;
                    _detail.Mobilization = request.Mobilization;
                    _detail.PeaceBuilding = request.PeaceBuilding;
                    _detail.SocialProtection = request.SocialProtection;
                    _detail.SustainableLivelihood = request.SustainableLivelihood;
                    _detail.Advocacy = request.Advocacy;
                    _detail.Literacy = request.Literacy;
                    _detail.EducationCapacityBuilding = request.EducationCapacityBuilding;
                    _detail.SchoolUpgrading = request.SchoolUpgrading;
                    _detail.EducationInEmergency = request.EducationInEmergency;
                    _detail.OnlineEducation = request.OnlineEducation;
                    _detail.CommunityBasedEducation = request.CommunityBasedEducation;
                    _detail.AcceleratedLearningProgram = request.AcceleratedLearningProgram;
                    _detail.PrimaryHealthServices = request.PrimaryHealthServices;
                    _detail.ReproductiveHealth = request.ReproductiveHealth;
                    _detail.Immunization = request.Immunization;
                    _detail.InfantandYoungChildFeeding = request.InfantandYoungChildFeeding;
                    _detail.Nutrition = request.Nutrition;
                    _detail.CommunicableDisease = request.CommunityBasedEducation;
                    _detail.Hygiene = request.Hygiene;
                    _detail.EnvironmentalHealth = request.EnvironmentalHealth;
                    _detail.MentalHealthandDisabilityService = request.MentalHealthandDisabilityService;
                    _detail.HealthCapacityBuilding = request.HealthCapacityBuilding;
                    _detail.Telemedicine = request.Telemedicine;
                    _detail.MitigationProjects = request.MitigationProjects;
                    _detail.WaterSupply = request.WaterSupply;
                    _detail.Sanitation = request.Sanitation;
                    _detail.DisasterRiskHygiene = request.DisasterRiskHygiene;
                    _detail.DisasterCapacityBuilding = request.DisasterCapacityBuilding;
                    _detail.EmergencyResponse = request.EmergencyResponse;
                    _detail.RenewableEnergy = request.RenewableEnergy;
                    _detail.Shelter = request.Shelter;
                    _detail.NaturalResourceManagement = request.NaturalResourceManagement;
                    _detail.AggriculutreCapacityBuilding = request.AggriculutreCapacityBuilding;
                    _detail.LivestockManagement = request.LivestockManagement;
                    _detail.FoodSecurity = request.FoodSecurity;
                    _detail.ResearchandPublication = request.ResearchandPublication;
                    _detail.Horticulture = request.Horticulture;
                    _detail.Irrigation = request.Irrigation;
                    _detail.Livelihood = request.Livelihood;
                    _detail.ValueChain = request.ValueChain;
                    _detail.Children = request.Children;
                    _detail.Disabled = request.Disabled;
                    _detail.IDPs = request.IDPs;
                    _detail.Returnees = request.Returnees;
                    _detail.Kuchis = request.Kuchis;
                    _detail.Widows = request.Widows;
                    _detail.Women = request.Women;
                    _detail.Youth = request.Youth;
                    _detail.Men = request.Men;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = request.ModifiedById;
                    _detail.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }

            return response;
        }
    }
}
