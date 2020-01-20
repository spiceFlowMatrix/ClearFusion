import { EmployeePensionService } from './../../services/employee-pension.service';
import { IDropDownModel } from './../../../store/models/purchase';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Observable, of, forkJoin, ReplaySubject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import {
  EmployeePensionReportModel,
  PensionReportModel,
  CurrencyCodeModel
} from '../../models/employee-pension.models';

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
  currencyData: CurrencyCodeModel;
  currencycodeList: CurrencyCodeModel[];

  pensionProfitTotal: any;
  pensionDeductionTotal: any;
  pensionTotal: any;
  previousPensionDeduction: any;
  previousPensionRate: any;
  previousProfit: any;
  previousTotal: any;

  currentYear: number[] = [];

  pensionList: EmployeePensionReportModel[];
  pensionReportList: PensionReportModel[] = [];

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService,
    private employeePensionService: EmployeePensionService,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.initModel();
    this.selectedFinancialYearPension = {
      value: 0,
      name: 'Financial Year'
    };
    this.selectedFinancialYearTax = { value: 0, name: 'Financial Year' };
    this.getCurrencyList();
    // this.getFinancialYearList();
    this.activeRoute.params.subscribe(params => {
      this.employeeId = params['id'];
    });

    forkJoin([this.getCurrencyList(), this.getFinancialYearList()])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeAllCurrency(result[0]);
        this.subscribeAllFinanacialYear(result[1]);
        this.getAllPensionList(
          this.currentYear,
          this.selectedCurrencyPension.value
        );
        // this.getAllSalaryTaxData(this.selectedFinancialYearTax.value[0], this.selectedCurrencyPension.value[0]);
        // this.getEmployeeTaxCalculation(this.employeeId, this.selectedFinancialYearTax.value[0]);
      });
  }

  initModel() {
    this.selectedCurrencyTax = {
      name: null,
      value: null
    };
    this.selectedCurrencyPension = {
      name: null,
      value: null
    };
    this.selectedFinancialYearPension = {
      name: null,
      value: null
    };
    this.selectedFinancialYearTax = {
      name: null,
      value: null
    };
  }

  //#region "get currency  List"
  getCurrencyList() {
    return this.employeePensionService.GetCurrencyList();
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
  }
  //#endregion

  subscribeAllFinanacialYear(response: any) {
    debugger
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
      this.financialYearList$.subscribe(
        element => {
          this.selectedFinancialYearTax = {
            value: element[0].value,
            name: element[0].name
          };
          this.selectedFinancialYearPension = {
            value: element[0].value,
            name: element[0].name
          };
        this.financialYearList$.subscribe(element=> {
          this.currentYear.push(element[0].value);
        });
        },
        error => {
          this.commonLoader.hideLoader();
        }
      );
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
    debugger;
    if (yearId != undefined && currencyId != undefined) {
      const model = {
        OfficeId: parseInt(localStorage.getItem('SelectedOfficeId')),
        EmployeeId: this.employeeId,
        FinancialYearId: yearId,
        CurrencyId: currencyId
      };
      this.employeePensionService.GetAllPensionList(model).subscribe(
        data => {
          if (data != null) {
            this.pensionList = [];
            this.pensionReportList = [];
            if (
              data.StatusCode === 200 &&
              data.data.EmployeePensionModel != null
            ) {
              data.data.EmployeePensionModel.EmployeePensionReportList.forEach(
                element => {
                  this.pensionList.push({
                    Year: new Date(
                      new Date(element.Date).getTime() -
                        new Date().getTimezoneOffset() * 60000
                    ).getFullYear(),
                    Date: new Date(
                      new Date(element.Date).getTime() -
                        new Date().getTimezoneOffset() * 60000
                    ),
                    GrossSalary: element.GrossSalary,
                    PensionDeduction: element.PensionDeduction.toFixed(4),
                    PensionRate: element.PensionRate,
                    Profit: element.Profit,
                    Total: element.Total.toFixed(4)
                  });
                }
              );
              this.pensionList.forEach(a => {
                if (
                  this.pensionReportList.filter(x => x.Year === a.Year)
                    .length === 0
                ) {
                  this.pensionReportList.push({
                    Year: a.Year,
                    PensionReportList: [
                      {
                        Date: a.Date,
                        GrossSalary: a.GrossSalary,
                        PensionDeduction: a.PensionDeduction,
                        PensionRate: a.PensionRate,
                        Profit: a.Profit,
                        Total: a.Total
                      }
                    ]
                  });
                } else {
                  this.pensionReportList
                    .filter(x => x.Year === a.Year)[0]
                    .PensionReportList.push({
                      Date: a.Date,
                      GrossSalary: a.GrossSalary,
                      PensionDeduction: a.PensionDeduction,
                      PensionRate: a.PensionRate,
                      Profit: a.Profit,
                      Total: a.Total
                    });
                }
              });
              if (
                data.data.EmployeePensionModel.EmployeePensionReportList[0] !=
                  null &&
                data.data.EmployeePensionModel.EmployeePensionReportList[0] !==
                  0
              ) {
                const currencyId =
                  data.data.EmployeePensionModel.EmployeePensionReportList[0]
                    .CurrencyId;
                this.currencyData = this.currencycodeList.filter(
                  x => x.CurrencyId === currencyId
                )[0];
              }

              this.pensionProfitTotal = data.data.EmployeePensionModel.PensionProfitTotal.toFixed(
                4
              );
              this.pensionDeductionTotal = data.data.EmployeePensionModel.PensionDeductionTotal.toFixed(
                4
              );
              this.pensionTotal = data.data.EmployeePensionModel.PensionTotal.toFixed(
                4
              );
              this.previousPensionDeduction =
                data.data.EmployeePensionModel.PreviousPensionDeduction;
              this.previousPensionRate =
                data.data.EmployeePensionModel.PreviousPensionRate;
              this.previousProfit =
                data.data.EmployeePensionModel.PreviousProfit;
              this.previousTotal = data.data.EmployeePensionModel.PreviousTotal;

              // this.showHidePensionInfoLoading(false);
            } else if (
              data.StatusCode === 200 &&
              data.data.EmployeePensionModel == null
            ) {
              this.toastr.info(data.Message);
              // this.showHidePensionInfoLoading(false);
            } else if (data.StatusCode === 400) {
              this.toastr.error(data.Message);
              // this.showHidePensionInfoLoading(false);
            }
          }
          // this.showHidePensionInfoLoading(false);
        },
        error => {
          // this.showHideSalaryTaxInfoLoading(false);
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
            this.toastr.error('Something went wrong!');
          }
        }
      );
    }
  }
  //#endregion
}
