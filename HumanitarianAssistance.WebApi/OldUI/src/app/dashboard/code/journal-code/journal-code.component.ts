import { Component, OnInit } from '@angular/core';
import {
  CodeService,
  JournalCodeData,
  DeleteJournalCode
} from '../code.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { JournalCodeList } from '../../../model/code-model';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-journal-code',
  templateUrl: './journal-code.component.html',
  styleUrls: ['./journal-code.component.css']
})
export class JournalCodeComponent implements OnInit {
  showFilterRow: boolean;
  popupAddJournalVisible = false;
  journalcodedata: JournalCodeData;
  journalcodelist: JournalCodeList[];
  isEditingAllowed = false;

  // loader
  journalCodeListLoading = false;
  journalCodePopupLoading = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private codeservice: CodeService,
    private commonservice: CommonService
  ) {
    this.showFilterRow = true;
    this.journalcodedata = this.codeservice.getJournalCodeData();
  }

  ngOnInit() {
    this.getJournalCodeList();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.JournalCodes
    );
  }

  // TODO: Event for ADD, UPDATE, DELETE
  logEvent(eventName) {}

  UpdateJournalCode(eventName) {}

  ShowPopup() {
    this.journalcodedata = this.codeservice.getJournalCodeData();
    this.popupAddJournalVisible = true;
  }

  HidePopup() {
    this.popupAddJournalVisible = false;
  }

  // Get all Journal Details
  getJournalCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_JournalCode_GetAllJournalDetail
      )
      .subscribe(
        data => {
          this.journalcodelist = [];

          if (
            data.StatusCode === 200 &&
            data.data.JournalDetailList.length > 0
          ) {
            data.data.JournalDetailList.forEach(element => {
              this.journalcodelist.push({
                JournalCode: element.JournalCode,
                JournalName: element.JournalName
              });
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
          } else {
          }
          this.commonservice.setLoader(false);
        }
      );
  }

  onFormSubmit(model) {
    this.AddJournalCode(model);
  }

  AddJournalCode(model) {
    this.journalCodePopupLoading = true;
    const addjournalcode: JournalCodeData = {
      JournalCode: 0,
      JournalName: model.JournalName
    };
    this.codeservice
      .AddEditJournalCode(
        this.setting.getBaseUrl() + GLOBAL.API_JournalCode_AddJournalDetail,
        addjournalcode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Journal Code Added Successfully!!!');
            this.getJournalCodeList();
            this.HidePopup();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Journal Name already exist!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.journalCodePopupLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  EditJournalCode(model) {
    this.journalCodePopupLoading = true;
    const addjournalcode: JournalCodeData = {
      JournalCode: model.key.JournalCode,
      JournalName: model.key.JournalName
    };
    this.codeservice
      .AddEditJournalCode(
        this.setting.getBaseUrl() + GLOBAL.API_JournalCode_EditJournalDetail,
        addjournalcode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.HidePopup();
            this.toastr.success('Journal Code Updated Successfully!!!');
            this.getJournalCodeList();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Journal Name already exist!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.journalCodePopupLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  DeleteJournalCode(model) {
    const deletejournalcode: DeleteJournalCode = {
      JournalCode: model.key.JournalCode
    };
    this.codeservice
      .DeleteJournalCode(
        this.setting.getBaseUrl() + GLOBAL.API_JournalCode_DeleteJournalDetail,
        deletejournalcode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Journal Code Deleted Successfully!!!');
            this.getJournalCodeList();
          } else {
            this.toastr.error('Error!!!');
          }
        },
        error => {
          // error message
        }
      );
  }
}
