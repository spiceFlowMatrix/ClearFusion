export class EmployeePensionReportModel {
  Year: any;
  Date: any;
  GrossSalary: any;
  PensionRate: any;
  PensionDeduction: any;
  Profit: any;
  Total: any;
}
export interface PensionReportModel {
  Year: any;
  PensionReportList: PensionReportList[];
}
export interface PensionReportList {
  Date: any;
  GrossSalary: any;
  PensionRate: any;
  PensionDeduction: any;
  Profit: any;
  Total: any;
}

export interface CurrencyCodeModel {
  CurrencyId: number;
  CurrencyCode: string;
  CurrencyName: string;
}
export class EmployeePensionReportPdfModel {
  EmployeeId: any;
  EmployeeName: string;
  Currency: string;
  PensionReportModel: PensionReportModel[];
  PensionDeductionTotal: any;
  PensionProfitTotal: any;
  Total: any;
}
export interface PensionDetailModel {
  CurrencyId: any;
  Amount: any;
}
export class EmployeeTaxReportModel {
  TaxPayerIdentificationNumber: any;
  NameOfBusiness: any;
  AddressOfBusiness: any;
  TelephoneNumber: any;
  EmailAddressEmployer: any;

  EmployeeName: any;
  EmployeeTaxpayerIdentification: any;
  EmployeeAddress: any;
  TelephoneNumberEmployee: any;
  EmailAddressEmployee: any;

  AnnualTaxPeriod: any;
  DatesOfEmployeement: any;
  TotalWages: any;
  TotalTax: any;

  OfficerName: any;
  Position: any;
  Date: any;
}
