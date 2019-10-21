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
  selector: 'app-designation-type',
  templateUrl: './designation-type.component.html',
  styleUrls: ['./designation-type.component.css']
})
export class DesignationTypeComponent implements OnInit {
  designationList: Designation[];
  designationData: Designation;
  designationpopupVisible = false;
  isEditingAllowed = false;

  // loader
  designationTypeListLoading = false;
  designationTypePopupLoading = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private setting: AppSettingsService,
    private codeservice: CodeService,
    private commonservice: CommonService
  ) {}

  ngOnInit() {
    this.getDesignationList();
    this.initializeForm();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.Designation
    );
  }

  initializeForm() {
    this.designationData = {
      DesignationId: null,
      Designation: null
    };
  }

  getDesignationList() {
    this.showHideDesignationTypeListLoading(true);

    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllDesignation
      )
      .subscribe(
        data => {
          this.designationList = [];
          if (data.StatusCode === 200 && data.data.DesignationList != null) {
            if (data.data.DesignationList.length > 0) {
            data.data.DesignationList.forEach(element => {
              this.designationList.push({
                DesignationId: element.DesignationId,
                Designation: element.Designation
              });
            });
          }
        }
          this.showHideDesignationTypeListLoading(false);
        },
        error => {
          this.showHideDesignationTypeListLoading(false);
        }
      );
  }

  onFormSubmit(model: Designation) {
    if (model.DesignationId === 0 || model.DesignationId == null) {
      this.addDesignationDetail(model);
    } else {
      this.editDesignationDetail(model);
    }
  }

  addDesignationDetail(model: Designation) {
    this.designationTypePopupLoading = true;

    const addDesignationData: Designation = {
      DesignationId: 0,
      Designation: model.Designation
    };
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddDesignation,
        addDesignationData
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Designation Added Successfully!!!');
            this.getDesignationList();
            this.HidePopup();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Designation already exist.');
          } else {
            this.toastr.error('Error!!!');
          }
          this.designationTypePopupLoading = false;
        },
        error => {
          this.designationTypePopupLoading = false;
        }
      );
  }

  editDesignationDetail(model: Designation) {
    this.designationTypePopupLoading = true;

    const editDesignationData: Designation = {
      DesignationId: model.DesignationId,
      Designation: model.Designation
    };
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditDesignation,
        editDesignationData
      )
      .subscribe(data => {
        if ((data.StatusCode = 200)) {
          this.toastr.success('Designation Updated Successfully!!!');
          this.getDesignationList();
          this.HidePopup();
        } else if (data.StatusCode === 900) {
          this.toastr.error('Designation already exist.');
        } else {
          this.toastr.error('Error!!!');
        }
        this.designationTypePopupLoading = false;
      });
  }

  showDesignationPopup(data: any) {
    this.designationData.DesignationId = data.data.DesignationId;
    this.designationData.Designation = data.data.Designation;
    this.designationpopupVisible = true;
  }

  ShowPopup() {
    this.designationData = {
      DesignationId: null,
      Designation: null
    };
    this.designationpopupVisible = true;
  }
  HidePopup() {
    this.designationpopupVisible = false;
  }

  showHideDesignationTypeListLoading(flag: boolean) {
    this.designationTypeListLoading = flag;
  }
}

export interface Designation {
  DesignationId: number;
  Designation: string;
}
