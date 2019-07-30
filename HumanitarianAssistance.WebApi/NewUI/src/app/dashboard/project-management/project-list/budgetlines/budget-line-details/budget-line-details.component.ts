import {
  Component,
  OnInit,
  Input,
  EventEmitter,
  Output,
  OnChanges,
  HostListener,
} from '@angular/core';
import {
  ICurrencyListModel,
  IProjectJobModel,
  IBudgetLineModel,
  ITransactionDetailModel
} from '../models/budget-line.models';
import { BudgetLineService } from '../budget-line.service';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-budget-line-details',
  templateUrl: './budget-line-details.component.html',
  styleUrls: ['./budget-line-details.component.scss']
})
export class BudgetLineDetailsComponent implements OnInit, OnChanges {
  //#region "variables"
  @Input() budgetId: number;
  @Input() currencyList: ICurrencyListModel[] = [];
  @Input() projectJobList: IProjectJobModel[] = [];
  @Input() Projectid: any;
  @Input() budgetDetailObj: any;

  @Output() budgetDetailChanged = new EventEmitter<IBudgetLineModel>();
  @Output() transactiondetail = new  EventEmitter<any>();

  budgetLineDetail: IBudgetLineModel;
  transactionDetailList: ITransactionDetailModel[] = [];
  transactionDebit: ITransactionDetailModel[] = [];
  transactionCredit: ITransactionDetailModel[] = [];
  selectedCurrency: number = null;

  // "budgetDetailLoader"
  budgetDetailLoader = false;
  editBudgetDetailLoader = false;
  totalExpenditure: number;
  showEditTempDetail = false;
  transactionListLoaderFlag = false;
  noDataFoundFlag = false;

  // screen sizes
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  //#endregion

  constructor(
    public dialog: MatDialog,
    private budgetService: BudgetLineService,
    private toastr: ToastrService
  ) {}

  //#region "ngOnInit"
  ngOnInit() {
    this.initForm();
  //  this.getProjectTransactionList(this.Projectid);
  }
  //#endregion

  //#region "ngOnChanges"
  ngOnChanges() {
    if (
      this.budgetId !== 0 &&
      this.budgetId !== null &&
      this.budgetId !== undefined
    ) {
      this.getBudgetDetailByBudgetId(this.budgetId);
    }

    this.getTransactionList(this.budgetDetailObj);
  }
  //#endregion

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "initForm"
  initForm() {
    this.budgetLineDetail = {
      BudgetLineId: null,
      BudgetCode: null,
      BudgetName: null,
      ProjectJobId: null,
      InitialBudget: 0,
      ProjectId: null,
      CurrencyId: null,
      ProjectJobCode: null,
      ProjectJobName: null,
      CurrencyName: null,
      CreatedDate: null,
      DebitPercentage: 0
    };
  }
  //#endregion

  //#region "getBudgetDetailByBudgetId"
  getBudgetDetailByBudgetId(budgetId: number) {
    this.budgetDetailLoader = true;

    this.budgetService.GetBudgetDetailByBudgetIdId(budgetId).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach((element: IBudgetLineModel) => {
            this.budgetLineDetail = {
              BudgetLineId: element.BudgetLineId,
              BudgetCode: element.BudgetCode,
              BudgetName: element.BudgetName,
              CurrencyId: element.CurrencyId,
              CurrencyName: element.CurrencyName,
              InitialBudget: element.InitialBudget,
              ProjectId: element.ProjectId,
              ProjectJobCode: element.ProjectJobCode,
              ProjectJobId: element.ProjectJobId,
              ProjectJobName: element.ProjectJobName,
              CreatedDate: element.CreatedDate,
              DebitPercentage: element.DebitPercentage
            };
            this.selectedCurrency = this.budgetLineDetail.CurrencyId;
          });
        } else if (response.statusCode === 400 && response.data === null) {
          this.toastr.warning(response.message);
        }
        this.budgetDetailLoader = false;
      },
      error => {
        this.budgetDetailLoader = false;
      }
    );
  }

  //#endregion

  //#region "onBudgetLineValuechange"
  onBudgetLineValuechange() {
    this.editBudgetDetailById(this.budgetLineDetail);
  }
  //#endregion

  //#region "editBudgetDetailById"
  editBudgetDetailById(data: IBudgetLineModel) {
    this.editBudgetDetailLoader = true;
    this.budgetService.EditBudgetLineDetailById(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          // To get updated transaction list
          this.getTransactionList(data);
          // data.Expenditure = this.getTotalExpenditures;
          // this.budgetDetailChanged.emit(data);
        } else if (response.statusCode === 400) {
          this.toastr.warning(response.message);
        }
        this.editBudgetDetailLoader = false;
      },
      error => {
        this.toastr.error('Someting went wrong');
        this.editBudgetDetailLoader = false;
      }
    );
  }
  //#endregion

  //#region "getProjectTransactionList" pk comment
  // getProjectTransactionList(projecId) {
  //   debugger;
  //   this.transactionDetailList = [];
  //   this.budgetService.GetTransationListByProjectId(projecId).subscribe(
  //     (response: IResponseData) => {
  //       if (response.statusCode === 200 && response.data !== null) {
  //         console.log(response.data);
  //         response.data.forEach((element: ITransactionDetailModel) => {
  //           this.transactionDetailList.push({
  //             UserName: element.UserName,
  //             Credit: element.Credit,
  //             Debit: element.Debit,
  //             CurrencyName: element.CurrencyName,
  //             CurrencyId: element.CurrencyId,
  //             TransactionDate:
  //               element.TransactionDate != null
  //                 ? new Date(
  //                     new Date(element.TransactionDate).getTime() -
  //                       new Date().getTimezoneOffset() * 60000
  //                   )
  //                 : null
  //           });
  //         });
  //       }
  //       // this.BudgetListLoaderFlag = false;
  //     },
  //     error => {
  //       // this.BudgetListLoaderFlag = false;
  //     }
  //   );
  // }
  //#endregion

  //#region "getProjectTransactionList"
  getTransactionList(BudgetLineDetailList) {
    this.noDataFoundFlag = false;
    if (BudgetLineDetailList !== undefined) {
    this.transactionDetailList = [];
      this.budgetService.GetTransationList(BudgetLineDetailList).subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach((element: ITransactionDetailModel) => {
              this.transactionDetailList.push({
                UserName: element.UserName,
                Credit: element.Credit,
                Debit: element.Debit,
                DebitPercentage: this.getDebitPercentage,
                CurrencyName: element.CurrencyName,
                CurrencyId: element.CurrencyId,
                TransactionDate:
                  element.TransactionDate != null
                    ? new Date(
                        new Date(element.TransactionDate).getTime() -
                          new Date().getTimezoneOffset() * 60000
                      )
                    : null,
              });
            });
            this.transactiondetail.emit(this.transactionDetailList);
          } else if (response.statusCode === 120) {
           this.noDataFoundFlag = true;
            // this.toastr.warning(response.message);
          }
          this.selectedCurrency = BudgetLineDetailList.CurrencyId;
          BudgetLineDetailList.Expenditure = this.getTotalExpenditures;
          this.budgetDetailChanged.emit(BudgetLineDetailList);

          // this.BudgetListLoaderFlag = false;
        },
        error => {
          // this.BudgetListLoaderFlag = false;
          this.toastr.error(error.message);
        }
      );
    }
  }
  //#endregion

//#region "Calculate debit percentage"
get getDebitPercentage(): number {
  return (this.getTotalExpenditures / this.budgetLineDetail.InitialBudget) * 100;
}
//#endregion

  //#region "getTotalExpenditures"
  get getTotalExpenditures(): number {
    return this.transactionDetailList.reduce((a, { Debit }) => a + Debit, 0);
  }

  //#endregion

  //#region "getInitialBudget"
 get getInitialBudget() {
    return this.transactionDetailList.reduce((a, { Credit }) => a + Credit, 0);
  }
  //#endregion

  //#region "getTotalBalance"
  getTotalBalance() {
    // return this.getTotalExpenditures() - this.getInitialBudget();
    return this.budgetLineDetail.InitialBudget - this.getTotalExpenditures;
  }
  //#endregion

  showEditDetail(data: any, ev: any) {
    if (ev === true && this.showEditTempDetail === false) {
      this.showEditTempDetail = true;
    } else {
      this.showEditTempDetail = false;
    }
  }
}
