import { Component, OnInit, Input } from '@angular/core';
import { StoreService } from '../../store.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-unit-type-master',
  templateUrl: './unit-type-master.component.html',
  styleUrls: ['./unit-type-master.component.css']
})
export class UnitTypeMasterComponent implements OnInit {
  unitTypeMasterDataSource: UnitTypeModel[];
  @Input() isEditingAllowed: boolean;

  // loader
  unitTypeMasterLoading = false;

  constructor(
    private storeService: StoreService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.unitTypeMasterDataSource = [];
    this.getAllUnitTypeDetails();
  }

  //#region "getAllUnitTypeDetails"
  getAllUnitTypeDetails() {
    this.showHideUnitTypeMasterLoading(true);
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllPurchaseUnitType
      )
      .subscribe(
        data => {
          this.unitTypeMasterDataSource = [];

          if (
            data.StatusCode === 200 &&
            data.data.PurchaseUnitTypeList != null
          ) {
            if (data.data.PurchaseUnitTypeList.length > 0) {
              data.data.PurchaseUnitTypeList.forEach(element => {
                this.unitTypeMasterDataSource.push(element);
              });
            }
          }
          this.showHideUnitTypeMasterLoading(false);
        },
        error => {
          this.showHideUnitTypeMasterLoading(false);
        }
      );
  }
  //#endregion

  //#region "addUnitTypeDetail"
  addUnitTypeDetail(model: any) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddPurchaseUnitType,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Unit Type Added Successfully!');
            this.getAllUnitTypeDetails();
          } else {
            this.toastr.error(data.Message);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "editUnitTypeDetail"
  editUnitTypeDetail(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditPurchaseUnitType,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Unit Type Updated Successfully!');
            this.getAllUnitTypeDetails();
          } else {
            this.toastr.error(data.Message);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "deleteUnitTypeDetail"
  deleteUnitTypeDetail(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_DeletePurchaseUnitType,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Unit Type Deleted Successfully!');
            this.getAllUnitTypeDetails();
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
        const addDataModel: UnitTypeModel = {
          UnitTypeId: obj.data.UnitTypeId,
          UnitTypeName: obj.data.UnitTypeName
        };
        this.addUnitTypeDetail(addDataModel);
      }
    } else if (eventName === 'RowUpdating') {
      if (obj != null) {
        const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

        const editDataModel: UnitTypeModel = {
          UnitTypeId: value.UnitTypeId,
          UnitTypeName: value.UnitTypeName
        };
        this.editUnitTypeDetail(editDataModel);
      }
    } else if (eventName === 'RowRemoving') {
      if (obj != null) {
        const deleteDataModel: UnitTypeModel = {
          UnitTypeId: obj.data.UnitTypeId,
          UnitTypeName: obj.data.UnitTypeName
        };

        this.deleteUnitTypeDetail(deleteDataModel);
      }
    }
  }
  //#endregion

  //#region "Loader"
  showHideUnitTypeMasterLoading(flag: boolean) {
    this.unitTypeMasterLoading = flag;
  }
  //#endregion
}

class UnitTypeModel {
  UnitTypeId: number;
  UnitTypeName: string;
}
