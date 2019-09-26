import { Injectable } from '@angular/core';
import { GlobalService } from '../../shared/services/global-services.service';
import { AppUrlService } from '../../shared/services/app-url.service';
import { GLOBAL } from '../../shared/global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from '../../../app/dashboard/accounting/vouchers/models/status-code.model';
import { IFilterValueModel } from '../models/purchase';
import { retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) { }

  //#region "GetPurchaseFilterList"
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

  //#region "GetInventoriesByInventoryTypeId"
  GetInventoriesByInventoryTypeId(Id: number): any {
    return this.globalService
      .getStoreInventoriesById(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllInventories, Id)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.InventoryList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        }));
  }
  //#endregion

  //#region "GetItemGroupByInventoryId"
  GetItemGroupByInventoryId(Id: number): any {
    return this.globalService
      .getItemById(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllStoreItemGroups, Id)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.storeItemGroupList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        }));
  }
  //#endregion

  //#region "GetItemsByItemGroupId"
  GetItemsByItemGroupId(Id: number): any {
    return this.globalService
      .getItemById(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllInventoryItems, Id)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.InventoryItemList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        }));
  }
  //#endregion

  //#region "GetPurchaseListFilter"
  GetFilteredPurchaseList(filter: IFilterValueModel): any {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetFilteredPurchaseList, filter)
      .pipe(
        map(x => {
          return x;
        })
      );
  }
  //#endregion
}
