export interface IHistoricalLogDetails {
  EmployeeID?: number;
  HistoryId?: number;
  HistoryDate?: any;
  Description?: string;
}

export interface IEducationDetails {
  EmployeeEducationsId?: number;
  EducationFrom?: any;
  EducationTo?: any;
  FieldOfStudy?: string;
  Institute?: string;
  Degree?: string;
}

export interface IHistoryOutsideCountryDetails {
  EmployeeHistoryOutsideCountryId?: number;
  EmploymentFrom?: any;
  EmploymentTo?: any;
  Organization?: string;
  MonthlySalary?: number;
  ReasonForLeaving?: string;
  Position?: string;
}

export interface IHistoryOutsideOrganizationDetails {
  EmployeeHistoryOutsideOrganizationId?: number;
  EmploymentFrom?: any;
  EmploymentTo?: any;
  Organization?: string;
  MonthlySalary?: number;
  ReasonForLeaving?: string;
  Position?: string;
}

export interface IEmployeeCloseRelativeDetails {
  EmployeeRelativeInfoId?: number;
  Name: string;
  Relationship: string;
  Position: string;
  Organization: string;
  PhoneNo: number;
  Email: string;
}
export interface IEmployeeThreeReferenceDetails {
  EmployeeInfoReferencesId?: number;
  Name: string;
  Relationship: string;
  Position: string;
  Organization: string;
  PhoneNo: number;
  Email: string;
}
export interface IEmployeeOtherSkillDetails {
  EmployeeOtherSkillsId: number;
  AbilityLevel: string;
  Experience: string;
  Remarks: string;
  TypeOfSkill: string;
}

export interface IEmployeeSalaryBudgetDetails {
  EmployeeSalaryBudgetId: number;
  BudgetDisbursed: any;
  CurrencyId?: any;
  SalaryBudget: any;
  Year: string;
  CurrencyName?: string;
}
export interface IEmployeeLanguageDetails {
  SpeakLanguageId: number;
  Language: any;
  Writing: any;
  Speaking: any;
  Reading: any;
  Listening: any;
}
