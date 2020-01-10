import { Component, OnInit, Input, EventEmitter, Output, OnChanges } from '@angular/core';
import { SubmitPurchaseListComponent } from '../submit-purchase-list/submit-purchase-list.component';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { LogisticService } from '../logistic.service';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { LogisticRequestStatus } from 'src/app/shared/enum';
import { ToastrService } from 'ngx-toastr';
import { GoodsRecievedUploadComponent } from '../goods-recieved-upload/goods-recieved-upload.component';
import { PurchaseVoucherVerificationComponent } from '../purchase-voucher-verification/purchase-voucher-verification.component';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';

@Component({
  selector: 'app-purchase-order',
  templateUrl: './purchase-order.component.html',
  styleUrls: ['./purchase-order.component.scss']
})
export class PurchaseOrderComponent implements OnInit, OnChanges {

  @Input() requestStatus = 0;
  @Input() comparativeStatus = 1;
  @Input() totalCost = 0;
  @Input() requestedItems: any[];
  @Input() requestId;
  @Output() selectedItemChange = new EventEmitter();
  @Output() StatusChange = new EventEmitter();
  @Output() goodsRecievedChange = new EventEmitter();

  purchasedItemsHeaders$ = of(['Item', 'Quantity', 'Final Cost']);
  purchasedItemsData$ = of([]);
  selectedItems: any[];
  goodsNoteSubmitted = false;
  goodsRecievedModel: GoodsRecievedNote = {AttachmentName: '', AttachmentUrl: '', UploadedBy: ''};
  purchaseOrderDetail;

  constructor(private dialog: MatDialog,
    private routeActive: ActivatedRoute,
    private logisticservice: LogisticService,
    private router: Router,
    public toastr: ToastrService,
    private globalService: GlobalSharedService,
    private appurl: AppUrlService) {
      this.logisticservice.goodsRecievedChange$.subscribe(val => {
        this.goodsNoteSubmitted = val;
        if (this.goodsNoteSubmitted) {
          this.getGoodsRecievedNote();
        }
      });
    }

  ngOnInit() {
  }

  ngOnChanges() {
    if (this.requestStatus === 4) {
      this.getPurchasedItemsList();
      this.getGoodsRecievedNote();
    }
    if (this.requestStatus === 7) {
      this.getPurchasedItemsList();
      this.getGoodsRecievedNote();
      this.getCompletedPurchaseOrderDetail();
    }
  }
  submitPurchase() {
    this.router.navigate(['submit-purchase'], { relativeTo: this.routeActive });
  }

  getPurchasedItemsList() {
    this.logisticservice.getPurchasedItemsList(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 && res.data.LogisticsItemList != null) {
        this.purchasedItemsData$ = of(res.data.LogisticsItemList).pipe(
          map(r => r.map(v => ({
            Item: v.Item,
            Quantity: v.Quantity,
            FinalCost: v.EstimatedCost,
           }) )));
      } else {
        // this.toastr.error('Something went wrong!');
      }
    });
  }

  rejectPurchaseOrder() {
    this.logisticservice.rejectPurchaseOrder(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        this.StatusChange.emit(LogisticRequestStatus['Issue Purchase Order']);
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
          this.logisticservice.goodsRecievedChange$.next(true);
          // this.getGoodsRecievedNote();
          } else {
          }
      });
    } else {
      const dialogRef = this.dialog.open(PurchaseVoucherVerificationComponent, {
        width: '500px',
        height: '630px',
        data: {RequestId: this.requestId}
      });
      dialogRef.afterClosed().subscribe(result => {
        if (result !== undefined && result.data != null ) {
            this.requestStatus = LogisticRequestStatus['Purchase Completed'];
            this.StatusChange.emit(this.requestStatus);
            this.getCompletedPurchaseOrderDetail();
          } else {
          }
      });
    }
  }

  getGoodsRecievedNote() {
    this.logisticservice.getGoodsRecievedNote(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        if (res.data.GoodsRecievedNote == null) {
          this.logisticservice.goodsRecievedChange$.next(false);
        } else {
          if ( !this.logisticservice.goodsRecievedChange$.getValue()) {
            this.logisticservice.goodsRecievedChange$.next(true);
          }
          this.goodsRecievedModel = res.data.GoodsRecievedNote;
        }
      } else {
         this.toastr.error('Something went wrong!');
      }
    });
  }

  getCompletedPurchaseOrderDetail() {
    this.logisticservice.getCompletedPurchaseOrderDetail(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 && res.data.PurchaseOrderDetail !== null) {
        this.purchaseOrderDetail = res.data.PurchaseOrderDetail;
        this.logisticservice.VoucherReference$.next(this.purchaseOrderDetail.VoucherReferenceNo);
      } else {
         this.toastr.error('Something went wrong!');
      }
    });
  }

  navigateToPurchase(id) {
    this.router.navigate(['/store/purchase/edit/' + id]);
  }

  //#region "Download goods received pdf"
  downLoadPdf() {
     const  LogisticRequestId: any = this.requestId;
     if (LogisticRequestId != null && LogisticRequestId !== undefined) {
      this.globalService
        .getFile(
          this.appurl.getApiUrl() +
            GLOBAL.API_Pdf_GetLogisticGoodsNoteReportPdf,
            LogisticRequestId
        )
        .pipe()
        .subscribe();
    }
  }
  //#endregion
}

interface GoodsRecievedNote {
  AttachmentName: string;
  AttachmentUrl: string;
  UploadedBy: string;
}
