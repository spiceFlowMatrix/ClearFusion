import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { VoucherService } from '../../voucher.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IAddVoucherModel } from '../../models/voucher.model';
import { IResponseData } from '../../models/status-code.model';
import { VoucherType } from 'src/app/shared/enum';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { of } from 'rxjs/internal/observable/of';

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
  voucherNo: any;
  errorMessage: string;

  constructor(private router: Router, private routeActive: ActivatedRoute,
    public fb: FormBuilder,
    public voucherService: VoucherService,
    public toastr: ToastrService) {

    this.routeActive.url.subscribe(params => {
      this.voucherNo = +params[0].path;
    });
  }

  ngOnInit() {
    this.addVoucherForm = this.fb.group({
      VoucherNo: [null],
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

    forkJoin([
      this.getVoucherTypeList(),
      this.getAllCurrency(),
      this.getOfficeList(),
      this.getJournalList(),
    ])
      .subscribe(result => {
        this.subscribeGetVoucherTypeList(of(result[0]));
        this.subscribeGetAllCurrency(of(result[1]));
        this.subscribeGetOfficeList(of(result[2]));
        this.subscribeGetJournalList(of(result[3]));
      });

    if (this.voucherNo && this.voucherNo !== NaN) {
      this.getVoucherByVoucherNo();
    }
  }

  //#endregion

  cancelButtonClicked() {
    window.history.back();
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
        TimezoneOffset: data.TimezoneOffset,
        OperationalType: VoucherType['Direct Voucher']
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
            this.errorMessage = response.message;
          }
        },
        (error) => {
          this.isFormSubmitted = false;
          this.errorMessage = error;
        }
      );
    }
  }
  //#endregion

  //#region "onAddVoucher"
  onAddVoucher(data): void {

    const voucherData: IAddVoucherModel = {
      VoucherNo: this.voucherNo !== NaN ? this.voucherNo : 0,
      CurrencyId: data.value.CurrencyId,
      VoucherDate: StaticUtilities.getLocalDate(data.value.VoucherDate) ,
      ChequeNo: data.value.ChequeNo,
      Description: data.value.Description,
      JournalCode: data.value.JournalCode,
      VoucherTypeId: data.value.VoucherTypeId,
      OfficeId: data.value.OfficeId,
      TimezoneOffset: new Date().getTimezoneOffset()
    };

    if (this.voucherNo && this.voucherNo !== NaN) {
      this.editVoucherDetail(voucherData);
    } else {
      this.addVoucher(voucherData);
    }
  }
  //#endregion

  editVoucherDetail(data) {
    if (this.addVoucherForm.valid) {

      this.isFormSubmitted = true;
      this.voucherService.EditVoucherDetailById(data).subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            this.isFormSubmitted = false;
            this.cancelButtonClicked();
            this.toastr.success('Voucher updated successfully');
            this.cancelButtonClicked();
          } else {
            this.isFormSubmitted = false;
            this.errorMessage = response.message;
          }
        },
        (error) => {
          this.isFormSubmitted = false;
          this.errorMessage = error;
        }
      );
    }
  }

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
    return this.voucherService.GetCurrencyList();
  }

  subscribeGetAllCurrency(res) {
    res.subscribe(x => {
      this.currencyList = [];
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
    return this.voucherService.GetOfficeList();
  }
  //#endregion

  subscribeGetOfficeList(res) {
    res.subscribe(
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
      error => { }
    );
  }

  //#region "getJournalList"
  getJournalList() {
    return this.voucherService.GetJournalList();
  }
  //#endregion

  subscribeGetJournalList(res) {
    res.subscribe(
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
      error => { }
    );
  }

  //#region "getVoucherTypeList"
  getVoucherTypeList() {
    return this.voucherService.GetVoucherTypeList();
  }
  //#endregion

  subscribeGetVoucherTypeList(res) {
    console.log(res, 'vt');
    res.subscribe(
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
      error => { }
    );
  }

  //#region "getVoucherByVoucherNo"
  getVoucherByVoucherNo() {
    this.errorMessage = '';
    this.voucherService.GetVoucherDetailById(this.voucherNo).subscribe(
      (response: IResponseData) => {
        this.voucherTypeList = [];
        if (response.statusCode === 200 && response.data !== null) {
          console.log(response.data);
          this.addVoucherForm.patchValue({
            VoucherNo: this.voucherNo,
            CurrencyId: response.data.CurrencyId,
            ChequeNo: response.data.ChequeNo,
            VoucherDate: StaticUtilities.setLocalDate(response.data.VoucherDate),
            Description: response.data.Description,
            JournalCode: response.data.JournalCode,
            VoucherTypeId: response.data.VoucherTypeId,
            OfficeId: response.data.OfficeId,
            ProjectId: response.data.ProjectId,
          });
        } else {
          this.errorMessage = response.message;
        }
      },
      error => {
        this.toastr.warning(error);
        this.errorMessage = error;
      }
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
