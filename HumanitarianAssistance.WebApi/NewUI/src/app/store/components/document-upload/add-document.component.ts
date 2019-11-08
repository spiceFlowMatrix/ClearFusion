import { Component, OnInit, Inject } from '@angular/core';
import { IDropDownModel } from '../../models/purchase';
import { PurchaseService } from '../../services/purchase.service';
import { Observable, of } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-document',
  templateUrl: './add-document.component.html',
  styleUrls: ['./add-document.component.scss']
})
export class AddDocumentComponent implements OnInit {
  documentTypes$: Observable<IDropDownModel[]>;
  addDocumentForm: FormGroup;

  constructor(private _purchaseService: PurchaseService, private _fb: FormBuilder,
    public _toastr: ToastrService, @Inject(MAT_DIALOG_DATA) public data: any,
    private _dialogRef: MatDialogRef<AddDocumentComponent>) {
    this.addDocumentForm = this._fb.group({
      'id': [0],
      'file': [null, Validators.required],
      'documentType': [null, Validators.required],
      'documentName': [null],
      'uploadDate': [new Date()]
    });

  }

  ngOnInit() {
    this.documentTypes$ = this._purchaseService.getPurchaseDocumentTypes();
  }

  openInput() {
    document.getElementById('fileInput').click();
  }

  fileChange(file: any) {
    this.addDocumentForm.controls['file'].patchValue(file);
  }

  submitAddDocument() {
    if (this.addDocumentForm.valid) {
      if (this.data.purchaseDocumentList.length > 0) {
        const index = this.data.purchaseDocumentList.findIndex(x => x.Filename === this.addDocumentForm.controls['file'].value[0].name);

        if (index >= 0) {
          this._toastr.warning('Filename already Exists. Please re-upload a document with different filename');
          return;
        }
      }

      let documentTypes: any[] = [];

       this.documentTypes$.subscribe(x => documentTypes = x);

      const documentName = documentTypes.filter(x => x.value === this.addDocumentForm.get('documentType').value)

      this.addDocumentForm.get('documentName').patchValue(documentName[0].name);
      this._dialogRef.close(this.addDocumentForm.value);
    } else {
      this._toastr.warning('Please correct errors in add document form and submit again');
    }
  }

  cancelButtonClicked() {
    this._dialogRef.close();
  }
}
