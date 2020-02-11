import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
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
  MatRadioModule,
  MatProgressSpinnerModule
} from '@angular/material';
import { AddHistoricalLogComponent } from './components/employee-history/add-historical-log/add-historical-log.component';
import { AddEducationComponent } from './components/employee-history/add-education/add-education.component';
// tslint:disable-next-line: max-line-length
import { AddHistoryOutsideCountryComponent } from './components/employee-history/add-history-outside-country/add-history-outside-country.component';
import { AddCloseRelativeComponent } from './components/employee-history/add-close-relative/add-close-relative.component';
// tslint:disable-next-line: max-line-length
import { AddThreeReferenceDetailsComponent } from './components/employee-history/add-three-reference-details/add-three-reference-details.component';
import { AddOtherSkillsComponent } from './components/employee-history/add-other-skills/add-other-skills.component';
import { AddSalaryBudgetComponent } from './components/employee-history/add-salary-budget/add-salary-budget.component';
import { AddLanguageComponent } from './components/employee-history/add-language/add-language.component';
import { SatDatepickerModule, SatNativeDateModule } from 'saturn-datepicker';
// tslint:disable-next-line: max-line-length
import { AddSalaryConfigurationComponent } from './components/employee-salary-config/add-salary-configuration/add-salary-configuration.component';
import { AddBonusComponent } from './components/employee-salary-config/add-bonus/add-bonus.component';
import { AddFineComponent } from './components/employee-salary-config/add-fine/add-fine.component';
import { EmployeePensionComponent } from './components/employee-pension/employee-pension.component';
import { AddEmployeeComponent } from './components/add-employee/add-employee.component';
import { SetEmployeeAttendanceComponent } from './components/set-employee-attendance/set-employee-attendance.component';
import {NgxMaterialTimepickerModule} from '../../../node_modules/ngx-material-timepicker';
import { EmployeeAdvanceListComponent } from './components/employee_advance/employee-advance-list/employee-advance-list.component';
import { NewAdvanceRequestComponent } from './components/employee_advance/new-advance-request/new-advance-request.component';
import { AdvanceHistoryComponent } from './components/employee_advance/advance-history/advance-history.component';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { SeeDaysComponent } from './components/employee-leave/see-days/see-days.component';
import { AddContractComponent } from './components/employee-contract/add-contract/add-contract.component';
import { AddAdvanceRecoveryComponent } from './components/employee-salary-config/add-advance-recovery/add-advance-recovery.component';
import { AddOpeningPensionComponent } from './components/add-employee/add-opening-pension/add-opening-pension.component';
// tslint:disable-next-line: max-line-length
import { IncrementDecrementSalaryComponent } from './components/employee-salary-config/increment-decrement-salary/increment-decrement-salary.component';
import { AddHistoryOutsideOrganizationComponent } from './components/employee-history/add-history-outside-organization/add-history-outside-organization.component';
import { EditEmployeeAttendanceComponent } from './components/employee-attendance/edit-employee-attendance/edit-employee-attendance.component';
import { EmployeeTerminationComponent } from './components/employee-termination/employee-termination.component';
import {MatTooltipModule} from '@angular/material/tooltip';
import { AuditLogsComponent } from './components/audit-logs/audit-logs.component';
import { AdministerPayrollComponent } from './components/administer-payroll/administer-payroll.component';
import { EmployeeAnalyticalComponent } from './components/employee-analytical/employee-analytical.component';

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
    EmployeePensionComponent,
    AddEmployeeComponent,
    SetEmployeeAttendanceComponent,
    EmployeeAdvanceListComponent,
    NewAdvanceRequestComponent,
    AdvanceHistoryComponent,
    AddSalaryConfigurationComponent,
    AddBonusComponent,
    AddFineComponent,
    SeeDaysComponent,
    AddContractComponent,
    AddAdvanceRecoveryComponent,
    AddOpeningPensionComponent,
    IncrementDecrementSalaryComponent,
    AddHistoryOutsideOrganizationComponent,
    EditEmployeeAttendanceComponent,
    EmployeeTerminationComponent,
    AuditLogsComponent,
    AdministerPayrollComponent,
    EmployeeAnalyticalComponent
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
    MatRadioModule,
    MatProgressSpinnerModule,
    NgxMaterialTimepickerModule,
    MatTooltipModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    TranslateService,
    HttpClient,
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
    SetEmployeeAttendanceComponent,
    AddSalaryConfigurationComponent,
    AddBonusComponent,
    AddFineComponent,
    SeeDaysComponent,
    AddOpeningPensionComponent,
    IncrementDecrementSalaryComponent,
    NewAdvanceRequestComponent,
    AdvanceHistoryComponent,
    AddAdvanceRecoveryComponent,
    AddHistoryOutsideOrganizationComponent,
    EditEmployeeAttendanceComponent,
    EmployeeTerminationComponent,
    AdministerPayrollComponent
  ]
})
export class HrModule {
  entryComponents: [];
}
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
