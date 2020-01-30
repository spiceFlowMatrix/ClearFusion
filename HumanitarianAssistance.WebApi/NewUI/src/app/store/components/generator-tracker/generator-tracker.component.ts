import { Component, OnInit, HostListener } from '@angular/core';
import { Observable, of } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { AddHoursComponent } from '../add-hours/add-hours.component';
import { PurchaseService } from '../../services/purchase.service';
import { IGeneratorTrackerFilter, IGeneratorList } from '../../models/generator';
import { IDropDownModel } from 'src/app/dashboard/accounting/report-services/report-models';

@Component({
  selector: 'app-generator-tracker',
  templateUrl: './generator-tracker.component.html',
  styleUrls: ['./generator-tracker.component.scss']
})
export class GeneratorTrackerComponent implements OnInit {
  generatorListHeaders$ = of(['Id', 'K.V.', 'Fuel Consumption Rate', 'Incurred Usage(Hours)', 'Total Usage(Hours)', 'Total Cost',
                              'Original Cost']);
  generatorList$: Observable<IGeneratorList[]>;
  currencyList$: Observable<IDropDownModel[]>;
  actions: TableActionsModel;
  recordsCount: number;
  generatorTrackerFilter: IGeneratorTrackerFilter;
  selectedDisplayCurrency: number;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

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
    this.getScreenSize();
    this.getAllCurrencies();
  }

  initializeModel() {
    this.generatorTrackerFilter = {
      Voltage: null,
      OfficeId: null,
      ModelYear: null,
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

  goToDetails(e) {
    this.router.navigate(['store/generator/detail', e.GeneratorId]);
  }

  openHoursModal(event) {
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
    this.generatorTrackerFilter = {
      TotalCost: selectedFilter.TotalCost,
      ModelYear: selectedFilter.ModelYear,
      OfficeId: selectedFilter.OfficeId,
      Voltage: selectedFilter.Voltage,
      DisplayCurrency: this.selectedDisplayCurrency,
      pageSize: 10,
      pageIndex: 0
    };

    this.getGeneratorList(this.generatorTrackerFilter);
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

          this.getGeneratorList(this.generatorTrackerFilter);
        }
      },
        (error) => {
        });
  }

  selectedDisplayCurrencyChanged() {
    this.generatorTrackerFilter.DisplayCurrency = this.selectedDisplayCurrency;
    this.getGeneratorList(this.generatorTrackerFilter);

  }

  //#region "pageEvent"
  pageEvent(e) {
    this.generatorTrackerFilter.pageIndex = e.pageIndex;
    this.generatorTrackerFilter.pageSize = e.pageSize;
    this.getGeneratorList(this.generatorTrackerFilter);
  }
  //#endregion
}
