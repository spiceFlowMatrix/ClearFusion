using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
   public class AddEditPurposeofInitiativeCriteriaCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? ProductServiceId { get; set; }
        public bool? Awareness { get; set; }
        public long ProjectId { get; set; }
        public bool? Infrastructure { get; set; }
        public bool? CapacityBuilding { get; set; }
        public bool? IncomeGeneration { get; set; }
        public bool? Mobilization { get; set; }
        public bool? PeaceBuilding { get; set; }
        public bool? SocialProtection { get; set; }
        public bool? SustainableLivelihood { get; set; }
        public bool? Advocacy { get; set; }
        public bool? Literacy { get; set; }
        public bool? EducationCapacityBuilding { get; set; }
        public bool? SchoolUpgrading { get; set; }
        public bool? EducationInEmergency { get; set; }
        public bool? OnlineEducation { get; set; }
        public bool? CommunityBasedEducation { get; set; }
        public bool? AcceleratedLearningProgram { get; set; }
        public bool? PrimaryHealthServices { get; set; }
        public bool? ReproductiveHealth { get; set; }
        public bool? Immunization { get; set; }
        public bool? InfantandYoungChildFeeding { get; set; }
        public bool? Nutrition { get; set; }
        public bool? CommunicableDisease { get; set; }
        public bool? Hygiene { get; set; }
        public bool? EnvironmentalHealth { get; set; }
        public bool? MentalHealthandDisabilityService { get; set; }
        public bool? HealthCapacityBuilding { get; set; }
        public bool? Telemedicine { get; set; }
        public bool? MitigationProjects { get; set; }
        public bool? WaterSupply { get; set; }
        public bool? Sanitation { get; set; }
        public bool? DisasterRiskHygiene { get; set; }
        public bool? DisasterCapacityBuilding { get; set; }
        public bool? EmergencyResponse { get; set; }
        public bool? RenewableEnergy { get; set; }
        public bool? Shelter { get; set; }
        public bool? NaturalResourceManagement { get; set; }
        public bool? AggriculutreCapacityBuilding { get; set; }
        public bool? LivestockManagement { get; set; }
        public bool? FoodSecurity { get; set; }
        public bool? ResearchandPublication { get; set; }
        public bool? Horticulture { get; set; }
        public bool? Irrigation { get; set; }
        public bool? Livelihood { get; set; }
        public bool? ValueChain { get; set; }
        public bool? Children { get; set; }
        public bool? Disabled { get; set; }
        public bool? IDPs { get; set; }
        public bool? Returnees { get; set; }
        public bool? Kuchis { get; set; }
        public bool? Widows { get; set; }
        public bool? Men { get; set; }
        public bool? Women { get; set; }
        public bool? Youth { get; set; }
    }
}
