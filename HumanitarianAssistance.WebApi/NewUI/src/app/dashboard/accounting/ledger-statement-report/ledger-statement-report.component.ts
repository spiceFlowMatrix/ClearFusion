import { Component, OnInit } from '@angular/core';
import {ReportService} from 'src/app/dashboard/accounting/report-services/report.service';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { of, Observable } from 'rxjs';
import { IDropDownModel } from '../report-services/report-models';
import { map } from 'rxjs/operators';

interface LedgerFilter {
  CurrencyId: number;
  RecordType: number;
  FromDate: any;
  ToDate: any;
  accountLists: any;
  OfficeIdList: any;
}

@Component({
  selector: 'app-ledger-statement-report',
  templateUrl: './ledger-statement-report.component.html',
  styleUrls: ['./ledger-statement-report.component.scss']
})
export class LedgerStatementReportComponent implements OnInit {

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

  ledgerFilterForm: FormGroup;
  menuList: IMenuList[] = [];
  ledgerListHeaders$ = of(['Transaction Date', 'Voucher', 'Description', 'Currency', 'Account No', 'Account Name',
  'Debit', 'Credit']);
  currencyId$: Observable<IDropDownModel[]>;
  recordType$: Observable<IDropDownModel[]>;
  ledgerFilterList$: Observable<ILedgerList[]>;
  // Report
  viewPdfFlag = true;
  debitSumForReport = 0.0;
  creditSumForReport = 0.0;
  balanceSumForReport = 0.0;
  //#endregion
  constructor(
    private accountservice: ReportService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private globalService: GlobalSharedService
  ) {
    this.FromDate = '01/01/' + new Date().getFullYear();
    const currentDate = new Date().getDate();
    const currentMonth = new Date().getMonth() + 1;
    const currentYear = new Date().getFullYear();
    this.recordTypeDropdown = [
      {
        value: 1,
        name: 'Single'
      },
      {
        value: 2,
        name: 'Consolidate'
      }
    ];
    this.recordType$ = of(this.recordTypeDropdown);

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
    this.globalService.setMenuHeaderName('Ledger');
    this.globalService.setMenuList(this.menuList);
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
    this.ledgerFilterForm = this.fb.group({
      'OfficesList': ['', Validators.required],
      'CurrencyId': ['', Validators.required],
      'RecordType': ['', Validators.required],
      'accountLists': ['', Validators.required],
      'date': [{'begin': new Date(new Date().getFullYear(), new Date().getMonth(), 1), 'end': new Date()}]
    });
  }

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

  getCurrencyCodeListResponse() {
    return this.accountservice.GetAllCurrencyCodeList();
  }
  //#endregion

  //#region  "Get Office Code in Add, Edit Dropdown"
  getOfficeCodeListResponse() {
    return this.accountservice.GetAllOfficeCodeList();
  }
  //#endregion

  //#region  "GetAccountDetails"
  GetAccountDetailsResponse() {
    return this.accountservice.GetAccountDetails();
  }
  //#endregion

  getCurrencyCodeList(response: any) {
    console.log(response);
    if (response.StatusCode === 200 && response.data.CurrencyList != null) {
      this.currencyDropdown = [];
      response.data.CurrencyList.forEach(element => {
        this.currencyDropdown.push(element);
      });
      this.currencyId$ = of(this.currencyDropdown.map(y => {
        return {
          value: y.CurrencyId,
          name: y.CurrencyCode + '-' + y.CurrencyName
        };
      }));
      this.selectedCurrency = this.currencyDropdown[0].CurrencyId;
    } else {
      this.toastr.error(response.Message);
    }
  }

  getOfficeCodeList(response: any) {
    console.log(response);

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
    console.log(response);

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
    debugger;
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
        OfficeIdList: value.OfficesList
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
            this.ledgerFilterList$ = of(this.dataSource).pipe(
              map(r => r.map(v => ({
                Transaction_Date: new Date(v.TransactionDate).getDate() + '/' + new Date(v.TransactionDate).getMonth() +
                '/' + new Date(v.TransactionDate).getFullYear(),
                Voucher: v.VoucherReferenceNo,
                Description: v.Description,
                Currency: v.CurrencyName,
                Account_Code: v.ChartOfAccountNewCode,
                Account_Name: v.ChartAccountName,
                DebitAmount: v.DebitAmount,
                CreditAmount: v.CreditAmount
               }) as ILedgerList))
            );
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
interface ILedgerList {
  Transaction_Date;
  Voucher;
  Description;
  Currency;
  Account_Code;
  Account_Name;
  DebitAmount;
  CreditAmount;
}
