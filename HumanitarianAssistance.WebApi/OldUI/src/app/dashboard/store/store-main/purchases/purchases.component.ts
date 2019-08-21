import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Input,

  ViewChild
} from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { StoreService } from '../../store.service';
import { GLOBAL } from '../../../../shared/global';
import { DatePipe } from '@angular/common';
import { CodeService } from '../../../code/code.service';
import { CommonService } from '../../../../service/common.service';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { DxFileUploaderComponent } from 'devextreme-angular';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {

    @ViewChild('imageUploader') imageUploader: DxFileUploaderComponent;
    @ViewChild('invoiceUploader') invoiceUploader: DxFileUploaderComponent;
  @Output() getItemAmounts: EventEmitter<string> = new EventEmitter<string>();
  @Input() isEditingAllowed: boolean;

  //#region "variables"
  profileImageChangePopupLoading: boolean;
  purchaseDataSource: PurchaseModel[];
  currencyModel: any[];
  employeeList: any[];
  unitTypeDataSource: any[];
  projectArr: any[];
  budgetLineDropdown: any[];
  voucherDataSource: any[];
  statusAtTimeOfIssueDropdown: any[];
  receiptTypeDropdown: any[];
  deleteDataModel: PurchaseModel;
  storeLocationDropdown: any[];
  paymentTypesDataSource: any[];


  imageFlag = true;
  imageURL: any;
  popupImageUpdateVisible = false;
  selectedPurchaseId: any;
  flag = 0;

  purchaseDetailsFormImageFileName: any;
  purchaseDetailsFormInvoice: any;
  datePipe: any;
  deleteConfirmationPopup = false;
  isItemPresentFlag = false;
  journalcodelist: any[];

  // document
  defaultDoc: any;
  defaultObj: any;

  // ItemId
  inventoryItemId: any;
  purchaseDetailsForm: PurchaseModel;

  // loading
  addPurchaseFormPopupLoading = false;
  editPurchaseFormPopupLoading = false;
  deletePurchaseFormPopupLoading = false;

  // flag
  addPurchaseFormPopupVisible = false;
  editPurchaseFormPopupVisible = false;
  purchaseDocumentPopupVisible = false;

  assetTypeDropdown = [
    {
      AssetTypeId: 1,
      AssetTypeName: 'Cash'
    },
    {
      AssetTypeId: 2,
      AssetTypeName: 'In Kind'
    }
  ];

  defaultImagePath = 'assets/images/blank-image.png';
  selectedProfileImage: any[] = [];

  //#endregion

  constructor(
    private storeService: StoreService,
    private commonService: CommonService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private codeservice: CodeService
  ) {}

  ngOnInit() {
    this.purchaseDataSource = [];
    this.storeLocationDropdown = [];

    this.initializeForm();
    this.getCurrencyCodeList();
    this.getAllEmployeeList();
    this.getAllUnitTypeDetails();
    this.getAllProjectDetails();
    this.getAllVoucherList();
    this.getAllStatusAtTimeOfIssue();
    this.getAllReceiptType();
    this.getSourceCodeDatalist();
    this.getAllPaymentTypes();
    this.getJournalCodeList();

    this.commonService.getStoreOfficeId().subscribe(data => {
      this.getAllVoucherList();
      this.getAllEmployeeList();
      this.getAllProjectDetails();
    });
  }

  //#region "initializeForm"
  initializeForm() {
    this.purchaseDetailsForm = {
      PurchaseId: null,
      SerialNo: null, // Barcode Value
      InventoryItem: null, // Item Id
      PurchaseDate: null, // Date Of Purchase
      DeliveryDate: null, // The date that the item arrived at it's desired location or a service took place.
      Currency: null, // Currency ID
      UnitType: null,
      UnitCost: null,
      Quantity: null,
      ApplyDepreciation: false,
      DepreciationRate: null,
      ImageFileName: null, // Image String
      InvoiceFileName: null, // Invoice String
      PurchasedById: null,

      // newly added fields
      VoucherId: null,
      VoucherDate: null, // use to determine voucher date
      AssetTypeId: null, // 1.Cash, 2.In Kind
      InvoiceNo: null,
      InvoiceDate: null,
      Status: null,
      ReceiptTypeId: null,
      ReceivedFromLocation: null,
      ProjectId: null,
      BudgetLineId: null,
      PaymentTypeId: null,
      IsPurchaseVerified: false,
      VerifiedPurchaseVoucher: 0,
      OfficeId: 0,
      JournalCode: 0,
      VerifiedPurchaseVoucherReferenceNo: null,
      TimezoneOffset: null
    };
  }
  //#endregion

  //#region "getAllPurchaseList"
  getAllPurchaseList(itemId: any) {
    this.flag = 0;
    this.inventoryItemId = itemId;
    this.datePipe = new DatePipe('en-US');

    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllPurchasesByItem,
        'itemId',
        itemId
      )
      .subscribe(
        data => {
          this.purchaseDataSource = [];
            if (data.data.StoreItemsPurchaseViewList != null) {
                if (data.data.StoreItemsPurchaseViewList.length > 0) {
                    data.data.StoreItemsPurchaseViewList.forEach(element => {
                        this.purchaseDataSource.push({
                            PurchaseId: element.PurchaseId,
                            SerialNo: element.SerialNo,
                            Currency: element.Currency,
                            UnitCost: element.UnitCost,
                            Quantity: element.Quantity,
                            TotalCost: element.TotalCost,
                            UnitType: element.UnitType,
                            TotalCostUSD: element.TotalCostUSD,
                            CurrentQuantity: element.CurrentQuantity,
                            ImageFileName:
                                element.ImageFileName === ''
                                    ? this.defaultImagePath
                                    : this.setting.getDocUrl() + element.ImageFileName,
                            InvoiceFileName: element.Invoice,
                            PurchaseDate:
                                element.PurchaseDate != null
                                    ? new Date(
                                        new Date(element.PurchaseDate).getTime() -
                                        new Date().getTimezoneOffset() * 60000
                                    )
                                    : null, // mandatory
                            DeliveryDate:
                                element.DeliveryDate != null
                                    ? new Date(
                                        new Date(element.DeliveryDate).getTime() -
                                        new Date().getTimezoneOffset() * 60000
                                    )
                                    : null, // mandatory
                            ApplyDepreciation: element.ApplyDepreciation,
                            DepreciationRate: element.DepreciationRate,
                            PurchasedById: element.PurchasedById,
                            InventoryItem: element.InventoryItem,
                            // newly added fields
                            VoucherId: element.VoucherId,
                            VoucherDate:
                                element.VoucherDate != null
                                    ? new Date(
                                        new Date(element.VoucherDate).getTime() -
                                        new Date().getTimezoneOffset() * 60000
                                    )
                                    : null, // mandatory
                            AssetTypeId: element.AssetTypeId, // 1.Cash, 2.In Kind
                            InvoiceNo: element.InvoiceNo,
                            InvoiceDate:
                                element.InvoiceDate != null
                                    ? new Date(
                                        new Date(element.InvoiceDate).getTime() -
                                        new Date().getTimezoneOffset() * 60000
                                    )
                                    : null, // mandatory
                            Status: element.Status,
                            ReceiptTypeId: element.ReceiptTypeId,
                            ReceivedFromLocation: element.ReceivedFromLocation,
                            ProjectId: element.ProjectId,
                            BudgetLineId: element.BudgetLineId,
                            PaymentTypeId: element.PaymentTypeId,
                            IsPurchaseVerified: element.IsPurchaseVerified,
                            VerifiedPurchaseVoucher: element.VerifiedPurchaseVoucher,
                            OfficeId: element.OfficeId,
                            JournalCode: element.JournalCode,
                            VerifiedPurchaseVoucherReferenceNo:
                                element.VerifiedPurchaseVoucherReferenceNo,
                            TimezoneOffset: new Date().getTimezoneOffset()
                        });
                    });
                }
            } else {
                if (data.StatusCode == 400) {
                    this.toastr.error(data.Message);
                }
            }
        },
        error => {
            debugger;
            this.toastr.error(error.Message);
        }
      );
  }
  //#endregion

  //#region "getCurrencyCodeList"
  getCurrencyCodeList() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyModel = [];
          if (data.data.CurrencyList != null) {
            data.data.CurrencyList.forEach(element => {
              this.currencyModel.push(element);
            });
            // sort in Asc
            this.currencyModel = this.commonService.sortDropdown(
              this.currencyModel,
              'CurrencyCode'
            );
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getStoreLocationList"
  // Get all Source Code Data Details
  getSourceCodeDatalist() {
    this.storeService
      .GetSourceCode(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllStoreSourceCode,
        null
      )
      .subscribe(
        data => {
          this.storeLocationDropdown = [];

          if (
            data.StatusCode === 200 &&
            data.data.SourceCodeDatalist.length > 0
          ) {
            data.data.SourceCodeDatalist.forEach(element => {
              this.storeLocationDropdown.push({
                SourceCodeId: element.SourceCodeId,
                SourceCodeName: element.Code + '-' + element.Description
              });
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
  }

  //#endregion

  //#region "getAllEmployeeList"
  getAllEmployeeList() {
    // tslint:disable-next-line:radix
    const OfficeId = parseInt(localStorage.getItem('STOREOFFICEID'));
    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetEmployeeDetailByOfficeId,
        'OfficeId',
        OfficeId
      )
      .subscribe(
        data => {
          this.employeeList = [];
          if (data.data.EmployeeDetailListData != null) {
            data.data.EmployeeDetailListData.forEach(element => {
              this.employeeList.push(element);
            });

            // sort in Asc
            this.employeeList = this.commonService.sortDropdown(
              this.employeeList,
              'CodeEmployeeName'
            );
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllUnitTypeDetails"
  getAllUnitTypeDetails() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllPurchaseUnitType
      )
      .subscribe(
        data => {
          this.unitTypeDataSource = [];

          if (
            data.StatusCode === 200 &&
            data.data.PurchaseUnitTypeList != null
          ) {
            if (data.data.PurchaseUnitTypeList.length > 0) {
              data.data.PurchaseUnitTypeList.forEach(element => {
                this.unitTypeDataSource.push(element);
              });

              // sort in Asc
              this.unitTypeDataSource = this.commonService.sortDropdown(
                this.unitTypeDataSource,
                'UnitTypeName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllProjectDetails"
  getAllProjectDetails() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_GetAllProjectDetail
      )
      .subscribe(
        data => {
          this.projectArr = [];
          if (
            data.data.ProjectDetailList != null &&
            data.data.ProjectDetailList.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.ProjectDetailList.forEach(element => {
              this.projectArr.push({
                ProjectId: element.ProjectId,
                ProjectName: element.ProjectName,
                ProjectCodeName: element.ProjectCode + '-' + element.ProjectName
              });
            });
          }
        },
        error => {}
      );
  }
  //#endregion

  // //#region "Get All Budget Lines Details"
  // getAllBudgetLineDetails(ProjectId: any) {
  //   this.showAddPurchaseFormPopupLoading();
  //   this.showEditPurchaseFormPopupLoading();

  //   this.storeService
  //     .GetAllDetailsById(
  //       this.setting.getBaseUrl() +
  //         GLOBAL.API_Accounting_GetAllBudgetLineDetails,
  //       'ProjectId',
  //       ProjectId
  //     )
  //     .subscribe(
  //       data => {
  //         this.budgetLineDropdown = [];
  //         if (
  //           data.data.ProjectBudgetLineList != null &&
  //           data.StatusCode === 200
  //         ) {
  //           data.data.ProjectBudgetLineList.forEach(element => {
  //             this.budgetLineDropdown.push(element);
  //           });
  //         }
  //         this.hideAddPurchaseFormPopupLoading();
  //         this.hideEditPurchaseFormPopupLoading();
  //       },
  //       error => {
  //         this.hideAddPurchaseFormPopupLoading();
  //         this.hideEditPurchaseFormPopupLoading();
  //       }
  //     );
  // }
  // //#endregion

  //#region "Get All Budget Lines Details"
  getAllBudgetLineDetails(ProjectId: any) {
    this.showAddPurchaseFormPopupLoading();
    this.showEditPurchaseFormPopupLoading();
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_GetAllBudgetLineDetails,
        ProjectId
      )
      .subscribe(
        data => {
          this.budgetLineDropdown = [];
          if (
            data.data.ProjectBudgetLineDetailList != null &&
            data.StatusCode === 200
          ) {
            data.data.ProjectBudgetLineDetailList.forEach(element => {
              this.budgetLineDropdown.push(element);
            });
            this.hideAddPurchaseFormPopupLoading();
            this.hideEditPurchaseFormPopupLoading();
          }
        }
        ,
        error => {
          this.hideAddPurchaseFormPopupLoading();
          this.hideEditPurchaseFormPopupLoading();
        }
      );
  }
  //#endregion

  //#region "GetAllStatusAtTimeOfIssue"
  getAllStatusAtTimeOfIssue() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllStatusAtTimeOfIssue
      )
      .subscribe(
        data => {
          this.statusAtTimeOfIssueDropdown = [];
          if (
            data.data.StatusAtTimeOfIssueList != null &&
            data.StatusCode === 200
          ) {
            data.data.StatusAtTimeOfIssueList.forEach(element => {
              this.statusAtTimeOfIssueDropdown.push(element);
            });

            // sort in Asc
            this.statusAtTimeOfIssueDropdown = this.commonService.sortDropdown(
              this.statusAtTimeOfIssueDropdown,
              'StatusName'
            );
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "GetAllReceiptType"
  getAllReceiptType() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllReceiptType
      )
      .subscribe(
        data => {
          this.receiptTypeDropdown = [];
          if (data.data.ReceiptTypeList != null && data.StatusCode === 200) {
            data.data.ReceiptTypeList.forEach(element => {
              this.receiptTypeDropdown.push(element);
            });

            // sort in Asc
            this.receiptTypeDropdown = this.commonService.sortDropdown(
              this.receiptTypeDropdown,
              'ReceiptTypeName'
            );
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllVoucherList"
  getAllVoucherList() {
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('STOREOFFICEID'));
    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllVouchersByOfficeId,
        'officeid',
        officeId
      )
      .subscribe(
        data => {
          this.voucherDataSource = [];
          if (data.StatusCode === 200 && data.data.VoucherDetailList != null) {
            if (data.data.VoucherDetailList.length > 0) {
              data.data.VoucherDetailList.forEach(element => {
                this.voucherDataSource.push(element);
              });

              // sort in Asc
              this.voucherDataSource = this.commonService.sortDropdown(
                this.voucherDataSource,
                'ReferenceNo'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "addPurchaseItemDetail"
  addPurchaseItemDetail(model: any) {
    this.showAddPurchaseFormPopupLoading();
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddPurchase,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Purchase Item Added Successfully!');
            // this.getAllPurchaseList();
          } else {
            this.toastr.error(data.Message);
          }
          // Parent call
          this.getItemAmounts.emit(
            localStorage.getItem('SelectedInventoryItem')
          );
          this.getAllPurchaseList(
            localStorage.getItem('SelectedInventoryItem')
            );
           this.imageUploader.instance.reset();
          this.hideAddPurchaseFormPopupVisible();
          this.hideAddPurchaseFormPopupLoading();
        },
        error => {
          this.hideAddPurchaseFormPopupLoading();
        }
      );
  }
  //#endregion

  //#region "editPurchaseItemDetail"
  editPurchaseItemDetail(model) {
    this.showEditPurchaseFormPopupLoading();
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditPurchase,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Purchase Item Updated Successfully!');
            // this.getAllPurchaseList();
          } else {
            this.toastr.error(data.Message);
          }
          // Parent call
          this.getItemAmounts.emit(
            localStorage.getItem('SelectedInventoryItem')
          );
          this.getAllPurchaseList(
            localStorage.getItem('SelectedInventoryItem')
          );
          this.hideEditPurchaseFormPopupVisible();
          this.hideEditPurchaseFormPopupLoading();
        },
        error => {
          this.hideEditPurchaseFormPopupLoading();
        }
      );
  }
  //#endregion

  //#region "deletePurchaseItemDetail"
  deletePurchaseItemDetail(model) {
    this.showDeletePurchaseFormPopupLoading();
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_DeletePurchase,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Purchase Item Deleted Successfully!');
            this.hideConfirmationPopup();

            this.getItemAmounts.emit(
              localStorage.getItem('SelectedInventoryItem')
            );
            this.getAllPurchaseList(
              localStorage.getItem('SelectedInventoryItem')
            );
          } else {
            this.toastr.error(data.Message);
          }
          this.hideDeletePurchaseFormPopupLoading();
        },
        error => {}
      );
  }
  //#endregion

  //#region "onPurchaseFormSubmit"
  onPurchaseFormSubmit(data: PurchaseModel) {
    if (data != null) {
      if (data.PurchaseId == null) {
        if (data.InventoryItem != null) {
          const voucherDate = new Date(data.VoucherDate);

          const addDataModel: PurchaseModel = {
            PurchaseId: null,
            SerialNo: data.SerialNo,
            InventoryItem: data.InventoryItem,
            PurchaseDate:
              data.PurchaseDate != null
                ? new Date(
                    new Date(data.PurchaseDate).getFullYear(),
                    new Date(data.PurchaseDate).getMonth(),
                    new Date(data.PurchaseDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,
            DeliveryDate:
              data.DeliveryDate != null
                ? new Date(
                    new Date(data.DeliveryDate).getFullYear(),
                    new Date(data.DeliveryDate).getMonth(),
                    new Date(data.DeliveryDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,
            Currency: data.Currency,
            UnitType: data.UnitType,
            UnitCost: data.UnitCost,
            Quantity: data.Quantity,
            ApplyDepreciation: data.ApplyDepreciation,
            DepreciationRate:
              data.ApplyDepreciation === true ? data.DepreciationRate : 0,
            ImageFileName: this.purchaseDetailsFormImageFileName, // data.ImageFileName,
            InvoiceFileName: this.purchaseDetailsFormInvoice, // data.Invoice,
            PurchasedById: data.PurchasedById,

            // newly added fields
            VoucherId: data.VoucherId,
            VoucherDate:
              data.VoucherDate != null
                ? new Date(
                    new Date(data.VoucherDate).getFullYear(),
                    new Date(data.VoucherDate).getMonth(),
                    new Date(data.VoucherDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,
            AssetTypeId: data.AssetTypeId, // 1.Cash, 2.In Kind
            InvoiceNo: data.InvoiceNo,
            InvoiceDate:
              data.InvoiceDate != null
                ? new Date(
                    new Date(data.InvoiceDate).getFullYear(),
                    new Date(data.InvoiceDate).getMonth(),
                    new Date(data.InvoiceDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,
            Status: data.Status,
            ReceiptTypeId: data.ReceiptTypeId,
            ReceivedFromLocation: data.ReceivedFromLocation,
            ProjectId: data.ProjectId,
            BudgetLineId: data.BudgetLineId,
            PaymentTypeId: data.PaymentTypeId,
            IsPurchaseVerified: data.IsPurchaseVerified,
            VerifiedPurchaseVoucher: data.VerifiedPurchaseVoucher,
            // tslint:disable-next-line:radix
            OfficeId: parseInt(localStorage.getItem('STOREOFFICEID')),
            JournalCode: data.JournalCode,
            VerifiedPurchaseVoucherReferenceNo:
              data.VerifiedPurchaseVoucherReferenceNo,
              TimezoneOffset: new Date().getTimezoneOffset()
          };

          this.addPurchaseItemDetail(addDataModel);
        } else {
          this.toastr.warning('No Inventory item is selected');
        }
      } else {
        if (data.InventoryItem != null) {
          const editDataModel: PurchaseModel = {
            PurchaseId: data.PurchaseId,
            SerialNo: data.SerialNo,
            InventoryItem: data.InventoryItem,
            PurchaseDate: new Date(
              new Date(data.PurchaseDate).getFullYear(),
              new Date(data.PurchaseDate).getMonth(),
              new Date(data.PurchaseDate).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
            DeliveryDate: new Date(
              new Date(data.DeliveryDate).getFullYear(),
              new Date(data.DeliveryDate).getMonth(),
              new Date(data.DeliveryDate).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
            Currency: data.Currency,
            UnitType: data.UnitType,
            UnitCost: data.UnitCost,
            Quantity: data.Quantity,
            ApplyDepreciation: data.ApplyDepreciation,
            DepreciationRate:
              data.ApplyDepreciation === true ? data.DepreciationRate : 0,
            ImageFileName: this.purchaseDetailsFormImageFileName, // data.ImageFileName,
            InvoiceFileName: this.purchaseDetailsFormInvoice, // data.Invoice,
            PurchasedById: data.PurchasedById,

            // newly added fields
            VoucherId: data.VoucherId,
            VoucherDate:
              data.VoucherDate != null
                ? new Date(
                    new Date(data.VoucherDate).getFullYear(),
                    new Date(data.VoucherDate).getMonth(),
                    new Date(data.VoucherDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,
            AssetTypeId: data.AssetTypeId, // 1.Cash, 2.In Kind
            InvoiceNo: data.InvoiceNo,
            InvoiceDate:
              data.InvoiceDate != null
                ? new Date(
                    new Date(data.InvoiceDate).getFullYear(),
                    new Date(data.InvoiceDate).getMonth(),
                    new Date(data.InvoiceDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,
            Status: data.Status,
            ReceiptTypeId: data.ReceiptTypeId,
            ReceivedFromLocation: data.ReceivedFromLocation,
            ProjectId: data.ProjectId,
            BudgetLineId: data.BudgetLineId,
            PaymentTypeId: data.PaymentTypeId,
            IsPurchaseVerified: data.IsPurchaseVerified,
            VerifiedPurchaseVoucher: data.VerifiedPurchaseVoucher,
            // tslint:disable-next-line:radix
            OfficeId: parseInt(localStorage.getItem('STOREOFFICEID')),
            JournalCode: data.JournalCode,
            VerifiedPurchaseVoucherReferenceNo:
              data.VerifiedPurchaseVoucherReferenceNo,
              TimezoneOffset: new Date().getTimezoneOffset()
          };

          this.editPurchaseItemDetail(editDataModel);
        } else {
          this.toastr.warning('No Inventory item is selected');
        }
      }
    }
  }
  //#endregion

  onVoucherDateChanged(e: any) {}

  // selectedPurchaseItemImage: any;

  //#region "onAddPurchasePopup"
  onAddPurchasePopup() {
    // initialize form
    this.purchaseDetailsForm = {
      PurchaseId: null,
      SerialNo: null, // Barcode Value
      InventoryItem: this.inventoryItemId != null ? this.inventoryItemId : null, // Item Id
      PurchaseDate: null, // Date Of Purchase
      DeliveryDate: null, // The date that the item arrived at it's desired location or a service took place.
      Currency: null, // Currency ID
      UnitType: null,
      UnitCost: null,
      Quantity: null,
      ApplyDepreciation: false,
      DepreciationRate: null,
      ImageFileName: null, // Image String
      InvoiceFileName: null, // Invoice String
      PurchasedById: null,

      // newly added fields
      VoucherId: null,
      VoucherDate: null, // use to determine voucher date
      AssetTypeId: null, // 1.Cash, 2.In Kind
      InvoiceNo: null,
      InvoiceDate: null,
      Status: null,
      ReceiptTypeId: null,
      ReceivedFromLocation: null,
      ProjectId: null,
      BudgetLineId: null,
      PaymentTypeId: null,
      IsPurchaseVerified: false,
      VerifiedPurchaseVoucher: null,
      OfficeId: null,
      JournalCode: null,
      VerifiedPurchaseVoucherReferenceNo: null,
      TimezoneOffset: null
      };
      debugger;

     

    this.showAddPurchaseFormPopupVisible();
  }
  //#endregion

  //#region "onEditPurchasePopup"
  onEditPurchasePopup(data) {
    // tslint:disable-next-line:curly
    if (data != null)
      // initialize form
      this.purchaseDetailsForm = {
        PurchaseId: data.data.PurchaseId,
        SerialNo: data.data.SerialNo,
        InventoryItem: data.data.InventoryItem,
        PurchaseDate: data.data.PurchaseDate,
        DeliveryDate: data.data.DeliveryDate,
        Currency: data.data.Currency,
        UnitType: data.data.UnitType,
        UnitCost: data.data.UnitCost,
        Quantity: data.data.Quantity,
        ApplyDepreciation: data.data.ApplyDepreciation,
        DepreciationRate: data.data.DepreciationRate,
        ImageFileName: data.data.ImageFileName,
        InvoiceFileName: data.data.Invoice,
        PurchasedById: data.data.PurchasedById,

        // newly added fields
        VoucherId: data.data.VoucherId,
        VoucherDate: data.data.VoucherDate, // use to determine voucher date
        AssetTypeId: data.data.AssetTypeId, // 1.Cash, 2.In Kind
        InvoiceNo: data.data.InvoiceNo,
        InvoiceDate: data.data.InvoiceDate,
        Status: data.data.Status,
        ReceiptTypeId: data.data.ReceiptTypeId,
        ReceivedFromLocation:
          data.data.ReceivedFromLocation != null
            // tslint:disable-next-line:radix
            ? parseInt(data.data.ReceivedFromLocation)
            : data.data.ReceivedFromLocation,
        ProjectId: data.data.ProjectId,
        BudgetLineId: data.data.BudgetLineId,
        PaymentTypeId: data.data.PaymentTypeId,
        IsPurchaseVerified: data.data.IsPurchaseVerified,
        VerifiedPurchaseVoucher: data.data.VerifiedPurchaseVoucher,
        OfficeId: data.data.OfficeId,
        JournalCode: data.data.JournalCode,
        VerifiedPurchaseVoucherReferenceNo:
          data.data.VerifiedPurchaseVoucherReferenceNo,
          TimezoneOffset: new Date().getTimezoneOffset()
      };

    if (data.data.ProjectId != null || data.data.ProjectId === 0) {
      this.getAllBudgetLineDetails(data.data.ProjectId);
      this.purchaseDetailsForm.BudgetLineId = data.data.BudgetLineId;
    }
    this.showEditPurchaseFormPopupVisible();
  }
  //#endregion

  // Get all PaymentTypes Details
  getAllPaymentTypes() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllPaymentTypes
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200 && data.data.PaymentTypesList.length > 0) {
            this.paymentTypesDataSource = [];

            data.data.PaymentTypesList.forEach(element => {
              this.paymentTypesDataSource.push({
                AccountId: element.AccountId,
                AccountName: element.AccountName,
                Id: element.PaymentId,
                Name: element.Name
              });
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
  }

  //#region "onFieldDataChanged"
  onFieldDataChanged(event: any) {
    if (event.value != null && event.dataField === 'SerialNo') {
      // backend call and verify
    } else if (event.value != null && event.dataField === 'ProjectId') {
      // backend call and verify
      this.getAllBudgetLineDetails(event.value);
    } else if (event.value != null && event.dataField === 'VoucherId') {
      const voucherData = this.voucherDataSource.filter(
        x => x.VoucherNo === event.value
      );
      this.purchaseDetailsForm.VoucherDate = this.voucherDataSource.filter(
        x => x.VoucherNo === event.value
      )[0].VoucherDate;
    }
  }
  //#endregion

  //#region "onDeletePurchase"
  onDeletePurchase(data: any) {
    if (data != null) {
      this.deleteDataModel = {
        PurchaseId: data.PurchaseId,
        SerialNo: data.SerialNo,
        InventoryItem: data.InventoryItem,
        PurchaseDate: new Date(
          new Date(data.PurchaseDate).getFullYear(),
          new Date(data.PurchaseDate).getMonth(),
          new Date(data.PurchaseDate).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        DeliveryDate: new Date(
          new Date(data.DeliveryDate).getFullYear(),
          new Date(data.DeliveryDate).getMonth(),
          new Date(data.DeliveryDate).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        Currency: data.Currency,
        UnitType: data.UnitType,
        UnitCost: data.UnitCost,
        Quantity: data.Quantity,
        ApplyDepreciation: data.ApplyDepreciation,
        DepreciationRate:
          data.ApplyDepreciation === true ? data.DepreciationRate : 0,
        ImageFileName: this.purchaseDetailsFormImageFileName, // data.ImageFileName,
        InvoiceFileName: this.purchaseDetailsFormInvoice, // data.Invoice,
        PurchasedById: data.PurchasedById,

        // newly added fields
        VoucherId: data.VoucherId,
        VoucherDate: data.VoucherDate,
        AssetTypeId: data.AssetTypeId, // 1.Cash, 2.In Kind
        InvoiceNo: data.InvoiceNo,
        InvoiceDate: data.InvoiceDate,
        Status: data.Status,
        ReceiptTypeId: data.ReceiptTypeId,
        ReceivedFromLocation: data.ReceivedFromLocation,
        ProjectId: data.ProjectId,
        BudgetLineId: data.BudgetLineId,
        PaymentTypeId: data.PaymentTypeId,
        IsPurchaseVerified: data.IsPurchaseVerified,
        VerifiedPurchaseVoucher: data.VerifiedPurchaseVoucher,
        OfficeId: data.OfficeId,
        JournalCode: data.JournalCode,
        VerifiedPurchaseVoucherReferenceNo:
          data.VerifiedPurchaseVoucherReferenceNo,
          TimezoneOffset: new Date().getTimezoneOffset()
      };

      this.ShowConfirmationPopup();
    }
  }
  //#endregion

  //#region "Verifying Purchase"
  verifyPurchase() {
    if (
      this.purchaseDetailsForm.JournalCode != null &&
      this.purchaseDetailsForm.JournalCode !== 0 &&
      this.purchaseDetailsForm.PaymentTypeId != null &&
      this.purchaseDetailsForm.PaymentTypeId !== 0
    ) {
      this.purchaseDetailsForm.IsPurchaseVerified = true;
      // tslint:disable-next-line:radix
      this.purchaseDetailsForm.OfficeId = parseInt(
        localStorage.getItem('STOREOFFICEID')
      );
      this.showEditPurchaseFormPopupLoading();
      this.storeService
        .AddEditByModel(
          this.setting.getBaseUrl() + GLOBAL.API_Store_VerifyPurchase,
          this.purchaseDetailsForm
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Purchase Item Verified Successfully!');
            } else {
              this.toastr.error(data.Message);
            }
            // Parent call
            this.getItemAmounts.emit(
              localStorage.getItem('SelectedInventoryItem')
            );
            this.getAllPurchaseList(
              localStorage.getItem('SelectedInventoryItem')
            );
            this.hideEditPurchaseFormPopupVisible();
            this.hideEditPurchaseFormPopupLoading();
          },
          error => {
            this.hideEditPurchaseFormPopupLoading();
          }
        );
    } else {
      this.toastr.warning('Voucher Creation Fields Not Selected!!!');
    }
  }
  //#endregion

  UnverifyPurchase() {
    this.purchaseDetailsForm.IsPurchaseVerified = false;
    // tslint:disable-next-line:radix
    this.purchaseDetailsForm.OfficeId = parseInt(
      localStorage.getItem('STOREOFFICEID')
    );
    this.showEditPurchaseFormPopupLoading();
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_UnverifyPurchase,
        this.purchaseDetailsForm
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Purchase Item Unverified Successfully!');
          } else {
            this.toastr.error(data.Message);
          }
          // Parent call
          this.getItemAmounts.emit(
            localStorage.getItem('SelectedInventoryItem')
          );
          this.getAllPurchaseList(
            localStorage.getItem('SelectedInventoryItem')
          );
          this.hideEditPurchaseFormPopupVisible();
          this.hideEditPurchaseFormPopupLoading();
        },
        error => {
          this.hideEditPurchaseFormPopupLoading();
        }
      );
    }

    onAddPurchasePopUpHide() {
        debugger;
        this.imageUploader.instance.reset();
        this.invoiceUploader.instance.reset();
    }

  //#region "onSelectPurchaseForDoc"
  onSelectPurchaseForDoc(data: PurchaseModel) {
    // Child Call
    if (data != null) {
      // this.defaultDoc = data.InvoiceFileName;
      this.defaultObj = data;
    }
    this.showPurchaseDocumentPopupVisible();
  }
  //#endregion

  //#region "deletePurchaseItemConfirmed"
  deletePurchaseItemConfirmed() {
    // tslint:disable-next-line:curly
    if (this.deleteDataModel != null)
      this.deletePurchaseItemDetail(this.deleteDataModel);
  }
  //#endregion

  //#region "show/hide"

  // Add popup
  showAddPurchaseFormPopupVisible() {
    this.addPurchaseFormPopupVisible = true;
  }
  hideAddPurchaseFormPopupVisible() {
      this.addPurchaseFormPopupVisible = false;
      this.imageUploader.instance.reset();
      this.invoiceUploader.instance.reset();
  }

  // edit popup
  showEditPurchaseFormPopupVisible() {
    this.editPurchaseFormPopupVisible = true;
  }
  hideEditPurchaseFormPopupVisible() {
    this.editPurchaseFormPopupVisible = false;
  }

  // Delete confirmation
  ShowConfirmationPopup() {
    this.deleteConfirmationPopup = true;
  }

  hideConfirmationPopup() {
    this.deleteConfirmationPopup = false;
  }

  // Document
  showPurchaseDocumentPopupVisible() {
    this.purchaseDocumentPopupVisible = true;
  }

  hidePurchaseDocumentPopupVisible() {
    this.purchaseDocumentPopupVisible = false;
  }

  // Add Loader
  showAddPurchaseFormPopupLoading() {
    this.addPurchaseFormPopupLoading = true;
  }

  hideAddPurchaseFormPopupLoading() {
    this.addPurchaseFormPopupLoading = false;
  }

  // Edit Loader
  showEditPurchaseFormPopupLoading() {
    this.editPurchaseFormPopupLoading = true;
  }

  hideEditPurchaseFormPopupLoading() {
    this.editPurchaseFormPopupLoading = false;
  }

  showDeletePurchaseFormPopupLoading() {
    this.deletePurchaseFormPopupLoading = true;
  }
  hideDeletePurchaseFormPopupLoading() {
    this.deletePurchaseFormPopupLoading = false;
  }
  //#endregion

  //#region "onPurchaseImageSelect"
  onPurchaseImageSelect(event) {
    this.purchaseDetailsFormImageFileName = null;
    if (event.value != null) {
      const fileImage: File = event.value[0];
      const myReaderImage: FileReader = new FileReader();
      myReaderImage.readAsDataURL(fileImage);

      myReaderImage.onloadend = e => {
        this.purchaseDetailsFormImageFileName = myReaderImage.result;
      };
    }
  }
  //#endregion

  //#region "onPurchaseInvoiceSelect"
  onPurchaseInvoiceSelect(eventInvoice) {
    this.purchaseDetailsFormInvoice = null;
    if (eventInvoice.value != null) {
      const fileInvoice: File = eventInvoice.value[0];
      const myReaderInvoice: FileReader = new FileReader();
      myReaderInvoice.readAsDataURL(fileInvoice);
      myReaderInvoice.onloadend = e => {
        this.purchaseDetailsFormInvoice = myReaderInvoice.result;
      };
    }
  }
  //#endregion

  //#region "logEvent"
  logEvent(actionName: string, e: any) {}
  //#endregion

  //#region "Image Update"
  onImageSelectUpdate(event: any, rowData) {
    if (this.flag === 0) {
      this.flag = 1;
      this.popupImageUpdateVisible = false;
      this.selectedPurchaseId = rowData.data.PurchaseId;
      if (this.imageFlag) {
        const file: File = event.value[0];
        const myReader: FileReader = new FileReader();
        myReader.readAsDataURL(file);
        myReader.onloadend = e => {
          this.imageURL = myReader.result;
        };
        this.popupImageUpdateVisible = true;
      } else {
        this.popupImageUpdateVisible = false;
        this.imageFlag = true;
      }
    }
  }

  closeImageUpdateForm() {
    this.imageFlag = false;
    this.popupImageUpdateVisible = false;
    this.selectedProfileImage = [];
    this.flag = 0;
  }
  //#endregion

  //#region "Profile Image Change Service call"
  ChangeEmployeeImage() {
    this.imageFlag = true;
    this.profileImageChangePopupLoading = true;
    const changeEmployeeImage: any = {
      PurchaseId: this.selectedPurchaseId,
      Invoice: this.imageURL
    };
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_UpdatePurchaseImage,
        changeEmployeeImage
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Item Image Updated Successfully!!!');
          }
          this.getAllPurchaseList(
            localStorage.getItem('SelectedInventoryItem')
          );
          this.popupImageUpdateVisible = false;
          this.profileImageChangePopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
  }
  //#endregion

  //#region  getJournalCodeList
  getJournalCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_JournalCode_GetAllJournalDetail
      )
      .subscribe(
        data => {
          this.journalcodelist = [];
          if (data.data.JournalDetailList != null) {
            data.data.JournalDetailList.forEach(element => {
              this.journalcodelist.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  selectedReferenceNo(data) {}
}

class PurchaseModel {
  PurchaseId: any;
  SerialNo: any; // Barcode Value
  InventoryItem: any; // Item Id
  PurchaseDate: any; // Date Of Purchase
  DeliveryDate: any; // The date that the item arrived at it's desired location or a service took place.
  Currency: any; // Currency ID
  UnitType: any;
  UnitCost: any;
  TotalCostUSD?: any;

  TotalCost?: any;
  CurrentQuantity?: any;

  Quantity: any;
  ApplyDepreciation: boolean;
  DepreciationRate: any;
  ImageFileName: string; // Image String
  InvoiceFileName: string; // Invoice String
  PurchasedById: any; // Employee ID

  // newly added fields
  VoucherId: number;
  VoucherDate: any; // use to determine voucher date
  AssetTypeId: number; // 1.Cash, 2.In Kind
  InvoiceNo?: any;
  InvoiceDate?: any;
  Status: number;
  ReceiptTypeId: number;
  ReceivedFromLocation: number;
  ProjectId: number;
  BudgetLineId: number;
  PaymentTypeId: number;
  IsPurchaseVerified: boolean;
  VerifiedPurchaseVoucher: number;
  OfficeId?: number;
  JournalCode?: number;
  VerifiedPurchaseVoucherReferenceNo: string;
  TimezoneOffset: number;
}
