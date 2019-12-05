import { Component, OnInit, Input, EventEmitter, Output, OnChanges } from '@angular/core';
import { of } from 'rxjs';
import { MatDialog, MatDialogRef } from '@angular/material';
import { SubmitPurchaseListComponent } from '../submit-purchase-list/submit-purchase-list.component';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { LogisticService } from '../logistic.service';

@Component({
  selector: 'app-request-status',
  templateUrl: './request-status.component.html',
  styleUrls: ['./request-status.component.scss']
})
export class RequestStatusComponent implements OnInit, OnChanges {
  selected_case = 3;
  purchasedItemsHeaders$ = of(['Item', 'Quantity', 'Final Cost']);
  purchasedItemsData$ = of([]);
  requestId;
  selectedItems: any[];

  @Input() requestStatus = 0;
  @Input() comparativeStatus = 1;
  @Input() totalCost = 0;
  @Input() requestedItems: any[];
  @Output() selectedItemChange = new EventEmitter();
  @Output() comparativeStatusChange = new EventEmitter();
  @Output() StatusChange = new EventEmitter();

  constructor(private dialog: MatDialog,
    private routeActive: ActivatedRoute,
    private logisticservice: LogisticService) { }

  ngOnInit() {
    this.routeActive.params.subscribe(params => {
      this.requestId = +params['id'];
    });
  }

  ngOnChanges() {

  }

  selectedPurchaseItemChange(value) {
    this.selectedItemChange.emit(value);
  }

  comparativeStatusEmit(value) {
    this.comparativeStatusChange.emit(value);
  }

  StatusEmit(value) {
    this.StatusChange.emit(value);
  }

}
