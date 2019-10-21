import { Component, OnInit } from '@angular/core';
import { ScheduleInterviewService } from './schedule-interview.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-schedule-interview',
  templateUrl: './schedule-interview.component.html',
  styleUrls: ['./schedule-interview.component.css']
})
export class ScheduleInterviewComponent implements OnInit {
  selectedIndex = 0;
  showInfoTabs: any[];
  tabValue = 0;
  currentDate = new Date();

  //#region "Schedule interview Variables"
  scheduleInterviewSearchFormData: any;
  jobCodeDropdown: JobCodeDropdown[];
  jobGradeDropdown: JobGradeDropdown[];
  professionTypeDropdown: any;
  interviewRoundDropdown: any;

  selectValueForJobCode: number;

  // form multi value selection
  selectedRowsList: any[];

  finalScheduledModel: FinalScheduledModel[];
  prospectiveEmployeeData: ProspectiveEmployeeData[];

  scheduleformData: any;

  // loader
  schedularInterviewListLoader = false;

  //#endregion

  constructor(
    private scheduleInterviewService: ScheduleInterviewService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonservice: CommonService
  ) {}
  ngOnInit() {
    //#region "TABS"
    this.showInfoTabs = [
      {
        id: 0,
        text: 'Schedule Interview'
      },
      {
        id: 1,
        text: 'Approval'
      },
      {
        id: 2,
        text: 'Approved Employee'
      }
    ];

    //#endregion "TABS"

    this.commonservice.setLoader(true);

    this.initialize();
    this.getjobCodeList();
    this.getAllJobGrade();
    this.getAllProfessionType();
  }

  initialize() {
    // init select list (important)
    this.selectedRowsList = [];

    this.scheduleInterviewSearchFormData = {
      JobCode: null
    };

    this.scheduleformData = {
      Job: null,
      Date: new Date()
    };
  }

  //#region "Get All Job Code"
  getjobCodeList() {
    this.scheduleInterviewService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllJobHiringDetails
      )
      .subscribe(
        data => {
          this.jobCodeDropdown = [];
          if (
            data.data.JobHiringDetailsList != null &&
            data.data.JobHiringDetailsList.length > 0
          ) {
            data.data.JobHiringDetailsList.forEach(element => {
              // Need only Active job
              if (element.IsActive === true) {
                this.jobCodeDropdown.push({
                  JobId: element.JobId,
                  JobCode: element.JobCode,
                  ProfessionId: element.ProfessionId,
                  ProfessionName: element.ProfessionName,
                  GradeId: element.GradeId,
                  GradeName: element.GradeName
                });
              }
            });
            this.selectValueForJobCode = this.jobCodeDropdown[0].JobId;
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
  //#endregion "Get All Job Code"

  //#region "Get All Job Grade"
  getAllJobGrade() {
    this.scheduleInterviewService
      .GetAllDetails(this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllJobGrade)
      .subscribe(
        data => {
          this.jobGradeDropdown = [];
          if (
            data.data.JobGradeList != null &&
            data.data.JobGradeList.length > 0
          ) {
            data.data.JobGradeList.forEach(element => {
              this.jobGradeDropdown.push({
                GradeId: element.GradeId,
                GradeName: element.GradeName
              });
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

  //#region "Get All Profession Type"
  getAllProfessionType() {
    this.scheduleInterviewService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllProfession
      )
      .subscribe(
        data => {
          this.professionTypeDropdown = [];
          if (
            data.data.ProfessionList != null &&
            data.data.ProfessionList.length > 0
          ) {
            data.data.ProfessionList.forEach(element => {
              this.professionTypeDropdown.push({
                ProfessionId: element.ProfessionId,
                ProfessionName: element.ProfessionName
              });
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
          } else {
          }
        }
      );
  }
  //#endregion "Get All Profession Type"

  //#region "Add Interview Schedule Details"
  addInterviewScheduleDetails(data) {
    this.commonservice.setLoader(true);

    this.scheduleInterviewService
      .AddInterviewScheduleDetails(
        this.setting.getBaseUrl() + GLOBAL.API_HR_AddInterviewScheduleDetails,
        data
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully!!!');
            this.initialize();
            this.onJobCodeSelectedValue(this.selectValueForJobCode);
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
          this.commonservice.setLoader(false);
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
          this.commonservice.setLoader(false);
        }
      );
  }
  //#endregion

  //#region "onJobCodeSelectedValue"
  onJobCodeSelectedValue(e) {
    this.selectValueForJobCode = e;
    // need Profession id
    const professionId = this.jobCodeDropdown.filter(p => p.JobId === e)[0]
      .ProfessionId;
    this.getProspectiveEmployeesByProfessionId(professionId);
  }
  //#endregion

  //#region "getProspectiveEmployeesForInterview"
  getProspectiveEmployeesByProfessionId(professionId: number) {
    this.scheduleInterviewService
      .GetProspectiveEmployeesByProfessionId(
        this.setting.getBaseUrl() +
          GLOBAL.API_HR_GetProspectiveEmployeesByProfessionId,
        professionId
      )
      .subscribe(
        data => {
          this.prospectiveEmployeeData = [];

          if (
            data.data.ISFPEmployeeList != null &&
            data.data.ISFPEmployeeList.length > 0
          ) {
            data.data.ISFPEmployeeList.forEach(element => {
              this.prospectiveEmployeeData.push({
                EmployeeId: element.EmployeeId,
                EmployeeCode: element.EmployeeCode,
                EmployeeName: element.EmployeeName,
                PhoneNo: element.PhoneNo,
                ProfessionId: element.ProfessionId,
                ProfessionName: element.ProfessionName,
                Resume: element.Resume
              });
            });
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.commonservice.setLoader(false);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.commonservice.setLoader(false);
        }
      );
  }

  //#endregion

  //#region "data Selected from grid"
  selectionChangedHandler(e) {
    // All list
    this.selectedRowsList = e.selectedRowsData;
  }

  //#endregion

  //#region "Form Value Set"
  onScheduleFormSet(data) {
    this.saveData(data);
  }
  //#endregion

  //#region "Save Data"
  saveData(data) {
    this.finalScheduledModel = [];
    if (this.selectedRowsList.length > 0) {
      this.selectedRowsList.forEach(element => {
        this.finalScheduledModel.push({
          EmployeeId: element.EmployeeId,
          JobId: this.selectValueForJobCode,
          GradeId: this.jobCodeDropdown.filter(
            p => p.JobId === this.selectValueForJobCode
          )[0].GradeId,
          InterviewStatus: 1,
          Date: data.Date,
          Time: '00 00 00 00',
          Approval1: null,
          Approval2: null,
          Approval3: null,
          Approval4: null
        });
      });
      this.addInterviewScheduleDetails(this.finalScheduledModel);
    } else {
      this.toastr.error('Please Select Employee');
    }
  }
  //#endregion

  //#region "on tab click"
  onTabClick(e) {
    this.tabValue = e.itemIndex;
  }
  //#endregion "on tab click"
}

//#region "Schedule Interview Models"
export class JobCodeDropdown {
  JobId: number;
  JobCode: string;
  ProfessionId?: number;
  ProfessionName: string;
  GradeId?: number;
  GradeName: string;
}

export class ScheduleInterviewSearchModel {
  JobCode: number;
  Date: Date;
}

export class ProspectiveEmployeeData {
  EmployeeId: number;
  EmployeeCode: string;
  EmployeeName: Date;
  PhoneNo: string;
  Resume: string;
  ProfessionId?: number;
  ProfessionName: string;
}

export class ProfessionTypeDropdown {
  ProfessionId: number;
  ProfessionName: string;
}

export class JobGradeDropdown {
  GradeId: number;
  GradeName: string;
}

export class FinalScheduledModel {
  EmployeeId: number;
  JobId: number;
  GradeId?: number;
  InterviewStatus: number;
  Date: Date;
  Time: string;
  Approval1: boolean;
  Approval2: boolean;
  Approval3: boolean;
  Approval4: boolean;
}
//#endregion "Schedule Interview"
