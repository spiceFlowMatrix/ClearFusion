import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChartOfAccountsRoutingModule } from './chart-of-accounts-routing.module';
import { AddAccountComponent } from './add-account/add-account.component';
import { AssetsComponent } from './assets/assets.component';
import { ExpenseComponent } from './expense/expense.component';
import { FundsComponent } from './funds/funds.component';
import { IncomeComponent } from './income/income.component';
import { LiabilitiesComponent } from './liabilities/liabilities.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { TooltipModule } from 'primeng/primeng';
import { ChartOfAccountsComponent } from './chart-of-accounts.component';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    ChartOfAccountsComponent,
    AddAccountComponent,
    AssetsComponent,
    ExpenseComponent,
    FundsComponent,
    IncomeComponent,
    LiabilitiesComponent
  ],
  imports: [
    CommonModule,
    ChartOfAccountsRoutingModule,
    FormsModule,
    ReactiveFormsModule,

    // material
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatDialogModule,
    MatIconModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    TooltipModule
  ],
  entryComponents: [
    AddAccountComponent
  ]
})
export class ChartOfAccountsModule { }
