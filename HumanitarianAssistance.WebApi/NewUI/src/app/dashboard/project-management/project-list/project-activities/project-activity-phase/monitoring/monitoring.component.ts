import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  OnDestroy
} from '@angular/core';
import { ProjectActivitiesService } from 'src/app/dashboard/project-management/project-list/project-activities/service/project-activities.service';
import { ToastrService } from 'ngx-toastr';
import { MonitoringModel } from './monitoring-model';
import { MatDialog } from '@angular/material/dialog';
import { MonitoringReviewComponent } from './monitoring-review/monitoring-review.component';
import {
  IProjectActivityDocumentModel,
  IDocumentsModel
} from '../../models/project-activities.model';
import { ProjectActivityPhases } from 'src/app/shared/enum';
import { ProjectActivityDocumentsComponent } from '../../project-activity-documents/project-activity-documents.component';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';

@Component({
  selector: 'app-monitoring',
  templateUrl: './monitoring.component.html',
  styleUrls: ['./monitoring.component.scss']
})
export class MonitoringComponent implements OnInit, OnChanges, OnDestroy {
  constructor(
    public activitiesService: ProjectActivitiesService,
    public toastr: ToastrService,
    public dialog: MatDialog
  ) {}
  @Input() projectId: number;
  @Input() activityId: number;
  @Input() monitoingDocumentsList: IDocumentsModel[] = [];
  @Output() documentListRefresh = new EventEmitter<any>();

  projectMonitoring: MonitoringModel[] = [];
  editProjectMonitoringModel: MonitoringModel;
  monitoringId = 0;

  // Subscription
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  //#endregion

  tableHeaderList: string[] = ['Question', 'Verification', 'Score'];

  ngOnInit() {
    this.getProjectMonitoringList();
  }

  initializeModel() {
    this.editProjectMonitoringModel = {
      ProjectMonitoringReviewId: 0,
      ActivityId: 0,
      MonitoringDate: '',
      MonitoringReviewModel: [],
      NegativePoints: '',
      PositivePoints: '',
      ProjectId: 0,
      Recommendations: '',
      Remarks: ''
    };
  }

  //#region "ngOnChanges"
  ngOnChanges() {
    if (
      this.activityId !== 0 &&
      this.activityId !== null &&
      this.activityId !== undefined
    ) {
      this.getProjectMonitoringList();
    }
  }
  //#endregion

  //#region "getProjectMonitoringList"
  getProjectMonitoringList() {
    this.projectMonitoring = [];
    if (this.activityId !== undefined && this.activityId !== 0) {
      this.activitiesService
        .getProjectMonitoringList(this.activityId)
        .pipe(takeUntil(this.destroyed$))
        .subscribe(
          (data: IResponseData) => {
            if (data.statusCode === 200 && data.data != null) {
              data.data.forEach(x => {
                this.projectMonitoring.push({
                  ActivityId: x.ActivityId,
                  NegativePoints: x.NegativePoints,
                  PositivePoints: x.PositivePoints,
                  Recommendations: x.Recommendations,
                  Remarks: x.Remarks,
                  ProjectMonitoringReviewId: x.ProjectMonitoringReviewId,
                  ProjectId: x.ProjectId,
                  MonitoringReviewModel: x.MonitoringReviewModel,
                  MonitoringDate: StaticUtilities.setLocalDate(
                    x.MonitoringDate
                  ),
                  Rating: x.Rating
                });
              });
            }
          },
          error => {
            this.toastr.error('Something went wrong');
          }
        );
    }
  }
  //#endregion

  //#region "openAddProjectMonitoringDialog"
  openEditProjectMonitoringDialog(Id: number): void {
    if (Id !== 0) {
      const index = this.projectMonitoring.findIndex(
        x => x.ProjectMonitoringReviewId === Id
      );
      this.editProjectMonitoringModel = this.projectMonitoring[index];
      this.openDialog(this.editProjectMonitoringModel);
    }
  }
  //#endregion

  //#region "openDialog"
  openDialog(monitoringModel: MonitoringModel) {
    const dialogRef = this.dialog.open(MonitoringReviewComponent, {
      width: '550px',
      data: {
        monitoringReviewModel: monitoringModel
      },
      autoFocus: false
    });

    dialogRef.componentInstance.refreshMonitoringPhase.subscribe(() => {});

    dialogRef.afterClosed().subscribe(result => {
      this.getProjectMonitoringList();
    });
  }
  //#endregion

  //#region "openDocumentsDialog"
  openDocumentsDialog(Id: number): void {
    // NOTE: It passed the data into the Document Model
    this.monitoringId = Id;

    const modelData: IProjectActivityDocumentModel = {
      HeaderText: 'Monitoring Documents',
      // DocumentModel: this.monitoingDocumentsList,
      ProjectPhaseId: ProjectActivityPhases.Monitoring,
      ProjectActivityId: this.activityId,
      MonitoringId: this.monitoringId
    };

    const dialogRef = this.dialog.open(ProjectActivityDocumentsComponent, {
      width: '400px',
      maxHeight: '500px',
      autoFocus: false,
      data: modelData
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "ngOnDestroy"
  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
  //#endregion
}
