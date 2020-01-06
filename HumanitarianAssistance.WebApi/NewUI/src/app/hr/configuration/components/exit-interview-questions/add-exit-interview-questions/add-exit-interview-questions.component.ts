import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HrService } from 'src/app/hr/services/hr.service';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs/internal/observable/of';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-add-exit-interview-questions',
  templateUrl: './add-exit-interview-questions.component.html',
  styleUrls: ['./add-exit-interview-questions.component.scss']
})
export class AddExitInterviewQuestionsComponent implements OnInit {


  addExitInterviewQuestion: FormGroup;
  isFormSubmitted = false;
  title = 'Add New Exit Interview Question';

  questionTypes$ = of([
    {value: 1, name: 'Feeling About Employee Aspects'},
    {value: 2, name: 'Reason Of Leaving'},
    {value: 3, name: 'The Department'},
    {value: 4, name: 'The Job Itself'},
    {value: 5, name: 'My Supervisor'},
    {value: 6, name: 'The Management'},
  ]);

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddExitInterviewQuestionsComponent>,
    private hrService: HrService, private toastr: ToastrService, private commonLoader: CommonLoaderService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.addExitInterviewQuestion = this.fb.group({
      'Id': [null],
      'QuestionText': [null, [Validators.required]],
      'QuestionType': [null, [Validators.required]],
      'SequencePosition': [null, [Validators.required]],
    });

    if (this.data) {
      this.title = 'Edit Exit Interview Question';
      this.addExitInterviewQuestion.get('Id').patchValue(this.data.Id);
      this.addExitInterviewQuestion.get('QuestionText').patchValue(this.data.QuestionText);
      this.addExitInterviewQuestion.get('QuestionType').patchValue(this.data.QuestionType);
      this.addExitInterviewQuestion.get('SequencePosition').patchValue(this.data.SequencePosition);
    }
  }

  upsertExitInterviewQuestion() {
    this.isFormSubmitted = true;
    this.hrService.UpsertExitInterviewQuestion(this.addExitInterviewQuestion.value).subscribe(x => {
      if (x) {
        this.toastr.success('Success');
        this.isFormSubmitted = false;
        this.dialogRef.close();
      } else {
        this.toastr.warning('Something went wrong');
        this.isFormSubmitted = false;
      }
    }, error => {
      this.toastr.warning(error);
      this.isFormSubmitted = false;
    });
}

saveQuestion() {
  if (this.addExitInterviewQuestion.valid) {
    this.upsertExitInterviewQuestion();
  }
}

  onCancelPopup() {
    this.dialogRef.close();
  }

  getQuestionTypeSelectedValue(event) {
    this.getSequenceNumber(event);
  }

  getSequenceNumber(questionType) {
    this.commonLoader.showLoader();

    const model = {
      QuestionType: questionType,
      Id: this.data ? this.data.Id : 0
    };
    this.hrService.getSequenceNumber(model).subscribe(x => {
      this.commonLoader.hideLoader();
      if (x) {
        this.addExitInterviewQuestion.get('SequencePosition').patchValue(x);
      } else {
        this.toastr.warning('Something went wrong');
      }
    }, error => {
      this.toastr.warning(error);
      this.commonLoader.hideLoader();
    });
  }
}
