export interface HiringList {
  HiringRequestId?: string;
  JobCode?: string;
  JobGrade?: string;
  Position?: string;
  TotalVacancies?: string;
  FilledVacancies?: string;
  PayCurrency?: string;
  PayRate?: string;
  Status?: string;
}

export interface IFilterModel {
  FilterValue: string;
  pageIndex?: number;
  pageSize?: number;
  ProjectId?: number;
  TotalCount?: number;
}
