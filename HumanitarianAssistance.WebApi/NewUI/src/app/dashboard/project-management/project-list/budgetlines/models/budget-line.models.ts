export interface IBudgetLineModel {
  BudgetLineId: number;
  BudgetCode: string;
  BudgetName: string;
  ProjectJobId: number;
  InitialBudget: number;
  ProjectId: number;
  CurrencyId: number;
  ProjectJobCode: string;
  ProjectJobName: string;
  CurrencyName: string;
  CreatedDate: any;
  DebitPercentage: number;
  Expenditure?: number;
}


export interface ICurrencyListModel {
  CurrencyId: number;
  CurrencyName: string;
  CurrencyCode: string;

}
export interface IProjectJobModel {
  ProjectJobId: number;
  ProjectJobCode: string;
  ProjectJobName: string;
}

export interface IBudgetListFilterModel {
  FilterValue: string;

  BudgetLineIdFlag: boolean;
  BudgetCodeFlag: boolean;
  BudgetNameFlag: boolean;
  ProjectJobIdFlag: boolean;
  InitialBudgetFlag: boolean;
  DateFlag: boolean;

  pageIndex?: number;
  pageSize?: number;
  totalCount?: number;
  ProjectId?: number;
}


export interface ITransactionDetailModel {
  UserName: string;
  Credit: number;
  Debit: number;
  CurrencyName: string;
  CurrencyId: number;
  TransactionDate: any;
  DebitPercentage: number;
}

export interface ITransactionModel {
  UserName: string;
  CurrencyId: number;
  BudgetLineId: number;
}
