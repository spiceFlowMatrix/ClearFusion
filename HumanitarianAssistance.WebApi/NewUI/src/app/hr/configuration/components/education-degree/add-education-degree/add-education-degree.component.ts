import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HrService } from 'src/app/hr/services/hr.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AddDesignationComponent } from '../../add-designation/add-designation.component';

@Component({
  selector: 'app-add-education-degree',
  templateUrl: './add-education-degree.component.html',
  styleUrls: ['./add-education-degree.component.scss']
})
export class AddEducationDegreeComponent implements OnInit {

  addDegreeForm: FormGroup;
  isFormSubmitted = false;
  title = 'Add Education Degree';

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddDesignationComponent>,
    private hrService: HrService, private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.addDegreeForm = this.fb.group({
      'Id': [null],
      'Name': [null, [Validators.required]]
    });

    if (this.data) {
      this.title = 'Edit Education Degree';
      this.addDegreeForm.get('Id').patchValue(this.data.Id);
      this.addDegreeForm.get('Name').patchValue(this.data.Name);
    }
  }

  addDegree() {
    this.isFormSubmitted = true;
    this.hrService.addDegree(this.addDegreeForm.value).subscribe(x => {
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

editDegree() {
  this.isFormSubmitted = true;
  this.hrService.editDegree(this.addDegreeForm.value).subscribe(x => {
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

saveDegree() {
  if (this.addDegreeForm.valid) {
    if (this.addDegreeForm.value.Id == null) {
      this.addDegree();
    } else {
      this.editDegree();
    }
  }
}

  onCancelPopup() {
    this.dialogRef.close();
  }

}
