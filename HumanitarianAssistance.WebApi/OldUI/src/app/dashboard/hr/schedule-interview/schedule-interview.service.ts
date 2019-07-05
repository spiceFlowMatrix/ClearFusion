import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ScheduleInterviewService {
  constructor(private http: Http) {}

  //#region "Get All"
  GetAllDetails(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Get All prospective emp by Profession Id"
  GetProspectiveEmployeesByProfessionId(url: string, id: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?ProfessionId=' + id, options)
      .map((response: Response) => {
        const emp = response.json();
        if (emp) {
          return emp;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Add Interview Schedule Details"
  AddInterviewScheduleDetails(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Get All prospective emp by Profession Id"
  GetById(url: string, id: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?ProfessionId=' + id, options)
      .map((response: Response) => {
        const emp = response.json();
        if (emp) {
          return emp;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region Interview Approvals
  InterviewApprovals(url: string, model: any, approvalId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url + '?approvalId=' + approvalId, model, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Server error');
  }
}
