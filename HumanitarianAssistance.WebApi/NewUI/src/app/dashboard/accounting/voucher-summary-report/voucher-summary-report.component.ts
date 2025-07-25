import {
  Component,
  OnInit,
  OnDestroy,
  HostListener,
  ViewChild
} from '@angular/core';
import { VoucherSummaryReportService } from './voucher-summary-report.service';
import {
  VoucherSummaryFilterModel,
  IReportVoucherModel,
  BudgetlineListModel,
  VoucherSummaryTransactionModel,
  IVoucherSummaryTransaction
} from './voucher-summary-model';
import { CurrencyModel } from '../../project-management/project-list/project-details/models/project-details.model';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import {
  IOfficeListModel,
  IAccountListModel,
  IJournalListModel,
  IProjectListModel
} from '../vouchers/models/voucher.model';
import { ExchangeRateService } from '../exchange-rate/exchange-rate-listing/exchange-rate.service';
import { VoucherService } from '../vouchers/voucher.service';
import { IProjectJobModel } from '../../project-management/project-list/budgetlines/models/budget-line.models';
import { ToastrService } from 'ngx-toastr';
import { BudgetLineService } from '../../project-management/project-list/budgetlines/budget-line.service';
import {
  IDataSource
} from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { IResponseData } from '../vouchers/models/status-code.model';
import { VoucherSummaryFilterComponent } from './voucher-summary-filter/voucher-summary-filter.component';

@Component({
  selector: 'app-voucher-summary-report',
  templateUrl: './voucher-summary-report.component.html',
  styleUrls: ['./voucher-summary-report.component.scss']
})
export class VoucherSummaryReportComponent implements OnInit, OnDestroy {

  //#endregion
  recordtypetext;
  constructor(
    private voucherSummaryService: VoucherSummaryReportService,
    private appUrl: AppUrlService,
    private globalService: GlobalService,
    private exchangeRateService: ExchangeRateService,
    private voucherService: VoucherService,
    public toastr: ToastrService,
    public budgetService: BudgetLineService,
    private globalSharedService: GlobalSharedService,
    private appurl: AppUrlService
  ) {
    // Set Menu Header Name
    this.globalSharedService.setMenuHeaderName(this.setProjectHeader);

    // Set Menu Header List
    this.globalSharedService.setMenuList([]);
    this.getScreenSize();
  }
  //#region "variables"

  @ViewChild(VoucherSummaryFilterComponent)
  filter: VoucherSummaryFilterComponent;

  // filter
  filterData: VoucherSummaryFilterModel;

  demoSignedURL: string = null;

  // report datasource
  voucherSummaryReport: IReportVoucherModel[];
  voucherSummaryTransactionModel: VoucherSummaryTransactionModel[];
  VoucherSummaryTransaction: IVoucherSummaryTransaction[];

  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;

  // filter fields datasource
  currencyList: CurrencyModel[];
  officeList: IOfficeListModel[] = [];
  inputLevelAccountList: IAccountListModel[] = [];
  journalList: IJournalListModel[] = [];
  projectList: IProjectListModel[] = [];
  budgetLineList: BudgetlineListModel[] = [];
  projectJobList: IProjectJobModel[] = [];
  recordType: any[];

  // selectedCurrency: number = null;
  // selectedRecordType: number = null;
  totalCount: number = null;

  setProjectHeader = 'Voucher Summary Report';

  // dropdown multiselect datasource
  multiAccountsList: IDataSource[] = [];
  multiOfficesList: IDataSource[] = [];
  multiJournalList: IDataSource[] = [];
  multiProjectList: IDataSource[] = [];
  multiBudgetLineList: IDataSource[] = [];
  multiProjectJobList: IDataSource[] = [];

  // loader
  summaryLoader = false;

  // subscription destroy
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  //#endregion

  progress = 20;

  ngOnInit() {
    this.onInitialize();

    this.voucherSummaryReport = [];

    this.recordType = [
      { id: 1, name: 'Single' },
      { id: 2, name: 'Consolidated' }
    ];

    this.GetAllCurrency();
    this.getOfficeList();
    this.getInputLevelAccountList();
    this.getJournalList();
    this.getProjectList();
  }

  onInitialize() {
    this.filterData = {
      Accounts: [],
      BudgetLines: [],
      Currency: null,
      Journals: [],
      Offices: [],
      ProjectJobs: [],
      Projects: [],
      RecordType: null,
      pageIndex: 0,
      pageSize: 10
    };
  }

  //#region "getVoucherSummaryList"
  getVoucherSummaryList(data: VoucherSummaryFilterModel) {
    this.filter.filterLoaderFlag = true;

    this.voucherSummaryReport = [];
    this.voucherSummaryService
      .voucherSummaryReportList(data)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(
              (element: {
                VoucherNo: number;
                VoucherDate: any;
                VoucherCode: string;
                VoucherDescription: string;
              }) => {
                this.voucherSummaryReport.push({
                  VoucherNo: element.VoucherNo,
                  Date: element.VoucherDate,
                  VoucherCode: element.VoucherCode,
                  VoucherDescription: element.VoucherDescription,
                  VoucherTransactions: []
                });
              }
            );

            this.totalCount = response.total;
          }
          this.filter.filterLoaderFlag = false;
        },
        error => {
          this.filter.filterLoaderFlag = false;
        }
      );
  }
  //#endregion

  GetAllCurrency() {
    this.currencyList = [];
    this.globalService
      .getList(this.appUrl.getApiUrl() + GLOBAL.API_code_GetAllCurrency)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(data => {
        if (data.StatusCode === 200) {
          if (data.data.CurrencyList != null) {
            if (data.data.CurrencyList.length > 0) {
              data.data.CurrencyList.forEach(element => {
                this.currencyList.push({
                  CurrencyId: element.CurrencyId,
                  CurrencyCode: element.CurrencyCode
                });
              });

              // child component: set selected currency
              this.filter.selectedCurrency = this.currencyList[0].CurrencyId;
            }
          }
        } else if (data.StatusCode === 400) {
        }
      });
  }

  //#region "getOfficeList"
  getOfficeList() {
    this.exchangeRateService
      .GetOfficeList()
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: any) => {
          this.officeList = [];
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(element => {
              this.officeList.push({
                OfficeId: element.OfficeId,
                OfficeName: element.OfficeName
              });
              this.multiOfficesList.push({
                Id: element.OfficeId,
                Name: element.OfficeName
              });
            });

            // child component: set selected office
            this.filter.officeFilter = this.multiOfficesList.map(x => x.Id);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getInputLevelAccountList"
  getInputLevelAccountList() {
    this.voucherService
      .GetInputLevelAccountList()
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          this.inputLevelAccountList = [];
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(element => {
              this.inputLevelAccountList.push({
                AccountCode: element.AccountCode,
                AccountName: element.AccountName,
                ChartOfAccountNewCode: element.ChartOfAccountNewCode
              });

              this.multiAccountsList.push({
                Id: element.AccountCode,
                Name: element.AccountName
              });
            });

            // child component: set selected account
            this.filter.accountFilter = this.multiAccountsList.map(x => x.Id);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getJournalList"
  getJournalList() {
    this.voucherService
      .GetJournalList()
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          this.journalList = [];
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(element => {
              this.journalList.push({
                JournalCode: element.JournalCode,
                JournalName: element.JournalName,
                JournalType: element.JournalType
              });

              this.multiJournalList.push({
                Id: element.JournalCode,
                Name: element.JournalName
              });
            });

            // child component: set selected joural
            this.filter.journalFilter = this.multiJournalList.map(x => x.Id);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getProjectList"
  getProjectList() {
    this.voucherService
      .GetProjectList()
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          this.projectList = [];
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(element => {
              this.projectList.push({
                ProjectId: element.ProjectId,
                ProjectCode: element.ProjectCode,
                ProjectName: element.ProjectName,
                ProjectNameCode: element.ProjectCode + '-' + element.ProjectName
              });

              this.multiProjectList.push({
                Id: element.ProjectId,
                Name: element.ProjectCode + '-' + element.ProjectName
              });
            });
          }
        },
        error => {}
      );
  }
  //#endregion

  getVoucherTransactions(voucherNo: number) {
    this.summaryLoader = true;

    this.voucherSummaryTransactionModel = [];
    const data = {
      voucherno: voucherNo,
      currencyId: this.filterData.Currency,
      recordType: this.filterData.RecordType
    };

    this.voucherSummaryService
      .voucherTransactionList(data)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          const index = this.voucherSummaryReport.findIndex(
            x => x.VoucherNo === voucherNo
          );

          if (index !== -1) {
            if (response.statusCode === 200 && response.data !== null) {
              this.voucherSummaryReport[index].VoucherTransactions = [];
              response.data.forEach(
                (element: {
                  AccountCode: string;
                  AccountName: string;
                  CurrencyName: string;
                  TransactionDescription: string;
                  Amount: number;
                  TransactionType: string;
                }) => {
                  // this.voucherSummaryReport[index].VoucherTransactions = [];
                  this.voucherSummaryReport[index].VoucherTransactions.push({
                    AccountCode: element.AccountCode,
                    AccountName: element.AccountName,
                    Amount: element.Amount,
                    CurrencyName: element.CurrencyName,
                    TransactionDescription: element.TransactionDescription,
                    TransactionType: element.TransactionType
                  });
                }
              );
            }
          }

          this.summaryLoader = false;
        },
        error => {
          this.summaryLoader = false;
        }
      );
  }

  filterApplied(value: VoucherSummaryFilterModel) {
    this.onInitialize();
    this.filterData = value;
    if (value.RecordType.toString() === '2'){
      this.recordtypetext = 'Consolidated';
    } else {
      this.recordtypetext = 'Single';
    }
    // tslint:disable-next-line: no-unused-expression
    this.recordtypetext = 'Note: Showing Vouchers for Record Type as ' + this.recordtypetext +
    ' and Currency as ' + this.currencyList[this.currencyList.findIndex(x => x.CurrencyId === value.Currency)]['CurrencyCode'];
    this.getVoucherSummaryList(value);
  }

  //#region "Dynamic Scroll"
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

  //#region "onExportPdf"
  onExportPdf() {
    this.globalSharedService
      .getFile(this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetAllVoucherSummaryReportPdf,
                this.filter.filterModel
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe();
  }
  //#endregion

  //#region "pageEvent"
  pageEvent(e) {
    this.filterData.pageIndex = e.pageIndex;
    this.filterData.pageSize = e.pageSize;
    this.getVoucherSummaryList(this.filterData);
  }
  //#endregion

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
