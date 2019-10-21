import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { map } from 'rxjs/internal/operators/map';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { IProposalReportFilter } from './models/proposal-report.model';
import { Observable } from 'rxjs/internal/Observable';
import { IResponseData } from '../../accounting/vouchers/models/status-code.model';

@Injectable({
  providedIn: 'root'
})
export class ProposalReportService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "GetProjectProposalReport"
  GetProjectProposalReport(data: IProposalReportFilter) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectProposalReport,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectProposalReportList,
            total: x.data.TotalCount,
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

  //#region "GetAmountSummaryList"
  GetAmountSummaryList(data: IProposalReportFilter): Observable<any> {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectProposalAmountSummary, data)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectProposalAmountSummary,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  setDateTime(data): any {
    return new Date(
      new Date(data).getFullYear(),
      new Date(data).getMonth(),
      new Date(data).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
  }

  getDateTime(data): any {
    return new Date(
      new Date(data).getTime() - new Date().getTimezoneOffset() * 60000
    );
  }
}
