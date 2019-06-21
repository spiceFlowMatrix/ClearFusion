import { Component, OnInit, HostListener } from '@angular/core';
import { ExchangeRateService } from './exchange-rate.service';
import { IExchangeRateFilterModel, IExchangeRateModel, ICurrencyListModel, IOfficeListModel } from '../models/exchange-rate.model';
import { ExchangeRateAddComponent } from '../exchange-rate-add/exchange-rate-add.component';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { LocalStorageService } from '../../../../shared/services/localstorage.service';
import { ApplicationPages } from '../../../../shared/applicationpagesenum';

@Component({
  selector: 'app-exchange-rate-listing',
  templateUrl: './exchange-rate-listing.component.html',
  styleUrls: ['./exchange-rate-listing.component.scss']
})
export class ExchangeRateListingComponent implements OnInit {

  colsm6 = 'col-sm-10 col-sm-offset-1';
  exchangeRateFilter: IExchangeRateFilterModel;
  exchangeRateListLoaderFlag = false;
  exchangeRateList: any[] = [];
  CurrencyList: ICurrencyListModel[] = [];
  selectedExchangeRateDate: any;
  showExchangeRateDetail: boolean;
  officeList: IOfficeListModel[] = [];
  isVerified: boolean;
  exchangeRateVerificationId: number;
  exchangeRateItem: any;
  isDeleted = false;
  isEditingAllowed = false;
  pageId = ApplicationPages.ExchangeRates;

 // screen
 screenHeight: any;
 screenWidth: any;
 scrollStyles: any;

  constructor(private exchangeRateService: ExchangeRateService,   public dialog: MatDialog, private toastr: ToastrService,
    private localStorageService: LocalStorageService) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.initExchangeRateFilter();
    this.getCurrencyList();
    this.getOfficeList();
    this.getSavedExchangeRatesDate();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
  }

  //#region "initVoucherFilter"
  initExchangeRateFilter() {
    this.exchangeRateFilter = {
      TillDateFilter: null,
      FromDateFilter: null,
      VerifiedFilter: null,
      totalCount: null,
      pageIndex: null,
      pageSize: 10
    };

    this.exchangeRateList = [];
  }
  //#endregion

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

  //#region "Add Voucher Popup"
  openAddExchangeRateDialog(): void {
    // NOTE: It passed the data into the Add Voucher Model

    const dialogRef = this.dialog.open(ExchangeRateAddComponent, {
      width: '550px',
      data: {
        data: 'AddExchangeRate',
        currencyList: this.CurrencyList,
      },
      autoFocus: false
    });

    dialogRef.componentInstance.onListRefresh.subscribe(() => {
      this.getSavedExchangeRatesDate();
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }
  //#endregion

  //#region "getCurrencyList"
  getCurrencyList() {
    this.exchangeRateService.GetCurrencyList().subscribe(
      (response: any) => {
        this.CurrencyList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.CurrencyList.push({
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

   //#region "onItemClick"
   onItemClick(item: any) {
    this.exchangeRateItem = item;
    this.exchangeRateVerificationId = item.ExchangeRateVerificationId;
    this.showExchangeRateDetailPanel();
  }

  //#region "show/ hide"
  showExchangeRateDetailPanel() {
    this.showExchangeRateDetail = true;
    this.colsm6 = this.showExchangeRateDetail
      ? 'col-sm-6'
      : 'col-sm-10 col-sm-offset-1';
  }

  //#region "getOfficeList"
  getOfficeList() {
    this.exchangeRateService.GetOfficeList().subscribe(
      (response: any) => {
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

  //#region "getExchangeRatesVerificationList"
  getSavedExchangeRatesDate() {
    this.exchangeRateListLoaderFlag = true;
    this.exchangeRateList = [];

    const filter = {
      FromDate: this.exchangeRateFilter.FromDateFilter !== null ?
      this.exchangeRateService.getLocalDate(this.exchangeRateFilter.FromDateFilter) :
      this.exchangeRateFilter.FromDateFilter,
      ToDate: this.exchangeRateFilter.TillDateFilter !== null ?
      this.exchangeRateService.getLocalDate(this.exchangeRateFilter.TillDateFilter) : this.exchangeRateFilter.TillDateFilter ,
      IsVerified: this.exchangeRateFilter.VerifiedFilter,
      TotalCount: this.exchangeRateFilter.totalCount,
      PageSize: this.exchangeRateFilter.pageSize,
      PageIndex: this.exchangeRateFilter.pageIndex
    };
    this.exchangeRateService.GetSavedExchangeRates(filter).subscribe(
      (response: any) => {
        if (response.statusCode === 200 && response.data !== undefined && response.data !== null) {
          this.exchangeRateListLoaderFlag = false;
          response.data.forEach(element => {
            this.exchangeRateList.push({
              ExchangeRateVerificationId: element.ExRateVerificationId,
              ExchangeRateDate: element.Date,
              VerificationStatus: element.IsVerified
            });
          });

          if (this.exchangeRateList.length > 0) {
            this.selectedExchangeRateDate = this.exchangeRateList[0].ExchangeRateDate;
          }

          this.isDeleted = false;
          this.exchangeRateFilter.totalCount = response.total;

        } else {
          this.exchangeRateListLoaderFlag = false;
        }
      },
      error => {
        this.exchangeRateListLoaderFlag = false;
      }
    );
  }
  //#endregion

   //#region "onFilterReset"
  onFilterReset() {
    this.exchangeRateFilter = {
      TillDateFilter: null,
      FromDateFilter: null,
      VerifiedFilter: null,
      totalCount: null,
      pageIndex: null,
      pageSize: 10
    };

    this.getSavedExchangeRatesDate();
  }

  onDeleteExchangeRate(exchangeRateDate: any) {
    this.exchangeRateListLoaderFlag = true;
    this.isDeleted = true;
    exchangeRateDate = this.exchangeRateService.getLocalDate(exchangeRateDate);
    this.exchangeRateService.DeleteExchangeRates(exchangeRateDate).subscribe(
      response => {
        if (
          response.statusCode === 200
        ) {
          this.exchangeRateListLoaderFlag = false;
          this.toastr.success(response.message);
          this.getSavedExchangeRatesDate();
        } else {
          this.toastr.error(response.message);
          this.exchangeRateListLoaderFlag = false;
        }
      },
      error => {
        this.toastr.error('Something went wrong!!!');
        this.exchangeRateListLoaderFlag = false;
      });
    }

    listRefresh(event: any) {
      this.getSavedExchangeRatesDate();
    }

    //#region "pageEvent"
  pageEvent(e) {
    this.exchangeRateFilter.pageIndex = e.pageIndex;
    this.exchangeRateFilter.pageSize = e.pageSize;
    this.getSavedExchangeRatesDate();
  }
  //#endregion
  }
