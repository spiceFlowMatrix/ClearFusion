import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MatDialog } from '@angular/material';
import { GLOBAL } from 'src/app/shared/global';

@Injectable({
  providedIn: 'root'
})
export class EmployeeListService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private dialog: MatDialog) { }

  GetAllOfficeCodeList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice
    );
  }

  getAllEmployeeList(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_GetAllEmployeeDetailList,
      model
    );
  }

  deleteMurtipleEmployeesById(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_DeleteMurtipleEmployeesById,
      model
    );
  }

  addResignation(EmployeeID) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_AddEmployeeResignation,
      EmployeeID
    );
  }

  saveResignation(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_SaveEmployeeResignation,
      model
    );
  }
}
