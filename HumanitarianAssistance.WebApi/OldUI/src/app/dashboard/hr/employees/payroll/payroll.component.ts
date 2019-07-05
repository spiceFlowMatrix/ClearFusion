import { Component, OnInit, Input } from '@angular/core';
import { CodeService } from '../../../code/code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountsService } from '../../../accounts/accounts.service';
import { CustomValidation } from '../../../../shared/customValidations';
import { parse } from 'url';
import { ProjectsService } from '../../../pmu/projects/projects.service';
import {
  applicationPages,
  applicationModule
} from '../../../../shared/application-pages-enum';
import { ThirdPartyKey } from '../../../../shared/thirdPartyKey';
import { GoogleObj, CommonService } from '../../../../service/common.service';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-payroll',
  templateUrl: './payroll.component.html',
  styleUrls: ['./payroll.component.css']
})
export class PayrollComponent implements OnInit {
  @Input() employeeId: number;

  //#region "Payroll Variables"
  currencyModel: any[];
  salaryTypeModel: any[];
  employeePayrollList: any[];
  count = 0;
  employeeAccountHeadList: any[];
  pensionRate = 0.045;
  transactionTypeDropdown: any;
  AccountsArr: any[];
  tabs: any[];
  payrollAccountForm: any;
  salaryHeadTabFlag = true;
  isSalaryHeadSaved = false;
  isPayrollHeadSaved = false;
  isEditingAllowed = false;

  payrollForm: any;

  // view contact
  selectedEmployeesReportData: any[];
  employeeNameDari = '';
  fatherNameDari = '';
  provinceNameDari = '';
  loading = false;
  contractPhoto: any;

  public googleObj: GoogleObj = new GoogleObj();
  public key = ThirdPartyKey.googleKey;

  // loader
  payrollInfoLoading = false;

  // popup
  contractEmployeePopup = false;

  //#endregion
  constructor(
    private codeservice: CodeService,
    private commonService: CommonService,
    private projectsService: ProjectsService,
    private accountservice: AccountsService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {
    this.transactionTypeDropdown = [
      { TransactionTypeId: 1, TransactionTypeName: 'Credit' },
      { TransactionTypeId: 2, TransactionTypeName: 'Debit' }
    ];
  }

  ngOnInit() {
    this.salaryTypeModel = [
      {
        Id: 1,
        Type: 'Fixed Type'
      },
      {
        Id: 2,
        Type: 'Hourly Type'
      }
    ];

    this.tabs = [
      {
        id: 1,
        text: 'Salary Heads'
      },
      {
        id: 2,
        text: 'Payroll Account Heads'
      }
    ];

    this.getCurrencyCodeList();
    this.GetEmployeePayrollDetails();
    this.GetAllEmployeesProject();
    this.GetLevelFourAccountDetails();
    this.initializeForm();
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.Employees
    );
  }

  //#region "initializeForm"
  initializeForm() {
    this.payrollForm = {
      CurrencyValue: null,
      PaymentType: 2,
      employeePayrollList: null
    };
  }
  //#endregion

  //#region "Get Currency Dropdown List"
  getCurrencyCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyModel = [];
          if (data.StatusCode === 200 && data.data.CurrencyList.length > 0) {
            data.data.CurrencyList.forEach(element => {
              this.currencyModel.push({
                CurrencyId: element.CurrencyId,
                CurrencyCode: element.CurrencyCode,
                CurrencyName: element.CurrencyName
              });
            });
            this.payrollForm.CurrencyValue = this.currencyModel[0].CurrencyId;
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          // tslint:disable-next-line:curly
          } else this.toastr.error('Something went wrong!');
        }
      );
  }
  //#endregion

  //#region "Get Employee Payroll Details"
  GetEmployeePayrollDetails() {
    this.showPayrollInfoLoader();
    this.accountservice
      .GetEmployeePayrollDetails(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetEmployeePayrollDetails,
        this.employeeId
      )
      .subscribe(
        data => {
          this.employeePayrollList = [];
          if (data.StatusCode === 200) {
            if (data.data.EmployeePayrollList != null) {
              let i = 1; // to set keyExpr="ID" value in HTML
              data.data.EmployeePayrollList.forEach(element => {
                this.employeePayrollList.push({
                  ID: i,
                  PayrollId: element.PayrollId,
                  SalaryHeadType: element.SalaryHeadType,
                  HeadTypeId: element.HeadTypeId,
                  SalaryHeadId: element.SalaryHeadId,
                  SalaryHead: element.SalaryHead,
                  MonthlyAmount: element.MonthlyAmount,
                  CurrencyId: element.CurrencyId,
                  PaymentType: element.PaymentType,
                  EmployeeID: this.employeeId,
                  PensionRate: element.PensionRate,
                  AccountNo: element.AccountNo,
                  TransactionTypeId: element.HeadTypeId === 2 ? 1 : 2
                });
                i++;
              });

              // TODO: If value is not set the add default
              this.payrollForm.CurrencyValue =
                data.data.EmployeePayrollList[0].CurrencyId === 0
                  ? this.payrollForm.CurrencyValue
                  : data.data.EmployeePayrollList[0].CurrencyId;

              this.payrollForm.PaymentType =
                data.data.EmployeePayrollList[0].PaymentType === 0 ||
                data.data.EmployeePayrollList[0].PaymentType == null
                  ? 2
                  : data.data.EmployeePayrollList[0].PaymentType;

              this.pensionRate =
                data.data.EmployeePayrollList[0].PensionRate !== 0 ||
                data.data.EmployeePayrollList[0].PensionRate != null
                  ? data.data.EmployeePayrollList[0].PensionRate
                  : 0.045;

              this.isSalaryHeadSaved = data.data.isSalaryHeadSaved;
            }
            if (data.data.EmployeePayrollAccountHeadList != null) {
              this.employeeAccountHeadList = [];
              let i = 1; // to set keyExpr="ID" value in HTML
              data.data.EmployeePayrollAccountHeadList.forEach(element => {
                this.employeeAccountHeadList.push({
                  ID: i,
                  SalaryHeadType: element.SalaryHeadType,
                  PayrollHeadTypeId: element.PayrollHeadTypeId,
                  PayrollHeadName: element.PayrollHeadName,
                  EmployeeId: this.employeeId,
                  PayrollHeadId: element.PayrollHeadId,
                  AccountNo: element.AccountNo,
                  TransactionTypeId: element.PayrollHeadTypeId === 1 ? 2 : 1
                });
                i++;
              });

              this.isPayrollHeadSaved = data.data.isPayrollHeadSaved;
            }
          // tslint:disable-next-line:curly
          } else this.toastr.error(data.Message);

          this.hidePayrollInfoLoader();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          // tslint:disable-next-line:curly
          } else this.toastr.error('Something went wrong!');

          this.hidePayrollInfoLoader();
        }
      );
  }
  //#endregion

  ////#region "Add Payroll"
  //AddPayrollDetails(data) {
  //  this.showPayrollInfoLoader();
  //  const obj = [];
  //  data.forEach(element => {
  //    obj.push({
  //      PayrollId: element.PayrollId,
  //      SalaryHeadType: element.SalaryHeadType,
  //      HeadTypeId: element.HeadTypeId,
  //      SalaryHeadId: element.SalaryHeadId,
  //      SalaryHead: element.SalaryHead,
  //      MonthlyAmount: element.MonthlyAmount,
  //      PaymentType: this.payrollForm.PaymentType,
  //      CurrencyId: this.payrollForm.CurrencyValue,
  //      EmployeeID: this.employeeId,
  //      PensionRate: this.pensionRate
  //    });
  //  });

  //  this.accountservice
  //    .AddVoucher(
  //      this.setting.getBaseUrl() + GLOBAL.API_HR_AddEmployeeSalaryDetail,
  //      obj
  //    )
  //    .subscribe(
  //      // tslint:disable-next-line:no-shadowed-variable
  //      data => {
  //        this.hidePayrollInfoLoader();
  //        if (data.StatusCode === 200) {
  //          this.toastr.success('Added Successfully!');
  //        // tslint:disable-next-line:curly
  //        } else if (data.StatusCode === 400)
  //          this.toastr.error('Something went wrong!');

  //        this.GetEmployeePayrollDetails();
  //      },
  //      error => {}
  //    );
  //}
  ////#endregion

  //#region "Edit Payroll"
  EditPayrollDetails(data) {
    this.showPayrollInfoLoader();
    const obj = [];
    data.forEach(element => {
      obj.push({
        PayrollId: element.PayrollId,
        SalaryHeadType: element.SalaryHeadType,
        HeadTypeId: element.HeadTypeId,
        SalaryHeadId: element.SalaryHeadId,
        SalaryHead: element.SalaryHead,
        MonthlyAmount: element.MonthlyAmount,
        PaymentType: this.payrollForm.PaymentType,
        CurrencyId: this.payrollForm.CurrencyValue,
        EmployeeID: this.employeeId,
        PensionRate: this.pensionRate,
        AccountNo: element.AccountNo,
        TransactionTypeId: element.TransactionTypeId
      });
    });
    this.accountservice
      .AddVoucher(
        this.setting.getBaseUrl() + GLOBAL.API_HR_EditEmployeeSalaryDetail,
        obj
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          this.hidePayrollInfoLoader();

          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully!!!');
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong! ' + data.Message);

          this.GetEmployeePayrollDetails();
        },
        error => {
          this.hidePayrollInfoLoader();
        }
      );
  }
  //#endregion

  //#region "onPayrollFormSubmit"
  onPayrollFormSubmit(data: any) {
    if (data.CurrencyValue != null && data.CurrencyValue !== 0) {
      this.payrollForm.CurrencyValue = data.CurrencyValue;
      // this.payrollForm.PaymentType = data.PaymentType;
      this.payrollForm.PaymentType = 2; // hourly bases

      // TODO: If payroll id not present then add else edit
      this.EditPayrollDetails(this.employeePayrollList);
    } else {
      this.toastr.warning('Please Select Currency !');
    }
  }
  //#endregion

  //#region "onPayrollFormSubmit"
  onPayrollAccountFormSubmit(data: any) {
    // TODO: If payroll id not present then add else edit
    this.EditPayrollAccountDetails(this.employeeAccountHeadList);
    // tslint:disable-next-line:max-line-length
    // this.employeePayrollList[0].PayrollId == 0 || this.employeePayrollList[0].PayrollId == null ? this.AddPayrollDetails(this.employeePayrollList) : this.EditPayrollDetails(this.employeePayrollList);
  }
  //#endregion

  //#region "Edit Payroll"
  EditPayrollAccountDetails(data) {
    this.showPayrollInfoLoader();
    const obj = [];
    data.forEach(element => {
      obj.push({
        SalaryHeadType: element.SalaryHeadType,
        PayrollHeadTypeId: element.PayrollHeadTypeId,
        PayrollHeadName: element.PayrollHeadName,
        EmployeeId: this.employeeId,
        AccountNo: element.AccountNo,
        TransactionTypeId: element.TransactionTypeId,
        PayrollHeadId: element.PayrollHeadId
      });
    });
    this.accountservice
      .AddVoucher(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeHR_EditAccountEmployeeSalaryDetail,
        obj
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          this.hidePayrollInfoLoader();

          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully!!!');
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.GetEmployeePayrollDetails();
        },
        error => {
          this.hidePayrollInfoLoader();
        }
      );
  }
  //#endregion

  //#region "Employee Assigned Projects"
  // tslint:disable-next-line:member-ordering
  employeeProjectList: any;
  GetAllEmployeesProject() {
    this.accountservice
      .GetEmployeePayrollDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_GetAllEmployeeProject,
        this.employeeId
      )
      .subscribe(
        data => {
          this.employeeProjectList = [];
          if (data.StatusCode === 200 && data.data.EmployeeProjectList != null) {
            if (data.data.EmployeeProjectList.length > 0) {
              let i = 1; // to set keyExpr="ID" value in HTML
              data.data.EmployeeProjectList.forEach(element => {
                this.employeeProjectList.push({
                  ID: i,
                  ProjectId: element.ProjectId,
                  ProjectName: element.ProjectName,
                  BudgetLineId: element.BudgetLineId,
                  BudgetLineName: element.BudgetLineName,
                  ProjectPercentage: element.ProjectPercentage,
                  EmployeeId: element.EmployeeId
                });
                i++;
              });
            }
          }
          this.hidePayrollInfoLoader();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          // tslint:disable-next-line:curly
          } else this.toastr.error('Something went wrong!');

          this.hidePayrollInfoLoader();
        }
      );
  }
  //#endregion

  //#region "Employee Project Percentage Assignment"

  onContentReadyProject(data) {
    if (this.count === 1) {
      this.count = 0;
      this.AddEmployeeProjectDetails(this.employeeProjectList);
    }
  }

  onRowUpdatingEventProject(obj) {
    this.count = 1;
    const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
    const itemIndex = this.employeeProjectList.findIndex(
      item => item.ID === value.ID
    );
    this.employeeProjectList[itemIndex] = value;
  }

  AddEmployeeProjectDetails(data) {
    const obj = [];
    data.forEach(element => {
      obj.push({
        ProjectId: element.ProjectId,
        ProjectName: element.ProjectName,
        BudgetLineId: element.BudgetLineId,
        BudgetLineName: element.BudgetLineName,
        ProjectPercentage: element.ProjectPercentage,
        EmployeeId: element.EmployeeId
      });
    });

    this.accountservice
      .AddVoucher(
        this.setting.getBaseUrl() +
          GLOBAL.API_Hr_AssignEmployeeProjectPercentage,
        obj
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully!');
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.GetAllEmployeesProject();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
            this.toastr.error('Something went wrong!');
          }
        }
      );
  }

  //#endregion

  logEventSalaryHead(eventName: string, obj: any) {
    this.isSalaryHeadSaved = false;
  }

  logEventPayrollHead(eventName: string, obj: any) {
    this.isPayrollHeadSaved = false;
  }

  GetLevelFourAccountDetails() {
    this.accountservice
      .GetAccountDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetLevelFourAccountDetails
      )
      .subscribe(
        data => {
          this.AccountsArr = [];
          if (data.StatusCode === 200) {
            data.data.AccountDetailList.forEach(element => {
              this.AccountsArr.push({
                AccountCode: element.AccountCode,
                AccountName: element.AccountName
              });
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
  }

  onSalaryHeadDataChanged(e: any) {
    if (e.dataField === 'CurrencyValue' && e.value !== 0) {
      this.isSalaryHeadSaved = false;
    }
  }

  //#region "On tab Select"
  selectTab(e) {
    e.itemData.id === 1
      ? (this.salaryHeadTabFlag = true)
      : (this.salaryHeadTabFlag = false);
  }
  //#endregion

  //#region "show / hide"
  showPayrollInfoLoader() {
    this.payrollInfoLoading = true;
  }

  hidePayrollInfoLoader() {
    this.payrollInfoLoading = false;
  }

  showPayrollInfopopup() {
    this.contractEmployeePopup = true;
  }

  hidePayrollInfopopup() {
    this.contractEmployeePopup = false;
  }
  //#endregion
}
