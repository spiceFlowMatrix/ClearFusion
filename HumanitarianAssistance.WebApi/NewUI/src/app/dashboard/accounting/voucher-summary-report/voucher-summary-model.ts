export interface VoucherSummaryFilterModel {
  Accounts: number[];
  Offices: number[];
  Journals: number[];
  Projects: number[];
  BudgetLines: number[];
  ProjectJobs: number[];
  Currency: number;
  RecordType: number;
  pageSize?: number;
  pageIndex?: number;
}

export interface IReportVoucherModel {
  VoucherNo: number;
  VoucherCode: string;
  VoucherDescription: string;
  Date: any;
  VoucherTransactions: IVoucherSummaryTransaction[];
}

export interface IVoucherSummaryTransaction {
  AccountCode: any;
  AccountName: any;
  CurrencyName: any;
  TransactionDescription: any;
  Amount: any;
  TransactionType: any;
}

export interface BudgetlineListModel {
  BudgetLineId: number;
  BudgetCode: string;
  BudgetName: string;
}

export interface VoucherSummaryTransactionModel {
  AccountCode: string;
  AccountName: string;
  CurrencyName: string;
  TransactionDescription: string;
  Amount: number;
  TransactionType: string;
}
