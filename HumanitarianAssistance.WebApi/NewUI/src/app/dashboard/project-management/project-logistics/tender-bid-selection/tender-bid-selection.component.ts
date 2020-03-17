import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { ToastrService } from 'ngx-toastr';
import { LogisticService } from '../logistic.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-tender-bid-selection',
  templateUrl: './tender-bid-selection.component.html',
  styleUrls: ['./tender-bid-selection.component.scss']
})
export class TenderBidSelectionComponent implements OnInit {

  bidDropdown = [];
  bidSelectionForm: FormGroup;
  max = {TotalScore: 0 , BidId: 0};
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private fb: FormBuilder,
  private dialogRef: MatDialogRef<TenderBidSelectionComponent>,
  private toastr: ToastrService, private logisticservice: LogisticService, private commonLoader: CommonLoaderService) { }

  ngOnInit() {
    this.bidSelectionForm = this.fb.group({
      SelectedBid: ['', Validators.required]
    });
    this.data.BidDetail.forEach(element => {
      this.bidDropdown.push({
        Id: element.BidId,
        Name: element.Name + '-' + (element.Profile_Experience + element.OfferValidity +
          element.TOR_SOWAcceptance + element.Securities_BankGuarantee +
          element.OfferDocumentation +
          element.Company_GoodsSpecification + element.Service_Warranty +
          element.Certification_GMP_COPP + element.WorkExperience +
          element.DeliveryDateScore)
      });
    });

    this.max = {TotalScore: 0 , BidId: 0};
    this.data.BidDetail.forEach(element => {
      if ((element.Profile_Experience + element.OfferValidity +
        element.TOR_SOWAcceptance + element.Securities_BankGuarantee +
        element.OfferDocumentation +
        element.Company_GoodsSpecification + element.Service_Warranty +
        element.Certification_GMP_COPP + element.WorkExperience +
        element.DeliveryDateScore) > this.max.TotalScore) {
        this.max.TotalScore = (element.Profile_Experience + element.OfferValidity +
          element.TOR_SOWAcceptance + element.Securities_BankGuarantee +
          element.OfferDocumentation +
          element.Company_GoodsSpecification + element.Service_Warranty +
          element.Certification_GMP_COPP + element.WorkExperience +
          element.DeliveryDateScore);
        this.max.BidId = element.BidId;
      }
    });

    if (this.max.BidId !== 0) {
      this.bidSelectionForm.controls['SelectedBid'].setValue(this.max.BidId);
    }
  }

  get SelectedBid() {
    return this.bidSelectionForm.get('SelectedBid').value;
  }

  onOpenedBidSelectionChange(event: IOpenedChange) {
    this.bidSelectionForm.controls['SelectedBid'].setValue(event.Value);
  }

  selectBid() {
    if (!this.bidSelectionForm.valid) {
      this.toastr.warning('Please provide Bid Selection!');
      return;
    }
    if (this.bidSelectionForm.value.SelectedBid !== this.max.BidId) {
      this.toastr.warning('Please select Bid with highest score!');
      return;
    }
    this.commonLoader.showLoader();
    this.logisticservice.selectTenderBid(this.bidSelectionForm.value.SelectedBid).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.toastr.success('Bid Selection Sucessfull!');
        this.dialogRef.close({data: 'Success'});
      } else {
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  closeDialog() {
    this.dialogRef.close({data: null});
  }
}
