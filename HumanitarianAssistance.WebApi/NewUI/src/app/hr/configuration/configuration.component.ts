import { Component, OnInit } from '@angular/core';
import { PurchaseListComponent } from 'src/app/store/components/purchase-list/purchase-list.component';
import { DesignationListingComponent } from './components/designation-listing/designation-listing.component';
import { Router } from '@angular/router';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.scss']
})
export class ConfigurationComponent implements OnInit {

  navLinks: any[] = [];
  activeLinkIndex = 0;
  setConfigurationHeader = 'HR';

  constructor(private router: Router, private globalService: GlobalSharedService) {

    this.navLinks = [
      {
        label: 'GENERAL',
        link: './general',
        index: 0
      },
      {
        label: 'DESIGNATION',
        link: './designation',
        index: 1
      },
      {
        label: 'EXIT INTERVIEW',
        link: './exit-interview-questions',
        index: 2
      },
    ];
  }

  ngOnInit() {
    this.router.events.subscribe((res) => {
      this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.link === '.' + this.router.url));
    });
    this.globalService.setMenuHeaderName(this.setConfigurationHeader);
  }
}
