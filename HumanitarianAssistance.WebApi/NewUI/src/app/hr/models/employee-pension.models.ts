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
