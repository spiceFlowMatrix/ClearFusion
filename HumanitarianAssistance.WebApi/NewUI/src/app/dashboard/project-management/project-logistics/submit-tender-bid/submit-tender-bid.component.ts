import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-submit-tender-bid',
  templateUrl: './submit-tender-bid.component.html',
  styleUrls: ['./submit-tender-bid.component.scss']
})
export class SubmitTenderBidComponent implements OnInit {

  tenderBidForm: FormGroup;
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.tenderBidForm = this.fb.group({
      Name: ['', Validators.required],
      Email: ['', Validators.required],
      PhoneNumber: ['', Validators.required],
      Address: [''],
      Owner: ['', Validators.required],
      OpeningDate: ['', Validators.required],
      SecurityDate: ['', Validators.required],
      QuotedAmount: ['', Validators.required],
      SecurityAmount: ['', Validators.required],
      isResultQualified: [false, Validators.required],

      Profile_Experience: ['', Validators.required],
      OfferValididty: ['', Validators.required],
      TOR_SOWAcceptance: ['', Validators.required],
      Securities_BankGuarantee: ['', Validators.required],
      OfferDocumentation: [false, Validators.required],

      Company_GoodsSpecification: ['', Validators.required],
      Service_Warranty: ['', Validators.required],
      Certification_GMP_COPP: ['', Validators.required],
      WorkExperience: ['', Validators.required],
      DeliveryDate: [false, Validators.required],
    });
  }

  onResultsQualifiedChange(value) {
    this.tenderBidForm.controls['isResultQualified'].setValue(value.checked);
  }
}
