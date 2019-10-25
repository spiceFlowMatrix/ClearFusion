import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/lib/models/table-actions-model';
import { AddLogisticItemsComponent } from '../add-logistic-items/add-logistic-items.component';
import { MatDialog } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { LogisticService } from '../logistic.service';
import { RequestDetailComponent } from '../../project-hiring/request-detail/request-detail.component';
import { map } from 'rxjs/operators';


@Component({
  selector: 'app-logistic-request-details',
  templateUrl: './logistic-request-details.component.html',
  styleUrls: ['./logistic-request-details.component.scss']
})
export class LogisticRequestDetailsComponent implements OnInit {

  requestedItemsHeaders$ = of([
    'ItemId',
    'Item',
    'Quantity',
    'Estimated Cost',
    'Availability'
  ]);
  requestedItemsData$: Observable<IItemList[]>;
  requestId;
  requestItemList: any[];
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
      width: '300px',
      data: {RequestId: this.requestId}
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

  getAllRequestItems() {
    this.logisticservice.getAllRequestItems(this.requestId).subscribe(res => {
      this.requestItemList = [];
      if (res.StatusCode === 200 && res.data.requestItemList != null) {
        res.data.logisticRequestList.forEach(element => {
          this.requestItemList.push(element);
        });
        this.requestedItemsData$ = of(this.requestItemList).pipe(
          map(r => r.map(v => ({
            ItemId: v.ItemId,
            Item: v.ItemName,
            Quantity: v.RequestName,
            EstimatedCost: v.TotalCost,
            Availability: v.Availability
           }) as IItemList)));
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

interface IItemList {
  ItemId;
  Item;
  Quantity;
  EstimatedCost;
  Availability;
}
