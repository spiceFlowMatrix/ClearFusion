import { Component, OnInit } from '@angular/core';
import { CodeService } from '../../code/code.service';
import { GLOBAL } from '../../../shared/global';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-task-activity',
  templateUrl: './task-activity.component.html',
  styleUrls: ['./task-activity.component.css']
})
export class TaskActivityComponent implements OnInit {
  popupVisibleAddTask = false;
  popupVisibleAddActivity = false;

  taskModel: any;
  activityModel: any;
  Status: any[];
  taskDiv: any[];

  projectList: any[];
  priorityArr: any[];

  allTaskArr: any[];
  allActivityArr: any[];
  withAnimationOptionsVisible: any = false;

  constructor(
    private codeservice: CodeService,
    private setting: AppSettingsService
  ) {
    this.Initialize();
  }

  ngOnInit() {
    this.GetAllProjectDetails();
    this.GetAllTask();
    this.GetAllActivity();
  }

  Initialize() {
    this.taskModel = {
      TaskId: null,
      TaskName: null,
      Description: null,
      Priority: null
    };

    this.priorityArr = [
      {
        Priority: 'High'
      },
      {
        Priority: 'Medium'
      },
      {
        Priority: 'Low'
      }
    ];

    this.activityModel = {
      ActivityName: null,
      Priority: null,
      Description: null,
      Status: null
    };

    this.Status = [
      {
        Id: 1,
        Value: 'Active'
      },
      {
        Id: 2,
        Value: 'Inactive'
      }
    ];
  }

  GetAllProjectDetails() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_GetAllProjectDetail
      )
      .subscribe(
        data => {
          this.projectList = [];
          if (data.data.ProjectDetailList != null && data.StatusCode === 200) {
            data.data.ProjectDetailList.forEach(element => {
              this.projectList.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            // this.toastr.error("Internal Server Error....");
          } else if (error.StatusCode === 401) {
            // this.toastr.error("Unauthorized Access Error....");
          } else if (error.StatusCode === 403) {
            // this.toastr.error("Forbidden Error....");
          }
        }
      );
  }

  GetAllTask() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_TaskAndActivity_GetAllTask
      )
      .subscribe(
        data => {
          this.allTaskArr = [];
          if (data.data.TaskMasterList != null && data.StatusCode === 200) {
            data.data.TaskMasterList.forEach(element => {
              this.allTaskArr.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            // this.toastr.error("Internal Server Error....");
          } else if (error.StatusCode === 401) {
            // this.toastr.error("Unauthorized Access Error....");
          } else if (error.StatusCode === 403) {
            // this.toastr.error("Forbidden Error....");
          }
        }
      );
  }

  GetAllActivity() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_TaskAndActivity_GetAllActivity
      )
      .subscribe(
        data => {
          this.allActivityArr = [];
          if (data.data.ActivityMasterList != null && data.StatusCode === 200) {
            data.data.ActivityMasterList.forEach(element => {
              this.allActivityArr.push(element);
            });
          }
          this.taskDiv = [];
          this.allActivityArr.forEach(element => {
            this.taskDiv.push({
              TaskId: element.TaskId,
              TaskName: element.TaskName,
              Priority: element.Priority,
              Activity: [
                {
                  ActivityId: element.ActivityId,
                  ActivityName: element.ActivityName
                }
              ]
            });
          });
        },
        error => {
          if (error.StatusCode === 500) {
            // this.toastr.error("Internal Server Error....");
          } else if (error.StatusCode === 401) {
            // this.toastr.error("Unauthorized Access Error....");
          } else if (error.StatusCode === 403) {
            // this.toastr.error("Forbidden Error....");
          }
        }
      );
  }

  toggleWiithShadingOptions() {
    this.withAnimationOptionsVisible = !this.withAnimationOptionsVisible;
  }

  addTaskShowPopup() {
    this.popupVisibleAddTask = true;
  }

  addActivityShowPopup() {
    this.popupVisibleAddActivity = true;
  }

  HidePopup() {
    this.popupVisibleAddTask = false;
    this.popupVisibleAddActivity = false;
    this.Initialize();
  }
}
