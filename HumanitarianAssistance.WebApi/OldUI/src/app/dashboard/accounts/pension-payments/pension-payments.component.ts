import { Component, OnInit } from '@angular/core';
import { HrService } from '../../hr/hr.service';
import { CodeService } from '../../code/code.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import { AccountsService } from '../../accounts/accounts.service';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { Subscription } from 'rxjs/Subscription';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-pension-payments',
  templateUrl: './pension-payments.component.html',
  styleUrls: ['./pension-payments.component.css']
})
export class PensionPaymentsComponent implements OnInit {
  employeePensionDataSource: EmployeePensionModel[];
  employeePensionForm: EmployeePensionPopUpFormModel;
  levelFourAccounts: any;
  journalDropdown: any[];
  voucherTypeArr: any;
  subscription: Subscription;
  officecodelist: any[]= [];

  currencyDataSource: CurrencyCode[];
  employeeListDataSource: EmployeeListModel[];
  voucherDropdown: any[];

  pensionDateSelectedValue = new Date();
  employeeSelectedValue: any;
  pensionHistoryDataSource: any[];
  isEditingAllowed = false;
  selectedOffice: any;

  // popup
  payPensionPopupVisible = false;
  showPensionHistoryPopUp = false;

  // loader
  pensionLoader = false;
  addEmpPensionPopupLoading = false;

  currentDate = new Date();
  //#endregion

  constructor(
    private hrService: HrService,
    private codeService: CodeService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService,
    private accountservice: AccountsService
  ) {
    this.voucherTypeArr = [
      { VoucherTypeId: 1, VoucherTypeName: 'Journal' },
      { VoucherTypeId: 2, VoucherTypeName: 'Adjustment' }
    ];
  }

  ngOnInit() {
    this.initializeForm();
    this.getOfficeCodeList();
    this.getCurrencyCodeList();
    this.getLevelFourAccountDetails();
    this.getJournalDropdownList();
    // this.getAllEmployeeListByOfficeId();
    //this.getAllEmployeePension();
    // this.commonService.getAccountingOfficeId().subscribe(data => {
    //   this.getAllEmployeePension();
    //   this.getAllEmployeeListByOfficeId();
    // });

    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.JournalCodes
    );
  }

  initializeForm() {
    this.employeePensionForm = {
      CreditAccount: null,
      CurrencyId: null,
     // DebitAccount: null,
      EmployeeId: null,
      EmployeeName: null,
      OfficeId: null,
      PensionAmount: null,
      JournalCode: null,
      VoucherTypeId: null,
      PensionBalanceAmount: null,
      TimezoneOffset: null
    };
  }

  //#region "Get All Employee Advances"
  getAllEmployeePension(officeId: number) {
    this.pensionLoader = true;

    // tslint:disable-next-line:radix
    //const officeId = parseInt(localStorage.getItem('ACCOUNTINGOFFICEID'));
    this.hrService
      .GetAllDetail(
        this.setting.getBaseUrl() + GLOBAL.API_EmployeeHR_GetAllEmployeePension,
        officeId
      )
      .subscribe(
        data => {
          this.employeePensionDataSource = [];
          if (data.StatusCode === 200 && data.data.PensionPayment != null) {
            if (data.data.PensionPayment.length > 0) {
              data.data.PensionPayment.forEach(element => {
                this.employeePensionDataSource.push({
                  EmployeeId: element.EmployeeId,
                  EmployeeName: element.EmployeeName,
                  EmployeeCode: element.EmployeeCode,
                  CurrencyId: element.CurrencyId,
                  TotalPensionAmount: element.TotalPensionAmount,
                  PensionAmountPaid: element.PensionAmountPaid,
                  OfficeId: element.OfficeId,
                  BalancePensionAmount:
                    element.TotalPensionAmount - element.PensionAmountPaid
                });
              });
            }
          } else {
          }
          this.pensionLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.pensionLoader = false;
        }
      );
  }
  //#endregion

  //#region "Get all Office Details"
  getOfficeCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          const AllOffices = localStorage.getItem('ALLOFFICES').split(',');
          this.officecodelist = [];
          if (
            data.StatusCode === 200 &&
            data.data.OfficeDetailsList.length > 0
          ) {
            data.data.OfficeDetailsList.forEach(element => {
              const officeFound = AllOffices.indexOf('' + element.OfficeId);
              if (officeFound !== -1) {
                this.officecodelist.push({
                  OfficeId: element.OfficeId,
                  OfficeCode: element.OfficeCode,
                  OfficeName: element.OfficeName,
                  SupervisorName: element.SupervisorName,
                  PhoneNo: element.PhoneNo,
                  FaxNo: element.FaxNo,
                  OfficeKey: element.OfficeKey
                });
              }
            });
            // tslint:disable-next-line:radix
            this.selectedOffice = this.selectedOffice === undefined ? this.officecodelist[0].OfficeId : this.selectedOffice;

            this.getAllEmployeePension(this.selectedOffice);
            this.getAllEmployeeListByOfficeId(this.selectedOffice);

            // let officeId = localStorage.getItem('ACCOUNTINGOFFICEID');

            // if(officeId ==null || officeId == undefined){
            //   this.commonService.setAccountingOfficeId(this.selectedOffice);
            //   this.getAllEmployeePension();
            // }
            // //
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

  //#region  "Get all Currency Details"
  getCurrencyCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyDataSource = [];
          if (data.data.CurrencyList != null) {
            if (data.data.CurrencyList.length > 0) {
              data.data.CurrencyList.forEach(element => {
                this.currencyDataSource.push(element);
              });

              // sort in Asc
              this.commonService.sortDropdown(
                this.currencyDataSource,
                'CurrencyName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region  "Get all Voucher details"
  getAllVoucherDetails() {
    this.codeService
      .GetAllDetails(
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
        error => {}
      );
  }
  //#endregion

  //#region "Get All Employee List By OfficeId"
  getAllEmployeeListByOfficeId(officeId:number) {
    // tslint:disable-next-line:radix
    // const officeId = parseInt(localStorage.getItem('ACCOUNTINGOFFICEID'));
    this.hrService
      .GetAllDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetEmployeeDetailByOfficeId,
        officeId
      )
      .subscribe(
        data => {
          this.employeeListDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeDetailListData != null &&
            data.data.EmployeeDetailListData.length > 0
          ) {
            data.data.EmployeeDetailListData.forEach(element => {
              this.employeeListDataSource.push(element);
            });

            this.commonService.sortDropdown(
              this.employeeListDataSource,
              'CodeEmployeeName'
            );
          } else {
            // tslint:disable-next-line:curly
            if (data.data.EmployeeDetailListData == null)
              this.toastr.warning('No record found!');
            // tslint:disable-next-line:curly
            else if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');
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

  //#region "Add Employee Advances"
  AddEmployeePensionPayment(model: EmployeePensionPopUpFormModel) {
    if (model.PensionAmount <= model.PensionBalanceAmount) {
      const pensionModel: EmployeePensionPopUpFormModel = {
        EmployeeId: model.EmployeeId,
        CurrencyId: model.CurrencyId,
        CreditAccount: model.CreditAccount,
       // DebitAccount: model.DebitAccount,
        PensionAmount: model.PensionAmount,
        // tslint:disable-next-line:radix
        OfficeId: this.selectedOffice,
        EmployeeName: model.EmployeeName,
        JournalCode: model.JournalCode,
        VoucherTypeId: model.VoucherTypeId,
        PensionBalanceAmount: model.PensionBalanceAmount,
        TimezoneOffset: new Date().getTimezoneOffset()
      };

      this.addEmpPensionPopupLoading = true;

      this.codeService
        .AddEditDetails(
          this.setting.getBaseUrl() +
            GLOBAL.API_Account_AddEmployeePensionPayment,
          pensionModel
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success(
                'Pension Payment Voucher Created Successfully !'
              );
              this.hideEmpPensionAddPopup();
              this.getAllEmployeePension(this.selectedOffice);
            } else {
              if (data.StatusCode === 400) {
                this.toastr.error(data.Message);
              } else {
                this.toastr.error('Something went wrong !');
              }
            }
            this.addEmpPensionPopupLoading = false;
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
            }
            this.hideEmpPensionAddPopup();
            this.addEmpPensionPopupLoading = false;
          }
        );
    } else {
      this.toastr.error('Pension amount exceeded balance pension amount');
      this.employeePensionForm.PensionAmount = 0;
    }
  }
  //#endregion

  onShowEmployeePensionHistoryPopUp(model: any) {
    if (model) {
      this.onShowPensionHistoryPopUp();
      this.showPensionLoader();
      const employeeId: number = model.key.EmployeeId;
      const officeId = this.selectedOffice;
      this.pensionHistoryDataSource = [];
      this.hrService
        .GetDataByEmployeeIdAndOfficeId(
          this.setting.getBaseUrl() +
            GLOBAL.API_EmployeeHR_GetEmployeePensionHistoryDetail,
          employeeId,
          officeId
        )
        .subscribe(
          data => {
            if (
              data.StatusCode === 200 &&
              data.data.PensionPaymentHistory != null &&
              data.data.PensionPaymentHistory.length > 0
            ) {
              data.data.PensionPaymentHistory.forEach(element => {
                this.pensionHistoryDataSource.push(element);
              });

              this.commonService.sortDropdown(
                this.pensionHistoryDataSource,
                'PaymentDate'
              );
              this.hidePensionLoader();
            } else {
              // tslint:disable-next-line:curly
              if (data.StatusCode === 400)
                this.toastr.error('Something went wrong!');
                this.hidePensionLoader();
                this.pensionHistoryDataSource= null;
            }
            this.hidePensionLoader();
          },
          error => {
            if (error.StatusCode === 500) {
              this.toastr.error('Internal Server Error....');
              this.hidePensionLoader();
            } else if (error.StatusCode === 401) {
              this.toastr.error('Unauthorized Access Error....');
              this.hidePensionLoader();
            } else if (error.StatusCode === 403) {
              this.toastr.error('Forbidden Error....');
              this.hidePensionLoader();
            }
          }
        );
    }
  }

  //#region "getLevelFourAccountDetails"
  getLevelFourAccountDetails() {
    this.accountservice
      .GetAccountDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetLevelFourAccountDetails
      )
      .subscribe(
        data => {
          this.levelFourAccounts = [];

          if (data.StatusCode === 200) {
            if (data.data.AccountDetailList != null) {
              data.data.AccountDetailList.forEach(element => {
                this.levelFourAccounts.push({
                  AccountCode: element.AccountCode,
                  AccountName: element.AccountName
                });
              });

              // Sort in Ascending order
              this.commonService.sortDropdown(
                this.levelFourAccounts,
                'AccountName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getJournalDropdownList"
  getJournalDropdownList() {
    this.codeService
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

            this.commonService.sortDropdown(
              this.journalDropdown,
              'JournalName'
            );
          }
        },
        error => {}
      );
  }
  //#endregion

  onShowPensionHistoryPopUp() {
    this.showPensionHistoryPopUp = true;
  }

  onHidePensionHistoryPopUp() {
    this.showPensionHistoryPopUp = false;
  }

  showEmpPensionPopup(data) {
    this.initializeForm();
    this.employeeListDataSource;
    this.employeePensionForm = {
      EmployeeId: data.key.EmployeeId,
      EmployeeName: data.key.EmployeeName,
      CurrencyId: data.key.CurrencyId,
      PensionBalanceAmount: data.key.BalancePensionAmount,
      CreditAccount: null,
     // DebitAccount: null,
      OfficeId: null,
      PensionAmount: null,
      JournalCode: null,
      VoucherTypeId: null,
      TimezoneOffset: null
    };

    this.employeeSelectedValue = null;
    this.payPensionPopupVisible = true;
  }

  hideEmpPensionAddPopup() {
    this.payPensionPopupVisible = false;
  }

  // Loader

  showPensionLoader() {
    this.pensionLoader = true;
  }
  hidePensionLoader() {
    this.pensionLoader = false;
  }

  onOffEmpAdvancesAddPopupLoading() {
    this.addEmpPensionPopupLoading = !this.addEmpPensionPopupLoading;
  }

  //#region "on office Selected"
  onOfficeSelected(event) {
    // this.loading = true;
    // this.commonServices.setLoader(true);
    this.getAllEmployeeListByOfficeId(event);
    this.getAllEmployeePension(event);
  }
  //#endregion
}

//#region "classes"
class EmployeePensionPopUpFormModel {
  EmployeeId: number;
  EmployeeName: string;
  CurrencyId: number;
  PensionAmount: number;
  // DebitAccount: any;
  CreditAccount: any;
  OfficeId: number;
  JournalCode: any;
  VoucherTypeId: number;
  PensionBalanceAmount: number;
  TimezoneOffset: any;
}

class EmployeePensionModel {
  EmployeeId: number;
  EmployeeName: string;
  EmployeeCode: any;
  CurrencyId: any;
  OfficeId: number;
  TotalPensionAmount: number;
  PensionAmountPaid: number;
  BalancePensionAmount: number;
}

class CurrencyCode {
  CurrencyId: number;
  CurrencyCode: string;
  CurrencyName: string;
  CurrencyRate: DoubleRange;
}

class EmployeeListModel {
  EmployeeId: any;
  EmployeeName: any;
  EmployeeCode: any;
  CodeEmployeeName: any;
}

//#endregion
