import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from 'src/app/shared/global';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { MatDialog } from '@angular/material';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';

@Injectable({
  providedIn: 'root'
})
export class HrLeaveService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient,
    private dialog: MatDialog
  ) {}

   //#region "getEmployeeBalanceLeave"
   getEmployeeBalanceLeave(employeeid: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_HR_GetAllEmployeeAssignLeave + '?EmployeeId=' +
        employeeid
    );
  }
  //#endregion

  //#region "addEmployeeLeave"
  addEmployeeLeave(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HR_AddEmployeeLeave,
        model
    );
  }
  //#endregion

  //#region "getFinancialYearList"
  getFinancialYearList(): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_Code_GetCurrentFinancialYear
    );
  }
  //#endregion

  //#region "getAllLeaveReasonList"
  getAllLeaveReasonType(): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_Code_LeaveReasonType
    );
  }
  //#endregion

   //#region "assignLeave"
   assignLeave(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HR_AssignLeaveToEmployeeDetail, model
    );
  }
  //#endregion

   //#region "getAllLeaveInfoById"
   getAllLeaveInfoById(employeeid: any): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_HR_GetEmployeeAppliedLeaves + '?id=' + employeeid
    );
  }
  //#endregion
}
