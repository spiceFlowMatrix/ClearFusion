import { Component, OnInit, HostListener } from '@angular/core';
import { of, Observable } from 'rxjs';
import { IVehicleList, IVehicleTrackerFilter } from '../../models/vehicles';
import { TableActionsModel } from 'projects/library/src/public_api';
import { MatDialog } from '@angular/material';
import { AddMilageComponent } from '../add-milage/add-milage.component';
import { Router } from '@angular/router';
import { PurchaseService } from '../../services/purchase.service';

@Component({
  selector: 'app-vehicle-tracker',
  templateUrl: './vehicle-tracker.component.html',
  styleUrls: ['./vehicle-tracker.component.scss']
})
export class VehicleTrackerComponent implements OnInit {

  vehicleListHeaders$ = of(['Plate No', 'Driver', 'Fuel Consumption Rate', 'Total Mileage (KM)', 'Total Cost', 'Original Cost']);
  vehicleList$: Observable<IVehicleList[]>;
  actions: TableActionsModel;
  vehicleTrackerFilter: IVehicleTrackerFilter;

  // Paging
  pageSize = 10;
  pageIndex = 0;
  recordsCount = 0;

  // Screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  constructor(private dialog: MatDialog, private router: Router,
    private purchaseService: PurchaseService) { }


  ngOnInit() {
    this.initializeModel();
    this.getVehicleList(this.vehicleTrackerFilter);
    this.actions = {
      items: {
        button: { status: true, text: 'Add Milage' },
        delete: false,
        download: false,
      },
      subitems: {
      }

    };
    this.getScreenSize();
  }

  initializeModel() {
    this.vehicleTrackerFilter = {
      EmployeeId: null,
      OfficeId: null,
      PlateNo: null,
      TotalCost: null,
      pageIndex: 0,
      pageSize: 10
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

  getFilteredVehicleList(selectedFilter) {
    debugger;
    const filter = {
      TotalCost: selectedFilter.TotalCost,
      EmployeeId: selectedFilter.EmployeeId,
      OfficeId: selectedFilter.OfficeId,
      PlateNo: selectedFilter.PlateNo,
      pageSize: 10,
      pageIndex: 0
    };

    this.getVehicleList(filter);
  }

  getVehicleList(filter) {
    this.purchaseService.getVehicleList(filter)
      .subscribe(response => {
        this.recordsCount = response.TotalRecords;
        this.vehicleList$ = of(response.VehicleList.map((element) => {
          return {
            PlateNo: element.PlateNo,
            Driver: element.EmployeeName,
            FCRate: element.FuelConsumptionRate,
            TotalMileage: element.TotalMileage,
            TotalCost: element.TotalCost,
            OriginalCost: element.OriginalCost,
          };
        }));
      });
  }


  goToDetails(e) {
    this.router.navigate(['store/vehicle/detail', 1]);
    // console.log(e);
  }

  openMilageModal(event) {
    if (event.type === 'button') {
      const dialogRef = this.dialog.open(AddMilageComponent, {
        width: '850px',
        data: {
          // value: event.item.Id,
          //  officeId: this.filterValueModel.OfficeId
        }
      });
    }
  }

  //#region "pageEvent"
  pageEvent(e) {
    this.vehicleTrackerFilter.pageIndex = e.pageIndex;
    this.vehicleTrackerFilter.pageSize = e.pageSize;
    this.getVehicleList(this.vehicleTrackerFilter);
  }
  //#endregion
}
