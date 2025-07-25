import {
  Component,
  OnInit,
  Input,
  SimpleChanges,
  OnChanges
} from '@angular/core';
import { HrService } from '../../hr.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService } from '../../../../service/common.service';
import { CodeService } from '../../../code/code.service';
import {
  IInterviewLanguagesModel,
  IInterviewTrainingModel,
  IInterviewTechnicalQuestionModel,
  IEmployeeListModel,
  ISkillRatingModel,
  IEmpInterviewFormModel
} from '../interview-form.models';

@Component({
  selector: 'app-employee-interview-form',
  templateUrl: './employee-interview-form.component.html',
  styleUrls: ['./employee-interview-form.component.css']
})
export class EmployeeInterviewFormComponent implements OnInit, OnChanges {
  //#region "Variables"

  // Data Source
  interviewDataSource: any[];
  languagesListDataSource: IInterviewLanguagesModel[];
  trainingListDataSource: IInterviewTrainingModel[];
  technicalQuestionsListDataSource: IInterviewTechnicalQuestionModel[] = [];
  technicalQuestionsList: any[];
  Interviewers: any[] = [];
  officecodelist: any[] = [];
  officeDropdownList: any[];
  selectedOffice: any;

  employeeListDataSource: IEmployeeListModel[];
  genderTypesDropdown: any[];
  ratingBasedDropDown: any[];
  trainingTypeDropdown: any[];
  departmentTypeDropdown: any[];

  currentInterviewDetailsId: number;
  currentApproveReject: boolean;
  interviewFormViewOnly: boolean;
  @Input() officeId: any;

  yesNoRadioGroup: any[];
  interviewFormRadioGroup: any[];
  ratingBasedCriteriaDataSource: any[] = [];
  ratingBasedCriteriaQuestionsList: any[];
  @Input() isEditingAllowed: boolean;

  // form
  empInterviewFormMainForm: IEmpInterviewFormModel;

  skillRatingDropdown: ISkillRatingModel[];
  skillRatingRadioGroup: any[];
  jobCodeDropdown: any[];

  // Flag
  empInterviewFormListFlag = true;
  addEmpInterviewDetailsFlag = false;
  viewEmpInterviewDetailsFlag = false;
  editEmpInterviewDetailsFlag = false;
  selectEmpDropdownFlag = false;

  // Popup
  popupApprovalFormConfVisible = false;

  // loader
  empInterviewFormLoader = false;

  // for details
  employeeId = 0;

  //#endregion

  constructor(
    private hrService: HrService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService,
    private route: ActivatedRoute,
    private codeService: CodeService
  ) {}
  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.employeeId = +params['empId'];
    });
    this.initializeForm();
  }

  ngOnChanges(changes: SimpleChanges) {
    // this.getAllInterviewDetails();

    if (changes !== undefined && changes.officeId !== undefined) {
      this.officeId = changes.officeId.currentValue;
      console.log(changes.officeId.currentValue);
      this.getAllEmployeeListByOfficeId();
      //  this.getAllInterviewDetails();
      this.getjobCodeList();
      this.getDepartmentType(this.officeId);

      this.technicalQuestionsList = [];
      this.ratingBasedCriteriaQuestionsList = [];
      this.getAllCriteriaQuestions();
      this.getAllInterviewQuestions();
    }
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
    this.technicalQuestionsList = [
      {
        Question: null,
        InterviewTechnicalQuestionsId: null
      }
    ];
    this.ratingBasedCriteriaQuestionsList = [
      {
        CriteriaQuestion: null,
        CriteriaQuestionId: null
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
    this.technicalQuestionsListDataSource = [];

    this.empInterviewFormMainForm = {
      InterviewDetailsId: null,

      EmployeeID: null,
      JobId: null,
      Qualification: null,
      DutyStation: null,
      PassportNo: null,

      University: null,

      PlaceOfBirth: null,
      TazkiraIssuePlace: null,
      MaritalStatus: null,

      Experience: null,
      RatingBasedCriteriaModelList: [],

      ProfessionalCriteriaMarks: 0,

      MarksObtained: 0,
      WrittenTestMarks: 0,
      Ques1: null,
      Ques2: null,
      Ques3: null,
      PreferedLocation: null,
      NoticePeriod: null,
      JoiningDate: new Date(),

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
    // this.getAllCriteriaQuestions();
    // this.getAllInterviewQuestions();
  }

  //#region "getAllInterviewDetails"
  getAllInterviewDetails() {
    if (this.officeId !== undefined) {
      this.empInterviewFormLoader = true;

      this.hrService
        .GetAllDetailsByOfficeId(
          this.setting.getBaseUrl() + GLOBAL.API_Hr_GetAllInterviewDetails,
          this.officeId
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
              if (this.employeeId > 0) {
                const interviewDetailbyId = this.interviewDataSource.find(
                  r => r.EmployeeID === this.employeeId
                );
                this.onEditEmpInterviewShowForm(interviewDetailbyId, false);
              }
            } else {
              // tslint:disable-next-line:curly
              if (data.data.InterviewDetailList == null) {
                // this.toastr.warning('No record found!');
              } else if (data.StatusCode === 400) {
                this.toastr.error('Something went wrong!');
              }
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
  }
  //#endregion

  //#region "Get All Employee List By OfficeId"
  getAllEmployeeListByOfficeId() {
    // tslint:disable-next-line:radix
    const officeId = this.officeId;
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
            this.getAllInterviewDetails();
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

  //#region "Get All Job Code"
  getjobCodeList() {
    if (this.officeId !== undefined) {
      // tslint:disable-next-line:radix
      const officeId = this.officeId;
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
  }
  //#endregion "Get All Job Code"

  technicalQuestionsValueChanged(obj) {
    let totalProfesionalMarks = 0;
    this.technicalQuestionsListDataSource.forEach(element => {
        totalProfesionalMarks = totalProfesionalMarks + Number(element.Answer);
      });
      this.empInterviewFormMainForm.MarksObtained = totalProfesionalMarks;
  }
  ratingCriteriaQuestionsValueChanged(obj) {
    let totalCriteriaMarks = 0;
    this.ratingBasedCriteriaDataSource.forEach(element => {
        totalCriteriaMarks = totalCriteriaMarks + Number(element.Rating);
      });
      this.empInterviewFormMainForm.ProfessionalCriteriaMarks = totalCriteriaMarks;
  }

  //#region "on Add Exit Interview Form Submit"
  onAddInterviewFormSubmit(model: IEmpInterviewFormModel) {
    const interviewFormModel: IEmpInterviewFormModel = {
      InterviewDetailsId: 0,
      EmployeeID: model.EmployeeID,
      JobId: model.JobId,
      Qualification: null,
      DutyStation: model.DutyStation,
      PassportNo: model.PassportNo,

      University: model.University,

      PlaceOfBirth: model.PlaceOfBirth,
      TazkiraIssuePlace: model.TazkiraIssuePlace,
      MaritalStatus: model.MaritalStatus,

      Experience: model.Experience,
      RatingBasedCriteriaModelList: this.ratingBasedCriteriaDataSource,

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
    const interviewFormModel: IEmpInterviewFormModel = {
      InterviewDetailsId: model.InterviewDetailsId,
      EmployeeID: model.EmployeeID,
      JobId: model.JobId,

      Qualification: model.Qualification,
      DutyStation: model.DutyStation,

      PassportNo: model.PassportNo,

      University: model.University,

      PlaceOfBirth: model.PlaceOfBirth,
      TazkiraIssuePlace: model.TazkiraIssuePlace,
      MaritalStatus: model.MaritalStatus,

      Experience: model.Experience,

      RatingBasedCriteriaModelList: this.ratingBasedCriteriaDataSource,

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
  onEditEmpInterviewShowForm(model: IEmpInterviewFormModel, viewOnly: boolean) {
    if (model != null) {
      this.disableSelectEmpDropdownFlag();
      this.interviewFormViewOnly = viewOnly;
      this.empInterviewFormMainForm = {
        InterviewDetailsId: model.InterviewDetailsId,
        EmployeeID: model.EmployeeID,
        JobId: model.JobId,
        DutyStation: model.DutyStation,
        Qualification: model.Qualification,
        PassportNo: model.PassportNo,
        University: model.University,
        PlaceOfBirth: model.PlaceOfBirth,
        TazkiraIssuePlace: model.TazkiraIssuePlace,
        MaritalStatus: model.MaritalStatus,

        Experience: model.Experience,
        RatingBasedCriteriaModelList:
          model.RatingBasedCriteriaModelList === undefined
            ? []
            : model.RatingBasedCriteriaModelList,

        ProfessionalCriteriaMarks: model.ProfessionalCriteriaMarks,

        MarksObtained: model.MarksObtained,
        WrittenTestMarks: model.WrittenTestMarks,
        Ques1: model.Ques1,
        Ques2: model.Ques2,
        Ques3: model.Ques3,
        PreferedLocation: model.PreferedLocation,
        NoticePeriod: model.NoticePeriod,
        JoiningDate: model.JoiningDate,

        InterviewLanguageModelList:
          model.InterviewLanguageModelList === undefined
            ? []
            : model.InterviewLanguageModelList,
        InterviewTrainingModelList:
          model.InterviewTrainingModelList === undefined
            ? []
            : model.InterviewTrainingModelList,
        InterviewTechQuesModelList:
          model.InterviewTechQuesModelList === undefined
            ? []
            : model.InterviewTechQuesModelList,

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
        Interviewers:
          model.Interviewers === undefined ? [] : model.Interviewers,

        InterviewStatus: model.InterviewStatus
      };
    }

    this.ratingBasedCriteriaDataSource =
      model === undefined
        ? []
        : model.RatingBasedCriteriaModelList === undefined
        ? []
        : model.RatingBasedCriteriaModelList;

    this.languagesListDataSource =
      model === undefined
        ? []
        : model.InterviewLanguageModelList === undefined
        ? []
        : model.InterviewLanguageModelList;

    this.trainingListDataSource =
      model === undefined
        ? []
        : model.InterviewTrainingModelList === undefined
        ? []
        : model.InterviewTrainingModelList;

    this.technicalQuestionsListDataSource =
      model === undefined
        ? []
        : model.InterviewTechQuesModelList === undefined
        ? []
        : model.InterviewTechQuesModelList;

    this.Interviewers =
      model === undefined
        ? []
        : model.Interviewers === undefined
        ? []
        : model.Interviewers;

    // TODO: disable Save button
    this.currentApproveReject =
      model === undefined ? true : model.InterviewStatus == null ? true : false;
if (this.interviewFormViewOnly === false) {
    this.showEditEmpInterviewForm();
} else {
  this.showViewEmpInterviewForm();
}
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

  //#region  "getOfficeCodeList"
  getOfficeCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          this.officecodelist = [];
          if (
            data.StatusCode === 200 &&
            data.data.OfficeDetailsList.length > 0
          ) {
            data.data.OfficeDetailsList.forEach(element => {
              this.officecodelist.push({
                Office: element.OfficeId,
                OfficeCode: element.OfficeCode,
                OfficeName: element.OfficeName,
                SupervisorName: element.SupervisorName,
                PhoneNo: element.PhoneNo,
                FaxNo: element.FaxNo,
                OfficeKey: element.OfficeKey
              });
            });

            const AllOffices = localStorage.getItem('ALLOFFICES').split(',');

            data.data.OfficeDetailsList.forEach(element => {
              const officeFound = AllOffices.indexOf('' + element.OfficeId);
              if (officeFound !== -1) {
                this.officeDropdownList.push({
                  OfficeId: element.OfficeId,
                  OfficeCode: element.OfficeCode,
                  OfficeName: element.OfficeName,
                  SupervisorName: element.SupervisorName,
                  PhoneNo: element.PhoneNo,
                  FaxNo: element.FaxNo,
                  OfficeKey: element.OfficeKey
                });
              }
            });

            this.selectedOffice =
              this.selectedOffice === null || this.selectedOffice === undefined
                ? this.officeDropdownList[0].OfficeId
                : this.selectedOffice;

            this.getAllEmployeeListByOfficeId();
            this.getjobCodeList();
            this.getAllInterviewDetails();
            this.getDepartmentType(this.selectedOffice);
            // this.getAllEmployeeAppraisalMoreDetails();

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
  //#endregion

  //#region "on Back Button Click"
  onBackButtonClick() {
    this.viewEmpInterviewDetailsFlag = false;
    if (this.empInterviewFormListFlag === true) {
      this.empInterviewFormListFlag = false;
      this.addEmpInterviewDetailsFlag = true;
    } else if (this.empInterviewFormListFlag === false) {
      this.empInterviewFormListFlag = true;
      this.addEmpInterviewDetailsFlag = false;
    }
  }
  //#endregion

  //#region "Get Department Type"
  getDepartmentType(eventId: any) {
    this.hrService
      .GetDepartmentDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetDepartmentsByOfficeId,
        eventId
      )
      .subscribe(
        data => {
          this.departmentTypeDropdown = [];
          if (
            data.data.Departments != null &&
            data.data.Departments.length > 0
          ) {
            data.data.Departments.forEach(element => {
              this.departmentTypeDropdown.push(element);
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
          }
        }
      );
  }

  //#region "Get All Interview Questions"
  getAllInterviewQuestions() {
    // tslint:disable-next-line:radix
    const officeId = this.officeId;
    this.codeService
      .GetAppraisalQuestions(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetInterviewQuestions,
        officeId
      )
      .subscribe(
        data => {
          this.technicalQuestionsList = [];
          if (
            data.StatusCode === 200 &&
            data.data.InterviewTechnicalQuestionsList.length > 0 &&
            data.data.InterviewTechnicalQuestionsList != null
          ) {
            data.data.InterviewTechnicalQuestionsList.forEach(element => {
              this.technicalQuestionsList.push(element);
            });
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

  //#region "Get All Interview Questions"
  getAllCriteriaQuestions() {
    // tslint:disable-next-line:radix
    const officeId = this.officeId;
    this.codeService
      .GetAppraisalQuestions(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_GetRatingBasedCriteriaQuestions,
        officeId
      )
      .subscribe(
        data => {
          this.ratingBasedCriteriaQuestionsList = [];
          if (
            data.StatusCode === 200 &&
            data.data.RatingBasedCriteriaQuestionList.length > 0 &&
            data.data.RatingBasedCriteriaQuestionList != null
          ) {
            data.data.RatingBasedCriteriaQuestionList.forEach(element => {
              this.ratingBasedCriteriaQuestionsList.push({
                CriteriaQuestionId: element.QuestionsId,
                CriteriaQuestion: element.Question
              });
            });
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
  // #endregion

  //#region "Add Training"
  logEventTraining(eventName, obj) {
    obj.data.StartDate = new Date();
    obj.data.EndDate = new Date();
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

  showViewEmpInterviewForm() {
    this.empInterviewFormListFlag = false;
    this.viewEmpInterviewDetailsFlag = true;
  }

  hideAddEmpInterviewForm() {
    this.empInterviewFormListFlag = true;
    this.addEmpInterviewDetailsFlag = false;
    this.viewEmpInterviewDetailsFlag = false;
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
