(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["accounting-accounting-module"],{

/***/ "./node_modules/rxjs-compat/_esm5/observable/forkJoin.js":
/*!***************************************************************!*\
  !*** ./node_modules/rxjs-compat/_esm5/observable/forkJoin.js ***!
  \***************************************************************/
/*! exports provided: forkJoin */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "forkJoin", function() { return rxjs__WEBPACK_IMPORTED_MODULE_0__["forkJoin"]; });


//# sourceMappingURL=forkJoin.js.map

/***/ }),

/***/ "./src/app/dashboard/accounting/accounting-routing.module.ts":
/*!*******************************************************************!*\
  !*** ./src/app/dashboard/accounting/accounting-routing.module.ts ***!
  \*******************************************************************/
/*! exports provided: AccountingRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AccountingRoutingModule", function() { return AccountingRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _accounting_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./accounting.component */ "./src/app/dashboard/accounting/accounting.component.ts");
/* harmony import */ var src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/services/role-guard */ "./src/app/shared/services/role-guard.ts");
/* harmony import */ var _journal_report_journal_report_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./journal-report/journal-report.component */ "./src/app/dashboard/accounting/journal-report/journal-report.component.ts");
/* harmony import */ var _ledger_statement_report_ledger_statement_report_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./ledger-statement-report/ledger-statement-report.component */ "./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.ts");
/* harmony import */ var _trial_balance_report_trial_balance_report_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./trial-balance-report/trial-balance-report.component */ "./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.ts");
/* harmony import */ var _exchange_gain_loss_report_exchange_gain_loss_report_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./exchange-gain-loss-report/exchange-gain-loss-report.component */ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};









var ModuleId = src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationModule"].AccountingNew;
var routes = [
    {
        path: '',
        component: _accounting_component__WEBPACK_IMPORTED_MODULE_2__["AccountingComponent"],
        children: [
            {
                path: 'chart-of-accounts',
                loadChildren: './chart-of-accounts/chart-of-accounts.module#ChartOfAccountsModule',
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    isModule: true
                }
            },
            {
                path: 'financial-report',
                loadChildren: './financial-report/financial-report.module#FinancialReportModule',
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    isModule: true
                }
            },
            {
                path: 'vouchers',
                loadChildren: './vouchers/vouchers.module#VouchersModule',
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    page: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["accountingNewMaster"].Vouchers
                }
            },
            {
                path: 'gain-loss-report',
                loadChildren: './gain-loss-report/gain-loss-report.module#GainLossReportModule',
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    page: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["accountingNewMaster"].ExchangeGainLoss
                }
            },
            {
                path: 'exchange-rate',
                loadChildren: './exchange-rate/exchange-rate.module#ExchangeRateModule',
                canActivate: [src_app_shared_services_role_guard__WEBPACK_IMPORTED_MODULE_4__["RoleGuardService"]],
                data: {
                    module: ModuleId,
                    page: src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["accountingNewMaster"].ExchangeRates
                }
            },
            {
                path: 'journal-report',
                component: _journal_report_journal_report_component__WEBPACK_IMPORTED_MODULE_5__["JournalReportComponent"]
            },
            {
                path: 'ledger-report',
                component: _ledger_statement_report_ledger_statement_report_component__WEBPACK_IMPORTED_MODULE_6__["LedgerStatementReportComponent"]
            },
            {
                path: 'trial-balance',
                component: _trial_balance_report_trial_balance_report_component__WEBPACK_IMPORTED_MODULE_7__["TrialBalanceReportComponent"]
            },
            {
                path: 'exchange-gain-loss-report',
                component: _exchange_gain_loss_report_exchange_gain_loss_report_component__WEBPACK_IMPORTED_MODULE_8__["ExchangeGainLossReportComponent"]
            },
        ]
    }
];
var AccountingRoutingModule = /** @class */ (function () {
    function AccountingRoutingModule() {
    }
    AccountingRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]] // important to export
        })
    ], AccountingRoutingModule);
    return AccountingRoutingModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/accounting.component.html":
/*!****************************************************************!*\
  !*** ./src/app/dashboard/accounting/accounting.component.html ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"accounting-main\">\r\n  <div class=\"main_body\">\r\n  <router-outlet></router-outlet>\r\n\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/accounting.component.scss":
/*!****************************************************************!*\
  !*** ./src/app/dashboard/accounting/accounting.component.scss ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2FjY291bnRpbmcuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/accounting.component.ts":
/*!**************************************************************!*\
  !*** ./src/app/dashboard/accounting/accounting.component.ts ***!
  \**************************************************************/
/*! exports provided: AccountingComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AccountingComponent", function() { return AccountingComponent; });
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

var AccountingComponent = /** @class */ (function () {
    function AccountingComponent() {
    }
    AccountingComponent.prototype.ngOnInit = function () {
    };
    AccountingComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-accounting',
            template: __webpack_require__(/*! ./accounting.component.html */ "./src/app/dashboard/accounting/accounting.component.html"),
            styles: [__webpack_require__(/*! ./accounting.component.scss */ "./src/app/dashboard/accounting/accounting.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], AccountingComponent);
    return AccountingComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/accounting.module.ts":
/*!***********************************************************!*\
  !*** ./src/app/dashboard/accounting/accounting.module.ts ***!
  \***********************************************************/
/*! exports provided: AccountingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AccountingModule", function() { return AccountingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _accounting_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./accounting.component */ "./src/app/dashboard/accounting/accounting.component.ts");
/* harmony import */ var _accounting_routing_module__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./accounting-routing.module */ "./src/app/dashboard/accounting/accounting-routing.module.ts");
/* harmony import */ var _vouchers_voucher_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./vouchers/voucher.service */ "./src/app/dashboard/accounting/vouchers/voucher.service.ts");
/* harmony import */ var _journal_report_journal_report_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./journal-report/journal-report.component */ "./src/app/dashboard/accounting/journal-report/journal-report.component.ts");
/* harmony import */ var _ledger_statement_report_ledger_statement_report_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./ledger-statement-report/ledger-statement-report.component */ "./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.ts");
/* harmony import */ var _trial_balance_report_trial_balance_report_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./trial-balance-report/trial-balance-report.component */ "./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! projects/library/src/public_api */ "./projects/library/src/public_api.ts");
/* harmony import */ var saturn_datepicker__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! saturn-datepicker */ "./node_modules/saturn-datepicker/fesm5/saturn-datepicker.js");
/* harmony import */ var _exchange_gain_loss_report_exchange_gain_loss_report_component__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./exchange-gain-loss-report/exchange-gain-loss-report.component */ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.ts");
/* harmony import */ var _exchange_gain_loss_report_configuration_filter_configuration_filter_component__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./exchange-gain-loss-report/configuration-filter/configuration-filter.component */ "./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.ts");
/* harmony import */ var _exchange_gain_loss_report_consolidate_gain_loss_consolidate_gain_loss_component__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component */ "./src/app/dashboard/accounting/exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component.ts");
/* harmony import */ var _exchange_gain_loss_report_exchange_gain_loss_report_service__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./exchange-gain-loss-report/exchange-gain-loss-report.service */ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
















var AccountingModule = /** @class */ (function () {
    function AccountingModule() {
    }
    AccountingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatInputModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatSelectModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatButtonModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatDatepickerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatDividerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatIconModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatPaginatorModule"],
                saturn_datepicker__WEBPACK_IMPORTED_MODULE_11__["SatDatepickerModule"],
                saturn_datepicker__WEBPACK_IMPORTED_MODULE_11__["SatNativeDateModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__["LibraryModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__["SubHeaderTemplateModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatCardModule"],
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_9__["ReactiveFormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_9__["FormsModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatTableModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatTabsModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatCheckboxModule"],
                _accounting_routing_module__WEBPACK_IMPORTED_MODULE_3__["AccountingRoutingModule"] // Routing
            ],
            declarations: [
                _accounting_component__WEBPACK_IMPORTED_MODULE_2__["AccountingComponent"],
                _journal_report_journal_report_component__WEBPACK_IMPORTED_MODULE_5__["JournalReportComponent"],
                _ledger_statement_report_ledger_statement_report_component__WEBPACK_IMPORTED_MODULE_6__["LedgerStatementReportComponent"],
                _trial_balance_report_trial_balance_report_component__WEBPACK_IMPORTED_MODULE_7__["TrialBalanceReportComponent"],
                _exchange_gain_loss_report_exchange_gain_loss_report_component__WEBPACK_IMPORTED_MODULE_12__["ExchangeGainLossReportComponent"],
                _exchange_gain_loss_report_configuration_filter_configuration_filter_component__WEBPACK_IMPORTED_MODULE_13__["ConfigurationFilterComponent"],
                _exchange_gain_loss_report_consolidate_gain_loss_consolidate_gain_loss_component__WEBPACK_IMPORTED_MODULE_14__["ConsolidateGainLossComponent"],
            ],
            providers: [
                _vouchers_voucher_service__WEBPACK_IMPORTED_MODULE_4__["VoucherService"],
                _exchange_gain_loss_report_exchange_gain_loss_report_service__WEBPACK_IMPORTED_MODULE_15__["ExchangeGainLossReportService"]
            ],
            exports: [],
            entryComponents: []
        })
    ], AccountingModule);
    return AccountingModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.html":
/*!*************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.html ***!
  \*************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<hum-config-card [showCard]=\"showConfig\" (cardState)=\"getState($event)\">\r\n  <div title>Calculator Configuration</div>\r\n  <mat-divider></mat-divider>\r\n  <div content class=\"padding_top_30\">\r\n    <form [formGroup] = \"gainLossConfigForm\" (ngSubmit)=\"saveCalculatorConfigData()\">\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-12\">\r\n          <b>Consolidation Currency</b>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <lib-hum-dropdown [validation]=\"gainLossConfigForm.controls['CurrencyId'].hasError('required')\"\r\n            [options]=\"currency$\" formControlName='CurrencyId' [placeHolder]=\"'Currency'\"></lib-hum-dropdown>\r\n        </div>\r\n        <br>\r\n        <div class=\"col-sm-12\">\r\n          <b>Accounting Period</b>\r\n          <hum-button [type]=\"'add'\" [text]=\"'USE DEFAULT'\" (click)=\"useDefault($event, 'period')\"></hum-button>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field>\r\n            <input matInput [matDatepicker]=\"StartDate\" formControlName='StartDate' placeholder=\"Start Date\">\r\n            <mat-datepicker-toggle matSuffix [for]=\"StartDate\"></mat-datepicker-toggle>\r\n            <mat-datepicker #StartDate></mat-datepicker>\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field>\r\n            <input matInput [matDatepicker]=\"EndDate\" formControlName='EndDate' placeholder=\"End Date\">\r\n            <mat-datepicker-toggle matSuffix [for]=\"EndDate\"></mat-datepicker-toggle>\r\n            <mat-datepicker #EndDate></mat-datepicker>\r\n          </mat-form-field>\r\n        </div>\r\n        <br>\r\n        <div class=\"col-sm-12\">\r\n          <b>Comparision Date</b>\r\n          <hum-button [type]=\"'add'\" [text]=\"'USE DEFAULT'\" (click)=\"useDefault($event, 'comparisionDate')\"></hum-button>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field>\r\n            <input matInput [matDatepicker]=\"ComparisionDate\" formControlName='ComparisionDate'\r\n              placeholder=\"Comparision Date\">\r\n            <mat-datepicker-toggle matSuffix [for]=\"ComparisionDate\"></mat-datepicker-toggle>\r\n            <mat-datepicker #ComparisionDate></mat-datepicker>\r\n          </mat-form-field>\r\n        </div>\r\n        <br>\r\n        <div class=\"col-sm-12\">\r\n          <b>NET Gain/Loss Accounts</b>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <lib-hum-dropdown [validation]=\"gainLossConfigForm.controls['DebitAccount'].hasError('required')\"\r\n            [options]=\"accounts$\" formControlName='DebitAccount' [placeHolder]=\"'Credit Account for NET Gains'\"\r\n            (change)=\"getCreditAccountSelectedValue($event)\">\r\n          </lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <lib-hum-dropdown [validation]=\"gainLossConfigForm.controls['CreditAccount'].hasError('required')\"\r\n            [options]=\"accounts$\" formControlName='CreditAccount' [placeHolder]=\"'Debit Account for NET Losses'\"\r\n            (change)=\"getDebitAccountSelectedValue($event)\">\r\n          </lib-hum-dropdown>\r\n        </div>\r\n        <br>\r\n        <div class=\"col-sm-12\">\r\n          <hum-button [disabled]=\"!gainLossConfigForm.valid\" [isSubmit]=\"true\" [type]=\"'save'\" [text]=\"'save'\">\r\n          </hum-button>\r\n        </div>\r\n      </div>\r\n    </form>\r\n  </div>\r\n  <div footer>\r\n  </div>\r\n</hum-config-card>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.scss":
/*!*************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.scss ***!
  \*************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2V4Y2hhbmdlLWdhaW4tbG9zcy1yZXBvcnQvY29uZmlndXJhdGlvbi1maWx0ZXIvY29uZmlndXJhdGlvbi1maWx0ZXIuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.ts":
/*!***********************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.ts ***!
  \***********************************************************************************************************************/
/*! exports provided: ConfigurationFilterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ConfigurationFilterComponent", function() { return ConfigurationFilterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_store_services_field_config_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/store/services/field-config.service */ "./src/app/store/services/field-config.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var rxjs_observable_forkJoin__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/observable/forkJoin */ "./node_modules/rxjs-compat/_esm5/observable/forkJoin.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _exchange_gain_loss_report_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ../exchange-gain-loss-report.service */ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.service.ts");
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










var ConfigurationFilterComponent = /** @class */ (function () {
    function ConfigurationFilterComponent(eRef, toastr, fieldConfig, fb, gainLossService) {
        this.eRef = eRef;
        this.toastr = toastr;
        this.fieldConfig = fieldConfig;
        this.fb = fb;
        this.gainLossService = gainLossService;
        this.showConfig = false;
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_7__["ReplaySubject"](1);
        this.configData = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    ConfigurationFilterComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.onInItForm();
        Object(rxjs_observable_forkJoin__WEBPACK_IMPORTED_MODULE_5__["forkJoin"])([
            this.getAllCurrency(),
            this.getAllAccounts(),
            this.getDefaultAccountingPeriod()
        ])
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeAllCurrency(result[0]);
            _this.subscribeAllInputAccounts(result[1]);
            _this.subscribeDefaultAccountingPeriod(result[2]);
        });
        this.defaultFinancialYearDate = {
            startDate: null,
            endDate: null
        };
        this.getData();
    };
    ConfigurationFilterComponent.prototype.onInItForm = function () {
        this.gainLossConfigForm = this.fb.group({
            'CurrencyId': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'StartDate': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'EndDate': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'ComparisionDate': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'DebitAccount': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'CreditAccount': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'DebitAccountName': [null],
            'CreditAccountName': [null]
        });
    };
    //#region "Dynamic Scroll"
    ConfigurationFilterComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 170 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    ConfigurationFilterComponent.prototype.getAllCurrency = function () {
        return this.gainLossService.GetCurrencyList();
    };
    ConfigurationFilterComponent.prototype.getAllAccounts = function () {
        return this.gainLossService.GetInputLevelAccountList();
    };
    ConfigurationFilterComponent.prototype.subscribeAllCurrency = function (response) {
        this.currency$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_4__["of"])(response.data.map(function (y) {
            return {
                value: y.CurrencyId,
                name: y.CurrencyCode + '-' + y.CurrencyName
            };
        }));
    };
    ConfigurationFilterComponent.prototype.subscribeAllInputAccounts = function (response) {
        this.accounts$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_4__["of"])(response.data.map(function (y) {
            return {
                value: y.AccountCode,
                name: y.AccountName
            };
        }));
        this.accountsList = response.data;
    };
    ConfigurationFilterComponent.prototype.saveCalculatorConfigData = function () {
        var _this = this;
        if (!this.gainLossConfigForm.valid) {
            this.toastr.warning('Please correct form errors and submit again');
            return;
        }
        this.gainLossConfigForm.value.ComparisionDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].setLocalDate(this.gainLossConfigForm.value.ComparisionDate);
        this.gainLossConfigForm.value.StartDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].setLocalDate(this.gainLossConfigForm.value.StartDate);
        this.gainLossConfigForm.value.EndDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].setLocalDate(this.gainLossConfigForm.value.EndDate);
        this.gainLossService.SaveCalculatorConfigData(this.gainLossConfigForm.value)
            .subscribe(function (x) {
            if (x) {
                _this.toastr.success('Configuration Updated');
                _this.configDataEmit();
            }
        }, function (error) {
            _this.toastr.error('Failed, Please try again');
        });
    };
    ConfigurationFilterComponent.prototype.getData = function () {
        this.getConfigurationFilter();
        this.getDefaultAccountingPeriod();
    };
    ConfigurationFilterComponent.prototype.getConfigurationFilter = function () {
        var _this = this;
        this.gainLossService.GetGainLossCaculatorConfiguration().subscribe(function (x) {
            if (x.CalculatorConfiguration) {
                _this.onInItForm();
                _this.gainLossConfigForm.patchValue({
                    'CurrencyId': x.CalculatorConfiguration.CurrencyId,
                    'StartDate': x.CalculatorConfiguration.StartDate,
                    'EndDate': x.CalculatorConfiguration.EndDate,
                    'ComparisionDate': x.CalculatorConfiguration.ComparisionDate,
                    'DebitAccount': x.CalculatorConfiguration.DebitAccount,
                    'CreditAccount': x.CalculatorConfiguration.CreditAccount,
                    'CreditAccountName': x.CalculatorConfiguration.CreditAccountName,
                    'DebitAccountName': x.CalculatorConfiguration.DebitAccountName,
                });
                _this.configDataEmit();
            }
        }, function (error) {
            _this.toastr.error('Failed, Please try again');
        });
    };
    ConfigurationFilterComponent.prototype.getDefaultAccountingPeriod = function () {
        return this.gainLossService.GetDefaultAccountingPeriod();
    };
    ConfigurationFilterComponent.prototype.show = function () {
        this.showConfig = true;
        this.getData();
    };
    ConfigurationFilterComponent.prototype.getState = function (e) {
        this.showConfig = e;
    };
    ConfigurationFilterComponent.prototype.useDefault = function (event, type) {
        if (type === 'period') {
            if (this.defaultFinancialYearDate.startDate && this.defaultFinancialYearDate.endDate) {
                this.gainLossConfigForm.patchValue({
                    'StartDate': this.defaultFinancialYearDate.startDate,
                    'EndDate': this.defaultFinancialYearDate.endDate
                });
            }
            else {
                this.toastr.warning('no default Accounting Period set');
            }
        }
        else if (type === 'comparisionDate') {
            if (this.defaultFinancialYearDate.endDate) {
                this.gainLossConfigForm.patchValue({
                    'ComparisionDate': src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].getLocalDate(this.defaultFinancialYearDate.endDate),
                });
            }
        }
    };
    ConfigurationFilterComponent.prototype.subscribeDefaultAccountingPeriod = function (response) {
        if (response.AccountingPeriod) {
            this.defaultFinancialYearDate = {
                startDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].getLocalDate(response.AccountingPeriod.StartDate),
                endDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_9__["StaticUtilities"].getLocalDate(response.AccountingPeriod.EndDate)
            };
        }
    };
    ConfigurationFilterComponent.prototype.getCreditAccountSelectedValue = function (value) {
        var index = this.accountsList.findIndex(function (x) { return x.AccountCode === value; });
        if (index !== -1) {
            this.gainLossConfigForm.controls['CreditAccountName'].patchValue(this.accountsList[index].AccountName);
        }
    };
    ConfigurationFilterComponent.prototype.getDebitAccountSelectedValue = function (value) {
        var index = this.accountsList.findIndex(function (x) { return x.AccountCode === value; });
        if (index !== -1) {
            this.gainLossConfigForm.controls['DebitAccountName'].patchValue(this.accountsList[index].AccountName);
        }
    };
    //#region "onListRefresh"
    ConfigurationFilterComponent.prototype.configDataEmit = function () {
        this.configData.emit(this.gainLossConfigForm.value);
    };
    //#endregion
    ConfigurationFilterComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], ConfigurationFilterComponent.prototype, "configData", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], ConfigurationFilterComponent.prototype, "getScreenSize", null);
    ConfigurationFilterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-configuration-filter',
            template: __webpack_require__(/*! ./configuration-filter.component.html */ "./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.html"),
            styles: [__webpack_require__(/*! ./configuration-filter.component.scss */ "./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"], ngx_toastr__WEBPACK_IMPORTED_MODULE_1__["ToastrService"],
            src_app_store_services_field_config_service__WEBPACK_IMPORTED_MODULE_2__["FieldConfigService"], _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormBuilder"], _exchange_gain_loss_report_service__WEBPACK_IMPORTED_MODULE_8__["ExchangeGainLossReportService"]])
    ], ConfigurationFilterComponent);
    return ConfigurationFilterComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component.html":
/*!***************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component.html ***!
  \***************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">Exchange Gain/Loss Consolidation\r\n    <hum-button [type]=\"'save'\" [text]=\"'Commit'\" (click)=\"onCommitConsolidate()\"></hum-button>\r\n    <hum-button [type]=\"'cancel'\" [text]=\"'Cancel'\" (click)=\"cancel()\"></hum-button>\r\n  </span>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n<div class=\"row\">\r\n  <div class=\"col-md-12\">\r\n    <mat-tab-group (selectedTabChange)=\"tabChanged($event)\">\r\n      <mat-tab label=\"GAINS C/L VOUCHER\">\r\n        <ng-template matTabContent>\r\n          <ng-container *ngTemplateOutlet=\"gainloass\"></ng-container>\r\n        </ng-template>\r\n      </mat-tab>\r\n      <mat-tab label=\"LOSSES C/L VOUCHER\">\r\n        <ng-template matTabContent>\r\n          <ng-container *ngTemplateOutlet=\"gainloass\"></ng-container>\r\n        </ng-template>\r\n      </mat-tab>\r\n    </mat-tab-group>\r\n  </div>\r\n</div>\r\n\r\n<ng-template #gainloass>\r\n  <mat-card humAddScroll [height]=\"250\">\r\n    <div class=\"row\">\r\n      <form [formGroup]=\"voucherDataForm\">\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field>\r\n            <textarea mat-autosize formControlName='Description' matInput placeholder=\"Voucher Description\"></textarea>\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <lib-hum-dropdown [validation]=\"voucherDataForm.controls['JournalId'].hasError('required')\"\r\n            [options]=\"journalList$\" formControlName='JournalId' [placeHolder]=\"'Voucher Journal'\"\r\n            ></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <lib-hum-dropdown [validation]=\"voucherDataForm.controls['VoucherType'].hasError('required')\"\r\n          [options]=\"voucherTypeList$\" formControlName='VoucherType' [placeHolder]=\"'VoucherType'\"\r\n          ></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <lib-hum-dropdown [validation]=\"voucherDataForm.controls['OfficeId'].hasError('required')\"\r\n          [options]=\"officeList$\" formControlName='OfficeId' [placeHolder]=\"'Office'\"\r\n          ></lib-hum-dropdown>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <div subtitle class=\"info_text\"><i class=\"fas fa-info-circle icon_color_yellow\"></i>\r\n            Voucher Date is set to End Date of the Active Financial Period</div>\r\n          <div subtitle class=\"info_text\"><i class=\"fas fa-info-circle icon_color_yellow\"></i>\r\n            Voucher Currency is set to the Comparision Currency configured for the Exchange Gain/Loss Calculator\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm-12 transcation_container\" >\r\n          <div title> Transcations</div>\r\n          <hum-table [hideColums$]=\"hideColums\" [headers]=\"transactionHeaders$\" [items]=\"transactionList$\"></hum-table>\r\n        </div>\r\n      </form>\r\n    </div>\r\n  </mat-card>\r\n</ng-template>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component.scss":
/*!***************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component.scss ***!
  \***************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".icon_color_yellow {\n  color: darkgoldenrod; }\n\n.info_text {\n  font-size: 10px;\n  color: gray; }\n\n.transcation_container {\n  margin-top: 20px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvZXhjaGFuZ2UtZ2Fpbi1sb3NzLXJlcG9ydC9jb25zb2xpZGF0ZS1nYWluLWxvc3MvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcZGFzaGJvYXJkXFxhY2NvdW50aW5nXFxleGNoYW5nZS1nYWluLWxvc3MtcmVwb3J0XFxjb25zb2xpZGF0ZS1nYWluLWxvc3NcXGNvbnNvbGlkYXRlLWdhaW4tbG9zcy5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNJLG9CQUFvQixFQUFBOztBQUd4QjtFQUNJLGVBQWU7RUFDZixXQUFXLEVBQUE7O0FBRWY7RUFDSSxnQkFBZ0IsRUFBQSIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2V4Y2hhbmdlLWdhaW4tbG9zcy1yZXBvcnQvY29uc29saWRhdGUtZ2Fpbi1sb3NzL2NvbnNvbGlkYXRlLWdhaW4tbG9zcy5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5pY29uX2NvbG9yX3llbGxvd3tcclxuICAgIGNvbG9yOiBkYXJrZ29sZGVucm9kO1xyXG4gIH1cclxuXHJcbi5pbmZvX3RleHR7XHJcbiAgICBmb250LXNpemU6IDEwcHg7XHJcbiAgICBjb2xvcjogZ3JheTtcclxufVxyXG4udHJhbnNjYXRpb25fY29udGFpbmVye1xyXG4gICAgbWFyZ2luLXRvcDogMjBweDtcclxufVxyXG4iXX0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component.ts":
/*!*************************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component.ts ***!
  \*************************************************************************************************************************/
/*! exports provided: ConsolidateGainLossComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ConsolidateGainLossComponent", function() { return ConsolidateGainLossComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _exchange_gain_loss_report_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../exchange-gain-loss-report.service */ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.service.ts");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var ConsolidateGainLossComponent = /** @class */ (function () {
    function ConsolidateGainLossComponent(fb, gainLossReportService, toastr, commonLoader) {
        this.fb = fb;
        this.gainLossReportService = gainLossReportService;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.emitType = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.gainList = [];
        this.lossList = [];
        this.tabIndex = 0;
        this.accountIds = [];
        this.hideColums = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])({
            headers: ['Account', 'Credit Amount', 'Debit Amount', 'Description'],
            items: ['Account', 'CreditAmount', 'DebitAmount', 'Description']
        });
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_1__["ReplaySubject"](1);
        this.transactionHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(['Account', 'Credit Amount', 'Debit Amount', 'Description']);
    }
    ConsolidateGainLossComponent.prototype.ngOnChanges = function () {
        var _this = this;
        this.gainList = this.selectedData.filter(function (x) { return x.GainLossStatus === 1; });
        this.lossList = this.selectedData.filter(function (x) { return x.GainLossStatus === -1; });
        this.getTotalGain();
        this.selectedData.forEach(function (x) {
            _this.accountIds.push(x.AccountId);
        });
    };
    ConsolidateGainLossComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.inItForm();
        Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["forkJoin"])([
            this.getJournalList(),
            this.getVoucherTypeList(),
            this.getOfficeList()
        ])
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_4__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeJournalList(result[0]);
            _this.subscribeVoucherTypeList(result[1]);
            _this.subscribeOfficeList(result[2]);
        });
    };
    ConsolidateGainLossComponent.prototype.inItForm = function () {
        this.voucherDataForm = this.fb.group({
            'JournalId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            'VoucherType': [null, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required],
            'Description': [null],
            'OfficeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]]
        });
    };
    ConsolidateGainLossComponent.prototype.tabChanged = function (value) {
        this.tabIndex = value.index;
        if (value.index === 0) {
            this.getTotalGain();
        }
        else if (value.index === 1) {
            this.getTotalLoss();
        }
    };
    ConsolidateGainLossComponent.prototype.cancel = function () {
        this.emitType.emit('');
    };
    //#region "getJournalList"
    ConsolidateGainLossComponent.prototype.getJournalList = function () {
        return this.gainLossReportService.GetJournalList();
    };
    //#endregion
    ConsolidateGainLossComponent.prototype.subscribeJournalList = function (response) {
        if (response.statusCode === 200 && response.data !== null) {
            this.journalList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.data.map(function (y) {
                return {
                    name: y.JournalName,
                    value: y.JournalCode
                };
            }));
        }
    };
    //#region "getVoucherTypeList"
    ConsolidateGainLossComponent.prototype.getVoucherTypeList = function () {
        return this.gainLossReportService.GetVoucherTypeList();
    };
    //#endregion
    ConsolidateGainLossComponent.prototype.subscribeVoucherTypeList = function (response) {
        console.log(response.data, 'vouchertype');
        if (response.statusCode === 200 && response.data !== null) {
            this.voucherTypeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.data.map(function (y) {
                return {
                    name: y.VoucherTypeName,
                    value: y.VoucherTypeId
                };
            }));
        }
    };
    //#region "getOfficeList"
    ConsolidateGainLossComponent.prototype.getOfficeList = function () {
        return this.gainLossReportService.GetOfficeList();
    };
    //#endregion
    ConsolidateGainLossComponent.prototype.subscribeOfficeList = function (response) {
        console.log(response.data, 'office');
        if (response.statusCode === 200 && response.data !== null) {
            this.officeList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(response.data.map(function (y) {
                return {
                    name: y.OfficeName,
                    value: y.OfficeId
                };
            }));
        }
    };
    ConsolidateGainLossComponent.prototype.onCommitConsolidate = function () {
        var _this = this;
        if (!this.voucherDataForm.valid) {
            this.toastr.warning('Please correct errors on voucher form and submit again');
            return;
        }
        if (!this.calculatorConfigData.DebitAccount && !this.calculatorConfigData.CreditAmount) {
            this.toastr.warning('Please select Debit and Credit account on calculator config.');
            return;
        }
        this.commonLoader.showLoader();
        var transactionlist = [];
        this.transactionList$.subscribe(function (x) { return transactionlist = x; });
        transactionlist.splice((transactionlist.length - 1), 1);
        var model = {
            CurrencyId: this.calculatorConfigData.CurrencyId,
            JournalId: this.voucherDataForm.value.JournalId,
            VoucherType: this.voucherDataForm.value.VoucherType,
            OfficeId: this.voucherDataForm.value.OfficeId,
            TimeZoneOffset: new Date().getTimezoneOffset(),
            Description: this.voucherDataForm.value.Description,
            VoucherDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_7__["StaticUtilities"].setLocalDate(new Date()),
            TransactionList: transactionlist,
            StartDate: this.calculatorConfigData.StartDate,
            EndDate: this.calculatorConfigData.EndDate
        };
        this.gainLossReportService.AddGainLossVoucher(model).subscribe(function (response) {
            if (response.statusCode === 200 && response.data !== null) {
                _this.commonLoader.hideLoader();
                _this.toastr.success('Account Consolidated');
                _this.cancel();
                // this.toastr.success('Voucher Created Successfully');
            }
            else {
                _this.commonLoader.hideLoader();
                _this.toastr.error(response.message);
            }
            _this.commonLoader.hideLoader();
        }, function (error) {
            _this.commonLoader.hideLoader();
            _this.toastr.error(error);
        });
    };
    ConsolidateGainLossComponent.prototype.getTotalGain = function () {
        this.gainList = [];
        this.gainList = this.selectedData.filter(function (x) { return x.GainLossStatus === 1; });
        this.totals = this.gainList.reduce(function (a, _a) {
            var ResultingGainLoss = _a.ResultingGainLoss;
            return a + ResultingGainLoss;
        }, 0);
        if (this.gainList.length > 0) {
            this.gainList.push({
                AccountId: this.calculatorConfigData.CreditAccount,
                AccountCode: '',
                AccountName: this.calculatorConfigData.CreditAccountName,
                BalanceOnOriginalTransactionDates: 0,
                BalanceOnComparisionDate: 0,
                ResultingGainLoss: this.totals,
                GainLossStatus: 0
            });
            this.gainList.push({
                AccountId: 0,
                AccountCode: '0',
                AccountName: '<b>Totals</b>',
                BalanceOnOriginalTransactionDates: this.totals,
                BalanceOnComparisionDate: this.totals,
                ResultingGainLoss: this.totals,
                GainLossStatus: 0
            });
        }
        this.transactionList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(this.gainList.map(function (x) {
            return {
                AccountId: x.AccountId,
                Account: x.GainLossStatus !== 0 ? (x.AccountCode + '-' + x.AccountName) : x.AccountName,
                CreditAmount: (x.GainLossStatus === 0 || x.AccountId === 0) ? x.ResultingGainLoss : 0,
                DebitAmount: (x.GainLossStatus !== 0 || x.AccountId === 0) ? x.ResultingGainLoss : 0,
                Description: x.AccountId === 0 ? '' : 'Gain'
            };
        }));
    };
    ConsolidateGainLossComponent.prototype.getTotalLoss = function () {
        this.totals = this.lossList.reduce(function (a, _a) {
            var ResultingGainLoss = _a.ResultingGainLoss;
            return a + ResultingGainLoss;
        }, 0);
        this.lossList = [];
        this.lossList = this.selectedData.filter(function (x) { return x.GainLossStatus === -1; });
        this.totals = this.lossList.reduce(function (a, _a) {
            var ResultingGainLoss = _a.ResultingGainLoss;
            return a + ResultingGainLoss;
        }, 0);
        if (this.lossList.length > 0) {
            this.lossList.push({
                AccountId: this.calculatorConfigData.DebitAccount,
                AccountCode: '',
                AccountName: this.calculatorConfigData.DebitAccountName,
                BalanceOnOriginalTransactionDates: 0,
                BalanceOnComparisionDate: 0,
                ResultingGainLoss: this.totals,
                GainLossStatus: 0
            });
            this.lossList.push({
                AccountId: 0,
                AccountCode: '0',
                AccountName: '<b>Totals</b>',
                BalanceOnOriginalTransactionDates: this.totals,
                BalanceOnComparisionDate: this.totals,
                ResultingGainLoss: this.totals,
                GainLossStatus: 0
            });
        }
        this.transactionList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["of"])(this.lossList.map(function (x) {
            return {
                AccountId: x.AccountId,
                Account: x.GainLossStatus !== 0 ? (x.AccountCode + '-' + x.AccountName) : x.AccountName,
                CreditAmount: (x.GainLossStatus !== 0 || x.AccountId === 0) ? x.ResultingGainLoss : 0,
                DebitAmount: (x.GainLossStatus === 0 || x.AccountId === 0) ? x.ResultingGainLoss : 0,
                Description: x.AccountId === 0 ? '' : 'Loss'
            };
        }));
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], ConsolidateGainLossComponent.prototype, "emitType", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Array)
    ], ConsolidateGainLossComponent.prototype, "selectedData", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], ConsolidateGainLossComponent.prototype, "calculatorConfigData", void 0);
    ConsolidateGainLossComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-consolidate-gain-loss',
            template: __webpack_require__(/*! ./consolidate-gain-loss.component.html */ "./src/app/dashboard/accounting/exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component.html"),
            styles: [__webpack_require__(/*! ./consolidate-gain-loss.component.scss */ "./src/app/dashboard/accounting/exchange-gain-loss-report/consolidate-gain-loss/consolidate-gain-loss.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"], _exchange_gain_loss_report_service__WEBPACK_IMPORTED_MODULE_3__["ExchangeGainLossReportService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_5__["ToastrService"], src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__["CommonLoaderService"]])
    ], ConsolidateGainLossComponent);
    return ConsolidateGainLossComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.html":
/*!*********************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.html ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">Results\r\n    <hum-button [type]=\"'add'\" [text]=\"'CONSOLIDATE EXCHANGE GAIN/LOSS'\" *ngIf=\"type != 'consolidation'\"\r\n      (click)=\"onConsolidation()\"></hum-button>\r\n  </span>\r\n  <div class=\"action_section\">\r\n    <hum-button [type]=\"'filter'\" [text]=\"'TRANSACTION FILTERS'\"\r\n      (click)=\"type='filter'\"></hum-button>\r\n      <hum-button [type]=\"'filter'\" [text]=\"'CLEAR TRANSACTION FILTERS'\"\r\n      (click)=\"clearTransactionFilters()\"></hum-button>\r\n    <hum-button [type]=\"'filter'\" [text]=\"'CONFIGURATION'\" (click)=\"showConfiguration()\"></hum-button>\r\n  </div>\r\n</lib-sub-header-template>\r\n<mat-divider></mat-divider>\r\n\r\n<ng-container [ngSwitch]=\"type\">\r\n  <div class=\"row\" *ngSwitchDefault humAddScroll [height]=\"150\">\r\n    <div class=\"col-md-12\">\r\n\r\n      <mat-table #table [dataSource]=\"gainLossReportList\">\r\n\r\n        <!-- <ng-container matColumnDef=\"Checked\">\r\n          <mat-header-cell *matHeaderCellDef>Select</mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\">\r\n            <mat-checkbox [(ngModel)]=\"element.Checked\"></mat-checkbox>\r\n          </mat-cell>\r\n        </ng-container> -->\r\n        <!-- Checkbox Column -->\r\n        <ng-container matColumnDef=\"select\">\r\n          <th style=\"border-bottom-width:0px\" mat-header-cell *matHeaderCellDef>\r\n            <mat-checkbox (change)=\"$event ? masterToggle() : null\" [checked]=\"selection.hasValue() && isAllSelected()\"\r\n              [indeterminate]=\"selection.hasValue() && !isAllSelected()\" [aria-label]=\"checkboxLabel()\">\r\n            </mat-checkbox>\r\n          </th>\r\n          <td style=\"border-bottom-width:0px\" mat-cell *matCellDef=\"let row\">\r\n            <mat-checkbox (click)=\"$event.stopPropagation()\" (change)=\"$event ? selection.toggle(row) : null\"\r\n              [checked]=\"selection.isSelected(row)\" [aria-label]=\"checkboxLabel(row)\">\r\n            </mat-checkbox>\r\n          </td>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"AccountCode\">\r\n          <mat-header-cell *matHeaderCellDef> Account Code </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.AccountCode}} </mat-cell>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"AccountName\">\r\n          <mat-header-cell *matHeaderCellDef> Account Name </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.AccountName}} </mat-cell>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"BalanceOnOriginalTransactionDates\">\r\n          <mat-header-cell *matHeaderCellDef> Bal. On Original Dates </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.BalanceOnOriginalTransactionDates}} </mat-cell>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"BalanceOnComparisionDate\">\r\n          <mat-header-cell *matHeaderCellDef> Bal. On Comparision Date </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.BalanceOnComparisionDate}} </mat-cell>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"GainLossStatus\">\r\n          <mat-header-cell *matHeaderCellDef>Gain/Loss</mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> <span\r\n              [ngClass]=\"getLabelClass(element.GainLossStatus)\">{{labelText}}</span>\r\n          </mat-cell>\r\n        </ng-container>\r\n\r\n        <ng-container matColumnDef=\"ResultingGainLoss\">\r\n          <mat-header-cell *matHeaderCellDef> Resulting Gain/Loss </mat-header-cell>\r\n          <mat-cell *matCellDef=\"let element\"> {{element.ResultingGainLoss}}\r\n          </mat-cell>\r\n        </ng-container>\r\n\r\n        <mat-header-row *matHeaderRowDef=\"displayedColumns\"></mat-header-row>\r\n        <mat-row *matRowDef=\"let row; columns: displayedColumns;\" (click)=\"selection.toggle(row)\"></mat-row>\r\n      </mat-table>\r\n\r\n      <!-- <mat-paginator [length]=\"purchaseRecordCount\" [pageSize]=\"filterValueModel.PageSize\"\r\n          [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n        </mat-paginator> -->\r\n    </div>\r\n  </div>\r\n\r\n  <mat-card *ngSwitchCase=\"'filter'\">\r\n    <h3>Voucher Transaction Filters</h3>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-2\">\r\n        <lib-search-dropdown placeholder=\"Accounts\" [multiSelect]=\"true\" placeholderSearchLabel=\"Find Account...\"\r\n          noEntriesFoundLabel=\"No matching account found\" [dataSource]=\"accountDataSource\"\r\n          [selectedValue]=\"AccountIdList\" (openedChange)=\"openedChange($event)\"\r\n          (selectionChanged)=\"onSelectionChanged($event)\">\r\n        </lib-search-dropdown>\r\n      </div>\r\n    </div>\r\n    <div class=\"row\">\r\n      <form [formGroup]=\"transactionFiltersForm\" (ngSubmit)=getExchangeGainLossData()>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field>\r\n            <mat-label>Offices</mat-label>\r\n            <mat-select formControlName=\"offices\" multiple>\r\n              <mat-option *ngFor=\"let office of officeList\" [value]=\"office.OfficeId\">{{office.OfficeName}}\r\n              </mat-option>\r\n            </mat-select>\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field>\r\n            <mat-label>Journals</mat-label>\r\n            <mat-select formControlName=\"journals\" multiple>\r\n              <mat-option *ngFor=\"let journal of journalList\" [value]=\"journal.JournalCode\">\r\n                {{journal.JournalCode+'-'+journal.JournalName}}</mat-option>\r\n            </mat-select>\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <mat-form-field>\r\n            <mat-label>Projects</mat-label>\r\n            <mat-select formControlName=\"projects\" multiple>\r\n              <mat-option *ngFor=\"let project of projectList\" [value]=\"project.ProjectId\">\r\n                {{project.ProjectCode + '-'+ project.ProjectName}}</mat-option>\r\n            </mat-select>\r\n          </mat-form-field>\r\n        </div>\r\n        <div class=\"col-sm-12\">\r\n          <hum-button [disabled]=\"!transactionFiltersForm.valid\" [isSubmit]=\"true\" [type]=\"'save'\" [text]=\"'APPLY'\">\r\n          </hum-button>\r\n          <hum-button (click)=\"type=''\" [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n        </div>\r\n      </form>\r\n    </div>\r\n  </mat-card>\r\n  <app-consolidate-gain-loss [calculatorConfigData]=\"calculatorConfigData\" [selectedData]=\"selection.selected\"\r\n    (emitType)=\"subscribeType($event)\" *ngSwitchCase=\"'consolidation'\"></app-consolidate-gain-loss>\r\n</ng-container>\r\n\r\n\r\n<app-configuration-filter (configData)=\"subscribeConfigData($event)\"></app-configuration-filter>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.scss":
/*!*********************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.scss ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2V4Y2hhbmdlLWdhaW4tbG9zcy1yZXBvcnQvZXhjaGFuZ2UtZ2Fpbi1sb3NzLXJlcG9ydC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.ts":
/*!*******************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.ts ***!
  \*******************************************************************************************************/
/*! exports provided: ExchangeGainLossReportComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExchangeGainLossReportComponent", function() { return ExchangeGainLossReportComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var _configuration_filter_configuration_filter_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./configuration-filter/configuration-filter.component */ "./src/app/dashboard/accounting/exchange-gain-loss-report/configuration-filter/configuration-filter.component.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _exchange_gain_loss_report_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./exchange-gain-loss-report.service */ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.service.ts");
/* harmony import */ var rxjs_observable_forkJoin__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/observable/forkJoin */ "./node_modules/rxjs-compat/_esm5/observable/forkJoin.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
/* harmony import */ var _angular_cdk_collections__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/cdk/collections */ "./node_modules/@angular/cdk/esm5/collections.es5.js");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};













var ExchangeGainLossReportComponent = /** @class */ (function () {
    function ExchangeGainLossReportComponent(globalSharedService, gainLossReportService, fb, toastr, commonLoader) {
        this.globalSharedService = globalSharedService;
        this.gainLossReportService = gainLossReportService;
        this.fb = fb;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.showFilters = false;
        this.type = '';
        this.gainLossReportList = [];
        this.accountList = [];
        // gainLossAddVoucherForm: IGainLossAddVoucherForm;
        /** control for the MatSelect filter keyword multi-selection */
        this.accountMultiFilterCtrl = new _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormControl"]();
        this.displayedColumns = ['select', 'AccountCode', 'AccountName',
            'BalanceOnOriginalTransactionDates', 'BalanceOnComparisionDate', 'GainLossStatus', 'ResultingGainLoss'];
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_7__["ReplaySubject"](1);
        this.filteredAccountsMulti = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_7__["ReplaySubject"](1);
        this.selection = new _angular_cdk_collections__WEBPACK_IMPORTED_MODULE_11__["SelectionModel"](true, []);
    }
    ExchangeGainLossReportComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.globalSharedService.setMenuHeaderName('Currency Exchange Gain Loss Calculator');
        this.globalSharedService.setMenuList([]);
        this.onFormInIt();
        this.selectedRows = [];
        this.GainLossFilter = {
            AccountIdList: [],
            ComparisionDate: null,
            FromDate: null,
            JournalIdList: [],
            OfficeIdList: [],
            ProjectIdList: [],
            ToCurrencyId: null,
            ToDate: null
        };
        Object(rxjs_observable_forkJoin__WEBPACK_IMPORTED_MODULE_5__["forkJoin"])([
            this.getProjectList(),
            this.getJournalList(),
            this.getOfficeList(),
            this.getInputLevelAccountList(),
            this.getExchangeGainLossFilterAccountList()
        ])
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (result) {
            _this.subscribeProjectList(result[0]);
            _this.subscribeJournalList(result[1]);
            _this.subscribeOfficeList(result[2]);
            _this.subscribeInputAccountList(result[3]);
            _this.subscribeExchangeGainLossAccountList(result[4]);
        });
    };
    ExchangeGainLossReportComponent.prototype.onFormInIt = function () {
        this.transactionFiltersForm = this.fb.group({
            'offices': [[]],
            'journals': [[]],
            'projects': [[]],
        });
        this.AccountIdList = [];
    };
    ExchangeGainLossReportComponent.prototype.getLabelClass = function (value) {
        this.labelText = value === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["GainLossStatus"].Gain ? 'Gain' : value < 0 ? 'Loss' : value === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["GainLossStatus"].Consolidated ?
            'Consolidated' : 'Balanced';
        return value === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["GainLossStatus"].Gain ? 'label label-success' : value < src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["GainLossStatus"].Balanced ?
            'label label-danger' : value === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["GainLossStatus"].Consolidated ?
            'label label-primary' : 'label label-info';
    };
    ExchangeGainLossReportComponent.prototype.showConfiguration = function () {
        this.fieldConfig.show();
    };
    //#region "getProjectList"
    ExchangeGainLossReportComponent.prototype.getProjectList = function () {
        return this.gainLossReportService.GetProjectList();
    };
    //#endregion
    ExchangeGainLossReportComponent.prototype.subscribeProjectList = function (response) {
        var _this = this;
        this.projectList = [];
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
    };
    //#region "getJournalList"
    ExchangeGainLossReportComponent.prototype.getJournalList = function () {
        return this.gainLossReportService.GetJournalList();
    };
    //#endregion
    ExchangeGainLossReportComponent.prototype.subscribeJournalList = function (response) {
        var _this = this;
        this.journalList = [];
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
    };
    //#region "getOfficeList"
    ExchangeGainLossReportComponent.prototype.getOfficeList = function () {
        return this.gainLossReportService.GetOfficeList();
    };
    //#endregion
    ExchangeGainLossReportComponent.prototype.subscribeOfficeList = function (response) {
        var _this = this;
        this.officeList = [];
        if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(function (element) {
                _this.officeList.push({
                    OfficeId: element.OfficeId,
                    OfficeName: element.OfficeName,
                    IsChecked: false
                });
            });
        }
    };
    //#region "getInputLevelAccountList"
    ExchangeGainLossReportComponent.prototype.getInputLevelAccountList = function () {
        return this.gainLossReportService.GetInputLevelAccountList();
    };
    //#endregion
    ExchangeGainLossReportComponent.prototype.subscribeInputAccountList = function (response) {
        var _this = this;
        this.accountList = [];
        this.accountDataSource = [];
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
            this.filteredAccountsMulti.next(this.accountList.slice());
            // listen for search field value changes
            this.accountMultiFilterCtrl.valueChanges
                .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
                .subscribe(function () {
                _this.filterAccounts();
            });
        }
    };
    //#region "FILTER: Account filter"
    ExchangeGainLossReportComponent.prototype.filterAccounts = function () {
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
    //#endregion
    // #region "gainLossAccountSelectionChanged"
    ExchangeGainLossReportComponent.prototype.openedChange = function (event) {
        this.AccountIdList = event.Value;
        if (!event.Flag) {
            this.saveExchangeGainLossFilterAccountList();
        }
    };
    // #endregion
    //#region "getExchangeGainLossFilterAccountList"
    ExchangeGainLossReportComponent.prototype.saveExchangeGainLossFilterAccountList = function () {
        this.gainLossReportService.SaveExchangeGainLossFilterAccountList(this.AccountIdList).subscribe(function (response) {
            if (response.statusCode === 200) {
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getExchangeGainLossFilterAccountList"
    ExchangeGainLossReportComponent.prototype.getExchangeGainLossFilterAccountList = function () {
        return this.gainLossReportService.GetExchangeGainLossFilterAccountList();
    };
    //#endregion
    ExchangeGainLossReportComponent.prototype.subscribeExchangeGainLossAccountList = function (response) {
        var _this = this;
        this.AccountIdList = [];
        if (response.statusCode === 200 && response.data !== null) {
            this.AccountIdList = response.data;
            setTimeout(function () {
                _this.getExchangeGainLossData();
            }, 1000);
        }
    };
    ExchangeGainLossReportComponent.prototype.onSelectionChanged = function (event) {
        // this.gainLossReportfilter.AccountIdList = event;
    };
    ExchangeGainLossReportComponent.prototype.subscribeConfigData = function (event) {
        this.calculatorConfigData = {
            ComparisionDate: event.ComparisionDate,
            CreditAccount: event.CreditAccount,
            DebitAccount: event.DebitAccount,
            CurrencyId: event.CurrencyId,
            EndDate: event.EndDate,
            StartDate: event.StartDate,
            CreditAccountName: event.CreditAccountName,
            DebitAccountName: event.DebitAccountName
        };
        this.getExchangeGainLossData();
    };
    ExchangeGainLossReportComponent.prototype.applyTransactionFilter = function () {
        this.getExchangeGainLossData();
        this.toggeleShowFilter();
    };
    ExchangeGainLossReportComponent.prototype.toggeleShowFilter = function () {
        this.showFilters = !this.showFilters;
    };
    ExchangeGainLossReportComponent.prototype.getExchangeGainLossData = function () {
        var _this = this;
        if (!this.calculatorConfigData.CurrencyId && !this.calculatorConfigData.StartDate &&
            !this.calculatorConfigData.EndDate && !this.calculatorConfigData.ComparisionDate) {
            this.toastr.warning('Calculator configuration not set');
            return;
        }
        if (this.AccountIdList.length === 0) {
            this.toastr.warning('Accounts not selected in transaction filter');
            return;
        }
        this.commonLoader.showLoader();
        this.GainLossFilter.ComparisionDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].setLocalDate(this.calculatorConfigData.ComparisionDate);
        this.GainLossFilter.FromDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].setLocalDate(this.calculatorConfigData.StartDate);
        this.GainLossFilter.ToDate = src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_10__["StaticUtilities"].setLocalDate(this.calculatorConfigData.EndDate);
        this.GainLossFilter.AccountIdList = this.AccountIdList;
        this.GainLossFilter.JournalIdList = this.transactionFiltersForm.value.journals;
        this.GainLossFilter.OfficeIdList = this.transactionFiltersForm.value.offices;
        this.GainLossFilter.ProjectIdList = this.transactionFiltersForm.value.projects;
        this.GainLossFilter.ToCurrencyId = this.calculatorConfigData.CurrencyId;
        this.gainLossReportService
            .GetGainLossReportList(this.GainLossFilter)
            .subscribe(function (response) {
            _this.gainLossReportList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.gainLossReportList.push({
                        AccountId: element.AccountId,
                        AccountCode: element.AccountCode,
                        AccountName: element.AccountName,
                        BalanceOnOriginalTransactionDates: element.BalanceOnOriginalDate,
                        BalanceOnComparisionDate: element.BalanceOnCurrentDate,
                        ResultingGainLoss: element.GainLossAmount,
                        GainLossStatus: element.GainLossStatus
                    });
                });
                _this.type = '';
                // this.sumOfGainLossAmount();
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
    ExchangeGainLossReportComponent.prototype.selectionevent = function (event) {
        console.log(event, 'e');
        console.log(event, 'e');
    };
    ExchangeGainLossReportComponent.prototype.onConsolidation = function () {
        if (this.selection.selected.length <= 0) {
            this.toastr.warning('Please select atleast one account');
        }
        else {
            var accounts = this.selection.selected.filter(function (x) { return x.GainLossStatus === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["GainLossStatus"].Consolidated ||
                x.GainLossStatus === src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["GainLossStatus"].Balanced; });
            if (accounts.length > 0) {
                this.toastr.warning('Please remove selected consolidated or balanced accounts and try again');
                return;
            }
            this.type = 'consolidation';
        }
    };
    ExchangeGainLossReportComponent.prototype.subscribeType = function (event) {
        this.type = '';
        this.selection.clear();
        this.getExchangeGainLossData();
    };
    ExchangeGainLossReportComponent.prototype.clearTransactionFilters = function () {
        this.onFormInIt();
    };
    /** Whether the number of selected elements matches the total number of rows. */
    ExchangeGainLossReportComponent.prototype.isAllSelected = function () {
        var numSelected = this.selection.selected.length;
        var numRows = this.gainLossReportList.length;
        return numSelected === numRows;
    };
    /** Selects all rows if they are not all selected; otherwise clear selection. */
    ExchangeGainLossReportComponent.prototype.masterToggle = function () {
        var _this = this;
        this.isAllSelected() ?
            this.selection.clear() :
            this.gainLossReportList.forEach(function (row) { return _this.selection.select(row); });
    };
    /** The label for the checkbox on the passed row */
    ExchangeGainLossReportComponent.prototype.checkboxLabel = function (row) {
        if (!row) {
            return (this.isAllSelected() ? 'select' : 'deselect') + " all";
        }
        return (this.selection.isSelected(row) ? 'deselect' : 'select') + " row " + (row.position + 1);
    };
    ExchangeGainLossReportComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewChild"])(_configuration_filter_configuration_filter_component__WEBPACK_IMPORTED_MODULE_2__["ConfigurationFilterComponent"]),
        __metadata("design:type", _configuration_filter_configuration_filter_component__WEBPACK_IMPORTED_MODULE_2__["ConfigurationFilterComponent"])
    ], ExchangeGainLossReportComponent.prototype, "fieldConfig", void 0);
    ExchangeGainLossReportComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-exchange-gain-loss-report',
            template: __webpack_require__(/*! ./exchange-gain-loss-report.component.html */ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.html"),
            styles: [__webpack_require__(/*! ./exchange-gain-loss-report.component.scss */ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_1__["GlobalSharedService"],
            _exchange_gain_loss_report_service__WEBPACK_IMPORTED_MODULE_4__["ExchangeGainLossReportService"], _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormBuilder"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_8__["ToastrService"], src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_9__["CommonLoaderService"]])
    ], ExchangeGainLossReportComponent);
    return ExchangeGainLossReportComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.service.ts":
/*!*****************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/exchange-gain-loss-report/exchange-gain-loss-report.service.ts ***!
  \*****************************************************************************************************/
/*! exports provided: ExchangeGainLossReportService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExchangeGainLossReportService", function() { return ExchangeGainLossReportService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/internal/operators/map */ "./node_modules/rxjs/internal/operators/map.js");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__);
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





// import { IGainLossAddVoucherForm } from './gain-loss-report.model';
var ExchangeGainLossReportService = /** @class */ (function () {
    //#endregion
    function ExchangeGainLossReportService(globalService, appurl) {
        this.globalService = globalService;
        this.appurl = appurl;
    }
    //#region "GetGainLossReportList"
    ExchangeGainLossReportService.prototype.GetGainLossReportList = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_GainLossReport_GetExchangeGainLossReport, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.GetInputLevelAccountList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Account_GetAllInputLevelAccountCode)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.GetCurrencyList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_code_GetAllCurrency)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.GetOfficeList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_code_GetAllOffice)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.GetJournalList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Code_GetAllJournalDetail)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.GetProjectList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Project_GetAllProjectList)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.deleteAccountFromFilter = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Project_GetAllProjectList)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.SaveGainLossAccountList = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_FinancialReport_SaveGainLossAccountList, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.GetExchangeGainLossFilterAccountList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_GainLossReport_GetExchangeGainLossFilterAccountList)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.SaveExchangeGainLossFilterAccountList = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_FinancialReport_SaveGainLossAccountList, model)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.GetVoucherTypeList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Account_GetAllVoucherType)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
            var responseData = {
                data: x.data.VoucherTypeList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "SaveCalculatorConfigData"
    ExchangeGainLossReportService.prototype.SaveCalculatorConfigData = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_Account_SaveCalculatorConfigData, model);
    };
    //#endregion
    //#region "AddGainLossVoucher"
    ExchangeGainLossReportService.prototype.AddGainLossVoucher = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_GainLossReport_AddExchangeGainLossVoucher, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.GetGainLossVoucherList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_GainLossReport_GetExchangeGainLossVoucherList)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
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
    ExchangeGainLossReportService.prototype.DeleteGainLossVoucher = function (data) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_GainLossReport_DeleteGainLossVoucherTransaction, data)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_3__["map"])(function (x) {
            var responseData = {
                data: null,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetGainLossCaculatorConfiguration"
    ExchangeGainLossReportService.prototype.GetGainLossCaculatorConfiguration = function () {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_GainLossReport_GetGainLossCaculatorConfiguration);
    };
    //#endregion
    ExchangeGainLossReportService.prototype.GetDefaultAccountingPeriod = function () {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_4__["GLOBAL"].API_GainLossReport_GetDefaultAccountingPeriod);
    };
    ExchangeGainLossReportService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"]])
    ], ExchangeGainLossReportService);
    return ExchangeGainLossReportService;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/journal-report/journal-report.component.html":
/*!***********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/journal-report/journal-report.component.html ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-card [ngStyle]=\"scrollStyles\">\r\n    <div class=\"main-journal\">\r\n      <mat-card>\r\n        <form [formGroup]=\"journalFilterForm\" (ngSubmit)=\"onApplyingFilter(journalFilterForm.value)\">\r\n          <div class=\"row\">\r\n            <div class=\"col-md-3\">\r\n                <lib-search-dropdown\r\n                placeholder=\"Office\"\r\n                [multiSelect]=\"true\"\r\n                placeholderSearchLabel=\"Find Offices...\"\r\n                noEntriesFoundLabel=\"No matching Accounts found\"\r\n                [dataSource]=\"officeDropdown\"\r\n                [selectedValue]=\"OfficeIds\"\r\n                (openedChange)=\"\r\n                        onOpenedOfficeMultiSelectChange($event)\r\n                      \"\r\n              >\r\n              </lib-search-dropdown>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <lib-hum-dropdown [options]=\"currencyId$\" formControlName=\"CurrencyId\" [placeHolder]=\"'Currency'\"\r\n                ></lib-hum-dropdown>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n              <lib-hum-dropdown [options]=\"recordType$\" formControlName=\"RecordType\" [placeHolder]=\"'Record Type'\"\r\n                ></lib-hum-dropdown>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <lib-search-dropdown\r\n                placeholder=\"Journal\"\r\n                [multiSelect]=\"true\"\r\n                placeholderSearchLabel=\"Find Journals...\"\r\n                noEntriesFoundLabel=\"No matching Journals found\"\r\n                [dataSource]=\"journalDropdown\"\r\n                [selectedValue]=\"journalIds\"\r\n                (openedChange)=\"onOpenedJournalMultiSelectChange($event)\"\r\n              >\r\n              </lib-search-dropdown>\r\n            </div>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-md-3\">\r\n                <lib-search-dropdown\r\n                placeholder=\"Accounts\"\r\n                [multiSelect]=\"true\"\r\n                placeholderSearchLabel=\"Find Accounts...\"\r\n                noEntriesFoundLabel=\"No matching Accounts found\"\r\n                [dataSource]=\"accountDropdown\"\r\n                [selectedValue]=\"AccountIds\"\r\n                (openedChange)=\"onOpenedAccountMultiSelectChange($event)\"\r\n              >\r\n            </lib-search-dropdown>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <mat-form-field>\r\n                  <input matInput [satDatepicker]=\"resultPicker\" formControlName=\"date\">\r\n                  <sat-datepicker\r\n                      #resultPicker\r\n                      [rangeMode]=\"true\">\r\n                  </sat-datepicker>\r\n                  <sat-datepicker-toggle matSuffix [for]=\"resultPicker\"></sat-datepicker-toggle>\r\n                </mat-form-field>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <lib-search-dropdown\r\n                placeholder=\"Projects\"\r\n                [multiSelect]=\"true\"\r\n                placeholderSearchLabel=\"Find Project...\"\r\n                noEntriesFoundLabel=\"No matching project found\"\r\n                [dataSource]=\"multiProjectList\"\r\n                [selectedValue]=\"ProjectIds\"\r\n                (openedChange)=\"onOpenedProjectMultiSelectChange($event)\"\r\n              >\r\n              </lib-search-dropdown>\r\n            </div>\r\n            <div class=\"col-md-3\">\r\n                <lib-search-dropdown\r\n                placeholder=\"BudgetLines\"\r\n                [multiSelect]=\"true\"\r\n                placeholderSearchLabel=\"Find BudgetLine...\"\r\n                noEntriesFoundLabel=\"No matching budgetline found\"\r\n                [dataSource]=\"multiBudgetLineList\"\r\n                [selectedValue]=\"budgetLineIds\"\r\n                (openedChange)=\"onOpenedBudgetLineMultiSelectChange($event)\"\r\n              >\r\n              </lib-search-dropdown>\r\n            </div>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-md-3\">\r\n                <lib-search-dropdown\r\n                placeholder=\"Project Jobs\"\r\n                [multiSelect]=\"true\"\r\n                placeholderSearchLabel=\"Find Project Job...\"\r\n                noEntriesFoundLabel=\"No matching project job found\"\r\n                [dataSource]=\"multiProjectJobList\"\r\n                [selectedValue]=\"projectJobIds\"\r\n                (openedChange)=\"onOpenedProjectJobMultiSelectChange($event)\"\r\n                ></lib-search-dropdown>\r\n            </div>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-md-10\"></div>\r\n            <div class=\"col-md-2\">\r\n                <hum-button [isSubmit]=\"true\" [type]=\"'save'\" [text]=\"'UPDATE FILTERS'\">\r\n                  </hum-button>\r\n            </div>\r\n          </div>\r\n\r\n        </form>\r\n      </mat-card>\r\n      <lib-sub-header-template>\r\n        <span class=\"action_header\">\r\n        </span>\r\n\r\n        <div class=\"action_section\">\r\n            <hum-button (click)=\"ExportJournalExcel(journalFilterForm.value)\" [type]=\"'download'\" [text]=\"'EXPORT TO EXCEL'\"></hum-button>\r\n            <hum-button (click)=\"ExportJournalPdf(journalFilterForm.value)\" [type]=\"'download'\" [text]=\"'JOURNAL REPORT'\"></hum-button>\r\n          <hum-button (click)=\"ExportBudgetlinePdf(journalFilterForm.value)\" [type]=\"'download'\" [text]=\"'SUMMARY REPORT'\"></hum-button>\r\n          <hum-button (click)=\"ExportLedgerPdf(journalFilterForm.value)\" [type]=\"'download'\" [text]=\"'LEDGER REPORT'\"></hum-button>\r\n          <hum-button (click)=\"ExportTrialBalPdf(journalFilterForm.value)\" [type]=\"'download'\" [text]=\"'TRIAL REPORT'\"></hum-button>\r\n        </div>\r\n      </lib-sub-header-template>\r\n      <mat-card>\r\n        <hum-table [headers]=\"journalListHeaders$\" [items]=\"journalFilterList$\"></hum-table>\r\n\r\n        <div *ngIf=\"(journalFilterList$ | async)\">\r\n          <h5 class=\"display_inline_block\">Total Debits: </h5><strong>{{this.debitSumForReport}}</strong><br>\r\n          <h5 class=\"display_inline_block\">Total Credits: </h5><strong>{{this.creditSumForReport}}</strong>\r\n        </div>\r\n      </mat-card>\r\n    </div>\r\n</mat-card>\r\n\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/journal-report/journal-report.component.scss":
/*!***********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/journal-report/journal-report.component.scss ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2pvdXJuYWwtcmVwb3J0L2pvdXJuYWwtcmVwb3J0LmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/journal-report/journal-report.component.ts":
/*!*********************************************************************************!*\
  !*** ./src/app/dashboard/accounting/journal-report/journal-report.component.ts ***!
  \*********************************************************************************/
/*! exports provided: JournalReportComponent, JournalVoucherModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "JournalReportComponent", function() { return JournalReportComponent; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "JournalVoucherModel", function() { return JournalVoucherModel; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_dashboard_accounting_report_services_report_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/dashboard/accounting/report-services/report.service */ "./src/app/dashboard/accounting/report-services/report.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
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







var JournalReportComponent = /** @class */ (function () {
    //#endregion
    function JournalReportComponent(accountservice, toastr, fb, globalService) {
        this.accountservice = accountservice;
        this.toastr = toastr;
        this.fb = fb;
        this.globalService = globalService;
        this.menuList = [];
        this.initialFlag = 0;
        this.projectList = [];
        // Report
        this.viewPdfFlag = true;
        this.debitSumForReport = '0.00';
        this.creditSumForReport = '0.00';
        this.balanceSumForReport = '0.00';
        // Loader
        this.journalListLoading = false;
        this.intialFlagValue = 0;
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_3__["ReplaySubject"](1);
        this.journalListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(['Transaction Date', 'Voucher Code', 'Transaction Description', 'Currency', 'Account Code', 'Account Name',
            'Debit Amount', 'Credit Amount', 'Project', 'Budget Line', 'Job Code']);
        this.FromDate = '01/01/' + new Date().getFullYear();
        this.currentDateFinal = new Date();
        this.recordTypeDropdown = [
            {
                value: 1,
                name: 'Single'
            },
            {
                value: 2,
                name: 'Consolidate'
            }
        ];
        this.recordType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(this.recordTypeDropdown);
        this.defaultRecordType = this.recordTypeDropdown[0].Id;
        this.journalFilter = {
            CurrencyId: null,
            JournalCode: null,
            OfficesList: [],
            RecordType: null,
            date: { 'begin': new Date(new Date().getFullYear(), new Date().getMonth(), 1), 'end': new Date() },
            BudgetLine: null,
            Project: null,
            JobCode: null,
            accountLists: null,
            FromDate: null,
            ToDate: null
        };
        // Journal DateRange
        this.journalDateRange = [];
        this.journalDateRange.push(new Date(new Date().getFullYear(), 0, 1, new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()));
        this.journalDateRange.push(new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()));
        // Set Menu Header List
        this.globalService.setMenuList(this.menuList);
        this.getScreenSize();
    }
    JournalReportComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    JournalReportComponent.prototype.ngOnInit = function () {
        this.globalService.setMenuHeaderName('Journal');
        this.getProjectList();
        this.getCurrencyCodeList();
        this.getOfficeCodeList();
        this.getJournalDropdownList();
        // this.GetAllJournalDetails();
        this.GetAccountDetails();
        this.journalFilterForm = this.fb.group({
            'OfficesList': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'CurrencyId': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'JournalCode': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'RecordType': [1, _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'accountLists': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'date': [{ 'begin': new Date(new Date().getFullYear(), 0, 1), 'end': new Date() },
                _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'Project': [[]],
            'JobCode': [[]],
            'BudgetLine': [[]]
        });
    };
    //#region "onApplyingFilter"
    JournalReportComponent.prototype.onApplyingFilter = function (value) {
        if (this.journalDateRange == null) {
            this.toastr.error('Please Select Date Range');
        }
        else {
            this.journalFilter = {
                CurrencyId: value.CurrencyId,
                JournalCode: value.JournalCode,
                OfficesList: value.OfficesList,
                RecordType: value.RecordType == null ? this.defaultRecordType : value.RecordType,
                FromDate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
                ToDate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
                BudgetLine: value.BudgetLine,
                Project: value.Project,
                JobCode: value.JobCode,
                accountLists: value.accountLists,
                date: null
            };
            this.GetAllJournalDetails(this.journalFilter);
        }
    };
    //#endregion
    JournalReportComponent.prototype.onOpenedAccountMultiSelectChange = function (event) {
        this.journalFilterForm.controls['accountLists'].setValue(event.Value);
    };
    JournalReportComponent.prototype.onOpenedOfficeMultiSelectChange = function (event) {
        this.journalFilterForm.controls['OfficesList'].setValue(event.Value);
    };
    JournalReportComponent.prototype.onOpenedJournalMultiSelectChange = function (event) {
        this.journalFilterForm.controls['JournalCode'].setValue(event.Value);
    };
    JournalReportComponent.prototype.onOpenedProjectMultiSelectChange = function (event) {
        this.journalFilterForm.controls['Project'].setValue(event.Value);
        var projectFilter = event.Value;
        if (event.Flag === true) {
            return;
        }
        else if ((projectFilter.length > 0)) {
            this.multiBudgetLineList = [];
            this.multiProjectJobList = [];
            this.journalFilterForm.controls['JobCode'].setValue([]);
            this.journalFilterForm.controls['BudgetLine'].setValue([]);
            this.getProjectBudgetLineList(projectFilter);
        }
        else {
            this.multiBudgetLineList = [];
            this.multiProjectJobList = [];
            this.journalFilterForm.controls['JobCode'].setValue([]);
            this.journalFilterForm.controls['BudgetLine'].setValue([]);
        }
    };
    JournalReportComponent.prototype.onOpenedProjectJobMultiSelectChange = function (event) {
        this.journalFilterForm.controls['JobCode'].setValue(event.Value);
    };
    JournalReportComponent.prototype.onOpenedBudgetLineMultiSelectChange = function (event) {
        this.journalFilterForm.controls['BudgetLine'].setValue(event.Value);
        var projectbudgetFilter = event.Value;
        if (event.Flag === true) {
            return;
        }
        else if (projectbudgetFilter.length > 0) {
            this.getProjectJobList(projectbudgetFilter);
        }
        else {
            this.multiProjectJobList = [];
            this.journalFilterForm.controls['JobCode'].setValue([]);
        }
    };
    Object.defineProperty(JournalReportComponent.prototype, "AccountIds", {
        get: function () {
            return this.journalFilterForm.get('accountLists').value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(JournalReportComponent.prototype, "journalIds", {
        get: function () {
            return this.journalFilterForm.get('JournalCode').value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(JournalReportComponent.prototype, "OfficeIds", {
        get: function () {
            return this.journalFilterForm.get('OfficesList').value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(JournalReportComponent.prototype, "ProjectIds", {
        get: function () {
            return this.journalFilterForm.get('Project').value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(JournalReportComponent.prototype, "projectJobIds", {
        get: function () {
            return this.journalFilterForm.get('JobCode').value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(JournalReportComponent.prototype, "budgetLineIds", {
        get: function () {
            return this.journalFilterForm.get('BudgetLine').value;
        },
        enumerable: true,
        configurable: true
    });
    JournalReportComponent.prototype.getProjectList = function () {
        var _this = this;
        this.accountservice
            .GetProjectList()
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.projectList = [];
            _this.multiProjectList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.projectList.push({
                        ProjectId: element.ProjectId,
                        ProjectCode: element.ProjectCode,
                        ProjectName: element.ProjectName,
                        ProjectNameCode: element.ProjectCode + '-' + element.ProjectName
                    });
                    _this.multiProjectList.push({
                        Id: element.ProjectId,
                        Name: element.ProjectCode + '-' + element.ProjectName
                    });
                });
            }
        }, function (error) { });
    };
    JournalReportComponent.prototype.getProjectJobList = function (projectbudgetIds) {
        var _this = this;
        this.accountservice
            .getProjectJobList(projectbudgetIds)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.multiProjectJobList = [];
            _this.selectedJobs = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.multiProjectJobList.push({
                        Id: element.ProjectJobId,
                        Name: element.ProjectJobName
                    });
                });
                _this.multiProjectJobList.forEach(function (x) {
                    _this.selectedJobs.push(x.Id);
                });
                _this.journalFilterForm.controls['JobCode'].setValue(_this.selectedJobs);
            }
        }, function (error) { });
    };
    JournalReportComponent.prototype.getProjectBudgetLineList = function (projectIds) {
        var _this = this;
        this.accountservice
            .getProjectBudgetLineList(projectIds)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.multiBudgetLineList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.multiBudgetLineList.push({
                        Id: element.BudgetLineId,
                        Name: element.BudgetName
                    });
                });
                _this.journalFilterForm.controls['BudgetLine'].setValue([]);
            }
        }, function (error) { });
    };
    //#region "getCurrencyCodeList"
    JournalReportComponent.prototype.getCurrencyCodeList = function () {
        var _this = this;
        this.accountservice
            .GetAllCurrencyCodeList()
            .subscribe(function (data) {
            _this.currencyDropdown = [];
            if (data.data.CurrencyList != null) {
                data.data.CurrencyList.forEach(function (element) {
                    _this.currencyDropdown.push(element);
                });
                _this.selectedCurrency = _this.currencyDropdown[0].CurrencyId;
                _this.journalFilterForm.controls['CurrencyId'].setValue(_this.selectedCurrency);
                _this.currencyId$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(_this.currencyDropdown.map(function (y) {
                    return {
                        value: y.CurrencyId,
                        name: y.CurrencyCode + '-' + y.CurrencyName
                    };
                }));
                // Initial Page Loading
                _this.intialFlagValue += 1;
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
    //#region  Get Office Code in Add, Edit Dropdown
    JournalReportComponent.prototype.getOfficeCodeList = function () {
        var _this = this;
        this.accountservice
            .GetAllOfficeCodeList()
            .subscribe(function (data) {
            if (data.data.OfficeDetailsList != null) {
                _this.officeDropdown = [];
                // const allOffices = [];
                var officeIds = localStorage.getItem('ALLOFFICES') != null
                    ? localStorage.getItem('ALLOFFICES').split(',')
                    : null;
                data.data.OfficeDetailsList.forEach(function (element) {
                    _this.officeDropdown.push({
                        Id: element.OfficeId,
                        Name: element.OfficeName
                    });
                });
                // officeIds.forEach(x => {
                //   const officeData = allOffices.filter(
                //     // tslint:disable-next-line:radix
                //     e => e.OfficeId === parseInt(x)
                //   )[0];
                //   this.officeDropdown.push(officeData);
                // });
                _this.selectedOffices = [];
                _this.officeDropdown.forEach(function (x) {
                    // tslint:disable-next-line:radix
                    _this.selectedOffices.push(x.Id);
                });
                _this.journalFilterForm.controls['OfficesList'].setValue(_this.selectedOffices);
                // Initial Page Loading
                _this.intialFlagValue += 1;
                // sort in Asc
                _this.officeDropdown = _this.accountservice.sortDropdown(_this.officeDropdown, 'OfficeName');
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
    //#region "GetAllJournalDetails"
    JournalReportComponent.prototype.GetAllJournalDetails = function (journalFilter) {
        var _this = this;
        this.showjournalListLoading();
        this.journalReportDataSource = [];
        this.debitSumForReport = '0.0000';
        this.creditSumForReport = '0.0000';
        this.balanceSumForReport = '0.0000';
        this.accountservice
            .GetAllJournalDetails(journalFilter)
            .subscribe(function (data) {
            _this.journalDataSource = [];
            if (data.StatusCode === 200 &&
                data.data.JournalVoucherViewList != null &&
                data.data.JournalVoucherViewList.length > 0) {
                _this.journalReportDataSource = data.data.JournalReportList;
                _this.debitSumForReport = _this.accountservice
                    .sumOfListInArray(_this.journalReportDataSource, 'DebitAmount')
                    .toFixed(4);
                _this.creditSumForReport = _this.accountservice
                    .sumOfListInArray(_this.journalReportDataSource, 'CreditAmount')
                    .toFixed(4);
                _this.balanceSumForReport = (parseFloat(_this.debitSumForReport) -
                    parseFloat(_this.creditSumForReport)).toFixed(4);
                data.data.JournalVoucherViewList.forEach(function (element) {
                    _this.journalDataSource.push(element);
                });
                _this.journalFilterList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])(_this.journalDataSource).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["map"])(function (r) { return r.map(function (v) { return ({
                    Transaction_Date: new Date(v.TransactionDate).getDate() + '/' + (new Date(v.TransactionDate).getMonth() + 1) +
                        '/' + new Date(v.TransactionDate).getFullYear(),
                    Voucher_Code: v.ReferenceNo,
                    Transaction_Description: v.TransactionDescription,
                    Currency: _this.currencyDropdown[_this.currencyDropdown.findIndex(function (x) { return x.CurrencyId === v.CurrencyId; })]['CurrencyName'],
                    Account_Code: v.AccountCode,
                    Account_Name: v.AccountName,
                    DebitAmount: v.DebitAmount,
                    CreditAmount: v.CreditAmount,
                    Project: v.Project,
                    BudgetLine: v.BudgetLineDescription,
                    JobCode: v.JobCode
                }); }); }));
            }
            else {
                _this.journalFilterList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["of"])();
            }
            // else
            //   this.toastr.error(data.Message);
            _this.hidejournalListLoading();
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
            _this.hidejournalListLoading();
        });
    };
    //#endregion
    //#region  Get all Journal Dropdown
    JournalReportComponent.prototype.getJournalDropdownList = function () {
        var _this = this;
        // this.journalCodeListLoading = true;
        this.accountservice
            .GetAllJournalCodeList()
            .subscribe(function (data) {
            _this.journalDropdown = [];
            if (data.StatusCode === 200 &&
                data.data.JournalDetailList != null) {
                if (data.data.JournalDetailList.length > 0) {
                    data.data.JournalDetailList.forEach(function (element) {
                        _this.journalDropdown.push({
                            Id: element.JournalCode,
                            Name: element.JournalName
                        });
                    });
                    _this.selectedJournal = [];
                    var JournalCodes_1 = [];
                    _this.journalDropdown.forEach(function (x) {
                        JournalCodes_1.push(x.Id);
                    });
                    _this.selectedJournal = JournalCodes_1;
                    _this.journalFilterForm.controls['JournalCode'].setValue(_this.selectedJournal);
                    // Initial Page Loading
                    _this.intialFlagValue += 1;
                }
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
            else {
            }
        });
    };
    //#endregion
    //#region  "GetAccountDetails"
    JournalReportComponent.prototype.GetAccountDetails = function () {
        var _this = this;
        this.accountservice
            .GetAccountDetails()
            .subscribe(function (data) {
            _this.accountDropdown = [];
            _this.selectedAccounts = [];
            if (data.StatusCode === 200 && data.data.AccountDetailList != null) {
                data.data.AccountDetailList = data.data.AccountDetailList.filter(function (x) { return x.AccountLevelId === 4; });
                if (data.data.AccountDetailList.length > 0) {
                    data.data.AccountDetailList.forEach(function (element) {
                        _this.accountDropdown.push({
                            Id: element.AccountCode,
                            Name: element.AccountName
                        });
                    });
                    var AccountNos = [];
                    _this.accountDropdown.forEach(function (x) {
                        return _this.selectedAccounts.push(x.Id);
                    });
                    _this.journalFilterForm.controls['accountLists'].setValue(_this.selectedAccounts);
                }
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
            else {
            }
        });
    };
    //#endregion
    JournalReportComponent.prototype.ExportTrialBalPdf = function (value) {
        this.journalFilter = {
            CurrencyId: value.CurrencyId,
            JournalCode: value.JournalCode,
            OfficesList: value.OfficesList,
            RecordType: value.RecordType == null ? this.defaultRecordType : value.RecordType,
            FromDate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            ToDate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            BudgetLine: value.BudgetLine,
            Project: value.Project,
            JobCode: value.JobCode,
            accountLists: value.accountLists,
            date: null
        };
        this.accountservice.onExportJournalTrialBalancePdf(this.journalFilter);
    };
    JournalReportComponent.prototype.ExportBudgetlinePdf = function (value) {
        this.journalFilter = {
            CurrencyId: value.CurrencyId,
            JournalCode: value.JournalCode,
            OfficesList: value.OfficesList,
            RecordType: value.RecordType == null ? this.defaultRecordType : value.RecordType,
            FromDate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            ToDate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            BudgetLine: value.BudgetLine,
            Project: value.Project,
            JobCode: value.JobCode,
            accountLists: value.accountLists,
            date: null
        };
        this.accountservice.onExportJournalBudgetlinePdf(this.journalFilter);
    };
    JournalReportComponent.prototype.ExportLedgerPdf = function (value) {
        this.journalFilter = {
            CurrencyId: value.CurrencyId,
            JournalCode: value.JournalCode,
            OfficesList: value.OfficesList,
            RecordType: value.RecordType == null ? this.defaultRecordType : value.RecordType,
            FromDate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            ToDate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            BudgetLine: value.BudgetLine,
            Project: value.Project,
            JobCode: value.JobCode,
            accountLists: value.accountLists,
            date: null
        };
        this.accountservice.onExportJournalLedgerPdf(this.journalFilter);
    };
    JournalReportComponent.prototype.ExportJournalPdf = function (value) {
        this.journalFilter = {
            CurrencyId: value.CurrencyId,
            JournalCode: value.JournalCode,
            OfficesList: value.OfficesList,
            RecordType: value.RecordType == null ? this.defaultRecordType : value.RecordType,
            FromDate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            ToDate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            BudgetLine: value.BudgetLine,
            Project: value.Project,
            JobCode: value.JobCode,
            accountLists: value.accountLists,
            date: null
        };
        this.accountservice.onExportJournalPdf(this.journalFilter);
    };
    JournalReportComponent.prototype.ExportJournalExcel = function (value) {
        this.journalFilter = {
            CurrencyId: value.CurrencyId,
            JournalCode: value.JournalCode,
            OfficesList: value.OfficesList,
            RecordType: value.RecordType == null ? this.defaultRecordType : value.RecordType,
            FromDate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            ToDate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            BudgetLine: value.BudgetLine,
            Project: value.Project,
            JobCode: value.JobCode,
            accountLists: value.accountLists,
            date: null
        };
        this.accountservice.onExportJournalExcel(this.journalFilter);
    };
    //#region "export pdf"
    JournalReportComponent.prototype.printJournalReport = function () {
        var printContents, popupWin;
        printContents = document.getElementById('print-content-journal-report')
            .innerHTML;
        popupWin = window.open('', '_blank', '');
        popupWin.document.open();
        popupWin.document.write("\n  <html>\n    <head>\n      <title></title>\n      <style>\n      //........Customized style.......\n      </style>\n    </head>\n    <body onload=\"window.print();window.close()\">" + printContents + "</body>\n  </html>");
        popupWin.document.close();
    };
    //#endregion
    //#region "Hide/Show"
    JournalReportComponent.prototype.showjournalListLoading = function () {
        this.journalListLoading = true;
    };
    JournalReportComponent.prototype.hidejournalListLoading = function () {
        this.journalListLoading = false;
    };
    JournalReportComponent.prototype.showJournalPdf = function () {
        this.viewPdfFlag = false;
    };
    JournalReportComponent.prototype.hideJournalPdf = function () {
        this.viewPdfFlag = true;
    };
    //#endregion
    JournalReportComponent.prototype.customizeValue = function (data) {
        if (data.value !== 0) {
            return parseFloat(data.value).toFixed(4);
        }
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], JournalReportComponent.prototype, "getScreenSize", null);
    JournalReportComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-journal-report',
            template: __webpack_require__(/*! ./journal-report.component.html */ "./src/app/dashboard/accounting/journal-report/journal-report.component.html"),
            styles: [__webpack_require__(/*! ./journal-report.component.scss */ "./src/app/dashboard/accounting/journal-report/journal-report.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_dashboard_accounting_report_services_report_service__WEBPACK_IMPORTED_MODULE_1__["ReportService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_4__["FormBuilder"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_5__["GlobalSharedService"]])
    ], JournalReportComponent);
    return JournalReportComponent;
}());

var JournalModel = /** @class */ (function () {
    function JournalModel() {
    }
    return JournalModel;
}());
// Journal
var JournalVoucherModel = /** @class */ (function () {
    function JournalVoucherModel() {
    }
    return JournalVoucherModel;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.html":
/*!*****************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.html ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-card [ngStyle]=\"scrollStyles\">\r\n  <div class=\"main-journal\">\r\n    <mat-card>\r\n      <form [formGroup]=\"ledgerFilterForm\" (ngSubmit)=\"onApplyingFilter(ledgerFilterForm.value)\">\r\n        <div class=\"row\">\r\n          <div class=\"col-md-12\">\r\n                <div class=\"col-md-3\">\r\n                    <lib-search-dropdown\r\n                    placeholder=\"Office\"\r\n                    [multiSelect]=\"true\"\r\n                    placeholderSearchLabel=\"Find Offices...\"\r\n                    noEntriesFoundLabel=\"No matching Accounts found\"\r\n                    [dataSource]=\"officeDropdown\"\r\n                    [selectedValue]=\"OfficeIds\"\r\n                    (openedChange)=\"\r\n                            onOpenedOfficeMultiSelectChange($event)\r\n                          \"\r\n                  >\r\n                  </lib-search-dropdown>\r\n                  <!-- <mat-form-field>\r\n                      <mat-label>Office</mat-label>\r\n                      <mat-select formControlName=\"OfficesList\" multiple>\r\n                          <mat-option *ngFor=\"let office of officeDropdown\" [value]=\"office.OfficeId\">{{office.OfficeName}}</mat-option>\r\n                      </mat-select>\r\n                  </mat-form-field> -->\r\n                </div>\r\n                <div class=\"col-md-3\">\r\n                    <lib-hum-dropdown [options]=\"currencyId$\" formControlName=\"CurrencyId\" [placeHolder]=\"'Currency'\"\r\n                    ></lib-hum-dropdown>\r\n                </div>\r\n                <div class=\"col-md-3\">\r\n                  <lib-hum-dropdown [options]=\"recordType$\" formControlName=\"RecordType\" [placeHolder]=\"'Record Type'\"\r\n                    ></lib-hum-dropdown>\r\n                </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"row\">\r\n          <div class=\"col-md-12\">\r\n              <div class=\"col-md-3\">\r\n                  <lib-search-dropdown\r\n                  placeholder=\"Accounts\"\r\n                  [multiSelect]=\"true\"\r\n                  placeholderSearchLabel=\"Find Accounts...\"\r\n                  noEntriesFoundLabel=\"No matching Accounts found\"\r\n                  [dataSource]=\"accountDropdown\"\r\n                  [selectedValue]=\"AccountIds\"\r\n                  (openedChange)=\"\r\n                          onOpenedAccountMultiSelectChange($event)\r\n                        \"\r\n                >\r\n                </lib-search-dropdown>\r\n                  <!-- <mat-form-field>\r\n                      <mat-label>Accounts</mat-label>\r\n                      <mat-select formControlName=\"accountLists\" multiple>\r\n                          <mat-option *ngFor=\"let account of accountDropdown\" [value]=\"account.AccountCode\">{{account.AccountName}}</mat-option>\r\n                      </mat-select>\r\n                  </mat-form-field> -->\r\n              </div>\r\n              <div class=\"col-md-3\">\r\n                  <mat-form-field>\r\n                    <input matInput [satDatepicker]=\"resultPicker\" formControlName=\"date\">\r\n                    <sat-datepicker\r\n                        #resultPicker\r\n                        [rangeMode]=\"true\">\r\n                    </sat-datepicker>\r\n                    <sat-datepicker-toggle matSuffix [for]=\"resultPicker\"></sat-datepicker-toggle>\r\n                  </mat-form-field>\r\n              </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-md-8\"></div>\r\n          <div class=\"col-md-4\">\r\n            <!-- <button class=\"pull-right\" type=\"submit\" mat-stroked-button color=\"accent\">Update Filters</button> -->\r\n            <hum-button [isSubmit]=\"true\" [type]=\"'save'\" [text]=\"'UPDATE FILTERS'\">\r\n            </hum-button>\r\n            <hum-button (click)=\"ExportPdf(ledgerFilterForm.value)\" [type]=\"'download'\" [text]=\"'PDF EXPORT'\">\r\n            </hum-button>\r\n          </div>\r\n        </div>\r\n      </form>\r\n\r\n      <div class=\"row\">\r\n        <div class=\"col-md-6 text-center\">\r\n          <h3>Opening Balance</h3>\r\n          <p>{{this.openingBalance}}</p>\r\n        </div>\r\n        <div class=\"col-md-6 text-center\">\r\n          <h3>Closing Balance</h3>\r\n          <p>{{this.closingBalance}}</p>\r\n        </div>\r\n      </div>\r\n      <mat-divider></mat-divider>\r\n      <hum-table [headers]=\"ledgerListHeaders$\" [items]=\"ledgerFilterList$\"></hum-table>\r\n    </mat-card>\r\n  </div>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.scss":
/*!*****************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.scss ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL2xlZGdlci1zdGF0ZW1lbnQtcmVwb3J0L2xlZGdlci1zdGF0ZW1lbnQtcmVwb3J0LmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.ts":
/*!***************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.ts ***!
  \***************************************************************************************************/
/*! exports provided: LedgerStatementReportComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LedgerStatementReportComponent", function() { return LedgerStatementReportComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_dashboard_accounting_report_services_report_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/dashboard/accounting/report-services/report.service */ "./src/app/dashboard/accounting/report-services/report.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var rxjs_observable_forkJoin__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/observable/forkJoin */ "./node_modules/rxjs-compat/_esm5/observable/forkJoin.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
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








var LedgerStatementReportComponent = /** @class */ (function () {
    //#endregion
    //#endregion
    function LedgerStatementReportComponent(accountservice, toastr, fb, globalService) {
        this.accountservice = accountservice;
        this.toastr = toastr;
        this.fb = fb;
        this.globalService = globalService;
        this.OpenningBalance = 0.0;
        this.ClosingBalance = 0.0;
        this.OpenningBalanceType = null;
        this.ClosingBalanceType = null;
        this.openingBalance = 0;
        this.closingBalance = 0;
        // loader
        this.ledgerLoading = false;
        this.initialFlag = 0;
        this.menuList = [];
        this.ledgerListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(['Transaction Date', 'Voucher', 'Description', 'Currency', 'Account No', 'Account Name',
            'Debit', 'Credit']);
        // Report
        this.viewPdfFlag = true;
        this.debitSumForReport = 0.0;
        this.creditSumForReport = 0.0;
        this.balanceSumForReport = 0.0;
        this.FromDate = '01/01/' + new Date().getFullYear();
        var currentDate = new Date().getDate();
        var currentMonth = new Date().getMonth() + 1;
        var currentYear = new Date().getFullYear();
        this.recordTypeDropdown = [
            {
                value: 1,
                name: 'Single'
            },
            {
                value: 2,
                name: 'Consolidate'
            }
        ];
        this.recordType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(this.recordTypeDropdown);
        this.defaultRecordType = this.recordTypeDropdown[0].value;
        this.ledgerDateRange = [];
        this.ledgerDateRange.push(new Date(new Date().getFullYear(), 0, 1, new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()));
        this.ledgerDateRange.push(new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()));
        this.getScreenSize();
    }
    LedgerStatementReportComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    LedgerStatementReportComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.initForm();
        this.globalService.setMenuHeaderName('Ledger');
        this.globalService.setMenuList(this.menuList);
        // get all api response
        var currencyRequest = this.getCurrencyCodeListResponse();
        var officeRequest = this.getOfficeCodeListResponse();
        var accountResponse = this.GetAccountDetailsResponse();
        // perform filter when we get all api responses
        Object(rxjs_observable_forkJoin__WEBPACK_IMPORTED_MODULE_3__["forkJoin"])([currencyRequest, officeRequest, accountResponse]).subscribe(function (results) {
            _this.getCurrencyCodeList(results[0]);
            _this.getOfficeCodeList(results[1]);
            _this.GetAccountDetails(results[2]);
            var obj = {
                CurrencyId: _this.selectedCurrency,
                accountLists: _this.selectedAccounts,
                RecordType: _this.defaultRecordType,
                date: { 'begin': new Date(new Date().getFullYear(), 0, 1), 'end': new Date() },
                OfficesList: _this.selectedOffices
            };
            _this.onApplyingFilter(obj);
        }, function (error) {
            _this.toastr.error('Internal server error');
        });
        this.ledgerFilterForm = this.fb.group({
            'OfficesList': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'CurrencyId': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'RecordType': [1, _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'accountLists': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].required],
            'date': [{ 'begin': new Date(new Date().getFullYear(), 0, 1), 'end': new Date() }]
        });
    };
    LedgerStatementReportComponent.prototype.initForm = function () {
        this.ledgerFilter = {
            CurrencyId: 0,
            RecordType: 1,
            accountLists: [],
            FromDate: this.FromDate,
            ToDate: new Date(),
            OfficeIdList: []
        };
    };
    LedgerStatementReportComponent.prototype.getCurrencyCodeListResponse = function () {
        return this.accountservice.GetAllCurrencyCodeList();
    };
    //#endregion
    //#region  "Get Office Code in Add, Edit Dropdown"
    LedgerStatementReportComponent.prototype.getOfficeCodeListResponse = function () {
        return this.accountservice.GetAllOfficeCodeList();
    };
    //#endregion
    //#region  "GetAccountDetails"
    LedgerStatementReportComponent.prototype.GetAccountDetailsResponse = function () {
        return this.accountservice.GetAccountDetails();
    };
    //#endregion
    LedgerStatementReportComponent.prototype.getCurrencyCodeList = function (response) {
        var _this = this;
        // console.log(response);
        if (response.StatusCode === 200 && response.data.CurrencyList != null) {
            this.currencyDropdown = [];
            response.data.CurrencyList.forEach(function (element) {
                _this.currencyDropdown.push(element);
            });
            this.currencyId$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(this.currencyDropdown.map(function (y) {
                return {
                    value: y.CurrencyId,
                    name: y.CurrencyCode + '-' + y.CurrencyName
                };
            }));
            this.selectedCurrency = this.currencyDropdown[0].CurrencyId;
            this.ledgerFilterForm.controls['CurrencyId'].setValue(this.selectedCurrency);
        }
        else {
            this.toastr.error(response.Message);
        }
    };
    LedgerStatementReportComponent.prototype.getOfficeCodeList = function (response) {
        // console.log(response);
        var _this = this;
        if (response.StatusCode === 200 &&
            response.data.OfficeDetailsList != null) {
            this.officeDropdown = [];
            response.data.OfficeDetailsList.forEach(function (element) {
                _this.officeDropdown.push({
                    Id: element.OfficeId,
                    Name: element.OfficeName
                });
            });
            var officeIds = localStorage.getItem('ALLOFFICES') != null
                ? localStorage.getItem('ALLOFFICES').split(',')
                : null;
            // fetch only allowed office
            // officeIds.forEach(x => {
            //   const officeData = this.officeDropdown.filter(
            //     // tslint:disable-next-line:radix
            //     e => e.OfficeId === parseInt(x)
            //   )[0];
            //   this.officeDropdown.push(officeData);
            // });
            this.selectedOffices = [];
            this.officeDropdown.forEach(function (x) {
                // tslint:disable-next-line:radix
                _this.selectedOffices.push(x.Id);
            });
            this.ledgerFilterForm.controls['OfficesList'].setValue(this.selectedOffices);
        }
        else {
            this.toastr.error(response.Message);
        }
    };
    LedgerStatementReportComponent.prototype.GetAccountDetails = function (response) {
        // console.log(response);
        var _this = this;
        this.accountDropdown = [];
        this.selectedAccounts = [];
        if (response.StatusCode === 200 &&
            response.data.AccountDetailList != null) {
            response.data.AccountDetailList = response.data.AccountDetailList.filter(function (x) { return x.AccountLevelId === 4; });
            if (response.data.AccountDetailList.length > 0) {
                response.data.AccountDetailList.forEach(function (element) {
                    _this.accountDropdown.push({
                        Id: element.AccountCode,
                        Name: element.AccountName
                    });
                });
                this.accountDropdown.map(function (x) {
                    _this.selectedAccounts.push(x.Id);
                });
                this.ledgerFilterForm.controls['accountLists'].setValue(this.selectedAccounts);
            }
        }
        else {
            this.toastr.error(response.Message);
        }
    };
    //#region "onApplyingFilter"
    LedgerStatementReportComponent.prototype.onApplyingFilter = function (value) {
        if (this.ledgerDateRange == null) {
            this.toastr.error('Please Select Date Range');
        }
        else {
            var ledgerFilter = {
                CurrencyId: value.CurrencyId,
                accountLists: value.accountLists,
                RecordType: value.RecordType,
                FromDate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
                ToDate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
                OfficeIdList: value.OfficesList
            };
            this.GetLedgerDetails(ledgerFilter);
        }
    };
    //#endregion
    LedgerStatementReportComponent.prototype.onOpenedAccountMultiSelectChange = function (event) {
        this.ledgerFilterForm.controls['accountLists'].setValue(event.Value);
    };
    LedgerStatementReportComponent.prototype.onOpenedOfficeMultiSelectChange = function (event) {
        this.ledgerFilterForm.controls['OfficesList'].setValue(event.Value);
    };
    Object.defineProperty(LedgerStatementReportComponent.prototype, "AccountIds", {
        get: function () {
            return this.ledgerFilterForm.get('accountLists').value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(LedgerStatementReportComponent.prototype, "OfficeIds", {
        get: function () {
            return this.ledgerFilterForm.get('OfficesList').value;
        },
        enumerable: true,
        configurable: true
    });
    //#region "GetLedgerDetails"
    LedgerStatementReportComponent.prototype.GetLedgerDetails = function (journalFilter) {
        var _this = this;
        this.showLedgerLoading();
        this.ledgerReportFinal = []; // report
        this.openingBalance = 0;
        this.closingBalance = 0;
        this.accountservice
            .GetAllLedgerDetails(journalFilter)
            .subscribe(function (data) {
            _this.dataSource = [];
            if (data.StatusCode === 200 &&
                data.data.LedgerList != null &&
                data.data.LedgerList.length > 0) {
                if (data.data.AccountOpendingAndClosingBL != null) {
                    _this.openingBalance =
                        data.data.AccountOpendingAndClosingBL.OpeningBalance != null
                            ? data.data.AccountOpendingAndClosingBL.OpeningBalance
                            : null;
                    _this.closingBalance =
                        data.data.AccountOpendingAndClosingBL.ClosingBalance != null
                            ? data.data.AccountOpendingAndClosingBL.ClosingBalance
                            : null;
                }
                data.data.LedgerList.forEach(function (element) {
                    _this.dataSource.push(element);
                });
                _this.ledgerFilterList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_6__["of"])(_this.dataSource).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_7__["map"])(function (r) { return r.map(function (v) { return ({
                    Transaction_Date: new Date(v.TransactionDate).getDate() + '/' + (new Date(v.TransactionDate).getMonth() + 1) +
                        '/' + new Date(v.TransactionDate).getFullYear(),
                    Voucher: v.VoucherReferenceNo,
                    Description: v.Description,
                    Currency: v.CurrencyName,
                    Account_Code: v.ChartOfAccountNewCode,
                    Account_Name: v.ChartAccountName,
                    DebitAmount: v.DebitAmount,
                    CreditAmount: v.CreditAmount
                }); }); }));
                if (data.StatusCode === 200 &&
                    data.data.ledgerReportFinal != null &&
                    data.data.ledgerReportFinal.length > 0) {
                    _this.ledgerReportFinal = data.data.ledgerReportFinal;
                }
            }
            else if (data.StatusCode === 400) {
                _this.toastr.warning(data.Message);
            }
            _this.hideLedgerLoading();
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
            _this.hideLedgerLoading();
        });
    };
    //#endregion
    LedgerStatementReportComponent.prototype.ExportPdf = function (value) {
        var ledgerFilter = {
            CurrencyId: value.CurrencyId,
            accountLists: value.accountLists,
            RecordType: value.RecordType,
            FromDate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            ToDate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            OfficeIdList: value.OfficesList
        };
        this.accountservice.onExportLedgerPdf(ledgerFilter);
    };
    //#region "export pdf"
    LedgerStatementReportComponent.prototype.printLedgerReport = function () {
        var printContents, popupWin;
        printContents = document.getElementById('print-content-ledger-report')
            .innerHTML;
        popupWin = window.open('', '_blank', '');
        popupWin.document.open();
        popupWin.document.write("\n        <html>\n        <head>\n            <title></title>\n            <style>\n            //........Customized style.......\n            </style>\n        </head>\n        <body onload=\"window.print();window.close()\">" + printContents + "</body>\n        </html>");
        popupWin.document.close();
    };
    //#endregion
    //#region "show/hide"
    LedgerStatementReportComponent.prototype.showLedgerLoading = function () {
        this.ledgerLoading = true;
    };
    LedgerStatementReportComponent.prototype.hideLedgerLoading = function () {
        this.ledgerLoading = false;
    };
    LedgerStatementReportComponent.prototype.showLedgerPdf = function () {
        this.viewPdfFlag = false;
    };
    LedgerStatementReportComponent.prototype.hideLedgerPdf = function () {
        this.viewPdfFlag = true;
    };
    //#endregion
    LedgerStatementReportComponent.prototype.customizeValue = function (data) {
        return parseFloat(data.value).toFixed(4);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], LedgerStatementReportComponent.prototype, "getScreenSize", null);
    LedgerStatementReportComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-ledger-statement-report',
            template: __webpack_require__(/*! ./ledger-statement-report.component.html */ "./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.html"),
            styles: [__webpack_require__(/*! ./ledger-statement-report.component.scss */ "./src/app/dashboard/accounting/ledger-statement-report/ledger-statement-report.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_dashboard_accounting_report_services_report_service__WEBPACK_IMPORTED_MODULE_1__["ReportService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_4__["FormBuilder"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_5__["GlobalSharedService"]])
    ], LedgerStatementReportComponent);
    return LedgerStatementReportComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/report-services/report.service.ts":
/*!************************************************************************!*\
  !*** ./src/app/dashboard/accounting/report-services/report.service.ts ***!
  \************************************************************************/
/*! exports provided: ReportService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ReportService", function() { return ReportService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var ReportService = /** @class */ (function () {
    function ReportService(globalService, globalSharedService, appurl) {
        this.globalService = globalService;
        this.globalSharedService = globalSharedService;
        this.appurl = appurl;
    }
    ReportService.prototype.GetAllCurrencyCodeList = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllCurrency);
    };
    ReportService.prototype.GetAllOfficeCodeList = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllOffice);
    };
    ReportService.prototype.sortDropdown = function (dataSource, fieldName) {
        // Sorted in Asc
        return dataSource.sort(function (x, y) {
            // tslint:disable-next-line:curly
            if (x[fieldName] < y[fieldName])
                return -1;
            // tslint:disable-next-line:curly
            if (x[fieldName] > y[fieldName])
                return 1;
            return 0;
        });
    };
    ReportService.prototype.GetAllJournalDetails = function (journalFilter) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_AccountReports_GetJournalVoucherDetails, journalFilter);
    };
    ReportService.prototype.sumOfListInArray = function (items, prop) {
        if (items == null) {
            return 0;
        }
        return items.reduce(function (a, b) {
            return b[prop] == null ? a : a + b[prop];
        }, 0);
    };
    ReportService.prototype.GetAllJournalCodeList = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Code_GetAllJournalDetail);
    };
    ReportService.prototype.GetAccountDetails = function () {
        return this.globalService.getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Accounting_GetAccountDetails);
    };
    ReportService.prototype.GetAllLedgerDetails = function (journalFilter) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_AccountReports_GetAllLedgerDetails, journalFilter);
    };
    ReportService.prototype.GetAllTrailBalance = function (trialBalanceFilter) {
        return this.globalService.post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_AccountReports_GetTrialBalanceReport, trialBalanceFilter);
    };
    ReportService.prototype.GetProjectList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetAllProjectList)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectDetailModel,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    ReportService.prototype.getProjectJobList = function (projectBudgetIds) {
        return this.globalService
            .post(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetProjectJobsByBudgetLineIds, projectBudgetIds)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectJobDetailModel,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    ReportService.prototype.getProjectBudgetLineList = function (projectIds) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetProjectBudgetLinesByProjectIds, projectIds)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["map"])(function (x) {
            var responseData = {
                data: x.data.ProjectBudgetLineDetailList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    ReportService.prototype.onExportTrialBalancePdf = function (value) {
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Pdf_TrialBalanceReportPdf, value)
            .pipe()
            .subscribe();
    };
    ReportService.prototype.onExportLedgerPdf = function (value) {
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Pdf_LedgerReportPdf, value)
            .pipe()
            .subscribe();
    };
    ReportService.prototype.onExportJournalTrialBalancePdf = function (value) {
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Pdf_JournalTrialBalanceReportPdf, value)
            .pipe()
            .subscribe();
    };
    ReportService.prototype.onExportJournalBudgetlinePdf = function (value) {
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Pdf_GetJournalBudgetLineSummaryPdf, value)
            .pipe()
            .subscribe();
    };
    ReportService.prototype.onExportJournalLedgerPdf = function (value) {
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Pdf_GetJournalLedgerReportPdf, value)
            .pipe()
            .subscribe();
    };
    ReportService.prototype.onExportJournalPdf = function (value) {
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Pdf_GetJournalReportPdf, value)
            .pipe()
            .subscribe();
    };
    ReportService.prototype.onExportJournalExcel = function (value) {
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Pdf_GetJournalReportExcel, value)
            .pipe()
            .subscribe();
    };
    ReportService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_4__["GlobalSharedService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"]])
    ], ReportService);
    return ReportService;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.html":
/*!***********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.html ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-card [ngStyle]=\"scrollStyles\">\r\n    <div class=\"main-journal\">\r\n      <mat-card>\r\n        <form [formGroup]=\"trailbalFilterForm\" (ngSubmit)=\"onApplyingFilter(trailbalFilterForm.value)\">\r\n          <div class=\"row\">\r\n            <div class=\"col-md-12\">\r\n                  <div class=\"col-md-3\">\r\n                      <lib-search-dropdown\r\n                      placeholder=\"Office\"\r\n                      [multiSelect]=\"true\"\r\n                      placeholderSearchLabel=\"Find Offices...\"\r\n                      noEntriesFoundLabel=\"No matching Accounts found\"\r\n                      [dataSource]=\"officeDropdown\"\r\n                      [selectedValue]=\"OfficeIds\"\r\n                      (openedChange)=\"\r\n                              onOpenedOfficeMultiSelectChange($event)\r\n                            \"\r\n                    >\r\n                    </lib-search-dropdown>\r\n                    <!-- <mat-form-field>\r\n                        <mat-label>Office</mat-label>\r\n                        <mat-select formControlName=\"OfficesList\" multiple>\r\n                            <mat-option *ngFor=\"let office of officeDropdown\" [value]=\"office.OfficeId\">{{office.OfficeName}}</mat-option>\r\n                        </mat-select>\r\n                    </mat-form-field> -->\r\n                  </div>\r\n                  <div class=\"col-md-3\">\r\n                      <lib-hum-dropdown [options]=\"currencyId$\" formControlName=\"CurrencyId\" [placeHolder]=\"'Currency'\"\r\n                      ></lib-hum-dropdown>\r\n                  </div>\r\n                  <div class=\"col-md-3\">\r\n                    <lib-hum-dropdown [options]=\"recordType$\" formControlName=\"RecordType\" [placeHolder]=\"'Record Type'\"\r\n                      ></lib-hum-dropdown>\r\n                  </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-md-12\">\r\n                <div class=\"col-md-3\">\r\n                    <lib-search-dropdown\r\n                    placeholder=\"Accounts\"\r\n                    [multiSelect]=\"true\"\r\n                    placeholderSearchLabel=\"Find Accounts...\"\r\n                    noEntriesFoundLabel=\"No matching Accounts found\"\r\n                    [dataSource]=\"accountDropdown\"\r\n                    [selectedValue]=\"AccountIds\"\r\n                    (openedChange)=\"\r\n                            onOpenedAccountMultiSelectChange($event)\r\n                          \"\r\n                  >\r\n                  </lib-search-dropdown>\r\n                    <!-- <mat-form-field>\r\n                        <mat-label>Accounts</mat-label>\r\n                        <mat-select formControlName=\"accountLists\" multiple>\r\n                            <mat-option *ngFor=\"let account of accountDropdown\" [value]=\"account.AccountCode\">{{account.AccountName}}</mat-option>\r\n                        </mat-select>\r\n                    </mat-form-field> -->\r\n                </div>\r\n                <div class=\"col-md-3\">\r\n                    <mat-form-field>\r\n                      <input matInput [satDatepicker]=\"resultPicker\" formControlName=\"date\">\r\n                      <sat-datepicker\r\n                          #resultPicker\r\n                          [rangeMode]=\"true\">\r\n                      </sat-datepicker>\r\n                      <sat-datepicker-toggle matSuffix [for]=\"resultPicker\"></sat-datepicker-toggle>\r\n                    </mat-form-field>\r\n                </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-md-8\"></div>\r\n            <div class=\"col-md-4\">\r\n                <!-- <button class=\"pull-right\" mat-stroked-button color=\"accent\"><mat-icon aria-hidden=\"false\" aria-label=\"Example home icon\">\r\n                    vertical_align_bottom </mat-icon\r\n                  >Pdf Export</button> -->\r\n                <!-- <button class=\"pull-right\" type=\"submit\" mat-stroked-button color=\"accent\" style=\"margin-right: 10px\">Update Filters</button> -->\r\n                <hum-button [isSubmit]=\"true\" [type]=\"'save'\" [text]=\"'UPDATE FILTERS'\">\r\n                  </hum-button>\r\n                <hum-button (click)=\"ExportPdf(trailbalFilterForm.value)\" [isSubmit]=\"true\" [type]=\"'download'\" [text]=\"'PDF EXPORT'\">\r\n                  </hum-button>\r\n              </div>\r\n          </div>\r\n        </form><br>\r\n        <mat-divider></mat-divider>\r\n        <hum-table [headers]=\"trialListHeaders$\" [items]=\"trialbalFilterList$\"></hum-table>\r\n        <div *ngIf=\"(trialbalFilterList$ | async)\">\r\n            <h5 class=\"display_inline_block\">Total Debits: </h5><strong>{{this.debitSumForReport}}</strong><br>\r\n            <h5 class=\"display_inline_block\">Total Credits: </h5><strong>{{this.creditSumForReport}}</strong>\r\n          </div>\r\n          <!-- <mat-paginator\r\n            [length]=\"totalCount\"\r\n            [pageSize]=\"pageSize\"\r\n            [pageIndex]=\"pageIndex\"\r\n            [pageSizeOptions]=\"[5, 10, 25, 100]\"\r\n            (page)=\"pageEvent($event)\"\r\n          >\r\n          </mat-paginator> -->\r\n      </mat-card>\r\n    </div>\r\n  </mat-card>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.scss":
/*!***********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.scss ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL3RyaWFsLWJhbGFuY2UtcmVwb3J0L3RyaWFsLWJhbGFuY2UtcmVwb3J0LmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.ts":
/*!*********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.ts ***!
  \*********************************************************************************************/
/*! exports provided: TrialBalanceReportComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TrialBalanceReportComponent", function() { return TrialBalanceReportComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _report_services_report_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../report-services/report.service */ "./src/app/dashboard/accounting/report-services/report.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
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







var TrialBalanceFilter = /** @class */ (function () {
    function TrialBalanceFilter() {
    }
    return TrialBalanceFilter;
}());
var TrialBalanceReportComponent = /** @class */ (function () {
    function TrialBalanceReportComponent(accountservice, toastr, fb, globalService) {
        this.accountservice = accountservice;
        this.toastr = toastr;
        this.fb = fb;
        this.globalService = globalService;
        this.trialBalanceLoader = false;
        this.intialFlagValue = 0;
        this.menuList = [];
        // Report
        this.viewPdfFlag = true;
        this.debitSumForReport = 0.0;
        this.creditSumForReport = 0.0;
        this.balanceSumForReport = 0.0;
        this.trialListHeaders$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_5__["of"])(['Account', 'Account Name', 'Description', 'Currency',
            'Debit', 'Credit']);
        this.totalCount = 0;
        this.pageSize = 10;
        this.FromDate = '01/01/' + new Date().getFullYear();
        this.trailFilters = {
            CurrencyId: 1,
            OfficesList: [],
            RecordType: 1,
            fromdate: this.FromDate,
            // todate: new Date(),
            accountLists: [],
            date: { 'begin': new Date(new Date().getFullYear(), 0, 1), 'end': new Date() }
        };
        this.recordTypeDropdown = [
            {
                value: 1,
                name: 'Single'
            },
            {
                value: 2,
                name: 'Consolidate'
            }
        ];
        this.recordType$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_5__["of"])(this.recordTypeDropdown);
        this.defaultRecordType = this.recordTypeDropdown[0].Id;
        this.trialBalanceDateRange = [];
        this.trialBalanceDateRange.push(new Date(new Date().getFullYear(), 0, 1, new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()));
        this.trialBalanceDateRange.push(new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()));
        this.getScreenSize();
    }
    TrialBalanceReportComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    TrialBalanceReportComponent.prototype.ngOnInit = function () {
        this.getCurrencyCodeList();
        this.getOfficeCodeList();
        this.GetAccountDetails();
        this.globalService.setMenuHeaderName('Trial Balance');
        this.globalService.setMenuList(this.menuList);
        this.trailbalFilterForm = this.fb.group({
            'OfficesList': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'CurrencyId': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'RecordType': [1, _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'accountLists': ['', _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required],
            'date': [{ 'begin': new Date(new Date().getFullYear(), 0, 1), 'end': new Date() }]
        });
    };
    //#region "onApplyingFilter"
    TrialBalanceReportComponent.prototype.onApplyingFilter = function (value) {
        if (this.FromDate == null) {
            this.toastr.error('Please Select Date');
        }
        else {
            TrialBalanceFilter.trialBalanceFilter = {
                CurrencyId: value.CurrencyId,
                accountLists: value.accountLists,
                RecordType: value.RecordType,
                OfficesList: value.OfficesList,
                fromdate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
                todate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            };
            this.GetAllTrailBalance(TrialBalanceFilter.trialBalanceFilter);
        }
    };
    TrialBalanceReportComponent.prototype.pageEvent = function (e) {
        this.pageIndex = e.pageIndex;
        this.pageSize = e.pageSize;
        // this.voucherFilter.totalCount =  e.length;
        // this.getVoucherList();
    };
    //#endregion
    TrialBalanceReportComponent.prototype.onOpenedAccountMultiSelectChange = function (event) {
        this.trailbalFilterForm.controls['accountLists'].setValue(event.Value);
    };
    TrialBalanceReportComponent.prototype.onOpenedOfficeMultiSelectChange = function (event) {
        this.trailbalFilterForm.controls['OfficesList'].setValue(event.Value);
    };
    //#region  "getCurrencyCodeList"
    TrialBalanceReportComponent.prototype.getCurrencyCodeList = function () {
        var _this = this;
        this.accountservice
            .GetAllCurrencyCodeList()
            .subscribe(function (data) {
            _this.currencyDropdown = [];
            if (data.data.CurrencyList != null) {
                data.data.CurrencyList.forEach(function (element) {
                    _this.currencyDropdown.push(element);
                });
                _this.currencyId$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_5__["of"])(_this.currencyDropdown.map(function (y) {
                    return {
                        value: y.CurrencyId,
                        name: y.CurrencyCode + '-' + y.CurrencyName
                    };
                }));
                _this.selectedCurrency = _this.currencyDropdown[0].CurrencyId;
                _this.trailbalFilterForm.controls['CurrencyId'].setValue(_this.selectedCurrency);
                _this.intialFlagValue += 1;
            }
        }, function (error) { });
    };
    //#endregion
    //#region  Get Office Code in Add, Edit Dropdown
    TrialBalanceReportComponent.prototype.getOfficeCodeList = function () {
        var _this = this;
        this.accountservice
            .GetAllOfficeCodeList()
            .subscribe(function (data) {
            if (data.data.OfficeDetailsList != null) {
                _this.officeDropdown = [];
                // const allOffices = [];
                var officeIds = localStorage.getItem('ALLOFFICES') != null
                    ? localStorage.getItem('ALLOFFICES').split(',')
                    : null;
                data.data.OfficeDetailsList.forEach(function (element) {
                    _this.officeDropdown.push({
                        Id: element.OfficeId,
                        Name: element.OfficeName
                    });
                });
                officeIds.forEach(function (x) {
                    var officeData = _this.officeDropdown.filter(
                    // tslint:disable-next-line:radix
                    function (e) { return e.OfficeId === parseInt(x); })[0];
                    // this.officeDropdown.push(officeData);
                });
                _this.selectedOffices = [];
                _this.officeDropdown.forEach(function (x) {
                    // tslint:disable-next-line:radix
                    _this.selectedOffices.push(x.Id);
                });
                _this.trailbalFilterForm.controls['OfficesList'].setValue(_this.selectedOffices);
                _this.intialFlagValue += 1;
                _this.officeDropdown = _this.accountservice.sortDropdown(_this.officeDropdown, 'OfficeName');
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
    //#region  "GetAccountDetails"
    TrialBalanceReportComponent.prototype.GetAccountDetails = function () {
        var _this = this;
        this.accountservice
            .GetAccountDetails()
            .subscribe(function (data) {
            _this.accountDropdown = [];
            _this.selectedAccounts = [];
            if (data.StatusCode === 200) {
                data.data.AccountDetailList = data.data.AccountDetailList.filter(function (x) { return x.AccountLevelId === 4; });
                data.data.AccountDetailList.forEach(function (element) {
                    _this.accountDropdown.push({
                        Id: element.AccountCode,
                        Name: element.AccountName
                    });
                });
                data.data.AccountDetailList.forEach(function (e) {
                    _this.selectedAccounts.push(e.AccountCode);
                });
                _this.trailbalFilterForm.controls['accountLists'].setValue(_this.selectedAccounts);
                _this.intialFlagValue += 1;
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
            else {
            }
        });
    };
    Object.defineProperty(TrialBalanceReportComponent.prototype, "AccountIds", {
        get: function () {
            return this.trailbalFilterForm.get('accountLists').value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(TrialBalanceReportComponent.prototype, "OfficeIds", {
        get: function () {
            return this.trailbalFilterForm.get('OfficesList').value;
        },
        enumerable: true,
        configurable: true
    });
    //#endregion
    //#region  "GetAllTrailBalance"
    TrialBalanceReportComponent.prototype.GetAllTrailBalance = function (trialBalanceFilter) {
        var _this = this;
        this.showHideTrialBalanceLoader(true);
        this.accountservice
            .GetAllTrailBalance(trialBalanceFilter)
            .subscribe(function (data) {
            _this.trailBalanceData = [];
            if (data.StatusCode === 200 &&
                data.data.TrialBalanceList != null &&
                data.data.TrialBalanceList.length > 0) {
                data.data.TrialBalanceList.forEach(function (element) {
                    _this.trailBalanceData.push(element);
                });
                _this.trialbalFilterList$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_5__["of"])(_this.trailBalanceData).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_6__["map"])(function (r) { return r.map(function (v) { return ({
                    Account: v.ChartOfAccountNewCode,
                    AccountName: v.ChartOfAccountNewCode + '-' + v.AccountName,
                    Description: v.Description,
                    Currency: v.CurrencyName,
                    Debit: v.DebitAmount,
                    Credit: v.CreditAmount
                }); }); }));
                _this.debitSumForReport = _this.accountservice.sumOfListInArray(_this.trailBalanceData, 'DebitAmount');
                _this.creditSumForReport = _this.accountservice.sumOfListInArray(_this.trailBalanceData, 'CreditAmount');
                _this.balanceSumForReport =
                    _this.debitSumForReport - _this.creditSumForReport;
            }
            else if (data.StatusCode === 400) {
                _this.toastr.warning(data.Message);
            }
            _this.showHideTrialBalanceLoader(false);
            // console.log(this.trialbalFilterList$);
        }, function (error) {
            _this.showHideTrialBalanceLoader(false);
        });
    };
    //#endregion
    //#region "export pdf"
    TrialBalanceReportComponent.prototype.printTrialBalanceReport = function () {
        var printContents, popupWin;
        printContents = document.getElementById('print-content-trial-balance-report').innerHTML;
        popupWin = window.open('', '_blank', '');
        popupWin.document.open();
        popupWin.document.write("\n            <html>\n            <head>\n                <title></title>\n                <style>\n                //........Customized style.......\n                </style>\n            </head>\n            <body onload=\"window.print();window.close()\">" + printContents + "</body>\n            </html>");
        popupWin.document.close();
    };
    //#endregion
    //#region "show/hide"
    TrialBalanceReportComponent.prototype.showHideTrialBalanceLoader = function (flag) {
        this.trialBalanceLoader = flag;
    };
    TrialBalanceReportComponent.prototype.showTrialBalancePdf = function () {
        this.viewPdfFlag = false;
    };
    TrialBalanceReportComponent.prototype.hideTrialBalancePdf = function () {
        this.viewPdfFlag = true;
    };
    //#endregion
    TrialBalanceReportComponent.prototype.ExportPdf = function (value) {
        TrialBalanceFilter.trialBalanceFilter = {
            CurrencyId: value.CurrencyId,
            accountLists: value.accountLists,
            RecordType: value.RecordType,
            OfficesList: value.OfficesList,
            fromdate: new Date(new Date(value.date.begin).getFullYear(), new Date(value.date.begin).getMonth(), new Date(value.date.begin).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
            todate: new Date(new Date(value.date.end).getFullYear(), new Date(value.date.end).getMonth(), new Date(value.date.end).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()),
        };
        this.accountservice.onExportTrialBalancePdf(TrialBalanceFilter.trialBalanceFilter);
    };
    TrialBalanceReportComponent.prototype.customizeValue = function (data) {
        return parseFloat(data.value).toFixed(4);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], TrialBalanceReportComponent.prototype, "getScreenSize", null);
    TrialBalanceReportComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-trial-balance-report',
            template: __webpack_require__(/*! ./trial-balance-report.component.html */ "./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.html"),
            styles: [__webpack_require__(/*! ./trial-balance-report.component.scss */ "./src/app/dashboard/accounting/trial-balance-report/trial-balance-report.component.scss")]
        }),
        __metadata("design:paramtypes", [_report_services_report_service__WEBPACK_IMPORTED_MODULE_1__["ReportService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormBuilder"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_4__["GlobalSharedService"]])
    ], TrialBalanceReportComponent);
    return TrialBalanceReportComponent;
}());

var TrialBalanceFilterModel = /** @class */ (function () {
    function TrialBalanceFilterModel() {
    }
    return TrialBalanceFilterModel;
}());


/***/ })

}]);
//# sourceMappingURL=accounting-accounting-module.js.map