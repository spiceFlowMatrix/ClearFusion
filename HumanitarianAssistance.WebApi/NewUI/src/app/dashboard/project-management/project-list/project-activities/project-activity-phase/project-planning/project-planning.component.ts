import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges,
  OnDestroy
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProjectActivityDocumentsComponent } from '../../project-activity-documents/project-activity-documents.component';
import {
  IDocumentsModel,
  IProjectActivityDocumentModel,
  IProjectActivityDetail,
  IBudgetLine,
  IActivityExtensionMode,
  IEmployeeList,
  IOfficeList
} from '../../models/project-activities.model';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ProjectActivityStatus } from 'src/app/shared/enum';
import { AddExtensionsComponent } from '../add-extensions/add-extensions.component';
import { ProjectActivitiesService } from '../../service/project-activities.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { DatePipe } from '@angular/common';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { IProjectRoles, IProjectPeople } from '../../../project-details/models/project-people.model';
import { ProjectListService } from '../../../service/project-list.service';

@Component({
  selector: 'app-project-planning',
  templateUrl: './project-planning.component.html',
  styleUrls: ['./project-planning.component.scss']
})
export class ProjectPlanningComponent implements OnInit, OnChanges, OnDestroy {
  //#region "variables"
  @Input() activityDetail: IProjectActivityDetail;
  @Input() planningDocumentsList: IDocumentsModel[] = [];
  @Output() selectedProvinceId = new EventEmitter<any>();
  @Output() selectedDistrictId = new EventEmitter<any>();

  @Input() recurringTypeList: any[] = [];
  @Input() budgetLineList: IBudgetLine[] = [];
  @Input() employeeList: IEmployeeList[] = [];
  @Input() officeList: IOfficeList[] = [];
  @Input() provinceSelectionList: any[] = [];
  @Input() districtMultiSelectList: any[] = [];
  @Input() districtLoaderFlag: boolean;

  extensionList: IActivityExtensionMode[] = [];

  projectActivityForm: FormGroup;
  activityListLoader = false;

  // lib datasource
  tableHeaderList: string[] = [
    'ExtensionId',
    'StartDate',
    'EndDate',
    'Description'
  ];

  // permission
  actualProjectPermissions: IProjectRoles[] = [];
  projectPermissions: IProjectPeople[] = [];
  addUploadDocumentPermission = false;
  addExtensionPermission = false;

  // Subscription
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  //#endregion

  constructor(
    public dialog: MatDialog,
    private fb: FormBuilder,
    public toastr: ToastrService,
    private activitiesService: ProjectActivitiesService,
    private projectListService: ProjectListService,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    this.getActivityPermission();
  }

  ngOnChanges(changes: SimpleChanges) {
    this.initForm();
    if (
      this.activityDetail.ActivityId !== 0 &&
      this.activityDetail.ActivityId != null &&
      this.activityDetail.ActivityId !== undefined
    ) {
      this.onChanges();
      this.getAllExtensionList();
    }
  }

  //#region "initForm"
  initForm() {
    // const activitylist = this.activityDetail;
    this.projectActivityForm = this.fb.group({
      Description: [this.activityDetail.ActivityDescription],
      ActivityStartDate: [this.activityDetail.PlannedStartDate],
      ActivityEndDate: [this.activityDetail.PlannedEndDate],
      ActualStartDate: [this.activityDetail.ActualStartDate],
      ActualEndDate: [this.activityDetail.ActualEndDate],
      BudgetLine: [this.activityDetail.BudgetLineId],
      Assignee: [this.activityDetail.EmployeeID],
      Location: [this.activityDetail.OfficeId],
      Recurring: [this.activityDetail.Recurring],
      RecurringCount: [this.activityDetail.RecurringCount],
      RecurringType: [this.activityDetail.RecurrinTypeId],
      ProvinceId: [this.activityDetail.ProvinceId],
      DistrictID: [this.activityDetail.DistrictID],
    });

  }
  //#endregion

  //#region "onChanges"
  onChanges(): void {
    this.projectActivityForm.valueChanges.subscribe(val => {
      if (this.projectActivityForm.valid) {
        this.activityDetail.ActivityDescription = val.Description;
        this.activityDetail.PlannedStartDate = val.ActivityStartDate;
        this.activityDetail.PlannedEndDate = val.ActivityEndDate;
        this.activityDetail.BudgetLineId = val.BudgetLine;
        this.activityDetail.EmployeeID = val.Assignee;
        this.activityDetail.OfficeId = val.Location;
        this.activityDetail.Recurring = val.Recurring;
        this.activityDetail.RecurringCount = val.RecurringCount;
        this.activityDetail.RecurrinTypeId = val.RecurringType;
        this.activityDetail.ProvinceId = val.ProvinceId;
        this.activityDetail.DistrictID = val.DistrictID;
        this.activityDetail.ActualStartDate = val.ActualStartDate;
        this.activityDetail.ActualEndDate = val.ActualEndDate;
      }
    });
  }
  //#endregion

  //#region "openDocumentsDialog"
  openDocumentsDialog(): void {
    // NOTE: It passed the data into the Document Model

    const modelData: IProjectActivityDocumentModel = {
      HeaderText: 'Planning Documents',
      // DocumentModel: this.planningDocumentsList,
      ProjectPhaseId: ProjectActivityStatus.Planning,
      ProjectActivityId: this.activityDetail.ActivityId
    };

    const dialogRef = this.dialog.open(ProjectActivityDocumentsComponent, {
      width: '400px',
      maxHeight: '500px',
      autoFocus: false,
      data: modelData
    });

    // delete
    // dialogRef.componentInstance.deleteDocument.subscribe(
    //   (item: IDocumentsModel) => {
    //     this.deleteDocument(item);
    //   }
    // );

    // dialogRef.componentInstance.documentListRefresh.subscribe(
    //   (item: IDocumentsModel) => {
    //     this.documentListRefresh.emit(item);
    //   }
    // );

    // dialogRef.componentInstance.documentDownload.subscribe(
    //   (item: IDocumentsModel) => {
    //     this.documentDownload.emit(item);
    //   }
    // );

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "Permission"
  getActivityPermission() {
    this.activitiesService.activityPermissionSubject.subscribe(data => {
      this.projectPermissions = data;
      this.actualProjectPermissions = this.projectListService.GetActivitiesControlRole();
      if (this.projectPermissions.length > 0) {
        // check Upload document Permission
        this.addUploadDocumentPermission = this.checkUploadDocumentPermission();
        // check Add Extension Permission
        this.addExtensionPermission = this.checkAddExtensionPermission();
      }
    });
  }

  checkUploadDocumentPermission(): boolean {
    // NOTE: Document can be uploaded by any person "EXCEPT PLANNING OFFICER"
    return this.projectPermissions.filter(x => x.RoleId !== this.actualProjectPermissions[0].Id).length > 0 ?  true : false;
  }

  checkAddExtensionPermission(): boolean {
    // NOTE: Only "PLANNING OFFICER" will Add Extension
    return this.projectPermissions.filter(x => x.RoleId === this.actualProjectPermissions[0].Id).length > 0 ?  true : false;
  }
  //#endregion

  onExtensionClicked() {
    this.openAddExtensionDialog();
  }

  openAddExtensionDialog() {
    const dialogRef = this.dialog.open(AddExtensionsComponent, {
      width: '550px',
      autoFocus: false,
      data: { ActivityId: this.activityDetail.ActivityId }
    });

    dialogRef.componentInstance.onListRefresh.subscribe(x => {
      this.getAllExtensionList();
    });
  }

  //#region "getAllExtensionList"
  getAllExtensionList() {
    this.extensionList = [];
    this.activitiesService
      .GetAllExtensionList(this.activityDetail.ActivityId)
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

              // this.extensionList.map(x => {
              //   if (x.StartDate != null) {
              //     x.StartDate = ;
              //   }
              //   if (x.EndDate != null) {
              //     x.EndDate = this.datePipe.transform( x.EndDate, 'dd-MM-yyyy');
              //   }
              // });
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

  //#region  "emit province detailchanges"
  onProvinceDetailChange(event: any) {
    this.selectedProvinceId.emit(event.value);
  }
  //#endregion



  //#region "onDistrictDetailsChange"
  onDistrictDetailsChange(event: any) {
    this.selectedDistrictId.emit(event);
  }
  //#endregion

  get activityStartDate() {
    return this.projectActivityForm.get('ActivityStartDate').value;
  }
  get recurringFlag() {
    return this.projectActivityForm.get('Recurring').value;
  }

  get activityActualStartdate(){
    return this.projectActivityForm.get('ActualStartDate').value;
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
