import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VoucherService } from '../../voucher.service';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-voucher-detail',
  templateUrl: './voucher-detail.component.html',
  styleUrls: ['./voucher-detail.component.scss']
})



export class VoucherDetailComponent implements OnInit {

  voucherNo: any;
  voucherDetail: any;
  displayedColumns: string[] = ['Type', 'AccountCode', 'Description', 'Amount', 'ProjectName', 'BudgetLineName', 'JobName'];
  ELEMENT_DATA: any[] = [];
  isModifyTransactions = false;
  transactionDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
  selection = new SelectionModel<any>(true, []);
  addEditTransaction = false;

  constructor(private routeActive: ActivatedRoute,
    private router: Router, private voucherService: VoucherService,
    private toastr: ToastrService) {
    this.routeActive.url.subscribe(params => {
      this.voucherNo = +params[0].path;
    });
    this.onFormInIt();
  }

  ngOnInit() {
    this.getDetailsByVoucherNo();
    this.getVoucherTransactionsByVoucherNo();
  }

  onFormInIt() {
    this.voucherDetail = {
      VoucherNo: null,
      CurrencyCode: null,
      CurrencyId: null,
      VoucherDate: null,
      ChequeNo: null,
      ReferenceNo: null,
      Description: null,
      JournalName: null,
      JournalCode: null,
      VoucherTypeId: null,
      VoucherTypeName: null,
      OfficeId: null,
      ProjectId: null,
      BudgetLineId: null,
      OfficeName: null,
      FinancialYearId: null,
      FinancialYearName: null,
      IsVoucherVerified: null,
      IsExchangeGainLos: null,
    };
  }

  getDetailsByVoucherNo() {
    this.voucherService.GetVoucherDetailById(this.voucherNo).subscribe(x => {
      this.voucherDetail = x.data;
    });
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.transactionDataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.transactionDataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  onModifyTransactionsClick() {
    this.isModifyTransactions = true;
    this.displayedColumns = ['select', 'Type', 'AccountCode', 'Description', 'Amount', 'ProjectName', 'BudgetLineName', 'JobName'];
  }

  getVoucherTransactionsByVoucherNo() {
    this.voucherService.GetTransactionByVoucherId(this.voucherNo).subscribe(x => {
      debugger;
      this.ELEMENT_DATA = [];
      if (x.statusCode === 200) {
        debugger;
        this.ELEMENT_DATA = x.data;
        this.transactionDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
        this.selection.clear();
      }
    }, error => {
      this.selection.clear();
      this.toastr.warning(error);
    });
  }

  onAddTransactionBtnClick() {
    this.addEditTransaction = true;
  }
}
