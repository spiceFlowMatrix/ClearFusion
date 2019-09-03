import { Component, OnInit, OnDestroy, Output, EventEmitter } from '@angular/core';
import {
  FileTypes,
  ProposalFileDetailsModel
} from '../../project-details/models/project-details.model';
import { ToastrService } from 'ngx-toastr';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ActivatedRoute } from '@angular/router';
import { ProjectListService } from '../../service/project-list.service';
import { GLOBAL } from 'src/app/shared/global';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-proposal-document',
  templateUrl: './proposal-document.component.html',
  styleUrls: ['./proposal-document.component.scss']
})
export class ProposalDocumentComponent implements OnInit, OnDestroy {
  //#region "variables"

  @Output() isProposalDocumentAvailable = new EventEmitter<boolean>();
  @Output() changeStartDate = new EventEmitter<any>();

  projectId: number;
  selectedFileType: any;
  docUrl: string;
  fileToUpload: File = null;
  fileDetailsModel: ProposalFileDetailsModel[] = [];

  proposalFileTypes: FileTypes[] = [
    { Id: 1, Name: 'Proposal' },
    { Id: 2, Name: 'EOI' },
    { Id: 3, Name: 'Budget' },
    { Id: 4, Name: 'Concept' },
    { Id: 5, Name: 'Presentation' }
  ];

  // flag
  documentFileLoader = false;
  documentListingLoader = false;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  //#endregion

  constructor(
    private routeActive: ActivatedRoute,
    private appurl: AppUrlService,
    private projectListService: ProjectListService,
    private toastr: ToastrService
  ) {
    this.docUrl = this.appurl.getDocUrl();
  }

  ngOnInit() {
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });

    this.selectedFileType = this.proposalFileTypes[0].Id;
    this.getProposalDocuments(this.projectId);
  }

  public getProposalDocuments(projectid: number) {
    this.documentListingLoader = true;
    this.fileDetailsModel = [];
    this.projectListService
      .GetProjectproposalDocumentById(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_GetProjectproposalDocumnetsById,
        projectid
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        response => {
          if (response != null) {
            if (response.StatusCode === 200) {
              if (response.data.ProjectProposalModelList != null) {
                if (response.data.ProjectProposalModelList.length > 0) {
                  response.data.ProjectProposalModelList.forEach(element => {
                    this.fileDetailsModel.push({
                      ProjectProposaldetailId: element.ProjectProposaldetailId,
                      ProposalDocumentName: element.ProposalDocumentName,
                      ProjectId: element.ProjectId,
                      ProposalWebLink: element.ProposalWebLink,
                      ProposalExtType: element.ProposalExtType,
                      UserName: element.UserName,
                      CreatedDate: StaticUtilities.setLocalDate(element.CreatedDate),
                      ProposalDocumentType: this.getFileTypeName(
                        element.ProposalDocumentTypeId
                      )
                    });
                  });
                  this.isProposalDocumentAvailable.emit(true);
                }
              }
            }
          }
          this.documentListingLoader = false;
        },
        error => {
          this.documentListingLoader = false;
        }
      );
  }

  onGetProposalDoc(objectName: string) {
    const objectData = {
      ObjectName: objectName
    };
    this.projectListService
      .GetSignedUrl(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DownloadFileFromBucket,
        objectData
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        data => {
          if (data != null) {
            if (data.StatusCode === 200 && data.data.SignedUrl != null) {
              window.open(data.data.SignedUrl, '_blank');
            }
          }
        },
        error => {}
      );
  }

  uploadProposalFile(files: FileList) {
    this.fileToUpload = files.item(0);
    const selectedFileType = this.selectedFileType;
    this.uploadProposalDocument(this.fileToUpload, selectedFileType);
  }

  uploadProposalDocument(fileToUpload: File, selectedFileType: any) {
    this.documentFileLoader = true;
    const formData = new FormData();

    formData.append('filesData', fileToUpload, fileToUpload.name);
    formData.append('projectId', this.projectId.toString());
    formData.append('data', selectedFileType);

    const data = this.fileDetailsModel.find(
      x => x.ProposalDocumentName === fileToUpload.name
    );

    // Check if file name already exist
    if (data == null) {
      this.projectListService
        .uploadEDIFile(
          this.appurl.getApiUrl() +
            GLOBAL.API_Project_StartProposalDragAndDropFile,
          this.projectId,
          formData
        )
        .pipe(takeUntil(this.destroyed$))
        .subscribe(
          res => {
            if (res.StatusCode === 200) {
              if (res.data.ProjectProposalDocumentModel != null) {
                // Step 1
                // TODO: Trigger/emit/output-binding to update the start date
                // Add your code here

                // Step 2
                // Add to list
                // const responseData = res.data.ProjectProposalDocumentModel;
                // var createdDate = StaticUtilities.setLocalDate(responseData.CreatedDate);
                // this.fileDetailsModel.unshift({
                //   ProjectProposaldetailId: responseData.ProjectProposaldetailId,
                //   ProposalDocumentName: responseData.ProposalDocumentName,
                //   ProjectId: responseData.ProjectId,
                //   ProposalWebLink: responseData.ProposalWebLink,
                //   ProposalExtType: responseData.ProposalExtType,
                //   UserName: responseData.UserName,
                //   CreatedDate: createdDate,
                //   ProposalDocumentType: this.getFileTypeName(
                //     responseData.ProposalDocumentTypeId
                //   )
                // });
                 this.getProposalDocuments(this.projectId);
                if (this.fileDetailsModel.length === 1) {
                  this.onFirstFileUploaded();
                }

              }
            } else if (res.StatusCode === 400) {
              this.toastr.warning(res.Message);
            }

            this.documentFileLoader = false;
          },
          error => {
            this.documentFileLoader = false;
            this.toastr.warning('Some error occured. Try Again');
          }
        );
    } else {
      this.toastr.warning('File name already exist');
      this.documentFileLoader = false;
    }
  }

  private getFileTypeName(id: number): string {
    if (this.proposalFileTypes.find(x => x.Id === id) != null) {
      return this.proposalFileTypes.find(x => x.Id === id).Name;
    } else {
      return '';
    }
  }


  private onFirstFileUploaded(): void {
    // change flag for proposal document availability
    this.isProposalDocumentAvailable.emit(true);
    this.changeStartDate.emit();
  }


  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
