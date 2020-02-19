import { Component, OnInit } from '@angular/core';
import { HrService } from 'src/app/hr/services/hr.service';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-contract-clauses',
  templateUrl: './contract-clauses.component.html',
  styleUrls: ['./contract-clauses.component.scss']
})
export class ContractClausesComponent implements OnInit {
  officeId: number;
  contractTypeId: number;
  ckeditorContentEnglish: string;
  ckeditorContentDari: string;
  isContractSaved = false;
  constructor(
    private hrService: HrService,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService
  ) {}

  ngOnInit() {
    // tslint:disable-next-line: radix
    this.officeId = parseInt(localStorage.getItem('SelectedOfficeId'));
    this.contractTypeId = 1;
    this.GetAllContractTypeContent();
  }

  GetAllContractTypeContent() {
    this.commonLoader.showLoader();
    this.ckeditorContentEnglish = '';
    this.ckeditorContentDari = '';
    this.hrService
      .GetAllContractTypeContent(this.officeId, this.contractTypeId)
      .subscribe(
        x => {
          this.commonLoader.hideLoader();
          this.ckeditorContentEnglish =
            x.data.ContractTypeContentList.ContentEnglish;
          this.ckeditorContentDari = x.data.ContractTypeContentList.ContentDari;
        },
        error => {
          this.commonLoader.hideLoader();
        }
      );
  }

  SaveContractContent() {
    this.isContractSaved = true;
    const model = {
      EmployeeContractTypeId: this.contractTypeId,
      ContentEnglish: this.ckeditorContentEnglish,
      ContentDari: this.ckeditorContentDari,
      OfficeId: this.officeId
    };
    this.hrService
      .saveContractContent(model)
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Content Saved Successfully!');
          this.isContractSaved = false;
        } else {
          this.toastr.error('Error!');
          this.isContractSaved = false;
        }
      });
  }

  onTabChange(event: any) {
    if (event.index === 0) {
      this.contractTypeId = 1;
    } else if (event.index === 1) {
      this.contractTypeId = 2;
    } else if (event.index === 2) {
      this.contractTypeId = 3;
    }
    this.GetAllContractTypeContent();
  }
}
