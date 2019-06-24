import { Injectable } from '@angular/core';
import {
  Http,
  Headers,
  Response,
  RequestOptions,
} from '@angular/http';
import { Observable } from 'rxjs/Observable';

export class Employee {
  id: number;
  color: string;
}

export class SchedulerData {
  text: string;
  employeeID: number;
  startDate: any;
  endDate: any;
  priority: any;
  overTimeHours?: string;
  attendanceId?: number;
}
export class Priority {
  text: string;
  id: number;
  color: string;
}

export class Resource {
  text: string;
  id: number;
  color: string;
}

@Injectable()
export class SchedulerService {
  constructor(private http: Http) {}

  //#region "Get Employee Attendence"
  GetEmployeeAttendanceDetailsById(url: string, employeeFilter: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, employeeFilter, options)
      .map(r => r.json())
      .catch(this.handleError);
  }

  //#endregion

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Server error');
  }
}
