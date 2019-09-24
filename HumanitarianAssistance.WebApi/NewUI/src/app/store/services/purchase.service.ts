import { Injectable } from '@angular/core';
import { GlobalService } from '../../shared/services/global-services.service';
import { AppUrlService } from '../../shared/services/app-url.service';
import { GLOBAL } from '../../shared/global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from '../../../app/dashboard/accounting/vouchers/models/status-code.model';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) { }

  //#region "GetVoucherTypeList"
  GetPurchaseFilterList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetAllPurchaseFilters)
      .pipe(
        map(x => {
          return x;
        })
      );
  }
  //#endregion
}
