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
  MatTabsModule
} from '@angular/material';
import { RequestDetailComponent } from './request-detail/request-detail.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    HiringRequestsComponent, JobDetailComponent, RequestDetailComponent
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
    MatTabsModule
  ],
  exports: [HiringRequestsComponent, JobDetailComponent]
})
export class ProjectHiringModule {}
