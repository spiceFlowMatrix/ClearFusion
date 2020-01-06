import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogisticRequestsComponent } from './logistic-requests/logistic-requests.component';
import { LogisticRequestDetailsComponent } from './logistic-request-details/logistic-request-details.component';
import { AddLogisticRequestComponent } from './add-logistic-request/add-logistic-request.component';

const routes: Routes = [
  // {
  //   path: '', component: LogisticRequestsComponent,
  //   children: [
  //     {
  //       path: ':id', component: LogisticRequestDetailsComponent
  //     },
  //     {
  //       path: 'new-request', component: AddLogisticRequestComponent
  //     }
  //   ]
  // },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectLogisticsRoutingModule { }
