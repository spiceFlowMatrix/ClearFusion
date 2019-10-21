import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { IVehicleList } from '../../models/vehicles';
import { TableActionsModel } from 'projects/library/src/public_api';
import { MatDialog } from '@angular/material';
import { AddMilageComponent } from '../add-milage/add-milage.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vehicle-tracker',
  templateUrl: './vehicle-tracker.component.html',
  styleUrls: ['./vehicle-tracker.component.scss']
})
export class VehicleTrackerComponent implements OnInit {

  vehicleListHeaders$ = of(["Plate No", "Driver", "Fuel Consumption Rate", "Total Mileage (KM)", "Total Cost", "Original Cost"]);
  vehicleList$: Observable<IVehicleList[]>;
  actions: TableActionsModel;
  constructor(private dialog: MatDialog , private router: Router) { }

  ngOnInit() {
    this.vehicleList$ = of([{
      PlateNo: 'KBL-3534-32',
      Driver: 'Employee Name',
      FCRate: '32.7',
      TotalMileage: '8700',
      TotalCost: '8700',
      OriginalCost: '8700',
    },
    {
      PlateNo: 'KBL-3534-32',
      Driver: 'Employee Name',
      FCRate: '32.7',
      TotalMileage: '8700',
      TotalCost: '8700',
      OriginalCost: '8700',
    }, {
      PlateNo: 'KBL-3534-32',
      Driver: 'Employee Name',
      FCRate: '32.7',
      TotalMileage: '8700',
      TotalCost: '8700',
      OriginalCost: '8700',
    }] as IVehicleList[]);

    this.actions = {
      items: {
        button: { status: true, text: 'Add Milage' },
        delete: false,
        download: false,
      },
      subitems: {
      }

    }
  }
  goToDetails(e) {
    this.router.navigate(['store/vehicle/detail',1]);
  //  console.log(e);
  }
  openMilageModal(event) {
    if (event.type == "button") {
      const dialogRef = this.dialog.open(AddMilageComponent, {
        width: '850px',
        data: {
          //value: event.item.Id,
          //  officeId: this.filterValueModel.OfficeId
        }
      });
    }
  }
}
