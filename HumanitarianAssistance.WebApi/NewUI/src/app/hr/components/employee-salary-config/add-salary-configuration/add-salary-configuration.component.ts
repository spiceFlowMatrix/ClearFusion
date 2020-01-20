import { Component, OnInit, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef } from '@angular/material';
import { FormBuilder, FormGroup } from '@angular/forms';
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
  isFormSubmitted = false;
  constructor(
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private toastr: ToastrService,
    private salaryConfigService: EmployeeSalaryConfigService,
    private globalSharedService: GlobalSharedService,
    public dialogRef: MatDialogRef<AddSalaryConfigurationComponent>,
  ) { }

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
onFormSubmit(data: any)
{

}
}
