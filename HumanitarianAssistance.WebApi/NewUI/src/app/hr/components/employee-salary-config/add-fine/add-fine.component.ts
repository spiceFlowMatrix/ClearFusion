import { Component, OnInit, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef } from '@angular/material';
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
  ) {
    this.fineForm = this.fb.group({
      SalaryHead: ['', [Validators.required]],
      SalaryAmount: ['', [Validators.required]],
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
onFormSubmit(data: any) {}
}
