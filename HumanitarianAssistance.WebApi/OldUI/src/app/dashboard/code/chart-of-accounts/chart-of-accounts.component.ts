import { Component, OnInit } from '@angular/core';
import {
  CodeService,
  MainLevelAccount,
  AccountLevel,
  chartOfAccountsData,
  AccountType,
  ControlLevelAccount,
  SubLevelAccount,
  InputLevelAccount,
} from '../code.service';
import { Toast, ToastrService } from 'ngx-toastr';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { GLOBAL } from '../../../shared/global';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-chart-of-accounts',
  templateUrl: './chart-of-accounts.component.html',
  styleUrls: ['./chart-of-accounts.component.css']
})
export class ChartOfAccountsComponent implements OnInit {
  windows: any;
  popupVisibleAddChartOfAccount = false;
  popupVisibleEditChartOfAccount = false;
  formToggle: number;
  isEditingAllowed = false;

  // popup main level filter
  selectValue = 1;

  // Forms Classes
  fMainLevelAccount: MainLevelAccount;
  fControlLevelAccount: ControlLevelAccount;
  fSubLevelAccount: SubLevelAccount;
  fInputLevelAccount: InputLevelAccount;

  // Dropdowns for Add popup
  mainLevelArr: any[];
  controlLevelArr: any[];
  subLevelArr: any[];

  // dataSource
  chartOfAccountsData: chartOfAccountsData[];
  mainLeveFormlData: MainLevelAccount[];
  accountTypeDropdown: AccountType[];
  accountLevelDropdown: AccountLevel[];

  // Loader
  chartOfAccountpopupLoader = false;

  constructor(
    private codeService: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) {
    // Form Initialize
    this.allFormInitialize();
    this.windows = window;
  }

  ngOnInit() {
    this.formToggle = 1;

    // AccountType
    this.getAccountType();
    this.getChartOfAccountsList();
    this.commonService.getOfficeId().subscribe(data => {
      this.getChartOfAccountsList();
    });
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.ChartOfAccount
    );
  }

  // Initialize forms
  allFormInitialize() {
    // Form Main Level Account
    this.fMainLevelAccount = {
      ParentID: null,
      AccountLevelId: 1,
      AccountTypeName: 'Main Level Accounts',
      AccountNote: 1,
      AccountName: null,
      AccountTypeId: null,
      Show: true,
      MDCode: null
    };

    // Form Control Level Account
    this.fControlLevelAccount = {
      ParentID: null,
      AccountLevelId: 2,
      AccountTypeName: 'Control Level Accounts',
      AccountNote: 2,
      MainLevel: 0,
      AccountName: null,
      AccountTypeId: null,
      Show: true,
      MDCode: null,
      AccountCode: null,
      AccountCodePref: null
    };

    // Form Sub Level Account
    this.fSubLevelAccount = {
      ParentID: null,
      AccountLevelId: 3,
      AccountTypeName: 'Sub Level Accounts',
      AccountNote: 3,
      ControlLevel: 0,
      AccountName: null,
      AccountTypeId: null,
      Show: true,
      MDCode: null,
      AccountCode: null,
      AccountCodePref: null
    };

    // Form Input Level Account
    this.fInputLevelAccount = {
      ParentID: null,
      AccountLevelId: 4,
      AccountTypeName: 'Input Level Accounts',
      AccountNote: 4,
      SubLevel: 0,
      AccountName: null,
      AccountTypeId: null,
      Show: true,
      MDCode: null,
      AccountCode: null,
      AccountCodePref: null
    };
  }

  //#region Account Type Dropdown
  getAccountType() {
    this.codeService
      .getAccountType(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAllAccoutnType
      )
      .subscribe(
        data => {
          this.accountTypeDropdown = [];

          if (data != null) {
            if (data.data.AccountTypeList != null) {
              if (data.data.AccountTypeList.length > 0) {
                data.data.AccountTypeList.forEach(element => {
                  this.accountTypeDropdown.push(element);
                });
              }
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

  //#region Get All List for Tree
  getChartOfAccountsList() {
    this.codeService
      .getMainLevelAccounts(
        this.setting.getBaseUrl() +
          GLOBAL.API_ChartOfAccount_GetAllChartAccountDetail
      )
      .subscribe(
        data => {
          this.chartOfAccountsData = [];
          this.mainLevelArr = [];
          this.controlLevelArr = [];
          this.subLevelArr = [];
          let counter = 1;
          data != null || data !== undefined
            ? data.data.ChartAccountList.forEach(element => {
                // TODO: To start Tree from Zero
                if (element.AccountLevelId === 1) {
                  this.chartOfAccountsData.push({
                    ID: counter,
                    ParentID: 0,
                    AccountName: element.AccountName,
                    AccountCode: element.ChartOfAccountNewId,
                    AccountTypeId: element.AccountTypeId,
                    AccountLevelId: element.AccountLevelId - 1,
                    ChartOfAccountCode: element.ChartOfAccountNewCode
                  });
                  if (element.AccountLevelId === 1) {
                    this.mainLevelArr.push({
                      AccountCode: element.ChartOfAccountNewId,
                      AccountName: element.AccountName,
                      ChartOfAccountCode: element.ChartOfAccountNewCode
                    });
                  }
                } else {
                  this.chartOfAccountsData.push({
                    ID: counter,
                    ParentID: element.ParentID,
                    AccountName: element.AccountName,
                    AccountCode: element.ChartOfAccountNewId,
                    AccountTypeId: element.AccountTypeId,
                    AccountLevelId: element.AccountLevelId,
                    ChartOfAccountCode: element.ChartOfAccountNewCode
                  });

                  if (element.AccountLevelId === 2) {
                    this.controlLevelArr.push({
                      AccountCode: element.ChartOfAccountNewId,
                      AccountName: element.AccountName,
                      ChartOfAccountCode: element.ChartOfAccountNewCode
                    });
                  }
                  if (element.AccountLevelId === 3) {
                    this.subLevelArr.push({
                      AccountCode: element.ChartOfAccountNewId,
                      AccountName: element.AccountName,
                      ChartOfAccountCode: element.ChartOfAccountNewCode
                    });
                  }
                }

                counter++;
              })
            // tslint:disable-next-line:no-unused-expression
            : null;
          this.commonService.setLoader(false);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.commonService.setLoader(false);
        }
      );
  }
  //#endregion

  //#region Main Account Level Filter in Dropdown
  accountLevelSelectedValue(event: any) {
    // this.toastr.success(event.value.AccountLevelName);
    this.formToggle = event.value;
  }
  //#endregion

  //#region Add Main, Control, Sub, Input level Account ----
  onAddAccounts(data: any) {
    this.showChartOfAccountpopupLoader();
    // Note: ChartOfAccountCode = AccountCodePref + AccountCode
    const dataModel = {
      ChartOfAccountCode:
        data.AccountCodePref != null && data.AccountCode != null
          // tslint:disable-next-line:radix
          ? parseInt(
              data.AccountCodePref.toString() + data.AccountCode.toString()
            )
          : data.AccountCodePref == null && data.AccountCode != null
          ? data.AccountCode
          : 0,
      AccountLevelId: data.AccountLevelId,
      AccountName: data.AccountName,
      AccountNote: data.AccountNote,
      AccountTypeId: data.AccountTypeId,
      AccountTypeName: data.AccountTypeName,
      MDCode: data.MDCode,
      ParentID: data.AccountCodePref,
      Show: data.Show,
      SubLevel: data.SubLevel
    };

    if (data.MainLevel) {
      dataModel.ParentID = data.MainLevel;
    } else if (data.ControlLevel) {
      dataModel.ParentID = data.ControlLevel;
    } else if (data.SubLevel) {
      dataModel.ParentID = data.SubLevel;
    }

    this.codeService
      .AddMainLevelAccount(
        this.setting.getBaseUrl() +
          GLOBAL.API_ChartOfAccount_AddChartAccountDetail,
        dataModel
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Account Added Successfully !');
            this.cancelChartOfAccount();
            this.getChartOfAccountsList();
          } else if (res.StatusCode === 520) {
            this.toastr.warning(res.Message);
          } else {
            this.toastr.warning(res.Message);
          }

          this.hideChartOfAccountpopupLoader();
        },
        error => {
          this.getChartOfAccountsList();
          this.cancelChartOfAccount();
          this.hideChartOfAccountpopupLoader();
        }
      );
  }
  //#endregion

  //#region Update Chart of Account Detail
  EditChartOfAccount(model: any) {
    // merge old data and new data
    const value = Object.assign(model.oldData, model.newData);
    this.codeService
      .EditChartOfAccountDetail(
        this.setting.getBaseUrl() +
          GLOBAL.API_ChartOfAccount_EditChartAccountDetail,
        value
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully !');
          }
          this.getChartOfAccountsList();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
            this.getChartOfAccountsList();
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
            this.getChartOfAccountsList();
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
            this.getChartOfAccountsList();
          }
          this.getChartOfAccountsList();
        }
      );
  }
  //#endregion

  //#region on add Popup show
  onShowing() {
    this.formToggle = 1;
    this.selectValue = 1;
    this.allFormInitialize();

    // Select Account Level (Global Filter)
    this.accountLevelDropdown = this.codeService.getAccountLevels();

    // AccountType
    this.getAccountType();
    this.getChartOfAccountsList();
  }
  //#endregion

  //#region ""onControlLevelFieldDataChanged"
  onControlLevelFieldDataChanged(e: any) {
    if (e != null) {
      if (e.dataField === 'MainLevel') {
        e.value != null
          ? (this.fControlLevelAccount.AccountCodePref = this.mainLevelArr.filter(
              x => x.AccountCode === e.value
            )[0].ChartOfAccountCode)
          // tslint:disable-next-line:no-unused-expression
          : null;
      }
    }
  }
  //#endregion

  //#region "onSubLevelFieldDataChanged"
  onSubLevelFieldDataChanged(e: any) {
    if (e != null) {
      if (e.dataField === 'ControlLevel') {
        e.value != null
          ? (this.fSubLevelAccount.AccountCodePref = this.controlLevelArr.filter(
              x => x.AccountCode === e.value
            )[0].ChartOfAccountCode)
          // tslint:disable-next-line:no-unused-expression
          : null;
      }
    }
  }
  //#endregion

  //#region "onInputLevelFieldDataChanged"
  onInputLevelFieldDataChanged(e: any) {
    if (e != null) {
      if (e.dataField === 'SubLevel') {
        e.value != null
          ? (this.fInputLevelAccount.AccountCodePref = this.subLevelArr.filter(
              x => x.AccountCode === e.value
            )[0].ChartOfAccountCode)
          // tslint:disable-next-line:no-unused-expression
          : null;
      }
    }
  }
  //#endregion

  // EVENT: On Add Popup called
  addChartOfAccount() {
    this.popupVisibleAddChartOfAccount = true;
  }

  // EVENT: On Add Popup cancelled
  cancelChartOfAccount() {
    this.popupVisibleAddChartOfAccount = false;
  }

  //#region "show / hide"
  showChartOfAccountpopupLoader() {
    this.chartOfAccountpopupLoader = true;
  }
  hideChartOfAccountpopupLoader() {
    this.chartOfAccountpopupLoader = false;
  }
  //#endregion
}
