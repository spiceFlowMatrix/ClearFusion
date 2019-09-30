import { Injectable } from '@angular/core';
import { GlobalService } from '../../shared/services/global-services.service';
import { AppUrlService } from '../../shared/services/app-url.service';
import { GLOBAL } from '../../shared/global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from '../../../app/dashboard/accounting/vouchers/models/status-code.model';
import { IFilterValueModel } from '../models/purchase';
import { retry, finalize } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { of, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient,
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
        return this.http.get<any>(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllInventories + '?AssetType=' + Id).pipe(
          map((response) => {
            const responseData: IResponseData = {
                    data: response.data.InventoryList,
                    statusCode: response.StatusCode,
                    message: response.Message
                  };
                  return responseData;
          }),
          finalize(() => {
          })
        );
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

  //#region "GetPurchaseFilterList"
  GetAllInventoryTypeList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetAllInventoriesType)
      .pipe(
        map(x => {
          return x;
        })
      );
  }
  //#endregion

  //#region "GetPurchaseFilterList"
  GetAllProjectList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectList)
      .pipe(
        map(x => {
          return x;
        })
      );
  }
  //#endregion

  //#region "GetPurchaseFilterList"
  GetAllOfficeList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice)
      .pipe(
        map(x => {
          return x;
        })
      );
  }
  //#endregion

  getPurchaseAssetType(): Observable<any[]> {
    return of(
      [
        {
            AssetTypeId: 1,
            AssetTypeName: 'Cash'
        },
        {
            AssetTypeId: 2,
            AssetTypeName: 'In Kind'
        }
    ]
    );
  }

  getAllStoreSource() {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllStoreSourceCode)
      .pipe(
        map(x => {
          return x;
        })
      );
  }

  getEmployeesByOfficeId(Id) {
      return this.http.get<any>(this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_GetEmployeeListByOfficeId + '?OfficeId=' + Id)
      .pipe(
        map((response) => {
          const responseData: IResponseData = {
            data: response.data.InventoryItemList,
            statusCode: response.StatusCode,
            message: response.Message
        };

        return responseData;
      }),
        finalize(() => {
          // this.loader.hideLoader();
        })
      );
  }

  getAllReceiptType() {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllReceiptType)
      .pipe(
        map(x => {
          return x;
        })
      );
  }

  getAllStatusAtTimeOfIssue() {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllStatusAtTimeOfIssue)
      .pipe(
        map(x => {
          return x;
        })
      );
  }

  getAllCurrency() {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency)
      .pipe(
        map(x => {
          return x;
        })
      );
  }
}
