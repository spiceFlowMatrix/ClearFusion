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
  @Output() subActionClick = new EventEmitter<any>();
  subItemHeaders: Observable<string[]>;

  mainItems: Observable<Array<Object>>;
  subItems: Array<Object> = [];
  itemHeaders: Observable<string[]>;
  itemActions: TableActionsModel;

  isShowSubList = [];
  constructor() {
    this.actions = {
      items: {
      },
      subitems: {
      }
    }
  }

  ngOnInit() {

    console.log(this.actions)
  }
  ngOnChanges(): void {
    this.itemActions = this.actions
    console.log(this.itemActions)
    if (this.items) {
      this.items.subscribe(res => {
        this.subItems = [];
        if (res == null || res.length > 0) {
          this.itemHeaders = of(Object.keys(res[0]));
          res.forEach((element, i) => {
            if (element['subItems']) {
              this.subItems.push(element['subItems']);
              if (element['subItems'].length > 0) { 
                this.subItemHeaders = of(Object.keys(element['subItems'][0]));
              }
            }

          });
          if (this.subItems.length > 0) {
          
            this.itemHeaders.subscribe(r => {
              const index = r.findIndex(v => v === 'subItems');
              r.splice(index);
            });
          }
        }

      });
    }

  }
  action(item, type, e: Event) {
    e.stopPropagation();
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

    this.subActionClick.emit(model);
  }

  switchSubList(i) {
    this.isShowSubList[i] = !this.isShowSubList[i];
  }

}
