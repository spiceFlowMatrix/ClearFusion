import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material';
import { GLOBAL } from 'src/app/shared/global';

@Injectable({
  providedIn: 'root'
})
export class EmployeeSalaryConfigService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient,
    private dialog: MatDialog
  ) { }

    //#region "GetCurrencyList"
    GetCurrencyList(): any {
      return this.globalService.getList(
        this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency
      );
    }
    //#endregion

    //#region "SaveBasicSalary"
    saveBasicSalary(model: any): any {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_AddEmployeeSalaryCurrencyAndBasicSalary, model
      );
    }
    //#endregion

    //#region "getEmployeeBasicPayAndCurrency"
    getEmployeeBasicPayAndCurrency(employeeId: number): any {
      return this.globalService.getDataById(
        this.appurl.getApiUrl() + GLOBAL.API_HR_GetEmployeeBasicPayAndCurrency + '?id=' + employeeId
      );
    }
    //#endregion

     //#region "editBasicSalary"
     editBasicSalary(model: any): any {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_EditEmployeeSalaryCurrencyAndBasicSalary, model
      );
    }
    //#endregion

    //#region "saveBonusFineSalaryHead"
    saveBonusFineSalaryHead(model: any): any {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_AddEmployeeBonusFineSalaryHead, model
      );
    }
    //#endregion

    //#region "getEmployeeBonusFineSalaryHead"
    getEmployeeBonusFineSalaryHead(model: any): any {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_GetEmployeeBonusFineSalaryHead, model
      );
    }
    //#endregion

    //#region "deleteEmployeeBonusFineSalaryHead"
    deleteEmployeeBonusFineSalaryHead(id): any {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_DeleteEmployeeBonusFineSalaryHead, id
      );
    }
    //#endregion

    //#region "getEmployeeAccumulatedSalaryHead"
    getEmployeeAccumulatedSalaryHead(id): any {
      return this.globalService.getDataById(
        this.appurl.getApiUrl() + GLOBAL.API_HR_GetEmployeeAccumulatedSalaryHead + '?id=' + id
      );
    }
    //#endregion

    //#region "getEmployeePayroll"
    getEmployeePayroll(model): any {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_GetEmployeeMonthlyPayroll, model
      );
    }
    //#endregion

    //#region "approvePayroll"
    approvePayroll(model): any {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_ApproveEmployeeMonthlyPayroll, model
      );
    }
    //#endregion

    //#region "revokeEmployeePayroll"
    revokeEmployeePayroll(model): any {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_RevokeEmployeeMonthlyPayroll, model
      );
    }
    //#endregion

    //#region "addAdvanceRecovery"
    addAdvanceRecovery(model): any {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_EmployeePayroll_AddAdvanceRecovery, model
      );
    }
    //#endregion

    //#region "getEmployeeAdvanceDetail"
    getEmployeeAdvanceDetail(id): any {
      return this.globalService.getDataById(
        this.appurl.getApiUrl() + GLOBAL.API_EmployeePayroll_GetEmployeePayrollAdvanceDetail + '?id=' + id
      );
    }
    //#endregion
}
