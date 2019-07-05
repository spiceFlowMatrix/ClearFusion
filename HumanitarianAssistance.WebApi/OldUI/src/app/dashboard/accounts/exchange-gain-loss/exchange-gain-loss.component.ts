import { Component, OnInit } from '@angular/core';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../../code/code.service';
import { AccountsService } from '../accounts.service';

import CustomStore from 'devextreme/data/custom_store';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-exchange-gain-loss',
  templateUrl: './exchange-gain-loss.component.html',
  styleUrls: ['./exchange-gain-loss.component.css']
})
export class ExchangeGainLossComponent implements OnInit {
  //#region "variable"
  exchangeGainLossFilter: ExchangeGainLossFilterModel;
  accountDropdown: any[];
  selectedAccounts: any[];
  selectedOffices: any[];

  officeDropdown: any[];
  journalDropdown: any[];
  allOfficeDropdown: any[];
  currencyDropdown: any[];
  voucherDropdown: any;

  exchangeGainLossDateRange: any[];
  exchangeGainOrLossDataSource: any = {};

  voucherFieldFlag = true;
  totalExchangeGainLoss: any;

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
  ) {}

  ngOnInit() {
    this.initializeForm();
    this.getAccountDetails();
    this.getOfficeCodeList();
    this.getJournalDropdownList();
    this.getCurrencyCodeList();
  }

  initializeForm() {
    this.exchangeGainLossDateRange = [
      new Date(
        new Date().getFullYear() - 1,
        new Date().getMonth(),
        new Date().getDate()
      ),
      new Date(
        new Date().getFullYear(),
        new Date().getMonth(),
        new Date().getDate()
      )
    ];

    this.exchangeGainLossFilter = {
      OfficeIdList: null,
      JournalId: null,
      VoucherList: null,
      ProjectList: null,
      ToDate: new Date(),
      FromDate: new Date(),
      DateOfComparison: null,
      ComparisonCurrencyId: null,
      AccountList: null
    };
  }

  //#region "getExchangeGainOrLossAmount"
  getExchangeGainOrLossAmount(model) {
    this.showExchangeGainLossLoader();
    this.accountservice
      .GetAllDetailsByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetExchangeGainOrLossAmount,
          model
      )
      .subscribe(
        data => {
          this.exchangeGainOrLossDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.ExchangeGainOrLossModel != null
          ) {
            this.exchangeGainOrLossDataSource =
              data.data.ExchangeGainOrLossModel;
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
              if (data.data.AccountDetailList.length > 0) {
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

                this.selectedAccounts = [];
                this.selectedAccounts.push(this.accountDropdown[0].AccountCode);
              }
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

            // fetch only allowed office
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
            this.exchangeGainLossFilter.OfficeIdList = this.officeDropdown;
            this.exchangeGainLossFilter.DateOfComparison = new Date();

            this.selectedOffices = [];

            officeIds.forEach(x => {
              // tslint:disable-next-line:radix
              this.selectedOffices.push(parseInt(x));
            });
          }
        },
        error => {}
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
            this.exchangeGainLossFilter.JournalId = this.journalDropdown[0].JournalCode;
          }
        },
        error => {}
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
              this.exchangeGainLossFilter.ComparisonCurrencyId = this.currencyDropdown[0].CurrencyId;
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "onApplyingFilter"
  onApplyingFilter(data) {
    if (this.exchangeGainLossDateRange != null) {
      const exchangeGainLossFilter = {
        OfficeId: data.OfficeId,
        JournalId: data.JournalId,
        VoucherList: data.VoucherList,
        ProjectList: data.ProjectList,
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
        AccountList: data.AccountList
      };

      this.getExchangeGainOrLossAmount(exchangeGainLossFilter);
    }
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
            OfficeIdList: this.exchangeGainLossFilter.OfficeIdList,
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
        if (
          e.dataField === 'DateRange' &&
          e.value != null &&
          this.exchangeGainLossDateRange != null &&
          this.exchangeGainLossFilter.JournalId != null
        ) {
          const journalVoucherFilters: JournalVoucherFilter = {
            OfficeIdList: this.exchangeGainLossFilter.OfficeIdList,
            JournalNo: this.exchangeGainLossFilter.JournalId,
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
        if (this.exchangeGainLossFilter.JournalId == null && e.value == null) {
          // if journal is unselected then clearing all vouchers loaded for journal
          this.voucherDropdown = null;
        }
        if (
          e.dataField === 'OfficeIdList' &&
          e.value != null &&
          this.exchangeGainLossDateRange != null &&
          this.exchangeGainLossFilter.JournalId != null
        ) {
          const journalVoucherFilters: JournalVoucherFilter = {
            OfficeIdList: this.exchangeGainLossFilter.OfficeIdList,
            JournalNo: this.exchangeGainLossFilter.JournalId,
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
        if (this.exchangeGainLossFilter.JournalId == null && e.value == null) {
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
        this.exchangeGainLossFilter.OfficeIdList != null &&
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

  //#region "show / hide"
  showExchangeGainLossLoader() {
    this.exchangeGainLossLoader = true;
  }
  hideExchangeGainLossLoader() {
    this.exchangeGainLossLoader = false;
  }

  //#endregion
  onDateRangeChanged(e) {
    if (
      e != null &&
      this.exchangeGainLossDateRange != null &&
      this.exchangeGainLossFilter.JournalId != null
    ) {
      const journalVoucherFilters: JournalVoucherFilter = {
        OfficeIdList: this.exchangeGainLossFilter.OfficeIdList,
        JournalNo: this.exchangeGainLossFilter.JournalId,
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
    if (this.exchangeGainLossFilter.JournalId == null && e.value == null) {
      // if journal is unselected then clearing all vouchers loaded for journal
      this.voucherDropdown = null;
    }
  }
}

class ExchangeGainLossFilterModel {
  OfficeIdList: any;
  JournalId: any;
  VoucherList: any;
  AccountList: any;
  ProjectList: any;
  FromDate: any;
  ToDate: any;
  DateOfComparison: any;
  ComparisonCurrencyId: number;
}

interface JournalVoucherFilter {
  OfficeIdList: any;
  JournalNo: number;
  FromDate: any;
  ToDate: any;
}
