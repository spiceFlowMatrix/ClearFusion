import { Component, OnInit, OnDestroy } from '@angular/core';
import { PurchaseService } from '../../services/purchase.service';
import { IDropDownModel } from '../../models/purchase';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { Subject } from 'rxjs';
import { BudgetLineService } from 'src/app/dashboard/project-management/project-list/budgetlines/budget-line.service';

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

  //destroy$: Subject<boolean> = new Subject<boolean>();
  purchaseFormFilters: FormGroup;

  constructor(private purchaseService: PurchaseService,
    private fb: FormBuilder, private budgetLineService: BudgetLineService) {

    this.purchaseFormFilters = this.fb.group({
      InventoryTypeId: [null],
      ReceiptTypeId: [null],
      OfficeId: [null],
      CurrencyId: [null],
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
    this.purchaseService.GetPurchaseFilterList()
    //.takeUntil(this.destroy$)
    .subscribe(x  => {
      debugger;
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
    this.GetInventoriesByInventoryTypeId(event);
  }

  // getReceiptTypeSelectedValue(event: any) {
  //   this.purchaseFormFilters.get('ReceiptTypeId').patchValue(event);
  // }

  // getOfficeSelectedValue(event: any) {
  //   this.purchaseFormFilters.get('OfficeId').patchValue(event);
  // }

  // getCurrenciesSelectedValue(event: any) {
  //   this.purchaseFormFilters.get('CurrencyId').patchValue(event);
  // }

  getProjectSelectedValue(event: any) {
    this.purchaseFormFilters.get('ProjectId').patchValue(event);
    this.getJobsByProjectId(event);
  }

  // getJobSelectedValue(event: any) {
  //   this.purchaseFormFilters.get('JobId').patchValue(event);
  // }

  getMasterInventorySelectedValue(event: any) {
    this.purchaseFormFilters.get('ItemId').patchValue(null);
    this.GetAllStoreItemGroups(event);
  }

  getItemGroupSelectedValue(event: any) {
    this.purchaseFormFilters.get('ItemGroupId').patchValue(event);
    this.GetAllStoreItemsByGroupId(event);
  }

  // getItemSelectedValue(event: any) {
  //   this.purchaseFormFilters.get('ItemId').patchValue(event);
  // }

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

  GetInventoriesByInventoryTypeId(inventoryTypeId: number) {
    this.purchaseService
        .GetInventoriesByInventoryTypeId(inventoryTypeId)
        .subscribe(x => {
          debugger;
          this.storeInventory$ = of(x.data.map(y => {
            return {
              name: y.InventoryCode + '-' + y.InventoryName,
              value: y.InventoryId
            };
          }));
        });
  }

  GetAllStoreItemGroups(inventoryId: number) {
    this.purchaseService
        .GetItemGroupByInventoryId(inventoryId)
        .subscribe(x => {
          debugger;
          this.storeItemGroups$ = of(x.data.map(y => {
            return {
              name: y.ItemGroupCode + '-' + y.ItemGroupName,
              value: y.ItemGroupId
            };
          }));
        });
  }

  GetAllStoreItemsByGroupId(groupId: number) {
    this.purchaseService
        .GetItemsByItemGroupId(groupId)
        .subscribe(x => {
          debugger;
          this.storeItems$ = of(x.data.map(y => {
            return {
              name: y.ItemCode + '-' + y.ItemName,
              value: y.ItemId
            };
          }));
        });
  }

  ngOnDestroy() {
    // this.destroy$.next(true);
    // // unsubscribe from the subject itself:
    // this.destroy$.unsubscribe();
  }
}
