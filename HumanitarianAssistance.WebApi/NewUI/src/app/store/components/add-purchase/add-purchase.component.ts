import { Component, OnInit, HostListener, OnDestroy, ViewChild } from '@angular/core';
import { Validators, FormGroup, FormBuilder, AbstractControl, FormArray } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { Observable, of, forkJoin, ReplaySubject } from 'rxjs';
import { IDropDownModel } from '../../models/purchase';
import { BudgetLineService } from 'src/app/dashboard/project-management/project-list/budgetlines/budget-line.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { takeUntil } from 'rxjs/operators';
import { StoreMasterCategory, StoreItemGroups } from 'src/app/shared/enum';
import { VehicleDetailComponent } from '../vehicle-detail/vehicle-detail.component';
import { AddDocumentComponent } from '../document-upload/add-document.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-add-purchase',
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.scss']
})
export class AddPurchaseComponent implements OnInit, OnDestroy {

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

  exchangeRateMessage = '';
  isAddPurchaseFormSubmitted = false;
  transportItemPlaceholder = '';
  selectedItemGroupName = '';
  selectedItemName = '';

  // store enum in a variable to access it in html
  MasterCategory = StoreMasterCategory;
  ItemGroups = StoreItemGroups;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  @ViewChild(VehicleDetailComponent) child;

  constructor(private purchaseService: PurchaseService,
    private fb: FormBuilder, private budgetLineService: BudgetLineService,
    private commonLoader: CommonLoaderService, private toastr: ToastrService,
    private router: Router, private transformDate: DatePipe,
    private dialog: MatDialog) {

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
      'DepreciationRate': [null],
      'TransportVehicles': new FormArray([]),
      'TransportGenerators': new FormArray([]),
      'TransportItemId': [null]
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
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
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
      // this.logValidationErrors(this.addPurchaseForm);
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
        name: y.ReceiptTypeName
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
    this.getInventoriesByInventoryTypeId(event);
  }

  getMasterInventorySelectedValue(event: any) {
    this.getAllStoreItemGroups(event);
    if (event === StoreMasterCategory.Transport &&
      this.addPurchaseForm.get('ItemGroupId').value === this.ItemGroups.Vehicle) {
      this.addVehicles();
    } else if (event === StoreMasterCategory.Transport &&
      this.addPurchaseForm.get('ItemGroupId').value === this.ItemGroups.Generator) {
      this.addGenerators();
    }
  }

  getItemGroupSelectedValue(event: any) {
    this.getAllStoreItemsByGroupId(event);
    // Remove generator or vehicle if any
    this.removeGenerators();
    this.removeVehicles();

    if (this.addPurchaseForm.get('InventoryId').value === StoreMasterCategory.Transport &&
      event === this.ItemGroups.Vehicle) {
      this.addVehicles();
    } else if (this.addPurchaseForm.get('InventoryId').value === StoreMasterCategory.Transport &&
      event === this.ItemGroups.Generator) {
      this.addGenerators();
    }

    this.storeItemGroups$.subscribe(x => {
      const index = x.findIndex(y => y.value === event);
      this.selectedItemGroupName = x[index].name;
    });
  }

  getItemSelectedValue(event: any) {
    this.storeItems$.subscribe(x => {
      const index = x.findIndex(y => y.value === event);
      this.selectedItemName = x[index].name;
    });
  }

  getOfficeSelectedValue(event: any) {
    this.getEmployeesByOfficeId(event);
  }

  getProjectSelectedValue(event: any) {
    this.getBudgetLineByProjectId(event);
  }

  getEmployeesByOfficeId(officeId: any) {
    this.purchaseService.getEmployeesByOfficeId(officeId)
      .pipe(takeUntil(this.destroyed$))
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
      .pipe(takeUntil(this.destroyed$))
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
      .pipe(takeUntil(this.destroyed$))
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
      .pipe(takeUntil(this.destroyed$))
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
      .pipe(takeUntil(this.destroyed$))
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
    console.log(this.addPurchaseForm);
    if (this.addPurchaseForm.valid) {
      this.isAddPurchaseFormSubmitted = true;
      this.purchaseService.addPurchase(this.addPurchaseForm.value)
        .pipe(takeUntil(this.destroyed$))
        .subscribe(x => {
          if (x.StatusCode === 200) {
            let control: AbstractControl = null;
            this.addPurchaseForm.reset();
            this.addPurchaseForm.markAsUntouched();
            Object.keys(this.addPurchaseForm.controls).forEach((name) => {
              control = this.addPurchaseForm.controls[name];
              control.setErrors(null);
            });
            this.isAddPurchaseFormSubmitted = false;

            this.toastr.success(x.Message);
          } else if (x.StatusCode === 400) {
            this.isAddPurchaseFormSubmitted = false;
            this.toastr.warning(x.Message);
          }
        },
          error => {
            this.isAddPurchaseFormSubmitted = false;
            console.log(error);
          });
    } else {
      this.toastr.warning('Please correct errors in purchase form and submit again');
    }
  }

  PurchaseDateChange(event: any) {
    if (event.value) {
      this.checkExchangeRateExists(event.value);
    }
  }

  checkExchangeRateExists(exchangeRateDate: any) {
    if (this.addPurchaseForm.value.OfficeId == null || this.addPurchaseForm.value.OfficeId === undefined ||
      this.addPurchaseForm.value.OfficeId === 0) {
      return;
    }

    const checkExchangeRateModel = {
      ExchangeRateDate: new Date(
        new Date(exchangeRateDate).getFullYear(),
        new Date(exchangeRateDate).getMonth(),
        new Date(exchangeRateDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      OfficeId: this.addPurchaseForm.value.OfficeId
    };

    this.purchaseService
      .checkExchangeRateExists(checkExchangeRateModel)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        data => {
          if (data.StatusCode === 200) {

            if (!data.ResponseData) {
              this.exchangeRateMessage = 'No Exchange Rate Defined for ' +
                this.transformDate.transform(checkExchangeRateModel.ExchangeRateDate, 'dd-MM-yyyy');
            } else {
              this.exchangeRateMessage = '';
            }
          } else {
            this.toastr.error(data.Message);
          }
        },
        error => {
        }
      );
  }

  addTransportItemButtonClicked(transportItemType: number) {
    if (transportItemType === this.ItemGroups.Vehicle) {
      this.addVehicles();
    } else if (transportItemType === this.ItemGroups.Generator) {
      this.addGenerators();
    }
  }

  cancelButtonClicked() {
    this.router.navigate(['store/purchases']);
  }

  addVehicles() {
    this.removeGenerators();
    (<FormArray>this.addPurchaseForm.get('TransportVehicles')).push(this.fb.group({
      'PlateNo': ['', Validators.required],
      'EmployeeId': ['', Validators.required],
      'StartingMileage': ['', [Validators.required, Validators.min(0)]],
      'IncurredMileage': ['', [Validators.required, Validators.min(0)]],
      'FuelConsumptionRate': ['', [Validators.required, Validators.min(0)]],
      'MobilOilConsumptionRate': ['', [Validators.required, Validators.min(0)]],
      'OfficeId': ['', Validators.required],
      'ModelYear': ['', [Validators.required, Validators.maxLength(4)]],
    }));
  }

  addGenerators() {
    this.removeVehicles();
    (<FormArray>this.addPurchaseForm.get('TransportGenerators')).push(this.fb.group({
      'Voltage': ['', [Validators.required, Validators.min(0)]],
      'StartingUsageHours': ['', Validators.required],
      'IncurredUsageHours': ['', [Validators.required, Validators.min(0)]],
      'ModelYear': ['', [Validators.required, Validators.min(0)], Validators.maxLength(4)],
      'FuelConsumptionRate': ['', [Validators.required, Validators.min(0)]],
      'MobilOilConsumptionRate': ['', [Validators.required, Validators.min(0)]],
      'OfficeId': ['', Validators.required]
    }));
  }

  removeVehicles() {
    // remove vehicle if any
    const control = <FormArray>this.addPurchaseForm.controls['TransportVehicles'];
    while (control.length > 0) {
      control.removeAt(0);
    }
  }

  removeGenerators() {
    // remove generator if any
    const control = <FormArray>this.addPurchaseForm.controls['TransportGenerators'];
    while (control.length > 0) {
      control.removeAt(0);
    }
  }

  openAddDocumentDialog(): void {
    const dialogRef = this.dialog.open(AddDocumentComponent, {
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
