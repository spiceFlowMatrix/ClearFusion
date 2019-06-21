import { Component, OnInit, Input } from '@angular/core';
import { StoreService } from '../../store.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-item-type-master',
  templateUrl: './item-type-master.component.html',
  styleUrls: ['./item-type-master.component.css']
})
export class ItemTypeMasterComponent implements OnInit {
  itemTypeMasterDataSource: InventoryItemTypeModel[];
  @Input() isEditingAllowed: boolean;

  // loader
  itemTypeMasterLoading = false;

  constructor(
    private storeService: StoreService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.itemTypeMasterDataSource = [];
    this.getAllInventoryItemTypeDetails();
  }

  //#region "getAllInventoryItemTypeDetails"
  getAllInventoryItemTypeDetails() {
    this.showHideitemTypeMasterLoading(true);
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
            }
          }
          this.showHideitemTypeMasterLoading(false);
        },
        error => {
          this.showHideitemTypeMasterLoading(false);
        }
      );
  }
  //#endregion

  //#region "addInventoryItemTypeDetail"
  addInventoryItemTypeDetail(model: any) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddInventoryItemsType,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Item Type Added Successfully!');
            this.getAllInventoryItemTypeDetails();
          } else {
            this.toastr.error(data.Message);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "editInventoryItemTypeDetail"
  editInventoryItemTypeDetail(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditInventoryItemsType,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Item Type Updated Successfully!');
            this.getAllInventoryItemTypeDetails();
          } else {
            this.toastr.error(data.Message);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "deleteInventoryItemTypeDetail"
  deleteInventoryItemTypeDetail(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_DeleteInventoryItemsType,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Item Type Deleted Successfully!');
            this.getAllInventoryItemTypeDetails();
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
        const addDataModel: InventoryItemTypeModel = {
          ItemType: obj.data.ItemType,
          TypeName: obj.data.TypeName
        };

        this.addInventoryItemTypeDetail(addDataModel);
      }
    } else if (eventName === 'RowUpdating') {
      if (obj != null) {
        const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

        const editDataModel: InventoryItemTypeModel = {
          ItemType: value.ItemType,
          TypeName: value.TypeName
        };
        this.editInventoryItemTypeDetail(editDataModel);
      }
    } else if (eventName === 'RowRemoving') {
      if (obj != null) {
        const deleteDataModel: InventoryItemTypeModel = {
          ItemType: obj.data.ItemType,
          TypeName: obj.data.TypeName
        };

        this.deleteInventoryItemTypeDetail(deleteDataModel);
      }
    }
  }
  //#endregion

  //#region "Loader"
  showHideitemTypeMasterLoading(flag: boolean) {
    this.itemTypeMasterLoading = flag;
  }
  //#endregion
}

class InventoryItemTypeModel {
  ItemType: number;
  TypeName: string;
}
