import { Injectable } from '@angular/core';
import {
  ProjectDetailModel,
  ProjectChatModel,
  ProgramModel,
  AreaModel,
  SectorModel,
  ProjectProgramModel,
  ProjectAreaModel,
  ProjectSectorModel,
  ApproveProjectDetailModel,
  WinApprovalDetailModel,
  ProjectOtherDetailModel,
  ProposalDocModel,
  securityConsiderationMultiSelectModel,
  ProvinceMultiSelectModel,
  DistrictMultiSelectModel,
  CountryMultiSelectModel
} from '../project-details/models/project-details.model';
import { IProjectFilterModel } from '../models/projectList.model';
import { ProjectJobsFilterModel } from '../project-jobs/project-jobsmodel';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import {
  DonorDetailModel,
  DonorFilterModel
} from '../../project-donor/donor-master/Models/donar-detail.model';
import {
  ProjectIndicatorFilterModel,
  IndicatorDetailModel
} from 'src/app/dashboard/project-management/project-indicators/project-indicators-model';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { MonitoringModel } from 'src/app/dashboard/project-management/project-list/project-activities/project-activity-phase/monitoring/monitoring-model';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { IProjectRoles } from '../project-details/models/project-people.model';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { projectPagesMaster } from 'src/app/shared/applicationpagesenum';
@Injectable({
  providedIn: 'root'
})
export class ProjectListService {
  showHideHeader = false;

  opportunityControlRoles: IProjectRoles[] = [
    {
      Id: 1,
      Role: 'Proposal Documenter'
    },
    {
      Id: 2,
      Role: 'Proposal Reviewer'
    },
    {
      Id: 3,
      Role: 'Proposal Director'
    },
    {
      Id: 4,
      Role: 'Budget Manager'
    },
    {
      Id: 5,
      Role: 'Project Administrator'
    }
  ];

  logisticsControlRoles: IProjectRoles[] = [
    {
      Id: 1,
      Role: 'Control'
    },
    {
      Id: 2,
      Role: 'Logistic Officer'
    },
    {
      Id: 3,
      Role: 'General Admin & Finance'
    },
    {
      Id: 4,
      Role: 'Field Office Manager'
    },
    {
      Id: 5,
      Role: 'General Assembly'
    }
  ];

  activitiesControlRoles: IProjectRoles[] = [
    {
      Id: 1,
      Role: 'Planning Officer'
    },
    {
      Id: 2,
      Role: 'Implementation Officer'
    },
    {
      Id: 3,
      Role: 'Monitoring Officer'
    }
  ];

  hiringControlRoles: IProjectRoles[] = [
    {
      Id: 1,
      Role: 'Hiring Officer'
    },
    {
      Id: 2,
      Role: 'Control Officer'
    },
    {
      Id: 3,
      Role: 'General Assembly'
    },
    {
      Id: 4,
      Role: 'General Admin & Finance'
    },
    {
      Id: 5,
      Role: 'Field Office Manager'
    }
  ];


  menuList: IMenuList[] = [
    {
      Id: 1,
      PageId: projectPagesMaster.ProjectDetails,
      Text: 'Details',
      Link: 'detail'
    },
    {
      Id: 2,
      PageId: projectPagesMaster.CriteriaEvaluation,
      Text: 'Criteria Evaluation',
      Link: 'criteria-evaluation'
    },
    {
      Id: 3,
      PageId: projectPagesMaster.Proposal,
      Text: 'Proposal',
      Link: 'proposal'
    },
    {
      Id: 4,
      PageId: projectPagesMaster.ProjectJobs,
      Text: 'Project Jobs',
      Link: 'project-jobs'
    },
    {
      Id: 5,
      PageId: projectPagesMaster.ProjectBudgetLine,
      Text: 'Budget Lines',
      Link: 'budget-lines'
    },
    {
      Id: 6,
      PageId: projectPagesMaster.ProjectActivities,
      Text: 'Project Activities',
      Link: 'project-activities'
    },
    {
      Id: 7,
      PageId: projectPagesMaster.ProjectPeople,
      Text: 'People',
      Link: 'people'
    },
    {
      Id: 8,
      PageId: projectPagesMaster.HiringRequests,
      Text: 'Hiring Requests',
      Link: 'hiring-request'
    }
  ];

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "AddProjectDetail"
  getAllProjectMenu(): IMenuList[]  {
    return this.menuList;
  }
  //#endregion

  //#region "AddProjectDetail"
  AddProjectDetail(url: string, data: ProjectDetailModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddProjectChat"
  AddProjectChat(url: string, data: ProjectChatModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "GetChatByProjectId"
  GetChatByProjectId(url: string, id: number) {
    return this.globalService.getListById(url, id);
  }
  //#endregion

  GetProjectFilterDetails(url: string, data: IProjectFilterModel) {
    return this.globalService.post(url, data);
  }

  GetProjectDetails(url: string) {
    return this.globalService.getList(url);
  }

  GetProjectDetailsByProjectId(url: string, id: number) {
    return this.globalService.getListById(url, id);
  }

  GetProjectWinLossDetail(url: string, id: number) {
    return this.globalService.post(url, id);
  }

  GetIsApprovedCriteriaEvaluationDetail(url: string, id: number) {
    return this.globalService.post(url, id);
  }
  //#region  show/Hide
  onShowHideHeader(flag: boolean) {
    this.showHideHeader = flag;
  }
  //#endregion
  //#region Donor master
  AddDonorDetail(url: string, data: DonorDetailModel) {
    return this.globalService.post(url, data);
  }
  GetAllDonorfilterList(url: string, data: DonorFilterModel) {
    return this.globalService.post(url, data);
  }
  GetAllDonorList(url: string) {
    return this.globalService.getList(url);
  }

  GetDonarDetailsByDonarId(url: string, id: number) {
    return this.globalService.getListById(url, id);
  }
  DeleteDonorDetail(url: string, id: number) {
    return this.globalService.post(url, id);
  }
  EditDonorDetail(url: string, data: DonorDetailModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region
  GetAllSectorList(url: string) {
    return this.globalService.getList(url);
  }

  GetAllAreaList(url: string) {
    return this.globalService.getList(url);
  }
  GetAllProgramList(url: string) {
    return this.globalService.getList(url);
  }
  // endregion
  //#region "AddProgramDetail"
  AddProgramDetail(url: string, data: ProgramModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "AddAreaDetail"
  AddAreaDetail(url: string, data: AreaModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "AddSectorDetail"
  AddSectorDetail(url: string, data: SectorModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "AddeditSelectProjectProgramvalue"
  AddeditSelectProjectProgramvalue(url: string, data: ProjectProgramModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "getProjectProgramById"
  GetProjectProgramById(url: string, Id: number) {
    return this.globalService.getListById(url, Id);
  }
  //#endregion
  //#region "getProjectAreaById"
  getProjectAreaById(url: string, Id: number) {
    return this.globalService.getListById(url, Id);
  }
  //#endregion
  //#region "AddeditSelectProjectProgramvalue"
  AddeditSelectAreaProgramvalue(url: string, data: ProjectAreaModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddeditSelectProjectProgramvalue"
  AddeditSelectSectorvalue(url: string, data: ProjectSectorModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "getProjectSectorById"
  getProjectSectorById(url: string, Id: number) {
    return this.globalService.getListById(url, Id);
  }
  //#endregion

  //#region
  //#region "AddProjectDetail"
  AddProjectApprovalDetail(url: string, data: ApproveProjectDetailModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "AddProjectDetail"
  WinProjectApprovalDetail(url: string, data: WinApprovalDetailModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#endregion
  //#region "getAllProvinceList"
  getAllCountryList(url: string) {
    return this.globalService.getList(url);
  }
  //#endregion
  //#region "getAllProvinceList"
  getAllProvinceList(url: string) {
    return this.globalService.getList(url);
  }
  //#endregion
  //#region "GetAllStrengthConsiderationDetails"
  GetAllStrengthConsiderationDetails(url: string) {
    return this.globalService.getList(url);
  }
  //#endregion

  //#region "GetAllCurrency"
  GetAllCurrency(url: string) {
    return this.globalService.getList(url);
  }
  //#endregion

  //#endregion
  //#region "GetAllstrength"
  GetAllGender(url: string) {
    return this.globalService.getList(url);
  }
  //#endregion
  //#region "GetAllstrength"
  GetAllSecurityDetails(url: string) {
    return this.globalService.getList(url);
  }
  //#endregion
  //#region "GetAllstrength"
  GetAllSecurityConsideration(url: string) {
    return this.globalService.getList(url);
  }
  //#endregion

  //#region "GetAllDistrictvalueByProvinceId"
  GetAllDistrictvalueByProvinceId(url: string, Id: any) {
    return this.globalService.getListByListId(url, Id);
  }
  //#endregion
   //#region "GetAllDistrictvalueByProvinceId"
   getAllProvinceListByCountryId(url: string, Id: any) {
    return this.globalService.getListByListId(url, Id);
  }
  //#endregion

  //#region "AddProjectDetail other details"
  AddEditProjectotherDetail(url: string, data: ProjectOtherDetailModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region GetotherprojectlistById
  GetOtherProjectDetailsByProjectId(url: string, id: number) {
    return this.globalService.getListById(url, id);
  }
  //#endregion
  //#region GetotherprojectlistById
  GetOtherSecurityConsiByProjectId(url: string, id: number) {
    return this.globalService.getListById(url, id);
  }
  //#endregion

  //#region "AddProjectDetail"
  GetAllOfficeList(url: string) {
    return this.globalService.getList(url);
  }
  //#region "GetAllDistrictvalueByProvinceId"
  CreateProjectproposal(url: string, Id: any) {
    return this.globalService.getListById(url, Id);
  }
  //#endregion
  //#region "GetAllDistrictvalueByProvinceId"
  GetProjectproposalById(url: string, Id: any) {
    return this.globalService.getListById(url, Id);
  }
  //#endregion
  //#region "uploadEDIFile"
  uploadEDIFile(url: string, Id: number, Formdata: any): any {
    return this.globalService.post(url, Formdata);
  }
  //#endregion
  //#region "AddEditProjectProposalDetail"
  AddEditProjectProposalDetail(url: string, data: ProposalDocModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "GetAllUserList"
  GetAllUserList() {

    return this.globalService.getList(this.appurl.getApiUrl() + GLOBAL.API_Account_GetAllUserDetails)
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.UserDetailList,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
  //#endregion

  //#region "GetAllRoleList"
  GetAllRoleList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Account_GetRoles
    );
  }
  //#endregion

  //#region "AddProjectDetail other details"
  AddEditSecurityMultiSelect(
    url: string,
    data: securityConsiderationMultiSelectModel
  ) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "AddProjectDetail other details"
  AddEditCountryMultiSelect(url: string, data: CountryMultiSelectModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "AddProjectDetail other details"
  AddEditProvinceMultiSelect(url: string, data: ProvinceMultiSelectModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddProjectDetail other details"
  AddEditDistricteMultiSelect(url: string, data: DistrictMultiSelectModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "AddProjectDetail other details"
  AddEditDistrictMultiSelect(url: string, data: DistrictMultiSelectModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  GetProjectJobList(url: string, data: ProjectJobsFilterModel) {
    return this.globalService.post(url, data);
  }

  GetProjectIndicatorsList(url: string, data: ProjectIndicatorFilterModel) {
    return this.globalService.post(url, data);
  }

  //#region "AddProjectIndicatorQuestions"
  AddProjectIndicatorQuestions(url: string) {
    return this.globalService.post(url, null);
  }
  //#endregion

  //#region "EditProjectIndicatorQuestions"
  EditProjectIndicatorQuestions(url: string, model: IndicatorDetailModel) {
    return this.globalService.post(url, model);
  }
  //#endregion

  //#region "EditProjectIndicatorQuestions"
  GetProjectIndicatorById(url: string, id: number) {
    return this.globalService.post(url, id);
  }
  //#endregion

  //#region "EditProjectIndicatorQuestions"
  GetProjectIndicatorQuestionById(url: string, id: number) {
    return this.globalService.post(url, id);
  }
  //#endregion

  //#region "AddProjectMonitoringReview"
  AddProjectMonitoringReview(url: string, data: MonitoringModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "EditProjectMonitoringReview"
  EditProjectMonitoringReview(url: string, data: MonitoringModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "uploadEDIFile"
  uploadReviewFile(
    url: string,
    Id: ApproveProjectDetailModel,
    Formdata: any
  ): any {
    return this.globalService.post(url, Formdata);
  }
  //#endregion

  //#region "uploadEDIFile"
  uploadFinalizeFile(
    url: string,
    Id: WinApprovalDetailModel,
    Formdata: any
  ): any {
    return this.globalService.post(url, Formdata);
  }
  //#endregion

  //#region "GetSignedUrl"
  GetSignedUrl(url: string, data) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "GetOpportunityControlRole"
  GetOpportunityControlRole() {
    return this.opportunityControlRoles;
  }
  //#endregion

  //#region "GetLogisticsControlRole"
  GetLogisticsControlRole() {
    return this.logisticsControlRoles;
  }
  //#endregion

  //#region "GetActivitiesControlRole"
  GetActivitiesControlRole() {
    return this.activitiesControlRoles;
  }
  //#endregion

  //#region "GetHiringControlRole"
  GetHiringControlRole() {
    return this.hiringControlRoles;
  }
  //#endregion

  //#region "GetOpportunityControl"
  GetOpportunityControl(data: any) {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_GetOpportunityControlList,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.OpportunityControlList,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
  //#endregion

  //#region "AddUserForOpportunityControl"
  AddUserForOpportunityControl(data: any) {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_AddOpportunityControl,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.CommonId.LongId,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
  //#endregion

  //#region "EditUserForOpportunityControl"
  EditUserForOpportunityControl(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_EditOpportunityControl,
        data
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

  //#region DeleteUserForOpportunityControl"
  DeleteUserForOpportunityControl(data: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_DeleteOpportunityControl,
        data
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


  //#region "GetLogisticsControl"
  GetLogisticsControl(data: any) {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_GetLogisticsControlList,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.LogisticsControlList,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
  //#endregion

  //#region "AddUserForLogisticsControl"
  AddUserForLogisticsControl(data: any) {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_AddLogisticsControl,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.CommonId.LongId,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
  //#endregion

  //#region "EditUserForLogisticsControl"
  EditUserForLogisticsControl(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_EditLogisticsControl,
        data
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

  //#region DeleteUserForLogisticsControl"
  DeleteUserForLogisticsControl(data: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_DeleteLogisticsControl,
        data
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


  //#region "GetActivityControl"
  GetActivityControl(data: any) {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_GetActivitiesControl,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.ActivitiesControlList,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
  //#endregion

  //#region "AddUserForActivitiesControl"
  AddUserForActivitiesControl(data: any) {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_AddActivitiesControl,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.CommonId.LongId,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
  //#endregion

  //#region "EditUserForActivitiesControl"
  EditUserForActivitiesControl(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_EditActivitiesControl,
        data
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

  //#region DeleteUserForActivitiesControl"
  DeleteUserForActivitiesControl(data: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_DeleteActivitiesControl,
        data
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



  //#region "GetHiringControl"
  GetHiringControl(data: any) {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_GetHiringControl,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.data.HiringControlList,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
  //#endregion

  //#region "AddUserForHiringControl"
  AddUserForHiringControl(data: any) {
    return this.globalService
    .post(
      this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_AddHiringControl,
      data
    )
    .pipe(
      map(x => {
        const responseData: IResponseData = {
          data: x.CommonId.LongId,
          statusCode: x.StatusCode,
          message: x.Message
        };
        return responseData;
      })
    );
  }
  //#endregion

  //#region "EditUserForHiringControl"
  EditUserForHiringControl(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_EditHiringControl,
        data
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

  //#region DeleteUserForHiringControl"
  DeleteUserForHiringControl(data: number) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_ProjectPeople_DeleteHiringControl,
        data
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



}
