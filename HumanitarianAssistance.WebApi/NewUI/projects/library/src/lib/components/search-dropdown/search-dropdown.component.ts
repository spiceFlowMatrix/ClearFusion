import { Component, OnInit, Input, ViewChild, Output, EventEmitter, OnChanges, ChangeDetectionStrategy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { Subject } from 'rxjs/internal/Subject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { IDataSource, IOpenedChange } from './search-dropdown.model';
import { MatOption } from '@angular/material/core';

@Component({
  selector: 'lib-search-dropdown',
  templateUrl: './search-dropdown.component.html',
  styleUrls: ['./search-dropdown.component.css']
})
export class SearchDropdownComponent implements OnInit, OnChanges {

  @ViewChild('allSelected') private allSelected: MatOption;

  @Input() dataSource: IDataSource[] = [];
  @Input() placeholder = '';
  @Input() placeholderSearchLabel = 'Search ...';
  @Input() multiSelect: boolean;
  @Input() selectedValue: number[];
  @Input() formControlName = new FormControl();

  @Output() openedChange = new EventEmitter<IOpenedChange>();
  @Output() selectionChanged = new EventEmitter<number[]>();

  public dataSourceFilterCtrl: FormControl = new FormControl();
  public filteredDataSource: ReplaySubject<IDataSource[]> = new ReplaySubject<IDataSource[]>(1);
  protected _onDestroy = new Subject<void>();

  constructor() {

  }

  ngOnInit() {

  }

  ngOnChanges() {
    // NOTE: load the initial Account list
    this.dataSource !== undefined ? this.filteredDataSource.next(this.dataSource.slice()) : null;

    // listen for search field value changes
    this.dataSourceFilterCtrl.valueChanges
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        this.filter();
      });
  }

  //#region "onOpenedChange"
  onOpenedChange(event: any) {
    this.openedChange.emit(
      {
        Flag: event,
        Value: this.multiSelect ? this.selectedValue.filter(x => x !== 0) : this.selectedValue
      }
    );

  }
  //#endregion

  //#region "onSelectionChanged"
  onSelectionChanged() {
      this.selectionChanged.emit(this.selectedValue);
  }
  //#endregion


  //#region "onTossle"
  onTossle(all) {

    if (this.multiSelect) {
      if (this.allSelected.selected) {
        this.allSelected.deselect();
        return false;
      }
      if (this.selectedValue.length === this.dataSource.length) {
        this.allSelected.select();
      }
    } else {
      return;
    }

  }
  //#endregion

  //#region "selectAll"
  selectAll() {
    if (this.allSelected.selected) {
      this.selectedValue = [...this.dataSource.map(item => item.Id), 0];
    } else {
      this.selectedValue = [];
    }
  }
  //#endregion

  //#region "FILTER: filter"
  protected filter() {
    if (!this.dataSource) {
      return;
    }
    // get the search keyword
    let search = this.dataSourceFilterCtrl.value;
    if (!search) {
      this.filteredDataSource.next(this.dataSource.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter
    this.filteredDataSource.next(
      this.dataSource.filter(
        acc => acc.Name.toLowerCase().indexOf(search) > -1
      )
    );
  }
  //#endregion

}
