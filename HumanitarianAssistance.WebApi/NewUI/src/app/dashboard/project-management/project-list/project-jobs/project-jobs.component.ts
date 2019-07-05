import { Component, OnInit, HostListener, EventEmitter } from '@angular/core';
import { Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { AddprojectJobsComponent } from './addproject-jobs/addproject-jobs.component';
import {
  IProjectJobModel,
  ProjectJobsFilterModel
} from '../project-jobs/project-jobsmodel';
import { ProjectJobsService } from '../project-jobs/project-jobs.service';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { GLOBAL } from 'src/app/shared/global';
import { ProjectListService } from '../service/project-list.service';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-project-jobs',
  templateUrl: './project-jobs.component.html',
  styleUrls: ['./project-jobs.component.scss']
})
export class ProjectJobsComponent implements OnInit {
  //#region
  colsm6 = 'col-sm-10 col-sm-offset-1';
  showProjectJobsDetail = false;
  isEditingAllowed = false;
  firsts = 0;

  // screen scroll
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;
  selectedProjectJobId: number;
  //#endregion

  @Output() projectJobsDetailChanged = new EventEmitter<IProjectJobModel>();
  @Output() projectJobListRefresh = new EventEmitter<any>();
  projectId: number;

  projectJobsFilterModel: ProjectJobsFilterModel;
  projectJobDetail: IProjectJobModel;
  ProjectJobDetail: IProjectJobModel[];
  projectJobDetailList: IProjectJobModel[] = [];

  //#region loaderFlag
  ProjectJobsListLoaderFlag = false;
  pageId = ApplicationPages.ProjectJobs;
  //#endregion loaderFlag

  // "ProjectJobDetailLoader"
  projectJobDetailLoader = false;
  editProjectJobDetailLoader = false;
  //#endregion

  constructor(
    private routeActive: ActivatedRoute,
    private appurl: AppUrlService,
    public toastr: ToastrService,
    public dialog: MatDialog,
    public projectJobsService: ProjectJobsService,
    public projectListService: ProjectListService,
    public localStorageService: LocalStorageService
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    // this.getProjectJobList();
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
  });

    // this.projectId = +this.routeActive.snapshot.paramMap.get('id');

    this.initilizeProjectJobsFilterModel();
    this.getAllProjectJobsFilterList();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
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

  //#region initilizeModel
  initilizeProjectJobsFilterModel() {
    this.projectJobsFilterModel = {
      FilterValue: '',
      ProjectJobNameFlag: true,
      DateFlag: true,
      PageIndex: 0,
      PageSize: 10,
      TotalCount: 0,
      ProjectId: null
    };
  }
  //#endregion

  //#region pageEvent
  pageEvent(e) {
    this.projectJobsFilterModel.PageIndex = e.pageIndex;
    this.projectJobsFilterModel.PageSize = e.pageSize;
    this.onFilterApplied();
  }
  //#endregion
  onFilterApplied() {
    this.getAllProjectJobsFilterList();
  }

  paginate(event) {
    this.firsts = event.first / event.rows;
  }

  //#region "getAllProjectJobsFilterList"
  getAllProjectJobsFilterList() {
    this.ProjectJobsListLoaderFlag = true;
    this.projectJobsFilterModel.TotalCount = 0;
    this.projectJobsFilterModel.ProjectId = this.projectId;
    this.projectListService
      .GetProjectJobList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectJobFilterList,
        this.projectJobsFilterModel
      )
      .subscribe(
        data => {
          this.projectJobDetailList = [];
          if (
            data.data.ProjectJobDetail.length > 0 &&
            data.StatusCode === 200
          ) {
            this.projectJobsFilterModel.TotalCount =
              data.data.TotalCount != null ? data.data.TotalCount : 0;
            data.data.ProjectJobDetail.forEach(element => {
              this.projectJobDetailList.push({
                ProjectJobId: element.ProjectJobId,
                ProjectJobName: element.ProjectJobName,
                ProjectJobCode: element.ProjectJobCode,
                ProjectId: element.ProjectId
              });
              this.ProjectJobDetail = this.projectJobDetailList;
            });
          }
          this.ProjectJobsListLoaderFlag = false;
        },
        error => {
          this.ProjectJobsListLoaderFlag = false;
          this.toastr.error('Something went wrong!');
        }
      );
  }
  //#endregion
  //#region open dialgog
  openAddProjectJobsDialog(): void {
    // NOTE: It passed the data into the Add Voucher Model
    const dialogRef = this.dialog.open(AddprojectJobsComponent, {
      width: '550px',
      data: {
        data: 'hello',
        projectJobList: this.projectJobDetailList,
        projectId: this.projectId
      }
    });
    // list refeshed from add-budget to listing page
    dialogRef.componentInstance.onListRefresh.subscribe(() => {
      console.log('Refreshed');
      this.getAllProjectJobsFilterList();
      this.projectJobListRefresh.emit();
      // this.getProjectJobList();
    });

    dialogRef.afterClosed().subscribe(result => {
      // console.log(result);
    });
  }
  //#endregion

  //#region "getProjectJobList"
  getProjectJobList() {
    this.projectJobsService.GetProjectJobList().subscribe(
      (response: any) => {
        this.projectJobDetailList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.projectJobDetailList.push({
              ProjectJobId: element.ProjectJobId,
              ProjectJobCode: element.ProjectJobCode,
              ProjectJobName: element.ProjectJobName,
              ProjectId: element.ProjectId
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "onItemClick"
  onItemClick(item: number) {
    if (this.isEditingAllowed) {
      this.selectedProjectJobId = item;
      this.showProjectJobsDetailDetailPanel();
    }
  }
  //#endregion

  //#region "show/ hide"
  showProjectJobsDetailDetailPanel() {
    this.showProjectJobsDetail = true;
    this.colsm6 = this.showProjectJobsDetail
      ? 'col-sm-6'
      : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "onBudgetLineValuechange"
  onProjectJobsValuechange() {
    this.editProjectJobsDetailById(this.projectJobDetail);
  }
  //#endregion

  //#region "editBudgetDetailById"
  editProjectJobsDetailById(data: IProjectJobModel) {
    this.editProjectJobDetailLoader = true;
    this.projectJobsService.EditProjectJobsDetailById(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.projectJobsDetailChanged.emit(data);
        } else if (response.statusCode === 400) {
          this.toastr.warning(response.message);
        }
        this.editProjectJobDetailLoader = false;
      },
      error => {
        this.toastr.error('Someting went wrong');
        this.editProjectJobDetailLoader = false;
      }
    );
  }
  //#endregion

  //#region projectJobsDetailChangedEmit changes to list

  projectJobsDetailChangedEmit(event: IProjectJobModel) {
    console.log(event);
    const data = this.projectJobDetailList.find(
      x => x.ProjectJobId === event.ProjectJobId
    );
    const indexOfProjectJobs = this.projectJobDetailList.indexOf(data);
    this.projectJobDetailList[indexOfProjectJobs] = event;
  }
  //#endregion
  //#region "Delete Project Change emit"
  deleteProjectChangeEmit(id) {
    const index = this.projectJobDetailList.findIndex(
      r => r.ProjectJobId === id.id
    );
    this.projectJobDetailList.splice(index, 1);
  }
}
