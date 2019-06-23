import { Component, OnInit } from '@angular/core';
import { CodeService } from '../code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-exchange-rate',
  templateUrl: './exchange-rate.component.html',
  styleUrls: ['./exchange-rate.component.css']
})
export class ExchangeRateComponent implements OnInit {
  exchangeRateDetails: any[];
  currencycodedt: any[];
  editingFlag = false;
  addExchangeRate = false;
  windows: any;
  officeDropdown: any[];
  isEditingAllowed = false;
  exchangeRateForm: any;

  // loader
  exchangeRateLoading = false;

  constructor(
    private codeService: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonservice: CommonService
  ) {
    this.windows = window;
    this.exchangeRateDetails = [];
  }

  ngOnInit() {
    this.exchangeRateForm = {
      Date: '',
      FromCurrency: 0,
      ToCurrency: 0,
      Rate: 0,
      OfficeId: 0
    };
    this.GetAllExchangeRate();
    this.getCurrencyCodeList();
    this.getOfficeCodeList();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.ExchangeRate
    );
  }

  GetAllExchangeRate() {
    this.showHideExchangeRateLoading(true);
    this.codeService
      .GetAllExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_ExchangeRate_GetExchangeRate
      )
      .subscribe(
        data => {
          this.exchangeRateDetails = [];
          if (data.StatusCode === 200 && data.data.ExchangeRateList.length > 0) {
            data.data.ExchangeRateList.forEach(element => {
              this.exchangeRateDetails.push(element);
            });
          }
          this.showHideExchangeRateLoading(false);
        },
        error => {
          this.showHideExchangeRateLoading(false);
        }
      );
  }

  //#region "getOfficeCodeList"
  getOfficeCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          if (data.data.OfficeDetailsList != null) {
            this.officeDropdown = [];

            const officeIds: any[] =
              localStorage.getItem('ALLOFFICES') != null
                ? localStorage.getItem('ALLOFFICES').split(',')
                : null;

            data.data.OfficeDetailsList.forEach(element => {
              this.officeDropdown.push(element);
            });
          }
        },
        error => {}
      );
  }
  //#endregion

  logEvent(eventName, obj) {
    if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      this.EditExchangeRate(value);
    } else if (eventName === 'RowInserting') {
      this.AddExchangeRate(obj.data);
    }
    if (eventName === 'InitNewRow') {
      // this.editingFlag = true;
    }

    if (eventName === 'EditingStart') {
    }
  }

  AddExchangeRate(data) {
    this.exchangeRateLoading = true;
    console.log(data.Date);

    const exchangeRateData: any = {
      ExchangeRateId: 0,
      Date: new Date(
        new Date(data.Date).getFullYear(),
        new Date(data.Date).getMonth(),
        new Date(data.Date).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      FromCurrency: data.FromCurrency,
      ToCurrency: data.ToCurrency,
      Rate: data.Rate,
      OfficeId: data.OfficeId
    };

    this.codeService
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_ExchangeRate_AddExchangeRate,
        exchangeRateData
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.exchangeRateLoading = false;
            this.addExchangeRate = false;
            this.toastr.success('Exchange Rate Added Successfully!!!');
          }

          this.GetAllExchangeRate();
        },
        error => {
          this.exchangeRateLoading = false;
        }
      );
  }

  EditExchangeRate(obj) {
    this.exchangeRateLoading = true;
    this.codeService
      .EditExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_ExchangeRate_EditExchangeRate,
        obj
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.exchangeRateLoading = false;
            this.toastr.success('Exchange Rate Updated Successfully!!!');
          }
        },
        error => {
          this.exchangeRateLoading = false;
          this.GetAllExchangeRate();
        }
      );
  }

  getCurrencyCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencycodedt = [];
          data.data.CurrencyList.forEach(element => {
            this.currencycodedt.push({
              FromCurrency: element.CurrencyId,
              ToCurrency: element.CurrencyId,
              CurrencyCode: element.CurrencyCode,
              CurrencyName: element.CurrencyName,
              CurrencyRate: element.CurrencyRate
            });
          });
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

  onExchangeRateAdd(data) {
    const exchangeRateData: any = {
      ExchangeRateId: 0,
      Date: data.Date,
      FromCurrency: data.FromCurrency,
      ToCurrency: data.ToCurrency,
      Rate: data.Rate,
      OfficeId: data.OfficeId
    };

    this.AddExchangeRate(exchangeRateData);
  }

  HidePopup() {
    this.addExchangeRate = false;
  }

  onShowAddExchangeRateForm() {
    this.exchangeRateForm = {
      Date: new Date(),
      FromCurrency: 0,
      ToCurrency: 0,
      Rate: 0,
      OfficeId: 0
    };
    this.addExchangeRate = true;
  }

  showHideExchangeRateLoading(flag: boolean) {
    this.exchangeRateLoading = flag;
  }
}
