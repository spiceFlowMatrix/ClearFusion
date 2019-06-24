import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CodeService, HeadType } from '../code.service';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { SalaryHeadType, TransactionType } from '../../../shared/enums';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-set-payroll-account',
  templateUrl: './set-payroll-account.component.html',
  styleUrls: ['./set-payroll-account.component.css']
})
export class SetPayrollAccountComponent implements OnInit {
  payrollHeadList: PayrollHeadModel[];
  salaryHeadTypeDropdown: HeadType[];
  selectedPayrollList: any[];
  levelFourAccounts: any;
  transactionTypeDropdown: any;
  payrollHeadDataSource: any;
  checkBoxesMode: string;
  allMode: string;
  savingPayrollHead: boolean;
  isEditingAllowed = false;

  // Loader
  salaryHeadPopupLoading = false;

  constructor(
    private commonService: CommonService,
    private router: Router,
    private toastr: ToastrService,
    private setting: AppSettingsService,
    private codeservice: CodeService
  ) {
    this.checkBoxesMode = 'onClick';
    this.allMode = 'allPages';

    this.transactionTypeDropdown = [
      { TransactionTypeId: 1, TransactionTypeName: 'Credit' },
      { TransactionTypeId: 2, TransactionTypeName: 'Debit' }
    ];
  }

  ngOnInit() {
    this.payrollHeadDataSource = [];
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.JournalCodes
    );
    this.salaryHeadTypeDropdown = this.codeservice.getHeadType();
    this.getLevelFourAccountDetails();
    this.GetAllPayrollHeadDetails();
  }

  //#region "Get All Payroll Head"
  GetAllPayrollHeadDetails() {
    this.savingPayrollHead = true;

    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllPayrollHead
      )
      .subscribe(
        data => {
          this.payrollHeadDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.PayrollAccountHead.length > 0
          ) {
            data.data.PayrollAccountHead.forEach(element => {
              this.payrollHeadDataSource.push(element);
            });
            this.savingPayrollHead = false;
          }

          this.commonService.setLoader(false);
        },
        // tslint:disable-next-line:no-shadowed-variable
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
            this.savingPayrollHead = false;
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
            this.savingPayrollHead = false;
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
            this.savingPayrollHead = false;
          }
          this.commonService.setLoader(false);
        }
      );
  }
  //#endregion

  //#region "getLevelFourAccountDetails"
  getLevelFourAccountDetails() {
    this.codeservice
      .GetAllDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetLevelFourAccountDetails
      )
      .subscribe(
        data => {
          this.levelFourAccounts = [];

          if (data.StatusCode === 200) {
            if (data.data.AccountDetailList != null) {
              data.data.AccountDetailList.forEach(element => {
                this.levelFourAccounts.push({
                  AccountNo: element.AccountCode,
                  AccountName: element.AccountName
                });
              });

              // Sort in Ascending order
              this.commonService.sortDropdown(
                this.levelFourAccounts,
                'AccountName'
              );
            }
          }
        }
      );
  }
  //#endregion

  //#region "AddPayrollSalaryHead"
  AddPayrollSalaryHead(data) {
    const addPayrollHead: PayrollHeadModel = {
      PayrollHeadId: 0,
      PayrollHeadTypeId: data.PayrollHeadTypeId,
      PayrollHeadName: data.PayrollHeadName,
      Description: data.Description,
      AccountNo: data.AccountNo,
      TransactionTypeId: data.TransactionTypeId
    };

    this.salaryHeadPopupLoading = true;
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddPayrollAccountHead,
        addPayrollHead
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully!');
          } else if (data.StatusCode === 900) {
            this.toastr.error(data.Message);
          } else {
            this.toastr.error('Something went wrong!');
          }
          this.GetAllPayrollHeadDetails();
        },
        // tslint:disable-next-line:no-shadowed-variable
        error => {
          this.toastr.error('Something went wrong!');
        }
      );
  }
  //#endregion

  //#region "Update Salary Head"
  UpdatePayrollSalaryHead(data: any) {
    this.savingPayrollHead = true;
    const updatePayrollHead: PayrollHeadModel = {
      PayrollHeadId: data.PayrollHeadId,
      PayrollHeadTypeId: data.PayrollHeadTypeId,
      PayrollHeadName: data.PayrollHeadName,
      Description: data.Description,
      AccountNo: data.AccountNo,
      TransactionTypeId: data.TransactionTypeId
    };

    if (data.PayrollHeadTypeId === SalaryHeadType.ALLOWANCE) {
      updatePayrollHead.TransactionTypeId = TransactionType.Debit;
    } else if (
      data.PayrollHeadTypeId === SalaryHeadType.GENERAL ||
      data.PayrollHeadTypeId === SalaryHeadType.DEDUCTION
    ) {
      updatePayrollHead.TransactionTypeId = TransactionType.Credit;
    }

    this.salaryHeadPopupLoading = true;

    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_UpdatePayrollAccountHead,
        updatePayrollHead
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully!');
            this.savingPayrollHead = false;
          } else if (data.StatusCode === 900) {
            this.toastr.error(data.Message);
            this.savingPayrollHead = false;
          } else {
            this.toastr.error('Something went wrong!');
            this.savingPayrollHead = false;
          }
          this.GetAllPayrollHeadDetails();
          this.salaryHeadPopupLoading = false;
          this.savingPayrollHead = false;
        },
        // tslint:disable-next-line:no-shadowed-variable
        error => {
          this.toastr.error('Something went wrong!');
          this.savingPayrollHead = false;
        }
      );
  }
  //#endregion

  //#region "Update Salary Head"
  DeletePayrollSalaryHead(data: any) {
    const deletePayrollHead: PayrollHeadModel = {
      PayrollHeadId: data.PayrollHeadId,
      PayrollHeadTypeId: data.PayrollHeadTypeId,
      PayrollHeadName: data.PayrollHeadName,
      Description: data.Description,
      AccountNo: data.AccountNo,
      TransactionTypeId: data.TransactionTypeId
    };
    this.salaryHeadPopupLoading = true;
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_DeletePayrollAccountHead,
        deletePayrollHead
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Deleted Successfully!');
          } else if (data.StatusCode === 900) {
            this.toastr.error(data.Message);
          } else {
            this.toastr.error('Something went wrong!');
          }
          this.GetAllPayrollHeadDetails();
          this.salaryHeadPopupLoading = false;
        },
        // tslint:disable-next-line:no-shadowed-variable
        error => {
          this.toastr.error('Something went wrong!');
        }
      );
  }
  //#endregion

  //#region "Update Salary Head"
  saveSelectedPayrollHeads() {
    // tslint:disable-next-line:radix
    const OfficeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));

    this.selectedPayrollList.forEach(element => (element.OfficeId = OfficeId));

    this.savingPayrollHead = true;
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_UpdatePayrollAccountHeadAllEmployees,
        this.selectedPayrollList
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully!');
            this.savingPayrollHead = false;
          } else if (data.StatusCode === 900) {
            this.toastr.error(data.Message);
            this.savingPayrollHead = false;
          } else {
            this.toastr.error('Something went wrong!');
            this.savingPayrollHead = false;
          }
          this.GetAllPayrollHeadDetails();
          this.salaryHeadPopupLoading = false;
          this.savingPayrollHead = false;
        },
        // tslint:disable-next-line:no-shadowed-variable
        error => {
          this.toastr.error('Something went wrong!');
          this.savingPayrollHead = false;
        }
      );
  }
  //#endregion

  //#region "logEvent"
  logEvent(eventName, obj) {
    if (eventName === 'RowInserting') {
      this.AddPayrollSalaryHead(obj.data);
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData);
      this.UpdatePayrollSalaryHead(value);
    } else if (eventName === 'RowRemoving') {
      this.DeletePayrollSalaryHead(obj.data);
    }
  }
  //#endregion

  selectionChangedHandler(e) {
    this.selectedPayrollList = e.selectedRowsData;
  }
}

export interface PayrollHeadModel {
  PayrollHeadId: number;
  PayrollHeadTypeId: number;
  PayrollHeadName: string;
  Description: string;
  AccountNo: number;
  TransactionTypeId: number;
}
