import { Component, OnInit } from '@angular/core';
import { HrService } from '../hr.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BsModalService } from 'ngx-bootstrap';
import { GLOBAL } from '../../../shared/global';
import { CodeService } from '../../code/code.service';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';
import { applicationPages } from '../../../shared/application-pages-enum';
import { IAttendanceGroup } from '../../code/attendance-group-master/attendance-group-master.component';

@Component({
  selector: 'app-payroll-monthly-hours',
  templateUrl: './payroll-monthly-hours.component.html',
  styleUrls: ['./payroll-monthly-hours.component.css']
})
export class PayrollMonthlyHoursComponent implements OnInit {
  addPayrollMonthlyHoursPopupVisible = false;
  showFilterRow: boolean;
  monthlyhoursDatasource: any[];
  monthlyhoursdata: any;
  officelistdt: any[];
  setPayrollHoursFilter: any; // form filter
  editPayrollMonthlyHoursPopupVisible = false;
  editPayrollMonthlyPopupLoading = false;
  isEditingAllowed = false;
  attendanceGroupList: IAttendanceGroup[];

  // loader
  addPayrollMonthlyPopupLoading: boolean;
  payrollMonthlyHourLoader = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private hrservice: HrService,
    private codeservice: CodeService,
    private commonService: CommonService
  ) {
    this.showFilterRow = true;
  }

  ngOnInit() {
    // this.getPayrollMonthlyHoursList();
    this.getAllOfficeList();
    this.getAttendanceGroupList();
    this.initializeForm();
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.PayrollDailyHours
    );
  }

  initializeForm() {
    this.monthlyhoursdata = {
      OfficeId: null,
      Hours: null,
      Date: null,
      TotalWorkingHours: 0,
      AttendanceGroupId: null
    };
    this.setPayrollHoursFilter = {};
  }

  //#region "getPayrollMonthlyHoursList"
  //getPayrollMonthlyHoursList() {
  //  this.hrservice
  //    .GetAllDropdown(
  //      this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllPayrollMonthlyHourDetail
  //    )
  //    .subscribe(
  //      data => {
  //        this.monthlyhoursDatasource = [];
  //        data.data.PayrollMonthlyHourList.forEach(element => {
  //          this.monthlyhoursDatasource.push({
  //            PayrollMonthlyHourID: element.PayrollMonthlyHourID,
  //            OfficeId: element.OfficeId,
  //            OfficeName: element.OfficeName,
  //            PayrollMonth: element.PayrollMonth,
  //            PayrollYear: element.PayrollYear,
  //            WorkingTime: element.WorkingTime,
  //            PayrollMonthYear: new Date(
  //              element.PayrollYear,
  //              element.PayrollMonth,
  //              0
  //            ),
  //            Hours: element.Hours,
  //            InTime: new Date(
  //              new Date(element.InTime).getTime() -
  //                new Date().getTimezoneOffset() * 60000
  //            ),
  //            OutTime: new Date(
  //              new Date(element.OutTime).getTime() -
  //                new Date().getTimezoneOffset() * 60000
  //            )
  //          });
  //        });
  //        this.commonService.setLoader(false);
  //      },
  //      error => {
  //        if (error.StatusCode === 500) {
  //          this.toastr.error('Internal Server Error....');
  //        } else if (error.StatusCode === 401) {
  //          this.toastr.error('Unauthorized Access Error....');
  //        } else if (error.StatusCode === 403) {
  //          this.toastr.error('Forbidden Error....');
  //        } else {
  //        }
  //      }
  //    );
  //}
  //#endregion

  //#region "getPayrollHoursList"
  getPayrollHoursList(filterData: any) {
    const filteredData = {
      Year: new Date(filterData.Year).getFullYear(),
      OfficeId: filterData.OfficeId,
      AttendanceGroupId: filterData.AttendanceGroupId
    };

    this.showPayrollMonthlyHourLoader();

    this.hrservice
      .AddByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_HR_GetAllPayrollMonthlyHourDetailFilter,
        filteredData
      )
      .subscribe(
        data => {
          this.monthlyhoursDatasource = [];
          data.data.PayrollMonthlyHourList.forEach(element => {
            this.monthlyhoursDatasource.push({
              PayrollMonthlyHourID: element.PayrollMonthlyHourID,
              OfficeId: element.OfficeId,
              OfficeName: element.OfficeName,
              PayrollMonth: element.PayrollMonth,
              PayrollYear: element.PayrollYear,
              WorkingTime: element.WorkingTime,
              AttendanceGroupId: element.AttendanceGroupId,
              PayrollMonthYear: new Date(
                element.PayrollYear,
                element.PayrollMonth,
                0
              ),
              Hours: element.Hours,
              InTime: new Date(
                new Date(element.InTime).getTime() -
                  new Date().getTimezoneOffset() * 60000
              ),
              OutTime: new Date(
                new Date(element.OutTime).getTime() -
                  new Date().getTimezoneOffset() * 60000
              )
            });
          });
          this.hidePayrollMonthlyHourLoader();
        },
        error => {
          this.hidePayrollMonthlyHourLoader();
        }
      );
  }
  //#endregion

  //#region "getAllOfficeList"
  getAllOfficeList() {
    this.codeservice
      .GetAllCodeList(this.setting.getBaseUrl() + GLOBAL.API_AllOffice_URL)
      .subscribe(
        data => {
          this.officelistdt = [];
          data.data.OfficeDetailsList.forEach(element => {
            this.officelistdt.push({
              OfficeId: element.OfficeId,
              OfficeName: element.OfficeName
            });
          });
          this.commonService.sortDropdown(this.officelistdt, 'OfficeName');
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

  getAttendanceGroupList() {
    this.codeservice.GetAllDetails(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAttendanceGroups)
      .subscribe(data => {
        this.attendanceGroupList = [];
        if (data.StatusCode === 200) {
          if (data.data.AttendanceGroupMasterList.length > 0
            || data.data.AttendanceGroupMasterList !== undefined
            || data.data.AttendanceGroupMasterList !== null) {
            data.data.AttendanceGroupMasterList.forEach(element => {
              this.attendanceGroupList.push(element);
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

  //#region "onFormSubmit"
  onFormSubmit(model, saveForAllOffice: boolean) {
    this.AddPayrollHoursDetail(model, saveForAllOffice);
  }
  //#endregion

  //#region "AddPayrollHoursDetail"
  AddPayrollHoursDetail(model, saveForAllOffice) {
    this.showHidePayrollMonthlyPopupLoading();
    const dataModel = {
      PayrollMonthlyHourID: 0,
      OfficeId: model.OfficeId,
      PayrollMonth: new Date(model.Date).getMonth() + 1,
      PayrollYear: new Date(model.Date).getFullYear(),
      WorkingTime: model.WorkingTime,
      InTime: new Date(new Date(model.InTime).getFullYear(), new Date(model.InTime).getMonth() + 1, model.InTime.getDate(),
      model.InTime.getHours(), model.InTime.getMinutes(), 0),
      OutTime: new Date(new Date(model.OutTime).getFullYear(), new Date(model.OutTime).getMonth() + 1,
      model.OutTime.getDate(), model.OutTime.getHours(), model.OutTime.getMinutes(), 0),
      // InTime: new Date(model.InTime),
      // OutTime: new Date(model.OutTime),
      SaveForAllOffice: saveForAllOffice,
      AttendanceGroupId: model.AttendanceGroupId
    };

    this.hrservice
      .AddEditPayrollMonthlyHour(
        this.setting.getBaseUrl() + GLOBAL.API_EmployeeHR_AddPayrollMonthlyHourDetail,
        dataModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Office Hours Added Successfully!!!');

            this.showHidePopup();
            this.showHidePayrollMonthlyPopupLoading();
          }
          if (data.StatusCode === 900) {
            this.toastr.error(data.Message);

            this.showHidePopup();
            this.showHidePayrollMonthlyPopupLoading();
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.getPayrollHoursList(this.setPayrollHoursFilter);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.showHidePayrollMonthlyPopupLoading();
        }
      );
  }
  //#endregion

  //#region "onEditMonthlyhoursClick"
  onEditMonthlyhoursClick(data) {
    this.monthlyhoursdata = {
      PayrollMonthlyHourID: data.PayrollMonthlyHourID,
      AttendanceGroupId: data.AttendanceGroupId,
      OfficeId: data.OfficeId,
      Date:
        data.PayrollMonth != null && data.PayrollYear != null
          ? new Date(data.PayrollYear, data.PayrollMonth - 1)
          : null,
      WorkingTime: data.WorkingTime,
      InTime: new Date(new Date(data.InTime).getFullYear(), new Date(data.InTime).getMonth() + 1, data.InTime.getDate(),
      data.InTime.getHours(), data.InTime.getMinutes(), 0),
      OutTime: new Date(new Date(data.OutTime).getFullYear(), new Date(data.OutTime).getMonth() + 1, data.OutTime.getDate(),
      data.OutTime.getHours(), data.OutTime.getMinutes(), 0)
      // InTime: new Date(data.InTime),
      // OutTime: new Date(data.OutTime)
    };
    this.showHideEditPopup();
  }
  //#endregion

  //#region "onEditFormSubmit"
    onEditFormSubmit(model) {
    this.showHideEditPayrollMonthlyPopupLoading();
    const dataModel = {
      PayrollMonthlyHourID: model.PayrollMonthlyHourID,
      OfficeId: model.OfficeId,
      AttendanceGroupId: model.AttendanceGroupId,
      PayrollMonth: new Date(model.Date).getMonth() + 1,
      PayrollYear: new Date(model.Date).getFullYear(),
      WorkingTime: model.WorkingTime,
      InTime: new Date(model.InTime),
      OutTime: new Date(model.OutTime)
    };

    this.hrservice
      .AddEditPayrollMonthlyHour(
        this.setting.getBaseUrl() + GLOBAL.API_HR_EditPayrollMonthlyHourDetail,
        dataModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Office Hours Updated Successfully!!!');

            this.showHideEditPopup();
          }
          if (data.StatusCode === 900) {
            this.toastr.error(data.Message);

            this.showHideEditPopup();
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
          this.showHideEditPayrollMonthlyPopupLoading();
          this.getPayrollHoursList(this.setPayrollHoursFilter);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.showHideEditPayrollMonthlyPopupLoading();
        }
      );
  }
  //#endregion

  //#region "onPayrollHoursFilter"
  onPayrollHoursFilter() {
    if (this.setPayrollHoursFilter.Year !== undefined && this.setPayrollHoursFilter.OfficeId !== undefined
      && this.setPayrollHoursFilter.AttendanceGroupId !== undefined) {
      this.getPayrollHoursList(this.setPayrollHoursFilter);
    } else {
      this.toastr.warning('Year, Office or Attendance Group Empty!!!');
    }
  }
  //#endregion

  //#region "showHidePopup"
  showHidePopup() {
    this.monthlyhoursdata = {};
    this.addPayrollMonthlyHoursPopupVisible = !this
      .addPayrollMonthlyHoursPopupVisible;
  }
  showHideEditPopup() {
    this.editPayrollMonthlyHoursPopupVisible = !this
      .editPayrollMonthlyHoursPopupVisible;
  }
  //#endregion

  //#region "addPayrollMonthlyPopupLoading"
  showHidePayrollMonthlyPopupLoading() {
    this.addPayrollMonthlyPopupLoading = !this.addPayrollMonthlyPopupLoading;
  }
  //#endregion

  //#region "addPayrollMonthlyPopupLoading"
  showHideEditPayrollMonthlyPopupLoading() {
    this.editPayrollMonthlyPopupLoading = !this.editPayrollMonthlyPopupLoading;
  }
  //#endregion

  //#region "showHideLoader"
  showPayrollMonthlyHourLoader() {
    this.payrollMonthlyHourLoader = true;
  }
  hidePayrollMonthlyHourLoader() {
    this.payrollMonthlyHourLoader = false;
  }
  //#endregion
}
