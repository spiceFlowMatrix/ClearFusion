import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppUrlService } from '../services/app-url.service';
import { ApplicationPages, ApplicationModule, projectPagesMaster, configurationPagesMaster, HRPagesMaster, StorePagesMaster } from '../applicationpagesenum';
import { GlobalService } from '../services/global-services.service';
import { LocalStorageService } from '../services/localstorage.service';
import { GLOBAL } from '../global';
import { LoginService } from 'src/app/login/login.service';

@Component({
  selector: 'app-dbsidebar',
  templateUrl: './dbsidebar.component.html',
  styleUrls: ['./dbsidebar.component.scss']
})
export class DbsidebarComponent implements OnInit {
  selectedLink: number;

  // flag
  sidebarFlag = false;

  Marketing: any = {
    TimeCategory: ApplicationPages.TimeCategory,
    Quality: ApplicationPages.Quality,
    Phase: ApplicationPages.Phase,
    Nature: ApplicationPages.Nature,
    Medium: ApplicationPages.Medium,
    MediaCategory: ApplicationPages.MediaCategory,
    ActivityType: ApplicationPages.ActivityType,
    Clients: ApplicationPages.Clients,
    UnitRates: ApplicationPages.UnitRates,
    Jobs: ApplicationPages.Jobs,
    Contracts: ApplicationPages.Contracts,
    Producer: ApplicationPages.Producer,
    Channel: ApplicationPages.Channel
  };

  ConfigurationPages: any = {
    AppraisalQuestions: ApplicationPages.AppraisalQuestions,
    CurrencyCodes: ApplicationPages.CurrencyCodes,
    Department: ApplicationPages.Department,
    Designation: ApplicationPages.Designation,
    EmailSettings: ApplicationPages.EmailSettings,
    EmployeeContract: ApplicationPages.EmployeeContract,
    FinancialYear: ApplicationPages.FinancialYear,
    JobGrade: ApplicationPages.JobGrade,
    JournalCodes: ApplicationPages.JournalCodes,
    LeaveType: ApplicationPages.LeaveReason,
    OfficeCodes: ApplicationPages.OfficeCodes,
    PensionRate: ApplicationPages.PensionRate,
    TechnicalQuestions: ApplicationPages.TechnicalQuestions,
    Profession: ApplicationPages.Profession,
    Qualification: ApplicationPages.Qualification,
    SalaryHead: ApplicationPages.SalaryHead,
    SalaryTaxReportContent: ApplicationPages.SalaryTaxReportContent,
    PayrollAccount: ApplicationPages.SetPayrollAccount
    }

    HRPages: any = {
      Advances: ApplicationPages.Advances,
      ApproveLeave: ApplicationPages.ApproveLeave,
      Attendance: ApplicationPages.Attendance,
      EmployeeAppraisal: ApplicationPages.EmployeeAppraisal,
      Employees: ApplicationPages.Employees,
      Holidays: ApplicationPages.Holidays,
      Interview: ApplicationPages.Interview,
      Jobs: ApplicationPages.Jobs,
      MonthlyPayrollRegister: ApplicationPages.MonthlyPayrollRegister,
      PayrollDailyHours: ApplicationPages.PayrollDailyHours,
      Summary: ApplicationPages.Summary
      };

      StorePages: any = {
        Categories: ApplicationPages.Categories,
        DepreciationReport: ApplicationPages.DepreciationReport,
        PaymentTypes: ApplicationPages.PaymentTypes,
        ProcurementSummary: ApplicationPages.ProcurementSummary,
        Store: ApplicationPages.Store,
        StoreSourceCodes: ApplicationPages.StoreSourceCodes
      };

  AccountingNew: any = {
    Assets: ApplicationPages.Assets,
    Liabilities: ApplicationPages.Liabilities,
    Income: ApplicationPages.Income,
    Expense: ApplicationPages.Expense,
    BalanceSheet: ApplicationPages.BalanceSheet,
    IncomeExpenseReport: ApplicationPages.IncomeExpenseReport,
    Vouchers: ApplicationPages.Vouchers,
    ExchangeRates: ApplicationPages.ExchangeRates,
    VoucherSummaryReport: ApplicationPages.VoucherSummaryreport
  };

  ModulesAccounting: any = {
    ChartOfAccount: [
      ApplicationPages.Assets,
      ApplicationPages.Income,
      ApplicationPages.Expense,
      ApplicationPages.Liabilities
    ],
    FinancialReport: [
      ApplicationPages.BalanceSheet,
      ApplicationPages.IncomeExpenseReport
    ],
    Vouchers: [ApplicationPages.Vouchers],
    ExchangeRates: [ApplicationPages.ExchangeRates],
    GainLossReport: [ApplicationPages.ExchangeGainLoss],
    Journal: [ApplicationPages.Journal],
    Ledger: [ApplicationPages.LedgerStatement],
    TrialBalance: [ApplicationPages.TrialBalance],
    PensionPayments: [ApplicationPages.PensionPayments],
    VoucherSummaryReport: [ApplicationPages.VoucherSummaryreport]
  };

  ModulesMarketing: any = {
    Marketing: [
      ApplicationPages.Clients,
      ApplicationPages.Jobs,
      ApplicationPages.Contracts,
      ApplicationPages.UnitRates
    ],
    ModulesMarketingMaster: [
      ApplicationPages.TimeCategory,
      ApplicationPages.Quality,
      ApplicationPages.Phase,
      ApplicationPages.Nature,
      ApplicationPages.Medium,
      ApplicationPages.MediaCategory,
      ApplicationPages.ActivityType,
      ApplicationPages.Producer,
      ApplicationPages.Channel
    ]
  };

  ModulesProject: any = {
    Project: [
      projectPagesMaster.ProjectJobs,
      projectPagesMaster.ProjectActivities,
      projectPagesMaster.MyProjects,
      projectPagesMaster.Donors,
      projectPagesMaster.ProjectDetails,
      projectPagesMaster.Proposal,
      projectPagesMaster.CriteriaEvaluation,
      projectPagesMaster.ProjectDashboard,
      projectPagesMaster.ProjectCashFlow,
      projectPagesMaster.ProjectBudgetLine,
      projectPagesMaster.ProposalReport
    ]
  };

  UserModule: any = ApplicationPages.Users;

  ModulesConfigurationPages: any = {
    ConfigurationPages:[
      configurationPagesMaster.AppraisalQuestions,
      configurationPagesMaster.CurrencyCodes,
      configurationPagesMaster.Department,
      configurationPagesMaster.Designation,
      configurationPagesMaster.EmailSettings,
      configurationPagesMaster.EmployeeContract,
      configurationPagesMaster.FinancialYear,
      configurationPagesMaster.JobGrade,
      configurationPagesMaster.JournalCodes,
      configurationPagesMaster.LeaveType,
      configurationPagesMaster.OfficeCodes,
      configurationPagesMaster.PensionRate,
      configurationPagesMaster.Profession,
      configurationPagesMaster.Proposal,
      configurationPagesMaster.ProposalReport,
      configurationPagesMaster.Qualification,
      configurationPagesMaster.SalaryHead,
      configurationPagesMaster.SalaryTaxReportContent,
      configurationPagesMaster.SetPayrollAccount,
      configurationPagesMaster.TechnicalQuestions
    ]};

  HRModulePagesMaster: any = {
    HRPages: [
      HRPagesMaster.Advances,
      HRPagesMaster.ApproveLeave,
      HRPagesMaster.Attendance,
      HRPagesMaster.EmployeeAppraisal,
      HRPagesMaster.Employees,
      HRPagesMaster.Holidays,
      HRPagesMaster.Interview,
      HRPagesMaster.Jobs,
      HRPagesMaster.MonthlyPayrollRegister,
      HRPagesMaster.PayrollDailyHours,
      HRPagesMaster.Summary
      ]};

      StoreModulePagesMaster: any = {
        StorePages: [
          StorePagesMaster.Categories,
          StorePagesMaster.DepreciationReport,
          StorePagesMaster.PaymentTypes,
          StorePagesMaster.ProcurementSummary,
          StorePagesMaster.Store,
          StorePagesMaster.StoreSourceCodes
          ]};

  constructor(
    private router: Router,
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private localstorageservice: LocalStorageService,
    private loginService: LoginService
  ) {}

  ngOnInit() {
    const rolePermissions = JSON.parse(localStorage.getItem('RolePermissions'));
    const userId = localStorage.getItem('UserId');
    this.GetApplicationPages();
  }

  onsideBarLinkClicked(routePath: string, selectedSideBar: number) {
    // selectedLink
    this.selectedLink = selectedSideBar;

    this.router.navigate([routePath]);
  }

  //#region "onOldUIClick"
  onOldUIClick() {
    window.open(this.appurl.getOldUiUrl(), '_blank');
  }

  onOldUiPageClick(path: string, selectedSideBar: number) {
    window.open(this.appurl.getOldUiUrl() + path, '_blank');
    this.selectedLink = selectedSideBar;
  }
  //#endregion

  //#region "GetApplicationPages"
  GetApplicationPages() {
    this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Code_GetApplicationPages)
      .subscribe(data => {
        if (data.data.ApplicationPagesList != null) {
          localStorage.setItem(
            'ApplicationPages',
            JSON.stringify(data.data.ApplicationPagesList)
          );
        }
      });
  }
  //#endregion

  //#region "display"
  displayModulePages(pageId: number): boolean {
    let isAllowed = false;
    isAllowed = this.localstorageservice.displayModulePages(pageId);
    return isAllowed;
  }

  displayModule(moduleId: number): boolean {
    let isAllowed = false;
    isAllowed = this.localstorageservice.displayModule(moduleId);
    return isAllowed;
  }

  displayChildModule(module: number[]): boolean {
    let isAllowed = false;
    isAllowed = this.localstorageservice.displayChildModule(module);
    return isAllowed;
  }
  //#endregion

  //#region
  onLogout() {
    this.selectedLink = undefined;

    this.loginService.logout();
  }
  //#endregion
}
