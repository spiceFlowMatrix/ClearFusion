import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IResponseData } from '../../vouchers/models/status-code.model';
import { IExchangeRateAddModel } from '../models/exchange-rate.model';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import { ExchangeRateService } from '../exchange-rate-listing/exchange-rate.service';
import {STEPPER_GLOBAL_OPTIONS} from '@angular/cdk/stepper';
import { CommonLoaderService } from '../../../../shared/common-loader/common-loader.service';

@Component({
  selector: 'app-exchange-rate-add',
  templateUrl: './exchange-rate-add.component.html',
  styleUrls: ['./exchange-rate-add.component.scss'],
  providers: [{
    provide: STEPPER_GLOBAL_OPTIONS, useValue: {showError: true}
  }]
})
export class ExchangeRateAddComponent implements OnInit {

  exchangeRateAddModel: FormGroup;
  exchangeRateGenerateModel: FormGroup;
  addExchangeRateLoader = false;
  onListRefresh = new EventEmitter();
  CurrencyList: any[] = [];
  selectedCurrencyId: number;
  selectedCurrencyName: string;

  constructor(private fb: FormBuilder,
    private exchangeRateService: ExchangeRateService,
    public dialogRef: MatDialogRef<ExchangeRateAddComponent>,
    public commonLoader: CommonLoaderService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public toastr: ToastrService) {
      this.CurrencyList = this.data.currencyList;
      this.FormInitialize();
    }

  ngOnInit() {

  }

  FormInitialize() {
    this.exchangeRateAddModel = this.fb.group({
      CurrencyId: ['', [Validators.required]],
      Date: ['', [Validators.required]],
    });

    this.exchangeRateGenerateModel = this.fb.group({
      Currencies: this.fb.array([], Validators.required)
    });

    this.addCurrency();
  }

  get formData() { return this.exchangeRateGenerateModel.get('Currencies'); }

  addCurrency() {
    // add Currency to the list
      const control = <FormArray>this.exchangeRateGenerateModel.controls.Currencies;

      this.CurrencyList.forEach(x => {
        control.push(
          this.fb.group({
          CurrencyId: x.CurrencyId,
          CurrencyName: x.CurrencyName,
          Rate: new FormControl(null, Validators.required),
          FromCurrency:  null,
          }));
      });
}
  //#region "addExchangeRate"
  addExchangeRate(data: IExchangeRateAddModel) {

  }
  //#endregion

  ExchangeRateGenerationStepChanged(event: any) {
    const currencyDetail = this.CurrencyList.find(x => x.CurrencyId === this.exchangeRateAddModel.value.CurrencyId);
    this.selectedCurrencyId = currencyDetail.CurrencyId;
    this.selectedCurrencyName = currencyDetail.CurrencyName;
  }

  SaveSelectedCurrencyExchangeRates() {
    if (this.exchangeRateAddModel.valid && this.exchangeRateGenerateModel.valid) {

      this.addExchangeRateLoader = true;

      const exchangeRateData: any[] = [];

      this.exchangeRateGenerateModel.value.Currencies.forEach(x =>
        exchangeRateData.push({
          FromCurrencyId: this.exchangeRateAddModel.value.CurrencyId,
          ToCurrencyId: x.CurrencyId,
          Rate: x.Rate,
          Date: this.exchangeRateService.getLocalDate(this.exchangeRateAddModel.value.Date)
        }));


      this.exchangeRateService.AddExchangeRate(exchangeRateData).subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            this.dialogRef.close(true);
            this.exchangeRateListRefresh();
            this.toastr.success('Added Exchange Rate successfully');
          } else {
            this.toastr.error(response.message);
          }
          this.addExchangeRateLoader = false;
        },
        (error) => {
          this.toastr.error('Someting went wrong');
          this.addExchangeRateLoader = false;
        }
      );
    }
  }

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close(false);
  }
  //#endregion

  //#region "onListRefresh"
  exchangeRateListRefresh() {
    this.onListRefresh.emit();
  }
  //#endregion

}
