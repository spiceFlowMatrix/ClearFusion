import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HiringRequestsComponent } from './hiring-requests/hiring-requests.component';
import { JobDetailComponent } from './job-detail/job-detail.component';
import { InterviewDetailComponent } from './interview-detail/interview-detail.component';
import { EntryComponentComponent } from './entry-component/entry-component.component';
import { RequestDetailComponent } from './request-detail/request-detail.component';

const routes: Routes = [
  {
    path: '', component: EntryComponentComponent,
    children: [
      { path: 'job-detail', component: JobDetailComponent },
      { path: 'interview-detail', component: InterviewDetailComponent },
      // { path: 'interview-detail/:id', component: InterviewDetailComponent },
      { path: 'requests', component: HiringRequestsComponent },
      { path: ':id', component: RequestDetailComponent },

    ]
  }

  // { path: 'interview-detail', component: InterviewDetailComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectHiringRoutingModule { }
