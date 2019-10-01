import { Injectable } from '@angular/core';
import { GlobalService } from '../../shared/services/global-services.service';
import { AppUrlService } from '../../shared/services/app-url.service';
import { GLOBAL } from '../../shared/global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from '../../../app/dashboard/accounting/vouchers/models/status-code.model';
import { IFilterValueModel, IAddEditPurchaseModel } from '../models/purchase';
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
  getPurchaseFilterList(): any {
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
  getInventoriesByInventoryTypeId(Id: number): any {
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
  getItemGroupByInventoryId(Id: number): any {
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
  getItemsByItemGroupId(Id: number): any {
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
  getFilteredPurchaseList(filter: IFilterValueModel): any {
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
  getAllInventoryTypeList(): any {
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
  getAllProjectList(): any {
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
  getAllOfficeList(): any {
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
            data: response.data.EmployeeDetailListData,
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

  getAllUnitTypeDetails() {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllPurchaseUnitType)
      .pipe(
        map(x => {
          return x;
        })
      );
  }

  addPurchase(purchase: any) {
    const purchaseModel: IAddEditPurchaseModel = {
      ApplyDepreciation: purchase.ApplyDepreciation,
      AssetTypeId: purchase.AssetTypeId,
      BudgetLineId: purchase.BudgetLineId,
      Currency: purchase.CurrencyId,
      DeliveryDate: this.getLocalDate(purchase.ReceiptDate) ,
      DepreciationRate: purchase.DepreciationRate,
      InventoryId: purchase.InventoryId,
      InventoryItem: purchase.ItemId,
      InvoiceDate: this.getLocalDate(purchase.InvoiceDate),
      InvoiceNo: purchase.InvoiceNo,
      OfficeId: purchase.OfficeId,
      ProjectId: purchase.ProjectId,
      PurchaseDate: this.getLocalDate(purchase.PurchaseOrderDate),
      PurchaseName: purchase.PurchaseName,
      PurchaseOrderNo: purchase.PurchaseOrderNo,
      PurchasedById: purchase.ReceivedFromEmployeeId,
      Quantity: purchase.Quantity,
      ReceivedFromLocation: purchase.ReceivedFromLocation,
      ReceiptTypeId: purchase.ReceiptTypeId,
      Status: purchase.StatusId,
      UnitCost: purchase.Price,
      UnitType: purchase.Unit,
      TimezoneOffset: new Date().getTimezoneOffset()
    };

    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_Store_AddPurchase, purchaseModel)
      .pipe(
        map(x => {
          return x;
        })
      );
  }

  //#region "getLocalDate"
  getLocalDate(date: any) {
    return new Date(
      new Date(date).getFullYear(),
      new Date(date).getMonth(),
      new Date(date).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds(),
      new Date().getMilliseconds(),
    );
  }
  //#endregion
}
