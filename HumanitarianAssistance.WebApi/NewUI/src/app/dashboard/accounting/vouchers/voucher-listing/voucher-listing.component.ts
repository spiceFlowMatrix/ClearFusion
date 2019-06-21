import { Component, OnInit, HostListener } from '@angular/core';
import {
  IVoucherListFilterModel,
  IVoucherListModel,
  IJournalListModel,
  ICurrencyListModel,
  IOfficeListModel,
  IVoucherTypeListModel,
  IProjectListModel,
  IVoucherDetailModel,
  IAccountListModel
} from '../models/voucher.model';
import { MatDialog } from '@angular/material/dialog';
import { VoucherService } from '../voucher.service';
import { VoucherAddComponent } from '../voucher-add/voucher-add.component';
import { IResponseData } from '../models/status-code.model';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';

@Component({
  selector: 'app-voucher-listing',
  templateUrl: './voucher-listing.component.html',
  styleUrls: ['./voucher-listing.component.scss']
})
export class VoucherListingComponent implements OnInit {
  //#region "Variables"

  // detail panel
  colsm6 = 'col-sm-10 col-sm-offset-1';
  showVoucherDetail = false;

  voucherList: IVoucherListModel[] = [];
  voucherTypeList: IVoucherTypeListModel[] = [];
  journalList: IJournalListModel[] = [];
  currencyList: ICurrencyListModel[] = [];
  officeList: IOfficeListModel[] = [];
  projectList: IProjectListModel[] = [];
  budgetLineList: ICurrencyListModel[] = [];
  inputLevelAccountList: IAccountListModel[] = [];
  voucherFilter: IVoucherListFilterModel;
  selectedVoucherId: number;
  voucherListLoaderFlag = false;
  isEditingAllowed = false;
  pageId = ApplicationPages.Vouchers;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  listingScreenWidth = 100;
  detailScreenWidth = 0;


  //#endregion

  constructor(
    public dialog: MatDialog,
    private voucherService: VoucherService,
    private localStorageService: LocalStorageService
  ) {
    this.getScreenSize();
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

  //#region
  // ngAfterViewInit() {
  //   this.viewHeight = this.elementView.nativeElement.offsetHeight;
  //   this.scrollStyles = {
  //     'overflow-y': 'auto',
  //     'height': this.screenHeight - 110 + 'px',
  //     };
  // }
  //#endregion

  ngOnInit() {
    this.initVoucherFilter();
    this.getInputLevelAccountList();
    this.getVoucherTypeList();
    this.getJournalList();
    this.getCurrencyList();
    this.getProjectList();
    // this.getBudgetLineList();
    this.getOfficeList();
    this.getVoucherList();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
  }

  //#region "initVoucherFilter"
  initVoucherFilter() {
    this.voucherFilter = {
      FilterValue: '',
      VoucherNoFlag: true,
      ReferenceNoFlag: true,
      DescriptionFlag: true,
      JournalNameFlag: true,
      DateFlag: true,

      pageIndex: 0,
      pageSize: 10,
      totalCount: 0
    };
  }
  //#endregion

  //#region "getVoucherList"
  getVoucherList() {
    // init count
    this.voucherFilter.totalCount = 0;

    // this.commonLoader.showLoader();
    this.voucherListLoaderFlag = true;

    this.voucherService.GetVoucherList(this.voucherFilter).subscribe(
      response => {
        this.voucherList = [];
        if (
          response.StatusCode === 200 &&
          response.data.VoucherDetailList != null
        ) {
          if (response.data.VoucherDetailList.length > 0) {
            this.voucherFilter.totalCount =
              response.data.TotalCount != null ? response.data.TotalCount : 0;

            response.data.VoucherDetailList.forEach(element => {
              this.voucherList.push({
                VoucherNo: element.VoucherNo,
                CurrencyCode: element.CurrencyCode,
                CurrencyId: element.CurrencyId,
                VoucherDate:
                  element.VoucherDate != null
                    ? new Date(
                        new Date(element.VoucherDate).getTime() -
                          new Date().getTimezoneOffset() * 60000
                      )
                    : null,
                ChequeNo: element.ChequeNo,
                ReferenceNo: element.ReferenceNo,
                Description: element.Description,
                JournalName: element.JournalName,
                JournalCode: element.JournalCode,
                VoucherTypeId: element.VoucherTypeId,
                OfficeId: element.OfficeId,
                ProjectId: element.ProjectId,
                BudgetLineId: element.BudgetLineId,
                OfficeName: element.OfficeName
              });
            });
          }
        }
        // this.commonLoader.hideLoader();

        this.voucherListLoaderFlag = false;
      },
      error => {
        this.voucherListLoaderFlag = false;
        // this.commonLoader.hideLoader();
      }
    );
  }
  //#endregion

  //#region "getVoucherTypeList"
  getVoucherTypeList() {
    this.voucherService.GetVoucherTypeList().subscribe(
      (response: IResponseData) => {
        this.voucherTypeList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.voucherTypeList.push({
              VoucherTypeId: element.VoucherTypeId,
              VoucherTypeName: element.VoucherTypeName
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
    this.voucherService.GetJournalList().subscribe(
      (response: IResponseData) => {
        this.journalList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.journalList.push({
              JournalCode: element.JournalCode,
              JournalName: element.JournalName,
              JournalType: element.JournalType
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getCurrencyList"
  getCurrencyList() {
    this.voucherService.GetCurrencyList().subscribe(
      (response: IResponseData) => {
        this.currencyList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.currencyList.push({
              CurrencyId: element.CurrencyId,
              CurrencyName: element.CurrencyName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getOfficeList"
  getOfficeList() {
    this.voucherService.GetOfficeList().subscribe(
      (response: IResponseData) => {
        this.officeList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.officeList.push({
              OfficeId: element.OfficeId,
              OfficeName: element.OfficeName
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
    this.voucherService.GetProjectList().subscribe(
      (response: IResponseData) => {
        this.projectList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.projectList.push({
              ProjectId: element.ProjectId,
              ProjectCode: element.ProjectCode,
              ProjectName: element.ProjectName,
              ProjectNameCode: element.ProjectCode+"-"+element.ProjectName
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
    this.voucherService.GetInputLevelAccountList().subscribe(
      (response: IResponseData) => {
        this.inputLevelAccountList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.inputLevelAccountList.push({
              AccountCode: element.AccountCode,
              AccountName: element.AccountName,
              ChartOfAccountNewCode: element.ChartOfAccountNewCode
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "onFilterApplied"
  onFilterApplied() {
    // back to index 0
    this.voucherFilter.pageIndex = 0;
    this.getVoucherList();
  }
  //#endregion

  //#region "onFilterReset"
  onFilterReset() {
    this.initVoucherFilter();
    this.getVoucherList();
  }
  //#endregion

  //#region "pageEvent"
  pageEvent(e) {
    this.voucherFilter.pageIndex = e.pageIndex;
    this.voucherFilter.pageSize = e.pageSize;
    // this.voucherFilter.totalCount =  e.length;

    this.getVoucherList();
  }
  //#endregion

  //#region "onItemClick"
  onItemClick(item: number) {
    this.selectedVoucherId = item;
    this.showVoucherDetailPanel();
  }
  //#endregion

  //#region "show/ hide"
  showVoucherDetailPanel() {
    this.showVoucherDetail = true;
    this.colsm6 = this.showVoucherDetail
      ? 'col-sm-12'
      : 'col-sm-10 col-sm-offset-1';


    this.listingScreenWidth = 50;
    this.detailScreenWidth = 50;

  }
  // hideVoucherDetailPanel() {
  //   this.showVoucherDetail = false;
  //   this.colsm6 = this.showVoucherDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  // }
  //#endregion

  //#region "Add Voucher Popup"
  openAddVoucherDialog(): void {
    // NOTE: It passed the data into the Add Voucher Model

    const dialogRef = this.dialog.open(VoucherAddComponent, {
      width: '550px',
      data: {
        data: 'hello',
        journalList: this.journalList,
        currencyList: this.currencyList,
        officeList: this.officeList,
        projectList: this.projectList,
        budgetLineList: this.budgetLineList,
        voucherTypeList: this.voucherTypeList
      }
    });

    dialogRef.componentInstance.onListRefresh.subscribe(() => {
      // do something
      this.getVoucherList();
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "voucherDetailChangedEmit"
  voucherDetailChangedEmit(e: IVoucherDetailModel) {
    const data = this.voucherList.find(x => x.VoucherNo === e.VoucherNo);
    const indexOfVoucher = this.voucherList.indexOf(data);

    // set journal to journal in table
    e.JournalName = this.journalList.find(
      x => x.JournalCode === e.JournalCode
    ).JournalName;

     this.voucherList[indexOfVoucher] = e;
  }
  //#endregion
}
