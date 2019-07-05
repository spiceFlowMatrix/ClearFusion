import { Injectable } from '@angular/core';
import {
  Http,
  Headers,
  Response,
  RequestOptions,
} from '@angular/http';
import { Observable } from 'rxjs/Observable';

export class Document {
  ID: any;
  DocumentName: string;
  DocumentFilePath?: string;
  DocumentDate?: string;
  VoucherNo?: string;
}

// Ledger Class
export class Ledger {
  AccountCode: number;
  ChartAccountName: string;
  CurrencyName: string;
  transactionlist: Transactionlist[];
}

export class Transactionlist {
  TransactionNo: number;
  AccountName: string;
  TransactionDate: string;
  DebitAmount: number;
  CreditAmount: number;
  VoucherNo: number;
  Description: string;
}

// Journal
export class JournalVoucherModel {
  JournalCode: number;
  AccountCode: number;
  Amount: number;
  TransactionNo: number;
  TransactionDate: string;
  TransactionType: string;
  VoucherNo: number;
}

// FOR ADVANCE DEDUCTION

export class AdvanceDeductionClass {
  ID: number;
  Currency: string;
  Office: string;
  Month: string;
  AdvanceDeductionSalarys: AdvanceDeductionSalary[];
}

export class AdvanceDeductionSalary {
  ID: number;
  EmployeeName: string;
  NetSalaryAdvance: number;
  AdvanceDeduction: number;
  NetSalary: number;
}

const advanceDeduction: AdvanceDeductionClass[] = [
  {
    ID: 1,
    Currency: 'AFG-AFG',
    Office: 'TestOffice',
    Month: '2017/11/17',
    AdvanceDeductionSalarys: [
      {
        ID: 4,
        EmployeeName: 'Naval Bhatt',
        NetSalaryAdvance: 50000,
        AdvanceDeduction: 10000,
        NetSalary: 40000
      },
      {
        ID: 4,
        EmployeeName: 'Fuster Cluck',
        NetSalaryAdvance: 60000,
        AdvanceDeduction: 10000,
        NetSalary: 50000
      },
      {
        ID: 4,
        EmployeeName: 'Hamza',
        NetSalaryAdvance: 70000,
        AdvanceDeduction: 40000,
        NetSalary: 30000
      }
    ]
  },
  {
    ID: 2,
    Currency: 'USD-USD',
    Office: 'TestOffice1',
    Month: '2017/11/18',
    AdvanceDeductionSalarys: [
      {
        ID: 5,
        EmployeeName: 'Rohit Grover',
        NetSalaryAdvance: 50000,
        AdvanceDeduction: 10000,
        NetSalary: 40000
      },
      {
        ID: 5,
        EmployeeName: 'Bipul',
        NetSalaryAdvance: 60000,
        AdvanceDeduction: 10000,
        NetSalary: 50000
      },
      {
        ID: 5,
        EmployeeName: 'Shubham',
        NetSalaryAdvance: 70000,
        AdvanceDeduction: 40000,
        NetSalary: 30000
      },
      {
        ID: 5,
        EmployeeName: 'Alpit',
        NetSalaryAdvance: 70000,
        AdvanceDeduction: 40000,
        NetSalary: 30000
      }
    ]
  }
];

// END ADVANCE DEDUCTION

// FOR Financial-Report
export class Tab {
  id: number;
  text: string;
}

// tslint:disable-next-line:class-name
export class FinancialReport_IncomeClass {
  ID: number;
  Description: string;
  Notes: number;
  Balance: number;
  Currency: string;
}

const financial_Income: FinancialReport_IncomeClass[] = [
  {
    ID: 10,
    Description: 'Currency Exchange Loss',
    Notes: 15,
    Balance: 480,
    Currency: 'USD-USD'
  },
  {
    ID: 11,
    Description: 'Currency Exchange Loss',
    Notes: 15,
    Balance: 480,
    Currency: 'AFG-AFG'
  },
  {
    ID: 12,
    Description: 'Currency Exchange Loss',
    Notes: 15,
    Balance: 480,
    Currency: 'AFG-AFG'
  }
];

// End Financial-Report

@Injectable()
export class AccountsService {
  constructor(private http: Http) {}

  // Document Function

  getAdvanceDeduction() {
    return advanceDeduction;
  }

  getFinancial_Income() {
    return financial_Income;
  }

  // Voucher Services

  GetAllVoucherDetailsByFilter(url: string, filterModel: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, filterModel, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllVoucherDetails(url: string) {
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

  AddVoucher(url: string, model: any) {
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

  EditVoucher(url: string, model: any) {
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

  GetEmployeePayrollDetails(url: string, employeeid: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?employeeid=' + employeeid, options)
      .map((response: Response) => {
        const voucher = response.json();
        if (voucher) {
          return voucher;
        }
      })
      .catch(this.handleError);
  }

  DeleteVoucher(url: string, VoucherNo: any, UserId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?VoucherNo=' + VoucherNo + '&UserId=' + UserId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  // Voucher Document Services
  GetVoucherDocumentDetails(url: string, VoucherNo) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?VoucherNo=' + VoucherNo, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetEmployeeDocumentDetails(url: string, EmployeeId) {
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

  AddVoucherDocument(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    const obj: any = {
      DocumentName: model.DocumentName,
      FilePath: model.DocumentFilePath,
      DocumentDate: model.DocumentDate,
      VoucherNo: model.VoucherNo
    };

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

  AddEmployeeDocument(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    const obj: any = {
      DocumentName: model.DocumentName,
      FilePath: model.DocumentFilePath,
      DocumentDate: model.DocumentDate,
      EmployeeID: model.EmployeeId,
      DocumentType: model.DocumentType
    };

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

  AddProjectDocument(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    const obj: any = {
      DocumentName: model.DocumentName,
      FilePath: model.DocumentFilePath,
      DocumentDate: model.DocumentDate,
      ProjectId: model.ProjectId
    };

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

  // Voucher Transaction Services

  // Dropdown Bind For Credit & Debit Accounts
  GetAccountDetails(url: string) {
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

  // Dropdown Bind For Credit & Debit Accounts
  // GetLevelFourAccountDetails(url: string) {
  //     let Myheaders = new Headers();
  //     Myheaders.append("Authorization", "Bearer " + localStorage.getItem("authenticationtoken"));
  //     let options = new RequestOptions({ headers: Myheaders });
  //     return this.http.get(url, options)
  //         .map((response: Response) => {
  //             let codelist = response.json();
  //             if (codelist) {
  //                 return codelist;
  //             }
  //         }).catch(this.handleError);
  // }

  GetDetailByVoucherNo(url: string, VoucherNo) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?VoucherNo=' + VoucherNo, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllVoucherByOfficeId(url: string, OfficeId) {
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

  AddVoucherTransaction(url: string, model: any) {
    const date = new Date(model.TransactionDate);
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    const obj = {
      DebitAccount: model.DebitAccount,
      CreditAccount: model.CreditAccount,
      Amount: model.Amount,
      Description: model.Description,
      // TransactionDate: date.toUTCString(),
      VoucherNo: model.VoucherNo,
      CurrencyId: model.CurrencyId,
      FinancialYearId: model.FinancialYearId,
      AccountNo: model.AccountNo,
      Debit: model.Debit,
      Credit: model.Credit
    };

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

  EditVoucherTransaction(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    const obj = {
      DebitAccount: model.DebitAccount,
      CreditAccount: model.CreditAccount,
      Amount: model.Amount,
      Description: model.Description,
      // TransactionDate: model.TransactionDate,
      VoucherNo: model.VoucherNo,
      TransactionId: model.TransactionId,
      CurrencyId: model.CurrencyId,
      FinancialYearId: model.FinancialYearId,

      AccountNo: model.AccountNo,
      Debit: model.Debit,
      Credit: model.Credit
    };

    return this.http
      .post(url, JSON.stringify(obj), options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  DeleteVoucherTransactionDetails(url: string, Id: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?Id=' + Id, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  DeleteCategoryPopulator(url: string, categoryPopulatorId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?categoryPopulatorId=' + categoryPopulatorId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  // Journal Services

  GetAllJournalDetails(url: string, model: any) {
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

  // Ledger Services

  GetAllLedgerDetails(url: string, model: any) {
    const obj = {
      CurrencyId: model.CurrencyId,
      accountLists: model.accountLists,
      RecordType: model.RecordType,
      AccountId: model.AccountId,
      FromDate: model.FromDate,
      ToDate: model.ToDate,
      OfficeIdList: model.OfficeIdList
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
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  // Trial Balance Services

  GetAllTrailBalance(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    Myheaders.append('Content-Type', 'application/json');
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
  GetAllDetailsByModel(url: string, model: any) {
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
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetEmployeeSalaryVoucher(
    url: string,
    employeeid: number,
    month: number,
    year: number
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url + '?employeeid=' + employeeid + '&month=' + month + '&year=' + year,
        options
      )
      .map((response: Response) => {
        const voucher = response.json();
        if (voucher) {
          return voucher;
        }
      })
      .catch(this.handleError);
  }

  ReverseEmployeeSalaryVoucher(url: string, VoucherNo: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?VoucherNo=' + VoucherNo, options)
      .map((response: Response) => {
        const voucher = response.json();
        if (voucher) {
          return voucher;
        }
      })
      .catch(this.handleError);
  }

  //#region perform sum on array objects
  sumOfListInArray(items, prop) {
    if (items == null) {
      return 0;
    }
    return items.reduce(function(a, b) {
      return b[prop] == null ? a : a + b[prop];
    }, 0);
  }
  //#endregion

  AddVoucherTransactionList(url: string, model: any) {
    const date = new Date(model.TransactionDate);
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

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Server error');
  }
}
