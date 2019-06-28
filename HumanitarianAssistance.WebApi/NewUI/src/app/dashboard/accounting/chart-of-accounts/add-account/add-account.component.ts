import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; ;
import { ChartOfAccountModel } from '../models/chart-of-account.model';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { AccountLevels, AccountLevelLimits } from 'src/app/shared/enum';
import { GLOBAL } from 'src/app/shared/global';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';


@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.scss']
})
export class AddAccountComponent implements OnInit {

  AccountHeadType: number;
  AccountLevel: number;
  chartOfAccountList: any[];
  addCharOfAccountForm: FormGroup;
  addAccountLoader = false;
  mainLevelData: any;
  controlLevelData: any;
  subLevelData: any;
  parentAccountCode: any;
  min: number;
  max: number;
  maxlength: number;
  submitted = false;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);


  constructor(public dialogRef: MatDialogRef<AddAccountComponent>,
    public commonLoader: CommonLoaderService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    public toastr: ToastrService,
    private globalService: GlobalService,
    private appUrl: AppUrlService ) {
      this.AccountHeadType = data.AccountHeadType;
      this.chartOfAccountList = data.AccountList;
      this.mainLevelData = data.mainLevelData;
      this.AccountLevel = data.AccountLevel;
      this.controlLevelData = data.controlLevelData;
      this.subLevelData = data.subLevelData;

      if (this.AccountLevel === AccountLevels.MainLevel) {
        this.initForm(null);
        this.min = 1;
        this.max = 9;
        this.maxlength = 1;
      } else if (this.AccountLevel === AccountLevels.ControlLevel) {
        this.initForm(this.mainLevelData.ChartOfAccountNewCode);
        this.min = 1;
        this.max = 9;
        this.maxlength = 1;
      } else if (this.AccountLevel === AccountLevels.SubLevel) {
        this.initForm(this.controlLevelData.ChartOfAccountNewCode);
        this.min = 1;
        this.max = 99;
        this.maxlength = 2;
      } else if (this.AccountLevel === AccountLevels.InputLevel) {
        this.initForm(this.subLevelData.ChartOfAccountNewCode);
        this.min = 1;
        this.max = 99;
        this.maxlength = 2;
      }

    }

  ngOnInit() {
  }

//#region "initForm"
initForm(AccountCode: any) {
  this.parentAccountCode = AccountCode;
  this.addCharOfAccountForm = this.fb.group({
    AccountCode: [null, Validators.required],
    AccountName: [null, Validators.required],
    AccountHeadType: this.AccountHeadType,
    AccountLevel: this.AccountLevel
  });
}
//#endregion

get f() {
  return this.addCharOfAccountForm.controls;
}

//#region "onFormSubmit"
onFormSubmit(data: any) {
  this.submitted = true;
  if (data.AccountLevel === AccountLevels.MainLevel) {
    data.AccountCode = this.parentAccountCode != null ? this.parentAccountCode + data.AccountCode : data.AccountCode;
    this.addMainLevelAccountDetail(data);
  } else if (data.AccountLevel === AccountLevels.ControlLevel) {
    data.AccountCode = this.parentAccountCode + data.AccountCode;
    this.addControlLevelAccountDetail(data);
  } else if (data.AccountLevel === AccountLevels.SubLevel) {
    data.AccountCode = this.parentAccountCode + data.AccountCode;
    this.addSubLevelAccountDetail(data);
  } else if (data.AccountLevel === AccountLevels.InputLevel) {
    data.AccountCode = this.parentAccountCode + data.AccountCode;
    this.addInputLevelAccountDetail(data);
  }
}
//#endregion

 //#region "addMainLevelAccountDetail"
 addMainLevelAccountDetail(model: any) {

  if (this.addCharOfAccountForm.valid) {
    this.addAccountLoader = true;
    const count = this.chartOfAccountList.length;
    if (count <  AccountLevelLimits.MainLevel) {

    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: AccountLevels.MainLevel,
      AccountHeadTypeId: this.AccountHeadType,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId,
      ChartOfAccountNewCode: model.AccountCode
    };

    const item = this.chartOfAccountList.length - 1; // use to calculate the index

    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_AddChartOfAccount, obj)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        response => {
          if (response.StatusCode === 200) {

            if (response.data.ChartOfAccountNewDetail !== null) {

              const responseData = response.data.ChartOfAccountNewDetail;

              obj.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
              obj.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
              obj.AccountName = responseData.AccountName;
              obj.ParentID = responseData.ParentID;
              obj.AccountLevelId = responseData.AccountLevelId;
              obj.AccountHeadTypeId = responseData.AccountHeadTypeId;
              obj.AccountTypeId = responseData.AccountTypeId;
              obj.AccountFilterTypeId = responseData.AccountFilterTypeId;

              obj.Children = [];
              obj._IsDeleted = false;
              obj._IsError = false;
              obj._IsLoading = false;
              this.addAccountLoader = false;

              // Update the Obj and Push into the list
              this.chartOfAccountList.push(obj);
              this.dialogRef.close();

            }

          } else if (response.StatusCode === 400) {
            this.addAccountLoader = false;

            this.toastr.error(response.Message);
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');
          this.addAccountLoader = false;
        }
      );
    } else {
      this.toastr.error('Limit Exceeded');
      this.addAccountLoader = false;
    }
  }
}
//#endregion

//#region "addControlLevelAccountDetail"
addControlLevelAccountDetail(model: any) {
  // Main Level

  if (this.addCharOfAccountForm.valid) {

    this.addAccountLoader = true;
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === this.mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);

    const count = this.chartOfAccountList[mainLevelIndex].Children.length - 1;
    if (count < AccountLevelLimits.ControlLevel) {

    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      ChartOfAccountNewCode: model.AccountCode,
      AccountName: model.AccountName,
      ParentID: mainLevelItem.ChartOfAccountNewId,
      AccountLevelId: AccountLevels.ControlLevel,
      AccountHeadTypeId: this.AccountHeadType,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };

    // Error handling and loading handling
    const index = this.chartOfAccountList[mainLevelIndex].Children.length - 1; // use to calculate the index

    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_AddChartOfAccount, obj)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            if (response.data.ChartOfAccountNewDetail !== null) {
              const responseData = response.data.ChartOfAccountNewDetail;
              obj.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
              obj.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
              obj.AccountName = responseData.AccountName;
              obj.ParentID = responseData.ParentID;
              obj.AccountLevelId = responseData.AccountLevelId;
              obj.AccountHeadTypeId = responseData.AccountHeadTypeId;
              obj.AccountTypeId = responseData.AccountTypeId;
              obj.AccountFilterTypeId = responseData.AccountFilterTypeId;
              obj.Children = [];
              obj._IsDeleted = false;
              obj._IsError = false;
              obj._IsLoading = false;
              this.addAccountLoader = false;

              // Update the Obj and Push into the list
              this.chartOfAccountList[mainLevelIndex].Children.push(obj);
              this.dialogRef.close();
            }

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);
            this.addAccountLoader = false;
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');
          this.addAccountLoader = false;
        }
      );
    } else {
      this.toastr.error('Limit Exceeded');
      this.addAccountLoader = false;
    }
  }
}
//#endregion

//#region "addSubLevelAccountDetail"
addSubLevelAccountDetail(model: any) {

  if (this.addCharOfAccountForm.valid) {

    this.addAccountLoader = true;

// Main Level
const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === this.mainLevelData.ChartOfAccountNewId);
const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);


// Control Level
const controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
                             .find(x => x.ChartOfAccountNewId === this.controlLevelData.ChartOfAccountNewId);
const controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
                              .indexOf(controlLevelItem);

const count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1;
if (count < AccountLevelLimits.SubLevel) {

const obj: ChartOfAccountModel = {
  ChartOfAccountNewId: 0,
  AccountName: model.AccountName,
  ParentID: this.controlLevelData.ChartOfAccountNewId,
  AccountLevelId: AccountLevels.SubLevel,
  AccountHeadTypeId: this.AccountHeadType,
  AccountTypeId: model.AccountTypeId,
  AccountFilterTypeId: model.AccountFilterTypeId,
  ChartOfAccountNewCode: model.AccountCode,
};

// Error handling and loading handling
const index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1; // use to calculate the index

this.globalService
  .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_AddChartOfAccount, obj)
  .pipe(takeUntil(this.destroyed$))
  .subscribe(
    response => {
      if (response.StatusCode === 200) {
        if (response.data.ChartOfAccountNewDetail !== null) {
          const responseData = response.data.ChartOfAccountNewDetail;
          obj.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
          obj.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
          obj.AccountName = responseData.AccountName;
          obj.ParentID = responseData.ParentID;
          obj.AccountLevelId = responseData.AccountLevelId;
          obj.AccountHeadTypeId = responseData.AccountHeadTypeId;
          obj.AccountTypeId = responseData.AccountTypeId;
          obj.AccountFilterTypeId = responseData.AccountFilterTypeId;
          obj.Children = [];
          obj._IsDeleted = false;
          obj._IsError = false;
          obj._IsLoading = false;

          this.addAccountLoader = false;

          // Update the Obj and Push into the list
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.push(obj);
          this.dialogRef.close();
        }

      } else if (response.StatusCode === 400) {

        this.toastr.error(response.Message);
        this.addAccountLoader = false;
      }
    },
    error => {
      this.toastr.error('Something went wrong ! Try Again');
      this.addAccountLoader = false;
    }
  );
} else {
  this.toastr.error('Limit Exceeded');
  this.addAccountLoader = false;
}
  }
}
//#endregion


//#region "addInputLevelAccountDetail"
addInputLevelAccountDetail(model: any) {

  if (this.addCharOfAccountForm.valid) {

    this.addAccountLoader = true;

    // Main Level
  const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === this.mainLevelData.ChartOfAccountNewId);
  const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);


  // Control Level
  const controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
                               .find(x => x.ChartOfAccountNewId === this.controlLevelData.ChartOfAccountNewId);
  const controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
                                .indexOf(controlLevelItem);

  // Sub Level
  const subLevelItem = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                           .find(x => x.ChartOfAccountNewId === this.subLevelData.ChartOfAccountNewId);
  const subLevelIndex = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                            .indexOf(subLevelItem);

  const count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.length - 1;
  if (count < AccountLevelLimits.InputLevel) {

  const obj: ChartOfAccountModel = {
    ChartOfAccountNewId: 0,
    AccountName: model.AccountName,
    ParentID: this.subLevelData.ChartOfAccountNewId,
    AccountLevelId: AccountLevels.InputLevel,
    AccountHeadTypeId: this.AccountHeadType,
    AccountTypeId: model.AccountTypeId,
    AccountFilterTypeId: model.AccountFilterTypeId,
    ChartOfAccountNewCode: model.AccountCode
  };

  // Error handling and loading handling
  const index =  this.chartOfAccountList[mainLevelIndex]
                     .Children[controlLevelIndex].Children[subLevelIndex].Children.length - 1; // use to calculate the index

  this.globalService
    .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_AddChartOfAccount, obj)
    .pipe(takeUntil(this.destroyed$))
    .subscribe(
      response => {
        if (response.StatusCode === 200) {
          if (response.data.ChartOfAccountNewDetail !== null) {
            const responseData = response.data.ChartOfAccountNewDetail;
            obj.ChartOfAccountNewId = responseData.ChartOfAccountNewId;
            obj.ChartOfAccountNewCode = responseData.ChartOfAccountNewCode;
            obj.AccountName = responseData.AccountName;
            obj.ParentID = responseData.ParentID;
            obj.AccountLevelId = responseData.AccountLevelId;
            obj.AccountHeadTypeId = responseData.AccountHeadTypeId;
            obj.AccountTypeId = responseData.AccountTypeId;
            obj.AccountFilterTypeId = responseData.AccountFilterTypeId;
            obj.Children = [];
            obj._IsDeleted = false;
            obj._IsError = false;
            obj._IsLoading = false;

            this.addAccountLoader = false;

            // Update the Obj and Push into the list
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.push(obj);
            this.dialogRef.close();
          }

        } else if (response.StatusCode === 400) {

          this.toastr.error(response.Message);
          this.addAccountLoader = false;
        }
      },
      error => {
        this.toastr.error('Something went wrong ! Try Again');
        this.addAccountLoader = false;
      }
    );
  } else {
    this.toastr.error('Limit Exceeded');
    this.addAccountLoader = false;
  }

  }


}
//#endregion


ngOnDestroy() {

  this.destroyed$.next(true);
  this.destroyed$.complete();

}

}
