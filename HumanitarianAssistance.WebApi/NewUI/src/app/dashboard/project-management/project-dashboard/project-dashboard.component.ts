import { Component, OnInit } from '@angular/core';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { projectPagesMaster } from 'src/app/shared/applicationpagesenum';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-project-dashboard',
  templateUrl: './project-dashboard.component.html',
  styleUrls: ['./project-dashboard.component.scss']
})
export class ProjectDashboardComponent implements OnInit {

  setProjectHeader = 'Projects';
  menuList: IMenuList[] = [
    {
      Id: 1,
      PageId: projectPagesMaster.ProjectDashboard,
      Text: 'Dashboard',
      Link: '/project/project-dashboard'
    },
    {
      Id: 2,
      PageId: projectPagesMaster.MyProjects,
      Text: 'My Projects',
      Link: '/project/my-projects'
    },
    {
      Id: 3,
      PageId: projectPagesMaster.Donors,
      Text: 'Donors',
      Link: '/project/project-donor'
    },
    {
      Id: 4,
      PageId: projectPagesMaster.ProjectCashFlow,
      Text: 'Cash Flow',
      Link: '/project/project-cash-flow'
    },
    {
      Id: 5,
      PageId: projectPagesMaster.ProposalReport,
      Text: 'Proposal Report',
      Link: '/project/proposal-report'
    },
    {
      Id: 6,
      PageId: projectPagesMaster.ProjectIndicators,
      Text: 'Project Indicators',
      Link: '/project/project-indicators'
    }
  ];
  authorizedMenuList: IMenuList[] = [];

  constructor(
        private globalService: GlobalSharedService,
        private localStorageservice: LocalStorageService
        ) {
    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);

   this.authorizedMenuList = this.localStorageservice.GetAuthorizedPages(this.menuList);

    // Set Menu Header List
    this.globalService.setMenuList(this.authorizedMenuList);
  }

  ngOnInit() {


  }


}
