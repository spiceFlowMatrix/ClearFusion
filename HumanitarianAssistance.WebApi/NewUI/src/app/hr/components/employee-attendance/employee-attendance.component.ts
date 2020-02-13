import { Component, OnInit, ViewChild, HostListener, Input } from '@angular/core';
import { MatDatepicker, MatDialog } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { AttendanceService } from '../../services/attendance.service';
import { ToastrService } from 'ngx-toastr';
import { IAttendanceModel } from '../../models/attendance-models';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { DatePipe } from '@angular/common';
import { EditEmployeeAttendanceComponent } from './edit-employee-attendance/edit-employee-attendance.component';

@Component({
  selector: 'app-employee-attendance',
  templateUrl: './employee-attendance.component.html',
  styleUrls: ['./employee-attendance.component.scss']
})
export class EmployeeAttendanceComponent implements OnInit {
  //#region "Input/Output"
  @ViewChild(MatDatepicker) picker;
  @Input() officeId;
  //#endregion
  err = null;
  // Variables
  maxDate = new Date();
  attendanceForm: any;
  employeeId: number;
  Month: any;
  isNoAttendanceMarked = false;
  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  attendanceList: IAttendanceModel[] = [];
  constructor(
    private routeActive: ActivatedRoute,
    private attendanceService: AttendanceService,
    private toastr: ToastrService,
    public commonLoader: CommonLoaderService,
    private datePipe: DatePipe,
    public dialog: MatDialog
  ) {
    this.getScreenSize();
  }
  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 200 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  ngOnInit() {
    this.routeActive.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
    this.maxDate.setDate(this.maxDate.getDate());
    this.initForm();
    if (this.employeeId != null && this.employeeId !== undefined) {
      this.getAttendanceList(this.employeeId);
    }
    this.Month = new Date();
  }
  //#region "initForm"
  initForm() {
    this.attendanceForm = {
      Year: new Date().getFullYear(),
      Month: new Date().getMonth() + 1,
      PageIndex: 0,
      PageSize: 10,
      TotalCount: 0,
      EmployeeId: null
    };
  }


  //#endregion
  pageEvent(event: any) {
    this.attendanceForm.PageIndex = event.pageIndex;
    this.attendanceForm.PageSize = event.pageSize;
    this.getAttendanceList(this.employeeId);
  }
  //#region "GetAttendanceFileterList"
  getAttendanceList(employeeId: any) {
    this.commonLoader.showLoader();
    this.attendanceForm.TotalCount = 0;
    this.attendanceForm.EmployeeId = employeeId;
    this.attendanceList = [];
    this.attendanceService.getAttendanceList(this.attendanceForm).subscribe(
      response => {
        if (response != null && response.attendanceList.length > 0) {
          response.attendanceList.forEach(element => {
            this.attendanceList.push({
              AttendanceId: element.AttendanceId,
              DisplayDate: element.DisplayDate,
              Date: element.Date,
              InTime: this.datePipe.transform(StaticUtilities.setLocalDate(element.InTime), 'shortTime'),
              OutTime: this.datePipe.transform(StaticUtilities.setLocalDate(element.OutTime), 'shortTime'),
              Attended: element.Attended
            });
          });
          this.attendanceForm.TotalCount = response.TotalCount;
        }
        if (response.TotalCount === 0) {
          this.isNoAttendanceMarked = true;
        } else {
          this.isNoAttendanceMarked = false;
        }
        this.commonLoader.hideLoader();
      },
      error => {
        this.toastr.warning(error);
        this.commonLoader.hideLoader();
      }
    );
  }
  //#endregion

  //#region "monthSelected"
  monthSelected(params) {
    if (params != null && params !== undefined) {
      this.picker.close();
      this.Month = params;
      const year = new Date(params).getFullYear();
      this.attendanceForm.Year = year;

      this.attendanceForm.Month = new Date(params).getMonth() + 1;
      this.attendanceForm.MonthFilterValue = params;
      this.getAttendanceList(this.employeeId);
    }
  }

  markWholeMonthAttendance() {
    this.err = null;
    const model = {
      EmployeeId: this.employeeId,
      Month: this.attendanceForm.Month,
      Year: this.attendanceForm.Year
    };
    this.commonLoader.showLoader();
    this.attendanceService.markWholeMonthAttendance(model).subscribe(res => {
      if (res) {
        this.toastr.success('Attendance Marked Successfully!');
        this.getAttendanceList(this.employeeId);
        this.commonLoader.hideLoader();
      } else {
        this.commonLoader.hideLoader();
      }
    }, err => {
      this.err = err;
      this.commonLoader.hideLoader();
    });
  }
  //#endregion

  editAttendance (attendanceId, attended, date, InTime, OutTime) {
    const dialogRef = this.dialog.open(EditEmployeeAttendanceComponent, {
      width: '500px',
      data: {
        EmployeeId: this.employeeId,
        AttendanceId: attendanceId,
        AttendanceTypeId: (attended === 'Yes') ? 1 : 2,
        InTime: new Date(
          new Date(date).getFullYear(),
          new Date(date).getMonth(),
          new Date(date).getDate(),
          this.convert12hTimeToHours(InTime),
          this.convert12hTimeToMinutes(InTime)
        ),
        OutTime: new Date(
          new Date(date).getFullYear(),
          new Date(date).getMonth(),
          new Date(date).getDate(),
          this.convert12hTimeToHours(OutTime),
          this.convert12hTimeToMinutes(OutTime)
        ),
        Date: new Date(date),
        OfficeId: this.officeId
      }
    });
    // refresh the data after new request created
    dialogRef.afterClosed().subscribe(() => {
      this.getAttendanceList(this.employeeId);
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
