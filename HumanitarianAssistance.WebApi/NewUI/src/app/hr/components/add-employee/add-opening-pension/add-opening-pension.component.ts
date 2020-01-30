import {
  IEmployeePensionDetails,
  IEmployeePensionListModel
} from './../../../models/employee-detail.model';
import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ReplaySubject, forkJoin, Observable, of } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { AddEmployeeService } from 'src/app/hr/services/add-employee.service';

@Component({
  selector: 'app-add-opening-pension',
  templateUrl: './add-opening-pension.component.html',
  styleUrls: ['./add-opening-pension.component.scss']
})
export class AddOpeningPensionComponent implements OnInit {
  employeePensionDetailForm: FormGroup;
  isFormSubmitted = false;
  pensionId: number;
  pensionDetails: any;
  currencyList$: Observable<IDropDownModel[]>;
  onPensionDetailListRefresh = new EventEmitter();
  onUpdatePensionDetailListRefresh = new EventEmitter();
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService,
    private employeeService: AddEmployeeService,
    public dialogRef: MatDialogRef<AddOpeningPensionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.employeePensionDetailForm = this.fb.group({
      Id: [null],
      Currency: ['', [Validators.required]],
      Amount: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.pensionId = this.data.pensionId;
    this.pensionDetails = this.data.item;
    forkJoin([this.getCurrencyList()])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeCurrencyList(result[0]);
      });
    if (this.pensionDetails !== undefined) {
      this.employeePensionDetailForm.patchValue({
        Id: this.pensionDetails.Id,
        Currency: this.pensionDetails.Currency,
        Amount: this.pensionDetails.Amount
      });
    }
  }
  //#region "Get all currency list"
  getCurrencyList() {
    this.commonLoader.showLoader();
    return this.employeeService.GetCurrencyList();
  }
  subscribeCurrencyList(response: any) {
    this.commonLoader.hideLoader();
    this.currencyList$ = of(
      response.data.CurrencyList.map(y => {
        return {
          value: y.CurrencyId,
          name: y.CurrencyName
        };
      })
    );
  }
  //#endregion

  onFormSubmit(data: IEmployeePensionListModel) {
    this.isFormSubmitted = true;
    if (this.employeePensionDetailForm.valid) {

      if(this.pensionDetails === undefined)
      {
      data.Id = this.data.pensionId;
      this.AddPensionDetailListRefresh(data);
      } else {
        data.Id = this.pensionDetails.Id;
        this.UpdatePensionDetailListRefresh(data);
      }
    }
  }
  //#region "On historical list refresh"
  AddPensionDetailListRefresh(data: IEmployeePensionListModel) {
    this.onPensionDetailListRefresh.emit(data);
    this.isFormSubmitted = false;
    this.onNoClick();
  }
  UpdatePensionDetailListRefresh(data: IEmployeePensionListModel) {
    this.onUpdatePensionDetailListRefresh.emit(data);
    this.isFormSubmitted = false;
    this.onNoClick();
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
