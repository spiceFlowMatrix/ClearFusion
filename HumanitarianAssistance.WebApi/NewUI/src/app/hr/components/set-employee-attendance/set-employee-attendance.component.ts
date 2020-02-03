import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { AttendanceService } from '../../services/attendance.service';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-set-employee-attendance',
  templateUrl: './set-employee-attendance.component.html',
  styleUrls: ['./set-employee-attendance.component.scss']
})
export class SetEmployeeAttendanceComponent implements OnInit {

  attendanceForm: FormGroup;
  constructor(private dialogRef: MatDialogRef<SetEmployeeAttendanceComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private attendanceService: AttendanceService,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService) {
      this.attendanceForm = this.fb.group({
        AttendanceDate: [null],
        EmployeeAttendance: this.fb.array([this.createAttendanceEntry(0, null, new Date(), new Date(), 1, 0)])
      });
    }

  ngOnInit() {
    this.initializeAttendanceFormArray();
  }

  createAttendanceEntry(EmployeeId, EmployeeName, InTime, OutTime, Attendance, AttendanceGroupId): FormGroup {
    return this.fb.group({
      EmployeeId: [EmployeeId],
      EmployeeName: [EmployeeName],
      InTime: [InTime],
      OutTime: [OutTime],
      Attendance: [Attendance],
      AttendanceGroupId: [AttendanceGroupId]
    });
  }

  initializeAttendanceFormArray() {
   // this.attendanceForm.controls.AttendanceDate.setValue(this.data.AttendanceDate);
    (<FormArray>this.attendanceForm.get('EmployeeAttendance')).removeAt(0);
    this.data.EmployeeList.forEach(element => {
      (this.attendanceForm.controls['EmployeeAttendance'] as FormArray)
        .push(this.createAttendanceEntry(element.EmployeeId, element.Name,
        this.datePipe.transform(element.InTime, 'shortTime'),
        this.datePipe.transform(element.OutTime, 'shortTime'), element.Attendance,
        element.AttendanceGroupId));
    });
  }

  closeDialog() {
    this.dialogRef.close();
  }

  saveEmployeeAttendance(value) {
    this.commonLoader.showLoader();
    this.data.AttendanceDates.forEach(val => {
     
      const model = {
        AttendanceDate: StaticUtilities.getLocalDate(val.toUTCString()),
        OfficeId: this.data.OfficeId,
        EmployeeAttendance: []
      };
      value.EmployeeAttendance.forEach(element => {
        const obj = {
          EmployeeId: element.EmployeeId,
          AttendanceTypeId: element.Attendance,
          InTime: new Date(
            val.getFullYear(),
            val.getMonth(),
            val.getDate(),
            this.convert12hTimeToHours(element.InTime),
            this.convert12hTimeToMinutes(element.InTime)
          ).toUTCString(),
          OutTime: new Date(
            val.getFullYear(),
            val.getMonth(),
            val.getDate(),
            this.convert12hTimeToHours(element.OutTime),
            this.convert12hTimeToMinutes(element.OutTime)
          ).toUTCString(),
          AttendanceGroupId: element.AttendanceGroupId,
          Date: StaticUtilities.getLocalDate(val.toUTCString())
        };
        model.EmployeeAttendance.push(obj);
      });
      this.attendanceService.saveEmployeeAttendance(model).subscribe(res => {
        if (res) {
          this.commonLoader.hideLoader();
          this.dialogRef.close();
          this.toastr.success('Attendance Saved Successfully!');
        } else {
          this.commonLoader.hideLoader();
          this.toastr.warning('Something went wrong!');
        }
      }, err => {
        this.toastr.warning(err);
        this.commonLoader.hideLoader();
      });
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
