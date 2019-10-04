import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';

@Component({
  selector: 'app-document-upload',
  templateUrl: './document-upload.component.html',
  styleUrls: ['./document-upload.component.scss']
})
export class DocumentUploadComponent implements OnInit {
  actions: TableActionsModel
  constructor() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: true,
      },
      subitems: {}

    }
  }
  documentHeaders$ = of(['Name', 'Type', 'Uploaded On', 'Uploaded By']);
  documentsList$ = of([
    { name: 'Document name', type: 'invoice', uploadedon: '25 April', uploadedby: 'User' },
    { name: 'Document name', type: 'invoice', uploadedon: '25 April', uploadedby: 'User' },
    { name: 'Document name', type: 'invoice', uploadedon: '25 April', uploadedby: 'User' },
    { name: 'Document name', type: 'invoice', uploadedon: '25 April', uploadedby: 'User' }
  ]);
  ngOnInit() {
  }

}
