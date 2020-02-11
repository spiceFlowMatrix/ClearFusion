import { Component, OnInit } from '@angular/core';
import { TransactionType } from 'src/app/shared/enum';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { IResponseData } from '../../models/status-code.model';
import { MatTableDataSource } from '@angular/material';
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

  voucherNo: any;
  voucherDetail: any;
  displayedColumns: string[] = ['select', 'Type', 'AccountCode', 'Description', 'Amount', 'ProjectName', 'BudgetLineName', 'JobName'];
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
  showAddTransaction = false;
  filterdOptionsProjectList: Observable<any[]>;
  filterdOptionsBudgetLineList: Observable<any[]>;
  errorMessage: string;

  constructor(private routeActive: ActivatedRoute,
    private router: Router, private voucherService: VoucherService,
    private toastr: ToastrService, private fb: FormBuilder,
    private budgetLineService: BudgetLineService) {
    this.routeActive.url.subscribe(params => {
      this.voucherNo = +params[0].path;
    });
  }

  ngOnInit() {
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
      TotalDebit: 0
    };
  }

  onInItAddTransactionForm() {
    this.addEditTransactionForm = this.fb.group({
      'AccountId': ['', [Validators.required]],
      'AccountName': [null],
      'Description': [null, [Validators.required]],
      'Amount': [null, [Validators.required]],
      'ProjectId': [''],
      'BudgetLine': [null],
      'JobId': [null],
      'JobName': [''],
      'Type': [null, [Validators.required]],
    });
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
    this.voucherService.GetTransactionByVoucherId(this.voucherNo).subscribe(x => {
      this.ELEMENT_DATA = [];
      if (x.statusCode === 200) {
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
    this.showAddTransaction = true;
  }

  cancelTransactionBtnClicked() {
    this.showAddTransaction = false;
  }

  private filterAccountName(value: string): any[] {
    if (value) {
      const filterValue = value.toLowerCase();
      if (value.length >= 3) {
        this.getFilteredAccountList(filterValue);
        return this.accountList;
      }
    }
  }

  private filterProjectName(value: string): any[] {
    if (value) {
      const filterValue = value.toLowerCase();
      if (value.length >= 3) {
        this.getFilteredProjectList(filterValue);
        return this.projectList;
      }
    }
  }

  private filterBudgetLineName(value: string): any[] {
    if (value) {
      const filterValue = value.toLowerCase();
      if (value.length >= 3) {
        this.getFilteredBudgetLineList(filterValue);
        return this.budgetLineList;
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
      // this.getBudgetLineByProjectId(id);
    }
  }

  onChangeBudgetLineValue(event: any, id: number) {
    if (id !== undefined && id != null) {
      this.selectedBudgetLineId = id;
      this.selectedBudgetLineName = event.source.value;
       this.getProjectJobDetailByBudgetLineId(id);
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

    // const jobIndex = this.jobList.findIndex(x => x.JobId === this.addEditTransactionForm.value.Job);
    const model = {
      AccountNo: this.selectedAccountNo,
      Debit: this.addEditTransactionForm.value.Type === TransactionType.Debit ? this.addEditTransactionForm.value.Amount.toFixed(2) : 0,
      Credit: this.addEditTransactionForm.value.Type === TransactionType.Credit ? this.addEditTransactionForm.value.Amount.toFixed(2) : 0,
      Amount: this.addEditTransactionForm.value.Amount.toFixed(2),
      ProjectId: this.selectedProjectId,
      ProjectName: this.selectedProjectName,
      BudgetLineName: this.selectedBudgetLineName,
      BudgetLineId: this.selectedBudgetLineId,
      Description: this.addEditTransactionForm.value.Description,
      TransactionId: 0,
      VoucherNo: this.voucherNo,
      JobId: this.addEditTransactionForm.value.JobId,
      JobName: this.addEditTransactionForm.value.JobName,
      Type: this.addEditTransactionForm.value.Type === TransactionType.Debit ? 'Debit' : 'Credit',
      AccountCode: this.selectedAccountName
    };
    this.voucherDetail.TotalCredit = this.voucherDetail.TotalCredit === undefined ? 0 : this.voucherDetail.TotalCredit;
    this.voucherDetail.TotalDebit = this.voucherDetail.TotalDebit === undefined ? 0 : this.voucherDetail.TotalDebit;

    if (this.addEditTransactionForm.value.Type === TransactionType.Debit) {
      this.voucherDetail.TotalDebit += parseFloat(this.addEditTransactionForm.value.Amount.toFixed(2));
    } else {
      this.voucherDetail.TotalCredit += parseFloat(this.addEditTransactionForm.value.Amount.toFixed(2));
    }

    this.selectedAccountNo = null;
    this.selectedProjectName = null;
    this.selectedProjectId = null;
    this.selectedProjectName = null;
    this.showAddTransaction = true;
    this.addEditTransactionForm.reset();
    this.setAutoComplete();
    this.showAddTransaction = false;

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

    if (creditsTotal.length === 0 || debitsTotal.length === 0) {
      this.errorMessage = 'Credits and Debits must be equal';
      this.isFormSubmitted = false;
      return;
    }

    if (creditsTotal.map(item => item.Credit).reduce((prev, next) => prev + next) !==
      debitsTotal.map(item => item.Debit).reduce((prev, next) => prev + next)) {
      this.errorMessage = 'Credits and Debits must be equal';
      this.isFormSubmitted = false;
      return;
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
        this.errorMessage ='Something went wrong. Please try again';
        this.isFormSubmitted = false;
      }
    }, error => {
      this.errorMessage = error;
      this.isFormSubmitted = false;
    });
  }
}

export interface ITransaction {
  VoucherNo: any;
  TransactionList: any[];
}
