import { DashboardComponent } from './dashboard.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '', component: DashboardComponent,
        children: [
            // { path: '', redirectTo: 'project', pathMatch: 'full'},
            { path: 'project', loadChildren: './project-management/project-management.module#ProjectManagementModule' },
            { path: 'marketing', loadChildren: './marketing/marketing.module#MarketingModule' },
            { path: 'accounting', loadChildren: './accounting/accounting.module#AccountingModule' },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]   // important to export
})
export class DashboardRoutingModule {

}
