 import {
  Component,
  OnInit,
  NgZone,
  Output,
  EventEmitter,
  TemplateRef,
} from '@angular/core';
import {
  ActivatedRoute,
  Router,
} from '@angular/router';
// import {ChangePasswordComponent} from '../shared/changePassword.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import {
  Validators,
  FormGroup,
  FormBuilder,
  FormControl
} from '@angular/forms';
import { CustomValidation } from '../../shared/customValidations';
import { Http } from '@angular/http';
// import { ChangePasswordComponent } from './changePassword.component';
import { GLOBAL } from '../../shared/global';
import { UserService } from '../user/user.service';
import { ToastrService } from 'ngx-toastr';
import { DashboardComponent } from '../dashboard.component';
import { CodeService } from '../code/code.service';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { CurrentPasswordModel } from '../../model/current-password-model';
import { CommonService } from '../../service/common.service';
import { AppSettingsService } from '../../service/app-settings.service';
import { Subject } from 'rxjs/Subject';
import { AuthGuard } from '../../auths/authentications';
@Component({
  selector: 'app-header',
  templateUrl: './appHeader.component.html',
  providers: [GLOBAL]
})
export class AppHeaderComponent implements OnInit {
  @Output() checkToken: EventEmitter<any> = new EventEmitter<any>();

  notificationPanelVisible = false;
  notificationIsReadCount: any;

  isConfirmPassword = true;
  isFormValid = false;

  display = false;
  signalRMessage: string;
  currentLanguage = 'en';
  loading = false;
  // commonService = new CommonService();
  idleState = 'Not started.';
  timedOut = false;
  lastPing?: Date = null;
  modalChangePassword: BsModalRef;
  ChangePassword: FormGroup;
  private CurrentPassword: FormControl;
  private unsubscribe: Subject<void> = new Subject<void>();

  config = {
    animated: true,
    keyboard: true,
    backdrop: true,
    ignoreBackdropClick: false
  };

  UserName: any;

  isValidPassword = true;

  // Office dropdown
  officecodelist: any[];

  selectedOffice: any;

  // private _hubConnection: HubConnection | undefined;
  private _hubConnection: HubConnection;
  public async: any;
  message = '';
  messages: string[] = [];

  notificationDataSource: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private zone: NgZone,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private http: Http,
    private setting: AppSettingsService,
    private userService: UserService,
    private toastr: ToastrService,
    public _dashboard: DashboardComponent,
    private authGuard: AuthGuard,
    private codeservice: CodeService,
    private commonServices: CommonService,
  ) {
    const token = localStorage.getItem('authenticationtoken');

    this.reset();

    this.CurrentPassword = new FormControl(
      '',
      Validators.compose([Validators.required])
    );
    this.ChangePassword = this.fb.group({
      CurrentPassword: this.CurrentPassword,
      // 'CurrentPassword' : ["", [Validators.required],[this.cs.validateCurrentPassword.bind(this)]],
      NewPassword: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(20)
        ])
      ],
      ConfirmPassword: [
        '',
        Validators.compose([
          CustomValidation.ConfirmPassword,
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(20)
        ])
      ]
    });
  }

  ngOnInit() {
    this.UserName = localStorage.getItem('UserName');

    // Get All Office
    this.getOfficeCodeList();

    // Notification count
    this.getNotificationCount();

    // SignalR
    this._hubConnection = new signalR.HubConnectionBuilder()
      // .withUrl(this.settings.getHubUrl + 'chathub')
      // .withUrl('http://localhost:5000/chathub')
      .withUrl(this.setting.getHubUrl())
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this._hubConnection.start().catch(err => console.error(err.toString()));

    this._hubConnection.on('Send', (data: any) => {
      this.notificationDataSource = data;
      // this.toastr.success(data);
      const received = `Received: ${data}`;
      this.messages.push(received);
    });
  }

  //#region "For Automatic logOut"
  autoLogOut() {

    localStorage.clear();
    this.router.navigate(['../login']);
    this.authGuard.previousUrl = this.router.url;
    this.checkToken.emit();
  }
  //#endregion

  onSubmitPasswordChange(value: CurrentPasswordModel) {
    if (value != null) {
      this.loading = true;
      this.userService
        .changePassword(
          this.setting.getBaseUrl() + GLOBAL.API_ChangePassword,
          value
        )
        .subscribe(data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Password changed');
            this.modalChangePassword.hide();
          } else {
            this.toastr.error('there is some error on page');
          }
          this.loading = false;
        });
    }
  }
  confirmPasswordCheck(value, newpass) {
    if (value !== newpass) {
      this.isConfirmPassword = false;
      this.isFormValid = false;
    } else {
      this.isConfirmPassword = true;
      this.isFormValid = true;
    }
  }

  passwordIsValid(value) {
    if (value !== undefined && value != null && value !== '') {
      this.userService
        .checkCurrentPassword(
          this.setting.getBaseUrl() + GLOBAL.API_CheckCurrentPassword,
          value
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.isValidPassword = true;
              this.isFormValid = true;
            } else {
              this.isFormValid = false;
              this.isValidPassword = false;
            }
          },
          error => {}
        );
    } else {
      this.isValidPassword = true;
    }
  }
  get newPassword() {
    return this.ChangePassword.controls['NewPassword'];
  }
  get currentPassword() {
    return this.ChangePassword.controls['CurrentPassword'];
  }
  onblur(e) {}
  changeLang(lang) {
    // this.languageChange.changeLang(lang)
    this.currentLanguage = lang;
  }

  logout() {

    localStorage.clear();

    this.router.navigate(['../login']);
    this.authGuard.previousUrl = '/dashboard';
    this.checkToken.emit();
  }
  changePassword(display: boolean) {
    // this.changepassword.clickme(true);
  }
  reset() {
    this.idleState = 'Started.';
    this.timedOut = false;
  }

  ChangePasswordModal(template: TemplateRef<any>) {
    this.modalChangePassword = this.modalService.show(
      template,
      Object.assign({}, this.config, { class: 'gray modal-lg' })
    );
  }

  currentPasswordMatch(event) {}
  // tslint:disable-next-line:use-life-cycle-interface
  ngOnDestroy(): void {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }

  toggleSideFun() {
    this._dashboard.toggleSide = !this._dashboard.toggleSide;
  }

  //#region "Get all Office Details"
  getOfficeCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          const AllOffices = localStorage.getItem('ALLOFFICES').split(',');
          this.officecodelist = [];
          if (
            data.StatusCode === 200 &&
            data.data.OfficeDetailsList.length > 0
          ) {
            data.data.OfficeDetailsList.forEach(element => {
              const officeFound = AllOffices.indexOf('' + element.OfficeId);
              if (officeFound !== -1) {
                this.officecodelist.push({
                  OfficeId: element.OfficeId,
                  OfficeCode: element.OfficeCode,
                  OfficeName: element.OfficeName,
                  SupervisorName: element.SupervisorName,
                  PhoneNo: element.PhoneNo,
                  FaxNo: element.FaxNo,
                  OfficeKey: element.OfficeKey
                });
              }
            });
            // tslint:disable-next-line:radix
            this.selectedOffice = parseInt(localStorage.getItem('OFFICEID'));
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
  }
  //#endregion

  //#region "getNotificationCount"
  getNotificationCount() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() +
          GLOBAL.API_Notification_GetNotificationIsReadCount
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.notificationIsReadCount = data.data.notificationIsReadCount;
        }
      });
  }
  //#endregion

  //#region "on office Selected"
  onOfficeSelected(event) {
    // this.loading = true;
    // this.commonServices.setLoader(true);
    this.commonServices.setOfficeId(event);
    this.commonServices.getOfficeId();
  }
  //#endregion

  //#region "toggleNotificationPanel"
  toggleNotificationPanel() {
    this.notificationPanelVisible = !this.notificationPanelVisible;
  }
  //#endregion

  // public sendMessage(): void {

  //     const data = `Sent: ${this.message}`;
  //     if (this._hubConnection) {

  //         this._hubConnection.invoke('Send', "ALPIT");
  //     }
  //     this.messages.push(data);
  // }

  public setNotificationIsReadCount(dataCount: any) {
    this.notificationIsReadCount = dataCount;
  }
}

class LoggerDetailsModel {
  LoggerDetailsId: any;
  NotificationId: any;
  IsRead: any;
  UserName: any;
  UserId: any;
  LoggedDetail: any;
  CreatedDate: any;
}
