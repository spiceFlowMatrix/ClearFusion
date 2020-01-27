import { Injectable } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { GLOBAL } from 'src/app/shared/global';

@Injectable({
  providedIn: 'root'
})
export class EmployeeAdvanceService {

  constructor(private globalService: GlobalService,
    private appurl: AppUrlService) { }

  //#region "getAdvanceListByEmployeeId"
  getAdvanceListByEmployeeId(id: any) {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() +
        GLOBAL.API_EmployeePayroll_GetAdvanceListByEmployeeId + '?id=' + id
      );
  }
  //#endregion
}
