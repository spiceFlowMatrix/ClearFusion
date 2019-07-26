import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  OnDestroy
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddExtensionsComponent } from '../add-extensions/add-extensions.component';
import {
  IProjectSubActivityListingModel,
  IEditProjectSubActivityModel,
  IProjectActivityDocumentModel,
  IActivityExtensionMode
} from '../../models/project-activities.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { ProjectActivitiesService } from '../../service/project-activities.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';
import { ProjectActivityStatus } from 'src/app/shared/enum';
import { ProjectActivityDocumentsComponent } from '../../project-activity-documents/project-activity-documents.component';
import { DatePipe } from '@angular/common';
import {
  IProjectPeople,
  IProjectRoles
} from '../../../project-details/models/project-people.model';
import { ProjectListService } from '../../../service/project-list.service';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-sub-activities',
  templateUrl: './sub-activities.component.html',
  styleUrls: ['./sub-activities.component.scss']
})
export class SubActivitiesComponent implements OnInit, OnChanges, OnDestroy {
  //#region "variables"
  @Input() subActivityDetail: IProjectSubActivityListingModel;
  @Output() updateActivityStatusId = new EventEmitter<any>();

  projectSubActivityForm: FormGroup;
  SubActivityTitle: string;
  projectSubActivityList: IProjectSubActivityListingModel[] = [];
  // flag
  subActivityCompletedFlag = false;
  startActivityLoaderFlag = false;
  endActivityLoaderFlag = false;
  compltedActivityLoaderFlag = false;
  saveActivityLoaderFlag = false;

  IsEditSubActivityLoaderFlag = false;

  panelOpenState = false;
  tableHeaderList: string[] = ['StartDate', 'EndDate', 'Description'];
  extensionList: IActivityExtensionMode[] = [];

  // subscription destroy
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  // permission
  actualProjectPermissions: IProjectRoles[] = [];
  projectPermissions: IProjectPeople[] = [];
  markCompletePermission = false;
  addExtensionPermission = false;

  // // signal-r
  // private _hubConnection: HubConnection | undefined;

  //#endregion

  constructor(
    public dialog: MatDialog,
    private fb: FormBuilder,
    public activitiesService: ProjectActivitiesService,
    public projectListService: ProjectListService,
    public toastr: ToastrService,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    this.getActivityPermission();
  }

  ngOnChanges() {
    this.initForm();
    const SubActivityTitle = this.subActivityDetail.SubActivityTitle;
    this.SubActivityTitle = SubActivityTitle;
    // this.onChanges();
    this.getAllExtensionList();
  }

  initForm() {
    this.projectSubActivityForm = this.fb.group({
      ActivityDescription: [this.subActivityDetail.ActivityDescription],
      IsCompleted: [this.subActivityDetail.IsCompleted],
      ChallengesAndSolutions: [this.subActivityDetail.ChallengesAndSolutions],
      PlannedStartDate: [this.subActivityDetail.PlannedStartDate],
      PlannedEndDate: [this.subActivityDetail.PlannedEndDate],
      ActualStartDate: [this.subActivityDetail.ActualStartDate],
      ActualEndDate: [this.subActivityDetail.ActualEndDate],
      EmployeeID: [this.subActivityDetail.EmployeeID],
      Progress: [],
      Target: [
        this.subActivityDetail.Target,
        [Validators.min(0), Validators.max(100), Validators.maxLength(3)]
      ],
      Achieved: [
        this.subActivityDetail.Achieved,
        [Validators.min(0), Validators.max(100)]
      ],
      SubActivityTitle: [this.subActivityDetail.SubActivityTitle]
    });
  }

  onChanges(data: any): void {
    this.IsEditSubActivityLoaderFlag = true;
    if (data != null) {
      this.saveActivityLoaderFlag = true;
      this.editProjectSubActivity(data);
    }
  }

  //#region "Permission"
  getActivityPermission() {
    this.activitiesService.activityPermissionSubject
      .pipe(takeUntil(this.destroyed$))
      .subscribe(data => {
        this.projectPermissions = data;
        this.actualProjectPermissions = this.projectListService.GetActivitiesControlRole();
        if (this.projectPermissions.length > 0) {
          // Mark complete permission
          this.markCompletePermission = this.checkMarkCompletePermission();
          // Mark Add Extension permission
          this.addExtensionPermission = this.checkAddExtensionPermission();

          console.log(this.markCompletePermission);
          console.log(this.addExtensionPermission);
        }
      });
  }

  checkMarkCompletePermission(): boolean {
    // NOTE: "PLANNING OFFICER" & "MONITORING OFFICER" can mark as complete
    return this.projectPermissions.filter(
      x =>
        x.RoleId === this.actualProjectPermissions[0].Id ||
        x.RoleId === this.actualProjectPermissions[2].Id
    ).length > 0
      ? true
      : false;
  }

  checkAddExtensionPermission(): boolean {
    // NOTE: Only "PLANNING OFFICER" will Add Extension
    return this.projectPermissions.filter(
      x => x.RoleId === this.actualProjectPermissions[0].Id
    ).length > 0
      ? true
      : false;
  }
  //#endregion

  //#region "editProjectSubActivity"
  editProjectSubActivity(data: IEditProjectSubActivityModel) {
    if (this.projectSubActivityForm.valid) {
      // this.addSubActivityLoaderFlag = true;
      const activityData: IEditProjectSubActivityModel = {
        ActivityId: this.subActivityDetail.ActivityId,
        ActivityDescription: data.ActivityDescription,
        PlannedStartDate: StaticUtilities.getLocalDate(data.PlannedStartDate),
        PlannedEndDate: StaticUtilities.getLocalDate(data.PlannedEndDate),
        EmployeeID: data.EmployeeID,
        Target: data.Target,
        ChallengesAndSolutions: data.ChallengesAndSolutions,
        Achieved: data.Achieved,
        BudgetLineId: data.BudgetLineId,
        SubActivityTitle: data.SubActivityTitle
      };

      if (activityData.PlannedEndDate >= activityData.PlannedStartDate) {
        this.activitiesService
          .EditProjectSubActivity(activityData)
          .pipe(takeUntil(this.destroyed$))
          .subscribe(
            (response: IResponseData) => {
              if (response.statusCode === 200 && response.data != null) {
                this.SubActivityTitle = response.data.SubActivityTitle;
                this.subActivityDetail.Achieved = response.data.Achieved;
                this.saveActivityLoaderFlag = false;
              } else {
                this.toastr.error(response.message);
                this.saveActivityLoaderFlag = false;
              }
              this.IsEditSubActivityLoaderFlag = false;
            },
            error => {
              this.IsEditSubActivityLoaderFlag = false;

              this.toastr.error('Someting went wrong');
              this.saveActivityLoaderFlag = false;
            }
          );
      }
    } else {
    // Note : if form is not valid
    this.IsEditSubActivityLoaderFlag = false;
    }
  }
  //#endregion

  //#region "onExtensionClicked"
  onExtensionClicked() {
    this.openAddExtensionDialog();
  }
  //#endregion

  //#region "onCompleteClicked"
  onCompleteClicked(event: any) {
    this.IsComplete = !this.IsComplete;
    this.compltedActivityLoaderFlag = true;
    if (event != null) {
      const activityId = this.subActivityDetail.ActivityId;
      this.activitiesService
        .ProjectSubActivityIsComplete(activityId)
        .pipe(takeUntil(this.destroyed$))
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200 && response.data !== null) {
              this.updateActivityStatusId.emit(response.data);
              this.subActivityCompletedFlag = !this.subActivityCompletedFlag;
            } else {
              this.toastr.error(response.message);
            }
          },
          error => {
            // this.compltedActivityLoaderFlag = false;
            this.toastr.error('Someting went wrong');
            // this.addSubActivityLoaderFlag = false;
          }
        );
    }
  }

  //#endregion

  //#region "openDocumentsDialog"
  openDocumentsDialog(): void {
    // NOTE: It passed the data into the Document Model

    const modelData: IProjectActivityDocumentModel = {
      HeaderText: 'Sub Activity Documents',
      // DocumentModel: this.planningDocumentsList,
      ProjectPhaseId: ProjectActivityStatus.Implementation,
      ProjectActivityId: this.subActivityDetail.ActivityId
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

  //#region "onStartActivityClicked"
  onStartActivityClicked() {
    this.startActivityLoaderFlag = true;
    this.IsStartSubActivity = !this.IsStartSubActivity;
    const activityId = this.subActivityDetail.ActivityId;
    this.activitiesService
      .StartProjectActivity(activityId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data != null) {
            this.projectSubActivityForm.controls['ActualStartDate'].setValue(
              StaticUtilities.setLocalDate(response.data.ActualStartDate)
            );
            this.startActivityLoaderFlag = false;
            this.updateActivityStatusId.emit(response.data);
            // this.onCancelPopup();
          } else {
            this.toastr.error(response.message);
            this.startActivityLoaderFlag = false;
          }
          // this.addSubActivityLoaderFlag = false;
        },
        error => {
          this.toastr.error('Someting went wrong');
          this.startActivityLoaderFlag = false;
          // this.addSubActivityLoaderFlag = false;
        }
      );
  }
  //#endregion

  //#region "onEndActivityClicked"
  onEndActivityClicked() {
    this.IsEndSubActivity = !this.IsEndSubActivity;
    this.endActivityLoaderFlag = true;
    this.activitiesService
      .EndProjectActivity(this.subActivityDetail.ActivityId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data != null) {
            this.projectSubActivityForm.controls['ActualEndDate'].setValue(
              StaticUtilities.setLocalDate(response.data.ActualEndDate)
            );
            this.endActivityLoaderFlag = false;
            // this.onCancelPopup();
          } else {
            this.toastr.error(response.message);
            this.endActivityLoaderFlag = false;
          }
          // this.addSubActivityLoaderFlag = false;
        },
        error => {
          this.toastr.error('Someting went wrong');
          this.endActivityLoaderFlag = false;
          // this.addSubActivityLoaderFlag = false;
        }
      );
  }
  //#endregion

  //#region "openAddExtensionDialog"
  openAddExtensionDialog() {
    const dialogRef = this.dialog.open(AddExtensionsComponent, {
      width: '550px',
      autoFocus: false,
      data: { ActivityId: this.subActivityDetail.ActivityId }
    });

    dialogRef.componentInstance.onListRefresh.subscribe(x => {
      this.getAllExtensionList();
    });
  }
  //#endregion

  //#region "getAllExtensionList"
  getAllExtensionList() {
    this.extensionList = [];
    this.activitiesService
      .GetAllExtensionList(this.subActivityDetail.ActivityId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach((element: IActivityExtensionMode) => {
              this.extensionList.push({
                ExtensionId: element.ExtensionId,
                ActivityId: element.ActivityId,
                StartDate: this.datePipe.transform(
                  StaticUtilities.getLocalDate(element.StartDate),
                  'dd-MM-yyyy'
                ),
                EndDate: this.datePipe.transform(
                  StaticUtilities.getLocalDate(element.EndDate),
                  'dd-MM-yyyy'
                ),
                Description: element.Description
              });
            });
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "deleteExtension"
  deleteExtension(data: IActivityExtensionMode) {
    const index = this.extensionList.indexOf(data);
    this.extensionList.splice(index, 1);

    this.activitiesService
      .DeleteProjectActivityExtension(data.ExtensionId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            // remove from the list
          }
        },
        error => {
          this.toastr.error('Something went wrong.');
        }
      );
  }
  //#endregion

  get IsComplete() {
    return this.projectSubActivityForm.controls['IsCompleted'].value;
  }

  set IsComplete(completeCHeck) {
    this.projectSubActivityForm.controls['IsCompleted'].setValue(completeCHeck);
  }

  get IsStartSubActivity() {
    return this.projectSubActivityForm.controls['ActualStartDate'].value;
  }

  set IsStartSubActivity(start) {
    this.projectSubActivityForm.controls['ActualStartDate'].setValue(start);
  }
  get IsEndSubActivity() {
    return this.projectSubActivityForm.controls['ActualEndDate'].value;
  }

  set IsEndSubActivity(start) {
    this.projectSubActivityForm.controls['ActualEndDate'].setValue(start);
  }
  //#region "CalculateProgress"
  set CalculateProgress(data) {}

  get CalculateProgress() {
    return (isNaN(
      (this.projectSubActivityForm.controls['Achieved'].value /
        this.projectSubActivityForm.controls['Target'].value) *
        100
    )
      ? 0
      : (this.projectSubActivityForm.controls['Achieved'].value /
          this.projectSubActivityForm.controls['Target'].value) *
        100
    ).toFixed(2);
  }
  //#endregion

  // //#region "Signal-r"
  // subscriptionToPermissionsThroughSignalR() {
  //   // Signal-R
  //   this._hubConnection = new signalR.HubConnectionBuilder()
  //     .withUrl('http://localhost:5004/chathub')
  //     .configureLogging(signalR.LogLevel.Information)
  //     .build();

  //   this._hubConnection.start().catch(err => console.error(err.toString()));

  //   this._hubConnection.on('Send', (data: any) => {
  //     // const received = `Received: ${data}`;
  //     console.log(data);
  //     this.getActivityPermission();
  //   });
  // }
  // //#endregion

  //#region "ngOnDestroy"
  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();

    // this._hubConnection.stop().finally(() => console.log('connection closed')).catch(err => console.error(err.toString()));
  }
  //#endregion
}
