import {
  Component,
  OnInit,
  Input,
  PipeTransform,
  Sanitizer
} from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { AccountsService } from '../../accounts.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../../shared/global';
import { DomSanitizer } from '@angular/platform-browser';
import { VouchersComponent } from '../vouchers.component';
import { OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';
import { AppSettingsService } from '../../../../service/app-settings.service';
import { CommonService } from '../../../../service/common.service';

export class SafePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {}

  public transform(url: string): any {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
}

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html'
})
export class DocumentComponent implements OnInit, OnDestroy {
  popupVisible = false;
  addNewDocument: any;
  voucherNumber: any;
  @Input() docpath: any;
  imageURL: any;
  filePathUrl: any;

  imageData = { Image: '' };
  windows: any;

  docPopupVisible = false;
  resetUploader = false;

  // loader
  addDocPopupLoading = false;

  selectedDropdown: any;

  // Get Data From Voucher
  @Input() VoucherNo: any;
  @Input() voucherDocumentDetails: any[];

  ngOnDestroy(): void {
    this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + 'nodoc.pdf'
    );
  }
  // dataSource: any[];

  constructor(
    private accountservice: AccountsService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private router: Router,
    private fb: FormBuilder,
    private commonservice: CommonService,
    private _DomSanitizer: DomSanitizer
  ) {
    this.voucherNumber = this.commonservice.voucherNumber;
    this.voucherDocumentDetails = [{ DocumentGUID: '', DocumentName: '' }];
    this.addNewDocument = {
      DocumentName: null,
      DocumentFilePath: null,
      DocumentDate: null
    };
    this.windows = window;
    this.docpath = _DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + 'nodoc.pdf'
    );
  }

  ngOnInit() {
    this.GetVoucherDocumentList();
  }

  addDocument() {
    this.addNewDocument = {
      DocumentName: null,
      DocumentFilePath: null,
      DocumentDate: null
    };
    this.popupVisible = true;
  }

  cancelDeleteVoucher() {
    this.popupVisible = false;
  }

  backToVouchers() {
    this.router.navigate(['../vouchers']);
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
        localStorage.getItem('SelectedVoucherNumber')
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
            this.selectedDropdown = this.voucherDocumentDetails[
              this.voucherDocumentDetails.length - 1
            ].DocumentGUID;
            // this.voucherDocumentDetails = this.voucherDocumentDetails[this.voucherDocumentDetails.length -1].DocumentGUID;
            this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
              this.setting.getDocUrl() +
                this.voucherDocumentDetails[
                  this.voucherDocumentDetails.length - 1
                ].DocumentGUID
            );
          } else {
            this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
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
  AddVoucherDocument(model) {
    this.showHideAddDocPopupLoading();
    this.accountservice
      .AddVoucherDocument(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_AddVouchersDocument,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Document Added Successfully!!!');
          }
          this.GetVoucherDocumentList();
          this.cancelDeleteVoucher();
          this.showHideAddDocPopupLoading();
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
          this.cancelDeleteVoucher();
          this.showHideAddDocPopupLoading();
        }
      );
  }

  docPreview() {
    this.docPopupVisible = true;
  }
  selectDoc(e) {
    this.filePathUrl = this.voucherDocumentDetails.filter(
      x => x.DocumentGUID === e.value
    )[0].DocumentName;
    // this.docpath = this._DomSanitizer.bypassSecurityTrustUrl(this.setting.getDocUrl() + e.value);
    // this.filePath = this._DomSanitizer.bypassSecurityTrustResourceUrl(e);
    this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + e.value
    );
  }

  showHideAddDocPopupLoading() {
    this.addDocPopupLoading = !this.addDocPopupLoading;
  }
}
