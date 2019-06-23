import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../../../code/code.service';
import { GLOBAL } from '../../../../shared/global';
import { OfficeCode } from '../../../../model/code-model';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-bidding-committee',
  templateUrl: './bidding-committee.component.html',
  styleUrls: ['./bidding-committee.component.css']
})
export class BiddingCommitteeComponent implements OnInit {
  officecodedt: OfficeCode[];

  constructor(
    private setting: AppSettingsService,
    private codeservice: CodeService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          this.officecodedt = [];

          if (
            data.StatusCode === 200 &&
            data.data.OfficeDetailsList.length > 0
          ) {
            data.data.OfficeDetailsList.forEach(element => {
              this.officecodedt.push({
                OfficeId: element.OfficeId,
                OfficeCode: element.OfficeCode,
                OfficeName: element.OfficeName,
                SupervisorName: element.SupervisorName,
                PhoneNo: element.PhoneNo,
                FaxNo: element.FaxNo,
                OfficeKey: element.OfficeKey
              });
            });
          }
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
}
