import { Component, OnInit, HostListener } from '@angular/core';
import { of, Observable } from 'rxjs';
import { IVehicleList, IVehicleTrackerFilter } from '../../models/vehicles';
import { TableActionsModel } from 'projects/library/src/public_api';
import { MatDialog } from '@angular/material';
import { AddMilageComponent } from '../add-milage/add-milage.component';
import { Router } from '@angular/router';
import { PurchaseService } from '../../services/purchase.service';
import { IDropDownModel } from '../../models/purchase';

@Component({
  selector: 'app-vehicle-tracker',
  templateUrl: './vehicle-tracker.component.html',
  styleUrls: ['./vehicle-tracker.component.scss']
})
export class VehicleTrackerComponent implements OnInit {

  vehicleListHeaders$ = of(['Vechile Id', 'Plate No', 'Driver', 'Fuel Consumption Rate', 'Total Mileage (KM)', 'Total Cost',
                            'Original Cost']);
  vehicleList$: Observable<IVehicleList[]>;
  actions: TableActionsModel;
  vehicleTrackerFilter: IVehicleTrackerFilter;
  currencyList$: Observable<IDropDownModel[]>;
  selectedDisplayCurrency: number;

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

    this.actions = {
      items: {
        button: { status: true, text: 'Add Mileage' },
        delete: false,
        download: false,
      },
      subitems: {
      }

    };
    this.getScreenSize();
    this.getAllCurrencies();
  }

  initializeModel() {
    this.vehicleTrackerFilter = {
      EmployeeId: null,
      OfficeId: null,
      PlateNo: null,
      TotalCost: null,
      DisplayCurrency: null,
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
    this.vehicleTrackerFilter = {
      TotalCost: selectedFilter.TotalCost,
      EmployeeId: selectedFilter.EmployeeId,
      OfficeId: selectedFilter.OfficeId,
      PlateNo: selectedFilter.PlateNo,
      DisplayCurrency: this.selectedDisplayCurrency,
      pageSize: 10,
      pageIndex: 0
    };

    this.getVehicleList(this.vehicleTrackerFilter);
  }

  getVehicleList(filter) {
    this.purchaseService.getVehicleList(filter)
      .subscribe(response => {
        this.recordsCount = response.TotalRecords;
        this.vehicleList$ = of(response.VehicleList.map((element) => {
          return {
            VehicleId: element.VehicleId,
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
    this.router.navigate(['store/vehicle/detail', e.VehicleId]);
  }

  openMilageModal(event) {
    if (event.type === 'button') {
      const dialogRef = this.dialog.open(AddMilageComponent, {
        width: '850px',
        data: {
            vehicleId: event.item.VehicleId
        }
      });
    }
  }

  getAllCurrencies() {
    this.purchaseService.getAllCurrencies()
      .subscribe(x => {
        if (x.StatusCode === 200) {
          this.selectedDisplayCurrency = x.data.CurrencyList[0].CurrencyId;
           this.currencyList$ = of(x.data.CurrencyList.map(y => {
            return {
              name: y.CurrencyCode + '-' + y.CurrencyName,
              value: y.CurrencyId
            };
          }));
          this.vehicleTrackerFilter.DisplayCurrency = this.selectedDisplayCurrency;
          this.getVehicleList(this.vehicleTrackerFilter);
        }
      },
        (error) => {
          console.error(error);
        });
  }

  selectedDisplayCurrencyChanged() {
    this.vehicleTrackerFilter.DisplayCurrency = this.selectedDisplayCurrency;
    this.getVehicleList(this.vehicleTrackerFilter);

  }

  //#region "pageEvent"
  pageEvent(e) {
    this.vehicleTrackerFilter.pageIndex = e.pageIndex;
    this.vehicleTrackerFilter.pageSize = e.pageSize;
    this.getVehicleList(this.vehicleTrackerFilter);
  }
  //#endregion
}
