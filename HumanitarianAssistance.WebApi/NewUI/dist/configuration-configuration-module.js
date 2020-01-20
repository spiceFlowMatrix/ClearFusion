(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["configuration-configuration-module"],{

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

/***/ "./src/app/hr/configuration/components/add-designation/add-designation.component.html":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/add-designation/add-designation.component.html ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>{{title}}</h1>\r\n  <hr>\r\n</div>\r\n<form [formGroup]='addDesignationForm' (ngSubmit)=\"saveDesignation()\">\r\n  <mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-12\">\r\n        <h4>What is the name of this designation</h4>\r\n      </div>\r\n      <div class=\"col-sm-3\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"DesignationName\" placeholder=\"Designation\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-9\"></div>\r\n      <div class=\"col-sm-12\">\r\n        <h4>Please describe this designation</h4>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <textarea matInput formControlName=\"Description\" placeholder=\"Designation Description\"></textarea>\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-6\"></div>\r\n    </div>\r\n    <mat-divider></mat-divider>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-12\">\r\n        <div class=\"col-sm-4\">\r\n          <h4>Technical Questions</h4>\r\n        </div>\r\n        <div class=\"col-sm-4\">\r\n          <hum-button [type]=\"'add'\" [text]=\"'ADD NEW TECHNICAL QUESTION'\" (click)=\"addQuestion()\"></hum-button>\r\n        </div>\r\n        <div class=\"col-sm-12\" formArrayName=\"Questions\">\r\n          <div class=\"row\">\r\n            <div *ngFor=\"let item of addDesignationForm.controls['Questions'].controls; let i=index\">\r\n              <div class=\"col-sm-10\" [formGroupName]=\"i\">\r\n                <mat-form-field class=\"example-full-width\">\r\n                  <textarea matInput formControlName=\"Question\" placeholder=\"Technical Question {{i+1}}\"></textarea>\r\n                </mat-form-field>\r\n              </div>\r\n              <div class=\"col-sm-2\">\r\n                <span class=\"action-span\" *ngIf=\"addDesignationForm.get('Questions').length >1\">\r\n                  <i class=\"fas fa-trash\" (click)='deleteQuestion(i)'></i>\r\n                </span>\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </mat-dialog-content>\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/add-designation/add-designation.component.scss":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/add-designation/add-designation.component.scss ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9hZGQtZGVzaWduYXRpb24vYWRkLWRlc2lnbmF0aW9uLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/add-designation/add-designation.component.ts":
/*!******************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/add-designation/add-designation.component.ts ***!
  \******************************************************************************************/
/*! exports provided: AddDesignationComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddDesignationComponent", function() { return AddDesignationComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
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





var AddDesignationComponent = /** @class */ (function () {
    function AddDesignationComponent(fb, dialogRef, hrService, toastr, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add Designation';
    }
    AddDesignationComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.addDesignationForm = this.fb.group({
            'Id': [null],
            'DesignationName': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Description': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Questions': this.fb.array([], _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required),
        });
        if (this.data) {
            this.title = 'Edit Designation';
            this.addDesignationForm.get('Id').patchValue(this.data.Id);
            this.addDesignationForm.get('DesignationName').patchValue(this.data.Designation);
            this.addDesignationForm.get('Description').patchValue(this.data.Description);
            if (this.data.subItems === undefined && this.data.subItems.length < 0) {
                this.addQuestion();
            }
            else {
                this.data.subItems.forEach(function (x) {
                    _this.editQuestionPatchValue(x);
                });
            }
        }
        else {
            this.addQuestion();
        }
    };
    AddDesignationComponent.prototype.addQuestion = function () {
        this.addDesignationForm.get('Questions').push(this.fb.group({
            QuestionId: [0],
            Question: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]
        }));
    };
    AddDesignationComponent.prototype.deleteQuestion = function (index) {
        this.addDesignationForm.get('Questions').removeAt(index);
    };
    AddDesignationComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddDesignationComponent.prototype.editQuestionPatchValue = function (item) {
        this.addDesignationForm.get('Questions').push(this.fb.group({
            QuestionId: [item.QuestionId],
            Question: [item.Question, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]
        }));
    };
    AddDesignationComponent.prototype.addDesignation = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.addDesignation(this.addDesignationForm.value).subscribe(function (x) {
            if (x) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddDesignationComponent.prototype.editDesignation = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.editDesignation(this.addDesignationForm.value).subscribe(function (x) {
            if (x) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddDesignationComponent.prototype.saveDesignation = function () {
        if (this.addDesignationForm.valid) {
            if (this.addDesignationForm.value.Id == null) {
                this.addDesignation();
            }
            else {
                this.editDesignation();
            }
        }
    };
    AddDesignationComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-designation',
            template: __webpack_require__(/*! ./add-designation.component.html */ "./src/app/hr/configuration/components/add-designation/add-designation.component.html"),
            styles: [__webpack_require__(/*! ./add-designation.component.scss */ "./src/app/hr/configuration/components/add-designation/add-designation.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"], Object])
    ], AddDesignationComponent);
    return AddDesignationComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.html":
/*!******************************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.html ***!
  \******************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>{{title}}</h1>\r\n  <hr>\r\n</div>\r\n<form [formGroup]='addAttendanceGroupForm' (ngSubmit)=\"saveAttendanceGroup()\">\r\n  <mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"Name\" placeholder=\"Name\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"Description\" placeholder=\"Description\">\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </mat-dialog-content>\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\" [disabled]=\"!addAttendanceGroupForm.valid || !addAttendanceGroupForm.dirty\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.scss":
/*!******************************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.scss ***!
  \******************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9hdHRlbmRhbmNlLWdyb3VwLW1hc3Rlci9hZGQtYXR0ZW5kYW5jZS1ncm91cC9hZGQtYXR0ZW5kYW5jZS1ncm91cC5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.ts":
/*!****************************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.ts ***!
  \****************************************************************************************************************************/
/*! exports provided: AddAttendanceGroupComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddAttendanceGroupComponent", function() { return AddAttendanceGroupComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
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





var AddAttendanceGroupComponent = /** @class */ (function () {
    function AddAttendanceGroupComponent(fb, dialogRef, hrService, toastr, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add Attendance Group';
    }
    AddAttendanceGroupComponent.prototype.ngOnInit = function () {
        this.addAttendanceGroupForm = this.fb.group({
            'Id': [null],
            'Name': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Description': [null],
        });
        if (this.data) {
            this.title = 'Edit Attendance Group';
            this.addAttendanceGroupForm.get('Id').patchValue(this.data.AttendanceGroupId);
            this.addAttendanceGroupForm.get('Name').patchValue(this.data.Name);
            this.addAttendanceGroupForm.get('Description').patchValue(this.data.Description);
        }
    };
    AddAttendanceGroupComponent.prototype.addAttendanceGroup = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.addAttendanceGroup(this.addAttendanceGroupForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddAttendanceGroupComponent.prototype.editAttendanceGroup = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.editAttendanceGroup(this.addAttendanceGroupForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning(x.Message);
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddAttendanceGroupComponent.prototype.saveAttendanceGroup = function () {
        if (this.addAttendanceGroupForm.valid) {
            if (this.addAttendanceGroupForm.value.Id == null) {
                this.addAttendanceGroup();
            }
            else {
                this.editAttendanceGroup();
            }
        }
    };
    AddAttendanceGroupComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddAttendanceGroupComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-attendance-group',
            template: __webpack_require__(/*! ./add-attendance-group.component.html */ "./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.html"),
            styles: [__webpack_require__(/*! ./add-attendance-group.component.scss */ "./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_4__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"], Object])
    ], AddAttendanceGroupComponent);
    return AddAttendanceGroupComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/attendance-group-master/attendance-group-master.component.html":
/*!************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/attendance-group-master/attendance-group-master.component.html ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container width_100\">\r\n  <div class=\"row\">\r\n    <div class=\"col-md-12\" class=\"items-float-right\">\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD ATTENDANCE GROUP'\" (click)=\"addAttendanceGroup()\"></hum-button>\r\n    </div>\r\n    <div class=\"col-md-12\">\r\n      <div class=\"departmentTable\">\r\n      <hum-table [headers]=\"attendanceGroupHeaders$\" [items]=\"attendanceGroupList$\"\r\n         (actionClick)=\"actionEvents($event)\" [actions]=\"actions\"></hum-table>\r\n      </div>\r\n      <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n        [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n      </mat-paginator>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/attendance-group-master/attendance-group-master.component.scss":
/*!************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/attendance-group-master/attendance-group-master.component.scss ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9hdHRlbmRhbmNlLWdyb3VwLW1hc3Rlci9hdHRlbmRhbmNlLWdyb3VwLW1hc3Rlci5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/configuration/components/attendance-group-master/attendance-group-master.component.ts":
/*!**********************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/attendance-group-master/attendance-group-master.component.ts ***!
  \**********************************************************************************************************/
/*! exports provided: AttendanceGroupMasterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AttendanceGroupMasterComponent", function() { return AttendanceGroupMasterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _add_attendance_group_add_attendance_group_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./add-attendance-group/add-attendance-group.component */ "./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var AttendanceGroupMasterComponent = /** @class */ (function () {
    function AttendanceGroupMasterComponent(hrService, dialog, toastr, commonLoader) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.attendanceGroupHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Attendance Group Name', 'Description']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0
        };
    }
    AttendanceGroupMasterComponent.prototype.ngOnInit = function () {
        this.actions = {
            items: {
                button: { status: false, text: '' },
                download: false,
                edit: true,
                delete: true
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false
            }
        };
        this.getAttendanceGroupList();
    };
    AttendanceGroupMasterComponent.prototype.getAttendanceGroupList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getAttendanceGroupList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            _this.attendanceGroupList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.Result.map(function (element) {
                return {
                    AttendanceGroupId: element.AttendanceGroupId,
                    Name: element.Name,
                    Description: element.Description
                };
            }));
            _this.RecordCount = x.RecordCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    AttendanceGroupMasterComponent.prototype.addAttendanceGroup = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_attendance_group_add_attendance_group_component__WEBPACK_IMPORTED_MODULE_6__["AddAttendanceGroupComponent"], {
            width: '450px'
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getAttendanceGroupList();
        });
    };
    AttendanceGroupMasterComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.hrService.openDeleteDialog().subscribe(function (res) {
                if (res === true) {
                    _this.Id = event.item.AttendanceGroupId;
                    _this.hrService.deleteAttendenceDetail(_this.Id).subscribe(function (response) {
                        if (response === true) {
                            _this.getAttendanceGroupList();
                        }
                    });
                }
            });
        }
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_attendance_group_add_attendance_group_component__WEBPACK_IMPORTED_MODULE_6__["AddAttendanceGroupComponent"], {
                width: '450px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getAttendanceGroupList();
            });
        }
    };
    //#region "pageEvent"
    AttendanceGroupMasterComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getAttendanceGroupList();
    };
    AttendanceGroupMasterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-attendance-group-master',
            template: __webpack_require__(/*! ./attendance-group-master.component.html */ "./src/app/hr/configuration/components/attendance-group-master/attendance-group-master.component.html"),
            styles: [__webpack_require__(/*! ./attendance-group-master.component.scss */ "./src/app/hr/configuration/components/attendance-group-master/attendance-group-master.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_4__["HrService"],
            _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialog"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"]])
    ], AttendanceGroupMasterComponent);
    return AttendanceGroupMasterComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.html":
/*!**************************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.html ***!
  \**************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>{{title}}</h1>\r\n  <hr>\r\n</div>\r\n<form [formGroup]='addDepartmentForm' (ngSubmit)=\"saveDepartment()\">\r\n  <mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"DepartmentName\" placeholder=\"Department Name\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n        <lib-hum-dropdown [validation]=\"addDepartmentForm.controls['OfficeId'].hasError('required')\"\r\n            [options]=\"offices$\" formControlName=\"OfficeId\" [placeHolder]=\"'Office'\">\r\n          </lib-hum-dropdown>\r\n      </div>\r\n    </div>\r\n  </mat-dialog-content>\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\" [disabled]=\"!addDepartmentForm.valid || !addDepartmentForm.dirty\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.scss":
/*!**************************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.scss ***!
  \**************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9kZXBhcnRtZW50LW1hc3Rlci9hZGQtZGVwYXJ0bWVudC1tYXN0ZXIvYWRkLWRlcGFydG1lbnQtbWFzdGVyLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.ts":
/*!************************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.ts ***!
  \************************************************************************************************************************/
/*! exports provided: AddDepartmentMasterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddDepartmentMasterComponent", function() { return AddDepartmentMasterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var src_app_store_services_purchase_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/store/services/purchase.service */ "./src/app/store/services/purchase.service.ts");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_6__);
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







var AddDepartmentMasterComponent = /** @class */ (function () {
    function AddDepartmentMasterComponent(fb, dialogRef, hrService, toastr, purchaseService, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.purchaseService = purchaseService;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add Department';
    }
    AddDepartmentMasterComponent.prototype.ngOnInit = function () {
        this.addDepartmentForm = this.fb.group({
            'DepartmentId': [null],
            'DepartmentName': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'OfficeId': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
        });
        if (this.data) {
            this.title = 'Edit Department';
            this.addDepartmentForm.get('DepartmentId').patchValue(this.data.DepartmentId);
            this.addDepartmentForm.get('DepartmentName').patchValue(this.data.DepartmentName);
            this.addDepartmentForm.get('OfficeId').patchValue(this.data.OfficeId);
        }
        this.getAllOffice();
    };
    AddDepartmentMasterComponent.prototype.addDepartment = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.addDepartment(this.addDepartmentForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddDepartmentMasterComponent.prototype.editDepartment = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.editDepartment(this.addDepartmentForm.value).subscribe(function (x) {
            if (x) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddDepartmentMasterComponent.prototype.saveDepartment = function () {
        if (this.addDepartmentForm.valid) {
            if (this.addDepartmentForm.value.DepartmentId == null) {
                this.addDepartment();
            }
            else {
                this.editDepartment();
            }
        }
    };
    AddDepartmentMasterComponent.prototype.getAllOffice = function () {
        var _this = this;
        this.purchaseService.getAllOfficeList().subscribe(function (x) {
            _this.offices$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_6__["of"])(x.data.OfficeDetailsList.map(function (y) {
                return {
                    value: y.OfficeId,
                    name: y.OfficeCode + '-' + y.OfficeName
                };
            }));
        });
    };
    AddDepartmentMasterComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddDepartmentMasterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-department-master',
            template: __webpack_require__(/*! ./add-department-master.component.html */ "./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.html"),
            styles: [__webpack_require__(/*! ./add-department-master.component.scss */ "./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.scss")]
        }),
        __param(5, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_4__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"], src_app_store_services_purchase_service__WEBPACK_IMPORTED_MODULE_5__["PurchaseService"], Object])
    ], AddDepartmentMasterComponent);
    return AddDepartmentMasterComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/department-master/department-master.component.html":
/*!************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/department-master/department-master.component.html ***!
  \************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container width_100\">\r\n  <div class=\"row\">\r\n    <div class=\"col-md-12\" class=\"items-float-right\">\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD DEPARTMENT'\" (click)=\"addDepartment()\"></hum-button>\r\n    </div>\r\n    <div class=\"col-md-12\">\r\n      <div class=\"departmentTable\">\r\n      <hum-table [headers]=\"departmentHeaders$\" [items]=\"departmentList$\" [hideColums$]=\"hideColums$\"\r\n         (actionClick)=\"actionEvents($event)\" [actions]=\"actions\"></hum-table>\r\n      </div>\r\n      <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n        [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n      </mat-paginator>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/department-master/department-master.component.scss":
/*!************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/department-master/department-master.component.scss ***!
  \************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "div.departmentTable {\n  overflow: auto; }\n\ndiv.departmentTable {\n  white-space: nowrap; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvaHIvY29uZmlndXJhdGlvbi9jb21wb25lbnRzL2RlcGFydG1lbnQtbWFzdGVyL2Q6XFxEYXkgVXNlclxcQXZpbmFzaFxcT2ZmaWNpYWxcXEh1bWFuaXRhcmlhblxcR2l0TGFiUmVwb1xcY2xlYXItZnVzaW9uXFxIdW1hbml0YXJpYW5Bc3Npc3RhbmNlLldlYkFwaVxcTmV3VUkvc3JjXFxhcHBcXGhyXFxjb25maWd1cmF0aW9uXFxjb21wb25lbnRzXFxkZXBhcnRtZW50LW1hc3RlclxcZGVwYXJ0bWVudC1tYXN0ZXIuY29tcG9uZW50LnNjc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDRSxjQUFjLEVBQUE7O0FBR2hCO0VBQ0UsbUJBQW1CLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9oci9jb25maWd1cmF0aW9uL2NvbXBvbmVudHMvZGVwYXJ0bWVudC1tYXN0ZXIvZGVwYXJ0bWVudC1tYXN0ZXIuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyJkaXYuZGVwYXJ0bWVudFRhYmxlIHtcclxuICBvdmVyZmxvdzogYXV0bztcclxufVxyXG5cclxuZGl2LmRlcGFydG1lbnRUYWJsZSB7XHJcbiAgd2hpdGUtc3BhY2U6IG5vd3JhcDtcclxufVxyXG4iXX0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/department-master/department-master.component.ts":
/*!**********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/department-master/department-master.component.ts ***!
  \**********************************************************************************************/
/*! exports provided: DepartmentMasterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DepartmentMasterComponent", function() { return DepartmentMasterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _add_department_master_add_department_master_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./add-department-master/add-department-master.component */ "./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var DepartmentMasterComponent = /** @class */ (function () {
    function DepartmentMasterComponent(hrService, dialog, toastr, commonLoader) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.departmentHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Department Name', 'Office Name']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0
        };
    }
    DepartmentMasterComponent.prototype.ngOnInit = function () {
        this.actions = {
            items: {
                button: { status: false, text: '' },
                download: false,
                edit: true,
                delete: true,
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false,
            }
        };
        this.getDepartmentList();
    };
    DepartmentMasterComponent.prototype.getDepartmentList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getDepartmentList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            _this.departmentList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.Result.map(function (element) {
                return {
                    DepartmentId: element.DepartmentId,
                    DepartmentName: element.DepartmentName,
                    OfficeName: element.OfficeName,
                    OfficeId: element.OfficeId
                };
            }));
            _this.hideColums$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])({ headers: ['Id', 'Department Name', 'Office Name'], items: ['DepartmentId', 'DepartmentName', 'OfficeName'] });
            _this.RecordCount = x.RecordCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    DepartmentMasterComponent.prototype.addDepartment = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_department_master_add_department_master_component__WEBPACK_IMPORTED_MODULE_6__["AddDepartmentMasterComponent"], {
            width: '450px',
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getDepartmentList();
        });
    };
    DepartmentMasterComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.hrService.openDeleteDialog().subscribe(function (res) {
                if (res === true) {
                    _this.Id = event.item.DepartmentId;
                    _this.hrService.deleteDepartmentDetail(_this.Id).subscribe(function (response) {
                        if (response === true) {
                            _this.getDepartmentList();
                        }
                    });
                }
            });
        }
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_department_master_add_department_master_component__WEBPACK_IMPORTED_MODULE_6__["AddDepartmentMasterComponent"], {
                width: '450px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getDepartmentList();
            });
        }
    };
    //#region "pageEvent"
    DepartmentMasterComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getDepartmentList();
    };
    DepartmentMasterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-department-master',
            template: __webpack_require__(/*! ./department-master.component.html */ "./src/app/hr/configuration/components/department-master/department-master.component.html"),
            styles: [__webpack_require__(/*! ./department-master.component.scss */ "./src/app/hr/configuration/components/department-master/department-master.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__["HrService"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MatDialog"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"]])
    ], DepartmentMasterComponent);
    return DepartmentMasterComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/designation-listing/designation-listing.component.html":
/*!****************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/designation-listing/designation-listing.component.html ***!
  \****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">Designation\r\n    <hum-button [type]=\"'add'\" [text]=\"'ADD NEW DESIGNATION'\" (click)=\"addDesignation()\"></hum-button>\r\n  </span>\r\n</lib-sub-header-template>\r\n\r\n<mat-card humAddScroll>\r\n  <div class=\"container\">\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n        <div class=\"designationTable\">\r\n        <hum-table [headers]=\"designationListHeaders$\" [items]=\"designationList$\" [subHeaders]=\"subListHeaders$\"\r\n          [subTitle]=\"'Technical Questions'\" (actionClick)=\"actionEvents($event)\"\r\n          [actions]=\"actions\"></hum-table>\r\n        </div>\r\n        <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n          [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n        </mat-paginator>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/designation-listing/designation-listing.component.scss":
/*!****************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/designation-listing/designation-listing.component.scss ***!
  \****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "div.designationTable {\n  overflow: auto; }\n\ndiv.designationTable {\n  white-space: nowrap; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvaHIvY29uZmlndXJhdGlvbi9jb21wb25lbnRzL2Rlc2lnbmF0aW9uLWxpc3RpbmcvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcaHJcXGNvbmZpZ3VyYXRpb25cXGNvbXBvbmVudHNcXGRlc2lnbmF0aW9uLWxpc3RpbmdcXGRlc2lnbmF0aW9uLWxpc3RpbmcuY29tcG9uZW50LnNjc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDRSxjQUFjLEVBQUE7O0FBR2hCO0VBQ0UsbUJBQW1CLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9oci9jb25maWd1cmF0aW9uL2NvbXBvbmVudHMvZGVzaWduYXRpb24tbGlzdGluZy9kZXNpZ25hdGlvbi1saXN0aW5nLmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsiZGl2LmRlc2lnbmF0aW9uVGFibGUge1xyXG4gIG92ZXJmbG93OiBhdXRvO1xyXG59XHJcblxyXG5kaXYuZGVzaWduYXRpb25UYWJsZSB7XHJcbiAgd2hpdGUtc3BhY2U6IG5vd3JhcDtcclxufVxyXG4iXX0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/designation-listing/designation-listing.component.ts":
/*!**************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/designation-listing/designation-listing.component.ts ***!
  \**************************************************************************************************/
/*! exports provided: DesignationListingComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DesignationListingComponent", function() { return DesignationListingComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _add_designation_add_designation_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../add-designation/add-designation.component */ "./src/app/hr/configuration/components/add-designation/add-designation.component.ts");
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







var DesignationListingComponent = /** @class */ (function () {
    function DesignationListingComponent(hrService, dialog, toastr, commonLoader) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.designationListHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Name', 'Description']);
        this.subListHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Question']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0
        };
    }
    DesignationListingComponent.prototype.ngOnInit = function () {
        this.getDesignationList();
        this.actions = {
            items: {
                button: { status: false, text: '' },
                download: false,
                edit: true,
                delete: true,
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false,
            }
        };
    };
    DesignationListingComponent.prototype.getDesignationList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getDesignationList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            _this.designationList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.DesignationList.map(function (element) {
                return {
                    Id: element.DesignationId,
                    Designation: element.Designation,
                    Description: element.Description,
                    subItems: element.TechnicalQuestionList ? (element.TechnicalQuestionList.map(function (r) {
                        return {
                            QuestionId: r.QuestionId,
                            Question: r.Question
                        };
                    })) : null
                };
            }));
            _this.RecordCount = x.RecordCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    DesignationListingComponent.prototype.addDesignation = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_designation_add_designation_component__WEBPACK_IMPORTED_MODULE_5__["AddDesignationComponent"], {
            width: '650px',
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getDesignationList();
        });
    };
    DesignationListingComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.hrService.openDeleteDialog().subscribe(function (res) {
                if (res === true) {
                    _this.Id = event.item.Id;
                    _this.hrService.deleteDesignationDetail(_this.Id).subscribe(function (response) {
                        if (response === true) {
                            _this.getDesignationList();
                        }
                    });
                }
            });
        }
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_designation_add_designation_component__WEBPACK_IMPORTED_MODULE_5__["AddDesignationComponent"], {
                width: '650px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getDesignationList();
            });
        }
    };
    //#region "pageEvent"
    DesignationListingComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getDesignationList();
    };
    DesignationListingComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-designation-listing',
            template: __webpack_require__(/*! ./designation-listing.component.html */ "./src/app/hr/configuration/components/designation-listing/designation-listing.component.html"),
            styles: [__webpack_require__(/*! ./designation-listing.component.scss */ "./src/app/hr/configuration/components/designation-listing/designation-listing.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__["HrService"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MatDialog"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__["CommonLoaderService"]])
    ], DesignationListingComponent);
    return DesignationListingComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.html":
/*!***********************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.html ***!
  \***********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n    <h1 mat-dialog-title>{{title}}</h1>\r\n    <hr>\r\n  </div>\r\n  <form [formGroup]='addDegreeForm' (ngSubmit)=\"saveDegree()\">\r\n    <mat-dialog-content>\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-6\">\r\n          <mat-form-field class=\"example-full-width\">\r\n            <input matInput formControlName=\"Name\" placeholder=\"Education Degree\">\r\n          </mat-form-field>\r\n        </div>\r\n      </div>\r\n    </mat-dialog-content>\r\n    <mat-dialog-actions class=\"items-float-right\">\r\n      <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"></hum-button>\r\n      <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n      <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n    </mat-dialog-actions>\r\n  </form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.scss":
/*!***********************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.scss ***!
  \***********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9lZHVjYXRpb24tZGVncmVlL2FkZC1lZHVjYXRpb24tZGVncmVlL2FkZC1lZHVjYXRpb24tZGVncmVlLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.ts":
/*!*********************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.ts ***!
  \*********************************************************************************************************************/
/*! exports provided: AddEducationDegreeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddEducationDegreeComponent", function() { return AddEducationDegreeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
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





var AddEducationDegreeComponent = /** @class */ (function () {
    function AddEducationDegreeComponent(fb, dialogRef, hrService, toastr, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add Education Degree';
    }
    AddEducationDegreeComponent.prototype.ngOnInit = function () {
        this.addDegreeForm = this.fb.group({
            'Id': [null],
            'Name': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]]
        });
        if (this.data) {
            this.title = 'Edit Education Degree';
            this.addDegreeForm.get('Id').patchValue(this.data.Id);
            this.addDegreeForm.get('Name').patchValue(this.data.Name);
        }
    };
    AddEducationDegreeComponent.prototype.addDegree = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.addDegree(this.addDegreeForm.value).subscribe(function (x) {
            if (x) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddEducationDegreeComponent.prototype.editDegree = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.editDegree(this.addDegreeForm.value).subscribe(function (x) {
            if (x) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddEducationDegreeComponent.prototype.saveDegree = function () {
        if (this.addDegreeForm.valid) {
            if (this.addDegreeForm.value.Id == null) {
                this.addDegree();
            }
            else {
                this.editDegree();
            }
        }
    };
    AddEducationDegreeComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddEducationDegreeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-education-degree',
            template: __webpack_require__(/*! ./add-education-degree.component.html */ "./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.html"),
            styles: [__webpack_require__(/*! ./add-education-degree.component.scss */ "./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"], Object])
    ], AddEducationDegreeComponent);
    return AddEducationDegreeComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/education-degree/education-degree.component.html":
/*!**********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/education-degree/education-degree.component.html ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<!-- <mat-card humAddScroll> -->\r\n    <div class=\"container width_100\">\r\n      <div class=\"row\">\r\n        <div class=\"col-md-12\" class=\"items-float-right\">\r\n            <hum-button [type]=\"'add'\" [text]=\"'ADD EDUCATION DEGREE'\" (click)=\"addDegree()\"></hum-button>\r\n        </div>\r\n        <div class=\"col-md-12\">\r\n          <div class=\"designationTable\">\r\n          <hum-table [headers]=\"educationDegreeListHeaders$\" [items]=\"educationDegreeList$\"\r\n             (actionClick)=\"actionEvents($event)\" [actions]=\"actions\"></hum-table>\r\n          </div>\r\n          <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n            [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n          </mat-paginator>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  <!-- </mat-card> -->\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/education-degree/education-degree.component.scss":
/*!**********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/education-degree/education-degree.component.scss ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9lZHVjYXRpb24tZGVncmVlL2VkdWNhdGlvbi1kZWdyZWUuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/hr/configuration/components/education-degree/education-degree.component.ts":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/education-degree/education-degree.component.ts ***!
  \********************************************************************************************/
/*! exports provided: EducationDegreeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EducationDegreeComponent", function() { return EducationDegreeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _add_education_degree_add_education_degree_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./add-education-degree/add-education-degree.component */ "./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var EducationDegreeComponent = /** @class */ (function () {
    function EducationDegreeComponent(hrService, dialog, toastr, commonLoader) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.educationDegreeListHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Name']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0
        };
        this.actions = {
            items: {
                button: { status: false, text: '' },
                download: false,
                edit: true,
                delete: true
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false
            }
        };
    }
    EducationDegreeComponent.prototype.ngOnInit = function () {
        this.getEducationDegreeList();
    };
    EducationDegreeComponent.prototype.getEducationDegreeList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getEducationDegreeList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            _this.educationDegreeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.EducationDegreeList.map(function (element) {
                return {
                    Id: element.EducationDegreeId,
                    Name: element.EducationDegreeName
                };
            }));
            _this.RecordCount = x.TotalCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    EducationDegreeComponent.prototype.addDegree = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_education_degree_add_education_degree_component__WEBPACK_IMPORTED_MODULE_6__["AddEducationDegreeComponent"], {
            width: '450px'
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getEducationDegreeList();
        });
    };
    EducationDegreeComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.hrService.openDeleteDialog().subscribe(function (res) {
                if (res === true) {
                    _this.Id = event.item.Id;
                    _this.hrService.deleteEducationDegree(_this.Id).subscribe(function (response) {
                        if (response === true) {
                            _this.getEducationDegreeList();
                        }
                    });
                }
            });
        }
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_education_degree_add_education_degree_component__WEBPACK_IMPORTED_MODULE_6__["AddEducationDegreeComponent"], {
                width: '450px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getEducationDegreeList();
            });
        }
    };
    //#region "pageEvent"
    EducationDegreeComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getEducationDegreeList();
    };
    EducationDegreeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-education-degree',
            template: __webpack_require__(/*! ./education-degree.component.html */ "./src/app/hr/configuration/components/education-degree/education-degree.component.html"),
            styles: [__webpack_require__(/*! ./education-degree.component.scss */ "./src/app/hr/configuration/components/education-degree/education-degree.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__["HrService"],
            _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MatDialog"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"]])
    ], EducationDegreeComponent);
    return EducationDegreeComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/entry-component/entry-component.component.html":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/entry-component/entry-component.component.html ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-sidenav-container class=\"example-container\">\r\n  <mat-sidenav #sidenav mode=\"side\" opened=\"true\">\r\n    <app-dbsidebar></app-dbsidebar>\r\n  </mat-sidenav>\r\n\r\n  <mat-sidenav-content>\r\n    <mat-card class=\"header header_mat_card\">\r\n   <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n          <div class=\"col-sm-12 col-xs-12\">\r\n            <app-dbheader></app-dbheader>\r\n          </div>\r\n        </div>\r\n   </div>\r\n    </mat-card>\r\n\r\n    <router-outlet></router-outlet>\r\n  </mat-sidenav-content>\r\n</mat-sidenav-container>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/entry-component/entry-component.component.scss":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/entry-component/entry-component.component.scss ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9lbnRyeS1jb21wb25lbnQvZW50cnktY29tcG9uZW50LmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/entry-component/entry-component.component.ts":
/*!******************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/entry-component/entry-component.component.ts ***!
  \******************************************************************************************/
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
        this.globalservice.setMenuHeaderName('HR');
    };
    EntryComponentComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-entry-component',
            template: __webpack_require__(/*! ./entry-component.component.html */ "./src/app/hr/configuration/components/entry-component/entry-component.component.html"),
            styles: [__webpack_require__(/*! ./entry-component.component.scss */ "./src/app/hr/configuration/components/entry-component/entry-component.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_1__["GlobalSharedService"]])
    ], EntryComponentComponent);
    return EntryComponentComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.html":
/*!***********************************************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.html ***!
  \***********************************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>{{title}}</h1>\r\n  <hr>\r\n</div>\r\n<form [formGroup]='addExitInterviewQuestion' (ngSubmit)=\"saveQuestion()\">\r\n  <mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-12\">\r\n        <h5>Please write the question text</h5>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <textarea matInput formControlName=\"QuestionText\" placeholder=\"Question Text\"></textarea>\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <h5>Which part of the form will this question be in?</h5>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n        <lib-hum-dropdown [validation]=\"addExitInterviewQuestion.controls['QuestionType'].hasError('required')\"\r\n          [options]=\"questionTypes$\" formControlName=\"QuestionType\" (change)=\"getQuestionTypeSelectedValue($event)\"\r\n          [placeHolder]=\"'Question Type'\">\r\n        </lib-hum-dropdown>\r\n      </div>\r\n      <div class=\"col-sm-6\"></div>\r\n      <div class=\"col-sm-12\">\r\n        <h5>What position will this question appear in the sequence?</h5>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <span style=\"color: gray;\">By default the last position will be selected here</span>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"SequencePosition\" placeholder=\"Sequence Position\">\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </mat-dialog-content>\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"\r\n      [disabled]=\"!addExitInterviewQuestion.valid || !addExitInterviewQuestion.dirty\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.scss":
/*!***********************************************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.scss ***!
  \***********************************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9leGl0LWludGVydmlldy1xdWVzdGlvbnMvYWRkLWV4aXQtaW50ZXJ2aWV3LXF1ZXN0aW9ucy9hZGQtZXhpdC1pbnRlcnZpZXctcXVlc3Rpb25zLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.ts":
/*!*********************************************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.ts ***!
  \*********************************************************************************************************************************************/
/*! exports provided: AddExitInterviewQuestionsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddExitInterviewQuestionsComponent", function() { return AddExitInterviewQuestionsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__);
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
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};







var AddExitInterviewQuestionsComponent = /** @class */ (function () {
    function AddExitInterviewQuestionsComponent(fb, dialogRef, hrService, toastr, commonLoader, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add New Exit Interview Question';
        this.questionTypes$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_5__["of"])([
            { value: 1, name: 'Feeling About Employee Aspects' },
            { value: 2, name: 'Reason Of Leaving' },
            { value: 3, name: 'The Department' },
            { value: 4, name: 'The Job Itself' },
            { value: 5, name: 'My Supervisor' },
            { value: 6, name: 'The Management' },
        ]);
    }
    AddExitInterviewQuestionsComponent.prototype.ngOnInit = function () {
        this.addExitInterviewQuestion = this.fb.group({
            'Id': [null],
            'QuestionText': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'QuestionType': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'SequencePosition': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
        });
        if (this.data) {
            this.title = 'Edit Exit Interview Question';
            this.addExitInterviewQuestion.get('Id').patchValue(this.data.Id);
            this.addExitInterviewQuestion.get('QuestionText').patchValue(this.data.QuestionText);
            this.addExitInterviewQuestion.get('QuestionType').patchValue(this.data.QuestionType);
            this.addExitInterviewQuestion.get('SequencePosition').patchValue(this.data.SequencePosition);
        }
    };
    AddExitInterviewQuestionsComponent.prototype.upsertExitInterviewQuestion = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.UpsertExitInterviewQuestion(this.addExitInterviewQuestion.value).subscribe(function (x) {
            if (x) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddExitInterviewQuestionsComponent.prototype.saveQuestion = function () {
        if (this.addExitInterviewQuestion.valid) {
            this.upsertExitInterviewQuestion();
        }
    };
    AddExitInterviewQuestionsComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddExitInterviewQuestionsComponent.prototype.getQuestionTypeSelectedValue = function (event) {
        this.getSequenceNumber(event);
    };
    AddExitInterviewQuestionsComponent.prototype.getSequenceNumber = function (questionType) {
        var _this = this;
        this.commonLoader.showLoader();
        var model = {
            QuestionType: questionType,
            Id: this.data ? this.data.Id : 0
        };
        this.hrService.getSequenceNumber(model).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            if (x) {
                _this.addExitInterviewQuestion.get('SequencePosition').patchValue(x);
            }
            else {
                _this.toastr.warning('Something went wrong');
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.commonLoader.hideLoader();
        });
    };
    AddExitInterviewQuestionsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-exit-interview-questions',
            template: __webpack_require__(/*! ./add-exit-interview-questions.component.html */ "./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.html"),
            styles: [__webpack_require__(/*! ./add-exit-interview-questions.component.scss */ "./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.scss")]
        }),
        __param(5, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"], src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_6__["CommonLoaderService"], Object])
    ], AddExitInterviewQuestionsComponent);
    return AddExitInterviewQuestionsComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.html":
/*!**************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.html ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">\r\n    <hum-button [type]=\"'add'\" [text]=\"'ADD NEW QUESTION'\" (click)=\"addQuestion()\"></hum-button>\r\n  </span>\r\n</lib-sub-header-template>\r\n\r\n<mat-card humAddScroll>\r\n  <div class=\"container\">\r\n    <div class=\"row\">\r\n      <div class=\"col-md-12\">\r\n        <div>\r\n        <hum-table [headers]=\"exitInterviewQuestionsHeaders$\" [items]=\"exitInterviewQuestionsList$\"\r\n          (actionClick)=\"actionEvents($event)\" [hideColums$]=\"hideColums$\" [actions]=\"actions\"></hum-table>\r\n        </div>\r\n        <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n          [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n        </mat-paginator>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.scss":
/*!**************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.scss ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9leGl0LWludGVydmlldy1xdWVzdGlvbnMvZXhpdC1pbnRlcnZpZXctcXVlc3Rpb25zLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.ts":
/*!************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.ts ***!
  \************************************************************************************************************/
/*! exports provided: ExitInterviewQuestionsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ExitInterviewQuestionsComponent", function() { return ExitInterviewQuestionsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _add_exit_interview_questions_add_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./add-exit-interview-questions/add-exit-interview-questions.component */ "./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.ts");
/* harmony import */ var src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/shared/enum */ "./src/app/shared/enum.ts");
/* harmony import */ var src_app_store_services_config_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/store/services/config.service */ "./src/app/store/services/config.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};









var ExitInterviewQuestionsComponent = /** @class */ (function () {
    function ExitInterviewQuestionsComponent(hrService, dialog, toastr, commonLoader, configservice) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.configservice = configservice;
        this.exitInterviewQuestionsHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Question Text', 'Question Type', 'Sequence Position']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0,
            IsPaginated: true
        };
        this.hideColums$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])({ headers: ['Id', 'Question Text', 'Question Type', 'Sequence Position'],
            items: ['Id', 'QuestionText', 'QuestionTypeText', 'SequencePosition'] });
    }
    ExitInterviewQuestionsComponent.prototype.ngOnInit = function () {
        this.getExitInterviewQuestionsList();
        this.actions = {
            items: {
                button: { status: false, text: '' },
                delete: true,
                download: false,
                edit: true
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false,
            }
        };
    };
    ExitInterviewQuestionsComponent.prototype.getExitInterviewQuestionsList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getExitInterviewQuestionsList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            if (x.Result.length > 0) {
                _this.exitInterviewQuestionsList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.Result.map(function (element) {
                    return {
                        Id: element.Id,
                        QuestionText: element.QuestionText,
                        QuestionType: element.QuestionType,
                        QuestionTypeText: src_app_shared_enum__WEBPACK_IMPORTED_MODULE_7__["QuestionTypeName"].get(element.QuestionType),
                        SequencePosition: element.SequencePosition,
                    };
                }));
            }
            _this.RecordCount = x.RecordCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    ExitInterviewQuestionsComponent.prototype.addQuestion = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_exit_interview_questions_add_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_6__["AddExitInterviewQuestionsComponent"], {
            width: '650px',
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getExitInterviewQuestionsList();
        });
    };
    ExitInterviewQuestionsComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_exit_interview_questions_add_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_6__["AddExitInterviewQuestionsComponent"], {
                width: '650px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getExitInterviewQuestionsList();
            });
        }
        else if (event.type === 'delete') {
            this.configservice.openDeleteDialog().subscribe(function (res) {
                if (res) {
                    _this.commonLoader.showLoader();
                    _this.hrService.deleteExitinterviewQuestion(event.item.Id).subscribe(function (x) {
                        _this.commonLoader.hideLoader();
                        if (x) {
                            _this.toastr.success('Deleted');
                            _this.getExitInterviewQuestionsList();
                        }
                        _this.RecordCount = x.RecordCount;
                    }, function (error) {
                        _this.commonLoader.hideLoader();
                    });
                }
            });
        }
    };
    //#region "pageEvent"
    ExitInterviewQuestionsComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getExitInterviewQuestionsList();
    };
    ExitInterviewQuestionsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-exit-interview-questions',
            template: __webpack_require__(/*! ./exit-interview-questions.component.html */ "./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.html"),
            styles: [__webpack_require__(/*! ./exit-interview-questions.component.scss */ "./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__["HrService"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MatDialog"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"], src_app_store_services_config_service__WEBPACK_IMPORTED_MODULE_8__["ConfigService"]])
    ], ExitInterviewQuestionsComponent);
    return ExitInterviewQuestionsComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/general/general.component.html":
/*!****************************************************************************!*\
  !*** ./src/app/hr/configuration/components/general/general.component.html ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<lib-sub-header-template>\r\n  <span class=\"action_header\">General\r\n  </span>\r\n</lib-sub-header-template>\r\n\r\n<mat-card humAddScroll [height]=\"250\">\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <mat-accordion>\r\n        <mat-expansion-panel>\r\n          <mat-expansion-panel-header>\r\n            <mat-panel-title>\r\n              <h4>Education Degree</h4>\r\n            </mat-panel-title>\r\n          </mat-expansion-panel-header>\r\n          <ng-template matExpansionPanelContent>\r\n            <app-education-degree></app-education-degree>\r\n          </ng-template>\r\n        </mat-expansion-panel>\r\n      </mat-accordion>\r\n    </div>\r\n  </div>\r\n  <br>\r\n\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <mat-accordion>\r\n        <mat-expansion-panel>\r\n          <mat-expansion-panel-header>\r\n            <mat-panel-title>\r\n              <h4>Office</h4>\r\n            </mat-panel-title>\r\n          </mat-expansion-panel-header>\r\n          <ng-template matExpansionPanelContent>\r\n            <app-office-master></app-office-master>\r\n          </ng-template>\r\n        </mat-expansion-panel>\r\n      </mat-accordion>\r\n    </div>\r\n  </div>\r\n  <br>\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <mat-accordion>\r\n        <mat-expansion-panel>\r\n          <mat-expansion-panel-header>\r\n            <mat-panel-title>\r\n              <h4>Department</h4>\r\n            </mat-panel-title>\r\n          </mat-expansion-panel-header>\r\n          <ng-template matExpansionPanelContent>\r\n            <app-department-master></app-department-master>\r\n          </ng-template>\r\n        </mat-expansion-panel>\r\n      </mat-accordion>\r\n    </div>\r\n  </div>\r\n  <br>\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <mat-accordion>\r\n        <mat-expansion-panel>\r\n          <mat-expansion-panel-header>\r\n            <mat-panel-title>\r\n              <h4>Job Grades</h4>\r\n            </mat-panel-title>\r\n          </mat-expansion-panel-header>\r\n          <ng-template matExpansionPanelContent>\r\n            <app-job-grade-master></app-job-grade-master>\r\n          </ng-template>\r\n        </mat-expansion-panel>\r\n      </mat-accordion>\r\n    </div>\r\n  </div>\r\n  <br>\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <mat-accordion>\r\n        <mat-expansion-panel>\r\n          <mat-expansion-panel-header>\r\n            <mat-panel-title>\r\n              <h4>Attendance Group</h4>\r\n            </mat-panel-title>\r\n          </mat-expansion-panel-header>\r\n          <ng-template matExpansionPanelContent>\r\n            <app-attendance-group-master></app-attendance-group-master>\r\n          </ng-template>\r\n        </mat-expansion-panel>\r\n      </mat-accordion>\r\n    </div>\r\n  </div>\r\n  <br>\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <mat-accordion>\r\n        <mat-expansion-panel>\r\n          <mat-expansion-panel-header>\r\n            <mat-panel-title>\r\n              <h4>Profession</h4>\r\n            </mat-panel-title>\r\n          </mat-expansion-panel-header>\r\n          <ng-template matExpansionPanelContent>\r\n            <app-profession-master></app-profession-master>\r\n          </ng-template>\r\n        </mat-expansion-panel>\r\n      </mat-accordion>\r\n    </div>\r\n  </div>\r\n  <br>\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <mat-accordion>\r\n        <mat-expansion-panel>\r\n          <mat-expansion-panel-header>\r\n            <mat-panel-title>\r\n              <h4>Qualification</h4>\r\n            </mat-panel-title>\r\n          </mat-expansion-panel-header>\r\n          <ng-template matExpansionPanelContent>\r\n            <app-qualification-master></app-qualification-master>\r\n          </ng-template>\r\n        </mat-expansion-panel>\r\n      </mat-accordion>\r\n    </div>\r\n  </div>\r\n  <br>\r\n  <!-- <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <mat-accordion>\r\n        <mat-expansion-panel>\r\n          <mat-expansion-panel-header>\r\n            <mat-panel-title>\r\n              <h4>Leave Type</h4>\r\n            </mat-panel-title>\r\n          </mat-expansion-panel-header>\r\n          <ng-template matExpansionPanelContent>\r\n            <app-leave-type></app-leave-type>\r\n          </ng-template>\r\n        </mat-expansion-panel>\r\n      </mat-accordion>\r\n    </div>\r\n  </div> -->\r\n\r\n</mat-card>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/general/general.component.scss":
/*!****************************************************************************!*\
  !*** ./src/app/hr/configuration/components/general/general.component.scss ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9nZW5lcmFsL2dlbmVyYWwuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/hr/configuration/components/general/general.component.ts":
/*!**************************************************************************!*\
  !*** ./src/app/hr/configuration/components/general/general.component.ts ***!
  \**************************************************************************/
/*! exports provided: GeneralComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GeneralComponent", function() { return GeneralComponent; });
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

var GeneralComponent = /** @class */ (function () {
    function GeneralComponent() {
    }
    GeneralComponent.prototype.ngOnInit = function () {
    };
    GeneralComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-general',
            template: __webpack_require__(/*! ./general.component.html */ "./src/app/hr/configuration/components/general/general.component.html"),
            styles: [__webpack_require__(/*! ./general.component.scss */ "./src/app/hr/configuration/components/general/general.component.scss")]
        }),
        __metadata("design:paramtypes", [])
    ], GeneralComponent);
    return GeneralComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.html":
/*!*********************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.html ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>{{title}}</h1>\r\n  <hr>\r\n</div>\r\n<form [formGroup]='addGradeForm' (ngSubmit)=\"saveJobGrade()\">\r\n  <mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"GradeName\" placeholder=\"Grade Name\">\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </mat-dialog-content>\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\" [disabled]=\"!addGradeForm.valid || !addGradeForm.dirty\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.scss":
/*!*********************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.scss ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9qb2ItZ3JhZGUtbWFzdGVyL2FkZC1qb2ItZ3JhZGUvYWRkLWpvYi1ncmFkZS5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.ts":
/*!*******************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.ts ***!
  \*******************************************************************************************************/
/*! exports provided: AddJobGradeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddJobGradeComponent", function() { return AddJobGradeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
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





var AddJobGradeComponent = /** @class */ (function () {
    function AddJobGradeComponent(fb, dialogRef, hrService, toastr, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add Job Grade';
    }
    AddJobGradeComponent.prototype.ngOnInit = function () {
        this.addGradeForm = this.fb.group({
            'GradeId': [null],
            'GradeName': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
        });
        if (this.data) {
            this.title = 'Edit Job Grade';
            this.addGradeForm.get('GradeId').patchValue(this.data.GradeId);
            this.addGradeForm.get('GradeName').patchValue(this.data.GradeName);
        }
    };
    AddJobGradeComponent.prototype.addJobGrade = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.addJobGrade(this.addGradeForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddJobGradeComponent.prototype.editJobGrade = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.editJobGrade(this.addGradeForm.value).subscribe(function (x) {
            if (x) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddJobGradeComponent.prototype.saveJobGrade = function () {
        if (this.addGradeForm.valid) {
            if (this.addGradeForm.value.GradeId == null) {
                this.addJobGrade();
            }
            else {
                this.editJobGrade();
            }
        }
    };
    AddJobGradeComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddJobGradeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-job-grade',
            template: __webpack_require__(/*! ./add-job-grade.component.html */ "./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.html"),
            styles: [__webpack_require__(/*! ./add-job-grade.component.scss */ "./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_4__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"], Object])
    ], AddJobGradeComponent);
    return AddJobGradeComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/job-grade-master/job-grade-master.component.html":
/*!**********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/job-grade-master/job-grade-master.component.html ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container width_100\">\r\n  <div class=\"row\">\r\n    <div class=\"col-md-12\" class=\"items-float-right\">\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD JOB GRADE'\" (click)=\"addJobGrade()\"></hum-button>\r\n    </div>\r\n    <div class=\"col-md-12\">\r\n      <div class=\"departmentTable\">\r\n      <hum-table [headers]=\"jobGradeHeaders$\" [items]=\"jobGradeList$\"\r\n         (actionClick)=\"actionEvents($event)\" [actions]=\"actions\"></hum-table>\r\n      </div>\r\n      <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n        [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n      </mat-paginator>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/job-grade-master/job-grade-master.component.scss":
/*!**********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/job-grade-master/job-grade-master.component.scss ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9qb2ItZ3JhZGUtbWFzdGVyL2pvYi1ncmFkZS1tYXN0ZXIuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/hr/configuration/components/job-grade-master/job-grade-master.component.ts":
/*!********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/job-grade-master/job-grade-master.component.ts ***!
  \********************************************************************************************/
/*! exports provided: JobGradeMasterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "JobGradeMasterComponent", function() { return JobGradeMasterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _add_job_grade_add_job_grade_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./add-job-grade/add-job-grade.component */ "./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.ts");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var JobGradeMasterComponent = /** @class */ (function () {
    function JobGradeMasterComponent(hrService, dialog, toastr, commonLoader) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.jobGradeHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_2__["of"])(['Id', 'Grade Name']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0
        };
    }
    JobGradeMasterComponent.prototype.ngOnInit = function () {
        this.actions = {
            items: {
                button: { status: false, text: '' },
                download: false,
                edit: true,
                delete: true,
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false,
            }
        };
        this.getJobGradeList();
    };
    JobGradeMasterComponent.prototype.getJobGradeList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getJobGradeList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            _this.jobGradeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_2__["of"])(x.Result.map(function (element) {
                return {
                    GradeId: element.GradeId,
                    GradeName: element.GradeName,
                };
            }));
            _this.RecordCount = x.RecordCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    JobGradeMasterComponent.prototype.addJobGrade = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_job_grade_add_job_grade_component__WEBPACK_IMPORTED_MODULE_1__["AddJobGradeComponent"], {
            width: '450px',
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getJobGradeList();
        });
    };
    JobGradeMasterComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.hrService.openDeleteDialog().subscribe(function (res) {
                if (res === true) {
                    _this.Id = event.item.GradeId;
                    _this.hrService.deleteJobGradeDetail(_this.Id).subscribe(function (response) {
                        if (response === true) {
                            _this.getJobGradeList();
                        }
                    });
                }
            });
        }
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_job_grade_add_job_grade_component__WEBPACK_IMPORTED_MODULE_1__["AddJobGradeComponent"], {
                width: '450px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getJobGradeList();
            });
        }
    };
    //#region "pageEvent"
    JobGradeMasterComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getJobGradeList();
    };
    JobGradeMasterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-job-grade-master',
            template: __webpack_require__(/*! ./job-grade-master.component.html */ "./src/app/hr/configuration/components/job-grade-master/job-grade-master.component.html"),
            styles: [__webpack_require__(/*! ./job-grade-master.component.scss */ "./src/app/hr/configuration/components/job-grade-master/job-grade-master.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__["HrService"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_5__["MatDialog"], ngx_toastr__WEBPACK_IMPORTED_MODULE_6__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_4__["CommonLoaderService"]])
    ], JobGradeMasterComponent);
    return JobGradeMasterComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.html":
/*!*****************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.html ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>{{title}}</h1>\r\n  <hr>\r\n</div>\r\n<form [formGroup]='addLeaveTypeForm' (ngSubmit)=\"saveLeaveType()\">\r\n  <mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-12\">\r\n        <h5>Please write the Leave Reason</h5>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n          <mat-form-field class=\"example-full-width\">\r\n              <input matInput formControlName=\"ReasonName\" placeholder=\"Leave Reason\">\r\n          </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <h5>Provide description for leave reason</h5>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <textarea matInput formControlName=\"Description\" placeholder=\"Description\"></textarea>\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-12\">\r\n          <h5>Provide leave hours</h5>\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n          <mat-form-field class=\"example-full-width\">\r\n            <input matInput type=\"number\" formControlName=\"Unit\" placeholder=\"Hours\">\r\n          </mat-form-field>\r\n        </div>\r\n    </div>\r\n  </mat-dialog-content>\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\"\r\n      [disabled]=\"!addLeaveTypeForm.valid || !addLeaveTypeForm.dirty\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.scss":
/*!*****************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.scss ***!
  \*****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9sZWF2ZS10eXBlL2FkZC1sZWF2ZS10eXBlL2FkZC1sZWF2ZS10eXBlLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.ts":
/*!***************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.ts ***!
  \***************************************************************************************************/
/*! exports provided: AddLeaveTypeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddLeaveTypeComponent", function() { return AddLeaveTypeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
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






var AddLeaveTypeComponent = /** @class */ (function () {
    function AddLeaveTypeComponent(fb, dialogRef, hrService, toastr, commonLoader, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add New Leave Type';
    }
    AddLeaveTypeComponent.prototype.ngOnInit = function () {
        this.addLeaveTypeForm = this.fb.group({
            'LeaveReasonId': [null],
            'ReasonName': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'Description': [null],
            'Unit': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
        });
        if (this.data) {
            this.title = 'Edit Leave Type';
            this.addLeaveTypeForm.get('LeaveReasonId').patchValue(this.data.LeaveReasonId);
            this.addLeaveTypeForm.get('ReasonName').patchValue(this.data.ReasonName);
            this.addLeaveTypeForm.get('Description').patchValue(this.data.Description);
            this.addLeaveTypeForm.get('Unit').patchValue(this.data.Unit);
        }
    };
    AddLeaveTypeComponent.prototype.updateLeaveType = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.updateLeaveType(this.addLeaveTypeForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddLeaveTypeComponent.prototype.addLeaveType = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.addLeaveType(this.addLeaveTypeForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddLeaveTypeComponent.prototype.saveLeaveType = function () {
        if (this.addLeaveTypeForm.valid) {
            if (this.addLeaveTypeForm.value.LeaveReasonId) {
                this.updateLeaveType();
            }
            else {
                this.addLeaveType();
            }
        }
    };
    AddLeaveTypeComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddLeaveTypeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-leave-type',
            template: __webpack_require__(/*! ./add-leave-type.component.html */ "./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.html"),
            styles: [__webpack_require__(/*! ./add-leave-type.component.scss */ "./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.scss")]
        }),
        __param(5, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_5__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"], src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__["CommonLoaderService"], Object])
    ], AddLeaveTypeComponent);
    return AddLeaveTypeComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/leave-type/leave-type.component.html":
/*!**********************************************************************************!*\
  !*** ./src/app/hr/configuration/components/leave-type/leave-type.component.html ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container width_100\" humAddScroll>\r\n  <div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n      LEAVE TYPES\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD LEAVE TYPE'\" (click)=\"addLeaveType()\"></hum-button>\r\n    </div>\r\n    <div class=\"col-md-12\">\r\n      <hum-table [headers]=\"leaveTypeHeaders$\" [items]=\"leaveTypeList$\"\r\n         (actionClick)=\"actionEvents($event)\" [actions]=\"actions\"></hum-table>\r\n      <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n        [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n      </mat-paginator>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/leave-type/leave-type.component.scss":
/*!**********************************************************************************!*\
  !*** ./src/app/hr/configuration/components/leave-type/leave-type.component.scss ***!
  \**********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9sZWF2ZS10eXBlL2xlYXZlLXR5cGUuY29tcG9uZW50LnNjc3MifQ== */"

/***/ }),

/***/ "./src/app/hr/configuration/components/leave-type/leave-type.component.ts":
/*!********************************************************************************!*\
  !*** ./src/app/hr/configuration/components/leave-type/leave-type.component.ts ***!
  \********************************************************************************/
/*! exports provided: LeaveTypeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LeaveTypeComponent", function() { return LeaveTypeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _add_leave_type_add_leave_type_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./add-leave-type/add-leave-type.component */ "./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.ts");
/* harmony import */ var src_app_store_services_config_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/store/services/config.service */ "./src/app/store/services/config.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var LeaveTypeComponent = /** @class */ (function () {
    function LeaveTypeComponent(hrService, dialog, toastr, commonLoader, configservice) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.configservice = configservice;
        this.leaveTypeHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Name', 'Description', 'Allowed No Of Hours']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0
        };
    }
    LeaveTypeComponent.prototype.ngOnInit = function () {
        this.actions = {
            items: {
                button: { status: false, text: '' },
                delete: true,
                download: false,
                edit: true
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false,
            }
        };
        this.getLeaveTypeList();
    };
    LeaveTypeComponent.prototype.getLeaveTypeList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getLeaveTypeList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            _this.leaveTypeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.Result.map(function (element) {
                return {
                    LeaveReasonId: element.LeaveReasonId,
                    ReasonName: element.ReasonName,
                    Description: element.Description,
                    Unit: element.Unit,
                };
            }));
            // this.hideColums$ = of({headers: ['Id', 'Department Name', 'Office Name'], items: ['DepartmentId', 'DepartmentName', 'OfficeName']});
            _this.RecordCount = x.RecordCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    LeaveTypeComponent.prototype.addLeaveType = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_leave_type_add_leave_type_component__WEBPACK_IMPORTED_MODULE_6__["AddLeaveTypeComponent"], {
            width: '450px',
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getLeaveTypeList();
        });
    };
    LeaveTypeComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_leave_type_add_leave_type_component__WEBPACK_IMPORTED_MODULE_6__["AddLeaveTypeComponent"], {
                width: '450px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getLeaveTypeList();
            });
        }
        else if (event.type === 'delete') {
            this.configservice.openDeleteDialog().subscribe(function (res) {
                if (res) {
                    _this.commonLoader.showLoader();
                    _this.hrService.deleteLeaveType(event.item.LeaveReasonId).subscribe(function (x) {
                        _this.commonLoader.hideLoader();
                        if (x) {
                            _this.toastr.success('Deleted');
                            _this.getLeaveTypeList();
                        }
                        _this.RecordCount = x.RecordCount;
                    }, function (error) {
                        _this.commonLoader.hideLoader();
                    });
                }
            });
        }
    };
    //#region "pageEvent"
    LeaveTypeComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getLeaveTypeList();
    };
    LeaveTypeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-leave-type',
            template: __webpack_require__(/*! ./leave-type.component.html */ "./src/app/hr/configuration/components/leave-type/leave-type.component.html"),
            styles: [__webpack_require__(/*! ./leave-type.component.scss */ "./src/app/hr/configuration/components/leave-type/leave-type.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__["HrService"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MatDialog"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"], src_app_store_services_config_service__WEBPACK_IMPORTED_MODULE_7__["ConfigService"]])
    ], LeaveTypeComponent);
    return LeaveTypeComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.html":
/*!**************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.html ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>{{title}}</h1>\r\n  <hr>\r\n</div>\r\n<form [formGroup]='addOfficeForm' (ngSubmit)=\"saveOffice()\">\r\n  <mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"OfficeName\" placeholder=\"Office Name\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"OfficeCode\" placeholder=\"Office Code\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"SupervisorName\" placeholder=\"Supervisor Name\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"FaxNo\" placeholder=\"Fax No\">\r\n        </mat-form-field>\r\n      </div>\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"PhoneNo\" placeholder=\"Phone No\">\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </mat-dialog-content>\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\" [disabled]=\"!addOfficeForm.valid || !addOfficeForm.dirty\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.scss":
/*!**************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.scss ***!
  \**************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9vZmZpY2UtbWFzdGVyL2FkZC1vZmZpY2UtbWFzdGVyL2FkZC1vZmZpY2UtbWFzdGVyLmNvbXBvbmVudC5zY3NzIn0= */"

/***/ }),

/***/ "./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.ts":
/*!************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.ts ***!
  \************************************************************************************************************/
/*! exports provided: AddOfficeMasterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddOfficeMasterComponent", function() { return AddOfficeMasterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
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





var AddOfficeMasterComponent = /** @class */ (function () {
    function AddOfficeMasterComponent(fb, dialogRef, hrService, toastr, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add Office';
    }
    AddOfficeMasterComponent.prototype.ngOnInit = function () {
        this.addOfficeForm = this.fb.group({
            'OfficeId': [null],
            'OfficeName': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'OfficeCode': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'SupervisorName': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'FaxNo': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
            'PhoneNo': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].maxLength(10), _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].minLength(10), _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].pattern('[0-9]{0,10}')]],
        });
        if (this.data) {
            this.title = 'Edit Office';
            this.addOfficeForm.get('OfficeId').patchValue(this.data.OfficeId);
            this.addOfficeForm.get('OfficeName').patchValue(this.data.OfficeName);
            this.addOfficeForm.get('OfficeCode').patchValue(this.data.OfficeCode);
            this.addOfficeForm.get('SupervisorName').patchValue(this.data.SupervisorName);
            this.addOfficeForm.get('FaxNo').patchValue(this.data.FaxNo);
            this.addOfficeForm.get('PhoneNo').patchValue(this.data.PhoneNo);
        }
    };
    AddOfficeMasterComponent.prototype.addOffice = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.addOffice(this.addOfficeForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddOfficeMasterComponent.prototype.editOffice = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.editOffice(this.addOfficeForm.value).subscribe(function (x) {
            if (x) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning('Something went wrong');
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddOfficeMasterComponent.prototype.saveOffice = function () {
        if (this.addOfficeForm.valid) {
            if (this.addOfficeForm.value.OfficeId == null) {
                this.addOffice();
            }
            else {
                this.editOffice();
            }
        }
    };
    AddOfficeMasterComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddOfficeMasterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-office-master',
            template: __webpack_require__(/*! ./add-office-master.component.html */ "./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.html"),
            styles: [__webpack_require__(/*! ./add-office-master.component.scss */ "./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"], Object])
    ], AddOfficeMasterComponent);
    return AddOfficeMasterComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/office-master/office-master.component.html":
/*!****************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/office-master/office-master.component.html ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container width_100\">\r\n  <div class=\"row\">\r\n    <div class=\"col-md-12\" class=\"items-float-right\">\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD OFFICE'\" (click)=\"addOffice()\"></hum-button>\r\n    </div>\r\n    <div class=\"col-md-12\">\r\n      <div class=\"officeTable\">\r\n      <hum-table [headers]=\"officeHeaders$\" [items]=\"officeList$\"\r\n         (actionClick)=\"actionEvents($event)\" [actions]=\"actions\"></hum-table>\r\n      </div>\r\n      <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n        [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n      </mat-paginator>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/office-master/office-master.component.scss":
/*!****************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/office-master/office-master.component.scss ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "div.officeTable {\n  overflow: auto; }\n\ndiv.officeTable {\n  white-space: nowrap; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvaHIvY29uZmlndXJhdGlvbi9jb21wb25lbnRzL29mZmljZS1tYXN0ZXIvZDpcXERheSBVc2VyXFxBdmluYXNoXFxPZmZpY2lhbFxcSHVtYW5pdGFyaWFuXFxHaXRMYWJSZXBvXFxjbGVhci1mdXNpb25cXEh1bWFuaXRhcmlhbkFzc2lzdGFuY2UuV2ViQXBpXFxOZXdVSS9zcmNcXGFwcFxcaHJcXGNvbmZpZ3VyYXRpb25cXGNvbXBvbmVudHNcXG9mZmljZS1tYXN0ZXJcXG9mZmljZS1tYXN0ZXIuY29tcG9uZW50LnNjc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDRSxjQUFjLEVBQUE7O0FBR2hCO0VBQ0UsbUJBQW1CLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9oci9jb25maWd1cmF0aW9uL2NvbXBvbmVudHMvb2ZmaWNlLW1hc3Rlci9vZmZpY2UtbWFzdGVyLmNvbXBvbmVudC5zY3NzIiwic291cmNlc0NvbnRlbnQiOlsiZGl2Lm9mZmljZVRhYmxlIHtcclxuICBvdmVyZmxvdzogYXV0bztcclxufVxyXG5cclxuZGl2Lm9mZmljZVRhYmxlIHtcclxuICB3aGl0ZS1zcGFjZTogbm93cmFwO1xyXG59XHJcbiJdfQ== */"

/***/ }),

/***/ "./src/app/hr/configuration/components/office-master/office-master.component.ts":
/*!**************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/office-master/office-master.component.ts ***!
  \**************************************************************************************/
/*! exports provided: OfficeMasterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "OfficeMasterComponent", function() { return OfficeMasterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var _add_office_master_add_office_master_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./add-office-master/add-office-master.component */ "./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var OfficeMasterComponent = /** @class */ (function () {
    function OfficeMasterComponent(hrService, dialog, toastr, commonLoader) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.officeHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Office Code', 'Office Name', 'Supervisor Name', 'Phone No', 'Fax No']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0
        };
    }
    OfficeMasterComponent.prototype.ngOnInit = function () {
        this.actions = {
            items: {
                button: { status: false, text: '' },
                download: false,
                edit: true,
                delete: true,
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false,
            }
        };
        this.getOfficeList();
    };
    OfficeMasterComponent.prototype.getOfficeList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getOfficeList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            _this.officeList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.Result.map(function (element) {
                return {
                    OfficeId: element.OfficeId,
                    OfficeCode: element.OfficeCode,
                    OfficeName: element.OfficeName,
                    SupervisorName: element.SupervisorName,
                    PhoneNo: element.PhoneNo,
                    FaxNo: element.FaxNo
                };
            }));
            _this.RecordCount = x.RecordCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    OfficeMasterComponent.prototype.addOffice = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_office_master_add_office_master_component__WEBPACK_IMPORTED_MODULE_6__["AddOfficeMasterComponent"], {
            width: '450px',
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getOfficeList();
        });
    };
    OfficeMasterComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.hrService.openDeleteDialog().subscribe(function (res) {
                if (res) {
                    _this.Id = event.item.OfficeId;
                    _this.hrService.deleteOfficeDegree(_this.Id).subscribe(function (response) {
                        if (response.StatusCode === 200) {
                            _this.getOfficeList();
                        }
                    });
                }
            });
        }
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_office_master_add_office_master_component__WEBPACK_IMPORTED_MODULE_6__["AddOfficeMasterComponent"], {
                width: '450px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getOfficeList();
            });
        }
    };
    //#region "pageEvent"
    OfficeMasterComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getOfficeList();
    };
    OfficeMasterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-office-master',
            template: __webpack_require__(/*! ./office-master.component.html */ "./src/app/hr/configuration/components/office-master/office-master.component.html"),
            styles: [__webpack_require__(/*! ./office-master.component.scss */ "./src/app/hr/configuration/components/office-master/office-master.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__["HrService"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_4__["MatDialog"], ngx_toastr__WEBPACK_IMPORTED_MODULE_5__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_3__["CommonLoaderService"]])
    ], OfficeMasterComponent);
    return OfficeMasterComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.html":
/*!************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.html ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>{{title}}</h1>\r\n  <hr>\r\n</div>\r\n<form [formGroup]='addProfessionForm' (ngSubmit)=\"saveProfession()\">\r\n  <mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"ProfessionName\" placeholder=\"Profession Name\">\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </mat-dialog-content>\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\" [disabled]=\"!addProfessionForm.valid || !addProfessionForm.dirty\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.scss":
/*!************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.scss ***!
  \************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9wcm9mZXNzaW9uLW1hc3Rlci9hZGQtcHJvZmVzc2lvbi9hZGQtcHJvZmVzc2lvbi5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.ts":
/*!**********************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.ts ***!
  \**********************************************************************************************************/
/*! exports provided: AddProfessionComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddProfessionComponent", function() { return AddProfessionComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
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





var AddProfessionComponent = /** @class */ (function () {
    function AddProfessionComponent(fb, dialogRef, hrService, toastr, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add Profession';
    }
    AddProfessionComponent.prototype.ngOnInit = function () {
        this.addProfessionForm = this.fb.group({
            'ProfessionId': [null],
            'ProfessionName': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
        });
        if (this.data) {
            this.title = 'Edit Profession';
            this.addProfessionForm.get('ProfessionId').patchValue(this.data.ProfessionId);
            this.addProfessionForm.get('ProfessionName').patchValue(this.data.ProfessionName);
        }
    };
    AddProfessionComponent.prototype.addProfession = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.addProfession(this.addProfessionForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning(x.Message);
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddProfessionComponent.prototype.editProfession = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.editProfession(this.addProfessionForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning(x.Message);
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddProfessionComponent.prototype.saveProfession = function () {
        if (this.addProfessionForm.valid) {
            if (this.addProfessionForm.value.ProfessionId == null) {
                this.addProfession();
            }
            else {
                this.editProfession();
            }
        }
    };
    AddProfessionComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddProfessionComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-profession',
            template: __webpack_require__(/*! ./add-profession.component.html */ "./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.html"),
            styles: [__webpack_require__(/*! ./add-profession.component.scss */ "./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"], Object])
    ], AddProfessionComponent);
    return AddProfessionComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/profession-master/profession-master.component.html":
/*!************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/profession-master/profession-master.component.html ***!
  \************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container width_100\">\r\n  <div class=\"row\">\r\n    <div class=\"col-md-12\" class=\"items-float-right\">\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD PROFESSION'\" (click)=\"addProfession()\"></hum-button>\r\n    </div>\r\n    <div class=\"col-md-12\">\r\n      <div class=\"professionTable\">\r\n      <hum-table [headers]=\"professionHeaders$\" [items]=\"professionList$\"\r\n         (actionClick)=\"actionEvents($event)\" [actions]=\"actions\"></hum-table>\r\n      </div>\r\n      <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n        [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n      </mat-paginator>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/profession-master/profession-master.component.scss":
/*!************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/profession-master/profession-master.component.scss ***!
  \************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9wcm9mZXNzaW9uLW1hc3Rlci9wcm9mZXNzaW9uLW1hc3Rlci5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/configuration/components/profession-master/profession-master.component.ts":
/*!**********************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/profession-master/profession-master.component.ts ***!
  \**********************************************************************************************/
/*! exports provided: ProfessionMasterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProfessionMasterComponent", function() { return ProfessionMasterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _add_profession_add_profession_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./add-profession/add-profession.component */ "./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var ProfessionMasterComponent = /** @class */ (function () {
    function ProfessionMasterComponent(hrService, dialog, toastr, commonLoader) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.professionHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Profession Name']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0
        };
    }
    ProfessionMasterComponent.prototype.ngOnInit = function () {
        this.actions = {
            items: {
                button: { status: false, text: '' },
                download: false,
                edit: true,
                delete: true
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false
            }
        };
        this.getProfessionList();
    };
    ProfessionMasterComponent.prototype.getProfessionList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getProfessionList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            _this.professionList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.Result.map(function (element) {
                return {
                    ProfessionId: element.ProfessionId,
                    ProfessionName: element.ProfessionName
                };
            }));
            _this.RecordCount = x.RecordCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    ProfessionMasterComponent.prototype.addProfession = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_profession_add_profession_component__WEBPACK_IMPORTED_MODULE_6__["AddProfessionComponent"], {
            width: '450px'
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getProfessionList();
        });
    };
    ProfessionMasterComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.hrService.openDeleteDialog().subscribe(function (res) {
                if (res === true) {
                    _this.Id = event.item.ProfessionId;
                    _this.hrService.deleteProfessionDetail(_this.Id).subscribe(function (response) {
                        if (response === true) {
                            _this.getProfessionList();
                        }
                    });
                }
            });
        }
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_profession_add_profession_component__WEBPACK_IMPORTED_MODULE_6__["AddProfessionComponent"], {
                width: '450px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getProfessionList();
            });
        }
    };
    //#region "pageEvent"
    ProfessionMasterComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getProfessionList();
    };
    ProfessionMasterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-profession-master',
            template: __webpack_require__(/*! ./profession-master.component.html */ "./src/app/hr/configuration/components/profession-master/profession-master.component.html"),
            styles: [__webpack_require__(/*! ./profession-master.component.scss */ "./src/app/hr/configuration/components/profession-master/profession-master.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__["HrService"],
            _angular_material_dialog__WEBPACK_IMPORTED_MODULE_3__["MatDialog"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"]])
    ], ProfessionMasterComponent);
    return ProfessionMasterComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.html":
/*!*********************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.html ***!
  \*********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div>\r\n  <h1 mat-dialog-title>{{title}}</h1>\r\n  <hr>\r\n</div>\r\n<form [formGroup]='addQualificationForm' (ngSubmit)=\"saveQualification()\">\r\n  <mat-dialog-content>\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-6\">\r\n        <mat-form-field class=\"example-full-width\">\r\n          <input matInput formControlName=\"QualificationName\" placeholder=\"Qualification Name\">\r\n        </mat-form-field>\r\n      </div>\r\n    </div>\r\n  </mat-dialog-content>\r\n  <mat-dialog-actions class=\"items-float-right\">\r\n    <hum-button *ngIf=\"!isFormSubmitted\" [type]=\"'save'\" [text]=\"'save'\" [isSubmit]=\"true\" [disabled]=\"!addQualificationForm.valid || !addQualificationForm.dirty\"></hum-button>\r\n    <hum-button *ngIf=\"isFormSubmitted\" [type]=\"'loading'\" [text]=\"'Saving....'\"></hum-button>\r\n    <hum-button (click)='onCancelPopup()' [type]=\"'cancel'\" [text]=\"'cancel'\"></hum-button>\r\n  </mat-dialog-actions>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.scss":
/*!*********************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.scss ***!
  \*********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9xdWFsaWZpY2F0aW9uLW1hc3Rlci9hZGQtcXVhbGlmaWNhdGlvbi9hZGQtcXVhbGlmaWNhdGlvbi5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.ts":
/*!*******************************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.ts ***!
  \*******************************************************************************************************************/
/*! exports provided: AddQualificationComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddQualificationComponent", function() { return AddQualificationComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
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





var AddQualificationComponent = /** @class */ (function () {
    function AddQualificationComponent(fb, dialogRef, hrService, toastr, data) {
        this.fb = fb;
        this.dialogRef = dialogRef;
        this.hrService = hrService;
        this.toastr = toastr;
        this.data = data;
        this.isFormSubmitted = false;
        this.title = 'Add Qualification';
    }
    AddQualificationComponent.prototype.ngOnInit = function () {
        this.addQualificationForm = this.fb.group({
            'QualificationId': [null],
            'QualificationName': [null, [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required]],
        });
        if (this.data) {
            this.title = 'Edit Qualification';
            this.addQualificationForm.get('QualificationId').patchValue(this.data.QualificationId);
            this.addQualificationForm.get('QualificationName').patchValue(this.data.QualificationName);
        }
    };
    AddQualificationComponent.prototype.addQualification = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.addQualification(this.addQualificationForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning(x.Message);
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddQualificationComponent.prototype.editQualification = function () {
        var _this = this;
        this.isFormSubmitted = true;
        this.hrService.editQualification(this.addQualificationForm.value).subscribe(function (x) {
            if (x.StatusCode === 200) {
                _this.toastr.success('Success');
                _this.isFormSubmitted = false;
                _this.dialogRef.close();
            }
            else {
                _this.toastr.warning(x.Message);
                _this.isFormSubmitted = false;
            }
        }, function (error) {
            _this.toastr.warning(error);
            _this.isFormSubmitted = false;
        });
    };
    AddQualificationComponent.prototype.saveQualification = function () {
        if (this.addQualificationForm.valid) {
            if (this.addQualificationForm.value.QualificationId == null) {
                this.addQualification();
            }
            else {
                this.editQualification();
            }
        }
    };
    AddQualificationComponent.prototype.onCancelPopup = function () {
        this.dialogRef.close();
    };
    AddQualificationComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add-qualification',
            template: __webpack_require__(/*! ./add-qualification.component.html */ "./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.html"),
            styles: [__webpack_require__(/*! ./add-qualification.component.scss */ "./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.scss")]
        }),
        __param(4, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MAT_DIALOG_DATA"])),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"], _angular_material_dialog__WEBPACK_IMPORTED_MODULE_2__["MatDialogRef"],
            src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_3__["HrService"], ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"], Object])
    ], AddQualificationComponent);
    return AddQualificationComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/components/qualification-master/qualification-master.component.html":
/*!******************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/qualification-master/qualification-master.component.html ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container width_100\">\r\n  <div class=\"row\">\r\n    <div class=\"col-md-12\" class=\"items-float-right\">\r\n        <hum-button [type]=\"'add'\" [text]=\"'ADD QUALIFICATION'\" (click)=\"addQualification()\"></hum-button>\r\n    </div>\r\n    <div class=\"col-md-12\">\r\n      <div class=\"professionTable\">\r\n      <hum-table [headers]=\"qualificationHeaders$\" [items]=\"qualificationList$\"\r\n         (actionClick)=\"actionEvents($event)\" [actions]=\"actions\"></hum-table>\r\n      </div>\r\n      <mat-paginator [length]=\"RecordCount\" [pageSize]=\"pageModel.PageSize\"\r\n        [pageSizeOptions]=\"[10, 5, 25, 100]\" (page)=\"pageEvent($event)\">\r\n      </mat-paginator>\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/components/qualification-master/qualification-master.component.scss":
/*!******************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/qualification-master/qualification-master.component.scss ***!
  \******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2hyL2NvbmZpZ3VyYXRpb24vY29tcG9uZW50cy9xdWFsaWZpY2F0aW9uLW1hc3Rlci9xdWFsaWZpY2F0aW9uLW1hc3Rlci5jb21wb25lbnQuc2NzcyJ9 */"

/***/ }),

/***/ "./src/app/hr/configuration/components/qualification-master/qualification-master.component.ts":
/*!****************************************************************************************************!*\
  !*** ./src/app/hr/configuration/components/qualification-master/qualification-master.component.ts ***!
  \****************************************************************************************************/
/*! exports provided: QualificationMasterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "QualificationMasterComponent", function() { return QualificationMasterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs/internal/observable/of */ "./node_modules/rxjs/internal/observable/of.js");
/* harmony import */ var rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/hr/services/hr.service */ "./src/app/hr/services/hr.service.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/common-loader/common-loader.service */ "./src/app/shared/common-loader/common-loader.service.ts");
/* harmony import */ var _add_qualification_add_qualification_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./add-qualification/add-qualification.component */ "./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var QualificationMasterComponent = /** @class */ (function () {
    function QualificationMasterComponent(hrService, dialog, toastr, commonLoader) {
        this.hrService = hrService;
        this.dialog = dialog;
        this.toastr = toastr;
        this.commonLoader = commonLoader;
        this.qualificationHeaders$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(['Id', 'Qualification Name']);
        this.pageModel = {
            PageSize: 10,
            PageIndex: 0
        };
    }
    QualificationMasterComponent.prototype.ngOnInit = function () {
        this.actions = {
            items: {
                button: { status: false, text: '' },
                download: false,
                edit: true,
                delete: true,
            },
            subitems: {
                button: { status: false, text: '' },
                delete: false,
                download: false
            }
        };
        this.getQualificationList();
    };
    QualificationMasterComponent.prototype.getQualificationList = function () {
        var _this = this;
        this.commonLoader.showLoader();
        this.hrService.getQualificationList(this.pageModel).subscribe(function (x) {
            _this.commonLoader.hideLoader();
            _this.qualificationList$ = Object(rxjs_internal_observable_of__WEBPACK_IMPORTED_MODULE_1__["of"])(x.Result.map(function (element) {
                return {
                    QualificationId: element.QualificationId,
                    QualificationName: element.QualificationName
                };
            }));
            _this.RecordCount = x.RecordCount;
        }, function (error) {
            _this.commonLoader.hideLoader();
        });
    };
    QualificationMasterComponent.prototype.addQualification = function () {
        var _this = this;
        var dialogRef = this.dialog.open(_add_qualification_add_qualification_component__WEBPACK_IMPORTED_MODULE_6__["AddQualificationComponent"], {
            width: '450px'
        });
        dialogRef.afterClosed().subscribe(function (x) {
            _this.getQualificationList();
        });
    };
    QualificationMasterComponent.prototype.actionEvents = function (event) {
        var _this = this;
        if (event.type === 'delete') {
            this.hrService.openDeleteDialog().subscribe(function (res) {
                if (res === true) {
                    _this.Id = event.item.QualificationId;
                    _this.hrService.deleteQualificationDetail(_this.Id).subscribe(function (response) {
                        if (response.StatusCode === 200) {
                            _this.getQualificationList();
                        }
                    });
                }
            });
        }
        if (event.type === 'edit') {
            var dialogRef = this.dialog.open(_add_qualification_add_qualification_component__WEBPACK_IMPORTED_MODULE_6__["AddQualificationComponent"], {
                width: '450px',
                data: event.item
            });
            dialogRef.afterClosed().subscribe(function (x) {
                _this.getQualificationList();
            });
        }
    };
    //#region "pageEvent"
    QualificationMasterComponent.prototype.pageEvent = function (e) {
        this.pageModel.PageIndex = e.pageIndex;
        this.pageModel.PageSize = e.pageSize;
        this.getQualificationList();
    };
    QualificationMasterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-qualification-master',
            template: __webpack_require__(/*! ./qualification-master.component.html */ "./src/app/hr/configuration/components/qualification-master/qualification-master.component.html"),
            styles: [__webpack_require__(/*! ./qualification-master.component.scss */ "./src/app/hr/configuration/components/qualification-master/qualification-master.component.scss")]
        }),
        __metadata("design:paramtypes", [src_app_hr_services_hr_service__WEBPACK_IMPORTED_MODULE_2__["HrService"],
            _angular_material__WEBPACK_IMPORTED_MODULE_3__["MatDialog"],
            ngx_toastr__WEBPACK_IMPORTED_MODULE_4__["ToastrService"],
            src_app_shared_common_loader_common_loader_service__WEBPACK_IMPORTED_MODULE_5__["CommonLoaderService"]])
    ], QualificationMasterComponent);
    return QualificationMasterComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/configuration-routing.module.ts":
/*!******************************************************************!*\
  !*** ./src/app/hr/configuration/configuration-routing.module.ts ***!
  \******************************************************************/
/*! exports provided: ConfigurationRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ConfigurationRoutingModule", function() { return ConfigurationRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _configuration_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./configuration.component */ "./src/app/hr/configuration/configuration.component.ts");
/* harmony import */ var _components_designation_listing_designation_listing_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/designation-listing/designation-listing.component */ "./src/app/hr/configuration/components/designation-listing/designation-listing.component.ts");
/* harmony import */ var _components_general_general_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./components/general/general.component */ "./src/app/hr/configuration/components/general/general.component.ts");
/* harmony import */ var _components_exit_interview_questions_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./components/exit-interview-questions/exit-interview-questions.component */ "./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.ts");
/* harmony import */ var _components_leave_type_leave_type_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./components/leave-type/leave-type.component */ "./src/app/hr/configuration/components/leave-type/leave-type.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};







var routes = [
    {
        path: '', component: _configuration_component__WEBPACK_IMPORTED_MODULE_2__["ConfigurationComponent"],
        children: [
            {
                path: 'general', component: _components_general_general_component__WEBPACK_IMPORTED_MODULE_4__["GeneralComponent"]
            },
            {
                path: 'designation', component: _components_designation_listing_designation_listing_component__WEBPACK_IMPORTED_MODULE_3__["DesignationListingComponent"]
            },
            {
                path: 'exit-interview-questions', component: _components_exit_interview_questions_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_5__["ExitInterviewQuestionsComponent"]
            },
            {
                path: 'leave-policy', component: _components_leave_type_leave_type_component__WEBPACK_IMPORTED_MODULE_6__["LeaveTypeComponent"]
            },
            {
                path: '', redirectTo: 'general', pathMatch: 'full'
            }
        ]
    },
];
var ConfigurationRoutingModule = /** @class */ (function () {
    function ConfigurationRoutingModule() {
    }
    ConfigurationRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], ConfigurationRoutingModule);
    return ConfigurationRoutingModule;
}());



/***/ }),

/***/ "./src/app/hr/configuration/configuration.component.html":
/*!***************************************************************!*\
  !*** ./src/app/hr/configuration/configuration.component.html ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-sidenav-container class=\"example-container\">\r\n  <mat-sidenav #sidenav mode=\"side\" opened=\"true\">\r\n    <app-dbsidebar></app-dbsidebar>\r\n  </mat-sidenav>\r\n  <mat-sidenav-content>\r\n    <mat-card class=\"header header_mat_card\">\r\n   <div class=\"container-fluid\">\r\n      <div class=\"row\">\r\n          <div class=\"col-sm-12 col-xs-12\">\r\n            <app-dbheader></app-dbheader>\r\n          </div>\r\n        </div>\r\n   </div>\r\n    </mat-card>\r\n    <lib-sub-header-template>\r\n        <span class=\"action_header\">Configuration\r\n        </span>\r\n        <div class=\"action_section\">\r\n        </div>\r\n      </lib-sub-header-template>\r\n    <nav mat-tab-nav-bar>\r\n      <a mat-tab-link\r\n         *ngFor=\"let link of navLinks\"\r\n         [routerLink]=\"link.link\"\r\n         routerLinkActive #rla=\"routerLinkActive\"\r\n         [active]=\"rla.isActive\">\r\n        {{link.label}}\r\n      </a>\r\n    </nav>\r\n\r\n    <router-outlet></router-outlet>\r\n  </mat-sidenav-content>\r\n</mat-sidenav-container>\r\n"

/***/ }),

/***/ "./src/app/hr/configuration/configuration.component.scss":
/*!***************************************************************!*\
  !*** ./src/app/hr/configuration/configuration.component.scss ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".example-container {\n  position: absolute;\n  top: 0;\n  bottom: 0;\n  left: 0;\n  right: 0; }\n\n.header_mat_card {\n  height: 77px; }\n\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvaHIvY29uZmlndXJhdGlvbi9kOlxcRGF5IFVzZXJcXEF2aW5hc2hcXE9mZmljaWFsXFxIdW1hbml0YXJpYW5cXEdpdExhYlJlcG9cXGNsZWFyLWZ1c2lvblxcSHVtYW5pdGFyaWFuQXNzaXN0YW5jZS5XZWJBcGlcXE5ld1VJL3NyY1xcYXBwXFxoclxcY29uZmlndXJhdGlvblxcY29uZmlndXJhdGlvbi5jb21wb25lbnQuc2NzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtFQUNFLGtCQUFrQjtFQUNsQixNQUFNO0VBQ04sU0FBUztFQUNULE9BQU87RUFDUCxRQUFRLEVBQUE7O0FBRVY7RUFDRSxZQUFZLEVBQUEiLCJmaWxlIjoic3JjL2FwcC9oci9jb25maWd1cmF0aW9uL2NvbmZpZ3VyYXRpb24uY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyIuZXhhbXBsZS1jb250YWluZXIge1xyXG4gIHBvc2l0aW9uOiBhYnNvbHV0ZTtcclxuICB0b3A6IDA7XHJcbiAgYm90dG9tOiAwO1xyXG4gIGxlZnQ6IDA7XHJcbiAgcmlnaHQ6IDA7XHJcbn1cclxuLmhlYWRlcl9tYXRfY2FyZCB7XHJcbiAgaGVpZ2h0OiA3N3B4O1xyXG59XHJcbiJdfQ== */"

/***/ }),

/***/ "./src/app/hr/configuration/configuration.component.ts":
/*!*************************************************************!*\
  !*** ./src/app/hr/configuration/configuration.component.ts ***!
  \*************************************************************/
/*! exports provided: ConfigurationComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ConfigurationComponent", function() { return ConfigurationComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
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



var ConfigurationComponent = /** @class */ (function () {
    function ConfigurationComponent(router, globalService) {
        this.router = router;
        this.globalService = globalService;
        this.navLinks = [];
        this.activeLinkIndex = 0;
        this.setConfigurationHeader = 'HR';
        this.navLinks = [
            {
                label: 'GENERAL',
                link: './general',
                index: 0
            },
            {
                label: 'DESIGNATION',
                link: './designation',
                index: 1
            },
            {
                label: 'EXIT INTERVIEW',
                link: './exit-interview-questions',
                index: 2
            },
            {
                label: 'LEAVE POLICY',
                link: './leave-policy',
                index: 3
            },
        ];
    }
    ConfigurationComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.router.events.subscribe(function (res) {
            _this.activeLinkIndex = _this.navLinks.indexOf(_this.navLinks.find(function (tab) { return tab.link === '.' + _this.router.url; }));
        });
        this.globalService.setMenuHeaderName(this.setConfigurationHeader);
    };
    ConfigurationComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-configuration',
            template: __webpack_require__(/*! ./configuration.component.html */ "./src/app/hr/configuration/configuration.component.html"),
            styles: [__webpack_require__(/*! ./configuration.component.scss */ "./src/app/hr/configuration/configuration.component.scss")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"], src_app_shared_services_global_shared_service__WEBPACK_IMPORTED_MODULE_2__["GlobalSharedService"]])
    ], ConfigurationComponent);
    return ConfigurationComponent;
}());



/***/ }),

/***/ "./src/app/hr/configuration/configuration.module.ts":
/*!**********************************************************!*\
  !*** ./src/app/hr/configuration/configuration.module.ts ***!
  \**********************************************************/
/*! exports provided: ConfigurationModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ConfigurationModule", function() { return ConfigurationModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _configuration_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./configuration-routing.module */ "./src/app/hr/configuration/configuration-routing.module.ts");
/* harmony import */ var _components_add_designation_add_designation_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/add-designation/add-designation.component */ "./src/app/hr/configuration/components/add-designation/add-designation.component.ts");
/* harmony import */ var _components_designation_listing_designation_listing_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./components/designation-listing/designation-listing.component */ "./src/app/hr/configuration/components/designation-listing/designation-listing.component.ts");
/* harmony import */ var _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./components/entry-component/entry-component.component */ "./src/app/hr/configuration/components/entry-component/entry-component.component.ts");
/* harmony import */ var _configuration_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./configuration.component */ "./src/app/hr/configuration/configuration.component.ts");
/* harmony import */ var _angular_material_sidenav__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/material/sidenav */ "./node_modules/@angular/material/esm5/sidenav.es5.js");
/* harmony import */ var _angular_material_card__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material/card */ "./node_modules/@angular/material/esm5/card.es5.js");
/* harmony import */ var src_app_shared_share_layout_module__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! src/app/shared/share-layout.module */ "./src/app/shared/share-layout.module.ts");
/* harmony import */ var projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! projects/library/src/public_api */ "./projects/library/src/public_api.ts");
/* harmony import */ var _angular_material_tabs__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/material/tabs */ "./node_modules/@angular/material/esm5/tabs.es5.js");
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/material/dialog */ "./node_modules/@angular/material/esm5/dialog.es5.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_material_input__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/material/input */ "./node_modules/@angular/material/esm5/input.es5.js");
/* harmony import */ var _angular_cdk_text_field__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @angular/cdk/text-field */ "./node_modules/@angular/cdk/esm5/text-field.es5.js");
/* harmony import */ var _angular_material_divider__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! @angular/material/divider */ "./node_modules/@angular/material/esm5/divider.es5.js");
/* harmony import */ var _angular_material_paginator__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! @angular/material/paginator */ "./node_modules/@angular/material/esm5/paginator.es5.js");
/* harmony import */ var _components_general_general_component__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ./components/general/general.component */ "./src/app/hr/configuration/components/general/general.component.ts");
/* harmony import */ var _components_education_degree_education_degree_component__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ./components/education-degree/education-degree.component */ "./src/app/hr/configuration/components/education-degree/education-degree.component.ts");
/* harmony import */ var _angular_material_expansion__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! @angular/material/expansion */ "./node_modules/@angular/material/esm5/expansion.es5.js");
/* harmony import */ var _components_education_degree_add_education_degree_add_education_degree_component__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ./components/education-degree/add-education-degree/add-education-degree.component */ "./src/app/hr/configuration/components/education-degree/add-education-degree/add-education-degree.component.ts");
/* harmony import */ var _components_office_master_office_master_component__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ./components/office-master/office-master.component */ "./src/app/hr/configuration/components/office-master/office-master.component.ts");
/* harmony import */ var _components_office_master_add_office_master_add_office_master_component__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! ./components/office-master/add-office-master/add-office-master.component */ "./src/app/hr/configuration/components/office-master/add-office-master/add-office-master.component.ts");
/* harmony import */ var _components_department_master_department_master_component__WEBPACK_IMPORTED_MODULE_24__ = __webpack_require__(/*! ./components/department-master/department-master.component */ "./src/app/hr/configuration/components/department-master/department-master.component.ts");
/* harmony import */ var _components_department_master_add_department_master_add_department_master_component__WEBPACK_IMPORTED_MODULE_25__ = __webpack_require__(/*! ./components/department-master/add-department-master/add-department-master.component */ "./src/app/hr/configuration/components/department-master/add-department-master/add-department-master.component.ts");
/* harmony import */ var _components_job_grade_master_job_grade_master_component__WEBPACK_IMPORTED_MODULE_26__ = __webpack_require__(/*! ./components/job-grade-master/job-grade-master.component */ "./src/app/hr/configuration/components/job-grade-master/job-grade-master.component.ts");
/* harmony import */ var _components_job_grade_master_add_job_grade_add_job_grade_component__WEBPACK_IMPORTED_MODULE_27__ = __webpack_require__(/*! ./components/job-grade-master/add-job-grade/add-job-grade.component */ "./src/app/hr/configuration/components/job-grade-master/add-job-grade/add-job-grade.component.ts");
/* harmony import */ var _components_attendance_group_master_attendance_group_master_component__WEBPACK_IMPORTED_MODULE_28__ = __webpack_require__(/*! ./components/attendance-group-master/attendance-group-master.component */ "./src/app/hr/configuration/components/attendance-group-master/attendance-group-master.component.ts");
/* harmony import */ var _components_attendance_group_master_add_attendance_group_add_attendance_group_component__WEBPACK_IMPORTED_MODULE_29__ = __webpack_require__(/*! ./components/attendance-group-master/add-attendance-group/add-attendance-group.component */ "./src/app/hr/configuration/components/attendance-group-master/add-attendance-group/add-attendance-group.component.ts");
/* harmony import */ var _components_profession_master_profession_master_component__WEBPACK_IMPORTED_MODULE_30__ = __webpack_require__(/*! ./components/profession-master/profession-master.component */ "./src/app/hr/configuration/components/profession-master/profession-master.component.ts");
/* harmony import */ var _components_profession_master_add_profession_add_profession_component__WEBPACK_IMPORTED_MODULE_31__ = __webpack_require__(/*! ./components/profession-master/add-profession/add-profession.component */ "./src/app/hr/configuration/components/profession-master/add-profession/add-profession.component.ts");
/* harmony import */ var _components_qualification_master_qualification_master_component__WEBPACK_IMPORTED_MODULE_32__ = __webpack_require__(/*! ./components/qualification-master/qualification-master.component */ "./src/app/hr/configuration/components/qualification-master/qualification-master.component.ts");
/* harmony import */ var _components_qualification_master_add_qualification_add_qualification_component__WEBPACK_IMPORTED_MODULE_33__ = __webpack_require__(/*! ./components/qualification-master/add-qualification/add-qualification.component */ "./src/app/hr/configuration/components/qualification-master/add-qualification/add-qualification.component.ts");
/* harmony import */ var _components_exit_interview_questions_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_34__ = __webpack_require__(/*! ./components/exit-interview-questions/exit-interview-questions.component */ "./src/app/hr/configuration/components/exit-interview-questions/exit-interview-questions.component.ts");
/* harmony import */ var _components_exit_interview_questions_add_exit_interview_questions_add_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_35__ = __webpack_require__(/*! ./components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component */ "./src/app/hr/configuration/components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component.ts");
/* harmony import */ var _components_leave_type_leave_type_component__WEBPACK_IMPORTED_MODULE_36__ = __webpack_require__(/*! ./components/leave-type/leave-type.component */ "./src/app/hr/configuration/components/leave-type/leave-type.component.ts");
/* harmony import */ var _components_leave_type_add_leave_type_add_leave_type_component__WEBPACK_IMPORTED_MODULE_37__ = __webpack_require__(/*! ./components/leave-type/add-leave-type/add-leave-type.component */ "./src/app/hr/configuration/components/leave-type/add-leave-type/add-leave-type.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






































var ConfigurationModule = /** @class */ (function () {
    function ConfigurationModule() {
    }
    ConfigurationModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _components_add_designation_add_designation_component__WEBPACK_IMPORTED_MODULE_3__["AddDesignationComponent"],
                _components_designation_listing_designation_listing_component__WEBPACK_IMPORTED_MODULE_4__["DesignationListingComponent"],
                _components_entry_component_entry_component_component__WEBPACK_IMPORTED_MODULE_5__["EntryComponentComponent"],
                _configuration_component__WEBPACK_IMPORTED_MODULE_6__["ConfigurationComponent"],
                _components_general_general_component__WEBPACK_IMPORTED_MODULE_18__["GeneralComponent"],
                _components_education_degree_education_degree_component__WEBPACK_IMPORTED_MODULE_19__["EducationDegreeComponent"],
                _components_education_degree_add_education_degree_add_education_degree_component__WEBPACK_IMPORTED_MODULE_21__["AddEducationDegreeComponent"],
                _components_office_master_office_master_component__WEBPACK_IMPORTED_MODULE_22__["OfficeMasterComponent"],
                _components_office_master_add_office_master_add_office_master_component__WEBPACK_IMPORTED_MODULE_23__["AddOfficeMasterComponent"],
                _components_department_master_department_master_component__WEBPACK_IMPORTED_MODULE_24__["DepartmentMasterComponent"],
                _components_department_master_add_department_master_add_department_master_component__WEBPACK_IMPORTED_MODULE_25__["AddDepartmentMasterComponent"],
                _components_job_grade_master_job_grade_master_component__WEBPACK_IMPORTED_MODULE_26__["JobGradeMasterComponent"],
                _components_job_grade_master_add_job_grade_add_job_grade_component__WEBPACK_IMPORTED_MODULE_27__["AddJobGradeComponent"],
                _components_attendance_group_master_attendance_group_master_component__WEBPACK_IMPORTED_MODULE_28__["AttendanceGroupMasterComponent"],
                _components_attendance_group_master_add_attendance_group_add_attendance_group_component__WEBPACK_IMPORTED_MODULE_29__["AddAttendanceGroupComponent"],
                _components_profession_master_profession_master_component__WEBPACK_IMPORTED_MODULE_30__["ProfessionMasterComponent"],
                _components_profession_master_add_profession_add_profession_component__WEBPACK_IMPORTED_MODULE_31__["AddProfessionComponent"],
                _components_qualification_master_qualification_master_component__WEBPACK_IMPORTED_MODULE_32__["QualificationMasterComponent"],
                _components_qualification_master_add_qualification_add_qualification_component__WEBPACK_IMPORTED_MODULE_33__["AddQualificationComponent"],
                _components_exit_interview_questions_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_34__["ExitInterviewQuestionsComponent"],
                _components_exit_interview_questions_add_exit_interview_questions_add_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_35__["AddExitInterviewQuestionsComponent"],
                _components_leave_type_leave_type_component__WEBPACK_IMPORTED_MODULE_36__["LeaveTypeComponent"],
                _components_leave_type_add_leave_type_add_leave_type_component__WEBPACK_IMPORTED_MODULE_37__["AddLeaveTypeComponent"],
            ],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _configuration_routing_module__WEBPACK_IMPORTED_MODULE_2__["ConfigurationRoutingModule"],
                _angular_material_sidenav__WEBPACK_IMPORTED_MODULE_7__["MatSidenavModule"],
                _angular_material_card__WEBPACK_IMPORTED_MODULE_8__["MatCardModule"],
                _angular_material_tabs__WEBPACK_IMPORTED_MODULE_11__["MatTabsModule"],
                _angular_material_dialog__WEBPACK_IMPORTED_MODULE_12__["MatDialogModule"],
                _angular_material_expansion__WEBPACK_IMPORTED_MODULE_20__["MatExpansionModule"],
                _angular_material_paginator__WEBPACK_IMPORTED_MODULE_17__["MatPaginatorModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_13__["ReactiveFormsModule"],
                _angular_material_input__WEBPACK_IMPORTED_MODULE_14__["MatInputModule"],
                _angular_material_divider__WEBPACK_IMPORTED_MODULE_16__["MatDividerModule"],
                _angular_cdk_text_field__WEBPACK_IMPORTED_MODULE_15__["TextFieldModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_13__["FormsModule"],
                src_app_shared_share_layout_module__WEBPACK_IMPORTED_MODULE_9__["ShareLayoutModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__["SubHeaderTemplateModule"],
                projects_library_src_public_api__WEBPACK_IMPORTED_MODULE_10__["LibraryModule"]
            ],
            entryComponents: [_components_add_designation_add_designation_component__WEBPACK_IMPORTED_MODULE_3__["AddDesignationComponent"], _components_education_degree_add_education_degree_add_education_degree_component__WEBPACK_IMPORTED_MODULE_21__["AddEducationDegreeComponent"], _components_office_master_add_office_master_add_office_master_component__WEBPACK_IMPORTED_MODULE_23__["AddOfficeMasterComponent"],
                _components_department_master_add_department_master_add_department_master_component__WEBPACK_IMPORTED_MODULE_25__["AddDepartmentMasterComponent"], _components_job_grade_master_add_job_grade_add_job_grade_component__WEBPACK_IMPORTED_MODULE_27__["AddJobGradeComponent"], _components_attendance_group_master_add_attendance_group_add_attendance_group_component__WEBPACK_IMPORTED_MODULE_29__["AddAttendanceGroupComponent"],
                _components_profession_master_add_profession_add_profession_component__WEBPACK_IMPORTED_MODULE_31__["AddProfessionComponent"], _components_qualification_master_add_qualification_add_qualification_component__WEBPACK_IMPORTED_MODULE_33__["AddQualificationComponent"], _components_exit_interview_questions_add_exit_interview_questions_add_exit_interview_questions_component__WEBPACK_IMPORTED_MODULE_35__["AddExitInterviewQuestionsComponent"],
                _components_leave_type_add_leave_type_add_leave_type_component__WEBPACK_IMPORTED_MODULE_37__["AddLeaveTypeComponent"]]
        })
    ], ConfigurationModule);
    return ConfigurationModule;
}());



/***/ })

}]);
//# sourceMappingURL=configuration-configuration-module.js.map