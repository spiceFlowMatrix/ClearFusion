import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { of } from 'rxjs';
import { MatDialog, MatDialogRef } from '@angular/material';
import { SubmitPurchaseListComponent } from '../submit-purchase-list/submit-purchase-list.component';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-request-status',
  templateUrl: './request-status.component.html',
  styleUrls: ['./request-status.component.scss']
})
export class RequestStatusComponent implements OnInit {
  selected_case = 3;
  purchasedItemsHeaders$ = of(['Item', 'Quantity', 'Final Cost']);
  purchasedItemsData$ = of([]);
  requestId;
  selectedItems: any[];

  @Input() requestStatus = 0;
  @Input() totalCost = 0;
  @Input() requestedItems: any[];
  @Output() selectedItemChange = new EventEmitter();

  constructor(private dialog: MatDialog,
    private routeActive: ActivatedRoute) { }

  ngOnInit() {
    this.routeActive.params.subscribe(params => {
      this.requestId = +params['id'];
    });
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
}
