import {
  Component,
  OnInit,
  HostListener,
  EventEmitter,
  Output
} from '@angular/core';
import { AddbudgetLineComponent } from '../addbudget-line/addbudget-line.component';
import {
  ICurrencyListModel,
  IBudgetLineModel,
  IProjectJobModel,
  IBudgetListFilterModel,
  ITransactionDetailModel
} from '../models/budget-line.models';
import { BudgetLineService } from '../budget-line.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Component({
  selector: 'app-budget-line-listing',
  templateUrl: './budget-line-listing.component.html',
  styleUrls: ['./budget-line-listing.component.scss']
})
export class BudgetLineListingComponent implements OnInit {
  //#region "Variables"

  // detail panel
  colsm6 = 'col-sm-10 col-sm-offset-1';
  showBudgetDetail = false;

  currencyList: ICurrencyListModel[] = [];
  projectJobList: IProjectJobModel[] = [];
  BudgetLineDetailList: IBudgetLineModel[] = [];
  budgetFilter: IBudgetListFilterModel;
  transactionList: ITransactionDetailModel[] = [];
  //#endregion

  //#region loaderFlag
  BudgetListLoaderFlag = false;
  //#endregion loaderFlag

  Projectid: number;
  selectedProjectId: number;

  //#region   "input/Output"
  @Output() budgetLineListRefresh = new EventEmitter();

  //#endregion

  // listrefresh

  // screen sizes
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  selectedBudgetId: number;
  budgetDetailObj: IBudgetLineModel;

  constructor(
    public dialog: MatDialog,
    public budgetService: BudgetLineService,
    public toastr: ToastrService,
    private routeActive: ActivatedRoute
  ) {
    this.getScreenSize();
    this.routeActive.parent.params.subscribe(params => {
      this.Projectid = +params['id'];
    });
    this.selectedProjectId = +this.Projectid;
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
  //#endregion
  ngOnInit() {
    this.getCurrencyList();
    this.getProjectJobList(this.selectedProjectId);
    // this.getProjectBudgetLineList(this.Projectid);
    this.initBudgetFilter();

    this.getProjectBudgetLinedetailList();
  }

  //#region "initbudgetFilter"
  initBudgetFilter() {
    this.budgetFilter = {
      FilterValue: '',
      BudgetLineIdFlag: true,
      BudgetCodeFlag: true,
      BudgetNameFlag: true,
      ProjectJobIdFlag: true,
      InitialBudgetFlag: true,
      DateFlag: true,

      pageIndex: 0,
      pageSize: 10,
      totalCount: 0
    };
  }
  //#endregion

  //#region "onItemClick"
  onItemClick(item: any) {
    this.selectedBudgetId = item.BudgetLineId;
    this.budgetDetailObj = item;
    this.showBudgetDetailPanel();
  }
  //#endregion

  //#region "show/ hide"
  showBudgetDetailPanel() {
    this.showBudgetDetail = true;
    this.colsm6 = this.showBudgetDetail
      ? 'col-sm-6'
      : 'col-sm-10 col-sm-offset-1';
  }

  //#endregion

  transactiondetailList(event: any) {}

  //#region "openDocumentsDialog"
  //  openDocumentsDialog() {
  //   const modelData: IProjectBudgetLineDocumentModel = {
  //     HeaderText: 'Budegt Documents',
  //     // DocumentModel: this.planningDocumentsList,
  //     ProjectId: this.Projectid,
  //     BudgetLineId: this.selectedBudgetId
  //   };

  // const dialogRef = this.dialog.open(BudgetLineDocumentsComponent, {
  //   width: '400px',
  //   maxHeight: '500px',
  //   autoFocus: false,
  //   data: modelData
  // });
  // dialogRef.afterClosed().subscribe(result => {});
  // }
  //#endregion

  //#region "Add Budget Popup"
  openAddBudgetLineDialog(): void {
    // NOTE: It passed the data into the Add Budget Model
    const dialogRef = this.dialog.open(AddbudgetLineComponent, {
      width: '550px',
      data: {
        data: 'hello',
        ProjectJobList: this.projectJobList,
        CurrencyList: this.currencyList,
        Projectid: this.Projectid,
        BudgetLineDetailList: this.BudgetLineDetailList
        // officeList: this.officeList,
        // projectList: this.projectList,
        // voucherTypeList: this.voucherTypeList,
      }
    });

    // list refeshed from add-budget to listing page
    dialogRef.componentInstance.onListRefresh.subscribe(() => {
      this.getProjectBudgetLinedetailList();
      this.budgetLineListRefresh.emit();
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "getCurrencyList"
  getCurrencyList() {
    this.budgetService.GetCurrencyList().subscribe(
      (response: IResponseData) => {
        this.currencyList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.currencyList.push({
              CurrencyId: element.CurrencyId,
              CurrencyName: element.CurrencyName,
              CurrencyCode: element.CurrencyCode
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getProjectJobList"
  getProjectJobList(projectId) {
    this.budgetService.GetProjectJobList(projectId).subscribe(
      (response: IResponseData) => {
        this.projectJobList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.projectJobList.push({
              ProjectJobId: element.ProjectJobId,
              ProjectJobCode: element.ProjectJobCode,
              ProjectJobName: element.ProjectJobName
            });
          });
        }
      },
      error => {
        this.toastr.error('Something went Wrong. Please try again');
      }
    );
  }
  //#endregion

  //#region "GetBudgetLine List"
  getProjectBudgetLineList(projecId) {
    this.BudgetListLoaderFlag = true;
    // this.budgetFilter.totalCount = 0;
    this.budgetService.GetProjectBudgetLineList(projecId).subscribe(
      (response: IResponseData) => {
        this.BudgetLineDetailList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.BudgetLineDetailList.push({
              ProjectJobId: element.ProjectJobId,
              ProjectJobCode: element.ProjectJobCode,
              ProjectJobName: element.ProjectJobName,
              BudgetCode: element.BudgetCode,
              BudgetLineId: element.BudgetLineId,
              BudgetName: element.BudgetName,
              CurrencyId: element.CurrencyId,
              CurrencyName: element.CurrencyName,
              InitialBudget: element.InitialBudget,
              ProjectId: element.ProjectId,
              CreatedDate: element.CreatedDate,
              DebitPercentage: element.DebitPercentage,
              Expenditure: element.Expenditure
            });
          });
        }
        this.BudgetListLoaderFlag = false;
      },

      error => {
        this.BudgetListLoaderFlag = false;
        this.toastr.error('Something Went Wrong. Please try again');
      }
    );
  }
  //#endregion

  //#region  "getBudgetLineList filter list"
    getProjectBudgetLinedetailList() {
    this.budgetFilter.totalCount = 0;
    this.BudgetListLoaderFlag = true;
    this.budgetFilter.ProjectId = this.Projectid;

    this.budgetService.GetProjectBudgetdetailList(this.budgetFilter).subscribe(
      response => {
        this.BudgetLineDetailList = [];
        if (response.statusCode === 200 && response.data != null) {
          if (response.data.length > 0) {
            this.budgetFilter.totalCount =
              response.total != null ? response.total : 0;

            response.data.forEach(element => {
              this.BudgetLineDetailList.push({
                BudgetCode: element.BudgetCode,
                BudgetName: element.BudgetName,
                CurrencyName: element.CurrencyName,
                InitialBudget: element.InitialBudget,
                ProjectJobCode: element.ProjectJobCode,
                ProjectJobId: element.ProjectJobId,
                ProjectJobName: element.ProjectJobName,
                CurrencyId: element.CurrencyId,
                CreatedDate:
                  element.CreatedDate != null
                    ? new Date(
                        new Date(element.VoucherDate).getTime() -
                          new Date().getTimezoneOffset() * 60000
                      )
                    : null,
                BudgetLineId: element.BudgetLineId,
                ProjectId: element.Projectid,
                DebitPercentage: element.DebitPercentage,
                Expenditure: element.Expenditure
              });
            });
          }
        }
        this.BudgetListLoaderFlag = false;
      },
      error => {
        this.BudgetListLoaderFlag = false;
      }
    );
  }
  //#endregion

  //#region  "get transaction list emit from budget line detail"

  transactiondetailListEmit(event: any) {
    event.forEach(element => {});
  }
  //#endregion
  //#region budgetLineDetailChangedEmit changes to list

  budgetLineDetailChangedEmit(event: IBudgetLineModel) {
    const data = this.BudgetLineDetailList.find(
      x => x.BudgetLineId === event.BudgetLineId
    );
    const indexOfBudget = this.BudgetLineDetailList.indexOf(data);
    // get currency name
    event.CurrencyName = this.currencyList.find(
      x => x.CurrencyId === event.CurrencyId
    ).CurrencyName;
    event.ProjectJobName = this.projectJobList.find(
      x => x.ProjectJobId === event.ProjectJobId
    ).ProjectJobName;
    // event.DebitPercentage === this.transactionList.reduce((a, { Debit }) => a + Debit, 0);

    console.log(event);
    this.BudgetLineDetailList[indexOfBudget] = event;
  }
  //#endregion

  //#region "onFilterApplied"
  onFilterApplied() {
    // back to index 0
    this.budgetFilter.pageIndex = 0;
    this.getProjectBudgetLinedetailList();
  }
  //#endregion

  //#region "onFilterReset"
  onFilterReset() {
    this.initBudgetFilter();
    this.getProjectBudgetLinedetailList();
  }
  //#endregion

  //#region "pageEvent"
  pageEvent(e) {
    this.budgetFilter.pageIndex = e.pageIndex;
    this.budgetFilter.pageSize = e.pageSize;
    this.budgetFilter.totalCount = e.length;

    this.getProjectBudgetLinedetailList();
  }
  //#endregion

  //#region "excel import list refresh"
  budgetLineListExcelRefresh(event: any) {
    if (event) {
      this.getProjectBudgetLinedetailList();
    }
  }
  //#endregion
}
