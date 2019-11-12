import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/lib/models/table-actions-model';
import { AddLogisticItemsComponent } from '../add-logistic-items/add-logistic-items.component';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { LogisticService } from '../logistic.service';
import { RequestDetailComponent } from '../../project-hiring/request-detail/request-detail.component';
import { map } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { LogisticRequestStatus } from 'src/app/shared/enum';


@Component({
  selector: 'app-logistic-request-details',
  templateUrl: './logistic-request-details.component.html',
  styleUrls: ['./logistic-request-details.component.scss']
})
export class LogisticRequestDetailsComponent implements OnInit {

  requestedItemsHeaders$ = of([
    'Id',
    'ItemId',
    'Item',
    'Quantity',
    'Estimated Cost',
    'Availability'
  ]);
  requestedItemsData$: Observable<IItemList[]>;
  requestId;
  requestItemList: any[];
  requestDetail: RequestDetail = {RequestName: '', ProjectId: '', Status: 0, TotalCost: '', RequestId: '' , ComparativeStatus: 0,
Currency: '', BudgetLine: '', Office: ''};
  actions: TableActionsModel;
  totalCost = 0;
  unavailableItemCost = 0;
  availabilityPercentage = 0;
  submitPurchaseItems: any[] = [];

  constructor(private dialog: MatDialog, private routeActive: ActivatedRoute,
    private logisticservice: LogisticService,
    public toastr: ToastrService,
    private commonLoader: CommonLoaderService,
    private router: Router) { }

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
    this.getAllRequestItems();
  }
  addItemDialog() {
    const dialogRef = this.dialog.open(AddLogisticItemsComponent, {
      width: '300px',
      data: {RequestId: this.requestId}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        this.getAllRequestItems();
      }
    });
  }

  refreshItemList(value) {
    this.requestItemList.push(value);
    this.requestedItemsData$ = of(this.requestItemList).pipe(
      map(r => r.map(v => ({
        Id: v.Id,
        ItemId: v.ItemId,
        Item: v.Item,
        Quantity: v.Quantity,
        EstimatedCost: v.EstimatedCost,
        Availability: v.Availability
       }) as IItemList)));
    this.getTotalRequestCost();
    this.getUnavailableItemsCost();
    this.getAvailabilityPercentage();
  }
  getRequestDetails() {
    this.logisticservice.getLogisticRequestDetail(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 && res.data.logisticRequest != null) {
        this.requestDetail.RequestId = res.data.logisticRequest.RequestId;
        this.requestDetail.ProjectId = res.data.logisticRequest.ProjectId;
        this.requestDetail.RequestName = res.data.logisticRequest.RequestName;
        this.requestDetail.Status = res.data.logisticRequest.Status;
        this.requestDetail.TotalCost = res.data.logisticRequest.TotalCost;
        this.requestDetail.Currency = res.data.logisticRequest.Currency;
        this.requestDetail.BudgetLine = res.data.logisticRequest.BudgetLine;
        this.requestDetail.Office = res.data.logisticRequest.Office;
        this.requestDetail.ComparativeStatus = 1;
      }
    });
  }

  getAllRequestItems() {
    this.logisticservice.getAllRequestItems(this.requestId).subscribe(res => {
      this.requestItemList = [];
      if (res.StatusCode === 200 && res.data.LogisticsItemList != null) {
        res.data.LogisticsItemList.forEach(element => {
          this.requestItemList.push(element);
        });
        this.requestedItemsData$ = of(this.requestItemList).pipe(
          map(r => r.map(v => ({
            Id: v.Id,
            ItemId: v.ItemId,
            Item: v.Item,
            Quantity: v.Quantity,
            EstimatedCost: v.EstimatedCost,
            Availability: v.Availability
           }) as IItemList)));
      }
      this.getTotalRequestCost();
      this.getUnavailableItemsCost();
      this.getAvailabilityPercentage();
    });
  }

  getTotalRequestCost() {
    this.totalCost = 0;
    this.requestItemList.forEach(element => {
      this.totalCost += element.EstimatedCost;
    });
  }

  getUnavailableItemsCost() {
    this.unavailableItemCost = 0;
    this.requestItemList.forEach(element => {
      if (element.Availability < element.Quantity) {
        this.unavailableItemCost += ((element.EstimatedCost / element.Quantity) * (element.Quantity - element.Availability));
      }
    });
  }

  getAvailabilityPercentage() {
    this.availabilityPercentage = 0;
    let TotalAvailibility = 0;
    let TotalQuantity = 0;
    this.requestItemList.forEach(element => {
      TotalAvailibility += element.Availability;
      TotalQuantity += element.Quantity;
    });
    this.availabilityPercentage = (TotalAvailibility / TotalQuantity) * 100;
  }

  onActionClick(event) {
    if (event.type === 'delete') {
      this.logisticservice.openDeleteDialog().subscribe(v => {
        if (v) {
          this.logisticservice.deleteLogisticRequestItemsById(event.item.Id).subscribe(res => {
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
    if (event.type === 'edit') {
      const dialogRef = this.dialog.open(AddLogisticItemsComponent, {
        width: '300px',
        data: {Id: event.item.Id, ItemId: event.item.ItemId, Quantity: event.item.Quantity,
          EstimatedCost: event.item.EstimatedCost, RequestId: this.requestId}
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result !== undefined && result.data != null ) {
          this.getAllRequestItems();
        }
      });
    }
  }

  refreshRequestListAfterDelete(value) {
    const index = this.requestItemList.findIndex(v => v.Id === value);
    if (index !== -1) {
      this.requestItemList.splice(index, 1);
    }
    this.requestedItemsData$ = of(this.requestItemList).pipe(
      map(r => r.map(v => ({
        Id: v.Id,
        ItemId: v.ItemId,
        Item: v.Item,
        Quantity: v.Quantity,
        EstimatedCost: v.EstimatedCost,
        Availability: v.Availability
       }) as IItemList)));
    this.getTotalRequestCost();
    this.getUnavailableItemsCost();
    this.getAvailabilityPercentage();
  }

  cancelLogisticRequest() {
    this.commonLoader.showLoader();
    this.logisticservice.cancelLogisticRequest(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 ) {
        this.getRequestDetails();
        this.commonLoader.hideLoader();
      } else {
        this.commonLoader.hideLoader();
      }
    });
  }

  issuePurchaseOrder() {
    this.commonLoader.showLoader();
    this.logisticservice.issuePurchaseOrder(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 ) {
        this.getRequestDetails();
        this.commonLoader.hideLoader();
      } else {
        this.commonLoader.hideLoader();
      }
    });
  }

  onBackClick() {
    this.router.navigate(['../../logistic-requests'] , { relativeTo: this.routeActive });
  }

  selectedItemChange(value) {
    this.submitPurchaseItems = value;
  }

  completePurchaseOrder() {
    if (this.submitPurchaseItems.length === 0) {
      this.toastr.warning('Submit Purchase items first!');
    } else {
      this.commonLoader.showLoader();
      const requestItems = this.submitPurchaseItems.map(function(val) {
        return val.Id;
      });
      const model = {
        Id: requestItems,
        Status : LogisticRequestStatus['Complete Purchase']
      };
      this.logisticservice.completePurchaseOrder(model).subscribe(res => {
        if (res.StatusCode === 200) {
          this.commonLoader.hideLoader();
          this.getRequestDetails();
        } else {
          this.commonLoader.hideLoader();
          this.toastr.error('Something went wrong!');
        }
      });
      console.log(model);
    }
  }

}
interface RequestDetail {
  RequestId;
  ProjectId;
  RequestName;
  Status;
  TotalCost;
  ComparativeStatus;
  Currency;
  BudgetLine;
  Office;
}

interface IItemList {
  Id;
  ItemId;
  Item;
  Quantity;
  EstimatedCost;
  Availability;
}
