import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';

@Injectable({
  providedIn: 'root'
})
export class LogisticService {

  constructor(
    private globalService: GlobalService,
    private globalSharedService: GlobalSharedService,
    private appurl: AppUrlService
  ) { }

  addLogisticRequest(value) {
    debugger;
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_AddLogisticRequest,
      value
    );
  }
}
