import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { IVehicleList } from '../../models/vehicles';
import { TableActionsModel } from 'projects/library/src/public_api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-generator-tracker',
  templateUrl: './generator-tracker.component.html',
  styleUrls: ['./generator-tracker.component.scss']
})
export class GeneratorTrackerComponent implements OnInit {
  vehicleListHeaders$ = of(["Plate No", "Driver", "Fuel Consumption Rate", "Total Mileage (KM)", "Total Cost", "Original Cost"]);
  vehicleList$: Observable<IVehicleList[]>;
  actions: TableActionsModel;
  constructor(private router: Router) { }

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
    this.router.navigate(['store/generator/detail',1]);
  }
  // openMilageModal(event) {
  //   if (event.type == "button") {
  //     const dialogRef = this.dialog.open(AddMilageComponent, {
  //       width: '850px',
  //       data: {
  //         //value: event.item.Id,
  //         //  officeId: this.filterValueModel.OfficeId
  //       }
  //     });
  //   }
  // }
}
