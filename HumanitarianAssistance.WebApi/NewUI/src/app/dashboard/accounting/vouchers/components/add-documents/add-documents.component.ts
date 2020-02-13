import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { Observable } from 'rxjs/Observable';
import { FileSourceEntityTypes } from 'src/app/shared/enum';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-add-documents',
  templateUrl: './add-documents.component.html',
  styleUrls: ['./add-documents.component.scss']
})
export class AddDocumentsComponent implements OnInit {

  addDocumentForm: FormGroup;
  isFormSubmitted = false;

  constructor(private _fb: FormBuilder,
    public _toastr: ToastrService, @Inject(MAT_DIALOG_DATA) public data: any,
    private _dialogRef: MatDialogRef<AddDocumentsComponent>, private globalSharedService: GlobalSharedService,
    ) {
    this.addDocumentForm = this._fb.group({
      'id': [0],
      'file': [null, Validators.required],
      'documentName': [null],
      'uploadDate': [new Date()]
    });

  }

  ngOnInit() {
  }

  openInput() {
    document.getElementById('fileInput').click();
  }

  fileChange(file: any) {
    this.addDocumentForm.controls['file'].patchValue(file);
  }

  submitAddDocument() {
    if (this.addDocumentForm.valid) {
      this.isFormSubmitted = true;
            this.globalSharedService
              .uploadFile(FileSourceEntityTypes.Voucher, this.data.voucherNo, this.addDocumentForm.controls['file'].value[0])
              .subscribe(x => {
                this.isFormSubmitted = false;
                this._dialogRef.close(this.addDocumentForm.value);
              }, error => {
                this.isFormSubmitted = false;
              });
    } else {
      this._toastr.warning('Please correct errors in add document form and submit again');
    }
  }

  cancelButtonClicked() {
    this._dialogRef.close();
  }

}
