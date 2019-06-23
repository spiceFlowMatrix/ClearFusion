export interface IProjectFilterModel {
  FilterValue?: string;
  ProjectNameFlag?: boolean;
  DateFlag?: boolean;
  ProjectIdFlag?: boolean;
  ProjectCodeFlag?: boolean;
  DescriptionFlag?: boolean;

  pageIndex?: number;
  pageSize?: number;
  totalCount?: number;
}
