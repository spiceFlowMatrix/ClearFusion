import { Component, OnInit } from '@angular/core';
import {
  Router,
  RouteConfigLoadStart,
  RouteConfigLoadEnd
} from '@angular/router';
import { CommonLoaderService } from './shared/common-loader/common-loader.service';
import { SignalRService } from './shared/services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Humanitarian';

  constructor(
    private router: Router,
    public commonLoader: CommonLoaderService,
    private signalRService: SignalRService
  ) {}

  ngOnInit() {
    this.router.events.subscribe(event => {
      if (event instanceof RouteConfigLoadStart) {
        this.commonLoader.showLoader();
      } else if (event instanceof RouteConfigLoadEnd) {
        this.commonLoader.hideLoader();
      }
    });

    // hub connection
    this.startHubConnection();
  }

  startHubConnection() {
    this.signalRService.startConnection();
  }

}
