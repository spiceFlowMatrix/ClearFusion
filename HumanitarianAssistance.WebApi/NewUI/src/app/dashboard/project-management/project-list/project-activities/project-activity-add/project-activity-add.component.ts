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
  countryList: any[] = [];
  projectActivityForm: FormGroup;
  addActivityLoader = false;
  projectId: number;
  diasbleEndDate = false;
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
    this.countryList = data.CountryList;
    this.projectId = data.ProjectId;
    this.initForm();
  }

  ngOnInit() {}

  //#region "initForm"
  initForm() {
    this.projectActivityForm = this.fb.group({
      ActivityDescription: [null, Validators.required],
      PlannedStartDate: [new Date(), [Validators.required]],
      PlannedEndDate: [new Date(), Validators.required],
      BudgetLineId: [null],
      EmployeeID: [null, Validators.required],
      OfficeId: [null],
      Recurring: [false, Validators.required],
      RecurringCount: [null],
      RecurrinTypeId: [1],
      ProvinceId: [null, Validators.required],
      DistrictID: [null],
      CountryId: [null, Validators.required]
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

  onCountryDetailsChange(event: any) {
    if (event != null && event !== undefined) {
      this.getAllProvinceListByCountryId(event);
    }
  }

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

  getAllProvinceListByCountryId(id: any) {
    const provinceId = id.value;
    // this.provinceDistrictFlag = true;
    if (provinceId != null && provinceId !== undefined) {
      this.provinceSelectionList = [];
      this.activitiesService
        .getAllProvinceListByCountryId([provinceId])
        .subscribe(
          response => {
            if (response.statusCode === 200 && response.data != null) {
              response.data.forEach(element => {
                this.provinceSelectionList.push({
                  value: element.ProvinceId,
                  label: element.ProvinceName
                });
              });
              // this.GetProvinceByProjectId(this.ProjectId);
            }
            // this.provinceDistrictFlag = false;
          },
          error => {
            // this.provinceDistrictFlag = false;
          }
        );
    }
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
        DistrictID: data.DistrictID,
        CountryId: data.CountryId,
        ProjectId: this.projectId
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

  onRecurringChange(event: any) {
    console.log(event);
    if (event.checked === true) {
      this.diasbleEndDate = true;
    } else {
      this.diasbleEndDate = false;
    }
  }
}
