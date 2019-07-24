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
  selector: 'app-qualification-type',
  templateUrl: './qualification-type.component.html',
  styleUrls: ['./qualification-type.component.css']
})
export class QualificationTypeComponent implements OnInit {
  qualificationList: Qualification[];
  qualificationData: Qualification;
  qualificationpopupVisible = false;
  isEditingAllowed = false;

  // loader
  qualificationListLoading = false;
  qualificationPopupLoading = false;

  deleteQualificationPopupLoading = false;

// confirmation     
  deleteConfirmationPopupVisible = false;


  constructor(
    private router: Router,
    private toastr: ToastrService,
    private setting: AppSettingsService,
    private codeservice: CodeService,
    private commonservice: CommonService
  ) {}

  ngOnInit() {
    this.getQualificationList();
    this.initializeForm();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.Qualification
    );
  }

  initializeForm() {
    this.qualificationData = {
      QualificationId: null,
      QualificationName: null
    };
  }

  getQualificationList() {
    this.showHideQualificationListLoading(true);
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllQualification
      )
      .subscribe(
        data => {
          this.qualificationList = [];
          if (
            data.StatusCode === 200 &&
            data.data.QualificationDetailsList.length > 0
          ) {
            data.data.QualificationDetailsList.forEach(element => {
              this.qualificationList.push({
                QualificationId: element.QualificationId,
                QualificationName: element.QualificationName
              });
            });
          }
          this.showHideQualificationListLoading(false);
        },
        error => {
          this.showHideQualificationListLoading(false);
        }
      );
  }

  onFormSubmit(model) {
    if (model.QualificationId === 0 || model.QualificationId == null) {
      this.addQualificationDetail(model);
    } else {
      this.editQualificationDetail(model);
    }
  }

  addQualificationDetail(model: Qualification) {
    this.qualificationPopupLoading = true;
    const addQualificationData: Qualification = {
      QualificationId: 0,
      QualificationName: model.QualificationName
    };
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_AddQualificationDetails,
        addQualificationData
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Qualification Added Successfully!!!');
          this.getQualificationList();
          this.HidePopup();
        } else if (data.StatusCode === 900) {
          this.toastr.error('Qualification already exist.');
        } else {
          this.toastr.error('Error!!!');
        }
        this.qualificationPopupLoading = false;
      });
  }

  editQualificationDetail(model) {
    this.qualificationPopupLoading = true;
    const editQualificationData: Qualification = {
      QualificationId: model.QualificationId,
      QualificationName: model.QualificationName
    };
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_EditQualifactionDetails,
        editQualificationData
      )
      .subscribe(data => {
        if ((data.StatusCode = 200)) {
          this.toastr.success('Qualification Updated Successfully!!!');
          this.getQualificationList();
          this.HidePopup();
        } else if (data.StatusCode === 900) {
          this.toastr.error('Qualification already exist.');
        } else {
          this.toastr.error('Error!!!');
        }
        this.qualificationPopupLoading = false;
      });
  }

  showQualificationPopup(data: any) {
    this.qualificationData.QualificationId = data.data.QualificationId;
    this.qualificationData.QualificationName = data.data.QualificationName;
    this.qualificationpopupVisible = true;
  }

  ShowPopup() {
    this.qualificationData = {
      QualificationId: null,
      QualificationName: null
    };
    this.qualificationpopupVisible = true;
  }

  HidePopup() {
    this.qualificationpopupVisible = false;
  }

  showHideQualificationListLoading(flag: boolean) {
    this.qualificationListLoading = flag;
  }

  //#region "deleteQualificationById"
  deleteQualificationById(data: any) {
    if (data != null) {
      this.qualificationData = {
        QualificationId: data.data.QualificationId,
        QualificationName: data.data.QualificationName
      };
      this.showDeleteQualificationPopup();
    }
  }
  //#endregion

  showDeleteQualificationPopup(){
    this.deleteConfirmationPopupVisible = true;
  }

  hideDeleteQualificationPopup() {
    this.deleteConfirmationPopupVisible = false;
  }

  showDeleteQualificationPopupLoading() {
    this.deleteQualificationPopupLoading = true;
  }
  hideDeleteQualificationPopupLoading() {
    this.deleteQualificationPopupLoading = false;
  }

 //#region "Delete qualification "
 deleteQualificationDetail() {
  if (this.qualificationData != null) {
    this.showDeleteQualificationPopupLoading();

    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_DeleteQualifactionDetails,
        this.qualificationData
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Deleted Successfully!!!');
        } else if (data.StatusCode === 900) {
          this.toastr.error(data.Message);
        } else {
          this.toastr.error(data.Message);
        }

        this.getQualificationList();
        this.hideDeleteQualificationPopup();
        this.hideDeleteQualificationPopupLoading();
      });
  }
}
//#endregion

}



export interface Qualification {
  QualificationId: number;
  QualificationName: string;
}
