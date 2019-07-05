import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  SimpleChanges
} from '@angular/core';
import { HrService } from '../../hr.service';
import { AccountsService } from '../../../accounts/accounts.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer } from '@angular/platform-browser';
import { GLOBAL } from '../../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../../shared/application-pages-enum';
import { CommonService } from '../../../../service/common.service';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { EmployeeType } from '../../../../shared/enums';
import { IAttendanceGroup } from '../../../code/attendance-group-master/attendance-group-master.component';
import { CodeService } from '../../../code/code.service';

@Component({
  selector: 'app-professional-info',
  templateUrl: './professional-info.component.html',
  styleUrls: ['./professional-info.component.css']
})
export class ProfessionalInfoComponent implements OnInit {
  @Input() employeeId: number;
  @Input() tabEventValue: number;

  @Output() employeeListRefresh = new EventEmitter<any>();
  @Output() triggerEmployeeLeavePopUp = new EventEmitter<any>();

  professionalDetails: ProfessionalDetails;
  editpProfessionalDetails: ProfessionalDetails;
  popupProfesionalinfoVisible = false;
  popupAssignLeaveVisible = false;
  isEditingAllowed = false;
  // leaveInfoDataSource: LeaveInfoModel[];
  disabledDates: any[];
  financialYearDropdown: any;
  leaveReasonId: number;
  assignUnitIsValid = true;
  assignLeaveToEmployee: number;

  currentDate = new Date();

  officeId: number;
  OfficeId: number;
  employeeTypeIdCheck: number;
  LeaveReasonIdBinding: number;

  DepartmentId: number;
  // leaveInfoData: LeaveInfoData;
  leaveReasonTypeDropdown: any;

  employeeTypeDropdown: any[];
  departmentTypeDropdown: any[];
  designationDropdown: any[];
  officeTypeDropdown: any[];
  attendanceGroupList: IAttendanceGroup[];

  hiredOnFlag = true; // disabled
  firedOnFlag = true; // disabled
  resignationOnFlag = true; // disabled

  // loader
  professionalInfoLoading = false;
  leaveInfoLoading = false;
  editProfessionalInfoPopupLoading: boolean;
  employeeContractType: any[];

  constructor(
    private hrService: HrService,
    private common: CommonService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private codeService: CodeService
  ) {}

  ngOnInit() {
    this.initializeForm();

    // Employee Type
    this.getEmployeeTypeDropdown();
    this.getOfficeTypeDropdown();
    this.GetAllDesignationDropdown();
    this.getEmployeeContractType();
    this.GetLeaveReasonTypeDropdown();
    this.getAllDisableCalendarDate(this.employeeId);
    this.GetFinancialYearDropdown();
    this.getAttendanceGroupList();
    this.isEditingAllowed = this.common.IsEditingAllowed(
      applicationPages.Employees
    );
  }

  // TODO: For Input Binding if EmployeeID value changed
  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(changes: SimpleChanges) {
    // tslint:disable-next-line:forin
    for (const propName in changes) {
      const change = changes[propName];

      const curVal = JSON.stringify(change.currentValue);
      const prevVal = JSON.stringify(change.previousValue);

      // TODO: On Employee Type Changed value must be updatedfor current employee
      if (propName === 'employeeId') {
        // tslint:disable-next-line:curly
        if (curVal !== prevVal)
          this.GetEmployeeProfessionalDetailById(this.employeeId);
      }
    }
  }

  initializeForm() {
    this.professionalDetails = {
      EmployeeProfessionalId: 0,
      EmployeeTypeId: 0,
      Status: null,
      OfficeId: null,
      DepartmentId: null,
      DesignationId: null,
      EmployeeContractTypeId: null,
      HiredOn: null,
      FiredOn: null,
      FiredReason: null,
      ResignationOn: null,
      ResignationReason: null,
      JobDescription: null,
      TrainingBenefits: null,
      EmployeeId: 0,
      OfficeName: null,
      DesignationName: null,
      DepartmentName: null,
      EmployeeTypeName: null,
      AttendanceGroupId: null
    };
  }

  //#region "Get Employee Contract Type"
  getEmployeeContractType() {
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_GetEmployeeContractType
      )
      .subscribe(
        data => {
          this.employeeContractType = [];
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeContractTypeList.length > 0
          ) {
            this.employeeContractType = data.data.EmployeeContractTypeList;
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

  //#region "Get Employee Type"
  getEmployeeTypeDropdown() {
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllEmployeeType
      )
      .subscribe(
        data => {
          this.employeeTypeDropdown = [];
          if (
            data.data.EmployeeTypeList != null &&
            data.data.EmployeeTypeList.length > 0
          ) {
            data.data.EmployeeTypeList.forEach(element => {
              // TODO: For Perspective Employee
              // tslint:disable-next-line:curly
              if (this.tabEventValue === 1)
                this.employeeTypeDropdown.push(element);

              // TODO: For Active And Terminated
              if (this.tabEventValue === 2 || this.tabEventValue === 3) {
                // tslint:disable-next-line:curly
                if (element.EmployeeTypeId !== 1)
                  this.employeeTypeDropdown.push(element);
              }
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

  //#region "Get Office Type"
  getOfficeTypeDropdown() {
    this.hrService
      .GetAllDropdown(this.setting.getBaseUrl() + GLOBAL.API_AllOffice_URL)
      .subscribe(
        data => {
          this.officeTypeDropdown = [];
          if (
            data.data.OfficeDetailsList != null &&
            data.data.OfficeDetailsList.length > 0
          ) {
            data.data.OfficeDetailsList.forEach(element => {
              this.officeTypeDropdown.push(element);
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

  //#region "Get Department Type"
  getDepartmentType(eventId: any) {
    this.officeId = eventId;
    this.hrService
      .GetDepartmentDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetDepartmentsByOfficeId,
        eventId
      )
      .subscribe(
        data => {
          this.departmentTypeDropdown = [];
          if (
            data.data.Departments != null &&
            data.data.Departments.length > 0
          ) {
            data.data.Departments.forEach(element => {
              this.departmentTypeDropdown.push(element);
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

  //#region "Get Designation Type"
  GetAllDesignationDropdown() {
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllDesignation
      )
      .subscribe(
        data => {
          this.designationDropdown = [];
          if (
            data.data.DesignationList != null &&
            data.data.DesignationList.length > 0
          ) {
            data.data.DesignationList.forEach(element => {
              this.designationDropdown.push(element);
            });

            this.common.sortDropdown(this.designationDropdown, 'Designation');
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

  //#region "Get Professional Details By Id"
  GetEmployeeProfessionalDetailById(empId: number) {
    this.professionalInfoLoading = true;
    this.hrService
      .GetEmployeesDetailsByEmployeeId(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetEmployeeProfessionalDetail,
        empId
      )
      .subscribe(
        data => {
          if (
            data.StatusCode === 200 &&
            data.data.EmployeeProfessionalList != null &&
            data.data.EmployeeProfessionalList.length > 0
          ) {
            this.professionalDetails = data.data.EmployeeProfessionalList[0];
            this.professionalDetails.HiredOn != null
              ? localStorage.setItem(
                  'HIREDON',
                  this.professionalDetails.HiredOn.toString()
                )
              // tslint:disable-next-line:no-unused-expression
              : null;
            this.employeeTypeIdCheck = this.professionalDetails.EmployeeTypeId;
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.professionalInfoLoading = false;
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

  //#region "Edit Professional Info"
  EditProfessionalDetails(model: any) {
    this.editProfessionalInfoPopupLoading = true;
    const profDetails: ProfessionalDetails = {
      EmployeeProfessionalId: model.EmployeeProfessionalId,
      OfficeId: this.officeId,
      EmployeeId: this.employeeId,
      DepartmentId: model.DepartmentId,
      DesignationId: model.DesignationId,
      EmployeeTypeId: model.EmployeeTypeId,
      EmployeeContractTypeId: model.EmployeeContractTypeId,
      AttendanceGroupId: model.AttendanceGroupId,
      FiredOn:
        model.FiredOn == null || ''
          ? null
          : new Date(
              new Date(model.FiredOn).getFullYear(),
              new Date(model.FiredOn).getMonth(),
              new Date(model.FiredOn).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
      FiredReason: model.FiredReason,
      HiredOn:
        model.HiredOn == null || ''
          ? null
          : new Date(
              new Date(model.HiredOn).getFullYear(),
              new Date(model.HiredOn).getMonth(),
              new Date(model.HiredOn).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
      JobDescription: model.JobDescription,
      ResignationOn:
        model.ResignationOn == null || ''
          ? null
          : new Date(
              new Date(model.ResignationOn).getFullYear(),
              new Date(model.ResignationOn).getMonth(),
              new Date(model.ResignationOn).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
      ResignationReason: model.ResignationReason,
      Status: model.Status,
      TrainingBenefits: model.TrainingBenefits
    };
    this.assignLeaveToEmployee = this.employeeId;
    this.hrService
      .addProfessionalInfo(
        this.setting.getBaseUrl() +
          GLOBAL.API_HR_EditEmployeeProfessionalDetail,
        profDetails
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Employee Updated Successfully!!!');
            if (this.employeeTypeIdCheck !== model.EmployeeTypeId) {
              // TODO: Output Binding
              this.initializeForm(); // Init professional field
              this.employeeListRefresh.emit();

              let showLeavePopUp = false;

              if (model.EmployeeTypeId !== EmployeeType.Terminated) {
                showLeavePopUp = true;
              }
              this.triggerEmployeeLeavePopUp.emit({
                employeeid: this.assignLeaveToEmployee,
                displayLeavePopUp: showLeavePopUp
              });
            } else {
              this.GetEmployeeProfessionalDetailById(this.employeeId);
            }
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.popupProfesionalinfoVisible = false;
          this.editProfessionalInfoPopupLoading = false;
          this.popupAssignLeaveVisible = true;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.editProfessionalInfoPopupLoading = false;
        }
      );
  }
  //#endregion

  //#region "Add Professional Info"
  AddProfessionalDetails(model: any) {
    this.editProfessionalInfoPopupLoading = true;

    const professionalDetails: ProfessionalDetails = {
      EmployeeProfessionalId: 0,
      OfficeId: this.officeId,
      EmployeeId: this.employeeId,
      DepartmentId: model.DepartmentId,
      DesignationId: model.DesignationId,
      EmployeeTypeId: model.EmployeeTypeId,
      EmployeeContractTypeId: model.EmployeeContractTypeId,
      AttendanceGroupId: model.AttendanceGroupId,
      FiredOn:
        model.FiredOn == null || ''
          ? null
          : new Date(
              new Date(model.FiredOn).getFullYear(),
              new Date(model.FiredOn).getMonth(),
              new Date(model.FiredOn).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
      HiredOn:
        model.HiredOn == null || ''
          ? null
          : new Date(
              new Date(model.HiredOn).getFullYear(),
              new Date(model.HiredOn).getMonth(),
              new Date(model.HiredOn).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
      ResignationOn:
        model.ResignationOn == null || ''
          ? null
          : new Date(
              new Date(model.ResignationOn).getFullYear(),
              new Date(model.ResignationOn).getMonth(),
              new Date(model.ResignationOn).getDate(),
              new Date().getHours(),
              new Date().getMinutes(),
              new Date().getSeconds()
            ),
      FiredReason: model.FiredReason,
      JobDescription: model.JobDescription,
      ResignationReason: model.ResignationReason,
      Status: model.Status,
      TrainingBenefits: model.TrainingBenefits
    };
    this.assignLeaveToEmployee = this.employeeId;
    this.hrService
      .addProfessionalInfo(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_AddEmployeeProfessionalDetail,
        professionalDetails
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Employee Updated Successfully!!!');
            this.GetEmployeeProfessionalDetailById(this.employeeId);
            this.popupAssignLeaveVisible = true; // Assignleave to employee
            this.popupProfesionalinfoVisible = false;
            this.editProfessionalInfoPopupLoading = false;
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

          this.editProfessionalInfoPopupLoading = false;
        }
      );
  }
  //#endregion

  //#region "On Form Submit"
  OnFormSubmit(model) {
    // tslint:disable-next-line:curly
    //if (model.EmployeeProfessionalId === 0)
    //this.AddProfessionalDetails(model);
    //// tslint:disable-next-line:curly
    //else

        this.EditProfessionalDetails(model);
  }
  //#endregion

  //#region "show / Hide Edit Model"
  showProfEditForm(value) {

    // Prospective
    if (this.tabEventValue === 1) {
      this.hiredOnFlag = true;
      this.firedOnFlag = true;
      this.resignationOnFlag = true;
    }
    // Active
    // tslint:disable-next-line:one-line
    else if (this.tabEventValue === 2) {
      this.hiredOnFlag = false;
      this.firedOnFlag = true;
      this.resignationOnFlag = true;
    }
    // Terminated
    // tslint:disable-next-line:one-line
    else if (this.tabEventValue === 3) {
      this.hiredOnFlag = true;
      this.firedOnFlag = false;
      this.resignationOnFlag = false;
    }
    this.editpProfessionalDetails = {
      EmployeeProfessionalId: value.EmployeeProfessionalId,
      EmployeeTypeId: value.EmployeeTypeId,
      Status: value.Status,
      OfficeId: value.OfficeId,
      DepartmentId: value.DepartmentId,
      DesignationId: value.DesignationId,
      EmployeeContractTypeId: value.EmployeeContractTypeId,
      HiredOn: value.HiredOn,
      FiredOn: value.FiredOn,
      FiredReason: value.FiredReason,
      ResignationOn: value.ResignationOn,
      ResignationReason: value.ResignationReason,
      JobDescription: value.JobDescription,
      TrainingBenefits: value.TrainingBenefits,
      EmployeeId: this.employeeId
    };

    this.popupProfesionalinfoVisible = true;
    this.OfficeId = this.professionalDetails.OfficeId;
    this.getDepartmentType(this.OfficeId);
    this.DepartmentId = this.professionalDetails.DepartmentId;
  }
  closeProfForm() {
    this.popupProfesionalinfoVisible = false;
  }
  //#endregion

  onFieldDataChanged(data: any) {
    if (data.dataField === 'EmployeeTypeId') {
      // Prospective
      if (data.value === 1) {
        this.hiredOnFlag = true;
        this.firedOnFlag = true;
        this.resignationOnFlag = true;
      }
      // Active
      // tslint:disable-next-line:one-line
      else if (data.value === 2) {
        this.hiredOnFlag = false;
        this.firedOnFlag = true;
        this.resignationOnFlag = true;
      }
      // Terminated
      // tslint:disable-next-line:one-line
      else if (data.value === 3) {
        this.hiredOnFlag = true;
        this.firedOnFlag = false;
        this.resignationOnFlag = false;
      }
    }
  }

  showAssignLeaveLoading() {
    this.professionalInfoLoading = true;
  }

  hideAssignLeaveLoading() {
    this.professionalInfoLoading = false;
  }

  hideAssignLeavePopUp() {
    this.popupAssignLeaveVisible = false;
  }

  //#region "Get All Leave Reason Type"
  GetLeaveReasonTypeDropdown() {
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_LeaveReasonType
      )
      .subscribe(
        data => {
          this.leaveReasonTypeDropdown = [];

          if (
            data.data.LeaveReasonList != null &&
            data.data.LeaveReasonList.length > 0
          ) {
            data.data.LeaveReasonList.forEach(element => {
              this.leaveReasonTypeDropdown.push(element);
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

  getAttendanceGroupList() {
    this.codeService.GetAllDetails(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAttendanceGroups)
      .subscribe(data => {
        this.attendanceGroupList = [];
        if (data.StatusCode === 200) {
          if (data.data.AttendanceGroupMasterList.length > 0
            || data.data.AttendanceGroupMasterList !== undefined
            || data.data.AttendanceGroupMasterList !== null) {
            data.data.AttendanceGroupMasterList.forEach(element => {
              this.attendanceGroupList.push(element);
            });
          }
        } else {
          this.toastr.error(data.Message);
        }
      }, error => {
        if (error.StatusCode === 500) {
          this.toastr.error('Internal Server Error....');
        } else if (error.StatusCode === 401) {
          this.toastr.error('Unauthorized Access Error....');
        } else if (error.StatusCode === 403) {
          this.toastr.error('Forbidden Error....');
        }
      });
  }

  //#region "Get All Holidays & Already applied leave"
  getAllDisableCalendarDate(employeeId) {
    this.hrService
      .GetDataByEmployeeIdAndOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllDisableCalanderDate,
        employeeId,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          this.disabledDates = [];
          if (
            data.StatusCode === 200 &&
            data.data.ApplyLeaveList != null &&
            data.data.ApplyLeaveList.length > 0
          ) {
            data.data.ApplyLeaveList.forEach(element => {
              this.disabledDates.push(
                new Date(
                  new Date(element.Date).getTime() -
                    new Date().getTimezoneOffset() * 60000
                )
              );
            });
          // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400) this.toastr.warning(data.Message);
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

  //#region "Get All Financial Year"
  GetFinancialYearDropdown() {
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetCurrentFinancialYear
      )
      .subscribe(
        data => {
          this.financialYearDropdown = [];

          if (
            data.data.CurrentFinancialYearList != null &&
            data.data.CurrentFinancialYearList.length > 0
          ) {
            data.data.CurrentFinancialYearList.forEach(element => {
              this.financialYearDropdown.push({
                StartDate: new Date(
                  new Date(element.StartDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                EndDate: new Date(
                  new Date(element.EndDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                FinancialYearId: element.FinancialYearId,
                FinancialYearName: element.FinancialYearName
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
}

export class ProfessionalDetails {
  EmployeeProfessionalId?: number;
  EmployeeTypeId: number;
  Status: string;
  OfficeId: number;
  DepartmentId: number;
  DesignationId: number;
  EmployeeContractTypeId: number;
  HiredOn: Date;
  FiredOn: Date;
  FiredReason: string;
  ResignationOn: Date;
  ResignationReason: string;
  JobDescription: string;
  TrainingBenefits: string;
  EmployeeId?: number;
  OfficeName?: string;
  DesignationName?: string;
  DepartmentName?: string;
  EmployeeTypeName?: string;
  AttendanceGroupId?: number;
  AttendanceGroupName?: string;
}
