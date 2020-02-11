import { HrService } from 'src/app/hr/services/hr.service';
import { Component, OnInit } from '@angular/core';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { of, Observable } from 'rxjs';

@Component({
  selector: 'app-audit-logs',
  templateUrl: './audit-logs.component.html',
  styleUrls: ['./audit-logs.component.scss']
})
export class AuditLogsComponent implements OnInit {
  logListHeader$ = of(['Id', 'Entity Type', 'Action', 'ActionDescription']);
  logList$: Observable<any[]>;
  employeeId: number;
  constructor(
    private datePipe: DatePipe,
    private commonLoader: CommonLoaderService,
    private routeActive: ActivatedRoute,
    private hrService: HrService
  ) {}

  ngOnInit() {
    this.routeActive.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
    this.getAllAuditLogById();
  }

  getAllAuditLogById() {
    this.commonLoader.showLoader();
    this.hrService.GetAllAuditLogById(this.employeeId).subscribe(res => {
      if (res.AuditLogs != undefined) {
        this.logList$ = of(
          res.AuditLogs.map(y => {
            return {
              Id: y.AuditLogId,
              TypeOfEntity: y.TypeOfEntity,
              ActionType: y.ActionType,
              ActionDescription: y.ActionDescription
            };
          })
        );
      }
    });
    this.commonLoader.hideLoader();
  }
}
