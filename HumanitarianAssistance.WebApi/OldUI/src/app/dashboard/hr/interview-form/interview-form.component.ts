import { Component, OnInit } from '@angular/core';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { CommonService } from '../../../service/common.service';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../../code/code.service';
import { AppSettingsService } from '../../../service/app-settings.service';
import { HrService } from '../hr.service';

@Component({
  selector: 'app-interview-form',
  templateUrl: './interview-form.component.html',
  styleUrls: ['./interview-form.component.css']
})
export class InterviewFormComponent implements OnInit {
  //#region "Variables"
  isEditingAllowed = false;
  interviewFormTabFlag = true;

  officeDropdownList: any[] = [];
  officecodelist: any[];
  selectedOffice: any;

  tabs: any[] = [
    {
      id: 1,
      text: 'Interview Form'
    },
    {
      id: 2,
      text: 'Exit Interview Form'
    }
  ];
  //#endregion

  constructor(
    private commonService: CommonService,
    private toastr: ToastrService,
    private codeService: CodeService,
    private setting: AppSettingsService,
    private hrService: HrService
  ) {}

  ngOnInit() {

    this.getOfficeCodeList();
  }

  //#region "Tab Events"
  interviewFormTabClicked() {}
  //#endregion

  //#region "getOfficeCodeList"

  getOfficeCodeList() {
    this.codeService
        .GetAllCodeList(
            this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
        )
        .subscribe(
            data => {
                this.officecodelist = [];
                if (
                    data.StatusCode === 200 &&
                    data.data.OfficeDetailsList.length > 0
                ) {
                    data.data.OfficeDetailsList.forEach(element => {
                        this.officecodelist.push({
                            Office: element.OfficeId,
                            OfficeCode: element.OfficeCode,
                            OfficeName: element.OfficeName,
                            SupervisorName: element.SupervisorName,
                            PhoneNo: element.PhoneNo,
                            FaxNo: element.FaxNo,
                            OfficeKey: element.OfficeKey
                        });
                    });

                    const AllOffices = localStorage.getItem('ALLOFFICES').split(',');

                    data.data.OfficeDetailsList.forEach(element => {
                        const officeFound = AllOffices.indexOf('' + element.OfficeId);
                        if (officeFound !== -1) {
                            this.officeDropdownList.push({
                                OfficeId: element.OfficeId,
                                OfficeCode: element.OfficeCode,
                                OfficeName: element.OfficeName,
                                SupervisorName: element.SupervisorName,
                                PhoneNo: element.PhoneNo,
                                FaxNo: element.FaxNo,
                                OfficeKey: element.OfficeKey
                            });
                        }
                    });

                    this.selectedOffice =
                        (this.selectedOffice === null || this.selectedOffice === undefined)
                                ? this.officeDropdownList[0].OfficeId
                            : this.selectedOffice;

                            this.isEditingAllowed = this.commonService.IsEditingAllowed(
                              applicationPages.Interview);
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
                } else {
                }
            }
        );
}
//#endregion

onOfficeSelected(officeId: number) {
  this.selectedOffice = officeId;
}
  //#region "On tab Select"
  selectTab(e) {
    e.itemData.id === 1
      ? (this.interviewFormTabFlag = true)
      : (this.interviewFormTabFlag = false);
  }
  //#endregion
}
