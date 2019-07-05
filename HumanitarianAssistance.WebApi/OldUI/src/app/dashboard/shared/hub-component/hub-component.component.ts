import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hub-component',
  templateUrl: './hub-component.component.html',
  styleUrls: ['./hub-component.component.css']
})
export class HubComponentComponent implements OnInit {
  private _hubConnection: HubConnection | undefined;
  public async: any;
  message = '';
  messages: string[] = [];

  constructor(private toastr:ToastrService) {
  }

  public sendMessage(): void {
      const data = `Sent: ${this.message}`;

      if (this._hubConnection) {
          this._hubConnection.invoke('Send', data);
      }
      this.messages.push(data);
  }

  ngOnInit() {
      this._hubConnection = new signalR.HubConnectionBuilder()
          .withUrl('https://localhost:5000/loopy')
          .configureLogging(signalR.LogLevel.Information)
          .build();

      this._hubConnection.start().catch(err => console.error(err.toString()));

      this._hubConnection.on('Send', (data: any) => {
        this.toastr.success(data);
          const received = `Received: ${data}`;
          this.messages.push(received);
      });
  }

}
