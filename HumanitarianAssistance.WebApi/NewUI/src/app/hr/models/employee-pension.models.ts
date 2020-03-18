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
  EmployeeId?: string;
  TaxPayerIdentificationNumber: string;
  NameOfBusiness: string;
  AddressOfBusiness: string;
  TelephoneNumber: string;
  EmailAddressEmployer: string;

  EmployeeName: string;
  EmployeeTaxpayerIdentification: string;
  EmployeeAddress: string;
  TelephoneNumberEmployee: string;
  EmailAddressEmployee: string;

  AnnualTaxPeriod: string;
  DatesOfEmployeement: string;
  TotalWages: string;
  TotalTax: string;

  OfficerName: string;
  Position: string;
  Date: string;
}

export interface IPensionDetails {
  EmployeeID?: number;
  PensionDate?: Date;
  PensionDetail?: IPensionList[];
}

export interface IPensionList {
  PensionId?: number;
  CurrencyId: number;
  Amount: number;
}
