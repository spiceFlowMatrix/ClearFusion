// import { Component } from '@angular/core';
// import { FormBuilder } from '@angular/forms';
// import { BsModalService } from 'ngx-bootstrap';
// import { AppSettingsService } from '../../../../Services/App-settings.Service';
// import { ActivitiesData,Activity, ProjectsService, BudgetType, ResourceType, ActivityLocationType, DayOfMonth, Month } from '../../projects/projects.service';

// @Component({
//   selector: 'app-project-activities-implementation',
//   templateUrl: './project-activities-implementation.component.html',
//   styleUrls: ['./project-activities-implementation.component.css']
// })
// export class ProjectActivitiesImplementationComponent {
  
//   //#region "variable declaration"
//   activity: Activity[];
//   activitiesdata: ActivitiesData;

//   popupAddActivitiesVisible = false;
//   taskType:any;
//   recurringTaskType:any;
  
//   dataSource: any;
//   budgettype: BudgetType[];
//   resourcetype: ResourceType[];
//   activitylocationtype: ActivityLocationType[];
//   dayOfMonth: DayOfMonth[];
//   month: Month[];
//   //#endregion "variable declaration"

//   constructor(private projectsService: ProjectsService,private fb: FormBuilder,private modalService: BsModalService,private setting : AppSettingsService) { 

//     // this.taskType = false;
//     // this.recurringTaskType = false;

//     this.budgettype = this.projectsService.getBudgetType();
//     this.resourcetype = this.projectsService.getResourceType();
//     this.activitylocationtype = this.projectsService.getLocationType();
//     this.dayOfMonth = this.projectsService.getDayOfMonth();
//     this.month = this.projectsService.getMonth();

//     this.activitiesdata = this.projectsService.getActivitiesData();

//     this.activity = this.projectsService.getActivities();
//     this.dataSource = {
//       store: {
//         type: 'array',
//         key: 'ID',
//         data: this.projectsService.getActivities()
//       }
//     }
//   } 



//   //#region "onchange recurring function"

//   // changeTaskType(event: any) {
//   //   if(event.value==1)
//   //   {

//   //   }
//   //   else {
//   //     this.taskType= true;
//   //   }
//   // }

//   // changeRecurringTaskType(event: any) {
//   //   this.recurringTaskType = event.value.ID;
//   // }
  
//   //#endregion "onchange recurring function"

//   //#region "radio-button data"
//       tasks = [
//         { tasktype: "Single Task", tasktypevalue: "SingleTask" },
//         { tasktype: "Recurring Task", tasktypevalue: "RecurringTask" }
//       ];

//       recurringTasks = [
//         { recurringTaskType: "Daily", recurringTaskTypeValue: "Daily" },
//         { recurringTaskType: "Weekly", recurringTaskTypeValue: "Weekly" },
//         { recurringTaskType: "Monthly", recurringTaskTypeValue: "Monthly" },
//         { recurringTaskType: "Yearly", recurringTaskTypeValue: "Yearly" }
//       ];
//   //#endregion "dropdown"

//   //#region "popup"

//     ShowPopup(){
//         this.activitiesdata = this.projectsService.getActivitiesData();
//         this.popupAddActivitiesVisible = true;
//     }

//     HidePopup(){
//         this.popupAddActivitiesVisible = false;
//     }

//     onFormSubmit(model){
//         this.AddActivities(model);
//     }

//     AddActivities(model){
//       var addactivities: ActivitiesData = {
//         ActivityDesc : model.ActivityDesc,
//         PlannedStartDate : model.PlannedStartDate,
//         PlannedEndDate : model.PlannedEndDate,
//         BudgetLine : model.BudgetLine,
//         Resource : model.Resource,
//         LocationOfActivity : model.LocationOfActivity,
//         TaskType : model.TaskType,   
//         RecurringType : model.RecurringType,
//         RecurringDay : model.RecurringDay,
//         RecurringMonth : model.RecurringMonth,
//         RecurringWeekday : model.RecurringWeekday
//       };
      
//     }

//     GetActivitiesDetail(data)
//     {
         
//         this.activitiesdata.ActivityDesc = data.data.ActivityDesc;
//         this.activitiesdata.PlannedStartDate = data.data.PlannedStartDate;
//         this.activitiesdata.PlannedEndDate = data.data.PlannedEndDate;
//         this.activitiesdata.BudgetLine = data.data.BudgetLine;
//         this.activitiesdata.Resource = data.data.Resource;
//         this.activitiesdata.LocationOfActivity = data.data.LocationOfActivity;
//         this.activitiesdata.TaskType = data.data.TaskType;
//         this.activitiesdata.RecurringType = data.data.RecurringType;
//         this.activitiesdata.RecurringDay = data.data.RecurringDay;
//         this.activitiesdata.RecurringMonth = data.data.RecurringMonth;
//         this.activitiesdata.RecurringWeekday = data.data.RecurringWeekday;
//         this.popupAddActivitiesVisible = true;    
//     }

//   //#endregion "popup"
// }
