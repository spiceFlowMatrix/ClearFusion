import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/lib/models/table-actions-model';
import { AddLogisticRequestComponent } from '../add-logistic-request/add-logistic-request.component';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { LogisticService } from '../logistic.service';
import { map } from 'rxjs/operators';
import { LogisticRequestStatus } from 'src/app/shared/enum';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';

@Component({
  selector: 'app-logistic-requests',
  templateUrl: './logistic-requests.component.html',
  styleUrls: ['./logistic-requests.component.scss']
})
export class LogisticRequestsComponent implements OnInit {

  logisticListHeaders$ = of([
    'Id',
    'Name',
    'Status',
    'Total Cost',
  ]);
  logisticListData$: Observable<any>;
  actions: TableActionsModel;
  projectId;
  logisticRequestList;
  constructor(
    private dialog: MatDialog,
    private router: Router,
    private routeActive: ActivatedRoute,
    private logisticservice: LogisticService,
    private commonLoader: CommonLoaderService,
    public toastr: ToastrService
    ) { }

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
    this.getAllRequest();
  }

  openAddRequestDialog(): void {
    const dialogRef = this.dialog.open(AddLogisticRequestComponent, {
      width: '300px',
      data: {ProjectId: this.projectId}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        this.refreshRequestList(result.data);
      }
    });
  }

  refreshRequestList(value) {
    this.logisticRequestList.push(value);
    this.logisticListData$ = of(this.logisticRequestList).pipe(
      map(r => r.map(v => ({
        Id: v.RequestId,
        Name: v.RequestName,
        Status: LogisticRequestStatus[v.Status],
        TotalCost: v.TotalCost
       }) as IRequestList)));
  }
  requestRowClicked(event) {
    this.router.navigate(['logistic-requests/', 1]);
  }

  getAllRequest() {
    this.logisticservice.getAllLogisticRequests(this.projectId).subscribe(res => {
      this.logisticRequestList = [];
      if (res.StatusCode === 200 && res.data.logisticRequestList != null) {
        res.data.logisticRequestList.forEach(element => {
          this.logisticRequestList.push(element);
        });
        this.logisticListData$ = of(this.logisticRequestList).pipe(
          map(r => r.map(v => ({
            Id: v.RequestId,
            Name: v.RequestName,
            Status: ((LogisticRequestStatus[v.Status] === 'NewRequest') ? 'New Request' : LogisticRequestStatus[v.Status]),
            TotalCost: v.TotalCost
           }) as IRequestList)));
      }
    });
  }

  onDeleteRequest(event) {
    if (event.type === 'delete') {
      this.logisticservice.openDeleteDialog().subscribe(v => {
        if (v) {
          this.logisticservice.deleteLogisticRequestById(event.item.Id).subscribe(res => {
            if (res.StatusCode === 200) {
              this.refreshRequestListAfterDelete(event.item.Id);
              this.toastr.success('Deleted Sucessfully!');
            } else {
              this.toastr.error('Something went wrong!');
            }
          });
        }
      });
    }
  }

  refreshRequestListAfterDelete(value) {
    const index = this.logisticRequestList.findIndex(v => v.RequestId === value);
    if (index !== -1) {
      this.logisticRequestList.splice(index, 1);
    }
    this.logisticListData$ = of(this.logisticRequestList).pipe(
      map(r => r.map(v => ({
        Id: v.RequestId,
        Name: v.RequestName,
        Status: ((LogisticRequestStatus[v.Status] === 'NewRequest') ? 'New Request' : LogisticRequestStatus[v.Status]),
        TotalCost: v.TotalCost
       }) as IRequestList)));
  }
}

interface IRequestList {
  Name;
  Status;
  TotalCost;
}
