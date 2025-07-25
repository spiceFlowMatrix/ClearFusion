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
  // officeTypeDropdown: OfficeTypeModel[];
  professionTypeDropdown: ProfessionTypeModel[];
  jobGradeTypeDropdown: JobGradeTypeDropdown[];
  //#endregion

  jobHiringDetails: JobHiringDetailsModel[];
  popupAddJobHiringVisible = false;
  isEditingAllowed = false;

  jobHiringForm: JobHiringDetailsModel;
  OfficeId: any;
  selectedOffice: number;
  officecodelist: any[];
  officeDropdownList: any[] = [];
  jobsLoading= false;


  // loader
  addJobHiringPopupLoading: boolean;

  constructor(
    private commonService: CommonService,
    private jobHiringService: JobHiringService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {
    this.initialize();

    // dropdowns
    this.getAllProfession();
    this.getAllJobGrade();

    // dataSource
  }

  ngOnInit() {
    // Office Id
    this.getOfficeCodeList();
    this.commonService.getEmployeeOfficeId().subscribe(data => {
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

  onOfficeSelected(officeId: number) {
    this.selectedOffice = officeId;
    this.getJobHiringDetails();
}

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
            (this.selectedOffice === null || this.selectedOffice == undefined)
                    ? this.officeDropdownList[0].OfficeId
                : this.selectedOffice;
        }
        this.getJobHiringDetails();
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
    this.jobsLoading = true;
    const officeId = this.selectedOffice;
   // this.OfficeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
    this.jobHiringService
      .GetJobHiringDetailByOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllJobHiringDetails,
        officeId
      )
      .subscribe(
        data => {
          this.jobHiringDetails = [];
          if (data.data.JobHiringDetailsList.length > 0) {
            this.jobsLoading = false;
            this.jobHiringDetails = data.data.JobHiringDetailsList;
          } else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong!');
            this.jobsLoading = false;
          } else {
            this.jobsLoading = false;
          }
          this.commonService.setLoader(false);
        },
        error => {
          this.jobsLoading = false;
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
    const officeId = this.selectedOffice;

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
