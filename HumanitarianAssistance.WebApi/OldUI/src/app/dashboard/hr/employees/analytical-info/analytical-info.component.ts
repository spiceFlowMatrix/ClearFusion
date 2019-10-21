import { Component, OnInit, Input } from "@angular/core";
import { HrService } from "../../hr.service";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { GLOBAL } from "../../../../shared/global";
import {
  applicationPages,
  applicationModule
} from "../../../../shared/application-pages-enum";
import { CommonService } from "../../../../service/common.service";
import { AppSettingsService } from "../../../../service/app-settings.service";

@Component({
  selector: "app-analytical-info",
  templateUrl: "./analytical-info.component.html",
  styleUrls: ["./analytical-info.component.css"]
})
export class AnalyticalInfoComponent implements OnInit {
  @Input() employeeId: number;
  // @Input() tabEventValue: number;

  salaryAnalyticalInfoDS: SalaryAnalyticalInfoModel[];
  analyticalForm: SalaryAnalyticalInfoModel;
  analyticalDeleteForm: SalaryAnalyticalInfoModel;

  projectList: any[];
  budgetLineList: any[];
  accountsList: any[];
  isEditingAllowed = false;

  addEditAnalyticalInfoPopupVisible = false;
  deleteConfirmationPopup = false;

  // loader
  deleteAnalyticalFormPopupLoading = false;
  analyticalPopupLoading = false;

  constructor(
    private hrService: HrService,
    private router: Router,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private commonservice: CommonService
  ) {}

  ngOnInit() {
    this.salaryAnalyticalInfoDS = [];
    this.getAllProjectDetails();
    this.getAccountDetail();
    this.getAllEmployeeSalaryAnalyticalInfo();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.JournalCodes
    );
  }

  //#region "getAllEmployeeSalaryAnalyticalInfo"
  getAllEmployeeSalaryAnalyticalInfo() {
    this.hrService
      .GetAllDetailsById(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_GetAllEmployeeSalaryAnalyticalInfo,
        "EmployeeId",
        this.employeeId
      )
      .subscribe(
        data => {
          this.salaryAnalyticalInfoDS = [];
          if (
            data.data.EmployeeSalaryAnalyticalInfoList != null &&
            data.StatusCode === 200
          ) {
            if (data.data.EmployeeSalaryAnalyticalInfoList.length > 0) {
              data.data.EmployeeSalaryAnalyticalInfoList.forEach(element => {
                this.salaryAnalyticalInfoDS.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "getAllProjectDetails"
  getAllProjectDetails() {
    this.hrService
      .GetAllDetails(
        this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_GetAllProjectDetail
      )
      .subscribe(
        data => {
          this.projectList = [];
          if (data != null) {
            if (
              data.data.ProjectDetailList != null &&
              data.StatusCode === 200
            ) {
              if (data.data.ProjectDetailList.length > 0) {
                data.data.ProjectDetailList.forEach(element => {
                  this.projectList.push({
                    ProjectId: element.ProjectId,
                    ProjectName: element.ProjectName,
                    ProjectCode: element.ProjectCode,
                    ProjectCodeName:
                      element.ProjectCode + "-" + element.ProjectName
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

  //#region  "getInputLevel account detail"
  getAccountDetail() {
    this.accountsList = [];
    this.commonservice.GetInputLevelAccountDetails().subscribe(response => {
      if (response != null) {
        response.forEach(element => {
          this.accountsList.push({
            AccountCode: element.AccountCode,
            AccountName: element.AccountName,
            AccountLevelId: element.AccountLevelId
          });
        });
      }
    });
  }
  //#endregion

  //#region "getBudgetLineDetails"
  getBudgetLineDetails(ProjectId) {
    this.showAnalyticalPopupLoading();
    this.hrService
      .GetBudgetLineByProjectId(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_GetAllBudgetLineDetails,
        ProjectId
      )
      .subscribe(
        data => {
          this.budgetLineList = [];
          if (
            data.data.ProjectBudgetLineDetailList != null &&
            data.StatusCode === 200 &&
            data.data.ProjectBudgetLineDetailList.length > 0
          ) {
            data.data.ProjectBudgetLineDetailList.forEach(element => {
              this.budgetLineList.push(element);
            });
          }
          this.hideAnalyticalPopupLoading();
        },
        error => {
          this.hideAnalyticalPopupLoading();
        }
      );
  }
  //#endregion

  //#region "addEmployeeSalaryAnalyticalInfo"
  addEmployeeSalaryAnalyticalInfo(data: any) {
    this.showAnalyticalPopupLoading();
    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_AddEmployeeSalaryAnalyticalInfo,
        data
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success("Added Successfully!");
          } else {
            this.toastr.error(data.Message);
          }
          this.hideAnalyticalPopupLoading();
          this.hideAddEditAnalyticalInfoPopupVisible();
          this.getAllEmployeeSalaryAnalyticalInfo();
        },
        error => {
          this.hideAnalyticalPopupLoading();
          this.hideAddEditAnalyticalInfoPopupVisible();
        }
      );
  }
  //#endregion

  //#region "editEmployeeSalaryAnalyticalInfo"
  editEmployeeSalaryAnalyticalInfo(data: any) {
    this.showAnalyticalPopupLoading();
    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_EditEmployeeSalaryAnalyticalInfo,
        data
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success("Added Successfully!");
          } else {
            this.toastr.error(data.Message);
          }
          this.hideAnalyticalPopupLoading();
          this.hideAddEditAnalyticalInfoPopupVisible();
          this.getAllEmployeeSalaryAnalyticalInfo();
        },
        error => {
          this.hideAnalyticalPopupLoading();
          this.hideAddEditAnalyticalInfoPopupVisible();
        }
      );
  }
  //#endregion

  //#region "deleteEmployeeSalaryAnalyticalInfo"
  deleteEmployeeSalaryAnalyticalInfo(data: any) {
    this.showDeleteAnalyticalFormPopupLoading();
    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeDetail_DeleteEmployeeSalaryAnalyticalInfo,
        data
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success("Deleted Successfully!");
          } else {
            this.toastr.error(data.Message);
          }
          this.hideDeleteAnalyticalFormPopupLoading();
          this.hideDeleteConfirmationPopup();
          this.getAllEmployeeSalaryAnalyticalInfo();
        },
        error => {
          this.hideDeleteAnalyticalFormPopupLoading();
          this.hideDeleteConfirmationPopup();
        }
      );
  }
  //#endregion

  //#region "AddEditAnalyticalInfo"
  AddEditAnalyticalInfo(data: any) {
    // tslint:disable-next-line:radix
    const employeeId = parseInt(localStorage.getItem("SelectedEmployee"));

    if (
      data.EmployeeSalaryAnalyticalInfoId === 0 ||
      data.EmployeeSalaryAnalyticalInfoId == null
    ) {
      const addModel: SalaryAnalyticalInfoModel = {
        EmployeeSalaryAnalyticalInfoId: 0,
        AccountCode: data.AccountCode,
        ProjectId: data.ProjectId,
        BudgetLineId: data.BudgetLineId,
        SalaryPercentage: data.SalaryPercentage,
        EmployeeID: employeeId,
      };

      this.addEmployeeSalaryAnalyticalInfo(addModel);
    } else {
      const editModel: SalaryAnalyticalInfoModel = {
        EmployeeSalaryAnalyticalInfoId: data.EmployeeSalaryAnalyticalInfoId,
        AccountCode: data.AccountCode,
        ProjectId: data.ProjectId,
        BudgetLineId: data.BudgetLineId,
        SalaryPercentage: data.SalaryPercentage,
        EmployeeID: data.EmployeeID
      };

      this.editEmployeeSalaryAnalyticalInfo(editModel);
    }
  }
  //#endregion

  //#region "deleteAnalyticalInfo"
  deleteAnalyticalInfoConfirm() {
    if (this.analyticalDeleteForm != null) {
      this.deleteEmployeeSalaryAnalyticalInfo(this.analyticalDeleteForm);
    }
  }
  //#endregion

  //#region "onShowAddAnalyticalForm"
  onShowAddAnalyticalForm() {
    this.analyticalForm = {
      AccountCode: null,
      BudgetLineId: null,
      EmployeeID: null,
      EmployeeSalaryAnalyticalInfoId: 0,
      ProjectId: null,
      SalaryPercentage: null
    };
    this.showAddEditAnalyticalInfoPopupVisible();
  }
  //#endregion

  //#region "onShowAddAnalyticalForm"
  onShowEditAnalyticalForm(data: any) {
    this.analyticalForm = {
      AccountCode: data.AccountCode,
      BudgetLineId: data.BudgetLineId,
      EmployeeID: data.EmployeeID,
      EmployeeSalaryAnalyticalInfoId: data.EmployeeSalaryAnalyticalInfoId,
      ProjectId: data.ProjectId,
      SalaryPercentage: data.SalaryPercentage
    };

    this.getBudgetLineDetails(data.ProjectId);

    this.showAddEditAnalyticalInfoPopupVisible();
  }
  //#endregion

  //#region "onShowAddAnalyticalForm"
  onShowDeleteAnalyticalForm(data: any) {
    this.analyticalDeleteForm = {
      AccountCode: null,
      BudgetLineId: null,
      EmployeeID: null,
      EmployeeSalaryAnalyticalInfoId: 0,
      ProjectId: null,
      SalaryPercentage: null
    };

    this.analyticalDeleteForm = {
      AccountCode: data.AccountCode,
      BudgetLineId: data.BudgetLineId,
      EmployeeID: data.EmployeeID,
      EmployeeSalaryAnalyticalInfoId: data.EmployeeSalaryAnalyticalInfoId,
      ProjectId: data.ProjectId,
      SalaryPercentage: data.SalaryPercentage
    };

    this.showDeleteConfirmationPopup();
  }
  //#endregion

  //#region "onFieldValueChanged"
  onFieldDataChanged(e: any) {
    if (e != null) {
      if (e.dataField === "ProjectId" && e.value != null) {
        this.getBudgetLineDetails(e.value);
      }
    }
  }
  //#endregion

  projectSelectionChanged(item: any) {}

  //#region "show / hide"
  showAddEditAnalyticalInfoPopupVisible() {
    this.addEditAnalyticalInfoPopupVisible = true;
  }
  hideAddEditAnalyticalInfoPopupVisible() {
    this.addEditAnalyticalInfoPopupVisible = false;
  }

  showDeleteConfirmationPopup() {
    this.deleteConfirmationPopup = true;
  }
  hideDeleteConfirmationPopup() {
    this.deleteConfirmationPopup = false;
  }
  //#endregion

  //#region "Loader"
  showAnalyticalPopupLoading() {
    this.analyticalPopupLoading = true;
  }

  hideAnalyticalPopupLoading() {
    this.analyticalPopupLoading = false;
  }

  showDeleteAnalyticalFormPopupLoading() {
    this.deleteAnalyticalFormPopupLoading = true;
  }
  hideDeleteAnalyticalFormPopupLoading() {
    this.deleteAnalyticalFormPopupLoading = false;
  }
  //#endregion
}

class SalaryAnalyticalInfoModel {
  EmployeeSalaryAnalyticalInfoId: number;
  AccountCode: number;
  ProjectId: number;
  BudgetLineId: number;
  SalaryPercentage: number;
  EmployeeID: number;

  BudgetLineName?: string;
}
