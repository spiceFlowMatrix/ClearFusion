export interface ICurrencyModel {
  CurrencyId: number;
  CurrencyName: string;
  CurrencyCode: string;
}

export interface IFilterOption {
  FilterId: number;
  FilterName: string;
}

export interface IProposalReportFilter {
  ProjectName: string;
  DueDate: any;
  DueDateFilterOption: number;
  StartDate: any;
  StartDateFilterOption: number;
  CurrencyId: number;
  Amount: number;
  AmountFilterOption: number;
  IsCompleted: boolean;
  IsLate: boolean;
  totalCount?: number;
  pageSize?: number;
  pageIndex?: number;
}

export interface IProposalReport {
  ProjectCode: string;
  ProjectsName: string;
  ProjectCurrencyId: number;
  ProjectStartDate: any;
  Progress: number;
  TooltipText: string;
  ProjectEndDate: any;
  BudgetEstimation: number;
  ColorCode: number;
  ReviewCompletionDate?: any;
  DueDays?: number;
}

export interface IProjectReportFilterModel {
  // FilterValue?: string;
  // ProjectNameFlag?: boolean;
  // DateFlag?: boolean;
  // ProjectIdFlag?: boolean;
  // ProjectCodeFlag?: boolean;
  // DescriptionFlag?: boolean;

  pageIndex?: number;
  pageSize?: number;
  totalCount?: number;
}

export interface IAmountSummary {
  CurrencyId: Number;
  ProposalAmount: Number;
}
