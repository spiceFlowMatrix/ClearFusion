import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IAddVoucherModel } from '../models/voucher.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { VoucherService } from '../voucher.service';
import { IResponseData } from '../models/status-code.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-voucher-add',
  templateUrl: './voucher-add.component.html',
  styleUrls: ['./voucher-add.component.scss']
})

export class VoucherAddComponent implements OnInit {

  //#region "variables"
   addVoucherForm: FormGroup;
   addVoucherLoader = false;

  onListRefresh = new EventEmitter();

  //#endregion

  constructor(
    public dialogRef: MatDialogRef<VoucherAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DataSources,
    public fb: FormBuilder,
    public voucherService: VoucherService,
    public toastr: ToastrService
  ) {}

    ngOnInit() {
      this.initForm();
    }

  //#region "initForm"
    initForm() {
      this.addVoucherForm = this.fb.group({
        CurrencyId: [''],
        ChequeNo: [''],
        VoucherDate: ['', [Validators.required]],
        Description: ['', [Validators.required]],
        JournalCode: ['', [Validators.required]],
        VoucherTypeId: ['', [Validators.required]],
        OfficeId: ['', [Validators.required]],
        ProjectId: [''],
        // BudgetLineId: ['', [Validators.required]]

      });
    }
  //#endregion


  //#region "addVoucher"
  addVoucher(data: IAddVoucherModel) {
    if (this.addVoucherForm.valid) {

      this.addVoucherLoader = true;

      const voucherData: IAddVoucherModel = {
        VoucherNo: data.VoucherNo,
        CurrencyId: data.CurrencyId,
        VoucherDate: this.setDateTime(data.VoucherDate),
        ChequeNo: data.ChequeNo,
        Description: data.Description,
        JournalCode: data.JournalCode,
        VoucherTypeId: data.VoucherTypeId,
        OfficeId: data.OfficeId,
        TimezoneOffset: data.TimezoneOffset
        // ProjectId: data.ProjectId,
        // BudgetLineId: data.value.BudgetLineId,
        // FinancialYearId: data.value.FinancialYearId, // calculate on backend
      };

      this.voucherService.AddVoucher(voucherData).subscribe(
        (response: IResponseData) => {

          if (response.statusCode === 200) {
            this.onCancelPopup();
            this.voucherListRefresh();
            this.toastr.success('Voucher is created successfully');
          } else {
            this.toastr.error(response.message);
          }
          this.addVoucherLoader = false;
        },
        (error) => {
          this.toastr.error('Someting went wrong');
          this.addVoucherLoader = false;
        }
      );
    }
  }
  //#endregion

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  //#region "onAddVoucher"
  onAddVoucher(data): void {

    const voucherData: IAddVoucherModel = {
      VoucherNo: data.value.VoucherNo,
      CurrencyId: data.value.CurrencyId,
      VoucherDate: data.value.VoucherDate,
      ChequeNo: data.value.ChequeNo,
      Description: data.value.Description,
      JournalCode: data.value.JournalCode,
      VoucherTypeId: data.value.VoucherTypeId,
      OfficeId: data.value.OfficeId,
      TimezoneOffset: new Date().getTimezoneOffset()
      // ProjectId: data.value.ProjectId,
      // BudgetLineId: data.value.BudgetLineId,
      // FinancialYearId: data.value.FinancialYearId, // calculate on backend
    };
    this.addVoucher(voucherData);
  }
  //#endregion

//#region "onListRefresh"
  voucherListRefresh() {
    this.onListRefresh.emit();
  }
  //#endregion

  //#region "setDateTime"
  setDateTime(data): any {
    return new Date(
      new Date(data).getFullYear(),
      new Date(data).getMonth(),
      new Date(data).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
  }
//#endregion

}

class DataSources {
  data: any;
  journalList: any;
  currencyList: any;
  officeList: any;
  projectList: any;
  budgetLineList: any;
  voucherTypeList: any;
}
