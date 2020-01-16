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
export class AttendanceService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "getAttendancelist"
  getAttendanceList(model: any) {
    debugger;
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_GetFilteredAttendanceDetails,
        model
      );

  }
  //#endregion
}
