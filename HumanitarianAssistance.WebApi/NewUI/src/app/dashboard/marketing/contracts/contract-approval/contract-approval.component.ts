import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ContractsService } from '../service/contracts.service';
import { ToastrService } from 'ngx-toastr';
import { ContractDetailsModel } from '../model/contract-details.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';


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
    this.onListRefresh.emit('true');
  }


  onCancelPopup(): void {
    this.dialogRef.close();
  }

}
interface DataSources {
  data: any;
}
