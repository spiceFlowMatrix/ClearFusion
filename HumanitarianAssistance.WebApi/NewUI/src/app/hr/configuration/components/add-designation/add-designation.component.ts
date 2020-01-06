import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
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
  title = 'Add Designation';

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddDesignationComponent>,
              private hrService: HrService, private toastr: ToastrService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.addDesignationForm = this.fb.group({
      'Id': [null],
      'DesignationName': [null , [Validators.required]],
      'Description': [null, [Validators.required]],
      'Questions': this.fb.array([], Validators.required),
    });

    if (this.data) {
      this.title = 'Edit Designation';
      this.addDesignationForm.get('Id').patchValue(this.data.Id);
      this.addDesignationForm.get('DesignationName').patchValue(this.data.Designation);
      this.addDesignationForm.get('Description').patchValue(this.data.Description);

      if ( this.data.subItems === undefined && this.data.subItems.length < 0) {
        this.addQuestion();
      } else {
        this.data.subItems.forEach(x=> {
          this.editQuestionPatchValue(x);
        });
      }
    } else {
      this.addQuestion();
    }
  }

  addQuestion() {
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

  editQuestionPatchValue(item: any) {
    (<FormArray>this.addDesignationForm.get('Questions')).push(this.fb.group({
      QuestionId: [item.QuestionId],
      Question: [item.Question, Validators.required]
    }));
  }

  addDesignation() {
      this.isFormSubmitted = true;
      this.hrService.addDesignation(this.addDesignationForm.value).subscribe(x=> {
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

  editDesignation() {
    this.isFormSubmitted = true;
    this.hrService.editDesignation(this.addDesignationForm.value).subscribe(x => {
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

  saveDesignation() {
    if (this.addDesignationForm.valid) {

      if (this.addDesignationForm.value.Id == null) {
        this.addDesignation();
      } else {
        this.editDesignation();
      }
    }
  }
}
