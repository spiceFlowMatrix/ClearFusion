import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-project-main',
  templateUrl: './project-main.component.html',
  styleUrls: ['./project-main.component.css']
})

export class ProjectMainComponent implements OnInit {
  openInfoTab = 1;
  projectDetailsList: any[];

  addEditProjectFormPopupVisible = false;
  projectDetailsForm: VoucherDetailsModel;
  showNewOpportunityForm = true;


  constructor() { }

  ngOnInit() {

  }

  //#region "initForm"
  initForm() {
    this.projectDetailsForm = {
      ProjectId: null,
      ProjectCode: null,
      ProjectName: null,
      Description: null,
      MeasureType: null,
      UserId: null,
      IsArchive: null,
      ExtensionEndDate: null,
      ExtensionStartDate: null,
      ProjectPhase: null,
      CriteriaScore: null,
      BudgetDetails: null,
      TimeInPhase: null
    };
  }
  //#region "onShowAddProjectForm"
  onShowAddProjectForm() {
    this.initForm();
    this.showAddEditProjectForm();
  }
  //#endregion



  //#region "onFieldDataChanged"
  onFieldDataChanged(e) {

  }
  //#endregion


  //#region "onVoucherSelectionChanged"
  onVoucherSelectionChanged(e) {

  }
  //#endregion


  //#region "show/hide"
  showAddEditProjectForm() {
    this.addEditProjectFormPopupVisible = true;
  }
  hideAddEditProjectForm() {
    this.addEditProjectFormPopupVisible = false;
  }
  //#endregion


  openInfoTabs(e) {
    this.openInfoTab = e;
  }

  onToggleNewOpportunity() {
    this.showNewOpportunityForm = !this.showNewOpportunityForm;
  }

}

class VoucherDetailsModel {
  ProjectId: any;
  ProjectCode: any;
  ProjectName: any;
  Description: any;
  MeasureType: any;
  UserId: any;
  IsArchive: any;
  ExtensionEndDate: any;
  ExtensionStartDate: any;
  ProjectPhase: any;
  CriteriaScore: any;
  BudgetDetails: any;
  TimeInPhase: any;
}
