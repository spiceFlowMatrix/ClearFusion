import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectListService } from './service/project-list.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { UIModuleHeaders } from '../../../shared/enum';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { IProjectFilterModel } from './models/projectList.model';
import {
  ApplicationPages,
  projectPagesMaster
} from '../../../shared/applicationpagesenum';
import { LocalStorageService } from '../../../shared/services/localstorage.service';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { takeWhile } from 'rxjs/operators';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {
  //#region "variables"
  setSelectedHeader = UIModuleHeaders.ProjectModule;
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

  Showaddproject = true;
  hideproject = false;
  projectList: any[];
  ProjectDescription: string;
  projectFilterModel: IProjectFilterModel;

  // screen scroll
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;

  isEditingAllowed = false;
  pageId = ApplicationPages.MyProjects;

  // flag list
  projectListLoaderFlag = false;

  //#endregion

  constructor(
    public router: Router,
    public projectListService: ProjectListService,
    private appurl: AppUrlService,
    private commonLoaderService: CommonLoaderService,
    private localStorageService: LocalStorageService,
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

    this.getScreenSize();
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
  initProjectFilter() {
    this.projectFilterModel = {
      FilterValue: '',
      DateFlag: true,
      ProjectIdFlag: true,
      ProjectCodeFlag: true,
      ProjectNameFlag: true,
      DescriptionFlag: true,

      pageIndex: 0,
      pageSize: 10,
      totalCount: 0
    };
  }

  ngOnInit() {
    this.initProjectFilter();
    this.getAllProjectFilterList();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
  }

  //#region  paginatorEvent
  pageEvent(e) {
    this.projectFilterModel.pageIndex = e.pageIndex;
    this.projectFilterModel.pageSize = e.pageSize;

    this.onFilterApplied();
  }
  //#endregion

  //#region "onFilterApplied"
  onFilterApplied() {
    this.getAllProjectFilterList();
  }
  //#endregion

  //#region "getAllProjectList"
  getAllProjectFilterList() {
    this.projectFilterModel.totalCount = 0;
    this.projectListLoaderFlag = true;
    // this.commonLoaderService.showLoader();
    this.projectListService
      .GetProjectFilterDetails(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectFilterList,
        this.projectFilterModel
      )
      .subscribe(
        res => {
          this.projectList = [];
          if (res.data.ProjectDetailModel.length > 0 && res.StatusCode === 200) {
            this.projectFilterModel.totalCount =
            res.data.TotalCount != null ? res.data.TotalCount : 0;
            res.data.ProjectDetailModel.forEach(element => {
            this.projectList.push({
              ProjectId: element.ProjectId,
              ProjectName: element.ProjectName,
              ProjectPhase: element.ProjectPhase,
              TotalDaysinHours: element.TotalDaysinHours,
              ProjectCode: element.ProjectCode,
              ProjectDescription: element.ProjectDescription,
              IsWin: element.IsWin
            });
          });
          }
            this.projectListLoaderFlag = false;
        },
        error => {
          this.projectListLoaderFlag = false;
        }
      );
    // this.projectList = this.store.select(state => state.projectList);
  }
  //#endregion

  //#endregion "addNewProject"
  addNewProject() {
    const projectDetail = {
      ProjectId: 0,
      ProjectName: ''
    };
    this.commonLoaderService.showLoader();

    this.projectListService
      .AddProjectDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditProjectDetail,
        projectDetail
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            if (response.data.ProjectDetail != null) {
              const responseData = response.data.ProjectDetail;
              const projectData = {
                ProjectId: responseData.ProjectId,
                ProjectCode: responseData.ProjectCode,
                ProjectName: responseData.ProjectName,
                ProjectPhase: responseData.ProjectPhase,
                TotalDaysinHours: responseData.TotalDaysinHours,
                ProjectDescription: responseData.ProjectDescription
              };
              this.projectList.push(projectData);
              this.projectList.sort(function(a, b) {
                return b.ProjectId - a.ProjectId;
              });

              this.router.navigate(['/project/my-project', projectData.ProjectId]);
            }
          }
          this.commonLoaderService.hideLoader();
        },
        error => {
          this.commonLoaderService.hideLoader();
        }
      );
  }
  //#endregion

  onProjectClicked(data: number) {
    this.projectListService.onShowHideHeader(false);
    this.router.navigate(['/project/my-project', data]);
  }

  // addproject() {
  //   this.Showaddproject = false;
  // }
  // backlistproject() {
  //   this.Showaddproject = true;
  // }

  //#region "onFilterReset"
  onFilterReset() {
    this.initProjectFilter();
    this.getAllProjectFilterList();
  }
  //#endregion
}
