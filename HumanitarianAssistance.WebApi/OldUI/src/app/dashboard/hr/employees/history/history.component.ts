import { Component, OnInit, Input } from '@angular/core';
import { HrService, EmpHistory } from '../../hr.service';
import { AccountsService } from '../../../accounts/accounts.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService } from '../../../../service/common.service';
@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  @Input() employeeId: number;
  @Input() tabEventValue: number;

  empHistory: EmpHistory;
  currentDate = new Date();
  showEmployeeHistoryData: any[];

  popupEmpHistoryVisible = false;
  isEditingAllowed = false;

  // loader
  historyInfoLoading = false;
  addHistoryPopupLoading: boolean;

  constructor(
    private hrService: HrService,
    private accountservice: AccountsService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonservice: CommonService
  ) {}

  ngOnInit() {
    this.initializeForm();
    this.getAllEmployeeHistoryByEmployeeId(this.employeeId);
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.Employees
    );
  }

  initializeForm() {
    this.empHistory = {
      HistoryID: 0,
      EmployeeID: 0,
      HistoryDate: new Date(),
      Description: null
    };
  }

  //#region "GET ALL EMPLOYEE HISTORY"
  getAllEmployeeHistoryByEmployeeId(id: number) {
    this.historyInfoLoading = true;
    this.hrService
      .GetAllEmployeeHistoryByEmployeeId(
        this.setting.getBaseUrl() +
          GLOBAL.API_Hr_GetAllEmployeeHistoryByEmployeeId,
        id
      )
      .subscribe(
        data => {
          this.showEmployeeHistoryData = [];

          if (
            data.StatusCode === 200 &&
            data.data.EmployeeHistoryDetailList != null &&
            data.data.EmployeeHistoryDetailList.length > 0
          ) {
            data.data.EmployeeHistoryDetailList.forEach(element => {
              this.showEmployeeHistoryData.push({
                HistoryID: element.HistoryID,
                HistoryDate: new Date(
                  new Date(element.HistoryDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                Description: element.Description
              });
            });
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.historyInfoLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.historyInfoLoading = false;
        }
      );
  }
  //#endregion

  //#region "ADD EMPLOYEE HISTORY"
  addEmployeeHistoryDetail(data) {
    this.addHistoryPopupLoading = true;

    const historyModel: EmpHistory = {
      EmployeeID: this.employeeId,
      HistoryDate: new Date(
        new Date(data.HistoryDate).getFullYear(),
        new Date(data.HistoryDate).getMonth(),
        new Date(data.HistoryDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      Description: data.Description,
      HistoryID: 0
    };
    this.hrService
      .AddEmployeeHistoryDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_AddEmployeeHistoryDetail,
        historyModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('History Added Successfully!!!');
            this.getAllEmployeeHistoryByEmployeeId(this.employeeId);
            this.HideAddEmpHistoryPopup();
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.addHistoryPopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getAllEmployeeHistoryByEmployeeId(historyModel.EmployeeID);
          this.addHistoryPopupLoading = false;
        }
      );
  }
  //#endregion

  //#region "EDIT EMPLOYEE HISTORY"
  editEmployeeHistoryDetail(value) {
    const historyModel: EmpHistory = {
      EmployeeID: this.employeeId,
      HistoryDate: new Date(
        new Date(value.HistoryDate).getFullYear(),
        new Date(value.HistoryDate).getMonth(),
        new Date(value.HistoryDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ).toUTCString(),
      Description: value.Description,
      HistoryID: value.HistoryID
    };
    this.hrService
      .EditEmployeeHistoryDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_EditEmployeeHistoryDetail,
        historyModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('History Updated successfully!!!');
            this.getAllEmployeeHistoryByEmployeeId(this.employeeId);
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
          this.getAllEmployeeHistoryByEmployeeId(value);
        }
      );
  }
  //#endregion

  //#region "DELETE EMPLOYEE HISTORY"
  deleteEmployeeHistoryDetail(historyId: number) {
    this.hrService
      .DeleteEmployeeHistoryDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_DeleteEmployeeHistoryDetail,
        historyId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('History Detail Deleted Successfully!!!');
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
          else {
            this.getAllEmployeeHistoryByEmployeeId(this.employeeId);
          }
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
  //#endregion "DELETE EMPLOYEE HISTORY"

  //#region "Event for ADD, UPDATE, DELETE"
  logEvent(eventName, obj) {
    if (eventName === 'RowHistoryUpdating') {
      const value = Object.assign(obj.oldData, obj.newData);
      this.editEmployeeHistoryDetail(value);
    } else if (eventName === 'RowHistoryRemoving') {
      this.deleteEmployeeHistoryDetail(obj.data.HistoryID);
    }
  }
  //#endregion "Event for ADD, UPDATE, DELETE"

  //#region "Show / Hide"
  ShowAddEmpHistoryPopup() {
    this.empHistory = {
      Description: null,
      EmployeeID: 0,
      HistoryDate: new Date(),
      HistoryID: 0
    };
    this.popupEmpHistoryVisible = true;
  }

  HideAddEmpHistoryPopup() {
    this.popupEmpHistoryVisible = false;
  }
  //#endregion
}
