import { Component, OnInit, ViewChild } from '@angular/core';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MasterPageServiceService } from '../service/master-page-service.service';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { ChannelModel } from '../model/mastrer-pages.model';
import { ChannelDetailComponent } from './channel-detail/channel-detail.component';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Component({
  selector: 'app-channel',
  templateUrl: './channel.component.html',
  styleUrls: ['./channel.component.scss']
})
export class ChannelComponent implements OnInit {

  channelList: ChannelModel[];
  channelDetails: ChannelModel = {};
  channel;
  channelId;
  showChannelDetail = false;
  isEditingAllowed = false;
  pageId = ApplicationPages.Channel;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  public selectedRowID;
  channelListLoaderFlag = false;
  @ViewChild(ChannelDetailComponent) child: ChannelDetailComponent;
  constructor(private appurl: AppUrlService, private masterService: MasterPageServiceService,
  private localStorageService: LocalStorageService) { }

  ngOnInit() {
    this.init();
    this.getChannels();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }

  init() {
  }

  getChannels() {
    this.channelList = [];
    this.channelListLoaderFlag = true;
    this.masterService.GetChannelList().subscribe((data:IResponseData) => {
      if (data.statusCode === 200) {
        this.channelListLoaderFlag = false;
        this.channelList =  data.data;
      } else {
        this.channelListLoaderFlag = false;
      }
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.channelListLoaderFlag = false;
    });
  }

  onItemClick(id: number) {
     if (this.isEditingAllowed) {
      this.channelId = id;
      if (this.channelId === 0 || this.channelId === undefined || this.channelId === null) {
         this.child.ResetFormOnAddNewChannel();
      }
      this.selectedRowID = id;
      this.showChannelDetailPanel();
     }
  }

  //#region "show/hide"
  showChannelDetailPanel() {
    this.showChannelDetail = true;
    this.colsm6 = this.showChannelDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideChannelDetailPanel() {
    this.showChannelDetail = false;
    this.colsm6 = this.showChannelDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(event) {
    this.hideChannelDetailPanel();
  }
  //#endregion

  onChannelDeleted(id) {
    const index = this.channelList.findIndex(r => r.ChannelId === id.id);
    this.channelList.splice(index, 1);
    this.child.ResetFormOnAddNewChannel();
    this.hideChannelDetailPanel();
  }

  addChannelList(e) {
    this.channelList.push(e);
  }

  updateChannelList(e) {
    const index = this.channelList.findIndex(r => r.ChannelId === e.ChannelId);
    if (index !== -1) {
      this.channelList[index] = e;
    }
  }


}
