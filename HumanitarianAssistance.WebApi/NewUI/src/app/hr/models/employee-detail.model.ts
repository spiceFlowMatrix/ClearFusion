export interface IEmployeeDetail {
  Name: string;
  FatherName: string;
  Email: string;
  Phone: string;
  Sex: string;
  DateOfBirth: string;
  Country: string;
  State: string;
  Profession: string;
  Qualification: string;
  ExperienceYear: string;
  ExperienceMonth: string;
  PreviousWork: string;
  CurrentAddress: string;
  PermanentAddress: string;
  EmployementStatus: string;
  EmploymentStatusId: number;
  DutyStation: string;
  HiredOn: string;
  AttendanceGroup: string;
  JobDescription: string;
  Resigned: string;
  ResignedOn: string;
  ResignedReason: string;
  Terminated: string;
  TerminatedOn: string;
  TerminationReason: string;
  OfficeId?: number;
  IsResigned: boolean;
  ResignationStatus: number;
  Tenure: string;
}
export interface IEmployeeDetailModel {
  EmployeeID: number;
  FirstName: string;
  LastName: string;
  EmployeeCode: string;
  Designation: number;
  ContractStartDate: Date;
  ContractEndDate: Date;
  DurationOfContract: number;
  Grade: number;
  DutyStation: number;
  Country: number;
  Province: number;
  Project: number;
  Job: string;
  BudgetLine: number;
  WorkTime: string;
  WorkDay: number;
}

export interface IEmployeeAllDetails {
  EmployeeBasicDetail: IEmployeeBasicDetail;
  EmployeeProfessionalDetails: IEmployeeProfessionalDetails;
  EmployeePensionDetail: IEmployeePensionDetails;
}

export interface IEmployeeBasicDetail {
  EmployeeId?: number;
  FullName?: string;
  FatherName?: string;
  Email?: string;
  PhoneNo?: string;
  Password?: string;
  Gender?: number;
  DateOfBirth?: Date;
  MaritalStatus?: number;
  Country?: number;
  Province?: number;
  District?: number;
  BirthPlace?: number;
  TinNumber?: string;
  PassportNumber?: string;
  University?: string;
  Profession?: number;
  Qualification?: number;
  ExperienceYear?: number;
  ExperienceMonth?: number;
  IssuePlace?: string;
  ReferBy?: string;
  PreviousWork?: string;
  CurrentAddress?: string;
  PermanentAddress?: string;
}
export interface IEmployeeProfessionalDetails {
  EmployeeType?: number;
  JobGrade?: number;
  Office?: number;
  Department?: number;
  Designation?: number;
  EmployeeCotractType?: number;
  HiredOn?: Date;
  AttendanceGroup?: number;
  DutyStation?: number;
  TrainingAndBenefits?: string;
  JobDescription?: string;
}

export interface IEmployeePensionDetails {
  PensionDate?: Date;
  PensionList?: IEmployeePensionList[];
}

export interface IEmployeePensionList {
  PensionId?: number;
  Currency: number;
  Amount: number;
}

export interface IEmployeePensionListModel {
  Id?: number;
  PensionId?: number;
  Currency: number;
  Amount: number;
}
