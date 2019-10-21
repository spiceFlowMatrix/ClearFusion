import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { AddHoursComponent } from '../add-hours/add-hours.component';
import { PurchaseService } from '../../services/purchase.service';
import { IGeneratorTrackerFilter, IGeneratorList } from '../../models/generator';

@Component({
  selector: 'app-generator-tracker',
  templateUrl: './generator-tracker.component.html',
  styleUrls: ['./generator-tracker.component.scss']
})
export class GeneratorTrackerComponent implements OnInit {
  generatorListHeaders$ = of(['Id', 'K.V.', 'Fuel Consumption Rate', 'Incurred Usage(Hours)', 'Total Usage(Hours)', 'Total Cost',
                              'Original Cost']);
  generatorList$: Observable<IGeneratorList[]>;
  actions: TableActionsModel;
  recordsCount: number;
  generatorTrackerFilter: IGeneratorTrackerFilter;

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
    };
    this.initializeModel();
    this.getGeneratorList(this.generatorTrackerFilter);
  }

  initializeModel() {
    this.generatorTrackerFilter = {
      Voltage: null,
      OfficeId: null,
      ModelYear: null,
      TotalCost: null,
      pageIndex: 0,
      pageSize: 10
    };
  }

  goToDetails(e) {
    this.router.navigate(['store/generator/detail', e.GeneratorId]);
  }

  openHoursModal(event) {
    debugger;
    if (event.type === 'button') {
      const dialogRef = this.dialog.open(AddHoursComponent, {
        width: '850px',
        data: {
           generatorId: event.item.GeneratorId,
        }
      });
    }
  }

  getFilteredGeneratorList(selectedFilter) {
    const filter = {
      TotalCost: selectedFilter.TotalCost,
      ModelYear: selectedFilter.ModelYear,
      OfficeId: selectedFilter.OfficeId,
      Voltage: selectedFilter.Voltage,
      pageSize: 10,
      pageIndex: 0
    };

    this.getGeneratorList(filter);
  }

  getGeneratorList(filter) {
    this.purchaseService.getGeneratorList(filter)
      .subscribe(response => {
        this.recordsCount = response.TotalRecords;
        this.generatorList$ = of(response.GeneratorTrackerList.map((element) => {
          return {
            GeneratorId: element.GeneratorId,
            Voltage: element.Voltage,
            FCRate: element.FuelConsumptionRate,
            IncurredUsage: element.IncurredUsage,
            TotalUsage: element.TotalUsage,
            TotalCost: element.TotalCost,
            OriginalCost: element.OriginalCost,
          };
        }));
      });
  }

  //#region "pageEvent"
  pageEvent(e) {
    this.generatorTrackerFilter.pageIndex = e.pageIndex;
    this.generatorTrackerFilter.pageSize = e.pageSize;
    this.getGeneratorList(this.generatorTrackerFilter);
  }
  //#endregion
}
