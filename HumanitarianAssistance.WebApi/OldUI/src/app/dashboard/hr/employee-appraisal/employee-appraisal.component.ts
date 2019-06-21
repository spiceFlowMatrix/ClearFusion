import { Component, OnInit } from '@angular/core';
import { HrService } from '../hr.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import { CodeService } from '../../code/code.service';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-employee-appraisal',
  templateUrl: './employee-appraisal.component.html',
  styleUrls: ['./employee-appraisal.component.css']
})
export class EmployeeAppraisalComponent implements OnInit {
  //#region "Variables"

  employeeAppraisalListDataSource: EmployeeAppraisalModel[];
  employeeAppraisalQuestionForm: EmployeeAppraisalModel;

  questionSourceData: EmployeeAppraisalQuestionList[];
  remarksList: RemarkModel[];

  employeeListDataSource: EmployeeListModel[];
  employeeDetailListData: EmployeeDetailListModel[];
  employeeAppraisalPeriod: any[];

  // Appraisal More Details
  evaluationOfEmployees: any[];
  pointsOfEmployeesForm: EmployeeAppraisalMoreDetailsModel;
  employeeEvaluationDataSource: EmployeeEvaluationModel[];
  appraisalTeamMemberDataSource: any[];
  employeeMoreDetailListDataSource: any[];
  appraisalPeriodDropdown: any[];

  strongPointsSourceData: any[];
  weakPointsSourceData: any[];

  onEmployeeMoreDetailValue: number;
  isEditingAllowed = false;

  // Appraisal
  approveRejectEmployeeAppraisalDetailsId: number;
  currentAppraisalApproveReject: boolean;
  popupAppraisalFormConfVisible = false;

  // Evaluation
  approveRejectEmployeeEvaluationId: number;
  currentEvaluationApproveReject: boolean;
  popupEvaluationFormConfVisible = false;

  // Flag
  employeeSelectedFlag = false;
  employeeAppraisalListFlag = true; // MAIN GRID
  addAppraisalDetailsFlag = false;
  editAppraisalDetailsFlag = false;
  addAppraisalMoreDetailsFlag = false;
  editAppraisalMoreDetailsFlag = false;
  backButtonFlag = false;

  // Loader
  empAppraisalLoader = false;

  evaluationStatusRadioGroup = ['Approve', 'Reject'];

  trainingProgram = ['Organization vision & Objectives', 'Employee\'s Work'];

  yesNoItem = ['Yes', 'No'];

  catchLevelItem = [
    '1 - Weak',
    '2 - Satisfactory',
    '3 - Average',
    '4 - Good',
    '5 - Excellent'
  ];

  updatingFlag = true; // Question grid update flag in Appraisal Request Grid

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
    // this.commonService.setLoader(false);

    this.employeeAppraisalPeriod = [{
      PeriodId: 1,
      PeriodDuration: 'Annual'
    },
    {
      PeriodId: 2,
      PeriodDuration: 'Probationary'
    }];

    this.commonService.getEmployeeOfficeId().subscribe(data => {
      this.getAllEmployeeAppraisalList();
      this.getAllEmployeeAppraisalMoreDetails();
      this.getAllEmployeeListByOfficeId();
    });

    this.getAllRemarks();

    this.employeeEvaluationDataSource = [];

    // this.getAllEmployeeContractType();
    this.getAllAppraisalQuestions();
    this.initializeForm();

    this.getAllEmployeeListByOfficeId();

    this.getAllEmployeeAppraisalMoreDetails();
    this.getAllEmployeeAppraisalList();
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.EmployeeAppraisal
    );
  }

  //#region "Initialize Form"
  initializeForm() {
    this.initEmployeeAppraisalQuestionForm();

    this.pointsOfEmployeesForm = {
      EmployeeEvaluationId: null,
      EmployeeAppraisalDetailsId: null,

      EmployeeEvaluationModelList: null,
      StrongPoints: null,
      WeakPoints: null,
      CurrentAppraisalDate: null,

      FinalResultQues1: null,
      FinalResultQues2: null,
      FinalResultQues3: null,
      FinalResultQues4: null,
      FinalResultQues5: null,

      DirectSupervisor: null,

      EmployeeAppraisalTeamMemberList: null,

      CommentsByEmployee: null,

      EmployeeId: null,
      OfficeId: null,
      EvaluationStatus: null
    };
  }

  initEmployeeAppraisalQuestionForm() {
    this.employeeAppraisalQuestionForm = {
      EmployeeAppraisalDetailsId: null,
      EmployeeId: null,
      EmployeeCode: null,
      EmployeeName: null,
      FatherName: null,
      Position: null,
      Department: null,
      Qualification: null,
      DutyStation: null,
      RecruitmentDate: null,
      AppraisalPeriod: null,
      CurrentAppraisalDate: null,
      OfficeId: null,
      TotalScore: 0,
      EmployeeAppraisalQuestionList: null,
      AppraisalStatus: false
    };
  }
  //#endregion

  //#region "Get All Remarks"

  getAllRemarks() {
    this.remarksList = [
      {
        RemarkId: 0,
        RemarkName: '0 - Weak'
      },
      {
        RemarkId: 1,
        RemarkName: '1 - Satisfactory'
      },
      {
        RemarkId: 2,
        RemarkName: '2 - Average'
      },
      {
        RemarkId: 3,
        RemarkName: '3 - Good'
      },
      {
        RemarkId: 4,
        RemarkName: '4 - Excellent'
      }
    ];
  }

  //#endregion

  //#region "Get All Employee Appraisal List"
  getAllEmployeeAppraisalList() {
    this.showEmpAppraisalLoader();
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
    this.hrService
      .GetAllDetailsByOfficeId(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_GetAllEmployeeAppraisalDetails,
        officeId
      )
      .subscribe(
        data => {
          this.employeeAppraisalListDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeAppraisalDetailsModelLst != null
          ) {
            if (data.data.EmployeeAppraisalDetailsModelLst.length > 0) {
              data.data.EmployeeAppraisalDetailsModelLst.forEach(element => {
                this.employeeAppraisalListDataSource.push(element);
              });
            }
          } else {
            this.toastr.error('Something went wrong!');
          }
          this.hideEmpAppraisalLoader();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideEmpAppraisalLoader();
        }
      );
  }
  //#endregion

  //#region "Get All Appraisal Questions"
  getAllAppraisalQuestions() {
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
    this.codeService
      .GetAppraisalQuestions(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAppraisalQuestions,
        officeId
      )
      .subscribe(
        data => {
          this.questionSourceData = [];
          if (
            data.StatusCode === 200 &&
            data.data.AppraisalList.length > 0 &&
            data.data.AppraisalList != null
          ) {
            data.data.AppraisalList.forEach(element => {
              this.questionSourceData.push({
                AppraisalGeneralQuestionsId:
                  element.AppraisalGeneralQuestionsId,
                QuestionEnglish: element.Question,
                QuestionDari: element.DariQuestion,
                SequenceNo: element.SequenceNo,
                Remarks: element.Remarks,
                Score: element.Score
              });
            });
          } else {
            if (
              data.data.AppraisalList.length === 0 ||
              data.data.AppraisalList == null
            ) {
              this.toastr.warning('Appraisal Questions Not Found !');
            } else if (data.StatusCode === 400) {
              // failStatusCode
              this.toastr.error('Something went wrong !');
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

            // sort in Asc
            this.employeeListDataSource = this.commonService.sortDropdown(
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

  //#region "Get All Employee Details By EmployeeId"
  getEmployeeDetailByEmployeeId(employeeId: number) {
    this.hrService
      .GetEmployeeDataByEmployeeId(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_GetEmployeeDetailByEmployeeId,
        employeeId
      )
      .subscribe(
        data => {
          this.employeeDetailListData = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeDetailListData != null &&
            data.data.EmployeeDetailListData.length > 0
          ) {
            data.data.EmployeeDetailListData.forEach(element => {
              this.employeeDetailListData.push(element);

              this.employeeAppraisalQuestionForm = {
                EmployeeAppraisalDetailsId: element.EmployeeAppraisalDetailsId,
                EmployeeId: element.EmployeeId,
                EmployeeCode: element.EmployeeCode,
                EmployeeName: element.EmployeeName,
                FatherName: element.FathersName,
                Position: element.Position,
                Department: element.Department,
                Qualification: element.Qualification,
                DutyStation: element.DutyStation,
                RecruitmentDate: element.RecruitmentDate,
                AppraisalPeriod: null,
                CurrentAppraisalDate: null,
                // tslint:disable-next-line:radix
                OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
                TotalScore: null,
                EmployeeAppraisalQuestionList: this.questionSourceData,
                AppraisalStatus: element.AppraisalStatus
              };
            });
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

  //#region "Get All Employee Contract Type"
  // getAllEmployeeContractType() {
  //   this.hrService
  //     .GetAllDetails(
  //       this.setting.getBaseUrl() + GLOBAL.API_Hr_GetEmployeeContractType
  //     )
  //     .subscribe(
  //       data => {
  //         this.appraisalPeriodDropdown = [];
  //         if (
  //           data.StatusCode === 200 &&
  //           data.data.EmployeeContractTypeList.length > 0 &&
  //           data.data.EmployeeContractTypeList != null
  //         ) {
  //           data.data.EmployeeContractTypeList.forEach(element => {
  //             this.appraisalPeriodDropdown.push({
  //               EmployeeContractTypeId: element.EmployeeContractTypeId,
  //               EmployeeContractTypeName: element.EmployeeContractTypeName
  //             });
  //           });
  //         } else {
  //           if (
  //             data.data.EmployeeContractTypeList.length === 0 ||
  //             data.data.EmployeeContractTypeList == null
  //           ) {
  //             this.toastr.warning('No Record Found !');
  //           } else if (data.StatusCode === 400) {
  //             // failStatusCode
  //             this.toastr.error('Something went wrong !');
  //           }
  //         }
  //       },
  //       error => {
  //         if (error.StatusCode === 500) {
  //           this.toastr.error('Internal Server Error....');
  //         } else if (error.StatusCode === 401) {
  //           this.toastr.error('Unauthorized Access Error....');
  //         } else if (error.StatusCode === 403) {
  //           this.toastr.error('Forbidden Error....');
  //         }
  //       }
  //     );
  // }
  //#endregion

  //#region "Get All Employee Appraisal More Details"
  getAllEmployeeAppraisalMoreDetails() {
    this.showEmpAppraisalLoader();
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
    this.hrService
      .GetAllDetail(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_GetAllEmployeeAppraisalMoreDetails,
        officeId
      )
      .subscribe(
        data => {
          this.employeeMoreDetailListDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeAppraisalDetailsModelLst != null
          ) {
            if (data.data.EmployeeAppraisalDetailsModelLst.length > 0) {
              data.data.EmployeeAppraisalDetailsModelLst.forEach(element => {
                this.employeeMoreDetailListDataSource.push(element);
              });
            }
          } else {
            this.toastr.error('Something went wrong !');
          }
          this.hideEmpAppraisalLoader();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideEmpAppraisalLoader();
        }
      );
  }
  //#endregion

  //#region "Add Employee Appraisal"
  addEmployeeAppraisal(appraisalData: EmployeeAppraisalModel) {
    this.showEmpAppraisalLoader();
    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddEmployeeAppraisalDetails,
        appraisalData
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Record Successfully Updated !');
          } else {
            // tslint:disable-next-line:curly
            if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');
          }
          this.hideEmpAppraisalLoader();
          this.getAllEmployeeAppraisalList();
          this.hideAddEmployeeAppraisal();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideEmpAppraisalLoader();
          this.getAllEmployeeAppraisalList();
          this.hideAddEmployeeAppraisal();
        }
      );
  }

  //#endregion

  //#region "Add Employee Appraisal More Details"
  addEmployeeAppraisalMoreDetails(appraisalData: any) {
    this.showEmpAppraisalLoader();
    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_AddEmployeeAppraisalMoreDetails,
        appraisalData
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Record Successfully Updated !');
          } else {
            // tslint:disable-next-line:curly
            if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');
          }
          this.hideEmpAppraisalLoader();
          this.getAllEmployeeAppraisalMoreDetails();
          this.hideAddAppraisalMoreDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideEmpAppraisalLoader();
          this.hideAddAppraisalMoreDetails();
        }
      );
  }

  //#endregion

  //#region "Edit Employee Appraisal"
  editEmployeeAppraisal(appraisalData: any) {
    this.showEmpAppraisalLoader();
    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_EditEmployeeAppraisalDetails,
        appraisalData
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Record Successfully Updated !');
            this.getAllEmployeeAppraisalList();
          } else {
            // tslint:disable-next-line:curly
            if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');
          }
          this.hideEmpAppraisalLoader();
          this.hideEditEmployeeAppraisal();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideEmpAppraisalLoader();
          this.hideEditEmployeeAppraisal();
        }
      );
  }

  //#endregion

  //#region "Edit Employee Appraisal More Details"
  editEmployeeAppraisalMoreDetails(appraisalData: any) {
    this.showEmpAppraisalLoader();
    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_EditEmployeeAppraisalMoreDetails,
        appraisalData
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Record Successfully Updated !');
          } else {
            // tslint:disable-next-line:curly
            if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');
          }
          this.hideEmpAppraisalLoader();
          this.getAllEmployeeAppraisalMoreDetails();
          this.hideEditAppraisalMoreDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideEmpAppraisalLoader();
          this.hideEditAppraisalMoreDetails();
        }
      );
  }

  //#endregion

  //#region "ApprovalAndRejectionEmployeeAppraisal"
  approvalOrRejectEmployeeAppraisal(
    employeeAppraisalDetailsId: any,
    approveRejectFlag: boolean
  ) {
    this.showEmpAppraisalLoader();
    if (approveRejectFlag === true) {
      this.hrService
        .ApprovalOrRejectEmployeeAppraisal(
          this.setting.getBaseUrl() +
            GLOBAL.API_Code_ApproveEmployeeAppraisalRequest,
          employeeAppraisalDetailsId
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Approval Successfully !');
            } else {
              // tslint:disable-next-line:curly
              if (data.StatusCode === 400) this.toastr.error(data.Message);
            }
            this.hideEmpAppraisalLoader();
            this.getAllEmployeeAppraisalList();
            this.getAllEmployeeAppraisalMoreDetails();
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
            }
            this.hideEmpAppraisalLoader();
            this.getAllEmployeeAppraisalList();
            this.getAllEmployeeAppraisalMoreDetails();
          }
        );
    } else {
      this.hrService
        .ApprovalOrRejectEmployeeAppraisal(
          this.setting.getBaseUrl() +
            GLOBAL.API_Code_RejectEmployeeAppraisalRequest,
          employeeAppraisalDetailsId
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Rejected Successfully !');
            } else {
              // tslint:disable-next-line:curly
              if (data.StatusCode === 400) this.toastr.error(data.Message);
            }
            this.hideEmpAppraisalLoader();
            this.getAllEmployeeAppraisalList();
            this.getAllEmployeeAppraisalMoreDetails();
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
            }
            this.hideEmpAppraisalLoader();
            this.getAllEmployeeAppraisalList();
            this.getAllEmployeeAppraisalMoreDetails();
          }
        );
    }
  }

  //#endregion

  //#region "ApprovalAndRejectionEmployeeEvaluation"
  approvalAndRejectionEmployeeEvaluation(
    employeeEvaluationId: number,
    approvalFlag: boolean
  ) {
    this.showEmpAppraisalLoader();
    if (approvalFlag === true) {
      // Approve: True
      this.hrService
        .ApprovalAndRejectionEmployeeEvaluation(
          this.setting.getBaseUrl() +
            GLOBAL.API_Code_ApproveEmployeeEvaluationRequest,
          employeeEvaluationId
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Approval Successfully !');
            } else {
              // tslint:disable-next-line:curly
              if (data.StatusCode === 400)
                this.toastr.error('Something went wrong!');
            }
            this.hideEmpAppraisalLoader();
            this.getAllEmployeeAppraisalMoreDetails();
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
            }
            this.hideEmpAppraisalLoader();
            this.getAllEmployeeAppraisalMoreDetails();
          }
        );
    } else {
      // Reject: False
      this.showEmpAppraisalLoader();
      this.hrService
        .ApprovalAndRejectionEmployeeEvaluation(
          this.setting.getBaseUrl() +
            GLOBAL.API_Code_RejectEmployeeEvaluationRequest,
          employeeEvaluationId
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Rejection Successfully !');
            } else {
              // tslint:disable-next-line:curly
              if (data.StatusCode === 400)
                this.toastr.error('Something went wrong!');
            }
            this.hideEmpAppraisalLoader();
            this.getAllEmployeeAppraisalMoreDetails();
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
            }
            this.hideEmpAppraisalLoader();
            this.getAllEmployeeAppraisalMoreDetails();
          }
        );
    }
  }

  //#endregion

  //#region "On Add Form Submit"
  onAddFormSubmit(data: EmployeeAppraisalModel) {
    const appraisalData: EmployeeAppraisalModel = {
      EmployeeAppraisalDetailsId: 0,
      EmployeeId: data.EmployeeId,
      EmployeeCode: data.EmployeeCode,
      EmployeeName: data.EmployeeName,
      FatherName: data.FatherName,
      Position: data.Position,
      Department: data.Department,
      Qualification: data.Qualification,
      DutyStation: data.DutyStation,
      RecruitmentDate: new Date(
        new Date(data.RecruitmentDate).getFullYear(),
        new Date(data.RecruitmentDate).getMonth(),
        new Date(data.RecruitmentDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      AppraisalPeriod: data.AppraisalPeriod, // data.AppraisalPeriod,
      CurrentAppraisalDate: new Date(
        new Date(data.CurrentAppraisalDate).getFullYear(),
        new Date(data.CurrentAppraisalDate).getMonth(),
        new Date(data.CurrentAppraisalDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
      TotalScore: data.TotalScore,
      EmployeeAppraisalQuestionList: this.questionSourceData,
      AppraisalStatus: false
    };

    appraisalData.TotalScore = 0;
    this.questionSourceData.forEach(element => {
      appraisalData.TotalScore =
        appraisalData.TotalScore +
        (element.Score === undefined || element.Score == null
          ? 0
          : element.Score);
    });

    if (this.employeeSelectedFlag) {
      this.addEmployeeAppraisal(appraisalData);
    } else {
      this.toastr.warning('Please Select Employee !');
    }
  }
  //#endregion

  //#region "On Edit Form Submit"
  onEditFormSubmit(data: any) {
    const appraisalData: EmployeeAppraisalModel = {
      EmployeeAppraisalDetailsId: data.EmployeeAppraisalDetailsId,
      EmployeeId: data.EmployeeId,
      EmployeeCode: data.EmployeeCode,
      EmployeeName: data.EmployeeName,
      FatherName: data.FatherName,
      Position: data.Position,
      Department: data.Department,
      Qualification: data.Qualification,
      DutyStation: data.DutyStation,
      RecruitmentDate: new Date(
        new Date(data.RecruitmentDate).getFullYear(),
        new Date(data.RecruitmentDate).getMonth(),
        new Date(data.RecruitmentDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      AppraisalPeriod: data.AppraisalPeriod,
      CurrentAppraisalDate: new Date(
        new Date(data.CurrentAppraisalDate).getFullYear(),
        new Date(data.CurrentAppraisalDate).getMonth(),
        new Date(data.CurrentAppraisalDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
      TotalScore: data.TotalScore,
      EmployeeAppraisalQuestionList: this.questionSourceData,
      AppraisalStatus: data.AppraisalStatus
    };

    appraisalData.TotalScore = 0;

    this.questionSourceData.forEach(element => {
      appraisalData.TotalScore =
        appraisalData.TotalScore +
        (element.Score === undefined || element.Score == null
          ? 0
          : element.Score);
    });

    this.editEmployeeAppraisal(appraisalData);
  }
  //#endregion

  //#region "onEmployeeSelectedValue"
  onEmployeeSelectedValue(data: any) {
    if (data != null && (data.value !== 0 && data.value != null)) {
      this.employeeSelectedFlag = true;
      this.getEmployeeDetailByEmployeeId(data.value);
    } else {
      // for init
      this.employeeSelectedFlag = false;
      this.initEmployeeAppraisalQuestionForm();
    }
  }
  //#endregion

  //#region "onEmployeeMoreDetailSelectedValue"
  onEmployeeMoreDetailSelectedValue(data: any) {
    this.onEmployeeMoreDetailValue = data.value;
  }
  //#endregion

  //#region "on Add Appraisal More Detail Form Submit"
  onAddAppraisalMoreDetailFormSubmit(data: EmployeeAppraisalMoreDetailsModel) {
    const dataModel: EmployeeAppraisalMoreDetailsModel = {
      EmployeeEvaluationId: 0,
      EmployeeAppraisalDetailsId: data.EmployeeAppraisalDetailsId,
      EmployeeEvaluationModelList: this.employeeEvaluationDataSource,

      StrongPoints: this.strongPointsSourceData,
      WeakPoints: this.weakPointsSourceData,

      CurrentAppraisalDate: data.CurrentAppraisalDate,

      FinalResultQues1: data.FinalResultQues1,
      FinalResultQues2: data.FinalResultQues2,
      FinalResultQues3: data.FinalResultQues3,
      FinalResultQues4: data.FinalResultQues4,
      FinalResultQues5: data.FinalResultQues5,

      DirectSupervisor: data.DirectSupervisor,
      EmployeeAppraisalTeamMemberList: this.appraisalTeamMemberDataSource,

      CommentsByEmployee: data.CommentsByEmployee,
      EmployeeId: this.onEmployeeMoreDetailValue,
      EvaluationStatus: null,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
    };
    this.addEmployeeAppraisalMoreDetails(dataModel);
  }
  //#endregion

  //#region "on Edit Appraisal More Detail Form Submit"
  onEditAppraisalMoreDetailFormSubmit(data: EmployeeAppraisalMoreDetailsModel) {
    const strongPt = [],
      weakPt = [],
      teamMember = [];

    this.strongPointsSourceData.forEach(element => {
      strongPt.push(element.StrongPoints);
    });

    this.weakPointsSourceData.forEach(element => {
      weakPt.push(element.WeakPoints);
    });

    this.appraisalTeamMemberDataSource.forEach(element => {
      teamMember.push(element.EmployeeId);
    });

    const dataModel: EmployeeAppraisalMoreDetailsModel = {
      EmployeeEvaluationId: data.EmployeeEvaluationId,
      EmployeeAppraisalDetailsId: data.EmployeeAppraisalDetailsId,

      EmployeeEvaluationModelList: this.employeeEvaluationDataSource,

      StrongPoints: strongPt,
      WeakPoints: weakPt,
      CurrentAppraisalDate: data.CurrentAppraisalDate,

      FinalResultQues1: data.FinalResultQues1,
      FinalResultQues2: data.FinalResultQues2,
      FinalResultQues3: data.FinalResultQues3,
      FinalResultQues4: data.FinalResultQues4,
      FinalResultQues5: data.FinalResultQues5,

      DirectSupervisor: data.DirectSupervisor,
      EmployeeAppraisalTeamMemberList: teamMember,

      CommentsByEmployee: data.CommentsByEmployee,
      EmployeeId: data.EmployeeId,
      EvaluationStatus: data.EvaluationStatus,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
    };

    this.editEmployeeAppraisalMoreDetails(dataModel);
  }
  //#endregion

  //#region "Events"

  onEditEmpAppraisalDetailShowForm(data: any, questionUpdateFlag: any) {
    if (data != null) {
      this.employeeAppraisalQuestionForm = {
        EmployeeAppraisalDetailsId: data.EmployeeAppraisalDetailsId,
        EmployeeId: data.EmployeeId,
        EmployeeCode: data.EmployeeCode,
        EmployeeName: data.EmployeeName,
        FatherName: data.FatherName,
        Position: data.Position,
        Department: data.Department,
        Qualification: data.Qualification,
        DutyStation: data.DutyStation,
        RecruitmentDate: data.RecruitmentDate,
        AppraisalPeriod: data.AppraisalPeriod,
        CurrentAppraisalDate: data.CurrentAppraisalDate,
        OfficeId: data.OfficeId,
        TotalScore: data.TotalScore,
        EmployeeAppraisalQuestionList: data.EmployeeAppraisalQuestionList,
        AppraisalStatus: data.AppraisalStatus
      };

      this.questionSourceData = data.EmployeeAppraisalQuestionList;

      // tslint:disable-next-line:curly
      if (questionUpdateFlag === 1) this.updatingFlag = true;
      // tslint:disable-next-line:curly
      else this.updatingFlag = false;
      this.showEditEmployeeAppraisal();
      this.empAppraisalLoader = false;
    }
  }

  // For updating Flag in dx grid while updating the rows
  onRowPreparedEvent(event) {
    event.editorOptions.disabled = !this.updatingFlag;
  }

  getEmployeeAppraisalRequest(data) {
    this.empAppraisalLoader = true;
    const setUpdateFlag = 0; // 1 for enable 0 for disable
    this.hrService
      .GetAllDetailsByEmployeeIdAndDate(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_GetAllEmployeeAppraisalDetailsByEmployeeId,
        data.EmployeeId,
        data.CurrentAppraisalDate
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeAppraisalDetailsModel != null
          ) {
            this.onEditEmpAppraisalDetailShowForm(
              data.data.EmployeeAppraisalDetailsModel,
              setUpdateFlag
            );
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
          this.hideEmpAppraisalLoader();
        }
      );
  }

  // tslint:disable-next-line:member-ordering
  currentAppraisalStatus: boolean;
  onEditEmpAppraisalMoreDetailShowForm(data: any) {
    if (data != null) {
      const keyValue = data.key;

      this.currentAppraisalStatus =
        keyValue.EvaluationStatus == null ? true : false;
      this.employeeEvaluationDataSource = [];

      this.strongPointsSourceData = [];
      this.weakPointsSourceData = [];

      this.appraisalTeamMemberDataSource = [];

      keyValue.EmployeeEvaluationModelList != null
        ? keyValue.EmployeeEvaluationModelList.forEach(element => {
            this.employeeEvaluationDataSource.push(element);
          })
        // tslint:disable-next-line:no-unused-expression
        : null;

      keyValue.StrongPoints != null
        ? keyValue.StrongPoints.forEach(element => {
            this.strongPointsSourceData.push({
              StrongPoints: element
            });
          })
        // tslint:disable-next-line:no-unused-expression
        : null;

      keyValue.WeakPoints != null
        ? keyValue.WeakPoints.forEach(element => {
            this.weakPointsSourceData.push({
              WeakPoints: element
            });
          })
        // tslint:disable-next-line:no-unused-expression
        : null;

      keyValue.EmployeeAppraisalTeamMemberList != null
        ? keyValue.EmployeeAppraisalTeamMemberList.forEach(element => {
            this.appraisalTeamMemberDataSource.push({
              EmployeeId: element
            });
          })
        // tslint:disable-next-line:no-unused-expression
        : null;

      this.pointsOfEmployeesForm = {
        EmployeeEvaluationId: keyValue.EmployeeEvaluationId,
        EmployeeAppraisalDetailsId: keyValue.EmployeeAppraisalDetailsId,

        EmployeeEvaluationModelList: null,

        StrongPoints: null,
        WeakPoints: null,

        CurrentAppraisalDate: keyValue.CurrentAppraisalDate,

        FinalResultQues1:
          keyValue.FinalResultQues1 != null ? keyValue.FinalResultQues1 : '',
        FinalResultQues2:
          keyValue.FinalResultQues2 != null ? keyValue.FinalResultQues2 : '',
        FinalResultQues3:
          keyValue.FinalResultQues3 != null ? keyValue.FinalResultQues3 : '',
        FinalResultQues4:
          keyValue.FinalResultQues4 != null ? keyValue.FinalResultQues4 : '',
        FinalResultQues5:
          keyValue.FinalResultQues5 != null ? keyValue.FinalResultQues5 : '',

        DirectSupervisor:
          keyValue.DirectSupervisor != null ? keyValue.DirectSupervisor : '',
        EmployeeAppraisalTeamMemberList: null,

        CommentsByEmployee:
          keyValue.CommentsByEmployee != null
            ? keyValue.CommentsByEmployee
            : '',

        EvaluationStatus: keyValue.EvaluationStatus,

        EmployeeId: keyValue.EmployeeId,
        // tslint:disable-next-line:radix
        OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      };
      this.showEditAppraisalMoreDetails();
    }
  }

  onIsApprovedValueChanged(cellData: any, e: any) {
    // Appraisal Approval
    if (e != null && cellData != null) {
      if (cellData.key.EmployeeAppraisalDetailsId !== 0) {
        this.showAppraisalApprovalConfirmation();
        this.approveRejectEmployeeAppraisalDetailsId =
          cellData.key.EmployeeAppraisalDetailsId;
        e.value === 'Approve'
          ? (this.currentAppraisalApproveReject = true)
          : (this.currentAppraisalApproveReject = false);
      }
    }
  }
  onApproveAppraisalConfirmClick() {
    this.approvalOrRejectEmployeeAppraisal(
      this.approveRejectEmployeeAppraisalDetailsId,
      this.currentAppraisalApproveReject === true
    );
    this.hideAppraisalApprovalConfirmation();
  }

  // Evaluate Approval
  onIsEvaluationApprovedValueChanged(cellData: any, e: any) {
    this.showEvaluateApprovalConfirmation();
    if (e != null && cellData != null) {
      if (cellData.key.EmployeeEvaluationId !== 0) {
        this.approveRejectEmployeeEvaluationId =
          cellData.key.EmployeeEvaluationId;
        e.value.toLowerCase() === 'approve'
          ? (this.currentEvaluationApproveReject = true)
          : (this.currentEvaluationApproveReject = false);
      }
    }
  }
  onApprovalEvaluationConfirmClick() {
    this.currentEvaluationApproveReject === true
      ? this.approvalAndRejectionEmployeeEvaluation(
          this.approveRejectEmployeeEvaluationId,
          true
        )
      : this.approvalAndRejectionEmployeeEvaluation(
          this.approveRejectEmployeeEvaluationId,
          false
        );
    this.hideEvaluateApprovalConfirmation();
  }

  onBackButtonClick() {
    this.backButtonFlag = false;
    if (this.addAppraisalDetailsFlag === true) {
      this.addAppraisalDetailsFlag = false;
      this.employeeAppraisalListFlag = true;
    } else if (this.editAppraisalDetailsFlag === true) {
      this.editAppraisalDetailsFlag = false;
      this.employeeAppraisalListFlag = true;
    } else if (this.addAppraisalMoreDetailsFlag === true) {
      this.addAppraisalMoreDetailsFlag = false;
      this.employeeAppraisalListFlag = true;
    } else if (this.editAppraisalMoreDetailsFlag === true) {
      this.editAppraisalMoreDetailsFlag = false;
      this.employeeAppraisalListFlag = true;
    }
  }

  // Evaluation Add
  onEvaluationAdding(data: any) {
    // this.employeeEvaluationDataSource.push(data.data);
  }

  onEvaluationUpdating(data: any) {}

  onEvaluationRemoving(data: any) {}

  //#endregion

  //#region "Show / hide"
  showAddEmployeeAppraisalForm() {
    this.initializeForm();
    this.getAllRemarks();
    this.getAllAppraisalQuestions();

    // more details
    this.employeeEvaluationDataSource = [];
    this.strongPointsSourceData = [];
    this.weakPointsSourceData = [];

    this.showAddEmployeeAppraisal();
  }

  // appraisal More Detail Form
  showAddEmployeeAppraisalMoreDetailForm() {
    this.initializeForm();
    // more details
    this.strongPointsSourceData = [];
    this.weakPointsSourceData = [];

    this.showAddAppraisalMoreDetails();
  }

  showAddEmployeeAppraisal() {
    this.backButtonFlag = true;

    this.employeeAppraisalListFlag = false; // main grid
    this.addAppraisalDetailsFlag = true;
  }

  hideAddEmployeeAppraisal() {
    this.backButtonFlag = false;

    this.employeeAppraisalListFlag = true; // main grid
    this.addAppraisalDetailsFlag = false;
  }

  showEditEmployeeAppraisal() {
    this.backButtonFlag = true;

    this.employeeAppraisalListFlag = false; // main grid
    this.editAppraisalDetailsFlag = true;
  }

  hideEditEmployeeAppraisal() {
    this.backButtonFlag = false;

    this.employeeAppraisalListFlag = true; // main grid
    this.editAppraisalDetailsFlag = false;
  }

  // More Details
  showAddAppraisalMoreDetails() {
    this.backButtonFlag = true;

    this.employeeAppraisalListFlag = false; // main grid
    this.addAppraisalMoreDetailsFlag = true;
  }

  hideAddAppraisalMoreDetails() {
    this.backButtonFlag = false;

    this.employeeAppraisalListFlag = true; // main grid
    this.addAppraisalMoreDetailsFlag = false;
  }

  showEditAppraisalMoreDetails() {
    this.backButtonFlag = true;

    this.employeeAppraisalListFlag = false; // main grid
    this.editAppraisalMoreDetailsFlag = true;
  }

  hideEditAppraisalMoreDetails() {
    this.backButtonFlag = false;

    this.employeeAppraisalListFlag = true; // main grid
    this.editAppraisalMoreDetailsFlag = false;
  }

  // Appraisal Approve
  showAppraisalApprovalConfirmation() {
    this.popupAppraisalFormConfVisible = true;
  }
  hideAppraisalApprovalConfirmation() {
    this.popupAppraisalFormConfVisible = false;
  }

  // Evaluate Approve
  showEvaluateApprovalConfirmation() {
    this.popupEvaluationFormConfVisible = true;
  }
  hideEvaluateApprovalConfirmation() {
    this.popupEvaluationFormConfVisible = false;
  }

  // Loader
  showEmpAppraisalLoader() {
    this.empAppraisalLoader = true;
  }

  hideEmpAppraisalLoader() {
    this.empAppraisalLoader = false;
  }

  //#endregion
}

//#region "Classes"

class EmployeeEvaluationDetailsMode {
  EmployeeId: number;
  EmployeeName: string;
  EmployeeAppraisalModel: EmployeeAppraisalMoreDetailsModel;
}

class EmployeeAppraisalModel {
  EmployeeAppraisalDetailsId: number;
  EmployeeId: number;
  EmployeeCode: any;
  EmployeeName: any;
  FatherName: any;
  Position: any;
  Department: any;
  Qualification: any;
  DutyStation: any;
  RecruitmentDate: any;
  AppraisalPeriod: any;
  CurrentAppraisalDate: any;
  OfficeId: any;
  TotalScore: number;
  EmployeeAppraisalQuestionList: EmployeeAppraisalQuestionList[];
  AppraisalStatus: any;
}

class EmployeeAppraisalQuestionList {
  EmployeeAppraisalQuestionsId?: number;
  QuestionEnglish: any;
  QuestionDari: any;
  AppraisalGeneralQuestionsId: any;
  SequenceNo: any;
  Score: any;
  Remarks: any;
}

class RemarkModel {
  RemarkId: number;
  RemarkName: string;
}

class EmployeeListModel {
  EmployeeId: any;
  EmployeeName: any;
  EmployeeCode: any;
  CodeEmployeeName: string;
}

class EmployeeDetailListModel {
  EmployeeId: any;
  EmployeeName: any;
  EmployeeCode: any;
  FathersName: any;
  Position: any;
  Department: any;
  Qualification: any;
  DutyStation: any;
  RecruitmentDate: any;
}

class StrongPoints {
  StrongPointsId: number;
  StrongPointsDesc: string;
}
class WeakPoints {
  WeakPointsId: number;
  WeakPointsDesc: string;
}

class EmployeeAppraisalMoreDetailsModel {
  EmployeeEvaluationId: number;
  EmployeeAppraisalDetailsId: number;

  EmployeeEvaluationModelList: EmployeeEvaluationModel[];

  StrongPoints: string[];
  WeakPoints: string[];

  FinalResultQues1: string;
  FinalResultQues2: string;
  FinalResultQues3: string;
  FinalResultQues4: string;
  FinalResultQues5: string;

  DirectSupervisor: any;

  EmployeeAppraisalTeamMemberList: any[];

  CommentsByEmployee: string;

  CurrentAppraisalDate: any;

  EmployeeId: number;
  OfficeId: number;
  EvaluationStatus: any;
}

class EmployeeEvaluationModel {
  TrainingProgram: any;
  Program: any;
  Participated: any;
  CatchLevel: any;
  RefresherTrm: any;
  OthRecommendation: any;
}
