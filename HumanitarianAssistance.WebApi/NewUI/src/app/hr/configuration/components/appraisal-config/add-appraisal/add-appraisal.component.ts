import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HrService } from 'src/app/hr/services/hr.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-add-appraisal',
  templateUrl: './add-appraisal.component.html',
  styleUrls: ['./add-appraisal.component.scss']
})
export class AddAppraisalComponent implements OnInit {
  addAppraisalQuestionForm: FormGroup;
  addEditAppraisalQuestion: addEditAppraisalQuestion = {};
  isFormSubmitted = false;
  constructor(private fb: FormBuilder, private hrservice: HrService, private dialogRef: MatDialogRef<AddAppraisalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.createForm();
    if (this.data) {
      this.addAppraisalQuestionForm.controls.question.setValue(this.data.Question);
      this.addAppraisalQuestionForm.controls.dariquestion.setValue(this.data.DariQuestion);
      this.addAppraisalQuestionForm.controls.sequence.setValue(this.data.SequenceNo);
    }
  }
  createForm() {
    this.addAppraisalQuestionForm = this.fb.group({
      question: ['', Validators.required],
      dariquestion: [''],
      sequence: [0]
    });
  }
  onCancelPopup() {
    this.dialogRef.close();
  }
  saveQuestion() {
    this.isFormSubmitted = true;
    if(this.data){
      this.addEditAppraisalQuestion.AppraisalGeneralQuestionsId = this.data.Id;
    }
    this.addEditAppraisalQuestion.Question = this.addAppraisalQuestionForm.controls.question.value;
    this.addEditAppraisalQuestion.DariQuestion = this.addAppraisalQuestionForm.controls.dariquestion.value;
    this.addEditAppraisalQuestion.SequenceNo = this.addAppraisalQuestionForm.controls.sequence.value;

    if (this.data) {
      this.hrservice.editAppraisalQuestion(this.addEditAppraisalQuestion).subscribe(res => {
        this.isFormSubmitted = false;
        this.dialogRef.close();
      })
    } else {
      this.hrservice.addAppraisalQuestion(this.addEditAppraisalQuestion).subscribe(res => {
        this.isFormSubmitted = false;
        this.dialogRef.close();
      })
    }

  }

}
interface addEditAppraisalQuestion {
  AppraisalGeneralQuestionsId?: number;
  Question?: string;
  DariQuestion?: string;
  SequenceNo?: number;
}
