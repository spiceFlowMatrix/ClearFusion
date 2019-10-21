import { Component, OnInit } from '@angular/core';
import { ActivitiesData, ProjectsService, ActivityLocationType, ResourceType, BudgetType, Activity, MonitoringData, RatingType } from '../../projects/projects.service';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap';

@Component({
  selector: 'app-project-activities-monitoring',
  templateUrl: './project-activities-monitoring.component.html',
  styleUrls: ['./project-activities-monitoring.component.css']
})

export class ProjectActivitiesMonitoringComponent implements OnInit {

    monitoringdata: MonitoringData;
    popupVisible = false;

    activity: Activity[];
    dataSource: any;
    budgettype: BudgetType[];
    resourcetype: ResourceType[];   
    activitylocationtype: ActivityLocationType[];
    ratingtype: RatingType[];
    openPopupVisible = false;
    
    constructor(private projectsService: ProjectsService,private fb: FormBuilder,private modalService: BsModalService) { 

      this.activity = this.projectsService.getActivities();
      this.budgettype = this.projectsService.getBudgetType();
      this.resourcetype = this.projectsService.getResourceType();
      this.activitylocationtype = this.projectsService.getLocationType();
      this.ratingtype = this.projectsService.getRatingType();   

      this.monitoringdata = this.projectsService.getMonitoringData();
      this.dataSource = {
        store: {
          type: 'array',
          key: 'ID',
          data: this.projectsService.getActivities()
        }
    }
  } 

  ngOnInit() {
  }
  
  //#region "radio-button data"
  tasks = [
    { tasktype: "Single Task", tasktypevalue: "SingleTask" },
    { tasktype: "Recurring Task", tasktypevalue: "RecurringTask" }
  ];

  recurringtasks = [
    { recurringTaskType: "Daily", recurringTaskTypeValue: "Daily" },
    { recurringTaskType: "Weekly", recurringTaskTypeValue: "Weekly" },
    { recurringTaskType: "Monthly", recurringTaskTypeValue: "Monthly" },
    { recurringTaskType: "Yearly", recurringTaskTypeValue: "Yearly" }
  ];
//#endregion "dropdown"

  //#region "popup"
    ShowPopup()
    {
        this.monitoringdata = this.projectsService.getMonitoringData();
        this.popupVisible = true;
    }
  
    HidePopup()
    {
        this.popupVisible = false;
    }

    onFormSubmit(model)
    {
        this.AddMonitoringActivities(model);
    }

    AddMonitoringActivities(model)
    {
      var addmonitoringactivities: MonitoringData = {
        Rating : model.Rating,
        VerificationSource : model.VerificationSource,
        Strengths : model.Strengths,
        Weeknesses : model.Weeknesses,
        Challenges : model.Challenges,
        Recommendations : model.Recommendations,
        Comments : model.Comments,
        FrequencyOfMonitoring : model.FrequencyOfMonitoring
      }
    }

    openPopup() {
      this.openPopupVisible = true;
    }

  //#endregion "popup"    

}
