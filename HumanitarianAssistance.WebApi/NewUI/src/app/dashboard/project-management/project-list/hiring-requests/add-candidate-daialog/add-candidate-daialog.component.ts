import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {
  IHiringReuestDataSource,
  IEmployeeListModel,
  IHiringReuestCandidateModel
} from '../models/hiring-requests-model';
import { HiringRequestsService } from '../hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-candidate-daialog',
  templateUrl: './add-candidate-daialog.component.html',
  styleUrls: ['./add-candidate-daialog.component.scss']
})
export class AddCandidateDaialogComponent implements OnInit {
  // Input/Output
  selectedEmployeId = new EventEmitter<any>();

  // Model
  EmployeeList: IEmployeeListModel[] = [];
  EmployeeModel: IEmployeeListModel;
  candidateDetail: IHiringReuestCandidateModel;
  HiringRequestId: number;

  // Flag
  candidateloaderFlag = false;

  constructor(
    public dialogRef: MatDialogRef<AddCandidateDaialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IHiringReuestDataSource,
    public toastr: ToastrService,
    public hiringRequestService: HiringRequestsService
  ) {
    this.EmployeeList = data.EmployeeList;
    this.HiringRequestId = data.HiringRequestId;
  }

  ngOnInit() {
    this.initModel();
  }

  initModel() {
    this.EmployeeModel = {
      EmployeeId: null,
      EmployeeName: null
    };

    this.candidateDetail = {
      EmployeeID: null,
      HiringRequestId: null,
      CandidateId: null
    };
  }
  OnSelectionChange(event: any) {
    this.candidateloaderFlag = true;
    console.log(event);
    this.AddNewCandidate(event.value);
  }

  //#region "AddNewCandidate"
  AddNewCandidate(data: number) {
    if (data != null && data !== undefined) {
      const candidateDetail: IHiringReuestCandidateModel = {
        EmployeeID: data,
        HiringRequestId: this.HiringRequestId,
        ProjectId: this.data.ProjectId
      };
      this.hiringRequestService
        .AddHiringRequestCandidate(candidateDetail)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200) {
              this.toastr.success('Candidate added successfully');
              this.selectedEmployeId.emit(this.HiringRequestId);
              this.onCancelPopup();
            } else {
              this.toastr.error(response.message);
            }
            this.candidateloaderFlag = false;
          },
          error => {
            this.toastr.error('Someting went wrong');
            this.onCancelPopup();
            this.candidateloaderFlag = false;
          }
        );
    }
  }
  //#endregion

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion
}
