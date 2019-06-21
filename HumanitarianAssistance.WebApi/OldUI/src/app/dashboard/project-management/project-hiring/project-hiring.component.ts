import { Component, OnInit } from '@angular/core';
import { CodeService } from '../../code/code.service';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-project-hiring',
  templateUrl: './project-hiring.component.html',
  styleUrls: ['./project-hiring.component.css']
})
export class ProjectHiringComponent implements OnInit {
  //#region "Variables"

  // DataSource
  projectDataSource: any[];
  hiringPanelDataSource: any[];

  selectedTab = 1;

  // tab
  tabs: any[] = [
    {
      id: 1,
      text: 'New Candidate'
    },
    {
      id: 2,
      text: 'Existing Employees'
    }
  ];

  // loading
  projectHiringLoading = false;

  //#endregion

  constructor(
    private codeservice: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.hiringPanelDataSource = [];
    this.getAllProjectDetails();
  }

  //#region getAllProjectDetails
  getAllProjectDetails() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetProjectAndBudgetLine
      )
      .subscribe(
        data => {
          this.projectDataSource = [];
          if (
            data.data.ProjectBudgetLinesModel != null &&
            data.StatusCode === 200
          ) {
            if (data.data.ProjectBudgetLinesModel.ProjectList.length > 0) {
              data.data.ProjectBudgetLinesModel.ProjectList.forEach(element => {
                this.projectDataSource.push(element);
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "selectTab"
  selectTab(e) {
    console.log(e);
    this.selectedTab = e.id;
  }
  //#endregion

  //#region "onProjectSelectionChanged"
  onProjectSelectionChanged(e) {
    console.log(e);
  }
  //#endregion

  //#region "logEvent"
  logEvent(eventName, obj) {}
  //#endregion

  //#region "show/hide"
  showHideProjectHiringLoading(flag: boolean) {
    this.projectHiringLoading = flag;
  }
  //#endregion
}
