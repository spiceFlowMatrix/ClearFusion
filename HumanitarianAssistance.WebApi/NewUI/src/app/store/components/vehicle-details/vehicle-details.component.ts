import { Component, OnInit, HostListener, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material';
import { AddMilageComponent } from '../add-milage/add-milage.component';
import { Router, ActivatedRoute } from '@angular/router';
import { PurchaseService } from '../../services/purchase.service';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.scss']
})
export class VehicleDetailsComponent implements OnInit, OnDestroy {

  vehicleDetailForm: any;

  vehicleId: number;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private dialog: MatDialog, private router: Router,
    private activatedRoute: ActivatedRoute, private purchaseService: PurchaseService) {
      this.activatedRoute.params.subscribe(params => {
        this.vehicleId = params['id'];
      });
    }

  ngOnInit() {
    this.initForm();
    this.getVehicleDetailById();
    this.getScreenSize();
  }

  initForm() {
    this.vehicleDetailForm = {
      VehicleId: null,
      PlateNo: null,
      EmployeeId: null,
      StartingMileage: null,
      IncurredMileage: null,
      MobilOilConsumptionRate: null,
      ModelYear: null,
      OfficeId: null,
      FuelConsumptionRate: null,
      EmployeeName: null,
      PurchaseName: null,
      PurchaseId: null
    };
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  openMilageModal() {
    const dialogRef = this.dialog.open(AddMilageComponent, {
      width: '850px',
      data: {
        vehicleId: this.vehicleId
      }
    });
  }

  getVehicleDetailById() {
    this.purchaseService.getVehicleDetailById(this.vehicleId)
    .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        this.vehicleDetailForm = {
          VehicleId: x.VehicleId,
          PlateNo: x.PlateNo,
          EmployeeId: x.EmployeeId,
          StartingMileage: x.StartingMileage,
          IncurredMileage: x.IncurredMileage,
          MobilOilConsumptionRate: x.MobilOilConsumptionRate,
          ModelYear: x.ModelYear,
          OfficeId: x.OfficeId,
          FuelConsumptionRate: x.FuelConsumptionRate,
          EmployeeName: x.EmployeeName,
          PurchaseName: x.PurchaseName,
          PurchaseId: x.PurchaseId
        };
      });
  }

  editVehicleDetail() {
    this.router.navigate(['store/vehicle/edit', this.vehicleId]);
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
