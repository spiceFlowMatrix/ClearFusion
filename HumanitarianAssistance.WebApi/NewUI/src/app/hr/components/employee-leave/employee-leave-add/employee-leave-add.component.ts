import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { ToastrService } from 'ngx-toastr';
import { HrLeaveService } from 'src/app/hr/services/hr-leave.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-employee-leave-add',
  templateUrl: './employee-leave-add.component.html',
  styleUrls: ['./employee-leave-add.component.scss']
})
export class EmployeeLeaveAddComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<EmployeeLeaveAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private fb: FormBuilder,
    private toastr: ToastrService, private hrLeaveService: HrLeaveService,
    private commonLoader: CommonLoaderService) { }

  isFormSubmitted = false;
  applyLeaveForm: FormGroup;

  ngOnInit() {
    this.onFormInIt();
  }

  onFormInIt() {
    this.applyLeaveForm = this.fb.group({
      'EmployeeId': [this.data.EmployeeId],
      'LeaveType': [{value: this.data.LeaveType, disabled: true}],
      'LeaveReasonId': [this.data.LeaveReasonId],
      'LeaveDate': [{'begin': null, 'end': null},
                     Validators.required],
      'BalanceLeave': [{value: this.data.HourBalance, disabled: true}],
      'LeaveApplied': [null, [Validators.required, Validators.min(1), Validators.max(this.data.HourBalance)]],
      'Remarks': [null, [Validators.required]]
    });
  }

  applyLeave() {
    if (!this.applyLeaveForm.valid) {
      this.toastr.warning('Please correct errors in form and submit again');
      return;
    }
    this.isFormSubmitted = true;

   const FromDate = new Date(
      new Date(this.applyLeaveForm.value.LeaveDate.begin).getFullYear(),
      new Date(this.applyLeaveForm.value.LeaveDate.begin).getMonth(),
      new Date(this.applyLeaveForm.value.LeaveDate.begin).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
    const ToDate = new Date(
      new Date(this.applyLeaveForm.value.LeaveDate.end).getFullYear(),
      new Date(this.applyLeaveForm.value.LeaveDate.end).getMonth(),
      new Date(this.applyLeaveForm.value.LeaveDate.end).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );

    // const difference = ((ToDate.getTime() - FromDate.getTime()) / (1000 * 3600 * 24));

    // if ((this.applyLeaveForm.value.BalanceLeave - difference) < 0) {
    //   this.toastr.warning('Applied leave exceeds Balance leave');
    //   this.isFormSubmitted = false;
    //   return;
    // }

       const model = {
        LeaveReasonId: this.applyLeaveForm.value.LeaveReasonId,
        Remarks: this.applyLeaveForm.value.Remarks,
        LeaveReasonName: this.applyLeaveForm.value.LeaveType,
        BlanceLeave: this.applyLeaveForm.value.BalanceLeave,
        LeaveApplied: this.applyLeaveForm.value.LeaveApplied,
        EmployeeId: this.applyLeaveForm.value.EmployeeId,
        FromDate: FromDate,
        ToDate: ToDate
      };

    this.hrLeaveService.addEmployeeLeave(model).subscribe(x => {
      if (x) {
        this.toastr.success('Added Successfully');
        this.isFormSubmitted = false;
        this.closeDialog();
      }
    }, error => {
      this.toastr.warning(error);
      this.isFormSubmitted = false;
    });
  }

  // dateInput(event) {
  //   debugger;
  //   const model = {
  //     EmployeeId: this.data.EmployeeId,

  //   }
  //   this.commonLoader.showLoader();
  //   this.hrLeaveService.addEmployeeLeave(model).subscribe(x => {
  //     if (x) {
  //       this.commonLoader.hideLoader();
  //     } else {
  //       this.commonLoader.showLoader();
  //       this.toastr.warning('Could not retrieve working hour for the selected month');
  //     }
  //   }, error => {
  //     this.toastr.warning(error);
  //     this.commonLoader.hideLoader();
  //   });
  // }

  onAppliedHourChange(event) {
    this.applyLeaveForm.controls['BalanceLeave'].setValue(this.data.HourBalance - (+event.target.value));
    this.applyLeaveForm.updateValueAndValidity();
  }

  closeDialog() {
    this.dialogRef.close();
  }

  myFilter = (d: Date): boolean => {
    const day = d.getDay();
    // Prevent Saturday and Sunday from being selected.
    return day !== 0 && day !== 6;
  }
}
