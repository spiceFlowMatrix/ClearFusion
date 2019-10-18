import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-voucher-summary',
  templateUrl: './voucher-summary.component.html',
  styleUrls: ['./voucher-summary.component.scss']
})
export class VoucherSummaryComponent implements OnInit {

  @Input() currentCredit: number;
  @Input() currentDebit: number;

  constructor() { }

  ngOnInit() {
  }

}
