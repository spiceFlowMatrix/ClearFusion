import { Component, OnInit } from '@angular/core';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../../code/code.service';
import { AccountsService } from '../accounts.service';
import CustomStore from 'devextreme/data/custom_store';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-exchange-gain-loss-transaction',
  templateUrl: './exchange-gain-loss-transaction.component.html',
  styleUrls: ['./exchange-gain-loss-transaction.component.css']
})
export class ExchangeGainLossTransactionComponent implements OnInit {
  exchangeGainLossFilter: ExchangeGainLossFilterModel;
  // exGainLossFilter: any;
  accountDropdown: any[];
  levelFourAccounts: any[];
  voucherDetailsForm: VoucherDetailsModel;
  voucherDetails: VoucherDetailsModel[];
  voucherList: any[];
  officeDropdown: any[];
  journalDropdown: any[];
  allOfficeDropdown: any[];
  currencyDropdown: any[];
  voucherDropdown: any;
  financialYearArr: any;
  voucherTypeArr: any[];
  transactionTypeDropdown: any[];
  deleteVoucherPopupVisible: boolean;
  exchangeGainLossDateRange: any[];
  exchangeGainOrLossDataSource: any = {};

  gainLossAmountFlag: boolean;

  voucherFieldFlag = true;
  totalExchangeGainLoss: any;
  // dataSource: any = {};

  addEditVoucherFormPopupVisible = false;
  addEditVoucherFormPopupLoading = false;

  // Loader
  exchangeGainLossLoader = false;
  //#endregion

  store: CustomStore;

  constructor(
    private accountservice: AccountsService,
    private codeservice: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) {
    // VoucherType Static array
    this.voucherTypeArr = [
      { VoucherTypeId: 1, VoucherTypeName: 'Journal' },
      { VoucherTypeId: 2, VoucherTypeName: 'Adjustment' }
    ];
    this.transactionTypeDropdown = [
      { TransactionTypeId: 1, TransactionTypeName: 'Credit' },
      { TransactionTypeId: 2, TransactionTypeName: 'Debit' }
    ];
  }

  ngOnInit() {
    this.initializeForm();
    this.getAccountDetails();
    this.getOfficeCodeList();
    this.getJournalDropdownList();
    this.getCurrencyCodeList();
    this.getFinancialYear();
    this.getLevelFourAccountDetails();
  }

  initializeForm() {
    this.exchangeGainLossFilter = {
      OfficeId: null,
      JournalId: null,
      ProjectList: null,
      ToDate: null,
      FromDate: null,
      DateOfComparison: null,
      ComparisonCurrencyId: null,
      TransactionType: null
    };

    this.voucherDetailsForm = {
      BudgetLineId: null,
      ChequeNo: null,
      CurrencyCode: null,
      CurrencyId: null,
      Description: null,
      FinancialYearId: null,
      FinancialYearName: null,
      JournalCode: null,
      JournalName: null,
      OfficeId: null,
      OfficeName: null,
      ProjectId: null,
      ReferenceNo: null,
      VoucherDate: null,
      VoucherNo: null,
      VoucherTypeId: null,
      AccountName: null,
      AccountCode: null,
      ExchangeGainLossAmount: null,
      AccountCodeCredit: null,
      AccountCodeDebit: null
    };
  }

  //#region "getExchangeGainOrLossAmount"
  getExchangeGainOrLossTransactionAmount(model) {
    this.showExchangeGainLossLoader();
    this.accountservice
      .GetAllDetailsByModel(
        this.setting.getBaseUrl() +
        GLOBAL.API_Accounting_GetExchangeGainOrLossTransactionAmount,
        model
      )
      .subscribe(
        data => {
          this.exchangeGainOrLossDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.ExchangeGainOrLossModel != null
          ) {
            // this.exchangeGainOrLossDataSource = { data: data.data.ExchangeGainOrLossModel, totalCount: 82 };
            this.exchangeGainOrLossDataSource =
              data.data.ExchangeGainOrLossModel;
            // this.totalExchangeGainLoss = data.data.ExchangeGainOrLossModel.Total;
          } else if (data.StatusCode === 400) {
            this.toastr.warning(data.Message);
          }
          this.hideExchangeGainLossLoader();
        },
        error => {
          this.hideExchangeGainLossLoader();
        }
      );
  }
  //#endregion

  //#region "getAccountDetails"
  getAccountDetails() {
    this.showExchangeGainLossLoader();
    this.accountservice
      .GetAccountDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAccountDetails
      )
      .subscribe(
        data => {
          this.accountDropdown = [];

          if (data.StatusCode === 200) {
            if (data.data.AccountDetailList != null) {
              data.data.AccountDetailList.forEach(element => {
                this.accountDropdown.push({
                  AccountCode: element.AccountCode,
                  AccountName: element.AccountName
                });
              });

              // Sort in Ascending order
              this.commonService.sortDropdown(
                this.accountDropdown,
                'AccountName'
              );
            }
          }
          this.hideExchangeGainLossLoader();
        },
        error => {
          this.hideExchangeGainLossLoader();
        }
      );
  }
  //#endregion

  //#region "getLevelFourAccountDetails"
  getLevelFourAccountDetails() {
    this.showExchangeGainLossLoader();
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
          this.hideExchangeGainLossLoader();
        },
        error => {
          this.hideExchangeGainLossLoader();
        }
      );
  }
  //#endregion

  //#region "getOfficeCodeList"
  getOfficeCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          if (data.data.OfficeDetailsList != null) {
            this.officeDropdown = [];
            this.allOfficeDropdown = [];

            const officeIds: any[] =
              localStorage.getItem('ALLOFFICES') != null
                ? localStorage.getItem('ALLOFFICES').split(',')
                : null;

            data.data.OfficeDetailsList.forEach(element => {
              this.allOfficeDropdown.push(element);
            });

            // //fetch only allowed office
            officeIds.forEach(x => {
              const officeData = this.allOfficeDropdown.filter(
                // tslint:disable-next-line:radix
                e => e.OfficeId === parseInt(x)
              )[0];
              this.officeDropdown.push(officeData);
            });

            // sort in Asc
            this.officeDropdown = this.commonService.sortDropdown(
              this.officeDropdown,
              'OfficeName'
            );
          }
        },
        error => { }
      );
  }
  //#endregion

  //#region "getJournalDropdownList"
  getJournalDropdownList() {
    // this.journalCodeListLoading = true;
    this.codeservice
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

              this.commonService.sortDropdown(
                this.journalDropdown,
                'JournalName'
              );
            }
          }
        },
        error => { }
      );
  }
  //#endregion

  //#region "getVoucherDropdownListbyJournalNo"
  getVoucherDropdownListbyJournalNo(
    JournalVoucherFilters: JournalVoucherFilter
  ) {
    this.showExchangeGainLossLoader();

    this.codeservice
      .GetDetailByPassingModel(
        this.setting.getBaseUrl() +
        GLOBAL.API_Accounting_GetAllVoucherByJouranlId,
        JournalVoucherFilters
      )
      .subscribe(
        data => {
          this.voucherDropdown = [];

          if (data.StatusCode === 200 && data.data.VouchersList != null) {
            if (data.data.VouchersList.length > 0) {
              data.data.VouchersList.forEach(element => {
                this.voucherDropdown.push({
                  VoucherId: element.VoucherNo,
                  VoucherName: element.ReferenceNo
                });
              });
              this.commonService.sortDropdown(
                this.voucherDropdown,
                'VoucherName'
              );

              // this.selectedJournal = [];
              // this.selectedJournal.push(this.journalDropdown[0].JournalCode);
            }
          }
          this.hideExchangeGainLossLoader();
        },
        error => {
          this.hideExchangeGainLossLoader();
        }
      );
  }
  //#endregion

  //#region "getCurrencyCodeList"
  getCurrencyCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyDropdown = [];
          if (data.data.CurrencyList != null) {
            if (data.data.CurrencyList.length > 0) {
              data.data.CurrencyList.forEach(element => {
                this.currencyDropdown.push(element);
              });

              // sort in Asc
              this.commonService.sortDropdown(
                this.currencyDropdown,
                'CurrencyName'
              );
            }
          }
        },
        error => { }
      );
  }
  //#endregion

  //#region "onApplyingFilter"
  onApplyingFilter(data) {
    if (this.exchangeGainLossDateRange != null) {
      const exchangeGainLossFilter = {
        // tslint:disable-next-line:radix
        OfficeId: parseInt(localStorage.getItem('OFFICEID')),
        JournalId: data.JournalId,
        ProjectList: data.ProjectList,
        AccountList: data.AccountList,
        FromDate: new Date(
          new Date(this.exchangeGainLossDateRange[0]).getFullYear(),
          new Date(this.exchangeGainLossDateRange[0]).getMonth(),
          new Date(this.exchangeGainLossDateRange[0]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        ToDate: new Date(
          new Date(this.exchangeGainLossDateRange[1]).getFullYear(),
          new Date(this.exchangeGainLossDateRange[1]).getMonth(),
          new Date(this.exchangeGainLossDateRange[1]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        DateOfComparison: data.DateOfComparison,
        ComparisonCurrencyId: data.ComparisonCurrencyId,
        TransactionType: data.TransactionType
      };

      this.getExchangeGainOrLossTransactionAmount(exchangeGainLossFilter);
    }
  }
  //#endregion

  //#region AddVoucher
  AddVoucher(data: VoucherDetailsModel) {
    this.showAddEditVoucherFormPopupLoading();

    const voucherData: VoucherDetailsModel = {
      VoucherNo: 0,
      CurrencyId: data.CurrencyId,
      OfficeId: data.OfficeId,
      VoucherDate: new Date(
        new Date(data.VoucherDate).getFullYear(),
        new Date(data.VoucherDate).getMonth(),
        new Date(data.VoucherDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      ChequeNo: data.ChequeNo,
      JournalCode: data.JournalCode,
      VoucherTypeId: data.VoucherTypeId,
      Description: data.Description,
      ProjectId: data.ProjectId,
      BudgetLineId: data.BudgetLineId,
      FinancialYearId: data.FinancialYearId,
      AccountName: data.AccountName,
      AccountCode: data.AccountCode,
      ExchangeGainLossAmount: data.ExchangeGainLossAmount,
      CurrencyCode: '',
      FinancialYearName: '',
      JournalName: '',
      OfficeName: '',
      ReferenceNo: '',
      AccountCodeCredit: data.AccountCodeCredit,
      AccountCodeDebit: data.AccountCodeDebit
    };

    this.accountservice
      .AddVoucher(
        this.setting.getBaseUrl() +
        GLOBAL.API_Accounting_AddExchangeGainLossVoucher,
        voucherData
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success(res.LoggerDetailsModel.LoggedDetail);
            if (res.LoggerDetailsModel != null) {
            }
            this.fireNotification(res.LoggerDetailsModel);
          }

          // this.voucherFilterForm.Skip=0;//
          this.voucherDetails = [];
          this.hideAddEditVoucherForm();
          this.hideAddEditVoucherFormPopupLoading();
        },
        error => {
          this.hideAddEditVoucherForm();
          this.hideAddEditVoucherFormPopupLoading();
        }
      );
  }
  //#endregion

  //#region "onFieldDataChanged"
  onFieldDataChanged(e) {
    if (e != null) {
      if (e.dataField != null) {
        if (
          e.dataField === 'JournalId' &&
          e.value != null &&
          this.exchangeGainLossDateRange != null
        ) {
          const journalVoucherFilters: JournalVoucherFilter = {
            OfficeId: this.exchangeGainLossFilter.OfficeId,
            JournalNo: e.value,
            FromDate: new Date(
              new Date(this.exchangeGainLossDateRange[0]).getFullYear(),
              new Date(this.exchangeGainLossDateRange[0]).getMonth(),
              new Date(this.exchangeGainLossDateRange[0]).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
            ToDate: new Date(
              new Date(this.exchangeGainLossDateRange[1]).getFullYear(),
              new Date(this.exchangeGainLossDateRange[1]).getMonth(),
              new Date(this.exchangeGainLossDateRange[1]).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            )
          };

          this.getVoucherDropdownListbyJournalNo(journalVoucherFilters);
        }
        if (e.dataField === 'JournalId' && e.value == null) {
          // if journal is unselected then clearing all vouchers loaded for journal
          this.voucherDropdown = null;
        }
      }
    }

    if (
      this.exchangeGainLossDateRange !== undefined ||
      this.exchangeGainLossDateRange != null
    ) {
      if (
        this.exchangeGainLossFilter.OfficeId != null &&
        this.exchangeGainLossFilter.JournalId != null &&
        this.exchangeGainLossDateRange.length > 0
      ) {
        this.voucherFieldFlag = false;
      } else {
        this.voucherFieldFlag = true;
      }
    }
  }
  //#endregion

  //#region "onVoucherFormSubmit"
  onVoucherFormSubmit(data: VoucherDetailsModel) {
    if (data != null) {
      this.AddVoucher(data);
    }
  }
  //#endregion

  //#region getFinancialYear
  getFinancialYear() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllFinancialYearDetail
      )
      .subscribe(
        data => {
          this.financialYearArr = [];
          if (
            data.data.FinancialYearDetailList != null &&
            data.data.FinancialYearDetailList.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.FinancialYearDetailList.forEach(element => {
              this.financialYearArr.push(element);
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
          }
        }
      );
  }
  //#endregion

  onShowAddVoucherForm(data) {
    this.showAddEditVoucherForm(data);
  }

  onShowDeleteVoucherPopUp(data) {
    this.getAllExchangeGainLossVoucherByOfficeId();
  }

  getAllExchangeGainLossVoucherByOfficeId() {
    this.showExchangeGainLossLoader();
    // tslint:disable-next-line:radix
    const OfficeId = parseInt(localStorage.getItem('OFFICEID'));
    this.accountservice
      .GetAllVoucherByOfficeId(
        this.setting.getBaseUrl() +
        GLOBAL.API_Accounting_GetExchangeGainLossVoucherList,
        OfficeId
      )
      .subscribe(
        data => {
          this.voucherList = [];

          if (data.StatusCode === 200) {
            if (data.data.VoucherDetailList != null) {
              data.data.VoucherDetailList.forEach(element => {
                this.voucherList.push({
                  VoucherNo: element.VoucherNo,
                  ReferenceNo: element.ReferenceNo
                });
              });

              // Sort in Ascending order
              this.commonService.sortDropdown(this.voucherList, 'VoucherNo');
            }
          }
          this.hideExchangeGainLossLoader();
        },
        error => {
          this.hideExchangeGainLossLoader();
        }
      );

    this.deleteVoucherPopupVisible = true;
  }

  onDeleteVoucher(model) {
    const userId = localStorage.getItem('UserId');
    const voucherNo = model.VoucherNo;
    this.showExchangeGainLossLoader();
    this.accountservice
      .DeleteVoucher(
        this.setting.getBaseUrl() +
        GLOBAL.API_Accounting_DeleteExchangeGainLossVoucher,
        voucherNo,
        userId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Voucher Deleted Successfully!!!');
            if (data.LoggerDetailsModel != null) {
            }
            this.fireNotification(data.LoggerDetailsModel);
          }
          this.hideDeleteVoucherPopUp();
          this.hideExchangeGainLossLoader();
        },
        error => {
          this.hideDeleteVoucherPopUp();
          this.hideExchangeGainLossLoader();
        }
      );
  }

  showAddEditVoucherForm(data) {
    this.voucherDetailsForm.CurrencyId = data.ComparisonCurrencyId;
    this.gainLossAmountFlag =
      this.accountservice.sumOfListInArray(
        this.exchangeGainOrLossDataSource,
        'GainLossAmount'
      ) < 0
        ? false
        : true;
    this.voucherDetailsForm.ExchangeGainLossAmount = Math.abs(
      this.accountservice.sumOfListInArray(
        this.exchangeGainOrLossDataSource,
        'GainLossAmount'
      )
    );
    // tslint:disable-next-line:radix
    this.voucherDetailsForm.OfficeId = parseInt(
      localStorage.getItem('OFFICEID')
    );
    this.addEditVoucherFormPopupVisible = true;
  }

  hideAddEditVoucherForm() {
    this.addEditVoucherFormPopupVisible = false;
  }

  hideDeleteVoucherPopUp() {
    this.deleteVoucherPopupVisible = false;
  }

  showAddEditVoucherFormPopupLoading() {
    this.addEditVoucherFormPopupLoading = true;
  }
  hideAddEditVoucherFormPopupLoading() {
    this.addEditVoucherFormPopupLoading = false;
  }

  //#region "show / hide"
  showExchangeGainLossLoader() {
    this.exchangeGainLossLoader = true;
  }
  hideExchangeGainLossLoader() {
    this.exchangeGainLossLoader = false;
  }
  //#endregion

  //#region
  fireNotification(model) {
    model.CreatedDate = new Date();
    model.NotificationPath = './accounts/vouchers';

    this.commonService.sendMessage(model);
  }
  //#endregion
}

class ExchangeGainLossFilterModel {
  OfficeId: any;
  JournalId: any;
  ProjectList: any;
  FromDate: any;
  ToDate: any;
  DateOfComparison: any;
  ComparisonCurrencyId: number;
  TransactionType: number;
}
interface JournalVoucherFilter {
  OfficeId: number;
  JournalNo: number;
  FromDate: any;
  ToDate: any;
}

class VoucherDetailsModel {
  BudgetLineId: any;
  ChequeNo: any;
  CurrencyCode: any;
  CurrencyId: any;
  Description: any;
  FinancialYearId: any;
  FinancialYearName: any;
  JournalCode: any;
  JournalName: any;
  OfficeId: any;
  OfficeName: any;
  ProjectId: any;
  ReferenceNo: any;
  VoucherDate: any;
  VoucherNo: any;
  VoucherTypeId: any;
  AccountCode: any;
  ExchangeGainLossAmount: any;
  AccountName: any;
  AccountCodeCredit: any;
  AccountCodeDebit: any;
}
