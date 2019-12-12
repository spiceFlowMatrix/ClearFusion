import { Component, OnInit, Input, OnDestroy, OnChanges } from '@angular/core';
import { IDropDownModel } from '../../models/purchase';
import { Observable } from 'rxjs/internal/Observable';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { FormGroup } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { of } from 'rxjs/internal/observable/of';

@Component({
  selector: 'app-generator-detail',
  templateUrl: './generator-detail.component.html',
  styleUrls: ['./generator-detail.component.scss']
})
export class GeneratorDetailComponent implements OnInit, OnChanges, OnDestroy {

  offices$: Observable<IDropDownModel[]>;
  employeeList$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  @Input() officeId: number;
  @Input() generatorDetailForm: FormGroup;

  constructor(private purchaseService: PurchaseService) {
  }

  ngOnInit() {
    this.getAllOffice();
    // console.log(this.generatorDetailForm);
    this.generatorDetailForm.controls['OfficeId'].valueChanges.subscribe(x => {
      this.getEmployeesByOfficeId(x);
    });
    this.markFormGroupTouched(this.generatorDetailForm);
    this.markFormGroupTouched(this.generatorDetailForm);
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

  public markFormGroupTouched(formGroup: FormGroup) {
    (<any>Object).values(formGroup.controls).forEach(control => {
      control.markAsTouched();

      if (control.controls) {
        this.markFormGroupTouched(control);
      }
    });
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

  ngOnChanges() {
    if (this.officeId !== undefined && this.officeId != null) {
      this.getEmployeesByOfficeId(this.officeId);
    }
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }

}
