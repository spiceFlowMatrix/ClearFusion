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
import { FileModel, UploadModel } from './file-management-model';
import { Observable } from 'rxjs/Observable';
import { RequestStatus } from '../enums';
import { Injectable } from '@angular/core';
import { concatMap } from 'rxjs/operators';

@Injectable()
export class FileManagementService {
    constructor(
        private settings: AppSettingsService,
        private http: Http) {
    }

    objectName: any;

    PostByModel(url: string, model: any) {
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
        const Myheaders = new Headers();
        Myheaders.append('Content-Type', model.type);
        const options = new RequestOptions({ headers: Myheaders, });
        return this.http
            .put(url, model, options)
            .map((response: Response) => {
                return response;
            })
            .catch(this.handleError);
    }

    GetById(url: string, Id: number) {
        debugger;
        const Myheaders = new Headers();
        Myheaders.append(
          'Authorization',
          'Bearer ' + localStorage.getItem('authenticationtoken')
        );
        const options = new RequestOptions({ headers: Myheaders });
        return this.http
          .get(
            url + '?Id=' + Id,
            options
          )
          .map((response: Response) => {
            const codelist = response.json();
            if (codelist) {
              return codelist;
            }
          })
          .catch(this.handleError);
      }

    getSignedURL(dataModel: UploadModel) {
        this.objectName = SignedUrlObjectName.getSignedURLObjectName(dataModel.PageId, dataModel.EntityId);

        if (this.objectName == null && this.objectName === '' && this.objectName === undefined) {
            throw new Error('object name cannot be empty');
        }

        this.objectName = this.objectName + dataModel.File.name;

        const DownloadObjectGCBucketModel = {
            ObjectName: this.objectName,
            FileName: dataModel.File.name,
            FileType: dataModel.File.type
        };

        const url = this.settings.getBaseUrl() + GLOBAL.API_FileManagement_GetSignedURL;
        return this.PostByModel(url, DownloadObjectGCBucketModel);
    }

    // #uploadFile
    uploadFile(dataModel: UploadModel) {
        return this.getSignedURL(dataModel).pipe(concatMap(res => {
            if (res['StatusCode'] === 200) {
                return this.uploadFileBySignedUrl(res['data'].SignedUrl, dataModel.File)
            } else {
                throw new Error('Could not get signed URL');
            }
        }), concatMap(res1 => {

            if (res1['status'] === 200) {
                debugger;
                const data: FileModel = {
                    FileName: dataModel.File.name,
                    FilePath: this.objectName,
                    FileSize: dataModel.File.size,
                    FileType: dataModel.File.type,
                    PageId: dataModel.PageId,
                    RecordId: dataModel.EntityId,
                    DocumentTypeId: dataModel.DocumentTypeId,
                    DocumentFileId: dataModel.DocumentFileId
                };

                if (dataModel.DocumentFileId == null || dataModel.DocumentFileId == 0 || dataModel.DocumentFileId == undefined) {
                    return this.saveUploadedFileInfo(data);

                } else {
                    return this.updateUploadedFileInfo(data);
                }
            }
        }));
    }
    //#endregion

    //#region "uploadFileBySignedUrl"
    uploadFileBySignedUrl(url: string, data: any, options?: any) {
        return this.PutByModel(url, data);
    }
    //#endregion

    //#region "saveUploadedFileInfo"
    saveUploadedFileInfo(data: FileModel) {

        const url = this.settings.getBaseUrl() + GLOBAL.API_FileManagement_SaveUploadedFileInfo;
        return this.PostByModel(url, data);
    }
    //#endregion

    //#region "saveUploadedFileInfo"
    updateUploadedFileInfo(data: FileModel) {

        const url = this.settings.getBaseUrl() + GLOBAL.API_FileManagement_UpdateUploadedFileInfo;
        return this.PostByModel(url, data);
    }
    //#endregion

    //#region "saveUploadedFileInfo"
    getSignedURLByDocumenFileId(id: number) {

        const url = this.settings.getBaseUrl() + GLOBAL.API_FileManagement_GetSignedURLByDocumentFileId;
        return this.GetById(url, id);
    }
    //#endregion

    private handleError(error: Response) {
        return Observable.throw(error || 'Server error');
    }

}
