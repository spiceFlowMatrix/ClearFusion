import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { ToastrService } from 'ngx-toastr';
import { HrLeaveService } from 'src/app/hr/services/hr-leave.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-employee-leave-add',
  templateUrl: './employee-leave-add.component.html',
  styleUrls: ['./employee-leave-add.component.scss']
})
export class EmployeeLeaveAddComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<EmployeeLeaveAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private fb: FormBuilder,
    private toastr: ToastrService, private hrLeaveService: HrLeaveService,
    private commonLoader: CommonLoaderService, private datePipe: DatePipe) { }

  isFormSubmitted = false;
  applyLeaveForm: FormGroup;
  disabledDates: any[] = [];
  errorMessage = '';

  ngOnInit() {
    this.onFormInIt();
    this.getAppliedLeaveDates();
  }

  onFormInIt() {
    this.applyLeaveForm = this.fb.group({
      'EmployeeId': [this.data.EmployeeId],
      'LeaveType': [{value: this.data.LeaveType, disabled: true}],
      'LeaveReasonId': [this.data.LeaveReasonId],
      'LeaveDate': [{'begin': null, 'end': null},
                     Validators.required],
      'BalanceLeave': [{value: this.data.HourBalance, disabled: true}, [Validators.min(0)]],
      'LeaveApplied': [{value: null, disabled: true}, [Validators.required, Validators.min(1), Validators.max(this.data.HourBalance)]],
      // 'Remarks': [null, [Validators.required]]
    });
  }

  applyLeave() {
    if (!this.applyLeaveForm.valid) {
      this.toastr.warning('Please correct errors in form and submit again');
      return;
    } else if (this.applyLeaveForm.getRawValue().LeaveApplied === 0) {
      this.toastr.warning('Applied hours can not be 0');
      return;
    }
    this.isFormSubmitted = true;

   const FromDate = new Date(
      new Date(this.applyLeaveForm.getRawValue().LeaveDate.begin).getFullYear(),
      new Date(this.applyLeaveForm.getRawValue().LeaveDate.begin).getMonth(),
      new Date(this.applyLeaveForm.getRawValue().LeaveDate.begin).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
    const ToDate = new Date(
      new Date(this.applyLeaveForm.getRawValue().LeaveDate.end).getFullYear(),
      new Date(this.applyLeaveForm.getRawValue().LeaveDate.end).getMonth(),
      new Date(this.applyLeaveForm.getRawValue().LeaveDate.end).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );

       const model = {
        LeaveReasonId: this.applyLeaveForm.getRawValue().LeaveReasonId,
        Remarks: '',
        LeaveReasonName: this.applyLeaveForm.getRawValue().LeaveType,
        BlanceLeave: this.applyLeaveForm.getRawValue().BalanceLeave,
        LeaveApplied: this.applyLeaveForm.getRawValue().LeaveApplied,
        EmployeeId: this.applyLeaveForm.getRawValue().EmployeeId,
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

  closeDialog() {
    this.dialogRef.close();
  }

  myFilter = (d: Date): boolean => {
    const day = d.getDay();
    // Prevent Saturday and Sunday from being selected.
    return day !== 0 && day !== 6;
  }

  DateSelectionChanged(event) {
    const model = {
      StartDate: StaticUtilities.getLocalDate(event.value.begin),
      EndDate: StaticUtilities.getLocalDate(event.value.end),
      EmployeeId: this.data.EmployeeId
    };

    this.hrLeaveService.getAppliedLeaveHours(model).subscribe(x => {
      if (x && x.AppliedHours !== undefined && x.AppliedHours !== null) {
        this.errorMessage = '';
        this.applyLeaveForm.controls['LeaveApplied'].setValue(x.AppliedHours);
        const value = this.data.HourBalance - x.AppliedHours;
        this.applyLeaveForm.controls['BalanceLeave'].setValue(value);

        if (x.AppliedHours > this.data.HourBalance) {
          this.errorMessage = 'Max Applied hour unit allowed is ' + this.data.HourBalance;
        } else {
          this.errorMessage = '';
        }
      } else {
        this.errorMessage = 'Something went wrong, Please try again';
      }
    }, error => {
      this.applyLeaveForm.controls['LeaveApplied'].setValue(null);
      this.applyLeaveForm.controls['BalanceLeave'].setValue(this.data.HourBalance);
      this.errorMessage = error;
    });
  }

  getAppliedLeaveDates() {
    const model = {
      LeaveReasonId: this.data.LeaveReasonId,
      EmployeeId: this.data.EmployeeId
    };

    this.hrLeaveService.getAppliedLeaveDates(model).subscribe(x => {
      if (x && x.LeaveDates) {
        this.disabledDates = x.LeaveDates;
      } else {
        this.errorMessage = 'Something went wrong, Please try again';
      }
    }, error => {
      this.errorMessage = error;
    });
  }

  // disableDates = (d: Date): boolean => {
  //   const dates = [];

  //   const date = this.datePipe.transform(d, 'M/d/yyyy');

  //   for (let i = 0; i < this.disabledDates.length; i++) {
  //     if (this.disabledDates[i].ToDate === this.disabledDates[i].FromDate) {
  //       if (date === this.data.FromDate) {
  //         dates.push(date);
  //       }
  //     } else {
  //       if (date >= new Date(this.disabledDates[i].FromDate) && date <= new Date(this.disabledDates[i].ToDate)) {
  //         dates.push(date);
  //       }
  //     }
  //   }

  //   const highlightDate = dates
  //     .map(strDate => new Date(strDate))
  //     .some(d => d.getDate() === date.getDate() && d.getMonth() === date.getMonth() && d.getFullYear() === date.getFullYear());
  //   return highlightDate ? true : false;
  // }
}
