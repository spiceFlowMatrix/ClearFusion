import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { Observable, Observer, of, timer } from 'rxjs';
import 'rxjs/add/operator/filter';
import * as auth0 from 'auth0-js';
import { flatMap } from 'rxjs/operators';
import { GlobalService } from '../shared/services/global-services.service';
import { GLOBAL } from '../shared/global';
import { AppUrlService } from '../shared/services/app-url.service';

(window as any).global = window;

@Injectable()
export class Auth0Service {

  auth0 = new auth0.WebAuth({
    clientID: environment.Auth0Config.clientID,
    domain: environment.Auth0Config.domain,
    responseType: 'token id_token',
    audience: environment.Auth0Config.apiUrl,
    redirectUri: environment.Auth0Config.callbackURL,
    scope: 'openid profile email read:messages'
  });

  userProfile: any;
  refreshSubscription: any;
  userRolesArr: any[] = [];
  userRoles: string;
  observer: Observer<boolean>;
  ssoAuthComplete$: Observable<boolean> = new Observable(
    obs => (this.observer = obs)
  );

  constructor(public router: Router, private globalService: GlobalService,
    private appURL: AppUrlService,) { }

  public login(): void {
    this.auth0.authorize();
  }

  public handleAuthentication(): void {
    this.auth0.parseHash((err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        this.setSession(authResult);
        const data = {
          UserId: authResult.idTokenPayload.sub.substring(6)
        };
        this.onFormSubmit(data);
        this.router.navigate(['/']);
      } else if (err) {
        this.router.navigate(['/']);
        console.log(err);
        alert(`Error: ${err.error}. Check the console for further details.`);
      }
    });
  }

  public getProfile(cb): void {
    const accessToken = localStorage.getItem('access_token');
    if (!accessToken) {
      throw new Error('Access token must exist to fetch profile');
    }

    const self = this;
    this.auth0.client.userInfo(accessToken, (err, profile) => {
      if (profile) {
        self.userProfile = profile;
      }
      cb(err, profile);
    });
  }

  private setSession(authResult): void {
    // Set the time that the access token will expire at
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('authenticationtoken', authResult.accessToken);
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);
  }

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('access_token');
    localStorage.removeItem('authenticationtoken');
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    // Go back to the home route
    this.router.navigate(['/']);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // access token's expiry time
    const expiresAt = JSON.parse(localStorage.getItem('expires_at') || '{}');
    return new Date().getTime() < expiresAt;
  }


  public renewToken() {
    this.auth0.checkSession({},
      (err, result) => {
        if (err) {
          alert(
            `Could not get a new token (${err.error}: ${err.error_description}).`
          );
          this.login();
        } else {
          this.setSession(result);
          this.observer.next(true);
        }
      }
    );
  }

  public scheduleRenewal() {
    if (!this.isAuthenticated()) return;
    this.unscheduleRenewal();

    const expiresAt = JSON.parse(window.localStorage.getItem('expires_at'));

    const source = of(expiresAt).pipe(flatMap(expiresAt => {
      const now = Date.now();

      // Use the delay in a timer to
      // run the refresh at the proper time
      return timer(Math.max(1, expiresAt - now));
    }));

    // Once the delay time from above is
    // reached, get a new JWT and schedule
    // additional refreshes
    this.refreshSubscription = source.subscribe(() => {
      this.renewToken();
      this.scheduleRenewal();
    });
  }

  public unscheduleRenewal() {
    if (!this.refreshSubscription) return;
    this.refreshSubscription.unsubscribe();
  }

  onFormSubmit(data) {
    const loginData = {
      UserName: null,
      Password: null,
      UserId: data.UserId
    };

    // this.commonLoaderService.showLoader();

    this.globalService
      .post(this.appURL.getApiUrl() + GLOBAL.API_Login_Auth_Url, loginData)
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {

          if (data.StatusCode === 200) {
            // set token
            // localStorage.setItem('authenticationtoken', data.data.Token);

            data.data.Roles.forEach(element => {
              this.userRolesArr.push(element);
            });
            this.userRoles = this.userRolesArr.join(',');
            localStorage.setItem('UserRoles', this.userRoles);

            if (data.data.RolePermissionModelList != null) {
              localStorage.setItem(
                'RolePermissions',
                JSON.stringify(data.data.RolePermissionModelList)
              );
            }
            if (data.data.ApproveRejectPermissionsInRole != null) {
              localStorage.setItem(
                'ApproveRejectRolePermissions',
                JSON.stringify(data.data.ApproveRejectPermissionsInRole)
              );
            }

            if (data.data.AgreeDisagreePermissionsInRole != null) {

              localStorage.setItem('AgreeDisagreeRolePermissions', JSON.stringify(data.data.AgreeDisagreePermissionsInRole));
            }

            if (data.data.OrderSchedulePermissionsInRole != null) {

              localStorage.setItem('OrderScheduleRolePermissions', JSON.stringify(data.data.OrderSchedulePermissionsInRole));
            }

            // check if Office Id present
            localStorage.setItem('ALLOFFICES', '');
            if (
              data.data.UserOfficeList != null &&
              data.data.UserOfficeList.length > 0
            ) {
              const Offices = data.data.UserOfficeList.join(',');
              localStorage.setItem('ALLOFFICES', Offices);
            }

            // redirect to dashboard
            this.router.navigate(['']);

          } else if (data.StatusCode === 400) {
            // this.toastr.error(data.Message);
          }

          // this.commonLoaderService.hideLoader();
        },
        error => {
          // this.commonLoaderService.hideLoader();
        }
      );
  }

}

