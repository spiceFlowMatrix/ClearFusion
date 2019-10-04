import { Component, OnInit } from '@angular/core';
import { CodeService } from '../code.service';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';

@Component({
  selector: 'app-interview-rating-questions',
  templateUrl: './interview-rating-questions.component.html',
  styleUrls: ['./interview-rating-questions.component.css']
})
export class InterviewRatingQuestionsComponent implements OnInit {
  // form
  ratingBasedCriteriaQuestionsDataSource: IRatingBasedCriteriaQuestionModel[];
  ratingBasedCriteriaQuestionForm: IRatingBasedCriteriaQuestionModel;

  // popup
  addRatingBasedCriteriaQuestionsPopupVisible = false;
  editRatingBasedCriteriaQuestionsPopupVisible = false;
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
    this.getAllRatingBasedCriteriaQuestions();
  }

  allFormInitialize() {
    this.ratingBasedCriteriaQuestionForm = {
      QuestionsId: null,
      Question: null,
      OfficeId: null
    };
  }

  onShowHideRatingBasedCriteriaQuestionsAddPopup() {
    this.addRatingBasedCriteriaQuestionsPopupVisible = !this
      .addRatingBasedCriteriaQuestionsPopupVisible;

    if (this.addRatingBasedCriteriaQuestionsPopupVisible) {
      this.allFormInitialize();
    }
  }

  //#region "on Criteria Based Questions Form Submit"
  onRatingBasedCriteriaQuestionsFormSubmit(model: IRatingBasedCriteriaQuestionModel) {
    if (model.QuestionsId == null || model.QuestionsId === 0) {
      this.AddRatingBasedCriteriaQuestions(model);
    } else {
       this.EditRatingBasedCriteriaQuestions(model);
    }
  }
  //#endregion

  //#region "Add Interview Questions"
  AddRatingBasedCriteriaQuestions(model: IRatingBasedCriteriaQuestionModel) {
    const questionModel: IRatingBasedCriteriaQuestionModel = {
      QuestionsId: 0,
      Question: model.Question,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
    };

    this.addRatingBasedCriteriaQuestionsPopupVisible = false;
    this.loading = true;
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddRatingBasedCriteriaQuestion,
        questionModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success(
              'Rating Based Criteria Question Added Successfully !'
            );
            this.getAllRatingBasedCriteriaQuestions();
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

    //#region "Get Rating Based Criteria Questions"
    getAllRatingBasedCriteriaQuestions() {
      // tslint:disable-next-line:radix
      const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
      this.codeService
        .GetAppraisalQuestions(
          this.setting.getBaseUrl() + GLOBAL.API_Code_GetRatingBasedCriteriaQuestions,
          officeId
        )
        .subscribe(
          data => {
            this.ratingBasedCriteriaQuestionsDataSource = [];
            if (
              data.StatusCode === 200 &&
              data.data.RatingBasedCriteriaQuestionList.length > 0 &&
              data.data.RatingBasedCriteriaQuestionList != null
            ) {
              data.data.RatingBasedCriteriaQuestionList.forEach(element => {
                this.ratingBasedCriteriaQuestionsDataSource.push(element);
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

     //#region "on Rating Based Criteria Questions Edit Click"
  onRatingBasedCriteriaQuestionsEditClick(model: IRatingBasedCriteriaQuestionModel) {
    this.ratingBasedCriteriaQuestionForm = {
      QuestionsId: model.QuestionsId,
      Question: model.Question,
      OfficeId: model.OfficeId
    };
    this.onShowHideRatingBasedCriteriaQuestionsEditPopup();
  }
  //#endregion
  onShowHideRatingBasedCriteriaQuestionsEditPopup() {
    this.editRatingBasedCriteriaQuestionsPopupVisible = !this
      .editRatingBasedCriteriaQuestionsPopupVisible;
  }

  //#region "Edit Rating Based Criteria Questions"
  EditRatingBasedCriteriaQuestions(model: IRatingBasedCriteriaQuestionModel) {
    this.editRatingBasedCriteriaQuestionsPopupVisible = false;
    this.loading = true;
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditRatingBasedCriteriaQuestion,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Rating Based Criteria Question Updated Successfully!!!');
            this.getAllRatingBasedCriteriaQuestions();
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

interface IRatingBasedCriteriaQuestionModel {
  QuestionsId: number;
  Question: string;
  OfficeId: number;
}
