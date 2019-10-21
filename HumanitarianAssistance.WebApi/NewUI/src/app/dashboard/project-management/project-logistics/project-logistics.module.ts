import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectLogisticsRoutingModule } from './project-logistics-routing.module';
import { LogisticRequestsComponent } from './logistic-requests/logistic-requests.component';
import { MatDividerModule, MatInputModule, MatCardModule, MatPaginatorModule } from '@angular/material';
import { SubHeaderTemplateModule, LibraryModule } from 'projects/library/src/public_api';
import { AddLogisticRequestComponent } from './add-logistic-request/add-logistic-request.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LogisticRequestDetailsComponent } from './logistic-request-details/logistic-request-details.component';
import { AddLogisticItemsComponent } from './add-logistic-items/add-logistic-items.component';
import { RequestStatusComponent } from './request-status/request-status.component';

@NgModule({
  declarations: [LogisticRequestsComponent, AddLogisticRequestComponent, LogisticRequestDetailsComponent, AddLogisticItemsComponent, RequestStatusComponent],
  imports: [
    CommonModule,
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
    AddLogisticItemsComponent
  ]
})
export class ProjectLogisticsModule { }
