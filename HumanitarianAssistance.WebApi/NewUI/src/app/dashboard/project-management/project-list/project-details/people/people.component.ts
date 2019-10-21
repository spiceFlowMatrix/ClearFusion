import { Component, OnInit, HostListener } from '@angular/core';
import { ProjectListService } from '../../service/project-list.service';
import { IUserListModel } from '../models/project-details.model';
import { SignalRService } from 'src/app/shared/services/signal-r.service';
import { NotifySignalRService } from 'src/app/shared/services/notify-signalr.service';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.scss']
})
export class PeopleComponent implements OnInit {
  userList: IUserListModel[] = [];

  opportunityRoleList: any[] = [];
  logisticsRoleList: any[] = [];
  activitiesRoleList: any[] = [];
  hiringRoleList: any[] = [];

  // screen scroll
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;

  constructor(
    private projectListService: ProjectListService,
    private signalRService: NotifySignalRService) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.getAllUserList();

    this.opportunityRoleList = this.projectListService.GetOpportunityControlRole();
    this.logisticsRoleList = this.projectListService.GetLogisticsControlRole();
    this.activitiesRoleList = this.projectListService.GetActivitiesControlRole();
    this.hiringRoleList = this.projectListService.GetHiringControlRole();
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "getAllUserList"
  getAllUserList() {
    this.projectListService.GetAllUserList().subscribe(response => {
    this.userList = [];

      if (response.data != null) {
        if (response.data.length > 0) {
          response.data.forEach((element: any) => {
            this.userList.push({
              UserId: element.UserID,
              Username: element.FirstName + ' ' + element.LastName
            });
          });
        }
      }
    });
  }
  //#endregion

  //#region "invokePeoplePermissionChange"
  invokePeoplePermissionChange() {
    this.signalRService.activityPermissionChangedInvoke('ActivityPermissionChanged');

  }
  //#endregion


}
