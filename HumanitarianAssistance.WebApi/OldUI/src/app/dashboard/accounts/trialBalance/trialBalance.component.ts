import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../accounts.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import { CodeService } from '../../code/code.service';

import { Http } from '@angular/http';
import { TrialBalanceFilter } from './trialBalance-filter';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-trialBalance',
  templateUrl: './trialBalance.component.html',
  styleUrls: ['./trialBalance.component.css']
})
export class TrailBalanceComponent implements OnInit {
  //#region
  trailBalanceData: any[];
  trailFilters: TrialFilter;
  trialBalanceLoader = false;
  FromDate: any;
  trialBalanceDateRange: any[];

  accountDropdown: any[];
  currencyDropdown: any[];
  officeDropdown: any[];
  recordTypeDropdown: any[];

  intialFlagValue = 0;
  selectedOffices: any[];
  selectedCurrency: any;
  selectedJournal: any;
  defaultRecordType: any;
  selectedAccounts: any[];

  // Report
  viewPdfFlag = true;
  debitSumForReport = 0.0;
  creditSumForReport = 0.0;
  balanceSumForReport = 0.0;

  //#endregion

  constructor(
    private accountservice: AccountsService,
    private codeservice: CodeService,
    private http: Http,
    private setting: AppSettingsService,
    private commonService: CommonService,
    private toastr: ToastrService
  ) {
    this.FromDate = '01/01/' + new Date().getFullYear();
    this.trailFilters = {
      CurrencyId: 1,
      OfficesList: [],
      RecordType: 1,
      fromdate: this.FromDate,
      // todate: new Date(),
      accountLists: []
    };
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

    this.trialBalanceDateRange = [];
    this.trialBalanceDateRange.push(
      new Date(
        new Date().getFullYear(),
        0,
        1,
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      )
    );
    this.trialBalanceDateRange.push(
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
    this.GetAccountDetails();
  }

  //#region "onApplyingFilter"
  onApplyingFilter(value) {
    if (this.FromDate == null) {
      this.toastr.error('Please Select Date');
    } else {
      TrialBalanceFilter.trialBalanceFilter = {
        CurrencyId: value.CurrencyId,
        accountLists: value.accountLists,
        RecordType: value.RecordType,
        OfficesList: value.OfficesList,
        fromdate: new Date(
          new Date(this.trialBalanceDateRange[0]).getFullYear(),
          new Date(this.trialBalanceDateRange[0]).getMonth(),
          new Date(this.trialBalanceDateRange[0]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        todate: new Date(
          new Date(this.trialBalanceDateRange[1]).getFullYear(),
          new Date(this.trialBalanceDateRange[1]).getMonth(),
          new Date(this.trialBalanceDateRange[1]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        )
      };
      this.GetAllTrailBalance(TrialBalanceFilter.trialBalanceFilter);
    }
  }
  //#endregion

  //#region  "getCurrencyCodeList"
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

            this.intialFlagValue += 1;
          }
        },
        error => {}
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

            this.intialFlagValue += 1;

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
          if (data.StatusCode === 200) {
            data.data.AccountDetailList = data.data.AccountDetailList.filter(
              x => x.AccountLevelId === 4
            );
            data.data.AccountDetailList.forEach(element => {
              this.accountDropdown.push({
                AccountCode: element.AccountCode,
                AccountName: element.AccountName
              });
            });

            data.data.AccountDetailList.forEach(e =>
              this.selectedAccounts.push(e.AccountCode)
            );

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
          } else {
          }
        }
      );
  }
  //#endregion

  //#region  "GetAllTrailBalance"
  GetAllTrailBalance(trialBalanceFilter: any) {
    this.showHideTrialBalanceLoader(true);

    this.accountservice
      .GetAllTrailBalance(
        this.setting.getBaseUrl() +
          GLOBAL.API_AccountReports_GetTrialBalanceReport,
        trialBalanceFilter
      )
      .subscribe(
        data => {
          this.trailBalanceData = [];

          if (
            data.StatusCode === 200 &&
            data.data.TrialBalanceList != null &&
            data.data.TrialBalanceList.length > 0
          ) {
            data.data.TrialBalanceList.forEach(element => {
              this.trailBalanceData.push(element);
            });

            this.debitSumForReport = this.accountservice.sumOfListInArray(
              this.trailBalanceData,
              'DebitAmount'
            );
            this.creditSumForReport = this.accountservice.sumOfListInArray(
              this.trailBalanceData,
              'CreditAmount'
            );
            this.balanceSumForReport =
              this.debitSumForReport - this.creditSumForReport;
          } else if (data.StatusCode === 400) {
            this.toastr.warning(data.Message);
          }
          this.showHideTrialBalanceLoader(false);
        },
        error => {
          this.showHideTrialBalanceLoader(false);
        }
      );
  }
  //#endregion

  //#region "export pdf"
  printTrialBalanceReport(): void {
    let printContents, popupWin;
    printContents = document.getElementById(
      'print-content-trial-balance-report'
    ).innerHTML;
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

  showHideTrialBalanceLoader(flag: boolean) {
    this.trialBalanceLoader = flag;
  }

  showTrialBalancePdf() {
    this.viewPdfFlag = false;
  }

  hideTrialBalancePdf() {
    this.viewPdfFlag = true;
  }
  //#endregion

  customizeValue(data: any) {
    return parseFloat(data.value).toFixed(4);
}
}

interface TrialFilter {
  OfficesList: any;
  CurrencyId: number;
  fromdate: any;
  // todate: any;
  RecordType: number;
  accountLists: any;
}
