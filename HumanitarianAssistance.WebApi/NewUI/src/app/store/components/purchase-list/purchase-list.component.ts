import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';

@Component({
  selector: 'app-purchase-list',
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.scss']
})
export class PurchaseListComponent implements OnInit {
  headers = of(['Test Header', 'Test Header1', 'Test Header2', 'Test Header3']);
  values = of([
    { 'TestHeader': 'Test value', 'TestHeader1': 'Testvalue', 'TestHeader2': 'Test value', 'TestHeader3': 'Test value' },
    { 'TestHeader': 'Test value', 'TestHeader1': 'Testvalue', 'TestHeader2': 'Test value', 'TestHeader3': 'Test value' }
  ]);

  constructor() { }

  ngOnInit() {
  }

}
