import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IProjectAdvanceFilterModel } from '../models/project-activities.model';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ProjectActivitiesService } from '../service/project-activities.service';

@Component({
  selector: 'app-project-activity-filtering',
  templateUrl: './project-activity-filtering.component.html',
  styleUrls: ['./project-activity-filtering.component.scss']
})
export class ProjectActivityFilteringComponent implements OnInit {

  @Input() projectId: number;
  @Input() budgetLineList: any[] = [];
  @Input() employeeList: any[] = [];

  @Output() projectActivityListUpdate = new EventEmitter<any>();

  advanceFilter: IProjectAdvanceFilterModel;
  filterLoaderFlag = false;

  options: any = {
    floor: 0,
    ceil: 100,
    draggableRange: true
  };

  constructor(private activitiesService: ProjectActivitiesService) { }

  ngOnInit() {
    this.initFilterForm();
  }

  initFilterForm() {
    this.advanceFilter = {
      ProjectId: this.projectId,
      PlannedStartDate: null,
      PlannedEndDate: null,
      ActualStartDate: null,
      ActualEndDate: null,
      BudgetLineId: [],
      AssigneeId: [],

      // status
      Planning: false,
      Implementation: false,
      Completed: false,

      // range
      ProgressRange: [0, 0],
      SleepageMin: null,
      SleepageMax: null,
      DurationMin: null,
      DurationMax: null,

      LateStart: false,
      LateEnd: false,
      OnSchedule: false,
    };
  }

  onAdvanceFilterClicked () {
    this.getAdvancefilterData();
  }

  onAdvanceResetClicked() {
    this.initFilterForm();
  }

  //#region "getAdvancefilterData"
  getAdvancefilterData() {
    this.filterLoaderFlag = true;

    const filterData: any = {
      ProjectId: this.advanceFilter.ProjectId,
      PlannedStartDate: this.advanceFilter.PlannedStartDate,
      PlannedEndDate: this.advanceFilter.PlannedEndDate,
      ActualStartDate: this.advanceFilter.ActualStartDate,
      ActualEndDate: this.advanceFilter.ActualEndDate,
      BudgetLineId: this.advanceFilter.BudgetLineId,
      AssigneeId: this.advanceFilter.AssigneeId,

      // status
      Planning: this.advanceFilter.Planning,
      Implementation: this.advanceFilter.Implementation,
      Completed: this.advanceFilter.Completed,

      // range
      ProgressRangeMin: this.advanceFilter.ProgressRange[0],
      ProgressRangeMax: this.advanceFilter.ProgressRange[1],

      SleepageMin: this.advanceFilter.SleepageMin,
      SleepageMax: this.advanceFilter.SleepageMax,
      DurationMin: this.advanceFilter.DurationMin,
      DurationMax: this.advanceFilter.DurationMax,

      LateStart: this.advanceFilter.LateStart,
      LateEnd: this.advanceFilter.LateEnd,
      OnSchedule: this.advanceFilter.OnSchedule,
    };

    this.activitiesService.GetProjectActivityAdvanceFilterList(filterData).subscribe(
      (response: IResponseData) => {

        if (response.statusCode === 200) {
          this.projectActivityListUpdate.emit(response.data);
        }

        this.filterLoaderFlag = false;

      },
      error => {
        this.filterLoaderFlag = false;
      }
    );
  }
  //#endregion

}
