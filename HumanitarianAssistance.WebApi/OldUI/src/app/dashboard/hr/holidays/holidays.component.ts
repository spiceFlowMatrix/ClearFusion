import { Component, OnInit, ViewChild } from '@angular/core';
import { HrService } from '../hr.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';

import { DxSchedulerComponent } from 'devextreme-angular';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-holidays',
  templateUrl: './holidays.component.html',
  styleUrls: ['./holidays.component.css']
})
export class HolidaysComponent implements OnInit {
  // NOTE: "scheduler" keyword IS NOT AN ID (in html)
  @ViewChild(DxSchedulerComponent) scheduler: DxSchedulerComponent;

  isDouble = 0; // for double click

  popupHolidayVisible = false;
  popupDeleteHolidayVisible = false;
  popupHolidayWeeklyVisible = false;
  holidayFormWeeklyDataFlag = false;

  holidayFormData: HolidayData;
  holidayFormWeeklyData: HolidayFormWeeklyData;

  repeatWeeklyDay: RepeatWeeklyDay[];

  holidayData: any[];
  dataSource: any;
  currentDate = new Date();
  minDateValue = new Date();
  maxDateValue = new Date();

  cellData: any;

  holidayToggleRadioButton = ['Day wise', 'Repeat Weekly Day'];

  // loader
  addHolidayPopupLoading = false;
  holidayInfoLoading = false;
  deleteHolidayPopupLoading = false;
  holidayWeeklyPopupLoading = false;

  constructor(
    private hrService: HrService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) {}

  flag = false;
  ngOnInit() {
    this.initializeForm();
    this.getCurrentFinancialYear();
    this.getAllHolidays();

    if (this.flag === false) {
      this.commonService.getEmployeeOfficeId().subscribe(data => {
        this.getAllHolidays();
      });
    }
  }

  initializeForm() {
    this.holidayFormData = {
      Date: new Date(),
      HolidayId: 0,
      HolidayName: null,
      Remarks: null
    };

    this.holidayFormWeeklyData = {
      Sun: false,
      Mon: false,
      Tue: false,
      Wed: false,
      Thu: false,
      Fri: false,
      Sat: false
    };
  }

  //#region "Scheduler events"
  onCellClick(event) {
    this.isDouble++;
    setTimeout(a => {
      if (this.isDouble === 2) {
        this.ShowAddHolidayPopup(event);
      }
      this.isDouble = 0;
    }, 300);
  }

  onContentReady(event) {}

  onAppointmentDblClick(event) {
    event.cancel = true;
  }

  onAppointmentRendered(event) {
    event.cancel = true;
    event.appointmentElement.style.backgroundColor =
      event.appointmentData.color;
  }

  onAppointmentClick(e) {}

  hideToolTip() {
    this.scheduler.instance.hideAppointmentTooltip();
  }

  //#endregion

  //#region "Get All Holidays"
  getAllHolidays() {
    this.commonService.setLoader(true);
    this.hrService
      .GetAllDetailsByOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_GetAllHolidayDetails,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          this.holidayData = [];
          if (
            data.StatusCode === 200 &&
            data.data.HolidayDetailsList != null &&
            data.data.HolidayDetailsList.length > 0
          ) {
            data.data.HolidayDetailsList.forEach(element => {
              this.holidayData.push({
                HolidayName: element.HolidayName,
                HolidayId: element.HolidayId,
                startDate: new Date(
                  new Date(element.Date).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ), // mandatory
                endDate: new Date(
                  new Date(element.Date).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ), // mandatory
                Remarks: element.Remarks,
                color: '#0099CC'
              });
            });
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.commonService.setLoader(false);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.commonService.setLoader(false);
        }
      );
  }
  //#endregion

  //#region "Add holiday"
  addHolidayDate(data: any) {
    // tslint:disable-next-line:curly
    if (data.HolidayType === 1) this.addHolidayPopupLoading = true;
    // tslint:disable-next-line:curly
    else if (data.HolidayType === 2) this.holidayWeeklyPopupLoading = true;

    const holidayModel = {
      Date:
        data.Date == null || ''
          ? new Date()
          : new Date(
              data.Date.getFullYear(),
              data.Date.getMonth(),
              data.Date.getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
      HolidayId: data.HolidayId,
      HolidayName: data.HolidayName,
      Remarks: data.Remarks,
      HolidayType: data.HolidayType,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
      RepeatWeeklyDay: data.RepeatWeeklyDay
    };

    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_AddHolidayDetails,
        holidayModel
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully!!!');
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.HideAddHolidayPopup();
          this.addHolidayPopupLoading = false;
          this.HideAddHolidayWeeklyPopup();

          this.holidayWeeklyPopupLoading = false;
          this.getAllHolidays();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          // tslint:disable-next-line:curly
          } else this.toastr.error('Something went wrong!!!');
          this.getAllHolidays();
          this.addHolidayPopupLoading = false;
          this.HideAddHolidayWeeklyPopup();
          this.holidayWeeklyPopupLoading = false;
        }
      );
  }
  //#endregion

  //#region Edit Holiday
  editHolidayDate(data: any) {
    // tslint:disable-next-line:curly
    if (data.HolidayType === 1) this.addHolidayPopupLoading = true;
    // tslint:disable-next-line:curly
    if (data.HolidayType === 2) this.holidayWeeklyPopupLoading = true;

    let holidayModel;
    if (data.HolidayId === 0 || data.HolidayId == null || data.Date == null) {
      holidayModel = {
        Date:
          data.Date == null || ''
            ? new Date()
            : new Date(
                data.Date.getFullYear(),
                data.Date.getMonth(),
                data.Date.getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              ),
        HolidayId: data.HolidayId,
        HolidayName: data.HolidayName,
        Remarks: data.Remarks,
        HolidayType: data.HolidayType,
        // tslint:disable-next-line:radix
        OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
        RepeatWeeklyDay: data.RepeatWeeklyDay
      };
    } else {
      holidayModel = {
        Date: new Date(
          data.Date.getFullYear(),
          data.Date.getMonth(),
          data.Date.getDate(),
          data.Date.getHours(),
          data.Date.getMinutes(),
          data.Date.getSeconds()
        ).toUTCString(),
        HolidayId: data.HolidayId,
        HolidayName: data.HolidayName,
        Remarks: data.Remarks,
        HolidayType: data.HolidayType,
        // tslint:disable-next-line:radix
        OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
        RepeatWeeklyDay: data.RepeatWeeklyDay
      };
    }

    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_EditHolidayDetails,
        holidayModel
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully');
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.hideToolTip();
          this.HideAddHolidayPopup();
          this.addHolidayPopupLoading = false;

          this.HideAddHolidayWeeklyPopup();
          this.holidayWeeklyPopupLoading = false;
          this.getAllHolidays();
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
          this.addHolidayPopupLoading = false;

          this.HideAddHolidayWeeklyPopup();
          this.getAllHolidays();
        }
      );
  }
  //#endregion

  //#region "Delete Holiday"
  DeleteHoliday(holidayId: number) {
    this.deleteHolidayPopupLoading = true;
    this.hrService
      .DeleteHolidayDetail(
        this.setting.getBaseUrl() + GLOBAL.API_HR_DeleteHolidayDetails,
        holidayId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Deleted Successfully!!!');
            this.getAllHolidays();
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.hideToolTip();
          this.HideDeleteHolidayPopup();
          this.deleteHolidayPopupLoading = false;
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
          this.getAllHolidays();
          this.HideDeleteHolidayPopup();
          this.deleteHolidayPopupLoading = false;
        }
      );
  }
  //#endregion

  //#region "Get All Holiday Weekly Details"
  getAllHolidayWeeklyDetails() {
    this.holidayWeeklyPopupLoading = true;

    this.holidayFormWeeklyData = {
      Sun: false,
      Mon: false,
      Tue: false,
      Wed: false,
      Thu: false,
      Fri: false,
      Sat: false
    };

    this.hrService
      .GetAllDetailsByOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllHolidayWeeklyDetails,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          if (
            data.StatusCode === 200 &&
            data.data.HolidayWeeklyDetailsList != null &&
            data.data.HolidayWeeklyDetailsList.length > 0
          ) {
            data.data.HolidayWeeklyDetailsList.forEach(element => {
              // tslint:disable-next-line:curly
              if (element.Day === 'Sunday')
                this.holidayFormWeeklyData.Sun = true;
              // tslint:disable-next-line:curly
              if (element.Day === 'Monday')
                this.holidayFormWeeklyData.Mon = true;
              // tslint:disable-next-line:curly
              if (element.Day === 'Tuesday')
                this.holidayFormWeeklyData.Tue = true;
              // tslint:disable-next-line:curly
              if (element.Day === 'Wednesday')
                this.holidayFormWeeklyData.Wed = true;
              // tslint:disable-next-line:curly
              if (element.Day === 'Thursday')
                this.holidayFormWeeklyData.Thu = true;
              // tslint:disable-next-line:curly
              if (element.Day === 'Friday')
                this.holidayFormWeeklyData.Fri = true;
              // tslint:disable-next-line:curly
              if (element.Day === 'Saturday')
                this.holidayFormWeeklyData.Sat = true;
            });
            this.holidayFormWeeklyDataFlag = true;
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
          else {
            this.holidayFormWeeklyDataFlag = false;
          }
          this.holidayWeeklyPopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.commonService.setLoader(false);
          this.holidayWeeklyPopupLoading = false;
        }
      );
  }

  //#endregion

  //#region "Get Current Financial Year"
  getCurrentFinancialYear() {
    this.hrService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Account_GetFinancialYearDetails
      )
      .subscribe(
        data => {
          if (
            data.StatusCode === 200 &&
            data.data.CurrentFinancialYearList != null &&
            data.data.CurrentFinancialYearList.length > 0
          ) {
            data.data.CurrentFinancialYearList.forEach(element => {
              this.minDateValue = new Date(
                new Date(element.StartDate).getTime() -
                  new Date().getTimezoneOffset() * 60000
              );
              this.maxDateValue = new Date(
                new Date(element.EndDate).getTime() -
                  new Date().getTimezoneOffset() * 60000
              );
            });
            // Set limit
            this.maxDateValue < this.currentDate
              ? (this.currentDate = this.maxDateValue)
              : (this.currentDate = new Date());
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

  //#region "Show / Hide"
  ShowEditHolidayPopup(data) {
    this.holidayFormData = {
      HolidayName: data.HolidayName,
      HolidayId: data.HolidayId,
      Date: data.startDate,
      Remarks: data.Remarks
    };
    this.popupHolidayVisible = true;
  }

  ShowDeleteHolidayPopup(data) {
    this.holidayFormData = {
      HolidayName: data.HolidayName,
      HolidayId: data.HolidayId,
      Date: data.startDate,
      Remarks: data.Remarks
    };
    this.popupDeleteHolidayVisible = true;
  }

  HideDeleteHolidayPopup() {
    this.popupDeleteHolidayVisible = false;
  }

  ShowAddHolidayPopup(event) {
    this.holidayFormData = {
      Date: event.cellData.startDate,
      HolidayId: 0,
      HolidayName: '',
      Remarks: ''
    };
    this.popupHolidayVisible = true;
  }

  HideAddHolidayPopup() {
    this.popupHolidayVisible = false;
  }

  ShowAddHolidayWeeklyPopup() {
    this.popupHolidayWeeklyVisible = true;
    this.getAllHolidayWeeklyDetails();
  }

  HideAddHolidayWeeklyPopup() {
    this.popupHolidayWeeklyVisible = false;
  }
  //#endregion

  onFormSubmit(holidayType: number, data: any) {
    // Date wise
    if (holidayType === 1) {
      const finalData: any = {
        HolidayId:
          data.HolidayId !== 0 || data.HolidayId != null || data.HolidayId !== ''
            ? data.HolidayId
            : 0,
        Date: data.Date,
        HolidayName: data.HolidayName,
        Remarks: data.Remarks,
        HolidayType: holidayType,
        RepeatWeeklyDay: this.repeatWeeklyDay
      };

      // tslint:disable-next-line:curly
      if (data.HolidayId === 0 || data.HolidayId == null)
        this.addHolidayDate(finalData);
      // tslint:disable-next-line:curly
      else this.editHolidayDate(finalData);
    }
    // Weekly Holidays
    // tslint:disable-next-line:one-line
    else if (holidayType === 2) {
      this.repeatWeeklyDay = [];
      if (data.Sun === true) {
        this.repeatWeeklyDay.push({
          Day: 'Sunday'
        });
      }
      if (data.Mon === true) {
        this.repeatWeeklyDay.push({
          Day: 'Monday'
        });
      }
      if (data.Tue === true) {
        this.repeatWeeklyDay.push({
          Day: 'Tuesday'
        });
      }
      if (data.Wed === true) {
        this.repeatWeeklyDay.push({
          Day: 'Wednesday'
        });
      }
      if (data.Thu === true) {
        this.repeatWeeklyDay.push({
          Day: 'Thursday'
        });
      }
      if (data.Fri === true) {
        this.repeatWeeklyDay.push({
          Day: 'Friday'
        });
      }
      if (data.Sat === true) {
        this.repeatWeeklyDay.push({
          Day: 'Saturday'
        });
      }

      if (this.holidayFormWeeklyDataFlag === false) {
        const finalData: any = {
          HolidayId: 0,
          HolidayType: holidayType,
          RepeatWeeklyDay: this.repeatWeeklyDay
        };
        this.addHolidayDate(finalData);
      } else {
        const finalData: any = {
          HolidayId: data.HolidayId,
          HolidayType: holidayType,
          RepeatWeeklyDay: this.repeatWeeklyDay
        };
        this.editHolidayDate(finalData);
      }
    }
  }
}

export interface HolidayData {
  HolidayId: number;
  HolidayName: string;
  Date?: Date;
  Remarks?: string;
}

export interface HolidayFormWeeklyData {
  Sun: boolean;
  Mon: boolean;
  Tue: boolean;
  Wed: boolean;
  Thu: boolean;
  Fri: boolean;
  Sat: boolean;
}

export interface RepeatWeeklyDay {
  Day: string;
}
