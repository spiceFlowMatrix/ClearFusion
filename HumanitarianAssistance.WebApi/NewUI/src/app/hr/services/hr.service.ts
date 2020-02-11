import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from 'src/app/shared/global';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { MatDialog } from '@angular/material';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';

@Injectable({
  providedIn: 'root'
})
export class HrService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient,
    private dialog: MatDialog
  ) {}

  //#region "getDesignatonList"
  getDesignationList(pageModel: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_GetAllDesignationDetail,
      pageModel
    );
  }
  //#endregion

  //#region "addDesignation"
  addDesignation(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_AddDesignationDetail,
      model
    );
  }
  //#endregion

  //#region "editDesignation"
  editDesignation(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_EditDesignationDetail,
      model
    );
  }
  //#endregion

  //#region "getEducationDegreeList"
  getEducationDegreeList(pageModel: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_GetEducationDegreeList,
      pageModel
    );
  }
  //#endregion

  //#region "addDegree"
  addDegree(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_AddEducationDegree,
      model
    );
  }
  //#endregion

  //#region "editDegree"
  editDegree(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_EditEducationDegree,
      model
    );
  }
  //#endregion

  //#region "getEducationDegreeList"
  getOfficeList(pageModel: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetOfficeList,
      pageModel
    );
  }
  //#endregion

  //#region "addOffice"
  addOffice(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_OfficeCode_AddOfficeDetail,
      model
    );
  }
  //#endregion

  //#region "editOffice"
  editOffice(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_OfficeCode_EditOfficeDetails,
      model
    );
  }
  //#endregion

  //#region "getDepartmentList"
  getDepartmentList(pageModel: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetDepartmentList,
      pageModel
    );
  }
  //#endregion

  //#region "addDepartment"
  addDepartment(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Department_AddDepartment,
      model
    );
  }
  //#endregion

  //#region "editDepartment"
  editDepartment(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Department_EditDepartment,
      model
    );
  }
  //#endregion

  //#region "getJobGradeList"
  getJobGradeList(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetJobGradeList,
      model
    );
  }
  //#endregion

  //#region "addJobGrade"
  addJobGrade(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HR_AddJobGradeDetail,
      model
    );
  }
  //#endregion

  //#region "editJobGrade"
  editJobGrade(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HR_EditJobGradeDetail,
      model
    );
  }
  //#endregion

  //#region "getAttendanceGroupList"
  getAttendanceGroupList(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_GetAttendanceGroupList,
      model
    );
  }
  //#endregion

  //#region "addAttendanceGroup"
  addAttendanceGroup(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_AddAttendanceGroups,
      model
    );
  }
  //#endregion

  //#region "editAttendanceGroup"
  editAttendanceGroup(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_EditAttendanceGroups,
      model
    );
  }
  //#endregion

  //#region "getProfessionList"
  getProfessionList(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetProfessionList,
      model
    );
  }
  //#endregion

  //#region "addProfession"
  addProfession(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Profession_AddProfession,
      model
    );
  }
  //#endregion

  //#region "editProfession"
  editProfession(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Profession_EditProfession,
      model
    );
  }
  //#endregion

  //#region "getQualificationList"
  getQualificationList(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetQualificationList,
      model
    );
  }
  //#endregion

  //#region "addQualification"
  addQualification(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_AddQualificationDetails,
      model
    );
  }
  //#endregion

  //#region "editQualification"
  editQualification(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_EditQualifactionDetails,
      model
    );
  }
  //#endregion

  //#region "getExitInterviewQuestionsList"
  getExitInterviewQuestionsList(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_GetAllExitInterviewQuestions,
      model
    );
  }
  //#endregion

  //#region "UpsertExitInterviewQuestion"
  UpsertExitInterviewQuestion(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_UpsertExitInterviewQuestion,
      model
    );
  }
  //#endregion

  //#region "getSequenceNumber"
  getSequenceNumber(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_GetSequenceNumber,
      model
    );
  }
  //#endregion

  //#region "deleteExitinterviewQuestion"
  deleteExitinterviewQuestion(Id: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_DeleteExitInterviewQuestion,
      Id
    );
  }
  //#endregion

  //#region "getLeaveTypeList"
  getLeaveTypeList(pageModel: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_GetAllLeaveReasonType,
      pageModel
    );
  }
  //#endregion

  //#region "updateLeaveType"
  updateLeaveType(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_EditLeaveReasonDetail,
      model
    );
  }
  //#endregion

  //#region "addLeaveType"
  addLeaveType(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_AddLeaveReasonDetail,
      model
    );
  }
  //#endregion

  //#region "deleteLeaveType"
  deleteLeaveType(Id: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HRConfiguration_DeleteLeaveType,
      Id
    );
  }
  //#endregion
  // common methods
  openDeleteDialog() {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });
    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText = Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText = Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => {});

    return dialogRef.componentInstance.confirmDelete;
  }

  deleteEducationDegree(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_DeleteEducationDegree,
      data
    );
  }

  deleteOfficeDegree(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_OfficeCode_DeleteOfficeDetails,
      data
    );
  }
  deleteDepartmentDetail(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Department_DeleteDepartment,
      data
    );
  }

  deleteJobGradeDetail(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_DeleteJobGradeDetail,
      data
    );
  }
  deleteAttendenceDetail(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_DeleteAttendanceGroups,
      data
    );
  }
  deleteProfessionDetail(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_Profession_DeleteProfession,
      data
    );
  }
  deleteQualificationDetail(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Code_DeleteQualifactionDetails,
      data
    );
  }
  deleteDesignationDetail(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_DeleteDesignationDetail,
      data
    );
  }

  getAllHolidaysList(model: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HR_GetAllHoliday,
      model
    );
  }

  //#region "addHoliday"
  addHoliday(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_AddHoliday,
      model
    );
  }
  //#endregion

  getAllOfficeCodeList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice
    );
  }

  //#region "EditHoliday"
  editHoliday(model: any): any {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_EditHoliday,
      model
    );
  }
  //#endregion
  deleteHoliday(holidayId: any) {
    return this.globalService.deleteById(
      this.appurl.getApiUrl() +
        GLOBAL.API_Hr_DeleteHolidayDetails +
        '?id=' +
        holidayId
    );
  }
  getWeeklyHolidaysList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() +
        GLOBAL.API_HR_GetAllHolidayWeeklyDetails
    );
  }
  getAppraisalQuestions(officeId: number) {
    return this.globalService.getList(
      this.appurl.getApiUrl() +
        'Code/GetAppraisalQuestions' +
        '?OfficeId=' +
        officeId
    );
  }

  addAppraisalQuestion(data) {
    return this.globalService.post(
      this.appurl.getApiUrl() + 'Code/AddAppraisalQuestion',
      data
    );
  }
  editAppraisalQuestion(data) {
    return this.globalService.post(
      this.appurl.getApiUrl() + 'Code/EditAppraisalQuestion',
      data
    );
  }

  GetAccountList() {
    return this.globalService
      .getDataById(
        this.appurl.getApiUrl() + GLOBAL.API_Accounting_GetAccountList
      );
  }

  GetAllAuditLogById(EmployeeId: number) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_HRConfiguration_GetAllAuditLogById,
        EmployeeId
    );
  }
}
