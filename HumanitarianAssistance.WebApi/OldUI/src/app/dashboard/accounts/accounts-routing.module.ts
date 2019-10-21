import { Routes, RouterModule } from '@angular/router';
import { AccountsComponent } from './accounts.component';
import { UserComponent } from '../user/user.component';
import { VouchersComponent } from './vouchers/vouchers.component';
import { JournalComponent } from './journal/journal.component';
import { LedgerComponent } from './ledger/ledger.component';
import { TrailBalanceComponent } from './trialBalance/trialBalance.component';
import { FinancialReportComponent } from './financial-report/financial-report.component';
import { NgModule } from '@angular/core';
import { BudgetBalanceComponent } from './budget-balance/budget-balance.component';
import { TransactionComponent } from './vouchers/transaction/transaction.component';
import { DocumentComponent } from './vouchers/document/document.component';
import { CurrencyExchangeAnalysisComponent } from './currency-exchange-analysis/currency-exchange-analysis.component';
import { CategoryPopulatorComponent } from './category-populator/category-populator.component';
import { ExchangeGainLossComponent } from './exchange-gain-loss/exchange-gain-loss.component';
import { ExchangeGainLossTransactionComponent } from './exchange-gain-loss-transaction/exchange-gain-loss-transaction.component';
import { PensionPaymentsComponent } from './pension-payments/pension-payments.component';
import {
  applicationPages,
  applicationModule
} from '../../shared/application-pages-enum';
import { RoleGuardService } from '../../service/role-guard.service';

const Accounting: any = {
  Vouchers: applicationPages.Vouchers,
  Journal: applicationPages.Journal,
  LedgerStatement: applicationPages.LedgerStatement,
  BudgetBalance: applicationPages.BudgetBalance,
  TrialBalance: applicationPages.TrialBalance,
  FinancialReport: applicationPages.FinancialReport,
  CategoryPopulator: applicationPages.CategoryPopulator,
  ExchangeGainLoss: applicationPages.ExchangeGainLoss,
  GainLossTransaction: applicationPages.GainLossTransaction,
  PensionPayments: applicationPages.PensionPayments
};

const AccountingModule: any = {
  ModuleId: applicationModule.Accounting
};

const Account_Router: Routes = [
  {
    path: '',
    component: AccountsComponent,
    children: [
      // { path: '', redirectTo: 'vouchers', pathMatch: 'full' },
      {
        path: 'vouchers',
        component: VouchersComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.Vouchers
        }
      },
      {
        path: 'journal',
        component: JournalComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.Journal
        }
      },
      { path: 'vouchers/document', component: DocumentComponent },
      { path: 'vouchers/transaction', component: TransactionComponent },
      {
        path: 'ledger',
        component: LedgerComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.LedgerStatement
        }
      },
      {
        path: 'budgetbalance',
        component: BudgetBalanceComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.BudgetBalance
        }
      },
      {
        path: 'trialbalance',
        component: TrailBalanceComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.TrialBalance
        }
      },
      {
        path: 'financialreport',
        component: FinancialReportComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.FinancialReport
        }
      },
      {
        path: 'currency-exchange-analysis',
        component: CurrencyExchangeAnalysisComponent
      },
      {
        path: 'category-populator',
        component: CategoryPopulatorComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.CategoryPopulator
        }
      },
      {
        path: 'exchange-gain-loss',
        component: ExchangeGainLossComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.ExchangeGainLoss
        }
      },
      {
        path: 'exchange-gain-loss-transaction',
        component: ExchangeGainLossTransactionComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.GainLossTransaction
        }
      },
      {
        path: 'vouchers/:id',
        component: VouchersComponent
      },
      {
        path: 'pension-payments',
        component: PensionPaymentsComponent,
        canActivate: [RoleGuardService],
        data: {
          module: 3,
          page: Accounting.PensionPayments
        }
      }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(Account_Router)],
  exports: [RouterModule]
})
export class AccountsRoutingModule {}
