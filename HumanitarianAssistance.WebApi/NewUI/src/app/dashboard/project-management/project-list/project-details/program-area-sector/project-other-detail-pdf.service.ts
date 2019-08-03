import { Injectable } from '@angular/core';
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
    const pageMiddle = (this.margins.width - (this.margins.width / 2)) + this.margins.left;
    let splitTitle = '';
    let textCurrentLocationYAxis = this.margins.top;

    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.ProjectName, this.margins.width);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // Project Description
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Description, this.margins.width);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Opportunity Details");
    
    // OpportunityType & Donor
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Opportunity Type");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "Donor");

    // col 1
    let inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.OpportunityType, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Donor, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    
    // OpportunityNo & Opportunity
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Opportunity No");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "Opportunity");

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.OpportunityNo, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Opportunity, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }


    // Opportunity Description
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Opportunity Description");
    textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.OpportunityDescription, this.margins.width);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }


    
    // OpportunityNo & Opportunity
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Country");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "Province");

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Country, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Province, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }

    
    // District & Office
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "District");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "Office");

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.District, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Office, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }


    // Sector & Program
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Sector");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "Program");

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Sector, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Sector, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }

    // Start Date & End Date
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Start Date");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "End Date");

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.StartDate, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.EndDate, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }

    doc.addPage();
    textCurrentLocationYAxis = linePadding

     // Project Objective & Goal
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Project Objective & Goal");
    
    // Project Goal & Project Objective
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Project Goal");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "Project Objective");

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.ProjectGoal, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.ProjectObjective, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    
    // Main Activity & REOI Receive Date
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Main Activity");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "REOI Receive Date");

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.MainActivities, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.REOIReceiveDate, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }

    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.SubmissionDate, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    

    // Beneficiary Details
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Beneficiary Details");
    
    // Direct Beneficiary Male & In-Direct Beneficiary Male
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Direct Beneficiary Male");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "In-Direct Beneficiary Male");

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.DirectbeneficiarMale, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.InDirectbeneficiarMale, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    

    // Direct Beneficiary Male & In-Direct Beneficiary Male
    doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Direct Beneficiary Female");
    doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "In-Direct Beneficiary Female");

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.DirectbeneficiarFemale, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize(projectOtherDetailPdf.InDirectbeneficiarFemale, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    

    // col 1
    inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
    splitTitle = doc.splitTextToSize('Total Direct Beneficaiary: ' + projectOtherDetailPdf.TotalDirectBeneficiary, this.margins.width - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }
    
    // col 2
    // use to start the 2nd column content from 1st column content y-axis
    textCurrentLocationYAxis = inlineHeaderContentColSm6;
    splitTitle = doc.splitTextToSize('Total InDirect Beneficaiary: ' + projectOtherDetailPdf.TotalInDirectBeneficiary, this.margins.width  - pageMiddle);
    for (let i = 0; i < splitTitle.length; i++) {                
      if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
        textCurrentLocationYAxis = this.margins.top;
          doc.addPage();
      }
        doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
        textCurrentLocationYAxis += linePadding;
    }


    
     // Gender Consideration
     doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Gender Consideration");
    
     // Strength Consideration & Gender Consideration
     doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Strength Consideration");
     doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "Gender Consideration");
 
     // col 1
     inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
     splitTitle = doc.splitTextToSize(projectOtherDetailPdf.StrengthConsideration, this.margins.width - pageMiddle);
     for (let i = 0; i < splitTitle.length; i++) {                
       if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
         textCurrentLocationYAxis = this.margins.top;
           doc.addPage();
       }
         doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
         textCurrentLocationYAxis += linePadding;
     }
     
     // col 2
     // use to start the 2nd column content from 1st column content y-axis
     textCurrentLocationYAxis = inlineHeaderContentColSm6;
     splitTitle = doc.splitTextToSize(projectOtherDetailPdf.GenderConsideration, this.margins.width  - pageMiddle);
     for (let i = 0; i < splitTitle.length; i++) {                
       if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
         textCurrentLocationYAxis = this.margins.top;
           doc.addPage();
       }
         doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
         textCurrentLocationYAxis += linePadding;
     }

     doc.text(this.margins.left, textCurrentLocationYAxis, "Gender Remark");
     textCurrentLocationYAxis += linePadding,
     splitTitle = doc.splitTextToSize(projectOtherDetailPdf.GenderConsideration, this.margins.width);
     for (let i = 0; i < splitTitle.length; i++) {                
       if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
         textCurrentLocationYAxis = this.margins.top;
           doc.addPage();
       }
         doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
         textCurrentLocationYAxis += linePadding;
     }
     
    
     

     // Security Consideration
     doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Security Consideration");
    
     // Security & Security Consideration
     doc.text(this.margins.left, textCurrentLocationYAxis += linePadding, "Security");
     doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, "Security Consideration");
 
     // col 1
     inlineHeaderContentColSm6 = textCurrentLocationYAxis += linePadding;
     splitTitle = doc.splitTextToSize(projectOtherDetailPdf.Security, this.margins.width - pageMiddle);
     for (let i = 0; i < splitTitle.length; i++) {                
       if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
         textCurrentLocationYAxis = this.margins.top;
           doc.addPage();
       }
         doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
         textCurrentLocationYAxis += linePadding;
     }
     
     // col 2
     // use to start the 2nd column content from 1st column content y-axis
     textCurrentLocationYAxis = inlineHeaderContentColSm6;
     splitTitle = doc.splitTextToSize(projectOtherDetailPdf.SecurityConsideration, this.margins.width  - pageMiddle);
     for (let i = 0; i < splitTitle.length; i++) {                
       if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
         textCurrentLocationYAxis = this.margins.top;
           doc.addPage();
       }
         doc.text(this.margins.left + pageMiddle, textCurrentLocationYAxis, splitTitle[i]);
         textCurrentLocationYAxis += linePadding;
     }

     doc.text(this.margins.left, textCurrentLocationYAxis, "Security Remark");
     textCurrentLocationYAxis += linePadding,
     splitTitle = doc.splitTextToSize(projectOtherDetailPdf.SecurityRemarks, this.margins.width);
     for (let i = 0; i < splitTitle.length; i++) {                
       if (textCurrentLocationYAxis > pageHeight - this.margins.bottom) {
         textCurrentLocationYAxis = this.margins.top;
           doc.addPage();
       }
         doc.text(this.margins.left, textCurrentLocationYAxis, splitTitle[i]);
         textCurrentLocationYAxis += linePadding;
     }
     
    


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
