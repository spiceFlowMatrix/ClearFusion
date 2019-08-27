import { Injectable } from '@angular/core';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { IResponseData } from '../vouchers/models/status-code.model';
import { map } from 'rxjs/internal/operators/map';

declare const require: any;
const jsPDF = require('jspdf');
require('jspdf-autotable');

@Injectable({
  providedIn: 'root'
})
export class ChartOfAccountsPdfService {
  margins = {
    top: 40,
    bottom: 40,
    left: 40,
    right: 40,
    width: 480
  };

  dataSource: any = [];

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  ExportChartOfAccountPdf() {
    return this.globalService
      .getList(
        this.appurl.getApiUrl() +
          GLOBAL.API_Pdf_GetAllChartOfAccountHierarchyPdf
      )
      .pipe(
        map(x => {
          const responseData: IResponseData = {
            data: x.ResponseData,
            statusCode: x.StatusCode,
            message: x.Message
          };
          return responseData;
        })
      );
  }

  public ExportToPdf(): void {

    const doc = new jsPDF('p', 'pt', 'a4');
    doc.setFontSize(50);

    const pageHeight = doc.internal.pageSize.height;
    const pageWidth = doc.internal.pageSize.width;

    doc.setFontSize(14);

    let linePadding = 20;
    const pageMiddle =
      this.margins.width - this.margins.width / 2 + this.margins.left;
    let splitTitle = '';
    let textCurrentLocationYAxis = this.margins.top;

    // main heading
    splitTitle = doc.splitTextToSize(
      'COORDINATION OF HUMANITARIAN ASSISTANCE',
      this.margins.width
    );
    for (let i = 0; i < splitTitle.length; i++) {
      doc.text(
        StaticUtilities.pdfTextCenter(doc, splitTitle[i], 14),
        textCurrentLocationYAxis,
        splitTitle[i]
      );
      textCurrentLocationYAxis += linePadding;
    }

    // sub heading
    splitTitle = doc.splitTextToSize('CHART OF ACCOUNTS', this.margins.width);
    for (let i = 0; i < splitTitle.length; i++) {
      doc.text(
        StaticUtilities.pdfTextCenter(doc, splitTitle[i], 14),
        textCurrentLocationYAxis,
        splitTitle[i]
      );
      textCurrentLocationYAxis += linePadding;
    }

    doc.line(
      this.margins.left,
      textCurrentLocationYAxis,
      pageWidth - this.margins.right,
      textCurrentLocationYAxis
    );

    doc.setFontSize(11);

    // col 1
    let inlineHeaderContentColSm6 = (textCurrentLocationYAxis += linePadding);
    splitTitle = doc.splitTextToSize(
      'A/C No.',
      this.margins.width - pageMiddle
    );
    for (let i = 0; i < splitTitle.length; i++) {
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
        doc.addPage();
      }
      doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
      textCurrentLocationYAxis += linePadding;
    }

    linePadding = 10;
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(
      'Account Description',
      this.margins.width - pageMiddle
    );
    for (let i = 0; i < splitTitle.length; i++) {
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
        doc.addPage();
      }
      doc.text(pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
      textCurrentLocationYAxis += linePadding;
    }

    doc.line(
      this.margins.left,
      textCurrentLocationYAxis,
      pageWidth - this.margins.right,
      textCurrentLocationYAxis
    );
    textCurrentLocationYAxis += linePadding;


    // main level
    for (const itemMain of this.dataSource) {
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
        doc.addPage();
      }

      // col 1
      inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
      doc.text(
        this.margins.left,
        textCurrentLocationYAxis,
        itemMain.ChartOfAccountCode
      );
      textCurrentLocationYAxis += linePadding;

      // col 2
      // use to start the 2nd column content from 1st column content y-axis
      textCurrentLocationYAxis = inlineHeaderContentColSm6;
      splitTitle = doc.splitTextToSize(
        itemMain.AccountName,
        this.margins.width - this.margins.right
      );
      for (let i = 0; i < splitTitle.length; i++) {
        doc.text(
          this.margins.left + 80,
          textCurrentLocationYAxis,
          splitTitle[i]
        );
        textCurrentLocationYAxis += linePadding;
        if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
          textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
        }
      }

      // control level
      for (const itemControl of itemMain.ChildAccounts) {
        // col 1
        inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
        doc.text(
          this.margins.left,
          textCurrentLocationYAxis,
          itemControl.ChartOfAccountCode
        );
        textCurrentLocationYAxis += linePadding;

        // col 2
        // use to start the 2nd column content from 1st column content y-axis
        textCurrentLocationYAxis = inlineHeaderContentColSm6;
        splitTitle = doc.splitTextToSize(
          itemControl.AccountName,
          this.margins.width - this.margins.right
        );
        for (let i = 0; i < splitTitle.length; i++) {
          doc.text(
            this.margins.left + 110,
            textCurrentLocationYAxis,
            splitTitle[i]
          );
          textCurrentLocationYAxis += linePadding;
          if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
            textCurrentLocationYAxis = this.margins.top;
            doc.addPage();
          }
        }

        // sub level
        for (const itemSub of itemControl.ChildAccounts) {
          // col 1
          inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
          doc.text(
            this.margins.left,
            textCurrentLocationYAxis,
            itemSub.ChartOfAccountCode
          );
          textCurrentLocationYAxis += linePadding;

          // col 2
          // use to start the 2nd column content from 1st column content y-axis
          textCurrentLocationYAxis = inlineHeaderContentColSm6;
          splitTitle = doc.splitTextToSize(
            itemSub.AccountName,
            this.margins.width - this.margins.right
          );
          for (let i = 0; i < splitTitle.length; i++) {
            doc.text(
              this.margins.left + 140,
              textCurrentLocationYAxis,
              splitTitle[i]
            );
            textCurrentLocationYAxis += linePadding;
            if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
              textCurrentLocationYAxis = this.margins.top;
              doc.addPage();
            }
          }

          // input level
          for (const itemInput of itemSub.ChildAccounts) {
            // col 1
            inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
            doc.text(
              this.margins.left,
              textCurrentLocationYAxis,
              itemInput.ChartOfAccountCode
            );
            textCurrentLocationYAxis += linePadding;

            // col 2
            // use to start the 2nd column content from 1st column content y-axis
            textCurrentLocationYAxis = inlineHeaderContentColSm6;
            splitTitle = doc.splitTextToSize(
              itemInput.AccountName,
              this.margins.width - this.margins.right
            );
            for (let i = 0; i < splitTitle.length; i++) {
              doc.text(
                this.margins.left + 170,
                textCurrentLocationYAxis,
                splitTitle[i]
              );
              textCurrentLocationYAxis += linePadding;
              if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
                textCurrentLocationYAxis = this.margins.top;
                doc.addPage();
              }
            }
          }
        }
      }
      textCurrentLocationYAxis += 20;
    }

    doc.save('chart-of-account.pdf');
  }
}
