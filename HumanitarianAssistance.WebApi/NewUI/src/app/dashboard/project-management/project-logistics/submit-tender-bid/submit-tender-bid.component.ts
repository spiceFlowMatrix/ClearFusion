import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { LogisticService } from '../logistic.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { takeUntil } from 'rxjs/operators';
import { FileSourceEntityTypes } from 'src/app/shared/enum';
import { ReplaySubject } from 'rxjs';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-submit-tender-bid',
  templateUrl: './submit-tender-bid.component.html',
  styleUrls: ['./submit-tender-bid.component.scss']
})
export class SubmitTenderBidComponent implements OnInit {

  tenderBidForm: FormGroup;
  attachment = [];
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<SubmitTenderBidComponent>,
    private toastr: ToastrService, private logisticservice: LogisticService, private commonLoader: CommonLoaderService,
    private globalSharedService: GlobalSharedService, @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.tenderBidForm = this.fb.group({
      Name: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      PhoneNumber: ['', [
        Validators.required,
        Validators.pattern(/^-?(0|[1-9]\d*)?$/),
        Validators.minLength(10),
        Validators.maxLength(14)
        ]],
      Address: [''],
      Owner: ['', Validators.required],
      OpeningDate: ['', Validators.required],
      SecurityDate: ['', Validators.required],
      QuotedAmount: ['', Validators.required],
      SecurityAmount: ['', Validators.required],
      isResultQualified: [false, Validators.required],

      Profile_Experience: [0, Validators.required],
      OfferValididty: [0, Validators.required],
      TOR_SOWAcceptance: [0, Validators.required],
      Securities_BankGuarantee: [0, Validators.required],
      OfferDocumentation: [0, Validators.required],

      Company_GoodsSpecification: [0, Validators.required],
      Service_Warranty: [0, Validators.required],
      Certification_GMP_COPP: [0, Validators.required],
      WorkExperience: [0, Validators.required],
      DeliveryDate: [0, Validators.required],
      LogisticRequestsId : [this.data.RequestId]
    });
  }

  openInput() {
    document.getElementById('fileInput').click();
  }

  fileChange(file: any) {
    this.attachment = [];
    this.attachment.push(file);
  }

  onResultsQualifiedChange(value) {
    this.tenderBidForm.controls['isResultQualified'].setValue(value.checked);
  }

  closeDialog() {
    this.dialogRef.close({data: null});
  }

  addTenderBid(value) {
    if (!this.tenderBidForm.valid) {
      this.toastr.warning('Please fill required fields!');
      return;
    }
    if (this.attachment.length === 0) {
      this.toastr.warning('Please attach Contract/Guarantee Letter!');
      return;
    }
    this.tenderBidForm.controls.OpeningDate.setValue(StaticUtilities.getLocalDate(this.tenderBidForm.get('OpeningDate').value));
    this.tenderBidForm.controls.SecurityDate.setValue(StaticUtilities.getLocalDate(this.tenderBidForm.get('SecurityDate').value));
    this.commonLoader.showLoader();
    this.logisticservice.addTenderBid(value).subscribe(res => {
      if (res.StatusCode === 200 && res.CommonId.LongId != null) {
        this.globalSharedService
            .uploadFile(FileSourceEntityTypes.TenderBidContractLetter, res.CommonId.LongId, this.attachment[0][0])
            .pipe(takeUntil(this.destroyed$))
            .subscribe(y => {
                this.commonLoader.hideLoader();
                this.toastr.success('Bid Submitted Successfully!');
                this.dialogRef.close({data: 'Success'});
            },
            err => {
              this.commonLoader.hideLoader();
              this.toastr.error('Something went wrong!');
            });
      }
    });

  }
}
