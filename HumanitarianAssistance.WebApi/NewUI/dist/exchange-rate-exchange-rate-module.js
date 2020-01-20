(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["exchange-rate-exchange-rate-module"],{

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.html":
/*!*******************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.html ***!
  \*******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>\r\n    New Exchange Rate\r\n    <button mat-icon-button [mat-dialog-close]=\"data\" class=\"pull-right\">\r\n      <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n    </button>\r\n  </h1>\r\n  <div mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-12\">\r\n        <mat-horizontal-stepper\r\n          linear\r\n          #stepper\r\n          (selectionChange)=\"ExchangeRateGenerationStepChanged($event)\"\r\n        >\r\n          <mat-step [stepControl]=\"exchangeRateAddModel\">\r\n            <br />\r\n            <p>\r\n              Select currency and date you want to initiate exchange rates for\r\n            </p>\r\n            <br />\r\n            <form [formGroup]=\"exchangeRateAddModel\">\r\n              <ng-template matStepLabel>Fill Currency</ng-template>\r\n              <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-6\">\r\n                      <mat-form-field class=\"example-full-width\">\r\n                        <mat-select\r\n                          placeholder=\"Currency\"\r\n                          name=\"CurrencyId\"\r\n                          formControlName=\"CurrencyId\"\r\n                        >\r\n                          <mat-option\r\n                            *ngFor=\"let item of data.currencyList\"\r\n                            [value]=\"item.CurrencyId\"\r\n                          >\r\n                            {{ item.CurrencyName }}\r\n                          </mat-option>\r\n                        </mat-select>\r\n                      </mat-form-field>\r\n\r\n                      <mat-form-field>\r\n                        <input\r\n                          matInput\r\n                          [matDatepicker]=\"dateFilterPicker\"\r\n                          name=\"Date\"\r\n                          formControlName=\"Date\"\r\n                          placeholder=\"Date\"\r\n                        />\r\n                        <mat-datepicker-toggle\r\n                          matSuffix\r\n                          [for]=\"dateFilterPicker\"\r\n                        ></mat-datepicker-toggle>\r\n                        <mat-datepicker #dateFilterPicker></mat-datepicker>\r\n                      </mat-form-field>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n              <div>\r\n                <button mat-raised-button matStepperNext>Next</button>\r\n              </div>\r\n            </form>\r\n          </mat-step>\r\n          <mat-step [stepControl]=\"exchangeRateGenerateModel\">\r\n            <br />\r\n            <p>\r\n              Please set the exchange rates from your selected currency to the\r\n              displayed currencies\r\n            </p>\r\n            <br />\r\n            <form\r\n              [formGroup]=\"exchangeRateGenerateModel\"\r\n              (ngSubmit)=\"SaveSelectedCurrencyExchangeRates()\"\r\n            >\r\n              <ng-template matStepLabel>Generate Exchange Rate</ng-template>\r\n              <!-- <mat-form-field>\r\n                    <input matInput placeholder=\"Address\" formControlName=\"secondCtrl\" required>\r\n                  </mat-form-field> -->\r\n              <div class=\"row\">\r\n                <div class=\"col-sm-6\">\r\n                  FROM <strong>{{ selectedCurrencyName }}</strong> TO\r\n                </div>\r\n                <div class=\"col-sm-6\" formArrayName=\"Currencies\">\r\n                  <div\r\n                    *ngFor=\"\r\n                      let currency of exchangeRateGenerateModel.get(\r\n                        'Currencies'\r\n                      )['controls'];\r\n                      let i = index\r\n                    \"\r\n                  >\r\n                    <div class=\"row\" [formGroupName]=\"i\">\r\n                      <div class=\"col-sm-6\">\r\n                        <label>{{ currency.value.CurrencyName }}</label>\r\n                      </div>\r\n                      <div class=\"col-sm-4\">\r\n                        <mat-form-field>\r\n                          <input\r\n                            type=\"number\"\r\n                            formControlName=\"Rate\"\r\n                            matInput\r\n                            placeholder=\"Rate\"\r\n                          />\r\n                        </mat-form-field>\r\n                      </div>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n              <div>\r\n                <button\r\n                  mat-raised-button\r\n                  matStepperPrevious\r\n                  [disabled]=\"addExchangeRateLoader\"\r\n                >\r\n                  Back\r\n                </button>\r\n                &nbsp;\r\n\r\n                <button\r\n                  mat-raised-button\r\n                  color=\"accent\"\r\n                  [disabled]=\"\r\n                    !exchangeRateGenerateModel.valid || addExchangeRateLoader\r\n                  \"\r\n                  (click)=\"SaveSelectedCurrencyExchangeRates()\"\r\n                  type=\"submit\"\r\n                >\r\n                  <span class=\"display_inline_block\">\r\n                    <mat-spinner\r\n                      *ngIf=\"addExchangeRateLoader\"\r\n                      [diameter]=\"15\"\r\n                    ></mat-spinner>\r\n                  </span>\r\n                  Save\r\n                </button>\r\n              </div>\r\n            </form>\r\n          </mat-step>\r\n        </mat-horizontal-stepper>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <!-- <div mat-dialog-actions class=\"pull-right\"></div> -->\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.scss":
/*!*******************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.scss ***!
  \*******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2V4Y2hhbmdlLXJhdGUvZXhjaGFuZ2UtcmF0ZS1hZGQvZXhjaGFuZ2UtcmF0ZS1hZGQuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.ts":
/*!*****************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.ts ***!
  \*****************************************************************************************************/
/*! exports provided: ExchangeRateAddComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExchangeRateAddComponent", function() { return ExchangeRateAddComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _exchange_rate_listing_exchange_rate_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../exchange-rate-listing/exchange-rate.service */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate.service.ts");
/* harmony import */ var _angular_cdk_stepper__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/cdk/stepper */ "./node_modules/@angular/cdk/esm5/stepper.es5.js");
/* harmony import */ var _shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../../../shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
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







var ExchangeRateAddComponent = /** @class */ (function () {
    function ExchangeRateAddComponent(fb, exchangeRateService, dialogRef, commonLoader, data, toastr) {
        this.fb = fb;
        this.exchangeRateService = exchangeRateService;
        this.dialogRef = dialogRef;
        this.commonLoader = commonLoader;
        this.data = data;
        this.toastr = toastr;
        this.addExchangeRateLoader = false;
        this.onListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.CurrencyList = [];
        this.CurrencyList = this.data.currencyList;
        this.FormInitialize();
    }
    ExchangeRateAddComponent.prototype.ngOnInit = function () {
    };
    ExchangeRateAddComponent.prototype.FormInitialize = function () {
        this.exchangeRateAddModel = this.fb.group({
            CurrencyId: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
            Date: [new Date(), [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]],
        });
        this.exchangeRateGenerateModel = this.fb.group({
            Currencies: this.fb.array([], _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required)
        });
        this.addCurrency();
    };
    Object.defineProperty(ExchangeRateAddComponent.prototype, "formData", {
        get: function () { return this.exchangeRateGenerateModel.get('Currencies'); },
        enumerable: true,
        configurable: true
    });
    ExchangeRateAddComponent.prototype.addCurrency = function () {
        var _this = this;
        // add Currency to the list
        var control = this.exchangeRateGenerateModel.controls.Currencies;
        this.CurrencyList.forEach(function (x) {
            control.push(_this.fb.group({
                CurrencyId: x.CurrencyId,
                CurrencyName: x.CurrencyName,
                Rate: new _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormControl"](null, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required),
                FromCurrency: null,
            }));
        });
    };
    //#region "addExchangeRate"
    ExchangeRateAddComponent.prototype.addExchangeRate = function (data) {
    };
    //#endregion
    ExchangeRateAddComponent.prototype.ExchangeRateGenerationStepChanged = function (event) {
        var _this = this;
        var currencyDetail = this.CurrencyList.find(function (x) { return x.CurrencyId === _this.exchangeRateAddModel.value.CurrencyId; });
        this.selectedCurrencyId = currencyDetail.CurrencyId;
        this.selectedCurrencyName = currencyDetail.CurrencyName;
    };
    ExchangeRateAddComponent.prototype.SaveSelectedCurrencyExchangeRates = function () {
        var _this = this;
        if (this.exchangeRateAddModel.valid && this.exchangeRateGenerateModel.valid) {
            this.addExchangeRateLoader = true;
            var exchangeRateData_1 = [];
            this.exchangeRateGenerateModel.value.Currencies.forEach(function (x) {
                return exchangeRateData_1.push({
                    FromCurrencyId: _this.exchangeRateAddModel.value.CurrencyId,
                    ToCurrencyId: x.CurrencyId,
                    Rate: x.Rate,
                    Date: _this.exchangeRateService.getLocalDate(_this.exchangeRateAddModel.value.Date)
                });
            });
            this.exchangeRateService.AddExchangeRate(exchangeRateData_1).subscribe(function (response) {
                if (response.statusCode === 200) {
                    _this.dialogRef.close(true);
                    _this.exchangeRateListRefresh();
                    _this.toastr.success('Added Exchange Rate successfully');
                }
                else {
                    _this.toastr.error(response.message);
                }
                _this.addExchangeRateLoader = false;
            }, function (error) {
                _this.toastr.error('Someting went wrong');
                _this.addExchangeRateLoader = false;
            });
        }
    };
    //#region "onCancelPopup"
    ExchangeRateAddComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close(false);
    };
    //#endregion
    //#region "onListRefresh"
    ExchangeRateAddComponent.prototype.exchangeRateListRefresh = function () {
        this.onListRefresh.emit();
    };
    ExchangeRateAddComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-exchange-rate-add',
            template: __webpack_require__(/*! ./exchange-rate-add.component.html */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.html"),
            providers: [{
                    provide: _angular_cdk_stepper__WEBPACK_IMPORTED_MODULE_5__["STEPPER_GLOBAL_OPTIONS"], useValue: { showError: true }
                }],
            styles: [__webpack_require__(/*! ./exchange-rate-add.component.scss */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormBuilder"],
            _exchange_rate_listing_exchange_rate_service__WEBPACK_IMPORTED_MODULE_4__["ExchangeRateService"],
            _angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"],
            _shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__["CommonLoaderService"], Object, ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"]])
    ], ExchangeRateAddComponent);
    return ExchangeRateAddComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-detail/exchange-rate-detail.component.html":
/*!*************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-detail/exchange-rate-detail.component.html ***!
  \*************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"row\">\r\n  <!-- <div class=\"col-sm-12\" *ngIf=\"exchangeRateLoader\">\r\n      <mat-spinner class=\"center_loader\" diameter=\"50\"></mat-spinner>\r\n  </div> -->\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <div class=\"col-sm-10 font_x_large\">\r\n        {{ exchangeRateDate | date: \"d, MMM, yyyy\" }}\r\n      </div>\r\n      <div class=\"col-sm-2\">\r\n        <!-- <button mat-raised-button (click)=\"verifyExchangeRate()\" [disabled]=\"isVerified || verifyExchangeRateLoader || verified\">\r\n              <span class=\"pull-left spinner_center\" *ngIf=\"verifyExchangeRateLoader\"><mat-spinner [diameter]=\"15\"></mat-spinner></span>Verify</button> -->\r\n\r\n        <button\r\n          mat-raised-button\r\n          color=\"accent\"\r\n          [disabled]=\"isVerified || verifyExchangeRateLoader\"\r\n          (click)=\"verifyExchangeRate()\"\r\n          *ngIf=\"isEditingAllowed\"\r\n        >\r\n          <span\r\n            *ngIf=\"\r\n              !verifyExchangeRateLoader;\r\n              else exchangeRateVerificationLoaderTemplate\r\n            \"\r\n            >Verify</span\r\n          >\r\n          <ng-template #exchangeRateVerificationLoaderTemplate>\r\n            <div class=\"spinner_center padding_top_5 padding_bottom_5\">\r\n              <mat-spinner [diameter]=\"25\"> </mat-spinner>\r\n            </div>\r\n          </ng-template>\r\n        </button>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <hr />\r\n\r\n  <div class=\"row\">\r\n    <div class=\"col-sm12\">\r\n      <p style=\"padding-left:30px; padding-right:30px; text-align:'left'\">\r\n        All of the below exchange rates were generated as a result of all the\r\n        currencies we maintain and the exchange rates provided at the creation\r\n        form. In certain cases, the machine calculations for the exchange rates\r\n        may not reflect real life exchange rates.<br />\r\n        Please <strong>check and verify</strong> that all the provided exchange\r\n        rates are correct for all currencies.<br /><br />\r\n        <b>Note:</b> Each office has unique set of exchange rates for each\r\n        currency.<br /><br />\r\n      </p>\r\n    </div>\r\n  </div>\r\n\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <div class=\"col-sm-5\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <mat-select\r\n            placeholder=\"Office\"\r\n            name=\"OfficeId\"\r\n            [(ngModel)]=\"selectedOffice\"\r\n            (selectionChange)=\"getExchangeRates()\"\r\n          >\r\n            <mat-option *ngFor=\"let item of officeList\" [value]=\"item.OfficeId\">\r\n              {{ item.OfficeName }}\r\n            </mat-option>\r\n          </mat-select>\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-5\" *ngIf=\"isEditingAllowed\">\r\n        <!-- <button mat-raised-button (click)=\"saveExchangeRatesForOffice(true)\"  [disabled]=\"isVerified\"></button> -->\r\n        <button\r\n          mat-raised-button\r\n          color=\"accent\"\r\n          [disabled]=\"isVerified || exchangeRateLoader\"\r\n          (click)=\"saveExchangeRatesForOffice(true)\"\r\n        >\r\n          <span\r\n            *ngIf=\"\r\n              !exchangeRateLoader;\r\n              else exchangeRateOfficeAddLoaderTemplate\r\n            \"\r\n            >Copy & Save for All Office</span\r\n          >\r\n          <ng-template #exchangeRateOfficeAddLoaderTemplate>\r\n            <div class=\"spinner_center padding_top_5 padding_bottom_5\">\r\n              <mat-spinner [diameter]=\"25\"> </mat-spinner>\r\n            </div>\r\n          </ng-template>\r\n        </button>\r\n      </div>\r\n      <div class=\"col-sm-2\" *ngIf=\"isEditingAllowed\">\r\n        <!-- <button mat-raised-button   [disabled]=\"isVerified\">Save</button> -->\r\n        <button\r\n          mat-raised-button\r\n          color=\"accent\"\r\n          [disabled]=\"isVerified || exchangeRateLoader\"\r\n          (click)=\"saveExchangeRatesForOffice(false)\"\r\n        >\r\n          <span *ngIf=\"!exchangeRateLoader; else exchangeRateAddLoaderTemplate\"\r\n            >Save</span\r\n          >\r\n          <ng-template #exchangeRateAddLoaderTemplate>\r\n            <div class=\"spinner_center padding_top_5 padding_bottom_5\">\r\n              <mat-spinner [diameter]=\"25\"> </mat-spinner>\r\n            </div>\r\n          </ng-template>\r\n        </button>\r\n      </div>\r\n    </div>\r\n  </div>\r\n\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <table>\r\n        <tr>\r\n          <th>From Currency</th>\r\n          <th>To Currency</th>\r\n          <th>Rate</th>\r\n        </tr>\r\n\r\n        <tr *ngFor=\"let item of exchangeRateList\" class=\"text-center\">\r\n          <span\r\n            *ngIf=\"\r\n              item.FromCurrency == item.ToCurrency;\r\n              else exchangeRateTemplate\r\n            \"\r\n          ></span>\r\n          <ng-template #exchangeRateTemplate>\r\n            <td class=\"text-center\" text-align=\"left\">\r\n              {{ item.FromCurrency }}\r\n            </td>\r\n            <td class=\"text-center\" text-align=\"left\">\r\n              {{ item.ToCurrency }}\r\n            </td>\r\n            <td\r\n              class=\"text-center\"\r\n              text-align=\"left\"\r\n              *ngIf=\"!isVerified && isEditingAllowed\"\r\n            >\r\n              <mat-form-field>\r\n                <input\r\n                  type=\"number\"\r\n                  [(ngModel)]=\"item.Rate\"\r\n                  matInput\r\n                  placeholder=\"Rate\"\r\n                />\r\n              </mat-form-field>\r\n            </td>\r\n            <td\r\n              class=\"text-center\"\r\n              text-align=\"left\"\r\n              *ngIf=\"isVerified || !isEditingAllowed\"\r\n            >\r\n              {{ item.Rate }}\r\n            </td>\r\n          </ng-template>\r\n        </tr>\r\n      </table>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-detail/exchange-rate-detail.component.scss":
/*!*************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-detail/exchange-rate-detail.component.scss ***!
  \*************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "table {\n  width: 100%; }\n\nth, td {\n  padding: 8px;\n  text-align: left;\n  border-bottom: 1px solid #ddd; }\n\ntr:hover {\n  background-color: #f5f5f5; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvZXhjaGFuZ2UtcmF0ZS9leGNoYW5nZS1yYXRlLWRldGFpbC9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxkYXNoYm9hcmRcXGFjY291bnRpbmdcXGV4Y2hhbmdlLXJhdGVcXGV4Y2hhbmdlLXJhdGUtZGV0YWlsXFxleGNoYW5nZS1yYXRlLWRldGFpbC5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNFLFdBQVcsRUFBQTs7QUFHYjtFQUNFLFlBQVk7RUFDWixnQkFBZ0I7RUFDaEIsNkJBQTZCLEVBQUE7O0FBRy9CO0VBQVUseUJBQXdCLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9kYXNoYm9hcmQvYWNjb3VudGluZy9leGNoYW5nZS1yYXRlL2V4Y2hhbmdlLXJhdGUtZGV0YWlsL2V4Y2hhbmdlLXJhdGUtZGV0YWlsLmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsidGFibGUge1xyXG4gIHdpZHRoOiAxMDAlO1xyXG59XHJcblxyXG50aCwgdGQge1xyXG4gIHBhZGRpbmc6IDhweDtcclxuICB0ZXh0LWFsaWduOiBsZWZ0O1xyXG4gIGJvcmRlci1ib3R0b206IDFweCBzb2xpZCAjZGRkO1xyXG59XHJcblxyXG50cjpob3ZlciB7YmFja2dyb3VuZC1jb2xvcjojZjVmNWY1O31cclxuIl19 */"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-detail/exchange-rate-detail.component.ts":
/*!***********************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-detail/exchange-rate-detail.component.ts ***!
  \***********************************************************************************************************/
/*! exports provided: ExchangeRateDetailComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExchangeRateDetailComponent", function() { return ExchangeRateDetailComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _exchange_rate_listing_exchange_rate_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../exchange-rate-listing/exchange-rate.service */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate.service.ts");
/* harmony import */ var _shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _verify_exchange_rate_verify_exchange_rate_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../verify-exchange-rate/verify-exchange-rate.component */ "./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var ExchangeRateDetailComponent = /** @class */ (function () {
    function ExchangeRateDetailComponent(exchangeRateService, toastr, commonLoader, dialog) {
        this.exchangeRateService = exchangeRateService;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.dialog = dialog;
        this.currencyList = [];
        this.officeList = [];
        this.listRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.exchangeRateVerify = false;
        this.exchangeRateLoader = false;
        this.isVerified = false;
        this.verifyExchangeRateLoader = false;
    }
    ExchangeRateDetailComponent.prototype.ngOnInit = function () {
    };
    //#region "ngOnChanges"
    ExchangeRateDetailComponent.prototype.ngOnChanges = function (changes) {
        if (this.exchangeRateItem !== undefined) {
            this.isVerified = this.exchangeRateItem.VerificationStatus;
            // this.exchangeRateDate = this.selectedExchangeRateDate;
            this.exchangeRateDate = this.exchangeRateItem.ExchangeRateDate;
        }
        this.getExchangeRates();
    };
    //#endregion
    ExchangeRateDetailComponent.prototype.getExchangeRates = function () {
        this.exchangeRateList = [];
        if (this.exchangeRateItem !== null &&
            this.exchangeRateItem !== undefined) {
            this.getExchangeRateDetailByDate(this.exchangeRateDate);
        }
    };
    ExchangeRateDetailComponent.prototype.getExchangeRateDetailByDate = function (ExchangeRateDate) {
        var _this = this;
        this.exchangeRateItem.ExchangeRateDate = this.exchangeRateService.getLocalDate(this.exchangeRateItem.ExchangeRateDate);
        this.selectedOffice = this.selectedOffice === undefined ? this.officeList[0].OfficeId : this.selectedOffice;
        this.exchangeRateService.GetExchangeRatesByDate(ExchangeRateDate, this.selectedOffice).subscribe(function (response) {
            if (response.statusCode === 200 && response.data !== null) {
                _this.exchangeRateList = [];
                response.data.forEach(function (element) {
                    _this.exchangeRateList.push({
                        FromCurrencyId: element.FromCurrency,
                        ToCurrencyId: element.ToCurrency,
                        FromCurrency: _this.currencyList.filter(function (x) { return x.CurrencyId === element.FromCurrency; })[0].CurrencyName,
                        ToCurrency: _this.currencyList.filter(function (x) { return x.CurrencyId === element.ToCurrency; })[0].CurrencyName,
                        Rate: element.Rate
                    });
                });
                _this.isVerified = response.IsExchangeRateVerified;
            }
        }, function (error) {
        });
    };
    ExchangeRateDetailComponent.prototype.saveExchangeRatesForOffice = function (saveForAllOffice) {
        var _this = this;
        this.exchangeRateLoader = true;
        var officeExchangeRateModels = {
            OfficeId: 0,
            GenerateExchangeRateModel: [],
            SaveForAllOffices: false
        };
        if (this.exchangeRateList != null && this.exchangeRateList !== undefined) {
            this.exchangeRateList.forEach(function (x) {
                return officeExchangeRateModels.GenerateExchangeRateModel.push({
                    FromCurrencyId: x.FromCurrencyId,
                    ToCurrencyId: x.ToCurrencyId,
                    Rate: x.Rate,
                    Date: _this.exchangeRateService.getLocalDate(_this.exchangeRateItem.ExchangeRateDate)
                });
            });
            officeExchangeRateModels.SaveForAllOffices = saveForAllOffice;
            officeExchangeRateModels.OfficeId = this.selectedOffice;
            this.exchangeRateService.SaveExchangeRatesForAllOffices(officeExchangeRateModels).subscribe(function (response) {
                if (response.statusCode === 200) {
                    _this.exchangeRateLoader = false;
                    _this.toastr.success('Exchange Rates Added successfully');
                }
                else {
                    _this.toastr.error(response.message);
                }
                _this.exchangeRateLoader = false;
            }, function (error) {
                _this.toastr.error('Someting went wrong');
                _this.exchangeRateLoader = false;
            });
        }
    };
    ExchangeRateDetailComponent.prototype.verifyExchangeRate = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_verify_exchange_rate_verify_exchange_rate_component__WEBPACK_IMPORTED_MODULE_5__["VerifyExchangeRateComponent"], {
            width: '400px',
            data: { data: '' },
            autoFocus: false
        });
        dialogRef.afterClosed().subscribe(function (result) {
            if (result) {
                _this.verifyExchangeRates();
            }
        });
    };
    ExchangeRateDetailComponent.prototype.verifyExchangeRates = function () {
        var _this = this;
        this.verifyExchangeRateLoader = true;
        this.exchangeRateItem.ExchangeRateDate = this.exchangeRateService.getLocalDate(this.exchangeRateItem.ExchangeRateDate);
        this.exchangeRateService.VerifyExchangeRates(this.exchangeRateItem.ExchangeRateDate).subscribe(function (response) {
            if (response.statusCode === 200 && response.data !== null) {
                _this.exchangeRateListRefresh();
                _this.isVerified = true;
                _this.verifyExchangeRateLoader = false;
                _this.toastr.success(response.message);
            }
            else {
                _this.verifyExchangeRateLoader = false;
                _this.toastr.error('Something went wrong!!');
            }
        }, function (error) {
            _this.verifyExchangeRateLoader = false;
            _this.toastr.error('Something went wrong!!');
        });
    };
    //#region "onListRefresh"
    ExchangeRateDetailComponent.prototype.exchangeRateListRefresh = function () {
        this.listRefresh.emit(this.exchangeRateItem.ExchangeRateVerificationId);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Array)
    ], ExchangeRateDetailComponent.prototype, "currencyList", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Array)
    ], ExchangeRateDetailComponent.prototype, "officeList", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Boolean)
    ], ExchangeRateDetailComponent.prototype, "isEditingAllowed", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], ExchangeRateDetailComponent.prototype, "exchangeRateItem", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], ExchangeRateDetailComponent.prototype, "listRefresh", void 0);
    ExchangeRateDetailComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-exchange-rate-detail',
            template: __webpack_require__(/*! ./exchange-rate-detail.component.html */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-detail/exchange-rate-detail.component.html"),
            styles: [__webpack_require__(/*! ./exchange-rate-detail.component.scss */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-detail/exchange-rate-detail.component.scss")]
        }),
        __metadata("design:paramtypes", [_exchange_rate_listing_exchange_rate_service__WEBPACK_IMPORTED_MODULE_1__["ExchangeRateService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"], _shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__["CommonLoaderService"],
            _angular_material_dialog__WEBPACK_IMPORTED_MODULE_4__["MatDialog"]])
    ], ExchangeRateDetailComponent);
    return ExchangeRateDetailComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.html":
/*!***************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.html ***!
  \***************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"exchange-rate-listing-main\">\r\n  <div class=\"body-content\">\r\n    <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n        <div [class]=\"colsm6\">\r\n          <div #exchangeRateListing>\r\n            <mat-card [ngStyle]=\"scrollStyles\">\r\n              <div>\r\n                <button mat-raised-button color=\"accent\" (click)=\"openAddExchangeRateDialog()\" *ngIf=\"isEditingAllowed\">\r\n                  Add\r\n                </button>\r\n                <hr />\r\n                <div class=\"row text-center\">\r\n                  <div class=\"col-sm-6\">\r\n                    <mat-form-field>\r\n                      <input matInput [matDatepicker]=\"fromDateFilterPicker\"\r\n                        [(ngModel)]=\"exchangeRateFilter.FromDateFilter\" placeholder=\"From Date\" />\r\n                      <mat-datepicker-toggle matSuffix [for]=\"fromDateFilterPicker\"></mat-datepicker-toggle>\r\n                      <mat-datepicker #fromDateFilterPicker></mat-datepicker>\r\n                    </mat-form-field>\r\n                  </div>\r\n                  <div class=\"col-sm-6\">\r\n                    <mat-form-field>\r\n                      <input matInput [min]=\"exchangeRateFilter.FromDateFilter\" [matDatepicker]=\"tillDateFilterPicker\"\r\n                        [(ngModel)]=\"exchangeRateFilter.TillDateFilter\" placeholder=\"Till Date\" />\r\n                      <mat-datepicker-toggle matSuffix [for]=\"tillDateFilterPicker\"></mat-datepicker-toggle>\r\n                      <mat-datepicker #tillDateFilterPicker></mat-datepicker>\r\n                    </mat-form-field>\r\n                  </div>\r\n                </div>\r\n\r\n                <div class=\"row text-center\">\r\n                  <div class=\"col-sm-12\">\r\n                    <div class=\"row\">\r\n                      <div class=\"col-sm-4\">\r\n                        <mat-checkbox [(ngModel)]=\"exchangeRateFilter.VerifiedFilter\">Verified</mat-checkbox>\r\n                      </div>\r\n                      <div class=\"col-sm-4\">\r\n                        <button mat-raised-button color=\"accent\" class=\"margin_left_10\"\r\n                          (click)=\"getSavedExchangeRatesDate()\">\r\n                          Apply\r\n                        </button>\r\n                      </div>\r\n                      <div class=\"col-sm-4\">\r\n                        <!-- Reset -->\r\n                        <button mat-raised-button (click)=\"onFilterReset()\">\r\n                          Clear\r\n                        </button>\r\n                      </div>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n\r\n                <div class=\"row\">\r\n                  <div class=\"col-sm-12\">\r\n                    {{ exchangeRateFilter.totalCount }} Records\r\n                  </div>\r\n                </div>\r\n\r\n                <div class=\"row\">\r\n                  <div class=\"col-sm-12\"></div>\r\n                </div>\r\n                <br />\r\n                <div class=\"row\">\r\n                  <div *ngIf=\"\r\n                      exchangeRateListLoaderFlag;\r\n                      else exchangeRateListTemplate\r\n                    \" class=\"col-sm-12\">\r\n                    <mat-spinner class=\"center_loader\" diameter=\"50\"></mat-spinner>\r\n                  </div>\r\n                  <ng-template #exchangeRateListTemplate>\r\n                    <div class=\"col-sm-12\">\r\n                      <div class=\"responsive_table-responsive\">\r\n                        <table class=\"table table-bordered\">\r\n                          <tbody>\r\n                            <tr>\r\n                              <td width=\"2%\">\r\n                                <p class=\"width_8\"></p>\r\n                              </td>\r\n                              <td width=\"46%\" class=\"text-left\">\r\n                                <strong>Date</strong>\r\n                              </td>\r\n                              <td width=\"46%\" text-align=\"left\">\r\n                                <strong>Verification</strong>\r\n                              </td>\r\n                              <td width=\"4%\"></td>\r\n                            </tr>\r\n                            <tr *ngFor=\"let item of exchangeRateList\" (click)=\"\r\n                                onItemClick(\r\n                                  item\r\n                                )\r\n                              \" [ngClass]=\"{\r\n                                selected: exchangeRateVerificationId == item.ExchangeRateVerificationId\r\n                              }\">\r\n                              <td width=\"2%\">\r\n                                <p class=\"width_8\"></p>\r\n                              </td>\r\n                              <td width=\"46%\" text-align=\"left\">\r\n                                <p class=\"diamond\">\r\n                                  {{\r\n                                    item.ExchangeRateDate | date: \"dd/MM/yyyy\"\r\n                                  }}\r\n                                </p>\r\n                              </td>\r\n                              <td width=\"46%\" text-align=\"left\">\r\n                                <p class=\"text_overflow\">\r\n                                  {{\r\n                                    item.VerificationStatus\r\n                                      ? \"Verified\"\r\n                                      : \"Pending\"\r\n                                  }}\r\n                                </p>\r\n                              </td>\r\n                              <td class=\"text-center\" width=\"4%\" *ngIf=\"isEditingAllowed\">\r\n                                <button mat-icon-button [disabled]=\"item.VerificationStatus\"\r\n                                  (click)=\"onDeleteExchangeRate(item.ExchangeRateDate)\" color=\"warn\"\r\n                                  aria-label=\"Example icon-button with a heart icon\">\r\n                                  <mat-icon>\r\n                                    delete\r\n                                  </mat-icon>\r\n\r\n                                </button>\r\n                              </td>\r\n                              <td class=\"text-center\" width=\"4%\">\r\n                                <i *ngIf=\"exchangeRateVerificationId == item.ExchangeRateVerificationId\"\r\n                                  class=\"material-icons font-12\">\r\n                                  arrow_forward_ios\r\n                                </i>\r\n                              </td>\r\n                            </tr>\r\n                          </tbody>\r\n                        </table>\r\n                      </div>\r\n                      <mat-paginator [length]=\"exchangeRateFilter.totalCount\" [pageSize]=\"exchangeRateFilter.pageSize\"\r\n                        [pageIndex]=\"exchangeRateFilter.pageIndex\" [pageSizeOptions]=\"[5, 10, 25, 100]\"\r\n                        (page)=\"pageEvent($event)\">\r\n                      </mat-paginator>\r\n                    </div>\r\n                  </ng-template>\r\n                </div>\r\n              </div>\r\n            </mat-card>\r\n          </div>\r\n        </div>\r\n\r\n        <!-- Exchange Rate Detail -->\r\n        <div class=\"col-sm-6\" [hidden]=\"!showExchangeRateDetail\">\r\n          <mat-card [ngStyle]=\"scrollStyles\">\r\n            <app-exchange-rate-detail [currencyList]=\"CurrencyList\" [officeList]=\"officeList\"\r\n              [exchangeRateItem]=\"exchangeRateItem\" (listRefresh)=\"listRefresh($event)\"\r\n              [isEditingAllowed]=\"isEditingAllowed\"></app-exchange-rate-detail>\r\n          </mat-card>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.scss":
/*!***************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.scss ***!
  \***************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2V4Y2hhbmdlLXJhdGUvZXhjaGFuZ2UtcmF0ZS1saXN0aW5nL2V4Y2hhbmdlLXJhdGUtbGlzdGluZy5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.ts":
/*!*************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.ts ***!
  \*************************************************************************************************************/
/*! exports provided: ExchangeRateListingComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExchangeRateListingComponent", function() { return ExchangeRateListingComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _exchange_rate_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./exchange-rate.service */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate.service.ts");
/* harmony import */ var _exchange_rate_add_exchange_rate_add_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../exchange-rate-add/exchange-rate-add.component */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../../shared/services/localstorage.service */ "./src/app/shared/services/localstorage.service.ts");
/* harmony import */ var _shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../../../shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var ExchangeRateListingComponent = /** @class */ (function () {
    function ExchangeRateListingComponent(exchangeRateService, dialog, toastr, localStorageService) {
        this.exchangeRateService = exchangeRateService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.localStorageService = localStorageService;
        this.colsm6 = 'col-sm-10 col-sm-offset-1';
        this.exchangeRateListLoaderFlag = false;
        this.exchangeRateList = [];
        this.CurrencyList = [];
        this.officeList = [];
        this.isDeleted = false;
        this.isEditingAllowed = false;
        this.pageId = _shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_6__["ApplicationPages"].ExchangeRates;
        this.getScreenSize();
    }
    ExchangeRateListingComponent.prototype.ngOnInit = function () {
        this.initExchangeRateFilter();
        this.getCurrencyList();
        this.getOfficeList();
        this.getSavedExchangeRatesDate();
        this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
    };
    //#region "initVoucherFilter"
    ExchangeRateListingComponent.prototype.initExchangeRateFilter = function () {
        this.exchangeRateFilter = {
            TillDateFilter: null,
            FromDateFilter: null,
            VerifiedFilter: null,
            totalCount: null,
            pageIndex: null,
            pageSize: 10
        };
        this.exchangeRateList = [];
    };
    //#endregion
    //#region "Dynamic Scroll"
    ExchangeRateListingComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    //#region "Add Voucher Popup"
    ExchangeRateListingComponent.prototype.openAddExchangeRateDialog = function () {
        // NOTE: It passed the data into the Add Voucher Model
        var _this = this;
        var dialogRef = this.dialog.open(_exchange_rate_add_exchange_rate_add_component__WEBPACK_IMPORTED_MODULE_2__["ExchangeRateAddComponent"], {
            width: '550px',
            data: {
                data: 'AddExchangeRate',
                currencyList: this.CurrencyList,
            },
            autoFocus: false
        });
        dialogRef.componentInstance.onListRefresh.subscribe(function () {
            _this.getSavedExchangeRatesDate();
        });
        dialogRef.afterClosed().subscribe(function (result) {
        });
    };
    //#endregion
    //#region "getCurrencyList"
    ExchangeRateListingComponent.prototype.getCurrencyList = function () {
        var _this = this;
        this.exchangeRateService.GetCurrencyList().subscribe(function (response) {
            _this.CurrencyList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.CurrencyList.push({
                        CurrencyId: element.CurrencyId,
                        CurrencyName: element.CurrencyName
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "onItemClick"
    ExchangeRateListingComponent.prototype.onItemClick = function (item) {
        this.exchangeRateItem = item;
        this.exchangeRateVerificationId = item.ExchangeRateVerificationId;
        this.showExchangeRateDetailPanel();
    };
    //#region "show/ hide"
    ExchangeRateListingComponent.prototype.showExchangeRateDetailPanel = function () {
        this.showExchangeRateDetail = true;
        this.colsm6 = this.showExchangeRateDetail
            ? 'col-sm-6'
            : 'col-sm-10 col-sm-offset-1';
    };
    //#region "getOfficeList"
    ExchangeRateListingComponent.prototype.getOfficeList = function () {
        var _this = this;
        this.exchangeRateService.GetOfficeList().subscribe(function (response) {
            _this.officeList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.officeList.push({
                        OfficeId: element.OfficeId,
                        OfficeName: element.OfficeName
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getExchangeRatesVerificationList"
    ExchangeRateListingComponent.prototype.getSavedExchangeRatesDate = function () {
        var _this = this;
        this.exchangeRateListLoaderFlag = true;
        this.exchangeRateList = [];
        var filter = {
            FromDate: this.exchangeRateFilter.FromDateFilter !== null ?
                this.exchangeRateService.getLocalDate(this.exchangeRateFilter.FromDateFilter) :
                this.exchangeRateFilter.FromDateFilter,
            ToDate: this.exchangeRateFilter.TillDateFilter !== null ?
                this.exchangeRateService.getLocalDate(this.exchangeRateFilter.TillDateFilter) : this.exchangeRateFilter.TillDateFilter,
            IsVerified: this.exchangeRateFilter.VerifiedFilter,
            TotalCount: this.exchangeRateFilter.totalCount,
            PageSize: this.exchangeRateFilter.pageSize,
            PageIndex: this.exchangeRateFilter.pageIndex
        };
        this.exchangeRateService.GetSavedExchangeRates(filter).subscribe(function (response) {
            if (response.statusCode === 200 && response.data !== undefined && response.data !== null) {
                _this.exchangeRateListLoaderFlag = false;
                response.data.forEach(function (element) {
                    _this.exchangeRateList.push({
                        ExchangeRateVerificationId: element.ExRateVerificationId,
                        ExchangeRateDate: element.Date,
                        VerificationStatus: element.IsVerified
                    });
                });
                if (_this.exchangeRateList.length > 0) {
                    _this.selectedExchangeRateDate = _this.exchangeRateList[0].ExchangeRateDate;
                }
                _this.isDeleted = false;
                _this.exchangeRateFilter.totalCount = response.total;
            }
            else {
                _this.exchangeRateListLoaderFlag = false;
            }
        }, function (error) {
            _this.exchangeRateListLoaderFlag = false;
        });
    };
    //#endregion
    //#region "onFilterReset"
    ExchangeRateListingComponent.prototype.onFilterReset = function () {
        this.exchangeRateFilter = {
            TillDateFilter: null,
            FromDateFilter: null,
            VerifiedFilter: null,
            totalCount: null,
            pageIndex: null,
            pageSize: 10
        };
        this.getSavedExchangeRatesDate();
    };
    ExchangeRateListingComponent.prototype.onDeleteExchangeRate = function (exchangeRateDate) {
        var _this = this;
        this.exchangeRateListLoaderFlag = true;
        this.isDeleted = true;
        exchangeRateDate = this.exchangeRateService.getLocalDate(exchangeRateDate);
        this.exchangeRateService.DeleteExchangeRates(exchangeRateDate).subscribe(function (response) {
            if (response.statusCode === 200) {
                _this.exchangeRateListLoaderFlag = false;
                _this.toastr.success(response.message);
                _this.getSavedExchangeRatesDate();
            }
            else {
                _this.toastr.error(response.message);
                _this.exchangeRateListLoaderFlag = false;
            }
        }, function (error) {
            _this.toastr.error('Something went wrong!!!');
            _this.exchangeRateListLoaderFlag = false;
        });
    };
    ExchangeRateListingComponent.prototype.listRefresh = function (event) {
        this.getSavedExchangeRatesDate();
    };
    //#region "pageEvent"
    ExchangeRateListingComponent.prototype.pageEvent = function (e) {
        this.exchangeRateFilter.pageIndex = e.pageIndex;
        this.exchangeRateFilter.pageSize = e.pageSize;
        this.getSavedExchangeRatesDate();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], ExchangeRateListingComponent.prototype, "getScreenSize", null);
    ExchangeRateListingComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-exchange-rate-listing',
            template: __webpack_require__(/*! ./exchange-rate-listing.component.html */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.html"),
            styles: [__webpack_require__(/*! ./exchange-rate-listing.component.scss */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.scss")]
        }),
        __metadata("design:paramtypes", [_exchange_rate_service__WEBPACK_IMPORTED_MODULE_1__["ExchangeRateService"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MatDialog"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            _shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_5__["LocalStorageService"]])
    ], ExchangeRateListingComponent);
    return ExchangeRateListingComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate.service.ts":
/*!***************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate.service.ts ***!
  \***************************************************************************************************/
/*! exports provided: ExchangeRateService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExchangeRateService", function() { return ExchangeRateService; });
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





var ExchangeRateService = /** @class */ (function () {
    //#endregion
    function ExchangeRateService(globalService, appurl) {
        this.globalService = globalService;
        this.appurl = appurl;
    }
    //#region "GetExchangeRateList"
    ExchangeRateService.prototype.GetExchangeRateList = function (data) {
        return this.globalService.post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_VoucherTransaction_GetAllVoucherList, data);
    };
    //#endregion
    //#region "Add Exchange Rate"
    ExchangeRateService.prototype.AddExchangeRate = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_ExchangeRates_SaveSystemGeneratedExchangeRates, data)
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
    //#region "Add Exchange Rate"
    ExchangeRateService.prototype.SaveExchangeRatesForAllOffices = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_ExchangeRates_SaveExchangeRatesForAllOffices, data)
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
    ExchangeRateService.prototype.GetCurrencyList = function () {
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
    //#region "GetOfficeList"
    ExchangeRateService.prototype.GetOfficeList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllOffice)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.OfficeDetailsList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetSavedExchangeRates"
    ExchangeRateService.prototype.GetSavedExchangeRates = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_ExchangeRates_GetSavedExchangeRates, data).pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ExchangeRateVerificationList,
                statusCode: x.StatusCode,
                message: x.Message,
                total: x.data.TotalExchangeRateCount
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "Get Saved ExchangeRates"
    ExchangeRateService.prototype.GetExchangeRatesByDate = function (date, officeId) {
        var data = {
            ExchangeRateDate: this.getLocalDate(date),
            OfficeId: officeId
        };
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_ExchangeRates_GetExchangeRatesDetail, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ExchangeRateDetailViewModelList,
                statusCode: x.StatusCode,
                message: x.Message,
                IsExchangeRateVerified: x.data.IsExchangeRateVerified
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "Verify ExchangeRates"
    ExchangeRateService.prototype.VerifyExchangeRates = function (ExchangeRateDate) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_ExchangeRates_VerifyExchangeRates, ExchangeRateDate)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: '',
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "Delete ExchangeRates"
    ExchangeRateService.prototype.DeleteExchangeRates = function (ExchangeRateDate) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_ExchangeRates_DeleteExchangeRates, ExchangeRateDate)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: '',
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "getLocalDate"
    ExchangeRateService.prototype.getLocalDate = function (date) {
        return new Date(new Date(date).getFullYear(), new Date(date).getMonth(), new Date(date).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds(), new Date().getMilliseconds());
    };
    ExchangeRateService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            _shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"]])
    ], ExchangeRateService);
    return ExchangeRateService;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-routing.module.ts":
/*!************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate-routing.module.ts ***!
  \************************************************************************************/
/*! exports provided: ExchangeRateRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExchangeRateRoutingModule", function() { return ExchangeRateRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _exchange_rate_listing_exchange_rate_listing_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./exchange-rate-listing/exchange-rate-listing.component */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.ts");
/* harmony import */ var _exchange_rate_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./exchange-rate.component */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var routes = [
    {
        path: '',
        component: _exchange_rate_component__WEBPACK_IMPORTED_MODULE_3__["ExchangeRateComponent"],
        children: [
            {
                path: '',
                component: _exchange_rate_listing_exchange_rate_listing_component__WEBPACK_IMPORTED_MODULE_2__["ExchangeRateListingComponent"]
            }
        ]
    }
];
var ExchangeRateRoutingModule = /** @class */ (function () {
    function ExchangeRateRoutingModule() {
    }
    ExchangeRateRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], ExchangeRateRoutingModule);
    return ExchangeRateRoutingModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"main_body\">\r\n  <router-outlet></router-outlet>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.scss":
/*!*********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.scss ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2V4Y2hhbmdlLXJhdGUvZXhjaGFuZ2UtcmF0ZS5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.ts ***!
  \*******************************************************************************/
/*! exports provided: ExchangeRateComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExchangeRateComponent", function() { return ExchangeRateComponent; });
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


var ExchangeRateComponent = /** @class */ (function () {
    function ExchangeRateComponent(globalService) {
        this.globalService = globalService;
        this.setProjectHeader = 'Exchange Rates';
        // Set Menu Header Name
        this.globalService.setMenuHeaderName(this.setProjectHeader);
        // Set Menu Header List
        this.globalService.setMenuList([]);
    }
    ExchangeRateComponent.prototype.ngOnInit = function () {
    };
    ExchangeRateComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-exchange-rate',
            template: __webpack_require__(/*! ./exchange-rate.component.html */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.html"),
            styles: [__webpack_require__(/*! ./exchange-rate.component.scss */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_1__["GlobalSharedService"]])
    ], ExchangeRateComponent);
    return ExchangeRateComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/exchange-rate.module.ts":
/*!****************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/exchange-rate.module.ts ***!
  \****************************************************************************/
/*! exports provided: ExchangeRateModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExchangeRateModule", function() { return ExchangeRateModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _exchange_rate_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./exchange-rate-routing.module */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-routing.module.ts");
/* harmony import */ var _exchange_rate_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./exchange-rate.component */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate.component.ts");
/* harmony import */ var _angular_material_input__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material/input */ "./node_modules/@angular/material/esm5/input.es5.js");
/* harmony import */ var _angular_material_button__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/material/button */ "./node_modules/@angular/material/esm5/button.es5.js");
/* harmony import */ var _angular_material_card__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/material/card */ "./node_modules/@angular/material/esm5/card.es5.js");
/* harmony import */ var _angular_material_paginator__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/material/paginator */ "./node_modules/@angular/material/esm5/paginator.es5.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/material/datepicker */ "./node_modules/@angular/material/esm5/datepicker.es5.js");
/* harmony import */ var _angular_material_icon__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/material/icon */ "./node_modules/@angular/material/esm5/icon.es5.js");
/* harmony import */ var _angular_material_select__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/material/select */ "./node_modules/@angular/material/esm5/select.es5.js");
/* harmony import */ var _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/material/progress-spinner */ "./node_modules/@angular/material/esm5/progress-spinner.es5.js");
/* harmony import */ var _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material/checkbox */ "./node_modules/@angular/material/esm5/checkbox.es5.js");
/* harmony import */ var _exchange_rate_listing_exchange_rate_listing_component__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./exchange-rate-listing/exchange-rate-listing.component */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate-listing.component.ts");
/* harmony import */ var _exchange_rate_add_exchange_rate_add_component__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./exchange-rate-add/exchange-rate-add.component */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-add/exchange-rate-add.component.ts");
/* harmony import */ var _exchange_rate_detail_exchange_rate_detail_component__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./exchange-rate-detail/exchange-rate-detail.component */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-detail/exchange-rate-detail.component.ts");
/* harmony import */ var _verify_exchange_rate_verify_exchange_rate_component__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ./verify-exchange-rate/verify-exchange-rate.component */ "./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.ts");
/* harmony import */ var _exchange_rate_listing_exchange_rate_service__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ./exchange-rate-listing/exchange-rate.service */ "./src/app/dashboard/accounting/exchange-rate/exchange-rate-listing/exchange-rate.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_stepper__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! @angular/material/stepper */ "./node_modules/@angular/material/esm5/stepper.es5.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};





















var ExchangeRateModule = /** @class */ (function () {
    function ExchangeRateModule() {
    }
    ExchangeRateModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _exchange_rate_component__WEBPACK_IMPORTED_MODULE_3__["ExchangeRateComponent"],
                _exchange_rate_listing_exchange_rate_listing_component__WEBPACK_IMPORTED_MODULE_14__["ExchangeRateListingComponent"],
                _exchange_rate_add_exchange_rate_add_component__WEBPACK_IMPORTED_MODULE_15__["ExchangeRateAddComponent"],
                _exchange_rate_detail_exchange_rate_detail_component__WEBPACK_IMPORTED_MODULE_16__["ExchangeRateDetailComponent"],
                _verify_exchange_rate_verify_exchange_rate_component__WEBPACK_IMPORTED_MODULE_17__["VerifyExchangeRateComponent"],
            ],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_19__["FormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_19__["ReactiveFormsModule"],
                _exchange_rate_routing_module__WEBPACK_IMPORTED_MODULE_2__["ExchangeRateRoutingModule"],
                // material
                _angular_material_input__WEBPACK_IMPORTED_MODULE_4__["MatInputModule"],
                _angular_material_button__WEBPACK_IMPORTED_MODULE_5__["MatButtonModule"],
                _angular_material_card__WEBPACK_IMPORTED_MODULE_6__["MatCardModule"],
                _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_13__["MatCheckboxModule"],
                _angular_material_paginator__WEBPACK_IMPORTED_MODULE_7__["MatPaginatorModule"],
                _angular_material_dialog__WEBPACK_IMPORTED_MODULE_8__["MatDialogModule"],
                _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_9__["MatDatepickerModule"],
                _angular_material_icon__WEBPACK_IMPORTED_MODULE_10__["MatIconModule"],
                _angular_material_select__WEBPACK_IMPORTED_MODULE_11__["MatSelectModule"],
                _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_12__["MatProgressSpinnerModule"],
                _angular_material_stepper__WEBPACK_IMPORTED_MODULE_20__["MatStepperModule"],
            ],
            providers: [
                _exchange_rate_listing_exchange_rate_service__WEBPACK_IMPORTED_MODULE_18__["ExchangeRateService"],
            ],
            exports: [],
            entryComponents: [
                _exchange_rate_add_exchange_rate_add_component__WEBPACK_IMPORTED_MODULE_15__["ExchangeRateAddComponent"],
                _verify_exchange_rate_verify_exchange_rate_component__WEBPACK_IMPORTED_MODULE_17__["VerifyExchangeRateComponent"],
            ]
        })
    ], ExchangeRateModule);
    return ExchangeRateModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.html":
/*!*************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.html ***!
  \*************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"row\" >\r\n  <div class=\"col-sm-12\">\r\n    <div class=\"row text-center\" >\r\n      <div class=\"col-sm-12\">\r\n        <h3>Are You Sure?</h3>\r\n      </div>\r\n    </div>\r\n    <hr />\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-12\">\r\n        <p>\r\n          This verification will lock in all exchange rates for all currencies\r\n          on this date. This applies to all offices.\r\n        </p>\r\n        <br />\r\n        <p class=\"text-center\"><strong>This action is not reversible!</strong></p>\r\n      </div>\r\n    </div>\r\n    <div class=\"row text-right\">\r\n        <div class=\"col-sm-4\"></div>\r\n      <div class=\"col-sm-4\">\r\n        <button mat-button color=\"warn\" (click)=\"onCloseVerifyDialog(false)\">\r\n          <strong>I'm Not Sure!</strong>\r\n        </button>\r\n      </div>\r\n      <div class=\"col-sm-4\">\r\n        <button\r\n          mat-raised-button\r\n          (click)=\"onCloseVerifyDialog(true)\"\r\n          color=\"accent\"\r\n        >\r\n          Verify\r\n        </button>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.scss":
/*!*************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.scss ***!
  \*************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2V4Y2hhbmdlLXJhdGUvdmVyaWZ5LWV4Y2hhbmdlLXJhdGUvdmVyaWZ5LWV4Y2hhbmdlLXJhdGUuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.ts":
/*!***********************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.ts ***!
  \***********************************************************************************************************/
/*! exports provided: VerifyExchangeRateComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VerifyExchangeRateComponent", function() { return VerifyExchangeRateComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
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



var VerifyExchangeRateComponent = /** @class */ (function () {
    function VerifyExchangeRateComponent(dialogRef, data, commonLoader) {
        this.dialogRef = dialogRef;
        this.data = data;
        this.commonLoader = commonLoader;
    }
    VerifyExchangeRateComponent.prototype.ngOnInit = function () {
    };
    VerifyExchangeRateComponent.prototype.onCloseVerifyDialog = function (verified) {
        this.dialogRef.close(verified);
    };
    VerifyExchangeRateComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-verify-exchange-rate',
            template: __webpack_require__(/*! ./verify-exchange-rate.component.html */ "./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.html"),
            styles: [__webpack_require__(/*! ./verify-exchange-rate.component.scss */ "./src/app/dashboard/accounting/exchange-rate/verify-exchange-rate/verify-exchange-rate.component.scss")]
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object, _shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_2__["CommonLoaderService"]])
    ], VerifyExchangeRateComponent);
    return VerifyExchangeRateComponent;
}());



/***/ })

}]);
//# sourceMappingURL=exchange-rate-exchange-rate-module.js.map