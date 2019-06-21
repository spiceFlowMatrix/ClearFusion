import { Component, OnInit, DebugElement } from '@angular/core';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../code.service';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-salary-tax-report-content',
  templateUrl: './salary-tax-report-content.component.html',
  styleUrls: ['./salary-tax-report-content.component.css']
})
export class SalaryTaxReportContentComponent implements OnInit {
  //#region "Variable"
  salaryTaxReportForm: salaryTaxReportModel;
  isEditingAllowed = false;

  // loader
  salaryTaxReportContentLoader = false;

  //#endregion

  constructor(
    private toastr: ToastrService,
    private setting: AppSettingsService,
    private codeservice: CodeService,
    private commonService: CommonService
  ) {}

  ngOnInit() {
    this.initializeForm();
    this.GetSalaryTaxReportContent();

    // Office Id
    this.commonService.getEmployeeOfficeId().subscribe(data => {
      this.GetSalaryTaxReportContent();
    });

    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.SalaryTaxReportContent
    );
  }

  initializeForm() {
    this.salaryTaxReportForm = {
      SalaryTaxReportContentId: 0,
      EmployerAuthorizedOfficerName: null,
      PositionAuthorizedOfficer: null,
      OfficeId: null
    };
  }

  //#region "GetSalaryTaxReportContent"
  GetSalaryTaxReportContent() {
    this.showSalaryTaxReportContentLoader();
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));

    this.codeservice
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_GetSalaryTaxReportContentDetails,
        'officeId',
        officeId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (data.data.SalaryTaxReportContentDetails != null) {
              this.salaryTaxReportForm = {
                SalaryTaxReportContentId:
                  data.data.SalaryTaxReportContentDetails
                    .SalaryTaxReportContentId,
                EmployerAuthorizedOfficerName:
                  data.data.SalaryTaxReportContentDetails
                    .EmployerAuthorizedOfficerName,
                PositionAuthorizedOfficer:
                  data.data.SalaryTaxReportContentDetails
                    .PositionAuthorizedOfficer,
                OfficeId: data.data.SalaryTaxReportContentDetails.OfficeId
              };
            } else {
              this.salaryTaxReportForm = {
                SalaryTaxReportContentId: 0,
                EmployerAuthorizedOfficerName: null,
                PositionAuthorizedOfficer: null,
                OfficeId: null
              };
            }
          }
          // else
          //   this.toastr.warning(data.Message);

          this.hideSalaryTaxReportContentLoader();
        },
        error => {}
      );
  }
  //#endregion

  //#region "onFormSubmit"
  onFormSubmit(data: any) {
    if (data != null) {
      if (
        data.SalaryTaxReportContentId === 0 ||
        data.SalaryTaxReportContentId == null
      ) {
        const dataModel: salaryTaxReportModel = {
          SalaryTaxReportContentId: 0,
          EmployerAuthorizedOfficerName: data.EmployerAuthorizedOfficerName,
          PositionAuthorizedOfficer: data.PositionAuthorizedOfficer,
          // tslint:disable-next-line:radix
          OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
        };
        this.AddSalaryTaxReportContentDetail(dataModel);
      } else {
        const dataModel: salaryTaxReportModel = {
          SalaryTaxReportContentId: data.SalaryTaxReportContentId,
          EmployerAuthorizedOfficerName: data.EmployerAuthorizedOfficerName,
          PositionAuthorizedOfficer: data.PositionAuthorizedOfficer,
          // tslint:disable-next-line:radix
          OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
        };
        this.EditSalaryTaxReportContentDetail(dataModel);
      }
    }
  }
  //#endregion

  //#region "AddSalaryTaxReportContentDetail"
  AddSalaryTaxReportContentDetail(data: salaryTaxReportModel) {
    this.showSalaryTaxReportContentLoader();

    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_AddSalaryTaxReportContentDetails,
        data
      )
      // tslint:disable-next-line:no-shadowed-variable
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Saved Successfully!!!');
        } else if (data.StatusCode === 900) {
          this.toastr.error(data.Message);
        } else {
          this.toastr.error('Something went wrong!');
        }
        this.hideSalaryTaxReportContentLoader();
      });
  }
  //#endregion

  //#region "EditSalaryTaxReportContentDetail"
  EditSalaryTaxReportContentDetail(data) {
    this.showSalaryTaxReportContentLoader();

    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_EditSalaryTaxReportContentDetails,
        data
      )
      // tslint:disable-next-line:no-shadowed-variable
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Saved Successfully!!!');
        } else if (data.StatusCode === 900) {
          this.toastr.error(data.Message);
        } else {
          this.toastr.error('Something went wrong!');
        }
        this.hideSalaryTaxReportContentLoader();
      });
  }
  //#endregion

  //#region "Loader"
  showSalaryTaxReportContentLoader() {
    this.salaryTaxReportContentLoader = true;
  }
  hideSalaryTaxReportContentLoader() {
    this.salaryTaxReportContentLoader = false;
  }

  //#endregion
}

// tslint:disable-next-line:class-name
class salaryTaxReportModel {
  SalaryTaxReportContentId: number;
  EmployerAuthorizedOfficerName: any;
  PositionAuthorizedOfficer: any;
  OfficeId: number;
}
