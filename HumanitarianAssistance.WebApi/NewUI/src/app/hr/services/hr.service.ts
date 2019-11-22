import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from 'src/app/shared/global';

@Injectable({
  providedIn: 'root'
})
export class HrService {

  constructor(private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient) { }

  //#region "getDesignatonList"
  getDesignationList(pageModel: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetAllDesignationDetail, pageModel
      );
  }
  //#endregion

  //#region "addDesignation"
  addDesignation(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_AddDesignationDetail, model
      );
  }
  //#endregion

  //#region "editDesignation"
  editDesignation(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_EditDesignationDetail, model
      );
  }
  //#endregion

  //#region "getEducationDegreeList"
  getEducationDegreeList(pageModel: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetEducationDegreeList, pageModel
      );
  }
  //#endregion

  //#region "addDegree"
  addDegree(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_AddEducationDegree, model
      );
  }
  //#endregion

  //#region "editDegree"
  editDegree(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_EditEducationDegree, model
      );
  }
  //#endregion
}


