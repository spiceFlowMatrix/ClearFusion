import { Component, OnInit } from '@angular/core';
import { PurchaseService } from '../../services/purchase.service';
import { IPurchaseFilter, IDropDownModel } from '../../models/purchase';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';

@Component({
  selector: 'app-purchase-filters',
  templateUrl: './purchase-filters.component.html',
  styleUrls: ['./purchase-filters.component.scss']
})
export class PurchaseFiltersComponent implements OnInit {

 // purchaseFilterDropdown: IPurchaseFilter;

  inventoryType$: Observable<IDropDownModel[]>;
  offices$: Observable<IDropDownModel[]>;
  receiptType$: Observable<IDropDownModel[]>;
  currencies$: Observable<IDropDownModel[]>;
  projects$: Observable<IDropDownModel[]>;
  storeInventory$: Observable<IDropDownModel[]>;


  purchaseFormFilters: FormGroup;

  constructor(private purchase: PurchaseService, private fb: FormBuilder) {

    this.purchaseFormFilters = this.fb.group({
      InventoryType: [null],
      ReceiptType: [null],
      OfficeId: [null],
      CurrencyId: [null],
      ProjectId: [null],
      JobId: [null],
      DateOfPurchase: [null],
      DateOfIssue: [null],
      InventoryMaster: [null],
      ItemGroup: [null],
      Item: [null]
    });

  }

  ngOnInit() {
    this.getPurchaseFilters();
  }

  getPurchaseFilters() {
    this.purchase.GetPurchaseFilterList().subscribe(x  => {
      debugger;
      this.inventoryType$ = of(x.InventoryType.map(y => {
        return {
          Value: y.Id,
          Name: y.InventoryName
        };
      }));

      this.offices$ = of(x.Offices.map(y => {
        return {
          Value: y.OfficeId,
          Name: y.OfficeCode + '-' + y.OfficeName
        };
      }));

      this.currencies$ = of(x.CurrencyModel.map(y => {
        return {
          Value: y.CurrencyId,
          Name: y.CurrencyCode + '-' + y.CurrencyName
        };
      }));

      this.projects$ = of(x.ProjectModel.map(y => {
        return {
          Value: y.ProjectId,
          Name: y.ProjectCode + '-' + y.ProjectName
        };
      }));

      this.receiptType$ = of(x.ReceiptType.map(y => {
        return {
          Value: y.ReceiptTypeId,
          Name:  y.ReceiptTypeName
        };
      }));

      this.storeInventory$ = of(x.ReceiptType.map(y => {
        return {
          Value: y.InventoryId,
          Name:  y.InventoryCode + '-' + y.InventoryName
        };
      }));

    },
    err => {
      debugger;
    });

  }
}
