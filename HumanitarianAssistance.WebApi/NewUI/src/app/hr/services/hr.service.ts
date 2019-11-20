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
  getDesignationList(): any {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllDesignation
      );
  }
  //#endregion

  //#region "addDesignation"
  addDesignation(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Code_AddDesignation, model
      );
  }
  //#endregion
}


