import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeControlPanelComponent } from './components/employee-control-panel/employee-control-panel.component';

const routes: Routes = [
  {
    path: '', component: EntryComponentComponent,
    children: [
      { path: 'employees', component: EmployeeListComponent },
      { path: 'employee/:id', component: EmployeeControlPanelComponent }
    ]
  },
  {
    path: 'configuration',
    loadChildren: './configuration/configuration.module#ConfigurationModule'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HrRoutingModule { }
