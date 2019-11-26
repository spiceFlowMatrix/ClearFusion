import { Component, OnInit, Input, EventEmitter, Output, OnChanges } from '@angular/core';
import { SubmitPurchaseListComponent } from '../submit-purchase-list/submit-purchase-list.component';
import { MatDialog } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { LogisticService } from '../logistic.service';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-purchase-order',
  templateUrl: './purchase-order.component.html',
  styleUrls: ['./purchase-order.component.scss']
})
export class PurchaseOrderComponent implements OnInit, OnChanges {

  @Input() requestStatus = 0;
  @Input() comparativeStatus = 1;
  @Input() totalCost = 0;
  @Input() requestedItems: any[];
  @Input() requestId;
  @Output() selectedItemChange = new EventEmitter();

  purchasedItemsHeaders$ = of(['Item', 'Quantity', 'Final Cost']);
  purchasedItemsData$ = of([]);
  selectedItems: any[];

  constructor(private dialog: MatDialog,
    private routeActive: ActivatedRoute,
    private logisticservice: LogisticService) { }

  ngOnInit() {
  }

  ngOnChanges() {
    if (this.requestStatus === 4) {
      this.getPurchasedItemsList();
    }
  }
  submitPurchase() {
    const dialogRef = this.dialog.open(SubmitPurchaseListComponent, {
      width: '600px',
      data: {requestedItems: this.requestedItems}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        this.selectedItems = result.data;
        this.purchasedItemsData$ = of(this.selectedItems).pipe(
          map(r => r.map(v => ({
            Item: v.Items,
            Quantity: v.Quantity,
            FinalCost: v.EstimatedCost,
           }) )));
        this.selectedItemChange.emit(this.selectedItems);
      }
    });
  }

  getPurchasedItemsList() {
    this.logisticservice.getPurchasedItemsList(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 && res.data.LogisticsItemList != null) {
        this.purchasedItemsData$ = of(res.data.LogisticsItemList).pipe(
          map(r => r.map(v => ({
            Item: v.Item,
            Quantity: v.Quantity,
            FinalCost: v.EstimatedCost,
           }) )));
      } else {
        // this.toastr.error('Something went wrong!');
      }
    });
  }

}
