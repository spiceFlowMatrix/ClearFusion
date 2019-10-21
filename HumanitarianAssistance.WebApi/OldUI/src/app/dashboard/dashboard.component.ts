import { Component } from '@angular/core';
import { CommonService } from '../service/common.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  title = 'app';
  // commonService = new CommonService();
  public toggleSide = false;
  LoaderFlag = false;

  constructor(private commonservice: CommonService) {
    this.commonservice.getLoader().subscribe(data => {
      this.LoaderFlag = data;
    });
  }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnInit() {
    // var token = localStorage.getItem('authenticationtoken');
    // if (token && token != "") {
    //   this.commonService.menuVisibility = true;
    // //  this.commonService.applyRoleOnMenu();
    //  this.roleService.addRoles(
    //    {
    //      'SUPERADMIN':['canRead','canAdd']
    //    });
    //  }
  }
  // recheckToken(){
  //  var token = localStorage.getItem('authenticationtoken');
  //   if (token && token != "") {
  //     this.commonService.menuVisibility = true;
  //    // this.commonService.applyRoleOnMenu();
  //   } else {
  //     this.commonService.menuVisibility = false;
  //   }
  // }
}
