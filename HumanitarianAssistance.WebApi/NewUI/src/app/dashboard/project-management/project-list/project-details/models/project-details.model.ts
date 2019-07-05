
export interface ProjectDetailModel {
    ProjectId?: number;
    ProjectCode?: string;
    ProjectName?: string;
    StartData?: any;
    EndDate?: any;
    ProjectPhaseDetailsId?: number;
    IsProposalComplate?: boolean;
    ProjectDescription?: string;
    IsWin?: boolean;
    IsApproved?: boolean;
    TotalDaysinHours?: string;
    ProjectPhase?: string;
    IsCriteriaEvaluationSubmit?: boolean;
    IsProposalSubmit?: boolean;

    ReviewerId?: number;
    DirectorId?: number;
}
export interface ProjectChatModel {
    ProjectId?: any;
    ProjectDescription: string;
    FilePath?: string;
    FileName?: string;
    UserRole?: string;
    CreatedByName?: string;
    CreatedDate?: string;
    room?: string;
}
export interface SectorModel {
    SectorId?: number;
    SectorName?: string;
    SectorCode?: string;
}
export interface ProgramModel {
    ProgramId?: number;
    ProgramName?: string;
    ProgramCode?: string;
}
export interface AreaModel {
    AreaId?: number;
    AreaName?: string;
    AreaCode?: string;
}
export interface ProjectProgramModel {
    ProjectProgramId?: number;
    ProjectId?: number;
    ProgramId?: number;
}
export interface ProjectAreaModel {
    ProjectAreaId?: number;
    ProjectId?: number;
    AreaId?: number;
}
export interface ProjectSectorModel {
    ProjectSectorId?: number;
    ProjectId?: number;
    SectorId?: number;
}
export interface ProvinceModel {
    ProvinceId?: number;
    ProvinceName?: string;

}
export interface ProjectProvinceModel {
    value: number;
    label: string;
}

export interface ProjectOtherDetailModel {
    ProjectOtherDetailId?: number;
    opportunityNo?: string;
    opportunity?: string;
    opportunitydescription?: string;
    ProjectId?: number;
    // ProvinceId?: string;
    ProvinceId?: number[];
    DistrictID?: string;
    OfficeId?: any;
    StartDate?: any;
    EndDate?: any;
    CurrencyId?: any;
    budget?: string;
    beneficiaryMale?: number;
    beneficiaryFemale?: number;
    projectGoal?: string;
    projectObjective?: string;
    mainActivities?: string;
    DonorId?: any;
    SubmissionDate?: any;
    REOIReceiveDate?: any;
    StrengthConsiderationId?: number;
    GenderConsiderationId?: number;
    GenderRemarks?: string;
    SecurityId?: number;
    SecurityConsiderationId?: string;
    SecurityRemarks?: string;

    ProvinceIdList?: number[];
    InDirectBeneficiaryMale?: number;
    InDirectBeneficiaryFemale?: number;
    OpportunityType?: number;
}
export interface SectorModel {
    SectorId?: number;
    SectorName?: string;
    SectorCode?: string;
    ProjectId?: number;
}
export interface ProgramModel {
    ProgramId?: number;
    ProgramName?: string;
    ProgramCode?: string;
    ProjectId?: number;
}
export interface AreaModel {
    AreaId?: number;
    AreaName?: string;
    AreaCode?: string;
}
export interface ProjectProgramModel {
    ProjectProgramId?: number;
    ProjectId?: number;
    ProgramId?: number;
}
export interface ProjectAreaModel {
    ProjectAreaId?: number;
    ProjectId?: number;
    AreaId?: number;
}
export interface ProjectSectorModel {
    ProjectSectorId?: number;
    ProjectId?: number;
    SectorId?: number;
}
export interface ApproveProjectDetailModel {
    ProjectId?: number;
    CommentText: string;
    FileName: string;
    FilePath: string;
    IsApproved: boolean;
    UploadedFile: any;
}
export interface WinApprovalDetailModel {
    ProjectId?: number;
    CommentText: string;
    FileName: string;
    FilePath: string;
    IsWin: boolean;
    UploadedFile: any;
}
export interface OfficeModel {
    OfficeId?: number;
    OfficeName?: string;
}
export interface SecurityModel {
    SecurityId?: number;
    SecurityName?: string;
}
export interface strengthModel {
    StrengthConsiderationId?: number;
    StrengthConsiderationName?: string;
}
export interface GenderConsiderationvalueModel {
    GenderConsiderationId?: number;
    GenderConsiderationName?: string;
}
export interface DonorModel {
    DonorId?: number;
    Name?: string;
}
export interface CurrencyModel {
    CurrencyId?: number;
    CurrencyCode?: string;
}
export interface OfficeModel {
    OfficeId?: number;
    OfficeName?: string;
}
export interface SecurityModel {
    SecurityId?: number;
    SecurityName?: string;
}
export interface strengthModel {
    StrengthConsiderationId?: number;
    StrengthConsiderationName?: string;
}
export interface GenderConsiderationvalueModel {
    GenderConsiderationId?: number;
    GenderConsiderationName?: string;
}
export interface DonorModel {
    DonorId?: number;
    Name?: string;
}
export interface CurrencyModel {
    CurrencyId?: number;
    CurrencyCode?: string;
}
export interface ProposalDocModel {
    ProjectId?: number;
    UserId?: number;
    ProposalStartDate?: any;
    ProposalBudget?: any;
    ProposalDueDate?: any;
    ProjectAssignTo?: number;
    IsProposalAccept?: any;
    CurrencyId?: any;
    IsApproved?: boolean;
}
export interface UserListModel {
    UserID?: number;
    Username?: string;
}

//multiselect
export interface securityConsiderationMultiSelectModel {

    SecurityConsiderationMultiSelectId?: number;
    SecurityConsiderationId?: number[];
    ProjectId: number;
}


export interface ProvinceMultiSelectModel {
    ProvinceMultiSelectId?: number;
    ProjectId?: number;
    ProvinceId?: number[];
    ProvinceSelectionId?: number;

}

export interface DistrictMultiSelectModel {
    DistrictMultiSelectId?: number;
    ProjectId?: number;
    DistrictID?: number[];
    DistrictSelectionId?: number;
    ProvinceId?: number[];

}


//Critera evaluation form

export interface DonorCEModel {
    ProjectId?: number;
    DonorCEId?: number;
    MethodOfFunding?: number;
    PastFundingExperience?: any;
    ProposalAccepted?: any;
    ProposalExperience?: boolean;
    Professional?: any;
    FundsOnTime?: any;
    EffectiveCommunication?: any;
    Dispute?: any;
    OtherDeliverable?: any;
    OtherDeliverableType?: string;
    PastWorkingExperience?: boolean;
    CriticismPerformance?: any;
    TimeManagement?: any;
    MoneyAllocation?: any;
    Accountability?: any;
    DeliverableQuality?: any;
    DonorFinancingHistory?: number;
    ReligiousStanding?: number;
    PoliticalStanding?: number;

}

export interface ProductAndServiceCEModel {
    ProjectId?: number;
    ProductServiceId?: number;
    Women?: any;
    Children?: any;
    Awareness?: any;
    Education?: any;
    DrugAbuses?: any;
    Right?: any;
    Culture?: any;
    Music?: any;
    Documentaries?: any;
    InvestigativeJournalism?: any;
    HealthAndNutrition?: any;
    News?: any;
    SocioPolitiacalDebate?: any;
    Studies?: any;
    Reports?: any;

    CommunityDevelopment?: any;
    Aggriculture?: any;
    DRR?: any;
    ServiceEducation?: any;
    ServiceHealthAndNutrition?: any;

    RadioProduction?: any;
    TVProgram?: any;
    PrintedMedia?: any;
    RoundTable?: any;
    Others?: any;
    OtherActivity?: any;

    TargetBenificaiaryWomen?: any;
    TargetBenificiaryMen?: any;
    TargetBenificiaryAgeGroup?: any;
    TargetBenificiaryaOccupation?: any;
    Product?: boolean;
    Service?: boolean;
}
export interface EligibilityCEModel {
    EligibilityId?: number;
    ProjectId?: number;
    DonorCriteriaMet?: any;
    EligibilityDealine?: any;
    CoPartnership?: any;

}
export interface FeasibilityCEModel {
    FeasibilityId?: number;
    ProjectId: number;
    CapacityAvailableForProject?: any;
    TrainedStaff?: boolean;
    ByEquipment?: boolean;
    ExpandScope?: boolean;
    GeoGraphicalPresence?: boolean;

    ThirdPartyContract?: any;
    CostOfCompensationMonth?: number;
    CostOfCompensationMoney?: number;

    AnyInKindComponent?: any;
    UseableByOrganisation?: any;
    FeasibleExpertDeploy?: any;
    FeasibilityExpert?: any;

    EnoughTimeForProject?: any;
    ProjectAllowedBylaw?: any;
    ProjectByLeadership?: any;
    IsProjectPractical?: any;
    PresenceCoverageInProject?: any;
    ProjectInLineWithOrgFocus?: any;
    EnoughTimeToPrepareProposal?: any;

    ProjectRealCost?: any
    IsCostGreaterthenBudget?: boolean;
    PerCostGreaterthenBudget?: any;
    IsFinancialContribution?: boolean;
    IsSecurity?: boolean;
    IsGeographical?: boolean;
    IsSeasonal?: boolean;

}

export interface TargetBeneficiaryModel {
    TargetId?: number;
    TargetName?: string;
    ProjectId: number;
    TargetType: any;
    _index?: number;
    _error?: boolean;
}
export interface PriorityCEmodel {
    PriorityCriteriaDetailId?: number;
    ProjectId?: number;
    IncreaseEligibility?: any;
    IncreaseReputation?: any;
    ImproveDonorRelationship?: any;
    GoodCause?: any;
    ImprovePerformancecapacity?: any;
    SkillImprove?: any;
    FillingFundingGap?: any;
    NewSoftware?: any;
    NewEquipment?: any;
    CoverageAreaExpansion?: any;
    NewTraining?: any;
    Others?: boolean;
    ExpansionGoal?: any;
}

export interface FinancialProfitabilityModel {

    FinancialCriteriaDetailId?: number;
    ProjectId?: number;
    ProjectActivities?: number;
    Operational?: number;
    Overhead_Admin?: number;
    Lump_Sum?: number;
    Total?: number;

}
export interface RiskSecurityModel {
    RiskCriteriaDetailId?: number;
    ProjectId?: number;
    Security?: boolean;
    Staff?: any;
    ProjectAssets?: any;
    Suppliers?: any;
    Beneficiaries?: any;
    OverallOrganization?: any;
    DeliveryFaiLure?: boolean;
    PrematureSeizure?: any;
    GovernmentConfiscation?: any;
    DesctructionByTerroristActivity?: any;
    Reputation?: boolean;
    Religious?: any;
    Sectarian?: any;
    Ethinc?: any;
    Social?: any;
    Traditional?: any;
    FocusDivertingrisk?: any;
    Financiallosses?: any;
    Opportunityloss?: any;
    ProjectSelectionId?: number[];
    Probablydelaysinfunding?: any;
    OtherOrganizationalHarms?: any;
    OrganizationalDescription?: any;
}

export interface FinancialProjectDetailModel {
    FinancialProjectDetailId?: number;
    ProjectId?: number;
    ProjectSelectionId?: number[];
    ProjectName?: string;
}

export interface IApproveRejectModel {
    text: string,
    flag: boolean,
    data: any
}

export interface IUserListModel {
  UserId: number;
  Username: string;
}


export interface IAddPeoplePermissionModel {
  UserId: number;
  RoleId: number;
}
