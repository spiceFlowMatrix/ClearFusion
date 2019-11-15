import { Component, OnInit, OnDestroy, Input } from '@angular/core';
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

  @Input() transportType: number; // TransportType vehicle or generator
  @Input() entityId: number; // VehicleId or GeneratorId

  constructor(private purchaseService: PurchaseService) { }

  ngOnInit() {
    this.getLogs();
  }

  getLogs() {
    this.purchaseService.getStoreLogs(this.transportType, this.entityId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(x => {
        if (x !== undefined && x.length > 0) {
          this.logList$ = of(x.map(y => {
            return {
              EventType: y.EventType,
              EventBy: y.EventBy,
              EventOn: y.EventOn,
              Detail: y.LogText + (y.PurchaseId ? '<a href=store/purchase/edit/' + y.PurchaseId
                                                  + '>' + y.PurchaseName + '</a>' : ''),
            } as ILogs;
          }));
        }
      });
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
