import { Injectable } from '@angular/core';
import { GlobalService } from '../../../../shared/services/global-services.service';
import { Observable } from 'rxjs';
import { ActivityTypeModel } from '../model/mastrer-pages.model';
import { GLOBAL } from 'src/app/shared/global';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { map } from 'rxjs/internal/operators/map';
import { AppUrlService } from 'src/app/shared/services/app-url.service';

@Injectable({
  providedIn: 'root'
})
export class MasterPageServiceService {

  constructor(private appurl: AppUrlService, private globalService: GlobalService) { }

  Add(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }

  Post(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }

  Delete(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }

  GetById(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }

  GetList(url): Observable<any> {
    return this.globalService.getList(url);
  }

  PaginatedList(url: string, data) {
   return this.globalService.post(url, data);
 }

 GetChannelList(): Observable<any> {
  return this.globalService
          .getList(this.appurl.getApiUrl() + GLOBAL.API_Scheduler_GetChannel)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.ChannelList,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
}

GetMediumList(): Observable<any> {
 return this.globalService
         .getList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMedium)
         .pipe(
           map(x => {
             const responseData: IResponseData = {
               data: x.data.Mediums,
               statusCode: x.StatusCode,
               message: x.Message
             };
             return responseData;
           })
         );
}

GetChannelById(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Scheduler_GetChannelById, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.channelById,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
 }

 AddChannel(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Scheduler_AddChannel, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.channelById,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
 }

 DeleteChannel(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Scheduler_DeleteChannel, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.channelById,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
 }

 GetActivityTypeList(): Observable<any> {
  return this.globalService
          .getList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetActivityType)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.ActivityTypes,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
 }

 GetActivityById(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetActivityById, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.activityById,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
 }

 AddActivity(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddActivityType, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.activityById,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
 }

 DeleteActivity(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddActivityType, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.activityById,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
 }

 GetUnitRateList(): Observable<any> {
  return this.globalService
          .getList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetUnitRateList)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.UnitRateDetails,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
 }

 GetUnitRatePaginatedList(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetUnitRatePaginatedList, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.UnitRates,
                statusCode: x.StatusCode,
                message: x.Message,
                total: x.data.TotalCount
              };
              return responseData;
            })
          );
 }

 GetMediaCategoryList(): Observable<any> {
  return this.globalService
          .getList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMediaCategory)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.MediaCategories,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
 }

 GetMediaCategoryById(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMediaCategoryById, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.mediaCategoryById,
                statusCode: x.StatusCode,
                message: x.Message,
                total: x.data.TotalCount
              };
              return responseData;
            })
          );
 }

 AddMediaCategory(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddMediaCategory, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.mediaCategoryById,
                statusCode: x.StatusCode,
                message: x.Message,
                total: x.data.TotalCount
              };
              return responseData;
            })
          );
 }

 DeleteMediaCategory(data): Observable<any> {
  return this.globalService
          .post(this.appurl.getApiUrl() + GLOBAL.API_Contract_DeleteMediaCategory, data)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.mediaCategoryById,
                statusCode: x.StatusCode,
                message: x.Message,
                total: x.data.TotalCount
              };
              return responseData;
            })
          );
 }

}
