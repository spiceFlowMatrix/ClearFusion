import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material';
import { GLOBAL } from 'src/app/shared/global';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeePensionService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient,
    private dialog: MatDialog,
    private globalSharedService: GlobalSharedService,

  ) {}

  //#region "GetCurrencyList"
  GetCurrencyList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency
    );
  }
  //#endregion
  //#region "GetFinancialYearList"
  GetFinancialYearList(): any {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAllFinancialYearDetail
    );
  }
  //#endregion
  //#region "Add By Model"
  GetAllPensionList(model: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_HR_EmployeePensionReport,
      model
    );
  }
  //#endregion
  //#region "getAllTaxlist"
  GetAllSalaryTaxList(model: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_Hr_EmployeeSalaryTaxDetails,
      model
    );
  }
  //#endregion
 // #region "GetEmloyeeTaxcaluclution"
 GetEmployeeTaxCalculation(model: any) {
  return this.globalService.post(
    this.appurl.getApiUrl() + GLOBAL.API_Hr_EmployeeTaxCalculation,
    model
  );
 }
 //#endregion

 //#region "DownloadPDF"
 DownloadPDF(pdfmodel: any) {
  if (pdfmodel != null && pdfmodel !== undefined) {
    this.globalSharedService
      .getFile(
        this.appurl.getApiUrl() +
          GLOBAL.API_Pdf_GetEmployeePensionPdf,
        pdfmodel
      )
      .pipe()
      .subscribe();
  }
}
//#endregion
//#region "DownloadPDF"
DownloadTaxPDF(pdfmodel: any) {
 if (pdfmodel != null && pdfmodel !== undefined) {
   this.globalSharedService
     .getFile(
       this.appurl.getApiUrl() +
         GLOBAL.API_Pdf_GetEmployeeSalaryTaxPdf,
       pdfmodel
     )
     .pipe()
     .subscribe();
 }
}
//#endregion

}
