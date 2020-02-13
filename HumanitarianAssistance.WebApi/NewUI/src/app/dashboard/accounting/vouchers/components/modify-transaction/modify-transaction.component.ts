import { Component, OnInit, ViewChild } from '@angular/core';
import { TransactionType, VoucherType } from 'src/app/shared/enum';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { IResponseData } from '../../models/status-code.model';
import { MatTableDataSource } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, Router } from '@angular/router';
import { VoucherService } from '../../voucher.service';
import { ToastrService } from 'ngx-toastr';
import { BudgetLineService } from 'src/app/dashboard/project-management/project-list/budgetlines/budget-line.service';
import { startWith, map } from 'rxjs/operators';
import { of } from 'rxjs/internal/observable/of';

@Component({
  selector: 'app-modify-transaction',
  templateUrl: './modify-transaction.component.html',
  styleUrls: ['./modify-transaction.component.scss']
})
export class ModifyTransactionComponent implements OnInit {

  constructor(private routeActive: ActivatedRoute,
    private router: Router, private voucherService: VoucherService,
    private toastr: ToastrService, private fb: FormBuilder) {
    this.routeActive.url.subscribe(params => {
      this.voucherNo = +params[0].path;
    });
  }

  transactionPagingModel: any;

  voucherNo: any;
  voucherDetail: any;
  displayedColumns: string[] = ['select', 'AccountCode', 'Description', 'CreditAmount', 'DebitAmount', 'ProjectName', 'BudgetLineName', 'JobName'];
  ELEMENT_DATA: any[] = [];
  isModifyTransactions = false;
  transactionDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
  selection = new SelectionModel<any>(true, []);
  addEditTransaction = false;
  addEditTransactionForm: FormGroup;
  filterdOptionsAccountList: Observable<any[]>;
  accountList: any[] = [];
  projectList: any[] = [];
  jobList: any[] = [];
  budgetLineList: any[] = [];
  typeList: any[] = [];
  selectedAccountNo: any;
  selectedAccountName: any;
  selectedProjectId: any;
  selectedBudgetLineId: any;
  selectedProjectName: any;
  selectedBudgetLineName: any;
  isFormSubmitted = false;
  showAddTransactionTop = false;
  showAddTransactionBottom = false;
  filterdOptionsProjectList: Observable<any[]>;
  filterdOptionsBudgetLineList: Observable<any[]>;
  errorMessage: string;
  recordCount = 0;
  VoucherTypeEnum = VoucherType;

  ngOnInit() {
    this.transactionPagingModel = {
      PageSize: 10,
      PageIndex: 0,
      VoucherNo: this.voucherNo
    };

    this.onFormInIt();
    this.getDetailsByVoucherNo();
    this.getVoucherTransactionsByVoucherNo();
    this.setAutoComplete();

  }

  setAutoComplete() {

    this.filterdOptionsAccountList = this.addEditTransactionForm.controls[
      'AccountId'
    ].valueChanges.pipe(
      startWith<string | any>(''),
      map(value => this.filterAccountName(value))
    );
    this.filterdOptionsProjectList = this.addEditTransactionForm.controls[
      'ProjectId'
    ].valueChanges.pipe(
      startWith<string | any>(''),
      map(value => this.filterProjectName(value))
    );
    this.filterdOptionsBudgetLineList = this.addEditTransactionForm.controls[
      'BudgetLine'
    ].valueChanges.pipe(
      startWith<string | any>(''),
      map(value => this.filterBudgetLineName(value))
    );
  }
  onFormInIt() {
    this.onInItAddTransactionForm();
    this.typeList = [{ name: 'Credit', value: 1 }, { name: 'Debit', value: 2 }];
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
      TotalCredit: 0,
      TotalDebit: 0,
      PurchaseOrderModel: {
        ProjectId: null,
        Code: null,
        PurchaseOrderId: null,
        Description: null,
        ApprovedBy: null,
        ApprovedOn: null
      }
    };
  }

  onInItAddTransactionForm() {
    this.addEditTransactionForm = this.fb.group({
      'AccountId': ['', [Validators.required]],
      'AccountName': [null],
      'Description': [null, [Validators.required]],
      'Credit': [0.00],
      'Debit': [0.00],
      'ProjectId': [''],
      'BudgetLine': [null],
      'JobId': [null],
      'JobName': [''],
      'Type': [null],
    });
    this.addEditTransactionForm.controls['BudgetLine'].disable();
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
  }

  getVoucherTransactionsByVoucherNo() {
    this.voucherService.GetTransactionByVoucherId(this.transactionPagingModel).subscribe(x => {
      this.ELEMENT_DATA = [];
      if (x.statusCode === 200) {
        this.ELEMENT_DATA = x.data;
        this.recordCount = x.total;
        this.transactionDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
        this.selection.clear();
      }
    }, error => {
      this.selection.clear();
      this.toastr.warning(error);
    });
  }

  onAddTransactionTopBtnClick() {
   // this.onInItAddTransactionForm();
   this.addEditTransactionForm.get('Credit').setValue(0);
   this.addEditTransactionForm.get('Debit').setValue(0);
   this.addEditTransactionForm.get('Debit').enable();
   this.addEditTransactionForm.get('Credit').enable();
    this.showAddTransactionTop = true;
  }

  cancelTransactionTopBtnClicked() {
    this.showAddTransactionTop = false;
  }

  onAddTransactionBottomBtnClick() {
    // this.onInItAddTransactionForm();
    this.addEditTransactionForm.get('Credit').setValue(0);
    this.addEditTransactionForm.get('Debit').setValue(0);
    this.addEditTransactionForm.get('Debit').enable();
    this.addEditTransactionForm.get('Credit').enable();
     this.showAddTransactionBottom = true;
   }

   cancelTransactionBottomBtnClicked() {
     this.showAddTransactionBottom = false;
   }

  private filterAccountName(value: string): any[] {
    if (value) {
      const filterValue = value.toLowerCase();
      if (value.length > 2) {
        this.getFilteredAccountList(filterValue);
        return this.accountList;
      } else {
        this.accountList = [];
      }
    }
  }

  private filterProjectName(value: string): any[] {
    if (value) {
      const filterValue = value.toLowerCase();
      if (value.length > 2) {
        this.getFilteredProjectList(filterValue);
        return this.projectList;
      } else {
        this.addEditTransactionForm.controls['BudgetLine'].disable();
        this.projectList = [];
      }
    }
  }

  private filterBudgetLineName(value: string): any[] {
    if (value) {
      const filterValue = value.toLowerCase();
      if (value.length >= 3) {
        this.getFilteredBudgetLineList(filterValue);
        return this.budgetLineList;
      } else {
        this.budgetLineList = [];
      }
    }
  }

  getFilteredAccountList(data: string) {
    if (data !== undefined && data != null) {
      this.voucherService
        .GetFilteredInputLevelAccountList(data)
        .subscribe(resp => {
          this.accountList = [];
          if (resp !== undefined && resp.AccountList.length > 0) {
            resp.AccountList.forEach(element => {
              this.accountList.push({
                AccountId: element.ChartOfAccountNewId,
                AccountName: element.ChartOfAccountNewCode + '-' + element.AccountName
              });
            });
          }
        });
    }
  }

  getFilteredProjectList(data: string) {
    if (data !== undefined && data != null) {
      this.voucherService
        .GetFilteredProjectList(data)
        .subscribe(resp => {
          this.projectList = [];
          if (resp !== undefined && resp.projectList.length > 0) {
            resp.projectList.forEach(element => {
              this.projectList.push({
                ProjectId: element.ProjectId,
                ProjectName: element.ProjectCode + '-' + element.ProjectName
              });
            });
          }
        });
    }
  }

  getFilteredBudgetLineList(data: string) {
    const model = {
      ProjectId: this.selectedProjectId,
      FilterValue: data
    }
    if (data !== undefined && data != null) {
      this.voucherService
        .getFilteredBudgetLineList(model)
        .subscribe(resp => {
          this.budgetLineList = [];
          if (resp !== undefined && resp.budgetLineList.length > 0) {
            resp.budgetLineList.forEach(element => {
              this.budgetLineList.push({
                BudgetLineId: element.BudgetLineId,
                BudgetLineName: element.BudgetLineCode + '-' + element.BudgetLineName
              });
            });
          }
        });
    }
  }

  onChangeAccountValue(event: any, id: number) {
    if (id !== undefined && id != null) {
      // this.addEditTransactionForm.controls['AccountId'].setValue(id);
      this.selectedAccountNo = id;
      this.selectedAccountName = event.source.value;
    }
  }

  onChangeProjectValue(event: any, id: number) {
    if (id !== undefined && id != null) {
      this.selectedProjectId = id;
      this.selectedProjectName = event.source.value;
      this.addEditTransactionForm.controls['BudgetLine'].enable();
    } else {
      this.addEditTransactionForm.controls['BudgetLine'].disable();
    }
  }

  onChangeBudgetLineValue(event: any, id: number) {
    if (id !== undefined && id != null) {
      this.selectedBudgetLineId = id;
      this.selectedBudgetLineName = event.source.value;
      this.getProjectJobDetailByBudgetLineId(id);
    } else {

    }
  }

  getProjectJobDetailByBudgetLineId(id) {
    this.voucherService.getProjectJobDetailByBudgetLineId(id).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.addEditTransactionForm.controls['JobName'].setValue(response.data.ProjectJobCode + '-' +
            response.data.ProjectJobName);
          this.addEditTransactionForm.controls['JobId'].setValue(response.data.ProjectJobId);
        } else if (response.statusCode === 400) {
          this.toastr.warning(response.message);
        }
      },
      error => {
        this.toastr.error(error);
      }
    );
  }

  addTransaction() {

    if (!this.addEditTransactionForm.valid) {
      this.errorMessage = 'Please correct form errors and try again';
      return;
    }
    const creditamount = Number(this.addEditTransactionForm.getRawValue().Credit);
    const debitamount = Number(this.addEditTransactionForm.getRawValue().Debit);
    if (creditamount == 0) {
      this.addEditTransactionForm.controls.Type.setValue(TransactionType.Debit)
    } else {
      this.addEditTransactionForm.controls.Type.setValue(TransactionType.Credit)
    }
    // const jobIndex = this.jobList.findIndex(x => x.JobId === this.addEditTransactionForm.getRawValue().Job);
    const model = {
      AccountNo: this.selectedAccountNo,
      Debit: debitamount.toFixed(2),
      Credit: creditamount.toFixed(2),
      Amount: 0,
      ProjectId: this.selectedProjectId,
      ProjectName: this.selectedProjectName,
      BudgetLineName: this.selectedBudgetLineName,
      BudgetLineId: this.selectedBudgetLineId,
      Description: this.addEditTransactionForm.getRawValue().Description,
      TransactionId: 0,
      VoucherNo: this.voucherNo,
      JobId: this.addEditTransactionForm.getRawValue().JobId,
      JobName: this.addEditTransactionForm.getRawValue().JobName,
      Type: this.addEditTransactionForm.getRawValue().Type === TransactionType.Debit ? 'Debit' : 'Credit',
      AccountCode: this.selectedAccountName
    };
    this.voucherDetail.TotalCredit = this.voucherDetail.TotalCredit === undefined ? 0 : this.voucherDetail.TotalCredit;
    this.voucherDetail.TotalDebit = this.voucherDetail.TotalDebit === undefined ? 0 : this.voucherDetail.TotalDebit;

    if (this.addEditTransactionForm.getRawValue().Type === TransactionType.Debit) {
      this.voucherDetail.TotalDebit += parseFloat(debitamount.toFixed(2));
    } else {
      this.voucherDetail.TotalCredit += parseFloat(creditamount.toFixed(2));
    }

    this.selectedAccountNo = null;
    this.selectedProjectName = null;
    this.selectedProjectId = null;
    this.selectedProjectName = null;
    this.showAddTransactionTop = false;
    this.showAddTransactionBottom = false;
    this.addEditTransactionForm.reset();
    this.setAutoComplete();
    this.ELEMENT_DATA.push(model);
    this.transactionDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
  }

  cancelButtonClicked() {
    this.router.navigate(['../'], { relativeTo: this.routeActive });
  }

  backClick() {
    this.router.navigate(['../'], { relativeTo: this.routeActive });
  }

  deleteTransactions() {
    this.selection.selected.forEach(x => {
      const index = this.ELEMENT_DATA.findIndex(y => y === x);
      if (this.ELEMENT_DATA[index].Type === 'Credit') {
        this.voucherDetail.TotalCredit -= this.ELEMENT_DATA[index].Credit;
      } else {
        this.voucherDetail.TotalDebit -= this.ELEMENT_DATA[index].Debit;
      }
      this.ELEMENT_DATA.splice(index, 1);
    });
    this.transactionDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
    this.selection.clear();
  }

  saveTransactions() {
    this.isFormSubmitted = true;

    const model: ITransaction = {
      VoucherNo: this.voucherNo,
      TransactionList: []
    };

    const creditsTotal = this.ELEMENT_DATA.filter(x => x.Type === 'Credit');
    const debitsTotal = this.ELEMENT_DATA.filter(x => x.Type === 'Debit');

    if (creditsTotal.length === 0 && debitsTotal.length === 0) {
      // this.errorMessage = 'Credits and Debits must be equal';
      // this.isFormSubmitted = false;
      // return;
    } else {
      if (creditsTotal.length === 0 || debitsTotal.length === 0) {
        this.errorMessage = 'Credits and Debits must be equal';
        this.isFormSubmitted = false;
        return;
      }
      else {
        let credit = 0;
        let debit = 0;
        creditsTotal.map(item => item.Credit).reduce((prev, next) => credit = Number(prev) + Number(next));
        debitsTotal.map(item => item.Debit).reduce((prev, next) => debit = Number(prev) + Number(next))
        if (credit !== debit) {
          this.errorMessage = 'Credits and Debits must be equal';
          this.isFormSubmitted = false;
          return;
        }
      }
    }



    this.errorMessage = '';

    // traveler.map(item => item.Amount).reduce((prev, next) => prev + next);

    this.ELEMENT_DATA.forEach(x => {
      let item = {
        AccountNo: x.AccountNo,
        Debit: x.Debit,
        Credit: x.Credit,
        BudgetLineId: x.BudgetLineId,
        ProjectId: x.ProjectId,
        Description: x.Description,
        TransactionId: x.TransactionId,
        VoucherNo: this.voucherNo,
        JobId: x.JobId,
        Type: x.Type === 'Credit' ? TransactionType.Credit : TransactionType.Debit
      };
      model.TransactionList.push(item);
    });
    this.voucherService.saveTransactions(model).subscribe(x => {
      if (x) {
        this.toastr.success('Saved Successfully');
        this.isFormSubmitted = false;
      } else {
        this.errorMessage = 'Something went wrong. Please try again';
        this.isFormSubmitted = false;
      }
    }, error => {
      this.errorMessage = error;
      this.isFormSubmitted = false;
    });
  }

  onAmountEvent(value, type) {
    const val = Number(value);
    //if (val && val !== 0) {
      if (type === 2 && val !== 0) {
        this.addEditTransactionForm.get('Debit').setValue(val.toFixed(2));
        this.addEditTransactionForm.get('Credit').setValue(0);
        this.addEditTransactionForm.get('Credit').disable();
        // this.addEditTransactionForm.get('Credit').setValue(0);
      } else if (type === 2 && val == 0) {
        this.addEditTransactionForm.get('Credit').enable();
      } else if(type === 1 && val !== 0) {
        this.addEditTransactionForm.get('Credit').setValue(val.toFixed(2));
        this.addEditTransactionForm.get('Debit').setValue(0);
        this.addEditTransactionForm.get('Debit').disable();
      } else if(type === 1 && val == 0) {
        this.addEditTransactionForm.get('Debit').enable();
      }
    //}

  }
  checkAmount(val) {
    if (val == 1) {
      // this.addEditTransactionForm.get('Credit').enable();
      this.addEditTransactionForm.get('Debit').setValue(0);
     //  this.addEditTransactionForm.get('Debit').disable();
    } else {
      // this.addEditTransactionForm.get('Debit').enable();
      this.addEditTransactionForm.get('Credit').setValue(0);
     // this.addEditTransactionForm.get('Credit').disable();
    }
  }
  pagination(event) {
    this.transactionPagingModel.PageIndex = event.pageIndex;
    this.transactionPagingModel.PageSize = event.pageSize;
    this.getVoucherTransactionsByVoucherNo();
  }

  navigateToLogisticRequest() {
    this.router.navigate(['project/my-project/' + this.voucherDetail.PurchaseOrderModel.ProjectId +
      '/logistic-requests/' + this.voucherDetail.PurchaseOrderModel.PurchaseOrderId]);
  }
  addNewCreditDebit() {
    //  this.showAddTransaction = false;
    this.addTransaction();
  }

  enableDisableBudgetLine(value) {
    if (value == null || value == undefined || value== '' ) {
      this.addEditTransactionForm.controls['BudgetLine'].setValue('');
      this.addEditTransactionForm.controls['BudgetLine'].disable();
    }
  }

}

export interface ITransaction {
  VoucherNo: any;
  TransactionList: any[];
}
