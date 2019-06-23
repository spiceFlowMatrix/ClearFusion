import { Component, OnInit, ViewChild } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { MasterPageServiceService } from '../service/master-page-service.service';
import { TimeCategoryModel } from '../model/mastrer-pages.model';
import { TimeCategoryDetailsComponent } from './time-category-details/time-category-details.component';
import { LocalStorageService } from '../../../../shared/services/localstorage.service';
import { ApplicationPages } from '../../../../shared/applicationpagesenum';

@Component({
  selector: 'app-time-category',
  templateUrl: './time-category.component.html',
  styleUrls: ['./time-category.component.scss']
})
export class TimeCategoryComponent implements OnInit {
  timCategoryName: string;
  timeCategoryId: number;
  pageId = ApplicationPages.TimeCategory;
  showTimeCategoryDetail = false;
  timeCategoryListLoaderFlag = false;
  isEditingAllowed = false;
  public selectedRowID: number;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  @ViewChild(TimeCategoryDetailsComponent) child: TimeCategoryDetailsComponent;
  constructor(private appurl: AppUrlService, private masterService: MasterPageServiceService,
    private localstorageservice: LocalStorageService) { }

  getTimeCategoryList: TimeCategoryModel[];
  model: TimeCategoryModel = {};

  ngOnInit() {
    this.getTimeCategory();
    this.isEditingAllowed = this.localstorageservice.IsEditingAllowed(this.pageId);
  }

  getTimeCategory() {
    this.getTimeCategoryList = [];
    this.timeCategoryListLoaderFlag = true;
    this.masterService.GetList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetTimeCategory).subscribe(data => {
      if (data.StatusCode === 200) {
        this.getTimeCategoryList = data.data.TimeCategories;
      }
        this.timeCategoryListLoaderFlag = false;
    },
    error => {
      this.timeCategoryListLoaderFlag = false;
    });
  }

  onItemClick(id: number) {
    if (this.isEditingAllowed) {
      this.timeCategoryId = id;
      if (this.timeCategoryId === 0 || this.timeCategoryId === undefined || this.timeCategoryId === null) {
        this.child.ResetFormOnAddNewTimeCategory();
        this.child.CreateTimeCategoryonAddNew();
      }
      this.selectedRowID  = id;
      this.showProjectDetailPanel();
    }
  }

  //#region "show/hide"
  showProjectDetailPanel() {
    this.showTimeCategoryDetail = true;
    this.colsm6 = this.showTimeCategoryDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideProjectDetailPanel() {
    this.showTimeCategoryDetail = false;
    this.colsm6 = this.showTimeCategoryDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(event) {
    this.hideProjectDetailPanel();
  }
  //#endregion

  onTimeCategoryDeleted(event) {
    const index = this.getTimeCategoryList.findIndex(r => r.TimeCategoryId === event.id);
    this.getTimeCategoryList.splice(index, 1);
    this.child.ResetFormOnAddNewTimeCategory();
    this.hideProjectDetailPanel();
  }

  addTimeCategoryList(e) {
    this.getTimeCategoryList.push(e);
  }

  updateTimeCategoryList(e) {
    const index = this.getTimeCategoryList.findIndex(r => r.TimeCategoryId === e.TimeCategoryId);
    if (index !== -1) {
      this.getTimeCategoryList[index] = e;
    }
  }
}
