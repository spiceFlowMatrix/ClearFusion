import { Injectable, Optional, OnInit } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { GLOBAL } from '../shared/global';
import { AppSettingsService } from './app-settings.service';
import { FormControl, ValidationErrors } from '@angular/forms';
import { RequestStatus } from '../shared/enums';
import { Subject } from 'rxjs/Subject';

import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class CommonService implements OnInit {
  voucherNumber: any;
  LoaderFlag = false;
  Accounts: any[] = [];

  private checktimeout;
  public notificationDataSource: Subject<any>;

  private OfficeId = new Subject<any>();
  private EmployeeOfficeId = new Subject<any>();
  private StoreOfficeId = new Subject<any>();
  private AccountingOfficeId = new Subject<any>();
  private Loading = new Subject<any>();
  // private _hubConnection: HubConnection | undefined;

  private _hubConnection: HubConnection;
  public async: any;
  message = '';
  messages: string[] = [];

  constructor(
    private settings: AppSettingsService,
    private http: Http,
    private toastr: ToastrService
  ) {
    // #region "SignalR"
    this._hubConnection = new signalR.HubConnectionBuilder()
      // .withUrl(this.settings.getHubUrl + 'chathub')
      // .withUrl('http://localhost:5000/chathub')
      .withUrl(this.settings.getHubUrl())
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this._hubConnection.start().catch(err => console.error(err.toString()));

    //#endregion
  }

  ngOnInit() {
    // this._hubConnection.on('Send', (data: any) => {
    //     this.toastr.success(data);
    //     // this.notificationDataSource = data;
    // });
  }

  GetProviders() {
    const URL =
      this.settings.getBaseUrl() + GLOBAL.API_Common_GetProviderByMaster;
    return this.http
      .request(URL, {
        method: 'GET',
        headers: new Headers({
          'Content-Type': 'application/json'
        })
      })
      .map((response: Response) => {
        // login successful if there's a jwt token in the response
        return response.json();
      })
      .catch((error: any) => {
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
      });
  }
  validateCurrentPassword(
    control: FormControl
  ): Promise<ValidationErrors> | null | Observable<ValidationErrors> {
    clearTimeout(this.checktimeout);
    return new Promise<any>((resolve, reject) => {
      this.checktimeout = setTimeout(() => {
        const Myheaders = new Headers();
        Myheaders.append(
          'Authorization',
          'Bearer ' + localStorage.getItem('authenticationtoken')
        );
        Myheaders.append('Content-Type', 'application/json');
        const options = new RequestOptions({ headers: Myheaders });
        const URL =
          this.settings.getBaseUrl() + GLOBAL.API_CheckCurrentPassword;
        // let obj={Password:control.value.toString()};
        return this.http
          .get(URL + '?pwd=' + control.value, options)
          .map(response => response.json())
          .subscribe(data => {
            if (data.StausCode === 200) {
              resolve({ validateCurrentPassword: true });
            } else {
              resolve(null);
            }
          });
      }, 2000);
    });
  }

  verifyCurrentDomain() {
    const parts = location.hostname.split('.');
    const SubDomain = parts.shift();
  }

  setSelectedVoucherNumber(voucherNo) {
    this.voucherNumber = voucherNo;
  }


  setOfficeId(id: any) {
    localStorage.setItem('OFFICEID', id.toString());
    this.OfficeId.next(id);
  }

  setAccountingOfficeId(id: any) {
    localStorage.setItem('ACCOUNTINGOFFICEID', id.toString());
    this.AccountingOfficeId.next(id);
  }

  setStoreOfficeId(id: any) {
    localStorage.setItem('STOREOFFICEID', id.toString());
    this.StoreOfficeId.next(id);
  }

  setEmployeeOfficeId(id: any) {
    localStorage.setItem('EMPLOYEEOFFICEID', id.toString());
    this.EmployeeOfficeId.next(id);
  }

  getEmployeeOfficeId(): Observable<any> {
    return this.EmployeeOfficeId.asObservable();
  }

  getStoreOfficeId(): Observable<any> {
    return this.StoreOfficeId.asObservable();
  }

  getAccountingOfficeId(): Observable<any> {
    return this.AccountingOfficeId.asObservable();
  }

  getOfficeId(): Observable<any> {
    return this.OfficeId.asObservable();
  }

  setLoader(e: boolean) {
    this.LoaderFlag = e;
    this.Loading.next(this.LoaderFlag);
  }

  getLoader() {
    return this.Loading.asObservable();
  }

  translate(obj: GoogleObj, key: string) {
    return this.http.post(url + key, obj);
  }

  //#region "Signal R"
  public sendMessage(data: any) {
    // var data = modelData;
    if (this._hubConnection) {
      this._hubConnection.invoke('Send', data);
      // this._hubConnection.invoke('Send', JSON.stringify(data));
    }
  }

  //#endregion

  //#region "sort"
  sortDropdown(dataSource: any[], fieldName: string) {
    // Sorted in Asc
    return dataSource.sort((x, y) => {
      // tslint:disable-next-line:curly
      if (x[fieldName] < y[fieldName]) return -1;
      // tslint:disable-next-line:curly
      if (x[fieldName] > y[fieldName]) return 1;
      return 0;
    });
  }

  GetInputLevelAccountDetails() {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        this.settings.getBaseUrl() + GLOBAL.API_Accounting_GetAccountDetails,
        options
      )
      .map((response: Response) => {
        const data = response.json();
        if (data.data.AccountDetailList.length > 0) {
          this.Accounts = data.data.AccountDetailList.filter(
            x => x.AccountLevelId === 4
          );
          return this.Accounts;
        }
      })
      .catch(this.handleError);
  }

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Server error');
  }

  public IsEditingAllowed(pageId: number): boolean {
    let isSuperadmin = false;
    let roles: any;

    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');
    }

    if (roles.length > 0 && roles != null) {
      for (let i = 0; i < roles.length; i++) {
        if (roles[i].toLowerCase() === 'superadmin') {
          isSuperadmin = true;
        }
      }
    }

    if (!isSuperadmin) {
      // when role is not superadmin then check for permission

      const permissionList: any[] = JSON.parse(
        localStorage.getItem('RolePermissions')
      );

      const modulePageExists = permissionList.find(x => x.PageId === pageId);

      if (modulePageExists != null) {
        // if user has permission for the page to view/edit
        if (modulePageExists.CanEdit) {
          return true;
        } else {
          return false;
        }
      } else {
        // if user is not having permission then return false
        return false;
      }
    } else {
      // if superadmin then return editing true
      return isSuperadmin;
    }
  }
}

const url = 'https://translation.googleapis.com/language/translate/v2?key=';

export class GoogleObj {
  q: string;
  readonly source: string = 'en';
  readonly target: string = 'fa';
  readonly format: string = 'text';

  constructor() {}
}

export class LoggerDetailsModel {
  LoggerDetailsId: any;
  NotificationId: any;
  IsRead: any;
  UserName: any;
  UserId: any;
  LoggedDetail: any;
  CreatedDate: any;
}
