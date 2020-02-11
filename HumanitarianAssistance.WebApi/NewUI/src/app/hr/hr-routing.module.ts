import { AddEmployeeComponent } from './components/add-employee/add-employee.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeControlPanelComponent } from './components/employee-control-panel/employee-control-panel.component';
import { AddEmployeeAppraisalComponent } from './components/employee-appraisal/add-employee-appraisal/add-employee-appraisal.component';
import { EmployeeAppraisalComponent } from './components/employee-appraisal/employee-appraisal.component';

const routes: Routes = [
  {
    path: '',
    component: EntryComponentComponent,
    children: [
      {
        path: 'employee/:id',
        component: EmployeeControlPanelComponent
      },
      { path: 'employees', component: EmployeeListComponent },
      { path: 'addEmployee', component: AddEmployeeComponent },
      {
        path: 'employee/:id/addAppraisal',
        component: AddEmployeeAppraisalComponent
      },
      {
        path: 'employee/:id/employeeAppraisal',
        component: EmployeeAppraisalComponent
      }
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
export class HrRoutingModule {}
