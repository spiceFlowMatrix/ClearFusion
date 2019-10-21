import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class EmployeeSalaryService {
  constructor(private http: Http) {}

  GetAllEmployeeMonthlyPayrollList(
    url: string,
    officeid: number,
    month: number,
    year: number,
    paymentType: number
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url +
          '?officeid=' +
          officeid +
          '&month=' +
          month +
          '&year=' +
          year +
          '&paymentType=' +
          paymentType,
        options
      )
      .map((response: Response) => {
        const res = response.json();
        if (res) {
          return res;
        }
      })
      .catch(this.handleError);
  }

  GetAllEmployeeMonthlyPayrollApprovedList(
    url: string,
    officeid: number,
    month: number,
    year: number,
    paymentType: number
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url +
          '?officeid=' +
          officeid +
          '&month=' +
          month +
          '&year=' +
          year +
          '&paymentType=' +
          paymentType,
        options
      )
      .map((response: Response) => {
        const res = response.json();
        if (res) {
          return res;
        }
      })
      .catch(this.handleError);
  }

  ApproveEmployeeMonthlyPayrollList(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const res = response.json();
        if (res) {
          return res;
        }
      })
      .catch(this.handleError);
  }

  DisapproveEmployeeMonthlyPayrollList(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const res = response.json();
        if (res) {
          return res;
        }
      })
      .catch(this.handleError);
  }

  RemoveApprovedList(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });

    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const res = response.json();
        if (res) {
          return res;
        }
      })
      .catch(this.handleError);
  }

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Server error');
  }
}
