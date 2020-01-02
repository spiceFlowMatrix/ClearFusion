import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectLogisticsRoutingModule } from './project-logistics-routing.module';
import { LogisticRequestsComponent } from './logistic-requests/logistic-requests.component';
import { MatDividerModule, MatInputModule, MatCardModule, MatPaginatorModule,
  MatDialogRef, MatTableModule, MatCheckboxModule, MatDialogModule, MatExpansionModule,
  MatTabsModule, MatDatepickerModule, MatSlideToggleModule } from '@angular/material';
import { SubHeaderTemplateModule, LibraryModule } from 'projects/library/src/public_api';
import { AddLogisticRequestComponent } from './add-logistic-request/add-logistic-request.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LogisticRequestDetailsComponent } from './logistic-request-details/logistic-request-details.component';
import { AddLogisticItemsComponent } from './add-logistic-items/add-logistic-items.component';
import { RequestStatusComponent } from './request-status/request-status.component';
import { SubmitPurchaseListComponent } from './submit-purchase-list/submit-purchase-list.component';
import { ComparativeStatementComponent } from './comparative-statement/comparative-statement.component';
import { AddSupplierComponent } from './add-supplier/add-supplier.component';
import { PurchaseOrderComponent } from './purchase-order/purchase-order.component';
import { SubmitComparativeStatementComponent } from './submit-comparative-statement/submit-comparative-statement.component';
import { TenderStatementComponent } from './tender-statement/tender-statement.component';
import { PurchaseFinalCostComponent } from './purchase-final-cost/purchase-final-cost.component';
import { GoodsRecievedUploadComponent } from './goods-recieved-upload/goods-recieved-upload.component';
import { PurchaseVoucherVerificationComponent } from './purchase-voucher-verification/purchase-voucher-verification.component';
import { SubmitTenderDocumentComponent } from './submit-tender-document/submit-tender-document.component';
import { SubmitTenderBidComponent } from './submit-tender-bid/submit-tender-bid.component';

@NgModule({
  declarations: [LogisticRequestsComponent, AddLogisticRequestComponent,
    LogisticRequestDetailsComponent, AddLogisticItemsComponent, RequestStatusComponent,
    SubmitPurchaseListComponent, ComparativeStatementComponent, AddSupplierComponent,
    PurchaseOrderComponent, SubmitComparativeStatementComponent, TenderStatementComponent, PurchaseFinalCostComponent,
    GoodsRecievedUploadComponent,
    PurchaseVoucherVerificationComponent,
    SubmitTenderDocumentComponent, SubmitTenderBidComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatCheckboxModule,
    ProjectLogisticsRoutingModule,
    MatDividerModule,
    MatInputModule,
    MatCardModule,
    MatPaginatorModule,
    SubHeaderTemplateModule,
    LibraryModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatTabsModule,
    MatDatepickerModule,
    MatSlideToggleModule
  ],
  entryComponents: [
    PurchaseFinalCostComponent,
    AddLogisticItemsComponent,
    SubmitTenderDocumentComponent,
    AddSupplierComponent,
    SubmitComparativeStatementComponent,
    GoodsRecievedUploadComponent,
    PurchaseVoucherVerificationComponent,
    SubmitTenderBidComponent
  ]
})
export class ProjectLogisticsModule { }
