import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';

@Component({
  selector: 'app-consolidate-gain-loss',
  templateUrl: './consolidate-gain-loss.component.html',
  styleUrls: ['./consolidate-gain-loss.component.scss']
})
export class ConsolidateGainLossComponent implements OnInit {
  transactionHeaders$ = of(['Account', 'Credit Amount', 'Debit Amount', 'Desctiption'])
  constructor() { }

  ngOnInit() {
  }

}
