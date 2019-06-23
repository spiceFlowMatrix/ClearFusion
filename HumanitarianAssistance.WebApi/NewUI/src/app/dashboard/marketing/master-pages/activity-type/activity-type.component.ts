import { Component, OnInit, ViewChild } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import {MasterPageServiceService} from '../service/master-page-service.service';
import { ActivityTypeModel } from '../model/mastrer-pages.model';
import { ActivitytypeDetailsComponent } from './activitytype-details/activitytype-details.component';
import { ApplicationPages } from '../../../../shared/applicationpagesenum';
import { LocalStorageService } from '../../../../shared/services/localstorage.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
@Component({
  selector: 'app-activity-type',
  templateUrl: './activity-type.component.html',
  styleUrls: ['./activity-type.component.scss']
})
export class ActivityTypeComponent implements OnInit {
  activityType;
  activityTypeId;
  isEditingAllowed = false;
  pageId = ApplicationPages.ActivityType;
  constructor( private appurl: AppUrlService, private masterService: MasterPageServiceService,
    private localStorageService: LocalStorageService) { }
  showActivityDetail = false;
  activityTypeList: ActivityTypeModel[];
  model: ActivityTypeModel = {};
  colsm6 = 'col-sm-10 col-sm-offset-1';
  public selectedRowID;
  activityTypeListLoaderFlag = false;
  @ViewChild(ActivitytypeDetailsComponent) child: ActivitytypeDetailsComponent;

  ngOnInit() {
    this.init();
    this.getActivityType();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }

  init() {
    // this.jobsList = [];
  }

  getActivityType() {
    this.activityTypeListLoaderFlag = true;
    this.activityTypeList = [];
    this.masterService.GetActivityTypeList().subscribe((data:IResponseData) => {
      if (data.statusCode === 200) {
        this.activityTypeList =  data.data;
      } else {
      }
      this.activityTypeListLoaderFlag = false;
    }, error => {
      this.activityTypeListLoaderFlag = false;
    }
    );
  }

  onItemClick(id: number) {
    if (this.isEditingAllowed) {
      this.activityTypeId = id;
      if (this.activityTypeId === 0 || this.activityTypeId === undefined || this.activityTypeId === null) {
        this.child.ResetFormOnAddNewActivity();
      }
      this.selectedRowID = id;
      this.showProjectDetailPanel();
    }
  }

  //#region "show/hide"
  showProjectDetailPanel() {
    this.showActivityDetail = true;
    this.colsm6 = this.showActivityDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideProjectDetailPanel() {
    this.showActivityDetail = false;
    this.colsm6 = this.showActivityDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(event) {
    this.hideProjectDetailPanel();
  }
  //#endregion

  onActivityDeleted(id) {
    const index = this.activityTypeList.findIndex(r => r.ActivityTypeId === id.id);
    this.activityTypeList.splice(index, 1);
    this.child.ResetFormOnAddNewActivity();
    this.hideProjectDetailPanel();
  }

  addActivityList(e) {
    this.activityTypeList.push(e);
  }

  updateActivityList(e) {
    const index = this.activityTypeList.findIndex(r => r.ActivityTypeId === e.ActivityTypeId);
    if (index !== -1) {
      this.activityTypeList[index] = e;
    }
  }

}
