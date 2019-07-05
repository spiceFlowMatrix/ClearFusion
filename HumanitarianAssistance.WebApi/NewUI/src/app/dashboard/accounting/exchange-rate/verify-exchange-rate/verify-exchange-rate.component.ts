import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonLoaderService } from '../../../../shared/common-loader/common-loader.service';

@Component({
  selector: 'app-verify-exchange-rate',
  templateUrl: './verify-exchange-rate.component.html',
  styleUrls: ['./verify-exchange-rate.component.scss']
})
export class VerifyExchangeRateComponent implements OnInit {


  constructor( public dialogRef: MatDialogRef<VerifyExchangeRateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, public commonLoader: CommonLoaderService) { }

  ngOnInit() {
  }

  onCloseVerifyDialog(verified: boolean) {
    this.dialogRef.close(verified);
  }

}
