import { CodeComponent } from './code.component';
import { NgModule } from '@angular/core';
// import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from '@angular/common';
import { JournalCodeComponent } from './journal-code/journal-code.component';
import { CodeRoutingModule } from './code-routing.module';
import { ChartOfAccountsComponent } from './chart-of-accounts/chart-of-accounts.component';
import {
  DxFileUploaderModule,
  DxDataGridModule,
  DxSelectBoxModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxButtonModule,
  DxFormModule,
  DxPopupModule,
  DxTemplateModule,
  DxTabsModule,
  DxTreeListModule,
  DxLookupModule,
  DxTabPanelModule,
  DxTextAreaModule
} from 'devextreme-angular';
import { TranslateModule } from '@ngx-translate/core';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { CodeService } from './code.service';
import { AnalyticalCodesComponent } from './analytical-codes/analytical-codes.component';
import { CurrencyCodeComponent } from './currency-code/currency-code.component';
import { OfficeCodeComponent } from './office-code/office-code.component';
import { EmailSettingComponent } from './email-setting/email-setting.component';
import { ExchangeRateComponent } from './exchange-rate/exchange-rate.component';
import { LeavereasonTypeComponent } from './leavereason-type/leavereason-type.component';
import { FinancialYearComponent } from './financial-year/financial-year.component';
import { DepartmentCodeComponent } from './department-code/department-code.component';
import { ProfessionDetailComponent } from './profession-detail/profession-detail.component';
import { QualificationTypeComponent } from './qualification-type/qualification-type.component';
import { DesignationTypeComponent } from './designation-type/designation-type.component';
import { JobGradeComponent } from './job-grade/job-grade.component';
import { SalaryHeadComponent } from './salary-head/salary-head.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PensionRateComponent } from './pension-rate/pension-rate.component';
import { EmployeeBackgroundInfoComponent } from './employee-background-info/employee-background-info.component';
import { CKEditorModule } from 'ng2-ckeditor';
import { AppraisalQuestionsComponent } from './appraisal-questions/appraisal-questions.component';
import { TechnicalInterviewQuestionsComponent } from './technical-interview-questions/technical-interview-questions.component';
import { SalaryTaxReportContentComponent } from './salary-tax-report-content/salary-tax-report-content.component';
import { SetPayrollAccountComponent } from './set-payroll-account/set-payroll-account.component';
import { CommonService } from '../../service/common.service';
import { PensionDebitAccountComponent } from './pension-debit-account/pension-debit-account.component';
import { AttendanceGroupMasterComponent } from './attendance-group-master/attendance-group-master.component';
@NgModule({
  imports: [
    CommonModule,
    CodeRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    DxFileUploaderModule,
    DxDataGridModule,
    DxSelectBoxModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxButtonModule,
    DxFormModule,
    DxTabsModule,
    DxTabPanelModule,
    DxTreeListModule,
    DxPopupModule,
    DxLookupModule,
    DxTemplateModule,
    DxTextAreaModule,

    TranslateModule.forChild({}),
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.5)',
      backdropBorderRadius: '0px',
      fullScreenBackdrop: true,
      primaryColour: '#31c3aa',
      secondaryColour: '#000',
      tertiaryColour: '#a129'
    }),
    CKEditorModule
  ],
  declarations: [
    CodeComponent,
    JournalCodeComponent,
    ChartOfAccountsComponent,
    AnalyticalCodesComponent,
    CurrencyCodeComponent,
    OfficeCodeComponent,
    EmailSettingComponent,
    ExchangeRateComponent,
    LeavereasonTypeComponent,
    FinancialYearComponent,
    ProfessionDetailComponent,
    DepartmentCodeComponent,
    QualificationTypeComponent,
    DesignationTypeComponent,
    JobGradeComponent,
    SalaryHeadComponent,
    PensionRateComponent,
    EmployeeBackgroundInfoComponent,
    AppraisalQuestionsComponent,
    TechnicalInterviewQuestionsComponent,
    SalaryTaxReportContentComponent,
    SetPayrollAccountComponent,
    PensionDebitAccountComponent,
    AttendanceGroupMasterComponent
  ],
  providers: [CodeService, CommonService]
})
export class CodeModule {}
