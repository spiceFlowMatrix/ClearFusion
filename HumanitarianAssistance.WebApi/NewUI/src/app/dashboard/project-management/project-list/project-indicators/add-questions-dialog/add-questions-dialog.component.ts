import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ProjectIndicatorService } from '../project-indicator.service';
import {
  IQuestionsDataSource,
  IQuestionDetailModel,
  IVerificationSourceModel
} from '../project-indicators-model';

@Component({
  selector: 'app-add-questions-dialog',
  templateUrl: './add-questions-dialog.component.html',
  styleUrls: ['./add-questions-dialog.component.scss']
})
export class AddQuestionsDialogComponent implements OnInit {
  // tslint:disable-next-line: no-output-on-prefix
  @Output() onAddQuestionListRefresh = new EventEmitter();
  // tslint:disable-next-line: no-output-on-prefix
  @Output() onUpdatedQuestionListRefresh = new EventEmitter();
  //#region  "variables"
  public questionForm: FormGroup;
  verificationSounceLoaderFlag: false;
  questionDetailModel: IQuestionDetailModel;
  IndicatorDetail: any;
  questionDetail: any;
  // flag
  EditLoaderFlag = false;
  // data-Source
  questionType = [
    { Id: 1, Name: 'Qualitative' },
    { Id: 2, Name: 'Quantitative' }
  ];
  //#endregion

  constructor(
    public dialogRef: MatDialogRef<AddQuestionsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IQuestionsDataSource,
    private fb: FormBuilder,
    public toastr: ToastrService,
    public indicatorService: ProjectIndicatorService
  ) {
    this.IndicatorDetail = data.ProjectindicatorDetail;
    this.questionDetail = data.QuestionDetail;
    console.log('wwhuh', this.questionDetail);
  }

  ngOnInit() {
    this.initializeModel();
    if (this.questionDetail != null && this.questionDetail !== undefined) {
      this.setIndicatorQuestionDetail();
    }
  }

  initializeModel() {
    this.questionForm = this.fb.group({
      IndicatorQuestion: ['', Validators.required],
      IndicatorQuestionId: null,
      ProjectIndicatorId: null,
      QuestionType: ['', Validators.required],
      VerificationSources: this.fb.array([])
    });

    this.questionDetailModel = {
      IndicatorQuestionId: null,
      IndicatorQuestion: null,
      QuestionType: null,
      ProjectIndicatorId: null,
      VerificationSources: null,
      QuestionTypeName: null
    };
  }
  // set details after edit question
  setIndicatorQuestionDetail() {
    this.questionForm = this.fb.group({
      IndicatorQuestion: [this.questionDetail.IndicatorQuestion],
      IndicatorQuestionId: [this.questionDetail.IndicatorQuestionId],
      ProjectIndicatorId: [this.questionDetail.ProjectIndicatorId],
      QuestionType: [this.questionDetail.QuestionType]
    });
    this.questionForm.setControl(
      'VerificationSources',
      this.setVerificationSource(this.questionDetail.VerificationSources)
    );
  }

  setVerificationSource(sources: IVerificationSourceModel[]): FormArray {
    const formArray = new FormArray([]);
    sources.forEach(s => {
      formArray.push(
        this.fb.group({
          VerificationSourceId: s.VerificationSourceId,
          VerificationSourceName: s.VerificationSourceName
        })
      );
    });

    return formArray;
  }
  //#region "OnSubmit"
  OnSubmit(formValue: any) {
    if (
      formValue.IndicatorQuestionId != null &&
      formValue.IndicatorQuestionId !== undefined
    ) {
      this.EditIndicatorQuestion(formValue);
    } else {
      this.AddIndicatorQuestion(formValue);
    }
  }
  //#region "AddIndicatorQuestion"
  AddIndicatorQuestion(formValue: any) {
    this.EditLoaderFlag = true;
    const indicatorId = this.IndicatorDetail.ProjectIndicatorId;
    if (
      formValue.IndicatorQuestion != null &&
      formValue.IndicatorQuestion != ''
    ) {
      const model: IQuestionDetailModel = {
        ProjectIndicatorId: indicatorId,
        IndicatorQuestion: formValue.IndicatorQuestion,
        QuestionType: formValue.QuestionType,
        VerificationSources: []
      };

      formValue.VerificationSources.forEach(element => {
        model.VerificationSources.push({
          VerificationSourceId: element.VerificationSourceId,
          VerificationSourceName: element.VerificationSourceName
        });
      });

      this.indicatorService.AddProjectIndicatorQuestions(model).subscribe(
        response => {
          if (response.statusCode === 200 && response.data != null) {
            this.IndicatorQuestionListRefresh(response.data);
            this.toastr.success('Project Indicator Updated Successfully');
          }
          this.EditLoaderFlag = false;
          this.onCancelPopup();
        },
        error => {
          this.EditLoaderFlag = false;
          this.toastr.success('Something went wrong');
        }
      );
    } else {
      this.EditLoaderFlag = false;
    }
  }

  EditIndicatorQuestion(formValue: any) {
    this.EditLoaderFlag = true;
    const indicatorId = this.IndicatorDetail.ProjectIndicatorId;
    if (
      formValue.IndicatorQuestion != null &&
      formValue.IndicatorQuestion != ''
    ) {
      const model: IQuestionDetailModel = {
        ProjectIndicatorId: indicatorId,
        IndicatorQuestionId: formValue.IndicatorQuestionId,
        IndicatorQuestion: formValue.IndicatorQuestion,
        QuestionType: formValue.QuestionType,
        VerificationSources: []
      };

      formValue.VerificationSources.forEach(element => {
        model.VerificationSources.push({
          VerificationSourceId: element.VerificationSourceId,
          VerificationSourceName: element.VerificationSourceName
        });
      });

      this.indicatorService.EditProjectIndicatorQuestions(model).subscribe(
        response => {
          if (response.statusCode === 200 && response.data != null) {
            this.UpdatedQuestionListRefresh(response.data);
            this.toastr.success('Project Indicator Updated Successfully');
          }
          this.EditLoaderFlag = false;
          this.onCancelPopup();
        },
        error => {
          this.EditLoaderFlag = false;
          this.toastr.success('Something went wrong');
        }
      );
    } else {
      this.EditLoaderFlag = false;
    }
  }

  //#endregion
  //#endregion
  // to get verication sources controls html
  get verificationSources(): FormArray {
    return this.questionForm.get('VerificationSources') as FormArray;
  }

  createItem(): FormGroup {
    return this.fb.group({
      VerificationSourceId: 0,
      VerificationSourceName: ''
    });
  }

  //#region "On add verification source"
  addItem() {
    this.verificationSources.push(this.createItem());
  }
  //#endregion

  //#region "onDelete"
  onDelete(index: number) {
    const control = <FormArray>(
      this.questionForm.controls['VerificationSources']
    );
    control.removeAt(index);
  }
  //#endregion

  //#region "onCancelPopup"
  onCancelPopup() {
    this.dialogRef.close();
  }
  //#endregion

  //#region "indicatorListRefresh"
  IndicatorQuestionListRefresh(data: any) {
    this.onAddQuestionListRefresh.emit(data);
  }
  //#endregion

  //#region "UpdatedQuestionListRefresh"
  UpdatedQuestionListRefresh(data: any) {
    this.onUpdatedQuestionListRefresh.emit(data);
  }
  //#endregion
}
