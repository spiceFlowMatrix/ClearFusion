import { environment } from '../../../environments/environment';
import { Injectable, Inject } from '@angular/core';

@Injectable()
export class AppUrlService {

  constructor(@Inject('BASE_URL') private baseUrl: string) { }

  public getApiUrl(): string {
    return '/api/';
    // return this.baseUrl + 'api/';
  }
  public getDocUrl(): string {
    return '/Docs/';
    // return this.baseUrl + 'Docs/';
  }
  public getHubUrl(): string {
    return '/chathub/';
    // return this.baseUrl + 'chathub/';
  }
  public getOldUiUrl(): string {
    return environment.oldUiUrl;
  }
  public getUploadDocUrl(): string {
    return environment.uploadUrl;
  }

  // private getApiURL(baseURL):string {

  //  return (baseURL.substring(baseURL.lastIndexOf('/')))
  // }

}
