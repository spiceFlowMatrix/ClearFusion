(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["gain-loss-report-gain-loss-report-module"],{

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
/*! exports provided: LibraryComponent, LibraryModule, SubHeaderTemplateModule, LibraryService */
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

/***/ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report-routing.module.ts":
/*!******************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/gain-loss-report/gain-loss-report-routing.module.ts ***!
  \******************************************************************************************/
/*! exports provided: GainLossReportRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GainLossReportRoutingModule", function() { return GainLossReportRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _gain_loss_report_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./gain-loss-report.component */ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var routes = [
    {
        path: '', component: _gain_loss_report_component__WEBPACK_IMPORTED_MODULE_2__["GainLossReportComponent"]
    }
];
var GainLossReportRoutingModule = /** @class */ (function () {
    function GainLossReportRoutingModule() {
    }
    GainLossReportRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], GainLossReportRoutingModule);
    return GainLossReportRoutingModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.html":
/*!***************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.html ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"body-content\">\r\n  <div class=\"balance-sheet-main\">\r\n    <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-9\">\r\n          <div class=\"row\">\r\n            <div class=\"col-sm-12\">\r\n              <mat-card>\r\n                <div class=\"row\">\r\n                  <div class=\"col-sm-5\">\r\n                    <lib-search-dropdown\r\n                      placeholder=\"Accounts\"\r\n                      [multiSelect]=\"true\"\r\n                      placeholderSearchLabel=\"Find Account...\"\r\n                      noEntriesFoundLabel=\"No matching account found\"\r\n                      [dataSource]=\"accountDataSource\"\r\n                      [selectedValue]=\"gainLossReportfilter.AccountIdList\"\r\n                      (openedChange)=\"openedChange($event)\"\r\n                      (selectionChanged)=\"onSelectionChanged($event)\"\r\n                    >\r\n                    </lib-search-dropdown>\r\n\r\n                    <!-- <mat-form-field class=\"example-full-width\">\r\n                      <mat-select\r\n                        placeholder=\"Accounts\"\r\n                        name=\"Accounts\"\r\n                        multiple\r\n                        [(ngModel)]=\"gainLossReportfilter.AccountIdList\"\r\n                        (openedChange)=\"openedChange($event)\"\r\n                      >\r\n                      <ngx-mat-select-search [formControl]=\"accountMultiFilterCtrl\"\r\n                      [placeholderLabel]=\"'Find Account...'\"\r\n                      [noEntriesFoundLabel]=\"'No matching account found'\"\r\n                      ></ngx-mat-select-search>\r\n                        <mat-option\r\n                          *ngFor=\"let item of filteredAccountsMulti | async\"\r\n                          [value]=\"item.AccountCode\"\r\n                        >\r\n                          {{ item.AccountName }}\r\n                        </mat-option>\r\n                      </mat-select>\r\n                    </mat-form-field> -->\r\n                  </div>\r\n                  <div class=\"col-sm-7\">\r\n                    <button\r\n                      mat-button\r\n                      color=\"accent\"\r\n                      class=\"pull-right\"\r\n                      (click)=\"showVoucherSection()\"\r\n                    >\r\n                      {{ settingFlag ? \"Vouchers\" : \"Settings\" }}\r\n                      <mat-icon aria-label=\"icon\">chevron_right</mat-icon>\r\n                    </button>\r\n                  </div>\r\n                </div>\r\n              </mat-card>\r\n              <br />\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <mat-card [ngStyle]=\"scrollStyles\">\r\n                <div class=\"row\">\r\n                  <div class=\"col-sm-12\">\r\n                    <div class=\"col-sm-12\">\r\n                      <div class=\"responsive_table-responsive\">\r\n                        <table class=\"table table-bordered\">\r\n                          <tbody>\r\n                            <tr>\r\n                              <td width=\"1%\"><p class=\"width_8\"></p></td>\r\n                              <td width=\"29%\" class=\"text-left\">\r\n                                Account Code - Name\r\n                              </td>\r\n                              <td width=\"23%\" class=\"text-left\">\r\n                                Balance on original date\r\n                              </td>\r\n                              <td width=\"23%\" class=\"text-left\">\r\n                                Balance on current date\r\n                              </td>\r\n                              <td width=\"23%\" class=\"text-left\">\r\n                                Gain/Loss Amount\r\n                              </td>\r\n                              <td width=\"1%\" class=\"text-left\"></td>\r\n                            </tr>\r\n                            <tr\r\n                              *ngFor=\"let item of gainLossReportList\"\r\n                              class=\"all_grey_text\"\r\n                            >\r\n                              <td width=\"1%\"><p class=\"width_8\"></p></td>\r\n                              <td width=\"29%\">{{ item.AccountCodeName }}</td>\r\n                              <td width=\"23%\" text-align=\"left\">\r\n                                {{ item.BalanceOnOriginalDate }}\r\n                                {{\r\n                                  selectedCurrency | currencyCode: currencyList\r\n                                }}\r\n                              </td>\r\n                              <td width=\"23%\" text-align=\"left\">\r\n                                {{ item.BalanceOnCurrentDate }}\r\n                                {{\r\n                                  selectedCurrency | currencyCode: currencyList\r\n                                }}\r\n                              </td>\r\n                              <td width=\"23%\" class=\"left\">\r\n                                {{ item.GainLossAmount }}\r\n                                {{\r\n                                  selectedCurrency | currencyCode: currencyList\r\n                                }}\r\n                              </td>\r\n                              <td width=\"1%\" class=\"text-center\">\r\n                                <button\r\n                                  mat-icon-button\r\n                                  (click)=\"\r\n                                    deleteAccountFromFilter(item.AccountId)\r\n                                  \"\r\n                                >\r\n                                  <mat-icon aria-label=\"delete\"\r\n                                    >delete</mat-icon\r\n                                  >\r\n                                </button>\r\n                              </td>\r\n                            </tr>\r\n                          </tbody>\r\n                        </table>\r\n                      </div>\r\n                      <!-- <mat-paginator\r\n                            [length]=\"voucherFilter.totalCount\"\r\n                            [pageSize]=\"voucherFilter.pageSize\"\r\n                            [pageIndex]=\"voucherFilter.pageIndex\"\r\n                            [pageSizeOptions]=\"[5, 10, 25, 100]\"\r\n                            (page)=\"pageEvent($event)\"\r\n                          >\r\n                          </mat-paginator> -->\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n              </mat-card>\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm-3\">\r\n          <mat-card [ngStyle]=\"scrollStylesSearch\">\r\n            <div *ngIf=\"settingFlag; else voucherTemplate\">\r\n              <div class=\"row border_bottom padding_bottom_10\">\r\n                <div class=\"col-sm-7 font_large\">Settings</div>\r\n                <div class=\"col-sm-5\">\r\n                  <button\r\n                    mat-raised-button\r\n                    color=\"accent\"\r\n                    (click)=\"onGainLossFilterAppled()\"\r\n                    [disabled]=\"\r\n                      gainLossReportfilter.AccountIdList.length == 0 ||\r\n                      gainLossReportfilter.JournalIdList.length == 0 ||\r\n                      gainLossReportfilter.OfficeIdList.length == 0 ||\r\n                      gainLossReportfilter.ProjectIdList.length == 0 ||\r\n                      gainLossReportfilter.ToCurrencyId == null ||\r\n                      gainLossReportfilter.ComparisionDate == null ||\r\n                      gainLossReportfilter.FromDate == null ||\r\n                      gainLossReportfilter.ToDate == null\r\n                    \"\r\n                  >\r\n                    Apply\r\n                  </button>\r\n                </div>\r\n                <br />\r\n              </div>\r\n              <br />\r\n              <div class=\"row\">\r\n                <div class=\"col-sm-12 padding_bottom_10 all_grey_text\">\r\n                  All the setting below are used to configure the report on the\r\n                  left hand side.\r\n                </div>\r\n              </div>\r\n              <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <mat-select\r\n                      placeholder=\"Currency\"\r\n                      name=\"Currency\"\r\n                      [(ngModel)]=\"gainLossReportfilter.ToCurrencyId\"\r\n                    >\r\n                      <mat-option\r\n                        *ngFor=\"let item of currencyList\"\r\n                        [value]=\"item.CurrencyId\"\r\n                      >\r\n                        {{ item.CurrencyName }}\r\n                      </mat-option>\r\n                    </mat-select>\r\n                  </mat-form-field>\r\n\r\n                  <mat-form-field>\r\n                    <input\r\n                      matInput\r\n                      [matDatepicker]=\"comparionPicker\"\r\n                      placeholder=\"Comparison Date\"\r\n                      [(ngModel)]=\"gainLossReportfilter.ComparisionDate\"\r\n                    />\r\n                    <mat-datepicker-toggle\r\n                      matSuffix\r\n                      [for]=\"comparionPicker\"\r\n                    ></mat-datepicker-toggle>\r\n                    <mat-datepicker #comparionPicker></mat-datepicker>\r\n                  </mat-form-field>\r\n\r\n                  <mat-form-field>\r\n                    <input\r\n                      matInput\r\n                      [matDatepicker]=\"fromPicker\"\r\n                      placeholder=\"Transactions Starting From\"\r\n                      [(ngModel)]=\"gainLossReportfilter.FromDate\"\r\n                    />\r\n                    <mat-datepicker-toggle\r\n                      matSuffix\r\n                      [for]=\"fromPicker\"\r\n                    ></mat-datepicker-toggle>\r\n                    <mat-datepicker #fromPicker></mat-datepicker>\r\n                  </mat-form-field>\r\n\r\n                  <mat-form-field>\r\n                    <input\r\n                      matInput\r\n                      [matDatepicker]=\"toPicker\"\r\n                      placeholder=\"Transactions Until\"\r\n                      [(ngModel)]=\"gainLossReportfilter.ToDate\"\r\n                    />\r\n                    <mat-datepicker-toggle\r\n                      matSuffix\r\n                      [for]=\"toPicker\"\r\n                    ></mat-datepicker-toggle>\r\n                    <mat-datepicker #toPicker></mat-datepicker>\r\n                  </mat-form-field>\r\n\r\n                  <!--<mat-form-field class=\"example-full-width\">\r\n                    <mat-select\r\n                      placeholder=\"Offices\"\r\n                      name=\"Offices\"\r\n                      [(ngModel)]=\"gainLossReportfilter.OfficeIdList\"\r\n                      multiple\r\n                    >\r\n                      <mat-option\r\n                        *ngFor=\"let item of officeList\"\r\n                        [value]=\"item.OfficeId\"\r\n                      >\r\n                        {{ item.OfficeName }}\r\n                      </mat-option>\r\n                    </mat-select>\r\n                  </mat-form-field>\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <mat-select\r\n                      placeholder=\"Journals\"\r\n                      name=\"Journals\"\r\n                      [(ngModel)]=\"gainLossReportfilter.JournalIdList\"\r\n                      multiple\r\n                    >\r\n                      <mat-option\r\n                        *ngFor=\"let item of journalList\"\r\n                        [value]=\"item.JournalCode\"\r\n                      >\r\n                        {{ item.JournalName }}\r\n                      </mat-option>\r\n                    </mat-select>\r\n                  </mat-form-field>\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <mat-select\r\n                      placeholder=\"Projects\"\r\n                      name=\"Projects\"\r\n                      [(ngModel)]=\"gainLossReportfilter.ProjectIdList\"\r\n                      multiple\r\n                    >\r\n                      <mat-option\r\n                        *ngFor=\"let item of projectList\"\r\n                        [value]=\"item.ProjectId\"\r\n                      >\r\n                        {{ item.ProjectName }}\r\n                      </mat-option>\r\n                    </mat-select>\r\n                  </mat-form-field> -->\r\n\r\n                  <!-- office -->\r\n                  <div class=\"font_larger border_bottom padding_bottom_5\">\r\n                    Offices\r\n                    <button\r\n                      mat-icon-button\r\n                      (click)=\"openAddOfficeDialog()\"\r\n                      class=\"pull-right margin_top_minus_8px\"\r\n                    >\r\n                      <i class=\"material-icons\"> add </i>\r\n                    </button>\r\n                  </div>\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-12\">\r\n                      <ul\r\n                        *ngFor=\"let item of officeList\"\r\n                        class=\"padding_left_0\"\r\n                      >\r\n                        <div\r\n                          *ngIf=\"item.IsChecked === true\"\r\n                          class=\"border_bottom\"\r\n                        >\r\n                          <button\r\n                            mat-icon-button\r\n                            (click)=\"removeOfficeFromList(item.OfficeId)\"\r\n                          >\r\n                            <i class=\"fas fa-minus-circle\"></i>\r\n                          </button>\r\n                          {{ item.OfficeName }}\r\n                        </div>\r\n                      </ul>\r\n                    </div>\r\n                  </div>\r\n\r\n                  <!-- journals -->\r\n                  <div class=\"font_larger border_bottom padding_bottom_5\">\r\n                    Journals\r\n                    <button\r\n                      mat-icon-button\r\n                      (click)=\"openAddJournalDialog()\"\r\n                      class=\"pull-right margin_top_minus_8px\"\r\n                    >\r\n                      <i class=\"material-icons\"> add </i>\r\n                    </button>\r\n                  </div>\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-12\">\r\n                      <ul\r\n                        *ngFor=\"let item of journalList\"\r\n                        class=\"padding_left_0\"\r\n                      >\r\n                        <div\r\n                          *ngIf=\"item.IsChecked === true\"\r\n                          class=\"border_bottom\"\r\n                        >\r\n                          <button\r\n                            mat-icon-button\r\n                            (click)=\"removeJournalFromList(item.JournalCode)\"\r\n                          >\r\n                            <i class=\"fas fa-minus-circle\"></i>\r\n                          </button>\r\n                          {{ item.JournalName }}\r\n                        </div>\r\n                      </ul>\r\n                    </div>\r\n                  </div>\r\n\r\n                  <!-- project -->\r\n                  <div class=\"font_larger border_bottom padding_bottom_5\">\r\n                    Project\r\n                    <button\r\n                      mat-icon-button\r\n                      (click)=\"openAddProjectDialog()\"\r\n                      class=\"pull-right margin_top_minus_8px\"\r\n                    >\r\n                      <i class=\"material-icons\"> add </i>\r\n                    </button>\r\n                  </div>\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-12\">\r\n                      <ul\r\n                        *ngFor=\"let item of projectList\"\r\n                        class=\"padding_left_0\"\r\n                      >\r\n                        <div\r\n                          *ngIf=\"item.IsChecked === true\"\r\n                          class=\"border_bottom\"\r\n                        >\r\n                          <button\r\n                            mat-icon-button\r\n                            (click)=\"removeProjectFromList(item.ProjectId)\"\r\n                          >\r\n                            <i class=\"fas fa-minus-circle\"></i>\r\n                          </button>\r\n                          {{ item.ProjectName }}\r\n                        </div>\r\n                      </ul>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n            </div>\r\n            <ng-template #voucherTemplate>\r\n              <div>\r\n                <div class=\"row border_bottom padding_bottom_10\">\r\n                  <div class=\"col-sm-7 font_large\">Vouchers</div>\r\n                  <br />\r\n                </div>\r\n                <br />\r\n                <div class=\"row\">\r\n                  <div class=\"col-sm-12 padding_bottom_10 all_grey_text\">\r\n                    The report on the left represent the following statistic\r\n                  </div>\r\n                </div>\r\n                <div class=\"row padding_bottom_10 border_bottom\">\r\n                  <div class=\"col-sm-5\">\r\n                    <strong>Net Gain</strong>\r\n                  </div>\r\n                  <div class=\"col-sm-7\">\r\n                    <span class=\" pull-right\"\r\n                      >{{ gainLossAddVoucherForm.Amount | number }}\r\n                      {{ selectedCurrency | currencyCode: currencyList }}\r\n                    </span>\r\n                  </div>\r\n                </div>\r\n                <div class=\"border_bottom\">\r\n                  <div class=\"row\">\r\n                    <br />\r\n                    <div class=\"col-sm-8\">\r\n                      <button\r\n                        mat-raised-button\r\n                        [disabled]=\"addVoucherValidation()\"\r\n                        color=\"accent\"\r\n                        (click)=\"onGainLossVoucher()\"\r\n                      >\r\n                        Add Voucher\r\n                      </button>\r\n                      <br />\r\n                      <br />\r\n                    </div>\r\n                    <div class=\"col-sm-2\">\r\n                      <button\r\n                        mat-icon-button\r\n                        pTooltip=\"Reset\"\r\n                        tooltipPosition=\"top\"\r\n                        *ngIf=\"\r\n                          !gainLossAddVoucherLoader;\r\n                          else gainLossAddVoucherLoaderTemplate\r\n                        \"\r\n                        (click)=\"onResetAddVoucherForm()\"\r\n                      >\r\n                        <mat-icon aria-label=\"reset\">refresh</mat-icon>\r\n                      </button>\r\n\r\n                      <ng-template #gainLossAddVoucherLoaderTemplate>\r\n                        <mat-spinner [diameter]=\"25\"></mat-spinner>\r\n                      </ng-template>\r\n                    </div>\r\n                  </div>\r\n\r\n                  <!-- Office -->\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-10\">\r\n                      <mat-form-field class=\"example-full-width\">\r\n                        <mat-select\r\n                          placeholder=\"Office\"\r\n                          name=\"Office\"\r\n                          [(ngModel)]=\"gainLossAddVoucherForm.OfficeId\"\r\n                        >\r\n                          <mat-option\r\n                            *ngFor=\"let item of officeList\"\r\n                            [value]=\"item.OfficeId\"\r\n                          >\r\n                            {{ item.OfficeName }}\r\n                          </mat-option>\r\n                        </mat-select>\r\n                      </mat-form-field>\r\n                    </div>\r\n                  </div>\r\n                  <!-- voucher type -->\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-10\">\r\n                      <mat-form-field class=\"example-full-width\">\r\n                        <mat-select\r\n                          placeholder=\"Voucher Type\"\r\n                          name=\"VoucherType\"\r\n                          [(ngModel)]=\"gainLossAddVoucherForm.VoucherType\"\r\n                        >\r\n                          <mat-option\r\n                            *ngFor=\"let item of voucherTypeList\"\r\n                            [value]=\"item.VoucherTypeId\"\r\n                          >\r\n                            {{ item.VoucherTypeName }}\r\n                          </mat-option>\r\n                        </mat-select>\r\n                      </mat-form-field>\r\n                    </div>\r\n                  </div>\r\n                  <!-- journal -->\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-10\">\r\n                      <mat-form-field class=\"example-full-width\">\r\n                        <mat-select\r\n                          placeholder=\"Journals\"\r\n                          name=\"Journal\"\r\n                          [(ngModel)]=\"gainLossAddVoucherForm.JournalId\"\r\n                        >\r\n                          <mat-option\r\n                            *ngFor=\"let item of journalList\"\r\n                            [value]=\"item.JournalCode\"\r\n                          >\r\n                            {{ item.JournalName }}\r\n                          </mat-option>\r\n                        </mat-select>\r\n                      </mat-form-field>\r\n                    </div>\r\n                  </div>\r\n                  <!-- Credit Account -->\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-10\">\r\n                      <mat-form-field class=\"example-full-width\">\r\n                        <mat-select\r\n                          placeholder=\"Credit Account\"\r\n                          name=\"CreditAccount\"\r\n                          [(ngModel)]=\"gainLossAddVoucherForm.CreditAccount\"\r\n                        >\r\n                          <mat-option\r\n                            *ngFor=\"let item of accountList\"\r\n                            [value]=\"item.AccountCode\"\r\n                          >\r\n                            {{ item.AccountName }}\r\n                          </mat-option>\r\n                        </mat-select>\r\n                      </mat-form-field>\r\n                    </div>\r\n                  </div>\r\n                  <!-- Debit Account -->\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-10\">\r\n                      <mat-form-field class=\"example-full-width\">\r\n                        <mat-select\r\n                          placeholder=\"Debit Account\"\r\n                          name=\"DebitAccount\"\r\n                          [(ngModel)]=\"gainLossAddVoucherForm.DebitAccount\"\r\n                        >\r\n                          <mat-option\r\n                            *ngFor=\"let item of accountList\"\r\n                            [value]=\"item.AccountCode\"\r\n                          >\r\n                            {{ item.AccountName }}\r\n                          </mat-option>\r\n                        </mat-select>\r\n                      </mat-form-field>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n                <div>\r\n                  <div class=\"row\">\r\n                    <br />\r\n                    <div class=\"col-sm-8 font_larger margin_top_10\">\r\n                      Existing Vouchers\r\n                    </div>\r\n                    <div class=\"col-sm-2\">\r\n                      <button\r\n                        mat-icon-button\r\n                        pTooltip=\"Refresh\"\r\n                        tooltipPosition=\"top\"\r\n                        *ngIf=\"\r\n                          !gainLossListVoucherLoader;\r\n                          else gainLossVoucherListLoaderTemplate\r\n                        \"\r\n                        (click)=\"onResetGainLossVoucherList()\"\r\n                      >\r\n                        <mat-icon aria-label=\"refresh\">refresh</mat-icon>\r\n                      </button>\r\n                      <ng-template #gainLossVoucherListLoaderTemplate>\r\n                        <mat-spinner\r\n                          [diameter]=\"20\"\r\n                          class=\"margin_top_10\"\r\n                        ></mat-spinner>\r\n                      </ng-template>\r\n                    </div>\r\n                  </div>\r\n                  <br />\r\n                  <div class=\"row\">\r\n                    <div class=\"col-sm-12\">\r\n                      <div class=\"responsive_table-responsive\">\r\n                        <table class=\"table table-bordered\">\r\n                          <tbody>\r\n                            <tr>\r\n                              <td width=\"1%\"><p class=\"width_8\"></p></td>\r\n                              <td width=\"39%\" class=\"text-left\">\r\n                                Name\r\n                              </td>\r\n                              <td width=\"30%\" class=\"text-left\">\r\n                                Journal\r\n                              </td>\r\n                              <td width=\"39%\" class=\"text-left\">\r\n                                Date\r\n                              </td>\r\n                              <td width=\"1%\" class=\"text-left\"></td>\r\n                            </tr>\r\n                            <tr\r\n                              *ngFor=\"let item of gainLossVoucherList\"\r\n                              class=\"all_grey_text\"\r\n                            >\r\n                              <td width=\"1%\"><p class=\"width_8\"></p></td>\r\n                              <td width=\"25%\">{{ item.VoucherName }}</td>\r\n                              <td width=\"25%\" text-align=\"left\">\r\n                                {{ item.JournalName }}\r\n                              </td>\r\n                              <td width=\"25%\" text-align=\"left\">\r\n                                {{ item.VoucherDate | date: \"dd MMM, yyyy\" }}\r\n                              </td>\r\n                              <td width=\"25%\" class=\"text-center\">\r\n                                <button\r\n                                  mat-icon-button\r\n                                  color=\"warn\"\r\n                                  *ngIf=\"\r\n                                    !item.IsLoading && !item.IsError;\r\n                                    else gainLossVoucherActionTemplate\r\n                                  \"\r\n                                  (click)=\"\r\n                                    onDeleteGainLossVoucher(item.VoucherId)\r\n                                  \"\r\n                                >\r\n                                  <mat-icon aria-label=\"delete\"\r\n                                    >delete</mat-icon\r\n                                  >\r\n                                </button>\r\n                                <ng-template #gainLossVoucherActionTemplate>\r\n                                  <button\r\n                                    mat-icon-button\r\n                                    color=\"warn\"\r\n                                    *ngIf=\"\r\n                                      item.IsError && !item.IsLoading;\r\n                                      else gainLossVoucherLoaderTemplate\r\n                                    \"\r\n                                  >\r\n                                    <mat-icon aria-label=\"warning\"\r\n                                      >warning</mat-icon\r\n                                    >\r\n                                  </button>\r\n                                  <ng-template #gainLossVoucherLoaderTemplate>\r\n                                    <mat-spinner [diameter]=\"20\"></mat-spinner>\r\n                                  </ng-template>\r\n                                </ng-template>\r\n                              </td>\r\n                              <td width=\"1%\"><p class=\"width_8\"></p></td>\r\n                            </tr>\r\n                          </tbody>\r\n                        </table>\r\n                      </div>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n            </ng-template>\r\n          </mat-card>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.scss":
/*!***************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.scss ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2dhaW4tbG9zcy1yZXBvcnQvZ2Fpbi1sb3NzLXJlcG9ydC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.ts":
/*!*************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.ts ***!
  \*************************************************************************************/
/*! exports provided: GainLossReportComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GainLossReportComponent", function() { return GainLossReportComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _gain_loss_report_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./gain-loss-report.service */ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _multi_select_list_multi_select_list_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./multi-select-list/multi-select-list.component */ "./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var rxjs_internal_Subject__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! rxjs/internal/Subject */ "./node_modules/rxjs/internal/Subject.js");
/* harmony import */ var rxjs_internal_Subject__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_Subject__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! src/app/shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! src/app/shared/services/localstorage.service */ "./src/app/shared/services/localstorage.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};














var GainLossReportComponent = /** @class */ (function () {
    //#endregion
    function GainLossReportComponent(gainLossReportService, toastr, commonLoader, dialog, globalService, localStorageService) {
        this.gainLossReportService = gainLossReportService;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.dialog = dialog;
        this.globalService = globalService;
        this.localStorageService = localStorageService;
        //#region "variable"
        this.setProjectHeader = 'Currency Exchange Report';
        this.gainLossReportList = [];
        this.accountList = [];
        this.currencyList = [];
        this.officeList = [];
        this.journalList = [];
        this.projectList = [];
        this.voucherTypeList = [];
        this.gainLossVoucherList = [];
        this.selectedCurrency = null;
        this.isEditingAllowed = false;
        this.pageId = src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_12__["ApplicationPages"].ExchangeGainLoss;
        //#region "accounting filter"
        /** control for the MatSelect filter keyword multi-selection */
        this.accountMultiFilterCtrl = new _angular_forms__WEBPACK_IMPORTED_MODULE_7__["FormControl"]();
        /** list of accounts filtered by search keyword */
        this.filteredAccountsMulti = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_8__["ReplaySubject"](1);
        /** Subject that emits when the component has been destroyed. */
        this._onDestroy = new rxjs_internal_Subject__WEBPACK_IMPORTED_MODULE_9__["Subject"]();
        //#endregion
        // flag
        this.settingFlag = true;
        this.gainLossAddVoucherLoader = false;
        this.gainLossListVoucherLoader = false;
        this.getScreenSize();
        // Set Menu Header Name
        this.globalService.setMenuHeaderName(this.setProjectHeader);
        // Set Menu Header List
        this.globalService.setMenuList([]);
    }
    GainLossReportComponent.prototype.ngOnInit = function () {
        this.initFilter();
        this.initAddVoucherForm();
        this.getCurrencyList();
        this.getOfficeList();
        this.getJournalList();
        this.getProjectList();
        this.getInputLevelAccountList();
        this.getExchangeGainLossFilterAccountList();
        this.getVoucherTypeList();
        this.getGainLossVoucherList();
        this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
    };
    GainLossReportComponent.prototype.ngAfterViewInit = function () {
        // this.setAccountInitialValue();
    };
    GainLossReportComponent.prototype.ngOnDestroy = function () {
        this._onDestroy.next();
        this._onDestroy.complete();
    };
    //#region "Dynamic Scroll"
    GainLossReportComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 220 + 'px',
            'overflow-x': 'hidden'
        };
        this.scrollStylesSearch = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    //#region "Add Office Popup"
    GainLossReportComponent.prototype.openAddOfficeDialog = function () {
        var _this = this;
        // NOTE: It passed the data into the Add Voucher Model
        var dialogRef = this.dialog.open(_multi_select_list_multi_select_list_component__WEBPACK_IMPORTED_MODULE_4__["MultiSelectListComponent"], {
            width: '350px',
            data: {
                data: this.officeList,
                showHideDropdown: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_6__["ShowHideDropdownEnum"].Office,
                selectedValues: this.gainLossReportfilter.OfficeIdList
            }
        });
        dialogRef.componentInstance.officeItemAddRemove.subscribe(function (data) {
            // do something
            _this.getCheckedOfficeList();
        });
        dialogRef.afterClosed().subscribe(function (result) { });
    };
    //#endregion
    //#region "Add Journal Popup"
    GainLossReportComponent.prototype.openAddJournalDialog = function () {
        var _this = this;
        // NOTE: It passed the data into the Add Voucher Model
        var dialogRef = this.dialog.open(_multi_select_list_multi_select_list_component__WEBPACK_IMPORTED_MODULE_4__["MultiSelectListComponent"], {
            width: '350px',
            data: {
                data: this.journalList,
                showHideDropdown: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_6__["ShowHideDropdownEnum"].Journal,
                selectedValues: this.gainLossReportfilter.JournalIdList
            }
        });
        dialogRef.componentInstance.journalItemAddRemove.subscribe(function (data) {
            // do something
            _this.getCheckedJournalList();
        });
        dialogRef.afterClosed().subscribe(function (result) { });
    };
    //#endregion
    //#region "Add Project Popup"
    GainLossReportComponent.prototype.openAddProjectDialog = function () {
        var _this = this;
        // NOTE: It passed the data into the Add Voucher Model
        var dialogRef = this.dialog.open(_multi_select_list_multi_select_list_component__WEBPACK_IMPORTED_MODULE_4__["MultiSelectListComponent"], {
            width: '350px',
            data: {
                data: this.projectList,
                showHideDropdown: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_6__["ShowHideDropdownEnum"].Project,
                selectedValues: this.gainLossReportfilter.ProjectIdList
            }
        });
        dialogRef.componentInstance.projectItemAddRemove.subscribe(function (data) {
            // do something
            _this.getCheckedProjectList();
        });
        dialogRef.afterClosed().subscribe(function (result) { });
    };
    //#endregion
    //#region "initFilter"
    GainLossReportComponent.prototype.initFilter = function () {
        this.gainLossReportfilter = {
            ToCurrencyId: null,
            ComparisionDate: new Date(),
            ToDate: new Date(),
            FromDate: new Date(),
            OfficeIdList: [],
            JournalIdList: [],
            ProjectIdList: [],
            AccountIdList: []
        };
    };
    //#endregion
    //#region "initAddVoucherForm"
    GainLossReportComponent.prototype.initAddVoucherForm = function () {
        this.gainLossAddVoucherForm = {
            CurrencyId: null,
            JournalId: null,
            CreditAccount: null,
            DebitAccount: null,
            Amount: 0,
            VoucherType: null,
            OfficeId: null
        };
    };
    //#endregion
    //#region "getVoucherTypeList"
    GainLossReportComponent.prototype.getVoucherTypeList = function () {
        var _this = this;
        this.gainLossReportService.GetVoucherTypeList().subscribe(function (response) {
            _this.voucherTypeList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.voucherTypeList.push({
                        VoucherTypeId: element.VoucherTypeId,
                        VoucherTypeName: element.VoucherTypeName
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getInputLevelAccountList"
    GainLossReportComponent.prototype.getInputLevelAccountList = function () {
        var _this = this;
        this.gainLossReportService.GetInputLevelAccountList().subscribe(function (response) {
            _this.accountList = [];
            _this.accountDataSource = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.accountList.push({
                        AccountCode: element.AccountCode,
                        AccountName: element.AccountName,
                        ChartOfAccountNewCode: element.ChartOfAccountNewCode
                    });
                    _this.accountDataSource.push({
                        Id: element.AccountCode,
                        Name: element.AccountName,
                    });
                });
                // NOTE: load the initial Account list
                _this.filteredAccountsMulti.next(_this.accountList.slice());
                // listen for search field value changes
                _this.accountMultiFilterCtrl.valueChanges
                    .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_10__["takeUntil"])(_this._onDestroy))
                    .subscribe(function () {
                    _this.filterAccounts();
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getCurrencyList"
    GainLossReportComponent.prototype.getCurrencyList = function () {
        var _this = this;
        this.gainLossReportService.GetCurrencyList().subscribe(function (response) {
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
    //#region "getOfficeList"
    GainLossReportComponent.prototype.getOfficeList = function () {
        var _this = this;
        this.gainLossReportService.GetOfficeList().subscribe(function (response) {
            _this.officeList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.officeList.push({
                        OfficeId: element.OfficeId,
                        OfficeName: element.OfficeName,
                        IsChecked: false
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getJournalList"
    GainLossReportComponent.prototype.getJournalList = function () {
        var _this = this;
        this.gainLossReportService.GetJournalList().subscribe(function (response) {
            _this.journalList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.journalList.push({
                        JournalCode: element.JournalCode,
                        JournalName: element.JournalName,
                        JournalType: element.JournalType,
                        IsChecked: false
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getProjectList"
    GainLossReportComponent.prototype.getProjectList = function () {
        var _this = this;
        this.gainLossReportService.GetProjectList().subscribe(function (response) {
            _this.projectList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.projectList.push({
                        ProjectId: element.ProjectId,
                        ProjectName: element.ProjectName,
                        ProjectCode: element.ProjectCode,
                        IsChecked: false
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getGainLossVoucherList"
    GainLossReportComponent.prototype.getGainLossVoucherList = function () {
        var _this = this;
        this.gainLossListVoucherLoader = true;
        this.gainLossReportService.GetGainLossVoucherList().subscribe(function (response) {
            _this.gainLossVoucherList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.gainLossVoucherList.push({
                        VoucherId: element.VoucherId,
                        VoucherName: element.VoucherName,
                        JournalName: element.JournalName,
                        VoucherDate: element.VoucherDate,
                        IsDeleted: element.IsDeleted,
                        IsError: element.IsError,
                        IsLoading: element.IsLoading
                    });
                });
            }
            _this.gainLossListVoucherLoader = false;
        }, function (error) {
            _this.gainLossListVoucherLoader = false;
        });
    };
    //#endregion
    //#region "addGainLossVoucher"
    GainLossReportComponent.prototype.addGainLossVoucher = function (data) {
        var _this = this;
        if (this.gainLossAddVoucherForm.CurrencyId !== null) {
            this.gainLossAddVoucherLoader = true;
            this.gainLossReportService.AddGainLossVoucher(data).subscribe(function (response) {
                if (response.statusCode === 200 && response.data !== null) {
                    _this.gainLossVoucherList.push(response.data);
                    // this.toastr.success('Voucher Created Successfully');
                }
                else {
                    _this.toastr.error(response.message);
                }
                _this.gainLossAddVoucherLoader = false;
            }, function (error) {
                _this.gainLossAddVoucherLoader = false;
                _this.toastr.error(error);
            });
        }
        else {
            this.toastr.warning('Please select Currency');
        }
    };
    //#endregion
    //#region "onGainLossFilterAppled"
    GainLossReportComponent.prototype.onGainLossFilterAppled = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.gainLossReportfilter.ComparisionDate = new Date(new Date(this.gainLossReportfilter.ComparisionDate).getFullYear(), new Date(this.gainLossReportfilter.ComparisionDate).getMonth(), new Date(this.gainLossReportfilter.ComparisionDate).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds());
        this.gainLossReportfilter.FromDate = new Date(new Date(this.gainLossReportfilter.FromDate).getFullYear(), new Date(this.gainLossReportfilter.FromDate).getMonth(), new Date(this.gainLossReportfilter.FromDate).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds());
        this.gainLossReportfilter.ToDate = new Date(new Date(this.gainLossReportfilter.ToDate).getFullYear(), new Date(this.gainLossReportfilter.ToDate).getMonth(), new Date(this.gainLossReportfilter.ToDate).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds());
        this.selectedCurrency = this.gainLossReportfilter.ToCurrencyId;
        this.gainLossReportService
            .GetGainLossReportList(this.gainLossReportfilter)
            .subscribe(function (response) {
            _this.gainLossReportList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.gainLossReportList.push({
                        AccountId: element.AccountId,
                        AccountCode: element.AccountCode,
                        AccountName: element.AccountName,
                        AccountCodeName: element.AccountCodeName,
                        BalanceOnOriginalDate: element.BalanceOnOriginalDate,
                        BalanceOnCurrentDate: element.BalanceOnCurrentDate,
                        GainLossAmount: element.GainLossAmount
                    });
                });
                _this.sumOfGainLossAmount();
            }
            else {
                _this.toastr.error(response.message);
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.toastr.error('Someting went wrong!');
            _this.commonLoader.hideLoader();
        });
    };
    //#endregion
    //#region "deleteAccountFromFilter"
    GainLossReportComponent.prototype.deleteAccountFromFilter = function (accountId) {
        this.toastr.success('Account Removed');
        var index = this.gainLossReportList.findIndex(function (x) { return x.AccountId === accountId; });
        if (index !== -1) {
            this.gainLossReportList.splice(index, 1);
        }
        // this.gainLossReportService.deleteAccountFromFilter().subscribe(
        //   (response: IResponseData) => {
        //       if (response.statusCode === 200 && response.data !== null) {
        //        this.toastr.success('Account Removed');
        //       }
        //    },
        //   (error) => {
        //   }
        // );
    };
    //#endregion
    GainLossReportComponent.prototype.onSelectionChanged = function (event) {
        // this.gainLossReportfilter.AccountIdList = event;
    };
    // #region "gainLossAccountSelectionChanged"
    GainLossReportComponent.prototype.openedChange = function (event) {
        this.gainLossReportfilter.AccountIdList = event.Value;
        if (!event.Flag) {
            this.saveExchangeGainLossFilterAccountList();
        }
    };
    // #endregion
    //#region "getExchangeGainLossFilterAccountList"
    GainLossReportComponent.prototype.saveExchangeGainLossFilterAccountList = function () {
        var model = {
            AccountIds: this.gainLossReportfilter.AccountIdList,
            CurrencyId: this.gainLossReportfilter.ToCurrencyId
        };
        this.gainLossReportService.SaveExchangeGainLossFilterAccountList(model).subscribe(function (response) {
            if (response.statusCode === 200) {
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getExchangeGainLossFilterAccountList"
    GainLossReportComponent.prototype.getExchangeGainLossFilterAccountList = function () {
        var _this = this;
        this.gainLossReportService.GetExchangeGainLossFilterAccountList().subscribe(function (response) {
            _this.gainLossReportfilter.AccountIdList = [];
            if (response.statusCode === 200 && response.data !== null) {
                // response.data.forEach(element => {
                //   this.gainLossReportfilter.AccountIdList.push(
                //     element.ChartOfAccountNewId
                //   );
                // });
                _this.gainLossReportfilter.AccountIdList = response.data;
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getSelectedProjectItems"
    GainLossReportComponent.prototype.getSelectedProjectItems = function () {
        var _this = this;
        return this.gainLossReportfilter.ProjectIdList.map(function (y) { return _this.projectList.find(function (x) { return x.ProjectId === y; }).ProjectName; });
        // return this.projectList.forEach(x => x.ProjectId === projectIds).ProjectName;
    };
    //#endregion
    //#region "removeOfficeFromList"
    GainLossReportComponent.prototype.removeOfficeFromList = function (officeId) {
        this.officeList.map(function (x) {
            if (x.OfficeId === officeId) {
                x.IsChecked = false;
            }
        });
        this.getCheckedOfficeList();
    };
    //#endregion
    //#region "removeJournalFromList"
    GainLossReportComponent.prototype.removeJournalFromList = function (journalId) {
        this.journalList.map(function (x) {
            if (x.JournalCode === journalId) {
                x.IsChecked = false;
            }
        });
        this.getCheckedJournalList();
    };
    //#endregion
    //#region "removeProjectFromList"
    GainLossReportComponent.prototype.removeProjectFromList = function (projectId) {
        this.projectList.map(function (x) {
            if (x.ProjectId === projectId) {
                x.IsChecked = false;
            }
        });
        this.getCheckedProjectList();
    };
    //#endregion
    //#region "getCheckedOfficeList"
    GainLossReportComponent.prototype.getCheckedOfficeList = function () {
        this.gainLossReportfilter.OfficeIdList = this.officeList
            .filter(function (x) { return x.IsChecked === true; })
            .map(function (x) {
            return x.OfficeId;
        });
    };
    //#endregion
    //#region "getCheckedJournalList"
    GainLossReportComponent.prototype.getCheckedJournalList = function () {
        this.gainLossReportfilter.JournalIdList = this.journalList
            .filter(function (x) { return x.IsChecked === true; })
            .map(function (x) {
            return x.JournalCode;
        });
    };
    //#endregion
    //#region "getCheckedProjectList"
    GainLossReportComponent.prototype.getCheckedProjectList = function () {
        this.gainLossReportfilter.ProjectIdList = this.projectList
            .filter(function (x) { return x.IsChecked === true; })
            .map(function (x) {
            return x.ProjectId;
        });
    };
    //#endregion
    //#region "deleteGainLossVoucher"
    GainLossReportComponent.prototype.deleteGainLossVoucher = function (voucherId) {
        var _this = this;
        var voucherIndex = this.gainLossVoucherList.findIndex(function (x) { return x.VoucherId === voucherId; });
        if (voucherIndex !== -1) {
            this.gainLossVoucherList[voucherIndex].IsLoading = true;
            this.gainLossReportService.DeleteGainLossVoucher(voucherId).subscribe(function (response) {
                if (response.statusCode === 200) {
                    _this.gainLossVoucherList.splice(voucherIndex, 1);
                }
                else {
                    _this.toastr.error(response.message);
                    _this.gainLossVoucherList[voucherIndex].IsLoading = false;
                    _this.gainLossVoucherList[voucherIndex].IsError = true;
                }
            }, function (error) {
                _this.toastr.error('Someting went wrong!');
                _this.gainLossVoucherList[voucherIndex].IsLoading = false;
                _this.gainLossVoucherList[voucherIndex].IsError = true;
            });
        }
        else {
            this.toastr.error('Voucher not found');
        }
    };
    //#endregion
    //#region "onDeleteGainLossVoucher"
    GainLossReportComponent.prototype.onDeleteGainLossVoucher = function (voucherId) {
        this.deleteGainLossVoucher(voucherId);
    };
    //#endregion
    //#region "onGainLossVoucher"
    GainLossReportComponent.prototype.onGainLossVoucher = function () {
        if (this.gainLossAddVoucherForm.Amount !== 0) {
            this.gainLossAddVoucherForm.CurrencyId = this.selectedCurrency;
            this.gainLossAddVoucherForm.TimeZoneOffset = new Date().getTimezoneOffset();
            this.addGainLossVoucher(this.gainLossAddVoucherForm);
        }
        else {
            this.toastr.warning('Amount should be greater than 0');
        }
    };
    //#endregion
    //#region "onResetAddVoucherForm"
    GainLossReportComponent.prototype.onResetAddVoucherForm = function () {
        this.gainLossAddVoucherForm.JournalId = null;
        this.gainLossAddVoucherForm.CreditAccount = null;
        this.gainLossAddVoucherForm.DebitAccount = null;
        this.gainLossAddVoucherForm.VoucherType = null;
        this.gainLossAddVoucherForm.OfficeId = null;
    };
    //#endregion
    //#region "onResetGainLossVoucherList"
    GainLossReportComponent.prototype.onResetGainLossVoucherList = function () {
        this.getGainLossVoucherList();
    };
    //#endregion
    //#region "sumOfGainLossAmount"
    GainLossReportComponent.prototype.sumOfGainLossAmount = function () {
        this.gainLossAddVoucherForm.Amount = this.gainLossReportList.reduce(function (a, _a) {
            var GainLossAmount = _a.GainLossAmount;
            return a + GainLossAmount;
        }, 0);
    };
    //#endregion
    //#region "showVoucherSection"
    GainLossReportComponent.prototype.showVoucherSection = function () {
        this.settingFlag = !this.settingFlag;
    };
    //#endregion
    //#region "addVoucherValidation"
    GainLossReportComponent.prototype.addVoucherValidation = function () {
        return this.selectedCurrency == null ||
            this.gainLossAddVoucherForm.CreditAccount == null ||
            this.gainLossAddVoucherForm.DebitAccount == null ||
            this.gainLossAddVoucherForm.JournalId == null ||
            this.gainLossAddVoucherForm.OfficeId == null ||
            this.gainLossAddVoucherForm.VoucherType == null ||
            this.gainLossAddVoucherLoader === true ||
            this.gainLossReportList.length === 0
            ? true
            : false;
    };
    //#endregion
    //#region "FILTER: Account filter"
    GainLossReportComponent.prototype.filterAccounts = function () {
        if (!this.accountList) {
            return;
        }
        // get the search keyword
        var search = this.accountMultiFilterCtrl.value;
        if (!search) {
            this.filteredAccountsMulti.next(this.accountList.slice());
            return;
        }
        else {
            search = search.toLowerCase();
        }
        // filter the accounts
        this.filteredAccountsMulti.next(this.accountList.filter(function (acc) { return acc.AccountName.toLowerCase().indexOf(search) > -1; }));
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], GainLossReportComponent.prototype, "getScreenSize", null);
    GainLossReportComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-gain-loss-report',
            template: __webpack_require__(/*! ./gain-loss-report.component.html */ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.html"),
            styles: [__webpack_require__(/*! ./gain-loss-report.component.scss */ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.scss")]
        }),
        __metadata("design:paramtypes", [_gain_loss_report_service__WEBPACK_IMPORTED_MODULE_1__["GainLossReportService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__["CommonLoaderService"],
            _angular_material_dialog__WEBPACK_IMPORTED_MODULE_5__["MatDialog"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_11__["GlobalSharedService"],
            src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_13__["LocalStorageService"]])
    ], GainLossReportComponent);
    return GainLossReportComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.module.ts":
/*!**********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.module.ts ***!
  \**********************************************************************************/
/*! exports provided: GainLossReportModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GainLossReportModule", function() { return GainLossReportModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _gain_loss_report_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./gain-loss-report-routing.module */ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report-routing.module.ts");
/* harmony import */ var _multi_select_list_multi_select_list_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./multi-select-list/multi-select-list.component */ "./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.ts");
/* harmony import */ var _gain_loss_report_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./gain-loss-report.component */ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.component.ts");
/* harmony import */ var _angular_material_input__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/material/input */ "./node_modules/@angular/material/esm5/input.es5.js");
/* harmony import */ var _angular_material_button__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/material/button */ "./node_modules/@angular/material/esm5/button.es5.js");
/* harmony import */ var _angular_material_card__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/material/card */ "./node_modules/@angular/material/esm5/card.es5.js");
/* harmony import */ var _angular_material_paginator__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material/paginator */ "./node_modules/@angular/material/esm5/paginator.es5.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/material/datepicker */ "./node_modules/@angular/material/esm5/datepicker.es5.js");
/* harmony import */ var _angular_material_core__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/material/core */ "./node_modules/@angular/material/esm5/core.es5.js");
/* harmony import */ var _angular_material_icon__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/material/icon */ "./node_modules/@angular/material/esm5/icon.es5.js");
/* harmony import */ var _angular_material_select__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material/select */ "./node_modules/@angular/material/esm5/select.es5.js");
/* harmony import */ var _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/material/progress-spinner */ "./node_modules/@angular/material/esm5/progress-spinner.es5.js");
/* harmony import */ var ngx_mat_select_search__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ngx-mat-select-search */ "./node_modules/ngx-mat-select-search/fesm5/ngx-mat-select-search.js");
/* harmony import */ var primeng_primeng__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! primeng/primeng */ "./node_modules/primeng/primeng.js");
/* harmony import */ var primeng_primeng__WEBPACK_IMPORTED_MODULE_16___default = /*#__PURE__*/__webpack_require__.n(primeng_primeng__WEBPACK_IMPORTED_MODULE_16__);
/* harmony import */ var projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! projects/library/src/public_api */ "./projects/library/src/public_api.ts");
/* harmony import */ var _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! @angular/material/checkbox */ "./node_modules/@angular/material/esm5/checkbox.es5.js");
/* harmony import */ var _gain_loss_report_service__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ./gain-loss-report.service */ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_pipes_pipe_export_pipe_export_module__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! src/app/shared/pipes/pipe-export/pipe-export.module */ "./src/app/shared/pipes/pipe-export/pipe-export.module.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






















var GainLossReportModule = /** @class */ (function () {
    function GainLossReportModule() {
    }
    GainLossReportModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _gain_loss_report_component__WEBPACK_IMPORTED_MODULE_4__["GainLossReportComponent"],
                _multi_select_list_multi_select_list_component__WEBPACK_IMPORTED_MODULE_3__["MultiSelectListComponent"]
            ],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _gain_loss_report_routing_module__WEBPACK_IMPORTED_MODULE_2__["GainLossReportRoutingModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_20__["FormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_20__["ReactiveFormsModule"],
                src_app_shared_pipes_pipe_export_pipe_export_module__WEBPACK_IMPORTED_MODULE_21__["PipeExportModule"],
                // Custom Modules
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_17__["LibraryModule"],
                // material
                _angular_material_input__WEBPACK_IMPORTED_MODULE_5__["MatInputModule"],
                _angular_material_button__WEBPACK_IMPORTED_MODULE_6__["MatButtonModule"],
                _angular_material_card__WEBPACK_IMPORTED_MODULE_7__["MatCardModule"],
                _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_18__["MatCheckboxModule"],
                _angular_material_paginator__WEBPACK_IMPORTED_MODULE_8__["MatPaginatorModule"],
                _angular_material_dialog__WEBPACK_IMPORTED_MODULE_9__["MatDialogModule"],
                _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_10__["MatDatepickerModule"],
                _angular_material_core__WEBPACK_IMPORTED_MODULE_11__["MatNativeDateModule"],
                _angular_material_icon__WEBPACK_IMPORTED_MODULE_12__["MatIconModule"],
                _angular_material_select__WEBPACK_IMPORTED_MODULE_13__["MatSelectModule"],
                ngx_mat_select_search__WEBPACK_IMPORTED_MODULE_15__["NgxMatSelectSearchModule"],
                _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_14__["MatProgressSpinnerModule"],
                primeng_primeng__WEBPACK_IMPORTED_MODULE_16__["TooltipModule"]
            ],
            // exports: [
            //   CurrencyCodePipe
            // ],
            entryComponents: [
                _multi_select_list_multi_select_list_component__WEBPACK_IMPORTED_MODULE_3__["MultiSelectListComponent"]
            ],
            providers: [
                // CurrencyCodePipe,
                _gain_loss_report_service__WEBPACK_IMPORTED_MODULE_19__["GainLossReportService"]
            ]
        })
    ], GainLossReportModule);
    return GainLossReportModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.service.ts":
/*!***********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/gain-loss-report/gain-loss-report.service.ts ***!
  \***********************************************************************************/
/*! exports provided: GainLossReportService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GainLossReportService", function() { return GainLossReportService; });
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





var GainLossReportService = /** @class */ (function () {
    //#endregion
    function GainLossReportService(globalService, appurl) {
        this.globalService = globalService;
        this.appurl = appurl;
    }
    //#region "GetGainLossReportList"
    GainLossReportService.prototype.GetGainLossReportList = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GainLossReport_GetExchangeGainLossReport, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ExchangeGainLossReportList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetInputLevelAccountList"
    GainLossReportService.prototype.GetInputLevelAccountList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Account_GetAllInputLevelAccountCode)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.AccountDetailList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetCurrencyList"
    GainLossReportService.prototype.GetCurrencyList = function () {
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
    //#region "GetOfficeList"
    GainLossReportService.prototype.GetOfficeList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllOffice)
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
    //#region "GetJournalList"
    GainLossReportService.prototype.GetJournalList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Code_GetAllJournalDetail)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.JournalDetailList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetProjectList"
    GainLossReportService.prototype.GetProjectList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetAllProjectList)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectDetailModel,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "deleteAccountFromFilter"
    GainLossReportService.prototype.deleteAccountFromFilter = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetAllProjectList)
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
    //#region "GetGainLossReportList"
    GainLossReportService.prototype.SaveGainLossAccountList = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_FinancialReport_SaveGainLossAccountList, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.ExchangeGainLossReportList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetJournalList"
    GainLossReportService.prototype.GetExchangeGainLossFilterAccountList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GainLossReport_GetExchangeGainLossFilterAccountList)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.GainLossSelectedAccounts,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "SaveExchangeGainLossFilterAccountList"
    GainLossReportService.prototype.SaveExchangeGainLossFilterAccountList = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_FinancialReport_SaveGainLossAccountList, model)
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
    //#region "GetVoucherTypeList"
    GainLossReportService.prototype.GetVoucherTypeList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Account_GetAllVoucherType)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.VoucherTypeList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "AddGainLossVoucher"
    GainLossReportService.prototype.AddGainLossVoucher = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GainLossReport_AddExchangeGainLossVoucher, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.GainLossVoucherDetail,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetGainLossVoucherList"
    GainLossReportService.prototype.GetGainLossVoucherList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GainLossReport_GetExchangeGainLossVoucherList)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.GainLossVoucherList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "DeleteGainLossVoucher"
    GainLossReportService.prototype.DeleteGainLossVoucher = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GainLossReport_DeleteGainLossVoucherTransaction, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: null,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    GainLossReportService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"]])
    ], GainLossReportService);
    return GainLossReportService;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.html":
/*!**********************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.html ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>\r\n    <span *ngIf=\"data.showHideDropdown === office\">Select Office</span>\r\n    <span *ngIf=\"data.showHideDropdown === journal\">Select Journal</span>\r\n    <span *ngIf=\"data.showHideDropdown === project\">Select Project</span>\r\n\r\n    <!-- <button mat-icon-button [mat-dialog-close]=\"data.data\" class=\"pull-right\">\r\n      <mat-icon aria-label=\"cancel\">clear</mat-icon>\r\n    </button> -->\r\n  </h1>\r\n  <div mat-dialog-content>\r\n    <!-- <div class=\"row\"> -->\r\n    <div class=\"col-sm-12\">\r\n      <div class=\"main-multi-select-list\">\r\n        <div *ngIf=\"data.showHideDropdown === office\">\r\n          <ul *ngFor=\"let item of data.data\" class=\"padding_left_0\">\r\n            <mat-checkbox\r\n              (change)=\"onOfficeItemClick(item)\"\r\n              [(ngModel)]=\"item.IsChecked\"\r\n              >{{ item.OfficeName }}</mat-checkbox\r\n            >\r\n          </ul>\r\n        </div>\r\n        <div *ngIf=\"data.showHideDropdown === journal\">\r\n          <ul *ngFor=\"let item of data.data\" class=\"padding_left_0\">\r\n            <mat-checkbox\r\n              (change)=\"onJournalItemClick(item)\"\r\n              [(ngModel)]=\"item.IsChecked\"\r\n              >{{ item.JournalName }}</mat-checkbox\r\n            >\r\n          </ul>\r\n        </div>\r\n        <div *ngIf=\"data.showHideDropdown === project\">\r\n          <ul *ngFor=\"let item of data.data\" class=\"padding_left_0\">\r\n            <mat-checkbox\r\n              (change)=\"onProjectItemClick(item)\"\r\n              [(ngModel)]=\"item.IsChecked\"\r\n              >{{ item.ProjectName }}</mat-checkbox\r\n            >\r\n          </ul>\r\n        </div>\r\n      </div>\r\n    </div>\r\n    <!-- </div> -->\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.scss":
/*!**********************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.scss ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2dhaW4tbG9zcy1yZXBvcnQvbXVsdGktc2VsZWN0LWxpc3QvbXVsdGktc2VsZWN0LWxpc3QuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.ts":
/*!********************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.ts ***!
  \********************************************************************************************************/
/*! exports provided: MultiSelectListComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "MultiSelectListComponent", function() { return MultiSelectListComponent; });
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
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



var MultiSelectListComponent = /** @class */ (function () {
    function MultiSelectListComponent(dialogRef, data) {
        this.dialogRef = dialogRef;
        this.data = data;
        this.officeItemAddRemove = new _angular_core__WEBPACK_IMPORTED_MODULE_2__["EventEmitter"]();
        this.journalItemAddRemove = new _angular_core__WEBPACK_IMPORTED_MODULE_2__["EventEmitter"]();
        this.projectItemAddRemove = new _angular_core__WEBPACK_IMPORTED_MODULE_2__["EventEmitter"]();
        this.office = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_0__["ShowHideDropdownEnum"].Office;
        this.journal = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_0__["ShowHideDropdownEnum"].Journal;
        this.project = src_app_shared_enum__WEBPACK_IMPORTED_MODULE_0__["ShowHideDropdownEnum"].Project;
    }
    MultiSelectListComponent.prototype.ngOnInit = function () {
    };
    //#region "onOfficeItemClick"
    MultiSelectListComponent.prototype.onOfficeItemClick = function (e) {
        this.officeItemAddRemove.emit(e);
    };
    //#endregion
    //#region "onJournalItemClick"
    MultiSelectListComponent.prototype.onJournalItemClick = function (e) {
        this.journalItemAddRemove.emit(e);
    };
    //#endregion
    //#region "onProjectItemClick"
    MultiSelectListComponent.prototype.onProjectItemClick = function (e) {
        this.projectItemAddRemove.emit(e);
    };
    MultiSelectListComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"])({
            selector: 'app-multi-select-list',
            template: __webpack_require__(/*! ./multi-select-list.component.html */ "./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.html"),
            styles: [__webpack_require__(/*! ./multi-select-list.component.scss */ "./src/app/dashboard/accounting/gain-loss-report/multi-select-list/multi-select-list.component.scss")]
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"], Object])
    ], MultiSelectListComponent);
    return MultiSelectListComponent;
}());



/***/ })

}]);
//# sourceMappingURL=gain-loss-report-gain-loss-report-module.js.map