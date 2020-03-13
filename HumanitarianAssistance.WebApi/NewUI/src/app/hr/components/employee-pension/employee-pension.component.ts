import { FileTypes } from './../../../dashboard/project-management/project-list/project-details/models/project-details.model';
import { PdfExportService } from './../../../shared/services/pdf-export.service';
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
  EmployeeTaxReportModel,
  IPensionDetails
} from '../../models/employee-pension.models';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { DatePipe } from '@angular/common';
import jsPDF from 'jspdf';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment';
import { TableActionsModel } from 'projects/library/src/public_api';
import { AddOpeningPensionComponent } from '../add-employee/add-opening-pension/add-opening-pension.component';
import { MatDialog } from '@angular/material';
import { HrService } from '../../services/hr.service';
import { HttpClient } from '@angular/common/http';
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
  pensionDetailListHeaders$ = of(['Id', 'Currency', 'Amount']);
  taxListHeaders$ = of(['Date', 'Currency', 'Office', 'Total Tax']);
  salaryTaxFlag = false;
  // variables
  pensionList$: Observable<any[]>;
  pensionDetailList$: Observable<any[]>;
  pensionDetailListDisplay$: Observable<any[]>;
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
  fileContent = '';
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
  actions: TableActionsModel;
  pensionDetail: IPensionDetails;
  isError = false;
  IsPensionDateSet = false;
  IsVerified = false;
  setPensionDate: Date;
  ErrorMessage = '';

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
    private translate: TranslateService,
    public dialog: MatDialog,
    private hrService: HrService,
    private pdfExportService: PdfExportService,
    private httpSvc: HttpClient
  ) {
    translate.setDefaultLang('fa');
  }

  ngOnInit() {
    this.initModel();
    this.pensionDetail = {
      EmployeeID: 0,
      PensionDate: null,
      PensionDetail: []
    };
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
    this.getAllPensionDetailList();
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

    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
        edit: true
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false
      }
    };
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
          // if (error.StatusCode === 500) {
          //   this.toastr.error('Internal Server Error....');
          // } else if (error.StatusCode === 401) {
          //   this.toastr.error('Unauthorized Access Error....');
          // } else if (error.StatusCode === 403) {
          //   this.toastr.error('Forbidden Error....');
          // } else {

          //  this.toastr.error('Something went wrong!');
          // }
          this.isError = true;
          this.ErrorMessage = error.Message;
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
            // this.toastr.error('Something went wrong!');
            this.isError = true;
            this.ErrorMessage = data.Message;
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
    this.getEmployeeTaxCalculation(this.employeeId, this.selectedSalaryTaxYear);
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
              this.isError = true;
              this.ErrorMessage = 'No record found !';
              // this.toastr.warning('No record found !');
            } else if (data.StatusCode === 400) {
              this.isError = true;
              this.ErrorMessage = data.Message;
              // failStatusCode
              //  this.toastr.error('Something went wrong !');
            }
          }
        } else {
        }
      },
      error => {
        // this.showHidePensionInfoLoading(false);
        // if (error.StatusCode === 500) {
        //   this.toastr.error('Internal Server Error....');
        // } else if (error.StatusCode === 401) {
        //   this.toastr.error('Unauthorized Access Error....');
        // } else if (error.StatusCode === 403) {
        //   this.toastr.error('Forbidden Error....');
        // }
        this.isError = true;
        this.ErrorMessage = error.Message;
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
        EmployeeId: this.employeeId.toString(),
        TaxPayerIdentificationNumber: taxDetail.TaxPayerIdentificationNumber.toString(),
        NameOfBusiness: taxDetail.NameOfBusiness.toString(),
        AddressOfBusiness: taxDetail.AddressOfBusiness.toString(),
        TelephoneNumber: taxDetail.TelephoneNumber.toString(),
        EmailAddressEmployer: taxDetail.EmailAddressEmployer.toString(),

        EmployeeName: taxDetail.EmployeeName.toString(),
        EmployeeTaxpayerIdentification:
        taxDetail.EmployeeTaxpayerIdentification.toString(),
        EmployeeAddress: taxDetail.EmployeeAddress.toString(),
        TelephoneNumberEmployee: taxDetail.TelephoneNumberEmployee.toString(),
        EmailAddressEmployee: taxDetail.EmailAddressEmployee.toString(),

        AnnualTaxPeriod: taxDetail.AnnualTaxPeriod.toString(),
        DatesOfEmployeement: taxDetail.DatesOfEmployeement.toString(),
        TotalWages: taxDetail.TotalWages.toString(),
        TotalTax: taxDetail.TotalTax.toString(),

        OfficerName: taxDetail.OfficerName.toString(),
        Position: taxDetail.Position.toString(),
        Date: taxDetail.Date.toString()
      };
      const isDownload = this.pdfExportService.GenratePdfExportForSalaryPensionDetails(pdfModel);
      if (isDownload !== true) {
        this.toastr.warning('Please Try Again');
      }
  }
}
  getAllPensionDetailList() {
    this.employeePensionService
      .GetAllPensionDetailList(this.employeeId)
      .subscribe(
        x => {
          if (x.ResponseData.length === 0) {
            this.IsPensionDateSet = true;
          }
          if (x.StatusCode === 200 && x.ResponseData.length > 0) {
            this.IsPensionDateSet = false;
            this.pensionDetailList$ = of(
              x.ResponseData.map(element => {
                this.setPensionDate = StaticUtilities.setLocalDate(
                  element.Date
                );
                return {
                  PensionId: element.PensionId,
                  Currency: element.CurrencyId,
                  Amount: element.Amount
                };
              })
            );
            this.pensionDetailListDisplay$ = of(
              x.ResponseData.map(element => {
                return {
                  PensionId: element.PensionId,
                  Currency: element.CurrencyName,
                  Amount: element.Amount
                };
              })
            );
          } else {
            this.setPensionDate = null;
            // this.toastr.warning(x.Message);
          }
        },
        error => {
          this.toastr.warning(error);
        }
      );
  }

  //#endregion
  ActionEvents(event) {
    if (event.type === 'edit') {
      let currencyId;
      this.currencyList$.subscribe(res => {
        currencyId = res.find(x => x.name === event.item.Currency).value;
      });
      const model = {
        PensionId: event.item.PensionId,
        Currency: currencyId,
        Amount: event.item.Amount
      };
      /** Open Education dialog box*/
      const dialogRef = this.dialog.open(AddOpeningPensionComponent, {
        width: '400px',
        data: { item: model }
      });
      // refresh the list after new request created
      dialogRef.componentInstance.onUpdatePensionDetailListRefresh.subscribe(
        result => {
          if (result !== undefined) {
            const dataModel = {
              Amount: result.Amount,
              PensionId: result.PensionId,
              Currency: result.Currency
            };
            this.employeePensionService
              .EditPensionDetail(dataModel)
              .subscribe(res => {
                this.getAllPensionDetailList();
              });
          }
        }
      );
      dialogRef.afterClosed().subscribe(() => {});
    } else if (event.type === 'delete') {
      this.hrService.openDeleteDialog().subscribe(res => {
        if (res === true) {
          this.employeePensionService
            .DeletePensionDetail(event.item.PensionId)
            .subscribe(res => {
              let index;
              this.pensionDetailListDisplay$.subscribe(r => {
                index = r.findIndex(
                  x => x.PensionId === event.item.PensionId
                );
                r.splice(index, 1);
                this.pensionDetailListDisplay$ = of(r);
              });
              this.getAllPensionDetailList();
            });
        }
      });
    }
  }

  // #region "Add Pension"
  AddNewPension(): void {
    if (this.IsVerified === true || this.IsPensionDateSet === false) {
      /** Open Education dialog box*/
      const dialogRef = this.dialog.open(AddOpeningPensionComponent, {
        width: '400px',
        data: {}
      });
      // refresh the list after new request created
      dialogRef.componentInstance.onPensionDetailListRefresh.subscribe(
        result => {
          if (result !== undefined) {
            this.pensionDetail.EmployeeID = this.employeeId;
            this.pensionDetail.PensionDetail.push({
              Amount: result.Amount,
              CurrencyId: result.Currency
            });
            if (this.IsPensionDateSet === false) {
              this.pensionDetail.PensionDate = StaticUtilities.getLocalDate(
                this.setPensionDate
              );
            }
            this.employeePensionService
              .AddPensionDetail(this.pensionDetail)
              .subscribe(res => {
                this.pensionDetail = {
                  EmployeeID: 0,
                  PensionDate: null,
                  PensionDetail: []
                };
                this.getAllPensionDetailList();
              });
          }
        }
      );
      dialogRef.afterClosed().subscribe(() => {});
    } else {
      this.toastr.warning('Please set pension date first');
    }
  }

  checkExchangeRateVerified(exchangeRateDate: any) {
    // this.pensionForm.PensionDate = exchangeRateDate;
    const checkExchangeRateModel = {
      ExchangeRateDate: StaticUtilities.getLocalDate(exchangeRateDate)
    };
    this.employeePensionService
      .CheckExchangeRatesVerified(checkExchangeRateModel)
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (data.ResponseData) {
              this.IsVerified = true;
              this.pensionDetail.PensionDate = StaticUtilities.getLocalDate(
                exchangeRateDate
              );
              this.toastr.success('Date is verified');
            } else {
              this.toastr.warning(
                'No Exchange Rate set/verified for this date'
              );
            }
          } else {
            this.toastr.error(data.Message);
          }
        },
        () => {}
      );
  }
}
