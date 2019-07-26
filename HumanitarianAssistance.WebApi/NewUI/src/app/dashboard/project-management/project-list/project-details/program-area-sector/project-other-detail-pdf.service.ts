import { Injectable } from '@angular/core';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { IProjectOtherDetailPdf } from '../models/project-details.model';

declare const require: any;
const jsPDF = require('jspdf');
require('jspdf-autotable');

@Injectable({
  providedIn: 'root'
})
export class ProjectOtherDetailPdfService {
  
  margins = {
    top: 40,
    bottom: 40,
    left: 40,
    right: 40,
    width: 500
  };

  constructor() { }

  
  onExportPdf(projectOtherDetailPdf: IProjectOtherDetailPdf) {

    const doc = new jsPDF('p', 'pt', 'a4');

    const pageHeight = doc.internal.pageSize.height;
    const pageWidth = doc.internal.pageSize.width;
   
    doc.setFontSize(11);
    
    // doc.fromHTML(
    //   document.getElementById('projectOtherDetailHeaderPdfTemplate'),
    //   this.margins.left, // x coord
    //   this.margins.top,
    //   {
    //     // y coord
    //     width: this.margins.width
    //   },
    //   this.margins
    // );

    // // horizontal line
    // doc.setLineWidth(0.2);
    // doc.line(10, 30, 200, 30);

    const linePadding = 20;
    const pageMiddle = this.margins.width - 150;
    let splitTitle = "";
    let textCurrentLocationYAxis = this.margins.top;

    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.ProjectName, this.margins.width);
    for (let i = 0; i < splitTitle.length; i++) {                
        if (textCurrentLocationYAxis > this.margins.width) {
          textCurrentLocationYAxis = linePadding;
            doc.addPage();
        }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // Project Description
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Description, this.margins.width);
    for (let i = 0; i < splitTitle.length; i++) {                
        if (textCurrentLocationYAxis > this.margins.width) {
          textCurrentLocationYAxis = linePadding;
            doc.addPage();
        }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Opportunity Details");
    
    // OpportunityType
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Opportunity Type");
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.OpportunityType, this.margins.width);
    for (let i = 0; i < splitTitle.length; i++) {                
        if (textCurrentLocationYAxis > this.margins.width) {
          textCurrentLocationYAxis = linePadding;
            doc.addPage();
        }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }

    // Donor
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Donor");
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Donor, this.margins.width);
    for (let i = 0; i < splitTitle.length; i++) {                
        if (textCurrentLocationYAxis > this.margins.width) {
          textCurrentLocationYAxis = linePadding;
            doc.addPage();
        }
        doc.text(this.margins.left, textCurrentLocationYAxis += 20, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }

    // doc.text(projectOtherDetailPdf.ProjectName, 20, 20);
    // doc.text(projectOtherDetailPdf.Description, 20, 40);


    doc.save('abc.pdf');
  }


  // headerFooterFormatting(doc, totalPages) {
  //   for (let i = totalPages; i >= 1; i--) {
  //     console.log(totalPages);
  //     doc.setPage(i);
  //     // header
  //     this.header(doc);

  //     // this.footer(doc, i, totalPages);
  //     doc.page++;
  //   }
  // }

  // header(doc) {
  //   doc.setFontSize(30);
  //   doc.setTextColor(40);
  //   doc.setFontStyle('normal');

  //   // if (this.base64Img) {
  //   //   doc.addImage(this.base64Img, 'JPEG', this.margins.left, 10, 40, 40);
  //   // }

  //   doc.text('Report Header Template', this.margins.left + 50, 40);

  //   // doc.line(3, 70, this.margins.width + 43, 70); // horizontal line
  // }

  // footer(doc, pageNumber, totalPages) {
  //   const str = 'Page ' + pageNumber + ' of ' + totalPages;

  //   doc.setFontSize(10);
  //   doc.text(str, this.margins.left, doc.internal.pageSize.height - 20);
  // }

}
