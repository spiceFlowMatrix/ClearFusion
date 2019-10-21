import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HiringRequestsComponent } from './hiring-requests/hiring-requests.component';
import { JobDetailComponent } from './job-detail/job-detail.component';

const routes: Routes = [
  { path: '', component: HiringRequestsComponent },
  { path: 'job-detail', component: JobDetailComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectHiringRoutingModule { }
