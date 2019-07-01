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
    // this.previousUrl = state.url;
    if (
      localStorage.getItem('authenticationtoken') === '' ||
      localStorage.getItem('authenticationtoken') == null
    ) {

      // not logged in so redirect to login page with the return url
      // this.router.navigate(['/login'], {
      //   queryParams: { returnUrl: state.url }
      // });

      this.router.navigate(['../login']);

      return false;
    } else {
      // this.router.navigate(['../login']);
      return true;
    }
  }

  autoLogOut(route) {
    this.previousUrl = route;
    return true;
  }
}
