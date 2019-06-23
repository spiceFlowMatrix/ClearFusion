import { Component, OnInit } from '@angular/core';
import { StoreService } from '../store.service';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../../code/code.service';
import { GLOBAL } from '../../../shared/global';
import { Router } from '@angular/router';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-procurment-summary',
  templateUrl: './procurment-summary.component.html',
  styleUrls: ['./procurment-summary.component.css']
})
export class ProcurmentSummaryComponent implements OnInit {
  //#region "Variables"

  keyValue: any;
  storeTabs = [
    {
      id: 1,
      text: 'Consumables'
    },
    {
      id: 2,
      text: 'Expendables'
    },
    {
      id: 3,
      text: 'Non-Expendables'
    }
  ];

  employeeList: any[];
  procurmentDataSource: ProcurmentSummaryModel[];
  procurementSummaryLoading = false;
  currencyDropdown: any[];
  selectedEmployeeId: any;
  selectedCurrency: any;
  isEditingAllowed = false;

  initialOfficeId: number;

  popoverData: any;

  // Flag
  defaultVisible = false;

  //#endregion

  constructor(
    private storeService: StoreService,
    private setting: AppSettingsService,
    private commonService: CommonService,
    public router: Router,
    private codeService: CodeService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.getAllEmployeeList();
    this.getCurrencyCodeList();
    this.commonService.getStoreOfficeId().subscribe(data => {
      this.getAllEmployeeList();
    });

    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.ProcurementSummary
    );
  }

  //#region "getAllEmployeeList"
  getAllEmployeeList() {
    this.showProcurementSummaryLoading();
    // tslint:disable-next-line:radix
    const OfficeId = parseInt(localStorage.getItem('STOREOFFICEID'));
    this.storeService
      .GetAllDetailsById(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetEmployeeDetailByOfficeId,
        'OfficeId',
        OfficeId
      )
      .subscribe(
        data => {
          this.employeeList = [];
          if (data.data.EmployeeDetailListData != null) {
            data.data.EmployeeDetailListData.forEach(element => {
              this.employeeList.push(element);
            });

            // sort in Asc
            this.employeeList = this.commonService.sortDropdown(
              this.employeeList,
              'CodeEmployeeName'
            );
          }
          this.hideProcurementSummaryLoading();
        },
        error => {}
      );
  }
  //#endregion

  //#region "employeeSelectedValue"
  onEmployeeSelectedValue(res) {
    if (res != null) {
      if (res.element.id === 'employeedropdown') {
        this.selectedEmployeeId = res.value;
      } else {
        this.selectedCurrency = res.value;
      }

      this.showProcurementSummaryLoading();

      this.storeService
        .GetProcurementSummary(
          this.setting.getBaseUrl() + GLOBAL.API_Store_GetProcurementSummary,
          this.selectedEmployeeId,
          this.selectedCurrency
        )
        .subscribe(
          data => {
            this.procurmentDataSource = [];
            if (data.data.ProcurmentSummaryModelList != null) {
              if (data.data.ProcurmentSummaryModelList.length > 0) {
                data.data.ProcurmentSummaryModelList.forEach(element => {
                  this.procurmentDataSource.push({
                    ProcurementId: element.ProcurementId,
                    ProcurementDate:
                      element.ProcurementDate != null
                        ? new Date(
                            new Date(element.ProcurementDate).getTime() -
                              new Date().getTimezoneOffset() * 60000
                          )
                        : null, // mandatory
                    EmployeeName: element.EmployeeName,
                    Store:
                      element.Store != null
                        ? this.storeTabs.filter(x => x.id === element.Store)[0]
                            .text
                        : null,
                    Inventory: element.Inventory,
                    Item: element.Item,
                    TotalCost: element.TotalCost,
                    MustReturn: element.MustReturn,
                    Returned: element.Returned,
                    TotalCostDetails: element.TotalCostDetails,
                    VoucherDate: element.VoucherDate,
                    VoucherNo: element.VoucherNo
                  });
                });
              }

              if (data.data.StatusCode === 400) {
                this.toastr.warning(data.data.Message);
              }
            }

            this.hideProcurementSummaryLoading();
          },
          error => {}
        );
    }
  }
  //#endregion

  NaviageToVoucherDetail(data) {
    this.router.navigate(['/dashboard/accounts/vouchers', data.data.VoucherNo]);
  }


  //#region ""
  toggleDefault(e) {
    if (e != null) {
      const keyValue = e.key.ProcurementId;
      this.keyValue = '#id' + e.key.ProcurementId; // for target

      this.popoverData = this.procurmentDataSource.filter(
        x => x.ProcurementId === keyValue
      )[0].TotalCostDetails;
    }

    this.defaultVisible = !this.defaultVisible;
  }
  //#endregion

  //#region "Loader"
  showProcurementSummaryLoading() {
    this.procurementSummaryLoading = true;
  }
  hideProcurementSummaryLoading() {
    this.procurementSummaryLoading = false;
  }
  //#endregion

  //#region "getCurrencyCodeList"
  getCurrencyCodeList() {
    this.codeService
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
}

class ProcurmentSummaryModel {
  ProcurementId: any;
  ProcurementDate: any;
  EmployeeName: any;
  Store: any;
  Inventory: any;
  Item: any;
  TotalCost: any;
  MustReturn: any;
  Returned: any;
  TotalCostDetails: TotalCostDetails;
  VoucherDate: any;
  VoucherNo: any;
}

class TotalCostDetails {
  UnitType: any;
  Amount: any;
  UnitCost: any;
  Currency: any;
}
