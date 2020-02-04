import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { AttendanceService } from 'src/app/hr/services/attendance.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-edit-employee-attendance',
  templateUrl: './edit-employee-attendance.component.html',
  styleUrls: ['./edit-employee-attendance.component.scss']
})
export class EditEmployeeAttendanceComponent implements OnInit {

  editAttendanceForm: FormGroup;
  constructor(public dialogRef: MatDialogRef<EditEmployeeAttendanceComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private attendanceService: AttendanceService,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService) {
    }

  ngOnInit() {
    this.editAttendanceForm = this.fb.group({
      AttendanceId: [this.data.AttendanceId],
      AttendanceTypeId: [this.data.AttendanceTypeId],
      InTime: [this.datePipe.transform(this.data.InTime, 'shortTime')],
      OutTime: [this.datePipe.transform(this.data.OutTime, 'shortTime')]
    });
  }

  closePopup() {
    this.dialogRef.close();
  }

  saveAttendance(value) {
    this.commonLoader.showLoader();
    const model = {
      AttendanceId: value.AttendanceId,
      AttendanceTypeId: value.AttendanceTypeId,
      EmployeeId: this.data.EmployeeId,
      Date: new Date(
        new Date(StaticUtilities.getLocalDate(this.data.Date)).getFullYear(),
        new Date(StaticUtilities.getLocalDate(this.data.Date)).getMonth(),
        new Date(StaticUtilities.getLocalDate(this.data.Date)).getDate(),
        this.convert12hTimeToHours(value.InTime),
        this.convert12hTimeToMinutes(value.InTime)
      ).toUTCString(),

      InTime: new Date(
        new Date(StaticUtilities.getLocalDate(this.data.Date)).getFullYear(),
        new Date(StaticUtilities.getLocalDate(this.data.Date)).getMonth(),
        new Date(StaticUtilities.getLocalDate(this.data.Date)).getDate(),
        this.convert12hTimeToHours(value.InTime),
        this.convert12hTimeToMinutes(value.InTime)
      ).toUTCString(),
      OutTime: new Date(
        new Date(this.data.Date).getFullYear(),
        new Date(this.data.Date).getMonth(),
        new Date(this.data.Date).getDate(),
        this.convert12hTimeToHours(value.OutTime),
        this.convert12hTimeToMinutes(value.OutTime)
      ).toUTCString(),
      LeaveStatus: false,
      OfficeId: this.data.OfficeId
    };
    this.attendanceService.editEmployeeAttendance(model).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.toastr.success('Attendance edited successfully!');
        this.dialogRef.close();
      } else {
        this.commonLoader.hideLoader();
        this.toastr.warning('Something went wrong!');
      }
    }, err => {
      this.commonLoader.hideLoader();
      this.toastr.warning('Something went wrong!');
    });
  }

  convert12hTimeToHours(time12h): number {
    const [time, modifier] = time12h.split(' ');
    let [hours, minutes] = time.split(':');
    if (hours === '12') {
      hours = '00';
    }
    if (modifier === 'PM') {
      hours = parseInt(hours, 10) + 12;
    }
    return hours;
  }

  convert12hTimeToMinutes(time12h): number {
    const [time, modifier] = time12h.split(' ');
    let [hours, minutes] = time.split(':');
    if (hours === '12') {
      hours = '00';
    }
    if (modifier === 'PM') {
      hours = parseInt(hours, 10) + 12;
    }
    return minutes;
  }

}
