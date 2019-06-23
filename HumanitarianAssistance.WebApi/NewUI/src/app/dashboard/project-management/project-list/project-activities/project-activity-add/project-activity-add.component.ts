import {
  Component,
  OnInit,
  EventEmitter,
  Inject,
  OnDestroy,
  Output
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import {
  IAddProjectActivityDataSources,
  IEmployeeList,
  IOfficeList,
  IPlanningActivityDetail,
  IBudgetLine
} from '../models/project-activities.model';
import { ProjectActivitiesService } from '../service/project-activities.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ProjectActivityStatus } from 'src/app/shared/enum';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-project-activity-add',
  templateUrl: './project-activity-add.component.html',
  styleUrls: ['./project-activity-add.component.scss']
})
export class ProjectActivityAddComponent implements OnInit, OnDestroy {
  @Output() selectAddProvinceID = new EventEmitter<any>();
  //#region "variables"
  onListRefresh = new EventEmitter();
  onDistrictListRefresh = new EventEmitter();

  budgetLineList: IBudgetLine[] = [];
  employeeList: IEmployeeList[] = [];
  officeList: IOfficeList[] = [];
  recurringTypeList: any[] = [];
  districtSelectionList: any[] = [];
  districtSelectedList: any[] = [];
  provinceSelectionList: any[] = [];

  projectActivityForm: FormGroup;
  addActivityLoader = false;

  // subscribe
  addProjectActivitySubscribe: Subscription;

  //#endregion

  constructor(
    public dialogRef: MatDialogRef<ProjectActivityAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IAddProjectActivityDataSources,
    private fb: FormBuilder,
    public toastr: ToastrService,
    public activitiesService: ProjectActivitiesService
  ) {
    this.budgetLineList = data.BudgetLineList;
    this.employeeList = data.EmployeeList;
    this.officeList = data.OfficeList;
    this.recurringTypeList = data.RecurringTypeList;
    this.provinceSelectionList = data.ProvinceSelectionList;
    this.districtSelectionList = this.districtSelectionList;
    this.initForm();
  }

  ngOnInit() {}

  //#region "initForm"
  initForm() {
    this.projectActivityForm = this.fb.group({
      ActivityDescription: [null, Validators.required],
      PlannedStartDate: [null, [Validators.required]],
      PlannedEndDate: [null, Validators.required],
      BudgetLineId: [null, Validators.required],
      EmployeeID: [null, Validators.required],
      OfficeId: [null],
      Recurring: [false, Validators.required],
      RecurringCount: [null],
      RecurrinTypeId: [1],
      ProvinceId: [null, Validators.required],
      DistrictID: [null]
    });
  }
  //#endregion

  //#region "projectActivityListRefresh"
  projectActivityListRefresh() {
    this.onListRefresh.emit();
  }
  //#endregion

  //#region "onFormSubmit"
  onFormSubmit(data: any) {
    this.addProjectActivity(data);
  }
  //#endregion

  //#region "onProvinceDetailChange  for GetAllDistrictvalueByProvinceId"
  onProvinceDetailChange(event: any) {
    // this.selectAddProvinceID.emit(event.value);
    this.GetAllDistrictvalueByProvinceId(event.value);
  }
  //#endregion

  //#region "GetAllDistrictvalueByProvinceId"
  // to get the list of District on select of province id
  GetAllDistrictvalueByProvinceId(model: any) {
    // this.provinceSelectedFlag = true;
    this.districtSelectionList = [];
    const id = model;
    // this.provinceSelectedFlag = true;
    this.activitiesService.GetAllDistrictvalueByProvinceId(id).subscribe(
      (res: IResponseData) => {

        if (res.statusCode === 200 && res.data !== null) {
          res.data.forEach(element => {
            this.districtSelectionList.push({
              value: element.DistrictID,
              label: element.District
            });
          });
          // this.GetDistrictByProjectId(this.ProjectId);
        }
        // this.provinceSelectedFlag = false;
      },
      error => {
        // this.provinceSelectedFlag = false;
      }
    );
  }

  //#endregion

  //#region "addProjectActivity"
  addProjectActivity(data: any) {
    if (this.projectActivityForm.valid) {
      this.addActivityLoader = true;

      const activityData: IPlanningActivityDetail = {
        // Planning
        ActivityId: 0,
        ActivityName: data.ActivityDescription,
        ActivityDescription: data.ActivityDescription,
        PlannedStartDate: this.getLocalDate(data.PlannedStartDate),
        PlannedEndDate: this.getLocalDate(data.PlannedEndDate),
        BudgetLineId: data.BudgetLineId,
        EmployeeID: data.EmployeeID,
        OfficeId: data.OfficeId,
        StatusId: ProjectActivityStatus.Planning,
        Recurring: data.Recurring,
        RecurringCount: data.RecurringCount,
        RecurrinTypeId: data.RecurrinTypeId,
        ProvinceId: data.ProvinceId,
        DistrictID: data.DistrictID
      };

      if (activityData.PlannedEndDate >= activityData.PlannedStartDate) {
        this.addProjectActivitySubscribe = this.activitiesService
          .AddProjectActivity(activityData)
          .subscribe(
            (response: IResponseData) => {
              if (response.statusCode === 200) {
                this.onCancelPopup();
                this.projectActivityListRefresh();
                this.toastr.success('Activity is created successfully');
              } else {
                this.toastr.error(response.message);
              }
              this.addActivityLoader = false;
            },
            error => {
              this.toastr.error('Someting went wrong');
              this.addActivityLoader = false;
            }
          );
      } else {
        this.toastr.warning(
          'Planning End Date of activity must be greater than or equal to Start Date'
        );
        this.addActivityLoader = false;
      }
    }
  }
  //#endregion

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  //#regionngOnDestroy
  ngOnDestroy() {
    if (
      this.addProjectActivitySubscribe &&
      !this.addProjectActivitySubscribe.closed
    ) {
      this.addProjectActivitySubscribe.unsubscribe();
    }
  }
  //#endregion

  get startDate() {
    return this.projectActivityForm.get('PlannedStartDate').value;
  }

  get endDate() {
    return this.projectActivityForm.get('PlannedEndDate').value;
  }

  get recurringFlag() {
    return this.projectActivityForm.get('Recurring').value;
  }

  //#region "getLocalDate"
  getLocalDate(date: any) {
    return new Date(
      new Date(date).getFullYear(),
      new Date(date).getMonth(),
      new Date(date).getDate(),
      new Date(date).getHours(),
      new Date().getMinutes(),
      new Date().getSeconds(),
      new Date().getMilliseconds()
    );
  }
  //#endregion
}
