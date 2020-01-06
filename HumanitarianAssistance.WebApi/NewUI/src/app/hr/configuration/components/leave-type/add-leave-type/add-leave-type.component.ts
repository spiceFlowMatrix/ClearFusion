import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { HrService } from 'src/app/hr/services/hr.service';

@Component({
  selector: 'app-add-leave-type',
  templateUrl: './add-leave-type.component.html',
  styleUrls: ['./add-leave-type.component.scss']
})
export class AddLeaveTypeComponent implements OnInit {

  addLeaveTypeForm: FormGroup;
  isFormSubmitted = false;
  title = 'Add New Leave Type';

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddLeaveTypeComponent>,
    private hrService: HrService, private toastr: ToastrService, private commonLoader: CommonLoaderService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.addLeaveTypeForm = this.fb.group({
      'LeaveReasonId': [null],
      'ReasonName': [null, [Validators.required]],
      'Description': [null],
      'Unit': [null, [Validators.required]],
    });

    if (this.data) {
      this.title = 'Edit Leave Type';
      this.addLeaveTypeForm.get('LeaveReasonId').patchValue(this.data.LeaveReasonId);
      this.addLeaveTypeForm.get('ReasonName').patchValue(this.data.ReasonName);
      this.addLeaveTypeForm.get('Description').patchValue(this.data.Description);
      this.addLeaveTypeForm.get('Unit').patchValue(this.data.Unit);
    }
  }

  updateLeaveType() {
    this.isFormSubmitted = true;
    this.hrService.updateLeaveType(this.addLeaveTypeForm.value).subscribe(x => {
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

addLeaveType() {
  this.isFormSubmitted = true;
  this.hrService.addLeaveType(this.addLeaveTypeForm.value).subscribe(x => {
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

saveLeaveType() {
  if (this.addLeaveTypeForm.valid) {
    if (this.addLeaveTypeForm.value.LeaveReasonId) {
      this.updateLeaveType();
    } else {
      this.addLeaveType();
    }
  }
}

  onCancelPopup() {
    this.dialogRef.close();
  }
}
