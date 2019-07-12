import {
  Component,
  OnInit,
  ViewChild,
  OnDestroy
} from '@angular/core';
import { StoreService } from '../store.service';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { PurchasesComponent } from './purchases/purchases.component';
import { OrdersComponent } from './orders/orders.component';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';
import { CodeService } from '../../code/code.service';

@Component({
  selector: 'app-store-main',
  templateUrl: './store-main.component.html',
  styleUrls: ['./store-main.component.css']
})
export class StoreMainComponent implements OnInit, OnDestroy {
  @ViewChild(PurchasesComponent)
  private purchasesChild: PurchasesComponent;

  @ViewChild(OrdersComponent)
  private orderChild: OrdersComponent;

  //#region "Variables"
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

  itemTabs = [
    {
      id: 1,
      text: 'Purchases'
    },
    {
      id: 2,
      text: 'Orders'
    }
  ];

  assetTypeList = [
    {
      AssetTypeId: 1,
      AssetTypeName: 'Consumables'
    },
    {
      AssetTypeId: 2,
      AssetTypeName: 'Expendables'
    },
    {
      AssetTypeId: 3,
      AssetTypeName: 'Non-Expendables'
    }
  ];

  itemSelectedTab = 1;
  storeSelectedTab = 1;
  isEditingAllowed = false;
  selectedInventoryId: any;
  selectedItemGroupId: any;

  inventoryList: InventoryModel[];
  itemGroupList: ItemGroupModel[];

  inventoryItemDataSource: any[];
  itemGroupDataSource: any[] = [];

  itemTypeDataSource: any[];
  voucherDataSource: any[];
  accountsList: any[];
  officecodelist: any[] =[];
  inventoryCode: string;
  inventoryItemCode: string;
  selectedOffice: any;

  inventoryItemSelectedForm: InventoryItemModel;

  inventoryItemForm: InventoryItemModel;
  itemGroupForm: ItemGroupModel;

  amountDetails: AmountDetailsModel;

  itemSpecificationDataSource: any[];
  itemSpecificationDataSourceMaster: any[];

  // form
  inventoryForm: InventoryModel;

  // flag
  inventoryItemEditPopupVisible = false;
  itemGroupEditPopupVisible = false;

  // loader
  addEditInventoryPopupLoading = false;
  addEditItemGroupPopupLoading = false;
  addEditInventoryItemPopupLoading = false;
  storeMainLoading = false;

  //#endregion

  constructor(
    private storeService: StoreService,
    private setting: AppSettingsService,
    private commonservice: CommonService,
    private toastr: ToastrService,
    private codeService: CodeService
  ) {}

  ngOnInit() {
    this.initializeForm();
    this.getOfficeCodeList();
    this.GetInputAccounts();
    this.getAllInventoryItemTypeDetails();
    this.getAllInventoryDetails(this.storeTabs[0].id);
    this.commonservice.getStoreOfficeId().subscribe(data => {
      this.getAllInventoryDetails(this.storeSelectedTab);
    });
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.Store
    );
  }

  //#region "initializeForm"
  initializeForm() {
    this.inventoryItemSelectedForm = {
      ItemId: null,
      ItemName: null,
      ItemCode: null,
      Description: null,
      ItemType: null,
      ItemGroupId: null
    };

    this.inventoryForm = {
      InventoryId: null,
      InventoryCode: null,
      InventoryName: null,
      InventoryDescription: null,
      InventoryDebitAccount: null,
      InventoryCreditAccount: null,
      AssetType: null
    };

    // Amount Detail
    this.amountDetails = {
      CurrentAmount: 0,
      ProcuredAmount: 0,
      SpentAmount: 0
    };
  }
  //#endregion

  //#region "getAllInventoryDetails"
    getAllInventoryDetails(assetType: number) {
    this.showHideStoreMainLoading(true);

    this.initializeForm();

    // Initialize grid
    this.purchasesChild.isItemPresentFlag = false; // imp
    this.orderChild.isItemPresentFlag = false; // imp
    this.purchasesChild.purchaseDataSource = [];
    this.orderChild.orderDataSource = [];

    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllInventories,
        'AssetType',
        assetType
      )
      .subscribe(
        data => {
          // datasource
          this.inventoryList = [];
          this.itemGroupDataSource = [];
          this.inventoryItemDataSource = [];
          this.initializeForm();

          if (data.StatusCode === 200 && data.data.InventoryList != null) {
            if (data.data.InventoryList.length > 0) {

              data.data.InventoryList = data.data.InventoryList.sort((n1, n2) =>  n2.InventoryId - n1.InventoryId);
              data.data.InventoryList.forEach(element => {
                this.inventoryList.push(element);
                });

                if (this.inventoryList != null && this.inventoryList != undefined) {
                    this.selectedInventoryId= this.inventoryList[0].InventoryId
                }

                this.getAllItemGroupList(this.selectedInventoryId);

            }
          }

          this.showHideStoreMainLoading(false);
        },
        error => {
          this.showHideStoreMainLoading(false);
        }
      );
  }
  //#endregion

  //#region "getAllItemGroupList"
  getAllItemGroupList(inventoryId) {
    this.showHideStoreMainLoading(true);

    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllStoreItemGroups,
        'Id',
        inventoryId
      )
      .subscribe(
        data => {
          this.itemGroupDataSource = [];
          this.inventoryItemDataSource = [];
          if (data.StatusCode === 200 && data.data.storeItemGroupList != null) {
            if (data.data.storeItemGroupList.length > 0) {
              data.data.storeItemGroupList.forEach(element => {
                this.itemGroupDataSource.push(element);
              });

                this.itemGroupDataSource = this.itemGroupDataSource.sort((a, b) => b.ItemGroupId - a.ItemGroupId);

                if (this.itemGroupDataSource != null && this.itemGroupDataSource != undefined) {
                    this.selectedItemGroupId = this.itemGroupDataSource[0].ItemGroupId;
                }
                
                                                                     
              this.getAllInventoryItemList(
                this.selectedItemGroupId
              );
            } else {
            }
          }

          this.showHideStoreMainLoading(false);
        },
        error => {
          this.showHideStoreMainLoading(false);
        }
      );
  }
  //#endregion

  //#region "getAllInventoryItemList"
  getAllInventoryItemList(itemGroupId) {
    this.showHideStoreMainLoading(true);

    // Initialize grid
    this.purchasesChild.purchaseDataSource = [];
    this.orderChild.orderDataSource = [];

    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllInventoryItems,
        'Id',
        itemGroupId
      )
      .subscribe(
        data => {
          this.inventoryItemDataSource = [];
          if (data.StatusCode === 200 && data.data.InventoryItemList != null) {
            if (data.data.InventoryItemList.length > 0) {
              data.data.InventoryItemList.forEach(element => {
                this.inventoryItemDataSource.push(element);
              });

              this.inventoryItemSelectedForm = {
                ItemId: this.inventoryItemDataSource[0].ItemId,
                ItemName: this.inventoryItemDataSource[0].ItemName,
                ItemCode: this.inventoryItemDataSource[0].ItemCode,
                Description: this.inventoryItemDataSource[0].Description,
                ItemType: this.inventoryItemDataSource[0].ItemType,
                ItemGroupId: this.inventoryItemDataSource[0].ItemGroupId
              };

              // for child call
              localStorage.setItem(
                'SelectedInventoryItem',
                this.inventoryItemDataSource[0].ItemId
              );

              this.purchasesChild.getAllPurchaseList(
                this.inventoryItemDataSource[0].ItemId
              );
              this.orderChild.getAllOrderDetails();

              // Hide / Show grid
              this.purchasesChild.isItemPresentFlag = true; // imp
              this.orderChild.isItemPresentFlag = true; // imp

              this.getItemAmounts(this.inventoryItemDataSource[0].ItemId);

              this.getItemSpecificationList(this.inventoryItemDataSource[0]);
            } else {
              // initialize
              this.inventoryItemSelectedForm = {
                ItemId: null,
                ItemName: null,
                ItemCode: null,
                Description: null,
                ItemType: null,
                ItemGroupId: null
                // Voucher: null
              };

              this.amountDetails = {
                CurrentAmount: 0,
                ProcuredAmount: 0,
                SpentAmount: 0
              };
            }
          }

          this.showHideStoreMainLoading(false);
        },
        error => {
          this.showHideStoreMainLoading(false);
        }
      );
  }
  //#endregion

  //#region "getAllInventoryItemTypeDetails"
  getAllInventoryItemTypeDetails() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllInventoryItemsType
      )
      .subscribe(
        data => {
          this.itemTypeDataSource = [];

          if (
            data.StatusCode === 200 &&
            data.data.InventoryItemTypeList != null
          ) {
            if (data.data.InventoryItemTypeList.length > 0) {
              data.data.InventoryItemTypeList.forEach(element => {
                this.itemTypeDataSource.push(element);
              });

              this.commonservice.sortDropdown(
                this.itemTypeDataSource,
                'TypeName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  GetInputAccounts() {
    this.commonservice.GetInputLevelAccountDetails().subscribe(data => {
      this.accountsList = data;
    });
  }

  //#region "getItemAmounts"
  getItemAmounts(itemId: any) {
    this.amountDetails = {
      CurrentAmount: 0,
      ProcuredAmount: 0,
      SpentAmount: 0
    };
    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetItemAmounts,
        'ItemId',
        itemId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200 && data.ItemAmount != null) {
            this.amountDetails = {
              CurrentAmount: data.ItemAmount.CurrentAmount,
              ProcuredAmount: data.ItemAmount.ProcuredAmount,
              SpentAmount: data.ItemAmount.SpentAmount
            };
          }

          localStorage.setItem('ProcuredAmount', '0'); // set default
          localStorage.setItem('SpentAmount', '0'); // set default
          localStorage.setItem('CurrentAmount', '0'); // set default
          localStorage.setItem(
            'ProcuredAmount',
            this.amountDetails.ProcuredAmount
          );
          localStorage.setItem('SpentAmount', this.amountDetails.SpentAmount);
          localStorage.setItem(
            'CurrentAmount',
            this.amountDetails.CurrentAmount
          );
        },
        error => {}
      );
  }

  //#endregion

  //#region "getItemSpecificationList"
  getItemSpecificationList(itemDetails: any) {
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('STOREOFFICEID'));

    if (officeId !== 0 && itemDetails.itemType !== 0) {
      // Master
      this.storeService
        .GetAllItemSpecification(
          this.setting.getBaseUrl() +
            GLOBAL.API_Store_GetItemSpecificationsMaster,
          officeId,
          itemDetails.itemType
        )
        .subscribe(
          data => {
            this.itemSpecificationDataSourceMaster = [];

            if (
              data.StatusCode === 200 &&
              data.data.ItemSpecificationMasterList != null
            ) {
              if (data.data.ItemSpecificationMasterList.length > 0) {
                data.data.ItemSpecificationMasterList.forEach(element => {
                  this.itemSpecificationDataSourceMaster.push(element);
                });
              }

              // Main
              this.storeService
                .GetAllItemSpecificationForMainPage(
                  this.setting.getBaseUrl() +
                    GLOBAL.API_Store_GetAllItemSpecificationsDetails,
                  itemDetails.ItemType,
                  itemDetails.ItemId,
                  officeId
                )
                .subscribe(
                  res => {
                    this.itemSpecificationDataSource = [];
                    if (
                      res.StatusCode === 200 &&
                      res.data.ItemSpecificationDetailList != null
                    ) {
                      res.data.ItemSpecificationDetailList.forEach(element => {
                        this.itemSpecificationDataSource.push({
                          ItemId: element.ItemId,
                          ItemSpecificationMasterId:
                            element.ItemSpecificationMasterId,
                          ItemSpecificationValue:
                            element.ItemSpecificationValue,
                          ItemSpecificationField: element.ItemSpecificationField
                        });
                      });
                    }
                  },
                  error => {}
                );
            }
          },
          error => {}
        );
    } else {
      this.toastr.warning('Office or Item type is not proper !');
    }
  }
  //#endregion

  //#region "editItemGroupDetail"
  editItemGroupDetail(data) {
    this.showHideAddEditItemGroupPopupLoading(true);
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditStoreItemGroup,
        data
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Item Group Updated Successfully!');
            // this.getAllInventoryDetails(this.storeSelectedTab);
            this.hideItemGroupEditPopup();
          } else {
            this.toastr.error(res.Message);
          }
          this.showHideAddEditItemGroupPopupLoading(false);
        },
        error => {
          this.showHideAddEditItemGroupPopupLoading(false);
        }
      );
  }
  //#endregion

  //#region "editInventoryItemDetail"
  editInventoryItemDetail(data) {
    this.showHideAddEditInventoryItemPopupLoading(true);
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditInventoryItems,
        data
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Inventory Updated Successfully!');
            this.getAllInventoryDetails(this.storeSelectedTab);
            this.hideInventoryItemEditPopup();
          } else {
            this.toastr.error(res.Message);
          }
          this.showHideAddEditInventoryItemPopupLoading(false);
        },
        error => {
          this.showHideAddEditInventoryItemPopupLoading(false);
        }
      );
  }
  //#endregion

  //#region "addItemGroupDetail"
  addItemGroupDetail(data) {
    this.showHideAddEditItemGroupPopupLoading(true);
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddStoreItemGroup,
        data
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.selectedItemGroupId = undefined;
            this.toastr.success('Item Group Added Successfully!');
            this.getAllItemGroupList(this.selectedInventoryId);
            this.hideItemGroupEditPopup();
          } else {
            this.toastr.error(res.Message);
          }
          this.showHideAddEditItemGroupPopupLoading(false);
        },
        error => {
          this.showHideAddEditItemGroupPopupLoading(false);
        }
      );
  }
  //#endregion

  //#region "addInventoryItemDetail"
  addInventoryItemDetail(data) {
    this.showHideAddEditInventoryItemPopupLoading(true);
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddInventoryItems,
        data
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Inventory Item Added Successfully!');
            this.getAllInventoryItemList(this.selectedItemGroupId);
            this.hideInventoryItemEditPopup();
          } else {
            this.toastr.error(res.Message);
          }
          this.showHideAddEditInventoryItemPopupLoading(false);
        },
        error => {
          this.showHideAddEditInventoryItemPopupLoading(false);
        }
      );
  }
  //#endregion

  //#region "addInventoryDetail"
  addInventoryDetail(data: any) {
    this.showHideAddEditInventoryPopupLoading(true);
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddInventory,
        data
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.selectedInventoryId = undefined;
            this.toastr.success('Inventory Added Successfully!');
            this.getAllInventoryDetails(this.storeSelectedTab);
            this.hideInventoryAddEditPopupVisible();
          } else {
            this.toastr.error(res.Message);
          }
          this.showHideAddEditInventoryPopupLoading(false);
        },
        error => {
          this.showHideAddEditInventoryPopupLoading(false);
        }
      );
  }
  //#endregion

  //#region "editInventoryDetail"
  editInventoryDetail(data) {
    this.showHideAddEditInventoryPopupLoading(true);

    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditInventory,
        data
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Inventory Updated Successfully!');
            this.getAllInventoryDetails(this.storeSelectedTab);
            this.hideInventoryAddEditPopupVisible();
          } else {
            this.toastr.error(res.Message);
          }

          this.showHideAddEditInventoryPopupLoading(false);
        },
        error => {
          this.showHideAddEditInventoryPopupLoading(false);
        }
      );
  }
  //#endregion

  //#region "deleteInventoryDetail"
  deleteInventoryDetail(data) {
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_DeleteInventory,
        data
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Inventory Deleted Successfully!');
            this.getAllInventoryDetails(this.storeSelectedTab);
          } else {
            this.toastr.error(res.Message);
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "onInventoryClick"
  onInventoryClick(data: any) {
    if (data != null) {
      if (data.InventoryId != null) {
        this.getAllItemGroupList(data.InventoryId);
        this.selectedInventoryId = data.InventoryId;
      }
    }
  }
  //#endregion

  //#region "onItemGroupClick"
  onItemGroupClick(data: any) {
    if (data != null) {
      if (data.ItemGroupId != null) {
        this.getAllInventoryItemList(data.ItemGroupId);
        this.selectedItemGroupId = data.ItemGroupId;
      }
    }
  }
  //#endregion

  //#region "onInventoryItemClick"
  onInventoryItemClick(data: any) {
    if (data != null) {
      this.inventoryItemSelectedForm = {
        ItemId: data.ItemId,
        ItemName: data.ItemName,
        ItemCode: data.ItemCode,
        Description: data.Description,
        ItemType: data.ItemType,
        ItemGroupId: data.ItemGroupId
        // Voucher: data.Voucher,
      };

      // for child call
      localStorage.setItem('SelectedInventoryItem', data.ItemId);

      this.purchasesChild.getAllPurchaseList(data.ItemId);
      this.orderChild.getAllOrderDetails();
      this.getItemAmounts(data.ItemId);

      this.getItemSpecificationList(data);
    }
  }
  //#endregion

  //#region "onAddEditItemGroup"
    onAddEditItemGroup(data: ItemGroupModel) {
    if (data != null) {
      if (data.ItemGroupId == null) {
        const addInventoryItem: ItemGroupModel = {
          ItemGroupId: null,
          ItemGroupName: data.ItemGroupName,
          ItemGroupCode: data.ItemGroupCode,
          Description: data.Description,
          InventoryId: data.InventoryId
        };

        this.addItemGroupDetail(addInventoryItem);
      } else {
        const editInventoryItem: ItemGroupModel = {
          ItemGroupId: data.ItemGroupId,
          ItemGroupName: data.ItemGroupName,
          ItemGroupCode: data.ItemGroupCode,
          Description: data.Description,
          InventoryId: data.InventoryId
        };

        this.editItemGroupDetail(editInventoryItem);
      }
    }
  }
  //#endregion

  //#region "onAddEditInventoryItem"
  onAddEditInventoryItem(data: InventoryItemModel) {
    if (data != null) {
      if (data.ItemId == null) {
        const addInventoryItem = {
          ItemId: data.ItemId,
          ItemName: data.ItemName,
          ItemCode: data.ItemCode,
          Description: data.Description,
          ItemType: data.ItemType,
          ItemGroupId: data.ItemGroupId,
          ItemInventory: this.selectedInventoryId
        };

        this.addInventoryItemDetail(addInventoryItem);
      } else {
        const editInventoryItem = {
          ItemId: data.ItemId,
          ItemName: data.ItemName,
          ItemCode: data.ItemCode,
          Description: data.Description,
          ItemType: data.ItemType,
          ItemGroupId: data.ItemGroupId,
          ItemInventory: this.selectedInventoryId
        };

        this.editInventoryItemDetail(editInventoryItem);
      }
    }
  }
  //#endregion

  //#region "onAddEditInventory"
  onAddEditInventory(data: InventoryModel) {
    if (data != null) {
      if (data.InventoryId == null) {
        const addInventoryData: InventoryModel = {
          InventoryId: null,
          InventoryCode: data.InventoryCode,
          InventoryName: data.InventoryName,
          InventoryDescription: data.InventoryDescription,
          InventoryDebitAccount: data.InventoryDebitAccount,
          InventoryCreditAccount: data.InventoryCreditAccount,
          AssetType: data.AssetType
        };
        this.addInventoryDetail(addInventoryData);
      } else {
        const editInventoryData: InventoryModel = {
          InventoryId: data.InventoryId,
          InventoryCode: data.InventoryCode,
          InventoryName: data.InventoryName,
          InventoryDescription: data.InventoryDescription,
          InventoryDebitAccount: data.InventoryDebitAccount,
          InventoryCreditAccount: data.InventoryCreditAccount,
          AssetType: data.AssetType
        };

        this.editInventoryDetail(editInventoryData);
      }
    }
  }
  //#endregion

  //#region "onShowEditInventoryItemForm"
  onShowEditInventoryItemForm() {
    this.inventoryItemForm = this.inventoryItemSelectedForm;

    this.showInventoryItemEditPopup();
  }
  //#endregion

  //#region "On tab Select"
  selectTab(e) {
    if (e != null) {
      if (e.itemData.id === 1) {
        localStorage.setItem('SelectedStoreType', '1');
        this.storeSelectedTab = 1;
        this.getAllInventoryDetails(1);
      } else if (e.itemData.id === 2) {
        localStorage.setItem('SelectedStoreType', '2');
        this.storeSelectedTab = 2;
        this.getAllInventoryDetails(2);
      } else if (e.itemData.id === 3) {
        localStorage.setItem('SelectedStoreType', '3');
        this.storeSelectedTab = 3;
        this.getAllInventoryDetails(3);
      }
    }
  }
  //#endregion

  //#region "selectItemTab"
  selectItemTab(e) {
    if (e === 1) {
      this.itemSelectedTab = 1;
      this.purchasesChild.getAllEmployeeList();
    } else {
      this.itemSelectedTab = 2;

      this.orderChild.getAllPurchaseList();
      this.orderChild.getAllEmployeeList();
      this.orderChild.getAllProjectDetails();
      this.orderChild.getAllStatusAtTimeOfIssue();
    }
  }
  //#endregion

  //#region "openAddEditInventoryForm"
  openAddEditInventoryForm() {
    this.storeService
      .GetInventoryCode(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetInventoryCode,
        this.storeSelectedTab
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.inventoryCode = data.data.InventoryCode;

            this.inventoryForm = {
              InventoryId: null,
              InventoryCode: this.inventoryCode,
              InventoryName: null,
              InventoryDescription: null,
              InventoryDebitAccount: null,
              InventoryCreditAccount: null,
              AssetType: this.storeSelectedTab
            };

            this.showInventoryAddEditPopupVisible();
          } else {
            this.toastr.error(data.Message);
          }
          this.showHideAddEditInventoryPopupLoading(false);
        },
        error => {
          this.showHideAddEditInventoryPopupLoading(false);
        }
      );
  }
  //#endregion

  //#region "onShowEditInventoryForm"
  onShowEditInventoryForm(data: InventoryModel) {
    this.inventoryForm = {
      InventoryId: data.InventoryId,
      InventoryCode: data.InventoryCode,
      InventoryName: data.InventoryName,
      InventoryDescription: data.InventoryDescription,
      InventoryDebitAccount: data.InventoryDebitAccount,
      InventoryCreditAccount: data.InventoryCreditAccount,
      AssetType: data.AssetType
    };

    this.showInventoryAddEditPopupVisible();
  }
  //#endregion

  //#region "onShowEditItemGroupForm"
  onShowEditItemGroupForm(data: ItemGroupModel) {
    this.itemGroupForm = {
      ItemGroupId: data.ItemGroupId,
      ItemGroupCode: data.ItemGroupCode,
      ItemGroupName: data.ItemGroupName,
      Description: data.Description,
      InventoryId: data.InventoryId
    };

    this.showItemGroupEditPopup();
  }
  //#endregion

  //#region "openAddEditItemGroupForm"
  openAddEditItemGroupForm() {
    this.itemGroupForm = {
      ItemGroupId: null,
      ItemGroupCode: null,
      ItemGroupName: null,
      Description: null,
      InventoryId: this.selectedInventoryId
    };
    this.getGroupItemCode(this.selectedInventoryId);
  }
  //#endregion

  //#region "openAddEditInventoryItemForm"
  openAddEditInventoryItemForm() {
    this.inventoryItemForm = {
      ItemId: null,
      ItemName: null,
      ItemCode: null,
      Description: null,
      ItemType: null,
      ItemGroupId: this.selectedItemGroupId
      // Voucher: null,
    };

    this.getItemCode(this.selectedItemGroupId);
  }
  //#endregion

  //#region "EditItemSpectification"
  EditItemSpectification(data: any) {
    if (this.itemSpecificationDataSource) {
      this.storeService
        .AddEditByModel(
          this.setting.getBaseUrl() +
            GLOBAL.API_Store_EditItemSpecificationsDetails,
          data
        )
        .subscribe(
          res => {
            if (res.StatusCode === 200) {
              this.toastr.success('Item Specification Updated Successfully!!!');
            }
          },
          error => {}
        );
    } else {
      this.toastr.warning('Nothing to save');
    }
  }
  //#endregion

  //#region "onFieldDataChangedItemGroup"
  onFieldDataChangedItemGroup(event: any) {
    if (
      this.itemGroupForm.ItemGroupId == null &&
      event.value != null &&
      event.dataField === 'InventoryId'
    ) {
      this.showHideAddEditItemGroupPopupLoading(true);
      this.storeService
        .GetInventoryItemCode(
          this.setting.getBaseUrl() + GLOBAL.API_Store_GetStoreGroupItemCode,
          event.value,
          this.storeSelectedTab
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.itemGroupForm.ItemGroupCode = data.data.ItemGroupCode;
            } else {
              this.toastr.error(data.Message);
            }
            this.showHideAddEditItemGroupPopupLoading(false);
          },
          error => {
            this.showHideAddEditItemGroupPopupLoading(false);
          }
        );
    }
  }
  //#endregion

  getGroupItemCode(selectedInventoryId: any ) {

    // this.itemGroupForm.InventoryId = this.selectedInventoryId;

      this.showHideAddEditItemGroupPopupLoading(true);
      this.storeService
        .GetInventoryItemCode(
          this.setting.getBaseUrl() + GLOBAL.API_Store_GetStoreGroupItemCode,
          selectedInventoryId,
          this.storeSelectedTab
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.itemGroupForm.InventoryId = this.selectedInventoryId;
              this.itemGroupForm.ItemGroupCode = data.data.ItemGroupCode;
              this.showItemGroupEditPopup();
            } else {
              this.toastr.error(data.Message);
            }
            this.showHideAddEditItemGroupPopupLoading(false);
          },
          error => {
            this.showHideAddEditItemGroupPopupLoading(false);
          }
        );
  }


  //#region "Get all Office Details"
  getOfficeCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          const AllOffices = localStorage.getItem('ALLOFFICES').split(',');
          this.officecodelist = [];
          if (
            data.StatusCode === 200 &&
            data.data.OfficeDetailsList.length > 0
          ) {
            data.data.OfficeDetailsList.forEach(element => {
              const officeFound = AllOffices.indexOf('' + element.OfficeId);
              if (officeFound !== -1) {
                this.officecodelist.push({
                  OfficeId: element.OfficeId,
                  OfficeCode: element.OfficeCode,
                  OfficeName: element.OfficeName,
                  SupervisorName: element.SupervisorName,
                  PhoneNo: element.PhoneNo,
                  FaxNo: element.FaxNo,
                  OfficeKey: element.OfficeKey
                });
              }
            });
            // tslint:disable-next-line:radix
            this.selectedOffice = this.selectedOffice == undefined? this.officecodelist[0].OfficeId : parseInt(localStorage.getItem('StoreOFFICEID'));

            const OfficeId = localStorage.getItem('STOREOFFICEID');
            if(OfficeId=== undefined || OfficeId=== null){
              this.commonservice.setStoreOfficeId(this.selectedOffice);
            }
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

  //#region "onFieldDataChanged"
  onFieldDataChanged(event: any) {
    if (
      this.inventoryItemForm.ItemId == null &&
      event.value != null &&
      event.dataField === 'ItemGroupId'
    ) {
     this.getItemCode(this.selectedItemGroupId);
    }
  }
  //#endregion

  getItemCode(itemGroupId: any) {
    this.storeService
        .GetInventoryItemCode(
          this.setting.getBaseUrl() + GLOBAL.API_Store_GetInventoryItemCode,
          itemGroupId,
          this.storeSelectedTab
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.inventoryItemCode = data.data.InventoryItemCode;
              this.inventoryItemForm.ItemCode = this.inventoryItemCode;
              this.showInventoryItemEditPopup();
            } else {
              this.toastr.error(data.Message);
            }
            this.showHideAddEditInventoryPopupLoading(false);
          },
          error => {
            this.showHideAddEditInventoryPopupLoading(false);
          }
        );
  }

  //#region "logEvent"
  logEvent(eventName, obj) {
    if (eventName === 'RowUpdating') {
      if (obj != null) {
        const value = Object.assign(obj.oldData, obj.newData);

        const editDataModel = {};

        this.EditItemSpectification(value);
      }
    }
  }
  //#endregion

  //#region "on office Selected"
  onOfficeSelected(event) {
    this.commonservice.setStoreOfficeId(event);
    this.commonservice.getStoreOfficeId();
  }
  //#endregion

  //#region "show/hide"

  // Group Item show/hide
  showItemGroupEditPopup() {
    this.itemGroupEditPopupVisible = true;
  }
  hideItemGroupEditPopup() {
    this.itemGroupEditPopupVisible = false;
  }

  showInventoryItemEditPopup() {
    this.inventoryItemEditPopupVisible = true;
  }
  hideInventoryItemEditPopup() {
    this.inventoryItemEditPopupVisible = false;
  }

  // tslint:disable-next-line:member-ordering
  inventoryAddEditPopupVisible = false;
  showInventoryAddEditPopupVisible() {
    this.inventoryAddEditPopupVisible = true;
  }
  hideInventoryAddEditPopupVisible() {
    this.inventoryAddEditPopupVisible = false;
  }
  //#endregion

  //#region "loader"
  showHideAddEditInventoryPopupLoading(flag: boolean) {
    this.addEditInventoryPopupLoading = flag;
  }

  showHideAddEditItemGroupPopupLoading(flag: boolean) {
    this.addEditItemGroupPopupLoading = flag;
  }

  showHideAddEditInventoryItemPopupLoading(flag: boolean) {
    this.addEditInventoryItemPopupLoading = flag;
  }

  showHideStoreMainLoading(flag: boolean) {
    this.storeMainLoading = flag;
  }

  //#endregion

  //#region "ngOnDestroy"
  ngOnDestroy() {
    localStorage.removeItem('SelectedInventoryItem');
    localStorage.removeItem('SelectedStoreType');

    localStorage.removeItem('ProcuredAmount');
    localStorage.removeItem('SpentAmount');
    localStorage.removeItem('CurrentAmount');
  }
  //#endregion
}

class InventoryModel {
  InventoryId: any;
  InventoryCode: any;
  InventoryName: any;
  InventoryDescription: any;
  InventoryDebitAccount: any;
  InventoryCreditAccount: any;
  AssetType: any;
}

class ItemGroupModel {
  ItemGroupId: any;
  ItemGroupName: any;
  ItemGroupCode: any;
  Description: any;
  InventoryId: any;
}

class InventoryItemModel {
  ItemId: any;
  ItemName: any;
  ItemCode: any;
  Description: any;
  ItemType: any;
  ItemGroupId: any;
  // Voucher: any;
}

class AmountDetailsModel {
  ProcuredAmount: any;
  SpentAmount: any;
  CurrentAmount: any;
}
