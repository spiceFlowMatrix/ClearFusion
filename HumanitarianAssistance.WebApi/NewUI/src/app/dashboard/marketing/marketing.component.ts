import { Component, OnInit } from '@angular/core';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { marketingPagesMaster } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-marketing',
  templateUrl: './marketing.component.html',
  styleUrls: ['./marketing.component.scss']
})
export class MarketingComponent implements OnInit {
  setProjectHeader = 'Marketing';
  menuList: IMenuList[] = [
    {
      Id: 1,
      PageId: marketingPagesMaster.Clients,
      Text: 'Clients',
      Link: '/marketing/clients'
    },
    {
      Id: 2,
      PageId: marketingPagesMaster.UnitRates,
      Text: 'Unit Rates',
      Link: '/marketing/unitRateList'
    },
    {
      Id: 3,
      PageId: marketingPagesMaster.Jobs,
      Text: 'Jobs',
      Link: '/marketing/jobs'
    },
    {
      Id: 4,
      PageId: marketingPagesMaster.Contracts,
      Text: 'Contracts',
      Link: '/marketing/contracts'
    },
    {
      Id: 5,
      PageId: marketingPagesMaster.BroadCastPolicy,
      Text: 'Broadcast Policy',
      Link: '/marketing/policy'
    },
    {
      Id: 6,
      PageId: marketingPagesMaster.Scheduler,
      Text: 'Scheduler',
      Link: '/marketing/scheduler'
    }
  ];
  authorizedMenuList: IMenuList[] = [];

  constructor(
    private globalService: GlobalSharedService,
    private localStorageservice: LocalStorageService
  ) {
    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);

    this.authorizedMenuList = this.localStorageservice.GetAuthorizedPages(
      this.menuList
    );

    // Set Menu Header List
    this.globalService.setMenuList(this.authorizedMenuList);
  }

  ngOnInit() {}
}
