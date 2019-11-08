export interface HiringList {
  HiringRequestId?: number;
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
export interface HiringRequestDetailList {
  HiringRequestId?: string;
  Description?: string;
  JobCode?: string;
  JobGrade?: string;
  Position?: string;
  TotalVacancies?: string;
  FilledVacancies?: string;
  PayCurrency?: string;
  PayRate?: string;
  Status?: string;
  Office?: string;
}

export interface OfficeDetailModel {
  ProjectId?: any;
  OfficeId?: number;
  ProfessionId?: number;
}

export interface ICandidateDetailModel {
FirstName?: string;
LastName?: string;
Email?: string;
PhoneNumber?: string;
AccountStatus?: number;
Gender?: number;
DateOfBirth?: Date;
Grade?: number;
Profession?: number;
Office?: number;
Country?: number;
Province?: number;
District?: number;
ExperienceInMonth?: number;
ExperienceInYear?: number;
}

export interface IHiringRequestModel {
  HiringRequestId?: number;
  ProjectId?: any;
  JobCategory?: number;
  MinEducationLevel?: string;
  TotalVacancy?: number;
  Position?: number;
  Organization?: string;
  Office?: number;
  ContractType?: string;
  ContractDuration?: number;
  Gender?: number;
  Nationality?: number;
  SalaryRange?: string;
  AnouncingDate?: Date;
  ClosingDate?: Date;
  Country?: number;
  Province?: number;
  JobType?: string;
  JobShift?: number;
  JobStatus?: string;
  Experience?: string;
  Background?: string;
  SpecificDutiesAndResponsibilities?: string;
  KnowledgeAndSkillsRequired?: string;
  SubmissionGuidelines?: string;
}
