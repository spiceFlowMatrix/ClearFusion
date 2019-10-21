import { Component, OnInit } from '@angular/core';
import { StoreService } from '../../store.service';
import { GLOBAL } from '../../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService } from '../../../../service/common.service';

@Component({
  selector: 'app-inventory-item-master',
  templateUrl: './inventory-item-master.component.html',
  styleUrls: ['./inventory-item-master.component.css']
})
export class InventoryItemMasterComponent implements OnInit {
  //#region "variables"
  inventoryItemMasterDataSource: any[];
  inventoryMasterDataSource: any[];
  voucherDataSource: any[];
  itemTypeMasterDataSource: any[];

  //#endregion

  constructor(
    private storeService: StoreService,
    private setting: AppSettingsService,
    private commonService: CommonService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.inventoryMasterDataSource = [];

    this.getAllVoucherList();
    this.getAllInventoryDetails();
    this.getAllInventoryItemTypeDetails();

    this.getAllInventoryItemList();
  }

  //#region "getAllInventoryItemList"
  getAllInventoryItemList() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllInventoryItems
      )
      .subscribe(
        data => {
          this.inventoryItemMasterDataSource = [];
          if (data.StatusCode === 200 && data.data.InventoryItemList != null) {
            if (data.data.InventoryItemList.length > 0) {
              data.data.InventoryItemList.forEach(element => {
                this.inventoryItemMasterDataSource.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

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

              // sort in Asc
              this.inventoryMasterDataSource = this.commonService.sortDropdown(
                this.inventoryMasterDataSource,
                'InventoryName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllVoucherList"
  getAllVoucherList() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAllVoucherDetails
      )
      .subscribe(
        data => {
          this.voucherDataSource = [];
          if (data.StatusCode === 200 && data.data.VoucherDetailList != null) {
            if (data.data.VoucherDetailList.length > 0) {
              data.data.VoucherDetailList.forEach(element => {
                this.voucherDataSource.push(element);
              });

              // sort in Asc
              this.voucherDataSource = this.commonService.sortDropdown(
                this.voucherDataSource,
                'ReferenceNo'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllInventoryItemTypeDetails"
  getAllInventoryItemTypeDetails() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllInventoryItemsType
      )
      .subscribe(
        data => {
          this.itemTypeMasterDataSource = [];

          if (
            data.StatusCode === 200 &&
            data.data.InventoryItemTypeList != null
          ) {
            if (data.data.InventoryItemTypeList.length > 0) {
              data.data.InventoryItemTypeList.forEach(element => {
                this.itemTypeMasterDataSource.push(element);
              });
              // sort in Asc
              this.itemTypeMasterDataSource = this.commonService.sortDropdown(
                this.itemTypeMasterDataSource,
                'TypeName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "addInventoryItemDetail"
  addInventoryItemDetail(model: any) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddInventoryItems,
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

  //#region "editInventoryItemDetail"
  editInventoryItemDetail(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditInventoryItems,
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

  //#region "deleteInventoryItemDetail"
  deleteInventoryItemDetail(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_DeleteInventoryItems,
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
        const addDataModel: InventoryItemModel = {
          ItemId: obj.data.ItemId,
          ItemInventory: obj.data.ItemInventory,
          ItemName: obj.data.ItemName,
          ItemCode: obj.data.ItemCode,
          Description: obj.data.Description,
          Voucher: obj.data.Voucher,
          ItemType: obj.data.ItemType
        };

        this.addInventoryItemDetail(addDataModel);
      }
    } else if (eventName === 'RowUpdating') {
      if (obj != null) {
        const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

        const editDataModel: InventoryItemModel = {
          ItemId: value.ItemId,
          ItemInventory: value.ItemInventory,
          ItemName: value.ItemName,
          ItemCode: value.ItemCode,
          Description: value.Description,
          Voucher: value.Voucher,
          ItemType: value.ItemType
        };
        this.editInventoryItemDetail(editDataModel);
      }
    } else if (eventName === 'RowRemoving') {
      if (obj != null) {
        const deleteDataModel: InventoryItemModel = {
          ItemId: obj.data.ItemId,
          ItemInventory: obj.data.ItemInventory,
          ItemName: obj.data.ItemName,
          ItemCode: obj.data.ItemCode,
          Description: obj.data.Description,
          Voucher: obj.data.Voucher,
          ItemType: obj.data.ItemType
        };
        this.deleteInventoryItemDetail(deleteDataModel);
      }
    }
  }
  //#endregion
}

class InventoryItemModel {
  ItemId: any;
  ItemInventory: any;
  ItemName: any;
  ItemCode: any;
  Description: any;
  Voucher: any;
  ItemType: any;
}
