export class EmployeeFilterModel {
    EmploymentStatusFilter;
    FirstNameFilter;
    LastNameFilter;
    GenderFilter;
    EmployeeIdFilter;
    OfficeId;
    PageSize;
    PageIndex;
}

export interface EmployeeDetailList {
  EmployeeId: number;
  Code: string;
  FirstName: string;
  LastName: string;
  EmploymentStatus: string;
  Profession: string;
}
