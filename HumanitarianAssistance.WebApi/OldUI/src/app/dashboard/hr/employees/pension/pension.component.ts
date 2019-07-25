import { Component, OnInit, Input } from '@angular/core';
import { HrService } from '../../hr.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { CodeService } from '../../../code/code.service';
import { TranslateService } from '@ngx-translate/core';
import {applicationPages, applicationModule} from '../../../../shared/application-pages-enum';
import { CommonService } from '../../../../service/common.service';
import { AppSettingsService } from '../../../../service/app-settings.service';


declare var jquery: any;
declare let jsPDF;
declare var $: any;
@Component({
  selector: 'app-pension',
  templateUrl: './pension.component.html',
  styleUrls: ['./pension.component.css']
})
export class PensionComponent implements OnInit {

  @Input() employeeId: number;

  pensionList: EmployeePensionReportModel[];
  financialYearList: CurrentFinancialYearModel[];
  financialId: number;
  selectedEmployeeName: string;
  currencyData: CurrencyCodeModel;
  currencycodeList: CurrencyCodeModel[];
  humanitarianReportLogoPath: string;
  pensionLoading = false;
  pensionFilter: PensionFilterModel;
  isEditingAllowed = false;

  pensionProfitTotal: any;
  pensionDeductionTotal: any;
  pensionTotal: any;
  previousPensionDeduction: any;
  previousPensionRate: any;
  previousProfit: any;
  previousTotal: any;
  pensionInfoLoading = false;
  salaryTaxReportloading = false;
  employeePensionReportFlag = true;

  employeeTaxReportData: EmployeeTaxReportModel;
  salaryTaxDataSource: any;

  constructor(
    private commonService: CommonService,
    private translate: TranslateService,
    private codeService: CodeService,
    private hrService: HrService,
    private router: Router,
    private setting: AppSettingsService, private toastr: ToastrService) {
    translate.setDefaultLang('fa');
  }

  ngOnInit() {
    this.getFinancialYear();
    this.getCurrencyCodeList();
    this.GetSalaryTaxReportContent();
    this.initializeForm();
    this.humanitarianReportLogoPath = this.setting.getDocUrl() + 'humanitarianReportLogo.PNG';
    this.selectedEmployeeName = localStorage.getItem('SelectedEmployeeName');
    this.isEditingAllowed = this.commonService.IsEditingAllowed(applicationPages.Employees);
  }

  initializeForm() {
    this.currencyData = {
      CurrencyCode: null,
      CurrencyId: null,
      CurrencyName: null,
      CurrencyRate: null
    };

    this.pensionFilter = {
      CurrencyId: null,
      FinancialYearId: []
    };


    this.employeeTaxReportData = {
      TaxPayerIdentificationNumber: null,
      NameOfBusiness: null,
      AddressOfBusiness: null,
      TelephoneNumber: null,
      EmailAddressEmployer: null,

      EmployeeName: null,
      EmployeeTaxpayerIdentification: null,
      EmployeeAddress: null,
      TelephoneNumberEmployee: null,
      EmailAddressEmployee: null,

      AnnualTaxPeriod: null,
      DatesOfEmployeement: null,
      TotalWages: null,
      TotalTax: null,

      OfficerName: null,
      Position: null,
      Date: null
    };

  }

  //#region "GET ALL Pension By financial year id"
  getAllPensionList(yearId: number[], currencyId: number) {
    this.showHidePensionInfoLoading(true);
    const model = {
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'), 32),
      EmployeeId: this.employeeId,
      FinancialYearId: yearId,
      CurrencyId: currencyId
    };

    this.hrService.AddByModel(this.setting.getBaseUrl() + GLOBAL.API_HR_EmployeePensionReport,
    model).subscribe(
      data => {
        if (data != null) {
          this.pensionList = [];

          if (data.StatusCode === 200 && data.data.EmployeePensionModel != null) {
            data.data.EmployeePensionModel.EmployeePensionReportList.forEach(element => {
              this.pensionList.push({
                Date: new Date(new Date(element.Date).getTime() - (new Date().getTimezoneOffset() * 60000)),
                GrossSalary: element.GrossSalary,
                PensionDeduction: element.PensionDeduction,
                PensionRate: element.PensionRate,
                Profit: element.Profit,
                Total: element.Total
              });
            });
            if (data.data.EmployeePensionModel.EmployeePensionReportList[0] != null &&
              data.data.EmployeePensionModel.EmployeePensionReportList[0] !== 0) {

              const currencyId = data.data.EmployeePensionModel.EmployeePensionReportList[0].CurrencyId;
              this.currencyData = this.currencycodeList.filter(x => x.CurrencyId === currencyId)[0];
            }

            this.pensionProfitTotal = data.data.EmployeePensionModel.PensionProfitTotal;
            this.pensionDeductionTotal = data.data.EmployeePensionModel.PensionDeductionTotal;
            this.pensionTotal = data.data.EmployeePensionModel.PensionTotal;
            this.previousPensionDeduction = data.data.EmployeePensionModel.PreviousPensionDeduction;
            this.previousPensionRate = data.data.EmployeePensionModel.PreviousPensionRate;
            this.previousProfit = data.data.EmployeePensionModel.PreviousProfit;
            this.previousTotal = data.data.EmployeePensionModel.PreviousTotal;

            this.showHidePensionInfoLoading(false);

          }else if (data.StatusCode === 200 && data.data.EmployeePensionModel == null) {

            this.toastr.info(data.Message);
            this.showHidePensionInfoLoading(false);
          }else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong!');
            this.showHidePensionInfoLoading(false);
          }
        }
        this.showHidePensionInfoLoading(false);
      },
      error => {
        this.showHideSalaryTaxInfoLoading(false);
        if (error.StatusCode === 500) {
          this.toastr.error('Internal Server Error....');
        }else if (error.StatusCode === 401) {
          this.toastr.error('Unauthorized Access Error....');
        }else if (error.StatusCode === 403) {
          this.toastr.error('Forbidden Error....');
        } else {
          this.toastr.error('Something went wrong!');
        }
      });
  }
  //#endregion

  //#region "Get all financial year"
  getFinancialYear() {
    this.codeService.GetAllCodeList(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllFinancialYearDetail).subscribe(
      data => {
        this.financialYearList = [];
        if (data.data.FinancialYearDetailList != null && data.data.FinancialYearDetailList.length > 0 && data.StatusCode === 200) {
          data.data.FinancialYearDetailList.forEach(element => {
            this.financialYearList.push({
              EndDate: element.EndDate,
              FinancialYearId: element.FinancialYearId,
              FinancialYearName: element.FinancialYearName,
              StartDate: element.StartDate
            });
          });

          this.financialId = this.financialYearList[0].FinancialYearId;
        }else if (data.StatusCode === 400) {
          this.toastr.error('Something went wrong !');
        }
      },
      error => {
        if (error.StatusCode === 500) {
          this.toastr.error('Internal Server Error....');
        }else if (error.StatusCode === 401) {
          this.toastr.error('Unauthorized Access Error....');
        }else if (error.StatusCode === 403) {
          this.toastr.error('Forbidden Error....');
        }else {
          this.toastr.error('Something went wrong !');
        }
      });
  }
  //#endregion

  //#region "Get all Currency Details"
  getCurrencyCodeList() {

    this.codeService.GetAllCodeList(this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency).subscribe(
      data => {
        this.currencycodeList = [];
        if (data.StatusCode === 200 && data.data.CurrencyList.length > 0) {

          data.data.CurrencyList.forEach(element => {
            this.currencycodeList.push({
              CurrencyId: element.CurrencyId,
              CurrencyCode: element.CurrencyCode,
              CurrencyName: element.CurrencyName,
              CurrencyRate: element.CurrencyRate
            });
          });
        }else if (data.StatusCode === 400) {
          this.toastr.error('Something went wrong !');
        }else {
          this.toastr.error('Something went wrong !');
        }
      },
      error => {
        if (error.StatusCode === 500) {
          this.toastr.error('Internal Server Error....');
        }else if (error.StatusCode === 401) {
          this.toastr.error('Unauthorized Access Error....');
        }else if (error.StatusCode === 403) {
          this.toastr.error('Forbidden Error....');
        }else {
          this.toastr.error('Something went wrong !');
        }
      });
  }
  //#endregion

  getAllSalaryTaxData(yearId: number, currencyId: number) {

    const model = {
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'), 32),
      EmployeeId: this.employeeId,
      FinancialYearId: yearId,
      CurrencyId: currencyId
    };

    this.showHideSalaryTaxInfoLoading(true);
    this.hrService.AddByModel(this.setting.getBaseUrl() + GLOBAL.API_Hr_EmployeeSalaryTaxDetails,
    model).subscribe(
      data => {
        this.salaryTaxDataSource = [];
        if (data != null) {
          if (data.StatusCode === 200 && data.data.SalaryTaxReportModelList.length > 0) {
            this.salaryTaxDataSource = data.data.SalaryTaxReportModelList;
            this.showHideSalaryTaxInfoLoading(false);
          }else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong!');
            this.showHideSalaryTaxInfoLoading(false);
          }
        }else {
          this.showHideSalaryTaxInfoLoading(false);
        }
        this.showHideSalaryTaxInfoLoading(false);
      },
      error => {
        this.showHideSalaryTaxInfoLoading(false);
      });
  }

  //#endregion

  salaryTaxReportForm: any;
  //#region "GetSalaryTaxReportContent"
  GetSalaryTaxReportContent() {

    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));

    this.codeService.GetAllDetailsById(this.setting.getBaseUrl() + GLOBAL.API_Code_GetSalaryTaxReportContentDetails, 'officeId', officeId).subscribe(
      data => {

        if (data.StatusCode === 200) {
          if (data.data.SalaryTaxReportContentDetails != null) {

            this.salaryTaxReportForm = {
              SalaryTaxReportContentId: data.data.SalaryTaxReportContentDetails.SalaryTaxReportContentId,
              EmployerAuthorizedOfficerName: data.data.SalaryTaxReportContentDetails.EmployerAuthorizedOfficerName,
              PositionAuthorizedOfficer: data.data.SalaryTaxReportContentDetails.PositionAuthorizedOfficer,
              OfficeId: data.data.SalaryTaxReportContentDetails.OfficeId,
            };
          }
          else {

            this.salaryTaxReportForm = {
              SalaryTaxReportContentId: 0,
              EmployerAuthorizedOfficerName: null,
              PositionAuthorizedOfficer: null,
              OfficeId: null
            };
          }
        }
      },
      error => {
      });
  }
  //#endregion

  generateExcel(e) {

    window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=pensionReportExcel]').html()));
    e.preventDefault();
  }

  //#region "Generate Pension Pdf"
  generatePensionPdf() {

    const pdf = new jsPDF('p', 'pt', 'legal'),
      pdfConf = {
        pagesplit: false,
        background: '#fff'
      };

    pdf.addHTML($('#pensionReportPdf'), 0, 15, pdfConf, function () {


      pdf.save('Employee-Pension.pdf');
    });

  }
  //#endregion

  //#region "Generate Pdf"
  generateSalaryTaxPdf() {

    const pdf = new jsPDF('p', 'pt', 'legal'),
      pdfConf = {
        pagesplit: false,
        background: '#fff'
      };
    pdf.addHTML($('#salaryTaxPdf'), 0, 0, pdfConf, function () {
      pdf.save('Employee-Salary-tax.pdf');
    });

  }
  //#endregion



  showClosePensionReport() {
    this.employeePensionReportFlag = !this.employeePensionReportFlag;
  }

  //#region "Get Salary Tax Info"
  getEmployeeTaxCalculation(employeeId, year) {

    this.showHidePensionInfoLoading(true);
    const model = {
      EmployeeId: employeeId,
      FinancialYearId: year,
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'), 32)
    };

    this.hrService.AddByModel(this.setting.getBaseUrl() + GLOBAL.API_Hr_EmployeeTaxCalculation, model).subscribe(
      data => {
        if (data != null) {
          if (data.StatusCode === 200 && data.data.EmployeeTaxReport != null) {

            this.employeeTaxReportData = data.data.EmployeeTaxReport;
          } else {
            if (data.data.EmployeeTaxReport == null) {
              this.toastr.warning('No record found !');
              this.showHidePensionInfoLoading(false);
            }else if (data.StatusCode === 400) {
              // failStatusCode
              this.toastr.error('Something went wrong !');
              this.showHidePensionInfoLoading(false);
            }
          }
          this.showHideSalaryTaxInfoLoading(false);
        }else {
          this.showHidePensionInfoLoading(false);
        }
      },
      error => {
        this.showHidePensionInfoLoading(false);
        if (error.StatusCode === 500) {
          this.toastr.error('Internal Server Error....');
        }else if (error.StatusCode === 401) {
          this.toastr.error('Unauthorized Access Error....');
        }else if (error.StatusCode === 403) {
          this.toastr.error('Forbidden Error....');
        }
      });
  }
  //#endregion

  onApplyingFilter(data) {
    this.getAllPensionList(data.FinancialYearId, data.CurrencyId);
    this.getAllSalaryTaxData(data.FinancialYearId, data.CurrencyId);
    this.getEmployeeTaxCalculation(this.employeeId, data.FinancialYearId);
  }



  showHidePensionInfoLoading(e: boolean) {
    this.pensionInfoLoading = e;
  }

  showHideSalaryTaxInfoLoading(e: boolean) {
    this.salaryTaxReportloading = e;
  }

}

export class CurrentFinancialYearModel {
  EndDate: any;
  FinancialYearId: number;
  FinancialYearName: string;
  StartDate: any;
}
export class EmployeePensionReportModel {
  Date: any;
  GrossSalary: any;
  PensionRate: any;
  PensionDeduction: any;
  Profit: any;
  Total: any;
}

export interface CurrencyCodeModel {
  CurrencyId: any;
  CurrencyCode: string;
  CurrencyName: string;
  CurrencyRate: any;
}

class EmployeeTaxReportModel {
  TaxPayerIdentificationNumber: any;
  NameOfBusiness: any;
  AddressOfBusiness: any;
  TelephoneNumber: any;
  EmailAddressEmployer: any;

  EmployeeName: any;
  EmployeeTaxpayerIdentification: any;
  EmployeeAddress: any;
  TelephoneNumberEmployee: any;
  EmailAddressEmployee: any;

  AnnualTaxPeriod: any;
  DatesOfEmployeement: any;
  TotalWages: any;
  TotalTax: any;

  OfficerName: any;
  Position: any;
  Date: any;
}

interface PensionFilterModel {
  CurrencyId: number;
  FinancialYearId: number[];
}
