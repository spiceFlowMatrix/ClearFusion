import { Component, OnInit } from '@angular/core';
import {
  JobHiringService,
  JobHiringDetailsModel,
  OfficeTypeModel,
  ProfessionTypeModel,
  JobGradeTypeDropdown
} from './job-hiring.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-job-hiring-details',
  templateUrl: './job-hiring-details.component.html',
  styleUrls: ['./job-hiring-details.component.css']
})
export class JobHiringDetailsComponent implements OnInit {
  //#region "Dropdowns"
  officeTypeDropdown: OfficeTypeModel[];
  professionTypeDropdown: ProfessionTypeModel[];
  jobGradeTypeDropdown: JobGradeTypeDropdown[];
  //#endregion

  jobHiringDetails: JobHiringDetailsModel[];
  popupAddJobHiringVisible = false;
  isEditingAllowed = false;

  jobHiringForm: JobHiringDetailsModel;
  OfficeId: any;

  // loader
  addJobHiringPopupLoading: boolean;

  constructor(
    private commonService: CommonService,
    private jobHiringService: JobHiringService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {
    this.initialize();

    // dropdowns
    this.getOfficeCodeList();
    this.getAllProfession();
    this.getAllJobGrade();

    // dataSource
    this.getJobHiringDetails();
  }

  ngOnInit() {
    // Office Id
    this.commonService.getEmployeeOfficeId().subscribe(data => {
      this.getJobHiringDetails();
    });
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.Jobs
    );
  }

  //#region "Init Forms"
  initialize() {
    this.jobHiringForm = {
      JobId: 0,
      JobCode: null,
      JobDescription: null,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
      ProfessionId: 0,
      GradeId: 0,
      Unit: 0,
      IsActive: true
    };
  }
  //#endregion

  //#region "Show / Hide Popup"
  showHideAddJobHiringPopup() {
    this.GetJobCode();
    this.popupAddJobHiringVisible = !this.popupAddJobHiringVisible;
  }
  //#endregion

  //#region "On Form Submit"
  onFormSubmit(formName: string, e: any) {
    if ((formName = 'addJobHiringDetails')) {
      this.AddJobHiringDetails(e);
    }
  }
  //#endregion

  //#region  "onRowUpdating"
  logEvent(eventName: string, obj) {
    if (eventName === 'onRowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      this.editJobHiringDetail(value);
    }
  }
  //#endregion

  //#region "Get All Office"
  getOfficeCodeList() {
    this.jobHiringService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          this.officeTypeDropdown = [];

          const allOffices = [];
          const officeIds: any[] =
            localStorage.getItem('ALLOFFICES') != null
              ? localStorage.getItem('ALLOFFICES').split(',')
              : null;

          data.data.OfficeDetailsList.forEach(element => {
            allOffices.push(element);
          });

          officeIds.forEach(x => {
            const officeData = allOffices.filter(
              // tslint:disable-next-line:radix
              e => e.OfficeId === parseInt(x)
            )[0];
            this.officeTypeDropdown.push(officeData);
          });

          // sort in Asc
          this.commonService.sortDropdown(
            this.officeTypeDropdown,
            'OfficeName'
          );
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
  //#endregion

  //#region "Get All Profession"
  getAllProfession() {
    this.jobHiringService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllProfession
      )
      .subscribe(
        data => {
          this.professionTypeDropdown = [];

          data.data.ProfessionList.forEach(element => {
            this.professionTypeDropdown.push({
              ProfessionId: element.ProfessionId,
              ProfessionName: element.ProfessionName
            });
          });
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
  //#endregion

  //#region "Get All JobGrade"
  getAllJobGrade() {
    this.jobHiringService
      .GetAllDetails(this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllJobGrade)
      .subscribe(
        data => {
          this.jobGradeTypeDropdown = [];
          data.data.JobGradeList.forEach(element => {
            this.jobGradeTypeDropdown.push({
              GradeId: element.GradeId,
              GradeName: element.GradeName
            });
          });
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
  //#endregion "Get All Office"

  //#region Get All Job Hiring
  getJobHiringDetails() {
    // tslint:disable-next-line:radix
    this.OfficeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));

    this.jobHiringService
      .GetJobHiringDetailByOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllJobHiringDetails,
        this.OfficeId
      )
      .subscribe(
        data => {
          this.jobHiringDetails = [];
          // tslint:disable-next-line:curly
          if (data.data.JobHiringDetailsList.length > 0)
            this.jobHiringDetails = data.data.JobHiringDetailsList;
          // tslint:disable-next-line:curly
          else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

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
          this.commonService.setLoader(false);
        }
      );
  }
  //#endregion

  //#region "ADD Job"
  AddJobHiringDetails(model: any) {
    this.addJobHiringPopupLoading = true;

    this.jobHiringService
      .AddJobHiringDetail(
        this.setting.getBaseUrl() + GLOBAL.API_HR_AddJobHiringDetail,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully!!!');
            this.getJobHiringDetails();
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 900) this.toastr.error(data.Message);
          // tslint:disable-next-line:curly
          else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.showHideAddJobHiringPopup();
          this.addJobHiringPopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.addJobHiringPopupLoading = false;
        }
      );
  }
  //#endregion "ADD EMPLOYEE"

  //#region "Update Job Description"
  editJobHiringDetail(value) {
    this.jobHiringService
      .EditJobHiringDetail(
        this.setting.getBaseUrl() + GLOBAL.API_HR_EditJobHiringDetail,
        value
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Updated Successfully!!!');
          } else if (data.StatusCode === 900) {
            this.toastr.error(data.Message);
            this.getJobHiringDetails();
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
          this.getJobHiringDetails();
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

  //#region "ADD Job"
  GetJobCode() {
    this.addJobHiringPopupLoading = true;
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'), 32);

    this.jobHiringService
      .GetJobHiringDetailByOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_EmployeeHR_GetJobCode,
        officeId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.jobHiringForm = {
              JobId: 0,
              JobCode: data.data.JobCode,
              JobDescription: null,
              // tslint:disable-next-line:radix
              OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
              ProfessionId: 0,
              GradeId: 0,
              Unit: 0,
              IsActive: true
            };
            // this.toastr.success('Added Successfully!!!');
            this.addJobHiringPopupLoading = false;
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 900) this.toastr.error(data.Message);
          // tslint:disable-next-line:curly
          else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

            this.addJobHiringPopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }

          this.addJobHiringPopupLoading = false;
        }
      );
  }
  //#endregion "ADD EMPLOYEE"
}
