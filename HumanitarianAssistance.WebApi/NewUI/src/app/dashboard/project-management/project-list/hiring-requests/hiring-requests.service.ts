import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { map } from 'rxjs/operators';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { IHiringRequestDetailModel, ProjectHiringRequestFilterModel, IHiringReuestCandidateModel,
IReuestedCandidateDetailModel, IitervireCandidateModel,
 ISelectedCandidateModel, CandidateDetailModel } from './models/hiring-requests-model';
import { BehaviorSubject } from 'rxjs';
import { IProjectPermissionMode } from '../project-activities/models/project-activities.model';

@Injectable({
  providedIn: 'root'
})
export class HiringRequestsService {

  public hiringPermissionSubject = new BehaviorSubject<IProjectPermissionMode[]>([]);

  constructor( private globalService: GlobalService,
    private appurl: AppUrlService) { }

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
//#endregion
  //#region "GetOfficeList"
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

   //#region "AddHiringRequestDetail"
  AddHiringRequestDetail(data: IHiringRequestDetailModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_AddHiringRequestDetail,
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
 EditHiringRequestDetail(data: IHiringRequestDetailModel) {
  return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_EditHiringRequestDetail,
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

//#region "EditCandidateDetail"
 EditCandidateDetail(data: CandidateDetailModel) {
  return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_HREmployee_EditEmployeeProfessionalDetail,
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
  GetProjectHiringRequestFilterList(data: ProjectHiringRequestFilterModel): any {
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
GetAllEmployeeList(): any {
  return this.globalService
    .getList(this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_GetAllEmployeeList)
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
    .getDataById(this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_GetEmployeeListByOfficeId + '?OfficeId=' + OfficeId)
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
    .getList(this.appurl.getApiUrl() + GLOBAL.API_HREmployee_GetAllEmployeeContractType)
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
      this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_AddHiringRequestCandidate,
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
    this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_EditHiringRequestCandidate,
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
    this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_HiringRequestSelectCandidate,
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
      this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_GetHiringCandidatesListById,
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
      this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_AddCandidateInterviewDetail,
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
IsCompltedeHrDEtail(hiringRequestId: number ) {
  return this.globalService
  .post(
    this.appurl.getApiUrl() + GLOBAL.API_HiringRequest_CompleteHiringRequest,
    hiringRequestId
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
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllCountryDetails)
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
    //#region "GetProvinceList"
    GetProvinceList(): any {
      return this.globalService
        .getList(this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProvinceDetails)
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




setHiringPermissions(permissionList: IProjectPermissionMode[]) {
  this.hiringPermissionSubject.next(permissionList);
}

}
