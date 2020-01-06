import { Component, OnInit, HostListener, ElementRef, OnDestroy, Output, EventEmitter } from '@angular/core';
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
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-configuration-filter',
  templateUrl: './configuration-filter.component.html',
  styleUrls: ['./configuration-filter.component.scss']
})
export class ConfigurationFilterComponent implements OnInit, OnDestroy {

  showConfig = false;
  gainLossConfigForm: FormGroup;
  defaultFinancialYearDate: IDefaultFinancialYearDate;
  currency$: Observable<IDropDownModel[]>;
  accounts$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  @Output() configData = new EventEmitter();

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
      this.getAllAccounts(),
      this.getDefaultAccountingPeriod()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeAllCurrency(result[0]);
        this.subscribeAllInputAccounts(result[1]);
        this.subscribeDefaultAccountingPeriod(result[2]);
      });

    this.defaultFinancialYearDate = {
      startDate: null,
      endDate: null
    };

    this.getData();

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
      return;
    }

    this.gainLossConfigForm.value.ComparisionDate = StaticUtilities.setLocalDate(this.gainLossConfigForm.value.ComparisionDate);
    this.gainLossConfigForm.value.StartDate = StaticUtilities.setLocalDate(this.gainLossConfigForm.value.StartDate);
    this.gainLossConfigForm.value.EndDate = StaticUtilities.setLocalDate(this.gainLossConfigForm.value.EndDate);

    this.gainLossService.SaveCalculatorConfigData(this.gainLossConfigForm.value)
      .subscribe(x => {
        if (x) {
          this.toastr.success('Configuration Updated');
          this.configDataEmit();
        }
      }, error => {
        this.toastr.error('Failed, Please try again');
      });
  }

  getData() {
    this.getConfigurationFilter();
    this.getDefaultAccountingPeriod();
  }

  getConfigurationFilter() {
    this.gainLossService.GetGainLossCaculatorConfiguration().subscribe(x => {
      if (x.CalculatorConfiguration) {
        this.onInItForm();
        this.gainLossConfigForm.patchValue({
          'CurrencyId': x.CalculatorConfiguration.CurrencyId,
          'StartDate': x.CalculatorConfiguration.StartDate,
          'EndDate': x.CalculatorConfiguration.EndDate,
          'ComparisionDate': x.CalculatorConfiguration.ComparisionDate,
          'DebitAccount': x.CalculatorConfiguration.DebitAccount,
          'CreditAccount': x.CalculatorConfiguration.CreditAccount,
        });

        this.configDataEmit();
      }
    }, error => {
      this.toastr.error('Failed, Please try again');
    });
  }

  getDefaultAccountingPeriod() {
    return this.gainLossService.GetDefaultAccountingPeriod();
  }

  show() {
    this.showConfig = true;
    this.getData();
  }

  getState(e) {
    this.showConfig = e;
  }

  useDefault(event, type: string) {
    if (type === 'period') {

      if (this.defaultFinancialYearDate.startDate && this.defaultFinancialYearDate.endDate) {
        this.gainLossConfigForm.patchValue({
          'StartDate': this.defaultFinancialYearDate.startDate,
          'EndDate': this.defaultFinancialYearDate.endDate
        });
      } else {
        this.toastr.warning('no default Accounting Period set');
      }
    } else if (type === 'comparisionDate') {
      if (this.defaultFinancialYearDate.endDate) {
        this.gainLossConfigForm.patchValue({
          'ComparisionDate': StaticUtilities.getLocalDate(this.defaultFinancialYearDate.endDate),
        });
      }
    }
  }

  subscribeDefaultAccountingPeriod(response) {
    if (response.AccountingPeriod) {
      this.defaultFinancialYearDate = {
        startDate: StaticUtilities.getLocalDate(response.AccountingPeriod.StartDate),
        endDate: StaticUtilities.getLocalDate(response.AccountingPeriod.EndDate)
      };
    }
  }

   //#region "onListRefresh"
   configDataEmit() {
    this.configData.emit(this.gainLossConfigForm.value);
  }
  //#endregion

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }

}

export interface IDefaultFinancialYearDate {
  startDate: any;
  endDate: any;
}
