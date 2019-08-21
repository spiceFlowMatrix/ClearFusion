import { Component, OnInit } from '@angular/core';
import { CurrencyData, CodeService } from '../code.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { CurrencyCode } from '../../../model/code-model';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-currency-code',
  templateUrl: './currency-code.component.html',
  styleUrls: ['./currency-code.component.css']
})
export class CurrencyCodeComponent implements OnInit {
  showFilterRow: boolean;
  popupVisible = false;
  currencydata: CurrencyData;
  currencycodedt: CurrencyCode[];
  isEditingAllowed = false;

  // loader
  currencyCodeListLoading = false;
  currencyCodePopupLoading = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private codeservice: CodeService,
    private commonservice: CommonService
  ) {
    this.initializeForm();
    this.showFilterRow = true;
  }

  ngOnInit() {
    this.getCurrencyCodeList();
    this.initializeForm();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.JournalCodes
    );
  }

  initializeForm() {
    this.currencydata = {
      CurrencyCode: null,
      CurrencyId: null,
      CurrencyName: null
    };
  }

  showPopup() {
    this.initializeForm();
    this.popupVisible = true;
  }

  HidePopup() {
    this.popupVisible = false;
  }

  // Get all Currency Details
  getCurrencyCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencycodedt = [];
          if (data.StatusCode === 200 && data.data.CurrencyList.length > 0) {
            data.data.CurrencyList.forEach(element => {
              this.currencycodedt.push({
                CurrencyId: element.CurrencyId,
                CurrencyCode: element.CurrencyCode,
                CurrencyName: element.CurrencyName
              });
            });
          }
          this.commonservice.setLoader(false);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
          this.commonservice.setLoader(false);
        }
      );
  }

  onFormSubmit(model) {
    if (model.CurrencyId == null) {
      this.AddCurrencyCode(model);
    } else {
      this.EditCurrencyCode(model);
    }
  }

  AddCurrencyCode(model) {
    this.currencyCodePopupLoading = true;
    const addcurrencycode: CurrencyCode = {
      CurrencyId: 0,
      CurrencyCode: model.CurrencyCode,
      CurrencyName: model.CurrencyName
    };
    this.codeservice
      .AddEditCurrencyCode(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_AddCurrency,
        addcurrencycode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Currency Code Added Successfully!!!');
            this.getCurrencyCodeList();
            this.initializeForm();
            this.HidePopup();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Currency Code already exist!!!');
          } else {
            this.toastr.error(data.Message);
          }
          this.currencyCodePopupLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  EditCurrencyCode(model) {
    this.currencyCodePopupLoading = true;
    const editcurrencycode: CurrencyCode = {
      CurrencyId: model.CurrencyId,
      CurrencyCode: model.CurrencyCode,
      CurrencyName: model.CurrencyName
    };

    this.codeservice
      .AddEditCurrencyCode(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCode_EditCurrency,
        editcurrencycode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.HidePopup();
            this.toastr.success('Currency Code Updated Successfully!!!');
            this.getCurrencyCodeList();
            this.initializeForm();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Currency Code already exist!!!');
          } else {
            this.toastr.error(data.Message);
          }
          this.currencyCodePopupLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  GetCurrencyDetailByCode(data) {
    this.currencydata.CurrencyId = data.data.CurrencyId;
    this.currencydata.CurrencyCode = data.data.CurrencyCode;
    this.currencydata.CurrencyName = data.data.CurrencyName;
    this.popupVisible = true;
  }
}
