import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountingComponent } from './accounting.component';
import { AccountingRoutingModule } from './accounting-routing.module';
import { VoucherService } from './vouchers/voucher.service';
import { JournalReportComponent } from './journal-report/journal-report.component';
import { LedgerStatementReportComponent } from './ledger-statement-report/ledger-statement-report.component';
import { TrialBalanceReportComponent } from './trial-balance-report/trial-balance-report.component';
import { MatInputModule, MatSelectModule, MatCardModule, MatDatepickerModule, MatButtonModule,
  MatDividerModule, MatIconModule, MatPaginatorModule, MatTableModule, MatCheckboxModule } from '@angular/material';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { LibraryModule, SubHeaderTemplateModule } from 'projects/library/src/public_api';
import { SatDatepickerModule, SatNativeDateModule } from 'saturn-datepicker';
import { MatRangeDatepickerModule, MatRangeNativeDateModule } from 'mat-range-datepicker';
import { ExchangeGainLossReportComponent } from './exchange-gain-loss-report/exchange-gain-loss-report.component';
import { ConfigurationFilterComponent } from './exchange-gain-loss-report/configuration-filter/configuration-filter.component';
import { TransactionFilterComponent } from './exchange-gain-loss-report/transaction-filter/transaction-filter.component';
import { ConsolidateGainLossComponent } from './exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component';
import { ExchangeGainLossReportService } from './exchange-gain-loss-report/exchange-gain-loss-report.service';

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
    FormsModule,
    MatTableModule,
    MatCheckboxModule,
    AccountingRoutingModule // Routing
  ],
  declarations: [
    AccountingComponent,
    JournalReportComponent,
    LedgerStatementReportComponent,
    TrialBalanceReportComponent,
    ExchangeGainLossReportComponent,
    ConfigurationFilterComponent,
    TransactionFilterComponent,
    ConsolidateGainLossComponent,
  ],
  providers: [
    VoucherService,
    ExchangeGainLossReportService
  ],
  exports: [],
  entryComponents: [
  ]
})
export class AccountingModule {}
