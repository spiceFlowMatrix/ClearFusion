import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PurchaseService } from '../../services/purchase.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-edit-vehicle',
  templateUrl: './edit-vehicle.component.html',
  styleUrls: ['./edit-vehicle.component.scss']
})
export class EditVehicleComponent implements OnInit, OnDestroy {

  vehicleId: number;
  vehicleDetailForm: FormGroup;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private router: Router, private purchaseService: PurchaseService,
    private activatedRoute: ActivatedRoute, private fb: FormBuilder, private commonLoader: CommonLoaderService) {
    this.activatedRoute.params.subscribe(params => {
      this.vehicleId = params['id'];
    });
  }

  ngOnInit() {
    this.vehicleDetailForm = this.fb.group({
      'VehicleId': [null],
      'PlateNo': [null, [Validators.required]],
      'EmployeeId': [null, [Validators.required]],
      'StartingMileage': [null],
      'IncurredMileage': [null],
      'MobilOilConsumptionRate': [null],
      'ModelYear': [null, [Validators.required]],
      'OfficeId': [null, [Validators.required]],
      'FuelConsumptionRate': [null],
      'ManufacturerCountry': ['', [Validators.required]],
      'EngineNo': ['', [Validators.required]],
      'RegistrationNo': ['', [Validators.required]],
      'ChasisNo': ['', [Validators.required]],
      'Remarks': [''],
    });

    this.getVehicleDetailById();
  }

  getVehicleDetailById() {
    this.purchaseService.getVehicleDetailById(this.vehicleId)
    .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        this.vehicleDetailForm.setValue({
          VehicleId: x.VehicleId,
          PlateNo: x.PlateNo,
          EmployeeId: x.EmployeeId,
          StartingMileage: x.StartingMileage,
          IncurredMileage: x.IncurredMileage,
          MobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
          ModelYear: x.ModelYear,
          OfficeId: x.OfficeId,
          FuelConsumptionRate: x.StandardFuelConsumptionRate,
          ManufacturerCountry: x.ManufacturerCountry,
          EngineNo: x.EngineNo,
          RegistrationNo: x.RegistrationNo,
          ChasisNo: x.ChasisNo,
          Remarks: x.Remarks
        });
      });
  }

  saveVehicleDetail() {
    this.commonLoader.showLoader();
    if (this.vehicleDetailForm.valid) {
      this.purchaseService.saveVehicleDetail(this.vehicleDetailForm.value)
                          .subscribe(x => {
                            if (x) {
                              this.backToDetails();
                              this.commonLoader.hideLoader();
                            }
                          }, error => {
                            this.commonLoader.hideLoader();
                          });
    }
  }

  backToDetails() {
    this.router.navigate(['store/vehicle/detail', this.vehicleId]);
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
