import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { GLOBAL } from '../shared/global';
import { AppSettingsService } from './app-settings.service';
import { RequestStatus } from '../shared/enums';
import { Observable } from 'rxjs/Observable';
import { UserLogin } from '../model/User';
import { map} from 'rxjs/operators';
import { catchError } from 'rxjs/operators/catchError';

@Injectable()
export class AuthenticationService {
  constructor(private settings: AppSettingsService, private http: Http) {}

  login(loginDetails: UserLogin) {
    const url = this.settings.getBaseUrl() + GLOBAL.API_Login_Auth_Url;
    return this.http
      .get(url, {
        body: loginDetails,
        method: 'POST',
        headers: new Headers({
          'Content-Type': 'application/json'
        })
      }).pipe(map((response: Response) => {
        const loginResponse = response.json();
        localStorage.setItem('authenticationtoken', loginResponse.Token);
        // localStorage.setItem('plainRolesText', loginResponse.Roles);
        // localStorage.setItem('userRoles', loginResponse.RolesTicket);
        // localStorage.setItem('userId', loginResponse.UserId);
        return loginResponse;
      }),
      catchError((error: any) => {
        if (error.status === RequestStatus.InternalError) {
          return Observable.throw(new Error(error.status));
        } else if (error.status === RequestStatus.NotFound) {
          return Observable.throw(new Error(error.status));
        } else if (error.status === RequestStatus.NotAcceptable) {
          return Observable.throw(new Error(error.status));
        } else if (error.status === RequestStatus.Confilct) {
          return Observable.throw(new Error(error.status));
        } else {
          return Observable.throw(new Error(error.status));
        }
      })
      );
  }

  logout() {
    // remove user from local storage to log user out
    // localStorage.removeItem('authenticationtoken');
    // localStorage.removeItem('plainRolesText');
    // localStorage.removeItem('userRoles');
    // localStorage.removeItem('userId');
    // localStorage.removeItem('ApplicationPages');
    // localStorage.removeItem('RolePermissions');

    localStorage.clear();
  }

  getPermissions(role) {
    if (role !== undefined) {
      const Roles = localStorage.getItem('plainRolesText');
      const userRoles = Roles.split('|');
      if (
        userRoles.filter(x => x.toLowerCase() === role.toLowerCase()).length > 0
      ) {
        return true;
      } else {
        return false;
      }
    } else {
      return false;
    }
  }

  // getUserType() {
  //     return this.httpClient
  //         .get(GLOBAL.API_Account_GetUserType);
  // }

  getRolesAndPermissionsByUserId(url: string, UserId) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?userid=' + UserId, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Server error');
  }
}
