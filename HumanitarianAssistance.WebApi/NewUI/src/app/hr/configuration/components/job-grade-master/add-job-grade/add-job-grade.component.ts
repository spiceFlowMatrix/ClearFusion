import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { HrService } from 'src/app/hr/services/hr.service';

@Component({
  selector: 'app-add-job-grade',
  templateUrl: './add-job-grade.component.html',
  styleUrls: ['./add-job-grade.component.scss']
})
export class AddJobGradeComponent implements OnInit {

  addGradeForm: FormGroup;
  isFormSubmitted = false;
  title = 'Add Job Grade';

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddJobGradeComponent>,
    private hrService: HrService, private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.addGradeForm = this.fb.group({
      'GradeId': [null],
      'GradeName': [null, [Validators.required]],
    });

    if (this.data) {
      this.title = 'Edit Job Grade';
      this.addGradeForm.get('GradeId').patchValue(this.data.GradeId);
      this.addGradeForm.get('GradeName').patchValue(this.data.GradeName);
    }
  }

  addJobGrade() {
    this.isFormSubmitted = true;
    this.hrService.addJobGrade(this.addGradeForm.value).subscribe(x => {
      if (x.StatusCode === 200) {
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

editJobGrade() {
  this.isFormSubmitted = true;
  this.hrService.editJobGrade(this.addGradeForm.value).subscribe(x => {
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

saveJobGrade() {
  if (this.addGradeForm.valid) {
    if (this.addGradeForm.value.GradeId == null) {
      this.addJobGrade();
    } else {
      this.editJobGrade();
    }
  }
}

  onCancelPopup() {
    this.dialogRef.close();
  }

}
