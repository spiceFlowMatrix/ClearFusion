import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { Observable } from 'rxjs/Observable';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { MatDialog } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { EmployeeContractService } from 'src/app/hr/services/employee-contract.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employee-advance-list',
  templateUrl: './employee-advance-list.component.html',
  styleUrls: ['./employee-advance-list.component.scss']
})
export class EmployeeAdvanceListComponent implements OnInit {

  contractListHeader$ = of([
    'Currency',
    'Approved By',
    'Mode of Return',
    'Request Amount',
    'Advance Amount',
    'Status'
  ]);

  actions: TableActionsModel;
  contractList$: Observable<any[]>;
  employeeId: number;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(public dialog: MatDialog,
    private datePipe: DatePipe,
    private commonLoader: CommonLoaderService,
    private routeActive: ActivatedRoute) {
      this.actions = {
        items: {
          button: { status: true, text: '' },
          download: false,
          edit: false,
          delete: false
        },
        subitems: {
          button: { status: false, text: '' },
          delete: false,
          download: false
        }
      };
    }

  ngOnInit() {
    this.routeActive.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
  }

}
