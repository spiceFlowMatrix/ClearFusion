import {
  Component,
  OnInit,
  Input,
  SimpleChanges,
  OnChanges
} from '@angular/core';
import { HrService } from '../../hr.service';
import { CodeService } from '../../../code/code.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService } from '../../../../service/common.service';
import {
  IEmpExitInterviewFormModel,
  IEmployeeListModel
} from '../interview-form.models';

@Component({
  selector: 'app-employee-exit-interview-form',
  templateUrl: './employee-exit-interview-form.component.html',
  styleUrls: ['./employee-exit-interview-form.component.css']
})
export class EmployeeExitInterviewFormComponent implements OnInit, OnChanges {
  //#region "Variables"

  // Data Sources
  employeeListDataSource: IEmployeeListModel[];
  exitInterviewDataSource: any[];
  genderTypesDropdown: any[];

  deleteConfVisiblePopup = false;
  selectedEmployeeForDelete: number;
  isExitInterviewViewOnly: boolean;

  employeeSelectedValue: number;
  employeeSelectedValueFlag = false;
  @Input() officeId: any;
  @Input() isEditingAllowed;
  @Input() selectedEmployee: number;

  // Radio Group
  viewsRadioGroup: any;
  yesNoRadioGroup: any;
  empAspectsFeedbackRadioGroup: any;

  // Form
  empExitInterviewFormMainForm: IEmpExitInterviewFormModel;

  // Flag
  empExitInterviewFormListFlag = true;
  addEmpExitInterviewDetailsFlag = false;

  // Loader
  empInterviewExitFormLoader = false;

  fileName: any;

  //#endregion

  constructor(
    private hrService: HrService,
    private codeService: CodeService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) {}
  ngOnInit() {
    this.initializeForm();
    // this.exportAnualReportPdf();
  }

  initializeForm() {
    this.yesNoRadioGroup = ['Yes', 'No'];

    this.empAspectsFeedbackRadioGroup = [
      'Very Satisfied',
      'Satisfied',
      'Dissatisfied'
    ];

    this.viewsRadioGroup = [
      'Strongly Disagree',
      'Disagree',
      'Neutral',
      'Agree',
      'Strongly Agree'
    ];

    this.genderTypesDropdown = [
      { GenderTypeId: 1, GenderTypeName: 'Male' },
      { GenderTypeId: 2, GenderTypeName: 'Female' },
      { GenderTypeId: 3, GenderTypeName: 'Other' }
    ];

    this.initExitForm();
  }

  initExitForm() {
    this.empExitInterviewFormMainForm = {
      ExistInterviewDetailsId: null,
      EmployeeId: null,

      EmployeeCode: null,
      EmployeeName: null,
      Position: null,
      Department: null,
      TenureWithCHA: null,
      Gender: null,

      // FeelingAboutEmployee
      DutiesOfJob: null,
      TrainingAndDevelopmentPrograms: null,
      OpportunityAdvancement: null,
      SalaryTreatment: null,
      BenefitProgram: null,
      WorkingConditions: null,
      WorkingHours: null,
      CoWorkers: null,
      Supervisors: null,
      GenderFriendlyEnvironment: null,
      OverallJobSatisfaction: null,

      // ReasonOfLeaving
      Benefits: false,
      BetterJobOpportunity: false,
      FamilyReasons: false,
      NotChallenged: false,
      Pay: false,
      PersonalReasons: false,
      Relocation: false,
      ReturnToSchool: false,
      ConflictWithSuoervisors: false,
      ConflictWithOther: false,
      WorkRelationship: false,
      CompanyInstability: false,
      CareerChange: false,
      HealthIssue: false,

      // TheDepartment
      HadGoodSynergy: null,
      HadAdequateEquipment: null,
      WasAdequatelyStaffed: null,
      WasEfficient: null,

      // TheJobItself
      JobWasChallenging: null,
      SkillsEffectivelyUsed: null,
      JobOrientation: null,
      WorkLoadReasonable: null,
      SufficientResources: null,
      WorkEnvironment: null,
      ComfortableAppropriately: null,
      Equipped: null,

      // MySupervisor
      HadKnowledgeOfJob: null,
      HadKnowledgeSupervision: null,
      WasOpenSuggestions: null,
      RecognizedEmployeesContribution: null,

      // TheManagement
      GaveFairTreatment: null,
      WasAvailableToDiscuss: null,
      WelcomedSuggestions: null,
      MaintainedConsistent: null,
      ProvidedRecognition: null,
      EncouragedCooperation: null,
      ProvidedDevelopment: null,

      Question: 'No',
      Explain: null,
      OfficeId: null
    };
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes !== undefined && changes.officeId !== undefined) {
      this.officeId = changes.officeId.currentValue;

      this.getAllEmployeeListByOfficeId();
      this.GetAllExitInterviewByOfficeId();
    }
  }

  //#region "GetAllExitInterview"
  GetAllExitInterviewByOfficeId() {
    this.empInterviewExitFormLoader = true;

    this.hrService
      .GetAllDetailsByOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllExitInterview,
        this.officeId
      )
      .subscribe(
        data => {
          this.exitInterviewDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.ExitInterviewList != null &&
            data.data.ExitInterviewList.length > 0
          ) {
            data.data.ExitInterviewList.forEach(element => {
              this.exitInterviewDataSource.push(element);
            });
          } else {
            // tslint:disable-next-line:curly
            if (data.data.ExitInterviewList == null)
              this.toastr.warning('No record found!');
            // tslint:disable-next-line:curly
            else if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');
          }
          this.empInterviewExitFormLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.empInterviewExitFormLoader = false;
        }
      );
  }
  //#endregion

  //#region "Get All Employee List By OfficeId"
  getAllEmployeeListByOfficeId() {
    // tslint:disable-next-line:radix
    const officeId = this.officeId;
    this.hrService
      .GetAllDetailsByOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetEmployeeDetailByOfficeId,
        officeId
      )
      .subscribe(
        data => {
          this.employeeListDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeDetailListData != null &&
            data.data.EmployeeDetailListData.length > 0
          ) {
            data.data.EmployeeDetailListData.forEach(element => {
              this.employeeListDataSource.push(element);
            });
            this.commonService.sortDropdown(
              this.employeeListDataSource,
              'CodeEmployeeName'
            );
          } else {
            // tslint:disable-next-line:curly
            if (data.data.EmployeeDetailListData == null)
              this.toastr.warning('No record found!');
            // tslint:disable-next-line:curly
            else if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region "getEmployeeMoreDetailByEmpId"
  getEmployeeMoreDetailByEmpId(empId: number) {
    this.hrService
      .GetEmployeeDataByEmployeeId(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_GetEmployeeDetailByEmployeeId,
        empId
      )
      .subscribe(
        data => {
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeDetailListData != null &&
            data.data.EmployeeDetailListData.length > 0
          ) {
            // var empData;
            data.data.EmployeeDetailListData.forEach(element => {
              // empData = element;

              this.empExitInterviewFormMainForm.EmployeeId = element.EmployeeId;
              this.empExitInterviewFormMainForm.EmployeeCode =
                element.EmployeeCode;
              this.empExitInterviewFormMainForm.EmployeeName =
                element.EmployeeName;
              this.empExitInterviewFormMainForm.Position = element.Position;
              this.empExitInterviewFormMainForm.Department = element.Department;
              this.empExitInterviewFormMainForm.TenureWithCHA =
                element.TenureWithCHA;
              this.empExitInterviewFormMainForm.Gender =
                element.Gender.toLowerCase() === 'male'
                  ? 1
                  : element.Gender.toLowerCase() === 'female'
                  ? 2
                  : 3;
            });
          } else {
            // tslint:disable-next-line:curly
            if (data.data.EmployeeDetailListData == null) {
              // this.toastr.warning('No record found!');
            } else if (data.StatusCode === 400) {
              this.toastr.error('Something went wrong!');
            }
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region "on Add Exit Interview Form Submit"
  onAddExitInterviewFormSubmit(model: IEmpExitInterviewFormModel) {
    const exitInterviewFormModel: IEmpExitInterviewFormModel = {
      ExistInterviewDetailsId: 0,
      EmployeeId: this.employeeSelectedValue,

      EmployeeCode: model.EmployeeCode,
      EmployeeName: model.EmployeeName,
      Position: model.Position,
      Department: model.Department,
      TenureWithCHA: model.TenureWithCHA,
      Gender:
        model.Gender === 1 ? 'Male' : model.Gender === 2 ? 'Female' : 'Other',

      // FeelingAboutEmployee
      DutiesOfJob: model.DutiesOfJob,
      TrainingAndDevelopmentPrograms: model.TrainingAndDevelopmentPrograms,
      OpportunityAdvancement: model.OpportunityAdvancement,
      SalaryTreatment: model.SalaryTreatment,
      BenefitProgram: model.BenefitProgram,
      WorkingConditions: model.WorkingConditions,
      WorkingHours: model.WorkingHours,
      CoWorkers: model.CoWorkers,
      Supervisors: model.Supervisors,
      GenderFriendlyEnvironment: model.GenderFriendlyEnvironment,
      OverallJobSatisfaction: model.OverallJobSatisfaction,

      // ReasonOfLeaving
      Benefits: model.Benefits,
      BetterJobOpportunity: model.BetterJobOpportunity,
      FamilyReasons: model.FamilyReasons,
      NotChallenged: model.NotChallenged,
      Pay: model.Pay,
      PersonalReasons: model.PersonalReasons,
      Relocation: model.Relocation,
      ReturnToSchool: model.ReturnToSchool,
      ConflictWithSuoervisors: model.ConflictWithSuoervisors,
      ConflictWithOther: model.ConflictWithOther,
      WorkRelationship: model.WorkRelationship,
      CompanyInstability: model.CompanyInstability,
      CareerChange: model.CareerChange,
      HealthIssue: model.HealthIssue,

      // TheDepartment
      HadGoodSynergy: model.HadGoodSynergy,
      HadAdequateEquipment: model.HadAdequateEquipment,
      WasAdequatelyStaffed: model.WasAdequatelyStaffed,
      WasEfficient: model.WasEfficient,

      // TheJobItself
      JobWasChallenging: model.JobWasChallenging,
      SkillsEffectivelyUsed: model.SkillsEffectivelyUsed,
      JobOrientation: model.JobOrientation,
      WorkLoadReasonable: model.WorkLoadReasonable,
      SufficientResources: model.SufficientResources,
      WorkEnvironment: model.WorkEnvironment,
      ComfortableAppropriately: model.ComfortableAppropriately,
      Equipped: model.Equipped,

      // MySupervisor
      HadKnowledgeOfJob: model.HadKnowledgeOfJob,
      HadKnowledgeSupervision: model.HadKnowledgeSupervision,
      WasOpenSuggestions: model.WasOpenSuggestions,
      RecognizedEmployeesContribution: model.RecognizedEmployeesContribution,

      // TheManagement
      GaveFairTreatment: model.GaveFairTreatment,
      WasAvailableToDiscuss: model.WasAvailableToDiscuss,
      WelcomedSuggestions: model.WelcomedSuggestions,
      MaintainedConsistent: model.MaintainedConsistent,
      ProvidedRecognition: model.ProvidedRecognition,
      EncouragedCooperation: model.EncouragedCooperation,
      ProvidedDevelopment: model.ProvidedDevelopment,

      Question:
        model.Question.toLowerCase() === 'Yes'.toLowerCase() ? true : false,
      Explain: model.Explain,
      OfficeId: this.officeId
    };

    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddExitInterview,
        exitInterviewFormModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully');
          } else {
            // tslint:disable-next-line:curly
            if (data.StatusCode === 400)
              this.toastr.warning('Something went wrong!');
          }
          this.hideAddEmpExitInterviewForm();
          this.GetAllExitInterviewByOfficeId();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region "on Edit Exit Interview Form Submit"
  onEditExitInterviewFormSubmit(model: any) {
    const exitInterviewFormModel: IEmpExitInterviewFormModel = {
      ExistInterviewDetailsId: model.ExistInterviewDetailsId,
      EmployeeId: model.EmployeeId,

      EmployeeCode: model.EmployeeCode,
      EmployeeName: model.EmployeeName,
      Position: model.Position,
      Department: model.Department,
      TenureWithCHA: model.TenureWithCHA,
      Gender:
        model.Gender === 1 ? 'Male' : model.Gender === 2 ? 'Female' : 'Other',

      // FeelingAboutEmployee
      DutiesOfJob: model.DutiesOfJob,
      TrainingAndDevelopmentPrograms: model.TrainingAndDevelopmentPrograms,
      OpportunityAdvancement: model.OpportunityAdvancement,
      SalaryTreatment: model.SalaryTreatment,
      BenefitProgram: model.BenefitProgram,
      WorkingConditions: model.WorkingConditions,
      WorkingHours: model.WorkingHours,
      CoWorkers: model.CoWorkers,
      Supervisors: model.Supervisors,
      GenderFriendlyEnvironment: model.GenderFriendlyEnvironment,
      OverallJobSatisfaction: model.OverallJobSatisfaction,

      // ReasonOfLeaving
      Benefits: model.Benefits,
      BetterJobOpportunity: model.BetterJobOpportunity,
      FamilyReasons: model.FamilyReasons,
      NotChallenged: model.NotChallenged,
      Pay: model.Pay,
      PersonalReasons: model.PersonalReasons,
      Relocation: model.Relocation,
      ReturnToSchool: model.ReturnToSchool,
      ConflictWithSuoervisors: model.ConflictWithSuoervisors,
      ConflictWithOther: model.ConflictWithOther,
      WorkRelationship: model.WorkRelationship,
      CompanyInstability: model.CompanyInstability,
      CareerChange: model.CareerChange,
      HealthIssue: model.HealthIssue,

      // TheDepartment
      HadGoodSynergy: model.HadGoodSynergy,
      HadAdequateEquipment: model.HadAdequateEquipment,
      WasAdequatelyStaffed: model.WasAdequatelyStaffed,
      WasEfficient: model.WasEfficient,

      // TheJobItself
      JobWasChallenging: model.JobWasChallenging,
      SkillsEffectivelyUsed: model.SkillsEffectivelyUsed,
      JobOrientation: model.JobOrientation,
      WorkLoadReasonable: model.WorkLoadReasonable,
      SufficientResources: model.SufficientResources,
      WorkEnvironment: model.WorkEnvironment,
      ComfortableAppropriately: model.ComfortableAppropriately,
      Equipped: model.Equipped,

      // MySupervisor
      HadKnowledgeOfJob: model.HadKnowledgeOfJob,
      HadKnowledgeSupervision: model.HadKnowledgeSupervision,
      WasOpenSuggestions: model.WasOpenSuggestions,
      RecognizedEmployeesContribution: model.RecognizedEmployeesContribution,

      // TheManagement
      GaveFairTreatment: model.GaveFairTreatment,
      WasAvailableToDiscuss: model.WasAvailableToDiscuss,
      WelcomedSuggestions: model.WelcomedSuggestions,
      MaintainedConsistent: model.MaintainedConsistent,
      ProvidedRecognition: model.ProvidedRecognition,
      EncouragedCooperation: model.EncouragedCooperation,
      ProvidedDevelopment: model.ProvidedDevelopment,

      Question:
        model.Question.toLowerCase() === 'Yes'.toLowerCase() ? true : false,
      Explain: model.Explain,
      OfficeId: this.officeId
    };

    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditExitInterview,
        exitInterviewFormModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully');
          } else {
            // tslint:disable-next-line:curly
            if (data.StatusCode === 400)
              this.toastr.warning('Something went wrong!');
          }
          this.hideAddEmpExitInterviewForm();
          this.GetAllExitInterviewByOfficeId();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region "onExitInterviewFormSubmit"
  onExitInterviewFormSubmit(model: any) {
    if (model != null) {
      if (this.employeeSelectedValue != null) {
        model.ExistInterviewDetailsId === 0 ||
        model.ExistInterviewDetailsId == null
          ? this.onAddExitInterviewFormSubmit(model)
          : this.onEditExitInterviewFormSubmit(model);
      } else {
        this.toastr.warning('Please select Employee');
      }
    }
  }
  //#endregion

  //#region "onEditEmpExitInterviewShowForm"
  onEditEmpExitInterviewShowForm(model: any, isViewOnly: boolean) {
    const modelData = model.data;
    this.isExitInterviewViewOnly = isViewOnly;

    if (modelData != null) {
      this.empExitInterviewFormMainForm = {
        ExistInterviewDetailsId: modelData.ExistInterviewDetailsId,
        EmployeeId: modelData.EmployeeId,

        EmployeeCode: modelData.EmployeeCode,
        EmployeeName: modelData.EmployeeName,
        Position: modelData.Position,
        Department: modelData.Department,
        TenureWithCHA: modelData.TenureWithCHA,
        Gender:
          modelData.Gender.toLowerCase() === 'male'
            ? 1
            : modelData.Gender.toLowerCase() === 'female'
            ? 2
            : 3,

        // FeelingAboutEmployee
        DutiesOfJob: modelData.DutiesOfJob,
        TrainingAndDevelopmentPrograms:
          modelData.TrainingAndDevelopmentPrograms,
        OpportunityAdvancement: modelData.OpportunityAdvancement,
        SalaryTreatment: modelData.SalaryTreatment,
        BenefitProgram: modelData.BenefitProgram,
        WorkingConditions: modelData.WorkingConditions,
        WorkingHours: modelData.WorkingHours,
        CoWorkers: modelData.CoWorkers,
        Supervisors: modelData.Supervisors,
        GenderFriendlyEnvironment: modelData.GenderFriendlyEnvironment,
        OverallJobSatisfaction: modelData.OverallJobSatisfaction,

        // ReasonOfLeaving
        Benefits: modelData.Benefits,
        BetterJobOpportunity: modelData.BetterJobOpportunity,
        FamilyReasons: modelData.FamilyReasons,
        NotChallenged: modelData.NotChallenged,
        Pay: modelData.Pay,
        PersonalReasons: modelData.PersonalReasons,
        Relocation: modelData.Relocation,
        ReturnToSchool: modelData.ReturnToSchool,
        ConflictWithSuoervisors: modelData.ConflictWithSuoervisors,
        ConflictWithOther: modelData.ConflictWithOther,
        WorkRelationship: modelData.WorkRelationship,
        CompanyInstability: modelData.CompanyInstability,
        CareerChange: modelData.CareerChange,
        HealthIssue: modelData.HealthIssue,

        // TheDepartment
        HadGoodSynergy: modelData.HadGoodSynergy,
        HadAdequateEquipment: modelData.HadAdequateEquipment,
        WasAdequatelyStaffed: modelData.WasAdequatelyStaffed,
        WasEfficient: modelData.WasEfficient,

        // TheJobItself
        JobWasChallenging: modelData.JobWasChallenging,
        SkillsEffectivelyUsed: modelData.SkillsEffectivelyUsed,
        JobOrientation: modelData.JobOrientation,
        WorkLoadReasonable: modelData.WorkLoadReasonable,
        SufficientResources: modelData.SufficientResources,
        WorkEnvironment: modelData.WorkEnvironment,
        ComfortableAppropriately: modelData.ComfortableAppropriately,
        Equipped: modelData.Equipped,

        // MySupervisor
        HadKnowledgeOfJob: modelData.HadKnowledgeOfJob,
        HadKnowledgeSupervision: modelData.HadKnowledgeSupervision,
        WasOpenSuggestions: modelData.WasOpenSuggestions,
        RecognizedEmployeesContribution:
          modelData.RecognizedEmployeesContribution,

        // TheManagement
        GaveFairTreatment: modelData.GaveFairTreatment,
        WasAvailableToDiscuss: modelData.WasAvailableToDiscuss,
        WelcomedSuggestions: modelData.WelcomedSuggestions,
        MaintainedConsistent: modelData.MaintainedConsistent,
        ProvidedRecognition: modelData.ProvidedRecognition,
        EncouragedCooperation: modelData.EncouragedCooperation,
        ProvidedDevelopment: modelData.ProvidedDevelopment,

        Question: modelData.Question === true ? 'Yes' : 'No',
        Explain: modelData.Explain,
        OfficeId: modelData.OfficeId
      };
    }

    this.employeeSelectedValue = modelData.EmployeeId; // For Dropdown bind

    this.showEditEmpExitInterviewForm();
  }
  //#endregion

  //#region  "onEmployeeSelectedValue"
  onEmployeeSelectedValue(data: any) {
    if (data) {
      this.employeeSelectedValue = data.value;
      this.getEmployeeMoreDetailByEmpId(data.value);
    }
  }
  //#endregion

  //#region "on Back Button Click"
  onBackButtonClick() {
    if (this.empExitInterviewFormListFlag === true) {
      this.showAddEmpExitInterviewForm();
    } else if (this.empExitInterviewFormListFlag === false) {
      this.hideAddEmpExitInterviewForm();
    }
  }
  //#endregion

  //#region "onDeleteEmpExitInterviewShowPopup"
  onDeleteEmpExitInterviewShowPopup(data: any) {
    if (data != null) {
      this.selectedEmployeeForDelete = data.key.ExistInterviewDetailsId;
      this.showDeleteConfVisiblePopup();
    }
  }
  //#endregion

  //#region "onDeleteConfirmationClick"
  onDeleteConfirmationClick() {
    // tslint:disable-next-line:curly
    if (this.selectedEmployeeForDelete != null)
      this.hrService
        .DeleteExitInterviewForm(
          this.setting.getBaseUrl() + GLOBAL.API_Code_DeleteExitInterview,
          this.selectedEmployeeForDelete
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Deleted Successfully');
            } else {
              // tslint:disable-next-line:curly
              if (data.StatusCode === 400)
                this.toastr.warning('Something went wrong!');
            }
            this.hideDeleteConfVisiblePopup();
            this.GetAllExitInterviewByOfficeId();
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
            }
            this.hideDeleteConfVisiblePopup();
          }
        );
  }
  //#endregion

  //#region "Show / Hide"
  showAddEmpExitInterviewForm() {
    this.initExitForm();
    this.employeeSelectedValue = null; // init dropdown
    this.employeeSelectedValueFlag = false; // dropdown enable disabled
    this.empExitInterviewFormListFlag = false;
    this.addEmpExitInterviewDetailsFlag = true;
    this.isExitInterviewViewOnly = false;
  }

  showEditEmpExitInterviewForm() {
    this.employeeSelectedValueFlag = true; // dropdown enable disabled

    this.empExitInterviewFormListFlag = false;
    this.addEmpExitInterviewDetailsFlag = true;
  }

  hideAddEmpExitInterviewForm() {
    this.empExitInterviewFormListFlag = true;
    this.addEmpExitInterviewDetailsFlag = false;
  }

  showDeleteConfVisiblePopup() {
    this.deleteConfVisiblePopup = true;
  }
  hideDeleteConfVisiblePopup() {
    this.deleteConfVisiblePopup = false;
  }
  //#endregion
  //#region "exportPdf"
  exportPdf(modelData: any) {
    if (modelData.data != null && modelData.data !== undefined) {
      this.empExitInterviewFormMainForm = {
        ExistInterviewDetailsId: modelData.data.ExistInterviewDetailsId,
        EmployeeId: modelData.data.EmployeeId,

        EmployeeCode: modelData.data.EmployeeCode,
        EmployeeName: modelData.data.EmployeeName,
        Position: null,
        Department: modelData.data.Department,
        TenureWithCHA: modelData.data.TenureWithCHA,
        Gender: modelData.data.Gender,

        // FeelingAboutEmployee
        DutiesOfJob: modelData.data.DutiesOfJob,
        TrainingAndDevelopmentPrograms:
          modelData.data.TrainingAndDevelopmentPrograms,
        OpportunityAdvancement: modelData.data.OpportunityAdvancement,
        SalaryTreatment: modelData.data.SalaryTreatment,
        BenefitProgram: modelData.data.BenefitProgram,
        WorkingConditions: modelData.data.WorkingConditions,
        WorkingHours: modelData.data.WorkingHours,
        CoWorkers: modelData.data.CoWorkers,
        Supervisors: modelData.data.Supervisors,
        GenderFriendlyEnvironment: modelData.data.GenderFriendlyEnvironment,
        OverallJobSatisfaction: modelData.data.OverallJobSatisfaction,

        // ReasonOfLeaving
        Benefits: modelData.data.Benefits,
        BetterJobOpportunity: modelData.data.BetterJobOpportunity,
        FamilyReasons: modelData.data.FamilyReasons,
        NotChallenged: modelData.data.NotChallenged,
        Pay: modelData.data.Pay,
        PersonalReasons: modelData.data.PersonalReasons,
        Relocation: modelData.data.Relocation,
        ReturnToSchool: modelData.data.ReturnToSchool,
        ConflictWithSuoervisors: modelData.data.ConflictWithSuoervisors,
        ConflictWithOther: modelData.data.ConflictWithOther,
        WorkRelationship: modelData.data.WorkRelationship,
        CompanyInstability: modelData.data.CompanyInstability,
        CareerChange: modelData.data.CareerChange,
        HealthIssue: modelData.data.HealthIssue,

        // TheDepartment
        HadGoodSynergy: modelData.data.HadGoodSynergy,
        HadAdequateEquipment: modelData.data.HadAdequateEquipment,
        WasAdequatelyStaffed: modelData.data.WasAdequatelyStaffed,
        WasEfficient: modelData.data.WasEfficient,

        // TheJobItself
        JobWasChallenging: modelData.data.JobWasChallenging,
        SkillsEffectivelyUsed: modelData.data.SkillsEffectivelyUsed,
        JobOrientation: modelData.data.JobOrientation,
        WorkLoadReasonable: modelData.data.WorkLoadReasonable,
        SufficientResources:
          modelData.data.SufficientResources === undefined
            ? null
            : modelData.data.SufficientResources,
        WorkEnvironment: modelData.data.WorkEnvironment,
        ComfortableAppropriately: modelData.data.ComfortableAppropriately,
        Equipped: modelData.data.Equipped,

        // MySupervisor
        HadKnowledgeOfJob: modelData.data.HadKnowledgeOfJob,
        HadKnowledgeSupervision: modelData.data.HadKnowledgeSupervision,
        WasOpenSuggestions: modelData.data.WasOpenSuggestions,
        RecognizedEmployeesContribution:
          modelData.data.RecognizedEmployeesContribution,

        // TheManagement
        GaveFairTreatment: modelData.data.GaveFairTreatment,
        WasAvailableToDiscuss: modelData.data.WasAvailableToDiscuss,
        WelcomedSuggestions:
          modelData.data.WelcomedSuggestions === undefined
            ? null
            : modelData.data.WelcomedSuggestions,
        MaintainedConsistent:
          modelData.data.MaintainedConsistent === undefined
            ? null
            : modelData.data.MaintainedConsistent,
        ProvidedRecognition: modelData.data.ProvidedRecognition,
        EncouragedCooperation: modelData.data.EncouragedCooperation,
        ProvidedDevelopment: modelData.data.ProvidedDevelopment,

        Question: modelData.data.Question,
        Explain:
          modelData.data.Explain === undefined ? null : modelData.data.Explain,
        OfficeId: modelData.data.OfficeId
      };

      this.hrService
        .DownloadPDF(
          this.setting.getBaseUrl() + GLOBAL.API_Pdf_GetEmployeeExitInteviewPdf,
          this.empExitInterviewFormMainForm
        )
        .subscribe(
          x => {
            this.fileName = 'EmployeeExitInterviewReport' + '.pdf';
            if (window.navigator.msSaveOrOpenBlob) {
              window.navigator.msSaveOrOpenBlob(x, this.fileName);
            } else {
              const link = document.createElement('a');
              link.setAttribute('type', 'hidden');
              link.download = this.fileName;
              link.href = window.URL.createObjectURL(x);
              document.body.appendChild(link);
              link.click();
            }
          },
          error => {
            this.toastr.warning(error);
          }
        );
    }
  }
  //#endregion

  //#region "exportPdf"
  exportAnualReportPdf() {
    const data = {
      OfficeId : this.officeId
    };
    this.hrService
      .DownloadPDF(
        this.setting.getBaseUrl() + GLOBAL.API_Pdf_EmployeeAnnualTunoverReport,
        data
      )
      .subscribe(
        x => {
          this.fileName = 'EmployeeAnnualTunoverReport' + '.pdf';
          if (window.navigator.msSaveOrOpenBlob) {
            window.navigator.msSaveOrOpenBlob(x, this.fileName);
          } else {
            const link = document.createElement('a');
            link.setAttribute('type', 'hidden');
            link.download = this.fileName;
            link.href = window.URL.createObjectURL(x);
            document.body.appendChild(link);
            link.click();
          }
        },
        error => {
          this.toastr.warning(error);
        }
      );
  }
  //#endregion
}
