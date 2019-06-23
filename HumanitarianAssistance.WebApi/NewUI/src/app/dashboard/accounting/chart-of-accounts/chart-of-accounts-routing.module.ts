import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChartOfAccountsComponent } from './chart-of-accounts.component';
import { AssetsComponent } from './assets/assets.component';
import { RoleGuardService } from 'src/app/shared/services/role-guard';
import { accountingNewMaster, ApplicationModule } from 'src/app/shared/applicationpagesenum';
import { LiabilitiesComponent } from './liabilities/liabilities.component';
import { FundsComponent } from './funds/funds.component';
import { IncomeComponent } from './income/income.component';
import { ExpenseComponent } from './expense/expense.component';

const ModuleId: number = ApplicationModule.AccountingNew;

const routes: Routes = [
  {
    path: '',
    component: ChartOfAccountsComponent,
    children: [
      {
        path: 'assets',
        component: AssetsComponent,
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: accountingNewMaster.Assets
        }
      },
      {
        path: 'liabilities',
        component: LiabilitiesComponent,
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: accountingNewMaster.Liabilities
        }
      },
      { path: 'funds', component: FundsComponent },
      {
        path: 'income',
        component: IncomeComponent,
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: accountingNewMaster.Income
        }
      },
      {
        path: 'expense',
        component: ExpenseComponent,
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: accountingNewMaster.Expense
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChartOfAccountsRoutingModule {}
