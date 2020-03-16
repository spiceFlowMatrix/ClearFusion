import { Injectable } from '@angular/core';
import { GlobalService } from '../../shared/services/global-services.service';
import { AppUrlService } from '../../shared/services/app-url.service';
import { GLOBAL } from '../../shared/global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from '../../../app/dashboard/accounting/vouchers/models/status-code.model';
import {
  IFilterValueModel,
  IAddEditPurchaseModel,
  IAddEditProcurementModel,
  IDeleteProcurementModel,
  IDropDownModel
} from '../models/purchase';
import { retry, finalize, tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { of, Observable } from 'rxjs';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient
  ) {}

//#region "GetPurchaseFilterList"
getPurchaseFilterList(): any {
  return this.globalService
    .getList(
      this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetAllPurchaseFilters
    )
    .pipe(
      map(x => {
        return x;
      })
    );
}
//#endregion

  //#region "GetInventoriesByInventoryTypeId"
  getInventoriesByInventoryTypeId(Id: number): any {
    return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_Store_GetAllInventories +
          '?AssetType=' +
          Id
      )
      .pipe(
        map(response => {
          const responseData: IResponseData = {
            data: response.data.InventoryList,
            statusCode: response.StatusCode,
            message: response.Message
          };
          return responseData;
        }),
        finalize(() => {})
      );
  }
  //#endregion

  //#region "GetItemGroupByInventoryId"
  getItemGroupByInventoryId(Id: number): any {
    return this.globalService
      .getItemById(
        this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllStoreItemGroups,
        Id
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.storeItemGroupList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "getTransportItemCategoryType"
  getTransportItemCategoryType(Id: number): any {
    return this.globalService
      .getItemById(
        this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetTransportItemCategoryType,
        Id
      );
  }
  //#endregion

  //#region "GetItemsByItemGroupId"
  getItemsByItemGroupId(Id: number): any {
    return this.globalService
      .getItemById(
        this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllInventoryItems,
        Id
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.InventoryItemList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetPurchaseListFilter"
  getFilteredPurchaseList(filter: IFilterValueModel): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_StorePurchase_GetFilteredPurchaseList,
        filter
      )
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
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetAllInventoriesType
      )
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
    return of([
      {
        AssetTypeId: 1,
        AssetTypeName: 'Cash'
      },
      {
        AssetTypeId: 2,
        AssetTypeName: 'In Kind'
      }
    ]);
  }

  getPurchaseDocumentTypes(): Observable<IDropDownModel[]> {
    return of([{ name: 'Image', value: 1 }, { name: 'Invoice', value: 2 }]);
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
    return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetEmployeeListByOfficeId +
          '?OfficeId=' +
          Id
      )
      .pipe(
        map(response => {
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
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllStatusAtTimeOfIssue
      )
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
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllPurchaseUnitType
      )
      .pipe(
        map(x => {
          return x;
        })
      );
  }

  addPurchase(purchase: any, itemGroupTransportCategory: number, itemTransportCategory: number, quantity: number) {
    const purchaseModel: IAddEditPurchaseModel = {
      ApplyDepreciation: purchase.ApplyDepreciation,
      AssetTypeId: purchase.AssetTypeId,
      BudgetLineId: purchase.BudgetLineId,
      Currency: purchase.CurrencyId,
      DeliveryDate: this.getLocalDate(purchase.ReceiptDate),
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
      Quantity: quantity,
      ReceivedFromLocation: purchase.ReceivedFromLocation,
      ReceiptTypeId: purchase.ReceiptTypeId,
      Status: purchase.StatusId,
      UnitCost: purchase.Price,
      UnitType: purchase.Unit,
      TimezoneOffset: new Date().getTimezoneOffset(),
      PurchasedVehicleList: purchase.TransportVehicles,
      PurchasedGeneratorList: purchase.TransportGenerators,
      TransportItemId: purchase.TransportItemId,
      ItemGroupTransportCategory: itemGroupTransportCategory,
      ItemTransportCategory: itemTransportCategory
    };

    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_AddStorePurchase,
        purchaseModel,
        { observe: 'response' }
      )
      .pipe(
       // tap(resp => // console.log('response', resp)),
        map((x: Response) => {
          return {
            StatusCode: x.status,
            Message: x.status === 200 ? 'Success' : 'Fail',
            PurchaseId: x.body
          };
        })
      );
  }

  addProcurement(procurement: any) {
    const procurementModel: IAddEditProcurementModel = {
      Purchase: procurement.PurchaseId,
      InventoryItem: procurement.ItemId,
      IssuedQuantity: procurement.IssuedQuantity,
      MustReturn: procurement.MustReturn,
      IssuedToEmployeeId: procurement.IssuedToEmployeeId,
      IssueDate: StaticUtilities.getLocalDate(procurement.IssueDate),
      IssedToLocation: procurement.StoreSourceId,
      StatusAtTimeOfIssue: procurement.StatusId,
      Project: procurement.ProjectId,
      VoucherNo: procurement.VoucherNo
    };

    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Store_AddItemOrder,
        procurementModel
      )
      .pipe(
        map(x => {
          return x;
        })
      );
  }

  editProcurement(procurement: any) {
    const procurementModel: IAddEditProcurementModel = {
      OrderId: procurement.ProcurementId,
      Purchase: procurement.PurchaseId,
      InventoryItem: procurement.ItemId,
      IssuedQuantity: procurement.IssuedQuantity,
      MustReturn: procurement.MustReturn,
      IssuedToEmployeeId: procurement.IssuedToEmployeeId,
      IssueDate: StaticUtilities.getLocalDate(procurement.IssueDate),
      IssedToLocation: procurement.StoreSourceId,
      StatusAtTimeOfIssue: procurement.StatusId,
      Project: procurement.ProjectId,
      Returned: procurement.Returned,
      VoucherNo: procurement.VoucherNo
    };

    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Store_EditItemOrder,
        procurementModel
      );
  }

  deleteProcurement(orderId: number) {
    const procurementModel: IDeleteProcurementModel = {
      OrderId: orderId
    };

    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Store_DeleteItemOrder,
        procurementModel
      )
      .pipe(
        map(x => {
          return x;
        })
      );
  }

  checkExchangeRateExists(model: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_ExchangeRates_CheckExchangeRatesExist,
        model
      )
      .pipe(
        map(x => {
          return x;
        })
      );
  }

  //#region "getPurchaseListByItemId"
  getPurchaseListByItemId(Id) {
    return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_Store_GetAllPurchasesByItem +
          '?ItemId=' +
          Id
      )
      .pipe(
        map(response => {
          const responseData: IResponseData = {
            data: response.data.StoreItemsPurchaseViewList,
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
  //#endregion
//#region "getVechileList"
getVehicleList(model: any) {
  return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_VehicleTracker_GetVehicleList, model);
}

//#region "getProcurementDetailsByProcurementId"
getProcurementDetailsByProcurementId(id) {
  return this.globalService
    .getDataById(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetProcurementDetailsByProcurementId + '?id=' + id);
}


//#region "getGeneratorList"
getGeneratorList(model: any) {
  return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_GeneratorTracker_GetGeneratorList, model);
}

addVehicleMileage(model: any) {
  return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_VehicleTracker_AddVehicleMileage, model);
}

// getStorePurchaseById
getStorePurchaseById(id: number) {
  return this.http
    .get<any>(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetStorePurchaseById + '?id=' + id);
}

// getTransportItemDataSource
getTransportItemDataSource(id: number) {
  return this.http
  .get<any>(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetTransportItemDataSource + '?id=' + id);
}

 //#region "getItemDetailByItemId"
 GetLoggedInUserUsername() {
  return this.http
    .get<any>(
      this.appurl.getApiUrl() +
        GLOBAL.API_Account_GetLoggedInUserUserName);
}
//#endregion


  //#region "getItemDetailByItemId"
  getItemDetailByPurchaseId(Id) {
    return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_StorePurchase_GetItemDetailByItemId +
          '?PurchaseId=' +
          Id
      )
      .pipe(
        map(x => {
          return x;
        })
      );
  }
  //#endregion

  getVehicleDetailById(Id) {
    return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_VehicleTracker_GetVehicleById +
          '?id=' +
          Id);
  }

  saveVehicleDetail(model: any) {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_VehicleTracker_SaveVehicleDetail, model);
  }


  getGeneratorDetailById(Id) {
    return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_GeneratorTracker_GetGeneratorById +
          '?id=' +
          Id);
  }

  // addGeneratorUsageHours
addGeneratorUsageHours(model: any) {
  return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_GeneratorTracker_AddGeneratorUsageHours, model);
}

 // getVehicleMonthlyBreakdown
 getVehicleMonthlyBreakdown(model: any) {
  return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_VehicleTracker_GetVehicleMonthlyBreakdownDataById, model);
}

 // addGeneratorUsageHours
 getGeneratorMonthlyBreakdown(model: any) {
  return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_GeneratorTracker_GetGeneratorMonthlyBreakdownDataById, model);
}

// saveGeneratorDetail
saveGeneratorDetail(model: any) {
  return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_GeneratorTracker_EditGeneratorDetail, model);
}

// saveGeneratorDetail
EditStorePurchase(purchase: any) {
  const purchaseModel: IAddEditPurchaseModel = {
    ApplyDepreciation: purchase.ApplyDepreciation,
    AssetTypeId: purchase.AssetTypeId,
    BudgetLineId: purchase.BudgetLineId,
    Currency: purchase.CurrencyId,
    DeliveryDate: this.getLocalDate(purchase.ReceiptDate),
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
    TimezoneOffset: new Date().getTimezoneOffset(),
    PurchasedVehicleList: purchase.TransportVehicles,
    PurchasedGeneratorList: purchase.TransportGenerators,
    TransportItemId: purchase.TransportItemId,
    PurchaseId: purchase.PurchaseId
  };
  return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_EditStorePurchase, purchaseModel);
}

deleteVehicle(Id) {
  return this.http
    .delete<any>(
      this.appurl.getApiUrl() +
        GLOBAL.API_VehicleTracker_DeletePurchasedVehicle +
        '?id=' +
        Id);
}

deleteGenerator(Id) {
  return this.http
    .delete<any>(
      this.appurl.getApiUrl() +
        GLOBAL.API_GeneratorTracker_DeletePurchasedGenerator +
        '?id=' +
        Id);
}

getAllCurrencies() {
  return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_code_GetAllCurrency);
}

getStoreLogs(id: number, entityId: number) {
  return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_StorePurchase_GetVehicleGeneratorTrackerLogs + '?id=' + id + '&entityId=' + entityId);
}

 // getDefaultUnitTypeByItemId
 getDefaultUnitTypeByItemId(ItemId: number) {
  return this.globalService
    .getDataById(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetDefaultUnitTypeByItemId + '?id=' + ItemId);
}

getProcurementDetailWithReturnsList(id) {
  return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_StorePurchase_GetProcurementDetailWithReturnsList + '?id=' + id);
}

addProcurementReturn(model: any) {
  return this.globalService
  .post(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_AddProcurementReturn, model);
}

deleteReturnProcurement(id) {
  return this.globalService
  .post(this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_DeleteReturnProcurement, id);
}

getAllVouchers() {
  return this.http
      .get<any>(
        this.appurl.getApiUrl() +
          GLOBAL.API_Account_GetAllVouchersWithoutFilter);
}

getPreviousYearsList(years: number): Observable<IDropDownModel[]> {

  const yearDropDown: IDropDownModel[] = [];
  const year = new Date().getFullYear();
  for (let i = 0; i <= years; i++) {
    yearDropDown.push({name: (year - i).toString(),
    value: year - i});
  }
  return of(yearDropDown);
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
      new Date().getMilliseconds()
    );
  }
  //#endregion


  GetFilteredInventoryMaterList(data: any) {
    debugger
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetFilteredInventoryMasterList , data
    );
  }
  GetFilteredItemGroupList(data: any) {
    debugger
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetFilteredItemGroupList , data
    );
  }
  GetFilteredItemList(data: any) {
    debugger
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetFilteredItemList , data
    );
  }
  GetFilteredProjectList(data: any) {
    debugger
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetFilteredProjectList , data
    );
  }
  GetFilteredBudegtList(data: any) {
    debugger
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetFilteredBudegtList , data
    );
  }
  GetFilteredReceivedFromLocationList(data: any) {
    debugger
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetFilteredReceivedFromLocationList , data
    );
  }
  GetFilteredReceivedFromEmpList(data: any) {
    debugger
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetFilteredReceivedFromEmpList , data
    );
  }
}
