import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidatorFn } from '@angular/forms';
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
      Email: ['', [Validators.required, Validators.email, this.emailValidator(this.data)]],
      PhoneNumber: ['', [
        Validators.required,
        Validators.pattern(/^-?(0|[1-9]\d*)?$/),
        Validators.minLength(10),
        Validators.maxLength(14),
        this.phoneValidator(this.data)
        ]],
      Address: [''],
      Owner: ['', Validators.required],
      OpeningDate: ['', Validators.required],
      TenderDeliveryDate: ['', Validators.required],
      QuotedAmount: ['', Validators.required],
      SecurityAmount: ['', Validators.required],
      isResultQualified: [false, Validators.required],

      Profile_Experience: [0, Validators.required],
      OfferValidity: [0, Validators.required],
      TOR_SOWAcceptance: [0, Validators.required],
      Securities_BankGuarantee: [0, Validators.required],
      OfferDocumentation: [0, Validators.required],

      Company_GoodsSpecification: [0, Validators.required],
      Service_Warranty: [0, Validators.required],
      Certification_GMP_COPP: [0, Validators.required],
      WorkExperience: [0, Validators.required],
      DeliveryDateScore: [0, Validators.required],
      LogisticRequestsId : [this.data.RequestId]
    });

    if (this.data.BidDetail !== undefined) {
      this.tenderBidForm.patchValue({
        Name: this.data.BidDetail.Name,
        Email: this.data.BidDetail.Email,
        PhoneNumber: this.data.BidDetail.PhoneNumber,
        Address: this.data.BidDetail.Address,
        Owner: this.data.BidDetail.Owner,
        OpeningDate: this.data.BidDetail.OpeningDate,
        TenderDeliveryDate: this.data.BidDetail.TenderDeliveryDate,
        QuotedAmount: this.data.BidDetail.QuotedAmount,
        SecurityAmount: this.data.BidDetail.SecurityAmount,
        isResultQualified: this.data.BidDetail.isResultQualified,

        Profile_Experience: this.data.BidDetail.Profile_Experience,
        OfferValidity: this.data.BidDetail.OfferValidity,
        TOR_SOWAcceptance: this.data.BidDetail.TOR_SOWAcceptance,
        Securities_BankGuarantee: this.data.BidDetail.Securities_BankGuarantee,
        OfferDocumentation: this.data.BidDetail.OfferDocumentation,

        Company_GoodsSpecification: this.data.BidDetail.Company_GoodsSpecification,
        Service_Warranty: this.data.BidDetail.Service_Warranty,
        Certification_GMP_COPP: this.data.BidDetail.Certification_GMP_COPP,
        WorkExperience: this.data.BidDetail.WorkExperience,
        DeliveryDateScore: this.data.BidDetail.DeliveryDateScore
    });
  }
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
    if (this.attachment.length === 0 && this.data.BidDetail === undefined) {
      this.toastr.warning('Please attach Contract/Guarantee Letter!');
      return;
    }
    if (this.data.BidDetail === undefined) {
      this.tenderBidForm.controls.OpeningDate.setValue(StaticUtilities.getLocalDate(this.tenderBidForm.get('OpeningDate').value));
      this.tenderBidForm.controls.TenderDeliveryDate.setValue
      (StaticUtilities.getLocalDate(this.tenderBidForm.get('TenderDeliveryDate').value));
      this.commonLoader.showLoader();
      this.logisticservice.addTenderBid(this.tenderBidForm.value).subscribe(res => {
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
    } else {
      this.commonLoader.showLoader();
      this.tenderBidForm.controls.OpeningDate.setValue(StaticUtilities.getLocalDate(this.tenderBidForm.get('OpeningDate').value));
      this.tenderBidForm.controls.TenderDeliveryDate.setValue
      (StaticUtilities.getLocalDate(this.tenderBidForm.get('TenderDeliveryDate').value));
      const model = this.tenderBidForm.value;
      model.isContractLetterUpdated = (this.attachment.length === 0) ? false : true;
      model.BidId = this.data.BidDetail.BidId;
      this.logisticservice.editTenderBid(model).subscribe(res => {
        if (res.StatusCode === 200 && res.CommonId.LongId != null) {
          if (this.attachment.length !== 0) {
            this.globalSharedService
              .uploadFile(FileSourceEntityTypes.TenderBidContractLetter, res.CommonId.LongId, this.attachment[0][0])
              .pipe(takeUntil(this.destroyed$))
              .subscribe(y => {
                  this.commonLoader.hideLoader();
                  this.toastr.success('Bid Updated Successfully!');
                  this.dialogRef.close({data: 'Success'});
              },
              err => {
                this.commonLoader.hideLoader();
                this.toastr.error('Something went wrong!');
              });
          } else {
              this.commonLoader.hideLoader();
              this.toastr.success('Bid Updated Successfully!');
              this.dialogRef.close({data: 'Success'});
          }
        }
      });
    }

  }

  emailValidator(data): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      if (data.BidDetail === undefined && data.ExistingBids !== undefined && data.ExistingBids.some(x => x.Email === control.value)) {
        return { 'emailError': true };
      }
      if (data.BidDetail !== undefined && data.ExistingBids !== undefined &&
        data.ExistingBids.some(x => x.Email === control.value && x.BidId !== data.BidDetail.BidId)) {
        return { 'emailError': true };
      }
      return null;
    };
  }

  phoneValidator(data): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      if (data.BidDetail === undefined && data.ExistingBids !== undefined && data.ExistingBids.some(x => x.PhoneNumber === control.value)) {
        return { 'phoneError': true };
      }
      if (data.BidDetail !== undefined && data.ExistingBids !== undefined &&
        data.ExistingBids.some(x => x.PhoneNumber === control.value && x.BidId !== data.BidDetail.BidId)) {
        return { 'phoneError': true };
      }
      return null;
    };
  }
}
