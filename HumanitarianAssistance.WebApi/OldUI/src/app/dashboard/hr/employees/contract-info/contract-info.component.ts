import { Component, OnInit, Input } from '@angular/core';
import { HrService } from '../../hr.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { CodeService } from '../../../code/code.service';
import { DomSanitizer } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { ThirdPartyKey } from '../../../../shared/thirdPartyKey';
import {
  applicationPages,
  applicationModule
} from '../../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService, GoogleObj } from '../../../../service/common.service';

declare let jsPDF;
declare var $: any;

@Component({
  selector: 'app-contract-info',
  templateUrl: './contract-info.component.html',
  styleUrls: ['./contract-info.component.css']
})
export class ContractInfoComponent implements OnInit {
  //#region
  @Input() employeeId: number;

  contractInfoDataSource: EmployeeContractModel[];
  employeeContractForm: EmployeeContractModel;
  designationDropdown: any[];
  selectedEmployeeDetails: any[];
  projectBudgetLineDataSource: any[];
  officeListDataSource: any[];
  stateDropdown: any[];
  projectDataSource: any[];
  countryDropdown: any[];
  isEditingAllowed = false;
  employeeListDataSource: any[];
  googleKey: string;

  contractDetails: any; // report content

  // popup
  popupContractVisible = false;
  contractEmployeePopup = false;

  public googleObj: GoogleObj = new GoogleObj();

  gradeDropDown = [
    {
      GradeId: 1,
      GradeName: 'Grade - 1'
    },
    {
      GradeId: 2,
      GradeName: 'Grade - 2'
    },
    {
      GradeId: 3,
      GradeName: 'Grade - 3'
    },
    {
      GradeId: 4,
      GradeName: 'Grade - 4'
    },
    {
      GradeId: 5,
      GradeName: 'Grade - 5'
    },
    {
      GradeId: 6,
      GradeName: 'Grade - 6'
    },
    {
      GradeId: 7,
      GradeName: 'Grade - 7'
    },
    {
      GradeId: 8,
      GradeName: 'Grade - 8'
    },
    {
      GradeId: 9,
      GradeName: 'Grade - 9'
    },
    {
      GradeId: 10,
      GradeName: 'Grade - 10'
    },
    {
      GradeId: 11,
      GradeName: 'Grade - 11'
    },
    {
      GradeId: 12,
      GradeName: 'Grade - 12'
    }
  ];

  // loader
  contractLoading = false;
  contractListLoading = false;
  //#endregion

  constructor(
    private hrService: HrService,
    private codeService: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonService: CommonService,
    private translate: TranslateService,
    private _DomSanitizer: DomSanitizer
  ) {
    translate.setDefaultLang('fa');
    this.googleKey = ThirdPartyKey.googleKey;
  }

  ngOnInit() {
    this.initializeForm();
    this.GetAllDesignationDropdown();
    this.getAllOfficeList();
    this.getAllCountry();
    this.getAllProjectDetails();
    this.GetAllContractInfo();
    this.getAllEmployeeListByOfficeId();
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.Employees
    );
  }

  initializeForm() {
    this.employeeContractForm = {
      EmployeeId: this.employeeId,
      FatherName: null,
      EmployeeCode: null,
      Designation: null,
      ContractStartDate: null,
      ContractEndDate: null,
      DurationOfContract: 0,
      Salary: 0,
      Grade: null,
      Project: null,
      DutyStation: null,
      Country: null,
      Province: null,
      BudgetLine: null,
      Job: null,
      WorkTime: 0,
      WorkDayHours: 0,
      EmployeeContractId: 0,
      CountryDari: '',
      DesignationDari: '',
      DutyStationDari: '',
      FatherNameDari: '',
      GradeDari: '',
      JobDari: '',
      OfficeId: 0,
      ProvinceDari: '',
      EmployeeNameDari: '',
      GradeName: '',
      ProjectNameDari: '',
      BudgetLineDari: ''
    };

    this.contractDetails = {
      EmployeeName: null,
      FatherName: null,
      EmployeeCode: null,
      Designation: null,
      ContractStartDate: null,
      ContractEndDate: null,
      DurationOfContract: null,
      Salary: null,
      Grade: null,
      ProjectName: null,
      ProjectCode: null,
      DutyStation: null,
      Province: null,
      BudgetLine: null,
      Job: null,
      WorkTime: null,
      WorkDayHours: null,
      ContentEnglish: null,
      ContentDari: null,
      EmployeeImage: null,
      CountryDari: '',
      DesignationDari: '',
      DutyStationDari: '',
      FatherNameDari: '',
      GradeDari: '',
      JobDari: '',
      OfficeId: 0,
      ProvinceDari: '',
      GradeName: '',
      ProjectNameDari: '',
      BudgetLineDari: ''
    };
  }

  //#region "GetAllContractInfo"
  GetAllContractInfo() {
    this.showHideContractLoading(true);
    this.hrService
      .GetEmployeeHealthDetail(
        this.setting.getBaseUrl() +
          GLOBAL.API_Hr_GetSelectedEmployeeContractByEmployeeId,
        this.employeeId
      )
      .subscribe(
        data => {
          if (data != null) {
            if (
              data.StatusCode === 200 &&
              data.data.EmployeeContractDetails != null
            ) {
              if (data.data.EmployeeContractDetails.length > 0) {
                this.contractInfoDataSource = data.data.EmployeeContractDetails;
              }
            }
          }
          this.showHideContractLoading(false);
        },
        error => {
          this.showHideContractLoading(false);
        }
      );
  }
  //#endregion

  //#region "Get All Employee Details By EmployeeId"
  getEmployeeDetailByEmployeeId(employeeId: number) {
    this.hrService
      .GetEmployeeDataByEmployeeId(
        this.setting.getBaseUrl() +
          GLOBAL.API_Code_GetEmployeeDetailByEmployeeId,
        employeeId
      )
      .subscribe(
        data => {
          this.selectedEmployeeDetails = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeDetailListData != null
          ) {
            if (data.data.EmployeeDetailListData.length > 0) {
              data.data.EmployeeDetailListData.forEach(element => {
                this.selectedEmployeeDetails.push(element);
              });
              this.employeeContractForm.FatherName = this.selectedEmployeeDetails[0].FathersName;
              this.employeeContractForm.EmployeeCode = this.selectedEmployeeDetails[0].EmployeeCode;
              this.employeeContractForm.ContractEndDate = this.selectedEmployeeDetails[0].ContractEndDate;
              this.employeeContractForm.ContractStartDate = this.selectedEmployeeDetails[0].ContractStartDate;
              this.employeeContractForm.DutyStation = this.selectedEmployeeDetails[0].DutyStation;
              this.employeeContractForm.OfficeId = this.selectedEmployeeDetails[0].officeId;
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "Get Designation Type"
  GetAllDesignationDropdown() {
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllDesignation
      )
      .subscribe(
        data => {
          this.designationDropdown = [];
          if (data.data.DesignationList != null) {
            if (data.data.DesignationList.length > 0) {
              data.data.DesignationList.forEach(element => {
                this.designationDropdown.push(element);
              });
            }
            // tslint:disable-next-line:curly
          } else this.toastr.error(data.Message);
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllOfficeList"
  getAllOfficeList() {
    this.codeService
      .GetAllCodeList(this.setting.getBaseUrl() + GLOBAL.API_AllOffice_URL)
      .subscribe(
        data => {
          this.officeListDataSource = [];

          if (data.data.OfficeDetailsList != null && data.StatusCode === 200) {
            if (data.data.OfficeDetailsList.length > 0) {
              data.data.OfficeDetailsList.forEach(element => {
                this.officeListDataSource.push({
                  OfficeId: element.OfficeId,
                  OfficeName: element.OfficeName
                });
              });

              this.officeListDataSource = this.commonService.sortDropdown(
                this.officeListDataSource,
                'OfficeName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllCountry"
  getAllCountry() {
    this.hrService
      .GetAllCountry(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllCountry)
      .subscribe(
        data => {
          this.countryDropdown = [];

          if (data.data.CountryDetailsList != null && data.StatusCode === 200) {
            if (data.data.CountryDetailsList.length > 0) {
              data.data.CountryDetailsList.forEach(element => {
                this.countryDropdown.push(element);
              });

              this.countryDropdown = this.commonService.sortDropdown(
                this.countryDropdown,
                'CountryName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllState"
  getAllState(e) {
    this.hrService
      .GetAllProvinceDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllProvinceDetails,
        e
      )
      .subscribe(
        data => {
          this.stateDropdown = [];

          if (
            data.data.ProvinceDetailsList != null &&
            data.StatusCode === 200
          ) {
            if (data.data.ProvinceDetailsList.length > 0) {
              data.data.ProvinceDetailsList.forEach(element => {
                this.stateDropdown.push(element);
              });

              this.stateDropdown = this.commonService.sortDropdown(
                this.stateDropdown,
                'ProvinceName'
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllProjectDetails"
  getAllProjectDetails() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_GetAllProjectDetail
      )
      .subscribe(
        data => {
          this.projectDataSource = [];
          if (data != null) {
            if (
              data.data.ProjectDetailList != null &&
              data.StatusCode === 200
            ) {
              if (data.data.ProjectDetailList.length > 0) {
                data.data.ProjectDetailList.forEach(element => {
                  this.projectDataSource.push({
                    ProjectId: element.ProjectId,
                    ProjectName: element.ProjectName,
                    ProjectNameCode: element.ProjectCode + '-' + element.ProjectName
                  });
                });
              }
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "Get All Budget Lines Details"
  getAllBudgetLineDetails(ProjectId: any) {
    this.codeService
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_GetAllBudgetLineDetails,
        ProjectId
      )
      .subscribe(
        data => {
          this.projectBudgetLineDataSource = [];
          if (
            data.data.ProjectBudgetLineDetailList != null &&
            data.StatusCode === 200
          ) {
            data.data.ProjectBudgetLineDetailList.forEach(element => {
              this.projectBudgetLineDataSource.push(element);
            });
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "AddEmployeeContract"
  AddEmployeeContract(data: any) {
    this.showHideContractLoading(true);

    this.hrService
      .addProfessionalInfo(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_AddEmployeeContractDetails,
        data
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          // tslint:disable-next-line:curly
          if (data.StatusCode === 200)
            this.toastr.success('Contract Updated Successfully !');
          else {
            this.toastr.warning(data.Message);
          }
          this.showHideContractLoading(false);
          this.hideAddContractPopup();

          this.GetAllContractInfo();
        },
        error => {
          this.showHideContractLoading(false);
        }
      );
  }
  //#endregion

  //#region "onemployeeContractFormSubmit"
  onEmployeeContractFormSubmit(data: any) {
     
    const dataModel: EmployeeContractModel = {
      EmployeeId: data.EmployeeId,
      FatherName: data.FatherName,
      EmployeeCode: data.EmployeeCode,
      Designation: data.Designation,
      ContractStartDate:
        data.ContractStartDate != null
          ? new Date(
              new Date(data.ContractStartDate).getFullYear(),
              new Date(data.ContractStartDate).getMonth(),
              new Date(data.ContractStartDate).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            )
          : null,
      ContractEndDate:
        data.ContractEndDate != null
          ? new Date(
              new Date(data.ContractEndDate).getFullYear(),
              new Date(data.ContractEndDate).getMonth(),
              new Date(data.ContractEndDate).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            )
          : null,
      DurationOfContract: data.DurationOfContract,
      Salary: data.Salary,
      Grade: data.Grade,
      Project: data.Project,
      DutyStation: data.DutyStation,
      Country: data.Country,
      Province: data.Province,
      BudgetLine: data.BudgetLine,
      Job: data.Job,
      WorkTime: data.WorkTime,
      WorkDayHours: data.WorkDayHours,
      EmployeeContractId: data.EmployeeContractId,
      CountryDari: data.CountryDari,
      DesignationDari: data.DesignationDari,
      DutyStationDari: data.DutyStationDari,
      FatherNameDari: data.FatherNameDari,
      GradeDari: data.GradeDari,
      JobDari: data.JobDari,
      ProvinceDari: data.ProvinceDari,
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'), 32),
      EmployeeNameDari: data.EmployeeNameDari,
      GradeName: data.GradeName,
      ProjectNameDari: data.ProjectNameDari,
      BudgetLineDari: data.BudgetLineDari
    };

    this.AddEmployeeContract(dataModel);
  }
  //#endregion

  //#region "onFieldDataChanged"
  onFieldDataChanged(e) {
     
    if (e != null) {
      let fieldName = '';
      let value = '';

      switch (e.dataField) {
        case 'Project': {
          if (e.value != null && e.value !== 0) {
            fieldName = 'Project';
            const project = this.projectDataSource.find(
              x => x.ProjectId === e.value
            );
            if (project !== null) {
              value = project.ProjectNameCode;
            }
            this.projectBudgetLineDataSource = [];
            this.getAllBudgetLineDetails(e.value);
          }
          break;
        }
        case 'Country': {
          if (e.value != null && e.value !== 0) {
            this.stateDropdown = [];
            this.getAllState(e.value);
            fieldName = 'Country';
            const country = this.countryDropdown.find(
              x => x.CountryId === e.value
            );
            if (country != null) {
              value = country.CountryName;
            }
          }
          break;
        }
        case 'Designation': {
          if (e.value != null && e.value !== 0) {
            const designation = this.designationDropdown.find(
              x => x.DesignationId === e.value
            );
            if (designation !== null) {
              this.googleTranslate(designation.Designation, e.dataField);
            }
          }
          break;
        }
        case 'DutyStation': {
          if (e.value != null && e.value !== 0) {
            fieldName = 'DutyStation';
            const dutyStation = this.officeListDataSource.find(
              x => x.OfficeId === e.value
            );
            if (dutyStation != null) {
              value = dutyStation.OfficeName;
            }
          }
          break;
        }
        case 'Province': {
          if (e.value != null && e.value !== 0) {
            fieldName = 'Province';
            const provinces = this.stateDropdown.find(
              x => x.ProvinceId === e.value
            );
            if (provinces != null) {
              value = provinces.ProvinceName;
            }
          }
          break;
        }
        case 'Job': {
          if (e.value != null && e.value !== 0) {
            this.googleTranslate(e.value, e.dataField);
          }
          break;
        }
        case 'FatherName': {
          if (e.value != null && e.value !== 0) {
            this.googleTranslate(e.value, e.dataField);
          }
          break;
        }
        case 'Grade': {
          if (e.value != null && e.value !== 0) {
            fieldName = 'Grade';
            const grade = this.gradeDropDown.find(x => x.GradeId === e.value);
            if (grade !== null) {
              value = grade.GradeName;
            }
            this.googleTranslate(e.value, e.dataField);
          }
          break;
        }
        case 'EmployeeId': {
          if (e.value != null && e.value !== 0) {
            fieldName = 'EmployeeId';
            const employee = this.employeeListDataSource.find(
              x => x.EmployeeId === e.value
            );
            if (employee !== null) {
              value = employee.EmployeeName;
            }
          }
          break;
        }
        case 'BudgetLine': {
          if (e.value != null && e.value !== 0) {
            fieldName = 'BudgetLine';
            const budgetLine = this.projectBudgetLineDataSource.find(
              x => x.BudgetLineId === e.value
            );
            if (budgetLine !== null) {
              value = budgetLine.BudgetCodeName;
            }
          }
          break;
        }
      }

      this.googleTranslate(value, fieldName);
    }
  }
  //#endregion

  //#region "ShowAddContractPopup"
  ShowAddContractPopup() {
    this.employeeContractForm = {
      EmployeeId: this.employeeId,
      FatherName: null,
      EmployeeCode: null,
      Designation: null,
      ContractStartDate: null,
      ContractEndDate: null,
      DurationOfContract: 0,
      Salary: 0,
      Grade: null,
      Project: null,
      DutyStation: null,
      Country: null,
      Province: null,
      BudgetLine: null,
      Job: null,
      WorkTime: 0,
      WorkDayHours: 0,
      EmployeeContractId: 0,
      CountryDari: null,
      DesignationDari: null,
      DutyStationDari: null,
      FatherNameDari: null,
      GradeDari: null,
      JobDari: null,
      OfficeId: 0,
      ProvinceDari: null,
      EmployeeNameDari: null,
      GradeName: null,
      ProjectNameDari: null,
      BudgetLineDari: null
    };

    this.selectedEmployeeDetails = [];
    this.getEmployeeDetailByEmployeeId(this.employeeId);

    this.showAddContractPopup();
  }
  //#endregion

  //#region "getContractDetailReport"
  getContractDetailReport(data) {
    if (data != null) {
      if (data.key != null) {
        this.contractDetails = data.key;
        this.contractDetails.EmployeeImage =
          this.setting.getDocUrl() + data.key.EmployeeImage;

        console.log(this.contractDetails.EmployeeImage);

        this.showcontractEmployeePopup();
      }
    }
  }
  //#endregion

  //#region "RemoveEmployeeContract"
  logEvent(eventName: string, event: any) {
    if (eventName === 'RowRemoved') {
      const employeeContractId = event.data.EmployeeContractId;

      this.showHideContractLoading(true);

      this.hrService
        .DeleteEmployeeContractDetail(
          this.setting.getBaseUrl() +
            GLOBAL.API_Hr_RemoveEmployeeContractDetails,
          employeeContractId
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success('Contract Deleted Successfully !!!');
              this.GetAllContractInfo();
            } else {
              this.toastr.warning(data.Message);
            }
            this.showHideContractLoading(false);
            // this.GetAllContractInfo();
          },
          error => {
            this.showHideContractLoading(false);
          }
        );
    }
  }
  //#endregion

  //#region "generatePdf"
  // tslint:disable-next-line:member-ordering
  base64Img = null;
  // tslint:disable-next-line:member-ordering
  margins = {
    top: 70,
    bottom: 40,
    left: 30,
    width: 550
  };
  generatePdf() {
    let printContents, popupWin;
    printContents = document.getElementById('contractReportPdf').innerHTML;
    popupWin = window.open('', '_blank', '');
    popupWin.document.open();
    popupWin.document.write(`
    <html>
    <head>
        <title></title>
        <style>
        //........Customized style.......
        </style>
    </head>
    <body onload="window.print();window.close()">${printContents}</body>
    </html>`);
    popupWin.document.close();

    // var pdf = new jsPDF('p', 'pt', 'legal'),
    //   pdfConf = {
    //     // pagesplit: true,
    //     background: '#fff'
    //   };
  }
  //#endregion

  //#region "Get All Employee List By OfficeId"
  getAllEmployeeListByOfficeId() {
    // tslint:disable-next-line:radix
    const officeId = parseInt(localStorage.getItem('EMPLOYEEOFFICEID'));
    this.hrService
      .GetAllDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetEmployeeDetailByOfficeId,
        officeId
      )
      .subscribe(
        data => {
          this.employeeListDataSource = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeDetailListData != null &&
            data.data.EmployeeDetailListData.length > 0
          ) {
            data.data.EmployeeDetailListData.forEach(element => {
              this.employeeListDataSource.push(element);
            });

            this.commonService.sortDropdown(
              this.employeeListDataSource,
              'CodeEmployeeName'
            );
          } else {
            // tslint:disable-next-line:curly
            if (data.data.EmployeeDetailListData == null)
              this.toastr.warning('No record found!');
            // tslint:disable-next-line:curly
            else if (data.StatusCode === 400)
              this.toastr.error('Something went wrong!');
          }
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

  selectionChangedHandler(event) {}

  //#region "Show / Hide"
  showHideContractLoading(flag: boolean) {
    this.contractListLoading = flag;
  }

  showAddContractPopup() {
    this.popupContractVisible = true;
  }
  hideAddContractPopup() {
    this.popupContractVisible = false;
  }

  // showContractListLoading() {
  //   this.contractListLoading = true;
  // }
  // hideContractListLoading() {
  //   this.contractListLoading = false;
  // }

  showcontractEmployeePopup() {
    this.contractEmployeePopup = true;
  }
  hideContractEmployeePopup() {
    this.contractEmployeePopup = false;
  }

  //#endregion

  //#region "Translate Function"
  googleTranslate(inputText, fieldName): string {
    let translatedValue = '';

    this.googleObj.q = inputText;
    this.commonService.translate(this.googleObj, this.googleKey).subscribe(
      (res: any) => {
        const translatedData = JSON.parse(res._body);
        translatedValue =
          translatedData.data.translations.length > 0
            ? translatedData.data.translations[0].translatedText
            : '';

        switch (fieldName) {
          case 'Designation': {
            this.employeeContractForm.DesignationDari = translatedValue;
            break;
          }
          case 'Country': {
            this.employeeContractForm.CountryDari = translatedValue;
            break;
          }
          case 'DutyStation': {
            this.employeeContractForm.DutyStationDari = translatedValue;
            break;
          }
          case 'Province': {
            this.employeeContractForm.ProvinceDari = translatedValue;
            break;
          }
          case 'Job': {
            this.employeeContractForm.JobDari = translatedValue;
            break;
          }
          case 'FatherName': {
            this.employeeContractForm.FatherNameDari = translatedValue;
            break;
          }
          case 'Grade': {
            this.employeeContractForm.GradeDari = translatedValue;
            break;
          }
          case 'EmployeeId': {
            this.employeeContractForm.EmployeeNameDari = translatedValue;
            break;
          }
          case 'Project': {
            this.employeeContractForm.ProjectNameDari = translatedValue;
            break;
          }
          case 'BudgetLine': {
            this.employeeContractForm.BudgetLineDari = translatedValue;
            break;
          }
        }

        // actionName === 'add'
        //   ? (this.addAppraisalQuestionsPopupLoading = false)
        //   : (this.editAppraisalQuestionsPopupLoading = false);
      },
      err => {}
    );

    return translatedValue;
  }
  //#endregion
}

class EmployeeContractModel {
  EmployeeContractId?: number;
  EmployeeId: any;
  FatherName: any;
  FatherNameDari: any;
  EmployeeCode: any;
  Designation: any;
  DesignationDari: any;
  ContractStartDate: any;
  ContractEndDate: any;
  DurationOfContract: any;
  Salary: any;
  Grade: any;
  GradeDari: any;
  Project: any;
  DutyStation: any;
  DutyStationDari: any;
  Country: any;
  CountryDari: any;
  Province: any;
  ProvinceDari: any;
  BudgetLine: any;
  Job: any;
  JobDari: any;
  WorkTime: any;
  WorkDayHours: any;
  OfficeId?: number;
  EmployeeNameDari: any;
  GradeName: any;
  ProjectNameDari: any;
  BudgetLineDari: any;
}

class EmployeeContractDetails {
  EmployeeName: any;
  FatherName: any;
  EmployeeCode: any;
  Designation: any;
  ContractStartDate: any;
  ContractEndDate: any;
  DurationOfContract: any;
  Salary: any;
  Grade: any;
  ProjectName: any;
  ProjectCode: any;
  DutyStation: any;
  Province: any;
  BudgetLine: any;
  Job: any;
  WorkTime: any;
  WorkDayHours: any;
  ContentEnglish: any;
  ContentDari: any;
  EmployeeImage: any;
  EmployeeNameDari: any;
  GradeName: any;
  ProjectNameDari: any;
  BudgetLineDari: any;
}
