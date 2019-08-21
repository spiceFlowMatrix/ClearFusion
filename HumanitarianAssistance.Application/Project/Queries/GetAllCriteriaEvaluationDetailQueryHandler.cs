using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Project.Models;
using System;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllCriteriaEvaluationDetailQueryHandler : IRequestHandler<GetAllCriteriaEvaluationDetailQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllCriteriaEvaluationDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllCriteriaEvaluationDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var details =
                    (from obj in _dbContext.ProjectDetail
                     join donor in _dbContext.DonorCriteriaDetail on obj.ProjectId equals donor.ProjectId into a
                     from donor in a.DefaultIfEmpty()
                     join purpose in _dbContext.PurposeofInitiativeCriteria on obj.ProjectId equals purpose.ProjectId into d
                     from purpose in d.DefaultIfEmpty()
                     join eligibility in _dbContext.EligibilityCriteriaDetail on obj.ProjectId equals eligibility.ProjectId into e
                     from eligibility in e.DefaultIfEmpty()
                     join feasibility in _dbContext.FeasibilityCriteriaDetail on obj.ProjectId equals feasibility.ProjectId into g
                     from feasibility in g.DefaultIfEmpty()
                     join Priority in _dbContext.PriorityCriteriaDetail on obj.ProjectId equals Priority.ProjectId into pr
                     from Priority in pr.DefaultIfEmpty()
                     join financial in _dbContext.FinancialCriteriaDetail on obj.ProjectId equals financial.ProjectId into fi
                     from financial in fi.DefaultIfEmpty()
                     join risk in _dbContext.RiskCriteriaDetail on obj.ProjectId equals risk.ProjectId into ri
                     from risk in ri.DefaultIfEmpty()
                     join currency in _dbContext.ProjectProposalDetail on obj.ProjectId equals currency.ProjectId into cr
                     from currency in cr.DefaultIfEmpty()


                     select new CriteriaEveluationModel
                     {
                         ProjectId = obj.ProjectId,
                         IsCriteriaEvaluationSubmit = obj.IsCriteriaEvaluationSubmit,
                         DonorCEId = donor != null ? donor.DonorCEId : 0,
                         MethodOfFunding = donor.MethodOfFunding,
                         PastFundingExperience = donor.PastFundingExperience,
                         ProposalAccepted = donor.ProposalAccepted,
                         ProposalExperience = donor.ProposalExperience,
                         Professional = donor.Professional,
                         FundsOnTime = donor.FundsOnTime,
                         EffectiveCommunication = donor.EffectiveCommunication,
                         Dispute = donor.Dispute,
                         OtherDeliverable = donor.OtherDeliverable,
                         OtherDeliverableType = donor.OtherDeliverableType,
                         PastWorkingExperience = donor.PastWorkingExperience,
                         CriticismPerformance = donor.CriticismPerformance,
                         TimeManagement = donor.TimeManagement,
                         MoneyAllocation = donor.MoneyAllocation,
                         Accountability = donor.Accountability,
                         DeliverableQuality = donor.DeliverableQuality,
                         DonorFinancingHistory = donor.DonorFinancingHistory,
                         ReligiousStanding = donor.ReligiousStanding,
                         PoliticalStanding = donor.PoliticalStanding,
                         ProductServiceId = purpose != null ? purpose.ProductServiceId : 0,
                         Awareness = purpose.Awareness,
                         Infrastructure = purpose.Infrastructure,
                         CapacityBuilding = purpose.CapacityBuilding,
                         IncomeGeneration = purpose.IncomeGeneration,
                         Mobilization = purpose.Mobilization,
                         PeaceBuilding = purpose.PeaceBuilding,
                         SocialProtection = purpose.SocialProtection,
                         SustainableLivelihood = purpose.SustainableLivelihood,
                         Advocacy = purpose.Advocacy,
                         Literacy = purpose.Literacy,
                         EducationCapacityBuilding = purpose.EducationCapacityBuilding,
                         SchoolUpgrading = purpose.SchoolUpgrading,
                         EducationInEmergency = purpose.EducationInEmergency,
                         OnlineEducation = purpose.OnlineEducation,
                         CommunityBasedEducation = purpose.CommunityBasedEducation,
                         AcceleratedLearningProgram = purpose.AcceleratedLearningProgram,
                         PrimaryHealthServices = purpose.PrimaryHealthServices,
                         ReproductiveHealth = purpose.ReproductiveHealth,
                         Immunization = purpose.Immunization,
                         InfantandYoungChildFeeding = purpose.InfantandYoungChildFeeding,
                         Nutrition = purpose.Nutrition,
                         CommunicableDisease = purpose.CommunityBasedEducation,
                         Hygiene = purpose.Hygiene,
                         EnvironmentalHealth = purpose.EnvironmentalHealth,
                         MentalHealthandDisabilityService = purpose.MentalHealthandDisabilityService,
                         HealthCapacityBuilding = purpose.HealthCapacityBuilding,
                         Telemedicine = purpose.Telemedicine,
                         MitigationProjects = purpose.MitigationProjects,
                         WaterSupply = purpose.WaterSupply,
                         Sanitation = purpose.Sanitation,
                         DisasterRiskHygiene = purpose.DisasterRiskHygiene,
                         DisasterCapacityBuilding = purpose.DisasterCapacityBuilding,
                         EmergencyResponse = purpose.EmergencyResponse,
                         RenewableEnergy = purpose.RenewableEnergy,
                         Shelter = purpose.Shelter,
                         NaturalResourceManagement = purpose.NaturalResourceManagement,
                         AggriculutreCapacityBuilding = purpose.AggriculutreCapacityBuilding,
                         LivestockManagement = purpose.LivestockManagement,
                         FoodSecurity = purpose.FoodSecurity,
                         ResearchandPublication = purpose.ResearchandPublication,
                         Horticulture = purpose.Horticulture,
                         Irrigation = purpose.Irrigation,
                         Livelihood = purpose.Livelihood,
                         ValueChain = purpose.ValueChain,
                         Women = purpose.Women,
                         Youth = purpose.Youth,
                         Men = purpose.Men,
                         Children = purpose.Children,
                         Disabled = purpose.Disabled,
                         IDPs = purpose.IDPs,
                         Returnees = purpose.Returnees,
                         Kuchis = purpose.Kuchis,
                         Widows = purpose.Widows,
                         DonorCriteriaMet = eligibility.DonorCriteriaMet,
                         EligibilityDealine = eligibility.EligibilityDealine,
                         CoPartnership = eligibility.CoPartnership,
                         CapacityAvailableForProject = feasibility.CapacityAvailableForProject,
                         TrainedStaff = feasibility.TrainedStaff,
                         ByEquipment = feasibility.ByEquipment,
                         ExpandScope = feasibility.ExpandScope,
                         GeoGraphicalPresence = feasibility.GeoGraphicalPresence,
                         ThirdPartyContract = feasibility.ThirdPartyContract,
                         CostOfCompensationMonth = feasibility.CostOfCompensationMonth,
                         CostOfCompensationMoney = feasibility.CostOfCompensationMoney,
                         AnyInKindComponent = feasibility.AnyInKindComponent,
                         UseableByOrganisation = feasibility.UseableByOrganisation,
                         FeasibleExpertDeploy = feasibility.FeasibleExpertDeploy,
                         EnoughTimeForProject = feasibility.EnoughTimeForProject,
                         ProjectAllowedBylaw = feasibility.ProjectAllowedBylaw,
                         ProjectByLeadership = feasibility.ProjectByLeadership,
                         IsProjectPractical = feasibility.IsProjectPractical,
                         PresenceCoverageInProject = feasibility.PresenceCoverageInProject,
                         ProjectInLineWithOrgFocus = feasibility.ProjectInLineWithOrgFocus,
                         EnoughTimeToPrepareProposal = feasibility.EnoughTimeToPrepareProposal,
                         ProjectRealCost = feasibility.ProjectRealCost,
                         IsCostGreaterthenBudget = feasibility.IsCostGreaterthenBudget,
                         PerCostGreaterthenBudget = feasibility.PerCostGreaterthenBudget,
                         IsFinancialContribution = feasibility.IsFinancialContribution,
                         IsSecurity = feasibility.IsSecurity,
                         IsGeographical = feasibility.IsGeographical,
                         IsSeasonal = feasibility.IsSeasonal,
                         IncreaseEligibility = Priority.IncreaseEligibility,
                         IncreaseReputation = Priority.IncreaseReputation,
                         ImproveDonorRelationship = Priority.ImproveDonorRelationship,
                         GoodCause = Priority.GoodCause,
                         ImprovePerformancecapacity = Priority.ImprovePerformancecapacity,
                         SkillImprove = Priority.SkillImprove,
                         FillingFundingGap = Priority.FillingFundingGap,
                         NewSoftware = Priority.NewSoftware,
                         NewEquipment = Priority.NewEquipment,
                         CoverageAreaExpansion = Priority.CoverageAreaExpansion,
                         NewTraining = Priority.NewTraining,
                         ExpansionGoal = Priority.ExpansionGoal,
                         Total = financial.Total,
                         ProjectActivities = financial.ProjectActivities,
                         Operational = financial.Operational,
                         Overhead_Admin = financial.Overhead_Admin,
                         Lump_Sum = financial.Lump_Sum,
                         Security = risk.Security,
                         Staff = risk.Staff,
                         ProjectAssets = risk.ProjectAssets,
                         Suppliers = risk.Suppliers,
                         Beneficiaries = risk.Beneficiaries,
                         OverallOrganization = risk.OverallOrganization,
                         DeliveryFaiLure = risk.DeliveryFaiLure,
                         PrematureSeizure = risk.PrematureSeizure,
                         GovernmentConfiscation = risk.GovernmentConfiscation,
                         DesctructionByTerroristActivity = risk.DesctructionByTerroristActivity,
                         Reputation = risk.Reputation,
                         Religious = risk.Religious,
                         Sectarian = risk.Sectarian,
                         Ethinc = risk.Ethinc,
                         Social = risk.Social,
                         Traditional = risk.Traditional,
                         FocusDivertingrisk = risk.FocusDivertingrisk,
                         Financiallosses = risk.Financiallosses,
                         Opportunityloss = risk.Opportunityloss,
                         Geographical = risk.Geographical,
                         Insecurity = risk.Insecurity,
                         Season = risk.Season,
                         Ethnicity = risk.Ethnicity,
                         Culture = risk.Culture,
                         ReligiousBeliefs = risk.ReligiousBeliefs,
                         CurrencyId = currency.CurrencyId,

                         //ProjectSelectionId = selected.ProjectSelectionId,

                         Probablydelaysinfunding = risk.Probablydelaysinfunding,
                         OtherOrganizationalHarms = risk.OtherOrganizationalHarms,
                         OrganizationalDescription = risk.OrganizationalDescription,


                     }).FirstOrDefault(x => x.ProjectId == request.ProjectId);


                List<long?> selectedProjects = await _dbContext.FinancialProjectDetail.Where(x => x.ProjectId == request.ProjectId &&
                                                                                                   x.IsDeleted == false
                                                                                                ).Select(x => x.ProjectSelectionId).
                                                                                                  ToListAsync();

                details.ProjectSelectionId = selectedProjects != null ? selectedProjects : null;

                response.data.CriteriaEveluationModel = details;
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