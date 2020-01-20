(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["default~configuration-configuration-module~project-hiring-project-hiring-module~project-management-p~b18fa7fb"],{

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



/***/ }),

/***/ "./src/app/store/services/purchase.service.ts":
/*!****************************************************!*\
  !*** ./src/app/store/services/purchase.service.ts ***!
  \****************************************************/
/*! exports provided: PurchaseService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PurchaseService", function() { return PurchaseService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../shared/services/global-services.service */ "./src/app/shared/services/global-services.service.ts");
/* harmony import */ var _shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../shared/services/app-url.service */ "./src/app/shared/services/app-url.service.ts");
/* harmony import */ var _shared_global__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../shared/global */ "./src/app/shared/global.ts");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/internal/operators/map */ "./node_modules/rxjs/internal/operators/map.js");
/* harmony import */ var rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! src/app/shared/static-utilities */ "./src/app/shared/static-utilities.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};









var PurchaseService = /** @class */ (function () {
    function PurchaseService(globalService, appurl, http) {
        this.globalService = globalService;
        this.appurl = appurl;
        this.http = http;
    }
    //#region "GetPurchaseFilterList"
    PurchaseService.prototype.getPurchaseFilterList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetAllPurchaseFilters)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    //#endregion
    //#region "GetInventoriesByInventoryTypeId"
    PurchaseService.prototype.getInventoriesByInventoryTypeId = function (Id) {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_GetAllInventories +
            '?AssetType=' +
            Id)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) {
            var responseData = {
                data: response.data.InventoryList,
                statusCode: response.StatusCode,
                message: response.Message
            };
            return responseData;
        }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["finalize"])(function () { }));
    };
    //#endregion
    //#region "GetItemGroupByInventoryId"
    PurchaseService.prototype.getItemGroupByInventoryId = function (Id) {
        return this.globalService
            .getItemById(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_GetAllStoreItemGroups, Id)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.storeItemGroupList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "getTransportItemCategoryType"
    PurchaseService.prototype.getTransportItemCategoryType = function (Id) {
        return this.globalService
            .getItemById(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetTransportItemCategoryType, Id);
    };
    //#endregion
    //#region "GetItemsByItemGroupId"
    PurchaseService.prototype.getItemsByItemGroupId = function (Id) {
        return this.globalService
            .getItemById(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_GetAllInventoryItems, Id)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            var responseData = {
                data: x.data.InventoryItemList,
                statusCode: x.StatusCode,
                message: x.Message
            };
            return responseData;
        }));
    };
    //#endregion
    //#region "GetPurchaseListFilter"
    PurchaseService.prototype.getFilteredPurchaseList = function (filter) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetFilteredPurchaseList, filter)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    //#endregion
    //#region "GetPurchaseFilterList"
    PurchaseService.prototype.getAllInventoryTypeList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetAllInventoriesType)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    //#endregion
    //#region "GetPurchaseFilterList"
    PurchaseService.prototype.getAllProjectList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Project_GetAllProjectList)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    //#endregion
    //#region "GetPurchaseFilterList"
    PurchaseService.prototype.getAllOfficeList = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllOffice)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    //#endregion
    PurchaseService.prototype.getPurchaseAssetType = function () {
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_7__["of"])([
            {
                AssetTypeId: 1,
                AssetTypeName: 'Cash'
            },
            {
                AssetTypeId: 2,
                AssetTypeName: 'In Kind'
            }
        ]);
    };
    PurchaseService.prototype.getPurchaseDocumentTypes = function () {
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_7__["of"])([{ name: 'Image', value: 1 }, { name: 'Invoice', value: 2 }]);
    };
    PurchaseService.prototype.getAllStoreSource = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_GetAllStoreSourceCode)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    PurchaseService.prototype.getEmployeesByOfficeId = function (Id) {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_HiringRequest_GetEmployeeListByOfficeId +
            '?OfficeId=' +
            Id)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) {
            var responseData = {
                data: response.data.EmployeeDetailListData,
                statusCode: response.StatusCode,
                message: response.Message
            };
            return responseData;
        }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["finalize"])(function () {
            // this.loader.hideLoader();
        }));
    };
    PurchaseService.prototype.getAllReceiptType = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_GetAllReceiptType)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    PurchaseService.prototype.getAllStatusAtTimeOfIssue = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_GetAllStatusAtTimeOfIssue)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    PurchaseService.prototype.getAllCurrency = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllCurrency)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    PurchaseService.prototype.getAllUnitTypeDetails = function () {
        return this.globalService
            .getList(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_GetAllPurchaseUnitType)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    PurchaseService.prototype.addPurchase = function (purchase, itemGroupTransportCategory, itemTransportCategory) {
        var purchaseModel = {
            ApplyDepreciation: purchase.ApplyDepreciation,
            AssetTypeId: purchase.AssetTypeId,
            BudgetLineId: purchase.BudgetLineId,
            Currency: purchase.CurrencyId,
            DeliveryDate: this.getLocalDate(purchase.ReceiptDate),
            DepreciationRate: purchase.DepreciationRate,
            InventoryId: purchase.InventoryId,
            InventoryItem: purchase.ItemId,
            InvoiceDate: this.getLocalDate(purchase.InvoiceDate),
            InvoiceNo: purchase.InvoiceNo,
            OfficeId: purchase.OfficeId,
            ProjectId: purchase.ProjectId,
            PurchaseDate: this.getLocalDate(purchase.PurchaseOrderDate),
            PurchaseName: purchase.PurchaseName,
            PurchaseOrderNo: purchase.PurchaseOrderNo,
            PurchasedById: purchase.ReceivedFromEmployeeId,
            Quantity: purchase.Quantity,
            ReceivedFromLocation: purchase.ReceivedFromLocation,
            ReceiptTypeId: purchase.ReceiptTypeId,
            Status: purchase.StatusId,
            UnitCost: purchase.Price,
            UnitType: purchase.Unit,
            TimezoneOffset: new Date().getTimezoneOffset(),
            PurchasedVehicleList: purchase.TransportVehicles,
            PurchasedGeneratorList: purchase.TransportGenerators,
            TransportItemId: purchase.TransportItemId,
            ItemGroupTransportCategory: itemGroupTransportCategory,
            ItemTransportCategory: itemTransportCategory
        };
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_AddStorePurchase, purchaseModel, { observe: 'response' })
            .pipe(
        // tap(resp => // console.log('response', resp)),
        Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return {
                StatusCode: x.status,
                Message: x.status === 200 ? 'Success' : 'Fail',
                PurchaseId: x.body
            };
        }));
    };
    PurchaseService.prototype.addProcurement = function (procurement) {
        var procurementModel = {
            Purchase: procurement.PurchaseId,
            InventoryItem: procurement.ItemId,
            IssuedQuantity: procurement.IssuedQuantity,
            MustReturn: procurement.MustReturn,
            IssuedToEmployeeId: procurement.IssuedToEmployeeId,
            IssueDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_8__["StaticUtilities"].getLocalDate(procurement.IssueDate),
            IssedToLocation: procurement.StoreSourceId,
            StatusAtTimeOfIssue: procurement.StatusId,
            Project: procurement.ProjectId,
            VoucherNo: procurement.VoucherNo
        };
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_AddItemOrder, procurementModel)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    PurchaseService.prototype.editProcurement = function (procurement) {
        var procurementModel = {
            OrderId: procurement.ProcurementId,
            Purchase: procurement.PurchaseId,
            InventoryItem: procurement.ItemId,
            IssuedQuantity: procurement.IssuedQuantity,
            MustReturn: procurement.MustReturn,
            IssuedToEmployeeId: procurement.IssuedToEmployeeId,
            IssueDate: src_app_shared_static_utilities__WEBPACK_IMPORTED_MODULE_8__["StaticUtilities"].getLocalDate(procurement.IssueDate),
            IssedToLocation: procurement.StoreSourceId,
            StatusAtTimeOfIssue: procurement.StatusId,
            Project: procurement.ProjectId,
            Returned: procurement.Returned,
            VoucherNo: procurement.VoucherNo
        };
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_EditItemOrder, procurementModel);
    };
    PurchaseService.prototype.deleteProcurement = function (orderId) {
        var procurementModel = {
            OrderId: orderId
        };
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_DeleteItemOrder, procurementModel)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    PurchaseService.prototype.checkExchangeRateExists = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_ExchangeRates_CheckExchangeRatesExist, model)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    //#region "getPurchaseListByItemId"
    PurchaseService.prototype.getPurchaseListByItemId = function (Id) {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Store_GetAllPurchasesByItem +
            '?ItemId=' +
            Id)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) {
            var responseData = {
                data: response.data.StoreItemsPurchaseViewList,
                statusCode: response.StatusCode,
                message: response.Message
            };
            return responseData;
        }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["finalize"])(function () {
            // this.loader.hideLoader();
        }));
    };
    //#endregion
    //#region "getVechileList"
    PurchaseService.prototype.getVehicleList = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_VehicleTracker_GetVehicleList, model);
    };
    //#region "getProcurementDetailsByProcurementId"
    PurchaseService.prototype.getProcurementDetailsByProcurementId = function (id) {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetProcurementDetailsByProcurementId + '?id=' + id);
    };
    //#region "getGeneratorList"
    PurchaseService.prototype.getGeneratorList = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GeneratorTracker_GetGeneratorList, model);
    };
    PurchaseService.prototype.addVehicleMileage = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_VehicleTracker_AddVehicleMileage, model);
    };
    // getStorePurchaseById
    PurchaseService.prototype.getStorePurchaseById = function (id) {
        return this.http
            .get(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetStorePurchaseById + '?id=' + id);
    };
    // getTransportItemDataSource
    PurchaseService.prototype.getTransportItemDataSource = function (id) {
        return this.http
            .get(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetTransportItemDataSource + '?id=' + id);
    };
    //#region "getItemDetailByItemId"
    PurchaseService.prototype.GetLoggedInUserUsername = function () {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Account_GetLoggedInUserUserName);
    };
    //#endregion
    //#region "getItemDetailByItemId"
    PurchaseService.prototype.getItemDetailByPurchaseId = function (Id) {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetItemDetailByItemId +
            '?PurchaseId=' +
            Id)
            .pipe(Object(rxjs_internal_operators_map__WEBPACK_IMPORTED_MODULE_4__["map"])(function (x) {
            return x;
        }));
    };
    //#endregion
    PurchaseService.prototype.getVehicleDetailById = function (Id) {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_VehicleTracker_GetVehicleById +
            '?id=' +
            Id);
    };
    PurchaseService.prototype.saveVehicleDetail = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_VehicleTracker_SaveVehicleDetail, model);
    };
    PurchaseService.prototype.getGeneratorDetailById = function (Id) {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GeneratorTracker_GetGeneratorById +
            '?id=' +
            Id);
    };
    // addGeneratorUsageHours
    PurchaseService.prototype.addGeneratorUsageHours = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GeneratorTracker_AddGeneratorUsageHours, model);
    };
    // getVehicleMonthlyBreakdown
    PurchaseService.prototype.getVehicleMonthlyBreakdown = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_VehicleTracker_GetVehicleMonthlyBreakdownDataById, model);
    };
    // addGeneratorUsageHours
    PurchaseService.prototype.getGeneratorMonthlyBreakdown = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GeneratorTracker_GetGeneratorMonthlyBreakdownDataById, model);
    };
    // saveGeneratorDetail
    PurchaseService.prototype.saveGeneratorDetail = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GeneratorTracker_EditGeneratorDetail, model);
    };
    // saveGeneratorDetail
    PurchaseService.prototype.EditStorePurchase = function (purchase) {
        var purchaseModel = {
            ApplyDepreciation: purchase.ApplyDepreciation,
            AssetTypeId: purchase.AssetTypeId,
            BudgetLineId: purchase.BudgetLineId,
            Currency: purchase.CurrencyId,
            DeliveryDate: this.getLocalDate(purchase.ReceiptDate),
            DepreciationRate: purchase.DepreciationRate,
            InventoryId: purchase.InventoryId,
            InventoryItem: purchase.ItemId,
            InvoiceDate: this.getLocalDate(purchase.InvoiceDate),
            InvoiceNo: purchase.InvoiceNo,
            OfficeId: purchase.OfficeId,
            ProjectId: purchase.ProjectId,
            PurchaseDate: this.getLocalDate(purchase.PurchaseOrderDate),
            PurchaseName: purchase.PurchaseName,
            PurchaseOrderNo: purchase.PurchaseOrderNo,
            PurchasedById: purchase.ReceivedFromEmployeeId,
            Quantity: purchase.Quantity,
            ReceivedFromLocation: purchase.ReceivedFromLocation,
            ReceiptTypeId: purchase.ReceiptTypeId,
            Status: purchase.StatusId,
            UnitCost: purchase.Price,
            UnitType: purchase.Unit,
            TimezoneOffset: new Date().getTimezoneOffset(),
            PurchasedVehicleList: purchase.TransportVehicles,
            PurchasedGeneratorList: purchase.TransportGenerators,
            TransportItemId: purchase.TransportItemId,
            PurchaseId: purchase.PurchaseId
        };
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_EditStorePurchase, purchaseModel);
    };
    PurchaseService.prototype.deleteVehicle = function (Id) {
        return this.http
            .delete(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_VehicleTracker_DeletePurchasedVehicle +
            '?id=' +
            Id);
    };
    PurchaseService.prototype.deleteGenerator = function (Id) {
        return this.http
            .delete(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_GeneratorTracker_DeletePurchasedGenerator +
            '?id=' +
            Id);
    };
    PurchaseService.prototype.getAllCurrencies = function () {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_code_GetAllCurrency);
    };
    PurchaseService.prototype.getStoreLogs = function (id, entityId) {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetVehicleGeneratorTrackerLogs + '?id=' + id + '&entityId=' + entityId);
    };
    // getDefaultUnitTypeByItemId
    PurchaseService.prototype.getDefaultUnitTypeByItemId = function (ItemId) {
        return this.globalService
            .getDataById(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetDefaultUnitTypeByItemId + '?id=' + ItemId);
    };
    PurchaseService.prototype.getProcurementDetailWithReturnsList = function (id) {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_GetProcurementDetailWithReturnsList + '?id=' + id);
    };
    PurchaseService.prototype.addProcurementReturn = function (model) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_AddProcurementReturn, model);
    };
    PurchaseService.prototype.deleteReturnProcurement = function (id) {
        return this.globalService
            .post(this.appurl.getApiUrl() + _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_StorePurchase_DeleteReturnProcurement, id);
    };
    PurchaseService.prototype.getAllVouchers = function () {
        return this.http
            .get(this.appurl.getApiUrl() +
            _shared_global__WEBPACK_IMPORTED_MODULE_3__["GLOBAL"].API_Account_GetAllVouchersWithoutFilter);
    };
    PurchaseService.prototype.getPreviousYearsList = function (years) {
        var yearDropDown = [];
        var year = new Date().getFullYear();
        for (var i = 0; i <= years; i++) {
            yearDropDown.push({ name: (year - i).toString(),
                value: year - i });
        }
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_7__["of"])(yearDropDown);
    };
    //#region "getLocalDate"
    PurchaseService.prototype.getLocalDate = function (date) {
        return new Date(new Date(date).getFullYear(), new Date(date).getMonth(), new Date(date).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds(), new Date().getMilliseconds());
    };
    PurchaseService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [_shared_services_global_services_service__WEBPACK_IMPORTED_MODULE_1__["GlobalService"],
            _shared_services_app_url_service__WEBPACK_IMPORTED_MODULE_2__["AppUrlService"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_6__["HttpClient"]])
    ], PurchaseService);
    return PurchaseService;
}());



/***/ })

}]);
//# sourceMappingURL=default~configuration-configuration-module~project-hiring-project-hiring-module~project-management-p~b18fa7fb.js.map