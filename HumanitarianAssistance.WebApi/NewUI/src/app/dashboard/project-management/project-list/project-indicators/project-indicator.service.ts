import { Injectable } from "@angular/core";
import { GlobalService } from "src/app/shared/services/global-services.service";
import { AppUrlService } from "src/app/shared/services/app-url.service";
import {
  IProjectIndicatorModel,
  IQuestionDetailModel
} from "./project-indicators-model";
import { GLOBAL } from "src/app/shared/global";
import { IResponseData } from "src/app/dashboard/accounting/vouchers/models/status-code.model";
import { map } from "rxjs/operators";
import { registerContentQuery } from "@angular/core/src/render3";

@Injectable({
  providedIn: "root"
})
export class ProjectIndicatorService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "AddbudgetLineDetailList"
  AddIndicatorDetail(data: IProjectIndicatorModel) {
    debugger;
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddProjectIndicator,
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

  //#region "EditHiringRequestDetail"
  EditProjectIndicatorDetail(data: IProjectIndicatorModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EditProjectIndicator,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.ResponseData,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "EditProjectIndicatorQuestions"
  AddProjectIndicatorQuestions(model: IQuestionDetailModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddProjectIndicatorQuestions,
        model
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.ResponseData,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion
  //#region "GetIndicatorQuestionById"
  GetIndicatorQuestionById(id: number) {
    debugger
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetIndicatorQuestionDetailById,
        id
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.ResponseData,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion
//#region "DeleteQuestionDetail"
DeleteQuestionDetail(id: number) {
  return this.globalService
  .post(
    this.appurl.getApiUrl() +
      GLOBAL.API_Project_GetIndicatorQuestionDetailById,
    id
  )
  .pipe(
    map(x => {
      const responseData: IResponseData = {
        data: x.ResponseData,
        statusCode: x.StatusCode,
        message: x.Message
      };
      return responseData;
    })
  );
}
//#endregion
}
