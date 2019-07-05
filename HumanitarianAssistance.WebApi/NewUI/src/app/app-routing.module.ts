import { LoginComponent } from './login/login.component';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './shared/auth/auth-guard';
import { DbstyleGuideComponent } from './shared/dbstyle-guide/dbstyle-guide.component';


const appRoutes: Routes = [
    // { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
   { path: '', loadChildren: './dashboard/dashboard.module#DashboardModule', canActivate: [AuthGuard] },
    // { path: '', loadChildren: './dashboard/dashboard.module#DashboardModule' },
    { path: 'login', component: LoginComponent },
    { path: 'style-guide', component: DbstyleGuideComponent }
];


@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule]   // important to export
})
export class AppRoutingModule { }
