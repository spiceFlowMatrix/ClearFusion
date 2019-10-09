import { Component, OnInit, Input, OnChanges, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDropDownModel } from '../../models/purchase';
import { Observable } from 'rxjs/internal/Observable';
import { PurchaseService } from '../../services/purchase.service';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { of } from 'rxjs/internal/observable/of';

@Component({
  selector: 'app-vehicle-detail',
  templateUrl: './vehicle-detail.component.html',
  styleUrls: ['./vehicle-detail.component.scss']
})
export class VehicleDetailComponent implements OnInit, OnChanges, OnDestroy {

  employeeList$: Observable<IDropDownModel[]>;
  offices$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  @Input() officeId: number;
  @Input() vehicleDetailForm: FormGroup;

  constructor(private purchaseService: PurchaseService) {

  }

  ngOnInit() {
    this.getAllOffice();
  }

  ngOnChanges() {
    if (this.officeId !== undefined && this.officeId != null) {
      this.getEmployeesByOfficeId(this.officeId);
    }
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

  getAllOffice() {
    this.purchaseService.getAllOfficeList()
    .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        this.offices$ = of(x.data.OfficeDetailsList.map(y => {
          return {
            value: y.OfficeId,
            name: y.OfficeCode + '-' + y.OfficeName
          };
        }));
      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
