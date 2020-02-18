export class EmployeeFilterModel {
    EmploymentStatusFilter;
    NameFilter;
    // LastNameFilter;
    GenderFilter;
    EmployeeIdFilter;
    OfficeIds;
    PageSize;
    PageIndex;
}

export interface EmployeeDetailList {
  EmployeeId: number;
  Code: string;
  Name: string;
  FatherName: string;
  EmploymentStatus: string;
  Profession: string;
}

