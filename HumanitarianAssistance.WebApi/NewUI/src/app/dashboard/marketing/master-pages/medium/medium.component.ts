import { Component, OnInit, ViewChild } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { MasterPageServiceService } from '../service/master-page-service.service';
import { MediumModel } from '../model/mastrer-pages.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MediumDetailsComponent } from './medium-details/medium-details.component';
import { ApplicationPages } from '../../../../shared/applicationpagesenum';
import { LocalStorageService } from '../../../../shared/services/localstorage.service';

@Component({
  selector: 'app-medium',
  templateUrl: './medium.component.html',
  styleUrls: ['./medium.component.scss']
})
export class MediumComponent implements OnInit {
  logedInForm; // These are variables
  mediumName;
  mediumId;
  isEditingAllowed = false;
  pageId = ApplicationPages.Medium;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  showMediumDetail = false;
  public selectedRowID;
  @ViewChild(MediumDetailsComponent) child: MediumDetailsComponent;
  constructor(private appurl: AppUrlService, private masterService: MasterPageServiceService,
    private localStorageService: LocalStorageService) { }

  getMediumList: MediumModel[];
  model: MediumModel = {};
  mediumListLoaderFlag = false;
  ngOnInit() {
    this.init();
    this.getMedium();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }

  init() {
    // this.jobsList = [];
  }

  getMedium() {
    this.mediumListLoaderFlag = true;
    this.getMediumList = [];
    this.masterService.GetList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMedium).subscribe(data => {
      if (data.StatusCode === 200) {
        this.mediumListLoaderFlag = false;
        this.getMediumList = data.data.Mediums;
      } else {
        this.mediumListLoaderFlag = false;
      }
    }, error => {
      // this.commonLoaderService.hideLoader();
      this.mediumListLoaderFlag = false;
    });
  }

  onItemClick(id: number) {
    if (this.isEditingAllowed) {
      this.mediumId = id;
      if (this.mediumId === 0 || this.mediumId === undefined || this.mediumId === null) {
        this.child.ResetFormOnAddNewMedium();
        this.child.CreateMediumonAddNew();
      }
      this.selectedRowID = id;
      this.showProjectDetailPanel();
    }
  }

  //#region "show/hide"
  showProjectDetailPanel() {
    this.showMediumDetail = true;
    this.colsm6 = this.showMediumDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideProjectDetailPanel() {
    this.showMediumDetail = false;
    this.colsm6 = this.showMediumDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(event) {
    this.hideProjectDetailPanel();
  }
  //#endregion

  onMediumDeleted(id) {
    const index = this.getMediumList.findIndex(r => r.MediumId === id.id);
    this.getMediumList.splice(index, 1);
    this.child.ResetFormOnAddNewMedium();
    this.hideProjectDetailPanel();
  }

  addMediumList(e) {
    this.getMediumList.push(e);
  }

  updateMediumList(e) {
    const index = this.getMediumList.findIndex(r => r.MediumId === e.MediumId);
    if (index !== -1) {
      this.getMediumList[index] = e;
    }
  }


}
