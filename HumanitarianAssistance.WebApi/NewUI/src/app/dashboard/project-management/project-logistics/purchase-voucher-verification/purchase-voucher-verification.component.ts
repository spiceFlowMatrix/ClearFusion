import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { forkJoin, Observable, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { LogisticService } from '../logistic.service';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

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
    TotalCost: 0,
    OfficeId: ''
  };
  accountDropdown: any[];
  journalDropdown: any[];
  journalDropdown$: Observable<IDropDownModel[]>;
  purchaseVerificationForm: FormGroup;
  exchangeRateMessage = '';

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  public toastr: ToastrService,
  private commonLoader: CommonLoaderService,
  private logisticservice: LogisticService,
  private fb: FormBuilder,
  private dialogRef: MatDialogRef<PurchaseVoucherVerificationComponent>,
  private datePipe: DatePipe,
  private routeActive: ActivatedRoute,
  private router: Router) {
   }

  ngOnInit() {
    this.purchaseVerificationForm = this.fb.group({
      Journal: ['', Validators.required],
      VoucherDescription: ['', Validators.required],
      CreditAccount: ['', Validators.required],
      DebitAccount: ['', Validators.required],
      DebitDescription: ['', Validators.required],
      CreditDescription: ['', Validators.required]
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
      const checkExchangeRateModel = {
        ExchangeRateDate: this.purchaseDetail.PurchasedDate,
        OfficeId: this.purchaseDetail.OfficeId
      };
      this.logisticservice.checkExchangeRateExists(checkExchangeRateModel).subscribe(data => {
        if (data.StatusCode === 200) {

          if (!data.ResponseData) {
            this.exchangeRateMessage = 'No Exchange Rate Defined for ' +
              this.datePipe.transform(checkExchangeRateModel.ExchangeRateDate, 'dd-MM-yyyy');
          } else {
            this.exchangeRateMessage = '';
          }
        } else {
          this.toastr.error(data.Message);
        }
      },
      error => {
      }
    );
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

  get CreditAccountId() {
    return this.purchaseVerificationForm.get('CreditAccount').value;
  }
  get DebitAccountId() {
    return this.purchaseVerificationForm.get('DebitAccount').value;
  }

  onOpenedCreditAccountChange(event: IOpenedChange) {
    this.purchaseVerificationForm.controls['CreditAccount'].setValue(event.Value);
  }

  onOpenedDebitAccountChange(event: IOpenedChange) {
    this.purchaseVerificationForm.controls['DebitAccount'].setValue(event.Value);
  }

  closeDialog() {
    this.dialogRef.close();
  }

  navigateToExchangeRate() {
    this.dialogRef.close();
    this.router.navigate(['/accounting/exchange-rate']);
  }

  submitPurchaseVerification(value) {
    if (!this.purchaseVerificationForm.valid) {
      this.toastr.warning('Please fill required fields!');
      return;
    }
    if (this.exchangeRateMessage !== '' && this.exchangeRateMessage !== null) {
      this.toastr.warning('Exchange Rate not defined!');
      return;
    }
    this.commonLoader.showLoader();
    const model = {
      RequestId: this.data.RequestId,
      Journal: value.Journal,
      VoucherDescription: value.VoucherDescription,
      CreditAccount: value.CreditAccount,
      CreditDescription: value.CreditDescription,
      DebitAccount: value.DebitAccount,
      DebitDescription: value.DebitDescription,
      TotalCost: this.purchaseDetail.TotalCost
    };
    this.logisticservice.verifyPurchaseOrder(model).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.dialogRef.close({data: 'Success'});
        this.toastr.success('Updated successfully!');
      } else {
        this.commonLoader.hideLoader();
        this.dialogRef.close({data: null});
        this.toastr.warning(res.Message);
      }
    });
  }
}

export interface PurchaseDetailModel {
  PurchasedDate;
  Currency;
  Office;
  OfficeId;
  TotalCost;
  Project;
  BudgetLine;
  ProjectJob;
}
