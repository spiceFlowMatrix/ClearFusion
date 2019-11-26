import { Component, OnInit, HostListener, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { AddHoursComponent } from '../add-hours/add-hours.component';
import { PurchaseService } from '../../services/purchase.service';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject, Observable } from 'rxjs';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { IDropDownModel, IMonthlyBreakDown } from '../../models/purchase';
import { TransportItemCategory } from 'src/app/shared/enum';

@Component({
  selector: 'app-generator-details',
  templateUrl: './generator-details.component.html',
  styleUrls: ['./generator-details.component.scss']
})
export class GeneratorDetailsComponent implements OnInit, OnDestroy {

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  // variables
  generatorDetailForm: any;
  generatorId: number;
  transportType = TransportItemCategory.Generator;

  monthlyBreakdownYear: number;
  monthlyBreakdownYearList$: Observable<IDropDownModel[]>;
  generatorMonthlyBreakdownList: IMonthlyBreakDown;

  // subject
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private dialog: MatDialog, private router: Router, private activatedRoute: ActivatedRoute,
    private purchaseService: PurchaseService, private commonLoader: CommonLoaderService) {
    this.activatedRoute.params.subscribe(params => {
      this.generatorId = params['id'];
    });
  }

  ngOnInit() {
    this.getScreenSize();
    this.initForm();
    this.getGeneratorDetailById();
    this.getMonthlyBreakDownYears();
  }

  initForm() {
    this.generatorDetailForm = {
      GeneratorId: null,
      Voltage: null,
      StartingUsage: null,
      IncurredUsage: null,
      StandardMobilOilConsumptionRate: null,
      ModelYear: null,
      OfficeId: null,
      StandardFuelConsumptionRate: null,
      PurchaseName: null,
      PurchaseId: null,
      OfficeName: null,
      PurchasedBy: null,
      TotalFuelUsage: null,
      TotalMobilOilUsage: null,
      FuelTotalCost: null,
      MobilOilTotalCost: null,
      SparePartsTotalCost: null,
      ServicesAndMaintenanceTotalCost: null,
      CurrentUsage: null,
      GeneratorStartingCost: null,
      ActualFuelConsumptionRate: null,
      ActualMobilOilConsumptionRate: null
    };

    this.generatorMonthlyBreakdownList = {
      StartingMileage: null,
      IncurredMileage: null,
      StartingUsage: null,
      IncurredUsage: null,
      StandardMobilOilConsumptionRate: null,
      StandardFuelConsumptionRate: null,
      StartingCost: null,
      CostAnalysisBreakDownList: [],
      UsageAnalysisBreakDownList: []
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

  getGeneratorDetailById() {
    this.commonLoader.showLoader();
    this.purchaseService.getGeneratorDetailById(this.generatorId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        this.generatorDetailForm = {
          GeneratorId: x.GeneratorId,
          Voltage: x.Voltage,
          StartingUsage: x.StartingUsage,
          IncurredUsage: x.IncurredUsage,
          StandardMobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
          ModelYear: x.ModelYear,
          OfficeId: x.OfficeId,
          StandardFuelConsumptionRate: x.StandardFuelConsumptionRate,
          PurchaseName: x.PurchaseName,
          PurchaseId: x.PurchaseId,
          OfficeName: x.OfficeName,
          PurchasedBy: x.PurchasedBy,
          TotalFuelUsage: x.TotalFuelUsage,
          TotalMobilOilUsage: x.TotalMobilOilUsage,
          FuelTotalCost: x.FuelTotalCost,
          MobilOilTotalCost: x.MobilOilTotalCost,
          SparePartsTotalCost: x.SparePartsTotalCost,
          ServicesAndMaintenanceTotalCost: x.ServicesAndMaintenanceTotalCost,
          CurrentUsage: x.CurrentUsage,
          GeneratorStartingCost: x.GeneratorStartingCost,
          ActualFuelConsumptionRate: x.ActualFuelConsumptionRate,
          ActualMobilOilConsumptionRate: x.ActualMobilOilConsumptionRate
        };
        this.commonLoader.hideLoader();
      }, error => {
        this.commonLoader.hideLoader();
      });
  }

  openHoursModal(event) {
    const dialogRef = this.dialog.open(AddHoursComponent, {
      width: '850px',
      data: {
        generatorId: this.generatorId
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getGeneratorDetailById();
      this.getGeneratorMonthlyBreakdownData();
    });
  }

  onTabClick(event) {
    if (event.index === 1) {
      this.getGeneratorMonthlyBreakdownData();
    } else if(event.index === 0) {
      this.getGeneratorDetailById();
    }
  }

  getGeneratorMonthlyBreakdownData() {
    const data = {
      GeneratorId: +this.generatorId,
      SelectedYear: this.monthlyBreakdownYear
    };

    this.purchaseService.getGeneratorMonthlyBreakdown(data)
      .pipe(takeUntil(this.destroyed$))
        .subscribe(x => {
          this.generatorMonthlyBreakdownList = {
            StartingUsage: x.StartingUsage,
            IncurredUsage: x.IncurredUsage,
            StandardMobilOilConsumptionRate: x.StandardMobilOilConsumptionRate,
            StandardFuelConsumptionRate: x.StandardFuelConsumptionRate,
            StartingCost: x.StartingCost,
            CostAnalysisBreakDownList: x.CostAnalysisBreakDownList,
            UsageAnalysisBreakDownList: x.UsageAnalysisBreakDownList
          };
        });
  }

  getMonthlyBreakDownYears() {
    this.monthlyBreakdownYearList$ = this.purchaseService.getPreviousYearsList(10);
    this.monthlyBreakdownYearList$.subscribe(x => {
      this.monthlyBreakdownYear = x[0].value;
    });
  }

  goToDetails() {
    this.router.navigate(['store/generator/edit', this.generatorId]);
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
