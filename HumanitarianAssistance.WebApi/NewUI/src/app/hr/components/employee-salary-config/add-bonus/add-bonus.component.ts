import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { AddSalaryConfigurationComponent } from '../add-salary-configuration/add-salary-configuration.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { EmployeeSalaryConfigService } from 'src/app/hr/services/employee-salary-config.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-add-bonus',
  templateUrl: './add-bonus.component.html',
  styleUrls: ['./add-bonus.component.scss']
})
export class AddBonusComponent implements OnInit {
  onAddBonusRefresh = new EventEmitter();
  bonusForm: FormGroup;
  isFormSubmitted = false;
  constructor(
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private salaryConfigService: EmployeeSalaryConfigService,
    private globalSharedService: GlobalSharedService,
    private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<AddSalaryConfigurationComponent>
  ) {
    this.bonusForm = this.fb.group({
      SalaryHead: ['', [Validators.required]],
      Amount: ['', [Validators.required, Validators.min(1)]],
      Description: ['', [Validators.required]]
    });
  }

  ngOnInit() {}

  //#region "Add Salary Configuration Refresh"
  AddBonusRefresh() {
    this.onAddBonusRefresh.emit();
  }
  //#endregion
  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }

  //#endregion
  onFormSubmit(data: any) {
    if (!this.bonusForm.valid) {
      this.toastr.warning('Please correct form errors and submit again');
      return;
    }
    this.commonLoader.showLoader();
    const model = {
      SalaryHead: this.bonusForm.value.SalaryHead,
      Amount: this.bonusForm.value.Amount,
      Description: this.bonusForm.value.Description,
      EmployeeId: this.data.EmployeeId,
      IsBonus: true
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
