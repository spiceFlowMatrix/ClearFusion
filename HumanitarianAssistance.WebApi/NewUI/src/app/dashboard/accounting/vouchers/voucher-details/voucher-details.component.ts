import {
  Component,
  OnInit,
  Input,
  OnChanges,
  Output,
  EventEmitter,
  HostListener,
  OnDestroy
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { VoucherService } from '../voucher.service';
import { IResponseData } from '../models/status-code.model';
import {
  IVoucherDetailModel,
  IJournalListModel,
  ICurrencyListModel,
  IOfficeListModel,
  IProjectListModel,
  IVoucherListModel,
  IAccountListModel,
  IEditTransactionModel,
  AddEditTransactionModel
} from '../models/voucher.model';
import { ToastrService } from 'ngx-toastr';
import {
  IBudgetLineModel,
  IProjectJobModel
} from 'src/app/dashboard/project-management/project-list/budgetlines/models/budget-line.models';
import { IDataSource } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { BudgetLineService } from 'src/app/dashboard/project-management/project-list/budgetlines/budget-line.service';
import { ProjectJobsService } from 'src/app/dashboard/project-management/project-list/project-jobs/project-jobs.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { DocumentListingComponent } from 'projects/library/src/lib/components/document-listing/document-listing.component';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { FileSourceEntityTypes } from 'src/app/shared/enum';

@Component({
  selector: 'app-voucher-details',
  templateUrl: './voucher-details.component.html',
  styleUrls: ['./voucher-details.component.scss']
})
export class VoucherDetailsComponent implements OnInit, OnChanges, OnDestroy {
  //#region "variables"
  @Input() voucherId: number;
  @Input() journalList: IJournalListModel;
  @Input() currencyList: ICurrencyListModel;
  @Input() officeList: IOfficeListModel;
  @Input() projectList: IProjectListModel;
  @Input() voucherTypeList: IVoucherListModel;
  @Input() inputLevelAccountList: IAccountListModel[];
  @Input() isEditingAllowed: boolean;
  @Output() voucherDetailChanged = new EventEmitter<IVoucherDetailModel>();

  accountDataSource: IDataSource[];
  selectedAccount: number[];

  voucherDetail: IVoucherDetailModel;
  transactionCreditList: IEditTransactionModel[] = [];
  transactionDebitList: IEditTransactionModel[] = [];
  budgetLineDetailList: IBudgetLineModel[] = [];
  projectJobList: IProjectJobModel[] = [];
  budgetLineLoader = false;
  transactionListTEMP: IEditTransactionModel[] = [];

  checkTransactionFlag = false;

  // loader
  voucherDetailLoader = false;
  fileUploadLoader = false;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  //#endregion

  constructor(
    public dialog: MatDialog,
    public commonLoader: CommonLoaderService,
    private voucherService: VoucherService,
    private toastr: ToastrService,
    private budgetLineService: BudgetLineService,
    private projectJobService: ProjectJobsService,
    private globalSharedService: GlobalSharedService
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.initForm();
    this.accountDataSource = [];
  }

  //#region "ngOnChanges"
  ngOnChanges() {
    this.transactionListTEMP = [];

    if (
      this.voucherId !== 0 &&
      this.voucherId !== null &&
      this.voucherId !== undefined
    ) {
      this.getVoucherDetailById(this.voucherId);
      this.getTransactionByVoucherId(this.voucherId);
      this.getAllProjectJobDetail();
    }
    this.inputLevelAccountList.forEach(x => {
      this.accountDataSource.push({
        Id: x.AccountCode,
        Name: x.AccountName
      });
    });
  }
  //#endregion

  initForm() {
    this.voucherDetail = {
      VoucherNo: null,

      ReferenceNo: null,
      Description: null,
      VoucherDate: null,
      CurrencyId: null,
      JournalCode: null,
      ChequeNo: null,

      VoucherTypeId: null,
      OfficeId: null,
      ProjectId: null,
      BudgetLineId: null,
      FinancialYearId: null,
      IsVoucherVerified: false
    };
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 200 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "getVoucherDetailById"
  getVoucherDetailById(id: number) {
    this.voucherDetailLoader = true;

    this.voucherService.GetVoucherDetailById(id)
    .pipe(takeUntil(this.destroyed$))
    .subscribe(
      (response: IResponseData) => {
        this.voucherDetail = null;

        if (response.statusCode === 200 && response.data !== null) {
          this.voucherDetail = {
            VoucherNo: response.data.VoucherNo,

            ReferenceNo: response.data.ReferenceNo,
            Description: response.data.Description,
            VoucherDate: new Date(
              new Date(response.data.VoucherDate).getTime() -
                new Date().getTimezoneOffset() * 60000
            ),
            CurrencyId: response.data.CurrencyId,
            JournalCode: response.data.JournalCode,
            ChequeNo: response.data.ChequeNo,

            VoucherTypeId: response.data.VoucherTypeId,
            OfficeId: response.data.OfficeId,
            ProjectId: response.data.ProjectId,
            BudgetLineId: response.data.BudgetLineId,
            FinancialYearId: response.data.FinancialYearId,
            IsVoucherVerified: response.data.IsVoucherVerified
          };
        } else if (response.statusCode === 400 && response.data === null) {
          this.toastr.warning(response.message);
        }
        this.voucherDetailLoader = false;
      },
      error => {
        this.voucherDetailLoader = false;
      }
    );
  }
  //#endregion

  //#region "getTransactionByVoucherId"
  getTransactionByVoucherId(id: number) {

    this.transactionListTEMP = [];
    this.transactionCreditList = [];
    this.transactionDebitList = [];


    this.voucherService.GetTransactionByVoucherId(id)
    .pipe(takeUntil(this.destroyed$))
    .subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.transactionCreditList = response.data.filter(
            x => x.Credit !== 0
          );
          this.transactionDebitList = response.data.filter(x => x.Debit !== 0);
        } else if (response.statusCode === 400 && response.data === null) {
          this.toastr.warning(response.message);
        }
      },
      error => {
      }
    );
  }
  //#endregion

  //#region "GetallBudgetLine list"
  getAllBudgetDetail() {
    this.voucherDetailLoader = true;
    this.voucherService.GetBudgetLineList()
    .pipe(takeUntil(this.destroyed$))
    .subscribe(
      (response: IResponseData) => {
        this.budgetLineDetailList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.budgetLineDetailList.push({
              ProjectJobId: element.ProjectJobId,
              ProjectJobCode: element.ProjectJobCode,
              ProjectJobName: element.ProjectJobName,
              BudgetCode: element.BudgetCode,
              BudgetLineId: element.BudgetLineId,
              BudgetName: element.BudgetName,
              CurrencyId: element.CurrencyId,
              CurrencyName: element.CurrencyName,
              InitialBudget: element.InitialBudget,
              ProjectId: element.ProjectId,
              CreatedDate: element.CreatedDate,
              DebitPercentage: element.DebitPercentage,
            });
          });
        } else if (response.statusCode === 400 && response.data === null) {
          this.toastr.warning(response.message);
        }
        this.voucherDetailLoader = false;
      },
      error => {
        this.voucherDetailLoader = false;
      }
    );
  }
  //#endregion

  //#region "GetallBudgetLine list"
  getAllProjectJobDetail() {
    this.voucherDetailLoader = true;
    this.voucherService.GetProjectobList()
    .pipe(takeUntil(this.destroyed$))
    .subscribe(
      (response: IResponseData) => {
        this.projectJobList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.projectJobList.push({
              ProjectJobId: element.ProjectJobId,
              ProjectJobCode: element.ProjectJobCode,
              ProjectJobName: element.ProjectJobName
            });
          });
        } else if (response.statusCode === 400 && response.data === null) {
          // this.toastr.warning(response.message);
        }
        this.voucherDetailLoader = false;
      },
      error => {
        this.voucherDetailLoader = false;
      }
    );
  }
  //#endregion

  //#region "addEditTransactionList"
  addEditTransactionList(data: IEditTransactionModel[]) {
    if (this.voucherId !== undefined) {
      const transactionModel: AddEditTransactionModel = {
        VoucherNo: this.voucherId,
        VoucherTransactions: data
      };

      this.checkTransactionFlag = true;

      this.voucherService.AddEditTransactionList(transactionModel)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          // this.voucherDetail = null;
          if (response.statusCode === 200) {
            this.toastr.success('Transaction Updated Successfully');
            this.getTransactionByVoucherId(this.voucherId);
          } else if (response.statusCode === 400) {
            this.toastr.warning(response.message);
          }
          this.commonLoader.hideLoader();

          this.checkTransactionFlag = false;
        },
        error => {
          this.commonLoader.hideLoader();
          this.toastr.error('Someting went wrong');
          this.checkTransactionFlag = false;
        }
      );
    } else {
      this.toastr.error('Voucher is undefined');
    }
  }
  //#endregion

  //#region "editVoucherDetailById"
  editVoucherDetailById(data: IVoucherDetailModel) {
    this.checkTransactionFlag = true;

    const voucherDetails = {
      VoucherNo: data.VoucherNo,
      ReferenceNo: data.ReferenceNo,
      Description: data.Description,
      VoucherDate: StaticUtilities.getLocalDate(data.VoucherDate),
      CurrencyId: data.CurrencyId,
      JournalCode: data.JournalCode,
      ChequeNo: data.ChequeNo,
      VoucherTypeId: data.VoucherTypeId,
      OfficeId: data.OfficeId,
      ProjectId: data.ProjectId,
      BudgetLineId: data.BudgetLineId,
      FinancialYearId: data.FinancialYearId,
      IsVoucherVerified: data.IsVoucherVerified,
      TimezoneOffset: new Date().getTimezoneOffset()
    };

    this.voucherService.EditVoucherDetailById(voucherDetails)
    .pipe(takeUntil(this.destroyed$))
    .subscribe(
      (response: IResponseData) => {

        if (response.statusCode === 200) {
          this.voucherDetailChanged.emit(voucherDetails);
        } else if (response.statusCode === 400) {
          this.toastr.warning(response.message);
        }
        this.checkTransactionFlag = false;
      },
      error => {
        this.toastr.error('Someting went wrong');
        this.checkTransactionFlag = false;
      }
    );
  }
  //#endregion

  //#region "voucherVerify"
  voucherVerify(voucherNo: number) {
    if (voucherNo !== undefined) {
      // this.checkTransactionFlag = true;

      this.voucherDetail.IsVoucherVerified = !this.voucherDetail.IsVoucherVerified;

      this.voucherService.VoucherVerify(voucherNo)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            // this.voucherDetail.IsVoucherVerified = response.data !== null ? response.data : false;
            // this.toastr.success(response.message);
          } else if (response.statusCode === 400) {
            this.toastr.warning(response.message);
          }

          // this.checkTransactionFlag = false;
        },
        error => {
          this.toastr.error('Someting went wrong');
          // this.checkTransactionFlag = false;
        }
      );
    }
  }
  //#endregion

  //#region "onVoucherValuechange"
  onVoucherValuechange() {
    this.editVoucherDetailById(this.voucherDetail);
  }
  //#endregion

  //#region "onTransactionCreditDelete"
  onTransactionCreditDelete(data: IEditTransactionModel) {
    let tempListIndex: number = null;
    let index: number = null;

    if (data.TransactionId !== 0) {
      tempListIndex = this.transactionListTEMP.findIndex(
        x => x.TransactionId === data.TransactionId
      );

      index = this.transactionCreditList.findIndex(
        x => x.TransactionId === data.TransactionId
      );

      // delete from list
      this.transactionCreditList.splice(index, 1);

      //#region "perform operation (add/edit on temp list)"
      data.IsDeleted = true;
      if (tempListIndex === -1) {
        // add to list
        this.transactionListTEMP.push(data);
      } else {
        this.transactionListTEMP[tempListIndex] = data;
      }
      //#endregion
    } else {
      tempListIndex = this.transactionListTEMP.findIndex(
        x => x._IsId === data._IsId
      );
      index = this.transactionCreditList.findIndex(x => x._IsId === data._IsId);

      // delete from list
      this.transactionCreditList.splice(index, 1);

      // delete from list
      this.transactionListTEMP.splice(tempListIndex, 1);
    }
  }
  //#endregion

  //#region "onTransactionDebitDelete"
  onTransactionDebitDelete(data: IEditTransactionModel) {
    let tempListIndex: number = null;
    let index: number = null;

    if (data.TransactionId !== 0) {
      tempListIndex = this.transactionListTEMP.findIndex(
        x => x.TransactionId === data.TransactionId
      );

      index = this.transactionDebitList.findIndex(
        x => x.TransactionId === data.TransactionId
      );

      // delete from list
      this.transactionDebitList.splice(index, 1);

      //#region "perform operation (add/edit on temp list)"
      data.IsDeleted = true;
      if (tempListIndex === -1) {
        // add to list
        this.transactionListTEMP.push(data);
      } else {
        this.transactionListTEMP[tempListIndex] = data;
      }
      //#endregion
    } else {
      tempListIndex = this.transactionListTEMP.findIndex(
        x => x._IsId === data._IsId
      );
      index = this.transactionDebitList.findIndex(x => x._IsId === data._IsId);

      // delete from list
      this.transactionDebitList.splice(index, 1);

      // delete from list
      this.transactionListTEMP.splice(tempListIndex, 1);
    }
  }
  //#endregion

  //#region "onTransactionDetailChanged"
  onTransactionDetailChanged(item: IEditTransactionModel, elementName: string) {
    let tempListIndex: number = null;

    //#region "searh / find"
    if (item.TransactionId !== 0) {
      tempListIndex = this.transactionListTEMP.findIndex(
        x => x.TransactionId === item.TransactionId
      );
    } else {
      tempListIndex = this.transactionListTEMP.findIndex(
        x => x._IsId === item._IsId
      );
    }
    //#endregion

    //#region "perform operation (add/edit on temp list)"
    item.IsDeleted = false;
    if (tempListIndex === -1) {
      // add to list
      this.transactionListTEMP.push(item);
    } else {
      this.transactionListTEMP[tempListIndex] = item;
    }
    //#endregion

    if (item.ProjectId !== null && item.ProjectId !== undefined) {
      this.getBudgetLineOnProjectId(item, elementName);
    }
    if (item.BudgetLineId !== undefined && item.BudgetLineId !== null) {
      this.GetProjectJobDetailByBudgetLineId(item);
    }
  }
  //#endregion

  //#region "getBudgetLineOnProjectId"
  GetProjectJobDetailByBudgetLineId(item: any) {
    this.projectJobService
      .GetProjectJobDetailByBudgetLineId(item.BudgetLineId)
      .subscribe(
        (response: IResponseData) => {
          // this.voucherDetail = null;
          if (response.statusCode === 200) {
            item.JobName = response.data.ProjectJobName;
            item.JobId = response.data.ProjectJobId;
            // this.toastr.success('Transaction Deleted Successfully');
          } else if (response.statusCode === 400) {
            this.toastr.warning(response.message);
          }
        },
        error => {
          this.toastr.error('Someting went wrong');
        }
      );
  }
  //#endregion

  //#region "getBudgetLineOnProjectId"
  getBudgetLineOnProjectId(item: any, elementName: string) {
    this.budgetLineLoader = true;
    if (elementName === 'Project') {
      item.BudgetLineId = null;
    }

    item.BudgetLineList = [];
    item.JobName = '';

    this.budgetLineService.GetProjectBudgetLineList(item.ProjectId).subscribe(
      (response: IResponseData) => {
        // this.voucherDetail = null;
        if (response.statusCode === 200) {
          response.data.forEach(x => item.BudgetLineList.push(x));
          // this.toastr.success('Transaction Deleted Successfully');
        } else if (response.statusCode === 400) {
          this.toastr.warning(response.message);
        }
        this.budgetLineLoader = false;
      },
      error => {
        this.budgetLineLoader = false;
        this.toastr.error('Someting went wrong');
      }
    );
  }
  //#endregion

  //#region "onAddTransactionCredit"
  onAddTransactionCredit() {
    const transactiondata: IEditTransactionModel = {
      TransactionId: 0, // strict 0
      VoucherNo: this.voucherId,
      AccountNo: null,
      Description: '',
      ProjectId: null,
      BudgetLineId: null,
      Credit: 0,
      Debit: 0,
      IsDeleted: false,
      JobId: 0,
      JobName: '',

      BudgetLineList: [],
      _IsId: this.RandomNum(),
      _IsDeleted: false
    };

    this.transactionListTEMP.push(transactiondata);
    this.transactionCreditList.push(transactiondata);
  }
  //#endregion

  //#region "onAddTransactionDebit"
  onAddTransactionDebit() {
    const transactiondata: IEditTransactionModel = {
      TransactionId: 0, // strict 0
      VoucherNo: this.voucherId,
      AccountNo: null,
      Description: '',
      ProjectId: null,
      BudgetLineId: null,
      Credit: 0,
      Debit: 0,
      IsDeleted: false,
      JobId: 0,
      JobName: '',

      BudgetLineList: [],
      _IsId: this.RandomNum(),
      _IsDeleted: false
    };

    this.transactionListTEMP.push(transactiondata);
    this.transactionDebitList.push(transactiondata);
  }
  //#endregion

  //#region "onTransactionListVerify"
  onTransactionListVerify() {
    if (this.totalCredits === this.totalDebits) {
      if (this.checkZeroCredits() > 0 || this.checkZeroDebits() > 0) {
        this.toastr.warning('Amount must be greater than 0');
      } else {
        if (
          this.transactionListTEMP.find(x => x.AccountNo == null) === undefined
        ) {
          this.transactionListTEMP.length > 0
            ? this.addEditTransactionList(this.transactionListTEMP)
            : this.toastr.info('No transaction to edit');
        } else {
          this.toastr.warning('Please select Account No');
        }
      }
    } else {
      this.toastr.error('Transaction is unbalanced');
    }
  }
  //#endregion

  //#region "onVoucherVerify"
  onVoucherVerify() {
    if (this.totalCredits === this.totalDebits) {
      if (this.checkZeroCredits() > 0 || this.checkZeroDebits() > 0) {
        this.toastr.warning('Amount must be greater than 0');
      } else {
        if (
          this.transactionListTEMP.find(x => x.AccountNo == null) === undefined
        ) {
          if (this.transactionListTEMP.length > 0) {
            this.toastr.warning('First save the transactions');
          } else {
            this.voucherVerify(this.voucherId);
          }
        } else {
          this.toastr.warning('Please select Account No');
        }
      }
    } else {
      this.toastr.error('Transaction is unbalanced');
    }
  }
  //#endregion


  //#region "openDocumentsDialog"
  openDocumentsDialog() {
    const dialogRef = this.dialog.open(DocumentListingComponent, {
      width: '400px',
      minHeight: '300px',
      maxHeight: '500px',
      autoFocus: false,
      data: {pageId: FileSourceEntityTypes.Voucher,
             recordId: this.voucherId}
    });

    // delete
    // dialogRef.componentInstance.deleteDocument.subscribe(
    //   (item: IDocumentsModel) => {
    //     this.deleteDocument(item);
    //   }
    // );

    // dialogRef.componentInstance.documentListRefresh.subscribe(
    //   (item: IDocumentsModel) => {
    //     this.documentListRefresh.emit(item);
    //   }
    // );

    // dialogRef.componentInstance.documentDownload.subscribe(
    //   (item: IDocumentsModel) => {
    //     this.documentDownload.emit(item);
    //   }
    // );

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion


  openedChange(event: any, Item: IEditTransactionModel) {
    Item.AccountNo = event.Value !== undefined ? event.Value : Item.AccountNo;
    this.onTransactionDetailChanged(Item, 'Account');
  }

  //#region "RandomNum" NOTE: Use for Add functionality
  RandomNum() {
    return Math.floor(Math.random() * 10000);
  }
  //#endregion

  //#region "Totals" NOTE: DONT CHANGE
  get totalCredits() {
    return this.transactionCreditList.reduce((a, { Credit }) => a + Credit, 0);
  }

  get totalDebits() {
    return this.transactionDebitList.reduce((a, { Debit }) => a + Debit, 0);
  }

  checkZeroCredits() {
    return this.transactionCreditList.filter(x => x.Credit === 0).length;
  }

  checkZeroDebits() {
    return this.transactionDebitList.filter(x => x.Debit === 0).length;
  }
  //#endregion

  /**
   * this is used to trigger the input
   */
  openInput(){
    // your can use ElementRef for this later
    document.getElementById('fileInput').click();
  }

  fileChange(files: File[]) {
    if (files.length > 0) {
      this.fileUploadLoader = true;

      for (let i = 0; i < files.length; i++ ) {
        this.globalSharedService.uploadFile(FileSourceEntityTypes.Voucher, this.voucherId, files[i])
        .pipe(takeUntil(this.destroyed$)).subscribe(x => this.fileUploadLoader = false);
      }
    }
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }

}
