import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppUrlService } from '../shared/services/app-url.service';
import { GlobalService } from '../shared/services/global-services.service';
import { GLOBAL } from '../shared/global';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonLoaderService } from '../shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { Auth0Service } from '../auth0/auth0.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  loginForm: FormGroup;
  userRolesArr: any[] = [];
  userRoles: string;

  constructor(
    private router: Router,
    private appURL: AppUrlService,
    private globalService: GlobalService,
    private fb: FormBuilder,
    private commonLoaderService: CommonLoaderService,
    private toastr: ToastrService,
    private auth0: Auth0Service
  ) {
    this.loginForm = this.fb.group({
      Username: [null, Validators.required],
      Password: [
        null,
        Validators.compose([
          Validators.required,
          Validators.maxLength(20)
        ])
      ]
    });
  }

  ngOnInit() {
    // this.auth0.logout();
    // this.auth0.logout();
     this.auth0.login();
     // this.auth0.handleAuthentication();
    // this.login();
  }

  onFormSubmit(data) {
    const loginData = {
      UserName: data.Username,
      Password: data.Password
    };

    this.commonLoaderService.showLoader();

    this.globalService
      .post(this.appURL.getApiUrl() + GLOBAL.API_Login_Auth_Url, loginData)
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {

          if (data.StatusCode === 200) {
            // set token
            localStorage.setItem('authenticationtoken', data.data.Token);

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
            this.toastr.error(data.Message);
          }

          this.commonLoaderService.hideLoader();
        },
        error => {
          this.commonLoaderService.hideLoader();
        }
      );
  }
  ngOnDestroy(): void {
//    this.auth0.handleAuthentication();
  }
}
