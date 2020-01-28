import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeSalaryConfigService } from 'src/app/hr/services/employee-salary-config.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-advance-recovery',
  templateUrl: './add-advance-recovery.component.html',
  styleUrls: ['./add-advance-recovery.component.scss']
})
export class AddAdvanceRecoveryComponent implements OnInit {

  advanceRecoveryForm: FormGroup;

  constructor(private fb: FormBuilder, private salaryConfigService: EmployeeSalaryConfigService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddAdvanceRecoveryComponent>) { }
    advanceData: any;
    isFormSubmitted =  false;

  ngOnInit() {
    this.advanceRecoveryForm = this.fb.group({
      'RecoveryAmount': [null, [Validators.required, Validators.min(1)]]
    });

    this.advanceData = {
      AdvanceId: null,
      RequestedAmount: 0,
      BalanceAmount: 0
    };
    this.getEmployeeAdvanceDetail();
  }

  onFormSubmit() {
    debugger;

    if (!this.advanceRecoveryForm.valid) {
      this.toastr.warning('Please correct form errors and submit again');
    }

    this.isFormSubmitted = true;
    const model = {
      Month: this.data.SelectedMonth,
      EmployeeId: this.data.EmployeeId,
      Amount: this.advanceRecoveryForm.value.RecoveryAmount,
      AdvanceId: this.advanceData.AdvanceId
    };

    this.salaryConfigService.addAdvanceRecovery(model).subscribe(x => {
      if (x) {
        this.isFormSubmitted = false;
        this.closeDialog();
      } else {
        this.isFormSubmitted = false;
        this.toastr.warning('Something went wrong. Please try again');
      }
    }, error => {
      this.isFormSubmitted = false;
      this.toastr.warning(error);
    });
  }

  getEmployeeAdvanceDetail() {
    this.salaryConfigService.getEmployeeAdvanceDetail(this.data.EmployeeId)
      .subscribe(x => {
        this.advanceData.AdvanceId = x.Advance.AdvanceId;
        this.advanceData.RequestedAmount = x.Advance.RequestedAmount;
        this.advanceData.BalanceAmount = x.Advance.BalanceAmount;

        this.advanceRecoveryForm.controls['RecoveryAmount'].setValidators([Validators.required, Validators.max(x.Advance.BalanceAmount)]);
        this.advanceRecoveryForm.updateValueAndValidity();
      });
  }

  closeDialog() {
    this.dialogRef.close();
  }
}
