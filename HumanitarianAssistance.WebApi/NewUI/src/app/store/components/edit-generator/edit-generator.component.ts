import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { PurchaseService } from '../../services/purchase.service';
import { IGeneratorItem } from '../../models/generator';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';

@Component({
  selector: 'app-edit-generator',
  templateUrl: './edit-generator.component.html',
  styleUrls: ['./edit-generator.component.scss']
})
export class EditGeneratorComponent implements OnInit, OnDestroy {

  generatorId: number;
  generatorDetailForm: FormGroup;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private router: Router, private purchaseService: PurchaseService,
    private activatedRoute: ActivatedRoute, private fb: FormBuilder) {
    this.activatedRoute.params.subscribe(params => {
      this.generatorId = params['id'];
    });
  }
  ngOnInit() {
    this.generatorDetailForm = this.fb.group({
      'GeneratorId': [null],
      'Voltage': [null, [Validators.required]],
      'StartingUsage': [null],
      'IncurredUsage': [null],
      'MobilOilConsumptionRate': [null],
      'ModelYear': [null, [Validators.required]],
      'OfficeId': [null, [Validators.required]],
      'FuelConsumptionRate': [null]
    });

    this.getGeneratorDetailById();
  }

  getGeneratorDetailById() {
    this.purchaseService.getGeneratorDetailById(this.generatorId)
    .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        this.generatorDetailForm.setValue({
          GeneratorId: x.GeneratorId,
          Voltage: x.Voltage,
          StartingUsage: x.StartingUsage,
          IncurredUsage: x.IncurredUsage,
          MobilOilConsumptionRate: x.MobilOilConsumptionRate,
          ModelYear: x.ModelYear,
          OfficeId: x.OfficeId,
          FuelConsumptionRate: x.FuelConsumptionRate
        });
      });
  }

  saveGeneratorDetail() {
    if (this.generatorDetailForm.valid) {
      this.purchaseService.saveGeneratorDetail(this.generatorDetailForm.value)
                          .subscribe(x => {
                            if (x) {
                              this.backToDetails();
                            }
                          });
    }
  }

  backToDetails() {
  this.router.navigate(['store/generator/detail', this.generatorId]);
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
