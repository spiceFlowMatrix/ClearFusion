import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectHiringRoutingModule } from './project-hiring-routing.module';
import { HiringRequestsComponent } from './hiring-requests/hiring-requests.component';
import { JobDetailComponent } from './job-detail/job-detail.component';
import {
  SubHeaderTemplateModule,
  LibraryModule
} from 'projects/library/src/public_api';
import {
  MatDividerModule,
  MatInputModule,
  MatCardModule,
  MatPaginatorModule,
  MatTabsModule,
  MatDialogModule,
  MatIconModule,
  MatFormFieldModule,
  MatSelectModule,
  MatDatepickerModule,
} from '@angular/material';
import { RequestDetailComponent } from './request-detail/request-detail.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AddHiringRequestComponent } from './add-hiring-request/add-hiring-request.component';

@NgModule({
  declarations: [
    HiringRequestsComponent, JobDetailComponent, RequestDetailComponent, AddHiringRequestComponent
  ],
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    ProjectHiringRoutingModule,
    SubHeaderTemplateModule,
    LibraryModule,
    MatDividerModule,
    MatInputModule,
    MatCardModule,
    MatPaginatorModule,
    MatTabsModule,
    MatDialogModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDatepickerModule
  ],
  exports: [HiringRequestsComponent, JobDetailComponent],
  entryComponents: [
    AddHiringRequestComponent]
})
export class ProjectHiringModule {}
