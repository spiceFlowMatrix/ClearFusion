﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class CriteriaEveluationModel
    {
        public long ProjectId { get; set; }
        public long? DonorCEId { get; set; }
        public int? MethodOfFunding { get; set; }
        public bool? PastFundingExperience { get; set; }
        public bool? ProposalAccepted { get; set; }
        public bool? ProposalExperience { get; set; }
        public bool? Professional { get; set; }
        public bool? FundsOnTime { get; set; }
        public bool? EffectiveCommunication { get; set; }
        public bool? Dispute { get; set; }
        public bool? OtherDeliverable { get; set; }
        public string OtherDeliverableType { get; set; }
        public bool? PastWorkingExperience { get; set; }
        public bool? CriticismPerformance { get; set; }
        public bool? TimeManagement { get; set; }
        public bool? MoneyAllocation { get; set; }
        public bool? Accountability { get; set; }
        public bool? DeliverableQuality { get; set; }
        public int? DonorFinancingHistory { get; set; }
        public int? ReligiousStanding { get; set; }
        public int? PoliticalStanding { get; set; }
        public long FeasibilityId { get; set; }
        public bool? CapacityAvailableForProject { get; set; }
        public bool? TrainedStaff { get; set; }
        public bool? ByEquipment { get; set; }
        public bool? ExpandScope { get; set; }
        public bool? GeoGraphicalPresence { get; set; }
        public bool? ThirdPartyContract { get; set; }
        public int? CostOfCompensationMonth { get; set; }
        public double? CostOfCompensationMoney { get; set; }
        public bool? AnyInKindComponent { get; set; }
        public bool? UseableByOrganisation { get; set; }
        public bool? FeasibleExpertDeploy { get; set; }
        public bool? EnoughTimeForProject { get; set; }
        public bool? ProjectAllowedBylaw { get; set; }
        public bool? ProjectByLeadership { get; set; }
        public bool? IsProjectPractical { get; set; }
        public bool? PresenceCoverageInProject { get; set; }
        public bool? ProjectInLineWithOrgFocus { get; set; }
        public bool? EnoughTimeToPrepareProposal { get; set; }
        public double? ProjectRealCost { get; set; }
        public bool? IsCostGreaterthenBudget { get; set; }
        public int? PerCostGreaterthenBudget { get; set; }
        public bool? IsFinancialContribution { get; set; }
        public bool? IsSecurity { get; set; }
        public bool? IsGeographical { get; set; }
        public bool? IsSeasonal { get; set; }
        public long ProductServiceId { get; set; }
        public bool? Women { get; set; }
        public long EligibilityId { get; set; }
        public bool? Children { get; set; }
        public bool? Awareness { get; set; }
        public bool? Education { get; set; }
        public bool? DrugAbuses { get; set; }
        public bool? Right { get; set; }
        public bool? Culture { get; set; }
        public bool? Music { get; set; }
        public bool? Documentaries { get; set; }
        public bool? InvestigativeJournalism { get; set; }
        public bool? HealthAndNutrition { get; set; }
        public bool? News { get; set; }
        public bool? SocioPolitiacalDebate { get; set; }
        public bool? Studies { get; set; }
        public bool? Reports { get; set; }
        public bool? CommunityDevelopment { get; set; }
        public bool? Aggriculture { get; set; }
        public bool? DRR { get; set; }
        public bool? ServiceEducation { get; set; }
        public bool? ServiceHealthAndNutrition { get; set; }
        public bool? RadioProduction { get; set; }
        public bool? TVProgram { get; set; }
        public bool? PrintedMedia { get; set; }
        public bool? RoundTable { get; set; }
        public bool? Others { get; set; }
        public string OtherActivity { get; set; }
        public bool? TargetBenificaiaryWomen { get; set; }
        public bool? TargetBenificiaryMen { get; set; }
        public bool? TargetBenificiaryAgeGroup { get; set; }
        public bool? TargetBenificiaryaOccupation { get; set; }
        public bool? Service { get; set; }
        public bool? Product { get; set; }
        public bool? DonorCriteriaMet { get; set; }
        public bool? EligibilityDealine { get; set; }
        public bool? CoPartnership { get; set; }
        public bool? IncreaseEligibility { get; set; }
        public bool? IncreaseReputation { get; set; }
        public bool? ImproveDonorRelationship { get; set; }
        public bool? GoodCause { get; set; }
        public bool? ImprovePerformancecapacity { get; set; }
        public bool? SkillImprove { get; set; }
        public bool? FillingFundingGap { get; set; }
        public bool? NewSoftware { get; set; }
        public bool? NewEquipment { get; set; }
        public bool? CoverageAreaExpansion { get; set; }
        public bool? NewTraining { get; set; }
        public bool? ExpansionGoal { get; set; }
        public double? Total { get; set; }
        
        public double? ProjectActivities { get; set; }
        public double? Operational { get; set; }
        public double? Overhead_Admin { get; set; }
        public double? Lump_Sum { get; set; }
        public bool? Security { get; set; }
        public bool? Staff { get; set; }
        public bool? ProjectAssets { get; set; }
        public bool? Suppliers { get; set; }
        public bool? Beneficiaries { get; set; }
        public bool? OverallOrganization { get; set; }
        public bool? DeliveryFaiLure { get; set; }
        public bool? PrematureSeizure { get; set; }
        public bool? GovernmentConfiscation { get; set; }
        public bool? DesctructionByTerroristActivity { get; set; }
        public bool? Reputation { get; set; }
        public bool? Religious { get; set; }
        public bool? Sectarian { get; set; }
        public bool? Ethinc { get; set; }
        public bool? Social { get; set; }
        public bool? Traditional { get; set; }
        public bool? FocusDivertingrisk { get; set; }
        public bool? Financiallosses { get; set; }
        public bool? Opportunityloss { get; set; }
        public List<long?> ProjectSelectionId { get; set; }

        public bool? Probablydelaysinfunding { get; set; }
        public bool? OtherOrganizationalHarms { get; set; }
        public string OrganizationalDescription { get; set; }
        public bool? IsCriteriaEvaluationSubmit { get; set; }

    }
}
