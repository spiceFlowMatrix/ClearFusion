import { Component, OnInit, HostListener, OnDestroy, ViewChild } from '@angular/core';
import { Validators, FormGroup, FormBuilder, AbstractControl, FormArray, FormGroupDirective } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { Observable, of, forkJoin, ReplaySubject } from 'rxjs';
import { IDropDownModel, IPurchasedFiles } from '../../models/purchase';
import { BudgetLineService } from 'src/app/dashboard/project-management/project-list/budgetlines/budget-line.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { takeUntil } from 'rxjs/operators';
import { StoreMasterCategory, StoreItemGroups, FileSourceEntityTypes, StoreItem, TransportItemCategory } from 'src/app/shared/enum';
import { VehicleDetailComponent } from '../vehicle-detail/vehicle-detail.component';
import { AddDocumentComponent } from '../document-upload/add-document.component';
import { MatDialog } from '@angular/material/dialog';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { GeneratorDetailComponent } from '../generator-detail/generator-detail.component';

@Component({
  selector: 'app-add-purchase',
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.scss']
})
export class AddPurchaseComponent implements OnInit, OnDestroy {

  addPurchaseForm: FormGroup;

  storeInventoryList: any[] = [];

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
  purchaseItemDataSource$: Observable<IDropDownModel[]>;
  hideUnitColums: Observable<{ headers?: string[], items?: string[] }>;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  ItemGroupTransportCategory: any;
  ItemTransportCategory: any;


  exchangeRateMessage = '';
  isAddPurchaseFormSubmitted = false;
  transportItemPlaceholder = '';
  selectedItemGroupName = '';
  selectedItemName = '';
  uploadedPurchasedFiles: IPurchasedFiles[] = [];
  headerText = '';
  purchaseId: number;
  showDownloadButton = false;
  selectedTransportItemType: number;

  // store enum in a variable to access it in html
  MasterCategory = StoreMasterCategory;
  ItemGroups = StoreItemGroups;
  StoreItems = StoreItem;
  ItemTransportCategoryEnum = TransportItemCategory;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  @ViewChild(VehicleDetailComponent) vehicleDetailChild: VehicleDetailComponent;
  @ViewChild(GeneratorDetailComponent) generatorDetailChild: GeneratorDetailComponent;

  constructor(private purchaseService: PurchaseService,
    private fb: FormBuilder, private budgetLineService: BudgetLineService,
    private commonLoader: CommonLoaderService, private toastr: ToastrService,
    private router: Router, private datePipe: DatePipe,
    private dialog: MatDialog, private globalSharedService: GlobalSharedService, private activatedRoute: ActivatedRoute) {

    this.initForm();
    this.activatedRoute.params.subscribe(params => {
      this.purchaseId = params['id'];
    });

    if (this.purchaseId) {
      this.headerText = 'Edit Purchase';
      this.showDownloadButton = true;
      this.getStorePurchaseById(this.purchaseId);
    } else {
      this.headerText = 'Add New Purchase';
      this.showDownloadButton = false;
    }
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

    this.getLoggedInUserUsername();

    this.addPurchaseForm.valueChanges.subscribe((data) => {
      // this.logValidationErrors(this.addPurchaseForm);
    });

    this.getScreenSize();

    this.hideUnitColums = of({
      headers: ['Name', 'Type', 'Uploaded On', 'Uploaded By'],
      items: ['Filename', 'DocumentTypeName', 'Date', 'UploadedBy']
    });



  }

  initForm() {
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
      'ReceivedFromEmployeeId': [null, [Validators.required]],
      'ReceiptTypeId': [null, [Validators.required]],
      'StatusId': [null],
      'ApplyDepreciation': [false],
      'DepreciationRate': [null],
      'TransportVehicles': new FormArray([]),
      'TransportGenerators': new FormArray([]),
      'TransportItemId': [null],
      'PurchaseId': [null]
    });
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

      // const index = response.data.PurchaseUnitTypeList.findIndex(x => x.IsDefault === true);

      // if (index !== -1) {
      //   this.addPurchaseForm.controls['Unit'].patchValue(response.data.PurchaseUnitTypeList[index].UnitTypeId);
      // }
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
  }

  getItemGroupSelectedValue(event: any) {
    this.getAllStoreItemsByGroupId(event);
  }

  getItemSelectedValue(event: any) {

    this.getTransportItemCategoryType(event);

    if (!this.purchaseId) {
      this.getDefaultUnitType(event);
    }

    this.storeItems$.subscribe(x => {
      const index = x.findIndex(y => y.value === event);
      this.selectedItemName = x[index].name;
    });
  }

  getSelectedItemName(event) {
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
    if (projectId !== undefined) {
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
        x.data.forEach(element => {
          this.storeInventoryList.push({
            name: element.InventoryCode + '-' + element.InventoryName,
            value: element.InventoryId
          })
        });
      });
  }

  getAllStoreItemGroups(inventoryId: number, groupId?: any) {
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

  getTransportItemCategoryType(itemId: number) {
    this.purchaseService
      .getTransportItemCategoryType(itemId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        this.ItemTransportCategory = x;

        if (this.ItemTransportCategory === this.ItemTransportCategoryEnum.Vehicle) {
          // this.removeVehicles(); // remove existing vehicle if any
          this.addVehicles();

          // Used to get transport item data source
          this.selectedTransportItemType = this.ItemTransportCategoryEnum.Vehicle;

          // Remove validations on Transport Item
          this.addPurchaseForm.get('TransportItemId').clearValidators();
          this.addPurchaseForm.controls['TransportItemId'].updateValueAndValidity();

          // set default quantity
          this.addPurchaseForm.controls['Quantity'].setValue(1);
          // disable quantity
          this.addPurchaseForm.controls['Quantity'].disable();
        }
        // else {
        //   this.removeVehicles();
        // }
        else if (this.ItemTransportCategory === this.ItemTransportCategoryEnum.Generator) {

          // this.removeGenerators(); // remove existing generator if any
          this.addGenerators();

          // Remove validations on Transport Item
          this.addPurchaseForm.get('TransportItemId').clearValidators();
          this.addPurchaseForm.controls['TransportItemId'].updateValueAndValidity();

          // set default quantity
          this.addPurchaseForm.controls['Quantity'].setValue(1);

          // disable quantity
          this.addPurchaseForm.controls['Quantity'].disable();
        }
        // else {
        //   this.removeGenerators();
        // }
        else {
          // enable quantity
          if (this.addPurchaseForm.controls['Quantity'].disabled) {
            this.addPurchaseForm.controls['Quantity'].enable();
            this.addPurchaseForm.controls['Quantity'].setValue(null);
          }

          this.removeGenerators();
          this.removeVehicles();
        }

        // Set dynamic required validation for transport item selected and get TransportItem Datasource for based on condition below
        if ((this.ItemGroupTransportCategory === this.ItemTransportCategoryEnum.Vehicle &&
          this.ItemTransportCategory !== this.ItemTransportCategoryEnum.Vehicle) ||
          (this.ItemGroupTransportCategory === this.ItemTransportCategoryEnum.Generator &&
            this.ItemTransportCategory !== this.ItemTransportCategoryEnum.Generator)) {
          this.getTransportItemDataSource(this.ItemTransportCategory);
          this.addPurchaseForm.get('TransportItemId').setValidators([Validators.required]);
          this.addPurchaseForm.controls['TransportItemId'].updateValueAndValidity();
          // enable quantity
          this.addPurchaseForm.controls['Quantity'].enable();
        }
        // else if (event === this.StoreItems.VehicleFuel || event === this.StoreItems.VehicleMaintenanceService ||
        //   event === this.StoreItems.VehicleMobilOil || event === this.StoreItems.VehicleSpareParts) {
        //   this.getTransportItemDataSource(TransportItemType.Vehicle);
        //   this.addPurchaseForm.get('TransportItemId').setValidators([Validators.required]);
        //   this.addPurchaseForm.controls['TransportItemId'].updateValueAndValidity();
        //   // enable quantity
        //   this.addPurchaseForm.controls['Quantity'].enable();
        // }
      });
  }

  getAllStoreItemsByGroupId(groupId: number, itemId?: any) {
    this.purchaseService
      .getItemsByItemGroupId(groupId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        this.ItemGroupTransportCategory = x.data.length > 0 ? x.data[0].ItemGroupTransportType : null;
        this.transportItemPlaceholder = (this.ItemGroupTransportCategory === this.ItemTransportCategoryEnum.Vehicle) ?
          'Purchased Vehicle Item' : 'Purchased Generator Item';
        this.storeItems$ = of(x.data.map(y => {
          return {
            name: y.ItemCode + '-' + y.ItemName,
            value: y.ItemId
          };
        }));
        if (itemId != null) {
          this.getSelectedItemName(itemId);
        }
      });
  }

  purchaseFormSubmit() {
    if (this.addPurchaseForm.get('PurchaseId').value == null || this.addPurchaseForm.get('PurchaseId').value == undefined) {
      this.addPurchaseFormSubmit();
    } else {
      this.editPurchaseFormSubmit();
    }
  }

  addPurchaseFormSubmit() {
    if (this.addPurchaseForm.valid) {
      this.isAddPurchaseFormSubmitted = true;
      this.purchaseService.addPurchase(this.addPurchaseForm.getRawValue(), this.ItemGroupTransportCategory, this.ItemTransportCategory)
        .pipe(takeUntil(this.destroyed$))
        .subscribe(x => {
          if (x.StatusCode === 200) {

            const filteredRecords = this.uploadedPurchasedFiles.filter(z => z.Id === 0);

            if (filteredRecords !== undefined && filteredRecords.length > 0) {

              for (let i = 0; i < filteredRecords.length; i++) {

                this.globalSharedService
                  .uploadFile(FileSourceEntityTypes.StorePurchase, x.PurchaseId, this.uploadedPurchasedFiles[i].File[0],
                    this.uploadedPurchasedFiles[i].DocumentTypeId)
                  .pipe(takeUntil(this.destroyed$))
                  .subscribe(y => {
                    if (i === filteredRecords.length - 1) {
                      this.isAddPurchaseFormSubmitted = false;
                      this.toastr.success('Success');
                      this.router.navigate(['store/purchases']);
                    }
                  });
              }
            } else {
              this.addPurchaseForm.reset();
              this.isAddPurchaseFormSubmitted = false;
              this.toastr.success('Success');
              this.router.navigate(['store/purchases']);
            }

          } else if (x.StatusCode === 400) {
            this.isAddPurchaseFormSubmitted = false;
            this.toastr.warning(x.Message);
          }
        },
          error => {
            this.isAddPurchaseFormSubmitted = false;
          });
    } else {
      this.toastr.warning('Please correct errors in purchase form and submit again');
    }
  }

  editPurchaseFormSubmit() {
    const purchaseId = this.addPurchaseForm.value.PurchaseId;
    if (this.addPurchaseForm.valid) {
      this.isAddPurchaseFormSubmitted = true;
      this.purchaseService.EditStorePurchase(this.addPurchaseForm.getRawValue())
        .pipe(takeUntil(this.destroyed$))
        .subscribe(x => {
          if (x) {

            const filteredRecords = this.uploadedPurchasedFiles.filter(z => z.Id === 0);
            if (filteredRecords !== undefined && filteredRecords.length > 0) {
              for (let i = 0; i < filteredRecords.length; i++) {

                if (this.uploadedPurchasedFiles[i].Id === 0) {
                  this.globalSharedService
                    .uploadFile(FileSourceEntityTypes.StorePurchase, purchaseId, this.uploadedPurchasedFiles[i].File[0],
                      this.uploadedPurchasedFiles[i].DocumentTypeId)
                    .pipe(takeUntil(this.destroyed$))
                    .subscribe(y => {
                      if (i === filteredRecords.length - 1) {
                        this.isAddPurchaseFormSubmitted = false;
                        this.router.navigate(['store/purchases']);
                        this.toastr.success('Success');
                      }
                    });
                }
              }
            } else {
              this.toastr.success('Success');
              this.isAddPurchaseFormSubmitted = false;
              this.router.navigate(['store/purchases']);
            }

          } else if (x.StatusCode === 400) {
            this.isAddPurchaseFormSubmitted = false;
            this.toastr.warning(x.Message);
          }
        },
          error => {
            this.isAddPurchaseFormSubmitted = false;
            // console.log(error);
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
      this.toastr.warning('Select Office');
      this.addPurchaseForm.get('PurchaseOrderDate').patchValue(null);
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
                this.datePipe.transform(checkExchangeRateModel.ExchangeRateDate, 'dd-MM-yyyy') +
                '. Please ensure that the exchange rate has been added and verified for the selected Purchase Order Date.';
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
    if (this.ItemTransportCategory === this.ItemTransportCategoryEnum.Vehicle) {
      // set default quantity
      this.addPurchaseForm.controls['Quantity'].setValue(this.addPurchaseForm.get('Quantity').value + 1);
      this.addVehicles();
    } else if (this.ItemTransportCategory === this.ItemTransportCategoryEnum.Generator) {
      // set default quantity
      this.addPurchaseForm.controls['Quantity'].setValue(this.addPurchaseForm.get('Quantity').value + 1);
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
      'ManufacturerCountry': ['', [Validators.required]],
      'EngineNo': ['', [Validators.required]],
      'RegistrationNo': ['', [Validators.required]],
      'ChasisNo': ['', [Validators.required]],
      'Remarks': [''],
    }));
  }

  addGenerators() {
    this.removeVehicles();
    (<FormArray>this.addPurchaseForm.get('TransportGenerators')).push(this.fb.group({
      'Voltage': ['', [Validators.required, Validators.min(0)]],
      'StartingUsage': ['', Validators.required],
      'IncurredUsage': ['', [Validators.required, Validators.min(0)]],
      'ModelYear': ['', [Validators.required, Validators.min(0)]],
      'FuelConsumptionRate': ['', [Validators.required, Validators.min(0)]],
      'MobilOilConsumptionRate': ['', [Validators.required, Validators.min(0)]],
      'OfficeId': ['', Validators.required],
      'ManufacturerCountry': ['', [Validators.required]],
      'EngineNo': ['', [Validators.required]],
      'RegistrationNo': ['', [Validators.required]],
      'ChasisNo': ['', [Validators.required]],
      'EmployeeId': ['', Validators.required],
      'Remarks': [''],
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
      data: {
        purchaseDocumentList: this.uploadedPurchasedFiles
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      // console.log(result);
      if (result) {
        this.uploadedPurchasedFiles.unshift({
          Id: result.id,
          Filename: result.file === undefined ? result.filename : result.file[0].name,
          DocumentTypeName: result.documentName,
          Date: this.datePipe.transform(result.uploadDate, 'dd-MM-yyyy'),
          UploadedBy: result.uploadBy === undefined ? localStorage.getItem('LoggedInUserName') : result.uploadBy,
          File: result.file,
          DocumentTypeId: result.documentType,
          SignedUrl: result.signedUrl
        });

        // For ngOnChanges on document-upload component
        this.uploadedPurchasedFiles = this.uploadedPurchasedFiles.slice();
      }
    });
  }

  getTransportItemDataSource(transportItemTypeId: number) {

    if (this.ItemGroupTransportCategory) {
      this.commonLoader.showLoader();
      this.purchaseService.getTransportItemDataSource(this.ItemGroupTransportCategory).subscribe(x => {
        this.purchaseItemDataSource$ = of(x.map(y => {
          return {
            value: y.ItemId,
            name: y.PurchaseIdName
          };
        }));

        this.commonLoader.hideLoader();

      }, error => {
        this.commonLoader.hideLoader();
      });
    }
  }

  getLoggedInUserUsername() {
    this.purchaseService.GetLoggedInUserUsername().subscribe(x => {
      localStorage.setItem('LoggedInUserName', x);
    });
  }

  getStorePurchaseById(purchaseId: number) {
    this.commonLoader.showLoader();
    this.purchaseService.getStorePurchaseById(Number(purchaseId))
      .subscribe(x => {
        // get All dropdown datasource
        this.getInventoriesByInventoryTypeId(x.InventoryTypeId);
        this.getAllStoreItemGroups(x.InventoryId, x.ItemGroupId);
        this.getAllStoreItemsByGroupId(x.ItemGroupId, x.ItemId);
        this.getEmployeesByOfficeId(x.OfficeId);
        this.getBudgetLineByProjectId(x.ProjectId);

        x.StoreDocumentList.forEach(y => {
          this.uploadedPurchasedFiles.push({
            Id: y.DocumentFileId,
            Filename: y.DocumentName,
            DocumentTypeName: y.DocumentTypeName,
            Date: this.datePipe.transform(y.UploadedOn, 'dd-MM-yyyy'),
            UploadedBy: y.UploadedBy,
            DocumentTypeId: y.DocumentTypeId,
            File: undefined,
            SignedUrl: y.SignedURL,
          });
        });
        // For ngOnChanges on document-upload component
        this.uploadedPurchasedFiles = this.uploadedPurchasedFiles.slice();

        this.addPurchaseForm.patchValue({
          InventoryTypeId: x.InventoryTypeId,
          InventoryId: x.InventoryId,
          ItemGroupId: x.ItemGroupId,
          ItemId: x.ItemId,
          OfficeId: x.OfficeId,
          ProjectId: x.ProjectId,
          BudgetLineId: x.BudgetLineId,
          PurchaseOrderNo: x.SerialNo,
          PurchaseName: x.PurchaseName,
          ReceiptDate: x.DeliveryDate,
          PurchaseOrderDate: x.PurchaseDate,
          InvoiceDate: x.InvoiceDate,
          InvoiceNo: x.InvoiceNo,
          AssetTypeId: x.AssetTypeId,
          Unit: x.UnitType,
          Quantity: x.Quantity,
          CurrencyId: x.Currency,
          Price: x.UnitCost,
          ReceivedFromLocation: x.ReceivedFromLocation,
          ReceivedFromEmployeeId: x.PurchasedById,
          ReceiptTypeId: x.ReceiptTypeId,
          StatusId: x.Status,
          ApplyDepreciation: x.ApplyDepreciation,
          DepreciationRate: x.DepreciationRate,
          TransportItemId: x.TransportItemId,
          PurchaseId: x.PurchaseId
        });

        this.ItemTransportCategory = x.TransportItemTypeCategory;
        this.ItemGroupTransportCategory = x.ItemGroupTransportCategory;

        if (x.PurchasedVehicleList.length > 0 || x.PurchasedGeneratorList.length > 0) {
          this.addPurchaseForm.controls['Quantity'].disable();
        }

        this.setVehicleValue(x.PurchasedVehicleList);
        this.setGeneratorValue(x.PurchasedGeneratorList);

        // get TransportItem Datasource for vehicle/generator based on condition below
        if (this.ItemGroupTransportCategory === this.ItemTransportCategoryEnum.Generator &&
          this.ItemTransportCategory !== this.ItemTransportCategoryEnum.Generator) {
          this.getTransportItemDataSource(this.ItemTransportCategory);
        } else if (this.ItemGroupTransportCategory === this.ItemTransportCategoryEnum.Vehicle &&
          this.ItemTransportCategory !== this.ItemTransportCategoryEnum.Vehicle) {
          this.getTransportItemDataSource(this.ItemTransportCategory);
        }

        this.commonLoader.hideLoader();
      }, error => {

        this.commonLoader.hideLoader();
      });
  }

  setVehicleValue(item: any[]) {
    const formArray = new FormArray([]);
    for (const x of item) {
      formArray.push(this.fb.group({
        Id: x.Id,
        PlateNo: [{ value: x.PlateNo, disabled: true }],
        EmployeeId: [{ value: x.EmployeeId, disabled: true }],
        StartingMileage: [{ value: x.StartingMileage, disabled: true }],
        IncurredMileage: [{ value: x.IncurredMileage, disabled: true }],
        FuelConsumptionRate: [{ value: x.FuelConsumptionRate, disabled: true }],
        MobilOilConsumptionRate: [{ value: x.MobilOilConsumptionRate, disabled: true }],
        OfficeId: [{ value: x.OfficeId, disabled: true }],
        ModelYear: [{ value: x.ModelYear, disabled: true }],
        ManufacturerCountry: [{ value: x.ManufacturerCountry, disabled: true }],
        EngineNo: [{ value: x.EngineNo, disabled: true }],
        RegistrationNo: [{ value: x.RegistrationNo, disabled: true }],
        ChasisNo: [{ value: x.ChasisNo, disabled: true }],
        Remarks: [{ value: x.Remarks, disabled: true }],
      }));
    }
    this.addPurchaseForm.setControl('TransportVehicles', formArray);
  }

  setGeneratorValue(item: any[]) {
    const formArray = new FormArray([]);
    for (const x of item) {
      formArray.push(this.fb.group({
        Id: x.Id,
        Voltage: [{ value: x.Voltage, disabled: true }],
        StartingUsage: [{ value: x.StartingUsage, disabled: true }],
        IncurredUsage: [{ value: x.IncurredUsage, disabled: true }],
        FuelConsumptionRate: [{ value: x.FuelConsumptionRate, disabled: true }],
        MobilOilConsumptionRate: [{ value: x.MobilOilConsumptionRate, disabled: true }],
        OfficeId: [{ value: x.OfficeId, disabled: true }],
        ModelYear: [{ value: x.ModelYear, disabled: true }],
        ManufacturerCountry: [{ value: x.ManufacturerCountry, disabled: true }],
        EngineNo: [{ value: x.EngineNo, disabled: true }],
        RegistrationNo: [{ value: x.RegistrationNo, disabled: true }],
        ChasisNo: [{ value: x.ChasisNo, disabled: true }],
        EmployeeId: [{ value: x.EmployeeId, disabled: true }],
        Remarks: [{ value: x.Remarks, disabled: true }],
      }));
    }
    this.addPurchaseForm.setControl('TransportGenerators', formArray);
  }

  deleteVehicle(index: number) {

    // decrease quantity
    this.addPurchaseForm.controls['Quantity'].setValue(this.addPurchaseForm.get('Quantity').value - 1);
    const arrayControl = this.addPurchaseForm.get('TransportVehicles') as FormArray;
    const item = arrayControl.at(index);

    if (item.value.Id !== 0 && item.value.Id !== undefined) {
      this.purchaseService.deleteVehicle(item.value.Id).subscribe(x => {
        if (x) {
          (<FormArray>this.addPurchaseForm.get('TransportVehicles')).removeAt(index);
        } else {
          this.toastr.warning('Something went wrong');
        }

      }, error => {
        this.toastr.error(error);
      });
    } else {
      (<FormArray>this.addPurchaseForm.get('TransportVehicles')).removeAt(index);
    }
  }

  deleteGenerator(index: number) {

    // decrease quantity
    this.addPurchaseForm.controls['Quantity'].setValue(this.addPurchaseForm.get('Quantity').value - 1);
    const arrayControl = this.addPurchaseForm.get('TransportGenerators') as FormArray;
    const item = arrayControl.at(index);

    if (item.value.Id !== 0 && item.value.Id !== undefined) {
      this.purchaseService.deleteGenerator(item.value.Id).subscribe(x => {
        if (x) {
          (<FormArray>this.addPurchaseForm.get('TransportGenerators')).removeAt(index);
        } else {
          this.toastr.warning('Something went wrong');
        }
      }, error => {
        console.log(error);
      });
    } else {
      (<FormArray>this.addPurchaseForm.get('TransportGenerators')).removeAt(index);
    }
  }

  onPurchaseDocumentButtonClick(event) {
    if (event.type === 'delete') {
      const index = this.uploadedPurchasedFiles.findIndex(obj => obj === event.item);

      if (index > -1) {
        if (this.uploadedPurchasedFiles[index].Id > 0) {// remove file from purchasedDocumentList and backend

          const model = {
            PageId: FileSourceEntityTypes.StorePurchase,
            DocumentFileId: event.item.Id
          };

          this.globalSharedService.deleteFile(model).subscribe((x) => {
            if (x.StatusCode === 200) {
              this.uploadedPurchasedFiles.splice(index, 1);
              this.uploadedPurchasedFiles = this.uploadedPurchasedFiles.filter(y => y.Id !== model.DocumentFileId);
            }
          });
        } else { // remove file from purchasedDocumentList
          this.uploadedPurchasedFiles.splice(index, 1);
        }
      } else {
        this.toastr.warning('Item not found to delete');
      }
    } else if (event.type === 'download') {
      window.open(event.item.SignedUrl, '_blank');
    }
  }

  enableVehicleGeneratorDiv(): boolean {
    let isEnable = false;
    if ((this.ItemGroupTransportCategory === this.ItemTransportCategoryEnum.Vehicle &&
        this.ItemTransportCategory !== this.ItemTransportCategoryEnum.Vehicle) ||
    (this.ItemGroupTransportCategory === this.ItemTransportCategoryEnum.Generator &&
      this.ItemTransportCategory !== this.ItemTransportCategoryEnum.Generator) && this.ItemTransportCategory) {
        isEnable = true;
      }
      return isEnable;
  }

  enablePurchaseItem() {
    let isEnable = false;
    if (this.ItemTransportCategory === this.ItemTransportCategoryEnum.MobilOil ||
      this.ItemTransportCategory === this.ItemTransportCategoryEnum.Fuel ||
      this.ItemTransportCategory === this.ItemTransportCategoryEnum.MaintenanceService ||
      this.ItemTransportCategory === this.ItemTransportCategoryEnum.SpareParts
      ) {
        isEnable = true;
      }
      return isEnable;
  }

  getDefaultUnitType(itemId) {
    this.purchaseService
    .getDefaultUnitTypeByItemId(itemId)
    .pipe(takeUntil(this.destroyed$))
    .subscribe(x => {

      if (x) {
        this.addPurchaseForm.controls['Unit'].patchValue(x);
      }

    });

  }

//#region "onChangeStoreInventoryValue"
onChangeStoreInventoryValue(event: any, id: number) {
debugger;
console.log(id);
}

filterInventoryName(event: any) {
  debugger;
}
//#endregion

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
