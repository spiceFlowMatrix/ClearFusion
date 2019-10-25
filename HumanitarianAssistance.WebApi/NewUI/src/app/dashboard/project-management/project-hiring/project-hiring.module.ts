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
  MatButtonModule,
} from '@angular/material';
import { RequestDetailComponent } from './request-detail/request-detail.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AddHiringRequestComponent } from './add-hiring-request/add-hiring-request.component';
import { AddNewCandidateComponent } from './add-new-candidate/add-new-candidate.component';

@NgModule({
  declarations: [
    HiringRequestsComponent, JobDetailComponent, RequestDetailComponent, AddHiringRequestComponent, AddNewCandidateComponent
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
    MatDatepickerModule,
    MatButtonModule
  ],
  exports: [HiringRequestsComponent, JobDetailComponent],
  entryComponents: [
    AddHiringRequestComponent,
    AddNewCandidateComponent]
})
export class ProjectHiringModule {}
