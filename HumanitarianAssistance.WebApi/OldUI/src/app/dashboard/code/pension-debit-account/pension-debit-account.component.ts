import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CodeService } from '../code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';

@Component({
  selector: 'app-pension-debit-account',
  templateUrl: './pension-debit-account.component.html',
  styleUrls: ['./pension-debit-account.component.css']
})
export class PensionDebitAccountComponent implements OnInit {

  inputLevelAccounts: any[]= [];
  selectedAccount: any= null;
  loading= false;

  constructor(private commonservice: CommonService,
    private setting: AppSettingsService,
    private codeservice: CodeService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.GetInputAccounts();
    this.getPensionDebitAccount();
  }


  GetInputAccounts() {
    this.commonservice.GetInputLevelAccountDetails().subscribe(data => {
      this.inputLevelAccounts = data;
    });
  }

  onAccountSelection(accountId: any) {
    this.loading = true;
    this.codeservice.AddEditDetails(this.setting.getBaseUrl() + GLOBAL.API_Code_AddEditPensionDebitAccount, accountId)
    .subscribe(
      data => {
        if (
          data.StatusCode === 200
        ) {
          // this.getPensionDebitAccount();
          this.loading = false;
          this.toastr.success(data.Message);
        }
      },
      error => {
        if (error.StatusCode === 500) {
          this.loading = false;
          this.toastr.error('Internal Server Error....');
        } else if (error.StatusCode === 401) {
          this.loading = false;
          this.toastr.error('Unauthorized Access Error....');
        } else if (error.StatusCode === 403) {
          this.loading = false;
          this.toastr.error('Forbidden Error....');
        }
      }
    );
  }

  getPensionDebitAccount() {
    this.loading = true;
    this.codeservice.GetAllDetails(this.setting.getBaseUrl() + GLOBAL.API_Code_GetPensionDebitAccount)
    .subscribe(
      data => {
        if (
          data.StatusCode === 200
        ) {
          this.selectedAccount = data.data.PensionDebitAccountId;
          this.loading = false;
        }
      },
      error => {
        if (error.StatusCode === 500) {
          this.loading = false;
          this.toastr.error('Internal Server Error....');
        } else if (error.StatusCode === 401) {
          this.loading = false;
          this.toastr.error('Unauthorized Access Error....');
        } else if (error.StatusCode === 403) {
          this.loading = false;
          this.toastr.error('Forbidden Error....');
        }
      }
    );
  }

}
