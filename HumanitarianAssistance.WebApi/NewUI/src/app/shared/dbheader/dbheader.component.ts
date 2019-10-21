import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { GlobalSharedService } from '../services/global-shared.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
@Component({
  selector: 'app-dbheader',
  templateUrl: './dbheader.component.html',
  styleUrls: ['./dbheader.component.scss']
})
export class DbheaderComponent implements OnInit, OnDestroy {
  //#region "variables"
  public menuList: IMenuList[] = [];
  public headerName = ' ';
  currentRoute = '';

  private trainingSubscription: Subscription;
  private routerEventSubscription: Subscription;

  //#endregion

  constructor(
    private router: Router,
    private globalSharedService: GlobalSharedService
  ) {

    // Get Menu Header
    this.globalSharedService.getMenuHeaderName().subscribe((res: string) => {
      this.headerName = res;
    });

    // Get Menu List
    this.globalSharedService.getMenuList().subscribe((res: any) => {
      this.menuList = res;
    });
  }

  ngOnInit() {
    // First time set
    this.currentRoute = this.router.url;

    this.routerEventSubscription = this.router.events.subscribe(r => {
      if (r instanceof NavigationEnd) {
        this.currentRoute = r.url;
      }
    });
  }


  //#region "onTrainingClick"
  onTrainingClick() {
    this.trainingSubscription = this.globalSharedService
      .GetTrainingLink()
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data != null) {
            window.open(response.data, '_blank');
          } else if (response.statusCode === 200) {
            console.log(response.message);
          }
        },
        (error: any) => {}
      );
  }
  //#endregion

  //#region "ngOnDestroy"
  ngOnDestroy() {
    if (this.trainingSubscription && !this.trainingSubscription.closed) {
      this.trainingSubscription.unsubscribe();
    }
    if (this.routerEventSubscription && !this.routerEventSubscription.closed) {
      this.routerEventSubscription.unsubscribe();
    }
  }
  //#endregion
}

export interface IMenuList {
  Id: number;
  PageId: number;
  Text: string;
  Link?: string;
}
