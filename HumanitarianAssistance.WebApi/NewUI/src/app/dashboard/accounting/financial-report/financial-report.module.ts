import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FinancialReportRoutingModule } from './financial-report-routing.module';
import { BalanceSheetViewComponent } from './balance-sheet/balance-sheet-view/balance-sheet-view.component';
import { BalanceSheetService } from './balance-sheet/balance-sheet.service';
import { BalanceSheetComponent } from './balance-sheet/balance-sheet.component';
import { IncomeExpenseViewComponent } from './income-expense/income-expense-view/income-expense-view.component';
import { IncomeExpenseService } from './income-expense/income-expense.service';
import { IncomeExpenseComponent } from './income-expense/income-expense.component';

import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { TooltipModule } from 'primeng/primeng';
import { FinancialReportComponent } from './financial-report.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PipeExportModule } from '../../../shared/pipes/pipe-export/pipe-export.module';
import { LibraryModule } from '../../../../../projects/library/src/public_api';
import { CurrencyCodePipe } from '../../../shared/pipes/currency-code.pipe';

@NgModule({
  declarations: [
    FinancialReportComponent,
    BalanceSheetViewComponent,
    BalanceSheetComponent,
    IncomeExpenseViewComponent,
    IncomeExpenseComponent
  ],
  imports: [
    CommonModule,
    FinancialReportRoutingModule,
    FormsModule,
    ReactiveFormsModule,

    // Custome pipe
    PipeExportModule,

    // Custom Modules
    LibraryModule,

    // material
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatSelectModule,
    NgxMatSelectSearchModule,
    MatProgressSpinnerModule,
    TooltipModule
  ],
  providers: [
    CurrencyCodePipe,
    BalanceSheetService,
    IncomeExpenseService
  ],
  entryComponents: [

  ]
})
export class FinancialReportModule { }
