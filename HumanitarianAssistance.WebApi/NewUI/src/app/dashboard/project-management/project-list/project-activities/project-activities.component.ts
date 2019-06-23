import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { ProjectActivityListingComponent } from './project-activity-listing/project-activity-listing.component';

@Component({
  selector: 'app-project-activities',
  templateUrl: './project-activities.component.html',
  styleUrls: ['./project-activities.component.scss']
})
export class ProjectActivitiesComponent implements OnInit {

  @ViewChild(ProjectActivityListingComponent) activityChild: ProjectActivityListingComponent;

  constructor() {}

  ngOnInit() {
  }

  refreshBudgetLine() {
    this.activityChild.getBudgetLineList();
  }

}
