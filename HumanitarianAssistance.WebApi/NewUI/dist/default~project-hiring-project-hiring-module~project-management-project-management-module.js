(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["default~project-hiring-project-hiring-module~project-management-project-management-module"],{

/***/ "./node_modules/rxjs-compat/_esm5/add/operator/pairwise.js":
/*!*****************************************************************!*\
  !*** ./node_modules/rxjs-compat/_esm5/add/operator/pairwise.js ***!
  \*****************************************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _operator_pairwise__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../operator/pairwise */ "./node_modules/rxjs-compat/_esm5/operator/pairwise.js");


rxjs__WEBPACK_IMPORTED_MODULE_0__["Observable"].prototype.pairwise = _operator_pairwise__WEBPACK_IMPORTED_MODULE_1__["pairwise"];
//# sourceMappingURL=pairwise.js.map

/***/ }),

/***/ "./node_modules/rxjs-compat/_esm5/operator/pairwise.js":
/*!*************************************************************!*\
  !*** ./node_modules/rxjs-compat/_esm5/operator/pairwise.js ***!
  \*************************************************************/
/*! exports provided: pairwise */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "pairwise", function() { return pairwise; });
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");

function pairwise() {
    return Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_0__["pairwise"])()(this);
}
//# sourceMappingURL=pairwise.js.map

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.html":
/*!********************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.html ***!
  \********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Employee Analytical Info Breakdown\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n    <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n  </button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"addAnalyticalInfoForm\" (ngSubmit)=\"onFormSubmit(addAnalyticalInfoForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <span>Please submit the employee analytical info breakdown for this existing employee</span><br /><br />\r\n                    <div class=\"row\">\r\n                        <div class=\"col-sm-12\">\r\n                            <div class=\"row\" formArrayName=\"EditAnalyticalInfo\">\r\n                                <div *ngFor=\"\r\n                                let Item of addAnalyticalInfoForm.get(\r\n                                  'EditAnalyticalInfo'\r\n                                )['controls'];\r\n                                let i = index\r\n                              \">\r\n                                    <div [formGroupName]=\"i\">\r\n                                        <div class=\"col-lg-3 col-sm-3\">\r\n                                            <lib-hum-dropdown formControlName=\"ProjectId\" [options]=\"projectList$\" [placeHolder]=\"'Project'\" [disabled]=\"true\"></lib-hum-dropdown>\r\n                                        </div>\r\n                                        <div class=\"col-lg-3 col-sm-3\">\r\n                                            <lib-hum-dropdown formControlName=\"BudgetlineId\" [options]=\"allBudgetLineList$\" [placeHolder]=\"'Budget Line'\" [disabled]=\"true\"></lib-hum-dropdown>\r\n                                        </div>\r\n                                        <div class=\"col-lg-3 col-sm-3\">\r\n                                            <lib-hum-dropdown formControlName=\"AccountCode\" [options]=\"accountList$\" [placeHolder]=\"'Account'\"></lib-hum-dropdown>\r\n                                        </div>\r\n                                        <div class=\"col-lg-3 col-sm-3\">\r\n                                            <mat-form-field class=\"example-full-width\">\r\n                                                <input matInput formControlName=\"SalaryPercentage\" placeholder=\"Percentage\" type=\"number\" />\r\n                                            </mat-form-field>\r\n                                        </div>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                            <!-- <app-candidate-table [headers]=\"analyticalInfoHeaders$\" [isDefaultAction]=\"false\" [items]=\"analyticalInfoList$\" ></app-candidate-table> -->\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <span>Entry for this hiring request selection</span><br /><br />\r\n            <div class=\"col-sm-12\">\r\n                <div class=\"row\">\r\n                    <div class=\"col-lg-3 col-sm-3\">\r\n                        <lib-hum-dropdown formControlName=\"ProjectId\" [validation]=\"\r\n                        addAnalyticalInfoForm.controls['ProjectId'].hasError(\r\n            'required'\r\n          )\r\n        \" [options]=\"projectList$\" [placeHolder]=\"'Project'\" [disabled]=\"true\"></lib-hum-dropdown>\r\n                    </div>\r\n                    <div class=\"col-lg-3 col-sm-3\">\r\n                        <lib-hum-dropdown formControlName=\"BudgetlineId\" [validation]=\"\r\n                      addAnalyticalInfoForm.controls['BudgetlineId'].hasError(\r\n          'required'\r\n        )\r\n      \" [options]=\"budgetLineList$\" [placeHolder]=\"'Budget Line'\" [disabled]=\"true\"></lib-hum-dropdown>\r\n                    </div>\r\n                    <div class=\"col-lg-3 col-sm-3\">\r\n                        <lib-hum-dropdown formControlName=\"AccountCode\" [validation]=\"\r\n                          addAnalyticalInfoForm.controls['AccountCode'].hasError(\r\n              'required'\r\n            )\r\n          \" [options]=\"accountList$\" [placeHolder]=\"'Account'\"></lib-hum-dropdown>\r\n                    </div>\r\n                    <div class=\"col-lg-3 col-sm-3\">\r\n                        <mat-form-field class=\"example-full-width\">\r\n                            <input matInput formControlName=\"SalaryPercentage\" placeholder=\"Percentage\" type=\"number\" />\r\n                        </mat-form-field>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div mat-dialog-actions class=\"pull-right\">\r\n            <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n            <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'Submit'\" [isSubmit]=\"true\"></hum-button>\r\n            <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n        </div>\r\n    </form>\r\n</div>"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.scss":
/*!********************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.scss ***!
  \********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9wcm9qZWN0LW1hbmFnZW1lbnQvcHJvamVjdC1oaXJpbmcvYWRkLWFuYWx5dGljYWwtaW5mby9hZGQtYW5hbHl0aWNhbC1pbmZvLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.ts":
/*!******************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.ts ***!
  \******************************************************************************************************************/
/*! exports provided: AddAnalyticalInfoComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddAnalyticalInfoComponent", function() { return AddAnalyticalInfoComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
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








var AddAnalyticalInfoComponent = /** @class */ (function () {
    function AddAnalyticalInfoComponent(dialogRef, data, commonLoader, hiringRequestService, toastr, fb) {
        this.dialogRef = dialogRef;
        this.data = data;
        this.commonLoader = commonLoader;
        this.hiringRequestService = hiringRequestService;
        this.toastr = toastr;
        this.fb = fb;
        this.isFormSubmitted = false;
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_1__["ReplaySubject"](1);
        this.onAddAnalyticalInfoRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.analyticalInfoHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])([
            'Project',
            'Budget Line',
            'Account',
            'Percentage'
        ]);
        //#region "Initialize candidate form"
        this.addAnalyticalInfoForm = this.fb.group({
            EmployeeID: [null],
            ProjectId: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_6__["Validators"].required]],
            BudgetlineId: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_6__["Validators"].required]],
            AccountCode: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_6__["Validators"].required]],
            SalaryPercentage: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_6__["Validators"].required]],
            EditAnalyticalInfo: this.fb.array([])
        });
        //#endregion
    }
    Object.defineProperty(AddAnalyticalInfoComponent.prototype, "formData", {
        get: function () { return this.addAnalyticalInfoForm.get('EditAnalyticalInfo'); },
        enumerable: true,
        configurable: true
    });
    AddAnalyticalInfoComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.projectId = this.data.projectId;
        this.hiringRequestId = this.data.hiringRequestId;
        this.employeeId = this.data.employeeId;
        this.budgetLineId = this.data.budgetLineId;
        this.addAnalyticalInfoForm.controls['EmployeeID'].setValue(this.employeeId);
        Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["forkJoin"])([
            this.getProjectList(),
            this.getBudgetLineList(),
            this.getAllBudgetLineList(),
            this.getAccountList(),
            this.getAnalyticalInfo()
        ])
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_7__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeProjectList(result[0]);
            _this.subscribeBudgetLineList(result[1]);
            _this.subscribeAllBudgetLineList(result[2]);
            _this.subscribeAccountList(result[3]);
            _this.subscribeAnalyticalInfo(result[4]);
        });
    };
    //#region "Get all budget line list"
    AddAnalyticalInfoComponent.prototype.getBudgetLineList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetBudgetLineList(this.projectId);
    };
    AddAnalyticalInfoComponent.prototype.subscribeBudgetLineList = function (response) {
        this.commonLoader.hideLoader();
        this.budgetLineList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.data.map(function (y) {
            return {
                value: y.BudgetLineId,
                name: y.BudgetCodeName
            };
        }));
    };
    //#endregion
    //#region "Get all budget line list"
    AddAnalyticalInfoComponent.prototype.getAllBudgetLineList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetAllBudgetLineList();
    };
    AddAnalyticalInfoComponent.prototype.subscribeAllBudgetLineList = function (response) {
        this.commonLoader.hideLoader();
        this.allBudgetLineList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.data.map(function (y) {
            return {
                value: y.BudgetLineId,
                name: y.BudgetCodeName
            };
        }));
    };
    //#endregion
    //#region "Get all budget line list"
    AddAnalyticalInfoComponent.prototype.getProjectList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetProjectList();
    };
    AddAnalyticalInfoComponent.prototype.subscribeProjectList = function (response) {
        this.commonLoader.hideLoader();
        this.projectList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.data.map(function (y) {
            return {
                value: y.ProjectId,
                name: y.ProjectName
            };
        }));
    };
    //#endregion
    //#region "Get all budget line list"
    AddAnalyticalInfoComponent.prototype.getAccountList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetAccountList();
    };
    AddAnalyticalInfoComponent.prototype.subscribeAccountList = function (response) {
        this.commonLoader.hideLoader();
        this.accountList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.data.map(function (y) {
            return {
                value: y.AccountCode,
                name: y.AccountName
            };
        }));
    };
    //#endregion
    AddAnalyticalInfoComponent.prototype.getAnalyticalInfo = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetAnalyticalInfoByEmployeeId(this.employeeId);
    };
    AddAnalyticalInfoComponent.prototype.subscribeAnalyticalInfo = function (response) {
        var _this = this;
        this.commonLoader.hideLoader();
        if (response.data !== undefined) {
            var control_1 = this.addAnalyticalInfoForm.controls.EditAnalyticalInfo;
            this.analyticalInfoList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.data.map(function (y) {
                control_1.push(_this.fb.group({
                    EmployeeSalaryAnalyticalInfoId: y.EmployeeSalaryAnalyticalInfoId,
                    ProjectId: y.ProjectId,
                    BudgetlineId: y.BudgetLineId,
                    AccountCode: y.AccountCode,
                    SalaryPercentage: y.SalaryPercentage
                }));
                return {
                    Project: y.ProjectId,
                    Budgetline: y.BudgetLineName,
                    Account: y.AccountCode,
                    Percentage: y.SalaryPercentage
                };
            }));
        }
        this.addAnalyticalInfoForm.patchValue({
            ProjectId: this.projectId,
            BudgetlineId: this.budgetLineId
        });
    };
    //#region "Refresh candidate status after adding analytical"
    AddAnalyticalInfoComponent.prototype.AddAnalyticalInfoRefresh = function () {
        this.onAddAnalyticalInfoRefresh.emit();
    };
    // #endregion
    //#region "on cancel popup"
    AddAnalyticalInfoComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    //#region "On form submission"
    AddAnalyticalInfoComponent.prototype.onFormSubmit = function (data) {
        var _this = this;
        var allowedPercentage = 0;
        if (data.EditAnalyticalInfo.length > 0) {
            data.EditAnalyticalInfo.forEach(function (element) {
                allowedPercentage = allowedPercentage + (+element.SalaryPercentage);
            });
        }
        if (this.addAnalyticalInfoForm.valid) {
            if (data.SalaryPercentage + allowedPercentage === 100) {
                this.isFormSubmitted = true;
                this.hiringRequestService.AddAnalyticalInfo(data).subscribe(function (response) {
                    if (response.statusCode === 200) {
                        _this.toastr.success('Analytical info added successfully');
                        _this.AddAnalyticalInfoRefresh();
                        _this.isFormSubmitted = false;
                    }
                    else {
                        _this.toastr.error(response.message);
                        _this.isFormSubmitted = false;
                    }
                    _this.onCancelPopup();
                }, function () {
                    _this.toastr.error('Someting went wrong. Please try again');
                    _this.isFormSubmitted = false;
                });
            }
            else {
                this.toastr.warning('Not Allowed');
            }
        }
    };
    AddAnalyticalInfoComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-analytical-info',
            template: __webpack_require__(/*! ./add-analytical-info.component.html */ "./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.html"),
            styles: [__webpack_require__(/*! ./add-analytical-info.component.scss */ "./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.scss")]
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"], Object, src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__["CommonLoaderService"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_5__["HiringRequestsService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_6__["FormBuilder"]])
    ], AddAnalyticalInfoComponent);
    return AddAnalyticalInfoComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.html":
/*!******************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.html ***!
  \******************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        Hiring Request : {{hiringRequestCode}}\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n      <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n    </button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"addHiringRequestForm\" (ngSubmit)=\"onFormSubmit()\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <h4> General </h4>\r\n                    <div class=\"row\">\r\n                        <!-- General-->\r\n\r\n                        <div class=\"col-lg-12 col-sm-12\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <textarea matInput formControlName=\"SpecificDutiesAndResponsibilities\" placeholder=\"Specific Duties And Responsibilities\" matTextareaAutosize matAutosizeMinRows=\"5\" matAutosizeMaxRows=\"7\"></textarea>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-12 col-sm-12\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <textarea matInput formControlName=\"SubmissionGuidelines\" placeholder=\"Submission Guidelines\" matTextareaAutosize matAutosizeMinRows=\"5\" matAutosizeMaxRows=\"7\"></textarea>\r\n                            </mat-form-field>\r\n                        </div>\r\n\r\n                        <div class=\"col-lg-12 col-sm-12\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <textarea matInput formControlName=\"Background\" placeholder=\"Background\" matTextareaAutosize matAutosizeMinRows=\"5\" matAutosizeMaxRows=\"7\"></textarea>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"Nationality\" [validation]=\"\r\n              addHiringRequestForm.controls['Nationality'].hasError('required')\r\n            \" [options]=\"countryList$\" [placeHolder]=\"'Nationality'\" (change)=\"onChangeNationality($event)\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"ProvinceId\" [validation]=\"\r\n            addHiringRequestForm.controls['ProvinceId'].hasError('required')\r\n          \" [options]=\"provinceList$\" [placeHolder]=\"'Province'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n\r\n\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"Office\" [validation]=\"\r\n                  addHiringRequestForm.controls['Office'].hasError('required')\r\n                \" [options]=\"officeList$\" [placeHolder]=\"'Duty Station'\" [disabled]=\"!isEditable\" (change)=\"onChangeDutyStation($event)\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"Position\" [validation]=\"\r\n                    addHiringRequestForm.controls['Position'].hasError('required')\r\n                  \" [options]=\"designationList$\" [placeHolder]=\"'Position'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"BudgetLine\" [validation]=\"\r\n                    addHiringRequestForm.controls['BudgetLine'].hasError('required')\r\n                  \" [options]=\"budgetLineList$\" [placeHolder]=\"'Budget Line'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput type=\"number\" min=\"1\" max=\"AvailableVacancies\" formControlName=\"TotalVacancy\" placeholder=\"Total Vacancy\" />\r\n                                <mat-error *ngIf=\"addHiringRequestForm.controls['TotalVacancy'].hasError()\">\r\n                                </mat-error>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput [matDatepicker]=\"AnouncingDatePicker\" placeholder=\"Anouncing Date\" formControlName=\"AnouncingDate\" />\r\n                                <mat-datepicker-toggle matSuffix [for]=\"AnouncingDatePicker\"></mat-datepicker-toggle>\r\n                                <mat-datepicker #AnouncingDatePicker></mat-datepicker>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput [matDatepicker]=\"ClosingDatePicker\" placeholder=\"Closing Date\" formControlName=\"ClosingDate\" />\r\n                                <mat-datepicker-toggle matSuffix [for]=\"ClosingDatePicker\"></mat-datepicker-toggle>\r\n                                <mat-datepicker #ClosingDatePicker></mat-datepicker>\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                    <br>\r\n                    <mat-divider></mat-divider>\r\n                    <!-- Contract Details -->\r\n                    <h4> Contract Details </h4>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"ContractType\" [validation]=\"\r\n                              addHiringRequestForm.controls['ContractType'].hasError('required')\r\n                            \" [options]=\"contractTypeList$\" [placeHolder]=\"'Contract Type'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"ContractDuration\" placeholder=\"Contract Duration (In Months)\" type=\"number\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"JobGrade\" [validation]=\"\r\n                  addHiringRequestForm.controls['JobGrade'].hasError('required')\r\n                \" [options]=\"jobGradeList$\" [placeHolder]=\"'Job Grade'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"JobShift\" [validation]=\"\r\n                        addHiringRequestForm.controls['JobShift'].hasError('required')\r\n                      \" [options]=\"jobShiftList$\" [placeHolder]=\"'Shift'\"></lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"JobType\" [validation]=\"\r\n                addHiringRequestForm.controls['JobType'].hasError('required')\r\n              \" [options]=\"jobTypeList$\" [placeHolder]=\"'Job Type'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"JobCategory\" [validation]=\"\r\n              addHiringRequestForm.controls['JobCategory'].hasError('required')\r\n            \" [options]=\"departmentList$\" [placeHolder]=\"'Job Category'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"PayCurrency\" [validation]=\"\r\n                  addHiringRequestForm.controls['PayCurrency'].hasError('required')\r\n                \" [options]=\"currencyList$\" [placeHolder]=\"'Pay Currency'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"PayHourlyRate\" placeholder=\"Pay Hourly Rate\" type=\"number\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                    <br>\r\n                    <mat-divider></mat-divider>\r\n                    <!-- Required Qualification-->\r\n                    <h4> Required Qualification </h4>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-12 col-sm-12\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <textarea matInput formControlName=\"KnowledgeAndSkillsRequired\" placeholder=\"Knowledge And Skills Required\" matTextareaAutosize matAutosizeMinRows=\"5\" matAutosizeMaxRows=\"7\"></textarea>\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"EducationDegree\" [validation]=\"addHiringRequestForm.controls['EducationDegree'].hasError('required')\r\n                \" [options]=\"educationDegreeList$\" [placeHolder]=\"'Education Degree'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <lib-hum-dropdown formControlName=\"Profession\" [validation]=\"\r\n                    addHiringRequestForm.controls['Profession'].hasError('required')\r\n                  \" [options]=\"professionList$\" [placeHolder]=\"'Profession'\" [disabled]=\"!isEditable\">\r\n                            </lib-hum-dropdown>\r\n                        </div>\r\n                        <div class=\"col-lg-6 col-sm-6\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Experience\" placeholder=\"Experience  (In Years)\" type=\"number\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div mat-dialog-actions class=\"pull-right\">\r\n            <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n            <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\" [disabled]='!addHiringRequestForm.dirty'></hum-button>\r\n            <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n        </div>\r\n    </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.scss":
/*!******************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.scss ***!
  \******************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9wcm9qZWN0LW1hbmFnZW1lbnQvcHJvamVjdC1oaXJpbmcvYWRkLWhpcmluZy1yZXF1ZXN0L2FkZC1oaXJpbmctcmVxdWVzdC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.ts":
/*!****************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.ts ***!
  \****************************************************************************************************************/
/*! exports provided: AddHiringRequestComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddHiringRequestComponent", function() { return AddHiringRequestComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
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









var AddHiringRequestComponent = /** @class */ (function () {
    function AddHiringRequestComponent(dialogRef, data, commonLoader, hiringRequestService, toastr, fb, loader) {
        this.dialogRef = dialogRef;
        this.data = data;
        this.commonLoader = commonLoader;
        this.hiringRequestService = hiringRequestService;
        this.toastr = toastr;
        this.fb = fb;
        this.loader = loader;
        this.isEditable = true;
        this.isFormSubmitted = false;
        this.onAddHiringRequestListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.onUpdateHiringRequestListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_3__["ReplaySubject"](1);
        this.addHiringRequestForm = this.fb.group({
            ProjectId: [null],
            HiringRequestId: [null],
            HiringRequestCode: [null],
            TotalVacancy: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Position: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Office: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            BudgetLine: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            ContractType: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            ContractDuration: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            JobGrade: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            AnouncingDate: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            ClosingDate: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            JobType: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            JobCategory: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            PayCurrency: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            PayHourlyRate: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            JobShift: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Experience: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            EducationDegree: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Profession: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            SpecificDutiesAndResponsibilities: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            KnowledgeAndSkillsRequired: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            SubmissionGuidelines: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Background: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Nationality: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            ProvinceId: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]]
        });
        this.jobShiftList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])([
            { name: 'Day', value: 1 },
            { name: 'Night', value: 2 }
        ]);
        this.jobTypeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])([
            { name: 'Part-time', value: 1 },
            { name: 'Full-time', value: 2 }
        ]);
        this.contractTypeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])([
            { name: 'Probationary', value: 1 },
            { name: 'Part-time', value: 2 },
            { name: 'Permanent/Full-time', value: 3 }
        ]);
    }
    AddHiringRequestComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.projectId = this.data.projectId;
        this.hiringRequestId = this.data.hiringRequestId;
        this.addHiringRequestForm.controls['ProjectId'].setValue(this.projectId);
        this.addHiringRequestForm.controls['HiringRequestId'].setValue(this.hiringRequestId);
        if (this.data.hiringRequestId !== 0) {
            this.isEditable = false;
            this.getAllHiringRequestDetail();
        }
        else {
            this.getHiringRequestCode();
        }
        Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["forkJoin"])([
            this.getAllOfficeList(),
            this.getAllCountryList(),
            this.getDesignationList(),
            this.getBudgetLineList(),
            this.getJobGradeList(),
            this.getCurrencyList(),
            this.getEducationDegreeList(),
            this.getAllProfessionList()
        ])
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeOfficeList(result[0]);
            _this.subscribeCountryList(result[1]);
            _this.subscribeDesignationList(result[2]);
            _this.subscribeBudgetLineList(result[3]);
            _this.subscribeJobGradeList(result[4]);
            _this.subscribeCurrencyList(result[5]);
            _this.subscribeEducationDegreeList(result[6]);
            _this.subscribeProfessionList(result[7]);
        });
    };
    //#region "Get all office List"
    AddHiringRequestComponent.prototype.getAllOfficeList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetOfficeList();
    };
    AddHiringRequestComponent.prototype.subscribeOfficeList = function (response) {
        this.commonLoader.hideLoader();
        this.officeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.map(function (y) {
            return {
                value: y.OfficeId,
                name: y.OfficeName
            };
        }));
    };
    //#endregion
    //#region "Get all country List"
    AddHiringRequestComponent.prototype.getAllCountryList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetCountryList();
    };
    AddHiringRequestComponent.prototype.subscribeCountryList = function (response) {
        this.commonLoader.hideLoader();
        this.countryList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.map(function (y) {
            return {
                value: y.CountryId,
                name: y.CountryName
            };
        }));
    };
    //#endregion
    //#region "Get all designation List"
    AddHiringRequestComponent.prototype.getDesignationList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.getDesignationList();
    };
    AddHiringRequestComponent.prototype.subscribeDesignationList = function (response) {
        this.commonLoader.hideLoader();
        this.designationList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.map(function (y) {
            return {
                value: y.DesignationId,
                name: y.Designation
            };
        }));
    };
    //#endregion
    //#region "Get all budget line list"
    AddHiringRequestComponent.prototype.getBudgetLineList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetBudgetLineList(this.projectId);
    };
    AddHiringRequestComponent.prototype.subscribeBudgetLineList = function (response) {
        this.commonLoader.hideLoader();
        this.budgetLineList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.map(function (y) {
            return {
                value: y.BudgetLineId,
                name: y.BudgetCodeName
            };
        }));
    };
    //#endregion
    //#region "Get all job grade list"
    AddHiringRequestComponent.prototype.getJobGradeList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetJobGradeList();
    };
    AddHiringRequestComponent.prototype.subscribeJobGradeList = function (response) {
        this.commonLoader.hideLoader();
        this.jobGradeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.map(function (y) {
            return {
                value: y.GradeId,
                name: y.GradeName
            };
        }));
    };
    //#endregion
    //#region "Get all currency list"
    AddHiringRequestComponent.prototype.getCurrencyList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetCurrencyList();
    };
    AddHiringRequestComponent.prototype.subscribeCurrencyList = function (response) {
        this.commonLoader.hideLoader();
        this.currencyList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.map(function (y) {
            return {
                value: y.CurrencyId,
                name: y.CurrencyName
            };
        }));
    };
    //#endregion
    //#region "Get all education degree list"
    AddHiringRequestComponent.prototype.getEducationDegreeList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetEducationDegreeList();
    };
    AddHiringRequestComponent.prototype.subscribeEducationDegreeList = function (response) {
        this.commonLoader.hideLoader();
        this.educationDegreeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.map(function (y) {
            return {
                value: y.Id,
                name: y.Name
            };
        }));
    };
    //#endregion
    //#region "Get all profession list"
    AddHiringRequestComponent.prototype.getAllProfessionList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetProfessionList();
    };
    AddHiringRequestComponent.prototype.subscribeProfessionList = function (response) {
        this.commonLoader.hideLoader();
        this.professionList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.map(function (y) {
            return {
                value: y.ProfessionId,
                name: y.ProfessionName
            };
        }));
    };
    //#endregion
    AddHiringRequestComponent.prototype.onChangeNationality = function (CountryId) {
        var _this = this;
        this.hiringRequestService
            .getAllProvinceListByCountryId([CountryId])
            .subscribe(function (x) {
            _this.provinceList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (element) {
                return {
                    value: element.ProvinceId,
                    name: element.ProvinceName
                };
            }));
        });
    };
    //#region "Get Department List"
    AddHiringRequestComponent.prototype.getDepartmentList = function (officeId) {
        var _this = this;
        this.hiringRequestService.getDepartmentList(officeId).subscribe(function (x) {
            _this.departmentList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.Departments.map(function (element) {
                return {
                    value: element.DepartmentId,
                    name: element.DepartmentName
                };
            }));
        });
    };
    //#endregion
    //#region "Get hiring request code"
    AddHiringRequestComponent.prototype.getHiringRequestCode = function () {
        var _this = this;
        this.commonLoader.hideLoader();
        this.hiringRequestService
            .GetHiringRequestCode(this.projectId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            if (response.statusCode === 200 && response.data !== null) {
                _this.hiringRequestCode = response.data;
                _this.addHiringRequestForm.controls['HiringRequestCode'].setValue(_this.hiringRequestCode);
            }
        });
    };
    //#endregion
    //#region "Get Hiring request details for edit"
    AddHiringRequestComponent.prototype.getAllHiringRequestDetail = function () {
        var _this = this;
        this.hiringRequestService
            .GetAllProjectHiringRequestDetailByHiringRequestId(this.data.hiringRequestId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.loader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                _this.onChangeNationality(response.data.Country);
                _this.hiringRequestCode = response.data.HiringRequestCode;
                _this.addHiringRequestForm.setValue({
                    HiringRequestId: response.data.HiringRequestId,
                    HiringRequestCode: response.data.HiringRequestCode,
                    ProjectId: response.data.ProjectId,
                    TotalVacancy: response.data.TotalVacancy,
                    Position: response.data.Position,
                    Office: response.data.Office,
                    ContractType: response.data.ContractType,
                    ContractDuration: response.data.ContractDuration,
                    AnouncingDate: response.data.AnouncingDate,
                    ClosingDate: response.data.ClosingDate,
                    JobType: response.data.JobType,
                    JobCategory: response.data.JobCategory,
                    JobShift: response.data.JobShift,
                    Experience: response.data.Experience,
                    BudgetLine: response.data.BudgetLine,
                    EducationDegree: response.data.EducationDegree,
                    JobGrade: response.data.JobGrade,
                    PayCurrency: response.data.PayCurrency,
                    PayHourlyRate: response.data.PayHourlyRate,
                    Profession: response.data.Profession,
                    SpecificDutiesAndResponsibilities: response.data.SpecificDutiesAndResponsibilities,
                    KnowledgeAndSkillsRequired: response.data.KnowledgeAndSkillsRequired,
                    SubmissionGuidelines: response.data.SubmissionGuidelines,
                    Background: response.data.Background,
                    ProvinceId: response.data.Province,
                    Nationality: response.data.Country
                });
                _this.addHiringRequestForm.controls['ContractDuration'].disable();
                _this.addHiringRequestForm.controls['ContractType'].disable();
                _this.addHiringRequestForm.controls['TotalVacancy'].disable();
                _this.getDepartmentList(response.data.Office);
            }
            _this.loader.hideLoader();
        }, function () {
            _this.loader.hideLoader();
        });
    };
    //#endregion
    //#region "Add hiring request"
    AddHiringRequestComponent.prototype.AddHiringRequest = function (data) {
        var _this = this;
        data.AnouncingDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_8__["StaticUtilities"].getLocalDate(data.AnouncingDate);
        data.ClosingDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_8__["StaticUtilities"].getLocalDate(data.ClosingDate);
        this.isFormSubmitted = true;
        this.hiringRequestService.AddHiringRequestDetail(data).subscribe(function (response) {
            if (response.statusCode === 200) {
                _this.toastr.success('New request is created successfully');
                _this.AddHiringRequestListRefresh();
                _this.isFormSubmitted = false;
            }
            else {
                _this.toastr.error(response.message);
                _this.isFormSubmitted = false;
            }
            _this.onCancelPopup();
        }, function () {
            _this.toastr.error('Someting went wrong. Please try again');
            _this.isFormSubmitted = false;
        });
    };
    //#endregion
    //#region "Edit hiring request"
    AddHiringRequestComponent.prototype.EditHiringRequest = function (data) {
        var _this = this;
        this.addHiringRequestForm.value.ClosingDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_8__["StaticUtilities"].getLocalDate(this.addHiringRequestForm.value.ClosingDate);
        this.addHiringRequestForm.value.AnouncingDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_8__["StaticUtilities"].getLocalDate(this.addHiringRequestForm.value.AnouncingDate);
        this.isFormSubmitted = true;
        this.hiringRequestService.EditHiringRequestDetail(data).subscribe(function (response) {
            if (response.statusCode === 200) {
                _this.toastr.success('Hiring request updated successfully');
                _this.UpdateHiringRequestListRefresh();
                _this.isFormSubmitted = false;
            }
            else {
                _this.toastr.error(response.message);
                _this.isFormSubmitted = false;
            }
            _this.onCancelPopup();
        }, function () {
            _this.toastr.error('Someting went wrong. Please try again');
            _this.isFormSubmitted = false;
        });
    };
    //#endregion
    //#region "On hiring request list refresh"
    AddHiringRequestComponent.prototype.AddHiringRequestListRefresh = function () {
        this.onAddHiringRequestListRefresh.emit();
    };
    AddHiringRequestComponent.prototype.UpdateHiringRequestListRefresh = function () {
        this.onUpdateHiringRequestListRefresh.emit();
    };
    //#endregion
    //#region "On change duty station"
    AddHiringRequestComponent.prototype.onChangeDutyStation = function (e) {
        this.OfficeId = e;
        this.getDepartmentList(e);
    };
    //#endregion
    //#region "On form submission"
    AddHiringRequestComponent.prototype.onFormSubmit = function () {
        if (this.addHiringRequestForm.valid) {
            if (this.hiringRequestId === 0) {
                this.AddHiringRequest(this.addHiringRequestForm.getRawValue());
            }
            else {
                this.EditHiringRequest(this.addHiringRequestForm.getRawValue());
            }
        }
    };
    //#endregion
    //#region "onCancelPopup"
    AddHiringRequestComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddHiringRequestComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-hiring-request',
            template: __webpack_require__(/*! ./add-hiring-request.component.html */ "./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.html"),
            styles: [__webpack_require__(/*! ./add-hiring-request.component.scss */ "./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.scss")]
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object, src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_7__["HiringRequestsService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_6__["ToastrService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"]])
    ], AddHiringRequestComponent);
    return AddHiringRequestComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.html":
/*!****************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.html ***!
  \****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n        New Candidate Form\r\n        <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n      <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n    </button>\r\n    </h1>\r\n    <form class=\"example-form\" [formGroup]=\"addNewCandidateForm\" (ngSubmit)=\"onFormSubmit(addNewCandidateForm.value)\">\r\n        <div mat-dialog-content>\r\n            <div class=\"row\">\r\n                <span class=\"formHeading\">Identity Details</span><br />\r\n                <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"FirstName\" placeholder=\"First Name\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"LastName\" placeholder=\"Last Name\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"PhoneNumber\" placeholder=\"Phone Number\" minlength=\"10\" maxlength=\"14\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Email\" placeholder=\"Email\" />\r\n                            </mat-form-field>\r\n                        </div>\r\n                        <div class=\"col-lg-4 col-sm-4\">\r\n                            <mat-form-field class=\"example-full-width\">\r\n                                <input matInput formControlName=\"Password\" placeholder=\"Password (Auto Generated)\" [(ngModel)]=\"autoGenratedPassword\" readonly/>\r\n                            </mat-form-field>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <span class=\"formHeading\">Personal Details</span><br />\r\n                        <div class=\"col-sm-12\">\r\n                            <div class=\"row\">\r\n                                <div class=\"col-lg-4 col-sm-4\">\r\n                                    <lib-hum-dropdown formControlName=\"Gender\" [validation]=\"\r\n                      addNewCandidateForm.controls['Gender'].hasError(\r\n                        'required'\r\n                      )\r\n                    \" [options]=\"genderList$\" [placeHolder]=\"'Gender'\"></lib-hum-dropdown>\r\n                                </div>\r\n                                <div class=\"col-lg-4 col-sm-4\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <input matInput [matDatepicker]=\"DateOfBirthPicker\" placeholder=\"Date Of Birth\" formControlName=\"DateOfBirth\" />\r\n                                        <mat-datepicker-toggle matSuffix [for]=\"DateOfBirthPicker\"></mat-datepicker-toggle>\r\n                                        <mat-datepicker #DateOfBirthPicker></mat-datepicker>\r\n                                    </mat-form-field>\r\n                                </div>\r\n                                <div class=\"col-lg-4 col-sm-4\">\r\n                                    <lib-hum-dropdown formControlName=\"Country\" [validation]=\"\r\n                      addNewCandidateForm.controls['Country'].hasError(\r\n                        'required'\r\n                      )\r\n                    \" [options]=\"countryList$\" [placeHolder]=\"'Country'\" (change)=\"onChangeCountry($event)\"></lib-hum-dropdown>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-lg-4 col-sm-4\">\r\n                                    <lib-hum-dropdown formControlName=\"Province\" [validation]=\"\r\n                      addNewCandidateForm.controls['Province'].hasError(\r\n                        'required'\r\n                      )\r\n                    \" [options]=\"provinceList$\" [placeHolder]=\"'Province/State'\" (change)=\"onChangeProvince($event)\"></lib-hum-dropdown>\r\n                                </div>\r\n                                <div class=\"col-lg-4 col-sm-4\">\r\n                                    <lib-hum-dropdown formControlName=\"District\" [validation]=\"\r\n                      addNewCandidateForm.controls['District'].hasError(\r\n                        'required'\r\n                      )\r\n                    \" [options]=\"districtList$\" [placeHolder]=\"'City/Village/District'\"></lib-hum-dropdown>\r\n                                </div>\r\n                                <div class=\"col-lg-4 col-sm-4\">\r\n                                    <lib-hum-dropdown formControlName=\"ExperienceYear\" [validation]=\"\r\n                      addNewCandidateForm.controls['ExperienceYear'].hasError(\r\n                        'required'\r\n                      )\r\n                    \" [options]=\"PreviousYearsList$\" [placeHolder]=\"'Experience Year'\"></lib-hum-dropdown>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-lg-4 col-sm-4\">\r\n                                    <lib-hum-dropdown formControlName=\"ExperienceMonth\" [validation]=\"\r\n                      addNewCandidateForm.controls['ExperienceMonth'].hasError(\r\n                        'required'\r\n                      )\r\n                    \" [options]=\"MonthsList$\" [placeHolder]=\"'Experience Month'\"></lib-hum-dropdown>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-lg-12 col-sm-12\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <textarea matInput rows=\"2\" formControlName=\"PreviousWork\" placeholder=\"Previous Work\"></textarea>\r\n                                    </mat-form-field>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-lg-12 col-sm-12\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <textarea matInput rows=\"2\" formControlName=\"CurrentAddress\" placeholder=\"Current Address\"></textarea>\r\n                                    </mat-form-field>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-lg-12 col-sm-12\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <textarea matInput rows=\"2\" formControlName=\"PermanentAddress\" placeholder=\"Permanent Address\"></textarea>\r\n                                    </mat-form-field>\r\n                                </div>\r\n                            </div>\r\n\r\n                            <div class=\"row\">\r\n                                <div class=\"col-lg-12 col-sm-12\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <textarea matInput rows=\"2\" formControlName=\"Remarks\" placeholder=\"Remarks/Description\"></textarea>\r\n                                    </mat-form-field>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <span class=\"formHeading\">Qualifications\r\n                <hum-button [type]=\"'attachment'\" [text]=\"'ATTACH CV'\" (click)='openInput()'></hum-button>\r\n                <input id=\"fileInput\" style=\"display:none\" hidden type=\"file\" (change)=\"fileChange($event.target.files)\" name=\"file\">\r\n            </span><br />\r\n                        <div class=\"col-sm-12\">\r\n                            <h5 *ngIf=\"this.attachmentCV.length === 0\"><i class=\"fa fa-exclamation-triangle fa-1\" style=\"color: red;\"></i> CV File required. Please Attach.</h5>\r\n                            <h5 *ngFor=\"let uploadedItem of this.attachmentCV\">{{uploadedItem[0].name}}</h5>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-lg-6 col-sm-6\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <input matInput type=\"number\" formControlName=\"RelevantExperienceInYear\" placeholder=\"Current Relevant Experience(in Years)\" />\r\n                                    </mat-form-field>\r\n                                </div>\r\n                                <div class=\"col-lg-6 col-sm-6\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <input matInput type=\"number\" formControlName=\"IrrelevantExperienceInYear\" placeholder=\"Current Irrelevant Experience(in Years)\" />\r\n                                    </mat-form-field>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row\">\r\n                                <div class=\"col-lg-6 col-sm-6\">\r\n                                    <lib-hum-dropdown formControlName=\"EducationDegree\" [validation]=\"\r\n                    addNewCandidateForm.controls['EducationDegree'].hasError(\r\n                      'required'\r\n                    )\r\n                  \" [options]=\"educationDegreeList$\" [placeHolder]=\"'Education'\"></lib-hum-dropdown>\r\n                                </div>\r\n                                <div class=\"col-lg-6 col-sm-6\">\r\n                                    <lib-hum-dropdown formControlName=\"Profession\" [validation]=\"\r\n                    addNewCandidateForm.controls['Profession'].hasError(\r\n                      'required'\r\n                    )\r\n                  \" [options]=\"professionList$\" [placeHolder]=\"'Profession'\"></lib-hum-dropdown>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div mat-dialog-actions class=\"pull-right\">\r\n            <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n            <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n            <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n        </div>\r\n    </form>\r\n</div>"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.scss":
/*!****************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.scss ***!
  \****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".formHeading {\n  padding-left: 1em;\n  font-size: 14px;\n  font-weight: 500; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL3Byb2plY3QtbWFuYWdlbWVudC9wcm9qZWN0LWhpcmluZy9hZGQtbmV3LWNhbmRpZGF0ZS9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxkYXNoYm9hcmRcXHByb2plY3QtbWFuYWdlbWVudFxccHJvamVjdC1oaXJpbmdcXGFkZC1uZXctY2FuZGlkYXRlXFxhZGQtbmV3LWNhbmRpZGF0ZS5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNFLGlCQUFpQjtFQUNqQixlQUFlO0VBQ2YsZ0JBQWdCLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9kYXNoYm9hcmQvcHJvamVjdC1tYW5hZ2VtZW50L3Byb2plY3QtaGlyaW5nL2FkZC1uZXctY2FuZGlkYXRlL2FkZC1uZXctY2FuZGlkYXRlLmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsiLmZvcm1IZWFkaW5nIHtcclxuICBwYWRkaW5nLWxlZnQ6IDFlbTtcclxuICBmb250LXNpemU6IDE0cHg7XHJcbiAgZm9udC13ZWlnaHQ6IDUwMDtcclxufVxyXG4iXX0= */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.ts":
/*!**************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.ts ***!
  \**************************************************************************************************************/
/*! exports provided: AddNewCandidateComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddNewCandidateComponent", function() { return AddNewCandidateComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var src_app_store_services_purchase_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/store/services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
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











var AddNewCandidateComponent = /** @class */ (function () {
    function AddNewCandidateComponent(dialogRef, data, commonLoader, hiringRequestService, toastr, fb, purchaseService, globalSharedService) {
        this.dialogRef = dialogRef;
        this.data = data;
        this.commonLoader = commonLoader;
        this.hiringRequestService = hiringRequestService;
        this.toastr = toastr;
        this.fb = fb;
        this.purchaseService = purchaseService;
        this.globalSharedService = globalSharedService;
        this.isFormSubmitted = false;
        this.onAddCandidateListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.attachmentCV = [];
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_6__["ReplaySubject"](1);
        this.accountStatusList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])([
            { name: 'Prospective', value: 1 },
            { name: 'Active', value: 2 }
        ]);
        this.genderList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])([
            { name: 'Male', value: 1 },
            { name: 'Female', value: 2 },
            { name: 'Other', value: 3 }
        ]);
    }
    AddNewCandidateComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.initCadidateForm();
        this.getAllMonthList();
        this.getPreviousYearsList();
        this.projectId = this.data.projectId;
        this.hiringRequestId = this.data.hiringRequestId;
        this.addNewCandidateForm.controls['ProjectId'].setValue(this.projectId);
        this.addNewCandidateForm.controls['HiringRequestId'].setValue(this.hiringRequestId);
        Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["forkJoin"])([
            this.getAllCountryList(),
            this.getAllJobGradeList(),
            this.getAllProfessionList(),
            this.getAllEducationDegreeList()
        ])
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_7__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeCountryList(result[0]);
            _this.subscribeGradeList(result[1]);
            _this.subscribeProfessionList(result[2]);
            _this.subscribeEducationDegreeList(result[3]);
        });
    };
    //#region "Initialize candidate form"
    AddNewCandidateComponent.prototype.initCadidateForm = function () {
        this.PasswordAutoGenrate();
        this.addNewCandidateForm = this.fb.group({
            ProjectId: [null],
            HiringRequestId: [null],
            FirstName: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            LastName: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            Email: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].email]],
            Password: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            PhoneNumber: [
                null,
                [
                    _angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required,
                    _angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].pattern(/^-?(0|[1-9]\d*)?$/),
                    _angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].minLength(10),
                    _angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].maxLength(14)
                ]
            ],
            EducationDegree: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            Gender: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            DateOfBirth: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            Country: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            Province: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            District: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            ExperienceYear: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            ExperienceMonth: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            PreviousWork: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            CurrentAddress: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            PermanentAddress: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            Profession: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            RelevantExperienceInYear: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            IrrelevantExperienceInYear: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]],
            Remarks: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required]]
        });
    };
    //#endregion
    //#region "Auto genrate password for new candidate"
    AddNewCandidateComponent.prototype.PasswordAutoGenrate = function () {
        this.autoGenratedPassword = Math.random()
            .toString(36)
            .slice(-8);
    };
    //#endregion
    //#region "Get all month list for ExperienceInMonth dropdown"
    AddNewCandidateComponent.prototype.getAllMonthList = function () {
        var monthDropDown = [];
        for (var i = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_9__["Month"]['January']; i <= src_app_shared_enum__WEBPACK_IMPORTED_MODULE_9__["Month"]['December']; i++) {
            monthDropDown.push({ name: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_9__["Month"][i], value: i });
        }
        this.MonthsList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(monthDropDown);
    };
    //#endregion
    //#region "Get all previous years list for ExperienceInYears dropdown"
    AddNewCandidateComponent.prototype.getPreviousYearsList = function () {
        this.PreviousYearsList$ = this.purchaseService.getPreviousYearsList(40);
    };
    //#endregion
    //#region "Get all countries list for country dropdown"
    AddNewCandidateComponent.prototype.getAllCountryList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetCountryList();
    };
    AddNewCandidateComponent.prototype.subscribeCountryList = function (response) {
        this.commonLoader.hideLoader();
        this.countryList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(response.data.map(function (y) {
            return {
                value: y.CountryId,
                name: y.CountryName
            };
        }));
    };
    //#endregion
    //#region "Get all job grades list for job grade dropdown"
    AddNewCandidateComponent.prototype.getAllJobGradeList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetJobGradeList();
    };
    AddNewCandidateComponent.prototype.subscribeGradeList = function (response) {
        this.commonLoader.hideLoader();
        this.gradeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(response.data.map(function (y) {
            return {
                value: y.GradeId,
                name: y.GradeName
            };
        }));
    };
    //#endregion
    //#region "Get all profession list for profession dropdown"
    AddNewCandidateComponent.prototype.getAllProfessionList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetProfessionList();
    };
    AddNewCandidateComponent.prototype.subscribeProfessionList = function (response) {
        this.commonLoader.hideLoader();
        this.professionList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(response.data.map(function (y) {
            return {
                value: y.ProfessionId,
                name: y.ProfessionName
            };
        }));
    };
    //#endregion
    //#region "Get all education degree list for education dropdown"
    AddNewCandidateComponent.prototype.getAllEducationDegreeList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetEducationDegreeList();
    };
    AddNewCandidateComponent.prototype.subscribeEducationDegreeList = function (response) {
        this.commonLoader.hideLoader();
        this.educationDegreeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(response.map(function (y) {
            return {
                value: y.Id,
                name: y.Name
            };
        }));
    };
    //#endregion
    //#region "Get all province list on selection of country dropdown"
    AddNewCandidateComponent.prototype.getAllProvinceList = function (CountryId) {
        var _this = this;
        this.hiringRequestService
            .getAllProvinceListByCountryId([CountryId])
            .subscribe(function (response) {
            _this.commonLoader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                _this.provinceList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(response.data.map(function (element) {
                    return {
                        value: element.ProvinceId,
                        name: element.ProvinceName
                    };
                }));
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    //#endregion
    //#region "Get all district list on selection of province dropdown"
    AddNewCandidateComponent.prototype.getAllDistrictList = function (ProvinceId) {
        var _this = this;
        this.hiringRequestService
            .GetAllDistrictvalueByProvinceId([ProvinceId])
            .subscribe(function (response) {
            _this.commonLoader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                _this.districtList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(response.data.map(function (element) {
                    return {
                        value: element.DistrictID,
                        name: element.District
                    };
                }));
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    //#endregion
    //#region "Adding new candidate"
    AddNewCandidateComponent.prototype.AddNewCandidate = function (data, attachmentCV) {
        var _this = this;
        this.isFormSubmitted = true;
        this.hiringRequestService.AddNewCandidateDetail(data).subscribe(function (response) {
            // response.CommonId.Id is CandidateId
            if (response.StatusCode === 200 && response.CommonId.Id != null) {
                _this.globalSharedService
                    .uploadFile(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_9__["FileSourceEntityTypes"].HiringRequestCandidateCV, response.CommonId.Id, attachmentCV)
                    .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_7__["takeUntil"])(_this.destroyed$))
                    .subscribe(function (y) {
                    _this.toastr.success('New Candidate added successfully');
                    _this.isFormSubmitted = false;
                    _this.AddCandidateListRefresh();
                });
            }
            else {
                _this.toastr.error(response.message);
                _this.isFormSubmitted = false;
            }
            _this.onCancelPopup();
        }, function (error) {
            _this.toastr.error('Someting went wrong. Please try again');
            _this.isFormSubmitted = false;
        });
    };
    //#endregion
    //#region "Cv upload fucntionality"
    AddNewCandidateComponent.prototype.openInput = function () {
        document.getElementById('fileInput').click();
    };
    AddNewCandidateComponent.prototype.fileChange = function (file) {
        this.attachmentCV = [];
        this.attachmentCV.push(file);
    };
    //#endregion
    //#region "Refresh candidate list after adding new candidate"
    AddNewCandidateComponent.prototype.AddCandidateListRefresh = function () {
        this.onAddCandidateListRefresh.emit();
    };
    // #endregion
    //#region "On change country selection"
    AddNewCandidateComponent.prototype.onChangeCountry = function (e) {
        this.provinceList$ = null;
        this.districtList$ = null;
        this.getAllProvinceList(e);
    };
    //#endregion
    //#region "On change province selection"
    AddNewCandidateComponent.prototype.onChangeProvince = function (e) {
        this.districtList$ = null;
        this.getAllDistrictList(e);
    };
    //#endregion
    //#region "On form submission"
    AddNewCandidateComponent.prototype.onFormSubmit = function (data) {
        if (this.attachmentCV.length === 0) {
            this.toastr.warning('Please attach CV!');
            return;
        }
        if (this.addNewCandidateForm.valid) {
            this.AddNewCandidate(data, this.attachmentCV[0][0]);
        }
    };
    //#endregion
    //#region "on cancel popup"
    AddNewCandidateComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddNewCandidateComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-new-candidate',
            template: __webpack_require__(/*! ./add-new-candidate.component.html */ "./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.html"),
            styles: [__webpack_require__(/*! ./add-new-candidate.component.scss */ "./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.scss")]
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object, src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__["CommonLoaderService"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_3__["HiringRequestsService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_5__["FormBuilder"],
            src_app_store_services_purchase_service__WEBPACK_IMPORTED_MODULE_8__["PurchaseService"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_10__["GlobalSharedService"]])
    ], AddNewCandidateComponent);
    return AddNewCandidateComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/candidate-table/candidate-table.component.html":
/*!************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/candidate-table/candidate-table.component.html ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<table class=\"table table-striped\">\r\n    <thead>\r\n        <tr>\r\n            <th *ngFor=\"let header of headers | async\">\r\n                {{header}}\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody *ngFor=\"let item of items | async; let i=index;\">\r\n        <tr (click)=\"switchSubList(i,item)\" class=\"main-table\">\r\n            <td *ngFor=\"let header of itemHeaders | async; let j=index \">\r\n                <span [innerHtml]=\"((item[header] != null) ? item[header] : 'N/A')\"></span>\r\n                <!-- {{(item[header] != null) ? item[header] : \"N/A\"}} -->\r\n            </td>\r\n            <td *ngIf=\"itemActions && (itemActions.items)\">\r\n                <span class=\"action-span\" *ngIf=\"isDefaultAction\">\r\n          <hum-button *ngIf=\"itemActions.items?.button?.status\" [type]=\"'add'\" [text]=\"itemActions.items.button.text\"\r\n            (click)='action(item,\"button\",$event)'></hum-button>\r\n        </span>\r\n                <span class=\"action-span\" *ngIf=\"!isDefaultAction\">\r\n          <span *ngFor=\"let actionType of item.itemAction\">\r\n            <hum-button *ngIf=\"actionType.button?.status\" [type]=\"actionType.button?.type\"\r\n              [text]=\"actionType.button?.text\" (click)='action(item,actionType.button?.text,$event)'></hum-button>\r\n              &nbsp;&nbsp;&nbsp;&nbsp;<span *ngIf=\"actionType.anchor?.status\"><b>Employee:&nbsp;</b>\r\n              <a [type]=\"actionType.anchor?.type\"\r\n              [text]=\"actionType.anchor?.text\" (click)='action(item,actionType.anchor?.text,$event)'></a></span>\r\n                </span>\r\n                </span>\r\n                <span class=\"action-span\">\r\n          <i *ngIf=\"itemActions.items?.download\" class=\"fas fa-download\" (click)='action(item,\"download\",$event)'></i>\r\n        </span>\r\n                <span class=\"action-span\">\r\n          <i *ngIf=\"itemActions.items?.edit\" class=\"fa fa-pencil\" (click)='action(item,\"edit\",$event)'></i>\r\n        </span>\r\n                <span class=\"action-span\">\r\n          <i *ngIf=\"itemActions.items?.delete\" class=\"fas fa-trash\" (click)='action(item,\"delete\",$event)'></i>\r\n        </span>\r\n                <span class=\"action-span\">\r\n          <p *ngIf=\"itemActions.items?.link\" (click)='action(item,\"delete\",$event)'></p>\r\n        </span>\r\n            </td>\r\n\r\n        </tr>\r\n\r\n        <tr *ngIf=\"isShowSubList[i]\" class=\"sub-table\">\r\n            <td [attr.colspan]=\"(headers | async).length + 1\">\r\n                <div class=\"sub-table\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-md-12\">\r\n                            <div class=\"col-md-6\">\r\n                                <div class=\"col-md-12\">\r\n                                    <div class=\"col-md-6\">\r\n                                        Current Qualification\r\n                                    </div>\r\n                                </div>\r\n                                <div class=\"col-md-12\">\r\n\r\n                                    <div class=\"col-md-6\">\r\n                                        <b>Education</b>\r\n                                    </div>\r\n                                    <div class=\"col-md-6\"> <span [innerHtml]=\"((subItems[i][0].EducationDegree!= null) ? subItems[i][0].EducationDegree : 'N/A')\"></span></div>\r\n                                    <div class=\"col-md-6\">\r\n                                        <b>Profession</b>\r\n                                    </div>\r\n                                    <div class=\"col-md-6\"><span [innerHtml]=\"((subItems[i][0].Profession!= null) ? subItems[i][0].Profession : 'N/A')\"></span></div>\r\n                                    <div class=\"col-md-6\">\r\n                                        <b>Relevant Experience</b>\r\n                                    </div>\r\n                                    <div class=\"col-md-6\"><span [innerHtml]=\"((subItems[i][0].RelevantExperienceInYear!= null) ? subItems[i][0].RelevantExperienceInYear : 'N/A')\"></span></div>\r\n                                    <div class=\"col-md-6\">\r\n                                        <b>Irrelevant Experience</b>\r\n                                    </div>\r\n                                    <div class=\"col-md-6\"><span [innerHtml]=\"((subItems[i][0].IrrelevantExperienceInYear!= null) ? subItems[i][0].IrrelevantExperienceInYear : 'N/A')\"></span></div>\r\n                                    <div class=\"col-md-6\">\r\n                                        <b>Total Experience</b>\r\n                                    </div>\r\n                                    <div class=\"col-md-6\"><span [innerHtml]=\"((subItems[i][0].TotalExperienceInYear!= null) ? subItems[i][0].TotalExperienceInYear : 'N/A')\"></span></div>\r\n                                </div>\r\n\r\n\r\n                            </div>\r\n                            <div class=\"col-md-6\">\r\n                                <div class=\"col-md-12\">\r\n                                    <div class=\"col-md-6\">\r\n                                        Contact Details\r\n                                    </div>\r\n                                </div>\r\n                                <div class=\"col-md-12\">\r\n\r\n                                    <div class=\"col-md-6\"><b>Phone Number</b></div>\r\n                                    <div class=\"col-md-6\"><span [innerHtml]=\"((subItems[i][0].PhoneNumber!= null) ? subItems[i][0].PhoneNumber : 'N/A')\"></span></div>\r\n                                    <div class=\"col-md-6\"><b>Email Address</b></div>\r\n                                    <div class=\"col-md-6\"><span [innerHtml]=\"((subItems[i][0].Email!= null) ? subItems[i][0].Email : 'N/A')\"></span></div>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n            </td>\r\n        </tr>\r\n\r\n\r\n\r\n    </tbody>\r\n</table>"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/candidate-table/candidate-table.component.scss":
/*!************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/candidate-table/candidate-table.component.scss ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".sub-table {\n  background-color: #f9f9f9; }\n\n.main-table {\n  background-color: #ffff !important; }\n\n.sub-table .col-md-6 {\n  padding-bottom: 10px !important; }\n\n.sub-table h5 {\n  margin-top: 0% !important;\n  margin-bottom: 0% !important; }\n\n.action-span {\n  margin-left: 10px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL3Byb2plY3QtbWFuYWdlbWVudC9wcm9qZWN0LWhpcmluZy9jYW5kaWRhdGUtdGFibGUvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcZGFzaGJvYXJkXFxwcm9qZWN0LW1hbmFnZW1lbnRcXHByb2plY3QtaGlyaW5nXFxjYW5kaWRhdGUtdGFibGVcXGNhbmRpZGF0ZS10YWJsZS5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNJLHlCQUNKLEVBQUE7O0FBQ0E7RUFDSSxrQ0FBa0MsRUFBQTs7QUFFdEM7RUFDSSwrQkFBK0IsRUFBQTs7QUFFbkM7RUFDSSx5QkFBeUI7RUFDekIsNEJBQTRCLEVBQUE7O0FBRWhDO0VBQ0ksaUJBQWlCLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9kYXNoYm9hcmQvcHJvamVjdC1tYW5hZ2VtZW50L3Byb2plY3QtaGlyaW5nL2NhbmRpZGF0ZS10YWJsZS9jYW5kaWRhdGUtdGFibGUuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyIuc3ViLXRhYmxle1xyXG4gICAgYmFja2dyb3VuZC1jb2xvcjogI2Y5ZjlmOVxyXG59XHJcbi5tYWluLXRhYmxle1xyXG4gICAgYmFja2dyb3VuZC1jb2xvcjogI2ZmZmYgIWltcG9ydGFudDtcclxufVxyXG4uc3ViLXRhYmxlIC5jb2wtbWQtNntcclxuICAgIHBhZGRpbmctYm90dG9tOiAxMHB4ICFpbXBvcnRhbnQ7XHJcbn1cclxuLnN1Yi10YWJsZSBoNXtcclxuICAgIG1hcmdpbi10b3A6IDAlICFpbXBvcnRhbnQ7XHJcbiAgICBtYXJnaW4tYm90dG9tOiAwJSAhaW1wb3J0YW50O1xyXG59XHJcbi5hY3Rpb24tc3BhbntcclxuICAgIG1hcmdpbi1sZWZ0OiAxMHB4O1xyXG59Il19 */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/candidate-table/candidate-table.component.ts":
/*!**********************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/candidate-table/candidate-table.component.ts ***!
  \**********************************************************************************************************/
/*! exports provided: CandidateTableComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CandidateTableComponent", function() { return CandidateTableComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var CandidateTableComponent = /** @class */ (function () {
    function CandidateTableComponent() {
        this.isDefaultAction = true;
        this.actionClick = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.subActionClick = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.rowClick = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.subItems = [];
        this.itemAction = [];
        this.isShowSubList = [];
        this.actions = {
            items: {},
            subitems: {}
        };
    }
    CandidateTableComponent.prototype.ngOnInit = function () {
    };
    CandidateTableComponent.prototype.ngOnChanges = function () {
        var _this = this;
        this.itemActions = this.actions;
        if (this.items) {
            this.items.subscribe(function (res) {
                _this.subItems = [];
                if (res == null || res.length > 0) {
                    _this.itemHeaders = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(Object.keys(res[0]));
                    res.forEach(function (element, i) {
                        if (element['subItems']) {
                            _this.subItems.push(element['subItems']);
                            if (element['subItems'].length > 0) {
                                _this.subItemHeaders = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(Object.keys(element['subItems'][0]));
                            }
                        }
                        // only if default action is false
                        if (element['itemAction']) {
                            _this.itemAction.push(element['itemAction']);
                        }
                    });
                    if (_this.subItems.length > 0) {
                        _this.itemHeaders.subscribe(function (r) {
                            var index = r.findIndex(function (v) { return v === 'subItems'; });
                            r.splice(index);
                        });
                    }
                    // only if default action is false
                    if (_this.itemAction.length > 0) {
                        _this.itemHeaders.subscribe(function (r) {
                            var index = r.findIndex(function (v) { return v === 'itemAction'; });
                            r.splice(index);
                        });
                    }
                }
            });
        }
        if (this.hideColums$ && this.itemHeaders) {
            this.hideColums$.subscribe(function (res) {
                _this.itemHeaders.subscribe(function (headers) {
                    _this.itemHeaders = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(headers.filter(function (r) { return res.items.includes(r); }));
                });
                _this.headers.subscribe(function (headers) {
                    _this.headers = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(res.headers);
                });
            });
        }
    };
    CandidateTableComponent.prototype.action = function (item, type, e) {
        e.stopPropagation();
        var model = {
            item: item,
            type: type
        };
        this.actionClick.emit(model);
    };
    CandidateTableComponent.prototype.subAction = function (subItemEvent, itemEvent, type) {
        var model = {
            subItem: subItemEvent,
            item: itemEvent,
            type: type
        };
        this.subActionClick.emit(model);
    };
    CandidateTableComponent.prototype.ngAfterViewInit = function () {
        var _this = this;
        if (this.hideColums$ && this.itemHeaders) {
            this.hideColums$.subscribe(function (res) {
                _this.itemHeaders.subscribe(function (headers) {
                    _this.itemHeaders = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(headers.filter(function (r) { return res.items.includes(r); }));
                });
                _this.headers.subscribe(function (headers) {
                    _this.headers = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(res.headers);
                });
            });
        }
    };
    CandidateTableComponent.prototype.switchSubList = function (i, event) {
        if (this.subItems.length > 0)
            this.isShowSubList[i] = !this.isShowSubList[i];
        this.rowClick.emit(event);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", rxjs__WEBPACK_IMPORTED_MODULE_1__["Observable"])
    ], CandidateTableComponent.prototype, "headers", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", rxjs__WEBPACK_IMPORTED_MODULE_1__["Observable"])
    ], CandidateTableComponent.prototype, "subHeaders", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", rxjs__WEBPACK_IMPORTED_MODULE_1__["Observable"])
    ], CandidateTableComponent.prototype, "items", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", String)
    ], CandidateTableComponent.prototype, "subTitle", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], CandidateTableComponent.prototype, "actions", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], CandidateTableComponent.prototype, "isDefaultAction", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", rxjs__WEBPACK_IMPORTED_MODULE_1__["Observable"])
    ], CandidateTableComponent.prototype, "hideColums$", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], CandidateTableComponent.prototype, "actionClick", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], CandidateTableComponent.prototype, "subActionClick", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], CandidateTableComponent.prototype, "rowClick", void 0);
    CandidateTableComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-candidate-table',
            template: __webpack_require__(/*! ./candidate-table.component.html */ "./src/app/dashboard/project-management/project-hiring/candidate-table/candidate-table.component.html"),
            styles: [__webpack_require__(/*! ./candidate-table.component.scss */ "./src/app/dashboard/project-management/project-hiring/candidate-table/candidate-table.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], CandidateTableComponent);
    return CandidateTableComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.html":
/*!************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.html ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<router-outlet></router-outlet>"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.scss":
/*!************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.scss ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9wcm9qZWN0LW1hbmFnZW1lbnQvcHJvamVjdC1oaXJpbmcvZW50cnktY29tcG9uZW50L2VudHJ5LWNvbXBvbmVudC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.ts":
/*!**********************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.ts ***!
  \**********************************************************************************************************/
/*! exports provided: EntryComponentComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EntryComponentComponent", function() { return EntryComponentComponent; });
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

var EntryComponentComponent = /** @class */ (function () {
    function EntryComponentComponent() {
    }
    EntryComponentComponent.prototype.ngOnInit = function () {
    };
    EntryComponentComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-entry-component',
            template: __webpack_require__(/*! ./entry-component.component.html */ "./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.html"),
            styles: [__webpack_require__(/*! ./entry-component.component.scss */ "./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], EntryComponentComponent);
    return EntryComponentComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.html":
/*!************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.html ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template [headerClass]=\"'sub_header_template_main1'\">\r\n    <span class=\"action_header\">Hiring Requests\r\n    <hum-button [type]=\"'add'\" [text]=\"'ADD'\" (click)=\"addNewHiringRequest()\"></hum-button>\r\n    <hum-button *ngIf=\"filterModel.IsOpenFlagId == 1 && filterModel.IsInProgress == 2\" [type]=\"'save'\"\r\n      [text]=\"'complete request'\" (click)=\"onComplteRequest()\"></hum-button>\r\n    <hum-button *ngIf=\"filterModel.IsOpenFlagId == 1 && filterModel.IsInProgress == 2\" [type]=\"'cancel'\"\r\n      [text]=\"'close request'\" (click)=\"onCloseRequest()\"></hum-button>\r\n  </span>\r\n\r\n    <!-- <div class=\"action_section\">\r\n     <hum-button [type]=\"'add'\" [text]=\"'add job'\" [routerLink]=\"['../job-detail']\"></hum-button> \r\n     <hum-button [type]=\"'down'\" [text]=\"'jobs'\"></hum-button> (rowClick)=\"requestDetail($event)\"  \r\n  </div> -->\r\n</lib-sub-header-template>\r\n<div>\r\n    <mat-divider></mat-divider>\r\n    <mat-tab-group (selectedTabChange)=\"onTabClick($event)\">\r\n        <mat-tab label=\"Active\">\r\n        </mat-tab>\r\n        <mat-tab label=\"Archived\">\r\n        </mat-tab>\r\n    </mat-tab-group>\r\n    <div humAddScroll>\r\n        <table mat-table [dataSource]=\"dataSource\" class=\"mat-elevation-z8\">\r\n\r\n            <ng-container matColumnDef=\"select\">\r\n                <th mat-header-cell *matHeaderCellDef>\r\n                    <mat-checkbox *ngIf=\"selectCheckBoxFlag == false\" (change)=\"$event ? masterToggle() : null\" [checked]=\"selection.hasValue() && isAllSelected()\" [indeterminate]=\"selection.hasValue() && !isAllSelected()\" [aria-label]=\"checkboxLabel()\">\r\n                    </mat-checkbox>\r\n                </th>\r\n                <td mat-cell *matCellDef=\"let row\">\r\n                    <mat-checkbox *ngIf=\"selectCheckBoxFlag == false\" (click)=\"$event.stopPropagation()\" (change)=\"$event ? selection.toggle(row) : null\" [checked]=\"selection.isSelected(row)\" [aria-label]=\"checkboxLabel(row)\">\r\n                    </mat-checkbox>\r\n                </td>\r\n            </ng-container>\r\n\r\n            <ng-container matColumnDef=\"HiringRequestId\">\r\n                <th mat-header-cell *matHeaderCellDef> Hiring Request Id </th>\r\n                <td mat-cell *matCellDef=\"let element\" (click)=\"requestDetail(element)\"> {{element.HiringRequestId}} </td>\r\n            </ng-container>\r\n\r\n            <!-- <ng-container matColumnDef=\"JobCode\">\r\n      <th mat-header-cell *matHeaderCellDef> Job Code </th>\r\n      <td mat-cell *matCellDef=\"let element\" (click)=\"requestDetail(element)\"> {{element.JobCode}} </td>\r\n    </ng-container> -->\r\n\r\n            <ng-container matColumnDef=\"JobGrade\">\r\n                <th mat-header-cell *matHeaderCellDef> Job Grade </th>\r\n                <td mat-cell *matCellDef=\"let element\" (click)=\"requestDetail(element)\"> {{element.JobGrade}} </td>\r\n            </ng-container>\r\n\r\n            <ng-container matColumnDef=\"Position\">\r\n                <th mat-header-cell *matHeaderCellDef> Position </th>\r\n                <td mat-cell *matCellDef=\"let element\" (click)=\"requestDetail(element)\"> {{element.Position}} </td>\r\n            </ng-container>\r\n\r\n            <ng-container matColumnDef=\"TotalVacancies\">\r\n                <th mat-header-cell *matHeaderCellDef> Total Vacancies </th>\r\n                <td mat-cell *matCellDef=\"let element\" (click)=\"requestDetail(element)\"> {{element.TotalVacancies}} </td>\r\n            </ng-container>\r\n            <ng-container matColumnDef=\"FilledVacancies\">\r\n                <th mat-header-cell *matHeaderCellDef> Filled Vacancies </th>\r\n                <td mat-cell *matCellDef=\"let element\" (click)=\"requestDetail(element)\"> {{element.FilledVacancies}} </td>\r\n            </ng-container>\r\n\r\n            <ng-container matColumnDef=\"PayCurrency\">\r\n                <th mat-header-cell *matHeaderCellDef> Pay Currency </th>\r\n                <td mat-cell *matCellDef=\"let element\" (click)=\"requestDetail(element)\"> {{element.PayCurrency}} </td>\r\n            </ng-container>\r\n\r\n            <ng-container matColumnDef=\"PayRate\">\r\n                <th mat-header-cell *matHeaderCellDef> Pay Rate </th>\r\n                <td mat-cell *matCellDef=\"let element\" (click)=\"requestDetail(element)\"> {{element.PayRate}} </td>\r\n            </ng-container>\r\n\r\n            <ng-container matColumnDef=\"Status\">\r\n                <th mat-header-cell *matHeaderCellDef> Status </th>\r\n                <td mat-cell *matCellDef=\"let element\"> {{element.Status}} </td>\r\n            </ng-container>\r\n\r\n\r\n\r\n            <tr mat-header-row *matHeaderRowDef=\"displayHeaderColumns\"></tr>\r\n            <tr mat-row *matRowDef=\"let row; columns: displayHeaderColumns;\" (click)=\"selection.toggle(row)\">\r\n            </tr>\r\n        </table>\r\n        <mat-paginator [length]=\"filterModel.TotalCount\" [pageSize]=\"filterModel.pageSize\" [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n        </mat-paginator>\r\n    </div>\r\n</div>\r\n<!-- <div class=\"container\">\r\n  <div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n      <hum-table [headers]=\"hiringListHeaders$\" [items]=\"hiringList$\" (rowClick)=\"requestDetail($event)\"></hum-table>\r\n      <mat-paginator [length]=\"filterModel.TotalCount\" [pageSize]=\"filterModel.pageSize\"\r\n        [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n      </mat-paginator>\r\n    </div>\r\n  </div>\r\n</div> -->"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.scss":
/*!************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.scss ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".sub_header_template_main {\n  background-color: white;\n  height: 55px;\n  padding: 10px 15px;\n  box-shadow: none !important; }\n\n.body-content {\n  padding-top: 0px !important; }\n\n.mat-column-select {\n  overflow: initial; }\n\ntable {\n  width: 100%; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL3Byb2plY3QtbWFuYWdlbWVudC9wcm9qZWN0LWhpcmluZy9oaXJpbmctcmVxdWVzdHMvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcZGFzaGJvYXJkXFxwcm9qZWN0LW1hbmFnZW1lbnRcXHByb2plY3QtaGlyaW5nXFxoaXJpbmctcmVxdWVzdHNcXGhpcmluZy1yZXF1ZXN0cy5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNJLHVCQUF1QjtFQUN2QixZQUFZO0VBQ1osa0JBQWtCO0VBQ2xCLDJCQUEyQixFQUFBOztBQUUvQjtFQUNJLDJCQUEyQixFQUFBOztBQUUvQjtFQUNFLGlCQUFpQixFQUFBOztBQUVuQjtFQUNFLFdBQVcsRUFBQSIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9wcm9qZWN0LW1hbmFnZW1lbnQvcHJvamVjdC1oaXJpbmcvaGlyaW5nLXJlcXVlc3RzL2hpcmluZy1yZXF1ZXN0cy5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5zdWJfaGVhZGVyX3RlbXBsYXRlX21haW4ge1xyXG4gICAgYmFja2dyb3VuZC1jb2xvcjogd2hpdGU7XHJcbiAgICBoZWlnaHQ6IDU1cHg7XHJcbiAgICBwYWRkaW5nOiAxMHB4IDE1cHg7XHJcbiAgICBib3gtc2hhZG93OiBub25lICFpbXBvcnRhbnQ7XHJcbn1cclxuLmJvZHktY29udGVudCB7XHJcbiAgICBwYWRkaW5nLXRvcDogMHB4ICFpbXBvcnRhbnQ7XHJcbn1cclxuLm1hdC1jb2x1bW4tc2VsZWN0IHtcclxuICBvdmVyZmxvdzogaW5pdGlhbDtcclxufVxyXG50YWJsZSB7XHJcbiAgd2lkdGg6IDEwMCU7XHJcbn1cclxuIl19 */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.ts":
/*!**********************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.ts ***!
  \**********************************************************************************************************/
/*! exports provided: HiringRequestsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HiringRequestsComponent", function() { return HiringRequestsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _add_hiring_request_add_hiring_request_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../add-hiring-request/add-hiring-request.component */ "./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_cdk_collections__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/cdk/collections */ "./node_modules/@angular/cdk/esm5/collections.es5.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};









var HiringRequestsComponent = /** @class */ (function () {
    function HiringRequestsComponent(dialog, routeActive, route, hiringRequestService, loader, toastr) {
        this.dialog = dialog;
        this.routeActive = routeActive;
        this.route = route;
        this.hiringRequestService = hiringRequestService;
        this.loader = loader;
        this.toastr = toastr;
        this.hiringRequestList = [];
        this.selection = new _angular_cdk_collections__WEBPACK_IMPORTED_MODULE_6__["SelectionModel"](true, []);
        this.hiringRequestListLoader = false;
        this.selectCheckBoxFlag = false;
        this.displayHeaderColumns = [
            'select',
            'HiringRequestId',
            'JobGrade',
            'Position',
            'TotalVacancies',
            'FilledVacancies',
            'PayCurrency',
            'PayRate',
            'Status'
        ];
    }
    HiringRequestsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.filterModel = {
            FilterValue: '',
            pageIndex: 0,
            pageSize: 50,
            ProjectId: null,
            TotalCount: 0,
            IsInProgress: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["HiringRequestStatus"]['In-Progress'],
            IsOpenFlagId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["HiringRequestStatus"].Open
        };
        this.completeRequestModel = {
            HiringRequestId: [],
            ProjectId: this.projectId
        };
        this.routeActive.parent.parent.parent.params.subscribe(function (params) {
            _this.projectId = +params['id'];
        });
        this.getAllHiringRequestFilterList(this.filterModel);
    };
    //#region "Get all hiring request filter list"
    HiringRequestsComponent.prototype.getAllHiringRequestFilterList = function (filterModel) {
        var _this = this;
        filterModel.ProjectId = this.projectId;
        filterModel.TotalCount = 0;
        this.hiringRequestList = [];
        this.loader.showLoader();
        this.hiringRequestService
            .GetProjectHiringRequestFilterList(this.filterModel)
            .subscribe(function (response) {
            _this.loader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                _this.filterModel.TotalCount =
                    response.total != null ? response.total : 0;
                response.data.forEach(function (element) {
                    _this.hiringRequestList.push({
                        HiringRequestId: element.HiringRequestId,
                        JobGrade: element.JobGrade,
                        Position: element.Position,
                        TotalVacancies: element.TotalVacancies,
                        FilledVacancies: element.FilledVacancies,
                        PayCurrency: element.PayCurrency,
                        PayRate: element.PayRate,
                        Status: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["HiringRequestStatus"][element.HiringRequestStatus]
                    });
                });
                // to bind data in datasource in mat table
                _this.dataSource = new _angular_material__WEBPACK_IMPORTED_MODULE_5__["MatTableDataSource"](_this.hiringRequestList);
            }
            _this.loader.hideLoader();
        }, function (error) {
            _this.loader.hideLoader();
        });
    };
    //#endregion
    //#region "Get completed and closed hiring request on tab click"
    HiringRequestsComponent.prototype.onTabClick = function (event) {
        if (event.index === 0) {
            this.selectCheckBoxFlag = false;
            this.filterModel = {
                pageIndex: 0,
                pageSize: 50,
                IsOpenFlagId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["HiringRequestStatus"].Open,
                IsInProgress: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["HiringRequestStatus"]['In-Progress'],
                FilterValue: ''
            };
            this.getAllHiringRequestFilterList(this.filterModel);
        }
        else if (event.index === 1) {
            this.selectCheckBoxFlag = true;
            this.filterModel = {
                pageIndex: 0,
                pageSize: 50,
                IsOpenFlagId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["HiringRequestStatus"].Closed,
                IsInProgress: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["HiringRequestStatus"].Completed,
                FilterValue: ''
            };
            this.getAllHiringRequestFilterList(this.filterModel);
        }
    };
    //#endregion
    // #region Add new hiring reqeust"
    HiringRequestsComponent.prototype.addNewHiringRequest = function () {
        var _this = this;
        // NOTE: It open AddHiringRequest dialog and passed the data into the AddHiringRequestsComponent Model
        var dialogRef = this.dialog.open(_add_hiring_request_add_hiring_request_component__WEBPACK_IMPORTED_MODULE_4__["AddHiringRequestComponent"], {
            width: '700px',
            autoFocus: false,
            data: {
                hiringRequestId: 0,
                projectId: this.projectId
            }
        });
        dialogRef.componentInstance.onAddHiringRequestListRefresh.subscribe(function () {
            _this.getAllHiringRequestFilterList(_this.filterModel);
        });
        dialogRef.afterClosed().subscribe(function (result) { });
    };
    //#endregion
    //#region On complete hiring request"
    HiringRequestsComponent.prototype.onComplteRequest = function () {
        var _this = this;
        this.completeRequestModel = {
            HiringRequestId: [],
            ProjectId: this.projectId
        };
        if (this.selection.selected.length > 0 &&
            this.selection.selected.length !== undefined) {
            this.selection.selected.forEach(function (element) {
                _this.completeRequestModel.HiringRequestId.push(element.HiringRequestId);
            });
            this.hiringRequestService
                .IsCompltedeHrDetail(this.completeRequestModel)
                .subscribe(function (responseData) {
                if (responseData.statusCode === 200) {
                    _this.getAllHiringRequestFilterList(_this.filterModel);
                }
                else if (responseData.statusCode === 400) {
                    _this.toastr.error('Something went wrong .Please try again.');
                }
            }, function (error) { });
        }
    };
    //#endregion
    //#region "On close hiring request"
    HiringRequestsComponent.prototype.onCloseRequest = function () {
        var _this = this;
        this.completeRequestModel = {
            HiringRequestId: [],
            ProjectId: this.projectId
        };
        if (this.selection.selected.length > 0 &&
            this.selection.selected.length !== undefined) {
            this.selection.selected.forEach(function (element) {
                _this.completeRequestModel.HiringRequestId.push(element.HiringRequestId);
            });
            this.hiringRequestService
                .IsCloasedHrDetail(this.completeRequestModel)
                .subscribe(function (responseData) {
                if (responseData.statusCode === 200) {
                    _this.getAllHiringRequestFilterList(_this.filterModel);
                }
                else if (responseData.statusCode === 400) {
                    _this.toastr.error('Something went wrong .Please try again.');
                }
            }, function (error) { });
        }
    };
    //#endregion
    //#region "On filter applied"
    HiringRequestsComponent.prototype.onFilterApplied = function (filterModel) {
        this.getAllHiringRequestFilterList(filterModel);
    };
    //#endregion
    //#region  Paginator event
    HiringRequestsComponent.prototype.pageEvent = function (e) {
        this.filterModel.pageIndex = e.pageIndex;
        this.filterModel.pageSize = e.pageSize;
        this.onFilterApplied(this.filterModel);
    };
    //#endregion
    /** Whether the number of selected elements matches the total number of rows. */
    HiringRequestsComponent.prototype.isAllSelected = function () {
        var numSelected = this.selection.selected.length;
        var numRows = this.hiringRequestList.length;
        return numSelected === numRows;
    };
    /** Selects all rows if they are not all selected; otherwise clear selection. */
    HiringRequestsComponent.prototype.masterToggle = function () {
        var _this = this;
        this.isAllSelected()
            ? this.selection.clear()
            : this.hiringRequestList.forEach(function (row) { return _this.selection.select(row); });
    };
    /** The label for the checkbox on the passed row */
    HiringRequestsComponent.prototype.checkboxLabel = function (row) {
        if (!row) {
            return (this.isAllSelected() ? 'select' : 'deselect') + " all";
        }
        return (this.selection.isSelected(row) ? 'deselect' : 'select') + " row " + (row.HiringRequestId + 1);
    };
    //#region  Navigate to request details page
    HiringRequestsComponent.prototype.requestDetail = function (e) {
        this.route.navigate([e.HiringRequestId], {
            relativeTo: this.routeActive.parent
        });
    };
    HiringRequestsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-hiring-requests',
            template: __webpack_require__(/*! ./hiring-requests.component.html */ "./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.html"),
            styles: [__webpack_require__(/*! ./hiring-requests.component.scss */ "./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_5__["MatDialog"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_2__["HiringRequestsService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__["CommonLoaderService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_8__["ToastrService"]])
    ], HiringRequestsComponent);
    return HiringRequestsComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.html":
/*!*************************************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.html ***!
  \*************************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>\r\n    Add Interviewers\r\n    <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n      <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n    </button>\r\n  </h1>\r\n  <div mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-12\">\r\n          <mat-form-field>\r\n            <mat-select\r\n              placeholder=\"Select Interviewers\"\r\n              (selectionChange)=\"OnInterviewersSelection($event)\"\r\n              name=\"Employee\"\r\n              multiple\r\n            >\r\n              <mat-option *ngFor=\"let item of employeesList\" [value]=\"item.EmployeeId\"\r\n                >{{ item.EmployeeName }}\r\n              </mat-option>\r\n            </mat-select>\r\n          </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <div mat-dialog-actions class=\"pull-right\">\r\n    <hum-button [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\" (click)=\"OnFormSubmit()\"></hum-button>\r\n    <hum-button\r\n      (click)=\"onCancelPopup()\"\r\n      [type]=\"'cancel'\"\r\n      [text]=\"'cancel'\"\r\n    ></hum-button>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.scss":
/*!*************************************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.scss ***!
  \*************************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9wcm9qZWN0LW1hbmFnZW1lbnQvcHJvamVjdC1oaXJpbmcvaW50ZXJ2aWV3LWRldGFpbC9hZGQtbmV3LWludGVydmlld2VyL2FkZC1uZXctaW50ZXJ2aWV3ZXIuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.ts":
/*!***********************************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.ts ***!
  \***********************************************************************************************************************************/
/*! exports provided: AddNewInterviewerComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddNewInterviewerComponent", function() { return AddNewInterviewerComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var AddNewInterviewerComponent = /** @class */ (function () {
    function AddNewInterviewerComponent(commonLoader, hiringRequestService, toastr, routeActive, dialogRef) {
        this.commonLoader = commonLoader;
        this.hiringRequestService = hiringRequestService;
        this.toastr = toastr;
        this.routeActive = routeActive;
        this.dialogRef = dialogRef;
        this.employeesList = [];
        this.employeesListTwo = [];
    }
    AddNewInterviewerComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.routeActive.queryParams.subscribe(function (params) {
            _this.hiringRequestId = +params['hiringId'];
            _this.projectId = +params['projectId'];
        });
        this.getEmployeeDropDownList();
    };
    AddNewInterviewerComponent.prototype.getEmployeeDropDownList = function () {
        var _this = this;
        var model = {
            ProjectId: this.projectId,
            HiringRequestId: this.hiringRequestId
        };
        this.hiringRequestService.GetAllEmployeeList(model).subscribe(function (response) {
            _this.commonLoader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.employeesList.push({
                        EmployeeId: element.EmployeeId,
                        EmployeeName: element.EmployeeName,
                        EmployeeCode: element.EmployeeCode
                    });
                });
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    AddNewInterviewerComponent.prototype.OnInterviewersSelection = function (data) {
        var _this = this;
        this.employeesListTwo = [];
        data.value.forEach(function (element) {
            _this.employeesListTwo.push(_this.employeesList.find(function (x) { return x.EmployeeId === element; }));
        });
    };
    AddNewInterviewerComponent.prototype.OnFormSubmit = function () {
        if (this.employeesListTwo.length === 0) {
            this.toastr.warning('Please Select Interviewers');
        }
        else {
            this.dialogRef.close(this.employeesListTwo);
        }
    };
    //#region "onCancelPopup"
    AddNewInterviewerComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddNewInterviewerComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddNewInterviewerComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-new-interviewer',
            template: __webpack_require__(/*! ./add-new-interviewer.component.html */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.html"),
            styles: [__webpack_require__(/*! ./add-new-interviewer.component.scss */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__["CommonLoaderService"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_3__["HiringRequestsService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_5__["ActivatedRoute"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"]])
    ], AddNewInterviewerComponent);
    return AddNewInterviewerComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.html":
/*!*******************************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.html ***!
  \*******************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>\r\n      Add Language\r\n      <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n        <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n      </button>\r\n    </h1>\r\n    <form\r\n      class=\"example-form\"\r\n      [formGroup]=\"languageDetailForm\"\r\n      (ngSubmit)=\"onFormSubmit(languageDetailForm.value)\"\r\n    >\r\n      <div mat-dialog-content>\r\n        <div class=\"row\">\r\n          <div class=\"col-sm-12\">\r\n            <div class=\"row\">\r\n              <div class=\"col-lg-3 col-sm-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                  <input\r\n                    matInput\r\n                    formControlName=\"LanguageName\"\r\n                    placeholder=\"Language Name\"\r\n                  />\r\n                </mat-form-field>\r\n              </div>\r\n              <div class=\"col-lg-3 col-sm-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                  <mat-select\r\n                    placeholder=\"Reading\"\r\n                    formControlName=\"LanguageReading\"\r\n                  >\r\n                    <mat-option\r\n                      *ngFor=\"let item of ratingBasedDropDown\"\r\n                      [value]=\"item.Id\"\r\n                    >\r\n                      {{ item.value }}\r\n                    </mat-option>\r\n                  </mat-select>\r\n                </mat-form-field>\r\n              </div>\r\n              <div class=\"col-lg-3 col-sm-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                  <mat-select\r\n                    placeholder=\"Writing\"\r\n                    formControlName=\"LanguageWriting\"\r\n                  >\r\n                    <mat-option\r\n                      *ngFor=\"let item of ratingBasedDropDown\"\r\n                      [value]=\"item.Id\"\r\n                    >\r\n                      {{ item.value }}\r\n                    </mat-option>\r\n                  </mat-select>\r\n                </mat-form-field>\r\n              </div>\r\n              <div class=\"col-lg-3 col-sm-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                  <mat-select\r\n                    placeholder=\"Listining\"\r\n                    formControlName=\"LanguageListining\"\r\n                  >\r\n                    <mat-option\r\n                      *ngFor=\"let item of ratingBasedDropDown\"\r\n                      [value]=\"item.Id\"\r\n                    >\r\n                      {{ item.value }}\r\n                    </mat-option>\r\n                  </mat-select>\r\n                </mat-form-field>\r\n              </div>\r\n              <div class=\"col-lg-3 col-sm-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                  <mat-select\r\n                    placeholder=\"Speaking\"\r\n                    formControlName=\"LanguageSpeaking\"\r\n                  >\r\n                    <mat-option\r\n                      *ngFor=\"let item of ratingBasedDropDown\"\r\n                      [value]=\"item.Id\"\r\n                    >\r\n                      {{ item.value }}\r\n                    </mat-option>\r\n                  </mat-select>\r\n                </mat-form-field>\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div mat-dialog-actions class=\"pull-right\">\r\n          <hum-button\r\n            [type]=\"'save'\"\r\n            [text]=\"'save'\"\r\n            [isSubmit]=\"true\"\r\n          ></hum-button>\r\n          <hum-button\r\n            (click)=\"onCancelPopup()\"\r\n            [type]=\"'cancel'\"\r\n            [text]=\"'cancel'\"\r\n          ></hum-button>\r\n        </div>\r\n      </div>\r\n    </form>\r\n  </div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.scss":
/*!*******************************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.scss ***!
  \*******************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9wcm9qZWN0LW1hbmFnZW1lbnQvcHJvamVjdC1oaXJpbmcvaW50ZXJ2aWV3LWRldGFpbC9hZGQtbmV3LWxhbmd1YWdlL2FkZC1uZXctbGFuZ3VhZ2UuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.ts":
/*!*****************************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.ts ***!
  \*****************************************************************************************************************************/
/*! exports provided: AddNewLanguageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddNewLanguageComponent", function() { return AddNewLanguageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
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






var AddNewLanguageComponent = /** @class */ (function () {
    function AddNewLanguageComponent(fb, commonLoader, hiringRequestService, toastr, dialogRef) {
        this.fb = fb;
        this.commonLoader = commonLoader;
        this.hiringRequestService = hiringRequestService;
        this.toastr = toastr;
        this.dialogRef = dialogRef;
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
            LanguageName: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            LanguageReading: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            LanguageWriting: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            LanguageListining: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            LanguageSpeaking: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]]
        });
    }
    AddNewLanguageComponent.prototype.ngOnInit = function () { };
    AddNewLanguageComponent.prototype.onFormSubmit = function (data) {
        if (this.languageDetailForm.valid) {
            this.dialogRef.close(data);
        }
        else {
            this.toastr.warning('Form is Not Valid');
        }
    };
    //#region "onCancelPopup"
    AddNewLanguageComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddNewLanguageComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddNewLanguageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-new-language',
            template: __webpack_require__(/*! ./add-new-language.component.html */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.html"),
            styles: [__webpack_require__(/*! ./add-new-language.component.scss */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__["CommonLoaderService"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_3__["HiringRequestsService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_5__["MatDialogRef"]])
    ], AddNewLanguageComponent);
    return AddNewLanguageComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.html":
/*!*****************************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.html ***!
  \*****************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>\r\n    Add Traning\r\n    <button mat-icon-button [mat-dialog-close] class=\"pull-right\">\r\n      <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n    </button>\r\n  </h1>\r\n  <form\r\n    class=\"example-form\"\r\n    [formGroup]=\"traningDetailForm\"\r\n    (ngSubmit)=\"onFormSubmit(traningDetailForm.value)\"\r\n  >\r\n    <div mat-dialog-content>\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-12\">\r\n          <div class=\"row\">\r\n            <div class=\"col-lg-3 col-sm-3\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                <input\r\n                  matInput\r\n                  formControlName=\"TraningType\"\r\n                  placeholder=\"Traning Type\"\r\n                />\r\n              </mat-form-field>\r\n            </div>\r\n            <div class=\"col-lg-3 col-sm-3\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                <input\r\n                  matInput\r\n                  formControlName=\"TraningName\"\r\n                  placeholder=\"Traning Name\"\r\n                />\r\n              </mat-form-field>\r\n            </div>\r\n            <div class=\"col-lg-3 col-sm-3\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                <input\r\n                  matInput\r\n                  formControlName=\"TraningCountryAndCity\"\r\n                  placeholder=\"Traning Country, City\"\r\n                />\r\n              </mat-form-field>\r\n            </div>\r\n            <div class=\"col-lg-3 col-sm-3\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                <input\r\n                  matInput\r\n                  [matDatepicker]=\"StartDatePicker\"\r\n                  placeholder=\"Start Date\"\r\n                  formControlName=\"TraningStartDate\"\r\n                />\r\n                <mat-datepicker-toggle\r\n                  matSuffix\r\n                  [for]=\"StartDatePicker\"\r\n                ></mat-datepicker-toggle>\r\n                <mat-datepicker #StartDatePicker></mat-datepicker>\r\n              </mat-form-field>\r\n            </div>\r\n            <div class=\"col-lg-3 col-sm-3\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                <input\r\n                  matInput\r\n                  [matDatepicker]=\"EndDatePicker\"\r\n                  placeholder=\"End Date\"\r\n                  formControlName=\"TraningEndDate\"\r\n                />\r\n                <mat-datepicker-toggle\r\n                  matSuffix\r\n                  [for]=\"EndDatePicker\"\r\n                ></mat-datepicker-toggle>\r\n                <mat-datepicker #EndDatePicker></mat-datepicker>\r\n              </mat-form-field>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n      <div mat-dialog-actions class=\"pull-right\">\r\n        <hum-button\r\n          [type]=\"'save'\"\r\n          [text]=\"'save'\"\r\n          [isSubmit]=\"true\"\r\n        ></hum-button>\r\n        <hum-button\r\n          (click)=\"onCancelPopup()\"\r\n          [type]=\"'cancel'\"\r\n          [text]=\"'cancel'\"\r\n        ></hum-button>\r\n      </div>\r\n    </div>\r\n  </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.scss":
/*!*****************************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.scss ***!
  \*****************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9wcm9qZWN0LW1hbmFnZW1lbnQvcHJvamVjdC1oaXJpbmcvaW50ZXJ2aWV3LWRldGFpbC9hZGQtbmV3LXRyYW5pbmcvYWRkLW5ldy10cmFuaW5nLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.ts":
/*!***************************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.ts ***!
  \***************************************************************************************************************************/
/*! exports provided: AddNewTraningComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddNewTraningComponent", function() { return AddNewTraningComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
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






var AddNewTraningComponent = /** @class */ (function () {
    function AddNewTraningComponent(fb, commonLoader, hiringRequestService, toastr, dialogRef) {
        this.fb = fb;
        this.commonLoader = commonLoader;
        this.hiringRequestService = hiringRequestService;
        this.toastr = toastr;
        this.dialogRef = dialogRef;
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
        this.traningDetailForm = this.fb.group({
            TraningType: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            TraningName: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            TraningCountryAndCity: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            TraningStartDate: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            TraningEndDate: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]]
        });
    }
    AddNewTraningComponent.prototype.ngOnInit = function () { };
    AddNewTraningComponent.prototype.onFormSubmit = function (data) {
        if (this.traningDetailForm.valid) {
            this.dialogRef.close(data);
        }
        else {
            this.toastr.warning('Form is Not Valid');
        }
    };
    //#region "onCancelPopup"
    AddNewTraningComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    AddNewTraningComponent.prototype.onNoClick = function () {
        this.dialogRef.close();
    };
    AddNewTraningComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-new-traning',
            template: __webpack_require__(/*! ./add-new-traning.component.html */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.html"),
            styles: [__webpack_require__(/*! ./add-new-traning.component.scss */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__["CommonLoaderService"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_3__["HiringRequestsService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_5__["MatDialogRef"]])
    ], AddNewTraningComponent);
    return AddNewTraningComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.html":
/*!**************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.html ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template [headerClass]=\"'sub_header_template_main1'\">\r\n    <span class=\"action_header\">\r\n    Interview Details\r\n    <hum-button\r\n      [type]=\"'save'\"\r\n      [text]=\"'Save '\"\r\n      [isSubmit]=\"true\"\r\n      (click)=\"interviewFormButton.click()\"\r\n    ></hum-button>\r\n    <hum-button\r\n      [type]=\"'cancel'\"\r\n      [text]=\"'Cancel'\"\r\n      (click)=\"backToRequestDetail()\"\r\n    ></hum-button>\r\n    <hum-button\r\n      *ngIf=\"!isDisplay\"\r\n      [type]=\"'download'\"\r\n      [text]=\"'PDF Export'\"\r\n      (click)=\"onExportInterviewDetailsPdf()\"\r\n    ></hum-button>\r\n  </span>\r\n    <div class=\"action_section\"></div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n\r\n<mat-card [ngStyle]=\"scrollStyles\">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n            <div class=\"col-md-6\">\r\n                <h5>Candidate Summary</h5>\r\n                <table class=\"table table-striped\">\r\n                    <tbody>\r\n                        <tr>\r\n                            <td>Full Name</td>\r\n                            <td>{{ candidateDetails.FullName }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Duty Station</td>\r\n                            <td>-</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Gender</td>\r\n                            <td>{{ candidateDetails.Gender }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Tazkira/Passport No.</td>\r\n                            <td>-</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Qualification</td>\r\n                            <td>{{ candidateDetails.Qualification }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Institute/University</td>\r\n                            <td>-</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Date of Birth</td>\r\n                            <td>{{ candidateDetails.DateOfBirth }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Place of Birth</td>\r\n                            <td>-</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Tazkira Issued Place</td>\r\n                            <td>-</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Marital Status</td>\r\n                            <td>-</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Curriculum Vitae</td>\r\n                            <td><a (click)=\"getCandidateCvByCandidateId()\">(Download)</a></td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n                <br />\r\n                <h5>Hiring Request Details</h5>\r\n                <table class=\"table table-striped\">\r\n                    <tbody>\r\n                        <tr>\r\n                            <td>Office</td>\r\n                            <td>{{ hiringRequestDetail.Office }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Position/Designation</td>\r\n                            <td>{{ hiringRequestDetail.Position }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Job Grade</td>\r\n                            <td>{{ hiringRequestDetail.JobGrade }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Total Vacancies</td>\r\n                            <td>{{ hiringRequestDetail.TotalVacancy }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Filled Vacancies</td>\r\n                            <td>{{ hiringRequestDetail.FilledVacancy }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Pay Currency</td>\r\n                            <td>{{ hiringRequestDetail.PayCurrency }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Pay Hourly Rate</td>\r\n                            <td>{{ hiringRequestDetail.PayHourlyRate }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Budget Line</td>\r\n                            <td>{{ hiringRequestDetail.BudgetLine }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Job Type</td>\r\n                            <td>{{ hiringRequestDetail.JobType }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Announcing Date</td>\r\n                            <td>{{ hiringRequestDetail.AnouncingDate }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Closing Date</td>\r\n                            <td>{{ hiringRequestDetail.ClosingDate }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Contract Type</td>\r\n                            <td>{{ hiringRequestDetail.ContractType }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Contract Duration</td>\r\n                            <td>{{ hiringRequestDetail.ContractDuration }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Job Shift</td>\r\n                            <td>{{ hiringRequestDetail.JobShift }}</td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n                <br />\r\n                <h5>Required Qualification</h5>\r\n                <br />\r\n                <span>{{ hiringRequestDetail.KnowledgeAndSkillsRequired }}</span>\r\n                <table class=\"table table-striped\">\r\n                    <tbody>\r\n                        <tr>\r\n                            <td>Education Degree</td>\r\n                            <td>{{ hiringRequestDetail.EducationDegree }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Profession</td>\r\n                            <td>{{ hiringRequestDetail.Profession }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Experience (In Years)</td>\r\n                            <td>{{ hiringRequestDetail.TotalExperienceInYear }}</td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <!-- <div class=\"col-md-12\"> -->\r\n        <form class=\"example-form\" [formGroup]=\"interviewDetailForm\" (ngSubmit)=\"onFormSubmit(interviewDetailForm.value)\">\r\n            <button #interviewFormButton type=\"submit\" style=\"display: none;\"></button>\r\n            <mat-accordion>\r\n                <mat-expansion-panel>\r\n                    <mat-expansion-panel-header>\r\n                        <mat-panel-title>\r\n                            <h5><b>Rating Based Criteria</b></h5>\r\n                            <h5 style=\"margin-left: auto;\">\r\n                                <b> Professional Criteria Marks: </b\r\n                >{{ professionalCriteriaMarks }}\r\n              </h5>\r\n            </mat-panel-title>\r\n          </mat-expansion-panel-header>\r\n          <div\r\n            class=\"col-lg-3 col-sm-3\"\r\n            *ngFor=\"let data of ratingBasedCriteriaQuestionList\"\r\n          >\r\n            <h5>{{ data.value }}</h5>\r\n            <mat-form-field>\r\n              <mat-select\r\n                placeholder=\"Score\"\r\n                name=\"Score\"\r\n                (selectionChange)=\"onChangeRatingBasedCriteria(data.Id, $event)\"\r\n                [(value)]=\"data.selected\"\r\n              >\r\n                <mat-option\r\n                  *ngFor=\"let item of ratingBasedDropDown\"\r\n                  [value]=\"item.Id\"\r\n                  >{{ item.value }}\r\n                </mat-option>\r\n              </mat-select>\r\n            </mat-form-field>\r\n          </div>\r\n          <span *ngIf=\"ratingBasedCriteriaQuestionList.length <= 0\">\r\n            <p>Please add rating based questions first then submit the form</p>\r\n          </span>\r\n        </mat-expansion-panel>\r\n\r\n        <mat-expansion-panel style=\"margin-top: 2px;\">\r\n          <mat-expansion-panel-header>\r\n            <mat-panel-title>\r\n              <h5><b>Technical Questions</b></h5>\r\n                            <h5 style=\"margin-left: auto;\">\r\n                                <b>Marks Obtain: </b>{{ marksObtain }}\r\n                            </h5>\r\n                        </mat-panel-title>\r\n                    </mat-expansion-panel-header>\r\n                    <div class=\"col-lg-3 col-sm-3\" *ngFor=\"let temp of technicalQuestionList\">\r\n                        <h5>{{ temp.value }}</h5>\r\n                        <mat-form-field>\r\n                            <mat-select placeholder=\"Score\" name=\"Score\" [(value)]=\"temp.selected\" (selectionChange)=\"onChangeTechnicalQuestion(temp.Id, $event)\">\r\n                                <mat-option *ngFor=\"let item of technicalQuestionDropdown\" [value]=\"item.Id\">{{ item.value }}\r\n                                </mat-option>\r\n                            </mat-select>\r\n                        </mat-form-field>\r\n                    </div>\r\n                    <span *ngIf=\"technicalQuestionList.length <= 0\">\r\n            <p>Please add Technical questions first then submit the form</p>\r\n          </span>\r\n                </mat-expansion-panel>\r\n            </mat-accordion>\r\n            <mat-card style=\"margin-top: 2px;\">\r\n                <h5>\r\n                    Languages\r\n                    <hum-button [type]=\"'add'\" [text]=\"'ADD'\" (click)=\"addNewLanguage()\"></hum-button>\r\n                </h5>\r\n                <hum-table [headers]=\"languagesHeaders$\" [isDefaultAction]=\"false\" [items]=\"languagesList$\" [actions]='actions' (actionClick)=\"actionEventsLanguage($event)\"></hum-table>\r\n            </mat-card>\r\n            <mat-card style=\"margin-top: 2px;\">\r\n                <h5>\r\n                    Training\r\n                    <hum-button [type]=\"'add'\" [text]=\"'ADD'\" (click)=\"addNewTraning()\"></hum-button>\r\n                </h5>\r\n                <hum-table [headers]=\"traningHeaders$\" [isDefaultAction]=\"false\" [items]=\"traningList$\" [actions]='actions' (actionClick)=\"actionEventsTraining($event)\"></hum-table>\r\n            </mat-card>\r\n            <mat-card style=\"margin-top: 2px;\">\r\n                <h5>\r\n                    <b>\r\n            Interview Form\r\n          </b>\r\n                </h5>\r\n                <div class=\"row\">\r\n                    <div class=\"col-md-5\">\r\n                        <div class=\"row shift-left\">\r\n                            <span>Are you willing to travel to other provinces if required?</span\r\n              >\r\n              <mat-slide-toggle\r\n                class=\"pull-right\"\r\n                name=\"QuestionOne\"\r\n                formControlName=\"InterviewQuestionOne\"\r\n              >\r\n              </mat-slide-toggle>\r\n            </div>\r\n            <div class=\"row shift-left\">\r\n              <span>Do you have any health related issues?</span>\r\n                            <mat-slide-toggle class=\"pull-right\" name=\"QuestionTwo\" formControlName=\"InterviewQuestionTwo\">\r\n                            </mat-slide-toggle>\r\n                        </div>\r\n                        <div class=\"row shift-left\">\r\n                            <span>Are yoy willing to consider opportunity if it's based in the\r\n                provinces?</span\r\n              >\r\n              <mat-slide-toggle\r\n                class=\"pull-right\"\r\n                name=\"QuestionThree\"\r\n                formControlName=\"InterviewQuestionThree\"\r\n              >\r\n              </mat-slide-toggle>\r\n            </div>\r\n            <div class=\"row shift-left\">\r\n              <span\r\n                >What province would you prefer, if you consider an opportunity\r\n                given to you?</span\r\n              >\r\n            </div>\r\n            <br />\r\n            <div class=\"row shift-left\">\r\n              <div class=\"col-lg-12\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                  <textarea\r\n                    matInput\r\n                    formControlName=\"Description\"\r\n                    placeholder=\"Description\"\r\n                    matTextareaAutosize\r\n                    matAutosizeMinRows=\"5\"\r\n                    matAutosizeMaxRows=\"7\"\r\n                  ></textarea>\r\n                </mat-form-field>\r\n              </div>\r\n              <div class=\"col-lg-6\">\r\n                <lib-hum-dropdown\r\n                  formControlName=\"NoticePeriod\"\r\n                  [validation]=\"\r\n                    interviewDetailForm.controls['NoticePeriod'].hasError(\r\n                      'required'\r\n                    )\r\n                  \"\r\n                  [options]=\"noticePeriodList$\"\r\n                  [placeHolder]=\"'Notice Period'\"\r\n                ></lib-hum-dropdown>\r\n              </div>\r\n              <div class=\"col-lg-6\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                  <input\r\n                    matInput\r\n                    [matDatepicker]=\"AvailableDatePicker\"\r\n                    placeholder=\"Date Available To Join\"\r\n                    formControlName=\"AvailableDate\"\r\n                  />\r\n                  <mat-datepicker-toggle\r\n                    matSuffix\r\n                    [for]=\"AvailableDatePicker\"\r\n                  ></mat-datepicker-toggle>\r\n                  <mat-datepicker #AvailableDatePicker></mat-datepicker>\r\n                </mat-form-field>\r\n              </div>\r\n              <div class=\"col-lg-6\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                  <input\r\n                    matInput\r\n                    formControlName=\"WrittenTestMarks\"\r\n                    placeholder=\"Written Test Marks Obtain\"\r\n                    type=\"number\" (change)=\"WritenTextMarks($event)\"\r\n                  />\r\n                </mat-form-field>\r\n              </div>\r\n            </div>\r\n            <br />\r\n            <div class=\"row shift-left\">\r\n              <h5>\r\n                <b>Total Marks Obtain: </b><span>{{ totalMarksObtain }}</span>\r\n                            </h5>\r\n                        </div>\r\n                        <br />\r\n                        <div class=\"row shift-left\">\r\n                            <h5><b>Compensation and Benifits Requirement</b></h5>\r\n                            <div class=\"row shift-left\">\r\n                                <h5>Current</h5>\r\n                                <div class=\"col-lg-4\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <input matInput formControlName=\"CurrentBase\" placeholder=\"Base Salary\" type=\"number\" />\r\n                                    </mat-form-field>\r\n                                </div>\r\n                                <div class=\"col-lg-4\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <input matInput formControlName=\"CurrentOther\" placeholder=\"Other\" type=\"number\" />\r\n                                    </mat-form-field>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row shift-left\">\r\n                                <div class=\"col-lg-4\">\r\n                                    <span>Transport</span>\r\n                                    <mat-checkbox class=\"pull-right\" name=\"CurrentTransport\" formControlName=\"CurrentTransport\">\r\n                                    </mat-checkbox>\r\n                                </div>\r\n                                <div class=\"col-lg-4\">\r\n                                    <span>Meal</span>\r\n                                    <mat-checkbox class=\"pull-right\" name=\"CurrentMeal\" formControlName=\"CurrentMeal\"></mat-checkbox>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row shift-left\">\r\n                                <h5>Expectation</h5>\r\n                                <div class=\"col-lg-4\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <input matInput formControlName=\"ExpectationBase\" placeholder=\"Base\" type=\"number\" />\r\n                                    </mat-form-field>\r\n                                </div>\r\n                                <div class=\"col-lg-4\">\r\n                                    <mat-form-field class=\"example-full-width\">\r\n                                        <input matInput formControlName=\"ExpectationOther\" placeholder=\"Other\" type=\"number\" />\r\n                                    </mat-form-field>\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"row shift-left\">\r\n                                <div class=\"col-lg-4\">\r\n                                    <span>Transport</span>\r\n                                    <mat-checkbox class=\"pull-right\" name=\"ExpectationTransport\" formControlName=\"ExpectationTransport\">\r\n                                    </mat-checkbox>\r\n                                </div>\r\n                                <div class=\"col-lg-4\">\r\n                                    <span>Meal</span>\r\n                                    <mat-checkbox class=\"pull-right\" name=\"ExpectationMeal\" formControlName=\"ExpectationMeal\">\r\n                                    </mat-checkbox>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                        <br />\r\n                        <div class=\"row shift-left\">\r\n                            <h5><b>Reccomendations</b></h5>\r\n                            <div class=\"row shift-left\">\r\n                                <h5>Status</h5>\r\n                                <div class=\"col-lg-6\">\r\n                                    <lib-hum-dropdown formControlName=\"Status\" [validation]=\"\r\n                      interviewDetailForm.controls['Status'].hasError(\r\n                        'required'\r\n                      )\r\n                    \" [options]=\"statusList$\" [placeHolder]=\"'Select'\"></lib-hum-dropdown>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n\r\n                        <div class=\"row shift-left\">\r\n                            <h5>\r\n                                Interviewers\r\n                                <hum-button [type]=\"'add'\" [text]=\"'ADD'\" (click)=\"addInterviewers()\"></hum-button>\r\n                            </h5>\r\n                            <hum-table [headers]=\"interviewerHeaders$\" [isDefaultAction]=\"false\" [items]=\"interviewerList$\" [actions]='actions' (actionClick)=\"actionEventsInterviewers($event)\">\r\n                            </hum-table>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </mat-card>\r\n        </form>\r\n    </div>\r\n    <!-- </div> -->\r\n    <br /><br />\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.scss":
/*!**************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.scss ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".shift-left {\n  padding-left: 1.1em; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL3Byb2plY3QtbWFuYWdlbWVudC9wcm9qZWN0LWhpcmluZy9pbnRlcnZpZXctZGV0YWlsL2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXGRhc2hib2FyZFxccHJvamVjdC1tYW5hZ2VtZW50XFxwcm9qZWN0LWhpcmluZ1xcaW50ZXJ2aWV3LWRldGFpbFxcaW50ZXJ2aWV3LWRldGFpbC5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNFLG1CQUFtQixFQUFBIiwiZmlsZSI6InNyYy9hcHAvZGFzaGJvYXJkL3Byb2plY3QtbWFuYWdlbWVudC9wcm9qZWN0LWhpcmluZy9pbnRlcnZpZXctZGV0YWlsL2ludGVydmlldy1kZXRhaWwuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyIuc2hpZnQtbGVmdCB7XHJcbiAgcGFkZGluZy1sZWZ0OiAxLjFlbTtcclxufVxyXG5cclxuIl19 */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.ts":
/*!************************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.ts ***!
  \************************************************************************************************************/
/*! exports provided: InterviewDetailComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "InterviewDetailComponent", function() { return InterviewDetailComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_add_operator_pairwise__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! rxjs/add/operator/pairwise */ "./node_modules/rxjs-compat/_esm5/add/operator/pairwise.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _add_new_language_add_new_language_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./add-new-language/add-new-language.component */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.ts");
/* harmony import */ var _add_new_traning_add_new_traning_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./add-new-traning/add-new-traning.component */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var _add_new_interviewer_add_new_interviewer_component__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./add-new-interviewer/add-new-interviewer.component */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.ts");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



















var InterviewDetailComponent = /** @class */ (function () {
    function InterviewDetailComponent(dialog, datePipe, fb, commonLoader, routeActive, router, hiringRequestService, globalSharedService, appurl, toastr) {
        this.dialog = dialog;
        this.datePipe = datePipe;
        this.fb = fb;
        this.commonLoader = commonLoader;
        this.routeActive = routeActive;
        this.router = router;
        this.hiringRequestService = hiringRequestService;
        this.globalSharedService = globalSharedService;
        this.appurl = appurl;
        this.toastr = toastr;
        this.languagesHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])([
            'Language',
            'Reading',
            'Writing',
            'Listining',
            'Speaking'
        ]);
        this.traningHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])([
            'Training Type',
            'Name',
            'Country/City',
            'Start Date',
            'End Date'
        ]);
        this.interviewerHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(['Employee Id', 'Employee Code', 'Full Name']);
        this.temp = 0;
        this.isDisplay = true;
        this.professionalCriteriaMarks = 0;
        this.marksObtain = 0;
        this.totalMarksObtain = 0;
        this.tempLanguagesList = [];
        this.ratingBasedCriteriaQuestionList = [];
        this.technicalQuestionList = [];
        this.ratingBasedCriteriaAnswerList = [];
        this.technicalAnswerList = [];
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_6__["ReplaySubject"](1);
        this.ratingBasedDropDown = [
            {
                Id: 0,
                value: '0-Poor'
            },
            {
                Id: 3,
                value: '3-Below Average'
            },
            {
                Id: 5,
                value: '5-Average'
            },
            {
                Id: 7,
                value: '7-Above Average'
            },
            {
                Id: 10,
                value: '10-Outstanding'
            }
        ];
        this.technicalQuestionDropdown = [
            {
                Id: 0,
                value: '0-Poor'
            },
            {
                Id: 10,
                value: '10-Fair'
            },
            {
                Id: 15,
                value: '15-Good'
            },
            {
                Id: 20,
                value: '20-Excellent'
            },
            {
                Id: 30,
                value: '30-Perfect'
            }
        ];
        this.noticePeriodList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])([
            { name: '15 Days', value: 1 },
            { name: '30 Days', value: 2 },
            { name: 'Others', value: 3 }
        ]);
        this.statusList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])([
            { name: 'Hire', value: 1 },
            { name: '2nd Choice', value: 2 },
            { name: 'Test Day', value: 3 },
            { name: 'Recommended for Other Position', value: 4 },
            { name: 'Reject', value: 5 },
            { name: 'Over Qualified', value: 6 }
        ]);
        this.interviewDetailForm = this.fb.group({
            CandidateId: [null],
            HiringRequestId: [null],
            InterviewId: [null],
            RatingBasedCriteriaList: [[], [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            TechnicalQuestionList: [[], [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            LanguageList: [[], [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            TraningList: [[], [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            InterviewerList: [[], [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            Description: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            NoticePeriod: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            AvailableDate: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            WrittenTestMarks: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            CurrentBase: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            CurrentOther: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            ExpectationBase: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            ExpectationOther: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            Status: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            InterviewQuestionOne: [false, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            InterviewQuestionTwo: [false, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            InterviewQuestionThree: [false, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            CurrentTransport: [false, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            CurrentMeal: [false, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            ExpectationTransport: [false, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            ExpectationMeal: [false, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            ProfessionalCriteriaMark: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            MarksObtain: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            TotalMarksObtain: [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]]
        });
    }
    InterviewDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        if (this.interviewId > 0) {
            this.isDisplay = false;
        }
        this.actions = {
            items: {
                button: { status: false, text: '' },
                delete: true,
                download: false,
                edit: false
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false
            }
        };
        this.routeActive.queryParams.subscribe(function (params) {
            _this.candidateId = +params['candId'];
            _this.hiringRequestId = +params['hiringId'];
            _this.interviewId = +params['interviewId'];
        });
        this.routeActive.parent.parent.parent.params.subscribe(function (params) {
            _this.projectId = +params['id'];
        });
        this.candidateDetails = {
            FullName: '',
            DutyStation: '',
            Gender: '',
            Qualification: '',
            DateOfBirth: null
        };
        this.hiringRequestDetail = {
            Office: '',
            Position: '',
            JobGrade: '',
            TotalVacancy: null,
            FilledVacancy: null,
            PayCurrency: '',
            PayHourlyRate: null,
            BudgetLine: '',
            JobType: '',
            AnouncingDate: null,
            ClosingDate: null,
            ContractType: '',
            ContractDuration: null,
            JobShift: '',
            KnowledgeAndSkillsRequired: '',
            Profession: '',
            EducationDegree: '',
            TotalExperienceInYear: ''
        };
        this.getScreenSize();
        this.getAllHiringRequestDetails();
        this.getCandidateDetails();
    };
    //#region "Dynamic Scroll"
    InterviewDetailComponent.prototype.getScreenSize = function () {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    // #region "Get hiring reuquest details"
    InterviewDetailComponent.prototype.getAllHiringRequestDetails = function () {
        var _this = this;
        var model = {
            HiringRequestId: this.hiringRequestId,
            ProjectId: this.projectId
        };
        this.hiringRequestService
            .GetAllHiringRequestDetailForInterviewByHiringRequestId(model)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.commonLoader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                _this.hiringRequestDetail = {
                    OfficeId: response.data.OfficeId,
                    DesignationId: response.data.DesignationId,
                    Office: response.data.Office,
                    Position: response.data.Position,
                    JobGrade: response.data.JobGrade,
                    TotalVacancy: response.data.TotalVacancy,
                    FilledVacancy: response.data.FilledVacancy,
                    PayCurrency: response.data.PayCurrency,
                    PayHourlyRate: response.data.PayHourlyRate,
                    BudgetLine: response.data.BudgetLine,
                    JobType: response.data.JobType,
                    AnouncingDate: response.data.AnouncingDate,
                    ClosingDate: response.data.ClosingDate,
                    ContractType: response.data.ContractType,
                    ContractDuration: response.data.ContractDuration,
                    JobShift: response.data.JobShift,
                    KnowledgeAndSkillsRequired: response.data.KnowledgeAndSkillsRequired,
                    Profession: response.data.Profession,
                    EducationDegree: response.data.EducationDegree,
                    TotalExperienceInYear: response.data.TotalExperienceInYear
                };
                Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["forkJoin"])([
                    _this.getRatingBasedCriteriaQuestion(response.data.OfficeId),
                    _this.getTechnicalQuestionsByDesignationId(response.data.DesignationId)
                ]).subscribe(function (res) {
                    _this.commonLoader.showLoader();
                    if (res[0].statusCode === 200 && res[0].data !== null) {
                        res[0].data.forEach(function (element) {
                            _this.ratingBasedCriteriaQuestionList.push({
                                Id: element.QuestionsId,
                                value: element.Question,
                                selected: null
                            });
                        });
                    }
                    if (res[1].statusCode === 200 && res[1].data !== null) {
                        res[1].data.forEach(function (element) {
                            _this.technicalQuestionList.push({
                                Id: element.QuestionId,
                                value: element.Question
                            });
                        });
                    }
                    _this.commonLoader.hideLoader();
                    if (_this.interviewId > 0) {
                        _this.isDisplay = false;
                        _this.getInterviewDetailsByInterviewId();
                    }
                });
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    //#endregion
    // #region "Get candidate details"
    InterviewDetailComponent.prototype.getCandidateDetails = function () {
        var _this = this;
        this.hiringRequestService
            .GetCandidateDetailsByCandidateId(this.candidateId)
            .subscribe(function (response) {
            _this.commonLoader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                _this.candidateDetails = {
                    FullName: response.data.FullName,
                    DutyStation: response.data.DutyStation,
                    Gender: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["Gender"][response.data.Gender],
                    Qualification: response.data.Qualification,
                    DateOfBirth: response.data.DateOfBirth
                };
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    //#endregion
    //#region "Download Candidate Cv"
    InterviewDetailComponent.prototype.getCandidateCvByCandidateId = function () {
        var _this = this;
        var candidateId = this.candidateId;
        if (candidateId != null && candidateId !== undefined) {
            this.commonLoader.showLoader();
            this.hiringRequestService
                .DownloadCandidateCvByRequestId(candidateId)
                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
                .subscribe(function (response) {
                if (response.statusCode === 200 && response.data !== null) {
                    _this.candidateCv = {
                        AttachmentName: response.data.AttachmentName,
                        AttachmentUrl: response.data.AttachmentUrl,
                        UploadedBy: response.data.UploadedBy
                    };
                    var anchor = document.createElement('a');
                    anchor.href = _this.candidateCv.AttachmentUrl;
                    anchor.target = '_blank';
                    anchor.click();
                }
                _this.commonLoader.hideLoader();
            }, function () {
                _this.commonLoader.hideLoader();
            });
        }
    };
    //#endregion
    //#region "Calculate total marks"
    InterviewDetailComponent.prototype.WritenTextMarks = function (data) {
        if (data.srcElement.value != null && data.srcElement.value !== undefined) {
            this.TotalMarks(data.srcElement.value);
        }
    };
    InterviewDetailComponent.prototype.TotalMarks = function (data) {
        if (data != null && data !== undefined) {
            this.totalMarksObtain =
                this.professionalCriteriaMarks + this.marksObtain + +data;
        }
        else {
            this.totalMarksObtain = this.professionalCriteriaMarks + this.marksObtain;
        }
        this.interviewDetailForm.controls['TotalMarksObtain'].setValue(this.totalMarksObtain);
    };
    //#endregion
    // #region "Get rating based criteria questions"
    InterviewDetailComponent.prototype.getRatingBasedCriteriaQuestion = function (OfficeId) {
        return this.hiringRequestService.GetRatingBasedCriteriaQuestion(OfficeId);
    };
    //#endregion
    // #region "Get technical questions by designationId"
    InterviewDetailComponent.prototype.getTechnicalQuestionsByDesignationId = function (DesignationId) {
        return this.hiringRequestService.GetTechnicalQuestionsByDesignationId(DesignationId);
    };
    //#endregion
    // #region "On change rating based criteria question score"
    InterviewDetailComponent.prototype.onChangeRatingBasedCriteria = function (questionId, score) {
        if (this.ratingBasedCriteriaAnswerList.find(function (x) { return x.QuestionId === questionId; }) == null) {
            this.ratingBasedCriteriaAnswerList.push({
                QuestionId: questionId,
                Score: score.value
            });
        }
        else {
            this.ratingBasedCriteriaAnswerList.find(function (x) { return x.QuestionId === questionId; }).Score = score.value;
        }
        this.professionalCriteriaMarks =
            this.ratingBasedCriteriaAnswerList.reduce(function (sum, item) { return sum + item.Score; }, 0) / this.ratingBasedCriteriaAnswerList.length;
        this.TotalMarks(this.interviewDetailForm.get('WrittenTestMarks').value);
        this.interviewDetailForm.controls['ProfessionalCriteriaMark'].setValue(this.professionalCriteriaMarks);
        this.interviewDetailForm.controls['RatingBasedCriteriaList'].setValue(this.ratingBasedCriteriaAnswerList);
    };
    //#endregion
    // #region "On change technical question score"
    InterviewDetailComponent.prototype.onChangeTechnicalQuestion = function (questionId, score) {
        if (this.technicalAnswerList.find(function (x) { return x.QuestionId === questionId; }) == null) {
            this.technicalAnswerList.push({
                QuestionId: questionId,
                Score: score.value
            });
        }
        else {
            this.technicalAnswerList.find(function (x) { return x.QuestionId === questionId; }).Score =
                score.value;
        }
        this.marksObtain = this.technicalAnswerList.reduce(function (sum, item) { return sum + item.Score; }, 0);
        this.TotalMarks(this.interviewDetailForm.get('WrittenTestMarks').value);
        this.interviewDetailForm.controls['MarksObtain'].setValue(this.marksObtain);
        this.interviewDetailForm.controls['TechnicalQuestionList'].setValue(this.technicalAnswerList);
    };
    //#endregion
    // #region "Add new language"
    InterviewDetailComponent.prototype.addNewLanguage = function () {
        var _this = this;
        /** Open AddNewLaguage dialog box*/
        var dialogRef = this.dialog.open(_add_new_language_add_new_language_component__WEBPACK_IMPORTED_MODULE_10__["AddNewLanguageComponent"], {
            width: '950px'
        });
        dialogRef.afterClosed().subscribe(function (result) {
            if (result !== undefined) {
                if (_this.languagesList$ === undefined) {
                    /** binding result data(language details) to laguages list*/
                    _this.languagesList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])([
                        {
                            LanguageName: result.LanguageName,
                            LanguageReading: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][result.LanguageReading],
                            LanguageWriting: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][result.LanguageWriting],
                            LanguageListining: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][result.LanguageListining],
                            LanguageSpeaking: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][result.LanguageSpeaking]
                        }
                    ]);
                }
                else {
                    result.LanguageName = result.LanguageName;
                    result.LanguageReading = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][result.LanguageReading];
                    result.LanguageWriting = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][result.LanguageWriting];
                    result.LanguageListining = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][result.LanguageListining];
                    result.LanguageSpeaking = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["RatingAction"][result.LanguageSpeaking];
                    _this.languagesList$.subscribe(function (res) {
                        res.push(result);
                        _this.languagesList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(res);
                    });
                }
                _this.toastr.success('Language Added Successfully');
                _this.languagesList$.subscribe(function (res) {
                    _this.interviewDetailForm.controls['LanguageList'].setValue(res);
                });
            }
        });
    };
    //#endregion
    // #region "Add new traning"
    InterviewDetailComponent.prototype.addNewTraning = function () {
        var _this = this;
        /** Open AddNewTraning dialog box*/
        var dialogRef = this.dialog.open(_add_new_traning_add_new_traning_component__WEBPACK_IMPORTED_MODULE_11__["AddNewTraningComponent"], {
            width: '850px'
        });
        dialogRef.afterClosed().subscribe(function (result) {
            if (result !== undefined) {
                (result.TraningStartDate = _this.datePipe.transform(src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_15__["StaticUtilities"].getLocalDate(result.TraningStartDate), 'dd-MM-yyyy')),
                    (result.TraningEndDate = _this.datePipe.transform(src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_15__["StaticUtilities"].getLocalDate(result.TraningEndDate), 'dd-MM-yyyy'));
                if (_this.traningList$ === undefined) {
                    /** binding result data(traning details) to traning list*/
                    _this.traningList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])([
                        {
                            TraningType: result.TraningType,
                            TraningName: result.TraningName,
                            TraningCountryAndCity: result.TraningCountryAndCity,
                            TraningStartDate: result.TraningStartDate,
                            TraningEndDate: result.TraningEndDate
                        }
                    ]);
                }
                else {
                    _this.traningList$.subscribe(function (res) {
                        res.push(result);
                        _this.traningList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(res);
                    });
                }
                _this.toastr.success('Training Added Successfully');
                _this.traningList$.subscribe(function (res) {
                    _this.interviewDetailForm.controls['TraningList'].setValue(res);
                });
            }
        });
    };
    //#endregion
    // #region "Add inerviewers(multiple or single)"
    InterviewDetailComponent.prototype.addInterviewers = function () {
        var _this = this;
        /** Open AddInterviewers dialog box*/
        var dialogRef = this.dialog.open(_add_new_interviewer_add_new_interviewer_component__WEBPACK_IMPORTED_MODULE_13__["AddNewInterviewerComponent"], {
            width: '500px'
        });
        dialogRef.afterClosed().subscribe(function (result) {
            if (result !== undefined) {
                /** binding result data(interviewers details) to interviewer list*/
                result.forEach(function (element) {
                    if (_this.temp === 0) {
                        _this.interviewerList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])([
                            {
                                EmployeeId: element.EmployeeId,
                                EmployeeCode: element.EmployeeCode,
                                EmployeeName: element.EmployeeName
                            }
                        ]);
                        _this.temp = 1;
                        _this.toastr.success('Added Successfully', element.EmployeeName);
                    }
                    else {
                        _this.interviewerList$.subscribe(function (res) {
                            if (res.find(function (x) { return x.EmployeeId === element.EmployeeId; }) != null) {
                                _this.toastr.warning(' Already Selected', element.EmployeeName);
                            }
                            else {
                                res.push(element);
                                _this.interviewerList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(res);
                                _this.toastr.success('Added Successfully', element.EmployeeName);
                            }
                        });
                    }
                });
                _this.interviewerList$.subscribe(function (res) {
                    _this.interviewDetailForm.controls['InterviewerList'].setValue(res);
                });
            }
        });
    };
    //#endregion
    // #region "Delete perticular data from list"
    InterviewDetailComponent.prototype.actionEventsLanguage = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            var index_1;
            this.languagesList$.subscribe(function (res) {
                index_1 = res.findIndex(function (x) { return x.LanguageName === event.item.LanguageName; });
                res.splice(index_1, 1);
                _this.languagesList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(res);
            });
            // const index = (this.interviewDetailForm.controls['LanguageList']
            //   .value as Array<any>).findIndex(
            //   x => x.LanguageName === event.item.LanguageName
            // );
            // (this.interviewDetailForm.controls['LanguageList'].value as Array<
            //   any
            // >).splice(index, 1);
        }
    };
    InterviewDetailComponent.prototype.actionEventsTraining = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            var index_2;
            this.traningList$.subscribe(function (res) {
                index_2 = res.findIndex(function (x) { return x.TraningName === event.item.TraningName; });
                res.splice(index_2, 1);
                _this.traningList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(res);
            });
            // const index = (this.interviewDetailForm.controls['TraningList']
            //   .value as Array<any>).findIndex(
            //   x => x.LanguageName === event.item.LanguageName
            // );
            // (this.interviewDetailForm.controls['TraningList'].value as Array<
            //   any
            // >).splice(index, 1);
        }
    };
    InterviewDetailComponent.prototype.actionEventsInterviewers = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            var index_3;
            this.interviewerList$.subscribe(function (res) {
                index_3 = res.findIndex(function (x) { return x.EmployeeId === event.item.EmployeeId; });
                res.splice(index_3, 1);
                _this.interviewerList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(res);
            });
            // const index = (this.interviewDetailForm.controls['InterviewerList']
            //   .value as Array<any>).findIndex(
            //   x => x.EmployeeId === event.item.EmployeeId
            // );
            // (this.interviewDetailForm.controls['InterviewerList'].value as Array<
            //   any
            // >).splice(index, 1);
        }
    };
    //#endregion
    //#region "Add interview details"
    InterviewDetailComponent.prototype.AddInterviewDetails = function (data) {
        var _this = this;
        this.commonLoader.showLoader();
        data.CandidateId = this.candidateId;
        data.HiringRequestId = this.hiringRequestId;
        this.hiringRequestService.AddInterviewDetails(data).subscribe(function (response) {
            if (response.statusCode === 200) {
                _this.toastr.success('Interview details added successfully');
                _this.backToRequestDetail();
            }
            else {
                _this.toastr.error(response.message);
            }
        }, function (error) {
            _this.toastr.error('Someting went wrong. Please try again');
        });
        this.commonLoader.showLoader();
    };
    //#endregion
    //#region "Add interview details"
    InterviewDetailComponent.prototype.EditInterviewDetails = function (data) {
        var _this = this;
        this.commonLoader.showLoader();
        data.CandidateId = this.candidateId;
        data.HiringRequestId = this.hiringRequestId;
        data.InterviewId = this.interviewId;
        this.languagesList$.subscribe(function (res) {
            data.LanguageList = res;
        });
        this.traningList$.subscribe(function (res) {
            data.TraningList = res;
        });
        data.ProfessionalCriteriaMark = this.professionalCriteriaMarks;
        data.MarksObtain = this.marksObtain;
        data.RatingBasedCriteriaList = this.ratingBasedCriteriaQuestionList;
        data.TechnicalQuestionList = this.technicalQuestionList;
        this.interviewerList$.subscribe(function (res) {
            data.InterviewerList = res;
        });
        this.hiringRequestService.EditInterviewDetails(data).subscribe(function (response) {
            if (response.statusCode === 200) {
                _this.toastr.success('Interview details updated successfully');
                //this.backToRequestDetail();
            }
            else {
                _this.toastr.error(response.message);
            }
        }, function (error) {
            _this.toastr.error('Someting went wrong. Please try again');
        });
        this.commonLoader.hideLoader();
    };
    //#endregion
    //#region "On submission of interview form"
    InterviewDetailComponent.prototype.onFormSubmit = function (data) {
        if (this.interviewDetailForm.valid) {
            if (this.interviewId > 0) {
                this.EditInterviewDetails(data);
            }
            else {
                this.AddInterviewDetails(data);
            }
        }
        else {
            this.toastr.warning('Please fill all required fields');
        }
    };
    //#endregion
    // #region "gGet interview details by InterviewId"
    InterviewDetailComponent.prototype.getInterviewDetailsByInterviewId = function () {
        var _this = this;
        var InterviewId = this.interviewId;
        this.hiringRequestService
            .GetInterviewDetailsByInterviewId(InterviewId)
            .subscribe(function (response) {
            _this.commonLoader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                _this.setRemainingInterviewDetails(response.data);
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    //#endregion
    //#region "Set remaining interview details"
    InterviewDetailComponent.prototype.setRemainingInterviewDetails = function (data) {
        var _this = this;
        this.interviewDetailForm.patchValue({
            CandidateId: data.CandidateId,
            HiringRequestId: data.HiringRequestId,
            InterviewId: data.InterviewId,
            Description: data.Description,
            NoticePeriod: data.NoticePeriod,
            AvailableDate: data.AvailableDate,
            WrittenTestMarks: data.WrittenTestMarks,
            CurrentBase: data.CurrentBase,
            CurrentOther: data.CurrentOther,
            ExpectationBase: data.ExpectationBase,
            ExpectationOther: data.ExpectationOther,
            Status: data.Status,
            InterviewQuestionOne: data.InterviewQuestionOne,
            InterviewQuestionTwo: data.InterviewQuestionTwo,
            InterviewQuestionThree: data.InterviewQuestionThree,
            CurrentTransport: data.CurrentTransport,
            CurrentMeal: data.CurrentMeal,
            ExpectationTransport: data.ExpectationTransport,
            ExpectationMeal: data.ExpectationMeal,
            LanguageList: data.LanguageList,
            TraningList: data.TraningList,
            InterviewerList: data.InterviewerList,
            ProfessionalCriteriaMark: data.ProfessionalCriteriaMark,
            MarksObtain: data.MarksObtain,
            TotalMarksObtain: data.TotalMarksObtain,
            RatingBasedCriteriaList: data.RatingBasedCriteriaList,
            TechnicalQuestionList: data.TechnicalQuestionList
        });
        data.CandidateId = this.candidateId;
        data.HiringRequestId = this.hiringRequestId;
        data.CandidateName = this.candidateDetails.FullName;
        data.Qualification = this.candidateDetails.Qualification;
        data.Position = this.hiringRequestDetail.Position;
        data.DutyStation = this.hiringRequestDetail.Office;
        data.MaritalStatus = '-';
        data.PassportNumber = '-';
        data.NameOfInstitute = '-';
        data.DateOfBirth = this.candidateDetails.DateOfBirth;
        data.TechnicalQuestionList.forEach(function (element, i) {
            _this.technicalQuestionList[i].selected = element.Score;
        });
        data.RatingBasedCriteriaList.forEach(function (element, i) {
            _this.ratingBasedCriteriaQuestionList[i].selected = element.Score;
        });
        this.totalMarksObtain = data.TotalMarksObtain;
        this.marksObtain = data.MarksObtain;
        this.professionalCriteriaMarks = data.ProfessionalCriteriaMark;
        this.languagesList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(data.LanguageList);
        this.traningList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(data.TraningList);
        this.interviewerList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(data.InterviewerList);
        //   this.interviewDetailForm = this.fb.group({
        //     CandidateId: data.CandidateId,
        //     HiringRequestId: data.HiringRequestId,
        //     Description: data.Description,
        //     NoticePeriod: data.NoticePeriod,
        //     AvailableDate: data.AvailableDate,
        //     WrittenTestMarks: data.WrittenTestMarks,
        //     CurrentBase: data.CurrentBase,
        //     CurrentOther: data.CurrentOther,
        //     ExpectationBase: data.ExpectationBase,
        //     ExpectationOther: data.ExpectationOther,
        //     Status: data.Status,
        //     InterviewQuestionOne: data.InterviewQuestionOne,
        //     InterviewQuestionTwo: data.InterviewQuestionTwo,
        //     InterviewQuestionThree: data.InterviewQuestionThree,
        //     CurrentTransport: data.CurrentTransport,
        //     CurrentMeal: data.CurrentMeal,
        //     ExpectationTransport: data.ExpectationTransport,
        //     ExpectationMeal: data.ExpectationMeal,
        //     LanguageList : data.LanguageList,
        //     TraningList : data.TraningList,
        //     InterviewerList : data.InterviewerList
        //   });
        //   this.interviewDetails = data;
    };
    //#endregion
    //#region "On export of interview details pdf"
    InterviewDetailComponent.prototype.onExportInterviewDetailsPdf = function () {
        this.commonLoader.showLoader();
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_16__["GLOBAL"].API_Pdf_GetInterviewDetailReportPdf, this.interviewDetails)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
            .subscribe();
        this.commonLoader.hideLoader();
    };
    //#endregion
    //#region "Route back to request detail page"
    InterviewDetailComponent.prototype.backToRequestDetail = function () {
        window.history.back();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", []),
        __metadata("design:returntype", void 0)
    ], InterviewDetailComponent.prototype, "getScreenSize", null);
    InterviewDetailComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-interview-detail',
            template: __webpack_require__(/*! ./interview-detail.component.html */ "./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.html"),
            styles: [__webpack_require__(/*! ./interview-detail.component.scss */ "./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_9__["MatDialog"],
            _angular_common__WEBPACK_IMPORTED_MODULE_14__["DatePipe"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__["CommonLoaderService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["ActivatedRoute"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_4__["HiringRequestsService"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_17__["GlobalSharedService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_18__["AppUrlService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_5__["ToastrService"]])
    ], InterviewDetailComponent);
    return InterviewDetailComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.html":
/*!**************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.html ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template [headerClass]=\"'sub_header_template_main1'\">\r\n  <span class=\"action_header\"\r\n    >Job Details\r\n    <hum-button\r\n\r\n      *ngIf=\"!isAddJobFormSubmitted\"\r\n\r\n      (click)=\"addJobFormSubmit()\"\r\n      [type]=\"'save'\"\r\n      [text]=\"'save'\"\r\n    >\r\n    </hum-button>\r\n    <hum-button\r\n      *ngIf=\"isAddJobFormSubmitted\"\r\n      [type]=\"'loading'\"\r\n      [text]=\"'Saving....'\"\r\n    ></hum-button>\r\n    <hum-button\r\n      [routerLink]=\"['../hiring-request']\"\r\n      [type]=\"'cancel'\"\r\n      [text]=\"'cancel'\"\r\n    ></hum-button>\r\n  </span>\r\n  <div class=\"action_section\">\r\n    <hum-button\r\n      [type]=\"'text'\"\r\n      [text]=\"'All Jobs'\"\r\n      [routerLink]=\"['job-detail']\"\r\n    ></hum-button>\r\n    <!-- <hum-button [type]=\"'down'\" [text]=\"'jobs'\"></hum-button> -->\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n\r\n<mat-card>\r\n  <div class=\"container\">\r\n    <form [formGroup]=\"addJobForm\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-6\">\r\n          <mat-form-field class=\"example-full-width\">\r\n            <textarea\r\n              matInput\r\n              formControlName=\"description\"\r\n              placeholder=\"Description\"\r\n            ></textarea>\r\n            <mat-error\r\n              *ngIf=\"addJobForm.controls['description'].hasError('required')\"\r\n              >*Required</mat-error\r\n            >\r\n          </mat-form-field>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-6\">\r\n          <div class=\"col-md-6\">\r\n            <lib-hum-dropdown\r\n              formControlName=\"office\"\r\n              [validation]=\"addJobForm.controls['office'].hasError('required')\"\r\n              [options]=\"officeList$\"\r\n              [placeHolder]=\"'Office'\"\r\n            ></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-6\">\r\n            <lib-hum-dropdown\r\n              formControlName=\"position\"\r\n              [validation]=\"\r\n                addJobForm.controls['position'].hasError('required')\r\n              \"\r\n              [options]=\"ProfessionList$\"\r\n              [placeHolder]=\"'Position'\"\r\n            ></lib-hum-dropdown>\r\n          </div>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-6\">\r\n          <div class=\"col-md-6\">\r\n            <lib-hum-dropdown\r\n              formControlName=\"jobGrade\"\r\n              [validation]=\"\r\n                addJobForm.controls['jobGrade'].hasError('required')\r\n              \"\r\n              [options]=\"gradeList$\"\r\n              [placeHolder]=\"'Job Grade'\"\r\n            ></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-6\">\r\n            <mat-form-field class=\"example-full-width\">\r\n              <input\r\n                matInput type=\"number\" min=\"0\"\r\n                formControlName=\"totalVacancies\"\r\n                placeholder=\"Total Vacancies\"\r\n              />\r\n              <mat-error\r\n                *ngIf=\"\r\n                  addJobForm.controls['totalVacancies'].hasError('required')\r\n                \"\r\n                >Required</mat-error\r\n              >\r\n            </mat-form-field>\r\n          </div>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-6\">\r\n          <div class=\"col-md-6\">\r\n            <lib-hum-dropdown\r\n              formControlName=\"payCurrency\"\r\n              [validation]=\"\r\n                addJobForm.controls['payCurrency'].hasError('required')\r\n              \"\r\n              [options]=\"payCurrencyList$\"\r\n              [placeHolder]=\"'Pay Currency'\"\r\n            ></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-6\">\r\n            <mat-form-field class=\"example-full-width\">\r\n              <input\r\n                matInput type=\"number\" min=\"0\"\r\n                formControlName=\"payRate\"\r\n                placeholder=\"Pay Hourly Rate\"\r\n              />\r\n              <mat-error\r\n                *ngIf=\"addJobForm.controls['payRate'].hasError('required')\"\r\n                >Required</mat-error\r\n              >\r\n            </mat-form-field>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </form>\r\n  </div>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.scss":
/*!**************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.scss ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9wcm9qZWN0LW1hbmFnZW1lbnQvcHJvamVjdC1oaXJpbmcvam9iLWRldGFpbC9qb2ItZGV0YWlsLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.ts":
/*!************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.ts ***!
  \************************************************************************************************/
/*! exports provided: JobDetailComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "JobDetailComponent", function() { return JobDetailComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var JobDetailComponent = /** @class */ (function () {
    function JobDetailComponent(fb, routeActive, commonLoader, toastr, router, hiringRequestService) {
        this.fb = fb;
        this.routeActive = routeActive;
        this.commonLoader = commonLoader;
        this.toastr = toastr;
        this.router = router;
        this.hiringRequestService = hiringRequestService;
        this.isAddJobFormSubmitted = false;
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_2__["ReplaySubject"](1);
        this.addJobForm = this.fb.group({
            description: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            office: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            position: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            jobGrade: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            totalVacancies: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            payCurrency: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            payRate: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            projectId: ['']
        });
    }
    JobDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.routeActive.parent.params.subscribe(function (params) {
            _this.projectId = +params['id'];
        });
        Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["forkJoin"])([
            this.getAllOfficeList(),
            this.getAllJobGradeList(),
            this.getAllPayCurrencyList(),
            this.getAllProfessionList()
        ])
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeOfficeList(result[0]);
            _this.subscribeGradeList(result[1]);
            _this.subscribePayCurrencyList(result[2]);
            _this.subscribeProfessionList(result[3]);
        });
    };
    JobDetailComponent.prototype.getAllOfficeList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetOfficeList();
    };
    JobDetailComponent.prototype.getAllJobGradeList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetJobGradeList();
    };
    JobDetailComponent.prototype.getAllPayCurrencyList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetCurrencyList();
    };
    JobDetailComponent.prototype.getAllProfessionList = function () {
        this.commonLoader.showLoader();
        return this.hiringRequestService.GetProfessionList();
    };
    JobDetailComponent.prototype.subscribeOfficeList = function (response) {
        this.commonLoader.hideLoader();
        this.officeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(response.data.map(function (y) {
            return {
                value: y.OfficeId,
                name: y.OfficeName
            };
        }));
    };
    JobDetailComponent.prototype.subscribeGradeList = function (response) {
        this.commonLoader.hideLoader();
        this.gradeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(response.data.map(function (y) {
            return {
                value: y.GradeId,
                name: y.GradeName
            };
        }));
    };
    JobDetailComponent.prototype.subscribePayCurrencyList = function (response) {
        this.commonLoader.hideLoader();
        this.payCurrencyList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(response.data.map(function (y) {
            return {
                value: y.CurrencyId,
                name: y.CurrencyName
            };
        }));
    };
    JobDetailComponent.prototype.subscribeProfessionList = function (response) {
        this.commonLoader.hideLoader();
        this.ProfessionList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(response.data.map(function (y) {
            return {
                value: y.ProfessionId,
                name: y.ProfessionName
            };
        }));
    };
    JobDetailComponent.prototype.addJobFormSubmit = function () {
        if (this.addJobForm.valid) {
            this.addJobForm.controls['projectId'].setValue(this.projectId);
            this.isAddJobFormSubmitted = true;
            this.hiringRequestService.addJobHiringDetails(this.addJobForm.value)
                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["takeUntil"])(this.destroyed$))
                .subscribe();
            this.isAddJobFormSubmitted = false;
            this.toastr.success('Job Sucessfully Inserted');
        }
        else {
            this.toastr.warning('Please correct errors in job form and submit again');
        }
    };
    JobDetailComponent.prototype.cancelButtonClicked = function () {
        this.router.navigate(['../hiring-request']);
    };
    JobDetailComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-job-detail',
            template: __webpack_require__(/*! ./job-detail.component.html */ "./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.html"),
            styles: [__webpack_require__(/*! ./job-detail.component.scss */ "./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"],
            _angular_router__WEBPACK_IMPORTED_MODULE_6__["ActivatedRoute"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_4__["CommonLoaderService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_7__["ToastrService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_6__["Router"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_5__["HiringRequestsService"]])
    ], JobDetailComponent);
    return JobDetailComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/project-hiring-routing.module.ts":
/*!**********************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/project-hiring-routing.module.ts ***!
  \**********************************************************************************************/
/*! exports provided: ProjectHiringRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProjectHiringRoutingModule", function() { return ProjectHiringRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _hiring_requests_hiring_requests_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./hiring-requests/hiring-requests.component */ "./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.ts");
/* harmony import */ var _job_detail_job_detail_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./job-detail/job-detail.component */ "./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.ts");
/* harmony import */ var _interview_detail_interview_detail_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./interview-detail/interview-detail.component */ "./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.ts");
/* harmony import */ var _entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./entry-component/entry-component.component */ "./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.ts");
/* harmony import */ var _request_detail_request_detail_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./request-detail/request-detail.component */ "./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};







var routes = [
    {
        path: '', component: _entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_5__["EntryComponentComponent"],
        children: [
            { path: 'job-detail', component: _job_detail_job_detail_component__WEBPACK_IMPORTED_MODULE_3__["JobDetailComponent"] },
            { path: 'interview-detail', component: _interview_detail_interview_detail_component__WEBPACK_IMPORTED_MODULE_4__["InterviewDetailComponent"] },
            // { path: 'interview-detail/:id', component: InterviewDetailComponent },
            { path: 'requests', component: _hiring_requests_hiring_requests_component__WEBPACK_IMPORTED_MODULE_2__["HiringRequestsComponent"] },
            { path: ':id', component: _request_detail_request_detail_component__WEBPACK_IMPORTED_MODULE_6__["RequestDetailComponent"] },
        ]
    }
    // { path: 'interview-detail', component: InterviewDetailComponent }
];
var ProjectHiringRoutingModule = /** @class */ (function () {
    function ProjectHiringRoutingModule() {
    }
    ProjectHiringRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], ProjectHiringRoutingModule);
    return ProjectHiringRoutingModule;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/project-hiring.module.ts":
/*!**************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/project-hiring.module.ts ***!
  \**************************************************************************************/
/*! exports provided: ProjectHiringModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProjectHiringModule", function() { return ProjectHiringModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _project_hiring_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./project-hiring-routing.module */ "./src/app/dashboard/project-management/project-hiring/project-hiring-routing.module.ts");
/* harmony import */ var _hiring_requests_hiring_requests_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./hiring-requests/hiring-requests.component */ "./src/app/dashboard/project-management/project-hiring/hiring-requests/hiring-requests.component.ts");
/* harmony import */ var _job_detail_job_detail_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./job-detail/job-detail.component */ "./src/app/dashboard/project-management/project-hiring/job-detail/job-detail.component.ts");
/* harmony import */ var projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! projects/library/src/public_api */ "./projects/library/src/public_api.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _request_detail_request_detail_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./request-detail/request-detail.component */ "./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _add_hiring_request_add_hiring_request_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./add-hiring-request/add-hiring-request.component */ "./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.ts");
/* harmony import */ var _add_new_candidate_add_new_candidate_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./add-new-candidate/add-new-candidate.component */ "./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.ts");
/* harmony import */ var _interview_detail_interview_detail_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./interview-detail/interview-detail.component */ "./src/app/dashboard/project-management/project-hiring/interview-detail/interview-detail.component.ts");
/* harmony import */ var _interview_detail_add_new_language_add_new_language_component__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./interview-detail/add-new-language/add-new-language.component */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-language/add-new-language.component.ts");
/* harmony import */ var _interview_detail_add_new_traning_add_new_traning_component__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./interview-detail/add-new-traning/add-new-traning.component */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-traning/add-new-traning.component.ts");
/* harmony import */ var _interview_detail_add_new_interviewer_add_new_interviewer_component__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./interview-detail/add-new-interviewer/add-new-interviewer.component */ "./src/app/dashboard/project-management/project-hiring/interview-detail/add-new-interviewer/add-new-interviewer.component.ts");
/* harmony import */ var _entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./entry-component/entry-component.component */ "./src/app/dashboard/project-management/project-hiring/entry-component/entry-component.component.ts");
/* harmony import */ var _candidate_table_candidate_table_component__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./candidate-table/candidate-table.component */ "./src/app/dashboard/project-management/project-hiring/candidate-table/candidate-table.component.ts");
/* harmony import */ var _add_analytical_info_add_analytical_info_component__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ./add-analytical-info/add-analytical-info.component */ "./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


















var ProjectHiringModule = /** @class */ (function () {
    function ProjectHiringModule() {
    }
    ProjectHiringModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _hiring_requests_hiring_requests_component__WEBPACK_IMPORTED_MODULE_3__["HiringRequestsComponent"],
                _job_detail_job_detail_component__WEBPACK_IMPORTED_MODULE_4__["JobDetailComponent"],
                _request_detail_request_detail_component__WEBPACK_IMPORTED_MODULE_7__["RequestDetailComponent"],
                _add_hiring_request_add_hiring_request_component__WEBPACK_IMPORTED_MODULE_9__["AddHiringRequestComponent"],
                _add_new_candidate_add_new_candidate_component__WEBPACK_IMPORTED_MODULE_10__["AddNewCandidateComponent"],
                _interview_detail_interview_detail_component__WEBPACK_IMPORTED_MODULE_11__["InterviewDetailComponent"],
                _interview_detail_add_new_language_add_new_language_component__WEBPACK_IMPORTED_MODULE_12__["AddNewLanguageComponent"],
                _interview_detail_add_new_traning_add_new_traning_component__WEBPACK_IMPORTED_MODULE_13__["AddNewTraningComponent"],
                _interview_detail_add_new_interviewer_add_new_interviewer_component__WEBPACK_IMPORTED_MODULE_14__["AddNewInterviewerComponent"],
                _interview_detail_interview_detail_component__WEBPACK_IMPORTED_MODULE_11__["InterviewDetailComponent"],
                _entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_15__["EntryComponentComponent"],
                _candidate_table_candidate_table_component__WEBPACK_IMPORTED_MODULE_16__["CandidateTableComponent"],
                _add_analytical_info_add_analytical_info_component__WEBPACK_IMPORTED_MODULE_17__["AddAnalyticalInfoComponent"]
            ],
            imports: [
                _angular_forms__WEBPACK_IMPORTED_MODULE_8__["ReactiveFormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_8__["FormsModule"],
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _project_hiring_routing_module__WEBPACK_IMPORTED_MODULE_2__["ProjectHiringRoutingModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_5__["SubHeaderTemplateModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_5__["LibraryModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatDividerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatInputModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatCardModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatPaginatorModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatTabsModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatDialogModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatIconModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatFormFieldModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatSelectModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatDatepickerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatButtonModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatTableModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatCheckboxModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatExpansionModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatSlideToggleModule"]
            ],
            exports: [_hiring_requests_hiring_requests_component__WEBPACK_IMPORTED_MODULE_3__["HiringRequestsComponent"], _job_detail_job_detail_component__WEBPACK_IMPORTED_MODULE_4__["JobDetailComponent"]],
            entryComponents: [
                _add_hiring_request_add_hiring_request_component__WEBPACK_IMPORTED_MODULE_9__["AddHiringRequestComponent"],
                _add_new_candidate_add_new_candidate_component__WEBPACK_IMPORTED_MODULE_10__["AddNewCandidateComponent"],
                _interview_detail_add_new_language_add_new_language_component__WEBPACK_IMPORTED_MODULE_12__["AddNewLanguageComponent"],
                _interview_detail_add_new_traning_add_new_traning_component__WEBPACK_IMPORTED_MODULE_13__["AddNewTraningComponent"],
                _interview_detail_add_new_interviewer_add_new_interviewer_component__WEBPACK_IMPORTED_MODULE_14__["AddNewInterviewerComponent"],
                _add_analytical_info_add_analytical_info_component__WEBPACK_IMPORTED_MODULE_17__["AddAnalyticalInfoComponent"]
            ]
        })
    ], ProjectHiringModule);
    return ProjectHiringModule;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.html":
/*!**********************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.html ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template [headerClass]=\"'sub_header_template_main1'\">\r\n    <span class=\"action_header\">\r\n    <i class=\"fa fa-arrow-left bckicon\" (click)=\"backToList()\"></i>\r\n      Request Details\r\n    <hum-button *ngIf=\"!IsHiringRequestCompleted && !IsHiringRequestClosed\"\r\n      [type]=\"'edit'\"\r\n      [text]=\"'Edit'\"\r\n      (click)=\"editHiringRequest()\"\r\n    ></hum-button>\r\n    <hum-button *ngIf=\"!IsHiringRequestCompleted && !IsHiringRequestClosed\"  [type]=\"'save'\" [text]=\"'complete '\" (click)=\"onCompleteRequest()\"></hum-button>\r\n    <hum-button *ngIf=\"!IsHiringRequestClosed && !IsHiringRequestCompleted\" [type]=\"'cancel'\" (click)=\"onCloseRequest()\" [text]=\"'close request'\"></hum-button>\r\n\r\n    <hum-button *ngIf=\"IsHiringRequestCompleted\"  [type]=\"'completed'\" [text]=\"'Completed '\"></hum-button>\r\n    <hum-button *ngIf=\"IsHiringRequestClosed\"  [type]=\"'cancel'\" [text]=\"'Closed '\" [disabled]=\"true\"></hum-button>\r\n    <hum-button  [type]=\"'download'\" [text]=\"'PDF Export'\" (click)=\"onExportHiringRequestPdf()\"></hum-button>\r\n  </span>\r\n    <div class=\"action_section\"></div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n\r\n<mat-card [ngStyle]=\"scrollStyles\">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n            <div class=\"col-md-6\">\r\n                <h4> <span> Code: </span> {{ hiringRequestDetails.HiringRequestCode }}</h4>\r\n                <h5> Specific Duties & Responsibilities</h5>\r\n                <mat-card-subtitle>\r\n                    {{hiringRequestDetails.SpecificDutiesAndResponsibilities}}\r\n                </mat-card-subtitle>\r\n                <h5>Submission Guildelines</h5>\r\n                <mat-card-subtitle>\r\n                    {{hiringRequestDetails.SubmissionGuidelines}}\r\n                </mat-card-subtitle>\r\n                <br>\r\n                <h5>Hiring Request Details</h5>\r\n                <table class=\"table table-striped\">\r\n                    <tbody>\r\n                        <tr>\r\n                            <td>Office</td>\r\n                            <td>{{ hiringRequestDetails.Office }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Position/Designation</td>\r\n                            <td>{{ hiringRequestDetails.Position }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Job Grade</td>\r\n                            <td>{{ hiringRequestDetails.JobGrade }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Total Vacancies</td>\r\n                            <td>{{ hiringRequestDetails.TotalVacancies }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Filled Vacancies</td>\r\n                            <td>{{ hiringRequestDetails.FilledVacancies }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Pay Currency</td>\r\n                            <td>{{ hiringRequestDetails.PayCurrency }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Pay Hourly Rate</td>\r\n                            <td>{{ hiringRequestDetails.PayRate }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Budget Line</td>\r\n                            <td>{{ hiringRequestDetails.BudgetName }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Job Type</td>\r\n                            <td>{{ hiringRequestDetails.JobType }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Job Category</td>\r\n                            <td>{{ hiringRequestDetails.JobCategory }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Announcing Date</td>\r\n                            <td>{{ hiringRequestDetails.AnouncingDate }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Closing Date</td>\r\n                            <td>{{ hiringRequestDetails.ClosingDate }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Contract Type</td>\r\n                            <td>{{ hiringRequestDetails.ContractType }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Contract Duration(Months)</td>\r\n                            <td>{{ hiringRequestDetails.ContractDuration }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Shift</td>\r\n                            <td>{{ hiringRequestDetails.Shift }}</td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-12\">\r\n            <div class=\"col-md-6\">\r\n                <h5>Required Qualifications</h5>\r\n                <mat-card-subtitle>\r\n                    {{ hiringRequestDetails.KnowledgeAndSkills }}\r\n                </mat-card-subtitle>\r\n                <table class=\"table table-striped\">\r\n                    <tbody>\r\n                        <tr>\r\n                            <td>Education Degree</td>\r\n                            <td>{{ hiringRequestDetails.EducationDegree }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Profession</td>\r\n                            <td>{{ hiringRequestDetails.Profession }}</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td>Experience(In Years)</td>\r\n                            <td>{{ hiringRequestDetails.Experience }}</td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n            <mat-tab-group>\r\n                <mat-tab label=\"New Candidates\">\r\n                    <h5>\r\n                        Candidates\r\n                        <hum-button *ngIf=\"!IsHiringRequestCompleted && !IsHiringRequestClosed\" [type]=\"'add'\" [text]=\"'ADD'\" (click)=\"addNewCandidate()\"></hum-button>\r\n                        <hum-button [type]=\"'download'\" [text]=\"'PDF Export'\" (click)=\"onExportPdf()\"></hum-button>\r\n                        &nbsp;&nbsp;&nbsp;&nbsp;\r\n\r\n                        <mat-form-field>\r\n                            <mat-select placeholder=\"Status Filter\" (selectionChange)=\"onStatusFilterCandidate($event)\" name=\"statusFilter\" multiple [(ngModel)]=\"CandidateStatusSelection\">\r\n                                <mat-option *ngFor=\"let item of statusFilter\" [value]=\"item.Id\">{{ item.value }}\r\n                                </mat-option>\r\n                            </mat-select>\r\n                        </mat-form-field>\r\n                    </h5>\r\n                    <app-candidate-table [headers]=\"newCandidatesHeaders$\" [isDefaultAction]=\"false\" [items]=\"newCandidatesList2$\" [subHeaders]=\"subListHeaders$\" [subTitle]=\"'Other Details'\" [actions]=\"actions\" (actionClick)=\"newCandActionEvents($event)\"></app-candidate-table>\r\n                </mat-tab>\r\n                <mat-tab label=\"Existing Candidates\">\r\n                    <h5>\r\n                        Candidates &nbsp;&nbsp;&nbsp;&nbsp;\r\n                        <mat-form-field *ngIf=\"!IsHiringRequestCompleted && !IsHiringRequestClosed\">\r\n                            <mat-select placeholder=\"Select Existing Employee'\" (selectionChange)=\"OnExistingEmployeeSelection($event)\" name=\"Employee\">\r\n                                <mat-option *ngFor=\"let item of existingEmployeesList\" [value]=\"item.Id\">{{ item.value }}\r\n                                </mat-option>\r\n                            </mat-select>\r\n                        </mat-form-field>\r\n                        &nbsp;&nbsp;&nbsp;&nbsp;\r\n                        <mat-form-field>\r\n                            <mat-select placeholder=\"Status Filter\" (selectionChange)=\"onStatusFilter($event)\" name=\"statusFilter\" multiple [(ngModel)]=\"EmployeeStatusSelection\">\r\n                                <mat-option *ngFor=\"let item of statusFilter\" [value]=\"item.Id\">{{ item.value }}\r\n                                </mat-option>\r\n                            </mat-select>\r\n                        </mat-form-field>\r\n                    </h5>\r\n                    <hum-table [headers]=\"existingCandidatesHeaders$\" [isDefaultAction]=\"false\" [items]=\"existingCandidatesList$\" [actions]=\"actions\" (actionClick)=\"empActionEvents($event)\"></hum-table>\r\n                </mat-tab>\r\n            </mat-tab-group>\r\n        </div>\r\n    </div>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.scss":
/*!**********************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.scss ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".custom-card {\n  margin-top: 20px; }\n\n.bckicon {\n  cursor: pointer;\n  font-size: 15px;\n  font-weight: lighter;\n  margin-right: 5px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL3Byb2plY3QtbWFuYWdlbWVudC9wcm9qZWN0LWhpcmluZy9yZXF1ZXN0LWRldGFpbC9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxkYXNoYm9hcmRcXHByb2plY3QtbWFuYWdlbWVudFxccHJvamVjdC1oaXJpbmdcXHJlcXVlc3QtZGV0YWlsXFxyZXF1ZXN0LWRldGFpbC5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNJLGdCQUFnQixFQUFBOztBQUdwQjtFQUNJLGVBQWU7RUFDZixlQUFlO0VBQ2Ysb0JBQW9CO0VBQ3BCLGlCQUFpQixFQUFBIiwiZmlsZSI6InNyYy9hcHAvZGFzaGJvYXJkL3Byb2plY3QtbWFuYWdlbWVudC9wcm9qZWN0LWhpcmluZy9yZXF1ZXN0LWRldGFpbC9yZXF1ZXN0LWRldGFpbC5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5jdXN0b20tY2FyZCB7XHJcbiAgICBtYXJnaW4tdG9wOiAyMHB4O1xyXG59XHJcblxyXG4uYmNraWNvbiB7XHJcbiAgICBjdXJzb3I6IHBvaW50ZXI7XHJcbiAgICBmb250LXNpemU6IDE1cHg7XHJcbiAgICBmb250LXdlaWdodDogbGlnaHRlcjtcclxuICAgIG1hcmdpbi1yaWdodDogNXB4O1xyXG59Il19 */"

/***/ }),

/***/ "./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.ts":
/*!********************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.ts ***!
  \********************************************************************************************************/
/*! exports provided: RequestDetailComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RequestDetailComponent", function() { return RequestDetailComponent; });
/* harmony import */ var _add_analytical_info_add_analytical_info_component__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./../add-analytical-info/add-analytical-info.component */ "./src/app/dashboard/project-management/project-hiring/add-analytical-info/add-analytical-info.component.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../project-list/hiring-requests/hiring-requests.service */ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _add_hiring_request_add_hiring_request_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../add-hiring-request/add-hiring-request.component */ "./src/app/dashboard/project-management/project-hiring/add-hiring-request/add-hiring-request.component.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _add_new_candidate_add_new_candidate_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ../add-new-candidate/add-new-candidate.component */ "./src/app/dashboard/project-management/project-hiring/add-new-candidate/add-new-candidate.component.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};















var RequestDetailComponent = /** @class */ (function () {
    function RequestDetailComponent(dialog, globalSharedService, appurl, toastr, routeActive, hiringRequestService, loader, router) {
        this.dialog = dialog;
        this.globalSharedService = globalSharedService;
        this.appurl = appurl;
        this.toastr = toastr;
        this.routeActive = routeActive;
        this.hiringRequestService = hiringRequestService;
        this.loader = loader;
        this.router = router;
        this.statusFilter = [
            { Id: 0, value: 'Pending Shortlist' },
            { Id: 1, value: 'Pending Interview' },
            { Id: 2, value: 'Pending Selection' },
            { Id: 3, value: 'Selected' },
            { Id: 4, value: 'Rejected' }
        ];
        this.newCandidatesHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])([
            'Candidate Id',
            'First Name',
            'Last Name',
            'Gender',
            'Interview',
            'Candidate Status'
        ]);
        this.subListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])([
            'Education',
            'Phone Number',
            'Profession',
            'Email Address',
            'Relevant Experience',
            'Irrelevant Experience',
            'Total Experience'
        ]);
        this.existingCandidatesHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])([
            'Employee Id',
            'Employee Code',
            'Full Name',
            'Gender',
            'Employee Status'
        ]);
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_2__["ReplaySubject"](1);
        this.existingEmployeesList = [];
        this.IsHiringRequestCompleted = false;
        this.IsHiringRequestClosed = false;
        this.CandidateStatusSelection = [0, 1, 2, 4];
        this.EmployeeStatusSelection = [0, 1, 2, 4];
        this.filterValueModel = {
            pageIndex: 0,
            pageSize: 10,
            TotalCount: null,
            FilterValue: '',
            ProjectId: null,
            HiringRequestId: null
        };
    }
    RequestDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.hiringRequestDetails = {
            HiringRequestId: null,
            JobGrade: '',
            Position: '',
            TotalVacancies: '',
            FilledVacancies: '',
            PayCurrency: '',
            PayRate: '',
            Status: '',
            Office: '',
            JobType: '',
            JobCategory: '',
            BudgetName: '',
            AnouncingDate: null,
            ClosingDate: null,
            ContractType: '',
            ContractDuration: null,
            Shift: '',
            EducationDegree: '',
            Profession: '',
            Experience: '',
            KnowledgeAndSkills: '',
            HiringRequestStatus: null,
            SpecificDutiesAndResponsibilities: '',
            SubmissionGuidelines: '',
            HiringRequestCode: '',
            BudgetLineId: null
        };
        this.routeActive.params.subscribe(function (params) {
            _this.hiringRequestId = +params['id'];
        });
        this.routeActive.parent.parent.parent.params.subscribe(function (params) {
            _this.projectId = +params['id'];
        });
        this.actions = {
            items: {
                button: { status: true, text: '' },
                delete: false,
                download: false,
                edit: false
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false
            }
        };
        this.completeRequestModel = {
            HiringRequestId: [],
            ProjectId: this.projectId
        };
        this.getHiringRequestDetailsByHiringRequestId();
        this.getExistingEmployeeDropDownList();
        this.getScreenSize();
    };
    //#region "Dynamic Scroll"
    RequestDetailComponent.prototype.getScreenSize = function () {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 130 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    //#region "get Hiring Request Details By HiringRequestId"
    RequestDetailComponent.prototype.getHiringRequestDetailsByHiringRequestId = function () {
        var _this = this;
        if (this.hiringRequestId != null && this.hiringRequestId !== undefined) {
            this.loader.showLoader();
            this.hiringRequestService
                .GetProjectHiringRequestDetailsByHiringRequestId(this.hiringRequestId)
                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
                .subscribe(function (response) {
                if (response.statusCode === 200 && response.data !== null) {
                    _this.hiringRequestDetails = {
                        HiringRequestId: response.data.HiringRequestId,
                        JobGrade: response.data.JobGrade,
                        Position: response.data.Position,
                        TotalVacancies: response.data.TotalVacancies,
                        FilledVacancies: response.data.FilledVacancies,
                        PayCurrency: response.data.PayCurrency,
                        PayRate: response.data.PayRate,
                        Office: response.data.Office,
                        OfficeId: response.data.OfficeId,
                        JobType: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["JobType"][response.data.JobType],
                        JobCategory: response.data.JobCategory,
                        BudgetName: response.data.BudgetName,
                        AnouncingDate: response.data.AnouncingDate,
                        ClosingDate: response.data.ClosingDate,
                        ContractType: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["ContractType"][response.data.ContractType],
                        ContractDuration: response.data.ContractDuration,
                        Shift: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["Shift"][response.data.Shift],
                        EducationDegree: response.data.EducationDegree,
                        Profession: response.data.Profession,
                        Experience: response.data.Experience,
                        KnowledgeAndSkills: response.data.KnowledgeAndSkills,
                        HiringRequestStatus: response.data.HiringRequestStatus,
                        SpecificDutiesAndResponsibilities: response.data.SpecificDutiesAndResponsibilities,
                        SubmissionGuidelines: response.data.SubmissionGuidelines,
                        HiringRequestCode: response.data.HiringRequestCode,
                        BudgetLineId: response.data.BudgetLineId
                    };
                    if (_this.hiringRequestDetails.HiringRequestStatus ===
                        src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["HiringRequestStatus"].Completed) {
                        _this.IsHiringRequestCompleted = true;
                    }
                    else if (_this.hiringRequestDetails.HiringRequestStatus ===
                        src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["HiringRequestStatus"].Closed) {
                        _this.IsHiringRequestClosed = true;
                    }
                }
                _this.getAllCandidateList(_this.filterValueModel);
                _this.getAllExistingCandidateList(_this.filterValueModel);
                _this.loader.hideLoader();
            }, function () {
                _this.loader.hideLoader();
            });
        }
    };
    //#endregion
    // #region edit hiring request
    RequestDetailComponent.prototype.editHiringRequest = function () {
        var _this = this;
        // NOTE: It open AddHiringRequest (AddHiringRequestsComponent)
        var dialogRef = this.dialog.open(_add_hiring_request_add_hiring_request_component__WEBPACK_IMPORTED_MODULE_7__["AddHiringRequestComponent"], {
            width: '700px',
            autoFocus: false,
            data: {
                hiringRequestId: this.hiringRequestDetails.HiringRequestId,
                projectId: this.projectId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onUpdateHiringRequestListRefresh.subscribe(function () {
            _this.getHiringRequestDetailsByHiringRequestId();
        });
        dialogRef.afterClosed().subscribe(function () { });
    };
    //#endregion
    //#region "Download pdf of Hiring Request Details"
    RequestDetailComponent.prototype.onExportHiringRequestPdf = function () {
        this.loader.showLoader();
        var data = {
            HiringRequestId: this.hiringRequestId,
            ProjectId: this.projectId
        };
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_13__["GLOBAL"].API_Pdf_GetHiringRequestFormPdf, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe();
        this.loader.hideLoader();
    };
    //#endregion
    //#region Complete and close hiring request
    RequestDetailComponent.prototype.onCompleteRequest = function () {
        var _this = this;
        this.completeRequestModel = {
            HiringRequestId: [],
            ProjectId: this.projectId
        };
        this.completeRequestModel.HiringRequestId.push(this.hiringRequestId);
        this.hiringRequestService
            .IsCompltedeHrDetail(this.completeRequestModel)
            .subscribe(function (responseData) {
            if (responseData.statusCode === 200) {
                _this.toastr.success('Hiring Request Successfully Completed');
                _this.IsHiringRequestCompleted = true;
            }
            else if (responseData.statusCode === 400) {
                _this.toastr.error('Something went wrong .Please try again.');
            }
        }, function (error) { });
    };
    RequestDetailComponent.prototype.onCloseRequest = function () {
        var _this = this;
        this.completeRequestModel = {
            HiringRequestId: [],
            ProjectId: this.projectId
        };
        this.completeRequestModel.HiringRequestId.push(this.hiringRequestId);
        this.hiringRequestService
            .IsCloasedHrDetail(this.completeRequestModel)
            .subscribe(function (responseData) {
            if (responseData.statusCode === 200) {
                _this.IsHiringRequestClosed = true;
            }
            else if (responseData.statusCode === 400) {
                _this.toastr.error('Something went wrong .Please try again.');
            }
        }, function (error) { });
    };
    //#endregion
    //#region "get All Candidate List"
    RequestDetailComponent.prototype.getAllCandidateList = function (filter) {
        var _this = this;
        filter.ProjectId = this.projectId;
        filter.HiringRequestId = this.hiringRequestId;
        this.loader.showLoader();
        this.hiringRequestService.getAllCandidateList(filter).subscribe(function (response) {
            if (response.statusCode === 200 && response.data !== null) {
                _this.newCandidatesList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(response.data.map(function (element) {
                    return {
                        CandidateId: element.CandidateId,
                        FirstName: element.FirstName,
                        LastName: element.LastName,
                        Gender: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["Gender"][element.Gender],
                        Interview: element.InterviewId == 0
                            ? 'Not Interviewed'
                            : '<a href="/project/my-project/' +
                                _this.projectId +
                                '/hiring-request/interview-detail?candId=' +
                                element.CandidateId +
                                '&hiringId=' +
                                _this.hiringRequestId +
                                '&interviewId=' +
                                element.InterviewId +
                                '">Interview ' +
                                element.InterviewId +
                                '</a>',
                        CandidateStatus: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"][element.CandidateStatus],
                        itemAction: element.CandidateStatus !== src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"].Rejected &&
                            element.CandidateStatus !== src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"].Selected &&
                            !_this.IsHiringRequestCompleted &&
                            !_this.IsHiringRequestClosed
                            ? [
                                {
                                    button: {
                                        status: true,
                                        text: 'Candidate Cv',
                                        type: 'download'
                                    },
                                    delete: false,
                                    download: true,
                                    edit: false
                                },
                                {
                                    button: {
                                        status: true,
                                        text: 'Reject',
                                        type: 'cancel'
                                    },
                                    delete: false,
                                    download: false,
                                    edit: false
                                },
                                {
                                    button: {
                                        status: true,
                                        text: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateAction"][element.CandidateStatus],
                                        type: 'save'
                                    },
                                    delete: false,
                                    download: false,
                                    edit: false
                                }
                            ]
                            : element.CandidateStatus === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"].Selected
                                ? [
                                    {
                                        anchor: {
                                            status: true,
                                            text: element.EmployeeCode + '-' + element.EmployeeName,
                                            type: 'link'
                                        },
                                        delete: false,
                                        download: false,
                                        edit: false,
                                        link: true
                                    }
                                ]
                                : [],
                        subItems: [
                            {
                                EducationDegree: element.EducationDegree,
                                PhoneNumber: element.PhoneNumber,
                                Profession: element.Profession,
                                Email: element.Email,
                                RelevantExperienceInYear: element.RelevantExperienceInYear,
                                IrrelevantExperienceInYear: element.IrrelevantExperienceInYear,
                                TotalExperienceInYear: element.RelevantExperienceInYear +
                                    element.IrrelevantExperienceInYear
                            }
                        ]
                    };
                }));
                _this.newCandidatesList2$ = _this.newCandidatesList$;
                _this.newCandidatesList$.subscribe(function (res) {
                    _this.newCandidatesList2$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res.filter(function (x) {
                        return _this.CandidateStatusSelection.includes(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"][x.CandidateStatus]);
                    }));
                });
            }
            _this.loader.hideLoader();
        }, function () {
            _this.loader.hideLoader();
        });
    };
    //#endregion
    // #region adding new candidate
    RequestDetailComponent.prototype.addNewCandidate = function () {
        var _this = this;
        // let candidateCount;
        // this.newCandidatesList$.subscribe(res => {
        //   candidateCount = res.filter(x => CandidateStatus[x.CandidateStatus] === CandidateStatus.Selected);
        // });
        if (this.hiringRequestDetails.FilledVacancies <
            this.hiringRequestDetails.TotalVacancies) {
            // NOTE: It open AddHiringRequest dialog and passed the data into the AddHiringRequestsComponent Model
            var dialogRef = this.dialog.open(_add_new_candidate_add_new_candidate_component__WEBPACK_IMPORTED_MODULE_9__["AddNewCandidateComponent"], {
                width: '700px',
                autoFocus: false,
                data: {
                    hiringRequestId: this.hiringRequestDetails.HiringRequestId,
                    projectId: this.projectId
                }
            });
            // refresh the list after new request created
            dialogRef.componentInstance.onAddCandidateListRefresh.subscribe(function () {
                _this.getAllCandidateList(_this.filterValueModel);
            });
            dialogRef.afterClosed().subscribe(function () { });
        }
        else {
            this.toastr.warning('Vacancies Already Filled');
        }
    };
    //#endregion
    // #region Changes Status of New candidate
    RequestDetailComponent.prototype.newCandActionEvents = function (data) {
        switch (data.type) {
            case 'Candidate Cv':
                this.getCandidateCvByCandidateId(data);
                break;
            case 'Select':
                this.selectCandidate(data);
                break;
            case 'Reject':
                this.rejectCandidate(data);
                break;
            case 'Shortlist':
                this.shortListCandidate(data);
                break;
            case 'Interview':
                this.router.navigate(['interview-detail'], {
                    relativeTo: this.routeActive.parent,
                    queryParams: {
                        candId: data.item.CandidateId,
                        hiringId: this.hiringRequestId,
                        projectId: this.projectId,
                        hiringRequestId: this.hiringRequestId
                    }
                });
                break;
            default:
                var id = data.type.split('-');
                id = id[0];
                id = id.substring(1);
                window.open(this.appurl.getOldUiUrl() +
                    'dashboard/hr/employees?empCode=' +
                    id +
                    '&officeId=' +
                    this.hiringRequestDetails.OfficeId, '_blank');
                break;
        }
    };
    RequestDetailComponent.prototype.shortListCandidate = function (data) {
        var candidateDetails = {
            statusId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"]['Pending Interview'],
            candidateId: data.item.CandidateId,
            projectId: this.projectId,
            hiringRequestId: this.hiringRequestId
        };
        this.updateCandidateStatus(candidateDetails);
    };
    RequestDetailComponent.prototype.selectCandidate = function (data) {
        var candidateDetails = {
            statusId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"].Selected,
            candidateId: data.item.CandidateId,
            projectId: this.projectId,
            hiringRequestId: this.hiringRequestId
        };
        this.updateCandidateStatus(candidateDetails);
        this.AddCandidateAsEmployee(candidateDetails);
    };
    RequestDetailComponent.prototype.rejectCandidate = function (data) {
        var candidateDetails = {
            statusId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"].Rejected,
            candidateId: data.item.CandidateId,
            projectId: this.projectId,
            hiringRequestId: this.hiringRequestId
        };
        this.updateCandidateStatus(candidateDetails);
    };
    RequestDetailComponent.prototype.getCandidateCvByCandidateId = function (data) {
        var _this = this;
        var candidateId = data.item.CandidateId;
        if (candidateId != null && candidateId !== undefined) {
            this.loader.showLoader();
            this.hiringRequestService
                .DownloadCandidateCvByRequestId(candidateId)
                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
                .subscribe(function (response) {
                if (response.statusCode === 200 && response.data !== null) {
                    _this.candidateCv = {
                        AttachmentName: response.data.AttachmentName,
                        AttachmentUrl: response.data.AttachmentUrl,
                        UploadedBy: response.data.UploadedBy
                    };
                    var anchor = document.createElement('a');
                    anchor.href = _this.candidateCv.AttachmentUrl;
                    anchor.target = '_blank';
                    anchor.click();
                }
                _this.loader.hideLoader();
            }, function () {
                _this.loader.hideLoader();
            });
        }
    };
    //#endregion
    // #region Filter New Candidate list by their status
    RequestDetailComponent.prototype.onStatusFilter = function (data) {
        var _this = this;
        if (data.value == '') {
            this.getAllExistingCandidateList(this.filterValueModel);
        }
        else {
            this.existingCandidatesList2$.subscribe(function (res) {
                _this.existingCandidatesList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res.filter(function (x) {
                    return data.value.includes(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"][x.CandidateStatus]);
                }));
            });
        }
    };
    //#endregion
    //#region "Download pdf of shortlist candidate"
    RequestDetailComponent.prototype.onExportPdf = function () {
        this.loader.showLoader();
        var data = {
            HiringRequestId: this.hiringRequestId,
            ProjectId: this.projectId
        };
        var IsHavingCandidate;
        this.newCandidatesList$.subscribe(function (element) {
            IsHavingCandidate = element.find(function (x) { return x.CandidateStatus === 'Pending Interview'; });
        });
        if (IsHavingCandidate) {
            this.globalSharedService
                .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_13__["GLOBAL"].API_Pdf_GetCandidateDetailReportPdf, data)
                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
                .subscribe();
        }
        else {
            this.toastr.warning('Not Having Shortlist Candidates');
        }
        this.loader.hideLoader();
    };
    //#endregion
    //#region "get All Existing Candidate List"
    RequestDetailComponent.prototype.getAllExistingCandidateList = function (filter) {
        var _this = this;
        filter.ProjectId = this.projectId;
        filter.HiringRequestId = this.hiringRequestId;
        this.loader.showLoader();
        this.hiringRequestService.GetAllExistingCandidateList(filter).subscribe(function (response) {
            if (response.statusCode === 200 && response.data !== null) {
                _this.existingCandidatesList2$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(response.data.map(function (element) {
                    return {
                        EmployeeId: element.EmployeeId,
                        EmployeeCode: element.EmployeeCode,
                        FullName: element.FullName,
                        Gender: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["Gender"][element.Gender],
                        CandidateStatus: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"][element.CandidateStatus],
                        itemAction: element.CandidateStatus != src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"].Rejected &&
                            element.CandidateStatus != src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"].Selected &&
                            !_this.IsHiringRequestCompleted &&
                            !_this.IsHiringRequestClosed
                            ? [
                                {
                                    button: {
                                        status: true,
                                        text: 'Reject',
                                        type: 'cancel'
                                    },
                                    delete: false,
                                    download: false,
                                    edit: false
                                },
                                {
                                    button: {
                                        status: true,
                                        text: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateAction"][element.CandidateStatus],
                                        type: 'save'
                                    },
                                    delete: false,
                                    download: false,
                                    edit: false
                                }
                            ]
                            : []
                    };
                }));
                _this.existingCandidatesList$ = _this.existingCandidatesList2$;
                _this.existingCandidatesList2$.subscribe(function (res) {
                    _this.existingCandidatesList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res.filter(function (x) {
                        return _this.EmployeeStatusSelection.includes(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"][x.CandidateStatus]);
                    }));
                });
            }
            _this.loader.hideLoader();
        }, function () {
            _this.loader.hideLoader();
        });
    };
    //#endregion
    //#region "get Existing Employee DropDown List"
    RequestDetailComponent.prototype.getExistingEmployeeDropDownList = function () {
        var _this = this;
        var model = {
            ProjectId: this.projectId,
            HiringRequestId: this.hiringRequestId
        };
        this.hiringRequestService.GetAllEmployeeList(model).subscribe(function (response) {
            _this.loader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.existingEmployeesList.push({
                        Id: element.EmployeeId,
                        value: element.EmployeeName
                    });
                });
            }
            _this.loader.hideLoader();
        }, function (error) {
            _this.loader.hideLoader();
        });
    };
    //#endregion
    // #region adding Existing Employee for hiring request
    RequestDetailComponent.prototype.OnExistingEmployeeSelection = function (data) {
        var _this = this;
        this.existingCandidatesList2$.subscribe(function (res) {
            if (res.findIndex(function (x) { return x.EmployeeId == data.value; }) > -1) {
                _this.toastr.warning('Employee already selected');
            }
            else {
                _this.loader.showLoader();
                var candidateDetails = {
                    HiringRequestId: _this.hiringRequestId,
                    ProjectId: _this.projectId,
                    EmployeeId: data.value
                };
                _this.hiringRequestService
                    .AddExistingCandidateDetail(candidateDetails)
                    .subscribe(function (response) {
                    if (response.statusCode === 200) {
                        _this.getAllExistingCandidateList(_this.filterValueModel);
                        _this.toastr.success('Employee successfully added');
                        _this.loader.hideLoader();
                    }
                    else {
                        _this.toastr.error(response.message);
                        _this.loader.hideLoader();
                    }
                }, function (error) {
                    _this.toastr.error('Someting went wrong. Please try again');
                    _this.loader.hideLoader();
                });
            }
        });
    };
    //#endregion
    // #region Changes Status of existing candidate
    RequestDetailComponent.prototype.empActionEvents = function (data) {
        switch (data.type) {
            case 'Select':
                this.AddAnalyticalInfo(data);
                // this.selectEmployee(data);
                break;
            case 'Reject':
                this.rejectEmployee(data);
                break;
            default:
                break;
        }
    };
    RequestDetailComponent.prototype.selectEmployee = function (data) {
        var candidateDetails = {
            statusId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"].Selected,
            // statusId: +CandidateStatus[data.item.CandidateStatus],
            employeeId: data.item.EmployeeId,
            projectId: this.projectId,
            hiringRequestId: this.hiringRequestId
        };
        this.updateCandidateStatus(candidateDetails);
    };
    RequestDetailComponent.prototype.rejectEmployee = function (data) {
        var candidateDetails = {
            statusId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"].Rejected,
            employeeId: data.item.EmployeeId,
            projectId: this.projectId,
            hiringRequestId: this.hiringRequestId
        };
        this.updateCandidateStatus(candidateDetails);
    };
    //#endregion
    RequestDetailComponent.prototype.AddCandidateAsEmployee = function (model) {
        var _this = this;
        // this.loader.showLoader();
        this.hiringRequestService
            .AddCandidateAsEmployee(model)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            if (response.statusCode === 200) {
                _this.updateCandidateStatus(model);
                _this.getAllCandidateList(_this.filterValueModel);
                _this.loader.hideLoader();
            }
            else {
                _this.toastr.error(response.message);
                _this.loader.hideLoader();
            }
        }, function (error) {
            _this.toastr.error('Someting went wrong. Please try again');
            _this.loader.hideLoader();
        });
    };
    // #region Filter Existing Candidate list by their status
    RequestDetailComponent.prototype.onStatusFilterCandidate = function (data) {
        var _this = this;
        if (data.value == '') {
            this.getAllCandidateList(this.filterValueModel);
        }
        else {
            this.newCandidatesList$.subscribe(function (res) {
                _this.newCandidatesList2$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res.filter(function (x) {
                    return data.value.includes(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"][x.CandidateStatus]);
                }));
            });
        }
    };
    //#endregion
    //#region "Update Status of new and Existing candidates"
    RequestDetailComponent.prototype.updateCandidateStatus = function (candidateDetails) {
        var _this = this;
        this.loader.showLoader();
        this.hiringRequestService
            .UpdateCandidateStatus(candidateDetails)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.loader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
                var data_1 = response.data;
                if (data_1.EmployeeID) {
                    _this.existingCandidatesList$.subscribe(function (res) {
                        var index = res.findIndex(function (x) { return x.EmployeeId == data_1.EmployeeID; });
                        var employee = res.find(function (x) { return x.EmployeeId == data_1.EmployeeID; });
                        employee.CandidateStatus =
                            src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["CandidateStatus"][data_1.CandidateStatus];
                        employee.itemAction = [];
                        res[index] = employee;
                        _this.existingCandidatesList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res);
                    });
                }
                else {
                    _this.getAllCandidateList(_this.filterValueModel);
                }
            }
            _this.loader.hideLoader();
        }, function () {
            _this.loader.hideLoader();
        });
    };
    //#endregion
    // #region add analytical info
    RequestDetailComponent.prototype.AddAnalyticalInfo = function (CandidateData) {
        var _this = this;
        // NOTE: It open AddAnalyticalInfo dialog and passed the data into the AddAnalyticalInfoComponent Model
        var dialogRef = this.dialog.open(_add_analytical_info_add_analytical_info_component__WEBPACK_IMPORTED_MODULE_0__["AddAnalyticalInfoComponent"], {
            width: '800px',
            autoFocus: false,
            data: {
                hiringRequestId: this.hiringRequestDetails.HiringRequestId,
                projectId: this.projectId,
                employeeId: CandidateData.item.EmployeeId,
                budgetLineId: this.hiringRequestDetails.BudgetLineId
            }
        });
        // refresh the list after new request created
        dialogRef.componentInstance.onAddAnalyticalInfoRefresh.subscribe(function () {
            _this.selectEmployee(CandidateData);
        });
        dialogRef.afterClosed().subscribe(function () {
            '';
        });
    };
    //#endregion
    //#region Navigate back to hiring requset list page
    RequestDetailComponent.prototype.backToList = function () {
        window.history.back();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", []),
        __metadata("design:returntype", void 0)
    ], RequestDetailComponent.prototype, "getScreenSize", null);
    RequestDetailComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-request-detail',
            template: __webpack_require__(/*! ./request-detail.component.html */ "./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.html"),
            styles: [__webpack_require__(/*! ./request-detail.component.scss */ "./src/app/dashboard/project-management/project-hiring/request-detail/request-detail.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_8__["MatDialog"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_11__["GlobalSharedService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_12__["AppUrlService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_14__["ToastrService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_5__["ActivatedRoute"],
            _project_list_hiring_requests_hiring_requests_service__WEBPACK_IMPORTED_MODULE_4__["HiringRequestsService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__["CommonLoaderService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_5__["Router"]])
    ], RequestDetailComponent);
    return RequestDetailComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts":
/*!******************************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-list/hiring-requests/hiring-requests.service.ts ***!
  \******************************************************************************************************/
/*! exports provided: HiringRequestsService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HiringRequestsService", function() { return HiringRequestsService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var HiringRequestsService = /** @class */ (function () {
    function HiringRequestsService(globalService, appurl) {
        this.globalService = globalService;
        this.appurl = appurl;
        this.hiringPermissionSubject = new rxjs__WEBPACK_IMPORTED_MODULE_5__["BehaviorSubject"]([]);
    }
    HiringRequestsService.prototype.setHiringPermissions = function (permissionList) {
        this.hiringPermissionSubject.next(permissionList);
    };
    //#region "GetCurrencyList"
    HiringRequestsService.prototype.GetCurrencyList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllCurrency)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.CurrencyList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetBudgetLineList"
    HiringRequestsService.prototype.GetBudgetLineList = function (projectId) {
        return this.globalService
            .getListById(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetProjectBudgetLineDetail, projectId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectBudgetLineDetailList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetAllBudgetLineList"
    HiringRequestsService.prototype.GetAllBudgetLineList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetAllProjectBudgetLineDetail)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectBudgetLineDetailList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetOfficeList"
    HiringRequestsService.prototype.GetOfficeList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllOffice)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.OfficeDetailsList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetProfessionList"
    HiringRequestsService.prototype.GetProfessionList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Code_GetAllProfession)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProfessionList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#region "GetEducationDegreeList"
    HiringRequestsService.prototype.GetEducationDegreeList = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Code_GetAllEducationDegreeList);
    };
    //#region "GetJobGradeList"
    HiringRequestsService.prototype.GetJobGradeList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HREmployee_GetAllJobGrade)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.JobGradeList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "getAllProvinceListByCountryId"
    HiringRequestsService.prototype.getAllProvinceListByCountryId = function (Id) {
        return this.globalService
            .getListByListId(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetAllProvinceDetailsByCountryId, Id)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProvinceDetailsList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetAllDistrictvalueByProvinceId"
    HiringRequestsService.prototype.GetAllDistrictvalueByProvinceId = function (id) {
        return this.globalService
            .getListByListId(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetAllDistrictvalueByProvinceId, id)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.Districtlist,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "AddHiringRequestDetail"
    HiringRequestsService.prototype.AddHiringRequestDetail = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_AddHiringRequestDetail, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "EditHiringRequestDetail"
    HiringRequestsService.prototype.EditHiringRequestDetail = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_EditHiringRequestDetail, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "AddNewCandidateDetail"
    HiringRequestsService.prototype.AddNewCandidateDetail = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_AddNewCandidateDetail, data);
    };
    //#endregion
    //#region "AddExistingCandidateDetail"
    HiringRequestsService.prototype.AddExistingCandidateDetail = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_AddExistingCandidateDetail, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "getAllCandidateList"
    HiringRequestsService.prototype.getAllCandidateList = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetAllCandidateList, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.CandidateList,
                total: x.data.TotalCount,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetAllExistingCandidateList"
    HiringRequestsService.prototype.GetAllExistingCandidateList = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetAllExistingCandidateList, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ExistingCandidateList,
                total: x.data.TotalCount,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "UpdateCandidateStatus"
    HiringRequestsService.prototype.UpdateCandidateStatus = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_UpdateCandidateStatusByStatusId, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.CandidateStatus,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "EditCandidateDetail"
    HiringRequestsService.prototype.EditCandidateDetail = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HREmployee_EditEmployeeProfessionalDetail, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetProjectActivityAdvanceFilterList"
    HiringRequestsService.prototype.GetProjectHiringRequestFilterList = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetProjectHiringRequestDetail, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectHiringRequestModel,
                total: x.data.TotalCount,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetAllEmployeeList"
    HiringRequestsService.prototype.GetAllEmployeeList = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetAllEmployeeList, model)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.EmployeeDetailListData,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetEmployeeListByOfficeId"
    HiringRequestsService.prototype.GetEmployeeListByOfficeId = function (OfficeId) {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetEmployeeListByOfficeId +
            '?OfficeId=' +
            OfficeId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.EmployeeDetailListData,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetAllEmployeeList"
    HiringRequestsService.prototype.GetAllAttendanceGroupList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Code_GetAttendanceGroupst)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.AttendanceGroupMasterList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetAllEmloyeeContractList"
    HiringRequestsService.prototype.GetAllEmloyeeContractList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HREmployee_GetAllEmployeeContractType)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.EmployeeContractTypeList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "AddbudgetLineDetailList"
    HiringRequestsService.prototype.AddHiringRequestCandidate = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_AddHiringRequestCandidate, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    HiringRequestsService.prototype.EditHirinigRequestCandidateDEtail = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_EditHiringRequestCandidate, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#region edit selected candidate detail
    HiringRequestsService.prototype.EditSelectedCandidateDEtail = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_HiringRequestSelectCandidate, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.ResponseData,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetRequestedCandidateById"
    HiringRequestsService.prototype.GetRequestedCandidateById = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetHiringCandidatesListById, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectHiringCandidateDetailModel,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "AddbudgetLineDetailList"
    HiringRequestsService.prototype.AddInterViewCandidateDetail = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_AddCandidateInterviewDetail, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region edit selected candidate detail
    HiringRequestsService.prototype.IsCompltedeHrDetail = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_CompleteHiringRequest, model)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.ResponseData,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region edit selected candidate detail
    HiringRequestsService.prototype.IsCloasedHrDetail = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_ClosedHiringRequest, model)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.ResponseData,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "DeleteCandidateDetailDetail"
    HiringRequestsService.prototype.DeleteCandidateDetailDetail = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_DeleteCandidatDetail, model)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.ResponseData,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetCountryList"
    HiringRequestsService.prototype.GetCountryList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetAllCountryDetails)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.CountryDetailsList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetProvinceList"
    HiringRequestsService.prototype.GetProvinceList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetAllProvinceDetails)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProvinceDetailsList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "AddJobHiringDetails"
    HiringRequestsService.prototype.addJobHiringDetails = function (data) {
        console.log(data);
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HREmployee_AddJobHiringDetail, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetProjectHiringRequestDetailsByHiringRequestId"
    HiringRequestsService.prototype.GetProjectHiringRequestDetailsByHiringRequestId = function (HiringRequestId) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetProjectHiringRequestDetailsByHiringRequestId, HiringRequestId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.ResponseData,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetAllProjectHiringRequestDetailByHiringRequestId"
    HiringRequestsService.prototype.GetAllProjectHiringRequestDetailByHiringRequestId = function (HiringRequestId) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetAllProjectHiringRequestDetailByHiringRequestId, HiringRequestId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectHiringRequestAllDetail,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetRatingBasedCriteriaQuestion"
    HiringRequestsService.prototype.GetRatingBasedCriteriaQuestion = function (OfficeId) {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Code_GetRatingBasedCriteriaQuestions +
            '?OfficeId=' +
            OfficeId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.RatingBasedCriteriaQuestionList,
                statusCode: x.StatusCode
                // message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetTechnicalQuestionsByDesignationId"
    HiringRequestsService.prototype.GetTechnicalQuestionsByDesignationId = function (DesignationId) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetTechnicalQuestionsByDesignationId, DesignationId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.TechnicalQuestionsList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetCandidateDetailsByCandidateId"
    HiringRequestsService.prototype.GetCandidateDetailsByCandidateId = function (CandidateId) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetCandidateDetailsByCandidateId, CandidateId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.CandidateDetails,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetProjectHiringRequestDetailsByHiringRequestId"
    HiringRequestsService.prototype.GetAllHiringRequestDetailForInterviewByHiringRequestId = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetAllHiringRequestDetailForInterviewByHiringRequestId, model)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.HiringRequestDetails,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "AddInterviewDetails"
    HiringRequestsService.prototype.AddInterviewDetails = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_AddInterviewDetails, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "AddInterviewDetails"
    HiringRequestsService.prototype.EditInterviewDetails = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_EditInterviewDetails, data)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetCandidateDetailsByCandidateId"
    HiringRequestsService.prototype.GetInterviewDetailsByInterviewId = function (InterviewId) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetInterviewDetailsByInterviewId, InterviewId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.InterviewDetails,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "getDesignationList"
    HiringRequestsService.prototype.getDesignationList = function () {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Code_GetAllDesignationList)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.ResponseData,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "getDepartmentList"
    HiringRequestsService.prototype.getDepartmentList = function (officeId) {
        return this.globalService.getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Code_GetDepartmentsByOfficeId +
            '?officeId=' +
            officeId);
    };
    HiringRequestsService.prototype.GetHiringRequestCode = function (ProjectId) {
        return this.globalService
            .getListByListId(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetHiringRequestCode, ProjectId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.HiringRequestCode,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "DownloadCandidateCvByRequestId"
    HiringRequestsService.prototype.DownloadCandidateCvByRequestId = function (CandidateId) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_DownloadCandidateCvByRequestId, CandidateId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.CandidateCvDownload,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "AddCandidateAsEmployee"
    HiringRequestsService.prototype.AddCandidateAsEmployee = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_EmployeeDetail_AddCandidateAsEmployee, model).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    HiringRequestsService.prototype.GetAnalyticalInfoByEmployeeId = function (EmployeeId) {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_EmployeeDetail_GetAllEmployeeSalaryAnalyticalInfo +
            '?EmployeeId=' +
            EmployeeId).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.EmployeeSalaryAnalyticalInfoList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    HiringRequestsService.prototype.GetProjectList = function () {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_ProjectBudget_GetAllProjectDetail).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectDetailList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    HiringRequestsService.prototype.GetAccountList = function () {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Accounting_GetAccountList).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.AccountDetailList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    HiringRequestsService.prototype.AddAnalyticalInfo = function (model) {
        return this.globalService.post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_EmployeeDetail_AddEmployeeSalaryAnalyticalInfo, model).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    HiringRequestsService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"]])
    ], HiringRequestsService);
    return HiringRequestsService;
}());



/***/ })

}]);
//# sourceMappingURL=default~project-hiring-project-hiring-module~project-management-project-management-module.js.map