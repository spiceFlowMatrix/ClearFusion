import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { IDropDownModel } from '../../models/purchase';
import { Observable } from 'rxjs/internal/Observable';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/operators';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { PurchaseService } from '../../services/purchase.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { of } from 'rxjs/internal/observable/of';
import { ToastrService } from 'ngx-toastr';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-procurements',
  templateUrl: './add-procurements.component.html',
  styleUrls: ['./add-procurements.component.scss']
})
export class AddProcurementsComponent implements OnInit, OnDestroy {

  addProcurementForm: FormGroup;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  inventoryType$: Observable<IDropDownModel[]>;
  storeInventory$: Observable<IDropDownModel[]>;
  storeItemGroups$: Observable<IDropDownModel[]>;
  storeItems$: Observable<IDropDownModel[]>;
  purchases$: Observable<IDropDownModel[]>;
  employeeList$: Observable<IDropDownModel[]>;
  projects$: Observable<IDropDownModel[]>;
  storeSource$: Observable<IDropDownModel[]>;
  statusList$: Observable<IDropDownModel[]>;

  purchaseId: number;
  officeId: number;
  itemsToBeProcuredCount: number;
  originalQuantityOfPurchase = 0;
  procuredQuantityOfPurchase = 0;
  originalProcuredQuantity = 0;
  currentQuantityOfPurchase = 0;
  showMaxProcurementMessage = false;
  originalIssuedQuantity = 0;
  procurementPageTitle = 'Add Procurement';
  maxProcurementMessage =
  'Issued quantity exceeds purchased quantity. Either decrease the quantity, return the issued item or create a new Purchase for the item';

  constructor(private fb: FormBuilder, private purchaseService: PurchaseService,
    private commonLoader: CommonLoaderService, public toastr: ToastrService,
    private dialogRef: MatDialogRef<AddProcurementsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {

  }

  ngOnInit() {
    this.initForm();
    if (this.data.procurement) {
      this.addProcurementForm.patchValue({
        'ProcurementId': this.data.procurement.OrderId,
        'IssuedToEmployeeId': this.data.procurement.EmployeeId,
        'MustReturn': this.data.procurement.MustReturn,
        'IssueDate': this.data.procurement.IssueDate,
        'IssuedQuantity': this.data.procurement.ProcuredAmount,
        'ProjectId': this.data.procurement.ProjectId,
        'StatusId': this.data.procurement.StatusId,
        'StoreSourceId': +this.data.procurement.LocationId,
        'Returned': this.data.procurement.Returned
      });
      this.procurementPageTitle = 'Edit Procurement';
    }

    this.purchaseId = this.data.value;
    this.officeId = this.data.officeId;

    forkJoin([
      this.getAllInventoryTypes(),
      this.getAllProjects(),
      this.getStoreLocations(),
      this.getAllStatusAtTimeOfIssue(),
      this.getAllEmployeesByOfficeId()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeInventoryTypes(result[0]);
        this.subscribeAllProjects(result[1]);
        this.subscribeStoreLocations(result[2]);
        this.subscribeAllStatusAtTimeOfIssue(result[3]);
        this.subscribeAllEmployeesByOfficeId(result[4]);
      });

    this.getItemDetailByPurchaseId(this.purchaseId);
  }

  initForm() {
    this.addProcurementForm = this.fb.group({
      'ProcurementId': [null],
      'InventoryTypeId': [{value: null, disabled: true}, [Validators.required]],
      'InventoryId': [{value: null, disabled: true}, [Validators.required]],
      'ItemGroupId': [{value: null, disabled: true}, [Validators.required]],
      'ItemId': [{value: null, disabled: true}, [Validators.required]],
      'PurchaseId': [{value: null, disabled: true}, [Validators.required]],
      'IssuedQuantity': [0, [Validators.required, Validators.min(1)]],
      'IssuedToEmployeeId': [null, [Validators.required]],
      'IssueDate': [null, [Validators.required]],
      'ProjectId': [null, [Validators.required]],
      'StoreSourceId': [null, [Validators.required]],
      'StatusId': [null, [Validators.required]],
      'MustReturn': [false],
      'EmployeeName': [null],
      'Returned': [false]
    });
  }

  getAllInventoryTypes() {
    return this.purchaseService.getAllInventoryTypeList();
  }

  getAllProjects() {
    return this.purchaseService.getAllProjectList();
  }

  getAllEmployeesByOfficeId() {
    if (this.officeId) {
      return this.purchaseService.getEmployeesByOfficeId(this.officeId);
    } else {
      this.toastr.warning('Office not selected');
    }
  }

  getStoreLocations() {
    return this.purchaseService.getAllStoreSource();
  }

  getAllStatusAtTimeOfIssue() {
    return this.purchaseService.getAllStatusAtTimeOfIssue();
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
    this.projects$ = of(response.data.ProjectDetailModel.map(y => {
      return {
        value: y.ProjectId,
        name: y.ProjectCode + '-' + y.ProjectName
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

  subscribeInventoriesByInventoryTypeId(response: any) {
    this.storeInventory$ = of(response.data.map(y => {
      return {
        name: y.InventoryCode + '-' + y.InventoryName,
        value: y.InventoryId
      };
    }));
  }

  subscribeAllStoreItemGroups(response: any) {
    this.storeItemGroups$ = of(response.data.map(y => {
      return {
        name: y.ItemGroupCode + '-' + y.ItemGroupName,
        value: y.ItemGroupId
      };
    }));
  }

  subscribeAllStoreItemsByGroupId(response: any) {
    this.storeItems$ = of(response.data.map(y => {
      return {
        name: y.ItemCode + '-' + y.ItemName,
        value: y.ItemId
      };
    }));
  }

  subscribeAllEmployeesByOfficeId(response: any) {
    this.employeeList$ = of(response.data.map(y => {
      return {
        name: y.EmployeeCode + '-' + y.EmployeeName,
        value: y.EmployeeId
      };
      }));
  }

  subscribePurchaseListByItemId(response: any) {
    this.commonLoader.hideLoader();
    if (response.statusCode === 200) {
      const index = response.data.findIndex(x => x.PurchaseId === this.purchaseId);

      if (index !== -1) {
        this.originalQuantityOfPurchase = response.data[index].Quantity;
        this.procuredQuantityOfPurchase = this.originalProcuredQuantity = response.data[index].ItemsIssuedCount;
        this.itemsToBeProcuredCount = this.currentQuantityOfPurchase  = this.originalQuantityOfPurchase  - this.procuredQuantityOfPurchase;
        this.originalIssuedQuantity = this.addProcurementForm.get('IssuedQuantity').value;
         this.addProcurementForm.get('IssuedQuantity').setValidators(Validators.max(this.itemsToBeProcuredCount));
       }

      this.purchases$ = of(response.data.map(y => {
        return {
          name: y.PurchaseCode,
          value: y.PurchaseId
        };
      }));

    } else {
      this.toastr.warning(response.message);
    }
  }

  validateMaxProcurement() {
    const currentQuantity = this.addProcurementForm.get('IssuedQuantity').value;
    const itemsAvailable =  (this.originalIssuedQuantity - currentQuantity + this.itemsToBeProcuredCount);
    this.showMaxProcurementMessage = (itemsAvailable >= 0) ? false : true;
    this.addProcurementForm.get('IssuedQuantity').setValidators(Validators.max(currentQuantity + itemsAvailable));

    this.currentQuantityOfPurchase = itemsAvailable;
    this.procuredQuantityOfPurchase = (this.originalProcuredQuantity + (currentQuantity - this.originalIssuedQuantity));
  }

  getInventoriesByInventoryTypeId(inventoryTypeId: number) {
    return this.purchaseService
      .getInventoriesByInventoryTypeId(inventoryTypeId);
  }

  getMasterInventorySelectedValue(event: any) {
    this.getAllStoreItemGroups(event);
  }

  getAllStoreItemGroups(inventoryId: number) {
    return this.purchaseService
      .getItemGroupByInventoryId(inventoryId);
  }

  getItemGroupSelectedValue(event: any) {
    this.getAllStoreItemsByGroupId(event);
  }

  getAllStoreItemsByGroupId(groupId: number) {
    return this.purchaseService
      .getItemsByItemGroupId(groupId);
  }

  getPurchaseListByItemId(event: any) {
    return this.purchaseService
      .getPurchaseListByItemId(event);
  }

  getItemDetailByPurchaseId(ItemId: number) {
    if (ItemId !== 0) {
      this.commonLoader.showLoader();
      this.purchaseService
        .getItemDetailByPurchaseId(ItemId)
        .pipe(takeUntil(this.destroyed$))
        .subscribe(x => {

          if (x != null) {
            this.addProcurementForm.get('ItemId').patchValue(x.ItemId);
            this.addProcurementForm.get('InventoryTypeId').patchValue(x.InventoryTypeId);
            this.addProcurementForm.get('InventoryId').patchValue(x.InventoryId);
            this.addProcurementForm.get('ItemGroupId').patchValue(x.ItemGroupId);
            this.addProcurementForm.get('PurchaseId').patchValue(x.PurchaseId);

            forkJoin([
              this.getInventoriesByInventoryTypeId(x.InventoryTypeId),
              this.getAllStoreItemGroups(x.InventoryId),
              this.getAllStoreItemsByGroupId(x.ItemGroupId),
              this.getPurchaseListByItemId(x.ItemId)
            ])
              .pipe(takeUntil(this.destroyed$))
              .subscribe(result => {
                this.subscribeInventoriesByInventoryTypeId(result[0]);
                this.subscribeAllStoreItemGroups(result[1]);
                this.subscribeAllStoreItemsByGroupId(result[2]);
                this.subscribePurchaseListByItemId(result[3]);
              });
          }
        },
          error => {
            console.error(error);
          });
    }
  }

  saveProcurement() {
    if (this.addProcurementForm.valid) {
      if (this.addProcurementForm.value.ProcurementId) {
        this.editProcurement();
      } else {
        this.addProcurement();
      }
    } else {
      this.toastr.warning('Please correct errors in procurement form and submit again');
    }
  }

  editProcurement() {
    if (this.addProcurementForm.value.IssuedQuantity === 0) {
      this.toastr.warning('Issued Quantity should be greater than 0');
      return;
    }
      this.commonLoader.showLoader();
      this.purchaseService.editProcurement(this.addProcurementForm.getRawValue())
      .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        if (x.StatusCode === 200) {
          // this.addProcurementForm.get('ProcurementId').patchValue(x.data.ProcurementModel.ProcurementId);
          // this.addProcurementForm.get('EmployeeName').patchValue(x.data.ProcurementModel.EmployeeName);
          // this.addProcurementForm.get('EmployeeId').patchValue(x.data.ProcurementModel.EmployeeId);
          this.dialogRef.close(this.addProcurementForm.getRawValue());
          this.toastr.success(x.Message);
          this.commonLoader.hideLoader();
        } else if (x.StatusCode === 400) {
          this.toastr.warning(x.Message);
          this.commonLoader.hideLoader();
        }
      },
      error => {
        console.log(error);
        this.commonLoader.hideLoader();
        // console.log(error);
      });
  }

  addProcurement() {
    if (this.addProcurementForm.value.IssuedQuantity === 0) {
      this.toastr.warning('Issued Quantity should be greater than 0');
      return;
    }
      this.commonLoader.showLoader();
      this.purchaseService.addProcurement(this.addProcurementForm.getRawValue())
      .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        if (x.StatusCode === 200) {
          // this.addProcurementForm.get('ProcurementId').patchValue(x.data.ProcurementModel.ProcurementId);
          // this.addProcurementForm.get('EmployeeName').patchValue(x.data.ProcurementModel.EmployeeName);
          // this.addProcurementForm.get('EmployeeId').patchValue(x.data.ProcurementModel.EmployeeId);
          this.dialogRef.close(this.addProcurementForm.getRawValue());
          this.toastr.success(x.Message);
          this.commonLoader.hideLoader();
        } else if (x.StatusCode === 400) {
          this.toastr.warning(x.Message);
          this.commonLoader.hideLoader();
        }
      },
      error => {
        console.log(error);
        this.commonLoader.hideLoader();
        // console.log(error);
      });
  }

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close(false);
  }
  //#endregion

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }

}
