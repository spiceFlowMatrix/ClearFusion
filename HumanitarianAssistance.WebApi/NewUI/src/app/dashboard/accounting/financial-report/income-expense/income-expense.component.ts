import { Component, OnInit, HostListener } from '@angular/core';
import {
  AccountHeadTypes_Enum,
  AccountCategory_Enum
} from '../../../../shared/enum';
import { AccountTypeModel } from '../models/financial-report.model';
import { GlobalService } from '../../../../shared/services/global-services.service';
import { AppUrlService } from '../../../../shared/services/app-url.service';
import { GLOBAL } from '../../../../shared/global';

import { CurrencyModel } from 'src/app/dashboard/project-management/project-list/project-details/models/project-details.model';

import { ToastrService } from 'ngx-toastr';
import { ApplicationPages } from '../../../../shared/applicationpagesenum';
import { LocalStorageService } from '../../../../shared/services/localstorage.service';

@Component({
  selector: 'app-income-expense',
  templateUrl: './income-expense.component.html',
  styleUrls: ['./income-expense.component.scss']
})
export class IncomeExpenseComponent implements OnInit {
  //#region "variables"
  show = false;

  // CONST
  INCOME_ID: number = AccountHeadTypes_Enum.Income;
  EXPENSE_ID: number = AccountHeadTypes_Enum.Expense;

  // DATASOURCE
  incomeList: AccountTypeModel[] = [];
  expenseList: AccountTypeModel[] = [];
  currencyList: CurrencyModel[] = [];

  // FLAG
  inputFieldIncomeFlag = false;
  inputFieldExpenseFlag = false;
  incomeListLoaderFlag = false;
  expenseListLoaderFlag = false;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  isEditingAllowed = false;
  pageId = ApplicationPages.IncomeExpenseReport;

  //#endregion

  constructor(
    private globalService: GlobalService,
    private appUrl: AppUrlService,
    private toastr: ToastrService,
    private localStorageService: LocalStorageService
  ) {
    this.getScreenSize();
  }

  selectedDate: Date;
  selectedToDate: Date;
  selectedCurrency: number;

  ngOnInit() {
    this.getCurrencies();
    this.getIncomeExpenseAccountTypes();
    this.selectedDate = new Date();
    this.selectedToDate = new Date();
    this.selectedCurrency = 1;
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  getCurrencies() {
    this.globalService
      .getList(this.appUrl.getApiUrl() + GLOBAL.API_code_GetAllCurrency)
      .subscribe(data => {
        if (data.StatusCode === 200) {
          if (data.data.CurrencyList != null) {
            if (data.data.CurrencyList.length > 0) {
              const currencyList: CurrencyModel[] = [];
              data.data.CurrencyList.forEach(element => {
                this.currencyList.push({
                  CurrencyId: element.CurrencyId,
                  CurrencyCode: element.CurrencyCode
                });
              });
            }
          }
        } else if (data.StatusCode === 400) {

        }
      });
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
  }

  //#region "get"
  getIncomeExpenseAccountTypes() {
    this.incomeListLoaderFlag = true;
    this.expenseListLoaderFlag = true;

    this.globalService
      .getListById(
        this.appUrl.getApiUrl() + GLOBAL.API_Code_GetAllAccountTypeByCategory,
        AccountCategory_Enum.IncomeExpenseReport
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (data.data.AccountTypeList != null) {
              if (data.data.AccountTypeList.length > 0) {
                let dataList: AccountTypeModel[] = [];

                data.data.AccountTypeList.forEach(element => {
                  dataList.push({
                    Id: element.AccountTypeId,
                    Name: element.AccountTypeName,
                    AccountCategory: element.AccountCategory,
                    AccountHeadTypeId: element.AccountHeadTypeId,
                    Balance: 0
                  });
                });

                this.incomeList = dataList.filter(
                  x => x.AccountHeadTypeId === this.INCOME_ID
                );
                this.expenseList = dataList.filter(
                  x => x.AccountHeadTypeId === this.EXPENSE_ID
                );

                dataList = []; // empty
              }
            }
          } else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong ! Try Again');
          }
          this.incomeListLoaderFlag = false;
          this.expenseListLoaderFlag = false;
        },
        error => {
          this.incomeListLoaderFlag = false;
          this.expenseListLoaderFlag = false;
        }
      );
  }
  //#endregion

  //#region "add"
  addIncomeExpenseAccountTypes(model: any) {
    const index = model._index;
    const accountHeadTypeId = model.AccountHeadTypeId;

    // error handling
    if (accountHeadTypeId === this.INCOME_ID) {
      this.incomeList[index]._IsLoading = true;
      this.incomeList[index]._IsError = false;
    } else if (accountHeadTypeId === this.EXPENSE_ID) {
      this.expenseList[index]._IsLoading = true;
      this.expenseList[index]._IsError = false;
    }

    const obj: any = {
      AccountTypeId: model.Id,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountCategory: model.AccountCategory,
      AccountTypeName: model.Name
    };

    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_Code_AddAccountType, obj)
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (accountHeadTypeId === this.INCOME_ID) {
              this.incomeList[index]._IsLoading = false;
              this.incomeList[index].Id = data.CommonId.Id;
            } else if (accountHeadTypeId === this.EXPENSE_ID) {
              this.expenseList[index]._IsLoading = false;
              this.expenseList[index].Id = data.CommonId.Id;
            }
          } else if (data.StatusCode === 400) {
            if (accountHeadTypeId === this.INCOME_ID) {
              this.incomeList[index]._IsLoading = false;
              this.incomeList[index]._IsError = true;
            } else if (accountHeadTypeId === this.EXPENSE_ID) {
              this.expenseList[index]._IsLoading = false;
              this.expenseList[index]._IsError = true;
            }
            this.toastr.error(data.Message);
          }
        },
        error => {
          // error handling
          if (accountHeadTypeId === this.INCOME_ID) {
            this.incomeList[index]._IsLoading = false;
            this.incomeList[index]._IsError = true;
          } else if (accountHeadTypeId === this.EXPENSE_ID) {
            this.expenseList[index]._IsLoading = false;
            this.expenseList[index]._IsError = true;
          }
          this.toastr.error('Something went wrong ! Try Again');
        }
      );
  }
  //#endregion

  //#region "edit"
  editIncomeExpenseAccountTypes(model: any) {
    const obj: any = {
      AccountTypeId: model.Id,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountCategory: model.AccountCategory,
      AccountTypeName: model.Name
    };

    //#region "error handling"
    if (model.AccountHeadTypeId === this.INCOME_ID) {
      const item = this.incomeList.find(x => x.Id === model.Id);
      const index = this.incomeList.indexOf(item);
      this.incomeList[index]._IsError = false;
      this.incomeList[index]._IsLoading = true;
    } else if (model.AccountHeadTypeId === this.EXPENSE_ID) {
      const item = this.expenseList.find(x => x.Id === model.Id);
      const index = this.expenseList.indexOf(item);
      this.expenseList[index]._IsError = false;
      this.expenseList[index]._IsLoading = true;
    }
    //#endregion

    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_Code_EditAccountType, obj)
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            //#region "error handling"
            if (model.AccountHeadTypeId === this.INCOME_ID) {
              const item = this.incomeList.find(x => x.Id === model.Id);
              const index = this.incomeList.indexOf(item);
              this.incomeList[index]._IsError = false;
              this.incomeList[index]._IsLoading = false;
            } else if (model.AccountHeadTypeId === this.EXPENSE_ID) {
              const item = this.expenseList.find(x => x.Id === model.Id);
              const index = this.expenseList.indexOf(item);
              this.expenseList[index]._IsError = false;
              this.expenseList[index]._IsLoading = false;
            }

            //#endregion
          } else if (data.StatusCode === 400) {
            //#region "error handling"
            if (model.AccountHeadTypeId === this.INCOME_ID) {
              const item = this.incomeList.find(x => x.Id === model.Id);
              const index = this.incomeList.indexOf(item);
              this.incomeList[index]._IsError = true;
              this.incomeList[index]._IsLoading = false;
            } else if (model.AccountHeadTypeId === this.EXPENSE_ID) {
              const item = this.expenseList.find(x => x.Id === model.Id);
              const index = this.expenseList.indexOf(item);
              this.expenseList[index]._IsError = true;
              this.expenseList[index]._IsLoading = false;
            }

            //#endregion
            this.toastr.error('\'' + obj.AccountTypeName + '\'' + data.Message);
          }
        },
        error => {
          //#region "error handling"
          if (model.AccountHeadTypeId === this.INCOME_ID) {
            const item = this.incomeList.find(x => x.Id === model.Id);
            const index = this.incomeList.indexOf(item);
            this.incomeList[index]._IsError = true;
            this.incomeList[index]._IsLoading = false;
          } else if (model.AccountHeadTypeId === this.EXPENSE_ID) {
            const item = this.expenseList.find(x => x.Id === model.Id);
            const index = this.expenseList.indexOf(item);
            this.expenseList[index]._IsError = true;
            this.expenseList[index]._IsLoading = false;
          }
          //#endregion
          this.toastr.error('Something went wrong !');
        }
      );
  }
  //#endregion

  //#region "onAdd"
  onAdd(type: number, value: string) {
    let obj: AccountTypeModel = {};
    if (type === this.INCOME_ID) {
      this.toggleInputFieldIncome();

      obj = {
        Id: 0,
        AccountHeadTypeId: this.INCOME_ID,
        AccountCategory: AccountCategory_Enum.IncomeExpenseReport,
        Name: value,
        _index: this.incomeList.length,
        _IsError: false
      };
      this.incomeList.push(obj);
    } else if (type === this.EXPENSE_ID) {
      this.toggleInputFieldExpense();

      obj = {
        Id: 0,
        AccountHeadTypeId: this.EXPENSE_ID,
        AccountCategory: AccountCategory_Enum.IncomeExpenseReport,
        Name: value,
        _index: this.expenseList.length,
        _IsError: false
      };
      this.expenseList.push(obj);
    }

    this.addIncomeExpenseAccountTypes(obj);
  }
  //#endregion

  //#region "Emit"
  onValueChangeEmit(data) {
    this.editIncomeExpenseAccountTypes(data);
  }

  addActionEmit(data) {
    this.addIncomeExpenseAccountTypes(data);
  }
  //#endregion

  //#region "show / hide"
  toggleInputFieldIncome() {
    this.inputFieldIncomeFlag = !this.inputFieldIncomeFlag;
  }

  toggleInputFieldExpense() {
    this.inputFieldExpenseFlag = !this.inputFieldExpenseFlag;
  }
  //#endregion
}
