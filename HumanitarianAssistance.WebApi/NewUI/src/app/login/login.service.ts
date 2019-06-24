import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private route: Router
  ) { }

  //#region "Logout"
    logout () {
      localStorage.clear();
      this.route.navigate(['/login']);
    }
  //#endregion

}
