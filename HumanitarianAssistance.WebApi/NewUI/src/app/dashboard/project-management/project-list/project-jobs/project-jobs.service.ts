import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { IProjectJobModel } from './project-jobsmodel';
import { GLOBAL } from 'src/app/shared/global';
import { IResponseData } from '../../../accounting/vouchers/models/status-code.model';
import { map } from 'rxjs/internal/operators/map';
import { Observable } from 'rxjs';

@Injectable({

  providedIn: 'root'
})
export class ProjectJobsService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,

  ) { }



  //#region "GetProjectJobList"
  GetProjectJobList(): any {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_BudgetLine_GetProjectJobDetail
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

  AddProjectJobsDetail(data: IProjectJobModel) {
     return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddProjectJobsDetail,
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

  //#region "EditVoucherDetailById"
  EditProjectJobsDetailById(data: IProjectJobModel): any {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_Project_EditProjectJobsDetail, data)
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

   //#region "GetProjectJobsDetailByProjectJobId"
   GetProjectJobDetailByProjectJobId(projectJobId:number) :any{
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectJobDetailByProjectJobId, projectJobId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectJobDetailModel,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetProjectJobDetailByBudgetLineId"
  GetProjectJobDetailByBudgetLineId(budgetLineId:number) :any{
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectJobDetailByBudgetLineId, budgetLineId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectJobModel,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  Delete(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }
}
