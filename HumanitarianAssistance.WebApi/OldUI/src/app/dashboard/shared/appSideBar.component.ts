import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { SelectItem } from 'primeng/primeng';
import { ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { GLOBAL } from '../../shared/global';
import { DashboardComponent } from '../dashboard.component';
import { CodeService } from '../code/code.service';
import { NgxPermissionsService } from 'ngx-permissions';
import { applicationPages } from '../../shared/application-pages-enum';
import { AuthenticationService } from '../../service/authentication.service';
import { AppSettingsService } from '../../service/app-settings.service';
import { CommonService } from '../../service/common.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './appSidebar.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppSidebarComponent implements OnInit {
  Patients: Array<SelectItem>;
  isMaster = false;
  isAdmin = false;

  status: string;
  User: any = { Users: applicationPages.Users };

  Code: any = {
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

  Accounting: any = {
    Vouchers: applicationPages.Vouchers,
    Journal: applicationPages.Journal,
    LedgerStatement: applicationPages.LedgerStatement,
    BudgetBalance: applicationPages.BudgetBalance,
    TrialBalance: applicationPages.TrialBalance,
    FinancialReport: applicationPages.FinancialReport,
    CategoryPopulator: applicationPages.CategoryPopulator,
    ExchangeGainLoss: applicationPages.ExchangeGainLoss,
    GainLossTransaction: applicationPages.GainLossTransaction,
    PensionPayments: applicationPages.PensionPayments
  };

  HR: any = {
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

  Store: any = {
    Categories: applicationPages.Categories,
    StoreSourceCodes: applicationPages.StoreSourceCodes,
    PaymentTypes: applicationPages.PaymentTypes,
    Store: applicationPages.Store,
    ProcurementSummary: applicationPages.ProcurementSummary,
    DepreciationReport: applicationPages.DepreciationReport
  };

  Module: any = {
    User: 1,
    Code: 2,
    Accounting: 3,
    HR: 4,
    Store: 5
  };

  @Input() toggle: boolean;
  constructor(
    private permissionsService: NgxPermissionsService,
    private router: Router,
    private changeDetector: ChangeDetectorRef,
    private authService: AuthenticationService,
    private appSettigs: AppSettingsService,
    public _dashboard: DashboardComponent,
    private commonservice: CommonService,
    private codeService: CodeService
  ) {}
  PermissionsArr: any;
  permissionwithRole: any;
  RolesAndPermissionsList: any;

  getRolesAndPermissionsByUserId(UserId) {
    this.authService
      .getRolesAndPermissionsByUserId(
        this.appSettigs.getBaseUrl() +
          GLOBAL.API_UserRoles_GetUserRolesByUserId,
        UserId
      )
      .subscribe(data => {
        this.PermissionsArr = [];
        this.permissionwithRole = {};
        if (
          data.StatusCode === 200 &&
          data.data.RoleList != null &&
          data.data.RoleList.length > 0
        ) {
          for (let i = 0; i < data.data.RoleList.length; i++) {
            const rolename = data.data.RoleList[i].RoleName;
            this.RolesAndPermissionsList = [];
            this.RolesAndPermissionsList =
              data.data.RoleList[i].PermissionsList;
            for (
              let j = 0;
              j < data.data.RoleList[i].PermissionsList.length;
              j++
            ) {
              this.PermissionsArr.push(
                data.data.RoleList[i].PermissionsList[j].Name
              );
            }
            this.permissionwithRole['' + rolename + ''] = this.PermissionsArr;
            this.PermissionsArr = [];
          }
          const permissionStr = this.PermissionsArr.join(',');

          // this.ngxroleservice.addRoles(this.permissionwithRole);
          // localStorage.setItem('UserPermissions', this.permissionwithRole);
        }
      });
  }
  ngOnInit() {
    // Role
    const perm =
      localStorage.getItem('UserRoles') !== '' ||
      localStorage.getItem('UserRoles') != null
        ? localStorage.getItem('UserRoles').split(',')
        : null;

    this.permissionsService.loadPermissions(perm);
    const rolePermissions = JSON.parse(localStorage.getItem('RolePermissions'));

    const userId = localStorage.getItem('UserId');
    this.GetApplicationPages();
  }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnDestroy() {
    this.permissionsService.flushPermissions();
  }

  toggleSideFun() {
    this._dashboard.toggleSide = !this._dashboard.toggleSide;
  }

  logout() {
    // localStorage.removeItem('authenticationtoken');
    // localStorage.removeItem('ng2Idle.main.expiry');
    // localStorage.removeItem('ng2Idle.main.idling');
    // localStorage.removeItem('UserId');
    // localStorage.removeItem('UserId');
    // localStorage.removeItem('UserName');
    // localStorage.removeItem('UserRoles');
    // localStorage.removeItem('SelectedVoucherNumber');
    // localStorage.removeItem('SelectedVoucherCurrency');
    // localStorage.removeItem('SelectedOfficeId');
    // localStorage.removeItem('ApplicationPages');
    // localStorage.removeItem('RolePermissions');

    localStorage.clear();
    this.router.navigate(['../login']);

    // this.commonService.menuVisibility = false;
    // this.checkToken.emit();
    // this.idle.stop();
  }

  LoaderEvent(e) {
    if (this.status !== e) {
      this.status = e;
      this.commonservice.setLoader(true);
    }
  }

  navigateToPath(path: string) {
    this.router.navigateByUrl('/dashboard/' + path);
  }

  //#region "GetApplicationPages"
  GetApplicationPages() {
    this.codeService.GetAllCodeList(
      this.appSettigs.getBaseUrl() + GLOBAL.API_Code_GetApplicationPages
    ).subscribe(data => {
      if (data.data.ApplicationPagesList != null) {
        localStorage.setItem(
          'ApplicationPages',
          JSON.stringify(data.data.ApplicationPagesList)
        );
      }
    });
  }
  //#endregion

  //#region "Check if permission to view/edit page exists for a role"
  displayModulePages(pageId: number): boolean {
    let isSuperAdmin = false;
    let roles: any;
    const permissionList = JSON.parse(localStorage.getItem('RolePermissions'));
    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');
    }
    if (roles != null && roles !== undefined) {
      if (roles.length > 0 && roles != null) {
        for (let i = 0; i < roles.length; i++) {
          if (roles[i].toLowerCase() === 'superadmin') {
            isSuperAdmin = true;
          }
        }
      }
    }

    if (!isSuperAdmin) {
      const modulePageExists = permissionList.filter(x => x.PageId === pageId);

      if (modulePageExists != null && modulePageExists.length > 0) {
        return true;
      } else {
        return false;
      }
    }

    return isSuperAdmin;
  }
  //#endregion

  //#region "Check if permission to view module exists for a role"
  displayModule(moduleId: number): boolean {
    let isSuperAdmin = false;
    let roles: any;
    const permissionList = JSON.parse(localStorage.getItem('RolePermissions'));
    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');
    }

    if (roles.length > 0 && roles != null) {
      for (let i = 0; i < roles.length; i++) {
        if (roles[i].toLowerCase() === 'superadmin') {
          isSuperAdmin = true;
        }
      }
    }

    if (!isSuperAdmin) {
      const modulePageExists = permissionList.filter(x => x.ModuleId === moduleId);

      if (modulePageExists != null && modulePageExists.length > 0) {
        return true;
      } else {
        return false;
      }
    }

    return isSuperAdmin;
  }
  //#endregion

  //#region "Display Separators"
  displaySeparator(pageIds: number[]): boolean {
    let isSuperAdmin = false;
    let roles: any;
    const permissionList = JSON.parse(localStorage.getItem('RolePermissions'));
    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');
    }

    if (roles.length > 0 && roles != null) {
      for (let i = 0; i < roles.length; i++) {
        if (roles[i].toLowerCase() === 'superadmin') {
          isSuperAdmin = true;
        }
      }
    }

    if (!isSuperAdmin) {
      const modulePageExists = permissionList.filter(x =>
        pageIds.includes(x.PageId)
      );

      if (modulePageExists != null && modulePageExists.length > 0) {
        return true;
      } else {
        return false;
      }
    }

    return isSuperAdmin;
  }
  //#endregion
}
