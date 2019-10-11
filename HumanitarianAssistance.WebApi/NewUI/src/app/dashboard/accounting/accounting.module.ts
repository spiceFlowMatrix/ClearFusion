import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountingComponent } from './accounting.component';
import { AccountingRoutingModule } from './accounting-routing.module';
import { VoucherService } from './vouchers/voucher.service';
import { JournalReportComponent } from './journal-report/journal-report.component';
import { LedgerStatementReportComponent } from './ledger-statement-report/ledger-statement-report.component';
import { TrialBalanceReportComponent } from './trial-balance-report/trial-balance-report.component';
import { MatInputModule, MatSelectModule, MatCardModule, MatDatepickerModule, MatButtonModule,
  MatDividerModule, MatIconModule, MatPaginatorModule } from '@angular/material';
import { ReactiveFormsModule } from '@angular/forms';
import { LibraryModule, SubHeaderTemplateModule } from 'projects/library/src/public_api';
import { SatDatepickerModule, SatNativeDateModule } from 'saturn-datepicker';
import { MatRangeDatepickerModule, MatRangeNativeDateModule } from 'mat-range-datepicker';

@NgModule({
  imports: [
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatDividerModule,
    MatIconModule,
    MatPaginatorModule,
    SatDatepickerModule,
    SatNativeDateModule,
    LibraryModule,
    SubHeaderTemplateModule,
    MatCardModule,
    CommonModule,
    ReactiveFormsModule,
    AccountingRoutingModule // Routing
  ],
  declarations: [
    AccountingComponent,
    JournalReportComponent,
    LedgerStatementReportComponent,
    TrialBalanceReportComponent,
  ],
  providers: [
    VoucherService
  ],
  exports: [],
  entryComponents: [
  ]
})
export class AccountingModule {}
