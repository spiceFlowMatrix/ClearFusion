import { Component, OnInit, HostListener } from '@angular/core';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { Observable, of, forkJoin } from 'rxjs';
import { IDropDownModel } from '../../models/purchase';
import { BudgetLineService } from 'src/app/dashboard/project-management/project-list/budgetlines/budget-line.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-add-purchase',
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.scss']
})
export class AddPurchaseComponent implements OnInit {

  addPurchaseForm: FormGroup;

  inventoryType$: Observable<IDropDownModel[]>;
  storeInventory$: Observable<IDropDownModel[]>;
  storeItemGroups$: Observable<IDropDownModel[]>;
  storeItems$: Observable<IDropDownModel[]>;
  offices$: Observable<IDropDownModel[]>;
  project$: Observable<IDropDownModel[]>;
  budgetLine$: Observable<IDropDownModel[]>;
  assetType$: Observable<IDropDownModel[]>;
  unit$: Observable<IDropDownModel[]>;
  currency$: Observable<IDropDownModel[]>;
  storeSource$: Observable<IDropDownModel[]>;
  employeeList$: Observable<IDropDownModel[]>;
  receiptType$: Observable<IDropDownModel[]>;
  statusList$: Observable<IDropDownModel[]>;

   // screen
   screenHeight: any;
   screenWidth: any;
   scrollStyles: any;

// This object will hold the messages to be displayed to the user
// Notice, each key in this object has the same name as the
// corresponding form control
formErrors = {
  'InventoryTypeId': '',
  'InventoryId': '',
  'ItemGroupId': '',
  'ItemId': '',
  'OfficeId': '',
  'PurchaseOrderDate': '',
  'Quantity': '',
  'CurrencyId': '',
  'Price': '',
  'ReceiptTypeId': '',
};

  // This object contains all the validation messages for this form
validationMessages = {
  'InventoryTypeId': {
    'required': 'Inventory Type is required.',
  },
  'InventoryId': {
    'required': 'Master Inventory is required.'
  },
  'ItemGroupId': {
    'required': 'Item Group is required.',
  },
  'ItemId': {
    'required': 'Item is required.',
  },
  'OfficeId': {
    'required': 'Office is required.',
  },
  'PurchaseOrderDate': {
    'required': 'Purchase Order Date is required.',
  },
  'Quantity': {
    'required': 'Quantity is required.',
  },
  'CurrencyId': {
    'required': 'Currency is required.',
  },
  'Price': {
    'required': 'Price is required.',
  },
  'ReceiptTypeId': {
    'required': 'Receipt Type is required.',
  },
};

  constructor(private purchaseService: PurchaseService,
    private fb: FormBuilder, private budgetLineService: BudgetLineService,
    private commonLoader: CommonLoaderService) {

    this.addPurchaseForm = this.fb.group({
      'InventoryTypeId': [null, [Validators.required]],
      'InventoryId': [null, [Validators.required]],
      'ItemGroupId': [null, [Validators.required]],
      'ItemId': [null, [Validators.required]],
      'OfficeId': [null, [Validators.required]],
      'ProjectId': [null],
      'BudgetLineId': [null],
      'PurchaseOrderNo': [null],
      'PurchaseName': [null, [Validators.required]],
      'ReceiptDate': [null],
      'PurchaseOrderDate': [null, [Validators.required]],
      'InvoiceDate': [null],
      'InvoiceNo': [null],
      'AssetTypeId': [null],
      'Unit': [null, [Validators.required]],
      'Quantity': [null, [Validators.required]],
      'CurrencyId': [null, [Validators.required]],
      'Price': [null, [Validators.required]],
      'ReceivedFromLocation': [null],
      'ReceivedFromEmployeeId': [null],
      'ReceiptTypeId': [null, [Validators.required]],
      'StatusId': [null],
      'ApplyDepreciation': [false],
      'DepreciationRate': [null]
    });

    this.addPurchaseForm.valueChanges.subscribe(r => {
      console.log(r);
  });
}


  ngOnInit() {

    forkJoin([
      this.getAllInventoryTypes(),
      this.getAllProjects(),
      this.getAllOffice(),
      this.getAssetType(),
      this.getAllCurrency(),
      this.getStoreLocations(),
      this.getAllStatusAtTimeOfIssue(),
      this.getAllUnitTypeDetails(),
      this.getAllReceiptType()
    ]).subscribe(result => {
      this.subscribeInventoryTypes(result[0]);
      this.subscribeAllProjects(result[1]);
      this.subscribeAllOffice(result[2]);
      this.subscribeAssetType(result[3]);
      this.subscribeAllCurrency(result[4]);
      this.subscribeStoreLocations(result[5]);
      this.subscribeAllStatusAtTimeOfIssue(result[6]);
      this.subscribeAllUnitTypeDetails(result[7]);
      this.subscribeAllReceiptType(result[8]);
    });

    this.addPurchaseForm.valueChanges.subscribe((data) => {
      this.logValidationErrors(this.addPurchaseForm);
    });

    this.getScreenSize();
  }

  subscribeInventoryTypes(response: any) {

    this.inventoryType$ = of(response.Result.map(y => {
      return {
        value: y.Id,
        name: y.InventoryName
      };
    }));
  }

  subscribeAllProjects(response: any) {
    this.project$ = of(response.data.ProjectDetailModel.map(y => {
      return {
        value: y.ProjectId,
        name: y.ProjectCode + '-' + y.ProjectName
      };
    }));
  }

  subscribeAllOffice(response: any) {
    this.offices$ = of(response.data.OfficeDetailsList.map(y => {
      return {
        value: y.OfficeId,
        name: y.OfficeCode + '-' + y.OfficeName
      };
    }));
  }

  subscribeAssetType(response: any[]) {

    const asset: IDropDownModel[] = [];

    response.forEach(x => {
      asset.push({
        value: x.AssetTypeId,
        name: x.AssetTypeName
      });
    });

    this.assetType$ = of(asset);
  }

  subscribeAllCurrency(response: any) {
    this.currency$ = of(response.data.CurrencyList.map(y => {
      return {
        value: y.CurrencyId,
        name: y.CurrencyCode + '-' + y.CurrencyName
      };
    }));
  }

  subscribeStoreLocations(response: any) {
    this.storeSource$ = of(response.data.SourceCodeDatalist.map(y => {
      return {
        value: y.SourceCodeId,
        name: y.Code + '-' + y.Description
      };
    }));
  }

  subscribeAllStatusAtTimeOfIssue(response: any) {
    this.statusList$ = of(response.data.StatusAtTimeOfIssueList.map(y => {
      return {
        value: y.StatusAtTimeOfIssueId,
        name: y.StatusName
      };
    }));
  }

  subscribeAllUnitTypeDetails(response: any) {
    this.unit$ = of(
      response.data.PurchaseUnitTypeList.map(y => {
        return {
          name: y.UnitTypeName,
          value: y.UnitTypeId
        };
    }));
  }

  subscribeAllReceiptType(response: any) {
    this.commonLoader.hideLoader();
    this.receiptType$ = of(response.data.ReceiptTypeList.map(y => {
      return {
        value: y.ReceiptTypeId,
        name:  y.ReceiptTypeName
      };
    }));
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion


  getAllInventoryTypes() {
    this.commonLoader.showLoader();
    return this.purchaseService.getAllInventoryTypeList();
  }

  getAllProjects() {
    return this.purchaseService.getAllProjectList();
  }

  getAllOffice() {
    return this.purchaseService.getAllOfficeList();
  }

  getAssetType() {
    return this.purchaseService.getPurchaseAssetType();
  }

  getAllCurrency() {
    return this.purchaseService.getAllCurrency();
  }

  getStoreLocations() {
    return this.purchaseService.getAllStoreSource();
  }

  getAllStatusAtTimeOfIssue() {
    return this.purchaseService.getAllStatusAtTimeOfIssue();
  }

  getAllUnitTypeDetails() {
    return this.purchaseService.getAllUnitTypeDetails();
  }

  getAllReceiptType() {
    return this.purchaseService.getAllReceiptType();
  }

  getInventoryTypeSelectedValue(event: any) {
    //  console.log(event);
    // this.addPurchase.get('InventoryType').patchValue(event);
    this.getInventoriesByInventoryTypeId(event);
  }

  getMasterInventorySelectedValue(event: any) {
    this.addPurchaseForm.get('InventoryId').patchValue(event);
    this.getAllStoreItemGroups(event);
  }

  getItemGroupSelectedValue(event: any) {
    this.addPurchaseForm.get('ItemGroupId').patchValue(event);
    this.getAllStoreItemsByGroupId(event);
  }

  getItemSelectedValue(event: any) {
    this.addPurchaseForm.get('ItemId').patchValue(event);
  }

  getOfficeSelectedValue(event: any) {
    this.addPurchaseForm.get('OfficeId').patchValue(event);
    this.getEmployeesByOfficeId(event);
  }

  getProjectSelectedValue(event: any) {
    this.addPurchaseForm.get('ProjectId').patchValue(event);
    this.getBudgetLineByProjectId(event);
  }

  getBudgetSelectedValue(event: any) {
    this.addPurchaseForm.get('BudgetLineId').patchValue(event);
  }

  getAssetTypeSelectedValue(event: any) {
    this.addPurchaseForm.get('AssetTypeId').patchValue(event);
  }

  getUnitSelectedValue(event: any) {
    this.addPurchaseForm.get('Unit').patchValue(event);
  }

  getCurrencySelectedValue(event: any) {
    this.addPurchaseForm.get('CurrencyId').patchValue(event);
  }

  getStoreSourceSelectedValue(event: any) {
    this.addPurchaseForm.get('ReceivedFromLocation').patchValue(event);
  }

  getEmployeeSelectedValue(event: any) {
    this.addPurchaseForm.get('ReceivedFromEmployeeId').patchValue(event);
  }

  getReceiptSelectedValue(event: any) {
    this.addPurchaseForm.get('ReceiptTypeId').patchValue(event);
  }

  getStatusSelectedValue(event: any) {
    this.addPurchaseForm.get('StatusId').patchValue(event);
  }

  getEmployeesByOfficeId(officeId: any) {
    this.purchaseService.getEmployeesByOfficeId(officeId)
      .subscribe(x => {
        this.employeeList$ = of(x.data.map(y => {
          return {
            name: y.CodeEmployeeName,
            value: y.EmployeeId
          };
        }));
      });
  }

  getBudgetLineByProjectId(projectId: any) {
    this.budgetLineService.GetProjectBudgetLineList(projectId)
      .subscribe(x => {
        this.budgetLine$ = of(x.data.map(y => {
          return {
            name: y.BudgetCode + '-' + y.BudgetName,
            value: y.BudgetLineId
          };
        }));
      });
  }


  getInventoriesByInventoryTypeId(inventoryTypeId: number) {
    this.purchaseService
      .getInventoriesByInventoryTypeId(inventoryTypeId)
      .subscribe(x => {

        this.storeInventory$ = of(x.data.map(y => {
          return {
            name: y.InventoryCode + '-' + y.InventoryName,
            value: y.InventoryId
          };
        }));
      });
  }

  getAllStoreItemGroups(inventoryId: number) {
    this.purchaseService
      .getItemGroupByInventoryId(inventoryId)
      .subscribe(x => {
        this.storeItemGroups$ = of(x.data.map(y => {
          return {
            name: y.ItemGroupCode + '-' + y.ItemGroupName,
            value: y.ItemGroupId
          };
        }));
      });
  }

  getAllStoreItemsByGroupId(groupId: number) {
    this.purchaseService
      .getItemsByItemGroupId(groupId)
      .subscribe(x => {
        this.storeItems$ = of(x.data.map(y => {
          return {
            name: y.ItemCode + '-' + y.ItemName,
            value: y.ItemId
          };
        }));
      });
  }

  addPurchaseFormSubmit() {
    debugger;
    if (this.addPurchaseForm.valid) {
      this.purchaseService.addPurchase(this.addPurchaseForm.value);
    }
  }

  logValidationErrors(group: FormGroup): void {

    // Loop through each control key in the FormGroup
    Object.keys(group.controls).forEach((key: string) => {
      // Get the control. The control can be a nested form group
      const abstractControl = group.get(key);
      // If the control is nested form group, recursively call
      // this same method
      if (abstractControl instanceof FormGroup) {
        this.logValidationErrors(abstractControl);
        // If the control is a FormControl
      } else {
        // Clear the existing validation errors
        this.formErrors[key] = '';
        if (abstractControl && !abstractControl.valid) {
          // Get all the validation messages of the form control
          // that has failed the validation
          const messages = this.validationMessages[key];
          // Find which validation has failed. For example required,
          // minlength or maxlength. Store that error message in the
          // formErrors object. The UI will bind to this object to
          // display the validation errors
          for (const errorKey in abstractControl.errors) {
            if (errorKey) {
              this.formErrors[key] += messages[errorKey] + ' ';
            }
          }
        }
      }
    });
  }
}
