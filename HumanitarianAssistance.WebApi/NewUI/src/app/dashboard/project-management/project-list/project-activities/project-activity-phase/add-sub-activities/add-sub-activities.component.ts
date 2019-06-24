import { Component, OnInit, Inject, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  IEmployeeList,
  IAddProjectSubActivityDataSources,
  IAddProjectSubActivityModel
} from '../../models/project-activities.model';
import { ToastrService } from 'ngx-toastr';
import { ProjectActivitiesService } from '../../service/project-activities.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-add-sub-activities',
  templateUrl: './add-sub-activities.component.html',
  styleUrls: ['./add-sub-activities.component.scss']
})
export class AddSubActivitiesComponent implements OnInit {
  projectSubActivityForm: FormGroup;
  employeeList: IEmployeeList[] = [];
  budgetLineId: number;
  activityId: number;
  addSubActivityLoaderFlag = false;
  addProjectSubActivitySubscribe: Subscription;
  onSubactivityListRefresh = new EventEmitter<any>();

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddSubActivitiesComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IAddProjectSubActivityDataSources,
    public toastr: ToastrService,
    public activitiesService: ProjectActivitiesService
  ) {
    this.activityId = data.ActivityId;
    this.budgetLineId = data.BudgetLineId;
    this.employeeList = data.EmployeeList;
  }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.projectSubActivityForm = this.fb.group({
      ActivityDescription: ['', Validators.required],
      PlannedStartDate: [null, [Validators.required]],
      PlannedEndDate: [null, Validators.required],
      EmployeeID: [null, Validators.required],
      Target: [0, Validators.required],
      SubActivityTitle: [null, Validators.required]
    });
  }

  onFormSubmit(data: any) {
    this.addProjectSubActivity(data);
  }

  //#region "addProjectSubActivity"
  addProjectSubActivity(data: IAddProjectSubActivityModel) {
    if (this.projectSubActivityForm.valid) {
      this.addSubActivityLoaderFlag = true;

      const activityData: IAddProjectSubActivityModel = {
        ActivityDescription: data.ActivityDescription,
        PlannedStartDate: StaticUtilities.getLocalDate(data.PlannedStartDate),
        PlannedEndDate: StaticUtilities.getLocalDate(data.PlannedEndDate),
        EmployeeID: data.EmployeeID,
        Target: data.Target,
        BudgetLineId: this.budgetLineId,
        ParentId: this.activityId,
        SubActivityTitle: data.SubActivityTitle
      };

      if (activityData.PlannedEndDate >= activityData.PlannedStartDate) {
        this.addProjectSubActivitySubscribe = this.activitiesService
          .AddProjectSubActivity(activityData)
          .subscribe(
            (response: IResponseData) => {
              if (response.statusCode === 200 && response.data != null) {
                this.onSubactivityListRefresh.emit(response.data);
                this.onCancelPopup();
              } else {
                this.toastr.error(response.message);
              }
              this.addSubActivityLoaderFlag = false;
            },
            error => {
              this.toastr.error('Someting went wrong');
              this.addSubActivityLoaderFlag = false;
            }
          );
      }
    }
  }
  //#endregion

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  get plannedStartDate() {
    return this.projectSubActivityForm.get('PlannedStartDate').value;
  }
}
