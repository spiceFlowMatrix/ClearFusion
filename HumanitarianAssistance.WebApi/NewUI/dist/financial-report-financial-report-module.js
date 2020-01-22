(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["financial-report-financial-report-module"],{

/***/ "./projects/library/src/lib/library.service.ts":
/*!*****************************************************!*\
  !*** ./projects/library/src/lib/library.service.ts ***!
  \*****************************************************/
/*! exports provided: LibraryService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LibraryService", function() { return LibraryService; });
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

var LibraryService = /** @class */ (function () {
    function LibraryService() {
    }
    LibraryService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [])
    ], LibraryService);
    return LibraryService;
}());



/***/ }),

/***/ "./projects/library/src/public_api.ts":
/*!********************************************!*\
  !*** ./projects/library/src/public_api.ts ***!
  \********************************************/
/*! exports provided: LibraryModule, LibraryService, LibraryComponent, SubHeaderTemplateModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _lib_library_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./lib/library.service */ "./projects/library/src/lib/library.service.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LibraryService", function() { return _lib_library_service__WEBPACK_IMPORTED_MODULE_0__["LibraryService"]; });

/* harmony import */ var _lib_library_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./lib/library.component */ "./projects/library/src/lib/library.component.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LibraryComponent", function() { return _lib_library_component__WEBPACK_IMPORTED_MODULE_1__["LibraryComponent"]; });

/* harmony import */ var _lib_library_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./lib/library.module */ "./projects/library/src/lib/library.module.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "LibraryModule", function() { return _lib_library_module__WEBPACK_IMPORTED_MODULE_2__["LibraryModule"]; });

/* harmony import */ var _sub_header_template_sub_header_template_module__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./sub-header-template/sub-header-template.module */ "./projects/library/src/sub-header-template/sub-header-template.module.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "SubHeaderTemplateModule", function() { return _sub_header_template_sub_header_template_module__WEBPACK_IMPORTED_MODULE_3__["SubHeaderTemplateModule"]; });

/*
 * Public API Surface of library
 */
// library



// sub-header-template



/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.html":
/*!**************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.html ***!
  \**************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"body-content\">\r\n  <div class=\"balance-sheet-main\">\r\n    <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-10 col-sm-offset-1\">\r\n          <mat-card [ngStyle]=\"scrollStyles\">\r\n            <div class=\"row border_bottom\">\r\n              <div class=\"col-sm-10\">\r\n                <h1 class=\"main_heading\">\r\n                  Balance Sheet\r\n                  <span class=\"font_smallcaps_12\">as of {{ asOfDate }} </span>\r\n                </h1>\r\n              </div>\r\n              <div class=\"col-sm-1\">\r\n                <button\r\n                  mat-icon-button\r\n                  class=\"pull-right\"\r\n                  (click)=\"onExportPdf()\"\r\n                  pTooltip=\"Export Pdf\"\r\n                  tooltipPosition=\"top\"\r\n                >\r\n                  <mat-icon aria-label=\"Export pdf\"\r\n                    >vertical_align_bottom</mat-icon\r\n                  >\r\n                </button>\r\n              </div>\r\n              <div class=\"col-sm-1\">\r\n                <button\r\n                  mat-icon-button\r\n                  class=\"pull-right\"\r\n                  [routerLink]=\"'../../../'\"\r\n                >\r\n                  <mat-icon>settings</mat-icon>\r\n                </button>\r\n              </div>\r\n            </div>\r\n            <div class=\"margin_top_50\">\r\n              <h4 class=\"main_heading bdr_heading\">\r\n                Assets <span class=\"font_smallcaps_12\">Property & Capital</span>\r\n              </h4>\r\n            </div>\r\n            <div class=\"row\" class=\"col-12-sm\">\r\n              <table class=\"collapsed table table-bordered\">\r\n                <tr\r\n                  *ngFor=\"let item of assetsList\"\r\n                  class=\"col-12-sm\"\r\n                  let\r\n                  ia=\"index\"\r\n                >\r\n                  <table class=\"collapsed table table-bordered\">\r\n                    <tr>\r\n                      <td\r\n                        class=\"font_caps_12\"\r\n                        style=\"width:100%\"\r\n                        (click)=\"showa = !showa\"\r\n                      >\r\n                        {{ item.NoteName }}\r\n                        <i class=\"fa fa-ellipsis-h\" aria-hidden=\"true\"></i>\r\n                      </td>\r\n                      <td\r\n                        class=\"font_caps_12 right_align\"\r\n                        (click)=\"showa = !showa\"\r\n                      >\r\n                        {{ item.NoteBalance | number: \"5.2-3\" }}\r\n                      </td>\r\n                    </tr>\r\n                    <tr *ngFor=\"let balance of item.AccountBalances\">\r\n                      <td\r\n                        class=\"font_caps_10 \"\r\n                        style=\"width:100%\"\r\n                        *ngIf=\"showa\"\r\n                      >\r\n                        {{ balance.AccountName }}\r\n                      </td>\r\n                      <td class=\"font_caps_10 right_align\" *ngIf=\"showa\">\r\n                        {{ balance.Balance | number: \"5.2-3\" }}\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </tr>\r\n              </table>\r\n            </div>\r\n\r\n            <div class=\"margin_top_50\">\r\n              <h4 class=\"main_heading bdr_heading\">Liabilities</h4>\r\n            </div>\r\n            <div class=\"row\" class=\"col-12-sm\">\r\n              <table class=\"collapsed table table-bordered\">\r\n                <tr\r\n                  *ngFor=\"let item of liabilitiesList\"\r\n                  class=\"col-12-sm\"\r\n                  let\r\n                  ib=\"index\"\r\n                >\r\n                  <table class=\"collapsed table table-bordered\">\r\n                    <tr>\r\n                      <td\r\n                        class=\"font_caps_12\"\r\n                        style=\"width:100%\"\r\n                        (click)=\"showb = !showb\"\r\n                      >\r\n                        {{ item.NoteName }}\r\n                        <i class=\"fa fa-ellipsis-h\" aria-hidden=\"true\"></i>\r\n                      </td>\r\n                      <td class=\"font_caps_12 right_align\">\r\n                        {{ item.NoteBalance | number: \"5.2-3\" }}\r\n                      </td>\r\n                    </tr>\r\n                    <tr *ngFor=\"let balance of item.AccountBalances\">\r\n                      <td\r\n                        class=\"font_caps_10 \"\r\n                        style=\"width:100%\"\r\n                        *ngIf=\"showb\"\r\n                      >\r\n                        {{ balance.AccountName }}\r\n                      </td>\r\n                      <td class=\"font_caps_10 right_align\" *ngIf=\"showb\">\r\n                        {{ balance.Balance | number: \"5.2-3\" }}\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </tr>\r\n              </table>\r\n            </div>\r\n            <div class=\"project-details margin_top_50\">\r\n              <h4 class=\"main_heading bdr_heading\"></h4>\r\n            </div>\r\n            <div>\r\n              <table style=\"width:100%;text-align:center;\">\r\n                <tr class=\"font_caps_12_bold\">\r\n                  <td>\r\n                    {{ totalLiabities | number: \"5.2-3\" }}\r\n                  </td>\r\n                  <td></td>\r\n                  <td>\r\n                    {{ totalNett | number: \"5.2-3\" }}\r\n                  </td>\r\n                  <td></td>\r\n                  <td>\r\n                    {{ totalAssets | number: \"5.2-3\" }}\r\n                  </td>\r\n                </tr>\r\n                <tr class=\"font_smallcaps_12\">\r\n                  <td style=\"width:33.3%\">Liabilities</td>\r\n                  <td>+</td>\r\n                  <td style=\"width:33.3%\">Nett</td>\r\n                  <td>=</td>\r\n                  <td style=\"width:33.3%\">Assets</td>\r\n                </tr>\r\n              </table>\r\n            </div>\r\n          </mat-card>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.scss":
/*!**************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.scss ***!
  \**************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".font_caps_12 {\n  text-transform: uppercase; }\n\n.font_caps_12_bold {\n  text-transform: uppercase;\n  font-weight: 600; }\n\n.font_smallcaps_12 {\n  font-variant-caps: all-small-caps;\n  text-transform: uppercase; }\n\n.font_caps_10 {\n  padding-top: 2px;\n  padding-bottom: px;\n  font-variant-caps: all-petite-caps;\n  text-transform: uppercase; }\n\n.right_align {\n  text-align: right; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvZmluYW5jaWFsLXJlcG9ydC9iYWxhbmNlLXNoZWV0L2JhbGFuY2Utc2hlZXQtdmlldy9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxkYXNoYm9hcmRcXGFjY291bnRpbmdcXGZpbmFuY2lhbC1yZXBvcnRcXGJhbGFuY2Utc2hlZXRcXGJhbGFuY2Utc2hlZXQtdmlld1xcYmFsYW5jZS1zaGVldC12aWV3LmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0kseUJBQXlCLEVBQUE7O0FBRzdCO0VBQ0kseUJBQXlCO0VBQ3pCLGdCQUFnQixFQUFBOztBQUdwQjtFQUNJLGlDQUFpQztFQUNqQyx5QkFBeUIsRUFBQTs7QUFHN0I7RUFDSSxnQkFBZTtFQUNmLGtCQUFpQjtFQUNqQixrQ0FBa0M7RUFDbEMseUJBQXlCLEVBQUE7O0FBRzdCO0VBQ0ksaUJBQWlCLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9kYXNoYm9hcmQvYWNjb3VudGluZy9maW5hbmNpYWwtcmVwb3J0L2JhbGFuY2Utc2hlZXQvYmFsYW5jZS1zaGVldC12aWV3L2JhbGFuY2Utc2hlZXQtdmlldy5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5mb250X2NhcHNfMTIge1xyXG4gICAgdGV4dC10cmFuc2Zvcm06IHVwcGVyY2FzZTtcclxufVxyXG5cclxuLmZvbnRfY2Fwc18xMl9ib2xkIHtcclxuICAgIHRleHQtdHJhbnNmb3JtOiB1cHBlcmNhc2U7XHJcbiAgICBmb250LXdlaWdodDogNjAwO1xyXG59XHJcblxyXG4uZm9udF9zbWFsbGNhcHNfMTIge1xyXG4gICAgZm9udC12YXJpYW50LWNhcHM6IGFsbC1zbWFsbC1jYXBzO1xyXG4gICAgdGV4dC10cmFuc2Zvcm06IHVwcGVyY2FzZTtcclxufVxyXG5cclxuLmZvbnRfY2Fwc18xMCB7XHJcbiAgICBwYWRkaW5nLXRvcDoycHg7XHJcbiAgICBwYWRkaW5nLWJvdHRvbTpweDtcclxuICAgIGZvbnQtdmFyaWFudC1jYXBzOiBhbGwtcGV0aXRlLWNhcHM7XHJcbiAgICB0ZXh0LXRyYW5zZm9ybTogdXBwZXJjYXNlO1xyXG59XHJcblxyXG4ucmlnaHRfYWxpZ24ge1xyXG4gICAgdGV4dC1hbGlnbjogcmlnaHQ7XHJcbn1cclxuXHJcblxyXG4gICJdfQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.ts":
/*!************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.ts ***!
  \************************************************************************************************************************/
/*! exports provided: BalanceSheetViewComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BalanceSheetViewComponent", function() { return BalanceSheetViewComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _balance_sheet_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../balance-sheet.service */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.service.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var jspdf__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! jspdf */ "./node_modules/jspdf/dist/jspdf.min.js");
/* harmony import */ var jspdf__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(jspdf__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var src_app_shared_pipes_currency_code_pipe__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! src/app/shared/pipes/currency-code.pipe */ "./src/app/shared/pipes/currency-code.pipe.ts");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};











var BalanceSheetViewComponent = /** @class */ (function () {
    //#endregion
    function BalanceSheetViewComponent(globalService, balanceSheetService, currencyCodePipe, appUrl, route, toastr) {
        this.globalService = globalService;
        this.balanceSheetService = balanceSheetService;
        this.currencyCodePipe = currencyCodePipe;
        this.appUrl = appUrl;
        this.route = route;
        this.toastr = toastr;
        //#region "variables"
        // CONST
        this.ASSETS_ID = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_4__["AccountHeadTypes_Enum"].Assets;
        this.LIABILITY_ID = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_4__["AccountHeadTypes_Enum"].Liabilities;
        // DATASOURCE
        this.assetsList = [];
        this.liabilitiesList = [];
        this.currencyList = [];
        this.totalAssets = 0;
        this.totalLiabities = 0;
        this.totalEquity = 0;
        this.totalNett = 0;
        // FLAG
        this.inputFieldAssetsFlag = false;
        this.inputFieldLiabilitiesFlag = false;
        this.inputFieldDonorsEquityFlag = false;
        this.showa = [];
        this.showb = [];
        this.getScreenSize();
    }
    BalanceSheetViewComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.asOfDate = params.asOfDate;
            _this.currency = +params.currency;
            _this.getAllCurrency();
            _this.getBalanceSheetAssetAccountBalances();
            _this.getBalanceSheetLiabilityAccountBalances();
        });
    };
    //#region "Dynamic Scroll"
    BalanceSheetViewComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    BalanceSheetViewComponent.prototype.numberWithCommas = function (x) {
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    };
    BalanceSheetViewComponent.prototype.getSum = function (list, field) {
        var sum = 0;
        if (list != null) {
            for (var i = 0; i < list.length; i++) {
                sum += list[i][field];
            }
        }
        return sum;
    };
    //#region "getAllCurrency"
    BalanceSheetViewComponent.prototype.getAllCurrency = function () {
        var _this = this;
        this.currencyList = [];
        this.balanceSheetService.GetCurrencyList().subscribe(function (response) {
            _this.currencyList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.currencyList.push({
                        CurrencyId: element.CurrencyId,
                        CurrencyCode: element.CurrencyCode,
                        CurrencyName: element.CurrencyName
                    });
                });
            }
        }, function (error) { });
    };
    //#region "getBalanceSheetAsset"
    BalanceSheetViewComponent.prototype.getBalanceSheetAssetAccountBalances = function () {
        var _this = this;
        var dataModel = {
            id: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_4__["AccountHeadTypes_Enum"].Assets,
            asOfDate: new Date(new Date(this.asOfDate).getFullYear(), new Date(this.asOfDate).getMonth(), new Date(this.asOfDate).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds(), new Date().getMilliseconds()),
            currency: this.currency
        };
        this.globalService
            .getListByIdAndDate(this.appUrl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_7__["GLOBAL"].API_FinancialReport_GetAllAccountBalancesByCategory, dataModel)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.NoteAccountBalances != null) {
                    if (data.data.NoteAccountBalances.length > 0) {
                        var assetsList_1 = [];
                        data.data.NoteAccountBalances.forEach(function (element) {
                            assetsList_1.push({
                                NoteId: element.NoteId,
                                NoteName: element.NoteName,
                                NoteHeadId: element.NoteHeadId,
                                AccountBalances: element.AccountBalances,
                                NoteBalance: _this.getSum(element.AccountBalances, 'Balance')
                            });
                        });
                        _this.assetsList = assetsList_1.filter(function (x) { return x.NoteHeadId === _this.ASSETS_ID; });
                    }
                }
                _this.totalAssets = _this.getSum(_this.assetsList, 'NoteBalance');
                _this.totalNett =
                    _this.totalAssets - _this.totalLiabities - _this.totalEquity;
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error(data.Message);
            }
        });
    };
    //#endregion
    //#region "getBalanceSheetLiability"
    BalanceSheetViewComponent.prototype.getBalanceSheetLiabilityAccountBalances = function () {
        var _this = this;
        var dataModel = {
            id: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_4__["AccountHeadTypes_Enum"].Liabilities,
            asOfDate: this.asOfDate,
            currency: this.currency
        };
        this.globalService
            .getListByIdAndDate(this.appUrl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_7__["GLOBAL"].API_FinancialReport_GetAllAccountBalancesByCategory, dataModel)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.NoteAccountBalances != null) {
                    if (data.data.NoteAccountBalances.length > 0) {
                        var liabilitiesList_1 = [];
                        data.data.NoteAccountBalances.forEach(function (element) {
                            liabilitiesList_1.push({
                                NoteId: element.NoteId,
                                NoteName: element.NoteName,
                                NoteHeadId: element.NoteHeadId,
                                AccountBalances: element.AccountBalances,
                                NoteBalance: _this.getSum(element.AccountBalances, 'Balance')
                            });
                        });
                        _this.liabilitiesList = liabilitiesList_1.filter(function (x) { return x.NoteHeadId === _this.LIABILITY_ID; });
                    }
                }
                _this.totalLiabities = _this.getSum(_this.liabilitiesList, 'NoteBalance');
                _this.totalNett = _this.totalAssets - _this.totalLiabities;
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error(data.Message);
            }
        });
    };
    //#endregion
    //#region "onExportPdf"
    BalanceSheetViewComponent.prototype.onExportPdf = function () {
        var doc = new jspdf__WEBPACK_IMPORTED_MODULE_8__();
        var pageHeight = doc.internal.pageSize.height;
        var pageWidth = doc.internal.pageSize.width;
        doc.setFontSize(10);
        doc.setFontSize(14);
        doc.text('COORDINATION OF HUMANITARIAN ASSISTANCE', src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].pdfTextCenter(doc, 'COORDINATION OF HUMANITARIAN ASSISTANCE', 14), 19);
        var text2 = ' BALANCE SHEET IN (' +
            this.currencyCodePipe.transform(this.currency, this.currencyList) +
            ') AS ' +
            this.asOfDate;
        doc.text(text2, src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].pdfTextCenter(doc, text2, 14), 26);
        // const text2 =
        //   'NOTES TO THE ACCOUNTS IN (' +
        //   this.currencyCodePipe.transform(this.currency, this.currencyList) +
        //   ') FOR THE YEAR ' +
        //   this.asOfDate;
        // doc.text(text2, StaticUtilities.pdfTextCenter(doc, text2, 14), 26);
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, 35, 200, 35);
        doc.setFontSize(12);
        doc.text('ASSETS PROPERTY & CAPITAL', 10, 45);
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, 47, 200, 47);
        var currentFontHeightLocation = 47;
        // ASSETS PROPERTY & CAPITAL List
        doc.setFontSize(10);
        for (var _i = 0, _a = this.assetsList; _i < _a.length; _i++) {
            var item = _a[_i];
            currentFontHeightLocation += 5;
            if (currentFontHeightLocation >= pageHeight) {
                doc.addPage();
                currentFontHeightLocation = 10;
            }
            doc.text(item.NoteName, 10, currentFontHeightLocation);
            var txtWidth = (doc.getStringUnitWidth(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            })) *
                10) /
                doc.internal.scaleFactor;
            doc.text(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            }), pageWidth - txtWidth - 10, currentFontHeightLocation);
        }
        currentFontHeightLocation += 5;
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
        currentFontHeightLocation += 5;
        doc.text('Total Assets ', 10, currentFontHeightLocation);
        var ASSETSText = this.totalAssets.toLocaleString(undefined, { minimumFractionDigits: 2 });
        doc.text(ASSETSText, src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].pdfTextRight(doc, ASSETSText, 10), currentFontHeightLocation);
        doc.setFontSize(12);
        currentFontHeightLocation += 10;
        doc.text('LIABILITIES', 10, currentFontHeightLocation);
        currentFontHeightLocation += 2;
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
        // LIABILITIES List
        doc.setFontSize(10);
        for (var _b = 0, _c = this.liabilitiesList; _b < _c.length; _b++) {
            var item = _c[_b];
            currentFontHeightLocation += 5;
            if (currentFontHeightLocation >= pageHeight) {
                doc.addPage();
                currentFontHeightLocation = 10;
            }
            doc.text(item.NoteName, 10, currentFontHeightLocation);
            var txtWidth = (doc.getStringUnitWidth(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            })) *
                10) /
                doc.internal.scaleFactor;
            doc.text(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            }), pageWidth - txtWidth - 10, currentFontHeightLocation);
        }
        if (currentFontHeightLocation >= pageHeight) {
            doc.addPage();
            currentFontHeightLocation = 10;
        }
        currentFontHeightLocation += 5;
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
        currentFontHeightLocation += 5;
        doc.text('Total Liabilities ', 10, currentFontHeightLocation);
        var LiabilitiesText = this.totalLiabities.toLocaleString(undefined, { minimumFractionDigits: 2 });
        doc.text(LiabilitiesText, src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].pdfTextRight(doc, LiabilitiesText, 10), currentFontHeightLocation);
        currentFontHeightLocation += 10;
        doc.text('DIFFERENCE (CARRIED FORWARD TO CHA OWN SOURCE FUND)', 10, currentFontHeightLocation);
        var NETTText = this.totalNett.toLocaleString(undefined, { minimumFractionDigits: 2 });
        doc.text(NETTText, src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].pdfTextRight(doc, NETTText, 10), currentFontHeightLocation);
        currentFontHeightLocation += 10;
        // doc.text(
        //   'LIABILITIES: ' +
        //     this.totalLiabities.toLocaleString(undefined, {
        //       minimumFractionDigits: 2
        //     }),
        //   10,
        //   currentFontHeightLocation
        // );
        // const NETTText =
        //   'NETT: ' +
        //   this.totalNett.toLocaleString(undefined, { minimumFractionDigits: 2 });
        // doc.text(
        //   NETTText,
        //   StaticUtilities.pdfTextCenter(doc, NETTText, 10),
        //   currentFontHeightLocation
        // );
        // const ASSETSText =
        //   'ASSETS: ' +
        //   this.totalNett.toLocaleString(undefined, { minimumFractionDigits: 2 });
        // doc.text(
        //   ASSETSText,
        //   StaticUtilities.pdfTextRight(doc, ASSETSText, 10),
        //   currentFontHeightLocation
        // );
        doc.save('Balance-Sheet-Report.pdf');
    };
    //#endregion
    //#region "onNoteExportPdf"
    BalanceSheetViewComponent.prototype.onNoteExportPdf = function () {
        var doc = new jspdf__WEBPACK_IMPORTED_MODULE_8__();
        var pageHeight = doc.internal.pageSize.height;
        var pageWidth = doc.internal.pageSize.width;
        doc.setFontSize(10);
        doc.setFontSize(14);
        doc.text('COORDINATION OF HUMANITARIAN ASSISTANCE', src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].pdfTextCenter(doc, 'COORDINATION OF HUMANITARIAN ASSISTANCE', 14), 19);
        var text2 = 'NOTES TO THE ACCOUNTS IN (' +
            this.currencyCodePipe.transform(this.currency, this.currencyList) +
            ') FOR THE YEAR ' +
            this.asOfDate;
        doc.text(text2, src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].pdfTextCenter(doc, text2, 14), 26);
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, 35, 200, 35);
        doc.setFontSize(12);
        doc.text('ASSETS PROPERTY & CAPITAL', 10, 45);
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, 47, 200, 47);
        var currentFontHeightLocation = 47;
        // ASSETS PROPERTY & CAPITAL List
        doc.setFontSize(10);
        for (var _i = 0, _a = this.assetsList; _i < _a.length; _i++) {
            var item = _a[_i];
            currentFontHeightLocation += 5;
            if (currentFontHeightLocation >= pageHeight) {
                doc.addPage();
                currentFontHeightLocation = 10;
            }
            doc.text(item.NoteName, 10, currentFontHeightLocation);
            var txtWidth = (doc.getStringUnitWidth(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            })) *
                10) /
                doc.internal.scaleFactor;
            doc.text(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            }), pageWidth - txtWidth - 10, currentFontHeightLocation);
        }
        if (currentFontHeightLocation >= pageHeight) {
            doc.addPage();
            currentFontHeightLocation = 10;
        }
        doc.setFontSize(12);
        currentFontHeightLocation += 10;
        doc.text('LIABILITIES', 10, currentFontHeightLocation);
        currentFontHeightLocation += 2;
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
        // LIABILITIES List
        doc.setFontSize(10);
        for (var _b = 0, _c = this.liabilitiesList; _b < _c.length; _b++) {
            var item = _c[_b];
            currentFontHeightLocation += 5;
            if (currentFontHeightLocation >= pageHeight) {
                doc.addPage();
                currentFontHeightLocation = 10;
            }
            doc.text(item.NoteName, 10, currentFontHeightLocation);
            var txtWidth = (doc.getStringUnitWidth(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            })) *
                10) /
                doc.internal.scaleFactor;
            doc.text(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            }), pageWidth - txtWidth - 10, currentFontHeightLocation);
        }
        if (currentFontHeightLocation >= pageHeight) {
            doc.addPage();
            currentFontHeightLocation = 10;
        }
        doc.save('Note-Report.pdf');
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], BalanceSheetViewComponent.prototype, "getScreenSize", null);
    BalanceSheetViewComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-balance-sheet-view',
            template: __webpack_require__(/*! ./balance-sheet-view.component.html */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.html"),
            styles: [__webpack_require__(/*! ./balance-sheet-view.component.scss */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_5__["GlobalService"],
            _balance_sheet_service__WEBPACK_IMPORTED_MODULE_3__["BalanceSheetService"],
            src_app_shared_pipes_currency_code_pipe__WEBPACK_IMPORTED_MODULE_9__["CurrencyCodePipe"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_6__["AppUrlService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"]])
    ], BalanceSheetViewComponent);
    return BalanceSheetViewComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.html":
/*!**************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.html ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"body-content\">\r\n  <div class=\"balance-sheet-main\">\r\n    <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-10 col-sm-offset-1\">\r\n          <mat-card [ngStyle]=\"scrollStyles\">\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-6\">\r\n                <h4 class=\"main_heading bdr_heading\">\r\n                  Assets\r\n                  <span class=\"font_smallcaps_12\">Property & Capital</span>\r\n                </h4>\r\n              </div>\r\n              <div class=\"col-sm-6\">\r\n                <div class=\"pull-right\">\r\n                  <button\r\n                    mat-icon-button\r\n                    pTooltip=\"Filter\"\r\n                    tooltipPosition=\"top\"\r\n                    (click)=\"show = !show\"\r\n                  >\r\n                    <i class=\"fa black_icon fa-sliders-h font_x_large\"> </i>\r\n                  </button>\r\n\r\n                  <button\r\n                    mat-icon-button\r\n                    color=\"accent\"\r\n                    pTooltip=\"View Report\"\r\n                    tooltipPosition=\"top\"\r\n                    class=\"pull-right\"\r\n                    [routerLink]=\"[\r\n                      'view',\r\n                      selectedDate | date: 'yyyy-MM-dd',\r\n                      selectedCurrency\r\n                    ]\"\r\n                  >\r\n                    <i class=\"fa fa-clipboard font_x_large\"></i>\r\n                  </button>\r\n                </div>\r\n              </div>\r\n            </div>\r\n\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-12\">\r\n                <mat-spinner\r\n                  *ngIf=\"assetsListLoaderFlag\"\r\n                  [diameter]=\"20\"\r\n                ></mat-spinner>\r\n                <tr *ngFor=\"let item of assetsList\">\r\n                  <td>\r\n                    <lib-inline-edit\r\n                      [value]=\"item\"\r\n                      (action)=\"onValueChangeEmit($event)\"\r\n                      (addAction)=\"addActionEmit($event)\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    ></lib-inline-edit>\r\n                  </td>\r\n                </tr>\r\n                <div *ngIf=\"inputFieldAssetsFlag\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <input\r\n                      #assets\r\n                      matInput\r\n                      type=\"text\"\r\n                      maxlength=\"150\"\r\n                      name=\"assets\"\r\n                      (keyup.enter)=\"onAdd(ASSETS_ID, assets.value)\"\r\n                      (change)=\"onAdd(ASSETS_ID, assets.value)\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    />\r\n                  </mat-form-field>\r\n                </div>\r\n                <i class=\"material-icons\"> </i>\r\n\r\n                <button\r\n                  mat-icon-button\r\n                  (click)=\"toggleInputFieldAssets()\"\r\n                  *ngIf=\"isEditingAllowed\"\r\n                >\r\n                  <mat-icon aria-label=\"Example icon-button with a heart icon\">\r\n                    add_circle_outline\r\n                  </mat-icon>\r\n                </button>\r\n              </div>\r\n            </div>\r\n\r\n            <div class=\"row margin_top_50\">\r\n              <div class=\"col-sm-6\">\r\n                <h4 class=\"main_heading bdr_heading\">\r\n                  Liabilities\r\n                </h4>\r\n              </div>\r\n            </div>\r\n\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-12\">\r\n                <mat-spinner\r\n                  *ngIf=\"liabilitiesListLoaderFlag\"\r\n                  [diameter]=\"20\"\r\n                ></mat-spinner>\r\n                <tr *ngFor=\"let item of liabilitiesList\">\r\n                  <td>\r\n                    <lib-inline-edit\r\n                      [value]=\"item\"\r\n                      (action)=\"onValueChangeEmit($event)\"\r\n                      (addAction)=\"addActionEmit($event)\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    ></lib-inline-edit>\r\n                  </td>\r\n                </tr>\r\n                <div *ngIf=\"inputFieldLiabilitiesFlag\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <input\r\n                      #liabilities\r\n                      matInput\r\n                      type=\"text\"\r\n                      maxlength=\"150\"\r\n                      name=\"liabilities\"\r\n                      (keyup.enter)=\"onAdd(LIABILITY_ID, liabilities.value)\"\r\n                      (change)=\"onAdd(LIABILITY_ID, liabilities.value)\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    />\r\n                  </mat-form-field>\r\n                </div>\r\n\r\n                <button\r\n                  mat-icon-button\r\n                  (click)=\"toggleInputFieldLiabilities()\"\r\n                  *ngIf=\"isEditingAllowed\"\r\n                >\r\n                  <mat-icon aria-label=\"Example icon-button with a heart icon\">\r\n                    add_circle_outline</mat-icon\r\n                  >\r\n                </button>\r\n              </div>\r\n            </div>\r\n\r\n            <!-- filter -->\r\n            <div *ngIf=\"show\" class=\"filtersPopup\">\r\n              <mat-form-field>\r\n                <input\r\n                  matInput\r\n                  [matDatepicker]=\"picker\"\r\n                  placeholder=\"As of date\"\r\n                  [(ngModel)]=\"selectedDate\"\r\n                />\r\n                <mat-datepicker-toggle\r\n                  matSuffix\r\n                  [for]=\"picker\"\r\n                ></mat-datepicker-toggle>\r\n                <mat-datepicker #picker></mat-datepicker>\r\n              </mat-form-field>\r\n              <mat-form-field class=\"settings-full-width\">\r\n                <mat-select\r\n                  placeholder=\"Currency\"\r\n                  [(ngModel)]=\"selectedCurrency\"\r\n                  class=\"settings-full-width\"\r\n                >\r\n                  <mat-option\r\n                    *ngFor=\"let currency of currencyList\"\r\n                    [value]=\"currency.CurrencyId\"\r\n                  >\r\n                    {{ currency.CurrencyCode }}\r\n                  </mat-option>\r\n                </mat-select>\r\n              </mat-form-field>\r\n              <button class=\"pull-right\" (click)=\"show = !show\">OK</button>\r\n            </div>\r\n            <!-- filter -->\r\n\r\n            <!-- <div class=\"col-sm-12 margin_top_50\">\r\n          Donor's Equity\r\n\r\n          <mat-spinner\r\n          *ngIf=\"donorsEquityListLoaderFlag\"\r\n          [diameter]=\"20\"\r\n          ></mat-spinner>\r\n          <tr *ngFor=\"let item of donorsEquityList\">\r\n            <td>\r\n              <lib-inline-edit\r\n              [value]=\"item\"\r\n              (action)=\"onValueChangeEmit($event)\"\r\n              (addAction)=\"addActionEmit($event)\"\r\n              ></lib-inline-edit>\r\n            </td>\r\n          </tr>\r\n          <div *ngIf=\"inputFieldLiabilitiesFlag\">\r\n            <mat-form-field class=\"example-full-width\">\r\n              <input\r\n              #donorsEquity\r\n              matInput\r\n              type=\"text\"\r\n              maxlength=\"150\"\r\n              name=\"donorsEquity\"\r\n              (keyup.enter)=\"\r\n              onAdd(DONORS_EQUITY_ID, donorsEquity.value)\r\n              \"\r\n              (change)=\"onAdd(DONORS_EQUITY_ID, donorsEquity.value)\"\r\n              [disabled]=\"!isEditingAllowed\"/>\r\n            </mat-form-field>\r\n          </div>\r\n\r\n          <button\r\n          mat-icon-button\r\n          (click)=\"toggleInputFieldDonorsEquity()\"\r\n          *ngIf=\"isEditingAllowed\">\r\n          <mat-icon aria-label=\"Example icon-button with a heart icon\">\r\n            add_circle_outline</mat-icon\r\n            >\r\n          </button>\r\n        </div>\r\n        </div>-->\r\n          </mat-card>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<router-outlet></router-outlet>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.scss":
/*!**************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.scss ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".font_smallcaps_12 {\n  text-transform: uppercase;\n  font-variant-caps: all-small-caps; }\n\n.filtersPopup {\n  position: absolute;\n  top: 35px;\n  right: 60px;\n  background-color: white;\n  padding: 12px;\n  max-width: 235px;\n  border: 1px solid silver;\n  z-index: 100; }\n\n.settings-full-width {\n  width: 100%; }\n\n.settings-full-width:nth-last-child() {\n  margin-bottom: 10px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvZmluYW5jaWFsLXJlcG9ydC9iYWxhbmNlLXNoZWV0L2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXGRhc2hib2FyZFxcYWNjb3VudGluZ1xcZmluYW5jaWFsLXJlcG9ydFxcYmFsYW5jZS1zaGVldFxcYmFsYW5jZS1zaGVldC5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNJLHlCQUF5QjtFQUN6QixpQ0FBaUMsRUFBQTs7QUFHckM7RUFDSSxrQkFBaUI7RUFBRSxTQUFRO0VBQUUsV0FBVztFQUN4Qyx1QkFBdUI7RUFDdkIsYUFBYTtFQUNiLGdCQUFnQjtFQUNoQix3QkFBd0I7RUFDeEIsWUFBWSxFQUFBOztBQU9kO0VBQ0UsV0FBVyxFQUFBOztBQUdiO0VBQ0UsbUJBQW1CLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9kYXNoYm9hcmQvYWNjb3VudGluZy9maW5hbmNpYWwtcmVwb3J0L2JhbGFuY2Utc2hlZXQvYmFsYW5jZS1zaGVldC5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5mb250X3NtYWxsY2Fwc18xMiB7XHJcbiAgICB0ZXh0LXRyYW5zZm9ybTogdXBwZXJjYXNlO1xyXG4gICAgZm9udC12YXJpYW50LWNhcHM6IGFsbC1zbWFsbC1jYXBzO1xyXG59XHJcblxyXG4uZmlsdGVyc1BvcHVwIHtcclxuICAgIHBvc2l0aW9uOmFic29sdXRlOyB0b3A6MzVweDsgcmlnaHQ6IDYwcHg7XHJcbiAgICBiYWNrZ3JvdW5kLWNvbG9yOiB3aGl0ZTtcclxuICAgIHBhZGRpbmc6IDEycHg7XHJcbiAgICBtYXgtd2lkdGg6IDIzNXB4O1xyXG4gICAgYm9yZGVyOiAxcHggc29saWQgc2lsdmVyO1xyXG4gICAgei1pbmRleDogMTAwO1xyXG59XHJcblxyXG5cclxuXHJcblxyXG4gIFxyXG4gIC5zZXR0aW5ncy1mdWxsLXdpZHRoIHtcclxuICAgIHdpZHRoOiAxMDAlO1xyXG4gIH1cclxuICBcclxuICAuc2V0dGluZ3MtZnVsbC13aWR0aDpudGgtbGFzdC1jaGlsZCgpIHtcclxuICAgIG1hcmdpbi1ib3R0b206IDEwcHg7XHJcbiAgfSJdfQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.ts":
/*!************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.ts ***!
  \************************************************************************************************/
/*! exports provided: BalanceSheetComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BalanceSheetComponent", function() { return BalanceSheetComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/localstorage.service */ "./src/app/shared/services/localstorage.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var BalanceSheetComponent = /** @class */ (function () {
    //#endregion
    function BalanceSheetComponent(globalService, appUrl, toastr, localStorageService) {
        this.globalService = globalService;
        this.appUrl = appUrl;
        this.toastr = toastr;
        this.localStorageService = localStorageService;
        //#region "variables"
        this.show = false;
        // CONST
        this.ASSETS_ID = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_5__["AccountHeadTypes_Enum"].Assets;
        this.LIABILITY_ID = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_5__["AccountHeadTypes_Enum"].Liabilities;
        this.NUMBER = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_5__["AccountHeadTypes_Enum"].OwnersEquity;
        // DATASOURCE
        this.assetsList = [];
        this.liabilitiesList = [];
        this.donorsEquityList = [];
        this.currencyList = [];
        // FLAG
        this.inputFieldAssetsFlag = false;
        this.inputFieldLiabilitiesFlag = false;
        this.inputFieldDonorsEquityFlag = false;
        this.assetsListLoaderFlag = false;
        this.liabilitiesListLoaderFlag = false;
        this.donorsEquityListLoaderFlag = false;
        this.isEditingAllowed = false;
        this.pageId = src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__["ApplicationPages"].BalanceSheet;
        this.getScreenSize();
    }
    BalanceSheetComponent.prototype.ngOnInit = function () {
        this.getCurrencies();
        this.getBalanceSheetAccountTypes();
        this.selectedDate = new Date();
        this.selectedCurrency = 1;
    };
    //#region "Dynamic Scroll"
    BalanceSheetComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    BalanceSheetComponent.prototype.getCurrencies = function () {
        var _this = this;
        this.globalService
            .getList(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_code_GetAllCurrency)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.CurrencyList != null) {
                    if (data.data.CurrencyList.length > 0) {
                        data.data.CurrencyList.forEach(function (element) {
                            _this.currencyList.push({
                                CurrencyId: element.CurrencyId,
                                CurrencyCode: element.CurrencyCode
                            });
                        });
                    }
                }
            }
            else if (data.StatusCode === 400) {
            }
        });
        this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
    };
    //#region "get"
    BalanceSheetComponent.prototype.getBalanceSheetAccountTypes = function () {
        var _this = this;
        this.assetsListLoaderFlag = true;
        this.liabilitiesListLoaderFlag = true;
        this.donorsEquityListLoaderFlag = true;
        this.globalService
            .getListById(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_GetAllAccountTypeByCategory, src_app_shared_enum__WEBPACK_IMPORTED_MODULE_5__["AccountCategory_Enum"].BalanceSheet)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.AccountTypeList != null) {
                    if (data.data.AccountTypeList.length > 0) {
                        var dataList_1 = [];
                        data.data.AccountTypeList.forEach(function (element) {
                            dataList_1.push({
                                Id: element.AccountTypeId,
                                Name: element.AccountTypeName,
                                AccountCategory: element.AccountCategory,
                                AccountHeadTypeId: element.AccountHeadTypeId
                            });
                        });
                        _this.assetsList = dataList_1.filter(function (x) { return x.AccountHeadTypeId === _this.ASSETS_ID; });
                        _this.liabilitiesList = dataList_1.filter(function (x) { return x.AccountHeadTypeId === _this.LIABILITY_ID; });
                        dataList_1 = []; // empty
                    }
                }
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error('Something went wrong ! Try Again');
            }
            _this.assetsListLoaderFlag = false;
            _this.liabilitiesListLoaderFlag = false;
            _this.donorsEquityListLoaderFlag = false;
        }, function (error) {
            _this.assetsListLoaderFlag = false;
            _this.liabilitiesListLoaderFlag = false;
            _this.donorsEquityListLoaderFlag = false;
        });
    };
    //#endregion
    //#region "add"
    BalanceSheetComponent.prototype.addBalanceSheetAccountTypes = function (model) {
        var _this = this;
        var obj = {};
        var index = model._index;
        var accountHeadTypeId = model.AccountHeadTypeId;
        // error handling
        if (accountHeadTypeId === this.ASSETS_ID) {
            this.assetsList[index]._IsLoading = true;
            this.assetsList[index]._IsError = false;
        }
        else if (accountHeadTypeId === this.LIABILITY_ID) {
            this.liabilitiesList[index]._IsLoading = true;
            this.liabilitiesList[index]._IsError = false;
        }
        else if (accountHeadTypeId === this.DONORS_EQUITY_ID) {
            this.donorsEquityList[index]._IsLoading = true;
            this.donorsEquityList[index]._IsError = false;
        }
        obj = {
            AccountTypeId: model.Id,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountCategory: model.AccountCategory,
            AccountTypeName: model.Name
        };
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_AddAccountType, obj)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (accountHeadTypeId === _this.ASSETS_ID) {
                    _this.assetsList[index]._IsLoading = false;
                    _this.assetsList[index].Id = data.CommonId.Id;
                }
                else if (accountHeadTypeId === _this.LIABILITY_ID) {
                    _this.liabilitiesList[index]._IsLoading = false;
                    _this.liabilitiesList[index].Id = data.CommonId.Id;
                }
                else if (accountHeadTypeId === _this.DONORS_EQUITY_ID) {
                    _this.donorsEquityList[index]._IsLoading = false;
                    _this.donorsEquityList[index].Id = data.CommonId.Id;
                }
            }
            else if (data.StatusCode === 400) {
                if (accountHeadTypeId === _this.ASSETS_ID) {
                    _this.assetsList[index]._IsError = true;
                    _this.assetsList[index]._IsLoading = false;
                }
                else if (accountHeadTypeId === _this.LIABILITY_ID) {
                    _this.liabilitiesList[index]._IsError = true;
                    _this.liabilitiesList[index]._IsLoading = false;
                }
                else if (accountHeadTypeId === _this.DONORS_EQUITY_ID) {
                    _this.donorsEquityList[index]._IsError = true;
                    _this.donorsEquityList[index]._IsLoading = false;
                }
                _this.toastr.error('\'' + obj.AccountTypeName + '\'' + data.Message);
            }
        }, function (error) {
            // error handling
            if (accountHeadTypeId === _this.ASSETS_ID) {
                _this.assetsList[index]._IsError = true;
                _this.assetsList[index]._IsLoading = false;
            }
            else if (accountHeadTypeId === _this.LIABILITY_ID) {
                _this.liabilitiesList[index]._IsError = true;
                _this.liabilitiesList[index]._IsLoading = false;
            }
            else if (accountHeadTypeId === _this.DONORS_EQUITY_ID) {
                _this.donorsEquityList[index]._IsError = true;
                _this.donorsEquityList[index]._IsLoading = false;
            }
            _this.toastr.error('Something went wrong ! Try Again');
        });
    };
    //#endregion
    //#region "edit"
    BalanceSheetComponent.prototype.editBalanceSheetAccountTypes = function (model) {
        var _this = this;
        var obj = {
            AccountTypeId: model.Id,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountCategory: model.AccountCategory,
            AccountTypeName: model.Name,
            _IsError: false
        };
        //#region "error handling"
        if (model.AccountHeadTypeId === this.ASSETS_ID) {
            var item = this.assetsList.find(function (x) { return x.Id === model.Id; });
            var index = this.assetsList.indexOf(item);
            this.assetsList[index]._IsError = false;
            this.assetsList[index]._IsLoading = true;
        }
        else if (model.AccountHeadTypeId === this.LIABILITY_ID) {
            var item = this.liabilitiesList.find(function (x) { return x.Id === model.Id; });
            var index = this.liabilitiesList.indexOf(item);
            this.liabilitiesList[index]._IsError = false;
            this.liabilitiesList[index]._IsLoading = true;
        }
        //#endregion
        this.globalService
            .post(this.appUrl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_EditAccountType, obj)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                //#region "error handling"
                if (model.AccountHeadTypeId === _this.ASSETS_ID) {
                    var item = _this.assetsList.find(function (x) { return x.Id === model.Id; });
                    var index = _this.assetsList.indexOf(item);
                    _this.assetsList[index]._IsError = false;
                    _this.assetsList[index]._IsLoading = false;
                }
                else if (model.AccountHeadTypeId === _this.LIABILITY_ID) {
                    var item = _this.liabilitiesList.find(function (x) { return x.Id === model.Id; });
                    var index = _this.liabilitiesList.indexOf(item);
                    _this.liabilitiesList[index]._IsError = false;
                    _this.liabilitiesList[index]._IsLoading = false;
                }
                //#endregion
            }
            else if (data.StatusCode === 400) {
                //#region "error handling"
                if (model.AccountHeadTypeId === _this.ASSETS_ID) {
                    var item = _this.assetsList.find(function (x) { return x.Id === model.Id; });
                    var index = _this.assetsList.indexOf(item);
                    _this.assetsList[index]._IsError = true;
                    _this.assetsList[index]._IsLoading = false;
                }
                else if (model.AccountHeadTypeId === _this.LIABILITY_ID) {
                    var item = _this.liabilitiesList.find(function (x) { return x.Id === model.Id; });
                    var index = _this.liabilitiesList.indexOf(item);
                    _this.liabilitiesList[index]._IsError = true;
                    _this.liabilitiesList[index]._IsLoading = false;
                }
                //#endregion
                _this.toastr.error('\'' + obj.AccountTypeName + '\'' + data.Message);
            }
        }, function (error) {
            //#region "error handling"
            if (model.AccountHeadTypeId === _this.ASSETS_ID) {
                var item = _this.assetsList.find(function (x) { return x.Id === model.Id; });
                var index = _this.assetsList.indexOf(item);
                _this.assetsList[index]._IsError = true;
                _this.assetsList[index]._IsLoading = false;
            }
            else if (model.AccountHeadTypeId === _this.LIABILITY_ID) {
                var item = _this.liabilitiesList.find(function (x) { return x.Id === model.Id; });
                var index = _this.liabilitiesList.indexOf(item);
                _this.liabilitiesList[index]._IsError = true;
                _this.liabilitiesList[index]._IsLoading = false;
            }
            //#endregion
            _this.toastr.error('Something went wrong ! Try Again');
        });
    };
    //#endregion
    //#region "onAdd"
    BalanceSheetComponent.prototype.onAdd = function (type, value) {
        var obj = {};
        if (type === this.ASSETS_ID) {
            this.toggleInputFieldAssets();
            obj = {
                Id: 0,
                AccountHeadTypeId: this.ASSETS_ID,
                AccountCategory: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_5__["AccountCategory_Enum"].BalanceSheet,
                Name: value,
                _index: this.assetsList.length
            };
            this.assetsList.push(obj);
        }
        else if (type === this.LIABILITY_ID) {
            this.toggleInputFieldLiabilities();
            obj = {
                Id: 0,
                AccountHeadTypeId: this.LIABILITY_ID,
                AccountCategory: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_5__["AccountCategory_Enum"].BalanceSheet,
                Name: value,
                _index: this.liabilitiesList.length
            };
            this.liabilitiesList.push(obj);
        }
        this.addBalanceSheetAccountTypes(obj);
    };
    //#endregion
    //#region "Emit"
    BalanceSheetComponent.prototype.onValueChangeEmit = function (data) {
        this.editBalanceSheetAccountTypes(data);
    };
    BalanceSheetComponent.prototype.addActionEmit = function (data) {
        this.addBalanceSheetAccountTypes(data);
    };
    //#endregion
    //#region "show / hide"
    BalanceSheetComponent.prototype.toggleInputFieldAssets = function () {
        this.inputFieldAssetsFlag = !this.inputFieldAssetsFlag;
    };
    BalanceSheetComponent.prototype.toggleInputFieldLiabilities = function () {
        this.inputFieldLiabilitiesFlag = !this.inputFieldLiabilitiesFlag;
    };
    BalanceSheetComponent.prototype.toggleInputFieldDonorsEquity = function () {
        this.inputFieldDonorsEquityFlag = !this.inputFieldDonorsEquityFlag;
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], BalanceSheetComponent.prototype, "getScreenSize", null);
    BalanceSheetComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-balance-sheet',
            template: __webpack_require__(/*! ./balance-sheet.component.html */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.html"),
            styles: [__webpack_require__(/*! ./balance-sheet.component.scss */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_6__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_7__["AppUrlService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_1__["ToastrService"],
            src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_3__["LocalStorageService"]])
    ], BalanceSheetComponent);
    return BalanceSheetComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.service.ts":
/*!**********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.service.ts ***!
  \**********************************************************************************************/
/*! exports provided: BalanceSheetService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BalanceSheetService", function() { return BalanceSheetService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/operators/map */ "./node_modules/rxjs/internal/operators/map.js");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var BalanceSheetService = /** @class */ (function () {
    function BalanceSheetService(globalService, appurl) {
        this.globalService = globalService;
        this.appurl = appurl;
    }
    //#region "GetCurrencyList"
    BalanceSheetService.prototype.GetCurrencyList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllCurrency)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.CurrencyList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    BalanceSheetService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"]])
    ], BalanceSheetService);
    return BalanceSheetService;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/financial-report-routing.module.ts":
/*!******************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/financial-report-routing.module.ts ***!
  \******************************************************************************************/
/*! exports provided: FinancialReportRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FinancialReportRoutingModule", function() { return FinancialReportRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _financial_report_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./financial-report.component */ "./src/app/dashboard/accounting/financial-report/financial-report.component.ts");
/* harmony import */ var src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/role-guard */ "./src/app/shared/services/role-guard.ts");
/* harmony import */ var _balance_sheet_balance_sheet_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./balance-sheet/balance-sheet.component */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.ts");
/* harmony import */ var _balance_sheet_balance_sheet_view_balance_sheet_view_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./balance-sheet/balance-sheet-view/balance-sheet-view.component */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.ts");
/* harmony import */ var _income_expense_income_expense_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./income-expense/income-expense.component */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.ts");
/* harmony import */ var _income_expense_income_expense_view_income_expense_view_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./income-expense/income-expense-view/income-expense-view.component */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.ts");
/* harmony import */ var src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};









var ModuleId = src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_8__["ApplicationModule"].AccountingNew;
var routes = [
    {
        path: '',
        component: _financial_report_component__WEBPACK_IMPORTED_MODULE_2__["FinancialReportComponent"],
        children: [
            {
                path: '', redirectTo: 'balance-sheet', pathMatch: 'full'
            },
            {
                path: 'balance-sheet',
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_3__["RoleGuardService"]],
                children: [
                    { path: '', component: _balance_sheet_balance_sheet_component__WEBPACK_IMPORTED_MODULE_4__["BalanceSheetComponent"] },
                    {
                        path: 'view/:asOfDate/:currency',
                        component: _balance_sheet_balance_sheet_view_balance_sheet_view_component__WEBPACK_IMPORTED_MODULE_5__["BalanceSheetViewComponent"]
                    }
                ]
            },
            {
                path: 'income-expense-report',
                children: [
                    { path: '', component: _income_expense_income_expense_component__WEBPACK_IMPORTED_MODULE_6__["IncomeExpenseComponent"] },
                    {
                        path: 'view/:asOfDate/:upToDate/:currency',
                        component: _income_expense_income_expense_view_income_expense_view_component__WEBPACK_IMPORTED_MODULE_7__["IncomeExpenseViewComponent"]
                    }
                ]
            },
            { path: '', redirectTo: 'balance-sheet' },
            {
                path: 'income-expense-report',
                component: _income_expense_income_expense_component__WEBPACK_IMPORTED_MODULE_6__["IncomeExpenseComponent"],
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_3__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    page: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_8__["accountingNewMaster"].IncomeExpenseReport
                }
            }
        ]
    }
];
var FinancialReportRoutingModule = /** @class */ (function () {
    function FinancialReportRoutingModule() {
    }
    FinancialReportRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], FinancialReportRoutingModule);
    return FinancialReportRoutingModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/financial-report.component.html":
/*!***************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/financial-report.component.html ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"main_body\">\r\n  <router-outlet></router-outlet>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/financial-report.component.scss":
/*!***************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/financial-report.component.scss ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2ZpbmFuY2lhbC1yZXBvcnQvZmluYW5jaWFsLXJlcG9ydC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/financial-report.component.ts":
/*!*************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/financial-report.component.ts ***!
  \*************************************************************************************/
/*! exports provided: FinancialReportComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FinancialReportComponent", function() { return FinancialReportComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _shared_enum__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/localstorage.service */ "./src/app/shared/services/localstorage.service.ts");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var FinancialReportComponent = /** @class */ (function () {
    function FinancialReportComponent(globalService, localStorageservice) {
        this.globalService = globalService;
        this.localStorageservice = localStorageservice;
        this.setSelectedHeader = _shared_enum__WEBPACK_IMPORTED_MODULE_1__["UIModuleHeaders"].FinancialAccountHeader;
        this.setProjectHeader = 'Financial Report';
        this.menuList = [
            {
                Id: 1,
                PageId: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__["accountingNewMaster"].BalanceSheet,
                Text: 'Balance Sheet',
                Link: '/accounting/financial-report/balance-sheet'
            },
            {
                Id: 2,
                PageId: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_2__["accountingNewMaster"].IncomeExpenseReport,
                Text: 'Income Expense Report',
                Link: '/accounting/financial-report/income-expense-report'
            }
        ];
        this.authorizedMenuList = [];
        // Set Menu Header Name
        this.globalService.setMenuHeaderName(this.setProjectHeader);
        this.authorizedMenuList = this.localStorageservice.GetAuthorizedPages(this.menuList);
        // Set Menu Header List
        this.globalService.setMenuList(this.authorizedMenuList);
    }
    FinancialReportComponent.prototype.ngOnInit = function () {
    };
    FinancialReportComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-financial-report',
            template: __webpack_require__(/*! ./financial-report.component.html */ "./src/app/dashboard/accounting/financial-report/financial-report.component.html"),
            styles: [__webpack_require__(/*! ./financial-report.component.scss */ "./src/app/dashboard/accounting/financial-report/financial-report.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_4__["GlobalSharedService"],
            src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_3__["LocalStorageService"]])
    ], FinancialReportComponent);
    return FinancialReportComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/financial-report.module.ts":
/*!**********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/financial-report.module.ts ***!
  \**********************************************************************************/
/*! exports provided: FinancialReportModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FinancialReportModule", function() { return FinancialReportModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _financial_report_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./financial-report-routing.module */ "./src/app/dashboard/accounting/financial-report/financial-report-routing.module.ts");
/* harmony import */ var _balance_sheet_balance_sheet_view_balance_sheet_view_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./balance-sheet/balance-sheet-view/balance-sheet-view.component */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet-view/balance-sheet-view.component.ts");
/* harmony import */ var _balance_sheet_balance_sheet_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./balance-sheet/balance-sheet.service */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.service.ts");
/* harmony import */ var _balance_sheet_balance_sheet_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./balance-sheet/balance-sheet.component */ "./src/app/dashboard/accounting/financial-report/balance-sheet/balance-sheet.component.ts");
/* harmony import */ var _income_expense_income_expense_view_income_expense_view_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./income-expense/income-expense-view/income-expense-view.component */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.ts");
/* harmony import */ var _income_expense_income_expense_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./income-expense/income-expense.service */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.service.ts");
/* harmony import */ var _income_expense_income_expense_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./income-expense/income-expense.component */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.ts");
/* harmony import */ var _angular_material_input__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/material/input */ "./node_modules/@angular/material/esm5/input.es5.js");
/* harmony import */ var _angular_material_button__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/material/button */ "./node_modules/@angular/material/esm5/button.es5.js");
/* harmony import */ var _angular_material_card__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/material/card */ "./node_modules/@angular/material/esm5/card.es5.js");
/* harmony import */ var _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/material/datepicker */ "./node_modules/@angular/material/esm5/datepicker.es5.js");
/* harmony import */ var _angular_material_core__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material/core */ "./node_modules/@angular/material/esm5/core.es5.js");
/* harmony import */ var _angular_material_icon__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/material/icon */ "./node_modules/@angular/material/esm5/icon.es5.js");
/* harmony import */ var _angular_material_select__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @angular/material/select */ "./node_modules/@angular/material/esm5/select.es5.js");
/* harmony import */ var _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! @angular/material/progress-spinner */ "./node_modules/@angular/material/esm5/progress-spinner.es5.js");
/* harmony import */ var ngx_mat_select_search__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ngx-mat-select-search */ "./node_modules/ngx-mat-select-search/fesm5/ngx-mat-select-search.js");
/* harmony import */ var primeng_primeng__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! primeng/primeng */ "./node_modules/primeng/primeng.js");
/* harmony import */ var primeng_primeng__WEBPACK_IMPORTED_MODULE_18___default = /*#__PURE__*/__webpack_require__.n(primeng_primeng__WEBPACK_IMPORTED_MODULE_18__);
/* harmony import */ var _financial_report_component__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ./financial-report.component */ "./src/app/dashboard/accounting/financial-report/financial-report.component.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _shared_pipes_pipe_export_pipe_export_module__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ../../../shared/pipes/pipe-export/pipe-export.module */ "./src/app/shared/pipes/pipe-export/pipe-export.module.ts");
/* harmony import */ var _projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ../../../../../projects/library/src/public_api */ "./projects/library/src/public_api.ts");
/* harmony import */ var _shared_pipes_currency_code_pipe__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! ../../../shared/pipes/currency-code.pipe */ "./src/app/shared/pipes/currency-code.pipe.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
























var FinancialReportModule = /** @class */ (function () {
    function FinancialReportModule() {
    }
    FinancialReportModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _financial_report_component__WEBPACK_IMPORTED_MODULE_19__["FinancialReportComponent"],
                _balance_sheet_balance_sheet_view_balance_sheet_view_component__WEBPACK_IMPORTED_MODULE_3__["BalanceSheetViewComponent"],
                _balance_sheet_balance_sheet_component__WEBPACK_IMPORTED_MODULE_5__["BalanceSheetComponent"],
                _income_expense_income_expense_view_income_expense_view_component__WEBPACK_IMPORTED_MODULE_6__["IncomeExpenseViewComponent"],
                _income_expense_income_expense_component__WEBPACK_IMPORTED_MODULE_8__["IncomeExpenseComponent"]
            ],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _financial_report_routing_module__WEBPACK_IMPORTED_MODULE_2__["FinancialReportRoutingModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_20__["FormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_20__["ReactiveFormsModule"],
                // Custome pipe
                _shared_pipes_pipe_export_pipe_export_module__WEBPACK_IMPORTED_MODULE_21__["PipeExportModule"],
                // Custom Modules
                _projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_22__["LibraryModule"],
                // material
                _angular_material_input__WEBPACK_IMPORTED_MODULE_9__["MatInputModule"],
                _angular_material_button__WEBPACK_IMPORTED_MODULE_10__["MatButtonModule"],
                _angular_material_card__WEBPACK_IMPORTED_MODULE_11__["MatCardModule"],
                _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_12__["MatDatepickerModule"],
                _angular_material_core__WEBPACK_IMPORTED_MODULE_13__["MatNativeDateModule"],
                _angular_material_icon__WEBPACK_IMPORTED_MODULE_14__["MatIconModule"],
                _angular_material_select__WEBPACK_IMPORTED_MODULE_15__["MatSelectModule"],
                ngx_mat_select_search__WEBPACK_IMPORTED_MODULE_17__["NgxMatSelectSearchModule"],
                _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_16__["MatProgressSpinnerModule"],
                primeng_primeng__WEBPACK_IMPORTED_MODULE_18__["TooltipModule"]
            ],
            providers: [
                _shared_pipes_currency_code_pipe__WEBPACK_IMPORTED_MODULE_23__["CurrencyCodePipe"],
                _balance_sheet_balance_sheet_service__WEBPACK_IMPORTED_MODULE_4__["BalanceSheetService"],
                _income_expense_income_expense_service__WEBPACK_IMPORTED_MODULE_7__["IncomeExpenseService"]
            ],
            entryComponents: []
        })
    ], FinancialReportModule);
    return FinancialReportModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.html":
/*!*****************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.html ***!
  \*****************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"body-content\">\r\n  <div class=\"balance-sheet-main\">\r\n    <div class=\"container-fluid\">\r\n      <div class=\"row pull-right\">\r\n        <div class=\"col-sm-1\">\r\n          <mat-spinner\r\n            [diameter]=\"25\"\r\n            *ngIf=\"detailOfNoteFlag; else downloadNotePdf\"\r\n          ></mat-spinner>\r\n\r\n          <ng-template #downloadNotePdf>\r\n            <button\r\n              mat-raised-button\r\n              color=\"primary\"\r\n              (click)=\"onDetailOfNotesPdf()\"\r\n              pTooltip=\"Export Note Pdf\"\r\n              tooltipPosition=\"top\"\r\n            >\r\n              View Note\r\n            </button>\r\n          </ng-template>\r\n        </div>\r\n      </div>\r\n\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-10 col-sm-offset-1\">\r\n          <mat-card [ngStyle]=\"scrollStyles\">\r\n            <div class=\"row border_bottom\">\r\n              <div class=\"col-sm-9\">\r\n                <h1 class=\"main_heading\">\r\n                  Income/Expense Report\r\n                  <span class=\"font_smallcaps_12\"\r\n                    >as of {{ asOfDate }} up to {{ upToDate }}</span\r\n                  >\r\n                </h1>\r\n              </div>\r\n              <div class=\"col-sm-1\"></div>\r\n              <div class=\"col-sm-1\">\r\n                <button\r\n                  mat-icon-button\r\n                  class=\"pull-right\"\r\n                  (click)=\"onExportPdf()\"\r\n                  pTooltip=\"Export Pdf\"\r\n                  tooltipPosition=\"top\"\r\n                >\r\n                  <mat-icon aria-label=\"Export pdf\"\r\n                    >vertical_align_bottom</mat-icon\r\n                  >\r\n                </button>\r\n              </div>\r\n              <div class=\"col-sm-1\">\r\n                <button\r\n                  mat-icon-button\r\n                  class=\"pull-right\"\r\n                  [routerLink]=\"'../../../../'\"\r\n                >\r\n                  <mat-icon>settings</mat-icon>\r\n                </button>\r\n              </div>\r\n            </div>\r\n\r\n            <div class=\"row\" class=\"col-12-sm\">\r\n              <table class=\"collapsed table table-bordered\">\r\n                <tr\r\n                  *ngFor=\"let item of incomeList\"\r\n                  class=\"col-12-sm\"\r\n                  let\r\n                  ia=\"index\"\r\n                >\r\n                  <table class=\"collapsed table table-bordered\">\r\n                    <tr>\r\n                      <td\r\n                        class=\"font_caps_12\"\r\n                        style=\"width:100%\"\r\n                        (click)=\"showa = !showa\"\r\n                      >\r\n                        {{ item.NoteName }}\r\n                        <i class=\"fa fa-ellipsis-h\" aria-hidden=\"true\"></i>\r\n                      </td>\r\n                      <td\r\n                        class=\"font_caps_12 right_align\"\r\n                        (click)=\"showa = !showa\"\r\n                      >\r\n                        {{\r\n                          item.NoteBalance.toLocaleString(undefined, {\r\n                            minimumFractionDigits: 2\r\n                          })\r\n                        }}\r\n                      </td>\r\n                    </tr>\r\n                    <tr *ngFor=\"let balance of item.AccountBalances\">\r\n                      <td\r\n                        class=\"font_caps_10 \"\r\n                        style=\"width:100%\"\r\n                        *ngIf=\"showa\"\r\n                      >\r\n                        {{ balance.AccountName }}\r\n                      </td>\r\n                      <td class=\"font_caps_10 right_align\" *ngIf=\"showa\">\r\n                        {{\r\n                          balance.Balance.toLocaleString(undefined, {\r\n                            minimumFractionDigits: 2\r\n                          })\r\n                        }}\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </tr>\r\n              </table>\r\n            </div>\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-12\">\r\n                <table style=\"    width: 100%;\">\r\n                  <tr>\r\n                    <td class=\"font_caps_12_bold \">\r\n                      TOTAL INCOME\r\n                    </td>\r\n                    <td class=\"font_caps_12_bold right_align\">\r\n                      {{\r\n                        totalIncome.toLocaleString(undefined, {\r\n                          minimumFractionDigits: 2\r\n                        })\r\n                      }}\r\n                    </td>\r\n                  </tr>\r\n                </table>\r\n              </div>\r\n            </div>\r\n\r\n            <div class=\"margin_top_50\">\r\n              <h4 class=\"main_heading bdr_heading\">Expenses</h4>\r\n            </div>\r\n            <div class=\"row\" class=\"col-12-sm\">\r\n              <table class=\"collapsed table table-bordered\">\r\n                <tr\r\n                  *ngFor=\"let item of expenseList\"\r\n                  class=\"col-12-sm\"\r\n                  let\r\n                  ia=\"index\"\r\n                >\r\n                  <table class=\"collapsed table table-bordered\">\r\n                    <tr>\r\n                      <td\r\n                        class=\"font_caps_12\"\r\n                        style=\"width:100%\"\r\n                        (click)=\"showa = !showa\"\r\n                      >\r\n                        {{ item.NoteName }}\r\n                        <i class=\"fa fa-ellipsis-h\" aria-hidden=\"true\"></i>\r\n                      </td>\r\n                      <td\r\n                        class=\"font_caps_12 right_align\"\r\n                        (click)=\"showa = !showa\"\r\n                      >\r\n                        {{\r\n                          item.NoteBalance.toLocaleString(undefined, {\r\n                            minimumFractionDigits: 2\r\n                          })\r\n                        }}\r\n                      </td>\r\n                    </tr>\r\n                    <tr *ngFor=\"let balance of item.AccountBalances\">\r\n                      <td\r\n                        class=\"font_caps_10 \"\r\n                        style=\"width:100%\"\r\n                        *ngIf=\"showa\"\r\n                      >\r\n                        {{ balance.AccountName }}\r\n                      </td>\r\n                      <td class=\"font_caps_10 right_align\" *ngIf=\"showa\">\r\n                        {{\r\n                          balance.Balance.toLocaleString(undefined, {\r\n                            minimumFractionDigits: 2\r\n                          })\r\n                        }}\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </tr>\r\n              </table>\r\n            </div>\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-12\">\r\n                <table style=\"    width: 100%;\">\r\n                  <tr>\r\n                    <td class=\"font_caps_12_bold \">\r\n                      TOTAL EXPENSES\r\n                    </td>\r\n                    <td class=\"font_caps_12_bold right_align\">\r\n                      {{\r\n                        totalExpenses.toLocaleString(undefined, {\r\n                          minimumFractionDigits: 2\r\n                        })\r\n                      }}\r\n                    </td>\r\n                  </tr>\r\n                </table>\r\n              </div>\r\n            </div>\r\n            <div class=\"margin_top_50\">\r\n              <h4 class=\"main_heading bdr_heading\">NETT INCOME - EXPENSES</h4>\r\n            </div>\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-12\">\r\n                <table style=\"width: 100%;\">\r\n                  <tr>\r\n                    <td class=\"font_caps_12_bold \">\r\n                      NETT\r\n                    </td>\r\n                    <td class=\"font_caps_12_bold right_align\">\r\n                      {{\r\n                        totalNett.toLocaleString(undefined, {\r\n                          minimumFractionDigits: 2\r\n                        })\r\n                      }}\r\n                    </td>\r\n                  </tr>\r\n                </table>\r\n              </div>\r\n            </div>\r\n          </mat-card>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.scss":
/*!*****************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.scss ***!
  \*****************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".font_caps_12 {\n  text-transform: uppercase; }\n\n.font_caps_12_bold {\n  text-transform: uppercase;\n  font-weight: 600; }\n\n.font_smallcaps_12 {\n  font-variant-caps: all-small-caps;\n  text-transform: uppercase; }\n\n.font_caps_10 {\n  padding-top: 2px;\n  padding-bottom: px;\n  font-variant-caps: all-petite-caps;\n  text-transform: uppercase; }\n\n.right_align {\n  text-align: right; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvZmluYW5jaWFsLXJlcG9ydC9pbmNvbWUtZXhwZW5zZS9pbmNvbWUtZXhwZW5zZS12aWV3L2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXGRhc2hib2FyZFxcYWNjb3VudGluZ1xcZmluYW5jaWFsLXJlcG9ydFxcaW5jb21lLWV4cGVuc2VcXGluY29tZS1leHBlbnNlLXZpZXdcXGluY29tZS1leHBlbnNlLXZpZXcuY29tcG9uZW50LnNjc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDSSx5QkFBeUIsRUFBQTs7QUFHN0I7RUFDSSx5QkFBeUI7RUFDekIsZ0JBQWdCLEVBQUE7O0FBR3BCO0VBQ0ksaUNBQWlDO0VBQ2pDLHlCQUF5QixFQUFBOztBQUc3QjtFQUNJLGdCQUFlO0VBQ2Ysa0JBQWlCO0VBQ2pCLGtDQUFrQztFQUNsQyx5QkFBeUIsRUFBQTs7QUFHN0I7RUFDSSxpQkFBaUIsRUFBQSIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2ZpbmFuY2lhbC1yZXBvcnQvaW5jb21lLWV4cGVuc2UvaW5jb21lLWV4cGVuc2Utdmlldy9pbmNvbWUtZXhwZW5zZS12aWV3LmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsiLmZvbnRfY2Fwc18xMiB7XHJcbiAgICB0ZXh0LXRyYW5zZm9ybTogdXBwZXJjYXNlO1xyXG59XHJcblxyXG4uZm9udF9jYXBzXzEyX2JvbGQge1xyXG4gICAgdGV4dC10cmFuc2Zvcm06IHVwcGVyY2FzZTtcclxuICAgIGZvbnQtd2VpZ2h0OiA2MDA7XHJcbn1cclxuXHJcbi5mb250X3NtYWxsY2Fwc18xMiB7XHJcbiAgICBmb250LXZhcmlhbnQtY2FwczogYWxsLXNtYWxsLWNhcHM7XHJcbiAgICB0ZXh0LXRyYW5zZm9ybTogdXBwZXJjYXNlO1xyXG59XHJcblxyXG4uZm9udF9jYXBzXzEwIHtcclxuICAgIHBhZGRpbmctdG9wOjJweDtcclxuICAgIHBhZGRpbmctYm90dG9tOnB4O1xyXG4gICAgZm9udC12YXJpYW50LWNhcHM6IGFsbC1wZXRpdGUtY2FwcztcclxuICAgIHRleHQtdHJhbnNmb3JtOiB1cHBlcmNhc2U7XHJcbn1cclxuXHJcbi5yaWdodF9hbGlnbiB7XHJcbiAgICB0ZXh0LWFsaWduOiByaWdodDtcclxufVxyXG5cclxuIl19 */"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.ts":
/*!***************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.ts ***!
  \***************************************************************************************************************************/
/*! exports provided: IncomeExpenseViewComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "IncomeExpenseViewComponent", function() { return IncomeExpenseViewComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var _income_expense_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../income-expense.service */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.service.ts");
/* harmony import */ var src_app_shared_pipes_currency_code_pipe__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/shared/pipes/currency-code.pipe */ "./src/app/shared/pipes/currency-code.pipe.ts");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
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
var IncomeExpenseViewComponent = /** @class */ (function () {
    //#endregion
    function IncomeExpenseViewComponent(globalService, incomeExpenseService, appUrl, currencyCodePipe, route, toastr) {
        this.globalService = globalService;
        this.incomeExpenseService = incomeExpenseService;
        this.appUrl = appUrl;
        this.currencyCodePipe = currencyCodePipe;
        this.route = route;
        this.toastr = toastr;
        // CONST
        this.INCOME_ID = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_3__["AccountHeadTypes_Enum"].Income;
        this.EXPENSE_ID = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_3__["AccountHeadTypes_Enum"].Expense;
        // DATASOURCE
        this.incomeList = [];
        this.expenseList = [];
        this.currencyList = [];
        // Notes
        this.detailsOfNotesFinalList = [];
        this.totalIncome = 0;
        this.totalExpenses = 0;
        this.totalNett = 0;
        this.showa = [];
        this.showb = [];
        this.detailOfNoteFlag = false;
        this.getScreenSize();
    }
    IncomeExpenseViewComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.asOfDate = params.asOfDate;
            _this.upToDate = params.upToDate;
            _this.selectedCurrency = +params.currency;
            _this.getAllCurrency();
            _this.getIncomeAccountBalances();
            _this.getExpenseAccountBalances();
            _this.getDetailOfNotes();
        });
    };
    //#region "Dynamic Scroll"
    IncomeExpenseViewComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 150 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    IncomeExpenseViewComponent.prototype.getSum = function (list, field) {
        var sum = 0;
        if (list != null) {
            for (var i = 0; i < list.length; i++) {
                sum += list[i][field];
            }
        }
        return sum;
    };
    //#region "getAllCurrency"
    IncomeExpenseViewComponent.prototype.getAllCurrency = function () {
        var _this = this;
        this.currencyList = [];
        this.incomeExpenseService.GetCurrencyList().subscribe(function (response) {
            _this.currencyList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.currencyList.push({
                        CurrencyId: element.CurrencyId,
                        CurrencyCode: element.CurrencyCode,
                        CurrencyName: element.CurrencyName
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "get"
    IncomeExpenseViewComponent.prototype.getIncomeAccountBalances = function () {
        var _this = this;
        var dataModel = {
            id: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_3__["AccountHeadTypes_Enum"].Income,
            asOfDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].getLocalDate(this.asOfDate),
            upToDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].getLocalDate(this.upToDate),
            currency: this.selectedCurrency
        };
        this.globalService
            .getListByIdAndDate(this.appUrl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_6__["GLOBAL"].API_FinancialReport_GetAllAccountIncomeExpensesByCategory, dataModel)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.NoteAccountBalances != null) {
                    if (data.data.NoteAccountBalances.length > 0) {
                        var incomeList_1 = [];
                        data.data.NoteAccountBalances.forEach(function (element) {
                            incomeList_1.push({
                                NoteId: element.NoteId,
                                NoteName: element.NoteName,
                                NoteHeadId: element.NoteHeadId,
                                AccountBalances: element.AccountBalances,
                                NoteBalance: _this.getSum(element.AccountBalances, 'Balance')
                            });
                        });
                        _this.incomeList = incomeList_1.filter(function (x) { return x.NoteHeadId === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_3__["AccountHeadTypes_Enum"].Income; });
                    }
                }
                _this.totalIncome = _this.getSum(_this.incomeList, 'NoteBalance');
                _this.totalNett = _this.totalIncome - _this.totalExpenses;
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error(data.Message);
            }
        });
    };
    //#endregion
    //#region "get"
    IncomeExpenseViewComponent.prototype.getExpenseAccountBalances = function () {
        var _this = this;
        var dataModel = {
            id: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_3__["AccountHeadTypes_Enum"].Expense,
            asOfDate: this.asOfDate,
            upToDate: this.upToDate,
            currency: this.selectedCurrency
        };
        this.globalService
            .getListByIdAndDate(this.appUrl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_6__["GLOBAL"].API_FinancialReport_GetAllAccountIncomeExpensesByCategory, dataModel)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.NoteAccountBalances != null) {
                    if (data.data.NoteAccountBalances.length > 0) {
                        var expenseList_1 = [];
                        data.data.NoteAccountBalances.forEach(function (element) {
                            expenseList_1.push({
                                NoteId: element.NoteId,
                                NoteName: element.NoteName,
                                NoteHeadId: element.NoteHeadId,
                                AccountBalances: element.AccountBalances,
                                NoteBalance: _this.getSum(element.AccountBalances, 'Balance')
                            });
                        });
                        _this.expenseList = expenseList_1.filter(function (x) { return x.NoteHeadId === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_3__["AccountHeadTypes_Enum"].Expense; });
                    }
                }
                _this.totalExpenses = _this.getSum(_this.expenseList, 'NoteBalance');
                _this.totalNett = _this.totalIncome - _this.totalExpenses;
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error(data.Message);
            }
        });
    };
    //#endregion
    //#region "getDetailOfNotes"
    IncomeExpenseViewComponent.prototype.getDetailOfNotes = function () {
        var _this = this;
        var dataModel = {
            CurrencyId: this.selectedCurrency,
            TillDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].getLocalDate(this.upToDate)
        };
        this.detailOfNoteFlag = true;
        this.incomeExpenseService
            .GetDetailOfNotes(dataModel)
            .subscribe(function (response) {
            if (response.statusCode === 200) {
                if (response.data != null) {
                    if (response.data.length > 0) {
                        response.data.forEach(function (element) {
                            _this.detailsOfNotesFinalList.push({
                                NoteName: element.NoteName,
                                TotalDebits: element.TotalDebits,
                                TotalCredits: element.TotalCredits,
                                Balance: element.Balance,
                                AccountSummary: element.AccountSummary
                            });
                        });
                    }
                }
            }
            else if (response.statusCode === 400) {
                _this.toastr.error(response.message);
            }
            _this.detailOfNoteFlag = false;
        }, function (error) {
            _this.detailOfNoteFlag = false;
        });
    };
    //#endregion
    IncomeExpenseViewComponent.prototype.onExportPdf = function () {
        var doc = new jsPDF();
        var pageHeight = doc.internal.pageSize.height;
        var pageWidth = doc.internal.pageSize.width;
        doc.setFontSize(10);
        doc.setFontSize(14);
        doc.text('COORDINATION OF HUMANITARIAN ASSISTANCE', src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].pdfTextCenter(doc, 'COORDINATION OF HUMANITARIAN ASSISTANCE', 14), 19);
        var text2 = 'INCOME/EXPENDITURE REPORT IN (' +
            this.currencyCodePipe.transform(this.selectedCurrency, this.currencyList) +
            ') AS ' +
            this.asOfDate +
            ' TO ' +
            this.upToDate;
        doc.text(text2, src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].pdfTextCenter(doc, text2, 14), 26);
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, 35, 200, 35);
        doc.setFontSize(12);
        doc.text('ASSETS PROPERTY & CAPITAL', 10, 45);
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, 47, 200, 47);
        doc.setFontSize(10);
        var currentFontHeightLocation = 47;
        // income list
        for (var _i = 0, _a = this.incomeList; _i < _a.length; _i++) {
            var item = _a[_i];
            currentFontHeightLocation += 5;
            if (currentFontHeightLocation >= pageHeight) {
                doc.addPage();
                currentFontHeightLocation = 10;
            }
            doc.text(item.NoteName, 10, currentFontHeightLocation);
            var txtWidthNoteBalance = (doc.getStringUnitWidth(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            })) *
                10) /
                doc.internal.scaleFactor;
            doc.text(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            }), pageWidth - txtWidthNoteBalance - 10, currentFontHeightLocation);
        }
        // Total Income
        currentFontHeightLocation += 10;
        doc.setLineWidth(0.1); // horizontal line
        doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
        currentFontHeightLocation = currentFontHeightLocation + 5;
        doc.text('Total Assets Property & Capital', 10, currentFontHeightLocation);
        var txtWidthTotalIncome = (doc.getStringUnitWidth(this.totalIncome.toLocaleString(undefined, {
            minimumFractionDigits: 2
        })) *
            10) /
            doc.internal.scaleFactor;
        doc.text(this.totalIncome.toLocaleString(undefined, {
            minimumFractionDigits: 2
        }), pageWidth - txtWidthTotalIncome - 10, currentFontHeightLocation);
        doc.setFontSize(12);
        currentFontHeightLocation += 20;
        doc.text('EXPENSES', 10, currentFontHeightLocation);
        currentFontHeightLocation += 2;
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
        doc.setFontSize(10);
        // expense list
        for (var _b = 0, _c = this.expenseList; _b < _c.length; _b++) {
            var item = _c[_b];
            currentFontHeightLocation += 5;
            if (currentFontHeightLocation >= pageHeight) {
                doc.addPage();
                currentFontHeightLocation = 10;
            }
            doc.text(item.NoteName, 10, currentFontHeightLocation);
            var txtWidth = (doc.getStringUnitWidth(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            })) *
                10) /
                doc.internal.scaleFactor;
            doc.text(item.NoteBalance.toLocaleString(undefined, {
                minimumFractionDigits: 2
            }), pageWidth - txtWidth - 10, currentFontHeightLocation);
        }
        if (currentFontHeightLocation >= pageHeight) {
            doc.addPage();
            currentFontHeightLocation = 10;
        }
        // Total Expenditure
        currentFontHeightLocation += 10;
        doc.setLineWidth(0.1); // horizontal line
        doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
        currentFontHeightLocation += 5;
        doc.text('Total Expenditure', 10, currentFontHeightLocation);
        var pageWidthTotalExpenses = (doc.getStringUnitWidth(this.totalExpenses.toLocaleString(undefined, {
            minimumFractionDigits: 2
        })) *
            10) /
            doc.internal.scaleFactor;
        doc.text(this.totalExpenses.toLocaleString(undefined, {
            minimumFractionDigits: 2
        }), pageWidth - pageWidthTotalExpenses - 10, currentFontHeightLocation);
        // Page Break
        if (currentFontHeightLocation >= pageHeight - 10) {
            doc.addPage();
            currentFontHeightLocation = 10;
        }
        // Excess of Expenditure
        currentFontHeightLocation += 20;
        doc.setLineWidth(0.1); // horizontal line
        doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
        currentFontHeightLocation += 10;
        doc.text('Excess of Expenditure (Carried Forward to CHA Own Source Funds)', 10, currentFontHeightLocation);
        var pageWidthExpenditure = (doc.getStringUnitWidth(this.totalNett.toLocaleString(undefined, {
            minimumFractionDigits: 2
        })) *
            10) /
            doc.internal.scaleFactor;
        doc.text(this.totalNett.toLocaleString(undefined, {
            minimumFractionDigits: 2
        }), pageWidth - pageWidthExpenditure - 10, currentFontHeightLocation);
        doc.save('income-expense-report.pdf');
    };
    IncomeExpenseViewComponent.prototype.onDetailOfNotesPdf = function () {
        var doc = new jsPDF();
        var pageHeight = doc.internal.pageSize.height;
        var paddingLeft = 14;
        doc.setFontSize(10);
        doc.setFontSize(14);
        doc.text('COORDINATION OF HUMANITARIAN ASSISTANCE', src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].pdfTextCenter(doc, 'COORDINATION OF HUMANITARIAN ASSISTANCE', 14), 19);
        var text2 = 'NOTES OF THE ACCOUNTS IN (' +
            this.currencyCodePipe.transform(this.selectedCurrency, this.currencyList) +
            ') FOR THE YEAR ' +
            this.upToDate;
        doc.text(text2, src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].pdfTextCenter(doc, text2, 14), 26);
        doc.setLineWidth(0.1); // horizontal line
        doc.line(10, 35, 200, 35);
        // doc.autoTable({
        //   head: [['Account Code', 'Account Name', 'Debit', 'Credit']],
        //   theme: 'plain', // 'striped'|'grid'|'plain'
        //   margin: { top: 36 }
        // });
        doc.setFontSize(10);
        var currentFontHeightLocation = 42;
        doc.text('Account Code', paddingLeft, currentFontHeightLocation);
        doc.text('Account Name', paddingLeft + 30, currentFontHeightLocation);
        doc.text('Debit', paddingLeft + 100, currentFontHeightLocation);
        doc.text('Credit', paddingLeft + 150, currentFontHeightLocation);
        currentFontHeightLocation += 3;
        doc.setLineWidth(0.2); // horizontal line
        doc.line(10, 45, 200, 45);
        doc.setFontSize(10);
        // income list
        // for (let i = 1; i < 1000 ; i++) {
        for (var _i = 0, _a = this.detailsOfNotesFinalList; _i < _a.length; _i++) {
            var item = _a[_i];
            if (currentFontHeightLocation >= pageHeight) {
                doc.addPage();
                currentFontHeightLocation = 10;
            }
            doc.setFontStyle('bold');
            doc.text(item.NoteName, paddingLeft, currentFontHeightLocation += 5);
            currentFontHeightLocation += 3;
            doc.setLineWidth(0.2); // horizontal line
            doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
            for (var _b = 0, _c = item.AccountSummary; _b < _c.length; _b++) {
                var itemSummary = _c[_b];
                if (currentFontHeightLocation >= pageHeight) {
                    doc.addPage();
                    currentFontHeightLocation = 10;
                }
                doc.setFontStyle('normal');
                doc.setFontSize(10);
                currentFontHeightLocation += 5;
                doc.text(itemSummary.AccountCode, paddingLeft, currentFontHeightLocation);
                var splitTitle = doc.splitTextToSize(itemSummary.AccountName, 60);
                doc.text(splitTitle, paddingLeft + 30, currentFontHeightLocation);
                // doc.text(itemSummary.AccountName, paddingLeft + 30, currentFontHeightLocation);
                doc.text('' + itemSummary.Debit, paddingLeft + 100, currentFontHeightLocation);
                doc.text('' + itemSummary.Credit, paddingLeft + 150, currentFontHeightLocation);
                // doc.text('' + itemSummary.Credit, StaticUtilities.pdfTextRight(doc, '' + itemSummary.Credit, 14 ), currentFontHeightLocation);
                if (splitTitle.length > 1) {
                    currentFontHeightLocation += 5;
                }
            }
            currentFontHeightLocation += 5;
            doc.setLineWidth(0.2); // horizontal line
            doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
            doc.setFontStyle('bold');
            doc.text('Balance : ' + item.Balance, 14, currentFontHeightLocation += 5);
            doc.text('' + item.TotalDebits, paddingLeft + 100, currentFontHeightLocation);
            // doc.text('' + item.TotalCredits, StaticUtilities.pdfTextRight(doc, '' + item.TotalCredits, 14 ), currentFontHeightLocation);
            doc.text('' + item.TotalCredits, paddingLeft + 150, currentFontHeightLocation);
            currentFontHeightLocation += 5;
            //   doc.autoTable({
            //     styles: {fillColor: [255, 0, 0]},
            //     columnStyles: {0: {halign: 'center', fillColor: [0, 255, 0]}}, // Cells in first column centered and green
            //     margin: {top: 10},
            //     body: [['Sweden', 'Japan', 'Canada'], ['Norway', 'China', 'USA'], ['Denmark', 'China', 'Mexico']]
            // })
            // doc.autoTable({
            //   head: [[item.NoteName]],
            //   theme: 'plain', // 'striped'|'grid'|'plain'
            //   pageBreak: 'auto', // 'auto'|'avoid'|'always',
            //   showHead:  'firstPage', // 'everyPage'|'firstPage'|'never'
            //   columnStyles: {0: {halign: 'center', fillColor: [0, 255, 0]}}, // Cells in first column centered and green
            //   startY: doc.previousAutoTable.finalY + 5
            // });
            // doc.autoTable({
            //   fillColor: [255, 0, 0],
            //   columnStyles: {0: {halign: 'center', fillColor: [0, 255, 0]}}, // Cells in first column centered and green
            //   theme: 'striped', // 'striped'|'grid'|'plain'
            //   pageBreak: 'auto', // 'auto'|'avoid'|'always',
            //   showHead:  'firstPage', // 'everyPage'|'firstPage'|'never'
            //   startY: doc.previousAutoTable.finalY + 2,
            //   body: item.AccountSummary
            // });
            // currentFontHeightLocation += 2;
            // if (currentFontHeightLocation >= pageHeight) {
            //   doc.addPage();
            //   currentFontHeightLocation = 10;
            // }
            // doc.autoTable({
            //   head: [['Balance : ' + item.Balance, '', item.TotalDebits, item.TotalCredits]],
            //   theme: 'plain', // 'striped'|'grid'|'plain'
            //   // margin: { top: currentFontHeightLocation }
            //   startY: doc.previousAutoTable.finalY + 2
            // });
            // currentFontHeightLocation = doc.previousAutoTable.finalY + 5;
            // doc.text('Balance : ' + item.Balance, 14, currentFontHeightLocation);
            // doc.text('' + item.TotalDebits, StaticUtilities.pdfTextRight(doc, '' + item.TotalCredits, 14 ) - debitX, currentFontHeightLocation);
            // doc.text('' + item.TotalCredits, StaticUtilities.pdfTextRight(doc, '' + item.TotalCredits, 14 ), currentFontHeightLocation);
        }
        doc.save('detail-of-notes.pdf');
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])('incomeExpenseReportTemplate'),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])
    ], IncomeExpenseViewComponent.prototype, "elementRef", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], IncomeExpenseViewComponent.prototype, "getScreenSize", null);
    IncomeExpenseViewComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-income-expense-view',
            template: __webpack_require__(/*! ./income-expense-view.component.html */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.html"),
            styles: [__webpack_require__(/*! ./income-expense-view.component.scss */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense-view/income-expense-view.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_4__["GlobalService"],
            _income_expense_service__WEBPACK_IMPORTED_MODULE_7__["IncomeExpenseService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_5__["AppUrlService"],
            src_app_shared_pipes_currency_code_pipe__WEBPACK_IMPORTED_MODULE_8__["CurrencyCodePipe"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"]])
    ], IncomeExpenseViewComponent);
    return IncomeExpenseViewComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.html":
/*!****************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.html ***!
  \****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"body-content\">\r\n  <div class=\"income-expense-main\">\r\n    <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-10 col-sm-offset-1\">\r\n          <mat-card [ngStyle]=\"scrollStyles\">\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-6\">\r\n                <h4 class=\"main_heading bdr_heading\">\r\n                  Income\r\n                </h4>\r\n              </div>\r\n              <div class=\"col-sm-6\">\r\n                <div class=\"pull-right\">\r\n                  <button\r\n                    mat-icon-button\r\n                    pTooltip=\"Filter\"\r\n                    tooltipPosition=\"top\"\r\n                    (click)=\"show = !show\"\r\n                  >\r\n                    <i class=\"fa black_icon fa-sliders-h font_x_large\"> </i>\r\n                  </button>\r\n\r\n                  <button\r\n                    mat-icon-button\r\n                    color=\"accent\"\r\n                    pTooltip=\"View Report\"\r\n                    tooltipPosition=\"top\"\r\n                    [routerLink]=\"[\r\n                      'view',\r\n                      selectedDate | date: 'yyyy-MM-dd',\r\n                      selectedToDate | date: 'yyyy-MM-dd',\r\n                      selectedCurrency\r\n                    ]\"\r\n                  >\r\n                    <i class=\"fa fa-clipboard font_x_large\"></i>\r\n                  </button>\r\n                </div>\r\n              </div>\r\n            </div>\r\n\r\n            <!-- filter -->\r\n            <div *ngIf=\"show\" class=\"filtersPopup\">\r\n              <mat-form-field>\r\n                <input\r\n                  matInput\r\n                  [matDatepicker]=\"picker\"\r\n                  placeholder=\"As of date\"\r\n                  [(ngModel)]=\"selectedDate\"\r\n                />\r\n                <mat-datepicker-toggle\r\n                  matSuffix\r\n                  [for]=\"picker\"\r\n                ></mat-datepicker-toggle>\r\n                <mat-datepicker #picker></mat-datepicker>\r\n              </mat-form-field>\r\n              <mat-form-field>\r\n                <input\r\n                  matInput\r\n                  [matDatepicker]=\"picker2\"\r\n                  placeholder=\"Up to date\"\r\n                  [(ngModel)]=\"selectedToDate\"\r\n                />\r\n                <mat-datepicker-toggle\r\n                  matSuffix\r\n                  [for]=\"picker2\"\r\n                ></mat-datepicker-toggle>\r\n                <mat-datepicker #picker2></mat-datepicker>\r\n              </mat-form-field>\r\n              <mat-form-field class=\"settings-full-width\">\r\n                <mat-select\r\n                  placeholder=\"Currency\"\r\n                  [(ngModel)]=\"selectedCurrency\"\r\n                  class=\"settings-full-width\"\r\n                >\r\n                  <mat-option\r\n                    *ngFor=\"let currency of currencyList\"\r\n                    [value]=\"currency.CurrencyId\"\r\n                  >\r\n                    {{ currency.CurrencyCode }}\r\n                  </mat-option>\r\n                </mat-select>\r\n              </mat-form-field>\r\n              <button class=\"pull-right\" (click)=\"show = !show\">OK</button>\r\n            </div>\r\n            <!-- filter -->\r\n\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-12\">\r\n                <mat-spinner\r\n                  *ngIf=\"incomeListLoaderFlag\"\r\n                  [diameter]=\"20\"\r\n                ></mat-spinner>\r\n                <tr *ngFor=\"let item of incomeList\">\r\n                  <td>\r\n                    <lib-inline-edit\r\n                      [value]=\"item\"\r\n                      (action)=\"onValueChangeEmit($event)\"\r\n                      (addAction)=\"addActionEmit($event)\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    ></lib-inline-edit>\r\n                  </td>\r\n                </tr>\r\n                <div *ngIf=\"inputFieldIncomeFlag\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <input\r\n                      #income\r\n                      matInput\r\n                      type=\"text\"\r\n                      maxlength=\"150\"\r\n                      name=\"income\"\r\n                      (keyup.enter)=\"onAdd(INCOME_ID, income.value)\"\r\n                      (change)=\"onAdd(INCOME_ID, income.value)\"\r\n                    />\r\n                  </mat-form-field>\r\n                </div>\r\n\r\n                <button\r\n                  mat-icon-button\r\n                  (click)=\"toggleInputFieldIncome()\"\r\n                  *ngIf=\"isEditingAllowed\"\r\n                >\r\n                  <mat-icon aria-label=\"icon\"> add_circle_outline</mat-icon>\r\n                </button>\r\n              </div>\r\n            </div>\r\n\r\n            <div class=\"row margin_top_50\">\r\n              <div class=\"col-sm-6\">\r\n                <h4 class=\"main_heading bdr_heading\">Expenses</h4>\r\n              </div>\r\n            </div>\r\n\r\n            <div class=\"row\">\r\n              <div class=\"col-sm-12\">\r\n                <mat-spinner\r\n                  *ngIf=\"expenseListLoaderFlag\"\r\n                  [diameter]=\"20\"\r\n                ></mat-spinner>\r\n                <tr *ngFor=\"let item of expenseList\">\r\n                  <td>\r\n                    <lib-inline-edit\r\n                      [value]=\"item\"\r\n                      (action)=\"onValueChangeEmit($event)\"\r\n                      (addAction)=\"addActionEmit($event)\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    ></lib-inline-edit>\r\n                  </td>\r\n                </tr>\r\n                <div *ngIf=\"inputFieldExpenseFlag\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <input\r\n                      #expense\r\n                      matInput\r\n                      type=\"text\"\r\n                      maxlength=\"150\"\r\n                      name=\"expense\"\r\n                      (keyup.enter)=\"onAdd(EXPENSE_ID, expense.value)\"\r\n                      (change)=\"onAdd(EXPENSE_ID, expense.value)\"\r\n                    />\r\n                  </mat-form-field>\r\n                </div>\r\n\r\n                <button\r\n                  mat-icon-button\r\n                  (click)=\"toggleInputFieldExpense()\"\r\n                  *ngIf=\"isEditingAllowed\"\r\n                >\r\n                  <mat-icon aria-label=\"icon\"> add_circle_outline</mat-icon>\r\n                </button>\r\n              </div>\r\n            </div>\r\n          </mat-card>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.scss":
/*!****************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.scss ***!
  \****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".font_smallcaps_12 {\n  text-transform: uppercase;\n  font-variant-caps: all-small-caps; }\n\n.filtersPopup {\n  position: absolute;\n  top: 35px;\n  right: 60px;\n  background-color: white;\n  padding: 12px;\n  max-width: 235px;\n  border: 1px solid silver;\n  z-index: 100; }\n\n.settings-full-width {\n  width: 100%; }\n\n.settings-full-width:nth-last-child() {\n  margin-bottom: 10px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvZmluYW5jaWFsLXJlcG9ydC9pbmNvbWUtZXhwZW5zZS9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxkYXNoYm9hcmRcXGFjY291bnRpbmdcXGZpbmFuY2lhbC1yZXBvcnRcXGluY29tZS1leHBlbnNlXFxpbmNvbWUtZXhwZW5zZS5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNJLHlCQUF5QjtFQUN6QixpQ0FBaUMsRUFBQTs7QUFHckM7RUFDSSxrQkFBaUI7RUFBRSxTQUFRO0VBQUUsV0FBVztFQUN4Qyx1QkFBdUI7RUFDdkIsYUFBYTtFQUNiLGdCQUFnQjtFQUNoQix3QkFBd0I7RUFDeEIsWUFBWSxFQUFBOztBQU9kO0VBQ0UsV0FBVyxFQUFBOztBQUdiO0VBQ0UsbUJBQW1CLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9kYXNoYm9hcmQvYWNjb3VudGluZy9maW5hbmNpYWwtcmVwb3J0L2luY29tZS1leHBlbnNlL2luY29tZS1leHBlbnNlLmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsiLmZvbnRfc21hbGxjYXBzXzEyIHtcclxuICAgIHRleHQtdHJhbnNmb3JtOiB1cHBlcmNhc2U7XHJcbiAgICBmb250LXZhcmlhbnQtY2FwczogYWxsLXNtYWxsLWNhcHM7XHJcbn1cclxuXHJcbi5maWx0ZXJzUG9wdXAge1xyXG4gICAgcG9zaXRpb246YWJzb2x1dGU7IHRvcDozNXB4OyByaWdodDogNjBweDtcclxuICAgIGJhY2tncm91bmQtY29sb3I6IHdoaXRlO1xyXG4gICAgcGFkZGluZzogMTJweDtcclxuICAgIG1heC13aWR0aDogMjM1cHg7XHJcbiAgICBib3JkZXI6IDFweCBzb2xpZCBzaWx2ZXI7XHJcbiAgICB6LWluZGV4OiAxMDA7XHJcbn1cclxuXHJcblxyXG5cclxuXHJcbiAgXHJcbiAgLnNldHRpbmdzLWZ1bGwtd2lkdGgge1xyXG4gICAgd2lkdGg6IDEwMCU7XHJcbiAgfVxyXG4gIFxyXG4gIC5zZXR0aW5ncy1mdWxsLXdpZHRoOm50aC1sYXN0LWNoaWxkKCkge1xyXG4gICAgbWFyZ2luLWJvdHRvbTogMTBweDtcclxuICB9Il19 */"

/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.ts":
/*!**************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.ts ***!
  \**************************************************************************************************/
/*! exports provided: IncomeExpenseComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "IncomeExpenseComponent", function() { return IncomeExpenseComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _shared_enum__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var _shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var _shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../../shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _shared_global__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../../shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../../../shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var _shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../../../shared/services/localstorage.service */ "./src/app/shared/services/localstorage.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var IncomeExpenseComponent = /** @class */ (function () {
    //#endregion
    function IncomeExpenseComponent(globalService, appUrl, toastr, localStorageService) {
        this.globalService = globalService;
        this.appUrl = appUrl;
        this.toastr = toastr;
        this.localStorageService = localStorageService;
        //#region "variables"
        this.show = false;
        // CONST
        this.INCOME_ID = _shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"].Income;
        this.EXPENSE_ID = _shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountHeadTypes_Enum"].Expense;
        // DATASOURCE
        this.incomeList = [];
        this.expenseList = [];
        this.currencyList = [];
        // FLAG
        this.inputFieldIncomeFlag = false;
        this.inputFieldExpenseFlag = false;
        this.incomeListLoaderFlag = false;
        this.expenseListLoaderFlag = false;
        this.isEditingAllowed = false;
        this.pageId = _shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_6__["ApplicationPages"].IncomeExpenseReport;
        this.getScreenSize();
    }
    IncomeExpenseComponent.prototype.ngOnInit = function () {
        this.getCurrencies();
        this.getIncomeExpenseAccountTypes();
        this.selectedDate = new Date();
        this.selectedToDate = new Date();
        this.selectedCurrency = 1;
    };
    //#region "Dynamic Scroll"
    IncomeExpenseComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    IncomeExpenseComponent.prototype.getCurrencies = function () {
        var _this = this;
        this.globalService
            .getList(this.appUrl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_code_GetAllCurrency)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.CurrencyList != null) {
                    if (data.data.CurrencyList.length > 0) {
                        var currencyList = [];
                        data.data.CurrencyList.forEach(function (element) {
                            _this.currencyList.push({
                                CurrencyId: element.CurrencyId,
                                CurrencyCode: element.CurrencyCode
                            });
                        });
                    }
                }
            }
            else if (data.StatusCode === 400) {
            }
        });
        this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
    };
    //#region "get"
    IncomeExpenseComponent.prototype.getIncomeExpenseAccountTypes = function () {
        var _this = this;
        this.incomeListLoaderFlag = true;
        this.expenseListLoaderFlag = true;
        this.globalService
            .getListById(this.appUrl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_GetAllAccountTypeByCategory, _shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountCategory_Enum"].IncomeExpenseReport)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (data.data.AccountTypeList != null) {
                    if (data.data.AccountTypeList.length > 0) {
                        var dataList_1 = [];
                        data.data.AccountTypeList.forEach(function (element) {
                            dataList_1.push({
                                Id: element.AccountTypeId,
                                Name: element.AccountTypeName,
                                AccountCategory: element.AccountCategory,
                                AccountHeadTypeId: element.AccountHeadTypeId,
                                Balance: 0
                            });
                        });
                        _this.incomeList = dataList_1.filter(function (x) { return x.AccountHeadTypeId === _this.INCOME_ID; });
                        _this.expenseList = dataList_1.filter(function (x) { return x.AccountHeadTypeId === _this.EXPENSE_ID; });
                        dataList_1 = []; // empty
                    }
                }
            }
            else if (data.StatusCode === 400) {
                _this.toastr.error('Something went wrong ! Try Again');
            }
            _this.incomeListLoaderFlag = false;
            _this.expenseListLoaderFlag = false;
        }, function (error) {
            _this.incomeListLoaderFlag = false;
            _this.expenseListLoaderFlag = false;
        });
    };
    //#endregion
    //#region "add"
    IncomeExpenseComponent.prototype.addIncomeExpenseAccountTypes = function (model) {
        var _this = this;
        var index = model._index;
        var accountHeadTypeId = model.AccountHeadTypeId;
        // error handling
        if (accountHeadTypeId === this.INCOME_ID) {
            this.incomeList[index]._IsLoading = true;
            this.incomeList[index]._IsError = false;
        }
        else if (accountHeadTypeId === this.EXPENSE_ID) {
            this.expenseList[index]._IsLoading = true;
            this.expenseList[index]._IsError = false;
        }
        var obj = {
            AccountTypeId: model.Id,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountCategory: model.AccountCategory,
            AccountTypeName: model.Name
        };
        this.globalService
            .post(this.appUrl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_AddAccountType, obj)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (accountHeadTypeId === _this.INCOME_ID) {
                    _this.incomeList[index]._IsLoading = false;
                    _this.incomeList[index].Id = data.CommonId.Id;
                }
                else if (accountHeadTypeId === _this.EXPENSE_ID) {
                    _this.expenseList[index]._IsLoading = false;
                    _this.expenseList[index].Id = data.CommonId.Id;
                }
            }
            else if (data.StatusCode === 400) {
                if (accountHeadTypeId === _this.INCOME_ID) {
                    _this.incomeList[index]._IsLoading = false;
                    _this.incomeList[index]._IsError = true;
                }
                else if (accountHeadTypeId === _this.EXPENSE_ID) {
                    _this.expenseList[index]._IsLoading = false;
                    _this.expenseList[index]._IsError = true;
                }
                _this.toastr.error(data.Message);
            }
        }, function (error) {
            // error handling
            if (accountHeadTypeId === _this.INCOME_ID) {
                _this.incomeList[index]._IsLoading = false;
                _this.incomeList[index]._IsError = true;
            }
            else if (accountHeadTypeId === _this.EXPENSE_ID) {
                _this.expenseList[index]._IsLoading = false;
                _this.expenseList[index]._IsError = true;
            }
            _this.toastr.error('Something went wrong ! Try Again');
        });
    };
    //#endregion
    //#region "edit"
    IncomeExpenseComponent.prototype.editIncomeExpenseAccountTypes = function (model) {
        var _this = this;
        var obj = {
            AccountTypeId: model.Id,
            AccountHeadTypeId: model.AccountHeadTypeId,
            AccountCategory: model.AccountCategory,
            AccountTypeName: model.Name
        };
        //#region "error handling"
        if (model.AccountHeadTypeId === this.INCOME_ID) {
            var item = this.incomeList.find(function (x) { return x.Id === model.Id; });
            var index = this.incomeList.indexOf(item);
            this.incomeList[index]._IsError = false;
            this.incomeList[index]._IsLoading = true;
        }
        else if (model.AccountHeadTypeId === this.EXPENSE_ID) {
            var item = this.expenseList.find(function (x) { return x.Id === model.Id; });
            var index = this.expenseList.indexOf(item);
            this.expenseList[index]._IsError = false;
            this.expenseList[index]._IsLoading = true;
        }
        //#endregion
        this.globalService
            .post(this.appUrl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_EditAccountType, obj)
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                //#region "error handling"
                if (model.AccountHeadTypeId === _this.INCOME_ID) {
                    var item = _this.incomeList.find(function (x) { return x.Id === model.Id; });
                    var index = _this.incomeList.indexOf(item);
                    _this.incomeList[index]._IsError = false;
                    _this.incomeList[index]._IsLoading = false;
                }
                else if (model.AccountHeadTypeId === _this.EXPENSE_ID) {
                    var item = _this.expenseList.find(function (x) { return x.Id === model.Id; });
                    var index = _this.expenseList.indexOf(item);
                    _this.expenseList[index]._IsError = false;
                    _this.expenseList[index]._IsLoading = false;
                }
                //#endregion
            }
            else if (data.StatusCode === 400) {
                //#region "error handling"
                if (model.AccountHeadTypeId === _this.INCOME_ID) {
                    var item = _this.incomeList.find(function (x) { return x.Id === model.Id; });
                    var index = _this.incomeList.indexOf(item);
                    _this.incomeList[index]._IsError = true;
                    _this.incomeList[index]._IsLoading = false;
                }
                else if (model.AccountHeadTypeId === _this.EXPENSE_ID) {
                    var item = _this.expenseList.find(function (x) { return x.Id === model.Id; });
                    var index = _this.expenseList.indexOf(item);
                    _this.expenseList[index]._IsError = true;
                    _this.expenseList[index]._IsLoading = false;
                }
                //#endregion
                _this.toastr.error('\'' + obj.AccountTypeName + '\'' + data.Message);
            }
        }, function (error) {
            //#region "error handling"
            if (model.AccountHeadTypeId === _this.INCOME_ID) {
                var item = _this.incomeList.find(function (x) { return x.Id === model.Id; });
                var index = _this.incomeList.indexOf(item);
                _this.incomeList[index]._IsError = true;
                _this.incomeList[index]._IsLoading = false;
            }
            else if (model.AccountHeadTypeId === _this.EXPENSE_ID) {
                var item = _this.expenseList.find(function (x) { return x.Id === model.Id; });
                var index = _this.expenseList.indexOf(item);
                _this.expenseList[index]._IsError = true;
                _this.expenseList[index]._IsLoading = false;
            }
            //#endregion
            _this.toastr.error('Something went wrong !');
        });
    };
    //#endregion
    //#region "onAdd"
    IncomeExpenseComponent.prototype.onAdd = function (type, value) {
        var obj = {};
        if (type === this.INCOME_ID) {
            this.toggleInputFieldIncome();
            obj = {
                Id: 0,
                AccountHeadTypeId: this.INCOME_ID,
                AccountCategory: _shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountCategory_Enum"].IncomeExpenseReport,
                Name: value,
                _index: this.incomeList.length,
                _IsError: false
            };
            this.incomeList.push(obj);
        }
        else if (type === this.EXPENSE_ID) {
            this.toggleInputFieldExpense();
            obj = {
                Id: 0,
                AccountHeadTypeId: this.EXPENSE_ID,
                AccountCategory: _shared_enum__WEBPACK_IMPORTED_MODULE_1__["AccountCategory_Enum"].IncomeExpenseReport,
                Name: value,
                _index: this.expenseList.length,
                _IsError: false
            };
            this.expenseList.push(obj);
        }
        this.addIncomeExpenseAccountTypes(obj);
    };
    //#endregion
    //#region "Emit"
    IncomeExpenseComponent.prototype.onValueChangeEmit = function (data) {
        this.editIncomeExpenseAccountTypes(data);
    };
    IncomeExpenseComponent.prototype.addActionEmit = function (data) {
        this.addIncomeExpenseAccountTypes(data);
    };
    //#endregion
    //#region "show / hide"
    IncomeExpenseComponent.prototype.toggleInputFieldIncome = function () {
        this.inputFieldIncomeFlag = !this.inputFieldIncomeFlag;
    };
    IncomeExpenseComponent.prototype.toggleInputFieldExpense = function () {
        this.inputFieldExpenseFlag = !this.inputFieldExpenseFlag;
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], IncomeExpenseComponent.prototype, "getScreenSize", null);
    IncomeExpenseComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-income-expense',
            template: __webpack_require__(/*! ./income-expense.component.html */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.html"),
            styles: [__webpack_require__(/*! ./income-expense.component.scss */ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.component.scss")]
        }),
        __metadata("design:paramtypes", [_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_2__["GlobalService"],
            _shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_3__["AppUrlService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_5__["ToastrService"],
            _shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_7__["LocalStorageService"]])
    ], IncomeExpenseComponent);
    return IncomeExpenseComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/financial-report/income-expense/income-expense.service.ts":
/*!************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/financial-report/income-expense/income-expense.service.ts ***!
  \************************************************************************************************/
/*! exports provided: IncomeExpenseService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "IncomeExpenseService", function() { return IncomeExpenseService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/operators/map */ "./node_modules/rxjs/internal/operators/map.js");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var IncomeExpenseService = /** @class */ (function () {
    function IncomeExpenseService(globalService, appurl) {
        this.globalService = globalService;
        this.appurl = appurl;
    }
    //#region "GetCurrencyList"
    IncomeExpenseService.prototype.GetCurrencyList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllCurrency)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.CurrencyList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetDetailOfNotes"
    IncomeExpenseService.prototype.GetDetailOfNotes = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_FinancialReport_GetDetailOfNotes, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.DetailsOfNotesFinalList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    IncomeExpenseService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_2__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_1__["AppUrlService"]])
    ], IncomeExpenseService);
    return IncomeExpenseService;
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
//# sourceMappingURL=financial-report-financial-report-module.js.map