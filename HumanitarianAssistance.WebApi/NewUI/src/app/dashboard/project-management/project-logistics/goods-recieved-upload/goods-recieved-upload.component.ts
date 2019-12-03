import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { FileSourceEntityTypes } from 'src/app/shared/enum';
import { takeUntil } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-goods-recieved-upload',
  templateUrl: './goods-recieved-upload.component.html',
  styleUrls: ['./goods-recieved-upload.component.scss']
})
export class GoodsRecievedUploadComponent implements OnInit {

  attachment = [];
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(private dialogRef: MatDialogRef<GoodsRecievedUploadComponent>,
    public toastr: ToastrService,
    private globalSharedService: GlobalSharedService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commonLoader: CommonLoaderService) { }

  ngOnInit() {
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

  uploadGoodsRecieved() {
    if (this.attachment.length === 0) {
      this.toastr.warning('Please upload attachment!');
    }
    this.globalSharedService
            .uploadFile(FileSourceEntityTypes.GoodsRecievedDocument, this.data.RequestId, this.attachment[0][0])
            .pipe(takeUntil(this.destroyed$))
            .subscribe(y => {
                this.toastr.success('Upload Successful');
                this.dialogRef.close({data: 'Success'});
            });
  }
}
