import { Injectable } from '@angular/core';
import { GlobalService } from '../../shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { UnitType, SourceCode, MasterInventoryModel, MasterItemGroupModel, MasterInventoryItemModel } from '../models/store-configuration';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor(private globalService: GlobalService, private appurl: AppUrlService, private dialog: MatDialog) { }
  getUnitType() {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllPurchaseUnitType)
  }
  saveUnit(data: UnitType) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_AddPurchaseUnitType, data);
  }
  deleteUnit(data: UnitType) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_DeletePurchaseUnitType, data);
  }
  editUnit(data: UnitType) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_EditPurchaseUnitType, data);
  }


  getAllSourceCodeTypes() {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllStoreSourceType)
  }
  getSourceCodeById(id) {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllStoreSourceCode + '?typeId=' + id)
      .pipe(
        map(x => {
          return x.data.SourceCodeDatalist;
        })
      );
  }
  getAllStoreSource(): Observable<SourceCode[]> {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllStoreSourceCode)
      .pipe(
        map(x => {
          return x.data.SourceCodeDatalist;
        })
      );
  }
  getCodeByType(id) {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetStoreTypeCode + '?CodeTypeId=' + id + '')
  }
  saveCode(data: SourceCode) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_AddStoreSourceCode, data)
  }
  editCode(data: SourceCode) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_EditStoreSourceCode, data)
  }
  deleteCode(id) {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Store_DeleteStoreSourceCode + '?Id=' + id + '')
  }

  // Inventories
  getInventories(assettype) {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_GetInventories + '?AssetType=' + assettype)
  }
  getAllAccounts() {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Accounting_GetAccountDetails);
  }
  getGroupItemCode(inventoryId, assetType) {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetStoreGroupItemCode
      + '?Id=' + inventoryId + '&TypeId=' + assetType)
  }
  getItemCode(groupId, assetType) {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetInventoryItemCode
      + '?Id=' + groupId + '&TypeId=' + assetType)
  }
  getInventoryCode(id: number) {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Store_GetInventoryCode + '?Id=' + id)
  }
  AddMasterInventory(data: MasterInventoryModel) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_AddInventory, data);
  }
  AddItemGroup(data: MasterItemGroupModel) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_AddStoreItemGroup, data);
  }
  AddItem(data: MasterInventoryItemModel) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_AddInventoryItems, data);

  }
  EditMasterInventory(data: MasterInventoryModel) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_EditInventory, data);
  }
  EditItemGroup(data: MasterItemGroupModel) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_EditStoreItemGroup, data);
  }
  EditItem(data: MasterInventoryItemModel) {
    return this.globalService.post(this.appurl.getApiUrl() + GLOBAL.API_Store_EditInventoryItems, data);

  }


  // common methods
  openDeleteDialog() {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });

    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText = Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText = Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => { });

    return dialogRef.componentInstance.confirmDelete
  }

}
