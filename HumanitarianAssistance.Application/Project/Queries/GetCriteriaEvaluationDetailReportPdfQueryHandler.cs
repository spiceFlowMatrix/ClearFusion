using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCriteriaEvaluationDetailReportPdfQueryHandler : IRequestHandler<GetCriteriaEvaluationDetailReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        private readonly IMapper _mapper;
        public GetCriteriaEvaluationDetailReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env, IMapper mapper)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
            _mapper = mapper;
        }

        public async Task<byte[]> Handle(GetCriteriaEvaluationDetailReportPdfQuery request, CancellationToken cancellationToken)
        {
            CriteriaEveluationModel model = new CriteriaEveluationModel();
            CriteriaEvaluationPdfModel pdfModel = new CriteriaEvaluationPdfModel();
            try
            {
                model =
                                   (from obj in _dbContext.ProjectDetail
                                    join fed in _dbContext.CEFeasibilityExpertOtherDetail on obj.ProjectId equals fed.ProjectId into ed
                                    from fed in ed.DefaultIfEmpty()
                                    join pod in _dbContext.PriorityOtherDetail on obj.ProjectId equals pod.ProjectId into pd
                                    from pod in pd.DefaultIfEmpty()
                                    join ad in _dbContext.CEAssumptionDetail on obj.ProjectId equals ad.ProjectId into asd
                                    from ad in asd.DefaultIfEmpty()
                                    join de in _dbContext.DonorEligibilityCriteria on obj.ProjectId equals de.ProjectId into dec
                                    from de in dec.DefaultIfEmpty()
                                    join donor in _dbContext.DonorCriteriaDetail on obj.ProjectId equals donor.ProjectId into a
                                    from donor in a.DefaultIfEmpty()
                                    join purpose in _dbContext.PurposeofInitiativeCriteria on obj.ProjectId equals purpose.ProjectId into d
                                    from purpose in d.DefaultIfEmpty()
                                    join eligibility in _dbContext.EligibilityCriteriaDetail on obj.ProjectId equals eligibility.ProjectId into e
                                    from eligibility in e.DefaultIfEmpty()
                                    join feasibility in _dbContext.FeasibilityCriteriaDetail on obj.ProjectId equals feasibility.ProjectId into g
                                    from feasibility in g.DefaultIfEmpty()
                                    join priority in _dbContext.PriorityCriteriaDetail on obj.ProjectId equals priority.ProjectId into pr
                                    from priority in pr.DefaultIfEmpty()
                                    join financial in _dbContext.FinancialCriteriaDetail on obj.ProjectId equals financial.ProjectId into fi
                                    from financial in fi.DefaultIfEmpty()
                                    join risk in _dbContext.RiskCriteriaDetail on obj.ProjectId equals risk.ProjectId into ri
                                    from risk in ri.DefaultIfEmpty()
                                    join currency in _dbContext.ProjectProposalDetail on obj.ProjectId equals currency.ProjectId into cr
                                    from currency in cr.DefaultIfEmpty()


                                    where obj.IsDeleted == false && obj.ProjectId == request.ProjectId
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
                                        CommunicableDisease = purpose.CommunicableDisease,
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
                                        IncreaseEligibility = priority.IncreaseEligibility,
                                        IncreaseReputation = priority.IncreaseReputation,
                                        ImproveDonorRelationship = priority.ImproveDonorRelationship,
                                        GoodCause = priority.GoodCause,
                                        Others = priority.Others,
                                        ImprovePerformancecapacity = priority.ImprovePerformancecapacity,
                                        SkillImprove = priority.SkillImprove,
                                        FillingFundingGap = priority.FillingFundingGap,
                                        NewSoftware = priority.NewSoftware,
                                        NewEquipment = priority.NewEquipment,
                                        CoverageAreaExpansion = priority.CoverageAreaExpansion,
                                        NewTraining = priority.NewTraining,
                                        ExpansionGoal = priority.ExpansionGoal,

                                        ProjectActivities = financial.ProjectActivities,
                                        Operational = financial.Operational,
                                        Overhead_Admin = financial.Overhead_Admin,
                                        Lump_Sum = financial.Lump_Sum,
                                        Total = (double)Math.Round(((financial.ProjectActivities ?? 0.0) + (financial.Operational ?? 0.0) + (financial.Overhead_Admin ?? 0.0) + (financial.Lump_Sum ?? 0.0)), 2),
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
                                        CEFeasibilityExpertOtherDetailModel = ed.Where(c => c.ProjectId == obj.ProjectId && c.IsDeleted == false)
                                                                                 .Select(c => new CEFeasibilityExpertOtherDetailModel
                                                                                 {
                                                                                     ExpertOtherDetailId = c.ExpertOtherDetailId,
                                                                                     Name = c.Name
                                                                                 }).ToList(),
                                        PriorityOtherDetailModel = pd.Where(p => p.ProjectId == obj.ProjectId && p.IsDeleted == false)
                                                                    .Select(p => new PriorityOtherDetailModel
                                                                    {
                                                                        PriorityOtherDetailId = p.PriorityOtherDetailId,
                                                                        Name = p.Name
                                                                    }).ToList(),


                                        AssumptionDetailModel = asd.Where(s => s.ProjectId == obj.ProjectId && s.IsDeleted == false)
                                                                    .Select(p => new AssumptionDetailModel
                                                                    {
                                                                        AssumptionDetailId = p.AssumptionDetailId,
                                                                        Name = p.Name
                                                                    }).ToList(),


                                        DonorEligibilityDetailModel = dec.Where(s => s.ProjectId == obj.ProjectId && s.IsDeleted == false)
                                                                    .Select(p => new DonorEligibilityDetailModel
                                                                    {
                                                                        DonorEligibilityDetailId = p.DonorEligibilityDetailId,
                                                                        Name = p.Name
                                                                    }).ToList(),

                                    }).FirstOrDefault(x => x.ProjectId == request.ProjectId);



                pdfModel = new CriteriaEvaluationPdfModel
                {


                    ProjectAllowedBylaw = model.ProjectAllowedBylaw == true ? "Yes" : "No",
                    CurrencyName = model.CurrencyName,

                    // feasibility
                    IsProjectPractical = model.IsProjectPractical == true ? "Yes" : "No",
                    CapacityAvailableForProject = model.CapacityAvailableForProject == true ? "Yes" : "No",
                    TrainedStaff = model.TrainedStaff == true ? "Yes" : "No",
                    ByEquipment = model.ByEquipment == true ? "Yes" : "No",
                    ExpandScope = model.ExpandScope == true ? "Yes" : "No",
                    GeoGraphicalPresence = model.GeoGraphicalPresence == true ? "Yes" : "No",
                    ThirdPartyContract = model.ThirdPartyContract == true ? "Yes" : "No",
                    CostOfCompensationMonth = model.CostOfCompensationMonth,
                    CostOfCompensationMoney = model.CostOfCompensationMoney,
                    AnyInKindComponent = model.AnyInKindComponent == true ? "Yes" : "No",
                    UseableByOrganisation = model.UseableByOrganisation == true ? "Yes" : "No",
                    FeasibleExpertDeploy = model.FeasibleExpertDeploy == true ? "Yes" : "No",

                    EnoughTimeForProject = model.EnoughTimeForProject == true ? "Yes" : "No",
                    ProjectByLeadership = model.ProjectByLeadership == true ? "Yes" : "No",
                    PresenceCoverageInProject = model.PresenceCoverageInProject == true ? "Yes" : "No",
                    ProjectInLineWithOrgFocus = model.ProjectInLineWithOrgFocus == true ? "Yes" : "No",
                    EnoughTimeToPrepareProposal = model.EnoughTimeToPrepareProposal == true ? "Yes" : "No",
                    ProjectRealCost = model.ProjectRealCost,
                    IsCostGreaterthenBudget = model.IsCostGreaterthenBudget == true ? "Yes" : "No",
                    PerCostGreaterthenBudget = model.PerCostGreaterthenBudget,
                    IsFinancialContribution = model.IsFinancialContribution == true ? "Yes" : "No",
                    IsSecurity = model.IsSecurity == true ? "Yes" : "No",
                    IsGeographical = model.IsGeographical == true ? "Yes" : "No",
                    IsSeasonal = model.IsSeasonal == true ? "Yes" : "No",
                    // donor 
                    MethodOfFunding = model.MethodOfFunding,
                    PastFundingExperience = model.PastFundingExperience == true ? "Yes" : "No",
                    ProposalAccepted = model.ProposalAccepted == true ? "Yes" : "No",
                    ProposalExperience = model.ProposalExperience == true ? "Yes" : "No",//Note: Pre/Post
                    Professional = model.Professional == true ? "Yes" : "No",
                    FundsOnTime = model.FundsOnTime == true ? "Yes" : "No",
                    EffectiveCommunication = model.EffectiveCommunication == true ? "Yes" : "No",
                    Dispute = model.Dispute == true ? "Yes" : "No",
                    OtherDeliverable = model.OtherDeliverable == true ? "Yes" : "No",
                    OtherDeliverableType = model.OtherDeliverableType,
                    PastWorkingExperience = model.PastWorkingExperience == true ? "Yes" : "No",
                    CriticismPerformance = model.CriticismPerformance == true ? "Yes" : "No",
                    TimeManagement = model.TimeManagement == true ? "Yes" : "No",
                    MoneyAllocation = model.MoneyAllocation == true ? "Yes" : "No",
                    Accountability = model.Accountability == true ? "Yes" : "No",
                    DeliverableQuality = model.DeliverableQuality == true ? "Yes" : "No",
                    DonorFinancingHistory = model.DonorFinancingHistory,// Note :
                    ReligiousStanding = model.ReligiousStanding,
                    PoliticalStanding = model.PoliticalStanding,

                    // sector and themes  
                    Awareness = model.Awareness,
                    Infrastructure = model.Infrastructure,
                    CapacityBuilding = model.CapacityBuilding,
                    IncomeGeneration = model.IncomeGeneration,
                    Mobilization = model.Mobilization,
                    PeaceBuilding = model.PeaceBuilding,
                    SocialProtection = model.SocialProtection,
                    SustainableLivelihood = model.SustainableLivelihood,
                    Advocacy = model.Advocacy,
                    Literacy = model.Literacy,
                    EducationCapacityBuilding = model.EducationCapacityBuilding,
                    SchoolUpgrading = model.SchoolUpgrading,
                    EducationInEmergency = model.EducationInEmergency,
                    OnlineEducation = model.OnlineEducation,
                    CommunityBasedEducation = model.CommunityBasedEducation,
                    AcceleratedLearningProgram = model.AcceleratedLearningProgram,
                    PrimaryHealthServices = model.PrimaryHealthServices,
                    ReproductiveHealth = model.ReproductiveHealth,
                    Immunization = model.Immunization,
                    InfantandYoungChildFeeding = model.InfantandYoungChildFeeding,
                    Nutrition = model.Nutrition,
                    CommunicableDisease = model.CommunicableDisease,
                    Hygiene = model.Hygiene,
                    EnvironmentalHealth = model.EnvironmentalHealth,
                    MentalHealthandDisabilityService = model.MentalHealthandDisabilityService,
                    HealthCapacityBuilding = model.HealthCapacityBuilding,
                    Telemedicine = model.Telemedicine,
                    MitigationProjects = model.MitigationProjects,
                    WaterSupply = model.WaterSupply,
                    Sanitation = model.Sanitation,
                    DisasterRiskHygiene = model.DisasterRiskHygiene,
                    DisasterCapacityBuilding = model.DisasterCapacityBuilding,
                    EmergencyResponse = model.EmergencyResponse,
                    RenewableEnergy = model.RenewableEnergy,
                    Shelter = model.Shelter,
                    NaturalResourceManagement = model.NaturalResourceManagement,
                    AggriculutreCapacityBuilding = model.AggriculutreCapacityBuilding,
                    LivestockManagement = model.LivestockManagement,
                    FoodSecurity = model.FoodSecurity,
                    ResearchandPublication = model.ResearchandPublication,
                    Horticulture = model.Horticulture,
                    Irrigation = model.Irrigation,
                    Livelihood = model.Livelihood,
                    ValueChain = model.ValueChain,
                    Women = model.Women,
                    Youth = model.Youth,
                    Men = model.Men,
                    Children = model.Children,
                    Disabled = model.Disabled,
                    IDPs = model.IDPs,
                    Returnees = model.Returnees,
                    Kuchis = model.Kuchis,
                    Widows = model.Widows,
                    // Eligibility : 
                    DonorCriteriaMet = model.DonorCriteriaMet == true ? "Yes" : "No",
                    EligibilityDealine = model.EligibilityDealine == true ? "Yes" : "No",
                    CoPartnership = model.CoPartnership == true ? "Yes" : "No",
                    // priority added value
                    IncreaseReputation = model.IncreaseReputation,
                    ImproveDonorRelationship = model.ImproveDonorRelationship,
                    GoodCause = model.GoodCause,
                    Others = model.Others,
                    ImprovePerformancecapacity = model.ImprovePerformancecapacity,
                    SkillImprove = model.SkillImprove,
                    FillingFundingGap = model.FillingFundingGap,
                    NewSoftware = model.NewSoftware,
                    NewEquipment = model.NewEquipment,
                    CoverageAreaExpansion = model.CoverageAreaExpansion,
                    NewTraining = model.NewTraining,
                    ExpansionGoal = model.ExpansionGoal == true ? "Yes" : "No",

                    //Finanacial PRofitability 
                    Total = model.Total,
                    ProjectActivities = model.ProjectActivities,
                    Operational = model.Operational,
                    Overhead_Admin = model.Overhead_Admin,
                    Lump_Sum = model.Lump_Sum,

                    // Risks
                    Security = model.Security == true ? "Yes" : "No",
                    Staff = model.Staff == true ? "Yes" : "No",
                    ProjectAssets = model.ProjectAssets == true ? "Yes" : "No",
                    Suppliers = model.Suppliers == true ? "Yes" : "No",
                    Beneficiaries = model.Beneficiaries == true ? "Yes" : "No",
                    OverallOrganization = model.OverallOrganization == true ? "Yes" : "No",
                    DeliveryFaiLure = model.DeliveryFaiLure == true ? "Yes" : "No",
                    PrematureSeizure = model.PrematureSeizure == true ? "Yes" : "No",
                    GovernmentConfiscation = model.GovernmentConfiscation == true ? "Yes" : "No",
                    DesctructionByTerroristActivity = model.DesctructionByTerroristActivity == true ? "Yes" : "No",
                    Reputation = model.Reputation == true ? "Yes" : "No",
                    Religious = model.Religious == true ? "Yes" : "No",
                    Sectarian = model.Sectarian == true ? "Yes" : "No",
                    Ethinc = model.Ethinc == true ? "Yes" : "No",
                    Social = model.Social == true ? "Yes" : "No",
                    Traditional = model.Traditional == true ? "Yes" : "No",
                    FocusDivertingrisk = model.FocusDivertingrisk == true ? "Yes" : "No",
                    Financiallosses = model.Financiallosses == true ? "Yes" : "No",
                    Opportunityloss = model.Opportunityloss == true ? "Yes" : "No",
                    Geographical = model.Geographical == true ? "Yes" : "No",
                    Insecurity = model.Insecurity == true ? "Yes" : "No",
                    Season = model.Season == true ? "Yes" : "No",
                    Ethnicity = model.Ethnicity == true ? "Yes" : "No",
                    Culture = model.Culture == true ? "Yes" : "No",
                    ReligiousBeliefs = model.ReligiousBeliefs == true ? "Yes" : "No",
                    Probablydelaysinfunding = model.Probablydelaysinfunding == true ? "Yes" : "No",
                    OtherOrganizationalHarms = model.OtherOrganizationalHarms == true ? "Yes" : "No",
                    OrganizationalDescription = model.OrganizationalDescription,
                    CheckedIconPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/check.png")?.PhysicalPath,
                    UnCheckedIconPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/uncheck.png")?.PhysicalPath,
                    CEFeasibilityExpertOtherDetailModel = model.CEFeasibilityExpertOtherDetailModel,
                    PriorityOtherDetailModel = model.PriorityOtherDetailModel,

                    AssumptionDetailModel = model.AssumptionDetailModel,
                    DonorEligibilityDetailModel = model.DonorEligibilityDetailModel,
                    // TotalScore = (double)Math.Round( (request.TotalScore ?? 0.0 ),3),
                    TotalScore = (double)(request.TotalScore ?? 0.0),

                    LogoPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath

                };


                var selectedProjects = await _dbContext.FinancialProjectDetail

                                                        .Include(x => x.SelectedProjectDetail)
                                                        .Where(x => x.ProjectId == request.ProjectId &&
                                                                    x.IsDeleted == false)
                                                        .ToListAsync();


                pdfModel.SelectedProjectsModel = new List<SelectedProjectsModel>();
                if (selectedProjects.Any())
                {
                    foreach (var item in selectedProjects)
                    {
                        SelectedProjectsModel modl = new SelectedProjectsModel();
                        modl.ProjectName = item.SelectedProjectDetail.ProjectName;
                        pdfModel.SelectedProjectsModel.Add(modl);
                    }
                }
                // Note : currency id Zero check is for old data stored in Proposal detail having currencyId is 0
                if (model.CurrencyId != null && model.CurrencyId != 0)

                {
                    var currencyName = await _dbContext.CurrencyDetails.Where(x => x.CurrencyId == model.CurrencyId).Select(c => new
                    {
                        CurrencyName = c.CurrencyName
                    }).FirstOrDefaultAsync();
                    pdfModel.CurrencyName = currencyName.CurrencyName;
                }


                // pdfModel.ProjectSelectionId = selectedProjects != null ? selectedProjects : null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return await _pdfExportService.ExportToPdf(pdfModel, "Pages/PdfTemplates/CriteriaEvaluationReport.cshtml");

        }
    }
}