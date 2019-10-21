import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap';
import { CodeService } from '../code.service';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { LeaveReason } from '../../../model/code-model';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-leavereason-type',
  templateUrl: './leavereason-type.component.html',
  styleUrls: ['./leavereason-type.component.css']
})
export class LeavereasonTypeComponent implements OnInit {
  popupVisible = false;
  showFilterRow: boolean;
  leavereasondt: LeaveReason[];
  leavereasondata: LeaveReason;
  isEditingAllowed = false;

  // loader
  leaveReasonPopupLoading = false;
  leaveReasonListLoading = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private codeservice: CodeService,
    private commonservice: CommonService
  ) {
    this.leavereasondata = this.codeservice.getLeaveReasonData();
    this.showFilterRow = true;
  }

  ngOnInit() {
    this.getLeaveReasonList();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.JournalCodes
    );
  }

  getLeaveReasonList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_LeaveReasonType
      )
      .subscribe(
        data => {
          this.leavereasondt = [];
          if (data.StatusCode === 200 && data.data.LeaveReasonList.length > 0) {
            data.data.LeaveReasonList.forEach(element => {
              this.leavereasondt.push({
                LeaveReasonId: element.LeaveReasonId,
                ReasonName: element.ReasonName,
                Unit: element.Unit
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

  onFormSubmit(model) {
    if (model.LeaveReasonId === '') {
      this.AddLeaveReasonDetail(model);
    } else {
      this.EditLeaveReasonDetail(model);
    }
  }

  AddLeaveReasonDetail(model) {
    this.leaveReasonPopupLoading = true;
    const obj: any = {};
    const addleavereason: LeaveReason = {
      LeaveReasonId: 0,
      ReasonName: model.ReasonName,
      Unit: model.Unit
    };
    this.codeservice
      .AddEditLeaveReasonDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddLeaveReasonDetail,
        addleavereason
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Leave Reason Added Successfully!!!');
            this.getLeaveReasonList();
            this.HidePopup();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Leave Reason already exist!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.leaveReasonPopupLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  EditLeaveReasonDetail(model) {
    this.leaveReasonPopupLoading = true;
    const obj: any = {};
    const addleavereason: LeaveReason = {
      LeaveReasonId: model.LeaveReasonId,
      ReasonName: model.ReasonName,
      Unit: model.Unit
    };
    this.codeservice
      .AddEditLeaveReasonDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditLeaveReasonDetail,
        addleavereason
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Leave Reason Updated Successfully!!!');
            this.getLeaveReasonList();
            this.HidePopup();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Leave Reason already exist!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.leaveReasonPopupLoading = false;
        },
        error => {
          // error message
          this.leaveReasonPopupLoading = false;
        }
      );
  }

  GetLeaveReasonDetailById(data) {
    this.leavereasondata.LeaveReasonId = data.data.LeaveReasonId;
    this.leavereasondata.ReasonName = data.data.ReasonName;
    this.leavereasondata.Unit = data.data.Unit;
    this.popupVisible = true;
  }

  showPopup() {
    this.leavereasondata = this.codeservice.getLeaveReasonData();
    this.popupVisible = true;
  }

  HidePopup() {
    this.popupVisible = false;
  }
}
