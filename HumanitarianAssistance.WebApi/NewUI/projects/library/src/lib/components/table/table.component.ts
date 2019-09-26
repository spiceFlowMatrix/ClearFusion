import { Component, OnInit, Input, OnChanges, ChangeDetectionStrategy } from '@angular/core';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'hum-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TableComponent implements OnInit, OnChanges {

  @Input() headers: Observable<string[]>;
  @Input() items: Observable<Array<Object>>;
  subItems:Observable<Array<Object>>
  itemHeaders: Observable<string[]>;
  constructor() { }

  ngOnInit() {

  }
  ngOnChanges(): void {
    if (this.items) {
      this.items.subscribe(res => {
        if (res == null || res.length > 0) {
          this.itemHeaders = of(Object.keys(res[0]));
        }

      });
    }

  }

}
