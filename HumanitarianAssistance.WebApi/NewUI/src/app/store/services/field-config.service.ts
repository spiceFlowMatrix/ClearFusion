import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FieldConfigService {
  columnsToShow = []

  private dataSource = new BehaviorSubject<any>(this.columnsToShow);
  data = this.dataSource.asObservable();
  constructor() { }

  updateList(list){
    this.dataSource.next(list);
  }
}
