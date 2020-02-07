import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDatepicker } from '@angular/material';
import { HrService } from 'src/app/hr/services/hr.service';
import { ToastrService } from 'ngx-toastr';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { AttendanceService } from 'src/app/hr/services/attendance.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-add-payroll-hours',
  templateUrl: './add-payroll-hours.component.html',
  styleUrls: ['./add-payroll-hours.component.scss']
})
export class AddPayrollHoursComponent implements OnInit {

  payrollForm: FormGroup;
  err = null;
  @ViewChild(MatDatepicker) picker;
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddPayrollHoursComponent>,
    private hrService: HrService, private toastr: ToastrService, private commonLoader: CommonLoaderService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private attendanceService: AttendanceService) {
      this.payrollForm = this.fb.group({
        OfficeId: null,
        Date: [null, Validators.required],
        InTime: [null, Validators.required],
        OutTime: [null, Validators.required]
      });
    }

  ngOnInit() {
  }

  onCancelPopup() {
    this.dialogRef.close();
  }

  monthSelected(params) {
    this.payrollForm.controls['Date'].setValue(params);
    this.picker.close();
  }

  submitPayrollForm() {
    this.err = null;
    if (!this.payrollForm.valid) {
      return;
    }
    this.commonLoader.showLoader();
    const model = {
      Date: StaticUtilities.getLocalDate(this.payrollForm.value.Date),
      AttendanceGroupId: this.data.AttendanceGroupId,
      InTime: new Date(new Date(StaticUtilities.getLocalDate(this.payrollForm.value.Date)).getFullYear(),
      new Date(StaticUtilities.getLocalDate(this.payrollForm.value.Date)).getMonth(),
      StaticUtilities.getLocalDate(this.payrollForm.value.Date).getDate(),
      this.payrollForm.value.InTime.split(':')[0], this.payrollForm.value.InTime.split(':')[1], 0),
      OutTime: new Date(new Date(StaticUtilities.getLocalDate(this.payrollForm.value.Date)).getFullYear(),
      new Date(StaticUtilities.getLocalDate(this.payrollForm.value.Date)).getMonth(),
      StaticUtilities.getLocalDate(this.payrollForm.value.Date).getDate(),
      this.payrollForm.value.OutTime.split(':')[0], this.payrollForm.value.OutTime.split(':')[1], 0),
      OfficeId: (this.data && this.data.SelectedOffice) ? this.data.SelectedOffice.value : null
    };
    this.attendanceService.setPayrollHoursToAttendanceGroup(model).subscribe(res => {
      if (res) {
        this.commonLoader.hideLoader();
        this.toastr.success('Payroll hours added successfully!');
        this.dialogRef.close();
      } else {
        this.commonLoader.hideLoader();
        this.err = 'Something went wrong!';
      }
    }, err => {
      this.commonLoader.hideLoader();
      this.err = err;
    });
  }
}
