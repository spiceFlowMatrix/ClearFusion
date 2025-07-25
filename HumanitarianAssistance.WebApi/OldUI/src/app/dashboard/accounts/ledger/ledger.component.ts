import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../accounts.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import { CodeService } from '../../code/code.service';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';
import { forkJoin } from 'rxjs/observable/forkJoin';

interface LedgerFilter {
  CurrencyId: number;
  RecordType: number;
  FromDate: any;
  ToDate: any;
  accountLists: any;
  OfficeIdList: any;
}

@Component({
  selector: 'app-ledger',
  templateUrl: './ledger.component.html',
  styleUrls: ['./ledger.component.css']
})
export class LedgerComponent implements OnInit {
  //#region "variables"
  dataSource: any[];
  ledgerReportFinal: any[];

  currencyDropdown: any[];
  officeDropdown: any[];
  recordTypeDropdown: any[];
  accountDropdown: any[];
  ledgerFilter: LedgerFilter;
  selectedOffices: any[];

  OpenningBalance: any = 0.0;
  ClosingBalance: any = 0.0;
  OpenningBalanceType: any = null;
  ClosingBalanceType: any = null;
  FromDate: any;
  currentDateFinal: any;

  ledgerDateRange: any;
  openingBalance = 0;
  closingBalance = 0;

  // loader
  ledgerLoading = false;

  selectedCurrency: any;
  defaultRecordType: any;
  selectedAccounts: any;
  initialFlag = 0;

  // Report
  viewPdfFlag = true;
  debitSumForReport = 0.0;
  creditSumForReport = 0.0;
  balanceSumForReport = 0.0;
  //#endregion

  constructor(
    private accountservice: AccountsService,
    private codeservice: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) {
    this.FromDate = '01/01/' + new Date().getFullYear();
    const currentDate = new Date().getDate();
    const currentMonth = new Date().getMonth() + 1;
    const currentYear = new Date().getFullYear();
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

    this.ledgerDateRange = [];
    this.ledgerDateRange.push(
      new Date(
        new Date().getFullYear(),
        0,
        1,
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      )
    );
    this.ledgerDateRange.push(
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
    this.initForm();

    // get all api response
    const currencyRequest = this.getCurrencyCodeListResponse();
    const officeRequest = this.getOfficeCodeListResponse();
    const accountResponse = this.GetAccountDetailsResponse();

    // perform filter when we get all api responses
    forkJoin([currencyRequest, officeRequest, accountResponse]).subscribe(
      results => {
        this.getCurrencyCodeList(results[0]);
        this.getOfficeCodeList(results[1]);
        this.GetAccountDetails(results[2]);

        const obj = {
          CurrencyId: this.selectedCurrency,
          accountLists: this.selectedAccounts,
          RecordType: this.defaultRecordType,
          FromDate: null,
          ToDate: null,
          OfficeIdList: this.selectedOffices
        };
        this.onApplyingFilter(obj);
      },
      error => {
          this.toastr.error('Internal server error');
      }
    );

  }

  //#region "intiForm"
  initForm() {
    this.ledgerFilter = {
      CurrencyId: 0,
      RecordType: 1,
      accountLists: [],
      FromDate: this.FromDate,
      ToDate: new Date(),
      OfficeIdList: []
    };
  }
  //#endregion

  //#region "getCurrencyCodeList"
  getCurrencyCodeListResponse() {
    return this.codeservice.GetAllCodeList(
      this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
    );
  }
  //#endregion

  //#region  "Get Office Code in Add, Edit Dropdown"
  getOfficeCodeListResponse() {
    return this.codeservice.GetAllCodeList(
      this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
    );
  }
  //#endregion

  //#region  "GetAccountDetails"
  GetAccountDetailsResponse() {
    return this.accountservice.GetAccountDetails(
      this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAccountDetails
    );
  }
  //#endregion

  getCurrencyCodeList(response: any) {
    if (response.StatusCode === 200 && response.data.CurrencyList != null) {
      this.currencyDropdown = [];
      response.data.CurrencyList.forEach(element => {
        this.currencyDropdown.push(element);
      });
      this.selectedCurrency = this.currencyDropdown[0].CurrencyId;
    } else {
      this.toastr.error(response.Message);
    }
  }

  getOfficeCodeList(response: any) {
    if (
      response.StatusCode === 200 &&
      response.data.OfficeDetailsList != null
    ) {
      this.officeDropdown = [];
      response.data.OfficeDetailsList.forEach(element => {
        this.officeDropdown.push(element);
      });

      const officeIds: any[] =
        localStorage.getItem('ALLOFFICES') != null
          ? localStorage.getItem('ALLOFFICES').split(',')
          : null;

      // fetch only allowed office
      officeIds.forEach(x => {
        const officeData = this.officeDropdown.filter(
          // tslint:disable-next-line:radix
          e => e.OfficeId === parseInt(x)
        )[0];
        // this.officeDropdown.push(officeData);
      });

      this.selectedOffices = [];
      officeIds.forEach(x => {
        // tslint:disable-next-line:radix
        this.selectedOffices.push(parseInt(x));
      });
    } else {
      this.toastr.error(response.Message);
    }
  }

  GetAccountDetails(response: any) {
    this.accountDropdown = [];
    this.selectedAccounts = [];
    if (
      response.StatusCode === 200 &&
      response.data.AccountDetailList != null
    ) {
      response.data.AccountDetailList = response.data.AccountDetailList.filter(
        x => x.AccountLevelId === 4
      );
      if (response.data.AccountDetailList.length > 0) {
        response.data.AccountDetailList.forEach(element => {
          this.accountDropdown.push({
            AccountCode: element.AccountCode,
            AccountName: element.AccountName
          });
        });

          this.accountDropdown.map(x => {
            this.selectedAccounts.push(x.AccountCode);
          });

      }
    } else {
      this.toastr.error(response.Message);
    }
  }

  //#region "onApplyingFilter"
  onApplyingFilter(value: any) {
    if (this.ledgerDateRange == null) {
      this.toastr.error('Please Select Date Range');
    } else {
      const ledgerFilter: LedgerFilter = {
        CurrencyId: value.CurrencyId,
        accountLists: value.accountLists,
        RecordType: value.RecordType,
        FromDate: new Date(
          new Date(this.ledgerDateRange[0]).getFullYear(),
          new Date(this.ledgerDateRange[0]).getMonth(),
          new Date(this.ledgerDateRange[0]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        ToDate: new Date(
          new Date(this.ledgerDateRange[1]).getFullYear(),
          new Date(this.ledgerDateRange[1]).getMonth(),
          new Date(this.ledgerDateRange[1]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        OfficeIdList: value.OfficeIdList
      };
      this.GetLedgerDetails(ledgerFilter);
    }
  }
  //#endregion

  //#region "GetLedgerDetails"
  GetLedgerDetails(journalFilter: any) {
    this.showLedgerLoading();
    this.ledgerReportFinal = []; // report
    this.openingBalance = 0;
    this.closingBalance = 0;

    this.accountservice
      .GetAllLedgerDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_AccountReports_GetAllLedgerDetails,
        journalFilter
      )
      .subscribe(
        data => {
          this.dataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.LedgerList != null &&
            data.data.LedgerList.length > 0
          ) {
            if (data.data.AccountOpendingAndClosingBL != null) {
              this.openingBalance =
                data.data.AccountOpendingAndClosingBL.OpeningBalance != null
                  ? data.data.AccountOpendingAndClosingBL.OpeningBalance
                  : null;
              this.closingBalance =
                data.data.AccountOpendingAndClosingBL.ClosingBalance != null
                  ? data.data.AccountOpendingAndClosingBL.ClosingBalance
                  : null;
            }

            data.data.LedgerList.forEach(element => {
              this.dataSource.push(element);
            });

            if (
              data.StatusCode === 200 &&
              data.data.ledgerReportFinal != null &&
              data.data.ledgerReportFinal.length > 0
            ) {
              this.ledgerReportFinal = data.data.ledgerReportFinal;
            }
          } else if (data.StatusCode === 400) {
            this.toastr.warning(data.Message);
          }
          this.hideLedgerLoading();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideLedgerLoading();
        }
      );
  }
  //#endregion

  //#region "export pdf"
  printLedgerReport(): void {
    let printContents, popupWin;
    printContents = document.getElementById('print-content-ledger-report')
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

  //#region "show/hide"
  showLedgerLoading() {
    this.ledgerLoading = true;
  }
  hideLedgerLoading() {
    this.ledgerLoading = false;
  }

  showLedgerPdf() {
    this.viewPdfFlag = false;
  }

  hideLedgerPdf() {
    this.viewPdfFlag = true;
  }
  //#endregion

  customizeValue(data: any) {
    return parseFloat(data.value).toFixed(4);
  }
}
