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
  EmployeePensionReportPdfModel,
  EmployeeTaxReportModel
} from '../../models/employee-pension.models';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { DatePipe } from '@angular/common';
import jsPDF from 'jspdf';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment';
declare var $: any;
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
  salaryTaxFlag = false;
  // variables
  pensionList$: Observable<any[]>;
  salaryTaxList$: Observable<any[]>;
  selectedCurrencyTax: IDropDownModel;
  selectedCurrencyPension: IDropDownModel;
  selectedFinancialYearPension: IDropDownModel;
  selectedFinancialYearTax: IDropDownModel;
  currencyList$: Observable<IDropDownModel[]>;
  financialYearList$: Observable<IDropDownModel[]>;
  employeeId: number;
  // currencyData: CurrencyCodeModel;
  // currencycodeList: CurrencyCodeModel[];

  pensionProfitTotal: any;
  pensionDeductionTotal: any;
  pensionTotal: any;
  previousPensionDeduction: any;
  previousPensionRate: any;
  previousProfit: any;
  previousTotal: any;

  selectedPensionYear: number[] = [];
  selectedSalaryTaxYear: number[] = [];

  pensionList: EmployeePensionReportModel[];
  pensionReportList: PensionReportModel[] = [];
  employeeTaxReportData: EmployeeTaxReportModel;

  // salary tax pdf
  marginX = 0;
  marginY = 80;
  doc = new jsPDF('p', 'pt', 'a4');
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService,
    private employeePensionService: EmployeePensionService,
    private activeRoute: ActivatedRoute,
    private datePipe: DatePipe,
    private translate: TranslateService
  ) {
    translate.setDefaultLang('fa');
  }

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
          this.selectedPensionYear,
          this.selectedCurrencyPension.value
        );
        this.getAllSalaryTaxData(
          this.selectedSalaryTaxYear,
          this.selectedCurrencyTax.value
        );
        this.getEmployeeTaxCalculation(
          this.employeeId,
          this.selectedSalaryTaxYear
        );
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
          this.financialYearList$.subscribe(element => {
            // note: to get current selected year and salary tax year
            this.selectedPensionYear.push(element[0].value);
            this.selectedSalaryTaxYear.push(element[0].value);
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

  //#region "getAllPensionList"
  getAllPensionList(yearId: number[], currencyId: number) {
    if (yearId !== undefined && currencyId !== undefined) {
      const model = {
        OfficeId: parseInt(localStorage.getItem('SelectedOfficeId'), 32),
        EmployeeId: this.employeeId,
        FinancialYearId: yearId,
        CurrencyId: currencyId
      };
      this.employeePensionService.GetAllPensionList(model).subscribe(
        data => {
          if (data != null) {
            this.pensionReportList = [];
            if (
              data.StatusCode === 200 &&
              data.data.EmployeePensionModel != null
            ) {
              this.pensionList$ = of(
                data.data.EmployeePensionModel.EmployeePensionReportList.map(
                  element => {
                    return {
                      Date: this.datePipe.transform(
                        StaticUtilities.setLocalDate(element.Date),
                        'dd-MM-yyyy'
                      ),
                      GrossSalary: element.GrossSalary,
                      PensionDeduction: element.PensionDeduction.toFixed(4),
                      PensionRate: element.PensionRate,
                      Profit: element.Profit,
                      Total: element.Total.toFixed(4)
                    };
                  }
                )
              );
              // Note to get pension report
              data.data.EmployeePensionModel.EmployeePensionReportList.forEach(
                a => {
                  const year = new Date(
                    new Date(a.Date).getTime() -
                      new Date().getTimezoneOffset() * 60000
                  ).getFullYear();
                  if (
                    this.pensionReportList.filter(x => x.Year === year)
                      .length === 0
                  ) {
                    this.pensionReportList.push({
                      Year: year,
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
                      .filter(x => x.Year === year)[0]
                      .PensionReportList.push({
                        Date: a.Date,
                        GrossSalary: a.GrossSalary,
                        PensionDeduction: a.PensionDeduction,
                        PensionRate: a.PensionRate,
                        Profit: a.Profit,
                        Total: a.Total
                      });
                  }
                }
              );

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
          } else {
            this.toastr.error('Something went wrong!');
          }
        }
      );
    }
  }
  //#endregion

  //#region "selection change for pension finanacialyear"
  onFilterSalaryPension(event: any, data: any) {
    if (event === 'pensionCurrency') {
      this.selectedCurrencyPension = {
        value: data.value,
        name: data.name
      };
    } else {
      this.selectedFinancialYearPension = {
        value: data.value,
        name: data.name
      };
    }
    this.selectedPensionYear = [];
    this.selectedPensionYear.push(this.selectedFinancialYearPension.value);
    this.getAllPensionList(
      this.selectedPensionYear,
      this.selectedCurrencyPension.value
    );
  }
  //#endregion

  //#region "getAllSalaryTaxData"
  getAllSalaryTaxData(yearId: number[], currencyId: number) {
    const model = {
      OfficeId: parseInt(localStorage.getItem('SelectedOfficeId')),
      EmployeeId: this.employeeId,
      FinancialYearId: yearId,
      CurrencyId: currencyId
    };
    this.employeePensionService.GetAllSalaryTaxList(model).subscribe(
      data => {
        // this.salaryTaxDataSource = [];
        if (data != null) {
          if (
            data.StatusCode === 200 &&
            data.data.SalaryTaxReportModelList.length > 0
          ) {
            this.salaryTaxList$ = of(
              data.data.SalaryTaxReportModelList.map(element => {
                return {
                  Date: this.datePipe.transform(
                    StaticUtilities.setLocalDate(element.Date),
                    'dd-MM-yyyy'
                  ),
                  Currency: element.Currency,
                  Office: element.Office,
                  TotalTax: element.TotalTax
                };
              })
            );
            // this.salaryTaxDataSource = data.data.SalaryTaxReportModelList;
            // this.showHideSalaryTaxInfoLoading(false);
          } else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong!');
          }
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "onselectionchange of salary and tax"
  onFilterSalaryTax(event: any, data: any) {
    // filter based on currency or financial year
    if (event === 'taxCurrency') {
      this.selectedCurrencyTax = {
        value: data.value,
        name: data.name
      };
    } else {
      this.selectedFinancialYearTax = {
        value: data.value,
        name: data.name
      };
    }
    this.selectedSalaryTaxYear = [];
    this.selectedSalaryTaxYear.push(this.selectedFinancialYearTax.value);
    this.getAllSalaryTaxData(
      this.selectedSalaryTaxYear,
      this.selectedCurrencyTax.value
    );
    // to get tax report data when we apply filter on year
    this.getEmployeeTaxCalculation(
      this.employeeId,
      this.selectedSalaryTaxYear
    );
  }
  //#endregion

  //#region "getEmployeeTaxCalculation"
  getEmployeeTaxCalculation(employeeId: number, year: number[]) {
    const model = {
      EmployeeId: employeeId,
      FinancialYearId: year,
      OfficeId: parseInt(localStorage.getItem('SelectedOfficeId'))
    };
    this.employeePensionService.GetEmployeeTaxCalculation(model).subscribe(
      data => {
        if (data != null) {
          if (data.StatusCode === 200 && data.data.EmployeeTaxReport != null) {
            this.employeeTaxReportData = data.data.EmployeeTaxReport;
          } else {
            if (data.data.EmployeeTaxReport == null) {
              this.toastr.warning('No record found !');
            } else if (data.StatusCode === 400) {
              // failStatusCode
              this.toastr.error('Something went wrong !');
            }
          }
        } else {
        }
      },
      error => {
        // this.showHidePensionInfoLoading(false);
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

  //#region "generatePensionPdf"
  generatePensionPdf() {
    const pdfModel: EmployeePensionReportPdfModel = {
      EmployeeId: this.employeeId,
      Currency: this.selectedCurrencyPension.name,
      EmployeeName: localStorage.getItem('selectedEmployeeName'),
      PensionReportModel: this.pensionReportList,
      PensionDeductionTotal: this.pensionDeductionTotal,
      PensionProfitTotal: this.pensionProfitTotal,
      Total: this.pensionTotal
    };
    if (this.pensionReportList != null && this.pensionReportList.length > 0) {
      this.employeePensionService.DownloadPDF(pdfModel);
    }
  }
  //#endregion

  //#region "Generate Pdf"
  generateSalaryTaxPdf() {
    const taxDetail = this.employeeTaxReportData;
    if (taxDetail != null && taxDetail != undefined) {
      const pdfModel: EmployeeTaxReportModel = {
        TaxPayerIdentificationNumber: taxDetail.TaxPayerIdentificationNumber,
        NameOfBusiness: taxDetail.NameOfBusiness,
        AddressOfBusiness: taxDetail.AddressOfBusiness,
        TelephoneNumber: taxDetail.TelephoneNumber,
        EmailAddressEmployer: taxDetail.EmailAddressEmployer,

        EmployeeName: taxDetail.EmployeeName,
        EmployeeTaxpayerIdentification:
          taxDetail.EmployeeTaxpayerIdentification,
        EmployeeAddress: taxDetail.EmployeeAddress,
        TelephoneNumberEmployee: taxDetail.TelephoneNumberEmployee,
        EmailAddressEmployee: taxDetail.EmailAddressEmployee,

        AnnualTaxPeriod: taxDetail.AnnualTaxPeriod,
        DatesOfEmployeement: taxDetail.DatesOfEmployeement,
        TotalWages: taxDetail.TotalWages,
        TotalTax: taxDetail.TotalTax,

        OfficerName: taxDetail.OfficerName,
        Position: taxDetail.Position,
        Date: taxDetail.Date
      };
      this.employeePensionService.DownloadTaxPDF(pdfModel);
    }
    // const pdf = new jsPDF('p', 'pt', 'legal'),
    //   pdfConf = {
    //     pagesplit: false,
    //     background: '#fff'
    //   };
    // pdf.addHTML($('#salaryTaxPdf'), 0, 0, pdfConf, function() {
    //   pdf.save('Employee-Salary-tax.pdf');
    // });

    // 2nd test
    // this.translate
    //   .get(['Islamic Republic of Afghanistan'])
    //   .subscribe(translations => {
    //     // this.pages= [
    //     //   { title: translations.HOME, component: HomePage},
    //     //   { title: translations.MY_ACCOUNT, component: MyAccountPage},
    //     //   { title: translations.CHANGE_PASSWORD, component: ChangePasswordPage}
    //     // ];
    //     // const islamicRepublic = translations.'Islamic Republic of Afghanistan';
    //     const text = translations['Islamic Republic of Afghanistan'];

    //     this.doc = new jsPDF('p', 'pt', 'a4');
    //     this.doc.addFileToVFS('Amiri-Regular.ttf', 'Amiri');
    //     this.doc.addFont('Amiri-Regular.ttf', 'Amiri', 'normal');

    //     // this.doc.setFont('Amiri-Regular'); // set font
    //     // this.doc.setFontStyle('bold');

    //     // this.doc.setFontSize(18);
    //     // this.doc.text('مرحبا', 100, 270);
    //      this.doc.text('text', 180, this.marginY + 40);
    //     this.doc.text('هماهنگی کمک های بشردوستانه' ?  'هماهنگی کمک های بشردوستانه' : '', 180, 273);
    //     this.doc.save('general');
    //   });

    // 3rd
  }

  //#endregion
}
