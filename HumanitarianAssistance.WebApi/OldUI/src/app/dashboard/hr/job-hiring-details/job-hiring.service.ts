import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

export class JobHiringDetailsModel {
  JobId: number;
  JobCode: string;
  JobDescription: string;
  OfficeId?: number;
  ProfessionId?: number;
  GradeId?: number;
  // ProfessionName: string;
  Unit?: number;
  IsActive?: boolean;

  ApprovedInterviews?: number;
}
export class OfficeTypeModel {
  OfficeId: number;
  OfficeName: string;
}
export class ProfessionTypeModel {
  ProfessionId: number;
  ProfessionName: string;
}
export class JobGradeTypeDropdown {
  GradeId: number;
  GradeName: string;
}

@Injectable()
export class JobHiringService {
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

  //#region "Get All"
  GetJobHiringDetailByOfficeId(url: string, OfficeId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    if (OfficeId != null) {
      return this.http
        .get(url + '?OfficeId=' + OfficeId, options)
        .map((response: Response) => {
          const codelist = response.json();
          if (codelist) {
            return codelist;
          }
        })
        .catch(this.handleError);
    }
  }

  //#region "Add Job Hiring"
  AddJobHiringDetail(url: string, model: any) {
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

  //#region "Update Job Hiring"
  EditJobHiringDetail(url: string, model: JobHiringDetailsModel) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, JSON.stringify(model), options)
      .map((response: Response) => {
        const emp = response.json();
        if (emp) {
          return emp;
        }
      })
      .catch(this.handleError);
  }

  //#endregion

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Server error');
  }
}
