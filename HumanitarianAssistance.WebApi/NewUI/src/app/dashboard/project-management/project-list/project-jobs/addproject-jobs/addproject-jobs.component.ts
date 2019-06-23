import {
  Component,
  OnInit,
  Inject,
  EventEmitter,
  Input
} from '@angular/core';
import { IProjectJobModel } from '../project-jobsmodel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { ProjectJobsService } from '../project-jobs.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Component({
  selector: 'app-addproject-jobs',
  templateUrl: './addproject-jobs.component.html',
  styleUrls: ['./addproject-jobs.component.scss']
})
export class AddprojectJobsComponent implements OnInit {
  //#region input /output
  @Input() projectId: number;
  //#endregion
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DataSources,
    public dialogRef: MatDialogRef<AddprojectJobsComponent>,
    public fb: FormBuilder,
    public projectJobsService: ProjectJobsService,
    public toastr: ToastrService
  ) {}
  addProjectJobsForm: FormGroup;
  addProjectJobsLoader = false;
  onListRefresh = new EventEmitter();
  ngOnInit() {
    this.initForm();
  }

  //#region "initForm"
  initForm() {
    this.addProjectJobsForm = this.fb.group({
      ProjectJobName: ['', [Validators.required]]
    });
  }
  //#endregion

  //#region "onAdd"
  onAddProjectJobs(data): void {
    const projectJobsData: IProjectJobModel = {
      ProjectJobId: data.value.ProjectJobId,
      ProjectJobCode: data.value.ProjectJobCode,
      ProjectJobName: data.value.ProjectJobName,
      ProjectId: this.data.projectId
    };
    this.addProjectJobs(projectJobsData);
  }
  //#endregion

  //#region "onAddProjectJobs"
  addProjectJobs(data: IProjectJobModel) {
    if (this.addProjectJobsForm.valid) {
      this.addProjectJobsLoader = true;
      const projectJobsData: IProjectJobModel = {
        ProjectJobId: data.ProjectJobId,
        ProjectJobCode: data.ProjectJobCode,
        ProjectJobName: data.ProjectJobName,
        ProjectId: data.ProjectId
      };

      this.projectJobsService.AddProjectJobsDetail(projectJobsData).subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            this.onCancelPopup();
            this.projectJobsListRefresh();
            this.toastr.success('Project Jobs is created successfully');
          } else {
            this.toastr.error(response.message);
          }
          this.addProjectJobsLoader = false;
        },
        error => {
          this.toastr.error('Someting went wrong');
          this.addProjectJobsLoader = false;
        }
      );
    }
  }
  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  //#region "onListRefresh"
  projectJobsListRefresh() {
    this.onListRefresh.emit();
  }
  //#endregion
}
class DataSources {
  data: any;
  projectList: any;
  projectJobList: any;
  projectId: any;
}
