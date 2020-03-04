import { Injectable } from '@angular/core';
import { GlobalService } from './global-services.service';
import { AppUrlService } from './app-url.service';
import { GLOBAL } from '../global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { Subject } from 'rxjs/internal/Subject';
import { IMenuList } from '../dbheader/dbheader.component';
import { FileModel } from '../file-management/file-management-model';
import { SignedUrlObjectName } from '../file-management/signed-url-object-name';
import { concatMap, catchError, tap } from 'rxjs/operators';
import { Observable, of, throwError } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { FileSourceEntityTypes } from '../enum';
import { HttpErrorResponse } from '@angular/common/http';
import 'rxjs/add/operator/catch';

@Injectable({
  providedIn: 'root'
})
export class GlobalSharedService {
  // Global Header Flag
  setSelectedHeader: number;

  private menuHeaderNameSubject = new Subject<string>();
  menuHeaderNameState = this.menuHeaderNameSubject.asObservable();

  private menuListSubject = new Subject<IMenuList[]>();
  menuListState = this.menuListSubject.asObservable();

  constructor(
    private appurl: AppUrlService,
    private globalService: GlobalService
  ) {}

  //#region "GetTrainingLink"
  GetTrainingLink() {
    return this.globalService
      .getList(this.appurl.getApiUrl() + GLOBAL.API_Dashboard_GetTrainingLink)
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.TrainingLink,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  setMenuHeaderName(data: string) {
    this.menuHeaderNameSubject.next(data);
  }

  getMenuHeaderName() {
    return this.menuHeaderNameSubject;
  }

  setMenuList(data: IMenuList[]) {
    this.menuListSubject.next(data);
  }

  getMenuList() {
    return this.menuListSubject;
  }

  //#region "uploadFileBySignedUrl"
  uploadFileBySignedUrl(url: string, data: any, options?: any) {
    return this.globalService
      .put(url, data, { observe: 'response', 'Content-Type': data.type })
      .pipe(
        map(x => {
          // console.log(x.status);
          const responseData: IResponseData = {
            data: null,
            statusCode: x.status,
            message: 'OK'
          };
          return x;
        })
      );
  }
  //#endregion

  //#region "uploadFile"
  uploadFile(pageId: number, entityId: any, file: any, documentTypeId: any = null, projectModel: any = null) {
    let objectName = SignedUrlObjectName.getSignedURLObjectName(pageId, entityId);

    if (objectName == null && objectName === '' && objectName === undefined) {
      throw new Error('object name cannot be empty');
    }

    objectName = objectName + file.name;

    const DownloadObjectGCBucketModel = {
      ObjectName: objectName,
      FileName: file.name,
      FileType: file.type
    };

    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_FileManagement_GetSignedURL,
        DownloadObjectGCBucketModel
      )
      .pipe(
       concatMap(res => {
         // console.log('res', res);
        const responseData: IResponseData = {
          data: res.data.SignedUrl,
          statusCode: res.StatusCode,
          message: res.Message
        };
        if (responseData.statusCode === 200) {
          return this.uploadFileBySignedUrl(responseData.data, file);
        } else {
          throw new Error('Could not get signed URL');
        }
      }), concatMap(res1 => {
        // console.log('res1', res1);
          if (res1['status'] === 200) {
            if (pageId === FileSourceEntityTypes.ProjectProposal) {
              projectModel.FilePath = objectName;
              projectModel.FileName = file.name;
              projectModel.Extension = file.type;

              return this.saveUploadedProjectFileInfo(projectModel);

            } else {
              const data: FileModel = {
                FileName: file.name,
                FilePath: objectName,
                FileSize: file.size,
                FileType: file.type,
                PageId: pageId,
                RecordId: entityId,
                DocumentTypeId: documentTypeId
              };
              return this.saveUploadedFileInfo(data);
            }
       } else {
        throw new Error('Could not upload file');
       }
      })
      );
  }

  //#endregion

  //#region "saveUploadedFileInfo"
  saveUploadedFileInfo(data: FileModel) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_FileManagement_SaveUploadedFileInfo,
        data
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.data.SignedUrl,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }
  //#endregion

  //#region "saveUploadedFileInfo"
  saveUploadedProjectFileInfo(data: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddProjectProposalFileDetail,
        data
      );
  }
  //#endregion

  //#region "getFile"
  getFile(url: string, data: any) {
    // for pdf use this options to avoid 406 (Not Acceptable)
    const options = { responseType: 'blob', observe: 'response' };

    return this.globalService
      .post(
        url,
        data,
        options
      )
      .pipe(
        map(event => {
          // get filename from header
          const contentDisposition = event.headers.get('Content-Disposition');
          const exmessage = event.headers.get('ExMessage');

          if (exmessage) {
           return this.handleDownloadPdfError(exmessage);
          }
          const filename = contentDisposition
            .split(';')[1]
            .split('filename')[1]
            .split('=')[1]
            .trim();

          // convert it to file
          const file = new Blob([event.body], { type: event.body.type });

          // force to download file
          const blobURL = window.URL.createObjectURL(file);
          const anchor = document.createElement('a');
          anchor.download = filename; // filename is important to set on backend
          anchor.href = blobURL;
          anchor.click();

          return event;
        }),
        catchError(this.handleError)
      );
  }
  //#endregion

  //#region "RandomNum" NOTE: Use for Add functionality
  RandomNum() {
    return Math.floor(Math.random() * 10000);
  }
  //#endregion

  //#region "deleteFile"
  deleteFile(model: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() + GLOBAL.API_FileManagement_DeleteDocumentFiles, model
      );
  }

  getPreviousYearsList(years: number): Observable<IDropDownModel[]> {

    const yearDropDown: IDropDownModel[] = [];
    const year = new Date().getFullYear();
    for (let i = 0; i <= years; i++) {
      yearDropDown.push({name: (year - i).toString(),
      value: year - i});
    }
    return of(yearDropDown);
  }
  handleError(error) {
    return throwError(error);
  }

  private handleDownloadPdfError(error: any) {
    return {
      error: true,
      message: error
    };
}
}
