import { Component, OnInit, HostListener } from '@angular/core';
import { ReportService } from '../report-services/report.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { Observable, of } from 'rxjs';
import { IDropDownModel } from '../report-services/report-models';
import { map } from 'rxjs/operators';
import { IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { GLOBAL } from 'src/app/shared/global';

class TrialBalanceFilter {
  public static trialBalanceFilter: TrialBalanceFilterModel;
}

@Component({
  selector: 'app-trial-balance-report',
  templateUrl: './trial-balance-report.component.html',
  styleUrls: ['./trial-balance-report.component.scss']
})

export class TrialBalanceReportComponent implements OnInit {
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
  menuList: IMenuList[] = [];
  // Report
  viewPdfFlag = true;
  debitSumForReport = 0.0;
  creditSumForReport = 0.0;
  balanceSumForReport = 0.0;

  currencyId$: Observable<IDropDownModel[]>;
  recordType$: Observable<IDropDownModel[]>;
  trialListHeaders$ = of(['Account', 'Account Name', 'Description', 'Currency',
  'Debit', 'Credit']);
  trialbalFilterList$: Observable<ITrialList[]>;
  trailbalFilterForm: FormGroup;

  totalCount = 0;
  pageSize = 10;
  pageIndex: 0;
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;

  constructor(
    private accountservice: ReportService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private globalService: GlobalSharedService
  ) {
    this.FromDate = '01/01/' + new Date().getFullYear();
    this.trailFilters = {
      CurrencyId: 1,
      OfficesList: [],
      RecordType: 1,
      fromdate: this.FromDate,
      // todate: new Date(),
      accountLists: [],
      date: {'begin': new Date(new Date().getFullYear(), 0, 1), 'end': new Date()}
    };
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
    this.getScreenSize();
  }

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

  ngOnInit() {
    this.getCurrencyCodeList();
    this.getOfficeCodeList();
    this.GetAccountDetails();

    this.globalService.setMenuHeaderName('Trial Balance');
    this.globalService.setMenuList(this.menuList);
    this.trailbalFilterForm = this.fb.group({
      'OfficesList': ['', Validators.required],
      'CurrencyId': ['', Validators.required],
      'RecordType': [1, Validators.required],
      'accountLists': ['', Validators.required],
      'date': [{'begin': new Date(new Date().getFullYear(), 0, 1), 'end': new Date()}]
    });
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
          new Date(value.date.begin).getFullYear(),
          new Date(value.date.begin).getMonth(),
          new Date(value.date.begin).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        todate: new Date(
          new Date(value.date.end).getFullYear(),
          new Date(value.date.end).getMonth(),
          new Date(value.date.end).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
      };
      this.GetAllTrailBalance(TrialBalanceFilter.trialBalanceFilter);
    }
  }
  pageEvent(e) {
    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;
    // this.voucherFilter.totalCount =  e.length;

    // this.getVoucherList();
  }
  //#endregion
  onOpenedAccountMultiSelectChange(event: IOpenedChange) {
    this.trailbalFilterForm.controls['accountLists'].setValue(event.Value);
  }
  onOpenedOfficeMultiSelectChange(event: IOpenedChange) {
    this.trailbalFilterForm.controls['OfficesList'].setValue(event.Value);
  }
  //#region  "getCurrencyCodeList"
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
            this.currencyId$ = of(this.currencyDropdown.map(y => {
              return {
                value: y.CurrencyId,
                name: y.CurrencyCode + '-' + y.CurrencyName
              };
            }));
            this.selectedCurrency = this.currencyDropdown[0].CurrencyId;
            this.trailbalFilterForm.controls['CurrencyId'].setValue(this.selectedCurrency);
            this.intialFlagValue += 1;

          }
        },
        error => {}
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

            // const allOffices = [];
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

            officeIds.forEach(x => {
              const officeData = this.officeDropdown.filter(
                // tslint:disable-next-line:radix
                e => e.OfficeId === parseInt(x)
              )[0];
              // this.officeDropdown.push(officeData);
            });

            this.selectedOffices = [];
            this.officeDropdown.forEach(x => {
              // tslint:disable-next-line:radix
              this.selectedOffices.push(x.Id);
            });
            this.trailbalFilterForm.controls['OfficesList'].setValue(this.selectedOffices);
            this.intialFlagValue += 1;

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

  //#region  "GetAccountDetails"
  GetAccountDetails() {
    this.accountservice
      .GetAccountDetails()
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
                Id: element.AccountCode,
                Name: element.AccountName
              });
            });

            data.data.AccountDetailList.forEach(e => {
              this.selectedAccounts.push(e.AccountCode);
            }
            );
            this.trailbalFilterForm.controls['accountLists'].setValue(this.selectedAccounts);

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

  get AccountIds() {
    return this.trailbalFilterForm.get('accountLists').value;
  }
  get OfficeIds() {
    return this.trailbalFilterForm.get('OfficesList').value;
  }
  //#endregion
  //#region  "GetAllTrailBalance"
  GetAllTrailBalance(trialBalanceFilter: any) {
    this.showHideTrialBalanceLoader(true);

    this.accountservice
      .GetAllTrailBalance(
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
            this.trialbalFilterList$ = of(this.trailBalanceData).pipe(
              map(r => r.map(v => ({
                Account: v.ChartOfAccountNewCode,
                AccountName: v.ChartOfAccountNewCode + '-' + v.AccountName,
                Description: v.Description,
                Currency: v.CurrencyName,
                Debit: v.DebitAmount,
                Credit: v.CreditAmount
               }) as ITrialList)));

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
          console.log(this.trialbalFilterList$);
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
  ExportPdf(value) {
    TrialBalanceFilter.trialBalanceFilter = {
      CurrencyId: value.CurrencyId,
      accountLists: value.accountLists,
      RecordType: value.RecordType,
      OfficesList: value.OfficesList,
      fromdate: new Date(
        new Date(value.date.begin).getFullYear(),
        new Date(value.date.begin).getMonth(),
        new Date(value.date.begin).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      todate: new Date(
        new Date(value.date.end).getFullYear(),
        new Date(value.date.end).getMonth(),
        new Date(value.date.end).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
    };
    this.accountservice.onExportTrialBalancePdf(TrialBalanceFilter.trialBalanceFilter);
  }
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
  date;
}

class TrialBalanceFilterModel {
  CurrencyId: number;
  accountLists: any;
  RecordType: number;
  OfficesList: any;
  fromdate: any;
  todate: any;
}

interface ITrialList {
  Account;
  AccountName;
  Description;
  Currency;
  Debit;
  Credit;
}
