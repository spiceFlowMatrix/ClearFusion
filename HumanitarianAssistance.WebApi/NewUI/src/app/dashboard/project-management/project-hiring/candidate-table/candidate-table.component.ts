import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Observable, of } from 'rxjs';
import { TableActionsModel } from '../models/hiring-requests-models';

@Component({
  selector: 'app-candidate-table',
  templateUrl: './candidate-table.component.html',
  styleUrls: ['./candidate-table.component.scss']
})
export class CandidateTableComponent implements OnInit {
  @Input() headers: Observable<string[]>;
  @Input() subHeaders: Observable<string[]>;
  @Input() items: Observable<Array<Object>>;
  @Input() subTitle: string;
  @Input() actions: TableActionsModel;
  @Input() isDefaultAction = true;
  @Input() hideColums$: Observable<{ headers: string[], items: string[] }>

  @Output() actionClick = new EventEmitter<any>();
  @Output() subActionClick = new EventEmitter<any>();
  @Output() rowClick = new EventEmitter<any>();
  subItemHeaders: Observable<string[]>;

  mainItems: Observable<Array<Object>>;
  subItems: Array<Object> = [];
  itemAction: Array<Object> = [];
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
  }
  ngOnChanges(): void {
    this.itemActions = this.actions;
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
            // only if default action is false
            if (element['itemAction']) {
              this.itemAction.push(element['itemAction']);
            }

          });

          if (this.subItems.length > 0) {

            this.itemHeaders.subscribe(r => {
              const index = r.findIndex(v => v === 'subItems');
              r.splice(index);
            });
          }
          // only if default action is false

          if (this.itemAction.length > 0) {
            this.itemHeaders.subscribe(r => {
              const index = r.findIndex(v => v === 'itemAction');
              r.splice(index);
            });
          }
        }

      });
    }
    if (this.hideColums$ && this.itemHeaders) {
      this.hideColums$.subscribe(res => {
        this.itemHeaders.subscribe(headers => {
          this.itemHeaders = of(headers.filter(r => res.items.includes(r)));
        })
        this.headers.subscribe(headers => {
          this.headers = of(res.headers);

        })
      })
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

  ngAfterViewInit() {
    if (this.hideColums$ && this.itemHeaders) {
      this.hideColums$.subscribe(res => {
        this.itemHeaders.subscribe(headers => {
          this.itemHeaders = of(headers.filter(r => res.items.includes(r)));
        })
        this.headers.subscribe(headers => {
          this.headers = of(res.headers);

        })
      })
    }

  }

  switchSubList(i, event) {
    if (this.subItems.length > 0) this.isShowSubList[i] = !this.isShowSubList[i];
    this.rowClick.emit(event);

  }
}
