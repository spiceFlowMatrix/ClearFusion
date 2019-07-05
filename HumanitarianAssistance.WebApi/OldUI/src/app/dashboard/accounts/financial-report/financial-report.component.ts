import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AccountsService, Tab } from '../accounts.service';
import {
  Notes,
  BalanceType,
  FinancialStatement
} from '../../../shared/enums';
import { CodeService } from '../../code/code.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService } from '../../../service/common.service';
declare let jsPDF;
declare var $;

@Component({
  selector: 'app-financial-report',
  templateUrl: './financial-report.component.html',
  styleUrls: ['./financial-report.component.css']
})
export class FinancialReportComponent implements OnInit {
  @ViewChild('printsection') el: ElementRef;
  @ViewChild('printsection1') el1: ElementRef;
  showSelectedTab = 0;
  tabs: Tab[];

  // dataSource
  tab4: any;

  // Variables
  windows: any;
  NotesDataSource: any;
  NotesArr: any[];
  BalanceTypeArr: any[];
  FinancialStatementArr: any[];
  AccountsArr: any[];
  BalanceSheetDataSource: any;
  accountTypeDropdown: any[];
  financialYearArr: any[];
  currencyModel: any[];

  IncomeFromDonor: any;
  IncomeFromProjects: any;
  ProfitOnBankDeposits: any;
  IncomeExpenditureFund: any;

  IncomeFromDonorTotal: number;
  IncomeFromProjectsTotal: number;
  ProfitOnBankDepositsTotal: number;
  IncomeExpenditureFundTotal: number;

  ExcessofExpenditureTotal: number;


  CapitalAssetsWrittenOff: any;
  CurrentAssets: any;
  Funds: any;
  EndownmentFund: any;
  ReserveAccountAdjustment: any;
  LongtermLiability: any;
  CurrentLiability: any;
  ReserveAccount: any;

  CapitalAssetsWrittenOffTotal: number;
  CurrentAssetsTotal: number;
  FundsTotal: number;
  EndownmentFundTotal: number;
  ReserveAccountAdjustmentTotal: number;
  LongtermLiabilityTotal: number;
  CurrentLiabilityTotal: number;
  ReserveAccountTotal: number;

  PropertyAndCapitalTotal = 0;
  FundsAndLiabilitiesTotal = 0;
  DifferenceCategoryTotal = 0;
  TotalFundsAndLiabilitiescategoryTotal = 0;


  // SelectedCurrency: any;
  balanceSheetFilterForm: BalanceSheetFilterModel;
  incomeExpenseFilterForm: IncomeExpenseFilterModel;

  incomeExpenseDateRange: any;

  SetCurrencyName: any;
  SetFinancialYearDate: any;
  setBalanceSheetYear: any;
  setIncomeExpensefromYear: any;
  setIncomeExpensetoYear: any;
  DetailOfNotesDataSource: any[];
  setClass = true;
  IncomeExpenseDataSource: any[];

  detailOfNotesReportDataSource: any[];

  //#region Balance Sheet Variables
  currentAsset: any;
  currentAssetTotal: any = 0;
  Current_Libility: any;
  Current_LibilityTotal: any = 0;
  Equity: any;
  EquityTotal: any = 0;
  Expense: any;
  ExpenseTotal: any = 0;
  // Funds: any;
  // FundsTotal: any = 0;
  Income: any;
  IncomeTotal: any = 0;
  Libility: any;
  LibilityTotal: any = 0;
  Long_Term_Libility: any;
  Long_Term_LibilityTotal: any = 0;
  Reserve_Account: any;
  Reserve_AccountTotal: any = 0;
  Reserve_Account_Adjustment: any;
  Reserve_Account_AdjustmentTotal: any = 0;
  Revenue: any;
  RevenueTotal: any = 0;
  GrandTotal: any = 0;
  GrandTotalIncomeAndExpense: any = 0;
  TotalIncomeRevenue: any = 0;
  TotalFundsAndLiabilities: any = 0;
  //#endregion Balance Sheet Variables

  // Loader
  financialReportLoader = false;

  // Flag
  detailsOfNotePdfFlag = false;

  // Select Type
  selectTypeArr: any[];
  selectedDate: any;
  officeDropdown: any[];
  selectedOffices: any[];

  constructor(
    private accountservice: AccountsService,
    private codeService: CodeService,
    private setting: AppSettingsService,
    private commonService: CommonService,
    private toastr: ToastrService
  ) {
    this.windows = window;
    this.setBalanceSheetYear = new Date().getFullYear().toString();

    this.selectedDate = new Date();

    //#region "Inserting Enums into array"
    this.NotesArr = [];
    for (const i in Notes) {
      if (typeof Notes[i] === 'number') {
        this.NotesArr.push({ NoteId: <any>Notes[i], NoteName: i });
      }
    }

    this.BalanceTypeArr = [];
    for (const i in BalanceType) {
      if (typeof BalanceType[i] === 'number') {
        this.BalanceTypeArr.push({
          BalanceTypeId: <any>BalanceType[i],
          BalanceTypeName: i
        });
      }
    }

    this.FinancialStatementArr = [];
    for (const i in FinancialStatement) {
      if (typeof FinancialStatement[i] === 'number') {
        this.FinancialStatementArr.push({
          FinancialStatementId: <any>FinancialStatement[i],
          FinancialStatementName: i
        });
      }
    }

    //#endregion "Inserting Enums into array"

    this.NotesDataSource = [
      {
        NoteId: null,
        Account: null,
        Narration: null,
        Notes: null,
        BalanceType: null,
        BalanceTypeName: null,
        FinancialReportType: null,
        FinancialReportTypeName: null,
        AccountType: null,
        AccountTypeName: null
      }
    ];
    this.tabs = [
      {
        id: 0,
        text: 'Notes'
      },
      {
        id: 1,
        text: 'Balance Sheet'
      },
      {
        id: 2,
        text: 'Income/Expense'
      },
      {
        id: 3,
        text: 'Details Of Notes'
      }
    ];

    this.selectTypeArr = [
      {
        id: 1,
        text: 'From the starting'
      },
      {
        id: 2,
        text: 'Within the financial year'
      }
    ];
    // this.BalanceSheetDataSource = [
    //     {
    //         AccountType: null,
    //         Narration: null,
    //         Note: null,
    //         Balance: null
    //     }
    // ];

    this.tab4 = {
      store: {
        type: 'array',
        key: 'ID',
        data: this.accountservice.getFinancial_Income()
      }
    };
  }

  ngOnInit() {
    this.initializeForm();

    this.getCurrencyCodeList();
    this.getFinancialYear();
    this.getAccountType();
    this.GetAccountDetails();
    this.getAllNotes();
    this.getOfficeCodeList();
  }

  initializeForm() {
    this.balanceSheetFilterForm = {
      Currency: 0,
      Date: new Date(),
      SelectType: 2,
      OfficeList: []
    };

    this.incomeExpenseFilterForm = {
      Currency: 0,
      StartDate: null,
      EndDate: null,
      OfficeList: []
    };
  }

  initializeReportVariables() {
    this.currentAsset = {};
    this.currentAssetTotal = 0;
    this.Current_Libility = {};
    this.Current_LibilityTotal = 0;
    this.Equity = {};
    this.EquityTotal = 0;
    this.Expense = {};
    this.ExpenseTotal = 0;
    this.Funds = {};
    this.FundsTotal = 0;
    this.Income = {};
    this.IncomeTotal = 0;
    this.Libility = {};
    this.LibilityTotal = 0;
    this.Long_Term_Libility = {};
    this.Long_Term_LibilityTotal = 0;
    this.Reserve_Account = {};
    this.Reserve_AccountTotal = 0;
    this.Reserve_Account_Adjustment = {};
    this.Reserve_Account_AdjustmentTotal = 0;
    this.Revenue = {};
    this.RevenueTotal = 0;
    this.GrandTotal = 0;
    this.TotalIncomeRevenue = 0;
    this.TotalFundsAndLiabilities = 0;
  }

  selectTab(e) {
    this.showSelectedTab = e.itemIndex; // showSelectedTab for changing the tab value
    if (this.showSelectedTab === 1) {
      if (this.currencyModel != null && this.financialYearArr != null) {
        if (this.currencyModel.length > 0) {
          this.balanceSheetFilterForm = {
            Currency: this.currencyModel[0].CurrencyId,
            Date: new Date(),
            SelectType: this.selectTypeArr[1].id,
            OfficeList: this.selectedOffices
          };

          this.SetCurrencyName = this.currencyModel[0].CurrencyCode; // For report currency name
          this.SetFinancialYearDate = this.financialYearArr[0].FinancialYearName;
        }
      }
    }

    if (this.showSelectedTab === 2) {
      if (this.currencyModel != null && this.financialYearArr != null) {
        if (this.currencyModel.length > 0) {
          this.incomeExpenseFilterForm = {
            Currency: this.currencyModel[0].CurrencyId,
            StartDate: this.financialYearArr[0].StartDate,
            EndDate: this.financialYearArr[0].EndDate,
            OfficeList: this.selectedOffices
          };

          this.SetCurrencyName = this.currencyModel[0].CurrencyCode; // For report currency name
        }
      }
    }
  }

  getOfficeCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          if (data.data.OfficeDetailsList != null) {
            this.officeDropdown = [];

            const allOffices = [];
            const officeIds: any[] =
              localStorage.getItem('ALLOFFICES') != null
                ? localStorage.getItem('ALLOFFICES').split(',')
                : null;

            data.data.OfficeDetailsList.forEach(element => {
              allOffices.push(element);
            });

            officeIds.forEach(x => {
              const officeData = allOffices.filter(
                // tslint:disable-next-line:radix
                e => e.OfficeId === parseInt(x)
              )[0];
              this.officeDropdown.push(officeData);
            });

            this.selectedOffices = [];
            officeIds.forEach(x => {
              // tslint:disable-next-line:radix
              this.selectedOffices.push(parseInt(x));
            });

            // sort in Asc
            this.officeDropdown = this.commonService.sortDropdown(
              this.officeDropdown,
              'OfficeName'
            );
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  getFinancialYear() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllFinancialYearDetail
      )
      .subscribe(
        data => {
          this.financialYearArr = [];
          if (
            data.data.FinancialYearDetailList != null &&
            data.data.FinancialYearDetailList.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.FinancialYearDetailList.forEach(element => {
              this.financialYearArr.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  getCurrencyCodeList() {
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyModel = [];
          if (
            data.StatusCode === 200 &&
            data.data.CurrencyList != null &&
            data.data.CurrencyList.length > 0
          ) {
            data.data.CurrencyList.forEach(element => {
              this.currencyModel.push(element);
            });
            // this.SelectedCurrency = this.currencyModel[0].CurrencyId;
            this.balanceSheetFilterForm.Currency = this.currencyModel[0].CurrencyId;
            this.incomeExpenseFilterForm.Currency = this.currencyModel[0].CurrencyId;
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  // #region "Notes"
  getAccountType() {
    this.codeService
      .getAccountType(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAllAccoutnType
      )
      .subscribe(
        data => {
          this.accountTypeDropdown = [];
          if (
            data.data.AccountTypeList != null &&
            data.data.AccountTypeList.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.AccountTypeList.forEach(element => {
              this.accountTypeDropdown.push(element);
            });

            // sort in Asc
            this.accountTypeDropdown = this.commonService.sortDropdown(
              this.accountTypeDropdown,
              'AccountTypeName'
            );
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  GetAccountDetails() {
    this.showFinancialReportLoader();
    this.accountservice
      .GetAccountDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Accounting_GetAccountDetails
      )
      .subscribe(
        data => {
          this.AccountsArr = [];
          if (
            data.StatusCode === 200 &&
            data.data.AccountDetailList.length > 0 &&
            data.data.AccountDetailList != null
          ) {
            data.data.AccountDetailList.forEach(element => {
              this.AccountsArr.push(element);
            });

            // sort in Asc
            this.AccountsArr = this.commonService.sortDropdown(
              this.AccountsArr,
              'AccountName'
            );
          }
          this.hideFinancialReportLoader();
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
          this.hideFinancialReportLoader();
        }
      );
  }
  logEvent(eventName, obj) {
    if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      this.editNote(value);
    }
    if (eventName === 'RowInserting') {
      this.addNote(obj.data);
    }
  }

  addNote(model) {
    const obj = {
      NoteId: model.NoteId,

      AccountCode: model.Account,
      Narration: model.Narration,
      Notes: model.Notes,

      BlanceType: 1,
      FinancialReportTypeId: 1,
      AccountTypeId: 1
    };
    this.codeService
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_Account_AddNotesDetails,
        obj
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Note Added Successfully!!!');
          }
          this.getAllNotes();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getAllNotes();
        }
      );
  }

  editNote(model) {
    const obj = {
      NoteId: model.NoteId,
      AccountCode: model.Account,
      Narration: model.Narration,
      Notes: model.Notes,
      BlanceType: model.BalanceType,
      FinancialReportTypeId: model.FinancialReportType,
      AccountTypeId: model.AccountType
    };
    this.codeService
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_Account_EditNotesDetails,
        obj
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Note Updated Successfully!!!');
          }
          this.getAllNotes();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getAllNotes();
        }
      );
  }

  getAllNotes() {
    this.showFinancialReportLoader();
    this.codeService
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_Account_GetAllNotesDetails
      )
      .subscribe(
        data => {
          this.NotesDataSource = [];
          if (
            data.data.NotesDetailsList != null &&
            data.StatusCode === 200 &&
            data.data.NotesDetailsList.length > 0
          ) {
            data.data.NotesDetailsList.forEach(element => {
              this.NotesDataSource.push({
                NoteId: element.NoteId,
                Account: element.AccountCode,
                Narration: element.Narration,
                Notes: element.Notes,
                BalanceType: element.BlanceType,
                FinancialReportType: element.FinancialReportTypeId,
                AccountType: element.AccountTypeId,
                ChartOfAccountCode: element.ChartOfAccountCode
              });
            });
          }
          this.hideFinancialReportLoader();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideFinancialReportLoader();
        }
      );
  }
  // #endregion "Notes"

  // #region "Balance Sheet"

  GetBalanceSheetDetails(date, CurrencyId, selectType, OfficeList) {
    this.showFinancialReportLoader();

    const balanceSheetData = {
      currencyid: CurrencyId,
      financialreporttype: 1,
      StartDate: null,
      EndDate: new Date(
        new Date(date).getFullYear(),
        new Date(date).getMonth(),
        new Date(date).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      SelectType: selectType,
      OfficeList: OfficeList
    };

    this.codeService
      .GetAllBalanceSheetDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Account_GetBalanceSheetDetails,
        balanceSheetData
      )
      .subscribe(
        data => {
          //#region "CapitalAssetsWrittenOff"
          this.CapitalAssetsWrittenOff = [];
          this.CapitalAssetsWrittenOffTotal = 0;

          if (data.data.BalanceSheet != null) {
            if (
              data.data.BalanceSheet.CapitalAssetsWrittenOff != null &&
              data.data.BalanceSheet.CapitalAssetsWrittenOff.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              this.CapitalAssetsWrittenOff =
                data.data.BalanceSheet.CapitalAssetsWrittenOff;
              data.data.BalanceSheet.CapitalAssetsWrittenOff.forEach(
                element => {
                  total = total + element.Balance;
                }
              );
              this.CapitalAssetsWrittenOffTotal = total;
            }
            //#endregion "Current Asset DataSource For Balance Sheet"

            //#region "CurrentAssets"
            this.CurrentAssets = [];
            this.CurrentAssetsTotal = 0;
            if (
              data.data.BalanceSheet.CurrentAssets != null &&
              data.data.BalanceSheet.CurrentAssets.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              this.CurrentAssets = data.data.BalanceSheet.CurrentAssets;
              data.data.BalanceSheet.CurrentAssets.forEach(element => {
                total = total + element.Balance;
              });
              this.CurrentAssetsTotal = total;
            }
            //#endregion "Current Asset DataSource For Balance Sheet"

            //#region "Funds"
            this.Funds = [];
            this.FundsTotal = 0;
            if (
              data.data.BalanceSheet.Funds != null &&
              data.data.BalanceSheet.Funds.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              this.Funds = data.data.BalanceSheet.Funds;
              data.data.BalanceSheet.Funds.forEach(element => {
                total = total + element.Balance;
              });
              this.FundsTotal = total;
            }
            //#endregion "Current Asset DataSource For Balance Sheet"

            //#region "EndownmentFund"
            this.EndownmentFund = [];
            this.EndownmentFundTotal = 0;
            if (
              data.data.BalanceSheet.EndownmentFund != null &&
              data.data.BalanceSheet.EndownmentFund.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              this.EndownmentFund = data.data.BalanceSheet.EndownmentFund;
              data.data.BalanceSheet.EndownmentFund.forEach(element => {
                total = total + element.Balance;
              });
              this.EndownmentFundTotal = total;
            }
            //#endregion "Current Asset DataSource For Balance Sheet"

            //#region "ReserveAccountAdjustment"
            this.ReserveAccountAdjustment = [];
            this.ReserveAccountAdjustmentTotal = 0;
            if (
              data.data.BalanceSheet.ReserveAccountAdjustment != null &&
              data.data.BalanceSheet.ReserveAccountAdjustment.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              data.data.BalanceSheet.ReserveAccountAdjustment.forEach(
                element => {
                  this.ReserveAccountAdjustment =
                    data.data.BalanceSheet.ReserveAccountAdjustment;
                  total = total + element.Balance;
                }
              );
              this.ReserveAccountAdjustmentTotal = total;
            }
            //#endregion "Current Asset DataSource For Balance Sheet"

            //#region "LongtermLiability"
            this.LongtermLiability = [];
            this.LongtermLiabilityTotal = 0;
            if (
              data.data.BalanceSheet.LongtermLiability != null &&
              data.data.BalanceSheet.LongtermLiability.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              data.data.BalanceSheet.LongtermLiability.forEach(element => {
                this.LongtermLiability =
                  data.data.BalanceSheet.LongtermLiability;
                total = total + element.Balance;
              });
              this.LongtermLiabilityTotal = total;
            }
            //#endregion

            //#region "CurrentLiability"
            this.CurrentLiability = [];
            this.CurrentLiabilityTotal = 0;
            if (
              data.data.BalanceSheet.CurrentLiability != null &&
              data.data.BalanceSheet.CurrentLiability.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              data.data.BalanceSheet.CurrentLiability.forEach(element => {
                this.CurrentLiability = data.data.BalanceSheet.CurrentLiability;
                total = total + element.Balance;
              });
              this.CurrentLiabilityTotal = total;
            }
            //#endregion

            //#region "ReserveAccount"
            this.ReserveAccount = [];
            this.ReserveAccountTotal = 0;
            if (
              data.data.BalanceSheet.ReserveAccount != null &&
              data.data.BalanceSheet.ReserveAccount.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              data.data.BalanceSheet.ReserveAccount.forEach(element => {
                this.ReserveAccount = data.data.BalanceSheet.ReserveAccount;
                total = total + element.Balance;
              });
              this.ReserveAccountTotal = total;
            }
            //#endregion

            this.PropertyAndCapitalTotal =
              this.CapitalAssetsWrittenOffTotal + this.CurrentAssetsTotal;

            this.FundsAndLiabilitiesTotal =
              this.FundsTotal +
              this.EndownmentFundTotal +
              this.ReserveAccountAdjustmentTotal +
              this.LongtermLiabilityTotal +
              this.CurrentLiabilityTotal +
              this.ReserveAccountTotal;

            this.DifferenceCategoryTotal =
              this.PropertyAndCapitalTotal - this.FundsAndLiabilitiesTotal;

            this.TotalFundsAndLiabilitiescategoryTotal =
              this.FundsAndLiabilitiesTotal - this.DifferenceCategoryTotal;
          }

          this.hideFinancialReportLoader();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideFinancialReportLoader();
        }
      );
  }

  selectedBalanceSheetCurrencyFilter(value) {
    if (value != null) {
      this.setBalanceSheetYear = new Date(value.Date).getFullYear().toString();

      this.balanceSheetFilterForm = {
        Currency: value.Currency,
        // Date: value.Date,
        Date: new Date(
          new Date(value.Date).getFullYear(),
          new Date(value.Date).getMonth(),
          new Date(value.Date).getDay()
        ),
        SelectType: value.SelectType,
        OfficeList: value.OfficeList
      };

      this.SetCurrencyName = this.currencyModel.filter(
        x => x.CurrencyId === value.Currency
      )[0].CurrencyCode;
      this.GetBalanceSheetDetails(
        value.Date,
        value.Currency,
        value.SelectType,
        value.OfficeList
      );
    }
  }

  print(): void {
    let printContents, popupWin;
    printContents = document.getElementById('print-content').innerHTML;
    popupWin = window.open('', '_blank', '');
    popupWin.document.open();
    popupWin.document.write(`
          <html>
            <head>
              <title></title>
              <style>
              //........Customized style.......
              </style>
            </head>
        <body onload="window.print();window.close()">${printContents}</body>
          </html>`);
    popupWin.document.close();
  }

  public pdfHtml() {
    const pdf = new jsPDF();
    const options = {
      // pagesplit: true
    };
    pdf.addHTML(this.el.nativeElement, 0, 0, options, () => {
      pdf.save('report.pdf');
    });
  }

  // #endregion "Balance Sheet"

  // #region "Income/Expenses Accounts"

  GetIncomeExpenseDetails(startDate, endDate, CurrencyId, officeList) {
    // var FinancialReportTypeId = 2;

    this.showFinancialReportLoader();

    const balanceSheetData = {
      currencyid: CurrencyId,
      financialreporttype: 2,
      StartDate: startDate,
      EndDate: endDate,
      OfficeList: officeList
    };

    this.codeService
      .GetAllBalanceSheetDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Account_GetBalanceSheetDetails,
        balanceSheetData
      )
      .subscribe(
        data => {
          if (data.data.BalanceSheet != null) {
            //#region "IncomeFromDonor"
            this.IncomeFromDonor = [];
            this.IncomeFromDonorTotal = 0;
            if (
              data.data.BalanceSheet.IncomeFromDonor != null &&
              data.data.BalanceSheet.IncomeFromDonor.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              this.IncomeFromDonor = data.data.BalanceSheet.IncomeFromDonor;
              data.data.BalanceSheet.IncomeFromDonor.forEach(element => {
                total = total + element.Balance;
              });
              this.IncomeFromDonorTotal = total;
            }
            //#endregion

            //#region "IncomeFromProjects"
            this.IncomeFromProjects = [];
            this.IncomeFromProjectsTotal = 0;
            if (
              data.data.BalanceSheet.IncomeFromProjects != null &&
              data.data.BalanceSheet.IncomeFromProjects.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              this.IncomeFromProjects =
                data.data.BalanceSheet.IncomeFromProjects;
              data.data.BalanceSheet.IncomeFromProjects.forEach(element => {
                total = total + element.Balance;
              });
              this.IncomeFromProjectsTotal = total;
            }
            //#endregion

            //#region "ProfitOnBankDeposits"
            this.ProfitOnBankDeposits = [];
            this.ProfitOnBankDepositsTotal = 0;
            if (
              data.data.BalanceSheet.ProfitOnBankDeposits != null &&
              data.data.BalanceSheet.ProfitOnBankDeposits.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              this.ProfitOnBankDeposits =
                data.data.BalanceSheet.ProfitOnBankDeposits;
              data.data.BalanceSheet.ProfitOnBankDeposits.forEach(element => {
                total = total + element.Balance;
              });
              this.ProfitOnBankDepositsTotal = total;
            }
            //#endregion

            //#region "IncomeExpenditureFund"
            this.IncomeExpenditureFund = [];
            this.IncomeExpenditureFundTotal = 0;
            if (
              data.data.BalanceSheet.IncomeExpenditureFund != null &&
              data.data.BalanceSheet.IncomeExpenditureFund.length > 0 &&
              data.StatusCode === 200
            ) {
              let total = 0;
              this.IncomeExpenditureFund =
                data.data.BalanceSheet.IncomeExpenditureFund;
              data.data.BalanceSheet.IncomeExpenditureFund.forEach(element => {
                total = total + element.Balance;
              });
              this.IncomeExpenditureFundTotal = total;
            }
            //#endregion

            this.TotalIncomeRevenue =
              this.IncomeFromDonorTotal +
              this.IncomeFromProjectsTotal +
              this.ProfitOnBankDepositsTotal;
            this.ExcessofExpenditureTotal =
              this.TotalIncomeRevenue - this.IncomeExpenditureFundTotal;
          }

          this.hideFinancialReportLoader();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideFinancialReportLoader();
        }
      );
  }

  selectedIncomeExpenseFilter(value) {
    if (value != null && this.incomeExpenseDateRange != null) {
      this.incomeExpenseFilterForm = {
        Currency: value.Currency,
        StartDate: new Date(
          new Date(this.incomeExpenseDateRange[0]).getFullYear(),
          new Date(this.incomeExpenseDateRange[0]).getMonth(),
          new Date(this.incomeExpenseDateRange[0]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        EndDate: new Date(
          new Date(this.incomeExpenseDateRange[1]).getFullYear(),
          new Date(this.incomeExpenseDateRange[1]).getMonth(),
          new Date(this.incomeExpenseDateRange[1]).getDate(),
          new Date().getHours(),
          new Date().getMinutes(),
          new Date().getSeconds()
        ),
        OfficeList: value.OfficeList
      };

      this.SetCurrencyName = this.currencyModel.filter(
        x => x.CurrencyId === value.Currency
      )[0].CurrencyCode;

      if (this.incomeExpenseFilterForm != null) {
        this.GetIncomeExpenseDetails(
          this.incomeExpenseFilterForm.StartDate,
          this.incomeExpenseFilterForm.EndDate,
          this.incomeExpenseFilterForm.Currency,
          this.incomeExpenseFilterForm.OfficeList
        );
      }
    } else {
      this.toastr.warning('Select Date Range');
    }
  }

  // #region "BalanceSheet Filter Data Changed"
  onFieldBalanceSheetChanged(e) {
    if (e != null) {
      this.setIncomeExpensefromYear = new Date(e.value).getFullYear();
    }
  }

  // #endregion

  // #region "IncomeExpense Filter Data Changed"
  onFieldIncomeExpenseChanged(e) {
    if (e != null) {
      this.setIncomeExpensefromYear =
        new Date(e[0]).getDate().toString() +
        '/' +
        (new Date(e[0]).getMonth() + 1).toString() +
        '/' +
        new Date(e[0]).getFullYear().toString();
      this.setIncomeExpensetoYear =
        new Date(e[1]).getDate().toString() +
        '/' +
        (new Date(e[1]).getMonth() + 1).toString() +
        '/' +
        new Date(e[1]).getFullYear().toString();
    }
  }

  // #endregion

  printIncomeExpense(): void {
    let printContents, popupWin;
    printContents = document.getElementById('print-content-incomeexpense')
      .innerHTML;
    popupWin = window.open('', '_blank', '');
    popupWin.document.open();
    popupWin.document.write(`
          <html>
            <head>
              <title></title>
              <style>
              //........Customized style.......
              </style>
            </head>
        <body onload="window.print();window.close()">${printContents}</body>
          </html>`);
    popupWin.document.close();
  }

  public pdfHtmlIncomeExpense() {
    const pdf = new jsPDF();
    const options = {
      // pagesplit: true
    };
    pdf.addHTML(this.el1.nativeElement, 0, 0, options, () => {
      pdf.save('report.pdf');
    });

    setTimeout(function() {
      this.setClass = false;
    }, 2000);
  }
  // #endregion "Income/Expenses Accounts"

  //#region "viewDetailsOfNotesPdf"
  viewDetailsOfNotesPdf() {
    this.showDetailsOfNotePdfFlag();
  }
  //#endregion

  //#region "show / hide"
  showFinancialReportLoader() {
    this.financialReportLoader = true;
  }
  hideFinancialReportLoader() {
    this.financialReportLoader = false;
  }

  showDetailsOfNotePdfFlag() {
    this.detailsOfNotePdfFlag = true;
  }
  hideDetailsOfNotePdfFlag() {
    this.detailsOfNotePdfFlag = false;
  }
  //#endregion
}

class BalanceSheetFilterModel {
  Date: any;
  Currency: number;
  SelectType: number;
  OfficeList: any;
}

class IncomeExpenseFilterModel {
  StartDate: any;
  EndDate: any;
  Currency: number;
  OfficeList: any;
}
