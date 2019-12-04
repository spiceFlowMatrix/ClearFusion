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
import { LogisticRequestStatus, LogisticComparativeStatus } from 'src/app/shared/enum';
import { GoodsRecievedUploadComponent } from '../goods-recieved-upload/goods-recieved-upload.component';


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
  requestDetail: RequestDetail = {ProjectId: '', Status: 0, TotalCost: '', RequestId: '' , ComparativeStatus: 0,
Currency: '', BudgetLine: '', Office: '', Description: ''};
  actions: TableActionsModel;
  totalCost = 0;
  unavailableItemCost = 0;
  availabilityPercentage = 0;
  submitPurchaseItems: any[] = [];
  hideItemColums;
  storeItemsList = [];
  storedropdownItemsList = [];
  goodsNoteSubmitted = false;
  goodsRecievedModel: GoodsRecievedNote;

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
    this.hideItemColums = of({
      headers: ['Item', 'Quantity', 'Estimated Cost', 'Availability'],
      items: ['Item', 'Quantity', 'EstimatedCost', 'Availability']
    });
    this.routeActive.params.subscribe(params => {
      this.requestId = +params['id'];
    });
    this.getRequestDetails();
    this.getAllRequestItems();
    this.getStoreItems();
  }
  addItemDialog() {
    const dialogRef = this.dialog.open(AddLogisticItemsComponent, {
      width: '400px',
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
    // this.getTotalRequestCost();
    this.getUnavailableItemsCost();
    this.getAvailabilityPercentage();
  }
  getRequestDetails() {
    this.logisticservice.getLogisticRequestDetail(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 && res.data.logisticRequest != null) {
        this.requestDetail = res.data.logisticRequest;
        // this.requestDetail.ProjectId = res.data.logisticRequest.ProjectId;
        // this.requestDetail.RequestName = res.data.logisticRequest.RequestName;
        // this.requestDetail.Status = res.data.logisticRequest.Status;
        // this.requestDetail.TotalCost = res.data.logisticRequest.TotalCost;
        // this.requestDetail.Currency = res.data.logisticRequest.Currency;
        // this.requestDetail.BudgetLine = res.data.logisticRequest.BudgetLine;
        // this.requestDetail.Office = res.data.logisticRequest.Office;
        // this.requestDetail.ComparativeStatus = res.data.logisticRequest.ComparativeStatus;
      }

      if (!(this.requestDetail.Status === 1) ) { // || !(this.requestDetail.ComparativeStatus === 1)
        this.actions = {
          items: {
            button: { status: false, text: '' },
            edit: false,
            delete: false,
            download: false,
          },
          subitems: {
          }
        };
      }

      if (this.requestDetail.Status === LogisticRequestStatus['Complete Purchase'] ) {
        this.getGoodsRecievedNote();
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
      // this.getTotalRequestCost();
      this.getUnavailableItemsCost();
      this.getAvailabilityPercentage();
    });
  }

  // getTotalRequestCost() {
  //   this.totalCost = 0;
  //   this.requestItemList.forEach(element => {
  //     this.totalCost += element.EstimatedCost;
  //   });
  // }

  getUnavailableItemsCost() {
    this.unavailableItemCost = 0;
    this.requestItemList.forEach(element => {
      if (element.Availability < element.Quantity) {
        this.unavailableItemCost += ((element.EstimatedCost) * (element.Quantity - element.Availability));
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
          const model = {
            ItemId: event.item.Id,
            RequestId: this.requestId
          };
          this.logisticservice.deleteLogisticRequestItemsById(model).subscribe(res => {
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
        width: '400px',
        data: {Id: event.item.Id, ItemId: event.item.ItemId, Quantity: event.item.Quantity,
          EstimatedCost: event.item.EstimatedCost, RequestId: this.requestId, 'Storeitems': this.storedropdownItemsList,
          }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result !== undefined && result.data != null ) {
          if (result.data === 'Success') {
            this.toastr.success('Updated Successfully!');
            this.getAllRequestItems();
            this.getRequestDetails();
          } else {
            this.toastr.warning(result.data);
          }
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
    this.getRequestDetails();
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
    if (this.requestItemList.length === 0) {
      this.toastr.warning('Please add items to purchase!');
      return;
    }
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

  // completePurchaseOrder() {
  //   if (this.submitPurchaseItems.length === 0) {
  //     this.toastr.warning('Submit Purchase items first!');
  //   } else {
  //     this.commonLoader.showLoader();
  //     const requestItems = this.submitPurchaseItems.map(function(val) {
  //       return {
  //         Id: val.Id,
  //         FinalCost: val.EstimatedCost
  //       };
  //     });
  //     const model = {
  //       submittedList: requestItems,
  //       Status : LogisticRequestStatus['Complete Purchase']
  //     };
  //     this.logisticservice.completePurchaseOrder(model).subscribe(res => {
  //       if (res.StatusCode === 200) {
  //         this.commonLoader.hideLoader();
  //         this.getRequestDetails();
  //       } else {
  //         this.commonLoader.hideLoader();
  //         this.toastr.error('Something went wrong!');
  //       }
  //     });
  //   }
  // }

  submitPurchase() {
    this.router.navigate(['submit-purchase'], { relativeTo: this.routeActive });
  }

  cancelComparativeRequest() {
    this.commonLoader.showLoader();
    this.logisticservice.cancelComparativeRequest(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.getRequestDetails();
      } else {
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  issueComparativeStatement() {
    this.commonLoader.showLoader();
    this.logisticservice.IssueComparativeStatement(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.getRequestDetails();
      } else {
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  comparativeStatusChange(value) {
    this.requestDetail.ComparativeStatus = value;
  }

  StatusChange(value) {
    this.requestDetail.Status = value;
    if (this.requestDetail.Status === LogisticRequestStatus['Complete Purchase'] ) {
      this.getGoodsRecievedNote();
    }
  }

  rejectComparativeStatement() {
    this.commonLoader.showLoader();
    this.logisticservice.rejectComparativeStatement(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.requestDetail.ComparativeStatus = LogisticComparativeStatus['Reject Statement'];
      } else {
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  approveComparativeStatement() {
    this.commonLoader.showLoader();
    this.logisticservice.approveComparativeStatement(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.requestDetail.ComparativeStatus = LogisticComparativeStatus['Approve Statement'];
        this.requestDetail.Status = LogisticRequestStatus['Issue Purchase Order'];
      } else {
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  getStoreItems() {
    this.logisticservice.getAllStoreItems().subscribe(res => {
      this.storeItemsList = [];
      this.storedropdownItemsList = [];
      if (res.StatusCode === 200 && res.data.InventoryItemList != null) {
        res.data.InventoryItemList.forEach(element => {
          this.storeItemsList.push(element);
          this.storedropdownItemsList.push({
            Id: element.ItemId,
            Name: element.ItemCode + '-' + element.ItemName
          });
        });
        // this.addLogisticItemsForm.controls['Item'].setValue(this.data.ItemId);
      }
    });
  }

  rejectPurchaseOrder() {
    this.logisticservice.rejectPurchaseOrder(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        this.requestDetail.Status = LogisticRequestStatus['Issue Purchase Order'];
      } else {
         this.toastr.error('Something went wrong!');
      }
    });
  }

  approvePurchaseOrder() {
    if (!this.goodsNoteSubmitted) {
      const dialogRef = this.dialog.open(GoodsRecievedUploadComponent, {
        width: '450px',
        data: {RequestId: this.requestId}
      });
      dialogRef.afterClosed().subscribe(result => {
        if (result !== undefined && result.data != null ) {
            this.goodsNoteSubmitted = true;
          } else {
            this.goodsNoteSubmitted = false;
          }
      });
    } else {

    }
  }

  getGoodsRecievedNote() {
    this.logisticservice.getGoodsRecievedNote(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        if (res.data.GoodsRecievedNote == null) {
          this.goodsNoteSubmitted = false;
        } else {
          this.goodsNoteSubmitted = true;
          this.goodsRecievedModel = res.data.GoodsRecievedNote;
        }
      } else {
         this.toastr.error('Something went wrong!');
      }
    });
  }

  editRequest() {
    if (this.requestDetail.Status !== LogisticRequestStatus['New Request']) {
      return;
    } else {
      this.router.navigate(['../../logistic-requests/new-request/'] ,
       { relativeTo: this.routeActive , queryParams: {requestId: this.requestId} });
    }
  }

}
export interface RequestDetail {
  RequestId;
  ProjectId;
  Status;
  TotalCost;
  ComparativeStatus;
  Currency;
  BudgetLine;
  Office;
  Description;
}

interface IItemList {
  Id;
  ItemId;
  Item;
  Quantity;
  EstimatedCost;
  Availability;
}

interface GoodsRecievedNote {
  AttachmentName;
  AttachmentUrl;
  UploadedBy;
}
