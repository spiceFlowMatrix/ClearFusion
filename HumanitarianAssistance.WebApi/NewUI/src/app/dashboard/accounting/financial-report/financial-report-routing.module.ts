import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FinancialReportComponent } from './financial-report.component';
import { RoleGuardService } from 'src/app/shared/services/role-guard';
import { BalanceSheetComponent } from './balance-sheet/balance-sheet.component';
import { BalanceSheetViewComponent } from './balance-sheet/balance-sheet-view/balance-sheet-view.component';
import { IncomeExpenseComponent } from './income-expense/income-expense.component';
import { IncomeExpenseViewComponent } from './income-expense/income-expense-view/income-expense-view.component';
import { accountingNewMaster, ApplicationModule } from 'src/app/shared/applicationpagesenum';

const ModuleId: number = ApplicationModule.AccountingNew;

const routes: Routes = [
  {
    path: '',
    component: FinancialReportComponent,
    children: [
      {
        path: '', redirectTo: 'balance-sheet', pathMatch: 'full'
      },
      {
        path: 'balance-sheet',
        canActivate: [RoleGuardService],
        children: [
          { path: '', component: BalanceSheetComponent },
          {
            path: 'view/:asOfDate/:currency',
            component: BalanceSheetViewComponent
          }
        ]
      },
      {
        path: 'income-expense-report',
        children: [
          { path: '', component: IncomeExpenseComponent },
          {
            path: 'view/:asOfDate/:upToDate/:currency',
            component: IncomeExpenseViewComponent
          }
        ]
      },
      { path: '', redirectTo: 'balance-sheet' },
      {
        path: 'income-expense-report',
        component: IncomeExpenseComponent,
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: accountingNewMaster.IncomeExpenseReport
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FinancialReportRoutingModule {}
