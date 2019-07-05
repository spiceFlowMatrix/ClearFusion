export interface ProjectActivityFilterModel {
  FilterValue: string;
}

export interface ProjectActivityList {
  ActivityId: number;
  ActivityName: string;
  StartDate: any;
  DueDate: any;
  Progress: number;
  Slippage: number;
  Status: number;
}

export interface IDocumentsModel {
  ActivityId: number;
  StatusId: number;
  ActivityDocumentsFilePath: string;
  ActivityDocumentsFileName: string;
  ActtivityDocumentId: number;

  Isdeleted: boolean;
  IsLoading: boolean;
  IsError: boolean;
}

export interface IProjectActivityDocumentModel {
  HeaderText: string;
  // DocumentModel: IDocumentsModel[];
  ProjectPhaseId: number;
  ProjectActivityId: number;
  MonitoringId?: number;
}

export interface IAddProjectActivityDataSources {
  BudgetLineList: any[];
  OfficeList: any[];
  EmployeeList: any[];
  RecurringTypeList: any[];
  ProvinceSelectionList: any[];
  DistrictMultiSelectList: any[];
}

export interface IEmployeeList {
  EmployeeId: number;
  EmployeeName: string;
  EmployeeCode: string;
  CodeEmployeeName: string;
}

// export interface IBudgetLineList {
//   BudgetLineId: number;
//   BudgetLineCode: string;
//   BudgetLineName: string;
//   BudgetLineCodeName: string;
// }

export interface IOfficeList {
  OfficeId: number;
  OfficeCode: string;
  OfficeName: string;
  OfficeCodeName: string;
}

export interface IProjectActivityDetail {
  // Planning
  ActivityId: number;
  ActivityName: string;
  ActivityDescription: string;
  PlannedStartDate: any;
  PlannedEndDate: any;
  BudgetLineId: number;
  EmployeeID: number;
  OfficeId: number;
  StatusId: number;
  Recurring: boolean;
  RecurringCount: number;
  RecurrinTypeId: number;
  ProvinceId?: number[];
  DistrictID?: number[];
  ParentId?: number;

  ActualStartDate: any;
  ActualEndDate: any;

  // Other Properties
  Progress?: number;
  Slippage?: number;

  // // Implementation
  // ImplementationProgress: number;
  // ImplementationStatus: boolean;
  // ImplementationMethod: string;
  // ImplementationChalanges: string;
  // OvercomingChallanges: string;
  // ExtensionStartDate: any;
  // ExtensionEndDate: any;

  // // Monitoring
  // MonitoringProgress: number;
  // MonitoringStatus: boolean;
  // MonitoringScore: number;
  // MonitoringFrequency: number;
  // VerificationSource: string;
  // Strengths: string;
  // Weeknesses: string;
  // MonitoringChallenges: string;
  // Recommendation: string;
  // Comments: string;

  IsLoading?: boolean;
  IsError?: boolean;
}

export interface IPlanningActivityDetail {
  // Planning
  ActivityId: number;
  ActivityName: string;
  ActivityDescription: string;
  PlannedStartDate: any;
  PlannedEndDate: any;
  BudgetLineId: number;
  EmployeeID: number;
  OfficeId: number;
  StatusId: number;
  Recurring: boolean;
  RecurringCount: number;
  RecurrinTypeId: number;
  ProvinceId?: number[];
  DistrictID?: number[];
}

export interface IBudgetLine {
  BudgetCode: string;
  BudgetLineId: number;
  BudgetName: string;
  CurrencyId: number;
  CurrencyName: string;
  InitialBudget: number;
  ProjectId: number;
  ProjectJobCode: string;
  ProjectJobId: number;
  ProjectJobName: string;
}

export interface IProjectSummaryModel {
  ProjectDuration: number;
  ActivityOnSchedule: number;
  LateStart: number;
  LateEnd: number;
  Progress: number;
  Slippage: number;
}

export interface IProjectAdvanceFilterModel {
  ProjectId: number;
  PlannedStartDate: any;
  PlannedEndDate: any;
  ActualStartDate: any;
  ActualEndDate: any;
  BudgetLineId: number[];
  AssigneeId: number[];

  // status
  Planning: boolean;
  Implementation: boolean;
  Completed: boolean;

  // range
  ProgressRange: number[];
  // SleepageRange: any;
  // DurationRange: any;
  SleepageMin: number;
  SleepageMax: number;

  DurationMin: number;
  DurationMax: number;

  LateStart: boolean;
  LateEnd: boolean;
  OnSchedule: boolean;
}

export interface IAddProjectSubActivityDataSources {
  BudgetLineId: number;
  ActivityId: number;
  EmployeeList: any[];
}

export interface IAddProjectSubActivityModel {
  ActivityDescription: string;
  PlannedStartDate: any;
  PlannedEndDate: any;
  EmployeeID: number;
  Target: number;
  BudgetLineId?: number;
  ParentId?: number;
  SubActivityTitle: string;
}
export interface IEditProjectSubActivityModel {
  ActivityId?:number;
  ActivityDescription: string;
  PlannedStartDate: any;
  PlannedEndDate: any;
  EmployeeID: number;
  Target: number;
  BudgetLineId?: number;
  ParentId?: number;
  Achieved?: number;
  ChallengesAndSolutions?:string;
  IsComplete?:boolean;
  ActualStartDate?:any;
  ActualEndDate?:any;
  SubActivityTitle: string;
}

export interface IProjectSubActivityListingModel {
  ActivityId?:number;
  ActivityDescription: string;
  PlannedStartDate: any;
  PlannedEndDate: any;
  EmployeeID: number;
  Target: number;
  BudgetLineId?: number;
  ParentId?: number;
  Achieved?: number;
  ChallengesAndSolutions?:string;
  IsCompleted?:boolean;
  ActualStartDate?:any;
  ActualEndDate?:any;
  SubActivityTitle: string;
}
export interface IAddExtensionDataSources {
  ActivityId: number;
}

export interface IActivityExtensionMode {
  ExtensionId: number;
  ActivityId: number;
  StartDate: any;
  EndDate: any;
  Description: string;
}

export interface IProjectPermissionMode {
  Id: number;
  ProjectId: number;
  RoleId: number;
  UserId: number;
  DateAdded: any;
}

