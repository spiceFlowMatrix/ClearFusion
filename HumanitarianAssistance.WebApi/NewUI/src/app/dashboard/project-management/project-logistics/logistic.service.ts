import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class LogisticService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private dialog: MatDialog,
  ) { }

  addLogisticRequest(value) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_AddLogisticRequest,
      value
    );
  }

  getAllLogisticRequests(projectId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetAllLogisticRequest,
      projectId
    );
  }

  deleteLogisticRequestById(RequestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_DeleteLogisticRequest,
      RequestId
    );
  }

  deleteLogisticRequestItemsById(ItemId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_DeleteLogisticRequestItem,
      ItemId
    );
  }

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

    return dialogRef.componentInstance.confirmDelete;
  }

  getLogisticRequestDetail(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetRequestDetailById,
      requestId
    );
  }

  getAllStoreItems() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Store_GetAllStoreInventoryItems
    );
  }

  addRequestItems(value) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_AddLogisticRequestItems,
      value
    );
  }

  editRequestItems(value) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_EditLogisticRequestItems,
      value
    );
  }

  getAllRequestItems(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetItemsByRequestId,
      requestId
    );
  }

  cancelLogisticRequest(RequestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_CancelLogisticRequest,
      RequestId
    );
  }

  issuePurchaseOrder(RequestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_IssuePurchaseOrder,
      RequestId
    );
  }
}
