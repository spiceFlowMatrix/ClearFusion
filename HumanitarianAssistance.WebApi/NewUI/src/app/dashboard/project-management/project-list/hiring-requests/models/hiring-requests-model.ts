import { ICurrencyList } from "src/app/dashboard/accounting/gain-loss-report/gain-loss-report.model";
import { INTERNAL_BROWSER_PLATFORM_PROVIDERS } from "@angular/platform-browser/src/browser";

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
  Description: string;
  ProfessionId: number;
  Position: string;
  TotalVacancies: number;
  FilledVacancies: number;
  BasicPay: number;
  BudgetLineId: number;
  OfficeId: number;
  GradeId: number;
  EmployeeID: number;
  ProjectId: number;
  IsCompleted: boolean;
  CurrencyId: number;
  RequestedBy?: string;
}
export interface ProjectHiringRequestFilterModel {
  FilterValue: string;
  pageIndex?: number;
  pageSize?: number;
  totalCount?: number;

  HiringRequestId?: number;
  HiringRequestCode: string;
  Description: string;
  ProfessionId: string;
  Position: string;
  TotalVacancies: number;
  FilledVacancies: number;
  BasicPay: number;
  BudgetLineId: number;
  OfficeId: number;
  GradeId: number;
  EmployeeID: number;
  ProjectId: number;
  IsCompleted: boolean;
  CurrencyId: number;
  BudgetName: string;
  CurrencyName: string;
  EmployeeName: string;
  GradeName: string;
  RequestedBy: string;
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
}
export interface ISelectedCandidateModel {
  BudgetLineId?: number;
  EmployeeId: number;
  ProjectId: number;
  HiringRequestId: number;
  IsSelected: boolean;
}
export interface IAttendaneGroupModel{
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
export interface CandidateDetailModel{
  EmployeeContractTypeId: number;
  AttendanceGroupId: number;
  ProjectId?: number;
  EmployeeId: number;
  HiredOn: Date;
  OfficeId: number;
  EmployeeTypeId: number;
}
