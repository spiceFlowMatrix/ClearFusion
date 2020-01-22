(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["vouchers-vouchers-module"],{

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

/***/ "./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.html":
/*!**************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.html ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <form\r\n    [formGroup]=\"addVoucherForm\"\r\n    (ngSubmit)=\"onAddVoucher(addVoucherForm)\"\r\n    role=\"form\"\r\n    action=\"POST\"\r\n  >\r\n    <h1 mat-dialog-title>\r\n      Create Voucher\r\n      <button mat-icon-button [mat-dialog-close]=\"data.data\" class=\"pull-right\">\r\n        <mat-icon aria-label=\"clear\">clear</mat-icon>\r\n      </button>\r\n    </h1>\r\n    <div mat-dialog-content>\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-12\">\r\n          <div class=\"row\">\r\n            <mat-form-field class=\"example-full-width\">\r\n              <textarea\r\n                matInput\r\n                placeholder=\"Description\"\r\n                name=\"Description\"\r\n                formControlName=\"Description\"\r\n              ></textarea>\r\n            </mat-form-field>\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-sm-6\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                <input\r\n                  matInput\r\n                  [matDatepicker]=\"voucherDatePicker\"\r\n                  placeholder=\"Voucher Date\"\r\n                  formControlName=\"VoucherDate\"\r\n                />\r\n                <mat-datepicker-toggle\r\n                  matSuffix\r\n                  [for]=\"voucherDatePicker\"\r\n                ></mat-datepicker-toggle>\r\n                <mat-datepicker #voucherDatePicker></mat-datepicker>\r\n              </mat-form-field>\r\n\r\n              <mat-form-field class=\"example-full-width\">\r\n                <mat-select\r\n                  placeholder=\"Currency\"\r\n                  name=\"CurrencyId\"\r\n                  formControlName=\"CurrencyId\"\r\n                >\r\n                  <mat-option\r\n                    *ngFor=\"let item of data.currencyList\"\r\n                    [value]=\"item.CurrencyId\"\r\n                  >\r\n                    {{ item.CurrencyName }}\r\n                  </mat-option>\r\n                </mat-select>\r\n              </mat-form-field>\r\n\r\n              <mat-form-field class=\"example-full-width\">\r\n                <mat-select\r\n                  placeholder=\"Office\"\r\n                  name=\"OfficeId\"\r\n                  formControlName=\"OfficeId\"\r\n                >\r\n                  <mat-option\r\n                    *ngFor=\"let item of data.officeList\"\r\n                    [value]=\"item.OfficeId\"\r\n                  >\r\n                    {{ item.OfficeName }}\r\n                  </mat-option>\r\n                </mat-select>\r\n              </mat-form-field>\r\n            </div>\r\n            <div class=\"col-sm-6\">\r\n              <mat-form-field class=\"example-full-width\">\r\n                <mat-select\r\n                  placeholder=\"Journal\"\r\n                  name=\"JournalCode\"\r\n                  formControlName=\"JournalCode\"\r\n                >\r\n                  <mat-option\r\n                    *ngFor=\"let item of data.journalList\"\r\n                    [value]=\"item.JournalCode\"\r\n                  >\r\n                    {{ item.JournalName }}\r\n                  </mat-option>\r\n                </mat-select>\r\n              </mat-form-field>\r\n              <mat-form-field class=\"example-full-width\">\r\n                <mat-select\r\n                  placeholder=\"Voucher Type\"\r\n                  name=\"VoucherTypeId\"\r\n                  formControlName=\"VoucherTypeId\"\r\n                >\r\n                  <mat-option\r\n                    *ngFor=\"let item of data.voucherTypeList\"\r\n                    [value]=\"item.VoucherTypeId\"\r\n                  >\r\n                    {{ item.VoucherTypeName }}\r\n                  </mat-option>\r\n                </mat-select>\r\n              </mat-form-field>\r\n\r\n              <mat-form-field class=\"example-form-field\">\r\n                <input\r\n                  matInput\r\n                  type=\"text\"\r\n                  placeholder=\"Cheque No\"\r\n                  name=\"ChequeNo\"\r\n                  formControlName=\"ChequeNo\"\r\n                />\r\n              </mat-form-field>\r\n\r\n              <!-- <mat-form-field class=\"example-full-width\">\r\n                <mat-select placeholder=\"Project\" name=\"ProjectId\" formControlName=\"ProjectId\">\r\n                    <mat-option *ngFor=\"let item of data.projectList\" [value]=\"item.ProjectId\" >\r\n                        {{item.ProjectName}}\r\n                    </mat-option>\r\n                </mat-select>\r\n            </mat-form-field> -->\r\n\r\n              <!-- <mat-form-field class=\"example-full-width\">\r\n                <mat-select placeholder=\"Budget Line\">\r\n                    <mat-option *ngFor=\"let item of data.budgetLineList\" [value]=\"item.BudgetLineId\">\r\n                        {{item.BudgetLineName}}\r\n                    </mat-option>\r\n                </mat-select>\r\n            </mat-form-field> -->\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n    <div mat-dialog-actions class=\"pull-right\">\r\n      <button mat-raised-button [mat-dialog-close]=\"data.data\">Cancel</button>\r\n\r\n      <button\r\n        mat-raised-button\r\n        color=\"accent\"\r\n        type=\"submit\"\r\n        [disabled]=\"!addVoucherForm.valid || addVoucherLoader\"\r\n        cdkFocusInitial\r\n        [disabled]=\"addVoucherLoader\"\r\n      >\r\n        <span class=\"display_inline_block\">\r\n          <mat-spinner *ngIf=\"addVoucherLoader\" [diameter]=\"15\"></mat-spinner>\r\n        </span>\r\n        <mat-icon *ngIf=\"!addVoucherLoader\" aria-label=\"done\">done</mat-icon>\r\n        Create\r\n      </button>\r\n    </div>\r\n  </form>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.scss":
/*!**************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.scss ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL3ZvdWNoZXJzL3ZvdWNoZXItYWRkL3ZvdWNoZXItYWRkLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.ts":
/*!************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.ts ***!
  \************************************************************************************/
/*! exports provided: VoucherAddComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VoucherAddComponent", function() { return VoucherAddComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _voucher_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../voucher.service */ "./src/app/dashboard/accounting/vouchers/voucher.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
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





var VoucherAddComponent = /** @class */ (function () {
    //#endregion
    function VoucherAddComponent(dialogRef, data, fb, voucherService, toastr) {
        this.dialogRef = dialogRef;
        this.data = data;
        this.fb = fb;
        this.voucherService = voucherService;
        this.toastr = toastr;
        this.addVoucherLoader = false;
        this.onListRefresh = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    VoucherAddComponent.prototype.ngOnInit = function () {
        this.initForm();
    };
    //#region "initForm"
    VoucherAddComponent.prototype.initForm = function () {
        this.addVoucherForm = this.fb.group({
            CurrencyId: [''],
            ChequeNo: [''],
            VoucherDate: [new Date(), [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            Description: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            JournalCode: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            VoucherTypeId: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            OfficeId: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]],
            ProjectId: [''],
        });
    };
    //#endregion
    //#region "addVoucher"
    VoucherAddComponent.prototype.addVoucher = function (data) {
        var _this = this;
        if (this.addVoucherForm.valid) {
            this.addVoucherLoader = true;
            var voucherData = {
                VoucherNo: data.VoucherNo,
                CurrencyId: data.CurrencyId,
                VoucherDate: this.setDateTime(data.VoucherDate),
                ChequeNo: data.ChequeNo,
                Description: data.Description,
                JournalCode: data.JournalCode,
                VoucherTypeId: data.VoucherTypeId,
                OfficeId: data.OfficeId,
                TimezoneOffset: data.TimezoneOffset
                // ProjectId: data.ProjectId,
                // BudgetLineId: data.value.BudgetLineId,
                // FinancialYearId: data.value.FinancialYearId, // calculate on backend
            };
            this.voucherService.AddVoucher(voucherData).subscribe(function (response) {
                if (response.statusCode === 200) {
                    _this.onCancelPopup();
                    _this.voucherListRefresh();
                    _this.toastr.success('Voucher is created successfully');
                }
                else {
                    _this.toastr.error(response.message);
                }
                _this.addVoucherLoader = false;
            }, function (error) {
                _this.toastr.error('Someting went wrong');
                _this.addVoucherLoader = false;
            });
        }
    };
    //#endregion
    //#region "onCancelPopup"
    VoucherAddComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    //#endregion
    //#region "onAddVoucher"
    VoucherAddComponent.prototype.onAddVoucher = function (data) {
        var voucherData = {
            VoucherNo: data.value.VoucherNo,
            CurrencyId: data.value.CurrencyId,
            VoucherDate: data.value.VoucherDate,
            ChequeNo: data.value.ChequeNo,
            Description: data.value.Description,
            JournalCode: data.value.JournalCode,
            VoucherTypeId: data.value.VoucherTypeId,
            OfficeId: data.value.OfficeId,
            TimezoneOffset: new Date().getTimezoneOffset()
            // ProjectId: data.value.ProjectId,
            // BudgetLineId: data.value.BudgetLineId,
            // FinancialYearId: data.value.FinancialYearId, // calculate on backend
        };
        this.addVoucher(voucherData);
    };
    //#endregion
    //#region "onListRefresh"
    VoucherAddComponent.prototype.voucherListRefresh = function () {
        this.onListRefresh.emit();
    };
    //#endregion
    //#region "setDateTime"
    VoucherAddComponent.prototype.setDateTime = function (data) {
        return new Date(new Date(data).getFullYear(), new Date(data).getMonth(), new Date(data).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds());
    };
    VoucherAddComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-voucher-add',
            template: __webpack_require__(/*! ./voucher-add.component.html */ "./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.html"),
            styles: [__webpack_require__(/*! ./voucher-add.component.scss */ "./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.scss")]
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MatDialogRef"],
            DataSources,
            _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            _voucher_service__WEBPACK_IMPORTED_MODULE_3__["VoucherService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"]])
    ], VoucherAddComponent);
    return VoucherAddComponent;
}());

var DataSources = /** @class */ (function () {
    function DataSources() {
    }
    return DataSources;
}());


/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-details.component.html":
/*!**********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-details/voucher-details.component.html ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"voucher-detail-main\">\r\n  <app-voucher-summary\r\n    [currentCredit]=\"totalCredits\"\r\n    [currentDebit]=\"totalDebits\"\r\n  ></app-voucher-summary>\r\n\r\n  <br />\r\n\r\n  <mat-card >\r\n    <div class=\"row\">\r\n      <div\r\n        class=\"col-sm-12\"\r\n        *ngIf=\"voucherDetailLoader; else voucherDetailtemplate\"\r\n      >\r\n        <mat-spinner class=\"center_loader\" diameter=\"50\"></mat-spinner>\r\n      </div>\r\n\r\n      <ng-template #voucherDetailtemplate>\r\n        <div class=\"col-sm-12\">\r\n          <div class=\"row\">\r\n            <div class=\"col-sm-12\">\r\n              <div class=\"border_bottom padding_bottom_10\">\r\n                <div class=\"row\">\r\n                  <div class=\"col-sm-5\">\r\n                    <button\r\n                      *ngIf=\"voucherDetail.IsVoucherVerified\"\r\n                      mat-raised-button\r\n                      color=\"accent\"\r\n                      pTooltip=\"Voucher validated\"\r\n                      tooltipPosition=\"top\"\r\n                      aria-label=\"Voucher validated\"\r\n                      (click)=\"onVoucherVerify()\"\r\n                    >\r\n                      <mat-icon\r\n                        aria-label=\"icon-button done icon\"\r\n                        class=\"margin_left_minus_5\"\r\n                        >done</mat-icon\r\n                      >\r\n                      Validate\r\n                    </button>\r\n                    <button\r\n                      *ngIf=\"!voucherDetail.IsVoucherVerified\"\r\n                      mat-stroked-button\r\n                      pTooltip=\"Validate voucher\"\r\n                      tooltipPosition=\"top\"\r\n                      aria-label=\"Verify voucher\"\r\n                      (click)=\"onVoucherVerify()\"\r\n                    >\r\n                      <mat-icon\r\n                        aria-label=\"icon-button done icon\"\r\n                        class=\"margin_left_minus_5\"\r\n                      >\r\n                        done\r\n                      </mat-icon>\r\n                      Invalidate\r\n                    </button>\r\n                  </div>\r\n                  <div class=\"col-sm-6\">\r\n                      <button mat-stroked-button color=\"accent\" style=\"left:64%;\" (click)=\"onExportPdf()\">\r\n                        <mat-icon aria-hidden=\"false\" aria-label=\"Example home icon\">\r\n                          vertical_align_bottom </mat-icon\r\n                        >Export Pdf\r\n                      </button>\r\n                  </div>\r\n                  <div class=\"col-sm-1 pull-right \">\r\n                    <!-- Filter -->\r\n                    <div *ngIf=\"!fileUploadLoader; else documentUploadtemplate\">\r\n                      <mat-icon\r\n                        class=\"icon_cursor padding_top_5\"\r\n                        aria-label=\"icon-button done icon\"\r\n                        pTooltip=\"Documents\"\r\n                        tooltipPosition=\"top\"\r\n                        [matMenuTriggerFor]=\"menu\"\r\n                        >attach_file</mat-icon\r\n                      >\r\n                    </div>\r\n                    <ng-template #documentUploadtemplate>\r\n                      <mat-spinner\r\n                        class=\"center_loader\"\r\n                        diameter=\"15\"\r\n                        *ngIf=\"fileUploadLoader\"\r\n                      ></mat-spinner>\r\n                    </ng-template>\r\n                    <mat-menu #menu=\"matMenu\">\r\n                      <div>\r\n                      <input\r\n                        id=\"fileInput\"\r\n                        style=\"display: contents;\"\r\n                        hidden\r\n                        type=\"file\"\r\n                        (change)=\"fileChange($event.target.files)\"\r\n                      />\r\n                      <button mat-menu-item (click)=\"openInput()\">\r\n                          <i class=\"fa fa-upload\" aria-hidden=\"true\"></i> &nbsp;\r\n                        Upload Document\r\n                      </button>\r\n                      <button mat-menu-item (click)=\"openDocumentsDialog()\">\r\n                          <i class=\"fa fa-eye\" aria-hidden=\"true\"></i> &nbsp;\r\n                        View Documents\r\n                      </button>\r\n                    </div>\r\n                    </mat-menu>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <h4>\r\n                {{ voucherDetail.ReferenceNo }}\r\n              </h4>\r\n            </div>\r\n            <div class=\"col-sm-12\">\r\n              <br />\r\n              <div class=\"row\">\r\n                <div class=\"col-sm-6\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <textarea\r\n                      matInput\r\n                      placeholder=\"Leave a comment\"\r\n                      [(ngModel)]=\"voucherDetail.Description\"\r\n                      (change)=\"onVoucherValuechange()\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    ></textarea>\r\n                  </mat-form-field>\r\n                </div>\r\n              </div>\r\n              <div class=\"row\">\r\n                <!-- Voucher date -->\r\n                <div class=\"col-sm-4\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <input\r\n                      matInput\r\n                      [matDatepicker]=\"voucherDatepicker\"\r\n                      placeholder=\"Voucher Date\"\r\n                      [(ngModel)]=\"voucherDetail.VoucherDate\"\r\n                      (dateChange)=\"onVoucherValuechange()\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    />\r\n                    <mat-datepicker-toggle\r\n                      matSuffix\r\n                      [for]=\"voucherDatepicker\"\r\n                    ></mat-datepicker-toggle>\r\n                    <mat-datepicker #voucherDatepicker></mat-datepicker>\r\n                  </mat-form-field>\r\n                </div>\r\n\r\n                <!-- Currency -->\r\n                <div class=\"col-sm-4\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <mat-select\r\n                      placeholder=\"Currency\"\r\n                      [(ngModel)]=\"voucherDetail.CurrencyId\"\r\n                      (selectionChange)=\"onVoucherValuechange()\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    >\r\n                      <mat-option\r\n                        *ngFor=\"let item of currencyList\"\r\n                        [value]=\"item.CurrencyId\"\r\n                      >\r\n                        {{ item.CurrencyName }}\r\n                      </mat-option>\r\n                    </mat-select>\r\n                  </mat-form-field>\r\n                </div>\r\n\r\n                <!-- Journal -->\r\n                <div class=\"col-sm-4\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <mat-select\r\n                      placeholder=\"Journal\"\r\n                      [(ngModel)]=\"voucherDetail.JournalCode\"\r\n                      (selectionChange)=\"onVoucherValuechange()\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    >\r\n                      <mat-option\r\n                        *ngFor=\"let item of journalList\"\r\n                        [value]=\"item.JournalCode\"\r\n                      >\r\n                        {{ item.JournalName }}\r\n                      </mat-option>\r\n                    </mat-select>\r\n                  </mat-form-field>\r\n                </div>\r\n              </div>\r\n\r\n              <div class=\"row\">\r\n                <!-- Voucher Type -->\r\n                <div class=\"col-sm-4\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <mat-select\r\n                      placeholder=\"Voucher Type\"\r\n                      [(ngModel)]=\"voucherDetail.VoucherTypeId\"\r\n                      (selectionChange)=\"onVoucherValuechange()\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    >\r\n                      <mat-option\r\n                        *ngFor=\"let item of voucherTypeList\"\r\n                        [value]=\"item.VoucherTypeId\"\r\n                      >\r\n                        {{ item.VoucherTypeName }}\r\n                      </mat-option>\r\n                    </mat-select>\r\n                  </mat-form-field>\r\n                </div>\r\n                <!-- Office -->\r\n                <div class=\"col-sm-4\">\r\n                  <mat-form-field class=\"example-full-width\">\r\n                    <mat-select\r\n                      placeholder=\"Office\"\r\n                      [(ngModel)]=\"voucherDetail.OfficeId\"\r\n                      (selectionChange)=\"onVoucherValuechange()\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    >\r\n                      <mat-option\r\n                        *ngFor=\"let item of officeList\"\r\n                        [value]=\"item.OfficeId\"\r\n                      >\r\n                        {{ item.OfficeName }}\r\n                      </mat-option>\r\n                    </mat-select>\r\n                  </mat-form-field>\r\n                </div>\r\n\r\n                <!-- Check No -->\r\n                <div class=\"col-sm-12\">\r\n                  <mat-form-field class=\"example-form-field\">\r\n                    <input\r\n                      matInput\r\n                      type=\"text\"\r\n                      placeholder=\"Cheque No\"\r\n                      [(ngModel)]=\"voucherDetail.ChequeNo\"\r\n                      (change)=\"onVoucherValuechange()\"\r\n                      [disabled]=\"!isEditingAllowed\"\r\n                    />\r\n                  </mat-form-field>\r\n                </div>\r\n              </div>\r\n\r\n              <!-- transaction -->\r\n              <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                  <div class=\"transaction-main\">\r\n                    <div class=\"row\">\r\n                      <div class=\"col-sm-12\">\r\n                        <div class=\"border_bottom\">\r\n                          <br />\r\n                          <h4 class=\"display_inline_block\">TRANSACTIONS</h4>\r\n\r\n                          <button\r\n                          mat-stroked-button\r\n                          color=\"accent\"\r\n                          class=\"pull-right\"\r\n                          pTooltip=\"Save Transaction\"\r\n                          tooltipPosition=\"top\"\r\n                          (click)=\"onTransactionListVerify()\"\r\n                          *ngIf=\"isEditingAllowed\"\r\n                          [disabled]=\"checkTransactionFlag\">\r\n                            <span class=\"display_inline_block\">\r\n                              <mat-spinner *ngIf=\"checkTransactionFlag\" [diameter]=\"15\"></mat-spinner>\r\n                            </span>\r\n                            <mat-icon  *ngIf=\"!checkTransactionFlag\"  aria-label=\"done\">done</mat-icon>\r\n                            Save\r\n                          </button>\r\n\r\n                        </div>\r\n                      </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                      <!-- debits -->\r\n                      <div class=\"col-sm-12\">\r\n                        <div class=\"total_debit clearfix\">\r\n                          <h6>\r\n                            <strong>Debits</strong>\r\n                            <span>Total Debits: {{ totalDebits }}</span>\r\n                          </h6>\r\n                          <button\r\n                            mat-icon-button\r\n                            (click)=\"onAddTransactionDebit()\"\r\n                            *ngIf=\"isEditingAllowed\"\r\n                          >\r\n                            <mat-icon aria-label=\"icon-button add icon\"\r\n                              >add</mat-icon\r\n                            >\r\n                          </button>\r\n                        </div>\r\n\r\n                        <!-- debits table -->\r\n                        <div class=\"row\">\r\n                          <div class=\"col-sm-12\">\r\n                            <div class=\"responsive_table-responsive \">\r\n                              <table class=\"table table-bordered\">\r\n                                <tbody>\r\n                                  <!-- <tr *ngFor=\"let item in transactionCreditList\"> -->\r\n                                  <tr *ngFor=\"let item of transactionDebitList\">\r\n                                    <td width=\"1%\">\r\n                                      <p class=\"width_8\"></p>\r\n                                    </td>\r\n\r\n                                    <td width=\"12%\" class=\"text-center\">\r\n                                      <!-- <mat-form-field class=\"example-full-width\">\r\n                                      <mat-select placeholder=\"Account\" [(ngModel)]=\"item.AccountNo\" (selectionChange)=\"\r\n                                          onTransactionDetailChanged(item, 'Account')\r\n                                        \" [disabled]=\"!isEditingAllowed\">\r\n                                        <mat-option *ngFor=\"\r\n                                            let i of inputLevelAccountList\r\n                                          \" [value]=\"i.AccountCode\">\r\n                                          {{ i.AccountName }}\r\n                                        </mat-option>\r\n                                      </mat-select>\r\n                                    </mat-form-field> -->\r\n\r\n                                      <lib-search-dropdown\r\n                                        placeholder=\"Accounts\"\r\n                                        [multiSelect]=\"false\"\r\n                                        placeholderSearchLabel=\"Find Account...\"\r\n                                        noEntriesFoundLabel=\"No matching account found\"\r\n                                        [dataSource]=\"accountDataSource\"\r\n                                        [selectedValue]=\"item.AccountNo\"\r\n                                        (openedChange)=\"\r\n                                          openedChange($event, item)\r\n                                        \"\r\n                                      >\r\n                                      </lib-search-dropdown>\r\n                                    </td>\r\n                                    <td\r\n                                      width=\"16%\"\r\n                                      class=\"text-center\"\r\n                                      text-align=\"left\"\r\n                                    >\r\n                                      <mat-form-field\r\n                                        class=\"example-form-field\"\r\n                                      >\r\n                                        <input\r\n                                          matInput\r\n                                          type=\"text\"\r\n                                          placeholder=\"Description\"\r\n                                          [(ngModel)]=\"item.Description\"\r\n                                          (change)=\"\r\n                                            onTransactionDetailChanged(\r\n                                              item,\r\n                                              'Description'\r\n                                            )\r\n                                          \"\r\n                                          [disabled]=\"!isEditingAllowed\"\r\n                                        />\r\n                                      </mat-form-field>\r\n                                    </td>\r\n                                    <td\r\n                                      width=\"12%\"\r\n                                      class=\"text-center\"\r\n                                      text-align=\"left\"\r\n                                    >\r\n                                      <mat-form-field\r\n                                        class=\"example-form-field\"\r\n                                      >\r\n                                        <input\r\n                                          matInput\r\n                                          min=\"0\"\r\n                                          type=\"number\"\r\n                                          placeholder=\"Amount\"\r\n                                          [(ngModel)]=\"item.Debit\"\r\n                                          (change)=\"\r\n                                            onTransactionDetailChanged(\r\n                                              item,\r\n                                              'Debit'\r\n                                            )\r\n                                          \"\r\n                                          [disabled]=\"!isEditingAllowed\"\r\n                                        />\r\n                                      </mat-form-field>\r\n                                    </td>\r\n                                    <td width=\"12%\" class=\"text-center\">\r\n                                        <lib-search-dropdown\r\n                                        placeholder=\"Project\"\r\n                                        [multiSelect]=\"false\"\r\n                                        placeholderSearchLabel=\"Find Projects...\"\r\n                                        noEntriesFoundLabel=\"No matching Project found\"\r\n                                        [dataSource]=\"projectDropdownList\"\r\n                                        [selectedValue]=\"item.ProjectId\"\r\n                                        (selectionChanged)=\"\r\n                                        onOpenedProjectChange($event,item)\"\r\n                                      >\r\n                                      </lib-search-dropdown>\r\n                                      <!-- <mat-form-field\r\n                                        class=\"example-full-width\"\r\n                                      >\r\n                                        <mat-select\r\n                                          placeholder=\"Project\"\r\n                                          [(ngModel)]=\"item.ProjectId\"\r\n                                          (selectionChange)=\"\r\n                                            onTransactionDetailChanged(\r\n                                              item,\r\n                                              'Project'\r\n                                            )\r\n                                          \"\r\n                                          [disabled]=\"!isEditingAllowed\"\r\n                                        >\r\n                                          <mat-option\r\n                                            *ngFor=\"let i of projectList\"\r\n                                            [value]=\"i.ProjectId\"\r\n                                          >\r\n                                            {{ i.ProjectNameCode }}\r\n                                          </mat-option>\r\n                                        </mat-select>\r\n                                      </mat-form-field> -->\r\n                                    </td>\r\n                                    <td width=\"12%\" class=\"text-center\">\r\n                                        <lib-search-dropdown\r\n                                        placeholder=\"Budget Line\"\r\n                                        [multiSelect]=\"false\"\r\n                                        placeholderSearchLabel=\"Find BudgetLine...\"\r\n                                        noEntriesFoundLabel=\"No matching BudgetLine found\"\r\n                                        [dataSource]=\"item.BudgetLineList\"\r\n                                        [selectedValue]=\"item.BudgetLineId\"\r\n                                        (openedChange)=\"\r\n                                        onOpenedBudgetLineChange($event,item)\"\r\n                                      >\r\n                                      </lib-search-dropdown>\r\n                                      <!-- <mat-form-field\r\n                                        class=\"example-full-width\"\r\n                                      >\r\n                                        <mat-select\r\n                                          placeholder=\"Budget Line\"\r\n                                          [(ngModel)]=\"item.BudgetLineId\"\r\n                                          (selectionChange)=\"\r\n                                            onTransactionDetailChanged(\r\n                                              item,\r\n                                              'BudgetLine'\r\n                                            )\r\n                                          \"\r\n                                          [disabled]=\"!isEditingAllowed\"\r\n                                        >\r\n                                          <mat-option\r\n                                            *ngFor=\"\r\n                                              let i of item.BudgetLineList\r\n                                            \"\r\n                                            [value]=\"i.BudgetLineId\"\r\n                                          >\r\n                                            {{ i.BudgetName }}\r\n                                          </mat-option>\r\n                                        </mat-select>\r\n                                      </mat-form-field> -->\r\n                                    </td>\r\n                                    <td width=\"12%\" class=\"text-center\">\r\n                                      Job: {{ item.JobName }}\r\n                                      <!-- <mat-form-field class=\"example-full-width\">\r\n                                      <mat-select placeholder=\"Project Job\">\r\n                                        <mat-option *ngFor=\"let i of projectJobList\" [value]=\"i.ProjectJobId\">\r\n                                          {{ i.JobName }}\r\n                                        </mat-option>\r\n                                      </mat-select>\r\n                                    </mat-form-field> -->\r\n                                    </td>\r\n                                    <td\r\n                                      width=\"12%\"\r\n                                      class=\"text-center\"\r\n                                      text-align=\"left\"\r\n                                    >\r\n                                      <button\r\n                                        mat-icon-button\r\n                                        (click)=\"onTransactionDebitDelete(item)\"\r\n                                        *ngIf=\"\r\n                                          isEditingAllowed &&\r\n                                          !voucherDetail.IsVoucherVerified\r\n                                        \"\r\n                                      >\r\n                                        <mat-icon aria-label=\"delete\"\r\n                                          >delete</mat-icon\r\n                                        >\r\n                                      </button>\r\n                                    </td>\r\n\r\n                                    <td width=\"1%\" class=\"text-center\"></td>\r\n                                  </tr>\r\n                                </tbody>\r\n                              </table>\r\n                            </div>\r\n                          </div>\r\n                        </div>\r\n                      </div>\r\n                      <!-- credits -->\r\n                      <div class=\"col-sm-12\">\r\n                        <div class=\"total_debit first_child clearfix\">\r\n                          <h6>\r\n                            <strong>Credits</strong>\r\n                            <span> Total Credits : {{ totalCredits }}</span>\r\n                          </h6>\r\n                          <button\r\n                            mat-icon-button\r\n                            (click)=\"onAddTransactionCredit()\"\r\n                            *ngIf=\"isEditingAllowed\"\r\n                          >\r\n                            <mat-icon aria-label=\"icon-button add icon\"\r\n                              >add</mat-icon\r\n                            >\r\n                          </button>\r\n                        </div>\r\n\r\n                        <!-- credit table -->\r\n                        <div class=\"row\">\r\n                          <div class=\"col-sm-12\">\r\n                            <div class=\"responsive_table-responsive \">\r\n                              <table class=\"table table-bordered\">\r\n                                <tbody>\r\n                                  <!-- <tr *ngFor=\"let item in transactionCreditList\"> -->\r\n                                  <tr\r\n                                    *ngFor=\"let item of transactionCreditList\"\r\n                                  >\r\n                                    <td width=\"1%\">\r\n                                      <p class=\"width_8\"></p>\r\n                                    </td>\r\n\r\n                                    <td width=\"12%\" class=\"text-center\">\r\n                                      <!-- <mat-form-field class=\"example-full-width\">\r\n                                      <mat-select placeholder=\"Account\" [(ngModel)]=\"item.AccountNo\" (selectionChange)=\"\r\n                                          onTransactionDetailChanged(item, 'Account')\r\n                                        \" [disabled]=\"!isEditingAllowed\">\r\n                                        <mat-option *ngFor=\"\r\n                                            let i of inputLevelAccountList\r\n                                          \" [value]=\"i.AccountCode\">\r\n                                          {{ i.AccountName }}\r\n                                        </mat-option>\r\n                                      </mat-select>\r\n                                    </mat-form-field> -->\r\n                                      <lib-search-dropdown\r\n                                        placeholder=\"Accounts\"\r\n                                        [multiSelect]=\"false\"\r\n                                        placeholderSearchLabel=\"Find Account...\"\r\n                                        noEntriesFoundLabel=\"No matching account found\"\r\n                                        [dataSource]=\"accountDataSource\"\r\n                                        [selectedValue]=\"item.AccountNo\"\r\n                                        (openedChange)=\"\r\n                                          openedChange($event, item)\r\n                                        \"\r\n                                      >\r\n                                      </lib-search-dropdown>\r\n                                    </td>\r\n                                    <td\r\n                                      width=\"16%\"\r\n                                      class=\"text-center\"\r\n                                      text-align=\"left\"\r\n                                    >\r\n                                      <mat-form-field\r\n                                        class=\"example-form-field\"\r\n                                      >\r\n                                        <input\r\n                                          matInput\r\n                                          type=\"text\"\r\n                                          placeholder=\"Description\"\r\n                                          [(ngModel)]=\"item.Description\"\r\n                                          (change)=\"\r\n                                            onTransactionDetailChanged(\r\n                                              item,\r\n                                              'Description'\r\n                                            )\r\n                                          \"\r\n                                          [disabled]=\"!isEditingAllowed\"\r\n                                        />\r\n                                      </mat-form-field>\r\n                                    </td>\r\n                                    <td\r\n                                      width=\"12%\"\r\n                                      class=\"text-center\"\r\n                                      text-align=\"left\"\r\n                                    >\r\n                                      <mat-form-field\r\n                                        class=\"example-form-field\"\r\n                                      >\r\n                                        <input\r\n                                          matInput\r\n                                          min=\"0\"\r\n                                          type=\"number\"\r\n                                          placeholder=\"Amount\"\r\n                                          [(ngModel)]=\"item.Credit\"\r\n                                          (change)=\"\r\n                                            onTransactionDetailChanged(\r\n                                              item,\r\n                                              'Credit'\r\n                                            )\r\n                                          \"\r\n                                          [disabled]=\"!isEditingAllowed\"\r\n                                        />\r\n                                      </mat-form-field>\r\n                                    </td>\r\n                                    <td width=\"12%\" class=\"text-center\">\r\n                                        <lib-search-dropdown\r\n                                        placeholder=\"Project\"\r\n                                        [multiSelect]=\"false\"\r\n                                        placeholderSearchLabel=\"Find Projects...\"\r\n                                        noEntriesFoundLabel=\"No matching Project found\"\r\n                                        [dataSource]=\"projectDropdownList\"\r\n                                        [selectedValue]=\"item.ProjectId\"\r\n                                        (selectionChanged)=\"\r\n                                        onOpenedProjectChange($event,item)\"\r\n                                      >\r\n                                      </lib-search-dropdown>\r\n                                    </td>\r\n                                    <td width=\"12%\" class=\"text-center\">\r\n                                      <lib-search-dropdown\r\n                                        placeholder=\"Budget Line\"\r\n                                        [multiSelect]=\"false\"\r\n                                        placeholderSearchLabel=\"Find BudgetLine...\"\r\n                                        noEntriesFoundLabel=\"No matching BudgetLine found\"\r\n                                        [dataSource]=\"item.BudgetLineList\"\r\n                                        [selectedValue]=\"item.BudgetLineId\"\r\n                                        (openedChange)=\"\r\n                                        onOpenedBudgetLineChange($event,item)\"\r\n                                      >\r\n                                      </lib-search-dropdown>\r\n                                      <!-- <mat-form-field\r\n                                        class=\"example-full-width\"\r\n                                      >\r\n                                        <mat-select\r\n                                          placeholder=\"Budget Line\"\r\n                                          [(ngModel)]=\"item.BudgetLineId\"\r\n                                          (selectionChange)=\"\r\n                                            onTransactionDetailChanged(\r\n                                              item,\r\n                                              'BudgetLine'\r\n                                            )\r\n                                          \"\r\n                                          [disabled]=\"!isEditingAllowed\"\r\n                                        >\r\n                                          <mat-option\r\n                                            *ngFor=\"\r\n                                              let i of item.BudgetLineList\r\n                                            \"\r\n                                            [value]=\"i.BudgetLineId\"\r\n                                          >\r\n                                            {{ i.BudgetName }}\r\n                                          </mat-option>\r\n                                        </mat-select>\r\n                                      </mat-form-field> -->\r\n                                    </td>\r\n                                    <td width=\"12%\" class=\"text-center\">\r\n                                      Job: {{ item.JobName }}\r\n                                      <!-- <mat-form-field class=\"example-full-width\">\r\n                                      <mat-select placeholder=\"Project Job\" [disabled]=\"!isEditingAllowed\" >\r\n                                        <mat-option *ngFor=\"let i of projectJobList\" [value]=\"i.ProjectJobId\">\r\n                                          {{ i.ProjectJobName }}\r\n                                        </mat-option>\r\n                                      </mat-select>\r\n                                    </mat-form-field> -->\r\n                                    </td>\r\n                                    <td\r\n                                      width=\"12%\"\r\n                                      class=\"text-center\"\r\n                                      text-align=\"left\"\r\n                                    >\r\n                                      <button\r\n                                        mat-icon-button\r\n                                        (click)=\"\r\n                                          onTransactionCreditDelete(item)\r\n                                        \"\r\n                                        *ngIf=\"\r\n                                          isEditingAllowed &&\r\n                                          !voucherDetail.IsVoucherVerified\r\n                                        \"\r\n                                      >\r\n                                        <mat-icon aria-label=\"delete\"\r\n                                          >delete</mat-icon\r\n                                        >\r\n                                      </button>\r\n                                    </td>\r\n\r\n                                    <td width=\"1%\" class=\"text-center\"></td>\r\n                                  </tr>\r\n                                </tbody>\r\n                              </table>\r\n                            </div>\r\n                          </div>\r\n                        </div>\r\n                      </div>\r\n                      <br />\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </ng-template>\r\n    </div>\r\n  </mat-card>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-details.component.scss":
/*!**********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-details/voucher-details.component.scss ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".margin_left_minus_5 {\n  margin-left: -5px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvdm91Y2hlcnMvdm91Y2hlci1kZXRhaWxzL2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXGRhc2hib2FyZFxcYWNjb3VudGluZ1xcdm91Y2hlcnNcXHZvdWNoZXItZGV0YWlsc1xcdm91Y2hlci1kZXRhaWxzLmNvbXBvbmVudC5zY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQXVCLGlCQUFpQixFQUFBIiwiZmlsZSI6InNyYy9hcHAvZGFzaGJvYXJkL2FjY291bnRpbmcvdm91Y2hlcnMvdm91Y2hlci1kZXRhaWxzL3ZvdWNoZXItZGV0YWlscy5jb21wb25lbnQuc2NzcyIsInNvdXJjZXNDb250ZW50IjpbIi5tYXJnaW5fbGVmdF9taW51c181IHsgbWFyZ2luLWxlZnQ6IC01cHg7fVxyXG4iXX0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-details.component.ts":
/*!********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-details/voucher-details.component.ts ***!
  \********************************************************************************************/
/*! exports provided: VoucherDetailsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VoucherDetailsComponent", function() { return VoucherDetailsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _voucher_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../voucher.service */ "./src/app/dashboard/accounting/vouchers/voucher.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var src_app_dashboard_project_management_project_list_budgetlines_budget_line_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/dashboard/project-management/project-list/budgetlines/budget-line.service */ "./src/app/dashboard/project-management/project-list/budgetlines/budget-line.service.ts");
/* harmony import */ var src_app_dashboard_project_management_project_list_project_jobs_project_jobs_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/dashboard/project-management/project-list/project-jobs/project-jobs.service */ "./src/app/dashboard/project-management/project-list/project-jobs/project-jobs.service.ts");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! rxjs/internal/operators/takeUntil */ "./node_modules/rxjs/internal/operators/takeUntil.js");
/* harmony import */ var rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! rxjs/internal/ReplaySubject */ "./node_modules/rxjs/internal/ReplaySubject.js");
/* harmony import */ var rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var projects_library_src_lib_components_document_listing_document_listing_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! projects/library/src/lib/components/document-listing/document-listing.component */ "./projects/library/src/lib/components/document-listing/document-listing.component.ts");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! src/app/shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var src_app_shared_global__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! src/app/shared/global */ "./src/app/shared/global.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};















var VoucherDetailsComponent = /** @class */ (function () {
    //#endregion
    function VoucherDetailsComponent(dialog, commonLoader, voucherService, toastr, budgetLineService, projectJobService, globalSharedService, appurl) {
        this.dialog = dialog;
        this.commonLoader = commonLoader;
        this.voucherService = voucherService;
        this.toastr = toastr;
        this.budgetLineService = budgetLineService;
        this.projectJobService = projectJobService;
        this.globalSharedService = globalSharedService;
        this.appurl = appurl;
        this.voucherDetailChanged = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.transactionCreditList = [];
        this.transactionDebitList = [];
        this.budgetLineDetailList = [];
        this.projectJobList = [];
        this.budgetLineLoader = false;
        this.transactionListTEMP = [];
        this.checkTransactionFlag = false;
        // loader
        this.voucherDetailLoader = false;
        this.fileUploadLoader = false;
        this.destroyed$ = new rxjs_internal_ReplaySubject__WEBPACK_IMPORTED_MODULE_9__["ReplaySubject"](1);
        this.getScreenSize();
    }
    VoucherDetailsComponent.prototype.ngOnInit = function () {
        this.initForm();
        this.getInputLevelAccountList();
    };
    //#region "ngOnChanges"
    VoucherDetailsComponent.prototype.ngOnChanges = function () {
        var _this = this;
        this.transactionListTEMP = [];
        if (this.voucherId !== 0 &&
            this.voucherId !== null &&
            this.voucherId !== undefined) {
            this.getVoucherDetailById(this.voucherId);
            this.getTransactionByVoucherId(this.voucherId);
            this.getAllProjectJobDetail();
        }
        this.projectDropdownList = [];
        this.projectList.forEach(function (x) { return _this.projectDropdownList.push({
            Id: x.ProjectId,
            Name: x.ProjectNameCode
        }); });
    };
    //#endregion
    VoucherDetailsComponent.prototype.initForm = function () {
        this.voucherDetail = {
            VoucherNo: null,
            ReferenceNo: null,
            Description: null,
            VoucherDate: null,
            CurrencyId: null,
            JournalCode: null,
            ChequeNo: null,
            VoucherTypeId: null,
            OfficeId: null,
            ProjectId: null,
            BudgetLineId: null,
            FinancialYearId: null,
            IsVoucherVerified: false
        };
    };
    //#region "Dynamic Scroll"
    VoucherDetailsComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 200 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    //#region "getInputLevelAccountList"
    VoucherDetailsComponent.prototype.getInputLevelAccountList = function () {
        var _this = this;
        this.voucherService.GetInputLevelAccountList().subscribe(function (response) {
            _this.debitaccountDataSource = [];
            _this.creditaccountDataSource = [];
            _this.accountDataSource = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.accountDataSource.push({
                        Id: element.AccountCode,
                        Name: element.AccountName
                    });
                    // if (element.AccountHeadTypeId === 2 || element.AccountHeadTypeId === 5 ) {
                    //   this.debitaccountDataSource.push({
                    //     Id: element.AccountCode,
                    //     Name: element.AccountName
                    //   });
                    // }
                    // if (element.AccountHeadTypeId === 1 || element.AccountHeadTypeId === 4 ) {
                    //   this.creditaccountDataSource.push({
                    //     Id: element.AccountCode,
                    //     Name: element.AccountName
                    //   });
                    // }
                });
                //// console.log(this.accountDataSource);
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getVoucherDetailById"
    VoucherDetailsComponent.prototype.getVoucherDetailById = function (id) {
        var _this = this;
        this.voucherDetailLoader = true;
        this.voucherService
            .GetVoucherDetailById(id)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.voucherDetail = null;
            if (response.statusCode === 200 && response.data !== null) {
                _this.voucherDetail = {
                    VoucherNo: response.data.VoucherNo,
                    ReferenceNo: response.data.ReferenceNo,
                    Description: response.data.Description,
                    VoucherDate: new Date(new Date(response.data.VoucherDate).getTime() -
                        new Date().getTimezoneOffset() * 60000),
                    CurrencyId: response.data.CurrencyId,
                    JournalCode: response.data.JournalCode,
                    ChequeNo: response.data.ChequeNo,
                    VoucherTypeId: response.data.VoucherTypeId,
                    OfficeId: response.data.OfficeId,
                    ProjectId: response.data.ProjectId,
                    BudgetLineId: response.data.BudgetLineId,
                    FinancialYearId: response.data.FinancialYearId,
                    IsVoucherVerified: response.data.IsVoucherVerified
                };
            }
            else if (response.statusCode === 400 && response.data === null) {
                _this.toastr.warning(response.message);
            }
            _this.voucherDetailLoader = false;
        }, function (error) {
            _this.voucherDetailLoader = false;
        });
    };
    //#endregion
    //#region "getTransactionByVoucherId"
    VoucherDetailsComponent.prototype.getTransactionByVoucherId = function (id) {
        var _this = this;
        this.transactionListTEMP = [];
        this.transactionCreditList = [];
        this.transactionDebitList = [];
        this.voucherService
            .GetTransactionByVoucherId(id)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            if (response.statusCode === 200 && response.data !== null) {
                _this.transactionCreditList = response.data.filter(function (x) { return x.Credit !== 0; });
                _this.transactionDebitList = response.data.filter(function (x) { return x.Debit !== 0; });
            }
            else if (response.statusCode === 400 && response.data === null) {
                _this.toastr.warning(response.message);
            }
        }, function (error) { });
    };
    //#endregion
    //#region "GetallBudgetLine list"
    VoucherDetailsComponent.prototype.getAllBudgetDetail = function () {
        var _this = this;
        this.voucherDetailLoader = true;
        this.voucherService
            .GetBudgetLineList()
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.budgetLineDetailList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.budgetLineDetailList.push({
                        ProjectJobId: element.ProjectJobId,
                        ProjectJobCode: element.ProjectJobCode,
                        ProjectJobName: element.ProjectJobName,
                        BudgetCode: element.BudgetCode,
                        BudgetLineId: element.BudgetLineId,
                        BudgetName: element.BudgetName,
                        CurrencyId: element.CurrencyId,
                        CurrencyName: element.CurrencyName,
                        InitialBudget: element.InitialBudget,
                        ProjectId: element.ProjectId,
                        CreatedDate: element.CreatedDate,
                        DebitPercentage: element.DebitPercentage
                    });
                });
            }
            else if (response.statusCode === 400 && response.data === null) {
                _this.toastr.warning(response.message);
            }
            _this.voucherDetailLoader = false;
        }, function (error) {
            _this.voucherDetailLoader = false;
        });
    };
    //#endregion
    //#region "GetallBudgetLine list"
    VoucherDetailsComponent.prototype.getAllProjectJobDetail = function () {
        var _this = this;
        this.voucherDetailLoader = true;
        this.voucherService
            .GetProjectobList()
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            _this.projectJobList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.projectJobList.push({
                        ProjectJobId: element.ProjectJobId,
                        ProjectJobCode: element.ProjectJobCode,
                        ProjectJobName: element.ProjectJobName
                    });
                });
            }
            else if (response.statusCode === 400 && response.data === null) {
                // this.toastr.warning(response.message);
            }
            _this.voucherDetailLoader = false;
        }, function (error) {
            _this.voucherDetailLoader = false;
        });
    };
    //#endregion
    //#region "addEditTransactionList"
    VoucherDetailsComponent.prototype.addEditTransactionList = function (data) {
        var _this = this;
        if (this.voucherId !== undefined) {
            var transactionModel = {
                VoucherNo: this.voucherId,
                VoucherTransactions: data
            };
            this.checkTransactionFlag = true;
            this.voucherService
                .AddEditTransactionList(transactionModel)
                .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
                .subscribe(function (response) {
                // this.voucherDetail = null;
                if (response.statusCode === 200) {
                    _this.toastr.success('Transaction Updated Successfully');
                    _this.getTransactionByVoucherId(_this.voucherId);
                }
                else if (response.statusCode === 400) {
                    _this.toastr.warning(response.message);
                }
                _this.commonLoader.hideLoader();
                _this.checkTransactionFlag = false;
            }, function (error) {
                _this.commonLoader.hideLoader();
                _this.toastr.error('Someting went wrong');
                _this.checkTransactionFlag = false;
            });
        }
        else {
            this.toastr.error('Voucher is undefined');
        }
    };
    //#endregion
    //#region "editVoucherDetailById"
    VoucherDetailsComponent.prototype.editVoucherDetailById = function (data) {
        var _this = this;
        this.checkTransactionFlag = true;
        var voucherDetails = {
            VoucherNo: data.VoucherNo,
            ReferenceNo: data.ReferenceNo,
            Description: data.Description,
            VoucherDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_7__["StaticUtilities"].getLocalDate(data.VoucherDate),
            CurrencyId: data.CurrencyId,
            JournalCode: data.JournalCode,
            ChequeNo: data.ChequeNo,
            VoucherTypeId: data.VoucherTypeId,
            OfficeId: data.OfficeId,
            ProjectId: data.ProjectId,
            BudgetLineId: data.BudgetLineId,
            FinancialYearId: data.FinancialYearId,
            IsVoucherVerified: data.IsVoucherVerified,
            TimezoneOffset: new Date().getTimezoneOffset()
        };
        this.voucherService
            .EditVoucherDetailById(voucherDetails)
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
            .subscribe(function (response) {
            if (response.statusCode === 200) {
                _this.voucherDetailChanged.emit(voucherDetails);
            }
            else if (response.statusCode === 400) {
                _this.toastr.warning(response.message);
            }
            _this.checkTransactionFlag = false;
        }, function (error) {
            _this.toastr.error('Someting went wrong');
            _this.checkTransactionFlag = false;
        });
    };
    //#endregion
    //#region "voucherVerify"
    VoucherDetailsComponent.prototype.voucherVerify = function (voucherNo) {
        var _this = this;
        if (voucherNo !== undefined) {
            // this.checkTransactionFlag = true;
            this.voucherDetail.IsVoucherVerified = !this.voucherDetail
                .IsVoucherVerified;
            this.voucherService
                .VoucherVerify(voucherNo)
                .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
                .subscribe(function (response) {
                if (response.statusCode === 200) {
                    // this.voucherDetail.IsVoucherVerified = response.data !== null ? response.data : false;
                    // this.toastr.success(response.message);
                }
                else if (response.statusCode === 400) {
                    _this.toastr.warning(response.message);
                }
                // this.checkTransactionFlag = false;
            }, function (error) {
                _this.toastr.error('Someting went wrong');
                // this.checkTransactionFlag = false;
            });
        }
    };
    //#endregion
    //#region "onVoucherValuechange"
    VoucherDetailsComponent.prototype.onVoucherValuechange = function () {
        this.editVoucherDetailById(this.voucherDetail);
    };
    //#endregion
    //#region "onTransactionCreditDelete"
    VoucherDetailsComponent.prototype.onTransactionCreditDelete = function (data) {
        var tempListIndex = null;
        var index = null;
        if (data.TransactionId !== 0) {
            tempListIndex = this.transactionListTEMP.findIndex(function (x) { return x.TransactionId === data.TransactionId; });
            index = this.transactionCreditList.findIndex(function (x) { return x.TransactionId === data.TransactionId; });
            // delete from list
            this.transactionCreditList.splice(index, 1);
            //#region "perform operation (add/edit on temp list)"
            data.IsDeleted = true;
            if (tempListIndex === -1) {
                // add to list
                this.transactionListTEMP.push(data);
            }
            else {
                this.transactionListTEMP[tempListIndex] = data;
            }
            //#endregion
        }
        else {
            tempListIndex = this.transactionListTEMP.findIndex(function (x) { return x._IsId === data._IsId; });
            index = this.transactionCreditList.findIndex(function (x) { return x._IsId === data._IsId; });
            // delete from list
            this.transactionCreditList.splice(index, 1);
            // delete from list
            this.transactionListTEMP.splice(tempListIndex, 1);
        }
    };
    //#endregion
    //#region "onTransactionDebitDelete"
    VoucherDetailsComponent.prototype.onTransactionDebitDelete = function (data) {
        var tempListIndex = null;
        var index = null;
        if (data.TransactionId !== 0) {
            tempListIndex = this.transactionListTEMP.findIndex(function (x) { return x.TransactionId === data.TransactionId; });
            index = this.transactionDebitList.findIndex(function (x) { return x.TransactionId === data.TransactionId; });
            // delete from list
            this.transactionDebitList.splice(index, 1);
            //#region "perform operation (add/edit on temp list)"
            data.IsDeleted = true;
            if (tempListIndex === -1) {
                // add to list
                this.transactionListTEMP.push(data);
            }
            else {
                this.transactionListTEMP[tempListIndex] = data;
            }
            //#endregion
        }
        else {
            tempListIndex = this.transactionListTEMP.findIndex(function (x) { return x._IsId === data._IsId; });
            index = this.transactionDebitList.findIndex(function (x) { return x._IsId === data._IsId; });
            // delete from list
            this.transactionDebitList.splice(index, 1);
            // delete from list
            this.transactionListTEMP.splice(tempListIndex, 1);
        }
    };
    //#endregion
    //#region "onTransactionDetailChanged"
    VoucherDetailsComponent.prototype.onTransactionDetailChanged = function (item, elementName) {
        var tempListIndex = null;
        //#region "searh / find"
        if (item.TransactionId !== 0) {
            tempListIndex = this.transactionListTEMP.findIndex(function (x) { return x.TransactionId === item.TransactionId; });
        }
        else {
            tempListIndex = this.transactionListTEMP.findIndex(function (x) { return x._IsId === item._IsId; });
        }
        //#endregion
        //#region "perform operation (add/edit on temp list)"
        item.IsDeleted = false;
        if (tempListIndex === -1) {
            // add to list
            this.transactionListTEMP.push(item);
        }
        else {
            this.transactionListTEMP[tempListIndex] = item;
        }
        //#endregion
        if (item.ProjectId !== null && item.ProjectId !== undefined) {
            this.getBudgetLineOnProjectId(item, elementName);
        }
        if (item.BudgetLineId !== undefined && item.BudgetLineId !== null) {
            this.GetProjectJobDetailByBudgetLineId(item);
        }
    };
    //#endregion
    VoucherDetailsComponent.prototype.onOpenedBudgetLineChange = function (event, item) {
        item.BudgetLineId = event.Value;
        this.onTransactionDetailChanged(item, 'BudgetLine');
    };
    VoucherDetailsComponent.prototype.onOpenedProjectChange = function (event, item) {
        item.ProjectId = event;
        this.onTransactionDetailChanged(item, 'Project');
    };
    //#region "getBudgetLineOnProjectId"
    VoucherDetailsComponent.prototype.GetProjectJobDetailByBudgetLineId = function (item) {
        var _this = this;
        this.projectJobService
            .GetProjectJobDetailByBudgetLineId(item.BudgetLineId)
            .subscribe(function (response) {
            // this.voucherDetail = null;
            if (response.statusCode === 200) {
                item.JobName = response.data.ProjectJobName;
                item.JobId = response.data.ProjectJobId;
                // this.toastr.success('Transaction Deleted Successfully');
            }
            else if (response.statusCode === 400) {
                _this.toastr.warning(response.message);
            }
        }, function (error) {
            _this.toastr.error('Someting went wrong');
        });
    };
    //#endregion
    //#region "getBudgetLineOnProjectId"
    VoucherDetailsComponent.prototype.getBudgetLineOnProjectId = function (item, elementName) {
        var _this = this;
        this.budgetLineLoader = true;
        if (elementName === 'Project') {
            item.BudgetLineId = null;
        }
        item.BudgetLineList = [];
        item.JobName = '';
        this.budgetLineService.GetProjectBudgetLineList(item.ProjectId).subscribe(function (response) {
            // this.voucherDetail = null;
            if (response.statusCode === 200) {
                item.BudgetLineList = [];
                // response.data.forEach(x => item.BudgetLineList.push(x));
                response.data.forEach(function (x) { return item.BudgetLineList.push({
                    Id: x.BudgetLineId,
                    Name: x.BudgetCodeName
                }); });
                // item.BudgetLineList.forEach(e => {
                //   this.BudgetLineDropdown.push({
                //     Id: e.BudgetLineId,
                //     Name: e.BudgetCodeName
                //   });
                // });
                // this.toastr.success('Transaction Deleted Successfully');
            }
            else if (response.statusCode === 400) {
                _this.toastr.warning(response.message);
            }
            _this.budgetLineLoader = false;
        }, function (error) {
            _this.budgetLineLoader = false;
            _this.toastr.error('Someting went wrong');
        });
    };
    //#endregion
    //#region "onAddTransactionCredit"
    VoucherDetailsComponent.prototype.onAddTransactionCredit = function () {
        var transactiondata = {
            TransactionId: 0,
            VoucherNo: this.voucherId,
            AccountNo: null,
            Description: '',
            ProjectId: null,
            BudgetLineId: null,
            Credit: 0,
            Debit: 0,
            IsDeleted: false,
            JobId: 0,
            JobName: '',
            BudgetLineList: [],
            _IsId: this.RandomNum(),
            _IsDeleted: false
        };
        this.transactionListTEMP.push(transactiondata);
        this.transactionCreditList.push(transactiondata);
    };
    //#endregion
    //#region "onAddTransactionDebit"
    VoucherDetailsComponent.prototype.onAddTransactionDebit = function () {
        var transactiondata = {
            TransactionId: 0,
            VoucherNo: this.voucherId,
            AccountNo: null,
            Description: '',
            ProjectId: null,
            BudgetLineId: null,
            Credit: 0,
            Debit: 0,
            IsDeleted: false,
            JobId: 0,
            JobName: '',
            BudgetLineList: [],
            _IsId: this.RandomNum(),
            _IsDeleted: false
        };
        this.transactionListTEMP.push(transactiondata);
        this.transactionDebitList.push(transactiondata);
    };
    //#endregion
    //#region "onTransactionListVerify"
    VoucherDetailsComponent.prototype.onTransactionListVerify = function () {
        if (this.totalCredits === this.totalDebits) {
            if (this.checkZeroCredits() > 0 || this.checkZeroDebits() > 0) {
                this.toastr.warning('Amount must be greater than 0');
            }
            else {
                if (this.transactionListTEMP.find(function (x) { return x.AccountNo == null; }) === undefined) {
                    this.transactionListTEMP.length > 0
                        ? this.addEditTransactionList(this.transactionListTEMP)
                        : this.toastr.info('No transaction to edit');
                }
                else {
                    this.toastr.warning('Please select Account No');
                }
            }
        }
        else {
            this.toastr.error('Transaction is unbalanced');
        }
    };
    //#endregion
    //#region "onVoucherVerify"
    VoucherDetailsComponent.prototype.onVoucherVerify = function () {
        if (this.totalCredits === this.totalDebits) {
            if (this.checkZeroCredits() > 0 || this.checkZeroDebits() > 0) {
                this.toastr.warning('Amount must be greater than 0');
            }
            else {
                if (this.transactionListTEMP.find(function (x) { return x.AccountNo == null; }) === undefined) {
                    if (this.transactionListTEMP.length > 0) {
                        this.toastr.warning('First save the transactions');
                    }
                    else {
                        this.voucherVerify(this.voucherId);
                    }
                }
                else {
                    this.toastr.warning('Please select Account No');
                }
            }
        }
        else {
            this.toastr.error('Transaction is unbalanced');
        }
    };
    //#endregion
    //#region "openDocumentsDialog"
    VoucherDetailsComponent.prototype.openDocumentsDialog = function () {
        var dialogRef = this.dialog.open(projects_library_src_lib_components_document_listing_document_listing_component__WEBPACK_IMPORTED_MODULE_10__["DocumentListingComponent"], {
            width: '400px',
            minHeight: '300px',
            maxHeight: '500px',
            autoFocus: false,
            data: { pageId: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["FileSourceEntityTypes"].Voucher, recordId: this.voucherId }
        });
        // delete
        // dialogRef.componentInstance.deleteDocument.subscribe(
        //   (item: IDocumentsModel) => {
        //     this.deleteDocument(item);
        //   }
        // );
        // dialogRef.componentInstance.documentListRefresh.subscribe(
        //   (item: IDocumentsModel) => {
        //     this.documentListRefresh.emit(item);
        //   }
        // );
        // dialogRef.componentInstance.documentDownload.subscribe(
        //   (item: IDocumentsModel) => {
        //     this.documentDownload.emit(item);
        //   }
        // );
        dialogRef.afterClosed().subscribe(function (result) { });
    };
    //#endregion
    VoucherDetailsComponent.prototype.openedChange = function (event, Item) {
        Item.AccountNo = event.Value !== undefined ? event.Value : Item.AccountNo;
        this.onTransactionDetailChanged(Item, 'Account');
    };
    //#region "RandomNum" NOTE: Use for Add functionality
    VoucherDetailsComponent.prototype.RandomNum = function () {
        return Math.floor(Math.random() * 10000);
    };
    Object.defineProperty(VoucherDetailsComponent.prototype, "totalCredits", {
        //#endregion
        //#region "Totals" NOTE: DONT CHANGE
        get: function () {
            return this.transactionCreditList.reduce(function (a, _a) {
                var Credit = _a.Credit;
                return a + Credit;
            }, 0);
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(VoucherDetailsComponent.prototype, "totalDebits", {
        get: function () {
            return this.transactionDebitList.reduce(function (a, _a) {
                var Debit = _a.Debit;
                return a + Debit;
            }, 0);
        },
        enumerable: true,
        configurable: true
    });
    VoucherDetailsComponent.prototype.checkZeroCredits = function () {
        return this.transactionCreditList.filter(function (x) { return x.Credit === 0; }).length;
    };
    VoucherDetailsComponent.prototype.checkZeroDebits = function () {
        return this.transactionDebitList.filter(function (x) { return x.Debit === 0; }).length;
    };
    //#endregion
    /**
     * this is used to trigger the input
     */
    VoucherDetailsComponent.prototype.openInput = function () {
        // your can use ElementRef for this later
        document.getElementById('fileInput').click();
    };
    VoucherDetailsComponent.prototype.fileChange = function (files) {
        var _this = this;
        if (files.length > 0) {
            this.fileUploadLoader = true;
            for (var i = 0; i < files.length; i++) {
                this.globalSharedService
                    .uploadFile(src_app_shared_enum__WEBPACK_IMPORTED_MODULE_12__["FileSourceEntityTypes"].Voucher, this.voucherId, files[i])
                    .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
                    .subscribe(function (x) { return (_this.fileUploadLoader = false); });
            }
        }
    };
    VoucherDetailsComponent.prototype.onExportPdf = function () {
        this.globalSharedService
            .getFile(this.appurl.getApiUrl() + src_app_shared_global__WEBPACK_IMPORTED_MODULE_14__["GLOBAL"].API_Pdf_GetAllVoucherSummaryReportPdf, { VoucherId: this.voucherId })
            .pipe(Object(rxjs_internal_operators_takeUntil__WEBPACK_IMPORTED_MODULE_8__["takeUntil"])(this.destroyed$))
            .subscribe();
    };
    VoucherDetailsComponent.prototype.ngOnDestroy = function () {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], VoucherDetailsComponent.prototype, "voucherId", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], VoucherDetailsComponent.prototype, "journalList", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], VoucherDetailsComponent.prototype, "currencyList", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], VoucherDetailsComponent.prototype, "officeList", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Array)
    ], VoucherDetailsComponent.prototype, "projectList", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], VoucherDetailsComponent.prototype, "voucherTypeList", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Boolean)
    ], VoucherDetailsComponent.prototype, "isEditingAllowed", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], VoucherDetailsComponent.prototype, "voucherDetailChanged", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], VoucherDetailsComponent.prototype, "getScreenSize", null);
    VoucherDetailsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-voucher-details',
            template: __webpack_require__(/*! ./voucher-details.component.html */ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-details.component.html"),
            styles: [__webpack_require__(/*! ./voucher-details.component.scss */ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-details.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MatDialog"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_4__["CommonLoaderService"],
            _voucher_service__WEBPACK_IMPORTED_MODULE_2__["VoucherService"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            src_app_dashboard_project_management_project_list_budgetlines_budget_line_service__WEBPACK_IMPORTED_MODULE_5__["BudgetLineService"],
            src_app_dashboard_project_management_project_list_project_jobs_project_jobs_service__WEBPACK_IMPORTED_MODULE_6__["ProjectJobsService"],
            src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_11__["GlobalSharedService"],
            src_app_shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_13__["AppUrlService"]])
    ], VoucherDetailsComponent);
    return VoucherDetailsComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-summary/voucher-summary.component.html":
/*!**************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-details/voucher-summary/voucher-summary.component.html ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"voucher-summary-main\">\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-4\">\r\n      <mat-card>\r\n        <h6>Current Debit</h6>\r\n        <div class=\"font_size_smaller\">{{currentDebit | number: \"1.2-2\"}}</div>\r\n      </mat-card>\r\n    </div>\r\n    <div class=\"col-sm-4\">\r\n      <mat-card >\r\n        <h6>Current Credit</h6>\r\n        <div class=\"font_size_smaller\">{{currentCredit | number: \"1.2-2\"}}</div></mat-card\r\n      >\r\n    </div>\r\n    <div class=\"col-sm-4\">\r\n      <mat-card>\r\n        <h6>Balance</h6>\r\n        <div class=\"font_size_smaller\">{{currentDebit - currentCredit | number: \"1.2-2\"}}</div></mat-card\r\n      >\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-summary/voucher-summary.component.scss":
/*!**************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-details/voucher-summary/voucher-summary.component.scss ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL3ZvdWNoZXJzL3ZvdWNoZXItZGV0YWlscy92b3VjaGVyLXN1bW1hcnkvdm91Y2hlci1zdW1tYXJ5LmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-summary/voucher-summary.component.ts":
/*!************************************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-details/voucher-summary/voucher-summary.component.ts ***!
  \************************************************************************************************************/
/*! exports provided: VoucherSummaryComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VoucherSummaryComponent", function() { return VoucherSummaryComponent; });
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

var VoucherSummaryComponent = /** @class */ (function () {
    function VoucherSummaryComponent() {
    }
    VoucherSummaryComponent.prototype.ngOnInit = function () {
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], VoucherSummaryComponent.prototype, "currentCredit", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], VoucherSummaryComponent.prototype, "currentDebit", void 0);
    VoucherSummaryComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-voucher-summary',
            template: __webpack_require__(/*! ./voucher-summary.component.html */ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-summary/voucher-summary.component.html"),
            styles: [__webpack_require__(/*! ./voucher-summary.component.scss */ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-summary/voucher-summary.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], VoucherSummaryComponent);
    return VoucherSummaryComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.html":
/*!**********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.html ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"voucher-listing-main\">\r\n  <div class=\"body-content\">\r\n    <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n        <div [ngStyle]=\"scrollStyles\" [class]=\"colsm6\">\r\n          <as-split direction=\"horizontal\">\r\n            <as-split-area [size]=\"listingScreenWidth\">\r\n              <div #voucherListing>\r\n                <mat-card [ngStyle]=\"scrollStyles\">\r\n                  <div>\r\n                    <button\r\n                      mat-raised-button\r\n                      color=\"accent\"\r\n                      (click)=\"openAddVoucherDialog()\"\r\n                      *ngIf=\"isEditingAllowed\"\r\n                    >\r\n                    <mat-icon aria-label=\"add\">add</mat-icon>\r\n\r\n                      Add\r\n                    </button>\r\n                    <hr />\r\n                    <div class=\"row\">\r\n                      <div class=\"col-sm-12\">\r\n                        <mat-form-field class=\"example-form-field\">\r\n                          <input\r\n                            matInput\r\n                            placeholder=\"Search\"\r\n                            [(ngModel)]=\"voucherFilter.FilterValue\"\r\n                            (keyup.enter)=\"\r\n                              voucherFilter.FilterValue != ''\r\n                                ? onFilterApplied()\r\n                                : null\r\n                            \"\r\n                          />\r\n                        </mat-form-field>\r\n                        <!-- Apply -->\r\n                        &nbsp;\r\n                        <button\r\n                          mat-raised-button\r\n                          color=\"accent\"\r\n                          class=\"margin_left_10\"\r\n                          [disabled]=\"voucherFilter.FilterValue === ''\"\r\n                          (click)=\"onFilterApplied()\"\r\n                        >\r\n                          <i class=\"fa fa-filter\" aria-hidden=\"true\"></i>\r\n                          Apply\r\n                        </button>\r\n                        &nbsp;\r\n                        <!-- Reset -->\r\n                        <button mat-raised-button (click)=\"onFilterReset()\">\r\n                          Reset\r\n                        </button>\r\n\r\n                        <!-- Filter -->\r\n                        <button\r\n                          mat-icon-button\r\n                          class=\"margin_left_10\"\r\n                          [matMenuTriggerFor]=\"menu\"\r\n                        >\r\n                          <mat-icon>more_vert</mat-icon>\r\n                        </button>\r\n                        <mat-menu #menu=\"matMenu\">\r\n                          <button\r\n                            mat-menu-item\r\n                            (click)=\"$event.stopPropagation()\"\r\n                          >\r\n                            <mat-checkbox\r\n                              [(ngModel)]=\"voucherFilter.VoucherNoFlag\"\r\n                              >Voucher No</mat-checkbox\r\n                            >\r\n                          </button>\r\n                          <button\r\n                            mat-menu-item\r\n                            (click)=\"$event.stopPropagation()\"\r\n                          >\r\n                            <mat-checkbox\r\n                              [(ngModel)]=\"voucherFilter.ReferenceNoFlag\"\r\n                              >Reference No</mat-checkbox\r\n                            >\r\n                          </button>\r\n                          <button\r\n                            mat-menu-item\r\n                            (click)=\"$event.stopPropagation()\"\r\n                          >\r\n                            <mat-checkbox\r\n                              [(ngModel)]=\"voucherFilter.DescriptionFlag\"\r\n                              >Description</mat-checkbox\r\n                            >\r\n                          </button>\r\n                          <button\r\n                            mat-menu-item\r\n                            (click)=\"$event.stopPropagation()\"\r\n                          >\r\n                            <mat-checkbox\r\n                              [(ngModel)]=\"voucherFilter.JournalNameFlag\"\r\n                              >Journal Name</mat-checkbox\r\n                            >\r\n                          </button>\r\n                          <button\r\n                            mat-menu-item\r\n                            (click)=\"$event.stopPropagation()\"\r\n                          >\r\n                            <mat-checkbox [(ngModel)]=\"voucherFilter.DateFlag\"\r\n                              >Date</mat-checkbox\r\n                            >\r\n                          </button>\r\n                        </mat-menu>\r\n                      </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                      <div class=\"col-sm-12\">\r\n                        {{ voucherFilter.totalCount }} Vouchers\r\n                      </div>\r\n                    </div>\r\n                    <br />\r\n                    <div class=\"row\">\r\n                      <div\r\n                        *ngIf=\"voucherListLoaderFlag; else voucherListTemplate\"\r\n                        class=\"col-sm-12\"\r\n                      >\r\n                        <mat-spinner\r\n                          class=\"center_loader\"\r\n                          diameter=\"50\"\r\n                        ></mat-spinner>\r\n                      </div>\r\n                      <ng-template #voucherListTemplate>\r\n                        <div class=\"col-sm-12\">\r\n                          <div class=\"responsive_table-responsive\">\r\n                            <table class=\"table table-bordered\">\r\n                              <tbody>\r\n                                <tr>\r\n                                  <td width=\"1%\"><p class=\"width_8\"></p></td>\r\n                                  <td width=\"5%\" class=\"text-center\">\r\n                                    Voucher No\r\n                                  </td>\r\n                                  <td width=\"20%\" class=\"text-left\">\r\n                                    Reference No\r\n                                  </td>\r\n                                  <td width=\"40%\" class=\"text-left\">\r\n                                    Description\r\n                                  </td>\r\n                                  <td width=\"20%\" class=\"text-center\">\r\n                                    Journal Name\r\n                                  </td>\r\n                                  <td width=\"5%\" class=\"text-center\">\r\n                                    Voucher Date\r\n                                  </td>\r\n                                  <td width=\"1%\" class=\"text-center\"></td>\r\n                                </tr>\r\n                                <tr\r\n                                  (click)=\"onItemClick(item.VoucherNo)\"\r\n                                  *ngFor=\"let item of voucherList\"\r\n                                  [ngClass]=\"{\r\n                                    selected:\r\n                                      selectedVoucherId == item.VoucherNo\r\n                                  }\"\r\n                                >\r\n                                  <td width=\"1%\"><p class=\"width_8\"></p></td>\r\n                                  <td width=\"5%\" class=\"grey-text\">\r\n                                    {{ item.VoucherNo }}\r\n                                  </td>\r\n                                  <td width=\"20%\" text-align=\"left\">\r\n                                    <p class=\"diamond\">\r\n                                      {{ item.ReferenceNo }}\r\n                                    </p>\r\n                                  </td>\r\n                                  <td width=\"40%\" text-align=\"left\">\r\n                                    <p class=\"text_overflow\">\r\n                                      {{ item.Description }}\r\n                                    </p>\r\n                                  </td>\r\n                                  <td width=\"20%\" class=\"text-center\">\r\n                                    {{ item.JournalName }}\r\n                                  </td>\r\n                                  <td width=\"5%\" class=\"text-center\">\r\n                                    {{ item.VoucherDate | date: \"dd/MM/yyyy\" }}\r\n                                  </td>\r\n                                  <td width=\"1%\" class=\"text-center\">\r\n                                    <i\r\n                                      *ngIf=\"\r\n                                        selectedVoucherId == item.VoucherNo\r\n                                      \"\r\n                                      class=\"material-icons font-12\"\r\n                                    >\r\n                                      arrow_forward_ios\r\n                                    </i>\r\n                                  </td>\r\n                                </tr>\r\n                              </tbody>\r\n                            </table>\r\n                          </div>\r\n                          <mat-paginator\r\n                            [length]=\"voucherFilter.totalCount\"\r\n                            [pageSize]=\"voucherFilter.pageSize\"\r\n                            [pageIndex]=\"voucherFilter.pageIndex\"\r\n                            [pageSizeOptions]=\"[5, 10, 25, 100]\"\r\n                            (page)=\"pageEvent($event)\"\r\n                          >\r\n                          </mat-paginator>\r\n                        </div>\r\n                      </ng-template>\r\n                    </div>\r\n                  </div>\r\n                </mat-card>\r\n              </div>\r\n            </as-split-area>\r\n\r\n            <!-- voucher detail -->\r\n            <as-split-area [size]=\"detailScreenWidth\" [hidden]=\"!showVoucherDetail\">\r\n              <div>\r\n                <app-voucher-details\r\n                  [voucherId]=\"selectedVoucherId\"\r\n                  [journalList]=\"journalList\"\r\n                  [currencyList]=\"currencyList\"\r\n                  [officeList]=\"officeList\"\r\n                  [projectList]=\"projectList\"\r\n                  [voucherTypeList]=\"voucherTypeList\"\r\n                  (voucherDetailChanged)=\"voucherDetailChangedEmit($event)\"\r\n                  [isEditingAllowed]=\"isEditingAllowed\"\r\n                ></app-voucher-details>\r\n              </div>\r\n              </as-split-area>\r\n          </as-split>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.scss":
/*!**********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.scss ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL3ZvdWNoZXJzL3ZvdWNoZXItbGlzdGluZy92b3VjaGVyLWxpc3RpbmcuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.ts":
/*!********************************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.ts ***!
  \********************************************************************************************/
/*! exports provided: VoucherListingComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VoucherListingComponent", function() { return VoucherListingComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _voucher_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../voucher.service */ "./src/app/dashboard/accounting/vouchers/voucher.service.ts");
/* harmony import */ var _voucher_add_voucher_add_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../voucher-add/voucher-add.component */ "./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.ts");
/* harmony import */ var src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/applicationpagesenum */ "./src/app/shared/applicationpagesenum.ts");
/* harmony import */ var src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/localstorage.service */ "./src/app/shared/services/localstorage.service.ts");
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







var VoucherListingComponent = /** @class */ (function () {
    //#endregion
    function VoucherListingComponent(dialog, voucherService, localStorageService) {
        this.dialog = dialog;
        this.voucherService = voucherService;
        this.localStorageService = localStorageService;
        //#region "Variables"
        // detail panel
        this.colsm6 = 'col-sm-10 col-sm-offset-1';
        this.showVoucherDetail = false;
        this.voucherList = [];
        this.voucherTypeList = [];
        this.journalList = [];
        this.currencyList = [];
        this.officeList = [];
        this.projectList = [];
        this.budgetLineList = [];
        this.voucherListLoaderFlag = false;
        this.isEditingAllowed = false;
        this.pageId = src_app_shared_applicationpagesenum__WEBPACK_IMPORTED_MODULE_4__["ApplicationPages"].Vouchers;
        this.listingScreenWidth = 100;
        this.detailScreenWidth = 0;
        this.destroyed$ = new rxjs__WEBPACK_IMPORTED_MODULE_6__["ReplaySubject"](1);
        this.getScreenSize();
    }
    //#region "Dynamic Scroll"
    VoucherListingComponent.prototype.getScreenSize = function (event) {
        this.screenHeight = window.innerHeight;
        this.screenWidth = window.innerWidth;
        this.scrollStyles = {
            'overflow-y': 'auto',
            height: this.screenHeight - 110 + 'px',
            'overflow-x': 'hidden'
        };
    };
    //#endregion
    //#region
    // ngAfterViewInit() {
    //   this.viewHeight = this.elementView.nativeElement.offsetHeight;
    //   this.scrollStyles = {
    //     'overflow-y': 'auto',
    //     'height': this.screenHeight - 110 + 'px',
    //     };
    // }
    //#endregion
    VoucherListingComponent.prototype.ngOnInit = function () {
        this.initVoucherFilter();
        this.getVoucherTypeList();
        this.getJournalList();
        this.getCurrencyList();
        this.getProjectList();
        // this.getBudgetLineList();
        this.getOfficeList();
        this.getVoucherList();
        this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
    };
    //#region "initVoucherFilter"
    VoucherListingComponent.prototype.initVoucherFilter = function () {
        this.voucherFilter = {
            FilterValue: '',
            VoucherNoFlag: true,
            ReferenceNoFlag: true,
            DescriptionFlag: true,
            JournalNameFlag: true,
            DateFlag: true,
            pageIndex: 0,
            pageSize: 10,
            totalCount: 0
        };
    };
    //#endregion
    //#region "getVoucherList"
    VoucherListingComponent.prototype.getVoucherList = function () {
        var _this = this;
        // init count
        this.voucherFilter.totalCount = 0;
        // this.commonLoader.showLoader();
        this.voucherListLoaderFlag = true;
        this.voucherService.GetVoucherList(this.voucherFilter).subscribe(function (response) {
            _this.voucherList = [];
            if (response.StatusCode === 200 &&
                response.data.VoucherDetailList != null) {
                if (response.data.VoucherDetailList.length > 0) {
                    _this.voucherFilter.totalCount =
                        response.data.TotalCount != null ? response.data.TotalCount : 0;
                    response.data.VoucherDetailList.forEach(function (element) {
                        _this.voucherList.push({
                            VoucherNo: element.VoucherNo,
                            CurrencyCode: element.CurrencyCode,
                            CurrencyId: element.CurrencyId,
                            VoucherDate: element.VoucherDate != null
                                ? new Date(new Date(element.VoucherDate).getTime() -
                                    new Date().getTimezoneOffset() * 60000)
                                : null,
                            ChequeNo: element.ChequeNo,
                            ReferenceNo: element.ReferenceNo,
                            Description: element.Description,
                            JournalName: element.JournalName,
                            JournalCode: element.JournalCode,
                            VoucherTypeId: element.VoucherTypeId,
                            OfficeId: element.OfficeId,
                            ProjectId: element.ProjectId,
                            BudgetLineId: element.BudgetLineId,
                            OfficeName: element.OfficeName
                        });
                    });
                }
            }
            // this.commonLoader.hideLoader();
            _this.voucherListLoaderFlag = false;
        }, function (error) {
            _this.voucherListLoaderFlag = false;
            // this.commonLoader.hideLoader();
        });
    };
    //#endregion
    //#region "getVoucherTypeList"
    VoucherListingComponent.prototype.getVoucherTypeList = function () {
        var _this = this;
        this.voucherService.GetVoucherTypeList().subscribe(function (response) {
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
    //#region "getJournalList"
    VoucherListingComponent.prototype.getJournalList = function () {
        var _this = this;
        this.voucherService.GetJournalList().subscribe(function (response) {
            _this.journalList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.journalList.push({
                        JournalCode: element.JournalCode,
                        JournalName: element.JournalName,
                        JournalType: element.JournalType
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getCurrencyList"
    VoucherListingComponent.prototype.getCurrencyList = function () {
        var _this = this;
        this.voucherService.GetCurrencyList().subscribe(function (response) {
            _this.currencyList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.currencyList.push({
                        CurrencyId: element.CurrencyId,
                        CurrencyName: element.CurrencyName
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "getOfficeList"
    VoucherListingComponent.prototype.getOfficeList = function () {
        var _this = this;
        this.voucherService.GetOfficeList().subscribe(function (response) {
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
    //#region "getProjectList"
    VoucherListingComponent.prototype.getProjectList = function () {
        var _this = this;
        this.voucherService.GetProjectList().subscribe(function (response) {
            _this.projectList = [];
            if (response.statusCode === 200 && response.data !== null) {
                response.data.forEach(function (element) {
                    _this.projectList.push({
                        ProjectId: element.ProjectId,
                        ProjectCode: element.ProjectCode,
                        ProjectName: element.ProjectName,
                        ProjectNameCode: element.ProjectCode + ' - ' + element.ProjectName
                    });
                });
            }
        }, function (error) { });
    };
    //#endregion
    //#region "onFilterApplied"
    VoucherListingComponent.prototype.onFilterApplied = function () {
        // back to index 0
        this.voucherFilter.pageIndex = 0;
        this.getVoucherList();
    };
    //#endregion
    //#region "onFilterReset"
    VoucherListingComponent.prototype.onFilterReset = function () {
        this.initVoucherFilter();
        this.getVoucherList();
    };
    //#endregion
    //#region "pageEvent"
    VoucherListingComponent.prototype.pageEvent = function (e) {
        this.voucherFilter.pageIndex = e.pageIndex;
        this.voucherFilter.pageSize = e.pageSize;
        // this.voucherFilter.totalCount =  e.length;
        this.getVoucherList();
    };
    //#endregion
    //#region "onItemClick"
    VoucherListingComponent.prototype.onItemClick = function (item) {
        this.selectedVoucherId = item;
        this.showVoucherDetailPanel();
    };
    //#endregion
    //#region "show/ hide"
    VoucherListingComponent.prototype.showVoucherDetailPanel = function () {
        this.showVoucherDetail = true;
        this.colsm6 = this.showVoucherDetail
            ? 'col-sm-12'
            : 'col-sm-10 col-sm-offset-1';
        this.listingScreenWidth = 50;
        this.detailScreenWidth = 50;
    };
    // hideVoucherDetailPanel() {
    //   this.showVoucherDetail = false;
    //   this.colsm6 = this.showVoucherDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
    // }
    //#endregion
    //#region "Add Voucher Popup"
    VoucherListingComponent.prototype.openAddVoucherDialog = function () {
        // NOTE: It passed the data into the Add Voucher Model
        var _this = this;
        var dialogRef = this.dialog.open(_voucher_add_voucher_add_component__WEBPACK_IMPORTED_MODULE_3__["VoucherAddComponent"], {
            width: '550px',
            data: {
                data: 'hello',
                journalList: this.journalList,
                currencyList: this.currencyList,
                officeList: this.officeList,
                projectList: this.projectList,
                budgetLineList: this.budgetLineList,
                voucherTypeList: this.voucherTypeList
            }
        });
        dialogRef.componentInstance.onListRefresh.subscribe(function () {
            // do something
            _this.getVoucherList();
        });
        dialogRef.afterClosed().subscribe(function (result) { });
    };
    //#endregion
    //#region "voucherDetailChangedEmit"
    VoucherListingComponent.prototype.voucherDetailChangedEmit = function (e) {
        var data = this.voucherList.find(function (x) { return x.VoucherNo === e.VoucherNo; });
        var indexOfVoucher = this.voucherList.indexOf(data);
        // set journal to journal in table
        e.JournalName = this.journalList.find(function (x) { return x.JournalCode === e.JournalCode; }).JournalName;
        this.voucherList[indexOfVoucher] = e;
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])('window:resize', ['$event']),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", [Object]),
        __metadata("design:returntype", void 0)
    ], VoucherListingComponent.prototype, "getScreenSize", null);
    VoucherListingComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-voucher-listing',
            template: __webpack_require__(/*! ./voucher-listing.component.html */ "./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.html"),
            styles: [__webpack_require__(/*! ./voucher-listing.component.scss */ "./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_material_dialog__WEBPACK_IMPORTED_MODULE_1__["MatDialog"],
            _voucher_service__WEBPACK_IMPORTED_MODULE_2__["VoucherService"],
            src_app_shared_services_localstorage_service__WEBPACK_IMPORTED_MODULE_5__["LocalStorageService"]])
    ], VoucherListingComponent);
    return VoucherListingComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/vouchers-routing.module.ts":
/*!**************************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/vouchers-routing.module.ts ***!
  \**************************************************************************/
/*! exports provided: VouchersRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VouchersRoutingModule", function() { return VouchersRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _vouchers_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./vouchers.component */ "./src/app/dashboard/accounting/vouchers/vouchers.component.ts");
/* harmony import */ var _voucher_listing_voucher_listing_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./voucher-listing/voucher-listing.component */ "./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var routes = [
    {
        path: '',
        component: _vouchers_component__WEBPACK_IMPORTED_MODULE_2__["VouchersComponent"],
        children: [
            {
                path: '',
                component: _voucher_listing_voucher_listing_component__WEBPACK_IMPORTED_MODULE_3__["VoucherListingComponent"]
            }
        ]
    }
];
var VouchersRoutingModule = /** @class */ (function () {
    function VouchersRoutingModule() {
    }
    VouchersRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], VouchersRoutingModule);
    return VouchersRoutingModule;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/vouchers.component.html":
/*!***********************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/vouchers.component.html ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"main_body\">\r\n  <router-outlet></router-outlet>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/vouchers.component.scss":
/*!***********************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/vouchers.component.scss ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2Rhc2hib2FyZC9hY2NvdW50aW5nL3ZvdWNoZXJzL3ZvdWNoZXJzLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/vouchers.component.ts":
/*!*********************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/vouchers.component.ts ***!
  \*********************************************************************/
/*! exports provided: VouchersComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VouchersComponent", function() { return VouchersComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _shared_enum__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/global-shared.service */ "./src/app/shared/services/global-shared.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var VouchersComponent = /** @class */ (function () {
    function VouchersComponent(globalService) {
        this.globalService = globalService;
        this.setSelectedHeader = _shared_enum__WEBPACK_IMPORTED_MODULE_1__["UIModuleHeaders"].VouchersHeader;
        this.setProjectHeader = 'Vouchers';
        // Set Menu Header Name
        this.globalService.setMenuHeaderName(this.setProjectHeader);
        // Set Menu Header List
        this.globalService.setMenuList([]);
    }
    VouchersComponent.prototype.ngOnInit = function () {
    };
    VouchersComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-vouchers',
            template: __webpack_require__(/*! ./vouchers.component.html */ "./src/app/dashboard/accounting/vouchers/vouchers.component.html"),
            styles: [__webpack_require__(/*! ./vouchers.component.scss */ "./src/app/dashboard/accounting/vouchers/vouchers.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_2__["GlobalSharedService"]])
    ], VouchersComponent);
    return VouchersComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/accounting/vouchers/vouchers.module.ts":
/*!******************************************************************!*\
  !*** ./src/app/dashboard/accounting/vouchers/vouchers.module.ts ***!
  \******************************************************************/
/*! exports provided: VouchersModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "VouchersModule", function() { return VouchersModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _vouchers_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./vouchers.component */ "./src/app/dashboard/accounting/vouchers/vouchers.component.ts");
/* harmony import */ var _voucher_listing_voucher_listing_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./voucher-listing/voucher-listing.component */ "./src/app/dashboard/accounting/vouchers/voucher-listing/voucher-listing.component.ts");
/* harmony import */ var _voucher_details_voucher_details_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./voucher-details/voucher-details.component */ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-details.component.ts");
/* harmony import */ var _voucher_add_voucher_add_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./voucher-add/voucher-add.component */ "./src/app/dashboard/accounting/vouchers/voucher-add/voucher-add.component.ts");
/* harmony import */ var _voucher_details_voucher_summary_voucher_summary_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./voucher-details/voucher-summary/voucher-summary.component */ "./src/app/dashboard/accounting/vouchers/voucher-details/voucher-summary/voucher-summary.component.ts");
/* harmony import */ var _vouchers_routing_module__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./vouchers-routing.module */ "./src/app/dashboard/accounting/vouchers/vouchers-routing.module.ts");
/* harmony import */ var _voucher_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./voucher.service */ "./src/app/dashboard/accounting/vouchers/voucher.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_input__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/material/input */ "./node_modules/@angular/material/esm5/input.es5.js");
/* harmony import */ var _angular_material_button__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/material/button */ "./node_modules/@angular/material/esm5/button.es5.js");
/* harmony import */ var _angular_material_card__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/material/card */ "./node_modules/@angular/material/esm5/card.es5.js");
/* harmony import */ var _angular_material_paginator__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/material/paginator */ "./node_modules/@angular/material/esm5/paginator.es5.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @angular/material/datepicker */ "./node_modules/@angular/material/esm5/datepicker.es5.js");
/* harmony import */ var _angular_material_core__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! @angular/material/core */ "./node_modules/@angular/material/esm5/core.es5.js");
/* harmony import */ var _angular_material_icon__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! @angular/material/icon */ "./node_modules/@angular/material/esm5/icon.es5.js");
/* harmony import */ var _angular_material_select__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! @angular/material/select */ "./node_modules/@angular/material/esm5/select.es5.js");
/* harmony import */ var _angular_material_progress_bar__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! @angular/material/progress-bar */ "./node_modules/@angular/material/esm5/progress-bar.es5.js");
/* harmony import */ var _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! @angular/material/progress-spinner */ "./node_modules/@angular/material/esm5/progress-spinner.es5.js");
/* harmony import */ var _angular_material_menu__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! @angular/material/menu */ "./node_modules/@angular/material/esm5/menu.es5.js");
/* harmony import */ var ngx_mat_select_search__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ngx-mat-select-search */ "./node_modules/ngx-mat-select-search/fesm5/ngx-mat-select-search.js");
/* harmony import */ var primeng_primeng__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! primeng/primeng */ "./node_modules/primeng/primeng.js");
/* harmony import */ var primeng_primeng__WEBPACK_IMPORTED_MODULE_23___default = /*#__PURE__*/__webpack_require__.n(primeng_primeng__WEBPACK_IMPORTED_MODULE_23__);
/* harmony import */ var projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_24__ = __webpack_require__(/*! projects/library/src/public_api */ "./projects/library/src/public_api.ts");
/* harmony import */ var _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_25__ = __webpack_require__(/*! @angular/material/checkbox */ "./node_modules/@angular/material/esm5/checkbox.es5.js");
/* harmony import */ var projects_library_src_lib_components_document_listing_document_listing_component__WEBPACK_IMPORTED_MODULE_26__ = __webpack_require__(/*! projects/library/src/lib/components/document-listing/document-listing.component */ "./projects/library/src/lib/components/document-listing/document-listing.component.ts");
/* harmony import */ var angular_split__WEBPACK_IMPORTED_MODULE_27__ = __webpack_require__(/*! angular-split */ "./node_modules/angular-split/fesm5/angular-split.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




























var VouchersModule = /** @class */ (function () {
    function VouchersModule() {
    }
    VouchersModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _vouchers_component__WEBPACK_IMPORTED_MODULE_2__["VouchersComponent"],
                _voucher_listing_voucher_listing_component__WEBPACK_IMPORTED_MODULE_3__["VoucherListingComponent"],
                _voucher_details_voucher_details_component__WEBPACK_IMPORTED_MODULE_4__["VoucherDetailsComponent"],
                _voucher_add_voucher_add_component__WEBPACK_IMPORTED_MODULE_5__["VoucherAddComponent"],
                _voucher_details_voucher_summary_voucher_summary_component__WEBPACK_IMPORTED_MODULE_6__["VoucherSummaryComponent"],
            ],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_9__["FormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_9__["ReactiveFormsModule"],
                _vouchers_routing_module__WEBPACK_IMPORTED_MODULE_7__["VouchersRoutingModule"],
                // Custom Modules
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_24__["LibraryModule"],
                // material
                _angular_material_input__WEBPACK_IMPORTED_MODULE_10__["MatInputModule"],
                _angular_material_button__WEBPACK_IMPORTED_MODULE_11__["MatButtonModule"],
                _angular_material_card__WEBPACK_IMPORTED_MODULE_12__["MatCardModule"],
                _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_25__["MatCheckboxModule"],
                _angular_material_paginator__WEBPACK_IMPORTED_MODULE_13__["MatPaginatorModule"],
                _angular_material_dialog__WEBPACK_IMPORTED_MODULE_14__["MatDialogModule"],
                _angular_material_datepicker__WEBPACK_IMPORTED_MODULE_15__["MatDatepickerModule"],
                _angular_material_core__WEBPACK_IMPORTED_MODULE_16__["MatNativeDateModule"],
                _angular_material_icon__WEBPACK_IMPORTED_MODULE_17__["MatIconModule"],
                _angular_material_select__WEBPACK_IMPORTED_MODULE_18__["MatSelectModule"],
                ngx_mat_select_search__WEBPACK_IMPORTED_MODULE_22__["NgxMatSelectSearchModule"],
                _angular_material_progress_bar__WEBPACK_IMPORTED_MODULE_19__["MatProgressBarModule"],
                _angular_material_menu__WEBPACK_IMPORTED_MODULE_21__["MatMenuModule"],
                _angular_material_progress_spinner__WEBPACK_IMPORTED_MODULE_20__["MatProgressSpinnerModule"],
                primeng_primeng__WEBPACK_IMPORTED_MODULE_23__["TooltipModule"],
                angular_split__WEBPACK_IMPORTED_MODULE_27__["AngularSplitModule"].forChild(),
            ],
            providers: [
                _voucher_service__WEBPACK_IMPORTED_MODULE_8__["VoucherService"]
            ],
            entryComponents: [
                _voucher_add_voucher_add_component__WEBPACK_IMPORTED_MODULE_5__["VoucherAddComponent"],
                projects_library_src_lib_components_document_listing_document_listing_component__WEBPACK_IMPORTED_MODULE_26__["DocumentListingComponent"]
            ]
        })
    ], VouchersModule);
    return VouchersModule;
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
//# sourceMappingURL=vouchers-vouchers-module.js.map