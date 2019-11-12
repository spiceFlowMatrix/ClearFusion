import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-comparative-statement',
  templateUrl: './comparative-statement.component.html',
  styleUrls: ['./comparative-statement.component.scss']
})
export class ComparativeStatementComponent implements OnInit {

  @Input() requestStatus = 0;
  @Input() comparativeStatus = 1;
  @Input() totalCost = 0;
  constructor() { }

  ngOnInit() {
  }

}
