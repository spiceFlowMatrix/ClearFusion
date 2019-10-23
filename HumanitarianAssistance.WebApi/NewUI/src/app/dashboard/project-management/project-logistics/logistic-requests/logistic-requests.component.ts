import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/lib/models/table-actions-model';
import { AddLogisticRequestComponent } from '../add-logistic-request/add-logistic-request.component';
import { MatDialog } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-logistic-requests',
  templateUrl: './logistic-requests.component.html',
  styleUrls: ['./logistic-requests.component.scss']
})
export class LogisticRequestsComponent implements OnInit {

  logisticListHeaders$ = of([
    'Name',
    'Status',
    'Total Cost',
  ]);
  logisticListData$ = of([
    {'Name': 'Test', 'Status': 'New Request', 'Total Cost': '8500' },
    {'Name': 'Abc', 'Status': 'New Request', 'Total Cost': '9200' }
  ]);
  actions: TableActionsModel;
  projectId;
  constructor(private dialog: MatDialog, private router: Router, private routeActive: ActivatedRoute) { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
      },
      subitems: {
      }

    };
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
  }

  openAddRequestDialog(): void {
    const dialogRef = this.dialog.open(AddLogisticRequestComponent, {
      width: '300px',
      data: {ProjectId: this.projectId}
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  requestRowClicked(event) {
    this.router.navigate(['logistic-requests/', 1]);
  }

}
