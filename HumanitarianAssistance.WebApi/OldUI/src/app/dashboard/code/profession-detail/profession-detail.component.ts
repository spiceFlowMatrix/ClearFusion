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
import { Profession } from '../../../model/code-model';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-profession-detail',
  templateUrl: './profession-detail.component.html',
  styleUrls: ['./profession-detail.component.css']
})
export class ProfessionDetailComponent implements OnInit {
  popupVisible = false;
  showFilterRow: boolean;
  professiondt: Profession[];
  professiondata: Profession;
  isEditingAllowed = false;

  // loader
  professionDetailLoading = false;
  professionDetailPopupLoading = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private codeservice: CodeService,
    private commonservice: CommonService
  ) {
    this.professiondata = this.codeservice.getProfessionData();
    this.showFilterRow = true;
  }

  ngOnInit() {
    this.getProfessionList();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.Profession
    );
  }

  ShowPopup() {
    this.professiondata = this.codeservice.getProfessionData();
    this.popupVisible = true;
  }

  HidePopup() {
    this.popupVisible = false;
  }

  getProfessionList() {
    // this.professionDetailLoading = true;
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllProfession
      )
      .subscribe(
        data => {
          this.professiondt = [];
          if (data.StatusCode === 200 && data.data.ProfessionList.length > 0) {
            data.data.ProfessionList.forEach(element => {
              this.professiondt.push({
                ProfessionId: element.ProfessionId,
                ProfessionName: element.ProfessionName
              });
            });
          }

          // this.professionDetailLoading = false;
          this.commonservice.setLoader(false);
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

  GetProfessionDetailById(data) {
    this.professiondata.ProfessionId = data.data.ProfessionId;
    this.professiondata.ProfessionName = data.data.ProfessionName;
    this.popupVisible = true;
  }

  onFormSubmit(model) {
    if (model.ProfessionId === 0) {
      this.AddProfessionDetail(model);
    } else {
      this.EditProfessionDetail(model);
    }
  }

  AddProfessionDetail(model) {
    this.professionDetailPopupLoading = true;
    const addprofession: Profession = {
      ProfessionId: 0,
      ProfessionName: model.ProfessionName
    };
     
    this.codeservice
      .AddEditProfessionDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Profession_AddProfession,
        addprofession
      )
      .subscribe(data => {
         
        if (data.StatusCode === 200) {
          this.toastr.success('Profession Added Successfully!!!');
          this.getProfessionList();
          this.HidePopup();
        } else if (data.StatusCode === 900) {
          this.toastr.error('Profession already exist.');
        } else {
          this.toastr.error('Error!!!');
        }
        this.professionDetailPopupLoading = false;
      });
  }

  EditProfessionDetail(model) {
    this.professionDetailPopupLoading = true;
    const editprofession: Profession = {
      ProfessionId: model.ProfessionId,
      ProfessionName: model.ProfessionName
    };
    this.codeservice
      .AddEditProfessionDetail(
        this.setting.getBaseUrl() + GLOBAL.API_Profession_EditProfession,
        editprofession
      )
      .subscribe(data => {
        if ((data.StatusCode = 200)) {
          this.toastr.success('Profession Update Successfully!!!');
          this.getProfessionList();
          this.HidePopup();
        } else if (data.StatusCode === 900) {
          this.toastr.error('Profession already exist.');
        } else {
          this.toastr.error('Error!!!');
        }
        this.professionDetailPopupLoading = false;
      });
  }
}
