import { Component, OnInit } from '@angular/core';
import { OfficeData, CodeService } from '../code.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { OfficeCode, OfficeCodefordelete } from '../../../model/code-model';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-office-code',
  templateUrl: './office-code.component.html',
  styleUrls: ['./office-code.component.css']
})
export class OfficeCodeComponent implements OnInit {
  showFilterRow: boolean;
  popupVisible = false;
  ConfirmationPopup = false;
  officedata: OfficeData;
  officecodedt: OfficeCode[];
  officecodeid: any;
  officeCodeFlag: boolean;
  isEditingAllowed = false;

  maskRules = {
    // a single character
    'S': '$',

    // a regular expression
    'H': /^[0-9]{10,14}$/,

    // an array of characters
    'N': ['$', '%', '&', '@'],
  };

  // loader
  officeCodeListLoading = false;
  officeCodePopupLoading = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private codeservice: CodeService,
    private commonservice: CommonService
  ) {
    this.officedata = this.codeservice.getOfficeData();
    this.showFilterRow = true;
  }

  ngOnInit() {
    this.getOfficeCodeList();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.OfficeCodes
    );
  }

  showPopup() {
    this.officedata = this.codeservice.getOfficeData();
    this.popupVisible = true;
    this.officeCodeFlag = false;
  }

  HidePopup() {
    this.popupVisible = false;
  }

  ShowConfirmationPopup(data) {
    this.officecodeid = data.data.OfficeId;
    this.ConfirmationPopup = true;
  }

  HideConfirmationPopup() {
    this.ConfirmationPopup = false;
  }

  // Get all Office Details
  getOfficeCodeList() {
    this.showOfficeCodeListLoading();
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          this.officecodedt = [];

          if (
            data.StatusCode === 200 &&
            data.data.OfficeDetailsList.length > 0
          ) {
            data.data.OfficeDetailsList.forEach(element => {
              this.officecodedt.push({
                OfficeId: element.OfficeId,
                OfficeCode: element.OfficeCode,
                OfficeName: element.OfficeName,
                SupervisorName: element.SupervisorName,
                PhoneNo: element.PhoneNo,
                FaxNo: element.FaxNo,
                OfficeKey: element.OfficeKey
              });
            });
          }

          this.hideOfficeCodeListLoading();
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
          this.hideOfficeCodeListLoading();
        }
      );
  }

  onFormSubmit(model) {
    if (model.OfficeId === '') {
      this.AddOfficeCode(model);
    } else {
      this.EditOfficeCode(model);
    }
  }

  AddOfficeCode(model) {
    this.officeCodePopupLoading = true;
    const obj: any = {};
    const addofficecode: OfficeCode = {
      OfficeId: 0,
      OfficeCode: model.OfficeCode,
      OfficeName: model.OfficeName,
      SupervisorName: model.SupervisorName,
      PhoneNo: model.PhoneNo,
      FaxNo: model.FaxNo,
      OfficeKey: model.OfficeKey
    };
    this.codeservice
      .AddEditOfficeCode(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_AddOfficeDetail,
        addofficecode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Office Code Added Successfully!!!');
            this.getOfficeCodeList();
            this.officedata = this.codeservice.getOfficeData();
            this.HidePopup();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Office Code already exist!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.officeCodePopupLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  EditOfficeCode(model) {
    this.officeCodePopupLoading = true;
    const obj: any = {};
    const addofficecode: OfficeCode = {
      OfficeId: model.OfficeId,
      OfficeCode: model.OfficeCode,
      OfficeName: model.OfficeName,
      SupervisorName: model.SupervisorName,
      PhoneNo: model.PhoneNo,
      FaxNo: model.FaxNo,
      OfficeKey: model.OfficeKey
    };
    this.codeservice
      .AddEditOfficeCode(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_EditOfficeDetails,
        addofficecode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.HidePopup();
            this.toastr.success('Office Code Updated Successfully!!!');
            this.getOfficeCodeList();
            this.officedata = this.codeservice.getOfficeData();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Office Code already exist!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.officeCodePopupLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  DeleteOfficeCode() {
    const obj: any = {};
    const addofficecode: OfficeCodefordelete = {
      OfficeId: this.officecodeid
    };
    this.codeservice
      .DeleteOfficeCode(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_DeleteOfficeDetails,
        addofficecode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.HideConfirmationPopup();
            this.toastr.success('Office Code Deleted Successfully!!!');
            this.getOfficeCodeList();
          } else {
            this.toastr.error('Error!!!');
          }
        },
        error => {
          // error message
        }
      );
  }

  GetOfficeDetailByCode(data) {
    this.officedata.OfficeId = data.data.OfficeId;
    this.officedata.OfficeCode = data.data.OfficeCode;
    this.officedata.OfficeName = data.data.OfficeName;
    this.officedata.SupervisorName = data.data.SupervisorName;
    this.officedata.PhoneNo = data.data.PhoneNo;
    this.officedata.FaxNo = data.data.FaxNo;
    this.officedata.OfficeKey = data.data.OfficeKey;
    this.popupVisible = true;
    this.officeCodeFlag = true;
  }

  onFieldDataChanged(e) {
    if (e.dataField === 'PhoneNo') {
      const phone = e.value.toString();

      if (phone !== '') {
        if (phone.length > 14 || phone.length < 10) {
          this.toastr.warning('Phone Number should be between 10-14 digits!!!');
        }
      }
    }
  }

  functionCache = {};
  validateRange(min, max) {
    if (!this.functionCache[`min${min}max${max}`])
      this.functionCache[`min${min}max${max}`] = (options: any) => {
        return options.value >= min && options.value <= max;
      };
    return this.functionCache[`min${min}max${max}`];
  }

  showOfficeCodeListLoading() {
    this.officeCodeListLoading = true;
  }
  hideOfficeCodeListLoading() {
    this.officeCodeListLoading = false;
  }
}
