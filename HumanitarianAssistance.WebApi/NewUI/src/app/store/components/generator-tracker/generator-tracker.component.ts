import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { IVehicleList } from '../../models/vehicles';
import { TableActionsModel } from 'projects/library/src/public_api';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { AddHoursComponent } from '../add-hours/add-hours.component';
import { PurchaseService } from '../../services/purchase.service';

@Component({
  selector: 'app-generator-tracker',
  templateUrl: './generator-tracker.component.html',
  styleUrls: ['./generator-tracker.component.scss']
})
export class GeneratorTrackerComponent implements OnInit {
  generatorListHeaders$ = of(['Id', 'K.V.', 'Fuel Consumption Rate', 'Incurred Usage(Hours)', 'Total Usage(Hours)', 'Total Cost',
                              'Original Cost']);
  generatorList$: Observable<IVehicleList[]>;
  actions: TableActionsModel;
  recordsCount: number;

  constructor(private router: Router , private dialog: MatDialog, private purchaseService: PurchaseService) { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: true, text: 'Add Hours' },
        delete: false,
        download: false,
      },
      subitems: {
      }

    }
  }
  goToDetails(e) {
    this.router.navigate(['store/generator/detail', 1]);
  }
  openHoursModal(event) {
    if (event.type === 'button') {
      const dialogRef = this.dialog.open(AddHoursComponent, {
        width: '850px',
        data: {
          //value: event.item.Id,
          //  officeId: this.filterValueModel.OfficeId
        }
      });
    }
  }

  getFilteredGeneratorList(selectedFilter) {
    const filter = {
      TotalCost: selectedFilter.TotalCost,
      ModelYear: selectedFilter.ModelYear,
      OfficeId: selectedFilter.OfficeId,
      PlateNo: selectedFilter.Voltage,
      pageSize: 10,
      pageIndex: 0
    };

    this.getGeneratorList(filter);
  }

  getGeneratorList(filter) {
    this.purchaseService.getVehicleList(filter)
      .subscribe(response => {
        this.recordsCount = response.TotalRecords;
        this.generatorList$ = of(response.VehicleList.map((element) => {
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
}
