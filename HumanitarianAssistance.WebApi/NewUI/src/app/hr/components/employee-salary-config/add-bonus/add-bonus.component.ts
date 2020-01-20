import { Component, OnInit, EventEmitter } from '@angular/core';
import { AddSalaryConfigurationComponent } from '../add-salary-configuration/add-salary-configuration.component';
import { MatDialogRef } from '@angular/material';
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
    public dialogRef: MatDialogRef<AddSalaryConfigurationComponent>
  ) {
    this.bonusForm = this.fb.group({
      SalaryHead: ['', [Validators.required]],
      SalaryAmount: ['', [Validators.required]],
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
  onNoClick(): void {
    this.dialogRef.close();
  }
  //#endregion
  onFormSubmit(data: any) {}
}
