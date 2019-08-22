import { SignedUrlObjectName } from './SignedUrlObjectName';
import { AppSettingsService } from '../../service/app-settings.service';
import {
    Http,
    Headers,
    Response,
    RequestOptions,
    RequestOptionsArgs
  } from '@angular/http';
import { GLOBAL } from '../global';
import { FileModel } from './file-management-model';
import { Observable } from 'rxjs/Observable';
import { RequestStatus } from '../enums';
import { Injectable } from '@angular/core';

@Injectable()
export class FileManagementService {
    constructor(
        private settings: AppSettingsService,
        private http: Http) {
    }

    PostByModel(url: string, model: any) {
        debugger;
        const Myheaders = new Headers();
        Myheaders.append(
          'Authorization',
          'Bearer ' + localStorage.getItem('authenticationtoken')
        );
        Myheaders.append('Content-Type', 'application/json');
        const options = new RequestOptions({ headers: Myheaders });
        return this.http
          .post(url, JSON.stringify(model), options)
          .map((response: Response) => {
            const journal = response.json();
            if (journal) {
              return journal;
            }
          })
          .catch(this.handleError);
      }

    PutByModel(url: string, model: any) {
        debugger;
        const Myheaders = new Headers();
          Myheaders.append('Content-Type', model.type);
        const options = new RequestOptions({ headers: Myheaders, });
        return this.http
          .put(url, model, options)
          .map((response: Response) => {
              debugger;
            // const journal = response.json();
            // if (journal) {
            //   return journal;
            // }

            return response;
          })
          .catch(this.handleError);
      }

//#region "uploadFile"
uploadFile(pageId: number, entityId: number, file: any) {
    debugger;
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

    const url = this.settings.getBaseUrl() + GLOBAL.API_FileManagement_GetSignedURL;
    return this.PostByModel(url, DownloadObjectGCBucketModel).subscribe(x => {

        if (x.StatusCode === 200) {
            debugger;
            this.uploadFileBySignedUrl(x.data.SignedUrl, file)
                .subscribe((response: any) => {
                    debugger;
                    if (response.status === 200) {
                        
                        const data: FileModel = {
                            FileName: file.name,
                            FilePath: objectName,
                            FileSize: file.size,
                            FileType: file.type,
                            PageId: pageId,
                            RecordId: entityId
                        };

                        this.saveUploadedFileInfo(data)
                            .subscribe((response: any) => {
                                debugger;
                                // const responseData: IResponseData = {
                                //     data: x.data.SignedUrl,
                                //     statusCode: x.StatusCode,
                                //     message: x.Message
                                // };
                            });
                    }
                });
        } else {
            throw new Error('Could not get signed URL');
        }
    });
}
//#endregion

//#region "uploadFileBySignedUrl"
uploadFileBySignedUrl(url: string, data: any, options?: any) {
    return this.PutByModel(url, data);
}
//#endregion

//#region "uploadFileBySignedUrl"
// uploadFileBySignedUrl(url: string, data: any, options?: any) {
//     return this.commonService
//         .put(url, data, { observe: 'response' })
//         .pipe(
//             map(x => {
//                 console.log(x.status);
//                 const responseData: IResponseData = {
//                     data: null,
//                     statusCode: x.status,
//                     message: 'OK'
//                 };
//                 return x;
//             })
//         );
// }
//#endregion

//#region "saveUploadedFileInfo"
    saveUploadedFileInfo(data: FileModel) {
        debugger;

    const url = this.settings.getBaseUrl() + GLOBAL.API_FileManagement_SaveUploadedFileInfo;
    return this.PostByModel(url, data);
}
//#endregion

//#region "saveUploadedFileInfo"
// saveUploadedFileInfo(data: FileModel) {
//     return this.commonService
//         .post(this.settings.getBaseUrl() + GLOBAL.API_FileManagement_SaveUploadedFileInfo, data)
//         .pipe(
//             map(x => {
//                 const responseData: IResponseData = {
//                     data: x.data.SignedUrl,
//                     statusCode: x.StatusCode,
//                     message: x.Message
//                 };
//                 return responseData;
//             })
//         );
// }
//#endregion

private handleError(error: Response) {
    debugger;
    return Observable.throw(error || 'Server error');
  }

}
