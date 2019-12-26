import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-tender-statement',
  templateUrl: './tender-statement.component.html',
  styleUrls: ['./tender-statement.component.scss']
})
export class TenderStatementComponent implements OnInit {

  @Input() requestStatus = 0;
  @Input() totalCost = 0;
  @Input() tenderStatus = 1;
  constructor() { }

  ngOnInit() {
  }

}
