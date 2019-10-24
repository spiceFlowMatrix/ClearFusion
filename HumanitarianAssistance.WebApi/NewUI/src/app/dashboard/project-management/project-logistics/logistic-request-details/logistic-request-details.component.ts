import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/lib/models/table-actions-model';
import { AddLogisticItemsComponent } from '../add-logistic-items/add-logistic-items.component';
import { MatDialog } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { LogisticService } from '../logistic.service';
import { RequestDetailComponent } from '../../project-hiring/request-detail/request-detail.component';


@Component({
  selector: 'app-logistic-request-details',
  templateUrl: './logistic-request-details.component.html',
  styleUrls: ['./logistic-request-details.component.scss']
})
export class LogisticRequestDetailsComponent implements OnInit {

  requestedItemsHeaders$ = of([
    'Item',
    'Quantity',
    'Estimated Cost',
    'Availability'
  ]);
  requestedItemsData$ = of([
    {'Item': 'Item1', 'Quantity': '2', 'Estimated Cost': '8500' , 'Availability': '3'},
    {'Item': 'Item2', 'Quantity': '3', 'Estimated Cost': '9200' , 'Availability': '4'}
  ]);
  requestId;
  requestDetail: RequestDetail = {RequestName: '', ProjectId: '', Status: 0, TotalCost: '', RequestId: ''};
  actions: TableActionsModel;
  constructor(private dialog: MatDialog, private routeActive: ActivatedRoute, private logisticservice: LogisticService) { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        edit: true,
        delete: true,
        download: false,
      },
      subitems: {
      }

    };
    this.routeActive.params.subscribe(params => {
      this.requestId = +params['id'];
    });
    this.getRequestDetails();
  }
  addItemDialog() {
    const dialogRef = this.dialog.open(AddLogisticItemsComponent, {
      width: '300px'
      // data: {name: this.name, animal: this.animal}
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  getRequestDetails() {
    this.logisticservice.getLogisticRequestDetail(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 && res.data.logisticRequest != null) {
        this.requestDetail.RequestId = res.data.logisticRequest.RequestId;
        this.requestDetail.ProjectId = res.data.logisticRequest.ProjectId;
        this.requestDetail.RequestName = res.data.logisticRequest.RequestName;
        this.requestDetail.Status = res.data.logisticRequest.Status;
        this.requestDetail.TotalCost = res.data.logisticRequest.TotalCost;
      }
    });
  }

}
interface RequestDetail {
RequestId;
ProjectId;
RequestName;
Status;
TotalCost;
}
