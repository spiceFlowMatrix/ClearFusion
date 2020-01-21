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
}
