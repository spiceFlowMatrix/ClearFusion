import { Injectable } from '@angular/core';
import {
  Http,
  Headers,
  Response,
  RequestOptions,
  RequestOptionsArgs
} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { AddUsers, EditUsers } from '../../model/add-user';
import { RestPasswordModel, CurrentPasswordModel } from '../../model/current-password-model';
@Injectable()
export class UserService {
  constructor(private http: Http) {}

  GetUserList(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  getUserRoles(url: string): Observable<any> {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  getUserRolesByUserId(url: string, UserId) {
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

  getUserDetailByUserId(url: string, UserId) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?UserId=' + UserId, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  assignRolesToUser(url: string, userId: string, addRoles: any[]) {
    const obj = {
      UserId: userId,
      Roles: addRoles
    };
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, obj, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  AddUser(url: string, model: AddUsers) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      FirstName: model.FirstName,
      LastName: model.LastName,
      UserName: model.UserName,
      Email: model.Email,
      Password: model.Password,
      UserType: 1,
      OfficeCode: model.OfficeCode,
      DepartmentId: model.DepartmentId,
      Status: model.Status,
      Phone: model.Phone,
      OfficeId: model.OfficeId
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  EditUser(url: string, model: EditUsers) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      FirstName: model.FirstName,
      LastName: model.LastName,
      UserName: model.UserName,
      Email: model.Email,
      // Password : model.Password,
      UserType: 1,
      OfficeCode: model.OfficeCode,
      DepartmentId: model.DepartmentId,
      Status: model.Status,
      Id: model.Id,
      Phone: model.Phone,
      OfficeId: model.OfficeId
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  getOffices(url: string): Observable<any> {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  getDepartment(url: string, officeCode) {
    const obj: any = {};
    obj.officeCode = officeCode;
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?OfficeId=' + officeCode, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  getPermissions(url: string): Observable<any> {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  PermissionsInRoles(url: string, obj): Observable<any> {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, obj, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }
  getPermissionByRoleId(url: string, roleid) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?roleid=' + roleid, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  resetPassword(url: string, model: RestPasswordModel) {
    const Myheaders = new Headers();

    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map(response => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }
  changePassword(url: string, model: CurrentPasswordModel) {
    const Myheaders = new Headers();

    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map(response => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }
  checkCurrentPassword(url: string, currentPassword: string) {
    const Myheaders = new Headers();

    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?pwd=' + currentPassword, options)
      .map(response => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  getRolePermissions(url: string, RoleId) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?roleid=' + RoleId, options)
      .map((response: Response) => {
        const user = response.json();
        if (user) {
          return user;
        }
      })
      .catch(this.handleError);
  }

  updateRolePermissions(url: string, obj: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, obj, options)
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
