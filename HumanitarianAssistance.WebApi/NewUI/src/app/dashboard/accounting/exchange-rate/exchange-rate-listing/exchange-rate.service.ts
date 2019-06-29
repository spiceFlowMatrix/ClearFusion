import { Injectable } from '@angular/core';
import { GlobalService } from '../../../../shared/services/global-services.service';
import { AppUrlService } from '../../../../shared/services/app-url.service';
import { GLOBAL } from '../../../../shared/global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from '../../vouchers/models/status-code.model';
import { IExchangeRateFilterModel, IExchangeRateAddModel, IOfficeExchangeRateModels } from '../models/exchange-rate.model';

@Injectable()
export class ExchangeRateService {
  //#endregion
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

//#region "GetExchangeRateList"
    GetExchangeRateList(data: IExchangeRateFilterModel) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_GetAllVoucherList,
      data
    );
  }
  //#endregion

  //#region "Add Exchange Rate"
  AddExchangeRate(data: any[]) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ExchangeRates_SaveSystemGeneratedExchangeRates,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "Add Exchange Rate"
  SaveExchangeRatesForAllOffices(data: IOfficeExchangeRateModels) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ExchangeRates_SaveExchangeRatesForAllOffices,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetCurrencyList"
  GetCurrencyList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.CurrencyList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetOfficeList"
  GetOfficeList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.OfficeDetailsList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetSavedExchangeRates"
  GetSavedExchangeRates( data: any): any {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_ExchangeRates_GetSavedExchangeRates,
      data
    ).pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ExchangeRateVerificationList,
            statusCode: x.StatusCode,
            message: x.Message,
            total: x.data.TotalExchangeRateCount
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "Get Saved ExchangeRates"
  GetExchangeRatesByDate(date: any, officeId: number) {

    const data = {
      ExchangeRateDate: this.getLocalDate(date),
      OfficeId: officeId
    };

    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ExchangeRates_GetExchangeRatesDetail,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ExchangeRateDetailViewModelList,
            statusCode: x.StatusCode,
            message: x.Message,
            IsExchangeRateVerified: x.data.IsExchangeRateVerified
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "Verify ExchangeRates"
  VerifyExchangeRates(ExchangeRateDate: any) {

    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ExchangeRates_VerifyExchangeRates,
        ExchangeRateDate
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: '',
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "Delete ExchangeRates"
  DeleteExchangeRates(ExchangeRateDate: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ExchangeRates_DeleteExchangeRates,
        ExchangeRateDate
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: '',
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "getLocalDate"
  getLocalDate(date: any) {
    return new Date(
      new Date(date).getFullYear(),
      new Date(date).getMonth(),
      new Date(date).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds(),
      new Date().getMilliseconds(),
    );
  }
  //#endregion
}
