import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from 'src/app/shared/global';
import { map } from 'rxjs/operators';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeAdvanceService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "getAdvanceListEmployeeId"
  getAdvanceListEmployeeId(id: any) {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() +
          GLOBAL.API_EmployeePayroll_GetAdvanceListByEmployeeId + '?id=' + id
      );
  }
  //#endregion

  //#region "getEmployeeList"
  getEmployeeList() {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() +
          GLOBAL.API_Code_GetAllEmployeeList
      );
  }
  //#endregion

  //#region "addAdvance"
  addAdvance(model) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_EmployeePayroll_AddNewAdvanceRequest, model
      );
  }
  //#endregion

  //#region "getAdvanceDetailById"
  getAdvanceDetailById(model) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_EmployeePayroll_GetAdvanceDetailById, model
      );
  }
  //#endregion

  //#region "approveAdvance"
  approveAdvance(model) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_EmployeePayroll_ApproveAdvanceRequest, model
      );
  }
  //#endregion

  //#region "editAdvance"
  editAdvance(model) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_EmployeePayroll_EditAdvanceRequest, model
      );
  }
  //#endregion

  //#region "rejectAdvance"
  rejectAdvance(id) {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() +
          GLOBAL.API_EmployeePayroll_RejectAdvanceRequest + '?id=' + id
      );
  }
  //#endregion

  //#region "getAdvanceHistory"
  getAdvanceHistory(id) {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() +
          GLOBAL.API_EmployeePayroll_GetAdvanceHistoryById + '?id=' + id
      );
  }
  //#endregion

  //#region "getEmployeePayrollCurrency"
  getEmployeePayrollCurrency(id) {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() +
          GLOBAL.API_EmployeePayroll_GetEmployeePayrollCurrency + '?id=' + id
      );
  }
  //#endregion
}
