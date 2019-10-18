import { Component, OnInit, Input } from '@angular/core';
import { ScheduleInterviewService } from '../schedule-interview.service';
import { Router } from '@angular/router';
import { GLOBAL } from '../../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-approvals',
  templateUrl: './approvals.component.html',
  styleUrls: ['./approvals.component.css']
})
export class ApprovalsComponent implements OnInit {
  scheduledProspectiveEmployeeData: any[];
  // form multi value selection
  selectedRowsList: any[];
  generalAssemblyApprovalList: any[];
  selectedRows: any;

  // datasource
  approveDataByGeneralAssembly: any[];
  approveDataByDirector: any[]; // grade != 1
  approveDataByGeneralAdmin: any[];
  approveDataByFieldOffice: any[];

  // popup
  popupSubmitApprovalVisible = false;

  // input binding
  @Input() jobCodeDropdown: any[];
  @Input() jobGradeDropdown: any[];

  // loader
  approveEmployeeListLoader = false;

  constructor(
    private scheduleInterviewService: ScheduleInterviewService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.initialize();
    this.getAllScheduledEmployeeList();
  }

  initialize() {
    // init select list (important)
    this.selectedRowsList = [];
  }

  //#region "Get All Scheduled Employee"
  getAllScheduledEmployeeList() {
    this.approveEmployeeListLoader = true;
    this.scheduleInterviewService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllScheduledEmployeeList
      )
      .subscribe(
        data => {
          this.approveDataByGeneralAssembly = []; // Grade == 1
          this.approveDataByDirector = []; // Grade > 1
          this.approveDataByGeneralAdmin = []; // Approved by  == 1 && 2
          this.approveDataByFieldOffice = []; // Approved by  == 1 && 2

          if (
            data.data.InterviewScheduleGeneralAssemblylist != null &&
            data.data.InterviewScheduleGeneralAssemblylist.length > 0
          ) {
            data.data.InterviewScheduleGeneralAssemblylist.forEach(element => {
              this.approveDataByGeneralAssembly.push({
                EmployeeId: element.EmployeeId,
                EmployeeName: element.EmployeeName,
                PhoneNo: element.PhoneNo,
                JobId: element.JobId,
                JobCode: element.JobCode,
                GradeId: element.GradeId,
                GradeName: element.GradeName,
                Approval1: false
              });
            });
          }

          if (
            data.data.InterviewScheduleDirectorlist != null &&
            data.data.InterviewScheduleDirectorlist.length > 0
          ) {
            data.data.InterviewScheduleDirectorlist.forEach(element => {
              this.approveDataByDirector.push({
                EmployeeId: element.EmployeeId,
                EmployeeName: element.EmployeeName,
                PhoneNo: element.PhoneNo,
                JobId: element.JobId,
                JobCode: element.JobCode,
                GradeId: element.GradeId,
                GradeName: element.GradeName,
                Approval2: false
              });
            });
          }

          if (
            data.data.InterviewScheduleGeneralAdminlist != null &&
            data.data.InterviewScheduleGeneralAdminlist.length > 0
          ) {
            data.data.InterviewScheduleGeneralAdminlist.forEach(element => {
              this.approveDataByGeneralAdmin.push({
                EmployeeId: element.EmployeeId,
                EmployeeName: element.EmployeeName,
                PhoneNo: element.PhoneNo,
                JobId: element.JobId,
                JobCode: element.JobCode,
                GradeId: element.GradeId,
                GradeName: element.GradeName,
                Approval3: false
              });
            });
          }

          if (
            data.data.InterviewScheduleFieldOfficelist != null &&
            data.data.InterviewScheduleFieldOfficelist.length > 0
          ) {
            data.data.InterviewScheduleFieldOfficelist.forEach(element => {
              this.approveDataByFieldOffice.push({
                EmployeeId: element.EmployeeId,
                EmployeeName: element.EmployeeName,
                PhoneNo: element.PhoneNo,
                JobId: element.JobId,
                JobCode: element.JobCode,
                GradeId: element.GradeId,
                GradeName: element.GradeName,
                Approval4: false
              });
            });
          }
          this.approveEmployeeListLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.approveEmployeeListLoader = false;
        }
      );
  }
  //#endregion

  switchValueChanged(moduleName, cell, e) {
    if (moduleName === 'generalAssembly') {
      this.approveDataByGeneralAssembly[cell.row.rowIndex].Approval1 = e;
    } else if (moduleName === 'director') {
      this.approveDataByDirector[cell.row.rowIndex].Approval2 = e;
    } else if (moduleName === 'generalAdmin') {
      this.approveDataByGeneralAdmin[cell.row.rowIndex].Approval3 = e;
    } else if (moduleName === 'fieldOfficeManager') {
      this.approveDataByFieldOffice[cell.row.rowIndex].Approval4 = e;
    }
  }

  //#region "data Selected from grid"
  selectionChangedHandler(e) {
    // All list
    this.selectedRowsList = e.selectedRowsData;
  }
  //#endregion

  approveEmployee(moduleName: string, event) {
    if (moduleName === 'generalAssembly') {
      this.interviewApprovals(this.approveDataByGeneralAssembly, 1);
    } else if (moduleName === 'director') {
      this.interviewApprovals(this.approveDataByDirector, 2);
    } else if (moduleName === 'generalAdmin') {
      this.interviewApprovals(this.approveDataByGeneralAdmin, 3);
    } else if (moduleName === 'fieldOfficeManager') {
      this.interviewApprovals(this.approveDataByFieldOffice, 4);
    }
  }

  //#region "Final Approved Function"
  interviewApprovals(model: any, approvalId: number) {
    this.approveEmployeeListLoader = true;
    this.scheduleInterviewService
      .InterviewApprovals(
        this.setting.getBaseUrl() + GLOBAL.API_HR_InterviewApprovals,
        model,
        approvalId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Approved Successfully!!!');
            this.initialize();
            this.getAllScheduledEmployeeList();
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.approveEmployeeListLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 400) {
            this.toastr.error('Something Went Wrong....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.approveEmployeeListLoader = false;
        }
      );
  }
  //#endregion
}
