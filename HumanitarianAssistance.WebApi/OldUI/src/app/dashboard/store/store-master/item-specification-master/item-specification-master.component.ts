import { Component, OnInit, Input } from '@angular/core';
import { StoreService } from '../../store.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CodeService } from '../../../code/code.service';

@Component({
  selector: 'app-item-specification-master',
  templateUrl: './item-specification-master.component.html',
  styleUrls: ['./item-specification-master.component.css']
})
export class ItemSpecificationMasterComponent implements OnInit {
  //#region "variables"
  itemSpecificationDataSource: any[];
  itemTypeMasterDataSource: InventoryItemTypeModel[];
  officeDropdownList: any[]= [];
  selectedOffice: null;

  selectedItemTypeValue = 0;
  @Input() isEditingAllowed: boolean;

  // loader
  itemSpecificationPopupLoading = false;

  //#endregion

  constructor(
    private storeService: StoreService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private codeservice: CodeService
  ) {}

  ngOnInit() {
    this.itemSpecificationDataSource = [];
    this.getAllInventoryItemTypeDetails();
    this.getOfficeCodeList();
  }

  //#region "getAllInventoryItemTypeDetails"
  getAllInventoryItemTypeDetails() {
    this.showItemSpecificationPopupLoading();
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

          this.hideItemSpecificationPopupLoading();
        },
        error => {
          this.hideItemSpecificationPopupLoading();
        }
      );
  }
  //#endregion

  //#region "Get all Office Details"
  getOfficeCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          if (
            data.StatusCode === 200 &&
            data.data.OfficeDetailsList.length > 0
          ) {
            const AllOffices = localStorage.getItem('ALLOFFICES') != null ? localStorage.getItem('ALLOFFICES').split(',') : [];

            data.data.OfficeDetailsList.forEach(element => {
              const officeFound = AllOffices.indexOf('' + element.OfficeId);
              if (officeFound !== -1) {
                this.officeDropdownList.push({
                  OfficeId: element.OfficeId,
                  OfficeCode: element.OfficeCode,
                  OfficeName: element.OfficeName,
                  SupervisorName: element.SupervisorName,
                  PhoneNo: element.PhoneNo,
                  FaxNo: element.FaxNo,
                  OfficeKey: element.OfficeKey
                });
              }

              this.selectedOffice = this.officeDropdownList[0].OfficeId;
            });

          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
  }
  //#endregion

  //#region "getAllItemSpecificationDetails"
  getAllItemSpecificationDetails(itemTypeId: number) {
    this.showItemSpecificationPopupLoading();

    // tslint:disable-next-line:radix
    const officeId = this.selectedOffice;

    if (officeId !== 0 && itemTypeId !== 0) {
      this.storeService
        .GetAllItemSpecification(
          this.setting.getBaseUrl() +
            GLOBAL.API_Store_GetItemSpecificationsMaster,
          officeId,
          itemTypeId
        )
        .subscribe(
          data => {
            this.itemSpecificationDataSource = [];

            if (
              data.StatusCode === 200 &&
              data.data.ItemSpecificationMasterList != null
            ) {
              if (data.data.ItemSpecificationMasterList.length > 0) {
                data.data.ItemSpecificationMasterList.forEach(element => {
                  this.itemSpecificationDataSource.push(element);
                });
              }
            }
            this.hideItemSpecificationPopupLoading();
          },
          error => {
            this.hideItemSpecificationPopupLoading();
          }
        );
    } else {
      this.toastr.warning('Office or Item type is not proper !');
    }
  }
  //#endregion

  //#region "selectedItemType"
  selectedItemType(e: any) {
    if (e != null || e !== 0) {
      this.selectedItemTypeValue = e;
      this.getAllItemSpecificationDetails(this.selectedItemTypeValue);
    }
  }
  //#endregion

  //#region "AddItemSpectification"
  AddItemSpectification(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_Store_AddItemSpecificationsMaster,
          model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Item Specification Added Successfully!!!');
          } else {
            this.toastr.error(data.Message);
          }
          this.getAllItemSpecificationDetails(this.selectedItemTypeValue);
        },
        error => {}
      );
  }
  //#endregion

  //#region "EditItemSpectification"
  EditItemSpectification(model) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_Store_EditItemSpecificationsMaster,
          model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Item Specification Updated Successfully!!!');
          }
          this.getAllItemSpecificationDetails(this.selectedItemTypeValue);
        },
        error => {}
      );
  }
  //#endregion

  //#region "logEvent"
  logEvent(eventName, obj) {
    if (eventName === 'RowInserting') {
      if (obj != null && this.selectedItemTypeValue !== 0) {
        const addDataModel: ItemSpecificationMasterModel = {
          ItemSpecificationMasterId: 0,
          ItemSpecificationField: obj.data.ItemSpecificationField,
          // tslint:disable-next-line:radix
          OfficeId: this.selectedOffice,
          ItemTypeId: this.selectedItemTypeValue
        };
        this.AddItemSpectification(addDataModel);
      }
    } else if (eventName === 'RowUpdating') {
      if (obj != null) {
        const value = Object.assign(obj.oldData, obj.newData);

        const editDataModel: ItemSpecificationMasterModel = {
          ItemSpecificationMasterId: value.ItemSpecificationMasterId,
          ItemSpecificationField: value.ItemSpecificationField,
          OfficeId: value.OfficeId,
          ItemTypeId: value.ItemTypeId
        };

        this.EditItemSpectification(editDataModel);
      }
    }
  }
  //#endregion

  //#region "Loader"
  showItemSpecificationPopupLoading() {
    this.itemSpecificationPopupLoading = true;
  }
  hideItemSpecificationPopupLoading() {
    this.itemSpecificationPopupLoading = false;
  }
  //#endregion

  onOfficeSelected(id) {
    this.selectedOffice = id;

    if (this.selectedItemTypeValue !== undefined) {
      this.getAllItemSpecificationDetails(this.selectedItemTypeValue);
    }
  }
}



class InventoryItemTypeModel {
  ItemType: number;
  TypeName: string;
}

class ItemSpecificationMasterModel {
  ItemSpecificationMasterId: number;
  ItemSpecificationField: number;
  OfficeId: number;
  ItemTypeId: any;
}
