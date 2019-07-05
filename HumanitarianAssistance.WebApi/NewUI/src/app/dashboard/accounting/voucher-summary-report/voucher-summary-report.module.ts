import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VoucherSummaryReportRoutingModule } from './voucher-summary-report-routing.module';
import { VoucherSummaryReportComponent } from './voucher-summary-report.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { VoucherSummaryFilterComponent } from './voucher-summary-filter/voucher-summary-filter.component';
import { VoucherSummaryReportService } from './voucher-summary-report.service';
import { ExchangeRateService } from '../exchange-rate/exchange-rate-listing/exchange-rate.service';
import { MatMenuModule } from '@angular/material/menu';
import {MatExpansionModule} from '@angular/material/expansion';
import { FormsModule } from '@angular/forms';
import { LibraryModule } from '../../../../../projects/library/src/public_api';

@NgModule({
  declarations: [
    VoucherSummaryReportComponent,
    VoucherSummaryFilterComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,

     // Custom Modules
     LibraryModule,

    // material
    MatButtonModule,
    MatCardModule,
    MatPaginatorModule,
    MatIconModule,
    MatSelectModule,
    MatMenuModule,
    MatProgressSpinnerModule,
    MatExpansionModule,
    VoucherSummaryReportRoutingModule
  ],
  providers: [
    VoucherSummaryReportService,
    ExchangeRateService
  ]
})
export class VoucherSummaryReportModule { }
