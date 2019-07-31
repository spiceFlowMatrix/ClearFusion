import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { map } from 'rxjs/internal/operators/map';
import {
  IPlanningActivityDetail,
  IProjectAdvanceFilterModel,
  IAddProjectSubActivityModel,
  IEditProjectSubActivityModel,
  IProjectPermissionMode
} from '../models/project-activities.model';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';

export class ProjectRecurringTypeModel {
  Id: number;
  Name: string;
}

export interface IProjectPhasesModel {
  Id: number;
  Name: string;
}

const recurringTypeList: ProjectRecurringTypeModel[] = [
  { Id: 1, Name: 'Daily' },
  { Id: 2, Name: 'Weekly' },
  { Id: 3, Name: 'Monthly' },
  { Id: 4, Name: 'Yearly' },
  { Id: 5, Name: 'Quarterly' }
];

const projectphasesList: IProjectPhasesModel[] = [
  { Id: 1, Name: 'Planning' },
  { Id: 2, Name: 'Implementation' },
  { Id: 3, Name: 'Completed' }
];


@Injectable()
export class ProjectActivitiesService {

  public activityPermissionSubject = new BehaviorSubject<IProjectPermissionMode[]>([]);

  //#endregion
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "GetVoucherList"
  GetVoucherList(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_VoucherTransaction_GetAllVoucherList,
      data
    );
  }
  //#endregion

  //#region "AddProjectActivity"
  AddProjectActivity(data: IPlanningActivityDetail) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddProjectActivityDetail,
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

  //#region "EditProjectActivity"
  EditProjectActivity(data: IPlanningActivityDetail) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EditProjectActivityDetail,
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

  //#region "DeleteProjectActivityDocument"
  DeleteProjectActivityDocument(activityDocumentId: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DeleteActivityDocument,
        activityDocumentId
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

  //#region "StartProjectActivity"
  StartProjectActivity(activityId: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_StartProjectSubActivity,
        activityId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityDetail,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "EndProjectActivity"
  EndProjectActivity(activityId: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EndProjectSubActivity,
        activityId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityDetail,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "MarkImplementationAsCompleted"
  MarkImplementationAsCompleted(activityId: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_MarkImplementationAsCompleted,
        activityId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityDetails,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "MarkMonitoringAsCompleted"
  MarkMonitoringAsCompleted(activityId: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_MarkMonitoringAsCompleted,
        activityId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityDetails,
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
      .getListById(
        this.appurl.getApiUrl() +
          GLOBAL.API_VoucherTransaction_GetVoucherDetailByVoucherNo,
        id
      )
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
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllJournalDetail)
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

  //#region "GetBudgetLineList"
  GetBudgetLineList(projectId: number): any {
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectBudgetLineDetail,
        projectId
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

  //#region "GetAllEmployeeList"
  GetAllEmployeeList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllEmployeeList)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.EmployeeDetailListData,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetProjectRecurringTypeList"
  GetProjectRecurringTypeList() {
    return recurringTypeList;
  }
  //#endregion

  //#region "GetAllProjectPhases"
  GetAllProjectPhases() {
    return projectphasesList;
  }
  //#endregion

  //#region "GetAllProjectActivityList"
  GetAllProjectActivityList(projectId: number): any {
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectActivityDetail,
        projectId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetAllProjectActivityList"
  GetAllProjectActivityListByActivityId(projectId: number): any {
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetProjectActivityDetailByActivityId,
        projectId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityDetails,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetAllProjectActivityList"
  GetAllProjectSubActivityDetail(projectId: number): any {
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetProjectSubActivityDetail,
        projectId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectSubActivityListModel,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetAllProjectActivityStatus"
  GetAllProjectActivityStatus(projectId: number): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AllActivityStatus,
        projectId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityStatusModel,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "getAllProvinceList"
  getAllProvinceList() {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProvinceDetails
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProvinceDetailsList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  GetAllDistrictvalueByProvinceId(id) {
    return this.globalService
      .getListByListId(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetAllDistrictvalueByProvinceId,
        id
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.Districtlist,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }

  //#region "DeleteProjectActivity"
  DeleteProjectActivity(activityId: number): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DeleteActivityDetail,
        activityId
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

  //#region "UploadProjectActivityDocument"
  UploadProjectActivityDocument(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_UploadProjectDocumnentFile,
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

  //#region "UploadDocument"
  UploadDocument(data: any) {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_Project_UploadFileDemo, data)
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

  //#region "GetActivityDocumentList"
  GetActivityDocumentList(activityId: number): any {
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetActivityDocumentDetail,
        activityId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ActivityDocumentDetailModel,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetSignedUrl"
  GetSignedUrl(objectName: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DownloadFileFromBucket,
        objectName
      )
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

  //#region "GetProjectActivityAdvanceFilterList"
  GetProjectActivityAdvanceFilterList(data: IProjectAdvanceFilterModel): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetProjectActivityAdvanceFilterList,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityList,
            total: x.data.TotalCount,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetSignedUrl"
  getProjectMonitoringList(activityId: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectMonitoringList,
        activityId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectMonitoring,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetSignedUrl"
  getProjectMonitoringByMonitoringId(Id: number): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetProjectMonitoringByMonitoringId,
        Id
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectMonitoringModel,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "AddProjectSubActivity"
  AddProjectSubActivity(data: IAddProjectSubActivityModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddProjectSubActivityDetail,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityModel,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "EditProjectSubActivity"
  EditProjectSubActivity(data: IEditProjectSubActivityModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_EditProjectSubActivityDetail,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityDetail,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetAllExtensionList"
  GetAllExtensionList(data: any): any {
    return this.globalService
      .getListById(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetProjectActivityExtension,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityExtensionsList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "IsCompleteProject subactivity"
  ProjectSubActivityIsComplete(id: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_ProjectSubActivityIscomplete,
        id
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityDetail,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion
  //#region "IsCompleteProject subactivity"
  StartProjectSubActivity(id: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_StartProjectSubActivity,
        id
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
  //#region "IsCompleteProject subactivity"
  EndProjectSubActivity(id: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Project_EndProjectSubActivity,
        id
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
  //#region "DeleteProjectActivityExtension"
  DeleteProjectActivityExtension(id: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_DeleteProjectActivityExtension,
        id
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

  //#region "AddProjectActivityExtension"
  AddProjectActivityExtension(data: IPlanningActivityDetail) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddProjectActivityExtension,
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

  //#region "GetActivityDocumentDetail"
  GetActivityDocumentDetail(data: any) {
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

  //#region "GetActivitiesControlPermission"
  GetActivitiesControlPermission(data: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_GetActivitiesControlPermission,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectActivityPermissionList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "UploadDocument"
  AddMessage(data: any) {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_Chat_AddMessage, data)
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

  setActivityPermissions(permissionList: IProjectPermissionMode[]) {
    this.activityPermissionSubject.next(permissionList);
  }
}
