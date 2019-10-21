import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap';
import { CodeService } from '../code.service';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';
import { OfficeCode, Department } from '../../../model/code-model';

@Component({
  selector: 'app-department-code',
  templateUrl: './department-code.component.html',
  styleUrls: ['./department-code.component.css']
})
export class DepartmentCodeComponent implements OnInit {
  popupVisible = false;
  showFilterRow: boolean;
  departmentdt: Department[];
  departmentdata: Department;
  officedt: OfficeCode[];

  // loader
  departmentCodeListPopupLoading = false;
  departmentCodePopupLoading = false;
  isEditingAllowed = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private codeservice: CodeService,
    private commonservice: CommonService
  ) {
    this.departmentdata = this.codeservice.getDepartmentData();
    this.showFilterRow = true;
  }

  ngOnInit() {
    this.GetAllDepartmentList();
    this.GetOfficeList();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.Department
    );
  }

  ShowPopup() {
    this.departmentdata = this.codeservice.getDepartmentData();
    this.popupVisible = true;
  }

  HidePopup() {
    this.popupVisible = false;
  }

  GetOfficeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          this.officedt = [];
          data.data.OfficeDetailsList.forEach(element => {
            this.officedt.push(element);
          });
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

  GetAllDepartmentList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Department_GetAllDepartment
      )
      .subscribe(
        data => {
          this.departmentdt = [];
          if (data.StatusCode === 200 && data.data.Departments.length > 0) {
            data.data.Departments.forEach(element => {
              this.departmentdt.push(element);
            });
          }
          this.commonservice.setLoader(false);
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

  onFormSubmit(model) {
    if (model.DepartmentId === 0) {
      this.AddDepartmentDetail(model);
    } else {
      this.EditDepartmentDetail(model);
    }
  }

  AddDepartmentDetail(model) {
    this.departmentCodePopupLoading = true;
    const addDepartment: Department = {
      DepartmentId: 0,
      DepartmentName: model.DepartmentName,
      OfficeId: model.OfficeId,
      OfficeName: ''
    };
    this.codeservice
      .AddEditDepartmentDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Department_AddDepartment,
        addDepartment
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Department Added Successfully!!!');
          this.GetAllDepartmentList();
          this.HidePopup();
        } else if (data.StatusCode === 900) {
          this.toastr.error(data.Message);
        } else {
          this.toastr.error('Error!!!');
        }
        this.departmentCodePopupLoading = false;
      });
  }

  EditDepartmentDetail(model) {
    this.departmentCodePopupLoading = true;

    const editdepartment: Department = {
      DepartmentId: model.DepartmentId,
      DepartmentName: model.DepartmentName,
      OfficeId: model.OfficeId,
      OfficeName: ''
    };
    this.codeservice
      .AddEditDepartmentDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Department_EditDepartment,
        editdepartment
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Department Updated Successfully!!!');
          this.GetAllDepartmentList();
          this.HidePopup();
        } else if (data.StatusCode === 900) {
          this.toastr.error(data.Message);
        } else {
          this.toastr.error('Error!!!');
        }
        this.departmentCodePopupLoading = false;
      });
  }

  GetDepartmentDetailByDepartmentId(data) {
    this.departmentdata.DepartmentId = data.data.DepartmentId;
    this.departmentdata.DepartmentName = data.data.DepartmentName;
    this.departmentdata.OfficeId = data.data.OfficeId;
    this.popupVisible = true;
  }
}
