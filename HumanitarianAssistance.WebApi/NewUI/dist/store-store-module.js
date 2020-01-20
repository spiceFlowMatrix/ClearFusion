(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["store-store-module"],{

/***/ "./node_modules/rxjs/internal/observable/forkJoin.js":
/*!***********************************************************!*\
  !*** ./node_modules/rxjs/internal/observable/forkJoin.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var Observable_1 = __webpack_require__(/*! ../Observable */ "./node_modules/rxjs/internal/Observable.js");
var isArray_1 = __webpack_require__(/*! ../util/isArray */ "./node_modules/rxjs/internal/util/isArray.js");
var empty_1 = __webpack_require__(/*! ./empty */ "./node_modules/rxjs/internal/observable/empty.js");
var subscribeToResult_1 = __webpack_require__(/*! ../util/subscribeToResult */ "./node_modules/rxjs/internal/util/subscribeToResult.js");
var OuterSubscriber_1 = __webpack_require__(/*! ../OuterSubscriber */ "./node_modules/rxjs/internal/OuterSubscriber.js");
var map_1 = __webpack_require__(/*! ../operators/map */ "./node_modules/rxjs/internal/operators/map.js");
function forkJoin() {
    var sources = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        sources[_i] = arguments[_i];
    }
    var resultSelector;
    if (typeof sources[sources.length - 1] === 'function') {
        resultSelector = sources.pop();
    }
    if (sources.length === 1 && isArray_1.isArray(sources[0])) {
        sources = sources[0];
    }
    if (sources.length === 0) {
        return empty_1.EMPTY;
    }
    if (resultSelector) {
        return forkJoin(sources).pipe(map_1.map(function (args) { return resultSelector.apply(void 0, args); }));
    }
    return new Observable_1.Observable(function (subscriber) {
        return new ForkJoinSubscriber(subscriber, sources);
    });
}
exports.forkJoin = forkJoin;
var ForkJoinSubscriber = (function (_super) {
    __extends(ForkJoinSubscriber, _super);
    function ForkJoinSubscriber(destination, sources) {
        var _this = _super.call(this, destination) || this;
        _this.sources = sources;
        _this.completed = 0;
        _this.haveValues = 0;
        var len = sources.length;
        _this.values = new Array(len);
        for (var i = 0; i < len; i++) {
            var source = sources[i];
            var innerSubscription = subscribeToResult_1.subscribeToResult(_this, source, null, i);
            if (innerSubscription) {
                _this.add(innerSubscription);
            }
        }
        return _this;
    }
    ForkJoinSubscriber.prototype.notifyNext = function (outerValue, innerValue, outerIndex, innerIndex, innerSub) {
        this.values[outerIndex] = innerValue;
        if (!innerSub._hasValue) {
            innerSub._hasValue = true;
            this.haveValues++;
        }
    };
    ForkJoinSubscriber.prototype.notifyComplete = function (innerSub) {
        var _a = this, destination = _a.destination, haveValues = _a.haveValues, values = _a.values;
        var len = values.length;
        if (!innerSub._hasValue) {
            destination.complete();
            return;
        }
        this.completed++;
        if (this.completed !== len) {
            return;
        }
        if (haveValues === len) {
            destination.next(values);
        }
        destination.complete();
    };
    return ForkJoinSubscriber;
}(OuterSubscriber_1.OuterSubscriber));
//# sourceMappingURL=forkJoin.js.map

/***/ }),

/***/ "./projects/library/src/sub-header-template/sub-header-template.component.css":
/*!************************************************************************************!*\
  !*** ./projects/library/src/sub-header-template/sub-header-template.component.css ***!
  \************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".sub_header_template_main {\r\n  background-color: white;\r\n  height: 55px;\r\n  padding: 10px 15px;\r\n  box-shadow: 0 2px 7px -5px #000000d6;\r\n}\r\n.sub_header_template_main1 {\r\n  background-color: white;\r\n  height: 55px;\r\n  padding: 10px 15px;\r\n  box-shadow: none;\r\n}\r\n.sub_header_template_main2 {\r\n  background-color: white;\r\n  height: 70px;\r\n  padding: 10px 15px;\r\n  box-shadow: none;\r\n}\r\n.float_right { float: right;}\r\n.display_inline { display: inline-block; margin-top: 0px !important}\r\n\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInByb2plY3RzL2xpYnJhcnkvc3JjL3N1Yi1oZWFkZXItdGVtcGxhdGUvc3ViLWhlYWRlci10ZW1wbGF0ZS5jb21wb25lbnQuY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0UsdUJBQXVCO0VBQ3ZCLFlBQVk7RUFDWixrQkFBa0I7RUFDbEIsb0NBQW9DO0FBQ3RDO0FBQ0E7RUFDRSx1QkFBdUI7RUFDdkIsWUFBWTtFQUNaLGtCQUFrQjtFQUNsQixnQkFBZ0I7QUFDbEI7QUFDQTtFQUNFLHVCQUF1QjtFQUN2QixZQUFZO0VBQ1osa0JBQWtCO0VBQ2xCLGdCQUFnQjtBQUNsQjtBQUNBLGVBQWUsWUFBWSxDQUFDO0FBQzVCLGtCQUFrQixxQkFBcUIsRUFBRSwwQkFBMEIiLCJmaWxlIjoicHJvamVjdHMvbGlicmFyeS9zcmMvc3ViLWhlYWRlci10ZW1wbGF0ZS9zdWItaGVhZGVyLXRlbXBsYXRlLmNvbXBvbmVudC5jc3MiLCJzb3VyY2VzQ29udGVudCI6WyIuc3ViX2hlYWRlcl90ZW1wbGF0ZV9tYWluIHtcclxuICBiYWNrZ3JvdW5kLWNvbG9yOiB3aGl0ZTtcclxuICBoZWlnaHQ6IDU1cHg7XHJcbiAgcGFkZGluZzogMTBweCAxNXB4O1xyXG4gIGJveC1zaGFkb3c6IDAgMnB4IDdweCAtNXB4ICMwMDAwMDBkNjtcclxufVxyXG4uc3ViX2hlYWRlcl90ZW1wbGF0ZV9tYWluMSB7XHJcbiAgYmFja2dyb3VuZC1jb2xvcjogd2hpdGU7XHJcbiAgaGVpZ2h0OiA1NXB4O1xyXG4gIHBhZGRpbmc6IDEwcHggMTVweDtcclxuICBib3gtc2hhZG93OiBub25lO1xyXG59XHJcbi5zdWJfaGVhZGVyX3RlbXBsYXRlX21haW4yIHtcclxuICBiYWNrZ3JvdW5kLWNvbG9yOiB3aGl0ZTtcclxuICBoZWlnaHQ6IDcwcHg7XHJcbiAgcGFkZGluZzogMTBweCAxNXB4O1xyXG4gIGJveC1zaGFkb3c6IG5vbmU7XHJcbn1cclxuLmZsb2F0X3JpZ2h0IHsgZmxvYXQ6IHJpZ2h0O31cclxuLmRpc3BsYXlfaW5saW5lIHsgZGlzcGxheTogaW5saW5lLWJsb2NrOyBtYXJnaW4tdG9wOiAwcHggIWltcG9ydGFudH1cclxuIl19 */"

/***/ }),

/***/ "./projects/library/src/sub-header-template/sub-header-template.component.html":
/*!*************************************************************************************!*\
  !*** ./projects/library/src/sub-header-template/sub-header-template.component.html ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div [ngClass]=\"headerClass\">\r\n  <h4 class=\"display_inline\">\r\n    <ng-content select=\".action_header\"></ng-content>\r\n  </h4>\r\n\r\n  <div class=\"float_right\">\r\n    <ng-content select=\".action_section\"></ng-content>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./projects/library/src/sub-header-template/sub-header-template.component.ts":
/*!***********************************************************************************!*\
  !*** ./projects/library/src/sub-header-template/sub-header-template.component.ts ***!
  \***********************************************************************************/
/*! exports provided: SubHeaderTemplateComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SubHeaderTemplateComponent", function() { return SubHeaderTemplateComponent; });
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

var SubHeaderTemplateComponent = /** @class */ (function () {
    function SubHeaderTemplateComponent() {
        this.headerClass = 'sub_header_template_main';
    }
    SubHeaderTemplateComponent.prototype.ngOnInit = function () {
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], SubHeaderTemplateComponent.prototype, "headerClass", void 0);
    SubHeaderTemplateComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'lib-sub-header-template',
            template: __webpack_require__(/*! ./sub-header-template.component.html */ "./projects/library/src/sub-header-template/sub-header-template.component.html"),
            changeDetection: _angular_core__WEBPACK_IMPORTED_MODULE_0__["ChangeDetectionStrategy"].OnPush,
            styles: [__webpack_require__(/*! ./sub-header-template.component.css */ "./projects/library/src/sub-header-template/sub-header-template.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], SubHeaderTemplateComponent);
    return SubHeaderTemplateComponent;
}());



/***/ }),

/***/ "./projects/library/src/sub-header-template/sub-header-template.module.ts":
/*!********************************************************************************!*\
  !*** ./projects/library/src/sub-header-template/sub-header-template.module.ts ***!
  \********************************************************************************/
/*! exports provided: SubHeaderTemplateModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SubHeaderTemplateModule", function() { return SubHeaderTemplateModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _sub_header_template_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./sub-header-template.component */ "./projects/library/src/sub-header-template/sub-header-template.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var SubHeaderTemplateModule = /** @class */ (function () {
    function SubHeaderTemplateModule() {
    }
    SubHeaderTemplateModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [_sub_header_template_component__WEBPACK_IMPORTED_MODULE_2__["SubHeaderTemplateComponent"]],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"]
            ],
            exports: [
                _sub_header_template_component__WEBPACK_IMPORTED_MODULE_2__["SubHeaderTemplateComponent"]
            ]
        })
    ], SubHeaderTemplateModule);
    return SubHeaderTemplateModule;
}());



/***/ }),

/***/ "./src/app/dashboard/project-management/project-list/budgetlines/budget-line.service.ts":
/*!**********************************************************************************************!*\
  !*** ./src/app/dashboard/project-management/project-list/budgetlines/budget-line.service.ts ***!
  \**********************************************************************************************/
/*! exports provided: BudgetLineService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BudgetLineService", function() { return BudgetLineService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var _shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _shared_global__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../../shared/global */ "./src/app/shared/global.ts");
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





var BudgetLineService = /** @class */ (function () {
    function BudgetLineService(globalService, appurl) {
        this.globalService = globalService;
        this.appurl = appurl;
    }
    //#region "GetVoucherList"
    BudgetLineService.prototype.GetVoucherList = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_VoucherTransaction_GetAllVoucherList, data);
    };
    //#endregion
    //#region "AddbudgetLineDetailList"
    BudgetLineService.prototype.AddBudgetLineDetail = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_BudgetLine_AddBudgetLineDetail, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetCurrencyList"
    BudgetLineService.prototype.GetCurrencyList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllCurrency)
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
    //#region "GetProjectJobList"
    BudgetLineService.prototype.GetProjectJobList = function (selectedProjectId) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_BudgetLine_GetProjectJobDetailByProjectId, selectedProjectId)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectJobDetail,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetProjectBudgetLineList"
    BudgetLineService.prototype.GetProjectBudgetLineList = function (projecId) {
        return this.globalService
            .getListById(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_BudgetLine_GetProjectBudgetLineDetail, projecId)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectBudgetLineDetailList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetProjectBudgetLineList"
    BudgetLineService.prototype.GetProjectBudgetdetailList = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_BudgetLine_GetAllBudgetLineList, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.spProjectBudgetLineList,
                total: x.data.TotalCount,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetBudgetDetailByBudgetIdId"
    BudgetLineService.prototype.GetBudgetDetailByBudgetIdId = function (budgetId) {
        return this.globalService
            .getListById(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_BudgetLine_GetBudgetLineDetailByBudgetId, budgetId)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectBudgetLineDetailByBudgetId,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "EditVoucherDetailById"
    BudgetLineService.prototype.EditBudgetLineDetailById = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_BudgetLine_AddBudgetLineDetail, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: null,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetTransactionList"
    BudgetLineService.prototype.GetTransationList = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_BudgetLine_GetTransactionList, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.TransactionBudgetModelList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "postBudgetLineDocument"
    BudgetLineService.prototype.postBudgetLineDocument = function (data) {
        // console.log(data);
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_BudgetLine_ExcelImportOfBudgetLine, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.activityDocumnentDetail,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    BudgetLineService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            _shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"]])
    ], BudgetLineService);
    return BudgetLineService;
}());



/***/ }),

/***/ "./src/app/store/components/add-hours/add-hours.component.html":
/*!*********************************************************************!*\
  !*** ./src/app/store/components/add-hours/add-hours.component.html ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title> Generator Usage Form</h1>\r\n  <div mat-dialog-subtitle>Please enter number of Hours the Generator has been used.</div>\r\n  <div mat-dialog-subtitle><b>Note:</b> User may provide a negative value if the generator's total incurred usage needs\r\n    to be reduced for some reason.</div>\r\n  <br />\r\n  <form [formGroup]=\"addUsageHourForm\" (ngSubmit)=\"addHours()\">\r\n\r\n\r\n    <mat-dialog-content>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-3\">\r\n            <mat-form-field class=\"example-full-width\">\r\n              <input matInput type=\"number\" placeholder=\"Usage(Hours)\" formControlName=\"Hours\">\r\n            </mat-form-field>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <mat-form-field>\r\n              <input matInput [matDatepicker]=\"picker\" [max]=\"maxDate\" formControlName=\"Month\" placeholder=\"Date\">\r\n              <mat-datepicker-toggle matSuffix [for]=\"picker\"></mat-datepicker-toggle>\r\n              <mat-datepicker #picker (monthSelected)=\"monthSelected($event)\" startView=\"multi-year\"></mat-datepicker>\r\n            </mat-form-field>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </mat-dialog-content>\r\n    <mat-dialog-actions class=\"items-float-right\">\r\n      <hum-button *ngIf=\"!isAddUsageHourFormSubmitted\" [disabled]=\"!addUsageHourForm.valid\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]='true'></hum-button>\r\n      <hum-button *ngIf=\"isAddUsageHourFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n      <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n    </mat-dialog-actions>\r\n\r\n  </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/store/components/add-hours/add-hours.component.scss":
/*!*********************************************************************!*\
  !*** ./src/app/store/components/add-hours/add-hours.component.scss ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvYWRkLWhvdXJzL2FkZC1ob3Vycy5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/store/components/add-hours/add-hours.component.ts":
/*!*******************************************************************!*\
  !*** ./src/app/store/components/add-hours/add-hours.component.ts ***!
  \*******************************************************************/
/*! exports provided: AddHoursComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddHoursComponent", function() { return AddHoursComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
/* harmony import */ var _angular_material_core__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/material/core */ "./node_modules/@angular/material/esm5/core.es5.js");
/* harmony import */ var moment__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! moment */ "./node_modules/moment/moment.js");
/* harmony import */ var moment__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(moment__WEBPACK_IMPORTED_MODULE_7__);
var __extends = (undefined && undefined.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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








// tslint:disable-next-line:no-duplicate-imports

var moment = moment__WEBPACK_IMPORTED_MODULE_7___default.a || moment__WEBPACK_IMPORTED_MODULE_7__;
// See the Moment.js docs for the meaning of these formats:
// https://momentjs.com/docs/#/displaying/format/
var CustomDateAdapter = /** @class */ (function (_super) {
    __extends(CustomDateAdapter, _super);
    function CustomDateAdapter() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    CustomDateAdapter.prototype.format = function (date, displayFormat) {
        var formatString = 'MMMM YYYY';
        return moment(date).format(formatString);
    };
    return CustomDateAdapter;
}(_angular_material_core__WEBPACK_IMPORTED_MODULE_6__["NativeDateAdapter"]));
var AddHoursComponent = /** @class */ (function () {
    function AddHoursComponent(fb, purchaseService, toastr, dialogRef, data) {
        this.fb = fb;
        this.purchaseService = purchaseService;
        this.toastr = toastr;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isAddUsageHourFormSubmitted = false;
        this.maxDate = new Date();
        this.addUsageHourForm = this.fb.group({
            'GeneratorId': [data.generatorId, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Hours': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Month': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]]
        });
    }
    AddHoursComponent.prototype.ngOnInit = function () {
        this.maxDate.setDate(this.maxDate.getDate());
    };
    //#region "onCancelPopup"
    AddHoursComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close(false);
    };
    //#endregion
    AddHoursComponent.prototype.addHours = function () {
        var _this = this;
        if (this.addUsageHourForm.valid) {
            this.isAddUsageHourFormSubmitted = true;
            this.addUsageHourForm.value.Month = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_5__["StaticUtilities"].setLocalDate(this.addUsageHourForm.value.Month);
            this.purchaseService.addGeneratorUsageHours(this.addUsageHourForm.value)
                .subscribe(function (x) {
                if (x) {
                    _this.dialogRef.close(false);
                    _this.isAddUsageHourFormSubmitted = false;
                    _this.toastr.success('Added Successfully');
                }
                else {
                    _this.toastr.warning('Something went wrong');
                    _this.isAddUsageHourFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning('Something went wrong');
                _this.isAddUsageHourFormSubmitted = false;
            });
        }
    };
    AddHoursComponent.prototype.monthSelected = function (params) {
        this.addUsageHourForm.controls['Month'].setValue(params);
        this.picker.close();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])(_angular_material__WEBPACK_IMPORTED_MODULE_4__["MatDatepicker"]),
        __metadata("design:type", Object)
    ], AddHoursComponent.prototype, "picker", void 0);
    AddHoursComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-hours',
            template: __webpack_require__(/*! ./add-hours.component.html */ "./src/app/store/components/add-hours/add-hours.component.html"),
            providers: [
                {
                    provide: _angular_material_core__WEBPACK_IMPORTED_MODULE_6__["DateAdapter"], useClass: CustomDateAdapter
                }
            ],
            styles: [__webpack_require__(/*! ./add-hours.component.scss */ "./src/app/store/components/add-hours/add-hours.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_4__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__["PurchaseService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_4__["MatDialogRef"], Object])
    ], AddHoursComponent);
    return AddHoursComponent;
}());



/***/ }),

/***/ "./src/app/store/components/add-item-category/add-item-category.component.html":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/add-item-category/add-item-category.component.html ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<form [formGroup]=\"masterForm\" (ngSubmit)=\"submit()\">\r\n  <mat-card-title>\r\n    Add New Item Category\r\n  </mat-card-title>\r\n  <mat-card-subtitle>\r\n  </mat-card-subtitle>\r\n  <mat-card-content>\r\n    <mat-card-subtitle>What is the name of the Item Category?</mat-card-subtitle>\r\n    <mat-form-field class=\"full-width\">\r\n      <input matInput placeholder=\"Item Name\" formControlName=\"name\">\r\n    </mat-form-field>\r\n    <mat-card-subtitle>Please describe the Item Category</mat-card-subtitle>\r\n    <mat-form-field class=\"full-width\">\r\n      <textarea formControlName=\"description\" [mat-autosize]=\"true\" [cdkAutosizeMinRows]=\"3\" matInput\r\n        placeholder=\"Description\"></textarea>\r\n    </mat-form-field>\r\n    <div [hidden]=\"!data.IsTransportCategory\">\r\n      <mat-card-subtitle><b>This Item Category is for a Transport Master Inventory</b></mat-card-subtitle>\r\n      <mat-card-subtitle>Please select the Transport category for this Item Category.</mat-card-subtitle>\r\n\r\n      <mat-radio-group aria-label=\"Select an option\" formControlName=\"inventorytype\">\r\n        <mat-radio-button value=\"1\">Vehicle</mat-radio-button>\r\n        <mat-radio-button value=\"2\">Generator</mat-radio-button>\r\n      </mat-radio-group>\r\n    </div>\r\n  </mat-card-content>\r\n  <mat-card-actions>\r\n    <hum-button *ngIf=\"!isSaving\" [disabled]=\"masterForm.invalid\" [isSubmit]=\"true\" [type]=\"'save'\" [text]=\"'save'\"\r\n      [isSubmit]=\"true\"></hum-button>\r\n    <hum-button *ngIf=\"isSaving\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='cancel()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-card-actions>\r\n</form>"

/***/ }),

/***/ "./src/app/store/components/add-item-category/add-item-category.component.scss":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/add-item-category/add-item-category.component.scss ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "mat-dialog-container {\n  padding: 0px !important; }\n\n.full-width {\n  width: 100%; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9hZGQtaXRlbS1jYXRlZ29yeS9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxzdG9yZVxcY29tcG9uZW50c1xcYWRkLWl0ZW0tY2F0ZWdvcnlcXGFkZC1pdGVtLWNhdGVnb3J5LmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksdUJBQXVCLEVBQUE7O0FBRTNCO0VBQ0ksV0FBVyxFQUFBIiwiZmlsZSI6InNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9hZGQtaXRlbS1jYXRlZ29yeS9hZGQtaXRlbS1jYXRlZ29yeS5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIm1hdC1kaWFsb2ctY29udGFpbmVye1xyXG4gICAgcGFkZGluZzogMHB4ICFpbXBvcnRhbnQ7XHJcbn1cclxuLmZ1bGwtd2lkdGgge1xyXG4gICAgd2lkdGg6IDEwMCU7XHJcbiAgfSJdfQ== */"

/***/ }),

/***/ "./src/app/store/components/add-item-category/add-item-category.component.ts":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/add-item-category/add-item-category.component.ts ***!
  \***********************************************************************************/
/*! exports provided: AddItemCategoryComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddItemCategoryComponent", function() { return AddItemCategoryComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _models_store_configuration__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../models/store-configuration */ "./src/app/store/models/store-configuration.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _services_config_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../services/config.service */ "./src/app/store/services/config.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
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






var AddItemCategoryComponent = /** @class */ (function () {
    function AddItemCategoryComponent(fb, dialogRef, data, configService, toastr) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.data = data;
        this.configService = configService;
        this.toastr = toastr;
        this.masterInventoryCategory = {};
        this.isSaving = false;
    }
    AddItemCategoryComponent.prototype.ngOnInit = function () {
        this.createForm();
        if (this.data.ItemGroupId) {
            this.masterForm.controls.name.setValue(this.data.ItemGroupName);
            this.masterForm.controls.description.setValue(this.data.Description);
            this.masterForm.controls.inventorytype.setValue(this.data.ItemTypeCategory ? this.data.ItemTypeCategory.toString() : null);
        }
    };
    AddItemCategoryComponent.prototype.createForm = function () {
        this.masterForm = this.fb.group({
            name: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            description: [''],
            inventorytype: ['1']
        });
    };
    AddItemCategoryComponent.prototype.submit = function () {
        var _this = this;
        if (this.masterForm.valid) {
            this.isSaving = true;
            this.masterInventoryCategory.Description = this.masterForm.controls.description.value;
            this.masterInventoryCategory.InventoryId = this.data.InventoryId;
            this.masterInventoryCategory.ItemGroupCode = this.data.ItemGroupCode;
            this.masterInventoryCategory.ItemGroupName = this.masterForm.controls.name.value;
            this.masterInventoryCategory.ItemTypeCategory = this.data.IsTransportCategory ?
                Number(this.masterForm.controls.inventorytype.value) : null;
            if (this.data.ItemGroupId) {
                this.masterInventoryCategory.ItemGroupId = this.data.ItemGroupId;
                this.configService.EditItemGroup(this.masterInventoryCategory).subscribe(function () {
                    _this.isSaving = false;
                    _this.toastr.success("Group updated successfully");
                    _this.dialogRef.close(1);
                });
            }
            else {
                this.configService.AddItemGroup(this.masterInventoryCategory).subscribe(function () {
                    _this.isSaving = false;
                    _this.toastr.success("Group added successfully");
                    _this.dialogRef.close(1);
                });
            }
        }
        console.log(this.masterForm.value);
    };
    AddItemCategoryComponent.prototype.cancel = function () {
        this.dialogRef.close();
    };
    AddItemCategoryComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-item-category',
            template: __webpack_require__(/*! ./add-item-category.component.html */ "./src/app/store/components/add-item-category/add-item-category.component.html"),
            styles: [__webpack_require__(/*! ./add-item-category.component.scss */ "./src/app/store/components/add-item-category/add-item-category.component.scss")]
        }),
        __param(2, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_3__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material__WEBPACK_IMPORTED_MODULE_3__["MatDialogRef"],
            _models_store_configuration__WEBPACK_IMPORTED_MODULE_2__["MasterItemGroupModel"], _services_config_service__WEBPACK_IMPORTED_MODULE_4__["ConfigService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_5__["ToastrService"]])
    ], AddItemCategoryComponent);
    return AddItemCategoryComponent;
}());



/***/ }),

/***/ "./src/app/store/components/add-item/add-item.component.html":
/*!*******************************************************************!*\
  !*** ./src/app/store/components/add-item/add-item.component.html ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<form [formGroup]=\"masterForm\" (ngSubmit)=\"submit()\">\r\n  <mat-card-title>\r\n    Add New Item Configuration\r\n  </mat-card-title>\r\n  <mat-card-subtitle>\r\n  </mat-card-subtitle>\r\n  <mat-card-content>\r\n    <mat-card-subtitle>What is the name of the Item?</mat-card-subtitle>\r\n    <mat-form-field class=\"full-width\">\r\n      <input matInput placeholder=\"Item Name\" formControlName=\"name\">\r\n    </mat-form-field>\r\n    <mat-card-subtitle>Please describe the Item</mat-card-subtitle>\r\n    <mat-form-field class=\"full-width\">\r\n      <textarea formControlName=\"description\" [mat-autosize]=\"true\" [cdkAutosizeMinRows]=\"3\" matInput\r\n        placeholder=\"Description\"></textarea>\r\n    </mat-form-field>\r\n    <div *ngIf=\"data.isGenerator!=null\">\r\n      <mat-card-subtitle *ngIf=\"data.isGenerator\"><b>This Item Configuration is for a Generator Item Category.</b></mat-card-subtitle>\r\n      <mat-card-subtitle *ngIf=\"data.isGenerator\">Please select the Generator Item Type for this Item.</mat-card-subtitle>\r\n      <mat-card-subtitle *ngIf=\"!data.isGenerator\"><b>This Item Configuration is for a Vehicle Item Category.</b></mat-card-subtitle>\r\n      <mat-card-subtitle *ngIf=\"!data.isGenerator\">Please select the Vehicle Item Type for this Item.</mat-card-subtitle>\r\n      <lib-hum-dropdown [validation]=\"masterForm.controls['itemtypecategory'].hasError('required')\"\r\n        [options]=\"itemsTypes$\" formControlName=\"itemtypecategory\" [placeHolder]=\"'Item Type'\">\r\n      </lib-hum-dropdown>\r\n    </div>\r\n    <div>\r\n      <mat-card-subtitle>This is the Unit Type that will be selected by default for Purchases of this item.</mat-card-subtitle>\r\n      <lib-hum-dropdown [validation]=\"masterForm.controls['defaultunittype'].hasError('required')\"\r\n      [options]=\"unitTypes$\" formControlName=\"defaultunittype\" [placeHolder]=\"'Default Unit Type'\">\r\n    </lib-hum-dropdown>\r\n    </div>\r\n  </mat-card-content>\r\n  <mat-card-actions>\r\n    <hum-button *ngIf=\"!isSaving\" [disabled]=\"masterForm.invalid\" [isSubmit]=\"true\" [type]=\"'save'\" [text]=\"'save'\"\r\n      [isSubmit]=\"true\"></hum-button>\r\n    <hum-button *ngIf=\"isSaving\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='cancel()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-card-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/store/components/add-item/add-item.component.scss":
/*!*******************************************************************!*\
  !*** ./src/app/store/components/add-item/add-item.component.scss ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "mat-dialog-container {\n  padding: 0px !important; }\n\n.full-width {\n  width: 100%; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9hZGQtaXRlbS9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxzdG9yZVxcY29tcG9uZW50c1xcYWRkLWl0ZW1cXGFkZC1pdGVtLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksdUJBQXVCLEVBQUE7O0FBRTNCO0VBQ0ksV0FBVyxFQUFBIiwiZmlsZSI6InNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9hZGQtaXRlbS9hZGQtaXRlbS5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIm1hdC1kaWFsb2ctY29udGFpbmVye1xyXG4gICAgcGFkZGluZzogMHB4ICFpbXBvcnRhbnQ7XHJcbn1cclxuLmZ1bGwtd2lkdGgge1xyXG4gICAgd2lkdGg6IDEwMCU7XHJcbiAgfSJdfQ== */"

/***/ }),

/***/ "./src/app/store/components/add-item/add-item.component.ts":
/*!*****************************************************************!*\
  !*** ./src/app/store/components/add-item/add-item.component.ts ***!
  \*****************************************************************/
/*! exports provided: AddItemComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddItemComponent", function() { return AddItemComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _services_config_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../services/config.service */ "./src/app/store/services/config.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _models_store_configuration__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../models/store-configuration */ "./src/app/store/models/store-configuration.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
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








var AddItemComponent = /** @class */ (function () {
    function AddItemComponent(fb, dialogRef, data, configService, toastr, purchaseService) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.data = data;
        this.configService = configService;
        this.toastr = toastr;
        this.purchaseService = purchaseService;
        this.inventoryItem = {};
        this.items = [
            { value: 1, name: 'Vehicle' },
            { value: 2, name: 'Generator' },
            { value: 3, name: 'Mobil oil' },
            { value: 4, name: 'Service & Maintenance' },
            { value: 5, name: 'Spare parts' },
            { value: 6, name: 'Fuel' }
        ];
        this.isSaving = false;
    }
    AddItemComponent.prototype.ngOnInit = function () {
        this.createForm();
        switch (this.data.AssetType) {
            case 1:
                var ids1_1 = [6, 3, 4];
                this.itemsTypes$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(this.items.filter(function (x) { return ids1_1.includes(x.value); }));
                break;
            case 2:
                var ids2_1 = [5, 1, 2];
                this.itemsTypes$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(this.items.filter(function (x) { return ids2_1.includes(x.value); }));
                break;
            case 3:
                var ids3_1 = [1, 2];
                this.itemsTypes$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(this.items.filter(function (x) { return ids3_1.includes(x.value); }));
                break;
            default:
                break;
        }
        if (this.data.ItemId) {
            this.masterForm.controls.name.setValue(this.data.ItemName);
            this.masterForm.controls.description.setValue(this.data.Description);
            this.masterForm.controls.itemtypecategory.setValue(this.data.ItemTypeCategory ? this.data.ItemTypeCategory : null);
            this.masterForm.controls.defaultunittype.setValue(this.data.DefaultUnitType);
        }
        this.getAllUnitTypeDetails();
    };
    AddItemComponent.prototype.createForm = function () {
        this.masterForm = this.fb.group({
            name: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            description: [''],
            itemtypecategory: [''],
            defaultunittype: [null, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]
        });
    };
    AddItemComponent.prototype.submit = function () {
        var _this = this;
        if (this.masterForm.valid) {
            this.isSaving = true;
            this.inventoryItem.Description = this.masterForm.controls.description.value;
            this.inventoryItem.ItemName = this.masterForm.controls.name.value;
            this.inventoryItem.ItemCode = this.data.ItemCode;
            this.inventoryItem.ItemGroupId = this.data.ItemGroupId;
            this.inventoryItem.ItemType = null;
            this.inventoryItem.ItemInventory = this.data.ItemInventory;
            this.inventoryItem.ItemTypeCategory = this.masterForm.controls.itemtypecategory.value;
            this.inventoryItem.DefaultUnitType = this.masterForm.controls.defaultunittype.value;
            if (this.data.ItemId) {
                this.inventoryItem.ItemId = this.data.ItemId;
                this.configService.EditItem(this.inventoryItem).subscribe(function () {
                    _this.isSaving = false;
                    _this.toastr.success('Item updated successfully');
                    _this.dialogRef.close(1);
                });
            }
            else {
                this.configService.AddItem(this.inventoryItem).subscribe(function () {
                    _this.isSaving = false;
                    _this.toastr.success('Item added successfully');
                    _this.dialogRef.close(1);
                });
            }
        }
    };
    AddItemComponent.prototype.cancel = function () {
        this.dialogRef.close();
    };
    AddItemComponent.prototype.getAllUnitTypeDetails = function () {
        var _this = this;
        this.purchaseService.getAllUnitTypeDetails().subscribe(function (response) {
            _this.unitTypes$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(response.data.PurchaseUnitTypeList.map(function (y) {
                return {
                    name: y.UnitTypeName,
                    value: y.UnitTypeId
                };
            }));
        });
    };
    AddItemComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-item',
            template: __webpack_require__(/*! ./add-item.component.html */ "./src/app/store/components/add-item/add-item.component.html"),
            styles: [__webpack_require__(/*! ./add-item.component.scss */ "./src/app/store/components/add-item/add-item.component.scss")]
        }),
        __param(2, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            _models_store_configuration__WEBPACK_IMPORTED_MODULE_5__["MasterInventoryItemModel"],
            _services_config_service__WEBPACK_IMPORTED_MODULE_3__["ConfigService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            _services_purchase_service__WEBPACK_IMPORTED_MODULE_7__["PurchaseService"]])
    ], AddItemComponent);
    return AddItemComponent;
}());



/***/ }),

/***/ "./src/app/store/components/add-master-inventory/add-master-inventory.component.html":
/*!*******************************************************************************************!*\
  !*** ./src/app/store/components/add-master-inventory/add-master-inventory.component.html ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<form [formGroup]=\"masterForm\" (ngSubmit)=\"submit()\">\r\n  <mat-card-title>\r\n    Add New Master Inventory\r\n  </mat-card-title>\r\n  <mat-card-subtitle>\r\n  </mat-card-subtitle>\r\n  <mat-card-content>\r\n    <mat-card-subtitle>What is the name of the Master Inventory?</mat-card-subtitle>\r\n    <mat-form-field class=\"full-width\">\r\n      <input matInput placeholder=\"Item Name\" formControlName=\"name\">\r\n    </mat-form-field>\r\n    <mat-card-subtitle>Please describe the Master Inventory</mat-card-subtitle>\r\n    <mat-form-field class=\"full-width\">\r\n      <textarea formControlName=\"description\" [mat-autosize]=\"true\" [cdkAutosizeMinRows]=\"3\" matInput\r\n        placeholder=\"Description\"></textarea>\r\n    </mat-form-field>\r\n    <lib-hum-dropdown [validation]=\"masterForm.controls['accountId'].hasError('required')\" [options]=\"accounts$\"\r\n      formControlName='accountId' [placeHolder]=\"'Account'\"></lib-hum-dropdown>\r\n  </mat-card-content>\r\n\r\n  <mat-card-subtitle>Is this a Transport related Master Inventory?</mat-card-subtitle>\r\n\r\n  <mat-radio-group aria-label=\"Select an option\" formControlName=\"istransport\">\r\n    <mat-radio-button value=\"0\">Yes</mat-radio-button>\r\n    <mat-radio-button value=\"1\">No</mat-radio-button>\r\n  </mat-radio-group>\r\n\r\n  <mat-card-actions>\r\n    <hum-button *ngIf=\"!isSaving\" [disabled]=\"masterForm.invalid\" [isSubmit]=\"true\" [type]=\"'save'\" [text]=\"'save'\"\r\n      [isSubmit]=\"true\"></hum-button>\r\n    <hum-button *ngIf=\"isSaving\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='cancel()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-card-actions>\r\n</form>"

/***/ }),

/***/ "./src/app/store/components/add-master-inventory/add-master-inventory.component.scss":
/*!*******************************************************************************************!*\
  !*** ./src/app/store/components/add-master-inventory/add-master-inventory.component.scss ***!
  \*******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "mat-dialog-container {\n  padding: 0px !important; }\n\n.full-width {\n  width: 100%; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9hZGQtbWFzdGVyLWludmVudG9yeS9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxzdG9yZVxcY29tcG9uZW50c1xcYWRkLW1hc3Rlci1pbnZlbnRvcnlcXGFkZC1tYXN0ZXItaW52ZW50b3J5LmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksdUJBQXVCLEVBQUE7O0FBRTNCO0VBQ0ksV0FBVyxFQUFBIiwiZmlsZSI6InNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9hZGQtbWFzdGVyLWludmVudG9yeS9hZGQtbWFzdGVyLWludmVudG9yeS5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIm1hdC1kaWFsb2ctY29udGFpbmVye1xyXG4gICAgcGFkZGluZzogMHB4ICFpbXBvcnRhbnQ7XHJcbn1cclxuLmZ1bGwtd2lkdGgge1xyXG4gICAgd2lkdGg6IDEwMCU7XHJcbiAgfSJdfQ== */"

/***/ }),

/***/ "./src/app/store/components/add-master-inventory/add-master-inventory.component.ts":
/*!*****************************************************************************************!*\
  !*** ./src/app/store/components/add-master-inventory/add-master-inventory.component.ts ***!
  \*****************************************************************************************/
/*! exports provided: AddMasterInventoryComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddMasterInventoryComponent", function() { return AddMasterInventoryComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _models_store_configuration__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../models/store-configuration */ "./src/app/store/models/store-configuration.ts");
/* harmony import */ var _services_config_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../services/config.service */ "./src/app/store/services/config.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
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








var AddMasterInventoryComponent = /** @class */ (function () {
    function AddMasterInventoryComponent(fb, dialogRef, data, configService, toastr) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.data = data;
        this.configService = configService;
        this.toastr = toastr;
        this.masterInventory = {};
        this.isSaving = false;
    }
    AddMasterInventoryComponent.prototype.ngOnInit = function () {
        this.createForm();
        this.getAccountCodes();
        if (this.data.InventoryId) {
            this.masterForm.controls.name.setValue(this.data.InventoryName);
            this.masterForm.controls.description.setValue(this.data.InventoryDescription);
            this.masterForm.controls.accountId.setValue(this.data.InventoryDebitAccount);
            if (this.data.IsTransportCategory == true) {
                this.masterForm.controls.istransport.setValue("0");
            }
            else {
                this.masterForm.controls.istransport.setValue("1");
            }
        }
        else {
        }
    };
    AddMasterInventoryComponent.prototype.createForm = function () {
        this.masterForm = this.fb.group({
            name: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            description: [''],
            accountId: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            istransport: [false]
        });
    };
    AddMasterInventoryComponent.prototype.getAccountCodes = function () {
        var _this = this;
        this.configService.getAllAccounts().subscribe(function (res) {
            _this.accounts$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(res.data.AccountDetailList).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_7__["map"])(function (x) { return x.map(function (y) {
                return {
                    name: y.AccountName,
                    value: y.AccountCode
                };
            }); }));
        });
    };
    AddMasterInventoryComponent.prototype.cancel = function (res) {
        this.dialogRef.close(res);
    };
    AddMasterInventoryComponent.prototype.submit = function () {
        var _this = this;
        if (this.masterForm.valid) {
            this.isSaving = true;
            this.masterInventory.AssetType = this.data.AssetType;
            this.masterInventory.InventoryCode = this.data.InventoryCode;
            this.masterInventory.InventoryName = this.masterForm.value.name;
            this.masterInventory.InventoryDescription = this.masterForm.value.description;
            this.masterInventory.InventoryDebitAccount = this.masterForm.value.accountId;
            this.masterInventory.InventoryCreditAccount = null;
            this.masterInventory.IsTransportCategory = (this.masterForm.value.istransport === "0") ? true : false;
            if (this.data.InventoryId) {
                this.masterInventory.InventoryId = this.data.InventoryId;
                this.configService.EditMasterInventory(this.masterInventory).subscribe(function () {
                    _this.isSaving = false;
                    _this.toastr.success('Inventory Updated');
                    _this.cancel(1);
                });
            }
            else {
                this.configService.AddMasterInventory(this.masterInventory).subscribe(function () {
                    _this.isSaving = false;
                    _this.toastr.success('Inventory added');
                    _this.cancel(1);
                });
            }
        }
    };
    AddMasterInventoryComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-master-inventory',
            template: __webpack_require__(/*! ./add-master-inventory.component.html */ "./src/app/store/components/add-master-inventory/add-master-inventory.component.html"),
            styles: [__webpack_require__(/*! ./add-master-inventory.component.scss */ "./src/app/store/components/add-master-inventory/add-master-inventory.component.scss")]
        }),
        __param(2, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            _models_store_configuration__WEBPACK_IMPORTED_MODULE_3__["MasterInventoryModel"], _services_config_service__WEBPACK_IMPORTED_MODULE_4__["ConfigService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_5__["ToastrService"]])
    ], AddMasterInventoryComponent);
    return AddMasterInventoryComponent;
}());



/***/ }),

/***/ "./src/app/store/components/add-milage/add-milage.component.html":
/*!***********************************************************************!*\
  !*** ./src/app/store/components/add-milage/add-milage.component.html ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>Vehicle Mileage Form</h1>\r\n  <div mat-dialog-subtitle>Please enter number of Kilometers the vehicle has been used.</div>\r\n  <div mat-dialog-subtitle><b>Note:</b> User may provide a negative value if the vehicle's total incurred mileage needs to be\r\n    reduced for some reason..</div>\r\n  <br/>\r\n  <form [formGroup]=\"mileageForm\" (ngSubmit)=\"addMilage()\">\r\n\r\n\r\n    <mat-dialog-content>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-3\">\r\n            <mat-form-field class=\"example-full-width\">\r\n              <input matInput placeholder=\"Mileage (KM)\" formControlName=\"Mileage\">\r\n            </mat-form-field>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <mat-form-field>\r\n              <input matInput [matDatepicker]=\"picker\" [max]=\"maxDate\" placeholder=\"Choose a month\" formControlName=\"Month\">\r\n              <mat-datepicker-toggle matSuffix [for]=\"picker\"></mat-datepicker-toggle>\r\n              <mat-datepicker #picker (monthSelected)=\"monthSelected($event)\" startView=\"multi-year\"></mat-datepicker>\r\n            </mat-form-field>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </mat-dialog-content>\r\n    <mat-dialog-actions class=\"items-float-right\">\r\n      <hum-button *ngIf=\"!isAddMileageFormSubmitted\" [disabled]=\"!mileageForm.valid\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]='true'></hum-button>\r\n      <hum-button *ngIf=\"isAddMileageFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n      <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n    </mat-dialog-actions>\r\n\r\n  </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/store/components/add-milage/add-milage.component.scss":
/*!***********************************************************************!*\
  !*** ./src/app/store/components/add-milage/add-milage.component.scss ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvYWRkLW1pbGFnZS9hZGQtbWlsYWdlLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/store/components/add-milage/add-milage.component.ts":
/*!*********************************************************************!*\
  !*** ./src/app/store/components/add-milage/add-milage.component.ts ***!
  \*********************************************************************/
/*! exports provided: AddMilageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddMilageComponent", function() { return AddMilageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_material_core__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material/core */ "./node_modules/@angular/material/esm5/core.es5.js");
/* harmony import */ var moment__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! moment */ "./node_modules/moment/moment.js");
/* harmony import */ var moment__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(moment__WEBPACK_IMPORTED_MODULE_9__);
var __extends = (undefined && undefined.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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










// tslint:disable-next-line:no-duplicate-imports

var moment = moment__WEBPACK_IMPORTED_MODULE_9___default.a || moment__WEBPACK_IMPORTED_MODULE_9__;
// See the Moment.js docs for the meaning of these formats:
// https://momentjs.com/docs/#/displaying/format/
var CustomDateAdapter = /** @class */ (function (_super) {
    __extends(CustomDateAdapter, _super);
    function CustomDateAdapter() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    CustomDateAdapter.prototype.format = function (date, displayFormat) {
        var formatString = 'MMMM YYYY';
        return moment(date).format(formatString);
    };
    return CustomDateAdapter;
}(_angular_material_core__WEBPACK_IMPORTED_MODULE_8__["NativeDateAdapter"]));
var AddMilageComponent = /** @class */ (function () {
    function AddMilageComponent(fb, purchaseService, commonLoader, toastr, dialogRef, data) {
        this.fb = fb;
        this.purchaseService = purchaseService;
        this.commonLoader = commonLoader;
        this.toastr = toastr;
        this.dialogRef = dialogRef;
        this.data = data;
        this.isAddMileageFormSubmitted = false;
        this.maxDate = new Date();
        this.mileageForm = this.fb.group({
            'VehicleId': [data.vehicleId, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Mileage': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Month': [moment(), [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]]
        });
        this.maxDate.setDate(this.maxDate.getDate());
    }
    AddMilageComponent.prototype.ngOnInit = function () {
    };
    //#region "onCancelPopup"
    AddMilageComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close(false);
    };
    //#endregion
    AddMilageComponent.prototype.addMilage = function () {
        var _this = this;
        if (this.mileageForm.valid) {
            this.mileageForm.value.Month = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_6__["StaticUtilities"].setLocalDate(this.mileageForm.value.Month);
            this.isAddMileageFormSubmitted = true;
            this.purchaseService.addVehicleMileage(this.mileageForm.value)
                .subscribe(function (x) {
                if (x) {
                    _this.dialogRef.close(false);
                    _this.isAddMileageFormSubmitted = false;
                    _this.toastr.success('Added Successfully');
                }
                else {
                    _this.toastr.warning('Something went wrong');
                    _this.isAddMileageFormSubmitted = false;
                }
            }, function (error) {
                _this.toastr.warning(error);
                _this.isAddMileageFormSubmitted = false;
            });
        }
    };
    AddMilageComponent.prototype.monthSelected = function (params) {
        this.mileageForm.controls['Month'].setValue(params);
        this.picker.close();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])(_angular_material__WEBPACK_IMPORTED_MODULE_7__["MatDatepicker"]),
        __metadata("design:type", Object)
    ], AddMilageComponent.prototype, "picker", void 0);
    AddMilageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-milage',
            template: __webpack_require__(/*! ./add-milage.component.html */ "./src/app/store/components/add-milage/add-milage.component.html"),
            providers: [
                {
                    provide: _angular_material_core__WEBPACK_IMPORTED_MODULE_8__["DateAdapter"], useClass: CustomDateAdapter
                }
            ],
            styles: [__webpack_require__(/*! ./add-milage.component.scss */ "./src/app/store/components/add-milage/add-milage.component.scss")]
        }),
        __param(5, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_5__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__["PurchaseService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__["CommonLoaderService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            _angular_material_dialog__WEBPACK_IMPORTED_MODULE_5__["MatDialogRef"], Object])
    ], AddMilageComponent);
    return AddMilageComponent;
}());



/***/ }),

/***/ "./src/app/store/components/add-procurements/add-procurements.component.html":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/add-procurements/add-procurements.component.html ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\"><i (click)=\"goBack()\" class=\"fas fa-arrow-left\"></i> &nbsp; &nbsp; Procurement Details Form &nbsp; &nbsp;\r\n    <hum-button form=\"ngForm\" [disabled]=\"!addProcurementForm.valid\" [type]=\"'save'\" [text]=\"'SAVE'\" (click)=\"saveProcurement()\"></hum-button>\r\n  </span>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n<mat-card humAddScroll [height]=\"150\">\r\n  <div class=\"alert alert-warning\" *ngIf=\"addProcurementForm.controls['IssuedQuantity'].hasError('max')\" role=\"alert\">\r\n    <p class=\"txt-center-align\">{{maxProcurementMessage}}</p>\r\n  </div>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-6\">\r\n        <form id=\"ngForm\" #documentEditForm=\"ngForm\" [formGroup]= \"addProcurementForm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-sm-12\">\r\n              <b>When was this Procurement issued?</b>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <mat-form-field>\r\n                <input matInput [matDatepicker]=\"IssueDate\" formControlName='IssueDate' placeholder=\"Issue Date\">\r\n                <mat-datepicker-toggle matSuffix [for]=\"IssueDate\"></mat-datepicker-toggle>\r\n                <mat-datepicker #IssueDate></mat-datepicker>\r\n              </mat-form-field>\r\n            </div>\r\n            <br>\r\n            <div class=\"col-sm-12\">\r\n              <b>Which office was this Procurement issued to?</b>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <lib-hum-dropdown [validation]=\"addProcurementForm.controls['OfficeId'].hasError('required')\"\r\n              [options]=\"officeList$\" formControlName='OfficeId' [placeHolder]=\"'Office'\"\r\n              (change)=\"getAllEmployeesByOfficeId($event)\"></lib-hum-dropdown>\r\n            </div>\r\n            <br>\r\n            <div class=\"col-sm-12\">\r\n              <b>Who was this Procurement issued to?</b>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <lib-hum-dropdown [validation]=\"addProcurementForm.controls['IssuedToEmployeeId'].hasError('required')\"\r\n              [options]=\"employeeList$\" formControlName='IssuedToEmployeeId' [placeHolder]=\"'Issued To Employee'\"\r\n              (change)=\"getItemGroupSelectedValue($event)\"></lib-hum-dropdown>\r\n            </div>\r\n            <br>\r\n            <div class=\"col-sm-12\">\r\n              <b>What quantity was originally issued?</b>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <mat-form-field>\r\n                <input matInput min=\"0\" type=\"number\"\r\n                  oninput=\"validity.valid||(value='');\" id=\"IssuedQuantity\" formControlName='IssuedQuantity'\r\n                  placeholder=\"Issued Quantity\">\r\n                <mat-error *ngIf=\"addProcurementForm.controls['IssuedQuantity'].hasError('required')\">required</mat-error>\r\n                <mat-error *ngIf=\"addProcurementForm.controls['IssuedQuantity'].hasError('max')\">Max quantity allowed is {{maxQuantity}}</mat-error>\r\n              </mat-form-field>\r\n\r\n            </div>\r\n            <br>\r\n            <div class=\"col-sm-12\">\r\n              <b>What condition was the Item(s) at the time of issue?</b>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <lib-hum-dropdown [validation]=\"addProcurementForm.controls['StatusId'].hasError('required')\"\r\n              [options]=\"statusList$\" formControlName='StatusId' [placeHolder]=\"'Status At Time Of Issue'\"\r\n              ></lib-hum-dropdown>\r\n            </div>\r\n            <br>\r\n            <div class=\"col-sm-12\">\r\n              <b>Which voucher is this issue tracked?</b>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <lib-hum-dropdown [validation]=\"addProcurementForm.controls['VoucherNo'].hasError('required')\"\r\n              [options]=\"voucherList$\" formControlName='VoucherNo' [placeHolder]=\"'Voucher No'\"\r\n              ></lib-hum-dropdown>\r\n            </div>\r\n            <br>\r\n            <div class=\"col-sm-12\">\r\n              <b>Which project is this Procurement issued for?</b>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <lib-hum-dropdown [validation]=\"addProcurementForm.controls['ProjectId'].hasError('required')\"\r\n              [options]=\"projects$\" formControlName='ProjectId' [placeHolder]=\"'Project'\"\r\n              ></lib-hum-dropdown>\r\n            </div>\r\n            <br>\r\n            <div class=\"col-sm-12\">\r\n              <b>Where is this Procurement issued to?</b>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <lib-hum-dropdown [validation]=\"addProcurementForm.controls['StoreSourceId'].hasError('required')\"\r\n              [options]=\"storeSource$\" formControlName='StoreSourceId' [placeHolder]=\"'Issued To Location'\"\r\n              ></lib-hum-dropdown>\r\n            </div>\r\n            <br>\r\n            <div class=\"col-sm-12\">\r\n              <b>Are the Procured items to be Returned?</b>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <mat-checkbox formControlName='MustReturn'>Must Return</mat-checkbox>\r\n            </div>\r\n          </div>\r\n      </form>\r\n      </div>\r\n    </div>\r\n\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/store/components/add-procurements/add-procurements.component.scss":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/add-procurements/add-procurements.component.scss ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvYWRkLXByb2N1cmVtZW50cy9hZGQtcHJvY3VyZW1lbnRzLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/store/components/add-procurements/add-procurements.component.ts":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/add-procurements/add-procurements.component.ts ***!
  \*********************************************************************************/
/*! exports provided: AddProcurementsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddProcurementsComponent", function() { return AddProcurementsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var rxjs_internal_observable_forkJoin__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/observable/forkJoin */ "./node_modules/rxjs/internal/observable/forkJoin.js");
/* harmony import */ var rxjs_internal_observable_forkJoin__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_forkJoin__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};










var AddProcurementsComponent = /** @class */ (function () {
    function AddProcurementsComponent(fb, purchaseService, commonLoader, toastr, route, router) {
        var _this = this;
        this.fb = fb;
        this.purchaseService = purchaseService;
        this.commonLoader = commonLoader;
        this.toastr = toastr;
        this.route = route;
        this.router = router;
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_2__["ReplaySubject"](1);
        this.originalQuantityOfPurchase = 0;
        this.procuredQuantityOfPurchase = 0;
        this.originalProcuredQuantity = 0;
        this.currentQuantityOfPurchase = 0;
        this.showMaxProcurementMessage = false;
        this.originalIssuedQuantity = 0;
        this.procurementPageTitle = 'Add Procurement';
        this.maxProcurementMessage = 'Issued quantity exceeds purchased quantity. Either decrease the quantity, return the issued item or create a new Purchase for the item';
        this.route.queryParams.subscribe(function (params) {
            _this.maxQuantity = +params['quantity'];
            _this.purchaseId = +params['purchaseId'];
            _this.itemId = +params['itemId'];
        });
        this.route.params.subscribe(function (params) {
            _this.procurementId = +params['id'];
        });
    }
    AddProcurementsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.initForm();
        // if (this.data.procurement) {
        //   this.addProcurementForm.patchValue({
        //     'ProcurementId': this.data.procurement.OrderId,
        //     'IssuedToEmployeeId': this.data.procurement.EmployeeId,
        //     'MustReturn': this.data.procurement.MustReturn,
        //     'IssueDate': this.data.procurement.IssueDate,
        //     'IssuedQuantity': this.data.procurement.ProcuredAmount,
        //     'ProjectId': this.data.procurement.ProjectId,
        //     'StatusId': this.data.procurement.StatusId,
        //     'StoreSourceId': +this.data.procurement.LocationId,
        //     'Returned': this.data.procurement.Returned
        //   });
        //   this.procurementPageTitle = 'Edit Procurement';
        // }
        // this.purchaseId = this.data.value;
        // this.officeId = this.data.officeId;
        Object(rxjs_internal_observable_forkJoin__WEBPACK_IMPORTED_MODULE_4__["forkJoin"])([
            // this.getAllInventoryTypes(),
            this.getAllProjects(),
            this.getStoreLocations(),
            this.getAllStatusAtTimeOfIssue(),
            this.getAllVouchers(),
            this.getAllOffices()
        ])
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            // this.subscribeInventoryTypes(result[0]);
            _this.subscribeAllProjects(result[0]);
            _this.subscribeStoreLocations(result[1]);
            _this.subscribeAllStatusAtTimeOfIssue(result[2]);
            _this.subscribeAllVouchersResult(result[3]);
            _this.subscribeAllOfficesResult(result[4]);
        });
        // this.getItemDetailByPurchaseId(this.purchaseId);
        this.route.url.subscribe(function (value) {
            if (value[1].path === 'edit-procurement') {
                _this.getProcurementDetailsByProcurementId();
            }
        });
    };
    AddProcurementsComponent.prototype.initForm = function () {
        this.addProcurementForm = this.fb.group({
            'ProcurementId': [null],
            'PurchaseId': [{ value: null, disabled: true }, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'IssuedQuantity': [0, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(1), _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].max(this.maxQuantity)]],
            'IssuedToEmployeeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'IssueDate': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'ProjectId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'StoreSourceId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'StatusId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'MustReturn': [false],
            'EmployeeName': [null],
            'Returned': [false],
            'VoucherNo': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            'OfficeId': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]
        });
    };
    AddProcurementsComponent.prototype.getAllInventoryTypes = function () {
        return this.purchaseService.getAllInventoryTypeList();
    };
    AddProcurementsComponent.prototype.getAllProjects = function () {
        return this.purchaseService.getAllProjectList();
    };
    AddProcurementsComponent.prototype.getAllEmployeesByOfficeId = function (officeId) {
        var _this = this;
        return this.purchaseService.getEmployeesByOfficeId(officeId).subscribe(function (response) {
            _this.employeeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_7__["of"])(response.data.map(function (y) {
                return {
                    name: y.EmployeeCode + '-' + y.EmployeeName,
                    value: y.EmployeeId
                };
            }));
        });
    };
    AddProcurementsComponent.prototype.getStoreLocations = function () {
        return this.purchaseService.getAllStoreSource();
    };
    AddProcurementsComponent.prototype.getAllStatusAtTimeOfIssue = function () {
        return this.purchaseService.getAllStatusAtTimeOfIssue();
    };
    AddProcurementsComponent.prototype.getAllVouchers = function () {
        return this.purchaseService.getAllVouchers();
    };
    AddProcurementsComponent.prototype.getAllOffices = function () {
        return this.purchaseService.getAllOfficeList();
    };
    // subscribeInventoryTypes(response: any) {
    //   this.inventoryType$ = of(response.Result.map(y => {
    //     return {
    //       value: y.Id,
    //       name: y.InventoryName
    //     };
    //   }));
    // }
    AddProcurementsComponent.prototype.subscribeAllProjects = function (response) {
        this.projects$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_7__["of"])(response.data.ProjectDetailModel.map(function (y) {
            return {
                value: y.ProjectId,
                name: y.ProjectCode + '-' + y.ProjectName
            };
        }));
    };
    AddProcurementsComponent.prototype.subscribeStoreLocations = function (response) {
        this.storeSource$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_7__["of"])(response.data.SourceCodeDatalist.map(function (y) {
            return {
                value: y.SourceCodeId,
                name: y.Code + '-' + y.Description
            };
        }));
    };
    AddProcurementsComponent.prototype.subscribeAllStatusAtTimeOfIssue = function (response) {
        this.statusList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_7__["of"])(response.data.StatusAtTimeOfIssueList.map(function (y) {
            return {
                value: y.StatusAtTimeOfIssueId,
                name: y.StatusName
            };
        }));
    };
    // subscribeInventoriesByInventoryTypeId(response: any) {
    //   this.storeInventory$ = of(response.data.map(y => {
    //     return {
    //       name: y.InventoryCode + '-' + y.InventoryName,
    //       value: y.InventoryId
    //     };
    //   }));
    // }
    // subscribeAllStoreItemGroups(response: any) {
    //   this.storeItemGroups$ = of(response.data.map(y => {
    //     return {
    //       name: y.ItemGroupCode + '-' + y.ItemGroupName,
    //       value: y.ItemGroupId
    //     };
    //   }));
    // }
    // subscribeAllStoreItemsByGroupId(response: any) {
    //   this.storeItems$ = of(response.data.map(y => {
    //     return {
    //       name: y.ItemCode + '-' + y.ItemName,
    //       value: y.ItemId
    //     };
    //   }));
    // }
    // subscribeAllEmployeesByOfficeId(response: any) {
    //   this.employeeList$ = of(response.data.map(y => {
    //     return {
    //       name: y.EmployeeCode + '-' + y.EmployeeName,
    //       value: y.EmployeeId
    //     };
    //     }));
    // }
    AddProcurementsComponent.prototype.subscribeAllVouchersResult = function (response) {
        this.voucherList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_7__["of"])(response.Vouchers.map(function (y) {
            return {
                name: y.ReferenceNo,
                value: y.VoucherNo
            };
        }));
    };
    AddProcurementsComponent.prototype.subscribeAllOfficesResult = function (response) {
        this.officeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_7__["of"])(response.data.OfficeDetailsList.map(function (y) {
            return {
                value: y.OfficeId,
                name: y.OfficeCode + '-' + y.OfficeName
            };
        }));
    };
    // subscribePurchaseListByItemId(response: any) {
    //   this.commonLoader.hideLoader();
    //   if (response.statusCode === 200) {
    //     const index = response.data.findIndex(x => x.PurchaseId === this.purchaseId);
    //     if (index !== -1) {
    //       this.originalQuantityOfPurchase = response.data[index].Quantity;
    //       this.procuredQuantityOfPurchase = this.originalProcuredQuantity = response.data[index].ItemsIssuedCount;
    //       this.itemsToBeProcuredCount = this.currentQuantityOfPurchase  = this.originalQuantityOfPurchase  - this.procuredQuantityOfPurchase;
    //       this.originalIssuedQuantity = this.addProcurementForm.get('IssuedQuantity').value;
    //        this.addProcurementForm.get('IssuedQuantity').setValidators(Validators.max(this.itemsToBeProcuredCount));
    //      }
    //     this.purchases$ = of(response.data.map(y => {
    //       return {
    //         name: y.PurchaseCode,
    //         value: y.PurchaseId
    //       };
    //     }));
    //   } else {
    //     this.toastr.warning(response.message);
    //   }
    // }
    // validateMaxProcurement() {
    //   const currentQuantity = this.addProcurementForm.get('IssuedQuantity').value;
    //   const itemsAvailable =  (this.originalIssuedQuantity - currentQuantity + this.itemsToBeProcuredCount);
    //   this.showMaxProcurementMessage = (itemsAvailable >= 0) ? false : true;
    //   this.addProcurementForm.get('IssuedQuantity').setValidators(Validators.max(currentQuantity + itemsAvailable));
    //   this.currentQuantityOfPurchase = itemsAvailable;
    //   this.procuredQuantityOfPurchase = (this.originalProcuredQuantity + (currentQuantity - this.originalIssuedQuantity));
    // }
    AddProcurementsComponent.prototype.getInventoriesByInventoryTypeId = function (inventoryTypeId) {
        return this.purchaseService
            .getInventoriesByInventoryTypeId(inventoryTypeId);
    };
    AddProcurementsComponent.prototype.getMasterInventorySelectedValue = function (event) {
        this.getAllStoreItemGroups(event);
    };
    AddProcurementsComponent.prototype.getAllStoreItemGroups = function (inventoryId) {
        return this.purchaseService
            .getItemGroupByInventoryId(inventoryId);
    };
    AddProcurementsComponent.prototype.getItemGroupSelectedValue = function (event) {
        this.getAllStoreItemsByGroupId(event);
    };
    AddProcurementsComponent.prototype.getAllStoreItemsByGroupId = function (groupId) {
        return this.purchaseService
            .getItemsByItemGroupId(groupId);
    };
    AddProcurementsComponent.prototype.getPurchaseListByItemId = function (event) {
        return this.purchaseService
            .getPurchaseListByItemId(event);
    };
    // getItemDetailByPurchaseId(ItemId: number) {
    //   if (ItemId !== 0) {
    //     this.commonLoader.showLoader();
    //     this.purchaseService
    //       .getItemDetailByPurchaseId(ItemId)
    //       .pipe(takeUntil(this.destroyed$))
    //       .subscribe(x => {
    //         if (x != null) {
    //           this.addProcurementForm.get('ItemId').patchValue(x.ItemId);
    //           this.addProcurementForm.get('InventoryTypeId').patchValue(x.InventoryTypeId);
    //           this.addProcurementForm.get('InventoryId').patchValue(x.InventoryId);
    //           this.addProcurementForm.get('ItemGroupId').patchValue(x.ItemGroupId);
    //           this.addProcurementForm.get('PurchaseId').patchValue(x.PurchaseId);
    //           forkJoin([
    //             this.getInventoriesByInventoryTypeId(x.InventoryTypeId),
    //             this.getAllStoreItemGroups(x.InventoryId),
    //             this.getAllStoreItemsByGroupId(x.ItemGroupId),
    //             this.getPurchaseListByItemId(x.ItemId)
    //           ])
    //             .pipe(takeUntil(this.destroyed$))
    //             .subscribe(result => {
    //               this.subscribeInventoriesByInventoryTypeId(result[0]);
    //               this.subscribeAllStoreItemGroups(result[1]);
    //               this.subscribeAllStoreItemsByGroupId(result[2]);
    //               this.subscribePurchaseListByItemId(result[3]);
    //             });
    //         }
    //       },
    //         error => {
    //           console.error(error);
    //         });
    //   }
    // }
    AddProcurementsComponent.prototype.saveProcurement = function () {
        if (this.addProcurementForm.valid) {
            var model = {
                IssueDate: this.addProcurementForm.value.IssueDate,
                IssuedToEmployeeId: this.addProcurementForm.value.IssuedToEmployeeId,
                IssuedQuantity: this.addProcurementForm.value.IssuedQuantity,
                StatusId: this.addProcurementForm.value.StatusId,
                VoucherNo: this.addProcurementForm.value.VoucherNo,
                ProjectId: this.addProcurementForm.value.ProjectId,
                StoreSourceId: this.addProcurementForm.value.StoreSourceId,
                MustReturn: this.addProcurementForm.value.MustReturn,
                ProcurementId: this.procurementId,
                PurchaseId: this.purchaseId,
                ItemId: this.itemId
            };
            if (this.procurementId) {
                this.editProcurement(model);
            }
            else {
                this.addProcurement(model);
            }
        }
        else {
            this.toastr.warning('Please correct errors in procurement form and submit again');
        }
    };
    AddProcurementsComponent.prototype.editProcurement = function (model) {
        var _this = this;
        if (model.IssuedQuantity === 0) {
            this.toastr.warning('Issued Quantity should be greater than 0');
            return;
        }
        this.commonLoader.showLoader();
        this.purchaseService.editProcurement(model)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            if (x.StatusCode === 200) {
                // this.addProcurementForm.get('ProcurementId').patchValue(x.data.ProcurementModel.ProcurementId);
                // this.addProcurementForm.get('EmployeeName').patchValue(x.data.ProcurementModel.EmployeeName);
                // this.addProcurementForm.get('EmployeeId').patchValue(x.data.ProcurementModel.EmployeeId);
                // this.dialogRef.close(this.addProcurementForm.getRawValue());
                _this.toastr.success(x.Message);
                _this.commonLoader.hideLoader();
                _this.router.navigate(['store/purchases']);
            }
            else if (x.StatusCode === 400) {
                _this.toastr.warning(x.Message);
                _this.commonLoader.hideLoader();
            }
        }, function (error) {
            console.log(error);
            _this.commonLoader.hideLoader();
            // console.log(error);
        });
    };
    AddProcurementsComponent.prototype.addProcurement = function (model) {
        var _this = this;
        if (model.IssuedQuantity === 0) {
            this.toastr.warning('Issued Quantity should be greater than 0');
            return;
        }
        this.commonLoader.showLoader();
        this.purchaseService.addProcurement(model)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.router.navigate(['store/purchases']);
                // this.addProcurementForm.get('ProcurementId').patchValue(x.data.ProcurementModel.ProcurementId);
                // this.addProcurementForm.get('EmployeeName').patchValue(x.data.ProcurementModel.EmployeeName);
                // this.addProcurementForm.get('EmployeeId').patchValue(x.data.ProcurementModel.EmployeeId);
                // this.dialogRef.close(this.addProcurementForm.getRawValue());
                _this.toastr.success(x.Message);
                _this.commonLoader.hideLoader();
            }
            else if (x.StatusCode === 400) {
                _this.toastr.warning(x.Message);
                _this.commonLoader.hideLoader();
            }
        }, function (error) {
            console.log(error);
            _this.commonLoader.hideLoader();
            // console.log(error);
        });
    };
    AddProcurementsComponent.prototype.getProcurementDetailsByProcurementId = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.purchaseService.getProcurementDetailsByProcurementId(this.procurementId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            if (x.Procurement) {
                _this.addProcurementForm.patchValue({
                    'ProcurementId': x.Procurement.OrderId,
                    'PurchaseId': x.Procurement.PurchaseId,
                    'IssuedQuantity': x.Procurement.IssuedQuantity,
                    'IssuedToEmployeeId': x.Procurement.IssuedToEmployeeId,
                    'IssueDate': x.Procurement.IssueDate,
                    'ProjectId': x.Procurement.ProjectId,
                    'StoreSourceId': x.Procurement.IssedToLocation,
                    'StatusId': x.Procurement.StatusAtTimeOfIssue,
                    'MustReturn': x.Procurement.MustReturn,
                    'EmployeeName': [null],
                    'Returned': [false],
                    'VoucherNo': x.Procurement.VoucherNo,
                    'OfficeId': x.Procurement.OfficeId
                });
                _this.purchaseId = x.Procurement.PurchaseId;
                _this.itemId = x.Procurement.ItemId;
                _this.getAllEmployeesByOfficeId(x.Procurement.OfficeId);
                _this.maxQuantity = x.Procurement.RemainingQuantity + x.Procurement.IssuedQuantity;
                _this.addProcurementForm.controls['IssuedQuantity'].setValidators(_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].max(_this.maxQuantity));
                _this.addProcurementForm.controls['IssuedQuantity'].updateValueAndValidity();
                _this.commonLoader.hideLoader();
            }
            else if (x.StatusCode === 400) {
                _this.toastr.warning(x.Message);
                _this.commonLoader.hideLoader();
            }
        }, function (error) {
            console.log(error);
            _this.commonLoader.hideLoader();
            // console.log(error);
        });
    };
    AddProcurementsComponent.prototype.goBack = function () {
        this.router.navigate(['store/purchases']);
    };
    //#region "onCancelPopup"
    AddProcurementsComponent.prototype.onCancelPopup = function () {
        // this.dialogRef.close(false);
    };
    //#endregion
    AddProcurementsComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    AddProcurementsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-procurements',
            template: __webpack_require__(/*! ./add-procurements.component.html */ "./src/app/store/components/add-procurements/add-procurements.component.html"),
            styles: [__webpack_require__(/*! ./add-procurements.component.scss */ "./src/app/store/components/add-procurements/add-procurements.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _services_purchase_service__WEBPACK_IMPORTED_MODULE_5__["PurchaseService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__["CommonLoaderService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_8__["ToastrService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_9__["ActivatedRoute"], _angular_router__WEBPACK_IMPORTED_MODULE_9__["Router"]])
    ], AddProcurementsComponent);
    return AddProcurementsComponent;
}());



/***/ }),

/***/ "./src/app/store/components/add-purchase/add-purchase.component.html":
/*!***************************************************************************!*\
  !*** ./src/app/store/components/add-purchase/add-purchase.component.html ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<form [formGroup]='addPurchaseForm' (ngSubmit)=\"purchaseFormSubmit()\">\r\n<lib-sub-header-template>\r\n  <span class=\"action_header\">{{headerText}}\r\n    <hum-button *ngIf=\"!isAddPurchaseFormSubmitted\" [isSubmit]=\"true\" [type]=\"'save'\" [text]=\"'save'\">\r\n    </hum-button>\r\n    <hum-button *ngIf=\"isAddPurchaseFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='cancelButtonClicked()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n\r\n  </span>\r\n  <div class=\"action_section\">\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n\r\n<div class=\"alert alert-warning\" *ngIf=\"exchangeRateMessage != '' && exchangeRateMessage != null\" role=\"alert\">\r\n  <p class=\"txt-center-align\">{{exchangeRateMessage}}</p>\r\n</div>\r\n\r\n<mat-card [ngStyle]=\"scrollStyles\">\r\n\r\n    <div class=\"row\">\r\n        <h4>Item Detail</h4>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['InventoryTypeId'].hasError('required')\"\r\n            [options]=\"inventoryType$\" formControlName=\"InventoryTypeId\" [placeHolder]=\"'Inventory'\"\r\n            (change)=\"getInventoryTypeSelectedValue($event)\">\r\n          </lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['InventoryId'].hasError('required')\"\r\n            [options]=\"storeInventory$\" formControlName='InventoryId' [placeHolder]=\"'Inventory(Master)'\"\r\n            (change)=\"getMasterInventorySelectedValue($event)\"></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['ItemGroupId'].hasError('required')\"\r\n            [options]=\"storeItemGroups$\" formControlName='ItemGroupId' [placeHolder]=\"'Item Group'\"\r\n            (change)=\"getItemGroupSelectedValue($event)\"></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['ItemId'].hasError('required')\"\r\n            [options]=\"storeItems$\" formControlName='ItemId' [placeHolder]=\"'Item'\"\r\n            (change)=\"getItemSelectedValue($event)\"></lib-hum-dropdown>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <h4>Project & Analytical Information</h4>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['OfficeId'].hasError('required')\"\r\n            [options]=\"offices$\" formControlName='OfficeId' [placeHolder]=\"'Office'\"\r\n            (change)=\"getOfficeSelectedValue($event)\"></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['ProjectId'].hasError('required')\"\r\n            [options]=\"project$\" formControlName='ProjectId' [placeHolder]=\"'Project'\"\r\n            (change)=\"getProjectSelectedValue($event)\"></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['BudgetLineId'].hasError('required')\"\r\n            [options]=\"budgetLine$\" formControlName='BudgetLineId' [placeHolder]=\"'Budget Line'\"\r\n            ></lib-hum-dropdown>\r\n        </div>\r\n    </div>\r\n\r\n    <!-- Transport Details-->\r\n    <div class=\"row\">\r\n\r\n      <div *ngIf=\"(ItemGroupTransportCategory == ItemTransportCategoryEnum.Vehicle && ItemTransportCategory ==ItemTransportCategoryEnum.Vehicle) ||\r\n      (ItemGroupTransportCategory == ItemTransportCategoryEnum.Generator && ItemTransportCategory ==ItemTransportCategoryEnum.Generator)\">\r\n        <div class=\"col-md-10\">\r\n          <strong>Note:</strong>The item you have selected is <strong>{{selectedItemName}}</strong>. Therefore, the\r\n          following <strong>transport related metadata</strong> must be filled out.\r\n        </div>\r\n\r\n        <div class=\"col-md-2\" *ngIf=\"addPurchaseForm.get('PurchaseId').value == null\">\r\n          <div class=\"items-float-right\">\r\n            <hum-button (click)='addTransportItemButtonClicked(addPurchaseForm.controls[\"ItemId\"].value)'\r\n              [type]=\"'add'\" [text]=\"'Add'\"></hum-button>\r\n          </div>\r\n        </div>\r\n        <br>\r\n\r\n      <div class=\"row\" *ngIf=\"ItemTransportCategory ==ItemTransportCategoryEnum.Vehicle\">\r\n        <div class=\"col-md-12\">\r\n          <mat-accordion>\r\n            <mat-expansion-panel formArrayName=\"TransportVehicles\"\r\n              *ngFor=\"let item of addPurchaseForm.controls['TransportVehicles'].controls; let i= index\">\r\n              <mat-expansion-panel-header>\r\n                <mat-panel-title>\r\n                  <h4>New Vehicle Detail Form - {{item.get('PlateNo').value}}</h4>\r\n                </mat-panel-title>\r\n                <hum-button (click)='deleteVehicle(i)' [type]=\"'remove'\" [text]=\"'Remove'\" *ngIf=\"addPurchaseForm.get('PurchaseId').value == null && addPurchaseForm.controls['TransportVehicles'].length > 1\"></hum-button>\r\n              </mat-expansion-panel-header>\r\n              <app-vehicle-detail [formGroupName]=\"i\" [officeId]='addPurchaseForm.controls[\"OfficeId\"].value'\r\n                [vehicleDetailForm]='item'></app-vehicle-detail>\r\n            </mat-expansion-panel>\r\n          </mat-accordion>\r\n        </div>\r\n      </div>\r\n\r\n      <div class=\"row\" *ngIf=\"ItemTransportCategory ==ItemTransportCategoryEnum.Generator\">\r\n        <div class=\"col-md-12\">\r\n          <mat-accordion>\r\n            <mat-expansion-panel formArrayName=\"TransportGenerators\"\r\n              *ngFor=\"let item of addPurchaseForm.controls['TransportGenerators'].controls; let i= index\">\r\n              <mat-expansion-panel-header>\r\n                <mat-panel-title>\r\n                  <h4>New Generator Detail Form - {{item.get('Voltage').value}}</h4>\r\n                </mat-panel-title>\r\n                <hum-button (click)='deleteGenerator(i)' [type]=\"'remove'\" [text]=\"'Remove'\" *ngIf=\"addPurchaseForm.get('PurchaseId').value == null && addPurchaseForm.controls['TransportGenerators'].length > 1\"></hum-button>\r\n              </mat-expansion-panel-header>\r\n              <app-generator-detail [formGroupName]=\"i\" [officeId]='addPurchaseForm.controls[\"OfficeId\"].value'\r\n                [generatorDetailForm]='item'></app-generator-detail>\r\n            </mat-expansion-panel>\r\n          </mat-accordion>\r\n        </div>\r\n      </div>\r\n    </div>\r\n\r\n      <div *ngIf=\"enablePurchaseItem()\" class=\"paddingleft\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <strong>Note:</strong>The item you have selected is <strong>{{selectedItemName}}</strong>. Therefore, the\r\n          following <strong>transport\r\n            related metadata</strong> must be filled out.\r\n        </div>\r\n      </div>\r\n      <br>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['TransportItemId'].hasError('required')\"\r\n            [options]=\"purchaseItemDataSource$\" formControlName='TransportItemId' [placeHolder]=\"transportItemPlaceholder\"></lib-hum-dropdown>\r\n        </div>\r\n      </div>\r\n      </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <h4>Purchase Detail</h4>\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <input matInput formControlName='PurchaseName' placeholder=\"Purchase Name\">\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <input matInput type=\"number\" formControlName='PurchaseOrderNo' placeholder=\"Purchase Order No\">\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <input matInput [matDatepicker]=\"PurchaseOrderDate\" (dateChange)=\"PurchaseDateChange($event)\"\r\n              formControlName='PurchaseOrderDate' placeholder=\"Purchase Order Date\">\r\n            <mat-datepicker-toggle matSuffix [for]=\"PurchaseOrderDate\"></mat-datepicker-toggle>\r\n            <mat-datepicker #PurchaseOrderDate></mat-datepicker>\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <input matInput [matDatepicker]=\"InvoiceDate\" formControlName='InvoiceDate' placeholder=\"Invoice Date\">\r\n            <mat-datepicker-toggle matSuffix [for]=\"InvoiceDate\"></mat-datepicker-toggle>\r\n            <mat-datepicker #InvoiceDate></mat-datepicker>\r\n          </mat-form-field>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <input matInput type=\"number\" formControlName='InvoiceNo' placeholder=\"Invoice No\">\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['AssetTypeId'].hasError('required')\"\r\n            [options]=\"assetType$\" formControlName='AssetTypeId' [placeHolder]=\"'Asset Type'\"\r\n            ></lib-hum-dropdown>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <h4>Quantity & Price Detail</h4>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['Unit'].hasError('required')\" [options]=\"unit$\"\r\n            formControlName='Unit' [placeHolder]=\"'Unit'\"></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <input matInput type=\"number\" formControlName='Quantity' placeholder=\"Quantity\">\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['CurrencyId'].hasError('required')\"\r\n            [options]=\"currency$\" formControlName='CurrencyId' [placeHolder]=\"'Currency'\"\r\n            ></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <input matInput type=\"number\" formControlName='Price' placeholder=\"Price\">\r\n\r\n          </mat-form-field>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <input matInput [matDatepicker]=\"ReceiptDate\" formControlName='ReceiptDate' placeholder=\"Receipt Date\">\r\n            <mat-datepicker-toggle matSuffix [for]=\"ReceiptDate\"></mat-datepicker-toggle>\r\n            <mat-datepicker #ReceiptDate></mat-datepicker>\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['ReceivedFromLocation'].hasError('required')\"\r\n            [options]=\"storeSource$\" formControlName='ReceivedFromLocation' [placeHolder]=\"'Received From Location'\"\r\n            ></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['ReceivedFromEmployeeId'].hasError('required')\"\r\n            [options]=\"employeeList$\" formControlName='ReceivedFromEmployeeId' [placeHolder]=\"'Received From Employee'\"\r\n            ></lib-hum-dropdown>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['ReceiptTypeId'].hasError('required')\"\r\n            [options]=\"receiptType$\" formControlName='ReceiptTypeId' [placeHolder]=\"'Receipt Type'\"\r\n            ></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <lib-hum-dropdown [validation]=\"addPurchaseForm.controls['StatusId'].hasError('required')\"\r\n            [options]=\"statusList$\" formControlName='StatusId' [placeHolder]=\"'Status'\"\r\n            ></lib-hum-dropdown>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <h4>Depreciation</h4>\r\n        <div class=\"col-md-3\">\r\n          <mat-checkbox formControlName='ApplyDepreciation'>Apply Depreciation</mat-checkbox>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <input matInput type=\"number\" formControlName='DepreciationRate' placeholder=\"Depreciation Rate(%)\">\r\n          </mat-form-field>\r\n        </div>\r\n      </div>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n        <h4 style=\"padding-left: 0px !important\">Documents\r\n          <hum-button [type]=\"'add'\" [text]=\"'add'\" (click)=\"openAddDocumentDialog()\"></hum-button>\r\n        </h4>\r\n        <app-document-upload [showDownloadButton]=\"showDownloadButton\" [hideColums$]=\"hideUnitColums\" [purchasedDocumentFiles]=\"uploadedPurchasedFiles\" (documentButtonClicked)=\"onPurchaseDocumentButtonClick($event)\"></app-document-upload>\r\n      </div>\r\n    </div>\r\n</mat-card>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/store/components/add-purchase/add-purchase.component.scss":
/*!***************************************************************************!*\
  !*** ./src/app/store/components/add-purchase/add-purchase.component.scss ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "h4 {\n  padding-left: 11px; }\n\n.paddingleft {\n  padding-left: 11px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9hZGQtcHVyY2hhc2UvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcc3RvcmVcXGNvbXBvbmVudHNcXGFkZC1wdXJjaGFzZVxcYWRkLXB1cmNoYXNlLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUNBO0VBQ0Usa0JBQWlCLEVBQUE7O0FBR25CO0VBQ0Usa0JBQWlCLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9zdG9yZS9jb21wb25lbnRzL2FkZC1wdXJjaGFzZS9hZGQtcHVyY2hhc2UuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyJcclxuaDQge1xyXG4gIHBhZGRpbmctbGVmdDoxMXB4O1xyXG59XHJcblxyXG4ucGFkZGluZ2xlZnQge1xyXG4gIHBhZGRpbmctbGVmdDoxMXB4O1xyXG59XHJcbiJdfQ== */"

/***/ }),

/***/ "./src/app/store/components/add-purchase/add-purchase.component.ts":
/*!*************************************************************************!*\
  !*** ./src/app/store/components/add-purchase/add-purchase.component.ts ***!
  \*************************************************************************/
/*! exports provided: AddPurchaseComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddPurchaseComponent", function() { return AddPurchaseComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var src_app_dashboard_project_management_project_list_budgetlines_budget_line_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/dashboard/project-management/project-list/budgetlines/budget-line.service */ "./src/app/dashboard/project-management/project-list/budgetlines/budget-line.service.ts");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var _vehicle_detail_vehicle_detail_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../vehicle-detail/vehicle-detail.component */ "./src/app/store/components/vehicle-detail/vehicle-detail.component.ts");
/* harmony import */ var _document_upload_add_document_component__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../document-upload/add-document.component */ "./src/app/store/components/document-upload/add-document.component.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var _generator_detail_generator_detail_component__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../generator-detail/generator-detail.component */ "./src/app/store/components/generator-detail/generator-detail.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
















var AddPurchaseComponent = /** @class */ (function () {
    function AddPurchaseComponent(purchaseService, fb, budgetLineService, commonLoader, toastr, router, datePipe, dialog, globalSharedService, activatedRoute) {
        var _this = this;
        this.purchaseService = purchaseService;
        this.fb = fb;
        this.budgetLineService = budgetLineService;
        this.commonLoader = commonLoader;
        this.toastr = toastr;
        this.router = router;
        this.datePipe = datePipe;
        this.dialog = dialog;
        this.globalSharedService = globalSharedService;
        this.activatedRoute = activatedRoute;
        this.exchangeRateMessage = '';
        this.isAddPurchaseFormSubmitted = false;
        this.transportItemPlaceholder = '';
        this.selectedItemGroupName = '';
        this.selectedItemName = '';
        this.uploadedPurchasedFiles = [];
        this.headerText = '';
        this.showDownloadButton = false;
        // store enum in a variable to access it in html
        this.MasterCategory = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["StoreMasterCategory"];
        this.ItemGroups = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["StoreItemGroups"];
        this.StoreItems = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["StoreItem"];
        this.ItemTransportCategoryEnum = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["TransportItemCategory"];
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_3__["ReplaySubject"](1);
        this.initForm();
        this.activatedRoute.params.subscribe(function (params) {
            _this.purchaseId = params['id'];
        });
        if (this.purchaseId) {
            this.headerText = 'Edit Purchase';
            this.showDownloadButton = true;
            this.getStorePurchaseById(this.purchaseId);
        }
        else {
            this.headerText = 'Add New Purchase';
            this.showDownloadButton = false;
        }
    }
    AddPurchaseComponent.prototype.ngOnInit = function () {
        var _this = this;
        Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["forkJoin"])([
            this.getAllInventoryTypes(),
            this.getAllProjects(),
            this.getAllOffice(),
            this.getAssetType(),
            this.getAllCurrency(),
            this.getStoreLocations(),
            this.getAllStatusAtTimeOfIssue(),
            this.getAllUnitTypeDetails(),
            this.getAllReceiptType()
        ])
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeInventoryTypes(result[0]);
            _this.subscribeAllProjects(result[1]);
            _this.subscribeAllOffice(result[2]);
            _this.subscribeAssetType(result[3]);
            _this.subscribeAllCurrency(result[4]);
            _this.subscribeStoreLocations(result[5]);
            _this.subscribeAllStatusAtTimeOfIssue(result[6]);
            _this.subscribeAllUnitTypeDetails(result[7]);
            _this.subscribeAllReceiptType(result[8]);
        });
        this.getLoggedInUserUsername();
        this.addPurchaseForm.valueChanges.subscribe(function (data) {
            // this.logValidationErrors(this.addPurchaseForm);
        });
        this.getScreenSize();
        this.hideUnitColums = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])({
            headers: ['Name', 'Type', 'Uploaded On', 'Uploaded By'],
            items: ['Filename', 'DocumentTypeName', 'Date', 'UploadedBy']
        });
    };
    AddPurchaseComponent.prototype.initForm = function () {
        this.addPurchaseForm = this.fb.group({
            'InventoryTypeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'InventoryId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'ItemGroupId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'ItemId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'OfficeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'ProjectId': [null],
            'BudgetLineId': [null],
            'PurchaseOrderNo': [null],
            'PurchaseName': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'ReceiptDate': [null],
            'PurchaseOrderDate': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'InvoiceDate': [null],
            'InvoiceNo': [null],
            'AssetTypeId': [null],
            'Unit': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Quantity': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'CurrencyId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Price': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'ReceivedFromLocation': [null],
            'ReceivedFromEmployeeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'ReceiptTypeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'StatusId': [null],
            'ApplyDepreciation': [false],
            'DepreciationRate': [null],
            'TransportVehicles': new _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormArray"]([]),
            'TransportGenerators': new _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormArray"]([]),
            'TransportItemId': [null],
            'PurchaseId': [null]
        });
    };
    AddPurchaseComponent.prototype.subscribeInventoryTypes = function (response) {
        this.inventoryType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.Result.map(function (y) {
            return {
                value: y.Id,
                name: y.InventoryName
            };
        }));
    };
    AddPurchaseComponent.prototype.subscribeAllProjects = function (response) {
        this.project$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.ProjectDetailModel.map(function (y) {
            return {
                value: y.ProjectId,
                name: y.ProjectCode + '-' + y.ProjectName
            };
        }));
    };
    AddPurchaseComponent.prototype.subscribeAllOffice = function (response) {
        this.offices$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.OfficeDetailsList.map(function (y) {
            return {
                value: y.OfficeId,
                name: y.OfficeCode + '-' + y.OfficeName
            };
        }));
    };
    AddPurchaseComponent.prototype.subscribeAssetType = function (response) {
        var asset = [];
        response.forEach(function (x) {
            asset.push({
                value: x.AssetTypeId,
                name: x.AssetTypeName
            });
        });
        this.assetType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(asset);
    };
    AddPurchaseComponent.prototype.subscribeAllCurrency = function (response) {
        this.currency$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.CurrencyList.map(function (y) {
            return {
                value: y.CurrencyId,
                name: y.CurrencyCode + '-' + y.CurrencyName
            };
        }));
    };
    AddPurchaseComponent.prototype.subscribeStoreLocations = function (response) {
        this.storeSource$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.SourceCodeDatalist.map(function (y) {
            return {
                value: y.SourceCodeId,
                name: y.Code + '-' + y.Description
            };
        }));
    };
    AddPurchaseComponent.prototype.subscribeAllStatusAtTimeOfIssue = function (response) {
        this.statusList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.StatusAtTimeOfIssueList.map(function (y) {
            return {
                value: y.StatusAtTimeOfIssueId,
                name: y.StatusName
            };
        }));
    };
    AddPurchaseComponent.prototype.subscribeAllUnitTypeDetails = function (response) {
        this.unit$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.PurchaseUnitTypeList.map(function (y) {
            return {
                name: y.UnitTypeName,
                value: y.UnitTypeId
            };
        }));
        // const index = response.data.PurchaseUnitTypeList.findIndex(x => x.IsDefault === true);
        // if (index !== -1) {
        //   this.addPurchaseForm.controls['Unit'].patchValue(response.data.PurchaseUnitTypeList[index].UnitTypeId);
        // }
    };
    AddPurchaseComponent.prototype.subscribeAllReceiptType = function (response) {
        this.commonLoader.hideLoader();
        this.receiptType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.ReceiptTypeList.map(function (y) {
            return {
                value: y.ReceiptTypeId,
                name: y.ReceiptTypeName
            };
        }));
    };
    //#region "Dynamic Scroll"
    AddPurchaseComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    AddPurchaseComponent.prototype.getAllInventoryTypes = function () {
        this.commonLoader.showLoader();
        return this.purchaseService.getAllInventoryTypeList();
    };
    AddPurchaseComponent.prototype.getAllProjects = function () {
        return this.purchaseService.getAllProjectList();
    };
    AddPurchaseComponent.prototype.getAllOffice = function () {
        return this.purchaseService.getAllOfficeList();
    };
    AddPurchaseComponent.prototype.getAssetType = function () {
        return this.purchaseService.getPurchaseAssetType();
    };
    AddPurchaseComponent.prototype.getAllCurrency = function () {
        return this.purchaseService.getAllCurrency();
    };
    AddPurchaseComponent.prototype.getStoreLocations = function () {
        return this.purchaseService.getAllStoreSource();
    };
    AddPurchaseComponent.prototype.getAllStatusAtTimeOfIssue = function () {
        return this.purchaseService.getAllStatusAtTimeOfIssue();
    };
    AddPurchaseComponent.prototype.getAllUnitTypeDetails = function () {
        return this.purchaseService.getAllUnitTypeDetails();
    };
    AddPurchaseComponent.prototype.getAllReceiptType = function () {
        return this.purchaseService.getAllReceiptType();
    };
    AddPurchaseComponent.prototype.getInventoryTypeSelectedValue = function (event) {
        this.getInventoriesByInventoryTypeId(event);
    };
    AddPurchaseComponent.prototype.getMasterInventorySelectedValue = function (event) {
        this.getAllStoreItemGroups(event);
    };
    AddPurchaseComponent.prototype.getItemGroupSelectedValue = function (event) {
        this.getAllStoreItemsByGroupId(event);
    };
    AddPurchaseComponent.prototype.getItemSelectedValue = function (event) {
        var _this = this;
        this.getTransportItemCategoryType(event);
        if (!this.purchaseId) {
            this.getDefaultUnitType(event);
        }
        this.storeItems$.subscribe(function (x) {
            var index = x.findIndex(function (y) { return y.value === event; });
            _this.selectedItemName = x[index].name;
        });
    };
    AddPurchaseComponent.prototype.getSelectedItemName = function (event) {
        var _this = this;
        this.storeItems$.subscribe(function (x) {
            var index = x.findIndex(function (y) { return y.value === event; });
            _this.selectedItemName = x[index].name;
        });
    };
    AddPurchaseComponent.prototype.getOfficeSelectedValue = function (event) {
        this.getEmployeesByOfficeId(event);
    };
    AddPurchaseComponent.prototype.getProjectSelectedValue = function (event) {
        this.getBudgetLineByProjectId(event);
    };
    AddPurchaseComponent.prototype.getEmployeesByOfficeId = function (officeId) {
        var _this = this;
        this.purchaseService.getEmployeesByOfficeId(officeId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.employeeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                return {
                    name: y.CodeEmployeeName,
                    value: y.EmployeeId
                };
            }));
        });
    };
    AddPurchaseComponent.prototype.getBudgetLineByProjectId = function (projectId) {
        var _this = this;
        if (projectId !== undefined) {
            this.budgetLineService.GetProjectBudgetLineList(projectId)
                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
                .subscribe(function (x) {
                _this.budgetLine$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                    return {
                        name: y.BudgetCode + '-' + y.BudgetName,
                        value: y.BudgetLineId
                    };
                }));
            });
        }
    };
    AddPurchaseComponent.prototype.getInventoriesByInventoryTypeId = function (inventoryTypeId) {
        var _this = this;
        this.purchaseService
            .getInventoriesByInventoryTypeId(inventoryTypeId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.storeInventory$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                return {
                    name: y.InventoryCode + '-' + y.InventoryName,
                    value: y.InventoryId
                };
            }));
        });
    };
    AddPurchaseComponent.prototype.getAllStoreItemGroups = function (inventoryId, groupId) {
        var _this = this;
        this.purchaseService
            .getItemGroupByInventoryId(inventoryId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.storeItemGroups$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                return {
                    name: y.ItemGroupCode + '-' + y.ItemGroupName,
                    value: y.ItemGroupId
                };
            }));
        });
    };
    AddPurchaseComponent.prototype.getTransportItemCategoryType = function (itemId) {
        var _this = this;
        this.purchaseService
            .getTransportItemCategoryType(itemId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.ItemTransportCategory = x;
            if (_this.ItemTransportCategory === _this.ItemTransportCategoryEnum.Vehicle) {
                // this.removeVehicles(); // remove existing vehicle if any
                _this.addVehicles();
                // Used to get transport item data source
                _this.selectedTransportItemType = _this.ItemTransportCategoryEnum.Vehicle;
                // Remove validations on Transport Item
                _this.addPurchaseForm.get('TransportItemId').clearValidators();
                _this.addPurchaseForm.controls['TransportItemId'].updateValueAndValidity();
                // set default quantity
                _this.addPurchaseForm.controls['Quantity'].setValue(1);
                // disable quantity
                _this.addPurchaseForm.controls['Quantity'].disable();
            }
            // else {
            //   this.removeVehicles();
            // }
            else if (_this.ItemTransportCategory === _this.ItemTransportCategoryEnum.Generator) {
                // this.removeGenerators(); // remove existing generator if any
                _this.addGenerators();
                // Remove validations on Transport Item
                _this.addPurchaseForm.get('TransportItemId').clearValidators();
                _this.addPurchaseForm.controls['TransportItemId'].updateValueAndValidity();
                // set default quantity
                _this.addPurchaseForm.controls['Quantity'].setValue(1);
                // disable quantity
                _this.addPurchaseForm.controls['Quantity'].disable();
            }
            // else {
            //   this.removeGenerators();
            // }
            else {
                // enable quantity
                if (_this.addPurchaseForm.controls['Quantity'].disabled) {
                    _this.addPurchaseForm.controls['Quantity'].enable();
                    _this.addPurchaseForm.controls['Quantity'].setValue(null);
                }
                _this.removeGenerators();
                _this.removeVehicles();
            }
            // Set dynamic required validation for transport item selected and get TransportItem Datasource for based on condition below
            if ((_this.ItemGroupTransportCategory === _this.ItemTransportCategoryEnum.Vehicle &&
                _this.ItemTransportCategory !== _this.ItemTransportCategoryEnum.Vehicle) ||
                (_this.ItemGroupTransportCategory === _this.ItemTransportCategoryEnum.Generator &&
                    _this.ItemTransportCategory !== _this.ItemTransportCategoryEnum.Generator)) {
                _this.getTransportItemDataSource(_this.ItemTransportCategory);
                _this.addPurchaseForm.get('TransportItemId').setValidators([_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]);
                _this.addPurchaseForm.controls['TransportItemId'].updateValueAndValidity();
                // enable quantity
                _this.addPurchaseForm.controls['Quantity'].enable();
            }
            // else if (event === this.StoreItems.VehicleFuel || event === this.StoreItems.VehicleMaintenanceService ||
            //   event === this.StoreItems.VehicleMobilOil || event === this.StoreItems.VehicleSpareParts) {
            //   this.getTransportItemDataSource(TransportItemType.Vehicle);
            //   this.addPurchaseForm.get('TransportItemId').setValidators([Validators.required]);
            //   this.addPurchaseForm.controls['TransportItemId'].updateValueAndValidity();
            //   // enable quantity
            //   this.addPurchaseForm.controls['Quantity'].enable();
            // }
        });
    };
    AddPurchaseComponent.prototype.getAllStoreItemsByGroupId = function (groupId, itemId) {
        var _this = this;
        this.purchaseService
            .getItemsByItemGroupId(groupId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.ItemGroupTransportCategory = x.data.length > 0 ? x.data[0].ItemGroupTransportType : null;
            _this.transportItemPlaceholder = (_this.ItemGroupTransportCategory === _this.ItemTransportCategoryEnum.Vehicle) ?
                'Purchased Vehicle Item' : 'Purchased Generator Item';
            _this.storeItems$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                return {
                    name: y.ItemCode + '-' + y.ItemName,
                    value: y.ItemId
                };
            }));
            if (itemId != null) {
                _this.getSelectedItemName(itemId);
            }
        });
    };
    AddPurchaseComponent.prototype.purchaseFormSubmit = function () {
        if (this.addPurchaseForm.get('PurchaseId').value == null || this.addPurchaseForm.get('PurchaseId').value == undefined) {
            this.addPurchaseFormSubmit();
        }
        else {
            this.editPurchaseFormSubmit();
        }
    };
    AddPurchaseComponent.prototype.addPurchaseFormSubmit = function () {
        var _this = this;
        if (this.addPurchaseForm.valid) {
            this.isAddPurchaseFormSubmitted = true;
            this.purchaseService.addPurchase(this.addPurchaseForm.getRawValue(), this.ItemGroupTransportCategory, this.ItemTransportCategory)
                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
                .subscribe(function (x) {
                if (x.StatusCode === 200) {
                    var filteredRecords_1 = _this.uploadedPurchasedFiles.filter(function (z) { return z.Id === 0; });
                    if (filteredRecords_1 !== undefined && filteredRecords_1.length > 0) {
                        var _loop_1 = function (i) {
                            _this.globalSharedService
                                .uploadFile(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["FileSourceEntityTypes"].StorePurchase, x.PurchaseId, _this.uploadedPurchasedFiles[i].File[0], _this.uploadedPurchasedFiles[i].DocumentTypeId)
                                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(_this.destroyed$))
                                .subscribe(function (y) {
                                console.log('uploadSuccess', y);
                                if (i === filteredRecords_1.length - 1) {
                                    _this.isAddPurchaseFormSubmitted = false;
                                    _this.toastr.success('Success');
                                    _this.router.navigate(['store/purchases']);
                                }
                            });
                        };
                        for (var i = 0; i < filteredRecords_1.length; i++) {
                            _loop_1(i);
                        }
                    }
                    else {
                        _this.addPurchaseForm.reset();
                        _this.isAddPurchaseFormSubmitted = false;
                        _this.toastr.success('Success');
                        _this.router.navigate(['store/purchases']);
                    }
                }
                else if (x.StatusCode === 400) {
                    _this.isAddPurchaseFormSubmitted = false;
                    _this.toastr.warning(x.Message);
                }
            }, function (error) {
                _this.isAddPurchaseFormSubmitted = false;
                console.log(error);
            });
        }
        else {
            this.toastr.warning('Please correct errors in purchase form and submit again');
        }
    };
    AddPurchaseComponent.prototype.editPurchaseFormSubmit = function () {
        var _this = this;
        console.log(this.addPurchaseForm);
        var purchaseId = this.addPurchaseForm.value.PurchaseId;
        if (this.addPurchaseForm.valid) {
            this.isAddPurchaseFormSubmitted = true;
            this.purchaseService.EditStorePurchase(this.addPurchaseForm.getRawValue())
                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
                .subscribe(function (x) {
                if (x) {
                    var filteredRecords_2 = _this.uploadedPurchasedFiles.filter(function (z) { return z.Id === 0; });
                    if (filteredRecords_2 !== undefined && filteredRecords_2.length > 0) {
                        var _loop_2 = function (i) {
                            if (_this.uploadedPurchasedFiles[i].Id === 0) {
                                _this.globalSharedService
                                    .uploadFile(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["FileSourceEntityTypes"].StorePurchase, purchaseId, _this.uploadedPurchasedFiles[i].File[0], _this.uploadedPurchasedFiles[i].DocumentTypeId)
                                    .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(_this.destroyed$))
                                    .subscribe(function (y) {
                                    if (i === filteredRecords_2.length - 1) {
                                        _this.isAddPurchaseFormSubmitted = false;
                                        console.log('uploadsuccess');
                                        _this.router.navigate(['store/purchases']);
                                        _this.toastr.success('Success');
                                    }
                                });
                            }
                        };
                        for (var i = 0; i < filteredRecords_2.length; i++) {
                            _loop_2(i);
                        }
                    }
                    else {
                        _this.toastr.success('Success');
                        _this.isAddPurchaseFormSubmitted = false;
                        _this.router.navigate(['store/purchases']);
                    }
                }
                else if (x.StatusCode === 400) {
                    _this.isAddPurchaseFormSubmitted = false;
                    _this.toastr.warning(x.Message);
                }
            }, function (error) {
                _this.isAddPurchaseFormSubmitted = false;
                // console.log(error);
            });
        }
        else {
            this.toastr.warning('Please correct errors in purchase form and submit again');
        }
    };
    AddPurchaseComponent.prototype.PurchaseDateChange = function (event) {
        if (event.value) {
            this.checkExchangeRateExists(event.value);
        }
    };
    AddPurchaseComponent.prototype.checkExchangeRateExists = function (exchangeRateDate) {
        var _this = this;
        if (this.addPurchaseForm.value.OfficeId == null || this.addPurchaseForm.value.OfficeId === undefined ||
            this.addPurchaseForm.value.OfficeId === 0) {
            this.toastr.warning('Select Office');
            this.addPurchaseForm.get('PurchaseOrderDate').patchValue(null);
            return;
        }
        var checkExchangeRateModel = {
            ExchangeRateDate: new Date(new Date(exchangeRateDate).getFullYear(), new Date(exchangeRateDate).getMonth(), new Date(exchangeRateDate).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            OfficeId: this.addPurchaseForm.value.OfficeId
        };
        this.purchaseService
            .checkExchangeRateExists(checkExchangeRateModel)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
            .subscribe(function (data) {
            if (data.StatusCode === 200) {
                if (!data.ResponseData) {
                    _this.exchangeRateMessage = 'No Exchange Rate Defined for ' +
                        _this.datePipe.transform(checkExchangeRateModel.ExchangeRateDate, 'dd-MM-yyyy') +
                        '. Please ensure that the exchange rate has been added and verified for the selected Purchase Order Date.';
                }
                else {
                    _this.exchangeRateMessage = '';
                }
            }
            else {
                _this.toastr.error(data.Message);
            }
        }, function (error) {
        });
    };
    AddPurchaseComponent.prototype.addTransportItemButtonClicked = function (transportItemType) {
        if (this.ItemTransportCategory === this.ItemTransportCategoryEnum.Vehicle) {
            // set default quantity
            this.addPurchaseForm.controls['Quantity'].setValue(this.addPurchaseForm.get('Quantity').value + 1);
            this.addVehicles();
        }
        else if (this.ItemTransportCategory === this.ItemTransportCategoryEnum.Generator) {
            // set default quantity
            this.addPurchaseForm.controls['Quantity'].setValue(this.addPurchaseForm.get('Quantity').value + 1);
            this.addGenerators();
        }
    };
    AddPurchaseComponent.prototype.cancelButtonClicked = function () {
        this.router.navigate(['store/purchases']);
    };
    AddPurchaseComponent.prototype.addVehicles = function () {
        this.removeGenerators();
        this.addPurchaseForm.get('TransportVehicles').push(this.fb.group({
            'PlateNo': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            'EmployeeId': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            'StartingMileage': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(0)]],
            'IncurredMileage': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(0)]],
            'FuelConsumptionRate': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(0)]],
            'MobilOilConsumptionRate': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(0)]],
            'OfficeId': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            'ModelYear': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].maxLength(4)]],
            'ManufacturerCountry': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'EngineNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'RegistrationNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'ChasisNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Remarks': [''],
        }));
    };
    AddPurchaseComponent.prototype.addGenerators = function () {
        this.removeVehicles();
        this.addPurchaseForm.get('TransportGenerators').push(this.fb.group({
            'Voltage': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(0)]],
            'StartingUsage': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            'IncurredUsage': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(0)]],
            'ModelYear': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(0)]],
            'FuelConsumptionRate': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(0)]],
            'MobilOilConsumptionRate': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].min(0)]],
            'OfficeId': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            'ManufacturerCountry': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'EngineNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'RegistrationNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'ChasisNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'EmployeeId': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            'Remarks': [''],
        }));
    };
    AddPurchaseComponent.prototype.removeVehicles = function () {
        // remove vehicle if any
        var control = this.addPurchaseForm.controls['TransportVehicles'];
        while (control.length > 0) {
            control.removeAt(0);
        }
    };
    AddPurchaseComponent.prototype.removeGenerators = function () {
        // remove generator if any
        var control = this.addPurchaseForm.controls['TransportGenerators'];
        while (control.length > 0) {
            control.removeAt(0);
        }
    };
    AddPurchaseComponent.prototype.openAddDocumentDialog = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_document_upload_add_document_component__WEBPACK_IMPORTED_MODULE_12__["AddDocumentComponent"], {
            data: {
                purchaseDocumentList: this.uploadedPurchasedFiles
            }
        });
        dialogRef.afterClosed().subscribe(function (result) {
            // console.log(result);
            if (result) {
                _this.uploadedPurchasedFiles.unshift({
                    Id: result.id,
                    Filename: result.file === undefined ? result.filename : result.file[0].name,
                    DocumentTypeName: result.documentName,
                    Date: _this.datePipe.transform(result.uploadDate, 'dd-MM-yyyy'),
                    UploadedBy: result.uploadBy === undefined ? localStorage.getItem('LoggedInUserName') : result.uploadBy,
                    File: result.file,
                    DocumentTypeId: result.documentType,
                    SignedUrl: result.signedUrl
                });
                // For ngOnChanges on document-upload component
                _this.uploadedPurchasedFiles = _this.uploadedPurchasedFiles.slice();
            }
        });
    };
    AddPurchaseComponent.prototype.getTransportItemDataSource = function (transportItemTypeId) {
        var _this = this;
        if (this.ItemGroupTransportCategory) {
            this.commonLoader.showLoader();
            this.purchaseService.getTransportItemDataSource(this.ItemGroupTransportCategory).subscribe(function (x) {
                _this.purchaseItemDataSource$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(x.map(function (y) {
                    return {
                        value: y.ItemId,
                        name: y.PurchaseIdName
                    };
                }));
                _this.commonLoader.hideLoader();
            }, function (error) {
                _this.commonLoader.hideLoader();
            });
        }
    };
    AddPurchaseComponent.prototype.getLoggedInUserUsername = function () {
        this.purchaseService.GetLoggedInUserUsername().subscribe(function (x) {
            localStorage.setItem('LoggedInUserName', x);
        });
    };
    AddPurchaseComponent.prototype.getStorePurchaseById = function (purchaseId) {
        var _this = this;
        this.commonLoader.showLoader();
        this.purchaseService.getStorePurchaseById(Number(purchaseId))
            .subscribe(function (x) {
            // get All dropdown datasource
            _this.getInventoriesByInventoryTypeId(x.InventoryTypeId);
            _this.getAllStoreItemGroups(x.InventoryId, x.ItemGroupId);
            _this.getAllStoreItemsByGroupId(x.ItemGroupId, x.ItemId);
            _this.getEmployeesByOfficeId(x.OfficeId);
            _this.getBudgetLineByProjectId(x.ProjectId);
            x.StoreDocumentList.forEach(function (y) {
                _this.uploadedPurchasedFiles.push({
                    Id: y.DocumentFileId,
                    Filename: y.DocumentName,
                    DocumentTypeName: y.DocumentTypeName,
                    Date: _this.datePipe.transform(y.UploadedOn, 'dd-MM-yyyy'),
                    UploadedBy: y.UploadedBy,
                    DocumentTypeId: y.DocumentTypeId,
                    File: undefined,
                    SignedUrl: y.SignedURL,
                });
            });
            // For ngOnChanges on document-upload component
            _this.uploadedPurchasedFiles = _this.uploadedPurchasedFiles.slice();
            _this.addPurchaseForm.patchValue({
                InventoryTypeId: x.InventoryTypeId,
                InventoryId: x.InventoryId,
                ItemGroupId: x.ItemGroupId,
                ItemId: x.ItemId,
                OfficeId: x.OfficeId,
                ProjectId: x.ProjectId,
                BudgetLineId: x.BudgetLineId,
                PurchaseOrderNo: x.SerialNo,
                PurchaseName: x.PurchaseName,
                ReceiptDate: x.DeliveryDate,
                PurchaseOrderDate: x.PurchaseDate,
                InvoiceDate: x.InvoiceDate,
                InvoiceNo: x.InvoiceNo,
                AssetTypeId: x.AssetTypeId,
                Unit: x.UnitType,
                Quantity: x.Quantity,
                CurrencyId: x.Currency,
                Price: x.UnitCost,
                ReceivedFromLocation: x.ReceivedFromLocation,
                ReceivedFromEmployeeId: x.PurchasedById,
                ReceiptTypeId: x.ReceiptTypeId,
                StatusId: x.Status,
                ApplyDepreciation: x.ApplyDepreciation,
                DepreciationRate: x.DepreciationRate,
                TransportItemId: x.TransportItemId,
                PurchaseId: x.PurchaseId
            });
            _this.ItemTransportCategory = x.TransportItemTypeCategory;
            _this.ItemGroupTransportCategory = x.ItemGroupTransportCategory;
            if (x.PurchasedVehicleList.length > 0 || x.PurchasedGeneratorList.length > 0) {
                _this.addPurchaseForm.controls['Quantity'].disable();
            }
            _this.setVehicleValue(x.PurchasedVehicleList);
            _this.setGeneratorValue(x.PurchasedGeneratorList);
            // get TransportItem Datasource for vehicle/generator based on condition below
            if (_this.ItemGroupTransportCategory === _this.ItemTransportCategoryEnum.Generator &&
                _this.ItemTransportCategory !== _this.ItemTransportCategoryEnum.Generator) {
                _this.getTransportItemDataSource(_this.ItemTransportCategory);
            }
            else if (_this.ItemGroupTransportCategory === _this.ItemTransportCategoryEnum.Vehicle &&
                _this.ItemTransportCategory !== _this.ItemTransportCategoryEnum.Vehicle) {
                _this.getTransportItemDataSource(_this.ItemTransportCategory);
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    AddPurchaseComponent.prototype.setVehicleValue = function (item) {
        var formArray = new _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormArray"]([]);
        for (var _i = 0, item_1 = item; _i < item_1.length; _i++) {
            var x = item_1[_i];
            formArray.push(this.fb.group({
                Id: x.Id,
                PlateNo: [{ value: x.PlateNo, disabled: true }],
                EmployeeId: [{ value: x.EmployeeId, disabled: true }],
                StartingMileage: [{ value: x.StartingMileage, disabled: true }],
                IncurredMileage: [{ value: x.IncurredMileage, disabled: true }],
                FuelConsumptionRate: [{ value: x.FuelConsumptionRate, disabled: true }],
                MobilOilConsumptionRate: [{ value: x.MobilOilConsumptionRate, disabled: true }],
                OfficeId: [{ value: x.OfficeId, disabled: true }],
                ModelYear: [{ value: x.ModelYear, disabled: true }],
                ManufacturerCountry: [{ value: x.ManufacturerCountry, disabled: true }],
                EngineNo: [{ value: x.EngineNo, disabled: true }],
                RegistrationNo: [{ value: x.RegistrationNo, disabled: true }],
                ChasisNo: [{ value: x.ChasisNo, disabled: true }],
                Remarks: [{ value: x.Remarks, disabled: true }],
            }));
        }
        this.addPurchaseForm.setControl('TransportVehicles', formArray);
    };
    AddPurchaseComponent.prototype.setGeneratorValue = function (item) {
        var formArray = new _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormArray"]([]);
        for (var _i = 0, item_2 = item; _i < item_2.length; _i++) {
            var x = item_2[_i];
            formArray.push(this.fb.group({
                Id: x.Id,
                Voltage: [{ value: x.Voltage, disabled: true }],
                StartingUsage: [{ value: x.StartingUsage, disabled: true }],
                IncurredUsage: [{ value: x.IncurredUsage, disabled: true }],
                FuelConsumptionRate: [{ value: x.FuelConsumptionRate, disabled: true }],
                MobilOilConsumptionRate: [{ value: x.MobilOilConsumptionRate, disabled: true }],
                OfficeId: [{ value: x.OfficeId, disabled: true }],
                ModelYear: [{ value: x.ModelYear, disabled: true }],
                ManufacturerCountry: [{ value: x.ManufacturerCountry, disabled: true }],
                EngineNo: [{ value: x.EngineNo, disabled: true }],
                RegistrationNo: [{ value: x.RegistrationNo, disabled: true }],
                ChasisNo: [{ value: x.ChasisNo, disabled: true }],
                EmployeeId: [{ value: x.EmployeeId, disabled: true }],
                Remarks: [{ value: x.Remarks, disabled: true }],
            }));
        }
        this.addPurchaseForm.setControl('TransportGenerators', formArray);
    };
    AddPurchaseComponent.prototype.deleteVehicle = function (index) {
        var _this = this;
        // decrease quantity
        this.addPurchaseForm.controls['Quantity'].setValue(this.addPurchaseForm.get('Quantity').value - 1);
        var arrayControl = this.addPurchaseForm.get('TransportVehicles');
        var item = arrayControl.at(index);
        if (item.value.Id !== 0 && item.value.Id !== undefined) {
            this.purchaseService.deleteVehicle(item.value.Id).subscribe(function (x) {
                if (x) {
                    _this.addPurchaseForm.get('TransportVehicles').removeAt(index);
                }
                else {
                    _this.toastr.warning('Something went wrong');
                }
            }, function (error) {
                console.log(error);
            });
        }
        else {
            this.addPurchaseForm.get('TransportVehicles').removeAt(index);
        }
    };
    AddPurchaseComponent.prototype.deleteGenerator = function (index) {
        var _this = this;
        // decrease quantity
        this.addPurchaseForm.controls['Quantity'].setValue(this.addPurchaseForm.get('Quantity').value - 1);
        var arrayControl = this.addPurchaseForm.get('TransportGenerators');
        var item = arrayControl.at(index);
        if (item.value.Id !== 0 && item.value.Id !== undefined) {
            this.purchaseService.deleteGenerator(item.value.Id).subscribe(function (x) {
                if (x) {
                    _this.addPurchaseForm.get('TransportGenerators').removeAt(index);
                }
                else {
                    _this.toastr.warning('Something went wrong');
                }
            }, function (error) {
                console.log(error);
            });
        }
        else {
            this.addPurchaseForm.get('TransportGenerators').removeAt(index);
        }
    };
    AddPurchaseComponent.prototype.onPurchaseDocumentButtonClick = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            var index_1 = this.uploadedPurchasedFiles.findIndex(function (obj) { return obj === event.item; });
            if (index_1 > -1) {
                if (this.uploadedPurchasedFiles[index_1].Id > 0) { // remove file from purchasedDocumentList and backend
                    var model_1 = {
                        PageId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_10__["FileSourceEntityTypes"].StorePurchase,
                        DocumentFileId: event.item.Id
                    };
                    this.globalSharedService.deleteFile(model_1).subscribe(function (x) {
                        if (x.StatusCode === 200) {
                            _this.uploadedPurchasedFiles.splice(index_1, 1);
                            _this.uploadedPurchasedFiles = _this.uploadedPurchasedFiles.filter(function (y) { return y.Id !== model_1.DocumentFileId; });
                        }
                    });
                }
                else { // remove file from purchasedDocumentList
                    this.uploadedPurchasedFiles.splice(index_1, 1);
                }
            }
            else {
                this.toastr.warning('Item not found to delete');
            }
        }
        else if (event.type === 'download') {
            window.open(event.item.SignedUrl, '_blank');
        }
    };
    AddPurchaseComponent.prototype.enableVehicleGeneratorDiv = function () {
        var isEnable = false;
        if ((this.ItemGroupTransportCategory === this.ItemTransportCategoryEnum.Vehicle &&
            this.ItemTransportCategory !== this.ItemTransportCategoryEnum.Vehicle) ||
            (this.ItemGroupTransportCategory === this.ItemTransportCategoryEnum.Generator &&
                this.ItemTransportCategory !== this.ItemTransportCategoryEnum.Generator) && this.ItemTransportCategory) {
            isEnable = true;
        }
        return isEnable;
    };
    AddPurchaseComponent.prototype.enablePurchaseItem = function () {
        var isEnable = false;
        if (this.ItemTransportCategory === this.ItemTransportCategoryEnum.MobilOil ||
            this.ItemTransportCategory === this.ItemTransportCategoryEnum.Fuel ||
            this.ItemTransportCategory === this.ItemTransportCategoryEnum.MaintenanceService ||
            this.ItemTransportCategory === this.ItemTransportCategoryEnum.SpareParts) {
            isEnable = true;
        }
        return isEnable;
    };
    AddPurchaseComponent.prototype.getDefaultUnitType = function (itemId) {
        var _this = this;
        this.purchaseService
            .getDefaultUnitTypeByItemId(itemId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_9__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            if (x) {
                _this.addPurchaseForm.controls['Unit'].patchValue(x);
            }
        });
    };
    AddPurchaseComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])(_vehicle_detail_vehicle_detail_component__WEBPACK_IMPORTED_MODULE_11__["VehicleDetailComponent"]),
        __metadata("design:type", _vehicle_detail_vehicle_detail_component__WEBPACK_IMPORTED_MODULE_11__["VehicleDetailComponent"])
    ], AddPurchaseComponent.prototype, "vehicleDetailChild", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])(_generator_detail_generator_detail_component__WEBPACK_IMPORTED_MODULE_15__["GeneratorDetailComponent"]),
        __metadata("design:type", _generator_detail_generator_detail_component__WEBPACK_IMPORTED_MODULE_15__["GeneratorDetailComponent"])
    ], AddPurchaseComponent.prototype, "generatorDetailChild", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], AddPurchaseComponent.prototype, "getScreenSize", null);
    AddPurchaseComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-purchase',
            template: __webpack_require__(/*! ./add-purchase.component.html */ "./src/app/store/components/add-purchase/add-purchase.component.html"),
            styles: [__webpack_require__(/*! ./add-purchase.component.scss */ "./src/app/store/components/add-purchase/add-purchase.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_purchase_service__WEBPACK_IMPORTED_MODULE_2__["PurchaseService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], src_app_dashboard_project_management_project_list_budgetlines_budget_line_service__WEBPACK_IMPORTED_MODULE_4__["BudgetLineService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_6__["ToastrService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_7__["Router"], _angular_common__WEBPACK_IMPORTED_MODULE_8__["DatePipe"],
            _angular_material_dialog__WEBPACK_IMPORTED_MODULE_13__["MatDialog"], src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_14__["GlobalSharedService"], _angular_router__WEBPACK_IMPORTED_MODULE_7__["ActivatedRoute"]])
    ], AddPurchaseComponent);
    return AddPurchaseComponent;
}());



/***/ }),

/***/ "./src/app/store/components/document-upload/add-document.component.html":
/*!******************************************************************************!*\
  !*** ./src/app/store/components/document-upload/add-document.component.html ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2 mat-dialog-title>Upload Document</h2>\r\n<form [formGroup]=\"addDocumentForm\" (submit)=\"submitAddDocument()\">\r\n    <mat-dialog-content class=\"mat-typography\">\r\n        <div class=\"row\">\r\n          <div class=\"col-sm-6\">\r\n              <button type=\"button\" mat-raised-button color=\"primary\" (click)=\"openInput()\">Select File to Upload</button>\r\n    <input id=\"fileInput\" style=\"display:none\" hidden type=\"file\" (change)=\"fileChange($event.target.files)\" name=\"file\">\r\n          </div>\r\n          <div class=\"col-sm-6\">\r\n                <lib-hum-dropdown [validation]=\"addDocumentForm.controls['documentType'].hasError('required')\"\r\n                  [options]=\"documentTypes$\" formControlName='documentType' [placeHolder]=\"'Document Type'\"\r\n                  ></lib-hum-dropdown>\r\n          </div>\r\n        </div>\r\n\r\n      </mat-dialog-content>\r\n      <mat-dialog-actions align=\"end\">\r\n        <hum-button [type]=\"'save'\" [isSubmit]=\"true\" [text]=\"'save'\">\r\n          </hum-button>\r\n          <hum-button (click)='cancelButtonClicked()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n      </mat-dialog-actions>\r\n</form>\r\n\r\n"

/***/ }),

/***/ "./src/app/store/components/document-upload/add-document.component.scss":
/*!******************************************************************************!*\
  !*** ./src/app/store/components/document-upload/add-document.component.scss ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvZG9jdW1lbnQtdXBsb2FkL2FkZC1kb2N1bWVudC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/store/components/document-upload/add-document.component.ts":
/*!****************************************************************************!*\
  !*** ./src/app/store/components/document-upload/add-document.component.ts ***!
  \****************************************************************************/
/*! exports provided: AddDocumentComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddDocumentComponent", function() { return AddDocumentComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
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





var AddDocumentComponent = /** @class */ (function () {
    function AddDocumentComponent(_purchaseService, _fb, _toastr, data, _dialogRef) {
        this._purchaseService = _purchaseService;
        this._fb = _fb;
        this._toastr = _toastr;
        this.data = data;
        this._dialogRef = _dialogRef;
        this.addDocumentForm = this._fb.group({
            'id': [0],
            'file': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required],
            'documentType': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required],
            'documentName': [null],
            'uploadDate': [new Date()]
        });
    }
    AddDocumentComponent.prototype.ngOnInit = function () {
        this.documentTypes$ = this._purchaseService.getPurchaseDocumentTypes();
    };
    AddDocumentComponent.prototype.openInput = function () {
        document.getElementById('fileInput').click();
    };
    AddDocumentComponent.prototype.fileChange = function (file) {
        this.addDocumentForm.controls['file'].patchValue(file);
    };
    AddDocumentComponent.prototype.submitAddDocument = function () {
        var _this = this;
        if (this.addDocumentForm.valid) {
            if (this.data.purchaseDocumentList.length > 0) {
                var index = this.data.purchaseDocumentList.findIndex(function (x) { return x.Filename === _this.addDocumentForm.controls['file'].value[0].name; });
                if (index >= 0) {
                    this._toastr.warning('Filename already Exists. Please re-upload a document with different filename');
                    return;
                }
            }
            var documentTypes_1 = [];
            this.documentTypes$.subscribe(function (x) { return documentTypes_1 = x; });
            var documentName = documentTypes_1.filter(function (x) { return x.value === _this.addDocumentForm.get('documentType').value; });
            this.addDocumentForm.get('documentName').patchValue(documentName[0].name);
            this._dialogRef.close(this.addDocumentForm.value);
        }
        else {
            this._toastr.warning('Please correct errors in add document form and submit again');
        }
    };
    AddDocumentComponent.prototype.cancelButtonClicked = function () {
        this._dialogRef.close();
    };
    AddDocumentComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-document',
            template: __webpack_require__(/*! ./add-document.component.html */ "./src/app/store/components/document-upload/add-document.component.html"),
            styles: [__webpack_require__(/*! ./add-document.component.scss */ "./src/app/store/components/document-upload/add-document.component.scss")]
        }),
        __param(3, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_4__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_services_purchase_service__WEBPACK_IMPORTED_MODULE_1__["PurchaseService"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"], Object, _angular_material_dialog__WEBPACK_IMPORTED_MODULE_4__["MatDialogRef"]])
    ], AddDocumentComponent);
    return AddDocumentComponent;
}());



/***/ }),

/***/ "./src/app/store/components/document-upload/document-upload.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/document-upload/document-upload.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<hum-table *ngIf=\"documentsList$\" [hideColums$]= 'hideColums$' [headers]=\"documentHeaders$\" [items]=\"documentsList$\" [actions]=\"actions\"\r\n   (actionClick)=\"documentButtonClick($event)\">\r\n</hum-table>\r\n"

/***/ }),

/***/ "./src/app/store/components/document-upload/document-upload.component.scss":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/document-upload/document-upload.component.scss ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvZG9jdW1lbnQtdXBsb2FkL2RvY3VtZW50LXVwbG9hZC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/store/components/document-upload/document-upload.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/store/components/document-upload/document-upload.component.ts ***!
  \*******************************************************************************/
/*! exports provided: DocumentUploadComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DocumentUploadComponent", function() { return DocumentUploadComponent; });
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


var DocumentUploadComponent = /** @class */ (function () {
    function DocumentUploadComponent() {
        this.documentButtonClicked = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    DocumentUploadComponent.prototype.ngOnInit = function () {
        this.documentHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Name', 'Type', 'Uploaded On', 'Uploaded By']);
        this.actions = {
            items: {
                button: { status: false, text: '' },
                delete: true,
                download: this.showDownloadButton ? true : false,
            },
            subitems: {}
        };
    };
    DocumentUploadComponent.prototype.ngOnChanges = function () {
        this.documentsList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(this.purchasedDocumentFiles);
    };
    DocumentUploadComponent.prototype.documentButtonClick = function (event) {
        this.documentButtonClicked.emit(event);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Array)
    ], DocumentUploadComponent.prototype, "purchasedDocumentFiles", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Boolean)
    ], DocumentUploadComponent.prototype, "showDownloadButton", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], DocumentUploadComponent.prototype, "documentButtonClicked", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", rxjs__WEBPACK_IMPORTED_MODULE_1__["Observable"])
    ], DocumentUploadComponent.prototype, "hideColums$", void 0);
    DocumentUploadComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-document-upload',
            template: __webpack_require__(/*! ./document-upload.component.html */ "./src/app/store/components/document-upload/document-upload.component.html"),
            styles: [__webpack_require__(/*! ./document-upload.component.scss */ "./src/app/store/components/document-upload/document-upload.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], DocumentUploadComponent);
    return DocumentUploadComponent;
}());



/***/ }),

/***/ "./src/app/store/components/edit-generator/edit-generator.component.html":
/*!*******************************************************************************!*\
  !*** ./src/app/store/components/edit-generator/edit-generator.component.html ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">\r\n    Edit Generator Details\r\n    <hum-button [type]=\"'save'\" [text]=\"'save'\" (click)=\"saveGeneratorDetail()\"></hum-button>\r\n    <hum-button [type]=\"'cancel'\" [text]=\"'cancel'\" (click)=\"backToDetails()\"></hum-button>\r\n  </span>\r\n  <div class=\"action_section\">\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-card>\r\n  <app-generator-detail [generatorDetailForm]=\"generatorDetailForm\"></app-generator-detail>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/store/components/edit-generator/edit-generator.component.scss":
/*!*******************************************************************************!*\
  !*** ./src/app/store/components/edit-generator/edit-generator.component.scss ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvZWRpdC1nZW5lcmF0b3IvZWRpdC1nZW5lcmF0b3IuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/store/components/edit-generator/edit-generator.component.ts":
/*!*****************************************************************************!*\
  !*** ./src/app/store/components/edit-generator/edit-generator.component.ts ***!
  \*****************************************************************************/
/*! exports provided: EditGeneratorComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EditGeneratorComponent", function() { return EditGeneratorComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_5__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var EditGeneratorComponent = /** @class */ (function () {
    function EditGeneratorComponent(router, purchaseService, activatedRoute, fb) {
        var _this = this;
        this.router = router;
        this.purchaseService = purchaseService;
        this.activatedRoute = activatedRoute;
        this.fb = fb;
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_3__["ReplaySubject"](1);
        this.activatedRoute.params.subscribe(function (params) {
            _this.generatorId = params['id'];
        });
    }
    EditGeneratorComponent.prototype.ngOnInit = function () {
        this.generatorDetailForm = this.fb.group({
            'GeneratorId': [null],
            'Voltage': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            'StartingUsage': [null],
            'IncurredUsage': [null],
            'MobilOilConsumptionRate': [null],
            'ModelYear': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            'OfficeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            'FuelConsumptionRate': [null],
            'ManufacturerCountry': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            'EngineNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            'RegistrationNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            'ChasisNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            'EmployeeId': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            'Remarks': [''],
        });
        this.getGeneratorDetailById();
    };
    EditGeneratorComponent.prototype.getGeneratorDetailById = function () {
        var _this = this;
        this.purchaseService.getGeneratorDetailById(this.generatorId)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_5__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.generatorDetailForm.setValue({
                GeneratorId: x.GeneratorId,
                Voltage: x.Voltage,
                StartingUsage: x.StartingUsage,
                IncurredUsage: x.IncurredUsage,
                MobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
                ModelYear: x.ModelYear,
                OfficeId: x.OfficeId,
                FuelConsumptionRate: x.StandardFuelConsumptionRate,
                ManufacturerCountry: x.ManufacturerCountry,
                EngineNo: x.EngineNo,
                RegistrationNo: x.RegistrationNo,
                ChasisNo: x.ChasisNo,
                Remarks: x.Remarks,
                EmployeeId: x.EmployeeId
            });
        });
    };
    EditGeneratorComponent.prototype.saveGeneratorDetail = function () {
        var _this = this;
        if (this.generatorDetailForm.valid) {
            this.purchaseService.saveGeneratorDetail(this.generatorDetailForm.value)
                .subscribe(function (x) {
                if (x) {
                    _this.backToDetails();
                }
            });
        }
    };
    EditGeneratorComponent.prototype.backToDetails = function () {
        this.router.navigate(['store/generator/detail', this.generatorId]);
    };
    EditGeneratorComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    EditGeneratorComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-edit-generator',
            template: __webpack_require__(/*! ./edit-generator.component.html */ "./src/app/store/components/edit-generator/edit-generator.component.html"),
            styles: [__webpack_require__(/*! ./edit-generator.component.scss */ "./src/app/store/components/edit-generator/edit-generator.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"], _services_purchase_service__WEBPACK_IMPORTED_MODULE_4__["PurchaseService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"]])
    ], EditGeneratorComponent);
    return EditGeneratorComponent;
}());



/***/ }),

/***/ "./src/app/store/components/edit-vehicle/edit-vehicle.component.html":
/*!***************************************************************************!*\
  !*** ./src/app/store/components/edit-vehicle/edit-vehicle.component.html ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">\r\n    Edit Vehicle Details\r\n     <hum-button [type]=\"'save'\" [text]=\"'save'\" (click)=\"saveVehicleDetail()\"></hum-button>\r\n    <hum-button [type]=\"'cancel'\" [text]=\"'cancel'\" (click)=\"backToDetails()\"></hum-button>\r\n  </span>\r\n  <div class=\"action_section\">\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-card>\r\n    <app-vehicle-detail [vehicleDetailForm]=\"vehicleDetailForm\"></app-vehicle-detail>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/store/components/edit-vehicle/edit-vehicle.component.scss":
/*!***************************************************************************!*\
  !*** ./src/app/store/components/edit-vehicle/edit-vehicle.component.scss ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvZWRpdC12ZWhpY2xlL2VkaXQtdmVoaWNsZS5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/store/components/edit-vehicle/edit-vehicle.component.ts":
/*!*************************************************************************!*\
  !*** ./src/app/store/components/edit-vehicle/edit-vehicle.component.ts ***!
  \*************************************************************************/
/*! exports provided: EditVehicleComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EditVehicleComponent", function() { return EditVehicleComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var EditVehicleComponent = /** @class */ (function () {
    function EditVehicleComponent(router, purchaseService, activatedRoute, fb, commonLoader) {
        var _this = this;
        this.router = router;
        this.purchaseService = purchaseService;
        this.activatedRoute = activatedRoute;
        this.fb = fb;
        this.commonLoader = commonLoader;
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5__["ReplaySubject"](1);
        this.activatedRoute.params.subscribe(function (params) {
            _this.vehicleId = params['id'];
        });
    }
    EditVehicleComponent.prototype.ngOnInit = function () {
        this.vehicleDetailForm = this.fb.group({
            'VehicleId': [null],
            'PlateNo': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            'EmployeeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            'StartingMileage': [null],
            'IncurredMileage': [null],
            'MobilOilConsumptionRate': [null],
            'ModelYear': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            'OfficeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            'FuelConsumptionRate': [null],
            'ManufacturerCountry': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            'EngineNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            'RegistrationNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            'ChasisNo': ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            'Remarks': [''],
        });
        this.getVehicleDetailById();
    };
    EditVehicleComponent.prototype.getVehicleDetailById = function () {
        var _this = this;
        this.purchaseService.getVehicleDetailById(this.vehicleId)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.vehicleDetailForm.setValue({
                VehicleId: x.VehicleId,
                PlateNo: x.PlateNo,
                EmployeeId: x.EmployeeId,
                StartingMileage: x.StartingMileage,
                IncurredMileage: x.IncurredMileage,
                MobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
                ModelYear: x.ModelYear,
                OfficeId: x.OfficeId,
                FuelConsumptionRate: x.StandardFuelConsumptionRate,
                ManufacturerCountry: x.ManufacturerCountry,
                EngineNo: x.EngineNo,
                RegistrationNo: x.RegistrationNo,
                ChasisNo: x.ChasisNo,
                Remarks: x.Remarks
            });
        });
    };
    EditVehicleComponent.prototype.saveVehicleDetail = function () {
        var _this = this;
        this.commonLoader.showLoader();
        if (this.vehicleDetailForm.valid) {
            this.purchaseService.saveVehicleDetail(this.vehicleDetailForm.value)
                .subscribe(function (x) {
                if (x) {
                    _this.backToDetails();
                    _this.commonLoader.hideLoader();
                }
            }, function (error) {
                _this.commonLoader.hideLoader();
            });
        }
    };
    EditVehicleComponent.prototype.backToDetails = function () {
        this.router.navigate(['store/vehicle/detail', this.vehicleId]);
    };
    EditVehicleComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    EditVehicleComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-edit-vehicle',
            template: __webpack_require__(/*! ./edit-vehicle.component.html */ "./src/app/store/components/edit-vehicle/edit-vehicle.component.html"),
            styles: [__webpack_require__(/*! ./edit-vehicle.component.scss */ "./src/app/store/components/edit-vehicle/edit-vehicle.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"], _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__["PurchaseService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"], _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormBuilder"], src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__["CommonLoaderService"]])
    ], EditVehicleComponent);
    return EditVehicleComponent;
}());



/***/ }),

/***/ "./src/app/store/components/entry-component/entry-component.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/entry-component/entry-component.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-sidenav-container class=\"example-container\">\r\n  <mat-sidenav #sidenav mode=\"side\" opened=\"true\">\r\n    <app-dbsidebar></app-dbsidebar>\r\n  </mat-sidenav>\r\n\r\n  <mat-sidenav-content>\r\n    <mat-card class=\"header header_mat_card\">\r\n   <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n          <div class=\"col-sm-12 col-xs-12\">\r\n            <app-dbheader></app-dbheader>\r\n          </div>\r\n        </div>\r\n   </div>\r\n    </mat-card>\r\n\r\n    <router-outlet></router-outlet>\r\n  </mat-sidenav-content>\r\n</mat-sidenav-container>\r\n"

/***/ }),

/***/ "./src/app/store/components/entry-component/entry-component.component.scss":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/entry-component/entry-component.component.scss ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".example-container {\n  position: absolute;\n  top: 0;\n  bottom: 0;\n  left: 0;\n  right: 0; }\n\n.header_mat_card {\n  height: 77px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9lbnRyeS1jb21wb25lbnQvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcc3RvcmVcXGNvbXBvbmVudHNcXGVudHJ5LWNvbXBvbmVudFxcZW50cnktY29tcG9uZW50LmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksa0JBQWtCO0VBQ2xCLE1BQU07RUFDTixTQUFTO0VBQ1QsT0FBTztFQUNQLFFBQVEsRUFBQTs7QUFFVjtFQUNFLFlBQVksRUFBQSIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvZW50cnktY29tcG9uZW50L2VudHJ5LWNvbXBvbmVudC5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5leGFtcGxlLWNvbnRhaW5lciB7XHJcbiAgICBwb3NpdGlvbjogYWJzb2x1dGU7XHJcbiAgICB0b3A6IDA7XHJcbiAgICBib3R0b206IDA7XHJcbiAgICBsZWZ0OiAwO1xyXG4gICAgcmlnaHQ6IDA7XHJcbiAgfVxyXG4gIC5oZWFkZXJfbWF0X2NhcmQge1xyXG4gICAgaGVpZ2h0OiA3N3B4O1xyXG4gIH1cclxuICAiXX0= */"

/***/ }),

/***/ "./src/app/store/components/entry-component/entry-component.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/store/components/entry-component/entry-component.component.ts ***!
  \*******************************************************************************/
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
        this.globalservice.setMenuHeaderName('Store');
    };
    EntryComponentComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-entry-component',
            template: __webpack_require__(/*! ./entry-component.component.html */ "./src/app/store/components/entry-component/entry-component.component.html"),
            styles: [__webpack_require__(/*! ./entry-component.component.scss */ "./src/app/store/components/entry-component/entry-component.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_1__["GlobalSharedService"]])
    ], EntryComponentComponent);
    return EntryComponentComponent;
}());



/***/ }),

/***/ "./src/app/store/components/generator-detail/generator-detail.component.html":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/generator-detail/generator-detail.component.html ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"row\">\r\n  <form [formGroup]=\"generatorDetailForm\">\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n        <div class=\"col-md-3\">\r\n            <mat-form-field class=\"example-full-width\">\r\n                <input matInput type='number' min=\"0\" oninput=\"validity.valid||(value='');\" formControlName='Voltage' placeholder=\"Voltage(KV)\">\r\n              </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field class=\"example-full-width\">\r\n            <input matInput type='number'  min=\"0\" oninput=\"validity.valid||(value='');\"formControlName=\"StartingUsage\" placeholder=\"Starting Usage(Hours)\">\r\n            </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n            <mat-form-field class=\"example-full-width\">\r\n                <input matInput type='number' min=\"0\" oninput=\"validity.valid||(value='');\" formControlName=\"IncurredUsage\" placeholder=\"Incurred Usage(Hours)\">\r\n              </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n            <mat-form-field class=\"example-full-width\">\r\n              <input matInput type='number' min=\"0\" oninput=\"validity.valid||(value='');\" formControlName=\"ModelYear\" placeholder=\"ModelYear\">\r\n              </mat-form-field>\r\n        </div>\r\n\r\n      </div>\r\n    </div>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field class=\"example-full-width\">\r\n            <input matInput type='number' min=\"0\" oninput=\"validity.valid||(value='');\" formControlName=\"FuelConsumptionRate\" placeholder=\"Standard Fuel Consumption Rate(litres/hr)\">\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n            <mat-form-field class=\"example-full-width\">\r\n                <input matInput type='number' min=\"0\" oninput=\"validity.valid||(value='');\" formControlName=\"MobilOilConsumptionRate\" placeholder=\"Standard Mobil Oil Consumption Rate(litres/hr)\">\r\n              </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <mat-form-field>\r\n            <mat-label>Location</mat-label>\r\n            <mat-select formControlName='OfficeId'>\r\n              <mat-option *ngFor=\"let office of offices$ | async\" [value]=\"office.value\">\r\n                {{office.name}}\r\n              </mat-option>\r\n            </mat-select>\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n            <mat-form-field>\r\n                <mat-label>Operator</mat-label>\r\n                <mat-select formControlName='EmployeeId'>\r\n                  <mat-option *ngFor=\"let employee of employeeList$ | async\" [value]=\"employee.value\">\r\n                    {{employee.name}}\r\n                  </mat-option>\r\n                </mat-select>\r\n              </mat-form-field>\r\n        </div>\r\n      </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n            <div class=\"col-md-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                    <input matInput formControlName='ManufacturerCountry' placeholder=\"Manufacturing Country\">\r\n                  </mat-form-field>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                    <input matInput formControlName='EngineNo' placeholder=\"Engine No\">\r\n                  </mat-form-field>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                    <input matInput formControlName='RegistrationNo' placeholder=\"Registration No\">\r\n                  </mat-form-field>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                    <input matInput formControlName='ChasisNo' placeholder=\"Chasis No\">\r\n                  </mat-form-field>\r\n            </div>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-6 padding_left_30\">\r\n            <mat-form-field class=\"example-full-width\">\r\n                <textarea formControlName='Remarks' matInput placeholder=\"Remarks\"></textarea>\r\n              </mat-form-field>\r\n        </div>\r\n      </div>\r\n  </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/store/components/generator-detail/generator-detail.component.scss":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/generator-detail/generator-detail.component.scss ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvZ2VuZXJhdG9yLWRldGFpbC9nZW5lcmF0b3ItZGV0YWlsLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/store/components/generator-detail/generator-detail.component.ts":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/generator-detail/generator-detail.component.ts ***!
  \*********************************************************************************/
/*! exports provided: GeneratorDetailComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GeneratorDetailComponent", function() { return GeneratorDetailComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var GeneratorDetailComponent = /** @class */ (function () {
    function GeneratorDetailComponent(purchaseService) {
        this.purchaseService = purchaseService;
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_1__["ReplaySubject"](1);
    }
    GeneratorDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.getAllOffice();
        // console.log(this.generatorDetailForm);
        this.generatorDetailForm.controls['OfficeId'].valueChanges.subscribe(function (x) {
            _this.getEmployeesByOfficeId(x);
        });
        this.markFormGroupTouched(this.generatorDetailForm);
        this.markFormGroupTouched(this.generatorDetailForm);
    };
    GeneratorDetailComponent.prototype.getAllOffice = function () {
        var _this = this;
        this.purchaseService.getAllOfficeList()
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.offices$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__["of"])(x.data.OfficeDetailsList.map(function (y) {
                return {
                    value: y.OfficeId,
                    name: y.OfficeCode + '-' + y.OfficeName
                };
            }));
        });
    };
    GeneratorDetailComponent.prototype.markFormGroupTouched = function (formGroup) {
        var _this = this;
        Object.values(formGroup.controls).forEach(function (control) {
            control.markAsTouched();
            if (control.controls) {
                _this.markFormGroupTouched(control);
            }
        });
    };
    GeneratorDetailComponent.prototype.getEmployeesByOfficeId = function (officeId) {
        var _this = this;
        this.purchaseService.getEmployeesByOfficeId(officeId)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.employeeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__["of"])(x.data.map(function (y) {
                return {
                    name: y.CodeEmployeeName,
                    value: y.EmployeeId
                };
            }));
        });
    };
    GeneratorDetailComponent.prototype.ngOnChanges = function () {
        if (this.officeId !== undefined && this.officeId != null) {
            this.getEmployeesByOfficeId(this.officeId);
        }
    };
    GeneratorDetailComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], GeneratorDetailComponent.prototype, "officeId", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormGroup"])
    ], GeneratorDetailComponent.prototype, "generatorDetailForm", void 0);
    GeneratorDetailComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-generator-detail',
            template: __webpack_require__(/*! ./generator-detail.component.html */ "./src/app/store/components/generator-detail/generator-detail.component.html"),
            styles: [__webpack_require__(/*! ./generator-detail.component.scss */ "./src/app/store/components/generator-detail/generator-detail.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_purchase_service__WEBPACK_IMPORTED_MODULE_3__["PurchaseService"]])
    ], GeneratorDetailComponent);
    return GeneratorDetailComponent;
}());



/***/ }),

/***/ "./src/app/store/components/generator-details/generator-details.component.html":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/generator-details/generator-details.component.html ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">\r\n    <a class=\"back_arrow\" routerLink=\"/store/generator/tracker\">\r\n      <i class=\"fas fa-arrow-left\"></i>\r\n    </a>\r\n    Generator Details\r\n    <hum-button [type]=\"'add'\" [text]=\"'ADD Hours'\" (click)=\"openHoursModal($event)\"></hum-button>\r\n    <hum-button [type]=\"'text'\" [text]=\"'Edit details'\" (click)=\"goToDetails()\"></hum-button>\r\n  </span>\r\n  <div class=\"action_section\">\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n<mat-tab-group [ngStyle]=\"scrollStyles\" (selectedTabChange)=\"onTabClick($event)\">\r\n  <mat-tab label=\"DETAILS\">\r\n    <ng-template matTabContent>\r\n      <ng-container *ngTemplateOutlet=\"generatorDetails\"></ng-container>\r\n    </ng-template>\r\n  </mat-tab>\r\n  <mat-tab label=\"MONTHLY BREAKDOWN\">\r\n    <div style=\"padding-left: 50px;\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-2\">\r\n          <!-- <lib-hum-dropdown [options]=\"monthlyBreakdownYearList$\" [formControl]=\"monthlyBreakdownYear\"\r\n                (change)=\"getMonthlyBreakdownYear($event)\" [placeHolder]=\"'Year'\">\r\n              </lib-hum-dropdown> -->\r\n          <div class=\"padding_top_10\">\r\n            <mat-form-field>\r\n              <mat-label>Year</mat-label>\r\n              <mat-select [(ngModel)]=\"monthlyBreakdownYear\" (selectionChange)='getGeneratorMonthlyBreakdownData()'>\r\n                <mat-option *ngFor=\"let item of monthlyBreakdownYearList$ | async\" [value]=\"item.value\">\r\n                  {{item.name}}\r\n                </mat-option>\r\n              </mat-select>\r\n            </mat-form-field>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card>\r\n            <p>Starting Usage:</p>\r\n            <h4>{{generatorMonthlyBreakdownList.StartingUsage}}</h4>\r\n          </mat-card>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card>\r\n            <p>Incurred Usage:</p>\r\n            <h4>{{generatorMonthlyBreakdownList.IncurredUsage}}</h4>\r\n          </mat-card>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card title=\"Standard Fuel Consumption Rate\">\r\n            <p>SFC Rate:</p>\r\n            <h4>{{generatorMonthlyBreakdownList.StandardFuelConsumptionRate}}</h4>\r\n          </mat-card>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card title=\"Standard Mobil Oil Consumption Rate\">\r\n            <p>SMOC Rate:</p>\r\n            <h4>{{generatorMonthlyBreakdownList.StandardMobilOilConsumptionRate}}</h4>\r\n          </mat-card>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card>\r\n            <p>Starting Cost:</p>\r\n            <h4>{{generatorMonthlyBreakdownList.StartingCost}}</h4>\r\n          </mat-card>\r\n        </div>\r\n      </div>\r\n    </div>\r\n\r\n    <mat-card style=\"margin-top: 5px;\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-12\">\r\n            <table class=\"table table-striped\">\r\n              <thead>\r\n                <th>\r\n                  <h5>Generator Usage Analysis</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jan</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Feb</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Mar</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Apr</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>May</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jun</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jul</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Aug</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Sept</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Oct</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Nov</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Dec</h5>\r\n                </th>\r\n              </thead>\r\n              <tbody>\r\n                <tr *ngFor=\"let item of generatorMonthlyBreakdownList.UsageAnalysisBreakDownList\">\r\n                  <td>{{item.Header}}</td>\r\n                  <td>{{item.January==0 ? '-' : item.January}}</td>\r\n                  <td>{{item.February==0 ? '-': item.February}}</td>\r\n                  <td>{{item.March==0 ? '-': item.March}}</td>\r\n                  <td>{{item.April==0? '-': item.April}}</td>\r\n                  <td>{{item.May==0? '-': item.May}}</td>\r\n                  <td>{{item.June==0? '-': item.June}}</td>\r\n                  <td>{{item.July==0? '-': item.July}}</td>\r\n                  <td>{{item.August==0? '-': item.August}}</td>\r\n                  <td>{{item.September==0? '-': item.September}}</td>\r\n                  <td>{{item.October==0? '-': item.October}}</td>\r\n                  <td>{{item.November==0? '-': item.November}}</td>\r\n                  <td>{{item.December==0? '-': item.December}}</td>\r\n                </tr>\r\n              </tbody>\r\n            </table>\r\n\r\n            <table class=\"table table-striped\">\r\n              <thead>\r\n                <th>\r\n                  <h5>Vehicle Cost Analysis</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jan</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Feb</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Mar</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Apr</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>May</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jun</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jul</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Aug</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Sept</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Oct</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Nov</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Dec</h5>\r\n                </th>\r\n              </thead>\r\n              <tbody>\r\n                <tr *ngFor=\"let item of generatorMonthlyBreakdownList.CostAnalysisBreakDownList\">\r\n                  <td>{{item.Header}}</td>\r\n                  <td>{{item.January==0 ? '-' : item.January}}</td>\r\n                  <td>{{item.February==0 ? '-': item.February}}</td>\r\n                  <td>{{item.March==0 ? '-': item.March}}</td>\r\n                  <td>{{item.April==0? '-': item.April}}</td>\r\n                  <td>{{item.May==0? '-': item.May}}</td>\r\n                  <td>{{item.June==0? '-': item.June}}</td>\r\n                  <td>{{item.July==0? '-': item.July}}</td>\r\n                  <td>{{item.August==0? '-': item.August}}</td>\r\n                  <td>{{item.September==0? '-': item.September}}</td>\r\n                  <td>{{item.October==0? '-': item.October}}</td>\r\n                  <td>{{item.November==0? '-': item.November}}</td>\r\n                  <td>{{item.December==0? '-': item.December}}</td>\r\n                </tr>\r\n              </tbody>\r\n            </table>\r\n\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </mat-card>\r\n\r\n  </mat-tab>\r\n  <mat-tab label=\"LOGS\">\r\n    <ng-template matTabContent>\r\n      <app-logs [transportType]=\"transportType\" [entityId]=\"generatorId\"></app-logs>\r\n    </ng-template>\r\n  </mat-tab>\r\n</mat-tab-group>\r\n<!-- Generator Details template-->\r\n<ng-template #generatorDetails>\r\n  <mat-card>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n        <div class=\"col-md-6\">\r\n          <h5>Generator Usage Analysis</h5>\r\n          <table class=\"table table-striped\">\r\n            <tbody>\r\n              <tr>\r\n                <td>Incurred Usage(Hours)</td>\r\n                <td>{{generatorDetailForm.IncurredUsage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Starting Usage(Hours)</td>\r\n                <td>{{generatorDetailForm.StartingUsage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Current Usage(Hours)</td>\r\n                <td>{{generatorDetailForm.CurrentUsage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Total Fuel Usage(Liter)</td>\r\n                <td>{{generatorDetailForm.TotalFuelUsage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Total Mobil Oil Usage(Liter)</td>\r\n                <td>{{generatorDetailForm.TotalMobilOilUsage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Standard Fuel Consumption Rate(Liter Per Hour)</td>\r\n                <td>{{generatorDetailForm.StandardFuelConsumptionRate}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Actual Fuel Consumption Rate(Liter Per Hour)</td>\r\n                <td>{{generatorDetailForm.ActualFuelConsumptionRate}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Standard Mobil Oil Consumption Rate(Liter Per Hour)</td>\r\n                <td>{{generatorDetailForm.StandardMobilOilConsumptionRate}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Actual Mobil Oil Consumption Rate(Liter Per Hour)</td>\r\n                <td>{{generatorDetailForm.ActualMobilOilConsumptionRate}}</td>\r\n              </tr>\r\n            </tbody>\r\n          </table>\r\n          <h5>Generator Cost Analysis</h5>\r\n          <table class=\"table table-striped\">\r\n            <tbody>\r\n              <tr>\r\n                <td>Total Fuel Cost</td>\r\n                <td>{{generatorDetailForm.FuelTotalCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Mobil Oil Total Cost</td>\r\n                <td>{{generatorDetailForm.MobilOilTotalCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Spare Parts Total Cost</td>\r\n                <td>{{generatorDetailForm.SparePartsTotalCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Services & Maintenance Total Cost</td>\r\n                <td>{{generatorDetailForm.ServicesAndMaintenanceTotalCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Generator Starting Cost</td>\r\n                <td>{{generatorDetailForm.GeneratorStartingCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Total Cost</td>\r\n                <td>{{generatorDetailForm.FuelTotalCost+\r\n                    generatorDetailForm.MobilOilTotalCost+\r\n                    generatorDetailForm.SparePartsTotalCost+\r\n                    generatorDetailForm.ServicesAndMaintenanceTotalCost+\r\n                    generatorDetailForm.GeneratorStartingCost\r\n                }}</td>\r\n              </tr>\r\n            </tbody>\r\n          </table>\r\n\r\n        </div>\r\n        <div class=\"col-md-6\">\r\n          <h5>Generator Details</h5>\r\n          <table class=\"table table-striped\">\r\n            <tbody>\r\n              <tr>\r\n                <td>KV</td>\r\n                <td>{{generatorDetailForm.Voltage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Employee Name</td>\r\n                <td>{{generatorDetailForm.PurchasedBy}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Model Year</td>\r\n                <td>{{generatorDetailForm.ModelYear}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Location</td>\r\n                <td>{{generatorDetailForm.OfficeName}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Purchase Id</td>\r\n                <td><a *ngIf=\"generatorDetailForm && generatorDetailForm.PurchaseId != null\"\r\n                    [routerLink]=\"['/store/purchase/edit', generatorDetailForm?.PurchaseId]\">{{generatorDetailForm.PurchaseName}}</a>\r\n                </td>\r\n              </tr>\r\n            </tbody>\r\n          </table>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </mat-card>\r\n\r\n</ng-template>\r\n"

/***/ }),

/***/ "./src/app/store/components/generator-details/generator-details.component.scss":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/generator-details/generator-details.component.scss ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvZ2VuZXJhdG9yLWRldGFpbHMvZ2VuZXJhdG9yLWRldGFpbHMuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/store/components/generator-details/generator-details.component.ts":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/generator-details/generator-details.component.ts ***!
  \***********************************************************************************/
/*! exports provided: GeneratorDetailsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GeneratorDetailsComponent", function() { return GeneratorDetailsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _add_hours_add_hours_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../add-hours/add-hours.component */ "./src/app/store/components/add-hours/add-hours.component.ts");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};









var GeneratorDetailsComponent = /** @class */ (function () {
    function GeneratorDetailsComponent(dialog, router, activatedRoute, purchaseService, commonLoader) {
        var _this = this;
        this.dialog = dialog;
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.purchaseService = purchaseService;
        this.commonLoader = commonLoader;
        this.transportType = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_8__["TransportItemCategory"].Generator;
        // subject
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_6__["ReplaySubject"](1);
        this.activatedRoute.params.subscribe(function (params) {
            _this.generatorId = params['id'];
        });
    }
    GeneratorDetailsComponent.prototype.ngOnInit = function () {
        this.getScreenSize();
        this.initForm();
        this.getGeneratorDetailById();
        this.getMonthlyBreakDownYears();
    };
    GeneratorDetailsComponent.prototype.initForm = function () {
        this.generatorDetailForm = {
            GeneratorId: null,
            Voltage: null,
            StartingUsage: null,
            IncurredUsage: null,
            StandardMobilOilConsumptionRate: null,
            ModelYear: null,
            OfficeId: null,
            StandardFuelConsumptionRate: null,
            PurchaseName: null,
            PurchaseId: null,
            OfficeName: null,
            PurchasedBy: null,
            TotalFuelUsage: null,
            TotalMobilOilUsage: null,
            FuelTotalCost: null,
            MobilOilTotalCost: null,
            SparePartsTotalCost: null,
            ServicesAndMaintenanceTotalCost: null,
            CurrentUsage: null,
            GeneratorStartingCost: null,
            ActualFuelConsumptionRate: null,
            ActualMobilOilConsumptionRate: null
        };
        this.generatorMonthlyBreakdownList = {
            StartingMileage: null,
            IncurredMileage: null,
            StartingUsage: null,
            IncurredUsage: null,
            StandardMobilOilConsumptionRate: null,
            StandardFuelConsumptionRate: null,
            StartingCost: null,
            CostAnalysisBreakDownList: [],
            UsageAnalysisBreakDownList: []
        };
    };
    //#region "Dynamic Scroll"
    GeneratorDetailsComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    GeneratorDetailsComponent.prototype.getGeneratorDetailById = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.purchaseService.getGeneratorDetailById(this.generatorId)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_5__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.generatorDetailForm = {
                GeneratorId: x.GeneratorId,
                Voltage: x.Voltage,
                StartingUsage: x.StartingUsage,
                IncurredUsage: x.IncurredUsage,
                StandardMobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
                ModelYear: x.ModelYear,
                OfficeId: x.OfficeId,
                StandardFuelConsumptionRate: x.StandardFuelConsumptionRate,
                PurchaseName: x.PurchaseName,
                PurchaseId: x.PurchaseId,
                OfficeName: x.OfficeName,
                PurchasedBy: x.PurchasedBy,
                TotalFuelUsage: x.TotalFuelUsage,
                TotalMobilOilUsage: x.TotalMobilOilUsage,
                FuelTotalCost: x.FuelTotalCost,
                MobilOilTotalCost: x.MobilOilTotalCost,
                SparePartsTotalCost: x.SparePartsTotalCost,
                ServicesAndMaintenanceTotalCost: x.ServicesAndMaintenanceTotalCost,
                CurrentUsage: x.CurrentUsage,
                GeneratorStartingCost: x.GeneratorStartingCost,
                ActualFuelConsumptionRate: x.ActualFuelConsumptionRate,
                ActualMobilOilConsumptionRate: x.ActualMobilOilConsumptionRate
            };
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    GeneratorDetailsComponent.prototype.openHoursModal = function (event) {
        var _this = this;
        var dialogRef = this.dialog.open(_add_hours_add_hours_component__WEBPACK_IMPORTED_MODULE_3__["AddHoursComponent"], {
            width: '850px',
            data: {
                generatorId: this.generatorId
            }
        });
        dialogRef.afterClosed().subscribe(function (result) {
            _this.getGeneratorDetailById();
            _this.getGeneratorMonthlyBreakdownData();
        });
    };
    GeneratorDetailsComponent.prototype.onTabClick = function (event) {
        if (event.index === 1) {
            this.getGeneratorMonthlyBreakdownData();
        }
        else if (event.index === 0) {
            this.getGeneratorDetailById();
        }
    };
    GeneratorDetailsComponent.prototype.getGeneratorMonthlyBreakdownData = function () {
        var _this = this;
        var data = {
            GeneratorId: +this.generatorId,
            SelectedYear: this.monthlyBreakdownYear
        };
        this.purchaseService.getGeneratorMonthlyBreakdown(data)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_5__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.generatorMonthlyBreakdownList = {
                StartingUsage: x.StartingUsage,
                IncurredUsage: x.IncurredUsage,
                StandardMobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
                StandardFuelConsumptionRate: x.StandardFuelConsumptionRate,
                StartingCost: x.StartingCost,
                CostAnalysisBreakDownList: x.CostAnalysisBreakDownList,
                UsageAnalysisBreakDownList: x.UsageAnalysisBreakDownList
            };
        });
    };
    GeneratorDetailsComponent.prototype.getMonthlyBreakDownYears = function () {
        var _this = this;
        this.monthlyBreakdownYearList$ = this.purchaseService.getPreviousYearsList(10);
        this.monthlyBreakdownYearList$.subscribe(function (x) {
            _this.monthlyBreakdownYear = x[0].value;
        });
    };
    GeneratorDetailsComponent.prototype.goToDetails = function () {
        this.router.navigate(['store/generator/edit', this.generatorId]);
    };
    GeneratorDetailsComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], GeneratorDetailsComponent.prototype, "getScreenSize", null);
    GeneratorDetailsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-generator-details',
            template: __webpack_require__(/*! ./generator-details.component.html */ "./src/app/store/components/generator-details/generator-details.component.html"),
            styles: [__webpack_require__(/*! ./generator-details.component.scss */ "./src/app/store/components/generator-details/generator-details.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialog"], _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"], _angular_router__WEBPACK_IMPORTED_MODULE_2__["ActivatedRoute"],
            _services_purchase_service__WEBPACK_IMPORTED_MODULE_4__["PurchaseService"], src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_7__["CommonLoaderService"]])
    ], GeneratorDetailsComponent);
    return GeneratorDetailsComponent;
}());



/***/ }),

/***/ "./src/app/store/components/generator-filters/generator-filters.component.html":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/generator-filters/generator-filters.component.html ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"row\">\r\n  <div class=\"col-md-12\">\r\n    <span class=\"pull-left\">\r\n      <h4>Filters</h4>\r\n    </span>\r\n    <span class=\"pull-right\">\r\n      <!-- <hum-button [type]=\"'filter'\" [text]=\"'advance filters'\" (click)=\"isBasic=!isBasic\"></hum-button> -->\r\n      <hum-button [type]=\"'clear'\" [text]=\"'clear filters'\" (click)=\"clearFilters()\"></hum-button>\r\n    </span>\r\n  </div>\r\n</div>\r\n<div class=\"row\">\r\n  <form [formGroup]= \"generatorTrackerFilterForm\">\r\n  <div class=\"col-md-12\">\r\n    <div class=\"col-md-3\">\r\n        <mat-form-field class=\"example-full-width\">\r\n            <input matInput type=\"number\" formControlName=\"Voltage\" placeholder=\"KV\">\r\n          </mat-form-field>\r\n    </div>\r\n    <div class=\"col-md-3\">\r\n        <lib-hum-dropdown [options]=\"officeList$\" formControlName=\"OfficeId\" [placeHolder]=\"'Office'\"></lib-hum-dropdown>\r\n    </div>\r\n    <div class=\"col-md-3\">\r\n      <mat-form-field class=\"example-full-width\">\r\n        <input matInput type=\"number\" maxlength=\"4\" formControlName=\"ModelYear\" placeholder=\"Model Year\">\r\n      </mat-form-field>\r\n  </div>\r\n    <div class=\"col-md-3\">\r\n        <mat-form-field class=\"example-full-width\">\r\n            <input matInput type=\"number\" placeholder=\"Total Cost\" formControlName=\"TotalCost\">\r\n        </mat-form-field>\r\n    </div>\r\n  </div>\r\n  </form>\r\n  </div>\r\n  <div class=\"row\">\r\n    <div class=\"col-md-10\"></div>\r\n      <div class=\"col-md-2\">\r\n          <hum-button [type]=\"'filter'\" [text]=\"'Apply Filter'\" (click)=\"onApplyFilters()\"></hum-button>\r\n      </div>\r\n  </div>\r\n"

/***/ }),

/***/ "./src/app/store/components/generator-filters/generator-filters.component.scss":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/generator-filters/generator-filters.component.scss ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvZ2VuZXJhdG9yLWZpbHRlcnMvZ2VuZXJhdG9yLWZpbHRlcnMuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/store/components/generator-filters/generator-filters.component.ts":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/generator-filters/generator-filters.component.ts ***!
  \***********************************************************************************/
/*! exports provided: GeneratorFiltersComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GeneratorFiltersComponent", function() { return GeneratorFiltersComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var GeneratorFiltersComponent = /** @class */ (function () {
    function GeneratorFiltersComponent(_fb, purchaseService) {
        this._fb = _fb;
        this.purchaseService = purchaseService;
        this.isBasic = true;
        this.applyFilterEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_2__["ReplaySubject"](1);
        this.generatorTrackerFilterForm = this._fb.group({
            'Voltage': [null],
            'OfficeId': [null],
            'ModelYear': [null],
            'TotalCost': [null]
        });
    }
    GeneratorFiltersComponent.prototype.ngOnInit = function () {
        this.getAllOffice();
    };
    GeneratorFiltersComponent.prototype.getAllOffice = function () {
        var _this = this;
        this.purchaseService.getAllOfficeList()
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.officeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__["of"])(response.data.OfficeDetailsList.map(function (y) {
                return {
                    value: y.OfficeId,
                    name: y.OfficeCode + '-' + y.OfficeName
                };
            }));
        });
    };
    GeneratorFiltersComponent.prototype.onApplyFilters = function () {
        this.applyFilterEvent.emit(this.generatorTrackerFilterForm.value);
    };
    GeneratorFiltersComponent.prototype.clearFilters = function () {
        this.generatorTrackerFilterForm.reset();
    };
    GeneratorFiltersComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], GeneratorFiltersComponent.prototype, "applyFilterEvent", void 0);
    GeneratorFiltersComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-generator-filters',
            template: __webpack_require__(/*! ./generator-filters.component.html */ "./src/app/store/components/generator-filters/generator-filters.component.html"),
            styles: [__webpack_require__(/*! ./generator-filters.component.scss */ "./src/app/store/components/generator-filters/generator-filters.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _services_purchase_service__WEBPACK_IMPORTED_MODULE_3__["PurchaseService"]])
    ], GeneratorFiltersComponent);
    return GeneratorFiltersComponent;
}());



/***/ }),

/***/ "./src/app/store/components/generator-tracker/generator-tracker.component.html":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/generator-tracker/generator-tracker.component.html ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n    <span class=\"action_header\">Generator Tracker\r\n      <!-- <hum-button [type]=\"'down'\" [text]=\"'DISPLAY CURRENCY'\"></hum-button> -->\r\n    </span>\r\n    <div class=\"action_section\">\r\n    </div>\r\n  </lib-sub-header-template>\r\n  <mat-divider></mat-divider>\r\n  <mat-card [ngStyle]=\"scrollStyles\">\r\n    <div style=\"padding-left: 50px;\">\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n          <mat-form-field>\r\n              <mat-label>Display Currency</mat-label>\r\n              <mat-select [(ngModel)]=\"selectedDisplayCurrency\" (selectionChange)='selectedDisplayCurrencyChanged()'>\r\n                <mat-option *ngFor=\"let currency of currencyList$ | async\" [value]=\"currency.value\">\r\n                  {{currency.name}}\r\n                </mat-option>\r\n              </mat-select>\r\n            </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <mat-card [ngStyle]=\"scrollStyles\">\r\n    <div class=\"container\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <app-generator-filters (applyFilterEvent)='getFilteredGeneratorList($event)'></app-generator-filters>\r\n          <mat-divider></mat-divider>\r\n          <h4>Generators</h4>\r\n          <hum-table [headers]=\"generatorListHeaders$\" [items]=\"generatorList$\"\r\n          (actionClick)=\"openHoursModal($event)\" [actions]=\"actions\" (rowClick)=\"goToDetails($event)\"></hum-table>\r\n        <mat-paginator [length]=\"recordsCount\" [pageSize]=\"generatorTrackerFilter.pageSize\"\r\n          [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n        </mat-paginator>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </mat-card>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/store/components/generator-tracker/generator-tracker.component.scss":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/generator-tracker/generator-tracker.component.scss ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvZ2VuZXJhdG9yLXRyYWNrZXIvZ2VuZXJhdG9yLXRyYWNrZXIuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/store/components/generator-tracker/generator-tracker.component.ts":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/generator-tracker/generator-tracker.component.ts ***!
  \***********************************************************************************/
/*! exports provided: GeneratorTrackerComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GeneratorTrackerComponent", function() { return GeneratorTrackerComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _add_hours_add_hours_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../add-hours/add-hours.component */ "./src/app/store/components/add-hours/add-hours.component.ts");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var GeneratorTrackerComponent = /** @class */ (function () {
    function GeneratorTrackerComponent(router, dialog, purchaseService) {
        this.router = router;
        this.dialog = dialog;
        this.purchaseService = purchaseService;
        this.generatorListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'K.V.', 'Fuel Consumption Rate', 'Incurred Usage(Hours)', 'Total Usage(Hours)', 'Total Cost',
            'Original Cost']);
    }
    GeneratorTrackerComponent.prototype.ngOnInit = function () {
        this.actions = {
            items: {
                button: { status: true, text: 'Add Hours' },
                delete: false,
                download: false,
            },
            subitems: {}
        };
        this.initializeModel();
        this.getScreenSize();
        this.getAllCurrencies();
    };
    GeneratorTrackerComponent.prototype.initializeModel = function () {
        this.generatorTrackerFilter = {
            Voltage: null,
            OfficeId: null,
            ModelYear: null,
            TotalCost: null,
            DisplayCurrency: null,
            pageIndex: 0,
            pageSize: 10
        };
    };
    //#region "Dynamic Scroll"
    GeneratorTrackerComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    GeneratorTrackerComponent.prototype.goToDetails = function (e) {
        this.router.navigate(['store/generator/detail', e.GeneratorId]);
    };
    GeneratorTrackerComponent.prototype.openHoursModal = function (event) {
        if (event.type === 'button') {
            var dialogRef = this.dialog.open(_add_hours_add_hours_component__WEBPACK_IMPORTED_MODULE_4__["AddHoursComponent"], {
                width: '850px',
                data: {
                    generatorId: event.item.GeneratorId,
                }
            });
        }
    };
    GeneratorTrackerComponent.prototype.getFilteredGeneratorList = function (selectedFilter) {
        this.generatorTrackerFilter = {
            TotalCost: selectedFilter.TotalCost,
            ModelYear: selectedFilter.ModelYear,
            OfficeId: selectedFilter.OfficeId,
            Voltage: selectedFilter.Voltage,
            DisplayCurrency: this.selectedDisplayCurrency,
            pageSize: 10,
            pageIndex: 0
        };
        this.getGeneratorList(this.generatorTrackerFilter);
    };
    GeneratorTrackerComponent.prototype.getGeneratorList = function (filter) {
        var _this = this;
        this.purchaseService.getGeneratorList(filter)
            .subscribe(function (response) {
            _this.recordsCount = response.TotalRecords;
            _this.generatorList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.GeneratorTrackerList.map(function (element) {
                return {
                    GeneratorId: element.GeneratorId,
                    Voltage: element.Voltage,
                    FCRate: element.FuelConsumptionRate,
                    IncurredUsage: element.IncurredUsage,
                    TotalUsage: element.TotalUsage,
                    TotalCost: element.TotalCost,
                    OriginalCost: element.OriginalCost,
                };
            }));
        });
    };
    GeneratorTrackerComponent.prototype.getAllCurrencies = function () {
        var _this = this;
        this.purchaseService.getAllCurrencies()
            .subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.selectedDisplayCurrency = x.data.CurrencyList[0].CurrencyId;
                _this.currencyList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(x.data.CurrencyList.map(function (y) {
                    return {
                        name: y.CurrencyCode + '-' + y.CurrencyName,
                        value: y.CurrencyId
                    };
                }));
                _this.getGeneratorList(_this.generatorTrackerFilter);
            }
        }, function (error) {
            console.error(error);
        });
    };
    GeneratorTrackerComponent.prototype.selectedDisplayCurrencyChanged = function () {
        this.generatorTrackerFilter.DisplayCurrency = this.selectedDisplayCurrency;
        this.getGeneratorList(this.generatorTrackerFilter);
    };
    //#region "pageEvent"
    GeneratorTrackerComponent.prototype.pageEvent = function (e) {
        this.generatorTrackerFilter.pageIndex = e.pageIndex;
        this.generatorTrackerFilter.pageSize = e.pageSize;
        this.getGeneratorList(this.generatorTrackerFilter);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], GeneratorTrackerComponent.prototype, "getScreenSize", null);
    GeneratorTrackerComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-generator-tracker',
            template: __webpack_require__(/*! ./generator-tracker.component.html */ "./src/app/store/components/generator-tracker/generator-tracker.component.html"),
            styles: [__webpack_require__(/*! ./generator-tracker.component.scss */ "./src/app/store/components/generator-tracker/generator-tracker.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"], _angular_material__WEBPACK_IMPORTED_MODULE_3__["MatDialog"], _services_purchase_service__WEBPACK_IMPORTED_MODULE_5__["PurchaseService"]])
    ], GeneratorTrackerComponent);
    return GeneratorTrackerComponent;
}());



/***/ }),

/***/ "./src/app/store/components/logs/logs.component.html":
/*!***********************************************************!*\
  !*** ./src/app/store/components/logs/logs.component.html ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-card>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <hum-table [headers]=\"logListHeaders$\" [items]=\"logList$\"></hum-table>\r\n        </div>\r\n      </div>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/store/components/logs/logs.component.scss":
/*!***********************************************************!*\
  !*** ./src/app/store/components/logs/logs.component.scss ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvbG9ncy9sb2dzLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/store/components/logs/logs.component.ts":
/*!*********************************************************!*\
  !*** ./src/app/store/components/logs/logs.component.ts ***!
  \*********************************************************/
/*! exports provided: LogsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LogsComponent", function() { return LogsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_3__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var LogsComponent = /** @class */ (function () {
    function LogsComponent(purchaseService) {
        this.purchaseService = purchaseService;
        this.logListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(['Event Type', 'By', 'Event On', 'Detail']);
        // subject
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_1__["ReplaySubject"](1);
    }
    LogsComponent.prototype.ngOnInit = function () {
        this.getLogs();
    };
    LogsComponent.prototype.getLogs = function () {
        var _this = this;
        this.purchaseService.getStoreLogs(this.transportType, this.entityId)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_3__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            if (x !== undefined && x.length > 0) {
                _this.logList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(x.map(function (y) {
                    return {
                        EventType: y.EventType,
                        EventBy: y.EventBy,
                        EventOn: y.EventOn,
                        Detail: y.LogText + (y.PurchaseId ? '<a href=store/purchase/edit/' + y.PurchaseId
                            + '>' + y.PurchaseName + '</a>' : ''),
                    };
                }));
            }
        });
    };
    LogsComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], LogsComponent.prototype, "transportType", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], LogsComponent.prototype, "entityId", void 0);
    LogsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-logs',
            template: __webpack_require__(/*! ./logs.component.html */ "./src/app/store/components/logs/logs.component.html"),
            styles: [__webpack_require__(/*! ./logs.component.scss */ "./src/app/store/components/logs/logs.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_purchase_service__WEBPACK_IMPORTED_MODULE_2__["PurchaseService"]])
    ], LogsComponent);
    return LogsComponent;
}());



/***/ }),

/***/ "./src/app/store/components/procurement-control-panel/procurement-control-panel.component.html":
/*!*****************************************************************************************************!*\
  !*** ./src/app/store/components/procurement-control-panel/procurement-control-panel.component.html ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\"> <i (click)=\"goToListingPage()\" class=\"fas fa-arrow-left\"></i> &nbsp; &nbsp; Procurement Control Panel\r\n    <hum-button [type]=\"'edit'\" [text]=\"'EDIT'\" (click)=\"editProcurement()\"></hum-button>\r\n    <hum-button [type]=\"'cancel'\" [text]=\"'CANCEL PROCUREMENT'\" (click)=\"cancelProcurement()\"></hum-button>\r\n  </span>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n  <div class=\"container\" humAddScroll [height]=\"150\">\r\n    <h4>Procurement Details</h4>\r\n    <br>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-6\">\r\n        <table class=\"table table-striped\">\r\n          <tr>\r\n            <td><b>Id</b></td>\r\n            <td>{{procurementDetail.Id}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Purchase Id</b></td>\r\n            <td>{{procurementDetail.PurchaseId}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Starting Balance/Quantity</b></td>\r\n            <td>{{procurementDetail.StartingBalance}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Current Balance/Quantity</b></td>\r\n            <td>{{procurementDetail.CurrentBalance}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Must Return</b></td>\r\n            <td>{{procurementDetail.MustReturn? 'Yes': 'No'}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Status</b></td>\r\n            <td>{{procurementDetail.Status}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Date</b></td>\r\n            <td>{{procurementDetail.Date}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Item Code</b></td>\r\n            <td>{{procurementDetail.ItemCode}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Project</b></td>\r\n            <td>{{procurementDetail.Project}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Voucher</b></td>\r\n            <td>{{procurementDetail.Voucher}}</td>\r\n          </tr>\r\n          <tr>\r\n            <td><b>Issued To Employee</b></td>\r\n            <td>{{procurementDetail.IssuedToEmployee}}</td>\r\n          </tr>\r\n        </table>\r\n      </div>\r\n    </div>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-1\">\r\n        <h4>Returns</h4>\r\n      </div>\r\n      <div class=\"col-md-11\">\r\n        <hum-button *ngIf=\"showAddReturnsButton\" [type]=\"'add'\" [text]=\"'ADD RETURNS'\" (click)=\"addReturns()\"></hum-button>\r\n      </div>\r\n    </div>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-6\">\r\n        <hum-table [headers]=\"returnListHeaders$\" [items]=\"returnList\" (actionClick)=\"actionEvents($event)\"\r\n          [actions]=\"actions\" [hideColums$]=\"hideColums\"></hum-table>\r\n      </div>\r\n    </div>\r\n  </div>\r\n\r\n\r\n<ng-template #unittype>\r\n  <h1 mat-dialog-title> Add Returns</h1>\r\n  <form [formGroup]=\"addReturnsForm\" (ngSubmit)=\"saveAddReturns()\">\r\n  <div mat-dialog-content>\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-6\">\r\n          <mat-form-field>\r\n            <input matInput formControlName=\"Date\" [matDatepicker]=\"picker\" placeholder=\"Choose a date\">\r\n            <mat-datepicker-toggle matSuffix [for]=\"picker\"></mat-datepicker-toggle>\r\n            <mat-datepicker #picker></mat-datepicker>\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n          <mat-form-field>\r\n            <input type=\"number\" matInput formControlName=\"Quantity\" placeholder=\"Quantity\">\r\n            <mat-error *ngIf=\"addReturnsForm.controls['Quantity'].hasError('max')\">Max return quantity allowed is {{procurementDetail.CurrentBalance}}</mat-error>\r\n            <mat-error *ngIf=\"addReturnsForm.controls['Quantity'].hasError('min')\">Return quantity should be above 0</mat-error>\r\n            <mat-error>Required</mat-error>\r\n          </mat-form-field>\r\n        </div>\r\n      </div>\r\n  </div>\r\n  <div mat-dialog-actions>\r\n    <hum-button [disabled]=\"!addReturnsForm.valid || !addReturnsForm.dirty\" [type]=\"'save'\" [isSubmit]=\"true\" [text]=\"'Save'\"></hum-button>\r\n  </div>\r\n</form>\r\n</ng-template>\r\n"

/***/ }),

/***/ "./src/app/store/components/procurement-control-panel/procurement-control-panel.component.scss":
/*!*****************************************************************************************************!*\
  !*** ./src/app/store/components/procurement-control-panel/procurement-control-panel.component.scss ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvcHJvY3VyZW1lbnQtY29udHJvbC1wYW5lbC9wcm9jdXJlbWVudC1jb250cm9sLXBhbmVsLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/store/components/procurement-control-panel/procurement-control-panel.component.ts":
/*!***************************************************************************************************!*\
  !*** ./src/app/store/components/procurement-control-panel/procurement-control-panel.component.ts ***!
  \***************************************************************************************************/
/*! exports provided: ProcurementControlPanelComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProcurementControlPanelComponent", function() { return ProcurementControlPanelComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
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








var ProcurementControlPanelComponent = /** @class */ (function () {
    function ProcurementControlPanelComponent(router, routeActive, purchaseService, dialog, fb, toastr) {
        var _this = this;
        this.router = router;
        this.routeActive = routeActive;
        this.purchaseService = purchaseService;
        this.dialog = dialog;
        this.fb = fb;
        this.toastr = toastr;
        this.returnListHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(['Id', 'Date', 'Quantity']);
        this.showAddReturnsButton = true;
        this.routeActive.queryParams.subscribe(function (params) {
            _this.quantity = +params['quantity'];
        });
    }
    ProcurementControlPanelComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.onInItForm();
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
                download: false,
                edit: false
            }
        };
        this.routeActive.params.subscribe(function (params) {
            _this.procurementId = +params['id'];
        });
        this.hideColums = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])({ headers: ['Date', 'Quantity'], items: ['Date', 'ReturnedQuantity'] });
        this.getProcurementDetails();
    };
    ProcurementControlPanelComponent.prototype.onReturnFormInIt = function () {
        this.addReturnsForm = this.fb.group({
            'Date': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required],
            'Quantity': [0, [_angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].min(1), _angular_forms__WEBPACK_IMPORTED_MODULE_5__["Validators"].max(this.procurementDetail.CurrentBalance)]]
        });
        this.addReturnsForm.controls['Quantity'].markAsTouched();
    };
    ProcurementControlPanelComponent.prototype.onInItForm = function () {
        this.procurementDetail = {
            CurrentBalance: null,
            Date: null,
            Id: null,
            IssuedToEmployee: null,
            ItemCode: null,
            MustReturn: null,
            Project: null,
            PurchaseId: null,
            ReturnedQuantity: null,
            StartingBalance: null,
            Status: null,
            Voucher: null,
        };
        this.onReturnFormInIt();
    };
    ProcurementControlPanelComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.purchaseService.deleteReturnProcurement(event.item.Id).subscribe(function (x) {
                if (x) {
                    _this.getProcurementDetails();
                }
            });
        }
    };
    ProcurementControlPanelComponent.prototype.addReturns = function () {
        this.onReturnFormInIt();
        this.openAddReturns();
    };
    ProcurementControlPanelComponent.prototype.goToListingPage = function () {
        this.router.navigate(['store/purchases']);
    };
    ProcurementControlPanelComponent.prototype.openAddReturns = function () {
        this.dialog.open(this.dialogRef, {
            width: '600px'
        });
    };
    ProcurementControlPanelComponent.prototype.saveAddReturns = function () {
        var _this = this;
        if (!this.addReturnsForm.valid) {
            this.toastr.warning('Please correct form errors and submit again');
        }
        var model = {
            PurchaseId: this.procurementDetail.PurchaseId,
            ProcurementId: this.procurementDetail.Id,
            ReturnedDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_6__["StaticUtilities"].getLocalDate(this.addReturnsForm.value.Date),
            ReturnedQuantity: this.addReturnsForm.value.Quantity
        };
        this.purchaseService.addProcurementReturn(model).subscribe(function (x) {
            _this.dialog.closeAll();
            _this.getProcurementDetails();
        });
    };
    ProcurementControlPanelComponent.prototype.cancelProcurement = function () {
        var _this = this;
        this.purchaseService.deleteProcurement(this.procurementId)
            .subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.goToListingPage();
                _this.toastr.success(x.Message);
            }
            else {
                _this.toastr.warning(x.Message);
            }
        }, function (error) {
            _this.toastr.error(error);
        });
    };
    ProcurementControlPanelComponent.prototype.editProcurement = function () {
        this.router.navigate(['store/purchases/edit-procurement/' + this.procurementId]);
    };
    ProcurementControlPanelComponent.prototype.getProcurementDetails = function () {
        var _this = this;
        if (this.procurementId) {
            this.purchaseService.getProcurementDetailWithReturnsList(this.procurementId)
                .subscribe(function (x) {
                if (x && x.ProcurementDetail) {
                    _this.procurementDetail.Id = x.ProcurementDetail.Id;
                    _this.procurementDetail.Date = x.ProcurementDetail.Date;
                    _this.procurementDetail.ItemCode = x.ProcurementDetail.ItemCode;
                    _this.procurementDetail.PurchaseId = x.ProcurementDetail.PurchaseId;
                    _this.procurementDetail.Status = x.ProcurementDetail.Status;
                    _this.procurementDetail.Voucher = x.ProcurementDetail.VoucherNo;
                    _this.procurementDetail.IssuedToEmployee = x.ProcurementDetail.EmployeeName;
                    _this.procurementDetail.StartingBalance = x.ProcurementDetail.StartingBalance;
                    _this.procurementDetail.CurrentBalance = x.ProcurementDetail.CurrentBalance;
                    _this.procurementDetail.Project = x.ProcurementDetail.ProjectName;
                    _this.procurementDetail.MustReturn = x.ProcurementDetail.MustReturn;
                    _this.returnList = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.ProcurementDetail.ProcurementReturnList);
                }
                _this.toggleShowAddReturnButton();
            });
        }
    };
    ProcurementControlPanelComponent.prototype.toggleShowAddReturnButton = function () {
        this.showAddReturnsButton = this.procurementDetail.CurrentBalance > 0 ? true : false;
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])('unittype'),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["TemplateRef"])
    ], ProcurementControlPanelComponent.prototype, "dialogRef", void 0);
    ProcurementControlPanelComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-procurement-control-panel',
            template: __webpack_require__(/*! ./procurement-control-panel.component.html */ "./src/app/store/components/procurement-control-panel/procurement-control-panel.component.html"),
            styles: [__webpack_require__(/*! ./procurement-control-panel.component.scss */ "./src/app/store/components/procurement-control-panel/procurement-control-panel.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"], _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
            _services_purchase_service__WEBPACK_IMPORTED_MODULE_4__["PurchaseService"], _angular_material__WEBPACK_IMPORTED_MODULE_2__["MatDialog"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_5__["FormBuilder"], ngx_toastr__WEBPACK_IMPORTED_MODULE_7__["ToastrService"]])
    ], ProcurementControlPanelComponent);
    return ProcurementControlPanelComponent;
}());



/***/ }),

/***/ "./src/app/store/components/purchase-filed-config/purchase-filed-config.component.html":
/*!*********************************************************************************************!*\
  !*** ./src/app/store/components/purchase-filed-config/purchase-filed-config.component.html ***!
  \*********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<hum-config-card [showCard]=\"showConfig\" (cardState)=\"getState($event)\">\r\n  <div title>Purchases Field Configuration</div>\r\n  <div subtitle>Please select all the fields that should be displayed in the purchases list and also the PDF\r\n    export.</div>\r\n  <div content [ngStyle]=\"scrollStyles\">\r\n    <!-- <mat-list>\r\n      <mat-list-item *ngFor=\"let item of columnsToShow\">\r\n        <mat-checkbox (change)=\"onClick($event, item)\" item.value class=\"example-margin\" [labelPosition]=\"'before'\">\r\n          {{item.name}}\r\n        </mat-checkbox>\r\n      </mat-list-item>\r\n      <mat-divider></mat-divider>\r\n    </mat-list> -->\r\n\r\n    <mat-selection-list [(ngModel)]=\"selectedOptions\" (ngModelChange)=\"change($event)\" #configFilter>\r\n        <mat-list-option *ngFor=\"let item of columnsToShow\" [value]=\"item\" [selected]=\"item.isSelected\">\r\n          {{item.name}}\r\n        </mat-list-option>\r\n      </mat-selection-list>\r\n\r\n  </div>\r\n  <div footer>\r\n\r\n  </div>\r\n\r\n</hum-config-card>\r\n"

/***/ }),

/***/ "./src/app/store/components/purchase-filed-config/purchase-filed-config.component.scss":
/*!*********************************************************************************************!*\
  !*** ./src/app/store/components/purchase-filed-config/purchase-filed-config.component.scss ***!
  \*********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".sidebar-open {\n  position: fixed;\n  top: 0;\n  width: 25%;\n  background: #ffffff;\n  height: 100%;\n  z-index: 999999;\n  -webkit-transition: all 0.3s;\n  transition: all 0.3s;\n  right: 0 !important;\n  opacity: 1 !important; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9wdXJjaGFzZS1maWxlZC1jb25maWcvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcc3RvcmVcXGNvbXBvbmVudHNcXHB1cmNoYXNlLWZpbGVkLWNvbmZpZ1xccHVyY2hhc2UtZmlsZWQtY29uZmlnLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksZUFBZTtFQUNmLE1BQU07RUFDTixVQUFVO0VBQ1YsbUJBQW1CO0VBQ25CLFlBQVk7RUFDWixlQUFlO0VBQ2YsNEJBQW9CO0VBQXBCLG9CQUFvQjtFQUNwQixtQkFBbUI7RUFDbkIscUJBQXFCLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9zdG9yZS9jb21wb25lbnRzL3B1cmNoYXNlLWZpbGVkLWNvbmZpZy9wdXJjaGFzZS1maWxlZC1jb25maWcuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyIuc2lkZWJhci1vcGVuIHtcclxuICAgIHBvc2l0aW9uOiBmaXhlZDtcclxuICAgIHRvcDogMDtcclxuICAgIHdpZHRoOiAyNSU7XHJcbiAgICBiYWNrZ3JvdW5kOiAjZmZmZmZmO1xyXG4gICAgaGVpZ2h0OiAxMDAlO1xyXG4gICAgei1pbmRleDogOTk5OTk5O1xyXG4gICAgdHJhbnNpdGlvbjogYWxsIDAuM3M7XHJcbiAgICByaWdodDogMCAhaW1wb3J0YW50OyBcclxuICAgIG9wYWNpdHk6IDEgIWltcG9ydGFudDtcclxuICB9Il19 */"

/***/ }),

/***/ "./src/app/store/components/purchase-filed-config/purchase-filed-config.component.ts":
/*!*******************************************************************************************!*\
  !*** ./src/app/store/components/purchase-filed-config/purchase-filed-config.component.ts ***!
  \*******************************************************************************************/
/*! exports provided: PurchaseFiledConfigComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PurchaseFiledConfigComponent", function() { return PurchaseFiledConfigComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _services_field_config_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/field-config.service */ "./src/app/store/services/field-config.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var PurchaseFiledConfigComponent = /** @class */ (function () {
    function PurchaseFiledConfigComponent(eRef, toastr, fieldConfig) {
        this.eRef = eRef;
        this.toastr = toastr;
        this.fieldConfig = fieldConfig;
        this.showConfig = false;
        // name = field to be shown on config filter
        // headerName= purchaselist header name
        // modelName=  purchaselist model key name
        // isSelected= keys to be shown checked by default
        // value = used at the time of pdf download for identifying selected columns to be printed on pdf
        this.columnsToShow = [
            { name: 'Purchase Id', headerName: 'Id', modelName: 'Id', isSelected: true, value: 1 },
            { name: 'Store Item Name', headerName: 'Item', modelName: 'Item', isSelected: true, value: 2 },
            { name: 'Purchased By', headerName: 'Purchased By', modelName: 'PurchasedBy', isSelected: true, value: 3 },
            { name: 'Project', headerName: 'Project', modelName: 'Project', isSelected: true, value: 4 },
            { name: 'Original Cost', headerName: 'Original Cost', modelName: 'OriginalCost', isSelected: true, value: 5 },
            { name: 'Depreciated Cost', headerName: 'Depreciated Cost', modelName: 'DepreciatedCost', isSelected: true, value: 6 },
            { name: 'Purchase Date', headerName: 'Purchase Date', modelName: 'PurchaseDate', isSelected: false, value: 7 },
            { name: 'Currency', headerName: 'Currency', modelName: 'Currency', isSelected: false, value: 8 },
            { name: 'PurchasedQuantity', headerName: 'PurchasedQuantity', modelName: 'PurchasedQuantity', isSelected: false, value: 9 },
            { name: 'Item Code', headerName: 'Item Code', modelName: 'ItemCode', isSelected: false, value: 10 },
            { name: 'Project Id', headerName: 'Project Id', modelName: 'ProjectId', isSelected: false, value: 11 },
            { name: 'Item Code Description', headerName: 'Item Code Description', modelName: 'ItemCodeDescription', isSelected: false, value: 12 },
            { name: 'Description', headerName: 'Description', modelName: 'Description', isSelected: false, value: 13 },
            { name: 'BudgetLine Name', headerName: 'BudgetLine Name', modelName: 'BudgetLineName', isSelected: false, value: 14 },
            { name: 'Depreciation Rate', headerName: 'Depreciation Rate', modelName: 'DepreciationRate', isSelected: false, value: 15 },
            { name: 'Master Inventory Code', headerName: 'Master Inventory Code', modelName: 'MasterInventoryCode', isSelected: false, value: 16 },
            { name: 'Office Code', headerName: 'Office Code', modelName: 'OfficeCode', isSelected: false, value: 17 },
            { name: 'Receipt Date', headerName: 'Receipt Date', modelName: 'ReceiptDate', isSelected: false, value: 18 },
            { name: 'Invoice Date', headerName: 'Invoice Date', modelName: 'InvoiceDate', isSelected: false, value: 19 },
            { name: 'Received From Location', headerName: 'Received From Location', modelName: 'ReceivedFromLocationName', isSelected: false,
                value: 20 },
            { name: 'Status', headerName: 'Status', modelName: 'Status', isSelected: false, value: 21 }
        ];
        this.selectedOptions = [];
    }
    PurchaseFiledConfigComponent.prototype.ngOnInit = function () {
        this.getScreenSize();
        this.change(this.columnsToShow.slice(0, 6));
        this.selectedOptions.push(this.columnsToShow.slice(0, 6));
    };
    PurchaseFiledConfigComponent.prototype.show = function () {
        this.showConfig = true;
    };
    PurchaseFiledConfigComponent.prototype.getState = function (e) {
        this.showConfig = e;
    };
    //#region "Dynamic Scroll"
    PurchaseFiledConfigComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 170 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    PurchaseFiledConfigComponent.prototype.change = function (list) {
        this.fieldConfig.updateList(list);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], PurchaseFiledConfigComponent.prototype, "getScreenSize", null);
    PurchaseFiledConfigComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-purchase-filed-config',
            template: __webpack_require__(/*! ./purchase-filed-config.component.html */ "./src/app/store/components/purchase-filed-config/purchase-filed-config.component.html"),
            styles: [__webpack_require__(/*! ./purchase-filed-config.component.scss */ "./src/app/store/components/purchase-filed-config/purchase-filed-config.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"], ngx_toastr__WEBPACK_IMPORTED_MODULE_1__["ToastrService"], _services_field_config_service__WEBPACK_IMPORTED_MODULE_2__["FieldConfigService"]])
    ], PurchaseFiledConfigComponent);
    return PurchaseFiledConfigComponent;
}());



/***/ }),

/***/ "./src/app/store/components/purchase-filters/purchase-filters.component.html":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/purchase-filters/purchase-filters.component.html ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "  <div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n      <span class=\"pull-left\">\r\n        <h4>Filters</h4>\r\n      </span>\r\n      <span class=\"pull-right\">\r\n        <hum-button [type]=\"'filter'\" [text]=\"'advance filters'\" (click)=\"isBasic=!isBasic\"></hum-button>\r\n        <hum-button [type]=\"'clear'\" [text]=\"'clear filters'\" (click)=\"clearFilters()\"></hum-button>\r\n      </span>\r\n    </div>\r\n  </div>\r\n  <div class=\"row\">\r\n    <form [formGroup]=\"purchaseFormFilters\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-3\">\r\n            <lib-hum-dropdown [options]=\"inventoryType$\" formControlName=\"InventoryTypeId\" [placeHolder]=\"'Inventory'\"\r\n              (change)=\"getInventoryTypeSelectedValue($event)\"></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <lib-hum-dropdown [options]=\"receiptType$\" formControlName=\"ReceiptTypeId\" [placeHolder]=\"'Receipt Type'\"\r\n              (change)=\"getReceiptTypeSelectedValue($event)\"></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <lib-hum-dropdown [options]=\"offices$\" formControlName=\"OfficeId\" [placeHolder]=\"'Office'\"\r\n              (change)=\"getOfficeSelectedValue($event)\"></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <lib-hum-dropdown [options]=\"currencies$\" formControlName=\"CurrencyId\" [placeHolder]=\"'Currency'\"\r\n              (change)=\"getCurrenciesSelectedValue($event)\"></lib-hum-dropdown>\r\n          </div>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\" [hidden]=\"isBasic\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-3\">\r\n            <lib-hum-dropdown [options]=\"projects$\" formControlName=\"ProjectId\" [placeHolder]=\"'Project'\"\r\n              (change)=\"getProjectSelectedValue($event)\"></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <lib-hum-dropdown [options]=\"projectJobs$\" formControlName=\"JobId\" [placeHolder]=\"'Job'\"\r\n              (change)=\"getJobSelectedValue($event)\"></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <mat-form-field>\r\n              <input matInput formControlName=\"DateOfPurchase\" [matDatepicker]=\"DateOfPurchase\" (dateChange)=\"getPurchaseDateSelectedValue($event.value)\"\r\n                placeholder=\"Date of Purchase\">\r\n              <mat-datepicker-toggle matSuffix [for]=\"DateOfPurchase\"></mat-datepicker-toggle>\r\n              <mat-datepicker #DateOfPurchase></mat-datepicker>\r\n            </mat-form-field>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <mat-form-field>\r\n              <input matInput formControlName=\"DateOfIssue\" [matDatepicker]=\"DateOfIssue\" placeholder=\"Date of Issue\" (dateChange)=\"getIssueDateSelectedValue($event.value)\">\r\n              <mat-datepicker-toggle matSuffix [for]=\"DateOfIssue\"></mat-datepicker-toggle>\r\n              <mat-datepicker #DateOfIssue></mat-datepicker>\r\n            </mat-form-field>\r\n          </div>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\" [hidden]=\"isBasic\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-3\">\r\n            <lib-hum-dropdown [options]=\"storeInventory$\" formControlName=\"InventoryMasterId\"\r\n              [placeHolder]=\"'Inventory(Master)'\" (change)=\"getMasterInventorySelectedValue($event)\"></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <lib-hum-dropdown [options]=\"storeItemGroups$\" formControlName=\"ItemGroupId\" [placeHolder]=\"'Item Group'\"\r\n              (change)=\"getItemGroupSelectedValue($event)\"></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <lib-hum-dropdown [options]=\"storeItems$\" formControlName=\"ItemId\" [placeHolder]=\"'Item'\"\r\n              (change)=\"getItemSelectedValue($event)\"></lib-hum-dropdown>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <mat-form-field>\r\n              <input matInput formControlName=\"DepreciationComparisionDate\" [matDatepicker]=\"DepreciationComparisionDate\" (dateChange)=\"getComparisionDateSelectedValue($event.value)\" placeholder=\"Depreciation Comparision Date\">\r\n              <mat-datepicker-toggle matSuffix [for]=\"DepreciationComparisionDate\"></mat-datepicker-toggle>\r\n              <mat-datepicker #DepreciationComparisionDate></mat-datepicker>\r\n            </mat-form-field>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </form>\r\n  </div>\r\n\r\n"

/***/ }),

/***/ "./src/app/store/components/purchase-filters/purchase-filters.component.scss":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/purchase-filters/purchase-filters.component.scss ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvcHVyY2hhc2UtZmlsdGVycy9wdXJjaGFzZS1maWx0ZXJzLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/store/components/purchase-filters/purchase-filters.component.ts":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/purchase-filters/purchase-filters.component.ts ***!
  \*********************************************************************************/
/*! exports provided: PurchaseFiltersComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PurchaseFiltersComponent", function() { return PurchaseFiltersComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var src_app_dashboard_project_management_project_list_budgetlines_budget_line_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/dashboard/project-management/project-list/budgetlines/budget-line.service */ "./src/app/dashboard/project-management/project-list/budgetlines/budget-line.service.ts");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
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








var PurchaseFiltersComponent = /** @class */ (function () {
    function PurchaseFiltersComponent(purchaseService, fb, budgetLineService, loader) {
        this.purchaseService = purchaseService;
        this.fb = fb;
        this.budgetLineService = budgetLineService;
        this.loader = loader;
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_4__["ReplaySubject"](1);
        this.isBasic = true;
        this.purchaseFilterSelected = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.purchaseFormFilters = this.fb.group({
            InventoryTypeId: [0, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required],
            ReceiptTypeId: [0, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required],
            OfficeId: [0, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required],
            CurrencyId: [0, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required],
            ProjectId: [0],
            JobId: [0],
            DateOfPurchase: [null],
            DateOfIssue: [null],
            InventoryMasterId: [0],
            ItemGroupId: [0],
            ItemId: [0],
            DepreciationComparisionDate: [null]
        });
    }
    PurchaseFiltersComponent.prototype.ngOnInit = function () {
        this.getPurchaseFilters();
    };
    PurchaseFiltersComponent.prototype.getPurchaseFilters = function () {
        var _this = this;
        this.loader.showLoader();
        this.purchaseService.getPurchaseFilterList()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.inventoryType$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.InventoryTypes.map(function (y) {
                return {
                    value: y.Id,
                    name: y.InventoryName
                };
            }));
            _this.offices$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.Offices.map(function (y) {
                return {
                    value: y.OfficeId,
                    name: y.OfficeCode + '-' + y.OfficeName
                };
            }));
            _this.currencies$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.CurrencyModel.map(function (y) {
                return {
                    value: y.CurrencyId,
                    name: y.CurrencyCode + '-' + y.CurrencyName
                };
            }));
            _this.projects$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.ProjectModel.map(function (y) {
                return {
                    value: y.ProjectId,
                    name: y.ProjectCode + '-' + y.ProjectName
                };
            }));
            _this.receiptType$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.ReceiptTypes.map(function (y) {
                return {
                    value: y.ReceiptTypeId,
                    name: y.ReceiptTypeName
                };
            }));
            _this.storeInventory$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.StoreInventoryModel.map(function (y) {
                return {
                    value: y.InventoryId,
                    name: y.InventoryCode + '-' + y.InventoryName
                };
            }));
            // Set defaults for filter
            // this.purchaseFormFilters.get('InventoryTypeId').patchValue(x.InventoryTypes !== null ? x.InventoryTypes[0].Id : null);
            // this.purchaseFormFilters.get('ReceiptTypeId').patchValue(x.ReceiptTypes !== null ? x.ReceiptTypes[0].ReceiptTypeId : null);
            // this.purchaseFormFilters.get('OfficeId').patchValue(x.Offices !== null ? x.Offices[0].OfficeId : null);
            // this.purchaseFormFilters.get('CurrencyId').patchValue(x.CurrencyModel !== null ? x.CurrencyModel[0].CurrencyId : null);
            _this.onPurchaseFilterSelectionChanged();
            _this.loader.hideLoader();
        }, function (err) {
            _this.loader.hideLoader();
            console.error(err);
        });
    };
    PurchaseFiltersComponent.prototype.getInventoryTypeSelectedValue = function (event) {
        this.getInventoriesByInventoryTypeId(event);
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getReceiptTypeSelectedValue = function (event) {
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getOfficeSelectedValue = function (event) {
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getCurrenciesSelectedValue = function (event) {
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getProjectSelectedValue = function (event) {
        this.onPurchaseFilterSelectionChanged();
        this.getJobsByProjectId(event);
    };
    PurchaseFiltersComponent.prototype.getJobSelectedValue = function (event) {
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getMasterInventorySelectedValue = function (event) {
        this.purchaseFormFilters.get('ItemId').patchValue(null);
        this.getAllStoreItemGroups(event);
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getItemGroupSelectedValue = function (event) {
        this.getAllStoreItemsByGroupId(event);
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getItemSelectedValue = function (event) {
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getPurchaseDateSelectedValue = function (event) {
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getIssueDateSelectedValue = function (event) {
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getComparisionDateSelectedValue = function (event) {
        this.onPurchaseFilterSelectionChanged();
    };
    PurchaseFiltersComponent.prototype.getJobsByProjectId = function (projectId) {
        var _this = this;
        this.budgetLineService
            .GetProjectJobList(projectId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.projectJobs$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                return {
                    value: y.ProjectJobId,
                    name: y.ProjectJobCode + '-' + y.ProjectJobName
                };
            }));
        });
    };
    PurchaseFiltersComponent.prototype.getInventoriesByInventoryTypeId = function (inventoryTypeId) {
        var _this = this;
        this.purchaseService
            .getInventoriesByInventoryTypeId(inventoryTypeId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.storeInventory$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                return {
                    name: y.InventoryCode + '-' + y.InventoryName,
                    value: y.InventoryId
                };
            }));
        });
    };
    PurchaseFiltersComponent.prototype.getAllStoreItemGroups = function (inventoryId) {
        var _this = this;
        this.purchaseService
            .getItemGroupByInventoryId(inventoryId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.storeItemGroups$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                return {
                    name: y.ItemGroupCode + '-' + y.ItemGroupName,
                    value: y.ItemGroupId
                };
            }));
        });
    };
    PurchaseFiltersComponent.prototype.getAllStoreItemsByGroupId = function (groupId) {
        var _this = this;
        this.purchaseService
            .getItemsByItemGroupId(groupId)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.storeItems$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                return {
                    name: y.ItemCode + '-' + y.ItemName,
                    value: y.ItemId
                };
            }));
        });
    };
    PurchaseFiltersComponent.prototype.onPurchaseFilterSelectionChanged = function () {
        if (this.purchaseFormFilters.valid) {
            this.purchaseFilterSelected.emit(this.purchaseFormFilters);
        }
    };
    PurchaseFiltersComponent.prototype.clearFilters = function () {
        this.purchaseFormFilters.reset();
        this.purchaseFilterSelected.emit(this.purchaseFormFilters);
    };
    PurchaseFiltersComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], PurchaseFiltersComponent.prototype, "purchaseFilterSelected", void 0);
    PurchaseFiltersComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-purchase-filters',
            template: __webpack_require__(/*! ./purchase-filters.component.html */ "./src/app/store/components/purchase-filters/purchase-filters.component.html"),
            styles: [__webpack_require__(/*! ./purchase-filters.component.scss */ "./src/app/store/components/purchase-filters/purchase-filters.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_purchase_service__WEBPACK_IMPORTED_MODULE_1__["PurchaseService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"], src_app_dashboard_project_management_project_list_budgetlines_budget_line_service__WEBPACK_IMPORTED_MODULE_5__["BudgetLineService"], src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_7__["CommonLoaderService"]])
    ], PurchaseFiltersComponent);
    return PurchaseFiltersComponent;
}());



/***/ }),

/***/ "./src/app/store/components/purchase-list/purchase-list.component.html":
/*!*****************************************************************************!*\
  !*** ./src/app/store/components/purchase-list/purchase-list.component.html ***!
  \*****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template [headerClass]=\"'sub_header_template_main2'\">\r\n  <span class=\"action_header\">Purchases & Procurements\r\n    <hum-button [type]=\"'add'\" [text]=\"'ADD PURCHASE'\" (click)=\"addPurchase()\"></hum-button>\r\n    <!-- <hum-button [type]=\"'down'\" [text]=\"'DISPLAY CURRENCY'\"></hum-button> -->\r\n    <mat-form-field>\r\n      <mat-placeholder class=\"fnt-label-size\">Display Currency</mat-placeholder>\r\n      <mat-select class=\"font-size\" [(ngModel)]=\"selectedDisplayCurrency\"\r\n        (selectionChange)='selectedDisplayCurrencyChanged()' >\r\n        <mat-option *ngFor=\"let currency of currencyList$ | async\" [value]=\"currency.value\">\r\n          {{currency.name}}\r\n        </mat-option>\r\n      </mat-select>\r\n    </mat-form-field>\r\n  </span>\r\n  <div class=\"action_section\">\r\n    <hum-button [type]=\"'text'\" [text]=\"'PDF EXPORT'\" (click)=\"onPdfExportClick()\"></hum-button>\r\n    <hum-button [type]=\"'text'\" [text]=\"'FIELD CONFIGURATION'\" (click)=\"showConfiguration()\"></hum-button>\r\n    <!-- <span> <a [routerLink]=\"['/store/purchase/add']\">ADD PURCHASE</a> </span>\r\n    <span> <a>DISPLAY CURRENCY</a> </span>\r\n    <span> <a>PDF EXPORT</a> </span>\r\n    <span> <a>FIELD CONFIGURATION</a> </span> -->\r\n\r\n  </div>\r\n\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n<mat-card [ngStyle]=\"scrollStyles\">\r\n  <!-- <div style=\"padding-left: 50px;\">\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n          <mat-form-field>\r\n              <mat-label>Display Currency</mat-label>\r\n              <mat-select [(ngModel)]=\"selectedDisplayCurrency\" (selectionChange)='selectedDisplayCurrencyChanged()'>\r\n                <mat-option *ngFor=\"let currency of currencyList$ | async\" [value]=\"currency.value\">\r\n                  {{currency.name}}\r\n                </mat-option>\r\n              </mat-select>\r\n            </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </div> -->\r\n\r\n  <mat-card>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n        <app-purchase-filters (purchaseFilterSelected)=\"onpurchaseFilterSelected($event)\"></app-purchase-filters>\r\n        <mat-divider></mat-divider>\r\n        <h4>Purchases</h4>\r\n        <div class=\"purchaseTable\">\r\n          <hum-table [headers]=\"purchaseListHeaders$\" [items]=\"purchaseList$\" [subHeaders]=\"subListHeaders$\"\r\n            [subTitle]=\"'Procurements'\" [isDefaultAction]=\"false\" (actionClick)=\"actionEvents($event)\"\r\n            (subActionClick)=\"procurementAction($event)\" [isDefaultSubAction]=\"false\" [actions]=\"actions\"\r\n            [hideColums$]=\"hideColums\" [hideColumsSub$]=\"hideColumsSub\"></hum-table>\r\n        </div>\r\n        <mat-paginator [length]=\"purchaseRecordCount\" [pageSize]=\"filterValueModel.PageSize\"\r\n          [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n        </mat-paginator>\r\n      </div>\r\n    </div>\r\n  </mat-card>\r\n</mat-card>\r\n\r\n<app-purchase-filed-config></app-purchase-filed-config>\r\n"

/***/ }),

/***/ "./src/app/store/components/purchase-list/purchase-list.component.scss":
/*!*****************************************************************************!*\
  !*** ./src/app/store/components/purchase-list/purchase-list.component.scss ***!
  \*****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "span {\n  padding-left: 20px; }\n\n.side-menu {\n  top: 0px; }\n\ndiv.purchaseTable {\n  overflow: auto; }\n\ndiv.purchaseTable {\n  white-space: nowrap; }\n\n.font-size {\n  font-size: 12px; }\n\n.fnt-label-size {\n  font-size: 16px !important;\n  color: #0390D1; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9wdXJjaGFzZS1saXN0L2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXHN0b3JlXFxjb21wb25lbnRzXFxwdXJjaGFzZS1saXN0XFxwdXJjaGFzZS1saXN0LmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksa0JBQ0osRUFBQTs7QUFDQTtFQUNJLFFBQVEsRUFBQTs7QUFHWjtFQUNFLGNBQWMsRUFBQTs7QUFHaEI7RUFDRSxtQkFBbUIsRUFBQTs7QUFFckI7RUFDRSxlQUFjLEVBQUE7O0FBR2hCO0VBQ0UsMEJBQTBCO0VBQzFCLGNBQWEsRUFBQSIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvcHVyY2hhc2UtbGlzdC9wdXJjaGFzZS1saXN0LmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsic3BhbiB7XHJcbiAgICBwYWRkaW5nLWxlZnQ6IDIwcHhcclxufVxyXG4uc2lkZS1tZW51e1xyXG4gICAgdG9wOiAwcHg7XHJcbn1cclxuXHJcbmRpdi5wdXJjaGFzZVRhYmxlIHtcclxuICBvdmVyZmxvdzogYXV0bztcclxufVxyXG5cclxuZGl2LnB1cmNoYXNlVGFibGUge1xyXG4gIHdoaXRlLXNwYWNlOiBub3dyYXA7XHJcbn1cclxuLmZvbnQtc2l6ZXtcclxuICBmb250LXNpemU6MTJweDtcclxufVxyXG5cclxuLmZudC1sYWJlbC1zaXplIHtcclxuICBmb250LXNpemU6IDE2cHggIWltcG9ydGFudDtcclxuICBjb2xvcjojMDM5MEQxO1xyXG4gfVxyXG4iXX0= */"

/***/ }),

/***/ "./src/app/store/components/purchase-list/purchase-list.component.ts":
/*!***************************************************************************!*\
  !*** ./src/app/store/components/purchase-list/purchase-list.component.ts ***!
  \***************************************************************************/
/*! exports provided: PurchaseListComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PurchaseListComponent", function() { return PurchaseListComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _purchase_filed_config_purchase_filed_config_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../purchase-filed-config/purchase-filed-config.component */ "./src/app/store/components/purchase-filed-config/purchase-filed-config.component.ts");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _services_field_config_service__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ../../services/field-config.service */ "./src/app/store/services/field-config.service.ts");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};














var PurchaseListComponent = /** @class */ (function () {
    function PurchaseListComponent(purchaseService, router, dialog, datePipe, toastr, loader, fieldConfigservice, globalSharedService, appurl) {
        this.purchaseService = purchaseService;
        this.router = router;
        this.dialog = dialog;
        this.datePipe = datePipe;
        this.toastr = toastr;
        this.loader = loader;
        this.fieldConfigservice = fieldConfigservice;
        this.globalSharedService = globalSharedService;
        this.appurl = appurl;
        this.purchaseRecordCount = 0;
        this.showConfig = false;
        this.purchaseListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Item', 'Purchased By', 'Project', 'Original Cost', 'Depreciated Cost', 'Purchase Date', 'Currency',
            'Purchased Quantity', 'Item Code', 'Project Id', 'Item Code Description', 'Description', 'BudgetLine Name',
            'Depreciation Rate', 'Master Inventory Code', 'Office Code', 'Receipt Date', 'Invoice Date',
            'Received From Location', 'Status']);
        this.subListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Date', 'Employee', 'Must Return', 'Procurement Balance (Quantity)', 'Status']);
        this.columnsToShownInPdf = [];
        this.filterValueModel = {
            CurrencyId: 0,
            InventoryId: 0,
            InventoryTypeId: 0,
            IssueEndDate: null,
            IssueStartDate: null,
            ItemGroupId: 0,
            ItemId: 0,
            OfficeId: 0,
            JobId: 0,
            ProjectId: 0,
            PurchaseEndDate: null,
            PurchaseStartDate: null,
            ReceiptTypeId: 0,
            DisplayCurrency: 0,
            DepreciationComparisionDate: null,
            PageIndex: 0,
            PageSize: 10,
            TotalCount: 0
        };
    }
    PurchaseListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.hideColumsSub = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])({
            headers: ['Date', 'Employee', 'Must Return', 'Procurement Balance (Quantity)', 'Status'],
            items: ['IssueDate', 'Employee', 'MustReturn', 'ProcuredAmount', 'Status']
        });
        this.getScreenSize();
        this.getAllCurrencies();
        this.actions = {
            items: {
                button: { status: true, text: 'Add Procurement' },
                delete: false,
                download: false,
                edit: true
            },
            subitems: {
                button: { status: true, text: 'Add Return' },
                delete: true,
                download: false,
                edit: false
            }
        };
        this.fieldConfigservice.data.subscribe(function (res) {
            if (res.length > 0) {
                var headers = res.map(function (r) { return r.headerName; });
                var items = res.map(function (r) { return r.modelName; });
                _this.hideColums = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])({ headers: headers, items: items });
                _this.columnsToShownInPdf = res;
            }
        });
    };
    //#region "Dynamic Scroll"
    PurchaseListComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'scroll',
            width: this.screenWidth
        };
    };
    //#endregion
    PurchaseListComponent.prototype.getPurchasesByFilter = function (filter) {
        var _this = this;
        this.loader.showLoader();
        this.filterValueModel = filter;
        this.purchaseService
            .getFilteredPurchaseList(filter).subscribe(function (x) {
            _this.purchaseRecordCount = x.RecordCount;
            _this.purchaseFilterConfigList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(x.PurchaseList);
            _this.purchaseList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(x.PurchaseList.map(function (element) {
                return {
                    Id: element.PurchaseId,
                    Item: element.ItemName,
                    PurchasedBy: element.EmployeeName,
                    Project: element.ProjectName,
                    OriginalCost: element.OriginalCost,
                    DepreciatedCost: element.DepreciatedCost,
                    PurchaseDate: element.PurchaseDate ? _this.datePipe.transform(new Date(element.PurchaseDate), 'dd/MM/yyyy') : null,
                    Currency: element.CurrencyName,
                    ItemCode: element.ItemCode,
                    ProjectId: element.ProjectId,
                    ItemCodeDescription: element.ItemCodeDescription,
                    Description: element.Description,
                    BudgetLineName: element.BudgetLineName,
                    DepreciationRate: element.DepreciationRate,
                    MasterInventoryCode: element.MasterInventoryCode,
                    OfficeCode: element.OfficeCode,
                    ReceiptDate: (element.ReceiptDate),
                    ItemId: element.ItemId,
                    // this.datePipe.transform(new Date(element.ReceiptDate), 'dd/MM/yyyy') : null,
                    InvoiceDate: (element.InvoiceDate),
                    // this.datePipe.transform(new Date(element.InvoiceDate), 'dd/MM/yyyy') : null,
                    ReceivedFromLocationName: element.ReceivedFromLocationName,
                    Status: element.Status,
                    ProcurementList: element.ProcurementList,
                    Quantity: element.Quantity,
                    subItemSubtitle: element.LogisticRequestId ? '<b>Note:</b> The purchase was created as a result of <a href="/project/my-project/' + element.ProjectId + '/logistic-requests/' + element.LogisticRequestId + '" target="_blank">purchase-order-id-' + element.LogisticRequestId + ' </a></br>' : '',
                    subItems: element.ProcurementList.map(function (r) {
                        return {
                            Id: r.OrderId,
                            IssueDate: (r.IssueDate != null && r.IssueDate !== undefined) ?
                                _this.datePipe.transform(new Date(r.IssueDate), 'dd/MM/yyyy') : null,
                            Employee: r.EmployeeName,
                            MustReturn: r.MustReturn ? 'Yes' : 'No',
                            ProcuredAmount: r.ProcuredAmount,
                            Status: r.IsDeleted ? 'Cancelled' : r.ProcuredAmount > 0 ? 'Active' : 'In-Active',
                            itemAction: ((r.MustReturn && r.ProcuredAmount > 0) && !r.IsDeleted) ? ({
                                button: {
                                    status: true,
                                    text: 'ADD RETURN',
                                    type: 'add'
                                },
                                delete: true,
                                download: false,
                                edit: false
                            }) : ({
                                button: {
                                    status: false,
                                    text: 'ADD RETURN',
                                    type: 'add'
                                },
                                delete: false,
                                download: false,
                                edit: false
                            })
                            // Returned: r.Returned ? 'Yes' : 'No',
                            // ReturnedOn: r.ReturnedOn ? this.datePipe.transform(new Date(r.ReturnedOn), 'dd/MM/yyyy') : null,
                        };
                    }),
                    itemAction: ((element.Quantity - element.ProcurementList
                        .filter(function (x) { return x.IsDeleted === false; })
                        .reduce(function (a, b) { return a + b.ProcuredAmount; }, 0)) > 0) ?
                        ([
                            {
                                button: {
                                    status: true,
                                    text: 'ADD PROCUREMENT',
                                    type: 'add'
                                },
                                delete: false,
                                download: false,
                                edit: true
                            }
                        ]) : ([
                        {
                            button: {
                                status: false,
                                text: 'ADD PROCUREMENT',
                            },
                            delete: false,
                            download: false,
                            edit: true
                        }
                    ])
                };
            }));
            _this.loader.hideLoader();
        }, function (error) {
            _this.loader.hideLoader();
        });
    };
    PurchaseListComponent.prototype.onpurchaseFilterSelected = function (event) {
        this.filterValueModel = {
            CurrencyId: event.value.CurrencyId,
            PurchaseStartDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_13__["StaticUtilities"].setLocalDate(event.value.DateOfPurchase),
            IssueStartDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_13__["StaticUtilities"].setLocalDate(event.value.DateOfIssue),
            InventoryTypeId: event.value.InventoryTypeId,
            ReceiptTypeId: event.value.ReceiptTypeId,
            OfficeId: event.value.OfficeId,
            ProjectId: event.value.ProjectId,
            JobId: event.value.JobId,
            InventoryId: event.value.InventoryMasterId,
            ItemGroupId: event.value.ItemGroupId,
            ItemId: event.value.ItemId,
            IssueEndDate: null,
            PurchaseEndDate: null,
            DisplayCurrency: this.selectedDisplayCurrency,
            DepreciationComparisionDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_13__["StaticUtilities"].setLocalDate(event.value.DepreciationComparisionDate),
            PageIndex: this.filterValueModel.PageIndex,
            PageSize: this.filterValueModel.PageSize
        };
        this.getPurchasesByFilter(this.filterValueModel);
    };
    //#region "pageEvent"
    PurchaseListComponent.prototype.pageEvent = function (e) {
        this.filterValueModel.PageIndex = e.pageIndex;
        this.filterValueModel.PageSize = e.pageSize;
        this.getPurchasesByFilter(this.filterValueModel);
    };
    //#endregion
    PurchaseListComponent.prototype.addPurchase = function () {
        this.router.navigate(['/store/purchase/add']);
    };
    PurchaseListComponent.prototype.actionEvents = function (event) {
        if (event.type === 'ADD PROCUREMENT') {
            var remainingQuantity = 0;
            if (event.item.subItems.length > 0) {
                var filteredObjects = (event.item.subItems.filter(function (x) { return x.Status !== 'Cancelled'; }));
                remainingQuantity = (event.item.Quantity - (filteredObjects.reduce(function (a, b) { return a + b.ProcuredAmount; }, 0)));
            }
            else {
                remainingQuantity = event.item.Quantity;
            }
            // if (!this.filterValueModel.OfficeId) {
            //   this.toastr.warning('Select office before adding procurement');
            //   return;
            // }
            // const data = {
            //   value: event.item.Id,
            //   officeId: this.filterValueModel.OfficeId,
            // };
            this.router.navigate(['/store/purchases/add-procurement'], {
                queryParams: {
                    quantity: remainingQuantity,
                    purchaseId: event.item.Id,
                    itemId: event.item.ItemId
                }
            });
            // this.openProcurementDialog(data);
        }
        else if (event.type === 'edit') {
            this.router.navigate(['/store/purchase/edit/' + event.item.Id]);
        }
    };
    // openProcurementDialog(item) {
    //   const dialogRef = this.dialog.open(AddProcurementsComponent, {
    //     width: '850px',
    //     data: item
    //   });
    //   dialogRef.afterClosed().subscribe(x => {
    //     this.getPurchasesByFilter(this.filterValueModel);
    //   });
    // }
    PurchaseListComponent.prototype.procurementAction = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.purchaseService.deleteProcurement(event.subItem.Id)
                .subscribe(function (x) {
                if (x.StatusCode === 200) {
                    _this.purchaseList$.subscribe(function (purchase) {
                        var index = purchase.findIndex(function (i) { return i.Id === event.item.Id; });
                        if (index >= 0) {
                            var subItemIndex = purchase[index].subItems.findIndex(function (i) { return i.Id === event.subItem.Id; });
                            purchase[index].subItems[subItemIndex].Status = 'Cancelled';
                        }
                        _this.purchaseList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(purchase);
                    });
                    _this.toastr.success(x.Message);
                }
                else {
                    _this.toastr.warning(x.Message);
                }
            }, function (error) {
                _this.toastr.error(error);
            });
        }
        else if (event.type === 'edit') {
            if (!this.filterValueModel.OfficeId) {
                this.toastr.warning('Select office before editing procurement');
                return;
            }
            var index = event.item.ProcurementList.findIndex(function (x) { return x.OrderId === event.subItem.Id; });
            var orderData = event.item.ProcurementList[index];
            var data = {
                value: event.item.Id,
                officeId: this.filterValueModel.OfficeId,
                procurement: orderData
            };
            // this.openProcurementDialog(data);
        }
        else if (event.type === 'add') {
            this.router.navigate(['store/purchases/procurement-control-panel/' + event.subItem.Id], {
                queryParams: {
                    quantity: event.subItem.ProcuredAmount,
                }
            });
        }
    };
    PurchaseListComponent.prototype.getAllCurrencies = function () {
        var _this = this;
        this.purchaseService.getAllCurrencies()
            .subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.currencyList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(x.data.CurrencyList.map(function (y) {
                    return {
                        name: y.CurrencyCode + '-' + y.CurrencyName,
                        value: y.CurrencyId
                    };
                }));
                _this.selectedDisplayCurrency = x.data.CurrencyList[0].CurrencyId;
            }
        }, function (error) {
            _this.toastr.error(error);
        });
    };
    PurchaseListComponent.prototype.selectedDisplayCurrencyChanged = function () {
        this.filterValueModel.DisplayCurrency = this.selectedDisplayCurrency;
        this.getPurchasesByFilter(this.filterValueModel);
    };
    PurchaseListComponent.prototype.onPdfExportClick = function () {
        var pdfColumns;
        this.hideColums.subscribe(function (x) { return pdfColumns = x.items; });
        var StorePurchaseFilter = this.filterValueModel;
        StorePurchaseFilter.SelectedColumns = [];
        StorePurchaseFilter.SelectedColumns = pdfColumns;
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_12__["GLOBAL"].API_Pdf_GetStorePurchasePdf, StorePurchaseFilter)
            .pipe()
            .subscribe();
    };
    PurchaseListComponent.prototype.showConfiguration = function () {
        this.fieldConfig.show();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])(_purchase_filed_config_purchase_filed_config_component__WEBPACK_IMPORTED_MODULE_7__["PurchaseFiledConfigComponent"]),
        __metadata("design:type", _purchase_filed_config_purchase_filed_config_component__WEBPACK_IMPORTED_MODULE_7__["PurchaseFiledConfigComponent"])
    ], PurchaseListComponent.prototype, "fieldConfig", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], PurchaseListComponent.prototype, "getScreenSize", null);
    PurchaseListComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-purchase-list',
            template: __webpack_require__(/*! ./purchase-list.component.html */ "./src/app/store/components/purchase-list/purchase-list.component.html"),
            styles: [__webpack_require__(/*! ./purchase-list.component.scss */ "./src/app/store/components/purchase-list/purchase-list.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_purchase_service__WEBPACK_IMPORTED_MODULE_2__["PurchaseService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"], _angular_material__WEBPACK_IMPORTED_MODULE_4__["MatDialog"],
            _angular_common__WEBPACK_IMPORTED_MODULE_5__["DatePipe"], ngx_toastr__WEBPACK_IMPORTED_MODULE_6__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_8__["CommonLoaderService"], _services_field_config_service__WEBPACK_IMPORTED_MODULE_9__["FieldConfigService"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_10__["GlobalSharedService"], src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_11__["AppUrlService"]])
    ], PurchaseListComponent);
    return PurchaseListComponent;
}());



/***/ }),

/***/ "./src/app/store/components/store-configuration/store-configuration.component.html":
/*!*****************************************************************************************!*\
  !*** ./src/app/store/components/store-configuration/store-configuration.component.html ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">Configuration\r\n  </span>\r\n  <div class=\"action_section\">\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n<mat-tab-group>\r\n  <mat-tab label=\"GENERAL\">\r\n    <ng-template matTabContent>\r\n      <ng-container *ngTemplateOutlet=\"general\"></ng-container>\r\n    </ng-template>\r\n  </mat-tab>\r\n  <mat-tab label=\"CONSUMABLES\">\r\n    <ng-template matTabContent>\r\n      <app-store-item-config [assetType]=\"'1'\"></app-store-item-config>\r\n    </ng-template>\r\n  </mat-tab>\r\n  <mat-tab label=\"EXPENDABLES\">\r\n    <ng-template matTabContent>\r\n      <app-store-item-config [assetType]=\"'2'\"></app-store-item-config>\r\n    </ng-template>\r\n  </mat-tab>\r\n  <mat-tab label=\"NON EXPENDABLES\">\r\n    <ng-template matTabContent>\r\n      <app-store-item-config [assetType]=\"'3'\"></app-store-item-config>\r\n    </ng-template>\r\n  </mat-tab>\r\n</mat-tab-group>\r\n\r\n<ng-template #general>\r\n  <mat-card humAddScroll>\r\n    <mat-accordion>\r\n      <mat-expansion-panel>\r\n        <mat-expansion-panel-header>\r\n          <mat-panel-title>\r\n            Unit Types\r\n          </mat-panel-title>\r\n        </mat-expansion-panel-header>\r\n        <div class=\"row\">\r\n          <div class=\"col-md-4\">\r\n            <hum-table [headers]=\"unitListHeaders$\" [items]=\"unitItems$\" [actions]=\"unitActions\"\r\n              [hideColums$]=\"hideUnitColums\" (actionClick)=\"unitAction($event)\"></hum-table>\r\n          </div>\r\n        </div>\r\n        <button mat-raised-button color=\"primary\" (click)=\"openUnitType()\">Add</button>\r\n        <!-- <hum-button [type]=\"'add'\" [text]=\"'Add'\" ></hum-button> -->\r\n      </mat-expansion-panel>\r\n      <mat-expansion-panel *ngFor=\"let codeType of sourceCodeTypes\" (opened)=\"openCodeType(codeType)\">\r\n        <mat-expansion-panel-header>\r\n          <mat-panel-title>\r\n            {{codeType.CodeTypeName}}\r\n          </mat-panel-title>\r\n\r\n        </mat-expansion-panel-header>\r\n\r\n        <hum-table [headers]=\"sourceCodeHeaders$\" [items]=\"sourceCodeByType$\" [actions]=\"unitActions\"\r\n          [hideColums$]=\"hideSourceCodeColums\" (actionClick)=\"codeAction($event)\"></hum-table>\r\n        <button mat-raised-button color=\"primary\" (click)=\"codePopUp(codeType.CodeTypeId)\">Add</button>\r\n      </mat-expansion-panel>\r\n    </mat-accordion>\r\n  </mat-card>\r\n</ng-template>\r\n<ng-template #unittype>\r\n  <form [formGroup]=\"unitTypeGroup\" (ngSubmit)=\"saveUnit()\">\r\n  <div mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field>\r\n          <input matInput placeholder=\"Unit Type\" formControlName=\"unitTypeName\">\r\n          <mat-error *ngIf=\"unitTypeGroup.get('unitTypeName').hasError('required')\">Required</mat-error>\r\n        </mat-form-field>\r\n      </div>\r\n      <!-- <div class=\"col-sm-6\" style=\"padding-top: 21px;\">\r\n          <mat-checkbox value=\"unitTypeGroup.get('isDefault').value\" formControlName=\"isDefault\">Set As Default</mat-checkbox>\r\n        </div> -->\r\n    </div>\r\n  </div>\r\n  <div mat-dialog-actions>\r\n    <hum-button [disabled]=\"unitTypeGroup.invalid\" [type]=\"'save'\" [text]=\"'Save'\" [isSubmit]=\"'true'\"></hum-button>\r\n  </div>\r\n</form>\r\n</ng-template>\r\n<ng-template #sourceCode>\r\n\r\n  <form [formGroup]=\"sourCodeForm\" (ngSubmit)=\"saveCode()\">\r\n    <div mat-dialog-content>\r\n      <div class=\"code-container\">\r\n        <mat-form-field>\r\n          <input matInput placeholder=\"Code\" [readonly]=\"true\" formControlName=\"code\">\r\n\r\n        </mat-form-field>\r\n        <mat-form-field>\r\n          <textarea matInput placeholder=\"Description\" formControlName=\"description\"></textarea>\r\n          <mat-error *ngIf=\"sourCodeForm.controls['description'].errors?.required\">Required</mat-error>\r\n        </mat-form-field>\r\n        <mat-form-field>\r\n          <textarea matInput placeholder=\"Address\" formControlName=\"address\"></textarea>\r\n\r\n          <mat-error *ngIf=\"sourCodeForm.controls['address'].errors?.required\">Required</mat-error>\r\n        </mat-form-field>\r\n        <mat-form-field>\r\n          <input matInput placeholder=\"Email Address\" formControlName=\"emailAddress\">\r\n          <mat-error *ngIf=\"sourCodeForm.controls['emailAddress'].errors?.required\">Required</mat-error>\r\n          <mat-error *ngIf=\"sourCodeForm.controls['emailAddress'].errors?.email\">Invalid email</mat-error>\r\n        </mat-form-field>\r\n        <mat-form-field>\r\n          <input matInput placeholder=\"Fax\" formControlName=\"fax\">\r\n          <mat-error *ngIf=\"sourCodeForm.controls['fax'].errors?.required\">Required</mat-error>\r\n        </mat-form-field>\r\n        <mat-form-field>\r\n          <input matInput placeholder=\"Guarantor\" formControlName=\"guarantor\">\r\n          <mat-error *ngIf=\"sourCodeForm.controls['guarantor'].errors?.required\">Required</mat-error>\r\n        </mat-form-field>\r\n        <mat-form-field>\r\n          <input matInput placeholder=\"Phone\" formControlName=\"phone\">\r\n          <mat-error *ngIf=\"sourCodeForm.controls['phone'].errors?.required\">Required</mat-error>\r\n          <mat-error *ngIf=\"sourCodeForm.controls['phone'].errors\">Phone no should be between 10 - 14 characters\r\n          </mat-error>\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n    <div mat-dialog-actions>\r\n      <hum-button [disabled]=\"sourCodeForm.invalid\" [type]=\"'save'\" [isSubmit]=\"'true'\" [text]=\"'Save'\"></hum-button>\r\n    </div>\r\n  </form>\r\n\r\n\r\n</ng-template>\r\n"

/***/ }),

/***/ "./src/app/store/components/store-configuration/store-configuration.component.scss":
/*!*****************************************************************************************!*\
  !*** ./src/app/store/components/store-configuration/store-configuration.component.scss ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".code-container {\n  display: -webkit-box;\n  display: flex;\n  -webkit-box-orient: vertical;\n  -webkit-box-direction: normal;\n          flex-direction: column; }\n\n.code-container > * {\n  width: 100%; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9zdG9yZS1jb25maWd1cmF0aW9uL2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXHN0b3JlXFxjb21wb25lbnRzXFxzdG9yZS1jb25maWd1cmF0aW9uXFxzdG9yZS1jb25maWd1cmF0aW9uLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksb0JBQWE7RUFBYixhQUFhO0VBQ2IsNEJBQXNCO0VBQXRCLDZCQUFzQjtVQUF0QixzQkFBc0IsRUFBQTs7QUFHeEI7RUFDRSxXQUFXLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9zdG9yZS9jb21wb25lbnRzL3N0b3JlLWNvbmZpZ3VyYXRpb24vc3RvcmUtY29uZmlndXJhdGlvbi5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5jb2RlLWNvbnRhaW5lciB7XHJcbiAgICBkaXNwbGF5OiBmbGV4O1xyXG4gICAgZmxleC1kaXJlY3Rpb246IGNvbHVtbjtcclxuICB9XHJcbiAgXHJcbiAgLmNvZGUtY29udGFpbmVyID4gKiB7XHJcbiAgICB3aWR0aDogMTAwJTtcclxuICB9Il19 */"

/***/ }),

/***/ "./src/app/store/components/store-configuration/store-configuration.component.ts":
/*!***************************************************************************************!*\
  !*** ./src/app/store/components/store-configuration/store-configuration.component.ts ***!
  \***************************************************************************************/
/*! exports provided: StoreConfigurationComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StoreConfigurationComponent", function() { return StoreConfigurationComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _services_config_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../services/config.service */ "./src/app/store/services/config.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
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








var StoreConfigurationComponent = /** @class */ (function () {
    function StoreConfigurationComponent(dialog, configservice, fb, loader, toastr) {
        this.dialog = dialog;
        this.configservice = configservice;
        this.fb = fb;
        this.loader = loader;
        this.toastr = toastr;
        this.unitListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(['Name']);
        this.sourceCodeHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(['Id', 'SourceCode Id', 'Code', 'Description', 'Address', 'Phone', 'Fax', 'Email Address', 'Guarantor']);
        this.unitType = {};
        this.sourceCodeTypes = [];
        this.sourceCode = {};
        this.isEditCode = false;
    }
    //#region "Dynamic Scroll"
    StoreConfigurationComponent.prototype.ngOnInit = function () {
        var _this = this;
        // this.typeName = new FormControl('', Validators.required);
        this.unitTypeGroup = this.fb.group({
            unitTypeId: [null],
            unitTypeName: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required]],
        });
        this.unitActions = {
            items: {
                edit: true,
                delete: true
            },
            subitems: {}
        };
        this.loader.showLoader();
        Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["forkJoin"])([this.configservice.getUnitType(),
            this.configservice.getAllSourceCodeTypes(),
            this.configservice.getAllStoreSource()]).subscribe(function (res) {
            _this.getAllUnitTypes(res[0]);
            _this.getAllSourCodeTypes(res[1]);
            _this.getAllsourceCodes(res[2]);
            _this.loader.hideLoader();
        });
        this.createSourceCodeForm();
    };
    StoreConfigurationComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 80 + 'px',
            'overflow-x': 'hidden'
        };
    };
    // Unit type configurations start
    StoreConfigurationComponent.prototype.getAllUnitTypes = function (res) {
        this.unitItems$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res.data.PurchaseUnitTypeList).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["map"])(function (x) { return x.map(function (y) { return ({
            UnitTypeId: y.UnitTypeId,
            UnitTypeName: y.UnitTypeName,
        }); }); }));
        this.hideUnitColums = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])({ headers: ['Name'], items: ['UnitTypeName'] });
    };
    StoreConfigurationComponent.prototype.addUnitType = function () {
        this.unitTypeGroup.reset();
        this.openUnitType();
    };
    StoreConfigurationComponent.prototype.openUnitType = function () {
        this.dialog.open(this.dialogRef, {
            width: '400px'
        });
    };
    StoreConfigurationComponent.prototype.saveUnit = function () {
        var _this = this;
        if (this.unitTypeGroup.valid) {
            this.loader.showLoader();
            if (this.unitTypeGroup.value.unitTypeId) {
                // this.unitType.UnitTypeName = this.unitTypeGroup.value.typeName;
                this.configservice.editUnit(this.unitTypeGroup.value).subscribe(function (res) {
                    _this.toastr.success('Updated Successfully');
                    _this.configservice.getUnitType().subscribe(function (res1) {
                        _this.getAllUnitTypes(res1);
                        _this.loader.hideLoader();
                    });
                    _this.unitType = {};
                    _this.dialog.closeAll();
                });
            }
            else {
                // this.unitType.UnitTypeName = this.typeName.value;
                this.unitTypeGroup.value.unitTypeId = 0;
                this.configservice.saveUnit(this.unitTypeGroup.value).subscribe(function (res) {
                    _this.toastr.success('Added Successfully');
                    _this.configservice.getUnitType().subscribe(function (res1) {
                        _this.getAllUnitTypes(res1);
                    });
                    _this.dialog.closeAll();
                    _this.loader.hideLoader();
                });
            }
            this.unitTypeGroup.reset();
        }
    };
    StoreConfigurationComponent.prototype.unitAction = function (data) {
        var _this = this;
        if (data.type == 'delete') {
            this.configservice.openDeleteDialog().subscribe(function (res) {
                if (res) {
                    _this.unitType = data.item;
                    _this.configservice.deleteUnit(_this.unitType).subscribe(function (res) {
                        _this.toastr.success('Deleted Successfully');
                        _this.configservice.getUnitType().subscribe(function (res1) {
                            _this.getAllUnitTypes(res1);
                        });
                    });
                }
            });
        }
        if (data.type === 'edit') {
            this.unitTypeGroup.reset();
            // this.unitType = data.item;
            // this.typeName.setValue(this.unitType.UnitTypeName);
            this.unitTypeGroup.patchValue({
                unitTypeId: data.item.UnitTypeId,
                unitTypeName: data.item.UnitTypeName,
            });
            this.openUnitType();
        }
    };
    // Unit type configurations ends
    // source code configuration start
    StoreConfigurationComponent.prototype.createSourceCodeForm = function () {
        var contactRegex = /^[0-9]{10,14}$/;
        this.sourCodeForm = this.fb.group({
            sourceCodeId: [''],
            code: [''],
            description: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            address: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            phone: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].pattern(contactRegex)]],
            fax: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required]],
            emailAddress: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].email]],
            guarantor: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required]],
            codeTypeId: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required]]
        });
    };
    StoreConfigurationComponent.prototype.getAllSourCodeTypes = function (res) {
        this.sourceCodeTypes = res.data.SourceCodeTypelist;
    };
    StoreConfigurationComponent.prototype.getAllsourceCodes = function (res) {
        this.sourceCodeItems$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res);
        this.sourceCodeByType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res.filter(function (r) { return r.CodeTypeId == 1; }));
        this.hideSourceCodeColums = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])({
            headers: ['Code', 'Description', 'Address', 'Phone', 'Fax', 'Email Address', 'Guarantor'],
            items: ['Code', 'Description', 'Address', 'Phone', 'Fax', 'EmailAddress', 'Guarantor']
        });
    };
    StoreConfigurationComponent.prototype.codePopUp = function (codeTypeId) {
        var _this = this;
        this.configservice.getCodeByType(codeTypeId).subscribe(function (res) {
            _this.isEditCode = false;
            _this.sourCodeForm.reset();
            _this.sourCodeForm.controls.code.setValue(res.data.StoreSourceCode);
            _this.sourCodeForm.controls.codeTypeId.setValue(codeTypeId);
            _this.dialog.open(_this.codeDialogRef, {
                width: '500px'
            });
        });
    };
    StoreConfigurationComponent.prototype.openCodeType = function (e) {
        var _this = this;
        this.sourceCodeItems$.subscribe(function (res) {
            _this.sourceCodeByType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res.filter(function (r) { return r.CodeTypeId == e.CodeTypeId; }));
        });
    };
    StoreConfigurationComponent.prototype.saveCode = function () {
        var _this = this;
        this.sourceCode = {};
        this.sourceCode.Code = this.sourCodeForm.controls.code.value;
        this.sourceCode.Description = this.sourCodeForm.controls.description.value;
        this.sourceCode.Address = this.sourCodeForm.controls.address.value;
        this.sourceCode.Phone = this.sourCodeForm.controls.phone.value;
        this.sourceCode.Fax = this.sourCodeForm.controls.fax.value;
        this.sourceCode.EmailAddress = this.sourCodeForm.controls.emailAddress.value;
        this.sourceCode.Guarantor = this.sourCodeForm.controls.guarantor.value;
        this.sourceCode.CodeTypeId = this.sourCodeForm.controls.codeTypeId.value;
        if (this.isEditCode) {
            this.sourceCode.SourceCodeId = this.sourCodeForm.controls.sourceCodeId.value;
            this.configservice.editCode(this.sourceCode).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["concatMap"])(function (val) {
                _this.toastr.success('Updated Successfully');
                return _this.configservice.getSourceCodeById(_this.sourceCode.CodeTypeId);
            })).subscribe(function (res) {
                _this.sourceCodeByType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res);
                _this.dialog.closeAll();
                _this.isEditCode = false;
                _this.sourceCode = {};
                _this.sourCodeForm.reset();
            });
        }
        else {
            this.sourceCode.SourceCodeId = 0;
            this.configservice.saveCode(this.sourceCode).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["concatMap"])(function (val) {
                _this.toastr.success('Added Successfully');
                return _this.configservice.getSourceCodeById(_this.sourceCode.CodeTypeId);
            })).subscribe(function (res) {
                _this.sourceCodeByType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res);
                _this.dialog.closeAll();
                _this.sourceCode = {};
                _this.sourCodeForm.reset();
            });
        }
    };
    StoreConfigurationComponent.prototype.codeAction = function (data) {
        var _this = this;
        if (data.type == 'delete') {
            this.configservice.openDeleteDialog().subscribe(function (res) {
                if (res) {
                    _this.configservice.deleteCode(data.item.SourceCodeId).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["concatMap"])(function (val) {
                        _this.toastr.success('Deleted Successfully');
                        return _this.configservice.getSourceCodeById(data.item.CodeTypeId);
                    })).subscribe(function (res) {
                        _this.sourceCodeByType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(res);
                    });
                }
            });
        }
        if (data.type == 'edit') {
            this.sourCodeForm.controls.code.setValue(data.item.Code);
            this.sourCodeForm.controls.description.setValue(data.item.Description);
            this.sourCodeForm.controls.address.setValue(data.item.Address);
            this.sourCodeForm.controls.phone.setValue(data.item.Phone);
            this.sourCodeForm.controls.fax.setValue(data.item.Fax);
            this.sourCodeForm.controls.emailAddress.setValue(data.item.EmailAddress);
            this.sourCodeForm.controls.guarantor.setValue(data.item.Guarantor);
            this.sourCodeForm.controls.codeTypeId.setValue(data.item.CodeTypeId);
            this.sourCodeForm.controls.sourceCodeId.setValue(data.item.SourceCodeId);
            this.isEditCode = true;
            this.dialog.open(this.codeDialogRef, {
                width: '500px'
            });
        }
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])('unittype'),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["TemplateRef"])
    ], StoreConfigurationComponent.prototype, "dialogRef", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])('sourceCode'),
        __metadata("design:type", _angular_core__WEBPACK_IMPORTED_MODULE_0__["TemplateRef"])
    ], StoreConfigurationComponent.prototype, "codeDialogRef", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], StoreConfigurationComponent.prototype, "getScreenSize", null);
    StoreConfigurationComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-store-configuration',
            template: __webpack_require__(/*! ./store-configuration.component.html */ "./src/app/store/components/store-configuration/store-configuration.component.html"),
            styles: [__webpack_require__(/*! ./store-configuration.component.scss */ "./src/app/store/components/store-configuration/store-configuration.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialog"],
            _services_config_service__WEBPACK_IMPORTED_MODULE_3__["ConfigService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_4__["FormBuilder"], src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__["CommonLoaderService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_7__["ToastrService"]])
    ], StoreConfigurationComponent);
    return StoreConfigurationComponent;
}());



/***/ }),

/***/ "./src/app/store/components/store-item-config/store-item-config.component.html":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/store-item-config/store-item-config.component.html ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\"> Master Inventories\r\n    <hum-button [type]=\"'add'\" [text]=\"'add new'\" (click)=\"addMasterInventory()\"></hum-button>\r\n  </span>\r\n  <div class=\"action_section\">\r\n\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n\r\n<mat-card humAddScroll [height]=\"250\">\r\n  <mat-tree [dataSource]=\"dataSource\" [treeControl]=\"treeControl\">\r\n    <button mat-icon-button></button>\r\n    <!-- This is the tree node template for leaf nodes -->\r\n    <mat-tree-node *matTreeNodeDef=\"let node\" matTreeNodePadding>\r\n      <!-- use a disabled button to provide padding for tree leaf -->\r\n      <button mat-icon-button disabled></button>\r\n      {{node.code}} - {{node.name}}\r\n      <hum-button *ngIf=\"node.level !=2\" [type]=\"'add'\" [text]=\"node.addText\"\r\n        (click)=\"openItem(node.level,node.id,node.invId, node.isTransportCategory)\">\r\n      </hum-button>\r\n      <hum-button [type]=\"'edit'\" [text]=\"node.editText\"\r\n        (click)=\"openEditItem(node.level,node.id,node.invId, node.groupId,node.isTransportCategory,node.itemTypeCategory )\">\r\n      </hum-button>\r\n    </mat-tree-node>\r\n    <!-- This is the tree node template for expandable nodes -->\r\n    <mat-tree-node *matTreeNodeDef=\"let node;when: hasChild\" matTreeNodePadding>\r\n      <button mat-icon-button matTreeNodeToggle [attr.aria-label]=\"'toggle ' + node.name\">\r\n        <mat-icon class=\"mat-icon-rtl-mirror\">\r\n          {{treeControl.isExpanded(node) ? 'expand_more' : 'chevron_right'}}\r\n        </mat-icon>\r\n      </button>\r\n      {{node.code}} - {{node.name}}\r\n      <hum-button [type]=\"'add'\" [text]=\"node.addText\"\r\n        (click)=\"openItem(node.level,node.id,node.invId,node.isTransportCategory)\"></hum-button>\r\n      <hum-button [type]=\"'edit'\" [text]=\"node.editText\"\r\n        (click)=\"openEditItem(node.level,node.id, node.invId, node.groupId,node.isTransportCategory, node.itemTypeCategory)\">\r\n      </hum-button>\r\n    </mat-tree-node>\r\n  </mat-tree>\r\n</mat-card>"

/***/ }),

/***/ "./src/app/store/components/store-item-config/store-item-config.component.scss":
/*!*************************************************************************************!*\
  !*** ./src/app/store/components/store-item-config/store-item-config.component.scss ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".mat-tree-node, .mat-nested-tree-node {\n  font-weight: 500 !important;\n  font-size: 15px !important; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9zdG9yZS1pdGVtLWNvbmZpZy9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxzdG9yZVxcY29tcG9uZW50c1xcc3RvcmUtaXRlbS1jb25maWdcXHN0b3JlLWl0ZW0tY29uZmlnLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0ksMkJBQTJCO0VBQzNCLDBCQUEwQixFQUFBIiwiZmlsZSI6InNyYy9hcHAvc3RvcmUvY29tcG9uZW50cy9zdG9yZS1pdGVtLWNvbmZpZy9zdG9yZS1pdGVtLWNvbmZpZy5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5tYXQtdHJlZS1ub2RlLCAubWF0LW5lc3RlZC10cmVlLW5vZGUge1xyXG4gICAgZm9udC13ZWlnaHQ6IDUwMCAhaW1wb3J0YW50O1xyXG4gICAgZm9udC1zaXplOiAxNXB4ICFpbXBvcnRhbnQ7XHJcbn0iXX0= */"

/***/ }),

/***/ "./src/app/store/components/store-item-config/store-item-config.component.ts":
/*!***********************************************************************************!*\
  !*** ./src/app/store/components/store-item-config/store-item-config.component.ts ***!
  \***********************************************************************************/
/*! exports provided: StoreItemConfigComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StoreItemConfigComponent", function() { return StoreItemConfigComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_cdk_tree__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/cdk/tree */ "./node_modules/@angular/cdk/esm5/tree.es5.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _services_config_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../services/config.service */ "./src/app/store/services/config.service.ts");
/* harmony import */ var _add_master_inventory_add_master_inventory_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../add-master-inventory/add-master-inventory.component */ "./src/app/store/components/add-master-inventory/add-master-inventory.component.ts");
/* harmony import */ var _add_item_category_add_item_category_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../add-item-category/add-item-category.component */ "./src/app/store/components/add-item-category/add-item-category.component.ts");
/* harmony import */ var _add_item_add_item_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../add-item/add-item.component */ "./src/app/store/components/add-item/add-item.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var StoreItemConfigComponent = /** @class */ (function () {
    function StoreItemConfigComponent(configService, dialog) {
        this.configService = configService;
        this.dialog = dialog;
        this.assetType = 1;
        this.inventories = [];
        this.modelWidth = '600px';
        this.masterInventory = {};
        this.masterInventoryGroup = {};
        this.masterInventoryItem = {};
        this._transformer = function (node, level) {
            return {
                expandable: !!node.children && node.children.length > 0,
                name: node.Name,
                id: node.Id,
                invId: node.InventoryId,
                groupId: node.ItemGroupId,
                level: level,
                code: node.Code,
                description: node.Description,
                addText: node.addText,
                editText: node.editText,
                isTransportCategory: node.IsTransportCategory ? node.IsTransportCategory : null,
                itemTypeCategory: node.ItemTypeCategory
            };
        };
        this.treeControl = new _angular_cdk_tree__WEBPACK_IMPORTED_MODULE_1__["FlatTreeControl"](function (node) { return node.level; }, function (node) { return node.expandable; });
        this.treeFlattener = new _angular_material__WEBPACK_IMPORTED_MODULE_2__["MatTreeFlattener"](this._transformer, function (node) { return node.level; }, function (node) { return node.expandable; }, function (node) { return node.children; });
        this.dataSource = new _angular_material__WEBPACK_IMPORTED_MODULE_2__["MatTreeFlatDataSource"](this.treeControl, this.treeFlattener);
        this.hasChild = function (_, node) { return node.expandable; };
    }
    StoreItemConfigComponent.prototype.ngOnInit = function () {
        this.getAllInventories();
    };
    StoreItemConfigComponent.prototype.getAllInventories = function () {
        var _this = this;
        this.configService.getInventories(this.assetType).subscribe(function (res) {
            _this.inventories = res.ResponseData;
            _this.inventories = _this.inventories.map(function (res) {
                res.addText = "add item category";
                res.editText = "edit master inventory";
                res.children.map(function (res2) {
                    res2.addText = "add item ";
                    res2.editText = "edit item category";
                    res2.children.map(function (res3) {
                        res3.editText = "edit item";
                    });
                });
                return res;
            });
            _this.dataSource.data = _this.inventories;
        });
    };
    // master inventory region
    StoreItemConfigComponent.prototype.addMasterInventory = function () {
        var _this = this;
        this.masterInventory = {};
        this.configService.getInventoryCode(this.assetType).subscribe(function (res) {
            _this.masterInventory.InventoryCode = res.data.InventoryCode;
            _this.masterInventory.AssetType = _this.assetType;
            _this.openMasterInv(_this.masterInventory);
        });
    };
    StoreItemConfigComponent.prototype.openMasterInv = function (data) {
        var _this = this;
        this.masterInventory = data;
        var dg = this.dialog.open(_add_master_inventory_add_master_inventory_component__WEBPACK_IMPORTED_MODULE_4__["AddMasterInventoryComponent"], {
            width: this.modelWidth,
            data: this.masterInventory
        });
        dg.afterClosed().subscribe(function (res) {
            if (res == 1)
                _this.getAllInventories();
        });
    };
    StoreItemConfigComponent.prototype.openItem = function (level, inventoryId, invId, isTransportCategory) {
        var _this = this;
        switch (level) {
            case 0:
                this.masterInventoryGroup = {};
                this.configService.getGroupItemCode(inventoryId, this.assetType).subscribe(function (res) {
                    _this.masterInventoryGroup.InventoryId = inventoryId;
                    _this.masterInventoryGroup.ItemGroupCode = res.data.ItemGroupCode;
                    _this.masterInventoryGroup.IsTransportCategory = isTransportCategory;
                });
                var dg = this.dialog.open(_add_item_category_add_item_category_component__WEBPACK_IMPORTED_MODULE_5__["AddItemCategoryComponent"], {
                    width: this.modelWidth,
                    data: this.masterInventoryGroup
                });
                dg.afterClosed().subscribe(function (res) {
                    if (res == 1)
                        _this.getAllInventories();
                });
                break;
            case 1:
                this.masterInventoryItem = {};
                this.configService.getItemCode(inventoryId, this.assetType).subscribe(function (res) {
                    _this.masterInventoryItem.ItemTypeCategory = Number(_this.inventories.find(function (x) { return x.Id == invId; }).children.find(function (x) { return x.Id == inventoryId; }).ItemTypeCategory);
                    if (_this.masterInventoryItem.ItemTypeCategory) {
                        _this.masterInventoryItem.isGenerator = _this.masterInventoryItem.ItemTypeCategory == 2 ? true : false;
                    }
                    else {
                        _this.masterInventoryItem.isGenerator = null;
                    }
                    _this.masterInventoryItem.ItemGroupId = new Number(inventoryId);
                    _this.masterInventoryItem.ItemCode = res.data.InventoryItemCode;
                    _this.masterInventoryItem.ItemInventory = new Number(invId);
                    _this.masterInventoryItem.AssetType = Number(_this.assetType);
                    var dgItem = _this.dialog.open(_add_item_add_item_component__WEBPACK_IMPORTED_MODULE_6__["AddItemComponent"], {
                        width: _this.modelWidth,
                        data: _this.masterInventoryItem
                    });
                    dgItem.afterClosed().subscribe(function (res) {
                        if (res == 1)
                            _this.getAllInventories();
                    });
                });
                break;
            default:
                break;
        }
    };
    StoreItemConfigComponent.prototype.openEditItem = function (level, level2ID, level1ID, level0ID, isTransport, itemcattype) {
        var _this = this;
        switch (level) {
            case 0:
                var inventory = this.inventories.find(function (x) { return x.Id == level2ID; });
                this.masterInventory.InventoryId = inventory.Id;
                this.masterInventory.AssetType = inventory.AssetType;
                this.masterInventory.InventoryCode = inventory.Code;
                this.masterInventory.InventoryDebitAccount = inventory.InventoryDebitAccount;
                this.masterInventory.InventoryDescription = inventory.Description;
                this.masterInventory.InventoryName = inventory.Name;
                this.masterInventory.IsTransportCategory = isTransport;
                var dg = this.dialog.open(_add_master_inventory_add_master_inventory_component__WEBPACK_IMPORTED_MODULE_4__["AddMasterInventoryComponent"], {
                    width: this.modelWidth,
                    data: this.masterInventory
                });
                dg.afterClosed().subscribe(function (res) {
                    if (res == 1)
                        _this.getAllInventories();
                });
                break;
            case 1:
                var itemgroup = this.inventories.find(function (x) { return x.Id == level1ID; }).children.find(function (x) { return x.Id == level2ID; });
                var isTransportCategory = this.inventories.find(function (x) { return x.Id == level1ID; }).IsTransportCategory;
                this.masterInventoryGroup.Description = itemgroup.Description;
                this.masterInventoryGroup.InventoryId = itemgroup.InventoryId;
                this.masterInventoryGroup.ItemGroupCode = itemgroup.Code;
                this.masterInventoryGroup.ItemGroupId = itemgroup.Id;
                this.masterInventoryGroup.ItemGroupName = itemgroup.Name;
                this.masterInventoryGroup.IsTransportCategory = isTransportCategory;
                this.masterInventoryGroup.ItemTypeCategory = itemcattype;
                var dgGroup = this.dialog.open(_add_item_category_add_item_category_component__WEBPACK_IMPORTED_MODULE_5__["AddItemCategoryComponent"], {
                    width: this.modelWidth,
                    data: this.masterInventoryGroup
                });
                dgGroup.afterClosed().subscribe(function (res) {
                    if (res == 1)
                        _this.getAllInventories();
                });
                break;
            case 2:
                var item = this.inventories.find(function (x) { return x.Id == level1ID; }).children.find(function (x) { return x.Id == level0ID; }).children.find(function (x) { return x.Id == level2ID; });
                if (this.inventories.find(function (x) { return x.Id == level1ID; }).children.find(function (x) { return x.Id == level0ID; }).ItemTypeCategory != null) {
                    this.masterInventoryItem.isGenerator = this.inventories.find(function (x) { return x.Id == level1ID; }).children.find(function (x) { return x.Id == level0ID; }).ItemTypeCategory == 2 ? true : false;
                }
                else {
                    this.masterInventoryItem.isGenerator = null;
                }
                this.masterInventoryItem.ItemTypeCategory = Number(this.inventories.find(function (x) { return x.Id == level1ID; })
                    .children.find(function (x) { return x.Id == level0ID; }).children.find(function (x) { return x.Id == level2ID; })
                    .ItemTypeCategory);
                this.masterInventoryItem.Description = item.Description;
                this.masterInventoryItem.ItemCode = item.Code;
                this.masterInventoryItem.ItemGroupId = item.ItemGroupId;
                this.masterInventoryItem.ItemId = item.Id;
                this.masterInventoryItem.ItemInventory = item.InventoryId;
                this.masterInventoryItem.ItemName = item.Name;
                this.masterInventoryItem.ItemType = null;
                this.masterInventoryItem.DefaultUnitType = item.DefaultUnitType;
                this.masterInventoryItem.AssetType = Number(this.assetType);
                // console.log(level, level2ID, level1ID, level0ID, isTransport, itemcattype)
                var dgItem = this.dialog.open(_add_item_add_item_component__WEBPACK_IMPORTED_MODULE_6__["AddItemComponent"], {
                    width: this.modelWidth,
                    data: this.masterInventoryItem
                });
                dgItem.afterClosed().subscribe(function (res) {
                    if (res == 1)
                        _this.getAllInventories();
                });
                break;
            default:
                break;
        }
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], StoreItemConfigComponent.prototype, "assetType", void 0);
    StoreItemConfigComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-store-item-config',
            template: __webpack_require__(/*! ./store-item-config.component.html */ "./src/app/store/components/store-item-config/store-item-config.component.html"),
            styles: [__webpack_require__(/*! ./store-item-config.component.scss */ "./src/app/store/components/store-item-config/store-item-config.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_config_service__WEBPACK_IMPORTED_MODULE_3__["ConfigService"], _angular_material__WEBPACK_IMPORTED_MODULE_2__["MatDialog"]])
    ], StoreItemConfigComponent);
    return StoreItemConfigComponent;
}());



/***/ }),

/***/ "./src/app/store/components/vehicle-detail/vehicle-detail.component.html":
/*!*******************************************************************************!*\
  !*** ./src/app/store/components/vehicle-detail/vehicle-detail.component.html ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"row\">\r\n    <form [formGroup]=\"vehicleDetailForm\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-3\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                  <input matInput formControlName='PlateNo' placeholder=\"Plate No\">\r\n                </mat-form-field>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <!-- <lib-hum-dropdown [validation]=\"vehicleDetailForm.controls['EmployeeId'].touched\"\r\n            [options]=\"employeeList$\" formControlName='EmployeeId' [placeHolder]=\"'Driver'\">\r\n            </lib-hum-dropdown> -->\r\n            <mat-form-field>\r\n              <mat-label>Driver</mat-label>\r\n              <mat-select formControlName='EmployeeId'>\r\n                <mat-option *ngFor=\"let employee of employeeList$ | async\" [value]=\"employee.value\">\r\n                  {{employee.name}}\r\n                </mat-option>\r\n              </mat-select>\r\n            </mat-form-field>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                  <input matInput type='number' formControlName=\"StartingMileage\" placeholder=\"Starting Mileage(KM)\">\r\n                </mat-form-field>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                <input matInput type='number' formControlName=\"IncurredMileage\" placeholder=\"Incurred Mileage(KM)\">\r\n                </mat-form-field>\r\n          </div>\r\n\r\n        </div>\r\n      </div>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-3\">\r\n            <mat-form-field class=\"example-full-width\">\r\n              <input matInput type='number' formControlName=\"FuelConsumptionRate\" placeholder=\"Standard Fuel Consumption Rate(litres/100KM)\">\r\n            </mat-form-field>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                  <input matInput type='number' formControlName=\"MobilOilConsumptionRate\" placeholder=\"Standard Mobil Oil Consumption Rate(litres/100KM)\">\r\n                </mat-form-field>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <!-- <lib-hum-dropdown [validation]=\"vehicleDetailForm.controls['OfficeId'].hasError('required') || vehicleDetailForm.controls['EmployeeId'].touched\"\r\n            [options]=\"offices$\" formControlName='OfficeId' [placeHolder]=\"'Location'\"></lib-hum-dropdown> -->\r\n\r\n            <mat-form-field>\r\n              <mat-label>Location</mat-label>\r\n              <mat-select formControlName='OfficeId'>\r\n                <mat-option *ngFor=\"let office of offices$ | async\" [value]=\"office.value\">\r\n                  {{office.name}}\r\n                </mat-option>\r\n              </mat-select>\r\n            </mat-form-field>\r\n          </div>\r\n          <div class=\"col-md-3\">\r\n            <mat-form-field class=\"example-full-width\">\r\n                <input matInput type='number' formControlName=\"ModelYear\" placeholder=\"Model Year\">\r\n              </mat-form-field>\r\n        </div>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n            <div class=\"col-md-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                    <input matInput formControlName='ManufacturerCountry' placeholder=\"Manufacturing Country\">\r\n                  </mat-form-field>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                    <input matInput formControlName='EngineNo' placeholder=\"Engine No\">\r\n                  </mat-form-field>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                    <input matInput formControlName='RegistrationNo' placeholder=\"Registration No\">\r\n                  </mat-form-field>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                    <input matInput formControlName='ChasisNo' placeholder=\"Chasis No\">\r\n                  </mat-form-field>\r\n            </div>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\">\r\n        <div class=\"col-md-6 padding_left_30\">\r\n            <mat-form-field class=\"example-full-width\">\r\n                <textarea formControlName='Remarks' matInput placeholder=\"Remarks\"></textarea>\r\n              </mat-form-field>\r\n        </div>\r\n      </div>\r\n    </form>\r\n  </div>\r\n"

/***/ }),

/***/ "./src/app/store/components/vehicle-detail/vehicle-detail.component.scss":
/*!*******************************************************************************!*\
  !*** ./src/app/store/components/vehicle-detail/vehicle-detail.component.scss ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvdmVoaWNsZS1kZXRhaWwvdmVoaWNsZS1kZXRhaWwuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/store/components/vehicle-detail/vehicle-detail.component.ts":
/*!*****************************************************************************!*\
  !*** ./src/app/store/components/vehicle-detail/vehicle-detail.component.ts ***!
  \*****************************************************************************/
/*! exports provided: VehicleDetailComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VehicleDetailComponent", function() { return VehicleDetailComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var VehicleDetailComponent = /** @class */ (function () {
    function VehicleDetailComponent(purchaseService, activatedRoute, fb) {
        var _this = this;
        this.purchaseService = purchaseService;
        this.activatedRoute = activatedRoute;
        this.fb = fb;
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_3__["ReplaySubject"](1);
        this.activatedRoute.params.subscribe(function (params) {
            _this.vehicleId = params['id'];
        });
    }
    VehicleDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.getAllOffice();
        this.vehicleDetailForm.controls['OfficeId'].valueChanges.subscribe(function (x) {
            _this.getEmployeesByOfficeId(x);
        });
        this.markFormGroupTouched(this.vehicleDetailForm);
    };
    VehicleDetailComponent.prototype.ngOnChanges = function () {
        if (this.officeId !== undefined && this.officeId != null) {
            this.getEmployeesByOfficeId(this.officeId);
        }
    };
    VehicleDetailComponent.prototype.getEmployeesByOfficeId = function (officeId) {
        var _this = this;
        this.purchaseService.getEmployeesByOfficeId(officeId)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.employeeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__["of"])(x.data.map(function (y) {
                return {
                    name: y.CodeEmployeeName,
                    value: y.EmployeeId
                };
            }));
        });
    };
    VehicleDetailComponent.prototype.getAllOffice = function () {
        var _this = this;
        this.purchaseService.getAllOfficeList()
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.offices$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__["of"])(x.data.OfficeDetailsList.map(function (y) {
                return {
                    value: y.OfficeId,
                    name: y.OfficeCode + '-' + y.OfficeName
                };
            }));
        });
    };
    VehicleDetailComponent.prototype.onSubmit = function () {
        // to validate child vehicle form from add purchase form
        this.markFormGroupTouched(this.vehicleDetailForm);
        this.vehicleDetailForm.updateValueAndValidity();
    };
    VehicleDetailComponent.prototype.markFormGroupTouched = function (formGroup) {
        var _this = this;
        Object.values(formGroup.controls).forEach(function (control) {
            control.markAsTouched();
            if (control.controls) {
                _this.markFormGroupTouched(control);
            }
        });
    };
    VehicleDetailComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], VehicleDetailComponent.prototype, "officeId", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormGroup"])
    ], VehicleDetailComponent.prototype, "vehicleDetailForm", void 0);
    VehicleDetailComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-vehicle-detail',
            template: __webpack_require__(/*! ./vehicle-detail.component.html */ "./src/app/store/components/vehicle-detail/vehicle-detail.component.html"),
            styles: [__webpack_require__(/*! ./vehicle-detail.component.scss */ "./src/app/store/components/vehicle-detail/vehicle-detail.component.scss")]
        }),
        __metadata("design:paramtypes", [_services_purchase_service__WEBPACK_IMPORTED_MODULE_2__["PurchaseService"], _angular_router__WEBPACK_IMPORTED_MODULE_6__["ActivatedRoute"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"]])
    ], VehicleDetailComponent);
    return VehicleDetailComponent;
}());



/***/ }),

/***/ "./src/app/store/components/vehicle-details/vehicle-details.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/vehicle-details/vehicle-details.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">\r\n    <a class=\"back_arrow\" routerLink=\"/store/vehicle/tracker\">\r\n      <i class=\"fas fa-arrow-left\"></i>\r\n    </a>\r\n    Vehicle Details\r\n    <hum-button [type]=\"'add'\" [text]=\"'Add Mileage'\" (click)=\"openMilageModal()\"></hum-button>\r\n    <hum-button [type]=\"'text'\" [text]=\"'Edit Details'\" (click)=\"editVehicleDetail()\"></hum-button>\r\n  </span>\r\n  <div class=\"action_section\">\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n<mat-tab-group [ngStyle]=\"scrollStyles\" (selectedTabChange)=\"onTabClick($event)\">\r\n  <mat-tab label=\"DETAILS\">\r\n    <ng-template matTabContent>\r\n      <ng-container *ngTemplateOutlet=\"vehicleDetails\"></ng-container>\r\n    </ng-template>\r\n  </mat-tab>\r\n  <mat-tab label=\"MONTHLY BREAKDOWN\">\r\n    <div style=\"padding-left: 50px;\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-2\">\r\n          <div class=\"padding_top_10\">\r\n            <mat-form-field>\r\n              <mat-label>Year</mat-label>\r\n              <mat-select [(ngModel)]=\"monthlyBreakdownYear\" (selectionChange)='getVehicleMonthlyBreakdownData()'>\r\n                <mat-option *ngFor=\"let item of monthlyBreakdownYearList$ | async\" [value]=\"item.value\">\r\n                  {{item.name}}\r\n                </mat-option>\r\n              </mat-select>\r\n            </mat-form-field>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card>\r\n            <p>Starting Mileage:</p>\r\n            <h4>{{vehicleMonthlyBreakdownList.StartingMileage}}</h4>\r\n          </mat-card>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card>\r\n            <p>Incurred Mileage:</p>\r\n            <h4>{{vehicleMonthlyBreakdownList.IncurredMileage}}</h4>\r\n          </mat-card>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card title=\"Standard Fuel Consumption Rate\">\r\n            <p>SFC Rate:</p>\r\n            <h4>{{vehicleMonthlyBreakdownList.StandardFuelConsumptionRate}}</h4>\r\n          </mat-card>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card title=\"Standard Mobil Oil Consumption Rate\">\r\n            <p>SMOC Rate:</p>\r\n            <h4>{{vehicleMonthlyBreakdownList.StandardMobilOilConsumptionRate}}</h4>\r\n          </mat-card>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <mat-card>\r\n            <p>Starting Cost:</p>\r\n            <h4>{{vehicleMonthlyBreakdownList.StartingCost}}</h4>\r\n          </mat-card>\r\n        </div>\r\n      </div>\r\n    </div>\r\n\r\n    <mat-card style=\"margin-top: 5px;\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-12\">\r\n            <table class=\"table table-striped\">\r\n              <thead>\r\n                <th>\r\n                  <h5>Vehicle Usage Analysis</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jan</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Feb</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Mar</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Apr</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>May</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jun</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jul</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Aug</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Sept</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Oct</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Nov</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Dec</h5>\r\n                </th>\r\n              </thead>\r\n              <tbody>\r\n                <tr *ngFor=\"let item of vehicleMonthlyBreakdownList.UsageAnalysisBreakDownList\">\r\n                  <td>{{item.Header}}</td>\r\n                  <td>{{item.January==0 ? '-' : item.January}}</td>\r\n                  <td>{{item.February==0 ? '-': item.February}}</td>\r\n                  <td>{{item.March==0 ? '-': item.March}}</td>\r\n                  <td>{{item.April==0? '-': item.April}}</td>\r\n                  <td>{{item.May==0? '-': item.May}}</td>\r\n                  <td>{{item.June==0? '-': item.June}}</td>\r\n                  <td>{{item.July==0? '-': item.July}}</td>\r\n                  <td>{{item.August==0? '-': item.August}}</td>\r\n                  <td>{{item.September==0? '-': item.September}}</td>\r\n                  <td>{{item.October==0? '-': item.October}}</td>\r\n                  <td>{{item.November==0? '-': item.November}}</td>\r\n                  <td>{{item.December==0? '-': item.December}}</td>\r\n                </tr>\r\n              </tbody>\r\n            </table>\r\n\r\n            <table class=\"table table-striped\">\r\n              <thead>\r\n                <th>\r\n                  <h5>Vehicle Cost Analysis</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jan</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Feb</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Mar</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Apr</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>May</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jun</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Jul</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Aug</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Sept</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Oct</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Nov</h5>\r\n                </th>\r\n                <th>\r\n                  <h5>Dec</h5>\r\n                </th>\r\n              </thead>\r\n              <tbody>\r\n                <tr *ngFor=\"let item of vehicleMonthlyBreakdownList.CostAnalysisBreakDownList\">\r\n                  <td>{{item.Header}}</td>\r\n                  <td>{{item.January==0 ? '-' : item.January}}</td>\r\n                  <td>{{item.February==0 ? '-': item.February}}</td>\r\n                  <td>{{item.March==0 ? '-': item.March}}</td>\r\n                  <td>{{item.April==0? '-': item.April}}</td>\r\n                  <td>{{item.May==0? '-': item.May}}</td>\r\n                  <td>{{item.June==0? '-': item.June}}</td>\r\n                  <td>{{item.July==0? '-': item.July}}</td>\r\n                  <td>{{item.August==0? '-': item.August}}</td>\r\n                  <td>{{item.September==0? '-': item.September}}</td>\r\n                  <td>{{item.October==0? '-': item.October}}</td>\r\n                  <td>{{item.November==0? '-': item.November}}</td>\r\n                  <td>{{item.December==0? '-': item.December}}</td>\r\n                </tr>\r\n              </tbody>\r\n            </table>\r\n\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </mat-card>\r\n\r\n  </mat-tab>\r\n  <mat-tab label=\"LOGS\">\r\n    <ng-template matTabContent>\r\n      <app-logs [transportType]=\"transportType\" [entityId]=\"vehicleId\"></app-logs>\r\n    </ng-template>\r\n  </mat-tab>\r\n</mat-tab-group>\r\n<!-- Vehicle Details template-->\r\n<ng-template #vehicleDetails>\r\n  <mat-card>\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n        <div class=\"col-md-6\">\r\n          <h5>Vehicle Usage Analysis</h5>\r\n          <table class=\"table table-striped\">\r\n            <tbody>\r\n              <tr>\r\n                <td>Incurred Mileage(KM)</td>\r\n\r\n                <td>{{vehicleDetailForm.IncurredMileage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Starting Mileage(KM)</td>\r\n                <td>{{vehicleDetailForm.StartingMileage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Current Mileage (KM)</td>\r\n                <td>{{vehicleDetailForm.CurrentMileage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Total Fuel Usage (Litres)</td>\r\n                <td>{{vehicleDetailForm.TotalFuelUsage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Total Mobil Oil Usage (Litres)</td>\r\n                <td>{{vehicleDetailForm.TotalMobilOilUsage}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Standard Fuel Consumption Rate (Litre Per 100 KM)</td>\r\n                <td>{{vehicleDetailForm.StandardFuelConsumptionRate}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Actual Fuel Consumption Rate (Litre Per 100 KM)</td>\r\n                <td>{{vehicleDetailForm.ActualFuelConsumptionRate}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Standard Mobil Oil Consumption Rate (Litre Per 100 KM)</td>\r\n                <td>{{vehicleDetailForm.StandardMobilOilConsumptionRate}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Actual Mobil Oil Consumption Rate (Litre Per 100 KM)</td>\r\n                <td>{{vehicleDetailForm.ActualMobilOilConsumptionRate}}</td>\r\n\r\n              </tr>\r\n            </tbody>\r\n          </table>\r\n          <h5>Vehicle Cost Analysis</h5>\r\n          <table class=\"table table-striped\">\r\n            <tbody>\r\n              <tr>\r\n                <td>Fuel Total Cost</td>\r\n\r\n                <td>{{vehicleDetailForm.FuelTotalCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Mobil Oil Total Cost</td>\r\n                <td>{{vehicleDetailForm.MobilOilTotalCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Spare Parts Total Cost</td>\r\n                <td>{{vehicleDetailForm.SparePartsTotalCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Services & Maintenance Total Cost</td>\r\n                <td>{{vehicleDetailForm.ServiceAndMaintenanceTotalCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Vehicle Starting Cost</td>\r\n                <td>{{vehicleDetailForm.VehicleStartingCost}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Total Cost</td>\r\n                <td>{{vehicleDetailForm.FuelTotalCost+\r\n                  vehicleDetailForm.MobilOilTotalCost+\r\n                  vehicleDetailForm.SparePartsTotalCost+\r\n                  vehicleDetailForm.ServiceAndMaintenanceTotalCost+\r\n                  vehicleDetailForm.VehicleStartingCost\r\n                }}</td>\r\n              </tr>\r\n            </tbody>\r\n          </table>\r\n\r\n        </div>\r\n        <div class=\"col-md-6\">\r\n          <h5>Vehicle Details</h5>\r\n          <table class=\"table table-striped\">\r\n            <tbody>\r\n              <tr>\r\n                <td>Plate No</td>\r\n\r\n                <td>{{vehicleDetailForm.PlateNo}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Driver</td>\r\n                <td>{{vehicleDetailForm.EmployeeName}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Model Year</td>\r\n                <td>{{vehicleDetailForm.ModelYear}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>Location</td>\r\n                <td>{{vehicleDetailForm.OfficeName}}</td>\r\n              </tr>\r\n              <tr>\r\n                <td>PurchaseId</td>\r\n                <td><a *ngIf=\"vehicleDetailForm && vehicleDetailForm.PurchaseId != null\"\r\n                    [routerLink]=\"['/store/purchase/edit', vehicleDetailForm.PurchaseId]\">{{vehicleDetailForm.PurchaseName}}</a>\r\n                </td>\r\n\r\n              </tr>\r\n            </tbody>\r\n          </table>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </mat-card>\r\n\r\n</ng-template>\r\n"

/***/ }),

/***/ "./src/app/store/components/vehicle-details/vehicle-details.component.scss":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/vehicle-details/vehicle-details.component.scss ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvdmVoaWNsZS1kZXRhaWxzL3ZlaGljbGUtZGV0YWlscy5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/store/components/vehicle-details/vehicle-details.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/store/components/vehicle-details/vehicle-details.component.ts ***!
  \*******************************************************************************/
/*! exports provided: VehicleDetailsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VehicleDetailsComponent", function() { return VehicleDetailsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _add_milage_add_milage_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../add-milage/add-milage.component */ "./src/app/store/components/add-milage/add-milage.component.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};









var VehicleDetailsComponent = /** @class */ (function () {
    function VehicleDetailsComponent(dialog, router, activatedRoute, purchaseService) {
        var _this = this;
        this.dialog = dialog;
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.purchaseService = purchaseService;
        this.date = new _angular_forms__WEBPACK_IMPORTED_MODULE_7__["FormControl"]();
        this.transportType = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_8__["TransportItemCategory"].Vehicle;
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5__["ReplaySubject"](1);
        this.activatedRoute.params.subscribe(function (params) {
            _this.vehicleId = params['id'];
        });
    }
    VehicleDetailsComponent.prototype.ngOnInit = function () {
        this.getScreenSize();
        this.initForm();
        this.getVehicleDetailById();
        this.getMonthlyBreakDownYears();
    };
    VehicleDetailsComponent.prototype.initForm = function () {
        this.vehicleDetailForm = {
            VehicleId: null,
            PlateNo: null,
            EmployeeId: null,
            StartingMileage: null,
            IncurredMileage: null,
            StandardMobilOilConsumptionRate: null,
            ModelYear: null,
            OfficeId: null,
            StandardFuelConsumptionRate: null,
            EmployeeName: null,
            PurchaseName: null,
            PurchaseId: null,
            OfficeName: null,
            TotalFuelUsage: null,
            TotalMobilOilUsage: null,
            ActualFuelConsumptionRate: null,
            ActualMobilOilConsumptionRate: null,
            FuelTotalCost: null,
            MobilOilTotalCost: null,
            SparePartsTotalCost: null,
            ServiceAndMaintenanceTotalCost: null,
            CurrentMileage: null,
            VehicleStartingCost: null
        };
        this.vehicleMonthlyBreakdownList = {
            StartingMileage: null,
            IncurredMileage: null,
            StandardMobilOilConsumptionRate: null,
            StandardFuelConsumptionRate: null,
            StartingCost: null,
            CostAnalysisBreakDownList: [],
            UsageAnalysisBreakDownList: []
        };
    };
    //#region "Dynamic Scroll"
    VehicleDetailsComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    VehicleDetailsComponent.prototype.openMilageModal = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_milage_add_milage_component__WEBPACK_IMPORTED_MODULE_2__["AddMilageComponent"], {
            width: '850px',
            data: {
                vehicleId: this.vehicleId
            }
        });
        dialogRef.afterClosed().subscribe(function (result) {
            _this.getVehicleDetailById();
            _this.getVehicleMonthlyBreakdownData();
        });
    };
    VehicleDetailsComponent.prototype.getVehicleDetailById = function () {
        var _this = this;
        this.purchaseService.getVehicleDetailById(this.vehicleId)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.vehicleDetailForm = {
                VehicleId: x.VehicleId,
                PlateNo: x.PlateNo,
                EmployeeId: x.EmployeeId,
                StartingMileage: x.StartingMileage,
                IncurredMileage: x.IncurredMileage,
                StandardMobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
                ModelYear: x.ModelYear,
                OfficeId: x.OfficeId,
                StandardFuelConsumptionRate: x.StandardFuelConsumptionRate,
                EmployeeName: x.EmployeeName,
                PurchaseName: x.PurchaseName,
                PurchaseId: x.PurchaseId,
                OfficeName: x.OfficeName,
                TotalFuelUsage: x.TotalFuelUsage,
                TotalMobilOilUsage: x.TotalMobilOilUsage,
                ActualFuelConsumptionRate: x.ActualFuelConsumptionRate,
                ActualMobilOilConsumptionRate: x.ActualMobilOilConsumptionRate,
                FuelTotalCost: x.FuelTotalCost,
                MobilOilTotalCost: x.MobilOilTotalCost,
                SparePartsTotalCost: x.SparePartsTotalCost,
                ServiceAndMaintenanceTotalCost: x.ServiceAndMaintenanceTotalCost,
                CurrentMileage: x.CurrentMileage,
                VehicleStartingCost: x.VehicleStartingCost
            };
        });
    };
    VehicleDetailsComponent.prototype.onTabClick = function (event) {
        if (event.index === 1) {
            this.getVehicleMonthlyBreakdownData();
        }
        else if (event.index === 0) {
            this.getVehicleDetailById();
        }
    };
    VehicleDetailsComponent.prototype.getVehicleMonthlyBreakdownData = function () {
        var _this = this;
        var data = {
            VehicleId: +this.vehicleId,
            SelectedYear: this.monthlyBreakdownYear
        };
        this.purchaseService.getVehicleMonthlyBreakdown(data)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.vehicleMonthlyBreakdownList = {
                StartingMileage: x.StartingMileage,
                IncurredMileage: x.IncurredMileage,
                StandardMobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
                StandardFuelConsumptionRate: x.StandardFuelConsumptionRate,
                StartingCost: x.StartingCost,
                CostAnalysisBreakDownList: x.CostAnalysisBreakDownList,
                UsageAnalysisBreakDownList: x.UsageAnalysisBreakDownList
            };
        });
    };
    VehicleDetailsComponent.prototype.getMonthlyBreakdownYear = function (event) {
        this.monthlyBreakdownYear = event;
    };
    VehicleDetailsComponent.prototype.editVehicleDetail = function () {
        this.router.navigate(['store/vehicle/edit', this.vehicleId]);
    };
    VehicleDetailsComponent.prototype.monthSelected = function (params) {
        this.date.setValue(params);
        this.picker.close();
    };
    VehicleDetailsComponent.prototype.getMonthlyBreakDownYears = function () {
        var _this = this;
        this.monthlyBreakdownYearList$ = this.purchaseService.getPreviousYearsList(10);
        this.monthlyBreakdownYearList$.subscribe(function (x) {
            _this.monthlyBreakdownYear = x[0].value;
        });
    };
    VehicleDetailsComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])(_angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDatepicker"]),
        __metadata("design:type", Object)
    ], VehicleDetailsComponent.prototype, "picker", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], VehicleDetailsComponent.prototype, "getScreenSize", null);
    VehicleDetailsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-vehicle-details',
            template: __webpack_require__(/*! ./vehicle-details.component.html */ "./src/app/store/components/vehicle-details/vehicle-details.component.html"),
            styles: [__webpack_require__(/*! ./vehicle-details.component.scss */ "./src/app/store/components/vehicle-details/vehicle-details.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_1__["MatDialog"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["ActivatedRoute"], _services_purchase_service__WEBPACK_IMPORTED_MODULE_4__["PurchaseService"]])
    ], VehicleDetailsComponent);
    return VehicleDetailsComponent;
}());



/***/ }),

/***/ "./src/app/store/components/vehicle-filters/vehicle-filters.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/vehicle-filters/vehicle-filters.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"row\">\r\n  <div class=\"col-md-12\">\r\n    <span class=\"pull-left\">\r\n      <h4>Filters</h4>\r\n    </span>\r\n    <span class=\"pull-right\">\r\n      <!-- <hum-button [type]=\"'filter'\" [text]=\"'advance filters'\" (click)=\"isBasic=!isBasic\"></hum-button> -->\r\n      <hum-button [type]=\"'clear'\" [text]=\"'clear filters'\" (click)=\"clearFilters()\"></hum-button>\r\n    </span>\r\n  </div>\r\n</div>\r\n<div class=\"row\">\r\n  <form [formGroup]= \"vehicleTrackerFilterForm\">\r\n  <div class=\"col-md-12\">\r\n    <div class=\"col-md-3\">\r\n        <mat-form-field class=\"example-full-width\">\r\n            <input matInput formControlName=\"PlateNo\" placeholder=\"Plate No\">\r\n          </mat-form-field>\r\n    </div>\r\n    <div class=\"col-md-3\">\r\n        <lib-hum-dropdown [options]=\"officeList$\" formControlName=\"OfficeId\" [placeHolder]=\"'Office'\"\r\n        (change)=\"getOfficeSelectedValue($event)\"></lib-hum-dropdown>\r\n    </div>\r\n    <div class=\"col-md-3\">\r\n      <lib-hum-dropdown [options]=\"employeeList$\" formControlName=\"EmployeeId\" [placeHolder]=\"'Driver'\"\r\n        ></lib-hum-dropdown>\r\n  </div>\r\n    <div class=\"col-md-3\">\r\n        <mat-form-field class=\"example-full-width\">\r\n            <input matInput type=\"number\" placeholder=\"Total Cost\" formControlName=\"TotalCost\">\r\n        </mat-form-field>\r\n    </div>\r\n  </div>\r\n  </form>\r\n  </div>\r\n  <div class=\"row\">\r\n    <div class=\"col-md-10\"></div>\r\n      <div class=\"col-md-2\">\r\n          <hum-button [type]=\"'filter'\" [text]=\"'Apply Filter'\" (click)=\"onApplyFilters()\"></hum-button>\r\n      </div>\r\n  </div>\r\n"

/***/ }),

/***/ "./src/app/store/components/vehicle-filters/vehicle-filters.component.scss":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/vehicle-filters/vehicle-filters.component.scss ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvdmVoaWNsZS1maWx0ZXJzL3ZlaGljbGUtZmlsdGVycy5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/store/components/vehicle-filters/vehicle-filters.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/store/components/vehicle-filters/vehicle-filters.component.ts ***!
  \*******************************************************************************/
/*! exports provided: VehicleFiltersComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VehicleFiltersComponent", function() { return VehicleFiltersComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var VehicleFiltersComponent = /** @class */ (function () {
    function VehicleFiltersComponent(_fb, purchaseService) {
        this._fb = _fb;
        this.purchaseService = purchaseService;
        this.isBasic = true;
        this.applyFilterEvent = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_5__["ReplaySubject"](1);
        this.vehicleTrackerFilterForm = this._fb.group({
            'PlateNo': [null],
            'EmployeeId': [null],
            'OfficeId': [null],
            'TotalCost': [null]
        });
    }
    VehicleFiltersComponent.prototype.ngOnInit = function () {
        this.getAllOffice();
    };
    VehicleFiltersComponent.prototype.getAllOffice = function () {
        var _this = this;
        this.purchaseService.getAllOfficeList()
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.officeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(response.data.OfficeDetailsList.map(function (y) {
                return {
                    value: y.OfficeId,
                    name: y.OfficeCode + '-' + y.OfficeName
                };
            }));
        });
    };
    VehicleFiltersComponent.prototype.getOfficeSelectedValue = function (event) {
        this.getEmployeesByOfficeId(event);
    };
    VehicleFiltersComponent.prototype.getEmployeesByOfficeId = function (officeId) {
        var _this = this;
        this.purchaseService.getEmployeesByOfficeId(officeId)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (x) {
            _this.employeeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_3__["of"])(x.data.map(function (y) {
                return {
                    name: y.CodeEmployeeName,
                    value: y.EmployeeId
                };
            }));
        });
    };
    VehicleFiltersComponent.prototype.clearFilters = function () {
        this.vehicleTrackerFilterForm.reset();
    };
    VehicleFiltersComponent.prototype.onApplyFilters = function () {
        this.applyFilterEvent.emit(this.vehicleTrackerFilterForm.value);
    };
    VehicleFiltersComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], VehicleFiltersComponent.prototype, "applyFilterEvent", void 0);
    VehicleFiltersComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-vehicle-filters',
            template: __webpack_require__(/*! ./vehicle-filters.component.html */ "./src/app/store/components/vehicle-filters/vehicle-filters.component.html"),
            styles: [__webpack_require__(/*! ./vehicle-filters.component.scss */ "./src/app/store/components/vehicle-filters/vehicle-filters.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _services_purchase_service__WEBPACK_IMPORTED_MODULE_2__["PurchaseService"]])
    ], VehicleFiltersComponent);
    return VehicleFiltersComponent;
}());



/***/ }),

/***/ "./src/app/store/components/vehicle-tracker/vehicle-tracker.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/vehicle-tracker/vehicle-tracker.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">Vehicle Tracker\r\n    <!-- <hum-button [type]=\"'down'\" [text]=\"'DISPLAY CURRENCY'\"></hum-button> -->\r\n  </span>\r\n  <div class=\"action_section\">\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n<mat-card [ngStyle]=\"scrollStyles\">\r\n    <div style=\"padding-left: 50px;\">\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n          <mat-form-field>\r\n              <mat-label>Display Currency</mat-label>\r\n              <mat-select [(ngModel)]=\"selectedDisplayCurrency\" (selectionChange)='selectedDisplayCurrencyChanged()'>\r\n                <mat-option *ngFor=\"let currency of currencyList$ | async\" [value]=\"currency.value\">\r\n                  {{currency.name}}\r\n                </mat-option>\r\n              </mat-select>\r\n            </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </div>\r\n<mat-card [ngStyle]=\"scrollStyles\">\r\n  <div class=\"container\">\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n        <app-vehicle-filters (applyFilterEvent)='getFilteredVehicleList($event)'></app-vehicle-filters>\r\n        <mat-divider></mat-divider>\r\n        <h4>Vehicles</h4>\r\n        <hum-table [headers]=\"vehicleListHeaders$\" [items]=\"vehicleList$\"\r\n          (actionClick)=\"openMilageModal($event)\" [actions]=\"actions\" (rowClick)=\"goToDetails($event)\"></hum-table>\r\n        <mat-paginator [length]=\"recordsCount\" [pageSize]=\"vehicleTrackerFilter.pageSize\"\r\n          [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n        </mat-paginator>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</mat-card>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/store/components/vehicle-tracker/vehicle-tracker.component.scss":
/*!*********************************************************************************!*\
  !*** ./src/app/store/components/vehicle-tracker/vehicle-tracker.component.scss ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3N0b3JlL2NvbXBvbmVudHMvdmVoaWNsZS10cmFja2VyL3ZlaGljbGUtdHJhY2tlci5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/store/components/vehicle-tracker/vehicle-tracker.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/store/components/vehicle-tracker/vehicle-tracker.component.ts ***!
  \*******************************************************************************/
/*! exports provided: VehicleTrackerComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VehicleTrackerComponent", function() { return VehicleTrackerComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _add_milage_add_milage_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../add-milage/add-milage.component */ "./src/app/store/components/add-milage/add-milage.component.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_purchase_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../services/purchase.service */ "./src/app/store/services/purchase.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var VehicleTrackerComponent = /** @class */ (function () {
    function VehicleTrackerComponent(dialog, router, purchaseService) {
        this.dialog = dialog;
        this.router = router;
        this.purchaseService = purchaseService;
        this.vehicleListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(['Vechile Id', 'Plate No', 'Driver', 'Fuel Consumption Rate', 'Total Mileage (KM)', 'Total Cost',
            'Original Cost']);
        // Paging
        this.pageSize = 10;
        this.pageIndex = 0;
        this.recordsCount = 0;
    }
    VehicleTrackerComponent.prototype.ngOnInit = function () {
        this.initializeModel();
        this.actions = {
            items: {
                button: { status: true, text: 'Add Mileage' },
                delete: false,
                download: false,
            },
            subitems: {}
        };
        this.getScreenSize();
        this.getAllCurrencies();
    };
    VehicleTrackerComponent.prototype.initializeModel = function () {
        this.vehicleTrackerFilter = {
            EmployeeId: null,
            OfficeId: null,
            PlateNo: null,
            TotalCost: null,
            DisplayCurrency: null,
            pageIndex: 0,
            pageSize: 10
        };
    };
    //#region "Dynamic Scroll"
    VehicleTrackerComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    VehicleTrackerComponent.prototype.getFilteredVehicleList = function (selectedFilter) {
        this.vehicleTrackerFilter = {
            TotalCost: selectedFilter.TotalCost,
            EmployeeId: selectedFilter.EmployeeId,
            OfficeId: selectedFilter.OfficeId,
            PlateNo: selectedFilter.PlateNo,
            DisplayCurrency: this.selectedDisplayCurrency,
            pageSize: 10,
            pageIndex: 0
        };
        this.getVehicleList(this.vehicleTrackerFilter);
    };
    VehicleTrackerComponent.prototype.getVehicleList = function (filter) {
        var _this = this;
        this.purchaseService.getVehicleList(filter)
            .subscribe(function (response) {
            _this.recordsCount = response.TotalRecords;
            _this.vehicleList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.VehicleList.map(function (element) {
                return {
                    VehicleId: element.VehicleId,
                    PlateNo: element.PlateNo,
                    Driver: element.EmployeeName,
                    FCRate: element.FuelConsumptionRate,
                    TotalMileage: element.TotalMileage,
                    TotalCost: element.TotalCost,
                    OriginalCost: element.OriginalCost,
                };
            }));
        });
    };
    VehicleTrackerComponent.prototype.goToDetails = function (e) {
        this.router.navigate(['store/vehicle/detail', e.VehicleId]);
    };
    VehicleTrackerComponent.prototype.openMilageModal = function (event) {
        if (event.type === 'button') {
            var dialogRef = this.dialog.open(_add_milage_add_milage_component__WEBPACK_IMPORTED_MODULE_3__["AddMilageComponent"], {
                width: '850px',
                data: {
                    vehicleId: event.item.VehicleId
                }
            });
        }
    };
    VehicleTrackerComponent.prototype.getAllCurrencies = function () {
        var _this = this;
        this.purchaseService.getAllCurrencies()
            .subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.selectedDisplayCurrency = x.data.CurrencyList[0].CurrencyId;
                _this.currencyList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(x.data.CurrencyList.map(function (y) {
                    return {
                        name: y.CurrencyCode + '-' + y.CurrencyName,
                        value: y.CurrencyId
                    };
                }));
                _this.vehicleTrackerFilter.DisplayCurrency = _this.selectedDisplayCurrency;
                _this.getVehicleList(_this.vehicleTrackerFilter);
            }
        }, function (error) {
            console.error(error);
        });
    };
    VehicleTrackerComponent.prototype.selectedDisplayCurrencyChanged = function () {
        this.vehicleTrackerFilter.DisplayCurrency = this.selectedDisplayCurrency;
        this.getVehicleList(this.vehicleTrackerFilter);
    };
    //#region "pageEvent"
    VehicleTrackerComponent.prototype.pageEvent = function (e) {
        this.vehicleTrackerFilter.pageIndex = e.pageIndex;
        this.vehicleTrackerFilter.pageSize = e.pageSize;
        this.getVehicleList(this.vehicleTrackerFilter);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], VehicleTrackerComponent.prototype, "getScreenSize", null);
    VehicleTrackerComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-vehicle-tracker',
            template: __webpack_require__(/*! ./vehicle-tracker.component.html */ "./src/app/store/components/vehicle-tracker/vehicle-tracker.component.html"),
            styles: [__webpack_require__(/*! ./vehicle-tracker.component.scss */ "./src/app/store/components/vehicle-tracker/vehicle-tracker.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material__WEBPACK_IMPORTED_MODULE_2__["MatDialog"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"],
            _services_purchase_service__WEBPACK_IMPORTED_MODULE_5__["PurchaseService"]])
    ], VehicleTrackerComponent);
    return VehicleTrackerComponent;
}());



/***/ }),

/***/ "./src/app/store/models/store-configuration.ts":
/*!*****************************************************!*\
  !*** ./src/app/store/models/store-configuration.ts ***!
  \*****************************************************/
/*! exports provided: MasterInventoryModel, MasterItemGroupModel, MasterInventoryItemModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "MasterInventoryModel", function() { return MasterInventoryModel; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "MasterItemGroupModel", function() { return MasterItemGroupModel; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "MasterInventoryItemModel", function() { return MasterInventoryItemModel; });
var MasterInventoryModel = /** @class */ (function () {
    function MasterInventoryModel() {
    }
    return MasterInventoryModel;
}());

var MasterItemGroupModel = /** @class */ (function () {
    function MasterItemGroupModel() {
    }
    return MasterItemGroupModel;
}());

var MasterInventoryItemModel = /** @class */ (function () {
    function MasterInventoryItemModel() {
    }
    return MasterInventoryItemModel;
}());



/***/ }),

/***/ "./src/app/store/store-routing.module.ts":
/*!***********************************************!*\
  !*** ./src/app/store/store-routing.module.ts ***!
  \***********************************************/
/*! exports provided: StoreRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StoreRoutingModule", function() { return StoreRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _components_purchase_list_purchase_list_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./components/purchase-list/purchase-list.component */ "./src/app/store/components/purchase-list/purchase-list.component.ts");
/* harmony import */ var _components_add_purchase_add_purchase_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/add-purchase/add-purchase.component */ "./src/app/store/components/add-purchase/add-purchase.component.ts");
/* harmony import */ var _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./components/entry-component/entry-component.component */ "./src/app/store/components/entry-component/entry-component.component.ts");
/* harmony import */ var _components_vehicle_tracker_vehicle_tracker_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./components/vehicle-tracker/vehicle-tracker.component */ "./src/app/store/components/vehicle-tracker/vehicle-tracker.component.ts");
/* harmony import */ var _components_generator_tracker_generator_tracker_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./components/generator-tracker/generator-tracker.component */ "./src/app/store/components/generator-tracker/generator-tracker.component.ts");
/* harmony import */ var _components_vehicle_details_vehicle_details_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./components/vehicle-details/vehicle-details.component */ "./src/app/store/components/vehicle-details/vehicle-details.component.ts");
/* harmony import */ var _components_generator_details_generator_details_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./components/generator-details/generator-details.component */ "./src/app/store/components/generator-details/generator-details.component.ts");
/* harmony import */ var _components_edit_vehicle_edit_vehicle_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./components/edit-vehicle/edit-vehicle.component */ "./src/app/store/components/edit-vehicle/edit-vehicle.component.ts");
/* harmony import */ var _components_edit_generator_edit_generator_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./components/edit-generator/edit-generator.component */ "./src/app/store/components/edit-generator/edit-generator.component.ts");
/* harmony import */ var _components_store_configuration_store_configuration_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./components/store-configuration/store-configuration.component */ "./src/app/store/components/store-configuration/store-configuration.component.ts");
/* harmony import */ var _components_procurement_control_panel_procurement_control_panel_component__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./components/procurement-control-panel/procurement-control-panel.component */ "./src/app/store/components/procurement-control-panel/procurement-control-panel.component.ts");
/* harmony import */ var _components_add_procurements_add_procurements_component__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./components/add-procurements/add-procurements.component */ "./src/app/store/components/add-procurements/add-procurements.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};














var routes = [
    {
        path: '', component: _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_4__["EntryComponentComponent"],
        children: [
            { path: 'purchases', component: _components_purchase_list_purchase_list_component__WEBPACK_IMPORTED_MODULE_2__["PurchaseListComponent"] },
            { path: 'purchase/add', component: _components_add_purchase_add_purchase_component__WEBPACK_IMPORTED_MODULE_3__["AddPurchaseComponent"] },
            { path: 'purchase/edit/:id', component: _components_add_purchase_add_purchase_component__WEBPACK_IMPORTED_MODULE_3__["AddPurchaseComponent"] },
            { path: 'vehicle/tracker', component: _components_vehicle_tracker_vehicle_tracker_component__WEBPACK_IMPORTED_MODULE_5__["VehicleTrackerComponent"] },
            { path: 'generator/tracker', component: _components_generator_tracker_generator_tracker_component__WEBPACK_IMPORTED_MODULE_6__["GeneratorTrackerComponent"] },
            { path: 'vehicle/detail/:id', component: _components_vehicle_details_vehicle_details_component__WEBPACK_IMPORTED_MODULE_7__["VehicleDetailsComponent"] },
            { path: 'generator/detail/:id', component: _components_generator_details_generator_details_component__WEBPACK_IMPORTED_MODULE_8__["GeneratorDetailsComponent"] },
            { path: 'vehicle/edit/:id', component: _components_edit_vehicle_edit_vehicle_component__WEBPACK_IMPORTED_MODULE_9__["EditVehicleComponent"] },
            { path: 'generator/edit/:id', component: _components_edit_generator_edit_generator_component__WEBPACK_IMPORTED_MODULE_10__["EditGeneratorComponent"] },
            { path: 'configuration', component: _components_store_configuration_store_configuration_component__WEBPACK_IMPORTED_MODULE_11__["StoreConfigurationComponent"] },
            { path: 'purchases/procurement-control-panel/:id', component: _components_procurement_control_panel_procurement_control_panel_component__WEBPACK_IMPORTED_MODULE_12__["ProcurementControlPanelComponent"] },
            { path: 'purchases/add-procurement', component: _components_add_procurements_add_procurements_component__WEBPACK_IMPORTED_MODULE_13__["AddProcurementsComponent"] },
            { path: 'purchases/edit-procurement/:id', component: _components_add_procurements_add_procurements_component__WEBPACK_IMPORTED_MODULE_13__["AddProcurementsComponent"] }
        ]
    }
];
var StoreRoutingModule = /** @class */ (function () {
    function StoreRoutingModule() {
    }
    StoreRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], StoreRoutingModule);
    return StoreRoutingModule;
}());



/***/ }),

/***/ "./src/app/store/store.module.ts":
/*!***************************************!*\
  !*** ./src/app/store/store.module.ts ***!
  \***************************************/
/*! exports provided: StoreModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StoreModule", function() { return StoreModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _store_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./store-routing.module */ "./src/app/store/store-routing.module.ts");
/* harmony import */ var _components_purchase_filters_purchase_filters_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/purchase-filters/purchase-filters.component */ "./src/app/store/components/purchase-filters/purchase-filters.component.ts");
/* harmony import */ var _components_purchase_list_purchase_list_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./components/purchase-list/purchase-list.component */ "./src/app/store/components/purchase-list/purchase-list.component.ts");
/* harmony import */ var _components_add_purchase_add_purchase_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./components/add-purchase/add-purchase.component */ "./src/app/store/components/add-purchase/add-purchase.component.ts");
/* harmony import */ var _components_add_procurements_add_procurements_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./components/add-procurements/add-procurements.component */ "./src/app/store/components/add-procurements/add-procurements.component.ts");
/* harmony import */ var _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./components/entry-component/entry-component.component */ "./src/app/store/components/entry-component/entry-component.component.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _shared_share_layout_module__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ../shared/share-layout.module */ "./src/app/shared/share-layout.module.ts");
/* harmony import */ var _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/material/datepicker */ "./node_modules/@angular/material/esm5/datepicker.es5.js");
/* harmony import */ var projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! projects/library/src/public_api */ "./projects/library/src/public_api.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_paginator__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material/paginator */ "./node_modules/@angular/material/esm5/paginator.es5.js");
/* harmony import */ var _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/material/checkbox */ "./node_modules/@angular/material/esm5/checkbox.es5.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _components_purchase_filed_config_purchase_filed_config_component__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./components/purchase-filed-config/purchase-filed-config.component */ "./src/app/store/components/purchase-filed-config/purchase-filed-config.component.ts");
/* harmony import */ var _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! @angular/material/progress-spinner */ "./node_modules/@angular/material/esm5/progress-spinner.es5.js");
/* harmony import */ var _components_document_upload_document_upload_component__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ./components/document-upload/document-upload.component */ "./src/app/store/components/document-upload/document-upload.component.ts");
/* harmony import */ var _components_vehicle_detail_vehicle_detail_component__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ./components/vehicle-detail/vehicle-detail.component */ "./src/app/store/components/vehicle-detail/vehicle-detail.component.ts");
/* harmony import */ var _components_generator_detail_generator_detail_component__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ./components/generator-detail/generator-detail.component */ "./src/app/store/components/generator-detail/generator-detail.component.ts");
/* harmony import */ var _components_vehicle_tracker_vehicle_tracker_component__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ./components/vehicle-tracker/vehicle-tracker.component */ "./src/app/store/components/vehicle-tracker/vehicle-tracker.component.ts");
/* harmony import */ var _components_generator_tracker_generator_tracker_component__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ./components/generator-tracker/generator-tracker.component */ "./src/app/store/components/generator-tracker/generator-tracker.component.ts");
/* harmony import */ var _components_generator_filters_generator_filters_component__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! ./components/generator-filters/generator-filters.component */ "./src/app/store/components/generator-filters/generator-filters.component.ts");
/* harmony import */ var _components_vehicle_filters_vehicle_filters_component__WEBPACK_IMPORTED_MODULE_24__ = __webpack_require__(/*! ./components/vehicle-filters/vehicle-filters.component */ "./src/app/store/components/vehicle-filters/vehicle-filters.component.ts");
/* harmony import */ var _components_generator_details_generator_details_component__WEBPACK_IMPORTED_MODULE_25__ = __webpack_require__(/*! ./components/generator-details/generator-details.component */ "./src/app/store/components/generator-details/generator-details.component.ts");
/* harmony import */ var _components_vehicle_details_vehicle_details_component__WEBPACK_IMPORTED_MODULE_26__ = __webpack_require__(/*! ./components/vehicle-details/vehicle-details.component */ "./src/app/store/components/vehicle-details/vehicle-details.component.ts");
/* harmony import */ var _components_add_milage_add_milage_component__WEBPACK_IMPORTED_MODULE_27__ = __webpack_require__(/*! ./components/add-milage/add-milage.component */ "./src/app/store/components/add-milage/add-milage.component.ts");
/* harmony import */ var _components_edit_vehicle_edit_vehicle_component__WEBPACK_IMPORTED_MODULE_28__ = __webpack_require__(/*! ./components/edit-vehicle/edit-vehicle.component */ "./src/app/store/components/edit-vehicle/edit-vehicle.component.ts");
/* harmony import */ var _components_edit_generator_edit_generator_component__WEBPACK_IMPORTED_MODULE_29__ = __webpack_require__(/*! ./components/edit-generator/edit-generator.component */ "./src/app/store/components/edit-generator/edit-generator.component.ts");
/* harmony import */ var _components_add_hours_add_hours_component__WEBPACK_IMPORTED_MODULE_30__ = __webpack_require__(/*! ./components/add-hours/add-hours.component */ "./src/app/store/components/add-hours/add-hours.component.ts");
/* harmony import */ var _components_logs_logs_component__WEBPACK_IMPORTED_MODULE_31__ = __webpack_require__(/*! ./components/logs/logs.component */ "./src/app/store/components/logs/logs.component.ts");
/* harmony import */ var _components_document_upload_add_document_component__WEBPACK_IMPORTED_MODULE_32__ = __webpack_require__(/*! ./components/document-upload/add-document.component */ "./src/app/store/components/document-upload/add-document.component.ts");
/* harmony import */ var _components_store_configuration_store_configuration_component__WEBPACK_IMPORTED_MODULE_33__ = __webpack_require__(/*! ./components/store-configuration/store-configuration.component */ "./src/app/store/components/store-configuration/store-configuration.component.ts");
/* harmony import */ var _components_store_item_config_store_item_config_component__WEBPACK_IMPORTED_MODULE_34__ = __webpack_require__(/*! ./components/store-item-config/store-item-config.component */ "./src/app/store/components/store-item-config/store-item-config.component.ts");
/* harmony import */ var _components_add_master_inventory_add_master_inventory_component__WEBPACK_IMPORTED_MODULE_35__ = __webpack_require__(/*! ./components/add-master-inventory/add-master-inventory.component */ "./src/app/store/components/add-master-inventory/add-master-inventory.component.ts");
/* harmony import */ var _components_add_item_category_add_item_category_component__WEBPACK_IMPORTED_MODULE_36__ = __webpack_require__(/*! ./components/add-item-category/add-item-category.component */ "./src/app/store/components/add-item-category/add-item-category.component.ts");
/* harmony import */ var _components_add_item_add_item_component__WEBPACK_IMPORTED_MODULE_37__ = __webpack_require__(/*! ./components/add-item/add-item.component */ "./src/app/store/components/add-item/add-item.component.ts");
/* harmony import */ var _angular_material_radio__WEBPACK_IMPORTED_MODULE_38__ = __webpack_require__(/*! @angular/material/radio */ "./node_modules/@angular/material/esm5/radio.es5.js");
/* harmony import */ var _components_procurement_control_panel_procurement_control_panel_component__WEBPACK_IMPORTED_MODULE_39__ = __webpack_require__(/*! ./components/procurement-control-panel/procurement-control-panel.component */ "./src/app/store/components/procurement-control-panel/procurement-control-panel.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};








































var StoreModule = /** @class */ (function () {
    function StoreModule() {
    }
    StoreModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _components_purchase_filters_purchase_filters_component__WEBPACK_IMPORTED_MODULE_3__["PurchaseFiltersComponent"],
                _components_purchase_list_purchase_list_component__WEBPACK_IMPORTED_MODULE_4__["PurchaseListComponent"],
                _components_add_purchase_add_purchase_component__WEBPACK_IMPORTED_MODULE_5__["AddPurchaseComponent"],
                _components_add_procurements_add_procurements_component__WEBPACK_IMPORTED_MODULE_6__["AddProcurementsComponent"],
                _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_7__["EntryComponentComponent"],
                _components_purchase_filed_config_purchase_filed_config_component__WEBPACK_IMPORTED_MODULE_16__["PurchaseFiledConfigComponent"],
                _components_document_upload_document_upload_component__WEBPACK_IMPORTED_MODULE_18__["DocumentUploadComponent"],
                _components_vehicle_detail_vehicle_detail_component__WEBPACK_IMPORTED_MODULE_19__["VehicleDetailComponent"],
                _components_generator_detail_generator_detail_component__WEBPACK_IMPORTED_MODULE_20__["GeneratorDetailComponent"],
                _components_vehicle_tracker_vehicle_tracker_component__WEBPACK_IMPORTED_MODULE_21__["VehicleTrackerComponent"],
                _components_generator_tracker_generator_tracker_component__WEBPACK_IMPORTED_MODULE_22__["GeneratorTrackerComponent"],
                _components_generator_filters_generator_filters_component__WEBPACK_IMPORTED_MODULE_23__["GeneratorFiltersComponent"],
                _components_vehicle_filters_vehicle_filters_component__WEBPACK_IMPORTED_MODULE_24__["VehicleFiltersComponent"],
                _components_generator_details_generator_details_component__WEBPACK_IMPORTED_MODULE_25__["GeneratorDetailsComponent"],
                _components_vehicle_details_vehicle_details_component__WEBPACK_IMPORTED_MODULE_26__["VehicleDetailsComponent"],
                _components_add_milage_add_milage_component__WEBPACK_IMPORTED_MODULE_27__["AddMilageComponent"],
                _components_edit_vehicle_edit_vehicle_component__WEBPACK_IMPORTED_MODULE_28__["EditVehicleComponent"],
                _components_edit_generator_edit_generator_component__WEBPACK_IMPORTED_MODULE_29__["EditGeneratorComponent"],
                _components_add_hours_add_hours_component__WEBPACK_IMPORTED_MODULE_30__["AddHoursComponent"],
                _components_logs_logs_component__WEBPACK_IMPORTED_MODULE_31__["LogsComponent"],
                _components_document_upload_add_document_component__WEBPACK_IMPORTED_MODULE_32__["AddDocumentComponent"],
                _components_store_configuration_store_configuration_component__WEBPACK_IMPORTED_MODULE_33__["StoreConfigurationComponent"],
                _components_store_item_config_store_item_config_component__WEBPACK_IMPORTED_MODULE_34__["StoreItemConfigComponent"],
                _components_add_master_inventory_add_master_inventory_component__WEBPACK_IMPORTED_MODULE_35__["AddMasterInventoryComponent"],
                _components_add_item_category_add_item_category_component__WEBPACK_IMPORTED_MODULE_36__["AddItemCategoryComponent"],
                _components_add_item_add_item_component__WEBPACK_IMPORTED_MODULE_37__["AddItemComponent"],
                _components_procurement_control_panel_procurement_control_panel_component__WEBPACK_IMPORTED_MODULE_39__["ProcurementControlPanelComponent"]
                // components
                //  DbsidebarComponent,
                // DbheaderComponent,
                // DbfooterComponent
            ],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _store_routing_module__WEBPACK_IMPORTED_MODULE_2__["StoreRoutingModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_12__["ReactiveFormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_12__["FormsModule"],
                // Material
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatMenuModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatIconModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatSidenavModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatCardModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatButtonModule"],
                _shared_share_layout_module__WEBPACK_IMPORTED_MODULE_9__["ShareLayoutModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_11__["SubHeaderTemplateModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_11__["LibraryModule"],
                _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_10__["MatDatepickerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatInputModule"],
                _angular_material_paginator__WEBPACK_IMPORTED_MODULE_13__["MatPaginatorModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatDividerModule"],
                _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_14__["MatCheckboxModule"],
                _angular_material_dialog__WEBPACK_IMPORTED_MODULE_15__["MatDialogModule"],
                _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_17__["MatProgressSpinnerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatListModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatExpansionModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatTabsModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatTreeModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatSelectModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatOptionModule"],
                _angular_material_radio__WEBPACK_IMPORTED_MODULE_38__["MatRadioModule"]
                // MatRangeDatepickerModule,
                // MatNativeDateModule
            ],
            entryComponents: [_components_add_procurements_add_procurements_component__WEBPACK_IMPORTED_MODULE_6__["AddProcurementsComponent"], _components_add_milage_add_milage_component__WEBPACK_IMPORTED_MODULE_27__["AddMilageComponent"],
                _components_add_hours_add_hours_component__WEBPACK_IMPORTED_MODULE_30__["AddHoursComponent"], _components_document_upload_add_document_component__WEBPACK_IMPORTED_MODULE_32__["AddDocumentComponent"], _components_add_master_inventory_add_master_inventory_component__WEBPACK_IMPORTED_MODULE_35__["AddMasterInventoryComponent"], _components_add_item_category_add_item_category_component__WEBPACK_IMPORTED_MODULE_36__["AddItemCategoryComponent"], _components_add_item_add_item_component__WEBPACK_IMPORTED_MODULE_37__["AddItemComponent"]]
        })
    ], StoreModule);
    return StoreModule;
}());



/***/ })

}]);
//# sourceMappingURL=store-store-module.js.map