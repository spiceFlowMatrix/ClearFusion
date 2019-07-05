import { Component, OnInit } from '@angular/core';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-interview-form',
  templateUrl: './interview-form.component.html',
  styleUrls: ['./interview-form.component.css']
})
export class InterviewFormComponent implements OnInit {
  //#region "Variables"
  isEditingAllowed = false;
  interviewFormTabFlag = true;
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
    private commonService: CommonService
  ) {}

  ngOnInit() {
    this.commonService.getEmployeeOfficeId().subscribe(data => {});

    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.Interview
    );
  }

  //#region "Tab Events"
  interviewFormTabClicked() {}
  //#endregion

  //#region "On tab Select"
  selectTab(e) {
    e.itemData.id === 1
      ? (this.interviewFormTabFlag = true)
      : (this.interviewFormTabFlag = false);
  }
  //#endregion
}
