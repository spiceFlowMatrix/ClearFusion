import {
  Component,
  OnInit,
  Inject,
  EventEmitter,
  Input,
  OnDestroy
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import {
  IProjectActivityDocumentModel,
  IDocumentsModel
} from '../models/project-activities.model';
import { FileSystemFileEntry, UploadEvent, UploadFile } from 'ngx-file-drop';
import { Subscription } from 'rxjs/internal/Subscription';
import { ProjectActivitiesService } from '../service/project-activities.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-project-activity-documents',
  templateUrl: './project-activity-documents.component.html',
  styleUrls: ['./project-activity-documents.component.scss']
})
export class ProjectActivityDocumentsComponent implements OnInit, OnDestroy {
  headerText = '';

  documentsList: IDocumentsModel[] = [];
  // documentListRefresh = new EventEmitter<any>();
  // documentDownload = new EventEmitter<any>();

  projectPhaseId: number;
  projectActivityId: number;
  documentPath: any;
  documentName = '';
  monitoringId = 0;
  loading = false;

  uploadDocumentFlag = false;
  uploadActivitySubscribe: Subscription;

  constructor(
    public dialogRef: MatDialogRef<ProjectActivityDocumentsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IProjectActivityDocumentModel,
    public toastr: ToastrService,
    public activitiesService: ProjectActivitiesService,
    private http: HttpClient,
    private appurl: AppUrlService,
    private commonLoader: CommonLoaderService
  ) {
    this.headerText = data.HeaderText;
    // this.documentsList = data.DocumentModel;
    this.projectPhaseId = data.ProjectPhaseId;
    this.projectActivityId = data.ProjectActivityId;
    this.monitoringId = data.MonitoringId;
  }

  ngOnInit() {
    if (this.monitoringId !== undefined && this.monitoringId !== 0 && this.monitoringId !== null) {
      this.getActivityDocumentDetail(this.projectActivityId, this.monitoringId, 0);
    } else {
      this.getActivityDocumentDetail(this.projectActivityId, 0, this.projectPhaseId);
    }
  }

  //#region "onDeleteDocument"
  onDeleteDocument(item: IDocumentsModel): any {
    this.deleteDocument(item);
  }
  //#endregion

  //#region "deleteDocument"
  deleteDocument(item: IDocumentsModel) {
    item.IsLoading = true;
    this.activitiesService
      .DeleteProjectActivityDocument(item.ActtivityDocumentId)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            const index = this.documentsList.indexOf(item);
            this.documentsList.splice(index, 1);
            item.IsLoading = false;
          } else {
            item.IsLoading = false;
            item.IsError = true;
          }
        },
        error => {
          item.IsLoading = false;
          item.IsError = true;
        }
      );
  }
  //#endregion

  //#region "uploadActivityDocument"
  uploadActivityDocument(data: any) {
    this.commonLoader.showLoader();
    this.uploadActivitySubscribe = this.activitiesService
      .UploadProjectActivityDocument(data)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data != null) {
            // this.documentListRefresh.emit(response.data);
            if (this.monitoringId !== undefined && this.monitoringId !== 0 && this.monitoringId !== null) {
              this.getActivityDocumentDetail(this.projectActivityId, this.monitoringId, 0);
            } else {
              this.getActivityDocumentDetail(this.projectActivityId, 0, this.projectPhaseId);
            }
            // this.projectActivityListRefresh();
            this.toastr.success('Document uploaded successfully');
          } else {
            this.toastr.error(response.message);
          }
          this.commonLoader.hideLoader();
          this.uploadDocumentFlag = false;
          this.documentName = '';
        },
        error => {
          this.toastr.error('Someting went wrong');
          this.commonLoader.hideLoader();
          this.uploadDocumentFlag = false;
          this.documentName = '';

        }
      );
  }
  //#endregion

  upload(files) {
    if (files.length !== 0) {
      this.documentName = '';
      const formData = new FormData();

      for (const file of files) {
        this.documentName = file.name;
        formData.append(file.name, file);
        formData.append(this.projectActivityId.toString(), file.name, file);
        // formData.append('activityId', '12');
      }

      this.uploadActivityDocument(formData);
    }
  }

  public onFileDropped(event: UploadEvent) {
    this.documentName = '';
    for (const droppedFile of event.files) {
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;

        fileEntry.file((file: File) => {
          // Here you can access the real file
          //  console.log(droppedFile.relativePath, file);

          this.documentName = droppedFile.relativePath;
          const formData = new FormData();
          // formData.append(this.projectActivityId.toString(), file, droppedFile.relativePath);
          formData.append('filesData', file, droppedFile.relativePath);
          formData.append('activityId', this.projectActivityId.toString());
          formData.append('statusId', this.projectPhaseId.toString());
          formData.append('monitoringId', this.monitoringId === undefined ? '0' : this.monitoringId.toString());
          this.uploadActivityDocument(formData);
        });
      }
    }
  }
  //#endregion

  //#region "fileOver"
  onFileOver(event: any) { }
  //#endregion

  //#region "fileLeave"
  onFileLeave(event: any) { }
  //#endregion

  getActivityDocumentDetail(activityId: number, monitoringId: number, phaseId: number) {
    let model: any = {
      ActivityId: 0,
      MonitoringId: 0,
      ProjectPhaseId: 0
    };

    this.loading = true;

    model.ActivityId = activityId;
    model.MonitoringId = monitoringId;
    model.ProjectPhaseId = phaseId;
    this.activitiesService
      .GetActivityDocumentDetail(model)
      .subscribe(
        (response: IResponseData) => {

          if (response.statusCode === 200 && response.data != null) {
            this.documentsList = response.data.data.ActivityDocumentDetailModel;
            this.loading = false;
          } else {
            this.toastr.error(response.message);
            this.loading = false;
          }
        },
        error => {
          this.toastr.error('Someting went wrong');
          this.loading = false;
        }
      );
  }

  //   //#region "documentDownload"
documentDownload(objectName: string) {
  const objectData = {
    ObjectName: objectName
  };
  this.activitiesService.GetSignedUrl(objectData).subscribe(
    (data: IResponseData) => {
      if (data != null) {
        if (data.statusCode === 200 && data.data != null) {
          window.open(data.data, '_blank');
        } else if (data.statusCode === 200) {
          this.toastr.error(data.message);
        }
      }
    },
    error => {}
  );
}
//#endregion

  //#region "onDocumentClicked"
  onDocumentClicked(filePath: string) {
    this.documentDownload(filePath);
  }
  //#endregion


  //#region "ngOnDestroy"
  ngOnDestroy() {
    if (this.uploadActivitySubscribe && !this.uploadActivitySubscribe.closed) {
      this.uploadActivitySubscribe.unsubscribe();
    }
  }
  //#endregion
}
