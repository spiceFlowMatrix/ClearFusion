import { IEmployeeAllDetails, IEmployeeAllDetailsForEdit } from './../models/employee-detail.model';
import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';

@Injectable({
  providedIn: 'root'
})
export class AddEmployeeService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "GetProfessionList"
  GetProfessionList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllProfession
    );
  }

  //#region "GetQualificationList"
  GetQualificationList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllQualification
    );
  }
  //#region "GetJobGradeList"
  GetJobGradeList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_HREmployee_GetAllJobGrade
    );
  }
  //#endregion

  //#region "getAllProvinceListByCountryId"
  getAllProvinceListByCountryId(Id: any) {
    return this.globalService.getListByListId(
      this.appurl.getApiUrl() +
        GLOBAL.API_Project_GetAllProvinceDetailsByCountryId,
      Id
    );
  }
  //#endregion
  //#region "GetAllDistrictvalueByProvinceId"
  GetAllDistrictvalueByProvinceId(id) {
    return this.globalService.getListByListId(
      this.appurl.getApiUrl() +
        GLOBAL.API_Project_GetAllDistrictvalueByProvinceId,
      id
    );
  }
  //#endregion
  //#region "GetCountryList"
  GetCountryList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllCountryDetails
    );
  }
  //#endregion
  //#region "GetEmployeeTypeList"
  GetEmployeeTypeList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllEmployeeType
    );
  }
  //#endregion
  //#region "GetOfficeList"
  GetOfficeList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice
    );
  }
  //#endregion
  //#region "getDesignationList"
  getDesignationList(): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllDesignationList
    );
  }
  //#endregion
  //#region "GetCurrencyList"
  GetCurrencyList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency
    );
  }
  //#endregion
  //#region "GetDepartmentList"
  GetDepartmentList(officeId): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_Code_GetDepartmentsByOfficeId +
        '?officeId=' +
        officeId
    );
  }
  //#endregion
  //#region "getContractTypeList"
  getContractTypeList(): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_GetEmployeeContractType
    );
  }
  //#endregion
  //#region "GetAllEmployeeList"
  GetAllAttendanceGroupList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAttendanceGroupst
    );
  }
  //#endregion
  //#region "AddNewEmployeeData"
  AddNewEmployeeDetails(model: IEmployeeAllDetails): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr__EditEmployee,
      model
    );
  }
  //#endregion

   //#region "AddNewEmployeeData"
   EditEmployeeDetails(model: IEmployeeAllDetailsForEdit): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr__AddNewEmployee,
      model
    );
  }
  //#endregion

  //#region "CheckExchangeRatesVerified"
  CheckExchangeRatesVerified(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ExchangeRates_CheckExchangeRatesVerified,
      model
    );
  }
  //#endregion

  //#region "GetEmployeeDetailByEmployeeId"
  GetEmployeeDetailByEmployeeId(employeeId): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_Hr_GetEmployeeById +
        '?EmployeeId=' +
        employeeId
    );
  }
  //#endregion

  //#region "GetEmployeeProfessionalDetailByEmployeeId"
  GetEmployeeProfessionalDetailByEmployeeId(employeeId): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_HR_GetEmployeeProfessionalDetail +
        '?EmployeeId=' +
        employeeId
    );
  }
  //#endregion
}
