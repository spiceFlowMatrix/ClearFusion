import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { ProjectListService } from '../service/project-list.service';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { projectPagesMaster } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { ActivatedRoute } from '@angular/router';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent implements OnInit {
  //#region "Variables"

  @ViewChild(ProjectDetailComponent) projectDetailChild: ProjectDetailComponent;

  projectId: number;
  setProjectHeader = 'Project';
  isProjectWin = false;

  menuList: IMenuList[] = [
    {
      Id: 1,
      PageId: projectPagesMaster.ProjectDetails,
      Text: 'Details',
      Link: 'detail'
    },
    {
      Id: 2,
      PageId: projectPagesMaster.CriteriaEvaluation,
      Text: 'Criteria Evaluation',
      Link: 'criteria-evaluation'
    },
    {
      Id: 3,
      PageId: projectPagesMaster.Proposal,
      Text: 'Proposal',
      Link: 'proposal'
    },
    {
      Id: 4,
      PageId: projectPagesMaster.ProjectJobs,
      Text: 'Project Jobs',
      Link: 'project-jobs'
    },
    {
      Id: 5,
      PageId: projectPagesMaster.ProjectBudgetLine,
      Text: 'Budget Lines',
      Link: 'budget-lines'
    },
    {
      Id: 6,
      PageId: projectPagesMaster.ProjectActivities,
      Text: 'Project Activities',
      Link: 'project-activities'
    },
    {
      Id: 7,
      PageId: projectPagesMaster.ProjectPeople,
      Text: 'People',
      Link: 'people'
    },
    {
      Id: 8,
      PageId: projectPagesMaster.HiringRequests,
      Text: 'Hiring Requests',
      Link: 'hiring-request'
    }
  ];
  authorizedMenuList: IMenuList[] = [];

  // screen scroll
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;
  //#endregion

  constructor(
    private routeActive: ActivatedRoute,
    private localStorageService: LocalStorageService,
    public projectListService: ProjectListService,
    private globalService: GlobalSharedService,
    private appurl: AppUrlService
  ) {
    this.getScreenSize();

    this.projectId = +this.routeActive.snapshot.paramMap.get('id');

    this.menuList.map(
      x => (x.Link = '/project/my-project/' + this.projectId + '/' + x.Link)
    ); // important for routing

    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);
    this.globalService.setMenuList(this.menuList.filter((i, index) => index < 3));

    // this.setHeaderMenu();
    this.getProjectWinLossDetail(this.projectId);
  }
  ngOnInit() {
  }

  setHeaderMenu() {
    // check weather the project is win or loss
    this.authorizedMenuList = this.localStorageService.GetAuthorizedPages(
      this.isProjectWin
        ? this.menuList
        : this.menuList.filter((i, index) => index < 3)
    );

    // Set Menu Header List
    this.globalService.setMenuList(this.authorizedMenuList);
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region Get win loss deytail by  project id
  getProjectWinLossDetail(projectId: number) {
    this.projectListService
      .GetProjectWinLossDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectWinLossStatus,
        projectId
      )
      .subscribe(
        data => {
          if (data != null) {
            if (data.data.ProjectWinLoss != null) {
              // check weather the project is win or loss
              this.isProjectWin = data.data.ProjectWinLoss;
              this.setHeaderMenu();
            }
          }
        },
        error => {}
      );
  }
  //#endregion
}
