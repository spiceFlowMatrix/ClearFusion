import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { LogisticService } from '../logistic.service';
import { MatDialog } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { SubmitTenderDocumentComponent } from '../submit-tender-document/submit-tender-document.component';
import { ActivatedRoute } from '@angular/router';
import { LogisticTenderStatus } from 'src/app/shared/enum';

@Component({
  selector: 'app-tender-statement',
  templateUrl: './tender-statement.component.html',
  styleUrls: ['./tender-statement.component.scss']
})
export class TenderStatementComponent implements OnInit {

  @Input() requestStatus = 0;
  @Input() totalCost = 0;
  @Input() tenderStatus = 1;

  requestId;
  tenderDocsList: any[] = [];
  constructor(private logisticservice: LogisticService,
    private dialog: MatDialog, private routeActive: ActivatedRoute,
    public toastr: ToastrService) {
      this.routeActive.params.subscribe(params => {
        this.requestId = +params['id'];
      });
    }

  ngOnInit() {
    if (this.tenderStatus === LogisticTenderStatus.Issued) {
      this.getTenderProposalDocument();
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

}
