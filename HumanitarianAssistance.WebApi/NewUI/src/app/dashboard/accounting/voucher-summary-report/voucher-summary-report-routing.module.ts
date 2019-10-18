import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VoucherSummaryReportComponent } from './voucher-summary-report.component';

const routes: Routes = [
  {
  path: '',
  component: VoucherSummaryReportComponent,
  // canActivate: [RoleGuardService],
  // data: {
  //   module: ModuleId,
  //   page: accountingNewMaster.ExchangeRates
  // }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VoucherSummaryReportRoutingModule { }
