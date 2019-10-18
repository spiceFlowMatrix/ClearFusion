import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../code.service';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-job-grade',
  templateUrl: './job-grade.component.html',
  styleUrls: ['./job-grade.component.css']
})
export class JobGradeComponent implements OnInit {
  jobGradeFormData: JobGradeModel;
  jobGradedt: JobGradeModel[];
  jobGradePopupVisible = false;
  isEditingAllowed = false;

  // loader
  jobGradeListPopupLoading = false;
  jobGradePopupLoading = false;
  constructor(
    private codeservice: CodeService,
    private router: Router,
    private toastr: ToastrService,
    private setting: AppSettingsService,
    private commonservice: CommonService
  ) {}
  ngOnInit() {
    this.initializeForm();
    this.GetAllJobGradeList();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.JobGrade
    );
  }

  initializeForm() {
    this.jobGradeFormData = {
      GradeId: 0,
      GradeName: ''
    };
  }

  GetAllJobGradeList() {
    this.showHideJobGradeListPopupLoading(true);
    this.codeservice
      .GetAllDetails(this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllJobGrade)
      .subscribe(
        data => {
          this.jobGradedt = [];
          if (data.StatusCode === 200 && data.data.JobGradeList.length > 0) {
            data.data.JobGradeList.forEach(element => {
              this.jobGradedt.push({
                GradeId: element.GradeId,
                GradeName: element.GradeName
              });
            });
          }
          this.showHideJobGradeListPopupLoading(false);
        },
        error => {
          this.showHideJobGradeListPopupLoading(false);
        }
      );
  }

  AddJobGradeDetail(model) {
    this.jobGradePopupLoading = true;

    const addJobGrade: JobGradeModel = {
      GradeId: 0,
      GradeName: model.GradeName
    };
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_HR_AddJobGradeDetail,
        addJobGrade
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Grade Added Successfully!!!');
            this.GetAllJobGradeList();
          }
          this.HideJobGradePopup();
          this.jobGradePopupLoading = false;
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

  EditJobGradeDetail(model) {
    this.jobGradePopupLoading = true;
    const editJobGrade: JobGradeModel = {
      GradeId: model.GradeId,
      GradeName: model.GradeName
    };
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_HR_EditJobGradeDetail,
        editJobGrade
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Grade Updated Successfully!!!');
            this.GetAllJobGradeList();
          }
          this.HideJobGradePopup();
          this.jobGradePopupLoading = false;
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

  onFormSubmit(data: JobGradeModel) {
    // tslint:disable-next-line:curly
    if (data.GradeId === 0) this.AddJobGradeDetail(data);
    // tslint:disable-next-line:curly
    else this.EditJobGradeDetail(data);
  }

  showEditPopup(data: JobGradeModel) {
    this.jobGradeFormData = {
      GradeId: data.GradeId,
      GradeName: data.GradeName
    };

    this.jobGradePopupVisible = true;
  }

  //#region "Show / Hide"
  ShowJobGradePopup() {
    this.jobGradeFormData = {
      GradeId: 0,
      GradeName: null
    };
    this.jobGradePopupVisible = true;
  }
  HideJobGradePopup() {
    this.jobGradePopupVisible = false;
  }

  showHideJobGradeListPopupLoading(flag: boolean) {
    this.jobGradeListPopupLoading = flag;
  }
  //#endregion
}

export interface JobGradeModel {
  GradeId?: number;
  GradeName: string;
}
