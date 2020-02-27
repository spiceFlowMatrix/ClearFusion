import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MatDialog } from '@angular/material';
import { GLOBAL } from 'src/app/shared/global';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeListService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private dialog: MatDialog,
    private globalSharedService: GlobalSharedService) { }

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

  getResignationDetailById(id) {
    return this.globalService.getItemById(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_GetEmployeeResignationById,
      id
    );
  }

  terminateEmployeeByEmployeeeId(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_TerminateEmployeeByEmployeeId,
      model
    );
  }

  deleteEmployeeByEmployeeeId(id) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_DeleteEmployeeByEmployeeId,
      id
    );
  }

  revokeEmployeeResignationByEmployeeId(id) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_RevokeEmployeeResignationById,
      id
    );
  }

  rehireEmployeeByEmployeeId(id) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_RehireEmployeeById,
      id
    );
  }
  createAllEmployeesToUser() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_EmployeesToUser);
  }
  exportPayrollExcel(model) {
    return this.globalSharedService.getFile(
      this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetEmployeesPayrollExcel,
      model
    );
  }
}
