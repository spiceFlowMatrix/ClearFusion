import {
  Component,
  OnInit,
  EventEmitter,
  Output,
  Input,
  OnDestroy
} from '@angular/core';
import { StoreService } from '../../store.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { CommonService } from '../../../../service/common.service';
import { AppSettingsService } from '../../../../service/app-settings.service';

// declare let jsPDF;
// declare var $;

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit, OnDestroy {
  @Output() getItemAmounts: EventEmitter<string> = new EventEmitter<string>();
  @Input() isEditingAllowed: boolean;

  showValidationForAdd = true;
  popoverData: any;
  keyValue: any;
  defaultVisible = false;

  storeTabs = [
    {
      id: 1,
      text: 'Consumables'
    },
    {
      id: 2,
      text: 'Expendables'
    },
    {
      id: 3,
      text: 'Non-Expendables'
    }
  ];

  orderDataSource: any[];
  purchaseDataSource: any[];
  employeeList: any[];
  inventoryList: any[];
  projectList: any[];
  statusAtTimeOfIssueDropdown: any[];
  deleteOrderForm: OrderModel;
  voucherDataSource: any;
  storeLocationDropdown: any[];

  // form
  orderDetailsForm: OrderModel;

  // flag
  addOrdersFormPopupVisible = false;
  deleteConfirmationPopup = false;
  orderGridFlag = true;

  isItemPresentFlag = false;

  // loader
  addEditOrderFormPopupLoading = false;
  deleteOrderFormPopupLoading = false;

  //#endregion

  constructor(
    private storeService: StoreService,
    private commonService: CommonService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.orderDataSource = [];
    this.storeLocationDropdown = [];

    this.initializeForm();
    this.getAllVoucherList();
    this.getSourceCodeDatalist();

    this.commonService.getStoreOfficeId().subscribe(data => {
      this.getAllVoucherList();
      this.getAllEmployeeList();
    });
  }
  //#region "initializeForm"
  initializeForm() {
    this.orderDetailsForm = {
      OrderId: null,
      Purchase: null,
      InventoryItem: null,
      IssuedQuantity: null,
      MustReturn: false,
      Returned: false,
      IssuedToEmployeeId: null,
      IssueDate: null,
      ReturnedDate: null,

      IssueVoucherNo: null,
      Remarks: null,
      Project: null,
      IssedToLocation: null,
      StatusAtTimeOfIssue: null
    };

    this.deleteOrderForm = {
      OrderId: null,
      Purchase: null,
      InventoryItem: null,
      IssuedQuantity: null,
      MustReturn: false,
      Returned: false,
      IssuedToEmployeeId: null,
      IssueDate: null,
      ReturnedDate: null,

      IssueVoucherNo: null,
      Remarks: null,
      Project: null,
      IssedToLocation: null,
      StatusAtTimeOfIssue: null
    };
  }
  //#endregion

  //#region "getAllOrderDetails"
  getAllOrderDetails() {
    const itemId = localStorage.getItem('SelectedInventoryItem');
    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllItemsOrder,
        'ItemId',
        itemId
      )
      .subscribe(
        data => {
          this.orderDataSource = [];

          if (data.StatusCode === 200 && data.data.ItemOrderModelList != null) {
            if (data.data.ItemOrderModelList.length > 0) {
              data.data.ItemOrderModelList.forEach(element => {
                this.orderDataSource.push({
                  InventoryItem: element.InventoryItem,
                  IssuedQuantity: element.IssuedQuantity,
                  IssuedToEmployeeId: element.IssuedToEmployeeId,
                  MustReturn: element.MustReturn,
                  Returned: element.Returned,
                  OrderId: element.OrderId,
                  Purchase: element.Purchase,
                  IssueDate:
                    element.IssueDate != null
                      ? new Date(
                          new Date(element.IssueDate).getTime() -
                            new Date().getTimezoneOffset() * 60000
                        )
                      : null, // mandatory
                  ReturnedDate:
                    element.ReturnedDate != null
                      ? new Date(
                          new Date(element.ReturnedDate).getTime() -
                            new Date().getTimezoneOffset() * 60000
                        )
                      : null, // mandatory

                  IssueVoucherNo: element.IssueVoucherNo,
                  Remarks: element.Remarks,
                  Project: element.Project,
                  IssedToLocation: element.IssedToLocation,
                  StatusAtTimeOfIssue: element.StatusAtTimeOfIssue
                });
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllPurchaseList"
  getAllPurchaseList() {
    const purchaseItemId = localStorage.getItem('SelectedInventoryItem');
    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllPurchasesByItem,
        'itemId',
        purchaseItemId
      )
      .subscribe(
        data => {
          this.purchaseDataSource = [];
          if (data.data.StoreItemsPurchaseViewList != null) {
            if (data.data.StoreItemsPurchaseViewList.length > 0) {
              data.data.StoreItemsPurchaseViewList.forEach(element => {
                this.purchaseDataSource.push(element);
              });

              // sort in Asc
              this.purchaseDataSource = this.commonService.sortDropdown(
                this.purchaseDataSource,
                'PurchaseId'
              );
            }
          }
        },
        error => {}
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

  //#region "getAllProjectDetails"
  getAllProjectDetails() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_GetAllProjectDetail
      )
      .subscribe(
        data => {
          this.projectList = [];

          if (data.data.ProjectDetailList != null && data.StatusCode === 200) {
            if (data.data.ProjectDetailList.length > 0) {
              data.data.ProjectDetailList.forEach(element => {
                this.projectList.push(element);
              });

              this.commonService.sortDropdown(this.projectList, 'ProjectName');
            }
          }
        },
        error => {}
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

  //#region "addOrderItemDetail"
  addOrderItemDetail(model: any) {
    this.showAddEditOrderFormPopupLoading();
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddItemOrder,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Order Item Added Successfully!');
          } else {
            this.toastr.error(data.Message);
          }
          this.getAllOrderDetails();
          this.hideAddOrdersFormPopupVisible();
          this.hideAddEditOrderFormPopupLoading();
          // Parent call
          this.getItemAmounts.emit(
            localStorage.getItem('SelectedInventoryItem')
          );
        },
        error => {}
      );
  }
  //#endregion

  //#region "editOrderItemDetail"
  editOrderItemDetail(model) {
    this.showAddEditOrderFormPopupLoading();
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditItemOrder,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Order Item Updated Successfully!');
          } else {
            this.toastr.error(data.Message);
          }
          this.getAllOrderDetails();
          this.hideAddOrdersFormPopupVisible();
          this.hideAddEditOrderFormPopupLoading();
          // Parent call
          this.getItemAmounts.emit(
            localStorage.getItem('SelectedInventoryItem')
          );
        },
        error => {}
      );
  }
  //#endregion

  //#region "deleteOrderItemDetail"
  deleteOrderItemDetail(model) {
    this.showDeleteOrderFormPopupLoading();
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_DeleteItemOrder,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Order Item Deleted Successfully!');
            // this.hideConfirmationPopup();
          } else {
            this.toastr.error(data.Message);
          }
          this.getAllOrderDetails();
          this.hideDeleteConfirmationPopup();
          this.hideDeleteOrderFormPopupLoading();
          // Parent call
          this.getItemAmounts.emit(
            localStorage.getItem('SelectedInventoryItem')
          );
        },
        error => {}
      );
  }
  //#endregion

  //#region "onOrderFormSubmit"
  onOrderFormSubmit(data: OrderModel) {
    if (data != null) {
      if (data.InventoryItem != null) {
        if (data.OrderId == null) {
          const addDataModel: OrderModel = {
            OrderId: null,
            Purchase: data.Purchase,
            MustReturn: data.MustReturn,
            Returned: data.Returned,
            InventoryItem: data.InventoryItem,
            IssuedQuantity: data.IssuedQuantity,
            IssuedToEmployeeId: data.IssuedToEmployeeId,
            IssueDate:
              data.IssueDate != null
                ? new Date(
                    new Date(data.IssueDate).getFullYear(),
                    new Date(data.IssueDate).getMonth(),
                    new Date(data.IssueDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,
            ReturnedDate:
              data.ReturnedDate != null
                ? new Date(
                    new Date(data.ReturnedDate).getFullYear(),
                    new Date(data.ReturnedDate).getMonth(),
                    new Date(data.ReturnedDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,

            IssueVoucherNo: data.IssueVoucherNo,
            Remarks: data.Remarks,
            Project: data.Project,
            IssedToLocation: data.IssedToLocation,
            StatusAtTimeOfIssue: data.StatusAtTimeOfIssue
          };
          this.addOrderItemDetail(addDataModel);
        } else {
          const editDataModel: OrderModel = {
            OrderId: data.OrderId,
            Purchase: data.Purchase,
            MustReturn: data.MustReturn,
            Returned: data.Returned,
            InventoryItem: data.InventoryItem,
            IssuedQuantity: data.IssuedQuantity,
            IssuedToEmployeeId: data.IssuedToEmployeeId,
            IssueDate:
              data.IssueDate != null
                ? new Date(
                    new Date(data.IssueDate).getFullYear(),
                    new Date(data.IssueDate).getMonth(),
                    new Date(data.IssueDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,
            ReturnedDate:
              data.ReturnedDate != null
                ? new Date(
                    new Date(data.ReturnedDate).getFullYear(),
                    new Date(data.ReturnedDate).getMonth(),
                    new Date(data.ReturnedDate).getDate(),
                    new Date().getHours(),
                    new Date().getMinutes(),
                    new Date().getSeconds()
                  )
                : null,

            IssueVoucherNo: data.IssueVoucherNo,
            Remarks: data.Remarks,
            Project: data.Project,
            IssedToLocation: data.IssedToLocation,
            StatusAtTimeOfIssue: data.StatusAtTimeOfIssue
          };
          this.editOrderItemDetail(editDataModel);
        }
      } else {
        this.toastr.warning('No Inventory item is selected');
      }
    }
  }
  //#endregion

  //#region "onAddOrderPopup"
  onAddOrderPopup() {
    this.showValidationForAdd = true; // for custom validation

    this.orderDetailsForm = {
      OrderId: null,
      Purchase: null,
      InventoryItem:
        localStorage.getItem('SelectedInventoryItem') != null
          ? localStorage.getItem('SelectedInventoryItem')
          : null,
      IssuedQuantity: null,
      MustReturn: false,
      Returned: false,
      IssuedToEmployeeId: null,
      IssueDate: null,
      ReturnedDate: null,

      IssueVoucherNo: null,
      Remarks: null,
      Project: null,
      IssedToLocation: null,
      StatusAtTimeOfIssue: null
    };
    this.showAddOrdersFormPopupVisible();
  }
  //#endregion

  //#region "onEditOrderPopup"
  onEditOrderPopup(data: OrderModel) {
    this.showValidationForAdd = false; // for custom validation

    this.orderDetailsForm = {
      OrderId: data.OrderId,
      Purchase: data.Purchase,
      InventoryItem: data.InventoryItem,
      IssuedQuantity: data.IssuedQuantity,
      MustReturn: data.MustReturn,
      Returned: data.Returned,
      IssuedToEmployeeId: data.IssuedToEmployeeId,
      IssueDate: data.IssueDate,
      ReturnedDate: data.ReturnedDate,

      IssueVoucherNo: data.IssueVoucherNo,
      Remarks: data.Remarks,
      Project: data.Project,
      IssedToLocation:
        data.IssedToLocation != null
          // tslint:disable-next-line:radix
          ? parseInt(data.IssedToLocation)
          : data.IssedToLocation,
      StatusAtTimeOfIssue: data.StatusAtTimeOfIssue
    };

    localStorage.setItem('EditCurrentIssQuantity', data.IssuedQuantity); // for validation on edit important

    this.showAddOrdersFormPopupVisible();
  }
  //#endregion

  //#region "onDeleteOrderItemConfirmed"
  onDeleteOrderItemConfirmed() {
    // tslint:disable-next-line:curly
    if (this.deleteOrderForm != null)
      this.deleteOrderItemDetail(this.deleteOrderForm);
  }
  //#endregion

  //#region "onDeleteOrderPopupConfirmation"
  onDeleteOrderPopupConfirmation(data: OrderModel) {
    // init
    this.deleteOrderForm = {
      OrderId: null,
      Purchase: null,
      InventoryItem: null,
      IssuedQuantity: null,
      MustReturn: false,
      Returned: false,
      IssuedToEmployeeId: null,
      IssueDate: null,
      ReturnedDate: null,

      IssueVoucherNo: null,
      Remarks: null,
      Project: null,
      IssedToLocation: null,
      StatusAtTimeOfIssue: null
    };

    this.deleteOrderForm = {
      OrderId: data.OrderId,
      Purchase: data.Purchase,
      InventoryItem: data.InventoryItem,
      IssuedQuantity: data.IssuedQuantity,
      MustReturn: data.MustReturn,
      Returned: data.Returned,
      IssuedToEmployeeId: data.IssuedToEmployeeId,
      IssueDate: data.IssueDate,
      ReturnedDate: data.ReturnedDate,

      IssueVoucherNo: data.IssueVoucherNo,
      Remarks: data.Remarks,
      Project: data.Project,
      IssedToLocation: data.IssedToLocation,
      StatusAtTimeOfIssue: data.StatusAtTimeOfIssue
    };
    this.showDeleteConfirmationPopup();
  }
  //#endregion

  //#region "onFieldDataChanged"
  onFieldDataChanged(e) {
    if (e != null) {
      if (e.dataField === 'IssuedQuantity' && e.value != null) {
        if (e.value != null && e.value !== 0) {
        }
      }
    }
  }
  //#endregion

  //#region "onValidationCallback"
  onValidationCallback(e: any) {
    // your logic here
    if (e != null) {
      if (e.value != null) {
        // tslint:disable-next-line:radix
        const currentAmount = parseInt(localStorage.getItem('CurrentAmount'));
        // tslint:disable-next-line:curly
        if (e.value === 0) return false;
        return e.value > currentAmount ? false : true;
      }
    }
  }
  //#endregion

  //#region "onValidationCallbackEdit"
  onValidationCallbackEdit(e: any) {
    // your logic here
    if (e != null) {
      if (e.value != null) {
        // tslint:disable-next-line:radix
        let SpentAmount = parseInt(localStorage.getItem('SpentAmount'));
        // tslint:disable-next-line:radix
        const CurrentAmount = parseInt(localStorage.getItem('CurrentAmount'));

        // tslint:disable-next-line:radix
        const editCurrentIssQuantity = parseInt(
          localStorage.getItem('EditCurrentIssQuantity')
        );

        if (editCurrentIssQuantity < e.value) {
          const difference = e.value - editCurrentIssQuantity;
          SpentAmount = SpentAmount + difference;

          if (SpentAmount > CurrentAmount) {
            return false;
          }
        }
        return true;
      }
    }
  }
  //#endregion

  //#region "togglePopoverDefault"
  togglePopoverDefault(e) {
    if (e != null) {
      const keyValue = e.key.OrderId;
      this.keyValue = '#id' + e.key.OrderId; // for target

      this.popoverData = this.orderDataSource.filter(
        x => x.OrderId === keyValue
      )[0].Remarks;
    }

    this.defaultVisible = !this.defaultVisible;
  }
  //#endregion

  //#region "show/ hide"

  // Add Order form
  showAddOrdersFormPopupVisible() {
    this.addOrdersFormPopupVisible = true;
  }
  hideAddOrdersFormPopupVisible() {
    this.addOrdersFormPopupVisible = false;
  }

  showDeleteConfirmationPopup() {
    this.deleteConfirmationPopup = true;
  }
  hideDeleteConfirmationPopup() {
    this.deleteConfirmationPopup = false;
  }

  // loader
  showAddEditOrderFormPopupLoading() {
    this.addEditOrderFormPopupLoading = true;
  }
  hideAddEditOrderFormPopupLoading() {
    this.addEditOrderFormPopupLoading = false;
  }

  showDeleteOrderFormPopupLoading() {
    this.deleteOrderFormPopupLoading = true;
  }
  hideDeleteOrderFormPopupLoading() {
    this.deleteOrderFormPopupLoading = false;
  }

  //#region "logEvent"
  logEvent(actionName: string, e: any) {}
  //#endregion

  //#region "ngOnDestroy"
  ngOnDestroy() {
    localStorage.removeItem('EditCurrentIssQuantity');
  }
  //#endregion
}

class OrderModel {
  OrderId: any;
  Purchase: any;
  InventoryItem: any;
  IssuedQuantity: any;
  MustReturn: boolean;
  Returned: boolean;
  IssuedToEmployeeId: number;
  IssueDate: any;
  ReturnedDate: any;

  IssueVoucherNo: number;
  Remarks: string;
  Project: number;
  IssedToLocation: any;
  StatusAtTimeOfIssue: number;
}
