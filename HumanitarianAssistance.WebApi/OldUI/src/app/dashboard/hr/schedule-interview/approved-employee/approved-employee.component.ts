import { Component, OnInit, Input } from '@angular/core';
import { ScheduleInterviewService } from '../schedule-interview.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-approved-employee',
  templateUrl: './approved-employee.component.html',
  styleUrls: ['./approved-employee.component.css']
})
export class ApprovedEmployeeComponent implements OnInit {
  approvedEmployeeData: any;
  selectedRows: any;

  // input binding
  @Input() jobCodeDropdown: any[];
  @Input() jobGradeDropdown: any[];

  // loader
  approvedEmployeeListLoader = false;

  constructor(
    private scheduleInterviewService: ScheduleInterviewService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}
  ngOnInit() {
    this.getAllApprovedEmployeeList();
  }

  //#region "Get All Approved Employee"
  getAllApprovedEmployeeList() {
    this.approvedEmployeeListLoader = true;
    this.scheduleInterviewService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllApprovedEmployeeList
      )
      .subscribe(
        data => {
          this.approvedEmployeeData = [];
          if (
            data.data.InterviewApprovedEmployeeList != null &&
            data.data.InterviewApprovedEmployeeList.length > 0
          ) {
            data.data.InterviewApprovedEmployeeList.forEach(element => {
              this.approvedEmployeeData.push({
                EmployeeId: element.EmployeeId,
                EmployeeName: element.EmployeeName,
                PhoneNo: element.PhoneNo,
                JobId: element.JobId,
                JobCode: element.JobCode,
                GradeId: element.GradeId,
                GradeName: element.GradeName,
                Approval1: element.Approval1,
                Approval2: element.Approval2,
                Approval3: element.Approval3,
                Approval4: element.Approval4
              });
            });
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.approvedEmployeeListLoader = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.approvedEmployeeListLoader = false;
        }
      );
  }
  //#endregion "Get All Profession Type"
}
