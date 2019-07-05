import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { IcandidateDetailDataSource,
  IAttendaneGroupModel, IHiringRequestDetailModel, IEmployeeContractList, CandidateDetailModel } from '../models/hiring-requests-model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HiringRequestsService } from '../hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { EmployeeType } from 'src/app/shared/enum';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-candidate-detail-dialog',
  templateUrl: './edit-candidate-detail-dialog.component.html',
  styleUrls: ['./edit-candidate-detail-dialog.component.scss']
})
export class EditCandidateDetailDialogComponent implements OnInit {
// variables
projectId: number;
employeeId: number;
attendanceGroupList: IAttendaneGroupModel[] = [];
hiringRequestDetail: IHiringRequestDetailModel;
employeeContractist: IEmployeeContractList[] = [];
candidateDetailForm: FormGroup;
// loader
editCandidateDetailLoader = false;
// input /output
@Output() employeeTypeDetial = new EventEmitter<any>();

// enum
employeeType = {
  Active: EmployeeType.Active,
  Candidate: EmployeeType.Candidate,
  Terminated: EmployeeType.Terminated
};
  constructor(public dialogRef: MatDialogRef<EditCandidateDetailDialogComponent>,
    private fb: FormBuilder,
    public toastr: ToastrService,
    public hiringRequestService: HiringRequestsService
    , @Inject(MAT_DIALOG_DATA) public data: IcandidateDetailDataSource) {
      this.projectId = data.ProjectId;
      this.attendanceGroupList = data.AttendanceGroupList,
      this.employeeId = data.EmployeeId,
      this.hiringRequestDetail = data.HiringRequestDetail,
      this.employeeContractist = data.EmployeeContractist;
    }

  ngOnInit() {
    this.initForm();
  }

   //#region "initForm"
   initForm() {
    this.candidateDetailForm = this.fb.group({
      EmployeeContractTypeId: ['', Validators.required],
      Id: ['', [Validators.required]],
      HiredOn: ['', Validators.required],
    });
  }
  //#endregion

  //#region "onFormSubmit"
  onFormSubmit(data: any) {
  if (this.candidateDetailForm.valid ) {
  this.EditCandidateDetail(data);
}
  }
  //#endregion
  EditCandidateDetail(data: any) {
    this.editCandidateDetailLoader = true;
    const employeeDetail: any = {
      EmployeeContractTypeId: data.EmployeeContractTypeId,
      AttendanceGroupId: data.Id,
      EmployeeId: this.employeeId,
      HiredOn: StaticUtilities.getLocalDate(data.HiredOn),
      OfficeId: this.hiringRequestDetail.OfficeId,
      EmployeeTypeId: this.employeeType.Active
    };
    this.hiringRequestService
    .EditCandidateDetail(employeeDetail)
    .subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.employeeTypeDetial.emit(employeeDetail.EmployeeTypeId);
        } else {
           this.toastr.error(response.message);
        }
        this.editCandidateDetailLoader = false;

        this.onCancelPopup();
      },
      error => {
         this.toastr.error('Someting went wrong');
        this.editCandidateDetailLoader = false;
      }
    );
  }
  onCancelPopup() {
    this.dialogRef.close();
  }
}
