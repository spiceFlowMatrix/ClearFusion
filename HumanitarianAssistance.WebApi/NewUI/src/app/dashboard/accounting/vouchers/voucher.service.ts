import { Injectable } from '@angular/core';
import { GlobalService } from '../../../shared/services/global-services.service';
import {
  IVoucherListFilterModel,
  IVoucherTypeListModel,
  IAddVoucherModel,
  IVoucherDetailModel,
  IEditTransactionModel,
  AddEditTransactionModel
} from './models/voucher.model';
import { AppUrlService } from '../../../shared/services/app-url.service';
import { GLOBAL } from '../../../shared/global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from './models/status-code.model';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Injectable()
export class VoucherService {
  //#endregion
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private globalSharedService: GlobalSharedService
  ) {}

  //#region "GetVoucherList"
  GetVoucherList(data) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_GetAllVoucherList,
      data
    );
  }
  //#endregion

  //#region "GetVoucherList"
  AddVoucher(data: IAddVoucherModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_AddVoucherDetail,
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

  //#region "GetVoucherTypeList"
  GetVoucherTypeList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Account_GetAllVoucherType)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.VoucherTypeList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetVoucherDetailById"
  GetVoucherDetailById(id: number): any {
      return this.globalService
        .getListById(this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_GetVoucherDetailByVoucherNo, id)
        .pipe(
          map(x => {
            const responseData: IResponseData = {
              data: x.data.VoucherDetail,
              statusCode: x.StatusCode,
              message: x.Message
            };
            return responseData;
          })
        );
    }
    //#endregion

  //#region "GetJournalList"
  GetJournalList(): any {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllJournalDetail
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.JournalDetailList,
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

 //#region "GetBudgetList"
 GetBudgetList(): any {
  return this.globalService
    .getList(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.OfficeDetailsList,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
}
//#endregion

  //#region "GetOfficeList"
  GetOfficeList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.OfficeDetailsList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetProjectList"
  GetProjectList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectList)
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
  //#endregion

  //#region "GetBudgetLineList"
  GetBudgetLineList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectBudgetLineDetail)
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


  //#region "GetBudgetLineList"
  GetProjectobList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectJobDetail)
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

  //#region "GetInputLevelAccountList"
  GetInputLevelAccountList(): any {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Account_GetAllInputLevelAccountCode
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.AccountDetailList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "EditVoucherDetailById"
  EditVoucherDetailById(data: IVoucherDetailModel): any {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_EditVoucherDetail, data)
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

  //#region "GetTransactionByVoucherId"
  GetTransactionByVoucherId(data): any {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_GetAllTransactionsByVoucherId, data)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.VoucherTransactions,
            statusCode: x.StatusCode,
            message: x.Message,
            total: x.data.TotalCount
          };
          return responseData;
        })
      );
  }
  //#endregion



  //#region "AddEditTransactionList"
  AddEditTransactionList(data: AddEditTransactionModel): any {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_AddEditTransactionList, data)
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

  //#region "VoucherVerify"
  VoucherVerify(data: number): any {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_VerifyVoucher, data)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.IsVoucherVerified,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

   //#region "GetVoucherDocumentList"
   GetVoucherDocumentDetail(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetActivityDocumentDetails,
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

   //#region "VerifySelectedVouchers"
   verifySelectedVouchers(data) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_VerifySelectedVouchers,
        data
      );
  }
  //#endregion

  //#region "deleteSelectedVouchers"
  deleteSelectedVouchers(data) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_DeleteSelectedVouchers,
        data
      );
  }
  //#endregion

  //#region "GetFilteredInputLevelAccountList"
  GetFilteredInputLevelAccountList(data): any {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Account_GetFilteredInputLevelAccountList + '?data=' + data
      );
  }
  //#endregion

  //#region "GetFilteredProjectList"
  GetFilteredProjectList(data): any {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Account_GetFilteredProjectList + '?data=' + data
      );
  }
  //#endregion

  //#region "getFilteredBudgetLineList"
  getFilteredBudgetLineList(data): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Account_GetFilteredBudgetLineList, data
      );
  }
  //#endregion

   //#region "getProjectJobDetailByBudgetLineId"
   getProjectJobDetailByBudgetLineId(id): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectJobDetailByBudgetLineId, id
      ).pipe(
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


  //#region "saveTransactions"
  saveTransactions(data): any {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_SaveTransactionList, data)
  }
  //#endregion

   //#region "exportPdf"
   exportPdf(data): any {
    this.globalSharedService
    .getFile(this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetAllVoucherSummaryReportPdf,
    data
    );
   }
  //#endregion
}
