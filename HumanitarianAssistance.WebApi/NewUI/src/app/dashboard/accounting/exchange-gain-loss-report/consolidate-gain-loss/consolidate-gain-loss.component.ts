import { Component, OnInit, Output, EventEmitter, Input, OnChanges } from '@angular/core';
import { of, Observable, forkJoin, ReplaySubject } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ExchangeGainLossReportService } from '../exchange-gain-loss-report.service';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from '../../vouchers/models/status-code.model';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-consolidate-gain-loss',
  templateUrl: './consolidate-gain-loss.component.html',
  styleUrls: ['./consolidate-gain-loss.component.scss']
})
export class ConsolidateGainLossComponent implements OnInit, OnChanges {

  @Output() emitType = new EventEmitter();
  @Input() selectedData: any[];
  gainList: any[] = [];
  lossList: any[] = [];
  totals: number;
  tabIndex = 0;
  accountIds: number[] = [];
  @Input() calculatorConfigData: any;
  transactionList$: Observable<any[]>;
  voucherDataForm: FormGroup;
  journalList$: Observable<IDropDownModel[]>;
  voucherTypeList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  hideColums = of({
    headers: ['Account', 'Credit Amount', 'Debit Amount', 'Description'],
    items: ['Account', 'CreditAmount', 'DebitAmount', 'Description']
  });
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  transactionHeaders$ = of(['Account', 'Credit Amount', 'Debit Amount', 'Description']);
  constructor(private fb: FormBuilder, private gainLossReportService: ExchangeGainLossReportService,
    private toastr: ToastrService, private commonLoader: CommonLoaderService) { }

  ngOnChanges() {
    this.gainList = this.selectedData.filter(x => x.GainLossStatus === 1);
    this.lossList = this.selectedData.filter(x => x.GainLossStatus === -1);
    this.getTotalGain();
    this.selectedData.forEach(x => {
      this.accountIds.push(x.AccountId);
    });
  }

  ngOnInit() {
    this.inItForm();

    forkJoin([
      this.getJournalList(),
      this.getVoucherTypeList(),
      this.getOfficeList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeJournalList(result[0]);
        this.subscribeVoucherTypeList(result[1]);
        this.subscribeOfficeList(result[2]);
      });
  }

  inItForm() {
    this.voucherDataForm = this.fb.group({
      'JournalId': [null, [Validators.required]],
      'VoucherType': [null, Validators.required],
      'Description': [null],
      'OfficeId': [null, [Validators.required]]
    });
  }

  tabChanged(value) {
    this.tabIndex = value.index;
    if (value.index === 0) {
      this.getTotalGain();
    } else if (value.index === 1) {
      this.getTotalLoss();
    }
  }

  cancel() {
    this.emitType.emit('');
  }

  //#region "getJournalList"
  getJournalList() {
    return this.gainLossReportService.GetJournalList();
  }
  //#endregion

  subscribeJournalList(response) {
    if (response.statusCode === 200 && response.data !== null) {

      this.journalList$ = of(response.data.map(y => {
        return {
          name: y.JournalName,
          value: y.JournalCode
        };
      }));
    }
  }

  //#region "getVoucherTypeList"
  getVoucherTypeList() {
    return this.gainLossReportService.GetVoucherTypeList();
  }
  //#endregion

  subscribeVoucherTypeList(response) {
    console.log(response.data, 'vouchertype');
    if (response.statusCode === 200 && response.data !== null) {
      this.voucherTypeList$ = of(response.data.map(y => {
        return {
          name: y.VoucherTypeName,
          value: y.VoucherTypeId
        };
      }));
    }
  }

  //#region "getOfficeList"
  getOfficeList() {
    return this.gainLossReportService.GetOfficeList();
  }
  //#endregion

  subscribeOfficeList(response) {
    console.log(response.data, 'office');
    if (response.statusCode === 200 && response.data !== null) {
      this.officeList$ = of(response.data.map(y => {
        return {
          name: y.OfficeName,
          value: y.OfficeId
        };
      }));
    }
  }

  onCommitConsolidate() {
    if (!this.voucherDataForm.valid) {
      this.toastr.warning('Please correct errors on voucher form and submit again');
      return;
    }

    if (!this.calculatorConfigData.DebitAccount && !this.calculatorConfigData.CreditAmount) {
      this.toastr.warning('Please select Debit and Credit account on calculator config.');
      return;
    }

    this.commonLoader.showLoader();
    let transactionlist: any[] = [];
    this.transactionList$.subscribe(x => transactionlist = x);

    transactionlist.splice((transactionlist.length - 1), 1);

    const model = {
      CurrencyId: this.calculatorConfigData.CurrencyId,
      JournalId: this.voucherDataForm.value.JournalId,
      VoucherType: this.voucherDataForm.value.VoucherType,
      OfficeId: this.voucherDataForm.value.OfficeId,
      TimeZoneOffset: new Date().getTimezoneOffset(),
      Description: this.voucherDataForm.value.Description,
      VoucherDate: StaticUtilities.setLocalDate(new Date()),
      TransactionList: transactionlist,
      StartDate: this.calculatorConfigData.StartDate,
      EndDate: this.calculatorConfigData.EndDate
    };

    this.gainLossReportService.AddGainLossVoucher(model).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.commonLoader.hideLoader();
          this.toastr.success('Account Consolidated');
          this.cancel();

          // this.toastr.success('Voucher Created Successfully');
        } else {
          this.commonLoader.hideLoader();
          this.toastr.error(response.message);
        }
        this.commonLoader.hideLoader();
      },
      error => {
        this.commonLoader.hideLoader();
        this.toastr.error(error);
      }
    );
  }

  getTotalGain() {
    this.gainList = [];
    this.gainList = this.selectedData.filter(x => x.GainLossStatus === 1);
    this.totals = this.gainList.reduce(
      (a, { ResultingGainLoss }) => a + ResultingGainLoss,
      0
    );

    if (this.gainList.length > 0) {
      this.gainList.push({
        AccountId: this.calculatorConfigData.CreditAccount,
        AccountCode: '',
        AccountName: this.calculatorConfigData.CreditAccountName,
        BalanceOnOriginalTransactionDates: 0,
        BalanceOnComparisionDate: 0,
        ResultingGainLoss: this.totals,
        GainLossStatus: 0
      });

      this.gainList.push({
        AccountId: 0,
        AccountCode: '0',
        AccountName: '<b>Totals</b>',
        BalanceOnOriginalTransactionDates: this.totals,
        BalanceOnComparisionDate: this.totals,
        ResultingGainLoss: this.totals,
        GainLossStatus: 0
      });
    }

    this.transactionList$ = of(this.gainList.map(x => {
      return {
        AccountId: x.AccountId,
        Account: x.GainLossStatus !== 0 ? (x.AccountCode + '-' + x.AccountName) : x.AccountName,
        CreditAmount: (x.GainLossStatus === 0 || x.AccountId === 0) ? x.ResultingGainLoss : 0,
        DebitAmount: (x.GainLossStatus !== 0 || x.AccountId === 0) ? x.ResultingGainLoss : 0,
        Description: x.AccountId === 0 ? '' : 'Gain'
      };
    }));
  }

  getTotalLoss() {
    this.totals = this.lossList.reduce(
      (a, { ResultingGainLoss }) => a + ResultingGainLoss,
      0
    );

    this.lossList = [];
    this.lossList = this.selectedData.filter(x => x.GainLossStatus === -1);
    this.totals = this.lossList.reduce(
      (a, { ResultingGainLoss }) => a + ResultingGainLoss,
      0
    );

    if (this.lossList.length > 0) {
      this.lossList.push({
        AccountId: this.calculatorConfigData.DebitAccount,
        AccountCode: '',
        AccountName: this.calculatorConfigData.DebitAccountName,
        BalanceOnOriginalTransactionDates: 0,
        BalanceOnComparisionDate: 0,
        ResultingGainLoss: this.totals,
        GainLossStatus: 0
      });
      this.lossList.push({
        AccountId: 0,
        AccountCode: '0',
        AccountName: '<b>Totals</b>',
        BalanceOnOriginalTransactionDates: this.totals,
        BalanceOnComparisionDate: this.totals,
        ResultingGainLoss: this.totals,
        GainLossStatus: 0
      });
    }

    this.transactionList$ = of(this.lossList.map(x => {
      return {
        AccountId: x.AccountId,
        Account: x.GainLossStatus !== 0 ? (x.AccountCode + '-' + x.AccountName) : x.AccountName,
        CreditAmount: (x.GainLossStatus !== 0 || x.AccountId === 0) ? x.ResultingGainLoss : 0,
        DebitAmount: (x.GainLossStatus === 0 || x.AccountId === 0) ? x.ResultingGainLoss : 0,
        Description: x.AccountId === 0 ? '' : 'Loss'
      };
    }));
  }
}
