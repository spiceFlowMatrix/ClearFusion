import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { forkJoin, Observable, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { LogisticService } from '../logistic.service';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-purchase-voucher-verification',
  templateUrl: './purchase-voucher-verification.component.html',
  styleUrls: ['./purchase-voucher-verification.component.scss']
})
export class PurchaseVoucherVerificationComponent implements OnInit {

  purchaseDetail: PurchaseDetailModel = {
    PurchasedDate: '',
    Currency: '',
    Office: '',
    Project: '',
    BudgetLine: '',
    ProjectJob: '',
    TotalAmount: 0,
  };
  accountDropdown: any[];
  journalDropdown: any[];
  journalDropdown$: Observable<IDropDownModel[]>;
  purchaseVerificationForm: FormGroup;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  public toastr: ToastrService,
  private commonLoader: CommonLoaderService,
  private logisticservice: LogisticService,
  private fb: FormBuilder) {
   }

  ngOnInit() {
    this.purchaseVerificationForm = this.fb.group({
      Journal: ['', Validators.required],
      VoucherDescription: ['', Validators.required]
    });
    const purchaseResponse = this.getPurchaseDetailResponse();
    const journalResponse = this.getJournalListResponse();
    const accountResponse = this.getAccountResponse();

    this.commonLoader.showLoader();
    forkJoin([purchaseResponse, journalResponse, accountResponse])
    .subscribe(
      results => {
        this.getPurchaseDetailList(results[0]);
        this.getJournalListList(results[1]);
        this.getAccountList(results[2]);
        this.commonLoader.hideLoader();
      },
      error => {
          this.toastr.error('Internal server error');
          // this.commonLoader.hideLoader();
      },
      () => {
        // this.commonLoader.hideLoader();
      }
    );
  }

  getPurchaseDetailResponse() {
    return this.logisticservice.getPurchaseOrderDetail(this.data.RequestId);
  }

  getJournalListResponse() {
    return this.logisticservice.getJournalList();
  }

  getAccountResponse() {
    return this.logisticservice.GetAccountDetails();
  }

  getPurchaseDetailList(result) {
    if (result.StatusCode === 200 && result.data.purchaseOrderDetail != null) {
      this.purchaseDetail = result.data.purchaseOrderDetail;
    }
  }

  getJournalListList(result) {
    if (result.StatusCode === 200 && result.data.JournalDetailList != null) {
      if (result.data.JournalDetailList.length > 0) {
        this.journalDropdown = [];
        result.data.JournalDetailList.forEach(element => {
          this.journalDropdown.push({
            value: element.JournalCode,
            name: element.JournalName
          });
        });
        this.journalDropdown$ = of(this.journalDropdown);
      }
    }
  }

  getAccountList(result) {
    if (result.StatusCode === 200 && result.data.AccountDetailList != null) {
      result.data.AccountDetailList = result.data.AccountDetailList.filter(
        x => x.AccountLevelId === 4
      );
      if (result.data.AccountDetailList.length > 0) {
        this.accountDropdown = [];
        result.data.AccountDetailList.forEach(element => {
          this.accountDropdown.push({
            Id: element.AccountCode,
            Name: element.AccountName
          });
        });
      }
    }
  }


}

export interface PurchaseDetailModel {
  PurchasedDate;
  Currency;
  Office;
  TotalAmount;
  Project;
  BudgetLine;
  ProjectJob;
}
