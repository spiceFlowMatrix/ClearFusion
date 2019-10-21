import { Injectable } from '@angular/core';
import { GlobalService } from '../../../../shared/services/global-services.service';
import { MarketingJobDetailModel, JobFilterModel, JobPaginationModel, FilterJobModel } from '../model/marketing-jobs.model';
import { Observable } from 'rxjs/internal/Observable';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { map } from 'rxjs/internal/operators/map';
@Injectable({
  providedIn: 'root'
})
export class MarketingJobsService {

  constructor(private appurl: AppUrlService, private globalService: GlobalService) {}


  GetFilteredList(url: string, data: JobFilterModel) {
    return this.globalService.post(url, data);
  }

  GetFilteredJobList(data: FilterJobModel) {
    return this.globalService
        .post(this.appurl.getApiUrl() + GLOBAL.API_Job_GetFilteredJobList, data)
        .pipe(
          map(x => {
            const responseData: IResponseData = {
              data: x.data.JobPriceDetailList,
              statusCode: x.StatusCode,
              message: x.Message,
              total: x.data.jobListTotalCount
            };
            return responseData;
          })
        );
  }

  AddJobDetail(data: MarketingJobDetailModel) {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Job_AddJob, data)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.JobPriceDetail,
          statusCode: x.StatusCode,
          message: x.Message,
          total: x.data.jobListTotalCount
        };
        return responseData;
      })
    );
  }

  RemoveInvoice(data) {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Job_RemoveInvoice, data)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.invoiceDetails,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }

  ApproveInvoice(data) {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Job_ApproveInvoice, data)
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

  GenerateInvoice(data) {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Job_GenerateInvoice, data)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.invoiceDetails,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }

  FetchInvoice(data) {
    return this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_Job_FetchInvoice, data)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.invoiceDetails,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }


  Post(url: string, data) {
    return this.globalService.post(url, data);
  }

  Get(url): Observable<any> {
    return  this.globalService.getList(url);
  }

  GetJobList(): Observable<any> {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Job_GetJobsList)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.JobDetailsModel,
            statusCode: x.StatusCode,
            message: x.Message,
            total: x.data.jobListTotalCount
          };
          return responseData;
        })
      );
    }

    GetContractListByClient(): Observable<any> {
      return this.globalService
        .getList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetContractListByClient)
        .pipe(
          map(x => {
            const responseData: IResponseData = {
              data: x.data.ContractByClientList,
              statusCode: x.StatusCode,
              message: x.Message
            };
            return responseData;
          })
        );
      }

      GetContractList(): Observable<any> {
        return this.globalService
          .getList(this.appurl.getApiUrl() + GLOBAL.API_Job_GetContractList)
          .pipe(
            map(x => {
              const responseData: IResponseData = {
                data: x.data.ContractDetails,
                statusCode: x.StatusCode,
                message: x.Message
              };
              return responseData;
            })
          );
        }

        GetPhaseList(): Observable<any> {
          return this.globalService
            .getList(this.appurl.getApiUrl() + GLOBAL.API_Job_GetPhaseList)
            .pipe(
              map(x => {
                const responseData: IResponseData = {
                  data: x.data.JobPhases,
                  statusCode: x.StatusCode,
                  message: x.Message
                };
                return responseData;
              })
            );
          }

      PaginatedJobList(data): Observable<any> {
        return this.globalService
        .post(this.appurl.getApiUrl() + GLOBAL.API_Job_GetJobsPaginatedList, data)
        .pipe(
          map(x => {
            const responseData: IResponseData = {
              data: x.data.JobDetailsModel,
              statusCode: x.StatusCode,
              message: x.Message,
              total: x.data.TotalCount
            };
            return responseData;
          })
        );
      }

      ApproveJob(data) : Observable<any> {
        return this.globalService
        .post(this.appurl.getApiUrl() + GLOBAL.API_Job_ApproveJob, data)
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

      DeleteJob(data) : Observable<any> {
        return this.globalService
        .post(this.appurl.getApiUrl() + GLOBAL.API_Job_DeleteJob, data)
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

      GetJobById(data) : Observable<any> {
        return this.globalService
        .post(this.appurl.getApiUrl() + GLOBAL.API_Job_GetJobById, data)
        .pipe(
          map(x => {
            const responseData: IResponseData = {
              data: x.data.JobPriceDetail,
              statusCode: x.StatusCode,
              message: x.Message
            };
            return responseData;
          })
        );
      }

      AcceptAgreement(data) : Observable<any> {
        return this.globalService
        .post(this.appurl.getApiUrl() + GLOBAL.API_Job_AcceptAgreement, data)
        .pipe(
          map(x => {
            const responseData: IResponseData = {
              data: x.data.JobDetailModel,
              statusCode: x.StatusCode,
              message: x.Message
            };
            return responseData;
          })
        );
      }

      GetContractByClient(data) : Observable<any> {
        return this.globalService
        .post(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetContractByClientId, data)
        .pipe(
          map(x => {
            const responseData: IResponseData = {
              data: x.data.ContractByClientList,
              statusCode: x.StatusCode,
              message: x.Message
            };
            return responseData;
          })
        );
      }

}
