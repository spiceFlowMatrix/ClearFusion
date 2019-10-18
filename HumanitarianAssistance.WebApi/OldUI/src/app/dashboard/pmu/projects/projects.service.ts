import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions, RequestOptionsArgs } from '@angular/http';
import { Observable } from 'rxjs/Observable';

//#region "Questions"
export class Ques {
    ID: number;
    Question: string;
    Rating1: string;
    Rating2: string;
    Rating3: string;
    Rating4: string;
    Rating5: string;
}

let ques: Ques[] = [
    {
        'ID': 1,
        "Question": "abc",
        "Rating1": "string",
        "Rating2": "string",
        "Rating3": "string",
        "Rating4": "string",
        "Rating5": "string",
    },
    {
        'ID': 2,
        "Question": "xyz",
        "Rating1": "string",
        "Rating2": "string",
        "Rating3": "string",
        "Rating4": "string",
        "Rating5": "string",
    }
]
//#endregion "Questions"

//#region "Documents"
export class Documents {
    ID: number;
    DocumentName: string;
    DocumentType: number;
    DocumentFile: string;
}

let docs: Documents[] = [
    {
        "ID": 1,
        "DocumentName": "Doc1",
        "DocumentType": 1,
        "DocumentFile": "string",
    },
    {
        "ID": 2,
        "DocumentName": "Doc2",
        "DocumentType": 2,
        "DocumentFile": "string",
    },
    {
        "ID": 3,
        "DocumentName": "Doc3",
        "DocumentType": 1,
        "DocumentFile": "string",
    },
]
//#endregion "Documents"

//#region "Beneficiaries"
export class Beneficiaries1 {
    ID: number;
    SerialNo: number;
    Name: string;
    FName: string;
    Province: string;
    District: string;
    Village: string;
    IDNo: number;
    Age: number;
    Sex: number;
    MaritalStatus: number;
    Referrer: string;
    ReferDate: string;
    TypeOfCase: string;
    TelephoneNo: number;
}

let beneficiaries: Beneficiaries1[] = [
    {
        "ID": 1,
        "SerialNo": 1,
        "Name": "AlpitG",
        "FName": "Alpit",
        "Province": "Nagpur",
        "District": "Nagpur",
        "Village": "Akola",
        "IDNo": 1,
        "Age": 22,
        "Sex": 1,
        "MaritalStatus": 1,
        "Referrer": "referrer1",
        "ReferDate": "03/11/2017",
        "TypeOfCase": "case1",
        "TelephoneNo": 9076861868,
    },
    {
        "ID": 2,
        "SerialNo": 2,
        "Name": "SurajM",
        "FName": "Suraj",
        "Province": "Chattisgarh",
        "District": "Bijapur",
        "Village": "Raipur",
        "IDNo": 1,
        "Age": 23,
        "Sex": 1,
        "MaritalStatus": 1,
        "Referrer": "referrer2",
        "ReferDate": "02/21/2017",
        "TypeOfCase": "case2",
        "TelephoneNo": 8857686868,
    }
]
//#endregion "Beneficiaries"

//#region "Project Activities"
export class Activity {
    // ID: number;
    ActivityDesc: string;
    PlannedStartDate: string;
    PlannedEndDate: string;
    BudgetLine: number;
    Resource: number;
    LocationOfActivity: number;
    ActualStartDate: string;
    ActualEndDate: string;
    ImplementationMethod: string;
    Challenges: string;
    OvercomingChallenges: string;
    DeviationJustification: string;
    RecurringType: string;
    RecurringDay: number;
    RecurringMonth: number;
    RecurringWeekday: string;
    Complete: boolean;
    Monitoring: boolean;
    TaskType: string;
}

let activity: Activity[] = [
    {
        // "ID": 1,
        "ActivityDesc": "Planning",
        "PlannedStartDate": "01-05-2017",
        "PlannedEndDate": "02-06-2017",
        "BudgetLine": 1,
        "Resource": 1,
        "LocationOfActivity": 1,
        "ActualStartDate": "02-07-2017",
        "ActualEndDate": "03-08-2017",
        "ImplementationMethod": "Waterfall",
        "Challenges": "deadline",
        "OvercomingChallenges": "agile",
        "DeviationJustification": "string",
        "RecurringType": "Weekly",
        "RecurringDay": 1,
        "RecurringMonth": 9,
        "RecurringWeekday": "Wednesday",
        "Complete": true,
        "Monitoring": false,
        "TaskType": "string",
    }
]
//#endregion "Project Activities"

//#region "add-new dropdown data"
export class MeasureType {
    MeasureTypeId: number;
    MeasureTypeName: string;
}
export class ProjectType {
    ProjectTypeId: number;
    ProjectTypeName: string;
}

let measuretype: MeasureType[] = [
    { MeasureTypeId: 1, MeasureTypeName: "Qualitative" },
    { MeasureTypeId: 2, MeasureTypeName: "Quantitative" },
    { MeasureTypeId: 3, MeasureTypeName: "Qualitative & Quantitative" }
]

let projecttype: ProjectType[] = [
    { ProjectTypeId: 1, ProjectTypeName: "vCIOPro" },
    { ProjectTypeId: 2, ProjectTypeName: "Child Day Care" },
    { ProjectTypeId: 3, ProjectTypeName: "Contrucks" }
]
//#endregion "add-new dropdown data"

//#region "project-activities dropdown data"
export class ActivityLocationType {
    ActivityLocationTypeId: number;
    ActivityLocationTypeName: string;
}

export class BudgetType {
    BudgetTypeId: number;
    BudgetTypeName: string;
}

export class ResourceType {
    ResourceTypeId: number;
    ResourceTypeName: string;
}

export class RatingType {
    RatingTypeId: number;
    RatingTypeName: string;
}

export class DayOfMonth {
    dayOfMonthId: number;
    dayOfMonthName: number;
}

export class Month {
    MonthId: number;
    MonthName: string;
}

let dayOfMonth: DayOfMonth[] = [
    { dayOfMonthId: 1, dayOfMonthName: 1 },
    { dayOfMonthId: 2, dayOfMonthName: 2 },
    { dayOfMonthId: 3, dayOfMonthName: 3 },
    { dayOfMonthId: 4, dayOfMonthName: 4 },
    { dayOfMonthId: 5, dayOfMonthName: 5 },
    { dayOfMonthId: 6, dayOfMonthName: 6 },
    { dayOfMonthId: 7, dayOfMonthName: 7 },
    { dayOfMonthId: 8, dayOfMonthName: 8 },
    { dayOfMonthId: 9, dayOfMonthName: 9 },
    { dayOfMonthId: 10, dayOfMonthName: 10 }
];

let month: Month[] = [
    { MonthId: 1, MonthName: "January" },
    { MonthId: 2, MonthName: "February" },
    { MonthId: 3, MonthName: "March" },
    { MonthId: 4, MonthName: "April" },
    { MonthId: 5, MonthName: "May" },
    { MonthId: 6, MonthName: "June" },
    { MonthId: 7, MonthName: "July" },
    { MonthId: 8, MonthName: "August" },
    { MonthId: 9, MonthName: "September" },
    { MonthId: 10, MonthName: "October" },
    { MonthId: 11, MonthName: "November" },
    { MonthId: 12, MonthName: "December" }
];

let ratingtype: RatingType[] = [
    { RatingTypeId: 1, RatingTypeName: 'ratingtype1' },
    { RatingTypeId: 2, RatingTypeName: 'ratingtype1' }
];

let budgettype: BudgetType[] = [
    { BudgetTypeId: 1, BudgetTypeName: 'Master Budget' },
    { BudgetTypeId: 2, BudgetTypeName: 'Operating Budget' },
    { BudgetTypeId: 3, BudgetTypeName: 'Cash Flow Budget' },
    { BudgetTypeId: 4, BudgetTypeName: 'Financial Budget' },
    { BudgetTypeId: 5, BudgetTypeName: 'Static Budget' }
];

let resourcetype: ResourceType[] = [
    { ResourceTypeId: 1, ResourceTypeName: 'E0001-Wasim Khan' },
    { ResourceTypeId: 2, ResourceTypeName: 'E0002-Salman Khan' },
    { ResourceTypeId: 3, ResourceTypeName: 'E0003-Amir Pathan' },
    { ResourceTypeId: 4, ResourceTypeName: 'E0004-Rahul Khan' },
    { ResourceTypeId: 4, ResourceTypeName: 'E0005-Sam Ansari' }
];

let activitylocationtype: ActivityLocationType[] = [
    { ActivityLocationTypeId: 1, ActivityLocationTypeName: 'TES-TestOffice' }
];
//#endregion "project-activities dropdown data"

//#region "activities popup"
export class ActivitiesData {
    ActivityDesc: string;
    PlannedStartDate: string;
    PlannedEndDate: string;
    BudgetLine: string;
    Resource: string;
    LocationOfActivity: string;
    TaskType: string;
}

let activitiesdata = {
    "ActivityDesc": "",
    "PlannedStartDate": "",
    "PlannedEndDate": "",
    "BudgetLine": "",
    "Resource": "",
    "LocationOfActivity": "",
    "TaskType": ""
}
//#endregion "activitiesdata"

//#region "monitoring popup"
export class MonitoringData {
    Rating: string;
    VerificationSource: string;
    Strengths: string;
    Weeknesses: string;
    Challenges: string;
    Recommendations: string;
    Comments: string;
    FrequencyOfMonitoring: string;
}

let monitoringdata = {
    "Rating": "",
    "VerificationSource": "",
    "Strengths": "",
    "Weeknesses": "",
    "Challenges": "",
    "Recommendations": "",
    "Comments": "",
    "FrequencyOfMonitoring": ""
}
//#endregion "monitoringdata"

@Injectable()
export class ProjectsService {

    constructor(private http: Http) { }

    getQues() {
        return ques;
    }

    getDocs() {
        return docs;
    }

    getBeneficiaries() {
        return beneficiaries;
    }

    getActivities() {
        return activity;
    }

    getMeasureType() {
        return measuretype;
    }

    getProjectType() {
        return projecttype;
    }

    getActivitiesData() {
        return activitiesdata;
    }

    getBudgetType() {
        return budgettype;
    }

    getResourceType() {
        return resourcetype;
    }

    getLocationType() {
        return activitylocationtype;
    }

    getMonitoringData() {
        return monitoringdata;
    }

    getRatingType() {
        return ratingtype;
    }

    getDayOfMonth() {
        return dayOfMonth;
    }

    getMonth() {
        return month;
    }

    //#region "Get the Assigned Employees in Budget Line"
    GetSelectedEmployeeContract(url: string, OfficeId: number, ProjectId: number, BudgetLineId: number, EmployeeId: number) {
        let Myheaders = new Headers();
        Myheaders.append("Authorization", "Bearer " + localStorage.getItem("authenticationtoken"));
        let options = new RequestOptions({ headers: Myheaders });
        return this.http.get(url + "?OfficeId=" + OfficeId + "&ProjectId=" + ProjectId + "&BudgetLineId=" + BudgetLineId + "&EmployeeId=" + EmployeeId, options)
            .map((response: Response) => {
                let codelist = response.json();
                if (codelist) {
                    return codelist;
                }
            }).catch(this.handleError);
    }
    //#endregion

    //#region "Get the Assigned Employees in Budget Line"
    GetAllEmployeesInBudgetLine(url: string, OfficeId: number, ProjectId: number, BudgetLineId: number) {
        let Myheaders = new Headers();
        Myheaders.append("Authorization", "Bearer " + localStorage.getItem("authenticationtoken"));
        let options = new RequestOptions({ headers: Myheaders });
        return this.http.get(url + "?OfficeId=" + OfficeId + "&ProjectId=" + ProjectId + "&BudgetLineId=" + BudgetLineId, options)
            .map((response: Response) => {
                let codelist = response.json();
                if (codelist) {
                    return codelist;
                }
            }).catch(this.handleError);
    }
    //#endregion

    //#region "AssignEmployeeToBudgetLine"
    AssignEmployeeToBudgetLine(url: string, EmployeeList: any) {
        let Myheaders = new Headers();
        Myheaders.append("Authorization", "Bearer " + localStorage.getItem("authenticationtoken"));
        let options = new RequestOptions({ headers: Myheaders });
        return this.http.post(url, EmployeeList, options)
            .map((response: Response) => {
                let codelist = response.json();
                if (codelist) {
                    return codelist;
                }
            }).catch(this.handleError);
    }
    //#endregion

    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }
}