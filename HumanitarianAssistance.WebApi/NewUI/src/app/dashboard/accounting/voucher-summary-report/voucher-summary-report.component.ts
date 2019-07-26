import { Component, OnInit, OnDestroy, HostListener } from '@angular/core';
import { VoucherSummaryReportService } from './voucher-summary-report.service';
import { VoucherSummaryFilterModel, ReportVoucherModel, BudgetlineListModel,
  VoucherSummaryTransactionModel } from './voucher-summary-model';
import { CurrencyModel } from '../../project-management/project-list/project-details/models/project-details.model';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { IOfficeListModel, IAccountListModel, IJournalListModel, IProjectListModel } from '../vouchers/models/voucher.model';
import { ExchangeRateService } from '../exchange-rate/exchange-rate-listing/exchange-rate.service';
import { VoucherService } from '../vouchers/voucher.service';
import { IProjectJobModel } from '../../project-management/project-list/budgetlines/models/budget-line.models';
import { ToastrService } from 'ngx-toastr';
import { BudgetLineService } from '../../project-management/project-list/budgetlines/budget-line.service';
import { IDataSource, IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { IResponseData } from '../vouchers/models/status-code.model';

@Component({
  selector: 'app-voucher-summary-report',
  templateUrl: './voucher-summary-report.component.html',
  styleUrls: ['./voucher-summary-report.component.scss']
})
export class VoucherSummaryReportComponent implements OnInit, OnDestroy {

  // filter
  filterData: VoucherSummaryFilterModel;

  demoSignedURL: string = null;

  // report datasource
  voucherSummaryReport: ReportVoucherModel[];
  voucherSummaryTransactionModel: VoucherSummaryTransactionModel[];

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

  constructor(
    private voucherSummaryService: VoucherSummaryReportService,
    private appUrl: AppUrlService,
    private globalService: GlobalService,
    private exchangeRateService: ExchangeRateService,
    private voucherService: VoucherService,
    public toastr: ToastrService,
    public budgetService: BudgetLineService,
    private globalSharedService: GlobalSharedService
  ) {
    // Set Menu Header Name
    this.globalSharedService.setMenuHeaderName(this.setProjectHeader);

    // Set Menu Header List
    this.globalSharedService.setMenuList([]);
    this.getScreenSize();
   }

  ngOnInit() {

    this.onInitialize();

    this.voucherSummaryReport = [];

    this.recordType = [
      {id: 1, name: 'Single'},
      {id: 2, name: 'Consolidated'}
    ];

    // this.getVoucherSummaryList(data);
    this.GetAllCurrency();
    this.getOfficeList();
    this.getInputLevelAccountList();
    this.getJournalList();
    this.getProjectList();
    // this.getProjectJobList();
  }

  onInitialize() {
    this.filterData = {
      Accounts: [],
      BudgetLines : [],
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
    this.voucherSummaryReport = [];
    this.voucherSummaryService.voucherSummaryReportList(data).pipe(
      takeUntil(this.destroyed$)
    ).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach((element: { VoucherNo: number, VoucherDate: any;
            VoucherCode: string; VoucherDescription: string; }) => {
            this.voucherSummaryReport.push({
              VoucherNo: element.VoucherNo,
              Date: element.VoucherDate,
              VoucherCode: element.VoucherCode,
              VoucherDescription: element.VoucherDescription
            });
          });

          this.totalCount = response.total;
        }
      },
      error => {}
    );
  }
  //#endregion

  GetAllCurrency() {
    this.currencyList = [];
    this.globalService
    .getList(this.appUrl.getApiUrl() + GLOBAL.API_code_GetAllCurrency).pipe(
      takeUntil(this.destroyed$)
    ).subscribe(data => {
      if (data.StatusCode === 200) {
        if (data.data.CurrencyList != null) {
          if (data.data.CurrencyList.length > 0) {
            data.data.CurrencyList.forEach(element => {
              this.currencyList.push({
                CurrencyId: element.CurrencyId,
                CurrencyCode: element.CurrencyCode
              });
            });
          }
        }
      } else if (data.StatusCode === 400) {
      }
    });
  }

  //#region "getOfficeList"
  getOfficeList() {
    this.exchangeRateService.GetOfficeList()
    .pipe(
      takeUntil(this.destroyed$)
    ).subscribe(
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
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getInputLevelAccountList"
  getInputLevelAccountList() {
    this.voucherService.GetInputLevelAccountList()
    .pipe(
      takeUntil(this.destroyed$)
    ).subscribe(
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
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getJournalList"
  getJournalList() {
    this.voucherService.GetJournalList()
    .pipe(
      takeUntil(this.destroyed$)
    ).subscribe(
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
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getProjectList"
  getProjectList() {
    this.voucherService.GetProjectList()
    .pipe(
      takeUntil(this.destroyed$)
    ).subscribe(
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

    this.voucherSummaryService.voucherTransactionList(data).pipe(
      takeUntil(this.destroyed$)
    ).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach((element: { AccountCode: string, AccountName: string;
            CurrencyName: string; TransactionDescription: string; Amount: number; TransactionType: string }) => {
            this.voucherSummaryTransactionModel.push({
             AccountCode: element.AccountCode,
             AccountName: element.AccountName,
             Amount: element.Amount,
             CurrencyName: element.CurrencyName,
             TransactionDescription: element.TransactionDescription,
             TransactionType: element.TransactionType
            });
          });
          this.summaryLoader = false;
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
    this.getVoucherSummaryList(value);
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
      this.screenHeight = window.innerHeight;
      this.screenWidth = window.innerWidth;

      this.scrollStyles = {
        'overflow-y': 'auto',
        'height': this.screenHeight - 110 + 'px',
        'overflow-x': 'hidden'
        };
  }
  //#endregion

  //#region "getProjectList"
  // getSignedURL(event: any) {

  //   const files = event.srcElement.files[0];

  //   const formData = new FormData();
  //         formData.append('file', files);

  //   this.voucherSummaryService.getSignedURLDemo()
  //   .pipe(
  //     takeUntil(this.destroyed$)
  //   ).subscribe(
  //     (response: any) => {
  //       if (response.statusCode === 200 && response.data !== null) {
  //         debugger;
  //         this.demoSignedURL = response.data;

  //         this.voucherSummaryService.upload(this.demoSignedURL, formData)
  //         .pipe(
  //           takeUntil(this.destroyed$)
  //         ).subscribe(
  //           (response: any) => {
  //           });

  //         // window.open(this.demoSignedURL, '_blank');

  //         // const xhr  = new XMLHttpRequest();
  //         // xhr.open('PUT', this.demoSignedURL, true);
  //         // xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
  //         // xhr.withCredentials = false;
  //         // xhr.setRequestHeader('Content-Type', 'text/plain');
  //         // xhr.send(formData);

  //         // req.onload = function(event) {
  //         //     this.loadFileList();
  //         // }.bind(this);
  //         // this.documentName = droppedFile.relativePath;
  //         // const formData = new FormData();
  //         // formData.append('filesData', file, droppedFile.relativePath);
  //         // formData.append('activityId', this.projectActivityId.toString());

  //       }
  //     },
  //     error => {}
  //   );
  // }
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
