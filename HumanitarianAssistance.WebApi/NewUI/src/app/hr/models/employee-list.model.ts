export class EmployeeFilterModel {
    EmploymentStatusFilter;
    NameFilter;
    // LastNameFilter;
    GenderFilter;
    EmployeeIdFilter;
    OfficeIds;
    ProjectFilter?;
    PageSize;
    PageIndex;
}

export interface EmployeeDetailList {
  EmployeeId: number;
  Code: string;
  Name: string;
  FatherName: string;
  EmploymentStatus: string;
  // Profession: string;
  Sex: string;
  Designation: string;
  CreatedDate: string;
  HiredDate: string;
}

