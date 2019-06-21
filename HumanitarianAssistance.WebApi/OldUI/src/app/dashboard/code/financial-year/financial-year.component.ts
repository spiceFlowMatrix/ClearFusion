import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { CodeService } from '../code.service';
import { GLOBAL } from '../../../shared/global';
import { BsModalService } from 'ngx-bootstrap';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { FinancialYear } from '../../../model/code-model';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-financial-year',
  templateUrl: './financial-year.component.html',
  styleUrls: ['./financial-year.component.css']
})
export class FinancialYearComponent implements OnInit {
  popupVisible = false;
  showFilterRow: boolean;
  financialyeardt: FinancialYear[];
  financialyeardata: FinancialYear;
  isEditingAllowed = false;

  // loader
  financialYearListLoading = false;
  financialYearPopupLoading = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private codeservice: CodeService,
    private commonservice: CommonService
  ) {
    this.financialyeardata = this.codeservice.getFinancialYearData();
    this.showFilterRow = true;
  }

  ngOnInit() {
    this.getFinancialYearList();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.FinancialYear
    );
  }

  getFinancialYearList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllFinancialYearDetail
      )
      .subscribe(
        data => {
          this.financialyeardt = [];
          if (
            data.StatusCode === 200 &&
            data.data.FinancialYearDetailList.length > 0
          ) {
            data.data.FinancialYearDetailList.forEach(element => {
              this.financialyeardt.push({
                FinancialYearId: element.FinancialYearId,
                StartDate: element.StartDate,
                EndDate: element.EndDate,
                FinancialYearName: element.FinancialYearName,
                Description: element.Description,
                IsDefault: element.IsDefault
              });
            });
          }
          this.commonservice.setLoader(false);
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

  showPopup() {
    this.financialyeardata = this.codeservice.getFinancialYearData();
    this.popupVisible = true;
  }

  HidePopup() {
    this.popupVisible = false;
  }

  onFormSubmit(model) {
    if (model.FinancialYearId === '') {
      this.AddFinancialYearDetail(model);
    } else {
      this.EditFinancialYearDetail(model);
    }
  }

  AddFinancialYearDetail(model) {
    this.financialYearPopupLoading = true;
    const obj: any = {};
    const addfinancialyear: FinancialYear = {
      FinancialYearId: 0,
      StartDate: new Date(
        new Date(model.StartDate).getFullYear(),
        new Date(model.StartDate).getMonth(),
        new Date(model.StartDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      EndDate: new Date(
        new Date(model.EndDate).getFullYear(),
        new Date(model.EndDate).getMonth(),
        new Date(model.EndDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      FinancialYearName: model.FinancialYearName,
      Description: model.Description,
      IsDefault: model.IsDefault
    };
    this.codeservice
      .AddEditFinancialYearDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddFinancialYearDetail,
        addfinancialyear
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('FinancialYear Added Successfully!!!');
            this.getFinancialYearList();
            this.HidePopup();
          } else if (data.StatusCode === 900) {
            this.toastr.error('FinancialYear already exist!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.financialYearPopupLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  EditFinancialYearDetail(model) {
    this.financialYearPopupLoading = true;
    const addfinancialyear: FinancialYear = {
      FinancialYearId: model.FinancialYearId,
      StartDate: model.StartDate,
      EndDate: model.EndDate,
      FinancialYearName: model.FinancialYearName,
      Description: model.Description,
      IsDefault: model.IsDefault
    };
    this.codeservice
      .AddEditFinancialYearDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditFinancialYearDetail,
        addfinancialyear
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('FinancialYear Update Successfully!!!');
            this.getFinancialYearList();
            this.HidePopup();
          } else if (data.StatusCode === 900) {
            this.toastr.error('FinancialYear already exist!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.financialYearPopupLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  GetFinancialYearDetailById(data) {
    this.financialyeardata.FinancialYearId = data.data.FinancialYearId;
    this.financialyeardata.StartDate = data.data.StartDate;
    this.financialyeardata.EndDate = data.data.EndDate;
    this.financialyeardata.FinancialYearName = data.data.FinancialYearName;
    this.financialyeardata.Description = data.data.Description;
    this.financialyeardata.IsDefault = data.data.IsDefault;
    this.popupVisible = true;
  }
}
