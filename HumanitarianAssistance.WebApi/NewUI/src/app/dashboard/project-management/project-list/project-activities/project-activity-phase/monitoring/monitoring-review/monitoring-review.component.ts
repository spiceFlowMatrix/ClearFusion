import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectActivitiesService } from 'src/app/dashboard/project-management/project-list/project-activities/service/project-activities.service';
import { ToastrService } from 'ngx-toastr';
import { ProjectListService } from '../../../../service/project-list.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import {
  ProjectIndicatorFilterModel,
  ProjectIndicatorModel,
  IndicatorQuestionModel
} from 'src/app/dashboard/project-management/project-indicators/project-indicators-model';
import { MonitoringModel } from 'src/app/dashboard/project-management/project-list/project-activities/project-activity-phase/monitoring/monitoring-model';

@Component({
  selector: 'app-monitoring-review',
  templateUrl: './monitoring-review.component.html',
  styleUrls: ['./monitoring-review.component.scss']
})
export class MonitoringReviewComponent implements OnInit {
  constructor(
    public activitiesService: ProjectActivitiesService,
    private toastr: ToastrService,
    public projectListService: ProjectListService,
    private appurl: AppUrlService,
    public dialogRef: MatDialogRef<MonitoringReviewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.initializeModel();
    this.projectId = data.projectId;
    this.activityId = data.activityId;
    this.monitoringReviewList = data.monitoringReviewModel;
  }

  indicatorFilterModel: ProjectIndicatorFilterModel;
  projectIndicatorList: ProjectIndicatorModel[];
  indicatorQuestions: IndicatorQuestionModel[];
  monitoringReviewList: MonitoringModel;
  monitoringScore: any[];
  monitoringVerificationSource: any[];
  projectIndicatorId: number;
  projectId: any;
  activityId: any;
  filterLoaderFlag = false;
  refreshMonitoringPhase = new EventEmitter();

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  ngOnInit() {
    this.indicatorFilterModel = {
      pageIndex: 0,
      pageSize: 0,
      totalCount: 0
    };

    this.getAllProjectIndicatorList();

    if (this.monitoringReviewList === undefined) {
      this.initializeModel();
    }

    this.monitoringScore = [
      {
        score: 1,
        name: '1 - Weak'
      },
      {
        score: 2,
        name: '2 - Satisfactory'
      },
      {
        score: 3,
        name: '3 - Average'
      },
      {
        score: 4,
        name: '4 - Good'
      },
      {
        score: 5,
        name: '5 - Excellent'
      }
    ];

    this.monitoringVerificationSource = [
      {
        id: 1,
        name: 'Documents'
      },
      {
        id: 2,
        name: 'Communication'
      }
    ];
  }

  initializeModel() {
    this.monitoringReviewList = {
      MonitoringReviewModel: [
        {
          ProjectIndicatorId: 0,
          IndicatorQuestions: []
        }
      ],
      MonitoringDate: new Date(),
      NegativePoints: '',
      PositivePoints: '',
      Recommendations: '',
      Remarks: '',
      ActivityId: this.activityId,
      ProjectId: this.projectId
    };
  }

  //#region "getAllProjectIndicatorList"
  getAllProjectIndicatorList() {
    this.projectListService
      .GetProjectIndicatorsList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectIndicators,
        this.indicatorFilterModel
      )
      .subscribe(
        data => {
          this.projectIndicatorList = [];
          if (
            data.data.ProjectIndicatorList.ProjectIndicators.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.ProjectIndicatorList.ProjectIndicators.forEach(
              element => {
                this.projectIndicatorList.push({
                  projectIndicatorId: element.ProjectIndicatorId,
                  projectIndicatorName: element.IndicatorName,
                  projectIndicatorCode: element.IndicatorCode
                });
              }
            );
          }
        },
        error => {
          this.toastr.success('Something went wrong');
        }
      );
  }
  //#endregion

  //#region "getQuestionsList"
  getQuestionsList(index: number) {
    this.projectIndicatorId = this.monitoringReviewList.MonitoringReviewModel[
      index
    ].ProjectIndicatorId;
    this.indicatorQuestions = [];
    this.projectListService
      .GetProjectIndicatorQuestionById(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetProjectIndicatorQuestionsById,
        this.projectIndicatorId
      )
      .subscribe(
        data => {
          if (data.data.Questions.length > 0 && data.StatusCode === 200) {
            this.monitoringReviewList.MonitoringReviewModel[
              index
            ].IndicatorQuestions = [];
            data.data.Questions.forEach(element => {
              this.monitoringReviewList.MonitoringReviewModel[
                index
              ].IndicatorQuestions.push({
                QuestionId: element.QuestionId,
                Question: element.QuestionText,
                Verification: ''
              });
            });
          }
        },
        error => {
          this.toastr.error('Something went wrong');
        }
      );
  }
  //#endregion

  addMonitoringIndicator() {
    this.monitoringReviewList.MonitoringReviewModel.push({
      ProjectIndicatorId: 0,
      IndicatorQuestions: []
    });
    this.projectIndicatorId = 0;
  }

  onDelete(index: number) {
    this.monitoringReviewList.MonitoringReviewModel.splice(index, 1);
  }

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  saveMonitoringReview() {
    if (
      this.monitoringReviewList.ProjectMonitoringReviewId === 0 ||
      this.monitoringReviewList.ProjectMonitoringReviewId === null ||
      this.monitoringReviewList.ProjectMonitoringReviewId === undefined
    ) {
      this.addMonitoringReview();
    } else {
      this.editMonitoringReview();
    }
  }

  //#region "addMonitoringReview"
  addMonitoringReview() {
    if (this.monitoringReviewList.MonitoringReviewModel.length > 0) {
      if (this.monitoringReviewList.MonitoringDate === '') {
        this.toastr.warning('Monitoring Date not selected');
        return;
      }

      for (
        let i = 0;
        i < this.monitoringReviewList.MonitoringReviewModel.length;
        i++
      ) {
        if (
          this.monitoringReviewList.MonitoringReviewModel[i]
            .ProjectIndicatorId === 0
        ) {
          this.toastr.warning('Indicator not selected');
          return;
        }
      }
    }

    this.filterLoaderFlag = true;

    this.monitoringReviewList.MonitoringDate = this.setDateTime(
      this.monitoringReviewList.MonitoringDate
    );

    this.projectListService
      .AddProjectMonitoringReview(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddProjectMonitoringReview,
        this.monitoringReviewList
      )
      .subscribe(
        data => {
          this.projectIndicatorList = [];
          if (data.StatusCode === 200) {
            this.toastr.success('Monitoring review added successfully');
            this.filterLoaderFlag = false;
            this.refreshMonitoringPhase.emit();
            this.dialogRef.close();
          } else if (data.StatusCode === 400) {
            this.filterLoaderFlag = false;
          }
        },
        error => {
          this.filterLoaderFlag = false;
          this.toastr.error('Something went wrong');
        }
      );
  }
  //#endregion

  //#region "addMonitoringReview"
  editMonitoringReview() {
    if (this.monitoringReviewList.MonitoringReviewModel.length > 0) {
      if (this.monitoringReviewList.MonitoringDate === '') {
        this.toastr.warning('Monitoring Date not selected');
        return;
      }

      for (
        let i = 0;
        i < this.monitoringReviewList.MonitoringReviewModel.length;
        i++
      ) {
        if (
          this.monitoringReviewList.MonitoringReviewModel[i]
            .ProjectIndicatorId === 0
        ) {
          this.toastr.warning('Indicator not selected');
          return;
        }
      }
    }

    this.filterLoaderFlag = true;

    this.monitoringReviewList.MonitoringDate = this.setDateTime(
      this.monitoringReviewList.MonitoringDate
    );

    this.projectListService
      .EditProjectMonitoringReview(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_EditProjectMonitoringByMonitoringId,
        this.monitoringReviewList
      )
      .subscribe(
        data => {
          this.projectIndicatorList = [];
          if (data.StatusCode === 200) {
            this.toastr.success('Monitoring review updated successfully');
            this.filterLoaderFlag = false;
            this.refreshMonitoringPhase.emit();
            this.dialogRef.close();
          } else if (data.StatusCode === 400) {
            this.filterLoaderFlag = false;
          }
        },
        error => {
          this.filterLoaderFlag = false;
          this.toastr.error('Something went wrong');
        }
      );
  }
  //#endregion

  //#region "setDateTime"
  setDateTime(data): any {
    return new Date(
      new Date(data).getFullYear(),
      new Date(data).getMonth(),
      new Date(data).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
  }
  //#endregion
}
