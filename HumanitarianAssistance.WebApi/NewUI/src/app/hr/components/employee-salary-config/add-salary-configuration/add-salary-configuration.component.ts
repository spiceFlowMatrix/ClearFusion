import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { EmployeeSalaryConfigService } from 'src/app/hr/services/employee-salary-config.service';
import { Observable, of } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';

@Component({
  selector: 'app-add-salary-configuration',
  templateUrl: './add-salary-configuration.component.html',
  styleUrls: ['./add-salary-configuration.component.scss']
})
export class AddSalaryConfigurationComponent implements OnInit {
  onAddSalaryConfigurationRefresh = new EventEmitter();
  currencyList$: Observable<IDropDownModel[]>;
  salaryConfigForm: FormGroup;
  fixedSalaryForm: FormGroup;
  isFormSubmitted = false;
  errMsg = null;
  constructor(
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private toastr: ToastrService,
    private salaryConfigService: EmployeeSalaryConfigService,
    private globalSharedService: GlobalSharedService,
    public dialogRef: MatDialogRef<AddSalaryConfigurationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    if (!this.data.ForAllEmployees) {
      this.salaryConfigForm = this.fb.group({
        PayrollId: [this.data.PayrollId],
        CurrencyId: [this.data.CurrencyId, [Validators.required]],
        ActiveSalary: [this.data.MonthlyAmount, [Validators.required, Validators.min(1)]]
      });
    } else {  // for Setting Fixed Salary from Employee Listing Page
      this.fixedSalaryForm = this.fb.group({
        FixedSalary: [null],
        CapacityBuilding: [null],
        Security: [null]
      });
    }
  }

  ngOnInit() {
    this.getCurrencyList();
  }
  //#region "Add Salary Configuration Refresh"
  AddSalaryConfigurationRefresh() {
    this.onAddSalaryConfigurationRefresh.emit();
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
  //#region "get currency  List"
  getCurrencyList() {
    this.commonLoader.showLoader();
    this.salaryConfigService
      .GetCurrencyList()
      .subscribe(
        x => {
          this.commonLoader.hideLoader();
          if (x.data.CurrencyList.length > 0) {
            this.currencyList$ = of(
              x.data.CurrencyList.map(y => {
                return {
                  value: y.CurrencyId,
                  name: y.CurrencyName
                } as IDropDownModel;
              })
            );
          }
        },
        () => {
          this.commonLoader.hideLoader();
        }
      );
  }
  //#endregion
  onFormSubmit(data: any) {
    if (this.salaryConfigForm.valid) {
      const model = {
        PayrollId: this.salaryConfigForm.value.PayrollId,
        CurrencyId: this.salaryConfigForm.value.CurrencyId,
        ActiveSalary: this.salaryConfigForm.value.ActiveSalary,
        EmployeeId: this.data.EmployeeId
      };
      if (this.salaryConfigForm.value.PayrollId === 0) {
        this.addBasicSalaryAndCurrency(model);
      } else {
        this.editBasicSalaryAndCurrency(model);
      }
    } else {
      this.toastr.warning('Please correct form errors and submit again');
    }
  }

  addBasicSalaryAndCurrency(model: any) {
      this.commonLoader.showLoader();
      this.salaryConfigService.saveBasicSalary(model).subscribe(x =>  {
        if (x) {
          this.commonLoader.hideLoader();
          this.onCancelPopup();
        } else {
          this.commonLoader.hideLoader();
          this.toastr.warning('Please try again');
        }
      }, error => {
        this.toastr.warning(error);
      });
  }

  editBasicSalaryAndCurrency(model: any) {
      this.commonLoader.showLoader();
      this.salaryConfigService.editBasicSalary(model).subscribe(x =>  {
        if (x) {
          this.commonLoader.hideLoader();
          this.onCancelPopup();
        } else {
          this.commonLoader.hideLoader();
          this.toastr.warning('Please try again');
        }
      }, error => {
        this.toastr.warning(error);
      });
  }

  setFixedSalaryForAllEmployees(value) {
    if ((value.FixedSalary === null) && (value.CapacityBuilding === null) && (value.Security === null)) {
      this.errMsg = 'Please enter one of the values to be updated!';
      return;
    }
  }
}
