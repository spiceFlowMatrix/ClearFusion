import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { ProjectListService } from '../service/project-list.service';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { Observable, forkJoin } from 'rxjs';

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
  isCEApproved = false;
  menuList: IMenuList[];
  authorizedMenuList: IMenuList[] = [];
  isSuperadmin = false;
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
    private appurl: AppUrlService,
    private router: Router
  ) {

    this.getScreenSize();
    this.projectId = +this.routeActive.snapshot.paramMap.get('id');

    this.menuList = [];

    this.projectListService.menuList.forEach(x => {
      this.menuList.push({
        Id: x.Id,
        PageId: x.PageId,
        Text: x.Text,
        Link: '/project/my-project/' + this.projectListService.selectedProject() + x.Link
      });
    });

    // this.menuList = this.projectListService.getAllProjectMenu();

    // this.menuList.map(
    //   x => (x.Link = this.router.url.substr(0, this.router.url.lastIndexOf('/') + 1) + x.Link)
    // );

    // important for routing

    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);
    this.globalService.setMenuList(this.menuList.filter((i, index) => index < 2));

    // this.setHeaderMenu();
    // this.getProjectWinLossDetail(this.projectId);
    // this.getCriteriaEvaluationApprovedDetail(this.projectId);

  }
  ngOnInit() {
    this.getMultipleApiReuest().subscribe((res) => {
      this.getResponseProjectWinLossDetail(res[0]);
      this.getResponseCriteriaEvaluationApproved(res[1]);
      this.setHeaderMenu();
    });
  }

  // fork join to get multiple api response
  getMultipleApiReuest(): Observable<any> {
    const projectDetailResp = this.getProjectWinLossDetail(this.projectId);
    const approvedCEResp = this.getCriteriaEvaluationApprovedDetail(this.projectId);
    return forkJoin([projectDetailResp, approvedCEResp]);
  }


  setHeaderMenu() {
    // check weather user is superadmin
    this.isSuperadmin = this.localStorageService.GetSuperAdminDetail();
    if (this.isSuperadmin) {
      this.globalService.setMenuList(this.menuList);
      // check weather the criteria evaluation is approved and project is win
    } else if (this.isProjectWin === true) {
      this.authorizedMenuList = this.localStorageService.GetAuthorizedPages(
        this.isProjectWin
          ? this.menuList
          : this.menuList.filter((i, index) => index < 3)
      );
      this.globalService.setMenuList(this.authorizedMenuList);
    } else {



      this.authorizedMenuList = this.localStorageService.GetAuthorizedPages(
        this.isCEApproved
          ? this.menuList.filter((i, index) => index < 3)
          : this.menuList.filter((i, index) => index < 2)

      );

      // this.authorizedMenuList =
      //   this.isCEApproved
      //     ? this.menuList.filter(res => {
      //       return pages1.findIndex(m => m === res.Id) > -1
      //     })
      //     : this.menuList.filter(res => {
      //       return pages2.findIndex(m => m === res.Id) > -1
      //     })
      this.authorizedMenuList.push(this.menuList.find(r=>r.Id===7));
      this.globalService.setMenuList(this.authorizedMenuList);
    }
    // Set Menu Header List
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
    return this.projectListService
      .GetProjectWinLossDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectWinLossStatus,
        projectId
      );

  }
  //#endregion


  //#region Get criteria evaluation approved by  project id
  getCriteriaEvaluationApprovedDetail(projectId: number) {
    return this.projectListService
      .GetIsApprovedCriteriaEvaluationDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetIsApprovedCriteriaEvaluationStatus,
        projectId
      );
  }
  //#endregion

  //#region "getResponseProjectWinLossDetail"
  getResponseProjectWinLossDetail(res) {
    if (res != null) {
      if (res.data.ProjectWinLoss != null) {
        // check weather the project is win or loss
        this.isProjectWin = res.data.ProjectWinLoss;
      }
    }

  }

  //#endregion
  //#region "getResponseCriteriaEvaluationApproved"
  getResponseCriteriaEvaluationApproved(data) {
    if (data != null) {
      if (data.data.IsApprovedCriteriaEvaluation != null) {
        // check weather the project is win or loss
        this.isCEApproved = data.data.IsApprovedCriteriaEvaluation;
      }
    }
  }
  //#endregion
}
