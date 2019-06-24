import { Component, OnInit, Input } from '@angular/core';
import { HrService } from '../../hr.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../../shared/application-pages-enum';
import { CommonService } from '../../../../service/common.service';
import { AppSettingsService } from '../../../../service/app-settings.service';
// import { parse } from 'path';

@Component({
  selector: 'app-health',
  templateUrl: './health.component.html',
  styleUrls: ['./health.component.css']
})
export class HealthComponent implements OnInit {
  //#region "Variables"
  @Input() employeeId: number;
  @Input() tabEventValue: number;
  isEditingAllowed = false;

  // healthForm: HealthDetailsModel;
  healthForm: any;

  healthQuestionDataSource: HealthQuestionModel[];

  // loader
  healthLoading = false;

  rateDS = [
    {
      rateId: 1,
      RateName: 'Normal'
    },
    {
      rateId: 2,
      RateName: 'Excellent'
    },
    {
      rateId: 3,
      RateName: 'Positive'
    }
  ];

  bloodGroupSource = [
    {
      BloodGroupId: 1,
      BloodGroup: 'A+'
    },
    {
      BloodGroupId: 2,
      BloodGroup: 'A-'
    },
    {
      BloodGroupId: 3,
      BloodGroup: 'B+'
    },
    {
      BloodGroupId: 4,
      BloodGroup: 'B-'
    },
    {
      BloodGroupId: 5,
      BloodGroup: 'O+'
    },
    {
      BloodGroupId: 6,
      BloodGroup: 'O-'
    },
    {
      BloodGroupId: 7,
      BloodGroup: 'AB+'
    },
    {
      BloodGroupId: 8,
      BloodGroup: 'AB-'
    }
  ];

  //#endregion

  constructor(
    private hrService: HrService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonservice: CommonService
  ) {}

  ngOnInit() {
    this.initializeForm();
    this.getHealthDetails();
    this.getHealthQuestion();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.Employees
    );
  }

  //#region "initializeForm"
  initializeForm() {
    this.healthQuestionDataSource = [];

    this.healthForm = {
      EmployeeHealthInfoId: null,
      EmployeeId: null,

      // HealthDetails
      PhysicanName: null,
      HospitalName: null,
      HospitalAddress: null,

      // Physical Exams
      Height: null,
      Weight: null,
      BloodPressure: null,
      VisualWithoutGlassesR: null,
      VisualWithoutGlassesL: null,
      VisualWithGlassesR: null,
      VisualWithGlassesL: null,
      HearingR: null,
      HearingRType: null,
      HearingL: null,
      HearingLType: null,
      HistoryOfPastIllness: null,
      HealthPresentCondition: null,
      ResultOfChestXRay: null,

      // Laboratory Test
      BloodGroup: null,
      Hbs: null,
      Hcv: null,
      OverallHealthCondition: null
    };
  }
  //#endregion

  //#region "getHealthDetails"
  getHealthDetails() {
    this.showHealthLoading();
    this.hrService
      .GetEmployeeHealthDetail(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeHealthInfo,
        this.employeeId
      )
      .subscribe(
        data => {
          if (data != null) {
            if (
              data.StatusCode === 200 &&
              data.data.EmployeeHealthInfo != null
            ) {
              this.healthForm = data.data.EmployeeHealthInfo;
              this.healthForm.HearingRType =
                data.data.EmployeeHealthInfo.HearingRType !== ''
                  ? parseInt(data.data.EmployeeHealthInfo.HearingRType)
                  : '';
              this.healthForm.HearingLType =
                data.data.EmployeeHealthInfo.HearingLType !== ''
                  ? parseInt(data.data.EmployeeHealthInfo.HearingLType)
                  : '';
              this.healthForm.Hbs =
                data.data.EmployeeHealthInfo.Hbs !== ''
                  ? parseInt(data.data.EmployeeHealthInfo.Hbs)
                  : '';
              this.healthForm.Hcv =
                data.data.EmployeeHealthInfo.Hcv !== ''
                  ? parseInt(data.data.EmployeeHealthInfo.Hcv)
                  : '';
              this.healthForm.OverallHealthCondition =
                data.data.EmployeeHealthInfo.OverallHealthCondition !== ''
                  ? parseInt(
                      data.data.EmployeeHealthInfo.OverallHealthCondition
                    )
                  : '';
            }
          }
          this.hideHealthLoading();
        },
        error => {
          this.hideHealthLoading();
        }
      );
  }
  //#endregion

  //#region "getHealthQuestion"
  getHealthQuestion() {
    this.showHealthLoading();
    this.hrService
      .GetEmployeeHealthDetail(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetEmployeeHealthQuestion,
        this.employeeId
      )
      .subscribe(
        data => {
          if (data != null) {
            if (
              data.StatusCode === 200 &&
              data.data.EmployeeHealthQuestionList != null
            ) {
              this.healthQuestionDataSource =
                data.data.EmployeeHealthQuestionList;
            }
          }
          this.hideHealthLoading();
        },
        error => {
          this.hideHealthLoading();
        }
      );
  }
  //#endregion

  //#region "addEditHealthInfo"
  addEditHealthInfo(data, API_Link: string) {
    this.hrService.AddByModel(API_Link, data).subscribe(
      data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Updated Successfully!!!');
        } else {
          this.toastr.error(data.Message);
        }
        this.getHealthDetails();
      },
      error => {}
    );
  }
  //#endregion

  //#region "addEditHealthQuestion"
  addEditHealthQuestion(data, API_Link: string) {
    this.hrService.AddByModel(API_Link, data).subscribe(
      data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Updated Successfully!!!');
        } else {
          this.toastr.error(data.Message);
        }
        this.getHealthQuestion();
      },
      error => {}
    );
  }
  //#endregion

  //#region "onHealthFormSubmit"
  onHealthFormSubmit(data: HealthDetailsModel) {
    this.showHealthLoading();

    if (data != null) {
      if (data.EmployeeHealthInfoId == null || data.EmployeeHealthInfoId === 0) {
        if (this.employeeId !== 0 || this.employeeId != null) {
          const addModel: HealthDetailsModel = {
            EmployeeHealthInfoId: 0,
            EmployeeId: this.employeeId,

            // HealthDetails
            PhysicanName: data.PhysicanName,
            HospitalName: data.HospitalName,
            HospitalAddress: data.HospitalAddress,

            // Physical Exams
            Height: data.Height,
            Weight: data.Weight,
            BloodPressure: data.BloodPressure,
            VisualWithoutGlassesR: data.VisualWithoutGlassesR,
            VisualWithoutGlassesL: data.VisualWithoutGlassesL,
            VisualWithGlassesR: data.VisualWithGlassesR,
            VisualWithGlassesL: data.VisualWithGlassesL,
            HearingR: data.HearingR,
            HearingRType: data.HearingRType,
            HearingL: data.HearingL,
            HearingLType: data.HearingLType,
            HistoryOfPastIllness: data.HistoryOfPastIllness,
            HealthPresentCondition: data.HealthPresentCondition,
            ResultOfChestXRay: data.ResultOfChestXRay,

            // Laboratory Test
            BloodGroup: data.BloodGroup,
            Hbs: data.Hbs,
            Hcv: data.Hcv,
            OverallHealthCondition: data.OverallHealthCondition
          };

          const apiLink =
            this.setting.getBaseUrl() +
            GLOBAL.API_EmployeeDetail_AddEmployeeHealthInfo;
          this.addEditHealthInfo(addModel, apiLink);
        }
        this.hideHealthLoading();
      } else {
        const editModel: HealthDetailsModel = {
          EmployeeHealthInfoId: data.EmployeeHealthInfoId,
          EmployeeId: data.EmployeeId,

          // HealthDetails
          PhysicanName: data.PhysicanName,
          HospitalName: data.HospitalName,
          HospitalAddress: data.HospitalAddress,

          // Physical Exams
          Height: data.Height,
          Weight: data.Weight,
          BloodPressure: data.BloodPressure,
          VisualWithoutGlassesR: data.VisualWithoutGlassesR,
          VisualWithoutGlassesL: data.VisualWithoutGlassesL,
          VisualWithGlassesR: data.VisualWithGlassesR,
          VisualWithGlassesL: data.VisualWithGlassesL,
          HearingR: data.HearingR,
          HearingRType: data.HearingRType,
          HearingL: data.HearingL,
          HearingLType: data.HearingLType,
          HistoryOfPastIllness: data.HistoryOfPastIllness,
          HealthPresentCondition: data.HealthPresentCondition,
          ResultOfChestXRay: data.ResultOfChestXRay,

          // Laboratory Test
          BloodGroup: data.BloodGroup,
          Hbs: data.Hbs,
          Hcv: data.Hcv,
          OverallHealthCondition: data.OverallHealthCondition
        };

        const apiLink =
          this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_EditEmployeeHealthInfo;
        this.addEditHealthInfo(editModel, apiLink);
        this.hideHealthLoading();
      }
    }
  }
  //#endregion

  //#region "logEvent"
  logEvent(actionName: string, obj) {
    if (actionName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_EditEmployeeHealthQuestion;

      this.addEditHealthQuestion(value, apiLink);
    } else if (actionName === 'RowInserting') {
      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_AddEmployeeHealthQuestion;

      const addModel: HealthQuestionModel = {
        EmployeeHealthQuestionId: 0,
        EmployeeId: this.employeeId,
        Question: obj.data.Question,
        Answer: obj.data.Answer
      };

      this.addEditHealthQuestion(addModel, apiLink);
    } else if (actionName === 'RowRemoving') {
      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_DeleteEmployeeHealthQuestion;
      const editModel: HealthQuestionModel = {
        EmployeeHealthQuestionId: obj.data.EmployeeHealthQuestionId,
        EmployeeId: obj.data.EmployeeId,
        Question: obj.data.Question,
        Answer: obj.data.Answer
      };
      this.addEditHealthQuestion(editModel, apiLink);
    }
  }
  //#endregion

  //#region "Loader"
  showHealthLoading() {
    this.healthLoading = true;
  }
  hideHealthLoading() {
    this.healthLoading = false;
  }
}

//#region "Classes"
class HealthDetailsModel {
  EmployeeHealthInfoId: number;
  EmployeeId: number;

  // HealthDetails
  PhysicanName: string;
  HospitalName: string;
  HospitalAddress: string;

  // Physical Exams
  Height: number;
  Weight: number;
  BloodPressure: number;
  VisualWithoutGlassesR: number;
  VisualWithoutGlassesL: number;
  VisualWithGlassesR: number;
  VisualWithGlassesL: number;
  HearingR: number;
  HearingRType: number;
  HearingL: number;
  HearingLType: number;
  HistoryOfPastIllness: string;
  HealthPresentCondition: string;
  ResultOfChestXRay: string;

  // Laboratory Test
  BloodGroup: number;
  Hbs: number;
  Hcv: number;
  OverallHealthCondition: number;
}
class HealthQuestionModel {
  EmployeeHealthQuestionId: number;
  EmployeeId: number;
  Question: string;
  Answer: string;
}
//#endregion
