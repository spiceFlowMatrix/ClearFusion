import { Component, OnInit } from '@angular/core';
import { CodeService } from '../code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-technical-interview-questions',
  templateUrl: './technical-interview-questions.component.html',
  styleUrls: ['./technical-interview-questions.component.css']
})
export class TechnicalInterviewQuestionsComponent implements OnInit {
  //#region
  interviewQuestionsDataSource: InterviewQuestionModel[];
  interviewQuestionsForm: InterviewQuestionModel;

  // popup
  addInterviewQuestionsPopupVisible = false;
  editInterviewQuestionsPopupVisible = false;
  isEditingAllowed = false;

  // loader
  loading = false;

  constructor(
    private codeService: CodeService,
    private commonService: CommonService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {
    this.allFormInitialize();
  }

  ngOnInit() {
    this.getAllInterviewQuestions();
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.TechnicalQuestions
    );
  }

  allFormInitialize() {
    this.interviewQuestionsForm = {
      InterviewTechnicalQuestionsId: null,
      Question: null,
      OfficeId: null
    };
  }

  //#region "Get All Interview Questions"
  getAllInterviewQuestions() {
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
    this.codeService
      .GetAppraisalQuestions(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetInterviewQuestions,
        officeId
      )
      .subscribe(
        data => {
          this.interviewQuestionsDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.InterviewTechnicalQuestionsList.length > 0 &&
            data.data.InterviewTechnicalQuestionsList != null
          ) {
            data.data.InterviewTechnicalQuestionsList.forEach(element => {
              this.interviewQuestionsDataSource.push(element);
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

  //#region "Hide / Show popups"
  onShowHideAppraisalQuestionsAddPopup() {
    this.addInterviewQuestionsPopupVisible = !this
      .addInterviewQuestionsPopupVisible;

    if (this.addInterviewQuestionsPopupVisible) {
      this.allFormInitialize();
    }
  }

  onShowHideAppraisalQuestionsEditPopup() {
    this.editInterviewQuestionsPopupVisible = !this
      .editInterviewQuestionsPopupVisible;
  }

  //#endregion

  //#region "on Interview Questins Edit Click"
  onInterviewQuestinsEditClick(model: InterviewQuestionModel) {
    this.interviewQuestionsForm = {
      InterviewTechnicalQuestionsId: model.InterviewTechnicalQuestionsId,
      Question: model.Question,
      OfficeId: model.OfficeId
    };
    this.onShowHideAppraisalQuestionsEditPopup();
  }
  //#endregion

  //#region "on Appraisal Questions Form Submit"
  onInterviewQuestionsFormSubmit(model: InterviewQuestionModel) {
    if (
      model.InterviewTechnicalQuestionsId == null ||
      model.InterviewTechnicalQuestionsId === 0
    ) {
      this.AddInterviewQuestions(model);
    } else {
      this.EditInterviewQuestions(model);
    }
  }
  //#endregion

  //#region "Add Interview Questions"
  AddInterviewQuestions(model: InterviewQuestionModel) {
    const questionModel: InterviewQuestionModel = {
      InterviewTechnicalQuestionsId: 0,
      Question: model.Question,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
    };

    this.addInterviewQuestionsPopupVisible = false;
    this.loading = true;
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddInterviewQuestion,
        questionModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success(
              'Interview Technical Question Added Successfully !'
            );
            this.getAllInterviewQuestions();
          } else {
            if (data.StatusCode === 400) {
              this.toastr.error('Something went wrong !');
            }
          }
          this.loading = false;
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

  //#region "Edit Appraisal Questions"
  EditInterviewQuestions(model: InterviewQuestionModel) {
    this.editInterviewQuestionsPopupVisible = false;
    this.loading = true;
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditInterviewQuestion,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Technical Interview Updated Successfully!!!');
            this.getAllInterviewQuestions();
          }
          this.loading = false;
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
}

interface InterviewQuestionModel {
  InterviewTechnicalQuestionsId: number;
  Question: string;
  OfficeId: number;
}
