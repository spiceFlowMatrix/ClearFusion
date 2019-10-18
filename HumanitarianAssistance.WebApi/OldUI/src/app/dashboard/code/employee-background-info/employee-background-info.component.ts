import { Component, OnInit } from '@angular/core';
import { CodeService } from '../code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-employee-background-info',
  templateUrl: './employee-background-info.component.html',
  styleUrls: ['./employee-background-info.component.css']
})
export class EmployeeBackgroundInfoComponent implements OnInit {
  ckeditorContentEnglish: string;
  ckeditorContentDari: string;
  selectedTab = 1;
  isEditingAllowed = false;

  constructor(
    private codeService: CodeService,
    private commonService: CommonService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.GetAllContractTypeContent();
    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.EmployeeContract
    );
  }

  GetAllContractTypeContent() {
    this.codeService
      .GetEmployeeContractType(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllContractTypeContent,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
        this.selectedTab
      )
      .subscribe(data => {
        if (
          data.StatusCode === 200 &&
          data.data.ContractTypeContentList != null
        ) {
          this.ckeditorContentEnglish =
            data.data.ContractTypeContentList.ContentEnglish;
          this.ckeditorContentDari =
            data.data.ContractTypeContentList.ContentDari;
        }
      });
  }

  Save() {
    const model: ContractType = {
      EmployeeContractTypeId: this.selectedTab,
      ContentEnglish: this.ckeditorContentEnglish,
      ContentDari: this.ckeditorContentDari,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
    };
    this.codeService
      .getExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_Code_SaveContractContent,
        model
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Content Saved Successfully!');
        } else {
          this.toastr.error('Error!');
        }
      });
  }

  SaveDari() {
    const model: ContractType = {
      EmployeeContractTypeId: this.selectedTab,
      ContentEnglish: this.ckeditorContentEnglish,
      ContentDari: this.ckeditorContentDari,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
    };
    this.codeService
      .getExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_Code_SaveContractContent,
        model
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Content Saved Successfully!');
        } else {
          this.toastr.error('Error!');
        }
      });
  }

  onTabClick(tabIndex) {
     
    this.selectedTab = tabIndex;
    this.ckeditorContentEnglish = '';
    this.ckeditorContentDari = '';
    this.GetAllContractTypeContent();
  }
}

interface ContractType {
  EmployeeContractTypeId: number;
  ContentEnglish?: string;
  ContentDari?: string;
  OfficeId: number;
}
