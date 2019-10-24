import { Component, OnInit, OnDestroy, Output, EventEmitter } from '@angular/core';
import { PurchaseService } from '../../services/purchase.service';
import { IDropDownModel } from '../../models/purchase';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { ReplaySubject } from 'rxjs';
import { BudgetLineService } from 'src/app/dashboard/project-management/project-list/budgetlines/budget-line.service';
import { takeUntil } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-purchase-filters',
  templateUrl: './purchase-filters.component.html',
  styleUrls: ['./purchase-filters.component.scss']
})
export class PurchaseFiltersComponent implements OnInit, OnDestroy {

 // purchaseFilterDropdown: IPurchaseFilter;

  inventoryType$: Observable<IDropDownModel[]>;
  offices$: Observable<IDropDownModel[]>;
  receiptType$: Observable<IDropDownModel[]>;
  currencies$: Observable<IDropDownModel[]>;
  projects$: Observable<IDropDownModel[]>;
  storeInventory$: Observable<IDropDownModel[]>;
  storeItemGroups$: Observable<IDropDownModel[]>;
  storeItems$: Observable<IDropDownModel[]>;
  projectJobs$: Observable<IDropDownModel[]>;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  purchaseFormFilters: FormGroup;
  isBasic = true;
  @Output() purchaseFilterSelected = new EventEmitter<FormGroup>();

  constructor(private purchaseService: PurchaseService,
    private fb: FormBuilder, private budgetLineService: BudgetLineService, private loader : CommonLoaderService) {

    this.purchaseFormFilters = this.fb.group({
      InventoryTypeId: [null, Validators.required],
      ReceiptTypeId: [null, Validators.required],
      OfficeId: [null, Validators.required],
      CurrencyId: [null, Validators.required],
      ProjectId: [null],
      JobId: [null],
      DateOfPurchase: [null],
      DateOfIssue: [null],
      InventoryMasterId: [null],
      ItemGroupId: [null],
      ItemId: [null]
    });
  }

  ngOnInit() {
    this.getPurchaseFilters();
  }

  getPurchaseFilters() {
    this.loader.showLoader();
    this.purchaseService.getPurchaseFilterList()
    .pipe(takeUntil(this.destroyed$))
    .subscribe(x  => {

      this.inventoryType$ = of(x.InventoryTypes.map(y => {
        return {
          value: y.Id,
          name: y.InventoryName
        };
      }));

      this.offices$ = of(x.Offices.map(y => {
        return {
          value: y.OfficeId,
          name: y.OfficeCode + '-' + y.OfficeName
        };
      }));

      this.currencies$ = of(x.CurrencyModel.map(y => {
        return {
          value: y.CurrencyId,
          name: y.CurrencyCode + '-' + y.CurrencyName
        };
      }));

      this.projects$ = of(x.ProjectModel.map(y => {
        return {
          value: y.ProjectId,
          name: y.ProjectCode + '-' + y.ProjectName
        };
      }));

      this.receiptType$ = of(x.ReceiptTypes.map(y => {
        return {
          value: y.ReceiptTypeId,
          name:  y.ReceiptTypeName
        };
      }));

      this.storeInventory$ = of(x.StoreInventoryModel.map(y => {
        return {
          value: y.InventoryId,
          name:  y.InventoryCode + '-' + y.InventoryName
        };
      }));

      // Set defaults for filter
      this.purchaseFormFilters.get('InventoryTypeId').patchValue(x.InventoryTypes !== null ? x.InventoryTypes[0].Id : null);
      this.purchaseFormFilters.get('ReceiptTypeId').patchValue(x.ReceiptTypes !== null ? x.ReceiptTypes[0].ReceiptTypeId : null);
      this.purchaseFormFilters.get('OfficeId').patchValue(x.Offices !== null ? x.Offices[0].OfficeId : null);
      this.purchaseFormFilters.get('CurrencyId').patchValue(x.CurrencyModel !== null ? x.CurrencyModel[0].CurrencyId : null);

      this.onPurchaseFilterSelectionChanged();
      this.loader.hideLoader();
    },
    err => {
      this.loader.hideLoader();
     console.error(err);
    });
  }

  getInventoryTypeSelectedValue(event: any) {
    this.getInventoriesByInventoryTypeId(event);
    this.onPurchaseFilterSelectionChanged();
  }

  getReceiptTypeSelectedValue(event: any) {
     this.onPurchaseFilterSelectionChanged();
   }

  getOfficeSelectedValue(event: any) {
     this.onPurchaseFilterSelectionChanged();
   }

  getCurrenciesSelectedValue(event: any) {
     this.onPurchaseFilterSelectionChanged();
   }

  getProjectSelectedValue(event: any) {
    this.onPurchaseFilterSelectionChanged();
    this.getJobsByProjectId(event);
  }

   getJobSelectedValue(event: any) {
     this.onPurchaseFilterSelectionChanged();
   }

  getMasterInventorySelectedValue(event: any) {
    this.purchaseFormFilters.get('ItemId').patchValue(null);
    this.getAllStoreItemGroups(event);
    this.onPurchaseFilterSelectionChanged();
  }

  getItemGroupSelectedValue(event: any) {
    this.getAllStoreItemsByGroupId(event);
    this.onPurchaseFilterSelectionChanged();
  }

   getItemSelectedValue(event: any) {
     this.onPurchaseFilterSelectionChanged();
   }

  getJobsByProjectId(projectId: number) {
    this.budgetLineService
        .GetProjectJobList(projectId)
        .pipe(takeUntil(this.destroyed$))
        .subscribe(x => {
          this.projectJobs$ = of(x.data.map(y => {
            return {
              value: y.ProjectJobId,
              name: y.ProjectJobCode + '-' + y.ProjectJobName
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

  onPurchaseFilterSelectionChanged() {
    if (this.purchaseFormFilters.valid) {
      this.purchaseFilterSelected.emit(this.purchaseFormFilters);
    }
  }

  clearFilters() {
    this.purchaseFormFilters.reset();
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
