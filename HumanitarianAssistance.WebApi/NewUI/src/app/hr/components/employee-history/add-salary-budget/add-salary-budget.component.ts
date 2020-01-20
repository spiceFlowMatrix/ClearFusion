import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { Observable, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { EmployeeHistoryService } from 'src/app/hr/services/employee-history.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { IEmployeeSalaryBudgetDetails } from 'src/app/hr/models/employee-history-models';

@Component({
  selector: 'app-add-salary-budget',
  templateUrl: './add-salary-budget.component.html',
  styleUrls: ['./add-salary-budget.component.scss']
})
export class AddSalaryBudgetComponent implements OnInit {
  salaryBudgetForm: FormGroup;
  isFormSubmitted = false;
  employeeId: number;
  PreviousYearsList$: Observable<IDropDownModel[]>;
  currencyList$: Observable<IDropDownModel[]>;
  onSalaryBudgetDetailListRefresh = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private toastr: ToastrService,
    private employeeHistoryService: EmployeeHistoryService,
    private globalSharedService: GlobalSharedService,
    public dialogRef: MatDialogRef<AddSalaryBudgetComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.salaryBudgetForm = this.fb.group({
      EmployeeID: [''],
      BudgetDisbursed: ['', [Validators.required]],
      CurrencyId: ['', [Validators.required]],
      SalaryBudget: ['', [Validators.required]],
      Year: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.employeeId = this.data.employeeId;
    this.salaryBudgetForm.controls['EmployeeID'].setValue(this.employeeId);
    this.getPreviousYearsList();
    this.getCurrencyList();
  }
 //#region "Get all previous years list for ExperienceInYears dropdown"
 getPreviousYearsList() {
  this.PreviousYearsList$ = this.globalSharedService.getPreviousYearsList(40);
}
//#endregion
  //#region "get currency  List"
  getCurrencyList() {
    this.commonLoader.showLoader();
    this.employeeHistoryService
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

  onFormSubmit(data: IEmployeeSalaryBudgetDetails) {
    if (this.salaryBudgetForm.valid) {
      this.isFormSubmitted = true;
      this.employeeHistoryService.addSalaryBudgetDetail(data).subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.toastr.success('Success');
            this.isFormSubmitted = false;
            this.dialogRef.close();
            this.AddSalaryBudgetDetailListRefresh();
          } else {
            this.toastr.warning(x.Message);
            this.isFormSubmitted = false;
          }
        },
        error => {
          this.toastr.warning(error);
          this.isFormSubmitted = false;
        }
      );
    }
  }
    //#region "Add Salary Budget Detail List Refresh"
    AddSalaryBudgetDetailListRefresh() {
      this.onSalaryBudgetDetailListRefresh.emit();
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
}
