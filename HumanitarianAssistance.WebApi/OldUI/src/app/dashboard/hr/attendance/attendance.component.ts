import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HrService, EmployeeAttendanceList } from '../hr.service';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';
import { IAttendanceGroup } from '../../code/attendance-group-master/attendance-group-master.component';
import { CodeService } from '../../code/code.service';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.css']
})
export class AttendanceComponent implements OnInit {
  //#region Variables
  selectedItems: any;
  employeeAttendanceList: EmployeeAttendanceList[];
  updatingEmployeeAttendanceList: EmployeeAttendanceList[];
  attendanceTypeDropdown: any[];
  isEditingAllowed = false;

  // flag
  isAttandanceMarked = false;
  ispreviousAttendanceMarked = false;

  // edit attendance
  employeePreviousAttendanceList: EmployeeAttendanceList[];
  employeeAttendance: EmployeeAttendanceList[];

  attendanceModifiedArray: any[];
  editAttendanceflag = 0;
  todaySetTime: Date;
  AttendanceGroupId: number = null;

  count = 0;

  selectedEmployeeList: any[];
  attendanceGroupList: IAttendanceGroup[];

  minDateValue = new Date();
  maxDateValue = new Date();
  currentDate = new Date();
  markAttendenceDate = new Date();
  previousAttendanceDate = new Date();
  holidaysDateList: any[];
  allMode: string;
  checkBoxesMode: string;

  // loader
  attendenceLoader = false;

  //#endregion

  constructor(
    private hrService: HrService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService,
    private codeService: CodeService
  ) {
    this.todaySetTime = new Date();
    this.allMode = 'allPages';
    this.checkBoxesMode = 'onClick';
    this.todaySetTime = new Date(
      this.todaySetTime.getFullYear(),
      this.todaySetTime.getMonth(),
      this.todaySetTime.getDate(),
      0,
      0,
      0
    );
  }

  ngOnInit() {
    this.getAttendanceGroupList();
    this.getCurrentFinancialYear();
    this.getAllDateforDisableCalenderDate();
    this.attendanceTypeDropdown = this.hrService
      .getAllAttendanceType()
      .filter(x => x.AttendanceTypeId === 1 || x.AttendanceTypeId === 2);
    this.commonService.getEmployeeOfficeId().subscribe(data => {
      this.getMarkAttendenceDate(this.markAttendenceDate, false, this.AttendanceGroupId);
      this.getAllDateforDisableCalenderDate();
    });

    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.Attendance
    );
  }

  //#region Get Mark Attandance By Date
  getMarkAttendenceDate(e, attendanceStaus: boolean, attendanceGroupId: number) {
    this.attendenceLoader = true;

    const model = {
      SelectedDate : e,
// tslint:disable-next-line: radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
      AttendanceStatus: attendanceStaus,
      AttendanceGroupId: attendanceGroupId
    };
    this.isAttandanceMarked = false; // for toggle
    this.hrService
      .GetAllActiveEmployeeForAttendanceByDate(
        this.setting.getBaseUrl() +
        GLOBAL.API_Hr_GetAllEmployeesAttendanceByDate, model
      )
      .subscribe(
        data => {
          this.employeeAttendanceList = [];

          if (data.data.AttendanceStatus === false) {
            // tslint:disable-next-line:no-unused-expression
            this.isAttandanceMarked === false; // Attendance Already marked
          } else if (
            data.StatusCode === 200 &&
            data.data.EmployeeAttendanceList != null
          ) {
            this.isAttandanceMarked = true;

            data.data.EmployeeAttendanceList.forEach(element => {
              this.employeeAttendanceList.push({
                EmployeeId: element.EmployeeId,
                AttendanceId: element.AttendanceId,
                EmployeeName: element.EmployeeName,
                EmployeeCode: element.EmployeeCode,
                AttendanceTypeId:
                  element.LeaveStatus === true ? 3 : element.AttendanceTypeId,

                InTime: new Date(
                  new Date(element.InTime).getTime() -
                  new Date().getTimezoneOffset() * 60000
                ),
                OutTime: new Date(
                  new Date(element.OutTime).getTime() -
                  new Date().getTimezoneOffset() * 60000
                ),
                Date: this.todaySetTime.toLocaleString(),
                TotalWorkTime: element.TotalWorkTime,
                LeaveStatus: element.LeaveStatus,
                OfficeId: element.OfficeId
              });
            });
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.attendenceLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.attendenceLoader = false;
        }
      );
  }//#endregion

  onDateSelectionChange(e, attendanceStatus) {
    if (!attendanceStatus) {
      this.markAttendenceDate = e;
      this.getMarkAttendenceDate(this.markAttendenceDate, attendanceStatus, this.AttendanceGroupId);
    } else {
      this.previousAttendanceDate = e;
      this.getPreviousAttendenceDate(e, attendanceStatus, this.AttendanceGroupId);
    }
  }

  onAttendanceGroupChange(e) {
    this.AttendanceGroupId = e;
    this.getMarkAttendenceDate(this.markAttendenceDate, false, this.AttendanceGroupId);
  }

  //#region Get Previous Attendance
  getPreviousAttendenceDate(e, attendanceStaus: boolean = true, attendanceGroupId: number) {
    this.attendenceLoader = true;
    const model = {
      SelectedDate : e,
// tslint:disable-next-line: radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
      AttendanceStatus: attendanceStaus,
      AttendanceGroupId: attendanceGroupId
    };
    this.hrService
      .GetAllActiveEmployeeForAttendanceByDate(
        this.setting.getBaseUrl() +
        GLOBAL.API_Hr_GetAllEmployeesAttendanceByDate,
        model
      )
      .subscribe(
        data => {
          this.employeePreviousAttendanceList = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeAttendanceList != null
          ) {
            if (data.data.AttendanceStatus === true) {
              this.ispreviousAttendanceMarked = true;

              data.data.EmployeeAttendanceList.forEach(element => {
                this.employeePreviousAttendanceList.push({
                  EmployeeId: element.EmployeeId,
                  AttendanceId: element.AttendanceId,
                  EmployeeName: element.EmployeeName,
                  EmployeeCode: element.EmployeeCode,
                  AttendanceTypeId:
                    element.LeaveStatus === true ? 3 : element.AttendanceTypeId,
                  InTime:
                    element.AttendanceTypeId === 2
                      ? new Date(element.InTime).getTime()
                      : new Date(element.InTime).setTime(
                        new Date(element.InTime).getTime() -
                        this.previousAttendanceDate.getTimezoneOffset() *
                        60000
                      ),
                  OutTime:
                    element.AttendanceTypeId === 2
                      ? new Date(element.InTime).getTime()
                      : new Date(element.OutTime).setTime(
                        new Date(element.OutTime).getTime() -
                        this.previousAttendanceDate.getTimezoneOffset() *
                        60000
                      ),
                  Date: new Date(
                    new Date(this.previousAttendanceDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                  ),
                  TotalWorkTime: element.TotalWorkTime,
                  LeaveStatus: element.LeaveStatus,
                  OfficeId: element.OfficeId
                });
              });
            } else {
              this.ispreviousAttendanceMarked = false;
            }
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.attendenceLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.attendenceLoader = false;
        }
      );
  }
  //#endregion

  //#region Edit Employee Attendance
  onAttendenceChangeEmployeeById(data) {
    const value = Object.assign(data.oldData, data.newData); // Merge old data with new Data

    const attendenceData = {
      AttendanceId: value.AttendanceId,
      AttendanceTypeId: value.AttendanceTypeId,
      EmployeeId: value.EmployeeId,
      Date: new Date(
        this.previousAttendanceDate.getFullYear(),
        this.previousAttendanceDate.getMonth(),
        this.previousAttendanceDate.getDate(),
        new Date().getHours(),
        new Date().getMinutes()
      ).toUTCString(),

      InTime: new Date(
        new Date(value.Date).getFullYear(),
        new Date(value.Date).getMonth(),
        new Date(value.Date).getDate(),
        new Date(value.InTime).getHours(),
        new Date(value.InTime).getMinutes()
      ).toUTCString(),
      OutTime: new Date(
        new Date(value.Date).getFullYear(),
        new Date(value.Date).getMonth(),
        new Date(value.Date).getDate(),
        new Date(value.OutTime).getHours(),
        new Date(value.OutTime).getMinutes()
      ).toUTCString(),
      LeaveStatus: false,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
    };

    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_EditEmployeeAttendanceByDate,
        attendenceData
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully');
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getPreviousAttendenceDate(this.previousAttendanceDate, true, this.AttendanceGroupId);

          this.commonService.setLoader(false);
        }
      );
  }
  //#endregion


  //#region "Get Attendance Group List"
  getAttendanceGroupList() {
    this.codeService.GetAllDetails(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAttendanceGroups)
      .subscribe(data => {
        this.attendanceGroupList = [];
        if (data.StatusCode === 200) {
          if (data.data.AttendanceGroupMasterList.length > 0
            || data.data.AttendanceGroupMasterList !== undefined
            || data.data.AttendanceGroupMasterList !== null) {
            data.data.AttendanceGroupMasterList.forEach(element => {
              this.attendanceGroupList.push(element);
              this.AttendanceGroupId = this.attendanceGroupList[0].Id;
            });
          }
        } else {
          this.toastr.error(data.Message);
        }
      }, error => {
        if (error.StatusCode === 500) {
          this.toastr.error('Internal Server Error....');
        } else if (error.StatusCode === 401) {
          this.toastr.error('Unauthorized Access Error....');
        } else if (error.StatusCode === 403) {
          this.toastr.error('Forbidden Error....');
        }
      });
  }
  //#endregion



  //#region "Get All Holidays List Date"
  getAllDateforDisableCalenderDate() {
    this.hrService
      .GetAllDetail(
        this.setting.getBaseUrl() +
        GLOBAL.API_HR_GetAllDateforDisableCalenderDate,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          this.holidaysDateList = [];
          if (
            data.StatusCode === 200 &&
            data.data.ApplyLeaveList != null &&
            data.data.ApplyLeaveList.length > 0
          ) {
            data.data.ApplyLeaveList.forEach(element => {
              this.holidaysDateList.push(
                new Date(
                  new Date(element.Date).getFullYear(),
                  new Date(element.Date).getMonth(),
                  new Date(element.Date).getDate()
                )
              );
            });
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region "Get Current Financial Year"
  getCurrentFinancialYear() {
    this.hrService
      .GetAllDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Account_GetFinancialYearDetails,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          if (
            data.StatusCode === 200 &&
            data.data.CurrentFinancialYearList != null &&
            data.data.CurrentFinancialYearList.length > 0
          ) {
            data.data.CurrentFinancialYearList.forEach(element => {
              (this.minDateValue = new Date(
                new Date(element.StartDate).getTime() -
                new Date().getTimezoneOffset() * 60000
              )),
                (this.maxDateValue = new Date(
                  new Date(element.EndDate).getTime() -
                  new Date().getTimezoneOffset() * 60000
                ));
            });
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  saveEmployeeAttendance() {
    this.selectedEmployeeList.forEach(element => {
      element.EmployeeId = element.EmployeeId,
        element.InTime = new Date(
          this.markAttendenceDate.getFullYear(),
          this.markAttendenceDate.getMonth(),
          this.markAttendenceDate.getDate(),
          new Date(element.InTime).getHours(),
          new Date(element.InTime).getMinutes()
        ).toUTCString(),
        element.OutTime = new Date(
          this.markAttendenceDate.getFullYear(),
          this.markAttendenceDate.getMonth(),
          this.markAttendenceDate.getDate(),
          new Date(element.OutTime).getHours(),
          new Date(element.OutTime).getMinutes()
        ).toUTCString(),
        element.Date = this.markAttendenceDate.toUTCString(),
        element.AttendanceGroupId = this.AttendanceGroupId;
    });
    this.attendenceLoader = true;

    this.hrService
      .AddEmployeeAttendanceDetails(
        this.setting.getBaseUrl() + GLOBAL.API_HR_AddEmployeeAttendanceDetails,
        this.selectedEmployeeList
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully');
          }

          if (data.StatusCode === 400) {
            this.toastr.error(data.Message);
          }

          if (data.StatusCode === 900) {
            this.toastr.success(data.Message);
          }
          // tslint:disable-next-line:no-unused-expression
          this.isAttandanceMarked === false;
          this.getMarkAttendenceDate(this.markAttendenceDate, false, this.AttendanceGroupId);
          this.attendenceLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.attendenceLoader = false;
        }
      );
  }
  //#endregion

  //#region OnTabClick
  previousAttendanceTabClicked() {
    this.getPreviousAttendenceDate(this.previousAttendanceDate, true, this.AttendanceGroupId);
  }

  markAttendanceTabClicked() {
    this.getMarkAttendenceDate(this.markAttendenceDate, false, this.AttendanceGroupId);
  }
  //#endregion

  selectionChangedHandler(e) {
    this.selectedEmployeeList = e.selectedRowsData;
  }
}
