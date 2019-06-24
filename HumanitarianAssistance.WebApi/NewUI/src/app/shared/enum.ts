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
  Broadcasting = 'Broadcasting',
  Production = 'Production'
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

  // services
  serviceEducation_Yes = 1,
  serviceEducation_No = 0,

  servEducationHealthandNutrition_Yes = 1,
  servEducationHealthandNutrition_No = 0,

  servCommunityDevelop_Yes = 1,
  servCommunityDevelop_No = 0,

  servAggriculture_Yes = 1,
  servAggriculture_No = 0,

  serDRR_Yes = 1,
  serDRR_No = 0,

  // Products

  prodAwareness_Yes = 1,
  prodAwareness_No = 0,

  prodhildren_Yes = 1,
  prodhildren_No = 0,

  prodWomen_Yes = 1,
  prodWomen_No = 0,

  prodEducation_Yes = 1,
  prodEducation_No = 0,

  prodDrugAndAbuse_Yes = 1,
  prodDrugAndAbuse_No = 0,

  prodRights_Yes = 1,
  prodRights_No = 0,

  prodCulture_Yes = 1,
  prodCulture_No = 0,

  prodMusic_Yes = 1,
  prodMusic_No = 0,

  prodDocumentaries_Yes = 1,
  prodDocumentaries_No = 0,

  prodInvestigativeJournlism_Yes = 1,
  prodInvestigativeJournlism_No = 0,

  prodHealthAndNutrition_Yes = 1,
  prodHealthAndNutrition_No = 0,

  prodNews_Yes = 1,
  prodNews_No = 0,

  prodSocioPoliticalDebate_Yes = 1,
  prodSocioPoliticalDebate_No = 0,

  prodStudies_Yes = 1,
  prodStudies_No = 0,

  prodReport_Yes = 1,
  prodReport_No = 0,

  // Activities

  activityRadioProduction_Yes = 1,
  activityRadioProduction_No = 0,

  activityTVprogram_Yes = 1,
  activityTVprogram_No = 0,

  activityPrintedMedia_Yes = 1,
  activityPrintedMedia_No = 0,

  activityRoundTables_Yes = 1,
  activityRoundTables_No = 0,

  activityOther_Yes = 1,
  activityOther_No = 0,

  // target Beneficiaries

  tagetbeneficiaryWomen_Yes = 1,
  tagetbeneficiaryWomen_No = 0,

  targetBeneficiaryMen_Yes = 1,
  targetBeneficiaryMen_No = 0,

  tagetBenificiaryAgeGroup_Yes = 1,
  tagetBenificiaryAgeGroup_No = 0,

  targetbenficiaryOccupation_Yes = 1,
  targetbenficiaryOccupation_No = 0,

  // donor Eligibility criteria
  onDonorELegibilityCrteria_Yes = 1,
  onDonorELegibilityCrteria_No = 0,

  donorEligibilityDeadline_Yes = 1,
  donorEligibilityDeadline_No = 0,

  donorELigibilityPartnership_Yes = 1,
  donorELigibilityPartnership_No = 0,

  // feasibility
  feasibilityCapacityForProject_Yes = 1,
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
  costGreaterThanBudget_Yes = -1, // 0 (real score -1 but use only 0 for right calculation)
  costGreaterThanBudget_No = 1,

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
  probabilityDelayCuts_No = 0

  //#endregion
}
export enum TargetBeneficiaryTypes_Enum {
  AgeGroup = 1,
  Occupation = 2
}
//#endregion

//#region "ProposalDocument_Enum"
export enum ProposalDocument_Enum {
  proposal = 'Proposal',
  edifile = 'EOI',
  budgetfile = 'BUDGET',
  conceptfile = 'CONCEPT',
  presentationfile = 'PRESENTATION'
}
//#endregion

//#region "Delete_Confirmation_Texts"
export enum Delete_Confirmation_Texts {
  deleteText1 = 'Are you sure ?',
  yesText = 'Confirm',
  noText = 'cancel'
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
    DonorDetail = 9,
}
//#endregion
//#region "EmployeeType"
export enum EmployeeType {
  Candidate = 1,
  Active = 2,
  Terminated = 3
}
//#endregion
