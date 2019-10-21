export interface PolicyModel {
  PolicyId?: number;
  PolicyName?: string;
  PolicyCode?: string;
  LanguageId?: number;
  LanguageName?: string;
  ProducerId?: number;
  ProducerName?: string;
  MediumId?: number;
  MediumName?: string;
  MediaCategoryId?: number;
  MediaCategoryName?: string;
  CreatedById?: string;
  CreatedBy?: string;
  CreatedDate?: any;
  IsDeleted?: boolean;
  ModifiedBy?: string;
  ModifiedById?: string;
  ModifiedDate?: any;
  Description?: string;
}



export interface FilterPolicyModel {
  Value?: string;
  Medium?: boolean;
  PolicyId?: boolean;
  PolicyName?: boolean;
}

export interface PolicyPaginationModel {
  pageIndex?: number;
  pageSize?: number;
}

export interface StartEndTimeModel {
  startTime?: any;
  endTime?: any;
}

export interface PolicyTimeModel {
  PolicyId?: number;
  RepeatDays?: any;
  StartTime?: string;
  EndTime?: string;
  Id?: number;
}

export interface PolicyDayModel {
  PolicyId?: number;
  Id?: number;
  Monday?: boolean;
  Tuesday?: boolean;
  Wednesday?: boolean;
  Thursday?: boolean;
  Friday?: boolean;
  Saturday?: boolean;
  Sunday?: boolean;
}

export interface PolicyOrderScheduleModel {
  PolicyId?: number;
  Id?: number;
  startDate?: Date;
  endDate?: Date;
}
