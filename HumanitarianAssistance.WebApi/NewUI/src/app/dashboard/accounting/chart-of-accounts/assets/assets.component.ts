import { Component, OnInit, HostListener } from '@angular/core';
import {
  ChartOfAccountModel,
  AccountFilterTypeModel,
  AccountTypeModel
} from '../models/chart-of-account.model';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { AddAccountComponent } from '../add-account/add-account.component';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { GLOBAL } from 'src/app/shared/global';
import { AccountLevelLimits, AccountLevels, AccountHeadTypes_Enum } from 'src/app/shared/enum';

@Component({
  selector: 'app-assets',
  templateUrl: './assets.component.html',
  styleUrls: ['./assets.component.scss']
})
export class AssetsComponent implements OnInit {
  //#region "variables"
  ACCOUNT_HEAD_TYPE = AccountHeadTypes_Enum.Assets;

  // Form
  mainLevelList: ChartOfAccountModel[];
  chartOfAccountList: ChartOfAccountModel[] = [];
  accountFilterTypeList: AccountFilterTypeModel[];
  accountTypeList: AccountTypeModel[];

  // screen
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;

  isEditingAllowed = false;
  pageId = ApplicationPages.Assets;

  //#endregion

  constructor(
    private globalService: GlobalService,
    private appUrl: AppUrlService,
    public toastr: ToastrService,
    public commonLoaderService: CommonLoaderService,
    private localStorageService: LocalStorageService,
    public dialog: MatDialog,
  ) {
    this.getScreenSize();
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
      this.screenHeight = window.innerHeight;
      this.screenWidth = window.innerWidth;

      this.scrollStyles = {
        'overflow-y': 'auto',
        'height': this.screenHeight - 110 + 'px',
        'overflow-x': 'hidden'
        };
  }
  //#endregion

  ngOnInit() {
    this.initList();
    this.getAllAccountFilterType();
    this.getAllAccountTypeByCategory();
    this.getMainLevelAccount(this.ACCOUNT_HEAD_TYPE);
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }
  initList() {}

  //#region "on main level click, get control level account"
  onMainLevelClicked(model: any) {
    // this.getAccountByParentId(data);

    this.commonLoaderService.showLoader();

    this.globalService
      .getListById(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_GetAllAccountsByParentId,
        model.ChartOfAccountNewId
      )
      .subscribe(
        data => {

          // Main Level
          const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
          const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);

          this.chartOfAccountList[mainLevelIndex].Children = []; // intitialize

          if (data.StatusCode === 200) {
            if (data.data.AllAccountList != null) {
              if (data.data.AllAccountList.length > 0) {
                data.data.AllAccountList.forEach(element => {
                  this.chartOfAccountList[mainLevelIndex].Children.push({
                    ChartOfAccountNewId: element.ChartOfAccountNewId,
                    AccountName: element.AccountName,
                    ChartOfAccountNewCode: element.ChartOfAccountNewCode,
                    ParentID: element.ParentID,
                    AccountHeadTypeId: element.AccountHeadTypeId,
                    AccountLevelId: element.AccountLevelId,
                    AccountTypeId: element.AccountTypeId,
                    AccountFilterTypeId: element.AccountFilterTypeId,

                    Children: [],
                    _IsDeleted: false,
                    _IsLoading: false,
                    _IsError: false,
                  });
                });
              }
            }
          }

          this.commonLoaderService.hideLoader();
        },
        error => {
          this.commonLoaderService.hideLoader();
        }
      );
  }
  //#endregion

  //#region "on control level click, get sub level account"
  onControlLevelClicked(mainLevel: any, model: any) {

    this.commonLoaderService.showLoader();

    this.globalService
      .getListById(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_GetAllAccountsByParentId,
        model.ChartOfAccountNewId
      )
      .subscribe(
        data => {

            // Main Level
            const mainLevelItem = this.chartOfAccountList.find(
              x => x.ParentID === mainLevel.ChartOfAccountNewId
            );
            const mainLevelIndex = this.chartOfAccountList.indexOf(
              mainLevelItem
            );

            // Control Level
            const controlLevelItem = this.chartOfAccountList[
              mainLevelIndex
            ].Children.find(
              x => x.ChartOfAccountNewId === model.ChartOfAccountNewId
            );
            const controlLevelIndex = this.chartOfAccountList[
              mainLevelIndex
            ].Children.indexOf(controlLevelItem);

            this.chartOfAccountList[mainLevelIndex].Children[
              controlLevelIndex
            ].Children = []; // intitialize

          if (data.StatusCode === 200) {
            if (data.data.AllAccountList != null) {
              if (data.data.AllAccountList.length > 0) {

                data.data.AllAccountList.forEach(element => {
                  this.chartOfAccountList[mainLevelIndex].Children[
                    controlLevelIndex
                  ].Children.push({
                    ChartOfAccountNewId: element.ChartOfAccountNewId,
                    AccountName: element.AccountName,
                    ChartOfAccountNewCode: element.ChartOfAccountNewCode,
                    ParentID: element.ParentID,
                    AccountHeadTypeId: element.AccountHeadTypeId,
                    AccountLevelId: element.AccountLevelId,
                    AccountTypeId: element.AccountTypeId,
                    AccountFilterTypeId: element.AccountFilterTypeId,

                    Children: [],
                    _IsDeleted: false,
                    _IsLoading: false,
                    _IsError: false,
                  });
                });
              }
            }
          }

          this.commonLoaderService.hideLoader();
        },
        error => {
          this.commonLoaderService.hideLoader();
        }
      );
  }
  //#endregion

  //#region "on sub level click, get input level account"
  onSubLevelClicked(mainLevel, controlLevel, model) {


    this.commonLoaderService.showLoader();

    this.globalService
      .getListById(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_GetAllAccountsByParentId,
        model.ChartOfAccountNewId
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (data.data.AllAccountList != null) {
              if (data.data.AllAccountList.length > 0) {

                // Main Level
                const mainLevelItem = this.chartOfAccountList.find(
                  x => x.ParentID === mainLevel.ChartOfAccountNewId
                );
                const mainLevelIndex = this.chartOfAccountList.indexOf(
                  mainLevelItem
                );

                // Control Level
                const controlLevelItem = this.chartOfAccountList[
                  mainLevelIndex
                ].Children.find(
                  x =>
                    x.ChartOfAccountNewId === controlLevel.ChartOfAccountNewId
                );
                const controlLevelIndex = this.chartOfAccountList[
                  mainLevelIndex
                ].Children.indexOf(controlLevelItem);

                // Sub Level
                const subLevelItem = this.chartOfAccountList[
                  mainLevelIndex
                ].Children[controlLevelIndex].Children.find(
                  x => x.ChartOfAccountNewId === model.ChartOfAccountNewId
                );
                const subLevelIndex = this.chartOfAccountList[
                  mainLevelIndex
                ].Children[controlLevelIndex].Children.indexOf(subLevelItem);

                this.chartOfAccountList[mainLevelIndex].Children[
                  controlLevelIndex
                ].Children[subLevelIndex].Children = []; // intitialize

                data.data.AllAccountList.forEach(element => {
                  this.chartOfAccountList[mainLevelIndex].Children[
                    controlLevelIndex
                  ].Children[subLevelIndex].Children.push({
                    ChartOfAccountNewId: element.ChartOfAccountNewId,
                    AccountName: element.AccountName,
                    ChartOfAccountNewCode: element.ChartOfAccountNewCode,
                    ParentID: element.ParentID,
                    AccountHeadTypeId: element.AccountHeadTypeId,
                    AccountLevelId: element.AccountLevelId,
                    AccountTypeId: element.AccountTypeId,
                    AccountFilterTypeId: element.AccountFilterTypeId,

                    Children: [],
                    _IsDeleted: false,
                    _IsError: false,
                    _IsLoading: false
                  });
                });
              }
            }
          }
          this.commonLoaderService.hideLoader();
        },
        error => {
          this.commonLoaderService.hideLoader();
        }
      );
  }
  //#endregion

  //#region "on input level click"
  onInputLevelClicked(mainLevel, controlLevel, subLevel, data) {

  }
  //#endregion

  //#region "getAllAccountFilterType"
  getAllAccountFilterType() {
    this.accountFilterTypeList = [];

    this.commonLoaderService.showLoader();

    this.globalService
      .getList(this.appUrl.getApiUrl() + GLOBAL.API_Account_GetAllAccountFilter)
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (data.data.AllAccountFilterList != null) {
              if (data.data.AllAccountFilterList.length > 0) {

                data.data.AllAccountFilterList.forEach(element => {
                  this.accountFilterTypeList.push({
                    AccountFilterTypeId: element.AccountFilterTypeId,
                    AccountFilterTypeName: element.AccountFilterTypeName
                  });
                });
              }
            }
          } else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong ! Try Again');
          }

          this.commonLoaderService.hideLoader();
        },
        error => {
          this.commonLoaderService.hideLoader();
        }
      );
  }
  //#endregion

  //#region "getAllAccountTypeByCategory"
  getAllAccountTypeByCategory() {
    this.accountTypeList = [];
    this.globalService
      .getListById(
        this.appUrl.getApiUrl() +
          GLOBAL.API_Code_GetAllAccountByAccountHeadTypeId,
        this.ACCOUNT_HEAD_TYPE
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          if (data.data.AccountTypeList != null) {
            if (data.data.AccountTypeList.length > 0) {

              data.data.AccountTypeList.forEach(element => {
                this.accountTypeList.push({
                  AccountTypeId: element.AccountTypeId,
                  AccountTypeName: element.AccountTypeName,
                  AccountCategory: element.AccountCategory,
                  AccountHeadTypeId: element.AccountHeadTypeId
                });
              });
            }
          }
        } else if (data.StatusCode === 400) {
          this.toastr.error('Something went wrong ! Try Again');
        }
      });
  }
  //#endregion


  //#region -- Main Level --

  //#region "getMainLevelAccount"
  getMainLevelAccount(id: number) {

    this.chartOfAccountList = [];

    this.commonLoaderService.showLoader();

    this.globalService
      .getListById(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_GetMainLevelAccount,
        id
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (data.data.MainLevelAccountList != null) {
              if (data.data.MainLevelAccountList.length > 0) {
                data.data.MainLevelAccountList.forEach(element => {
                  this.chartOfAccountList.push({
                    ChartOfAccountNewId: element.ChartOfAccountNewId,
                    AccountName: element.AccountName,
                    ChartOfAccountNewCode: element.ChartOfAccountNewCode,
                    ParentID: element.ParentID,
                    AccountHeadTypeId: element.AccountHeadTypeId,
                    AccountLevelId: element.AccountLevelId,
                    AccountTypeId: element.AccountTypeId,
                    AccountFilterTypeId: element.AccountFilterTypeId,

                    // Error Handling
                    _IsDeleted: false,
                    _IsLoading: false,
                    _IsError: false,
                  });
                });
              }
            }
          } else if (data.StatusCode === 400) {
            this.toastr.error('Something went wrong ! Try Again');
          }

          this.commonLoaderService.hideLoader();
        },
        error => {
          this.commonLoaderService.hideLoader();
        }
      );
  }
  //#endregion

  //#region "addMainLevelAccountDetail"
  addMainLevelAccountDetail(model: any) {

    const count = this.chartOfAccountList.length;
    if (count <  AccountLevelLimits.MainLevel) {

    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };

    this.chartOfAccountList.push(obj);

    // Error handling and loading handling
    const item = this.chartOfAccountList.length - 1; // use to calculate the index
    this.chartOfAccountList[item]._IsLoading = true;
    this.chartOfAccountList[item]._IsError = false;

    // // Error handling and loading handling
    // const item = this.chartOfAccountList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
    // const index = this.occupatonList.indexOf(item);
    // this.occupatonList[index]._IsLoading = true;
    // this.occupatonList[index]._IsError = false;


    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_AddChartOfAccount, obj)
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

              // Update the Obj and Push into the list
              this.chartOfAccountList[item] = obj;

            }

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[item]._IsLoading = false;
            this.chartOfAccountList[item]._IsError = true;

          } else {
            // Error Handling
            this.chartOfAccountList[item]._IsLoading = false;
            this.chartOfAccountList[item]._IsError = true;
          }

        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');

          // Error Handling
          this.chartOfAccountList[item]._IsLoading = false;
          this.chartOfAccountList[item]._IsError = true;

        }
      );


    } else {
      this.toastr.error('Limit Exceeded');
    }
  }
  //#endregion

  //#region "editMainLevelAccountDetail"
  editMainLevelAccountDetail(model: ChartOfAccountModel) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };


    // Error handling and loading handling
    const item = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
    const index = this.chartOfAccountList.indexOf(item);
    this.chartOfAccountList[index]._IsLoading = true;
    this.chartOfAccountList[index]._IsError = false;



    this.globalService
      .post(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_EditChartOfAccount,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {

            // Error Handling
            this.chartOfAccountList[index]._IsLoading = false;
            this.chartOfAccountList[index]._IsError = false;

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[index]._IsLoading = false;
            this.chartOfAccountList[index]._IsError = true;

          }

        },
        error => {
          // error handling

          this.toastr.error('Someting went wrong! Try again');

          // Error Handling
            this.chartOfAccountList[index]._IsLoading = false;
            this.chartOfAccountList[index]._IsError = true;


        }
      );
  }
  //#endregion

  //#region "deleteMainLevelAccountDetail"
  deleteMainLevelAccountDetail(model: ChartOfAccountModel) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };


    // Error handling and loading handling
    const item = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
    const index = this.chartOfAccountList.indexOf(item);
    this.chartOfAccountList[index]._IsLoading = true;
    this.chartOfAccountList[index]._IsError = false;



    this.globalService
      .post(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_DeleteChartOfAccount,
        obj.ChartOfAccountNewId
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {

            this.chartOfAccountList.splice(index, 1);

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[index]._IsLoading = false;
            this.chartOfAccountList[index]._IsError = true;

          }

        },
        error => {
          // error handling

          this.toastr.error('Someting went wrong! Try again');

          // Error Handling
            this.chartOfAccountList[index]._IsLoading = false;
            this.chartOfAccountList[index]._IsError = true;


        }
      );
  }
  //#endregion

  //#endregion




  //#region -- Control Level --


  //#region "addControlLevelAccountDetail"
  addControlLevelAccountDetail(mainLevelData: ChartOfAccountModel, model: any) {
    // Main Level
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);

    const count = this.chartOfAccountList[mainLevelIndex].Children.length - 1;
    if (count < AccountLevelLimits.ControlLevel) {

    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };

    this.chartOfAccountList[mainLevelIndex].Children.push(obj);

    // Error handling and loading handling
    const index = this.chartOfAccountList[mainLevelIndex].Children.length - 1; // use to calculate the index
    this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = true;
    this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = false;

    // // Error handling and loading handling
    // const item = this.chartOfAccountList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
    // const index = this.occupatonList.indexOf(item);
    // this.occupatonList[index]._IsLoading = true;
    // this.occupatonList[index]._IsError = false;


    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_AddChartOfAccount, obj)
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

              // Update the Obj and Push into the list
              this.chartOfAccountList[mainLevelIndex].Children[index] = obj;
            }

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;

          } else {
            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;
          }

        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');

          // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;

        }
      );


    } else {
      this.toastr.error('Limit Exceeded');
    }
  }
  //#endregion

  //#region "editControlLevelAccountDetail"
  editControlLevelAccountDetail(mainLevelData: ChartOfAccountModel, model: ChartOfAccountModel) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };

    // Main Level
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);

    // Error handling and loading handling
    const item = this.chartOfAccountList[mainLevelIndex].Children.find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
    const index = this.chartOfAccountList[mainLevelIndex].Children.indexOf(item);
    this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = true;
    this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = false;

    this.globalService
      .post(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_EditChartOfAccount,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = false;

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;

          }

        },
        error => {
          // error handling

          this.toastr.error('Someting went wrong! Try again');

          // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;
        }
      );
  }
  //#endregion

  //#region "deleteControlLevelAccountDetail"
  deleteControlLevelAccountDetail(mainLevelData: ChartOfAccountModel, model: ChartOfAccountModel) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };

    // Main Level
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);

    // Error handling and loading handling
    const item = this.chartOfAccountList[mainLevelIndex].Children.find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
    const index = this.chartOfAccountList[mainLevelIndex].Children.indexOf(item);
    this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = true;
    this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = false;

    this.globalService
      .post(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_DeleteChartOfAccount,
        obj.ChartOfAccountNewId
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children.splice(index, 1);

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;

          }

        },
        error => {
          // error handling

          this.toastr.error('Someting went wrong! Try again');

          // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[index]._IsError = true;
        }
      );
  }
  //#endregion

  //#endregion



  //#region -- Sub Level --


  //#region "addSubLevelAccountDetail"
  addSubLevelAccountDetail(mainLevelData: ChartOfAccountModel, controlLevelData: ChartOfAccountModel, model: any) {
    // Main Level
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);


    // Control Level
    const controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
                                 .find(x => x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId);
    const controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
                                  .indexOf(controlLevelItem);

    const count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1;
    if (count < AccountLevelLimits.SubLevel) {

    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };

    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.push(obj);

    // Error handling and loading handling
    const index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1; // use to calculate the index
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = true;
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = false;

    // // Error handling and loading handling
    // const item = this.chartOfAccountList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
    // const index = this.occupatonList.indexOf(item);
    // this.occupatonList[index]._IsLoading = true;
    // this.occupatonList[index]._IsError = false;


    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_AddChartOfAccount, obj)
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

              // Update the Obj and Push into the list
              this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index] = obj;
            }

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;

          } else {
            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;
          }

        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');

          // Error Handling
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;

        }
      );


    } else {
      this.toastr.error('Limit Exceeded');
    }
  }
  //#endregion

  //#region "editSubLevelAccountDetail"
  editSubLevelAccountDetail(mainLevelData: ChartOfAccountModel, controlLevelData: ChartOfAccountModel, model: ChartOfAccountModel) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };

    console.log(obj);

    // Main Level
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);

    // Control Level
    const controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
                                 .find(x => x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId);
    const controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
                                  .indexOf(controlLevelItem);

    // const count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1;

    // Error handling and loading handling
    const item = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                                                        .find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
    item.AccountName = obj.AccountName; // its needed
    item.AccountFilterTypeId = obj.AccountFilterTypeId; // its needed
    item.AccountTypeId = obj.AccountTypeId; // its needed
    const index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.indexOf(item);
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = true;
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = false;

    this.globalService
      .post(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_EditChartOfAccount,
        obj
      )
      .subscribe(
        response => {
          console.log(obj);
          if (response.StatusCode === 200) {

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = false;

          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;

          }

        },
        error => {
          // error handling

          this.toastr.error('Someting went wrong! Try again');

          // Error Handling
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;
        }
      );
  }
  //#endregion

  //#region "deleteSubLevelAccountDetail"
  deleteSubLevelAccountDetail(mainLevelData: ChartOfAccountModel, controlLevelData: ChartOfAccountModel, model: ChartOfAccountModel) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };


    // Main Level
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);

    // Control Level
    const controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
                                 .find(x => x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId);
    const controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
                                  .indexOf(controlLevelItem);

    // const count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.length - 1;

    // Error handling and loading handling
    const item = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                                                        .find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
    item.AccountName = obj.AccountName; // its needed
    item.AccountFilterTypeId = obj.AccountFilterTypeId; // its needed
    const index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.indexOf(item);
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = true;
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = false;

    this.globalService
      .post(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_DeleteChartOfAccount,
        obj.ChartOfAccountNewId
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {

            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children.splice(index, 1);

          } else if (response.StatusCode === 400) {
            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;

          }

        },
        error => {
          // error handling

          this.toastr.error('Someting went wrong! Try again');

          // Error Handling
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsLoading = false;
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[index]._IsError = true;
        }
      );
  }
  //#endregion



  //#endregion





  //#region -- Input Level --


  //#region "addInputLevelAccountDetail"
  addInputLevelAccountDetail(
    mainLevelData: ChartOfAccountModel,
    controlLevelData: ChartOfAccountModel,
    subLevelData: ChartOfAccountModel,
    model: any
    ) {
    // Main Level
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);


    // Control Level
    const controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
                                 .find(x => x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId);
    const controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
                                  .indexOf(controlLevelItem);

    // Sub Level
    const subLevelItem = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                             .find(x => x.ChartOfAccountNewId === subLevelData.ChartOfAccountNewId);
    const subLevelIndex = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                              .indexOf(subLevelItem);

    const count = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.length - 1;
    if (count < AccountLevelLimits.InputLevel) {

    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };

    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.push(obj);

    // Error handling and loading handling
    const index =  this.chartOfAccountList[mainLevelIndex]
                       .Children[controlLevelIndex].Children[subLevelIndex].Children.length - 1; // use to calculate the index
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = true;
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = false;

    // // Error handling and loading handling
    // const item = this.chartOfAccountList.find(x => x.OccupationOtherDetailId === model.OccupationOtherDetailId);
    // const index = this.occupatonList.indexOf(item);
    // this.occupatonList[index]._IsLoading = true;
    // this.occupatonList[index]._IsError = false;


    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_AddChartOfAccount, obj)
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

              // Update the Obj and Push into the list
              this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index] = obj;


            }

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;

          } else {
            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;
          }

        },
        error => {
          this.toastr.error('Something went wrong ! Try Again');

          // Error Handling
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;

        }
      );


    } else {
      this.toastr.error('Limit Exceeded');
    }
  }
  //#endregion

  //#region "editInputLevelAccountDetail"
  editInputLevelAccountDetail(
    mainLevelData: ChartOfAccountModel,
    controlLevelData: ChartOfAccountModel,
    subLevelData: ChartOfAccountModel,
    model: ChartOfAccountModel
  ) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };
    // Main Level
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);

    // Control Level
    const controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
                                 .find(x => x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId);
    const controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
                                  .indexOf(controlLevelItem);

    // Sub Level
    const subLevelItem = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                             .find(x => x.ChartOfAccountNewId === subLevelData.ChartOfAccountNewId);
    const subLevelIndex = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                              .indexOf(subLevelItem);

    // Error handling and loading handling
    const item = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children
                                                        .find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
    const index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.indexOf(item);

    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = true;
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = false;

    this.globalService
      .post(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_EditChartOfAccount,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = false;

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;

          }

        },
        error => {
          // error handling

          this.toastr.error('Someting went wrong! Try again');

          // Error Handling
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;
        }
      );
  }
  //#endregion

  //#region "deleteInputLevelAccountDetail"
  deleteInputLevelAccountDetail(
    mainLevelData: ChartOfAccountModel,
    controlLevelData: ChartOfAccountModel,
    subLevelData: ChartOfAccountModel,
    model: ChartOfAccountModel
  ) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };
    // Main Level
    const mainLevelItem = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === mainLevelData.ChartOfAccountNewId);
    const mainLevelIndex = this.chartOfAccountList.indexOf(mainLevelItem);

    // Control Level
    const controlLevelItem = this.chartOfAccountList[mainLevelIndex].Children
                                 .find(x => x.ChartOfAccountNewId === controlLevelData.ChartOfAccountNewId);
    const controlLevelIndex = this.chartOfAccountList[mainLevelIndex].Children
                                  .indexOf(controlLevelItem);

    // Sub Level
    const subLevelItem = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                             .find(x => x.ChartOfAccountNewId === subLevelData.ChartOfAccountNewId);
    const subLevelIndex = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children
                              .indexOf(subLevelItem);

    // Error handling and loading handling
    const item = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children
                                                        .find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
    const index = this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.indexOf(item);

    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = true;
    this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = false;

    this.globalService
      .post(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_DeleteChartOfAccount,
        obj.ChartOfAccountNewId
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children.splice(index, 1);

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
            this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;

          }

        },
        error => {
          // error handling

          this.toastr.error('Someting went wrong! Try again');

          // Error Handling
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsLoading = false;
          this.chartOfAccountList[mainLevelIndex].Children[controlLevelIndex].Children[subLevelIndex].Children[index]._IsError = true;
        }
      );
  }
  //#endregion



  //#endregion


  //#region " addAny"
  addAnyAccountDetail(model: any) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };

    this.commonLoaderService.showLoader();

    this.globalService
      .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_AddChartOfAccount, obj)
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.getMainLevelAccount(this.ACCOUNT_HEAD_TYPE);
          } else if (data.StatusCode === 400) {
            this.toastr.error(data.Message);
          }

          this.commonLoaderService.hideLoader();
        },
        error => {
          // error handling
          this.toastr.error('Something went wrong ! Try Again');
          this.commonLoaderService.hideLoader();
        }
      );
  }
  //#endregion

  //#region "editAny"
  editAnyAccountDetail(model: ChartOfAccountModel) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: model.ChartOfAccountNewId,
      AccountName: model.AccountName,
      ParentID: model.ParentID,
      AccountLevelId: model.AccountLevelId,
      AccountHeadTypeId: model.AccountHeadTypeId,
      AccountTypeId: model.AccountTypeId,
      AccountFilterTypeId: model.AccountFilterTypeId
    };


    // // Error handling and loading handling
    // const item = this.chartOfAccountList.find(x => x.ChartOfAccountNewId === model.ChartOfAccountNewId);
    // const index = this.chartOfAccountList.indexOf(item);
    // this.chartOfAccountList[index]._IsLoading = true;
    // this.chartOfAccountList[index]._IsError = false;



    this.globalService
      .post(
        this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_EditChartOfAccount,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {

            // Error Handling
            // this.chartOfAccountList[index]._IsLoading = false;
            // this.chartOfAccountList[index]._IsError = false;

          } else if (response.StatusCode === 400) {

            this.toastr.error(response.Message);

            // Error Handling
            // this.chartOfAccountList[index]._IsLoading = false;
            // this.chartOfAccountList[index]._IsError = true;

          }

        },
        error => {
          // error handling

          this.toastr.error('Someting went wrong! Try again');

          // Error Handling
            // this.chartOfAccountList[index]._IsLoading = false;
            // this.chartOfAccountList[index]._IsError = true;


        }
      );
  }
  //#endregion

  //#region "onAddMainLevelAccount"
  onAddMainLevelAccount() {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      AccountName: '',
      ParentID: -1,
      AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
      AccountLevelId: AccountLevels.MainLevel,
      AccountTypeId: null,
      AccountFilterTypeId: null,
      };
    this.addMainLevelAccountDetail(obj);
  }
  //#endregion

  //#region "onAddSubLevelClicked"
  onAddSubLevelClicked(mainLevelData: any, controlLevelData: any, data: any) {
  }
  //#endregion

  //#region "onAddControlLevelAccount"
  onAddControlLevelAccount(mainLevelData: ChartOfAccountModel) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      AccountName: '',
      ParentID: mainLevelData.ChartOfAccountNewId,
      AccountLevelId: AccountLevels.ControlLevel,
      AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
      AccountTypeId: null,
      AccountFilterTypeId: null
    };

    this.addControlLevelAccountDetail(mainLevelData, obj);


  }
  //#endregion

  //#region "onAddSubLevelAccount"
  onAddSubLevelAccount(mainLevelData: any, controlLevelData: ChartOfAccountModel) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      AccountName: '',
      ParentID: controlLevelData.ChartOfAccountNewId,
      AccountLevelId: AccountLevels.SubLevel,
      AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
      AccountTypeId: null,
      AccountFilterTypeId: null
    };

    this.addSubLevelAccountDetail(mainLevelData, controlLevelData, obj);
  }
  //#endregion

  //#region "onAddInputLevelAccount"
  onAddInputLevelAccount(
    mainLevelData: ChartOfAccountModel,
    controlLevelData: ChartOfAccountModel,
    subLevelData: ChartOfAccountModel
  ) {
    const obj: ChartOfAccountModel = {
      ChartOfAccountNewId: 0,
      AccountName: '',
      ParentID: subLevelData.ChartOfAccountNewId,
      AccountLevelId: AccountLevels.InputLevel,
      AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
      AccountTypeId: null,
      AccountFilterTypeId: null
    };
    // this.addAnyAccountDetail(obj);
    this.addInputLevelAccountDetail(mainLevelData, controlLevelData, subLevelData, obj);
  }
  //#endregion

  //#region "onBlurAddControlLevel"
  onBlurAddControlLevel(mainLevelData: any, data: any) {
    if (data !== '') {

      const controlLevelDetail: ChartOfAccountModel = {
        ChartOfAccountNewId: 0,
        AccountName: data,
        ParentID: mainLevelData.ChartOfAccountNewId,
        AccountLevelId: AccountLevels.ControlLevel,
        AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
        AccountTypeId: null,
        AccountFilterTypeId: null
      };

      this.addAnyAccountDetail(controlLevelDetail);
    }
  }
  //#endregion

  //#region "onBlurEditMainLevel_AccountName"
  onBlurEditMainLevelAccountName(mainLevelData: any, data: any) {
    if (data !== '') {
      const mainLevelDetail: ChartOfAccountModel = {
        ChartOfAccountNewId: mainLevelData.ChartOfAccountNewId,
        AccountName: data,
        ParentID: mainLevelData.ParentID,
        AccountLevelId: AccountLevels.MainLevel,
        AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
        AccountTypeId: mainLevelData.AccountTypeId,
        AccountFilterTypeId: mainLevelData.AccountFilterTypeId
      };

      this.editMainLevelAccountDetail(mainLevelDetail);
    }
  }
  //#endregion

  //#region "onBlurEditControlLevel_AccountName"
  onBlurEditControlLevelAccountName(mainLevelData: ChartOfAccountModel, controlLevelData: any, data: any) {
    if (data !== '') {

      const controlLevelDetail: ChartOfAccountModel = {
        ChartOfAccountNewId: controlLevelData.ChartOfAccountNewId,
        AccountName: data,
        ParentID: controlLevelData.ParentID,
        AccountLevelId: AccountLevels.ControlLevel,
        AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
        AccountTypeId: controlLevelData.AccountTypeId,
        AccountFilterTypeId: controlLevelData.AccountFilterTypeId
      };

      // this.editAnyAccountDetail(controlLevelDetail);
      this.editControlLevelAccountDetail(mainLevelData, controlLevelDetail);
    }
  }
  //#endregion

  //#region "onBlurEditSubLevel_AccountName"
  onBlurEditSubLevelAccountName(mainLevelData: any, controlLevelData: any, subLevelData: any, data: any) {
    if (data !== '') {

      const subLevelDetail: ChartOfAccountModel = {
        ChartOfAccountNewId: subLevelData.ChartOfAccountNewId,
        AccountName: data,
        ParentID: subLevelData.ParentID,
        AccountLevelId: AccountLevels.SubLevel,
        AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
        AccountTypeId: subLevelData.AccountTypeId,
        AccountFilterTypeId: subLevelData.AccountFilterTypeId
      };

      // this.editAnyAccountDetail(subLevelDetail);
      this.editSubLevelAccountDetail(mainLevelData, controlLevelData, subLevelDetail);
    }
  }
  //#endregion

  //#region "onBlurEditSubLevel_AccountFilterType"
  onBlurEditSubLevelAccountFilterType(
    mainLevelData: ChartOfAccountModel,
    controlLevelData: ChartOfAccountModel,
    subLevelData: ChartOfAccountModel,
    data: any
  ) {

    console.log('subLevelData -- ', subLevelData);

    if (data !== '') {

      const subLevelDetail: ChartOfAccountModel = {
        ChartOfAccountNewId: subLevelData.ChartOfAccountNewId,
        AccountName: subLevelData.AccountName,
        ParentID: subLevelData.ParentID,
        AccountLevelId: AccountLevels.SubLevel,
        AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
        AccountTypeId: subLevelData.AccountTypeId,
        AccountFilterTypeId: data
      };

      console.log('AccountFilterType', subLevelDetail);

      this.editSubLevelAccountDetail(mainLevelData, controlLevelData, subLevelDetail);
      // this.editAnyAccountDetail(controlLevelDetail);
    }
  }
  //#endregion

  //#region "onBlurEditSubLevel_AccountType"
  onBlurEditSubLevelAccountType(
    mainLevelData: ChartOfAccountModel,
    controlLevelData: ChartOfAccountModel,
    subLevelData: ChartOfAccountModel,
    data: any
  ) {

    console.log(data);

    if (data !== '') {

      const subLevelDetail: ChartOfAccountModel = {
        ChartOfAccountNewId: subLevelData.ChartOfAccountNewId,
        AccountName: subLevelData.AccountName,
        ParentID: subLevelData.ParentID,
        AccountLevelId: AccountLevels.SubLevel,
        AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
        AccountTypeId: data,
        AccountFilterTypeId: subLevelData.AccountFilterTypeId
      };

      console.log('AccountType', subLevelDetail);

      this.editSubLevelAccountDetail(mainLevelData, controlLevelData, subLevelDetail);
      // this.editAnyAccountDetail(controlLevelDetail);
    }
  }
  //#endregion

  //#region "onBlurEditInputLevelAccountName"
  onBlurEditInputLevelAccountName(mainLevelData: any, controlLevelData: any, subLevelData: any, inputLevelData: any, data: any) {
    if (data !== '') {

      const subLevelDetail: ChartOfAccountModel = {
        ChartOfAccountNewId: inputLevelData.ChartOfAccountNewId,
        AccountName: data,
        ParentID: inputLevelData.ParentID,
        AccountLevelId: AccountLevels.InputLevel,
        AccountHeadTypeId: this.ACCOUNT_HEAD_TYPE,
        AccountTypeId: inputLevelData.AccountTypeId,
        AccountFilterTypeId: inputLevelData.AccountFilterTypeId
      };

      this.editInputLevelAccountDetail(mainLevelData, controlLevelData, subLevelData, subLevelDetail);

      // this.editAnyAccountDetail(subLevelDetail);
    }
  }
  //#endregion

    //#region "openAddAccountDialog"
    openAddAccountDialog(AccountLevel: number, mainLevelData: any, controlLevelData: any, subLevelData: any): void {
    // NOTE: It passed the data into the Add Voucher Model

    const dialogRef = this.dialog.open(AddAccountComponent, {
      width: '450px',
      data: {
        AccountHeadType: this.ACCOUNT_HEAD_TYPE,
        AccountList: this.chartOfAccountList,
        mainLevelData: mainLevelData,
        AccountLevel: AccountLevel,
        controlLevelData: controlLevelData,
        subLevelData: subLevelData
      },
      autoFocus: false
    });

    // dialogRef.componentInstance.onListRefresh.subscribe(() => {
    //   this.getSavedExchangeRatesDate();
    // });

    dialogRef.afterClosed().subscribe(result => {
    });
  }
  //#endregion

  //#region "openAddAccountDialog"
  onAddMainLevelAccountDialog() {

    this.openAddAccountDialog(AccountLevels.MainLevel, null, null, null);
  }
 //#endregion

   //#region "openAddAccountDialog"
  onAddControlLevelAccountDialog(mainLevelData: ChartOfAccountModel) {

    this.openAddAccountDialog(AccountLevels.ControlLevel, mainLevelData, null, null);
  }
 //#endregion

  //#region "openAddAccountDialog"
  onAddSubLevelAccountDialog(mainLevelData: any, controlLevelData: ChartOfAccountModel) {

    this.openAddAccountDialog(AccountLevels.SubLevel, mainLevelData, controlLevelData, null);
  }
 //#endregion

 //#region "onAddInputLevelAccountDialog"
 onAddInputLevelAccountDialog(
  mainLevelData: ChartOfAccountModel,
  controlLevelData: ChartOfAccountModel,
  subLevelData: ChartOfAccountModel
) {
  this.openAddAccountDialog(AccountLevels.InputLevel, mainLevelData, controlLevelData, subLevelData);
}
//#endregion

  onDeleteChartOfAccount(accountDetail: ChartOfAccountModel) {
    this.deleteChartOfAccount(accountDetail);
  }

  //#region "deleteChartOfAccount"
  deleteChartOfAccount(accountDetail: ChartOfAccountModel) {

    this.globalService
    .post(this.appUrl.getApiUrl() + GLOBAL.API_ChartOfAccount_DeleteChartOfAccount, accountDetail.ChartOfAccountNewId)
    .subscribe(
      response => {
        if (response.StatusCode === 200) {
          // do something
        } else if (response.StatusCode === 400) {
          this.toastr.error(response.Message);
        }
      },
      error => {

      });
  }
  //#endregion


//#region "onDeleteMainLevel"
onDeleteMainLevel(mainLevelData: ChartOfAccountModel) {
    this.deleteMainLevelAccountDetail(mainLevelData);
  }
//#endregion

//#region "onDeleteControlLevel"
onDeleteControlLevel(mainLevelData: ChartOfAccountModel, controlLevelData: ChartOfAccountModel) {
    this.deleteControlLevelAccountDetail(mainLevelData, controlLevelData);
  }
//#endregion

  //#region "onDeleteSubLevel"
  onDeleteSubLevel(mainLevelData: ChartOfAccountModel, controlLevelData: ChartOfAccountModel, subLevelData: ChartOfAccountModel) {
    this.deleteSubLevelAccountDetail(mainLevelData, controlLevelData, subLevelData);
  }
//#endregion

  //#region "onDeleteInputLevel"
  onDeleteInputLevel(mainLevelData: ChartOfAccountModel,
                    controlLevelData: ChartOfAccountModel,
                    subLevelData: ChartOfAccountModel,
                    inputLevelData: ChartOfAccountModel ) {
    this.deleteInputLevelAccountDetail(mainLevelData, controlLevelData, subLevelData, inputLevelData);
  }
//#endregion


  // mouseEnter(event) {
  //   event.fromElement.hidden = true;
  //   // this.showHideDeleteIcon = true;
  // }

  // mouseLeave(event) {
  //   event.fromElement.hidden = false;
  //   // this.showHideDeleteIcon = false;
  // }

}
