export interface IProjectJobModel {
  ProjectJobId: number;
  ProjectJobCode: string;
  ProjectJobName: string;
  ProjectId: number;
  CanDelete?: boolean;
}

export interface ProjectJobsFilterModel {
  FilterValue?: string;
  ProjectJobNameFlag?: boolean;
  DateFlag?: boolean;
  PageIndex?: number;
  PageSize?: number;
  TotalCount?: number;
  ProjectId?: number;
}
