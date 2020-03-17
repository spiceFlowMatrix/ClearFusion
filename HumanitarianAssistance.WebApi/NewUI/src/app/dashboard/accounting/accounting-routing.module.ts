import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountingComponent } from './accounting.component';
import {
  ApplicationModule,
  accountingNewMaster
} from 'src/app/shared/applicationpagesenum';
import { RoleGuardService } from 'src/app/shared/services/role-guard';
import { JournalReportComponent } from './journal-report/journal-report.component';
import { LedgerStatementReportComponent } from './ledger-statement-report/ledger-statement-report.component';
import { TrialBalanceReportComponent } from './trial-balance-report/trial-balance-report.component';
import { ExchangeGainLossReportComponent } from './exchange-gain-loss-report/exchange-gain-loss-report.component';

const ModuleId: number = ApplicationModule.AccountingNew;

const routes: Routes = [
  {
    path: '',
    component: AccountingComponent,
    children: [
      {
        path: 'chart-of-accounts',
        loadChildren: './chart-of-accounts/chart-of-accounts.module#ChartOfAccountsModule',
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          isModule: true
        }
      },
      {
        path: 'financial-report',
        loadChildren: './financial-report/financial-report.module#FinancialReportModule',
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          isModule: true
        }
      },
      {
        path: 'vouchers',
        loadChildren: './vouchers/vouchers.module#VouchersModule',
        // canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: accountingNewMaster.Vouchers
        }
      },
      {
        path: 'gain-loss-report',
        loadChildren: './gain-loss-report/gain-loss-report.module#GainLossReportModule',
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: accountingNewMaster.ExchangeGainLoss
        }
      },
      {
        path: 'exchange-rate',
        loadChildren: './exchange-rate/exchange-rate.module#ExchangeRateModule',
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: accountingNewMaster.ExchangeRates
        }
      },
      {
        path: 'journal-report',
        component: JournalReportComponent
      },
      {
        path: 'ledger-report',
        component: LedgerStatementReportComponent
      },
      {
        path: 'trial-balance',
        component: TrialBalanceReportComponent
      },
      {
        path: 'exchange-gain-loss-report',
        component: ExchangeGainLossReportComponent
      }

      // {
      //   path: 'voucher-summary-report',
      //   loadChildren: './voucher-summary-report/voucher-summary-report.module#VoucherSummaryReportModule',
      //   canActivate: [RoleGuardService],
      //   data: {
      //     module: ModuleId,
      //     page: accountingNewMaster.VoucherSummaryReport
      //   }
      // }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule] // important to export
})
export class AccountingRoutingModule {}
