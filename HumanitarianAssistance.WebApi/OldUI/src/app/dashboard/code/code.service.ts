import { Injectable } from '@angular/core';
import {
  Http,
  Headers,
  Response,
  RequestOptions,
  RequestOptionsArgs
} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {
  CurrencyCode,
  OfficeCode,
  OfficeCodefordelete,
  EmailSetting,
  LeaveReason,
  FinancialYear,
  Profession,
  Department
} from '../../model/code-model';
import { SalaryHeadModel } from './salary-head/salary-head.component';

// chartOfAccounts------------------------------------>
export class ChartOfAccountLevel {
  ID: number;
  AccountLevel: string;
  ChartOfAccountLevelDetails: ChartOfAccountLevelDetail[];
}

export class ChartOfAccountLevelDetail {
  ID: number;
  AccountCode: number;
  AccountName: string;
  AccountType: number;
}

export class AccountType {
  AccountTypeId: number;
  AccountTypeName: string;
}
// chartOfAccounts------------------------------------>

// analyticalCodes
export class AnalyticalCode {
  ID: number;
  Code: string;
  Donor: string;
  Program: string;
  Project: string;
  Area: string;
  Sector: string;
  Job: string;
  Description: string;
  Amount: string;
  Currency: string;
  Book: string;
  DonorCode: string;
  Status: boolean;
}

// analyticalCodes ---> Projects Tab-5
export class ProjectTab {
  ID: number;
  Code: string;
  Description: string;
  Program: string;
  Budget: number;
  ProjectStartDate: string;
  ProjectEndDate: string;
  ReceivedAmount: number;
  ProjectCurrency: string;
  Status: boolean;
}

//#region "Salary Head"
export class HeadType {
  HeadTypeId: number;
  HeadTypeName: string;
}
const headTypeDropdown: HeadType[] = [
  {
    HeadTypeId: 1,
    HeadTypeName: 'Allowances'
  },
  {
    HeadTypeId: 2,
    HeadTypeName: 'Deductibles'
  },
  {
    HeadTypeId: 3,
    HeadTypeName: 'General'
  }
];

//#endregion

// chartOfAccounts
// let accountTypeDropdowns: string[] = [
//     'Expandable', 'Non Expandable', 'Other'];

const accountLevelDropdowns: string[] = [
  'Main level',
  'Control level',
  'Sub level',
  'Input level'
];

const chartOfAccountLevels: ChartOfAccountLevel[] = [
  {
    ID: 1,
    AccountLevel: 'Main Level',
    ChartOfAccountLevelDetails: [
      {
        ID: 1,
        AccountCode: 1,
        AccountName: 'Saving',
        AccountType: 1
      },
      {
        ID: 2,
        AccountCode: 2,
        AccountName: 'Current',
        AccountType: 2
      }
    ]
  },
  {
    ID: 2,
    AccountLevel: 'Control Level',
    ChartOfAccountLevelDetails: [
      {
        ID: 1,
        AccountCode: 1,
        AccountName: 'Saving',
        AccountType: 2
      },
      {
        ID: 3,
        AccountCode: 3,
        AccountName: 'Other',
        AccountType: 1
      }
    ]
  },
  {
    ID: 3,
    AccountLevel: 'Sub Level',
    ChartOfAccountLevelDetails: [
      {
        ID: 1,
        AccountCode: 1,
        AccountName: 'Saving',
        AccountType: 2
      }
    ]
  },
  {
    ID: 4,
    AccountLevel: 'Input Level',
    ChartOfAccountLevelDetails: [
      {
        ID: 1,
        AccountCode: 1,
        AccountName: 'Saving',
        AccountType: 3
      },
      {
        ID: 2,
        AccountCode: 1,
        AccountName: 'Saving',
        AccountType: 1
      },
      {
        ID: 3,
        AccountCode: 1,
        AccountName: 'Current',
        AccountType: 2
      },
      {
        ID: 4,
        AccountCode: 4,
        AccountName: 'Current',
        AccountType: 3
      }
    ]
  }
];

// analyticalCodes
const analyticalCodes: AnalyticalCode[] = [
  {
    ID: 1,
    Code: 'Main',
    Donor: 'Main',
    Program: 'Main',
    Project: 'Main',
    Area: 'Main',
    Sector: 'Main',
    Job: 'Main',
    Description: 'Main',
    Amount: 'Main',
    Currency: 'Afg',
    Book: 'Main',
    DonorCode: 'Main',
    Status: true
  },
  {
    ID: 2,
    Code: 'Main',
    Donor: 'Main',
    Program: 'Main',
    Project: 'Main',
    Area: 'Main',
    Sector: 'Main',
    Job: 'Main',
    Description: 'Main',
    Amount: 'Main',
    Currency: 'Afg',
    Book: 'Main',
    DonorCode: 'Main',
    Status: true
  },
  {
    ID: 3,
    Code: 'Main',
    Donor: 'Main',
    Program: 'Main',
    Project: 'Main',
    Area: 'Main',
    Sector: 'Main',
    Job: 'Main',
    Description: 'Main',
    Amount: 'Main',
    Currency: 'USD',
    Book: 'Main',
    DonorCode: 'Main',
    Status: false
  },
  {
    ID: 4,
    Code: 'Main',
    Donor: 'Main',
    Program: 'Main',
    Project: 'Main',
    Area: 'Main',
    Sector: 'Main',
    Job: 'Main',
    Description: 'Main',
    Amount: 'Main',
    Currency: 'USD',
    Book: 'Main',
    DonorCode: 'Main',
    Status: true
  }
];

// analyticalCodes --> Projects Tab-5
const projectTabs: ProjectTab[] = [
  {
    ID: 1,
    Code: '12232',
    Description: 'ERP',
    Program: 'ERP-Tech',
    Budget: 500000,
    ProjectStartDate: '01/20/2017',
    ProjectEndDate: '03/29/2017',
    ReceivedAmount: 152100,
    ProjectCurrency: 'USD',
    Status: true
  },
  {
    ID: 2,
    Code: '23412',
    Description: 'ERP',
    Program: 'ERP-Tech',
    Budget: 625124,
    ProjectStartDate: '01/20/2017',
    ProjectEndDate: '05/29/2017',
    ReceivedAmount: 25000,
    ProjectCurrency: 'AFG',
    Status: true
  },
  {
    ID: 3,
    Code: '12232',
    Description: 'Health Care',
    Program: 'ERP-Tech',
    Budget: 2000000,
    ProjectStartDate: '04/19/2017',
    ProjectEndDate: '07/30/2017',
    ReceivedAmount: 120000,
    ProjectCurrency: 'USD',
    Status: true
  }
];

export class CurrencyData {
  CurrencyId: any;
  CurrencyCode: string;
  CurrencyName: string;
}

const currencydata: CurrencyData = {
  CurrencyId: '',
  CurrencyCode: '',
  CurrencyName: ''
};

export class OfficeData {
  OfficeId: any;
  OfficeCode: string;
  OfficeName: string;
  SupervisorName: string;
  PhoneNo: string;
  FaxNo: string;
  OfficeKey: string;
}

const officedata: OfficeData = {
  OfficeId: '',
  OfficeCode: '',
  OfficeName: '',
  SupervisorName: '',
  PhoneNo: '',
  FaxNo: '',
  OfficeKey: ''
};

export class EmailSettingData {
  EmailId: any;
  SenderEmail: string;
  EmailTypeId: any;
  EmailTypeName: string;
  SenderPassword: string;
  SmtpPort: any;
  SmtpServer: string;
  EnableSSL: boolean;
}

const emailsetting: EmailSettingData = {
  EmailId: '',
  SenderEmail: '',
  EmailTypeId: '',
  EmailTypeName: '',
  SenderPassword: '',
  SmtpPort: '',
  SmtpServer: '',
  EnableSSL: false
};

export class JournalCodeData {
  JournalCode: any;
  JournalName: string;
}

const journalcodedata = {
  JournalCode: '',
  JournalName: ''
};

export class DeleteJournalCode {
  JournalCode: any;
}
const deletejournalcode: DeleteJournalCode = {
  JournalCode: ''
};

// Alpit 28-11-17
export class AccountLevel {
  ID: number;
  AccountLevelName: string;
}

const accountLevels: AccountLevel[] = [
  {
    ID: 1,
    AccountLevelName: 'Main Level Account'
  },
  {
    ID: 2,
    AccountLevelName: 'Control Level Account'
  },
  {
    ID: 3,
    AccountLevelName: 'Sub Level Account'
  },
  {
    ID: 4,
    AccountLevelName: 'Input Level Account'
  }
];

// 20-12-2017
export class ProjectDetails {
  ProjectName: any;
  Description: any;
  StartDate: any;
  EndDate: any;
  CurrencyId: any;
  Budget: DoubleRange;
  ReceivableAmount: DoubleRange;
  PayableAmount: DoubleRange;
  CurrentBalance: DoubleRange;
  Status: number;
}

const projectDetail: ProjectDetails = {
  ProjectName: '',
  Description: '',
  StartDate: '',
  EndDate: '',
  CurrencyId: '',
  Budget: null,
  ReceivableAmount: null,
  PayableAmount: null,
  CurrentBalance: null,
  Status: null
};

const leavereasondata: LeaveReason = {
  LeaveReasonId: '',
  ReasonName: '',
  Unit: null
};

//#region "financial year"
const financialyeardata: FinancialYear = {
  FinancialYearId: '',
  StartDate: '',
  EndDate: '',
  FinancialYearName: '',
  Description: '',
  IsDefault: false
};
//#endregion

const professiondata: Profession = {
  ProfessionId: 0,
  ProfessionName: ''
};

const departmentdata: Department = {
  DepartmentId: 0,
  DepartmentName: '',
  OfficeId: null,
  OfficeName: ''
};

@Injectable()
export class CodeService {
  constructor(private http: Http) { }

  getHeadType(): HeadType[] {
    return headTypeDropdown;
  }

  // Account level dropdown
  getAccountLevels() {
    return accountLevels;
  }
  // Manage Chat of account

  // chartOfAccounts
  getChartOfAccountLevels() {
    return chartOfAccountLevels;
  }

  // Dropdown for main level account
  getAccountType(url: string) {
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

  getExchangeRate(url: string, model) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  getAccountLevelDropdowns() {
    return accountLevelDropdowns;
  }

  // analyticalCodes
  getAnalyticalCodes() {
    return analyticalCodes;
  }
  // analyticalCodes --> Project Tab-5
  getProjectTabs() {
    return projectTabs;
  }

  getCurrencyData(): CurrencyData {
    currencydata.CurrencyId = '';
    currencydata.CurrencyCode = '';
    currencydata.CurrencyName = '';
    return currencydata;
  }

  projectDetailModel(): ProjectDetails {
    projectDetail.ProjectName = '';
    projectDetail.Description = '';
    projectDetail.StartDate = '';
    projectDetail.EndDate = '';
    projectDetail.CurrencyId = '';
    projectDetail.Budget = null;
    projectDetail.ReceivableAmount = null;
    projectDetail.PayableAmount = null;
    projectDetail.CurrentBalance = null;
    projectDetail.Status = null;
    return projectDetail;
  }

  GetAllCodeList(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
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

  AssignEmployeeProjectPercentage(url: string, model) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllDetails(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map(res => res.json())
      .catch(this.handleError);
  }

  GetDetailsByIdParameterGeneric(
    url: string,
    parameterName: string,
    parameterValue: any
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?' + parameterName + '=' + parameterValue, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllBalanceAndIncomeExpenditureDetails(url: string, reportTypeId: number) {
    const userId = localStorage.getItem('UserId');
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?reportTypeId=' + reportTypeId, options)
      .map(res => res.json())
      .catch(this.handleError);
  }

  GetAllNotificationDetails(url: string) {
    const userId = localStorage.getItem('UserId');
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?userid=' + userId, options)
      .map(res => res.json())
      .catch(this.handleError);
  }

  GetAllBalanceSheetDetails(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllDetailsOfNotes(url: string, FinancialYearID, CurrencyId) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url +
        '?financialyearid=' +
        FinancialYearID +
        '&currencyid=' +
        CurrencyId,
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

  GetAllTaskAndActivityList(url: string, projectid: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?projectid=' + projectid, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllTransactionByProject(url: string, ProjectId: any, BudgetLineId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url + '?projectId=' + ProjectId + '&budgetLineId=' + BudgetLineId,
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

  GetAllBudgetLineDetails(url: string, projectId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?ProjectId=' + projectId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetEmployeeContractType(url: string, officeId: any, selectedTab: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url +
        '?officeId=' +
        officeId +
        '&EmployeeContractTypeId=' +
        selectedTab,
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

  GetAllBudgetLineReceivable(url: string, projectId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?projectId=' + projectId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllBudgetLineReceivableByProjectId(
    url: string,
    projectId: any,
    budgetlineid: any
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url + '?projectId=' + projectId + '&budgetlineid=' + budgetlineid,
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

  GetAllBudgetLineReceivedDetails(
    url: string,
    projectId: any,
    budgetlineid: any,
    receivableId: any
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
        '?projectId=' +
        projectId +
        '&budgetlineid=' +
        budgetlineid +
        '&receivableId=' +
        receivableId,
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

  GetAllBudgetLinePaidDetails(
    url: string,
    projectId: any,
    budgetlineid: any,
    payableId: any
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
        '?projectId=' +
        projectId +
        '&budgetLineId=' +
        budgetlineid +
        '&payableId=' +
        payableId,
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

  AddEditCurrencyCode(url: string, model: CurrencyCode) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const b = {
      CurrencyId: model.CurrencyId,
      CurrencyCode: model.CurrencyCode,
      CurrencyName: model.CurrencyName
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  getOfficeData(): OfficeData {
    officedata.OfficeId = '';
    officedata.OfficeCode = '';
    officedata.OfficeName = '';
    officedata.SupervisorName = '';
    officedata.PhoneNo = '';
    officedata.FaxNo = '';
    officedata.OfficeKey = '';
    return officedata;
  }

  AddEditOfficeCode(url: string, model: OfficeCode) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      OfficeId: model.OfficeId,
      OfficeCode: model.OfficeCode,
      OfficeName: model.OfficeName,
      SupervisorName: model.SupervisorName,
      PhoneNo: model.PhoneNo,
      FaxNo: model.FaxNo,
      OfficeKey: model.OfficeKey
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  DeleteOfficeCode(url: string, model: OfficeCodefordelete) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      OfficeId: model.OfficeId
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  getEmailSettingData(): EmailSettingData {
    emailsetting.EmailId = '';
    emailsetting.SenderEmail = '';
    emailsetting.EmailTypeId = '';
    emailsetting.EmailTypeName = '';
    emailsetting.SenderPassword = '';
    emailsetting.SmtpPort = '';
    emailsetting.SmtpServer = '';
    emailsetting.EnableSSL = false;
    return emailsetting;
  }

  AddEditEmailSetting(url: string, model: EmailSetting) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      EmailId: model.EmailId,
      SenderEmail: model.SenderEmail,
      EmailTypeName: model.EmailTypeName,
      EmailTypeId: model.EmailTypeId,
      SenderPassword: model.SenderPassword,
      SmtpPort: model.SmtpPort,
      SmtpServer: model.SmtpServer,
      EnableSSL: model.EnableSSL
    };

    return this.http
      .post(url, b, options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  AddEditJournalCode(url: string, model: JournalCodeData) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      JournalCode: model.JournalCode,
      JournalName: model.JournalName
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  DeleteJournalCode(url: string, model: DeleteJournalCode) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      JournalCode: model.JournalCode
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  // MainLevelAccount ---------- Alpit 30-11-2017
  getMainLevelAccounts(url: string) {
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

  // Add MainLevelAccount ---------- Alpit 30-11-2017
  AddMainLevelAccount(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    const obj = {
      AccountName: model.AccountName,
      AccountLevelId: model.AccountLevelId,
      AccountTypeId: model.AccountTypeId,
      AccountTypeName: model.AccountTypeName,
      ParentID: model.ParentID,
      DepRate: model.DepRate,
      DepMethod: model.DepMethod,
      AccountNote: model.AccountNote,
      MDCode: model.MDCode,
      Show: model.Show,
      ChartOfAccountCode: model.ChartOfAccountCode
    };
    return this.http
      .post(url, JSON.stringify(obj), options)
      .map((response: Response) => {
        const chartOfAccount = response.json();
        if (chartOfAccount) {
          return chartOfAccount;
        }
      })
      .catch(this.handleError);
  }

  // Edit ChartOfAccountDetail---------- Alpit 2-12-2017
  EditChartOfAccountDetail(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    let obj = null;
    if (model.ParentID === 0) {
      obj = {
        AccountName: model.AccountName,
        AccountCode: model.AccountCode,
        AccountTypeId: model.AccountTypeId,
        AccountLevelId: model.AccountLevelId + 1,
        ParentID: model.ParentID + 1
        // AccountTypeId: model.AccountTypeId,
        // AccountTypeName: model.AccountTypeName,
        // ParentID: model.ParentID,
        // DepRate: model.DepRate,
        // DepMethod: model.DepMethod,
        // AccountNote: model.AccountNote,
        // MDCode: model.MDCode,
        // Show: model.Show
      };
    } else {
      obj = {
        AccountName: model.AccountName,
        AccountCode: model.AccountCode,
        AccountTypeId: model.AccountTypeId,
        AccountLevelId: model.AccountLevelId,
        ParentID: model.ParentID
        // AccountTypeId: model.AccountTypeId,
        // AccountTypeName: model.AccountTypeName,
        // ParentID: model.ParentID,
        // DepRate: model.DepRate,
        // DepMethod: model.DepMethod,
        // AccountNote: model.AccountNote,
        // MDCode: model.MDCode,
        // Show: model.Show
      };
    }
    return this.http
      .post(url, JSON.stringify(obj), options)
      .map((response: Response) => {
        const chartOfAccount = response.json();
        if (chartOfAccount) {
          return chartOfAccount;
        }
      })
      .catch(this.handleError);
  }

  getJournalCodeData(): JournalCodeData {
    journalcodedata.JournalCode = '';
    journalcodedata.JournalName = '';
    return journalcodedata;
  }

  GetAllExchangeRate(url: string) {
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

  AddExchangeRate(url: string, model: any) {
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
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  EditExchangeRate(url: string, model: any) {
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
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  AddTaskDetails(url: string, model: any) {
    const obj = {
      TaskId: 0,
      TaskName: model.TaskName,
      Priority: model.Priority,
      Description: model.Description,
      ProjectId: model.ProjectId,
      Status: 'Active',
      ActivityMasterList: []
    };
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
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  AddProjectDetails(url: string, model: any) {
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
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  // bipul
  getLeaveReasonData(): LeaveReason {
    leavereasondata.LeaveReasonId = '';
    leavereasondata.ReasonName = '';
    leavereasondata.Unit = null;
    return leavereasondata;
  }

  // bipul
  AddEditLeaveReasonDetail(url: string, model: LeaveReason) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      LeaveReasonId: model.LeaveReasonId,
      ReasonName: model.ReasonName,
      Unit: model.Unit
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  //#region  "Financial Year"

  getFinancialYearData(): FinancialYear {
    financialyeardata.FinancialYearId = '';
    financialyeardata.StartDate = '';
    financialyeardata.EndDate = '';
    financialyeardata.FinancialYearName = '';
    financialyeardata.Description = '';
    financialyeardata.IsDefault = false;
    return financialyeardata;
  }

  AddEditFinancialYearDetail(url: string, model: FinancialYear) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });

    const a = new RequestOptions();
    const b = {
      FinancialYearId: model.FinancialYearId,
      StartDate: model.StartDate,
      EndDate: model.EndDate,
      FinancialYearName: model.FinancialYearName,
      Description: model.Description,
      IsDefault: model.IsDefault
    };

    return this.http
      .post(url, JSON.stringify(b), options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }
  //#endregion

  getProfessionData(): Profession {
    professiondata.ProfessionId = 0;
    professiondata.ProfessionName = '';
    return professiondata;
  }

  AddEditProfessionDetail(url: string, model: Profession) {

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
        const profession = response.json();
        if (profession) {
          return profession;
        }
      })
      .catch(this.handleError);
  }

  getDepartmentData(): Department {
    departmentdata.DepartmentId = 0;
    departmentdata.DepartmentName = '';
    departmentdata.OfficeId = null;
    departmentdata.OfficeName = '';
    return departmentdata;
  }

  AddEditDepartmentDetail(url: string, model: Department) {
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
        const department = response.json();
        if (department) {
          return department;
        }
      })
      .catch(this.handleError);
  }

  AddEditDetails(url: string, model: any) {
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
        const res = response.json();
        if (res) {
          return res;
        }
      })
      .catch(this.handleError);
  }

  GetAppraisalQuestions(url: string, officeId?: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?OfficeId=' + officeId, options)
      .map((response: Response) => {
        const res = response.json();
        if (res) {
          return res;
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

  GetDetailByPassingModel(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  AddEditAttendanceGroup(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, model, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Server error');
  }
}

export class EditChartOfAccount {
  AccountCode: number;
  AccountName: string;
  AccountTypeId: number;
}

// MainLevelAccount     ---important
export class MainLevelAccount {
  ParentID: number;
  AccountLevelId: number;
  AccountTypeName: 'Main Level Accounts';
  AccountNote: number;
  AccountName: string;
  AccountTypeId: number;
  Show: boolean;
  MDCode: string;
  // DepMethod: string;
  // DepRate: number;
}

// ControlLevelAccount     ---important
export class ControlLevelAccount {
  ParentID: number;
  AccountLevelId: number;
  AccountTypeName: 'Control Level Accounts';
  AccountNote: number;
  MainLevel: number;
  AccountName: string;
  AccountTypeId: number;
  Show: boolean;
  MDCode: string;

  AccountCode: any;
  AccountCodePref: any;
}

// SubLevelAccount     ---important
export class SubLevelAccount {
  ParentID: number;
  AccountLevelId: number;
  AccountTypeName: 'Sub Level Accounts';
  AccountNote: number;
  ControlLevel: number;
  AccountName: string;
  AccountTypeId: number;
  Show: boolean;
  MDCode: string;

  AccountCode: any;
  AccountCodePref: any;
}

// InputLevelAccount     ---important
export class InputLevelAccount {
  ParentID: number;
  AccountLevelId: number;
  AccountTypeName: 'Input Level Accounts';
  AccountNote: number;
  SubLevel: number;
  AccountName: string;
  AccountTypeId: number;
  Show: boolean;
  MDCode: string;

  AccountCode: any;
  AccountCodePref: any;
}

// MainLevelAccount
// tslint:disable-next-line:class-name
export class chartOfAccountsData {
  ID: number;
  ParentID: number;
  AccountName: string;
  AccountCode: number;
  AccountTypeId: number;
  AccountLevelId: number;
  ChartOfAccountCode: number;
}
