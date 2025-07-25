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
export interface ICandidateFilterModel {
  HiringRequestId?: number;
  FilterValue: string;
  pageIndex?: number;
  pageSize?: number;
  ProjectId?: number;
  TotalCount?: number;
}

export interface IFilterModel {
  FilterValue: string;
  pageIndex?: number;
  pageSize?: number;
  ProjectId?: number;
  TotalCount?: number;
  IsOpenFlagId?: number;
  IsInProgress?: number;
}
export interface HiringRequestDetailList {
  HiringRequestId?: number;
  JobGrade?: string;
  Position?: string;
  TotalVacancies?: string;
  FilledVacancies?: string;
  PayCurrency?: string;
  PayRate?: string;
  Status?: string;
  Office?: string;
  OfficeId?: number;
  JobType?: string;
  JobCategory?: string;
  BudgetName?: string;
  BudgetLineId?: string;
  AnouncingDate?: any;
  ClosingDate?: any;
  ContractType?: string;
  ContractDuration?: number;
  Shift?: string;
  EducationDegree?: string;
  Profession?: string;
  Experience?: string;
  KnowledgeAndSkills?: string;
  HiringRequestStatus?: number;
  SpecificDutiesAndResponsibilities?: string;
  SubmissionGuidelines?: string;
  HiringRequestCode?: string;
  GradeId?: number;
}

export interface OfficeDetailModel {
  ProjectId?: any;
  OfficeId?: number;
  ProfessionId?: number;
}

export interface ICandidateDetailModel {
  CandidateId?: number;
  FirstName?: string;
  LastName?: string;
  Email?: string;
  Password?: string;
  PhoneNumber?: string;
  AccountStatus?: number;
  Gender?: number;
  DateOfBirth?: Date;
  EducationDegree?: number;
  Grade?: number;
  Profession?: number;
  Office?: number;
  Country?: number;
  Province?: number;
  District?: number;
  TotalExperienceInYear?: number;
  RelevantExperienceInYear?: number;
  IrrelevantExperienceInYear?: number;
  Remarks?: string;
  IsCvUpdated?: boolean;
}

export interface IHiringRequestModel {
  HiringRequestId?: number;
  HiringRequestCode?: string;
  ProjectId?: any;
  JobCategory?: number;
  MinEducationLevel?: string;
  TotalVacancy?: number;
  Position?: number;
  Organization?: string;
  Office?: number;
  ContractType?: number;
  ContractDuration?: number;
  Gender?: number;
  Nationality?: number;
  SalaryRange?: string;
  AnouncingDate?: Date;
  ClosingDate?: Date;
  Country?: number;
  ProvinceId?: number;
  JobType?: string;
  JobShift?: number;
  JobStatus?: string;
  Experience?: string;
  Background?: string;
  SpecificDutiesAndResponsibilities?: string;
  KnowledgeAndSkillsRequired?: string;
  SubmissionGuidelines?: string;
}
export interface CompleteHiringRequestModel {
  HiringRequestId: number[];
  ProjectId: number;
}

export interface ICandidateDetailList {
  CandidateId?: number;
  FirstName?: string;
  LastName?: string;
  Gender?: string;
  CvDocument?: string;
  Interview?: string;
  CandidateStatus?: string;
  EmployeeCode?: string;
  EmployeeName?: string;
  firstText?: string;
  subItems?: ISubCandidateList[];
}

export interface IExistingCandidateList {
  EmployeeId?: number;
  EmployeeCode?: string;
  FullName?: string;
  Gender?: string;
  CandidateStatus?: string;
  itemAction?: any[];
}

export interface ISubCandidateList {
  PhoneNumber?: string;
  Email?: string;
  DateOfBirth?: Date;
  EducationDegree?: string;
  Grade?: string;
  Profession?: string;
  Office?: string;
  Country?: string;
  Province?: string;
  District?: string;
  TotalExperienceInYear?: string;
  RelevantExperienceInYear?: string;
  IrrelevantExperienceInYear?: string;
  AccountStatus?: string;
}

export interface TableActionsModel {
  items: {
    button?: { status?: boolean; text?: string };
    delete?: boolean;
    edit?: boolean;
    download?: boolean;
    link?: boolean;
  };
  subitems?: {
    button?: { status?: boolean; text?: string };
    delete?: boolean;
    edit?: boolean;
    download?: boolean;
  };
}
export interface ICandidateDetail {
  FullName?: string;
  DutyStation?: string;
  Gender?: string;
  Qualification?: string;
  DateOfBirth?: Date;
}

export interface IHiringRequestDetailModel {
  OfficeId?: number;
  DesignationId?: number;
  Office?: string;
  Position?: string;
  JobGrade?: string;
  TotalVacancy?: number;
  FilledVacancy?: number;
  PayCurrency?: string;
  PayHourlyRate?: number;
  BudgetLine?: string;
  JobType?: string;
  AnouncingDate?: Date;
  ClosingDate?: Date;
  ContractType?: string;
  ContractDuration?: number;
  JobShift?: string;
  KnowledgeAndSkillsRequired?: string;
  Profession?: string;
  EducationDegree?: string;
  TotalExperienceInYear?: string;
}
export interface ILanguageDetailModel {
  LanguageName?: string;
  LanguageReading?: string;
  LanguageWriting?: string;
  LanguageListining?: string;
  LanguageSpeaking?: string;
}
export interface ITraningDetailModel {
  TraningType?: string;
  TraningName?: string;
  TraningCountryAndCity?: string;
  TraningStartDate?: any;
  TraningEndDate?: any;
}
export interface IInterviewerDetailModel {
  EmployeeId?: number;
  EmployeeCode?: string;
  EmployeeName?: string;
}
export interface InterviewQuestionDetailModel {
  QuestionId?: number;
  Score?: number;
}

export interface InterviewDetailModel {
  CandidateId?: number;
  HiringRequestId?: number;
  InterviewId?: number;
  RatingBasedCriteriaList?: InterviewQuestionDetailModel[];
  TechnicalQuestionList?: InterviewQuestionDetailModel[];
  LanguageList?: ILanguageDetailModel[];
  TraningList?: ITraningDetailModel[];
  InterviewerList?: IInterviewerDetailModel[];
  Description?: string;
  NoticePeriod?: number;
  AvailableDate?: Date;
  WrittenTestMarks?: number;
  CurrentBase?: number;
  CurrentOther?: number;
  ExpectationBase?: number;
  ExpectationOther?: number;
  Status?: number;
  InterviewQuestionOne?: boolean;
  InterviewQuestionTwo?: boolean;
  InterviewQuestionThree?: boolean;
  CurrentTransport?: boolean;
  CurrentMeal?: boolean;
  ExpectationTransport?: boolean;
  ExpectationMeal?: boolean;
  ProfessionalCriteriaMark?: number;
  MarksObtain?: number;
  TotalMarksObtain?: number;
  // Extra fields added for pdf
  CandidateName?: string;
  Qualification?: string;
  Position?: string;
  DutyStation?: string;
  MaritalStatus?: string;
  PassportNumber?: string;
  NameOfInstitute?: string;
  DateOfBirth?: any;
}

export interface ISelectBoxModel {
  Id: number;
  value: string;
}

export interface CvDownloadModel {
  AttachmentName: string;
  AttachmentUrl: string;
  UploadedBy: string;
}

export interface IAnalyticalInfoList {
  Project: string;
  Budgetline: string;
  Account: string;
  Percentage: string;
}
