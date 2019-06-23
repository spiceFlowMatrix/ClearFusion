import { Component, OnInit } from '@angular/core';
import { HrService } from '../hr.service';
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
  selector: 'app-approve-leave',
  templateUrl: './approve-leave.component.html',
  styleUrls: ['./approve-leave.component.css']
})
export class ApproveLeaveComponent implements OnInit {
  leavedetailsDataSource: LeaveDetailsModel[];
  currentLeaveDetails: any;

  applyLeaveId: number;
  selectedRowsList: any[];
  selectedRows = {};
  isEditingAllowed = false;

  // Loader
  approveLeaveListloading: boolean;

  // popup
  popupApproveRejectVisible = false;
  approveRejectText: string;
  approverejectbuttonToggle: string;
  approveLeavePopupLoading: boolean;

  constructor(
    private hrService: HrService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService
  ) {
    this.selectedRowsList = [];
  }

  ngOnInit() {
    this.GetAllLeaveDetails();
    this.commonService.getEmployeeOfficeId().subscribe(data => {
      this.GetAllLeaveDetails();
    });
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.ApproveLeave
    );
  }

  // Leave Details
  //#region "Get All Leave Details"
  GetAllLeaveDetails() {
    this.approveLeaveListloading = true;
    this.hrService
      .GetAllDetailsByOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllEmployeeApplyLeaveList,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          this.leavedetailsDataSource = [];
          if (
            data.data.EmployeeApplyLeaveList != null &&
            data.data.EmployeeApplyLeaveList.length > 0
          ) {
            data.data.EmployeeApplyLeaveList.forEach(element => {
              this.leavedetailsDataSource.push(element);
              this.currentLeaveDetails = null;
              this.currentLeaveDetails = this.leavedetailsDataSource[0];
            });
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.approveLeaveListloading = false;
          this.commonService.setLoader(false);
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

  selectionChangedHandler(e) {
    // All list
    this.selectedRowsList = [];
    if (e.selectedRowsData.length > 0 && e.selectedRowsData != null) {
      e.selectedRowsData.forEach(element => {
        this.selectedRowsList.push({
          ApplyLeaveId: element.ApplyLeaveId
        });
      });
    }
  }

  conformApproveRejectLeave(approval: string) {
    this.approveLeavePopupLoading = true;
    if (approval === 'Approve') {
      this.hrService
        .ApproveRejectEmployeeLeave(
          this.setting.getBaseUrl() + GLOBAL.API_HR_ApproveEmployeeLeave,
          this.selectedRowsList
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Approved Successfully!!!');
              this.GetAllLeaveDetails();
              this.popupApproveRejectVisible = false;
            }
            this.approveLeavePopupLoading = false; // popup
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
    } else if (approval === 'Reject') {
      this.hrService
        .ApproveRejectEmployeeLeave(
          this.setting.getBaseUrl() + GLOBAL.API_HR_RejectEmployeeLeave,
          this.selectedRowsList
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.warning('Rejected !!!');
              this.GetAllLeaveDetails();
              this.popupApproveRejectVisible = false;
            }
            this.approveLeavePopupLoading = false; // popup
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
  }

  //#region "show / hide popup"
  showApproveRejectPopup(approval: string) {
    if (this.selectedRowsList.length > 0 && this.selectedRowsList != null) {
      this.popupApproveRejectVisible = true;

      if (approval === 'Approve') {
        this.approverejectbuttonToggle = 'true';
        this.approveRejectText = 'Are you sure to approve this leave ?';
      } else if (approval === 'Reject') {
        this.approverejectbuttonToggle = 'false';
        this.approveRejectText = 'Are you sure to reject this leave ?';
      }
    } else {
      this.toastr.error('Please select the items to ' + approval.toLowerCase());
    }
  }

  closeApproveRejectPopup() {
    this.popupApproveRejectVisible = false;
  }
  //#endregion
}

// Leave Details
export class LeaveDetailsModel {
  ApplyLeaveId?: number;
  EmployeeCode: any;
  EmployeeName: string;
  EmployeeId?: number;
  FromDate: Date;
  ToDate: Date;
  LeaveReasonId?: number;
  LeaveReasonName: string;
  ApplyLeaveStatusId?: number;
  ApplyLeaveStatus: string;
  Remarks: string;
}
