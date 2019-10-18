import { Injectable } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { GLOBAL } from 'src/app/shared/global';
import { IResponseData } from '../../vouchers/models/status-code.model';
import { map } from 'rxjs/internal/operators/map';

@Injectable({
  providedIn: 'root'
})
export class IncomeExpenseService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

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

  //#region "GetDetailOfNotes"
  GetDetailOfNotes(data: any): any {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_FinancialReport_GetDetailOfNotes, data)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.DetailsOfNotesFinalList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion
}
