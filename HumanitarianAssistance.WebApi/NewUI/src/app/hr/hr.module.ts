import { AssignLeaveComponent } from './components/employee-leave/assign-leave/assign-leave.component';
import { EmployeeLeaveAddComponent } from './components/employee-leave/employee-leave-add/employee-leave-add.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HrRoutingModule } from './hr-routing.module';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { ShareLayoutModule } from '../shared/share-layout.module';
import { MatCardModule } from '@angular/material/card';
import { MatSidenavModule } from '@angular/material/sidenav';
import {
  LibraryModule,
  SubHeaderTemplateModule
} from 'projects/library/src/public_api';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDividerModule } from '@angular/material/divider';
import { EmployeeControlPanelComponent } from './components/employee-control-panel/employee-control-panel.component';
import { EmployeeHistoryComponent } from './components/employee-history/employee-history.component';
import { EmployeeLeaveComponent } from './components/employee-leave/employee-leave.component';
import { EmployeeAttendanceComponent } from './components/employee-attendance/employee-attendance.component';
import { EmployeeContractComponent } from './components/employee-contract/employee-contract.component';
import { EmployeeSalaryConfigComponent } from './components/employee-salary-config/employee-salary-config.component';
import { EmployeeResignationComponent } from './components/employee-resignation/employee-resignation.component';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';
import {
  MatTabsModule,
  MatDialogModule,
  MatFormFieldModule,
  MatSelectModule,
  MatOptionModule,
  MatTableModule,
  MatCheckboxModule,
  MatRadioModule
} from '@angular/material';
import { AddHistoricalLogComponent } from './components/employee-history/add-historical-log/add-historical-log.component';
import { AddEducationComponent } from './components/employee-history/add-education/add-education.component';
import { AddHistoryOutsideCountryComponent } from './components/employee-history/add-history-outside-country/add-history-outside-country.component';
import { AddCloseRelativeComponent } from './components/employee-history/add-close-relative/add-close-relative.component';
import { AddThreeReferenceDetailsComponent } from './components/employee-history/add-three-reference-details/add-three-reference-details.component';
import { AddOtherSkillsComponent } from './components/employee-history/add-other-skills/add-other-skills.component';
import { AddSalaryBudgetComponent } from './components/employee-history/add-salary-budget/add-salary-budget.component';
import { AddLanguageComponent } from './components/employee-history/add-language/add-language.component';
import { SatDatepickerModule, SatNativeDateModule } from 'saturn-datepicker';
import { AddSalaryConfigurationComponent } from './components/employee-salary-config/add-salary-configuration/add-salary-configuration.component';
import { AddBonusComponent } from './components/employee-salary-config/add-bonus/add-bonus.component';
import { AddFineComponent } from './components/employee-salary-config/add-fine/add-fine.component';
import { EmployeePensionComponent } from './components/employee-pension/employee-pension.component';
import { SeeDaysComponent } from './components/employee-leave/see-days/see-days.component';

@NgModule({
  declarations: [
    EmployeeListComponent,
    EntryComponentComponent,
    EmployeeControlPanelComponent,
    EmployeeHistoryComponent,
    EmployeeLeaveComponent,
    EmployeeAttendanceComponent,
    EmployeeContractComponent,
    EmployeeSalaryConfigComponent,
    EmployeeResignationComponent,
    EmployeeDetailComponent,
    AddHistoricalLogComponent,
    AddEducationComponent,
    AddHistoryOutsideCountryComponent,
    AddCloseRelativeComponent,
    AddThreeReferenceDetailsComponent,
    AddOtherSkillsComponent,
    AddSalaryBudgetComponent,
    AddLanguageComponent,
    EmployeeLeaveAddComponent,
    AssignLeaveComponent,
    AddSalaryConfigurationComponent,
    AddBonusComponent,
    AddFineComponent,
    EmployeePensionComponent,
    SeeDaysComponent
  ],
  imports: [
    MatFormFieldModule,
    MatSelectModule,
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
    MatTabsModule,
    MatDialogModule,
    // ConfigurationModule
    SatDatepickerModule,
    SatNativeDateModule,
    MatSelectModule,
    MatOptionModule,
    MatDialogModule,
    MatSelectModule,
    MatTableModule,
    MatCheckboxModule,
    MatRadioModule
  ],
  entryComponents: [
    EmployeeLeaveAddComponent,
    AssignLeaveComponent,
    AddHistoricalLogComponent,
    AddEducationComponent,
    AddHistoryOutsideCountryComponent,
    AddCloseRelativeComponent,
    AddThreeReferenceDetailsComponent,
    AddOtherSkillsComponent,
    AddSalaryBudgetComponent,
    AddLanguageComponent,
    AddSalaryConfigurationComponent,
    AddBonusComponent,
    AddFineComponent,
    SeeDaysComponent
  ]
})
export class HrModule {
  entryComponents: [];
}
