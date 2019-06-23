import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../../code/code.service';
import { AppHeaderComponent } from '../appHeader.component';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-notification-panel',
  templateUrl: './notification-panel.component.html',
  styleUrls: ['./notification-panel.component.css']
})
export class NotificationPanelComponent implements OnInit, OnChanges {
  //#region "Variable"
  @Input() notificationDataSource: any;

  @Output() notificationIsReadCount: EventEmitter<any> = new EventEmitter<
    any
  >();

  dummy: any[];
  // notificationIsReadCount: any;

  //#endregion

  constructor(
    private codeservice: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private appheader: AppHeaderComponent
  ) {
    // this.getAllNotificationData();
  }

  ngOnInit() {
    this.getAllNotificationList();
  }
  ngOnChanges() {
    this.getAllNotificationData();
  }

  getAllNotificationData() {
    // this.notificationDataSource;
    if (
      this.notificationDataSource != null ||
      this.notificationDataSource !== undefined
    ) {
      this.notificationDataSource.forEach(element => {
        if (
          element.notificationId === 1 ||
          element.notificationId === 2 ||
          element.notificationId === 3
        ) {
          element.NotificationPath = './accounts/vouchers';
        }
        if (
          element.notificationId === 4 ||
          element.notificationId === 5 ||
          element.notificationId === 6
        ) {
          element.NotificationPath = './hr/employees';
        }
      });
    }
    this.dummy = this.notificationDataSource;
    // this.dummy = [{
    //   CreatedDate: "22/02/2018",
    //   UserName: "Alpit G",
    //   UserId: 23,
    //   NotificationId: 34343,
    //   LoggerDetailsId: 2323,
    //   LoggedDetail: "Voucher Edited",
    //   IsRead: true
    // }, {
    //   CreatedDate: "22/02/2018",
    //   UserName: "Sanju R",
    //   UserId: 23,
    //   NotificationId: 34343,
    //   LoggerDetailsId: 2323,
    //   LoggedDetail: "Successfull",
    //   IsRead: false
    // }, {
    //   CreatedDate: "22/02/2018",
    //   UserName: "Shubham K",
    //   UserId: 23,
    //   NotificationId: 34343,
    //   LoggerDetailsId: 2323,
    //   LoggedDetail: "Employee Created",
    //   IsRead: false
    // }, {
    //   CreatedDate: "22/02/2018",
    //   UserName: "Sachin R",
    //   UserId: 23,
    //   NotificationId: 34343,
    //   LoggerDetailsId: 2323,
    //   LoggedDetail: "Successfull",
    //   IsRead: false
    // },
    // ];
  }

  //#region "getAllNotificationList"
  getAllNotificationList() {
    this.codeservice
      .GetAllNotificationDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAllNotifications
      )
      .subscribe(
        data => {
          this.dummy = [];
          if (data.data.LoggerDetailsModelList != null) {
            if (data.data.LoggerDetailsModelList.length > 0) {
              this.dummy = data.data.LoggerDetailsModelList;
              this.dummy.forEach(element => {
                if (
                  element.notificationId === 1 ||
                  element.notificationId === 2 ||
                  element.notificationId === 3
                ) {
                  element.notificationPath = './accounts/vouchers';
                }
                if (
                  element.notificationId === 4 ||
                  element.notificationId === 5 ||
                  element.notificationId === 6
                ) {
                  element.notificationPath = './hr/employees';
                }
                element.createdDate = new Date(
                  new Date(element.createdDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                );
              });
            }
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "setNotificationIsReadFlag"
  setNotificationIsReadFlag(notificationId: any) {
    this.codeservice
      .GetDetailsByIdParameterGeneric(
        this.setting.getBaseUrl() +
          GLOBAL.API_Notification_SetNotificationIsReadFlag,
        'loggerDetailsId',
        notificationId
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.notificationIsReadCount.emit(data.data.notificationIsReadCount);
        }
      });
  }
  //#endregion

  //#region "onNotificationClicked"
  onNotificationClicked(e) {
    this.appheader.notificationPanelVisible = false;
    const indexValue = this.dummy.findIndex(
      x => x.loggerDetailsId === e.loggerDetailsId
    );
    if (indexValue !== -1) {
      if (this.dummy[indexValue].isRead === false) {
        this.dummy[indexValue].isRead = true;
        this.setNotificationIsReadFlag(e.loggerDetailsId);
      }
    }

    // this.toastr.success(JSON.stringify(e));
    // let url = this.setting.getAppUrl() + 'dashboard/hr/employee-appraisal';
    // this.router.navigateByUrl('./hr/employee-appraisal');
    // this.router.navigate(['./hr/employee-appraisal']);
  }
}
