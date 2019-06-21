export interface DonorDetailModel {
  DonorId?: 0;
  Name?: string;
  ContactPerson?: string;
  ContactDesignation?: string;
  ContactPersonEmail?: string;
  ContactPersonCell?: string;
  Count?: number;
}

export interface DonorFilterModel {
  FilterValue?: string;
  DonorNameFlag?: boolean;
  DateFlag?: boolean;
  pageIndex?: number;
  pageSize?: number;
  totalCount?: number;
}
