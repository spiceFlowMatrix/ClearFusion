import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ContractsService } from '../service/contracts.service';
import { ToastrService } from 'ngx-toastr';
import { ContractDetailsModel } from '../model/contract-details.model';


@Component({
  selector: 'app-contract-approval',
  templateUrl: './contract-approval.component.html',
  styleUrls: ['./contract-approval.component.scss']
})
export class ContractApprovalComponent implements OnInit {
  onListRefresh = new EventEmitter();
  contractDetailsModel: ContractDetailsModel = {};
  constructor(@Inject(MAT_DIALOG_DATA) public data: DataSources, private appurl: AppUrlService, private contractService: ContractsService,
  private toastr: ToastrService, public dialogRef: MatDialogRef<ContractApprovalComponent>) { }

  ngOnInit() {
  }

  confirmAction() {
    // this.confirmApproveContractFlag = true;
    this.onListRefresh.emit('true');
    // tslint:disable-next-line:max-line-length
  }


  onCancelPopup(): void {
    this.dialogRef.close();
  }

}
interface DataSources {
  data: any;
}
