import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../../accounts/accounts.service';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../../code/code.service';
import { GLOBAL } from '../../../shared/global';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-advance-deduction',
  templateUrl: './advance-deduction.component.html',
  styleUrls: ['./advance-deduction.component.css']
})
export class AdvanceDeductionComponent implements OnInit {
  //#region "variables"
  advanceDeductionFilter: any;
  advanceDeductionDataSource: any[];
  officeDropdown: any[];
  currencyDropdown: any[];

  recordTypeDropdown = [
    { Id: 1, Name: 'Single' },
    { Id: 2, Name: 'Consolidate' }
  ];

  //#endregion

  constructor(
    private accountservice: AccountsService,
    private codeservice: CodeService,
    private setting: AppSettingsService,
    private commonService: CommonService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.initializeForm();
    this.getCurrencyCodeList();
    this.getOfficeCodeList();
  }

  //#region "initializeForm"
  initializeForm() {
    this.advanceDeductionFilter = {};
  }
  //#endregion

  //#region  "Get Office Code"
  getOfficeCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          if (data.data.OfficeDetailsList != null) {
            if (data.data.OfficeDetailsList.length > 0) {
              this.officeDropdown = [];

              const allOffices = [];
              const officeIds: any[] =
                localStorage.getItem('ALLOFFICES') != null
                  ? localStorage.getItem('ALLOFFICES').split(',')
                  : null;

              data.data.OfficeDetailsList.forEach(element => {
                allOffices.push(element);
              });

              officeIds.forEach(x => {
                const officeData = allOffices.filter(
                  // tslint:disable-next-line:radix
                  e => e.OfficeId === parseInt(x)
                )[0];
                this.officeDropdown.push(officeData);
              });

              // sort in Asc
              this.officeDropdown = this.commonService.sortDropdown(
                this.officeDropdown,
                'OfficeName'
              );
            }
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region "getCurrencyCodeList"
  getCurrencyCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyDropdown = [];
          if (data.data.CurrencyList != null) {
            if (data.data.CurrencyList.length > 0) {
              data.data.CurrencyList.forEach(element => {
                this.currencyDropdown.push(element);
              });

              // sort in Asc
              this.currencyDropdown = this.commonService.sortDropdown(
                this.currencyDropdown,
                'CurrencyName'
              );
            }
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region "onApplyingFilter"
  onApplyingFilter(data: any) {}
  //#endregion

  //#region "showAdvanceDeductionEditPopup"
  showAdvanceDeductionEditPopup(data: any) {}
  //#endregion
}
