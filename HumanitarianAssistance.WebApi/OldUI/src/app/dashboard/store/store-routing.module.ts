import { Routes, RouterModule } from '@angular/router';
import { StoreComponent } from './store.component';
import { NgModule } from '@angular/core';
import { StoreMasterComponent } from './store-master/store-master.component';
import { StoreMainComponent } from './store-main/store-main.component';
import { ProcurmentSummaryComponent } from './procurment-summary/procurment-summary.component';
import { StoreDepreciationReportComponent } from './store-depreciation-report/store-depreciation-report.component';
import { StoreSourceCodesComponent } from './store-source-codes/store-source-codes.component';
import { PaymentTypesComponent } from './payment-types/payment-types.component';
import {
  applicationPages,
  applicationModule
} from '../../shared/application-pages-enum';
import { RoleGuardService } from '../../service/role-guard.service';

const Store: any = {
  Categories: applicationPages.Categories,
  StoreSourceCodes: applicationPages.StoreSourceCodes,
  PaymentTypes: applicationPages.PaymentTypes,
  Store: applicationPages.Store,
  ProcurementSummary: applicationPages.ProcurementSummary,
  DepreciationReport: applicationPages.DepreciationReport
};

const StoreModule: any = {
  ModuleId: applicationModule.Store
};

const store_Router: Routes = [
  {
    path: '',
    component: StoreComponent,
    children: [
      {
        path: 'store-main',
        component: StoreMainComponent,
        canActivate: [RoleGuardService],
        data: {
          module: StoreModule.ModuleId,
          page: Store.Store
        }
      },
      {
        path: 'store-master',
        component: StoreMasterComponent,
        canActivate: [RoleGuardService],
        data: {
          module: StoreModule.ModuleId,
          page: Store.Categories
        }
      },
      {
        path: 'procurment-summary',
        component: ProcurmentSummaryComponent,
        canActivate: [RoleGuardService],
        data: {
          module: StoreModule.ModuleId,
          page: Store.ProcurementSummary
        }
      },
      {
        path: 'store-depreciation-report',
        component: StoreDepreciationReportComponent,
        canActivate: [RoleGuardService],
        data: {
          module: StoreModule.ModuleId,
          page: Store.DepreciationReport
        }
      },
      {
        path: 'store-source-codes',
        component: StoreSourceCodesComponent,
        canActivate: [RoleGuardService],
        data: {
          module: StoreModule.ModuleId,
          page: Store.StoreSourceCodes
        }
      },
      {
        path: 'payment-types',
        component: PaymentTypesComponent,
        canActivate: [RoleGuardService],
        data: {
          module: StoreModule.ModuleId,
          page: Store.PaymentTypes
        }
      }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(store_Router)],
  exports: [RouterModule]
})
export class StoreRoutingModule {}
