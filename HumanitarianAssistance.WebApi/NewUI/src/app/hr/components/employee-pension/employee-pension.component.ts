import { EmployeePensionService } from './../../services/employee-pension.service';
import { IDropDownModel } from './../../../store/models/purchase';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Observable, of, forkJoin, ReplaySubject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-employee-pension',
  templateUrl: './employee-pension.component.html',
  styleUrls: ['./employee-pension.component.scss']
})
export class EmployeePensionComponent implements OnInit {
  // dataSource
  pensionListHeaders$ = of([
    'Date',
    'Gross Salary',
    'Pension Rate',
    'Pension Deducation',
    'Profit',
    'Total'
  ]);
  taxListHeaders$ = of(['Date', 'Currency', 'Office', 'Total Tax']);

  // variables
  pensionList$: Observable<any[]>;
  taxList$: Observable<any[]>;
  selectedCurrencyTax: IDropDownModel;
  selectedCurrencyPension: IDropDownModel;
  selectedFinancialYearPension: IDropDownModel;
  selectedFinancialYearTax: IDropDownModel;
  currencyList$: Observable<IDropDownModel[]>;
  financialYearList$: Observable<IDropDownModel[]>;
  employeeId: number;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService,
    private employeePensionService: EmployeePensionService,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.getCurrencyList();
    this.getFinancialYearList();
    this.activeRoute.params.subscribe(params => {
      this.employeeId = params['id'];
    });

    forkJoin([this.getCurrencyList(), this.getFinancialYearList()])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeAllCurrency(result[0]);
        this.subscribeAllFinanacialYear(result[1]);
      });
    // this.getAllPensionList(data.FinancialYearId, data.CurrencyId);
    // this.getAllSalaryTaxData(data.FinancialYearId, data.CurrencyId);
    // this.getEmployeeTaxCalculation(this.employeeId, data.FinancialYearId);
  }
  //#region "get currency  List"
  getCurrencyList() {
    return this.employeePensionService.GetCurrencyList();
    // debugger;
    // this.commonLoader.showLoader();
    // this.employeePensionService.GetCurrencyList().subscribe(
    //   x => {
    //     this.commonLoader.hideLoader();
    //     if (x.data.CurrencyList.length > 0) {
    //       this.currencyList$ = of(
    //         x.data.CurrencyList.map(y => {
    //           return {
    //             value: y.CurrencyId,
    //             name: y.CurrencyName
    //           } as IDropDownModel;
    //         })
    //       );
    //       this.currencyList$.subscribe(element => {
    //         // pre selected currency
    //         this.selectedCurrencyTax = {
    //           value: element[0].value,
    //           name: element[0].name
    //         };
    //         this.selectedCurrencyPension = {
    //           value: element[0].value,
    //           name: element[0].name
    //         };
    //       });
    //     }
    //   },
    //   () => {
    //     this.commonLoader.hideLoader();
    //   }
    // );
  }
  //#endregion
  subscribeAllCurrency(response: any) {
    this.currencyList$ = of(
      response.data.CurrencyList.map(y => {
        return {
          value: y.CurrencyId,
          name: y.CurrencyName
        } as IDropDownModel;
      })
    );
    this.currencyList$.subscribe(element => {
      // pre selected currency
      this.selectedCurrencyTax = {
        value: element[0].value,
        name: element[0].name
      };
      this.selectedCurrencyPension = {
        value: element[0].value,
        name: element[0].name
      };
    });
  }

  //#region "get Financial Year List"
  getFinancialYearList() {
    this.commonLoader.showLoader();
    return this.employeePensionService.GetFinancialYearList();
    // this.employeePensionService.GetFinancialYearList().subscribe(
    //   x => {
    //     this.commonLoader.hideLoader();
    //     if (
    //       x.data.FinancialYearDetailList !== undefined &&
    //       x.data.FinancialYearDetailList.length > 0
    //     ) {
    //       this.financialYearList$ = of(
    //         x.data.FinancialYearDetailList.map(y => {
    //           return {
    //             value: y.FinancialYearId,
    //             name: y.FinancialYearName
    //           } as IDropDownModel;
    //         })
    //       );
    //       this.financialYearList$.subscribe(element => {
    //         this.selectedFinancialYearTax = {
    //           value: element[0].value,
    //           name: element[0].name
    //         };
    //         this.selectedFinancialYearPension = {
    //           value: element[0].value,
    //           name: element[0].name
    //         };
    //       });
    //     } else {
    //       this.selectedFinancialYearPension = {
    //         value: 0,
    //         name: "Financial Year"
    //       };
    //       this.selectedFinancialYearTax = { value: 0, name: "Financial Year" };
    //     }
    //   },
    //   error => {
    //     this.commonLoader.hideLoader();
    //   }
    // );
  }
  //#endregion

  subscribeAllFinanacialYear(response: any) {
    if (
      response.data.FinancialYearDetailList !== undefined &&
      response.data.FinancialYearDetailList.length > 0
    ) {
      this.financialYearList$ = of(
        response.data.FinancialYearDetailList.map(y => {
          return {
            value: y.FinancialYearId,
            name: y.FinancialYearName
          } as IDropDownModel;
        })
      );
      this.commonLoader.hideLoader();
      this.financialYearList$.subscribe(element => {
        this.selectedFinancialYearTax = {
          value: element[0].value,
          name: element[0].name
        };
        this.selectedFinancialYearPension = {
          value: element[0].value,
          name: element[0].name
        };
      }, error => {
        this.commonLoader.hideLoader();
      });
    } else {
      this.selectedFinancialYearPension = {
        value: 0,
        name: 'Financial Year'
      };
      this.selectedFinancialYearTax = { value: 0, name: 'Financial Year' };
    }
  }
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

  //#region "getAllPensionList"
  getAllPensionList(yearId: number[], currencyId: number) {
    const model = {
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'), 32),
      EmployeeId: this.employeeId,
      FinancialYearId: yearId,
      CurrencyId: currencyId
    };
  }
  //#endregion
}
