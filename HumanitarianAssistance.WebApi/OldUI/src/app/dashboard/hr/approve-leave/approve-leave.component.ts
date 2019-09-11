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
import { CodeService } from '../../code/code.service';

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
  officeDropdownList: any[] = [];
  officecodelist: any[];
  selectedOffice: any;
  departmentTypeDropdown: any[];

  // Loader
  approveLeaveListloading: boolean;

  // popup
  popupApproveRejectVisible = false;
  approveRejectText: string;
  approverejectbuttonToggle: string;
  approveLeavePopupLoading: boolean;

  constructor(
    private hrService: HrService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService,
    private codeService: CodeService
  ) {
    this.selectedRowsList = [];
  }

  ngOnInit() {
    // this.commonService.getEmployeeOfficeId().subscribe(data => {
    //   this.GetAllLeaveDetails();
    // });
    this.getOfficeCodeList(); 
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.ApproveLeave
    );
  }

  onOfficeSelected(officeId: number) {
    this.selectedOffice = officeId;
    this.GetAllLeaveDetails();
  }

  // Leave Details
  //#region "Get All Leave Details"
  GetAllLeaveDetails() {
    this.approveLeaveListloading = true;
    this.hrService
      .GetAllDetailsByOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllEmployeeApplyLeaveList,
        // tslint:disable-next-line:radix
        this.selectedOffice
      )
      .subscribe(
        data => {
          this.leavedetailsDataSource = [];
          if (
            data.data.EmployeeApplyLeaveList != null &&
            data.data.EmployeeApplyLeaveList.length > 0
          ) {
            debugger;
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

  getOfficeCodeList() {
    this.codeService
        .GetAllCodeList(
            this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
        )
        .subscribe(
            data => {
                this.officecodelist = [];
                if (
                    data.StatusCode === 200 &&
                    data.data.OfficeDetailsList.length > 0
                ) {
                    data.data.OfficeDetailsList.forEach(element => {
                        this.officecodelist.push({
                            Office: element.OfficeId,
                            OfficeCode: element.OfficeCode,
                            OfficeName: element.OfficeName,
                            SupervisorName: element.SupervisorName,
                            PhoneNo: element.PhoneNo,
                            FaxNo: element.FaxNo,
                            OfficeKey: element.OfficeKey
                        });
                    });

                    const AllOffices = localStorage.getItem('ALLOFFICES').split(',');

                    data.data.OfficeDetailsList.forEach(element => {
                        const officeFound = AllOffices.indexOf('' + element.OfficeId);
                        if (officeFound !== -1) {
                            this.officeDropdownList.push({
                                OfficeId: element.OfficeId,
                                OfficeCode: element.OfficeCode,
                                OfficeName: element.OfficeName,
                                SupervisorName: element.SupervisorName,
                                PhoneNo: element.PhoneNo,
                                FaxNo: element.FaxNo,
                                OfficeKey: element.OfficeKey
                            });
                        }
                    });

                    this.selectedOffice =
                        (this.selectedOffice === null || this.selectedOffice === undefined)
                                ? this.officeDropdownList[0].OfficeId
                            : this.selectedOffice;
                            this.GetAllLeaveDetails();
                            this.getDepartmentType(this.selectedOffice);
                    // tslint:disable-next-line:curly
                } else if (data.StatusCode === 400)
                    this.toastr.error('Something went wrong!');
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

//#region "Get Department Type"
getDepartmentType(eventId: any) {
  this.hrService
    .GetDepartmentDropdown(
      this.setting.getBaseUrl() + GLOBAL.API_Code_GetDepartmentsByOfficeId,
      eventId
    )
    .subscribe(
      data => {
        this.departmentTypeDropdown = [];
        if (
          data.data.Departments != null &&
          data.data.Departments.length > 0
        ) {
          data.data.Departments.forEach(element => {
            this.departmentTypeDropdown.push(element);
          });
        // tslint:disable-next-line:curly
        } else if (data.StatusCode === 400)
          this.toastr.error('Something went wrong!');
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
  DepartmentId?: number;
}
