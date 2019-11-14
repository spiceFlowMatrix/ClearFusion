import { Component, OnInit, Input } from '@angular/core';
import { of } from 'rxjs';

@Component({
  selector: 'app-comparative-statement',
  templateUrl: './comparative-statement.component.html',
  styleUrls: ['./comparative-statement.component.scss']
})
export class ComparativeStatementComponent implements OnInit {

  @Input() requestStatus = 0;
  @Input() comparativeStatus = 1;
  @Input() totalCost = 0;

  supplierHeaders$ = of(['Id', 'Supplier', 'Quantity', 'Final Price']);
  supplierSubHeaders$ = of(['']);
  supplierData$ = of([{'Supplier': 'abc', 'Quantity': 'abc', 'FinalPrice': 'abc', 'subItems' : [{'abc': 'bc'}]}]);
  hideSupplierColums;
  constructor() { }

  ngOnInit() {
    this.hideSupplierColums = of({
      headers: ['Supplier', 'Quantity', 'Final Price'],
      items: ['Supplier', 'Quantity', 'FinalPrice']
    });
  }

}
