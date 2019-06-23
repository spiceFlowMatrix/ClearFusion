import { Component, OnInit, ViewChild } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { MasterPageServiceService } from '../service/master-page-service.service';
import { MediumModel } from '../model/mastrer-pages.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { QualityModel } from '../../contracts/model/contract-details.model';
import { QualityDetailsComponent } from './quality-details/quality-details.component';
import { ApplicationPages } from '../../../../shared/applicationpagesenum';
import { LocalStorageService } from '../../../../shared/services/localstorage.service';
@Component({
  selector: 'app-quality',
  templateUrl: './quality.component.html',
  styleUrls: ['./quality.component.scss']
})
export class QualityComponent implements OnInit {
  logedInForm: FormGroup; // These are variables
  qualityName: string;
  display = 'none';
  qualityId: number;
  showQualityDetail;
  public selectedRowID: number;
  qualityListLoaderFlag = false;
  isEditingAllowed = false;
  pageId = ApplicationPages.Quality;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  @ViewChild(QualityDetailsComponent) child: QualityDetailsComponent;
  constructor(private appurl: AppUrlService, private masterService: MasterPageServiceService,
    private localStorageService: LocalStorageService) { }
  getQualityList: QualityModel[];
  model: QualityModel = {};
  ngOnInit() {
    this.init();
    this.getQuality();
    this.logedInForm = new FormGroup({
      qualityId: new FormControl(),
      qualityName: new FormControl('', [
        Validators.required])
    });

    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }

  init() {
    // this.jobsList = [];
  }

  getQuality() {
    this.getQualityList = [];
    this.qualityListLoaderFlag = true;
    this.masterService.GetList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetQualityList).subscribe(data => {
      if (data.StatusCode === 200) {
        this.getQualityList = data.data.Qualities;
      }
        this.qualityListLoaderFlag = false;

    },
    error => {
      this.qualityListLoaderFlag = false;
    });
  }

  onItemClick(id: number) {
    if (this.isEditingAllowed) {
      this.qualityId = id;
      if (this.qualityId === 0 || this.qualityId === undefined || this.qualityId === null) {
        this.child.ResetFormOnAddNewQuality();
        this.child.CreateQualityonAddNew();
      }
      this.selectedRowID = id;
      this.showProjectDetailPanel();
    }
  }

  //#region "show/hide"
  showProjectDetailPanel() {
    this.showQualityDetail = true;
    this.colsm6 = this.showQualityDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideProjectDetailPanel() {
    this.showQualityDetail = false;
    this.colsm6 = this.showQualityDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(event) {
    this.hideProjectDetailPanel();
  }
  //#endregion

  onQualityDeleted(event) {
    const index = this.getQualityList.findIndex(r => r.QualityId === event.id);
    this.getQualityList.splice(index, 1);
    this.child.ResetFormOnAddNewQuality();
    this.hideProjectDetailPanel();
  }

  addQualityList(e) {
    this.getQualityList.push(e);
  }

  updateQualityList(e) {
    const index = this.getQualityList.findIndex(r => r.QualityId === e.QualityId);
    if (index !== -1) {
      this.getQualityList[index] = e;
    }
  }
}
