import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VouchersComponent } from './vouchers.component';
import { VoucherListingComponent } from './components/voucher-listing/voucher-listing.component';
import { AddVoucherComponent } from './components/add-voucher/add-voucher.component';
import { VoucherDetailComponent } from './components/voucher-detail/voucher-detail.component';


const routes: Routes = [
  {
    path: '',
    component: VouchersComponent,
    children: [
      {
        path: '',
        component: VoucherListingComponent
      },
      {
        path: 'add-voucher',
        component: AddVoucherComponent
      },
      {
        path: ':id', component: VoucherDetailComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VouchersRoutingModule {}
