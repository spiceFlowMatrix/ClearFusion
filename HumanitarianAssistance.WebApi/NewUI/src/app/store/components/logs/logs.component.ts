import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, of, ReplaySubject } from 'rxjs';
import { ILogs } from '../../models/vehicles';
import { PurchaseService } from '../../services/purchase.service';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit, OnDestroy {
  logListHeaders$ = of(['Event Type', 'By', 'Event On', 'Detail']);
  logList$: Observable<ILogs[]>;
  // subject
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private purchaseService: PurchaseService) { }

  ngOnInit() {

    this.getLogs();

    this.logList$ = of([
      {
        EventType: 'Fuel Purchased',
        EventBy: 'User Name',
        EventOn: '25 Apr, 2019',
        Detail: '2 Liter Diesel Super Fuel Purchased In kjh43-a3f4rh54h-345h3-34'
      },
      {
        EventType: 'Fuel Purchased',
        EventBy: 'User Name',
        EventOn: '25 Apr, 2019',
        Detail: '2 Liter Diesel Super Fuel Purchased In kjh43-a3f4rh54h-345h3-34'
      },
      {
        EventType: 'Fuel Purchased',
        EventBy: 'User Name',
        EventOn: '25 Apr, 2019',
        Detail: '2 Liter Diesel Super Fuel Purchased In kjh43-a3f4rh54h-345h3-34'
      }
    ] as ILogs[]);
  }

  getLogs() {
    this.purchaseService.getStoreLogs()
      .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        debugger;

      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
