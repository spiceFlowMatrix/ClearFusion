import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LogisticService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private dialog: MatDialog,
  ) { }

  goodsRecievedChange$ = new BehaviorSubject(false);
  VoucherReference$ = new BehaviorSubject('');

  addLogisticRequest(value) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_AddLogisticRequest,
      value
    );
  }

  getAllLogisticRequests(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetAllLogisticRequest,
      model
    );
  }

  deleteLogisticRequestById(RequestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_DeleteLogisticRequest,
      RequestId
    );
  }

  deleteLogisticRequestItemsById(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_DeleteLogisticRequestItem,
      model
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

  submitPurchaseOrder(value) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_SubmitPurchaseOrder,
      value
    );
  }

  getPurchasedItemsList(RequestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetPurchasedItemsList,
      RequestId
    );
  }

  GetAllCurrencyCodeList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency
    );
  }

  GetAllOfficeCodeList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice
    );
  }

  GetBudgetLineListByProjectId(ProjectId) {
    return this.globalService.getListById(
      this.appurl.getApiUrl() +
        GLOBAL.API_BudgetLine_GetProjectBudgetLineDetail,
        ProjectId
    );
  }

  cancelComparativeRequest(RequestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_CancelComparativeRequest,
        RequestId
    );
  }

  IssueComparativeStatement(RequestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_IssueComparativeStatement,
        RequestId
    );
  }

  addSuppliers(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_AddLogisticSupplier,
        model
    );
  }

  getSuppliersList(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_GetLogisticSupplierList,
        requestId
    );
  }

  deleteSupplierById(supplierId) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_DeleteLogisticSupplier,
        supplierId
    );
  }

  editSuppliers(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_EditLogisticSupplier,
        model
    );
  }

  SubmitComparativeStatement(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_SubmitComparativeStatement,
        model
    );
  }

  getComparativeStatement(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_GetComparativeStatement,
        requestId
    );
  }

  rejectComparativeStatement(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_RejectComparativeStatement,
        requestId
    );
  }

  approveComparativeStatement(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_ApproveComparativeStatement,
        requestId
    );
  }

  rejectPurchaseOrder(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_RejectPurchaseOrder,
        requestId
    );
  }

  getGoodsRecievedNote(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_ProjectLogistics_GetGoodsRecievedNote,
        requestId
    );
  }

  editLogisticRequest(value) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_EditLogisticRequest,
      value
    );
  }

  getJournalList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllJournalDetail
    );
  }

  GetAccountDetails() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Accounting_GetAccountDetails
    );
  }

  getPurchaseOrderDetail(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetPurchaseOrderDetail,
      requestId
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

  verifyPurchaseOrder(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_VerifyPurchaseOrder,
      model
    );
  }

  // checkDefaultUnitType() {
  //   return this.globalService.getDataById(
  //     this.appurl.getApiUrl() + GLOBAL.API_StorePurchase_GetDefaultUnitTypeByItemId,
  //   );
  // }

  getCompletedPurchaseOrderDetail(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetCompletedPurchaseOrderDetail,
      requestId
    );
  }

  rejectTenderRequest(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_RejectTenderRequest,
      requestId
    );
  }

  initiateTenderRequest(requestId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_InitiateTenderRequest,
      requestId
    );
  }

  getTenderIssuer(requestId) {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetTenderIssuerName + '?RequestId=' + requestId,
    );
  }

  getTenderProposalDocument(requestId) {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetTenderProposalDocument + '?RequestId=' + requestId,
    );
  }

  deleteTenderProposalDocument(documentFileId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_DeleteTenderProposalDocument,
      documentFileId
    );
  }

  addTenderBid(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_AddTenderBid,
      model
    );
  }

  getAllTenderBids(requestId) {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetAllTenderBids + '?RequestId=' + requestId,
    );
  }

  deleteTenderBidById(bidId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_DeleteTenderBidById,
      bidId
    );
  }

  editTenderBid(model) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_EditTenderBid,
      model
    );
  }

  selectTenderBid(BidId) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_SelectTenderBid,
      BidId
    );
  }

  getSelectedBidDetail(requestId) {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectLogistics_GetSelectedBidDetail + '?RequestId=' + requestId,
    );
  }
}
