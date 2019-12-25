import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { map } from 'rxjs/operators';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import {
  IHiringReuestCandidateModel,
  IReuestedCandidateDetailModel,
  IitervireCandidateModel,
  ISelectedCandidateModel,
  CandidateDetailModel,
  IFilterModel} from './models/hiring-requests-model';
import { BehaviorSubject } from 'rxjs';
import { IProjectPermissionMode } from '../project-activities/models/project-activities.model';
import {
  IHiringRequestModel,
  CompleteHiringRequestModel,
  ICandidateDetailModel,
  ICandidateFilterModel,
  InterviewDetailModel
} from '../../project-hiring/models/hiring-requests-models';

@Injectable({
  providedIn: 'root'
})
export class HiringRequestsService {
  public hiringPermissionSubject = new BehaviorSubject<
    IProjectPermissionMode[]
  >([]);

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  setHiringPermissions(permissionList: IProjectPermissionMode[]) {
    this.hiringPermissionSubject.next(permissionList);
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

  //#region "GetProfessionList"
  GetProfessionList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllProfession)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProfessionList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }

  //#region "GetEducationDegreeList"
  GetEducationDegreeList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllEducationDegreeList
    );
  }
  //#region "GetJobGradeList"
  GetJobGradeList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_HREmployee_GetAllJobGrade)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.JobGradeList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "getAllProvinceListByCountryId"
  getAllProvinceListByCountryId(Id: any) {
    return this.globalService
      .getListByListId(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetAllProvinceDetailsByCountryId,
        Id
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
  //#region "GetAllDistrictvalueByProvinceId"
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
  //#endregion

  //#region "AddHiringRequestDetail"
  AddHiringRequestDetail(data: IHiringRequestModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_AddHiringRequestDetail,
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
  EditHiringRequestDetail(data: IHiringRequestModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_EditHiringRequestDetail,
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

  //#region "AddNewCandidateDetail"
  AddNewCandidateDetail(data: ICandidateDetailModel) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_AddNewCandidateDetail,
      data
    );
  }
  //#endregion

  //#region "AddExistingCandidateDetail"
  AddExistingCandidateDetail(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_AddExistingCandidateDetail,
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

  //#region "getAllCandidateList"
  getAllCandidateList(data: ICandidateFilterModel): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_GetAllCandidateList,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.CandidateList,
            total: x.data.TotalCount,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetAllExistingCandidateList"
  GetAllExistingCandidateList(data: ICandidateFilterModel): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetAllExistingCandidateList,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ExistingCandidateList,
            total: x.data.TotalCount,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "UpdateCandidateStatus"
  UpdateCandidateStatus(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_UpdateCandidateStatusByStatusId,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.CandidateStatus,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "EditCandidateDetail"
  EditCandidateDetail(data: CandidateDetailModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HREmployee_EditEmployeeProfessionalDetail,
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

  //#region "GetProjectActivityAdvanceFilterList"
  GetProjectHiringRequestFilterList(data: IFilterModel): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetProjectHiringRequestDetail,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectHiringRequestModel,
            total: x.data.TotalCount,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetAllEmployeeList"
  GetAllEmployeeList(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_GetAllEmployeeList,
        model
      )
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
  //#region "GetEmployeeListByOfficeId"
  GetEmployeeListByOfficeId(OfficeId: number) {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetEmployeeListByOfficeId +
          '?OfficeId=' +
          OfficeId
      )
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
  //#region "GetAllEmployeeList"
  GetAllAttendanceGroupList(): any {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Code_GetAttendanceGroupst)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.AttendanceGroupMasterList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion
  //#region "GetAllEmloyeeContractList"
  GetAllEmloyeeContractList(): any {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() +
          GLOBAL.API_HREmployee_GetAllEmployeeContractType
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.EmployeeContractTypeList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "AddbudgetLineDetailList"
  AddHiringRequestCandidate(data: IHiringReuestCandidateModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_AddHiringRequestCandidate,
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

  EditHirinigRequestCandidateDEtail(data: IHiringReuestCandidateModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_EditHiringRequestCandidate,
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
  //#region edit selected candidate detail
  EditSelectedCandidateDEtail(data: ISelectedCandidateModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_HiringRequestSelectCandidate,
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

  //#region "GetRequestedCandidateById"
  GetRequestedCandidateById(data: IReuestedCandidateDetailModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetHiringCandidatesListById,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectHiringCandidateDetailModel,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "AddbudgetLineDetailList"
  AddInterViewCandidateDetail(data: IitervireCandidateModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_AddCandidateInterviewDetail,
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
  //#region edit selected candidate detail
  IsCompltedeHrDetail(model: CompleteHiringRequestModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_CompleteHiringRequest,
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
  //#region edit selected candidate detail
  IsCloasedHrDetail(model: CompleteHiringRequestModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_ClosedHiringRequest,
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

  //#region "DeleteCandidateDetailDetail"
  DeleteCandidateDetailDetail(model: IHiringReuestCandidateModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_DeleteCandidatDetail,
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

  //#region "GetCountryList"
  GetCountryList(): any {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllCountryDetails
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.CountryDetailsList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion
  //#region "GetProvinceList"
  GetProvinceList(): any {
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

  //#region "AddJobHiringDetails"
  addJobHiringDetails(data: any) {
    console.log(data);
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HREmployee_AddJobHiringDetail,
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

  //#region "GetProjectHiringRequestDetailsByHiringRequestId"
  GetProjectHiringRequestDetailsByHiringRequestId(HiringRequestId: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetProjectHiringRequestDetailsByHiringRequestId,
        HiringRequestId
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

  //#region "GetAllProjectHiringRequestDetailByHiringRequestId"
  GetAllProjectHiringRequestDetailByHiringRequestId(HiringRequestId: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetAllProjectHiringRequestDetailByHiringRequestId,
        HiringRequestId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.ProjectHiringRequestAllDetail,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetRatingBasedCriteriaQuestion"
  GetRatingBasedCriteriaQuestion(OfficeId: number): any {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() +
          GLOBAL.API_Code_GetRatingBasedCriteriaQuestions +
          '?OfficeId=' +
          OfficeId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.RatingBasedCriteriaQuestionList,
            statusCode: x.StatusCode
            // message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetTechnicalQuestionsByDesignationId"
  GetTechnicalQuestionsByDesignationId(DesignationId: number): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetTechnicalQuestionsByDesignationId,
        DesignationId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.TechnicalQuestionsList,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetCandidateDetailsByCandidateId"
  GetCandidateDetailsByCandidateId(CandidateId: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetCandidateDetailsByCandidateId,
        CandidateId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.CandidateDetails,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "GetProjectHiringRequestDetailsByHiringRequestId"
  GetAllHiringRequestDetailForInterviewByHiringRequestId(model: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetAllHiringRequestDetailForInterviewByHiringRequestId,
        model
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.HiringRequestDetails,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "AddInterviewDetails"
  AddInterviewDetails(data: InterviewDetailModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_AddInterviewDetails,
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

  //#region "GetCandidateDetailsByCandidateId"
  GetInterviewDetailsByInterviewId(InterviewId: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_GetInterviewDetailsByInterviewId,
        InterviewId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.InterviewDetails,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "getDesignationList"
  getDesignationList(): any {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllDesignationList
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

  //#region "getDepartmentList"
  getDepartmentList(officeId): any {
    return this.globalService.getDataById(
      this.appurl.getApiUrl() +
        GLOBAL.API_Code_GetDepartmentsByOfficeId +
        '?officeId=' +
        officeId
    );
  }

  GetHiringRequestCode(ProjectId: number) {
    return this.globalService
      .getListByListId(
        this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_GetHiringRequestCode,
        ProjectId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.HiringRequestCode,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "DownloadCandidateCvByRequestId"
  DownloadCandidateCvByRequestId(CandidateId: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_HiringRequest_DownloadCandidateCvByRequestId,
        CandidateId
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.CandidateCvDownload,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "AddCandidateAsEmployee"
  AddCandidateAsEmployee(model): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_AddCandidateAsEmployee, model
    ).pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
}
