import { Component, OnInit, HostListener, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { AddHoursComponent } from '../add-hours/add-hours.component';
import { PurchaseService } from '../../services/purchase.service';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

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
  }

  initForm() {
    this.generatorDetailForm = {
      GeneratorId: null,
      Voltage: null,
      StartingUsage: null,
      IncurredUsage: null,
      MobilOilConsumptionRate: null,
      ModelYear: null,
      OfficeId: null,
      FuelConsumptionRate: null,
      PurchaseName: null,
      PurchaseId: null,
      OfficeName: null,
      PurchasedBy: null,
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
          OfficeName: x.OfficeName,
          MobilOilConsumptionRate: x.MobilOilConsumptionRate,
          ModelYear: x.ModelYear,
          OfficeId: x.OfficeId,
          FuelConsumptionRate: x.FuelConsumptionRate,
          PurchasedBy: x.PurchasedBy,
          PurchaseName: x.PurchaseName,
          PurchaseId: x.PurchaseId
        };

        this.commonLoader.hideLoader();
      }, error => {
        this.commonLoader.hideLoader();
      });
  }

  openHoursModal(event) {
    debugger;
    const dialogRef = this.dialog.open(AddHoursComponent, {
      width: '850px',
      data: {
        generatorId: this.generatorId
      }
    });
  }
  goToDetails() {
    this.router.navigate(['store/generator/edit', 1]);
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
