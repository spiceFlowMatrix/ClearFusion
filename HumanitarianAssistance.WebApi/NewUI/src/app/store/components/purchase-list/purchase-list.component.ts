import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { PurchaseService } from '../../services/purchase.service';
import { IFilterValueModel } from '../../models/purchase';

@Component({
  selector: 'app-purchase-list',
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.scss']
})
export class PurchaseListComponent implements OnInit {

  purchaseList$: Observable<any>;
  filterValueModel: IFilterValueModel;

  purchaseListHeaders$: Observable<any> = of(['Id', 'Item', 'Purchased By', 'Project', 'Original Cost', 'Deprecated Cost']);
  // values = of([
  //   { 'TestHeader': 'Test value', 'TestHeader1': 'Testvalue', 'TestHeader2': 'Test value', 'TestHeader3': 'Test value' },
  //   { 'TestHeader': 'Test value', 'TestHeader1': 'Testvalue', 'TestHeader2': 'Test value', 'TestHeader3': 'Test value' }
  // ]);

  constructor(private purchaseService: PurchaseService) {

    this.filterValueModel = {
      CurrencyId: 0,
      InventoryId: 1,
      InventoryTypeId: 0,
      IssueEndDate: null,
      IssueStartDate: null,
      ItemGroupId: 0,
      ItemId: 0,
      OfficeId: 0,
      ProjectId: 0,
      PurchaseEndDate: null,
      PurchaseStartDate: null,
      ReceiptTypeId: 0,
      pageIndex: null,
      pageSize: 0,
      totalCount: 0
    };
  }

  ngOnInit() {
    this.getPurchasesByFilter(this.filterValueModel);
  }

  getPurchasesByFilter(filter: IFilterValueModel) {
    const id = 0;
    this.purchaseService
        .GetFilteredPurchaseList(filter).subscribe(x => {
      debugger;
      this.purchaseList$ = x;
    });
  }

}
