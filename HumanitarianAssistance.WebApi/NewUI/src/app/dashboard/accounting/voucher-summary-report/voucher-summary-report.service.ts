import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { map } from 'rxjs/internal/operators/map';
import { VoucherSummaryFilterModel } from './voucher-summary-model';
import { IResponseData } from '../vouchers/models/status-code.model';

@Injectable()
export class VoucherSummaryReportService {
  //#endregion
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "voucherSummaryReportList"
  voucherSummaryReportList(data: VoucherSummaryFilterModel) {
  return this.globalService
             .post(this.appurl.getApiUrl() + GLOBAL.API_AccountReports_GetVoucherSummaryReportList, data)
             .pipe(
              map(x => {
                const responseData: IResponseData = {
                  data: x.data.VoucherSummaryList,
                  statusCode: x.StatusCode,
                  message: x.Message,
                  total: x.data.TotalCount
                };
                return responseData;
            })
            );
          }
  //#endregion

   //#region "voucherSummaryReportList"
   voucherTransactionList(data: any) {
    return this.globalService
               .post(this.appurl.getApiUrl() + GLOBAL.API_AccountReports_GetVoucherTransactionList, data)
               .pipe(
                map(x => {
                  const responseData: IResponseData = {
                    data: x.data.VoucherSummaryTransactionList,
                    statusCode: x.StatusCode,
                    message: x.Message
                  };
                  return responseData;
              })
              );
            }
    //#endregion

  //#region "getProjectJobList"
  getProjectJobList(projectIds: number[]) {
    return this.globalService
               .post(this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectJobsByProjectIds, projectIds)
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

    //#region "getProjectJobList"
    getProjectBudgetLineList(projectJobIds: number[]) {
    return this.globalService
               .post(this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectBudgetLinesByProjectJobIds, projectJobIds)
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

    //#region "Demo"
    // getSignedURLDemo() {
    // return this.globalService
    //            .getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetSignedURL)
    //            .pipe(
    //             map(x => {
    //               const responseData: IResponseData = {
    //                 data: x.data.SignedUrl,
    //                 statusCode: x.StatusCode,
    //                 message: x.Message
    //               };
    //               return responseData;
    //           })
    //           );
    //         }
    //#endregion

    //#region "Demo"
    upload(url: string, data: any, options?: any) {
      return this.globalService
                 .put(url, data, options)
                 .pipe(
                  map(x => {
                    const responseData: IResponseData = {
                      data: x.data.SignedUrl,
                      statusCode: x.StatusCode,
                      message: x.Message
                    };
                    return responseData;
                })
                );
              }
      //#endregion
}
