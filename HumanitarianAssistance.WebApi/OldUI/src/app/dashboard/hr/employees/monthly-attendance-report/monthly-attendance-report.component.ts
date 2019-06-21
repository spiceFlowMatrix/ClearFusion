import { Component, OnInit, Input } from '@angular/core';
import { HrService } from '../../hr.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-monthly-attendance-report',
  templateUrl: './monthly-attendance-report.component.html',
  styleUrls: ['./monthly-attendance-report.component.css']
})
export class MonthlyAttendanceReportComponent implements OnInit {
  @Input() employeeId: number;

  monthlyAttendanceReportList: any[];
  currentDate = new Date();

  // loader
  monthlyAttendanceReportInfoLoading = false;

  constructor(
    private hrService: HrService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}
  ngOnInit() {
    this.getAllMonthlyEmployeeAttendanceReport(
      this.employeeId,
      new Date().getFullYear(),
      new Date().getMonth() + 1
    );
  }

  //#region "on Date Selected"
  onMonthlyAttendanceDateSelected(data) {
    const monthlySelectedDate = data.value;
    this.getAllMonthlyEmployeeAttendanceReport(
      this.employeeId,
      new Date(monthlySelectedDate).getFullYear(),
      new Date(monthlySelectedDate).getMonth() + 1
    );
  }
  //#endregion

  //#region "GET ALL EMPLOYEE HISTORY"
  getAllMonthlyEmployeeAttendanceReport(
    empId: number,
    year: number,
    month: number
  ) {
    this.monthlyAttendanceReportInfoLoading = true;
    this.hrService
      .GetAllMonthlyEmployeeAttendanceReport(
        this.setting.getBaseUrl() +
          GLOBAL.API_HR_MonthlyEmployeeAttendanceReport,
        empId,
        year,
        month,
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          this.monthlyAttendanceReportList = [];
          if (
            data.StatusCode === 200 &&
            data.data.MonthlyEmployeeAttendanceList != null &&
            data.data.MonthlyEmployeeAttendanceList.length > 0
          ) {
            data.data.MonthlyEmployeeAttendanceList.forEach(element => {
              this.monthlyAttendanceReportList.push({
                Date: element.Date,

                InTime:
                  element.InTime !== 'NA'
                    ? new Date(
                        new Date(
                          new Date(
                            new Date().getFullYear() +
                              ' ' +
                              new Date().getMonth() +
                              ' ' +
                              new Date().getDay() +
                              ' ' +
                              element.InTime
                          )
                        ).getTime() -
                          new Date().getTimezoneOffset() * 60000
                      )
                    : 'NA',
                OutTime:
                  element.OutTime !== 'NA'
                    ? new Date(
                        new Date(
                          new Date(
                            new Date().getFullYear() +
                              ' ' +
                              new Date().getMonth() +
                              ' ' +
                              new Date().getDay() +
                              ' ' +
                              element.OutTime
                          )
                        ).getTime() -
                          new Date().getTimezoneOffset() * 60000
                      )
                    : 'NA',

                LateArrival: element.LateArrival,
                EarlyOut: element.EarlyOut,
                AttendanceType: element.AttendanceType,
                Hours: element.Hours,
                OverTimeHours: element.OverTimeHours
              });
            });
          } else if (data.StatusCode === 400)
            this.toastr.error(data.Message);

          this.monthlyAttendanceReportInfoLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.monthlyAttendanceReportInfoLoading = false;
        }
      );
  }
  //#endregion
}
