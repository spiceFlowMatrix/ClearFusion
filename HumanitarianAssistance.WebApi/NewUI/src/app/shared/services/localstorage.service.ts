import { IMenuList } from '../dbheader/dbheader.component';

export class LocalStorageService {

//#region
  GetAuthorizedPages(menuList: IMenuList[]): IMenuList[] {
    let isSuperAdmin = false;
    let roles: any;
    const menuListFiltered: IMenuList[] = [];
    const pagesPermissionList: IPagePermissions[] = JSON.parse(localStorage.getItem('RolePermissions'));
    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');

      if (roles != null && roles !== undefined) {
        if (roles.length > 0 && roles != null) {
          for (let i = 0; i < roles.length; i++) {
            if (roles[i].toLowerCase() === 'superadmin') {
              isSuperAdmin = true;
             return menuList;
            }
          }
        }
      }
    }

    if (!isSuperAdmin && pagesPermissionList !== null) {

      menuList.forEach((x: IMenuList) => {
        if (pagesPermissionList.findIndex(p => p.PageId ===  x.PageId) !== -1) {
          menuListFiltered.push(x);
        }
      });

      console.log(menuListFiltered);
    }

    return menuListFiltered;
  }
  //#endregion






  //#region "Check if permission to view/edit page exists for a role"
  displayModulePages(pageId: number): boolean {
    let isSuperAdmin = false;
    let roles: any;
    const permissionList = JSON.parse(localStorage.getItem('RolePermissions'));
    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');

      if (roles != null && roles !== undefined) {
        if (roles.length > 0 && roles != null) {
          for (let i = 0; i < roles.length; i++) {
            if (roles[i].toLowerCase() === 'superadmin') {
              isSuperAdmin = true;
            }
          }
        }
      }
    }

    if (!isSuperAdmin && permissionList !== null) {
      const modulePageExists = permissionList.filter(x => x.PageId === pageId);

      if (modulePageExists != null && modulePageExists.length > 0) {
        return true;
      } else {
        return false;
      }
    }

    return isSuperAdmin;
  }
  //#endregion

  //#region "Check if permission to view module exists for a role"
  displayModule(moduleId: number): boolean {
    let isSuperAdmin = false;
    let roles: any;
    const permissionList = JSON.parse(localStorage.getItem('RolePermissions'));
    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');
      if (roles.length > 0 && roles != null) {
        for (let i = 0; i < roles.length; i++) {
          if (roles[i].toLowerCase() === 'superadmin') {
            isSuperAdmin = true;
          }
        }
      }
    }

    if (!isSuperAdmin && permissionList !== null) {
      const modulePageExists = permissionList.filter(
        x => x.ModuleId === moduleId
      );

      if (modulePageExists != null && modulePageExists.length > 0) {
        return true;
      } else {
        return false;
      }
    }

    return isSuperAdmin;
  }
  //#endregion

  //#region "Check if permission to view child module exists for a role"
  displayChildModule(pages: number[]) {
    let isSuperAdmin = false;
    let roles: any;
    const permissionList = JSON.parse(localStorage.getItem('RolePermissions'));
    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');

      if (roles.length > 0 && roles != null) {
        for (let i = 0; i < roles.length; i++) {
          if (roles[i].toLowerCase() === 'superadmin') {
            isSuperAdmin = true;
          }
        }
      }
    }

    if (!isSuperAdmin && permissionList !== null) {
      const modulePageExists = permissionList.filter(x =>
        pages.includes(x.PageId)
      );

      if (modulePageExists != null && modulePageExists.length > 0) {
        return true;
      } else {
        return false;
      }
    }

    return isSuperAdmin;
  }
  //#endregion

  //#region "IsEditingAllowed"
  public IsEditingAllowed(pageId: number): boolean {
    let isSuperadmin = false;
    let roles: any;

    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');
      if (roles.length > 0 && roles != null) {
        for (let i = 0; i < roles.length; i++) {
          if (roles[i].toLowerCase() === 'superadmin') {
            isSuperadmin = true;
          }
        }
      }
    }

    if (!isSuperadmin) {
      // when role is not superadmin then check for permission

      const permissionList: any[] = JSON.parse(
        localStorage.getItem('RolePermissions')
      );

      const modulePageExists = permissionList.find(x => x.PageId === pageId);

      if (modulePageExists != null) {
        // if user has permission for the page to view/edit
        if (modulePageExists.CanEdit) {
          return true;
        } else {
          return false;
        }
      } else {
        // if user is not having permission then return false
        return false;
      }
    } else {
      // if superadmin then return editing true
      return isSuperadmin;
    }
  }
  //#endregion

  //#region "IsApproveRejectAllowed"
  public IsApproveRejectAllowed(pageId: number): boolean {
    let isSuperadmin = false;
    let roles: any;

    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');

      if (roles.length > 0 && roles != null) {
        for (let i = 0; i < roles.length; i++) {
          if (roles[i].toLowerCase() === 'superadmin') {
            isSuperadmin = true;
          }
        }
      }
    }

    if (!isSuperadmin) {
      // when role is not superadmin then check for permission

      const permissionList: any[] = JSON.parse(
        localStorage.getItem('ApproveRejectRolePermissions')
      );

      const modulePageExists = permissionList.find(x => x.PageId === pageId);

      if (modulePageExists != null) {
        // if user has permission for the page to view/edit
        if (modulePageExists.Approve || modulePageExists.Reject) {
          return true;
        } else {
          return false;
        }
      } else {
        // if user is not having permission then return false
        return false;
      }
    } else {
      // if superadmin then return editing true
      return isSuperadmin;
    }
  }

  public IsAgreeDisagreeAllowed(pageId: number): boolean {

  let isSuperadmin = false;
  let roles: any;

  const userRole = localStorage.getItem('UserRoles');

  if (userRole != null) {
      roles = userRole.split(',');
  }

  if (roles.length > 0 && roles != null) {

      for (let i = 0; i < roles.length; i++) {

          if (roles[i].toLowerCase() === 'superadmin') {

              isSuperadmin = true;
          }
      }
  }

  if (!isSuperadmin) {// when role is not superadmin then check for permission

      const permissionList: any[] = JSON.parse(localStorage.getItem('AgreeDisagreeRolePermissions'));

      const modulePageExists = permissionList.find(x => x.PageId === pageId);

      if (modulePageExists != null) { // if user has permission for the page to view/edit
          if (modulePageExists.Agree || modulePageExists.Disagree) {
              return true;
          } else {
              return false;
          }
      } else {// if user is not having permission then return false
          return false;
      }
  } else {// if superadmin then return editing true
      return isSuperadmin;
  }
}

public IsOrderScheduleAllowed(pageId: number): boolean {

  let isSuperadmin = false;
  let roles: any;

  const userRole = localStorage.getItem('UserRoles');

  if (userRole != null) {
      roles = userRole.split(',');
  }

  if (roles.length > 0 && roles != null) {

      for (let i = 0; i < roles.length; i++) {

          if (roles[i].toLowerCase() === 'superadmin') {

              isSuperadmin = true;
          }
      }
  }

  if (!isSuperadmin) {// when role is not superadmin then check for permission

      const permissionList: any[] = JSON.parse(localStorage.getItem('OrderScheduleRolePermissions'));

      const modulePageExists = permissionList.find(x => x.PageId === pageId);

      if (modulePageExists != null) { // if user has permission for the page to view/edit
          if (modulePageExists.OrderSchedule) {
              return true;
          } else {
              return false;
          }
      } else {// if user is not having permission then return false
          return false;
      }
  } else {// if superadmin then return editing true
      return isSuperadmin;
  }
}

}
export interface IPagePermissions {
  ModuleId: number;
  PageId: number;
  CanView: boolean;
  CanEdit: boolean;
  CanDelete?: boolean;
}
