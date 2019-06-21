import { Component } from '@angular/core';
import {
  Activity,
  ProjectsService,
  ActivitiesData,
  BudgetType,
  ResourceType,
  ActivityLocationType,
  Month,
  DayOfMonth
} from '../projects.service';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-project-activities',
  templateUrl: './project-activities.component.html',
  styleUrls: ['./project-activities.component.css']
})
export class ProjectActivitiesComponent {
  //#region "variable declaration"
  activity: Activity[];
  activitiesdata: ActivitiesData;

  popupAddActivitiesVisible = false;
  taskType: any;
  recurringTaskType: any;

  dataSource: any;
  budgettype: BudgetType[];
  resourcetype: ResourceType[];
  activitylocationtype: ActivityLocationType[];
  dayOfMonth: DayOfMonth[];
  month: Month[];
  //#endregion "variable declaration"

  constructor(
    private projectsService: ProjectsService,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private setting: AppSettingsService
  ) {
    // this.taskType = false;
    // this.recurringTaskType = false;

    this.budgettype = this.projectsService.getBudgetType();
    this.resourcetype = this.projectsService.getResourceType();
    this.activitylocationtype = this.projectsService.getLocationType();
    this.dayOfMonth = this.projectsService.getDayOfMonth();
    this.month = this.projectsService.getMonth();

    this.activitiesdata = this.projectsService.getActivitiesData();
    this.activity = this.projectsService.getActivities();
    this.dataSource = {
      store: {
        type: 'array',
        key: 'ID',
        data: this.projectsService.getActivities()
      }
    };
  }

  //#region "onchange recurring function"

  // changeTaskType(event: any) {
  //   if(event.value==1)
  //   {

  //   }
  //   else {
  //     this.taskType= true;
  //   }
  // }

  // changeRecurringTaskType(event: any) {
  //   this.recurringTaskType = event.value.ID;
  // }

  //#endregion "onchange recurring function"

  //#region "radio-button data"
  tasks = [
    { tasktype: 'Single Task', tasktypevalue: 'SingleTask' },
    { tasktype: 'Recurring Task', tasktypevalue: 'RecurringTask' }
  ];

  recurringTasks = [
    { recurringTaskType: 'Daily', recurringTaskTypeValue: 'Daily' },
    { recurringTaskType: 'Weekly', recurringTaskTypeValue: 'Weekly' },
    { recurringTaskType: 'Monthly', recurringTaskTypeValue: 'Monthly' },
    { recurringTaskType: 'Yearly', recurringTaskTypeValue: 'Yearly' }
  ];
  //#endregion "dropdown"

  //#region "popup"

  ShowPopup() {
    this.activitiesdata = this.projectsService.getActivitiesData();
    this.popupAddActivitiesVisible = true;
  }

  HidePopup() {
    this.popupAddActivitiesVisible = false;
  }

  onFormSubmit(model) {
    this.AddActivities(model);
  }

  AddActivities(model) {
    const addactivities: ActivitiesData = {
      ActivityDesc: model.ActivityDesc,
      PlannedStartDate: model.PlannedStartDate,
      PlannedEndDate: model.PlannedEndDate,
      BudgetLine: model.BudgetLine,
      Resource: model.Resource,
      LocationOfActivity: model.LocationOfActivity,
      TaskType: model.TaskType
    };
  }
  //#endregion "popup"
}
