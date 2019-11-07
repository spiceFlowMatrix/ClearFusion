
import { Component, OnInit, HostListener, OnDestroy, ViewChild } from '@angular/core';
import { MatDialog, MatDatepicker, NativeDateAdapter } from '@angular/material';
import { AddMilageComponent } from '../add-milage/add-milage.component';
import { Router, ActivatedRoute } from '@angular/router';
import { PurchaseService } from '../../services/purchase.service';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { FormControl } from '@angular/forms';
import { IDropDownModel } from '../../models/purchase';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.scss']
})
export class VehicleDetailsComponent implements OnInit, OnDestroy {

  vehicleDetailForm: any;
  vehicleId: number;
  date = new FormControl();
  monthlyBreakdownYear = new FormControl();
  monthlyBreakdownYearList$: Observable<IDropDownModel[]>;
  vehicleMonthlyBreakdownList: any;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  @ViewChild(MatDatepicker) picker;

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
    this.getMonthlyBreakDownYears();
  }

  initForm() {
    this.vehicleDetailForm = {
      VehicleId: null,
      PlateNo: null,
      EmployeeId: null,
      StartingMileage: null,
      IncurredMileage: null,
      StandardMobilOilConsumptionRate: null,
      ModelYear: null,
      OfficeId: null,
      StandardFuelConsumptionRate: null,
      EmployeeName: null,
      PurchaseName: null,
      PurchaseId: null,
      OfficeName: null,
      TotalFuelUsage: null,
      TotalMobilOilUsage: null,
      ActualFuelConsumptionRate: null,
      ActualMobilOilConsumptionRate: null,
      FuelTotalCost: null,
      MobilOilTotalCost: null,
      SparePartsTotalCost: null,
      ServiceAndMaintenanceTotalCost: null,
      CurrentMileage: null,
      VehicleStartingCost: null
    };

    this.vehicleMonthlyBreakdownList = {
      StartingMileage: null,
      IncurredMileage: null,
      StandardMobilOilConsumptionRate: null,
      StandardFuelConsumptionRate: null,
      StartingCost: null
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
          StandardMobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
          ModelYear: x.ModelYear,
          OfficeId: x.OfficeId,
          StandardFuelConsumptionRate: x.StandardFuelConsumptionRate,
          EmployeeName: x.EmployeeName,
          PurchaseName: x.PurchaseName,
          PurchaseId: x.PurchaseId,
          OfficeName: x.OfficeName,
          TotalFuelUsage: x.TotalFuelUsage,
          TotalMobilOilUsage: x.TotalMobilOilUsage,
          ActualFuelConsumptionRate: x.ActualFuelConsumptionRate,
          ActualMobilOilConsumptionRate: x.ActualMobilOilConsumptionRate,
          FuelTotalCost: x.FuelTotalCost,
          MobilOilTotalCost: x.MobilOilTotalCost,
          SparePartsTotalCost: x.SparePartsTotalCost,
          ServiceAndMaintenanceTotalCost: x.ServiceAndMaintenanceTotalCost,
          CurrentMileage: x.CurrentMileage,
          VehicleStartingCost: x.VehicleStartingCost
        };
      });
  }

  onTabClick(event) {

    if (event.index === 1) {
      this.getVehicleMonthlyBreakdownData();
    }
  }

  getVehicleMonthlyBreakdownData() {
    debugger;
    const data = {
      VehicleId: +this.vehicleId,
      SelectedYear: this.monthlyBreakdownYear.value
    };

    this.purchaseService.getVehicleMonthlyBreakdown(data)
      .pipe(takeUntil(this.destroyed$))
        .subscribe(x => {
          this.vehicleMonthlyBreakdownList = {
            StartingMileage: x.StartingMileage,
            IncurredMileage: x.IncurredMileage,
            StandardMobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
            StandardFuelConsumptionRate: x.StandardFuelConsumptionRate,
            StartingCost: x.StartingCost
          };
        });
  }

  getMonthlyBreakdownYear(event) {
    this.monthlyBreakdownYear = event;
  }

  editVehicleDetail() {
    this.router.navigate(['store/vehicle/edit', this.vehicleId]);
  }

  monthSelected(params) {
    this.date.setValue(params);
    this.picker.close();
  }

  getMonthlyBreakDownYears() {
    this.monthlyBreakdownYearList$ = this.purchaseService.getMonthlyBreakDownYears();
    this.monthlyBreakdownYearList$.subscribe(x => {
      this.monthlyBreakdownYear.setValue(x[0].value);
    });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
