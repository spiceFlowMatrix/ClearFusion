import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GainLossReportComponent } from './gain-loss-report.component';

const routes: Routes = [
  {
    path: '', component: GainLossReportComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GainLossReportRoutingModule { }
