(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["default~configuration-configuration-module~dashboard-dashboard-module~hr-hr-module~store-store-modul~b01bbfdc"],{

/***/ "./src/app/shared/dbfooter/dbfooter.component.html":
/*!*********************************************************!*\
  !*** ./src/app/shared/dbfooter/dbfooter.component.html ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>\r\n  dbfooter works!\r\n</p>\r\n"

/***/ }),

/***/ "./src/app/shared/dbfooter/dbfooter.component.scss":
/*!*********************************************************!*\
  !*** ./src/app/shared/dbfooter/dbfooter.component.scss ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3NoYXJlZC9kYmZvb3Rlci9kYmZvb3Rlci5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/shared/dbfooter/dbfooter.component.ts":
/*!*******************************************************!*\
  !*** ./src/app/shared/dbfooter/dbfooter.component.ts ***!
  \*******************************************************/
/*! exports provided: DbfooterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DbfooterComponent", function() { return DbfooterComponent; });
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

var DbfooterComponent = /** @class */ (function () {
    function DbfooterComponent() {
    }
    DbfooterComponent.prototype.ngOnInit = function () {
    };
    DbfooterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-dbfooter',
            template: __webpack_require__(/*! ./dbfooter.component.html */ "./src/app/shared/dbfooter/dbfooter.component.html"),
            styles: [__webpack_require__(/*! ./dbfooter.component.scss */ "./src/app/shared/dbfooter/dbfooter.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], DbfooterComponent);
    return DbfooterComponent;
}());



/***/ }),

/***/ "./src/app/shared/dbheader/dbheader.component.html":
/*!*********************************************************!*\
  !*** ./src/app/shared/dbheader/dbheader.component.html ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"db-header-main\">\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-10\">\r\n      <div class=\"top-header\">\r\n        <a\r\n          *ngIf=\"currentRoute.match('/my-project/')\"\r\n          routerLink=\"/project/my-projects\"\r\n          class=\"back_arrow\"\r\n        >\r\n          <i class=\"fas fa-arrow-left\"></i>\r\n        </a>\r\n        <h3>{{ headerName | uppercase }}</h3>\r\n        <nav class=\"navbar navbar-default\">\r\n          <div class=\"navbar-header\">\r\n            <span\r\n              type=\"button\"\r\n              class=\"navbar-toggle collapsed\"\r\n              data-toggle=\"collapse\"\r\n              data-target=\"#navbar\"\r\n              aria-expanded=\"false\"\r\n              aria-controls=\"navbar\"\r\n            >\r\n              <i class=\"material-icons\"> more_vert </i>\r\n            </span>\r\n          </div>\r\n        </nav>\r\n\r\n        <div id=\"navbar\" class=\"navbar-collapse collapse\">\r\n          <ul>\r\n            <!-- <li>\r\n              <a routerLink=\"/file-upload-demo\" routerLinkActive=\"active\">\r\n                file-upload-demo\r\n              </a>\r\n            </li> -->\r\n            <li *ngFor=\"let item of menuList\">\r\n              <a [routerLink]=\"[item.Link]\" routerLinkActive=\"active\">{{\r\n                item.Text\r\n              }}</a>\r\n            </li>\r\n          </ul>\r\n        </div>\r\n      </div>\r\n    </div>\r\n    <div class=\"col-sm-2 text-right\">\r\n      <button mat-button (click)=\"onTrainingClick()\">\r\n        <i class=\"material-icons\">link</i>\r\n        Training\r\n      </button>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/shared/dbheader/dbheader.component.scss":
/*!*********************************************************!*\
  !*** ./src/app/shared/dbheader/dbheader.component.scss ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "li.active > a {\n  color: #4cc9ff;\n  border-bottom: 2px solid #4cc9ff;\n  padding-bottom: 10px;\n  text-decoration: none; }\n\nli.active > a:hover {\n  color: #151b26;\n  font-weight: 400;\n  padding-bottom: 10px;\n  border-bottom: 2px solid #151b26; }\n\na.active {\n  color: #4cc9ff;\n  border-bottom: 2px solid #4cc9ff;\n  padding-bottom: 10px;\n  text-decoration: none; }\n\n.top-header h3 {\n  font-size: 18px;\n  margin: 0;\n  font-weight: 500;\n  z-index: 1; }\n\n.back_arrow {\n  color: #212121;\n  font-size: 14px;\n  float: left;\n  padding-right: 20px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc2hhcmVkL2RiaGVhZGVyL2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXHNoYXJlZFxcZGJoZWFkZXJcXGRiaGVhZGVyLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0UsY0FBYztFQUNkLGdDQUFnQztFQUNoQyxvQkFBb0I7RUFDcEIscUJBQXFCLEVBQUE7O0FBRXZCO0VBQ0UsY0FBYztFQUNkLGdCQUFnQjtFQUNoQixvQkFBb0I7RUFDcEIsZ0NBQStCLEVBQUE7O0FBR2pDO0VBQ0UsY0FBYztFQUNkLGdDQUFnQztFQUNoQyxvQkFBb0I7RUFDcEIscUJBQXFCLEVBQUE7O0FBRXZCO0VBQ0UsZUFBZTtFQUNmLFNBQVM7RUFDVCxnQkFBZ0I7RUFDaEIsVUFBVSxFQUFBOztBQUVaO0VBQ0UsY0FBYztFQUNkLGVBQWU7RUFDZixXQUFXO0VBQ1gsbUJBQW1CLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9zaGFyZWQvZGJoZWFkZXIvZGJoZWFkZXIuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyJsaS5hY3RpdmUgPiBhIHtcclxuICBjb2xvcjogIzRjYzlmZjtcclxuICBib3JkZXItYm90dG9tOiAycHggc29saWQgIzRjYzlmZjtcclxuICBwYWRkaW5nLWJvdHRvbTogMTBweDtcclxuICB0ZXh0LWRlY29yYXRpb246IG5vbmU7XHJcbn1cclxubGkuYWN0aXZlID4gYTpob3ZlciB7XHJcbiAgY29sb3I6ICMxNTFiMjY7XHJcbiAgZm9udC13ZWlnaHQ6IDQwMDtcclxuICBwYWRkaW5nLWJvdHRvbTogMTBweDtcclxuICBib3JkZXItYm90dG9tOjJweCBzb2xpZCAjMTUxYjI2O1xyXG59XHJcblxyXG5hLmFjdGl2ZSB7XHJcbiAgY29sb3I6ICM0Y2M5ZmY7XHJcbiAgYm9yZGVyLWJvdHRvbTogMnB4IHNvbGlkICM0Y2M5ZmY7XHJcbiAgcGFkZGluZy1ib3R0b206IDEwcHg7XHJcbiAgdGV4dC1kZWNvcmF0aW9uOiBub25lO1xyXG59XHJcbi50b3AtaGVhZGVyIGgzIHtcclxuICBmb250LXNpemU6IDE4cHg7XHJcbiAgbWFyZ2luOiAwO1xyXG4gIGZvbnQtd2VpZ2h0OiA1MDA7XHJcbiAgei1pbmRleDogMTtcclxufVxyXG4uYmFja19hcnJvdyB7XHJcbiAgY29sb3I6ICMyMTIxMjE7XHJcbiAgZm9udC1zaXplOiAxNHB4O1xyXG4gIGZsb2F0OiBsZWZ0O1xyXG4gIHBhZGRpbmctcmlnaHQ6IDIwcHg7XHJcbn1cclxuIl19 */"

/***/ }),

/***/ "./src/app/shared/dbheader/dbheader.component.ts":
/*!*******************************************************!*\
  !*** ./src/app/shared/dbheader/dbheader.component.ts ***!
  \*******************************************************/
/*! exports provided: DbheaderComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DbheaderComponent", function() { return DbheaderComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_global_shared_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var DbheaderComponent = /** @class */ (function () {
    //#endregion
    function DbheaderComponent(router, globalSharedService) {
        var _this = this;
        this.router = router;
        this.globalSharedService = globalSharedService;
        //#region "variables"
        this.menuList = [];
        this.headerName = ' ';
        this.currentRoute = '';
        // Get Menu Header
        this.globalSharedService.getMenuHeaderName().subscribe(function (res) {
            _this.headerName = res;
        });
        // Get Menu List
        this.globalSharedService.getMenuList().subscribe(function (res) {
            _this.menuList = res;
        });
    }
    DbheaderComponent.prototype.ngOnInit = function () {
        var _this = this;
        // First time set
        this.currentRoute = this.router.url;
        this.routerEventSubscription = this.router.events.subscribe(function (r) {
            if (r instanceof _angular_router__WEBPACK_IMPORTED_MODULE_1__["NavigationEnd"]) {
                _this.currentRoute = r.url;
            }
        });
    };
    //#region "onTrainingClick"
    DbheaderComponent.prototype.onTrainingClick = function () {
        this.trainingSubscription = this.globalSharedService
            .GetTrainingLink()
            .subscribe(function (response) {
            if (response.statusCode === 200 && response.data != null) {
                window.open(response.data, '_blank');
            }
            else if (response.statusCode === 200) {
                // console.log(response.message);
            }
        }, function (error) { });
    };
    //#endregion
    DbheaderComponent.prototype.goToRoute = function (route) {
        this.router.navigateByUrl(route);
    };
    //#region "ngOnDestroy"
    DbheaderComponent.prototype.ngOnDestroy = function () {
        if (this.trainingSubscription && !this.trainingSubscription.closed) {
            this.trainingSubscription.unsubscribe();
        }
        if (this.routerEventSubscription && !this.routerEventSubscription.closed) {
            this.routerEventSubscription.unsubscribe();
        }
    };
    DbheaderComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-dbheader',
            template: __webpack_require__(/*! ./dbheader.component.html */ "./src/app/shared/dbheader/dbheader.component.html"),
            styles: [__webpack_require__(/*! ./dbheader.component.scss */ "./src/app/shared/dbheader/dbheader.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"],
            _services_global_shared_service__WEBPACK_IMPORTED_MODULE_2__["GlobalSharedService"]])
    ], DbheaderComponent);
    return DbheaderComponent;
}());



/***/ }),

/***/ "./src/app/shared/dbsidebar/dbsidebar.component.html":
/*!***********************************************************!*\
  !*** ./src/app/shared/dbsidebar/dbsidebar.component.html ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"nav-side-menu\">\r\n  <!-- <div class=\"icon-wrap\">\r\n    <div class=\"icon\">\r\n      <div class=\"bar\"></div>\r\n\r\n    </div>\r\n  </div> -->\r\n\r\n  <div class=\"brand\"> <i class=\"fa fa-universal-access\"></i>Humanitarian</div>\r\n  <!-- <i class=\"fa fa-bars fa-2x toggle-btn\" data-toggle=\"collapse\" data-target=\"#menu-content\"></i> -->\r\n  <div class=\"menu-list\">\r\n\r\n    <ul id=\"menu-content\" class=\"menu-content collapse \" >\r\n      <li [ngClass]=\"selectedLink == 1 ? 'active' : ''\" (click)=\"onsideBarLinkClicked('project/my-projects', 1)\" *ngIf=\"displayChildModule(ModulesProject.Project)\">\r\n        <a class=\"projects\">Projects</a>\r\n      </li>\r\n\r\n      <li data-toggle=\"collapse\" data-target=\"#products\" class=\"collapsed\" *ngIf=\"displayChildModule(ModulesAccounting.ChartOfAccount) || displayChildModule(ModulesAccounting.FinancialReport) || displayChildModule(ModulesAccounting.Vouchers) || displayChildModule(ModulesAccounting.ExchangeRates) || displayChildModule(ModulesAccounting.GainLossReport)\r\n    || displayChildModule(ModulesAccounting.Journal) || displayChildModule(ModulesAccounting.Ledger) || displayChildModule(ModulesAccounting.TrialBalance) || displayChildModule(ModulesAccounting.PensionPayments) || displayChildModule(ModulesAccounting.VoucherSummaryReport)\">\r\n        <a>Accounting<span class=\"arrow\"></span></a>\r\n      </li>\r\n\r\n      <ul class=\"sub-menu collapse\" id=\"products\">\r\n\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 2 }\" *ngIf=\"displayChildModule(ModulesAccounting.Vouchers)\">\r\n          <a routerLink=\"/accounting/vouchers\">Vouchers</a>\r\n        </li> -->\r\n        <li [ngClass]=\"{'active': selectedLink == 2 }\" (click)=\"onsideBarLinkClicked('/accounting/vouchers', 2)\" *ngIf=\"displayChildModule(ModulesAccounting.Vouchers)\"><a>Vouchers</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 3 }\" (click)=\"onsideBarLinkClicked('/accounting/chart-of-accounts', 3)\" *ngIf=\"displayChildModule(ModulesAccounting.ChartOfAccount)\"><a>Charts of Account</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 4 }\" (click)=\"onsideBarLinkClicked('/accounting/financial-report', 4)\"  *ngIf=\"displayChildModule(ModulesAccounting.FinancialReport)\"><a>Financial Report</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 5 }\" (click)=\"onsideBarLinkClicked('/accounting/gain-loss-report', 5)\" *ngIf=\"displayChildModule(ModulesAccounting.GainLossReport)\"><a>Gain/Loss Report</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink ==63 }\" (click)=\"onsideBarLinkClicked('/accounting/exchange-gain-loss-report', 63)\" *ngIf=\"displayChildModule(ModulesAccounting.GainLossReport)\"><a>Gain/Loss Report(Beta)</a></li>\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 59 }\" (click)=\"onsideBarLinkClicked('/accounting/voucher-summary-report', 59)\" *ngIf=\"displayChildModule(ModulesAccounting.VoucherSummaryReport)\"><a>Voucher Summary Report</a></li> -->\r\n        <li [ngClass]=\"{'active': selectedLink == 17 }\" (click)=\"onsideBarLinkClicked('/accounting/exchange-rate', 17)\" *ngIf=\"displayChildModule(ModulesAccounting.ExchangeRates)\"><a>Exchange Rates</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 18 }\" (click)=\"onsideBarLinkClicked('/accounting/journal-report', 18)\" *ngIf=\"displayChildModule(ModulesAccounting.Journal)\"><a>Journal</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 19 }\" (click)=\"onsideBarLinkClicked('/accounting/ledger-report', 19)\" *ngIf=\"displayChildModule(ModulesAccounting.Ledger)\"><a>Ledger</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 20 }\" (click)=\"onsideBarLinkClicked('/accounting/trial-balance', 20)\" *ngIf=\"displayChildModule(ModulesAccounting.TrialBalance)\"><a>Trial Balance</a></li>\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 18 }\" (click)=\"onOldUiPageClick('dashboard/accounts/journal', 18)\" *ngIf=\"displayChildModule(ModulesAccounting.Journal)\"><a>Journal</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 19 }\" (click)=\"onOldUiPageClick('dashboard/accounts/ledger', 19)\" *ngIf=\"displayChildModule(ModulesAccounting.Ledger)\"><a>Ledger</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 20 }\" (click)=\"onOldUiPageClick('dashboard/accounts/trialbalance', 20)\" *ngIf=\"displayChildModule(ModulesAccounting.TrialBalance)\"><a>Trial Balance</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 21 }\" (click)=\"onOldUiPageClick('dashboard/accounts/pension-payments', 21)\" *ngIf=\"displayChildModule(ModulesAccounting.PensionPayments)\"><a>Pension Payments</a></li> -->\r\n\r\n      </ul>\r\n      <!-- Marketing Menus -->\r\n      <!-- <li data-toggle=\"collapse\" data-target=\"#service\" class=\"collapsed\" *ngIf=\"displayChildModule(ModulesMarketing.ModulesMarketingMaster)\">\r\n        <a> Marketing <span class=\"arrow\"></span></a>\r\n      </li>\r\n      <ul class=\"sub-menu collapse\" id=\"service\">\r\n        <li [ngClass]=\"{'active': selectedLink == 6 }\" (click)=\"onsideBarLinkClicked('/marketing', 6)\" *ngIf=\"displayChildModule(ModulesMarketing.Marketing)\"><a>Dashboard</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 7 }\" (click)=\"onsideBarLinkClicked('/marketing/activityType', 7)\" *ngIf=\"displayModulePages(Marketing.ActivityType)\"><a routerLinkActive=\"active-link\">Activity Type</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 18 }\" (click)=\"onsideBarLinkClicked('/marketing/channel', 18)\" *ngIf=\"displayModulePages(Marketing.Channel)\"><a>Channel</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 8 }\" (click)=\"onsideBarLinkClicked('/marketing/media-category', 8)\" *ngIf=\"displayModulePages(Marketing.MediaCategory)\"><a>Media Category</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 9 }\" (click)=\"onsideBarLinkClicked('/marketing/medium', 9)\" *ngIf=\"displayModulePages(Marketing.Medium)\"><a>Medium</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 10 }\" (click)=\"onsideBarLinkClicked('/marketing/nature', 10)\" *ngIf=\"displayModulePages(Marketing.Nature)\"><a>Nature</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 11 }\" (click)=\"onsideBarLinkClicked('/marketing/phase', 11)\" *ngIf=\"displayModulePages(Marketing.Phase)\"><a>Phase</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink ==  16 }\" (click)=\"onsideBarLinkClicked('/marketing/producer', 16)\" *ngIf=\"displayModulePages(Marketing.Producer)\"><a>Producer</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 12 }\" (click)=\"onsideBarLinkClicked('/marketing/quality', 12)\" *ngIf=\"displayModulePages(Marketing.Quality)\"><a>Quality</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 13 }\" (click)=\"onsideBarLinkClicked('/marketing/time-category', 13)\" *ngIf=\"displayModulePages(Marketing.TimeCategory)\"><a>Time Category</a></li>\r\n      </ul> -->\r\n\r\n      <!-- <li data-toggle=\"collapse\" data-target=\"#policyservice\" class=\"collapsed\" *ngIf=\"displayChildModule(ModulesMarketing.ModulesMarketingMaster)\">\r\n        <a> Policy Master <span class=\"arrow\"></span></a>\r\n      </li> -->\r\n      <!-- <ul class=\"sub-menu collapse\" id=\"policyservice\">\r\n        <li [ngClass]=\"{'active': selectedLink == 14 }\" (click)=\"onsideBarLinkClicked('/marketing/media-category', 14)\" *ngIf=\"displayModulePages(Marketing.MediaCategory)\"><a routerLinkActive=\"active-link\">Media Category</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 15 }\" (click)=\"onsideBarLinkClicked('/marketing/medium', 15)\" *ngIf=\"displayModulePages(Marketing.Medium)\"><a>Medium</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink ==  16 }\" (click)=\"onsideBarLinkClicked('/marketing/producer', 16)\" *ngIf=\"displayModulePages(Marketing.Producer)\"><a>Producer</a></li>\r\n      </ul> -->\r\n\r\n      <li (click)=\"onOldUiPageClick('dashboard/user/users', 58)\" *ngIf=\"displayModulePages(UserModule)\" > <a>Users</a>\r\n      </li>\r\n      <li data-toggle=\"collapse\" data-target=\"#configuration\" class=\"collapsed\" *ngIf=\"displayChildModule(ModulesConfigurationPages.ConfigurationPages)\">\r\n        <a> Configuration Pages <span class=\"arrow\"></span></a>\r\n      </li>\r\n      <ul class=\"sub-menu collapse\" id=\"configuration\">\r\n        <li [ngClass]=\"{'active': selectedLink == 22 }\" (click)=\"onOldUiPageClick('dashboard/code/journal-code', 22)\" *ngIf=\"displayModulePages(ConfigurationPages.JournalCodes)\"><a routerLinkActive=\"active-link\">Journal Codes</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 23 }\" (click)=\"onOldUiPageClick('dashboard/code/currency-code', 23)\" *ngIf=\"displayModulePages(ConfigurationPages.CurrencyCodes)\"><a>Currency Codes</a></li>\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  24 }\" (click)=\"onOldUiPageClick('dashboard/code/office-code', 24)\" *ngIf=\"displayModulePages(ConfigurationPages.OfficeCodes)\"><a>Office Codes</a></li> -->\r\n        <li [ngClass]=\"{'active': selectedLink ==  25 }\" (click)=\"onOldUiPageClick('dashboard/code/financial-year', 25)\" *ngIf=\"displayModulePages(ConfigurationPages.FinancialYear)\"><a>Financial Year</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink ==  26 }\" (click)=\"onOldUiPageClick('dashboard/code/pension-rate', 26)\" *ngIf=\"displayModulePages(ConfigurationPages.PensionRate)\"><a>Pension Rate</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink ==  27 }\" (click)=\"onOldUiPageClick('dashboard/code/employee-contract-content', 27)\" *ngIf=\"displayModulePages(ConfigurationPages.EmployeeContract)\"><a>Employee Contract</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink ==  28 }\" (click)=\"onOldUiPageClick('dashboard/code/appraisal-questions', 28)\" *ngIf=\"displayModulePages(ConfigurationPages.AppraisalQuestions)\"><a>Appraisal Questions</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink ==  63 }\" (click)=\"onOldUiPageClick('dashboard/code/interview-rating-questions', 63)\" *ngIf=\"displayModulePages(ConfigurationPages.RatingQuestions)\"><a>Rating Based Questions</a></li>\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  29 }\" (click)=\"onOldUiPageClick('dashboard/code/technical-questions', 29)\" *ngIf=\"displayModulePages(ConfigurationPages.TechnicalQuestions)\"><a>Technical Questions</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  30 }\" (click)=\"onOldUiPageClick('dashboard/code/email-setting', 30)\" *ngIf=\"displayModulePages(ConfigurationPages.EmailSettings)\"><a>Email Settings</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  31 }\" (click)=\"onOldUiPageClick('dashboard/code/exchange-rate', 31)\"><a>Exchange Rate</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  32 }\" (click)=\"onOldUiPageClick('dashboard/code/leavereason-type', 32)\" *ngIf=\"displayModulePages(ConfigurationPages.LeaveType)\"><a>Leave Type</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  33 }\" (click)=\"onOldUiPageClick('dashboard/code/profession-detail', 33)\" *ngIf=\"displayModulePages(ConfigurationPages.Profession)\"><a>Profession</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  34 }\" (click)=\"onOldUiPageClick('dashboard/code/department-code', 34)\" *ngIf=\"displayModulePages(ConfigurationPages.Department)\"><a>Department</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  35 }\" (click)=\"onOldUiPageClick('dashboard/code/qualification-type', 35)\" *ngIf=\"displayModulePages(ConfigurationPages.Qualification)\"><a>Qualification</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  36 }\" (click)=\"onOldUiPageClick('dashboard/code/designation-type', 36)\" *ngIf=\"displayModulePages(ConfigurationPages.Designation)\"><a>Designation</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink ==  37 }\" (click)=\"onOldUiPageClick('dashboard/code/job-grade', 37)\" *ngIf=\"displayModulePages(ConfigurationPages.JobGrade)\"><a>Job Grade</a></li> -->\r\n        <li [ngClass]=\"{'active': selectedLink ==  38 }\" (click)=\"onOldUiPageClick('dashboard/code/salary-head', 38)\" *ngIf=\"displayModulePages(ConfigurationPages.SalaryHead)\"><a>Salary Head</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink ==  39 }\" (click)=\"onOldUiPageClick('dashboard/code/salary-tax-report-content', 39)\" *ngIf=\"displayModulePages(ConfigurationPages.SalaryTaxReportContent)\"><a>Salary Tax Report Content</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink ==  40 }\" (click)=\"onOldUiPageClick('dashboard/code/set-payroll-account', 40)\" *ngIf=\"displayModulePages(ConfigurationPages.PayrollAccount)\"><a>Set Payroll Account</a></li>\r\n\r\n      </ul>\r\n\r\n      <li data-toggle=\"collapse\" data-target=\"#HR\" class=\"collapsed\"  *ngIf=\"displayChildModule(HRModulePagesMaster.HRPages)\">\r\n        <a>HR<span class=\"arrow\"></span></a>\r\n      </li>\r\n\r\n      <ul class=\"sub-menu collapse\" id=\"HR\">\r\n\r\n        <li [ngClass]=\"{'active': selectedLink == 41 }\" (click)=\"onOldUiPageClick('dashboard/hr/employees', 41)\" *ngIf=\"displayModulePages(HRPages.Employees)\"><a>Employees</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 42 }\" (click)=\"onOldUiPageClick('dashboard/hr/payroll-daily-hours', 42)\" *ngIf=\"displayModulePages(HRPages.PayrollDailyHours)\"><a>Payroll Daily Hours</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 43 }\" (click)=\"onOldUiPageClick('dashboard/hr/holidays', 43)\" *ngIf=\"displayModulePages(HRPages.Holidays)\"><a>Holidays</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 44 }\" (click)=\"onOldUiPageClick('dashboard/hr/attendance', 44)\" *ngIf=\"displayModulePages(HRPages.Attendance)\"><a>Attendance</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 45 }\" (click)=\"onOldUiPageClick('dashboard/hr/approve-leave', 45)\" *ngIf=\"displayModulePages(HRPages.ApproveLeave)\"><a>Approve Leave</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 46 }\" (click)=\"onOldUiPageClick('dashboard/hr/employee-salary', 46)\" *ngIf=\"displayModulePages(HRPages.MonthlyPayrollRegister)\"><a>Monthly Payroll Register</a></li>\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 47 }\" (click)=\"onOldUiPageClick('dashboard/hr/job-hiring-details', 47)\" *ngIf=\"displayModulePages(HRPages.Jobs)\"><a>Jobs</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 48 }\" (click)=\"onOldUiPageClick('dashboard/hr/interview-form', 48)\" *ngIf=\"displayModulePages(HRPages.Interview)\"><a>Interview</a></li> -->\r\n        <li [ngClass]=\"{'active': selectedLink == 49 }\" (click)=\"onOldUiPageClick('dashboard/hr/employee-appraisal', 49)\" *ngIf=\"displayModulePages(HRPages.EmployeeAppraisal)\"><a>Employee Appraisal</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 50 }\" (click)=\"onOldUiPageClick('dashboard/hr/advances', 50)\" *ngIf=\"displayModulePages(HRPages.Advances)\"><a>Advances</a></li>\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 64 }\" [routerLink]=\"['/hr/employees']\"><a>Employee List(Beta)</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 51 }\" (click)=\"onOldUiPageClick('dashboard/hr/summary', 51)\" *ngIf=\"displayModulePages(HRPages.Summary)\"><a>Summary</a></li> -->\r\n        <li [ngClass]=\"{'active': selectedLink == 62 }\" [routerLink]=\"['/hr/configuration']\"><a>Configuration</a></li>\r\n      </ul>\r\n\r\n      <li data-toggle=\"collapse\" data-target=\"#Store\" class=\"collapsed\" *ngIf=\"displayChildModule(StoreModulePagesMaster.StorePages)\">\r\n        <a>Store<span class=\"arrow\"></span></a>\r\n      </li>\r\n\r\n      <ul class=\"sub-menu collapse\" id=\"Store\">\r\n\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 52 }\" (click)=\"onOldUiPageClick('dashboard/store/store-master', 52)\" *ngIf=\"displayModulePages(StorePages.Categories)\"><a>Categories</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 53 }\" (click)=\"onOldUiPageClick('dashboard/store/store-source-codes', 53)\" *ngIf=\"displayModulePages(StorePages.StoreSourceCodes)\"><a>Store Source Codes</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 54 }\" (click)=\"onOldUiPageClick('dashboard/store/payment-types', 54)\" *ngIf=\"displayModulePages(StorePages.PaymentTypes)\"><a>Payment Types</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 55 }\" (click)=\"onOldUiPageClick('dashboard/store/store-main', 55)\" *ngIf=\"displayModulePages(StorePages.Store)\"><a>Store</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 56 }\" (click)=\"onOldUiPageClick('dashboard/store/procurment-summary', 56)\" *ngIf=\"displayModulePages(StorePages.ProcurementSummary)\"><a>Procurement Summary</a></li> -->\r\n        <!-- <li [ngClass]=\"{'active': selectedLink == 57 }\" (click)=\"onOldUiPageClick('dashboard/store/store-depreciation-report', 57)\" *ngIf=\"displayModulePages(StorePages.DepreciationReport)\"><a>Depreciation Report</a></li> -->\r\n        <li [ngClass]=\"{'active': selectedLink == 58 }\" [routerLink]=\"['/store/purchases']\"><a>Purchase & Procurement</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 59 }\" [routerLink]=\"['/store/vehicle/tracker']\"><a>Vehicle Tracker</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 60 }\" [routerLink]=\"['/store/generator/tracker']\"><a>Generator Tracker</a></li>\r\n        <li [ngClass]=\"{'active': selectedLink == 61 }\" [routerLink]=\"['/store/configuration']\"><a>Store Configuration</a></li>\r\n      </ul>\r\n\r\n      <!-- <li (click)=\"onsideBarLinkClicked('/style-guide')\"> <a>Style-guide</a>\r\n      </li> -->\r\n\r\n      <li (click)=\"onOldUIClick()\"> <a>Old UI</a>\r\n      </li>\r\n\r\n      <li (click)=\"onLogout()\"> <i class=\"fas fa-sign-out-alt\"></i> <a> Logout</a>\r\n      </li>\r\n\r\n    </ul>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/shared/dbsidebar/dbsidebar.component.scss":
/*!***********************************************************!*\
  !*** ./src/app/shared/dbsidebar/dbsidebar.component.scss ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".nav-side-menu {\n  overflow: auto;\n  font-size: 12px;\n  font-weight: 400;\n  background-color: #151b26;\n  top: 0px;\n  width: 240px;\n  height: 100%;\n  color: #e1ffff; }\n\n.nav-side-menu .brand {\n  line-height: 50px;\n  display: block;\n  font-size: 18px;\n  font-weight: 500;\n  color: #fff;\n  letter-spacing: 1px;\n  border-bottom: 1px solid #222b37;\n  padding: 13px 15px; }\n\n.nav-side-menu .brand i {\n  margin-right: 10px;\n  font-size: 28px;\n  position: relative;\n  top: 3px; }\n\n.nav-side-menu .toggle-btn {\n  display: none; }\n\n.nav-side-menu ul,\n.nav-side-menu li {\n  list-style: none;\n  margin: 0px;\n  cursor: pointer;\n  padding: 0; }\n\n.sub-menu.collapse, .sub-menu.collapsing {\n  padding-left: 10px !important; }\n\n.nav-side-menu ul :not(collapsed) .arrow:before,\n.nav-side-menu li :not(collapsed) .arrow:before {\n  font-family: FontAwesome;\n  content: \"\\f078\";\n  display: inline-block;\n  padding-left: 10px;\n  padding-right: 10px;\n  vertical-align: middle;\n  float: right; }\n\n.nav-side-menu ul .active,\n.nav-side-menu li .active {\n  background-color: #00d4b1;\n  color: black; }\n\n.nav-side-menu ul .sub-menu li,\n.nav-side-menu li .sub-menu li {\n  border: none;\n  line-height: 12px;\n  margin-left: 0px;\n  padding: 10px 15px; }\n\n.nav-side-menu ul .sub-menu li:hover,\n.nav-side-menu li .sub-menu li:hover {\n  background-color: rgba(255, 255, 255, 0.1); }\n\n.nav-side-menu ul .sub-menu li:before,\n.nav-side-menu li {\n  padding-left: 0px; }\n\n.nav-side-menu li a {\n  text-decoration: none;\n  color: #fff;\n  font-size: 13px; }\n\n.nav-side-menu li:active {\n  color: black; }\n\n.nav-side-menu li a i {\n  font-size: 14px;\n  position: relative;\n  top: 4px;\n  padding-right: 15px;\n  color: rgba(255, 255, 255, 0.6); }\n\n.nav-side-menu li:hover {\n  background-color: rgba(255, 255, 255, 0.1);\n  -webkit-transition: all 1s ease;\n  transition: all 1s ease; }\n\n.nav-side-menu .icon-wrap {\n  left: 0 !important; }\n\n.nav-side-menu.open .icon-wrap {\n  left: 240px !important; }\n\n@media (max-width: 767px) {\n  .nav-side-menu {\n    margin-bottom: 10px;\n    z-index: 1;\n    left: -250px;\n    -webkit-transition: 0.4s;\n    transition: 0.4s; }\n  .nav-side-menu.open {\n    left: 0; }\n  .nav-side-menu .toggle-btn {\n    display: block;\n    cursor: pointer;\n    right: 10px;\n    top: 10px;\n    z-index: 10 !important;\n    padding: 3px;\n    background-color: #ffffff;\n    color: #000;\n    width: 40px;\n    text-align: center; }\n  .brand {\n    text-align: left !important;\n    font-size: 22px;\n    padding-left: 20px;\n    line-height: 50px !important; }\n  .icon-wrap {\n    display: block; } }\n\n@media (min-width: 767px) {\n  .nav-side-menu .menu-list .menu-content {\n    display: block; } }\n\nbody {\n  margin: 0px;\n  padding: 0px; }\n\n.nav-side-menu li {\n  padding: 12px 15px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvc2hhcmVkL2Ric2lkZWJhci9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxzaGFyZWRcXGRic2lkZWJhclxcZGJzaWRlYmFyLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0UsY0FBYztFQUNkLGVBQWU7RUFDZixnQkFBZ0I7RUFDaEIseUJBQXlCO0VBQ3pCLFFBQVE7RUFDUixZQUFXO0VBQ1gsWUFBWTtFQUNaLGNBQWMsRUFBQTs7QUFFaEI7RUFDSSxpQkFBaUI7RUFDakIsY0FBYztFQUNkLGVBQWU7RUFDZixnQkFBZ0I7RUFDaEIsV0FBVztFQUNYLG1CQUFtQjtFQUNuQixnQ0FBZ0M7RUFDaEMsa0JBQWtCLEVBQUE7O0FBRXRCO0VBQXdCLGtCQUFrQjtFQUFFLGVBQWU7RUFBRSxrQkFBa0I7RUFBQyxRQUFRLEVBQUE7O0FBQ3hGO0VBQ0UsYUFBYSxFQUFBOztBQUVmOztFQUVFLGdCQUFnQjtFQUNkLFdBQVc7RUFDWixlQUFlO0VBQ2YsVUFBUyxFQUFBOztBQUVaO0VBQXlDLDZCQUE0QixFQUFBOztBQUVyRTs7RUFFRSx3QkFBd0I7RUFDeEIsZ0JBQWdCO0VBQ2hCLHFCQUFxQjtFQUNyQixrQkFBa0I7RUFDbEIsbUJBQW1CO0VBQ25CLHNCQUFzQjtFQUN0QixZQUFZLEVBQUE7O0FBRWQ7O0VBR0UseUJBQXlCO0VBQ3pCLFlBQVksRUFBQTs7QUFHZDs7RUFFRSxZQUFZO0VBQ1osaUJBQWlCO0VBQ2pCLGdCQUFnQjtFQUNoQixrQkFBa0IsRUFBQTs7QUFHcEI7O0VBRUUsMENBQXNDLEVBQUE7O0FBRXhDOztFQUVFLGlCQUFpQixFQUFBOztBQUVuQjtFQUNFLHFCQUFxQjtFQUNyQixXQUFXO0VBQ1gsZUFBYyxFQUFBOztBQUVoQjtFQUNFLFlBQVksRUFBQTs7QUFFZDtFQUNFLGVBQWU7RUFDZixrQkFBa0I7RUFDbEIsUUFBTztFQUNQLG1CQUFtQjtFQUNuQiwrQkFBMkIsRUFBQTs7QUFFN0I7RUFHRSwwQ0FBc0M7RUFDdEMsK0JBQStCO0VBSS9CLHVCQUF1QixFQUFBOztBQUV6QjtFQUEwQixrQkFBaUIsRUFBQTs7QUFDM0M7RUFBK0Isc0JBQXFCLEVBQUE7O0FBQ3BEO0VBQ0U7SUFDRSxtQkFBbUI7SUFDbkIsVUFBVTtJQUNWLFlBQVk7SUFDWix3QkFBZ0I7SUFBaEIsZ0JBQWdCLEVBQUE7RUFFbEI7SUFBcUIsT0FBTSxFQUFBO0VBQzNCO0lBQ0UsY0FBYztJQUNkLGVBQWU7SUFDZixXQUFXO0lBQ1gsU0FBUztJQUNULHNCQUFzQjtJQUN0QixZQUFZO0lBQ1oseUJBQXlCO0lBQ3pCLFdBQVc7SUFDWCxXQUFXO0lBQ1gsa0JBQWtCLEVBQUE7RUFFcEI7SUFDRSwyQkFBMkI7SUFDM0IsZUFBZTtJQUNmLGtCQUFrQjtJQUNsQiw0QkFBNEIsRUFBQTtFQUU5QjtJQUFXLGNBQWEsRUFBQSxFQUFHOztBQUUzQjtFQUNFO0lBQ0UsY0FBYyxFQUFBLEVBQ2pCOztBQUVIO0VBQ0UsV0FBVztFQUNYLFlBQVksRUFBQTs7QUFFZDtFQUFrQixrQkFBa0IsRUFBQSIsImZpbGUiOiJzcmMvYXBwL3NoYXJlZC9kYnNpZGViYXIvZGJzaWRlYmFyLmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsiLm5hdi1zaWRlLW1lbnUge1xyXG4gIG92ZXJmbG93OiBhdXRvO1xyXG4gIGZvbnQtc2l6ZTogMTJweDtcclxuICBmb250LXdlaWdodDogNDAwO1xyXG4gIGJhY2tncm91bmQtY29sb3I6ICMxNTFiMjY7XHJcbiAgdG9wOiAwcHg7XHJcbiAgd2lkdGg6MjQwcHg7XHJcbiAgaGVpZ2h0OiAxMDAlO1xyXG4gIGNvbG9yOiAjZTFmZmZmO1xyXG59XHJcbi5uYXYtc2lkZS1tZW51IC5icmFuZCB7XHJcbiAgICBsaW5lLWhlaWdodDogNTBweDtcclxuICAgIGRpc3BsYXk6IGJsb2NrO1xyXG4gICAgZm9udC1zaXplOiAxOHB4O1xyXG4gICAgZm9udC13ZWlnaHQ6IDUwMDtcclxuICAgIGNvbG9yOiAjZmZmO1xyXG4gICAgbGV0dGVyLXNwYWNpbmc6IDFweDtcclxuICAgIGJvcmRlci1ib3R0b206IDFweCBzb2xpZCAjMjIyYjM3O1xyXG4gICAgcGFkZGluZzogMTNweCAxNXB4O1xyXG59XHJcbi5uYXYtc2lkZS1tZW51IC5icmFuZCBpe21hcmdpbi1yaWdodDogMTBweDsgZm9udC1zaXplOiAyOHB4OyBwb3NpdGlvbjogcmVsYXRpdmU7dG9wOiAzcHg7fVxyXG4ubmF2LXNpZGUtbWVudSAudG9nZ2xlLWJ0biB7XHJcbiAgZGlzcGxheTogbm9uZTtcclxufVxyXG4ubmF2LXNpZGUtbWVudSAgdWwsXHJcbi5uYXYtc2lkZS1tZW51ICBsaSB7XHJcbiAgbGlzdC1zdHlsZTogbm9uZTtcclxuICAgIG1hcmdpbjogMHB4O1xyXG4gICBjdXJzb3I6IHBvaW50ZXI7XHJcbiAgIHBhZGRpbmc6MDtcclxufVxyXG4uc3ViLW1lbnUuY29sbGFwc2UgLC5zdWItbWVudS5jb2xsYXBzaW5ne3BhZGRpbmctbGVmdDoxMHB4ICFpbXBvcnRhbnQ7fVxyXG5cclxuLm5hdi1zaWRlLW1lbnUgdWwgOm5vdChjb2xsYXBzZWQpIC5hcnJvdzpiZWZvcmUsXHJcbi5uYXYtc2lkZS1tZW51IGxpIDpub3QoY29sbGFwc2VkKSAuYXJyb3c6YmVmb3JlIHtcclxuICBmb250LWZhbWlseTogRm9udEF3ZXNvbWU7XHJcbiAgY29udGVudDogXCJcXGYwNzhcIjtcclxuICBkaXNwbGF5OiBpbmxpbmUtYmxvY2s7XHJcbiAgcGFkZGluZy1sZWZ0OiAxMHB4O1xyXG4gIHBhZGRpbmctcmlnaHQ6IDEwcHg7XHJcbiAgdmVydGljYWwtYWxpZ246IG1pZGRsZTtcclxuICBmbG9hdDogcmlnaHQ7XHJcbn1cclxuLm5hdi1zaWRlLW1lbnUgdWwgLmFjdGl2ZSxcclxuLm5hdi1zaWRlLW1lbnUgbGkgLmFjdGl2ZSB7XHJcblxyXG4gIGJhY2tncm91bmQtY29sb3I6ICMwMGQ0YjE7XHJcbiAgY29sb3I6IGJsYWNrO1xyXG4gIC8vIGJhY2tncm91bmQtY29sb3I6cmdiYSgyNTUsMjU1LDI1NSwwLjEpO1xyXG59XHJcbi5uYXYtc2lkZS1tZW51IHVsIC5zdWItbWVudSBsaSxcclxuLm5hdi1zaWRlLW1lbnUgbGkgLnN1Yi1tZW51IGxpIHtcclxuICBib3JkZXI6IG5vbmU7XHJcbiAgbGluZS1oZWlnaHQ6IDEycHg7XHJcbiAgbWFyZ2luLWxlZnQ6IDBweDtcclxuICBwYWRkaW5nOiAxMHB4IDE1cHg7XHJcbn1cclxuXHJcbi5uYXYtc2lkZS1tZW51IHVsIC5zdWItbWVudSBsaTpob3ZlcixcclxuLm5hdi1zaWRlLW1lbnUgbGkgLnN1Yi1tZW51IGxpOmhvdmVyIHtcclxuICBiYWNrZ3JvdW5kLWNvbG9yOnJnYmEoMjU1LDI1NSwyNTUsMC4xKTtcclxufVxyXG4ubmF2LXNpZGUtbWVudSB1bCAuc3ViLW1lbnUgbGk6YmVmb3JlLFxyXG4ubmF2LXNpZGUtbWVudSBsaSB7XHJcbiAgcGFkZGluZy1sZWZ0OiAwcHg7XHJcbn1cclxuLm5hdi1zaWRlLW1lbnUgbGkgYSB7XHJcbiAgdGV4dC1kZWNvcmF0aW9uOiBub25lO1xyXG4gIGNvbG9yOiAjZmZmO1xyXG4gIGZvbnQtc2l6ZToxM3B4O1xyXG59XHJcbi5uYXYtc2lkZS1tZW51IGxpOmFjdGl2ZSB7XHJcbiAgY29sb3I6IGJsYWNrO1xyXG59XHJcbi5uYXYtc2lkZS1tZW51IGxpIGEgaSB7XHJcbiAgZm9udC1zaXplOiAxNHB4O1xyXG4gIHBvc2l0aW9uOiByZWxhdGl2ZTtcclxuICB0b3A6NHB4O1xyXG4gIHBhZGRpbmctcmlnaHQ6IDE1cHg7XHJcbiAgY29sb3I6IHJnYmEoMjU1LDI1NSwyNTUsLjYpO1xyXG59XHJcbi5uYXYtc2lkZS1tZW51IGxpOmhvdmVyIHtcclxuICAvLyBiYWNrZ3JvdW5kLWNvbG9yOiAjMDBkNGIxO1xyXG4gIC8vIGNvbG9yOiBibGFjaztcclxuICBiYWNrZ3JvdW5kLWNvbG9yOnJnYmEoMjU1LDI1NSwyNTUsMC4xKTtcclxuICAtd2Via2l0LXRyYW5zaXRpb246IGFsbCAxcyBlYXNlO1xyXG4gIC1tb3otdHJhbnNpdGlvbjogYWxsIDFzIGVhc2U7XHJcbiAgLW8tdHJhbnNpdGlvbjogYWxsIDFzIGVhc2U7XHJcbiAgLW1zLXRyYW5zaXRpb246IGFsbCAxcyBlYXNlO1xyXG4gIHRyYW5zaXRpb246IGFsbCAxcyBlYXNlO1xyXG59XHJcbi5uYXYtc2lkZS1tZW51IC5pY29uLXdyYXB7bGVmdDowICFpbXBvcnRhbnQ7fVxyXG4ubmF2LXNpZGUtbWVudS5vcGVuIC5pY29uLXdyYXB7bGVmdDoyNDBweCAhaW1wb3J0YW50O31cclxuQG1lZGlhIChtYXgtd2lkdGg6IDc2N3B4KSB7XHJcbiAgLm5hdi1zaWRlLW1lbnUge1xyXG4gICAgbWFyZ2luLWJvdHRvbTogMTBweDtcclxuICAgIHotaW5kZXg6IDE7XHJcbiAgICBsZWZ0OiAtMjUwcHg7XHJcbiAgICB0cmFuc2l0aW9uOiAwLjRzO1xyXG4gIH1cclxuICAubmF2LXNpZGUtbWVudS5vcGVuIHtsZWZ0OjB9XHJcbiAgLm5hdi1zaWRlLW1lbnUgLnRvZ2dsZS1idG4ge1xyXG4gICAgZGlzcGxheTogYmxvY2s7XHJcbiAgICBjdXJzb3I6IHBvaW50ZXI7XHJcbiAgICByaWdodDogMTBweDtcclxuICAgIHRvcDogMTBweDtcclxuICAgIHotaW5kZXg6IDEwICFpbXBvcnRhbnQ7XHJcbiAgICBwYWRkaW5nOiAzcHg7XHJcbiAgICBiYWNrZ3JvdW5kLWNvbG9yOiAjZmZmZmZmO1xyXG4gICAgY29sb3I6ICMwMDA7XHJcbiAgICB3aWR0aDogNDBweDtcclxuICAgIHRleHQtYWxpZ246IGNlbnRlcjtcclxuICB9XHJcbiAgLmJyYW5kIHtcclxuICAgIHRleHQtYWxpZ246IGxlZnQgIWltcG9ydGFudDtcclxuICAgIGZvbnQtc2l6ZTogMjJweDtcclxuICAgIHBhZGRpbmctbGVmdDogMjBweDtcclxuICAgIGxpbmUtaGVpZ2h0OiA1MHB4ICFpbXBvcnRhbnQ7XHJcbiAgfVxyXG4gIC5pY29uLXdyYXB7ZGlzcGxheTpibG9jazt9XHJcbiAgfVxyXG4gIEBtZWRpYSAobWluLXdpZHRoOiA3NjdweCkge1xyXG4gICAgLm5hdi1zaWRlLW1lbnUgLm1lbnUtbGlzdCAubWVudS1jb250ZW50IHtcclxuICAgICAgZGlzcGxheTogYmxvY2s7XHJcbiAgfVxyXG59XHJcbmJvZHkge1xyXG4gIG1hcmdpbjogMHB4O1xyXG4gIHBhZGRpbmc6IDBweDtcclxufVxyXG4ubmF2LXNpZGUtbWVudSBsaXtwYWRkaW5nOiAxMnB4IDE1cHg7fVxyXG5cclxuXHJcbiJdfQ== */"

/***/ }),

/***/ "./src/app/shared/dbsidebar/dbsidebar.component.ts":
/*!*********************************************************!*\
  !*** ./src/app/shared/dbsidebar/dbsidebar.component.ts ***!
  \*********************************************************/
/*! exports provided: DbsidebarComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DbsidebarComponent", function() { return DbsidebarComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var _services_global_services_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var _services_localstorage_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../services/localstorage.service */ "./src/app/shared/services/localstorage.service.ts");
/* harmony import */ var _global__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../global */ "./src/app/shared/global.ts");
/* harmony import */ var src_app_login_login_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/login/login.service */ "./src/app/login/login.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var DbsidebarComponent = /** @class */ (function () {
    function DbsidebarComponent(router, globalService, appurl, localstorageservice, loginService) {
        this.router = router;
        this.globalService = globalService;
        this.appurl = appurl;
        this.localstorageservice = localstorageservice;
        this.loginService = loginService;
        // flag
        this.sidebarFlag = false;
        this.Marketing = {
            TimeCategory: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].TimeCategory,
            Quality: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Quality,
            Phase: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Phase,
            Nature: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Nature,
            Medium: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Medium,
            MediaCategory: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].MediaCategory,
            ActivityType: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].ActivityType,
            Clients: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Clients,
            UnitRates: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].UnitRates,
            Jobs: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Jobs,
            Contracts: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Contracts,
            Producer: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Producer,
            Channel: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Channel
        };
        this.ConfigurationPages = {
            AppraisalQuestions: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].AppraisalQuestions,
            RatingQuestions: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].RatingQuestions,
            CurrencyCodes: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].CurrencyCodes,
            Department: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Department,
            Designation: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Designation,
            EmailSettings: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].EmailSettings,
            EmployeeContract: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].EmployeeContract,
            FinancialYear: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].FinancialYear,
            JobGrade: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].JobGrade,
            JournalCodes: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].JournalCodes,
            LeaveType: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].LeaveReason,
            OfficeCodes: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].OfficeCodes,
            PensionRate: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].PensionRate,
            TechnicalQuestions: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].TechnicalQuestions,
            Profession: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Profession,
            Qualification: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Qualification,
            SalaryHead: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].SalaryHead,
            SalaryTaxReportContent: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].SalaryTaxReportContent,
            PayrollAccount: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].SetPayrollAccount
        };
        this.HRPages = {
            Advances: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Advances,
            ApproveLeave: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].ApproveLeave,
            Attendance: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Attendance,
            EmployeeAppraisal: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].EmployeeAppraisal,
            Employees: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Employees,
            Holidays: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Holidays,
            Interview: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Interview,
            Jobs: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Jobs,
            MonthlyPayrollRegister: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].MonthlyPayrollRegister,
            PayrollDailyHours: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].PayrollDailyHours,
            Summary: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Summary
        };
        this.StorePages = {
            Categories: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Categories,
            DepreciationReport: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].DepreciationReport,
            PaymentTypes: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].PaymentTypes,
            ProcurementSummary: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].ProcurementSummary,
            Store: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Store,
            StoreSourceCodes: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].StoreSourceCodes
        };
        this.AccountingNew = {
            Assets: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Assets,
            Liabilities: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Liabilities,
            Income: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Income,
            Expense: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Expense,
            BalanceSheet: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].BalanceSheet,
            IncomeExpenseReport: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].IncomeExpenseReport,
            Vouchers: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Vouchers,
            ExchangeRates: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].ExchangeRates,
            VoucherSummaryReport: _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].VoucherSummaryreport
        };
        this.ModulesAccounting = {
            ChartOfAccount: [
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Assets,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Income,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Expense,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Liabilities
            ],
            FinancialReport: [
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].BalanceSheet,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].IncomeExpenseReport
            ],
            Vouchers: [_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Vouchers],
            ExchangeRates: [_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].ExchangeRates],
            GainLossReport: [_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].ExchangeGainLoss],
            Journal: [_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Journal],
            Ledger: [_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].LedgerStatement],
            TrialBalance: [_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].TrialBalance],
            PensionPayments: [_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].PensionPayments],
            VoucherSummaryReport: [_applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].VoucherSummaryreport]
        };
        this.ModulesMarketing = {
            Marketing: [
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Clients,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Jobs,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Contracts,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].UnitRates
            ],
            ModulesMarketingMaster: [
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].TimeCategory,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Quality,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Phase,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Nature,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Medium,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].MediaCategory,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].ActivityType,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Producer,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Channel
            ]
        };
        this.ModulesProject = {
            Project: [
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].ProjectJobs,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].ProjectActivities,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].MyProjects,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].Donors,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].ProjectDetails,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].Proposal,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].CriteriaEvaluation,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].ProjectDashboard,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].ProjectCashFlow,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].ProjectBudgetLine,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["projectPagesMaster"].ProposalReport
            ]
        };
        this.UserModule = _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["ApplicationPages"].Users;
        this.ModulesConfigurationPages = {
            ConfigurationPages: [
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].AppraisalQuestions,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].CurrencyCodes,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].Department,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].Designation,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].EmailSettings,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].EmployeeContract,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].FinancialYear,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].JobGrade,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].JournalCodes,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].LeaveType,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].OfficeCodes,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].PensionRate,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].Profession,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].Proposal,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].ProposalReport,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].Qualification,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].SalaryHead,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].SalaryTaxReportContent,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].SetPayrollAccount,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["configurationPagesMaster"].TechnicalQuestions
            ]
        };
        this.HRModulePagesMaster = {
            HRPages: [
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].Advances,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].ApproveLeave,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].Attendance,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].EmployeeAppraisal,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].Employees,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].Holidays,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].Interview,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].Jobs,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].MonthlyPayrollRegister,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].PayrollDailyHours,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["HRPagesMaster"].Summary
            ]
        };
        this.StoreModulePagesMaster = {
            StorePages: [
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["StorePagesMaster"].Categories,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["StorePagesMaster"].DepreciationReport,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["StorePagesMaster"].PaymentTypes,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["StorePagesMaster"].ProcurementSummary,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["StorePagesMaster"].Store,
                _applicationpagesenum__WEBPACK_IMPORTED_MODULE_3__["StorePagesMaster"].StoreSourceCodes
            ]
        };
    }
    DbsidebarComponent.prototype.ngOnInit = function () {
        var rolePermissions = JSON.parse(localStorage.getItem('RolePermissions'));
        var userId = localStorage.getItem('UserId');
        this.GetApplicationPages();
    };
    DbsidebarComponent.prototype.onsideBarLinkClicked = function (routePath, selectedSideBar) {
        // selectedLink
        this.selectedLink = selectedSideBar;
        this.router.navigate([routePath]);
    };
    //#region "onOldUIClick"
    DbsidebarComponent.prototype.onOldUIClick = function () {
        window.open(this.appurl.getOldUiUrl(), '_blank');
    };
    DbsidebarComponent.prototype.onOldUiPageClick = function (path, selectedSideBar) {
        window.open(this.appurl.getOldUiUrl() + path, '_blank');
        this.selectedLink = selectedSideBar;
    };
    //#endregion
    //#region "GetApplicationPages"
    DbsidebarComponent.prototype.GetApplicationPages = function () {
        this.globalService
            .getList(this.appurl.getApiUrl() + _global__WEBPACK_IMPORTED_MODULE_6__["GLOBAL"].API_Code_GetApplicationPages)
            .subscribe(function (data) {
            if (data.data.ApplicationPagesList != null) {
                localStorage.setItem('ApplicationPages', JSON.stringify(data.data.ApplicationPagesList));
            }
        });
    };
    //#endregion
    //#region "display"
    DbsidebarComponent.prototype.displayModulePages = function (pageId) {
        var isAllowed = false;
        isAllowed = this.localstorageservice.displayModulePages(pageId);
        return isAllowed;
    };
    DbsidebarComponent.prototype.displayModule = function (moduleId) {
        var isAllowed = false;
        isAllowed = this.localstorageservice.displayModule(moduleId);
        return isAllowed;
    };
    DbsidebarComponent.prototype.displayChildModule = function (module) {
        var isAllowed = false;
        isAllowed = this.localstorageservice.displayChildModule(module);
        return isAllowed;
    };
    //#endregion
    //#region
    DbsidebarComponent.prototype.onLogout = function () {
        this.selectedLink = undefined;
        this.loginService.logout();
    };
    DbsidebarComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-dbsidebar',
            template: __webpack_require__(/*! ./dbsidebar.component.html */ "./src/app/shared/dbsidebar/dbsidebar.component.html"),
            styles: [__webpack_require__(/*! ./dbsidebar.component.scss */ "./src/app/shared/dbsidebar/dbsidebar.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"],
            _services_global_services_service__WEBPACK_IMPORTED_MODULE_4__["GlobalService"],
            _services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"],
            _services_localstorage_service__WEBPACK_IMPORTED_MODULE_5__["LocalStorageService"],
            src_app_login_login_service__WEBPACK_IMPORTED_MODULE_7__["LoginService"]])
    ], DbsidebarComponent);
    return DbsidebarComponent;
}());



/***/ }),

/***/ "./src/app/shared/share-layout.module.ts":
/*!***********************************************!*\
  !*** ./src/app/shared/share-layout.module.ts ***!
  \***********************************************/
/*! exports provided: ShareLayoutModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ShareLayoutModule", function() { return ShareLayoutModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _dbsidebar_dbsidebar_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./dbsidebar/dbsidebar.component */ "./src/app/shared/dbsidebar/dbsidebar.component.ts");
/* harmony import */ var _dbheader_dbheader_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./dbheader/dbheader.component */ "./src/app/shared/dbheader/dbheader.component.ts");
/* harmony import */ var _dbfooter_dbfooter_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./dbfooter/dbfooter.component */ "./src/app/shared/dbfooter/dbfooter.component.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};







var ShareLayoutModule = /** @class */ (function () {
    function ShareLayoutModule() {
    }
    ShareLayoutModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _dbsidebar_dbsidebar_component__WEBPACK_IMPORTED_MODULE_2__["DbsidebarComponent"],
                _dbheader_dbheader_component__WEBPACK_IMPORTED_MODULE_3__["DbheaderComponent"],
                _dbfooter_dbfooter_component__WEBPACK_IMPORTED_MODULE_4__["DbfooterComponent"]
            ],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatButtonModule"],
                _angular_router__WEBPACK_IMPORTED_MODULE_5__["RouterModule"]
            ],
            exports: [
                _dbsidebar_dbsidebar_component__WEBPACK_IMPORTED_MODULE_2__["DbsidebarComponent"],
                _dbheader_dbheader_component__WEBPACK_IMPORTED_MODULE_3__["DbheaderComponent"],
                _dbfooter_dbfooter_component__WEBPACK_IMPORTED_MODULE_4__["DbfooterComponent"]
            ]
        })
    ], ShareLayoutModule);
    return ShareLayoutModule;
}());



/***/ })

}]);
//# sourceMappingURL=default~configuration-configuration-module~dashboard-dashboard-module~hr-hr-module~store-store-modul~b01bbfdc.js.map