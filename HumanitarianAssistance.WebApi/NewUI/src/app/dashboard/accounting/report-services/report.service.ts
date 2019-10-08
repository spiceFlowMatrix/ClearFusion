import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) { }

  GetAllCurrencyCodeList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency
    );
  }

  GetAllOfficeCodeList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice
    );
  }

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

  GetAllJournalDetails(journalFilter) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_AccountReports_GetJournalVoucherDetails,
      journalFilter
    );
  }

  sumOfListInArray(items, prop) {
    if (items == null) {
      return 0;
    }
    return items.reduce(function(a, b) {
      return b[prop] == null ? a : a + b[prop];
    }, 0);
  }

  GetAllJournalCodeList() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllJournalDetail
    );
  }

  GetAccountDetails() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Accounting_GetAccountDetails
    );
  }

  GetAllLedgerDetails(journalFilter) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_AccountReports_GetAllLedgerDetails,
      journalFilter
    );
  }

  GetAllTrailBalance(trialBalanceFilter) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_AccountReports_GetTrialBalanceReport,
      trialBalanceFilter
    );
  }
}
