import { Component, OnInit, Input, OnChanges, ChangeDetectionStrategy, EventEmitter, Output } from '@angular/core';
import { Observable, of } from 'rxjs';
import { IDeleteProcurementModel } from 'src/app/store/models/purchase';

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
  @Output() actionClick = new EventEmitter<any>();
  @Output() deleteClick = new EventEmitter<any>();
  subItemHeaders: Observable<string[]>;

  mainItems: Observable<Array<Object>>;
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
          });
          this.itemHeaders = of(Object.keys(res[0]));
          this.itemHeaders.subscribe(r => {
            const index = r.findIndex(v => v === 'subItems');
            r.splice(index);
          });
          this.subItemHeaders = of(Object.keys(this.subItems[0][0]));
        }

      });
    }

  }
  action(event) {
    this.actionClick.emit(event);
  }

  deleteClicked(subItemEvent, itemEvent) {
    const model: any = {
      subItem: subItemEvent,
      item: itemEvent
    };

    this.deleteClick.emit(model);
  }

}
