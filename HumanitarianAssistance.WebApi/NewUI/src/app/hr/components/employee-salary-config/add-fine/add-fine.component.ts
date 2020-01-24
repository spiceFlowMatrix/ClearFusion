import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { EmployeeSalaryConfigService } from 'src/app/hr/services/employee-salary-config.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-add-fine',
  templateUrl: './add-fine.component.html',
  styleUrls: ['./add-fine.component.scss']
})
export class AddFineComponent implements OnInit {
  onAddFineRefresh = new EventEmitter();
  fineForm: FormGroup;
  isFormSubmitted = false;
  constructor(
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private salaryConfigService: EmployeeSalaryConfigService,
    private globalSharedService: GlobalSharedService,
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddFineComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.fineForm = this.fb.group({
      SalaryHead: ['', [Validators.required]],
      Amount: ['', [Validators.required, Validators.min(1)]],
      Description: ['', [Validators.required]]
    });
   }

  ngOnInit() {
  }

//#region "Add Fine Refresh"
AddFineRefresh() {
  this.onAddFineRefresh.emit();
}
//#endregion
//#region "onCancelPopup"
onCancelPopup(): void {
  this.dialogRef.close();
}
//#endregion
onNoClick(): void {
  this.dialogRef.close();
}

onFormSubmit(data: any) {
  if (!this.fineForm.valid) {
    this.toastr.warning('Please correct form errors and submit again');
    return;
  }
  this.commonLoader.showLoader();
  const model = {
    SalaryHead: this.fineForm.value.SalaryHead,
    Amount: this.fineForm.value.Amount,
    Description: this.fineForm.value.Description,
    EmployeeId: this.data.EmployeeId,
    IsBonus: false,
    Month: this.data.SelectedMonth
  };
  this.salaryConfigService.saveBonusFineSalaryHead(model).subscribe(x => {
    if (x) {
      this.toastr.success('Added Successfully');
      this.commonLoader.hideLoader();
      this.onCancelPopup();
    } else {
      this.toastr.warning('Please try again');
      this.commonLoader.hideLoader();
    }
  }, error => {
    this.toastr.warning(error);
    this.commonLoader.hideLoader();
  });
}
}
