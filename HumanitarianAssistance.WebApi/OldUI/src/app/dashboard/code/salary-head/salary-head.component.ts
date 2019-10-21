import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CodeService, HeadType } from '../code.service';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { SalaryHeadType, TransactionType } from '../../../shared/enums';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-salary-head',
  templateUrl: './salary-head.component.html',
  styleUrls: ['./salary-head.component.css']
})
export class SalaryHeadComponent implements OnInit {
  salaryHeadList: SalaryHeadModel[];
  salaryHeadData: SalaryHeadModel;
  salaryHeadTypeDropdown: HeadType[];
  levelFourAccounts: any;
  transactionTypeDropdown: any;
  isEditingAllowed = false;

  DeleteSalaryHeadData: SalaryHeadModel;

  addSalaryHeadpopupVisible = false;
  deleteConfirmationPopupVisible = false;

  // loader
  salaryHeadPopupLoading = false;
  deleteSalaryHeadPopupLoading = false;

  constructor(
    private commonService: CommonService,
    private router: Router,
    private toastr: ToastrService,
    private setting: AppSettingsService,
    private codeservice: CodeService
  ) {
    this.transactionTypeDropdown = [
      { TransactionTypeId: 1, TransactionTypeName: 'Credit' },
      { TransactionTypeId: 2, TransactionTypeName: 'Debit' }
    ];
  }

  ngOnInit() {
    this.initializeForm();
    this.salaryHeadTypeDropdown = this.codeservice.getHeadType();
    this.GetAllSalaryHeadDetails();
    this.getLevelFourAccountDetails();
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.SalaryHead
    );
  }

  initializeForm() {
    this.salaryHeadData = {
      SalaryHeadId: 0,
      HeadTypeId: 0,
      HeadName: '',
      Description: '',
      AccountNo: 0,
      TransactionTypeId: 0,
      SaveForAll: false
    };
  }

  //#region "Get All Salary Head"
  GetAllSalaryHeadDetails() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllSalaryHead
      )
      .subscribe(
        data => {
          this.salaryHeadList = [];
          if (data.StatusCode === 200 && data.data.SalaryHeadList.length > 0) {
            data.data.SalaryHeadList.forEach(element => {
              this.salaryHeadList.push(element);
            });
          }

          this.commonService.setLoader(false);
        },
        // tslint:disable-next-line:no-shadowed-variable
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.commonService.setLoader(false);
        }
      );
  }
  //#endregion

  //#region "Add Salary Head"
  AddSalaryHeadDetail(data: any) {
    const addSalaryHead: SalaryHeadModel = {
      SalaryHeadId: 0,
      HeadTypeId: data.HeadTypeId,
      HeadName: data.HeadName,
      Description: data.Description,
      AccountNo: data.AccountNo,
      TransactionTypeId: data.TransactionTypeId,
      SaveForAll: data.SaveForAll
    };

    if (
      data.HeadTypeId === SalaryHeadType.ALLOWANCE ||
      data.HeadTypeId === SalaryHeadType.GENERAL
    ) {
      addSalaryHead.TransactionTypeId = TransactionType.Debit;
    } else if (data.HeadTypeId === SalaryHeadType.DEDUCTION) {
      addSalaryHead.TransactionTypeId = TransactionType.Credit;
    }

    this.salaryHeadPopupLoading = true;
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddSalaryHead,
        addSalaryHead
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully!');
          } else if (data.StatusCode === 900) {
            this.toastr.error(data.Message);
          } else {
            this.toastr.error('Something went wrong!');
          }
          this.GetAllSalaryHeadDetails();
          this.hideAddSalaryHeadPopup();
          this.salaryHeadPopupLoading = false;
        },
        () => {
          this.toastr.error('Something went wrong!');
        }
      );
  }

  //#endregion

  //#region "Edit Salary head"
  EditSalaryHeadDetail(data) {
    const editSalaryHead = {
      SalaryHeadId: data.SalaryHeadId,
      HeadTypeId: data.HeadTypeId,
      HeadName: data.HeadName,
      Description: data.Description,
      AccountNo: data.AccountNo,
      TransactionTypeId: data.TransactionTypeId,
      SaveForAll: data.SaveForAll
    };

    if (
      data.HeadTypeId === SalaryHeadType.ALLOWANCE ||
      data.HeadTypeId === SalaryHeadType.GENERAL
    ) {
      editSalaryHead.TransactionTypeId = TransactionType.Debit;
    } else if (data.HeadTypeId === SalaryHeadType.DEDUCTION) {
      editSalaryHead.TransactionTypeId = TransactionType.Credit;
    }

    this.salaryHeadPopupLoading = true;
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditSalaryHead,
        editSalaryHead
      )
      // tslint:disable-next-line:no-shadowed-variable
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Updated Successfully!!!');
        } else if (data.StatusCode === 900) {
          this.toastr.error(data.Message);
        } else {
          this.toastr.error('Something went wrong!');
        }
        this.GetAllSalaryHeadDetails();
        this.hideAddSalaryHeadPopup();
        this.salaryHeadPopupLoading = false;
      });
  }
  //#endregion

  //#region "Delete Salary head"
  deleteSalaryHeadDetail() {
    if (this.DeleteSalaryHeadData != null) {
      this.showDeleteSalaryHeadPopupLoading();

      this.codeservice
        .AddEditDetails(
          this.setting.getBaseUrl() + GLOBAL.API_Code_DeleteSalaryHead,
          this.DeleteSalaryHeadData
        )
        .subscribe(data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Deleted Successfully!!!');
          } else if (data.StatusCode === 900) {
            this.toastr.error(data.Message);
          } else {
            this.toastr.error(data.Message);
          }

          this.GetAllSalaryHeadDetails();
          this.hideDeleteSalaryHeadPopup();
          this.hideDeleteSalaryHeadPopupLoading();
        });
    }
  }
  //#endregion

  //#region "onFormSubmit"
  onFormSubmit(data: any) {
    if (data.SalaryHeadId === 0 || data.SalaryHeadId == null) {
      this.AddSalaryHeadDetail(data);
    } else {
      this.EditSalaryHeadDetail(data);
    }
  }
  //#endregion

  //#region "getSalaryHeadById"
  getSalaryHeadById(data: any) {
    this.salaryHeadData = {
      SalaryHeadId: data.data.SalaryHeadId,
      HeadTypeId: data.data.HeadTypeId,
      HeadName: data.data.HeadName,
      Description: data.data.Description,
      AccountNo: data.data.AccountNo,
      TransactionTypeId: data.data.TransactionTypeId,
      SaveForAll: data.data.SaveForAll
    };
    this.addSalaryHeadpopupVisible = true;
  }
  //#endregion


  //#region "deleteSalaryHeadById"
  deleteSalaryHeadById(data: any) {
    if (data != null) {
      this.DeleteSalaryHeadData = {
        SalaryHeadId: data.data.SalaryHeadId,
        HeadTypeId: data.data.HeadTypeId,
        HeadName: data.data.HeadName,
        Description: data.data.Description,
        AccountNo: data.data.AccountNo,
        TransactionTypeId: data.data.TransactionTypeId,
        SaveForAll: data.data.SaveForAll
      };
      this.showDeleteSalaryHeadPopup();
    }
  }
  //#endregion

  //#region "getLevelFourAccountDetails"
  getLevelFourAccountDetails() {
    this.showDeleteSalaryHeadPopupLoading();
    this.codeservice
      .GetAllDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllInputLevelAccountCode
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
          this.hideDeleteSalaryHeadPopupLoading();
        },
        // tslint:disable-next-line:no-shadowed-variable
        error => {
          this.hideDeleteSalaryHeadPopupLoading();
        }
      );
  }
  //#endregion

  //#region "Show / Hide"
  showAddSalaryHeadPopup() {
    this.salaryHeadData = {
      SalaryHeadId: 0,
      HeadTypeId: 0,
      HeadName: null,
      Description: null,
      AccountNo: null,
      TransactionTypeId: null,
      SaveForAll: false
    };
    this.addSalaryHeadpopupVisible = true;
  }

  hideAddSalaryHeadPopup() {
    this.addSalaryHeadpopupVisible = false;
  }

  showDeleteSalaryHeadPopup() {
    this.deleteConfirmationPopupVisible = true;
  }

  hideDeleteSalaryHeadPopup() {
    this.deleteConfirmationPopupVisible = false;
  }

  showDeleteSalaryHeadPopupLoading() {
    this.deleteSalaryHeadPopupLoading = true;
  }
  hideDeleteSalaryHeadPopupLoading() {
    this.deleteSalaryHeadPopupLoading = false;
  }
  //#endregion
}

export class SalaryHeadModel {
  SalaryHeadId: number;
  HeadTypeId: number;
  HeadName: string;
  Description: string;
  AccountNo: number;
  TransactionTypeId: number;
  SaveForAll: boolean;
}
