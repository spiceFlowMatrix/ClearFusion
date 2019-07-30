export enum UIModuleHeaders {
  AccountingModule = 1,
  ProjectModule = 2,
  MarketingModule = 3,
  ProjectModuleDetail = 4,

  // Accounting
  ChartOfAccountHeader = 5,
  FinancialAccountHeader = 6,
  VouchersHeader = 7,
  ReportsHeader = 8,
  CurrencyExchangeReport = 9,
  BalanceSheetHeader = 10,
  ExchangeRateHeader = 11

  // Marketing
}

export enum AccountHeadTypes_Enum {
  Assets = 1,
  Liabilities = 2,
  OwnersEquity = 3,
  Income = 4,
  Expense = 5
}

export enum AccountCategory_Enum {
  BalanceSheet = 1,
  IncomeExpenseReport = 2
}

export enum AccountLevels {
  MainLevel = 1,
  ControlLevel = 2,
  SubLevel = 3,
  InputLevel = 4
}

export enum AccountLevelLimits {
  MainLevel = 9,
  ControlLevel = 99,
  SubLevel = 999,
  InputLevel = 999999
}

export enum Activities {
  Broadcasting = "Broadcasting",
  Production = "Production"
}

//#region "Criteria Evaluation"
export enum criteriaEvaluationScores {
  methodOfFunding_Sole = 1,
  methodOfFunding_Source = 0.9,
  methodOfFunding_Default = 0,

  pastFundingExperience_Yes = 1,
  pastFundingExperience_No = 0,

  proposalAccepted_Yes = 1,
  proposalAccepted_No = 0,

  pre_ProposalExperience = 'pre',
  post_ProposalExperience = 'Post',

  proposalExp_Professional_Yes = 1,
  proposalExp_Professional_No = 0,

  proposalExp_FundsOnTime_Yes = 1,
  proposalExp_FundsOnTime_No = 0,

  proposalExp_EffectiveCommunication_Yes = 1,
  proposalExp_EffectiveCommunication_No = 0,

  proposalExp_Disputes_Yes = 1,
  proposalExp_Disputes_No = 0,

  proposalExp_OtherDeliverable_Yes = 1,
  proposalExp_OtherDeliverable_No = 0,

  pastWorkExperoence_Yes = 1,
  pastWorkExperoence_No = 0,

  pastCriticismPerfomance_Yes = 0,
  pastCriticismPerfomance_No = 0,

  pastCriticismTimeManagement_Yes = 1,
  pastCriticismTimeManagement_No = 0,

  pastCriticismMoneyAllocation_Yes = 1,
  pastCriticismMoneyAllocation_No = 0,

  pastCriticismAccountability_Yes = 1,
  pastCriticismAccountability_No = 0,

  pastCriticismQualityDeliverable_Yes = 1,
  pastCriticismQualityDeliverable_No = 0,

  finanacingHistory_Good = 1,
  finanacingHistory_Neutral = 0,
  finanacingHistory_Bad = -1,

  religiousStanding_Good = 1,
  religiousStanding_Neutral = 0,
  religiousStanding_Bad = -1,

  politicalStanding_Good = 1,
  politicalStanding_Neutral = 0,
  politicalStanding_Bad = -1,

  // Products && services


  prodAwareness_Yes = 1,
  prodAwareness_No = 0,

  prodInfrastructure_Yes = 1,
  prodInfrastructure_No = 0,

  prodCapacityBuilding_Yes = 1,
  prodCapacityBuilding_No = 0,

  prodIncomeGeneration_Yes = 1,
  prodIncomeGeneration_No = 0,

  prodMobilization_Yes = 1,
  prodMobilization_No = 0,

  prodPeaceBuilding_Yes = 1,
  prodPeaceBuilding_No = 0,

  prodSocialProtection_Yes = 1,
  prodSocialProtection_No = 0,

  prodSustainableLivelihood_Yes = 1,
  prodSustainableLivelihood_No = 0,

  prodAdvocacy_Yes = 1,
  prodAdvocacy_No = 0,

  prodLiteracy_Yes = 1,
  prodLiteracy_No = 0,

  prodEducationCapacityBuilding_Yes = 1,
  prodEducationCapacityBuilding_No = 0,

  prodSchoolUpgrading_Yes = 1,
  prodSchoolUpgrading_No = 0,

  prodEducationInEmergency_Yes = 1,
  prodEducationInEmergency_No = 0,

  prodOnlineEducation_Yes = 1,
  prodOnlineEducation_No = 0,

  prodCommunityBasedEducation_Yes = 1,
  prodCommunityBasedEducation_No = 0,

  AcceleratedLearningProgram_Yes = 1,
  AcceleratedLearningProgram_No = 0,

  PrimaryHealthServices_Yes = 1,
  PrimaryHealthServices_No = 0,

  ReproductiveHealth_Yes = 1,
  ReproductiveHealth_No = 0,

  Immunization_Yes = 1,
  Immunization_No = 0,

  InfantandYoungChildFeeding_Yes = 1,
  InfantandYoungChildFeeding_No = 0,

  Nutrition_Yes = 1,
  Nutrition_No = 0,

  CommunicableDisease_Yes = 1,
  CommunicableDisease_No = 0,

  Hygiene_Yes = 1,
  Hygiene_No = 0,

  EnvironmentalHealth_Yes = 1,
  EnvironmentalHealth_No = 0,

  MentalHealthandDisabilityService_Yes = 1,
  MentalHealthandDisabilityService_No = 0,

  HealthCapacityBuilding_Yes = 1,
  HealthCapacityBuilding_No = 0,

  Telemedicine_Yes = 1,
  Telemedicine_No = 0,

  MitigationProjects_Yes = 1,
  MitigationProjects_No = 0,

  WaterSupply_Yes = 1,
  WaterSupply_No = 0,

  Sanitation_Yes = 1,
  Sanitation_No = 0,

  DisasterRiskHygiene_Yes = 1,
  DisasterRiskHygiene_No = 0,

  DisasterCapacityBuilding_Yes = 1,
  DisasterCapacityBuilding_No = 0,

  EmergencyResponse_Yes = 1,
  EmergencyResponse_No = 0,

  RenewableEnergy_Yes = 1,
  RenewableEnergy_No = 0,

  Shelter_Yes = 1,
  Shelter_No = 0,

  NaturalResourceManagement_Yes = 1,
  NaturalResourceManagement_No = 0,

  AggriculutreCapacityBuilding_Yes = 1,
  AggriculutreCapacityBuilding_No = 0,

  LivestockManagement_Yes = 1,
  LivestockManagement_No = 0,

  FoodSecurity_Yes = 1,
  FoodSecurity_No = 0,

  ResearchandPublication_Yes = 1,
  ResearchandPublication_No = 0,

  Horticulture_Yes = 1,
  Horticulture_No = 0,

  Irrigation_Yes = 1,
  Irrigation_No = 0,

  Livelihood_Yes = 1,
  Livelihood_No = 0,

  ValueChain_Yes = 1,
  ValueChain_No = 0,

  Women_Yes = 1,
  Women_No = 0,

  Men_Yes = 1,
  Men_No = 0,

  Youth_Yes = 1,
  Youth_No = 0,

  Children_Yes = 2,
  Children_No = 0,

  Disabled_Yes = 2 ,
  Disabled_No = 0,

  IDPs_Yes = 1,
  IDPs_No = 0,

  Returnees_Yes = 1,
  Returnees_No = 0,

  Kuchis_Yes = 2,
  Kuchis_No = 0,

  Widows_Yes = 2,
  Widows_No = 0,


  // donor Eligibility criteria
  onDonorELegibilityCrteria_Yes = 1,
  onDonorELegibilityCrteria_No = 0,

  donorEligibilityDeadline_Yes = 1,
  donorEligibilityDeadline_No = 0,

  donorELigibilityPartnership_Yes = 1,
  donorELigibilityPartnership_No = 0,

  // feasibility
  feasibilityCapacityForProject_Yes = 4,
  feasibilityCapacityForProject_No = 0,

  compensationTrainedStaff_Yes = -1,
  compensationTrainedStaff_No = 0,

  compensateByEquipment_Yes = -1,
  compensateByEquipment_No = 0,

  compensateExpandScope_Yes = -1,
  compensateExpandScope_No = 0,

  compensateGeographical_Yes = -1,
  compensateGeographical_No = 0,

  // feasibility third party
  allowedThirdPartyContract_Yes = 1,
  allowedThirdPartyContract_No = 0,

  // cost of compensation
  costOfCompensationTime = -1,
  costTimedefault = 0,
  costOfCompensationMoney = -1,
  moneyDefault = 0,

  // any in kind component
  anyInKindComponent_Yes = 1,
  anyInKindComponent_No = 0,

  useableByOrganisation_Yes = 1,
  useableByOrganisation_No = 0,

  feasibleExpertDeploy_Yes = 1,
  feasibleExpertDeploy_No = 0,

  feasibilityExpert_Yes = 1,
  feasibilityExpert_No = 0,

  // feasibleRight
  enoughTimeForQualityWork_Yes = 1,
  enoughTimeForQualityWork_No = 0,

  projectAllowedByLaw_Yes = 1,
  projectAllowedByLaw_No = 0,

  projectAllowByOrganisation_Yes = 1,
  projectAllowByOrganisation_No = 0,

  isProjectPractical_Yes = 1,
  isProjectPractical_No = 0,

  presenceCoverage_Yes = 1,
  presenceCoverage_No = 0,

  projectinLineWithFocus_Yes = 1,
  projectinLineWithFocus_No = 0,

  enoughTimeToPrepareproposal_Yes = 1,
  enoughTimeToPrepareproposal_No = 0,

  // cost efficiency
  costGreaterThanBudget_Yes = 0, // -1 (real score -1 but use only 0 for right calculation)
  costGreaterThanBudget_No = 1,
  costGreaterThanBudgetDefault = 0,

  financialCopntributionFulfil_Yes = 1,
  financialCopntributionFulfil_No = 0,

  // disabling Condition
  disablingSecurity_Yes = 0,
  disablingSecurity_No = 0,

  disablingGeographical_Yes = 0,
  disablingGeographical_No = 0,

  disablingSeasonal_Yes = 0,
  disablingSeasonal_No = 0,

  //#region Priority non monitoring value
  anyNonMonitoringValue_Yes = 1,
  anyNonMonitoringValue_No = 0,

  increasedEligibility_Yes = 0.4,
  increasedEligibility_No = 0,

  increasedReputation_Yes = 0.1,
  increasedReputation_No = 0,

  improvedDonorRelationaship_Yes = 0.2,
  improvedDonorRelationaship_No = 0,

  goodCause_Yes = 0.1,
  goodCause_No = 0,

  improvedPerformanceCapacity_Yes = 0.1,
  improvedPerformanceCapacity_No = 0,

  skillImprovement_Yes = 0.1,
  skillImprovement_No = 0,

  fillingfundingGaps_Yes = 0.2,
  fillingfundingGaps_No = 0,

  newSoftware_Yes = 0.1,
  newSoftware_No = 0,

  newEquipment_Yes = 0.1,
  newEquipment_No = 0,

  coverageAreaExpansion_Yes = 0.2,
  coverageAreaExpansion_No = 0,

  newTraining_Yes = 0.1,
  newTraining_No = 0,

  priorityOther_Yes = 1,
  priorityOther_No = 0,

  projectInlineWithOrganisalGoal_Yes = 1,
  projectInlineWithOrganisalGoal_No = 0,

  //#endregion

  //#region risk security form
  riskSecurity_Yes = 0,
  riskSecurity_No = 1,


  riskReputation_Yes = 0,
  riskReputation_No = 1,

  focusDeliveryRisk_Yes = 1,
  focusDeliveryRisk_No = 0,

  deliveryFailure_Yes = 0,
  deliveryFailure_No = 1,

  otherWayToHarmOrg_Yes = 1,
  otherWayToHarmOrg_No = 0,

  financialLosses_Yes = 1,
  financialLosses_No = 0,

  opportunityLoss_Yes = 1,
  opportunityLoss_No = 0,

  probabilityDelayCuts_Yes = 1,
  probabilityDelayCuts_No = 0,

  Geographical_Yes = -1,
  Geographical_No = 0,

  Insecurity_Yes = -2,
  Insecurity_No = 0,

  Season_Yes = -1,
  Season_No = 0,

  Ethnicity_Yes = -1,
  Ethnicity_No = 0,

  Culture_Yes = -2,
  Culture_No = 0,

  ReligiousBeliefs_Yes = -2,
  ReligiousBeliefs_No = 0,

  //#endregion
}
export enum TargetBeneficiaryTypes_Enum {
  AgeGroup = 1,
  Occupation = 2
}
//#endregion

//#region "ProposalDocument_Enum"
export enum ProposalDocument_Enum {
  proposal = "Proposal",
  edifile = "EOI",
  budgetfile = "BUDGET",
  conceptfile = "CONCEPT",
  presentationfile = "PRESENTATION"
}
//#endregion

//#region "Delete_Confirmation_Texts"
export enum Delete_Confirmation_Texts {
  deleteText1 = "Are you sure ?",
  yesText = "Confirm",
  noText = "cancel"
}
//#endregion

//#region "ShowHideDropdownEnum"
export enum ShowHideDropdownEnum {
  Office = 1,
  Journal = 2,
  Project = 3
}
//#endregion

//#region "ProjectPlanningPhases"
export enum ProjectActivityStatus {
  Planning = 1,
  Implementation = 2,
  Completed = 3
}
//#endregion

//#region "ProjectPlanningPhases"
export enum ProjectActivityPhases {
  Planning = 1,
  Implementation = 2,
  Monitoring = 3
}
//#endregion

//#region "FileSourceEntityTypes"
export enum FileSourceEntityTypes {
  Voucher = 1,
  Account = 2,
  ProjectDetail = 3,
  ProjectProposal = 4,
  ProjectProposalSupportingDoc = 5,
  ProjectActivityImplementation = 6,
  ProjectActivityMonitoring = 7,
  ProjectActivityPlanning = 8,
  DonorDetail = 9
}
//#endregion
//#region "EmployeeType"
export enum EmployeeType {
  Candidate = 1,
  Active = 2,
  Terminated = 3
}
//#endregion
