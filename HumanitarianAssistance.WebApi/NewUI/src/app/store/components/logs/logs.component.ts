import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ILogs } from '../../models/vehicles';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit {
  logListHeaders$ = of(["Event Type", "By", "Event On", "Detail"]);
  logList$: Observable<ILogs[]>;
  constructor() { }

  ngOnInit() {
    this.logList$ = of([
      {
        EventType: 'Fuel Purchased',
        EventBy: 'User Name',
        EventOn: '25 Apr, 2019',
        Detail:'2 Liter Diesel Super Fuel Purchased In kjh43-a3f4rh54h-345h3-34'
      },
      {
        EventType: 'Fuel Purchased',
        EventBy: 'User Name',
        EventOn: '25 Apr, 2019',
        Detail:'2 Liter Diesel Super Fuel Purchased In kjh43-a3f4rh54h-345h3-34'
      },
      {
        EventType: 'Fuel Purchased',
        EventBy: 'User Name',
        EventOn: '25 Apr, 2019',
        Detail:'2 Liter Diesel Super Fuel Purchased In kjh43-a3f4rh54h-345h3-34'
      }
    ] as ILogs[]);
    // console.log(this.logList$);
  }


}
