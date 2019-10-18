import {
  Component,
  OnInit,
  Input,
  EventEmitter,
  Output
} from '@angular/core';
import { IProjectJobModel } from '../project-jobsmodel';
import { ProjectJobsService } from '../project-jobs.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-project-jobs-details',
  templateUrl: './project-jobs-details.component.html',
  styleUrls: ['./project-jobs-details.component.scss']
})
export class ProjectJobsDetailsComponent implements OnInit {
  [x: string]: any;

  //#region "variables"
  @Input() projectJobId: number;
  @Input() projectJobList: IProjectJobModel;
  @Input() projectJobDetailList: IProjectJobModel;
  @Input() projectId: any;


  @Output() deleteProjectJob = new EventEmitter<any>();

  @Output() projectJobsDetailChanged = new EventEmitter<IProjectJobModel>();

  // @Input() projectId: number;
  projectJobsDetail: IProjectJobModel;
  archiveButton = false;
  // "projectJobsDetailLoader"
  projectJobsDetailLoader = false;
  editProjectJobsDetailLoader = false;

  //#endregion

  constructor(
    public dialog: MatDialog,
    private projectJobsService: ProjectJobsService,
    private toastr: ToastrService,
    private appurl: AppUrlService,
    private projectService: ProjectJobsService
  ) {}

  ngOnInit() {
     //this.projectId = +this.routeActive.snapshot.paramMap.get('id');
  }

  ngOnChanges(): void {
    this.initForm();
    if (
      this.projectJobId !== 0 &&
      this.projectJobId !== null &&
      this.projectJobId !== undefined
    ) {
      this.archiveButton = true;
      this.getProjectJobDetailsById(this.projectJobId);
    } else {
      this.archiveButton = false;
    }
  }

  //#region "initForm"
  initForm() {
    this.projectJobsDetail = {
      ProjectJobId: null,
      ProjectJobCode: null,
      ProjectJobName: null,
      ProjectId: null
    };
  }

  //#region "getProjectDetailByProjectId"
  getProjectJobDetailsById(projectJobId: number) {
    this.projectJobsDetailLoader = true;

    this.projectJobsService
      .GetProjectJobDetailByProjectJobId(projectJobId)
      .subscribe(
        (response: IResponseData) => {
          if (
            response.statusCode === 200 &&
            response.data !== null &&
            response.data !== undefined
          ) {
            response.data.forEach((element: IProjectJobModel) => {
              this.projectJobsDetail = {
                ProjectJobCode: element.ProjectJobCode,
                ProjectJobId: element.ProjectJobId,
                ProjectJobName: element.ProjectJobName,
                ProjectId: element.ProjectId
              };
            });
          } else if (response.statusCode === 400 && response.data === null) {
            this.toastr.warning(response.message);
          }
          this.projectJobsDetailLoader = false;
        },
        error => {
          this.projectJobsDetailLoader = false;
        }
      );
  }

  //#endregion

  //#region "onBudgetLineValuechange"
  onProjectJobsValuechange() {
    this.editProjectJobsDetailById(this.projectJobsDetail);
  }
  //#endregion

  //#region "editProjectJobsDetailById"
  editProjectJobsDetailById(data: IProjectJobModel) {
    this.editProjectJobsDetailLoader = true;
    this.projectJobsService.EditProjectJobsDetailById(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.projectJobsDetailChanged.emit(data);
        } else if (response.statusCode === 400) {
          this.toastr.warning(response.message);
        }
        this.editProjectJobsDetailLoader = false;
      },
      error => {
        this.toastr.error('Someting went wrong');
        this.editProjectJobsDetailLoader = false;
      }
    );
  }
  //#endregion

  DeleteProjectJob(id) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });

    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText = Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText = Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });

    dialogRef.componentInstance.confirmDelete.subscribe(res => {
      console.log(res);
      dialogRef.componentInstance.isLoading = true;
      // this.phaseDetailsForm.disable();

      this.projectService
        .Delete(
          this.appurl.getApiUrl() + GLOBAL.API_Project_DeleteProjectJob,
          id
        )
        .subscribe(
          result => {
            if (result.StatusCode === 200) {
              //  this.phaseDetailsForm.enable();
              this.toastr.success(result.Message);
              dialogRef.componentInstance.onCancelPopup();
              this.deleteProjectJob.emit({ id: id });
              this.projectJobsDetail = {
                ProjectJobId: null,
                ProjectJobCode: null,
                ProjectJobName: null,
                ProjectId: null
              };
              this.projectJobId = 0;
            } else {
              // this.phaseDetailsForm.enable();
              this.toastr.error(result.Message);
            }
            dialogRef.componentInstance.isLoading = false;
          },
          error => {
            // this.phaseDetailsForm.enable();
            dialogRef.componentInstance.isLoading = false;
            this.toastr.error('Some error occured. Please try again later');
          }
        );
    });
  }
}
