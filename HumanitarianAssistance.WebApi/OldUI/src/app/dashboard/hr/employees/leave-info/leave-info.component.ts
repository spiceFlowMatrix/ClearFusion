import { Component, OnInit, Input } from "@angular/core";
import { HrService } from "../../hr.service";
import { Router } from "@angular/router";
import { GLOBAL } from '../../../../shared/global';
import { ToastrService } from "ngx-toastr";
import {
  applicationPages,
  applicationModule
} from "../../../../shared/application-pages-enum";
import { AppSettingsService } from "../../../../service/app-settings.service";
import { CommonService } from "../../../../service/common.service";

@Component({
  selector: "app-leave-info",
  templateUrl: "./leave-info.component.html",
  styleUrls: ["./leave-info.component.css"]
})
export class LeaveInfoComponent implements OnInit {
  @Input() employeeId: number;
  @Input() tabEventValue: number;

  hiredOnDate: any;
  isEditingAllowed: boolean = false;

  leaveInfoDataSource: LeaveInfoModel[];
  leaveInfoData: LeaveInfoData;
  popupAssignLeaveVisible: boolean;
  leaveReasonId: number;
  currentDate = new Date();

  selectedDates: any[];
  disabledDates: any[];
  minDateValue = new Date();
  maxDateValue = new Date();

  financialYearDropdown: FinancialYearDropdown[];
  leaveReasonTypeDropdown: any;

  // edit Leave info
  editLeaveInfoFormData: LeaveInfoModel;
  PopupEditLeaveInfoVisible = false;

  // Assign Leave
  LeaveReasonIdBinding: number; // two way binding
  assignUnitIsValid = true;

  // Apply Leave
  popupApplyLeaveVisible = false;
  applyLeaveDataSource: ApplyLeaveModel;

  // Leave Details
  popupLeaveDetailsVisible = false;
  leavedetailsDataSource: LeaveDetailsModel[];

  // loader
  leaveInfoLoading = false;
  applyLeavePopupLoading: boolean;
  assignLeavePopupLoading: boolean;
  leaveDetailsPopupLoading: boolean;
  editAssignLeavePopupLoading = false;

  constructor(
    private hrService: HrService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) {
    this.hiredOnDate = new Date(localStorage.getItem('HIREDON'));
  }

  ngOnInit() {
    this.intializedata();
    // this.getAllHolidays();
    this.getAllDisableCalendarDate(this.employeeId);
    this.GetFinancialYearDropdown();
    this.getCurrentFinancialYear();
    this.GetLeaveReasonTypeDropdown();
    this.GetAllLeaveInfo(this.employeeId);
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.Employees
    );
  }

  intializedata() {
    this.leaveInfoData = {
      FinancialYearId: null,
      LeaveReasonId: null,
      Unit: null,
      AssignUnit: null,
      Description: null
    };

    // edit Leave Info
    this.editLeaveInfoFormData = {
      AssignUnit: null,
      BlanceLeave: null,
      LeaveReasonName: null,
      Unit: null
    };

    // Apply Leave
    this.applyLeaveDataSource = {
      FromDate: null,
      ToDate: null,
      LeaveReasonId: 0,
      Remarks: '',
      BlanceLeave: 0,
      LeaveReasonName: ''
    };
  }

  //#region "Get All Holidays & Already applied leave"
  getAllDisableCalendarDate(employeeId) {
    this.hrService
      .GetDataByEmployeeIdAndOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllDisableCalanderDate,
        employeeId,
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          this.disabledDates = [];
          if (
            data.StatusCode === 200 &&
            data.data.ApplyLeaveList != null &&
            data.data.ApplyLeaveList.length > 0
          ) {
            data.data.ApplyLeaveList.forEach(element => {
              this.disabledDates.push(
                new Date(
                  new Date(element.Date).getTime() -
                    new Date().getTimezoneOffset() * 60000
                )
              );
            });
          } else if (data.StatusCode === 400) this.toastr.warning(data.Message);
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

  //#region "Get All Financial Year"
  GetFinancialYearDropdown() {
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetCurrentFinancialYear
      )
      .subscribe(
        data => {
          this.financialYearDropdown = [];

          if (
            data.data.CurrentFinancialYearList != null &&
            data.data.CurrentFinancialYearList.length > 0
          ) {
            data.data.CurrentFinancialYearList.forEach(element => {
              this.financialYearDropdown.push({
                StartDate: new Date(
                  new Date(element.StartDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                EndDate: new Date(
                  new Date(element.EndDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                FinancialYearId: element.FinancialYearId,
                FinancialYearName: element.FinancialYearName
              });
            });
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
              (this.minDateValue = new Date(
                new Date(element.StartDate).getTime() -
                  new Date().getTimezoneOffset() * 60000
              )),
                (this.maxDateValue = new Date(
                  new Date(element.EndDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ));
            });
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

  //#region "Get All Leave Reason Type"
  GetLeaveReasonTypeDropdown() {
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_LeaveReasonType
      )
      .subscribe(
        data => {
          this.leaveReasonTypeDropdown = [];

          if (
            data.data.LeaveReasonList != null &&
            data.data.LeaveReasonList.length > 0
          ) {
            data.data.LeaveReasonList.forEach(element => {
              this.leaveReasonTypeDropdown.push(element);
            });
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

  //#region "Get All Leave Info"
  GetAllLeaveInfo(employeeId: number) {
    this.leaveInfoLoading = true;
    this.hrService
      .GetAllLeaveInfoById(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllEmployeeAssignLeave,
        employeeId
      )
      .subscribe(
        data => {
          this.leaveInfoDataSource = [];
          if (
            data.data.AssignLeaveToEmployeeList != null &&
            data.data.AssignLeaveToEmployeeList.length > 0
          ) {
            data.data.AssignLeaveToEmployeeList.forEach(element => {
              this.leaveInfoDataSource.push(element);
            });
          } else if (data.StatusCode === 400) this.toastr.warning(data.Message);

          this.getAllDisableCalendarDate(this.employeeId);
          this.leaveInfoLoading = false;
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

  //#region "Add Leave Info"
  onFormSubmit(model) {
    if (model.Unit < model.AssignUnit || model.AssignUnit <= 0) {
      this.toastr.error('Assign unit is Exceeded');
      this.assignUnitIsValid = false;
    } else {
      this.AddLeaveInfo(model);
    }
  }

  AddLeaveInfo(model: any) {
    this.assignLeavePopupLoading = true;

    model.EmployeeId = this.employeeId;
    model.LeaveReasonId = this.leaveReasonId;

    this.hrService
      .addLeaveInfo(
        this.setting.getBaseUrl() + GLOBAL.API_HR_AssignLeaveToEmployeeDetail,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully!!!');
            this.GetAllLeaveInfo(this.employeeId);
            this.hideAssignLeavePopup();
            this.getAllDisableCalendarDate(this.employeeId);
          } else if (data.StatusCode === 900) this.toastr.warning(data.Message);
          this.assignLeavePopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideAssignLeavePopup();
          this.assignLeavePopupLoading = false;
        }
      );
  }

  //#endregion

  //#region "Get All Unit Type"
  getUnitType(e: any) {
    this.leaveReasonId = e.value;
    const days = this.leaveReasonTypeDropdown.filter(
      a => a.LeaveReasonId === e.value
    );
    this.leaveInfoData.Unit = days[0].Unit;
    this.leaveInfoData.AssignUnit = days[0].Unit;
  }
  //#endregion

  //#region "Edit Leave Info"
  onEditLeaveInfoSubmit(data: LeaveInfoModel) {
    if (data.Unit < data.AssignUnit) {
      this.toastr.error('Assigned leave is exceeded');
    } else {
      this.editAssignLeavePopupLoading = true;

      const editLeaveInfoModel: LeaveInfoModel = {
        LeaveId: data.LeaveId,
        AssignUnit: data.AssignUnit,
        BlanceLeave: data.BlanceLeave,
        LeaveReasonName: data.LeaveReasonName,
        Unit: data.Unit
      };
      this.hrService
        .AddByModel(
          this.setting.getBaseUrl() + GLOBAL.API_HR_EditEmployeeAssignLeave,
          editLeaveInfoModel
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Updated successfully!!!');
              this.GetAllLeaveInfo(this.employeeId);
            } else if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');

            this.editAssignLeavePopupLoading = false;
            this.hideEditLeaveInfoPopup();
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
            }
            this.editAssignLeavePopupLoading = false;
            this.GetAllLeaveInfo(this.employeeId);
          }
        );
    }
  }
  //#endregion

  //#region "on Leave Applied"
  addapplyleaveinfo: any[];
  onLeaveApplied(model: ApplyLeaveModel) {
    this.applyLeavePopupLoading = true;

    if (this.selectedDates == null) {
      this.toastr.error('Please select date!');
      this.applyLeavePopupLoading = false;
    } else {
      if (this.selectedDates.length > model.BlanceLeave) {
        this.toastr.error('Can\'t apply more than balance leave.');
        this.selectedDates = null;
        this.applyLeavePopupLoading = false;
      } else {
        this.addapplyleaveinfo = [];
        this.selectedDates.forEach(element => {
          this.addapplyleaveinfo.push({
            FromDate: new Date(
              new Date(element).getFullYear(),
              new Date(element).getMonth(),
              new Date(element).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
            ToDate: new Date(
              new Date(element).getFullYear(),
              new Date(element).getMonth(),
              new Date(element).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
            LeaveReasonId: model.LeaveReasonId,
            Remarks: model.Remarks,
            LeaveReasonName: model.LeaveReasonName,
            BlanceLeave: model.BlanceLeave,
            EmployeeId: this.employeeId
          });
        });

        this.hrService
          .AddByModel(
            this.setting.getBaseUrl() +
              GLOBAL.API_HR_AddEmployeeApplyLeaveDetail,
            this.addapplyleaveinfo
          )
          .subscribe(
            data => {
              if (data.StatusCode === 200) {
                this.toastr.success('Added Successfully!!!');
                this.hideApplyLeavePopup();
                this.intializedata();
                this.GetAllLeaveInfo(this.employeeId);
              } else if (data.StatusCode === 900) {
                this.toastr.warning(data.Message);
              } else if (data.StatusCode === 400)
                this.toastr.error('Something went wrong!');

              this.selectedDates = null;
              this.applyLeavePopupLoading = false;
            },
            error => {
              if (error.StatusCode === 500) {
                this.toastr.error('Internal Server Error....');
              } else if (error.StatusCode === 400) {
                this.toastr.error('Something Went Wrong....');
              } else if (error.StatusCode === 401) {
                this.toastr.error('Unauthorized Access Error....');
              } else if (error.StatusCode === 403) {
                this.toastr.error('Forbidden Error....');
              }
              this.selectedDates = null;
              this.applyLeavePopupLoading = false;
            }
          );
      }
    }
  }
  //#endregion

  // Leave Details
  //#region "Get All Leave Details"
  GetAllLeaveDetails(employeeId: number) {
    this.hrService
      .GetAllLeaveInfoById(
        this.setting.getBaseUrl() +
          GLOBAL.API_HR_GetEmployeeApplyLeaveDetailById,
        employeeId
      )
      .subscribe(
        data => {
          this.leavedetailsDataSource = [];
          if (
            data.data.EmployeeApplyLeaveList != null &&
            data.data.EmployeeApplyLeaveList.length > 0
          ) {
            data.data.EmployeeApplyLeaveList.forEach(element => {
              this.leavedetailsDataSource.push({
                ApplyLeaveId: element.ApplyLeaveId,
                ApplyLeaveStatus: element.ApplyLeaveStatus,
                ApplyLeaveStatusId: element.ApplyLeaveStatusId,
                EmployeeId: element.EmployeeId,
                FromDate: new Date(
                  new Date(element.FromDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                ToDate: new Date(
                  new Date(element.ToDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                LeaveReasonId: element.LeaveReasonId,
                LeaveReasonName: element.LeaveReasonName,
                Remarks: element.Remarks
              });
            });
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

  //#region "Applied leave details deleted"
  onAppliedLeaveDetailsDelete(e) {
    this.leaveDetailsPopupLoading = true;

    this.hrService
      .DeleteApplyEmployeeLeave(
        this.setting.getBaseUrl() + GLOBAL.API_HR_DeleteApplyEmployeeLeave,
        e.data.ApplyLeaveId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Deleted Successfully!!!');
            this.GetAllLeaveDetails(this.employeeId);
            this.GetAllLeaveInfo(this.employeeId);
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.leaveDetailsPopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.leaveDetailsPopupLoading = false;
        }
      );
  }
  //#endregion

  //#region "Hide / Show"
  // Assign Leave
  showAssignLeavePopup() {
    this.leaveInfoData = {
      AssignUnit: null,
      Description: null,
      FinancialYearId: null,
      LeaveReasonId: null,
      Unit: null
    };

    this.LeaveReasonIdBinding = null;
    this.popupAssignLeaveVisible = true;
  }

  hideAssignLeavePopup() {
    this.popupAssignLeaveVisible = false;
  }

  // Apply Leave
  onApplyLeaveByemployeeIdShowPopup(data: any) {
    this.popupApplyLeaveVisible = true; // popup

    this.selectedDates = null;

    // Data Binding
    this.applyLeaveDataSource = {
      BlanceLeave: data.data.BlanceLeave,
      EmployeeId: null,
      FromDate: new Date(),
      LeaveReasonId: data.data.LeaveReasonId,
      LeaveReasonName: data.data.LeaveReasonName,
      Remarks: null,
      ToDate: new Date()
    };
  }

  // Apply Leave
  hideApplyLeavePopup() {
    this.popupApplyLeaveVisible = false;
  }

  // Leave Details
  showHideLeaveDetailsPopup() {
    this.popupLeaveDetailsVisible = !this.popupLeaveDetailsVisible;
    if (this.popupLeaveDetailsVisible) this.GetAllLeaveDetails(this.employeeId);
  }

  // Edit Leave Info
  onAssignDaysShowPopup(data) {
    this.editLeaveInfoFormData = {
      LeaveId: data.data.LeaveId,
      LeaveReasonName: data.data.LeaveReasonName,
      Unit: data.data.Unit,
      AssignUnit: data.data.AssignUnit,
      BlanceLeave: data.data.BlanceLeave
    };
    this.PopupEditLeaveInfoVisible = true;
  }

  hideEditLeaveInfoPopup() {
    this.PopupEditLeaveInfoVisible = false;
  }
  //#endregion
}

export class LeaveInfoModel {
  LeaveId?: number;
  LeaveReasonName: string;
  Unit: number;
  AssignUnit: number;
  BlanceLeave: number;
}

export class LeaveInfoData {
  FinancialYearId: number;
  LeaveReasonId: number;
  Unit: number;
  AssignUnit: number;
  Description: string;
}

// Apply Leave
export class ApplyLeaveModel {
  FromDate: Date;
  ToDate: Date;
  LeaveReasonId?: number;
  LeaveReasonName: string;
  BlanceLeave: number;
  Remarks: string;
  EmployeeId?: number;
}

// Leave Details
export class LeaveDetailsModel {
  ApplyLeaveId?: number;
  EmployeeId?: number;
  FromDate: Date;
  ToDate: Date;
  LeaveReasonId?: number;
  LeaveReasonName: string;
  ApplyLeaveStatusId?: number;
  ApplyLeaveStatus: string;
  Remarks: string;
}

export class FinancialYearDropdown {
  FinancialYearId: number;
  FinancialYearName: string;
  StartDate?: Date;
  EndDate?: Date;
}
