
export interface GainLossReport {
  AccountId: number;
  AccountCode: string;
  AccountName: string;
  AccountCodeName: string;
  BalanceOnOriginalDate: number;
  BalanceOnCurrentDate: number;
  GainLossAmount: number;
}

export interface IGainLossReportFilter {
  ToCurrencyId: number;
  ComparisionDate: any;
  ToDate: any;
  FromDate: any;
  OfficeIdList: number[];
  JournalIdList: number[];
  ProjectIdList: number[];
  AccountIdList: number[];
}

export interface IGainLossAddVoucherForm {
  CurrencyId: number;
  JournalId: number;
  CreditAccount: number;
  DebitAccount: number;
  Amount: number;
  VoucherType: number;
  OfficeId: number;
}

export interface IAccountList {
  AccountCode: number;
  AccountName: string;
  ChartOfAccountNewCode: string;
}

export interface ICurrencyList {
  CurrencyId: number;
  CurrencyCode: string;
  CurrencyName: string;
}

export interface IOfficeList {
  OfficeId: number;
  OfficeName: string;
  IsChecked: boolean;
}

export interface IJournalList {
  JournalCode: number;
  JournalName: string;
  JournalType: string;
  IsChecked: boolean;
}

export interface IProjectList {
  ProjectId: number;
  ProjectName: string;
  ProjectCode: string;
  IsChecked: boolean;
}

export interface IVoucherTypeList {
  VoucherTypeId: number;
  VoucherTypeName: string;
}

export interface IGainLossVoucherList {
  VoucherId: number;
  VoucherName: string;
  JournalName: string;
  VoucherDate: any;

  IsDeleted: boolean;
  IsError: boolean;
  IsLoading: boolean;
}
