import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { VoucherService } from '../../voucher.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IAddVoucherModel } from '../../models/voucher.model';
import { IResponseData } from '../../models/status-code.model';

@Component({
  selector: 'app-add-voucher',
  templateUrl: './add-voucher.component.html',
  styleUrls: ['./add-voucher.component.scss']
})
export class AddVoucherComponent implements OnInit {

  addVoucherForm: FormGroup;
  currencyList: any[];
  officeList: any[];
  journalList: any[];
  voucherTypeList: any[];
  isFormSubmitted = false;

  constructor(private router: Router, private routeActive: ActivatedRoute,
    public fb: FormBuilder,
    public voucherService: VoucherService,
    public toastr: ToastrService) { }

  ngOnInit() {
    this.addVoucherForm = this.fb.group({
      CurrencyId: [''],
      ChequeNo: [''],
      VoucherDate: [new Date(), [Validators.required]],
      Description: ['', [Validators.required]],
      JournalCode: ['', [Validators.required]],
      VoucherTypeId: ['', [Validators.required]],
      OfficeId: ['', [Validators.required]],
      ProjectId: [''],
      // BudgetLineId: ['', [Validators.required]]
    });

    this.getAllCurrency();
    this.getOfficeList();
    this.getJournalList();
    this.getVoucherTypeList();
  }

  //#endregion

  cancelButtonClicked() {
    this.router.navigate(['../../vouchers'], { relativeTo: this.routeActive });
  }

  //#region "addVoucher"
  addVoucher(data: IAddVoucherModel) {
    if (this.addVoucherForm.valid) {

      this.isFormSubmitted = true;

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
            this.isFormSubmitted = false;
            this.cancelButtonClicked();
            this.toastr.success('Voucher is created successfully');
          } else {
            this.isFormSubmitted = false;
            this.toastr.error(response.message);
          }
        },
        (error) => {
          this.isFormSubmitted = false;
          this.toastr.error('Someting went wrong');
        }
      );
    }
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

  getAllCurrency() {
    return this.voucherService.GetCurrencyList().subscribe(x => {
      this.currencyList= [];
      x.data.forEach(ele => {
        this.currencyList.push({
          CurrencyId: ele.CurrencyId,
          CurrencyName: ele.CurrencyCode + '-' + ele.CurrencyName
        });
      });
    });
  }

  //#region "getOfficeList"
  getOfficeList() {
    this.voucherService.GetOfficeList().subscribe(
      (response: IResponseData) => {
        this.officeList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.officeList.push({
              OfficeId: element.OfficeId,
              OfficeName: element.OfficeName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getJournalList"
 getJournalList() {
  this.voucherService.GetJournalList().subscribe(
    (response: IResponseData) => {
      this.journalList = [];
      if (response.statusCode === 200 && response.data !== null) {
        response.data.forEach(element => {
          this.journalList.push({
            JournalCode: element.JournalCode,
            JournalName: element.JournalName,
            JournalType: element.JournalType
          });
        });
      }
    },
    error => {}
  );
}
//#endregion

//#region "getVoucherTypeList"
getVoucherTypeList() {
  this.voucherService.GetVoucherTypeList().subscribe(
    (response: IResponseData) => {
      this.voucherTypeList = [];
      if (response.statusCode === 200 && response.data !== null) {
        response.data.forEach(element => {
          this.voucherTypeList.push({
            VoucherTypeId: element.VoucherTypeId,
            VoucherTypeName: element.VoucherTypeName
          });
        });
      }
    },
    error => {}
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
