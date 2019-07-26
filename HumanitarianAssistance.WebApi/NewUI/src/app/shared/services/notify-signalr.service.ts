import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { AppUrlService } from './app-url.service';
import { Subject } from 'rxjs/internal/Subject';

@Injectable({
  providedIn: 'root'
})
export class NotifySignalRService {

  private hubConnection: signalR.HubConnection;

  public DemoMessage$ = new Subject<IChatModel>();

  constructor(private appUrlService: AppUrlService) {

  }


  public startConnection(): void {

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.appUrlService.getNotifyHubUrl())
      // .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection.on('BroadcastMessage', data => {
      console.log(data);
      this.DemoMessage$.next(data);
    });

    this.hubConnection
      .start()
      .then(() => console.log('Notify Connection started...'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public stopConnection(): void {
    this.hubConnection
      .stop()
      .then(() => console.log('Notify Connection closed...'))
      .catch(err => console.log('Error while closing connection: ' + err));
  }

  //#region "activityPermissionChanged - on"
  public BroadcastMessageOn() {

  }
  //#endregion
}

interface IChatModel {
  SourceEntityTypeId: number;
  EntityId: number;
  Message: string;
  UserName: string;
  }
