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
import { IAccountList, IGainLossAddVoucherForm } from '../gain-loss-report/gain-loss-report.model';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { SelectionModel } from '@angular/cdk/collections';
import { GainLossStatus } from 'src/app/shared/enum';

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

  selectedRows: GainLossReport[];
  // gainLossAddVoucherForm: IGainLossAddVoucherForm;
  /** control for the MatSelect filter keyword multi-selection */
  public accountMultiFilterCtrl: FormControl = new FormControl();
  displayedColumns = ['select', 'AccountCode', 'AccountName',
    'BalanceOnOriginalTransactionDates', 'BalanceOnComparisionDate', 'GainLossStatus', 'ResultingGainLoss'];

  labelText: string;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  public filteredAccountsMulti: ReplaySubject<any[]> = new ReplaySubject<any[]>(1);
  selection = new SelectionModel<any>(true, []);
  @ViewChild(ConfigurationFilterComponent) fieldConfig: ConfigurationFilterComponent;
  constructor(private globalSharedService: GlobalSharedService,
    private gainLossReportService: ExchangeGainLossReportService, private fb: FormBuilder,
    private toastr: ToastrService, private commonLoader: CommonLoaderService, ) { }

  ngOnInit() {
    this.globalSharedService.setMenuHeaderName('Currency Exchange Gain Loss Calculator');
    this.globalSharedService.setMenuList([]);

    this.onFormInIt();

    this.selectedRows = [];

    this.GainLossFilter = {
      AccountIdList: [],
      ComparisionDate: null,
      FromDate: null,
      JournalIdList: [],
      OfficeIdList: [],
      ProjectIdList: [],
      ToCurrencyId: null,
      ToDate: null
    };

    forkJoin([
      this.getProjectList(),
      this.getJournalList(),
      this.getOfficeList(),
      this.getInputLevelAccountList(),
      this.getExchangeGainLossFilterAccountList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeProjectList(result[0]);
        this.subscribeJournalList(result[1]);
        this.subscribeOfficeList(result[2]);
        this.subscribeInputAccountList(result[3]);
        this.subscribeExchangeGainLossAccountList(result[4]);
      });
  }

  onFormInIt() {
    this.transactionFiltersForm = this.fb.group({
      'offices': [[]],
      'journals': [[]],
      'projects': [[]],
    });
    this.AccountIdList = [];
  }

  getLabelClass(value) {
    this.labelText = value === GainLossStatus.Gain ? 'Gain' : value < 0 ? 'Loss' : value === GainLossStatus.Consolidated ?
    'Consolidated' : 'Balanced' ;
    return value === GainLossStatus.Gain ? 'label label-success' : value < GainLossStatus.Balanced ?
              'label label-danger' : value === GainLossStatus.Consolidated ?
              'label label-primary' : 'label label-info';
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
    return this.gainLossReportService.GetInputLevelAccountList();
  }
  //#endregion

  subscribeInputAccountList(response) {
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
  }

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
    return this.gainLossReportService.GetExchangeGainLossFilterAccountList();
  }
  //#endregion

  subscribeExchangeGainLossAccountList(response) {
    this.AccountIdList = [];
    if (response.statusCode === 200 && response.data !== null) {
      this.AccountIdList = response.data;
      setTimeout(() => {
        this.getExchangeGainLossData();
      }, 1000);

    }
  }

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
      StartDate: event.StartDate,
      CreditAccountName: event.CreditAccountName,
      DebitAccountName: event.DebitAccountName
    };

    this.getExchangeGainLossData();
  }

  applyTransactionFilter() {
    this.getExchangeGainLossData();
    this.toggeleShowFilter();
  }

  toggeleShowFilter() {
    this.showFilters = !this.showFilters;
  }

  getExchangeGainLossData() {
    if (!this.calculatorConfigData.CurrencyId && !this.calculatorConfigData.StartDate &&
      !this.calculatorConfigData.EndDate && !this.calculatorConfigData.ComparisionDate) {
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
    this.GainLossFilter.AccountIdList = this.AccountIdList;
    this.GainLossFilter.JournalIdList = this.transactionFiltersForm.value.journals;
    this.GainLossFilter.OfficeIdList = this.transactionFiltersForm.value.offices;
    this.GainLossFilter.ProjectIdList = this.transactionFiltersForm.value.projects;
    this.GainLossFilter.ToCurrencyId = this.calculatorConfigData.CurrencyId;

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
                BalanceOnOriginalTransactionDates: element.BalanceOnOriginalDate,
                BalanceOnComparisionDate: element.BalanceOnCurrentDate,
                ResultingGainLoss: element.GainLossAmount,
                GainLossStatus: element.GainLossStatus
              });
            })
            this.type = '';
            // this.sumOfGainLossAmount();
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

  selectionevent(event) {
    console.log(event, 'e');
    console.log(event, 'e');
  }

  onConsolidation() {
    if (this.selection.selected.length <= 0) {
      this.toastr.warning('Please select atleast one account');
    } else {
      const accounts =  this.selection.selected.filter(x => x.GainLossStatus === GainLossStatus.Consolidated ||
        x.GainLossStatus === GainLossStatus.Balanced);

      if (accounts.length > 0) {
        this.toastr.warning('Please remove selected consolidated or balanced accounts and try again');
        return;
      }
      this.type = 'consolidation';
    }
  }

  subscribeType(event) {
    this.type = '';
    this.selection.clear();
    this.getExchangeGainLossData();
  }

  clearTransactionFilters() {
    this.onFormInIt();
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.gainLossReportList.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.gainLossReportList.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

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
  CreditAccountName: string;
  DebitAccountName: string;
}

export interface IGainLossFilter {
  ComparisionDate: any;
  FromDate: any;
  ToDate: any;
  ToCurrencyId: number;
  AccountIdList: any[];
  JournalIdList: any[];
  OfficeIdList: any[];
  ProjectIdList: any[];
}

export interface GainLossReport {
  AccountId: number;
  AccountCode: string;
  AccountName: string;
  BalanceOnOriginalTransactionDates: number;
  BalanceOnComparisionDate: number;
  ResultingGainLoss: number;
  GainLossStatus: number;
}
