import { Component, OnInit, Input } from '@angular/core';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'hum-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  @Input() headers: Observable<string[]>;
  @Input() items: Observable<Array<Object>>;
  itemHeaders: Observable<string[]>;
  constructor() { }

  ngOnInit() {
    this.items.subscribe(res => {
      if (res) {
        this.itemHeaders = of(Object.keys(res[0]));
      }

    });
  }

}
