import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { AccountsService } from '../../accounts.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { CodeService } from '../../../code/code.service';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService } from '../../../../service/common.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html'
})
export class TransactionComponent implements OnInit {
  dataSource: any[];
  voucherNumber: any;
  AccountsArr: any[];
  projectArr: any[];
  projectBudgetLineArr: any[] = [];
  budgetLineFilteredList: any;
  transactionsLoading = false;
  @Input() isEditingAllowed: boolean;

  transactionFlag: boolean;

  constructor(
    private accountservice: AccountsService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private commonservice: CommonService,
    private codeservice: CodeService
  ) {
    this.getFilteredBudgetLines = this.getFilteredBudgetLines.bind(this);
    this.voucherNumber = this.commonservice.voucherNumber;
  }

  ngOnInit() {
    this.getAllProjectDetails();
    this.GetAccountDetails();
  }

  GetAccountDetails() {
    this.accountservice
      .GetAccountDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllInputLevelAccountCode
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

  GetLevelFourAccountDetails() {
    this.accountservice
      .GetAccountDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllInputLevelAccountCode
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

  GetAllVoucherTransactionDetail(selectedVoucherNumber: number) {
    this.accountservice
      .GetDetailByVoucherNo(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllVoucherTransactionDetail,
        selectedVoucherNumber
      )
      .subscribe(
        data => {
          this.dataSource = [];
          if (data.StatusCode === 200) {
            data.data.VoucherTransactionList.forEach(element => {
              this.dataSource.push(element);
            });
            this.transactionsLoading = false;
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
            this.transactionsLoading = false;
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
            this.transactionsLoading = false;
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
            this.transactionsLoading = false;
          } else {
            this.transactionsLoading = false;
          }
        }
      );
  }

  //#region getAllProjectDetails
  getAllProjectDetails() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetProjectAndBudgetLine
      )
      .subscribe(
        data => {
          this.projectArr = [];
          // this.projectBudgetLineArr = [];
          if (
            data.data.ProjectBudgetLinesModel != null &&
            data.StatusCode === 200
          ) {
            if (data.data.ProjectBudgetLinesModel.ProjectList.length > 0) {
              data.data.ProjectBudgetLinesModel.ProjectList.forEach(element => {
                this.projectArr.push(element);
              });
            }
            if (data.data.ProjectBudgetLinesModel.BudgetLines.length > 0) {
              data.data.ProjectBudgetLinesModel.BudgetLines.forEach(element => {
                this.projectBudgetLineArr.push(element);
              });
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
          }
        }
      );
  }

  //#endregion

  setProjectValue(rowData: any, value: any): void {
    rowData.ProjectId = null;
    (<any>this).defaultSetCellValue(rowData, value);
  }

  getFilteredBudgetLines(options) {
    return {
      store: this.projectBudgetLineArr,
      filter: options.data ? ['ProjectId', '=', options.data.ProjectId] : null
    };
  }

  onEditorPreparing(e) {
    if (e.parentType === 'dataRow' && e.dataField === 'BudgetLineId') {
      e.editorOptions.disabled = typeof e.row.data.ProjectId !== 'number';
    }
  }

  //#region "logEvent"
  logEvent(eventName, obj) {
    //

    // if(eventName=='EditingStart' && obj.data.ProjectId !=null && this.projectBudgetLineArr !=null)
    // {
    //     this.budgetLineFilteredList = this.projectBudgetLineArr.filter(x => x.ProjectId == obj.data.ProjectId);

    // }

    if (eventName === 'InitNewRow') {
      obj.data.Credit = 0;
      obj.data.Debit = 0;
    } else if (eventName === 'RowInserting') {
      // tslint:disable-next-line:radix
      obj.data.VoucherNo = parseInt(
        localStorage.getItem('SelectedVoucherNumber')
      );
      // tslint:disable-next-line:radix
      obj.data.CurrencyId = parseInt(
        localStorage.getItem('SelectedVoucherCurrency')
      );
      // tslint:disable-next-line:radix
      obj.data.OfficeId = parseInt(localStorage.getItem('SelectedOfficeId'));
      // tslint:disable-next-line:radix
      obj.data.FinancialYearId = parseInt(
        localStorage.getItem('SelectedFinancialYearId')
      );
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      // tslint:disable-next-line:radix
      value.VoucherNo = parseInt(localStorage.getItem('SelectedVoucherNumber'));
      // tslint:disable-next-line:radix
      value.CurrencyId = parseInt(
        localStorage.getItem('SelectedVoucherCurrency')
      );
      // tslint:disable-next-line:radix
      value.OfficeId = parseInt(localStorage.getItem('SelectedOfficeId'));
      // tslint:disable-next-line:radix
      value.FinancialYearId = parseInt(
        localStorage.getItem('SelectedFinancialYearId')
      );

      if (obj.newData.ProjectId !== 0) {
        obj.oldData.BudgetLineId = null;
      }
    } else if (eventName === 'RowRemoving') {
      if (obj.key.__KEY__ !== undefined) {
        const transactionId = obj.key.__KEY__;
        this.dataSource.splice(
          this.dataSource.findIndex(e => e.__KEY__ === transactionId),
          1
        );
      } else {
        const transactionId = obj.key.TransactionId;
        this.dataSource.splice(
          this.dataSource.findIndex(e => e.TransactionId === transactionId),
          1
        );
      }
    }

    // else if (eventName == "onEditorPreparing") {
    // }
  }
  //#endregion

  onValueChanged(e: any) {}

  AddVoucherTransaction(model) {
    this.accountservice
      .AddVoucherTransaction(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_AddVouchersTransaction,
          model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Transaction Added Successfully!!!');
          } else {
            this.toastr.error(data.Message);
          }
          this.GetAllVoucherTransactionDetail(
            // tslint:disable-next-line:radix
            parseInt(localStorage.getItem('SelectedVoucherNumber'))
          );
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

  EditVoucherTransaction(model) {
    this.accountservice
      .EditVoucherTransaction(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_EditVouchersTransaction,
          model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Transaction Updated Successfully!!!');
          }
          this.GetAllVoucherTransactionDetail(
            // tslint:disable-next-line:radix
            parseInt(localStorage.getItem('SelectedVoucherNumber'))
          );
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

  DeleteVoucherTransaction(transactionId: number) {
    this.accountservice
      .DeleteVoucherTransactionDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_DeleteVoucherTransactionDetail,
        transactionId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Transaction Deleted Successfully!!!');
          }
          this.GetAllVoucherTransactionDetail(
            // tslint:disable-next-line:radix
            parseInt(localStorage.getItem('SelectedVoucherNumber'))
          );
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

  SaveTransactions() {
    this.transactionsLoading = true;

    const VoucherTransactionList: any = [];

    this.dataSource.forEach(element => {
      VoucherTransactionList.push({
        AccountNo: element.AccountNo,
        Credit: element.Credit,
        Description: element.Description,
        FinancialYearId: element.FinancialYearId,
        VoucherNo: element.VoucherNo,
        Debit: element.Debit,
        ProjectId: element.ProjectId,
        BudgetLineId: element.BudgetLineId,
        // tslint:disable-next-line:radix
        OfficeId: parseInt(localStorage.getItem('OFFICEID'))
      });
    });

    if (this.dataSource.length !== 0) {
      const DebitAmount = this.Sum(VoucherTransactionList, 'Debit');
      const CreditAmount = this.Sum(VoucherTransactionList, 'Credit');

      if (DebitAmount === CreditAmount) {
        this.accountservice
          .AddVoucherTransactionList(
            this.setting.getBaseUrl() +
              GLOBAL.API_Accounting_AddVouchersTransaction,
            VoucherTransactionList
          )
          .subscribe(
            data => {
              if (data.StatusCode === 200) {
                this.toastr.success('Transaction Added Successfully!!!');
                this.transactionsLoading = false;
              } else {
                this.toastr.error(data.Message);
                this.transactionsLoading = false;
              }
              this.GetAllVoucherTransactionDetail(
                // tslint:disable-next-line:radix
                parseInt(localStorage.getItem('SelectedVoucherNumber'))
              );
            },
            error => {
              if (error.StatusCode === 500) {
                this.toastr.error('Internal Server Error....');
                this.transactionsLoading = false;
              } else if (error.StatusCode === 401) {
                this.toastr.error('Unauthorized Access Error....');
                this.transactionsLoading = false;
              } else if (error.StatusCode === 403) {
                this.toastr.error('Forbidden Error....');
                this.transactionsLoading = false;
              } else {
                this.transactionsLoading = false;
              }
            }
          );
      } else {
        this.toastr.error(
          'Total debit and total credit amount should be equal'
        );
        this.transactionsLoading = false;
      }
    } else {
      this.accountservice
        .DeleteVoucherTransactionDetails(
          this.setting.getBaseUrl() +
            GLOBAL.API_Accounting_DeleteVoucherTransactions,
          // tslint:disable-next-line:radix
          parseInt(localStorage.getItem('SelectedVoucherNumber'))
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Transaction Deleted Successfully!!!');
            }
            this.GetAllVoucherTransactionDetail(
              // tslint:disable-next-line:radix
              parseInt(localStorage.getItem('SelectedVoucherNumber'))
            );
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
  }

  //#region "onShowEditVoucherForm"
  onShowEditVoucherForm(e: any) {
    if (this.projectBudgetLineArr != null) {
      //
    }
  }
  //#endregion

  onValidationCallbackCredit(e: any) {
    // your logic here
    if (e != null) {
      if (e.data != null) {
        return (e.data.Debit > 0 && e.data.Credit === 0) ||
          (e.data.Credit === 0 && e.data.Debit === 0) ||
          (e.data.Credit > 0 && e.data.Debit === 0) ||
          (e.data.Credit === 0 && e.data.Debit === 0)
          ? true
          : false;
      }
    }
  }
  //#endregion

  Sum = function(items, prop) {
    return items.reduce(function(a, b) {
      return a + b[prop];
    }, 0);
  };
}
