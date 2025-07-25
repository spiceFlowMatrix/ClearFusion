import {
  Component,
  OnInit,
  HostListener,
  EventEmitter,
  Output,
  ChangeDetectorRef,
  AfterViewChecked,
  OnDestroy,
  OnChanges
} from '@angular/core';
import {
  DonorCEModel,
  EligibilityCEModel,
  FeasibilityCEModel,
  PriorityCEmodel,
  FinancialProfitabilityModel,
  RiskSecurityModel,
  ProductAndServiceCEModel,
  TargetBeneficiaryModel,
  FinancialProjectDetailModel,
  CurrencyModel,
  CurrencyDetailModel
} from '../project-details/models/project-details.model';
import { CriteriaEvaluationService } from '../service/criteria-evaluation.service';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { FormControl, Validators } from '@angular/forms';
import { SelectItem } from 'primeng/primeng';
import { ToastrService } from 'ngx-toastr';
import {
  IPriorityOtherModel,
  IFeasibilityExpert,
  ICEAssumptionModel,
  ICEAgeDEtailModel,
  ICEOccupationModel,
  ICEDonorEligibilityModel,
  ICEisCESubmitModel,
  ICriteriaEvaluationPdfModel
} from './criteria-evaluation.model';
import { ActivatedRoute, Router } from '@angular/router';
import {
  TargetBeneficiaryTypes_Enum,
  criteriaEvaluationScores
} from 'src/app/shared/enum';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { ProjectListService } from '../service/project-list.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
@Component({
  selector: 'app-criteria-evaluation',
  templateUrl: './criteria-evaluation.component.html',
  styleUrls: ['./criteria-evaluation.component.scss']
})
export class CriteriaEvaluationComponent
  implements OnInit, AfterViewChecked, OnDestroy {
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

  AllowedByLaw = false; // add one more property
  isSuperadmin = false;

  developmenmtValue = 0;
  educationValue = 0;
  healthNutritionValue = 0;
  disasterRiskValue = 0;
  aggricultureLiveStockValue = 0;

  //#region "Variables"
  methodOfFundingList = [
    { Id: 1, Name: 'Sole (Score: 2)' },
    { Id: 2, Name: 'Source/Co-finance (Score: 1)' }
  ];

  financialHistory = [
    { Id: 1, Name: 'Good/Bad' }
    // { Id: 2, Name: 'Neutral' },
    // { Id: 3, Name: 'Bad' }
  ];

  sectorAndThemes = [
    {
      Id: 1,
      Name: 'Products'
    },
    { Id: 2, Name: 'Services' }
  ];

  disableCriteriaEvaluationForm = false;
  isExpanded = true;
  isDisabledCriticism = true;
  isDisabledEligibilityCriteria: boolean;
  // isDisabledEligibilityCriteria = false;
  isDisabledCompensation = true;
  isDisabledAnyInKindComponent = true;
  isCostGreaterThanBudget = true;
  isDisabledTotal = true;
  isDisabledRisk = true;
  isDisabledReputation = true;
  isDisabledDelivery = true;
  IsOtherOrganizationalHarms = true;
  isOpportunityLoss = true;
  isPriorityOther = false;
  isFeasibilityExpert = false;
  startCriteriaEvaluationSubmitLoader = false;
  criteriaEvaluationLoader = false;

  donorCEForm: DonorCEModel;
  eligibilityForm: EligibilityCEModel;
  feasibilityForm: FeasibilityCEModel;
  priorityForm: PriorityCEmodel;
  financialForm: FinancialProfitabilityModel;
  riskForm: RiskSecurityModel;
  productAndServiceForm: ProductAndServiceCEModel;
  projectSelctionForm: FinancialProjectDetailModel;
  IsSubmitCEform: ICEisCESubmitModel;
  currencyDetailModel: CurrencyDetailModel;
  ProjectId: any;
  CostOfCompensation: FormControl;
  TotalProjectActivity: any;
  submitButton: any;

  // Count values
  proposalExperiemce = '';

  //#region  Donor eligibility creteia variables
  onDonorELegibilityCrteria = 0;
  donorEligibilityDeadline = 0;
  donorEligibilityPartnership = 0;
  //#endregion

  //#region  feasibilty variable
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
  financialContribution = 0;
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
  CurrencyList: CurrencyModel[];

  // to set project header menu
  menuList: IMenuList[] = [];
  authorizedMenuList: IMenuList[] = [];
  setProjectHeader = 'Project';

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  //#endregion

  //#region Input/Output
  @Output() isCriteriaEvaluationFormSubmit = new EventEmitter();
  //#endregion

  constructor(
    private routeActive: ActivatedRoute,
    private appurl: AppUrlService,
    public criteriaEvalService: CriteriaEvaluationService,
    public toastr: ToastrService,
    private cdRef: ChangeDetectorRef,

    private globalService: GlobalSharedService,
    private localStorageService: LocalStorageService,
    public projectListService: ProjectListService,
    private router: Router
  ) {
    // set menu header for project listing
    this.menuList = [];
    this.projectListService.menuList.forEach(x => {
      this.menuList.push({
        Id: x.Id,
        PageId: x.PageId,
        Text: x.Text,
        Link:
          // '/project/my-project/' +
          // this.projectListService.selectedProject() +
          x.Link
      });
    });

    this.menuList.map(
      x =>
        (x.Link =
          this.router.url.substr(0, this.router.url.lastIndexOf('/') + 1) +
          x.Link)
    ); // important for routing

    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);
    // this.initFeasibilityModel();
  }

  ngOnInit() {
    this.routeActive.parent.params.subscribe(params => {
      this.ProjectId = +params['id'];
    });
    // this.getData();
    this.getScreenSize();
    this.GetCriteraiEvaluationDetailById(this.ProjectId);
    this.initializeList();
    this.initilizeDonorEligibilityList();
    this.initDonorCEModel();
    this.initEligibilityModel();
    this.initFeasibilityModel();
    this.initPriorityModel();
    this.initFinancialProfitabilityModel();
    this.initRiskModel();
    this.initProductAndServiceModel();
    this.initProjectSelctionModel();
    this.initIsSubmitCE();
    this.initCurrencyDetailModel();
    this.CostOfCompensation = new FormControl('', [
      Validators.max(12),
      Validators.min(1)
    ]);
    this.GetAllProjectList();
    this.GetAllCurrency();
    this.getPriorityListByProjectId(this.ProjectId);
    this.getAssumptionByprojectId(this.ProjectId);
    // this.GetAgegroupByProjectId(this.ProjectId);
    this.getFeasibilityExpertByProjectId(this.ProjectId);
    // this.GetOccupationByProjectId(this.ProjectId);
    this.GetDonorEligibilityCriteriaByProjectId(this.ProjectId);
    //  this.onProjectallowedByLawChange(this.feasibilityForm.ProjectAllowedBylaw);
    this.onTabchangeScore(0);
  }

  // to show warning message if the total score is less than 30 -->
  ngAfterViewChecked() {
    const isAllowedByLaw = this.feasibilityForm.ProjectAllowedBylaw;
    if (isAllowedByLaw !== this.AllowedByLaw) {
      // check if it change, tell CD update view
      this.AllowedByLaw = isAllowedByLaw;
      this.cdRef.detectChanges();
    }
  }
  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 190 + 'px',
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
      RuralDevelopmentSocialProtection: null,
      HealthAndNutrition: null,
      DisasterRiskReduction: null,
      AgricultureAndLivestock: null,
      Education: null,
      Awareness: null,
      Infrastructure: null,
      CapacityBuilding: null,
      IncomeGeneration: null,
      Mobilization: null,
      PeaceBuilding: null,
      SocialProtection: null,
      SustainableLivelihood: null,
      Advocacy: null,
      Literacy: null,
      EducationCapacityBuilding: null,
      SchoolUpgrading: null,
      EducationInEmergency: null,
      OnlineEducation: null,
      CommunityBasedEducation: null,
      AcceleratedLearningProgram: null,
      PrimaryHealthServices: null,
      ReproductiveHealth: null,
      Immunization: null,
      InfantandYoungChildFeeding: null,
      Nutrition: null,
      CommunicableDisease: null,
      Hygiene: null,
      EnvironmentalHealth: null,
      MentalHealthandDisabilityService: null,
      HealthCapacityBuilding: null,
      Telemedicine: null,
      MitigationProjects: null,
      WaterSupply: null,
      Sanitation: null,
      DisasterRiskHygiene: null,
      DisasterCapacityBuilding: null,
      EmergencyResponse: null,
      RenewableEnergy: null,
      Shelter: null,
      NaturalResourceManagement: null,
      AggriculutreCapacityBuilding: null,
      LivestockManagement: null,
      FoodSecurity: null,
      ResearchandPublication: null,
      Horticulture: null,
      Irrigation: null,
      Livelihood: null,
      ValueChain: null,

      Children: null,
      Disabled: null,
      IDPs: null,
      Returnees: null,
      Kuchis: null,
      Widows: null,
      Women: null,
      Men: null,
      Youth: null
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
      IsCostGreaterthenBudget: null
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

      Geographical: null,
      Insecurity: null,
      Season: null,
      Ethnicity: null,
      Culture: null,
      ReligiousBeliefs: null,
      FocusDivertingrisk: null,
      Financiallosses: null,
      Opportunityloss: null,
      ProjectSelectionId: null,
      CurrencyId: null,
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
      }
    ];
  }

  initilizeDonorEligibilityList() {
    this.donorEligibilityList = [
      {
        DonorEligibilityDetailId: null,
        Name: null,
        ProjectId: null,

        _IsDeleted: false,
        _IsLoading: false,
        _IsError: false
      }
    ];
  }

  initIsSubmitCE() {
    this.IsSubmitCEform = {
      ProjectId: null,

      IsCriteriaEvaluationSubmit: false
    };
  }

  initCurrencyDetailModel() {
    this.currencyDetailModel = {
      ProjectId: null,
      CurrencyId: null
    };
  }
  //#endregion

  //#region DonorForm section
  onMethofOfFundingChange(value) {
    this.donorCEForm.MethodOfFunding = value;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  //  DonorForm section
  onPastFundingExperienceChange(value) {
    if (value.checked === true) {
      this.isDisabled = false;
    } else {
      this.isDisabled = true;
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
    }
    this.donorCEForm.PastFundingExperience = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onProposalAcceptChange(value) {
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
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.Professional = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onFundsOnTimeChange(value) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.FundsOnTime = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  onEffectiveCommunicationChange(value) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.EffectiveCommunication = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  onDisputesChange(value) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.Dispute = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onOtherDeliverablesChange(value) {
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
    } else {
      this.isDisabledCriticism = true;
      this.donorCEForm.CriticismPerformance = false;
      this.donorCEForm.TimeManagement = false;
      this.donorCEForm.MoneyAllocation = false;
      this.donorCEForm.Accountability = false;
      this.donorCEForm.DeliverableQuality = false;
    }
    this.donorCEForm.PastWorkingExperience = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onPerformanceChange(value) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.CriticismPerformance = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onTimeManagementChange(value) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.TimeManagement = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onMoneyAllocationChange(value) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.MoneyAllocation = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onAccountabilityChange(value) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.Accountability = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onDeliverableQualityChange(value) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.DeliverableQuality = value.checked;
    this.AddEditDonorCEForm(this.donorCEForm);
  }

  onFinancialHistoryChange(data) {
    // this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    this.donorCEForm.DonorFinancingHistory = data;
    if (data.checked === false) {
      this.donorCEForm.DonorFinancingHistory = 0;
    } else if (data.checked === true) {
      this.donorCEForm.DonorFinancingHistory = 1;
    }
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  // old 11-12-2019
  // onReligiousStandingChange(data) {
  //   this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
  //   this.donorCEForm.ReligiousStanding = data;
  //   this.AddEditDonorCEForm(this.donorCEForm);
  // }

  // new 11-12-2019
  onReligiousStandingChange(data) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    if (data.checked === false) {
      this.donorCEForm.ReligiousStanding = 0;
    } else if (data.checked === true) {
      this.donorCEForm.ReligiousStanding = 1;
    }
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  onPoliticalStandingChange(data) {
    this.donorCEForm.DonorCEId = this.donorCEForm.DonorCEId;
    if (data.checked === false) {
      this.donorCEForm.PoliticalStanding = 0;
    } else if (data.checked === true) {
      this.donorCEForm.PoliticalStanding = 1;
    }
    this.AddEditDonorCEForm(this.donorCEForm);
  }
  //#endregion

  //#region Purpose Of Initiating
  //  ----------------------- Rural development and Social Production ----------------------------------
  onProdAwarenessChange(value) {
    this.productAndServiceForm.Awareness = value.checked;
    this.ruralDevelopmentScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onProdNewonInfrastructureChangesChange(value) {
    this.productAndServiceForm.Infrastructure = value.checked;
    this.ruralDevelopmentScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onCapacityBuildingChange(value) {
    this.productAndServiceForm.CapacityBuilding = value.checked;
    this.ruralDevelopmentScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onIncomeGenerationChange(value) {
    this.ruralDevelopmentScore(value);
    this.productAndServiceForm.IncomeGeneration = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onMobilizationChange(value) {
    this.ruralDevelopmentScore(value);
    this.productAndServiceForm.Mobilization = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onPeaceBuildingChange(value) {
    this.productAndServiceForm.PeaceBuilding = value.checked;
    this.ruralDevelopmentScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onSocialProtectionChange(value) {
    this.productAndServiceForm.SocialProtection = value.checked;
    this.ruralDevelopmentScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onSustainableChange(value) {
    this.productAndServiceForm.SustainableLivelihood = value.checked;
    this.ruralDevelopmentScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onAdvocacyChange(value) {
    this.productAndServiceForm.Advocacy = value.checked;
    this.ruralDevelopmentScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  // -------------------------------  Education -------------------------------------------------

  onLiteracyChange(value) {
    this.productAndServiceForm.Literacy = value.checked;
    this.educationScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onEducationCapacityChange(value) {
    this.productAndServiceForm.EducationCapacityBuilding = value.checked;
    this.educationScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onSchoolUpgardingChange(value) {
    this.productAndServiceForm.SchoolUpgrading = value.checked;
    this.educationScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onEducationInEmergencyChange(value) {
    this.productAndServiceForm.EducationInEmergency = value.checked;
    this.educationScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onOnlineEducationChange(value) {
    this.productAndServiceForm.OnlineEducation = value.checked;
    this.educationScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onCommunityBasedEducationChange(value) {
    this.productAndServiceForm.CommunityBasedEducation = value.checked;
    this.educationScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onAcceleratedChange(value) {
    this.productAndServiceForm.AcceleratedLearningProgram = value.checked;
    this.educationScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  // ---------------------------------------Health and Nutrition --------------------------------------------
  onPrimaryHealthChange(value) {
    this.productAndServiceForm.PrimaryHealthServices = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onReproductiveChange(value) {
    this.healthNutritionScore(value);
    this.productAndServiceForm.ReproductiveHealth = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onImmunizationChange(value) {
    this.productAndServiceForm.Immunization = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onNutritionChange(value) {
    this.productAndServiceForm.Nutrition = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onCommunicableDiseaseChange(value) {
    this.productAndServiceForm.CommunicableDisease = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onHygieneChange(value) {
    this.productAndServiceForm.Hygiene = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onEnvironmentalChange(value) {
    this.productAndServiceForm.EnvironmentalHealth = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onHealthCapacityBuildingChange(value) {
    this.productAndServiceForm.HealthCapacityBuilding = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onTelemedicineChange(value) {
    this.productAndServiceForm.Telemedicine = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onInfantAndChildFeedingChange(value) {
    this.productAndServiceForm.InfantandYoungChildFeeding = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onMentalHealthChange(value) {
    this.productAndServiceForm.MentalHealthandDisabilityService = value.checked;
    this.healthNutritionScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  // -------------------------------------Disaster Risk Reduction --------------------------------------
  onMitigationChange(value) {
    this.productAndServiceForm.MitigationProjects = value.checked;
    this.disasterRiskScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onWaterSupplyChange(value) {
    this.productAndServiceForm.WaterSupply = value.checked;
    this.disasterRiskScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onSanitationChange(value) {
    this.productAndServiceForm.Sanitation = value.checked;
    this.disasterRiskScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onDisasterHygieneChange(value) {
    this.productAndServiceForm.DisasterRiskHygiene = value.checked;
    this.disasterRiskScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onDisasterCapacityBindingChange(value) {
    this.productAndServiceForm.DisasterCapacityBuilding = value.checked;
    this.disasterRiskScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onEmergencyChange(value) {
    this.productAndServiceForm.EmergencyResponse = value.checked;
    this.disasterRiskScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onRenewableChange(value) {
    this.productAndServiceForm.RenewableEnergy = value.checked;
    this.disasterRiskScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onShelterChange(value) {
    this.productAndServiceForm.Shelter = value.checked;
    this.disasterRiskScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  // ------------------------------------------ Agriculture and Livestock -------------------------------
  onNaturalResourceChange(value) {
    this.productAndServiceForm.NaturalResourceManagement = value.checked;
    this.aggricultureScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onAggriculutreCapacityBuildingChange(value) {
    this.productAndServiceForm.AggriculutreCapacityBuilding = value.checked;
    this.aggricultureScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onLivestockChange(value) {
    this.productAndServiceForm.LivestockManagement = value.checked;
    this.aggricultureScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onFoodSrcurityChange(value) {
    this.productAndServiceForm.FoodSecurity = value.checked;
    this.aggricultureScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onResearchAndPublicationChange(value) {
    this.productAndServiceForm.ResearchandPublication = value.checked;
    this.aggricultureScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onHorticultureChange(value) {
    this.productAndServiceForm.Horticulture = value.checked;
    this.aggricultureScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onIrrigationChange(value) {
    this.productAndServiceForm.Irrigation = value.checked;
    this.aggricultureScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onLivelihoodChange(value) {
    this.productAndServiceForm.Livelihood = value.checked;
    this.aggricultureScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onValueChainChange(value) {
    this.productAndServiceForm.ValueChain = value.checked;
    this.aggricultureScore(value);
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  // ---------------------------------- target and beneficiary detail ----------------------------------------
  onWomenChange(value) {
    this.productAndServiceForm.Women = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onMenChange(value) {
    this.productAndServiceForm.Men = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onYouthChange(value) {
    this.productAndServiceForm.Youth = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onChildrenChange(value) {
    this.productAndServiceForm.Children = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onDisabledChange(value: any) {
    this.productAndServiceForm.Disabled = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onIDPsChange(value) {
    this.productAndServiceForm.IDPs = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }

  onReturneesChange(value) {
    this.productAndServiceForm.Returnees = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onKuchisChange(value) {
    this.productAndServiceForm.Kuchis = value.checked;
    this.AddEditPurposeOfInitiatingForm(this.productAndServiceForm);
  }
  onWidowsChange(value) {
    this.productAndServiceForm.Widows = value.checked;
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
    if (value.checked === false) {
      this.isDisabledCompensation = true;
      this.feasibilityForm.TrainedStaff = false;
      this.feasibilityForm.ByEquipment = false;
      this.feasibilityForm.ExpandScope = false;
      this.feasibilityForm.GeoGraphicalPresence = false;
      this.compensationTrainStaff = 0;
      this.compensationByEquipment = 0;
      this.compensationExpandScope = 0;
      this.compensationGeographical = 0;
    } else {
      this.isDisabledCompensation = false;
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
      this.AddEditFeasibilityCEForm(this.feasibilityForm);
    }
    // }
  }
  //#endregion

  //#region AnyInKind component
  onAnyInKindComponentChange(value) {
    if (value.checked === true) {
      this.isDisabledAnyInKindComponent = false;
      // this.anyInKindComponent = criteriaEvaluationScores.anyInKindComponent_Yes;
    } else {
      // Note: to disable the expert record
      this.isFeasibilityExpert = false;
      this.isDisabledAnyInKindComponent = true;
      this.feasibilityForm.UseableByOrganisation = false;
      this.feasibilityForm.FeasibleExpertDeploy = false;
      this.useableByOrganisation = 0;
      this.fesibilityExpertDeployed = 0;
      // this.anyInKindComponent = criteriaEvaluationScores.anyInKindComponent_No;
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
      this.enoughTimeQuality =
        criteriaEvaluationScores.enoughTimeForQualityWork_No;
    }

    this.feasibilityForm.EnoughTimeForProject = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onProjectallowedByLawChange(value) {
    if (value.checked === false) {
      this.isExpanded = false;
      this.disableCriteriaEvaluationForm = true;
    } else if (
      value.checked === true &&
      this.feasibilityForm.ProjectByLeadership === true
    ) {
      // this.projectallowedByLaw =
      //   criteriaEvaluationScores.projectAllowedByLaw_Yes;
      this.isExpanded = true;
      this.disableCriteriaEvaluationForm = false;
      // } else {
      //   this.projectallowedByLaw =
      //     criteriaEvaluationScores.projectAllowedByLaw_No;
    }
    this.feasibilityForm.ProjectAllowedBylaw = value.checked;
    this.AddEditFeasibilityCEForm(this.feasibilityForm);
  }

  onProjectAllowedByOrganisationChange(value) {
    if (value.checked === true) {
      // this.projectAllowedByOrganisation =
      //   criteriaEvaluationScores.projectAllowByOrganisation_Yes;
      if (this.feasibilityForm.ProjectAllowedBylaw === true) {
        this.isExpanded = true;
        this.disableCriteriaEvaluationForm = false;
      }
    } else if (value.checked === false) {
      // this.projectAllowedByOrganisation =
      //   criteriaEvaluationScores.projectAllowByOrganisation_No;
      this.isExpanded = false;
      this.disableCriteriaEvaluationForm = true;
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
    } else {
      this.isCostGreaterThanBudget = true;
      this.feasibilityForm.PerCostGreaterthenBudget = 0;
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
    } else {
      this.isPriorityOther = false;
    }
    this.priorityForm.Others = value.checked;
    this.AddEditPriorityCEForm(this.priorityForm);
  }

  onProjectExpensionGoalChange(value) {
    if (value.checked === true) {
      this.projectExpensionGoal =
        criteriaEvaluationScores.projectInlineWithOrganisalGoal_Yes;
    } else {
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
    } else {
      this.isDisabledRisk = true;
      this.riskForm.Staff = false;
      this.riskForm.ProjectAssets = false;
      this.riskForm.Suppliers = false;
      this.riskForm.Beneficiaries = false;
      this.riskForm.OverallOrganization = false;
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
    } else {
      this.isDisabledReputation = true;
      this.riskForm.Religious = false;
      this.riskForm.Sectarian = false;
      this.riskForm.Ethinc = false;
      this.riskForm.Social = false;
      this.riskForm.Traditional = false;
      this.riskForm.Geographical = false;
      this.riskForm.Insecurity = false;
      this.riskForm.Season = false;
      this.riskForm.Ethnicity = false;
      this.riskForm.Culture = false;
      this.riskForm.ReligiousBeliefs = false;
      this.riskForm.FocusDivertingrisk = false;
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
      if (ev === 'Geographical') {
        this.riskForm.Geographical = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Insecurity') {
        this.riskForm.Insecurity = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Season') {
        this.riskForm.Season = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Ethnicity') {
        this.riskForm.Ethnicity = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'Culture') {
        this.riskForm.Culture = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
      if (ev === 'ReligiousBeliefs') {
        this.riskForm.ReligiousBeliefs = data;
        this.AddEditRiskSecurityCEForm(this.riskForm);
      }
    }
  }

  onFocusDivertingRiskChange(value) {
    this.riskForm.FocusDivertingrisk = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }

  onDeliveryFailureChange(value) {
    if (value.checked === true) {
      this.isDisabledDelivery = false;
    } else {
      this.isDisabledDelivery = true;
      this.riskForm.PrematureSeizure = false;
      this.riskForm.GovernmentConfiscation = false;
      this.riskForm.DesctructionByTerroristActivity = false;
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
    this.riskForm.Financiallosses = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }
  onOpportunityLossChange(value) {
    if (value.checked === true) {
      this.isOpportunityLoss = false;
    } else {
      this.isOpportunityLoss = true;
      this.riskForm.ProjectSelectionId = null;
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
    this.riskForm.Probablydelaysinfunding = value.checked;
    this.AddEditRiskSecurityCEForm(this.riskForm);
  }

  onOtherOrgHarmsChange(value) {
    if (value.checked === true) {
      this.IsOtherOrganizationalHarms = false;
    } else {
      this.IsOtherOrganizationalHarms = true;
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
          }
        }
      });
  }
  //#endregion

  //#region "getAllcurrency"
  GetAllCurrency() {
    this.CurrencyList = [];
    this.criteriaEvalService
      .GetAllCurrency(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency)
      .subscribe(
        data => {
          if (data != null && data.StatusCode === 200) {
            if (data.data.CurrencyList != null) {
              data.data.CurrencyList.forEach(element => {
                this.CurrencyList.push({
                  CurrencyId: element.CurrencyId,
                  CurrencyCode: element.CurrencyCode
                });
              });
            }
          }
        },
        error => {}
      );
  }

  //#endregion

  //#region Get Criteria evaluation by ProjectId Donor
  GetCriteraiEvaluationDetailById(ProjectId: number) {
    this.criteriaEvaluationLoader = true;
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.criteriaEvalService
        .GetCriteriaEvalDetailsByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetAllCriteriaEvaluationDetail,
          ProjectId
        )
        .subscribe(
          data => {
            if (data != null && data.StatusCode === 200) {
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
                this.productAndServiceForm.Awareness =
                  data.data.CriteriaEveluationModel.Awareness;
                if (this.productAndServiceForm.Awareness === true) {
                  this.developmenmtValue = this.developmenmtValue + 1;
                }

                this.productAndServiceForm.Infrastructure =
                  data.data.CriteriaEveluationModel.Infrastructure;
                if (this.productAndServiceForm.Infrastructure === true) {
                  this.developmenmtValue = this.developmenmtValue + 1;
                }
                this.productAndServiceForm.CapacityBuilding =
                  data.data.CriteriaEveluationModel.CapacityBuilding;
                if (this.productAndServiceForm.CapacityBuilding === true) {
                  this.developmenmtValue = this.developmenmtValue + 1;
                }

                this.productAndServiceForm.IncomeGeneration =
                  data.data.CriteriaEveluationModel.IncomeGeneration;
                if (this.productAndServiceForm.IncomeGeneration === true) {
                  this.developmenmtValue = this.developmenmtValue + 1;
                }
                this.productAndServiceForm.Mobilization =
                  data.data.CriteriaEveluationModel.Mobilization;
                if (this.productAndServiceForm.Mobilization === true) {
                  this.developmenmtValue = this.developmenmtValue + 1;
                }
                this.productAndServiceForm.PeaceBuilding =
                  data.data.CriteriaEveluationModel.PeaceBuilding;
                if (this.productAndServiceForm.PeaceBuilding === true) {
                  this.developmenmtValue = this.developmenmtValue + 1;
                }
                this.productAndServiceForm.SocialProtection =
                  data.data.CriteriaEveluationModel.SocialProtection;
                if (this.productAndServiceForm.SocialProtection) {
                  this.developmenmtValue = this.developmenmtValue + 1;
                }

                this.productAndServiceForm.SustainableLivelihood =
                  data.data.CriteriaEveluationModel.SustainableLivelihood;
                if (this.productAndServiceForm.SustainableLivelihood === true) {
                  this.developmenmtValue = this.developmenmtValue + 1;
                }
                this.productAndServiceForm.Advocacy =
                  data.data.CriteriaEveluationModel.Advocacy;
                if (this.productAndServiceForm.Advocacy === true) {
                  this.developmenmtValue = this.developmenmtValue + 1;
                }

                this.productAndServiceForm.Literacy =
                  data.data.CriteriaEveluationModel.Literacy;
                if (this.productAndServiceForm.Literacy === true) {
                  this.educationValue = this.educationValue + 1;
                }
                this.productAndServiceForm.EducationCapacityBuilding =
                  data.data.CriteriaEveluationModel.EducationCapacityBuilding;
                if (
                  this.productAndServiceForm.EducationCapacityBuilding === true
                ) {
                  this.educationValue = this.educationValue + 1;
                }
                this.productAndServiceForm.SchoolUpgrading =
                  data.data.CriteriaEveluationModel.SchoolUpgrading;
                if (this.productAndServiceForm.SchoolUpgrading === true) {
                  this.educationValue = this.educationValue + 1;
                }
                this.productAndServiceForm.EducationInEmergency =
                  data.data.CriteriaEveluationModel.EducationInEmergency;
                if (this.productAndServiceForm.EducationInEmergency === true) {
                  this.educationValue = this.educationValue + 1;
                }
                this.productAndServiceForm.OnlineEducation =
                  data.data.CriteriaEveluationModel.OnlineEducation;
                if (this.productAndServiceForm.OnlineEducation === true) {
                  this.educationValue = this.educationValue + 1;
                }
                this.productAndServiceForm.CommunityBasedEducation =
                  data.data.CriteriaEveluationModel.CommunityBasedEducation;
                if (
                  this.productAndServiceForm.CommunityBasedEducation === true
                ) {
                  this.educationValue = this.educationValue + 1;
                }
                this.productAndServiceForm.AcceleratedLearningProgram =
                  data.data.CriteriaEveluationModel.AcceleratedLearningProgram;
                if (
                  this.productAndServiceForm.AcceleratedLearningProgram == true
                ) {
                  this.educationValue = this.educationValue + 1;
                }
                this.productAndServiceForm.PrimaryHealthServices =
                  data.data.CriteriaEveluationModel.PrimaryHealthServices;
                if (this.productAndServiceForm.PrimaryHealthServices === true) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.ReproductiveHealth =
                  data.data.CriteriaEveluationModel.ReproductiveHealth;
                if (this.productAndServiceForm.ReproductiveHealth === true) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.Immunization =
                  data.data.CriteriaEveluationModel.Immunization;
                if (this.productAndServiceForm.Immunization === true) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.InfantandYoungChildFeeding =
                  data.data.CriteriaEveluationModel.InfantandYoungChildFeeding;
                if (
                  this.productAndServiceForm.InfantandYoungChildFeeding === true
                ) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.Nutrition =
                  data.data.CriteriaEveluationModel.Nutrition;
                if (this.productAndServiceForm.Nutrition === true) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.CommunicableDisease =
                  data.data.CriteriaEveluationModel.CommunicableDisease;
                if (this.productAndServiceForm.CommunicableDisease === true) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.Hygiene =
                  data.data.CriteriaEveluationModel.Hygiene;
                if (this.productAndServiceForm.Hygiene === true) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.EnvironmentalHealth =
                  data.data.CriteriaEveluationModel.EnvironmentalHealth;
                if (this.productAndServiceForm.EnvironmentalHealth === true) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.MentalHealthandDisabilityService =
                  data.data.CriteriaEveluationModel.MentalHealthandDisabilityService;
                if (
                  this.productAndServiceForm
                    .MentalHealthandDisabilityService === true
                ) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.HealthCapacityBuilding =
                  data.data.CriteriaEveluationModel.HealthCapacityBuilding;
                if (
                  this.productAndServiceForm.HealthCapacityBuilding === true
                ) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.Telemedicine =
                  data.data.CriteriaEveluationModel.Telemedicine;
                if (this.productAndServiceForm.Telemedicine === true) {
                  this.healthNutritionValue = this.healthNutritionValue + 1;
                }
                this.productAndServiceForm.MitigationProjects =
                  data.data.CriteriaEveluationModel.MitigationProjects;
                if (this.productAndServiceForm.MitigationProjects === true) {
                  this.disasterRiskValue = this.disasterRiskValue + 1;
                }
                this.productAndServiceForm.WaterSupply =
                  data.data.CriteriaEveluationModel.WaterSupply;
                if (this.productAndServiceForm.WaterSupply === true) {
                  this.disasterRiskValue = this.disasterRiskValue + 1;
                }
                this.productAndServiceForm.Sanitation =
                  data.data.CriteriaEveluationModel.Sanitation;
                if (this.productAndServiceForm.Sanitation === true) {
                  this.disasterRiskValue = this.disasterRiskValue + 1;
                }

                this.productAndServiceForm.DisasterRiskHygiene =
                  data.data.CriteriaEveluationModel.DisasterRiskHygiene;
                if (this.productAndServiceForm.DisasterRiskHygiene === true) {
                  this.disasterRiskValue = this.disasterRiskValue + 1;
                }
                this.productAndServiceForm.DisasterCapacityBuilding =
                  data.data.CriteriaEveluationModel.DisasterCapacityBuilding;
                if (
                  this.productAndServiceForm.DisasterCapacityBuilding === true
                ) {
                  this.disasterRiskValue = this.disasterRiskValue + 1;
                }
                this.productAndServiceForm.EmergencyResponse =
                  data.data.CriteriaEveluationModel.EmergencyResponse;
                if (this.productAndServiceForm.EmergencyResponse === true) {
                  this.disasterRiskValue = this.disasterRiskValue + 1;
                }
                this.productAndServiceForm.RenewableEnergy =
                  data.data.CriteriaEveluationModel.RenewableEnergy;
                if (this.productAndServiceForm.RenewableEnergy === true) {
                  this.disasterRiskValue = this.disasterRiskValue + 1;
                }
                this.productAndServiceForm.Shelter =
                  data.data.CriteriaEveluationModel.Shelter;
                if (this.productAndServiceForm.Shelter === true) {
                  this.disasterRiskValue = this.disasterRiskValue + 1;
                }
                this.productAndServiceForm.NaturalResourceManagement =
                  data.data.CriteriaEveluationModel.NaturalResourceManagement;
                if (
                  this.productAndServiceForm.NaturalResourceManagement === true
                ) {
                  this.aggricultureLiveStockValue =
                    this.aggricultureLiveStockValue + 1;
                }
                this.productAndServiceForm.AggriculutreCapacityBuilding =
                  data.data.CriteriaEveluationModel.AggriculutreCapacityBuilding;
                if (
                  this.productAndServiceForm.AggriculutreCapacityBuilding ===
                  true
                ) {
                  this.aggricultureLiveStockValue =
                    this.aggricultureLiveStockValue + 1;
                }
                this.productAndServiceForm.LivestockManagement =
                  data.data.CriteriaEveluationModel.LivestockManagement;
                if (this.productAndServiceForm.LivestockManagement === true) {
                  this.aggricultureLiveStockValue =
                    this.aggricultureLiveStockValue + 1;
                }
                this.productAndServiceForm.FoodSecurity =
                  data.data.CriteriaEveluationModel.FoodSecurity;
                if (this.productAndServiceForm.FoodSecurity === true) {
                  this.aggricultureLiveStockValue =
                    this.aggricultureLiveStockValue + 1;
                }
                this.productAndServiceForm.ResearchandPublication =
                  data.data.CriteriaEveluationModel.ResearchandPublication;
                if (
                  this.productAndServiceForm.ResearchandPublication === true
                ) {
                  this.aggricultureLiveStockValue =
                    this.aggricultureLiveStockValue + 1;
                }
                this.productAndServiceForm.Horticulture =
                  data.data.CriteriaEveluationModel.Horticulture;
                if (this.productAndServiceForm.Horticulture === true) {
                  this.aggricultureLiveStockValue =
                    this.aggricultureLiveStockValue + 1;
                }
                this.productAndServiceForm.Irrigation =
                  data.data.CriteriaEveluationModel.Irrigation;
                if (this.productAndServiceForm.Irrigation === true) {
                  this.aggricultureLiveStockValue =
                    this.aggricultureLiveStockValue + 1;
                }
                this.productAndServiceForm.Livelihood =
                  data.data.CriteriaEveluationModel.Livelihood;
                if (this.productAndServiceForm.Livelihood === true) {
                  this.aggricultureLiveStockValue =
                    this.aggricultureLiveStockValue + 1;
                }
                this.productAndServiceForm.ValueChain =
                  data.data.CriteriaEveluationModel.ValueChain;
                if (this.productAndServiceForm.ValueChain === true) {
                  this.aggricultureLiveStockValue =
                    this.aggricultureLiveStockValue + 1;
                }
                this.productAndServiceForm.Children =
                  data.data.CriteriaEveluationModel.Children;

                this.productAndServiceForm.Disabled =
                  data.data.CriteriaEveluationModel.Disabled;

                this.productAndServiceForm.IDPs =
                  data.data.CriteriaEveluationModel.IDPs;
                this.productAndServiceForm.Returnees =
                  data.data.CriteriaEveluationModel.Returnees;

                this.productAndServiceForm.Kuchis =
                  data.data.CriteriaEveluationModel.Kuchis;

                this.productAndServiceForm.Widows =
                  data.data.CriteriaEveluationModel.Widows;

                this.productAndServiceForm.Women =
                  data.data.CriteriaEveluationModel.Women;

                this.productAndServiceForm.Men =
                  data.data.CriteriaEveluationModel.Men;

                this.productAndServiceForm.Youth =
                  data.data.CriteriaEveluationModel.Youth;

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
                  this.isDisabledCompensation = false;
                  this.feasibilityForm.TrainedStaff =
                    data.data.CriteriaEveluationModel.TrainedStaff;
                  this.feasibilityForm.ByEquipment =
                    data.data.CriteriaEveluationModel.ByEquipment;
                  this.feasibilityForm.ExpandScope =
                    data.data.CriteriaEveluationModel.ExpandScope;
                  this.feasibilityForm.GeoGraphicalPresence =
                    data.data.CriteriaEveluationModel.GeoGraphicalPresence;
                } else {
                  this.isDisabledCompensation = true;
                }
                this.feasibilityForm.ThirdPartyContract =
                  data.data.CriteriaEveluationModel.ThirdPartyContract;
                if (
                  data.data.CriteriaEveluationModel.CostOfCompensationMonth ==
                    null ||
                  data.data.CriteriaEveluationModel.CostOfCompensationMonth ===
                    undefined
                ) {
                  this.feasibilityForm.CostOfCompensationMonth = null;
                } else {
                  this.feasibilityForm.CostOfCompensationMonth =
                    data.data.CriteriaEveluationModel.CostOfCompensationMonth;
                }
                this.feasibilityForm.CostOfCompensationMoney =
                  data.data.CriteriaEveluationModel.CostOfCompensationMoney;

                this.feasibilityForm.AnyInKindComponent =
                  data.data.CriteriaEveluationModel.AnyInKindComponent;
                if (this.feasibilityForm.AnyInKindComponent === true) {
                  this.isDisabledAnyInKindComponent = false;
                  // this.feasibilityForm.UseableByOrganisation = false;
                  // this.feasibilityForm.FeasibleExpertDeploy = false;
                }
                this.feasibilityForm.UseableByOrganisation =
                  data.data.CriteriaEveluationModel.UseableByOrganisation;
                this.feasibilityForm.FeasibleExpertDeploy =
                  data.data.CriteriaEveluationModel.FeasibleExpertDeploy;
                if (this.feasibilityForm.FeasibleExpertDeploy === true) {
                  this.isFeasibilityExpert = true;
                } else {
                  this.isFeasibilityExpert = false;
                }

                this.feasibilityForm.FeasibilityExpert =
                  data.data.CriteriaEveluationModel.FeasibilityExpert;

                this.feasibilityForm.EnoughTimeForProject =
                  data.data.CriteriaEveluationModel.EnoughTimeForProject;
                this.feasibilityForm.ProjectAllowedBylaw = data.data
                  .CriteriaEveluationModel.ProjectAllowedBylaw
                  ? data.data.CriteriaEveluationModel.ProjectAllowedBylaw
                  : false;
                if (
                  this.feasibilityForm.ProjectAllowedBylaw === false ||
                  data.data.CriteriaEveluationModel.ProjectByLeadership ===
                    false
                ) {
                  this.disableCriteriaEvaluationForm = true;
                  this.isExpanded = false;
                } else {
                  this.disableCriteriaEvaluationForm = false;
                  this.isExpanded = true;
                }
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
                this.priorityForm.Others =
                  data.data.CriteriaEveluationModel.Others;
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

                if (
                  data.data.CriteriaEveluationModel.ProjectActivities == null ||
                  data.data.CriteriaEveluationModel.ProjectActivities ===
                    undefined
                ) {
                  this.financialForm.ProjectActivities = 0;
                } else {
                  this.financialForm.ProjectActivities =
                    data.data.CriteriaEveluationModel.ProjectActivities;
                }
                //  if (this.financialForm.ProjectActivities == null ? 0 : data.data.CriteriaEveluationModel.ProjectActivities)
                if (
                  data.data.CriteriaEveluationModel.Operational == null ||
                  data.data.CriteriaEveluationModel.Operational === undefined
                ) {
                  this.financialForm.Operational = 0;
                } else {
                  this.financialForm.Operational =
                    data.data.CriteriaEveluationModel.Operational;
                }
                if (
                  data.data.CriteriaEveluationModel.Overhead_Admin === 0 ||
                  data.data.CriteriaEveluationModel.Overhead_Admin === undefined
                ) {
                  this.financialForm.Overhead_Admin = 0;
                } else {
                  this.financialForm.Overhead_Admin =
                    data.data.CriteriaEveluationModel.Overhead_Admin;
                }
                if (
                  data.data.CriteriaEveluationModel.Lump_Sum === 0 ||
                  data.data.CriteriaEveluationModel.Lump_Sum === undefined
                ) {
                  this.financialForm.Lump_Sum = 0;
                } else {
                  this.financialForm.Lump_Sum =
                    data.data.CriteriaEveluationModel.Lump_Sum;
                }
                // if (this.financialForm.Operational === null ? 0 : data.data.CriteriaEveluationModel.Operational)
                // if (this.financialForm.Overhead_Admin == null ? 0 : data.data.CriteriaEveluationModel.Overhead_Admin)
                // if (this.financialForm.Lump_Sum == null ? 0 : data.data.CriteriaEveluationModel.Lump_Sum)
                this.financialForm.Total =
                  data.data.CriteriaEveluationModel.Total;

                // add/edit riskSecurity
                this.riskForm.Security =
                  data.data.CriteriaEveluationModel.Security;
                if (this.riskForm.Security === true) {
                  this.isDisabledRisk = false;
                } else {
                  this.isDisabledRisk = true;
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
                if (this.riskForm.DeliveryFaiLure === true) {
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
                if (this.riskForm.Reputation === true) {
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
                this.riskForm.Geographical =
                  data.data.CriteriaEveluationModel.Geographical;
                this.riskForm.Insecurity =
                  data.data.CriteriaEveluationModel.Insecurity;
                this.riskForm.Season = data.data.CriteriaEveluationModel.Season;
                this.riskForm.Ethnicity =
                  data.data.CriteriaEveluationModel.Ethnicity;
                this.riskForm.Culture =
                  data.data.CriteriaEveluationModel.Culture;
                this.currencyDetailModel.CurrencyId =
                  data.data.CriteriaEveluationModel.CurrencyId;
                this.riskForm.ReligiousBeliefs =
                  data.data.CriteriaEveluationModel.ReligiousBeliefs;
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
                this.IsSubmitCEform.IsCriteriaEvaluationSubmit =
                  data.data.CriteriaEveluationModel.IsCriteriaEvaluationSubmit;
              }
            }
            this.criteriaEvaluationLoader = false;
            this.getPurposeOfInitialtiveScore();
          },
          error => {
            this.criteriaEvaluationLoader = false;
            this.toastr.error('Something Went Wrong..! Please Try Again.');
          }
        );
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
        Awareness: model.Awareness,
        Infrastructure: model.Infrastructure,
        CapacityBuilding: model.CapacityBuilding,
        IncomeGeneration: model.IncomeGeneration,
        Mobilization: model.Mobilization,
        PeaceBuilding: model.PeaceBuilding,
        SocialProtection: model.SocialProtection,
        SustainableLivelihood: model.SustainableLivelihood,
        Advocacy: model.Advocacy,
        Literacy: model.Literacy,
        EducationCapacityBuilding: model.EducationCapacityBuilding,
        SchoolUpgrading: model.SchoolUpgrading,
        EducationInEmergency: model.EducationInEmergency,
        OnlineEducation: model.OnlineEducation,
        CommunityBasedEducation: model.CommunityBasedEducation,
        AcceleratedLearningProgram: model.AcceleratedLearningProgram,
        PrimaryHealthServices: model.PrimaryHealthServices,
        ReproductiveHealth: model.ReproductiveHealth,
        Immunization: model.Immunization,
        InfantandYoungChildFeeding: model.InfantandYoungChildFeeding,
        Nutrition: model.Nutrition,
        CommunicableDisease: model.CommunicableDisease,
        Hygiene: model.Hygiene,
        EnvironmentalHealth: model.EnvironmentalHealth,
        MentalHealthandDisabilityService:
          model.MentalHealthandDisabilityService,
        HealthCapacityBuilding: model.HealthCapacityBuilding,
        Telemedicine: model.Telemedicine,
        MitigationProjects: model.MitigationProjects,
        WaterSupply: model.WaterSupply,
        Sanitation: model.Sanitation,
        DisasterRiskHygiene: model.DisasterRiskHygiene,
        DisasterCapacityBuilding: model.DisasterCapacityBuilding,
        EmergencyResponse: model.EmergencyResponse,
        RenewableEnergy: model.RenewableEnergy,
        Shelter: model.Shelter,
        NaturalResourceManagement: model.NaturalResourceManagement,
        AggriculutreCapacityBuilding: model.AggriculutreCapacityBuilding,
        LivestockManagement: model.LivestockManagement,
        FoodSecurity: model.FoodSecurity,
        ResearchandPublication: model.ResearchandPublication,
        Horticulture: model.Horticulture,
        Irrigation: model.Irrigation,
        Livelihood: model.Livelihood,
        ValueChain: model.ValueChain,
        Children: model.Children,
        Disabled: model.Disabled,
        IDPs: model.IDPs,
        Returnees: model.Returnees,
        Kuchis: model.Kuchis,
        Widows: model.Widows,
        Women: model.Women,
        Men: model.Men,
        Youth: model.Youth
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
            this.eligibilityForm.DonorCriteriaMet =
              response.data.eligibilityCriteriaDetail.DonorCriteriaMet;
            if (this.eligibilityForm.DonorCriteriaMet == false) {
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
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
            }
          },
          error => {
            this.toastr.error('Something went wrong! Please try Again');
          }
        );
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
        Lump_Sum: model.Lump_Sum
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
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
            }
          },
          error => {
            this.toastr.error('Something went wrong! Please try Again');
          }
        );
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
        Geographical: model.Geographical,
        Insecurity: model.Insecurity,
        Season: model.Season,
        Ethnicity: model.Ethnicity,
        Culture: model.Culture,
        ReligiousBeliefs: model.ReligiousBeliefs,
        FocusDivertingrisk: model.FocusDivertingrisk,
        Financiallosses: model.Financiallosses,
        Opportunityloss: model.Opportunityloss,
        ProjectSelectionId: model.ProjectSelectionId,
        CurrencyId: model.CurrencyId,
        Probablydelaysinfunding: model.Probablydelaysinfunding,
        OtherOrganizationalHarms: model.OtherOrganizationalHarms,
        OrganizationalDescription: model.OrganizationalDescription
      };
      this.criteriaEvalService
        .AddEditRiskSecurityCriteriaEForm(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditRiskCriteria,
          obj
        )
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
            }
          },
          error => {
            this.toastr.error('Something went wrong! Please try Again');
          }
        );
    }
  }
  //#endregion

  //#region "currencyDetailsChange"
  currencyDetailsChange(value) {
    this.currencyDetailModel.CurrencyId = value;
    this.currencyDetailModel.ProjectId = this.ProjectId;
    this.AddEditProjectCurrency(this.currencyDetailModel);
  }

  //#endregion

  //#region  add/edit other project
  AddEditProjectCurrency(model: any) {
    const currencyModel: CurrencyDetailModel = {
      ProjectId: model.ProjectId,
      CurrencyId: model.CurrencyId
    };

    this.projectListService
      .AddEditProjectCurrencyDetail(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditProjectCurrencyDetail,
        currencyModel
      )
      .pipe()
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            if (response.data.ProjectProposalDetail != null) {
            }
          }
        },
        error => {
          this.toastr.error('Something went wrong! Please try Again');
        }
      );
  }
  //#endregion

  //#region to calculate total value
  get totalValue() {
    this.totalScore =
      // (this.donorCEForm.PastFundingExperience === true
      //   ? criteriaEvaluationScores.pastFundingExperience_Yes
      //   : criteriaEvaluationScores.pastFundingExperience_No) +
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
      // (this.donorCEForm.PastWorkingExperience === true
      //   ? criteriaEvaluationScores.pastWorkExperoence_Yes
      //   : criteriaEvaluationScores.pastWorkExperoence_No) +
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
        : criteriaEvaluationScores.finanacingHistory_Neutral) +
      (this.donorCEForm.ReligiousStanding === 1
        ? criteriaEvaluationScores.religiousStanding_Good
        : // : this.donorCEForm.ReligiousStanding === 2
          // ? criteriaEvaluationScores.religiousStanding_Neutral
          // : this.donorCEForm.ReligiousStanding === 3
          // ? criteriaEvaluationScores.religiousStanding_Bad
          criteriaEvaluationScores.religiousStanding_Neutral) +
      (this.donorCEForm.PoliticalStanding === 1
        ? criteriaEvaluationScores.politicalStanding_Good
        : // : this.donorCEForm.PoliticalStanding === 2
          // ? criteriaEvaluationScores.politicalStanding_Neutral
          // : this.donorCEForm.PoliticalStanding === 3
          // ? criteriaEvaluationScores.politicalStanding_Bad
          criteriaEvaluationScores.politicalStanding_Neutral) +
      (this.productAndServiceForm.RuralDevelopmentSocialProtection === true
        ? criteriaEvaluationScores.ruralDevelopmentProtection_Yes
        : criteriaEvaluationScores.ruralDevelopmentProtection_No) +
      (this.productAndServiceForm.Education === true
        ? criteriaEvaluationScores.education_Yes
        : criteriaEvaluationScores.education_No) +
      (this.productAndServiceForm.HealthAndNutrition === true
        ? criteriaEvaluationScores.healthAndNutrition_Yes
        : criteriaEvaluationScores.healthAndNutrition_No) +
      (this.productAndServiceForm.DisasterRiskReduction === true
        ? criteriaEvaluationScores.disasterAndRisk_Yes
        : criteriaEvaluationScores.disasterAndRisk_No) +
      (this.productAndServiceForm.AgricultureAndLivestock === true
        ? criteriaEvaluationScores.agricultureaAndLivestock_Yes
        : criteriaEvaluationScores.agricultureaAndLivestock_No) +
      (this.productAndServiceForm.Children === true
        ? criteriaEvaluationScores.Children_Yes
        : criteriaEvaluationScores.Children_No) +
      (this.productAndServiceForm.Disabled === true
        ? criteriaEvaluationScores.Disabled_Yes
        : criteriaEvaluationScores.Disabled_No) +
      (this.productAndServiceForm.IDPs === true
        ? criteriaEvaluationScores.IDPs_Yes
        : criteriaEvaluationScores.IDPs_No) +
      (this.productAndServiceForm.Returnees === true
        ? criteriaEvaluationScores.Returnees_Yes
        : criteriaEvaluationScores.Returnees_No) +
      (this.productAndServiceForm.Kuchis === true
        ? criteriaEvaluationScores.Kuchis_Yes
        : criteriaEvaluationScores.Kuchis_No) +
      (this.productAndServiceForm.Widows === true
        ? criteriaEvaluationScores.Widows_Yes
        : criteriaEvaluationScores.Widows_No) +
      (this.productAndServiceForm.Women === true
        ? criteriaEvaluationScores.Women_Yes
        : criteriaEvaluationScores.Women_No) +
      (this.productAndServiceForm.Men === true
        ? criteriaEvaluationScores.Men_Yes
        : criteriaEvaluationScores.Men_No) +
      (this.productAndServiceForm.Youth === true
        ? criteriaEvaluationScores.Youth_Yes
        : criteriaEvaluationScores.Youth_No) +
      (this.eligibilityForm.DonorCriteriaMet === true
        ? criteriaEvaluationScores.onDonorELegibilityCrteria_Yes
        : criteriaEvaluationScores.onDonorELegibilityCrteria_No) +
      (this.eligibilityForm.EligibilityDealine === true
        ? criteriaEvaluationScores.donorEligibilityDeadline_Yes
        : criteriaEvaluationScores.donorEligibilityDeadline_No) +
      (this.eligibilityForm.CoPartnership === true
        ? criteriaEvaluationScores.donorELigibilityPartnership_Yes
        : criteriaEvaluationScores.donorELigibilityPartnership_No) +
      // (this.feasibilityForm.CapacityAvailableForProject === true
      //   ? criteriaEvaluationScores.feasibilityCapacityForProject_Yes
      //   : criteriaEvaluationScores.feasibilityCapacityForProject_No) +
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
      // this.feasibilityForm.CostOfCompensationMonth * -1 +
      // (this.feasibilityForm.CostOfCompensationMoney >= 5000
      //   ? (-1 * this.feasibilityForm.CostOfCompensationMoney) / 5000
      //   : 0) +
      // (this.feasibilityForm.AnyInKindComponent === true
      //   ? criteriaEvaluationScores.anyInKindComponent_Yes
      //   : criteriaEvaluationScores.anyInKindComponent_No) +
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
      // Note: **don't delete:condition for if the costGreaterThanBudget_Yes value is set to be 0
      (this.feasibilityForm.IsCostGreaterthenBudget === true
        ? criteriaEvaluationScores.costGreaterThanBudget_Yes
        : criteriaEvaluationScores.costGreaterThanBudget_No) +
      // Note: **don't delete :condition for if the costGreaterThanBudget_Yes value is set to be -1
      // (this.feasibilityForm.IsCostGreaterthenBudget === false
      // ? criteriaEvaluationScores.costGreaterThanBudget_No :
      // this.feasibilityForm.IsCostGreaterthenBudget === null ? 0 : criteriaEvaluationScores.costGreaterThanBudget_Yes) +

      // Note : below comment by pk 22 july 2019 if costGreaterThanBudget_Yes = -1
      // (this.feasibilityForm.IsCostGreaterthenBudget === true
      //   ? criteriaEvaluationScores.costGreaterThanBudget_Yes
      //   : criteriaEvaluationScores.costGreaterThanBudget_No) +
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
      (this.riskForm.Staff === true
        ? criteriaEvaluationScores.staffSecured_Yes
        : criteriaEvaluationScores.staffSecured_No) +
      (this.riskForm.ProjectAssets === true
        ? criteriaEvaluationScores.staffSecured_Yes
        : criteriaEvaluationScores.staffSecured_No) +
      (this.riskForm.Suppliers === true
        ? criteriaEvaluationScores.projectAssetSecured_Yes
        : criteriaEvaluationScores.projectAssetSecured_No) +
      (this.riskForm.Beneficiaries === true
        ? criteriaEvaluationScores.beneficiarieSecured_Yes
        : criteriaEvaluationScores.beneficiarieSecured_No) +
      (this.riskForm.OverallOrganization === true
        ? criteriaEvaluationScores.organizationSecured_Yes
        : criteriaEvaluationScores.organizationSecured_No) +
      (this.riskForm.PrematureSeizure === true
        ? criteriaEvaluationScores.prematureSeizure_Yes
        : criteriaEvaluationScores.prematureSeizure_No) +
      (this.riskForm.GovernmentConfiscation === true
        ? criteriaEvaluationScores.governmentConfiscation_Yes
        : criteriaEvaluationScores.governmentConfiscation_No) +
      (this.riskForm.DesctructionByTerroristActivity === true
        ? criteriaEvaluationScores.terroristActivity_Yes
        : criteriaEvaluationScores.terroristActivity_No) +
      (this.riskForm.Religious === true
        ? criteriaEvaluationScores.religious_Yes
        : criteriaEvaluationScores.religious_No) +
      (this.riskForm.Sectarian === true
        ? criteriaEvaluationScores.sectarian_Yes
        : criteriaEvaluationScores.sectarian_No) +
      (this.riskForm.Ethinc === true
        ? criteriaEvaluationScores.ethnic_Yes
        : criteriaEvaluationScores.ethnic_No) +
      (this.riskForm.Social === true
        ? criteriaEvaluationScores.social_Yes
        : criteriaEvaluationScores.social_No) +
      (this.riskForm.Traditional === true
        ? criteriaEvaluationScores.traditional_Yes
        : criteriaEvaluationScores.traditional_No) +
      (this.riskForm.Geographical === true
        ? criteriaEvaluationScores.geographical_Yes
        : criteriaEvaluationScores.geographical_No) +
      (this.riskForm.Insecurity === true
        ? criteriaEvaluationScores.insecurity_Yes
        : criteriaEvaluationScores.insecurity_No) +
      (this.riskForm.Season === true
        ? criteriaEvaluationScores.season_Yes
        : criteriaEvaluationScores.season_No) +
      (this.riskForm.Ethnicity === true
        ? criteriaEvaluationScores.ethnicity_Yes
        : criteriaEvaluationScores.ethnicity_No) +
      (this.riskForm.Culture === true
        ? criteriaEvaluationScores.culture_Yes
        : criteriaEvaluationScores.culture_No) +
      (this.riskForm.ReligiousBeliefs === true
        ? criteriaEvaluationScores.religiousBeliefs_Yes
        : criteriaEvaluationScores.religiousBeliefs_No) +
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
  //#endregion

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
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() +
            GLOBAL.API_GetAllPriorityOtherDetailByProjectId,
          ProjectId
        )
        .subscribe(
          data => {
            if (data != null) {
              if (data.data.PriorityOtherDetail != null) {
                data.data.PriorityOtherDetail.forEach(element => {
                  this.priorityOtherList.push({
                    PriorityOtherDetailId: element.PriorityOtherDetailId,
                    Name: element.Name,
                    ProjectId: this.ProjectId
                  });
                });
              }
            }
          },
          error => {
            this.toastr.error('Something went wrong. Please try again.');
          }
        );
    }
  }
  //#endregion

  //#region "addPriorityOther"
  addPriorityOther(data: IPriorityOtherModel) {
    const obj: IPriorityOtherModel = {
      PriorityOtherDetailId: 0,
      Name: data.Name,
      ProjectId: this.ProjectId
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
            if (
              response.CommonId.LongId != null &&
              response.CommonId.LongId !== 0
            ) {
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
    const item = this.priorityOtherList.find(
      x => x.PriorityOtherDetailId === model.PriorityOtherDetailId
    );
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
    const item = this.priorityOtherList.find(
      x => x.PriorityOtherDetailId === model.PriorityOtherDetailId
    );
    const index = this.priorityOtherList.indexOf(item);
    this.priorityOtherList[index]._IsDeleted = true;
    this.priorityOtherList[index]._IsLoading = true;
    this.priorityOtherList[index]._IsError = false;

    this.criteriaEvalService
      .DeletePriorityDetailByPriorityId(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DeletePriorityOtherDetail,
        obj.PriorityOtherDetailId
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
      ProjectId: this.ProjectId
    };

    this.deletePriorityOther(obj);
  }
  //#endregion

  //#region getFeasibility

  getFeasibilityExpertByProjectId(ProjectId: number) {
    this.feasivilityList = [];
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() +
            GLOBAL.API_GetAllFeasibilityExpertDetailByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.FeasibilityExpertOtherDetail != null) {
              data.data.FeasibilityExpertOtherDetail.forEach(element => {
                this.feasivilityList.push({
                  ExpertOtherDetailId: element.ExpertOtherDetailId,
                  Name: element.Name,
                  ProjectId: element.ProjectId
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
      ProjectId: data.ProjectId
    };

    this.criteriaEvalService
      .AddEditFeasibilityExpert(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddFeasibilityExpertOtherDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            // add to list
            if (
              response.CommonId.LongId != null &&
              response.CommonId.LongId !== 0
            ) {
              obj.ExpertOtherDetailId = response.CommonId.LongId;
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
    const item = this.feasivilityList.find(
      x => x.ExpertOtherDetailId === model.ExpertOtherDetailId
    );
    const index = this.feasivilityList.indexOf(item);
    this.feasivilityList[index]._IsLoading = true;
    this.feasivilityList[index]._IsError = false;

    this.criteriaEvalService
      .AddEditFeasibilityExpert(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_EditFeasibilityExpertDetail,
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
      ProjectId: this.ProjectId
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
    const item = this.feasivilityList.find(
      x => x.ExpertOtherDetailId === model.ExpertOtherDetailId
    );
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
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() +
            GLOBAL.API_GetAllAssumptionDetailByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.CEAssumptionDetail != null) {
              data.data.CEAssumptionDetail.forEach(element => {
                this.assumptionList.push({
                  AssumptionDetailId: element.AssumptionDetailId,
                  Name: element.Name,
                  ProjectId: element.ProjectId
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
      ProjectId: this.ProjectId
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
            if (
              response.CommonId.LongId != null &&
              response.CommonId.LongId !== 0
            ) {
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
    const item = this.assumptionList.find(
      x => x.AssumptionDetailId === model.AssumptionDetailId
    );
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
      ProjectId: null
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
    const item = this.assumptionList.find(
      x => x.AssumptionDetailId === model.AssumptionDetailId
    );
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
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
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
                  ProjectId: element.ProjectId
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
      ProjectId: this.ProjectId
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
            if (
              response.CommonId.LongId != null &&
              response.CommonId.LongId !== 0
            ) {
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
    const item = this.ageGroupList.find(
      x => x.AgeGroupOtherDetailId === model.AgeGroupOtherDetailId
    );
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
      ProjectId: null
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
    const item = this.ageGroupList.find(
      x => x.AgeGroupOtherDetailId === model.AgeGroupOtherDetailId
    );
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
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() +
            GLOBAL.API_GetAllOccupationDetailByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.CEOccupationDetail != null) {
              data.data.CEOccupationDetail.forEach(element => {
                this.occupatonList.push({
                  OccupationOtherDetailId: element.OccupationOtherDetailId,
                  Name: element.Name,
                  ProjectId: element.ProjectId
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
      ProjectId: this.ProjectId
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
            if (
              response.CommonId.LongId != null &&
              response.CommonId.LongId !== 0
            ) {
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
    const item = this.occupatonList.find(
      x => x.OccupationOtherDetailId === model.OccupationOtherDetailId
    );
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
      ProjectId: null
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
    const item = this.occupatonList.find(
      x => x.OccupationOtherDetailId === model.OccupationOtherDetailId
    );
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
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.criteriaEvalService
        .GetPriorityOtherDetailByProjectId(
          this.appurl.getApiUrl() +
            GLOBAL.API_GetAlldonorEligibilityByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.DonorEligibilityCriteria != null) {
              data.data.DonorEligibilityCriteria.forEach(element => {
                this.donorEligibilityList.push({
                  DonorEligibilityDetailId: element.DonorEligibilityDetailId,
                  Name: element.Name,
                  ProjectId: element.ProjectId
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
      ProjectId: this.ProjectId
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
            if (
              response.CommonId.LongId != null &&
              response.CommonId.LongId !== 0
            ) {
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
    const item = this.donorEligibilityList.find(
      x => x.DonorEligibilityDetailId === model.DonorEligibilityDetailId
    );
    const index = this.donorEligibilityList.indexOf(item);
    this.donorEligibilityList[index]._IsLoading = true;
    this.donorEligibilityList[index]._IsError = false;

    this.criteriaEvalService
      .AddEditDonorEligibilityOther(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_EditCEdonorEligibilityDetail,
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
      ProjectId: null
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
    const item = this.donorEligibilityList.find(
      x => x.DonorEligibilityDetailId === model.DonorEligibilityDetailId
    );
    const index = this.donorEligibilityList.indexOf(item);
    this.donorEligibilityList[index]._IsDeleted = true;
    this.donorEligibilityList[index]._IsLoading = true;
    this.donorEligibilityList[index]._IsError = false;

    this.criteriaEvalService
      .DeletePriorityDetailByPriorityId(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_DeleteDonorEligibilityDetail,
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
    if (this.ProjectId != null) {
      if (ev === 'IsCESubmit') {
        this.startCriteriaEvaluationSubmitLoader = true;
        this.IsSubmitCEform.IsCriteriaEvaluationSubmit = true;
      } else if (ev === 'IsCEReject') {
        this.startCriteriaEvaluationSubmitLoader = true;
        this.IsSubmitCEform.IsCriteriaEvaluationSubmit = false;
      }

      this.setHeaderMenu();
      this.IsSubmitCEform.ProjectId = this.ProjectId;
      this.AddIsSubmitCEDetail(this.IsSubmitCEform);
    }
  }
  //#endregion

  //#region "setHeaderMenu"
  setHeaderMenu() {
    this.isSuperadmin = this.localStorageService.GetSuperAdminDetail();
    if (this.isSuperadmin) {
      this.globalService.setMenuList(this.menuList);
      // check weather the criteria evaluation is approved and project is win
    } else if (this.IsSubmitCEform.IsCriteriaEvaluationSubmit === true) {
      // check weather the criteria evaluation is approved
      this.authorizedMenuList = this.localStorageService.GetAuthorizedPages(
        this.IsSubmitCEform.IsCriteriaEvaluationSubmit
          ? this.menuList.filter((i, index) => index < 3)
          : this.menuList.filter((i, index) => index < 2)
      );

      // Set Menu Header List
      this.authorizedMenuList.push(this.menuList.find(r => r.Id === 7));
      this.globalService.setMenuList(this.authorizedMenuList);
    }
    //#endregion
  }
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
            this.IsSubmitCEform.IsCriteriaEvaluationSubmit =
              response.CommonId.IsApproved;
            this.isCriteriaEvaluationFormSubmit.emit(
              this.IsSubmitCEform.IsCriteriaEvaluationSubmit
            );
          }
          // this.commonLoader.hideLoader();
        },
        error => {
          // this.commonLoader.hideLoader();
        }
      );
  }
  //#endregion

  downloadPdf() {
    const pdfmodel: ICriteriaEvaluationPdfModel = {
      ProjectId: this.ProjectId,
      TotalScore: this.totalScore
    };
    if (pdfmodel != null && pdfmodel !== undefined) {
      this.globalService
        .getFile(
          this.appurl.getApiUrl() +
            GLOBAL.API_Pdf_GetCriteriaEvaluationReportPdf,
          pdfmodel
        )
        .pipe()
        .subscribe();
    }
  }

  onTabchangeScore($event) {
    if ($event.any) {
      this.getPurposeOfInitialtiveScore();
    }
  }

  // common to get purpose of initiaitve scores

  getPurposeOfInitialtiveScore() {
    if (
      this.productAndServiceForm.Awareness === true ||
      this.productAndServiceForm.Infrastructure === true ||
      this.productAndServiceForm.CapacityBuilding === true ||
      this.productAndServiceForm.IncomeGeneration === true ||
      this.productAndServiceForm.Mobilization === true ||
      this.productAndServiceForm.PeaceBuilding === true ||
      this.productAndServiceForm.SocialProtection === true ||
      this.productAndServiceForm.SustainableLivelihood === true ||
      this.productAndServiceForm.Advocacy === true
    ) {
      this.productAndServiceForm.RuralDevelopmentSocialProtection = true;
    } else {
      this.productAndServiceForm.RuralDevelopmentSocialProtection = false;
    }
    if (
      this.productAndServiceForm.Literacy === true ||
      this.productAndServiceForm.EducationCapacityBuilding === true ||
      this.productAndServiceForm.SchoolUpgrading === true ||
      this.productAndServiceForm.EducationInEmergency === true ||
      this.productAndServiceForm.OnlineEducation === true ||
      this.productAndServiceForm.CommunityBasedEducation === true ||
      this.productAndServiceForm.AcceleratedLearningProgram === true
    ) {
      this.productAndServiceForm.Education = true;
    } else {
      this.productAndServiceForm.Education = false;
    }
    if (
      this.productAndServiceForm.PrimaryHealthServices === true ||
      this.productAndServiceForm.ReproductiveHealth === true ||
      this.productAndServiceForm.Immunization === true ||
      this.productAndServiceForm.Nutrition === true ||
      this.productAndServiceForm.CommunicableDisease === true ||
      this.productAndServiceForm.Hygiene === true ||
      this.productAndServiceForm.EnvironmentalHealth === true ||
      this.productAndServiceForm.HealthCapacityBuilding === true ||
      this.productAndServiceForm.Telemedicine === true ||
      this.productAndServiceForm.InfantandYoungChildFeeding === true ||
      this.productAndServiceForm.MentalHealthandDisabilityService === true
    ) {
      this.productAndServiceForm.HealthAndNutrition = true;
    } else {
      this.productAndServiceForm.HealthAndNutrition = false;
    }
    if (
      this.productAndServiceForm.MitigationProjects === true ||
      this.productAndServiceForm.WaterSupply === true ||
      this.productAndServiceForm.Sanitation === true ||
      this.productAndServiceForm.DisasterRiskHygiene === true ||
      this.productAndServiceForm.DisasterCapacityBuilding === true ||
      this.productAndServiceForm.EmergencyResponse === true ||
      this.productAndServiceForm.RenewableEnergy === true ||
      this.productAndServiceForm.Shelter === true
    ) {
      this.productAndServiceForm.DisasterRiskReduction = true;
    } else {
      this.productAndServiceForm.DisasterRiskReduction = false;
    }
    if (
      this.productAndServiceForm.NaturalResourceManagement === true ||
      this.productAndServiceForm.AggriculutreCapacityBuilding === true ||
      this.productAndServiceForm.LivestockManagement === true ||
      this.productAndServiceForm.FoodSecurity === true ||
      this.productAndServiceForm.ResearchandPublication === true ||
      this.productAndServiceForm.Horticulture === true ||
      this.productAndServiceForm.Irrigation === true ||
      this.productAndServiceForm.Livelihood === true ||
      this.productAndServiceForm.ValueChain === true
    ) {
      this.productAndServiceForm.AgricultureAndLivestock = true;
    } else {
      this.productAndServiceForm.AgricultureAndLivestock = false;
    }
  }
  ruralDevelopmentScore(value) {
    // Note :  check if all the rural development value is unchecked then only the score value will decreased.
    if (value.checked === true) {
      this.productAndServiceForm.RuralDevelopmentSocialProtection = true;
      this.developmenmtValue = this.developmenmtValue + 1;
    } else {
      this.developmenmtValue = this.developmenmtValue - 1;
      if (this.developmenmtValue <= 0) {
        this.productAndServiceForm.RuralDevelopmentSocialProtection = false;
      }
    }
  }
  educationScore(value) {
    if (value.checked === true) {
      this.productAndServiceForm.Education = true;
      this.educationValue = this.educationValue + 1;
    } else {
      this.educationValue = this.educationValue - 1;
      if (this.educationValue <= 0) {
        this.productAndServiceForm.Education = false;
      }
    }
  }
  healthNutritionScore(value) {
    if (value.checked === true) {
      this.healthNutritionValue = this.healthNutritionValue + 1;
      this.productAndServiceForm.HealthAndNutrition = true;
    } else {
      this.healthNutritionValue = this.healthNutritionValue - 1;
      if (this.healthNutritionValue <= 0) {
        this.productAndServiceForm.HealthAndNutrition = false;
      }
    }
  }
  aggricultureScore(value) {
    if (value.checked === true) {
      this.productAndServiceForm.AgricultureAndLivestock = true;
      this.aggricultureLiveStockValue = this.aggricultureLiveStockValue + 1;
    } else {
      this.aggricultureLiveStockValue = this.aggricultureLiveStockValue - 1;
      if (this.aggricultureLiveStockValue <= 0) {
        this.productAndServiceForm.AgricultureAndLivestock = false;
      }
    }
  }

  disasterRiskScore(value) {
    if (value.checked === true) {
      this.productAndServiceForm.DisasterRiskReduction = true;
      this.disasterRiskValue = this.disasterRiskValue + 1;
    } else {
      this.disasterRiskValue = this.disasterRiskValue - 1;
      if (this.disasterRiskValue <= 0) {
        this.productAndServiceForm.DisasterRiskReduction = false;
      }
    }
  }
  ngOnDestroy() {
    this.cdRef.detach();
  }
}
