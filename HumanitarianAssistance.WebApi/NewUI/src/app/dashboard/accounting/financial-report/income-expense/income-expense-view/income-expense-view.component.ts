import {
  Component,
  OnInit,
  HostListener,
  ElementRef,
  ViewChild
} from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountHeadTypes_Enum } from 'src/app/shared/enum';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { NoteAccountBalanceModel, IDetailsOfNotesFinalList } from '../../models/note-account-balance-model';
import { ICurrencyList } from '../../../gain-loss-report/gain-loss-report.model';
import { IncomeExpenseService } from '../income-expense.service';
import { IResponseData } from '../../../vouchers/models/status-code.model';
import { CurrencyCodePipe } from 'src/app/shared/pipes/currency-code.pipe';
import { StaticUtilities } from 'src/app/shared/static-utilities';
// import * as jsPDF from 'jspdf';
// import 'jspdf-autotable';

declare const require: any;
const jsPDF = require('jspdf');
require('jspdf-autotable');

@Component({
  selector: 'app-income-expense-view',
  templateUrl: './income-expense-view.component.html',
  styleUrls: ['./income-expense-view.component.scss']
})
export class IncomeExpenseViewComponent implements OnInit {
  //#region "variables"
  @ViewChild('incomeExpenseReportTemplate') elementRef: ElementRef;
  // CONST
  INCOME_ID: number = AccountHeadTypes_Enum.Income;
  EXPENSE_ID: number = AccountHeadTypes_Enum.Expense;

  // DATASOURCE
  incomeList: NoteAccountBalanceModel[] = [];
  expenseList: NoteAccountBalanceModel[] = [];
  currencyList: ICurrencyList[] = [];

  // Notes
  detailsOfNotesFinalList: IDetailsOfNotesFinalList[] = [];

  totalIncome = 0;
  totalExpenses = 0;
  totalNett = 0;

  showa = [];
  showb = [];
  asOfDate: string;
  upToDate: string;
  selectedCurrency: number;

  detailOfNoteFlag = false;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  //#endregion

  constructor(
    private globalService: GlobalService,
    private incomeExpenseService: IncomeExpenseService,
    private appUrl: AppUrlService,
    private currencyCodePipe: CurrencyCodePipe,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.asOfDate = params.asOfDate;
      this.upToDate = params.upToDate;
      this.selectedCurrency = +params.currency;
      this.getAllCurrency();
      this.getIncomeAccountBalances();
      this.getExpenseAccountBalances();

      this.getDetailOfNotes();
    });
  }

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

  getSum(list, field): number {
    let sum = 0;
    if (list != null) {
      for (let i = 0; i < list.length; i++) {
        sum += list[i][field];
      }
    }

    return sum;
  }

  //#region "getAllCurrency"
  getAllCurrency() {
    this.currencyList = [];
    this.incomeExpenseService.GetCurrencyList().subscribe(
      (response: IResponseData) => {
        this.currencyList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.currencyList.push({
              CurrencyId: element.CurrencyId,
              CurrencyCode: element.CurrencyCode,
              CurrencyName: element.CurrencyName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "get"
  getIncomeAccountBalances() {
    const dataModel = {
      id: AccountHeadTypes_Enum.Income,
      asOfDate: StaticUtilities.getLocalDate(this.asOfDate),
      upToDate: StaticUtilities.getLocalDate(this.upToDate),

      currency: this.selectedCurrency
    };
    this.globalService
      .getListByIdAndDate(
        this.appUrl.getApiUrl() +
          GLOBAL.API_FinancialReport_GetAllAccountIncomeExpensesByCategory,
        dataModel
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          if (data.data.NoteAccountBalances != null) {
            if (data.data.NoteAccountBalances.length > 0) {
              const incomeList: NoteAccountBalanceModel[] = [];

              data.data.NoteAccountBalances.forEach(element => {
                incomeList.push({
                  NoteId: element.NoteId,
                  NoteName: element.NoteName,
                  NoteHeadId: element.NoteHeadId,
                  AccountBalances: element.AccountBalances,
                  NoteBalance: this.getSum(element.AccountBalances, 'Balance')
                });
              });

              this.incomeList = incomeList.filter(
                x => x.NoteHeadId === AccountHeadTypes_Enum.Income
              );
            }
          }
          this.totalIncome = this.getSum(this.incomeList, 'NoteBalance');
          this.totalNett = this.totalIncome - this.totalExpenses;
        } else if (data.StatusCode === 400) {
          this.toastr.error(data.Message);
        }
      });
  }
  //#endregion

  //#region "get"
  getExpenseAccountBalances() {
    const dataModel = {
      id: AccountHeadTypes_Enum.Expense,
      asOfDate: this.asOfDate,
      upToDate: this.upToDate,
      currency: this.selectedCurrency
    };
    this.globalService
      .getListByIdAndDate(
        this.appUrl.getApiUrl() +
          GLOBAL.API_FinancialReport_GetAllAccountIncomeExpensesByCategory,
        dataModel
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          if (data.data.NoteAccountBalances != null) {
            if (data.data.NoteAccountBalances.length > 0) {
              const expenseList: NoteAccountBalanceModel[] = [];

              data.data.NoteAccountBalances.forEach(element => {
                expenseList.push({
                  NoteId: element.NoteId,
                  NoteName: element.NoteName,
                  NoteHeadId: element.NoteHeadId,
                  AccountBalances: element.AccountBalances,
                  NoteBalance: this.getSum(element.AccountBalances, 'Balance')
                });
              });

              this.expenseList = expenseList.filter(
                x => x.NoteHeadId === AccountHeadTypes_Enum.Expense
              );
            }
          }
          this.totalExpenses = this.getSum(this.expenseList, 'NoteBalance');
          this.totalNett = this.totalIncome - this.totalExpenses;
        } else if (data.StatusCode === 400) {
          this.toastr.error(data.Message);
        }
      });
  }
  //#endregion

    //#region "getDetailOfNotes"
    getDetailOfNotes() {
      const dataModel = {
        CurrencyId: this.selectedCurrency,
        TillDate: StaticUtilities.getLocalDate(this.upToDate)
      };

      this.detailOfNoteFlag  = true;

      this.incomeExpenseService
        .GetDetailOfNotes(dataModel)
        .subscribe((response: IResponseData) => {
          if (response.statusCode === 200) {
            if (response.data != null) {
              if (response.data.length > 0) {
                response.data.forEach((element: IDetailsOfNotesFinalList) => {
                  this.detailsOfNotesFinalList.push({
                    NoteName: element.NoteName,
                    TotalDebits: element.TotalDebits,
                    TotalCredits: element.TotalCredits,
                    Balance: element.Balance,
                    AccountSummary: element.AccountSummary
                  });
                });
              }
            }
          } else if (response.statusCode === 400) {
            this.toastr.error(response.message);
          }
          this.detailOfNoteFlag  = false;

        }, error => {

      this.detailOfNoteFlag  = false;

        });
    }
    //#endregion

  onExportPdf() {
    const doc = new jsPDF();

    const pageHeight = doc.internal.pageSize.height;
    const pageWidth = doc.internal.pageSize.width;

    doc.setFontSize(10);

    doc.setFontSize(14);
    doc.text(
      'COORDINATION OF HUMANITARIAN ASSISTANCE',
      StaticUtilities.pdfTextCenter(
        doc,
        'COORDINATION OF HUMANITARIAN ASSISTANCE',
        14
      ),
      19
    );

    const text2 =
      'INCOME/EXPENDITURE REPORT IN (' +
      this.currencyCodePipe.transform(
        this.selectedCurrency,
        this.currencyList
      ) +
      ') AS ' +
      this.asOfDate +
      ' TO ' +
      this.upToDate;
    doc.text(text2, StaticUtilities.pdfTextCenter(doc, text2, 14), 26);

    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, 35, 200, 35);

    doc.setFontSize(12);
    doc.text('ASSETS PROPERTY & CAPITAL', 10, 45);
    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, 47, 200, 47);

    doc.setFontSize(10);
    let currentFontHeightLocation = 47;

    // income list
    for (const item of this.incomeList) {
      currentFontHeightLocation += 5;
      if (currentFontHeightLocation >= pageHeight) {
        doc.addPage();
        currentFontHeightLocation = 10;
      }
      doc.text(item.NoteName, 10, currentFontHeightLocation);

      const txtWidthNoteBalance =
        (doc.getStringUnitWidth(
          item.NoteBalance.toLocaleString(undefined, {
            minimumFractionDigits: 2
          })
        ) *
          10) /
        doc.internal.scaleFactor;

      doc.text(
        item.NoteBalance.toLocaleString(undefined, {
          minimumFractionDigits: 2
        }),
        pageWidth - txtWidthNoteBalance - 10,
        currentFontHeightLocation
      );
    }

    // Total Income
    currentFontHeightLocation += 10;
    doc.setLineWidth(0.1); // horizontal line
    doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
    currentFontHeightLocation = currentFontHeightLocation + 5;
    doc.text('Total Assets Property & Capital', 10, currentFontHeightLocation);

    const txtWidthTotalIncome =
      (doc.getStringUnitWidth(
        this.totalIncome.toLocaleString(undefined, {
          minimumFractionDigits: 2
        })
      ) *
        10) /
      doc.internal.scaleFactor;

    doc.text(
      this.totalIncome.toLocaleString(undefined, {
        minimumFractionDigits: 2
      }),
      pageWidth - txtWidthTotalIncome - 10,
      currentFontHeightLocation
    );

    doc.setFontSize(12);
    currentFontHeightLocation += 20;
    doc.text('EXPENSES', 10, currentFontHeightLocation);
    currentFontHeightLocation += 2;
    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);

    doc.setFontSize(10);
    // expense list
    for (const item of this.expenseList) {
      currentFontHeightLocation += 5;
      if (currentFontHeightLocation >= pageHeight) {
        doc.addPage();
        currentFontHeightLocation = 10;
      }
      doc.text(item.NoteName, 10, currentFontHeightLocation);

      const txtWidth =
        (doc.getStringUnitWidth(
          item.NoteBalance.toLocaleString(undefined, {
            minimumFractionDigits: 2
          })
        ) *
          10) /
        doc.internal.scaleFactor;

      doc.text(
        item.NoteBalance.toLocaleString(undefined, {
          minimumFractionDigits: 2
        }),
        pageWidth - txtWidth - 10,
        currentFontHeightLocation
      );
    }

    if (currentFontHeightLocation >= pageHeight) {
      doc.addPage();
      currentFontHeightLocation = 10;
    }

    // Total Expenditure
    currentFontHeightLocation += 10;
    doc.setLineWidth(0.1); // horizontal line
    doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
    currentFontHeightLocation += 5;
    doc.text('Total Expenditure', 10, currentFontHeightLocation);

    const pageWidthTotalExpenses =
      (doc.getStringUnitWidth(
        this.totalExpenses.toLocaleString(undefined, {
          minimumFractionDigits: 2
        })
      ) *
        10) /
      doc.internal.scaleFactor;

    doc.text(
      this.totalExpenses.toLocaleString(undefined, {
        minimumFractionDigits: 2
      }),
      pageWidth - pageWidthTotalExpenses - 10,
      currentFontHeightLocation
    );

    // Page Break
    if (currentFontHeightLocation >= pageHeight - 10) {
      doc.addPage();
      currentFontHeightLocation = 10;
    }
    // Excess of Expenditure
    currentFontHeightLocation += 20;
    doc.setLineWidth(0.1); // horizontal line
    doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);
    currentFontHeightLocation += 10;
    doc.text(
      'Excess of Expenditure (Carried Forward to CHA Own Source Funds)',
      10,
      currentFontHeightLocation
    );

    const pageWidthExpenditure =
      (doc.getStringUnitWidth(
        this.totalNett.toLocaleString(undefined, {
          minimumFractionDigits: 2
        })
      ) *
        10) /
      doc.internal.scaleFactor;

    doc.text(
      this.totalNett.toLocaleString(undefined, {
        minimumFractionDigits: 2
      }),
      pageWidth - pageWidthExpenditure - 10,
      currentFontHeightLocation
    );

    doc.save('income-expense-report.pdf');
  }

  onDetailOfNotesPdf() {
    const doc = new jsPDF();

    const pageHeight = doc.internal.pageSize.height;
    const paddingLeft = 14;

    doc.setFontSize(10);

    doc.setFontSize(14);
    doc.text(
      'COORDINATION OF HUMANITARIAN ASSISTANCE',
      StaticUtilities.pdfTextCenter(
        doc,
        'COORDINATION OF HUMANITARIAN ASSISTANCE',
        14
      ),
      19
    );

    const text2 =
      'NOTES OF THE ACCOUNTS IN (' +
      this.currencyCodePipe.transform(
        this.selectedCurrency,
        this.currencyList
      ) +
      ') FOR THE YEAR ' +
      this.upToDate;
    doc.text(text2, StaticUtilities.pdfTextCenter(doc, text2, 14), 26);

    doc.setLineWidth(0.1); // horizontal line
    doc.line(10, 35, 200, 35);


    // doc.autoTable({
    //   head: [['Account Code', 'Account Name', 'Debit', 'Credit']],
    //   theme: 'plain', // 'striped'|'grid'|'plain'
    //   margin: { top: 36 }
    // });

    doc.setFontSize(10);
    let currentFontHeightLocation = 42;

    doc.text('Account Code', paddingLeft, currentFontHeightLocation);
    doc.text('Account Name', paddingLeft + 30, currentFontHeightLocation);
    doc.text('Debit', paddingLeft + 100, currentFontHeightLocation);
    doc.text('Credit', paddingLeft + 150, currentFontHeightLocation);

    currentFontHeightLocation += 3;

    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, 45, 200, 45);

    doc.setFontSize(10);

    // income list
    // for (let i = 1; i < 1000 ; i++) {
    for (const item of this.detailsOfNotesFinalList) {
      if (currentFontHeightLocation >= pageHeight) {
        doc.addPage();
        currentFontHeightLocation = 10;
      }

      doc.setFontStyle('bold');
      doc.text(item.NoteName, paddingLeft, currentFontHeightLocation += 5);

      currentFontHeightLocation += 3;

      doc.setLineWidth(0.2); // horizontal line
      doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);

      for (const itemSummary of item.AccountSummary) {
        if (currentFontHeightLocation >= pageHeight) {
          doc.addPage();
          currentFontHeightLocation = 10;
        }

        doc.setFontStyle('normal');
        doc.setFontSize(10);
        currentFontHeightLocation += 5;

        doc.text(itemSummary.AccountCode, paddingLeft, currentFontHeightLocation);

        const splitTitle: string[] = doc.splitTextToSize(itemSummary.AccountName, 60);

        doc.text(splitTitle, paddingLeft + 30, currentFontHeightLocation);
        // doc.text(itemSummary.AccountName, paddingLeft + 30, currentFontHeightLocation);

        doc.text('' + itemSummary.Debit, paddingLeft + 100, currentFontHeightLocation);
        doc.text('' + itemSummary.Credit, paddingLeft + 150, currentFontHeightLocation);
        // doc.text('' + itemSummary.Credit, StaticUtilities.pdfTextRight(doc, '' + itemSummary.Credit, 14 ), currentFontHeightLocation);
        if (splitTitle.length > 1) {
          currentFontHeightLocation += 5;
        }
      }
      currentFontHeightLocation += 5;

      doc.setLineWidth(0.2); // horizontal line
      doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);

      doc.setFontStyle('bold');
      doc.text('Balance : ' + item.Balance, 14, currentFontHeightLocation += 5);
      doc.text('' + item.TotalDebits, paddingLeft + 100, currentFontHeightLocation);
      // doc.text('' + item.TotalCredits, StaticUtilities.pdfTextRight(doc, '' + item.TotalCredits, 14 ), currentFontHeightLocation);
      doc.text('' + item.TotalCredits, paddingLeft + 150, currentFontHeightLocation);

      currentFontHeightLocation += 5;

      //   doc.autoTable({
      //     styles: {fillColor: [255, 0, 0]},
      //     columnStyles: {0: {halign: 'center', fillColor: [0, 255, 0]}}, // Cells in first column centered and green
      //     margin: {top: 10},
      //     body: [['Sweden', 'Japan', 'Canada'], ['Norway', 'China', 'USA'], ['Denmark', 'China', 'Mexico']]
      // })



      // doc.autoTable({
      //   head: [[item.NoteName]],
      //   theme: 'plain', // 'striped'|'grid'|'plain'
      //   pageBreak: 'auto', // 'auto'|'avoid'|'always',
      //   showHead:  'firstPage', // 'everyPage'|'firstPage'|'never'
      //   columnStyles: {0: {halign: 'center', fillColor: [0, 255, 0]}}, // Cells in first column centered and green

      //   startY: doc.previousAutoTable.finalY + 5
      // });

      // doc.autoTable({
      //   fillColor: [255, 0, 0],
      //   columnStyles: {0: {halign: 'center', fillColor: [0, 255, 0]}}, // Cells in first column centered and green
      //   theme: 'striped', // 'striped'|'grid'|'plain'
      //   pageBreak: 'auto', // 'auto'|'avoid'|'always',
      //   showHead:  'firstPage', // 'everyPage'|'firstPage'|'never'
      //   startY: doc.previousAutoTable.finalY + 2,
      //   body: item.AccountSummary
      // });

      // currentFontHeightLocation += 2;
      // if (currentFontHeightLocation >= pageHeight) {
      //   doc.addPage();
      //   currentFontHeightLocation = 10;
      // }
      // doc.autoTable({
      //   head: [['Balance : ' + item.Balance, '', item.TotalDebits, item.TotalCredits]],
      //   theme: 'plain', // 'striped'|'grid'|'plain'
      //   // margin: { top: currentFontHeightLocation }
      //   startY: doc.previousAutoTable.finalY + 2
      // });

      // currentFontHeightLocation = doc.previousAutoTable.finalY + 5;


    // doc.text('Balance : ' + item.Balance, 14, currentFontHeightLocation);
    // doc.text('' + item.TotalDebits, StaticUtilities.pdfTextRight(doc, '' + item.TotalCredits, 14 ) - debitX, currentFontHeightLocation);
    // doc.text('' + item.TotalCredits, StaticUtilities.pdfTextRight(doc, '' + item.TotalCredits, 14 ), currentFontHeightLocation);




    }
    doc.save('detail-of-notes.pdf');
  }
}
