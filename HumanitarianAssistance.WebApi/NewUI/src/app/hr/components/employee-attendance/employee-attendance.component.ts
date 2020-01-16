import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { MatDatepicker } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { AttendanceService } from '../../services/attendance.service';
import { ToastrService } from 'ngx-toastr';
import { IAttendanceModel } from '../../models/attendance-models';

@Component({
  selector: 'app-employee-attendance',
  templateUrl: './employee-attendance.component.html',
  styleUrls: ['./employee-attendance.component.scss']
})
export class EmployeeAttendanceComponent implements OnInit {
  //#region "Input/Output"
  @ViewChild(MatDatepicker) picker;

  //#endregion
  // Variables
  maxDate = new Date();
  attendanceForm: any;
  employeeId: number;
  Month: any;
  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  attendanceList: IAttendanceModel[] = [];
  constructor(
    private routeActive: ActivatedRoute,
    private attendanceService: AttendanceService,
    private toastr: ToastrService
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
    debugger;
    this.routeActive.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
    console.log(this.employeeId);
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
    debugger;
    this.attendanceForm.PageIndex = event.pageIndex;
    this.attendanceForm.PageSize = event.pageSize;
    this.getAttendanceList(this.employeeId);
  }
  //#region "GetAttendanceFileterList"
  getAttendanceList(employeeId: any) {
    debugger;
    this.attendanceForm.TotalCount = 0;
    this.attendanceForm.EmployeeId = employeeId;
    this.attendanceList = [];
    this.attendanceService.getAttendanceList(this.attendanceForm).subscribe(
      response => {
        if (response != null && response.attendanceList.length > 0) {
          response.attendanceList.forEach(element => {
            this.attendanceList.push({
              Date: element.Date,
              InTime: element.InTime,
              OutTime: element.OutTime,
              Attended: element.Attended
            });
          });
          this.attendanceForm.TotalCount = response.TotalCount;
        }
      },
      error => {
        this.toastr.warning(error);
      }
    );
  }
  //#endregion

  //#region "monthSelected"
  monthSelected(params) {
    debugger;
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
  //#endregion
}
