import { environment } from '../../../environments/environment';
import { Injectable, Inject } from '@angular/core';

@Injectable()
export class AppUrlService {

  constructor(@Inject('BASE_URL') private baseUrl: string) { }

  public getApiUrl(): string {
    return environment.apiUrl;
  }

  public getDocUrl(): string {
    return environment.docUrl;
  }

  public getHubUrl(): string {
    return environment.hubUrl;
  }

  public getOldUiUrl(): string {
    return environment.oldUiUrl;
  }

  public getUploadDocUrl(): string {
    return environment.uploadUrl;
  }

  public getNotifyHubUrl(): string {
    return environment.notifyHubUrl;
  }

  // private getApiURL(baseURL):string {

  //  return (baseURL.substring(baseURL.lastIndexOf('/')))
  // }

}
