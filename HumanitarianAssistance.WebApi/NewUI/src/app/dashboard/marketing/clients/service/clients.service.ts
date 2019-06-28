import { Injectable } from '@angular/core';
import { GlobalService } from '../../../../shared/services/global-services.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { map } from 'rxjs/internal/operators/map';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  constructor(private globalService: GlobalService,  private appurl: AppUrlService,) { }

  //  GetClientsList(url): Observable<any> {
  //   return  this.globalService.getList(url);
  //  }

   GetClientsList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Client_GetAllClient)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ClientDetails,
            total: x.data.jobListTotalCount,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }

  GetFilteredList(data): any {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Client_GetFilteredList, data)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.ClientDetails,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }

  DeleteClient(data): Observable<any> {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Client_DeleteClient, data)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: null,
          statusCode: x.StatusCode,
          message: x.Message,
          total: x.data.jobListTotalCount
        };
        return responseData;
      })
    );
  }

   PaginatedList(data) {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Client_GetClientsPaginatedList, data)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.ClientDetails,
          statusCode: x.StatusCode,
          message: x.Message,
          total: x.data.TotalCount
        };
        return responseData;
      })
    );
  }

   GetCategory(): Observable<any> {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Client_GetCategory)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.Categories,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
   }



   GetClientById(data): Observable<any> {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Client_GetClientDetails, data)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.clientDetailsById,
          statusCode: x.StatusCode,
          message: x.Message,
        };
        return responseData;
      })
    );
  }

  EditClient(data): Observable<any> {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Client_AddEditClient, data)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.clientDetailsById,
          statusCode: x.StatusCode,
          message: x.Message,
        };
        return responseData;
      })
    );
  }
}
