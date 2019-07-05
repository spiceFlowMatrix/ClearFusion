import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap';
import { GLOBAL } from '../../../shared/global';
import { StoreService } from '../../store/store.service';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-store-source-codes',
  templateUrl: './store-source-codes.component.html',
  styleUrls: ['./store-source-codes.component.css']
})
export class StoreSourceCodesComponent implements OnInit {
  showFilterRow: boolean;
  popupAddStoreSourceCodeVisible = false;
  sourceCodeData: SourceCodeData;
  sourceCodeDatalist: SourceCodeData[];
  sourceTypeDataList: SourceTypeData[];
  selectedSourceType: number;
  isEditingAllowed = false;

  // loader
  journalCodeListLoading = false;
  StoreCodeLoading = false;

  constructor(
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private commonservice: CommonService,
    private storeService: StoreService
  ) {}

  ngOnInit() {
    this.selectedSourceType = 1;
    this.getSourceTypelist();
    this.getSourceCodeDatalist();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.StoreSourceCodes
    );
  }

  // Get all Source Code Data Details
  getSourceCodeDatalist() {
    // this.journalCodeListLoading = true;
    this.storeService
      .GetSourceCode(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllStoreSourceCode,
        this.selectedSourceType
      )
      .subscribe(
        data => {
          this.sourceCodeDatalist = [];

          if (
            data.StatusCode === 200 &&
            data.data.SourceCodeDatalist.length > 0
          ) {
            data.data.SourceCodeDatalist.forEach(element => {
              this.sourceCodeDatalist.push({
                SourceCodeId: element.SourceCodeId,
                Code: element.Code,
                Address: element.Address,
                Description: element.Description,
                EmailAddress: element.EmailAddress,
                Fax: element.Fax,
                Guarantor: element.Guarantor,
                Phone: element.Phone,
                CodeTypeId: element.CodeTypeId
              });
            });
          }
          // this.journalCodeListLoading = false;
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

  // Get all Source Type Details
  getSourceTypelist() {
    this.storeService
      .GetSourceCodeType(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllStoreSourceType
      )
      .subscribe(
        data => {
          this.sourceTypeDataList = [];

          if (
            data.StatusCode === 200 &&
            data.data.SourceCodeTypelist.length > 0
          ) {
            data.data.SourceCodeTypelist.forEach(element => {
              this.sourceTypeDataList.push({
                Id: element.CodeTypeId,
                SourceTypeName: element.CodeTypeName
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

  sourceTypeChanged(eventName: any) {
    this.selectedSourceType = eventName.value;
    this.getSourceCodeDatalist();
  }

  ShowPopup() {
    this.storeService
      .GetStoreSourceCode(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetStoreTypeCode,
        this.selectedSourceType
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.sourceCodeData = {
              SourceCodeId: 0,
              Address: null,
              Code: data.data.StoreSourceCode,
              Description: null,
              EmailAddress: null,
              Fax: null,
              Guarantor: null,
              Phone: null,
              CodeTypeId: 0
            };

            this.popupAddStoreSourceCodeVisible = true;
          } else if (data.StatusCode === 900) {
            this.toastr.error('Something went wrong!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.StoreCodeLoading = false;
        },
        error => {
          this.toastr.error('Something went wrong!!!');
        }
      );
  }

  HidePopup() {
    this.popupAddStoreSourceCodeVisible = false;
  }

  onFormSubmit(model) {
    this.AddStoreSourceCode(model);
  }

  AddStoreSourceCode(model) {
    this.StoreCodeLoading = true;

    const addStoreCode: SourceCodeData = {
      SourceCodeId: 0,
      Address: model.Address,
      Code: model.Code,
      Description: model.Description,
      EmailAddress: model.EmailAddress,
      Fax: model.Fax,
      Guarantor: model.Guarantor,
      Phone: model.Phone,
      CodeTypeId: this.selectedSourceType
    };

    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_AddStoreSourceCode,
        addStoreCode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Store Source Code Added Successfully!!!');
            this.getSourceCodeDatalist();
            this.HidePopup();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Something went wrong!!!');
            this.HidePopup();
          } else {
            this.toastr.error(data.Message);
            this.HidePopup();
          }
          this.StoreCodeLoading = false;
        },
        error => {
          this.toastr.error('Something went wrong!!!');
        }
      );
  }

  EditStoreSourceCode(model) {
    this.StoreCodeLoading = true;

    const addStoreSourceCode: SourceCodeData = {
      Address: model.key.Address,
      Code: model.key.Code,
      CodeTypeId: this.selectedSourceType,
      Description: model.key.Description,
      EmailAddress: model.key.EmailAddress,
      Fax: model.key.Fax,
      Guarantor: model.key.Guarantor,
      Phone: model.key.Phone,
      SourceCodeId: model.key.SourceCodeId
    };
    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditStoreSourceCode,
        addStoreSourceCode
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.HidePopup();
            this.toastr.success('Store Source Code Updated Successfully!!!');
            this.getSourceCodeDatalist();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Something went wrong!!!');
          } else {
            this.toastr.error('Error!!!');
          }
          this.StoreCodeLoading = false;
        },
        error => {
          // error message
        }
      );
  }

  DeleteStoreSourceCode(model) {
    const sourceCodeId = model.key.SourceCodeId;

    this.storeService
      .DeleteStoreSourceCode(
        this.setting.getBaseUrl() + GLOBAL.API_Store_DeleteStoreSourceCode,
        sourceCodeId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Store Source Code Deleted Successfully!!!');
            this.getSourceCodeDatalist();
          } else {
            this.toastr.error('Error!!!');
          }
        },
        error => {
          // error message
        }
      );
  }

  logEvent(e: any) {}

  onFieldDataChanged(e) {
    if(e.dataField === "Phone"){

      let phone= e.value.toString();
      if(phone.length>14 || phone.length<10){
        this.toastr.warning('Phone Number should be between 10-14 digits!!!');
      }
    }
  }

  functionCache = {};
  validateRange(min, max) {
    if (!this.functionCache[`min${min}max${max}`])
      this.functionCache[`min${min}max${max}`] = (options: any) => {
        return options.value >= min && options.value <= max;
      }
    return this.functionCache[`min${min}max${max}`]
  }
}

export interface SourceCodeData {
  SourceCodeId: number;
  Code: any;
  Description: string;
  Address: string;
  Phone: any;
  Fax: any;
  EmailAddress: any;
  Guarantor: any;
  CodeTypeId: number;
}

export interface SourceTypeData {
  Id: number;
  SourceTypeName: string;
}
