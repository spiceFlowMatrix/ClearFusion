import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HrRoutingModule } from './hr-routing.module';
import { ConfigurationModule } from './configuration/configuration.module';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { ShareLayoutModule } from '../shared/share-layout.module';
import { MatCardModule } from '@angular/material/card';
import { MatSidenavModule } from '@angular/material/sidenav';
import { LibraryModule, SubHeaderTemplateModule } from 'projects/library/src/public_api';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';
import { EmployeeControlPanelComponent } from './components/employee-control-panel/employee-control-panel.component';
import { EmployeeHistoryComponent } from './components/employee-history/employee-history.component';
import { EmployeeLeaveComponent } from './components/employee-leave/employee-leave.component';
import { EmployeeAttendanceComponent } from './components/employee-attendance/employee-attendance.component';
import { EmployeeContractComponent } from './components/employee-contract/employee-contract.component';
import { EmployeeSalaryConfigComponent } from './components/employee-salary-config/employee-salary-config.component';
import { EmployeeResignationComponent } from './components/employee-resignation/employee-resignation.component';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';
import { MatTabsModule } from '@angular/material';
import { EmployeeLeaveAddComponent } from './components/employee-leave-add/employee-leave-add.component';

@NgModule({
  declarations: [EmployeeListComponent, EntryComponentComponent, EmployeeControlPanelComponent, EmployeeHistoryComponent, EmployeeLeaveComponent, EmployeeAttendanceComponent, EmployeeContractComponent, EmployeeSalaryConfigComponent, EmployeeResignationComponent, EmployeeDetailComponent, EmployeeLeaveAddComponent],
  imports: [
    CommonModule,
    HrRoutingModule,
    ShareLayoutModule,
    MatCardModule,
    MatSidenavModule,
    LibraryModule,
    SubHeaderTemplateModule,
    ReactiveFormsModule,
    FormsModule,
    // Material
    MatMenuModule,
    MatIconModule,
    MatSidenavModule,
    MatCardModule,
    MatButtonModule,
    ShareLayoutModule,
    SubHeaderTemplateModule,
    LibraryModule,
    MatDatepickerModule,
    MatInputModule,
    MatPaginatorModule,
    MatDividerModule,
    MatTabsModule
   // ConfigurationModule
  ]
})
export class HrModule {
  entryComponents: [];
}
