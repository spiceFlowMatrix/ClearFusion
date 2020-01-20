(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["default~configuration-configuration-module~hr-hr-module"],{

/***/ "./src/app/hr/services/hr.service.ts":
/*!*******************************************!*\
  !*** ./src/app/hr/services/hr.service.ts ***!
  \*******************************************/
/*! exports provided: HrService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HrService", function() { return HrService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var projects_library_src_lib_components_delete_confirmation_delete_confirmation_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! projects/library/src/lib/components/delete-confirmation/delete-confirmation.component */ "./projects/library/src/lib/components/delete-confirmation/delete-confirmation.component.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var HrService = /** @class */ (function () {
    function HrService(globalService, appurl, http, dialog) {
        this.globalService = globalService;
        this.appurl = appurl;
        this.http = http;
        this.dialog = dialog;
    }
    //#region "getDesignatonList"
    HrService.prototype.getDesignationList = function (pageModel) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetAllDesignationDetail, pageModel);
    };
    //#endregion
    //#region "addDesignation"
    HrService.prototype.addDesignation = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_AddDesignationDetail, model);
    };
    //#endregion
    //#region "editDesignation"
    HrService.prototype.editDesignation = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_EditDesignationDetail, model);
    };
    //#endregion
    //#region "getEducationDegreeList"
    HrService.prototype.getEducationDegreeList = function (pageModel) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetEducationDegreeList, pageModel);
    };
    //#endregion
    //#region "addDegree"
    HrService.prototype.addDegree = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_AddEducationDegree, model);
    };
    //#endregion
    //#region "editDegree"
    HrService.prototype.editDegree = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_EditEducationDegree, model);
    };
    //#endregion
    //#region "getEducationDegreeList"
    HrService.prototype.getOfficeList = function (pageModel) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetOfficeList, pageModel);
    };
    //#endregion
    //#region "addOffice"
    HrService.prototype.addOffice = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_OfficeCode_AddOfficeDetail, model);
    };
    //#endregion
    //#region "editOffice"
    HrService.prototype.editOffice = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_OfficeCode_EditOfficeDetails, model);
    };
    //#endregion
    //#region "getDepartmentList"
    HrService.prototype.getDepartmentList = function (pageModel) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetDepartmentList, pageModel);
    };
    //#endregion
    //#region "addDepartment"
    HrService.prototype.addDepartment = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Department_AddDepartment, model);
    };
    //#endregion
    //#region "editDepartment"
    HrService.prototype.editDepartment = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Department_EditDepartment, model);
    };
    //#endregion
    //#region "getJobGradeList"
    HrService.prototype.getJobGradeList = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetJobGradeList, model);
    };
    //#endregion
    //#region "addJobGrade"
    HrService.prototype.addJobGrade = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HR_AddJobGradeDetail, model);
    };
    //#endregion
    //#region "editJobGrade"
    HrService.prototype.editJobGrade = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HR_EditJobGradeDetail, model);
    };
    //#endregion
    //#region "getAttendanceGroupList"
    HrService.prototype.getAttendanceGroupList = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetAttendanceGroupList, model);
    };
    //#endregion
    //#region "addAttendanceGroup"
    HrService.prototype.addAttendanceGroup = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_AddAttendanceGroups, model);
    };
    //#endregion
    //#region "editAttendanceGroup"
    HrService.prototype.editAttendanceGroup = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_EditAttendanceGroups, model);
    };
    //#endregion
    //#region "getProfessionList"
    HrService.prototype.getProfessionList = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetProfessionList, model);
    };
    //#endregion
    //#region "addProfession"
    HrService.prototype.addProfession = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Profession_AddProfession, model);
    };
    //#endregion
    //#region "editProfession"
    HrService.prototype.editProfession = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Profession_EditProfession, model);
    };
    //#endregion
    //#region "getQualificationList"
    HrService.prototype.getQualificationList = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetQualificationList, model);
    };
    //#endregion
    //#region "addQualification"
    HrService.prototype.addQualification = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_AddQualificationDetails, model);
    };
    //#endregion
    //#region "editQualification"
    HrService.prototype.editQualification = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_EditQualifactionDetails, model);
    };
    //#endregion
    //#region "getExitInterviewQuestionsList"
    HrService.prototype.getExitInterviewQuestionsList = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetAllExitInterviewQuestions, model);
    };
    //#endregion
    //#region "UpsertExitInterviewQuestion"
    HrService.prototype.UpsertExitInterviewQuestion = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_UpsertExitInterviewQuestion, model);
    };
    //#endregion
    //#region "getSequenceNumber"
    HrService.prototype.getSequenceNumber = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetSequenceNumber, model);
    };
    //#endregion
    //#region "deleteExitinterviewQuestion"
    HrService.prototype.deleteExitinterviewQuestion = function (Id) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_DeleteExitInterviewQuestion, Id);
    };
    //#endregion
    //#region "getLeaveTypeList"
    HrService.prototype.getLeaveTypeList = function (pageModel) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_GetAllLeaveReasonType, pageModel);
    };
    //#endregion
    //#region "updateLeaveType"
    HrService.prototype.updateLeaveType = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_EditLeaveReasonDetail, model);
    };
    //#endregion
    //#region "addLeaveType"
    HrService.prototype.addLeaveType = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_AddLeaveReasonDetail, model);
    };
    //#endregion
    //#region "deleteLeaveType"
    HrService.prototype.deleteLeaveType = function (Id) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_DeleteLeaveType, Id);
    };
    //#endregion
    // common methods
    HrService.prototype.openDeleteDialog = function () {
        var dialogRef = this.dialog.open(projects_library_src_lib_components_delete_confirmation_delete_confirmation_component__WEBPACK_IMPORTED_MODULE_5__["DeleteConfirmationComponent"], {
            width: '300px',
            height: '250px',
            data: 'delete',
            disableClose: false
        });
        dialogRef.componentInstance.confirmMessage =
            src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["Delete_Confirmation_Texts"].deleteText1;
        dialogRef.componentInstance.confirmText = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["Delete_Confirmation_Texts"].yesText;
        dialogRef.componentInstance.cancelText = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["Delete_Confirmation_Texts"].noText;
        dialogRef.afterClosed().subscribe(function (result) { });
        return dialogRef.componentInstance.confirmDelete;
    };
    HrService.prototype.deleteEducationDegree = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_DeleteEducationDegree, data);
    };
    HrService.prototype.deleteOfficeDegree = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_OfficeCode_DeleteOfficeDetails, data);
    };
    HrService.prototype.deleteDepartmentDetail = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Department_DeleteDepartment, data);
    };
    HrService.prototype.deleteJobGradeDetail = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_DeleteJobGradeDetail, data);
    };
    HrService.prototype.deleteAttendenceDetail = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_DeleteAttendanceGroups, data);
    };
    HrService.prototype.deleteProfessionDetail = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_Profession_DeleteProfession, data);
    };
    HrService.prototype.deleteQualificationDetail = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_DeleteQualifactionDetails, data);
    };
    HrService.prototype.deleteDesignationDetail = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HRConfiguration_DeleteDesignationDetail, data);
    };
    HrService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"],
            _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatDialog"]])
    ], HrService);
    return HrService;
}());



/***/ })

}]);
//# sourceMappingURL=default~configuration-configuration-module~hr-hr-module.js.map