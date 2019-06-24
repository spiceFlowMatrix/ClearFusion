import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap';
import { GLOBAL } from '../../../shared/global';
import { StoreService } from '../../store/store.service';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-payment-types',
  templateUrl: './payment-types.component.html',
  styleUrls: ['./payment-types.component.css']
})
export class PaymentTypesComponent implements OnInit {
  paymentTypes: IPaymentTypes;
  paymentTypesDataSource: IPaymentTypes[] = [];
  showPaymentPopUp: boolean;
  paymentLoader: boolean;
  inputLevelAccounts: any[];
  isEditingAllowed = false;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private commonservice: CommonService,
    private storeService: StoreService
  ) {}

  ngOnInit() {
    this.initForm();
    this.getAllPaymentTypes();
    this.GetInputAccounts();
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.PaymentTypes
    );
  }

  initForm() {
    this.paymentTypes = {
      ChartOfAccountNewId: 0,
      AccountName: '',
      Id: 0,
      Name: ''
    };
  }

  // Get all PaymentTypes Details
  getAllPaymentTypes() {
    this.storeService
      .GetAllDetailsByURL(
        this.setting.getBaseUrl() + GLOBAL.API_Store_GetAllPaymentTypes
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200 && data.data.PaymentTypesList.length > 0) {

            this.paymentTypesDataSource = [];

            data.data.PaymentTypesList.forEach(element => {
              this.paymentTypesDataSource.push({
                ChartOfAccountNewId: element.ChartOfAccountNewId,
                AccountName: element.AccountName,
                Id: element.PaymentId,
                Name: element.Name
              });
            });
          }

          this.commonservice.setLoader(false);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
          this.commonservice.setLoader(false);
        }
      );
  }

  onAddPaymentType(data: any) {
    this.showLoader();

    const model: IPaymentTypes = {
      ChartOfAccountNewId: data.AccountId,
      AccountName: data.AccountName,
      Name: data.Name,
      Id: 0
    };

    const paymentNameExists = this.paymentTypesDataSource.find(
      x => x.Name === model.Name
    );

    if (paymentNameExists === undefined) {
      this.storeService
        .AddEditByModel(
          this.setting.getBaseUrl() + GLOBAL.API_Store_AddPaymentTypes,
          model
        )
        .subscribe(
          res => {
            if (res.StatusCode === 200) {
              // Success
              this.toastr.success('Payment Type Added Successfully!!!');
              this.getAllPaymentTypes();
              this.hideLoader();
              this.HidePopup();
            } else if (res.StatusCode === 900) {
              this.toastr.error('Something went wrong!!!');
              this.hideLoader();
            } else {
              this.toastr.error('Error!!!');
            }
            this.hideLoader();
          },
          error => {
            this.toastr.error('Something went wrong!!!');
            this.hideLoader();
          }
        );
    } else {
      this.toastr.error('Same Name Payment Type Already Exists!!!');
      this.hideLoader();
    }
  }

  EditPaymentTypes(model) {
    this.showLoader();

    const PaymentTypes: any = {
      ChartOfAccountNewId: model.key.ChartOfAccountNewId,
      AccountName: model.key.AccountName,
      PaymentId: model.key.Id,
      Name: model.key.Name
    };

    this.storeService
      .AddEditByModel(
        this.setting.getBaseUrl() + GLOBAL.API_Store_EditPaymentTypes,
        PaymentTypes
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Store Payment Type Updated Successfully!!!');
            this.getAllPaymentTypes();
            this.hideLoader();
          } else if (data.StatusCode === 900) {
            this.toastr.error('Something went wrong!!!');
            this.hideLoader();
          } else {
            this.toastr.error('Error!!!');
          }
          this.hideLoader();
        },
        error => {
          // error message
          this.hideLoader();
        }
      );
  }

  DeletePaymentTypes(model) {
    const PaymentId = model.key.Id;

    this.storeService
      .DeletePaymentTypes(
        this.setting.getBaseUrl() + GLOBAL.API_Store_DeletePaymentTypes,
        PaymentId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('Payment Type Deleted Successfully!!!');
            this.getAllPaymentTypes();
          } else {
            this.toastr.error('Error!!!');
          }
        },
        error => {
          // error message
        }
      );
  }

  GetInputAccounts() {
    this.commonservice.GetInputLevelAccountDetails().subscribe(data => {
      this.inputLevelAccounts = data;
    });
  }

  ShowPopup() {
    this.paymentTypes = {
      ChartOfAccountNewId: 0,
      AccountName: '',
      Id: 0,
      Name: ''
    };
    this.showPaymentPopUp = true;
  }

  HidePopup() {
    this.showPaymentPopUp = false;
  }

  showLoader() {
    this.paymentLoader = true;
  }

  hideLoader() {
    this.paymentLoader = false;
  }
}

interface IPaymentTypes {
  Id: number;
  Name: string;
  ChartOfAccountNewId: number;
  AccountName: string;
}
