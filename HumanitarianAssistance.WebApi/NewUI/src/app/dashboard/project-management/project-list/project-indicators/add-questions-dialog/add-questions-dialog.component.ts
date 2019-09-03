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

 @Output() onAddQuestionListRefresh = new EventEmitter();

  //#region  "variables"
  public questionForm: FormGroup;
  questionDetailModel: IQuestionDetailModel;
  IndicatorDetail: any;
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
  }

  ngOnInit() {
    this.initializeModel();
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

  //#region "OnSubmit"
  OnSubmit(formValue: any) {
    debugger;
    this.EditLoaderFlag = true;
    const indicatorId = this.IndicatorDetail.ProjectIndicatorId;
    if (formValue.IndicatorQuestion != null && formValue.IndicatorQuestion != '') {
      const model: IQuestionDetailModel = {
        ProjectIndicatorId: indicatorId,
        IndicatorQuestion: formValue.IndicatorQuestion,
        QuestionType: formValue.QuestionType,
        VerificationSources: [],
      };

      formValue.VerificationSources.forEach(element => {
        model.VerificationSources.push({
          VerificationSourceId: element.VerificationSourceId,
          VerificationSourceName: element.VerificationSourceName,
        });
      });

      this.indicatorService
        .AddProjectIndicatorQuestions(model)
        .subscribe(response => {
          if (response.statusCode === 200 && response.data != null) {
          this.IndicatorQuestionListRefresh(response.data);
            this.toastr.success('Project Indicator Updated Successfully');
          }
          this.EditLoaderFlag = false;
          this.onCancelPopup();

        },
          (error) => {
            this.EditLoaderFlag = false;
            this.toastr.success('Something went wrong');
          });
    }
    else {
      this.EditLoaderFlag = false;
    }



  }
  //#endregion
  // to get verication sources controls html
  get verificationSources(): FormArray {
    return this.questionForm.get('VerificationSources') as FormArray;
  }

  createItem(): FormGroup {
    debugger;
    return this.fb.group({
      VerificationSourceId: 0,
      VerificationSourceName: ''
    });
  }

  //#region "On add verification source"
  addItem() {
    debugger;
    this.verificationSources.push(this.createItem());
  }
  //#endregion

  //#region "onDelete"
  onDelete(index: number) {
    debugger;
    const control = <FormArray>this.questionForm.controls['VerificationSources'];
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

}
