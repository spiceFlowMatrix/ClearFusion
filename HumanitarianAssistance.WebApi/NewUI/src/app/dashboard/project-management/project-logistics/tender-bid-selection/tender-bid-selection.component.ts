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
        Name: element.Name
      });
    });
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
