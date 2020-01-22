(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["hr-hr-module"],{

/***/ "./src/app/hr/components/employee-attendance/employee-attendance.component.html":
/*!**************************************************************************************!*\
  !*** ./src/app/hr/components/employee-attendance/employee-attendance.component.html ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-card [ngStyle]=\"scrollStyles\">\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <div class=\"col-sm-3\">\r\n        <h4>\r\n          Daily Attendance\r\n        </h4>\r\n      </div>\r\n      <div class=\"col-md-3\">\r\n        <mat-form-field>\r\n          <input matInput [matDatepicker]=\"picker\" [max]=\"maxDate\" [(ngModel)]=\"Month\" placeholder=\"Date\">\r\n          <mat-datepicker-toggle matSuffix [for]=\"picker\"></mat-datepicker-toggle>\r\n          <mat-datepicker #picker (monthSelected)=\"monthSelected($event)\" startView=\"multi-year\"></mat-datepicker>\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <div class=\"responsive_table-responsive\">\r\n        <table class=\"table table-bordered\">\r\n          <tbody>\r\n            <tr>\r\n              <td></td>\r\n              <td>Date</td>\r\n              <td>In Time</td>\r\n              <td>Out Time</td>\r\n              <td>Attended</td>\r\n              <td width=\"1%\"></td>\r\n            </tr>\r\n\r\n            <tr *ngFor= 'let item of attendanceList' >\r\n              <td></td>\r\n              <td>\r\n                {{item.Date}}\r\n              </td>\r\n              <td> {{item.InTime}}</td>\r\n              <td> {{item.OutTime}}</td>\r\n              <td>{{item.Attended}}</td>\r\n              <td width=\"1%\"></td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n      </div>\r\n      <mat-paginator\r\n                    [length]=\"attendanceForm.TotalCount\"\r\n                    [pageSize]=\"attendanceForm.PageSize\"\r\n                    [pageIndex]=\"attendanceForm.PageIndex\"\r\n                    [pageSizeOptions]=\"[5, 10, 25, 100]\"\r\n                    (page)=\"pageEvent($event)\"\r\n                  >\r\n      </mat-paginator>\r\n    </div>\r\n  </div>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-attendance/employee-attendance.component.scss":
/*!**************************************************************************************!*\
  !*** ./src/app/hr/components/employee-attendance/employee-attendance.component.scss ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtYXR0ZW5kYW5jZS9lbXBsb3llZS1hdHRlbmRhbmNlLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/components/employee-attendance/employee-attendance.component.ts":
/*!************************************************************************************!*\
  !*** ./src/app/hr/components/employee-attendance/employee-attendance.component.ts ***!
  \************************************************************************************/
/*! exports provided: EmployeeAttendanceComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeAttendanceComponent", function() { return EmployeeAttendanceComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_attendance_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../services/attendance.service */ "./src/app/hr/services/attendance.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var EmployeeAttendanceComponent = /** @class */ (function () {
    function EmployeeAttendanceComponent(routeActive, attendanceService, toastr, commonLoader) {
        this.routeActive = routeActive;
        this.attendanceService = attendanceService;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        //#endregion
        // Variables
        this.maxDate = new Date();
        this.attendanceList = [];
        this.getScreenSize();
    }
    //#region "Dynamic Scroll"
    EmployeeAttendanceComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 200 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    EmployeeAttendanceComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.routeActive.params.subscribe(function (params) {
            _this.employeeId = +params['id'];
        });
        this.maxDate.setDate(this.maxDate.getDate());
        this.initForm();
        if (this.employeeId != null && this.employeeId !== undefined) {
            this.getAttendanceList(this.employeeId);
        }
        this.Month = new Date();
    };
    //#region "initForm"
    EmployeeAttendanceComponent.prototype.initForm = function () {
        this.attendanceForm = {
            Year: new Date().getFullYear(),
            Month: new Date().getMonth() + 1,
            PageIndex: 0,
            PageSize: 10,
            TotalCount: 0,
            EmployeeId: null
        };
    };
    //#endregion
    EmployeeAttendanceComponent.prototype.pageEvent = function (event) {
        this.attendanceForm.PageIndex = event.pageIndex;
        this.attendanceForm.PageSize = event.pageSize;
        this.getAttendanceList(this.employeeId);
    };
    //#region "GetAttendanceFileterList"
    EmployeeAttendanceComponent.prototype.getAttendanceList = function (employeeId) {
        var _this = this;
        this.commonLoader.showLoader();
        this.attendanceForm.TotalCount = 0;
        this.attendanceForm.EmployeeId = employeeId;
        this.attendanceList = [];
        this.attendanceService.getAttendanceList(this.attendanceForm).subscribe(function (response) {
            if (response != null && response.attendanceList.length > 0) {
                response.attendanceList.forEach(function (element) {
                    _this.attendanceList.push({
                        Date: element.Date,
                        InTime: element.InTime,
                        OutTime: element.OutTime,
                        Attended: element.Attended
                    });
                });
                _this.attendanceForm.TotalCount = response.TotalCount;
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.toastr.warning(error);
            _this.commonLoader.hideLoader();
        });
    };
    //#endregion
    //#region "monthSelected"
    EmployeeAttendanceComponent.prototype.monthSelected = function (params) {
        if (params != null && params !== undefined) {
            this.picker.close();
            this.Month = params;
            var year = new Date(params).getFullYear();
            this.attendanceForm.Year = year;
            this.attendanceForm.Month = new Date(params).getMonth() + 1;
            this.attendanceForm.MonthFilterValue = params;
            this.getAttendanceList(this.employeeId);
        }
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDatepicker"]),
        __metadata("design:type", Object)
    ], EmployeeAttendanceComponent.prototype, "picker", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], EmployeeAttendanceComponent.prototype, "getScreenSize", null);
    EmployeeAttendanceComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-employee-attendance',
            template: __webpack_require__(/*! ./employee-attendance.component.html */ "./src/app/hr/components/employee-attendance/employee-attendance.component.html"),
            styles: [__webpack_require__(/*! ./employee-attendance.component.scss */ "./src/app/hr/components/employee-attendance/employee-attendance.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"],
            _services_attendance_service__WEBPACK_IMPORTED_MODULE_3__["AttendanceService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"]])
    ], EmployeeAttendanceComponent);
    return EmployeeAttendanceComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-contract/employee-contract.component.html":
/*!**********************************************************************************!*\
  !*** ./src/app/hr/components/employee-contract/employee-contract.component.html ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>\r\n  employee-contract works!\r\n</p>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-contract/employee-contract.component.scss":
/*!**********************************************************************************!*\
  !*** ./src/app/hr/components/employee-contract/employee-contract.component.scss ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtY29udHJhY3QvZW1wbG95ZWUtY29udHJhY3QuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/hr/components/employee-contract/employee-contract.component.ts":
/*!********************************************************************************!*\
  !*** ./src/app/hr/components/employee-contract/employee-contract.component.ts ***!
  \********************************************************************************/
/*! exports provided: EmployeeContractComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeContractComponent", function() { return EmployeeContractComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var EmployeeContractComponent = /** @class */ (function () {
    function EmployeeContractComponent() {
    }
    EmployeeContractComponent.prototype.ngOnInit = function () {
    };
    EmployeeContractComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-employee-contract',
            template: __webpack_require__(/*! ./employee-contract.component.html */ "./src/app/hr/components/employee-contract/employee-contract.component.html"),
            styles: [__webpack_require__(/*! ./employee-contract.component.scss */ "./src/app/hr/components/employee-contract/employee-contract.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], EmployeeContractComponent);
    return EmployeeContractComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-control-panel/employee-control-panel.component.html":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/components/employee-control-panel/employee-control-panel.component.html ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template [headerClass]=\"'sub_header_template_main2'\">\r\n    <span class=\"action_header\"><i class=\"fas fa-arrow-left\" (click)=\"backClick()\"></i> &nbsp;Employee Control Panel\r\n    <hum-button [type]=\"'edit'\" [text]=\"'EDIT DETAILS'\"></hum-button>\r\n    <hum-button [type]=\"'delete'\" [text]=\"'DELETE'\"></hum-button>\r\n    <hum-button [type]=\"'exclamation'\" [text]=\"'TERMINATE'\"></hum-button>\r\n  </span>\r\n  <div class=\"action_section\">\r\n    <hum-button [type]=\"'text'\" [text]=\"'VIEW DETAILS'\" (click)=\"showEmployeeDetails()\"></hum-button>\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n\r\n<div class=\"row\">\r\n  <div class=\"col-md-12\">\r\n    <mat-tab-group>\r\n      <mat-tab label=\"HISTORY\">\r\n        <ng-template matTabContent>\r\n         <app-employee-history></app-employee-history>\r\n        </ng-template>\r\n      </mat-tab>\r\n      <mat-tab label=\"LEAVE\">\r\n        <ng-template matTabContent>\r\n          <app-employee-leave></app-employee-leave>\r\n        </ng-template>\r\n      </mat-tab>\r\n      <mat-tab label=\"ATTENDANCE\">\r\n        <ng-template matTabContent>\r\n          <app-employee-attendance></app-employee-attendance>\r\n        </ng-template>\r\n      </mat-tab>\r\n      <mat-tab label=\"CONTRACT\">\r\n        <ng-template matTabContent>\r\n          <app-employee-contract></app-employee-contract>\r\n        </ng-template>\r\n      </mat-tab>\r\n      <mat-tab label=\"TAX & PENSION\">\r\n        <ng-template matTabContent>\r\n          <app-employee-pension></app-employee-pension>\r\n        </ng-template>\r\n      </mat-tab>\r\n      <mat-tab label=\"SALARY CONFIG\">\r\n        <ng-template matTabContent>\r\n          <app-employee-salary-config></app-employee-salary-config>\r\n        </ng-template>\r\n      </mat-tab>\r\n      <mat-tab label=\"RESIGNATION\">\r\n        <ng-template matTabContent>\r\n          <app-employee-resignation [employeeDetail]=\"employeeDetail.employeeDetail\"></app-employee-resignation>\r\n        </ng-template>\r\n      </mat-tab>\r\n    </mat-tab-group>\r\n  </div>\r\n</div>\r\n  <app-employee-detail [employeeId] =\"employeeId\"></app-employee-detail>\r\n\r\n\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-control-panel/employee-control-panel.component.scss":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/components/employee-control-panel/employee-control-panel.component.scss ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtY29udHJvbC1wYW5lbC9lbXBsb3llZS1jb250cm9sLXBhbmVsLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/components/employee-control-panel/employee-control-panel.component.ts":
/*!******************************************************************************************!*\
  !*** ./src/app/hr/components/employee-control-panel/employee-control-panel.component.ts ***!
  \******************************************************************************************/
/*! exports provided: EmployeeControlPanelComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeControlPanelComponent", function() { return EmployeeControlPanelComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _employee_detail_employee_detail_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../employee-detail/employee-detail.component */ "./src/app/hr/components/employee-detail/employee-detail.component.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var EmployeeControlPanelComponent = /** @class */ (function () {
    function EmployeeControlPanelComponent(activatedRoute, router) {
        var _this = this;
        this.activatedRoute = activatedRoute;
        this.router = router;
        this.activatedRoute.params.subscribe(function (params) {
            _this.employeeId = +params['id'];
        });
    }
    EmployeeControlPanelComponent.prototype.ngOnInit = function () {
    };
    EmployeeControlPanelComponent.prototype.showEmployeeDetails = function () {
        this.employeeDetail.show();
    };
    EmployeeControlPanelComponent.prototype.backClick = function () {
        this.router.navigate(['/hr/employees']);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])(_employee_detail_employee_detail_component__WEBPACK_IMPORTED_MODULE_1__["EmployeeDetailComponent"]),
        __metadata("design:type", _employee_detail_employee_detail_component__WEBPACK_IMPORTED_MODULE_1__["EmployeeDetailComponent"])
    ], EmployeeControlPanelComponent.prototype, "employeeDetail", void 0);
    EmployeeControlPanelComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-employee-control-panel',
            template: __webpack_require__(/*! ./employee-control-panel.component.html */ "./src/app/hr/components/employee-control-panel/employee-control-panel.component.html"),
            styles: [__webpack_require__(/*! ./employee-control-panel.component.scss */ "./src/app/hr/components/employee-control-panel/employee-control-panel.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"], _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]])
    ], EmployeeControlPanelComponent);
    return EmployeeControlPanelComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-detail/employee-detail.component.html":
/*!******************************************************************************!*\
  !*** ./src/app/hr/components/employee-detail/employee-detail.component.html ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<hum-config-card [showCard]=\"showDetail\" (cardState)=\"getState($event)\">\r\n  <div title>Employee Details</div>\r\n  <!-- <div subtitle>Please select all the fields that should be displayed in the purchases list and also the PDF\r\n    export.</div> -->\r\n  <div content humAddScroll [height]=\"90\">\r\n      <div subtitle></div>\r\n      <h5>Identity Details</h5>\r\n      <table class=\"table table-striped\">\r\n        <tr>\r\n          <td>First Name</td>\r\n          <td>{{employeeDetail.FirstName}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Last Name</td>\r\n          <td>{{employeeDetail.LastName}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Email</td>\r\n          <td>{{employeeDetail.Email}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Phone Number</td>\r\n          <td>{{employeeDetail.Phone}}</td>\r\n        </tr>\r\n      </table>\r\n      <div subtitle></div>\r\n      <h5>Personal Details</h5>\r\n      <table class=\"table table-striped\">\r\n        <tr>\r\n          <td>Sex</td>\r\n          <td>{{employeeDetail.Sex}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Date Of Birth</td>\r\n          <td>{{employeeDetail.DateOfBirth}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Country</td>\r\n          <td>{{employeeDetail.Country}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>State/Village/Province</td>\r\n          <td>{{employeeDetail.State}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Profession</td>\r\n          <td>{{employeeDetail.Profession}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Qualification</td>\r\n          <td>{{employeeDetail.Qualification}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Experience Year</td>\r\n          <td>{{employeeDetail.ExperienceYear}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Experience Month</td>\r\n          <td>{{employeeDetail.ExperienceMonth}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Previous Work</td>\r\n          <td>{{employeeDetail.PreviousWork}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Current Address</td>\r\n          <td>{{employeeDetail.CurrentAddress}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Permanent Address</td>\r\n          <td>{{employeeDetail.PermanentAddress}}</td>\r\n        </tr>\r\n      </table>\r\n      <div subtitle></div>\r\n      <h5>Professional Details</h5>\r\n      <table class=\"table table-striped\">\r\n        <tr>\r\n          <td>Employement Status</td>\r\n          <td>{{employeeDetail.EmployementStatus}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Duty Station</td>\r\n          <td>{{employeeDetail.DutyStation}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Hired On</td>\r\n          <td>{{employeeDetail.HiredOn}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Attendance Group</td>\r\n          <td>{{employeeDetail.AttendanceGroup}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Job Description</td>\r\n          <td>{{employeeDetail.JobDescription}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Resigned</td>\r\n          <td>{{employeeDetail.Resigned}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Resigned On</td>\r\n          <td>{{employeeDetail.ResignedOn}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Resigned Reason</td>\r\n          <td>{{employeeDetail.ResignedReason}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Terminated</td>\r\n          <td>{{employeeDetail.Terminated}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Terminated On</td>\r\n          <td>{{employeeDetail.TerminatedOn}}</td>\r\n        </tr>\r\n        <tr>\r\n          <td>Terminated Reason</td>\r\n          <td>{{employeeDetail.TerminationReason}}</td>\r\n        </tr>\r\n      </table>\r\n\r\n  </div>\r\n  <div footer>\r\n  </div>\r\n</hum-config-card>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-detail/employee-detail.component.scss":
/*!******************************************************************************!*\
  !*** ./src/app/hr/components/employee-detail/employee-detail.component.scss ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".code-container {\n  display: -webkit-box;\n  display: flex;\n  -webkit-box-orient: vertical;\n  -webkit-box-direction: normal;\n          flex-direction: column; }\n\n.code-container > * {\n  width: 100%; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvaHIvY29tcG9uZW50cy9lbXBsb3llZS1kZXRhaWwvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcaHJcXGNvbXBvbmVudHNcXGVtcGxveWVlLWRldGFpbFxcZW1wbG95ZWUtZGV0YWlsLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0Usb0JBQWE7RUFBYixhQUFhO0VBQ2IsNEJBQXNCO0VBQXRCLDZCQUFzQjtVQUF0QixzQkFBc0IsRUFBQTs7QUFHeEI7RUFDRSxXQUFXLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9oci9jb21wb25lbnRzL2VtcGxveWVlLWRldGFpbC9lbXBsb3llZS1kZXRhaWwuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyIuY29kZS1jb250YWluZXIge1xyXG4gIGRpc3BsYXk6IGZsZXg7XHJcbiAgZmxleC1kaXJlY3Rpb246IGNvbHVtbjtcclxufVxyXG5cclxuLmNvZGUtY29udGFpbmVyID4gKiB7XHJcbiAgd2lkdGg6IDEwMCU7XHJcbn1cclxuIl19 */"

/***/ }),

/***/ "./src/app/hr/components/employee-detail/employee-detail.component.ts":
/*!****************************************************************************!*\
  !*** ./src/app/hr/components/employee-detail/employee-detail.component.ts ***!
  \****************************************************************************/
/*! exports provided: EmployeeDetailComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeDetailComponent", function() { return EmployeeDetailComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_hr_control_panel_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../services/hr-control-panel.service */ "./src/app/hr/services/hr-control-panel.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var EmployeeDetailComponent = /** @class */ (function () {
    function EmployeeDetailComponent(hrControlPanelService, toAstr) {
        this.hrControlPanelService = hrControlPanelService;
        this.toAstr = toAstr;
        this.showDetail = false;
    }
    EmployeeDetailComponent.prototype.ngOnInit = function () {
        this.onModelInIt();
    };
    EmployeeDetailComponent.prototype.ngOnChanges = function () {
        this.getEmployeeDetail(this.employeeId);
    };
    EmployeeDetailComponent.prototype.onModelInIt = function () {
        this.employeeDetail = {
            AttendanceGroup: '',
            Country: '',
            CurrentAddress: '',
            DateOfBirth: '',
            DutyStation: '',
            Email: '',
            EmployementStatus: '',
            ExperienceMonth: '',
            ExperienceYear: '',
            FirstName: '',
            HiredOn: '',
            JobDescription: '',
            LastName: '',
            PermanentAddress: '',
            Phone: '',
            PreviousWork: '',
            Profession: '',
            Qualification: '',
            Resigned: '',
            ResignedOn: '',
            ResignedReason: '',
            Sex: '',
            State: '',
            Terminated: '',
            TerminatedOn: '',
            TerminationReason: '',
            IsResigned: false,
            ResignationStatus: 0
        };
    };
    EmployeeDetailComponent.prototype.show = function () {
        this.showDetail = true;
    };
    EmployeeDetailComponent.prototype.getState = function (e) {
        this.showDetail = e;
    };
    EmployeeDetailComponent.prototype.getEmployeeDetail = function (id) {
        var _this = this;
        this.hrControlPanelService.getEmployeeDetail(id).subscribe(function (x) {
            if (x.EmployeeDetail) {
                _this.employeeDetail = {
                    AttendanceGroup: x.EmployeeDetail.AttendanceGroup,
                    Country: x.EmployeeDetail.Country,
                    CurrentAddress: x.EmployeeDetail.CurrentAddress,
                    DateOfBirth: x.EmployeeDetail.DateOfBirth,
                    DutyStation: x.EmployeeDetail.DutyStation,
                    Email: x.EmployeeDetail.Email,
                    EmployementStatus: x.EmployeeDetail.EmployementStatus,
                    ExperienceMonth: x.EmployeeDetail.ExperienceMonth,
                    ExperienceYear: x.EmployeeDetail.ExperienceYear,
                    FirstName: x.EmployeeDetail.FirstName,
                    HiredOn: x.EmployeeDetail.HiredOn,
                    JobDescription: x.EmployeeDetail.JobDescription,
                    LastName: x.EmployeeDetail.LastName,
                    PermanentAddress: x.EmployeeDetail.PermanentAddress,
                    Phone: x.EmployeeDetail.Phone,
                    PreviousWork: x.EmployeeDetail.PreviousWork,
                    Profession: x.EmployeeDetail.Profession,
                    Qualification: x.EmployeeDetail.Qualification,
                    Resigned: x.EmployeeDetail.Resigned,
                    ResignedOn: x.EmployeeDetail.ResignedOn,
                    ResignedReason: x.EmployeeDetail.ResignedReason,
                    Sex: x.EmployeeDetail.Sex,
                    State: x.EmployeeDetail.State,
                    Terminated: x.EmployeeDetail.Terminated,
                    TerminatedOn: x.EmployeeDetail.TerminatedOn,
                    TerminationReason: x.EmployeeDetail.TerminationReason,
                    IsResigned: x.EmployeeDetail.IsResigned,
                    ResignationStatus: x.EmployeeDetail.ResignationStatus
                };
            }
        }, function (error) {
            _this.toAstr.warning(error);
        });
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], EmployeeDetailComponent.prototype, "employeeId", void 0);
    EmployeeDetailComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-employee-detail',
            template: __webpack_require__(/*! ./employee-detail.component.html */ "./src/app/hr/components/employee-detail/employee-detail.component.html"),
            styles: [__webpack_require__(/*! ./employee-detail.component.scss */ "./src/app/hr/components/employee-detail/employee-detail.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_hr_control_panel_service__WEBPACK_IMPORTED_MODULE_1__["HrControlPanelService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"]])
    ], EmployeeDetailComponent);
    return EmployeeDetailComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.html":
/*!*****************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.html ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Add Information Regarding Close Relatives\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n      <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n    </button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"closeRelativeDetailForm\" (ngSubmit)=\"onFormSubmit(closeRelativeDetailForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Name\" placeholder=\"Name\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Relationship\" placeholder=\"Relationship\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Position\" placeholder=\"Position\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Organization\" placeholder=\"Organization\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Email\" placeholder=\"Email\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"PhoneNo\" placeholder=\"Phone Number\" minlength=\"10\" maxlength=\"14\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div mat-dialog-actions class=\"pull-right\">\r\n                <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n                <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n                <hum-button (click)=\"onCancelPopup()\" [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n            </div>\r\n        </div>\r\n    </form>\r\n</div>"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.scss":
/*!*****************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.scss ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtaGlzdG9yeS9hZGQtY2xvc2UtcmVsYXRpdmUvYWRkLWNsb3NlLXJlbGF0aXZlLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.ts":
/*!***************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.ts ***!
  \***************************************************************************************************/
/*! exports provided: AddCloseRelativeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddCloseRelativeComponent", function() { return AddCloseRelativeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/employee-history.service */ "./src/app/hr/services/employee-history.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var AddCloseRelativeComponent = /** @class */ (function () {
    function AddCloseRelativeComponent(fb, toastr, employeeHistoryService, dialogRef, data) {
        this.fb = fb;
        this.toastr = toastr;
        this.employeeHistoryService = employeeHistoryService;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isFormSubmitted = false;
        this.onAddCloseRelativeDetailListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.closeRelativeDetailForm = this.fb.group({
            EmployeeID: [''],
            Name: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Relationship: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Position: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Organization: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Email: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].email]],
            PhoneNo: [
                null,
                [
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required,
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].pattern(/^-?(0|[1-9]\d*)?$/),
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].minLength(10),
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].maxLength(14)
                ]
            ]
        });
    }
    AddCloseRelativeComponent.prototype.ngOnInit = function () {
        this.employeeId = this.data.employeeId;
        this.closeRelativeDetailForm.controls['EmployeeID'].setValue(this.employeeId);
    };
    AddCloseRelativeComponent.prototype.onFormSubmit = function (data) {
        var _this = this;
        if (this.closeRelativeDetailForm.valid) {
            this.isFormSubmitted = true;
            this.employeeHistoryService.addCloseRelativeDetail(data).subscribe(function (x) {
                if (x.StatusCode === 200) {
                    _this.toastr.success('Success');
                    _this.isFormSubmitted = false;
                    _this.dialogRef.close();
                    _this.AddCloseRelativeDetailListRefresh();
                }
                else {
                    _this.toastr.warning(x.Message);
                    _this.isFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning(error);
                _this.isFormSubmitted = false;
            });
        }
    };
    //#region "Add Close Relative Detail List Refresh"
    AddCloseRelativeComponent.prototype.AddCloseRelativeDetailListRefresh = function () {
        this.onAddCloseRelativeDetailListRefresh.emit();
    };
    //#endregion
    //#region "onCancelPopup"
    AddCloseRelativeComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddCloseRelativeComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddCloseRelativeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-close-relative',
            template: __webpack_require__(/*! ./add-close-relative.component.html */ "./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.html"),
            styles: [__webpack_require__(/*! ./add-close-relative.component.scss */ "./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__["EmployeeHistoryService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object])
    ], AddCloseRelativeComponent);
    return AddCloseRelativeComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-history/add-education/add-education.component.html":
/*!*******************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-education/add-education.component.html ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Add Education Detail\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n    <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n  </button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"educationForm\" (ngSubmit)=\"onFormSubmit(educationForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput [matDatepicker]=\"EducationFromPicker\" placeholder=\"Education From\" formControlName=\"EducationFrom\" />\r\n                                <mat-datepicker-toggle matSuffix [for]=\"EducationFromPicker\"></mat-datepicker-toggle>\r\n                                <mat-datepicker #EducationFromPicker></mat-datepicker>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput [matDatepicker]=\"EducationToPicker\" placeholder=\"Education To\" formControlName=\"EducationTo\" />\r\n                                <mat-datepicker-toggle matSuffix [for]=\"EducationToPicker\"></mat-datepicker-toggle>\r\n                                <mat-datepicker #EducationToPicker></mat-datepicker>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Degree\" placeholder=\"Degree\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"FieldOfStudy\" placeholder=\"Field Of Study\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Institute\" placeholder=\"Institute\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div mat-dialog-actions class=\"pull-right\">\r\n                <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n                <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n                <hum-button (click)=\"onCancelPopup()\" [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n            </div>\r\n        </div>\r\n    </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-education/add-education.component.scss":
/*!*******************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-education/add-education.component.scss ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtaGlzdG9yeS9hZGQtZWR1Y2F0aW9uL2FkZC1lZHVjYXRpb24uY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-education/add-education.component.ts":
/*!*****************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-education/add-education.component.ts ***!
  \*****************************************************************************************/
/*! exports provided: AddEducationComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddEducationComponent", function() { return AddEducationComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/employee-history.service */ "./src/app/hr/services/employee-history.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var AddEducationComponent = /** @class */ (function () {
    function AddEducationComponent(fb, toastr, employeeHistoryService, dialogRef, data) {
        this.fb = fb;
        this.toastr = toastr;
        this.employeeHistoryService = employeeHistoryService;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isFormSubmitted = false;
        this.onAddEducationListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.educationForm = this.fb.group({
            EmployeeID: [''],
            EducationFrom: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            EducationTo: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Degree: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            FieldOfStudy: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Institute: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]]
        });
    }
    AddEducationComponent.prototype.ngOnInit = function () {
        this.employeeId = this.data.employeeId;
        this.educationForm.controls['EmployeeID'].setValue(this.employeeId);
    };
    AddEducationComponent.prototype.onFormSubmit = function (data) {
        var _this = this;
        this.isFormSubmitted = true;
        if (this.educationForm.valid) {
            this.employeeHistoryService.addEducationDetail(data).subscribe(function (x) {
                if (x.StatusCode === 200) {
                    _this.toastr.success('Success');
                    _this.isFormSubmitted = false;
                    _this.dialogRef.close();
                    _this.AddEducationListRefresh();
                }
                else {
                    _this.toastr.warning(x.Message);
                    _this.isFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning(error);
                _this.isFormSubmitted = false;
            });
        }
    };
    //#region "On historical list refresh"
    AddEducationComponent.prototype.AddEducationListRefresh = function () {
        this.onAddEducationListRefresh.emit();
    };
    //#endregion
    //#region "onCancelPopup"
    AddEducationComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddEducationComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddEducationComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-education',
            template: __webpack_require__(/*! ./add-education.component.html */ "./src/app/hr/components/employee-history/add-education/add-education.component.html"),
            styles: [__webpack_require__(/*! ./add-education.component.scss */ "./src/app/hr/components/employee-history/add-education/add-education.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__["EmployeeHistoryService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object])
    ], AddEducationComponent);
    return AddEducationComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.html":
/*!*****************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.html ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Add Historical Log\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n  <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n</button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"historicalLogForm\" (ngSubmit)=\"onFormSubmit(historicalLogForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-12 col-sm-12\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput [matDatepicker]=\"HistoryDatePicker\" placeholder=\"History Date\" formControlName=\"HistoryDate\" />\r\n                                <mat-datepicker-toggle matSuffix [for]=\"HistoryDatePicker\"></mat-datepicker-toggle>\r\n                                <mat-datepicker #HistoryDatePicker></mat-datepicker>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-12 col-sm-12\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <textarea rows=\"4\" matInput formControlName=\"Description\" placeholder=\"Description\"></textarea>\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div mat-dialog-actions class=\"pull-right\">\r\n                <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n                <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n                <hum-button (click)=\"onCancelPopup()\" [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n            </div>\r\n        </div>\r\n    </form>\r\n</div>"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.scss":
/*!*****************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.scss ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtaGlzdG9yeS9hZGQtaGlzdG9yaWNhbC1sb2cvYWRkLWhpc3RvcmljYWwtbG9nLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.ts":
/*!***************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.ts ***!
  \***************************************************************************************************/
/*! exports provided: AddHistoricalLogComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddHistoricalLogComponent", function() { return AddHistoricalLogComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/employee-history.service */ "./src/app/hr/services/employee-history.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var AddHistoricalLogComponent = /** @class */ (function () {
    function AddHistoricalLogComponent(fb, toastr, employeeHistoryService, dialogRef, data) {
        this.fb = fb;
        this.toastr = toastr;
        this.employeeHistoryService = employeeHistoryService;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isFormSubmitted = false;
        this.onAddHistoricalListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.historicalLogForm = this.fb.group({
            EmployeeID: [''],
            Description: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            HistoryDate: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]]
        });
    }
    AddHistoricalLogComponent.prototype.ngOnInit = function () {
        this.employeeId = this.data.employeeId;
        this.historicalLogForm.controls['EmployeeID'].setValue(this.employeeId);
    };
    AddHistoricalLogComponent.prototype.onFormSubmit = function (data) {
        var _this = this;
        this.isFormSubmitted = true;
        if (this.historicalLogForm.valid) {
            this.employeeHistoryService.addHistoricalLogDetail(data).subscribe(function (x) {
                if (x.StatusCode === 200) {
                    _this.toastr.success('Success');
                    _this.isFormSubmitted = false;
                    _this.dialogRef.close();
                    _this.AddHistoricalListRefresh();
                }
                else {
                    _this.toastr.warning(x.Message);
                    _this.isFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning(error);
                _this.isFormSubmitted = false;
            });
        }
    };
    //#region "On historical list refresh"
    AddHistoricalLogComponent.prototype.AddHistoricalListRefresh = function () {
        this.onAddHistoricalListRefresh.emit();
    };
    //#endregion
    //#region "onCancelPopup"
    AddHistoricalLogComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddHistoricalLogComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddHistoricalLogComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-historical-log',
            template: __webpack_require__(/*! ./add-historical-log.component.html */ "./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.html"),
            styles: [__webpack_require__(/*! ./add-historical-log.component.scss */ "./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__["EmployeeHistoryService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object])
    ], AddHistoricalLogComponent);
    return AddHistoricalLogComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.html":
/*!***********************************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.html ***!
  \***********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Add Employeement History Outside of Country\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n<mat-icon aria-label=\"clear\">clear</mat-icon>\r\n</button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"historyOutsideCountryDetailForm\" (ngSubmit)=\"onFormSubmit(historyOutsideCountryDetailForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput [matDatepicker]=\"EmploymentFromPicker\" placeholder=\"Employment From\" formControlName=\"EmploymentFrom\" />\r\n                                <mat-datepicker-toggle matSuffix [for]=\"EmploymentFromPicker\"></mat-datepicker-toggle>\r\n                                <mat-datepicker #EmploymentFromPicker></mat-datepicker>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput [matDatepicker]=\"EmploymentToPicker\" placeholder=\"Employment To\" formControlName=\"EmploymentTo\" />\r\n                                <mat-datepicker-toggle matSuffix [for]=\"EmploymentToPicker\"></mat-datepicker-toggle>\r\n                                <mat-datepicker #EmploymentToPicker></mat-datepicker>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Organization\" placeholder=\"Organization\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"MonthlySalary\" placeholder=\"Monthly Salary\" type=\"number\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"ReasonForLeaving\" placeholder=\"Reason For Leaving\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Position\" placeholder=\"Position\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div mat-dialog-actions class=\"pull-right\">\r\n                <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n                <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n                <hum-button (click)=\"onCancelPopup()\" [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n            </div>\r\n        </div>\r\n    </form>\r\n</div>"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.scss":
/*!***********************************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.scss ***!
  \***********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtaGlzdG9yeS9hZGQtaGlzdG9yeS1vdXRzaWRlLWNvdW50cnkvYWRkLWhpc3Rvcnktb3V0c2lkZS1jb3VudHJ5LmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.ts":
/*!*********************************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.ts ***!
  \*********************************************************************************************************************/
/*! exports provided: AddHistoryOutsideCountryComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddHistoryOutsideCountryComponent", function() { return AddHistoryOutsideCountryComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/employee-history.service */ "./src/app/hr/services/employee-history.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var AddHistoryOutsideCountryComponent = /** @class */ (function () {
    function AddHistoryOutsideCountryComponent(fb, toastr, employeeHistoryService, dialogRef, data) {
        this.fb = fb;
        this.toastr = toastr;
        this.employeeHistoryService = employeeHistoryService;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isFormSubmitted = false;
        this.onAddHistoryOutsideCountryListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.historyOutsideCountryDetailForm = this.fb.group({
            EmployeeID: [''],
            EmploymentFrom: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            EmploymentTo: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Organization: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            MonthlySalary: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            ReasonForLeaving: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Position: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]]
        });
    }
    AddHistoryOutsideCountryComponent.prototype.ngOnInit = function () {
        this.employeeId = this.data.employeeId;
        this.historyOutsideCountryDetailForm.controls['EmployeeID'].setValue(this.employeeId);
    };
    AddHistoryOutsideCountryComponent.prototype.onFormSubmit = function (data) {
        var _this = this;
        if (this.historyOutsideCountryDetailForm.valid) {
            this.isFormSubmitted = true;
            this.employeeHistoryService.addHistoryOutsideCountry(data).subscribe(function (x) {
                if (x.StatusCode === 200) {
                    _this.toastr.success('Success');
                    _this.isFormSubmitted = false;
                    _this.dialogRef.close();
                    _this.AddHistoryOutsideCountryListRefresh();
                }
                else {
                    _this.toastr.warning(x.Message);
                    _this.isFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning(error);
                _this.isFormSubmitted = false;
            });
        }
    };
    //#region "On add history Outside Country refresh"
    AddHistoryOutsideCountryComponent.prototype.AddHistoryOutsideCountryListRefresh = function () {
        this.onAddHistoryOutsideCountryListRefresh.emit();
    };
    //#endregion
    //#region "onCancelPopup"
    AddHistoryOutsideCountryComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddHistoryOutsideCountryComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddHistoryOutsideCountryComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-history-outside-country',
            template: __webpack_require__(/*! ./add-history-outside-country.component.html */ "./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.html"),
            styles: [__webpack_require__(/*! ./add-history-outside-country.component.scss */ "./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__["EmployeeHistoryService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object])
    ], AddHistoryOutsideCountryComponent);
    return AddHistoryOutsideCountryComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-history/add-language/add-language.component.html":
/*!*****************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-language/add-language.component.html ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Add Language\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n      <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n    </button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"languageDetailForm\" (ngSubmit)=\"onFormSubmit(languageDetailForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-3 col-sm-3\">\r\n                            <lib-hum-dropdown formControlName=\"LanguageId\" [validation]=\"\r\n                            languageDetailForm.controls['LanguageId'].hasError('required')\r\n                    \" [options]=\"languageList$\" [placeHolder]=\"'Language'\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-3 col-sm-3\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <mat-select placeholder=\"Reading\" formControlName=\"Reading\">\r\n                                    <mat-option *ngFor=\"let item of ratingBasedDropDown\" [value]=\"item.Id\">\r\n                                        {{ item.value }}\r\n                                    </mat-option>\r\n                                </mat-select>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-3 col-sm-3\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <mat-select placeholder=\"Writing\" formControlName=\"Writing\">\r\n                                    <mat-option *ngFor=\"let item of ratingBasedDropDown\" [value]=\"item.Id\">\r\n                                        {{ item.value }}\r\n                                    </mat-option>\r\n                                </mat-select>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-3 col-sm-3\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <mat-select placeholder=\"Listening\" formControlName=\"Listening\">\r\n                                    <mat-option *ngFor=\"let item of ratingBasedDropDown\" [value]=\"item.Id\">\r\n                                        {{ item.value }}\r\n                                    </mat-option>\r\n                                </mat-select>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-3 col-sm-3\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <mat-select placeholder=\"Speaking\" formControlName=\"Speaking\">\r\n                                    <mat-option *ngFor=\"let item of ratingBasedDropDown\" [value]=\"item.Id\">\r\n                                        {{ item.value }}\r\n                                    </mat-option>\r\n                                </mat-select>\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div mat-dialog-actions class=\"pull-right\">\r\n                <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n                <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n                <hum-button (click)=\"onCancelPopup()\" [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n            </div>\r\n        </div>\r\n    </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-language/add-language.component.scss":
/*!*****************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-language/add-language.component.scss ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtaGlzdG9yeS9hZGQtbGFuZ3VhZ2UvYWRkLWxhbmd1YWdlLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-language/add-language.component.ts":
/*!***************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-language/add-language.component.ts ***!
  \***************************************************************************************/
/*! exports provided: AddLanguageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddLanguageComponent", function() { return AddLanguageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/hr/services/employee-history.service */ "./src/app/hr/services/employee-history.service.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};







var AddLanguageComponent = /** @class */ (function () {
    function AddLanguageComponent(fb, commonLoader, toastr, employeeHistoryService, dialogRef, data) {
        this.fb = fb;
        this.commonLoader = commonLoader;
        this.toastr = toastr;
        this.employeeHistoryService = employeeHistoryService;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isFormSubmitted = false;
        this.onLanguageDetailListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.ratingBasedDropDown = [
            {
                Id: 1,
                value: '1-Poor'
            },
            {
                Id: 2,
                value: '2-Good'
            },
            {
                Id: 3,
                value: '3-Very Good'
            },
            {
                Id: 4,
                value: '4-Excellent'
            }
        ];
        this.languageDetailForm = this.fb.group({
            EmployeeID: [''],
            LanguageId: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Reading: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Writing: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Listening: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Speaking: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]]
        });
    }
    AddLanguageComponent.prototype.ngOnInit = function () {
        this.employeeId = this.data.employeeId;
        this.languageDetailForm.controls['EmployeeID'].setValue(this.employeeId);
        this.getLanguageList();
    };
    //#region "get Language  List"
    AddLanguageComponent.prototype.getLanguageList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.employeeHistoryService
            .GetLanguageList()
            .subscribe(function (x) {
            _this.commonLoader.hideLoader();
            if (x.data.LanguageDetail.length > 0) {
                _this.languageList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(x.data.LanguageDetail.map(function (y) {
                    return {
                        value: y.LanguageId,
                        name: y.LanguageName
                    };
                }));
            }
        }, function () {
            _this.commonLoader.hideLoader();
        });
    };
    //#endregion
    AddLanguageComponent.prototype.onFormSubmit = function (data) {
        var _this = this;
        if (this.languageDetailForm.valid) {
            this.isFormSubmitted = true;
            this.employeeHistoryService.addLanguageDetail(data).subscribe(function (x) {
                if (x.StatusCode === 200) {
                    _this.toastr.success('Success');
                    _this.isFormSubmitted = false;
                    _this.dialogRef.close();
                    _this.AddLanguageDetailListRefresh();
                }
                else {
                    _this.toastr.warning(x.Message);
                    _this.isFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning(error);
                _this.isFormSubmitted = false;
            });
        }
    };
    //#region "Add Language Detail List Refresh"
    AddLanguageComponent.prototype.AddLanguageDetailListRefresh = function () {
        this.onLanguageDetailListRefresh.emit();
    };
    //#endregion
    //#region "onCancelPopup"
    AddLanguageComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddLanguageComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddLanguageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-language',
            template: __webpack_require__(/*! ./add-language.component.html */ "./src/app/hr/components/employee-history/add-language/add-language.component.html"),
            styles: [__webpack_require__(/*! ./add-language.component.scss */ "./src/app/hr/components/employee-history/add-language/add-language.component.scss")]
        }),
        __param(5, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__["CommonLoaderService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_5__["EmployeeHistoryService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object])
    ], AddLanguageComponent);
    return AddLanguageComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.html":
/*!*************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.html ***!
  \*************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Add Other Skill\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n<mat-icon aria-label=\"clear\">clear</mat-icon>\r\n</button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"otherSkillForm\" (ngSubmit)=\"onFormSubmit(otherSkillForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"AbilityLevel\" placeholder=\"Ability Level\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Experience\" placeholder=\"Experience\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"TypeOfSkill\" placeholder=\"Type Of Skill\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-12 col-sm-12\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <textarea rows=\"4\" matInput formControlName=\"Remarks\" placeholder=\"Remarks\"></textarea>\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div mat-dialog-actions class=\"pull-right\">\r\n                <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n                <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n                <hum-button (click)=\"onCancelPopup()\" [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n            </div>\r\n        </div>\r\n    </form>\r\n</div>"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.scss":
/*!*************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.scss ***!
  \*************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtaGlzdG9yeS9hZGQtb3RoZXItc2tpbGxzL2FkZC1vdGhlci1za2lsbHMuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.ts":
/*!***********************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.ts ***!
  \***********************************************************************************************/
/*! exports provided: AddOtherSkillsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddOtherSkillsComponent", function() { return AddOtherSkillsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/employee-history.service */ "./src/app/hr/services/employee-history.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var AddOtherSkillsComponent = /** @class */ (function () {
    function AddOtherSkillsComponent(fb, toastr, employeeHistoryService, dialogRef, data) {
        this.fb = fb;
        this.toastr = toastr;
        this.employeeHistoryService = employeeHistoryService;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isFormSubmitted = false;
        this.onOtherSkillDetailListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.otherSkillForm = this.fb.group({
            EmployeeID: [''],
            AbilityLevel: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Experience: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Remarks: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            TypeOfSkill: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]]
        });
    }
    AddOtherSkillsComponent.prototype.ngOnInit = function () {
        this.employeeId = this.data.employeeId;
        this.otherSkillForm.controls['EmployeeID'].setValue(this.employeeId);
    };
    AddOtherSkillsComponent.prototype.onFormSubmit = function (data) {
        var _this = this;
        if (this.otherSkillForm.valid) {
            this.isFormSubmitted = true;
            this.employeeHistoryService.addOtherSkillDetail(data).subscribe(function (x) {
                if (x.StatusCode === 200) {
                    _this.toastr.success('Success');
                    _this.isFormSubmitted = false;
                    _this.dialogRef.close();
                    _this.AddOtherSkillDetailListRefresh();
                }
                else {
                    _this.toastr.warning(x.Message);
                    _this.isFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning(error);
                _this.isFormSubmitted = false;
            });
        }
    };
    //#region "Add Other Skill Detail List Refresh"
    AddOtherSkillsComponent.prototype.AddOtherSkillDetailListRefresh = function () {
        this.onOtherSkillDetailListRefresh.emit();
    };
    //#endregion
    //#region "onCancelPopup"
    AddOtherSkillsComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddOtherSkillsComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddOtherSkillsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-other-skills',
            template: __webpack_require__(/*! ./add-other-skills.component.html */ "./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.html"),
            styles: [__webpack_require__(/*! ./add-other-skills.component.scss */ "./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__["EmployeeHistoryService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object])
    ], AddOtherSkillsComponent);
    return AddOtherSkillsComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.html":
/*!***************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.html ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Add Salary Budget\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n<mat-icon aria-label=\"clear\">clear</mat-icon>\r\n</button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"salaryBudgetForm\" (ngSubmit)=\"onFormSubmit(salaryBudgetForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <lib-hum-dropdown formControlName=\"CurrencyId\" [validation]=\"\r\n                      salaryBudgetForm.controls['CurrencyId'].hasError('required')\r\n                    \" [options]=\"currencyList$\" [placeHolder]=\"'Currency'\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"SalaryBudget\" placeholder=\"SalaryBudget\" type=\"number\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"BudgetDisbursed\" placeholder=\"Budget Disbursed\" type=\"number\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <lib-hum-dropdown formControlName=\"Year\" [validation]=\"\r\n                            salaryBudgetForm.controls['Year'].hasError(\r\n                              'required'\r\n                            )\r\n                          \" [options]=\"PreviousYearsList$\" [placeHolder]=\"'Year'\"></lib-hum-dropdown>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div mat-dialog-actions class=\"pull-right\">\r\n                <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n                <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n                <hum-button (click)=\"onCancelPopup()\" [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n            </div>\r\n        </div>\r\n    </form>\r\n</div>"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.scss":
/*!***************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.scss ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtaGlzdG9yeS9hZGQtc2FsYXJ5LWJ1ZGdldC9hZGQtc2FsYXJ5LWJ1ZGdldC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.ts":
/*!*************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.ts ***!
  \*************************************************************************************************/
/*! exports provided: AddSalaryBudgetComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddSalaryBudgetComponent", function() { return AddSalaryBudgetComponent; });
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/hr/services/employee-history.service */ "./src/app/hr/services/employee-history.service.ts");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};








var AddSalaryBudgetComponent = /** @class */ (function () {
    function AddSalaryBudgetComponent(fb, commonLoader, toastr, employeeHistoryService, globalSharedService, dialogRef, data) {
        this.fb = fb;
        this.commonLoader = commonLoader;
        this.toastr = toastr;
        this.employeeHistoryService = employeeHistoryService;
        this.globalSharedService = globalSharedService;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isFormSubmitted = false;
        this.onSalaryBudgetDetailListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_1__["EventEmitter"]();
        this.salaryBudgetForm = this.fb.group({
            EmployeeID: [''],
            BudgetDisbursed: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            CurrencyId: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            SalaryBudget: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            Year: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]]
        });
    }
    AddSalaryBudgetComponent.prototype.ngOnInit = function () {
        this.employeeId = this.data.employeeId;
        this.salaryBudgetForm.controls['EmployeeID'].setValue(this.employeeId);
        this.getPreviousYearsList();
        this.getCurrencyList();
    };
    //#region "Get all previous years list for ExperienceInYears dropdown"
    AddSalaryBudgetComponent.prototype.getPreviousYearsList = function () {
        this.PreviousYearsList$ = this.globalSharedService.getPreviousYearsList(40);
    };
    //#endregion
    //#region "get currency  List"
    AddSalaryBudgetComponent.prototype.getCurrencyList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.employeeHistoryService
            .GetCurrencyList()
            .subscribe(function (x) {
            _this.commonLoader.hideLoader();
            if (x.data.CurrencyList.length > 0) {
                _this.currencyList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_4__["of"])(x.data.CurrencyList.map(function (y) {
                    return {
                        value: y.CurrencyId,
                        name: y.CurrencyName
                    };
                }));
            }
        }, function () {
            _this.commonLoader.hideLoader();
        });
    };
    //#endregion
    AddSalaryBudgetComponent.prototype.onFormSubmit = function (data) {
        var _this = this;
        if (this.salaryBudgetForm.valid) {
            this.isFormSubmitted = true;
            this.employeeHistoryService.addSalaryBudgetDetail(data).subscribe(function (x) {
                if (x.StatusCode === 200) {
                    _this.toastr.success('Success');
                    _this.isFormSubmitted = false;
                    _this.dialogRef.close();
                    _this.AddSalaryBudgetDetailListRefresh();
                }
                else {
                    _this.toastr.warning(x.Message);
                    _this.isFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning(error);
                _this.isFormSubmitted = false;
            });
        }
    };
    //#region "Add Salary Budget Detail List Refresh"
    AddSalaryBudgetComponent.prototype.AddSalaryBudgetDetailListRefresh = function () {
        this.onSalaryBudgetDetailListRefresh.emit();
    };
    //#endregion
    //#region "onCancelPopup"
    AddSalaryBudgetComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddSalaryBudgetComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddSalaryBudgetComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-add-salary-budget',
            template: __webpack_require__(/*! ./add-salary-budget.component.html */ "./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.html"),
            styles: [__webpack_require__(/*! ./add-salary-budget.component.scss */ "./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.scss")]
        }),
        __param(6, Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormBuilder"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_7__["CommonLoaderService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_5__["ToastrService"],
            src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_6__["EmployeeHistoryService"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_0__["GlobalSharedService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"], Object])
    ], AddSalaryBudgetComponent);
    return AddSalaryBudgetComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.html":
/*!***********************************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.html ***!
  \***********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Add Information Three References Other Then Relatives\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n<mat-icon aria-label=\"clear\">clear</mat-icon>\r\n</button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"referenceDetailForm\" (ngSubmit)=\"onFormSubmit(referenceDetailForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Name\" placeholder=\"Name\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Relationship\" placeholder=\"Relationship\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Position\" placeholder=\"Position\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Organization\" placeholder=\"Organization\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"PhoneNo\" placeholder=\"Phone Number\" minlength=\"10\" maxlength=\"14\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Email\" placeholder=\"Email\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div mat-dialog-actions class=\"pull-right\">\r\n                <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n                <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n                <hum-button (click)=\"onCancelPopup()\" [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n            </div>\r\n        </div>\r\n    </form>\r\n</div>"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.scss":
/*!***********************************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.scss ***!
  \***********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtaGlzdG9yeS9hZGQtdGhyZWUtcmVmZXJlbmNlLWRldGFpbHMvYWRkLXRocmVlLXJlZmVyZW5jZS1kZXRhaWxzLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.ts":
/*!*********************************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.ts ***!
  \*********************************************************************************************************************/
/*! exports provided: AddThreeReferenceDetailsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddThreeReferenceDetailsComponent", function() { return AddThreeReferenceDetailsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/employee-history.service */ "./src/app/hr/services/employee-history.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var AddThreeReferenceDetailsComponent = /** @class */ (function () {
    function AddThreeReferenceDetailsComponent(fb, toastr, employeeHistoryService, dialogRef, data) {
        this.fb = fb;
        this.toastr = toastr;
        this.employeeHistoryService = employeeHistoryService;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isFormSubmitted = false;
        this.onThreeReferenceDetailListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.referenceDetailForm = this.fb.group({
            EmployeeID: [''],
            Name: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Relationship: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Position: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Organization: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            PhoneNo: [
                null,
                [
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required,
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].pattern(/^-?(0|[1-9]\d*)?$/),
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].minLength(10),
                    _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].maxLength(14)
                ]
            ],
            Email: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].email]]
        });
    }
    AddThreeReferenceDetailsComponent.prototype.ngOnInit = function () {
        this.employeeId = this.data.employeeId;
        this.referenceDetailForm.controls['EmployeeID'].setValue(this.employeeId);
    };
    AddThreeReferenceDetailsComponent.prototype.onFormSubmit = function (data) {
        var _this = this;
        if (this.referenceDetailForm.valid) {
            this.isFormSubmitted = true;
            this.employeeHistoryService.addThreeReferenceDetail(data).subscribe(function (x) {
                if (x.StatusCode === 200) {
                    _this.toastr.success('Success');
                    _this.isFormSubmitted = false;
                    _this.dialogRef.close();
                    _this.AddThreeReferenceDetailListRefresh();
                }
                else {
                    _this.toastr.warning(x.Message);
                    _this.isFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning(error);
                _this.isFormSubmitted = false;
            });
        }
    };
    //#region "Add Three Reference Detail List Refresh"
    AddThreeReferenceDetailsComponent.prototype.AddThreeReferenceDetailListRefresh = function () {
        this.onThreeReferenceDetailListRefresh.emit();
    };
    //#endregion
    //#region "onCancelPopup"
    AddThreeReferenceDetailsComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddThreeReferenceDetailsComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddThreeReferenceDetailsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-three-reference-details',
            template: __webpack_require__(/*! ./add-three-reference-details.component.html */ "./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.html"),
            styles: [__webpack_require__(/*! ./add-three-reference-details.component.scss */ "./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            src_app_hr_services_employee_history_service__WEBPACK_IMPORTED_MODULE_4__["EmployeeHistoryService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object])
    ], AddThreeReferenceDetailsComponent);
    return AddThreeReferenceDetailsComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-history/employee-history.component.html":
/*!********************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/employee-history.component.html ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-card [ngStyle]=\"scrollStyles\">\r\n    <div class=\"row\">\r\n        <lib-sub-header-template>\r\n            <span class=\"action_header\">Historical Log\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD ENTRY'\" (click)=\"addHistoricalLog()\"></hum-button>\r\n      </span>\r\n        </lib-sub-header-template>\r\n        <div class=\"col-md-6\">\r\n            <hum-table [headers]=\"historicalLogHeader$\" [isDefaultAction]=\"false\" [items]=\"historicalLogList$\" (actionClick)=\"actionEvents($event,'historicalLog')\" [actions]=\"actions\"></hum-table>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <lib-sub-header-template>\r\n            <span class=\"action_header\">Education\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD ENTRY'\" (click)=\"addEducation()\"></hum-button>\r\n      </span>\r\n        </lib-sub-header-template>\r\n        <div class=\"col-md-12\">\r\n            <hum-table [headers]=\"educationHeader$\" [isDefaultAction]=\"false\" [items]=\"educationList$\" (actionClick)=\"actionEvents($event,'education')\" [actions]=\"actions\"></hum-table>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <lib-sub-header-template>\r\n            <span class=\"action_header\">Employeement History Outside of Country\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD ENTRY'\" (click)=\"addHistoryOutsideCountry()\"></hum-button>\r\n      </span>\r\n        </lib-sub-header-template>\r\n        <div class=\"col-md-12\">\r\n            <hum-table [headers]=\"employeeHistoryOCHeader$\" [isDefaultAction]=\"false\" [items]=\"employeeHistoryOCList$\" (actionClick)=\"actionEvents($event,'outsideHistory')\" [actions]=\"actions\"></hum-table>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <lib-sub-header-template>\r\n            <span class=\"action_header\">Information Regarding Close Relatives\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD ENTRY'\" (click)=\"addCloseRelative()\"></hum-button>\r\n      </span>\r\n        </lib-sub-header-template>\r\n        <div class=\"col-md-12\">\r\n            <hum-table [headers]=\"infoOfCloseRelativeHeader$\" [isDefaultAction]=\"false\" [items]=\"employeeCloseRelativeList$\" (actionClick)=\"actionEvents($event,'closeReletive')\" [actions]=\"actions\"></hum-table>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <lib-sub-header-template>\r\n            <span class=\"action_header\">Information Three References Other Then Relatives\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD ENTRY'\" (click)=\"addThreeReference()\"></hum-button>\r\n      </span>\r\n        </lib-sub-header-template>\r\n        <div class=\"col-md-12\">\r\n            <hum-table [headers]=\"infoOfThreeRefHeader$\" [isDefaultAction]=\"false\" [items]=\"employeeThreeReferenceList$\" (actionClick)=\"actionEvents($event,'references')\" [actions]=\"actions\"></hum-table>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <lib-sub-header-template>\r\n            <span class=\"action_header\">Other Skill\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD ENTRY'\" (click)=\"addOtherSkill()\"></hum-button>\r\n      </span>\r\n        </lib-sub-header-template>\r\n        <div class=\"col-md-12\">\r\n            <hum-table [headers]=\"otherSkillHeader$\" [isDefaultAction]=\"false\" [items]=\"employeeOtherSkillList$\" (actionClick)=\"actionEvents($event,'otherSkill')\" [actions]=\"actions\"></hum-table>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <lib-sub-header-template>\r\n            <span class=\"action_header\">Salary Budget\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD ENTRY'\" (click)=\"addSalaryBudget()\"></hum-button>\r\n      </span>\r\n        </lib-sub-header-template>\r\n        <div class=\"col-md-12\">\r\n            <hum-table [headers]=\"salarybudgetHeader$\" [isDefaultAction]=\"false\" [items]=\"employeeSalaryBudgetList$\" (actionClick)=\"actionEvents($event,'salaryBudget')\" [actions]=\"actions\"></hum-table>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <lib-sub-header-template>\r\n            <span class=\"action_header\">Language Employee Can Speak\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD ENTRY'\" (click)=\"addLanguage()\"></hum-button>\r\n      </span>\r\n        </lib-sub-header-template>\r\n        <div class=\"col-md-12\">\r\n            <hum-table [headers]=\"languageHeader$\" [isDefaultAction]=\"false\" [items]=\"employeeLanguageList$\" (actionClick)=\"actionEvents($event,'language')\" [actions]=\"actions\"></hum-table>\r\n        </div>\r\n    </div><br><br><br>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-history/employee-history.component.scss":
/*!********************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/employee-history.component.scss ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtaGlzdG9yeS9lbXBsb3llZS1oaXN0b3J5LmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/components/employee-history/employee-history.component.ts":
/*!******************************************************************************!*\
  !*** ./src/app/hr/components/employee-history/employee-history.component.ts ***!
  \******************************************************************************/
/*! exports provided: EmployeeHistoryComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeHistoryComponent", function() { return EmployeeHistoryComponent; });
/* harmony import */ var _add_salary_budget_add_salary_budget_component__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./add-salary-budget/add-salary-budget.component */ "./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.ts");
/* harmony import */ var _add_three_reference_details_add_three_reference_details_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./add-three-reference-details/add-three-reference-details.component */ "./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.ts");
/* harmony import */ var _add_other_skills_add_other_skills_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./add-other-skills/add-other-skills.component */ "./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.ts");
/* harmony import */ var _add_language_add_language_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./add-language/add-language.component */ "./src/app/hr/components/employee-history/add-language/add-language.component.ts");
/* harmony import */ var _add_history_outside_country_add_history_outside_country_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./add-history-outside-country/add-history-outside-country.component */ "./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.ts");
/* harmony import */ var _add_close_relative_add_close_relative_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./add-close-relative/add-close-relative.component */ "./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.ts");
/* harmony import */ var _add_education_add_education_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./add-education/add-education.component */ "./src/app/hr/components/employee-history/add-education/add-education.component.ts");
/* harmony import */ var _add_historical_log_add_historical_log_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./add-historical-log/add-historical-log.component */ "./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.ts");
/* harmony import */ var _services_employee_history_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./../../services/employee-history.service */ "./src/app/hr/services/employee-history.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _services_hr_service__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../../services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


















var EmployeeHistoryComponent = /** @class */ (function () {
    function EmployeeHistoryComponent(dialog, hrService, routeActive, commonLoader, datePipe, employeeHistoryService) {
        this.dialog = dialog;
        this.hrService = hrService;
        this.routeActive = routeActive;
        this.commonLoader = commonLoader;
        this.datePipe = datePipe;
        this.employeeHistoryService = employeeHistoryService;
        this.historicalLogHeader$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(['Id', 'Date', 'Description']);
        this.educationHeader$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])([
            'Id',
            'Education From',
            'Education To',
            'Field of Study',
            'Institute',
            'Degree'
        ]);
        this.employeeHistoryOCHeader$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])([
            'Id',
            'Employment Form',
            'Employment To',
            'Organization',
            'Monthly Salary',
            'Reason for Leaving',
            'Position'
        ]);
        this.infoOfCloseRelativeHeader$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])([
            'Id',
            'Name',
            'Relationship',
            'Position',
            'Email',
            'Phone',
            'Organization'
        ]);
        this.infoOfThreeRefHeader$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])([
            'Id',
            'Name',
            'Relationship',
            'Position',
            'Email',
            'Phone',
            'Organization'
        ]);
        this.otherSkillHeader$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])([
            'Id',
            'Type of Skill',
            'Ability Level',
            'Experience',
            'Remarks'
        ]);
        this.salarybudgetHeader$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])([
            'Id',
            'Year',
            'Currency',
            'Salary Budget',
            'Budget Disbursed'
        ]);
        this.languageHeader$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])([
            'Id',
            'Language',
            'Writing',
            'Speaking',
            'Reading',
            'Listening'
        ]);
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_10__["ReplaySubject"](1);
        this.actions = {
            items: {
                button: { status: false, text: '' },
                download: false,
                edit: false,
                delete: true
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false
            }
        };
    }
    EmployeeHistoryComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.routeActive.params.subscribe(function (params) {
            _this.employeeId = +params['id'];
        });
        this.getScreenSize();
        Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["forkJoin"])([
            this.getEmployeeHistoricalLogList(),
            this.getEmployeeEducationDetailsList(),
            this.getEmployeeHistoryOfOutsideCountryDetailList(),
            this.getEmployeeCloseRelativeDetailList(),
            this.getEmployeeThreeReferenceDetailList(),
            this.getEmployeeOtherSkillDetailList(),
            this.getEmployeeSalaryBudgetDetailList(),
            this.getEmployeeLanguageDetailList()
        ])
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_17__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeEmployeeHistoricalLogList(result[0]);
            _this.subscribeEmployeeEducationDetailsList(result[1]);
            _this.subscribeEmployeeHistoryOfOutsideCountryDetailList(result[2]);
            _this.subscribeEmployeeCloseRelativeDetailList(result[3]);
            _this.subscribeEmployeeThreeReferenceDetailList(result[4]);
            _this.subscribeEmployeeOtherSkillDetailList(result[5]);
            _this.subscribeEmployeeSalaryBudgetDetailList(result[6]);
            _this.subscribeEmployeeLanguageDetailList(result[7]);
        });
    };
    //#region "Dynamic Scroll"
    EmployeeHistoryComponent.prototype.getScreenSize = function () {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    //#region "get Employee Historical Log List"
    EmployeeHistoryComponent.prototype.getEmployeeHistoricalLogList = function () {
        return this.employeeHistoryService.getHistoricalLogList(this.employeeId);
    };
    EmployeeHistoryComponent.prototype.subscribeEmployeeHistoricalLogList = function (response) {
        var _this = this;
        this.commonLoader.showLoader();
        if (response.data.EmployeeHistoryDetailList !== undefined) {
            this.historicalLogList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(response.data.EmployeeHistoryDetailList.map(function (y) {
                return {
                    HistoryId: y.HistoryID,
                    HistoryDate: _this.datePipe.transform(y.HistoryDate, 'dd-MM-yyyy'),
                    Description: y.Description
                };
            }));
        }
    };
    //#endregion
    // #region "Add HistoricalLog"
    EmployeeHistoryComponent.prototype.addHistoricalLog = function () {
        var _this = this;
        /** Open AddHistoricalLog dialog box*/
        var dialogRef = this.dialog.open(_add_historical_log_add_historical_log_component__WEBPACK_IMPORTED_MODULE_7__["AddHistoricalLogComponent"], {
            width: '500px',
            data: {
                employeeId: this.employeeId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onAddHistoricalListRefresh.subscribe(function () {
            _this.getEmployeeHistoricalLogList();
        });
        dialogRef.afterClosed().subscribe(function () { });
    };
    //#endregion
    // #region "Delete Historical Log"
    EmployeeHistoryComponent.prototype.deleteHistoricalLog = function (HistoryId) {
        var _this = this;
        this.hrService.openDeleteDialog().subscribe(function (res) {
            if (res === true) {
                _this.employeeHistoryService
                    .deleteHistoricalLog(HistoryId)
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        var index_1;
                        _this.historicalLogList$.subscribe(function (data) {
                            index_1 = data.findIndex(function (x) { return x.HistoryId === HistoryId; });
                            data.splice(index_1, 1);
                            _this.historicalLogList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(data);
                        });
                    }
                });
            }
        });
    };
    //#endregion
    //#region "get Employee Education Details List"
    EmployeeHistoryComponent.prototype.getEmployeeEducationDetailsList = function () {
        return this.employeeHistoryService.getEducationDetailList(this.employeeId);
    };
    EmployeeHistoryComponent.prototype.subscribeEmployeeEducationDetailsList = function (response) {
        var _this = this;
        if (response.data.EmployeeEducationsList !== undefined) {
            this.educationList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(response.data.EmployeeEducationsList.map(function (y) {
                return {
                    EmployeeEducationsId: y.EmployeeEducationsId,
                    EducationFrom: _this.datePipe.transform(y.EducationFrom, 'dd-MM-yyyy'),
                    EducationTo: _this.datePipe.transform(y.EducationTo, 'dd-MM-yyyy'),
                    FieldOfStudy: y.FieldOfStudy,
                    Institute: y.Institute,
                    Degree: y.Degree
                };
            }));
        }
    };
    //#endregion
    // #region "Add Education"
    EmployeeHistoryComponent.prototype.addEducation = function () {
        var _this = this;
        /** Open Education dialog box*/
        var dialogRef = this.dialog.open(_add_education_add_education_component__WEBPACK_IMPORTED_MODULE_6__["AddEducationComponent"], {
            width: '800px',
            data: {
                employeeId: this.employeeId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onAddEducationListRefresh.subscribe(function () {
            _this.getEmployeeEducationDetailsList();
        });
        dialogRef.afterClosed().subscribe(function () { });
    };
    //#endregion
    // #region "Delete Education Detail"
    EmployeeHistoryComponent.prototype.deleteEducationDetail = function (EmployeeEducationsId) {
        var _this = this;
        this.hrService.openDeleteDialog().subscribe(function (res) {
            if (res === true) {
                var model = {
                    EmployeeEducationsId: EmployeeEducationsId
                };
                _this.employeeHistoryService
                    .deleteEducation(model)
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        var index_2;
                        _this.educationList$.subscribe(function (data) {
                            index_2 = data.findIndex(function (x) { return x.EmployeeEducationsId === EmployeeEducationsId; });
                            data.splice(index_2, 1);
                            _this.educationList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(data);
                        });
                    }
                });
            }
        });
    };
    //#endregion
    //#region "get Employee History Of Outside Country Detail List"
    EmployeeHistoryComponent.prototype.getEmployeeHistoryOfOutsideCountryDetailList = function () {
        return this.employeeHistoryService.getEmployeeHistoryOfOutsideCountryDetailList(this.employeeId);
    };
    EmployeeHistoryComponent.prototype.subscribeEmployeeHistoryOfOutsideCountryDetailList = function (response) {
        var _this = this;
        if (response.data.EmployeeHistoryOutsideOrganizationList !== undefined) {
            this.employeeHistoryOCList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(response.data.EmployeeHistoryOutsideOrganizationList.map(function (y) {
                return {
                    EmployeeHistoryOutsideCountryId: y.EmployeeHistoryOutsideCountryId,
                    EmploymentFrom: _this.datePipe.transform(y.EmploymentFrom, 'dd-MM-yyyy'),
                    EmploymentTo: _this.datePipe.transform(y.EmploymentTo, 'dd-MM-yyyy'),
                    Organization: y.Organization,
                    MonthlySalary: y.MonthlySalary,
                    ReasonForLeaving: y.ReasonForLeaving,
                    Position: y.Position
                };
            }));
        }
    };
    //#endregion
    // #region "Add HistoryOutsideCountry"
    EmployeeHistoryComponent.prototype.addHistoryOutsideCountry = function () {
        var _this = this;
        /** Open HistoryOutsideCountry dialog box*/
        var dialogRef = this.dialog.open(_add_history_outside_country_add_history_outside_country_component__WEBPACK_IMPORTED_MODULE_4__["AddHistoryOutsideCountryComponent"], {
            width: '800px',
            data: {
                employeeId: this.employeeId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onAddHistoryOutsideCountryListRefresh.subscribe(function () {
            _this.getEmployeeHistoryOfOutsideCountryDetailList();
        });
        dialogRef.afterClosed().subscribe(function () { });
    };
    //#endregion
    // #region "Delete Outside Country Info"
    EmployeeHistoryComponent.prototype.deleteOutsideCountryInfo = function (EmployeeHistoryOutsideCountryId) {
        var _this = this;
        this.hrService.openDeleteDialog().subscribe(function (res) {
            if (res === true) {
                var model = {
                    EmployeeHistoryOutsideCountryId: EmployeeHistoryOutsideCountryId
                };
                _this.employeeHistoryService
                    .deleteEmployeeHistoryOutsideCountry(model)
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        var index_3;
                        _this.employeeHistoryOCList$.subscribe(function (data) {
                            index_3 = data.findIndex(function (x) {
                                return x.EmployeeHistoryOutsideCountryId ===
                                    EmployeeHistoryOutsideCountryId;
                            });
                            data.splice(index_3, 1);
                            _this.employeeHistoryOCList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(data);
                        });
                    }
                });
            }
        });
    };
    //#endregion
    //#region "get Employee Close Relative Detail List"
    EmployeeHistoryComponent.prototype.getEmployeeCloseRelativeDetailList = function () {
        return this.employeeHistoryService.getEmployeeCloseRelativeList(this.employeeId);
    };
    EmployeeHistoryComponent.prototype.subscribeEmployeeCloseRelativeDetailList = function (response) {
        if (response.data.EmployeeRelativeInfoList !== undefined) {
            this.employeeCloseRelativeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(response.data.EmployeeRelativeInfoList.map(function (y) {
                return {
                    EmployeeRelativeInfoId: y.EmployeeRelativeInfoId,
                    Name: y.Name,
                    Relationship: y.Relationship,
                    Position: y.Position,
                    Email: y.Email,
                    PhoneNo: y.PhoneNo,
                    Organization: y.Organization
                };
            }));
        }
    };
    //#endregion
    // #region "Add CloseRelative"
    EmployeeHistoryComponent.prototype.addCloseRelative = function () {
        var _this = this;
        /** Open AddCloseRelative dialog box*/
        var dialogRef = this.dialog.open(_add_close_relative_add_close_relative_component__WEBPACK_IMPORTED_MODULE_5__["AddCloseRelativeComponent"], {
            width: '800px',
            data: {
                employeeId: this.employeeId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onAddCloseRelativeDetailListRefresh.subscribe(function () {
            _this.getEmployeeCloseRelativeDetailList();
        });
        dialogRef.afterClosed().subscribe(function () { });
    };
    //#endregion
    // #region "Delete Close Relative Info"
    EmployeeHistoryComponent.prototype.deleteCloseRelativeInfo = function (EmployeeRelativeInfoId) {
        var _this = this;
        this.hrService.openDeleteDialog().subscribe(function (res) {
            if (res === true) {
                var model = {
                    EmployeeRelativeInfoId: EmployeeRelativeInfoId
                };
                _this.employeeHistoryService
                    .deleteCloseRelativeDetail(model)
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        var index_4;
                        _this.employeeCloseRelativeList$.subscribe(function (data) {
                            index_4 = data.findIndex(function (x) { return x.EmployeeRelativeInfoId === EmployeeRelativeInfoId; });
                            data.splice(index_4, 1);
                            _this.employeeCloseRelativeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(data);
                        });
                    }
                });
            }
        });
    };
    //#endregion
    //#region "get Employee Three Reference Detail List"
    EmployeeHistoryComponent.prototype.getEmployeeThreeReferenceDetailList = function () {
        return this.employeeHistoryService.getEmployeeThreeReferenceDetailList(this.employeeId);
    };
    EmployeeHistoryComponent.prototype.subscribeEmployeeThreeReferenceDetailList = function (response) {
        if (response.data.EmployeeRelativeInfoList !== undefined) {
            this.employeeThreeReferenceList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(response.data.EmployeeRelativeInfoList.map(function (y) {
                return {
                    EmployeeInfoReferencesId: y.EmployeeInfoReferencesId,
                    Name: y.Name,
                    Relationship: y.Relationship,
                    Position: y.Position,
                    Email: y.Email,
                    PhoneNo: y.PhoneNo,
                    Organization: y.Organization
                };
            }));
        }
    };
    //#endregion
    // #region "Add ThreeReference"
    EmployeeHistoryComponent.prototype.addThreeReference = function () {
        var _this = this;
        /** Open AddThreeReference dialog box*/
        var dialogRef = this.dialog.open(_add_three_reference_details_add_three_reference_details_component__WEBPACK_IMPORTED_MODULE_1__["AddThreeReferenceDetailsComponent"], {
            width: '800px',
            data: {
                employeeId: this.employeeId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onThreeReferenceDetailListRefresh.subscribe(function () {
            _this.getEmployeeThreeReferenceDetailList();
        });
        dialogRef.afterClosed().subscribe(function () { });
    };
    //#endregion
    // #region "Delete Employee Reference Info"
    EmployeeHistoryComponent.prototype.deleteEmployeeReferenceInfo = function (EmployeeInfoReferencesId) {
        var _this = this;
        this.hrService.openDeleteDialog().subscribe(function (res) {
            if (res === true) {
                var model = {
                    EmployeeInfoReferencesId: EmployeeInfoReferencesId
                };
                _this.employeeHistoryService
                    .deleteEmployeeReferenceInfoDetail(model)
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        var index_5;
                        _this.employeeThreeReferenceList$.subscribe(function (data) {
                            index_5 = data.findIndex(function (x) { return x.EmployeeInfoReferencesId === EmployeeInfoReferencesId; });
                            data.splice(index_5, 1);
                            _this.employeeThreeReferenceList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(data);
                        });
                    }
                });
            }
        });
    };
    //#endregion
    //#region "get Employee Other Skill Detail List"
    EmployeeHistoryComponent.prototype.getEmployeeOtherSkillDetailList = function () {
        return this.employeeHistoryService.getEmployeeOtherSkillDetailList(this.employeeId);
    };
    EmployeeHistoryComponent.prototype.subscribeEmployeeOtherSkillDetailList = function (response) {
        if (response.data.EmployeeOtherSkillsList !== undefined) {
            this.employeeOtherSkillList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(response.data.EmployeeOtherSkillsList.map(function (y) {
                return {
                    EmployeeOtherSkillsId: y.EmployeeOtherSkillsId,
                    TypeOfSkill: y.TypeOfSkill,
                    AbilityLevel: y.AbilityLevel,
                    Experience: y.Experience,
                    Remarks: y.Remarks
                };
            }));
        }
    };
    //#endregion
    // #region "Add OtherSkill"
    EmployeeHistoryComponent.prototype.addOtherSkill = function () {
        var _this = this;
        /** Open AddOtherSkill dialog box*/
        var dialogRef = this.dialog.open(_add_other_skills_add_other_skills_component__WEBPACK_IMPORTED_MODULE_2__["AddOtherSkillsComponent"], {
            width: '800px',
            data: {
                employeeId: this.employeeId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onOtherSkillDetailListRefresh.subscribe(function () {
            _this.getEmployeeOtherSkillDetailList();
        });
        dialogRef.afterClosed().subscribe(function () { });
    };
    //#endregion
    // #region "Delete Employee Other Skill"
    EmployeeHistoryComponent.prototype.deleteEmployeeOtherSkill = function (EmployeeOtherSkillsId) {
        var _this = this;
        this.hrService.openDeleteDialog().subscribe(function (res) {
            if (res === true) {
                var model = {
                    EmployeeOtherSkillsId: EmployeeOtherSkillsId
                };
                _this.employeeHistoryService
                    .deleteEmployeeOtherSkillDetail(model)
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        var index_6;
                        _this.employeeOtherSkillList$.subscribe(function (data) {
                            index_6 = data.findIndex(function (x) { return x.EmployeeOtherSkillsId === EmployeeOtherSkillsId; });
                            data.splice(index_6, 1);
                            _this.employeeOtherSkillList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(data);
                        });
                    }
                });
            }
        });
    };
    //#endregion
    //#region "get Employee Salary Budget Detail List"
    EmployeeHistoryComponent.prototype.getEmployeeSalaryBudgetDetailList = function () {
        return this.employeeHistoryService.getEmployeeSalaryBudgetDetailList(this.employeeId);
    };
    EmployeeHistoryComponent.prototype.subscribeEmployeeSalaryBudgetDetailList = function (response) {
        if (response.data.EmployeeSalaryBudgetList !== undefined) {
            this.employeeSalaryBudgetList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(response.data.EmployeeSalaryBudgetList.map(function (y) {
                return {
                    EmployeeSalaryBudgetId: y.EmployeeSalaryBudgetId,
                    Year: y.Year,
                    // CurrencyId: y.CurrencyId,
                    CurrencyName: y.CurrencyName,
                    SalaryBudget: y.SalaryBudget,
                    BudgetDisbursed: y.BudgetDisbursed
                };
            }));
        }
    };
    //#endregion
    // #region "Add SalaryBudget"
    EmployeeHistoryComponent.prototype.addSalaryBudget = function () {
        var _this = this;
        /** Open AddSalaryBudget dialog box*/
        var dialogRef = this.dialog.open(_add_salary_budget_add_salary_budget_component__WEBPACK_IMPORTED_MODULE_0__["AddSalaryBudgetComponent"], {
            width: '800px',
            data: {
                employeeId: this.employeeId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onSalaryBudgetDetailListRefresh.subscribe(function () {
            _this.getEmployeeSalaryBudgetDetailList();
        });
        dialogRef.afterClosed().subscribe(function () { });
    };
    //#endregion
    // #region "Delete Employee Salary Budget"
    EmployeeHistoryComponent.prototype.deleteEmployeeSalaryBudget = function (EmployeeSalaryBudgetId) {
        var _this = this;
        this.hrService.openDeleteDialog().subscribe(function (res) {
            if (res === true) {
                var model = {
                    EmployeeSalaryBudgetId: EmployeeSalaryBudgetId
                };
                _this.employeeHistoryService
                    .deleteEmployeeSalaryBudgetDetail(model)
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        var index_7;
                        _this.employeeSalaryBudgetList$.subscribe(function (data) {
                            index_7 = data.findIndex(function (x) { return x.EmployeeSalaryBudgetId === EmployeeSalaryBudgetId; });
                            data.splice(index_7, 1);
                            _this.employeeSalaryBudgetList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(data);
                        });
                    }
                });
            }
        });
    };
    //#endregion
    //#region "get Employee Language Detail List"
    EmployeeHistoryComponent.prototype.getEmployeeLanguageDetailList = function () {
        return this.employeeHistoryService.getEmployeeLanguageDetailList(this.employeeId);
    };
    EmployeeHistoryComponent.prototype.subscribeEmployeeLanguageDetailList = function (response) {
        if (response.data.EmployeeLanguagesList !== undefined) {
            this.employeeLanguageList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(response.data.EmployeeLanguagesList.map(function (y) {
                return {
                    SpeakLanguageId: y.SpeakLanguageId,
                    Language: y.LanguageName,
                    Writing: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][y.Writing],
                    Speaking: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][y.Speaking],
                    Reading: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][y.Reading],
                    Listening: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][y.Listening]
                };
            }));
        }
        this.commonLoader.hideLoader();
    };
    //#endregion
    // #region "Add Language"
    EmployeeHistoryComponent.prototype.addLanguage = function () {
        var _this = this;
        /** Open Language dialog box*/
        var dialogRef = this.dialog.open(_add_language_add_language_component__WEBPACK_IMPORTED_MODULE_3__["AddLanguageComponent"], {
            width: '800px',
            data: {
                employeeId: this.employeeId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onLanguageDetailListRefresh.subscribe(function () {
            _this.getEmployeeLanguageDetailList();
        });
        dialogRef.afterClosed().subscribe(function () { });
    };
    //#endregion
    // #region "Delete Employee Language Detail"
    EmployeeHistoryComponent.prototype.deleteEmployeeLanguageDetail = function (SpeakLanguageId) {
        var _this = this;
        this.hrService.openDeleteDialog().subscribe(function (res) {
            if (res === true) {
                var model = {
                    SpeakLanguageId: SpeakLanguageId
                };
                _this.employeeHistoryService
                    .deleteEmployeeLanguageDetail(model)
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        var index_8;
                        _this.employeeLanguageList$.subscribe(function (data) {
                            index_8 = data.findIndex(function (x) { return x.SpeakLanguageId === SpeakLanguageId; });
                            data.splice(index_8, 1);
                            _this.employeeLanguageList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_10__["of"])(data);
                        });
                    }
                });
            }
        });
    };
    //#endregion
    EmployeeHistoryComponent.prototype.actionEvents = function (event, type) {
        if (event.type === 'delete') {
            switch (type) {
                case 'historicalLog':
                    this.deleteHistoricalLog(event.item.HistoryId);
                    break;
                case 'education':
                    this.deleteEducationDetail(event.item.EmployeeEducationsId);
                    break;
                case 'outsideHistory':
                    this.deleteOutsideCountryInfo(event.item.EmployeeHistoryOutsideCountryId);
                    break;
                case 'closeReletive':
                    this.deleteCloseRelativeInfo(event.item.EmployeeRelativeInfoId);
                    break;
                case 'references':
                    this.deleteEmployeeReferenceInfo(event.item.EmployeeInfoReferencesId);
                    break;
                case 'otherSkill':
                    this.deleteEmployeeOtherSkill(event.item.EmployeeOtherSkillsId);
                    break;
                case 'salaryBudget':
                    this.deleteEmployeeSalaryBudget(event.item.EmployeeSalaryBudgetId);
                    break;
                case 'language':
                    this.deleteEmployeeLanguageDetail(event.item.SpeakLanguageId);
                    break;
            }
        }
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_9__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", []),
        __metadata("design:returntype", void 0)
    ], EmployeeHistoryComponent.prototype, "getScreenSize", null);
    EmployeeHistoryComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_9__["Component"])({
            selector: 'app-employee-history',
            template: __webpack_require__(/*! ./employee-history.component.html */ "./src/app/hr/components/employee-history/employee-history.component.html"),
            styles: [__webpack_require__(/*! ./employee-history.component.scss */ "./src/app/hr/components/employee-history/employee-history.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_13__["MatDialog"],
            _services_hr_service__WEBPACK_IMPORTED_MODULE_14__["HrService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_15__["ActivatedRoute"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_11__["CommonLoaderService"],
            _angular_common__WEBPACK_IMPORTED_MODULE_16__["DatePipe"],
            _services_employee_history_service__WEBPACK_IMPORTED_MODULE_8__["EmployeeHistoryService"]])
    ], EmployeeHistoryComponent);
    return EmployeeHistoryComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.html":
/*!***************************************************************************************!*\
  !*** ./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.html ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h4 mat-dialog-title>\r\n  Assign Leave\r\n</h4>\r\n<mat-dialog-content>\r\n<form [formGroup]='assignLeaveForm' (ngSubmit)=\"assignLeave()\">\r\n  <mat-divider></mat-divider>\r\n\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-12\">\r\n        <b>Select Financial Year for which Leave is to be assigned</b>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <mat-form-field>\r\n          <mat-label>Financial Year</mat-label>\r\n          <mat-select formControlName=\"FinancialYearId\">\r\n            <mat-option *ngFor=\"let fy of financialYearDropdown\" [value]=\"fy.FinancialYearId\">\r\n              {{fy.FinancialYearName}}\r\n            </mat-option>\r\n          </mat-select>\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <b>Select Leave Reason Type</b>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <mat-form-field>\r\n          <mat-label>Leave Reason</mat-label>\r\n          <mat-select (selectionChange)=\"selectedLeaveReason($event.value)\" formControlName=\"LeaveReasonId\">\r\n            <mat-option *ngFor=\"let leave of leaveReasonTypeDropdown\" [value]=\"leave.LeaveReasonId\">\r\n              {{leave.ReasonName}}\r\n            </mat-option>\r\n          </mat-select>\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <b>Units for selected Leave</b>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input formControlName=\"Units\" matInput type=\"number\" placeholder=\"Unit\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <b>Assigned Leave Unit</b>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input formControlName=\"AssignedUnit\" matInput type=\"number\" placeholder=\"Assigned Unit\">\r\n            <mat-error *ngIf=\"assignLeaveForm.controls['AssignedUnit'].hasError('max')\">Max Unit allowed is {{assignLeaveForm.controls['Units'].value}}</mat-error>\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <b>Remarks</b>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <textarea matInput placeholder=\"Remarks\"></textarea>\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n    <button type=\"submit\" style=\"display: none;\" #btnSubmit></button>\r\n  </form>\r\n  </mat-dialog-content>\r\n\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\"\r\n      [disabled]=\"!assignLeaveForm.valid || !assignLeaveForm.dirty\" (click)=\"btnSubmit.click()\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='closeDialog()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.scss":
/*!***************************************************************************************!*\
  !*** ./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.scss ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtbGVhdmUvYXNzaWduLWxlYXZlL2Fzc2lnbi1sZWF2ZS5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.ts":
/*!*************************************************************************************!*\
  !*** ./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.ts ***!
  \*************************************************************************************/
/*! exports provided: AssignLeaveComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AssignLeaveComponent", function() { return AssignLeaveComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_hr_leave_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/hr-leave.service */ "./src/app/hr/services/hr-leave.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var AssignLeaveComponent = /** @class */ (function () {
    function AssignLeaveComponent(dialogRef, data, fb, toastr, hrLeaveService) {
        this.dialogRef = dialogRef;
        this.data = data;
        this.fb = fb;
        this.toastr = toastr;
        this.hrLeaveService = hrLeaveService;
        this.isFormSubmitted = false;
    }
    AssignLeaveComponent.prototype.ngOnInit = function () {
        this.onFormInIt();
        this.GetFinancialYearDropdown();
        this.GetLeaveReasonTypeDropdown();
    };
    AssignLeaveComponent.prototype.onFormInIt = function () {
        this.assignLeaveForm = this.fb.group({
            'EmployeeId': [this.data.EmployeeId],
            'FinancialYearId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'LeaveReasonId': [this.data.LeaveReasonId, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Units': [{ value: null, disabled: true }, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            'AssignedUnit': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(1)]],
            // 'LeaveApplied': [null, [Validators.required, Validators.min(1), Validators.max(this.data.HourBalance)]],
            'Remarks': [null]
        });
    };
    //#region "Get All Financial Year"
    AssignLeaveComponent.prototype.GetFinancialYearDropdown = function () {
        var _this = this;
        this.hrLeaveService
            .getFinancialYearList()
            .subscribe(function (data) {
            _this.financialYearDropdown = [];
            if (data.data.CurrentFinancialYearList != null &&
                data.data.CurrentFinancialYearList.length > 0) {
                data.data.CurrentFinancialYearList.forEach(function (element) {
                    _this.financialYearDropdown.push({
                        StartDate: new Date(new Date(element.StartDate).getTime() -
                            new Date().getTimezoneOffset() * 60000),
                        EndDate: new Date(new Date(element.EndDate).getTime() -
                            new Date().getTimezoneOffset() * 60000),
                        FinancialYearId: element.FinancialYearId,
                        FinancialYearName: element.FinancialYearName
                    });
                });
                if (_this.financialYearDropdown.length > 0) {
                    _this.assignLeaveForm.controls['FinancialYearId'].setValue(_this.financialYearDropdown[0].FinancialYearId);
                }
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error('Something went wrong!');
            }
        }, function (error) {
            if (error.StatusCode === 500) {
                _this.toastr.error('Internal Server Error....');
            }
            else if (error.StatusCode === 401) {
                _this.toastr.error('Unauthorized Access Error....');
            }
            else if (error.StatusCode === 403) {
                _this.toastr.error('Forbidden Error....');
            }
        });
    };
    //#endregion
    //#region "Get All Leave Reason Type"
    AssignLeaveComponent.prototype.GetLeaveReasonTypeDropdown = function () {
        var _this = this;
        this.hrLeaveService
            .getAllLeaveReasonType()
            .subscribe(function (data) {
            _this.leaveReasonTypeDropdown = [];
            if (data.data.LeaveReasonList != null &&
                data.data.LeaveReasonList.length > 0) {
                data.data.LeaveReasonList.forEach(function (element) {
                    _this.leaveReasonTypeDropdown.push(element);
                });
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error('Something went wrong!');
            }
        }, function (error) {
            if (error.StatusCode === 500) {
                _this.toastr.error('Internal Server Error....');
            }
            else if (error.StatusCode === 401) {
                _this.toastr.error('Unauthorized Access Error....');
            }
            else if (error.StatusCode === 403) {
                _this.toastr.error('Forbidden Error....');
            }
        });
    };
    //#endregion
    AssignLeaveComponent.prototype.selectedLeaveReason = function (id) {
        var index = this.leaveReasonTypeDropdown.findIndex(function (x) { return x.LeaveReasonId === id; });
        if (index !== -1) {
            this.assignLeaveForm.controls['Units'].setValue(this.leaveReasonTypeDropdown[index].Unit);
            this.assignLeaveForm.controls['AssignedUnit'].setValidators(_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].max(this.leaveReasonTypeDropdown[index].Unit));
            this.assignLeaveForm.controls['AssignedUnit'].updateValueAndValidity();
        }
    };
    AssignLeaveComponent.prototype.assignLeave = function () {
        var _this = this;
        if (!this.assignLeaveForm.valid) {
            this.toastr.warning('Please correct form errors and submit again');
            return;
        }
        this.isFormSubmitted = true;
        var model = {
            LeaveId: 0,
            EmployeeId: this.assignLeaveForm.value.EmployeeId,
            LeaveReasonId: this.assignLeaveForm.value.LeaveReasonId,
            Unit: this.assignLeaveForm.getRawValue().Units,
            AssignUnit: this.assignLeaveForm.value.AssignedUnit,
            FinancialYearId: this.assignLeaveForm.value.FinancialYearId,
            Description: this.assignLeaveForm.value.Remarks
        };
        this.hrLeaveService.assignLeave(model)
            .subscribe(function (data) {
            if (data) {
                _this.toastr.success('Added Successfully!!!');
                _this.isFormSubmitted = false;
                _this.closeDialog();
            }
        }, function (error) {
            _this.isFormSubmitted = false;
            _this.toastr.warning(error);
        });
    };
    AssignLeaveComponent.prototype.closeDialog = function () {
        this.dialogRef.close();
    };
    AssignLeaveComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-assign-leave',
            template: __webpack_require__(/*! ./assign-leave.component.html */ "./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.html"),
            styles: [__webpack_require__(/*! ./assign-leave.component.scss */ "./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.scss")]
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"], Object, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"], src_app_hr_services_hr_leave_service__WEBPACK_IMPORTED_MODULE_4__["HrLeaveService"]])
    ], AssignLeaveComponent);
    return AssignLeaveComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.html":
/*!***************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.html ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <form [formGroup]='applyLeaveForm' (ngSubmit)=\"applyLeave()\">\r\n    <h4 mat-dialog-title>\r\n      Apply Leave\r\n    </h4>\r\n    <mat-divider></mat-divider>\r\n    <div mat-dialog-content>\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-12\">\r\n          <b>This is the Leave Reason</b>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field class=\"example-full-width\">\r\n            <input formControlName=\"LeaveType\" [readOnly]=\"true\" matInput placeholder=\"Leave Type\">\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <b>How much balance is left for this leave?</b>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field class=\"example-full-width\">\r\n            <input formControlName=\"BalanceLeave\" [readOnly]=\"true\" matInput placeholder=\"Balance Leave\">\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <b>When will this Leave be applied?</b>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field>\r\n            <input [matDatepickerFilter]=\"myFilter\" matInput [satDatepicker]=\"resultPicker\" formControlName=\"LeaveDate\">\r\n            <sat-datepicker\r\n                #resultPicker\r\n                [rangeMode]=\"true\">\r\n            </sat-datepicker>\r\n            <sat-datepicker-toggle matSuffix [for]=\"resultPicker\"></sat-datepicker-toggle>\r\n          </mat-form-field>\r\n        </div>\r\n        <!-- <div class=\"col-sm-12\">\r\n          <b>How much leave employee want to avail?</b>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field class=\"example-full-width\">\r\n            <input type=\"number\" matInput formControlName=\"LeaveApplied\" placeholder=\"Leave Applied\">\r\n            <mat-error *ngIf=\"applyLeaveForm.controls['LeaveApplied'].hasError('min')\">unit should be greater than 0</mat-error>\r\n            <mat-error *ngIf=\"applyLeaveForm.controls['LeaveApplied'].hasError('max')\">Max unit allowed is {{data.HourBalance}}</mat-error>\r\n          </mat-form-field>\r\n        </div> -->\r\n        <div class=\"col-sm-12\">\r\n          <b>Provide any remarks</b>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field class=\"example-full-width\">\r\n            <textarea matInput formControlName=\"Remarks\" placeholder=\"Remarks\"></textarea>\r\n          </mat-form-field>\r\n        </div>\r\n      </div>\r\n    </div>\r\n    <div mat-dialog-actions class=\"items-float-right\">\r\n      <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"\r\n        [disabled]=\"!applyLeaveForm.valid || !applyLeaveForm.dirty\"></hum-button>\r\n      <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n      <hum-button (click)='closeDialog()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n    </div>\r\n  </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.scss":
/*!***************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.scss ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtbGVhdmUvZW1wbG95ZWUtbGVhdmUtYWRkL2VtcGxveWVlLWxlYXZlLWFkZC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.ts":
/*!*************************************************************************************************!*\
  !*** ./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.ts ***!
  \*************************************************************************************************/
/*! exports provided: EmployeeLeaveAddComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeLeaveAddComponent", function() { return EmployeeLeaveAddComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_hr_leave_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/hr-leave.service */ "./src/app/hr/services/hr-leave.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var EmployeeLeaveAddComponent = /** @class */ (function () {
    function EmployeeLeaveAddComponent(dialogRef, data, fb, toastr, hrLeaveService) {
        this.dialogRef = dialogRef;
        this.data = data;
        this.fb = fb;
        this.toastr = toastr;
        this.hrLeaveService = hrLeaveService;
        this.isFormSubmitted = false;
        this.myFilter = function (d) {
            var day = d.getDay();
            // Prevent Saturday and Sunday from being selected.
            return day !== 0 && day !== 6;
        };
    }
    EmployeeLeaveAddComponent.prototype.ngOnInit = function () {
        this.onFormInIt();
    };
    EmployeeLeaveAddComponent.prototype.onFormInIt = function () {
        this.applyLeaveForm = this.fb.group({
            'EmployeeId': [this.data.EmployeeId],
            'LeaveType': [{ value: this.data.LeaveType, disabled: true }],
            'LeaveReasonId': [this.data.LeaveReasonId],
            'LeaveDate': [{ 'begin': new Date(new Date().getFullYear(), 0, 1), 'end': new Date() },
                _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required],
            'BalanceLeave': [{ value: this.data.HourBalance, disabled: true }],
            // 'LeaveApplied': [null, [Validators.required, Validators.min(1), Validators.max(this.data.HourBalance)]],
            'Remarks': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]]
        });
    };
    EmployeeLeaveAddComponent.prototype.applyLeave = function () {
        var _this = this;
        if (!this.applyLeaveForm.valid) {
            this.toastr.warning('Please correct errors in form and submit again');
            return;
        }
        this.isFormSubmitted = true;
        var FromDate = new Date(new Date(this.applyLeaveForm.value.LeaveDate.begin).getFullYear(), new Date(this.applyLeaveForm.value.LeaveDate.begin).getMonth(), new Date(this.applyLeaveForm.value.LeaveDate.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds());
        var ToDate = new Date(new Date(this.applyLeaveForm.value.LeaveDate.end).getFullYear(), new Date(this.applyLeaveForm.value.LeaveDate.end).getMonth(), new Date(this.applyLeaveForm.value.LeaveDate.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds());
        var difference = ((ToDate.getTime() - FromDate.getTime()) / (1000 * 3600 * 24));
        if ((this.applyLeaveForm.value.BalanceLeave - difference) < 0) {
            this.toastr.warning('Applied leave exceeds Balance leave');
            this.isFormSubmitted = false;
            return;
        }
        var model = {
            LeaveReasonId: this.applyLeaveForm.value.LeaveReasonId,
            Remarks: this.applyLeaveForm.value.Remarks,
            LeaveReasonName: this.applyLeaveForm.value.LeaveType,
            BlanceLeave: this.applyLeaveForm.value.LeaveApplied,
            EmployeeId: this.applyLeaveForm.value.EmployeeId,
            FromDate: FromDate,
            ToDate: ToDate,
            TotalLeaveCount: difference + 1
        };
        this.hrLeaveService.addEmployeeLeave(model).subscribe(function (x) {
            if (x) {
                _this.toastr.success('Added Successfully');
                _this.isFormSubmitted = false;
                _this.closeDialog();
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    EmployeeLeaveAddComponent.prototype.closeDialog = function () {
        this.dialogRef.close();
    };
    EmployeeLeaveAddComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-employee-leave-add',
            template: __webpack_require__(/*! ./employee-leave-add.component.html */ "./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.html"),
            styles: [__webpack_require__(/*! ./employee-leave-add.component.scss */ "./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.scss")]
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"], src_app_hr_services_hr_leave_service__WEBPACK_IMPORTED_MODULE_4__["HrLeaveService"]])
    ], EmployeeLeaveAddComponent);
    return EmployeeLeaveAddComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-leave/employee-leave.component.html":
/*!****************************************************************************!*\
  !*** ./src/app/hr/components/employee-leave/employee-leave.component.html ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"padding_left_30\" humAddScroll>\r\n  <div class=\"row\" >\r\n    <div class=\"col-sm-2\">\r\n      <h4>Leave Balances</h4>\r\n    </div>\r\n    <div class=\"col-sm-3\">\r\n      <hum-button [type]=\"'add'\" [text]=\"'ADD NEW LEAVE TYPE FROM POLICY'\" (click)=\"assignLeave()\"></hum-button>\r\n    </div>\r\n    <div class=\"col-sm-2\">\r\n      <hum-button [type]=\"'text'\" [text]=\"'PDF EXPORT'\"></hum-button>\r\n    </div>\r\n  </div>\r\n  <div class=\"row\">\r\n    <hum-table [headers]=\"leaveListHeaders$\" [items]=\"leaveList$\" (actionClick)=\"actionEvents($event)\"\r\n             [actions]=\"actions\" [hideColums$]=\"hideColums$\"></hum-table>\r\n  </div>\r\n  <div class=\"row\" >\r\n    <div class=\"col-sm-2\">\r\n      <h4>Leave Applications</h4>\r\n    </div>\r\n  </div>\r\n  <div class=\"row\">\r\n    <hum-table [isDefaultAction]=\"false\" [headers]=\"appliedleaveListHeaders$\" [items]=\"assignedLeaveList$\" (actionClick)=\"appliedLeaveActionEvents($event)\"\r\n              [hideColums$]=\"hideColumsAppliedHours$\"></hum-table>\r\n  </div>\r\n</div>\r\n\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-leave/employee-leave.component.scss":
/*!****************************************************************************!*\
  !*** ./src/app/hr/components/employee-leave/employee-leave.component.scss ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtbGVhdmUvZW1wbG95ZWUtbGVhdmUuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/hr/components/employee-leave/employee-leave.component.ts":
/*!**************************************************************************!*\
  !*** ./src/app/hr/components/employee-leave/employee-leave.component.ts ***!
  \**************************************************************************/
/*! exports provided: EmployeeLeaveComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeLeaveComponent", function() { return EmployeeLeaveComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _services_hr_leave_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/hr-leave.service */ "./src/app/hr/services/hr-leave.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _employee_leave_add_employee_leave_add_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./employee-leave-add/employee-leave-add.component */ "./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.ts");
/* harmony import */ var _assign_leave_assign_leave_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./assign-leave/assign-leave.component */ "./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};









var EmployeeLeaveComponent = /** @class */ (function () {
    function EmployeeLeaveComponent(hrLeave, activatedRoute, dialog, toastr, datePipe) {
        var _this = this;
        this.hrLeave = hrLeave;
        this.activatedRoute = activatedRoute;
        this.dialog = dialog;
        this.toastr = toastr;
        this.datePipe = datePipe;
        this.leaveListHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Leave Type', 'Policy Allowed Hours', 'Applied/Approved Hours',
            'Hour Balance']);
        this.appliedleaveListHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Leave Type', 'Applied Hours', 'Status']);
        this.hideColums$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])({
            headers: ['Leave Type', 'Policy Allowed Hours', 'Applied/Approved Hours',
                'Hour Balance'],
            items: ['LeaveType', 'AllowedHours', 'ApprovedHours', 'HourBalance',]
        });
        this.hideColumsAppliedHours$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])({
            headers: ['Leave Type', 'Applied Hours', 'Status'],
            items: ['LeaveType', 'AppliedHours', 'Status']
        });
        this.activatedRoute.params.subscribe(function (params) {
            _this.employeeId = +params['id'];
        });
    }
    EmployeeLeaveComponent.prototype.ngOnInit = function () {
        this.actions = {
            items: {
                button: { status: true, text: 'ADD LEAVE APPLICATION' },
                delete: false,
                download: false,
                edit: false
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false,
                edit: false
            }
        };
        this.getLeaveBalanceDetails();
        this.GetAllLeaveDetails();
    };
    EmployeeLeaveComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'button') {
            var dialogRef = this.dialog.open(_employee_leave_add_employee_leave_add_component__WEBPACK_IMPORTED_MODULE_5__["EmployeeLeaveAddComponent"], {
                width: '450px',
                height: '450px',
                data: {
                    EmployeeId: this.employeeId,
                    LeaveReasonId: event.item.Id,
                    LeaveType: event.item.LeaveType,
                    HourBalance: event.item.HourBalance
                }
            });
            dialogRef.afterClosed().subscribe(function (result) {
                _this.getLeaveBalanceDetails();
                _this.GetAllLeaveDetails();
            });
        }
    };
    EmployeeLeaveComponent.prototype.getLeaveBalanceDetails = function () {
        var _this = this;
        this.hrLeave
            .getEmployeeBalanceLeave(this.employeeId)
            .subscribe(function (data) {
            _this.leaveList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(data.data.AssignLeaveToEmployeeList.map(function (element) {
                return {
                    Id: element.LeaveReasonId,
                    LeaveType: element.LeaveReasonName,
                    AllowedHours: element.Unit,
                    ApprovedHours: element.AssignUnit,
                    HourBalance: element.BlanceLeave
                };
            }));
        }, function (error) {
            _this.toastr.warning(error);
        });
    };
    // Leave Details
    //#region "Get All Leave Details"
    EmployeeLeaveComponent.prototype.GetAllLeaveDetails = function () {
        var _this = this;
        this.hrLeave
            .getAllLeaveInfoById(this.employeeId)
            .subscribe(function (x) {
            if (x && x.LeaveList.length > 0) {
                _this.assignedLeaveList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.LeaveList.map(function (element) {
                    return {
                        Id: element.ApplyLeaveId,
                        LeaveType: element.LeaveReasonName,
                        AppliedHours: element.LeaveHoursCount,
                        Status: element.ApplyLeaveStatus,
                        StatusId: element.ApplyLeaveStatusId,
                        FromDate: element.FromDate,
                        ToDate: element.ToDate,
                        itemAction: (!element.ApplyLeaveStatusId) ? ([
                            {
                                button: {
                                    status: true,
                                    text: 'SEE DAYS',
                                    type: 'text'
                                },
                                delete: false,
                                download: false,
                                edit: false
                            },
                            {
                                button: {
                                    status: true,
                                    text: 'APPROVE',
                                    type: 'save'
                                },
                                delete: false,
                                download: false,
                                edit: false
                            },
                            {
                                button: {
                                    status: true,
                                    text: 'REJECT',
                                    type: 'cancel'
                                },
                                delete: false,
                                download: false,
                                edit: false
                            },
                        ]) : ([
                            {
                                button: {
                                    status: true,
                                    text: 'SEE DAYS',
                                    type: 'text'
                                },
                                delete: false,
                                download: false,
                                edit: false
                            }
                        ])
                    };
                }));
            }
        }, function (error) {
            _this.toastr.warning(error);
        });
    };
    //#endregion
    EmployeeLeaveComponent.prototype.appliedLeaveActionEvents = function (event) {
        var _this = this;
        if (event.type === 'SEE DAYS') {
            return;
        }
        var model = {
            Id: event.item.Id,
            EmployeeId: this.employeeId,
            Approved: false
        };
        if (event.type === 'APPROVE') {
            model.Approved = true;
        }
        else if (event.type === 'REJECT') {
            model.Approved = false;
        }
        this.hrLeave.approveRejectLeave(model).subscribe(function (x) {
            if (x) {
                _this.getLeaveBalanceDetails();
                _this.GetAllLeaveDetails();
            }
        }, function (error) {
            _this.toastr.warning(error);
        });
    };
    EmployeeLeaveComponent.prototype.assignLeave = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_assign_leave_assign_leave_component__WEBPACK_IMPORTED_MODULE_6__["AssignLeaveComponent"], {
            width: '450px',
            data: {
                EmployeeId: this.employeeId,
            }
        });
        dialogRef.afterClosed().subscribe(function (result) {
            _this.getLeaveBalanceDetails();
        });
    };
    EmployeeLeaveComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-employee-leave',
            template: __webpack_require__(/*! ./employee-leave.component.html */ "./src/app/hr/components/employee-leave/employee-leave.component.html"),
            styles: [__webpack_require__(/*! ./employee-leave.component.scss */ "./src/app/hr/components/employee-leave/employee-leave.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_hr_leave_service__WEBPACK_IMPORTED_MODULE_2__["HrLeaveService"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["ActivatedRoute"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_4__["MatDialog"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_7__["ToastrService"], _angular_common__WEBPACK_IMPORTED_MODULE_8__["DatePipe"]])
    ], EmployeeLeaveComponent);
    return EmployeeLeaveComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-list/employee-list.component.html":
/*!**************************************************************************!*\
  !*** ./src/app/hr/components/employee-list/employee-list.component.html ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template [headerClass]=\"'sub_header_template_main2'\">\r\n  <span class=\"action_header\">Employees\r\n    <hum-button [type]=\"'add'\" [text]=\"'ADD EMPLOYEE'\"></hum-button>\r\n    <hum-button [type]=\"'delete'\" [text]=\"'DELETE EMPLOYEE'\" (click)=\"deleteEmployee()\"></hum-button>\r\n    <button mat-button [matMenuTriggerFor]=\"OfficeMenu\">\r\n      <h5 style=\"color: #0390D1;\">{{selectedOffice.name | uppercase}} &nbsp;<i class=\"fas fa-caret-down\"></i></h5>\r\n    </button>\r\n    <button mat-button [matMenuTriggerFor]=\"OfficeMenu\" [disabled]=\"true\">\r\n      <h5>SET ATTENDANCE &nbsp;<i class=\"fas fa-caret-down\"></i></h5>\r\n    </button>\r\n    <button mat-button [matMenuTriggerFor]=\"OfficeMenu\" [disabled]=\"true\">\r\n      <h5>RECONFIGURE SALARY</h5>\r\n    </button>\r\n    <mat-menu #OfficeMenu=\"matMenu\">\r\n      <button (click)=\"selectedOfficeChanged(office)\" *ngFor=\"let office of officeDropdown$ | async\" mat-menu-item>{{office.name}}</button>\r\n    </mat-menu>\r\n  </span>\r\n  <div class=\"action_section\">\r\n  </div>\r\n\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n\r\n<div class=\"container-fluid\">\r\n  <form [formGroup]=\"employeeListFilterForm\" (ngSubmit)=\"filterEmployee(employeeListFilterForm.value)\">\r\n    <h4>Filters\r\n        <hum-button class=\"pull-right\" [type]=\"'filter'\" [text]=\"'APPLY FILTER'\" [isSubmit]=\"true\"></hum-button>\r\n    </h4>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-2\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput type=\"text\" formControlName=\"FirstName\" placeholder=\"First Name\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput type=\"text\" formControlName=\"LastName\" placeholder=\"Last Name\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <lib-hum-dropdown formControlName=\"Sex\" [validation]=\"\r\n        employeeListFilterForm.controls['Sex'].hasError()\" [options]=\"genderList$\" [placeHolder]=\"'Sex'\"></lib-hum-dropdown>\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <lib-hum-dropdown formControlName=\"EmploymentStatus\" [validation]=\"\r\n        employeeListFilterForm.controls['EmploymentStatus'].hasError()\" [options]=\"accountStatusList$\" [placeHolder]=\"'Employment Status'\"></lib-hum-dropdown>\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput type=\"text\" formControlName=\"EmployeeId\" placeholder=\"EmployeeId\">\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </form>\r\n</div>\r\n<div class=\"row\">\r\n  <div class=\"col-md-12\">\r\n      <mat-table #table [dataSource]=\"employeeDataSource\">\r\n        <ng-container matColumnDef=\"select\">\r\n          <th style=\"border-bottom-width:0px\" mat-header-cell *matHeaderCellDef>\r\n            <mat-checkbox (change)=\"$event ? masterToggle() : null\" [checked]=\"selection.hasValue() && isAllSelected()\"\r\n              [indeterminate]=\"selection.hasValue() && !isAllSelected()\" [aria-label]=\"checkboxLabel()\">\r\n            </mat-checkbox>\r\n          </th>\r\n          <td style=\"border-bottom-width:0px\" mat-cell *matCellDef=\"let row\">\r\n            <mat-checkbox (click)=\"$event.stopPropagation()\" (change)=\"$event ? selection.toggle(row) : null\"\r\n              [checked]=\"selection.isSelected(row)\" [aria-label]=\"checkboxLabel(row)\">\r\n            </mat-checkbox>\r\n          </td>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"Code\">\r\n          <mat-header-cell *matHeaderCellDef> Code </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.Code}} </mat-cell>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"FirstName\">\r\n          <mat-header-cell *matHeaderCellDef> First Name </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.FirstName}} </mat-cell>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"LastName\">\r\n          <mat-header-cell *matHeaderCellDef> Last Name </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.LastName}} </mat-cell>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"EmploymentStatus\">\r\n          <mat-header-cell *matHeaderCellDef> Employment Status </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.EmploymentStatus}} </mat-cell>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"Profession\">\r\n          <mat-header-cell *matHeaderCellDef> Profession </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.Profession}}\r\n          </mat-cell>\r\n        </ng-container>\r\n\r\n        <mat-header-row *matHeaderRowDef=\"displayedColumns\"></mat-header-row>\r\n        <mat-row *matRowDef=\"let row; columns: displayedColumns;\" (click)=\"navigateToDetails(row)\"></mat-row>\r\n      </mat-table>\r\n      <mat-paginator [length]=\"TotalCount\" [pageSize]=\"filterModel.PageSize\"\r\n      [pageIndex]=\"filterModel.PageIndex\" [pageSizeOptions]=\"[10, 25, 50, 100]\" (page)=\"pageEvent($event)\"></mat-paginator>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-list/employee-list.component.scss":
/*!**************************************************************************!*\
  !*** ./src/app/hr/components/employee-list/employee-list.component.scss ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".fnt-label-size {\n  font-size: 16px !important;\n  color: #0390D1; }\n\n.font-size {\n  font-size: 12px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvaHIvY29tcG9uZW50cy9lbXBsb3llZS1saXN0L2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXGhyXFxjb21wb25lbnRzXFxlbXBsb3llZS1saXN0XFxlbXBsb3llZS1saXN0LmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0UsMEJBQTBCO0VBQzFCLGNBQWEsRUFBQTs7QUFFZjtFQUNFLGVBQWMsRUFBQSIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtbGlzdC9lbXBsb3llZS1saXN0LmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsiLmZudC1sYWJlbC1zaXplIHtcclxuICBmb250LXNpemU6IDE2cHggIWltcG9ydGFudDtcclxuICBjb2xvcjojMDM5MEQxO1xyXG59XHJcbi5mb250LXNpemV7XHJcbiAgZm9udC1zaXplOjEycHg7XHJcbn1cclxuIl19 */"

/***/ }),

/***/ "./src/app/hr/components/employee-list/employee-list.component.ts":
/*!************************************************************************!*\
  !*** ./src/app/hr/components/employee-list/employee-list.component.ts ***!
  \************************************************************************/
/*! exports provided: EmployeeListComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeListComponent", function() { return EmployeeListComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_employee_list_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../services/employee-list.service */ "./src/app/hr/services/employee-list.service.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_cdk_collections__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/cdk/collections */ "./node_modules/@angular/cdk/esm5/collections.es5.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};










var EmployeeListComponent = /** @class */ (function () {
    function EmployeeListComponent(employeeListService, toastr, fb, router, commonLoader) {
        this.employeeListService = employeeListService;
        this.toastr = toastr;
        this.fb = fb;
        this.router = router;
        this.commonLoader = commonLoader;
        this.selectedOffice = { value: 0, name: 'OFFICE' };
        this.employeeList = [];
        this.selection = new _angular_cdk_collections__WEBPACK_IMPORTED_MODULE_5__["SelectionModel"](true, []);
        this.displayedColumns = ['select', 'Code', 'FirstName',
            'LastName', 'EmploymentStatus', 'Profession'];
        this.filterModel = { EmployeeIdFilter: null, EmploymentStatusFilter: 0, FirstNameFilter: null,
            LastNameFilter: null, PageIndex: 0, PageSize: 10, OfficeId: 0, GenderFilter: 0 };
        this.TotalCount = 0;
        this.accountStatusList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])([
            { name: 'Prospective', value: 1 },
            { name: 'Active', value: 2 },
            { name: 'Terminated', value: 3 }
        ]);
        this.genderList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])([
            { name: 'Male', value: 1 },
            { name: 'Female', value: 2 },
            { name: 'Other', value: 3 }
        ]);
        this.employeeListFilterForm = this.fb.group({
            FirstName: [''],
            LastName: [''],
            Sex: [''],
            EmploymentStatus: [''],
            EmployeeId: ['']
        });
    }
    EmployeeListComponent.prototype.ngOnInit = function () {
        this.getOfficeList();
    };
    EmployeeListComponent.prototype.getOfficeList = function () {
        var _this = this;
        this.employeeListService
            .GetAllOfficeCodeList()
            .subscribe(function (data) {
            if (data.data.OfficeDetailsList != null) {
                _this.officeDropdown$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(data.data.OfficeDetailsList.map(function (y) {
                    return {
                        value: y.OfficeId,
                        name: y.OfficeName
                    };
                }));
                _this.selectedOffice = { value: data.data.OfficeDetailsList[0].OfficeId, name: data.data.OfficeDetailsList[0].OfficeName };
                _this.filterModel.OfficeId = _this.selectedOffice.value;
                _this.getFilteredEmployeeList(_this.filterModel);
            }
        }, function (error) {
            if (error.StatusCode === 500) {
                _this.toastr.error('Internal Server Error....');
            }
            else if (error.StatusCode === 401) {
                _this.toastr.error('Unauthorized Access Error....');
            }
            else if (error.StatusCode === 403) {
                _this.toastr.error('Forbidden Error....');
            }
        });
    };
    EmployeeListComponent.prototype.selectedOfficeChanged = function (office) {
        this.selectedOffice = {
            value: office.value,
            name: office.name
        };
        this.filterModel.OfficeId = office.value;
        this.getFilteredEmployeeList(this.filterModel);
    };
    EmployeeListComponent.prototype.getFilteredEmployeeList = function (filterModel) {
        var _this = this;
        this.employeeListService.getAllEmployeeList(filterModel).subscribe(function (res) {
            if (res.EmployeeList) {
                _this.employeeList = [];
                _this.TotalCount = res.RecordCount;
                res.EmployeeList.forEach(function (element) {
                    _this.employeeList.push({
                        EmployeeId: element.EmployeeID,
                        Code: element.EmployeeCode,
                        FirstName: element.FirstName,
                        LastName: element.LastName,
                        EmploymentStatus: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_6__["EmploymentStatus"][element.EmployeeTypeId],
                        Profession: (element.Profession === undefined) ? 'N/A' : element.Profession
                    });
                });
                _this.employeeDataSource = new _angular_material__WEBPACK_IMPORTED_MODULE_7__["MatTableDataSource"](_this.employeeList);
            }
        }, function (err) {
            _this.toastr.warning(err);
        });
    };
    EmployeeListComponent.prototype.filterEmployee = function (value) {
        this.filterModel = { EmployeeIdFilter: value.EmployeeId, EmploymentStatusFilter: value.EmploymentStatus,
            FirstNameFilter: value.FirstName, LastNameFilter: value.LastName, PageIndex: 0, PageSize: 10, OfficeId: this.selectedOffice.value,
            GenderFilter: value.Sex
        };
        this.getFilteredEmployeeList(this.filterModel);
    };
    EmployeeListComponent.prototype.navigateToDetails = function (row) {
        this.router.navigate(['/hr/employee/' + row.EmployeeId]);
    };
    EmployeeListComponent.prototype.pageEvent = function (e) {
        this.filterModel.PageIndex = e.pageIndex;
        this.filterModel.PageSize = e.pageSize;
        this.filterModel.OfficeId = this.selectedOffice.value;
        this.getFilteredEmployeeList(this.filterModel);
    };
    EmployeeListComponent.prototype.deleteEmployee = function () {
        var _this = this;
        if (this.selection.selected.length === 0) {
            this.toastr.warning('Please select at least 1 record!');
            return;
        }
        this.commonLoader.showLoader();
        var EmpIds = [];
        this.selection.selected.forEach(function (e) {
            EmpIds.push(e.EmployeeId);
        });
        this.employeeListService.deleteMurtipleEmployeesById(EmpIds).subscribe(function (res) {
            if (res) {
                _this.toastr.success('Deleted Successfully!');
                _this.getFilteredEmployeeList(_this.filterModel);
                _this.commonLoader.hideLoader();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.commonLoader.hideLoader();
            }
        }, function (err) {
            _this.toastr.warning(err);
            _this.commonLoader.hideLoader();
        });
    };
    /** Whether the number of selected elements matches the total number of rows. */
    EmployeeListComponent.prototype.isAllSelected = function () {
        var numSelected = this.selection.selected.length;
        var numRows = this.employeeList.length;
        return numSelected === numRows;
    };
    /** Selects all rows if they are not all selected; otherwise clear selection. */
    EmployeeListComponent.prototype.masterToggle = function () {
        var _this = this;
        this.isAllSelected() ?
            this.selection.clear() :
            this.employeeList.forEach(function (row) { return _this.selection.select(row); });
    };
    /** The label for the checkbox on the passed row */
    EmployeeListComponent.prototype.checkboxLabel = function (row) {
        if (!row) {
            return (this.isAllSelected() ? 'select' : 'deselect') + " all";
        }
        return (this.selection.isSelected(row) ? 'deselect' : 'select') + " row " + (row.EmployeeId + 1);
    };
    EmployeeListComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-employee-list',
            template: __webpack_require__(/*! ./employee-list.component.html */ "./src/app/hr/components/employee-list/employee-list.component.html"),
            styles: [__webpack_require__(/*! ./employee-list.component.scss */ "./src/app/hr/components/employee-list/employee-list.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_employee_list_service__WEBPACK_IMPORTED_MODULE_1__["EmployeeListService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_4__["FormBuilder"],
            _angular_router__WEBPACK_IMPORTED_MODULE_8__["Router"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_9__["CommonLoaderService"]])
    ], EmployeeListComponent);
    return EmployeeListComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-pension/employee-pension.component.html":
/*!********************************************************************************!*\
  !*** ./src/app/hr/components/employee-pension/employee-pension.component.html ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-card humAddScroll>\r\n    <div class=\"row header\">\r\n        <span class=\"action_header\">Salary Taxes\r\n      <button mat-button [matMenuTriggerFor]=\"TaxCurrencyMenu\">\r\n        <h5 style=\"color: #0390D1;\">\r\n          {{selectedCurrencyTax.name | uppercase}}  &nbsp;<i class=\"fas fa-caret-down\"></i>\r\n        </h5>\r\n      </button>\r\n      <mat-menu #TaxCurrencyMenu=\"matMenu\">\r\n        <button (click)=\"selectedCurrencyChangedForTax(taxCurrency)\" *ngFor=\"let taxCurrency of currencyList$ | async\" mat-menu-item>\r\n          {{ taxCurrency.name }}\r\n        </button>\r\n      </mat-menu>\r\n      <button mat-button [matMenuTriggerFor]=\"TaxFinancialYearMenu\">\r\n        <h5 style=\"color: #0390D1;\">\r\n          {{selectedFinancialYearTax.name | uppercase}} &nbsp;<i class=\"fas fa-caret-down\"></i>\r\n        </h5>\r\n      </button>\r\n      <mat-menu #TaxFinancialYearMenu=\"matMenu\">\r\n        <button (click)=\"selectedFinancialYearForTax(financialYear)\"\r\n          *ngFor=\"let financialYear of financialYearList$ | async\"\r\n          mat-menu-item\r\n        >\r\n          {{ financialYear.name }}\r\n        </button>\r\n      </mat-menu>\r\n      <hum-button [type]=\"'download'\" [text]=\"'PDF EXPORT'\"></hum-button>\r\n    </span>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-6\">\r\n            <hum-table [headers]=\"taxListHeaders$\" [items]=\"taxList$\"></hum-table>\r\n        </div>\r\n    </div>\r\n    <br />\r\n    <div class=\"row header\">\r\n        <span class=\"action_header\">Salary Pension\r\n      <button mat-button [matMenuTriggerFor]=\"PensionCurrencyMenu\">\r\n        <h5 style=\"color: #0390D1;\">\r\n          {{selectedCurrencyPension.name | uppercase}} &nbsp;<i class=\"fas fa-caret-down\"></i>\r\n        </h5>\r\n      </button>\r\n      <mat-menu #PensionCurrencyMenu=\"matMenu\">\r\n        <button (click)=\"selectedCurrencyChangedForPension(pensionCurrency)\" *ngFor=\"let pensionCurrency of currencyList$ | async\" mat-menu-item>\r\n          {{ pensionCurrency.name }}\r\n        </button>\r\n      </mat-menu>\r\n      <button mat-button [matMenuTriggerFor]=\"PensionFinancialYearMenu\">\r\n        <h5 style=\"color: #0390D1;\">\r\n          {{selectedFinancialYearPension.name | uppercase}}&nbsp;<i class=\"fas fa-caret-down\"></i>\r\n        </h5>\r\n      </button>\r\n      <mat-menu #PensionFinancialYearMenu=\"matMenu\">\r\n        <button (click)=\"selectedFinancialYearForPension(financialYear)\"\r\n          *ngFor=\"let financialYear of financialYearList$ | async\"\r\n          mat-menu-item\r\n        >\r\n          {{ financialYear.name }}\r\n        </button>\r\n      </mat-menu>\r\n      <hum-button [type]=\"'download'\" [text]=\"'PDF EXPORT'\"></hum-button>\r\n    </span>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-8\">\r\n            <hum-table [headers]=\"pensionListHeaders$\" [items]=\"pensionList$\"></hum-table>\r\n        </div>\r\n    </div>\r\n</mat-card>"

/***/ }),

/***/ "./src/app/hr/components/employee-pension/employee-pension.component.scss":
/*!********************************************************************************!*\
  !*** ./src/app/hr/components/employee-pension/employee-pension.component.scss ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".header {\n  font-size: 18px;\n  font-weight: 500;\n  margin-left: 5px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvaHIvY29tcG9uZW50cy9lbXBsb3llZS1wZW5zaW9uL2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXGhyXFxjb21wb25lbnRzXFxlbXBsb3llZS1wZW5zaW9uXFxlbXBsb3llZS1wZW5zaW9uLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksZUFBZTtFQUNmLGdCQUFnQjtFQUNoQixnQkFBZ0IsRUFBQSIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtcGVuc2lvbi9lbXBsb3llZS1wZW5zaW9uLmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsiLmhlYWRlciB7XHJcbiAgICBmb250LXNpemU6IDE4cHg7XHJcbiAgICBmb250LXdlaWdodDogNTAwO1xyXG4gICAgbWFyZ2luLWxlZnQ6IDVweDtcclxufSJdfQ== */"

/***/ }),

/***/ "./src/app/hr/components/employee-pension/employee-pension.component.ts":
/*!******************************************************************************!*\
  !*** ./src/app/hr/components/employee-pension/employee-pension.component.ts ***!
  \******************************************************************************/
/*! exports provided: EmployeePensionComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeePensionComponent", function() { return EmployeePensionComponent; });
/* harmony import */ var _services_employee_pension_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./../../services/employee-pension.service */ "./src/app/hr/services/employee-pension.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var EmployeePensionComponent = /** @class */ (function () {
    function EmployeePensionComponent(toastr, commonLoader, employeePensionService, activeRoute) {
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.employeePensionService = employeePensionService;
        this.activeRoute = activeRoute;
        // dataSource
        this.pensionListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_4__["of"])([
            'Date',
            'Gross Salary',
            'Pension Rate',
            'Pension Deducation',
            'Profit',
            'Total'
        ]);
        this.taxListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_4__["of"])(['Date', 'Currency', 'Office', 'Total Tax']);
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_4__["ReplaySubject"](1);
    }
    EmployeePensionComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.initModel();
        this.selectedFinancialYearPension = {
            value: 0,
            name: 'Financial Year'
        };
        this.selectedFinancialYearTax = { value: 0, name: 'Financial Year' };
        this.getCurrencyList();
        // this.getFinancialYearList();
        this.activeRoute.params.subscribe(function (params) {
            _this.employeeId = params['id'];
        });
        Object(rxjs__WEBPACK_IMPORTED_MODULE_4__["forkJoin"])([this.getCurrencyList(), this.getFinancialYearList()])
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeAllCurrency(result[0]);
            _this.subscribeAllFinanacialYear(result[1]);
        });
        // this.getAllPensionList(data.FinancialYearId, data.CurrencyId);
        // this.getAllSalaryTaxData(data.FinancialYearId, data.CurrencyId);
        // this.getEmployeeTaxCalculation(this.employeeId, data.FinancialYearId);
    };
    EmployeePensionComponent.prototype.initModel = function () {
        this.selectedCurrencyTax = {
            name: null,
            value: null
        };
        this.selectedCurrencyPension = {
            name: null,
            value: null,
        };
        this.selectedFinancialYearPension = {
            name: null,
            value: null,
        };
        this.selectedFinancialYearTax = {
            name: null,
            value: null,
        };
    };
    //#region "get currency  List"
    EmployeePensionComponent.prototype.getCurrencyList = function () {
        return this.employeePensionService.GetCurrencyList();
        // this.commonLoader.showLoader();
        // this.employeePensionService.GetCurrencyList().subscribe(
        //   x => {
        //     this.commonLoader.hideLoader();
        //     if (x.data.CurrencyList.length > 0) {
        //       this.currencyList$ = of(
        //         x.data.CurrencyList.map(y => {
        //           return {
        //             value: y.CurrencyId,
        //             name: y.CurrencyName
        //           } as IDropDownModel;
        //         })
        //       );
        //       this.currencyList$.subscribe(element => {
        //         // pre selected currency
        //         this.selectedCurrencyTax = {
        //           value: element[0].value,
        //           name: element[0].name
        //         };
        //         this.selectedCurrencyPension = {
        //           value: element[0].value,
        //           name: element[0].name
        //         };
        //       });
        //     }
        //   },
        //   () => {
        //     this.commonLoader.hideLoader();
        //   }
        // );
    };
    //#endregion
    EmployeePensionComponent.prototype.subscribeAllCurrency = function (response) {
        var _this = this;
        this.currencyList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_4__["of"])(response.data.CurrencyList.map(function (y) {
            return {
                value: y.CurrencyId,
                name: y.CurrencyName
            };
        }));
        this.currencyList$.subscribe(function (element) {
            // pre selected currency
            _this.selectedCurrencyTax = {
                value: element[0].value,
                name: element[0].name
            };
            _this.selectedCurrencyPension = {
                value: element[0].value,
                name: element[0].name
            };
        });
    };
    //#region "get Financial Year List"
    EmployeePensionComponent.prototype.getFinancialYearList = function () {
        this.commonLoader.showLoader();
        return this.employeePensionService.GetFinancialYearList();
        // this.employeePensionService.GetFinancialYearList().subscribe(
        //   x => {
        //     this.commonLoader.hideLoader();
        //     if (
        //       x.data.FinancialYearDetailList !== undefined &&
        //       x.data.FinancialYearDetailList.length > 0
        //     ) {
        //       this.financialYearList$ = of(
        //         x.data.FinancialYearDetailList.map(y => {
        //           return {
        //             value: y.FinancialYearId,
        //             name: y.FinancialYearName
        //           } as IDropDownModel;
        //         })
        //       );
        //       this.financialYearList$.subscribe(element => {
        //         this.selectedFinancialYearTax = {
        //           value: element[0].value,
        //           name: element[0].name
        //         };
        //         this.selectedFinancialYearPension = {
        //           value: element[0].value,
        //           name: element[0].name
        //         };
        //       });
        //     } else {
        //       this.selectedFinancialYearPension = {
        //         value: 0,
        //         name: "Financial Year"
        //       };
        //       this.selectedFinancialYearTax = { value: 0, name: "Financial Year" };
        //     }
        //   },
        //   error => {
        //     this.commonLoader.hideLoader();
        //   }
        // );
    };
    //#endregion
    EmployeePensionComponent.prototype.subscribeAllFinanacialYear = function (response) {
        var _this = this;
        if (response.data.FinancialYearDetailList !== undefined &&
            response.data.FinancialYearDetailList.length > 0) {
            this.financialYearList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_4__["of"])(response.data.FinancialYearDetailList.map(function (y) {
                return {
                    value: y.FinancialYearId,
                    name: y.FinancialYearName
                };
            }));
            this.commonLoader.hideLoader();
            this.financialYearList$.subscribe(function (element) {
                _this.selectedFinancialYearTax = {
                    value: element[0].value,
                    name: element[0].name
                };
                _this.selectedFinancialYearPension = {
                    value: element[0].value,
                    name: element[0].name
                };
            }, function (error) {
                _this.commonLoader.hideLoader();
            });
        }
        else {
            this.selectedFinancialYearPension = {
                value: 0,
                name: 'Financial Year'
            };
            this.selectedFinancialYearTax = { value: 0, name: 'Financial Year' };
        }
    };
    EmployeePensionComponent.prototype.selectedCurrencyChangedForTax = function (taxCurrency) {
        this.selectedCurrencyTax = {
            value: taxCurrency.value,
            name: taxCurrency.name
        };
    };
    EmployeePensionComponent.prototype.selectedCurrencyChangedForPension = function (pensionCurrency) {
        this.selectedCurrencyPension = {
            value: pensionCurrency.value,
            name: pensionCurrency.name
        };
    };
    EmployeePensionComponent.prototype.selectedFinancialYearForTax = function (financialYear) {
        this.selectedFinancialYearTax = {
            value: financialYear.value,
            name: financialYear.name
        };
    };
    EmployeePensionComponent.prototype.selectedFinancialYearForPension = function (financialYear) {
        this.selectedFinancialYearPension = {
            value: financialYear.value,
            name: financialYear.name
        };
    };
    //#region "getAllPensionList"
    EmployeePensionComponent.prototype.getAllPensionList = function (yearId, currencyId) {
        var model = {
            OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'), 32),
            EmployeeId: this.employeeId,
            FinancialYearId: yearId,
            CurrencyId: currencyId
        };
    };
    EmployeePensionComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-employee-pension',
            template: __webpack_require__(/*! ./employee-pension.component.html */ "./src/app/hr/components/employee-pension/employee-pension.component.html"),
            styles: [__webpack_require__(/*! ./employee-pension.component.scss */ "./src/app/hr/components/employee-pension/employee-pension.component.scss")]
        }),
        __metadata("design:paramtypes", [ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__["CommonLoaderService"],
            _services_employee_pension_service__WEBPACK_IMPORTED_MODULE_0__["EmployeePensionService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_5__["ActivatedRoute"]])
    ], EmployeePensionComponent);
    return EmployeePensionComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-resignation/employee-resignation.component.html":
/*!****************************************************************************************!*\
  !*** ./src/app/hr/components/employee-resignation/employee-resignation.component.html ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container-fluid\" >\r\n    <h4 class=\"letter_spacing_2\">Resignation Exit Interview\r\n      <hum-button [type]=\"'add'\" [text]=\"'ADD RESIGNATION'\" (click)=\"addResignation()\" [disabled]=\"employeeDetail.IsResigned\"></hum-button>\r\n      <hum-button [type]=\"'edit'\" [text]=\"'SAVE CHANGES'\" (click)=\"saveResignationForm(resignationForm.value)\" *ngIf=\"employeeDetail.IsResigned\"></hum-button>\r\n      <hum-button [type]=\"'edit'\" [text]=\"'SAVE CHANGES'\" [disabled]=\"!employeeDetail.IsResigned\" *ngIf=\"!employeeDetail.IsResigned\"></hum-button>\r\n      <hum-button [type]=\"'minus'\" [text]=\"'REHIRE EMPLOYEE'\" [disabled]=\"!employeeDetail.IsResigned\"></hum-button>\r\n      <hum-button [type]=\"'exclamation'\" [text]=\"'REVOKE RESIGNATION'\" [disabled]=\"!employeeDetail.IsResigned\"></hum-button>\r\n    </h4>\r\n    <h5 *ngIf=\"!employeeDetail.IsResigned\"><i class=\"fas fa-info-circle icon_color_yellow\"></i> &nbsp;There is no Active Resignation. Please Add one in order to see its details.</h5>\r\n  <div class=\"row\" *ngIf=\"employeeDetail.IsResigned\" humAddScroll [height]=\"250\">\r\n    <div class=\"col-md-12\">\r\n      <form [formGroup]=\"resignationForm\">\r\n        <p class=\"font_size_16\">When did the employee resign?</p>\r\n        <mat-form-field>\r\n          <input #resignDate class=\"fixedWidth\"\r\n            formControlName=\"ResignDate\" matInput [matDatepicker]=\"picker\" placeholder=\"Resigned On (Date)\" >\r\n          <mat-datepicker-toggle matSuffix [for]=\"picker\"></mat-datepicker-toggle>\r\n          <mat-datepicker #picker></mat-datepicker>\r\n        </mat-form-field>\r\n        <p class=\"font_size_16\">Are there any Unresolved Issues or Additional Comments?</p>\r\n        <mat-radio-group formControlName=\"IsIssueUnresolved\">\r\n          <mat-radio-button value=\"true\">Yes</mat-radio-button>\r\n          <mat-radio-button value=\"false\">No</mat-radio-button>\r\n        </mat-radio-group>\r\n        <p class=\"font_size_16\">If answer is Yes, please explain fully</p>\r\n        <mat-form-field style=\"width: 50%;\" >\r\n          <textarea formControlName=\"Issues\" matInput rows=\"3\" placeholder=\"Comments & Issues\"></textarea>\r\n        </mat-form-field>\r\n        <mat-divider></mat-divider>\r\n        <h4>Feeling About Employee Aspects</h4>\r\n        <table class=\"table table-striped\">\r\n          <tbody formArrayName=\"QuestionType1\">\r\n            <tr *ngFor=\"let item of resignationForm.get('QuestionType1')['controls']; let i = index;\" [formGroupName]=\"i\">\r\n              <td width=\"30%\" class=\"font_size_16 font_weight_500\">{{item.value.QuestionText}}</td>\r\n              <td>\r\n                <mat-radio-group formControlName=\"Answer\">\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"1\">Very Satisfied</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"2\">Satisfied</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"3\">Dissatisfied</mat-radio-button>\r\n                </mat-radio-group>\r\n              </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n        <h4>Reason for Leaving</h4>\r\n        <div class=\"row\" formArrayName=\"QuestionType2\">\r\n          <div class=\"col-md-3\" *ngFor=\"let item of resignationForm.get('QuestionType2')['controls']; let i = index;\" [formGroupName]=\"i\">\r\n              <mat-checkbox formControlName=\"Answer\" class=\"font_size_16 font_weight_500\">{{item.value.QuestionText}}</mat-checkbox>\r\n          </div>\r\n        </div>\r\n        <h4>The Department</h4>\r\n        <table class=\"table table-striped\">\r\n          <tbody formArrayName=\"QuestionType3\">\r\n            <tr *ngFor=\"let item of resignationForm.get('QuestionType3')['controls']; let i = index;\" [formGroupName]=\"i\">\r\n              <td width=\"30%\" class=\"font_size_16 font_weight_500\">{{item.value.QuestionText}}</td>\r\n              <td>\r\n                <mat-radio-group formControlName=\"Answer\">\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"1\">Strongly Disagree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"2\">Disagree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"3\">Neutral</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"4\">Agree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"5\">Strongly Agree</mat-radio-button>\r\n                </mat-radio-group>\r\n              </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n        <h4>The Job Itself</h4>\r\n        <table class=\"table table-striped\">\r\n          <tbody formArrayName=\"QuestionType4\">\r\n            <tr *ngFor=\"let item of resignationForm.get('QuestionType4')['controls']; let i = index;\" [formGroupName]=\"i\">\r\n              <td width=\"30%\" class=\"font_size_16 font_weight_500\">{{item.value.QuestionText}}</td>\r\n              <td>\r\n                <mat-radio-group formControlName=\"Answer\">\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"1\">Strongly Disagree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"2\">Disagree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"3\">Neutral</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"4\">Agree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"5\">Strongly Agree</mat-radio-button>\r\n                </mat-radio-group>\r\n              </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n        <h4>My Supervisor</h4>\r\n        <table class=\"table table-striped\">\r\n          <tbody formArrayName=\"QuestionType5\">\r\n            <tr *ngFor=\"let item of resignationForm.get('QuestionType5')['controls']; let i = index;\" [formGroupName]=\"i\">\r\n              <td width=\"30%\" class=\"font_size_16 font_weight_500\">{{item.value.QuestionText}}</td>\r\n              <td>\r\n                <mat-radio-group formControlName=\"Answer\">\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"1\">Strongly Disagree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"2\">Disagree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"3\">Neutral</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"4\">Agree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"5\">Strongly Agree</mat-radio-button>\r\n                </mat-radio-group>\r\n              </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n        <h4>The Management</h4>\r\n        <table class=\"table table-striped\">\r\n          <tbody formArrayName=\"QuestionType6\">\r\n            <tr *ngFor=\"let item of resignationForm.get('QuestionType6')['controls']; let i = index;\" [formGroupName]=\"i\">\r\n              <td width=\"30%\" class=\"font_size_16 font_weight_500\">{{item.value.QuestionText}}</td>\r\n              <td>\r\n                <mat-radio-group formControlName=\"Answer\">\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"1\">Strongly Disagree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"2\">Disagree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"3\">Neutral</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"4\">Agree</mat-radio-button>\r\n                  <mat-radio-button class=\"font_size_16 padding_right_20\" value=\"5\">Strongly Agree</mat-radio-button>\r\n                </mat-radio-group>\r\n              </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n        <!-- <button type=\"submit\" #submitBtn style=\"display: none;\"></button> -->\r\n      </form>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-resignation/employee-resignation.component.scss":
/*!****************************************************************************************!*\
  !*** ./src/app/hr/components/employee-resignation/employee-resignation.component.scss ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".letter_spacing_2 {\n  letter-spacing: 2px; }\n\n.font_size_16 {\n  font-size: 16px; }\n\n.font_weight_500 {\n  font-weight: 500; }\n\n.padding_right_20 {\n  padding-right: 20px; }\n\n.icon_color_yellow {\n  color: darkgoldenrod; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvaHIvY29tcG9uZW50cy9lbXBsb3llZS1yZXNpZ25hdGlvbi9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxoclxcY29tcG9uZW50c1xcZW1wbG95ZWUtcmVzaWduYXRpb25cXGVtcGxveWVlLXJlc2lnbmF0aW9uLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0UsbUJBQW1CLEVBQUE7O0FBRXJCO0VBQ0UsZUFBZSxFQUFBOztBQUVqQjtFQUNFLGdCQUFnQixFQUFBOztBQUVsQjtFQUNFLG1CQUFtQixFQUFBOztBQUVyQjtFQUNFLG9CQUFvQixFQUFBIiwiZmlsZSI6InNyYy9hcHAvaHIvY29tcG9uZW50cy9lbXBsb3llZS1yZXNpZ25hdGlvbi9lbXBsb3llZS1yZXNpZ25hdGlvbi5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5sZXR0ZXJfc3BhY2luZ18ye1xyXG4gIGxldHRlci1zcGFjaW5nOiAycHg7XHJcbn1cclxuLmZvbnRfc2l6ZV8xNntcclxuICBmb250LXNpemU6IDE2cHg7XHJcbn1cclxuLmZvbnRfd2VpZ2h0XzUwMCB7XHJcbiAgZm9udC13ZWlnaHQ6IDUwMDtcclxufVxyXG4ucGFkZGluZ19yaWdodF8yMCB7XHJcbiAgcGFkZGluZy1yaWdodDogMjBweDtcclxufVxyXG4uaWNvbl9jb2xvcl95ZWxsb3d7XHJcbiAgY29sb3I6IGRhcmtnb2xkZW5yb2Q7XHJcbn1cclxuIl19 */"

/***/ }),

/***/ "./src/app/hr/components/employee-resignation/employee-resignation.component.ts":
/*!**************************************************************************************!*\
  !*** ./src/app/hr/components/employee-resignation/employee-resignation.component.ts ***!
  \**************************************************************************************/
/*! exports provided: EmployeeResignationComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeResignationComponent", function() { return EmployeeResignationComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _services_hr_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _services_employee_list_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../services/employee-list.service */ "./src/app/hr/services/employee-list.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};









var EmployeeResignationComponent = /** @class */ (function () {
    function EmployeeResignationComponent(commonLoader, toastr, hrService, fb, employeeService, activatedRoute) {
        var _this = this;
        this.commonLoader = commonLoader;
        this.toastr = toastr;
        this.hrService = hrService;
        this.fb = fb;
        this.employeeService = employeeService;
        this.activatedRoute = activatedRoute;
        this.questionTypes = [
            { value: 1, name: 'Feeling About Employee Aspects' },
            { value: 2, name: 'Reason Of Leaving' },
            { value: 3, name: 'The Department' },
            { value: 4, name: 'The Job Itself' },
            { value: 5, name: 'My Supervisor' },
            { value: 6, name: 'The Management' },
        ];
        this.questionByType = [];
        this.activatedRoute.params.subscribe(function (params) {
            _this.employeeId = +params['id'];
        });
        this.resignationForm = this.fb.group({
            ResignDate: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_6__["Validators"].required],
            IsIssueUnresolved: ['false'],
            Issues: [''],
            QuestionType1: this.fb.array([this.createQuestion(null, null, null, null)]),
            QuestionType2: this.fb.array([this.createQuestion(null, null, null, null)]),
            QuestionType3: this.fb.array([this.createQuestion(null, null, null, null)]),
            QuestionType4: this.fb.array([this.createQuestion(null, null, null, null)]),
            QuestionType5: this.fb.array([this.createQuestion(null, null, null, null)]),
            QuestionType6: this.fb.array([this.createQuestion(null, null, null, null)]),
        });
    }
    EmployeeResignationComponent.prototype.ngOnInit = function () {
        // this.getExitInterviewQuestionsList();
        if (this.employeeDetail.IsResigned) {
            this.getResignationDetail();
        }
    };
    EmployeeResignationComponent.prototype.ngOnChanges = function () {
        // console.log(this.employeeDetail);
    };
    EmployeeResignationComponent.prototype.createQuestion = function (QuestionId, QuestionText, QuestionTypeId, Answer) {
        return this.fb.group({
            QuestionId: [QuestionId],
            QuestionText: [QuestionText],
            QuestionTypeId: [QuestionTypeId],
            Answer: [Answer]
        });
    };
    EmployeeResignationComponent.prototype.getExitInterviewQuestionsList = function () {
        var _this = this;
        var pageModel = {
            PageSize: 0,
            PageIndex: 0,
            IsPaginated: false
        };
        this.hrService.getExitInterviewQuestionsList(pageModel).subscribe(function (x) {
            if (x.Result.length > 0) {
                _this.exitInterviewQuestionsList = [];
                _this.exitInterviewQuestionsList = x.Result.map(function (element) {
                    return {
                        Id: element.Id,
                        QuestionText: element.QuestionText,
                        QuestionType: element.QuestionType,
                        QuestionTypeText: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_4__["QuestionTypeName"].get(element.QuestionType),
                        SequencePosition: element.SequencePosition,
                    };
                });
                _this.groupedQuestions = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_5__["StaticUtilities"].groupBy(_this.exitInterviewQuestionsList, function (y) { return y.QuestionType; });
                _this.questionByType = [];
                _this.groupedQuestions.forEach(function (value, key) {
                    _this.questionByType[key] = value;
                });
                _this.initializeAllQuestionByType();
            }
        }, function (error) {
            _this.toastr.warning(error);
        });
    };
    EmployeeResignationComponent.prototype.initializeAllQuestionByType = function () {
        var _this = this;
        var _loop_1 = function (i) {
            this_1.resignationForm.get('QuestionType' + i).removeAt(0);
            this_1.resignationForm.controls['QuestionType' + i].setValue([]);
            this_1.questionByType[i].forEach(function (e) {
                if (i !== 2) {
                    _this.resignationForm.controls['QuestionType' + i]
                        .push(_this.createQuestion(e.Id, e.QuestionText, e.QuestionType, '2'));
                }
                else {
                    _this.resignationForm.controls['QuestionType' + i]
                        .push(_this.createQuestion(e.Id, e.QuestionText, e.QuestionType, false));
                }
            });
        };
        var this_1 = this;
        for (var i = 1; i <= 6; i++) {
            _loop_1(i);
        }
    };
    EmployeeResignationComponent.prototype.setAllAnswers = function () {
        var _this = this;
        var _loop_2 = function (i) {
            this_2.resignationForm.get('QuestionType' + i).removeAt(0);
            // (this.resignationForm.controls['QuestionType' + i] as FormArray) = this.fb.array([]);
            this_2.questionByType[i].forEach(function (e) {
                if (i !== 2) {
                    _this.resignationForm.controls['QuestionType' + i]
                        .push(_this.createQuestion(e.QuestionId, e.QuestionText, e.QuestionType, (e.Answer + '').trim()));
                }
                else {
                    _this.resignationForm.controls['QuestionType' + i]
                        .push(_this.createQuestion(e.QuestionId, e.QuestionText, e.QuestionType, (e.Answer === 1) ? true : false));
                }
            });
        };
        var this_2 = this;
        for (var i = 1; i <= 6; i++) {
            _loop_2(i);
        }
    };
    EmployeeResignationComponent.prototype.saveResignationForm = function (value) {
        var _this = this;
        if (!this.resignationForm.valid) {
            this.toastr.warning('Please select Resign Date');
            return;
        }
        if (value.IsIssueUnresolved === 'true' && value.Issues === '') {
            this.toastr.warning('Please enter Comments & Issues!');
            return;
        }
        this.commonLoader.showLoader();
        value.QuestionType2.forEach(function (element) {
            if (element.Answer) {
                element.Answer = '1';
            }
            else {
                element.Answer = '0';
            }
        });
        var model = {
            ResignDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_5__["StaticUtilities"].getLocalDate(this.resignationForm.get('ResignDate').value),
            EmployeeID: this.employeeId,
            IsUnresolvedIssue: (value.IsIssueUnresolved === 'true'),
            CommentsIssues: value.Issues,
            QuestionType1: value.QuestionType1,
            QuestionType2: value.QuestionType2,
            QuestionType3: value.QuestionType3,
            QuestionType4: value.QuestionType4,
            QuestionType5: value.QuestionType5,
            QuestionType6: value.QuestionType6,
        };
        this.employeeService.saveResignation(model).subscribe(function (res) {
            if (res) {
                _this.commonLoader.hideLoader();
                _this.toastr.success('Resignation saved successfully!');
            }
        }, function (err) {
            _this.commonLoader.hideLoader();
            _this.toastr.warning(err);
        });
    };
    EmployeeResignationComponent.prototype.addResignation = function () {
        var _this = this;
        if (this.employeeDetail.IsResigned) {
            return;
        }
        this.commonLoader.showLoader();
        this.employeeService.addResignation(this.employeeId).subscribe(function (res) {
            if (res) {
                _this.employeeDetail.IsResigned = true;
                _this.getResignationDetail();
                _this.commonLoader.hideLoader();
            }
        }, function (err) {
            _this.commonLoader.hideLoader();
            _this.toastr.warning(err);
        });
    };
    EmployeeResignationComponent.prototype.getResignationDetail = function () {
        var _this = this;
        this.employeeService.getResignationDetailById(this.employeeId).subscribe(function (res) {
            if (res.ResignationDetail !== null) {
                _this.resignationForm.patchValue({
                    ResignDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_5__["StaticUtilities"].setLocalDate(res.ResignationDetail.ResignDate),
                    IsIssueUnresolved: (' ' + res.ResignationDetail.IsIssueUnresolved).trim(),
                    Issues: res.ResignationDetail.CommentsIssues.trim()
                });
                _this.groupedQuestions = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_5__["StaticUtilities"].groupBy(res.ResignationQuestionDetail, function (y) { return y.QuestionType; });
                _this.questionByType = [];
                _this.groupedQuestions.forEach(function (value, key) {
                    _this.questionByType[key] = value;
                });
                _this.setAllAnswers();
            }
            else {
                _this.getExitInterviewQuestionsList();
            }
        }, function (err) {
            _this.toastr.warning(err);
        });
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], EmployeeResignationComponent.prototype, "employeeDetail", void 0);
    EmployeeResignationComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-employee-resignation',
            template: __webpack_require__(/*! ./employee-resignation.component.html */ "./src/app/hr/components/employee-resignation/employee-resignation.component.html"),
            styles: [__webpack_require__(/*! ./employee-resignation.component.scss */ "./src/app/hr/components/employee-resignation/employee-resignation.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_1__["CommonLoaderService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"],
            _services_hr_service__WEBPACK_IMPORTED_MODULE_3__["HrService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_6__["FormBuilder"],
            _services_employee_list_service__WEBPACK_IMPORTED_MODULE_7__["EmployeeListService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_8__["ActivatedRoute"]])
    ], EmployeeResignationComponent);
    return EmployeeResignationComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/employee-salary-config/employee-salary-config.component.html":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/components/employee-salary-config/employee-salary-config.component.html ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>\r\n  employee-salary-config works!\r\n</p>\r\n"

/***/ }),

/***/ "./src/app/hr/components/employee-salary-config/employee-salary-config.component.scss":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/components/employee-salary-config/employee-salary-config.component.scss ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW1wbG95ZWUtc2FsYXJ5LWNvbmZpZy9lbXBsb3llZS1zYWxhcnktY29uZmlnLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/components/employee-salary-config/employee-salary-config.component.ts":
/*!******************************************************************************************!*\
  !*** ./src/app/hr/components/employee-salary-config/employee-salary-config.component.ts ***!
  \******************************************************************************************/
/*! exports provided: EmployeeSalaryConfigComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeSalaryConfigComponent", function() { return EmployeeSalaryConfigComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var EmployeeSalaryConfigComponent = /** @class */ (function () {
    function EmployeeSalaryConfigComponent() {
    }
    EmployeeSalaryConfigComponent.prototype.ngOnInit = function () {
    };
    EmployeeSalaryConfigComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-employee-salary-config',
            template: __webpack_require__(/*! ./employee-salary-config.component.html */ "./src/app/hr/components/employee-salary-config/employee-salary-config.component.html"),
            styles: [__webpack_require__(/*! ./employee-salary-config.component.scss */ "./src/app/hr/components/employee-salary-config/employee-salary-config.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], EmployeeSalaryConfigComponent);
    return EmployeeSalaryConfigComponent;
}());



/***/ }),

/***/ "./src/app/hr/components/entry-component/entry-component.component.html":
/*!******************************************************************************!*\
  !*** ./src/app/hr/components/entry-component/entry-component.component.html ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-sidenav-container class=\"example-container\">\r\n  <mat-sidenav #sidenav mode=\"side\" opened=\"true\">\r\n    <app-dbsidebar></app-dbsidebar>\r\n  </mat-sidenav>\r\n\r\n  <mat-sidenav-content>\r\n    <mat-card class=\"header header_mat_card\">\r\n      <div class=\"container-fluid\">\r\n        <div class=\"row\">\r\n          <div class=\"col-sm-12 col-xs-12\">\r\n            <app-dbheader></app-dbheader>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </mat-card>\r\n    <router-outlet></router-outlet>\r\n  </mat-sidenav-content>\r\n</mat-sidenav-container>\r\n"

/***/ }),

/***/ "./src/app/hr/components/entry-component/entry-component.component.scss":
/*!******************************************************************************!*\
  !*** ./src/app/hr/components/entry-component/entry-component.component.scss ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".example-container {\n  position: absolute;\n  top: 0;\n  bottom: 0;\n  left: 0;\n  right: 0; }\n\n.header_mat_card {\n  height: 77px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvaHIvY29tcG9uZW50cy9lbnRyeS1jb21wb25lbnQvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcaHJcXGNvbXBvbmVudHNcXGVudHJ5LWNvbXBvbmVudFxcZW50cnktY29tcG9uZW50LmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0Usa0JBQWtCO0VBQ2xCLE1BQU07RUFDTixTQUFTO0VBQ1QsT0FBTztFQUNQLFFBQVEsRUFBQTs7QUFFVjtFQUNFLFlBQVksRUFBQSIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbXBvbmVudHMvZW50cnktY29tcG9uZW50L2VudHJ5LWNvbXBvbmVudC5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5leGFtcGxlLWNvbnRhaW5lciB7XHJcbiAgcG9zaXRpb246IGFic29sdXRlO1xyXG4gIHRvcDogMDtcclxuICBib3R0b206IDA7XHJcbiAgbGVmdDogMDtcclxuICByaWdodDogMDtcclxufVxyXG4uaGVhZGVyX21hdF9jYXJkIHtcclxuICBoZWlnaHQ6IDc3cHg7XHJcbn1cclxuIl19 */"

/***/ }),

/***/ "./src/app/hr/components/entry-component/entry-component.component.ts":
/*!****************************************************************************!*\
  !*** ./src/app/hr/components/entry-component/entry-component.component.ts ***!
  \****************************************************************************/
/*! exports provided: EntryComponentComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EntryComponentComponent", function() { return EntryComponentComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var EntryComponentComponent = /** @class */ (function () {
    function EntryComponentComponent(globalservice) {
        this.globalservice = globalservice;
    }
    EntryComponentComponent.prototype.ngOnInit = function () {
        this.globalservice.setMenuHeaderName('Employees');
    };
    EntryComponentComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-entry-component',
            template: __webpack_require__(/*! ./entry-component.component.html */ "./src/app/hr/components/entry-component/entry-component.component.html"),
            styles: [__webpack_require__(/*! ./entry-component.component.scss */ "./src/app/hr/components/entry-component/entry-component.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_1__["GlobalSharedService"]])
    ], EntryComponentComponent);
    return EntryComponentComponent;
}());



/***/ }),

/***/ "./src/app/hr/hr-routing.module.ts":
/*!*****************************************!*\
  !*** ./src/app/hr/hr-routing.module.ts ***!
  \*****************************************/
/*! exports provided: HrRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HrRoutingModule", function() { return HrRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./components/entry-component/entry-component.component */ "./src/app/hr/components/entry-component/entry-component.component.ts");
/* harmony import */ var _components_employee_list_employee_list_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/employee-list/employee-list.component */ "./src/app/hr/components/employee-list/employee-list.component.ts");
/* harmony import */ var _components_employee_control_panel_employee_control_panel_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./components/employee-control-panel/employee-control-panel.component */ "./src/app/hr/components/employee-control-panel/employee-control-panel.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};





var routes = [
    {
        path: '', component: _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_2__["EntryComponentComponent"],
        children: [
            { path: 'employees', component: _components_employee_list_employee_list_component__WEBPACK_IMPORTED_MODULE_3__["EmployeeListComponent"] },
            { path: 'employee/:id', component: _components_employee_control_panel_employee_control_panel_component__WEBPACK_IMPORTED_MODULE_4__["EmployeeControlPanelComponent"] }
        ]
    },
    {
        path: 'configuration',
        loadChildren: './configuration/configuration.module#ConfigurationModule'
    }
];
var HrRoutingModule = /** @class */ (function () {
    function HrRoutingModule() {
    }
    HrRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], HrRoutingModule);
    return HrRoutingModule;
}());



/***/ }),

/***/ "./src/app/hr/hr.module.ts":
/*!*********************************!*\
  !*** ./src/app/hr/hr.module.ts ***!
  \*********************************/
/*! exports provided: HrModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HrModule", function() { return HrModule; });
/* harmony import */ var _components_employee_leave_assign_leave_assign_leave_component__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./components/employee-leave/assign-leave/assign-leave.component */ "./src/app/hr/components/employee-leave/assign-leave/assign-leave.component.ts");
/* harmony import */ var _components_employee_leave_employee_leave_add_employee_leave_add_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./components/employee-leave/employee-leave-add/employee-leave-add.component */ "./src/app/hr/components/employee-leave/employee-leave-add/employee-leave-add.component.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _hr_routing_module__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./hr-routing.module */ "./src/app/hr/hr-routing.module.ts");
/* harmony import */ var _components_employee_list_employee_list_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./components/employee-list/employee-list.component */ "./src/app/hr/components/employee-list/employee-list.component.ts");
/* harmony import */ var _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./components/entry-component/entry-component.component */ "./src/app/hr/components/entry-component/entry-component.component.ts");
/* harmony import */ var _shared_share_layout_module__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../shared/share-layout.module */ "./src/app/shared/share-layout.module.ts");
/* harmony import */ var _angular_material_card__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material/card */ "./node_modules/@angular/material/esm5/card.es5.js");
/* harmony import */ var _angular_material_sidenav__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/material/sidenav */ "./node_modules/@angular/material/esm5/sidenav.es5.js");
/* harmony import */ var projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! projects/library/src/public_api */ "./projects/library/src/public_api.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_menu__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/material/menu */ "./node_modules/@angular/material/esm5/menu.es5.js");
/* harmony import */ var _angular_material_icon__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material/icon */ "./node_modules/@angular/material/esm5/icon.es5.js");
/* harmony import */ var _angular_material_button__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/material/button */ "./node_modules/@angular/material/esm5/button.es5.js");
/* harmony import */ var _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @angular/material/datepicker */ "./node_modules/@angular/material/esm5/datepicker.es5.js");
/* harmony import */ var _angular_material_input__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! @angular/material/input */ "./node_modules/@angular/material/esm5/input.es5.js");
/* harmony import */ var _angular_material_paginator__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! @angular/material/paginator */ "./node_modules/@angular/material/esm5/paginator.es5.js");
/* harmony import */ var _angular_material_divider__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! @angular/material/divider */ "./node_modules/@angular/material/esm5/divider.es5.js");
/* harmony import */ var _components_employee_control_panel_employee_control_panel_component__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ./components/employee-control-panel/employee-control-panel.component */ "./src/app/hr/components/employee-control-panel/employee-control-panel.component.ts");
/* harmony import */ var _components_employee_history_employee_history_component__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ./components/employee-history/employee-history.component */ "./src/app/hr/components/employee-history/employee-history.component.ts");
/* harmony import */ var _components_employee_leave_employee_leave_component__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ./components/employee-leave/employee-leave.component */ "./src/app/hr/components/employee-leave/employee-leave.component.ts");
/* harmony import */ var _components_employee_attendance_employee_attendance_component__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ./components/employee-attendance/employee-attendance.component */ "./src/app/hr/components/employee-attendance/employee-attendance.component.ts");
/* harmony import */ var _components_employee_contract_employee_contract_component__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! ./components/employee-contract/employee-contract.component */ "./src/app/hr/components/employee-contract/employee-contract.component.ts");
/* harmony import */ var _components_employee_salary_config_employee_salary_config_component__WEBPACK_IMPORTED_MODULE_24__ = __webpack_require__(/*! ./components/employee-salary-config/employee-salary-config.component */ "./src/app/hr/components/employee-salary-config/employee-salary-config.component.ts");
/* harmony import */ var _components_employee_resignation_employee_resignation_component__WEBPACK_IMPORTED_MODULE_25__ = __webpack_require__(/*! ./components/employee-resignation/employee-resignation.component */ "./src/app/hr/components/employee-resignation/employee-resignation.component.ts");
/* harmony import */ var _components_employee_detail_employee_detail_component__WEBPACK_IMPORTED_MODULE_26__ = __webpack_require__(/*! ./components/employee-detail/employee-detail.component */ "./src/app/hr/components/employee-detail/employee-detail.component.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_27__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _components_employee_history_add_historical_log_add_historical_log_component__WEBPACK_IMPORTED_MODULE_28__ = __webpack_require__(/*! ./components/employee-history/add-historical-log/add-historical-log.component */ "./src/app/hr/components/employee-history/add-historical-log/add-historical-log.component.ts");
/* harmony import */ var _components_employee_history_add_education_add_education_component__WEBPACK_IMPORTED_MODULE_29__ = __webpack_require__(/*! ./components/employee-history/add-education/add-education.component */ "./src/app/hr/components/employee-history/add-education/add-education.component.ts");
/* harmony import */ var _components_employee_history_add_history_outside_country_add_history_outside_country_component__WEBPACK_IMPORTED_MODULE_30__ = __webpack_require__(/*! ./components/employee-history/add-history-outside-country/add-history-outside-country.component */ "./src/app/hr/components/employee-history/add-history-outside-country/add-history-outside-country.component.ts");
/* harmony import */ var _components_employee_history_add_close_relative_add_close_relative_component__WEBPACK_IMPORTED_MODULE_31__ = __webpack_require__(/*! ./components/employee-history/add-close-relative/add-close-relative.component */ "./src/app/hr/components/employee-history/add-close-relative/add-close-relative.component.ts");
/* harmony import */ var _components_employee_history_add_three_reference_details_add_three_reference_details_component__WEBPACK_IMPORTED_MODULE_32__ = __webpack_require__(/*! ./components/employee-history/add-three-reference-details/add-three-reference-details.component */ "./src/app/hr/components/employee-history/add-three-reference-details/add-three-reference-details.component.ts");
/* harmony import */ var _components_employee_history_add_other_skills_add_other_skills_component__WEBPACK_IMPORTED_MODULE_33__ = __webpack_require__(/*! ./components/employee-history/add-other-skills/add-other-skills.component */ "./src/app/hr/components/employee-history/add-other-skills/add-other-skills.component.ts");
/* harmony import */ var _components_employee_history_add_salary_budget_add_salary_budget_component__WEBPACK_IMPORTED_MODULE_34__ = __webpack_require__(/*! ./components/employee-history/add-salary-budget/add-salary-budget.component */ "./src/app/hr/components/employee-history/add-salary-budget/add-salary-budget.component.ts");
/* harmony import */ var _components_employee_history_add_language_add_language_component__WEBPACK_IMPORTED_MODULE_35__ = __webpack_require__(/*! ./components/employee-history/add-language/add-language.component */ "./src/app/hr/components/employee-history/add-language/add-language.component.ts");
/* harmony import */ var saturn_datepicker__WEBPACK_IMPORTED_MODULE_36__ = __webpack_require__(/*! saturn-datepicker */ "./node_modules/saturn-datepicker/fesm5/saturn-datepicker.js");
/* harmony import */ var _components_employee_pension_employee_pension_component__WEBPACK_IMPORTED_MODULE_37__ = __webpack_require__(/*! ./components/employee-pension/employee-pension.component */ "./src/app/hr/components/employee-pension/employee-pension.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






































var HrModule = /** @class */ (function () {
    function HrModule() {
    }
    HrModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["NgModule"])({
            declarations: [
                _components_employee_list_employee_list_component__WEBPACK_IMPORTED_MODULE_5__["EmployeeListComponent"],
                _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_6__["EntryComponentComponent"],
                _components_employee_control_panel_employee_control_panel_component__WEBPACK_IMPORTED_MODULE_19__["EmployeeControlPanelComponent"],
                _components_employee_history_employee_history_component__WEBPACK_IMPORTED_MODULE_20__["EmployeeHistoryComponent"],
                _components_employee_leave_employee_leave_component__WEBPACK_IMPORTED_MODULE_21__["EmployeeLeaveComponent"],
                _components_employee_attendance_employee_attendance_component__WEBPACK_IMPORTED_MODULE_22__["EmployeeAttendanceComponent"],
                _components_employee_contract_employee_contract_component__WEBPACK_IMPORTED_MODULE_23__["EmployeeContractComponent"],
                _components_employee_salary_config_employee_salary_config_component__WEBPACK_IMPORTED_MODULE_24__["EmployeeSalaryConfigComponent"],
                _components_employee_resignation_employee_resignation_component__WEBPACK_IMPORTED_MODULE_25__["EmployeeResignationComponent"],
                _components_employee_detail_employee_detail_component__WEBPACK_IMPORTED_MODULE_26__["EmployeeDetailComponent"],
                _components_employee_history_add_historical_log_add_historical_log_component__WEBPACK_IMPORTED_MODULE_28__["AddHistoricalLogComponent"],
                _components_employee_history_add_education_add_education_component__WEBPACK_IMPORTED_MODULE_29__["AddEducationComponent"],
                _components_employee_history_add_history_outside_country_add_history_outside_country_component__WEBPACK_IMPORTED_MODULE_30__["AddHistoryOutsideCountryComponent"],
                _components_employee_history_add_close_relative_add_close_relative_component__WEBPACK_IMPORTED_MODULE_31__["AddCloseRelativeComponent"],
                _components_employee_history_add_three_reference_details_add_three_reference_details_component__WEBPACK_IMPORTED_MODULE_32__["AddThreeReferenceDetailsComponent"],
                _components_employee_history_add_other_skills_add_other_skills_component__WEBPACK_IMPORTED_MODULE_33__["AddOtherSkillsComponent"],
                _components_employee_history_add_salary_budget_add_salary_budget_component__WEBPACK_IMPORTED_MODULE_34__["AddSalaryBudgetComponent"],
                _components_employee_history_add_language_add_language_component__WEBPACK_IMPORTED_MODULE_35__["AddLanguageComponent"],
                _components_employee_leave_employee_leave_add_employee_leave_add_component__WEBPACK_IMPORTED_MODULE_1__["EmployeeLeaveAddComponent"],
                _components_employee_leave_assign_leave_assign_leave_component__WEBPACK_IMPORTED_MODULE_0__["AssignLeaveComponent"],
                _components_employee_pension_employee_pension_component__WEBPACK_IMPORTED_MODULE_37__["EmployeePensionComponent"]
            ],
            imports: [
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatFormFieldModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatSelectModule"],
                _angular_common__WEBPACK_IMPORTED_MODULE_3__["CommonModule"],
                _hr_routing_module__WEBPACK_IMPORTED_MODULE_4__["HrRoutingModule"],
                _shared_share_layout_module__WEBPACK_IMPORTED_MODULE_7__["ShareLayoutModule"],
                _angular_material_card__WEBPACK_IMPORTED_MODULE_8__["MatCardModule"],
                _angular_material_sidenav__WEBPACK_IMPORTED_MODULE_9__["MatSidenavModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__["LibraryModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__["SubHeaderTemplateModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_11__["ReactiveFormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_11__["FormsModule"],
                // Material
                _angular_material_menu__WEBPACK_IMPORTED_MODULE_12__["MatMenuModule"],
                _angular_material_icon__WEBPACK_IMPORTED_MODULE_13__["MatIconModule"],
                _angular_material_sidenav__WEBPACK_IMPORTED_MODULE_9__["MatSidenavModule"],
                _angular_material_card__WEBPACK_IMPORTED_MODULE_8__["MatCardModule"],
                _angular_material_button__WEBPACK_IMPORTED_MODULE_14__["MatButtonModule"],
                _shared_share_layout_module__WEBPACK_IMPORTED_MODULE_7__["ShareLayoutModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__["SubHeaderTemplateModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__["LibraryModule"],
                _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_15__["MatDatepickerModule"],
                _angular_material_input__WEBPACK_IMPORTED_MODULE_16__["MatInputModule"],
                _angular_material_paginator__WEBPACK_IMPORTED_MODULE_17__["MatPaginatorModule"],
                _angular_material_divider__WEBPACK_IMPORTED_MODULE_18__["MatDividerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatTabsModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatDialogModule"],
                // ConfigurationModule
                saturn_datepicker__WEBPACK_IMPORTED_MODULE_36__["SatDatepickerModule"],
                saturn_datepicker__WEBPACK_IMPORTED_MODULE_36__["SatNativeDateModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatSelectModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatOptionModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatDialogModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatSelectModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatTableModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatCheckboxModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_27__["MatRadioModule"]
            ],
            entryComponents: [
                _components_employee_leave_employee_leave_add_employee_leave_add_component__WEBPACK_IMPORTED_MODULE_1__["EmployeeLeaveAddComponent"],
                _components_employee_leave_assign_leave_assign_leave_component__WEBPACK_IMPORTED_MODULE_0__["AssignLeaveComponent"],
                _components_employee_history_add_historical_log_add_historical_log_component__WEBPACK_IMPORTED_MODULE_28__["AddHistoricalLogComponent"],
                _components_employee_history_add_education_add_education_component__WEBPACK_IMPORTED_MODULE_29__["AddEducationComponent"],
                _components_employee_history_add_history_outside_country_add_history_outside_country_component__WEBPACK_IMPORTED_MODULE_30__["AddHistoryOutsideCountryComponent"],
                _components_employee_history_add_close_relative_add_close_relative_component__WEBPACK_IMPORTED_MODULE_31__["AddCloseRelativeComponent"],
                _components_employee_history_add_three_reference_details_add_three_reference_details_component__WEBPACK_IMPORTED_MODULE_32__["AddThreeReferenceDetailsComponent"],
                _components_employee_history_add_other_skills_add_other_skills_component__WEBPACK_IMPORTED_MODULE_33__["AddOtherSkillsComponent"],
                _components_employee_history_add_salary_budget_add_salary_budget_component__WEBPACK_IMPORTED_MODULE_34__["AddSalaryBudgetComponent"],
                _components_employee_history_add_language_add_language_component__WEBPACK_IMPORTED_MODULE_35__["AddLanguageComponent"]
            ]
        })
    ], HrModule);
    return HrModule;
}());



/***/ }),

/***/ "./src/app/hr/services/attendance.service.ts":
/*!***************************************************!*\
  !*** ./src/app/hr/services/attendance.service.ts ***!
  \***************************************************/
/*! exports provided: AttendanceService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AttendanceService", function() { return AttendanceService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var AttendanceService = /** @class */ (function () {
    function AttendanceService(globalService, appurl) {
        this.globalService = globalService;
        this.appurl = appurl;
    }
    //#region "getAttendancelist"
    AttendanceService.prototype.getAttendanceList = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Attendance_GetFilteredAttendanceDetails, model);
    };
    AttendanceService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"]])
    ], AttendanceService);
    return AttendanceService;
}());



/***/ }),

/***/ "./src/app/hr/services/employee-history.service.ts":
/*!*********************************************************!*\
  !*** ./src/app/hr/services/employee-history.service.ts ***!
  \*********************************************************/
/*! exports provided: EmployeeHistoryService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeHistoryService", function() { return EmployeeHistoryService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var EmployeeHistoryService = /** @class */ (function () {
    function EmployeeHistoryService(globalService, appurl, http, dialog) {
        this.globalService = globalService;
        this.appurl = appurl;
        this.http = http;
        this.dialog = dialog;
    }
    //#region "GetCurrencyList"
    EmployeeHistoryService.prototype.GetCurrencyList = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_code_GetAllCurrency);
    };
    //#endregion
    //#region "GetCurrencyList"
    EmployeeHistoryService.prototype.GetLanguageList = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_EmployeeHR_GetLanguageList);
    };
    //#endregion
    //#region "getHistoricalLogList"
    EmployeeHistoryService.prototype.getHistoricalLogList = function (EmployeeId) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr_GetAllEmployeeHistoryByEmployeeId +
            '?EmployeeId=' +
            EmployeeId);
    };
    //#endregion
    //#region "getEducationDetailList"
    EmployeeHistoryService.prototype.getEducationDetailList = function (EmployeeId) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_EmployeeDetail_GetAllEmployeeEducations +
            '?EmployeeId=' +
            EmployeeId);
    };
    //#endregion
    //#region "getEmployeeHistoryOfOutsideCountryDetailList"
    EmployeeHistoryService.prototype.getEmployeeHistoryOfOutsideCountryDetailList = function (EmployeeId) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_EmployeeDetail_GetAllEmployeeHistoryOutsideCountry +
            '?EmployeeId=' +
            EmployeeId);
    };
    //#endregion
    //#region "getEmployeeCloseRelativeList"
    EmployeeHistoryService.prototype.getEmployeeCloseRelativeList = function (EmployeeId) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_EmployeeDetail_GetAllEmployeeRelativeInformation +
            '?EmployeeId=' +
            EmployeeId);
    };
    //#endregion
    //#region "getEmployeeThreeReferenceDetailList"
    EmployeeHistoryService.prototype.getEmployeeThreeReferenceDetailList = function (EmployeeId) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_EmployeeDetail_GetAllEmployeeInfoReferences +
            '?EmployeeId=' +
            EmployeeId);
    };
    //#endregion
    //#region "getEmployeeOtherSkillDetailList"
    EmployeeHistoryService.prototype.getEmployeeOtherSkillDetailList = function (EmployeeId) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_EmployeeDetail_GetAllEmployeeOtherSkills +
            '?EmployeeId=' +
            EmployeeId);
    };
    //#endregion
    //#region "getEmployeeSalarybudgetDetailList"
    EmployeeHistoryService.prototype.getEmployeeSalaryBudgetDetailList = function (EmployeeId) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_EmployeeDetail_GetAllEmployeeSalaryBudgets +
            '?EmployeeId=' +
            EmployeeId);
    };
    //#endregion
    //#region "getEmployeeLanguageDetailList"
    EmployeeHistoryService.prototype.getEmployeeLanguageDetailList = function (EmployeeId) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_EmployeeDetail_GetAllEmployeeLanguages +
            '?EmployeeId=' +
            EmployeeId);
    };
    //#endregion
    //#region "addHistoricalLogDetail"
    EmployeeHistoryService.prototype.addHistoricalLogDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr_AddEmployeeHistoryDetail, model);
    };
    //#endregion
    //#region "addEducationDetail"
    EmployeeHistoryService.prototype.addEducationDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr_AddEmployeeEducations, model);
    };
    //#endregion
    //#region "addHistoryOutsideCountry"
    EmployeeHistoryService.prototype.addHistoryOutsideCountry = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr_AddEmployeeHistoryOutsideCountry, model);
    };
    //#endregion
    //#region "addCloseRelativeDetail"
    EmployeeHistoryService.prototype.addCloseRelativeDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr_AddEmployeeRelativeInformation, model);
    };
    //#endregion
    //#region "addThreeReferenceDetail"
    EmployeeHistoryService.prototype.addThreeReferenceDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr_AddEmployeeInfoReferences, model);
    };
    //#endregion
    //#region "addOtherSkillDetail"
    EmployeeHistoryService.prototype.addOtherSkillDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr_AddEmployeeOtherSkills, model);
    };
    //#endregion
    //#region "addSalaryBudgetDetail"
    EmployeeHistoryService.prototype.addSalaryBudgetDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr_AddEmployeeSalaryBudgets, model);
    };
    //#endregion
    //#region "addLanguageDetail"
    EmployeeHistoryService.prototype.addLanguageDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr_AddEmployeeLanguages, model);
    };
    //#endregion
    //#region "deleteHistoricalLog"
    EmployeeHistoryService.prototype.deleteHistoricalLog = function (HistoryId) {
        return this.globalService.deleteById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr__DeleteEmployeeHistoryDetail +
            '?HistoryId=' +
            HistoryId);
    };
    //#endregion
    //#region "deleteEducation"
    EmployeeHistoryService.prototype.deleteEducation = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr__DeleteEmployeeEducations, model);
    };
    //#endregion
    //#region "deleteEmployeeHistoryOutsideCountry"
    EmployeeHistoryService.prototype.deleteEmployeeHistoryOutsideCountry = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr__DeleteEmployeeHistoryOutsideCountry, model);
    };
    //#endregion
    //#region "deleteCloseRelativeDetail"
    EmployeeHistoryService.prototype.deleteCloseRelativeDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr__DeleteEmployeeRelativeInformation, model);
    };
    //#endregion
    //#region "deleteEmployeeReferenceInfoDetail"
    EmployeeHistoryService.prototype.deleteEmployeeReferenceInfoDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr__DeleteEmployeeInfoReferences, model);
    };
    //#endregion
    //#region "deleteEmployeeOtherSkillDetail"
    EmployeeHistoryService.prototype.deleteEmployeeOtherSkillDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr__DeleteEmployeeOtherSkills, model);
    };
    //#endregion
    //#region "deleteEmployeeSalaryBudgetDetail"
    EmployeeHistoryService.prototype.deleteEmployeeSalaryBudgetDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr__DeleteEmployeeSalaryBudgets, model);
    };
    //#endregion
    //#region "deleteEmployeeOtherSkillDetail"
    EmployeeHistoryService.prototype.deleteEmployeeLanguageDetail = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Hr__RemoveEmployeeLanguages, model);
    };
    EmployeeHistoryService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"],
            _angular_material__WEBPACK_IMPORTED_MODULE_4__["MatDialog"]])
    ], EmployeeHistoryService);
    return EmployeeHistoryService;
}());



/***/ }),

/***/ "./src/app/hr/services/employee-list.service.ts":
/*!******************************************************!*\
  !*** ./src/app/hr/services/employee-list.service.ts ***!
  \******************************************************/
/*! exports provided: EmployeeListService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeeListService", function() { return EmployeeListService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var EmployeeListService = /** @class */ (function () {
    function EmployeeListService(globalService, appurl, dialog) {
        this.globalService = globalService;
        this.appurl = appurl;
        this.dialog = dialog;
    }
    EmployeeListService.prototype.GetAllOfficeCodeList = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_code_GetAllOffice);
    };
    EmployeeListService.prototype.getAllEmployeeList = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_EmployeeDetail_GetAllEmployeeDetailList, model);
    };
    EmployeeListService.prototype.deleteMurtipleEmployeesById = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_EmployeeDetail_DeleteMurtipleEmployeesById, model);
    };
    EmployeeListService.prototype.addResignation = function (EmployeeID) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_EmployeeDetail_AddEmployeeResignation, EmployeeID);
    };
    EmployeeListService.prototype.saveResignation = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_EmployeeDetail_SaveEmployeeResignation, model);
    };
    EmployeeListService.prototype.getResignationDetailById = function (id) {
        return this.globalService.getItemById(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_EmployeeDetail_GetEmployeeResignationById, id);
    };
    EmployeeListService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_3__["MatDialog"]])
    ], EmployeeListService);
    return EmployeeListService;
}());



/***/ }),

/***/ "./src/app/hr/services/employee-pension.service.ts":
/*!*********************************************************!*\
  !*** ./src/app/hr/services/employee-pension.service.ts ***!
  \*********************************************************/
/*! exports provided: EmployeePensionService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmployeePensionService", function() { return EmployeePensionService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var EmployeePensionService = /** @class */ (function () {
    function EmployeePensionService(globalService, appurl, http, dialog) {
        this.globalService = globalService;
        this.appurl = appurl;
        this.http = http;
        this.dialog = dialog;
    }
    //#region "GetCurrencyList"
    EmployeePensionService.prototype.GetCurrencyList = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_code_GetAllCurrency);
    };
    //#endregion
    //#region "GetFinancialYearList"
    EmployeePensionService.prototype.GetFinancialYearList = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_5__["GLOBAL"].API_Code_GetAllFinancialYearDetail);
    };
    EmployeePensionService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"],
            _angular_material__WEBPACK_IMPORTED_MODULE_4__["MatDialog"]])
    ], EmployeePensionService);
    return EmployeePensionService;
}());



/***/ }),

/***/ "./src/app/hr/services/hr-control-panel.service.ts":
/*!*********************************************************!*\
  !*** ./src/app/hr/services/hr-control-panel.service.ts ***!
  \*********************************************************/
/*! exports provided: HrControlPanelService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HrControlPanelService", function() { return HrControlPanelService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var HrControlPanelService = /** @class */ (function () {
    function HrControlPanelService(globalService, appurl, http, dialog) {
        this.globalService = globalService;
        this.appurl = appurl;
        this.http = http;
        this.dialog = dialog;
    }
    //#region "getEmployeeDetail"
    HrControlPanelService.prototype.getEmployeeDetail = function (employeeid) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_EmployeeDetail_GetEmployeeDetailById, employeeid);
    };
    HrControlPanelService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"],
            _angular_material__WEBPACK_IMPORTED_MODULE_5__["MatDialog"]])
    ], HrControlPanelService);
    return HrControlPanelService;
}());



/***/ }),

/***/ "./src/app/hr/services/hr-leave.service.ts":
/*!*************************************************!*\
  !*** ./src/app/hr/services/hr-leave.service.ts ***!
  \*************************************************/
/*! exports provided: HrLeaveService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HrLeaveService", function() { return HrLeaveService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var HrLeaveService = /** @class */ (function () {
    function HrLeaveService(globalService, appurl, http, dialog) {
        this.globalService = globalService;
        this.appurl = appurl;
        this.http = http;
        this.dialog = dialog;
    }
    //#region "getEmployeeBalanceLeave"
    HrLeaveService.prototype.getEmployeeBalanceLeave = function (employeeid) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HR_GetAllEmployeeAssignLeave + '?EmployeeId=' +
            employeeid);
    };
    //#endregion
    //#region "addEmployeeLeave"
    HrLeaveService.prototype.addEmployeeLeave = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HR_AddEmployeeLeave, model);
    };
    //#endregion
    //#region "getFinancialYearList"
    HrLeaveService.prototype.getFinancialYearList = function () {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_GetCurrentFinancialYear);
    };
    //#endregion
    //#region "getAllLeaveReasonList"
    HrLeaveService.prototype.getAllLeaveReasonType = function () {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_LeaveReasonType);
    };
    //#endregion
    //#region "assignLeave"
    HrLeaveService.prototype.assignLeave = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HR_AssignLeaveToEmployeeDetail, model);
    };
    //#endregion
    //#region "getAllLeaveInfoById"
    HrLeaveService.prototype.getAllLeaveInfoById = function (employeeid) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HR_GetEmployeeAppliedLeaves + '?id=' + employeeid);
    };
    //#endregion
    //#region "approveRejectLeave"
    HrLeaveService.prototype.approveRejectLeave = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_HR_ApproveRejectLeave, model);
    };
    HrLeaveService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"],
            _angular_material__WEBPACK_IMPORTED_MODULE_5__["MatDialog"]])
    ], HrLeaveService);
    return HrLeaveService;
}());



/***/ })

}]);
//# sourceMappingURL=hr-hr-module.js.map