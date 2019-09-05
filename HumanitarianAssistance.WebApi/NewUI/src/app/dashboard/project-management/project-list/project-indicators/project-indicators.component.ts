import { Component, OnInit, ViewChild, HostListener } from '@angular/core';

import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProjectIndicatorDetailComponent } from './project-indicator-detail/project-indicator-detail.component';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ProjectListService } from '../service/project-list.service';
import { GLOBAL } from 'src/app/shared/global';
import {
  ProjectIndicatorFilterModel,
  ProjectIndicatorModel,
  IProjectIndicatorModel
} from './project-indicators-model';
import { MatDialog } from '@angular/material';
import { AddProjectIndicatorComponent } from './add-project-indicator/add-project-indicator.component';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { ProjectIndicatorService } from './project-indicator.service';

@Component({
  selector: 'app-project-indicators',
  templateUrl: './project-indicators.component.html',
  styleUrls: ['./project-indicators.component.scss']
})
export class ProjectIndicatorsComponent implements OnInit {
  //#region "variables"
  @ViewChild(ProjectIndicatorDetailComponent) child;

  ProjectindicatorDetail: any;
  indicatorFilterModel: ProjectIndicatorFilterModel;
  projectId: number;

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

  constructor(
    private appurl: AppUrlService,
    public projectListService: ProjectListService,
    public router: Router,
    public toastr: ToastrService,
    public dialog: MatDialog,
    private routeActive: ActivatedRoute,
    public indicatorService: ProjectIndicatorService
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.indicatorFilterModel = {
      pageIndex: 0,
      pageSize: 10,
      totalCount: 0,
      ProjectId: null,
      Description: null,
      ProjectIndicatorId: null,
      Questions: null
    };
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getAllProjectIndicatorList();
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

  onItemClick(item: any) {
    this.projectIndicatorId = item.ProjectIndicatorId;
    if (
      this.projectIndicatorId === 0 ||
      this.projectIndicatorId === undefined ||
      this.projectIndicatorId === null
    ) {
      // this.child.formReset();
      // this.child.CreateProjectIndicatorOnAddNew();
    }
    this.selectedRowID = item.ProjectIndicatorId;
    this.ProjectindicatorDetail = item;
    console.log('detail',this.ProjectindicatorDetail);
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
    if (this.projectId != null && this.projectId != undefined) {
    this.projectIndicatorListLoaderFlag = true;
    this.indicatorFilterModel.totalCount = 0;
    this.indicatorFilterModel.ProjectId = this.projectId;
    this.projectListService
      .GetProjectIndicatorsList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProjectIndicators,
        this.indicatorFilterModel
      )
      .subscribe(
        data => {
          this.projectIndicatorList = [];
          if (
            data.data.ProjectIndicatorList !== undefined &&
            data.data.ProjectIndicatorList.ProjectIndicators !== undefined &&
            data.data.ProjectIndicatorList.ProjectIndicators !== null &&
            data.data.ProjectIndicatorList.ProjectIndicators.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.ProjectIndicatorList.ProjectIndicators.forEach(
              element => {
                this.projectIndicatorList.push({
                  ProjectIndicatorId: element.ProjectIndicatorId,
                  IndicatorName: element.IndicatorName,
                  IndicatorCode: element.IndicatorCode,
                  Description: element.Description,
                  Questions: element.Questions,
                });
                // this.DonorDetailModel = this.donorList;
              }
            );

            this.indicatorFilterModel.totalCount =
              data.data.ProjectIndicatorList.IndicatorRecordCount;
          }

          this.projectIndicatorListLoaderFlag = false;
        },
        error => {
          this.projectIndicatorListLoaderFlag = false;
          this.toastr.error('Something went wrong');
        }
      );
  }
}
  //#endregion

  addProjectIndicator(e) {
    this.projectIndicatorList.unshift(e.projectIndicator);
    this.indicatorFilterModel.totalCount = e.count;
  }

  editIndicatorList(e) {
    const index = this.projectIndicatorList.findIndex(
      r => r.ProjectIndicatorId === e.ProjectIndicatorId
    );
    if (index !== -1) {
      this.projectIndicatorList[index] = e;
    }
  }

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

  //#region "onAddNewIndicator"
  onAddNewIndicator() {
    this.openIndicatorDialog();
  }

  openIndicatorDialog(): void {
    // NOTE: It passed the data into the AddProjectIndicatorComponent Model
    const dialogRef = this.dialog.open(AddProjectIndicatorComponent, {
      width: '550px',
      autoFocus: false,
      data: {
        ProjectId: this.projectId
      }
    });

    // refresh the list after new request created
    dialogRef.componentInstance.onIndicatorListRefresh.subscribe(() => {
      // do something
      this.getAllProjectIndicatorList();
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion
//#region "Listupdate After update"
  OnindicatorListRefresh(event: any){
    const data = this.projectIndicatorList.find(
      x => x.ProjectIndicatorId === event.ProjectIndicatorId
    );
    const indexOfIndicatorList = this.projectIndicatorList.indexOf(data);
    this.projectIndicatorList[indexOfIndicatorList] = event;
  }
  //#endregion

  //#region "onIndicatorEditClick"
  onDeleteIndicator(data: any) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });

    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText =
      Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText =
      Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => {
    });
    dialogRef.componentInstance.confirmDelete.subscribe(res => {
      dialogRef.componentInstance.isLoading = true;
      if (
        data.ProjectIndicatorId != null &&
        data.ProjectIndicatorId !== undefined &&
        data.ProjectIndicatorId !== 0
      ) {
        this.indicatorService
          .DeleteIndicatorDetail(
            data.ProjectIndicatorId
          )
          .subscribe(response => {
             if (response.statusCode === 200) {
               //to rerfresh the question page list
               this.child.questionListOnindicatorDelete(data.ProjectIndicatorId) ;
               this.deletedListRefresh(data.ProjectIndicatorId);
             }
            dialogRef.componentInstance.isLoading = false;
            dialogRef.componentInstance.onCancelPopup();
          },
        error => {
          this.toastr.error('Someting went wrong');
          dialogRef.componentInstance.isLoading = false;
          dialogRef.componentInstance.onCancelPopup();
        });
      }
    });
  }
  //#endregion
  // refresh list after delete
deletedListRefresh(item: number) {
  const findIndex = this.projectIndicatorList.findIndex(
    x => x.ProjectIndicatorId === item
  );
  this.projectIndicatorList.splice(findIndex, 1);
}
}
