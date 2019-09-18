import { ICurrencyList } from "src/app/dashboard/accounting/gain-loss-report/gain-loss-report.model";

export interface IHiringReuestDataSource {
  EmployeeList: IEmployeeListModel[];
  BudgetLineList: IBudgetLineModel[];
  CurrencyList: ICurrencyList[];
  OfficeList: IOfficeListModel[];
  JobGradeList: IJobGradeModel[];
  ProjectId: number;
  HiringRequestDetail: any;
  HiringRequestId: number;
  ProfessionList: IProfessionList[];
  officeSelectionFlag: boolean;
  gender: any[];
  workingShift: any[];
}
export interface ICurrencyListModel {
  CurrencyId: number;
  CurrencyName: string;
  CurrencyCode: string;
}
export interface IBudgetLineModel {
  BudgetCode: string;
  BudgetLineId: number;
  BudgetName: string;
}
export interface IOfficeListModel {
  OfficeId: number;
  OfficeCode: string;
  OfficeName: string;
  OfficeCodeName: string;
}
export interface IJobGradeModel {
  GradeId?: number;
  GradeName: string;
}
export interface IHiringRequestDetailModel {
  HiringRequestId?: number;
  HiringRequestCode: string;
  RequestedBy?: string;
  BasicPay?: number;
  BudgetLineId?: number;
  CurrencyId?: number;
  Description?: string;
  EmployeeID?: number;
  FilledVacancies?: number;
  GradeId?: number;
  OfficeId?: number;
  Position?: string;
  ProfessionId?: number;
  ProjectId?: number;
  TotalVacancies?: number;
  AnouncingDate?: Date;
  JobType?: string;
  Background?: string;
  JobStatus?: string;
  KnowladgeAndSkillRequired?: string;
  SalaryRange?: string;
  Shift?: number;
  ProviceId?: number;
  SpecificDutiesAndResponsblities?: string;
  SubmissionGuidlines?: string;
  ClosingDate?: Date;
  ContractDuration?: number;
  ContractType?: string;
  CountryId?: number;
  GenderId?: number;
  MinimumEducationLevel?: string;
  Experience?: string;
  Organization?: string;
  IsCompleted: boolean;
}

export interface IHiringRequestModel {
  Description: string;
  Position: string;
  Profession: string;
  BudgetLine: string;
  TotalVacancies: string;
  Office: string;
  FilledVacancies: string;
  BasicPay: string;
  jobGrade: string;
}

export interface ProjectHiringRequestFilterModel {
  FilterValue: string;
  pageIndex?: number;
  pageSize?: number;
  totalCount?: number;

  HiringRequestId?: number;
  HiringRequestCode: string;
  RequestedBy?: string;
  BasicPay?: number;
  BudgetLineId?: number;
  CurrencyId?: number;
  Description?: string;
  EmployeeID?: number;
  FilledVacancies?: number;
  GradeId?: number;
  OfficeId?: number;
  Position?: string;
  ProfessionId?: number;
  ProjectId?: number;
  TotalVacancies?: number;
  AnouncingDate?: Date;
  JobType?: string;
  Background?: string;
  JobStatus?: string;
  KnowladgeAndSkillRequired?: string;
  SalaryRange?: string;
  Shift?: number;
  ProviceId?: number;
  SpecificDutiesAndResponsblities?: string;
  SubmissionGuidlines?: string;
  ClosingDate?: Date;
  ContractDuration?: number;
  ContractType?: string;
  CountryId?: number;
  GenderId?: number;
  MinimumEducationLevel?: string;
  Experience?: string;
  Organization?: string;
  IsCompleted: boolean;
}
export interface IEmployeeListModel {
  EmployeeId?: number;
  EmployeeName: string;
}
export interface IHiringReuestCandidateModel {
  CandidateId?: number;
  HiringRequestId?: number;
  EmployeeID?: number;
  IsShortListed?: boolean;
  ProjectId?: number;
}
export interface IReuestedCandidateDetailModel {
  CandidateId?: number;
  EmployeeID?: number;
  EmployeeCode?: number;
  EmployeeName?: string;
  GradeId?: number;
  EmployeeTypeId?: number;
  EmployeeTypeName?: string;
  HiringRequestId?: number;
  Gender?: string;
  IsInterViewed?: boolean;
  IsShortListed?: boolean;
  IsSelected?: boolean;
  IsSelectedFlag?: boolean;
}
export interface IProfessionList {
  ProfessionId?: number;
  ProfessionName?: string;
}
export interface IitervireCandidateModel {
  InterviewDetailsId?: number;
  EmployeeID: number;
  JobId?: number;
  Status?: string;
  JobDescription: string;
  OfficeId?: number;
}
export interface ISelectedCandidateModel {
  BudgetLineId?: number;
  EmployeeId: number;
  ProjectId: number;
  HiringRequestId: number;
  IsSelected: boolean;
}
export interface IAttendaneGroupModel {
  Description: string;
  Id: number;
  Name: number;
}
export interface IcandidateDetailDataSource {
  AttendanceGroupList: IAttendaneGroupModel[];
  HiringRequestDetail: IHiringRequestDetailModel;
  EmployeeId: number;
  ProjectId: number;
  EmployeeContractist: IEmployeeContractList[];
}
export interface IEmployeeContractList {
  EmployeeContractTypeId: number;
  EmployeeContractTypeName: string;
}
export interface CandidateDetailModel {
  EmployeeContractTypeId: number;
  AttendanceGroupId: number;
  ProjectId?: number;
  EmployeeId: number;
  HiredOn: Date;
  OfficeId: number;
  EmployeeTypeId: number;
}
export interface WorkingShift {
  Id: Number;
  value: string;
}
export interface Gender {
  Id: Number;
  value: string;
}
