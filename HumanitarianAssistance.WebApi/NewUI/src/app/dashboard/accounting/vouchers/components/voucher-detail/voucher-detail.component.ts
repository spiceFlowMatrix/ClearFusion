import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VoucherService } from '../../voucher.service';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { startWith, map } from 'rxjs/operators';
import { BudgetLineService } from 'src/app/dashboard/project-management/project-list/budgetlines/budget-line.service';
import { IResponseData } from '../../models/status-code.model';
import { TransactionType, VoucherType, FileSourceEntityTypes } from 'src/app/shared/enum';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { MatDialog } from '@angular/material/dialog';
import { AddDocumentComponent } from 'src/app/store/components/document-upload/add-document.component';
import { AddDocumentsComponent } from '../add-documents/add-documents.component';
import { of } from 'rxjs/internal/observable/of';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { Auth0Service } from 'src/app/auth0/auth0.service';
import { PermissionEnum } from 'src/app/shared/permission-enum';

@Component({
  selector: 'app-voucher-detail',
  templateUrl: './voucher-detail.component.html',
  styleUrls: ['./voucher-detail.component.scss']
})



export class VoucherDetailComponent implements OnInit {

  voucherNo: any;
  voucherDetail: any;
  displayedColumns: string[] = ['AccountCode', 'Description', 'DebitAmount', 'CreditAmount', 'ProjectName', 'BudgetLineName', 'JobName'];
  ELEMENT_DATA: any[] = [];
  documentList: any[] = [];
  isModifyTransactions = false;
  transactionDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
  selection = new SelectionModel<any>(true, []);
  addEditTransaction = false;
  filterdOptionsAccountList: Observable<any[]>;
  uploadedFiles: any[] = [];
  accountList: any[] = [];
  projectList: any[] = [];
  budgetLineList: any[] = [];
  typeList: any[] = [];
  selectedAccount: any;
  selectedProject: any;
  recordCount = 0;
  VoucherTypeEnum = VoucherType;
  filterdOptionsProjectList: Observable<any[]>;
  hideUnitColums: Observable<{ headers?: string[], items?: string[] }>;

  myControl = new FormControl();
  options: string[] = ['One', 'Two', 'Three'];
  filteredOptions: Observable<string[]>;

  transactionPagingModel: any;
  permissionsEnum = PermissionEnum;

  constructor(private routeActive: ActivatedRoute,
    private router: Router, private voucherService: VoucherService,
    private toastr: ToastrService, private fb: FormBuilder,
    private budgetLineService: BudgetLineService,
    private globalSharedService: GlobalSharedService,
    private appurl: AppUrlService, private dialog: MatDialog,
    private globalService: GlobalService,
    private auth0Service: Auth0Service) {
    this.routeActive.url.subscribe(params => {
      this.voucherNo = +params[0].path;
    });
  }

  ngOnInit() {
    this.transactionPagingModel = {
      PageSize: 10,
      PageIndex: 0,
      Records: 0,
      VoucherNo: this.voucherNo
    };

    this.onFormInIt();
    this.getDetailsByVoucherNo();
    this.getVoucherTransactionsByVoucherNo();
    this.hideUnitColums = of({
      headers: ['Name', 'Uploaded On'],
      items: ['FileName', 'Date']
    });
    this.getDocumentList();
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
      OperationalType: null,
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
    // this.isModifyTransactions = true;
    // this.displayedColumns = ['select', 'Type', 'AccountCode', 'Description', 'Amount', 'ProjectName', 'BudgetLineName', 'JobName'];
    this.router.navigate(['../../vouchers/' + this.voucherNo + '/transaction-detail'], { relativeTo: this.routeActive });
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

  onAddTransactionBtnClick() {
    this.addEditTransaction = true;
  }

  cancelTransactionBtnClicked() {
    this.addEditTransaction = false;
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

  onChangeAccountValue(event: any, id: number) {
    if (id !== undefined && id != null) {
      // this.addEditTransactionForm.controls['AccountId'].setValue(id);
      this.selectedAccount = id;
    }
  }

  onChangeProjectValue(event: any, id: number) {
    if (id !== undefined && id != null) {
      this.selectedProject = id;
      this.getBudgetLineByProjectId(id);
    }
  }

  getBudgetLineByProjectId(id) {
    this.budgetLineService.GetProjectBudgetLineList(id).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.budgetLineList = [];
          response.data.forEach(x => this.budgetLineList.push({
            Id: x.BudgetLineId,
            Name: x.BudgetCodeName
          }));
        } else if (response.statusCode === 400) {
          this.toastr.warning(response.message);
        }
      },
      error => {
        this.toastr.error(error);
      }
    );
  }

  backClick() {
    this.router.navigate(['../'], { relativeTo: this.routeActive });
  }

  editVoucher() {
    this.router.navigate(['../../vouchers/' + this.voucherNo + '/edit-voucher'], { relativeTo: this.routeActive });
  }

  validateVoucher() {
    const ids: any[] = [];
    ids.push(this.voucherNo);
    this.voucherService.verifySelectedVouchers(ids).subscribe(x => {
      if (x) {
        this.toastr.success('Verified successfully');
        this.voucherDetail.IsVoucherVerified = !this.voucherDetail.IsVoucherVerified
      }
    }, error => {
      this.toastr.warning(error);
    });
  }

  deleteVoucher() {
    const ids: any[] = [];
    ids.push(this.voucherNo);
    this.voucherService.deleteSelectedVouchers(ids).subscribe(x => {
      if (x) {
        this.toastr.success('deleted successfully');
      }
    }, error => {
      this.toastr.warning(error);
    });
  }

  exportVoucher() {
    const model = {
      VoucherIdList: []
    };
    model.VoucherIdList.push(this.voucherNo);
    this.globalSharedService
      .getFile(this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetAllVoucherSummaryReportPdf,
        model
      )
      .subscribe();
  }

  openAddDocumentDialog(): void {
    const dialogRef = this.dialog.open(AddDocumentsComponent, {
      width: '400px',
      data: {
        voucherNo: this.voucherNo
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getDocumentList();
      // console.log(result);
      // if (result) {
      //   this.uploadedPurchasedFiles.unshift({
      //     Id: result.id,
      //     Filename: result.file === undefined ? result.filename : result.file[0].name,
      //     DocumentTypeName: result.documentName,
      //     Date: this.datePipe.transform(result.uploadDate, 'dd-MM-yyyy'),
      //     UploadedBy: result.uploadBy === undefined ? localStorage.getItem('LoggedInUserName') : result.uploadBy,
      //     File: result.file,
      //     DocumentTypeId: result.documentType,
      //     SignedUrl: result.signedUrl
      //   });

      //   // For ngOnChanges on document-upload component
      //   this.uploadedPurchasedFiles = this.uploadedPurchasedFiles.slice();
      // }
    });
  }

  //#region "getDocumentList"
  getDocumentList() {
    const documentData = {
      RecordId: this.voucherNo,
      PageId: FileSourceEntityTypes.Voucher
    };

    this.globalService
      .post(this.appurl.getApiUrl() + GLOBAL.API_FileManagement_GetDocumentFiles, documentData)
      .subscribe(response => {
        if (response.StatusCode = 200 && response.data.DocumentFileList != null) {
          this.documentList = [];
          response.data.DocumentFileList.forEach((element) => {
            this.documentList.push({
              FileName: element.FileName,
              Date: element.Date,
              SignedUrl: element.FileSignedURL,
              DocumentFileId: element.DocumentFileId
            });
          });
        }
      }, error => {
      });
  }
  //#endregion

  onDeleteDocument(item) {
    if (item.DocumentFileId !== undefined) {

      const DocumentData = {
        DocumentFileId: item.DocumentFileId,
        PageId: FileSourceEntityTypes.Voucher
      };

      this.globalService
        .post(this.appurl.getApiUrl() + GLOBAL.API_FileManagement_DeleteDocumentFiles, DocumentData)
        .subscribe(response => {
          if (response.StatusCode = 200) {
            const index = this.documentList.findIndex(x => x.DocumentFileId === item.DocumentFileId);
            this.documentList.splice(index, 1);
          }
        });
    }
  }

  onDownloadDocument(item) {
    const doc = document.createElement('a');
    doc.href = item.SignedUrl;
    doc.target = '_blank';
    doc.click();
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

  checkPermission(permission) {
    debugger;
    return this.auth0Service.checkPermissions(permission);
  }

}
