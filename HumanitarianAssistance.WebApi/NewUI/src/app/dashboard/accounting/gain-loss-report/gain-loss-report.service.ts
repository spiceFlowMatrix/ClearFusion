import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from '../vouchers/models/status-code.model';
import { IGainLossAddVoucherForm } from './gain-loss-report.model';

@Injectable()
export class GainLossReportService {
  //#endregion
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "GetGainLossReportList"
  GetGainLossReportList(data: any) {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_FinancialReport_GetExchangeGainLossReport, data)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ExchangeGainLossReportList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetInputLevelAccountList"
  GetInputLevelAccountList() {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Account_GetAllInputLevelAccountCode)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.AccountDetailList,
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

  //#region "GetJournalList"
  GetJournalList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllJournalDetail)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.JournalDetailList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetProjectList"
  GetProjectList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectList)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectDetailModel,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "deleteAccountFromFilter"
  deleteAccountFromFilter(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectList)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: null,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetGainLossReportList"
  SaveGainLossAccountList(data: any) {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_FinancialReport_SaveGainLossAccountList, data)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ExchangeGainLossReportList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetJournalList"
  GetExchangeGainLossFilterAccountList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_GainLossReport_GetExchangeGainLossFilterAccountList)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.GainLossSelectedAccounts,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetVoucherTypeList"
  GetVoucherTypeList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Account_GetAllVoucherType)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.VoucherTypeList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "AddGainLossVoucher"
  AddGainLossVoucher(data: IGainLossAddVoucherForm) {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_GainLossReport_AddExchangeGainLossVoucher, data)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.GainLossVoucherDetail,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetGainLossVoucherList"
  GetGainLossVoucherList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_GainLossReport_GetExchangeGainLossVoucherList)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.GainLossVoucherList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "DeleteGainLossVoucher"
  DeleteGainLossVoucher(data: number) {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_GainLossReport_DeleteGainLossVoucherTransaction, data)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: null,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

}
