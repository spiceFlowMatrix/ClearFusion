export interface IProjectPeople {
  Id: number;
  ProjectId: number;
  RoleId: number;
  UserId: number;
  DateAdded: any;

  IsDeleted?: boolean;
}

export interface IProjectRoles {
  Id: number;
  Role: string;
}

