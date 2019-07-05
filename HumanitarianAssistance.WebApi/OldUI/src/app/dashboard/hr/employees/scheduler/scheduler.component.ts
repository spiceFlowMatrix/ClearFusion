import { Component, OnInit, ViewChild, Input } from '@angular/core';
import {
  Employee,
  SchedulerService,
  SchedulerData,
  Priority
} from './scheduler.service';

import { GLOBAL } from '../../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { DxSchedulerComponent } from 'devextreme-angular';
import { HrService } from '../../hr.service';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-scheduler',
  templateUrl: './scheduler.component.html',
  styleUrls: ['./scheduler.component.css']
})
export class SchedulerComponent implements OnInit {
  // NOTE: "scheduler" keyword IS NOT AN ID (in html)
  @ViewChild(DxSchedulerComponent) scheduler: DxSchedulerComponent;
  @Input() employeeId: number;

  dataSource: any;
  currentDate: Date = new Date();
  isDouble = 0;
  resourcesDataSource: Employee[];
  attendanceTypeDropdown: any[];
  schedularSelectedDate = new Date();

  // TODO: Show / Hide Edit button
  editBtnTooltipVisible = false;
  hideDateVisible: boolean;

  // Don't change its id and sequence (refered in another code)
  prioritiesData: Priority[] = [
    {
      text: 'Present',
      id: 1,
      color: '#007E33' // Green
    },
    {
      text: 'Absent',
      id: 2,
      color: '#bf360c' // Red
    },
    {
      text: 'Leave',
      id: 3,
      color: '#ef6c00' // Orange
    },
    {
      text: 'Holiday',
      id: 4,
      color: '#0099CC' // Blue
    }
  ];

  schedulerData: SchedulerData[];
  addAttendanceFormData: AddAttendanceFormData;
  popupAddAttendanceVisible = false;

  // loader
  schedulerInfoLoading = false;
  addAttendancePopupLoading = false;

  constructor(
    private schedulerservice: SchedulerService,
    private hrService: HrService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.attendanceTypeDropdown = this.hrService
      .getAllAttendanceType()
      .filter(x => x.AttendanceTypeId !== 3 && x.AttendanceTypeId !== 4);
    this.initializeForm();
    this.GetEmployeeAttendanceDetails(this.employeeId);
  }

  initializeForm() {
    this.addAttendanceFormData = {
      InTime: null,
      OutTime: null,
      Date: null,
      EmployeeId: 0,
      AttendanceTypeId: 0,
      AttendanceId: 0
    };
  }

  //#region "Scheduler events"
  onCellClick(event) {
    event.cancel = true;
  }

  onContentReady(event) {}

  onAppointmentDblClick(event) {
    event.cancel = true;
  }

  onAppointmentRendered(event) {
    event.cancel = true;
  }

  onAppointmentClick(event) {
    // TODO: To restrict editing for Leave, Holiday
    this.hideDateVisible =
      event.targetedAppointmentData.priority !== 1 &&
      event.targetedAppointmentData.priority !== 2
        ? true
        : false;
  }

  //#endregion

  //#region "Get All Attendance"
  GetEmployeeAttendanceDetails(employeeId) {
    this.schedulerInfoLoading = true;

    const employeeFilter = {
      EmployeeId: employeeId,
      Month:
        this.schedularSelectedDate != null
          ? new Date(this.schedularSelectedDate).getMonth() + 1
          : new Date().getMonth() + 1,
      Year:
        this.schedularSelectedDate != null
          ? new Date(this.schedularSelectedDate).getFullYear()
          : new Date().getFullYear()
    };

    this.schedulerservice
      .GetEmployeeAttendanceDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetEmployeeAttendanceDetails,
        employeeFilter
      )
      .subscribe(
        data => {
          this.schedulerData = [];
          if (
            data.StatusCode === 200 &&
            data.data.DisEmployeeAttendanceList.length > 0
          ) {
            data.data.DisEmployeeAttendanceList.forEach(element => {
              if (element.text === 'A') {
                this.schedulerData.push({
                  text: 'A',
                  attendanceId: element.attendanceId,
                  employeeID: element.employeeID,
                  overTimeHours:
                    element.OverTimeHours != null
                      ? element.OverTimeHours.toString() + ' h'
                      : '',
                  startDate: new Date(
                    new Date(element.startDate).getFullYear(),
                    new Date(element.startDate).getMonth(),
                    new Date(element.startDate).getDate(),
                    0,
                    0,
                    0,
                    0
                  ),
                  endDate: new Date(
                    new Date(element.endDate).getFullYear(),
                    new Date(element.endDate).getMonth(),
                    new Date(element.endDate).getDate(),
                    0,
                    0,
                    0,
                    0
                  ),
                  priority: 2
                });
              } else if (element.text === 'P') {
                this.schedulerData.push({
                  text: element.text,
                  attendanceId: element.attendanceId,
                  employeeID: element.employeeID,
                  overTimeHours:
                    element.OverTimeHours != null
                      ? element.OverTimeHours.toString() + ' h'
                      : '',
                  startDate: new Date(
                    new Date(element.startDate).getTime() -
                      new Date(element.startDate).getTimezoneOffset() * 60000
                  ),
                  endDate: new Date(
                    new Date(element.endDate).getTime() -
                      new Date(element.endDate).getTimezoneOffset() * 60000
                  ),
                  priority: 1
                });
              } else if (element.text === 'L') {
                // L
                this.schedulerData.push({
                  text: element.text,
                  attendanceId: element.attendanceId,
                  employeeID: element.employeeID,
                  startDate: new Date(
                    new Date(element.startDate).getTime() -
                      new Date(element.startDate).getTimezoneOffset() * 60000
                  ),
                  endDate: new Date(
                    new Date(element.endDate).getTime() -
                      new Date(element.endDate).getTimezoneOffset() * 60000
                  ),
                  priority: 3
                });
              } else {
                // H
                this.schedulerData.push({
                  text: element.text,
                  attendanceId: element.attendanceId,
                  employeeID: element.employeeID,
                  startDate: new Date(
                    new Date(element.startDate).getTime() -
                      new Date(element.startDate).getTimezoneOffset() * 60000
                  ),
                  endDate: new Date(
                    new Date(element.endDate).getTime() -
                      new Date(element.endDate).getTimezoneOffset() * 60000
                  ),
                  priority: 4
                });
              }
            });
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.schedulerInfoLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
  }
  //#endregion

  //#region "Edit Attendance"
  editAttendanceByEmployeeId(data: AddAttendanceFormData) {
    this.addAttendancePopupLoading = true;
    const attendenceData = {
      AttendanceId: data.AttendanceId,
      AttendanceTypeId: data.AttendanceTypeId,
      EmployeeId: data.EmployeeId,
      Date: new Date(
        data.InTime.getFullYear(),
        data.InTime.getMonth(),
        data.InTime.getDate(),
        data.InTime.getHours(),
        data.InTime.getMinutes(),
        data.InTime.getSeconds()
      ).toUTCString(),

      InTime: new Date(
        data.InTime.getFullYear(),
        data.InTime.getMonth(),
        data.InTime.getDate(),
        data.InTime.getHours(),
        data.InTime.getMinutes(),
        data.InTime.getSeconds()
      ).toUTCString(),

      OutTime: new Date(
        data.OutTime.getFullYear(),
        data.OutTime.getMonth(),
        data.OutTime.getDate(),
        data.OutTime.getHours(),
        data.OutTime.getMinutes(),
        data.OutTime.getSeconds()
      ).toUTCString(),
      LeaveStatus: data.AttendanceTypeId !== 3 ? true : false,
      OfficeId: localStorage.getItem('EMPLOYEEOFFICEID')
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
            this.toastr.success('Attendance Marked Successfully!!!');
            this.GetEmployeeAttendanceDetails(this.employeeId);
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.hideToolTip();
          this.HideAddAttendancePopup();
          this.addAttendancePopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideToolTip();
          this.GetEmployeeAttendanceDetails(this.employeeId);
          this.HideAddAttendancePopup();
        }
      );
  }
  //#endregion

  onFormSubmit(data: AddAttendanceFormData) {
    if (data.AttendanceTypeId === 0) {
      // this.addAttendanceByEmployeeId(data);
    // tslint:disable-next-line:curly
    } else this.editAttendanceByEmployeeId(data);
  }

  HideAddAttendancePopup() {
    this.popupAddAttendanceVisible = false;
  }

  showEditAttendancePopup(data: any) {
    this.addAttendanceFormData = {
      InTime: new Date(data.startDate),
      OutTime: new Date(data.endDate),
      AttendanceTypeId: data.priority,
      Date: new Date(data.startDate),
      EmployeeId: data.employeeID,
      AttendanceId: data.attendanceId
    };
    this.popupAddAttendanceVisible = true;
  }

  hideToolTip() {
    this.scheduler.instance.hideAppointmentTooltip();
  }
  //#endregion

  //#region "calendar Properties"
  // tslint:disable-next-line:member-ordering
  static isWeekEnd(date) {
    const day = date.getDay();
    return day === 0 || day === 6;
  }

  dataCellTemplate(cellData, index, container) {
    const employeeID = cellData.groups.employeeID,
      dataCellElement = container,
      currentTraining = 'abs-background';

    if (SchedulerComponent.isWeekEnd(cellData.startDate)) {
      dataCellElement.classList.add('employee-weekend-' + employeeID);
    }

    const element = document.createElement('div');

    element.classList.add(
      'day-cell',
      currentTraining,
      'employee-' + employeeID
    );
    element.textContent = cellData.text;

    return element;
  }
  //#endregion

  onOptionChangedEvent(e) {
    if (
      e.name === 'currentDate' &&
      e.name !== true &&
      e.fullName !== 'dataSource' &&
      e.value != null &&
      e.value.length !== 0
    ) {
      this.schedularSelectedDate = e.value;
      this.GetEmployeeAttendanceDetails(this.employeeId);
    }
  }
}

export interface AddAttendanceFormData {
  InTime: Date;
  OutTime: Date;
  Date?: Date;
  AttendanceTypeId?: number;
  EmployeeId?: number;
  AttendanceId?: number;

  hideDateVisible?: boolean;
}
