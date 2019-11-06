import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectLogisticsRoutingModule } from './project-logistics-routing.module';
import { LogisticRequestsComponent } from './logistic-requests/logistic-requests.component';
import { MatDividerModule, MatInputModule, MatCardModule, MatPaginatorModule, MatDialogRef, MatTableModule, MatCheckboxModule } from '@angular/material';
import { SubHeaderTemplateModule, LibraryModule } from 'projects/library/src/public_api';
import { AddLogisticRequestComponent } from './add-logistic-request/add-logistic-request.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LogisticRequestDetailsComponent } from './logistic-request-details/logistic-request-details.component';
import { AddLogisticItemsComponent } from './add-logistic-items/add-logistic-items.component';
import { RequestStatusComponent } from './request-status/request-status.component';
import { SubmitPurchaseListComponent } from './submit-purchase-list/submit-purchase-list.component';

@NgModule({
  declarations: [LogisticRequestsComponent, AddLogisticRequestComponent,
    LogisticRequestDetailsComponent, AddLogisticItemsComponent, RequestStatusComponent, SubmitPurchaseListComponent],
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
    ReactiveFormsModule
  ],
  entryComponents: [
    AddLogisticRequestComponent,
    AddLogisticItemsComponent,
    SubmitPurchaseListComponent
  ],
})
export class ProjectLogisticsModule { }
