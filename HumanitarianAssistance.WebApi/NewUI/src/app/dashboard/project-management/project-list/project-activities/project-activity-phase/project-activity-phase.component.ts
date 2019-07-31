import {
  Component,
  OnInit,
  HostListener,
  Input,
  OnChanges,
  OnDestroy,
  Output,
  EventEmitter,
  ViewChild
} from '@angular/core';
import {
  IDocumentsModel,
  IProjectActivityDetail
} from '../models/project-activities.model';
import { ProjectActivitiesService } from '../service/project-activities.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';
import { ProjectPlanningComponent } from './project-planning/project-planning.component';
import { MatDialog } from '@angular/material/dialog';
import { MonitoringReviewComponent } from 'src/app/dashboard/project-management/project-list/project-activities/project-activity-phase/monitoring/monitoring-review/monitoring-review.component';
import { AddSubActivitiesComponent } from './add-sub-activities/add-sub-activities.component';
import { MonitoringComponent } from './monitoring/monitoring.component';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { SignalRService } from 'src/app/shared/services/signal-r.service';
import { NotifySignalRService } from 'src/app/shared/services/notify-signalr.service';

@Component({
  selector: 'app-project-activity-phase',
  templateUrl: './project-activity-phase.component.html',
  styleUrls: ['./project-activity-phase.component.scss']
})
export class ProjectActivityPhaseComponent
  implements OnInit, OnChanges, OnDestroy {
  //#region "variables"

  @ViewChild(ProjectPlanningComponent) planningChild: ProjectPlanningComponent;
  @ViewChild(MonitoringComponent) projectMonitoringChild: MonitoringComponent;

  @Input() projectId;
  @Input() activityDetail: IProjectActivityDetail;
  @Input() projectActivityListById: IProjectActivityDetail;
  @Input() budgetLineList: any[] = [];
  @Input() employeeList: any[] = [];
  @Input() officeList: any[] = [];
  @Input() provinceSelectionList: any[] = [];
  @Input() districtMultiSelectList: any[] = [];
  @Input() activityByIdLoader: any;
  @Input() districtLoaderFlag: boolean;

  @Input() recurringTypeList: any[] = [];
  @Output() updateActivity = new EventEmitter<any>();
  @Output() refreshProjectSummary = new EventEmitter();
  @Output() updateActivityStatusId = new EventEmitter<IProjectActivityDetail>();
  @Output() selectedProvinceDetailId = new EventEmitter<any>();
  @Output() parentActivityListRefresh = new EventEmitter<any>();

  activityId: any;

  planningDocumentsList: IDocumentsModel[] = [];
  projectSubActivityList: any[] = [];

  editActivityLoader = false;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  // screen scroll
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;
  //#endregion

  constructor(
    public signalRService: NotifySignalRService,
    public activitiesService: ProjectActivitiesService,
    private toastr: ToastrService,
    public dialog: MatDialog
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.initactivityDetail();
    this.getActivitiesControlPermission();
    this.projectActivityPermissionListen();
  }

  ngOnChanges() {
    if (this.activityByIdLoader === true) {
      this.planningChild.activityListLoader = true;
    } else {
      this.planningChild.activityListLoader = false;
    }
    if (
      this.activityDetail.ActivityId != null &&
      this.activityDetail.ActivityId !== 0 &&
      this.activityDetail.ActivityId !== undefined
    ) {
      this.activityId = this.activityDetail.ActivityId;
      this.getAllProjectSubActivityDetails(this.activityId);
    }

  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;
    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 180 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "initactivityDetail"
  initactivityDetail() {
    this.activityDetail = {
      // Planning
      ActivityId: null,
      ActivityName: null,
      ActivityDescription: null,
      PlannedStartDate: null,
      PlannedEndDate: null,
      BudgetLineId: null,
      EmployeeID: null,
      OfficeId: null,
      StatusId: null,
      Recurring: null,
      RecurringCount: null,
      RecurrinTypeId: null,
      ProvinceId: null,
      DistrictID: null,

      ActualStartDate: null,
      ActualEndDate: null,
      ParentId: null,

      // Other Properties
      Progress: null,
      Slippage: null,

      IsError: false,
      IsLoading: false
    };
  }
  //#endregion

  //#region "getActivitiesControlPermission"
  getActivitiesControlPermission() {

    this.activitiesService
      .GetActivitiesControlPermission(this.projectId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data != null) {

            // console.log(response.data);
            this.activitiesService.setActivityPermissions(response.data);

          } else {
            this.toastr.error(response.message);
          }

        },
        error => {

        }
      );
  }
  //#endregion



  //#region "editProjectActivity"
  editProjectActivity(data: any) {
    this.editActivityLoader = true;

    const projectActivityDetail: IProjectActivityDetail = {
      // Planning
      ActivityId: data.ActivityId,
      ActivityName: data.ActivityName,
      ActivityDescription: data.ActivityDescription,
      PlannedStartDate: data.PlannedStartDate,
      PlannedEndDate: data.PlannedEndDate,
      BudgetLineId: data.BudgetLineId,
      EmployeeID: data.EmployeeID,
      OfficeId: data.OfficeId,
      StatusId: data.StatusId,
      Recurring: data.Recurring,
      RecurringCount: data.RecurringCount,
      RecurrinTypeId: data.RecurrinTypeId,
      DistrictID: data.DistrictID,
      ProvinceId: data.ProvinceId,
      ParentId: data.ParentId,

      ActualStartDate: data.ActualStartDate,
      ActualEndDate: data.ActualEndDate,

    };

    this.activitiesService
      .EditProjectActivity(projectActivityDetail)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            this.toastr.success('Activity updated successfully');
            this.updateActivity.emit(projectActivityDetail);
          } else {
            this.toastr.error(response.message);
          }
          this.editActivityLoader = false;
          this.refreshProjectSummary.emit();
        },
        error => {
          this.toastr.error('Someting went wrong');
          this.editActivityLoader = false;
        }
      );
  }
  //#endregion

  //#region "onSaveProjectActivity"
  onSaveProjectActivity() {
    if (
      this.planningChild.projectActivityForm.valid
    ) {
      this.editProjectActivity(this.activityDetail);
    } else {
      this.toastr.warning('Please fill the correct values');
    }
  }
  //#endregion

  //#region "selectedProvinceId emit event to activity listing"
  onSelectedProvinceId(event: any) {
    this.selectedProvinceDetailId.emit(event);
  }
  //#endregion
  onUpdateActivityStatusId(data: any) {
    this.updateActivityStatusId.emit(data);
  }

  //#region "openAddProjectMonitoringDialog"
  openAddProjectMonitoringDialog(): void {
    // NOTE: It passed the data into the Add Voucher Model

    const dialogRef = this.dialog.open(MonitoringReviewComponent, {
      width: '550px',
      data: {
        projectId: this.projectId,
        activityId: this.activityId
      },
      autoFocus: false
    });

    dialogRef.componentInstance.refreshMonitoringPhase.subscribe(() => {
      this.refreshMonitoringPhase();
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  refreshMonitoringPhase() {
    this.projectMonitoringChild.getProjectMonitoringList();
  }

  openAddSubActivityDialog(): void {
    // NOTE: It passed the data into the Add Voucher Model

    const dialogRef = this.dialog.open(AddSubActivitiesComponent, {
      width: '550px',
      data: {
        BudgetLineId: this.activityDetail.BudgetLineId,
        ActivityId: this.activityDetail.ActivityId,
        EmployeeList: this.employeeList
      },
      autoFocus: false
    });
    // to refresh the list
    dialogRef.componentInstance.onSubactivityListRefresh.subscribe((data) => {
      // check for status id is planning
      this.updateActivityStatusId.emit(data);
      this.getAllProjectSubActivityDetails(this.activityId);
    });

  }
  //#endregion


  //#region "getAllProjectSubActivityDetails 03/05/2019"
  getAllProjectSubActivityDetails(id) {
    this.projectSubActivityList = [];
    this.activitiesService.GetAllProjectSubActivityDetail(id)
    .pipe(takeUntil(this.destroyed$))
    .subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.projectSubActivityList = response.data;
        }
      },
      error => {
      }
    );
  }
  //#endregion

  projectActivityPermissionListen() {
    this.signalRService.activityPermissionChangedOn();

    this.signalRService.activityPermission$.subscribe((x: any) => {
      this.getActivitiesControlPermission();
    });
  }


  //#region "ngOnDestroy"
  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
  //#endregion
}
