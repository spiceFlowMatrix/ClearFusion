import { Component, OnInit, ViewChild } from '@angular/core';
import { AccountsService, JournalVoucherModel } from '../accounts.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import { CodeService } from '../../code/code.service';

import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-journal',
  templateUrl: './journal.component.html',
  styleUrls: ['./journal.component.css']
})
export class JournalComponent implements OnInit {
  journalDataSource: any[];
  journalReportDataSource: any[];
  drillDownDataSource: any;
  journalVoucher: JournalVoucherModel[];
  journalDropdown: any[];
  accountLists: any;
  accountDropdown: any[];
  selectedAccounts: any;
  initialFlag = 0;

  currencyDropdown: any[];
  officeDropdown: any[];
  recordTypeDropdown: any[];
  journalFilter: JournalFilterModel;
  // loading: any;
  FromDate: any;
  currentDateFinal: any;
  journalDateRange: any;
  journalDate: any;

  // Report
  viewPdfFlag = true;
  debitSumForReport = '0.00';
  creditSumForReport = '0.00';
  balanceSumForReport = '0.00';

  // Loader
  journalListLoading = false;

  intialFlagValue = 0;
  selectedOffices: any[];
  selectedCurrency: any;
  selectedJournal: any[];
  defaultRecordType: any;

  //#endregion

  constructor(
    private accountservice: AccountsService,
    private codeservice: CodeService,
    private setting: AppSettingsService,
    private commonService: CommonService,
    private toastr: ToastrService,
    private http: Http
  ) {
    this.FromDate = '01/01/' + new Date().getFullYear();

    this.currentDateFinal = new Date();
    this.recordTypeDropdown = [
      {
        Id: 1,
        Name: 'Single'
      },
      {
        Id: 2,
        Name: 'Consolidate'
      }
    ];

    this.defaultRecordType = this.recordTypeDropdown[0].Id;

    this.journalFilter = {
      CurrencyId: null,
      JournalCode: null,
      OfficesList: [],
      RecordType: null,
      FromDate: this.FromDate,
      ToDate: this.currentDateFinal,
      BudgetLine: null,
      Project: null,
      accountLists: null
    };

    // Journal DateRange
    this.journalDateRange = [];
    this.journalDateRange.push(
      new Date(
        new Date().getFullYear(),
        0,
        1,
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      )
    );
    this.journalDateRange.push(
      new Date(
        new Date().getFullYear(),
        new Date().getMonth(),
        new Date().getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      )
    );
  }

  ngOnInit() {
    this.getCurrencyCodeList();
    this.getOfficeCodeList();
    this.getJournalDropdownList();

    this.GetAllJournalDetails();
    this.GetAccountDetails();
  }

  //#region "onApplyingFilter"
  onApplyingFilter(value: JournalFilterModel) {
    if (this.journalDateRange == null) {
      this.toastr.error('Please Select Date Range');
    } else {
      this.journalFilter = {
        CurrencyId: value.CurrencyId,
        JournalCode: value.JournalCode,
        OfficesList: value.OfficesList,
        RecordType:
          value.RecordType == null ? this.defaultRecordType : value.RecordType,
        FromDate: new Date(
          new Date(this.journalDateRange[0]).getFullYear(),
          new Date(this.journalDateRange[0]).getMonth(),
          new Date(this.journalDateRange[0]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        ToDate: new Date(
          new Date(this.journalDateRange[1]).getFullYear(),
          new Date(this.journalDateRange[1]).getMonth(),
          new Date(this.journalDateRange[1]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        BudgetLine: value.BudgetLine,
        Project: value.Project,
        accountLists: value.accountLists
      };
      this.GetAllJournalDetails();
    }
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
            data.data.CurrencyList.forEach(element => {
              this.currencyDropdown.push(element);
            });

            this.selectedCurrency = this.currencyDropdown[0].CurrencyId;

            // Initial Page Loading
            this.intialFlagValue += 1;
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

  //#region  Get Office Code in Add, Edit Dropdown
  getOfficeCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          if (data.data.OfficeDetailsList != null) {
            this.officeDropdown = [];

            const allOffices = [];
            const officeIds: any[] =
              localStorage.getItem('ALLOFFICES') != null
                ? localStorage.getItem('ALLOFFICES').split(',')
                : null;

            data.data.OfficeDetailsList.forEach(element => {
              allOffices.push(element);
            });

            officeIds.forEach(x => {
              const officeData = allOffices.filter(
                // tslint:disable-next-line:radix
                e => e.OfficeId === parseInt(x)
              )[0];
              this.officeDropdown.push(officeData);
            });

            this.selectedOffices = [];
            officeIds.forEach(x => {
              // tslint:disable-next-line:radix
              this.selectedOffices.push(parseInt(x));
            });

            // Initial Page Loading
            this.intialFlagValue += 1;

            // sort in Asc
            this.officeDropdown = this.commonService.sortDropdown(
              this.officeDropdown,
              'OfficeName'
            );
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

  //#region "GetAllJournalDetails"
  GetAllJournalDetails() {
    this.showjournalListLoading();

    this.journalReportDataSource = [];

    this.debitSumForReport = '0.0000';
    this.creditSumForReport = '0.0000';
    this.balanceSumForReport = '0.0000';

    this.accountservice
      .GetAllJournalDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_AccountReports_GetJournalVoucherDetails,
        this.journalFilter
      )
      .subscribe(
        data => {
          this.journalDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.JournalVoucherViewList != null &&
            data.data.JournalVoucherViewList.length > 0
          ) {
            this.journalReportDataSource = data.data.JournalReportList;

            this.debitSumForReport = this.accountservice
              .sumOfListInArray(this.journalReportDataSource, 'DebitAmount')
              .toFixed(4);
            this.creditSumForReport = this.accountservice
              .sumOfListInArray(this.journalReportDataSource, 'CreditAmount')
              .toFixed(4);
            this.balanceSumForReport = (
              parseFloat(this.debitSumForReport) -
              parseFloat(this.creditSumForReport)
            ).toFixed(4);

            data.data.JournalVoucherViewList.forEach(element => {
              this.journalDataSource.push(element);
            });
          }
          // else
          //   this.toastr.error(data.Message);

          this.hidejournalListLoading();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hidejournalListLoading();
        }
      );
  }
  //#endregion

  //#region  Get all Journal Dropdown
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

              this.selectedJournal = [];
              const JournalCodes = [];
              this.journalDropdown.forEach(x => {
                JournalCodes.push(x.JournalCode);
              });

              this.selectedJournal = JournalCodes;

              // Initial Page Loading
              this.intialFlagValue += 1;
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

  //#region  "GetAccountDetails"
  GetAccountDetails() {
    this.accountservice
      .GetAccountDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAccountDetails
      )
      .subscribe(
        data => {
          this.accountDropdown = [];
          this.selectedAccounts = [];
          if (data.StatusCode === 200 && data.data.AccountDetailList != null) {
            data.data.AccountDetailList = data.data.AccountDetailList.filter(
              x => x.AccountLevelId === 4
            );
            if (data.data.AccountDetailList.length > 0) {
              data.data.AccountDetailList.forEach(element => {
                this.accountDropdown.push({
                  AccountCode: element.AccountCode,
                  AccountName: element.AccountName
                });
              });

              const AccountNos = [];

              data.data.AccountDetailList.forEach(x =>
                AccountNos.push(x.AccountCode)
              );

              this.selectedAccounts = AccountNos;
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

  //#region "export pdf"
  printJournalReport(): void {
    let printContents, popupWin;
    printContents = document.getElementById('print-content-journal-report')
      .innerHTML;
    popupWin = window.open('', '_blank', '');
    popupWin.document.open();
    popupWin.document.write(`
    <html>
      <head>
        <title></title>
        <style>
        //........Customized style.......
        </style>
      </head>
      <body onload="window.print();window.close()">${printContents}</body>
    </html>`);
    popupWin.document.close();
  }
  //#endregion

  //#region "Hide/Show"
  showjournalListLoading() {
    this.journalListLoading = true;
  }
  hidejournalListLoading() {
    this.journalListLoading = false;
  }

  showJournalPdf() {
    this.viewPdfFlag = false;
  }

  hideJournalPdf() {
    this.viewPdfFlag = true;
  }
  //#endregion
  customizeValue(data: any) {
    if (data.value != 0)
      return parseFloat(data.value).toFixed(4);
  }
}

class JournalModel {
  JournalCode: any;
  VoucherNo: any;
  TransactionDate: any;
  AccountCode: any;
  TransactionDescription: any;
  CurrencyId: any;
  Project: any;
  BudgetLineDescription: any;
  CreditAmount: any;
  DebitAmount: any;
}

interface JournalFilterModel {
  CurrencyId: number;
  JournalCode: any[];
  OfficesList: any[];
  RecordType: number;
  FromDate: any;
  ToDate: any;
  Project: any;
  BudgetLine: any;
  accountLists: any;
}
