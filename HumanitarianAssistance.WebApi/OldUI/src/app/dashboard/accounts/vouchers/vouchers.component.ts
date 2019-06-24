import {
  Component,
  OnInit,
  ViewChild,
  OnDestroy
} from '@angular/core';
import { AccountsService } from '../accounts.service';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../../code/code.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Router, ActivatedRoute } from '@angular/router';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { TransactionComponent } from './transaction/transaction.component';


import { HubConnection } from '@aspnet/signalr';
import DataSource from 'devextreme/data/data_source';
import { LoggerDetailsModel, CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-vouchers',
  templateUrl: './vouchers.component.html',
  styleUrls: ['./vouchers.component.css']
})
export class VouchersComponent implements OnInit, OnDestroy {
  @ViewChild(TransactionComponent)
  private transactionChild: TransactionComponent;

  selectedVoucherData: VoucherDetailsModel;
  voucherDetailsForm: VoucherDetailsModel;
  voucherDetailsList: any;
  budgetLineFilteredList: any[];
  isEditingAllowed = false;

  // Flag
  addEditVoucherFormPopupVisible = false;
  addEditVoucherFormPopupLoading = false;

  // loader
  voucherLoading = false;

  voucherFilterForm: VoucherFilterModel;
  currencyModel: any[];
  officeCodeModel: any[];

  selectedOffices: any[];

  journalcodelist: any[];
  voucherTypeArr: any[];
  voucherDetails: VoucherDetailsModel[];
  voucherNoMain: any; // set voucherNo on Popup
  voucherDocumentDetails: any[];
  popupVisibleDocument = false;
  projectArr: any[];
  projectBudgetLineArr: any[];
  budgetLineArr: any[];
  defaultDoc: any;
  popupVisible = false;
  docpath: any;
  docPopupVisible = false;
  imageURL: any;
  addNewDocument: any;
  financialYearArr: any;
  PurchaseVoucherNo: any;
  sub: any;

  voucherListTrack = 0;
  voucherListTrackBackend = 0;


  // signalR
  loggerMode: LoggerDetailsModel;
  private _hubConnection: HubConnection;
  public async: any;
  message = '';
  messages: string[] = [];

  constructor(
    private accountservice: AccountsService,
    private router: Router,
    private codeservice: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    public commonService: CommonService,
    private _DomSanitizer: DomSanitizer,
    private route: ActivatedRoute
  ) {
    // VoucherType Static array
    this.voucherTypeArr = [
      { VoucherTypeId: 1, VoucherTypeName: 'Journal' },
      { VoucherTypeId: 2, VoucherTypeName: 'Adjustment' }
    ];

    // this.windows = window;

    // this.budgetLineFilteredList = this.budgetLineFilteredList.bind(this);

    this.voucherDocumentDetails = [{ DocumentGUID: '', DocumentName: '' }];
    this.addNewDocument = {
      DocumentName: '',
      DocumentFilePath: '',
      DocumentDate: ''
    };
    // this.windows = window;
    this.docpath = _DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + 'nodoc.pdf'
    );
  }

  ngOnDestroy(): void {
    this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + 'nodoc.pdf'
    );
  }

  ngOnInit() {
    // tslint:disable-next-line:radix
    this.PurchaseVoucherNo = parseInt(this.route.snapshot.paramMap.get('id'));

    this.voucherDetails = [];
    this.voucherDetailsList = [];

    this.initForm();
    this.initFilterForm();
    this.getFinancialYear();
    this.getCurrencyCodeList();

    this.getJournalCodeList();
    // this.getAllVoucherDetailsByFilter();
    this.getAllProjectDetails();

    if (!isNaN(this.PurchaseVoucherNo)) {
      this.getVoucherDetailByVoucherNo();
    } else {
      this.getOfficeCodeList();
    }

    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.JournalCodes
    );
  }

  //#region "initForm"
  initForm() {
    this.voucherDetailsForm = {
      BudgetLineId: null,
      ChequeNo: null,
      CurrencyCode: null,
      CurrencyId: null,
      Description: null,
      FinancialYearId: null,
      FinancialYearName: null,
      JournalCode: null,
      JournalName: null,
      OfficeId: null,
      OfficeName: null,
      ProjectId: null,
      ReferenceNo: null,
      VoucherDate: null,
      VoucherNo: null,
      VoucherTypeId: null
    };
  }

  initFilterForm() {
    // Filter Form
    this.voucherFilterForm = {
      Date: null,
      OfficesList: null,
      Skip: 0
    };
  }
  //#endregion

  //#region getFinancialYear
  getFinancialYear() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllFinancialYearDetail
      )
      .subscribe(
        data => {
          this.financialYearArr = [];
          if (
            data.data.FinancialYearDetailList != null &&
            data.data.FinancialYearDetailList.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.FinancialYearDetailList.forEach(element => {
              this.financialYearArr.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region getCurrencyCodeList
  getCurrencyCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyModel = [];
          if (data.data.CurrencyList != null) {
            data.data.CurrencyList.forEach(element => {
              this.currencyModel.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region getOfficeCodeList
  getOfficeCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          this.officeCodeModel = [];
          if (data.data.OfficeDetailsList != null) {
            const allOffices = [];
            const officeIds: any[] =
              localStorage.getItem('ALLOFFICES') != null
                ? localStorage.getItem('ALLOFFICES').split(',')
                : null;
            data.data.OfficeDetailsList.forEach(element => {
              allOffices.push(element);
            });

            officeIds.forEach(x => {
              const officeData = allOffices.filter(
                // tslint:disable-next-line:radix
                e => e.OfficeId === parseInt(x)
              )[0];
              this.officeCodeModel.push(officeData);
            });
            this.selectedOffices = [];
            officeIds.forEach(x => {
              // tslint:disable-next-line:radix
              this.selectedOffices.push(parseInt(x));
            });

            // Function call for voucher details on page load
            const onFilterModel = {
              Date: null,
              OfficesList: this.selectedOffices
            };
            this.onFieldVoucheFilterChanged(onFilterModel);

            // sort in Asc
            this.officeCodeModel = this.commonService.sortDropdown(
              this.officeCodeModel,
              'OfficeName'
            );
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region  getJournalCodeList
  getJournalCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_JournalCode_GetAllJournalDetail
      )
      .subscribe(
        data => {
          this.journalcodelist = [];
          if (data.data.JournalDetailList != null) {
            data.data.JournalDetailList.forEach(element => {
              this.journalcodelist.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region  getAllVoucherDetailsByFilter
  getAllVoucherDetailsByFilter() {
    this.showVoucherLoading();
    this.accountservice
      .GetAllVoucherDetailsByFilter(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetAllVoucherDetailsByFilter,
        this.voucherFilterForm
      )
      .subscribe(
        data => {
          const itemIndex = this.voucherDetails.findIndex(i => i.VoucherNo === 0);
          this.voucherDetails.splice(itemIndex, 1);

          if (data.data.VoucherDetailList != null) {
            data.data.VoucherDetailList.forEach(element => {
              this.voucherDetails.push({
                BudgetLineId: element.BudgetLineId,
                ChequeNo: element.ChequeNo,
                CurrencyCode: element.CurrencyCode,
                CurrencyId: element.CurrencyId,
                Description: element.Description,
                FinancialYearId: element.FinancialYearId,
                FinancialYearName: element.FinancialYearName,
                JournalCode: element.JournalCode,
                JournalName: element.JournalName,
                OfficeId: element.OfficeId,
                OfficeName: element.OfficeName,
                ProjectId: element.ProjectId,
                ReferenceNo: element.ReferenceNo,
                VoucherDate: new Date(
                  new Date(element.VoucherDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                VoucherNo: element.VoucherNo,
                VoucherTypeId: element.VoucherTypeId
              });
            });

            if (this.voucherDetails.length > 0 && this.voucherListTrack < 10) {
              this.transactionChild.transactionFlag = true; // child call
              this.onVoucherSelectionChanged(this.voucherDetails[0]);
            } else {
              this.selectedVoucherData = null;
              this.transactionChild.transactionFlag = false; // child call
            }

            // this.voucherFilterForm.Skip = 0;
            this.voucherFilterForm.Skip = this.voucherDetails.length + 1;

            this.voucherDetails.push({
              BudgetLineId: 0,
              ChequeNo: '',
              CurrencyCode: '',
              CurrencyId: 0,
              Description: '',
              FinancialYearId: 0,
              FinancialYearName: '',
              JournalCode: '',
              JournalName: '',
              OfficeId: 0,
              OfficeName: '',
              ProjectId: 0,
              ReferenceNo: 'Show More',
              VoucherDate: null,
              VoucherNo: 0,
              VoucherTypeId: 0
            });

            // DataSource
            this.voucherDetailsList = this.voucherDetails;
          }
          this.hideVoucherLoading();
        },
        error => {
          this.hideVoucherLoading();
        }
      );
  }
  //#endregion

  //#region getAllProjectDetails
  getAllProjectDetails() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetProjectAndBudgetLine
      )
      .subscribe(
        data => {
          this.projectArr = [];
          this.projectBudgetLineArr = [];
          if (
            data.data.ProjectBudgetLinesModel != null &&
            data.StatusCode === 200
          ) {
            if (data.data.ProjectBudgetLinesModel.ProjectList.length > 0) {
              data.data.ProjectBudgetLinesModel.ProjectList.forEach(element => {
                this.projectArr.push(element);
              });
            }
            if (data.data.ProjectBudgetLinesModel.BudgetLines.length > 0) {
              data.data.ProjectBudgetLinesModel.BudgetLines.forEach(element => {
                this.projectBudgetLineArr.push(element);
              });
            }
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  //#endregion

  //#region AddVoucher
  AddVoucher(data: VoucherDetailsModel) {
    this.showAddEditVoucherFormPopupLoading();

    const voucherData: VoucherDetailsModel = {
      VoucherNo: 0,
      CurrencyId: data.CurrencyId,
      OfficeId: data.OfficeId,
      VoucherDate: new Date(
        new Date(data.VoucherDate).getFullYear(),
        new Date(data.VoucherDate).getMonth(),
        new Date(data.VoucherDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      ChequeNo: data.ChequeNo,
      JournalCode: data.JournalCode,
      VoucherTypeId: data.VoucherTypeId,
      Description: data.Description,
      ProjectId: data.ProjectId,
      BudgetLineId: data.BudgetLineId,
      FinancialYearId: data.FinancialYearId,

      CurrencyCode: '',
      FinancialYearName: '',
      JournalName: '',
      OfficeName: '',
      ReferenceNo: ''
    };

    this.accountservice
      .AddVoucher(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_AddVouchers,
        voucherData
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Voucher Added Successfully!!!');
            if (res.LoggerDetailsModel != null) {
            }
            this.fireNotification(res.LoggerDetailsModel);
          }

          this.voucherFilterForm.Skip = 0; //
          this.voucherDetails = [];
          this.getAllVoucherDetailsByFilter();
          this.hideAddEditVoucherForm();
          this.hideAddEditVoucherFormPopupLoading();
        },
        error => {
          this.getAllVoucherDetailsByFilter();
          this.hideAddEditVoucherForm();
          this.hideAddEditVoucherFormPopupLoading();
        }
      );
  }
  //#endregion

  //#region EditVoucher
  EditVoucher(data: any) {
    this.showAddEditVoucherFormPopupLoading();
    const voucherData: VoucherDetailsModel = {
      VoucherNo: data.VoucherNo,
      CurrencyId: data.CurrencyId,
      OfficeId: data.OfficeId,
      VoucherDate: new Date(
        new Date(data.VoucherDate).getFullYear(),
        new Date(data.VoucherDate).getMonth(),
        new Date(data.VoucherDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      ChequeNo: data.ChequeNo,
      JournalCode: data.JournalCode,
      VoucherTypeId: data.VoucherTypeId,
      Description: data.Description,
      ProjectId: data.ProjectId,
      BudgetLineId: data.BudgetLineId,
      FinancialYearId: data.FinancialYearId,

      CurrencyCode: '',
      FinancialYearName: '',
      JournalName: '',
      OfficeName: '',
      ReferenceNo: ''
    };

    this.accountservice
      .EditVoucher(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_EditVouchers,
        voucherData
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Voucher Updated Successfully!!!');
            this.fireNotification(res.LoggerDetailsModel);
          }
          this.getAllVoucherDetailsByFilter();
          this.hideAddEditVoucherForm();
          this.hideAddEditVoucherFormPopupLoading();
        },
        error => {
          this.getAllVoucherDetailsByFilter();
          this.hideAddEditVoucherForm();
          this.hideAddEditVoucherFormPopupLoading();
        }
      );
  }
  //#endregion

  //#region "onVoucherSelectionChanged"
  onVoucherSelectionChanged(data: any) {
    if (data != null) {
      if (data.VoucherNo !== 0) {
        this.selectedVoucherData = data; // property binding
        if (this.financialYearArr != null) {
          localStorage.setItem(
            'SelectedFinancialYearId',
            this.financialYearArr[0].FinancialYearId
          );
        }

        localStorage.setItem(
          'SelectedVoucherNumber',
          this.selectedVoucherData.VoucherNo
        );
        localStorage.setItem(
          'SelectedVoucherCurrency',
          this.selectedVoucherData.CurrencyId
        );
        localStorage.setItem(
          'SelectedOfficeId',
          this.selectedVoucherData.OfficeId
        );

        this.selectedVoucherData != null
          ? (this.transactionChild.transactionFlag = true)
          : (this.transactionChild.transactionFlag = false);

        this.transactionChild.GetAllVoucherTransactionDetail(
          this.selectedVoucherData.VoucherNo
        );
      } else {
        // lazy loading
        this.voucherFilterForm.OfficesList != null
          ? this.getAllVoucherDetailsByFilter()
          // tslint:disable-next-line:no-unused-expression
          : null;
      }
    }
  }
  //#endregion

  //#region "onVoucherFormSubmit"
  onVoucherFormSubmit(data: VoucherDetailsModel) {
    if (data != null) {
      data.VoucherNo == null || data.VoucherNo === 0
        ? this.AddVoucher(data)
        : this.EditVoucher(data);
    }
  }
  //#endregion

  //#region "onShowEditVoucherForm"
  onShowEditVoucherForm() {
    // tslint:disable-next-line:curly
    if (this.projectBudgetLineArr != null)
      this.budgetLineFilteredList = this.projectBudgetLineArr.filter(
        x => x.ProjectId === this.selectedVoucherData.ProjectId
      );
    this.voucherDetailsForm = this.selectedVoucherData;
    this.showAddEditVoucherForm();
  }
  //#endregion

  //#region "onShowAddVoucherForm"
  onShowAddVoucherForm() {
    this.initForm();
    this.showAddEditVoucherForm();
  }
  //#endregion

  //#region "onFieldDataChanged"
  onFieldDataChanged(event: any) {
    if (event.value != null && event.dataField === 'ProjectId') {
      this.budgetLineFilteredList = [];
      this.budgetLineFilteredList = this.projectBudgetLineArr.filter(
        x => x.ProjectId === event.value
      );
    }
  }
  //#endregion

  //#region "show/hide"
  showAddEditVoucherForm() {
    this.addEditVoucherFormPopupVisible = true;
  }

  hideAddEditVoucherForm() {
    this.addEditVoucherFormPopupVisible = false;
  }

  showAddEditVoucherFormPopupLoading() {
    this.addEditVoucherFormPopupLoading = true;
  }
  hideAddEditVoucherFormPopupLoading() {
    this.addEditVoucherFormPopupLoading = false;
  }

  //#endregion

  //#region Vouchers Document details popup trigger
  selectedReferenceNo(e) {
    if (e != null) {
      this.voucherNoMain = e.VoucherNo;
      localStorage.setItem('SelectedVoucherNumber', e.VoucherNo);
      this.GetVoucherDocumentList();
      this.popupVisibleDocument = true;
    }
  }
  //#endregion

  //#region  Document Functions
  onCancelVoucherDocument() {
    this.popupVisibleDocument = false;
  }
  addDocument() {
    this.popupVisibleDocument = true;
  }

  cancelDeleteVoucher() {
    this.popupVisible = false;
  }

  getfilename(docpath) {
    return this._DomSanitizer.bypassSecurityTrustResourceUrl(docpath);
  }

  filePath(data) {
    return this._DomSanitizer.bypassSecurityTrustResourceUrl(data);
  }

  // Event Fire on image Selection
  onImageSelect(event: any) {
    const file: File = event.value[0];
    const myReader: FileReader = new FileReader();
    myReader.readAsDataURL(file);
    myReader.onloadend = e => {
      this.imageURL = myReader.result;
    };
  }

  // Add Document with file uploader
  onFormSubmit(data: any) {
    this.addNewDocument.DocumentFilePath = this.imageURL;
    data.VoucherNo = localStorage.getItem('SelectedVoucherNumber');
    this.AddVoucherDocument(data);
  }

  GetVoucherDocumentList() {
    this.accountservice
      .GetVoucherDocumentDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetVoucherDocumentDetail,
        this.voucherNoMain
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.voucherDocumentDetails = [];
            data.data.VoucherDocumentDetailList.forEach(element => {
              this.voucherDocumentDetails.push(element);
            });
          }

          if (this.voucherDocumentDetails.length > 0) {
            this.defaultDoc = this._DomSanitizer.bypassSecurityTrustResourceUrl(
              this.setting.getDocUrl() +
                this.voucherDocumentDetails[
                  this.voucherDocumentDetails.length - 1
                ].DocumentGUID
            );
          } else {
            this.defaultDoc = this._DomSanitizer.bypassSecurityTrustResourceUrl(
              this.setting.getDocUrl() + 'nodoc.pdf'
            );
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
  }

  // Add New Voucher Document
  AddVoucherDocument(data) {
    this.accountservice
      .AddVoucherDocument(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_AddVouchersDocument,
        data
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Document Added Successfully!!!');
          }
          this.GetVoucherDocumentList();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
    this.cancelDeleteVoucher();
  }

  docPreview() {
    this.docPopupVisible = true;
  }

  selectDoc(e) {
    this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + e.value
    );
    this.popupVisibleDocument = true;
  }

  //#endregion

  //#region "onFieldVoucheFilterChanged"
  onFieldVoucheFilterChanged(e) {
    if (e != null) {
      this.voucherFilterForm = {
        Date:
          e.Date != null
            ? new Date(
                new Date(e.Date).getFullYear(),
                new Date(e.Date).getMonth(),
                new Date(e.Date).getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds()
              )
            : null,
        OfficesList: e.OfficesList,
        Skip: 0
      };

      this.voucherDetails = [];
      this.voucherDetailsList = [];

      this.voucherFilterForm.OfficesList != null
        ? this.getAllVoucherDetailsByFilter()
        // tslint:disable-next-line:no-unused-expression
        : null;
    }
  }
  //#endregion

  //#region "show / hide"
  showVoucherLoading() {
    this.voucherLoading = true;
  }
  hideVoucherLoading() {
    this.voucherLoading = false;
  }
  //#endregion

  //#region
  fireNotification(model) {
    model.CreatedDate = new Date();
    model.NotificationPath = './accounts/vouchers';
    this.commonService.sendMessage(model);
  }
  //#endregion

  //#region "Voucher List 20 record at a time"


  onVoucherItemRendered(e) {}

  //#region  getAllVoucherDetailsByFilter
  getVoucherDetailByVoucherNo() {
    this.showVoucherLoading();
    this.accountservice
      .GetDetailByVoucherNo(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_GetVoucherDetailByVoucherNo,
        this.PurchaseVoucherNo
      )
      .subscribe(
        data => {
          const itemIndex = this.voucherDetails.findIndex(i => i.VoucherNo === 0);
          this.voucherDetails.splice(itemIndex, 1);

          if (data.data.VoucherDetailList != null) {
            data.data.VoucherDetailList.forEach(element => {
              this.voucherDetails.push({
                BudgetLineId: element.BudgetLineId,
                ChequeNo: element.ChequeNo,
                CurrencyCode: element.CurrencyCode,
                CurrencyId: element.CurrencyId,
                Description: element.Description,
                FinancialYearId: element.FinancialYearId,
                FinancialYearName: element.FinancialYearName,
                JournalCode: element.JournalCode,
                JournalName: element.JournalName,
                OfficeId: element.OfficeId,
                OfficeName: element.OfficeName,
                ProjectId: element.ProjectId,
                ReferenceNo: element.ReferenceNo,
                VoucherDate: new Date(
                  new Date(element.VoucherDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                VoucherNo: element.VoucherNo,
                VoucherTypeId: element.VoucherTypeId
              });
            });

            if (this.voucherDetails.length > 0 && this.voucherListTrack < 10) {
              this.transactionChild.transactionFlag = true; // child call
              this.onVoucherSelectionChanged(this.voucherDetails[0]);
            } else {
              this.selectedVoucherData = null;
              this.transactionChild.transactionFlag = false; // child call
            }

            this.voucherFilterForm.Skip = 0;
            this.voucherFilterForm.Skip = this.voucherDetails.length + 1;

            this.voucherDetails.push({
              BudgetLineId: 0,
              ChequeNo: '',
              CurrencyCode: '',
              CurrencyId: 0,
              Description: '',
              FinancialYearId: 0,
              FinancialYearName: '',
              JournalCode: '',
              JournalName: '',
              OfficeId: 0,
              OfficeName: '',
              ProjectId: 0,
              ReferenceNo: 'Show More',
              VoucherDate: null,
              VoucherNo: 0,
              VoucherTypeId: 0
            });

            // DataSource
            this.voucherDetailsList = this.voucherDetails;

            this.voucherDetailsList = new DataSource({
              store: this.voucherDetails,
              paginate: true,
              pageSize: 10
            });
          }
          this.hideVoucherLoading();
        },
        error => {
          this.hideVoucherLoading();
        }
      );
  }
  //#endregion

  onContentReady(e) {}
  //#endregion
}

//#region "Classes"
class VoucherDetailsModel {
  BudgetLineId: any;
  ChequeNo: any;
  CurrencyCode: any;
  CurrencyId: any;
  Description: any;
  FinancialYearId: any;
  FinancialYearName: any;
  JournalCode: any;
  JournalName: any;
  OfficeId: any;
  OfficeName: any;
  ProjectId: any;
  ReferenceNo: any;
  VoucherDate: any;
  VoucherNo: any;
  VoucherTypeId: any;
}

class VoucherFilterModel {
  Date: any;
  OfficesList: any[];
  Skip: number;
}
//#endregion
