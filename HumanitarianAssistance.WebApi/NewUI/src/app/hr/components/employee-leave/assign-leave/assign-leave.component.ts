import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { HrLeaveService } from 'src/app/hr/services/hr-leave.service';

@Component({
  selector: 'app-assign-leave',
  templateUrl: './assign-leave.component.html',
  styleUrls: ['./assign-leave.component.scss']
})
export class AssignLeaveComponent implements OnInit {

  assignLeaveForm: FormGroup;
  isFormSubmitted = false;
  financialYearDropdown: any [];
  leaveReasonTypeDropdown: any[];
  constructor(private dialogRef: MatDialogRef<AssignLeaveComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private fb: FormBuilder,
    private toastr: ToastrService, private hrLeaveService: HrLeaveService) { }

  ngOnInit() {
    this.onFormInIt();
    this.GetFinancialYearDropdown();
    this.GetLeaveReasonTypeDropdown();
  }

  onFormInIt() {
    this.assignLeaveForm = this.fb.group({
      'EmployeeId': [this.data.EmployeeId],
      'FinancialYearId': [null, [Validators.required]],
      'LeaveReasonId': [this.data.LeaveReasonId, [Validators.required]],
      'Units': [ {value: null, disabled: true}, Validators.required],
      'AssignedUnit': [null, [Validators.required, Validators.min(1)]],
      // 'LeaveApplied': [null, [Validators.required, Validators.min(1), Validators.max(this.data.HourBalance)]],
      'Remarks': [null]
    });
  }

  //#region "Get All Financial Year"
  GetFinancialYearDropdown() {
    this.hrLeaveService
      .getFinancialYearList()
      .subscribe(
        data => {
          this.financialYearDropdown = [];

          if (
            data.data.CurrentFinancialYearList != null &&
            data.data.CurrentFinancialYearList.length > 0
          ) {
            data.data.CurrentFinancialYearList.forEach(element => {
              this.financialYearDropdown.push({
                StartDate: new Date(
                  new Date(element.StartDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                EndDate: new Date(
                  new Date(element.EndDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                FinancialYearId: element.FinancialYearId,
                FinancialYearName: element.FinancialYearName
              });
            });

            if (this.financialYearDropdown.length > 0) {
              this.assignLeaveForm.controls['FinancialYearId'].setValue(this.financialYearDropdown[0].FinancialYearId);
            }

          } else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong!');
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  //#region "Get All Leave Reason Type"
  GetLeaveReasonTypeDropdown() {
    this.hrLeaveService
      .getAllLeaveReasonType()
      .subscribe(
        data => {
          this.leaveReasonTypeDropdown = [];

          if (
            data.data.LeaveReasonList != null &&
            data.data.LeaveReasonList.length > 0
          ) {
            data.data.LeaveReasonList.forEach(element => {
              this.leaveReasonTypeDropdown.push(element);
            });
          } else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong!');
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  //#endregion

  selectedLeaveReason(id) {
    const index = this.leaveReasonTypeDropdown.findIndex(x => x.LeaveReasonId === id);

    if (index !== -1) {
      this.assignLeaveForm.controls['Units'].setValue(this.leaveReasonTypeDropdown[index].Unit);
      this.assignLeaveForm.controls['AssignedUnit'].setValidators(Validators.max(this.leaveReasonTypeDropdown[index].Unit));
      this.assignLeaveForm.controls['AssignedUnit'].updateValueAndValidity();
    }
  }

  assignLeave() {
    if (!this.assignLeaveForm.valid) {
      this.toastr.warning('Please correct form errors and submit again');
      return;
    }

    this.isFormSubmitted = true;

    const model = {
      LeaveId: 0,
      EmployeeId: this.assignLeaveForm.value.EmployeeId,
      LeaveReasonId: this.assignLeaveForm.value.LeaveReasonId,
      Unit: this.assignLeaveForm.getRawValue().Units,
      AssignUnit: this.assignLeaveForm.value.AssignedUnit,
      FinancialYearId: this.assignLeaveForm.value.FinancialYearId,
      Description: this.assignLeaveForm.value.Remarks
    };

    this.hrLeaveService.assignLeave(model)
    .subscribe(
      data => {
        if (data) {
          this.toastr.success('Added Successfully!!!');
          this.isFormSubmitted = false;
          this.closeDialog();
        }
      },
      error => {
        this.isFormSubmitted = false;
        this.toastr.warning(error);
      }
    );
  }

  closeDialog() {
    this.dialogRef.close();
  }

}
