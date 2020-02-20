import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
      {
        label: 'LEAVE POLICY',
        link: './leave-policy',
        index: 3
      },
      {
        label: 'APPRAISAL QUESTIONS',
        link: './appraisal-questions',
        index: 4
      },
      {
        label: 'ATTENDANCE GROUPS',
        link: './attendance-groups',
        index: 5
      },
      {
        label: 'CONTRACT CLAUSES',
        link: './contract-clauses',
        index: 6
      },
    ];
  }

  ngOnInit() {
    this.router.events.subscribe(() => {
      this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.link === '.' + this.router.url));
    });
    this.globalService.setMenuHeaderName(this.setConfigurationHeader);
  }
}
