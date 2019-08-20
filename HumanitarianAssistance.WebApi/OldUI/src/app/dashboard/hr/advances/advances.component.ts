import { Component, OnInit } from '@angular/core';
import { HrService } from '../hr.service';
import { CodeService } from '../../code/code.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-advances',
  templateUrl: './advances.component.html',
  styleUrls: ['./advances.component.css']
})
export class AdvancesComponent implements OnInit {
  //#region "Variables"
  employeeAdvancesDataSource: EmployeeAdvancesModel[];
  employeeAdvancesForm: EmployeeAdvancesFormModel;
  isEditingAllowed = false;
  approvedEditDisableFlag = true;

  currencyDataSource: CurrencyCode[];
  employeeListDataSource: EmployeeListModel[];
  voucherDropdown: any[];

  advanceDateSelectedValue = new Date();
  employeeSelectedValue: any;
  advancesHistoryDataSource: any;

  // popup
  addEmpAdvancesPopupVisible = false;
  editEmpAdvancesPopupVisible = false;
  conformEmpAdvancesPopupVisible = false;
  showAdvanceHistoryPopUp = false;

  // loader
  advancesLoader = false;
  addEmpAdvancesPopupLoading = false;
  editEmpAdvancesPopupLoading = false;
  conformEmpAdvancesPopupLoading = false;

  currentDate = new Date();
  //#endregion

  constructor(
    private hrService: HrService,
    private codeService: CodeService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) {}

  ngOnInit() {
    this.commonService.getEmployeeOfficeId().subscribe(data => {
      this.getAllEmployeeListByOfficeId();
      this.getAllEmployeeAdvances(
        new Date(this.currentDate).getMonth() + 1,
        new Date(this.currentDate).getFullYear()
      );
    });

    this.initializeForm();
    this.getCurrencyCodeList();
    this.getAllEmployeeListByOfficeId();
    this.getAllEmployeeAdvances(
      new Date(this.currentDate).getMonth() + 1,
      new Date(this.currentDate).getFullYear()
    );
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.Advances
    );
  }

  initializeForm() {
    this.employeeAdvancesForm = {
      AdvancesId: null,
      EmployeeId: null,
      AdvanceDate: null,
      CurrencyId: null,
      VoucherReferenceNo: null,
      ApprovedBy: null,
      ModeOfReturn: null,
      Description: null,
      RequestAmount: null,
      AdvanceAmount: null,
      OfficeId: null
    };
  }

  //#region "Get All Employee Advances"
  getAllEmployeeAdvances(month: number, year: number) {
    this.advancesLoader = true;

    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
    this.hrService
      .GetAllByOfficeIdMonthYear(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_GetAllAdvancesByOfficeId,
        officeId,
        month,
        year
      )
      .subscribe(
        data => {
          this.employeeAdvancesDataSource = [];
          if (data.StatusCode === 200 && data.data.AdvanceList != null) {
            if (data.data.AdvanceList.length > 0) {
              data.data.AdvanceList.forEach(element => {
                this.employeeAdvancesDataSource.push({
                  AdvancesId: element.AdvancesId,
                  AdvanceDate: new Date(
                    new Date(element.AdvanceDate).getTime() -
                      new Date().getTimezoneOffset() * 60000
                  ),
                  EmployeeId: element.EmployeeId,
                  EmployeeName: element.EmployeeName,
                  EmployeeDetail: element.EmployeeDetail,
                  EmployeeCode: element.EmployeeCode,
                  CurrencyId: element.CurrencyId,
                  CurrencyDetails: element.CurrencyDetails,
                  VoucherReferenceNo: element.VoucherReferenceNo,
                  Description: element.Description,
                  ModeOfReturn: element.ModeOfReturn,
                  ApprovedBy: element.ApprovedBy,
                  RequestAmount: element.RequestAmount,
                  AdvanceAmount: element.AdvanceAmount,
                  OfficeId: element.OfficeId,
                  IsApproved: element.IsApproved,
                  IsDeducted: element.IsDeducted,
                  NumberOfInstallments: element.NumberOfInstallments
                });
              });
            }
          } else {
          }
          this.advancesLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.advancesLoader = false;
        }
      );
  }
  //#endregion

  //#region  "Get all Currency Details"
  getCurrencyCodeList() {
    // this.currencyCodeListLoading = true;
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyDataSource = [];
          if (data.StatusCode === 200 && data.data.CurrencyList.length > 0) {
            data.data.CurrencyList.forEach(element => {
              this.currencyDataSource.push({
                CurrencyId: element.CurrencyId,
                CurrencyCode: element.CurrencyCode,
                CurrencyName: element.CurrencyName
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
  getAllEmployeeListByOfficeId() {
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
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
  AddEmployeeAdvances(model: EmployeeAdvancesFormModel) {
    const advanceModel: EmployeeAdvancesFormModel = {
      AdvancesId: 0,
      EmployeeId: model.EmployeeId,
      AdvanceDate: new Date(
        new Date(model.AdvanceDate).getFullYear(),
        new Date(model.AdvanceDate).getMonth(),
        new Date(model.AdvanceDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      CurrencyId: model.CurrencyId,
      VoucherReferenceNo: model.VoucherReferenceNo,
      ApprovedBy: model.ApprovedBy,
      ModeOfReturn: model.ModeOfReturn,
      Description: model.Description,
      RequestAmount: model.RequestAmount,
      AdvanceAmount: model.AdvanceAmount,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
      NumberOfInstallments: model.NumberOfInstallments
    };

    if (advanceModel.AdvanceAmount <= 0) {
      this.toastr.warning('Advance amount should be greater than 0');
      return;
    }
    if (advanceModel.CurrencyId == null || advanceModel.CurrencyId === undefined ) {
      this.toastr.warning('Currency not selected');
      return;
    }

    this.addEmpAdvancesPopupLoading = true;

    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_AddAdvances,
        advanceModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Advance Added Successfully !');
            this.hideEmpAdvancesAddPopup();
          } else {
            if (data.StatusCode === 400) {
              this.toastr.error(data.Message); // Employee already requested for advance in this month
            } else {
              this.toastr.error('Something went wrong !');
            }
          }
          this.getAllEmployeeAdvances(
            this.advanceDateSelectedValue.getMonth() + 1,
            this.advanceDateSelectedValue.getFullYear()
          );
          this.addEmpAdvancesPopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideEmpAdvancesAddPopup();
          this.addEmpAdvancesPopupLoading = false;
        }
      );
  }
  //#endregion

  //#region "Edit Employee Advances"
  EditEmployeeAdvances(model: EmployeeAdvancesFormModel) {
    const advanceModel: EmployeeAdvancesFormModel = {
      AdvancesId: model.AdvancesId,
      EmployeeId: model.EmployeeId,
      AdvanceDate: new Date(
        new Date(model.AdvanceDate).getFullYear(),
        new Date(model.AdvanceDate).getMonth(),
        new Date(model.AdvanceDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      CurrencyId: model.CurrencyId,
      VoucherReferenceNo: model.VoucherReferenceNo,
      ApprovedBy: model.ApprovedBy,
      ModeOfReturn: model.ModeOfReturn,
      Description: model.Description,
      RequestAmount: model.RequestAmount,
      AdvanceAmount: model.AdvanceAmount,
      OfficeId: model.OfficeId,
      NumberOfInstallments: model.NumberOfInstallments
    };

    this.editEmpAdvancesPopupLoading = true;
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_EditAdvances,
        advanceModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Advance Updated Successfully!!!');
          } else {
          }
          this.getAllEmployeeAdvances(
            this.advanceDateSelectedValue.getMonth() + 1,
            this.advanceDateSelectedValue.getFullYear()
          );
          this.onShowHideEmpAdvancesEditPopup();
          this.editEmpAdvancesPopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.onShowHideEmpAdvancesEditPopup();
          this.editEmpAdvancesPopupLoading = false;
        }
      );
  }

  //#endregion

  //#region "on Advance Date Selected"
  onAdvanceDateSelected(e: any) {
    this.advanceDateSelectedValue = new Date(e.value);
    this.getAllEmployeeAdvances(
      this.advanceDateSelectedValue.getMonth() + 1,
      this.advanceDateSelectedValue.getFullYear()
    );
  }
  //#endregion

  //#region "on Is Approved Value Changed"
  onIsApprovedValueChanged(rowData: any, e: any) {
    if (e.value) {
      this.conformEmpAdvancesPopupVisible = true;
      const dataKey = rowData.key;

      this.employeeAdvancesForm = {
        AdvancesId: dataKey.AdvancesId,
        EmployeeId: dataKey.EmployeeId,
        AdvanceDate: new Date(dataKey.AdvanceDate),
        CurrencyId: dataKey.CurrencyId,
        VoucherReferenceNo: dataKey.VoucherReferenceNo,
        ApprovedBy: dataKey.ApprovedBy,
        ModeOfReturn: dataKey.ModeOfReturn,
        Description: dataKey.Description,
        RequestAmount: dataKey.RequestAmount,
        AdvanceAmount: dataKey.AdvanceAmount,
        OfficeId: dataKey.OfficeId
      };
    }
  }
  //#endregion

  //#region "approval For Advance"
  approvalForAdvance() {
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_ApproveAdvances,
        this.employeeAdvancesForm
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Advance Approved Successfully!!!');
            this.getAllEmployeeAdvances(
              this.advanceDateSelectedValue.getMonth() + 1,
              this.advanceDateSelectedValue.getFullYear()
            );
          } else {
            if (data.StatusCode === 400) {
              this.toastr.error('Advance Approval failed !');
            } else {
              this.toastr.error('Something went wrong !');
            }
          }
          this.onShowHideEmpAdvancesConformPopup();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.onShowHideEmpAdvancesConformPopup();
        }
      );
  }
  //#endregion

  //#region "Events"

  // popup
  onEditEmpAdvancesShowPopup(data: any) {
    if (data) {
      const dataKey = data.key;

      dataKey.IsApproved === true
        ? this.showApprovedEditDisableFlag()
        : this.hideApprovedEditDisableFlag();

      this.employeeAdvancesForm = {
        AdvancesId: dataKey.AdvancesId,
        EmployeeId: dataKey.EmployeeId,
        AdvanceDate: dataKey.AdvanceDate,
        CurrencyId: dataKey.CurrencyId,
        VoucherReferenceNo: dataKey.VoucherReferenceNo,
        ApprovedBy: dataKey.ApprovedBy,
        ModeOfReturn: dataKey.ModeOfReturn,
        Description: dataKey.Description,
        RequestAmount: dataKey.RequestAmount,
        AdvanceAmount: dataKey.AdvanceAmount,
        OfficeId: dataKey.OfficeId,
        NumberOfInstallments: dataKey.NumberOfInstallments
      };
      this.onShowHideEmpAdvancesEditPopup();
    }
  }

  onShowEmployeeAdvanceHistoryPopUp(data: any) {
    if (data) {
      const AdvanceId: number = data.key.AdvancesId;

      this.hrService
        .GetAdvanceHIstory(
          this.setting.getBaseUrl() +
            GLOBAL.API_Code_GetEmployeeAdvanceHistoryDetail,
          AdvanceId
        )
        .subscribe(
          // tslint:disable-next-line:no-shadowed-variable
          data => {
            this.advancesHistoryDataSource = [];
            if (
              data.StatusCode === 200 &&
              data.data.AdvanceHistory != null &&
              data.data.AdvanceHistory.length > 0
            ) {
              data.data.AdvanceHistory.forEach(element => {
                this.advancesHistoryDataSource.push(element);
              });

              this.commonService.sortDropdown(
                this.advancesHistoryDataSource,
                'PaymentDate'
              );
            } else {
              // tslint:disable-next-line:curly
              if (data.StatusCode === 400)
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
      this.onShowAdvanceHistoryPopUp();
    }
  }

  onShowAdvanceHistoryPopUp() {
    this.showAdvanceHistoryPopUp = true;
  }

  onHideAdvanceHistoryPopUp() {
    this.showAdvanceHistoryPopUp = false;
  }

  showEmpAdvancesAddPopup() {
    this.initializeForm();
    this.employeeSelectedValue = null;
    this.addEmpAdvancesPopupVisible = true;
  }

  hideEmpAdvancesAddPopup() {
    this.addEmpAdvancesPopupVisible = false;
  }

  onShowHideEmpAdvancesEditPopup() {
    this.editEmpAdvancesPopupVisible = !this.editEmpAdvancesPopupVisible;
  }

  // Confirm
  onShowHideEmpAdvancesConformPopup() {
    this.conformEmpAdvancesPopupVisible = !this.conformEmpAdvancesPopupVisible;
  }

  // Loader

  showAdvancesLoader() {
    this.advancesLoader = true;
  }
  hideAdvancesLoader() {
    this.advancesLoader = false;
  }

  onOffEmpAdvancesAddPopupLoading() {
    this.addEmpAdvancesPopupLoading = !this.addEmpAdvancesPopupLoading;
  }

  onOffEmpAdvancesEditPopupLoading() {
    this.editEmpAdvancesPopupLoading = !this.editEmpAdvancesPopupLoading;
  }

  showApprovedEditDisableFlag() {
    this.approvedEditDisableFlag = true;
  }
  hideApprovedEditDisableFlag() {
    this.approvedEditDisableFlag = false;
  }
  //#endregion
}

//#region "classes"
class EmployeeAdvancesFormModel {
  AdvancesId: number;
  EmployeeId: number;
  AdvanceDate: any;
  CurrencyId: number;
  VoucherReferenceNo: any;
  ApprovedBy: any;
  ModeOfReturn?: any;
  Description: string;
  RequestAmount: DoubleRange;
  AdvanceAmount: DoubleRange;
  OfficeId: number;
  NumberOfInstallments?: number;
}

class EmployeeAdvancesModel {
  AdvancesId: number;
  AdvanceDate: any;
  EmployeeId: number;
  EmployeeName: string;
  EmployeeDetail: any;
  EmployeeCode: any;
  CurrencyId: any;
  CurrencyDetails: any;
  VoucherReferenceNo: any;
  Description: any;
  ModeOfReturn: any;
  ApprovedBy: any;
  RequestAmount: any;
  AdvanceAmount: any;
  OfficeId: number;
  IsApproved: any;
  IsDeducted: any;
  NumberOfInstallments?: number;
}

class CurrencyCode {
  CurrencyId: number;
  CurrencyCode: string;
  CurrencyName: string;
}

class EmployeeListModel {
  EmployeeId: any;
  EmployeeName: any;
  EmployeeCode: any;
  CodeEmployeeName: any;
}

//#endregion
