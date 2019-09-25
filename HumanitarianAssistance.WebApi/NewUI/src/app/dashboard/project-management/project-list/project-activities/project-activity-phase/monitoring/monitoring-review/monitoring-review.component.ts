import {
  Component,
  OnInit,
  Inject,
  EventEmitter,
  ɵConsole
} from '@angular/core';
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
import { element } from '@angular/core/src/render3';

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
    console.log('edit list', this.monitoringReviewList);
  }

  indicatorFilterModel: ProjectIndicatorFilterModel;
  projectIndicatorList: ProjectIndicatorModel[];
  indicatorQuestions: IndicatorQuestionModel[];
  monitoringReviewList: MonitoringModel;
  monitoringQuantitativeScore: any[];
  monitoringQualitativeScore: any[];
  questionTypeList: any[];
  projectIndicatorId: number;
  projectId: any;
  activityId: any;
  filterLoaderFlag = false;
  refreshMonitoringPhase = new EventEmitter();

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  QuestionTypeId: any = null;

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
    // console.log(this.monitoringReviewList);
    this.getAllProjectIndicatorList();

    if (
      this.monitoringReviewList === undefined &&
      this.monitoringReviewList == null
    ) {
      this.initializeModel();
    } else {
      // for (let i = 0; i < this.monitoringReviewList.MonitoringReviewModel.length;  i++  ) {
      //   this.getQuestionsList(i);
      // }
    }

    this.questionTypeList = [
      {
        Id: 1,
        Name: 'Qualitative'
      },
      {
        Id: 2,
        Name: 'Quantitative'
      }
    ];

    this.monitoringQuantitativeScore = [
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

    this.monitoringQualitativeScore = [
      {
        score: 1,
        name: '1 - Yes'
      },
      {
        score: 0,
        name: '0 - No'
      }
    ];
  }

  initializeModel() {
    this.monitoringReviewList = {
      MonitoringReviewModel: [
        {
          ProjectIndicatorId: 0,
          IndicatorQuestions: [],
          QuestionTypeId: 0
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
                    QuestionType: element.QuestionType,
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

  getQuestionsList(index: number) {
    debugger;
    this.projectIndicatorId = this.monitoringReviewList.MonitoringReviewModel[
      index
    ].ProjectIndicatorId;
    if (
      this.projectIndicatorId != null &&
      this.projectIndicatorId !== undefined &&
      this.projectIndicatorId !== 0
    ) {
      this.indicatorService
        .GetIndicatorQuestionById(this.projectIndicatorId)
        .subscribe(response => {
          this.indicatorQuestions = [];
          if (response.statusCode === 200) {
            this.monitoringReviewList.MonitoringReviewModel[
              index
            ].IndicatorQuestions = [];
            response.data.forEach(elemnt => {
              this.monitoringReviewList.MonitoringReviewModel[
                index
              ].IndicatorQuestions.push(elemnt);
            });
          }
          console.log(
            'getquestionlist',
            this.monitoringReviewList.MonitoringReviewModel
          );
          if (response.statusCode === 400) {
            this.toastr.error(response.message);
          }
        });
    }
  }

  addMonitoringIndicator() {
    this.monitoringReviewList.MonitoringReviewModel.push({
      ProjectIndicatorId: 0,
      IndicatorQuestions: [],
      QuestionTypeId: 0
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
    debugger;
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
        // filter the record on the bases of question type
        this.monitoringReviewList.MonitoringReviewModel[
          i
        ].IndicatorQuestions = this.monitoringReviewList.MonitoringReviewModel[
          i
        ].IndicatorQuestions.filter(
          x =>
            x.QuestionType ===
            this.monitoringReviewList.MonitoringReviewModel[i].QuestionTypeId
        );
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
    console.log(this.monitoringReviewList);

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
