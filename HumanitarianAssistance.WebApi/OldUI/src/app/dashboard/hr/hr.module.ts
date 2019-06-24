import { NgModule } from '@angular/core';
import { HrComponent } from './hr.component';
import { EmployeesComponent } from './employees/employees.component';
import { HrRoutingModule } from './hr-routing.module';
import { PayrollMonthlyHoursComponent } from './payroll-monthly-hours/payroll-monthly-hours.component';
import { HrService } from './hr.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {
  DxDataGridModule,
  DxSelectBoxModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxButtonModule,
  DxFormModule,
  DxPopupModule,
  DxLookupModule,
  DxTemplateModule,
  DxDateBoxModule,
  DxTabPanelModule,
  DxTabsModule,
  DxFileUploaderModule,
  DxTextBoxModule,
  DxListModule,
  DxRadioGroupModule,
  DxSchedulerModule,
  DxSwitchModule,
  DxTileViewModule,
  DxScrollViewModule,
  DxMenuModule
} from 'devextreme-angular';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { CodeService } from '../code/code.service';
import { AccountsService } from '../accounts/accounts.service';
import { AttendanceComponent } from './attendance/attendance.component';
import { SchedulerComponent } from './employees/scheduler/scheduler.component';
import { SchedulerService } from './employees/scheduler/scheduler.service';
import { JobHiringDetailsComponent } from './job-hiring-details/job-hiring-details.component';
import { JobHiringService } from './job-hiring-details/job-hiring.service';
import { ScheduleInterviewComponent } from './schedule-interview/schedule-interview.component';
import { ScheduleInterviewService } from './schedule-interview/schedule-interview.service';
import { ApprovalsComponent } from './schedule-interview/approvals/approvals.component';
import { ApprovedEmployeeComponent } from './schedule-interview/approved-employee/approved-employee.component';
import { LeaveInfoComponent } from './employees/leave-info/leave-info.component';
import { PayrollComponent } from './employees/payroll/payroll.component';
import { ApproveLeaveComponent } from './approve-leave/approve-leave.component';
import { EmployeeSalaryComponent } from './employee-salary/employee-salary.component';
import { EmployeeSalaryService } from './employee-salary/employeee-salary.service';
import { HistoryComponent } from './employees/history/history.component';
import { ProfessionalInfoComponent } from './employees/professional-info/professional-info.component';
import { HealthInfoComponent } from './employees/health-info/health-info.component';
import { HolidaysComponent } from './holidays/holidays.component';
import { CalendarModule } from 'primeng/primeng';
import { MonthlyAttendanceReportComponent } from './employees/monthly-attendance-report/monthly-attendance-report.component';
import { PensionComponent } from './employees/pension/pension.component';
import { SummaryComponent } from './summary/summary.component';
import {
  TranslateService,
  TranslateModule,
  TranslateLoader
} from '@ngx-translate/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { EmployeeAppraisalComponent } from './employee-appraisal/employee-appraisal.component';
import { AdvancesComponent } from './advances/advances.component';
import { InterviewFormComponent } from './interview-form/interview-form.component';
import { EmployeeInterviewFormComponent } from './interview-form/employee-interview-form/employee-interview-form.component';
import { EmployeeExitInterviewFormComponent } from './interview-form/employee-exit-interview-form/employee-exit-interview-form.component';
import { ProjectsService } from '../pmu/projects/projects.service';
import { AdvanceDeductionComponent } from './advance-deduction/advance-deduction.component';
import { AnalyticalInfoComponent } from './employees/analytical-info/analytical-info.component';
import { ProfessionalMoreInfoComponent } from './employees/professional-info/professional-more-info/professional-more-info.component';
import { HealthComponent } from './employees/health/health.component';
import { ContractInfoComponent } from './employees/contract-info/contract-info.component';
import { EmployeeSalarySlipComponent } from './employee-salary/employee-salary-slip/employee-salary-slip.component';

@NgModule({
  declarations: [
    HrComponent,
    EmployeesComponent,
    PayrollMonthlyHoursComponent,
    AttendanceComponent,
    SchedulerComponent,
    JobHiringDetailsComponent,
    ScheduleInterviewComponent,
    ApprovalsComponent,
    ApprovedEmployeeComponent,
    LeaveInfoComponent,
    PayrollComponent,
    ApproveLeaveComponent,
    EmployeeSalaryComponent,
    HistoryComponent,
    ProfessionalInfoComponent,
    HealthInfoComponent,
    HolidaysComponent,
    MonthlyAttendanceReportComponent,
    PensionComponent,
    SummaryComponent,
    EmployeeAppraisalComponent,
    AdvancesComponent,
    InterviewFormComponent,
    EmployeeInterviewFormComponent,
    EmployeeExitInterviewFormComponent,
    AdvanceDeductionComponent,
    AnalyticalInfoComponent,
    ProfessionalMoreInfoComponent,
    HealthComponent,
    ContractInfoComponent,
    EmployeeSalarySlipComponent
  ],
  imports: [
    CalendarModule,
    HrRoutingModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    DxDataGridModule,
    DxSelectBoxModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxTabPanelModule,
    DxButtonModule,
    DxFormModule,
    DxPopupModule,
    DxDateBoxModule,
    DxLookupModule,
    DxTabsModule,
    DxTemplateModule,
    DxListModule,
    DxTextBoxModule,
    DxRadioGroupModule,
    DxSchedulerModule,
    DxSwitchModule,
    DxTileViewModule,
    DxScrollViewModule,
    DxMenuModule,
    DxFileUploaderModule,

    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.5)',
      backdropBorderRadius: '0px',
      fullScreenBackdrop: true,
      primaryColour: '#31c3aa',
      secondaryColour: '#000',
      tertiaryColour: '#a129'
    }),

    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    HrService,
    CodeService,
    AccountsService,
    SchedulerService,
    JobHiringService,
    ScheduleInterviewService,
    EmployeeSalaryService,
    ProjectsService,
    TranslateService,
    HttpClient
  ]
})
export class HrModule {}

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
