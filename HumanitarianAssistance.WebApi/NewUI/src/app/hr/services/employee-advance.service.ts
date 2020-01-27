import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from 'src/app/shared/global';
import { map } from 'rxjs/operators';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeAdvanceService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "getAdvanceListEmployeeId"
  getAdvanceListEmployeeId(id: any) {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() +
          GLOBAL.API_EmployeePayroll_GetAdvanceListByEmployeeId + '?id=' + id
      );
  }
  //#endregion
}
