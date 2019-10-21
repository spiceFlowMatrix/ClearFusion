import { Component, OnInit } from '@angular/core';
import { CodeService } from '../code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { ThirdPartyKey } from '../../../shared/thirdPartyKey';
import { CommonService, GoogleObj } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-appraisal-questions',
  templateUrl: './appraisal-questions.component.html',
  styleUrls: ['./appraisal-questions.component.css']
})
export class AppraisalQuestionsComponent implements OnInit {
  //#region
  appraisalQuestionsDataSource: appraisalQuestionsModel[];
  appraisalQuestionsForm: appraisalQuestionsModel;
  isEditingAllowed = false;
  // officecodelist: any[]= [];
  // selectedOffice: any;

  //#region "Google Translator"
  public googleObj: GoogleObj = new GoogleObj();
  googleKey: string;

  // popup
  addAppraisalQuestionsPopupVisible = false;
  editAppraisalQuestionsPopupVisible = false;

  // Loader
  addAppraisalQuestionsPopupLoading = false;
  editAppraisalQuestionsPopupLoading = false;

  //#endregion

  constructor(
    private codeService: CodeService,
    private commonService: CommonService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonservice: CommonService
  ) {
    // Form Initialize
    this.allFormInitialize();
    this.googleKey = ThirdPartyKey.googleKey;
  }

  ngOnInit() {
    // this.getOfficeCodeList();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.AppraisalQuestions
    );

    this.getAllAppraisalQuestions();
  }

  allFormInitialize() {
    this.appraisalQuestionsForm = {
      AppraisalGeneralQuestionsId: null,
      SequenceNo: null,
      Question: null,
      DariQuestion: null,
     // OfficeId: null
    };
  }

  //#region "Get All Appraisal Questions"
  getAllAppraisalQuestions() {
    // tslint:disable-next-line:radix
    this.codeService
      .GetAppraisalQuestions(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAppraisalQuestions
      )
      .subscribe(
        data => {
          this.appraisalQuestionsDataSource = [];

          if (
            data.StatusCode === 200 &&
            data.data.AppraisalList.length > 0 &&
            data.data.AppraisalList != null
          ) {
            data.data.AppraisalList.forEach(element => {
              this.appraisalQuestionsDataSource.push(element);
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

  //#region "Add Appraisal Questions"
  AddAppraisalQuestions(model: appraisalQuestionsModel) {
    const questionModel: appraisalQuestionsModel = {
      AppraisalGeneralQuestionsId: 0,
      SequenceNo: model.SequenceNo,
      Question: model.Question,
      DariQuestion: model.DariQuestion,
      // // tslint:disable-next-line:radix
      // OfficeId: parseInt(localStorage.getItem('OFFICEID'))
    };

    this.addAppraisalQuestionsPopupLoading = true;

    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddAppraisalQuestion,
        questionModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Appraisal Question Added Successfully !');
            this.getAllAppraisalQuestions();
          } else {
            if (data.StatusCode === 400) {
              this.toastr.error('Something went wrong !');
            }
          }
          this.addAppraisalQuestionsPopupLoading = false;
          this.onShowHideAppraisalQuestionsAddPopup();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.addAppraisalQuestionsPopupLoading = false;
          this.onShowHideAppraisalQuestionsAddPopup();
        }
      );
  }
  //#endregion

  //#region "Edit Appraisal Questions"
  EditAppraisalQuestions(model: appraisalQuestionsModel) {
    this.editAppraisalQuestionsPopupLoading = true;
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditAppraisalQuestion,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Exchange Rate Updated Successfully!!!');
            this.getAllAppraisalQuestions();
          }
          this.editAppraisalQuestionsPopupLoading = false;
          this.onShowHideAppraisalQuestionsEditPopup();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.editAppraisalQuestionsPopupLoading = false;
          this.onShowHideAppraisalQuestionsEditPopup();
        }
      );
  }

  //#endregion

  //#region "on Appraisal Questins Edit Click"
  onAppraisalQuestinsEditClick(model: appraisalQuestionsModel) {
    this.appraisalQuestionsForm = {
      AppraisalGeneralQuestionsId: model.AppraisalGeneralQuestionsId,
      SequenceNo: model.SequenceNo,
      Question: model.Question,
      DariQuestion: model.DariQuestion,
     // OfficeId: model.OfficeId
    };
    this.onShowHideAppraisalQuestionsEditPopup();
  }
  //#endregion

  //#region "on Appraisal Questions Form Submit"
  onAppraisalQuestionsFormSubmit(model: appraisalQuestionsModel) {
    if (
      model.AppraisalGeneralQuestionsId == null ||
      model.AppraisalGeneralQuestionsId === 0
    ) {
      this.AddAppraisalQuestions(model);
    } else {
      this.EditAppraisalQuestions(model);
    }
  }
  //#endregion

  //#region "Events"

  onFieldDataChanged(event: any) {
    const formData = this.appraisalQuestionsForm;

    // Add
    if (formData.AppraisalGeneralQuestionsId == null) {
      if (event.value != null && event.dataField === 'Question') {
        this.googleTranslate(event.value, 'add');
      } else if (event.value != null && event.dataField === 'SequenceNo') {
        const sequenceNoTrack = this.appraisalQuestionsDataSource.filter(
          x => x.SequenceNo === event.value
        );
        sequenceNoTrack.length > 0
          ? this.toastr.warning('Sequence No. already assigned')
          // tslint:disable-next-line:no-unused-expression
          : null;
      }
    }

    // Edit
    // tslint:disable-next-line:one-line
    else {
      if (event.value != null && event.dataField === 'Question') {
        this.googleTranslate(event.value, 'edit');
      } else if (event.value != null && event.dataField === 'SequenceNo') {
        if (formData.SequenceNo !== event.value) {
          const sequenceNoTrack = this.appraisalQuestionsDataSource.filter(
            x => x.SequenceNo === event.value
          );
          sequenceNoTrack.length > 0
            ? this.toastr.warning('Sequence No. already assigned')
            // tslint:disable-next-line:no-unused-expression
            : null;
        }
      }
    }
  }

  //#endregion

  //#region "Translate Function"
  googleTranslate(inputText, actionName: string) {
    actionName === 'add'
      ? (this.addAppraisalQuestionsPopupLoading = true)
      : (this.editAppraisalQuestionsPopupLoading = true);

    this.googleObj.q = inputText;
    this.commonService.translate(this.googleObj, this.googleKey).subscribe(
      (res: any) => {
        const translatedData = JSON.parse(res._body);
        this.appraisalQuestionsForm.DariQuestion =
          translatedData.data.translations.length > 0
            ? translatedData.data.translations[0].translatedText
            : '';

        actionName === 'add'
          ? (this.addAppraisalQuestionsPopupLoading = false)
          : (this.editAppraisalQuestionsPopupLoading = false);
      },
      err => {}
    );
  }
  //#endregion

  //#region "Hide / Show popups"
  onShowHideAppraisalQuestionsAddPopup() {
    this.addAppraisalQuestionsPopupVisible = !this
      .addAppraisalQuestionsPopupVisible;

    if (this.addAppraisalQuestionsPopupVisible) {
      this.allFormInitialize();
    }
  }

  onShowHideAppraisalQuestionsEditPopup() {
    this.editAppraisalQuestionsPopupVisible = !this
      .editAppraisalQuestionsPopupVisible;
  }

  //#endregion

  //#region "on office Selected"
  // onOfficeSelected(event) {
  //   this.selectedOffice= event;

  //   this.getAllAppraisalQuestions();
  // }
  //#endregion
}

// tslint:disable-next-line:class-name
class appraisalQuestionsModel {
  AppraisalGeneralQuestionsId: any;
  SequenceNo: any;
  Question: any;
  DariQuestion: any;
  // OfficeId: number;
}
