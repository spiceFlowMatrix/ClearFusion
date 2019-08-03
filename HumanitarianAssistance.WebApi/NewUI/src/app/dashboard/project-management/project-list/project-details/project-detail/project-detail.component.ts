import {
  Component,
  OnInit,
  ViewChild,
  HostListener,
  Output,
  EventEmitter
} from '@angular/core';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { Observable } from 'rxjs/internal/Observable';
import { Subscription } from 'rxjs/internal/Subscription';
import { ProposalComponent } from '../../proposal/proposal.component';
import { BudgetLineListingComponent } from '../../budgetlines/budget-line-listing/budget-line-listing.component';
import { ProjectActivitiesComponent } from '../../project-activities/project-activities.component';
import { AcceptProposalComponent } from '../../accept-proposal/accept-proposal.component';
import {
  ProjectDetailModel,
  ApproveProjectDetailModel,
  WinApprovalDetailModel,
  UserListModel,
  IApproveRejectModel
} from '../models/project-details.model';
import { ProjectListService } from '../../service/project-list.service';
import { DonorMasterComponent } from '../../../project-donor/donor-master/donor-master.component';
import { ProgramAreaSectorComponent } from '../program-area-sector/program-area-sector.component';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.scss']
})
export class ProjectDetailComponent implements OnInit {
  //#region "Variables"

  @ViewChild(ProposalComponent) proposalChild: ProposalComponent;
  @ViewChild(BudgetLineListingComponent)
  budgetLineChild: BudgetLineListingComponent;
  @ViewChild(ProjectActivitiesComponent)
  activityChild: ProjectActivitiesComponent;
  @ViewChild(AcceptProposalComponent)
  acceptProposalChild: AcceptProposalComponent;
  @Output() isWinFlag  = new EventEmitter();

  projectJobList: any[] = [];

  currentProjectId: number;
  projectDetail: ProjectDetailModel;
  approvalDetail: ApproveProjectDetailModel;
  winapprovalDetail: WinApprovalDetailModel;
  projectDet: Observable<ProjectDetailModel>;
  service: Subscription;
  assignToBlueclicked = false;
  assignToUsershow = true;
  openProposalcompcheck = false;
  completeProp = true;
  projectId: number;
  assignedTowin = true;
  assignedTowinBlue = false;
  winProjectFlag?: boolean = null;
  isEditingAllowed = false;
  pageId = ApplicationPages.ProjectDetails;
  detailsLoaderFlag = false;
  UserList: UserListModel[] = [];

  // screen scroll
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;

  formdata = new FormData();
  @Output() IswinFlag = new EventEmitter<any>();
  //#endregion

  constructor(
    private routeActive: ActivatedRoute,
    private route: Router,
    public dialog: MatDialog,
    public projectListService: ProjectListService,
    private appurl: AppUrlService,
    private localStorageService: LocalStorageService,
  ) {
    this.getScreenSize();
    this.initProjectDetail();
  }

  ngOnInit() {
    this.GetAllUserList();
    this.initApprovalDetail();
    this.initWinApprovalDetail();

    // this.projectId = +this.routeActive.snapshot.paramMap.get('id');
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });

    this.GetProjectDetail(this.projectId);
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

  isCEFormsubmit(event: any) {
    this.projectDetail.IsCriteriaEvaluationSubmit = event;
  }

  //#region initilize models
  initProjectDetail() {
    this.projectDetail = {
      ProjectId: 0,
      ProjectCode: null,
      ProjectName: null,
      StartData: null,
      EndDate: null,
      ProjectPhaseDetailsId: null,
      IsProposalComplate: false,
      IsApproved: null,
      IsWin: null,
      TotalDaysinHours: null,
      ProjectPhase: null,
      ProjectDescription: null,
      IsCriteriaEvaluationSubmit: false,
      IsProposalSubmit: false,

      ReviewerId: null, // dont use 0
      DirectorId: null // dont use 0
    };
  }

  initApprovalDetail() {
    this.approvalDetail = {
      ProjectId: 0,
      CommentText: null,
      FileName: null,
      FilePath: null,
      IsApproved: false,
      UploadedFile: null
    };
  }

  initWinApprovalDetail() {
    this.winapprovalDetail = {
      ProjectId: 0,
      CommentText: null,
      FileName: null,
      FilePath: null,
      IsWin: null,
      UploadedFile: null
    };
  }

  //#endregion

  //#region open popup for add new donor
  addNewDonor() {
    const dialogRef = this.dialog.open(DonorMasterComponent);
    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region oprnFormModel
  openFormModal(value) {
    const dialogRef = this.dialog.open(ProgramAreaSectorComponent, {
      height: '600px',
      width: '800px',
      data: { 
        id: this.projectId,
        projectName: this.projectDetail.ProjectName,
        description: this.projectDetail.ProjectDescription,
      }
    });
    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region Get project details by  project id
  GetProjectDetail(projectId: number) {
    this.detailsLoaderFlag = true;

    this.projectListService
      .GetProjectDetailsByProjectId(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectListById,
        projectId
      )
      .subscribe(
        data => {
          if (data != null) {
            if (data.data.ProjectDetailModel1 != null) {
              this.projectDetail = data.data.ProjectDetailModel1;
              this.projectDetail.IsCriteriaEvaluationSubmit =
                data.data.ProjectDetailModel1.IsCriteriaEvaluationSubmit == null
                  ? false
                  : data.data.ProjectDetailModel1.IsCriteriaEvaluationSubmit;
              this.projectDetail.IsProposalSubmit =
                data.data.ProjectDetailModel1.IsProposalSubmit == null
                  ? false
                  : data.data.ProjectDetailModel1.IsProposalSubmit;
              this.projectDetail.IsApproved =
                data.data.ProjectDetailModel1.IsApproved;
              if (this.projectDetail.IsApproved === true) {
                this.winProjectFlag = true;
              }
              if (data.data.ProjectDetailModel1.IsWin == null) {
                //  this.winProjectFlag = false;
              }
              if (data.data.ProjectDetailModel1.IsWin) {
                this.projectDetail.IsWin = true;
              }
            }
          }
          this.detailsLoaderFlag = false;
        },
        error => {
          this.detailsLoaderFlag = false;
        }
      );
  }
  //#endregion

  //#region "GetAllUserList"
  GetAllUserList() {
    this.detailsLoaderFlag = true;
    this.UserList = [];
    this.projectListService
      .GetAllUserList()
      .subscribe(response => {
        if (response.data != null) {
          if (response.data.length > 0) {
            response.data.forEach((element: any) => {
              this.UserList.push({
                UserID: element.UserID,
                Username: element.FirstName + ' ' + element.LastName
              });
            });
          }
        }
        this.detailsLoaderFlag = false;
      });
  }
  //#endregion

  //#region to add name of the project
  onNameChange(ev, data: any) {
    if (data != null && data !== '' && data !== undefined) {
      if (ev.srcElement.name === 'projectName') {
        this.projectDetail.ProjectName = data;
        // // Set Menu Header Name
        // this.globalService.setMenuHeaderName(this.setProjectHeader);
      }
      if (ev.srcElement.name === 'projectDescription') {
        this.projectDetail.ProjectDescription = data;
      }
      if (ev.srcElement.name === 'IsProposalComplate') {
        this.projectDetail.IsProposalComplate = data;
      }
      if (ev.srcElement.name === 'reviewerId') {
        this.projectDetail.ReviewerId = data;
      }
      if (ev.srcElement.name === 'directorId') {
        this.projectDetail.DirectorId = data;
      }
      this.projectListService
        .AddProjectDetail(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditProjectDetail,
          this.projectDetail
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
            this.projectDetail.ProjectId = response.CommonId.Id;
            this.GetProjectDetail(this.projectDetail.ProjectId);
          }
        });
    }
  }

  //#endregion

  //#region "backToProject"
  backToProject() {
    this.route.navigate(['/projects']);
    this.projectListService.onShowHideHeader(true);
  }
  //#endregion

  // for destroy service
  // ngOnDestroy(): void {
  //   this.service.unsubscribe();
  // }

  //#region "onReviewerChanged"
  onReviewerChanged(value) {
    this.detailsLoaderFlag = true;
    this.projectDetail.ReviewerId = value;
    this.projectListService
      .AddProjectDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditProjectDetail,
        this.projectDetail
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            this.projectDetail.ProjectId = response.CommonId.Id;
            const projectname = this.projectDetail.ProjectName;
            const projectDes = this.projectDetail.ProjectDescription;
            // this.snackBar.open("Project Details Added Successfully!!!", "action", {
            //   duration: 2000,

            // });
            // this.projectDetail.IsApproved = false;
            this.GetProjectDetail(this.projectDetail.ProjectId);
          }
          this.detailsLoaderFlag = false;
        },
        error => {
          this.detailsLoaderFlag = false;
        }
      );
  }
  //#endregion

  //#region "onDirectorChanged"
  onDirectorChanged(value) {
    this.detailsLoaderFlag = true;
    this.projectDetail.DirectorId = value;
    this.projectListService
      .AddProjectDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditProjectDetail,
        this.projectDetail
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            this.projectDetail.ProjectId = response.CommonId.Id;
            const projectname = this.projectDetail.ProjectName;
            const projectDes = this.projectDetail.ProjectDescription;

            this.GetProjectDetail(this.projectDetail.ProjectId);
          }
          this.detailsLoaderFlag = false;
        },
        error => {
          this.detailsLoaderFlag = false;
        }
      );
  }
  //#endregion

  //#region check proposal hide and show div
  openPropsalComponent() {
    this.openProposalcompcheck = !this.openProposalcompcheck;
    this.winProjectFlag = false;
  }

  openPropsalWinComponent() {
    this.openProposalcompcheck = !this.openProposalcompcheck;
    this.winProjectFlag = true;
  }

  //#endregion

  //#region add approval and reject
  OnapprovelAdd(data) {
    this.acceptProposalChild.commonLoaderFlag = true;
    if (data != null && data !== '' && data !== undefined) {
      if (this.projectDetail.ProjectId > 0) {
        this.approvalDetail.ProjectId = this.projectId;
        this.approvalDetail.CommentText = data.text;
        this.approvalDetail.IsApproved = true;
        // this.approvalDetail.UploadedFile = data.data;
      }
      this.formdata = data.data;
      this.formdata.append('CommentText', data.text);
      this.formdata.append('IsApproved', 'true');
      this.formdata.append('ProjectId', this.projectId.toString());

      this.projectListService
        .uploadReviewFile(
          this.appurl.getApiUrl() + GLOBAL.API_Project_UploadReviewFile,
          this.approvalDetail,
          this.formdata
        )
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
              this.approvalDetail.IsApproved =
                response.data.ApproveProjectDetails.IsApproved;
              this.openProposalcompcheck = false;
              this.GetProjectDetail(this.projectDetail.ProjectId);
            }

            this.acceptProposalChild.commonLoaderFlag = false;
          },
          error => {
            this.acceptProposalChild.commonLoaderFlag = false;
          }
        );
    }
  }
  //#endregion

  //#region  on approval win and loss
  onWinLossApproval(data: IApproveRejectModel) {
    this.acceptProposalChild.commonWinLossFlag = true;
    if (data != null && data !== undefined) {
      if (this.projectDetail.ProjectId > 0) {
        this.winapprovalDetail.ProjectId = this.projectDetail.ProjectId;
        this.winapprovalDetail.CommentText = data.text;
        this.winapprovalDetail.IsWin = data.flag;

        this.winapprovalDetail.UploadedFile = data.data;
      }
      this.formdata = data.data;
      this.formdata.append('CommentText', data.text);
      if (this.winapprovalDetail.IsWin === true) {
        this.formdata.append('IsWin', 'true');
      } else if (this.winapprovalDetail.IsWin === false) {
        this.formdata.append('IsWin', 'false');
      }
      // this.commonLoader.showLoader();

      this.projectListService
        .uploadFinalizeFile(
          this.appurl.getApiUrl() + GLOBAL.API_Project_UploadFinalizeFile,
          this.winapprovalDetail,
          this.formdata
        )
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
              // this.snackBar.open("Project approved  Successfully!!!", "action", {
              // duration: 2000,
              this.winapprovalDetail.IsWin =
                response.data.WinProjectDetails.IsWin;
              this.openProposalcompcheck = false;
              this.isWinFlag.emit(response.data.WinProjectDetails.IsWin);

              this.GetProjectDetail(this.projectDetail.ProjectId);
              // if (this.winapprovalDetail.IsWin == true) {
              //   this.assignedTowin = true;
              //   this.assignedTowinBlue = false;
              //   this.assignToUsershow = true;
              //   //  this.assignToBlueclicked = true;
              //   this.openProposalcompcheck = false;
              // }
              // else if (this.winapprovalDetail.IsWin == false) {
              //   this.openProposalcompcheck = false;
              // }
              // if (projectname != null && projectDes != null) {
              // this.router.navigate(['projects']);

              // }
            }
            // this.IswinFlag.emit(this.projectDetail.IsWin);
            this.acceptProposalChild.commonWinLossFlag = false;
            // this.commonLoader.hideLoader();
          },
          error => {
            // this.commonLoader.hideLoader();
            this.acceptProposalChild.commonLoaderFlag = false;
          }
        );
    }
  }

  //#endregion

  //#region "proposalRejected"
  proposalRejected(data: any) {
    if (data != null && data !== '' && data !== undefined) {
      if (this.projectId > 0) {
        this.acceptProposalChild.commonLoaderFlag = true;

        this.approvalDetail.ProjectId = this.projectId;
        this.approvalDetail.CommentText = data.text;
        this.approvalDetail.IsApproved = false;
        this.approvalDetail.UploadedFile = data.data;
        this.formdata = data.data;
        this.formdata.append('CommentText', data.text);
        this.formdata.append('IsApproved', 'false');
        this.projectListService
          .uploadReviewFile(
            this.appurl.getApiUrl() + GLOBAL.API_Project_UploadReviewFile,
            this.approvalDetail,
            this.formdata
          )
          .subscribe(
            response => {
              if (response.StatusCode === 200) {
                this.GetProjectDetail(this.projectDetail.ProjectId);
                this.approvalDetail.IsApproved =
                  response.data.ApproveProjectDetails.IsApproved;
                this.openProposalcompcheck = false;
                // this.proposalChild.GetProposal(this.projectId);
                this.proposalChild.IsApproved = this.approvalDetail.IsApproved;
              }

              this.acceptProposalChild.commonLoaderFlag = false;
            },
            error => {
              this.acceptProposalChild.commonLoaderFlag = false;
            }
          );
      }
    }
  }
  //#endregion

  //#region "EMIT"
  proposalApprovedChange(e) {
    this.GetProjectDetail(e);
  }
  //#endregion

  //#region "onAcceptedApprovalEmit"
  onAcceptedApprovalEmit(data: IApproveRejectModel) {
    this.OnapprovelAdd(data);
  }
  //#endregion

  //#region "onRejectedApprovalEmit"
  onRejectedApprovalEmit(data: IApproveRejectModel) {
    this.proposalRejected(data);
  }
  //#endregion

  //#region "Validations"

  //#region "checkDirectorApproved"
  checkProjectApprovedForWinLoass(): boolean {
    if (
      this.projectDetail.IsApproved != null &&
      this.projectDetail.IsApproved === true &&
      this.projectDetail.IsWin == null &&
      this.checkProposalApproved()
    ) {
      return true;
    } else {
      return false;
    }
  }
  //#endregion

  //#region "checkProposalApproved"
  checkProposalApproved(): boolean {
    if (
      this.projectDetail.IsCriteriaEvaluationSubmit === true &&
      this.projectDetail.IsProposalSubmit === true
    ) {
      return true;
    } else {
      return false;
    }
  }
  //#endregion

  //#region "approvedRejectProjectCheck"
  approvedRejectProjectFalse() {
    if (
      this.projectDetail.ReviewerId == null ||
      (this.projectDetail.IsCriteriaEvaluationSubmit === false &&
        this.projectDetail.IsProposalSubmit === false)
    ) {
      return true;
    }
    return false;
  }
  //#endregion

  //#region "approvedRejectCheck"
  approvedRejectCheck() {
    if (
      this.projectDetail.IsCriteriaEvaluationSubmit === true &&
      this.projectDetail.IsProposalSubmit === true
    ) {
      return true;
    } else {
      // this.projectDetail.IsProposalSubmit = false;
      return false;
    }
  }
  //#endregion

  //#region  "projectJobListRefreshEmit"
  projectJobListRefreshEmit(data: any) {
    this.budgetLineChild.getProjectJobList(this.projectId);
  }
  //#endregion

  //#region  "projectBudgetLineListEmit"
  projectBudgetLineListEmit() {
    this.activityChild.refreshBudgetLine();
  }
  //#endregion

  //#endregion
}
