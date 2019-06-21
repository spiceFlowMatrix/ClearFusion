import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { GLOBAL } from '../../../shared/global';
import { CodeService } from '../code.service';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-pension-rate',
  templateUrl: './pension-rate.component.html',
  styleUrls: ['./pension-rate.component.css']
})
export class PensionRateComponent implements OnInit {
  financialyeardt: any[];
  popupVisible: boolean;
  pensionData: PensionModel;
  pensionDataSource: any;
  flag: any;
  isEditingAllowed = false;

  // loader
  financialYearPopupLoading = false;
  financialYearLoading = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private codeservice: CodeService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private commonservice: CommonService
  ) {}

  ngOnInit() {
    this.getFinancialYearList();
    this.GetAllPensionDetails();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.PensionRate
    );
  }

  Initialise() {
    this.pensionData = {
      FinancialYearId: null,
      FinancialYearName: null,
      IsDefault: false,
      PensionRate: null
    };
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
    this.flag = 0;
    this.Initialise();
    this.popupVisible = true;
  }

  onFormSubmit(model) {
    // tslint:disable-next-line:curly
    if (this.flag === 0) this.AddPensionRate(model);
    // tslint:disable-next-line:curly
    else if (this.flag === 1) this.EditPensionRate(model);
  }

  AddPensionRate(model) {
    this.showFinancialYearPopupLoading();
    this.codeservice
      .AssignEmployeeProjectPercentage(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddPensionRate,
        model
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Added successfully!');
          this.GetAllPensionDetails();
          this.HidePopup();
        }
        if (data.StatusCode === 700) {
          this.toastr.error(data.Message);
        }
        this.hideFinancialYearPopupLoading();
      });
  }

  EditPensionRate(model) {
    this.showFinancialYearPopupLoading();
    this.codeservice
      .AssignEmployeeProjectPercentage(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditPensionRate,
        model
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Updated successfully!');
          this.GetAllPensionDetails();
          this.HidePopup();
        }
        this.hideFinancialYearPopupLoading();
      });
  }

  GetAllPensionDetails() {
    this.showFinancialYearLoading();
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllPensionRate
      )
      .subscribe(data => {
        if (
          data.StatusCode === 200 &&
          data.data.EmployeePensionRateList.length > 0
        ) {
          this.pensionDataSource = data.data.EmployeePensionRateList;
        }
        this.hideFinancialYearLoading();
      });
  }

  onEditClick(rowData) {
    this.flag = 1;
    this.Initialise();
    this.pensionData.FinancialYearId = rowData.FinancialYearId;
    this.pensionData.FinancialYearName = rowData.FinancialYearName;
    this.pensionData.PensionRate = rowData.PensionRate;
    this.pensionData.IsDefault = rowData.IsDefault;
    this.popupVisible = true;
  }

  HidePopup() {
    this.popupVisible = false;
  }

  //#region " loading"
  showFinancialYearPopupLoading() {
    this.financialYearPopupLoading = true;
  }
  hideFinancialYearPopupLoading() {
    this.financialYearPopupLoading = false;
  }

  showFinancialYearLoading() {
    this.financialYearLoading = true;
  }
  hideFinancialYearLoading() {
    this.financialYearLoading = false;
  }
  //#endregion
}

interface PensionModel {
  FinancialYearId: number;
  FinancialYearName: string;
  PensionRate: DoubleRange;
  IsDefault: boolean;
}
