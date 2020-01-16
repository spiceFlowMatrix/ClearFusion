import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material';
import { GLOBAL } from 'src/app/shared/global';

@Injectable({
  providedIn: 'root'
})
export class EmployeeHistoryService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient,
    private dialog: MatDialog
  ) {}

  //#region "GetCurrencyList"
  GetCurrencyList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency
    );
  }
  //#endregion
  //#region "GetCurrencyList"
  GetLanguageList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeHR_GetLanguageList
    );
  }
  //#endregion
  //#region "getHistoricalLogList"
  getHistoricalLogList(EmployeeId: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_Hr_GetAllEmployeeHistoryByEmployeeId +
        '?EmployeeId=' +
        EmployeeId
    );
  }
  //#endregion
  //#region "getEducationDetailList"
  getEducationDetailList(EmployeeId: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetAllEmployeeEducations +
        '?EmployeeId=' +
        EmployeeId
    );
  }
  //#endregion
  //#region "getEmployeeHistoryOfOutsideCountryDetailList"
  getEmployeeHistoryOfOutsideCountryDetailList(EmployeeId: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetAllEmployeeHistoryOutsideCountry +
        '?EmployeeId=' +
        EmployeeId
    );
  }
  //#endregion
  //#region "getEmployeeCloseRelativeList"
  getEmployeeCloseRelativeList(EmployeeId: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetAllEmployeeRelativeInformation +
        '?EmployeeId=' +
        EmployeeId
    );
  }
  //#endregion
  //#region "getEmployeeThreeReferenceDetailList"
  getEmployeeThreeReferenceDetailList(EmployeeId: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetAllEmployeeInfoReferences +
        '?EmployeeId=' +
        EmployeeId
    );
  }
  //#endregion
  //#region "getEmployeeOtherSkillDetailList"
  getEmployeeOtherSkillDetailList(EmployeeId: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetAllEmployeeOtherSkills +
        '?EmployeeId=' +
        EmployeeId
    );
  }
  //#endregion
  //#region "getEmployeeSalarybudgetDetailList"
  getEmployeeSalaryBudgetDetailList(EmployeeId: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetAllEmployeeSalaryBudgets +
        '?EmployeeId=' +
        EmployeeId
    );
  }
  //#endregion
  //#region "getEmployeeLanguageDetailList"
  getEmployeeLanguageDetailList(EmployeeId: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetAllEmployeeLanguages +
        '?EmployeeId=' +
        EmployeeId
    );
  }
  //#endregion
  //#region "addHistoricalLogDetail"
  addHistoricalLogDetail(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddEmployeeHistoryDetail,
      model
    );
  }
  //#endregion
  //#region "addEducationDetail"
  addEducationDetail(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddEmployeeEducations,
      model
    );
  }
  //#endregion
  //#region "addHistoryOutsideCountry"
  addHistoryOutsideCountry(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddEmployeeHistoryOutsideCountry,
      model
    );
  }
  //#endregion
  //#region "addCloseRelativeDetail"
  addCloseRelativeDetail(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddEmployeeRelativeInformation,
      model
    );
  }
  //#endregion
  //#region "addThreeReferenceDetail"
  addThreeReferenceDetail(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddEmployeeInfoReferences,
      model
    );
  }
  //#endregion
  //#region "addOtherSkillDetail"
  addOtherSkillDetail(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddEmployeeOtherSkills,
      model
    );
  }
  //#endregion
  //#region "addSalaryBudgetDetail"
  addSalaryBudgetDetail(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddEmployeeSalaryBudgets,
      model
    );
  }
  //#endregion
   //#region "addLanguageDetail"
  addLanguageDetail(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddEmployeeLanguages,
      model
    );
  }
  //#endregion
  //#region "deleteHistoricalLog"
  deleteHistoricalLog(HistoryId: number) {
    return this.globalService.deleteById(
      this.appurl.getApiUrl() +
        GLOBAL.API_Hr__DeleteEmployeeHistoryDetail +
        '?HistoryId=' +
        HistoryId
    );
  }
  //#endregion
  //#region "deleteEducation"
  deleteEducation(model: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr__DeleteEmployeeEducations,
      model
    );
  }
  //#endregion
  //#region "deleteEmployeeHistoryOutsideCountry"
  deleteEmployeeHistoryOutsideCountry(model: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_Hr__DeleteEmployeeHistoryOutsideCountry,
      model
    );
  }
  //#endregion
  //#region "deleteCloseRelativeDetail"
  deleteCloseRelativeDetail(model: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_Hr__DeleteEmployeeRelativeInformation,
      model
    );
  }
  //#endregion
  //#region "deleteEmployeeReferenceInfoDetail"
  deleteEmployeeReferenceInfoDetail(model: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr__DeleteEmployeeInfoReferences,
      model
    );
  }
  //#endregion
  //#region "deleteEmployeeOtherSkillDetail"
  deleteEmployeeOtherSkillDetail(model: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr__DeleteEmployeeOtherSkills,
      model
    );
  }
  //#endregion
  //#region "deleteEmployeeSalaryBudgetDetail"
  deleteEmployeeSalaryBudgetDetail(model: any) {
      return this.globalService.post(
        this.appurl.getApiUrl() + GLOBAL.API_Hr__DeleteEmployeeSalaryBudgets,
        model
      );
  }
  //#endregion
  //#region "deleteEmployeeOtherSkillDetail"
  deleteEmployeeLanguageDetail(model: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr__RemoveEmployeeLanguages,
      model
    );
  }
  //#endregion
}
