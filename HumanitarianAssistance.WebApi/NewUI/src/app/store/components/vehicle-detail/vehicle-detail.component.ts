import { Component, OnInit, Input, OnChanges, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDropDownModel } from '../../models/purchase';
import { Observable } from 'rxjs/internal/Observable';
import { PurchaseService } from '../../services/purchase.service';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { of } from 'rxjs/internal/observable/of';
import { ActivatedRoute } from '@angular/router';

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
  vehicleId: number;

  constructor(private purchaseService: PurchaseService, private activatedRoute: ActivatedRoute,
              private fb: FormBuilder) {
    this.activatedRoute.params.subscribe(params => {
      this.vehicleId = params['id'];
    });
  }

  ngOnInit() {
    this.getAllOffice();



    this.vehicleDetailForm.controls['OfficeId'].valueChanges.subscribe(x => {
      this.getEmployeesByOfficeId(x);
    });
    this.markFormGroupTouched(this.vehicleDetailForm);
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

  onSubmit() {
    // to validate child vehicle form from add purchase form
    this.markFormGroupTouched(this.vehicleDetailForm);
    this.vehicleDetailForm.updateValueAndValidity();
  }

  public markFormGroupTouched(formGroup: FormGroup) {
    (<any>Object).values(formGroup.controls).forEach(control => {
      control.markAsTouched();

      if (control.controls) {
        this.markFormGroupTouched(control);
      }
    });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
