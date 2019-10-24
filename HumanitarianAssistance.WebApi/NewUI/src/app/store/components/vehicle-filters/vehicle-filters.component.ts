import { Component, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';
import { IDropDownModel } from 'src/app/dashboard/accounting/report-services/report-models';
import { PurchaseService } from '../../services/purchase.service';
import { of } from 'rxjs/internal/observable/of';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';

@Component({
  selector: 'app-vehicle-filters',
  templateUrl: './vehicle-filters.component.html',
  styleUrls: ['./vehicle-filters.component.scss']
})
export class VehicleFiltersComponent implements OnInit, OnDestroy {

  isBasic = true;
  vehicleTrackerFilterForm: FormGroup;
  @Output() applyFilterEvent = new EventEmitter<any>();

  employeeList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);


  constructor(private _fb: FormBuilder, private purchaseService: PurchaseService) {
    this.vehicleTrackerFilterForm = this._fb.group({
      'PlateNo': [null],
      'EmployeeId': [null],
      'OfficeId': [null],
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

  getOfficeSelectedValue(event: any) {
    this.getEmployeesByOfficeId(event);
  }

  getEmployeesByOfficeId(officeId: any) {
    this.purchaseService.getEmployeesByOfficeId(officeId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        this.employeeList$ = of(x.data.map(y => {
          return {
            name: y.CodeEmployeeName,
            value: y.EmployeeId
          };
        }));
      });
  }

  clearFilters() {

  }

  onApplyFilters() {

    this.applyFilterEvent.emit(this.vehicleTrackerFilterForm.value);
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
