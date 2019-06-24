import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { NgxPermissionsGuard } from 'ngx-permissions';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      {
        path: 'accounts',
        loadChildren: './accounts/accounts.module#AccountsModule',
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            // only: ['Admin', 'SuperAdmin', 'Accounting Manager'],
            // except: ['Guest']
          }
        }
        // redirectTo :{}
      },
      {
        path: 'user',
        loadChildren: './user/user.module#UserModule'
      },
      { path: 'code', loadChildren: './code/code.module#CodeModule' },
      {
        path: 'pmu',
        loadChildren: './pmu/pmu.module#PMUModule',
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            // only: ['Admin', 'SuperAdmin', 'Project Manager'],
            // except: ['Guest']
          }
        }
      },
      {
        path: 'project-management',
        loadChildren:
          './project-management/project-management.module#ProjectManagementModule',
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            // only: ['Admin', 'SuperAdmin', 'Project Manager'],
            // except: ['Guest']
          }
        }
      },
      {
        path: 'hr',
        loadChildren: './hr/hr.module#HrModule',
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            // only: ['Admin', 'SuperAdmin', 'HR Manager'],
            // except: ['Guest']
          }
        }
      },
      {
        path: 'store',
        loadChildren: './store/store.module#StoreModule',
        canActivate: [NgxPermissionsGuard],
        data: {
          permissions: {
            // only: ['Admin', 'SuperAdmin', 'HR Manager'],
            // except: ['Guest']
          }
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule {}
