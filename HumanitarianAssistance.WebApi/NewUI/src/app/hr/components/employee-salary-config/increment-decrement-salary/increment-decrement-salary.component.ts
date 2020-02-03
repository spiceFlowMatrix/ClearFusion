import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MatTabChangeEvent, MAT_DIALOG_DATA } from '@angular/material';
import { EmployeeSalaryConfigService } from 'src/app/hr/services/employee-salary-config.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-increment-decrement-salary',
  templateUrl: './increment-decrement-salary.component.html',
  styleUrls: ['./increment-decrement-salary.component.scss']
})
export class IncrementDecrementSalaryComponent implements OnInit {

  incrementDecrementForm: FormGroup;
  error = null;
  selectedTab = 0;
  constructor(private fb: FormBuilder,
    public dialogRef: MatDialogRef<IncrementDecrementSalaryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private salaryConfigService: EmployeeSalaryConfigService,
    private commonLoader: CommonLoaderService,
    private toastr: ToastrService) {
    this.incrementDecrementForm = this.fb.group({
      Percent: [null],
      Amount: [null]
    });
   }

  ngOnInit() {
  }

  incrementDecrementFormSubmit() {
    this.error = null;
    if ((this.incrementDecrementForm.value.Percent == null) && (this.incrementDecrementForm.value.Amount == null)) {
      this.error = 'Please enter Percentage or Amount for Reconfiguring Salary';
      return;
    }
    this.commonLoader.showLoader();
    const model = {
      EmployeeIds: this.data.SelectedEmployees.map(x => {
        return x.EmployeeId;
      }),
      Percentage: this.incrementDecrementForm.value.Percent,
      Amount: this.incrementDecrementForm.value.Amount,
      ReconfigureType: this.selectedTab
    };
    this.salaryConfigService.incrementDecrementSalary(model).subscribe(res => {
      if (res) {
        this.commonLoader.hideLoader();
        this.toastr.success('Salary updated Successfully!');
        this.dialogRef.close();
      } else {
        this.commonLoader.hideLoader();
        this.error = 'Something Went Wrong!';
      }
    }, err => {
      this.commonLoader.hideLoader();
      this.error = err;
    });
  }

  onCancelPopup(): void {
    this.dialogRef.close();
  }

  tabChanged(tabChangeEvent: MatTabChangeEvent) {
    this.error = null;
    this.selectedTab = tabChangeEvent.index;
    this.incrementDecrementForm.setValue({
      Percent: null,
      Amount: null
    });
    this.incrementDecrementForm.controls.Percent.enable();
    this.incrementDecrementForm.controls.Amount.enable();
  }


  PercentChanged() {
    if (this.incrementDecrementForm.controls.Percent.value !== null) {
      this.incrementDecrementForm.controls.Amount.disable();
    } else {
      this.incrementDecrementForm.controls.Amount.enable();
    }
  }

  AmountChanged() {
    if (this.incrementDecrementForm.controls.Amount.value !== null) {
      this.incrementDecrementForm.controls.Percent.disable();
    } else {
      this.incrementDecrementForm.controls.Percent.enable();
    }
  }
}
