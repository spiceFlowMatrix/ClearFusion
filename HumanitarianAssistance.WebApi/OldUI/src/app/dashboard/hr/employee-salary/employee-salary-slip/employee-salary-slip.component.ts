import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { Router } from '@angular/router';
import { AccountsService } from '../../../accounts/accounts.service';
import { HrService } from '../../hr.service';
import { CodeService } from '../../../code/code.service';
import { EmployeeSalaryService } from '../employeee-salary.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { EmployeePaymentDetail } from '../employee-salary.model';
import { PayrollHeadName } from '../../../../shared/enums';
import { TRISTATECHECKBOX_VALUE_ACCESSOR } from 'primeng/primeng';
import { CommonService } from '../../../../service/common.service';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-employee-salary-slip',
  templateUrl: './employee-salary-slip.component.html',
  styleUrls: ['./employee-salary-slip.component.css']
})
export class EmployeeSalarySlipComponent implements OnInit, OnChanges {
  //#region "variables"

  payrollHeadModelList: any[];
  transactionTypeDropdown: any;
  journalDropdown: any[];
  journalCode: number;
  showSalaryPaymentPopup: boolean;

  // input properties
  @Input() employeeSalaryDetail: EmployeePaymentDetail;
  @Input() levelFourAccounts: any;
  @Input() employeeId: number;
  @Input() inputDate: any;
  @Input() PayrollMonth: any;

  // output properties
  @Output() showEmployeeSalaryPaymentPopup = new EventEmitter<any>();

  voucherNo: number;
  referenceNo: string;

  salaryPaymentDetails: EmployeePayment;
  employeePaymentList: EmployeePayment[];

  // loader
  empSalaryLoading = false;

  constructor(
    private accountservice: AccountsService,
    private codeService: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) { }

  ngOnInit() {
    // this.salaryPaymentDetails = [];
    this.transactionTypeDropdown = [
      { TransactionTypeId: 1, TransactionTypeName: 'Credit' },
      { TransactionTypeId: 2, TransactionTypeName: 'Debit' }
    ];
    this.getEmployeeSalaryVoucher();
  }

  ngOnChanges() {
    this.getPrimarySalaryHeads(this.employeeId);
    this.getJournalDropdownList();
    this.getEmployeeSalaryVoucher();
    // this.getEmployeeSalaryVoucher(this.employeeId);
  }

  getEmployeeSalaryVoucher() {
    let Month = new Date(this.inputDate).getMonth();
    Month += 1;
    const Year = new Date(this.inputDate).getFullYear();

    this.voucherNo = null;
    this.referenceNo = null;

    this.accountservice
      .GetEmployeeSalaryVoucher(
        this.setting.getBaseUrl() +
        GLOBAL.API_Accounting_GetEmployeeSalaryVoucher,
        this.employeeId,
        Month,
        Year
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (data.data.VoucherReferenceNo != null) {
              this.voucherNo = data.data.VoucherNo;
              this.referenceNo = data.data.VoucherReferenceNo;
            }
          }
        },
        error => { }
      );
  }

  //#region  "Get all Primary Salary Heads"
  getPrimarySalaryHeads(EmployeeId: number) {
    this.accountservice
      .GetEmployeePayrollDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetPrimarySalaryHeads,
        EmployeeId
      )
      .subscribe(
        data => {
          this.payrollHeadModelList = [];

          if (data.StatusCode === 200) {
            if (data.data.PayrollHeadModelList != null) {
              data.data.PayrollHeadModelList.forEach(element => {
                this.payrollHeadModelList.push({
                  AccountNo: element.AccountNo,
                  PayrollHeadId: element.PayrollHeadId,
                  PayrollHeadName: element.PayrollHeadName,
                  PayrollHeadTypeId: element.PayrollHeadTypeId,
                  TransactionTypeId: element.TransactionTypeId,
                  Amount:
                    element.PayrollHeadId === PayrollHeadName.NetSalary
                      ? this.employeeSalaryDetail.NetSalary
                      : element.PayrollHeadId ===
                        PayrollHeadName.AdvanceDeduction
                        ? this.employeeSalaryDetail.AdvanceRecoveryAmount
                        : element.PayrollHeadId === PayrollHeadName.GrossSalary
                          ? this.employeeSalaryDetail.GrossSalary
                          : element.PayrollHeadId === PayrollHeadName.Pension
                            ? this.employeeSalaryDetail.PensionAmount
                            : element.PayrollHeadId === PayrollHeadName.SalaryTax
                              ? this.employeeSalaryDetail.SalaryTax
                              : 0
                });
              });
            }
          }
        },
        error => { }
      );
  }
  //#endregion

  //#region  "Get all Journal Dropdown"
  getJournalDropdownList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_JournalCode_GetAllJournalDetail
      )
      .subscribe(
        data => {
          this.journalDropdown = [];

          if (
            data.StatusCode === 200 &&
            data.data.JournalDetailList != null
          ) {
            if (
              data.data.JournalDetailList.length > 0
            ) {
              data.data.JournalDetailList.forEach(element => {
                this.journalDropdown.push({
                  JournalCode: element.JournalCode,
                  JournalName: element.JournalName
                });
              });

              // sort in Asc
              this.journalDropdown = this.commonService.sortDropdown(
                this.journalDropdown,
                'JournalName'
              );
            }
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
  //#endregion

  selectedJournal(e) {
    this.journalCode = e.value;
  }

  //#region "Reverse Salary Payments"
  onClickReverseSalary(VoucherNo) {
    this.empSalaryLoading = true;

    this.accountservice
      .ReverseEmployeeSalaryVoucher(
        this.setting.getBaseUrl() +
        GLOBAL.API_Accounting_ReverseEmployeeSalaryVoucher,
        VoucherNo
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.voucherNo = null;
            this.referenceNo = null;
            this.toastr.success('Salary Voucher Reversed');
            this.empSalaryLoading = false;
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
            this.empSalaryLoading = false;
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
            this.empSalaryLoading = false;
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
            this.empSalaryLoading = false;
          } else {
            this.empSalaryLoading = false;
          }
        }
      );
  }
  //#endregion

  onGenerateSalaryVoucher() {
    this.empSalaryLoading = true;

    this.salaryPaymentDetails = {
      CurrencyId: 0,
      EmployeeId: 0,
      EmployeePayrollLists: [],
      EmployeePayrollListPrimary: [],
      JournalCode: 0,
      OfficeId: 0,
      PresentHours: 0,
      PayrollMonth: null
    };

    this.employeeSalaryDetail.EmployeePayrollList.forEach(element => {
      this.salaryPaymentDetails.EmployeePayrollLists.push({
        SalaryHeadId: element.SalaryHeadId,
        HeadTypeId: element.HeadTypeId,
        AccountNo: element.AccountNo,
        TransactionTypeId: element.TransactionTypeId,
        Description: '',
        HeadName: element.SalaryHead,
        MonthlyAmount: element.MonthlyAmount
      });
    });

    this.salaryPaymentDetails.PayrollMonth = this.getLocalDate(this.PayrollMonth);

    this.payrollHeadModelList.forEach(element => {
      this.salaryPaymentDetails.EmployeePayrollListPrimary.push({
        CurrencyId: element.CurrencyId,
        EmployeeId: element.employeeId,
        MonthlyAmount: element.Amount,
        PayrollHeadId: element.PayrollHeadId,
        PayrollHeadName: element.PayrollHeadName,
        TransactionTypeId: element.TransactionTypeId,
        Amount: element.Amount,
        AccountNo: element.AccountNo
      });
    });

    // tslint:disable-next-line:radix
    this.salaryPaymentDetails.OfficeId = parseInt(
      localStorage.getItem('EMPLOYEEOFFICEID')
    );
    this.salaryPaymentDetails.EmployeeId = this.employeeId;
    this.salaryPaymentDetails.CurrencyId = this.employeeSalaryDetail.CurrencyId;
    this.salaryPaymentDetails.JournalCode = this.journalCode;
    this.salaryPaymentDetails.PresentHours = this.employeeSalaryDetail.PresentHours;

    if (this.journalCode != undefined) {
      this.accountservice
        .GetAllDetailsByModel(
          this.setting.getBaseUrl() + GLOBAL.API_Accounting_GenerateSalaryVoucher,
          this.salaryPaymentDetails
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.empSalaryLoading = false;
              this.referenceNo = data.data.VoucherReferenceNo;
              this.voucherNo = data.data.VoucherNo;

              this.showEmployeeSalaryPaymentPopup.emit({
                showEmployeeSalaryPaymentPopup: false
              });
            } else {
              this.empSalaryLoading = false;
              this.toastr.error(data.Message);
            }
          },
          error => {
            this.empSalaryLoading = false;
          }
        );
    } else {
      this.toastr.warning("Journal Not Selected");
      this.empSalaryLoading = false;
    }
  }

  //#region "getLocalDate"
  getLocalDate(date: any) {
    return new Date(
      new Date(date).getFullYear(),
      new Date(date).getMonth(),
      new Date(date).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds(),
      new Date().getMilliseconds(),
    );
  }
  //#endregion
}



export class EmployeePayment {
  EmployeePayrollListPrimary: any[];
  EmployeePayrollLists: any[];
  OfficeId: number;
  EmployeeId: number;
  CurrencyId: number;
  JournalCode: number;
  PresentHours: number;
  PayrollMonth: any;
}
