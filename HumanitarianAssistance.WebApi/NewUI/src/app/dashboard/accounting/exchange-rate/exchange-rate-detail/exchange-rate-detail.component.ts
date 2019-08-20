import { Component, OnInit, Input, OnChanges, EventEmitter, SimpleChanges, Output } from '@angular/core';
import { ICurrencyList } from '../../gain-loss-report/gain-loss-report.model';
import { ICurrencyListModel } from '../../vouchers/models/voucher.model';
import { IOfficeListModel, IOfficeExchangeRateModels } from 'src/app/dashboard/accounting/exchange-rate/models/exchange-rate.model';
import { ExchangeRateService } from '../exchange-rate-listing/exchange-rate.service';
import { IResponseData } from '../../vouchers/models/status-code.model';
import { CommonLoaderService } from '../../../../shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { VerifyExchangeRateComponent } from '../verify-exchange-rate/verify-exchange-rate.component';

@Component({
  selector: 'app-exchange-rate-detail',
  templateUrl: './exchange-rate-detail.component.html',
  styleUrls: ['./exchange-rate-detail.component.scss']
})
export class ExchangeRateDetailComponent implements OnInit, OnChanges {

@Input() currencyList: ICurrencyListModel[] = [];
@Input() officeList: IOfficeListModel[] = [];
@Input() isEditingAllowed: boolean;
@Input() exchangeRateItem: any;
@Output() listRefresh = new EventEmitter();

exchangeRateVerify = false;
exchangeRateLoader = false;
isVerified = false;
verifyExchangeRateLoader = false;
exchangeRateDate: any;

selectedOffice: number;
exchangeRateList: any[];

  constructor(private exchangeRateService: ExchangeRateService,
              private toastr: ToastrService, public commonLoader: CommonLoaderService,
              public dialog: MatDialog) { }

  ngOnInit() {
  }

  //#region "ngOnChanges"
  ngOnChanges( changes: SimpleChanges) {
    if (this.exchangeRateItem !== undefined) {
      this.isVerified = this.exchangeRateItem.VerificationStatus;
      // this.exchangeRateDate = this.selectedExchangeRateDate;

      this.exchangeRateDate = this.exchangeRateItem.ExchangeRateDate;
    }

    this.getExchangeRates();
  }
  //#endregion

  getExchangeRates() {
    this.exchangeRateList = [];
    if (
      this.exchangeRateItem !== null &&
      this.exchangeRateItem !== undefined
    ) {
      this.getExchangeRateDetailByDate(this.exchangeRateDate);
    }
  }

  getExchangeRateDetailByDate(ExchangeRateDate: any) {
    this.exchangeRateItem.ExchangeRateDate = this.exchangeRateService.getLocalDate(this.exchangeRateItem.ExchangeRateDate);
    this.selectedOffice = this.selectedOffice === undefined ? this.officeList[0].OfficeId : this.selectedOffice;

    this.exchangeRateService.GetExchangeRatesByDate(ExchangeRateDate, this.selectedOffice).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.exchangeRateList = [];

          response.data.forEach(element => {
            this.exchangeRateList.push({
              FromCurrencyId: element.FromCurrency,
              ToCurrencyId: element.ToCurrency,
              FromCurrency: this.currencyList.filter(x => x.CurrencyId === element.FromCurrency)[0].CurrencyName,
              ToCurrency: this.currencyList.filter(x => x.CurrencyId === element.ToCurrency)[0].CurrencyName,
              Rate: element.Rate
            });
          });

          this.isVerified = response.IsExchangeRateVerified;
        }
      },
      error => {
      }
    );
  }

  saveExchangeRatesForOffice(saveForAllOffice) {
    this.exchangeRateLoader = true;
    const officeExchangeRateModels: IOfficeExchangeRateModels = {
    OfficeId: 0,
    GenerateExchangeRateModel: [],
    SaveForAllOffices: false
    };

    if (this.exchangeRateList != null && this.exchangeRateList !== undefined) {
       this.exchangeRateList.forEach(x =>
        officeExchangeRateModels.GenerateExchangeRateModel.push({
          FromCurrencyId: x.FromCurrencyId,
          ToCurrencyId: x.ToCurrencyId,
          Rate: x.Rate,
          Date: this.exchangeRateService.getLocalDate(this.exchangeRateItem.ExchangeRateDate)
        }));

        officeExchangeRateModels.SaveForAllOffices = saveForAllOffice;
        officeExchangeRateModels.OfficeId = this.selectedOffice;

      this.exchangeRateService.SaveExchangeRatesForAllOffices(officeExchangeRateModels).subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            this.exchangeRateLoader = false;
            this.toastr.success('Exchange Rates Added successfully');
          } else {
            this.toastr.error(response.message);
          }
          this.exchangeRateLoader = false;
        },
        (error) => {
          this.toastr.error('Someting went wrong');
          this.exchangeRateLoader = false;
        }
      );
    }
  }

  verifyExchangeRate(): void {
    const dialogRef = this.dialog.open(VerifyExchangeRateComponent, {
      width: '400px',
      data: {data: '' },
      autoFocus: false
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.verifyExchangeRates();
      }
    });
  }

  verifyExchangeRates() {
    this.verifyExchangeRateLoader = true;
    this.exchangeRateItem.ExchangeRateDate = this.exchangeRateService.getLocalDate(this.exchangeRateItem.ExchangeRateDate);

    this.exchangeRateService.VerifyExchangeRates(this.exchangeRateItem.ExchangeRateDate).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.exchangeRateListRefresh();
          this.isVerified = true;
          this.verifyExchangeRateLoader = false;
          this.toastr.success(response.message);
        } else {
          this.verifyExchangeRateLoader = false;
          this.toastr.error('Something went wrong!!');
        }
      },
      error => {
        this.verifyExchangeRateLoader = false;
        this.toastr.error('Something went wrong!!');
      }
    );
  }

  //#region "onListRefresh"
  exchangeRateListRefresh() {
    this.listRefresh.emit(this.exchangeRateItem.ExchangeRateVerificationId);
  }
  //#endregion

}
