import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Injectable()
export class RoleGuardService implements CanActivate {
  constructor(public router: Router, private toastr: ToastrService) {}
  canActivate(route: ActivatedRouteSnapshot): boolean {
    let roles: any;
    const permissionList = localStorage.getItem('RolePermissions');
    const userRole = localStorage.getItem('UserRoles');

    if (userRole != null) {
      roles = userRole.split(',');
    }

    if (permissionList  != null) {
      const permissions = JSON.parse(permissionList);

      if (route.data != null) {

        let pageExists = permissions.find(x => x.PageId === route.data.page);

        if (pageExists !== undefined) {
          return true;
        } else if (route.data.isModule) {
          return true;
        } else if (route.data.module !== 0) {

          pageExists = permissions.find(x => x.ModuleId === route.data.module && x.PageId === route.data.page);

          if (pageExists !== undefined) {
            return true;
          } else {
            if (roles.length > 0 && roles != null) {
              for (let i = 0; i < roles.length; i++) {
                if (roles[i].toLowerCase() === 'superadmin') {
                  return true;
                }
              }
            }
            this.toastr.error('Not Authorised');
             this.router.navigateByUrl('/');
             return false;
          }
        }
      }
    }

    // let expectedRoleArray = route.data;
    //  expectedRoleArray = expectedRoleArray.expectedRole;

    // const rolePermissions = JSON.parse(localStorage.getItem('UserRolePermission'));

    // // // decode the token to get its payload
    // // const tokenPayload = decode(token);

    //  let  expectedRole = '';

    // for(let i=0; i<expectedRoleArray.length; i++){
    //   if(expectedRoleArray[i]==rolePermissions[0].RoleName){
    //     console.log("Roles Matched");
    //     expectedRole = rolePermissions[0].RoleName;
    //   }
    // }

    // if (rolePermissions[0].RoleName == expectedRole) {
    //   console.log("User permitted to access the route");
    //   return true;
    // }
     return false;

    // this.router.navigate(['login']);
  }
}
