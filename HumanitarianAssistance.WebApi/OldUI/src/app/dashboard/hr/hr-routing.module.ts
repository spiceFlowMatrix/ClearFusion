import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { HrComponent } from './hr.component';
import { EmployeesComponent } from './employees/employees.component';
import { PayrollMonthlyHoursComponent } from './payroll-monthly-hours/payroll-monthly-hours.component';
import { AttendanceComponent } from './attendance/attendance.component';
import { JobHiringDetailsComponent } from './job-hiring-details/job-hiring-details.component';
import { ScheduleInterviewComponent } from './schedule-interview/schedule-interview.component';
import { ApproveLeaveComponent } from './approve-leave/approve-leave.component';
import { EmployeeSalaryComponent } from './employee-salary/employee-salary.component';
import { HolidaysComponent } from './holidays/holidays.component';
import { MonthlyAttendanceReportComponent } from './employees/monthly-attendance-report/monthly-attendance-report.component';
import { SummaryComponent } from './summary/summary.component';
import { EmployeeAppraisalComponent } from './employee-appraisal/employee-appraisal.component';
import { AdvancesComponent } from './advances/advances.component';
import { InterviewFormComponent } from './interview-form/interview-form.component';
import { AdvanceDeductionComponent } from './advance-deduction/advance-deduction.component';
import {
  applicationPages,
  applicationModule
} from '../../shared/application-pages-enum';
import { RoleGuardService } from '../../service/role-guard.service';
// import {PensionPaymentsComponent} from './pension-payments/pension-payments.component'

const HR: any = {
  Employees: applicationPages.Employees,
  PayrollDailyHours: applicationPages.PayrollDailyHours,
  Holidays: applicationPages.Holidays,
  Attendance: applicationPages.Attendance,
  ApproveLeave: applicationPages.ApproveLeave,
  MonthlyPayrollRegister: applicationPages.MonthlyPayrollRegister,
  Jobs: applicationPages.Jobs,
  Interview: applicationPages.Interview,
  EmployeeAppraisal: applicationPages.EmployeeAppraisal,
  Advances: applicationPages.Advances,
  Summary: applicationPages.Summary
};

const HRModule: any = {
  ModuleId: applicationModule.HR
};

const hr_Router: Routes = [
  {
    path: '',
    component: HrComponent,
    children: [
      {
        path: 'employees',
        component: EmployeesComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.Employees
        }
      },
      {
        path: 'payroll-daily-hours',
        component: PayrollMonthlyHoursComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.PayrollDailyHours
        }
      },
      {
        path: 'attendance',
        component: AttendanceComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.Attendance
        }
      },
      {
        path: 'job-hiring-details',
        component: JobHiringDetailsComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.Jobs
        }
      },
      { path: 'schedule-interview', component: ScheduleInterviewComponent },
      {
        path: 'approve-leave',
        component: ApproveLeaveComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.ApproveLeave
        }
      },
      {
        path: 'employee-salary',
        component: EmployeeSalaryComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.MonthlyPayrollRegister
        }
      },
      {
        path: 'holidays',
        component: HolidaysComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.Holidays
        }
      },
      {
        path: 'monthly-attendance-report',
        component: MonthlyAttendanceReportComponent
      },
      {
        path: 'summary',
        component: SummaryComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.Summary
        }
      },
      {
        path: 'employee-appraisal',
        component: EmployeeAppraisalComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.EmployeeAppraisal
        }
      },
      {
        path: 'advances',
        component: AdvancesComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.Advances
        }
      },
      {
        path: 'interview-form',
        component: InterviewFormComponent,
        canActivate: [RoleGuardService],
        data: {
          module: HRModule.ModuleId,
          page: HR.Interview
        }
      },
      { path: 'advance-deduction', component: AdvanceDeductionComponent }
      // { path: 'pension-payments', component: PensionPaymentsComponent }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(hr_Router)],
  exports: [RouterModule]
})
export class HrRoutingModule {}
