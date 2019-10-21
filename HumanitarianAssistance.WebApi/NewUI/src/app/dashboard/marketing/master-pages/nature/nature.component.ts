import { Component, OnInit, ViewChild } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { MasterPageServiceService } from '../service/master-page-service.service';
import { NatureModel } from '../model/mastrer-pages.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NautreDetailsComponent } from './nautre-details/nautre-details.component';
import { LocalStorageService } from '../../../../shared/services/localstorage.service';
import { ApplicationPages } from '../../../../shared/applicationpagesenum';

@Component({
  selector: 'app-nature',
  templateUrl: './nature.component.html',
  styleUrls: ['./nature.component.scss']
})
export class NatureComponent implements OnInit {
  natureName;
  natureId;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  showNatureDetail = false;
  isEditingAllowed = false;
  pageId = ApplicationPages.Nature;
  public selectedRowID;
  natureListLoaderFlag = false;
  @ViewChild(NautreDetailsComponent) child: NautreDetailsComponent;
  constructor(private appurl: AppUrlService, private masterService: MasterPageServiceService,
    private localStorageService: LocalStorageService) { }

  getNatureList: NatureModel[];
  model: NatureModel = {};

  ngOnInit() {
    this.init();
    this.getNature();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }

  init() {
    // this.jobsList = [];
  }

  getNature() {
    this.natureListLoaderFlag = true;
    this.getNatureList = [];
    this.masterService.GetList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetNature).subscribe(data => {
      if (data.StatusCode === 200) {
        this.natureListLoaderFlag = false;
        this.getNatureList = data.data.Natures;
      } else {
        this.natureListLoaderFlag = false;
      }
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.natureListLoaderFlag = false;
    });
  }

  onItemClick(id: number) {
    if (this.isEditingAllowed) {
      this.natureId = id;
      if (this.natureId === 0 || this.natureId === undefined || this.natureId === null) {
        this.child.ResetFormOnAddNewNature();
        this.child.CreateNatureonAddNew();
      }
      this.selectedRowID = id;
      this.showProjectDetailPanel();
    }
  }

  //#region "show/hide"
  showProjectDetailPanel() {
    this.showNatureDetail = true;
    this.colsm6 = this.showNatureDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideProjectDetailPanel() {
    this.showNatureDetail = false;
    this.colsm6 = this.showNatureDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(event) {
    this.hideProjectDetailPanel();
  }
  //#endregion

  onNatureDeleted(id) {
    const index = this.getNatureList.findIndex(r => r.NatureId === id.id);
    this.getNatureList.splice(index, 1);
    this.child.ResetFormOnAddNewNature();
    this.hideProjectDetailPanel();
  }

  addNatureList(e) {
    this.getNatureList.push(e);
  }

  updateNatureList(e) {
    const index = this.getNatureList.findIndex(r => r.NatureId === e.NatureId);
    if (index !== -1) {
      this.getNatureList[index] = e;
    }
  }
}
