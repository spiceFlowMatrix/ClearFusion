(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["chart-of-accounts-chart-of-accounts-module"],{

/***/ "./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.html":
/*!***********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.html ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>\r\n    Add New Account\r\n  </h1>\r\n  <hr />\r\n  <form class=\"example-form\" [formGroup]=\"addCharOfAccountForm\" (ngSubmit)=\"onFormSubmit(addCharOfAccountForm.value)\">\r\n    <div mat-dialog-content>\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-12\">\r\n          Account Code:\r\n          <span>{{parentAccountCode}} </span>\r\n\r\n          <input [min]=\"min\" [max]=\"max\" [maxlength]=\"maxlength\" formControlName=\"AccountCode\" appNumberOnly=\"true\" [ngClass]=\"{ 'is-invalid': submitted && f.AccountCode.errors }\" />\r\n          <div *ngIf=\"submitted && f.AccountCode.errors\" class=\"invalid-feedback\">\r\n              <div *ngIf=\"f.AccountCode.errors.required\" style=\"color:red\">Account Code is required</div>\r\n              <div *ngIf=\"f.AccountCode.errors.minLengthValidator\" style=\"color:red\">You must input 2 digit for this level</div>\r\n              <div *ngIf=\"f.AccountCode.errors.numValidator\" style=\"color:red\">Account Code must be numeric</div>\r\n            </div>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <br/>\r\n          <span>Account Name: </span>\r\n          <input formControlName=\"AccountName\" [ngClass]=\"{ 'is-invalid': submitted && f.AccountName.errors }\" />\r\n          <div *ngIf=\"submitted && f.AccountName.errors\" class=\"invalid-feedback\">\r\n              <div *ngIf=\"f.AccountName.errors.required\" style=\"color:red\">Account Name is required</div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n\r\n    </div>\r\n\r\n    <div class=\"row pull-right\" mat-dialog-actions>\r\n\r\n      <button mat-raised-button color=\"accent\" type=\"submit\" [disabled]=\"addAccountLoader\">\r\n        <span *ngIf=\"!addAccountLoader; else addAccountLoaderTemplate\">Save</span>\r\n        <ng-template #addAccountLoaderTemplate>\r\n          <div class=\"spinner_center padding_top_5 padding_bottom_5\">\r\n            <mat-spinner [diameter]=\"25\"> </mat-spinner>\r\n          </div>\r\n        </ng-template>\r\n      </button>\r\n      <button mat-raised-button [mat-dialog-close]=\"data.data\">\r\n        Cancel\r\n      </button>\r\n\r\n    </div>\r\n  </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.scss":
/*!***********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.scss ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "input, input:focus {\n  border: transparent;\n  box-shadow: none;\n  border-bottom: 1px solid; }\n\ninput:focus {\n  border: transparent;\n  box-shadow: none;\n  border-bottom: 1px solid transparent; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvY2hhcnQtb2YtYWNjb3VudHMvYWRkLWFjY291bnQvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcZGFzaGJvYXJkXFxhY2NvdW50aW5nXFxjaGFydC1vZi1hY2NvdW50c1xcYWRkLWFjY291bnRcXGFkZC1hY2NvdW50LmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksbUJBQW1CO0VBQ25CLGdCQUFnQjtFQUNoQix3QkFBd0IsRUFBQTs7QUFFNUI7RUFDSSxtQkFBbUI7RUFDbkIsZ0JBQWdCO0VBQ2hCLG9DQUFvQyxFQUFBIiwiZmlsZSI6InNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvY2hhcnQtb2YtYWNjb3VudHMvYWRkLWFjY291bnQvYWRkLWFjY291bnQuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyJpbnB1dCwgaW5wdXQ6Zm9jdXMge1xyXG4gICAgYm9yZGVyOiB0cmFuc3BhcmVudDsgXHJcbiAgICBib3gtc2hhZG93OiBub25lO1xyXG4gICAgYm9yZGVyLWJvdHRvbTogMXB4IHNvbGlkO1xyXG59XHJcbmlucHV0OmZvY3VzIHtcclxuICAgIGJvcmRlcjogdHJhbnNwYXJlbnQ7IFxyXG4gICAgYm94LXNoYWRvdzogbm9uZTtcclxuICAgIGJvcmRlci1ib3R0b206IDFweCBzb2xpZCB0cmFuc3BhcmVudDtcclxufSJdfQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.ts":
/*!*********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.ts ***!
  \*********************************************************************************************/
/*! exports provided: AddAccountComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddAccountComponent", function() { return AddAccountComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10__);
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




;







var AddAccountComponent = /** @class */ (function () {
    function AddAccountComponent(dialogRef, commonLoader, data, fb, toastr, globalService, appUrl) {
        this.dialogRef = dialogRef;
        this.commonLoader = commonLoader;
        this.data = data;
        this.fb = fb;
        this.toastr = toastr;
        this.globalService = globalService;
        this.appUrl = appUrl;
        this.addAccountLoader = false;
        this.submitted = false;
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_9__["ReplaySubject"](1);
        this.AccountHeadType = data.AccountHeadType;
        this.chartOfAccountList = data.AccountList;
        this.mainLevelData = data.mainLevelData;
        this.AccountLevel = data.AccountLevel;
        this.controlLevelData = data.controlLevelData;
        this.subLevelData = data.subLevelData;
        if (this.AccountLevel === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].MainLevel) {
            this.initForm(null);
            this.min = 1;
            this.max = 9;
            this.maxlength = 1;
        }
        else if (this.AccountLevel === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].ControlLevel) {
            this.initForm(this.mainLevelData.ChartOfAccountNewCode);
            this.min = 1;
            this.max = 9;
            this.maxlength = 1;
        }
        else if (this.AccountLevel === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].SubLevel) {
            this.initForm(this.controlLevelData.ChartOfAccountNewCode);
            this.min = 1;
            this.max = 99;
            this.maxlength = 2;
        }
        else if (this.AccountLevel === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].InputLevel) {
            this.initForm(this.subLevelData.ChartOfAccountNewCode);
            this.min = 1;
            this.max = 99;
            this.maxlength = 2;
        }
    }
    AddAccountComponent.prototype.ngOnInit = function () {
    };
    //#region "initForm"
    AddAccountComponent.prototype.initForm = function (AccountCode) {
        this.parentAccountCode = AccountCode;
        this.addCharOfAccountForm = this.fb.group({
            AccountCode: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required, this.minLengthValidator(this.AccountLevel)]],
            AccountName: [null, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            AccountHeadType: this.AccountHeadType,
            AccountLevel: this.AccountLevel
        });
    };
    Object.defineProperty(AddAccountComponent.prototype, "f", {
        //#endregion
        get: function () {
            return this.addCharOfAccountForm.controls;
        },
        enumerable: true,
        configurable: true
    });
    AddAccountComponent.prototype.minLengthValidator = function (accountType) {
        return function (control) {
            if ((accountType === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].SubLevel || accountType === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].InputLevel) && (control.value.length < 2)) {
                return { 'minLengthValidator': true };
            }
            var regexp = new RegExp('^[0-9]+$');
            if (!regexp.test(control.value) && (control.value !== '')) {
                return { 'numValidator': true };
            }
            return null;
        };
    };
    //#region "onFormSubmit"
    AddAccountComponent.prototype.onFormSubmit = function (data) {
        this.submitted = true;
        if (data.AccountLevel === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].MainLevel) {
            data.AccountCode = this.parentAccountCode != null ? this.parentAccountCode + data.AccountCode : data.AccountCode;
            this.addMainLevelAccountDetail(data);
        }
        else if (data.AccountLevel === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].ControlLevel) {
            data.AccountCode = this.parentAccountCode + data.AccountCode;
            this.addControlLevelAccountDetail(data);
        }
        else if (data.AccountLevel === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].SubLevel) {
            data.AccountCode = this.parentAccountCode + data.AccountCode;
            this.addSubLevelAccountDetail(data);
        }
        else if (data.AccountLevel === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].InputLevel) {
            data.AccountCode = this.parentAccountCode + data.AccountCode;
            this.addInputLevelAccountDetail(data);
        }
    };
    //#endregion
    //#region "addMainLevelAccountDetail"
    AddAccountComponent.prototype.addMainLevelAccountDetail = function (model) {
        var _this = this;
        if (this.addCharOfAccountForm.valid) {
            this.addAccountLoader = true;
            var count = this.chartOfAccountList.length;
            if (count < src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevelLimits"].MainLevel) {
                var obj_1 = {
                    ChartOfAccountNewId: 0,
                    AccountName: model.AccountName,
                    ParentID: model.ParentID,
                    AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].MainLevel,
                    AccountHeadTypeId: this.AccountHeadType,
                    AccountTypeId: model.AccountTypeId,
                    AccountFilterTypeId: model.AccountFilterTypeId,
                    ChartOfAccountNewCode: model.AccountCode
                };
                var item = this.chartOfAccountList.length - 1; // use to calculate the index
                this.globalService
                    .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_8__["GLOBAL"].API_ChartOfAccount_AddChartOfAccount, obj_1)
                    .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10__["takeUntil"])(this.destroyed$))
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        if (response.data.ChartOfAccountNewDetail !== null) {
                            var responseData = response.data.ChartOfAccountNewDetail;
                            obj_1.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
                            obj_1.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
                            obj_1.AccountName = responseData.AccountName;
                            obj_1.ParentID = responseData.ParentID;
                            obj_1.AccountLevelId = responseData.AccountLevelId;
                            obj_1.AccountHeadTypeId = responseData.AccountHeadTypeId;
                            obj_1.AccountTypeId = responseData.AccountTypeId;
                            obj_1.AccountFilterTypeId = responseData.AccountFilterTypeId;
                            obj_1.Children = [];
                            obj_1._IsDeleted = false;
                            obj_1._IsError = false;
                            obj_1._IsLoading = false;
                            _this.addAccountLoader = false;
                            // Update the Obj and Push into the list
                            _this.chartOfAccountList.push(obj_1);
                            _this.dialogRef.close();
                        }
                    }
                    else if (response.StatusCode === 400) {
                        _this.addAccountLoader = false;
                        _this.toastr.error(response.Message);
                    }
                }, function (error) {
                    _this.toastr.error('Something went wrong ! Try Again');
                    _this.addAccountLoader = false;
                });
            }
            else {
                this.toastr.error('Limit Exceeded');
                this.addAccountLoader = false;
            }
        }
    };
    //#endregion
    //#region "addControlLevelAccountDetail"
    AddAccountComponent.prototype.addControlLevelAccountDetail = function (model) {
        // Main Level
        var _this = this;
        if (this.addCharOfAccountForm.valid) {
            this.addAccountLoader = true;
            var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === _this.mainLevelData.ChartOfAccountNewId; });
            var mainLevelIndex_1 = this.chartOfAccountList.indexOf(mainLevelItem);
            var count = this.chartOfAccountList[mainLevelIndex_1].Children.length - 1;
            if (count < src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevelLimits"].ControlLevel) {
                var obj_2 = {
                    ChartOfAccountNewId: 0,
                    ChartOfAccountNewCode: model.AccountCode,
                    AccountName: model.AccountName,
                    ParentID: mainLevelItem.ChartOfAccountNewId,
                    AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].ControlLevel,
                    AccountHeadTypeId: this.AccountHeadType,
                    AccountTypeId: model.AccountTypeId,
                    AccountFilterTypeId: model.AccountFilterTypeId
                };
                // Error handling and loading handling
                var index = this.chartOfAccountList[mainLevelIndex_1].Children.length - 1; // use to calculate the index
                this.globalService
                    .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_8__["GLOBAL"].API_ChartOfAccount_AddChartOfAccount, obj_2)
                    .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10__["takeUntil"])(this.destroyed$))
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        if (response.data.ChartOfAccountNewDetail !== null) {
                            var responseData = response.data.ChartOfAccountNewDetail;
                            obj_2.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
                            obj_2.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
                            obj_2.AccountName = responseData.AccountName;
                            obj_2.ParentID = responseData.ParentID;
                            obj_2.AccountLevelId = responseData.AccountLevelId;
                            obj_2.AccountHeadTypeId = responseData.AccountHeadTypeId;
                            obj_2.AccountTypeId = responseData.AccountTypeId;
                            obj_2.AccountFilterTypeId = responseData.AccountFilterTypeId;
                            obj_2.Children = [];
                            obj_2._IsDeleted = false;
                            obj_2._IsError = false;
                            obj_2._IsLoading = false;
                            _this.addAccountLoader = false;
                            // Update the Obj and Push into the list
                            _this.chartOfAccountList[mainLevelIndex_1].Children.push(obj_2);
                            _this.dialogRef.close();
                        }
                    }
                    else if (response.StatusCode === 400) {
                        _this.toastr.error(response.Message);
                        _this.addAccountLoader = false;
                    }
                }, function (error) {
                    _this.toastr.error('Something went wrong ! Try Again');
                    _this.addAccountLoader = false;
                });
            }
            else {
                this.toastr.error('Limit Exceeded');
                this.addAccountLoader = false;
            }
        }
    };
    //#endregion
    //#region "addSubLevelAccountDetail"
    AddAccountComponent.prototype.addSubLevelAccountDetail = function (model) {
        var _this = this;
        if (this.addCharOfAccountForm.valid) {
            this.addAccountLoader = true;
            // Main Level
            var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === _this.mainLevelData.ChartOfAccountNewId; });
            var mainLevelIndex_2 = this.chartOfAccountList.indexOf(mainLevelItem);
            // Control Level
            var controlLevelItem = this.chartOfAccountList[mainLevelIndex_2].Children
                .find(function (x) { return x.ChartOfAccountNewId === _this.controlLevelData.ChartOfAccountNewId; });
            var controlLevelIndex_1 = this.chartOfAccountList[mainLevelIndex_2].Children
                .indexOf(controlLevelItem);
            var count = this.chartOfAccountList[mainLevelIndex_2].Children[controlLevelIndex_1].Children.length - 1;
            if (count < src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevelLimits"].SubLevel) {
                var obj_3 = {
                    ChartOfAccountNewId: 0,
                    AccountName: model.AccountName,
                    ParentID: this.controlLevelData.ChartOfAccountNewId,
                    AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].SubLevel,
                    AccountHeadTypeId: this.AccountHeadType,
                    AccountTypeId: model.AccountTypeId,
                    AccountFilterTypeId: model.AccountFilterTypeId,
                    ChartOfAccountNewCode: model.AccountCode,
                };
                // Error handling and loading handling
                var index = this.chartOfAccountList[mainLevelIndex_2].Children[controlLevelIndex_1].Children.length - 1; // use to calculate the index
                this.globalService
                    .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_8__["GLOBAL"].API_ChartOfAccount_AddChartOfAccount, obj_3)
                    .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10__["takeUntil"])(this.destroyed$))
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        if (response.data.ChartOfAccountNewDetail !== null) {
                            var responseData = response.data.ChartOfAccountNewDetail;
                            obj_3.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
                            obj_3.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
                            obj_3.AccountName = responseData.AccountName;
                            obj_3.ParentID = responseData.ParentID;
                            obj_3.AccountLevelId = responseData.AccountLevelId;
                            obj_3.AccountHeadTypeId = responseData.AccountHeadTypeId;
                            obj_3.AccountTypeId = responseData.AccountTypeId;
                            obj_3.AccountFilterTypeId = responseData.AccountFilterTypeId;
                            obj_3.Children = [];
                            obj_3._IsDeleted = false;
                            obj_3._IsError = false;
                            obj_3._IsLoading = false;
                            _this.addAccountLoader = false;
                            // Update the Obj and Push into the list
                            _this.chartOfAccountList[mainLevelIndex_2].Children[controlLevelIndex_1].Children.push(obj_3);
                            _this.dialogRef.close();
                        }
                    }
                    else if (response.StatusCode === 400) {
                        _this.toastr.error(response.Message);
                        _this.addAccountLoader = false;
                    }
                }, function (error) {
                    _this.toastr.error('Something went wrong ! Try Again');
                    _this.addAccountLoader = false;
                });
            }
            else {
                this.toastr.error('Limit Exceeded');
                this.addAccountLoader = false;
            }
        }
    };
    //#endregion
    //#region "addInputLevelAccountDetail"
    AddAccountComponent.prototype.addInputLevelAccountDetail = function (model) {
        var _this = this;
        if (this.addCharOfAccountForm.valid) {
            this.addAccountLoader = true;
            // Main Level
            var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === _this.mainLevelData.ChartOfAccountNewId; });
            var mainLevelIndex_3 = this.chartOfAccountList.indexOf(mainLevelItem);
            // Control Level
            var controlLevelItem = this.chartOfAccountList[mainLevelIndex_3].Children
                .find(function (x) { return x.ChartOfAccountNewId === _this.controlLevelData.ChartOfAccountNewId; });
            var controlLevelIndex_2 = this.chartOfAccountList[mainLevelIndex_3].Children
                .indexOf(controlLevelItem);
            // Sub Level
            var subLevelItem = this.chartOfAccountList[mainLevelIndex_3].Children[controlLevelIndex_2].Children
                .find(function (x) { return x.ChartOfAccountNewId === _this.subLevelData.ChartOfAccountNewId; });
            var subLevelIndex_1 = this.chartOfAccountList[mainLevelIndex_3].Children[controlLevelIndex_2].Children
                .indexOf(subLevelItem);
            var count = this.chartOfAccountList[mainLevelIndex_3].Children[controlLevelIndex_2].Children[subLevelIndex_1].Children.length - 1;
            if (count < src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevelLimits"].InputLevel) {
                var obj_4 = {
                    ChartOfAccountNewId: 0,
                    AccountName: model.AccountName,
                    ParentID: this.subLevelData.ChartOfAccountNewId,
                    AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["AccountLevels"].InputLevel,
                    AccountHeadTypeId: this.AccountHeadType,
                    AccountTypeId: model.AccountTypeId,
                    AccountFilterTypeId: model.AccountFilterTypeId,
                    ChartOfAccountNewCode: model.AccountCode
                };
                // Error handling and loading handling
                var index = this.chartOfAccountList[mainLevelIndex_3]
                    .Children[controlLevelIndex_2].Children[subLevelIndex_1].Children.length - 1; // use to calculate the index
                this.globalService
                    .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_8__["GLOBAL"].API_ChartOfAccount_AddChartOfAccount, obj_4)
                    .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10__["takeUntil"])(this.destroyed$))
                    .subscribe(function (response) {
                    if (response.StatusCode === 200) {
                        if (response.data.ChartOfAccountNewDetail !== null) {
                            var responseData = response.data.ChartOfAccountNewDetail;
                            obj_4.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
                            obj_4.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
                            obj_4.AccountName = responseData.AccountName;
                            obj_4.ParentID = responseData.ParentID;
                            obj_4.AccountLevelId = responseData.AccountLevelId;
                            obj_4.AccountHeadTypeId = responseData.AccountHeadTypeId;
                            obj_4.AccountTypeId = responseData.AccountTypeId;
                            obj_4.AccountFilterTypeId = responseData.AccountFilterTypeId;
                            obj_4.Children = [];
                            obj_4._IsDeleted = false;
                            obj_4._IsError = false;
                            obj_4._IsLoading = false;
                            _this.addAccountLoader = false;
                            // Update the Obj and Push into the list
                            _this.chartOfAccountList[mainLevelIndex_3].Children[controlLevelIndex_2].Children[subLevelIndex_1].Children.push(obj_4);
                            _this.dialogRef.close();
                        }
                    }
                    else if (response.StatusCode === 400) {
                        _this.toastr.error(response.Message);
                        _this.addAccountLoader = false;
                    }
                }, function (error) {
                    _this.toastr.error('Something went wrong ! Try Again');
                    _this.addAccountLoader = false;
                });
            }
            else {
                this.toastr.error('Limit Exceeded');
                this.addAccountLoader = false;
            }
        }
    };
    //#endregion
    AddAccountComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    AddAccountComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-account',
            template: __webpack_require__(/*! ./add-account.component.html */ "./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.html"),
            styles: [__webpack_require__(/*! ./add-account.component.scss */ "./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.scss")]
        }),
        __param(2, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_4__["CommonLoaderService"], Object, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"],
            src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_5__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_6__["AppUrlService"]])
    ], AddAccountComponent);
    return AddAccountComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.html":
/*!*************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.html ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<app-chart-of-account-detail\r\n  [ACCOUNT_HEAD_TYPE]=\"ACCOUNT_HEAD_TYPE\"\r\n  [ACCOUNT_HEAD_NAME]=\"ACCOUNT_HEAD_NAME\"\r\n>\r\n</app-chart-of-account-detail>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.scss":
/*!*************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.scss ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2NoYXJ0LW9mLWFjY291bnRzL2Fzc2V0cy9hc3NldHMuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.ts":
/*!***********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.ts ***!
  \***********************************************************************************/
/*! exports provided: AssetsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AssetsComponent", function() { return AssetsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var AssetsComponent = /** @class */ (function () {
    function AssetsComponent() {
        this.ACCOUNT_HEAD_TYPE = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"].Assets;
        this.ACCOUNT_HEAD_NAME = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"][this.ACCOUNT_HEAD_TYPE];
    }
    AssetsComponent.prototype.ngOnInit = function () {
    };
    AssetsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-assets',
            template: __webpack_require__(/*! ./assets.component.html */ "./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.html"),
            styles: [__webpack_require__(/*! ./assets.component.scss */ "./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], AssetsComponent);
    return AssetsComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-account-detail/chart-of-account-detail.component.html":
/*!***********************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/chart-of-account-detail/chart-of-account-detail.component.html ***!
  \***********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">{{ ACCOUNT_HEAD_NAME }}</span>\r\n\r\n  <div class=\"action_section\">\r\n    <button mat-stroked-button color=\"accent\" (click)=\"onExportPdf()\">\r\n      <mat-icon aria-hidden=\"false\" aria-label=\"Example home icon\">\r\n        vertical_align_bottom </mat-icon\r\n      >Export Pdf\r\n    </button>\r\n    &nbsp;\r\n    <button\r\n      mat-raised-button\r\n      color=\"primary\"\r\n      (click)=\"onAddMainLevelAccountDialog()\"\r\n      *ngIf=\"isEditingAllowed\"\r\n    >\r\n      <mat-icon aria-hidden=\"false\" aria-label=\"Example home icon\">\r\n        add\r\n      </mat-icon>\r\n      Add Main Category\r\n    </button>\r\n  </div>\r\n</lib-sub-header-template>\r\n\r\n<div class=\"body-content\">\r\n  <div class=\"assets-main\">\r\n    <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-10 col-sm-offset-1\">\r\n          <mat-card [ngStyle]=\"scrollStyles\">\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-12\">\r\n                <!-- main level -->\r\n                <div *ngFor=\"let mainLevelData of chartOfAccountList\">\r\n                  <div\r\n                    class=\"panel-heading blue_bg\"\r\n                    role=\"tab\"\r\n                    [id]=\"'heading' + mainLevelData.ChartOfAccountNewId\"\r\n                  >\r\n                    <div class=\"row\">\r\n                      <div class=\"col-sm-1\">\r\n                        <div\r\n                          *ngIf=\"\r\n                            mainLevelData._IsLoading && !mainLevelData._IsError\r\n                          \"\r\n                        >\r\n                          <mat-spinner [diameter]=\"15\"></mat-spinner>\r\n                        </div>\r\n                        <div *ngIf=\"mainLevelData._IsError\">\r\n                          <i class=\"fas fa-exclamation-triangle\"></i>\r\n                        </div>\r\n                        <div\r\n                          *ngIf=\"\r\n                            !mainLevelData._IsLoading && !mainLevelData._IsError\r\n                          \"\r\n                        >\r\n                          <a\r\n                            (click)=\"onMainLevelClicked(mainLevelData)\"\r\n                            class=\"counts_list\"\r\n                            role=\"button\"\r\n                            data-toggle=\"collapse\"\r\n                            data-parent=\"#accordion\"\r\n                            [href]=\"\r\n                              '#collapse' + mainLevelData.ChartOfAccountNewId\r\n                            \"\r\n                            aria-expanded=\"true\"\r\n                            [attr.aria-controls]=\"\r\n                              'collapse' + mainLevelData.ChartOfAccountNewId\r\n                            \"\r\n                          >\r\n                            {{ mainLevelData.ChartOfAccountNewCode }}\r\n                          </a>\r\n                        </div>\r\n                      </div>\r\n                      <div class=\"col-sm-11\">\r\n                        <div class=\"example-form-field accordian_content\">\r\n                          <input\r\n                            matInput\r\n                            type=\"text\"\r\n                            maxlength=\"150\"\r\n                            [disabled]=\"\r\n                              mainLevelData._IsLoading || !isEditingAllowed\r\n                            \"\r\n                            [value]=\"mainLevelData.AccountName\"\r\n                            [name]=\"\r\n                              'mainLevelDataAccountName' +\r\n                              mainLevelData.ChartOfAccountNewId\r\n                            \"\r\n                            (change)=\"\r\n                              onBlurEditMainLevelAccountName(\r\n                                mainLevelData,\r\n                                $event.target.value\r\n                              )\r\n                            \"\r\n                          />\r\n                          &nbsp;\r\n                          <i\r\n                            class=\"fas fa-trash icon_cursor\"\r\n                            color=\"warn\"\r\n                            (click)=\"onDeleteMainLevel(mainLevelData)\"\r\n                          ></i>\r\n                        </div>\r\n\r\n                        <span class=\"border-arrow\">\r\n                          <a\r\n                            [hidden]=\"mainLevelData._IsLoading\"\r\n                            (click)=\"onMainLevelClicked(mainLevelData)\"\r\n                            class=\"arrow_accord\"\r\n                            role=\"button\"\r\n                            data-toggle=\"collapse\"\r\n                            data-parent=\"#accordion\"\r\n                            [href]=\"\r\n                              '#collapse' + mainLevelData.ChartOfAccountNewId\r\n                            \"\r\n                            aria-expanded=\"true\"\r\n                            [attr.aria-controls]=\"\r\n                              'collapse' + mainLevelData.ChartOfAccountNewId\r\n                            \"\r\n                          >\r\n                          </a>\r\n                        </span>\r\n                      </div>\r\n                    </div>\r\n                  </div>\r\n\r\n                  <!-- Control level -->\r\n                  <div\r\n                    [id]=\"'collapse' + mainLevelData.ChartOfAccountNewId\"\r\n                    class=\"panel-collapse collapse\"\r\n                    role=\"tabpanel\"\r\n                    [attr.aria-labelledby]=\"\r\n                      'heading' + mainLevelData.ChartOfAccountNewId\r\n                    \"\r\n                  >\r\n                    <div class=\"panel-body padding_0\">\r\n                      <div class=\"row\">\r\n                        <div class=\"col-sm-12\">\r\n                          <div class=\"panel-heading\">\r\n                            <div class=\"row\">\r\n                              <div class=\"col-sm-1\"></div>\r\n                              <div class=\"col-sm-11\">\r\n                                <a\r\n                                  (click)=\"\r\n                                    onAddControlLevelAccountDialog(\r\n                                      mainLevelData\r\n                                    )\r\n                                  \"\r\n                                  class=\"add_new\"\r\n                                  *ngIf=\"isEditingAllowed\"\r\n                                >\r\n                                  Add New...\r\n                                </a>\r\n                              </div>\r\n                            </div>\r\n                          </div>\r\n\r\n                          <div\r\n                            *ngFor=\"\r\n                              let controlLevelData of mainLevelData.Children\r\n                            \"\r\n                          >\r\n                            <div\r\n                              class=\"panel-heading child_div\"\r\n                              role=\"tab\"\r\n                              [id]=\"\r\n                                'heading' + controlLevelData.ChartOfAccountNewId\r\n                              \"\r\n                            >\r\n                              <div class=\"row\">\r\n                                <div class=\"col-sm-1\">\r\n                                  <div\r\n                                    *ngIf=\"\r\n                                      controlLevelData._IsLoading &&\r\n                                      !controlLevelData._IsError\r\n                                    \"\r\n                                  >\r\n                                    <mat-spinner [diameter]=\"15\"></mat-spinner>\r\n                                  </div>\r\n                                  <div *ngIf=\"controlLevelData._IsError\">\r\n                                    <i class=\"fas fa-exclamation-triangle\"></i>\r\n                                  </div>\r\n                                  <div\r\n                                    *ngIf=\"\r\n                                      !controlLevelData._IsLoading &&\r\n                                      !controlLevelData._IsError\r\n                                    \"\r\n                                  >\r\n                                    <a\r\n                                      (click)=\"\r\n                                        onControlLevelClicked(\r\n                                          mainLevelData,\r\n                                          controlLevelData\r\n                                        )\r\n                                      \"\r\n                                      role=\"button\"\r\n                                      data-toggle=\"collapse\"\r\n                                      data-parent=\"#accordion\"\r\n                                      [href]=\"\r\n                                        '#collapse' +\r\n                                        controlLevelData.ChartOfAccountNewId\r\n                                      \"\r\n                                      class=\"counts_list second_div\"\r\n                                      aria-expanded=\"true\"\r\n                                      [attr.aria-controls]=\"\r\n                                        'collapse' +\r\n                                        controlLevelData.ChartOfAccountNewId\r\n                                      \"\r\n                                    >\r\n                                      {{\r\n                                        controlLevelData.ChartOfAccountNewCode\r\n                                      }}\r\n                                    </a>\r\n                                  </div>\r\n                                </div>\r\n                                <div class=\"col-sm-11\">\r\n                                  <div\r\n                                    class=\"example-form-field accordian_content\"\r\n                                  >\r\n                                    <input\r\n                                      matInput\r\n                                      type=\"text\"\r\n                                      maxlength=\"150\"\r\n                                      [disabled]=\"\r\n                                        controlLevelData._IsLoading ||\r\n                                        !isEditingAllowed\r\n                                      \"\r\n                                      [value]=\"controlLevelData.AccountName\"\r\n                                      [name]=\"\r\n                                        'controlAccountName' +\r\n                                        controlLevelData.ChartOfAccountNewId\r\n                                      \"\r\n                                      (change)=\"\r\n                                        onBlurEditControlLevelAccountName(\r\n                                          mainLevelData,\r\n                                          controlLevelData,\r\n                                          $event.target.value\r\n                                        )\r\n                                      \"\r\n                                    />\r\n                                    &nbsp;\r\n                                    <i\r\n                                      class=\"fas fa-trash icon_cursor\"\r\n                                      color=\"warn\"\r\n                                      (click)=\"\r\n                                        onDeleteControlLevel(\r\n                                          mainLevelData,\r\n                                          controlLevelData\r\n                                        )\r\n                                      \"\r\n                                    ></i>\r\n                                  </div>\r\n\r\n                                  <a\r\n                                    [hidden]=\"controlLevelData._IsLoading\"\r\n                                    (click)=\"\r\n                                      onControlLevelClicked(\r\n                                        mainLevelData,\r\n                                        controlLevelData\r\n                                      )\r\n                                    \"\r\n                                    class=\"arrow_accord\"\r\n                                    role=\"button\"\r\n                                    data-toggle=\"collapse\"\r\n                                    data-parent=\"#accordion\"\r\n                                    [href]=\"\r\n                                      '#collapse' +\r\n                                      controlLevelData.ChartOfAccountNewId\r\n                                    \"\r\n                                    aria-expanded=\"true\"\r\n                                    [attr.aria-controls]=\"\r\n                                      'collapse' +\r\n                                      controlLevelData.ChartOfAccountNewId\r\n                                    \"\r\n                                  >\r\n                                  </a>\r\n                                </div>\r\n                              </div>\r\n                            </div>\r\n\r\n                            <!-- Sub level -->\r\n                            <div\r\n                              [id]=\"\r\n                                'collapse' +\r\n                                controlLevelData.ChartOfAccountNewId\r\n                              \"\r\n                              class=\"panel-collapse collapse\"\r\n                              role=\"tabpanel\"\r\n                              [attr.aria-labelledby]=\"\r\n                                'heading' + controlLevelData.ChartOfAccountNewId\r\n                              \"\r\n                            >\r\n                              <div class=\"panel-body padding_0\">\r\n                                <div class=\"row\">\r\n                                  <div class=\"col-sm-1\"></div>\r\n                                  <div class=\"col-sm-10\">\r\n                                    <div class=\"panel-heading\">\r\n                                      <div class=\"row\">\r\n                                        <div class=\"col-sm-10\">\r\n                                          <a\r\n                                            (click)=\"\r\n                                              onAddSubLevelAccountDialog(\r\n                                                mainLevelData,\r\n                                                controlLevelData\r\n                                              )\r\n                                            \"\r\n                                            class=\"add_new\"\r\n                                            *ngIf=\"isEditingAllowed\"\r\n                                          >\r\n                                            Add New...\r\n                                          </a>\r\n                                        </div>\r\n                                      </div>\r\n                                    </div>\r\n\r\n                                    <div\r\n                                      *ngFor=\"\r\n                                        let subLevelData of controlLevelData.Children\r\n                                      \"\r\n                                    >\r\n                                      <div\r\n                                        class=\"panel-heading child_div tertiarry_level\"\r\n                                        role=\"tab\"\r\n                                        [id]=\"\r\n                                          'heading' +\r\n                                          subLevelData.ChartOfAccountNewId\r\n                                        \"\r\n                                      >\r\n                                        <div class=\"row\">\r\n                                          <div class=\"col-sm-1\">\r\n                                            <div\r\n                                              *ngIf=\"\r\n                                                subLevelData._IsLoading &&\r\n                                                !subLevelData._IsError\r\n                                              \"\r\n                                            >\r\n                                              <mat-spinner\r\n                                                [diameter]=\"15\"\r\n                                              ></mat-spinner>\r\n                                            </div>\r\n                                            <div *ngIf=\"subLevelData._IsError\">\r\n                                              <i\r\n                                                class=\"fas fa-exclamation-triangle\"\r\n                                              ></i>\r\n                                            </div>\r\n                                            <div\r\n                                              *ngIf=\"\r\n                                                !subLevelData._IsLoading &&\r\n                                                !subLevelData._IsError\r\n                                              \"\r\n                                            >\r\n                                              <a\r\n                                                class=\"counts_list count_3\"\r\n                                                (click)=\"\r\n                                                  onSubLevelClicked(\r\n                                                    mainLevelData,\r\n                                                    controlLevelData,\r\n                                                    subLevelData\r\n                                                  )\r\n                                                \"\r\n                                                role=\"button\"\r\n                                                data-toggle=\"collapse\"\r\n                                                data-parent=\"#accordion\"\r\n                                                [href]=\"\r\n                                                  '#collapse' +\r\n                                                  subLevelData.ChartOfAccountNewId\r\n                                                \"\r\n                                                aria-expanded=\"true\"\r\n                                                [attr.aria-controls]=\"\r\n                                                  'collapse' +\r\n                                                  mainLevelData.ChartOfAccountNewId\r\n                                                \"\r\n                                              >\r\n                                                {{\r\n                                                  subLevelData.ChartOfAccountNewCode\r\n                                                }}\r\n                                              </a>\r\n                                            </div>\r\n                                          </div>\r\n                                          <div class=\"col-sm-3\">\r\n                                            <div\r\n                                              class=\"example-form-field accordian_content\"\r\n                                            >\r\n                                              <!-- <mat-form-field class=\"example-form-field\"> -->\r\n                                              <input\r\n                                                matInput\r\n                                                type=\"text\"\r\n                                                maxlength=\"150\"\r\n                                                [disabled]=\"\r\n                                                  subLevelData._IsLoading ||\r\n                                                  !isEditingAllowed\r\n                                                \"\r\n                                                [value]=\"\r\n                                                  subLevelData.AccountName\r\n                                                \"\r\n                                                [name]=\"\r\n                                                  'subLevelAccountName' +\r\n                                                  subLevelData.ChartOfAccountNewId\r\n                                                \"\r\n                                                (change)=\"\r\n                                                  onBlurEditSubLevelAccountName(\r\n                                                    mainLevelData,\r\n                                                    controlLevelData,\r\n                                                    subLevelData,\r\n                                                    $event.target.value\r\n                                                  )\r\n                                                \"\r\n                                              />\r\n                                              <!-- </mat-form-field> -->\r\n                                            </div>\r\n                                          </div>\r\n                                          <div class=\"col-sm-3\">\r\n                                            <div\r\n                                              class=\"example-form-field accordian_content\"\r\n                                            >\r\n                                              <!-- <mat-form-field class=\"example-form-field\"> -->\r\n                                              <mat-select\r\n                                                [disabled]=\"\r\n                                                  subLevelData._IsLoading ||\r\n                                                  !isEditingAllowed\r\n                                                \"\r\n                                                [value]=\"\r\n                                                  subLevelData.AccountFilterTypeId\r\n                                                \"\r\n                                                (selectionChange)=\"\r\n                                                  onBlurEditSubLevelAccountFilterType(\r\n                                                    mainLevelData,\r\n                                                    controlLevelData,\r\n                                                    subLevelData,\r\n                                                    $event.value\r\n                                                  )\r\n                                                \"\r\n                                              >\r\n                                                <mat-option\r\n                                                  *ngFor=\"\r\n                                                    let item of accountFilterTypeList\r\n                                                  \"\r\n                                                  [value]=\"\r\n                                                    item.AccountFilterTypeId\r\n                                                  \"\r\n                                                >\r\n                                                  {{\r\n                                                    item.AccountFilterTypeName\r\n                                                  }}\r\n                                                </mat-option>\r\n                                              </mat-select>\r\n                                              <!-- </mat-form-field> -->\r\n                                            </div>\r\n                                          </div>\r\n                                          <div class=\"col-sm-3\">\r\n                                            <div\r\n                                              class=\"example-form-field accordian_content\"\r\n                                            >\r\n                                              <!-- <mat-form-field class=\"example-form-field\"> -->\r\n                                              <mat-select\r\n                                                [disabled]=\"\r\n                                                  subLevelData._IsLoading ||\r\n                                                  !isEditingAllowed\r\n                                                \"\r\n                                                [value]=\"\r\n                                                  subLevelData.AccountTypeId\r\n                                                \"\r\n                                                (selectionChange)=\"\r\n                                                  onBlurEditSubLevelAccountType(\r\n                                                    mainLevelData,\r\n                                                    controlLevelData,\r\n                                                    subLevelData,\r\n                                                    $event.value\r\n                                                  )\r\n                                                \"\r\n                                              >\r\n                                                <mat-option\r\n                                                  *ngFor=\"\r\n                                                    let item of accountTypeList\r\n                                                  \"\r\n                                                  [value]=\"item.AccountTypeId\"\r\n                                                >\r\n                                                  {{ item.AccountTypeName }}\r\n                                                </mat-option>\r\n                                              </mat-select>\r\n                                              <!-- </mat-form-field> -->\r\n                                            </div>\r\n                                          </div>\r\n                                          <div class=\"col-sm-1\">\r\n                                            <i\r\n                                              class=\"fas fa-trash icon_cursor\"\r\n                                              color=\"warn\"\r\n                                              (click)=\"\r\n                                                onDeleteSubLevel(\r\n                                                  mainLevelData,\r\n                                                  controlLevelData,\r\n                                                  subLevelData\r\n                                                )\r\n                                              \"\r\n                                            ></i>\r\n                                          </div>\r\n                                          <div class=\"col-sm-1\">\r\n                                            <a\r\n                                              [hidden]=\"subLevelData._IsLoading\"\r\n                                              (click)=\"\r\n                                                onSubLevelClicked(\r\n                                                  mainLevelData,\r\n                                                  controlLevelData,\r\n                                                  subLevelData\r\n                                                )\r\n                                              \"\r\n                                              class=\"arrow_accord\"\r\n                                              role=\"button\"\r\n                                              data-toggle=\"collapse\"\r\n                                              data-parent=\"#accordion\"\r\n                                              [href]=\"\r\n                                                '#collapse' +\r\n                                                subLevelData.ChartOfAccountNewId\r\n                                              \"\r\n                                              aria-expanded=\"true\"\r\n                                              [attr.aria-controls]=\"\r\n                                                'collapse' +\r\n                                                mainLevelData.ChartOfAccountNewId\r\n                                              \"\r\n                                            >\r\n                                            </a>\r\n                                          </div>\r\n                                        </div>\r\n                                      </div>\r\n\r\n                                      <!-- Input level -->\r\n                                      <div\r\n                                        [id]=\"\r\n                                          'collapse' +\r\n                                          subLevelData.ChartOfAccountNewId\r\n                                        \"\r\n                                        class=\"panel-collapse collapse\"\r\n                                        role=\"tabpanel\"\r\n                                        [attr.aria-labelledby]=\"\r\n                                          'heading' +\r\n                                          subLevelData.ChartOfAccountNewId\r\n                                        \"\r\n                                      >\r\n                                        <div class=\"panel-body padding_0\">\r\n                                          <div class=\"\">\r\n                                            <div class=\"\">\r\n                                              <div\r\n                                                class=\"panel-heading clearfix\"\r\n                                              >\r\n                                                <div class=\"row\">\r\n                                                  <div class=\"col-sm-1\"></div>\r\n                                                  <div class=\"col-sm-9\">\r\n                                                    <a\r\n                                                      class=\"add_new\"\r\n                                                      (click)=\"\r\n                                                        onAddInputLevelAccountDialog(\r\n                                                          mainLevelData,\r\n                                                          controlLevelData,\r\n                                                          subLevelData\r\n                                                        )\r\n                                                      \"\r\n                                                      *ngIf=\"isEditingAllowed\"\r\n                                                    >\r\n                                                      Add New...\r\n                                                    </a>\r\n                                                  </div>\r\n                                                </div>\r\n                                              </div>\r\n\r\n                                              <div\r\n                                                *ngFor=\"\r\n                                                  let inputLevelData of subLevelData.Children\r\n                                                \"\r\n                                              >\r\n                                                <div\r\n                                                  class=\"panel-heading child_div fourth_level\"\r\n                                                  [id]=\"\r\n                                                    'heading' +\r\n                                                    inputLevelData.ChartOfAccountNewId\r\n                                                  \"\r\n                                                >\r\n                                                  <div class=\"row\">\r\n                                                    <div class=\"col-sm-1\">\r\n                                                      <div\r\n                                                        *ngIf=\"\r\n                                                          inputLevelData._IsLoading &&\r\n                                                          !inputLevelData._IsError\r\n                                                        \"\r\n                                                      >\r\n                                                        <mat-spinner\r\n                                                          [diameter]=\"15\"\r\n                                                        ></mat-spinner>\r\n                                                      </div>\r\n                                                      <div\r\n                                                        *ngIf=\"\r\n                                                          inputLevelData._IsError\r\n                                                        \"\r\n                                                      >\r\n                                                        <i\r\n                                                          class=\"fas fa-exclamation-triangle\"\r\n                                                        ></i>\r\n                                                      </div>\r\n                                                      <div\r\n                                                        *ngIf=\"\r\n                                                          !inputLevelData._IsLoading &&\r\n                                                          !inputLevelData._IsError\r\n                                                        \"\r\n                                                      >\r\n                                                        <a\r\n                                                          class=\"counts_list count_4\"\r\n                                                          role=\"button\"\r\n                                                          data-toggle=\"collapse\"\r\n                                                          [href]=\"\r\n                                                            '#collapse' +\r\n                                                            inputLevelData.ChartOfAccountNewId\r\n                                                          \"\r\n                                                        >\r\n                                                          {{\r\n                                                            inputLevelData.ChartOfAccountNewCode\r\n                                                          }}\r\n                                                        </a>\r\n                                                      </div>\r\n                                                    </div>\r\n                                                    <div class=\"col-sm-11\">\r\n                                                      <div\r\n                                                        class=\"example-form-field accordian_content\"\r\n                                                      >\r\n                                                        <!-- <mat-form-field class=\"example-form-field\"> -->\r\n                                                        <input\r\n                                                          matInput\r\n                                                          type=\"text\"\r\n                                                          maxlength=\"150\"\r\n                                                          [disabled]=\"\r\n                                                            inputLevelData._IsLoading ||\r\n                                                            !isEditingAllowed\r\n                                                          \"\r\n                                                          [value]=\"\r\n                                                            inputLevelData.AccountName\r\n                                                          \"\r\n                                                          [name]=\"\r\n                                                            'inputLevel' +\r\n                                                            inputLevelData.ChartOfAccountNewId\r\n                                                          \"\r\n                                                          (change)=\"\r\n                                                            onBlurEditInputLevelAccountName(\r\n                                                              mainLevelData,\r\n                                                              controlLevelData,\r\n                                                              subLevelData,\r\n                                                              inputLevelData,\r\n                                                              $event.target\r\n                                                                .value\r\n                                                            )\r\n                                                          \"\r\n                                                        />\r\n                                                        &nbsp;\r\n                                                        <i\r\n                                                          class=\"fas fa-trash icon_cursor\"\r\n                                                          color=\"warn\"\r\n                                                          (click)=\"\r\n                                                            onDeleteInputLevel(\r\n                                                              mainLevelData,\r\n                                                              controlLevelData,\r\n                                                              subLevelData,\r\n                                                              inputLevelData\r\n                                                            )\r\n                                                          \"\r\n                                                        ></i>\r\n                                                      </div>\r\n                                                    </div>\r\n                                                  </div>\r\n                                                </div>\r\n                                              </div>\r\n                                            </div>\r\n                                          </div>\r\n                                        </div>\r\n                                      </div>\r\n                                    </div>\r\n                                  </div>\r\n                                </div>\r\n                              </div>\r\n                            </div>\r\n                          </div>\r\n                        </div>\r\n                      </div>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n            </div>\r\n          </mat-card>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-account-detail/chart-of-account-detail.component.scss":
/*!***********************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/chart-of-account-detail/chart-of-account-detail.component.scss ***!
  \***********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2NoYXJ0LW9mLWFjY291bnRzL2NoYXJ0LW9mLWFjY291bnQtZGV0YWlsL2NoYXJ0LW9mLWFjY291bnQtZGV0YWlsLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-account-detail/chart-of-account-detail.component.ts":
/*!*********************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/chart-of-account-detail/chart-of-account-detail.component.ts ***!
  \*********************************************************************************************************************/
/*! exports provided: ChartOfAccountDetailComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ChartOfAccountDetailComponent", function() { return ChartOfAccountDetailComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/shared/services/localstorage.service */ "./src/app/shared/services/localstorage.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var _add_account_add_account_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ../add-account/add-account.component */ "./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.ts");
/* harmony import */ var _chart_of_accounts_pdf_service__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../chart-of-accounts-pdf.service */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts-pdf.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};












var ChartOfAccountDetailComponent = /** @class */ (function () {
    //#endregion
    function ChartOfAccountDetailComponent(globalService, appUrl, toastr, commonLoaderService, localStorageService, dialog, cofPdfService) {
        this.globalService = globalService;
        this.appUrl = appUrl;
        this.toastr = toastr;
        this.commonLoaderService = commonLoaderService;
        this.localStorageService = localStorageService;
        this.dialog = dialog;
        this.cofPdfService = cofPdfService;
        this.chartOfAccountList = [];
        this.isEditingAllowed = false;
        this.pageId = src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__["ApplicationPages"].Assets;
        this.getScreenSize();
    }
    //#region "Dynamic Scroll"
    ChartOfAccountDetailComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            'height': this.screenHeight - 160 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    ChartOfAccountDetailComponent.prototype.ngOnChanges = function (changes) {
        // // console.log(changes);
        // this.getMainLevelAccount(this.ACCOUNT_HEAD_TYPE);
        // this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
        // this.cd.detectChanges();
    };
    ChartOfAccountDetailComponent.prototype.ngOnInit = function () {
        this.initList();
        this.getAllAccountFilterType();
        this.getAllAccountTypeByCategory();
        this.getMainLevelAccount(this.ACCOUNT_HEAD_TYPE);
        this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
    };
    ChartOfAccountDetailComponent.prototype.initList = function () { };
    //#region "on main level click, get control level account"
    ChartOfAccountDetailComponent.prototype.onMainLevelClicked = function (model) {
        // this.getAccountByParentId(data);
        var _this = this;
        this.commonLoaderService.showLoader();
        this.globalService
            .getListById(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_GetAllAccountsByParentId, model.ChartOfAccountNewId)
            .subscribe(function (data) {
            // Main Level
            var mainLevelItem = _this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
            var mainLevelIndex = _this.chartOfAccountList.indexOf(mainLevelItem);
            _this.chartOfAccountList[mainLevelIndex].Children = []; // intitialize
            if (data.StatusCode === 200) {
                if (data.data.AllAccountList != null) {
                    if (data.data.AllAccountList.length > 0) {
                        data.data.AllAccountList.forEach(function (element) {
                            _this.chartOfAccountList[mainLevelIndex].Children.push({
                                ChartOfAccountNewId: element.ChartOfAccountNewId,
                                AccountName: element.AccountName,
                                ChartOfAccountNewCode: element.ChartOfAccountNewCode,
                                ParentID: element.ParentID,
                                AccountHeadTypeId: element.AccountHeadTypeId,
                                AccountLevelId: element.AccountLevelId,
                                AccountTypeId: element.AccountTypeId,
                                AccountFilterTypeId: element.AccountFilterTypeId,
                                Children: [],
                                _IsDeleted: false,
                                _IsLoading: false,
                                _IsError: false,
                            });
                        });
                    }
                }
            }
            _this.commonLoaderService.hideLoader();
        }, function (error) {
            _this.commonLoaderService.hideLoader();
        });
    };
    //#endregion
    //#region "on control level click, get sub level account"
    ChartOfAccountDetailComponent.prototype.onControlLevelClicked = function (mainLevel, model) {
        var _this = this;
        this.commonLoaderService.showLoader();
        this.globalService
            .getListById(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_GetAllAccountsByParentId, model.ChartOfAccountNewId)
            .subscribe(function (data) {
            // Main Level
            var mainLevelItem = _this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevel.ChartOfAccountNewId; });
            var mainLevelIndex = _this.chartOfAccountList.indexOf(mainLevelItem);
            // Control Level
            var controlLevelItem = _this.chartOfAccountList[mainLevelIndex].Children.find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
            var controlLevelIndex = _this.chartOfAccountList[mainLevelIndex].Children.indexOf(controlLevelItem);
            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children = []; // intitialize
            if (data.StatusCode === 200) {
                if (data.data.AllAccountList != null) {
                    if (data.data.AllAccountList.length > 0) {
                        data.data.AllAccountList.forEach(function (element) {
                            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.push({
                                ChartOfAccountNewId: element.ChartOfAccountNewId,
                                AccountName: element.AccountName,
                                ChartOfAccountNewCode: element.ChartOfAccountNewCode,
                                ParentID: element.ParentID,
                                AccountHeadTypeId: element.AccountHeadTypeId,
                                AccountLevelId: element.AccountLevelId,
                                AccountTypeId: element.AccountTypeId,
                                AccountFilterTypeId: element.AccountFilterTypeId,
                                Children: [],
                                _IsDeleted: false,
                                _IsLoading: false,
                                _IsError: false,
                            });
                        });
                    }
                }
            }
            _this.commonLoaderService.hideLoader();
        }, function (error) {
            _this.commonLoaderService.hideLoader();
        });
    };
    //#endregion
    //#region "on sub level click, get input level account"
    ChartOfAccountDetailComponent.prototype.onSubLevelClicked = function (mainLevel, controlLevel, model) {
        var _this = this;
        this.commonLoaderService.showLoader();
        this.globalService
            .getListById(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_GetAllAccountsByParentId, model.ChartOfAccountNewId)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.AllAccountList != null) {
                    if (data.data.AllAccountList.length > 0) {
                        // Main Level
                        var mainLevelItem = _this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevel.ChartOfAccountNewId; });
                        var mainLevelIndex_1 = _this.chartOfAccountList.indexOf(mainLevelItem);
                        // Control Level
                        var controlLevelItem = _this.chartOfAccountList[mainLevelIndex_1].Children.find(function (x) {
                            return x.ChartOfAccountNewId === controlLevel.ChartOfAccountNewId;
                        });
                        var controlLevelIndex_1 = _this.chartOfAccountList[mainLevelIndex_1].Children.indexOf(controlLevelItem);
                        // Sub Level
                        var subLevelItem = _this.chartOfAccountList[mainLevelIndex_1].Children[controlLevelIndex_1].Children.find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
                        var subLevelIndex_1 = _this.chartOfAccountList[mainLevelIndex_1].Children[controlLevelIndex_1].Children.indexOf(subLevelItem);
                        _this.chartOfAccountList[mainLevelIndex_1].Children[controlLevelIndex_1].Children[subLevelIndex_1].Children = []; // intitialize
                        data.data.AllAccountList.forEach(function (element) {
                            _this.chartOfAccountList[mainLevelIndex_1].Children[controlLevelIndex_1].Children[subLevelIndex_1].Children.push({
                                ChartOfAccountNewId: element.ChartOfAccountNewId,
                                AccountName: element.AccountName,
                                ChartOfAccountNewCode: element.ChartOfAccountNewCode,
                                ParentID: element.ParentID,
                                AccountHeadTypeId: element.AccountHeadTypeId,
                                AccountLevelId: element.AccountLevelId,
                                AccountTypeId: element.AccountTypeId,
                                AccountFilterTypeId: element.AccountFilterTypeId,
                                Children: [],
                                _IsDeleted: false,
                                _IsError: false,
                                _IsLoading: false
                            });
                        });
                    }
                }
            }
            _this.commonLoaderService.hideLoader();
        }, function (error) {
            _this.commonLoaderService.hideLoader();
        });
    };
    //#endregion
    //#region "on input level click"
    ChartOfAccountDetailComponent.prototype.onInputLevelClicked = function (mainLevel, controlLevel, subLevel, data) {
    };
    //#endregion
    //#region "getAllAccountFilterType"
    ChartOfAccountDetailComponent.prototype.getAllAccountFilterType = function () {
        var _this = this;
        this.accountFilterTypeList = [];
        this.commonLoaderService.showLoader();
        this.globalService
            .getList(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_Account_GetAllAccountFilter)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.AllAccountFilterList != null) {
                    if (data.data.AllAccountFilterList.length > 0) {
                        data.data.AllAccountFilterList.forEach(function (element) {
                            _this.accountFilterTypeList.push({
                                AccountFilterTypeId: element.AccountFilterTypeId,
                                AccountFilterTypeName: element.AccountFilterTypeName
                            });
                        });
                    }
                }
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error('Something went wrong ! Try Again');
            }
            _this.commonLoaderService.hideLoader();
        }, function (error) {
            _this.commonLoaderService.hideLoader();
        });
    };
    //#endregion
    //#region "getAllAccountTypeByCategory"
    ChartOfAccountDetailComponent.prototype.getAllAccountTypeByCategory = function () {
        var _this = this;
        this.accountTypeList = [];
        this.globalService
            .getListById(this.appUrl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_Code_GetAllAccountByAccountHeadTypeId, this.ACCOUNT_HEAD_TYPE)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.AccountTypeList != null) {
                    if (data.data.AccountTypeList.length > 0) {
                        data.data.AccountTypeList.forEach(function (element) {
                            _this.accountTypeList.push({
                                AccountTypeId: element.AccountTypeId,
                                AccountTypeName: element.AccountTypeName,
                                AccountCategory: element.AccountCategory,
                                AccountHeadTypeId: element.AccountHeadTypeId
                            });
                        });
                    }
                }
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error('Something went wrong ! Try Again');
            }
        });
    };
    //#endregion
    //#region -- Main Level --
    //#region "getMainLevelAccount"
    ChartOfAccountDetailComponent.prototype.getMainLevelAccount = function (id) {
        var _this = this;
        this.chartOfAccountList = [];
        this.commonLoaderService.showLoader();
        this.globalService
            .getListById(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_GetMainLevelAccount, id)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.MainLevelAccountList != null) {
                    if (data.data.MainLevelAccountList.length > 0) {
                        data.data.MainLevelAccountList.forEach(function (element) {
                            _this.chartOfAccountList.push({
                                ChartOfAccountNewId: element.ChartOfAccountNewId,
                                AccountName: element.AccountName,
                                ChartOfAccountNewCode: element.ChartOfAccountNewCode,
                                ParentID: element.ParentID,
                                AccountHeadTypeId: element.AccountHeadTypeId,
                                AccountLevelId: element.AccountLevelId,
                                AccountTypeId: element.AccountTypeId,
                                AccountFilterTypeId: element.AccountFilterTypeId,
                                // Error Handling
                                _IsDeleted: false,
                                _IsLoading: false,
                                _IsError: false,
                            });
                        });
                    }
                }
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error('Something went wrong ! Try Again');
            }
            _this.commonLoaderService.hideLoader();
        }, function (error) {
            _this.commonLoaderService.hideLoader();
        });
    };
    //#endregion
    //#region "addMainLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.addMainLevelAccountDetail = function (model) {
        var _this = this;
        var count = this.chartOfAccountList.length;
        if (count < src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevelLimits"].MainLevel) {
            var obj_1 = {
                ChartOfAccountNewId: 0,
                AccountName: model.AccountName,
                ParentID: model.ParentID,
                AccountLevelId: model.AccountLevelId,
                AccountHeadTypeId: model.AccountHeadTypeId,
                AccountTypeId: model.AccountTypeId,
                AccountFilterTypeId: model.AccountFilterTypeId
            };
            this.chartOfAccountList.push(obj_1);
            // Error handling and loading handling
            var item_1 = this.chartOfAccountList.length - 1; // use to calculate the index
            this.chartOfAccountList[item_1]._IsLoading = true;
            this.chartOfAccountList[item_1]._IsError = false;
            // // Error handling and loading handling
            // const item = this.chartOfAccountList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
            // const index = this.occupatonList.indexOf(item);
            // this.occupatonList[index]._IsLoading = true;
            // this.occupatonList[index]._IsError = false;
            this.globalService
                .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_AddChartOfAccount, obj_1)
                .subscribe(function (response) {
                if (response.StatusCode === 200) {
                    if (response.data.ChartOfAccountNewDetail !== null) {
                        var responseData = response.data.ChartOfAccountNewDetail;
                        obj_1.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
                        obj_1.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
                        obj_1.AccountName = responseData.AccountName;
                        obj_1.ParentID = responseData.ParentID;
                        obj_1.AccountLevelId = responseData.AccountLevelId;
                        obj_1.AccountHeadTypeId = responseData.AccountHeadTypeId;
                        obj_1.AccountTypeId = responseData.AccountTypeId;
                        obj_1.AccountFilterTypeId = responseData.AccountFilterTypeId;
                        obj_1.Children = [];
                        obj_1._IsDeleted = false;
                        obj_1._IsError = false;
                        obj_1._IsLoading = false;
                        // Update the Obj and Push into the list
                        _this.chartOfAccountList[item_1] = obj_1;
                    }
                }
                else if (response.StatusCode === 400) {
                    _this.toastr.error(response.Message);
                    // Error Handling
                    _this.chartOfAccountList[item_1]._IsLoading = false;
                    _this.chartOfAccountList[item_1]._IsError = true;
                }
                else {
                    // Error Handling
                    _this.chartOfAccountList[item_1]._IsLoading = false;
                    _this.chartOfAccountList[item_1]._IsError = true;
                }
            }, function (error) {
                _this.toastr.error('Something went wrong ! Try Again');
                // Error Handling
                _this.chartOfAccountList[item_1]._IsLoading = false;
                _this.chartOfAccountList[item_1]._IsError = true;
            });
        }
        else {
            this.toastr.error('Limit Exceeded');
        }
    };
    //#endregion
    //#region "editMainLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.editMainLevelAccountDetail = function (model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        // Error handling and loading handling
        var item = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
        var index = this.chartOfAccountList.indexOf(item);
        this.chartOfAccountList[index]._IsLoading = true;
        this.chartOfAccountList[index]._IsError = false;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_EditChartOfAccount, obj)
            .subscribe(function (response) {
            if (response.StatusCode === 200) {
                // Error Handling
                _this.chartOfAccountList[index]._IsLoading = false;
                _this.chartOfAccountList[index]._IsError = false;
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
                // Error Handling
                _this.chartOfAccountList[index]._IsLoading = false;
                _this.chartOfAccountList[index]._IsError = true;
            }
        }, function (error) {
            // error handling
            _this.toastr.error('Someting went wrong! Try again');
            // Error Handling
            _this.chartOfAccountList[index]._IsLoading = false;
            _this.chartOfAccountList[index]._IsError = true;
        });
    };
    //#endregion
    //#region "deleteMainLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.deleteMainLevelAccountDetail = function (model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        // Error handling and loading handling
        var item = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
        var index = this.chartOfAccountList.indexOf(item);
        this.chartOfAccountList[index]._IsLoading = true;
        this.chartOfAccountList[index]._IsError = false;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_DeleteChartOfAccount, obj.ChartOfAccountNewId)
            .subscribe(function (response) {
            if (response.StatusCode === 200) {
                _this.chartOfAccountList.splice(index, 1);
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
                // Error Handling
                _this.chartOfAccountList[index]._IsLoading = false;
                _this.chartOfAccountList[index]._IsError = true;
            }
        }, function (error) {
            // error handling
            _this.toastr.error('Someting went wrong! Try again');
            // Error Handling
            _this.chartOfAccountList[index]._IsLoading = false;
            _this.chartOfAccountList[index]._IsError = true;
        });
    };
    //#endregion
    //#endregion
    //#region -- Control Level --
    //#region "addControlLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.addControlLevelAccountDetail = function (mainLevelData, model) {
        var _this = this;
        // Main Level
        var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId; });
        var mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);
        var count = this.chartOfAccountList[mainLevelIndex].Children.length - 1;
        if (count < src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevelLimits"].ControlLevel) {
            var obj_2 = {
                ChartOfAccountNewId: 0,
                AccountName: model.AccountName,
                ParentID: model.ParentID,
                AccountLevelId: model.AccountLevelId,
                AccountHeadTypeId: model.AccountHeadTypeId,
                AccountTypeId: model.AccountTypeId,
                AccountFilterTypeId: model.AccountFilterTypeId
            };
            this.chartOfAccountList[mainLevelIndex].Children.push(obj_2);
            // Error handling and loading handling
            var index_1 = this.chartOfAccountList[mainLevelIndex].Children.length - 1; // use to calculate the index
            this.chartOfAccountList[mainLevelIndex].Children[index_1]._IsLoading = true;
            this.chartOfAccountList[mainLevelIndex].Children[index_1]._IsError = false;
            // // Error handling and loading handling
            // const item = this.chartOfAccountList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
            // const index = this.occupatonList.indexOf(item);
            // this.occupatonList[index]._IsLoading = true;
            // this.occupatonList[index]._IsError = false;
            this.globalService
                .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_AddChartOfAccount, obj_2)
                .subscribe(function (response) {
                if (response.StatusCode === 200) {
                    if (response.data.ChartOfAccountNewDetail !== null) {
                        var responseData = response.data.ChartOfAccountNewDetail;
                        obj_2.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
                        obj_2.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
                        obj_2.AccountName = responseData.AccountName;
                        obj_2.ParentID = responseData.ParentID;
                        obj_2.AccountLevelId = responseData.AccountLevelId;
                        obj_2.AccountHeadTypeId = responseData.AccountHeadTypeId;
                        obj_2.AccountTypeId = responseData.AccountTypeId;
                        obj_2.AccountFilterTypeId = responseData.AccountFilterTypeId;
                        obj_2.Children = [];
                        obj_2._IsDeleted = false;
                        obj_2._IsError = false;
                        obj_2._IsLoading = false;
                        // Update the Obj and Push into the list
                        _this.chartOfAccountList[mainLevelIndex].Children[index_1] = obj_2;
                    }
                }
                else if (response.StatusCode === 400) {
                    _this.toastr.error(response.Message);
                    // Error Handling
                    _this.chartOfAccountList[mainLevelIndex].Children[index_1]._IsLoading = false;
                    _this.chartOfAccountList[mainLevelIndex].Children[index_1]._IsError = true;
                }
                else {
                    // Error Handling
                    _this.chartOfAccountList[mainLevelIndex].Children[index_1]._IsLoading = false;
                    _this.chartOfAccountList[mainLevelIndex].Children[index_1]._IsError = true;
                }
            }, function (error) {
                _this.toastr.error('Something went wrong ! Try Again');
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[index_1]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[index_1]._IsError = true;
            });
        }
        else {
            this.toastr.error('Limit Exceeded');
        }
    };
    //#endregion
    //#region "editControlLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.editControlLevelAccountDetail = function (mainLevelData, model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        // Main Level
        var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId; });
        var mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);
        // Error handling and loading handling
        var item = this.chartOfAccountList[mainLevelIndex].Children.find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
        var index = this.chartOfAccountList[mainLevelIndex].Children.indexOf(item);
        this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = true;
        this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = false;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_EditChartOfAccount, obj)
            .subscribe(function (response) {
            if (response.StatusCode === 200) {
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = false;
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;
            }
        }, function (error) {
            // error handling
            _this.toastr.error('Someting went wrong! Try again');
            // Error Handling
            _this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            _this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;
        });
    };
    //#endregion
    //#region "deleteControlLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.deleteControlLevelAccountDetail = function (mainLevelData, model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        // Main Level
        var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId; });
        var mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);
        // Error handling and loading handling
        var item = this.chartOfAccountList[mainLevelIndex].Children.find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
        var index = this.chartOfAccountList[mainLevelIndex].Children.indexOf(item);
        this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = true;
        this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = false;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_DeleteChartOfAccount, obj.ChartOfAccountNewId)
            .subscribe(function (response) {
            if (response.StatusCode === 200) {
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children.splice(index, 1);
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;
            }
        }, function (error) {
            // error handling
            _this.toastr.error('Someting went wrong! Try again');
            // Error Handling
            _this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            _this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;
        });
    };
    //#endregion
    //#endregion
    //#region -- Sub Level --
    //#region "addSubLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.addSubLevelAccountDetail = function (mainLevelData, controlLevelData, model) {
        var _this = this;
        // Main Level
        var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId; });
        var mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);
        // Control Level
        var controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId; });
        var controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
            .indexOf(controlLevelItem);
        var count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1;
        if (count < src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevelLimits"].SubLevel) {
            var obj_3 = {
                ChartOfAccountNewId: 0,
                AccountName: model.AccountName,
                ParentID: model.ParentID,
                AccountLevelId: model.AccountLevelId,
                AccountHeadTypeId: model.AccountHeadTypeId,
                AccountTypeId: model.AccountTypeId,
                AccountFilterTypeId: model.AccountFilterTypeId
            };
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.push(obj_3);
            // Error handling and loading handling
            var index_2 = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1; // use to calculate the index
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index_2]._IsLoading = true;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index_2]._IsError = false;
            // // Error handling and loading handling
            // const item = this.chartOfAccountList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
            // const index = this.occupatonList.indexOf(item);
            // this.occupatonList[index]._IsLoading = true;
            // this.occupatonList[index]._IsError = false;
            this.globalService
                .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_AddChartOfAccount, obj_3)
                .subscribe(function (response) {
                if (response.StatusCode === 200) {
                    if (response.data.ChartOfAccountNewDetail !== null) {
                        var responseData = response.data.ChartOfAccountNewDetail;
                        obj_3.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
                        obj_3.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
                        obj_3.AccountName = responseData.AccountName;
                        obj_3.ParentID = responseData.ParentID;
                        obj_3.AccountLevelId = responseData.AccountLevelId;
                        obj_3.AccountHeadTypeId = responseData.AccountHeadTypeId;
                        obj_3.AccountTypeId = responseData.AccountTypeId;
                        obj_3.AccountFilterTypeId = responseData.AccountFilterTypeId;
                        obj_3.Children = [];
                        obj_3._IsDeleted = false;
                        obj_3._IsError = false;
                        obj_3._IsLoading = false;
                        // Update the Obj and Push into the list
                        _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index_2] = obj_3;
                    }
                }
                else if (response.StatusCode === 400) {
                    _this.toastr.error(response.Message);
                    // Error Handling
                    _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index_2]._IsLoading = false;
                    _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index_2]._IsError = true;
                }
                else {
                    // Error Handling
                    _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index_2]._IsLoading = false;
                    _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index_2]._IsError = true;
                }
            }, function (error) {
                _this.toastr.error('Something went wrong ! Try Again');
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index_2]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index_2]._IsError = true;
            });
        }
        else {
            this.toastr.error('Limit Exceeded');
        }
    };
    //#endregion
    //#region "editSubLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.editSubLevelAccountDetail = function (mainLevelData, controlLevelData, model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        // // console.log(obj);
        // Main Level
        var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId; });
        var mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);
        // Control Level
        var controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId; });
        var controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
            .indexOf(controlLevelItem);
        // const count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1;
        // Error handling and loading handling
        var item = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
        item.AccountName = obj.AccountName; // its needed
        item.AccountFilterTypeId = obj.AccountFilterTypeId; // its needed
        item.AccountTypeId = obj.AccountTypeId; // its needed
        var index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.indexOf(item);
        this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = true;
        this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = false;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_EditChartOfAccount, obj)
            .subscribe(function (response) {
            // // console.log(obj);
            if (response.StatusCode === 200) {
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = false;
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;
            }
        }, function (error) {
            // error handling
            _this.toastr.error('Someting went wrong! Try again');
            // Error Handling
            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;
        });
    };
    //#endregion
    //#region "deleteSubLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.deleteSubLevelAccountDetail = function (mainLevelData, controlLevelData, model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        // Main Level
        var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId; });
        var mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);
        // Control Level
        var controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId; });
        var controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
            .indexOf(controlLevelItem);
        // const count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1;
        // Error handling and loading handling
        var item = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
        item.AccountName = obj.AccountName; // its needed
        item.AccountFilterTypeId = obj.AccountFilterTypeId; // its needed
        var index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.indexOf(item);
        this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = true;
        this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = false;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_DeleteChartOfAccount, obj.ChartOfAccountNewId)
            .subscribe(function (response) {
            if (response.StatusCode === 200) {
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.splice(index, 1);
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;
            }
        }, function (error) {
            // error handling
            _this.toastr.error('Someting went wrong! Try again');
            // Error Handling
            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;
        });
    };
    //#endregion
    //#endregion
    //#region -- Input Level --
    //#region "addInputLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.addInputLevelAccountDetail = function (mainLevelData, controlLevelData, subLevelData, model) {
        var _this = this;
        // Main Level
        var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId; });
        var mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);
        // Control Level
        var controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId; });
        var controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
            .indexOf(controlLevelItem);
        // Sub Level
        var subLevelItem = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === subLevelData.ChartOfAccountNewId; });
        var subLevelIndex = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
            .indexOf(subLevelItem);
        var count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.length - 1;
        if (count < src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevelLimits"].InputLevel) {
            var obj_4 = {
                ChartOfAccountNewId: 0,
                AccountName: model.AccountName,
                ParentID: model.ParentID,
                AccountLevelId: model.AccountLevelId,
                AccountHeadTypeId: model.AccountHeadTypeId,
                AccountTypeId: model.AccountTypeId,
                AccountFilterTypeId: model.AccountFilterTypeId
            };
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.push(obj_4);
            // Error handling and loading handling
            var index_3 = this.chartOfAccountList[mainLevelIndex]
                .Children[controlLevelIndex].Children[subLevelIndex].Children.length - 1; // use to calculate the index
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index_3]._IsLoading = true;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index_3]._IsError = false;
            // // Error handling and loading handling
            // const item = this.chartOfAccountList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
            // const index = this.occupatonList.indexOf(item);
            // this.occupatonList[index]._IsLoading = true;
            // this.occupatonList[index]._IsError = false;
            this.globalService
                .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_AddChartOfAccount, obj_4)
                .subscribe(function (response) {
                if (response.StatusCode === 200) {
                    if (response.data.ChartOfAccountNewDetail !== null) {
                        var responseData = response.data.ChartOfAccountNewDetail;
                        obj_4.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
                        obj_4.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
                        obj_4.AccountName = responseData.AccountName;
                        obj_4.ParentID = responseData.ParentID;
                        obj_4.AccountLevelId = responseData.AccountLevelId;
                        obj_4.AccountHeadTypeId = responseData.AccountHeadTypeId;
                        obj_4.AccountTypeId = responseData.AccountTypeId;
                        obj_4.AccountFilterTypeId = responseData.AccountFilterTypeId;
                        obj_4.Children = [];
                        obj_4._IsDeleted = false;
                        obj_4._IsError = false;
                        obj_4._IsLoading = false;
                        // Update the Obj and Push into the list
                        _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index_3] = obj_4;
                    }
                }
                else if (response.StatusCode === 400) {
                    _this.toastr.error(response.Message);
                    // Error Handling
                    _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index_3]._IsLoading = false;
                    _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index_3]._IsError = true;
                }
                else {
                    // Error Handling
                    _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index_3]._IsLoading = false;
                    _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index_3]._IsError = true;
                }
            }, function (error) {
                _this.toastr.error('Something went wrong ! Try Again');
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index_3]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index_3]._IsError = true;
            });
        }
        else {
            this.toastr.error('Limit Exceeded');
        }
    };
    //#endregion
    //#region "editInputLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.editInputLevelAccountDetail = function (mainLevelData, controlLevelData, subLevelData, model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        // Main Level
        var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId; });
        var mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);
        // Control Level
        var controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId; });
        var controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
            .indexOf(controlLevelItem);
        // Sub Level
        var subLevelItem = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === subLevelData.ChartOfAccountNewId; });
        var subLevelIndex = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
            .indexOf(subLevelItem);
        // Error handling and loading handling
        var item = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
        var index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.indexOf(item);
        this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = true;
        this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = false;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_EditChartOfAccount, obj)
            .subscribe(function (response) {
            if (response.StatusCode === 200) {
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = false;
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;
            }
        }, function (error) {
            // error handling
            _this.toastr.error('Someting went wrong! Try again');
            // Error Handling
            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;
        });
    };
    //#endregion
    //#region "deleteInputLevelAccountDetail"
    ChartOfAccountDetailComponent.prototype.deleteInputLevelAccountDetail = function (mainLevelData, controlLevelData, subLevelData, model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        // Main Level
        var mainLevelItem = this.chartOfAccountList.find(function (x) { return x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId; });
        var mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);
        // Control Level
        var controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId; });
        var controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
            .indexOf(controlLevelItem);
        // Sub Level
        var subLevelItem = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === subLevelData.ChartOfAccountNewId; });
        var subLevelIndex = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
            .indexOf(subLevelItem);
        // Error handling and loading handling
        var item = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children
            .find(function (x) { return x.ChartOfAccountNewId === model.ChartOfAccountNewId; });
        var index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.indexOf(item);
        this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = true;
        this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = false;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_DeleteChartOfAccount, obj.ChartOfAccountNewId)
            .subscribe(function (response) {
            if (response.StatusCode === 200) {
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.splice(index, 1);
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
                // Error Handling
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
                _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;
            }
        }, function (error) {
            // error handling
            _this.toastr.error('Someting went wrong! Try again');
            // Error Handling
            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
            _this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;
        });
    };
    //#endregion
    //#endregion
    //#region " addAny"
    ChartOfAccountDetailComponent.prototype.addAnyAccountDetail = function (model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        this.commonLoaderService.showLoader();
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_AddChartOfAccount, obj)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                _this.getMainLevelAccount(_this.ACCOUNT_HEAD_TYPE);
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error(data.Message);
            }
            _this.commonLoaderService.hideLoader();
        }, function (error) {
            // error handling
            _this.toastr.error('Something went wrong ! Try Again');
            _this.commonLoaderService.hideLoader();
        });
    };
    //#endregion
    //#region "editAny"
    ChartOfAccountDetailComponent.prototype.editAnyAccountDetail = function (model) {
        var _this = this;
        var obj = {
            ChartOfAccountNewId: model.ChartOfAccountNewId,
            AccountName: model.AccountName,
            ParentID: model.ParentID,
            AccountLevelId: model.AccountLevelId,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountTypeId: model.AccountTypeId,
            AccountFilterTypeId: model.AccountFilterTypeId
        };
        // // Error handling and loading handling
        // const item = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
        // const index = this.chartOfAccountList.indexOf(item);
        // this.chartOfAccountList[index]._IsLoading = true;
        // this.chartOfAccountList[index]._IsError = false;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_EditChartOfAccount, obj)
            .subscribe(function (response) {
            if (response.StatusCode === 200) {
                // Error Handling
                // this.chartOfAccountList[index]._IsLoading = false;
                // this.chartOfAccountList[index]._IsError = false;
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
                // Error Handling
                // this.chartOfAccountList[index]._IsLoading = false;
                // this.chartOfAccountList[index]._IsError = true;
            }
        }, function (error) {
            // error handling
            _this.toastr.error('Someting went wrong! Try again');
            // Error Handling
            // this.chartOfAccountList[index]._IsLoading = false;
            // this.chartOfAccountList[index]._IsError = true;
        });
    };
    //#endregion
    //#region "onAddMainLevelAccount"
    ChartOfAccountDetailComponent.prototype.onAddMainLevelAccount = function () {
        var obj = {
            ChartOfAccountNewId: 0,
            AccountName: '',
            ParentID: -1,
            AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
            AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].MainLevel,
            AccountTypeId: null,
            AccountFilterTypeId: null,
        };
        this.addMainLevelAccountDetail(obj);
    };
    //#endregion
    //#region "onAddSubLevelClicked"
    ChartOfAccountDetailComponent.prototype.onAddSubLevelClicked = function (mainLevelData, controlLevelData, data) {
    };
    //#endregion
    //#region "onAddControlLevelAccount"
    ChartOfAccountDetailComponent.prototype.onAddControlLevelAccount = function (mainLevelData) {
        var obj = {
            ChartOfAccountNewId: 0,
            AccountName: '',
            ParentID: mainLevelData.ChartOfAccountNewId,
            AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].ControlLevel,
            AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
            AccountTypeId: null,
            AccountFilterTypeId: null
        };
        this.addControlLevelAccountDetail(mainLevelData, obj);
    };
    //#endregion
    //#region "onAddSubLevelAccount"
    ChartOfAccountDetailComponent.prototype.onAddSubLevelAccount = function (mainLevelData, controlLevelData) {
        var obj = {
            ChartOfAccountNewId: 0,
            AccountName: '',
            ParentID: controlLevelData.ChartOfAccountNewId,
            AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].SubLevel,
            AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
            AccountTypeId: null,
            AccountFilterTypeId: null
        };
        this.addSubLevelAccountDetail(mainLevelData, controlLevelData, obj);
    };
    //#endregion
    //#region "onAddInputLevelAccount"
    ChartOfAccountDetailComponent.prototype.onAddInputLevelAccount = function (mainLevelData, controlLevelData, subLevelData) {
        var obj = {
            ChartOfAccountNewId: 0,
            AccountName: '',
            ParentID: subLevelData.ChartOfAccountNewId,
            AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].InputLevel,
            AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
            AccountTypeId: null,
            AccountFilterTypeId: null
        };
        // this.addAnyAccountDetail(obj);
        this.addInputLevelAccountDetail(mainLevelData, controlLevelData, subLevelData, obj);
    };
    //#endregion
    //#region "onBlurAddControlLevel"
    ChartOfAccountDetailComponent.prototype.onBlurAddControlLevel = function (mainLevelData, data) {
        if (data !== '') {
            var controlLevelDetail = {
                ChartOfAccountNewId: 0,
                AccountName: data,
                ParentID: mainLevelData.ChartOfAccountNewId,
                AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].ControlLevel,
                AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
                AccountTypeId: null,
                AccountFilterTypeId: null
            };
            this.addAnyAccountDetail(controlLevelDetail);
        }
    };
    //#endregion
    //#region "onBlurEditMainLevel_AccountName"
    ChartOfAccountDetailComponent.prototype.onBlurEditMainLevelAccountName = function (mainLevelData, data) {
        if (data !== '') {
            var mainLevelDetail = {
                ChartOfAccountNewId: mainLevelData.ChartOfAccountNewId,
                AccountName: data,
                ParentID: mainLevelData.ParentID,
                AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].MainLevel,
                AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
                AccountTypeId: mainLevelData.AccountTypeId,
                AccountFilterTypeId: mainLevelData.AccountFilterTypeId
            };
            this.editMainLevelAccountDetail(mainLevelDetail);
        }
    };
    //#endregion
    //#region "onBlurEditControlLevel_AccountName"
    ChartOfAccountDetailComponent.prototype.onBlurEditControlLevelAccountName = function (mainLevelData, controlLevelData, data) {
        if (data !== '') {
            var controlLevelDetail = {
                ChartOfAccountNewId: controlLevelData.ChartOfAccountNewId,
                AccountName: data,
                ParentID: controlLevelData.ParentID,
                AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].ControlLevel,
                AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
                AccountTypeId: controlLevelData.AccountTypeId,
                AccountFilterTypeId: controlLevelData.AccountFilterTypeId
            };
            // this.editAnyAccountDetail(controlLevelDetail);
            this.editControlLevelAccountDetail(mainLevelData, controlLevelDetail);
        }
    };
    //#endregion
    //#region "onBlurEditSubLevel_AccountName"
    ChartOfAccountDetailComponent.prototype.onBlurEditSubLevelAccountName = function (mainLevelData, controlLevelData, subLevelData, data) {
        if (data !== '') {
            var subLevelDetail = {
                ChartOfAccountNewId: subLevelData.ChartOfAccountNewId,
                AccountName: data,
                ParentID: subLevelData.ParentID,
                AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].SubLevel,
                AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
                AccountTypeId: subLevelData.AccountTypeId,
                AccountFilterTypeId: subLevelData.AccountFilterTypeId
            };
            // this.editAnyAccountDetail(subLevelDetail);
            this.editSubLevelAccountDetail(mainLevelData, controlLevelData, subLevelDetail);
        }
    };
    //#endregion
    //#region "onBlurEditSubLevel_AccountFilterType"
    ChartOfAccountDetailComponent.prototype.onBlurEditSubLevelAccountFilterType = function (mainLevelData, controlLevelData, subLevelData, data) {
        // console.log('subLevelData -- ', subLevelData);
        if (data !== '') {
            var subLevelDetail = {
                ChartOfAccountNewId: subLevelData.ChartOfAccountNewId,
                AccountName: subLevelData.AccountName,
                ParentID: subLevelData.ParentID,
                AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].SubLevel,
                AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
                AccountTypeId: subLevelData.AccountTypeId,
                AccountFilterTypeId: data
            };
            // console.log('AccountFilterType', subLevelDetail);
            this.editSubLevelAccountDetail(mainLevelData, controlLevelData, subLevelDetail);
            // this.editAnyAccountDetail(controlLevelDetail);
        }
    };
    //#endregion
    //#region "onBlurEditSubLevel_AccountType"
    ChartOfAccountDetailComponent.prototype.onBlurEditSubLevelAccountType = function (mainLevelData, controlLevelData, subLevelData, data) {
        //   // console.log(data);
        if (data !== '') {
            var subLevelDetail = {
                ChartOfAccountNewId: subLevelData.ChartOfAccountNewId,
                AccountName: subLevelData.AccountName,
                ParentID: subLevelData.ParentID,
                AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].SubLevel,
                AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
                AccountTypeId: data,
                AccountFilterTypeId: subLevelData.AccountFilterTypeId
            };
            // console.log('AccountType', subLevelDetail);
            this.editSubLevelAccountDetail(mainLevelData, controlLevelData, subLevelDetail);
            // this.editAnyAccountDetail(controlLevelDetail);
        }
    };
    //#endregion
    //#region "onBlurEditInputLevelAccountName"
    ChartOfAccountDetailComponent.prototype.onBlurEditInputLevelAccountName = function (mainLevelData, controlLevelData, subLevelData, inputLevelData, data) {
        if (data !== '') {
            var subLevelDetail = {
                ChartOfAccountNewId: inputLevelData.ChartOfAccountNewId,
                AccountName: data,
                ParentID: inputLevelData.ParentID,
                AccountLevelId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].InputLevel,
                AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
                AccountTypeId: inputLevelData.AccountTypeId,
                AccountFilterTypeId: inputLevelData.AccountFilterTypeId
            };
            this.editInputLevelAccountDetail(mainLevelData, controlLevelData, subLevelData, subLevelDetail);
            // this.editAnyAccountDetail(subLevelDetail);
        }
    };
    //#endregion
    //#region "openAddAccountDialog"
    ChartOfAccountDetailComponent.prototype.openAddAccountDialog = function (AccountLevel, mainLevelData, controlLevelData, subLevelData) {
        // NOTE: It passed the data into the Add Voucher Model
        var dialogRef = this.dialog.open(_add_account_add_account_component__WEBPACK_IMPORTED_MODULE_10__["AddAccountComponent"], {
            width: '450px',
            data: {
                AccountHeadType: this.ACCOUNT_HEAD_TYPE,
                AccountList: this.chartOfAccountList,
                mainLevelData: mainLevelData,
                AccountLevel: AccountLevel,
                controlLevelData: controlLevelData,
                subLevelData: subLevelData
            },
            autoFocus: false
        });
        // dialogRef.componentInstance.onListRefresh.subscribe(() => {
        //   this.getSavedExchangeRatesDate();
        // });
        dialogRef.afterClosed().subscribe(function (result) {
        });
    };
    //#endregion
    //#region "openAddAccountDialog"
    ChartOfAccountDetailComponent.prototype.onAddMainLevelAccountDialog = function () {
        this.openAddAccountDialog(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].MainLevel, null, null, null);
    };
    //#endregion
    //#region "openAddAccountDialog"
    ChartOfAccountDetailComponent.prototype.onAddControlLevelAccountDialog = function (mainLevelData) {
        this.openAddAccountDialog(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].ControlLevel, mainLevelData, null, null);
    };
    //#endregion
    //#region "openAddAccountDialog"
    ChartOfAccountDetailComponent.prototype.onAddSubLevelAccountDialog = function (mainLevelData, controlLevelData) {
        this.openAddAccountDialog(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].SubLevel, mainLevelData, controlLevelData, null);
    };
    //#endregion
    //#region "onAddInputLevelAccountDialog"
    ChartOfAccountDetailComponent.prototype.onAddInputLevelAccountDialog = function (mainLevelData, controlLevelData, subLevelData) {
        this.openAddAccountDialog(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountLevels"].InputLevel, mainLevelData, controlLevelData, subLevelData);
    };
    //#endregion
    ChartOfAccountDetailComponent.prototype.onDeleteChartOfAccount = function (accountDetail) {
        this.deleteChartOfAccount(accountDetail);
    };
    //#region "deleteChartOfAccount"
    ChartOfAccountDetailComponent.prototype.deleteChartOfAccount = function (accountDetail) {
        var _this = this;
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_9__["GLOBAL"].API_ChartOfAccount_DeleteChartOfAccount, accountDetail.ChartOfAccountNewId)
            .subscribe(function (response) {
            if (response.StatusCode === 200) {
                // do something
            }
            else if (response.StatusCode === 400) {
                _this.toastr.error(response.Message);
            }
        }, function (error) {
        });
    };
    //#endregion
    //#region "onDeleteMainLevel"
    ChartOfAccountDetailComponent.prototype.onDeleteMainLevel = function (mainLevelData) {
        this.deleteMainLevelAccountDetail(mainLevelData);
    };
    //#endregion
    //#region "onDeleteControlLevel"
    ChartOfAccountDetailComponent.prototype.onDeleteControlLevel = function (mainLevelData, controlLevelData) {
        this.deleteControlLevelAccountDetail(mainLevelData, controlLevelData);
    };
    //#endregion
    //#region "onDeleteSubLevel"
    ChartOfAccountDetailComponent.prototype.onDeleteSubLevel = function (mainLevelData, controlLevelData, subLevelData) {
        this.deleteSubLevelAccountDetail(mainLevelData, controlLevelData, subLevelData);
    };
    //#endregion
    //#region "onDeleteInputLevel"
    ChartOfAccountDetailComponent.prototype.onDeleteInputLevel = function (mainLevelData, controlLevelData, subLevelData, inputLevelData) {
        this.deleteInputLevelAccountDetail(mainLevelData, controlLevelData, subLevelData, inputLevelData);
    };
    //#endregion
    ChartOfAccountDetailComponent.prototype.onExportPdf = function () {
        var _this = this;
        this.cofPdfService.ExportChartOfAccountPdf().subscribe(function (response) {
            if (response.statusCode === 200) {
                // do something
                // console.log(response.data);
                // set dataSouce value for pdf
                _this.cofPdfService.dataSource = response.data;
                _this.cofPdfService.ExportToPdf();
            }
            else if (response.statusCode === 400) {
                _this.toastr.error(response.message);
            }
        }, function (error) {
        });
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], ChartOfAccountDetailComponent.prototype, "ACCOUNT_HEAD_TYPE", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", String)
    ], ChartOfAccountDetailComponent.prototype, "ACCOUNT_HEAD_NAME", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], ChartOfAccountDetailComponent.prototype, "getScreenSize", null);
    ChartOfAccountDetailComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-chart-of-account-detail',
            template: __webpack_require__(/*! ./chart-of-account-detail.component.html */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-account-detail/chart-of-account-detail.component.html"),
            styles: [__webpack_require__(/*! ./chart-of-account-detail.component.scss */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-account-detail/chart-of-account-detail.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_5__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_4__["AppUrlService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_6__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_7__["CommonLoaderService"],
            src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_8__["LocalStorageService"],
            _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MatDialog"],
            _chart_of_accounts_pdf_service__WEBPACK_IMPORTED_MODULE_11__["ChartOfAccountsPdfService"]])
    ], ChartOfAccountDetailComponent);
    return ChartOfAccountDetailComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts-pdf.service.ts":
/*!*****************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts-pdf.service.ts ***!
  \*****************************************************************************************/
/*! exports provided: ChartOfAccountsPdfService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ChartOfAccountsPdfService", function() { return ChartOfAccountsPdfService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/operators/map */ "./node_modules/rxjs/internal/operators/map.js");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_5__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var jsPDF = __webpack_require__(/*! jspdf */ "./node_modules/jspdf/dist/jspdf.min.js");
__webpack_require__(/*! jspdf-autotable */ "./node_modules/jspdf-autotable/dist/jspdf.plugin.autotable.js");
var ChartOfAccountsPdfService = /** @class */ (function () {
    function ChartOfAccountsPdfService(globalService, appurl) {
        this.globalService = globalService;
        this.appurl = appurl;
        this.margins = {
            top: 40,
            bottom: 40,
            left: 40,
            right: 40,
            width: 480
        };
        this.dataSource = [];
    }
    ChartOfAccountsPdfService.prototype.ExportChartOfAccountPdf = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Pdf_GetAllChartOfAccountHierarchyPdf)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_5__["map"])(function (x) {
            var responseData = {
                data: x.ResponseData,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    ChartOfAccountsPdfService.prototype.ExportToPdf = function () {
        var doc = new jsPDF('p', 'pt', 'a4');
        doc.setFontSize(50);
        var pageHeight = doc.internal.pageSize.height;
        var pageWidth = doc.internal.pageSize.width;
        doc.setFontSize(14);
        var linePadding = 20;
        var pageMiddle = this.margins.width - this.margins.width / 2 + this.margins.left;
        var splitTitle = '';
        var textCurrentLocationYAxis = this.margins.top;
        // main heading
        splitTitle = doc.splitTextToSize('COORDINATION OF HUMANITARIAN ASSISTANCE', this.margins.width);
        for (var i = 0; i < splitTitle.length; i++) {
            doc.text(src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_1__["StaticUtilities"].pdfTextCenter(doc, splitTitle[i], 14), textCurrentLocationYAxis, splitTitle[i]);
            textCurrentLocationYAxis += linePadding;
        }
        // sub heading
        splitTitle = doc.splitTextToSize('CHART OF ACCOUNTS', this.margins.width);
        for (var i = 0; i < splitTitle.length; i++) {
            doc.text(src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_1__["StaticUtilities"].pdfTextCenter(doc, splitTitle[i], 14), textCurrentLocationYAxis, splitTitle[i]);
            textCurrentLocationYAxis += linePadding;
        }
        doc.line(this.margins.left, textCurrentLocationYAxis, pageWidth - this.margins.right, textCurrentLocationYAxis);
        doc.setFontSize(11);
        // col 1
        var inlineHeaderContentColSm6 = (textCurrentLocationYAxis += linePadding);
        splitTitle = doc.splitTextToSize('A/C No.', this.margins.width - pageMiddle);
        for (var i = 0; i < splitTitle.length; i++) {
            if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
                textCurrentLocationYAxis = this.margins.top;
                doc.addPage();
            }
            doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
            textCurrentLocationYAxis += linePadding;
        }
        linePadding = 10;
        // col 2
        // use to start the 2nd column content from 1st column content y-axis
        textCurrentLocationYAxis = inlineHeaderContentColSm6;
        splitTitle = doc.splitTextToSize('Account Description', this.margins.width - pageMiddle);
        for (var i = 0; i < splitTitle.length; i++) {
            if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
                textCurrentLocationYAxis = this.margins.top;
                doc.addPage();
            }
            doc.text(pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
            textCurrentLocationYAxis += linePadding;
        }
        doc.line(this.margins.left, textCurrentLocationYAxis, pageWidth - this.margins.right, textCurrentLocationYAxis);
        textCurrentLocationYAxis += linePadding;
        // main level
        for (var _i = 0, _a = this.dataSource; _i < _a.length; _i++) {
            var itemMain = _a[_i];
            if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
                textCurrentLocationYAxis = this.margins.top;
                doc.addPage();
            }
            // col 1
            inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
            doc.text(this.margins.left, textCurrentLocationYAxis, itemMain.ChartOfAccountCode);
            textCurrentLocationYAxis += linePadding;
            // col 2
            // use to start the 2nd column content from 1st column content y-axis
            textCurrentLocationYAxis = inlineHeaderContentColSm6;
            splitTitle = doc.splitTextToSize(itemMain.AccountName, this.margins.width - this.margins.right);
            for (var i = 0; i < splitTitle.length; i++) {
                doc.text(this.margins.left + 80, textCurrentLocationYAxis, splitTitle[i]);
                textCurrentLocationYAxis += linePadding;
                if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
                    textCurrentLocationYAxis = this.margins.top;
                    doc.addPage();
                }
            }
            // control level
            for (var _b = 0, _c = itemMain.ChildAccounts; _b < _c.length; _b++) {
                var itemControl = _c[_b];
                // col 1
                inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
                doc.text(this.margins.left, textCurrentLocationYAxis, itemControl.ChartOfAccountCode);
                textCurrentLocationYAxis += linePadding;
                // col 2
                // use to start the 2nd column content from 1st column content y-axis
                textCurrentLocationYAxis = inlineHeaderContentColSm6;
                splitTitle = doc.splitTextToSize(itemControl.AccountName, this.margins.width - this.margins.right);
                for (var i = 0; i < splitTitle.length; i++) {
                    doc.text(this.margins.left + 110, textCurrentLocationYAxis, splitTitle[i]);
                    textCurrentLocationYAxis += linePadding;
                    if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
                        textCurrentLocationYAxis = this.margins.top;
                        doc.addPage();
                    }
                }
                // sub level
                for (var _d = 0, _e = itemControl.ChildAccounts; _d < _e.length; _d++) {
                    var itemSub = _e[_d];
                    // col 1
                    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
                    doc.text(this.margins.left, textCurrentLocationYAxis, itemSub.ChartOfAccountCode);
                    textCurrentLocationYAxis += linePadding;
                    // col 2
                    // use to start the 2nd column content from 1st column content y-axis
                    textCurrentLocationYAxis = inlineHeaderContentColSm6;
                    splitTitle = doc.splitTextToSize(itemSub.AccountName, this.margins.width - this.margins.right);
                    for (var i = 0; i < splitTitle.length; i++) {
                        doc.text(this.margins.left + 140, textCurrentLocationYAxis, splitTitle[i]);
                        textCurrentLocationYAxis += linePadding;
                        if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
                            textCurrentLocationYAxis = this.margins.top;
                            doc.addPage();
                        }
                    }
                    // input level
                    for (var _f = 0, _g = itemSub.ChildAccounts; _f < _g.length; _f++) {
                        var itemInput = _g[_f];
                        // col 1
                        inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
                        doc.text(this.margins.left, textCurrentLocationYAxis, itemInput.ChartOfAccountCode);
                        textCurrentLocationYAxis += linePadding;
                        // col 2
                        // use to start the 2nd column content from 1st column content y-axis
                        textCurrentLocationYAxis = inlineHeaderContentColSm6;
                        splitTitle = doc.splitTextToSize(itemInput.AccountName, this.margins.width - this.margins.right);
                        for (var i = 0; i < splitTitle.length; i++) {
                            doc.text(this.margins.left + 170, textCurrentLocationYAxis, splitTitle[i]);
                            textCurrentLocationYAxis += linePadding;
                            if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
                                textCurrentLocationYAxis = this.margins.top;
                                doc.addPage();
                            }
                        }
                    }
                }
            }
            textCurrentLocationYAxis += 20;
        }
        doc.save('chart-of-account.pdf');
    };
    ChartOfAccountsPdfService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_2__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_3__["AppUrlService"]])
    ], ChartOfAccountsPdfService);
    return ChartOfAccountsPdfService;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts-routing.module.ts":
/*!********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts-routing.module.ts ***!
  \********************************************************************************************/
/*! exports provided: ChartOfAccountsRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ChartOfAccountsRoutingModule", function() { return ChartOfAccountsRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _chart_of_accounts_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./chart-of-accounts.component */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.ts");
/* harmony import */ var _assets_assets_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./assets/assets.component */ "./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.ts");
/* harmony import */ var src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/services/role-guard */ "./src/app/shared/services/role-guard.ts");
/* harmony import */ var src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var _liabilities_liabilities_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./liabilities/liabilities.component */ "./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.ts");
/* harmony import */ var _funds_funds_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./funds/funds.component */ "./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.ts");
/* harmony import */ var _income_income_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./income/income.component */ "./src/app/dashboard/accounting/chart-of-accounts/income/income.component.ts");
/* harmony import */ var _expense_expense_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./expense/expense.component */ "./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};










var ModuleId = src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_5__["ApplicationModule"].AccountingNew;
var routes = [
    {
        path: '',
        component: _chart_of_accounts_component__WEBPACK_IMPORTED_MODULE_2__["ChartOfAccountsComponent"],
        children: [
            {
                path: '',
                redirectTo: 'assets',
                pathMatch: 'full'
            },
            {
                path: 'assets',
                component: _assets_assets_component__WEBPACK_IMPORTED_MODULE_3__["AssetsComponent"],
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    page: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_5__["accountingNewMaster"].Assets
                }
            },
            {
                path: 'liabilities',
                component: _liabilities_liabilities_component__WEBPACK_IMPORTED_MODULE_6__["LiabilitiesComponent"],
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    page: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_5__["accountingNewMaster"].Liabilities
                }
            },
            { path: 'funds', component: _funds_funds_component__WEBPACK_IMPORTED_MODULE_7__["FundsComponent"] },
            {
                path: 'income',
                component: _income_income_component__WEBPACK_IMPORTED_MODULE_8__["IncomeComponent"],
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    page: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_5__["accountingNewMaster"].Income
                }
            },
            {
                path: 'expense',
                component: _expense_expense_component__WEBPACK_IMPORTED_MODULE_9__["ExpenseComponent"],
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    page: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_5__["accountingNewMaster"].Expense
                }
            }
        ]
    }
];
var ChartOfAccountsRoutingModule = /** @class */ (function () {
    function ChartOfAccountsRoutingModule() {
    }
    ChartOfAccountsRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], ChartOfAccountsRoutingModule);
    return ChartOfAccountsRoutingModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.html":
/*!*****************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.html ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"main_body\">\r\n  <router-outlet></router-outlet>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.scss":
/*!*****************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.scss ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2NoYXJ0LW9mLWFjY291bnRzL2NoYXJ0LW9mLWFjY291bnRzLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.ts":
/*!***************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.ts ***!
  \***************************************************************************************/
/*! exports provided: ChartOfAccountsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ChartOfAccountsComponent", function() { return ChartOfAccountsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/localstorage.service */ "./src/app/shared/services/localstorage.service.ts");
/* harmony import */ var src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var ChartOfAccountsComponent = /** @class */ (function () {
    function ChartOfAccountsComponent(globalService, localStorageservice) {
        this.globalService = globalService;
        this.localStorageservice = localStorageservice;
        this.setSelectedHeader = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_4__["UIModuleHeaders"].ChartOfAccountHeader;
        this.setProjectHeader = 'Chart Of Accounts';
        this.menuList = [
            {
                Id: 1,
                PageId: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__["accountingNewMaster"].Assets,
                Text: 'Assets',
                Link: '/accounting/chart-of-accounts/assets'
            },
            {
                Id: 2,
                PageId: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__["accountingNewMaster"].Liabilities,
                Text: 'Liabilities',
                Link: '/accounting/chart-of-accounts/liabilities'
            },
            {
                Id: 3,
                PageId: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__["accountingNewMaster"].Income,
                Text: 'Income',
                Link: '/accounting/chart-of-accounts/income'
            },
            {
                Id: 4,
                PageId: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__["accountingNewMaster"].Expense,
                Text: 'Expense',
                Link: '/accounting/chart-of-accounts/expense'
            }
        ];
        this.authorizedMenuList = [];
        // Set Menu Header Name
        this.globalService.setMenuHeaderName(this.setProjectHeader);
        this.authorizedMenuList = this.localStorageservice.GetAuthorizedPages(this.menuList);
        // Set Menu Header List
        this.globalService.setMenuList(this.authorizedMenuList);
    }
    ChartOfAccountsComponent.prototype.ngOnInit = function () { };
    ChartOfAccountsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-chart-of-accounts',
            template: __webpack_require__(/*! ./chart-of-accounts.component.html */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.html"),
            styles: [__webpack_require__(/*! ./chart-of-accounts.component.scss */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_3__["GlobalSharedService"],
            src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_1__["LocalStorageService"]])
    ], ChartOfAccountsComponent);
    return ChartOfAccountsComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.module.ts":
/*!************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.module.ts ***!
  \************************************************************************************/
/*! exports provided: ChartOfAccountsModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ChartOfAccountsModule", function() { return ChartOfAccountsModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _chart_of_accounts_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./chart-of-accounts-routing.module */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts-routing.module.ts");
/* harmony import */ var _add_account_add_account_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./add-account/add-account.component */ "./src/app/dashboard/accounting/chart-of-accounts/add-account/add-account.component.ts");
/* harmony import */ var _assets_assets_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./assets/assets.component */ "./src/app/dashboard/accounting/chart-of-accounts/assets/assets.component.ts");
/* harmony import */ var _expense_expense_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./expense/expense.component */ "./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.ts");
/* harmony import */ var _funds_funds_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./funds/funds.component */ "./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.ts");
/* harmony import */ var _income_income_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./income/income.component */ "./src/app/dashboard/accounting/chart-of-accounts/income/income.component.ts");
/* harmony import */ var _liabilities_liabilities_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./liabilities/liabilities.component */ "./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_input__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/material/input */ "./node_modules/@angular/material/esm5/input.es5.js");
/* harmony import */ var _angular_material_button__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/material/button */ "./node_modules/@angular/material/esm5/button.es5.js");
/* harmony import */ var _angular_material_card__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/material/card */ "./node_modules/@angular/material/esm5/card.es5.js");
/* harmony import */ var _angular_material_icon__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material/icon */ "./node_modules/@angular/material/esm5/icon.es5.js");
/* harmony import */ var _angular_material_select__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/material/select */ "./node_modules/@angular/material/esm5/select.es5.js");
/* harmony import */ var _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @angular/material/progress-spinner */ "./node_modules/@angular/material/esm5/progress-spinner.es5.js");
/* harmony import */ var primeng_primeng__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! primeng/primeng */ "./node_modules/primeng/primeng.js");
/* harmony import */ var primeng_primeng__WEBPACK_IMPORTED_MODULE_16___default = /*#__PURE__*/__webpack_require__.n(primeng_primeng__WEBPACK_IMPORTED_MODULE_16__);
/* harmony import */ var _chart_of_accounts_component__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ./chart-of-accounts.component */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts.component.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _chart_of_account_detail_chart_of_account_detail_component__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ./chart-of-account-detail/chart-of-account-detail.component */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-account-detail/chart-of-account-detail.component.ts");
/* harmony import */ var _chart_of_accounts_pdf_service__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ./chart-of-accounts-pdf.service */ "./src/app/dashboard/accounting/chart-of-accounts/chart-of-accounts-pdf.service.ts");
/* harmony import */ var projects_library_src_sub_header_template_sub_header_template_module__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! projects/library/src/sub-header-template/sub-header-template.module */ "./projects/library/src/sub-header-template/sub-header-template.module.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






















var ChartOfAccountsModule = /** @class */ (function () {
    function ChartOfAccountsModule() {
    }
    ChartOfAccountsModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _chart_of_accounts_component__WEBPACK_IMPORTED_MODULE_17__["ChartOfAccountsComponent"],
                _add_account_add_account_component__WEBPACK_IMPORTED_MODULE_3__["AddAccountComponent"],
                _assets_assets_component__WEBPACK_IMPORTED_MODULE_4__["AssetsComponent"],
                _expense_expense_component__WEBPACK_IMPORTED_MODULE_5__["ExpenseComponent"],
                _funds_funds_component__WEBPACK_IMPORTED_MODULE_6__["FundsComponent"],
                _income_income_component__WEBPACK_IMPORTED_MODULE_7__["IncomeComponent"],
                _liabilities_liabilities_component__WEBPACK_IMPORTED_MODULE_8__["LiabilitiesComponent"],
                _chart_of_account_detail_chart_of_account_detail_component__WEBPACK_IMPORTED_MODULE_19__["ChartOfAccountDetailComponent"]
            ],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _chart_of_accounts_routing_module__WEBPACK_IMPORTED_MODULE_2__["ChartOfAccountsRoutingModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_9__["FormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_9__["ReactiveFormsModule"],
                projects_library_src_sub_header_template_sub_header_template_module__WEBPACK_IMPORTED_MODULE_21__["SubHeaderTemplateModule"],
                // material
                _angular_material_input__WEBPACK_IMPORTED_MODULE_10__["MatInputModule"],
                _angular_material_button__WEBPACK_IMPORTED_MODULE_11__["MatButtonModule"],
                _angular_material_card__WEBPACK_IMPORTED_MODULE_12__["MatCardModule"],
                _angular_material_dialog__WEBPACK_IMPORTED_MODULE_18__["MatDialogModule"],
                _angular_material_icon__WEBPACK_IMPORTED_MODULE_13__["MatIconModule"],
                _angular_material_select__WEBPACK_IMPORTED_MODULE_14__["MatSelectModule"],
                _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_15__["MatProgressSpinnerModule"],
                primeng_primeng__WEBPACK_IMPORTED_MODULE_16__["TooltipModule"]
            ],
            entryComponents: [_add_account_add_account_component__WEBPACK_IMPORTED_MODULE_3__["AddAccountComponent"]],
            providers: [_chart_of_accounts_pdf_service__WEBPACK_IMPORTED_MODULE_20__["ChartOfAccountsPdfService"]]
        })
    ], ChartOfAccountsModule);
    return ChartOfAccountsModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.html":
/*!***************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.html ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<app-chart-of-account-detail\r\n  [ACCOUNT_HEAD_TYPE]=\"ACCOUNT_HEAD_TYPE\"\r\n  [ACCOUNT_HEAD_NAME]=\"ACCOUNT_HEAD_NAME\"\r\n>\r\n</app-chart-of-account-detail>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.scss":
/*!***************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.scss ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2NoYXJ0LW9mLWFjY291bnRzL2V4cGVuc2UvZXhwZW5zZS5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.ts":
/*!*************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.ts ***!
  \*************************************************************************************/
/*! exports provided: ExpenseComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExpenseComponent", function() { return ExpenseComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var ExpenseComponent = /** @class */ (function () {
    function ExpenseComponent() {
        this.ACCOUNT_HEAD_TYPE = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"].Expense;
        this.ACCOUNT_HEAD_NAME = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"][this.ACCOUNT_HEAD_TYPE];
    }
    ExpenseComponent.prototype.ngOnInit = function () {
    };
    ExpenseComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-expense',
            template: __webpack_require__(/*! ./expense.component.html */ "./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.html"),
            styles: [__webpack_require__(/*! ./expense.component.scss */ "./src/app/dashboard/accounting/chart-of-accounts/expense/expense.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], ExpenseComponent);
    return ExpenseComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.html":
/*!***********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.html ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<app-chart-of-account-detail\r\n  [ACCOUNT_HEAD_TYPE]=\"ACCOUNT_HEAD_TYPE\"\r\n  [ACCOUNT_HEAD_NAME]=\"ACCOUNT_HEAD_NAME\"\r\n>\r\n</app-chart-of-account-detail>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.scss":
/*!***********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.scss ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".margin_left_10 {\n  margin-left: 10px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvY2hhcnQtb2YtYWNjb3VudHMvZnVuZHMvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcZGFzaGJvYXJkXFxhY2NvdW50aW5nXFxjaGFydC1vZi1hY2NvdW50c1xcZnVuZHNcXGZ1bmRzLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0UsaUJBQWlCLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9kYXNoYm9hcmQvYWNjb3VudGluZy9jaGFydC1vZi1hY2NvdW50cy9mdW5kcy9mdW5kcy5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5tYXJnaW5fbGVmdF8xMHtcclxuICBtYXJnaW4tbGVmdDogMTBweDtcclxufVxyXG4iXX0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.ts":
/*!*********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.ts ***!
  \*********************************************************************************/
/*! exports provided: FundsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FundsComponent", function() { return FundsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var FundsComponent = /** @class */ (function () {
    function FundsComponent() {
        this.ACCOUNT_HEAD_TYPE = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"].OwnersEquity;
        this.ACCOUNT_HEAD_NAME = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"][this.ACCOUNT_HEAD_TYPE];
    }
    FundsComponent.prototype.ngOnInit = function () {
    };
    FundsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-funds',
            template: __webpack_require__(/*! ./funds.component.html */ "./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.html"),
            styles: [__webpack_require__(/*! ./funds.component.scss */ "./src/app/dashboard/accounting/chart-of-accounts/funds/funds.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], FundsComponent);
    return FundsComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/income/income.component.html":
/*!*************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/income/income.component.html ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<app-chart-of-account-detail\r\n  [ACCOUNT_HEAD_TYPE]=\"ACCOUNT_HEAD_TYPE\"\r\n  [ACCOUNT_HEAD_NAME]=\"ACCOUNT_HEAD_NAME\"\r\n>\r\n</app-chart-of-account-detail>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/income/income.component.scss":
/*!*************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/income/income.component.scss ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2NoYXJ0LW9mLWFjY291bnRzL2luY29tZS9pbmNvbWUuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/income/income.component.ts":
/*!***********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/income/income.component.ts ***!
  \***********************************************************************************/
/*! exports provided: IncomeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "IncomeComponent", function() { return IncomeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var IncomeComponent = /** @class */ (function () {
    function IncomeComponent() {
        this.ACCOUNT_HEAD_TYPE = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"].Income;
        this.ACCOUNT_HEAD_NAME = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"][this.ACCOUNT_HEAD_TYPE];
    }
    IncomeComponent.prototype.ngOnInit = function () {
    };
    IncomeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-income',
            template: __webpack_require__(/*! ./income.component.html */ "./src/app/dashboard/accounting/chart-of-accounts/income/income.component.html"),
            styles: [__webpack_require__(/*! ./income.component.scss */ "./src/app/dashboard/accounting/chart-of-accounts/income/income.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], IncomeComponent);
    return IncomeComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.html":
/*!***********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.html ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<app-chart-of-account-detail\r\n  [ACCOUNT_HEAD_TYPE]=\"ACCOUNT_HEAD_TYPE\"\r\n  [ACCOUNT_HEAD_NAME]=\"ACCOUNT_HEAD_NAME\"\r\n>\r\n</app-chart-of-account-detail>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.scss":
/*!***********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.scss ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2NoYXJ0LW9mLWFjY291bnRzL2xpYWJpbGl0aWVzL2xpYWJpbGl0aWVzLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.ts":
/*!*********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.ts ***!
  \*********************************************************************************************/
/*! exports provided: LiabilitiesComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LiabilitiesComponent", function() { return LiabilitiesComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var LiabilitiesComponent = /** @class */ (function () {
    function LiabilitiesComponent() {
        this.ACCOUNT_HEAD_TYPE = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"].Liabilities;
        this.ACCOUNT_HEAD_NAME = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"][this.ACCOUNT_HEAD_TYPE];
    }
    LiabilitiesComponent.prototype.ngOnInit = function () {
    };
    LiabilitiesComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-liabilities',
            template: __webpack_require__(/*! ./liabilities.component.html */ "./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.html"),
            styles: [__webpack_require__(/*! ./liabilities.component.scss */ "./src/app/dashboard/accounting/chart-of-accounts/liabilities/liabilities.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], LiabilitiesComponent);
    return LiabilitiesComponent;
}());



/***/ }),

/***/ "./src/app/shared/static-utilities.ts":
/*!********************************************!*\
  !*** ./src/app/shared/static-utilities.ts ***!
  \********************************************/
/*! exports provided: StaticUtilities */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StaticUtilities", function() { return StaticUtilities; });
var StaticUtilities = /** @class */ (function () {
    function StaticUtilities() {
    }
    StaticUtilities.pdfTextCenter = function (doc, text, fontsize) {
        var pageWidth = doc.internal.pageSize.width;
        var txtWidth = (doc.getStringUnitWidth(text) * fontsize) / doc.internal.scaleFactor;
        return (pageWidth - txtWidth) / 2;
    };
    StaticUtilities.pdfTextRight = function (doc, text, fontsize) {
        var pageWidth = doc.internal.pageSize.width;
        var txtWidth = (doc.getStringUnitWidth(text) * fontsize) / doc.internal.scaleFactor;
        return pageWidth - txtWidth - 10;
    };
    //#region "setLocalDate"
    StaticUtilities.setLocalDate = function (date) {
        if (date === null || date === undefined) {
            return null;
        }
        else {
            return new Date(new Date(date).getTime() - new Date().getTimezoneOffset() * 60000);
        }
    };
    //#endregion
    //#region "getLocalDate"
    StaticUtilities.getLocalDate = function (date) {
        return new Date(new Date(date).getFullYear(), new Date(date).getMonth(), new Date(date).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds(), new Date().getMilliseconds());
    };
    StaticUtilities.groupBy = function (list, keyGetter) {
        var newmap = new Map();
        list.forEach(function (item) {
            var key = keyGetter(item);
            var collection = newmap.get(key);
            if (!collection) {
                newmap.set(key, [item]);
            }
            else {
                collection.push(item);
            }
        });
        newmap = new Map(Array.from(newmap).sort(function (a, b) { return a[0] > b[0] ? 1 : -1; }));
        return newmap;
    };
    return StaticUtilities;
}());



/***/ })

}]);
//# sourceMappingURL=chart-of-accounts-chart-of-accounts-module.js.map