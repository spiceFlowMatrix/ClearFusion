import { EmployeePensionService } from './../../services/employee-pension.service';
import { IDropDownModel } from './../../../store/models/purchase';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-employee-pension',
  templateUrl: './employee-pension.component.html',
  styleUrls: ['./employee-pension.component.scss']
})
export class EmployeePensionComponent implements OnInit {
  pensionListHeaders$ = of([
    'Date',
    'Gross Salary',
    'Pension Rate',
    'Pension Deducation',
    'Profit',
    'Total'
  ]);
  taxListHeaders$ = of(['Date', 'Currency', 'Office', 'Total Tax']);
  pensionList$: Observable<any[]>;
  taxList$: Observable<any[]>;
  selectedCurrencyTax: IDropDownModel;
  selectedCurrencyPension: IDropDownModel;
  selectedFinancialYearPension: IDropDownModel;
  selectedFinancialYearTax: IDropDownModel;
  currencyList$: Observable<IDropDownModel[]>;
  financialYearList$: Observable<IDropDownModel[]>;
  constructor(
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService,
    private employeePensionService: EmployeePensionService
  ) {
    this.selectedCurrencyTax = { value: 0, name: 'Display Currency' };
    this.selectedCurrencyPension = { value: 0, name: 'Display Currency' };
    this.selectedFinancialYearTax = { value: 0, name: 'Financial Year' };
    this.selectedFinancialYearPension = { value: 0, name: 'Financial Year' };
  }

  ngOnInit() {
    this.getCurrencyList();
    this.getFinancialYearList();
  }
  //#region "get currency  List"
  getCurrencyList() {
    this.commonLoader.showLoader();
    this.employeePensionService.GetCurrencyList().subscribe(
      x => {
        this.commonLoader.hideLoader();
        if (x.data.CurrencyList.length > 0) {
          this.currencyList$ = of(
            x.data.CurrencyList.map(y => {
              return {
                value: y.CurrencyId,
                name: y.CurrencyName
              } as IDropDownModel;
            })
          );
        }
      },
      () => {
        this.commonLoader.hideLoader();
      }
    );
  }
  //#endregion

  //#region "get Financial Year List"
  getFinancialYearList() {
    this.commonLoader.showLoader();
    this.employeePensionService.GetFinancialYearList().subscribe(
      x => {
        this.commonLoader.hideLoader();
        if (x.data.FinancialYearDetailList.length > 0) {
          this.financialYearList$ = of(
            x.data.FinancialYearDetailList.map(y => {
              return {
                value: y.FinancialYearId,
                name: y.FinancialYearName
              } as IDropDownModel;
            })
          );
        }
      },
      () => {
        this.commonLoader.hideLoader();
      }
    );
  }
  //#endregion

  selectedCurrencyChangedForTax(taxCurrency: any) {
    this.selectedCurrencyTax = {
      value: taxCurrency.value,
      name: taxCurrency.name
    };
  }
  selectedCurrencyChangedForPension(pensionCurrency: any) {
    this.selectedCurrencyPension = {
      value: pensionCurrency.value,
      name: pensionCurrency.name
    };
  }
  selectedFinancialYearForTax(financialYear: any) {
    this.selectedFinancialYearTax = {
      value: financialYear.value,
      name: financialYear.name
    };
  }
  selectedFinancialYearForPension(financialYear: any) {
    this.selectedFinancialYearPension = {
      value: financialYear.value,
      name: financialYear.name
    };
  }
}
