import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../../accounts.service';
import { CodeService } from '../../../code/code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService } from '../../../../service/common.service';

@Component({
  selector: 'app-balance-sheet',
  templateUrl: './balance-sheet.component.html',
  styleUrls: ['./balance-sheet.component.css']
})
export class BalanceSheetComponent implements OnInit {
  capitalAssetsWrittenOffDataSource: any[]; // 1
  currentAssetsDataSource: any[]; // 2
  fundsDataSource: any[]; // 3
  endownmentFundDataSource: any[]; // 4
  reserveAccountAdjustmentDataSource: any[]; // 5
  longtermLiabilityDataSource: any[]; // 6
  currentLiabilityDataSource: any[]; // 7
  reserveAccountDataSource: any[]; // 8
  accountsDataSource: any[];
  accountTypeDropdown: any[];

  // Loader
  balanceSheetloading = false;

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

  constructor(
    private accountservice: AccountsService,
    private codeService: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) {}

  ngOnInit() {
    this.GetAccountDetails();
    this.getAccountType();
    this.GetAllCategoryPopulator();
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

              // sort in Asc
              this.accountsDataSource = this.commonService.sortDropdown(
                this.accountsDataSource,
                'AccountName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "GetAllCategoryPopulator"
  GetAllCategoryPopulator() {
    this.showBalanceSheetloading();
    this.codeService
      .GetAllDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllCategoryPopulator
      )
      .subscribe(
        data => {
          this.capitalAssetsWrittenOffDataSource = []; // 1
          this.currentAssetsDataSource = []; // 2
          this.fundsDataSource = []; // 3
          this.endownmentFundDataSource = []; // 4
          this.reserveAccountAdjustmentDataSource = []; // 5
          this.longtermLiabilityDataSource = []; // 6
          this.currentLiabilityDataSource = []; // 7
          this.reserveAccountDataSource = []; // 8

          if (
            data.StatusCode === 200 &&
            data.data.CategoryPopulatorLst != null
          ) {
            if (data.data.CategoryPopulatorLst.length > 0) {
              data.data.CategoryPopulatorLst.forEach(element => {
                if (element.AccountTypeId === 1) {
                  this.capitalAssetsWrittenOffDataSource.push(element); // 1
                } else if (element.AccountTypeId === 2) {
                  this.currentAssetsDataSource.push(element); // 2
                } else if (element.AccountTypeId === 3) {
                  this.fundsDataSource.push(element); // 3
                } else if (element.AccountTypeId === 4) {
                  this.endownmentFundDataSource.push(element); // 4
                } else if (element.AccountTypeId === 5) {
                  this.reserveAccountAdjustmentDataSource.push(element); // 5
                } else if (element.AccountTypeId === 6) {
                  this.longtermLiabilityDataSource.push(element); // 6
                } else if (element.AccountTypeId === 7) {
                  this.currentLiabilityDataSource.push(element); // 7
                } else if (element.AccountTypeId === 8) {
                  this.reserveAccountDataSource.push(element); // 8
                }
              });
            }
          }
          this.hideBalanceSheetloading();
        },
        error => {
          this.hideBalanceSheetloading();
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

  //#region "AddCategoryPopulator"
  AddCategoryPopulator(model: CategoryPopulator) {
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
          this.GetAllCategoryPopulator();
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

  //#region "EditCategoryPopulator"
  EditCategoryPopulator(model: CategoryPopulator) {
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
          this.GetAllCategoryPopulator();
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

  //#region "DeleteCategoryPopulator"
  DeleteCategoryPopulator(categoryPopulatorId: number) {
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
          this.GetAllCategoryPopulator();
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
      const addCategoryPopulator: CategoryPopulator = {
        CategoryPopulatorId: 0,
        SubCategoryLabel: obj.data.SubCategoryLabel,
        ChartOfAccountCode: obj.data.ChartOfAccountCode,
        AccountTypeId: accountTypeNumber,
        ValueSource: obj.data.ValueSource
      };

      this.AddCategoryPopulator(addCategoryPopulator);
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      const editCategoryPopulator: CategoryPopulator = {
        CategoryPopulatorId: value.CategoryPopulatorId,
        SubCategoryLabel: value.SubCategoryLabel,
        ChartOfAccountCode: value.ChartOfAccountCode,
        AccountTypeId: value.AccountTypeId,
        ValueSource: value.ValueSource
      };

      this.EditCategoryPopulator(editCategoryPopulator);
    } else if (eventName === 'RowRemoving') {
      const categoryPopulatorId = obj.key.CategoryPopulatorId;
      this.DeleteCategoryPopulator(categoryPopulatorId);
    }
  }
  //#endregion

  //#region "show / hide"
  showBalanceSheetloading() {
    this.balanceSheetloading = true;
  }
  hideBalanceSheetloading() {
    this.balanceSheetloading = false;
  }
  //#endregion
}

class CategoryPopulator {
  CategoryPopulatorId: number;
  SubCategoryLabel: string;
  ChartOfAccountCode: number;
  AccountTypeId: number;
  ValueSource: number;
}
