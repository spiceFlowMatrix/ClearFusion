import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { PurchaseService } from '../../services/purchase.service';
import { IFilterValueModel, IPurchaseList } from '../../models/purchase';

@Component({
  selector: 'app-purchase-list',
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.scss']
})
export class PurchaseListComponent implements OnInit {

  purchaseList$: Observable<IPurchaseList[]>;
  filterValueModel: IFilterValueModel;

  purchaseListHeaders$: Observable<any> = of(['Id', 'Item', 'Purchased By', 'Project', 'Original Cost', 'Deprecated Cost']);

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
  }

  getPurchasesByFilter(filter: IFilterValueModel) {
    debugger;
    this.purchaseService
      .GetFilteredPurchaseList(filter).subscribe(x => {
        debugger;
      this.purchaseList$ = of(x.map((element) => {
        return  {

          Id: element.PurchaseId,
          Item: element.ItemName,
          PurchasedBy: element.EmployeeName,
          Project: element.ProjectName,
          OriginalCost: element.OriginalCost,
          DepreciatedCost: element.DepreciatedCost
        };
      }));
    });
  }

  onpurchaseFilterSelected(event: any) {
    debugger;
    this.filterValueModel = event.value;
    this.getPurchasesByFilter(this.filterValueModel);
  }

}
