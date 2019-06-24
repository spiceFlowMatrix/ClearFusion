import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountingComponent } from './accounting.component';
import { AccountingRoutingModule } from './accounting-routing.module';
import { VoucherService } from './vouchers/voucher.service';

@NgModule({
  imports: [
    CommonModule,
    AccountingRoutingModule // Routing
  ],
  declarations: [
    AccountingComponent,
  ],
  providers: [
    VoucherService
  ],
  exports: [],
  entryComponents: [
  ]
})
export class AccountingModule {}
