import { Component, OnInit, Inject } from '@angular/core';
import { TenderProposalDocumentType } from 'src/app/shared/enum';
import { Observable, of, ReplaySubject } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { takeUntil } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-submit-tender-document',
  templateUrl: './submit-tender-document.component.html',
  styleUrls: ['./submit-tender-document.component.scss']
})
export class SubmitTenderDocumentComponent implements OnInit {

  documentType: {value: number; name: string}[] = [];
  documentType$: Observable<IDropDownModel[]>;
  DocumentTypeValue: FormControl;
  attachment = [];
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private dialogRef: MatDialogRef<SubmitTenderDocumentComponent>,
    private toastr: ToastrService, private globalSharedService: GlobalSharedService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commonLoader: CommonLoaderService) { }

  ngOnInit() {
    this.DocumentTypeValue = new FormControl('', Validators.required);
    this.getDocumentDropdown();
  }

  getDocumentDropdown() {
    for (const n in TenderProposalDocumentType) {
      if (typeof TenderProposalDocumentType[n] === 'number') {
        this.documentType.push({value: <any>TenderProposalDocumentType[n], name: n});
      }
    }
    this.documentType$ = of(this.documentType);
  }

  openInput() {
    document.getElementById('fileInput').click();
  }

  fileChange(file: any) {
    this.attachment = [];
    this.attachment.push(file);
  }

  closeDialog() {
    this.dialogRef.close({data: null});
  }

  uploadTenderDocument() {
    if (!this.DocumentTypeValue.valid) {
      this.toastr.warning('Please select Document Type!');
      return;
    }
    if (this.attachment.length === 0) {
      this.toastr.warning('Please select Document to Upload!');
      return;
    }
    this.commonLoader.showLoader();
    this.globalSharedService
            .uploadFile(this.DocumentTypeValue.value, this.data.RequestId, this.attachment[0][0])
            .pipe(takeUntil(this.destroyed$))
            .subscribe(y => {
                this.commonLoader.hideLoader();
                this.toastr.success('Upload Successful!');
                this.dialogRef.close({data: 'Success'});
            });
  }
}
