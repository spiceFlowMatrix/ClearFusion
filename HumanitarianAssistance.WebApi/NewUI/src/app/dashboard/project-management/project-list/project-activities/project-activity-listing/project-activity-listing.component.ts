import { Component, OnInit, HostListener, OnDestroy } from '@angular/core';
import { ProjectActivityAddComponent } from '../project-activity-add/project-activity-add.component';
import { MatDialog } from '@angular/material/dialog';
import {
  ProjectActivityFilterModel,
  IBudgetLine,
  IEmployeeList,
  IProjectActivityDetail,
  IProjectSummaryModel
} from '../models/project-activities.model';
import {
  ProjectActivitiesService,
  IProjectPhasesModel
} from '../service/project-activities.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';

import { StaticUtilities } from 'src/app/shared/static-utilities';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
@Component({
  selector: 'app-project-activity-listing',
  templateUrl: './project-activity-listing.component.html',
  styleUrls: ['./project-activity-listing.component.scss']
})
export class ProjectActivityListingComponent implements OnInit, OnDestroy {
  //#region "variables"
  projectId: number;
  recurringTypeList: any[] = [];
  subActivityList: any[] = [];
  budgetLineList: IBudgetLine[] = [];
  employeeList: any[] = [];
  officeList: any[] = [];
  provinceSelectionList: any[] = [];
  districtMultiSelectList: any[] = [];
  activitySummary: IProjectSummaryModel;
  projectPhasesList: IProjectPhasesModel[] = [];

  projectActivityFilter: ProjectActivityFilterModel;
  projectActivityList: IProjectActivityDetail[] = [];
  projectActivityListById: IProjectActivityDetail[] = [];

  selectedProjectActivityId: number;
  activityDetail: IProjectActivityDetail;
  activityListLoader = false;
  activitySummaryLoader = false;
  activityByIdLoader = false;
  districtLoaderFlag = false;

  deleteActivitySubscribe: Subscription;

  showProjectActivityDetail = false;
  colsm6 = 'col-sm-10 col-sm-offset-1';

  // private _hubConnection: HubConnection | undefined;
  // message = '';
  // messages: string[] = [];

  totalCount = 0;

  // flag
  filterViewFlag = true;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  //#endregion

  constructor(
    public dialog: MatDialog,
    public activitiesService: ProjectActivitiesService,
    private routeActive: ActivatedRoute
  ) {
    this.getScreenSize();
    // this.projectId = +this.routeActive.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
      this.getAllProvinceList();
    });

    this.initializeForm();
    this.getAllProjectActivityStatus();
    this.recurringTypeList = this.activitiesService.GetProjectRecurringTypeList();
    this.projectPhasesList = this.activitiesService.GetAllProjectPhases();

    this.getBudgetLineList();
    this.getAllEmployeeList();
    this.getOfficeList();
    this.getAllProvinceList();
    this.getAllProjectActivityList();

    // // Signal-R
    // this._hubConnection = new signalR.HubConnectionBuilder()
    //   .withUrl('http://localhost:5004/chathub')
    //   .configureLogging(signalR.LogLevel.Information)
    //   .build();

    // this._hubConnection.start().catch(err => console.error(err.toString()));

    // this._hubConnection.on('Send', (data: any) => {
    //   const received = `Received: ${data}`;
    //   this.messages.push(received);
    //   console.log(this.messages);
    // });
  }

  // ngOnChanges() {
  //   this.getAllProvinceList();
  // }
  // public sendMessage(): void {
  //   const data = `Sent: ${this.message}`;

  //   if (this._hubConnection) {
  //     this._hubConnection.invoke('Send', data);
  //   }
  //   this.messages.push(data);
  // }

  //#region "initializeForm"
  initializeForm() {
    this.projectActivityFilter = {
      FilterValue: ''
    };

    this.activitySummary = {
      ProjectDuration: 0,
      ActivityOnSchedule: 0,
      LateStart: 0,
      LateEnd: 0,
      Progress: 0,
      Slippage: 0
    };

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
      ParentId: null,

      ActualStartDate: null,
      ActualEndDate: null,

      // // Implementation
      // ImplementationProgress: null,
      // ImplementationStatus: null,
      // ImplementationMethod: null,
      // ImplementationChalanges: null,
      // OvercomingChallanges: null,
      // ExtensionStartDate: null,
      // ExtensionEndDate: null,

      // // Monitoring
      // MonitoringProgress: null,
      // MonitoringStatus: null,
      // MonitoringScore: null,
      // MonitoringFrequency: null,
      // VerificationSource: null,
      // Strengths: null,
      // Weeknesses: null,
      // MonitoringChallenges: null,
      // Recommendation: null,
      // Comments: null,

      // // Other Properties
      // Progress: null,
      // Slippage: null,

      IsLoading: false,
      IsError: false
    };
  }
  //#endregion

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

  //#region "openAddActivityDialog"
  openAddActivityDialog(): void {
    // NOTE: It passed the data into the Add Activity Model
    const dialogRef = this.dialog.open(ProjectActivityAddComponent, {
      width: '550px',
      autoFocus: false,
      data: {
        BudgetLineList: this.budgetLineList,
        OfficeList: this.officeList,
        EmployeeList: this.employeeList,
        RecurringTypeList: this.recurringTypeList,
        ProvinceSelectionList: this.provinceSelectionList,
        DistrictMultiSelectList: this.districtMultiSelectList
      }
    });

    dialogRef.componentInstance.onListRefresh.subscribe(() => {
      // do something
      this.getAllProjectActivityList();
      this.getAllProjectActivityStatus();
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "getOfficeList"
  getOfficeList() {
    this.activitiesService.GetOfficeList().subscribe(
      (response: IResponseData) => {
        this.officeList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.officeList.push({
              OfficeId: element.OfficeId,
              OfficeName: element.OfficeName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getBudgetLineList"
  getBudgetLineList() {
    this.activitiesService.GetBudgetLineList(this.projectId).subscribe(
      (response: IResponseData) => {
        this.budgetLineList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach((element: IBudgetLine) => {
            this.budgetLineList.push({
              BudgetCode: element.BudgetCode,
              BudgetLineId: element.BudgetLineId,
              BudgetName: element.BudgetName,
              CurrencyId: element.CurrencyId,
              CurrencyName: element.CurrencyName,
              InitialBudget: element.InitialBudget,
              ProjectId: element.ProjectId,
              ProjectJobCode: element.ProjectJobCode,
              ProjectJobId: element.ProjectJobId,
              ProjectJobName: element.ProjectJobName
            });

            // this.budgetLineList = this.budgetLineList.filter(x => x.ProjectId === this.projectId);
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getAllEmployeeList"
  getAllEmployeeList() {
    this.activitiesService.GetAllEmployeeList().subscribe(
      (response: IResponseData) => {
        this.employeeList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach((element: IEmployeeList) => {
            this.employeeList.push({
              EmployeeId: element.EmployeeId,
              EmployeeName: element.EmployeeName,
              EmployeeCode: element.EmployeeCode,
              CodeEmployeeName: element.CodeEmployeeName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getAllProjectActivityList"
  getAllProjectActivityList() {

    const filterData: any = {
      ProjectId: this.projectId,
      ActivityDescription: this.projectActivityFilter.FilterValue
    };
    this.activityListLoader = true;
    this.activitiesService.GetProjectActivityAdvanceFilterList(filterData).subscribe(
      (response: IResponseData) => {
        this.projectActivityList = [];
        if (response.statusCode === 200 && response.data !== null) {
          this.totalCount = response.total;
          this.setActivityList(response.data);
          this.onSelectedProvinceDetailId(response.data.ProvinceId);
        }
        this.activityListLoader = false;
      },
      error => {
        this.activityListLoader = false;
      });
  }
  //#endregion

  //#region "getAllProjectActivityStatus"
  getAllProjectActivityStatus() {
    this.activitySummaryLoader = true;
    this.activitiesService
      .GetAllProjectActivityStatus(this.projectId)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data != null) {
            this.activitySummary = {
              ProjectDuration: response.data.ProjectDuration,
              ActivityOnSchedule: response.data.ActivityOnSchedule,
              LateStart: response.data.LateStart,
              LateEnd: response.data.LateEnd,
              Progress:
                response.data.Progress !== 'NaN' ? response.data.Progress : 0,
              Slippage: response.data.Slippage
            };
          }
          this.activitySummaryLoader = false;
        },
        error => {
          this.activitySummaryLoader = false;
        }
      );
  }
  //#endregion

  //#region "getAllProvinceList"
  getAllProvinceList() {
    // this.provinceDistrictFlag = true;
    this.activitiesService.getAllProvinceList().subscribe(
      (response: IResponseData) => {
        this.provinceSelectionList = [];
        if (response.statusCode === 200 && response.data != null) {
          response.data.forEach(element => {
            this.provinceSelectionList.push({
              value: element.ProvinceId,
              label: element.ProvinceName
            });
          });
        }
        // this.provinceDistrictFlag = false;
      },
      error => {
        // this.provinceDistrictFlag = false;
      }
    );
  }

  //#endregion

  //#region "onSelectedProvinceDetailId"
  onSelectedProvinceDetailId(event: any) {
    if (event !== undefined) {
      this.GetAllDistrictvalueByProvinceId(event);
    }
  }
  //#endregion

  //#region "GetAllDistrictvalueByProvinceId"
  // to get the list of District on select of province id
  GetAllDistrictvalueByProvinceId(model: any) {
    this.districtLoaderFlag = true;
    this.districtMultiSelectList = [];
    const id = model;
    // this.provinceSelectedFlag = true;
    this.activitiesService.GetAllDistrictvalueByProvinceId(id).subscribe(
      (res: IResponseData) => {
        if (res.statusCode === 200 && res.data !== null) {
          res.data.forEach(element => {
            this.districtMultiSelectList.push({
              value: element.DistrictID,
              label: element.District
            });
          });
          this.districtLoaderFlag = false;
          // this.GetDistrictByProjectId(this.ProjectId);
        }
        // this.provinceSelectedFlag = false;
      },
      error => {
        this.districtLoaderFlag = false;
        // this.provinceSelectedFlag = false;
      }
    );
  }

  //#endregion

  //#region "deleteProjectActivity"
  deleteProjectActivity(activityId: number) {

    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });

    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText = Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText = Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => {});
    dialogRef.componentInstance.confirmDelete.subscribe(
      res => {
        dialogRef.componentInstance.isLoading = true;


    const index = this.projectActivityList.findIndex(
      x => x.ActivityId === activityId
    );
    if (index !== -1) {
      this.projectActivityList.map(x => {
        if (x.ActivityId === activityId) {
          x.IsLoading = true;
          x.IsError = false;
        }
      });

      this.deleteActivitySubscribe = this.activitiesService
        .DeleteProjectActivity(activityId)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200) {
              this.totalCount = this.totalCount - 1;
              this.GetRefreshedActivityList(index);
              this.projectActivityList.splice(index, 1);
            } else {
              this.projectActivityList.map(x => {
                if (x.ActivityId === activityId) {
                  x.IsLoading = false;
                  x.IsError = true;
                }
              });
            }
            dialogRef.componentInstance.isLoading = false;
            dialogRef.componentInstance.onCancelPopup();
          },
          error => {
            this.projectActivityList.map(x => {
              if (x.ActivityId === activityId) {
                x.IsLoading = false;
                x.IsError = true;
                dialogRef.componentInstance.isLoading = false;
                dialogRef.componentInstance.onCancelPopup();
              }
            });
          }
        );
    }
  });
}
  //#endregion

  //#region GetRefreshedActivityList
  GetRefreshedActivityList(index) {
// Note: If list is empty display activity listing page
    if (this.totalCount === 0) {
      this.showProjectActivityDetail = false;
      this.colsm6 = 'col-sm-10 col-sm-offset-1';
    } else if (this.totalCount === index) {
      // Note: if delete last item of list
      const activityId = this.projectActivityList[0].ActivityId;
      this.getAllProjectActivityDetailByActivityId(activityId);
    } else {
     // Note : Display next item on deletion of any item
    const activityId = this.projectActivityList[index + 1].ActivityId;
    this.getAllProjectActivityDetailByActivityId(activityId);
    }
  }
  //#endregion

  //#region "projectActivityListUpdate"
  projectActivityListUpdate(data: IProjectActivityDetail[]) {
    this.setActivityList(data);
  }
  //#endregion

  setActivityList(data: IProjectActivityDetail[]) {
    this.projectActivityList = [];
    data.forEach((element: IProjectActivityDetail) => {
      this.projectActivityList.push({
        // Planning
        ActivityId: element.ActivityId,
        ActivityName: element.ActivityName,
        ActivityDescription: element.ActivityDescription,
        PlannedStartDate: StaticUtilities.setLocalDate(element.PlannedStartDate),
        PlannedEndDate: StaticUtilities.setLocalDate(element.PlannedEndDate),
        BudgetLineId: element.BudgetLineId,
        EmployeeID: element.EmployeeID,
        OfficeId: element.OfficeId,
        StatusId: element.StatusId,
        Recurring: element.Recurring,
        RecurringCount: element.RecurringCount,
        RecurrinTypeId: element.RecurrinTypeId,
        DistrictID: element.DistrictID,
        ProvinceId: element.ProvinceId,

        ActualStartDate: StaticUtilities.setLocalDate(element.ActualStartDate),
        ActualEndDate: StaticUtilities.setLocalDate(element.ActualEndDate),

        // Implementation
        // ImplementationProgress: element.ImplementationProgress,
        // ImplementationStatus: element.ImplementationStatus,
        // ImplementationMethod: element.ImplementationMethod,
        // ImplementationChalanges: element.ImplementationChalanges,
        // OvercomingChallanges: element.OvercomingChallanges,
        // ExtensionStartDate: StaticUtilities.setLocalDate(element.ExtensionStartDate),
        // ExtensionEndDate: StaticUtilities.setLocalDate(element.ExtensionEndDate),

        // // Monitoring
        // MonitoringProgress: element.MonitoringProgress,
        // MonitoringStatus: element.MonitoringStatus,
        // MonitoringScore: element.MonitoringScore,
        // MonitoringFrequency: element.MonitoringFrequency,
        // VerificationSource: element.VerificationSource,
        // Strengths: element.Strengths,
        // Weeknesses: element.Weeknesses,
        // MonitoringChallenges: element.MonitoringChallenges,
        // Recommendation: element.Recommendation,
        // Comments: element.Comments,

        // Other Properties
        Progress: element.Progress,
        // this.findProgress(
        //   element.ImplementationProgress,
        //   element.MonitoringProgress
        // ),
        Slippage: element.Slippage,
        // this.findSleepage(
        //   StaticUtilities.setLocalDate(element.PlannedEndDate),
        //   StaticUtilities.setLocalDate(element.ActualEndDate)
        // ),

        IsLoading: false,
        IsError: false
      });
      // console.log(this.projectActivityList);
    });
  }

  //#region "onSearchFilterApplied"
  onSearchFilterApplied() {
    this.getAllProjectActivityList();
  }
  //#endregion

  //#region "onSearchFilterReset"
  onSearchFilterReset() {
    this.projectActivityFilter.FilterValue = '';
    this.getAllProjectActivityList();
  }
  //#endregion

  //#region "onDeleteActivityClick"
  onDeleteActivityClick(item: number) {
    this.deleteProjectActivity(item);
  }
  //#endregion

  // #region "onProjectActivityClick"
  // onProjectActivityClick(item: IProjectActivityDetail) {
  //   this.selectedProjectActivityId = item.ActivityId;
  //   this.activityDetail = item;
  //   this.showProjectDetailPanel();
  //  this.getAllProjectActivityDetailList(item.ActivityId);
  // }
  // #endregion

  // #region "onProjectActivityClick" pk
  onProjectActivityClick(item: IProjectActivityDetail) {
    this.selectedProjectActivityId = item.ActivityId;
    //  this.activityDetail = item;
    this.showProjectDetailPanel();
    this.getAllProjectActivityDetailByActivityId(item.ActivityId);
  }
  // #endregion

  getAllProjectActivityDetailByActivityId(activityId: number) {
    this.activityByIdLoader = true;
    this.activitiesService
      .GetAllProjectActivityListByActivityId(activityId)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data !== null) {
            this.activityDetail = response.data;
            this.activityDetail.PlannedStartDate =
              this.activityDetail.PlannedStartDate != null
                ? StaticUtilities.setLocalDate(
                    this.activityDetail.PlannedStartDate
                  )
                : null;
            this.activityDetail.PlannedEndDate =
              this.activityDetail.PlannedEndDate != null
                ? StaticUtilities.setLocalDate(
                    this.activityDetail.PlannedEndDate
                  )
                : null;
            this.activityDetail.ActualStartDate =
              this.activityDetail.ActualStartDate != null
                ? StaticUtilities.setLocalDate(
                    this.activityDetail.ActualStartDate
                  )
                : null;
            this.activityDetail.ActualEndDate =
              this.activityDetail.ActualEndDate != null
                ? StaticUtilities.setLocalDate(
                    this.activityDetail.ActualEndDate
                  )
                : null;
            this.onSelectedProvinceDetailId(response.data.ProvinceId);
          }

          this.activityByIdLoader = false;
        },
        error => {
          this.activityByIdLoader = false;
        }
      );
  }

  //#region "findProgress"
  findProgress(ImplementationProgress: number, MonitoringProgress: number) {
    if (ImplementationProgress === undefined) {
      ImplementationProgress = 0;
    }
    if (MonitoringProgress === undefined) {
      MonitoringProgress = 0;
    }
    const progress = (ImplementationProgress + MonitoringProgress) / 2;
    return progress;
  }
  //#endregion

  //#region "findSleepage"
  findSleepage(planningEndDate: any, ImplementationEndDate: any) {
    if (planningEndDate === undefined || planningEndDate == null) {
      planningEndDate = new Date();
    } else {
      planningEndDate = this.getLocalDate(planningEndDate);
    }
    if (ImplementationEndDate === undefined || ImplementationEndDate == null) {
      ImplementationEndDate = new Date();
    } else {
      ImplementationEndDate = this.getLocalDate(ImplementationEndDate);
    }
    // let sleepage = Math.round(
    //   (new Date(ImplementationEndDate).valueOf() -
    //     new Date(planningEndDate).valueOf()) /
    //     (1000 * 3600 * 24)
    // );
    const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
    // let sleepage = Math.round(Math.abs((ImplementationEndDate.getTime() - planningEndDate.getTime()) / (oneDay)));
    let sleepage = Math.round(
      (ImplementationEndDate.getTime() - planningEndDate.getTime()) / oneDay
    );

    if (sleepage <= 0) {
      sleepage = 0;
      // return sleepage;
    }

    return sleepage;
  }
  //#endregion

  //#region "setLocalDate"
  setLocalDate(date: any) {
    if (date === null || date === undefined) {
      return null;
    } else {
      return new Date(
        new Date(date).getTime() - new Date().getTimezoneOffset() * 60000
      );
    }
  }
  //#endregion

  //#region "getLocalDate"
  getLocalDate(date: any) {
    return new Date(
      new Date(date).getFullYear(),
      new Date(date).getMonth(),
      new Date(date).getDate(),
      new Date(date).getHours(),
      new Date(date).getMinutes(),
      new Date(date).getSeconds(),
      new Date(date).getMilliseconds()
    );
  }
  //#endregion

  //#region "showProjectDetailPanel"
  showProjectDetailPanel() {
    this.showProjectActivityDetail = true;
    this.colsm6 = this.showProjectActivityDetail
      ? 'col-sm-6'
      : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "onAddActivityClicked"
  onAddActivityClicked() {
    this.openAddActivityDialog();
  }
  //#endregion

  //#region "updateActivity"
  updateActivity(data: IProjectActivityDetail) {
    const activityDetailIndex = this.projectActivityList.findIndex(
      x => x.ActivityId === data.ActivityId
    );
    if (activityDetailIndex !== -1) {
      // Other Properties
      data.Progress = 0;
      // this.findProgress(
      //   data.ImplementationProgress,
      //   data.MonitoringProgress
      // );
      data.Slippage = 0;
      // this.findSleepage(
      //   data.PlannedEndDate,
      //   data.ActualEndDate
      // );
      this.projectActivityList[activityDetailIndex] = data;
    }
  }
  //#endregion

  //#region "refreshProjectSummary"
  refreshProjectSummary() {
    this.getAllProjectActivityStatus();
  }
  //#endregion

  onUpdateActivityStatusId(data: IProjectActivityDetail) {
    // Note: when we update the status on the bases of Start button click of sub activity
    if (data.ParentId != null && data.StatusId === 2) {
      const activityDetailIndex = this.projectActivityList.findIndex(
        x => x.ActivityId === data.ParentId
      );
      if (activityDetailIndex !== -1) {
        // Status Id update
        this.projectActivityList[activityDetailIndex].StatusId = data.StatusId;
        this.getAllProjectActivityStatus();
      }
    } else if (data.ParentId === undefined && data.StatusId === 3) {
      // Note: when the Complete button click
      const activityDetailIndex = this.projectActivityList.findIndex(
        x => x.ActivityId === data.ActivityId
      );
      if (activityDetailIndex !== -1) {
        // Status Id update
        this.projectActivityList[activityDetailIndex].StatusId = data.StatusId;
        this.getAllProjectActivityStatus();
      }
    }
  }

  onParentListRefrsh(data: any) {
    if (data != null) {
      this.getAllProjectActivityList();
    }
  }
  //   //#region "list refresh when new subactivity is added"
  //   OnactivityStatusListRefresh(data:IProjectActivityDetail){
  //     if (data.ParentId != null) {
  //       const activityDetailIndex = this.projectActivityList.findIndex(
  //         x => x.ActivityId === data.ParentId
  //       );
  //       if (activityDetailIndex !== -1) {
  //         // Status Id update
  //         this.projectActivityList[activityDetailIndex].StatusId = 2;
  //         // this.getAllProjectActivityStatus();
  //       }
  //   }
  // }
  //   //#endregion

  //#region "ngOnDestroy"
  ngOnDestroy() {
    if (this.deleteActivitySubscribe && !this.deleteActivitySubscribe.closed) {
      this.deleteActivitySubscribe.unsubscribe();
    }
  }
  //#endregion
}
