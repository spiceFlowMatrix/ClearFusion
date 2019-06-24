import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../../accounts.service';
import { CodeService } from '../../../code/code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-income-expenditure-report',
  templateUrl: './income-expenditure-report.component.html',
  styleUrls: ['./income-expenditure-report.component.css']
})
export class IncomeExpenditureReportComponent implements OnInit {
  incomeFromDonorDataSource: any[];
  incomeFromProjectsDataSource: any[];
  profitOnBankDepositsDataSource: any[];
  incomeExpenditureFundDataSource: any[];

  accountsDataSource: any[];
  accountTypeDropdown: any[];

  // 1.Balance, 2.Credit, 3.Debit
  ValueSourceDataSource = [
    {
      ValueSourceId: 1,
      ValueSourceName: 'Balance'
    },
    {
      ValueSourceId: 2,
      ValueSourceName: 'Credit'
    },
    {
      ValueSourceId: 3,
      ValueSourceName: 'Debit'
    }
  ];

  // Loader
  incomeExpenditureloading = false;

  //#endregion

  constructor(
    private accountservice: AccountsService,
    private codeService: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.GetAccountDetails();
    this.getAccountType();
    this.GetAllIncomeExpenditureDetails();
  }

  //#region "GetAccountDetails"
  GetAccountDetails() {
    this.accountservice
      .GetAccountDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllControlLevelAccountCode
      )
      .subscribe(
        data => {
          this.accountsDataSource = [];
          if (data.StatusCode === 200 && data.data.AccountDetailList != null) {
            if (data.data.AccountDetailList.length > 0) {
              data.data.AccountDetailList.forEach(element => {
                this.accountsDataSource.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "GetAllIncomeExpenditureDetails"
  GetAllIncomeExpenditureDetails() {
    this.showIncomeExpenditureloading();

    // GetAllBalanceAndIncomeExpenditureDetails
    this.codeService
      .GetAllDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllCategoryPopulator
      )
      .subscribe(
        data => {
          this.incomeFromDonorDataSource = []; // 9
          this.incomeFromProjectsDataSource = []; // 10
          this.profitOnBankDepositsDataSource = []; // 11
          this.incomeExpenditureFundDataSource = []; // 12

          if (
            data.StatusCode === 200 &&
            data.data.CategoryPopulatorLst != null
          ) {
            if (data.data.CategoryPopulatorLst.length > 0) {
              data.data.CategoryPopulatorLst.forEach(element => {
                if (element.AccountTypeId === 9) {
                  this.incomeFromDonorDataSource.push(element); // 9
                } else if (element.AccountTypeId === 10) {
                  this.incomeFromProjectsDataSource.push(element); // 10
                } else if (element.AccountTypeId === 11) {
                  this.profitOnBankDepositsDataSource.push(element); // 11
                } else if (element.AccountTypeId === 12) {
                  this.incomeExpenditureFundDataSource.push(element); // 12
                }
              });
            }
          }
          this.hideIncomeExpenditureloading();
        },
        error => {
          this.hideIncomeExpenditureloading();
        }
      );
  }
  //#endregion

  //#region  "getAccountType"
  getAccountType() {
    this.codeService
      .getAccountType(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAllAccoutnType
      )
      .subscribe(
        data => {
          this.accountTypeDropdown = [];
          data != null || data !== undefined
            ? data.data.AccountTypeList.forEach(element => {
                this.accountTypeDropdown.push(element);
              })
            // tslint:disable-next-line:no-unused-expression
            : null;
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

  //#region "AddIncomeExpenditureDetails"
  AddIncomeExpenditure(model: IncomeExpenditure) {
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_AddCategoryPopulator,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully!');
          } else {
            this.toastr.error(data.Message);
          }
          this.GetAllIncomeExpenditureDetails();
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

  //#region "EditIncomeExpenditure"
  EditIncomeExpenditure(model: IncomeExpenditure) {
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_EditCategoryPopulator,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully!');
          }
          this.GetAllIncomeExpenditureDetails();
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

  //#region "DeleteIncomeExpenditure"
  DeleteIncomeExpenditure(categoryPopulatorId: number) {
    this.accountservice
      .DeleteCategoryPopulator(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_DeleteCategoryPopulator,
        categoryPopulatorId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Deleted Successfully!!!');
          }
          this.GetAllIncomeExpenditureDetails();
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

  //#region "logEvent"
  logEvent(eventName, obj, accountTypeNumber: number) {
    if (eventName === 'RowInserting') {
      const addCategoryPopulator: IncomeExpenditure = {
        CategoryPopulatorId: 0,
        SubCategoryLabel: obj.data.SubCategoryLabel,
        ChartOfAccountCode: obj.data.ChartOfAccountCode,
        AccountTypeId: accountTypeNumber,
        ValueSource: obj.data.ValueSource
      };

      this.AddIncomeExpenditure(addCategoryPopulator);
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      if (value.CategoryPopulatorId !== 0) {
        const editCategoryPopulator: IncomeExpenditure = {
          CategoryPopulatorId: value.CategoryPopulatorId,
          SubCategoryLabel: value.SubCategoryLabel,
          ChartOfAccountCode: value.ChartOfAccountCode,
          AccountTypeId: value.AccountTypeId,
          ValueSource: value.ValueSource
        };
        this.EditIncomeExpenditure(editCategoryPopulator);
      }
    } else if (eventName === 'RowRemoving') {
      const categoryPopulatorId = obj.key.CategoryPopulatorId;
      this.DeleteIncomeExpenditure(categoryPopulatorId);
    }
  }
  //#endregion

  //#region "show / hide"
  showIncomeExpenditureloading() {
    this.incomeExpenditureloading = true;
  }
  hideIncomeExpenditureloading() {
    this.incomeExpenditureloading = false;
  }
  //#endregion
}

class IncomeExpenditure {
  CategoryPopulatorId: number;
  SubCategoryLabel: string;
  ChartOfAccountCode: number;
  AccountTypeId: number;
  ValueSource: number;
}
