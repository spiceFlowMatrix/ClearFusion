import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { HrService } from 'src/app/hr/services/hr.service';

@Component({
  selector: 'app-add-attendance-group',
  templateUrl: './add-attendance-group.component.html',
  styleUrls: ['./add-attendance-group.component.scss']
})
export class AddAttendanceGroupComponent implements OnInit {

  addAttendanceGroupForm: FormGroup;
  isFormSubmitted = false;
  title = 'Add Attendance Group';

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddAttendanceGroupComponent>,
    private hrService: HrService, private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.addAttendanceGroupForm = this.fb.group({
      'Id': [null],
      'Name': [null, [Validators.required]],
      'Description': [null],
    });

    if (this.data) {
      this.title = 'Edit Attendance Group';
      this.addAttendanceGroupForm.get('Id').patchValue(this.data.AttendanceGroupId);
      this.addAttendanceGroupForm.get('Name').patchValue(this.data.Name);
      this.addAttendanceGroupForm.get('Description').patchValue(this.data.Description);
    }
  }

  addAttendanceGroup() {
    this.isFormSubmitted = true;
    this.hrService.addAttendanceGroup(this.addAttendanceGroupForm.value).subscribe(x => {
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

editAttendanceGroup() {
  this.isFormSubmitted = true;
  this.hrService.editAttendanceGroup(this.addAttendanceGroupForm.value).subscribe(x => {
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

saveAttendanceGroup() {
  debugger;
  if (this.addAttendanceGroupForm.valid) {
    if (this.addAttendanceGroupForm.value.Id == null) {
      this.addAttendanceGroup();
    } else {
      this.editAttendanceGroup();
    }
  }
}

  onCancelPopup() {
    this.dialogRef.close();
  }

}
