export interface IProjectCashFlow {
  ProjectId: number[];
  CurrencyId: number;
  ProjectStartDate: any;
  ProjectEndDate: any;
  DonorID: number;
}
export interface IBudgetCashFlow {
  ProjectId: number;
  BudgetLinStartDate: any;
  BudgetLineEndDate: any;
  BudgetLineId: any[];
  Month: string;
  Debit: number;
}
export interface IMonthList {
  Month: string;
}
export interface IBudgetLineDetailModel {
  BudgetLineId: number;
  BudgetCode: string;
  BudgetName: string;
}
export interface IBudgetLineBreakdownFlowModel {
  ProjectId: number;
  BudgetLineId: number[];
  CurrencyId: number;
  BudgetLineStartDate: any;
  BudgetLineEndDate: any;
}
