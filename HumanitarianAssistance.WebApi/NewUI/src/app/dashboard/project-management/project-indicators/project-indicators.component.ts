import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { UIModuleHeaders } from 'src/app/shared/enum';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { projectPagesMaster } from 'src/app/shared/applicationpagesenum';
import { ProjectIndicatorDetailComponent } from './project-indicator-detail/project-indicator-detail.component';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { ProjectListService } from 'src/app/dashboard/project-management/project-list/service/project-list.service';
import { ProjectIndicatorFilterModel, ProjectIndicatorModel } from 'src/app/dashboard/project-management/project-indicators/project-indicators-model';
import { ToastrService } from 'ngx-toastr';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-project-indicators',
  templateUrl: './project-indicators.component.html',
  styleUrls: ['./project-indicators.component.scss']
})
export class ProjectIndicatorsComponent implements OnInit {

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



  indicatorFilterModel: ProjectIndicatorFilterModel;

   @ViewChild(ProjectIndicatorDetailComponent) child;

  projectIndicatorId: number;
  selectedRowID: number;
  projectIndicatorList: ProjectIndicatorModel[];

   //#region loaderFlag
  projectIndicatorListLoaderFlag = false;
  showJobDetail = false;
  isEditingAllowed = false;

  // screen
  scrollStyles: any;
  screenHeight: any;
  screenWidth: any;
  colsm6 = 'col-sm-10 col-sm-offset-1';

  constructor( private appurl: AppUrlService,
    public projectListService: ProjectListService,
    private localStorageService: LocalStorageService,
    private globalSharedService: GlobalSharedService,
    public router: Router,
    public toastr: ToastrService
    ) {

      // Set Menu Header Name
    this.globalSharedService.setMenuHeaderName(this.setProjectHeader);

    this.authorizedMenuList = this.localStorageService.GetAuthorizedPages(
      this.menuList
    );

    // Set Menu Header List
    this.globalSharedService.setMenuList(this.authorizedMenuList);

      this.getScreenSize();
    }

  ngOnInit() {
    this.indicatorFilterModel={
      pageIndex: 0,
      pageSize: 10,
      totalCount: 0
    }

    this.getAllProjectIndicatorList();
  }

  onItemClick(id: number) {
    this.projectIndicatorId = id;
    if (this.projectIndicatorId === 0 || this.projectIndicatorId === undefined || this.projectIndicatorId === null) {
      this.child.formReset();
      this.child.CreateProjectIndicatorOnAddNew();
    }
    this.selectedRowID = id;
    this.showProjectDetailPanel();
  }

   //#region pageEvent
   pageEvent(e) {
    this.indicatorFilterModel.pageIndex = e.pageIndex;
    this.indicatorFilterModel.pageSize = e.pageSize;

    this.onFilterApplied();
  }

  //#endregion
  onFilterApplied() {
    this.getAllProjectIndicatorList();
  }

  //#region "getAllProjectIndicatorList"
  getAllProjectIndicatorList() {
    this.projectIndicatorListLoaderFlag = true;
    this.indicatorFilterModel.totalCount = 0;
    this.projectListService.GetProjectIndicatorsList(this.appurl.getApiUrl() +
    GLOBAL.API_Project_GetAllProjectIndicators, this.indicatorFilterModel).subscribe(
      (data) => {
        this.projectIndicatorList = [];
        if (data.data.ProjectIndicatorList !== undefined && data.data.ProjectIndicatorList.ProjectIndicators !== undefined
          && data.data.ProjectIndicatorList.ProjectIndicators !== null &&
          data.data.ProjectIndicatorList.ProjectIndicators.length > 0 && data.StatusCode === 200) {
          data.data.ProjectIndicatorList.ProjectIndicators.forEach(element => {
            this.projectIndicatorList.push({
              projectIndicatorId: element.ProjectIndicatorId,
              projectIndicatorName: element.IndicatorName,
              projectIndicatorCode: element.IndicatorCode,
            });
           // this.DonorDetailModel = this.donorList;
          });

          this.indicatorFilterModel.totalCount= data.data.ProjectIndicatorList.IndicatorRecordCount;
        }

        this.projectIndicatorListLoaderFlag = false;
      },
      (error) => {
        this.projectIndicatorListLoaderFlag = false;
        this.toastr.error('Something went wrong');
      });
  }
  //#endregion

  addProjectIndicator(e) {
    this.projectIndicatorList.unshift(e.projectIndicator);
    this.indicatorFilterModel.totalCount = e.count;
  }

  editIndicatorList(e){
    const index = this.projectIndicatorList.findIndex(r => r.projectIndicatorId === e.projectIndicatorId);
    if (index !== -1) {
      this.projectIndicatorList[index] = e;
    }
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      'height': this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "show/hide"
  showProjectDetailPanel() {
    this.showJobDetail = true;
    this.colsm6 = this.showJobDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideProjectDetailPanel(e) {
    this.showJobDetail = false;
    this.colsm6 = this.showJobDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion
}
