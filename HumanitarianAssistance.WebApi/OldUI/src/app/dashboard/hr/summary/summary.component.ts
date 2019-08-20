import { Component, OnInit } from '@angular/core';
import { CodeService } from '../../code/code.service';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { HrService } from '../hr.service';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {
  Flag = 0;
  currencyFlag = true;
  summaryLoader = false;

  totalEmployee = 0;
  totalGrossSalary = 0;
  totalDeduction = 0;
  totalAllowances = 0;

  recordType = 1;
  employeeTypeId = 1;
  currencyId: number;
  allowanceId: number;
  deductionId: number;
  deductionName: any;
  allowanceName: any;
  maxDate = new Date();
  minDate = new Date();

  employeeSummaryDetailsList: EmployeeSummaryDetailsList[];
  summaryFilterFormData: summaryFilterModel;
  totalDeductionData: any[];
  totalAllowanceData: any[];
  currencyTypeList: any[];
  financialYearList: any[];
  financialYearId: number;
  monthSelected: number;

  employeeTypeList = [
    { EmployeeTypeId: 1, EmployeeTypeName: 'Prospective' },
    { EmployeeTypeId: 2, EmployeeTypeName: 'Active' },
    { EmployeeTypeId: 3, EmployeeTypeName: 'Terminated' }
  ];

  recordTypeList = [
    { RecordTypeId: 1, RecordTypeName: 'Single' },
    { RecordTypeId: 2, RecordTypeName: 'Consolidated' }
  ];

  constructor(
    private hrService: HrService,
    private commonService: CommonService,
    private codeService: CodeService,
    private toastr: ToastrService,
    private setting: AppSettingsService
  ) {
    this.commonService.setLoader(false);
  }

  ngOnInit() {
    this.summaryFilterFormData = {
      AllowanceId: null,
      CurrencyId: null,
      DeductionId: null,
      EmployeeTypeId: null,
      // FinancialYearId: null,
      Year: null,
      Month: new Date(),
      RecordTypeId: 1
    };

    this.getCurrencyCodeList();
    this.GetAllSalaryHeadDetails();
    this.getFinancialYearList();

    this.commonService.getEmployeeOfficeId().subscribe(data => {
      this.commonService.setLoader(false);
    });
  }

  //#region "Get all Currency Details"
  getCurrencyCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyTypeList = [];
          if (data.StatusCode === 200 && data.data.CurrencyList.length > 0) {
            data.data.CurrencyList.forEach(element => {
              this.currencyTypeList.push({
                CurrencyId: element.CurrencyId,
                CurrencyCode: element.CurrencyCode,
                CurrencyName: element.CurrencyName,
              });
              this.currencyId = this.currencyTypeList[0].CurrencyId;
            });
          }
          // tslint:disable-next-line:curly
          if (data.StatusCode === 400)
            this.toastr.error('Something went wrong !');

          this.commonService.setLoader(false);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
            this.toastr.error('Something went wrong !');
          }

          this.commonService.setLoader(false);
        }
      );
  }
  //#endregion

  //#region "Get All Salary Head"
  GetAllSalaryHeadDetails() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllSalaryHead
      )
      .subscribe(
        data => {
          this.totalDeductionData = [];
          this.totalAllowanceData = [];
          if (data.StatusCode === 200 && data.data.SalaryHeadList.length > 0) {
            data.data.SalaryHeadList.forEach(element => {
              // tslint:disable-next-line:curly
              if (element.HeadTypeId === 1)
                // Allowance
                this.totalAllowanceData.push(element);
              // tslint:disable-next-line:curly
              if (element.HeadTypeId === 2)
                // Deduction
                this.totalDeductionData.push(element);
            });
          // tslint:disable-next-line:curly
          } else if (data.data.SalaryHeadList.length <= 0)
            this.toastr.error('No data to display!');
          // tslint:disable-next-line:curly
          else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong !');

          this.commonService.setLoader(false);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          // tslint:disable-next-line:curly
          } else this.toastr.error('Something went wrong !');

          this.commonService.setLoader(false);
        }
      );
  }
  //#endregion

  //#region "Get all Count"
  getAllTotalCounts(data: any) {
    const officeId =
      // tslint:disable-next-line:radix
      parseInt(localStorage.getItem('EMPLOYEEOFFICEID')) != null
        // tslint:disable-next-line:radix
        ? parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
        : null;
    const filterData = {
      AllowanceId: data.AllowanceId,
      CurrencyId: data.CurrencyId,
      DeductionId: data.DeductionId,
      EmployeeTypeId: data.EmployeeTypeId,
      Year: new Date(data.Year).getFullYear(),
      // FinancialYearId: data.FinancialYearId,
      Month:
        data.MonthId == null ? null : new Date(data.MonthId).getMonth() + 1,
      RecordTypeId: data.RecordTypeId,
      OfficeId: officeId
    };
    this.summaryLoader = true;
    this.hrService
      .GetAlltotalCountsForSummary(
        this.setting.getBaseUrl() + GLOBAL.API_HR_EmployeesSalarySummary,
        filterData
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.employeeSummaryDetailsList = [];
            this.totalEmployee =
              data.data.TotalEmployees != null
                ? data.data.TotalEmployees
                : null;
            this.totalGrossSalary =
              data.data.TotalGrossSalary != null
                ? data.data.TotalGrossSalary
                : null;
            this.totalDeduction =
              data.data.TotalDeductions != null
                ? data.data.TotalDeductions
                : null;
            this.totalAllowances =
              data.data.TotalAllowances != null
                ? data.data.TotalAllowances
                : null;
            if (
              data.data.EmployeeSummaryDetailsList != null &&
              data.data.EmployeeSummaryDetailsList.length !== 0
            ) {
              data.data.EmployeeSummaryDetailsList.forEach(element => {
                this.employeeSummaryDetailsList.push({
                  Currency: element.Currency,
                  TotalAllowance: element.TotalAllowance,
                  TotalDeduction: element.TotalDeduction,
                  TotalGrossSalary: element.TotalGrossSalary,
                  CurrencyDetail: this.currencyTypeList.filter(
                    x => x.CurrencyId === element.Currency
                  )
                });
              });
            }
          }
          // tslint:disable-next-line:curly
          if (data.StatusCode === 400)
            this.toastr.error('Something went wrong !');
          this.summaryLoader = false;
          this.commonService.setLoader(false);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
            this.toastr.error('Something went wrong !');
          }
          this.summaryLoader = false;
          this.commonService.setLoader(false);
        }
      );
  }
  //#endregion

  //#region "Get all financial year"
  getFinancialYearList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllFinancialYearDetail
      )
      .subscribe(
        data => {
          this.financialYearList = [];
          if (
            data.StatusCode === 200 &&
            data.data.FinancialYearDetailList.length > 0
          ) {
            data.data.FinancialYearDetailList.forEach(element => {
              this.financialYearList.push({
                FinancialYearId: element.FinancialYearId,
                StartDate: element.StartDate,
                EndDate: element.EndDate,
                FinancialYearName: element.FinancialYearName,
                Description: element.Description,
                IsDefault: element.IsDefault
              });
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
          } else {
          }
        }
      );
  }
  //#endregion

  //#region "on Select changed"
  // onEmployeeTypeSelected(e: any) {
  //   this.employeeTypeId = e.value;
  //   this.getAllTotalCounts();
  // }

  // onRecordTypeSelected(e: any) {
  //   e.value == 1 ? this.currencyFlag = true : this.currencyFlag = false;
  // }

  // onCurrencySelect(e: any) {
  //   this.currencyId = e.value;
  //   this.getAllTotalCounts();

  // }

  // onAllowanceSelected(e: any) {
  //   this.allowanceId = e.value;
  //   this.getAllTotalCounts();

  // }

  // onDeductionSelected(e: any) {
  //   this.deductionId = e.value;
  //   this.getAllTotalCounts();

  // }

  // onFinancialYearSelect(e) {
  //   this.financialYearId = e.value;
  // }

  // onMonthSelected(e) {
  //   this.monthSelected = new Date(e.value).getMonth() + 1;
  // }

  // Form value change
  onFieldDataChanged(e: any) {
    if (e.dataField === 'EmployeeTypeId') {
      this.employeeTypeId = e.value;
    }
    if (e.dataField === 'RecordTypeId') {
      this.recordType = e.value;
      e.value === 1 ? (this.currencyFlag = true) : (this.currencyFlag = false);
      this.totalEmployee = 0;
      this.totalGrossSalary = 0;
      this.totalDeduction = 0;
      this.totalAllowances = 0;
    }
    if (e.dataField === 'CurrencyId') {
      this.currencyId = e.value;
    }

    if (e.dataField === 'DeductionId') {
      // tslint:disable-next-line:curly
      if (e.value == null) this.deductionName = null;
      // tslint:disable-next-line:curly
      if (e.value != null)
        this.deductionName = this.totalDeductionData.filter(
          x => x.SalaryHeadId === e.value
        )[0];
    }
    if (e.dataField === 'AllowanceId') {
      // tslint:disable-next-line:curly
      if (e.value == null) this.allowanceName = null;
      // tslint:disable-next-line:curly
      if (e.value != null)
        this.allowanceName = this.totalAllowanceData.filter(
          x => x.SalaryHeadId === e.value
        )[0];
    }
  }
  //#endregion
}

// tslint:disable-next-line:class-name
export class summaryFilterModel {
  EmployeeTypeId: number;
  RecordTypeId: number;
  CurrencyId: number;
  // FinancialYearId: number;
  Year: any;
  Month: any;
  DeductionId: number;
  AllowanceId: number;
}
export class EmployeeSummaryDetailsList {
  Currency: number;
  TotalAllowance: any;
  TotalDeduction: any;
  TotalGrossSalary: any;
  CurrencyDetail?: any;
}
