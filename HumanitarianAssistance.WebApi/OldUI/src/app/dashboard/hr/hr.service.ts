import { Injectable } from '@angular/core';
import {
  Http,
  Headers,
  Response,
  RequestOptions,
  RequestOptionsArgs
} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HealthModel } from './employees/employees.component';

export class EmployeeAttendanceList {
  EmployeeId: number;
  AttendanceId?: number;
  EmployeeName: string;
  EmployeeCode: string;
  InTime: any;
  OutTime: any;
  AttendanceTypeId: number;
  Date: any;
  TotalWorkTime: string;
  LeaveStatus: boolean;
  OfficeId: number;
}

//#region "Attendance Type"
export class AttendanceType {
  AttendanceTypeId: number;
  AttendanceTypeName: string;
}
const attendanceType: AttendanceType[] = [
  {
    AttendanceTypeId: 1,
    AttendanceTypeName: 'P'
  },
  {
    AttendanceTypeId: 2,
    AttendanceTypeName: 'A'
  },
  {
    AttendanceTypeId: 3,
    AttendanceTypeName: 'L'
  },
  {
    AttendanceTypeId: 4,
    AttendanceTypeName: 'H'
  }
];
//#endregion

// FOR Employees
export class Tab {
  id: number;
  text: string;
}

// HR tabs
@Injectable()
export class HrService {
  constructor(private http: Http) {}

  getAllAttendanceType(): AttendanceType[] {
    return attendanceType;
  }

  getSexTypes() {
    return sexTypes;
  }

  //#region  "Get All Attendance"
  GetAllActiveEmployeeForAttendance(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map(r => r.json())
      .catch(this.handleError);
  }
  //#endregion

  //#region  "Get All Attendance"
  GetAllActiveEmployeeForAttendanceByDate(
    url: string,
    data: any
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(
        url, data, options
      )
      .map(r => r.json())
      .catch(this.handleError);
  }
  //#endregion

  //#region  "Add All Attendance for today"
  AddEmployeeAttendanceDetails(url: string, data: EmployeeAttendanceList[]) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, data, options)
      .map(r => r.json())
      .catch(this.handleError);
  }
  //#endregion

  //#region "Update Profile image"
  EditEmployeeImage(url: string, model: any) {
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

  //#region "Get All Dropdowns"
  GetAllDropdown(url: string) {
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

  //#region "Get By Id Dropdowns"
  GetDepartmentDropdown(url: string, OfficeId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
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
  //#endregion

  //#region "ADD EMPLOYEE"
  addEmployee(url: string, info: GeneralInfo) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, info, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "ADD EMPLOYEE"

  //#region "GET ALL EMPLOYEES"
  GetAllEmployees(url: string, EmployeeType: number, OfficeId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url + '?EmployeeType=' + EmployeeType + '&OfficeId=' + OfficeId,
        options
      )
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET ALL EMPLOYEES"

  //#region "UPDATE EMPLOYEE"
  EditEmployee(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const emp = response.json();
        if (emp) {
          return emp;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "UPDATE EMPLOYEE"

  //#region  "GET EMPLOYEE BY ID"
  GetEmployeesDetailsByEmployeeId(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?EmployeeId=' + model, options)
      .map((response: Response) => {
        const emp = response.json();
        if (emp) {
          return emp;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET EMPLOYEE BY ID"

  //#region "GET ALL PROFESSIONS"
  GetAllProfession(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const professionlist = response.json();
        if (professionlist) {
          return professionlist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET ALL PROFESSIONS"

  //#region "GET ALL QUALIFICATION"
  GetAllQualification(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const qualificationlist = response.json();
        if (qualificationlist) {
          return qualificationlist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET ALL QUALIFICATION"

  //#region "GET ALL COUNTRY"
  GetAllCountry(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const countrylist = response.json();
        if (countrylist) {
          return countrylist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET ALL COUNTRY"

  //#region "GET ALL STATE"
  GetAllProvinceDetails(url: string, model: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?CountryId=' + model, options)
      .map((response: Response) => {
        const statelist = response.json();
        if (statelist) {
          return statelist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET ALL STATE"

  //#region "ADD Profession Info"
  addProfessionalInfo(url: string, info: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, info, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "ADD DOCUMENT"
  AddDocumentDetails(url: string, doc: EmpDocuments) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, doc, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "ADD DOCUMENT"

  //#region "GET ALL DOCUMENTS"
  GetAllDocumentDetails(url: string, EmployeeId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?EmployeeId=' + EmployeeId, options)
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }

  GetAllProjectDocumentDetails(url: string, ProjectId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?ProjectId=' + ProjectId, options)
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET ALL DOCUMENTS"

  //#region "UPDATE DOCUMENT"
  EditDocumentDetails(url: string, model: any) {
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
        const doc = response.json();
        if (doc) {
          return doc;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "UPDATE DOCUMENT"

  // region "DELETE DOCUMENT"
  DeleteDocumentDetail(url: string, model: DeleteDocument) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      DocumentId: model.DocumentId
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const document = response.json();
        if (document) {
          return document;
        }
      })
      .catch(this.handleError);
  }
  // endregion "DELETE DOCUMENT"

  //#region "ADD EMPLOYEE HISTORY"
  AddEmployeeHistoryDetail(url: string, info: EmpHistory) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, info, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "ADD EMPLOYEE HISTORY"

  //#region "UPDATE EMPLOYEE HISTORY"
  EditEmployeeHistoryDetail(url: string, model: any) {
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
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "UPDATE EMPLOYEE HISTORY"

  //#region "GET ALL EMPLOYEE HISTORY"
  GetAllEmployeeHistoryByEmployeeId(url: string, EmployeeId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?EmployeeId=' + EmployeeId, options)
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET ALL EMPLOYEE HISTORY"

  // region "DELETE EMPLOYEE HISTORY"

  //#region "Delete employee History"
  DeleteEmployeeHistoryDetail(url: string, HistoryId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });

    return this.http
      .delete(url + '?HistoryId=' + HistoryId, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Add Pay Roll Monthly"
  AddEditPayrollMonthlyHour(url: string, model: any) {
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

  //#region "Add Leave Info"
  addLeaveInfo(url: string, data: HealthModel) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, data, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Add Health Info"
  addHealthInfo(url: string, data: HealthModel) {
    const model = {
      EmployeeId: data.EmployeeId,
      BloodGroup: data.BloodGroup,
      MedicalHistory: data.MedicalHistory,
      SmokeAndDrink: data.SmokeAndDrink,
      Insurance: data.Insurance,
      MedicalInsurance: data.MedicalInsurance,
      MeasureDieases: data.MeasureDieases,
      AllergicSubstance: data.AllergicSubstance,
      FamilyHistory: data.FamilyHistory
    };
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

  //#region "Edit Health Info"
  editHealthInfo(url: string, data: HealthModel) {
    const model = {
      HealthInfoID: data.HealthInfoId,
      EmployeeId: data.EmployeeId,
      BloodGroup: data.BloodGroup,
      MedicalHistory: data.MedicalHistory,
      SmokeAndDrink: data.SmokeAndDrink,
      Insurance: data.Insurance,
      MedicalInsurance: data.MedicalInsurance,
      MeasureDieases: data.MeasureDieases,
      AllergicSubstance: data.AllergicSubstance,
      FamilyHistory: data.FamilyHistory
    };
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

  //#region "Get All Leave By id"
  GetAllLeaveInfoById(url: string, id: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?EmployeeId=' + id, options)
      .map((response: Response) => {
        const emp = response.json();
        if (emp) {
          return emp;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Approve Leave"
  ApproveRejectEmployeeLeave(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const r = response.json();
        if (r) {
          return r;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Delete applied Leave"
  DeleteApplyEmployeeLeave(url: string, applyleaveid: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?applyleaveid=' + applyleaveid, options)
      .map((response: Response) => {
        const r = response.json();
        if (r) {
          return r;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Get Employee Health Detail"
  GetEmployeeHealthDetail(url: string, employeeid: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?employeeid=' + employeeid, options)
      .map((response: Response) => {
        const countrylist = response.json();
        if (countrylist) {
          return countrylist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Add By Model"
  AddByModel(url: string, model: any) {
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

  GetAllDetail(url: string, OfficeId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?officeid=' + OfficeId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllDetailByGenericParameter(
    url: string,
    parameterName: string,
    parameter: any
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?' + parameterName + '=' + parameter, options)
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
  GetAllDetailsByOfficeId(url: string, OfficeId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
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
  //#endregion

  //#region "Get All"
  GetAllDetailsByEmployeeId(url: string, EmployeeId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?EmployeeId=' + EmployeeId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllDetailsById(url: string, idName: string, id: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?' + idName + '=' + id, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllDetailsByEmployeeIdAndDate(
    url: string,
    EmployeeId: number,
    CurrentAppraisalDate: any
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
          '?EmployeeId=' +
          EmployeeId +
          '&CurrentAppraisalDate=' +
          CurrentAppraisalDate,
        options
      )
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region  "Delete Holiday"
  DeleteHolidayDetail(url: string, holidayId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });

    return this.http
      .delete(url + '?holidayId=' + holidayId, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region  "Delete Holiday"
  DeleteEmployeeContractDetail(url: string, employeeContractId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });

    return this.http
      .delete(url + '?employeeContractId=' + employeeContractId, options)
      .map((response: Response) => {
        const result = response.json();
        if (result) {
          return result;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Get All"
  GetDataByEmployeeIdAndOfficeId(
    url: string,
    employeeid: number,
    OfficeId: number
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?employeeid=' + employeeid + '&officeid=' + OfficeId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "GET ALL EMPLOYEE HISTORY"
  GetAllMonthlyEmployeeAttendanceReport(
    url: string,
    employeeid: number,
    year: number,
    month: number,
    officeid: number
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
          '?EmployeeId=' +
          employeeid +
          '&year=' +
          year +
          '&month=' +
          month +
          '&officeid=' +
          officeid,
        options
      )
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET ALL EMPLOYEE HISTORY"

  //#region "Get All Details by paramaters"
  GetAlltotalCountsForSummary(url: string, data: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url +
          '?OfficeId=' +
          data.OfficeId +
          '&EmployeeTypeId=' +
          data.EmployeeTypeId +
          '&RecordType=' +
          data.RecordTypeId +
          '&CurrencyId=' +
          data.CurrencyId +
          '&AllowanceId=' +
          data.AllowanceId +
          '&DeductionId=' +
          data.DeductionId +
          '&Year=' +
          data.Year +
          '&Month=' +
          data.Month,
        options
      )
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  //#endregion

  //#region "GET ALL EMPLOYEE HISTORY"
  GetAllEmployeePensionReport(
    url: string,
    employeeId: number,
    officeId: number,
    financialYearId: number[],
    currencyId: number
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
          '?EmployeeId=' +
          employeeId +
          '&OfficeId=' +
          officeId +
          '&FinancialYearId=' +
          financialYearId +
          '&CurrencyId=' +
          currencyId,
        options
      )
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }

  GetAllEmployeeSalaryTax(
    url: string,
    employeeId: number,
    officeId: number,
    financialYearId: number,
    currencyId: number
  ) {
    const model = {
      OfficeId: officeId,
      EmployeeId: employeeId,
      FinancialYearId: financialYearId,
      CurrencyId: currencyId
    };
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion "GET ALL EMPLOYEE HISTORY"

  //#region "Get Employee Salary Details"
  GetEmployeeSalaryDetails(
    url: string,
    officeId: any,
    year: any,
    month: any,
    employeeId: number
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
          '?OfficeId=' +
          officeId +
          '&year=' +
          year +
          '&month=' +
          month +
          '&EmployeeId=' +
          employeeId,
        options
      )
      .map((response: Response) => {
        const SalaryDetails = response.json();
        if (SalaryDetails) {
          return SalaryDetails;
        }
      })
      .catch(this.handleError);
  }

  //#endregion

  //#region "Get Employee Salary Tax Details"
  GetEmployeeTaxReport(
    url: string,
    officeId: number,
    employeeId: number,
    year: any
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
          '?OfficeId=' +
          officeId +
          '&EmployeeId=' +
          employeeId +
          '&FinancialYearId=' +
          year,
        options
      )
      .map((response: Response) => {
        const SalaryDetails = response.json();
        if (SalaryDetails) {
          return SalaryDetails;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Get Employee Data By EmployeeId"
  GetEmployeeDataByEmployeeId(url: string, employeeId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?EmployeeId=' + employeeId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "GET ALL By OfficeId, Month , Year"
  GetAllByOfficeIdMonthYear(
    url: string,
    officeId: number,
    month: number,
    year: number
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url + '?OfficeId=' + officeId + '&month=' + month + '&year=' + year,
        options
      )
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "ApprovalOrRejectEmployeeAppraisal"
  ApprovalOrRejectEmployeeAppraisal(
    url: string,
    employeeAppraisalDetailsId: number
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url + '?EmployeeAppraisalDetailsId=' + employeeAppraisalDetailsId,
        options
      )
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "ApprovalOrRejectEmployeeAppraisal"
  ApprovalAndRejectionEmployeeEvaluation(
    url: string,
    employeeEvaluationId: number
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?EmployeeEvaluationId=' + employeeEvaluationId, options)
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "ApprovalOrRejectEmployeeAppraisal"
  ApprovalAndRejectionInterviewForm(url: string, interviewDetailsId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?InterviewDetailsId=' + interviewDetailsId, options)
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "DeleteExitInterviewForm"
  DeleteExitInterviewForm(url: string, existInterviewDetailsId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?existInterviewDetailsId=' + existInterviewDetailsId, options)
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Get Advances By advance ID"
  GetAdvanceHIstory(url: string, AdvanceID: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?AdvanceID=' + AdvanceID, options)
      .map((response: Response) => {
        const doclist = response.json();
        if (doclist) {
          return doclist;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  //#region "Add By Model"
  GetBudgetLineByProjectId(url: string, projectId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, projectId, options)
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

export interface GeneralInfo {
  EmployeeName: string;
  FatherName: string;
  PermanentAddress: string;
  CurrentAddress: string;
  City: string;
  ProvinceId: number;
  CountryId: number;
  Phone: string;
  Email: string;
  SexId: number;
  DateOfBirth: any;
  Age: number;
  ReferBy?: string;
  HigherQualificationId?: number;
  ExperienceMonth?: number;
  ProfessionId?: number;
  PreviousWork?: string;
  ExperienceYear?: number;
  Remarks?: string;
  EmployeePhoto?: string;
  Resume?: string;
  OfficeId?: number;

  EmployeeTypeId: number;

  PassportNo: string;
  University: string;
  BirthPlace: string;
  IssuePlace: string;
  MaritalStatusId: number;
  PlaceOfBirth: string;
  TinNumber: number;
}

export class Documents {
  ID: number;
  DocumentName: string;
  DocumentFile: string;
}

export class SexTypes {
  SexId: number;
  SexName: string;
}

export class EmpDocuments {
  DocumentId: number;
  DocumentName: string;
  DocumentDate: string;
  DocumentFilePath: string;
  EmployeeID: number;
}

export class EmpHistory {
  HistoryID: number;
  EmployeeID: number;
  HistoryDate: any;
  Description: string;
}

export class DeleteDocument {
  DocumentId: any;
}

const deleteDocument: DeleteDocument = {
  DocumentId: ''
};

const sexTypes: SexTypes[] = [
  { SexId: 1, SexName: 'Male' },
  { SexId: 2, SexName: 'Female' },
  { SexId: 3, SexName: 'Other' }
];
