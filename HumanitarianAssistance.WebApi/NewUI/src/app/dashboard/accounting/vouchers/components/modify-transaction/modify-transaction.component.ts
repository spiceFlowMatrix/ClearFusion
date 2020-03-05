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
  displayedColumns: string[] = ['select', 'AccountCode', 'Description', 'DebitAmount', 'CreditAmount',
    'ProjectName', 'BudgetLineName', 'JobName', 'Edit'];
  ELEMENT_DATA: any[] = [];
  isModifyTransactions = false;
  transactionDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
  selection = new SelectionModel<any>(true, []);
  addEditTransaction = false;
  addEditTransactionForm: FormGroup;
  filterdOptionsAccountList: Observable<any[]>;
  accountspinner = false;
  projectspinner = false;
  budgetlinespinner = false;
  accountList: any[] = [];
  defaultAccountList: any[] = [];
  defaultProjectList: any[] = [];
  defaultBudgetLineList: any[] = [];
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
    this.getDefaultAccountList();
    this.getDefaultProjectList();
    // this.setAutoComplete();

  }

  setAutoComplete() {

    // this.addEditTransactionForm.controls[
    //   'AccountId'
    // ].valueChanges.pipe(
    //   startWith<string | any>(''),
    //   map(value => this.filterAccountName(value))
    // );
    // this.filterdOptionsProjectList = this.addEditTransactionForm.controls[
    //   'ProjectId'
    // ].valueChanges.pipe(
    //   startWith<string | any>(''),
    //   map(value => this.filterProjectName(value))
    // );
    // this.filterdOptionsBudgetLineList = this.addEditTransactionForm.controls[
    //   'BudgetLine'
    // ].valueChanges.pipe(
    //   startWith<string | any>(''),
    //   map(value => this.filterBudgetLineName(value))
    // );
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
      'TransactionId': [null],
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
    this.selectedAccountNo = null;
    this.selectedProjectName = null;
    this.selectedProjectId = null;
    this.selectedProjectName = null;
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
    this.onInItAddTransactionForm();
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
    this.onInItAddTransactionForm();
    this.addEditTransactionForm.get('Credit').setValue(0);
    this.addEditTransactionForm.get('Debit').setValue(0);
    this.addEditTransactionForm.get('Debit').enable();
    this.addEditTransactionForm.get('Credit').enable();
    this.showAddTransactionBottom = true;
  }

  cancelTransactionBottomBtnClicked() {
    this.showAddTransactionBottom = false;
  }

  private filterAccountName(event) {
    const filterValue = event.target.value.toLowerCase();
    if (filterValue || filterValue !== '') {
      if (filterValue.length >= 2) {
        this.accountspinner = true;
        this.accountList = [];
        this.filterdOptionsAccountList = of(this.accountList);
        this.voucherService
          .GetFilteredInputLevelAccountList(filterValue)
          .subscribe(resp => {
            this.accountList = [];
            if (resp !== undefined && resp.AccountList.length > 0) {
              resp.AccountList.forEach(element => {
                this.accountList.push({
                  AccountId: element.ChartOfAccountNewId,
                  AccountName: element.ChartOfAccountNewCode + '-' + element.AccountName
                });
              });
              this.accountspinner = false;
            } else {
              this.accountspinner = false;
            }
            this.filterdOptionsAccountList = of(this.accountList);
            this.filterdOptionsAccountList.subscribe(console.log)
          }, error => {
            console.log(error);
            this.accountspinner = false;
          });
      }
    } else {
      this.filterdOptionsAccountList = of(this.defaultAccountList);
    }
  }

  private getDefaultAccountList() {
    this.accountList = [];
    this.defaultAccountList = [];
    this.voucherService
      .GetFilteredInputLevelAccountList(null)
      .subscribe(resp => {
        this.accountList = [];
        if (resp !== undefined && resp.AccountList.length > 0) {
          resp.AccountList.forEach(element => {
            this.accountList.push({
              AccountId: element.ChartOfAccountNewId,
              AccountName: element.ChartOfAccountNewCode + '-' + element.AccountName
            });
          });
          this.defaultAccountList = this.accountList;
          this.filterdOptionsAccountList = of(this.defaultAccountList);
          this.accountspinner = false;
        } else {
          this.accountspinner = false;
        }
      }, error => {
        console.log(error);
        this.accountspinner = false;
      });
  }

  private getDefaultProjectList() {
    this.projectList = [];
    this.defaultProjectList = [];
    this.projectspinner = true;
    this.voucherService
      .GetFilteredProjectList(null)
      .subscribe(resp => {
        this.projectList = [];
        if (resp !== undefined && resp.projectList.length > 0) {
          resp.projectList.forEach(element => {
            this.projectList.push({
              ProjectId: element.ProjectId,
              ProjectName: element.ProjectCode + '-' + element.ProjectName
            });
          });
          this.defaultProjectList = this.projectList;
          this.filterdOptionsProjectList = of(this.defaultProjectList);
          this.projectspinner = false;
        } else {
          this.projectspinner = false;
        }
      }, error => {
        this.projectspinner = false;
      });
  }

  private getDefaultBudgetLineList() {
    const model = {
      ProjectId: this.selectedProjectId,
      FilterValue: null
    };
    this.budgetlinespinner = true;
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
          this.defaultBudgetLineList = this.budgetLineList;
          this.filterdOptionsBudgetLineList = of(this.defaultBudgetLineList);
          this.budgetlinespinner = false;
        } else {
          this.budgetlinespinner = false;
        }
      }, error => {
        this.budgetlinespinner = false;
      });
  }

  private filterProjectName(event) {
    const filterValue = event.target.value.toLowerCase();
    if (filterValue || filterValue !== '') {
      if (filterValue.length >= 2) {
        // this.getFilteredProjectList(filterValue);
        this.projectspinner = true;
        this.voucherService
          .GetFilteredProjectList(filterValue)
          .subscribe(resp => {
            this.projectList = [];
            this.filterdOptionsProjectList = of(this.projectList);
            if (resp !== undefined && resp.projectList.length > 0) {
              resp.projectList.forEach(element => {
                this.projectList.push({
                  ProjectId: element.ProjectId,
                  ProjectName: element.ProjectCode + '-' + element.ProjectName
                });
              });
              this.filterdOptionsProjectList = of(this.projectList);
              this.projectspinner = false;
            } else {
              this.projectspinner = false;
            }
          }, error => {
            this.projectspinner = false;
          });
      } else {
        this.addEditTransactionForm.controls['BudgetLine'].disable();
        this.projectList = [];
        this.filterdOptionsProjectList = of(this.projectList);
      }
    } else {
      this.filterdOptionsProjectList = of(this.defaultProjectList);
    }
  }

  private filterBudgetLineName(event) {
    const filterValue = event.target.value.toLowerCase();
    this.budgetLineList = [];
    if (filterValue || filterValue !== '') {
      if (filterValue.length >= 2) {
        const model = {
          ProjectId: this.selectedProjectId,
          FilterValue: filterValue
        };
        this.budgetlinespinner = true;
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
              this.filterdOptionsBudgetLineList = of(this.budgetLineList);
              this.budgetlinespinner = false;
            } else {
              this.budgetlinespinner = false;
              this.filterdOptionsBudgetLineList = of(this.budgetLineList);
            }
          }, error => {
            this.budgetlinespinner = false;
          });
      }
    } else {
      this.filterdOptionsBudgetLineList = of(this.defaultBudgetLineList);
    }
  }

  getFilteredAccountList(data: string) {
    this.accountspinner = true;
    this.accountList = [];
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
          this.accountspinner = false;
        } else {
          this.accountspinner = false;
        }
      }, error => {
        this.accountspinner = false;
      });
  }

  getFilteredProjectList(data: string) {
    if (data !== undefined && data != null) {
      this.projectspinner = true;
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
            this.projectspinner = false;
          } else {
            this.projectspinner = false;
          }
        }, error => {
          this.projectspinner = false;
        });
    }
  }

  getFilteredBudgetLineList(data: string) {
    const model = {
      ProjectId: this.selectedProjectId,
      FilterValue: data
    }
    if (data !== undefined && data != null) {
      this.budgetlinespinner = true;
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
            this.budgetlinespinner = false;
          } else {
            this.budgetlinespinner = false;
          }
        }, error => {
          this.budgetlinespinner = false;
        });
    }
  }

  onChangeAccountValue(event: any, id: number) {
    if (id !== undefined && id != null) {
      // this.addEditTransactionForm.controls['AccountId'].setValue(id);
      this.selectedAccountNo = id;
      this.selectedAccountName = event.source.value;
      this.addEditTransactionForm.controls['AccountId'].setValue(event.source.value);
    }
  }

  onChangeProjectValue(event: any, id: number) {
    if (id !== undefined && id != null) {
      if (event.isUserInput) {
        this.selectedProjectId = id;
        this.selectedProjectName = event.source.value;
        this.addEditTransactionForm.controls['ProjectId'].setValue(event.source.value);
        this.addEditTransactionForm.controls['BudgetLine'].setValue('');
        this.getDefaultBudgetLineList();
        this.addEditTransactionForm.controls['BudgetLine'].enable();
      }
    } else {
      this.addEditTransactionForm.controls['BudgetLine'].disable();
    }
  }

  onChangeBudgetLineValue(event: any, id: number) {
    if (id !== undefined && id != null) {
      this.selectedBudgetLineId = id;
      this.selectedBudgetLineName = event.source.value;
      this.addEditTransactionForm.controls['BudgetLine'].setValue(event.source.value);
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

    if (this.addEditTransactionForm.value.TransactionId && this.addEditTransactionForm.value.TransactionId !== 0) {
      this.editTransaction();
    } else {
      this.addNewTransaction();
    }
  }

  addNewTransaction() {
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

    this.ELEMENT_DATA.push(model);
    this.transactionDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
    this.selectedAccountNo = null;
    this.selectedProjectName = null;
    this.selectedProjectId = null;
    this.selectedProjectName = null;
    this.showAddTransactionTop = false;
    this.showAddTransactionBottom = false;
    this.addEditTransactionForm.reset();
    this.setAutoComplete();
  }

  editTransaction() {
    const index = this.ELEMENT_DATA.findIndex(x => x.TransactionId === this.addEditTransactionForm.value.TransactionId);
    if (index !== -1) {
      const creditamount = Number(this.addEditTransactionForm.getRawValue().Credit);
      const debitamount = Number(this.addEditTransactionForm.getRawValue().Debit);
      if (creditamount === 0) {
        this.ELEMENT_DATA[index].Type = 'Debit';
      } else {
        this.ELEMENT_DATA[index].Type = 'Credit';
      }

      this.voucherDetail.TotalDebit = this.voucherDetail.TotalDebit + parseFloat(debitamount.toFixed(2));
      this.voucherDetail.TotalCredit = this.voucherDetail.TotalCredit + parseFloat(creditamount.toFixed(2));
      this.voucherDetail.TotalDebit = this.voucherDetail.TotalDebit - this.ELEMENT_DATA[index].Debit;
      this.voucherDetail.TotalCredit = this.voucherDetail.TotalCredit - this.ELEMENT_DATA[index].Credit;

      this.ELEMENT_DATA[index].AccountNo = this.selectedAccountNo,
        this.ELEMENT_DATA[index].Debit = debitamount.toFixed(2),
        this.ELEMENT_DATA[index].Credit = creditamount.toFixed(2),
        this.ELEMENT_DATA[index].Amount = 0,
        this.ELEMENT_DATA[index].ProjectId = this.selectedProjectId,
        this.ELEMENT_DATA[index].ProjectName = this.selectedProjectName,
        this.ELEMENT_DATA[index].BudgetLineName = this.selectedBudgetLineName,
        this.ELEMENT_DATA[index].BudgetLineId = this.selectedBudgetLineId,
        this.ELEMENT_DATA[index].Description = this.addEditTransactionForm.getRawValue().Description,
        this.ELEMENT_DATA[index].VoucherNo = this.voucherNo,
        this.ELEMENT_DATA[index].JobId = this.addEditTransactionForm.getRawValue().JobId,
        this.ELEMENT_DATA[index].JobName = this.addEditTransactionForm.getRawValue().JobName,
        this.ELEMENT_DATA[index].AccountCode = this.selectedAccountName;
    }

    this.selectedAccountNo = null;
    this.selectedProjectName = null;
    this.selectedProjectId = null;
    this.selectedProjectName = null;
    this.showAddTransactionTop = false;
    this.showAddTransactionBottom = false;
    this.addEditTransactionForm.reset();
    this.setAutoComplete();
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
    } else {
      if (creditsTotal.length === 0 || debitsTotal.length === 0) {
        this.errorMessage = 'Credits and Debits must be equal';
        this.isFormSubmitted = false;
        return;
      } else {
        let credit = 0;
        let debit = 0;
        if (creditsTotal.length == 1) {
          credit = Number(creditsTotal[0].Credit);
        } else { creditsTotal.map(item => item.Credit).reduce((prev, next) => credit = Number(prev) + Number(next)); }
        if (debitsTotal.length == 1) {
          debit = Number(debitsTotal[0].Debit);
        } else { debitsTotal.map(item => item.Debit).reduce((prev, next) => debit = Number(prev) + Number(next)); }
        if (credit !== debit) {
          this.errorMessage = 'Credits and Debits must be equal';
          this.isFormSubmitted = false;
          return;
        }
      }
    }



    this.errorMessage = '';

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
    } else if (type === 1 && val !== 0) {
      this.addEditTransactionForm.get('Credit').setValue(val.toFixed(2));
      this.addEditTransactionForm.get('Debit').setValue(0);
      this.addEditTransactionForm.get('Debit').disable();
    } else if (type === 1 && val == 0) {
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
    if (value == null || value == undefined || value == '') {
      this.addEditTransactionForm.controls['BudgetLine'].setValue('');
      this.addEditTransactionForm.controls['BudgetLine'].disable();
    }
  }

  editTransactionBtnClick(data) {
    this.showAddTransactionBottom = true;
    this.showAddTransactionTop = true;
    this.addEditTransactionForm.controls['Credit'].enable();
    this.addEditTransactionForm.controls['Debit'].enable();

    if (data.Credit === 0) {
      this.addEditTransactionForm.controls['Credit'].disable();
    } else if (data.Debit === 0) {
      this.addEditTransactionForm.controls['Debit'].disable();
    }

    this.addEditTransactionForm.patchValue({
      'TransactionId': data.TransactionId,
      'AccountId': data.AccountCode,
      'AccountName': data.AccountCode,
      'Description': data.Description,
      'Credit': data.Credit,
      'Debit': data.Debit,
      'ProjectId': data.ProjectName,
      'BudgetLine': data.BudgetLineName,
      'JobId': data.JobId,
      'JobName': data.JobName,
      'Type': data.Type,
    });
    this.selectedProjectId = data.ProjectId;
    this.selectedBudgetLineId = data.BudgetLineId;
    this.selectedAccountNo = data.AccountNo;
    this.selectedBudgetLineName = data.BudgetLineName;
    this.selectedProjectName = data.ProjectName;
    this.selectedAccountName = data.AccountCode;
    this.addEditTransactionForm.controls['BudgetLine'].enable();
  }

}

export interface ITransaction {
  VoucherNo: any;
  TransactionList: any[];
}
