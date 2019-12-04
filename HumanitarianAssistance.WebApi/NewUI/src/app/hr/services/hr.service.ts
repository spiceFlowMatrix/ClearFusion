import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from 'src/app/shared/global';

@Injectable({
  providedIn: 'root'
})
export class HrService {

  constructor(private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient) { }

  //#region "getDesignatonList"
  getDesignationList(pageModel: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetAllDesignationDetail, pageModel
      );
  }
  //#endregion

  //#region "addDesignation"
  addDesignation(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_AddDesignationDetail, model
      );
  }
  //#endregion

  //#region "editDesignation"
  editDesignation(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_EditDesignationDetail, model
      );
  }
  //#endregion

  //#region "getEducationDegreeList"
  getEducationDegreeList(pageModel: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetEducationDegreeList, pageModel
      );
  }
  //#endregion

  //#region "addDegree"
  addDegree(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_AddEducationDegree, model
      );
  }
  //#endregion

  //#region "editDegree"
  editDegree(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_EditEducationDegree, model
      );
  }
  //#endregion

  //#region "getEducationDegreeList"
  getOfficeList(pageModel: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetOfficeList, pageModel
      );
  }
  //#endregion

   //#region "addOffice"
   addOffice(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_OfficeCode_AddOfficeDetail, model
      );
  }
  //#endregion

   //#region "editOffice"
   editOffice(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_OfficeCode_EditOfficeDetails, model
      );
  }
  //#endregion

  //#region "getDepartmentList"
  getDepartmentList(pageModel: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetDepartmentList, pageModel
      );
  }
  //#endregion

  //#region "addDepartment"
  addDepartment(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Department_AddDepartment, model
      );
  }
  //#endregion

  //#region "editDepartment"
  editDepartment(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Department_EditDepartment, model
      );
  }
  //#endregion

  //#region "getJobGradeList"
  getJobGradeList(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetJobGradeList, model
      );
  }
  //#endregion

  //#region "addJobGrade"
  addJobGrade(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_AddJobGradeDetail, model
      );
  }
  //#endregion

  //#region "editJobGrade"
  editJobGrade(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HR_EditJobGradeDetail, model
      );
  }
  //#endregion

  //#region "getAttendanceGroupList"
  getAttendanceGroupList(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetAttendanceGroupList, model
      );
  }
  //#endregion

  //#region "addAttendanceGroup"
  addAttendanceGroup(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Code_AddAttendanceGroups, model
      );
  }
  //#endregion

  //#region "editAttendanceGroup"
  editAttendanceGroup(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Code_EditAttendanceGroups, model
      );
  }
  //#endregion

  //#region "getProfessionList"
  getProfessionList(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetProfessionList, model
      );
  }
  //#endregion

  //#region "addProfession"
  addProfession(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Profession_AddProfession, model
      );
  }
  //#endregion

  //#region "editProfession"
  editProfession(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Profession_EditProfession, model
      );
  }
  //#endregion

  //#region "getQualificationList"
  getQualificationList(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetQualificationList, model
      );
  }
  //#endregion


  //#region "addProfession"
  addQualification(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Code_AddQualificationDetails, model
      );
  }
  //#endregion

  //#region "editQualification"
  editQualification(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Code_EditQualifactionDetails, model
      );
  }
  //#endregion

  //#region "getExitInterviewQuestionsList"
  getExitInterviewQuestionsList(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetAllExitInterviewQuestions, model
      );
  }
  //#endregion

  //#region "UpsertExitInterviewQuestion"
  UpsertExitInterviewQuestion(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_UpsertExitInterviewQuestion, model
      );
  }
  //#endregion

  //#region "getSequenceNumber"
  getSequenceNumber(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetSequenceNumber, model
      );
  }
  //#endregion

  //#region "deleteExitinterviewQuestion"
  deleteExitinterviewQuestion(Id: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_DeleteExitInterviewQuestion, Id
      );
  }
  //#endregion

  //#region "getLeaveTypeList"
  getLeaveTypeList(pageModel: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetAllLeaveReasonType, pageModel
      );
  }
  //#endregion

  //#region "updateLeaveType"
  updateLeaveType(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Code_EditLeaveReasonDetail, model
      );
  }
  //#endregion

   //#region "addLeaveType"
   addLeaveType(model: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_Code_AddLeaveReasonDetail, model
      );
  }
  //#endregion

   //#region "deleteLeaveType"
   deleteLeaveType(Id: any): any {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_DeleteLeaveType, Id
      );
  }
  //#endregion
}


