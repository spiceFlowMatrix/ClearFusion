import { Component, OnInit, EventEmitter, Output, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IDropDownModel } from '../../models/purchase';
import { Observable } from 'rxjs/internal/Observable';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { PurchaseService } from '../../services/purchase.service';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { of } from 'rxjs/internal/observable/of';

@Component({
  selector: 'app-generator-filters',
  templateUrl: './generator-filters.component.html',
  styleUrls: ['./generator-filters.component.scss']
})
export class GeneratorFiltersComponent implements OnInit, OnDestroy {
  isBasic = true;

  generatorTrackerFilterForm: FormGroup;
  @Output() applyFilterEvent = new EventEmitter<any>();

  officeList$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private _fb: FormBuilder, private purchaseService: PurchaseService) {
    this.generatorTrackerFilterForm = this._fb.group({
      'Voltage': [null],
      'OfficeId': [null],
      'ModelYear': [null],
      'TotalCost': [null]
    });
  }

  ngOnInit() {
    this.getAllOffice();
  }

  getAllOffice() {
    this.purchaseService.getAllOfficeList()
    .pipe(takeUntil(this.destroyed$))
      .subscribe(response => {
        this.officeList$ = of(response.data.OfficeDetailsList.map(y => {
          return {
            value: y.OfficeId,
            name: y.OfficeCode + '-' + y.OfficeName
          };
        }));
      });
  }

  onApplyFilters() {
    this.applyFilterEvent.emit(this.generatorTrackerFilterForm.value);
  }

  clearFilters() { }


  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
