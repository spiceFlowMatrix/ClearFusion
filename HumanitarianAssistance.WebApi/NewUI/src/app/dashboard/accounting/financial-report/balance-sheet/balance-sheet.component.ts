import { Component, OnInit, HostListener } from '@angular/core';

import { AccountTypeModel } from '../models/financial-report.model';
import { CurrencyModel } from 'src/app/dashboard/project-management/project-list/project-details/models/project-details.model';
import { ToastrService } from 'ngx-toastr';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { GLOBAL } from 'src/app/shared/global';
import { AccountHeadTypes_Enum, AccountCategory_Enum } from 'src/app/shared/enum';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';

@Component({
  selector: 'app-balance-sheet',
  templateUrl: './balance-sheet.component.html',
  styleUrls: ['./balance-sheet.component.scss']
})
export class BalanceSheetComponent implements OnInit {
  //#region "variables"
  show = false;

  // CONST
  ASSETS_ID: number = AccountHeadTypes_Enum.Assets;
  LIABILITY_ID: number = AccountHeadTypes_Enum.Liabilities;
  DONORS_EQUITY_ID;
  NUMBER = AccountHeadTypes_Enum.OwnersEquity;

  // DATASOURCE
  assetsList: AccountTypeModel[] = [];
  liabilitiesList: AccountTypeModel[] = [];
  donorsEquityList: AccountTypeModel[] = [];
  currencyList: CurrencyModel[] = [];
  // FLAG
  inputFieldAssetsFlag = false;
  inputFieldLiabilitiesFlag = false;
  inputFieldDonorsEquityFlag = false;
  assetsListLoaderFlag = false;
  liabilitiesListLoaderFlag = false;
  donorsEquityListLoaderFlag = false;

  isEditingAllowed = false;
  pageId = ApplicationPages.BalanceSheet;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

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
  selectedCurrency: number;

  ngOnInit() {
    this.getCurrencies();
    this.getBalanceSheetAccountTypes();
    this.selectedDate = new Date();
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
  getBalanceSheetAccountTypes() {
    this.assetsListLoaderFlag = true;
    this.liabilitiesListLoaderFlag = true;
    this.donorsEquityListLoaderFlag = true;

    this.globalService
      .getListById(
        this.appUrl.getApiUrl() + GLOBAL.API_Code_GetAllAccountTypeByCategory,
        AccountCategory_Enum.BalanceSheet
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
                    AccountHeadTypeId: element.AccountHeadTypeId
                  });
                });

                this.assetsList = dataList.filter(
                  x => x.AccountHeadTypeId === this.ASSETS_ID
                );
                this.liabilitiesList = dataList.filter(
                  x => x.AccountHeadTypeId === this.LIABILITY_ID
                );

                dataList = []; // empty
              }
            }
          } else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong ! Try Again');
          }

          this.assetsListLoaderFlag = false;
          this.liabilitiesListLoaderFlag = false;
          this.donorsEquityListLoaderFlag = false;
        },
        error => {
          this.assetsListLoaderFlag = false;
          this.liabilitiesListLoaderFlag = false;
          this.donorsEquityListLoaderFlag = false;
        }
      );
  }
  //#endregion

  //#region "add"
  addBalanceSheetAccountTypes(model: any) {
    let obj: any = {};

    const index = model._index;
    const accountHeadTypeId = model.AccountHeadTypeId;

    // error handling
    if (accountHeadTypeId === this.ASSETS_ID) {
      this.assetsList[index]._IsLoading = true;
      this.assetsList[index]._IsError = false;
    } else if (accountHeadTypeId === this.LIABILITY_ID) {
      this.liabilitiesList[index]._IsLoading = true;
      this.liabilitiesList[index]._IsError = false;
    } else if (accountHeadTypeId === this.DONORS_EQUITY_ID) {
      this.donorsEquityList[index]._IsLoading = true;
      this.donorsEquityList[index]._IsError = false;
    }

    obj = {
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
            if (accountHeadTypeId === this.ASSETS_ID) {
              this.assetsList[index]._IsLoading = false;
              this.assetsList[index].Id = data.CommonId.Id;
            } else if (accountHeadTypeId === this.LIABILITY_ID) {
              this.liabilitiesList[index]._IsLoading = false;
              this.liabilitiesList[index].Id = data.CommonId.Id;
            } else if (accountHeadTypeId === this.DONORS_EQUITY_ID) {
              this.donorsEquityList[index]._IsLoading = false;
              this.donorsEquityList[index].Id = data.CommonId.Id;
            }
          } else if (data.StatusCode === 400) {
            if (accountHeadTypeId === this.ASSETS_ID) {
              this.assetsList[index]._IsError = true;
              this.assetsList[index]._IsLoading = false;
            } else if (accountHeadTypeId === this.LIABILITY_ID) {
              this.liabilitiesList[index]._IsError = true;
              this.liabilitiesList[index]._IsLoading = false;
            } else if (accountHeadTypeId === this.DONORS_EQUITY_ID) {
              this.donorsEquityList[index]._IsError = true;
              this.donorsEquityList[index]._IsLoading = false;
            }
            this.toastr.error('\'' + obj.AccountTypeName + '\'' + data.Message);
          }
        },
        error => {
          // error handling
          if (accountHeadTypeId === this.ASSETS_ID) {
            this.assetsList[index]._IsError = true;
            this.assetsList[index]._IsLoading = false;
          } else if (accountHeadTypeId === this.LIABILITY_ID) {
            this.liabilitiesList[index]._IsError = true;
            this.liabilitiesList[index]._IsLoading = false;
          } else if (accountHeadTypeId === this.DONORS_EQUITY_ID) {
            this.donorsEquityList[index]._IsError = true;
            this.donorsEquityList[index]._IsLoading = false;
          }
          this.toastr.error('Something went wrong ! Try Again');
        }
      );
  }
  //#endregion

  //#region "edit"
  editBalanceSheetAccountTypes(model: any) {
    const obj: any = {
      AccountTypeId: model.Id,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountCategory: model.AccountCategory,
      AccountTypeName: model.Name,
      _IsError: false
    };

    //#region "error handling"

    if (model.AccountHeadTypeId === this.ASSETS_ID) {
      const item = this.assetsList.find(x => x.Id === model.Id);
      const index = this.assetsList.indexOf(item);
      this.assetsList[index]._IsError = false;
      this.assetsList[index]._IsLoading = true;
    } else if (model.AccountHeadTypeId === this.LIABILITY_ID) {
      const item = this.liabilitiesList.find(x => x.Id === model.Id);
      const index = this.liabilitiesList.indexOf(item);
      this.liabilitiesList[index]._IsError = false;
      this.liabilitiesList[index]._IsLoading = true;
    }

    //#endregion

    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_Code_EditAccountType, obj)
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            //#region "error handling"
            if (model.AccountHeadTypeId === this.ASSETS_ID) {
              const item = this.assetsList.find(x => x.Id === model.Id);
              const index = this.assetsList.indexOf(item);
              this.assetsList[index]._IsError = false;
              this.assetsList[index]._IsLoading = false;
            } else if (model.AccountHeadTypeId === this.LIABILITY_ID) {
              const item = this.liabilitiesList.find(x => x.Id === model.Id);
              const index = this.liabilitiesList.indexOf(item);
              this.liabilitiesList[index]._IsError = false;
              this.liabilitiesList[index]._IsLoading = false;
            }
            //#endregion
          } else if (data.StatusCode === 400) {
            //#region "error handling"
            if (model.AccountHeadTypeId === this.ASSETS_ID) {
              const item = this.assetsList.find(x => x.Id === model.Id);
              const index = this.assetsList.indexOf(item);
              this.assetsList[index]._IsError = true;
              this.assetsList[index]._IsLoading = false;
            } else if (model.AccountHeadTypeId === this.LIABILITY_ID) {
              const item = this.liabilitiesList.find(x => x.Id === model.Id);
              const index = this.liabilitiesList.indexOf(item);
              this.liabilitiesList[index]._IsError = true;
              this.liabilitiesList[index]._IsLoading = false;
            }
            //#endregion
            this.toastr.error('\'' + obj.AccountTypeName + '\'' + data.Message);
          }
        },
        error => {
          //#region "error handling"
          if (model.AccountHeadTypeId === this.ASSETS_ID) {
            const item = this.assetsList.find(x => x.Id === model.Id);
            const index = this.assetsList.indexOf(item);
            this.assetsList[index]._IsError = true;
            this.assetsList[index]._IsLoading = false;
          } else if (model.AccountHeadTypeId === this.LIABILITY_ID) {
            const item = this.liabilitiesList.find(x => x.Id === model.Id);
            const index = this.liabilitiesList.indexOf(item);
            this.liabilitiesList[index]._IsError = true;
            this.liabilitiesList[index]._IsLoading = false;
          }
          //#endregion
          this.toastr.error('Something went wrong ! Try Again');
        }
      );
  }
  //#endregion

  //#region "onAdd"
  onAdd(type: number, value: string) {
    let obj: AccountTypeModel = {};
    if (type === this.ASSETS_ID) {
      this.toggleInputFieldAssets();

      obj = {
        Id: 0,
        AccountHeadTypeId: this.ASSETS_ID,
        AccountCategory: AccountCategory_Enum.BalanceSheet,
        Name: value,
        _index: this.assetsList.length
      };
      this.assetsList.push(obj);
    } else if (type === this.LIABILITY_ID) {
      this.toggleInputFieldLiabilities();

      obj = {
        Id: 0,
        AccountHeadTypeId: this.LIABILITY_ID,
        AccountCategory: AccountCategory_Enum.BalanceSheet,
        Name: value,
        _index: this.liabilitiesList.length
      };
      this.liabilitiesList.push(obj);
    }

    this.addBalanceSheetAccountTypes(obj);
  }
  //#endregion

  //#region "Emit"
  onValueChangeEmit(data) {
    this.editBalanceSheetAccountTypes(data);
  }

  addActionEmit(data) {
    this.addBalanceSheetAccountTypes(data);
  }
  //#endregion

  //#region "show / hide"
  toggleInputFieldAssets() {
    this.inputFieldAssetsFlag = !this.inputFieldAssetsFlag;
  }

  toggleInputFieldLiabilities() {
    this.inputFieldLiabilitiesFlag = !this.inputFieldLiabilitiesFlag;
  }

  toggleInputFieldDonorsEquity() {
    this.inputFieldDonorsEquityFlag = !this.inputFieldDonorsEquityFlag;
  }
  //#endregion
}
