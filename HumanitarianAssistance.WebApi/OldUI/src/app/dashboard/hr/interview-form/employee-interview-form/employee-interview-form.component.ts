import { Component, OnInit, Input } from '@angular/core';
import { HrService } from '../../hr.service';
import { CodeService } from '../../../code/code.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService } from '../../../../service/common.service';

@Component({
  selector: 'app-employee-interview-form',
  templateUrl: './employee-interview-form.component.html',
  styleUrls: ['./employee-interview-form.component.css']
})
export class EmployeeInterviewFormComponent implements OnInit {
  //#region "Variables"

  // Data Source
  interviewDataSource: any[];
  languagesListDataSource: InterviewLanguagesModel[];
  trainingListDataSource: InterviewTrainingModel[];
  technicalQuestionsListDataSource: InterviewTechnicalQuestionModel[];
  Interviewers: any[]= [];

  employeeListDataSource: EmployeeListModel[];
  genderTypesDropdown: any[];
  ratingBasedDropDown: any[];
  trainingTypeDropdown: any[];

  currentInterviewDetailsId: number;
  currentApproveReject: boolean;
  interviewFormViewOnly: boolean;

  yesNoRadioGroup: any[];
  interviewFormRadioGroup: any[];
  ratingBasedCriteriaDataSource: any[];
  @Input() isEditingAllowed: boolean;

  // form
  empInterviewFormMainForm: EmpInterviewFormModel;

  skillRatingDropdown: SkillRatingModel[];
  skillRatingRadioGroup: any[];
  jobCodeDropdown: any[];

  // Flag
  empInterviewFormListFlag = true;
  addEmpInterviewDetailsFlag = false;
  editEmpInterviewDetailsFlag = false;
  selectEmpDropdownFlag = false;

  // Popup
  popupApprovalFormConfVisible = false;

  // loader
  empInterviewFormLoader = false;

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
    this.getAllEmployeeListByOfficeId();
    this.getjobCodeList();

    this.getAllInterviewDetails();
  }

  initializeForm() {
    this.yesNoRadioGroup = ['Yes', 'No'];

    this.interviewFormRadioGroup = ['Approve', 'Reject'];

    this.genderTypesDropdown = [
      { GenderTypeId: 1, GenderTypeName: 'Male' },
      { GenderTypeId: 2, GenderTypeName: 'Female' },
      { GenderTypeId: 3, GenderTypeName: 'Other' }
    ];

    this.trainingTypeDropdown = [
      {
        TrainingTypeId: 1,
        TrainingTypeName: 'Languages'
      },
      {
        TrainingTypeId: 2,
        TrainingTypeName: 'Designing'
      },
      {
        TrainingTypeId: 3,
        TrainingTypeName: 'Other'
      }
    ];

    this.ratingBasedDropDown = [
      'Poor',
      'Below Average',
      'Average',
      'Above Average',
      'OutStanding'
    ];

    this.skillRatingDropdown = [
      {
        SkillRatingId: 1,
        SkillRatingName: 'Poor'
      },
      {
        SkillRatingId: 2,
        SkillRatingName: 'Good'
      },
      {
        SkillRatingId: 3,
        SkillRatingName: 'Very Good'
      },
      {
        SkillRatingId: 4,
        SkillRatingName: 'Excellent'
      }
    ];

    this.skillRatingRadioGroup = [
      {
        SkillRatingId: 1,
        SkillRatingName: 'Poor'
      },
      {
        SkillRatingId: 2,
        SkillRatingName: 'Fair'
      },
      {
        SkillRatingId: 3,
        SkillRatingName: 'Good'
      },
      {
        SkillRatingId: 4,
        SkillRatingName: 'Excellent'
      },
      {
        SkillRatingId: 5,
        SkillRatingName: 'Perfect'
      }
    ];

    this.initInterviewForm();
  }

  initInterviewForm() {
    this.languagesListDataSource = [
      {
        LanguageName: 'Dari',
        LanguageId: 1,
        Listening: null,
        Reading: null,
        Speaking: null,
        Writing: null
      },
      {
        LanguageName: 'Pashto',
        LanguageId: 2,
        Listening: null,
        Reading: null,
        Speaking: null,
        Writing: null
      }
    ];

    this.trainingListDataSource = [];
    this.ratingBasedCriteriaDataSource = [];

    this.technicalQuestionsListDataSource = [
      {
        TechnicalQuestionId: 1,
        Question: 'Accounting',
        Answer: null
      },
      {
        TechnicalQuestionId: 2,
        Question: 'Communication Skills',
        Answer: null
      }
    ];

    this.empInterviewFormMainForm = {
      InterviewDetailsId: null,

      EmployeeID: null,
      JobId: null,

      PassportNo: null,

      University: null,

      PlaceOfBirth: null,
      TazkiraIssuePlace: null,
      MaritalStatus: null,

      Experience: null,
      RatingBasedCriteriaList: [],

      ProfessionalCriteriaMarks: 0,

      MarksObtained: 0,
      WrittenTestMarks: 0,
      Ques1: null,
      Ques2: null,
      Ques3: null,
      PreferedLocation: null,
      NoticePeriod: null,
      JoiningDate: null,

      InterviewLanguageModelList: [],
      InterviewTrainingModelList: [],
      InterviewTechQuesModelList: [],

      // Compensation
      CurrentBase: 0,
      CurrentTransportation: false,
      CurrentMeal: false,
      CurrentOther: 0,
      ExpectationBase: 0,
      ExpectationTransportation: false,
      ExpectationMeal: false,
      ExpectationOther: 0,
      TotalMarksObtained: 0,

      // Recommendation
      Status: null,
      Interviewers: [],
      InterviewStatus: null
    };
    this.Interviewers = [];
  }

  //#region "getAllInterviewDetails"
  getAllInterviewDetails() {
    this.empInterviewFormLoader = true;

    this.hrService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_GetAllInterviewDetails
      )
      .subscribe(
        data => {
          this.interviewDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.InterviewDetailList != null &&
            data.data.InterviewDetailList.length > 0
          ) {
            data.data.InterviewDetailList.forEach(element => {
              this.interviewDataSource.push(element);
            });
          } else {
            // tslint:disable-next-line:curly
            if (data.data.InterviewDetailList == null)
              this.toastr.warning('No record found!');
            // tslint:disable-next-line:curly
            else if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');
          }
          this.empInterviewFormLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.empInterviewFormLoader = false;
        }
      );
  }
  //#endregion

  //#region "Get All Employee List By OfficeId"
  getAllEmployeeListByOfficeId() {
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
    this.hrService
      .GetAllDetail(
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

  //#region "Get All Job Code"
  getjobCodeList() {
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
    this.hrService
      .GetAllDetail(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllJobHiringDetails,
        officeId
      )
      .subscribe(
        data => {
          this.jobCodeDropdown = [];
          if (
            data.data.JobHiringDetailsList != null &&
            data.data.JobHiringDetailsList.length > 0
          ) {
            data.data.JobHiringDetailsList.forEach(element => {
              // Need only Active job
              if (element.IsActive === true) {
                this.jobCodeDropdown.push({
                  JobId: element.JobId,
                  JobCode: element.JobCode,
                  ProfessionId: element.ProfessionId,
                  ProfessionName: element.ProfessionName,
                  GradeId: element.GradeId,
                  GradeName: element.GradeName
                });
              }
            });
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
  }
  //#endregion "Get All Job Code"

  //#region "on Add Exit Interview Form Submit"
  onAddInterviewFormSubmit(model: EmpInterviewFormModel) {

    const interviewFormModel: EmpInterviewFormModel = {
      InterviewDetailsId: 0,
      EmployeeID: model.EmployeeID,
      JobId: model.JobId,

      PassportNo: model.PassportNo,

      University: model.University,

      PlaceOfBirth: model.PlaceOfBirth,
      TazkiraIssuePlace: model.TazkiraIssuePlace,
      MaritalStatus: model.MaritalStatus,

      Experience: model.Experience,
      RatingBasedCriteriaList: this.ratingBasedCriteriaDataSource,

      ProfessionalCriteriaMarks: model.ProfessionalCriteriaMarks,

      MarksObtained: model.MarksObtained,
      WrittenTestMarks: model.WrittenTestMarks,
      Ques1: model.Ques1,
      Ques2: model.Ques2,
      Ques3: model.Ques3,
      PreferedLocation: model.PreferedLocation,
      NoticePeriod: model.NoticePeriod,
      JoiningDate: model.JoiningDate,

      InterviewLanguageModelList: this.languagesListDataSource,
      InterviewTrainingModelList: this.trainingListDataSource,
      InterviewTechQuesModelList: this.technicalQuestionsListDataSource,

      // Compensation
      CurrentBase: model.CurrentBase,
      CurrentTransportation: model.CurrentTransportation,
      CurrentMeal: model.CurrentMeal,
      CurrentOther: model.CurrentOther,
      ExpectationBase: model.ExpectationBase,
      ExpectationTransportation: model.ExpectationTransportation,
      ExpectationMeal: model.ExpectationMeal,
      ExpectationOther: model.ExpectationOther,

      TotalMarksObtained: model.TotalMarksObtained,

      // Recommendation
      Status: model.Status,
      Interviewers: this.Interviewers,

      InterviewStatus: null
    };

    this.empInterviewFormLoader = true;

    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_AddInterviewDetails,
        interviewFormModel
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

          this.empInterviewFormLoader = false;

          this.hideAddEmpInterviewForm();
          this.getAllInterviewDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }

          this.empInterviewFormLoader = false;
        }
      );
  }
  //#endregion

  //#region "on Edit Exit Interview Form Submit"
    onEditInterviewFormSubmit(model: any) {
        
    const interviewFormModel: EmpInterviewFormModel = {
      InterviewDetailsId: model.InterviewDetailsId,
      EmployeeID: model.EmployeeID,
      JobId: model.JobId,

      PassportNo: model.PassportNo,

      University: model.University,

      PlaceOfBirth: model.PlaceOfBirth,
      TazkiraIssuePlace: model.TazkiraIssuePlace,
      MaritalStatus: model.MaritalStatus,

      Experience: model.Experience,

        RatingBasedCriteriaList: this.ratingBasedCriteriaDataSource,

      ProfessionalCriteriaMarks: model.ProfessionalCriteriaMarks,

      MarksObtained: model.MarksObtained,
      WrittenTestMarks: model.WrittenTestMarks,
      Ques1: model.Ques1,
      Ques2: model.Ques2,
      Ques3: model.Ques3,
      PreferedLocation: model.PreferedLocation,
      NoticePeriod: model.NoticePeriod,
      JoiningDate: model.JoiningDate,

        InterviewLanguageModelList: this.languagesListDataSource,
        InterviewTrainingModelList: this.trainingListDataSource,
        InterviewTechQuesModelList: this.technicalQuestionsListDataSource,

      // Compensation
      CurrentBase: model.CurrentBase,
      CurrentTransportation: model.CurrentTransportation,
      CurrentMeal: model.CurrentMeal,
      CurrentOther: model.CurrentOther,
      ExpectationBase: model.ExpectationBase,
      ExpectationTransportation: model.ExpectationTransportation,
      ExpectationMeal: model.ExpectationMeal,
      ExpectationOther: model.ExpectationOther,

      TotalMarksObtained: model.TotalMarksObtained,

      // Recommendation
      Status: model.Status,
      Interviewers: model.Interviewers,
      InterviewStatus: model.InterviewStatus
    };

    this.empInterviewFormLoader = true;

    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_EditInterviewDetails,
        interviewFormModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully');
          } else {
            // tslint:disable-next-line:curly
            if (data.StatusCode === 400)
              this.toastr.warning('Something went wrong!');
          }

          this.empInterviewFormLoader = false;

          this.hideAddEmpInterviewForm();
          this.getAllInterviewDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }

          this.empInterviewFormLoader = false;
        }
      );
  }
  //#endregion

  //#region "onEditEmpInterviewShowForm"
    onEditEmpInterviewShowForm(model: EmpInterviewFormModel, viewOnly: boolean) {
    if (model != null) {
      this.disableSelectEmpDropdownFlag();
      this.interviewFormViewOnly = viewOnly;

      this.empInterviewFormMainForm = {
        InterviewDetailsId: model.InterviewDetailsId,
        EmployeeID: model.EmployeeID,
        JobId: model.JobId,
        PassportNo: model.PassportNo,
        University: model.University,
        PlaceOfBirth: model.PlaceOfBirth,
        TazkiraIssuePlace: model.TazkiraIssuePlace,
        MaritalStatus: model.MaritalStatus,

        Experience: model.Experience,
        RatingBasedCriteriaList: model.RatingBasedCriteriaList === undefined ? [] : model.RatingBasedCriteriaList,

        ProfessionalCriteriaMarks: model.ProfessionalCriteriaMarks,

        MarksObtained: model.MarksObtained,
        WrittenTestMarks: model.WrittenTestMarks,
        Ques1: model.Ques1,
        Ques2: model.Ques2,
        Ques3: model.Ques3,
        PreferedLocation: model.PreferedLocation,
        NoticePeriod: model.NoticePeriod,
        JoiningDate: model.JoiningDate,

        InterviewLanguageModelList: model.InterviewLanguageModelList === undefined ? [] : model.InterviewLanguageModelList,
        InterviewTrainingModelList: model.InterviewTrainingModelList === undefined ? [] : model.InterviewTrainingModelList,
        InterviewTechQuesModelList: model.InterviewTechQuesModelList === undefined ? [] : model.InterviewTechQuesModelList,

        // Compensation
        CurrentBase: model.CurrentBase,
        CurrentTransportation: model.CurrentTransportation,
        CurrentMeal: model.CurrentMeal,
        CurrentOther: model.CurrentOther,
        ExpectationBase: model.ExpectationBase,
        ExpectationTransportation: model.ExpectationTransportation,
        ExpectationMeal: model.ExpectationMeal,
        ExpectationOther: model.ExpectationOther,

        TotalMarksObtained: model.TotalMarksObtained,

        // Recommendation
        Status: model.Status,
        Interviewers: model.Interviewers === undefined ? [] : model.Interviewers,

        InterviewStatus: model.InterviewStatus
      };
    }

    this.ratingBasedCriteriaDataSource = model.RatingBasedCriteriaList === undefined ? [] : model.RatingBasedCriteriaList;

    this.languagesListDataSource = model.InterviewLanguageModelList === undefined ? [] : model.InterviewLanguageModelList;
    this.trainingListDataSource = model.InterviewTrainingModelList === undefined ? [] : model.InterviewTrainingModelList;
    this.technicalQuestionsListDataSource = model.InterviewTechQuesModelList === undefined ? [] : model.InterviewTechQuesModelList;
    this.Interviewers = model.Interviewers === undefined ? [] : model.Interviewers;

    // TODO: disable Save button
    this.currentApproveReject = model.InterviewStatus == null ? true : false;

    this.showEditEmpInterviewForm();
  }
  //#endregion

  //#region "onExitInterviewFormSubmit"
  onInterviewFormSubmit(model: any) {
    if (model != null) {
      model.InterviewDetailsId === 0 || model.InterviewDetailsId == null
        ? this.onAddInterviewFormSubmit(model)
        : this.onEditInterviewFormSubmit(model);
    }
  }
  //#endregion

  //#region "ApproveRejectInterviewForm"
  ApproveRejectInterviewForm(
    InterviewDetailsId: number,
    approvalFlag: boolean
  ) {
    if (approvalFlag === true) {
      this.hrService
        .ApprovalAndRejectionInterviewForm(
          this.setting.getBaseUrl() +
            GLOBAL.API_Code_ApproveEmployeeInterviewRequest,
          InterviewDetailsId
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Interview Approved Successfully !');
            } else {
              // tslint:disable-next-line:curly
              if (data.StatusCode === 400)
                this.toastr.error('Something went wrong!');
            }
            this.getAllInterviewDetails();
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
            }
            this.getAllInterviewDetails();
          }
        );
    } else {
      this.hrService
        .ApprovalAndRejectionInterviewForm(
          this.setting.getBaseUrl() +
            GLOBAL.API_Code_RejectEmployeeInterviewRequest,
          InterviewDetailsId
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Interview Rejected Successfully !');
            } else {
              // tslint:disable-next-line:curly
              if (data.StatusCode === 400)
                this.toastr.error('Something went wrong!');
            }
            this.getAllInterviewDetails();
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
            }
            this.getAllInterviewDetails();
          }
        );
    }
  }

  //#endregion

  //#region "onIsInterviewFormApprovedValueChanged"
  onIsInterviewFormApprovedValueChanged(cellData: any, e: any) {
    this.showInterviewApprovalConfirmation();

    if (e != null && cellData != null) {
      this.currentInterviewDetailsId = cellData.key.InterviewDetailsId;
      e.value.toLowerCase() === 'approve'
        ? (this.currentApproveReject = true)
        : (this.currentApproveReject = false);
    }
  }
  //#endregion

  //#region "onApprovalConfirmationClick"
  onApprovalConfirmationClick() {
    this.currentApproveReject === true
      ? this.ApproveRejectInterviewForm(this.currentInterviewDetailsId, true)
      : this.ApproveRejectInterviewForm(this.currentInterviewDetailsId, false);
    this.hideInterviewApprovalConfirmation();
  }
  //#endregion

  //#region  "onEmployeeSelectedValue"
  onEmployeeSelectedValue(data: any) {
    if (data) {
    }
  }
  //#endregion

  //#region "on Back Button Click"
  onBackButtonClick() {
    if (this.empInterviewFormListFlag === true) {
      this.empInterviewFormListFlag = false;
      this.addEmpInterviewDetailsFlag = true;
    } else if (this.empInterviewFormListFlag === false) {
      this.empInterviewFormListFlag = true;
      this.addEmpInterviewDetailsFlag = false;
    }
  }
  //#endregion

  //#region "Show / hide"
  showAddEmpInterviewForm() {
    this.initInterviewForm();
    this.empInterviewFormListFlag = false;
    this.addEmpInterviewDetailsFlag = true;
    this.interviewFormViewOnly = false;

    this.currentApproveReject = true;
    this.enableSelectEmpDropdownFlag(); // Enable Employee Selection Dropdown
  }

  showEditEmpInterviewForm() {
    this.empInterviewFormListFlag = false;
    this.addEmpInterviewDetailsFlag = true;
  }

  hideAddEmpInterviewForm() {
    this.empInterviewFormListFlag = true;
    this.addEmpInterviewDetailsFlag = false;
  }

  showInterviewApprovalConfirmation() {
    this.popupApprovalFormConfVisible = true;
  }

  hideInterviewApprovalConfirmation() {
    this.popupApprovalFormConfVisible = false;
  }

  // Employee Dropdown
  disableSelectEmpDropdownFlag() {
    this.selectEmpDropdownFlag = true;
  }
  enableSelectEmpDropdownFlag() {
    this.selectEmpDropdownFlag = false;
  }

  //#endregion
}

class EmployeeListModel {
  EmployeeId: any;
  EmployeeName: any;
  EmployeeCode: any;
  CodeEmployeeName: any;
}

// Main Form
class EmpInterviewFormModel {
  InterviewDetailsId: number;
  EmployeeID: number;
  JobId: number;
  University: string;
  PassportNo: string;
  PlaceOfBirth: any;
  TazkiraIssuePlace: string;
  MaritalStatus: any;

  Experience: string;
  RatingBasedCriteriaList: any[];

  ProfessionalCriteriaMarks: number;

  MarksObtained: any;
  WrittenTestMarks: any;
  Ques1: any;
  Ques2: any;
  Ques3: any;
  PreferedLocation: any;
  NoticePeriod: any;
  JoiningDate: any;

  InterviewLanguageModelList: InterviewLanguagesModel[];
  InterviewTrainingModelList: InterviewTrainingModel[];
  InterviewTechQuesModelList: InterviewTechnicalQuestionModel[];

  // Compensation
  CurrentBase: any;
  CurrentTransportation: any;
  CurrentMeal: any;
  CurrentOther: any;
  ExpectationBase: any;
  ExpectationTransportation: any;
  ExpectationMeal: any;
  ExpectationOther: any;

  TotalMarksObtained: any;

  // Recommendation
  Status: number;
  Interviewers: any[];

  InterviewStatus: string;
}

class SkillRatingModel {
  SkillRatingId: number;
  SkillRatingName: string;
}

class InterviewLanguagesModel {
  LanguageName: string;
  LanguageId: number;
  Reading: number;
  Writing: number;
  Listening: number;
  Speaking: number;
}

class InterviewTrainingModel {
  TraininigType: any;
  TrainingName: any;
  StudyingCountry: any;
  StartDate: any;
  EndDate: any;
}

class InterviewTechnicalQuestionModel {
  TechnicalQuestionId: number;
  Question: any;
  Answer: string;
}
