import { IBudgetLineModel } from 'src/app/dashboard/project-management/project-list/budgetlines/models/budget-line.models';

export interface IVoucherListFilterModel {
  FilterValue: any;

  VoucherNoFlag: boolean;
  ReferenceNoFlag: boolean;
  DescriptionFlag: boolean;
  JournalNameFlag: boolean;
  DateFlag: boolean;

  pageIndex?: number;
  pageSize?: number;
  totalCount?: number;
}

export interface IVoucherListModel {
  VoucherNo?: number;
  CurrencyCode?: string;
  CurrencyId?: number;
  VoucherDate?: any;
  ChequeNo?: string;
  ReferenceNo?: string;
  Description?: string;
  JournalName?: string;
  JournalCode?: number;
  VoucherTypeId?: number;
  OfficeId?: number;
  ProjectId?: number;
  BudgetLineId?: number;
  OfficeName?: string;
}

export interface IVoucherTypeListModel {
  VoucherTypeId: number;
  VoucherTypeName: string;
}

export interface IJournalListModel {
  JournalCode: number;
  JournalName: string;
  JournalType: number;
}

export interface ICurrencyListModel {
  CurrencyId: number;
  CurrencyName: string;
}

export interface IOfficeListModel {
  OfficeId: number;
  OfficeName: string;
}

export interface IProjectListModel {
  ProjectId: number;
  ProjectCode: string;
  ProjectName: string;
  ProjectNameCode: string;
}

export interface IAddVoucherModel {
  VoucherNo?: number;
  CurrencyId: number;
  VoucherDate: any;
  ChequeNo: string;
  Description: string;
  JournalCode: number;
  VoucherTypeId: number;
  OfficeId: number;
  ProjectId?: number;
  BudgetLineId?: number;
  FinancialYearId?: number; // calculate on backend
  TimezoneOffset?: number;
}

export interface IVoucherDetailModel {
  VoucherNo: number;

  ReferenceNo: string;
  Description: string;
  VoucherDate: any;
  CurrencyId: number;
  JournalCode: number;
  ChequeNo: string;

  VoucherTypeId: number;
  OfficeId: number;
  ProjectId?: number;
  BudgetLineId?: number;
  FinancialYearId?: number;
  IsVoucherVerified: boolean;

  JournalName?: string;
  TimezoneOffset?: number;
}

export interface IAccountListModel {
  AccountCode: number;
  AccountName: string;
  ChartOfAccountNewCode: string;
}

export interface IEditTransactionModel {
  TransactionId: number;
  VoucherNo?: number;
  AccountNo: number;
  Description: string;
  ProjectId: number;
  BudgetLineId: number;
  Credit: number;
  Debit: number;
  IsDeleted?: boolean; // using in post method and it will be treated as edit in backend

  // use for handling Add, Edit, Delete on frontend only (don't use to post it)
  BudgetLineList?: IBudgetLineModel[];
  JobName?: string;
  JobId?: number;
  _IsId?: number;
  _IsDeleted?: boolean;
}

export interface AddEditTransactionModel {
  VoucherNo: number;
  VoucherTransactions: IEditTransactionModel[];
}
