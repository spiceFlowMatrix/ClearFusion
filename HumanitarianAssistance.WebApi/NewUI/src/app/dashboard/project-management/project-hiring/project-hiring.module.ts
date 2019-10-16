import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectHiringRoutingModule } from './project-hiring-routing.module';
import { HiringRequestsComponent } from './hiring-requests/hiring-requests.component';
import { JobDetailComponent } from './job-detail/job-detail.component';
import { SubHeaderTemplateModule, LibraryModule } from 'projects/library/src/public_api';
import { MatDividerModule, MatInputModule, MatCardModule } from '@angular/material';

@NgModule({
  declarations: [
    HiringRequestsComponent, JobDetailComponent
  ],
  imports: [
    CommonModule,
    ProjectHiringRoutingModule,
    SubHeaderTemplateModule,
    LibraryModule,
    MatDividerModule,
    MatInputModule,
    MatCardModule
  ],
  exports:[
    HiringRequestsComponent, JobDetailComponent
  ]
})
export class ProjectHiringModule { }
