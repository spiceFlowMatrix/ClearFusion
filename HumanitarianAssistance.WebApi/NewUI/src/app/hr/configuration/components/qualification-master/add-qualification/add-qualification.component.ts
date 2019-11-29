import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HrService } from 'src/app/hr/services/hr.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-qualification',
  templateUrl: './add-qualification.component.html',
  styleUrls: ['./add-qualification.component.scss']
})
export class AddQualificationComponent implements OnInit {

  addQualificationForm: FormGroup;
  isFormSubmitted = false;
  title = 'Add Qualification';

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddQualificationComponent>,
    private hrService: HrService, private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.addQualificationForm = this.fb.group({
      'QualificationId': [null],
      'QualificationName': [null, [Validators.required]],
    });

    if (this.data) {
      this.title = 'Edit Qualification';
      this.addQualificationForm.get('QualificationId').patchValue(this.data.QualificationId);
      this.addQualificationForm.get('QualificationName').patchValue(this.data.QualificationName);
    }
  }

  addQualification() {
    this.isFormSubmitted = true;
    this.hrService.addQualification(this.addQualificationForm.value).subscribe(x => {
      if (x.StatusCode === 200) {
        this.toastr.success('Success');
        this.isFormSubmitted = false;
        this.dialogRef.close();
      } else {
        this.toastr.warning(x.Message);
        this.isFormSubmitted = false;
      }
    }, error => {
      this.toastr.warning(error);
      this.isFormSubmitted = false;
    });
}

editQualification() {
  this.isFormSubmitted = true;
  this.hrService.editQualification(this.addQualificationForm.value).subscribe(x => {
    if (x.StatusCode === 200) {
      this.toastr.success('Success');
      this.isFormSubmitted = false;
      this.dialogRef.close();
    } else {
      this.toastr.warning(x.Message);
      this.isFormSubmitted = false;
    }
  }, error => {
    this.toastr.warning(error);
    this.isFormSubmitted = false;
  });
}

saveQualification() {
  if (this.addQualificationForm.valid) {
    if (this.addQualificationForm.value.QualificationId == null) {
      this.addQualification();
    } else {
      this.editQualification();
    }
  }
}

  onCancelPopup() {
    this.dialogRef.close();
  }

}
