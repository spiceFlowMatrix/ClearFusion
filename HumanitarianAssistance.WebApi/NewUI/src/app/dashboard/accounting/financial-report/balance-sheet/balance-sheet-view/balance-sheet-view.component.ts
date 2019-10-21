import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ICurrencyList } from '../../../gain-loss-report/gain-loss-report.model';
import { IResponseData } from '../../../vouchers/models/status-code.model';
import { BalanceSheetService } from '../balance-sheet.service';
import { AccountHeadTypes_Enum } from 'src/app/shared/enum';
import { NoteAccountBalanceModel } from '../../models/note-account-balance-model';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import * as jsPDF from 'jspdf';
import { CurrencyCodePipe } from 'src/app/shared/pipes/currency-code.pipe';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-balance-sheet-view',
  templateUrl: './balance-sheet-view.component.html',
  styleUrls: ['./balance-sheet-view.component.scss']
})
export class BalanceSheetViewComponent implements OnInit {
  //#region "variables"

  // CONST
  ASSETS_ID: number = AccountHeadTypes_Enum.Assets;
  LIABILITY_ID: number = AccountHeadTypes_Enum.Liabilities;

  // DATASOURCE
  assetsList: NoteAccountBalanceModel[] = [];
  liabilitiesList: NoteAccountBalanceModel[] = [];
  currencyList: ICurrencyList[] = [];

  totalAssets = 0;
  totalLiabities = 0;
  totalEquity = 0;
  totalNett = 0;

  // FLAG
  inputFieldAssetsFlag = false;
  inputFieldLiabilitiesFlag = false;
  inputFieldDonorsEquityFlag = false;

  showa = [];
  showb = [];
  asOfDate: string;
  currency: number;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  //#endregion

  constructor(
    private globalService: GlobalService,
    private balanceSheetService: BalanceSheetService,
    private currencyCodePipe: CurrencyCodePipe,
    private appUrl: AppUrlService,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.asOfDate = params.asOfDate;
      this.currency = +params.currency;
      this.getAllCurrency();
      this.getBalanceSheetAssetAccountBalances();
      this.getBalanceSheetLiabilityAccountBalances();
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

  numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
  }

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
    this.balanceSheetService.GetCurrencyList().subscribe(
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

  //#region "getBalanceSheetAsset"
  getBalanceSheetAssetAccountBalances() {
    const dataModel = {
      id: AccountHeadTypes_Enum.Assets,
      asOfDate: new Date(
        new Date(this.asOfDate).getFullYear(),
        new Date(this.asOfDate).getMonth(),
        new Date(this.asOfDate).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds(),
        new Date().getMilliseconds()
      ),
      currency: this.currency
    };

    this.globalService
      .getListByIdAndDate(
        this.appUrl.getApiUrl() +
          GLOBAL.API_FinancialReport_GetAllAccountBalancesByCategory,
        dataModel
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          if (data.data.NoteAccountBalances != null) {
            if (data.data.NoteAccountBalances.length > 0) {
              const assetsList: NoteAccountBalanceModel[] = [];

              data.data.NoteAccountBalances.forEach(element => {
                assetsList.push({
                  NoteId: element.NoteId,
                  NoteName: element.NoteName,
                  NoteHeadId: element.NoteHeadId,
                  AccountBalances: element.AccountBalances,
                  NoteBalance: this.getSum(element.AccountBalances, 'Balance')
                });
              });

              this.assetsList = assetsList.filter(
                x => x.NoteHeadId === this.ASSETS_ID
              );
            }
          }
          this.totalAssets = this.getSum(this.assetsList, 'NoteBalance');
          this.totalNett =
            this.totalAssets - this.totalLiabities - this.totalEquity;
        } else if (data.StatusCode === 400) {
          this.toastr.error(data.Message);
        }
      });
  }
  //#endregion

  //#region "getBalanceSheetLiability"
  getBalanceSheetLiabilityAccountBalances() {
    const dataModel = {
      id: AccountHeadTypes_Enum.Liabilities,
      asOfDate: this.asOfDate,
      currency: this.currency
    };

    this.globalService
      .getListByIdAndDate(
        this.appUrl.getApiUrl() +
          GLOBAL.API_FinancialReport_GetAllAccountBalancesByCategory,
        dataModel
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          if (data.data.NoteAccountBalances != null) {
            if (data.data.NoteAccountBalances.length > 0) {
              const liabilitiesList: NoteAccountBalanceModel[] = [];

              data.data.NoteAccountBalances.forEach(element => {
                liabilitiesList.push({
                  NoteId: element.NoteId,
                  NoteName: element.NoteName,
                  NoteHeadId: element.NoteHeadId,
                  AccountBalances: element.AccountBalances,
                  NoteBalance: this.getSum(element.AccountBalances, 'Balance')
                });
              });

              this.liabilitiesList = liabilitiesList.filter(
                x => x.NoteHeadId === this.LIABILITY_ID
              );
            }
          }
          this.totalLiabities = this.getSum(
            this.liabilitiesList,
            'NoteBalance'
          );
          this.totalNett = this.totalAssets - this.totalLiabities;
        } else if (data.StatusCode === 400) {
          this.toastr.error(data.Message);
        }
      });
  }
  //#endregion

  //#region "onExportPdf"
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
      ' BALANCE SHEET IN (' +
      this.currencyCodePipe.transform(this.currency, this.currencyList) +
      ') AS ' +
      this.asOfDate;
    doc.text(text2, StaticUtilities.pdfTextCenter(doc, text2, 14), 26);

    // const text2 =
    //   'NOTES TO THE ACCOUNTS IN (' +
    //   this.currencyCodePipe.transform(this.currency, this.currencyList) +
    //   ') FOR THE YEAR ' +
    //   this.asOfDate;
    // doc.text(text2, StaticUtilities.pdfTextCenter(doc, text2, 14), 26);

    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, 35, 200, 35);

    doc.setFontSize(12);
    doc.text('ASSETS PROPERTY & CAPITAL', 10, 45);
    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, 47, 200, 47);

    let currentFontHeightLocation = 47;

    // ASSETS PROPERTY & CAPITAL List
    doc.setFontSize(10);
    for (const item of this.assetsList) {
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

    currentFontHeightLocation += 5;
    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);


    currentFontHeightLocation += 5;

    doc.text(
      'Total Assets ', 10,
      currentFontHeightLocation
    );

    const ASSETSText = this.totalAssets.toLocaleString(undefined, { minimumFractionDigits: 2 });
    doc.text(
      ASSETSText,
      StaticUtilities.pdfTextRight(doc, ASSETSText, 10),
      currentFontHeightLocation
    );

    doc.setFontSize(12);
    currentFontHeightLocation += 10;
    doc.text('LIABILITIES', 10, currentFontHeightLocation);
    currentFontHeightLocation += 2;
    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);

    // LIABILITIES List
    doc.setFontSize(10);

    for (const item of this.liabilitiesList) {
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

    currentFontHeightLocation += 5;
    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);


    currentFontHeightLocation += 5;

    doc.text(
      'Total Liabilities ', 10,
      currentFontHeightLocation
    );

    const LiabilitiesText = this.totalLiabities.toLocaleString(undefined, { minimumFractionDigits: 2 });
    doc.text(
      LiabilitiesText,
      StaticUtilities.pdfTextRight(doc, LiabilitiesText, 10),
      currentFontHeightLocation
    );


    currentFontHeightLocation += 10;

    doc.text(
      'DIFFERENCE (CARRIED FORWARD TO CHA OWN SOURCE FUND)', 10,
      currentFontHeightLocation
    );

    const NETTText = this.totalNett.toLocaleString(undefined, { minimumFractionDigits: 2 });
    doc.text(
      NETTText,
      StaticUtilities.pdfTextRight(doc, NETTText, 10),
      currentFontHeightLocation
    );

    currentFontHeightLocation += 10;

    // doc.text(
    //   'LIABILITIES: ' +
    //     this.totalLiabities.toLocaleString(undefined, {
    //       minimumFractionDigits: 2
    //     }),
    //   10,
    //   currentFontHeightLocation
    // );

    // const NETTText =
    //   'NETT: ' +
    //   this.totalNett.toLocaleString(undefined, { minimumFractionDigits: 2 });
    // doc.text(
    //   NETTText,
    //   StaticUtilities.pdfTextCenter(doc, NETTText, 10),
    //   currentFontHeightLocation
    // );

    // const ASSETSText =
    //   'ASSETS: ' +
    //   this.totalNett.toLocaleString(undefined, { minimumFractionDigits: 2 });

    // doc.text(
    //   ASSETSText,
    //   StaticUtilities.pdfTextRight(doc, ASSETSText, 10),
    //   currentFontHeightLocation
    // );

    doc.save('Balance-Sheet-Report.pdf');
  }
  //#endregion

  //#region "onNoteExportPdf"
  onNoteExportPdf() {
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
      'NOTES TO THE ACCOUNTS IN (' +
      this.currencyCodePipe.transform(this.currency, this.currencyList) +
      ') FOR THE YEAR ' +
      this.asOfDate;
    doc.text(text2, StaticUtilities.pdfTextCenter(doc, text2, 14), 26);

    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, 35, 200, 35);

    doc.setFontSize(12);
    doc.text('ASSETS PROPERTY & CAPITAL', 10, 45);
    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, 47, 200, 47);

    let currentFontHeightLocation = 47;

    // ASSETS PROPERTY & CAPITAL List
    doc.setFontSize(10);
    for (const item of this.assetsList) {
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

    doc.setFontSize(12);
    currentFontHeightLocation += 10;
    doc.text('LIABILITIES', 10, currentFontHeightLocation);
    currentFontHeightLocation += 2;
    doc.setLineWidth(0.2); // horizontal line
    doc.line(10, currentFontHeightLocation, 200, currentFontHeightLocation);

    // LIABILITIES List
    doc.setFontSize(10);

    for (const item of this.liabilitiesList) {
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


    doc.save('Note-Report.pdf');
  }
  //#endregion
}
