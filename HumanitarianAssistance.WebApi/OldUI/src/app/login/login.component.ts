import {
  Component,
  Output,
  EventEmitter
} from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthenticationService } from '../service/authentication.service';
import { CommonService } from '../service/common.service';
import { UserLogin } from '../model/User';
import { AuthGuard } from '../auths/authentications';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-Login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  @Output() checkToken: EventEmitter<any> = new EventEmitter<any>();
  rForm: FormGroup;
  returnUrl: string;
  messages: any = [];
  userRolesArr: any[];
  userRoles: any;

  // UserPermissions
  userPermission: any[];

  constructor(
    private toastr: ToastrService,
    private fb: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService,
    private authGuard: AuthGuard,
    private commonService: CommonService,
  ) {
    this.rForm = this.fb.group({
      Username: [null, Validators.required],
      Password: [
        null,
        Validators.compose([
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(20)
        ])
      ]
    });
  }
  loginModel = new UserLogin();
  loading = false;

  onFormSubmit(e) {
    this.loading = true;
    this.loginModel.UserName = e.Username;
    this.loginModel.Password = e.Password;
    this.authenticationService.login(this.loginModel).subscribe(
      data => {
        if (data != null) {
          if (data.StatusCode === 200) {
            this.userRolesArr = [];
            data.data.Roles.forEach(element => {
              this.userRolesArr.push(element);
            });
            this.userRoles = this.userRolesArr.join(',');
            if (data.Message === 'Success' || data.StatusCode === 200) {
              localStorage.setItem('authenticationtoken', data.data.Token);
              localStorage.setItem('UserRoles', this.userRoles);
              localStorage.setItem('UserId', data.data.AspNetUserId);
              localStorage.setItem('UserName', this.loginModel.UserName);

              // check if Office Id present
              localStorage.setItem('ALLOFFICES', '');
              if (
                data.data.UserOfficeList != null &&
                data.data.UserOfficeList.length > 0
              ) {
                const Offices = data.data.UserOfficeList.join(',');
                localStorage.setItem('ALLOFFICES', Offices);
                this.commonService.setOfficeId(data.data.UserOfficeList[0]);
              }

              if (data.data.UserRolePermissions != null) {
                localStorage.setItem(
                  'UserRolePermission',
                  JSON.stringify(data.data.UserRolePermissions)
                );
              }

              if (data.data.RolePermissionModelList != null) {
                localStorage.setItem(
                  'RolePermissions',
                  JSON.stringify(data.data.RolePermissionModelList)
                );
              }

              // this.checkToken.emit();
              if (this.authGuard.previousUrl === undefined) {
                this.authGuard.previousUrl = '/dashboard';
              }
              this.router.navigateByUrl(this.authGuard.previousUrl);
            } else {
              this.loginModel.ErrorMessage = data.Result;
            }
          } else if (data.StatusCode === 400) {
            this.loading = false;
            this.toastr.error(data.Message);
          }
        }
      },
      error => {
        this.loading = false;
        // this.toastr.error("Invalid Password or User Name");

        this.toastr.error('Oops, Something went wrong. Please try again');
        if (error.message === 500) {
          this.messages.push({
            severity: 'error',
            summary: 'Error Message',
            detail: 'Oops, Something went wrong. Please try again.'
          });
        } else if (error.message === 0) {
          this.messages.push({
            severity: 'error',
            summary: 'Error Message',
            detail: 'Network error, Please try again later'
          });
        } else {
          this.messages.push({
            severity: 'error',
            summary: 'Error Message',
            detail: 'Some error occured, Please contact your admin'
          });
        }
      }
    );
  }
}
