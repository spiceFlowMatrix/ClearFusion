import { Component, OnInit, Input, Inject } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { takeUntil } from 'rxjs/operators';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { FileSourceEntityTypes } from 'src/app/shared/enum';

@Component({
  selector: 'lib-document-listing',
  templateUrl: './document-listing.component.html',
  styleUrls: ['./document-listing.component.css']
})
export class DocumentListingComponent implements OnInit {

  // @Input() recordId;
  // @Input() pageId;

  pageTitle = 'Documents';
  documentListLoader = false;

  documentList: IDocumentListModel[] = [];

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<DocumentListingComponent>
  ) { }

  ngOnInit() {
    this.getDocumentList();
    this.setPageTitle();
  }

  //#region "getDocumentList"
  getDocumentList() {
    this.documentListLoader = true;

    const documentData = {
      RecordId: this.data.recordId,
      PageId: this.data.pageId
    };

     this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_FileManagement_GetDocumentFiles, documentData)
    .pipe(takeUntil(this.destroyed$))
      .subscribe(response => {
        this.documentList = [];
        if (response.StatusCode = 200 && response.data.DocumentFileList != null) {
          this.documentListLoader = false;
          response.data.DocumentFileList.forEach(element => {
            this.documentList.push({
              DocumentName: element.FileName,
              DocumentSignedURL: element.FileSignedURL,
              DocumentFileId: element.DocumentFileId,
              IsDeleted: false
            });
          });
        }
        this.documentListLoader = false;
      });
  }
//#endregion

onDelete(item: IDocumentListModel) {

  item.IsDeleted = true;

  if (item.DocumentFileId !== undefined) {

    const DocumentData = {
      DocumentFileId: item.DocumentFileId,
      PageId: this.data.pageId
    };

    this.globalService
    .post(this.appurl.getApiUrl() + GLOBAL.API_FileManagement_DeleteDocumentFiles, DocumentData)
    .pipe(takeUntil(this.destroyed$))
      .subscribe(response => {
        if (response.StatusCode = 200) {
          const index = this.documentList.findIndex(x => x.DocumentFileId === item.DocumentFileId);
          this.documentList.splice(index, 1);
        }
      });
  }
}

setPageTitle() {
  switch (this.data.pageId) {
    case FileSourceEntityTypes.Voucher:
    this.pageTitle = 'Voucher ' + this.pageTitle;
    break;
  }
}

ngOnDestroy() {
  this.destroyed$.next(true);
  this.destroyed$.complete();
}


}

export interface IDocumentListModel {
DocumentName: string;
DocumentSignedURL: string;
DocumentFileId: number;
IsDeleted?: boolean;
}

export interface IDocumentListingDataSource {
  RecordId: number;
  PageId: number;
}
