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
  @Input() subHeaders: Observable<string[]>;
  @Input() items: Observable<Array<Object>>;
  @Input() subTitle: string;
  subItemHeaders: Observable<string[]>;

  subItems: Array<Object> = [];
  itemHeaders: Observable<string[]>;

  isShowSubList = [];
  constructor() { }

  ngOnInit() {

  }
  ngOnChanges(): void {
    if (this.items) {
      this.items.subscribe(res => {
        this.subItems = [];
        if (res == null || res.length > 0) {
          res.forEach((element, i) => {

            this.subItems.push(element['subItems']);
            delete element['subItems'];
          });
          console.log(this.subItems);
          this.itemHeaders = of(Object.keys(res[0]));
          this.subItemHeaders = of(Object.keys(this.subItems[0][0]));
        }

      });
    }

  }

}
