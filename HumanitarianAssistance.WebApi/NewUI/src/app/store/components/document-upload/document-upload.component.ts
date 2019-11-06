import { Component, OnInit, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { of, Observable } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { IPurchasedFiles } from '../../models/purchase';

@Component({
  selector: 'app-document-upload',
  templateUrl: './document-upload.component.html',
  styleUrls: ['./document-upload.component.scss']
})
export class DocumentUploadComponent implements OnInit, OnChanges {
  actions: TableActionsModel;

  @Input() purchasedDocumentFiles: IPurchasedFiles[];
  @Input() showDownloadButton: boolean;
  @Output() documentButtonClicked =  new EventEmitter<any>();
  @Input() hideColums$: Observable<{ headers?: string[], items?: string[] }>;

  documentHeaders$: Observable<any>;
  documentsList$: Observable<any>;

  constructor() {

  }
  ngOnInit() {
    this.documentHeaders$ = of(['Id', 'Name', 'Type', 'Uploaded On', 'Uploaded By']);
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: this.showDownloadButton ? true : false,
      },
      subitems: {}

    };
  }

  ngOnChanges() {
     this.documentsList$ = of(this.purchasedDocumentFiles);
  }

  documentButtonClick(event) {
      this.documentButtonClicked.emit(event);
  }
}
