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
import { StoreService } from '../../../store.service';
import { AppSettingsService } from '../../../../../service/app-settings.service';
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
  }

  addDocument() {
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
      if (x.StatusCode === 200) {

        if (x.data.SignedUrl !== undefined && x.data.SignedUrl !== null) {
          this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(x.data.SignedUrl);
        }
      }
    });
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
      this.cancelDeletePurchase();
      this.showHideAddPurchaseDocPopupLoading();
      this.getPurchasesDocumentList(x.data.DocumentFileId);
    });
  }
  //#endregion
}
