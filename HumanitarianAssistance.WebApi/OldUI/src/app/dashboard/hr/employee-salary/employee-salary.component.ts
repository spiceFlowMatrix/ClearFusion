import { Component, OnInit } from '@angular/core';
import { HrService, Tab } from '../hr.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer } from '@angular/platform-browser';
import { GLOBAL } from '../../../shared/global';
import { EmployeeSalaryService } from './employeee-salary.service';
import { CodeService } from '../../code/code.service';
import { AccountsService } from '../../accounts/accounts.service';
import { EmployeePaymentDetail } from './employee-salary.model';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

declare let jsPDF;
declare var $: any;

@Component({
  selector: 'app-employee-salary',
  templateUrl: './employee-salary.component.html',
  styleUrls: ['./employee-salary.component.css']
})
export class EmployeeSalaryComponent implements OnInit {
  employeeSalaryDetail: EmployeePaymentDetail; // use for Input binding
  employeeePayrollFilterData: EmployeePayrollFilterModel;
  currencyTypeDropdown: CurrencyTypeDropdown[];
  levelFourAccounts: any;
  isEditingAllowed = false;

 advanceRecoveryAmountDifference = 0;

  employeePayrollByHourlyBased: EmployeePayRollModel[];
  employeePayrollByFixedBased: EmployeePayRollModel[];

  currentDate = new Date(
    new Date().getFullYear(),
    new Date().getMonth() - 1,
    new Date().getDate()
  );

  payrollRegisterLoading = false;
  empSalaryLoading = false;

  // Employee More Details
  employeeMoreDetailsPopup = false;
  employeeId: number;
  openInfoTab: number;
  saveConfirmationPopup = false;

  fixedBasedMoreDetailsPopup = false;
  hourlyBasedMoreDetailsPopup = false;
  approveConfirmationPopup = false;
  salaryApprovalConfirmLoading = false;
  disapproveConfirmationPopup = false;

  employeeSalaryTabFlag = true;

  selectedCurrencyId: any;
  selectedDate: any;
  officeDropdownList: any[]= [];
  selectedOffice= null;

  // main form
  empSalaryForm: EmployeepayrollListModel;

  count = 0;
  hourlyBasedSetPayrollList: EmployeePayRollModel;
  fixedBasedSetPayrollList: EmployeePayRollModel;

  employeeSalarytabs = [
    {
      id: 1,
      text: 'Update Salary Info'
    },
    {
      id: 2,
      text: 'Approved Salary'
    }
  ];

  salaryTypeModel = [
    {
      Id: 1,
      Type: 'Fixed Type'
    },
    {
      Id: 2,
      Type: 'Hourly Type'
    }
  ];
  selectedPaymentType = 2;

  // Approved Salary
  approvedSalaryDataSource: any[];
  approvedHourlySalaryDataSource: any[];
  employeeApprovedSalaryFormFilter: any;
  approvedSalaryMoreDetailsPopup = false;
  transactionTypeDropdown: any[];

  // Salary Slip
  employeeSalarySlipList: EmployeeSalarySlipModel[];
  selectSalarySlipDateForm: SelectSalarySlipDateModel;
  hourlyBasedSalarySlipFlag = true;

  salaryPaymentPopup = false;

  //#region "TABS"
  showInfoTabsMain: Tab[] = [
    {
      id: 1,
      text: 'Professional Info'
    },
    {
      id: 2,
      text: 'History'
    },
    {
      id: 3,
      text: 'Leave Info'
    },
    {
      id: 4,
      text: 'Health Info'
    },
    {
      id: 5,
      text: 'Attendance'
    },
    {
      id: 6,
      text: 'Payroll'
    },
    {
      id: 7,
      text: 'Monthly Report'
    }
  ];
  //#endregion

  // loader
  constructor(
    private accountservice: AccountsService,
    private hrService: HrService,
    private codeService: CodeService,
    private employeeSalaryService: EmployeeSalaryService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private _DomSanitizer: DomSanitizer,
    private commonService: CommonService
  ) {}

  ngOnInit() {
    console.log(this.currentDate);
    this.getAllCurrencyType();
    this.getLevelFourAccountDetails();
    this.getOfficeCodeList();
    this.initializeForm();
    this.commonService.getEmployeeOfficeId().subscribe(data => {});
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.MonthlyPayrollRegister
    );
    this.transactionTypeDropdown = [
      { TransactionTypeId: 1, TransactionTypeName: 'Credit' },
      { TransactionTypeId: 2, TransactionTypeName: 'Debit' }
    ];
  }

  //#region "initializeForm"
  initializeForm() {
    this.employeeePayrollFilterData = {
      Date: this.currentDate,
      Month: 0,
      Year: 0,
      SelectedPaymentType: 1
    };

    this.selectSalarySlipDateForm = {
      Date: new Date(),
      Year: null,
      OfficeId: null,
      EmployeeId: null
    };

    this.empSalaryForm = {
      CurrencyId: 0,
      EmployeeId: 0,
      HeadTypeId: 0,
      MonthlyAmount: 0,
      PaymentType: null,
      PayrollId: null,
      PensionRate: null,
      SalaryHead: null,
      SalaryHeadId: null,
      SalaryHeadType: null,
      AccountNo: null,
      TransactionTypeId: null
    };

    this.employeeApprovedSalaryFormFilter = {
      Date: this.currentDate,
      Month: 0,
      Year: 0,
      SelectedPaymentType: 1
    };
  }
  //#endregion

  //#region "All Currency Type"
  getAllCurrencyType() {
    this.hrService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyTypeDropdown = [];
          if (
            data.StatusCode === 200 &&
            data.data.CurrencyList != null &&
            data.data.CurrencyList.length > 0
          ) {
            data.data.CurrencyList.forEach(element => {
              this.currencyTypeDropdown.push(element);
            });
            this.onEmployeePayrollFilter(this.employeeePayrollFilterData);
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
          }
        }
      );
  }
  //#endregion

  //#region "On Approve Payroll Register"
  // tslint:disable-next-line:member-ordering
  selectedRowsList: any[];
  selectionChangedHandler(e) {
    this.selectedRowsList = [];
    if (e.selectedRowsData.length > 0 && e.selectedRowsData != null) {
      e.selectedRowsData.forEach(element => {
        this.selectedRowsList.push({
          OfficeId: this.selectedOffice,
          CurrencyId: element.CurrencyId,
          FinancialYearDate: this.selectedDate,
          EmployeeId: element.EmployeeId,
          EmployeeName: element.EmployeeName,
          PaymentType: element.PaymentType,
          WorkingDays: element.WorkingDays,
          PresentDays: element.PresentDays,
          AbsentDays: element.AbsentDays,
          LeaveDays: element.LeaveDays,
          TotalWorkHours: element.TotalWorkHours,
          HourlyRate: element.HourlyRate,
          TotalGeneralAmount: element.TotalGeneralAmount,
          TotalAllowance: element.TotalAllowance,
          TotalDeduction: element.TotalDeduction,
          GrossSalary: element.GrossSalary,
          OverTimeHours: element.OverTimeHours,
          PensionAmount: element.PensionAmount,
          SalaryTax: element.SalaryTax,
          NetSalary: element.NetSalary,
          IsApproved: true,
          IsAdvanceApproved: element.IsAdvanceApproved,
          AdvanceAmount:
            element.IsAdvanceApproved === true ? element.AdvanceAmount : 0,
          IsAdvanceRecovery: element.IsAdvanceRecovery, // Opposite of flag (here false means TRUE and true means means FALSE)
          AdvanceRecoveryAmount: element.AdvanceRecoveryAmount,
          employeepayrolllist: element.employeepayrolllist,
          Year: this.employeeePayrollFilterData.Date.getFullYear(),
          Month: this.employeeePayrollFilterData.Date.getMonth() + 1,
          pensionRate: element.employeepayrolllist[0].PensionRate
        });
      });
    }
  }
  //#endregion

  //#region "On Approve Payroll Register"
  // tslint:disable-next-line:member-ordering
  employeeList: DisapproveSalaryModel;
  disapproveSalarySelectionChanged(e) {
    this.employeeList = {
      EmployeeList: [],
      Month: 0,
      OfficeId: 0,
      Year: 0
    };

    if (e.selectedRowsData.length > 0 && e.selectedRowsData != null) {
      e.selectedRowsData.forEach(element => {
        this.employeeList.EmployeeList.push({
          EmployeeId: element.EmployeeId
        });
        this.employeeList.OfficeId = this.selectedOffice;
        this.employeeList.Year = this.employeeApprovedSalaryFormFilter.Date.getFullYear();
        this.employeeList.Month =
          this.employeeApprovedSalaryFormFilter.Date.getMonth() + 1;
      });
    }
  }
  //#endregion

  //#region "onRowPreparedUpdateSalaryEvent"
  onRowPreparedUpdateSalaryEvent(e) {
    console.log(e);
    if (e.row !== undefined) {
      // const value = e.component.cellValue(e.row.rowIndex, 'AdvanceAmount');
      // // tslint:disable-next-line:curly
      // if (value === 0 || value == null) e.editorOptions.disabled = true;
    }
  }
  //#endregion

  onRowUpdating(e) {
    if (e.key.IsAdvanceRecovery) {
      e.key.AdvanceAmount += e.oldData.AdvanceRecoveryAmount - e.newData.AdvanceRecoveryAmount;
      e.key.NetSalary += e.oldData.AdvanceRecoveryAmount - e.newData.AdvanceRecoveryAmount;
    }

  }

  //#region "onApproveConfirmationPopup"
  onApproveConfirmationPopup() {
    this.showApproveConfirmationPopup();
  }
  onSaveConfirmationPopup() {
    this.showSaveConfirmationPopup();
  }
  //#endregion

  //#region "onApproveList"
  onApproveList() {
    if (this.selectedRowsList != null && this.selectedRowsList.length > 0) {
      if (this.selectedRowsList[0].PaymentType === 2) {
      }

      this.ApproveEmployeeMonthlyPayrollList(this.selectedRowsList);
    } else {
      this.toastr.error('Please select some records!');
      this.hideApproveConfirmationPopup();
    }
  }
  //#endregion

  //#region "onDisapproveList"
  onDisapproveList() {
    if (
      this.employeeList != null &&
      this.employeeList.EmployeeList.length > 0
    ) {
      this.DisapproveEmployeeMonthlyPayrollList(this.employeeList);
    } else {
      this.toastr.error('Please select some records!');
      this.hideApproveConfirmationPopup();
    }
  }
  //#endregion

  //#region "onSaveEmployeeSalaryDataList"
  onSaveEmployeeSalaryDataList() {
    if (this.employeePayrollByHourlyBased != null) {
      if (this.employeePayrollByHourlyBased.length > 0) {
        this.showEmployeeSalaryLoader();

        let tempList: any[];

        tempList = this.employeePayrollByHourlyBased;

        for (const item of tempList) {
          item.OfficeId = this.selectedOffice;
          item.CurrencyId = item.CurrencyId;
          item.FinancialYearDate = this.selectedDate;
          item.pensionRate = item.employeepayrolllist[0].PensionRate;
          item.Year = this.employeeePayrollFilterData.Date.getFullYear();
          item.Month = this.employeeePayrollFilterData.Date.getMonth() + 1;
        }

        this.employeeSalaryService
          .ApproveEmployeeMonthlyPayrollList(
            this.setting.getBaseUrl() +
              GLOBAL.API_EmployeeHr_EmployeePaymentTypeReportForSaveOnly,
            tempList
          )
          .subscribe(
            data => {
              if (data.StatusCode === 200) {
                this.toastr.success('Salary Info Updated Successfull !');
              } else {
                this.toastr.warning(data.Message);
              }

              // Refresh list
              const modelData = {
                CurrencyId: this.selectedCurrencyId,
                Date: this.selectedDate,
                Month: new Date(this.selectedDate).getMonth(),
                Year: new Date(this.selectedDate).getFullYear(),
                SelectedPaymentType: this.selectedPaymentType
              };
              this.onEmployeePayrollFilter(modelData);

              this.hideEmployeeSalaryLoader();
              this.hideSaveConfirmationPopup();
            },
            error => {}
          );
      } else {
        this.toastr.warning('Please select any Employee');
        this.hideSaveConfirmationPopup();
      }
    }
  }
  //#endregion

  //#region "on Delete List"
  //onDeleteList(e: any) {
  //  if (e.Date != null && e.CurrencyId != null) {
  //    const model = {
  //      // tslint:disable-next-line:radix
  //      OfficeId: this.selectedOffice,
  //      FinancialYearDate: new Date(
  //        new Date(e.Date).getFullYear(),
  //        new Date(e.Date).getMonth(),
  //        new Date(e.Date).getDate(),
  //        new Date().getHours(),
  //        new Date().getMinutes()
  //      ),
  //      PaymentType: 2 // hourly
  //      // PaymentType: this.empSalaryForm.PaymentType
  //    };
  //    this.employeeSalaryService
  //      .RemoveApprovedList(
  //        this.setting.getBaseUrl() + GLOBAL.API_HR_RemoveApprovedList,
  //        model
  //      )
  //      .subscribe(
  //        data => {
  //          if (data.StatusCode === 200) {
  //            this.toastr.success('Successfully Removed!');
  //          // tslint:disable-next-line:curly
  //          } else if (data.StatusCode === 400)
  //            this.toastr.error('Something went wrong !');

  //          // Refresh list
  //          const modelData = {
  //            CurrencyId: this.selectedCurrencyId,
  //            Date: this.selectedDate,
  //            Month: new Date(this.selectedDate).getMonth(),
  //            Year: new Date(this.selectedDate).getFullYear(),
  //            SelectedPaymentType: this.selectedPaymentType
  //          };
  //          this.onEmployeePayrollFilter(modelData);
  //        },
  //        error => {
  //          if (error.StatusCode === 500) {
  //            this.toastr.error('Internal Server Error....');
  //          } else if (error.StatusCode === 401) {
  //            this.toastr.error('Unauthorized Access Error....');
  //          } else if (error.StatusCode === 403) {
  //            this.toastr.error('Forbidden Error....');
  //          }
  //        }
  //      );
  //  }
  //}
  //#endregion

  //#region "ApproveEmployeeMonthlyPayrollList"
  ApproveEmployeeMonthlyPayrollList(selectedRowsList: any) {
    this.showSalaryApprovalConfirmLoading();

    this.employeeSalaryService
      .ApproveEmployeeMonthlyPayrollList(
        this.setting.getBaseUrl() +
          GLOBAL.API_HR_EmployeesPayrollRegisterApproval,
        selectedRowsList
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Approved Successfull !');
          }

          // Refresh list
          const modelData = {
            CurrencyId: this.selectedCurrencyId,
            Date: this.selectedDate,
            Month: new Date(this.selectedDate).getMonth(),
            Year: new Date(this.selectedDate).getFullYear(),
            SelectedPaymentType: this.selectedPaymentType
          };
          this.onEmployeePayrollFilter(modelData);

          this.hideSalaryApprovalConfirmLoading();

          this.hideApproveConfirmationPopup();
        },
        error => {}
      );
  }
  //#endregion

  //#region "On Filter"
  onEmployeePayrollFilter(model: EmployeePayrollFilterModel) {
    this.showEmployeeSalaryLoader();

    model.Month = model.Date.getMonth() + 1;
    model.Year = model.Date.getFullYear();
    model.SelectedPaymentType = this.selectedPaymentType;
    this.selectedDate = model.Date;
    // tslint:disable-next-line:radix
    const officeId = this.selectedOffice;
    this.employeeSalaryService
      .GetAllEmployeeMonthlyPayrollList(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeHR_GetAllEmployeeMonthlyPayrollList,
        officeId,
        model.Month,
        model.Year,
        model.SelectedPaymentType
      )
      .subscribe(
        data => {
          this.employeePayrollByFixedBased = [];
          this.employeePayrollByHourlyBased = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeMonthlyPayrolllist != null &&
            data.data.EmployeeMonthlyPayrolllist.length > 0
          ) {
            data.data.EmployeeMonthlyPayrolllist.forEach(element => {
              element.PaymentType === 1
                ? this.employeePayrollByFixedBased.push(element)
                : this.employeePayrollByHourlyBased.push(element);
            });
            if (this.selectedPaymentType === 1) {
              this.employeePayrollByHourlyBased = [];
            } else {
              this.employeePayrollByFixedBased = [];
            }
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400) this.toastr.error(data.Message);

          this.hideEmployeeSalaryLoader();
        },
        error => {}
      );
  }
  //#endregion

  //#region "On Filter Approved Salary"
  onEmployeeApprovedSalaryFilter(model: EmployeePayrollFilterModel) {
    this.showEmployeeSalaryLoader();

    model.Month = model.Date.getMonth() + 1;
    model.Year = model.Date.getFullYear();
    model.SelectedPaymentType = this.selectedPaymentType;
    this.selectedDate = model.Date;
    // tslint:disable-next-line:radix
    const officeId = this.selectedOffice;
    this.employeeSalaryService
      .GetAllEmployeeMonthlyPayrollApprovedList(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeHr_GetAllEmployeeMonthlyPayrollListApproved,
        officeId,
        model.Month,
        model.Year,
        model.SelectedPaymentType
      )
      .subscribe(
        data => {
          this.approvedSalaryDataSource = [];

          if (
            data.StatusCode === 200 &&
            data.data.EmployeeMonthlyPayrollApprovedList != null &&
            data.data.EmployeeMonthlyPayrollApprovedList.length > 0
          ) {
            data.data.EmployeeMonthlyPayrollApprovedList.forEach(element => {
              element.PaymentType === 2
                ? this.approvedSalaryDataSource.push(element)
                // tslint:disable-next-line:no-unused-expression
                : null;
            });
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400) this.toastr.error(data.Message);

          this.hideEmployeeSalaryLoader();
        },
        error => {}
      );
  }
  //#endregion

  //#region "Edit Payroll"
  EditPayrollDetails(baseId: number, data: EmployeePayRollModel) {
    let totalAllowance: any = 0;
    let deductionAllowance: any = 0;
    let totalBasicSalary: any = 0;

    const totalHours = data.PresentDays + data.OverTimeHours + data.LeaveHours;

    if (data != null) {
      for (let i = 0; i < data.employeepayrolllist.length; i++) {
        data.employeepayrolllist[
          i
        ].PensionRate = this.empSalaryForm.PensionRate;
        data.employeepayrolllist[
          i
        ].PaymentType = this.empSalaryForm.PaymentType;
        // tslint:disable-next-line:curly
        if (data.employeepayrolllist[i].HeadTypeId === 1)
          // Allowance
          totalAllowance =
            totalAllowance + data.employeepayrolllist[i].MonthlyAmount;
        // tslint:disable-next-line:curly
        if (data.employeepayrolllist[i].HeadTypeId === 2)
          // Deduction
          deductionAllowance =
            deductionAllowance + data.employeepayrolllist[i].MonthlyAmount;
        // tslint:disable-next-line:curly
        if (data.employeepayrolllist[i].HeadTypeId === 3)
          // General (Basic Salary)
          totalBasicSalary =
            totalBasicSalary + data.employeepayrolllist[i].MonthlyAmount;
      }

      if (baseId === 2) {
        const itemIndex = this.employeePayrollByHourlyBased.findIndex(
          item => item.EmployeeId === data.EmployeeId
        );
        this.employeePayrollByHourlyBased[itemIndex].employeepayrolllist =
          data.employeepayrolllist;
        this.employeePayrollByHourlyBased[
          itemIndex
        ].TotalAllowance = totalAllowance;
        this.employeePayrollByHourlyBased[itemIndex].GrossSalary =
          totalBasicSalary * totalHours + totalAllowance;

        this.employeePayrollByHourlyBased[itemIndex].PensionAmount =
          this.employeePayrollByHourlyBased[itemIndex].GrossSalary *
          data.employeepayrolllist[0].PensionRate;
        this.employeePayrollByHourlyBased[
          itemIndex
        ].TotalDeduction = deductionAllowance;
        this.employeePayrollByHourlyBased[itemIndex].NetSalary =
          this.employeePayrollByHourlyBased[itemIndex].GrossSalary -
          this.employeePayrollByHourlyBased[itemIndex].TotalDeduction -
          this.employeePayrollByHourlyBased[itemIndex].PensionAmount -
          this.employeePayrollByHourlyBased[itemIndex].SalaryTax;
        this.hourlyBasedSetPayrollList = this.employeePayrollByHourlyBased[
          itemIndex
        ];
      }
    }
  }

  // tslint:disable-next-line:member-ordering
  salaryTax: any;
  // SalaryCalculate(grossSalary, exchangeRate) {
  //   if (grossSalary < 5000) {
  //     this.salaryTax = 0;
  //   } else if (grossSalary >= 5000 && grossSalary < 12500) {
  //     this.salaryTax = ((grossSalary * exchangeRate - 5000) * 2) / 100;
  //   } else if (grossSalary >= 12500 && grossSalary < 100000) {
  //     this.salaryTax =
  //       (((grossSalary * exchangeRate - 12500) * 10) / 100 + 150) /
  //       exchangeRate;
  //   } else {
  //     this.salaryTax =
  //       (((grossSalary * exchangeRate - 100000) * 20) / 100 + 8900) /
  //       exchangeRate;
  //   }
  //   return this.salaryTax;
  // }
  // //#endregion

  //#region "onEmployeeNameClicked"
  onEmployeeNameClicked(data: any) {
    this.employeeId = data.data.EmployeeId;
    this.showEmployeeMoreDetailsPopup();
    this.openInfoTab = 1; // set Tab 1
  }
  //#endregion

  //#region "onRowClickFixedBased"
  onRowClickFixedBased(e: any) {
    // NOTE: Not working on Fixed rate

    this.fixedBasedMoreDetailsPopup = true;
    // for popup data bind
    this.fixedBasedSetPayrollList = e;

    this.empSalaryForm.EmployeeId = e.EmployeeId;
    this.empSalaryForm.PaymentType = e.PaymentType;

    this.empSalaryForm.PensionRate = this.fixedBasedSetPayrollList.employeepayrolllist[0].PensionRate;
    this.empSalaryForm.CurrencyId = this.fixedBasedSetPayrollList.employeepayrolllist[0].CurrencyId;
    this.empSalaryForm.PaymentType = this.fixedBasedSetPayrollList.employeepayrolllist[0].PaymentType;
  }
  //#endregion

  //#region "onRowClickHourlyBased"
  onRowClickHourlyBased(e: any) {
    this.showHourlyBasedMoreDetailsPopup();

    this.empSalaryForm = {
      CurrencyId: 0,
      EmployeeId: 0,
      HeadTypeId: 0,
      MonthlyAmount: 0,
      PaymentType: null,
      PayrollId: null,
      PensionRate: null,
      SalaryHead: null,
      SalaryHeadId: null,
      SalaryHeadType: null,
      AccountNo: null,
      TransactionTypeId: null
    };

    this.empSalaryForm.EmployeeId = e.EmployeeId;
    this.empSalaryForm.PaymentType = e.PaymentType;

    // for popup data bind
    this.hourlyBasedSetPayrollList = e;

    this.empSalaryForm.CurrencyId = this.selectedCurrencyId;

    this.empSalaryForm.PensionRate = this.hourlyBasedSetPayrollList.employeepayrolllist[0].PensionRate;
    this.empSalaryForm.PaymentType = this.hourlyBasedSetPayrollList.employeepayrolllist[0].PaymentType;
  }
  //#endregion

  //#region "onRowClickApprovedSalaryHourlyBased"
  onRowClickApprovedSalaryHourlyBased(e: any) {
    this.showApprovedSalaryMoreDetailsPopup();

    this.empSalaryForm = {
      CurrencyId: 0,
      EmployeeId: 0,
      HeadTypeId: 0,
      MonthlyAmount: 0,
      PaymentType: null,
      PayrollId: null,
      PensionRate: null,
      SalaryHead: null,
      SalaryHeadId: null,
      SalaryHeadType: null,
      AccountNo: null,
      TransactionTypeId: null
    };

    this.empSalaryForm.EmployeeId = e.EmployeeId;
    this.empSalaryForm.PaymentType = e.PaymentType;

    // //for popup data bind
    this.approvedHourlySalaryDataSource = e;

    this.empSalaryForm.PensionRate = e.PensionRate;
    this.empSalaryForm.PaymentType = e.EmployeePayrollList[0].PaymentType;
  }
  //#endregion

  //#region "onIsAdvanceApprovedValueChanged"
  onIsAdvanceApprovedValueChanged(rowData: any, e: any, baseId: number) {
    const dataKey = rowData.key;
    if (dataKey) {
      dataKey.IsAdvanceApproved = e.value;
      if (e.value === true) {
        if (
          (dataKey.AdvanceAmount !== 0 || dataKey.AdvanceAmount != null) &&
          dataKey.IsAdvanceApproved === true
        ) {
          if (baseId === 1) {
            const itemIndex = this.employeePayrollByFixedBased.findIndex(
              item => item.EmployeeId === dataKey.EmployeeId
            );
            this.employeePayrollByFixedBased[itemIndex].GrossSalary -=
              dataKey.AdvanceRecoveryAmount;
          } else {
            const itemIndex = this.employeePayrollByHourlyBased.findIndex(
              item => item.EmployeeId === dataKey.EmployeeId
            );
          }
        }
      }
    }
    // Api hit once done
  }
  //#endregion

  //#region "onIsAdvanceRecoveryApprovedValueChanged"
  onIsAdvanceRecoveryApprovedValueChanged(
    rowData: any,
    e: any,
    baseId: number
  ) {
    const dataKey = rowData.data;
    if (dataKey) {
      if (e.value === true) {
        const itemIndex = this.employeePayrollByHourlyBased.findIndex(
          item => item.EmployeeId === dataKey.EmployeeId
        );

        this.employeePayrollByHourlyBased[itemIndex].IsAdvanceRecovery = true;
        this.employeePayrollByHourlyBased[itemIndex].NetSalary = parseFloat(
          (
            this.employeePayrollByHourlyBased[itemIndex].NetSalary -
            (dataKey.AdvanceRecoveryAmount == null ? 0 : dataKey.AdvanceRecoveryAmount)
          ).toFixed(4)
        );
        this.employeePayrollByHourlyBased[itemIndex].AdvanceAmount =
          this.employeePayrollByHourlyBased[itemIndex].AdvanceAmount -
          (dataKey.AdvanceRecoveryAmount == null ? 0 : dataKey.AdvanceRecoveryAmount);
        this.employeePayrollByHourlyBased[itemIndex].AdvanceRecoveryAmount =
        (dataKey.AdvanceRecoveryAmount == null ? 0 : dataKey.AdvanceRecoveryAmount);
        rowData.row.cells[18].column.allowEditing = true;
      } else {
        const itemIndex = this.employeePayrollByHourlyBased.findIndex(
          item => item.EmployeeId === dataKey.EmployeeId
        );

        this.employeePayrollByHourlyBased[itemIndex].IsAdvanceRecovery = false;
        this.employeePayrollByHourlyBased[itemIndex].NetSalary = parseFloat(
          (
            this.employeePayrollByHourlyBased[itemIndex].NetSalary +
            (dataKey.AdvanceRecoveryAmount == null ? 0 : dataKey.AdvanceRecoveryAmount)
          ).toFixed(4)
        );
        this.employeePayrollByHourlyBased[itemIndex].AdvanceAmount =
          this.employeePayrollByHourlyBased[itemIndex].AdvanceAmount +
          (dataKey.AdvanceRecoveryAmount == null ? 0 : dataKey.AdvanceRecoveryAmount);
        this.employeePayrollByHourlyBased[itemIndex].AdvanceRecoveryAmount =
        (dataKey.AdvanceRecoveryAmount == null ? 0 : dataKey.AdvanceRecoveryAmount);
       // rowData.row.cells[18].column.allowEditing = false;
      }
    }
    // Api hit once done
  }
  //#endregion

  // tslint:disable-next-line:member-ordering
  //conversionRate: any;
  //getExchangeRate(ToCurrencyId, FromCurrencyId) {
  //  const model = {
  //    FromCurrency: FromCurrencyId,
  //    ToCurrency: ToCurrencyId
  //  };
  //  this.codeService
  //    .getExchangeRate(
  //      this.setting.getBaseUrl() + GLOBAL.API_Hr_GetExchangeRate,
  //      model
  //    )
  //    .subscribe(data => {
  //      this.payrollRegisterLoading = false;
  //      this.conversionRate = data.data.ExchangeRateLists.Rate;
  //      this.empSalaryForm.PaymentType === 1
  //        ? this.EditPayrollDetails(1, this.fixedBasedSetPayrollList)
  //        : this.EditPayrollDetails(2, this.fixedBasedSetPayrollList);
  //    });
  //}
  //#endregion

  //#region "getEmployeeSalaryDetails"
  //getEmployeeSalaryDetails(salarySlipData: SelectSalarySlipDateModel) {
  //  this.showEmployeeSalaryLoader();
  //  this.empSalaryLoading = true;

  //  this.hrService
  //    .GetEmployeeSalaryDetails(
  //      this.setting.getBaseUrl() + GLOBAL.API_Hr_GetEmployeeSalaryDetails,
  //      salarySlipData.OfficeId,
  //      salarySlipData.Year,
  //      salarySlipData.Month,
  //      salarySlipData.EmployeeId
  //    )
  //    .subscribe(
  //      data => {
  //        this.employeeSalarySlipList = [];
  //        if (
  //          data.StatusCode === 200 &&
  //          data.data.EmployeeSalarySlipModelList != null &&
  //          data.data.EmployeeSalarySlipModelList.length > 0
  //        ) {
  //          this.selectSalarySlipDateForm = salarySlipData;
  //          this.employeeSalarySlipList = data.data.EmployeeSalarySlipModelList;
  //          this.showSalarySlipDetails();
  //        } else {
  //          if (
  //            data.data.EmployeeSalarySlipModelList == null ||
  //            data.data.EmployeeSalarySlipModelList.length === 0
  //          ) {
  //            this.toastr.warning('Salary Slip Can\'t Be Generate.');
  //          }
  //        }
  //        this.hideEmployeeSalaryLoader();
  //      },
  //      error => {
  //        if (error.StatusCode === 500) {
  //          this.toastr.error('Internal Server Error....');
  //        } else if (error.StatusCode === 401) {
  //          this.toastr.error('Unauthorized Access Error....');
  //        } else if (error.StatusCode === 403) {
  //          this.toastr.error('Forbidden Error....');
  //        }
  //        this.hideEmployeeSalaryLoader();
  //      }
  //    );
  //}
  //#endregion

  //#region "generatePdf"
  generatePdf() {
    const pdf = new jsPDF('p', 'pt', 'legal'),
      pdfConf = {
        pagesplit: false,
        background: '#fff'
      };

    pdf.addHTML($('#salarySlipReportPdf'), 0, 15, pdfConf, function() {
      pdf.save('Employee-Salary-Slip.pdf');
    });
  }
  //#endregion

  //#region "onClickSalarySlip"
  //onClickSalarySlip(data: any) {
  //  if (data != null) {
  //    this.selectSalarySlipDateForm = {
  //      Date: new Date(
  //        new Date(this.employeeePayrollFilterData.Date).getFullYear(),
  //        new Date(this.employeeePayrollFilterData.Date).getMonth(),
  //        new Date(this.employeeePayrollFilterData.Date).getDate(),
  //        new Date().getHours(),
  //        new Date().getMinutes(),
  //        new Date().getSeconds()
  //      ),
  //      Year: new Date(this.employeeePayrollFilterData.Date).getFullYear(),
  //      Month: new Date(this.employeeePayrollFilterData.Date).getMonth() + 1,
  //      EmployeeId: data.EmployeeId,
  //      // tslint:disable-next-line:radix
  //      OfficeId: this.selectedOffice
  //    };
  //    this.getEmployeeSalaryDetails(this.selectSalarySlipDateForm);
  //  }
  //}
  //#endregion

  //#region "onEmpSalaryFormSubmit"
  onEmpSalaryFormSubmit(data: any) {
    this.EditPayrollDetails(2, this.hourlyBasedSetPayrollList);
    this.hideHourlyBasedMoreDetailsPopup();
  }
  //#endregion

  //#region "onClickGenaratePaymentLink"
  onClickGenaratePaymentLink(data: any) {
    if (data != null) {
      this.employeeSalaryDetail = null; // object
      this.employeeSalaryDetail = {
        EmployeeId: data.EmployeeId,
        EmployeeName: data.EmployeeName,
        EmployeeCode: data.EmployeeCode,
        PresentHours: data.PresentDays + data.OverTimeHours + data.LeaveHours,

        // Currency
        CurrencyId: data.CurrencyId,
        CurrencyCode: this.currencyTypeDropdown.find(
          x => x.CurrencyId === data.CurrencyId
        ).CurrencyCode,
        CurrencyName: this.currencyTypeDropdown.find(
          x => x.CurrencyId === data.CurrencyId
        ).CurrencyName,

        // primary
        NetSalary: data.NetSalary,
        AdvanceRecoveryAmount: data.AdvanceRecoveryAmount, // Advance that will cut from the salary
        PensionAmount: data.PensionAmount,
        SalaryTax: data.SalaryTax,
        GrossSalary: data.GrossSalary,

        HourlyRate: data.HourlyRate,
        PensionRate: data.PensionRate,
        PaymentType: data.PaymentType,

        // Total Counts
        TotalAllowance: data.TotalAllowance,
        TotalDeduction: data.TotalDeduction,
        TotalGeneralAmount: data.TotalGeneralAmount, // Basic Pay
        TotalWorkHours: data.TotalWorkHours,

        EmployeePayrollListPrimary: null,
        EmployeePayrollList: data.EmployeePayrollList
      };
      this.employeeId = data.EmployeeId;
      this.showSalaryPaymentPopup();
    }
  }
  //#endregion

  //#region "getLevelFourAccountDetails"
  getLevelFourAccountDetails() {
    this.accountservice
      .GetAccountDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllInputLevelAccountCode
      )
      .subscribe(
        data => {
          this.levelFourAccounts = [];

          if (data.StatusCode === 200) {
            if (data.data.AccountDetailList != null) {
              data.data.AccountDetailList.forEach(element => {
                this.levelFourAccounts.push({
                  AccountCode: element.AccountCode,
                  AccountName: element.AccountName,
                  ChartOfAccountNewCode: element.ChartOfAccountNewCode
                });
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "ApproveEmployeeMonthlyPayrollList"
  DisapproveEmployeeMonthlyPayrollList(employeeList: any) {
    this.showSalaryApprovalConfirmLoading();

    this.employeeSalaryService
      .DisapproveEmployeeMonthlyPayrollList(
        this.setting.getBaseUrl() +
          GLOBAL.API_Account_DisapproveEmployeeApprovedSalary,
        employeeList
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Payroll Disapproved');
          }

          this.hideSalaryApprovalConfirmLoading();
          this.hideDisapproveConfirmationPopup();
          this.onEmployeeApprovedSalaryFilter(
            this.employeeApprovedSalaryFormFilter
          );
        },
        error => {}
      );
  }
  //#endregion

  onOfficeSelected(officeId: any) {
    this.selectedOffice = officeId;
  }


  //#region "Get all Office Details"
  getOfficeCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          if (
            data.StatusCode === 200 &&
            data.data.OfficeDetailsList.length > 0
          ) {
            const AllOffices = localStorage.getItem('ALLOFFICES') != null ? localStorage.getItem('ALLOFFICES').split(',') : [];
            data.data.OfficeDetailsList.forEach(element => {
              const officeFound = AllOffices.indexOf('' + element.OfficeId);
              if (officeFound !== -1) {
                this.officeDropdownList.push({
                  OfficeId: element.OfficeId,
                  OfficeCode: element.OfficeCode,
                  OfficeName: element.OfficeName,
                  SupervisorName: element.SupervisorName,
                  PhoneNo: element.PhoneNo,
                  FaxNo: element.FaxNo,
                  OfficeKey: element.OfficeKey
                });
              }
            });


              this.selectedOffice = this.selectedOffice === null ? this.officeDropdownList[0].OfficeId : this.selectedOffice;
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
          } else {
          }
        }
      );
  }
  //#endregion

  showEmployeeSalaryPaymentPopup(model) {
    this.salaryPaymentPopup = model;
  }

  //#region "show / hide"
  hideFixedBasedMoreDetailsPopup() {
    this.fixedBasedMoreDetailsPopup = false;
  }

  showHourlyBasedMoreDetailsPopup() {
    this.hourlyBasedMoreDetailsPopup = true;
  }

  hideHourlyBasedMoreDetailsPopup() {
    this.hourlyBasedMoreDetailsPopup = false;
  }

  showSalarySlipDetails() {
    this.hourlyBasedSalarySlipFlag = false;
  }

  closeSalarySlipDetails() {
    this.hourlyBasedSalarySlipFlag = true;
  }

  showEmployeeSalaryLoader() {
    this.empSalaryLoading = true;
  }

  hideEmployeeSalaryLoader() {
    this.empSalaryLoading = false;
  }

  // Employee Details Page
  showEmployeeMoreDetailsPopup() {
    this.employeeMoreDetailsPopup = true;
  }

  hideEmployeeMoreDetailsPopup() {
    this.employeeMoreDetailsPopup = false;
  }

  showApproveConfirmationPopup() {
    this.approveConfirmationPopup = true;
  }

  hideApproveConfirmationPopup() {
    this.approveConfirmationPopup = false;
  }

  showSaveConfirmationPopup() {
    this.saveConfirmationPopup = true;
  }

  hideSaveConfirmationPopup() {
    this.saveConfirmationPopup = false;
  }

  // Approved Salary more Detail
  showApprovedSalaryMoreDetailsPopup() {
    this.approvedSalaryMoreDetailsPopup = true;
  }
  hideApprovedSalaryMoreDetailsPopup() {
    this.approvedSalaryMoreDetailsPopup = false;
  }

  // loader
  showSalaryApprovalConfirmLoading() {
    this.salaryApprovalConfirmLoading = true;
  }

  hideSalaryApprovalConfirmLoading() {
    this.salaryApprovalConfirmLoading = false;
  }

  showSalaryPaymentPopup() {
    this.salaryPaymentPopup = true;
  }
  hideSalaryPaymentPopup() {
    this.salaryPaymentPopup = false;
  }

  showDisapproveConfirmationPopup() {
    this.disapproveConfirmationPopup = true;
  }

  hideDisapproveConfirmationPopup() {
    this.disapproveConfirmationPopup = false;
  }
  //#endregion

  openInfoTabs(e) {
    this.openInfoTab = e;
  }

  selectEmployeeSalaryTab(e) {
    e.id === 2
      ? (this.employeeSalaryTabFlag = false)
      : (this.employeeSalaryTabFlag = true);
  }
}

export class EmployeePayRollModel {
  EmployeeId: number;
  EmployeeCode: string;
  EmployeeName: string;
  PaymentType: number;
  PresentDays: number;
  TotalWorkHours: number;
  GrossSalary: any;
  WorkingDays: any;
  AbsentDays: any;
  LeaveDays: any;
  LeaveHours?: any;
  OverTimeHours: any;
  NetSalary?: any;
  TotalGeneralAmount: any;
  TotalAllowance: any;
  TotalDeduction: any;
  Year?: number;
  Month?: number;
  BalanceAdvanceAmount?: any;
  OvertimeMinutes?: any;
  WorkingMinutes?: any;

  SalaryTax: any;
  PensionAmount?: any;
  PensionRate?: any;
  CurrencyId?: number;

  employeepayrolllist: EmployeepayrollListModel[];
  IsApproved?: boolean;

  AdvanceAmount?: any;
  IsAdvanceApproved?: boolean;

  AdvanceRecoveryAmount?: any;
  IsAdvanceRecovery?: boolean;
}

export class EmployeepayrollListModel {
  CurrencyId: number;
  EmployeeId: number;
  HeadTypeId: number;
  MonthlyAmount: any;
  PaymentType: number;
  PayrollId: number;
  PensionRate: any;
  SalaryHead: string;
  SalaryHeadId: number;
  SalaryHeadType: string;
  AccountNo: number;
  TransactionTypeId: number;
  IsDeleted?: boolean;
}

export class EmployeePayrollFilterModel {
  Date: Date;
  Month?: number;
  Year?: number;
  SelectedPaymentType: number;
}

export class CurrencyTypeDropdown {
  CurrencyId: number;
  CurrencyCode: string;
  CurrencyName: string;
}

class SelectSalarySlipDateModel {
  Date: Date;
  Year?: number;
  Month?: number;
  OfficeId?: any;
  EmployeeId?: any;
}

class DisapproveSalaryModel {
  EmployeeList: any[];
  OfficeId: any;
  Year: any;
  Month: any;
}

class EmployeeSalarySlipModel {
  EmployeeCode: any;
  EmployeeName: any;
  Designation: any;
  Type: any;
  Office: any;
  Sex: any;

  BudgetLine: any;
  Program: any;
  ProjectId: any;
  JobId: any;
  Sector: any;
  Area: any;
  Account: any;
  SalaryPercentage: any;
  Salary: any;

  BasicSalary: any;
  CurrencyCode: any;
  Attendance: any;
  Absentese: any;

  PayrollId: any;
  HeadTypeId: any;
  SalaryHeadId: any;
  EmployeeId: any;
  CurrencyId: any;
  PaymentType: any;

  SalaryHeadType: any;
  SalaryHead: any;
  MonthlyAmount: any;
  PensionRatev: any;
  GrossSalary: any;
  NetSalary: any;
  Description: any;
}
