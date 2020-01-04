import { Component, OnInit, Input, OnChanges, EventEmitter, Output } from '@angular/core';
import { LogisticService } from '../logistic.service';
import { MatDialog } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { SubmitTenderDocumentComponent } from '../submit-tender-document/submit-tender-document.component';
import { ActivatedRoute } from '@angular/router';
import { LogisticTenderStatus, FileSourceEntityTypes, LogisticRequestStatus } from 'src/app/shared/enum';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { SubmitTenderBidComponent } from '../submit-tender-bid/submit-tender-bid.component';
import { TenderBidSelectionComponent } from '../tender-bid-selection/tender-bid-selection.component';

@Component({
  selector: 'app-tender-statement',
  templateUrl: './tender-statement.component.html',
  styleUrls: ['./tender-statement.component.scss']
})
export class TenderStatementComponent implements OnInit {

  @Input() requestStatus = 0;
  @Input() totalCost = 0;
  @Input() tenderStatus = 1;

  @Output() tenderStatusChange = new EventEmitter();
  @Output() StatusChange = new EventEmitter();
  @Input() requestedItems: any[];
  @Output() selectedItemChange = new EventEmitter();

  requestId;
  tenderDocsList: any[] = [];
  tenderBidsList: any[] = [];
  constructor(private logisticservice: LogisticService,
    private dialog: MatDialog, private routeActive: ActivatedRoute,
    public toastr: ToastrService, private commonLoader: CommonLoaderService) {
      this.routeActive.params.subscribe(params => {
        this.requestId = +params['id'];
      });
    }

  ngOnInit() {
    if (this.tenderStatus === LogisticTenderStatus.Issued) {
      this.getTenderProposalDocument();
      this.getAllTenderBids();
    }
  }

  openFilesDialog() {
    const dialogRef = this.dialog.open(SubmitTenderDocumentComponent, {
      width: '450px',
      data: {RequestId: this.requestId}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        this.getTenderProposalDocument();
      }
    });
  }

  getTenderProposalDocument() {
    this.logisticservice.getTenderProposalDocument(this.requestId).subscribe(res => {
      this.tenderDocsList = [];
      if (res.StatusCode === 200 && res.data.TenderProposalDoc != null) {
        this.tenderDocsList = res.data.TenderProposalDoc;
      }
    });
  }

  deleteTenderDocument(docFileId) {
    this.commonLoader.showLoader();
    this.logisticservice.deleteTenderProposalDocument(docFileId).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.getTenderProposalDocument();
        this.toastr.success('Deleted Successfully!');
      } else {
        this.commonLoader.hideLoader();
        this.toastr.error(res.Message);
      }
    });
  }

  submitTenderBid() {
    const dialogRef = this.dialog.open(SubmitTenderBidComponent, {
      width: '500px',
      data: {RequestId: this.requestId}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        this.getAllTenderBids();
      }
    });
  }

  getAllTenderBids() {
    this.logisticservice.getAllTenderBids(this.requestId).subscribe(res => {
      this.tenderBidsList = [];
      if (res.StatusCode === 200 && res.data.TenderBids != null) {
        this.tenderBidsList = res.data.TenderBids;
      }
    });
  }

  editTenderBid(BidId) {
    const dialogRef = this.dialog.open(SubmitTenderBidComponent, {
      width: '500px',
      data: {RequestId: this.requestId, BidDetail: this.tenderBidsList.filter(x => x.BidId === BidId)[0]}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        this.getAllTenderBids();
      }
    });
  }

  deleteTenderBid(BidId) {
    this.logisticservice.openDeleteDialog().subscribe(v => {
      if (v) {
        this.logisticservice.deleteTenderBidById(BidId).subscribe(res => {
          if (res.StatusCode === 200) {
            this.getAllTenderBids();
            this.toastr.success('Deleted Sucessfully!');
          } else {
            this.toastr.error('Something went wrong!');
          }
        });
      }
    });
  }

  selectTenderBid() {
    const dialogRef = this.dialog.open(TenderBidSelectionComponent, {
      width: '400px',
      data: {BidDetail: this.tenderBidsList}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
         this.tenderStatusChange.emit(LogisticTenderStatus['Bid Selected']);
         this.StatusChange.emit(LogisticRequestStatus['Issue Purchase Order']);
      }
    });
  }

  selectedPurchaseItemChange(value) {
    this.selectedItemChange.emit(value);
  }

  StatusEmit(value) {
    this.StatusChange.emit(value);
  }
}
