import { Component, OnInit, OnDestroy, Output, EventEmitter } from '@angular/core';
import { PurchaseService } from '../../services/purchase.service';
import { IDropDownModel } from '../../models/purchase';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { ReplaySubject } from 'rxjs';
import { BudgetLineService } from 'src/app/dashboard/project-management/project-list/budgetlines/budget-line.service';
import { takeUntil } from 'rxjs/operators';

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
    private fb: FormBuilder, private budgetLineService: BudgetLineService) {

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

    this.purchaseFormFilters.controls['ReceiptTypeId'].valueChanges.subscribe(x => { console.log(x) });

  }

  ngOnInit() {
    this.getPurchaseFilters();
  }

  getPurchaseFilters() {
    this.purchaseService.GetPurchaseFilterList()
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

    },
    err => {
     console.error(err);
    });
  }

  getInventoryTypeSelectedValue(event: any) {
    this.purchaseFormFilters.get('InventoryTypeId').patchValue(event);
    this.getInventoriesByInventoryTypeId(event);
    this.onPurchaseFilterSelectionChanged();
  }

  getReceiptTypeSelectedValue(event: any) {
    this.purchaseFormFilters.get('ReceiptTypeId').patchValue(event);
     this.onPurchaseFilterSelectionChanged();
   }

  getOfficeSelectedValue(event: any) {
    this.purchaseFormFilters.get('OfficeId').patchValue(event);
     this.onPurchaseFilterSelectionChanged();
   }

  getCurrenciesSelectedValue(event: any) {
    this.purchaseFormFilters.get('CurrencyId').patchValue(event);
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
        .GetInventoriesByInventoryTypeId(inventoryTypeId)
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
        .GetItemGroupByInventoryId(inventoryId)
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
        .GetItemsByItemGroupId(groupId)
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

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
