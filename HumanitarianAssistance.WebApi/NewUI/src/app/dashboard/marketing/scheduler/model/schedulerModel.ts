export interface ScheduleDetailsModel {
  PolicyScheduleId?: number;
  PolicyId?: number;
  JobId?: number;
  ProjectId?: number;
  ScheduleType?: string;
  Title?: string;
  RepeatDays?: any[];
  Description?: string;
  Frequency?: number;
  ByMonth?: number;
  ByWeek?: number;
  ByDay?: number;
  StartDate?: Date;
  StartTime?: any;
  EndTime?: Date;
  EndDate?: any;
  isActive?: boolean;
  isDeleted?: boolean;
  ScheduleCode?: string;
  Repeat?: number;
}

export interface ScheduleTimeModel {
Id?: number;
PolicyId?: number;
StartTime?: string;
EndTime?: string;
Name?: string;
}

export interface ScheduleDetails {
Id?: number;
PolicyId?: number;
JobId?: number;
ProjectId?: number;
Name?: string;
PolicyOrderId?: number;
PolicyTimeId?: number;
PolicyDayId?: number;
ScheduleId?: number;
StartDate?: any;
StartTime?: any;
EndTime?: any;
EndDate?: any;
Sunday?: boolean;
Monday?: boolean;
Tuesday?: boolean;
Wednesday?: boolean;
Thursday?: boolean;
Friday?: boolean;
Saturday?: boolean;
}

export interface SchedulerModel {
  ScheduleId?: number;
  ScheduleType?: string;
  ScheduleCode?: string;
  ScheduleName?: string;
  PolicyId?: number;
  ProjectId?: number;
  JobId?: number;
  MediumId?: number;
  ChannelId?: number;
  Description?: string;
  StartDate?: any;
  EndDate?: any;
  StartTime?: any;
  EndTime?: any;
  RepeatDays?: any[];
  }

export interface FetchScheduleDetailsModel {
  OrderScheduleId?: number;
  TimeScheduleId?: number;
}

export interface filterSchedulerModel {
  MediumId?: number;
  ChannelId?: number;
  StartDate?: Date;
}

export interface playoutMinutesModel {
  ScheduleId?: number;
  TotalMinutes?: number;
  DroppedMinutes?: number;
  allowedMinutes?: any;
  startTime?: any;
  endTime?: any;
}
export interface FormDateModule{
  startDate: Date;
}
