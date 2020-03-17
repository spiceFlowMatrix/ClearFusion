import { Component, OnInit } from '@angular/core';
import { Auth0Service } from '../auth0/auth0.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  showFiller = false;
  profile: any;
  constructor(private auth: Auth0Service) { }

  ngOnInit() {
    if (this.auth.userProfile) {
       this.profile = this.auth.userProfile;
      console.log(this.auth.userProfile);
    } else {
      this.auth.getProfile((err, profile) => {
         this.profile = profile;
        console.log(this.profile);
      });
    }
  }


}
