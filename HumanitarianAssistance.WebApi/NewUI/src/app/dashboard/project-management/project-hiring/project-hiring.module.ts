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
  MatTableModule,
  MatCheckboxModule,
  MatExpansionModule,
  MatSlideToggleModule
} from '@angular/material';
import { RequestDetailComponent } from './request-detail/request-detail.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AddHiringRequestComponent } from './add-hiring-request/add-hiring-request.component';
import { AddNewCandidateComponent } from './add-new-candidate/add-new-candidate.component';
import { InterviewDetailComponent } from './interview-detail/interview-detail.component';
import { AddNewLanguageComponent } from './interview-detail/add-new-language/add-new-language.component';
import { AddNewTraningComponent } from './interview-detail/add-new-traning/add-new-traning.component';
import { AddNewInterviewerComponent } from './interview-detail/add-new-interviewer/add-new-interviewer.component';
import { EntryComponentComponent } from './entry-component/entry-component.component';
import { CandidateTableComponent } from './candidate-table/candidate-table.component';

@NgModule({
  declarations: [
    HiringRequestsComponent,
    JobDetailComponent,
    RequestDetailComponent,
    AddHiringRequestComponent,
    AddNewCandidateComponent,
    InterviewDetailComponent,
    AddNewLanguageComponent,
    AddNewTraningComponent,
    AddNewInterviewerComponent,
    InterviewDetailComponent,
    EntryComponentComponent,
    CandidateTableComponent
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
    MatButtonModule,
    MatTableModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatSlideToggleModule
  ],
  exports: [HiringRequestsComponent, JobDetailComponent],
  entryComponents: [
    AddHiringRequestComponent,
    AddNewCandidateComponent,
    AddNewLanguageComponent,
    AddNewTraningComponent,
    AddNewInterviewerComponent
  ]
})
export class ProjectHiringModule {}
