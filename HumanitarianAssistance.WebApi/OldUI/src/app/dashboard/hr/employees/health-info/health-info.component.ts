import { Component, OnInit, Input } from '@angular/core';
import { HrService } from '../../hr.service';
import { AccountsService } from '../../../accounts/accounts.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { HealthModel } from '../employees.component';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-health-info',
  templateUrl: './health-info.component.html',
  styleUrls: ['./health-info.component.css']
})
export class HealthInfoComponent implements OnInit {
  @Input() employeeId: number;
  @Input() tabEventValue: number;

  healthForm: boolean;
  healthModelAny: any[];
  healthModel: HealthModel;
  healthModelObj: HealthModel;
  bloodGroupSource: any;
  setFlagAddEdit = false;

  // loader
  healthInfoLoading = false;

  constructor(
    private hrService: HrService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.initializeModel();
    this.getHealthDetails(this.employeeId);
  }

  initializeModel() {
    this.bloodGroupSource = [
      {
        BloodGroup: 'A+'
      },
      {
        BloodGroup: 'A-'
      },
      {
        BloodGroup: 'B+'
      },
      {
        BloodGroup: 'B-'
      },
      {
        BloodGroup: 'O+'
      },
      {
        BloodGroup: 'O-'
      },
      {
        BloodGroup: 'AB+'
      },
      {
        BloodGroup: 'AB-'
      }
    ];
  }

  addHealthDetails() {
    this.healthModel = {
      EmployeeId: this.employeeId,
      BloodGroup: null,
      MedicalHistory: null,
      SmokeAndDrink: false,
      Insurance: false,
      MedicalInsurance: null,
      MeasureDieases: false,
      AllergicSubstance: false,
      FamilyHistory: false
    };
    this.healthForm = !this.healthForm;
  }

  editHealthDetails() {
    this.healthForm = !this.healthForm;
  }

  getHealthDetails(employeeId: number) {
    this.showHealthInfoLoading();

    this.hrService
      .GetEmployeeHealthDetail(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetEmployeeHealthDetail,
        employeeId
      )
      .subscribe(
        data => {
          this.setFlagAddEdit = false;
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeHealthInfoList.length > 0
          ) {
            this.healthModelObj = {};
            this.healthModel = {};
            data.data.EmployeeHealthInfoList.forEach(element => {
              this.setFlagAddEdit = true;
              this.healthModel = {
                AllergicSubstance: element.AllergicSubstance,
                BloodGroup: element.BloodGroup,
                EmployeeId: element.EmployeeId,
                FamilyHistory: element.FamilyHistory,
                HealthInfoId: element.HealthInfoId,
                Insurance: element.Insurance,
                MeasureDieases: element.MeasureDieases,
                MedicalHistory: element.MedicalHistory,
                MedicalInsurance: element.MedicalInsurance,
                SmokeAndDrink: element.SmokeAndDrink
              };
              this.healthModelObj = {
                AllergicSubstance: element.AllergicSubstance,
                BloodGroup: element.BloodGroup,
                EmployeeId: element.EmployeeId,
                FamilyHistory: element.FamilyHistory,
                HealthInfoId: element.HealthInfoId,
                Insurance: element.Insurance,
                MeasureDieases: element.MeasureDieases,
                MedicalHistory: element.MedicalHistory,
                MedicalInsurance: element.MedicalInsurance,
                SmokeAndDrink: element.SmokeAndDrink
              };
            });
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
          this.hideHealthInfoLoading();
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

  hideHealthPopup() {
    this.healthForm = !this.healthForm;
  }

  AddHealthInfo(model) {
    this.hrService
      .addHealthInfo(
        this.setting.getBaseUrl() + GLOBAL.API_HR_AddEmployeeHealthDetail,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.hideHealthPopup();
            this.toastr.success('Health Details Added Successfully!!!');
            this.getHealthDetails(this.employeeId);
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

  EditHealthInfo(model) {
    this.hrService
      .editHealthInfo(
        this.setting.getBaseUrl() + GLOBAL.API_HR_EditEmployeeHealthDetail,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Health Details Updated Successfully!!!');
            this.hideHealthPopup();
            this.getHealthDetails(this.employeeId);
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

  onFormSubmitHealth(model) {
    if (model.hasOwnProperty('HealthInfoId')) {
      this.EditHealthInfo(model);
    } else {
      this.AddHealthInfo(model);
    }
  }

  //#region "healthInfoLoading"

  showHealthInfoLoading() {
    this.healthInfoLoading = true;
  }
  hideHealthInfoLoading() {
    this.healthInfoLoading = false;
  }
}
