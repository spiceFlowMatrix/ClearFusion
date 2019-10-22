import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';

@Component({
  selector: 'app-request-status',
  templateUrl: './request-status.component.html',
  styleUrls: ['./request-status.component.scss']
})
export class RequestStatusComponent implements OnInit {
  selected_case = 3;
  purchasedItemsHeaders$ = of(['Item', 'Quantity', 'Final Cost']);
  purchasedItemsData$ = of([{'Item': 'Item1', 'Quantity': '12', 'Final Cost': '2500'}]);
  constructor() { }

  ngOnInit() {
  }

}
