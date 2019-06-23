import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user.component';
import { NgModule } from '@angular/core';
import { RoleGuardService as RoleGuard } from '../../service/role-guard.service';
import {
  applicationPages,
  applicationModule
} from '../../shared/application-pages-enum';

const userModule: any = {
  ModuleId: applicationModule.Users
};

const User: any = {
  UserPageId: applicationPages.Users
};

const Admin_Router: Routes = [
  {
    path: '',
    component: UserComponent,
    children: [
      {
        path: 'users',
        component: UserComponent,
        canActivate: [RoleGuard],
        data: {
          module: userModule.ModuleId,
          page: User.UserPageId
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(Admin_Router)],
  exports: [RouterModule]
})
export class UserRoutingModule {}
