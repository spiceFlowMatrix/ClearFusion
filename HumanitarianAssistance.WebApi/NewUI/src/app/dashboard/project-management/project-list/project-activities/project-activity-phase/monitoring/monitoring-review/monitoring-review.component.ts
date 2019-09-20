import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProjectActivitiesService } from 'src/app/dashboard/project-management/project-list/project-activities/service/project-activities.service';
import { ToastrService } from 'ngx-toastr';
import { ProjectListService } from '../../../../service/project-list.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';

import { MonitoringModel } from 'src/app/dashboard/project-management/project-list/project-activities/project-activity-phase/monitoring/monitoring-model';
import {
  ProjectIndicatorFilterModel,
  ProjectIndicatorModel,
  IndicatorQuestionModel
} from '../../../../project-indicators/project-indicators-model';
import { ProjectIndicatorService } from '../../../../project-indicators/project-indicator.service';

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
    public indicatorService: ProjectIndicatorService,
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
      totalCount: 0,
      ProjectIndicatorId: null,
      Description: null,
      Questions: null,
      ProjectId: null
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

    // this.monitoringVerificationSource = [
    //   {
    //     id: 1,
    //     name: 'Documents'
    //   },
    //   {
    //     id: 2,
    //     name: 'Communication'
    //   }
    // ];
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

  // //#region "getAllProjectIndicatorList"
  // getAllProjectIndicatorList() {
  //   debugger;
  //   this.projectListService
  //     .GetProjectIndicatorsList(
  //       this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectIndicators,
  //       this.indicatorFilterModel
  //     )
  //     .subscribe(
  //       data => {
  //         this.projectIndicatorList = [];
  //         if (
  //           data.data.ProjectIndicatorList.ProjectIndicators.length > 0 &&
  //           data.StatusCode === 200
  //         ) {
  //           data.data.ProjectIndicatorList.ProjectIndicators.forEach(
  //             element => {
  //               this.projectIndicatorList.push({
  //                 ProjectIndicatorId: element.ProjectIndicatorId,
  //                 IndicatorName: element.IndicatorName,
  //                 IndicatorCode: element.IndicatorCode,
  //                 Questions: element.Questions,
  //                 Description: element.Description
  //               });
  //             }
  //           );
  //         }
  //       },
  //       error => {
  //         this.toastr.success('Something went wrong');
  //       }
  //     );
  // }
  // //#endregion

 //#region "getAllProjectIndicatorList"
 getAllProjectIndicatorList() {
  if (this.projectId != null && this.projectId !== undefined) {
    // this.projectIndicatorListLoaderFlag = true;
    this.indicatorFilterModel.totalCount = 0;
    this.indicatorFilterModel.ProjectId = this.projectId;
    this.projectListService
      .GetProjectIndicatorsList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectIndicators,
        this.indicatorFilterModel
      )
      .subscribe(
        data => {
          this.projectIndicatorList = [];
          if (
            data.data.ProjectIndicatorList !== undefined &&
            data.data.ProjectIndicatorList.ProjectIndicators !== undefined &&
            data.data.ProjectIndicatorList.ProjectIndicators !== null &&
            data.data.ProjectIndicatorList.ProjectIndicators.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.ProjectIndicatorList.ProjectIndicators.forEach(
              element => {
                this.projectIndicatorList.push({
                  ProjectIndicatorId: element.ProjectIndicatorId,
                  IndicatorName: element.IndicatorName,
                  IndicatorCode: element.IndicatorCode,
                  Description: element.Description,
                  Questions: element.Questions
                });
                // this.DonorDetailModel = this.donorList;
              }
            );

            this.indicatorFilterModel.totalCount =
              data.data.ProjectIndicatorList.IndicatorRecordCount;
          }

        },
        error => {
          this.toastr.error('Something went wrong');
        }
      );
  }
}
//#endregion




  // //#region "getQuestionsList" raman
  // getQuestionsList(index: number) {
  //   this.projectIndicatorId = this.monitoringReviewList.MonitoringReviewModel[
  //     index
  //   ].ProjectIndicatorId;
  //   this.indicatorQuestions = [];
  //   this.projectListService
  //     .GetProjectIndicatorQuestionById(
  //       this.appurl.getApiUrl() +
  //         GLOBAL.API_Project_GetProjectIndicatorQuestionsById,
  //       this.projectIndicatorId
  //     )
  //     .subscribe(
  //       data => {
  //         if (data.data.Questions.length > 0 && data.StatusCode === 200) {
  //           this.monitoringReviewList.MonitoringReviewModel[
  //             index
  //           ].IndicatorQuestions = [];
  //           data.data.Questions.forEach(element => {
  //             this.monitoringReviewList.MonitoringReviewModel[
  //               index
  //             ].IndicatorQuestions.push({
  //               QuestionId: element.QuestionId,
  //               Question: element.QuestionText,
  //               Verification: ''
  //             });
  //           });
  //         }
  //       },
  //       error => {
  //         this.toastr.error('Something went wrong');
  //       }
  //     );
  // }
  //#endregion


  getQuestionsList(index: number) {
    this.projectIndicatorId = this.monitoringReviewList.MonitoringReviewModel[
           index
         ].ProjectIndicatorId;
    // if (index !== 0 && index !== undefined && index != null) {
      this.indicatorService.GetIndicatorQuestionById(this.projectIndicatorId)
      .subscribe(
        response => {
          this.indicatorQuestions = [];
          if (response.statusCode === 200) {
            response.data.forEach(element => {
            this.monitoringReviewList.MonitoringReviewModel[
              index
            ].IndicatorQuestions = [];
            response.data.forEach(element => {
              this.monitoringReviewList.MonitoringReviewModel[
                index
              ].IndicatorQuestions.push(element);
              });
            });
          }
          if (response.statusCode === 400){
             this.toastr.error(response.message);
          }
        });
  }



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
