import { Injectable } from '@angular/core';
import { IResponseData } from '../../../accounting/vouchers/models/status-code.model';
import { GlobalService } from '../../../../shared/services/global-services.service';
import { AppUrlService } from '../../../../shared/services/app-url.service';
import { GLOBAL } from '../../../../shared/global';
import { map } from 'rxjs/internal/operators/map';
import {
  IBudgetLineModel,
  ITransactionDetailModel,
  ITransactionModel,
  IBudgetListFilterModel
} from './models/budget-line.models';

@Injectable({
  providedIn: 'root'
})
export class BudgetLineService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}
  //#region "GetVoucherList"
  GetVoucherList(data: IBudgetLineModel) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_GetAllVoucherList,
      data
    );
  }
  //#endregion

  //#region "AddbudgetLineDetailList"
  AddBudgetLineDetail(data: IBudgetLineModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_BudgetLine_AddBudgetLineDetail,
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

  //#region "GetProjectJobList"
  GetProjectJobList(selectedProjectId: number): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_BudgetLine_GetProjectJobDetailByProjectId,
        selectedProjectId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectJobDetail,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetProjectBudgetLineList"
  GetProjectBudgetLineList(projecId: number): any {
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() +
          GLOBAL.API_BudgetLine_GetProjectBudgetLineDetail,
        projecId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectBudgetLineDetailList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion
  //#region "GetProjectBudgetLineList"
  GetProjectBudgetdetailList(data: IBudgetListFilterModel): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_BudgetLine_GetAllBudgetLineList,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectBudgetLineList,
            total: x.data.TotalCount,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetBudgetDetailByBudgetIdId"
  GetBudgetDetailByBudgetIdId(budgetId: number): any {
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() +
          GLOBAL.API_BudgetLine_GetBudgetLineDetailByBudgetId,
        budgetId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectBudgetLineDetailByBudgetId,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "EditVoucherDetailById"
  EditBudgetLineDetailById(data: IBudgetLineModel): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_BudgetLine_AddBudgetLineDetail,
        data
      )
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

  //#region "GetTransationListByProjectId"
  GetTransationListByProjectId(projecId: number): any {
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() +
          GLOBAL.API_BudgetLine_GetTransactionListByProjectId,
        projecId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.TransactionBudgetModelList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetTransactionList"
  GetTransationList(data: ITransactionModel): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_BudgetLine_GetTransactionList,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.TransactionBudgetModelList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "postBudgetLineDocument"
  postBudgetLineDocument(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_BudgetLine_ExcelImportOfBudgetLine,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.activityDocumnentDetail,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

}
