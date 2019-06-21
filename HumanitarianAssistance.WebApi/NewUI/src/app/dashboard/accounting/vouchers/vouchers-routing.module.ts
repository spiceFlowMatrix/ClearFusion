import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VouchersComponent } from './vouchers.component';
import { VoucherListingComponent } from './voucher-listing/voucher-listing.component';

const routes: Routes = [
  {
    path: '',
    component: VouchersComponent,
    children: [
      {
        path: '',
        component: VoucherListingComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VouchersRoutingModule {}
