import { Component, OnInit, HostListener, ElementRef, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FieldConfigService } from 'src/app/store/services/field-config.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { of } from 'rxjs/internal/observable/of';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { ExchangeGainLossReportService } from '../exchange-gain-loss-report.service';

@Component({
  selector: 'app-configuration-filter',
  templateUrl: './configuration-filter.component.html',
  styleUrls: ['./configuration-filter.component.scss']
})
export class ConfigurationFilterComponent implements OnInit, OnDestroy {

  showConfig = false;
  gainLossConfigForm: FormGroup;
  currency$: Observable<IDropDownModel[]>;
  accounts$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

   // screen
   screenHeight: any;
   screenWidth: any;
   scrollStyles: any;

  constructor(private eRef: ElementRef, private toastr: ToastrService,
    private fieldConfig: FieldConfigService, private fb: FormBuilder, private gainLossService: ExchangeGainLossReportService) { }

  ngOnInit() {
    this.onInItForm();
    forkJoin([
      this.getAllCurrency(),
      this.getAllAccounts()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeAllCurrency(result[0]);
        this.subscribeAllInputAccounts(result[1]);
      });

  }

  onInItForm() {
    this.gainLossConfigForm = this.fb.group({
      'CurrencyId': [null, Validators.required],
      'StartDate': [null, Validators.required],
      'EndDate': [null, Validators.required],
      'ComparisionDate': [null, Validators.required],
      'DebitAccount': [null, Validators.required],
      'CreditAccount': [null, Validators.required],
    });
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 170 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  getAllCurrency() {
    return this.gainLossService.GetCurrencyList();
  }

  getAllAccounts() {
    return this.gainLossService.GetInputLevelAccountList();
  }

  subscribeAllCurrency(response: any) {
    this.currency$ = of(response.data.map(y => {
      return {
        value: y.CurrencyId,
        name: y.CurrencyCode + '-' + y.CurrencyName
      };
    }));
  }

  subscribeAllInputAccounts(response: any) {
    this.accounts$ = of(response.data.map(y => {
      return {
        value: y.AccountCode,
        name: y.AccountName
      };
    }));
  }

  saveCalculatorConfigData() {
    if (!this.gainLossConfigForm.valid) {
      this.toastr.warning('Please correct form errors and submit again');
    }

    this.gainLossService.SaveCalculatorConfigData(this.gainLossConfigForm.value)
  }

  show() {
    this.showConfig = true;
  }

  getState(e) {
    this.showConfig = e;
  }

  useDefault(event, type: string) {

  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }

}
