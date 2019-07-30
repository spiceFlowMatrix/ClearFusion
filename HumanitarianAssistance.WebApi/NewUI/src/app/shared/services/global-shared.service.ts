import { Injectable } from '@angular/core';
import { GlobalService } from './global-services.service';
import { AppUrlService } from './app-url.service';
import { GLOBAL } from '../global';
import { map } from 'rxjs/internal/operators/map';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { Subject } from 'rxjs/internal/Subject';
import { IMenuList } from '../dbheader/dbheader.component';
import { FileManagementModel, FileModel } from '../file-management/file-management-model';
import { SignedUrlObjectName } from '../file-management/signed-url-object-name';
import { HttpResponse } from '@angular/common/http';


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
  ) { }

  //#region "GetTrainingLink"
  GetTrainingLink() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Dashboard_GetTrainingLink
    ).pipe(
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
      .put(url, data, { observe: 'response' })
      .pipe(
        map(x => {
          console.log(x.status);
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
  uploadFile(pageId: number, entityId: number, file: any) {
    let objectName = SignedUrlObjectName.getSignedURLObjectName(pageId);

    if (objectName == null && objectName === '' && objectName === undefined) {
      throw new Error('object name cannot be empty');
    }

    objectName = objectName + file.name;

    const DownloadObjectGCBucketModel = {
      ObjectName: objectName,
      FileName: file.name
    };

     return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_FileManagement_GetSignedURL, DownloadObjectGCBucketModel)
      .pipe(
        // switchMap(s => { return s}),
        // switchMap(s => { return s}),
        // switchMap(s => { return s}),
        map(x => {
          const responseData: IResponseData = {
            data: x.data.SignedUrl,
            statusCode: x.StatusCode,
            message: x.Message
          };

          if (responseData.statusCode === 200) {
            this.uploadFileBySignedUrl(responseData.data, file)
              .pipe().subscribe((response: any) => {
                if (response.status === 200) {

                  const data: FileModel = {
                    FileName: file.name,
                    FilePath: objectName,
                    FileSize: file.size,
                    FileType: file.type,
                    PageId: pageId,
                    RecordId: entityId
                  };

                   return this.saveUploadedFileInfo(data)
                    .pipe().subscribe((response: any) => {
                      const responseData: IResponseData = {
                        data: x.data.SignedUrl,
                        statusCode: x.StatusCode,
                        message: x.Message
                      };
                    });
                }
              });
          } else {
            throw new Error('Could not get signed URL');
          }
        }));
  }
  ////#endregion


  //#region "saveUploadedFileInfo"
  saveUploadedFileInfo(data: FileModel) {
    return this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_FileManagement_SaveUploadedFileInfo, data)
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
}
