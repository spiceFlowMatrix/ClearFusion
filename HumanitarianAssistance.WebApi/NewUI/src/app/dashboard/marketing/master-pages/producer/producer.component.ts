import { Component, OnInit, ViewChild } from '@angular/core';
import { ProducerModel } from '../model/mastrer-pages.model';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MasterPageServiceService } from '../service/master-page-service.service';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { GLOBAL } from 'src/app/shared/global';
import { ProducerDetailsComponent } from './producer-details/producer-details.component';

@Component({
  selector: 'app-producer',
  templateUrl: './producer.component.html',
  styleUrls: ['./producer.component.scss']
})
export class ProducerComponent implements OnInit {
  producerList: ProducerModel[];
  producerDetails: ProducerModel = {};
  producerId: number;
  showProducerDetail = false;
  isEditingAllowed = false;
  pageId = ApplicationPages.Producer;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  selectedRowID: number;
  producerListLoaderFlag = false;
  @ViewChild(ProducerDetailsComponent) child: ProducerDetailsComponent;
  constructor(private appurl: AppUrlService, private masterService: MasterPageServiceService,
  private localStorageService: LocalStorageService) { }

  ngOnInit() {
    this.getProducers();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }

  getProducers() {
    this.producerList = [];
    this.producerListLoaderFlag = true;
    this.masterService.GetList(this.appurl.getApiUrl() + GLOBAL.API_Producer_GetProducer).subscribe(data => {
      if (data.StatusCode === 200) {
        this.producerList =  data.data.Producers;
      }
       this.producerListLoaderFlag = false;
    },
    error => {
      this.producerListLoaderFlag = false;
    });
  }

  onItemClick(id: number) {
     if (this.isEditingAllowed) {
      this.producerId = id;
      if (this.producerId === 0 || this.producerId === undefined || this.producerId === null) {
         this.child.ResetFormOnAddNewProducer();
         this.child.CreateProduceronAddNew();
      }
      this.selectedRowID = id;
      this.showProducerDetailPanel();
     }
  }

  //#region "show/hide"
  showProducerDetailPanel() {
    this.showProducerDetail = true;
    this.colsm6 = this.showProducerDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideProducerDetailPanel() {
    this.showProducerDetail = false;
    this.colsm6 = this.showProducerDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(event: any) {
    this.hideProducerDetailPanel();
  }
  //#endregion

  onProducerDeleted(event) {
    const index = this.producerList.findIndex(r => r.ProducerId === event.id);
    this.producerList.splice(index, 1);
    this.child.ResetFormOnAddNewProducer();
    this.hideProducerDetailPanel();
  }

  addProducerList(e) {
    this.producerList.push(e);
  }

  updateProducerList(e) {
    const index = this.producerList.findIndex(r => r.ProducerId === e.ProducerId);
    if (index !== -1) {
      this.producerList[index] = e;
    }
  }
}
