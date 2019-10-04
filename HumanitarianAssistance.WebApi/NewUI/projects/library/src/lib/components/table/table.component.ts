import { Component, OnInit, Input, OnChanges, ChangeDetectionStrategy, EventEmitter, Output } from '@angular/core';
import { Observable, of } from 'rxjs';
import { IDeleteProcurementModel } from 'src/app/store/models/purchase';
import { TableActionsModel } from '../../models/table-actions-model';

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
  @Input() actions: TableActionsModel
  @Output() actionClick = new EventEmitter<any>();
  @Output() deleteClick = new EventEmitter<any>();
  subItemHeaders: Observable<string[]>;

  mainItems: Observable<Array<Object>>;
  subItems: Array<Object> = [];
  itemHeaders: Observable<string[]>;


  isShowSubList = [];
  constructor() { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: true, text: 'Add Procurement' },
        delete: false,
        edit: false,
        download: false

      },
      subitems: {
        button: { status: false, text: 'Add Procurement' },
        delete: true,
        edit: false,
        download: true
      }
    }
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
  action(item, type) {
    const model: any = {
      item: item,
      type: type
    };
    this.actionClick.emit(model);
  }

  subAction(subItemEvent, itemEvent, type) {
    const model: any = {
      subItem: subItemEvent,
      item: itemEvent,
      type: type
    };

    this.deleteClick.emit(model);
  }

}
