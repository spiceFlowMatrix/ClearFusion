import { Component, OnInit, Input } from '@angular/core';
import { HrService } from '../../../hr.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../../shared/global';
import { AppSettingsService } from '../../../../../service/app-settings.service';

@Component({
  selector: 'app-professional-more-info',
  templateUrl: './professional-more-info.component.html',
  styleUrls: ['./professional-more-info.component.css']
})
export class ProfessionalMoreInfoComponent implements OnInit {
  //#region "variables"

  educationDS: EducationModel[];
  employeeOutsideOrganizationDS: EmployeeOutsideOrganizationModel[];
  educationOutsideCountryDS: EmployeeOutsideOrganizationModel[];
  informatioRegardingCloseRelativesDS: InformatioRegardingCloseRelativesModel[];
  otherThanRelativesDS: InformatioRegardingCloseRelativesModel[];
  otherSkillsDS: OtherSkillsModel[];
  salaryBudgetsDS: SalaryBudgetsModel[];
  employeeLanguagesDS: EmployeeLanguageModel[];
  languageList: any[];
  languageSpeakingFluency: any[];
  @Input() isEditingAllowed: boolean;

  currencyDataSource: any[];

  employeeId: number;

  //#endregion

  constructor(
    private hrService: HrService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {
    this.languageSpeakingFluency = [
      {
        Id: 1,
        Fluency: 'Excellent'
      },
      {
        Id: 2,
        Fluency: 'Good'
      },
      {
        Id: 3,
        Fluency: 'Fair'
      },
      {
        Id: 4,
        Fluency: 'Low'
      }
    ];
  }

  ngOnInit() {
    this.educationDS = [];
    this.employeeOutsideOrganizationDS = [];
    this.educationOutsideCountryDS = [];
    this.informatioRegardingCloseRelativesDS = [];
    this.otherThanRelativesDS = [];
    this.otherSkillsDS = [];
    this.salaryBudgetsDS = [];
    this.employeeLanguagesDS = [];

    // tslint:disable-next-line:radix
    this.employeeId = parseInt(localStorage.getItem('SelectedEmployee'));

    this.getCurrencyCodeList();

    this.getAllEmployeeEducations(this.employeeId);
    this.getAllEmployeeHistoryOutsideOrganization(this.employeeId);
    this.GetAllEmployeeHistoryOutsideCountry(this.employeeId);
    this.getAllEmployeeRelativeInformation(this.employeeId);
    this.getAllEmployeeInfoReferences(this.employeeId);
    this.getAllEmployeeOtherSkills(this.employeeId);
    this.getAllEmployeeSalaryBudgets(this.employeeId);
    this.getAllEmployeeLanguages(this.employeeId);
    this.getEmployeeLanguageList();
  }

  getEmployeeLanguageList() {
    this.hrService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_EmployeeHR_GetLanguageList
      )
      .subscribe(
        data => {
          this.languageList = [];
          if (data.StatusCode === 200 && data.data.LanguageDetail.length > 0) {
            data.data.LanguageDetail.forEach(element => {
              this.languageList.push({
                LangaugeId: element.LanguageId,
                LanguageName: element.LanguageName
              });
            });
          }
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

  //#region  "Get all Currency Details"
  getCurrencyCodeList() {
    // this.currencyCodeListLoading = true;
    this.hrService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyDataSource = [];
          if (data.StatusCode === 200 && data.data.CurrencyList.length > 0) {
            data.data.CurrencyList.forEach(element => {
              this.currencyDataSource.push({
                CurrencyId: element.CurrencyId,
                CurrencyCode: element.CurrencyCode,
                CurrencyName: element.CurrencyName,
                CurrencyRate: element.CurrencyRate
              });
            });
          }
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

  //#region "Education"
  getAllEmployeeEducations(employeeId: number) {
    this.hrService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeEducations,
        'EmployeeId',
        employeeId
      )
      .subscribe(
        data => {
          this.educationDS = [];
          if (data.StatusCode === 200) {
            if (data.data.EmployeeEducationsList != null) {
              data.data.EmployeeEducationsList.forEach(element => {
                this.educationDS.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "OutsideOrganization"
  getAllEmployeeHistoryOutsideOrganization(employeeId: number) {
    this.hrService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeHistoryOutsideOrganization,
        'EmployeeId',
        employeeId
      )
      .subscribe(
        data => {
          this.employeeOutsideOrganizationDS = [];
          if (data.StatusCode === 200) {
            if (data.data.EmployeeHistoryOutsideOrganizationList != null) {
              data.data.EmployeeHistoryOutsideOrganizationList.forEach(
                element => {
                  this.employeeOutsideOrganizationDS.push(element);
                }
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "OutsideCountry"
  GetAllEmployeeHistoryOutsideCountry(employeeId: number) {
    this.hrService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeHistoryOutsideCountry,
        'EmployeeId',
        employeeId
      )
      .subscribe(
        data => {
          this.educationOutsideCountryDS = [];
          if (data.StatusCode === 200) {
            if (data.data.EmployeeHistoryOutsideOrganizationList != null) {
              data.data.EmployeeHistoryOutsideOrganizationList.forEach(
                element => {
                  this.educationOutsideCountryDS.push(element);
                }
              );
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "CloseRelatives"
  getAllEmployeeRelativeInformation(employeeId: number) {
    this.hrService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeRelativeInformation,
        'EmployeeId',
        employeeId
      )
      .subscribe(
        data => {
          this.informatioRegardingCloseRelativesDS = [];
          if (data.StatusCode === 200) {
            if (data.data.EmployeeRelativeInfoList != null) {
              data.data.EmployeeRelativeInfoList.forEach(element => {
                this.informatioRegardingCloseRelativesDS.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "OtherThanRelatives"
  getAllEmployeeInfoReferences(employeeId: number) {
    this.hrService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeInfoReferences,
        'EmployeeId',
        employeeId
      )
      .subscribe(
        data => {
          this.otherThanRelativesDS = [];
          if (data.StatusCode === 200) {
            if (data.data.EmployeeRelativeInfoList != null) {
              data.data.EmployeeRelativeInfoList.forEach(element => {
                this.otherThanRelativesDS.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "OtherSkills"
  getAllEmployeeOtherSkills(employeeId: number) {
    this.hrService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeOtherSkills,
        'EmployeeId',
        employeeId
      )
      .subscribe(
        data => {
          this.otherSkillsDS = [];
          if (data.StatusCode === 200) {
            if (data.data.EmployeeOtherSkillsList != null) {
              data.data.EmployeeOtherSkillsList.forEach(element => {
                this.otherSkillsDS.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "SalaryBudget"
  getAllEmployeeSalaryBudgets(employeeId: number) {
    this.hrService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeSalaryBudgets,
        'EmployeeId',
        employeeId
      )
      .subscribe(
        data => {
          this.salaryBudgetsDS = [];
          if (data.StatusCode === 200) {
            if (data.data.EmployeeSalaryBudgetList != null) {
              data.data.EmployeeSalaryBudgetList.forEach(element => {
                this.salaryBudgetsDS.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "EmployeeLanguages"
  getAllEmployeeLanguages(employeeId: number) {
    this.hrService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeLanguages,
        'EmployeeId',
        employeeId
      )
      .subscribe(
        data => {
          this.employeeLanguagesDS = [];
          if (data.StatusCode === 200) {
            if (data.data.EmployeeLanguagesList != null) {
              data.data.EmployeeLanguagesList.forEach(element => {
                this.employeeLanguagesDS.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "add"
  add(data: any, api_link: any, actionName: string) {
    this.hrService.AddByModel(api_link, data).subscribe(
      // tslint:disable-next-line:no-shadowed-variable
      data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Added Successfully!');

          // let employeeId = parseInt(localStorage.getItem("SelectedEmployee"));
          if (actionName === 'Education') {
            this.getAllEmployeeEducations(this.employeeId);
          } else if (actionName === 'OutsideOrganization') {
            this.getAllEmployeeHistoryOutsideOrganization(this.employeeId);
          } else if (actionName === 'OutsideCountry') {
            this.GetAllEmployeeHistoryOutsideCountry(this.employeeId);
          } else if (actionName === 'CloseRelatives') {
            this.getAllEmployeeRelativeInformation(this.employeeId);
          } else if (actionName === 'OtherThanRelatives') {
            this.getAllEmployeeInfoReferences(this.employeeId);
          } else if (actionName === 'OtherSkills') {
            this.getAllEmployeeOtherSkills(this.employeeId);
          } else if (actionName === 'SalaryBudget') {
            this.getAllEmployeeSalaryBudgets(this.employeeId);
          } else if (actionName === 'EmployeeLanguages') {
            this.getAllEmployeeLanguages(this.employeeId);
          }
        } else {
          this.toastr.error(data.Message);
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "Edit"
  edit(data: any, api_link: any, actionName: string) {
    this.hrService.AddByModel(api_link, data).subscribe(
      // tslint:disable-next-line:no-shadowed-variable
      data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Updated Successfully!');

          // let employeeId = parseInt(localStorage.getItem("SelectedEmployee"));

          if (actionName === 'Education') {
            this.getAllEmployeeEducations(this.employeeId);
          } else if (actionName === 'OutsideOrganization') {
            this.getAllEmployeeHistoryOutsideOrganization(this.employeeId);
          } else if (actionName === 'OutsideCountry') {
            this.GetAllEmployeeHistoryOutsideCountry(this.employeeId);
          } else if (actionName === 'CloseRelatives') {
            this.getAllEmployeeRelativeInformation(this.employeeId);
          } else if (actionName === 'OtherThanRelatives') {
            this.getAllEmployeeInfoReferences(this.employeeId);
          } else if (actionName === 'OtherSkills') {
            this.getAllEmployeeOtherSkills(this.employeeId);
          } else if (actionName === 'SalaryBudget') {
            this.getAllEmployeeSalaryBudgets(this.employeeId);
          } else if (actionName === 'EmployeeLanguages') {
            this.getAllEmployeeLanguages(this.employeeId);
          }
        } else {
          this.toastr.error(data.Message);
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "delete"
  delete(data: any, api_link: any, actionName: string) {
    this.hrService.AddByModel(api_link, data).subscribe(
      // tslint:disable-next-line:no-shadowed-variable
      data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Deleted Successfully!');

          // let employeeId = parseInt(localStorage.getItem("SelectedEmployee"));

          if (actionName === 'Education') {
            this.getAllEmployeeEducations(this.employeeId);
          } else if (actionName === 'OutsideOrganization') {
            this.getAllEmployeeHistoryOutsideOrganization(this.employeeId);
          } else if (actionName === 'OutsideCountry') {
            this.GetAllEmployeeHistoryOutsideCountry(this.employeeId);
          } else if (actionName === 'CloseRelatives') {
            this.getAllEmployeeRelativeInformation(this.employeeId);
          } else if (actionName === 'OtherThanRelatives') {
            this.getAllEmployeeInfoReferences(this.employeeId);
          } else if (actionName === 'OtherSkills') {
            this.getAllEmployeeOtherSkills(this.employeeId);
          } else if (actionName === 'SalaryBudget') {
            this.getAllEmployeeSalaryBudgets(this.employeeId);
          } else if (actionName === 'EmployeeLanguages') {
            this.getAllEmployeeLanguages(this.employeeId);
          }
        } else {
          this.toastr.error(data.Message);
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "logEventEducation"
  logEventEducation(eventName: string, obj) {
    // tslint:disable-next-line:radix
    const employeeId = parseInt(localStorage.getItem('SelectedEmployee'));

    if (eventName === 'RowInserting') {
      const educationAddModel: EducationModel = {
        EmployeeEducationsId: 0,
        EducationFrom:
          obj.data.EducationFrom != null
            ? new Date(
                new Date(obj.data.EducationFrom).getFullYear(),
                new Date(obj.data.EducationFrom).getMonth(),
                new Date(obj.data.EducationFrom).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        EducationTo:
          obj.data.EducationTo != null
            ? new Date(
                new Date(obj.data.EducationTo).getFullYear(),
                new Date(obj.data.EducationTo).getMonth(),
                new Date(obj.data.EducationTo).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        Degree: obj.data.Degree,
        FieldOfStudy: obj.data.FieldOfStudy,
        Institute: obj.data.Institute,
        EmployeeID: employeeId
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_AddEmployeeEducations;
      this.add(educationAddModel, apiLink, 'Education');
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      const educationEditModel: EducationModel = {
        EmployeeEducationsId: value.EmployeeEducationsId,
        EducationFrom:
          value.EducationFrom != null
            ? new Date(
                new Date(value.EducationFrom).getFullYear(),
                new Date(value.EducationFrom).getMonth(),
                new Date(value.EducationFrom).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        EducationTo:
          value.EducationTo != null
            ? new Date(
                new Date(value.EducationTo).getFullYear(),
                new Date(value.EducationTo).getMonth(),
                new Date(value.EducationTo).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        Degree: value.Degree,
        FieldOfStudy: value.FieldOfStudy,
        Institute: value.Institute,
        EmployeeID: value.EmployeeID
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_EditEmployeeEducations;
      this.edit(educationEditModel, apiLink, 'Education');
    } else if (eventName === 'RowRemoving') {
      const educationdeleteModel: EducationModel = {
        EmployeeEducationsId: obj.data.EmployeeEducationsId,
        EducationFrom: obj.data.EducationFrom,
        EducationTo: obj.data.EducationTo,
        Degree: obj.data.Degree,
        FieldOfStudy: obj.data.FieldOfStudy,
        Institute: obj.data.Institute,
        EmployeeID: obj.data.EmployeeID
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_DeleteEmployeeEducations;
      this.delete(educationdeleteModel, apiLink, 'Education');
    }
  }
  //#endregion

  //#region "logEventOutsideOrganization"
  logEventOutsideOrganization(eventName: string, obj) {
    // tslint:disable-next-line:radix
    const employeeId = parseInt(localStorage.getItem('SelectedEmployee'));
    if (eventName === 'RowInserting') {
      const addModel: EmployeeOutsideOrganizationModel = {
        EmployeeHistoryOutsideOrganizationId: 0,
        EmployeeHistoryOutsideCountryId: 0,
        EmploymentFrom:
          obj.data.EmploymentFrom != null
            ? new Date(
                new Date(obj.data.EmploymentFrom).getFullYear(),
                new Date(obj.data.EmploymentFrom).getMonth(),
                new Date(obj.data.EmploymentFrom).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        EmploymentTo:
          obj.data.EmploymentTo != null
            ? new Date(
                new Date(obj.data.EmploymentTo).getFullYear(),
                new Date(obj.data.EmploymentTo).getMonth(),
                new Date(obj.data.EmploymentTo).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        Organization: obj.data.Organization,
        MonthlySalary: obj.data.MonthlySalary,
        ReasonForLeaving: obj.data.ReasonForLeaving,
        EmployeeID: employeeId,
        Position: obj.data.Position
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_AddEmployeeHistoryOutsideOrganization;
      this.add(addModel, apiLink, 'OutsideOrganization');
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      const editModel: EmployeeOutsideOrganizationModel = {
        EmployeeHistoryOutsideOrganizationId:
          value.EmployeeHistoryOutsideOrganizationId,
        EmployeeHistoryOutsideCountryId: 0,
        EmploymentFrom:
          value.EmploymentFrom != null
            ? new Date(
                new Date(value.EmploymentFrom).getFullYear(),
                new Date(value.EmploymentFrom).getMonth(),
                new Date(value.EmploymentFrom).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        EmploymentTo:
          value.EmploymentTo != null
            ? new Date(
                new Date(value.EmploymentTo).getFullYear(),
                new Date(value.EmploymentTo).getMonth(),
                new Date(value.EmploymentTo).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        Organization: value.Organization,
        MonthlySalary: value.MonthlySalary,
        ReasonForLeaving: value.ReasonForLeaving,
        EmployeeID: value.EmployeeID,
        Position: value.Position
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_EditEmployeeHistoryOutsideOrganization;
      this.edit(editModel, apiLink, 'OutsideOrganization');
    } else if (eventName === 'RowRemoving') {
      const deleteModel: EmployeeOutsideOrganizationModel = {
        EmployeeHistoryOutsideOrganizationId:
          obj.data.EmployeeHistoryOutsideOrganizationId,
        EmployeeHistoryOutsideCountryId: 0,
        EmploymentFrom:
          obj.data.EmploymentFrom != null
            ? new Date(
                new Date(obj.data.EmploymentFrom).getFullYear(),
                new Date(obj.data.EmploymentFrom).getMonth(),
                new Date(obj.data.EmploymentFrom).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        EmploymentTo:
          obj.data.EmploymentTo != null
            ? new Date(
                new Date(obj.data.EmploymentTo).getFullYear(),
                new Date(obj.data.EmploymentTo).getMonth(),
                new Date(obj.data.EmploymentTo).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        Organization: obj.data.Organization,
        MonthlySalary: obj.data.MonthlySalary,
        ReasonForLeaving: obj.data.ReasonForLeaving,
        EmployeeID: obj.data.EmployeeID,
        Position: obj.data.Position
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_DeleteEmployeeHistoryOutsideOrganization;
      this.delete(deleteModel, apiLink, 'OutsideOrganization');
    }
  }
  //#endregion

  //#region "logEventOutsideCountry"
  logEventOutsideCountry(eventName: string, obj) {
    // tslint:disable-next-line:radix
    const employeeId = parseInt(localStorage.getItem('SelectedEmployee'));

    if (eventName === 'RowInserting') {
      const addModel: EmployeeOutsideOrganizationModel = {
        EmployeeHistoryOutsideOrganizationId: 0,
        EmployeeHistoryOutsideCountryId: 0,
        EmploymentFrom:
          obj.data.EmploymentFrom != null
            ? new Date(
                new Date(obj.data.EmploymentFrom).getFullYear(),
                new Date(obj.data.EmploymentFrom).getMonth(),
                new Date(obj.data.EmploymentFrom).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        EmploymentTo:
          obj.data.EmploymentTo != null
            ? new Date(
                new Date(obj.data.EmploymentTo).getFullYear(),
                new Date(obj.data.EmploymentTo).getMonth(),
                new Date(obj.data.EmploymentTo).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        Organization: obj.data.Organization,
        MonthlySalary: obj.data.MonthlySalary,
        ReasonForLeaving: obj.data.ReasonForLeaving,
        EmployeeID: employeeId,
        Position: obj.data.Position
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_AddEmployeeHistoryOutsideCountry;
      this.add(addModel, apiLink, 'OutsideCountry');
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      const editModel: EmployeeOutsideOrganizationModel = {
        EmployeeHistoryOutsideOrganizationId: 0,
        EmployeeHistoryOutsideCountryId: value.EmployeeHistoryOutsideCountryId,
        EmploymentFrom:
          value.EmploymentFrom != null
            ? new Date(
                new Date(value.EmploymentFrom).getFullYear(),
                new Date(value.EmploymentFrom).getMonth(),
                new Date(value.EmploymentFrom).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        EmploymentTo:
          value.EmploymentTo != null
            ? new Date(
                new Date(value.EmploymentTo).getFullYear(),
                new Date(value.EmploymentTo).getMonth(),
                new Date(value.EmploymentTo).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        Organization: value.Organization,
        MonthlySalary: value.MonthlySalary,
        ReasonForLeaving: value.ReasonForLeaving,
        EmployeeID: value.EmployeeID,
        Position: value.Position
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_EditEmployeeHistoryOutsideCountry;
      this.edit(editModel, apiLink, 'OutsideCountry');
    } else if (eventName === 'RowRemoving') {
      const deleteModel: EmployeeOutsideOrganizationModel = {
        EmployeeHistoryOutsideOrganizationId: 0,
        EmployeeHistoryOutsideCountryId:
          obj.data.EmployeeHistoryOutsideCountryId,
        EmploymentFrom:
          obj.data.EmploymentFrom != null
            ? new Date(
                new Date(obj.data.EmploymentFrom).getFullYear(),
                new Date(obj.data.EmploymentFrom).getMonth(),
                new Date(obj.data.EmploymentFrom).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        EmploymentTo:
          obj.data.EmploymentTo != null
            ? new Date(
                new Date(obj.data.EmploymentTo).getFullYear(),
                new Date(obj.data.EmploymentTo).getMonth(),
                new Date(obj.data.EmploymentTo).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        Organization: obj.data.Organization,
        MonthlySalary: obj.data.MonthlySalary,
        ReasonForLeaving: obj.data.ReasonForLeaving,
        EmployeeID: obj.data.EmployeeID,
        Position: obj.data.Position
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_DeleteEmployeeHistoryOutsideCountry;
      this.delete(deleteModel, apiLink, 'OutsideCountry');
    }
  }
  //#endregion

  //#region "logEventCloseRelatives"
    logEventCloseRelatives(eventName: string, obj) {
    // tslint:disable-next-line:radix
    const employeeId = parseInt(localStorage.getItem('SelectedEmployee'));

        if (obj.column != undefined && obj.column.dataField === 'PhoneNo') {
      let phone= obj.data.PhoneNo.toString();
      if(phone.length>14 || phone.length<10){
        this.toastr.warning('Phone Number should be between 10-14 digits!!!');
      }
    }

      if (eventName === 'RowInserting') {
      const addModel: InformatioRegardingCloseRelativesModel = {
        EmployeeRelativeInfoId: 0,
        EmployeeInfoReferencesId: 0,
        Name: obj.data.Name,
        Relationship: obj.data.Relationship,
        Position: obj.data.Position,
        Organization: obj.data.Organization,
        EmployeeID: employeeId,
        Email: obj.data.Email,
        PhoneNo: obj.data.PhoneNo
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_AddEmployeeRelativeInformation;
      this.add(addModel, apiLink, 'CloseRelatives');
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      const editModel: InformatioRegardingCloseRelativesModel = {
        EmployeeRelativeInfoId: value.EmployeeRelativeInfoId,
        EmployeeInfoReferencesId: 0,
        Name: value.Name,
        Relationship: value.Relationship,
        Position: value.Position,
        Organization: value.Organization,
        EmployeeID: value.EmployeeID,
        Email: value.Email,
        PhoneNo: value.PhoneNo
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_EditEmployeeRelativeInformation;
      this.edit(editModel, apiLink, 'CloseRelatives');
    } else if (eventName === 'RowRemoving') {
      const deleteModel: InformatioRegardingCloseRelativesModel = {
        EmployeeRelativeInfoId: obj.data.EmployeeRelativeInfoId,
        EmployeeInfoReferencesId: 0,
        Name: obj.data.Name,
        Relationship: obj.data.Relationship,
        Position: obj.data.Position,
        Organization: obj.data.Organization,
        EmployeeID: obj.data.EmployeeID,
        Email: obj.data.Email,
        PhoneNo: obj.data.PhoneNo
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_DeleteEmployeeRelativeInformation;
      this.delete(deleteModel, apiLink, 'CloseRelatives');
    }
  }
  //#endregion

  //#region "logEventotherThanRelatives"
  logEventotherThanRelatives(eventName: string, obj) {
    // tslint:disable-next-line:radix
    const employeeId = parseInt(localStorage.getItem('SelectedEmployee'));

      if (obj.column != undefined && obj.column.dataField === 'PhoneNo') {
      let phone= obj.data.PhoneNo.toString();
      if(phone.length>14 || phone.length<10){
        this.toastr.warning('Phone Number should be between 10-14 digits!!!');
      }
    }

    if (eventName === 'RowInserting') {
      const addModel: InformatioRegardingCloseRelativesModel = {
        EmployeeRelativeInfoId: 0,
        EmployeeInfoReferencesId: 0,
        Name: obj.data.Name,
        Relationship: obj.data.Relationship,
        Position: obj.data.Position,
        Organization: obj.data.Organization,
        EmployeeID: employeeId,
        Email: obj.data.Email,
        PhoneNo: obj.data.PhoneNo
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_AddEmployeeInfoReferences;
      this.add(addModel, apiLink, 'OtherThanRelatives');
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      const editModel: InformatioRegardingCloseRelativesModel = {
        EmployeeRelativeInfoId: 0,
        EmployeeInfoReferencesId: value.EmployeeInfoReferencesId,
        Name: value.Name,
        Relationship: value.Relationship,
        Position: value.Position,
        Organization: value.Organization,
        EmployeeID: value.EmployeeID,
        Email: value.Email,
        PhoneNo: value.PhoneNo
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_EditEmployeeInfoReferences;
      this.edit(editModel, apiLink, 'OtherThanRelatives');
    } else if (eventName === 'RowRemoving') {
      const deleteModel: InformatioRegardingCloseRelativesModel = {
        EmployeeRelativeInfoId: 0,
        EmployeeInfoReferencesId: obj.data.EmployeeInfoReferencesId,
        Name: obj.data.Name,
        Relationship: obj.data.Relationship,
        Position: obj.data.Position,
        Organization: obj.data.Organization,
        EmployeeID: obj.data.EmployeeID,
        Email: obj.data.Email,
        PhoneNo: obj.data.PhoneNo
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_DeleteEmployeeInfoReferences;
      this.delete(deleteModel, apiLink, 'OtherThanRelatives');
    }
  }
  //#endregion

  //#region "logEventOtherSkills"
  logEventOtherSkills(eventName: string, obj) {
    // tslint:disable-next-line:radix
    const employeeId = parseInt(localStorage.getItem('SelectedEmployee'));

    if (eventName === 'RowInserting') {
      const addModel: OtherSkillsModel = {
        EmployeeOtherSkillsId: 0,
        TypeOfSkill: obj.data.TypeOfSkill,
        AbilityLevel: obj.data.AbilityLevel,
        Experience: obj.data.Experience,
        Remarks: obj.data.Remarks,
        EmployeeID: employeeId
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_AddEmployeeOtherSkills;
      this.add(addModel, apiLink, 'OtherSkills');
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      const editModel: OtherSkillsModel = {
        EmployeeOtherSkillsId: value.EmployeeOtherSkillsId,
        TypeOfSkill: value.TypeOfSkill,
        AbilityLevel: value.AbilityLevel,
        Experience: value.Experience,
        Remarks: value.Remarks,
        EmployeeID: value.EmployeeID
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_EditEmployeeOtherSkills;
      this.edit(editModel, apiLink, 'OtherSkills');
    } else if (eventName === 'RowRemoving') {
      const deleteModel: OtherSkillsModel = {
        EmployeeOtherSkillsId: obj.data.EmployeeOtherSkillsId,
        TypeOfSkill: obj.data.TypeOfSkill,
        AbilityLevel: obj.data.AbilityLevel,
        Experience: obj.data.Experience,
        Remarks: obj.data.Remarks,
        EmployeeID: obj.data.EmployeeID
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_DeleteEmployeeOtherSkills;
      this.delete(deleteModel, apiLink, 'OtherSkills');
    }
  }
  //#endregion

  //#region "logEventSalaryBudgets"
  logEventSalaryBudgets(eventName: string, obj) {
    // tslint:disable-next-line:radix
    const employeeId = parseInt(localStorage.getItem('SelectedEmployee'));

    if (eventName === 'RowInserting') {
      const addModel: SalaryBudgetsModel = {
        EmployeeSalaryBudgetId: 0,
        Year: obj.data.Year,
        CurrencyId: obj.data.CurrencyId,
        SalaryBudget: obj.data.SalaryBudget,
        BudgetDisbursed: obj.data.BudgetDisbursed,
        EmployeeID: employeeId
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_AddEmployeeSalaryBudgets;
      this.add(addModel, apiLink, 'SalaryBudget');
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      const editModel: SalaryBudgetsModel = {
        EmployeeSalaryBudgetId: value.EmployeeSalaryBudgetId,
        Year: value.Year,
        CurrencyId: value.CurrencyId,
        SalaryBudget: value.SalaryBudget,
        BudgetDisbursed: value.BudgetDisbursed,
        EmployeeID: value.EmployeeID
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_EditEmployeeSalaryBudgets;
      this.edit(editModel, apiLink, 'SalaryBudget');
    } else if (eventName === 'RowRemoving') {
      const deleteModel: SalaryBudgetsModel = {
        EmployeeSalaryBudgetId: obj.data.EmployeeSalaryBudgetId,
        Year: obj.data.Year,
        CurrencyId: obj.data.CurrencyId,
        SalaryBudget: obj.data.SalaryBudget,
        BudgetDisbursed: obj.data.BudgetDisbursed,
        EmployeeID: obj.data.EmployeeID
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_DeleteEmployeeSalaryBudgets;
      this.delete(deleteModel, apiLink, 'SalaryBudget');
    }
  }
  //#endregion

  //#region "Languages Employee Can Speak"
  logEventEmployeeLanguages(eventName: string, obj) {
    // tslint:disable-next-line:radix
    const employeeId = parseInt(localStorage.getItem('SelectedEmployee'));

    if (eventName === 'RowInserting') {
      const addModel: EmployeeLanguageModel = {
        SpeakLanguageId: 0,
        Reading: obj.data.Reading,
        Writing: obj.data.Writing,
        Speaking: obj.data.Speaking,
        Listening: obj.data.Listening,
        LanguageId: obj.data.LanguageId,
        EmployeeId: employeeId
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_AddEmployeeLanguages;
      this.add(addModel, apiLink, 'EmployeeLanguages');
    } else if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data

      const editModel: EmployeeLanguageModel = {
        SpeakLanguageId: value.SpeakLanguageId,
        Reading: value.Reading,
        Writing: value.Writing,
        Speaking: value.Speaking,
        Listening: value.Listening,
        LanguageId: value.LanguageId,
        EmployeeId: value.EmployeeID
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_EditEmployeeLanguages;
      this.edit(editModel, apiLink, 'EmployeeLanguages');
    } else if (eventName === 'RowRemoving') {
      const deleteModel: EmployeeLanguageModel = {
        SpeakLanguageId: obj.data.SpeakLanguageId,
        Reading: obj.data.Reading,
        Writing: obj.data.Writing,
        Speaking: obj.data.Speaking,
        Listening: obj.data.Listening,
        LanguageId: obj.data.LanguageId,
        EmployeeId: obj.data.employeeId
      };

      const apiLink =
        this.setting.getBaseUrl() +
        GLOBAL.API_EmployeeDetail_RemoveEmployeeLanguages;
      this.delete(deleteModel, apiLink, 'EmployeeLanguages');
    }
  }
  //#endregion

  functionCache = {};
  validateRange(min, max) {
    if (!this.functionCache[`min${min}max${max}`])
      this.functionCache[`min${min}max${max}`] = (options: any) => {
        return options.value >= min && options.value <= max;
      }
    return this.functionCache[`min${min}max${max}`]
  }
}

//#region "Classes"

class EducationModel {
  EmployeeEducationsId: number;
  EducationFrom: any;
  EducationTo: any;
  FieldOfStudy: any;
  Institute: any;
  Degree: any;
  EmployeeID: number;
}

class EmployeeOutsideOrganizationModel {
  EmployeeHistoryOutsideOrganizationId?: any;
  EmployeeHistoryOutsideCountryId?: any;
  EmploymentFrom: any;
  EmploymentTo: any;
  Organization: any;
  MonthlySalary: number;
  ReasonForLeaving: any;
  EmployeeID: number;
  Position: string;
}

class InformatioRegardingCloseRelativesModel {
  EmployeeRelativeInfoId?: any;
  EmployeeInfoReferencesId?: any;
  Name: any;
  Relationship: any;
  Position: any;
  Organization: any;
  EmployeeID: number;
  PhoneNo: number;
  Email: string;
}

class OtherSkillsModel {
  EmployeeOtherSkillsId: Number;
  TypeOfSkill: any;
  AbilityLevel: any;
  Experience: any;
  Remarks: any;
  EmployeeID: number;
}

class SalaryBudgetsModel {
  EmployeeSalaryBudgetId: any;
  Year: any;
  CurrencyId: number;
  SalaryBudget: number;
  BudgetDisbursed: number;
  EmployeeID: number;
}

class EmployeeLanguageModel {
  SpeakLanguageId?: Number;
  Reading: any;
  Writing: any;
  Speaking: any;
  Listening: any;
  LanguageId: number;
  EmployeeId: number;
}

//#endregion
