import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { IEmployeeDetailModel } from '../models/employee-detail.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeContractService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "GetOfficeList"
  GetOfficeList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice
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

  //#region "getDesignationList"
  getDesignationList(): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllDesignationList
    );
  }
  //#endregion
  //#region "GetJobGradeList"
  GetJobGradeList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_HREmployee_GetAllJobGrade
    );
  }
  //#endregion
  //#region "getAllProjectList"
  getAllProjectList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectList
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
  //#region "GetBudgetLineList"
  GetBudgetLineList(projectId: number): any {
    return this.globalService.getListById(
      this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectBudgetLineDetail,
      projectId
    );
  }
  //#endregion
  //#region "addEmployeeContractDetail"
  addEmployeeContractDetail(model: IEmployeeDetailModel): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddEmployeeContractDetails,
      model
    );
  }
  //#endregion
  //#region "getEmployeeDetail"
  getEmployeeDetail(employeeid: number): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetEmployeeDetailForContractById,
      employeeid
    );
  }
  //#endregion
  //#region "getEmployeeDetail"
  getEmployeeContractDetailByEmployeeId(EmployeeId: number): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_Hr_GetSelectedEmployeeContractByEmployeeId
        + '?EmployeeId=' + EmployeeId
    );
  }
  //#endregion
}
