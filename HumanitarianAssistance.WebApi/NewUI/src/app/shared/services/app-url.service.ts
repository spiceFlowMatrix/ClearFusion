import { environment } from '../../../environments/environment';
import { Injectable, Inject } from '@angular/core';

@Injectable()
export class AppUrlService {

  constructor(@Inject('BASE_URL') private baseUrl: string) { }

  public getApiUrl(): string {
<<<<<<< HEAD

    return this.getApiURL(this.baseUrl) + 'api/';
   // return this.baseUrl + 'api/';
=======
    return environment.apiUrl;
>>>>>>> f73796b84f0e6d3d04ee71abf11400868ddb1505
  }

  public getDocUrl(): string {
<<<<<<< HEAD
    return this.getApiURL(this.baseUrl)  + 'Docs/';
   // return this.baseUrl + 'Docs/';
=======
    return environment.docUrl;
>>>>>>> f73796b84f0e6d3d04ee71abf11400868ddb1505
  }

  public getHubUrl(): string {
<<<<<<< HEAD
    return this.getApiURL(this.baseUrl)  + 'chathub/';
    //return this.baseUrl + 'chathub/';
=======
    return environment.hubUrl;
>>>>>>> f73796b84f0e6d3d04ee71abf11400868ddb1505
  }

  public getOldUiUrl(): string {
    return environment.oldUiUrl;
  }

  public getUploadDocUrl(): string {
    return environment.uploadUrl;
  }

<<<<<<< HEAD
  private getApiURL(baseURL):string {

    return (baseURL.substring(baseURL.lastIndexOf('/')))
  }
=======
  // private getApiURL(baseURL):string {

  //  return (baseURL.substring(baseURL.lastIndexOf('/')))
  // }
>>>>>>> f73796b84f0e6d3d04ee71abf11400868ddb1505

}
