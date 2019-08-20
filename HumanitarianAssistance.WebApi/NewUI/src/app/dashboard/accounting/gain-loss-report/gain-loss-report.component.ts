import {
  Component,
  OnInit,
  HostListener,
  OnDestroy,
  AfterViewInit
} from '@angular/core';
import { IResponseData } from '../vouchers/models/status-code.model';
import { GainLossReportService } from './gain-loss-report.service';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { MultiSelectListComponent } from './multi-select-list/multi-select-list.component';
import { MatDialog } from '@angular/material/dialog';
import {
  IOfficeList,
  IJournalList,
  IProjectList,
  IGainLossVoucherList,
  IGainLossAddVoucherForm,
  GainLossReport,
  IGainLossReportFilter,
  IAccountList,
  ICurrencyList,
  IVoucherTypeList
} from './gain-loss-report.model';
import { ShowHideDropdownEnum } from 'src/app/shared/enum';
import { FormControl } from '@angular/forms';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { Subject } from 'rxjs/internal/Subject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { IDataSource, IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';

@Component({
  selector: 'app-gain-loss-report',
  templateUrl: './gain-loss-report.component.html',
  styleUrls: ['./gain-loss-report.component.scss']
})
export class GainLossReportComponent
  implements OnInit, AfterViewInit, OnDestroy {
  //#region "variable"

  setProjectHeader = 'Currency Exchange Report';

  gainLossReportList: GainLossReport[] = [];
  gainLossReportfilter: IGainLossReportFilter;
  gainLossAddVoucherForm: IGainLossAddVoucherForm;
  accountList: IAccountList[] = [];
  accountDataSource: IDataSource[];

  currencyList: ICurrencyList[] = [];
  officeList: IOfficeList[] = [];
  journalList: IJournalList[] = [];
  projectList: IProjectList[] = [];
  voucherTypeList: IVoucherTypeList[] = [];
  gainLossVoucherList: IGainLossVoucherList[] = [];

  selectedCurrency: number = null;
  isEditingAllowed = false;
  pageId = ApplicationPages.ExchangeGainLoss;

  //#region "accounting filter"

  /** control for the MatSelect filter keyword multi-selection */
  public accountMultiFilterCtrl: FormControl = new FormControl();

  /** list of accounts filtered by search keyword */
  public filteredAccountsMulti: ReplaySubject<any[]> = new ReplaySubject<any[]>(1);

  /** Subject that emits when the component has been destroyed. */
  protected _onDestroy = new Subject<void>();
  //#endregion


  // flag
  settingFlag = true;
  gainLossAddVoucherLoader = false;
  gainLossListVoucherLoader = false;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  scrollStylesSearch: any;
  //#endregion

  constructor(
    private gainLossReportService: GainLossReportService,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService,
    public dialog: MatDialog,
    private globalService: GlobalSharedService,
    private localStorageService: LocalStorageService
  ) {
    this.getScreenSize();
    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);

    // Set Menu Header List
    this.globalService.setMenuList([]);
  }

  ngOnInit() {
    this.initFilter();
    this.initAddVoucherForm();
    this.getCurrencyList();
    this.getOfficeList();
    this.getJournalList();
    this.getProjectList();
    this.getInputLevelAccountList();
    this.getExchangeGainLossFilterAccountList();
    this.getVoucherTypeList();
    this.getGainLossVoucherList();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
  }

  ngAfterViewInit() {
    // this.setAccountInitialValue();
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }


  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 220 + 'px',
      'overflow-x': 'hidden'
    };

    this.scrollStylesSearch = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "Add Office Popup"
  openAddOfficeDialog(): void {
    // NOTE: It passed the data into the Add Voucher Model
    const dialogRef = this.dialog.open(MultiSelectListComponent, {
      width: '350px',
      data: {
        data: this.officeList,
        showHideDropdown: ShowHideDropdownEnum.Office,
        selectedValues: this.gainLossReportfilter.OfficeIdList
      }
    });

    dialogRef.componentInstance.officeItemAddRemove.subscribe(
      (data: IOfficeList) => {
        // do something
        this.getCheckedOfficeList();
      }
    );

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "Add Journal Popup"
  openAddJournalDialog(): void {
    // NOTE: It passed the data into the Add Voucher Model
    const dialogRef = this.dialog.open(MultiSelectListComponent, {
      width: '350px',
      data: {
        data: this.journalList,
        showHideDropdown: ShowHideDropdownEnum.Journal,
        selectedValues: this.gainLossReportfilter.JournalIdList
      }
    });

    dialogRef.componentInstance.journalItemAddRemove.subscribe(
      (data: IJournalList) => {
        // do something
        this.getCheckedJournalList();
      }
    );

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "Add Project Popup"
  openAddProjectDialog(): void {
    // NOTE: It passed the data into the Add Voucher Model
    const dialogRef = this.dialog.open(MultiSelectListComponent, {
      width: '350px',
      data: {
        data: this.projectList,
        showHideDropdown: ShowHideDropdownEnum.Project,
        selectedValues: this.gainLossReportfilter.ProjectIdList
      }
    });

    dialogRef.componentInstance.projectItemAddRemove.subscribe(
      (data: IProjectList) => {
        // do something
        this.getCheckedProjectList();
      }
    );

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "initFilter"
  initFilter() {
    this.gainLossReportfilter = {
      ToCurrencyId: null,
      ComparisionDate: new Date(),
      ToDate: new Date(),
      FromDate: new Date(),
      OfficeIdList: [],
      JournalIdList: [],
      ProjectIdList: [],
      AccountIdList: []
    };
  }
  //#endregion

  //#region "initAddVoucherForm"
  initAddVoucherForm() {
    this.gainLossAddVoucherForm = {
      CurrencyId: null,
      JournalId: null,
      CreditAccount: null,
      DebitAccount: null,
      Amount: 0,
      VoucherType: null,
      OfficeId: null
    };
  }
  //#endregion

  //#region "getVoucherTypeList"
  getVoucherTypeList() {
    this.gainLossReportService.GetVoucherTypeList().subscribe(
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
            .pipe(takeUntil(this._onDestroy))
            .subscribe(() => {
              this.filterAccounts();
            });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getCurrencyList"
  getCurrencyList() {
    this.gainLossReportService.GetCurrencyList().subscribe(
      (response: IResponseData) => {
        this.currencyList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.currencyList.push({
              CurrencyId: element.CurrencyId,
              CurrencyCode: element.CurrencyCode,
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
    this.gainLossReportService.GetOfficeList().subscribe(
      (response: IResponseData) => {
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
      },
      error => {}
    );
  }
  //#endregion

  //#region "getJournalList"
  getJournalList() {
    this.gainLossReportService.GetJournalList().subscribe(
      (response: IResponseData) => {
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
      },
      error => {}
    );
  }
  //#endregion

  //#region "getProjectList"
  getProjectList() {
    this.gainLossReportService.GetProjectList().subscribe(
      (response: IResponseData) => {
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
      },
      error => {}
    );
  }
  //#endregion

  //#region "getGainLossVoucherList"
  getGainLossVoucherList() {
    this.gainLossListVoucherLoader = true;

    this.gainLossReportService.GetGainLossVoucherList().subscribe(
      (response: IResponseData) => {
        this.gainLossVoucherList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach((element: IGainLossVoucherList) => {
            this.gainLossVoucherList.push({
              VoucherId: element.VoucherId,
              VoucherName: element.VoucherName,
              JournalName: element.JournalName,
              VoucherDate: element.VoucherDate,

              IsDeleted: element.IsDeleted,
              IsError: element.IsError,
              IsLoading: element.IsLoading
            });
          });
        }
        this.gainLossListVoucherLoader = false;
      },
      error => {
        this.gainLossListVoucherLoader = false;
      }
    );
  }
  //#endregion

  //#region "addGainLossVoucher"
  addGainLossVoucher(data: IGainLossAddVoucherForm) {
    if (this.gainLossAddVoucherForm.CurrencyId !== null) {
      this.gainLossAddVoucherLoader = true;

      data.TimezoneOffset = new Date().getTimezoneOffset();

      this.gainLossReportService.AddGainLossVoucher(data).subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data !== null) {
            this.gainLossVoucherList.push(response.data);

            // this.toastr.success('Voucher Created Successfully');
          } else {
            this.toastr.error(response.message);
          }
          this.gainLossAddVoucherLoader = false;
        },
        error => {
          this.gainLossAddVoucherLoader = false;
          this.toastr.error(error);
        }
      );
    } else {
      this.toastr.warning('Please select Currency');
    }
  }
  //#endregion

  //#region "onGainLossFilterAppled"
  onGainLossFilterAppled() {
    this.commonLoader.showLoader();

    this.gainLossReportfilter.ComparisionDate = new Date(
      new Date(this.gainLossReportfilter.ComparisionDate).getFullYear(),
      new Date(this.gainLossReportfilter.ComparisionDate).getMonth(),
      new Date(this.gainLossReportfilter.ComparisionDate).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );

    this.gainLossReportfilter.FromDate = new Date(
      new Date(this.gainLossReportfilter.FromDate).getFullYear(),
      new Date(this.gainLossReportfilter.FromDate).getMonth(),
      new Date(this.gainLossReportfilter.FromDate).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );

    this.gainLossReportfilter.ToDate = new Date(
      new Date(this.gainLossReportfilter.ToDate).getFullYear(),
      new Date(this.gainLossReportfilter.ToDate).getMonth(),
      new Date(this.gainLossReportfilter.ToDate).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );

    this.selectedCurrency = this.gainLossReportfilter.ToCurrencyId;

    this.gainLossReportService
      .GetGainLossReportList(this.gainLossReportfilter)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data !== null) {
            this.gainLossReportList = [];

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
  //#endregion

  //#region "deleteAccountFromFilter"
  deleteAccountFromFilter(accountId: number) {
    this.toastr.success('Account Removed');
    const index = this.gainLossReportList.findIndex(
      x => x.AccountId === accountId
    );
    if (index !== -1) {
      this.gainLossReportList.splice(index, 1);
    }

    // this.gainLossReportService.deleteAccountFromFilter().subscribe(
    //   (response: IResponseData) => {
    //       if (response.statusCode === 200 && response.data !== null) {
    //        this.toastr.success('Account Removed');
    //       }
    //    },
    //   (error) => {
    //   }
    // );
  }
  //#endregion


  onSelectionChanged(event: number[]) {
    // this.gainLossReportfilter.AccountIdList = event;
  }

  // #region "gainLossAccountSelectionChanged"
  openedChange(event: IOpenedChange) {
    this.gainLossReportfilter.AccountIdList = event.Value;
  }
  // #endregion

  //#region "getExchangeGainLossFilterAccountList"
  getExchangeGainLossFilterAccountList() {
    this.gainLossReportService.GetExchangeGainLossFilterAccountList().subscribe(
      (response: IResponseData) => {
        this.gainLossReportfilter.AccountIdList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.gainLossReportfilter.AccountIdList.push(
              element.ChartOfAccountNewId
            );
          });

        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getSelectedProjectItems"
  getSelectedProjectItems() {
    return this.gainLossReportfilter.ProjectIdList.map(
      y => this.projectList.find(x => x.ProjectId === y).ProjectName
    );
    // return this.projectList.forEach(x => x.ProjectId === projectIds).ProjectName;
  }
  //#endregion

  //#region "removeOfficeFromList"
  removeOfficeFromList(officeId: number) {
    this.officeList.map(x => {
      if (x.OfficeId === officeId) {
        x.IsChecked = false;
      }
    });
    this.getCheckedOfficeList();
  }
  //#endregion

  //#region "removeJournalFromList"
  removeJournalFromList(journalId: number) {
    this.journalList.map(x => {
      if (x.JournalCode === journalId) {
        x.IsChecked = false;
      }
    });
    this.getCheckedJournalList();
  }
  //#endregion

  //#region "removeProjectFromList"
  removeProjectFromList(projectId: number) {
    this.projectList.map(x => {
      if (x.ProjectId === projectId) {
        x.IsChecked = false;
      }
    });
    this.getCheckedProjectList();
  }
  //#endregion

  //#region "getCheckedOfficeList"
  getCheckedOfficeList() {
    this.gainLossReportfilter.OfficeIdList = this.officeList
      .filter(x => x.IsChecked === true)
      .map(x => {
        return x.OfficeId;
      });
  }
  //#endregion

  //#region "getCheckedJournalList"
  getCheckedJournalList() {
    this.gainLossReportfilter.JournalIdList = this.journalList
      .filter(x => x.IsChecked === true)
      .map(x => {
        return x.JournalCode;
      });
  }
  //#endregion

  //#region "getCheckedProjectList"
  getCheckedProjectList() {
    this.gainLossReportfilter.ProjectIdList = this.projectList
      .filter(x => x.IsChecked === true)
      .map(x => {
        return x.ProjectId;
      });
  }
  //#endregion

  //#region "deleteGainLossVoucher"
  deleteGainLossVoucher(voucherId: number) {
    const voucherIndex = this.gainLossVoucherList.findIndex(
      x => x.VoucherId === voucherId
    );

    if (voucherIndex !== -1) {
      this.gainLossVoucherList[voucherIndex].IsLoading = true;

      this.gainLossReportService.DeleteGainLossVoucher(voucherId).subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            this.gainLossVoucherList.splice(voucherIndex, 1);
          } else {
            this.toastr.error(response.message);
            this.gainLossVoucherList[voucherIndex].IsLoading = false;
            this.gainLossVoucherList[voucherIndex].IsError = true;
          }
        },
        error => {
          this.toastr.error('Someting went wrong!');
          this.gainLossVoucherList[voucherIndex].IsLoading = false;
          this.gainLossVoucherList[voucherIndex].IsError = true;
        }
      );
    } else {
      this.toastr.error('Voucher not found');
    }
  }
  //#endregion

  //#region "onDeleteGainLossVoucher"
  onDeleteGainLossVoucher(voucherId: number) {
    this.deleteGainLossVoucher(voucherId);
  }
  //#endregion

  //#region "onGainLossVoucher"
  onGainLossVoucher() {
    this.gainLossAddVoucherForm.CurrencyId = this.selectedCurrency;
    this.addGainLossVoucher(this.gainLossAddVoucherForm);
  }
  //#endregion

  //#region "onResetAddVoucherForm"
  onResetAddVoucherForm() {
    this.gainLossAddVoucherForm.JournalId = null;
    this.gainLossAddVoucherForm.CreditAccount = null;
    this.gainLossAddVoucherForm.DebitAccount = null;
    this.gainLossAddVoucherForm.VoucherType = null;
    this.gainLossAddVoucherForm.OfficeId = null;
  }
  //#endregion

  //#region "onResetGainLossVoucherList"
  onResetGainLossVoucherList() {
    this.getGainLossVoucherList();
  }
  //#endregion

  //#region "sumOfGainLossAmount"
  sumOfGainLossAmount() {
    this.gainLossAddVoucherForm.Amount = this.gainLossReportList.reduce(
      (a, { GainLossAmount }) => a + GainLossAmount,
      0
    );
  }
  //#endregion

  //#region "showVoucherSection"
  showVoucherSection() {
    this.settingFlag = !this.settingFlag;
  }
  //#endregion

  //#region "addVoucherValidation"
  addVoucherValidation(): boolean {
    return this.selectedCurrency == null ||
      this.gainLossAddVoucherForm.CreditAccount == null ||
      this.gainLossAddVoucherForm.DebitAccount == null ||
      this.gainLossAddVoucherForm.JournalId == null ||
      this.gainLossAddVoucherForm.OfficeId == null ||
      this.gainLossAddVoucherForm.VoucherType == null ||
      this.gainLossAddVoucherLoader === true ||
      this.gainLossReportList.length === 0
      ? true
      : false;
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
}
