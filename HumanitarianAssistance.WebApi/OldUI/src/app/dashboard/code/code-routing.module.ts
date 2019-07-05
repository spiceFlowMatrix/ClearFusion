import { Routes, RouterModule } from '@angular/router';
import { CodeComponent } from './code.component';
import { JournalCodeComponent } from './journal-code/journal-code.component';
import { NgModule } from '@angular/core';
import { ChartOfAccountsComponent } from './chart-of-accounts/chart-of-accounts.component';
import { AnalyticalCodesComponent } from './analytical-codes/analytical-codes.component';
import { CurrencyCodeComponent } from './currency-code/currency-code.component';
import { OfficeCodeComponent } from './office-code/office-code.component';
import { EmailSettingComponent } from './email-setting/email-setting.component';
import { ExchangeRateComponent } from './exchange-rate/exchange-rate.component';
import { LeavereasonTypeComponent } from './leavereason-type/leavereason-type.component';
import { FinancialYearComponent } from './financial-year/financial-year.component';
import { ProfessionDetailComponent } from './profession-detail/profession-detail.component';
import { DepartmentCodeComponent } from './department-code/department-code.component';
import { QualificationTypeComponent } from './qualification-type/qualification-type.component';
import { DesignationTypeComponent } from './designation-type/designation-type.component';
import { JobGradeComponent } from './job-grade/job-grade.component';
import { SalaryHeadComponent } from './salary-head/salary-head.component';
import { PensionRateComponent } from './pension-rate/pension-rate.component';
import { EmployeeBackgroundInfoComponent } from './employee-background-info/employee-background-info.component';
import { AppraisalQuestionsComponent } from './appraisal-questions/appraisal-questions.component';
import { TechnicalInterviewQuestionsComponent } from './technical-interview-questions/technical-interview-questions.component';
import { SalaryTaxReportContentComponent } from './salary-tax-report-content/salary-tax-report-content.component';
import { SetPayrollAccountComponent } from './set-payroll-account/set-payroll-account.component';
import {
  applicationPages,
  applicationModule
} from '../../shared/application-pages-enum';
import { RoleGuardService } from '../../service/role-guard.service';
import { PensionDebitAccountComponent } from './pension-debit-account/pension-debit-account.component';
import { AttendanceGroupMasterComponent } from './attendance-group-master/attendance-group-master.component';

const Code: any = {
  ChartOfAccount: applicationPages.ChartOfAccount,
  JournalCodes: applicationPages.JournalCodes,
  CurrencyCodes: applicationPages.CurrencyCodes,
  OfficeCodes: applicationPages.OfficeCodes,
  FinancialYear: applicationPages.FinancialYear,
  PensionRate: applicationPages.PensionRate,
  EmployeeContract: applicationPages.EmployeeContract,
  AppraisalQuestions: applicationPages.AppraisalQuestions,
  TechnicalQuestions: applicationPages.TechnicalQuestions,
  EmailSettings: applicationPages.EmailSettings,
  ExchangeRate: applicationPages.ExchangeRate,
  LeaveReason: applicationPages.LeaveReason,
  Profession: applicationPages.Profession,
  Department: applicationPages.Department,
  Qualification: applicationPages.Qualification,
  Designation: applicationPages.Designation,
  JobGrade: applicationPages.JobGrade,
  SalaryHead: applicationPages.SalaryHead,
  SalaryTaxReportContent: applicationPages.SalaryTaxReportContent,
  SetPayrollAccount: applicationPages.SetPayrollAccount,
  PensionDebitAccount: applicationPages.PensionDebitAccount,
  AttendanceGroupMaster: applicationPages.AttendanceGroupMaster
};

const CodeModule: any = {
  ModuleId: applicationModule.Code
};

const appRouter: Routes = [
  {
    path: '',
    component: CodeComponent,
    children: [
      {
        path: 'journal-code',
        component: JournalCodeComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.JournalCodes
        }
      },
      {
        path: 'chart-of-accounts',
        component: ChartOfAccountsComponent,

        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.ChartOfAccount
        }
      },
      { path: 'analytical-codes', component: AnalyticalCodesComponent },
      {
        path: 'currency-code',
        component: CurrencyCodeComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.CurrencyCodes
        }
      },
      {
        path: 'office-code',
        component: OfficeCodeComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.OfficeCodes
        }
      },
      {
        path: 'email-setting',
        component: EmailSettingComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.EmailSettings
        }
      },
      {
        path: 'exchange-rate',
        component: ExchangeRateComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.ExchangeRate
        }
      },
      {
        path: 'leavereason-type',
        component: LeavereasonTypeComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.LeaveReason
        }
      },
      {
        path: 'financial-year',
        component: FinancialYearComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.FinancialYear
        }
      },
      {
        path: 'profession-detail',
        component: ProfessionDetailComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.Profession
        }
      },
      {
        path: 'department-code',
        component: DepartmentCodeComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.Department
        }
      },
      {
        path: 'qualification-type',
        component: QualificationTypeComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.Qualification
        }
      },
      {
        path: 'designation-type',
        component: DesignationTypeComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.Designation
        }
      },
      {
        path: 'job-grade',
        component: JobGradeComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.JobGrade
        }
      },
      {
        path: 'salary-head',
        component: SalaryHeadComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.SalaryHead
        }
      },
      {
        path: 'pension-rate',
        component: PensionRateComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.PensionRate
        }
      },
      {
        path: 'employee-contract-content',
        component: EmployeeBackgroundInfoComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.EmployeeContract
        }
      },
      {
        path: 'appraisal-questions',
        component: AppraisalQuestionsComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.AppraisalQuestions
        }
      },
      {
        path: 'technical-questions',
        component: TechnicalInterviewQuestionsComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.TechnicalQuestions
        }
      },
      {
        path: 'salary-tax-report-content',
        component: SalaryTaxReportContentComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.SalaryTaxReportContent
        }
      },
      {
        path: 'set-payroll-account',
        component: SetPayrollAccountComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.SetPayrollAccount
        }
      },
      {
        path: 'pension-debit-account',
        component: PensionDebitAccountComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.PensionDebitAccount
        }
      },
      {
        path: 'attendance-group-master',
        component: AttendanceGroupMasterComponent,
        canActivate: [RoleGuardService],
        data: {
          module: CodeModule.ModuleId,
          page: Code.AttendanceGroupMaster
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(appRouter)],
  exports: [RouterModule]
})
export class CodeRoutingModule {}
