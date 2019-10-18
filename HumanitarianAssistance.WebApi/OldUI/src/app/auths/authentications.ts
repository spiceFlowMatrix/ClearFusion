import { Injectable } from '@angular/core';
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
  previousUrl: string;
  constructor(private router: Router) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.previousUrl = state.url;
    if (localStorage.getItem('authenticationtoken')) {
      if (state.url === '/login') {
        this.previousUrl = '/dashboard';
      }
      // logged in so return true
      return true;
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate(['../login'], {
      queryParams: { returnUrl: state.url }
    });
    return false;
  }

  // autoLogOut(route) {
  //   this.previousUrl = route;
  //   return true;
  // }
}
