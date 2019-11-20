import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { HrService } from 'src/app/hr/services/hr.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-designation',
  templateUrl: './add-designation.component.html',
  styleUrls: ['./add-designation.component.scss']
})
export class AddDesignationComponent implements OnInit {

  addDesignationForm: FormGroup;
  isFormSubmitted = false;

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddDesignationComponent>,
              private hrService: HrService, private toastr: ToastrService) { }

  ngOnInit() {
    this.addDesignationForm = this.fb.group({
      'Id': [null],
      'DesignationName': [null , [Validators.required]],
      'Description': [null, [Validators.required]],
      'Questions': this.fb.array([], Validators.required),
    });
    this.addQuestion();
  }

  addQuestion() {
   // const questionArray = this.addDesignationForm.controls.Questions as FormArray;
    (<FormArray>this.addDesignationForm.get('Questions')).push(this.fb.group({
      QuestionId: [0],
      Question: ['', Validators.required]
    }));
  }

  deleteQuestion(index: number) {
    (<FormArray>this.addDesignationForm.get('Questions')).removeAt(index);
  }

  onCancelPopup() {
    this.dialogRef.close();
  }

  addDesignation() {
    if (this.addDesignationForm.valid) {
      this.isFormSubmitted = true;
      this.hrService.addDesignation(this.addDesignationForm.value).subscribe(x=> {
        debugger;
        if (x.SuccessCode === 200) {
          this.toastr.success('Success');
          this.isFormSubmitted = false;
          this.dialogRef.close();
        } else {
          this.toastr.warning(x.Message);
          this.isFormSubmitted = false;
        }
      }, error => {
        this.toastr.warning('Something went wrong');
        this.isFormSubmitted = false;
      });
    }
  }
}
