import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../accounts.service';
import { CodeService } from '../../code/code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-currency-exchange-analysis',
  templateUrl: './currency-exchange-analysis.component.html',
  styleUrls: ['./currency-exchange-analysis.component.css']
})
export class CurrencyExchangeAnalysisComponent implements OnInit {
  //#region "Variables"
  currencyExchangeFilter: any;
  currencyAnalysisDataSource: any[];
  officeDropdown: any[];
  journalDropdown: any[];
  currencyDropdown: any[];
  voucherDropdown: any[];
  voucherFilteredDropdown: any[];
  projectDropdown: any[];
  trialBalanceDateRange: any[];

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
    this.getOfficeCodeList();
    this.getJournalDropdownList();
    this.getCurrencyCodeList();
    this.getAllVoucherDetails();
    this.getAllProjectDetails();
  }
  initializeForm() {
    this.currencyExchangeFilter = {};
  }

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

  //#region  "Get all Journal Dropdown"
  getJournalDropdownList() {
    // this.journalCodeListLoading = true;
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_JournalCode_GetAllJournalDetail
      )
      .subscribe(
        data => {
          this.journalDropdown = [];

          if (
            data.StatusCode === 200 &&
            data.data.JournalDetailList.length > 0
          ) {
            data.data.JournalDetailList.forEach(element => {
              this.journalDropdown.push({
                JournalCode: element.JournalCode,
                JournalName: element.JournalName
              });
            });

            // sort in Asc
            this.journalDropdown = this.commonService.sortDropdown(
              this.journalDropdown,
              'JournalName'
            );
          }
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
        }
      );
  }
  //#endregion

  //#region  "Get all Voucher details"
  getAllVoucherDetails() {
    this.accountservice
      .GetAllVoucherDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAllVoucherDetails
      )
      .subscribe(
        data => {
          this.voucherDropdown = [];
          if (data.data.VoucherDetailList != null) {
            data.data.VoucherDetailList.forEach(element => {
              this.voucherDropdown.push(element);
            });
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

  //#region  "getAllProjectDetails"
  getAllProjectDetails() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_GetAllProjectDetail
      )
      .subscribe(
        data => {
          this.projectDropdown = [];
          if (
            data.data.ProjectDetailList != null &&
            data.data.ProjectDetailList.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.ProjectDetailList.forEach(element => {
              this.projectDropdown.push(element);
            });
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

  //#region "onFieldValueChanged"
  onFieldDataChanged(e: any) {
    if (e != null) {
      if (e.dataField === 'Journal' && e.value != null) {
        if (this.voucherDropdown != null) {
          this.voucherFilteredDropdown = [];
          this.voucherFilteredDropdown = this.voucherDropdown.filter(
            x => x.JournalCode === e.value
          );
        }
      }
    }
  }
  //#endregion
}
