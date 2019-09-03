import {
  Component,
  OnInit,
  PipeTransform,
  Pipe,
  OnDestroy,
  Input,
  OnChanges
} from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { GLOBAL } from '../../../../../shared/global';
import { StoreService } from '../../../store.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormBuilder, NG_ASYNC_VALIDATORS } from '@angular/forms';
import { AppSettingsService } from '../../../../../service/app-settings.service';
import { CommonService } from '../../../../../service/common.service';
import { UploadModel } from '../../../../../shared/FileManagement/file-management-model';
import { DocumentFileTypes, FileSourceEntityTypes } from '../../../../../shared/enums';
import { FileManagementService } from '../../../../../shared/FileManagement/file-management.service';

export class SafePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }

  public transform(url: string): any {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
}

@Component({
  selector: 'app-purchases-document',
  templateUrl: './purchases-document.component.html',
  styleUrls: ['./purchases-document.component.css']
})
export class PurchasesDocumentComponent implements OnInit, OnDestroy, OnChanges {
  // @Input() documentPath: any;
  @Input() defaultObj: any;

  popupVisible = false;
  addNewDocument: any;
  filePathUrl: any;
  docpath: any;
  windows: any;

  selectedDropdown: any;
  purchaseDocumentDataSource: any[];

  purchasedocPopupVisible = false;
  resetUploader = false;
  documentFile: any;
  selectedInvoiceDocumentId: number;

  // loader
  addPurchaseDocPopupLoading = false;

  imageURL: any;

  constructor(
    private storeService: StoreService,
    private setting: AppSettingsService,
    private fileManagementService: FileManagementService,
    private _DomSanitizer: DomSanitizer
  ) { }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    // this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
    //   this.setting.getDocUrl() + 'nodoc.pdf'
    // );
  }

  ngOnChanges() {
    // TODO: Refresh (Parent call)
    // this.docpath = (this.defaultObj == null || this.defaultObj === undefined) ? '' :
    // this._DomSanitizer.bypassSecurityTrustResourceUrl(this.defaultObj.InvoiceFileName);

    this.getPurchasesDocumentList(this.defaultObj.InvoiceDocumentId);
  }

  //#region "show / hide"
  showHideAddPurchaseDocPopupLoading() {
    this.addPurchaseDocPopupLoading = !this.addPurchaseDocPopupLoading;
  }
  //#endregion

  selectDoc(e) {
    this.filePathUrl = this.purchaseDocumentDataSource.filter(
      x => x.DocumentGUID === e.value
    )[0].DocumentName;
    this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + e.value
    );
  }

  // Event Fire on image Selection
  onImageSelect(event: any) {
    const file: File = event.value[0];

    this.documentFile = file;
    // const myReader: FileReader = new FileReader();
    // myReader.readAsDataURL(file);
    // myReader.onloadend = e => {
    //   this.imageURL = myReader.result;
    // };
  }

  addDocument() {
    debugger
    this.addNewDocument = {
      DocumentName: null,
      DocumentFilePath: null,
      DocumentDate: null
    };

    this.popupVisible = true;
  }

  cancelDeletePurchase() {
    this.popupVisible = false;
  }

  // Add Document with file uploader
  onFormSubmit() {
    const data = {
      PurchaseId: this.defaultObj.PurchaseId,
      FileDocumentId: this.defaultObj.InvoiceDocumentId
    };
    this.AddVoucherDocument(data);
  }

  // #region "getPurchasesDocumentList"
  getPurchasesDocumentList(DocumentFileId) {

    this.docpath = null;

    this.fileManagementService.getSignedURLByDocumenFileId(DocumentFileId).subscribe(x => {
      debugger;
      if (x.StatusCode === 200) {

        if (x.data.SignedUrl !== undefined && x.data.SignedUrl !== null) {
          this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(x.data.SignedUrl);
        }
      }
    });

    // this.storeService
    //   .GetAllPurchaseInvoices(
    //     this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllPurchaseInvoices,
    //     DocumentFileId
    //   )
    //   .subscribe(
    //     data => {
    //       if (
    //         data.StatusCode === 200 &&
    //         data.data.UpdatePurchaseInvoiceModel != null
    //       ) {
    //         this.docpath =
    //           data.data.UpdatePurchaseInvoiceModel.Invoice === ''
    //             ? this._DomSanitizer.bypassSecurityTrustResourceUrl(
    //               this.setting.getDocUrl() + 'nodoc.pdf'
    //             )
    //             : this._DomSanitizer.bypassSecurityTrustResourceUrl(
    //               data.data.UpdatePurchaseInvoiceModel.Invoice
    //             );
    //         this.defaultObj.PurchaseId =
    //           data.data.UpdatePurchaseInvoiceModel.PurchaseId;
    //       }
    //     },
    //     error => { }
    //   );
  }
  //#endregion

  //#region  "Add New Purchase Document"
  AddVoucherDocument(dataSource) {
    this.showHideAddPurchaseDocPopupLoading();

    const dataModel: UploadModel = {
      DocumentTypeId: DocumentFileTypes.PurchaseInvoice,
      PageId: FileSourceEntityTypes.StorePurchase,
      EntityId: dataSource.PurchaseId,
      File: this.documentFile,
      DocumentFileId: dataSource.FileDocumentId
    };

    this.fileManagementService.uploadFile(dataModel).subscribe(x => {
     // this.getPurchasesDocumentList(dataSource.PurchaseId);
      this.cancelDeletePurchase();
      this.showHideAddPurchaseDocPopupLoading();
    });
  }
  //#endregion
}
