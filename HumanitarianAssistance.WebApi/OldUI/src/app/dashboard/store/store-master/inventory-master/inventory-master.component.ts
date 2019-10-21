import { Component, OnInit } from '@angular/core';
import { GLOBAL } from '../../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { StoreService } from '../../store.service';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService } from '../../../../service/common.service';

@Component({
  selector: 'app-inventory-master',
  templateUrl: './inventory-master.component.html',
  styleUrls: ['./inventory-master.component.css']
})
export class InventoryMasterComponent implements OnInit {
  //#region "variables"
  inventoryMasterDataSource: InventoryModel[];
  accountsList: any[];

  assetTypeList = [
    {
      AssetTypeId: 1,
      AssetTypeName: 'Consumables'
    },
    {
      AssetTypeId: 2,
      AssetTypeName: 'Expendables'
    },
    {
      AssetTypeId: 3,
      AssetTypeName: 'Non-Expendables'
    }
  ];

  //#endregion

  constructor(
    private storeService: StoreService,
    private router: Router,
    private setting: AppSettingsService,
    private commonService: CommonService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.inventoryMasterDataSource = [];
    this.getAccountDetails();
    this.getAllInventoryDetails();
  }

  //#region "getAllInventoryDetails"
  getAllInventoryDetails() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllInventories
      )
      .subscribe(
        data => {
          this.inventoryMasterDataSource = [];

          if (data.StatusCode === 200 && data.data.InventoryList != null) {
            if (data.data.InventoryList.length > 0) {
              data.data.InventoryList.forEach(element => {
                this.inventoryMasterDataSource.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAccountDetails"
  getAccountDetails() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAccountDetails
      )
      .subscribe(
        data => {
          this.accountsList = [];
          if (data.StatusCode === 200 && data.data.AccountDetailList != null) {
            if (data.data.AccountDetailList.length > 0) {
              data.data.AccountDetailList.forEach(element => {
                this.accountsList.push({
                  AccountCode: element.AccountCode,
                  AccountName: element.AccountName
                });
              });

              // sort in Asc
              this.accountsList = this.commonService.sortDropdown(
                this.accountsList,
                'AccountName'
              );
            }
          }
        },
        error => {}
      );
  }

  //#endregion

  //#region "addInventoryDetail"
  addInventoryDetail(model: any) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddInventory,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Inventory Added Successfully!');
            this.getAllInventoryDetails();
          } else {
            this.toastr.error(data.Message);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "editInventoryDetail"
  editInventoryDetail(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditInventory,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Inventory Updated Successfully!');
            this.getAllInventoryDetails();
          } else {
            this.toastr.error(data.Message);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "deleteInventoryDetail"
  deleteInventoryDetail(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_DeleteInventory,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Inventory Deleted Successfully!');
            this.getAllInventoryDetails();
          } else {
            this.toastr.error(data.Message);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "logEvent"
  logEvent(eventName, obj) {
    if (eventName === 'RowInserting') {
      if (obj != null) {
        const addDataModel: InventoryModel = {
          InventoryId: obj.data.InventoryId,
          InventoryCode: obj.data.InventoryCode,
          InventoryName: obj.data.InventoryName,
          InventoryDescription: obj.data.InventoryDescription,
          InventoryDebitAccount: obj.data.InventoryDebitAccount,
          InventoryCreditAccount: obj.data.InventoryCreditAccount,
          AssetType: obj.data.AssetType
        };

        this.addInventoryDetail(addDataModel);
      }
    } else if (eventName === 'RowUpdating') {
      if (obj != null) {
        const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

        const editDataModel: InventoryModel = {
          InventoryId: value.InventoryId,
          InventoryCode: value.InventoryCode,
          InventoryName: value.InventoryName,
          InventoryDescription: value.InventoryDescription,
          InventoryDebitAccount: value.InventoryDebitAccount,
          InventoryCreditAccount: value.InventoryCreditAccount,
          AssetType: value.AssetType
        };
        this.editInventoryDetail(editDataModel);
      }
    } else if (eventName === 'RowRemoving') {
      if (obj != null) {
        const deleteDataModel: InventoryModel = {
          InventoryId: obj.data.InventoryId,
          InventoryCode: obj.data.InventoryCode,
          InventoryName: obj.data.InventoryName,
          InventoryDescription: obj.data.InventoryDescription,
          InventoryDebitAccount: obj.data.InventoryDebitAccount,
          InventoryCreditAccount: obj.data.InventoryCreditAccount,
          AssetType: obj.data.AssetType
        };

        this.deleteInventoryDetail(deleteDataModel);
      }
    }
  }
  //#endregion
}

class InventoryModel {
  InventoryId: any;
  InventoryCode: any;
  InventoryName: any;
  InventoryDescription: any;
  InventoryDebitAccount: any;
  InventoryCreditAccount: any;
  AssetType: any;
}
