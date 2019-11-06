import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogisticRequestsComponent } from './logistic-requests/logistic-requests.component';
import { LogisticRequestDetailsComponent } from './logistic-request-details/logistic-request-details.component';

const routes: Routes = [
  // { path: '', component: LogisticRequestsComponent },
  // { path: ':id/details', component: LogisticRequestDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectLogisticsRoutingModule { }
