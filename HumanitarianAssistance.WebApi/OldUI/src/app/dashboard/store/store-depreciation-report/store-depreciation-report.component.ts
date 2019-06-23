import { Component, OnInit } from '@angular/core';
import { StoreService } from '../store.service';
import { GLOBAL } from '../../../shared/global';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-store-depreciation-report',
  templateUrl: './store-depreciation-report.component.html',
  styleUrls: ['./store-depreciation-report.component.css']
})
export class StoreDepreciationReportComponent implements OnInit {
  //#region "Variables"
  storeTabs = [
    {
      id: 1,
      text: 'Consumables'
    },
    {
      id: 2,
      text: 'Expendables'
    },
    {
      id: 3,
      text: 'Non-Expendables'
    }
  ];

  depreciationReportDataSource: DepreciationReportModel[];
  depreciationReportFilter: DepreciationReportFilterModel;

  inventoryItemDataSource: any[];
  inventoryList: any[];
  itemGroupList: any[];

  itemDropDownDisable = true;

  // Loader
  depreciationReportLoading = false;

  //#endregion

  constructor(
    private storeService: StoreService,
    private setting: AppSettingsService,
    private commonService: CommonService
  ) {}

  ngOnInit() {
    this.initializeForm();
  }

  initializeForm() {
    this.depreciationReportFilter = {
      StoreId: null,
      InventoryId: null,
      ItemGroupId: null,
      ItemId: null,
      CurrentDate: null
    };
  }

  //#region "getDepreciationReportDetails"
  getDepreciationReportDetails(dataModel: DepreciationReportFilterModel) {
    if (dataModel != null) {
      this.showDepreciationReportLoading();

      this.storeService
        .AddEditByModel(
          this.setting.getBaseUrl() +
            GLOBAL.API_Store_GetAllDepreciationByFilter,
          dataModel
        )
        .subscribe(
          data => {
            this.depreciationReportDataSource = [];
            if (data.data.DepreciationReportList != null) {
              if (data.data.DepreciationReportList.length > 0) {
                data.data.DepreciationReportList.forEach(element => {
                  this.depreciationReportDataSource.push({
                    ItemName: element.ItemName,
                    PurchaseId: element.PurchaseId,
                    HoursSincePurchase: element.HoursSincePurchase,
                    DepreciationRate: element.DepreciationRate,
                    DepreciationAmount: element.DepreciationAmount,
                    CurrentValue: element.CurrentValue,
                    PurchaseDate: new Date(
                      new Date(element.PurchaseDate).getTime() -
                        new Date().getTimezoneOffset() * 60000
                    ),
                    PurchasedCost: element.PurchasedCost
                  });
                });
              }
            }
            this.hideDepreciationReportLoading();
          },
          error => {
            this.hideDepreciationReportLoading();
          }
        );
    }
  }
  //#endregion

  //#region "getAllInventoryDetails"
  getAllInventoryDetails(assetType: number) {
    this.showDepreciationReportLoading();

    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllInventories,
        'AssetType',
        assetType
      )
      .subscribe(
        data => {
          this.inventoryList = [];
          if (data.StatusCode === 200 && data.data.InventoryList != null) {
            if (data.data.InventoryList.length > 0) {
              data.data.InventoryList.forEach(element => {
                this.inventoryList.push(element);
              });
            }
          }
          this.hideDepreciationReportLoading();
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllInventoryDetails"
  getAllItemGroupDetails(inventoryId: any) {
    this.showDepreciationReportLoading();

    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllStoreItemGroups,
        'Id',
        inventoryId
      )
      .subscribe(
        data => {
          this.itemGroupList = [];
          if (data.StatusCode === 200 && data.data.storeItemGroupList != null) {
            if (data.data.storeItemGroupList.length > 0) {
              data.data.storeItemGroupList.forEach(element => {
                this.itemGroupList.push(element);
              });
            }
          }
          this.hideDepreciationReportLoading();
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllInventoryItemList"
  getAllInventoryItemList(groupId) {
    this.showDepreciationReportLoading();

    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllInventoryItems,
        'Id',
        groupId
      )
      .subscribe(
        data => {
          this.inventoryItemDataSource = [];
          if (data.StatusCode === 200 && data.data.InventoryItemList != null) {
            if (data.data.InventoryItemList.length > 0) {
              data.data.InventoryItemList.forEach(element => {
                this.inventoryItemDataSource.push(element);
              });
            }
          }
          this.hideDepreciationReportLoading();
        },
        error => {}
      );
  }
  //#endregion

  //#region "onApplyingFilter"
  onApplyingFilter(data: DepreciationReportFilterModel) {
    const dataModel: DepreciationReportFilterModel = {
      CurrentDate:
        data.CurrentDate != null
          ? new Date(
              new Date(data.CurrentDate).getFullYear(),
              new Date(data.CurrentDate).getMonth(),
              new Date(data.CurrentDate).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getMinutes()
            )
          : null,
      StoreId: data.StoreId,
      InventoryId: data.InventoryId,
      ItemId: data.ItemId,
      ItemGroupId: data.ItemGroupId
    };

    this.getDepreciationReportDetails(dataModel);
  }
  //#endregion

  //#region "onFieldDataChanged"
  onFieldDataChanged(event: any) {
    if (event.value != null && event.dataField === 'StoreId') {
      this.depreciationReportFilter.InventoryId = null;
      this.depreciationReportFilter.ItemId = null;

      this.getAllInventoryDetails(event.value);
    } else if (event.dataField === 'InventoryId') {
      this.itemDropDownDisable = true; // item dropdown disable

      if (event.value != null) {
        this.itemDropDownDisable = false; // item dropdown enable

        this.depreciationReportFilter.ItemId = null;
        this.getAllItemGroupDetails(event.value);
      }
    } else if (event.dataField === 'ItemGroupId') {
      this.itemDropDownDisable = true; // item dropdown disable

      if (event.value != null) {
        this.itemDropDownDisable = false; // item dropdown enable

        this.depreciationReportFilter.ItemId = null;
        this.getAllInventoryItemList(event.value);
      }
    }
  }
  //#endregion

  //#region "Loader"
  showDepreciationReportLoading() {
    this.depreciationReportLoading = true;
  }
  hideDepreciationReportLoading() {
    this.depreciationReportLoading = false;
  }
  //#endregion
}

class DepreciationReportModel {
  ItemName: string;
  PurchaseId: string;
  HoursSincePurchase: number;
  DepreciationRate: number;
  DepreciationAmount: number;
  CurrentValue: number;

  PurchaseDate: any;
  PurchasedCost: number;
}

class DepreciationReportFilterModel {
  StoreId: any;
  InventoryId: any;
  ItemId: any;
  ItemGroupId: any;
  CurrentDate: any;
}
