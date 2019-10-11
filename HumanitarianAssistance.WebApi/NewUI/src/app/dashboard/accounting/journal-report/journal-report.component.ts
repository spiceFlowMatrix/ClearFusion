import { Component, OnInit } from '@angular/core';
import {ReportService} from 'src/app/dashboard/accounting/report-services/report.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from 'src/app/shared/global';
import { Observable, of } from 'rxjs';
import { IDropDownModel } from 'src/app/dashboard/accounting/report-services/report-models';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { map } from 'rxjs/operators';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';

@Component({
  selector: 'app-journal-report',
  templateUrl: './journal-report.component.html',
  styleUrls: ['./journal-report.component.scss']
})
export class JournalReportComponent implements OnInit {

  menuList: IMenuList[] = [];
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

  currencyId$: Observable<IDropDownModel[]>;
  recordType$: Observable<IDropDownModel[]>;
  journalListHeaders$ = of(['Transaction Date', 'Voucher Code', 'Transaction Description', 'Currency', 'Account Code', 'Account Name',
  'Debit Amount', 'Credit Amount', 'Project', 'Budget Line']);
  journalFilterList$: Observable<IJournalList[]>;
  journalFilterForm: FormGroup;
  //#endregion
  constructor(
    private accountservice: ReportService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private globalService: GlobalSharedService
  ) {
    this.FromDate = '01/01/' + new Date().getFullYear();

    this.currentDateFinal = new Date();
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

    this.journalFilter = {
      CurrencyId: null,
      JournalCode: null,
      OfficesList: [],
      RecordType: null,
      date: {'begin': new Date(new Date().getFullYear(), new Date().getMonth(), 1), 'end': new Date()},
      BudgetLine: null,
      Project: null,
      accountLists: null,
      FromDate: null ,
      ToDate : null
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
    // Set Menu Header List
    this.globalService.setMenuList(this.menuList);
   }

  ngOnInit() {
    this.globalService.setMenuHeaderName('Journal');
    this.getCurrencyCodeList();
    this.getOfficeCodeList();
    this.getJournalDropdownList();
    this.GetAllJournalDetails();
    this.GetAccountDetails();

    this.journalFilterForm = this.fb.group({
      'OfficesList': ['', Validators.required],
      'CurrencyId': ['', Validators.required],
      'JournalCode': ['', Validators.required],
      'RecordType': [1, Validators.required],
      'accountLists': ['', Validators.required],
      'date': [{'begin': new Date(new Date().getFullYear(), 0, 1), 'end': new Date()}]
    });
  }
 //#region "onApplyingFilter"
 onApplyingFilter(value: JournalFilterModel) {
   debugger;
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
        new Date(value.date.begin).getFullYear(),
        new Date(value.date.begin).getMonth(),
        new Date(value.date.begin).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      ToDate: new Date(
        new Date(value.date.end).getFullYear(),
        new Date(value.date.end).getMonth(),
        new Date(value.date.end).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      BudgetLine: value.BudgetLine,
      Project: value.Project,
      accountLists: value.accountLists,
      date: null
    };
    this.GetAllJournalDetails();
  }
}
//#endregion

onOpenedAccountMultiSelectChange(event: IOpenedChange) {
  this.journalFilterForm.controls['accountLists'].setValue(event.Value);
}
onOpenedOfficeMultiSelectChange(event: IOpenedChange) {
  this.journalFilterForm.controls['OfficesList'].setValue(event.Value);
}
onOpenedJournalMultiSelectChange(event: IOpenedChange) {
  this.journalFilterForm.controls['JournalCode'].setValue(event.Value);
}
get AccountIds() {
  return this.journalFilterForm.get('accountLists').value;
}
get journalIds() {
  return this.journalFilterForm.get('JournalCode').value;
}
get OfficeIds() {
  return this.journalFilterForm.get('OfficesList').value;
}
//#region "getCurrencyCodeList"
getCurrencyCodeList() {
  this.accountservice
    .GetAllCurrencyCodeList()
    .subscribe(
      data => {
        this.currencyDropdown = [];
        if (data.data.CurrencyList != null) {
          data.data.CurrencyList.forEach(element => {
            this.currencyDropdown.push(element);
          });

          this.selectedCurrency = this.currencyDropdown[0].CurrencyId;
          this.journalFilterForm.controls['CurrencyId'].setValue(this.selectedCurrency);
          this.currencyId$ = of(this.currencyDropdown.map(y => {
            return {
              value: y.CurrencyId,
              name: y.CurrencyCode + '-' + y.CurrencyName
            };
          }));
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
  this.accountservice
    .GetAllOfficeCodeList()
    .subscribe(
      data => {
        if (data.data.OfficeDetailsList != null) {
          this.officeDropdown = [];

          //const allOffices = [];
          const officeIds: any[] =
            localStorage.getItem('ALLOFFICES') != null
              ? localStorage.getItem('ALLOFFICES').split(',')
              : null;

          data.data.OfficeDetailsList.forEach(element => {
            this.officeDropdown.push({
              Id: element.OfficeId,
              Name: element.OfficeName
            });
          });

          // officeIds.forEach(x => {
          //   const officeData = allOffices.filter(
          //     // tslint:disable-next-line:radix
          //     e => e.OfficeId === parseInt(x)
          //   )[0];
          //   this.officeDropdown.push(officeData);
          // });

          this.selectedOffices = [];
          this.officeDropdown.forEach(x => {
            // tslint:disable-next-line:radix
            this.selectedOffices.push(x.Id);
          });
          this.journalFilterForm.controls['OfficesList'].setValue(this.selectedOffices);
          // Initial Page Loading
          this.intialFlagValue += 1;

          // sort in Asc
          this.officeDropdown = this.accountservice.sortDropdown(
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
  debugger;
  this.showjournalListLoading();

  this.journalReportDataSource = [];

  this.debitSumForReport = '0.0000';
  this.creditSumForReport = '0.0000';
  this.balanceSumForReport = '0.0000';

  this.accountservice
    .GetAllJournalDetails(
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
          this.journalFilterList$ = of(this.journalDataSource).pipe(
            map(r => r.map(v => ({
              Transaction_Date: new Date(v.TransactionDate).getDate() + '/' + new Date(v.TransactionDate).getMonth() +
              '/' + new Date(v.TransactionDate).getFullYear(),
              Voucher_Code: v.ReferenceNo,
              Transaction_Description: v.TransactionDescription,
              Currency: this.currencyDropdown[this.currencyDropdown.findIndex( x => x.CurrencyId === v.CurrencyId)]['CurrencyName'],
              Account_Code: v.AccountCode,
              Account_Name: v.AccountName,
              DebitAmount: v.DebitAmount,
              CreditAmount: v.CreditAmount,
              Project: v.Project,
              BudgetLine: v.BudgetLine
             }) as IJournalList))
          );
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
  this.accountservice
    .GetAllJournalCodeList()
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
                Id: element.JournalCode,
                Name: element.JournalName
              });
            });

            this.selectedJournal = [];
            const JournalCodes = [];
            this.journalDropdown.forEach(x => {
              JournalCodes.push(x.Id);
            });
            this.selectedJournal = JournalCodes;
            this.journalFilterForm.controls['JournalCode'].setValue(this.selectedJournal);
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
    .GetAccountDetails()
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
                Id: element.AccountCode,
                Name: element.AccountName
              });
            });

            const AccountNos = [];

            this.accountDropdown.forEach(x =>
              this.selectedAccounts.push(x.Id)
            );
            this.journalFilterForm.controls['accountLists'].setValue(this.selectedAccounts);
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
  if (data.value !== 0) {
    return parseFloat(data.value).toFixed(4);
  }
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
OfficesList: any[];
CurrencyId: number;
JournalCode: any[];
RecordType: number;
accountLists: any;
Project: any;
BudgetLine: any;
FromDate: any;
ToDate: any;
date: any;
}

// Journal
export class JournalVoucherModel {
  JournalCode: number;
  AccountCode: number;
  Amount: number;
  TransactionNo: number;
  TransactionDate: string;
  TransactionType: string;
  VoucherNo: number;
}

interface IJournalList {
  Transaction_Date;
  Voucher_Code;
  Transaction_Description;
  Currency;
  Account_Code;
  Account_Name;
  DebitAmount;
  CreditAmount;
  Project;
  BudgetLine;
}
