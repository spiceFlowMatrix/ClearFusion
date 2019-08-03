import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { environment } from 'src/environments/environment';
import { AppUrlService } from './app-url.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection;


  public activityPermission$ = new BehaviorSubject<any[]>([]);
  public DemoMessage$ = new BehaviorSubject<any[]>([]);

  constructor(private appUrlService: AppUrlService) {

  }


  public startConnection(): void {
    // if ($.connection.hub && $.connection.hub.state === $.signalR.connectionState.disconnected) {
    //   $.connection.hub.start()
    // }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.appUrlService.getHubUrl())
      // .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started...'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public stopConnection(): void {
    this.hubConnection
      .stop()
      .then(() => console.log('Connection closed...'))
      .catch(err => console.log('Error while closing connection: ' + err));
  }


  //#region "activityPermissionChanged - invoke"
  public activityPermissionChangedInvoke(data: any): void {
    this.hubConnection.invoke('ActivityPermissionChanged', data);
  }
  //#endregion

  //#region "activityPermissionChanged - on"
  public activityPermissionChangedOn(): void {
    this.hubConnection.on('activityPermissionChanged', data => {
      console.log(data);
      this.activityPermission$.next(data);
    });
  }
  //#endregion

  public addTransferChartDataListener(): void {
    this.hubConnection.on('transferchartdata', data => {
      console.log(data);
    });
  }

// //#region "activityPermissionChanged - invoke"
// public AddMessageInvoke(data: any): void {
//   debugger;
//   this.hubConnection.start();
//   this.hubConnection.invoke('AddMessage', data);

//   // this.hubConnection.on('ReceiveMessage', x => {
//   //   console.log(x);
//   //   this.DemoMessage$.next(data);
//   // });
// }
//#endregion

public AddMessageInvoke(data: any) {
  this.hubConnection.invoke('AddMessage', data);
}

//#region "activityPermissionChanged - on"
public BroadcastMessageOn(): void {
  this.hubConnection.on('BroadcastMessageOn', data => {
    console.log(data);
  });
}
//#endregion

}
