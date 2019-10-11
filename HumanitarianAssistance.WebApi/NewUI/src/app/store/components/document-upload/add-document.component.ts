import { Component, OnInit } from '@angular/core';
import { IDropDownModel } from '../../models/purchase';
import { PurchaseService } from '../../services/purchase.service';
import { Observable } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-document',
  templateUrl: './add-document.component.html',
  styleUrls: ['./add-document.component.scss']
})
export class AddDocumentComponent implements OnInit {
  documentTypes$: Observable<IDropDownModel[]>;
  addDocumentForm: FormGroup;

  constructor(private _purchaseService: PurchaseService, private _fb: FormBuilder) {
    this.addDocumentForm = this._fb.group({
      'file': [null, Validators.required],
      'documentType': [null, Validators.required]
    });
  }

  ngOnInit() {
    this.documentTypes$ = this._purchaseService.getPurchaseDocumentTypes();
  }

  submitAddDocument() {
    debugger;
    this.addDocumentForm;

  }

  openInput(){
    document.getElementById("fileInput").click();
}
}
