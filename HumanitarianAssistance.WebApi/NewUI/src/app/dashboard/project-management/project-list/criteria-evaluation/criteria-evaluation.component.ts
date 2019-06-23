import { Component, OnInit, HostListener, EventEmitter, Output } from '@angular/core';

import {
  DonorCEModel,
  EligibilityCEModel,
  FeasibilityCEModel,
  PriorityCEmodel,
  FinancialProfitabilityModel,
  RiskSecurityModel,
  ProductAndServiceCEModel,
  TargetBeneficiaryModel,
  FinancialProjectDetailModel
} from '../project-details/models/project-details.model';
import { CriteriaEvaluationService } from '../service/criteria-evaluation.service';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { FormControl, Validators } from '@angular/forms';
import { SelectItem } from 'primeng/primeng';
import { ToastrService } from 'ngx-toastr';
import { IPriorityOtherModel, IFeasibilityExpert, ICEAssumptionModel, ICEAgeDEtailModel, ICEOccupationModel, ICEDonorEligibilityModel, ICEisCESubmitModel } from './criteria-evaluation.model';
import { ActivatedRoute } from '@angular/router';
import { TargetBeneficiaryTypes_Enum, criteriaEvaluationScores } from 'src/app/shared/enum';

@Component({
  selector: 'app-criteria-evaluation',
  templateUrl: './criteria-evaluation.component.html',
  styleUrls: ['./criteria-evaluation.component.scss']
})
export class CriteriaEvaluationComponent implements OnInit {
  // [x: string]: any;
  //#region  agegroup and occupation
  inputFieldAgeFlag = false;
  inputFieldOccupationFlag = false;
  Age_ID: number = TargetBeneficiaryTypes_Enum.AgeGroup;
  Occupation_ID: number = TargetBeneficiaryTypes_Enum.Occupation;
  //#endregion
  // DATASOURCE
  AgeGroupList: TargetBeneficiaryModel[] = [];
  panelOpenState = false;
  OccupationList: any;

  ProjectSelectionList: SelectItem[];
  selectedprojectSelction: string[] = [];

  // expenseList: targetBeneficiaryModel[] = [];
  //#region "Variables"
  methodOfFundingList = [
    { Id: 1, Name: 'Sole' },
    { Id: 2, Name: 'Source/Co-finance' }
  ];

  financialHistory = [
    { Id: 1, Name: 'Good' },
    { Id: 2, Name: 'Neutral' },
    { Id: 3, Name: 'Bad' }
  ];

  sectorAndThemes = [
    {
      Id: 1,
      Name: 'Products'
    },
    { Id: 2, Name: 'Services' }
  ];

  isDisabledCriticism = true;
  isDisabledEligibilityCriteria: boolean;
  isDisableActivity: boolean;
  // isDisabledEligibilityCriteria = false;
  isDisabledCompensation = false;
  isDisabledAnyInKindComponent = true;
  isCostGreaterThanBudget = true;
  isDisabledTotal = true;
  isDisabledRisk = true;
  isDisabledReputation = true;
  isDisabledDelivery = true;
  IsOtherOrganizationalHarms = true;
  isOpportunityLoss = true;
  isAgeGroupEnable = false;
  isOccupationEnable = false;
  isPriorityOther = false;
  isFeasibilityExpert = false;
  startCriteriaEvaluationSubmitLoader = false;

  donorCEForm: DonorCEModel;
  eligibilityForm: EligibilityCEModel;
  feasibilityForm: FeasibilityCEModel;
  priorityForm: PriorityCEmodel;
  financialForm: FinancialProfitabilityModel;
  riskForm: RiskSecurityModel;
  productAndServiceForm: ProductAndServiceCEModel;
  projectSelctionForm: FinancialProjectDetailModel;
  IsSubmitCEform: ICEisCESubmitModel;


  ProjectId: any;
  CostOfCompensation: FormControl;
  TotalProjectActivity: any;
  submitButton: any;

  //#region //Count values
  // criteriaEvaluationTotal = 0;
  // methodOfFunding = 0;
  // pastFundingExperience = 0;
  // proposaAccept = 0;
  proposalExperiemce = '';



  //#region  Donor eligibility creteia variables
  onDonorELegibilityCrteria = 0;
  donorEligibilityDeadline = 0;
  donorEligibilityPartnership = 0;
  //#endregion

  //#region  feasibilty variable
  capacityAvailabilityProject = 0;
  compensationTrainStaff = 0;
  compensationByEquipment = 0;
  compensationExpandScope = 0;
  compensationGeographical = 0;
  allowedthirdParty = 0;
  costOfCompensation = 0;
  costOfCompensationMoney = 0;
  //#endregion

  //#region  any in kind component
  anyInKindComponent = 0;
  useableByOrganisation = 0;
  fesibilityExpertDeployed = 0;
  fesibilityExpert = 0;
  //#endregion

  //#region  feasibility right div
  enoughTimeQuality = 0;
  projectallowedByLaw = 0;
  projectAllowedByOrganisation = 0;
  isProjectPractical = 0;
  presenceCoverageinProject = 0;
  projectInLineOrganisational = 0;
  eNoughTimetoPrepare = 0;
  //#endregion

  //#region fessibility Cost efficiency
  // costGreaterThanBudget = 0;
  financialContribution = 0;
  // disablingSecurity = 0;
  disablingGeographicalCondition = 0;
  disablingSeasonalCondition = 0;

  //#endregion

  //#region Priority added value variables
  priorityIncresedEligibility = 0;
  priorityIncresedReputation = 0;
  priorityIncresedDonorRelation = 0;
  priorityGoodCause = 0;
  priorityImprovedPerformance = 0;
  priorityImprovedSkill = 0;
  priorityFillingFundingGap = 0;
  priorityNewSoftware = 0;
  priorityNewEquipment = 0;
  priorityCoverageAreaExp = 0;
  priorityNewTraining = 0;
  priorityOthers = 0;
  projectExpensionGoal = 0;

  //#endregion

  //#region financial Profitability form

  //#region Risk Security form
  // riskSecurity = 0;

  // riskReputation = 0;
  // focusDivertingRisk = 0;
  // deliveryFailure = 0;
  // financialLoses = 0;
  // opportunityLoses = 0;
  // opportunityDelay = 0;
  // otherOrganisationHarm = 0;

  //#endregion

  //#endregion

  //#region Flag

  // flags variables Feasibility
  capacityAvailableProject: boolean;
  isDisabled = true;
  projectactivity = 0;
  operational = 0;
  admin = 0;
  lump = 0;
  totalScore = 0;
  //#endregion

  //#region "other"
  priorityOtherList: IPriorityOtherModel[] = [];
  feasivilityList: IFeasibilityExpert[] = [];
  assumptionList: ICEAssumptionModel[] = [];
  ageGroupList: ICEAgeDEtailModel[] = [];
  occupatonList: ICEOccupationModel[] = [];
  donorEligibilityList: ICEDonorEligibilityModel[] = [];

  //#region "flag"
  // inputFieldOtherFeasibilityFlag = false;
  //#endregion
  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  //#region Input/Output
  // @Input() projectId: number;
  @Output() isCriteriaEvaluationFormSubmit = new EventEmitter();
  //#endregion

  constructor(
    private routeActive: ActivatedRoute,
    private appurl: AppUrlService,
    public criteriaEvalService: CriteriaEvaluationService,
    public toastr: ToastrService
  ) {
   }

  ngOnInit() {
    this.routeActive.parent.params.subscribe(params => {
      this.ProjectId = +params['id'];
    });
    this.initializeList();
    this.initilizeDonorEligibilityList();
    this.initDonorCEModel();
    this.initEligibilityModel();
    this.initFeasibilityModel();
    this.initPriorityModel();
    this.initFinancialProfitabilityModel();
    this.initRiskModel();
    this.initProductAndServiceModel();
    this.GetCriteraiEvaluationDetailById(this.ProjectId);
    this.CostOfCompensation = new FormControl('', [
      Validators.max(12),
      Validators.min(1)
    ]);
    this.GetAllProjectList();
    this.initProjectSelctionModel();
    this.getPriorityListByProjectId(this.ProjectId);
    this.getAssumptionByprojectId(this.ProjectId);
    this.GetAgegroupByProjectId(this.ProjectId);
    this.getFeasibilityExpertByProjectId(this.ProjectId);
    this.GetOccupationByProjectId(this.ProjectId);
    this.GetDonorEligibilityCriteriaByProjectId(this.ProjectId);
    this.initIsSubmitCE();
    this.getScreenSize();
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      'height': this.screenHeight - 190 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion


  //#region  Initilize model

  initDonorCEModel() {
    this.donorCEForm = {
      ProjectId: null,
      DonorCEId: null,
      MethodOfFunding: null,
      PastFundingExperience: null,
      ProposalAccepted: null,
      ProposalExperience: null,
      Professional: null,
      FundsOnTime: null,
      EffectiveCommunication: null,
      Dispute: null,
      OtherDeliverable: null,
      OtherDeliverableType: null,
      PastWorkingExperience: null,
      CriticismPerformance: null,
      TimeManagement: null,
      MoneyAllocation: null,
      Accountability: null,
      DeliverableQuality: null,
      DonorFinancingHistory: null,
      ReligiousStanding: null,
      PoliticalStanding: null
    };
  }

  initProductAndServiceModel() {
    this.productAndServiceForm = {
      ProjectId: null,
      ProductServiceId: null,
      Women: null,
      Children: null,
      Awareness: null,
      Education: null,
      DrugAbuses: null,
      Right: null,
      Culture: null,
      Music: null,
      Documentaries: null,
      InvestigativeJournalism: null,
      HealthAndNutrition: null,
      News: null,
      SocioPolitiacalDebate: null,
      Studies: null,
      Reports: null,
      CommunityDevelopment: null,
      Aggriculture: null,
      DRR: null,
      ServiceEducation: null,
      ServiceHealthAndNutrition: null,
      RadioProduction: null,
      TVProgram: null,
      PrintedMedia: null,
      RoundTable: null,
      Others: null,
      OtherActivity: null,
      TargetBenificaiaryWomen: null,
      TargetBenificiaryMen: null,
      TargetBenificiaryAgeGroup: null,
      TargetBenificiaryaOccupation: null,
      Product: null,
      Service: null
    };
  }

  initEligibilityModel() {
    this.eligibilityForm = {
      EligibilityId: 0,
      ProjectId: null,
      DonorCriteriaMet: null,
      EligibilityDealine: null,
      CoPartnership: null
    };
  }

  initFeasibilityModel() {
    this.feasibilityForm = {
      FeasibilityId: 0,
      ProjectId: null,
      CapacityAvailableForProject: null,
      TrainedStaff: null,
      ByEquipment: null,
      ExpandScope: null,
      GeoGraphicalPresence: null,

      ThirdPartyContract: null,
      CostOfCompensationMonth: 0,
      CostOfCompensationMoney: null,

      AnyInKindComponent: null,
      UseableByOrganisation: null,
      FeasibleExpertDeploy: null,
      FeasibilityExpert: null,

      EnoughTimeForProject: null,
      ProjectAllowedBylaw: null,
      ProjectByLeadership: null,
      IsProjectPractical: null,
      PresenceCoverageInProject: null,
      ProjectInLineWithOrgFocus: null,
      EnoughTimeToPrepareProposal: null,
      PerCostGreaterthenBudget: null,
      IsCostGreaterthenBudget: null,
    };
  }

  initPriorityModel() {
    this.priorityForm = {
      PriorityCriteriaDetailId: null,
      ProjectId: null,
      IncreaseEligibility: null,
      IncreaseReputation: null,
      ImproveDonorRelationship: null,
      GoodCause: null,
      ImprovePerformancecapacity: null,
      SkillImprove: null,
      FillingFundingGap: null,
      NewSoftware: null,
      NewEquipment: null,
      CoverageAreaExpansion: null,
      NewTraining: null,
      ExpansionGoal: null
    };
  }

  initFinancialProfitabilityModel() {
    this.financialForm = {
      FinancialCriteriaDetailId: null,
      ProjectId: null,
      ProjectActivities: 0,
      Operational: 0,
      Overhead_Admin: 0,
      Lump_Sum: 0,
      Total: 0
    };
  }

  initRiskModel() {
    this.riskForm = {
      RiskCriteriaDetailId: null,
      ProjectId: null,
      Security: null,
      Staff: null,
      ProjectAssets: null,
      Suppliers: null,
      Beneficiaries: null,
      OverallOrganization: null,
      DeliveryFaiLure: null,
      PrematureSeizure: null,
      GovernmentConfiscation: null,
      DesctructionByTerroristActivity: null,
      Reputation: null,
      Religious: null,
      Sectarian: null,
      Ethinc: null,
      Social: null,
      Traditional: null,
      FocusDivertingrisk: null,
      Financiallosses: null,
      Opportunityloss: null,
      ProjectSelectionId: null,
      Probablydelaysinfunding: null,
      OtherOrganizationalHarms: null,
      OrganizationalDescription: null
    };
  }
  // not used
  initProjectSelctionModel() {
    this.projectSelctionForm = {
      FinancialProjectDetailId: null,
      ProjectId: null,
      ProjectSelectionId: null,
      ProjectName: null
    };
  }
  //#endregion

  //#region "initializeList"
  initializeList() {
    this.priorityOtherList = [
      {
        PriorityOtherDetailId: null,
        Name: null,
        ProjectId: null,
        _IsDeleted: false,
        _IsLoading: false,
        _IsError: false
      },

    ];
  }

  initilizeDonorEligibilityList() {
    this.donorEligibilityList = [{
      DonorEligibilityDetailId: null,
      Name: null,
      ProjectId: null,

      _IsDeleted: false,
      _IsLoading: false,
      _IsError: false,
    }];
  }

  initIsSubmitCE() {
    this.IsSubmitCEform = {
      ProjectId: null,

      IsCriteriaEvaluationSubmit: false
    };
  }
  //#endregion



  //#region DonorForm section
  onMethofOfFundingChange(value) {

    // if (value == 1) {
    // this.methodOfFunding = criteriaEvaluationScores.methodOfFunding_Sole
    // } else {
    // this.methodOfFunding = criteriaEvaluationScores.methodOfFunding_Source

    // }

    this.donorCEForm.MethodOfFunding = value;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  //  DonorForm section
  onPastFundingExperienceChange(value) {
    if (value.checked === true) {
      this.isDisabled = false;
      // this.pastFundingExperience = criteriaEvaluationScores.pastFundingExperience_Yes
    } else {
      this.isDisabled = true;
      // this.pastFundingExperience = criteriaEvaluationScores.pastFundingExperience_No
      this.donorCEForm.ProposalAccepted = false;
      this.donorCEForm.ProposalExperience = null;
      this.donorCEForm.Professional = false;
      this.donorCEForm.FundsOnTime = false;
      this.donorCEForm.EffectiveCommunication = false;
      this.donorCEForm.Dispute = false;
      this.donorCEForm.OtherDeliverable = false;
      this.donorCEForm.OtherDeliverableType = null;
      this.donorCEForm.ProjectId = this.ProjectId;
      this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
      // this.proposaAccept = 0;
      // this.professional = 0;
      // this.fundsOnTime = 0;
      // this.effectiveCommunication = 0;
      // this.disputes = 0;
      // this.otherDeliverables = 0;
    }
    this.donorCEForm.PastFundingExperience = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onProposalAcceptChange(value) {
    // if (value.checked === true) {
    //   this.proposaAccept = criteriaEvaluationScores.proposalExp_Disputes_Yes
    // } else {
    //   this.proposaAccept = criteriaEvaluationScores.proposalExp_Disputes_No
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.ProposalAccepted = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  onProposalExperiemceChange(value) {
    if (value.value === 'pre') {
      this.donorCEForm.ProposalExperience = true;
      this.proposalExperiemce = criteriaEvaluationScores.pre_ProposalExperience;
    } else if (value.value === 'post') {
      this.donorCEForm.ProposalExperience = false;
      this.proposalExperiemce =
        criteriaEvaluationScores.post_ProposalExperience;
    }

    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onProfessionalChange(value) {
    // if (value.checked === true) {
    //   this.professional = criteriaEvaluationScores.proposalExp_Professional_Yes
    // } else {
    //   this.professional = criteriaEvaluationScores.proposalExp_Professional_No
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.Professional = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onFundsOnTimeChange(value) {
    // if (value.checked === true) {
    //   this.fundsOnTime = criteriaEvaluationScores.proposalExp_FundsOnTime_Yes
    // } else {
    //   this.fundsOnTime = criteriaEvaluationScores.proposalExp_Professional_No
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.FundsOnTime = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  onEffectiveCommunicationChange(value) {
    // if (value.checked === true) {
    //   this.effectiveCommunication = criteriaEvaluationScores.proposalExp_EffectiveCommunication_Yes
    // } else {
    //   this.effectiveCommunication = criteriaEvaluationScores.proposalExp_EffectiveCommunication_No
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.EffectiveCommunication = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  onDisputesChange(value) {
    // if (value.checked === true) {
    //   this.disputes = criteriaEvaluationScores.proposalExp_EffectiveCommunication_Yes
    // } else {
    //   this.disputes = criteriaEvaluationScores.proposalExp_EffectiveCommunication_No
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.Dispute = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onOtherDeliverablesChange(value) {
    // if (value.checked === true) {
    //   this.otherDeliverables = criteriaEvaluationScores.proposalExp_OtherDeliverable_Yes
    // } else {
    //   this.otherDeliverables = criteriaEvaluationScores.proposalExp_OtherDeliverable_No
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.OtherDeliverable = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onOtherTypeDeliverablesChange(ev, data: any) {
    if ((ev = 'OtherDeliverableType')) {
      if (data != null) {
        this.donorCEForm.OtherDeliverableType = data;
      } else {
      }
    }
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onPastWorkExperienceChange(value) {

    if (value.checked === true) {
      this.isDisabledCriticism = false;
      // this.pastWorkExperience = criteriaEvaluationScores.pastWorkExperoence_Yes
    } else {
      this.isDisabledCriticism = true;
      // this.pastWorkExperience = criteriaEvaluationScores.pastWorkExperoence_No
      this.donorCEForm.CriticismPerformance = false;
      this.donorCEForm.TimeManagement = false;
      this.donorCEForm.MoneyAllocation = false;
      this.donorCEForm.Accountability = false;
      this.donorCEForm.DeliverableQuality = false;
      // this.criticismPerformance = 0;
      // this.criticismTimeManagement = 0;
      // this.criticismMoneyAllocation = 0;
      // this.criticismAccountability = 0;
      // this.criticismDeliverableQuality = 0;
    }
    this.donorCEForm.PastWorkingExperience = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onPerformanceChange(value) {
    if (value.checked === true) {
      // this.criticismPerformance = criteriaEvaluationScores.pastCriticismPerfomance_Yes
    } else {
      // this.criticismPerformance = criteriaEvaluationScores.pastCriticismPerfomance_No
    }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.CriticismPerformance = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onTimeManagementChange(value) {
    if (value.checked === true) {
      // this.criticismTimeManagement = criteriaEvaluationScores.pastCriticismTimeManagement_Yes
    } else {
      // this.criticismTimeManagement = criteriaEvaluationScores.pastCriticismTimeManagement_No
    }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.TimeManagement = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onMoneyAllocationChange(value) {
    if (value.checked === true) {
      // this.criticismMoneyAllocation = criteriaEvaluationScores.pastCriticismMoneyAllocation_Yes
    } else {
      // this.criticismMoneyAllocation = criteriaEvaluationScores.pastCriticismMoneyAllocation_No
    }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.MoneyAllocation = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onAccountabilityChange(value) {
    if (value.checked === true) {
      // this.criticismAccountability = criteriaEvaluationScores.pastCriticismAccountability_Yes
    } else {
      // this.criticismAccountability = criteriaEvaluationScores.pastCriticismAccountability_No
    }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.Accountability = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onDeliverableQualityChange(value) {
    // if (value.checked === true) {
    //   this.criticismDeliverableQuality = criteriaEvaluationScores.pastCriticismQualityDeliverable_Yes
    // } else {
    //   this.criticismDeliverableQuality = criteriaEvaluationScores.pastCriticismQualityDeliverable_No
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.DeliverableQuality = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onFinancialHistoryChange(data) {
    // if (data === 1) {
    //   this.donorFinancialHistory = criteriaEvaluationScores.finanacingHistory_Good
    // }
    // else if (data === 2) {
    //   this.donorFinancialHistory = criteriaEvaluationScores.finanacingHistory_Neutral
    // }
    // else if (data === 3) {
    //   this.donorFinancialHistory = criteriaEvaluationScores.finanacingHistory_Bad
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.DonorFinancingHistory = data;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onReligiousStandingChange(data) {
    // if (data === 1) {
    //   this.donorReligiousStanding = criteriaEvaluationScores.religiousStanding_Good
    // }
    // else if (data === 2) {
    //   this.donorReligiousStanding = criteriaEvaluationScores.religiousStanding_Neutral

    // }
    // else if (data === 3) {
    //   this.donorReligiousStanding = criteriaEvaluationScores.religiousStanding_Bad
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.ReligiousStanding = data;
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  onPoliticalStandingChange(data) {
    // if (data === 1) {
    //   this.donorPoliticalStanding = criteriaEvaluationScores.politicalStanding_Good
    // }
    // else if (data === 2) {
    //   this.donorPoliticalStanding = criteriaEvaluationScores.politicalStanding_Neutral
    // }
    // else if (data === 3) {
    //   this.donorPoliticalStanding = criteriaEvaluationScores.politicalStanding_Bad
    // }
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.PoliticalStanding = data;
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  //#endregion

  //#region Purpose Of Initiating
  OnProductChange(ev) {
    if (ev === 'product') {
      this.productAndServiceForm.Product = !this.productAndServiceForm.Product;
      this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
    } else if (ev === 'service') {
      this.productAndServiceForm.Service = !this.productAndServiceForm.Service;
      this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
    }
  }

  onProdWomenChange(value) {
    // if (value.checked === true) {
    //   this.prodSelectWomen = criteriaEvaluationScores.prodWomen_Yes
    // } else {
    //   this.prodSelectWomen = criteriaEvaluationScores.prodWomen_No
    // }
    this.productAndServiceForm.Women = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdChildrenChange(value) {
    // if (value.checked === true) {
    //   this.prodSelectChildren = criteriaEvaluationScores.prodWomen_Yes
    // } else {
    //   this.prodSelectChildren = criteriaEvaluationScores.prodWomen_No
    // }
    this.productAndServiceForm.Children = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdAwarenessChange(value) {
    // if (value.checked === true) {
    //   this.prodProdAwareness = criteriaEvaluationScores.prodAwareness_Yes
    // } else {
    //   this.prodProdAwareness = criteriaEvaluationScores.prodEducation_No
    // }
    this.productAndServiceForm.Awareness = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onProdEducationChange(value) {
    // if (value.checked === true) {
    //   this.prodProdEducation = criteriaEvaluationScores.prodEducation_Yes
    // } else {
    //   this.prodProdEducation = criteriaEvaluationScores.prodEducation_No
    // }
    this.productAndServiceForm.Education = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onDrugAbuseChange(value) {
    // if (value.checked === true) {
    //   this.prodDrugAbuse = criteriaEvaluationScores.prodDrugAndAbuse_Yes
    // } else {
    //   this.prodDrugAbuse = criteriaEvaluationScores.prodDrugAndAbuse_No
    // }
    this.productAndServiceForm.DrugAbuses = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onProdRightsChange(value) {
    // if (value.checked === true) {
    //   this.prodRights = criteriaEvaluationScores.prodRights_Yes
    // } else {
    //   this.prodRights = criteriaEvaluationScores.prodRights_No
    // }
    this.productAndServiceForm.Right = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdCultureChange(value) {
    // if (value.checked === true) {
    //   this.prodCulture = criteriaEvaluationScores.prodRights_Yes
    // } else {
    //   this.prodCulture = criteriaEvaluationScores.prodRights_No
    // }
    this.productAndServiceForm.Culture = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onMusicChange(value) {
    // if (value.checked === true) {
    //   this.prodMusic = criteriaEvaluationScores.prodMusic_Yes
    // } else {
    //   this.prodMusic = criteriaEvaluationScores.prodMusic_No
    // }
    this.productAndServiceForm.Music = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdocumentariesChange(value) {
    // if (value.checked === true) {
    //   this.prodDocumentaries = criteriaEvaluationScores.prodDocumentaries_Yes
    // } else {
    //   this.prodDocumentaries = criteriaEvaluationScores.prodDocumentaries_Yes
    // }
    this.productAndServiceForm.Documentaries = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onProdJournalismChange(value) {
    // if (value.checked === true) {
    //   this.prodJournalism = criteriaEvaluationScores.prodInvestigativeJournlism_Yes
    // } else {
    //   this.prodJournalism = criteriaEvaluationScores.prodInvestigativeJournlism_No
    // }
    this.productAndServiceForm.InvestigativeJournalism = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdHealthNutritionChange(value) {
    // if (value.checked === true) {
    //   this.prodProdHealthNutrition = criteriaEvaluationScores.prodHealthAndNutrition_Yes
    // } else {
    //   this.prodProdHealthNutrition = criteriaEvaluationScores.prodHealthAndNutrition_No
    // }
    this.productAndServiceForm.HealthAndNutrition = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdNewsChange(value) {
    // if (value.checked === true) {
    //   this.prodProdNews = criteriaEvaluationScores.prodNews_Yes
    // } else {
    //   this.prodProdNews = criteriaEvaluationScores.prodNews_No
    // }
    this.productAndServiceForm.News = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdJSocioPoliticalChange(value) {
    // if (value.checked === true) {
    //   this.prodSocioPolitical = criteriaEvaluationScores.prodSocioPoliticalDebate_Yes
    // } else {
    //   this.prodSocioPolitical = criteriaEvaluationScores.prodSocioPoliticalDebate_No
    // }
    this.productAndServiceForm.SocioPolitiacalDebate = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdStudiesChange(value) {
    // if (value.checked === true) {
    //   this.prodStudies = criteriaEvaluationScores.prodStudies_Yes
    // } else {
    //   this.prodStudies = criteriaEvaluationScores.prodStudies_No
    // }
    this.productAndServiceForm.Studies = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdReportsChange(value) {
    // if (value.checked === true) {
    //   this.prodReports = criteriaEvaluationScores.prodReport_Yes
    // } else {
    //   this.prodReports = criteriaEvaluationScores.prodReport_No
    // }
    this.productAndServiceForm.Reports = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  // Service
  onServiCommunityChange(value) {
    // if (value.checked === true) {
    //   this.serviCommunity = criteriaEvaluationScores.servCommunityDevelop_Yes
    // } else {
    //   this.serviCommunity = criteriaEvaluationScores.servCommunityDevelop_No
    // }
    this.productAndServiceForm.CommunityDevelopment = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onServAggricultureChange(value) {
    // if (value.checked === true) {
    //   this.serviAggriculture = criteriaEvaluationScores.servAggriculture_Yes
    // } else {
    //   this.serviAggriculture = criteriaEvaluationScores.servAggriculture_No
    // }
    this.productAndServiceForm.Aggriculture = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onServDRRChange(value) {
    // if (value.checked === true) {
    //   this.serviDRR = criteriaEvaluationScores.serDRR_Yes
    // } else {
    //   this.serviDRR = criteriaEvaluationScores.serDRR_No
    // }
    this.productAndServiceForm.DRR = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onServEducationChange(value) {
    // if (value.checked === true) {
    //   this.serviEducation = criteriaEvaluationScores.serviceEducation_Yes
    // } else {
    //   this.serviEducation = criteriaEvaluationScores.serviceEducation_No
    // }
    this.productAndServiceForm.ServiceEducation = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onServHealthNutrityionChange(value) {
    // if (value.checked === true) {
    //   this.servihealthNutrition = criteriaEvaluationScores.serviceEducation_Yes
    // } else {
    //   this.servihealthNutrition = criteriaEvaluationScores.serviceEducation_No
    // }
    this.productAndServiceForm.ServiceHealthAndNutrition = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  // activities
  onActivityRadioChange(value) {
    // if (value.checked === true) {
    //   this.activityRadio = criteriaEvaluationScores.activityRadioProduction_Yes
    // } else {
    //   this.activityRadio = criteriaEvaluationScores.activityRadioProduction_No
    // }
    this.productAndServiceForm.RadioProduction = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onActivityTVProgramChange(value) {
    // if (value.checked === true) {
    //   this.activityTVprogram = criteriaEvaluationScores.activityTVprogram_Yes
    // } else {
    //   this.activityTVprogram = criteriaEvaluationScores.activityTVprogram_No
    // }
    this.productAndServiceForm.TVProgram = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onActivityPrtintedMediaChange(value) {
    // if (value.checked === true) {
    //   this.activityPrintedMedia = criteriaEvaluationScores.activityPrintedMedia_Yes
    // } else {
    //   this.activityPrintedMedia = criteriaEvaluationScores.activityPrintedMedia_No
    // }
    this.productAndServiceForm.PrintedMedia = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onActivityRoundTableChange(value) {
    // if (value.checked === true) {
    //   this.activityRoundTable = criteriaEvaluationScores.activityRoundTables_Yes
    // } else {
    //   this.activityRoundTable = criteriaEvaluationScores.activityRoundTables_No
    // }
    this.productAndServiceForm.RoundTable = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onActivityOthersChange(value) {
    if (value.checked === true) {
      this.isDisableActivity = false;
      //   this.activityOtherChangee = criteriaEvaluationScores.activityOther_Yes
    } else {
      this.isDisableActivity = true;
      this.productAndServiceForm.OtherActivity = null;
      //   this.activityOtherChangee = criteriaEvaluationScores.activityOther_No
    }
    this.productAndServiceForm.Others = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onOtherTypeActivityChange(ev, data: any) {
    if (data != null && data !== undefined && data !== '') {
      if (ev === 'otherActivity') {
        this.productAndServiceForm.OtherActivity = data;
        this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
      }
    }
  }

  onTagetBenificiaryWomenChange(value) {
    this.productAndServiceForm.TargetBenificaiaryWomen = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onTargetBeneficaryMenChange(value) {
    this.productAndServiceForm.TargetBenificiaryMen = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onTargetBeneficaryAgeGroupChange(value) {
    if (value.checked === true) {
      this.isAgeGroupEnable = true;
    } else {
      this.isAgeGroupEnable = false;
    }
    this.productAndServiceForm.TargetBenificiaryAgeGroup = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onTargetBenificaryOccupationChange(value) {
    if (value.checked) {
      this.isOccupationEnable = true;
    } else {
      this.isOccupationEnable = false;
    }
    this.productAndServiceForm.TargetBenificiaryaOccupation = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  // ELIGIBILITY
  onDonorEligibiltyCriteraChange(value) {
    if (value.checked === true) {
      this.isDisabledEligibilityCriteria = true;
      this.onDonorELegibilityCrteria =
        criteriaEvaluationScores.onDonorELegibilityCrteria_Yes;
      this.eligibilityForm.EligibilityDealine = false;
      this.eligibilityForm.CoPartnership = false;
      this.donorEligibilityDeadline = 0;
      this.donorEligibilityPartnership = 0;

    } else {
      this.isDisabledEligibilityCriteria = false;
      this.onDonorELegibilityCrteria =
        criteriaEvaluationScores.onDonorELegibilityCrteria_No;

    }

    this.eligibilityForm.DonorCriteriaMet = value.checked;
    this.eligibilityForm.EligibilityId = this.eligibilityForm.EligibilityId;
    this.AddEditEligibilityCEForm(this.eligibilityForm);
  }

  onDonorEligibilityDeadlineChange(value) {
    if (value.checked === true) {
      this.donorEligibilityDeadline =
        criteriaEvaluationScores.donorEligibilityDeadline_Yes;
    } else {
      this.donorEligibilityDeadline =
        criteriaEvaluationScores.donorEligibilityDeadline_No;
    }
    this.eligibilityForm.EligibilityDealine = value.checked;
    this.eligibilityForm.EligibilityId = this.eligibilityForm.EligibilityId;
    this.AddEditEligibilityCEForm(this.eligibilityForm);
  }

  onDonorEligibilityPartnershipChange(value) {
    if (value.checked === true) {
      this.donorEligibilityPartnership =
        criteriaEvaluationScores.feasibilityCapacityForProject_Yes;
    } else {
      this.donorEligibilityPartnership =
        criteriaEvaluationScores.feasibilityCapacityForProject_No;
    }
    this.eligibilityForm.CoPartnership = value.checked;
    this.eligibilityForm.EligibilityId = this.eligibilityForm.EligibilityId;
    this.AddEditEligibilityCEForm(this.eligibilityForm);
  }
  //#endregion

  //#region  Feasibility
  onCapacityAvailabilityChange(value) {
    if (value.checked === true) {
      this.isDisabledCompensation = true;
      this.feasibilityForm.TrainedStaff = false;
      this.feasibilityForm.ByEquipment = false;
      this.feasibilityForm.ExpandScope = false;
      this.feasibilityForm.GeoGraphicalPresence = false;
      this.compensationTrainStaff = 0;
      this.compensationByEquipment = 0;
      this.compensationExpandScope = 0;
      this.compensationGeographical = 0;
      this.capacityAvailabilityProject =
        criteriaEvaluationScores.pastFundingExperience_Yes;
    } else {
      this.isDisabledCompensation = false;
      this.capacityAvailabilityProject =
        criteriaEvaluationScores.pastFundingExperience_No;
    }
    this.feasibilityForm.CapacityAvailableForProject = value.checked;
    this.feasibilityForm.FeasibilityId = this.feasibilityForm.FeasibilityId;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onCompensationTrainStaffChange(value) {
    if (value.checked === true) {
      this.compensationTrainStaff =
        criteriaEvaluationScores.compensationTrainedStaff_Yes;
    } else {
      this.compensationTrainStaff =
        criteriaEvaluationScores.compensationTrainedStaff_No;
    }
    this.feasibilityForm.TrainedStaff = value.checked;
    this.feasibilityForm.FeasibilityId = this.feasibilityForm.FeasibilityId;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onCompensationByEquipmentChange(value) {
    if (value.checked === true) {
      this.compensationByEquipment =
        criteriaEvaluationScores.compensateByEquipment_Yes;
    } else {
      this.compensationByEquipment =
        criteriaEvaluationScores.compensateByEquipment_No;
    }
    this.feasibilityForm.ByEquipment = value.checked;
    this.feasibilityForm.FeasibilityId = this.feasibilityForm.FeasibilityId;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  oncompensationExpandScopeChange(value) {
    if (value.checked === true) {
      this.compensationExpandScope =
        criteriaEvaluationScores.compensateExpandScope_Yes;
    } else {
      this.compensationExpandScope =
        criteriaEvaluationScores.compensateExpandScope_No;
    }
    this.feasibilityForm.ExpandScope = value.checked;
    this.feasibilityForm.FeasibilityId = this.feasibilityForm.FeasibilityId;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onCompensationGeographicalChange(value) {
    if (value.checked === true) {
      this.compensationGeographical =
        criteriaEvaluationScores.compensateGeographical_Yes;
    } else {
      this.compensationGeographical =
        criteriaEvaluationScores.compensateGeographical_No;
    }
    this.feasibilityForm.GeoGraphicalPresence = value.checked;
    this.feasibilityForm.FeasibilityId = this.feasibilityForm.FeasibilityId;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onallowedthirdPartyChange(value) {
    if (value.checked === true) {
      this.allowedthirdParty =
        criteriaEvaluationScores.allowedThirdPartyContract_Yes;
    } else {
      this.allowedthirdParty =
        criteriaEvaluationScores.allowedThirdPartyContract_No;
    }
    this.feasibilityForm.ThirdPartyContract = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onCostOfCompensationChange(ev, data: any) {
    if (data <= 12 && data != null && data !== undefined && data !== '') {
      if (ev === 'CostOfCompensation') {
        this.feasibilityForm.CostOfCompensationMonth = data;
        // this.costOfCompensation = -1 * data;
        this.AddEditFeasibilityCEForm(this.feasibilityForm);
      }
    }
  }
  onCostOfCompensationMoneyChange(ev, data: any) {
    // if (data != null && data != undefined && data != "" && data >= 5000) {
    if (ev === 'CostOfCompensationMoney') {
      this.feasibilityForm.CostOfCompensationMoney = data;
      // this.costOfCompensationMoney = -1 * data / 5000;
      this.AddEditFeasibilityCEForm(this.feasibilityForm);
    }
    // }
  }
  //#endregion

  //#region AnyInKind component
  onAnyInKindComponentChange(value) {
    if (value.checked === true) {
      this.isDisabledAnyInKindComponent = false;
      this.anyInKindComponent = criteriaEvaluationScores.anyInKindComponent_Yes;
    } else {
      this.isDisabledAnyInKindComponent = true;
      this.feasibilityForm.UseableByOrganisation = false;
      this.feasibilityForm.FeasibleExpertDeploy = false;
      this.useableByOrganisation = 0;
      this.fesibilityExpertDeployed = 0;
      this.anyInKindComponent = criteriaEvaluationScores.anyInKindComponent_No;
    }
    this.feasibilityForm.AnyInKindComponent = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onUseableByOrganisationChange(value) {
    if (value.checked === true) {
      this.useableByOrganisation =
        criteriaEvaluationScores.useableByOrganisation_Yes;
    } else {
      this.useableByOrganisation =
        criteriaEvaluationScores.useableByOrganisation_No;
    }
    this.feasibilityForm.UseableByOrganisation = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onFeasibleExpertDeployedChange(value) {
    if (value.checked === true) {
      this.isFeasibilityExpert = true;

    } else {
      this.isFeasibilityExpert = false;
    }
    this.feasibilityForm.FeasibleExpertDeploy = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }
  // add plus icon functionationality pending
  onExpertsChange(value) {
    if (value.checked === true) {
      this.fesibilityExpert = criteriaEvaluationScores.feasibilityExpert_Yes;
    } else {
      this.fesibilityExpert = criteriaEvaluationScores.feasibilityExpert_No;
    }
    this.feasibilityForm.FeasibilityExpert = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }
  // feasibility right div
  onEnoughTimeQualityChange(value) {
    if (value.checked === true) {
      this.enoughTimeQuality =
        criteriaEvaluationScores.enoughTimeForQualityWork_Yes;
    } else {
      // this.toastr.warning(
      //   'ratio of *(time delay) over total time should be less than 0.33))'
      // );
      this.enoughTimeQuality =
        criteriaEvaluationScores.enoughTimeForQualityWork_No;
    }

    this.feasibilityForm.EnoughTimeForProject = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onProjectallowedByLawChange(value) {
    if (value.checked === true) {
      this.projectallowedByLaw =
        criteriaEvaluationScores.projectAllowedByLaw_Yes;
    } else {
      // this.toastr.warning('Project should be allowed by law');
      this.projectallowedByLaw =
        criteriaEvaluationScores.projectAllowedByLaw_No;
    }
    this.feasibilityForm.ProjectAllowedBylaw = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }
  onProjectAllowedByOrganisationChange(value) {
    if (value.checked === true) {
      this.projectAllowedByOrganisation =
        criteriaEvaluationScores.projectAllowByOrganisation_Yes;
    } else {
      this.projectAllowedByOrganisation =
        criteriaEvaluationScores.projectAllowByOrganisation_No;
    }
    this.feasibilityForm.ProjectByLeadership = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onIsProjectPracticalChange(value) {
    if (value.checked === true) {
      this.isProjectPractical = criteriaEvaluationScores.isProjectPractical_Yes;
    } else {
      this.isProjectPractical = criteriaEvaluationScores.isProjectPractical_No;
    }
    this.feasibilityForm.IsProjectPractical = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onPresenceCoverageinProjectChange(value) {
    if (value.checked === true) {
      this.presenceCoverageinProject =
        criteriaEvaluationScores.presenceCoverage_Yes;
    } else {
      this.presenceCoverageinProject =
        criteriaEvaluationScores.presenceCoverage_No;
    }
    this.feasibilityForm.PresenceCoverageInProject = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onProjectInLineOrganisationalChange(value) {
    if (value.checked === true) {
      this.projectInLineOrganisational =
        criteriaEvaluationScores.projectinLineWithFocus_Yes;
    } else {
      this.projectInLineOrganisational =
        criteriaEvaluationScores.projectinLineWithFocus_No;
    }
    this.feasibilityForm.ProjectInLineWithOrgFocus = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onENoughTimetoPrepareChange(value) {
    if (value.checked === true) {
      this.eNoughTimetoPrepare =
        criteriaEvaluationScores.enoughTimeToPrepareproposal_Yes;
    } else {
      // this.toastr.warning(
      //   'Minimum time to submit the proposal for old Project is 2 weeks'
      // );

      this.eNoughTimetoPrepare =
        criteriaEvaluationScores.enoughTimeToPrepareproposal_No;
    }
    this.feasibilityForm.EnoughTimeToPrepareProposal = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  //#region feasibility Cost efficiency
  onCostGreaterThanBudgetChange(value) {
    if (value.checked === true) {
      this.isCostGreaterThanBudget = false;

      // this.costGreaterThanBudget =
      // criteriaEvaluationScores.costGreaterThanBudget_Yes;
    } else {
      this.isCostGreaterThanBudget = true;
      this.feasibilityForm.PerCostGreaterthenBudget = 0;
      // this.costGreaterThanBudget =
      // criteriaEvaluationScores.costGreaterThanBudget_No;
    }
    this.feasibilityForm.IsCostGreaterthenBudget = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onProjectRealCostChange(data: any) {
    if (data != null && data !== undefined && data !== '') {
      this.feasibilityForm.ProjectRealCost = data;
      this.AddEditFeasibilityCEForm(this.feasibilityForm);
    }
  }

  onCostGreaterBudgetChange(data: any) {
    if (data != null && data !== undefined && data !== '') {
      this.feasibilityForm.PerCostGreaterthenBudget = data;
      this.AddEditFeasibilityCEForm(this.feasibilityForm);
    }
  }
  onFinancialContributionChange(value) {
    if (value.checked === true) {
      this.financialContribution =
        criteriaEvaluationScores.financialCopntributionFulfil_Yes;
    } else {
      this.financialContribution =
        criteriaEvaluationScores.financialCopntributionFulfil_No;
    }
    this.feasibilityForm.IsFinancialContribution = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }
  // disabling conditions
  onSecurityChange(value) {
    if (value.checked === true) {
      // this.disablingSecurity = criteriaEvaluationScores.disablingSecurity_Yes;
    } else {
      // this.disablingSecurity = criteriaEvaluationScores.disablingSecurity_No;
    }
    this.feasibilityForm.IsSecurity = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onGeographicalChange(value) {
    if (value.checked === true) {
      this.disablingGeographicalCondition =
        criteriaEvaluationScores.disablingGeographical_Yes;
    } else {
      this.disablingGeographicalCondition =
        criteriaEvaluationScores.disablingGeographical_No;
    }
    this.feasibilityForm.IsGeographical = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onSeasonalChange(value) {
    if (value.checked === true) {
      this.disablingSeasonalCondition =
        criteriaEvaluationScores.disablingSeasonal_Yes;
    } else {
      this.disablingSeasonalCondition =
        criteriaEvaluationScores.disablingSeasonal_No;
    }
    this.feasibilityForm.IsSeasonal = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  //#endregion

  //#region Priority CEForm
  onPIncresedEligibilityChange(value) {
    if (value.checked === true) {
      this.priorityIncresedEligibility =
        criteriaEvaluationScores.increasedEligibility_Yes;
    } else {
      this.priorityIncresedEligibility =
        criteriaEvaluationScores.increasedEligibility_No;
    }
    this.priorityForm.IncreaseEligibility = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onIncreasedReputationChange(value) {
    if (value.checked === true) {
      this.priorityIncresedReputation =
        criteriaEvaluationScores.increasedReputation_Yes;
    } else {
      this.priorityIncresedReputation =
        criteriaEvaluationScores.increasedReputation_No;
    }
    this.priorityForm.IncreaseReputation = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onImprovedDonorRelationChange(value) {
    if (value.checked === true) {
      this.priorityIncresedDonorRelation =
        criteriaEvaluationScores.improvedDonorRelationaship_Yes;
    } else {
      this.priorityIncresedDonorRelation =
        criteriaEvaluationScores.improvedDonorRelationaship_No;
    }
    this.priorityForm.ImproveDonorRelationship = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onGoodCauseChange(value) {
    if (value.checked === true) {
      this.priorityGoodCause = criteriaEvaluationScores.goodCause_Yes;
    } else {
      this.priorityGoodCause = criteriaEvaluationScores.goodCause_No;
    }
    this.priorityForm.GoodCause = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onImprovedPerformanceChange(value) {
    if (value.checked === true) {
      this.priorityImprovedPerformance =
        criteriaEvaluationScores.improvedPerformanceCapacity_Yes;
    } else {
      this.priorityImprovedPerformance =
        criteriaEvaluationScores.improvedPerformanceCapacity_No;
    }
    this.priorityForm.ImprovePerformancecapacity = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onSkillImprovementChange(value) {
    if (value.checked === true) {
      this.priorityImprovedSkill =
        criteriaEvaluationScores.skillImprovement_Yes;
    } else {
      this.priorityImprovedSkill = criteriaEvaluationScores.skillImprovement_No;
    }
    this.priorityForm.SkillImprove = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onFillingFundingGapChange(value) {
    if (value.checked === true) {
      this.priorityFillingFundingGap =
        criteriaEvaluationScores.fillingfundingGaps_Yes;
    } else {
      this.priorityFillingFundingGap =
        criteriaEvaluationScores.fillingfundingGaps_No;
    }
    this.priorityForm.FillingFundingGap = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onNewSoftwareChange(value) {
    if (value.checked === true) {
      this.priorityNewSoftware = criteriaEvaluationScores.newSoftware_Yes;
    } else {
      this.priorityNewSoftware = criteriaEvaluationScores.newSoftware_No;
    }
    this.priorityForm.NewSoftware = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onNewEquipmentChange(value) {
    if (value.checked === true) {
      this.priorityNewEquipment = criteriaEvaluationScores.newEquipment_Yes;
    } else {
      this.priorityNewEquipment = criteriaEvaluationScores.newEquipment_No;
    }
    this.priorityForm.NewEquipment = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onCoverageAreaExpensionChange(value) {
    if (value.checked === true) {
      this.priorityCoverageAreaExp =
        criteriaEvaluationScores.coverageAreaExpansion_Yes;
    } else {
      this.priorityCoverageAreaExp =
        criteriaEvaluationScores.coverageAreaExpansion_No;
    }
    this.priorityForm.CoverageAreaExpansion = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onNewTrainingChange(value) {
    if (value.checked === true) {
      this.priorityNewTraining = criteriaEvaluationScores.newTraining_Yes;
    } else {
      this.priorityNewTraining = criteriaEvaluationScores.newTraining_No;
    }
    this.priorityForm.NewTraining = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onPriorityOthersChange(value) {
    if (value.checked === true) {
      this.isPriorityOther = true;
      //   this.priorityOthers = criteriaEvaluationScores.other_Yes
    } else {
      this.isPriorityOther = false;
      // this.priorityOthers = criteriaEvaluationScores.other_No
    }
    this.priorityForm.Others = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onProjectExpensionGoalChange(value) {
    if (value.checked === true) {
      this.projectExpensionGoal =
        criteriaEvaluationScores.projectInlineWithOrganisalGoal_Yes;
    } else {
      // this.toastr.warning('Out of strategic goal');
      this.projectExpensionGoal =
        criteriaEvaluationScores.projectInlineWithOrganisalGoal_No;
    }
    this.priorityForm.ExpansionGoal = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }
  //#endregion

  //#region Financial profitability form
  onProjectActivityChange(ev, data: any) {

    if (data != null && data !== '' && data !== undefined) {
      // var total = data;
      if (ev === 'projectActivity') {
        this.financialForm.ProjectActivities = data;
        this.projectactivity = data;
        this.AddEditFinancialProfitability(this.financialForm);
      }
      if (ev === 'operational') {
        this.financialForm.Operational = data;
        this.operational = data;
        this.AddEditFinancialProfitability(this.financialForm);
      }
      if (ev === 'overheadAdmin') {
        this.admin = data;
        this.financialForm.Overhead_Admin = data;
        this.AddEditFinancialProfitability(this.financialForm);
      }
      if (ev === 'lumpsum') {
        this.lump = data;
        this.financialForm.Lump_Sum = data;
        this.AddEditFinancialProfitability(this.financialForm);
      }
    }
  }

  //#endregion

  //#region Risk Security form
  onRiskSecurityChange(value) {
    if (value.checked === true) {
      this.isDisabledRisk = false;
      // this.riskSecurity = criteriaEvaluationScores.riskSecurity_Yes
    } else {
      this.isDisabledRisk = true;
      this.riskForm.Staff = false;
      this.riskForm.ProjectAssets = false;
      this.riskForm.Suppliers = false;
      this.riskForm.Beneficiaries = false;
      this.riskForm.OverallOrganization = false;
      // this.riskSecurity = criteriaEvaluationScores.riskSecurity_No
    }
    this.riskForm.Security = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }

  onRiskChildChange(ev, data: any) {
    if (data != null && data !== '' && data !== undefined) {
      if (ev === 'Staff') {
        this.riskForm.Staff = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Assets') {
        this.riskForm.ProjectAssets = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Suppliers') {
        this.riskForm.Suppliers = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Beneficiaries') {
        this.riskForm.Beneficiaries = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'OverallOrganization') {
        this.riskForm.OverallOrganization = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
    }
  }

  onRiskReputationChange(value) {
    if (value.checked === true) {
      this.isDisabledReputation = false;
      // this.riskReputation = criteriaEvaluationScores.riskSecurity_Yes
    } else {
      this.isDisabledReputation = false;
      this.riskForm.Religious = false;
      this.riskForm.Sectarian = false;
      this.riskForm.Ethinc = false;
      this.riskForm.Social = false;
      this.riskForm.Traditional = false;
      this.riskForm.FocusDivertingrisk = false;
      // this.riskReputation = criteriaEvaluationScores.riskSecurity_No
    }
    this.riskForm.Reputation = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }

  onReputationChildChange(ev, data: any) {
    if (data != null && data !== '' && data !== undefined) {
      if (ev === 'Religious') {
        this.riskForm.Religious = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Sectarian') {
        this.riskForm.Sectarian = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Ethnic') {
        this.riskForm.Ethinc = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Social') {
        this.riskForm.Social = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Traditional') {
        this.riskForm.Traditional = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
    }
  }

  onFocusDivertingRiskChange(value) {
    // if (value.checked === true) {
    //   this.focusDivertingRisk = criteriaEvaluationScores.focusDeliveryRisk_Yes
    // } else {
    //   this.focusDivertingRisk = criteriaEvaluationScores.focusDeliveryRisk_No
    // }
    this.riskForm.FocusDivertingrisk = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }

  onDeliveryFailureChange(value) {
    if (value.checked === true) {
      this.isDisabledDelivery = false;
      // this.deliveryFailure = criteriaEvaluationScores.deliveryFailure_Yes
    } else {
      this.isDisabledDelivery = true;
      this.riskForm.PrematureSeizure = false;
      this.riskForm.GovernmentConfiscation = false;
      this.riskForm.DesctructionByTerroristActivity = false;
      // this.deliveryFailure = criteriaEvaluationScores.deliveryFailure_No
    }
    this.riskForm.DeliveryFaiLure = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }
  onRiskDeliveryFailureChildChange(ev, data: any) {
    if (data != null && data !== '' && data !== undefined) {
      if (ev === 'PrematureSeizure') {
        this.riskForm.PrematureSeizure = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'GovernmentConfiscation') {
        this.riskForm.GovernmentConfiscation = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'TerroristActivity') {
        this.riskForm.DesctructionByTerroristActivity = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
    }
  }

  onFinancialLossesChange(value) {
    // if (value.checked === true) {
    //   this.financialLoses = criteriaEvaluationScores.financialLosses_Yes
    // } else {
    //   this.financialLoses = criteriaEvaluationScores.financialLosses_No
    // }
    this.riskForm.Financiallosses = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }
  onOpportunityLossChange(value) {
    if (value.checked === true) {
      this.isOpportunityLoss = false;
      // this.opportunityLoses = criteriaEvaluationScores.opportunityLoss_Yes
    } else {
      this.isOpportunityLoss = true;
      this.riskForm.ProjectSelectionId = null;
      // this.opportunityLoses = criteriaEvaluationScores.opportunityLoss_No
    }
    this.riskForm.Opportunityloss = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }

  onProjectotherDetailsChange(ev, data: number[]) {
    if (ev === 'projectSelction' && data != null) {

      this.riskForm.ProjectSelectionId = data;
      // var unique = Array.from(new Set(data))
      this.AddEditRiskSecurityCEForm(this.riskForm);
    }
  }

  onProbabilityDelayChange(value) {
    // if (value.checked === true) {
    //   this.opportunityDelay = criteriaEvaluationScores.probabilityDelayCuts_Yes
    // } else {
    //   this.opportunityDelay = criteriaEvaluationScores.probabilityDelayCuts_No
    // }
    this.riskForm.Probablydelaysinfunding = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }

  onOtherOrgHarmsChange(value) {
    if (value.checked === true) {
      this.IsOtherOrganizationalHarms = false;
      // this.otherOrganisationHarm = criteriaEvaluationScores.otherWayToHarmOrg_Yes
    } else {
      this.IsOtherOrganizationalHarms = true;
      // this.otherOrganisationHarm = criteriaEvaluationScores.otherWayToHarmOrg_No
      this.riskForm.OrganizationalDescription = null;
    }
    this.riskForm.OtherOrganizationalHarms = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }
  onOrganizationalDescriptionChange(ev, data: any) {
    if (data != null && data !== '' && data !== undefined) {
      if (ev === 'OrganizationalDescription') {
        this.riskForm.OrganizationalDescription = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
    }
  }
  //#endregion

  //#endregion

  //#region getallprojectdetail

  GetAllProjectList() {
    this.ProjectSelectionList = [];
    this.criteriaEvalService
      .GetAllProjectList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectList
      )
      .subscribe(data => {
        if (data != null) {
          if (data.data.ProjectDetailModel != null) {
            data.data.ProjectDetailModel.forEach(element => {
              this.ProjectSelectionList.push({
                value: element.ProjectId,
                label: element.ProjectName
              });
            });

            // this.GetCriteraiEvaluationDetailById(this.ProjectId);
          }
        }
      });
  }
  //#endregion

  //#region Get Criteria evaluation by ProjectId Donor
  GetCriteraiEvaluationDetailById(ProjectId: number) {
    // this.OtherProjectList = [];
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.criteriaEvalService
        .GetCriteriaEvalDetailsByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetAllCriteriaEvaluationDetail,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.CriteriaEveluationModel != null) {
              this.donorCEForm.PastFundingExperience =
                data.data.CriteriaEveluationModel.PastFundingExperience;
              if (this.donorCEForm.PastFundingExperience === true) {
                this.isDisabled = false;
              } else {
                this.isDisabled = true;
              }
              this.donorCEForm.DonorCEId =
                data.data.CriteriaEveluationModel.DonorCEId;
              this.donorCEForm.MethodOfFunding =
                data.data.CriteriaEveluationModel.MethodOfFunding;
              this.donorCEForm.ProposalAccepted =
                data.data.CriteriaEveluationModel.ProposalAccepted;
              this.donorCEForm.ProposalExperience =
                data.data.CriteriaEveluationModel.ProposalExperience;
              this.donorCEForm.Professional =
                data.data.CriteriaEveluationModel.Professional;
              this.donorCEForm.FundsOnTime =
                data.data.CriteriaEveluationModel.FundsOnTime;
              this.donorCEForm.EffectiveCommunication =
                data.data.CriteriaEveluationModel.EffectiveCommunication;
              this.donorCEForm.Dispute =
                data.data.CriteriaEveluationModel.Dispute;
              this.donorCEForm.OtherDeliverable =
                data.data.CriteriaEveluationModel.OtherDeliverable;
              this.donorCEForm.OtherDeliverableType =
                data.data.CriteriaEveluationModel.OtherDeliverableType;

              this.donorCEForm.PastWorkingExperience =
                data.data.CriteriaEveluationModel.PastWorkingExperience;
              if (this.donorCEForm.PastWorkingExperience === true) {
                this.isDisabledCriticism = false;
              } else {
                this.isDisabledCriticism = true;
              }
              this.donorCEForm.CriticismPerformance =
                data.data.CriteriaEveluationModel.CriticismPerformance;
              this.donorCEForm.TimeManagement =
                data.data.CriteriaEveluationModel.TimeManagement;
              this.donorCEForm.MoneyAllocation =
                data.data.CriteriaEveluationModel.MoneyAllocation;
              this.donorCEForm.Accountability =
                data.data.CriteriaEveluationModel.Accountability;
              this.donorCEForm.DeliverableQuality =
                data.data.CriteriaEveluationModel.DeliverableQuality;

              this.donorCEForm.DonorFinancingHistory =
                data.data.CriteriaEveluationModel.DonorFinancingHistory;
              this.donorCEForm.ReligiousStanding =
                data.data.CriteriaEveluationModel.ReligiousStanding;
              this.donorCEForm.PoliticalStanding =
                data.data.CriteriaEveluationModel.PoliticalStanding;
              // profuct and service
              this.productAndServiceForm.Women =
                data.data.CriteriaEveluationModel.Women;
              this.productAndServiceForm.Children =
                data.data.CriteriaEveluationModel.Children;
              this.productAndServiceForm.Awareness =
                data.data.CriteriaEveluationModel.Awareness;

              this.productAndServiceForm.Education =
                data.data.CriteriaEveluationModel.Education;

              this.productAndServiceForm.DrugAbuses =
                data.data.CriteriaEveluationModel.DrugAbuses;

              this.productAndServiceForm.Right =
                data.data.CriteriaEveluationModel.Right;
              this.productAndServiceForm.Culture =
                data.data.CriteriaEveluationModel.Culture;
              this.productAndServiceForm.Music =
                data.data.CriteriaEveluationModel.Music;
              this.productAndServiceForm.Documentaries =
                data.data.CriteriaEveluationModel.Documentaries;
              this.productAndServiceForm.InvestigativeJournalism =
                data.data.CriteriaEveluationModel.InvestigativeJournalism;
              this.productAndServiceForm.HealthAndNutrition =
                data.data.CriteriaEveluationModel.HealthAndNutrition;
              this.productAndServiceForm.News =
                data.data.CriteriaEveluationModel.News;
              this.productAndServiceForm.SocioPolitiacalDebate =
                data.data.CriteriaEveluationModel.SocioPolitiacalDebate;
              this.productAndServiceForm.Studies =
                data.data.CriteriaEveluationModel.Studies;
              this.productAndServiceForm.Reports =
                data.data.CriteriaEveluationModel.Reports;
              this.productAndServiceForm.CommunityDevelopment =
                data.data.CriteriaEveluationModel.CommunityDevelopment;
              this.productAndServiceForm.Aggriculture =
                data.data.CriteriaEveluationModel.Aggriculture;
              this.productAndServiceForm.DRR =
                data.data.CriteriaEveluationModel.DRR;
              this.productAndServiceForm.ServiceEducation =
                data.data.CriteriaEveluationModel.ServiceEducation;
              this.productAndServiceForm.ServiceHealthAndNutrition =
                data.data.CriteriaEveluationModel.ServiceHealthAndNutrition;
              this.productAndServiceForm.RadioProduction =
                data.data.CriteriaEveluationModel.RadioProduction;
              this.productAndServiceForm.TVProgram =
                data.data.CriteriaEveluationModel.TVProgram;
              this.productAndServiceForm.PrintedMedia =
                data.data.CriteriaEveluationModel.PrintedMedia;
              this.productAndServiceForm.RoundTable =
                data.data.CriteriaEveluationModel.RoundTable;
              this.productAndServiceForm.Others =
                data.data.CriteriaEveluationModel.Others;
              if (this.productAndServiceForm.Others == true) {
                this.isDisableActivity = false;
                this.productAndServiceForm.OtherActivity =
                  data.data.CriteriaEveluationModel.OtherActivity;
              } else {
                this.isDisableActivity = true;
              }
              this.productAndServiceForm.TargetBenificaiaryWomen =
                data.data.CriteriaEveluationModel.TargetBenificaiaryWomen;
              this.productAndServiceForm.TargetBenificiaryMen =
                data.data.CriteriaEveluationModel.TargetBenificiaryMen;
              this.productAndServiceForm.TargetBenificiaryAgeGroup =
                data.data.CriteriaEveluationModel.TargetBenificiaryAgeGroup;
              if (
                this.productAndServiceForm.TargetBenificiaryAgeGroup === true
              ) {
                this.isAgeGroupEnable = true;
              } else {
                this.isAgeGroupEnable = false;
              }
              this.productAndServiceForm.TargetBenificiaryaOccupation =
                data.data.CriteriaEveluationModel.TargetBenificiaryaOccupation;
              if (
                this.productAndServiceForm.TargetBenificiaryaOccupation === true
              ) {
                this.isOccupationEnable = true;
              } else {
                this.isOccupationEnable = false;
              }
              this.productAndServiceForm.Product =
                data.data.CriteriaEveluationModel.Product;
              this.productAndServiceForm.Service =
                data.data.CriteriaEveluationModel.Service;

              // eligibility
              this.eligibilityForm.DonorCriteriaMet =
                data.data.CriteriaEveluationModel.DonorCriteriaMet;
              if (this.eligibilityForm.DonorCriteriaMet === true) {
                this.isDisabledEligibilityCriteria = true;
              } else {
                this.eligibilityForm.EligibilityDealine =
                  data.data.CriteriaEveluationModel.EligibilityDealine;
                this.eligibilityForm.CoPartnership =
                  data.data.CriteriaEveluationModel.CoPartnership;

              }

              this.feasibilityForm.CapacityAvailableForProject =
                data.data.CriteriaEveluationModel.CapacityAvailableForProject;
              if (this.feasibilityForm.CapacityAvailableForProject === true) {
                this.isDisabledCompensation = true;
              } else {
                this.isDisabledCompensation = false;
                this.feasibilityForm.TrainedStaff =
                  data.data.CriteriaEveluationModel.TrainedStaff;
                this.feasibilityForm.ByEquipment =
                  data.data.CriteriaEveluationModel.ByEquipment;
                this.feasibilityForm.ExpandScope =
                  data.data.CriteriaEveluationModel.ExpandScope;
                this.feasibilityForm.GeoGraphicalPresence =
                  data.data.CriteriaEveluationModel.GeoGraphicalPresence;
              }
              this.feasibilityForm.ThirdPartyContract =
                data.data.CriteriaEveluationModel.ThirdPartyContract;
              if (data.data.CriteriaEveluationModel.CostOfCompensationMonth == null || data.data.CriteriaEveluationModel.CostOfCompensationMonth == undefined) {
                this.feasibilityForm.CostOfCompensationMonth = null;
              } else {
                this.feasibilityForm.CostOfCompensationMonth =
                  data.data.CriteriaEveluationModel.CostOfCompensationMonth;
              }
              this.feasibilityForm.CostOfCompensationMoney =
                data.data.CriteriaEveluationModel.CostOfCompensationMoney;

              this.feasibilityForm.AnyInKindComponent =
                data.data.CriteriaEveluationModel.AnyInKindComponent;
              if (this.feasibilityForm.AnyInKindComponent == true) {
                this.feasibilityForm.UseableByOrganisation = false;
                this.feasibilityForm.FeasibleExpertDeploy = false;
              } else {
                this.feasibilityForm.UseableByOrganisation =
                  data.data.CriteriaEveluationModel.UseableByOrganisation;
                this.feasibilityForm.FeasibleExpertDeploy =
                  data.data.CriteriaEveluationModel.FeasibleExpertDeploy;
                if (this.feasibilityForm.FeasibleExpertDeploy == true) {
                  this.isFeasibilityExpert = true;
                } else {
                  this.isFeasibilityExpert = false;
                }
              }
              this.feasibilityForm.FeasibilityExpert =
                data.data.CriteriaEveluationModel.FeasibilityExpert;

              this.feasibilityForm.EnoughTimeForProject =
                data.data.CriteriaEveluationModel.EnoughTimeForProject;
              this.feasibilityForm.ProjectAllowedBylaw =
                data.data.CriteriaEveluationModel.ProjectAllowedBylaw;
              this.feasibilityForm.ProjectByLeadership =
                data.data.CriteriaEveluationModel.ProjectByLeadership;
              this.feasibilityForm.IsProjectPractical =
                data.data.CriteriaEveluationModel.IsProjectPractical;
              this.feasibilityForm.PresenceCoverageInProject =
                data.data.CriteriaEveluationModel.PresenceCoverageInProject;
              this.feasibilityForm.ProjectInLineWithOrgFocus =
                data.data.CriteriaEveluationModel.ProjectInLineWithOrgFocus;
              this.feasibilityForm.EnoughTimeToPrepareProposal =
                data.data.CriteriaEveluationModel.EnoughTimeToPrepareProposal;

              this.feasibilityForm.ProjectRealCost =
                data.data.CriteriaEveluationModel.ProjectRealCost;
              this.feasibilityForm.IsCostGreaterthenBudget =
                data.data.CriteriaEveluationModel.IsCostGreaterthenBudget;
              if (this.feasibilityForm.IsCostGreaterthenBudget === true) {
                this.isCostGreaterThanBudget = false;
                this.feasibilityForm.PerCostGreaterthenBudget = 0;
              } else {
                this.isCostGreaterThanBudget = true;
                this.feasibilityForm.PerCostGreaterthenBudget =
                  data.data.CriteriaEveluationModel.PerCostGreaterthenBudget;
              }
              this.feasibilityForm.IsFinancialContribution =
                data.data.CriteriaEveluationModel.IsFinancialContribution;
              this.feasibilityForm.IsSecurity =
                data.data.CriteriaEveluationModel.IsSecurity;
              this.feasibilityForm.IsGeographical =
                data.data.CriteriaEveluationModel.IsGeographical;
              this.feasibilityForm.IsSeasonal =
                data.data.CriteriaEveluationModel.IsSeasonal;
              // add/edit priority ec form

              this.priorityForm.IncreaseEligibility =
                data.data.CriteriaEveluationModel.IncreaseEligibility;
              this.priorityForm.IncreaseReputation =
                data.data.CriteriaEveluationModel.IncreaseReputation;
              this.priorityForm.ImproveDonorRelationship =
                data.data.CriteriaEveluationModel.ImproveDonorRelationship;
              this.priorityForm.GoodCause =
                data.data.CriteriaEveluationModel.GoodCause;
              this.priorityForm.ImprovePerformancecapacity =
                data.data.CriteriaEveluationModel.ImprovePerformancecapacity;
              this.priorityForm.SkillImprove =
                data.data.CriteriaEveluationModel.SkillImprove;
              this.priorityForm.FillingFundingGap =
                data.data.CriteriaEveluationModel.FillingFundingGap;
              this.priorityForm.NewSoftware =
                data.data.CriteriaEveluationModel.NewSoftware;
              this.priorityForm.NewEquipment =
                data.data.CriteriaEveluationModel.NewEquipment;
              this.priorityForm.CoverageAreaExpansion =
                data.data.CriteriaEveluationModel.CoverageAreaExpansion;
              this.priorityForm.NewTraining =
                data.data.CriteriaEveluationModel.NewTraining;
              this.priorityForm.Others =
                data.data.CriteriaEveluationModel.Others;
              if (this.priorityForm.Others == true) {
                this.isPriorityOther = true;
              } else {
                this.isPriorityOther = false;
              }

              this.priorityForm.ExpansionGoal =
                data.data.CriteriaEveluationModel.ExpansionGoal;

              // financial profitability

              if (data.data.CriteriaEveluationModel.ProjectActivities == null || data.data.CriteriaEveluationModel.ProjectActivities == undefined) {
                this.financialForm.ProjectActivities = 0;
              } else {
                this.financialForm.ProjectActivities = data.data.CriteriaEveluationModel.ProjectActivities;
              }
              //  if (this.financialForm.ProjectActivities == null ? 0 : data.data.CriteriaEveluationModel.ProjectActivities)
              if (data.data.CriteriaEveluationModel.Operational == null || data.data.CriteriaEveluationModel.Operational == undefined) {
                this.financialForm.Operational = 0;
              } else {
                this.financialForm.Operational =
                  data.data.CriteriaEveluationModel.Operational;
              }
              if (data.data.CriteriaEveluationModel.Overhead_Admin == 0 || data.data.CriteriaEveluationModel.Overhead_Admin == undefined) {
                this.financialForm.Overhead_Admin = 0;
              } else {
                this.financialForm.Overhead_Admin = data.data.CriteriaEveluationModel.Overhead_Admin;
              }
              if (data.data.CriteriaEveluationModel.Lump_Sum == 0 || data.data.CriteriaEveluationModel.Lump_Sum == undefined) {
                this.financialForm.Lump_Sum = 0;
              } else {
                this.financialForm.Lump_Sum = data.data.CriteriaEveluationModel.Lump_Sum;
              }
              // if (this.financialForm.Operational === null ? 0 : data.data.CriteriaEveluationModel.Operational)
              // if (this.financialForm.Overhead_Admin == null ? 0 : data.data.CriteriaEveluationModel.Overhead_Admin)
              // if (this.financialForm.Lump_Sum == null ? 0 : data.data.CriteriaEveluationModel.Lump_Sum)
              this.financialForm.Total =
                data.data.CriteriaEveluationModel.Total;

              // add/edit riskSecurity
              this.riskForm.Security =
                data.data.CriteriaEveluationModel.Security;
              if (this.riskForm.Security == true) {
                this.isDisabledRisk = false;
              } else {
                this.isDisabledRisk = true;
                // this.riskForm.Staff = false;
                // this.riskForm.ProjectAssets = false;
                // this.riskForm.Suppliers = false;
                // this.riskForm.Beneficiaries = false;
                // this.riskForm.OverallOrganization = false;
              }

              this.riskForm.Staff = data.data.CriteriaEveluationModel.Staff;
              this.riskForm.ProjectAssets =
                data.data.CriteriaEveluationModel.ProjectAssets;
              this.riskForm.Suppliers =
                data.data.CriteriaEveluationModel.Suppliers;
              this.riskForm.Beneficiaries =
                data.data.CriteriaEveluationModel.Beneficiaries;
              this.riskForm.OverallOrganization =
                data.data.CriteriaEveluationModel.OverallOrganization;
              this.riskForm.DeliveryFaiLure =
                data.data.CriteriaEveluationModel.DeliveryFaiLure;
              if (this.riskForm.DeliveryFaiLure == true) {
                this.isDisabledDelivery = false;
              } else {
                this.isDisabledDelivery = true;
              }
              this.riskForm.PrematureSeizure =
                data.data.CriteriaEveluationModel.PrematureSeizure;
              this.riskForm.GovernmentConfiscation =
                data.data.CriteriaEveluationModel.GovernmentConfiscation;
              this.riskForm.DesctructionByTerroristActivity =
                data.data.CriteriaEveluationModel.DesctructionByTerroristActivity;
              this.riskForm.Reputation =
                data.data.CriteriaEveluationModel.Reputation;
              if (this.riskForm.Reputation == true) {
                this.isDisabledReputation = false;
              } else {
                this.isDisabledReputation = true;
              }
              this.riskForm.Religious =
                data.data.CriteriaEveluationModel.Religious;
              this.riskForm.Sectarian =
                data.data.CriteriaEveluationModel.Sectarian;
              this.riskForm.Ethinc = data.data.CriteriaEveluationModel.Ethinc;
              this.riskForm.Social = data.data.CriteriaEveluationModel.Social;
              this.riskForm.Traditional =
                data.data.CriteriaEveluationModel.Traditional;
              this.riskForm.FocusDivertingrisk =
                data.data.CriteriaEveluationModel.FocusDivertingrisk;
              this.riskForm.Financiallosses =
                data.data.CriteriaEveluationModel.Financiallosses;
              this.riskForm.Opportunityloss =
                data.data.CriteriaEveluationModel.Opportunityloss;
              if (this.riskForm.Opportunityloss === true) {
                this.isOpportunityLoss = false;
                this.riskForm.ProjectSelectionId =
                  data.data.CriteriaEveluationModel.ProjectSelectionId;
              } else {
                this.riskForm.ProjectSelectionId = null;
              }
              this.riskForm.Probablydelaysinfunding =
                data.data.CriteriaEveluationModel.Probablydelaysinfunding;
              this.riskForm.OtherOrganizationalHarms =
                data.data.CriteriaEveluationModel.OtherOrganizationalHarms;
              if (this.riskForm.OtherOrganizationalHarms === true) {
                this.IsOtherOrganizationalHarms = false;
                this.riskForm.OrganizationalDescription =
                  data.data.CriteriaEveluationModel.OrganizationalDescription;
              } else {
                this.riskForm.OrganizationalDescription = null;
              }
              this.IsSubmitCEform.IsCriteriaEvaluationSubmit = data.data.CriteriaEveluationModel.IsCriteriaEvaluationSubmit;

            }
          }
        },
        error => {
          this.toastr.error('Something Went Wrong..! Please Try Again.');
        });
    }
  }

  //#region Add edit DOnorCE form
  AddEditDonorCEForm(model: any) {
    if (model != null) {
      const obj: DonorCEModel = {
        ProjectId: this.ProjectId,
        DonorCEId: model.DonorCEId,
        MethodOfFunding: model.MethodOfFunding,
        PastFundingExperience: model.PastFundingExperience,
        ProposalAccepted: model.ProposalAccepted,
        ProposalExperience: model.ProposalExperience,
        Professional: model.Professional,
        FundsOnTime: model.FundsOnTime,
        EffectiveCommunication: model.EffectiveCommunication,
        Dispute: model.Dispute,
        OtherDeliverable: model.OtherDeliverable,
        OtherDeliverableType: model.OtherDeliverableType,
        PastWorkingExperience: model.PastWorkingExperience,
        CriticismPerformance: model.CriticismPerformance,
        TimeManagement: model.TimeManagement,
        MoneyAllocation: model.MoneyAllocation,
        Accountability: model.Accountability,
        DeliverableQuality: model.DeliverableQuality,
        DonorFinancingHistory: model.DonorFinancingHistory,
        ReligiousStanding: model.ReligiousStanding,
        PoliticalStanding: model.PoliticalStanding
      };
      this.criteriaEvalService
        .AddEditDonorCriteriaEvaluationForm(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditDonorCriteria,
          obj
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
          }
        });
    }
  }
  //#endregion

  //#region  add edit purpose of initialting
  AddEditPurposeOfInitiatingForm(model: any) {
    if (model != null) {
      const obj: ProductAndServiceCEModel = {
        ProjectId: this.ProjectId,
        ProductServiceId: model.ProductServiceId,
        Women: model.Women,
        Children: model.Children,
        Awareness: model.Awareness,
        Education: model.Education,
        DrugAbuses: model.DrugAbuses,
        Right: model.Right,
        Culture: model.Culture,
        Music: model.Music,
        Documentaries: model.Documentaries,
        InvestigativeJournalism: model.InvestigativeJournalism,
        HealthAndNutrition: model.HealthAndNutrition,
        News: model.News,
        SocioPolitiacalDebate: model.SocioPolitiacalDebate,
        Studies: model.Studies,
        Reports: model.Reports,
        CommunityDevelopment: model.CommunityDevelopment,
        Aggriculture: model.Aggriculture,
        DRR: model.DRR,
        ServiceEducation: model.ServiceEducation,
        ServiceHealthAndNutrition: model.ServiceHealthAndNutrition,
        RadioProduction: model.RadioProduction,
        TVProgram: model.TVProgram,
        PrintedMedia: model.PrintedMedia,
        RoundTable: model.RoundTable,
        Others: model.Others,
        OtherActivity: model.OtherActivity,
        TargetBenificaiaryWomen: model.TargetBenificaiaryWomen,
        TargetBenificiaryMen: model.TargetBenificiaryMen,
        TargetBenificiaryAgeGroup: model.TargetBenificiaryAgeGroup,
        TargetBenificiaryaOccupation: model.TargetBenificiaryaOccupation,
        Product: model.Product,
        Service: model.Service
      };
      this.criteriaEvalService
        .AddEditProductServiceCEForm(
          this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditPurposeofInitiativeCriteria,
          obj
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
          }
        });
    }
  }

  //#endregion

  //#region  add eligibility form
  AddEditEligibilityCEForm(model: any) {
    if (model != null) {
      const obj: EligibilityCEModel = {
        EligibilityId: model.EligibilityId,
        ProjectId: this.ProjectId,
        DonorCriteriaMet: model.DonorCriteriaMet,
        EligibilityDealine: model.EligibilityDealine,
        CoPartnership: model.CoPartnership
      };
      this.criteriaEvalService
        .AddEditEligibilityCriteriaEForm(
          this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditEligibilityCriteriaDetail,
          obj
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
            this.eligibilityForm.DonorCriteriaMet = response.data.eligibilityCriteriaDetail.DonorCriteriaMet;
            if (this.eligibilityForm.DonorCriteriaMet == false) {
              // this.toastr.warning(
              //   '* warning Donor eligibility criteria is must)'
              // );
            }
          }
        });
    }
  }
  //#endregion

  //#region add edit feasibility form
  AddEditFeasibilityCEForm(model: any) {
    if (model != null) {
      const obj: FeasibilityCEModel = {
        FeasibilityId: model.FeasibilityId,
        ProjectId: this.ProjectId,
        CapacityAvailableForProject: model.CapacityAvailableForProject,
        TrainedStaff: model.TrainedStaff,
        ByEquipment: model.ByEquipment,
        ExpandScope: model.ExpandScope,
        GeoGraphicalPresence: model.GeoGraphicalPresence,

        ThirdPartyContract: model.ThirdPartyContract,
        CostOfCompensationMonth: model.CostOfCompensationMonth,
        CostOfCompensationMoney: model.CostOfCompensationMoney,

        AnyInKindComponent: model.AnyInKindComponent,
        UseableByOrganisation: model.UseableByOrganisation,
        FeasibleExpertDeploy: model.FeasibleExpertDeploy,
        FeasibilityExpert: model.FeasibilityExpert,

        EnoughTimeForProject: model.EnoughTimeForProject,
        ProjectAllowedBylaw: model.ProjectAllowedBylaw,
        ProjectByLeadership: model.ProjectByLeadership,
        IsProjectPractical: model.IsProjectPractical,
        PresenceCoverageInProject: model.PresenceCoverageInProject,
        ProjectInLineWithOrgFocus: model.ProjectInLineWithOrgFocus,
        EnoughTimeToPrepareProposal: model.EnoughTimeToPrepareProposal,

        ProjectRealCost: model.ProjectRealCost,
        IsCostGreaterthenBudget: model.IsCostGreaterthenBudget,
        PerCostGreaterthenBudget: model.PerCostGreaterthenBudget,
        IsFinancialContribution: model.IsFinancialContribution,
        IsSecurity: model.IsSecurity,
        IsGeographical: model.IsGeographical,
        IsSeasonal: model.IsSeasonal
      };
      this.criteriaEvalService
        .AddEditFeasibilityCriteriaEForm(
          this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditFeasibilityCriteria,
          obj
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
          }
        });
    }
  }
  //#endregion

  //#region add/edit priority ec form
  AddEditPriorityCEForm(model: any) {
    if (model != null) {
      const obj: PriorityCEmodel = {
        PriorityCriteriaDetailId: model.PriorityCriteriaDetailId,
        ProjectId: this.ProjectId,
        IncreaseEligibility: model.IncreaseEligibility,
        IncreaseReputation: model.IncreaseReputation,
        ImproveDonorRelationship: model.ImproveDonorRelationship,
        GoodCause: model.GoodCause,
        ImprovePerformancecapacity: model.ImprovePerformancecapacity,
        SkillImprove: model.SkillImprove,
        FillingFundingGap: model.FillingFundingGap,
        NewSoftware: model.NewSoftware,
        NewEquipment: model.NewEquipment,
        CoverageAreaExpansion: model.CoverageAreaExpansion,
        NewTraining: model.NewTraining,
        Others: model.Others,
        ExpansionGoal: model.ExpansionGoal
      };
      this.criteriaEvalService
        .AddEditPriorityCriteriaEForm(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditPriorityCriteria,
          obj
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
          }
        });
    }
  }
  //#endregion

  //#region add/edit financial profitability
  AddEditFinancialProfitability(model: any) {

    if (model != null) {
      const obj: FinancialProfitabilityModel = {
        FinancialCriteriaDetailId: model.FinancialCriteriaDetailId,
        ProjectId: this.ProjectId,
        ProjectActivities: model.ProjectActivities,
        Operational: model.Operational,
        Overhead_Admin: model.Overhead_Admin,
        Lump_Sum: model.Lump_Sum,
        // Total:
        //   model.Operational +
        //   model.Lump_Sum +
        //   model.Overhead_Admin +
        //   model.ProjectActivities
      };
      this.criteriaEvalService
        .AddEditFinancialCriteriaProfitability(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditFinancialCriteria,
          obj
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
          }
        },
          error => {
            this.toastr.error('Something went wrong! Please try Agian');
          });
    }
  }
  //#endregion

  //#region add/edit riskSecurity
  AddEditRiskSecurityCEForm(model: any) {
    if (model != null) {
      const obj: RiskSecurityModel = {
        RiskCriteriaDetailId: model.RiskCriteriaDetailId,
        ProjectId: this.ProjectId,
        Security: model.Security,
        Staff: model.Staff,
        ProjectAssets: model.ProjectAssets,
        Suppliers: model.Suppliers,
        Beneficiaries: model.Beneficiaries,
        OverallOrganization: model.OverallOrganization,
        DeliveryFaiLure: model.DeliveryFaiLure,
        PrematureSeizure: model.PrematureSeizure,
        GovernmentConfiscation: model.GovernmentConfiscation,
        DesctructionByTerroristActivity: model.DesctructionByTerroristActivity,
        Reputation: model.Reputation,
        Religious: model.Religious,
        Sectarian: model.Sectarian,
        Ethinc: model.Ethinc,
        Social: model.Social,
        Traditional: model.Traditional,
        FocusDivertingrisk: model.FocusDivertingrisk,
        Financiallosses: model.Financiallosses,
        Opportunityloss: model.Opportunityloss,
        ProjectSelectionId: model.ProjectSelectionId,
        Probablydelaysinfunding: model.Probablydelaysinfunding,
        OtherOrganizationalHarms: model.OtherOrganizationalHarms,
        OrganizationalDescription: model.OrganizationalDescription
      };
      this.criteriaEvalService
        .AddEditRiskSecurityCriteriaEForm(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditRiskCriteria,
          obj
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
          }
        });
    }
  }
  //#endregion

  //#region to calculate total value
  get totalValue() {

    // this.totalScore = 0;
    this.totalScore =
      (this.donorCEForm.PastFundingExperience === true
        ? criteriaEvaluationScores.pastFundingExperience_Yes
        : criteriaEvaluationScores.pastFundingExperience_No) +
      (this.donorCEForm.MethodOfFunding === 1
        ? criteriaEvaluationScores.methodOfFunding_Sole
        : this.donorCEForm.MethodOfFunding === 2
          ? criteriaEvaluationScores.methodOfFunding_Source
          : criteriaEvaluationScores.methodOfFunding_Default) +
      (this.donorCEForm.ProposalAccepted === true
        ? criteriaEvaluationScores.proposalAccepted_Yes
        : criteriaEvaluationScores.proposalAccepted_No) +
      (this.donorCEForm.Professional === true
        ? criteriaEvaluationScores.proposalExp_Professional_Yes
        : criteriaEvaluationScores.proposalExp_Professional_No) +
      (this.donorCEForm.FundsOnTime === true
        ? criteriaEvaluationScores.proposalExp_FundsOnTime_Yes
        : criteriaEvaluationScores.proposalExp_FundsOnTime_No) +
      (this.donorCEForm.EffectiveCommunication === true
        ? criteriaEvaluationScores.proposalExp_EffectiveCommunication_Yes
        : criteriaEvaluationScores.proposalExp_EffectiveCommunication_No) +
      (this.donorCEForm.Dispute === true
        ? criteriaEvaluationScores.proposalExp_Disputes_Yes
        : criteriaEvaluationScores.proposalExp_Disputes_No) +
      (this.donorCEForm.OtherDeliverable === true
        ? criteriaEvaluationScores.proposalExp_OtherDeliverable_Yes
        : criteriaEvaluationScores.proposalExp_OtherDeliverable_No) +
      (this.donorCEForm.PastWorkingExperience === true
        ? criteriaEvaluationScores.pastWorkExperoence_Yes
        : criteriaEvaluationScores.pastWorkExperoence_No) +
      (this.donorCEForm.CriticismPerformance === true
        ? criteriaEvaluationScores.pastCriticismPerfomance_Yes
        : criteriaEvaluationScores.pastCriticismPerfomance_No) +
      (this.donorCEForm.TimeManagement === true
        ? criteriaEvaluationScores.pastCriticismTimeManagement_Yes
        : criteriaEvaluationScores.pastCriticismTimeManagement_No) +
      (this.donorCEForm.MoneyAllocation === true
        ? criteriaEvaluationScores.pastCriticismMoneyAllocation_Yes
        : criteriaEvaluationScores.pastCriticismMoneyAllocation_No) +
      (this.donorCEForm.Accountability === true
        ? criteriaEvaluationScores.pastCriticismAccountability_Yes
        : criteriaEvaluationScores.pastCriticismAccountability_No) +
      (this.donorCEForm.DeliverableQuality === true
        ? criteriaEvaluationScores.pastCriticismQualityDeliverable_Yes
        : criteriaEvaluationScores.pastCriticismQualityDeliverable_No) +
      (this.donorCEForm.DonorFinancingHistory === 1
        ? criteriaEvaluationScores.finanacingHistory_Good
        : this.donorCEForm.DonorFinancingHistory === 2
          ? criteriaEvaluationScores.finanacingHistory_Neutral
          : this.donorCEForm.DonorFinancingHistory === 3
            ? criteriaEvaluationScores.finanacingHistory_Bad
            : criteriaEvaluationScores.finanacingHistory_Neutral) +
      (this.donorCEForm.ReligiousStanding === 1
        ? criteriaEvaluationScores.religiousStanding_Good
        : this.donorCEForm.ReligiousStanding === 2
          ? criteriaEvaluationScores.religiousStanding_Neutral
          : this.donorCEForm.ReligiousStanding === 3
            ? criteriaEvaluationScores.religiousStanding_Bad
            : criteriaEvaluationScores.religiousStanding_Neutral) +
      (this.donorCEForm.PoliticalStanding === 1
        ? criteriaEvaluationScores.politicalStanding_Good
        : this.donorCEForm.PoliticalStanding === 2
          ? criteriaEvaluationScores.politicalStanding_Neutral
          : this.donorCEForm.PoliticalStanding === 3
            ? criteriaEvaluationScores.politicalStanding_Bad
            : criteriaEvaluationScores.politicalStanding_Neutral) +
      (this.productAndServiceForm.Women === true
        ? criteriaEvaluationScores.prodWomen_Yes
        : criteriaEvaluationScores.prodWomen_No) +
      (this.productAndServiceForm.Children === true
        ? criteriaEvaluationScores.prodhildren_Yes
        : criteriaEvaluationScores.prodhildren_No) +
      (this.productAndServiceForm.Awareness === true
        ? criteriaEvaluationScores.prodAwareness_Yes
        : criteriaEvaluationScores.prodAwareness_No) +
      (this.productAndServiceForm.Education === true
        ? criteriaEvaluationScores.prodEducation_Yes
        : criteriaEvaluationScores.prodEducation_No) +
      (this.productAndServiceForm.DrugAbuses === true
        ? criteriaEvaluationScores.prodDrugAndAbuse_Yes
        : criteriaEvaluationScores.prodDrugAndAbuse_No) +
      (this.productAndServiceForm.Right === true
        ? criteriaEvaluationScores.prodRights_Yes
        : criteriaEvaluationScores.prodRights_No) +
      (this.productAndServiceForm.Culture === true
        ? criteriaEvaluationScores.prodCulture_Yes
        : criteriaEvaluationScores.prodCulture_No) +
      (this.productAndServiceForm.Music === true
        ? criteriaEvaluationScores.prodMusic_Yes
        : criteriaEvaluationScores.prodMusic_No) +
      (this.productAndServiceForm.Documentaries === true
        ? criteriaEvaluationScores.prodDocumentaries_Yes
        : criteriaEvaluationScores.prodDocumentaries_No) +
      (this.productAndServiceForm.InvestigativeJournalism === true
        ? criteriaEvaluationScores.prodInvestigativeJournlism_Yes
        : criteriaEvaluationScores.prodInvestigativeJournlism_No) +
      (this.productAndServiceForm.HealthAndNutrition === true
        ? criteriaEvaluationScores.prodHealthAndNutrition_Yes
        : criteriaEvaluationScores.prodHealthAndNutrition_No) +
      (this.productAndServiceForm.News === true
        ? criteriaEvaluationScores.prodNews_Yes
        : criteriaEvaluationScores.prodNews_No) +
      (this.productAndServiceForm.SocioPolitiacalDebate === true
        ? criteriaEvaluationScores.prodSocioPoliticalDebate_Yes
        : criteriaEvaluationScores.prodSocioPoliticalDebate_No) +
      (this.productAndServiceForm.Studies === true
        ? criteriaEvaluationScores.prodStudies_Yes
        : criteriaEvaluationScores.prodStudies_No) +
      (this.productAndServiceForm.Reports === true
        ? criteriaEvaluationScores.prodReport_Yes
        : criteriaEvaluationScores.prodReport_No) +
      (this.productAndServiceForm.CommunityDevelopment === true
        ? criteriaEvaluationScores.servCommunityDevelop_Yes
        : criteriaEvaluationScores.servCommunityDevelop_No) +
      (this.productAndServiceForm.Aggriculture === true
        ? criteriaEvaluationScores.servAggriculture_Yes
        : criteriaEvaluationScores.servAggriculture_No) +
      (this.productAndServiceForm.DRR === true
        ? criteriaEvaluationScores.serDRR_Yes
        : criteriaEvaluationScores.serDRR_No) +
      (this.productAndServiceForm.ServiceEducation === true
        ? criteriaEvaluationScores.serviceEducation_Yes
        : criteriaEvaluationScores.serviceEducation_No) +
      (this.productAndServiceForm.ServiceHealthAndNutrition === true
        ? criteriaEvaluationScores.servEducationHealthandNutrition_Yes
        : criteriaEvaluationScores.servEducationHealthandNutrition_No) +
      (this.productAndServiceForm.RadioProduction === true
        ? criteriaEvaluationScores.activityRadioProduction_Yes
        : criteriaEvaluationScores.activityRadioProduction_No) +
      (this.productAndServiceForm.TVProgram === true
        ? criteriaEvaluationScores.activityTVprogram_Yes
        : criteriaEvaluationScores.activityTVprogram_No) +
      (this.productAndServiceForm.PrintedMedia === true
        ? criteriaEvaluationScores.activityPrintedMedia_Yes
        : criteriaEvaluationScores.activityPrintedMedia_No) +
      (this.productAndServiceForm.RoundTable === true
        ? criteriaEvaluationScores.activityRoundTables_Yes
        : criteriaEvaluationScores.activityRoundTables_No) +
      (this.productAndServiceForm.Others === true
        ? criteriaEvaluationScores.activityOther_Yes
        : criteriaEvaluationScores.activityOther_No) +
      (this.productAndServiceForm.TargetBenificaiaryWomen === true
        ? criteriaEvaluationScores.tagetbeneficiaryWomen_Yes
        : criteriaEvaluationScores.tagetbeneficiaryWomen_No) +
      (this.productAndServiceForm.TargetBenificiaryMen === true
        ? criteriaEvaluationScores.targetBeneficiaryMen_Yes
        : criteriaEvaluationScores.targetBeneficiaryMen_No) +
      (this.productAndServiceForm.TargetBenificiaryAgeGroup === true
        ? criteriaEvaluationScores.tagetBenificiaryAgeGroup_Yes
        : criteriaEvaluationScores.tagetBenificiaryAgeGroup_No) +
      (this.productAndServiceForm.TargetBenificiaryaOccupation === true
        ? criteriaEvaluationScores.targetbenficiaryOccupation_Yes
        : criteriaEvaluationScores.targetbenficiaryOccupation_No) +
      (this.eligibilityForm.DonorCriteriaMet === true
        ? criteriaEvaluationScores.onDonorELegibilityCrteria_Yes
        : criteriaEvaluationScores.onDonorELegibilityCrteria_No) +
      (this.eligibilityForm.EligibilityDealine === true
        ? criteriaEvaluationScores.donorEligibilityDeadline_Yes
        : criteriaEvaluationScores.donorEligibilityDeadline_No) +
      (this.eligibilityForm.CoPartnership === true
        ? criteriaEvaluationScores.donorELigibilityPartnership_Yes
        : criteriaEvaluationScores.donorELigibilityPartnership_No) +
      (this.feasibilityForm.CapacityAvailableForProject === true
        ? criteriaEvaluationScores.feasibilityCapacityForProject_Yes
        : criteriaEvaluationScores.feasibilityCapacityForProject_No) +
      (this.feasibilityForm.TrainedStaff === true
        ? criteriaEvaluationScores.compensationTrainedStaff_Yes
        : criteriaEvaluationScores.compensationTrainedStaff_No) +
      (this.feasibilityForm.ByEquipment === true
        ? criteriaEvaluationScores.compensateByEquipment_Yes
        : criteriaEvaluationScores.compensateByEquipment_No) +
      // error
      (this.feasibilityForm.ExpandScope === true
        ? criteriaEvaluationScores.compensateExpandScope_Yes
        : criteriaEvaluationScores.compensateExpandScope_No) +
      (this.feasibilityForm.GeoGraphicalPresence === true
        ? criteriaEvaluationScores.compensateGeographical_Yes
        : criteriaEvaluationScores.compensateGeographical_No) +
      (this.feasibilityForm.ThirdPartyContract === true
        ? criteriaEvaluationScores.allowedThirdPartyContract_Yes
        : criteriaEvaluationScores.allowedThirdPartyContract_No) +
      (this.feasibilityForm.CostOfCompensationMonth * -1) +
      (this.feasibilityForm.CostOfCompensationMoney >= 5000
        ? (-1 * this.feasibilityForm.CostOfCompensationMoney) / 5000
        : 0) +
      (this.feasibilityForm.AnyInKindComponent === true
        ? criteriaEvaluationScores.anyInKindComponent_Yes
        : criteriaEvaluationScores.anyInKindComponent_No) +

      (this.feasibilityForm.UseableByOrganisation === true
        ? criteriaEvaluationScores.useableByOrganisation_Yes
        : criteriaEvaluationScores.useableByOrganisation_No) +
      (this.feasibilityForm.FeasibleExpertDeploy === true
        ? criteriaEvaluationScores.feasibleExpertDeploy_Yes
        : criteriaEvaluationScores.feasibleExpertDeploy_No) +
      (this.feasibilityForm.FeasibilityExpert === true
        ? criteriaEvaluationScores.feasibilityExpert_Yes
        : criteriaEvaluationScores.feasibilityExpert_No) +
      (this.feasibilityForm.EnoughTimeForProject === true
        ? criteriaEvaluationScores.enoughTimeForQualityWork_Yes
        : criteriaEvaluationScores.enoughTimeForQualityWork_No) +
      (this.feasibilityForm.ProjectAllowedBylaw === true
        ? criteriaEvaluationScores.projectAllowedByLaw_Yes
        : criteriaEvaluationScores.projectAllowedByLaw_No) +
      (this.feasibilityForm.ProjectByLeadership === true
        ? criteriaEvaluationScores.projectAllowByOrganisation_Yes
        : criteriaEvaluationScores.projectAllowByOrganisation_No) +
      (this.feasibilityForm.IsProjectPractical === true
        ? criteriaEvaluationScores.isProjectPractical_Yes
        : criteriaEvaluationScores.isProjectPractical_No) +
      (this.feasibilityForm.PresenceCoverageInProject === true
        ? criteriaEvaluationScores.presenceCoverage_Yes
        : criteriaEvaluationScores.presenceCoverage_No) +
      (this.feasibilityForm.ProjectInLineWithOrgFocus === true
        ? criteriaEvaluationScores.projectinLineWithFocus_Yes
        : criteriaEvaluationScores.projectinLineWithFocus_No) +
      (this.feasibilityForm.EnoughTimeToPrepareProposal === true
        ? criteriaEvaluationScores.enoughTimeToPrepareproposal_Yes
        : criteriaEvaluationScores.enoughTimeToPrepareproposal_No) +
      // if  don't delete :condition for if the costGreaterThanBudget_Yes value is set to be 0
      // ((this.feasibilityForm.IsCostGreaterthenBudget === false && this.feasibilityForm.IsCostGreaterthenBudget != null)
      // ? criteriaEvaluationScores.costGreaterThanBudget_No : criteriaEvaluationScores.costGreaterThanBudget_Yes) +

      // don't delete :condition for if the costGreaterThanBudget_Yes value is set to be -1
      // (this.feasibilityForm.IsCostGreaterthenBudget === false
      // ? criteriaEvaluationScores.costGreaterThanBudget_No : this.feasibilityForm.IsCostGreaterthenBudget === null ? 0 : criteriaEvaluationScores.costGreaterThanBudget_Yes) +
      (this.feasibilityForm.IsCostGreaterthenBudget === true
        ? criteriaEvaluationScores.costGreaterThanBudget_Yes : criteriaEvaluationScores.costGreaterThanBudget_No) +


      (this.feasibilityForm.IsFinancialContribution === true
        ? criteriaEvaluationScores.financialCopntributionFulfil_Yes
        : criteriaEvaluationScores.financialCopntributionFulfil_No) +
      (this.priorityForm.IncreaseEligibility === true
        ? criteriaEvaluationScores.increasedEligibility_Yes
        : criteriaEvaluationScores.increasedEligibility_No) +
      (this.priorityForm.IncreaseReputation === true
        ? criteriaEvaluationScores.increasedReputation_Yes
        : criteriaEvaluationScores.increasedReputation_No) +
      (this.priorityForm.ImproveDonorRelationship === true
        ? criteriaEvaluationScores.improvedDonorRelationaship_Yes
        : criteriaEvaluationScores.improvedDonorRelationaship_No) +
      (this.priorityForm.GoodCause === true
        ? criteriaEvaluationScores.goodCause_Yes
        : criteriaEvaluationScores.goodCause_No) +
      (this.priorityForm.ImprovePerformancecapacity === true
        ? criteriaEvaluationScores.improvedPerformanceCapacity_Yes
        : criteriaEvaluationScores.improvedPerformanceCapacity_No) +
      (this.priorityForm.SkillImprove === true
        ? criteriaEvaluationScores.skillImprovement_Yes
        : criteriaEvaluationScores.skillImprovement_No) +
      (this.priorityForm.FillingFundingGap === true
        ? criteriaEvaluationScores.fillingfundingGaps_Yes
        : criteriaEvaluationScores.fillingfundingGaps_No) +
      (this.priorityForm.NewSoftware === true
        ? criteriaEvaluationScores.newSoftware_Yes
        : criteriaEvaluationScores.newSoftware_No) +
      (this.priorityForm.NewEquipment === true
        ? criteriaEvaluationScores.newEquipment_Yes
        : criteriaEvaluationScores.newEquipment_No) +
      (this.priorityForm.CoverageAreaExpansion === true
        ? criteriaEvaluationScores.coverageAreaExpansion_Yes
        : criteriaEvaluationScores.coverageAreaExpansion_No) +
      (this.priorityForm.NewTraining === true
        ? criteriaEvaluationScores.newTraining_Yes
        : criteriaEvaluationScores.newTraining_No) +
      (this.priorityForm.Others === true
        ? criteriaEvaluationScores.priorityOther_Yes
        : criteriaEvaluationScores.priorityOther_No) +
      (this.priorityForm.ExpansionGoal === true
        ? criteriaEvaluationScores.projectInlineWithOrganisalGoal_Yes
        : criteriaEvaluationScores.projectInlineWithOrganisalGoal_No) +
      // (this.riskForm.Security === false && this.riskForm.Security != null
      //   ? criteriaEvaluationScores.riskSecurity_No
      //   : criteriaEvaluationScores.riskSecurity_Yes) +
      (this.riskForm.Security === true
        ? criteriaEvaluationScores.riskSecurity_Yes
        : criteriaEvaluationScores.riskSecurity_No) +
      // (this.riskForm.DeliveryFaiLure === false && this.riskForm.DeliveryFaiLure != null
      //   ? criteriaEvaluationScores.deliveryFailure_No
      //   : criteriaEvaluationScores.deliveryFailure_Yes) +
      (this.riskForm.DeliveryFaiLure === true
        ? criteriaEvaluationScores.deliveryFailure_Yes
        : criteriaEvaluationScores.deliveryFailure_No) +
      (this.riskForm.Reputation === true
        ? criteriaEvaluationScores.riskReputation_Yes
        : criteriaEvaluationScores.riskReputation_No) +
      (this.riskForm.FocusDivertingrisk === true
        ? criteriaEvaluationScores.focusDeliveryRisk_Yes
        : criteriaEvaluationScores.focusDeliveryRisk_No) +

      (this.riskForm.OtherOrganizationalHarms === true
        ? criteriaEvaluationScores.otherWayToHarmOrg_Yes
        : criteriaEvaluationScores.otherWayToHarmOrg_No) +
      (this.riskForm.Financiallosses === true
        ? criteriaEvaluationScores.financialLosses_Yes
        : criteriaEvaluationScores.financialLosses_No) +
      (this.riskForm.Opportunityloss === true
        ? criteriaEvaluationScores.opportunityLoss_Yes
        : criteriaEvaluationScores.opportunityLoss_No) +
      (this.riskForm.Probablydelaysinfunding === true
        ? criteriaEvaluationScores.probabilityDelayCuts_Yes
        : criteriaEvaluationScores.probabilityDelayCuts_No);



    return this.totalScore.toFixed(2);

  }

  // convertstring(totalScore) {
  //   let number_parsed: any = parseFloat(totalScore).toFixed(2)
  //   return number_parsed
  // }

  //#region "show / hide"
  toggleInputFieldAge() {
    this.inputFieldAgeFlag = !this.inputFieldAgeFlag;
  }
  toggleInputFieldOccupation() {
    this.inputFieldOccupationFlag = !this.inputFieldOccupationFlag;
  }
  //#endregion

  //#region "onAdd" target benificiary
  onAddtargetBeneficiary(type: number, value: string) {
    if (value != null && value !== undefined && value !== '') {
      let obj: TargetBeneficiaryModel = {
        ProjectId: this.ProjectId,
        TargetType: 0
      };
      if (type === this.Age_ID) {
        this.toggleInputFieldAge();

        obj = {
          TargetId: 0,
          TargetName: value,
          TargetType: this.Age_ID,
          ProjectId: this.ProjectId,
          _index: this.AgeGroupList.length,
          _error: false
        };
        this.AgeGroupList.push(obj);
      } else if (type === this.Occupation_ID) {
        this.toggleInputFieldOccupation();

        obj = {
          TargetId: 0,
          ProjectId: this.ProjectId,
          TargetType: this.Occupation_ID,
          TargetName: value,
          // _index: this.expenseList.length,
          _error: false
        };
        this.OccupationList.push(obj);
      }

      this.addtargetBeneficiaryTypes(obj);
    }
  }
  //#endregion

  //#region "Emit" target benioficary
  onValueChangeEmit(data) {
    // this.edittargetBeneficiaryTypes(data);
  }

  addActionEmit(data) {
    this.addtargetBeneficiaryTypes(data);
  }
  //#endregion

  //#region "add" target benificiary
  addtargetBeneficiaryTypes(model: any) {
    let obj: any = {};

    const index = model._index;
    const TargetType = model.TargetType;

    // error handling
    if (TargetType === this.Age_ID) {
      this.AgeGroupList[index]._error = false;
    } else if (TargetType === this.Occupation_ID) {
      this.OccupationList[index]._error = false;
    }

    obj = {
      TargetId: model.TargetId,
      TargetType: model.TargetType,
      ProjectId: this.ProjectId,
      TargetName: model.TargetName
    };

    this.criteriaEvalService
      .AddEditTargetBeneficiary(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditTargetBeneficiary,
        obj
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {

            if (TargetType === this.Age_ID) {
              this.AgeGroupList[index].TargetId = data.CommonId.LongId;
            } else if (TargetType === this.Occupation_ID) {
              this.OccupationList[index].TargetId = data.CommonId.LongId;
            }
          } else if (data.StatusCode === 400) {
            this.toastr.error(data.Message);


          }
        },
        error => {
          // error handling
          if (TargetType === this.Age_ID) {
            this.AgeGroupList[index]._error = true;
          } else if (TargetType === this.Occupation_ID) {
            this.OccupationList[index]._error = true;
          }
          this.toastr.error('Something went wrong ! Try Again');

        }
      );
  }
  //#endregion






  //#region getallPriorityListByprojectId

  getPriorityListByProjectId(ProjectId: number) {
    this.priorityOtherList = [];
    if (ProjectId != null && ProjectId !== undefined && ProjectId != 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetAllPriorityOtherDetailByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.PriorityOtherDetail != null) {
              data.data.PriorityOtherDetail.forEach(element => {
                this.priorityOtherList.push({
                  PriorityOtherDetailId: element.PriorityOtherDetailId,
                  Name: element.Name,
                  ProjectId: this.ProjectId,
                });
              });
            }

          }
        });
    }
  }
  //#endregion


  //#region "addPriorityOther"
  addPriorityOther(data: IPriorityOtherModel) {
    const obj: IPriorityOtherModel = {
      PriorityOtherDetailId: 0,
      Name: data.Name,
      ProjectId: this.ProjectId,
    };

    this.criteriaEvalService
      .AddEditPriorityOther(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddPriorityOtherDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // add to list
            if (response.CommonId.LongId != null && response.CommonId.LongId != 0) {
              obj.PriorityOtherDetailId = response.CommonId.LongId;
            }
            this.priorityOtherList.push(obj);
          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);
          } else {
            this.toastr.error(response.Message);
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');
        }
      );
  }
  //#endregion

  //#region "editPriorityOther"
  editPriorityOther(model: IPriorityOtherModel) {
    const obj: IPriorityOtherModel = {
      PriorityOtherDetailId: model.PriorityOtherDetailId,
      Name: model.Name,
      ProjectId: this.ProjectId
    };

    // Error handling and loading handling
    const item = this.priorityOtherList.find(x => x.PriorityOtherDetailId === model.PriorityOtherDetailId);
    const index = this.priorityOtherList.indexOf(item);
    this.priorityOtherList[index]._IsLoading = true;
    this.priorityOtherList[index]._IsError = false;

    this.criteriaEvalService
      .AddEditPriorityOther(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EditPriorityOtherDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // error handling
            // this.priorityOtherList[index].PriorityOtherDetailId = response.CommonId.LongId;
            this.priorityOtherList[index]._IsLoading = false;
            this.priorityOtherList[index]._IsError = false;
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.priorityOtherList[index]._IsLoading = false;
            this.priorityOtherList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.priorityOtherList[index]._IsLoading = false;
            this.priorityOtherList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.priorityOtherList[index]._IsLoading = false;
          this.priorityOtherList[index]._IsError = true;
        }
      );
  }
  //#endregion

  //#region "deletePriorityOther"
  deletePriorityOther(model: IPriorityOtherModel) {
    const obj: IPriorityOtherModel = {
      PriorityOtherDetailId: model.PriorityOtherDetailId,
      Name: model.Name,
      ProjectId: this.ProjectId
    };

    // Error handling and loading handling
    const item = this.priorityOtherList.find(x => x.PriorityOtherDetailId === model.PriorityOtherDetailId);
    const index = this.priorityOtherList.indexOf(item);
    this.priorityOtherList[index]._IsDeleted = true;
    this.priorityOtherList[index]._IsLoading = true;
    this.priorityOtherList[index]._IsError = false;

    this.criteriaEvalService
      .DeletePriorityDetailByPriorityId(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DeletePriorityOtherDetail, obj.PriorityOtherDetailId

      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // remove when successfully deleted
            this.priorityOtherList.splice(index, 1);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.priorityOtherList[index]._IsDeleted = false;
            this.priorityOtherList[index]._IsLoading = false;
            this.priorityOtherList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.priorityOtherList[index]._IsDeleted = false;
            this.priorityOtherList[index]._IsLoading = false;
            this.priorityOtherList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.priorityOtherList[index]._IsDeleted = false;
          this.priorityOtherList[index]._IsLoading = false;
          this.priorityOtherList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');

        }
      );
  }
  //#endregion

  //#region "onAddPriorityOther" Click
  onAddPriorityOther() {

    const obj: IPriorityOtherModel = {
      PriorityOtherDetailId: 0,
      Name: '',
      ProjectId: null,
      _IsDeleted: false,
      _IsError: false,
      _IsLoading: false
    };

    this.addPriorityOther(obj);
  }
  //#endregion

  //#region "onEditpriorityOtherEmit"
  onEditpriorityOtherEmit(value: IPriorityOtherModel) {

    const obj: IPriorityOtherModel = {
      PriorityOtherDetailId: value.PriorityOtherDetailId,
      Name: value.Name,
      ProjectId: value.ProjectId
    };

    this.editPriorityOther(obj);
  }
  //#endregion

  //#region "onDeletepriorityOtherEmit"
  onDeletepriorityOtherEmit(value) {

    const obj: IPriorityOtherModel = {
      PriorityOtherDetailId: value.PriorityOtherDetailId,
      Name: value.Name,
      ProjectId: this.ProjectId,
    };

    this.deletePriorityOther(obj);
  }
  //#endregion



  //#region getFeasibility

  getFeasibilityExpertByProjectId(ProjectId: number) {
    this.feasivilityList = [];
    if (ProjectId != null && ProjectId !== undefined && ProjectId != 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetAllFeasibilityExpertDetailByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.FeasibilityExpertOtherDetail != null) {
              data.data.FeasibilityExpertOtherDetail.forEach(element => {
                this.feasivilityList.push({
                  ExpertOtherDetailId: element.ExpertOtherDetailId,
                  Name: element.Name,
                  ProjectId: element.ProjectId,
                });
              });
            }

          }
        });
    }
  }
  //#endregion

  //#region onAddFeasibitlityExpertOther click
  onAddFeasibitlityExpertOther() {

    const obj: IFeasibilityExpert = {
      ExpertOtherDetailId: 0,
      Name: '',
      ProjectId: this.ProjectId,
      _IsDeleted: false,
      _IsError: false,
      _IsLoading: false
    };

    this.addFeasibilityExpert(obj);
  }
  //#endregion

  //#region  addFeasibilityExpert

  addFeasibilityExpert(data: IFeasibilityExpert) {
    const obj: IFeasibilityExpert = {
      ExpertOtherDetailId: data.ExpertOtherDetailId,
      Name: data.Name,
      ProjectId: data.ProjectId,
    };

    this.criteriaEvalService
      .AddEditFeasibilityExpert(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddFeasibilityExpertOtherDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // add to list
            if (response.CommonId.LongId != null && response.CommonId.LongId != 0) {
              obj.ExpertOtherDetailId = response.data.LongId;
            }
            this.feasivilityList.push(obj);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);
          } else {
            this.toastr.error(response.Message);
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');
        }
      );
  }
  //#endregion

  //#region onEditFeasibilityExpertEmit click
  onEditFeasibilityExpertEmit(value: IFeasibilityExpert) {
    const obj: IFeasibilityExpert = {
      ExpertOtherDetailId: value.ExpertOtherDetailId,
      Name: value.Name,
      ProjectId: this.ProjectId
    };

    // this.editPriorityOther(obj);
    this.editFeasibilityExpert(obj);

  }
  //#endregion

  //#region editFeasibilityExpert
  editFeasibilityExpert(model: IFeasibilityExpert) {
    const obj: IFeasibilityExpert = {
      ExpertOtherDetailId: model.ExpertOtherDetailId,
      Name: model.Name,
      ProjectId: model.ProjectId
    };

    // Error handling and loading handling
    const item = this.feasivilityList.find(x => x.ExpertOtherDetailId === model.ExpertOtherDetailId);
    const index = this.feasivilityList.indexOf(item);
    this.feasivilityList[index]._IsLoading = true;
    this.feasivilityList[index]._IsError = false;

    this.criteriaEvalService
      .AddEditFeasibilityExpert(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EditFeasibilityExpertDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // error handling
            // this.feasivilityList[index].ExpertOtherDetailId = response.data.LongId;
            this.feasivilityList[index]._IsLoading = false;
            this.feasivilityList[index]._IsError = false;
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);
            // error handling
            this.feasivilityList[index]._IsLoading = false;
            this.feasivilityList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.feasivilityList[index]._IsLoading = false;
            this.feasivilityList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.feasivilityList[index]._IsLoading = false;
          this.feasivilityList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');
        }
      );
  }
  //#endregion

  //#region onDeleteExpertEmit click
  onDeleteExpertEmit(value) {

    const obj: IFeasibilityExpert = {
      ExpertOtherDetailId: value.ExpertOtherDetailId,
      Name: value.Name,
      ProjectId: this.ProjectId,
    };

    this.deleteExpertOther(obj);
  }

  //#endregion

  //#region deleteExpertOther
  deleteExpertOther(model: IFeasibilityExpert) {
    const obj: IFeasibilityExpert = {
      ExpertOtherDetailId: model.ExpertOtherDetailId,
      Name: model.Name,
      ProjectId: model.ProjectId
    };

    // Error handling and loading handling
    const item = this.feasivilityList.find(x => x.ExpertOtherDetailId === model.ExpertOtherDetailId);
    const index = this.feasivilityList.indexOf(item);
    this.feasivilityList[index]._IsDeleted = true;
    this.feasivilityList[index]._IsLoading = true;
    this.feasivilityList[index]._IsError = false;

    this.criteriaEvalService
      .DeletePriorityDetailByPriorityId(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DeleteExpertDetail,
        obj.ExpertOtherDetailId
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // remove when successfully deleted
            this.feasivilityList.splice(index, 1);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);
            // error handling
            this.feasivilityList[index]._IsDeleted = false;
            this.feasivilityList[index]._IsLoading = false;
            this.feasivilityList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.feasivilityList[index]._IsDeleted = false;
            this.feasivilityList[index]._IsLoading = false;
            this.feasivilityList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.feasivilityList[index]._IsDeleted = false;
          this.feasivilityList[index]._IsLoading = false;
          this.feasivilityList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');

        }
      );
  }
  //#endregion



  //#region getassumptionByprojectId
  getAssumptionByprojectId(ProjectId: number) {
    this.assumptionList = [];
    if (ProjectId != null && ProjectId !== undefined && ProjectId != 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetAllAssumptionDetailByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.CEAssumptionDetail != null) {
              data.data.CEAssumptionDetail.forEach(element => {
                this.assumptionList.push({
                  AssumptionDetailId: element.AssumptionDetailId,
                  Name: element.Name,
                  ProjectId: element.ProjectId,
                });
              });
            }

          }
        });
    }
  }
  //#endregion

  //#region onAddAssumption click
  onAddAssumption() {
    const obj: ICEAssumptionModel = {
      AssumptionDetailId: 0,
      Name: '',
      ProjectId: null,
      _IsDeleted: false,
      _IsError: false,
      _IsLoading: false
    };
    this.addCEassumption(obj);
  }
  //#endregion

  //#region  addCEassumption
  addCEassumption(data: ICEAssumptionModel) {
    const obj: ICEAssumptionModel = {
      AssumptionDetailId: 0,
      Name: data.Name,
      ProjectId: this.ProjectId,
    };

    this.criteriaEvalService
      .AddEditAssumptionDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddCEassumptionDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // add to list
            if (response.CommonId.LongId != null && response.CommonId.LongId != 0) {
              obj.AssumptionDetailId = response.CommonId.LongId;
            }
            this.assumptionList.push(obj);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);
          } else {
            this.toastr.error(response.Message);
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');

        }
      );
  }

  //#endregion

  //#region onEditCEassumptionEmit
  onEditCEassumptionEmit(value) {

    const obj: ICEAssumptionModel = {
      AssumptionDetailId: value.AssumptionDetailId,
      Name: value.Name,
      ProjectId: this.ProjectId
    };

    // this.editPriorityOther(obj);
    this.editCEassumptionDetail(obj);
  }
  //#endregion

  //#region editCEassumptionDetail
  editCEassumptionDetail(model: ICEAssumptionModel) {
    const obj: ICEAssumptionModel = {
      AssumptionDetailId: model.AssumptionDetailId,
      Name: model.Name,
      ProjectId: model.ProjectId
    };

    // Error handling and loading handling
    const item = this.assumptionList.find(x => x.AssumptionDetailId === model.AssumptionDetailId);
    const index = this.assumptionList.indexOf(item);
    this.assumptionList[index]._IsLoading = true;
    this.assumptionList[index]._IsError = false;

    this.criteriaEvalService
      .AddEditAssumptionDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EditCEassumptionDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // error handling
            // this.assumptionList[index].AssumptionDetailId = response.CommonId.LongId;
            this.assumptionList[index]._IsLoading = false;
            this.assumptionList[index]._IsError = false;
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.assumptionList[index]._IsLoading = false;
            this.assumptionList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.assumptionList[index]._IsLoading = false;
            this.assumptionList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.assumptionList[index]._IsLoading = false;
          this.assumptionList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');

        }
      );
  }
  //#endregion

  //#region onDeleteCEassumptionEmit
  onDeleteCEassumptionEmit(value) {

    const obj: ICEAssumptionModel = {
      AssumptionDetailId: value.AssumptionDetailId,
      Name: value.Name,
      ProjectId: null,
    };

    this.deleteCEAssumptionther(obj);
  }
  //#endregion

  //#region deleteCEAssumptionther

  deleteCEAssumptionther(model: ICEAssumptionModel) {
    const obj: ICEAssumptionModel = {
      AssumptionDetailId: model.AssumptionDetailId,
      Name: model.Name,
      ProjectId: this.ProjectId
    };

    // Error handling and loading handling
    const item = this.assumptionList.find(x => x.AssumptionDetailId === model.AssumptionDetailId);
    const index = this.assumptionList.indexOf(item);
    this.assumptionList[index]._IsDeleted = true;
    this.assumptionList[index]._IsLoading = true;
    this.assumptionList[index]._IsError = false;

    this.criteriaEvalService
      .DeletePriorityDetailByPriorityId(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DeleteCEassumptionDetail,
        obj.AssumptionDetailId
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // remove when successfully deleted
            this.assumptionList.splice(index, 1);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.assumptionList[index]._IsDeleted = false;
            this.assumptionList[index]._IsLoading = false;
            this.assumptionList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.assumptionList[index]._IsDeleted = false;
            this.assumptionList[index]._IsLoading = false;
            this.assumptionList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.assumptionList[index]._IsDeleted = false;
          this.assumptionList[index]._IsLoading = false;
          this.assumptionList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');

        }
      );
  }
  //#endregion




  //#region GetAgegroupByProjectId
  GetAgegroupByProjectId(ProjectId: number) {
    this.ageGroupList = [];
    if (ProjectId != null && ProjectId !== undefined && ProjectId != 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetAllAgeDetailDetailByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.CEAgeGroupDetail != null) {
              data.data.CEAgeGroupDetail.forEach(element => {
                this.ageGroupList.push({
                  AgeGroupOtherDetailId: element.AgeGroupOtherDetailId,
                  Name: element.Name,
                  ProjectId: element.ProjectId,
                });
              });
            }

          }
        });
    }
  }
  //#endregion

  //#region onAddAgeDetail
  onAddAgeDetail() {
    const obj: ICEAgeDEtailModel = {
      AgeGroupOtherDetailId: 0,
      Name: '',
      ProjectId: null,
      _IsDeleted: false,
      _IsError: false,
      _IsLoading: false
    };
    this.addCEAgeDetail(obj);
  }
  //#endregion

  //#region addCEAgeDetail

  addCEAgeDetail(data: ICEAgeDEtailModel) {
    const obj: ICEAgeDEtailModel = {
      AgeGroupOtherDetailId: 0,
      Name: data.Name,
      ProjectId: this.ProjectId,
    };

    this.criteriaEvalService
      .AddEditAgeGroupDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddCEAgeDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // add to list
            if (response.CommonId.LongId != null && response.CommonId.LongId !== 0) {
              obj.AgeGroupOtherDetailId = response.CommonId.LongId;
            }
            this.ageGroupList.push(obj);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);
          } else {
            this.toastr.error(response.Message);
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');

        }
      );

  }
  //#endregion


  //#region onEditCEAgeDetailEmit
  onEditCEAgeDetailEmit(value) {

    const obj: ICEAgeDEtailModel = {
      AgeGroupOtherDetailId: value.AgeGroupOtherDetailId,
      Name: value.Name,
      ProjectId: value.ProjectId
    };

    // this.editPriorityOther(obj);
    this.editCEAgeDetail(obj);
  }
  //#endregion

  //#region editCEAgeDetail

  editCEAgeDetail(model: ICEAgeDEtailModel) {
    const obj: ICEAgeDEtailModel = {
      AgeGroupOtherDetailId: model.AgeGroupOtherDetailId,
      Name: model.Name,
      ProjectId: this.ProjectId
    };

    // Error handling and loading handling
    const item = this.ageGroupList.find(x => x.AgeGroupOtherDetailId === model.AgeGroupOtherDetailId);
    const index = this.ageGroupList.indexOf(item);
    this.ageGroupList[index]._IsLoading = true;
    this.ageGroupList[index]._IsError = false;

    this.criteriaEvalService
      .AddEditAgeGroupDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EditCEAgeDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // error handling
            // this.ageGroupList[index].AgeGroupOtherDetailId = response.CommonId.LongId;
            this.ageGroupList[index]._IsLoading = false;
            this.ageGroupList[index]._IsError = false;
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.ageGroupList[index]._IsLoading = false;
            this.ageGroupList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.ageGroupList[index]._IsLoading = false;
            this.ageGroupList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.ageGroupList[index]._IsLoading = false;
          this.ageGroupList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');

        }
      );
  }
  //#endregion

  //#region onDeleteCEAgeEmit
  onDeleteCEAgeEmit(value) {

    const obj: ICEAgeDEtailModel = {
      AgeGroupOtherDetailId: value.AgeGroupOtherDetailId,
      Name: value.Name,
      ProjectId: null,
    };

    this.deleteCEAgeDetail(obj);
  }
  //#endregion

  //#region deleteCEAgeDetail
  deleteCEAgeDetail(model: ICEAgeDEtailModel) {
    const obj: ICEAgeDEtailModel = {
      AgeGroupOtherDetailId: model.AgeGroupOtherDetailId,
      Name: model.Name,
      ProjectId: this.ProjectId
    };

    // Error handling and loading handling
    const item = this.ageGroupList.find(x => x.AgeGroupOtherDetailId === model.AgeGroupOtherDetailId);
    const index = this.ageGroupList.indexOf(item);
    this.ageGroupList[index]._IsDeleted = true;
    this.ageGroupList[index]._IsLoading = true;
    this.ageGroupList[index]._IsError = false;

    this.criteriaEvalService
      .DeletePriorityDetailByPriorityId(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DeleteCEAgeDetail,
        obj.AgeGroupOtherDetailId
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // remove when successfully deleted
            this.ageGroupList.splice(index, 1);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.ageGroupList[index]._IsDeleted = false;
            this.ageGroupList[index]._IsLoading = false;
            this.ageGroupList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.ageGroupList[index]._IsDeleted = false;
            this.ageGroupList[index]._IsLoading = false;
            this.ageGroupList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.ageGroupList[index]._IsDeleted = false;
          this.ageGroupList[index]._IsLoading = false;
          this.ageGroupList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');
        }
      );
  }
  //#endregion



  //#region getoccupationByProjectId
  GetOccupationByProjectId(ProjectId: number) {
    this.occupatonList = [];
    if (ProjectId != null && ProjectId !== undefined && ProjectId != 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetAllOccupationDetailByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.CEOccupationDetail != null) {
              data.data.CEOccupationDetail.forEach(element => {
                this.occupatonList.push({
                  OccupationOtherDetailId: element.OccupationOtherDetailId,
                  Name: element.Name,
                  ProjectId: element.ProjectId,
                });
              });
            }

          }
        });
    }
  }
  //#endregion

  //#region onAddOccupationDetail
  onAddOccupationDetail() {
    const obj: ICEOccupationModel = {
      OccupationOtherDetailId: 0,
      Name: '',
      ProjectId: null,
      _IsDeleted: false,
      _IsError: false,
      _IsLoading: false
    };
    this.addCEOccupationDetail(obj);
  }
  //#endregion

  //#region addCEOccupationDetail
  addCEOccupationDetail(data: ICEOccupationModel) {
    const obj: ICEOccupationModel = {
      OccupationOtherDetailId: 0,
      Name: data.Name,
      ProjectId: this.ProjectId,
    };

    this.criteriaEvalService
      .AddEditOccupationOther(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddCEOccupationDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // add to list
            if (response.CommonId.LongId != null && response.CommonId.LongId !== 0) {
              obj.OccupationOtherDetailId = response.CommonId.LongId;
            }
            this.occupatonList.push(obj);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);
          } else {
            this.toastr.error(response.Message);
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');
        }
      );

  }

  //#endregion


  //#region onEditCEOccupationEmit
  onEditCEOccupationEmit(value) {

    const obj: ICEOccupationModel = {
      OccupationOtherDetailId: value.OccupationOtherDetailId,
      Name: value.Name,
      ProjectId: value.ProjectId
    };

    // this.editPriorityOther(obj);
    this.editCEOccupationDetail(obj);

  }
  //#endregion

  //#region editCEOccupationDetail
  editCEOccupationDetail(model: ICEOccupationModel) {
    const obj: ICEOccupationModel = {
      OccupationOtherDetailId: model.OccupationOtherDetailId,
      Name: model.Name,
      ProjectId: this.ProjectId
    };

    // Error handling and loading handling
    const item = this.occupatonList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
    const index = this.occupatonList.indexOf(item);
    this.occupatonList[index]._IsLoading = true;
    this.occupatonList[index]._IsError = false;

    this.criteriaEvalService
      .AddEditOccupationOther(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EditCEoccupationDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // error handling
            // this.occupatonList[index].OccupationOtherDetailId = response.CommonId.LongId;
            this.occupatonList[index]._IsLoading = false;
            this.occupatonList[index]._IsError = false;
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.occupatonList[index]._IsLoading = false;
            this.occupatonList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.occupatonList[index]._IsLoading = false;
            this.occupatonList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.occupatonList[index]._IsLoading = false;
          this.occupatonList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');
        }
      );
  }
  //#endregion

  //#region onDeleteCEOccupationEmit

  onDeleteCEOccupationEmit(value) {

    const obj: ICEOccupationModel = {
      OccupationOtherDetailId: value.OccupationOtherDetailId,
      Name: value.Name,
      ProjectId: null,
    };

    this.deleteCEOccupationDetail(obj);
  }
  //#endregion

  //#region deleteCEOccupationDetail
  deleteCEOccupationDetail(model: ICEOccupationModel) {
    const obj: ICEOccupationModel = {
      OccupationOtherDetailId: model.OccupationOtherDetailId,
      Name: model.Name,
      ProjectId: this.ProjectId
    };

    // Error handling and loading handling
    const item = this.occupatonList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
    const index = this.occupatonList.indexOf(item);
    this.occupatonList[index]._IsDeleted = true;
    this.occupatonList[index]._IsLoading = true;
    this.occupatonList[index]._IsError = false;

    this.criteriaEvalService
      .DeletePriorityDetailByPriorityId(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DeleteCEoccupationDetail,
        obj.OccupationOtherDetailId
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // remove when successfully deleted
            this.occupatonList.splice(index, 1);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.occupatonList[index]._IsDeleted = false;
            this.occupatonList[index]._IsLoading = false;
            this.occupatonList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.occupatonList[index]._IsDeleted = false;
            this.occupatonList[index]._IsLoading = false;
            this.occupatonList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.occupatonList[index]._IsDeleted = false;
          this.occupatonList[index]._IsLoading = false;
          this.priorityOtherList[index]._IsError = true;


        }
      );
  }
  //#endregion




  //#region getCriteriaEvaluationByProjectId
  GetDonorEligibilityCriteriaByProjectId(ProjectId: number) {
    this.donorEligibilityList = [];
    if (ProjectId != null && ProjectId !== undefined && ProjectId != 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetAlldonorEligibilityByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.DonorEligibilityCriteria != null) {
              data.data.DonorEligibilityCriteria.forEach(element => {
                this.donorEligibilityList.push({
                  DonorEligibilityDetailId: element.DonorEligibilityDetailId,
                  Name: element.Name,
                  ProjectId: element.ProjectId,
                });
              });
            }

          }
        });
    }
  }

  //#endregion

  //#region onAddDonorEligibilityDetail
  onAddDonorEligibilityDetail() {
    const obj: ICEDonorEligibilityModel = {
      DonorEligibilityDetailId: 0,
      Name: '',
      ProjectId: null,
      _IsDeleted: false,
      _IsError: false,
      _IsLoading: false
    };
    this.addCEDonorEligibilityDetail(obj);
  }
  //#endregion

  //#region addCEDonorEligibilityDetail
  addCEDonorEligibilityDetail(data: ICEDonorEligibilityModel) {
    const obj: ICEDonorEligibilityModel = {
      DonorEligibilityDetailId: 0,
      Name: data.Name,
      ProjectId: this.ProjectId,
    };

    this.criteriaEvalService
      .AddEditDonorEligibilityOther(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DonorEligibilityDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // add to list
            if (response.CommonId.LongId != null && response.CommonId.LongId !== 0) {
              obj.DonorEligibilityDetailId = response.CommonId.LongId;
            }
            this.donorEligibilityList.push(obj);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);
          } else {
            this.toastr.error(response.Message);
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');

        }
      );

  }

  //#endregion

  //#region onEditCEDonorEligibilityEmit
  onEditCEDonorEligibilityEmit(value) {

    const obj: ICEDonorEligibilityModel = {
      DonorEligibilityDetailId: value.DonorEligibilityDetailId,
      Name: value.Name,
      ProjectId: value.ProjectId
    };

    // this.editPriorityOther(obj);
    this.editCEDonorEligibilityDetail(obj);

  }
  //#endregion

  //#region editCEOccupationDetail
  editCEDonorEligibilityDetail(model: ICEDonorEligibilityModel) {
    const obj: ICEDonorEligibilityModel = {
      DonorEligibilityDetailId: model.DonorEligibilityDetailId,
      Name: model.Name,
      ProjectId: this.ProjectId
    };

    // Error handling and loading handling
    const item = this.donorEligibilityList.find(x => x.DonorEligibilityDetailId === model.DonorEligibilityDetailId);
    const index = this.donorEligibilityList.indexOf(item);
    this.donorEligibilityList[index]._IsLoading = true;
    this.donorEligibilityList[index]._IsError = false;

    this.criteriaEvalService
      .AddEditDonorEligibilityOther(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EditCEdonorEligibilityDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // error handling
            // this.occupatonList[index].OccupationOtherDetailId = response.CommonId.LongId;
            this.donorEligibilityList[index]._IsLoading = false;
            this.donorEligibilityList[index]._IsError = false;
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.donorEligibilityList[index]._IsLoading = false;
            this.donorEligibilityList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);

            // error handling
            this.donorEligibilityList[index]._IsLoading = false;
            this.donorEligibilityList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.donorEligibilityList[index]._IsLoading = false;
          this.donorEligibilityList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');

        }
      );
  }
  //#endregion

  //#region onDeleteCEDonorEligibilityEmit

  onDeleteCEDonorEligibilityEmit(value) {

    const obj: ICEDonorEligibilityModel = {
      DonorEligibilityDetailId: value.DonorEligibilityDetailId,
      Name: value.Name,
      ProjectId: null,
    };

    this.deleteCEDonorEligibilityDetail(obj);
  }
  //#endregion

  //#region deleteCEOccupationDetail
  deleteCEDonorEligibilityDetail(model: ICEDonorEligibilityModel) {
    const obj: ICEDonorEligibilityModel = {
      DonorEligibilityDetailId: model.DonorEligibilityDetailId,
      Name: model.Name,
      ProjectId: this.ProjectId
    };

    // Error handling and loading handling
    const item = this.donorEligibilityList.find(x => x.DonorEligibilityDetailId === model.DonorEligibilityDetailId);
    const index = this.donorEligibilityList.indexOf(item);
    this.donorEligibilityList[index]._IsDeleted = true;
    this.donorEligibilityList[index]._IsLoading = true;
    this.donorEligibilityList[index]._IsError = false;

    this.criteriaEvalService
      .DeletePriorityDetailByPriorityId(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DeleteDonorEligibilityDetail,
        obj.DonorEligibilityDetailId
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // remove when successfully deleted
            this.donorEligibilityList.splice(index, 1);
          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // error handling
            this.donorEligibilityList[index]._IsDeleted = false;
            this.donorEligibilityList[index]._IsLoading = false;
            this.donorEligibilityList[index]._IsError = true;
          } else {
            this.toastr.error(response.Message);
            // error handling
            this.donorEligibilityList[index]._IsDeleted = false;
            this.donorEligibilityList[index]._IsLoading = false;
            this.donorEligibilityList[index]._IsError = true;
          }
        },
        error => {
          // error handling
          this.donorEligibilityList[index]._IsDeleted = false;
          this.donorEligibilityList[index]._IsLoading = false;
          this.donorEligibilityList[index]._IsError = true;
          this.toastr.error('Something went wrong ! Try Again');

        }
      );
  }
  //#endregion



  //#region to check the isCriteiaEvaluationSUBMIT
  OnCriteriaEvaluationSubmitChange(ev) {
    if (ev === 'IsCESubmit') {
      this.startCriteriaEvaluationSubmitLoader = true;
      this.IsSubmitCEform.IsCriteriaEvaluationSubmit = true,
        this.IsSubmitCEform.ProjectId = this.ProjectId;

      // this.editPriorityOther(obj);
      this.AddIsSubmitCEDetail(this.IsSubmitCEform);

    }
  }
  //#endregion

  //#region AddIsDeletedEligibilityDetail
  AddIsSubmitCEDetail(model: ICEisCESubmitModel) {
    const obj: ICEisCESubmitModel = {
      IsCriteriaEvaluationSubmit: model.IsCriteriaEvaluationSubmit,
      ProjectId: model.ProjectId
    };

    // this.commonLoader.showLoader();

    this.criteriaEvalService
      .AddIsCriteriaEvaluationSubmit(
        this.appurl.getApiUrl() + GLOBAL.API_Project_IsCriteriaEvaluationSubmit,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            this.startCriteriaEvaluationSubmitLoader = false;
            this.IsSubmitCEform.IsCriteriaEvaluationSubmit = response.CommonId.IsApproved;
            this.isCriteriaEvaluationFormSubmit.emit(this.IsSubmitCEform.IsCriteriaEvaluationSubmit);
          }
          // this.commonLoader.hideLoader();
        },
        (error) => {
          // this.commonLoader.hideLoader();
        });

  }
  //#endregion
}
