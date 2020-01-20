(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["project-hiring-project-hiring-module"],{

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



/***/ })

}]);
//# sourceMappingURL=project-hiring-project-hiring-module.js.map