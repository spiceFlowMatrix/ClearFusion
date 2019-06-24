import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from '../../accounting/vouchers/models/status-code.model';
import { IProjectCashFlow, IBudgetLineBreakdownFlowModel } from './project-cash-flow.models';


@Injectable({
  providedIn: 'root'
})
export class ProjectCashFlowService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService) { }

    GetAllProjectList(): any {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectList
      )
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


  GetAllDonorList(): any {
    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllDonorList)
      .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.DonorDetail,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }

  FilterProjectCashFlow(data: IProjectCashFlow): any{
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_Project_FilterProjectCashFlow,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.ProjectCashFlowModel,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }

  //#region "FilterBudgetLineBreakdown"
  FilterBudgetLineBreakdown(data: IBudgetLineBreakdownFlowModel): any {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_Project_FilterBudgetLineBreakdown,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.BudgetLineBreakdownModel,
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

  //  //#region "GetProjectBudgetLineList"
  //  GetAllExpenditureList(projecId: number): any {
  //   return this.globalService
  //     .getListById(
  //       this.appurl.getApiUrl() +
  //         GLOBAL.API_Project_GetAllExpenditureByProjectId,
  //       projecId
  //     )
  //     .pipe(
  //       map(x => {
  //         const responseData: IResponseData = {
  //           data: x.data.BudgetLineCashFlowModelList,
  //           statusCode: x.StatusCode,
  //           message: x.Message
  //         };
  //         return responseData;
  //       })
  //     );
  // }
  // //#endregion
}
