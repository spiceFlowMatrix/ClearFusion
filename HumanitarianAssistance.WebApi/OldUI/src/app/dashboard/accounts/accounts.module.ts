import { NgModule } from '@angular/core';
import { MultiSelectModule } from 'primeng/components/multiselect/multiselect';
import { DropdownModule } from 'primeng/components/dropdown/dropdown';
import { CommonModule, DatePipe } from '@angular/common';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { AccountsComponent } from './accounts.component';
import { VouchersComponent } from './vouchers/vouchers.component';
import { AccountsService } from './accounts.service';
import {
  DxDataGridModule,
  DxSelectBoxModule,
  DxCheckBoxModule,
  DxFormModule,
  DxButtonModule,
  DxNumberBoxModule,
  DxPopupModule,
  DxTemplateModule,
  DxFileUploaderModule,
  DxTabsModule,
  DxTreeListModule,
  DxPivotGridModule,
  DxLookupModule,
  DxTextBoxModule,
  DxTileViewModule,
  DxLoadIndicatorModule,
  DxBoxModule,
  DxListModule,
  DxPopoverModule,
  DxTagBoxModule,
  DxScrollViewModule
} from 'devextreme-angular';
import { JournalComponent } from './journal/journal.component';
import { TrailBalanceComponent } from './trialBalance/trialBalance.component';
import { LedgerComponent } from './ledger/ledger.component';
import { FinancialReportComponent } from './financial-report/financial-report.component';
import { AccountsRoutingModule } from './accounts-routing.module';
import { BudgetBalanceComponent } from './budget-balance/budget-balance.component';
import { TransactionComponent } from './vouchers/transaction/transaction.component';
import { DocumentComponent } from './vouchers/document/document.component';
import { CodeService } from '../code/code.service';
import { PmuService } from '../pmu/pmu.service';
import { ProjectsService } from '../pmu/projects/projects.service';
import { HrService } from '../hr/hr.service';
import { BROWSER_SANITIZATION_PROVIDERS } from '@angular/platform-browser/src/browser';
import { CalendarModule } from 'primeng/primeng';
import { CurrencyExchangeAnalysisComponent } from './currency-exchange-analysis/currency-exchange-analysis.component';
import { CategoryPopulatorComponent } from './category-populator/category-populator.component';
import { NgxPermissionsModule } from 'ngx-permissions';
import { BalanceSheetComponent } from './category-populator/balance-sheet/balance-sheet.component';
import { IncomeExpenditureReportComponent } from './category-populator/income-expenditure-report/income-expenditure-report.component';
import { DetailsOfNoteComponent } from './financial-report/details-of-note/details-of-note.component';
import { ExchangeGainLossComponent } from './exchange-gain-loss/exchange-gain-loss.component';
import { ExchangeGainLossFilter } from './exchange-gain-loss/exchange-gain-loss-filter';
import { ExchangeGainLossTransactionComponent } from './exchange-gain-loss-transaction/exchange-gain-loss-transaction.component';
import { PensionPaymentsComponent } from './pension-payments/pension-payments.component';
import { CommonService } from '../../service/common.service';

@NgModule({
  declarations: [
    VouchersComponent,
    AccountsComponent,
    JournalComponent,
    DocumentComponent,
    TrailBalanceComponent,
    LedgerComponent,
    FinancialReportComponent,
    BudgetBalanceComponent,
    TransactionComponent,
    CurrencyExchangeAnalysisComponent,
    CategoryPopulatorComponent,
    BalanceSheetComponent,
    IncomeExpenditureReportComponent,
    DetailsOfNoteComponent,
    ExchangeGainLossComponent,
    ExchangeGainLossTransactionComponent,
    PensionPaymentsComponent
  ],
  imports: [
    CommonModule,
    AccountsRoutingModule,
    DxFileUploaderModule,
    DxTreeListModule,
    DxDataGridModule,
    DxBoxModule,
    DxSelectBoxModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxButtonModule,
    DxFormModule,
    DxTemplateModule,
    DxPivotGridModule,
    DxListModule,
    DxPopupModule,
    DxPopoverModule,
    DxTabsModule,
    DxLookupModule,
    DxTextBoxModule,
    DxTileViewModule,
    FormsModule,
    ReactiveFormsModule,
    DxTagBoxModule,
    CalendarModule,
    DxScrollViewModule,

    // NgxPermissionsModule.forChild({
    //   permissionsIsolate: true,
    //   rolesIsolate: true}),

    TranslateModule.forChild({}),
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)',
      backdropBorderRadius: '4px',
      primaryColour: '#31c3aa',
      secondaryColour: '#000',
      tertiaryColour: '#a129'
    })
  ],
  providers: [
    AccountsService,
    CommonService,
    CodeService,
    DatePipe,
    PmuService,
    ProjectsService,
    HrService,
    CodeService
    // ExchangeGainLossFilter
  ]
})
export class AccountsModule {}
