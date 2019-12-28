import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { ConfigurationFilterComponent } from './configuration-filter/configuration-filter.component';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ExchangeGainLossReportService } from './exchange-gain-loss-report.service';
import { IResponseData } from '../vouchers/models/status-code.model';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { takeUntil } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { IDataSource, IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { IAccountList } from '../gain-loss-report/gain-loss-report.model';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-exchange-gain-loss-report',
  templateUrl: './exchange-gain-loss-report.component.html',
  styleUrls: ['./exchange-gain-loss-report.component.scss']
})
export class ExchangeGainLossReportComponent implements OnInit, OnDestroy {

  showFilters = false;
  type = '';
  transactionFiltersForm: FormGroup;
  projectList: any[];
  journalList: any[];
  officeList: any[];
  AccountIdList: any[];
  gainLossReportList: GainLossReport[] = [];
  GainLossFilter: IGainLossFilter;
  calculatorConfigData: ICalculatorConfig;
  accountDataSource: IDataSource[];
  accountList: IAccountList[] = [];
  /** control for the MatSelect filter keyword multi-selection */
  public accountMultiFilterCtrl: FormControl = new FormControl();
  displayedColumns = ['Checked', 'AccountCode', 'AccountName',
    'BalanceOnOriginalTransactionDates', 'BalanceOnComparisionDate', 'ResultingGainLoss'];

  dataSource = ELEMENT_DATA;
  labelText: string;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  public filteredAccountsMulti: ReplaySubject<any[]> = new ReplaySubject<any[]>(1);
  @ViewChild(ConfigurationFilterComponent) fieldConfig: ConfigurationFilterComponent;

  constructor(private globalSharedService: GlobalSharedService,
    private gainLossReportService: ExchangeGainLossReportService, private fb: FormBuilder,
    private toastr: ToastrService, private commonLoader: CommonLoaderService, ) { }

  ngOnInit() {
    this.globalSharedService.setMenuHeaderName('Currency Exchange Gain Loss Calculator');
    this.globalSharedService.setMenuList([]);

    this.transactionFiltersForm = this.fb.group({
      'offices': [[]],
      'journals': [[]],
      'projects': [[]],
    });

    forkJoin([
      this.getProjectList(),
      this.getJournalList(),
      this.getOfficeList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeProjectList(result[0]);
        this.subscribeJournalList(result[1]);
        this.subscribeOfficeList(result[2]);
      });

    this.getInputLevelAccountList();
    this.getExchangeGainLossFilterAccountList();
  }

  getLabelClass(value) {
    this.labelText = value > 0 ? 'Gain' : 'Loss';
    return value > 0 ? 'label label-success' : 'label label-danger';
  }

  showConfiguration() {
    this.fieldConfig.show();
  }

  //#region "getProjectList"
  getProjectList() {
    return this.gainLossReportService.GetProjectList();
  }
  //#endregion

  subscribeProjectList(response) {
    this.projectList = [];
    if (response.statusCode === 200 && response.data !== null) {
      response.data.forEach(element => {
        this.projectList.push({
          ProjectId: element.ProjectId,
          ProjectName: element.ProjectName,
          ProjectCode: element.ProjectCode,
          IsChecked: false
        });
      });
    }
  }

  //#region "getJournalList"
  getJournalList() {
    return this.gainLossReportService.GetJournalList();
  }
  //#endregion

  subscribeJournalList(response) {
    this.journalList = [];
    if (response.statusCode === 200 && response.data !== null) {
      response.data.forEach(element => {
        this.journalList.push({
          JournalCode: element.JournalCode,
          JournalName: element.JournalName,
          JournalType: element.JournalType,
          IsChecked: false
        });
      });
    }
  }

  //#region "getOfficeList"
  getOfficeList() {
    return this.gainLossReportService.GetOfficeList();
  }
  //#endregion

  subscribeOfficeList(response) {
    this.officeList = [];
    if (response.statusCode === 200 && response.data !== null) {
      response.data.forEach(element => {
        this.officeList.push({
          OfficeId: element.OfficeId,
          OfficeName: element.OfficeName,
          IsChecked: false
        });
      });
    }
  }

  //#region "getInputLevelAccountList"
  getInputLevelAccountList() {
    this.gainLossReportService.GetInputLevelAccountList().subscribe(
      (response: IResponseData) => {
        this.accountList = [];
        this.accountDataSource = [];
        if (response.statusCode === 200 && response.data !== null) {

          response.data.forEach(element => {
            this.accountList.push({
              AccountCode: element.AccountCode,
              AccountName: element.AccountName,
              ChartOfAccountNewCode: element.ChartOfAccountNewCode
            });

            this.accountDataSource.push({
              Id: element.AccountCode,
              Name: element.AccountName,
            });
          });


          // NOTE: load the initial Account list
          this.filteredAccountsMulti.next(this.accountList.slice());

          // listen for search field value changes
          this.accountMultiFilterCtrl.valueChanges
            .pipe(takeUntil(this.destroyed$))
            .subscribe(() => {
              this.filterAccounts();
            });
        }
      },
      error => { }
    );
  }
  //#endregion

  //#region "FILTER: Account filter"
  protected filterAccounts() {
    if (!this.accountList) {
      return;
    }
    // get the search keyword
    let search = this.accountMultiFilterCtrl.value;
    if (!search) {
      this.filteredAccountsMulti.next(this.accountList.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the accounts
    this.filteredAccountsMulti.next(
      this.accountList.filter(
        acc => acc.AccountName.toLowerCase().indexOf(search) > -1
      )
    );
  }
  //#endregion

  // #region "gainLossAccountSelectionChanged"
  openedChange(event: IOpenedChange) {
    this.AccountIdList = event.Value;

    if (!event.Flag) {
      this.saveExchangeGainLossFilterAccountList();
    }
  }
  // #endregion

  //#region "getExchangeGainLossFilterAccountList"
  saveExchangeGainLossFilterAccountList() {
    this.gainLossReportService.SaveExchangeGainLossFilterAccountList(this.AccountIdList).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
        }
      },
      error => { }
    );
  }
  //#endregion

  //#region "getExchangeGainLossFilterAccountList"
  getExchangeGainLossFilterAccountList() {
    this.gainLossReportService.GetExchangeGainLossFilterAccountList().subscribe(
      (response: IResponseData) => {
        this.AccountIdList = [];
        if (response.statusCode === 200 && response.data !== null) {
          // response.data.forEach(element => {
          //   this.gainLossReportfilter.AccountIdList.push(
          //     element.ChartOfAccountNewId
          //   );
          // });

          this.AccountIdList = response.data;

        }
      },
      error => { }
    );
  }
  //#endregion

  onSelectionChanged(event: number[]) {
    // this.gainLossReportfilter.AccountIdList = event;
  }

  subscribeConfigData(event) {
    this.calculatorConfigData = {
      ComparisionDate: event.ComparisionDate,
      CreditAccount: event.CreditAccount,
      DebitAccount: event.DebitAccount,
      CurrencyId: event.CurrencyId,
      EndDate: event.EndDate,
      StartDate: event.StartDate
    };
  }

  getExchangeGainLossData() {
    if (!this.calculatorConfigData.CurrencyId && !this.calculatorConfigData.StartDate &&
      !this.calculatorConfigData.EndDate) {
      this.toastr.warning('Calculator configuration not set');
      return;
    }

    if (this.AccountIdList.length === 0) {
      this.toastr.warning('Accounts not selected in transaction filter');
      return;
    }

    this.commonLoader.showLoader();

    this.GainLossFilter.ComparisionDate = StaticUtilities.setLocalDate(this.calculatorConfigData.ComparisionDate);
    this.GainLossFilter.FromDate = StaticUtilities.setLocalDate(this.calculatorConfigData.StartDate);
    this.GainLossFilter.ToDate = StaticUtilities.setLocalDate(this.calculatorConfigData.EndDate);

    this.gainLossReportService
      .GetGainLossReportList(this.GainLossFilter)
      .subscribe(
        (response: IResponseData) => {
          this.gainLossReportList = [];
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(element => {
              this.gainLossReportList.push({
                AccountId: element.AccountId,
                AccountCode: element.AccountCode,
                AccountName: element.AccountName,
                AccountCodeName: element.AccountCodeName,
                BalanceOnOriginalDate: element.BalanceOnOriginalDate,
                BalanceOnCurrentDate: element.BalanceOnCurrentDate,
                GainLossAmount: element.GainLossAmount
              });
            });
            this.sumOfGainLossAmount();
          } else {
            this.toastr.error(response.message);
          }

          this.commonLoader.hideLoader();
        },
        error => {
          this.toastr.error('Someting went wrong!');
          this.commonLoader.hideLoader();
        }
      );


  }

  //#region "sumOfGainLossAmount"
  sumOfGainLossAmount() {
    // this.gainLossAddVoucherForm.Amount = this.gainLossReportList.reduce(
    //   (a, { GainLossAmount }) => a + GainLossAmount,
    //   0
    // );
  }
  //#endregion

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}

export interface Element {
  Checked: boolean;
  AccountCode: string;
  AccountName: string;
  BalanceOnOriginalTransactionDates: number;
  BalanceOnComparisionDate: number;
  ResultingGainLoss: number;
}
export interface ICalculatorConfig {
  CurrencyId: number;
  StartDate: any;
  EndDate: any;
  ComparisionDate: any;
  DebitAccount: number;
  CreditAccount: number;
}

export interface IGainLossFilter {
  ComparisionDate: any;
  FromDate: any;
  ToDate: any;
  ToCurrencyId: number;
  Accounts: any[];
  Journals: any[];
  Offices: any[];
  Projects: any[];
}

export interface GainLossReport {
  AccountId: number;
  AccountCode: string;
  AccountName: string;
  AccountCodeName: string;
  BalanceOnOriginalDate: number;
  BalanceOnCurrentDate: number;
  GainLossAmount: number;
}

const ELEMENT_DATA: Element[] = [
  { Checked: false, AccountCode: '1', AccountName: 'Hydrogen', BalanceOnOriginalTransactionDates: 1.0079, BalanceOnComparisionDate: 22, ResultingGainLoss: 0 }]
