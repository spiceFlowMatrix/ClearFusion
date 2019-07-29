import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { ProjectActivitiesService } from '../project-list/project-activities/service/project-activities.service';
import { IResponseData } from '../../accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';
import * as jsPDF from 'jspdf';
import { SignalRService } from 'src/app/shared/services/signal-r.service';
import { FileSourceEntityTypes } from 'src/app/shared/enum';
import { GLOBAL } from 'src/app/shared/global';
import { NotifySignalRService } from 'src/app/shared/services/notify-signalr.service';

@Component({
  selector: 'app-file-upload-demo',
  templateUrl: './file-upload-demo.component.html',
  styleUrls: ['./file-upload-demo.component.scss']
})
export class FileUploadDemoComponent implements OnInit, OnDestroy {
  uploadActivitySubscribe: Subscription;

  Messages: IChatModel[];
  sampleMessage: string;
  ChatModel: IChatModel;

  constructor(
    private commonLoader: CommonLoaderService,
    private activitiesService: ProjectActivitiesService,
    private toastr: ToastrService,
    private notifySignalRService: NotifySignalRService
  ) {
  }

  ngOnInit() {
    this.Messages = [];

    this.notifySignalRService.DemoMessage$.subscribe(data => {
      this.Messages.push(data);
      console.log(this.Messages);
    });
  }

  public BroadcastMessageOn() {

  }

  //#region "uploadDocument"
  uploadDocument(data: any) {
    this.commonLoader.showLoader();
    this.uploadActivitySubscribe = this.activitiesService
      .UploadDocument(data)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data != null) {
            // this.documentListRefresh.emit(response.data);

            // this.projectActivityListRefresh();
            this.toastr.success('Document uploaded successfully');
          } else if (response.statusCode === 400) {
            this.toastr.error(response.message);
          }
          this.commonLoader.hideLoader();

        },
        error => {
          this.toastr.error('Someting went wrong');
          this.commonLoader.hideLoader();

        }
      );
  }
  //#endregion

  //#region "uploadFileEmit"
  uploadFileEmit(event: any) {
    this.uploadDocument(event);
  }
  //#endregion

  //#region "ngOnDestroy"
  ngOnDestroy() {
    if (this.uploadActivitySubscribe && !this.uploadActivitySubscribe.closed) {
      this.uploadActivitySubscribe.unsubscribe();
    }
  }
  //#endregion

  downloadPdf() {

    // SAMPLE ->
    // var doc = new jsPDF();
    // doc.text(20, 20, 'Hello world!');
    // doc.text(20, 30, 'This is client-side Javascript, pumping out a PDF.');
    // doc.addPage('a6','l');
    // doc.text(20, 20, 'Do you like that?');

    // FEATURES ->
    // doc.setFont("arial", "bold");
    // doc.setFontSize(22);
    // doc.setFontStyle("bolditalic");
    // doc.setTextColor(0, 255, 0);
    // doc.setTextColor(150);
    // doc.setFillColor(100, 100, 240);
    // doc.setDrawColor(100, 100, 0);
    // doc.setLineWidth(1);
    // doc.circle(150, 50, 5, 'FD');
    // Empty square
    // doc.rect(20, 20, 10, 10);

    // Filled square with red borders
    // doc.setDrawColor(255,0,0);
    // doc.rect(80, 20, 10, 10, 'FD');

    // Line
    // doc.line(50, 250, 100, 250);

    // doc.addPage(width, height);
    // doc.setPage(pageNumber);
    // doc.internal.getNumberOfPages();

    // doc.setProperties({
    //   title: 'Title',
    //   subject: 'This is the subject',
    //   author: 'Author Name',
    //   keywords: 'generated, javascript, web 2.0, ajax',
    //   creator: 'Creator Name'
    //  });


    // IMAGE
    // var img = new Image();
    // img.addEventListener('load', function() {
    //     var doc = new jsPDF();
    //     doc.addImage(img, 'png', 10, 50);
    // });
    // img.src = 'image_path/image_name.png';

    const doc = new jsPDF();

    doc.setFontSize(10);
    // doc.addImage(imagepath, 'JPEG', 150 , 10);

    // IMAGE
    const img = new Image();
    img.addEventListener('load', function() {
        // var doc = new jsPDF();
        // doc.addImage(img, 'png', 10, 50);
        doc.addImage(img, 'png', 150 , 10);

    doc.setFontSize(14);
    // doc.setFontStyle('bold');
    doc.text('NAWA RADIO', 85 , 10);
    doc.text('Marketing Department', 75 , 15);
    doc.text('Broadcasting Agreement Paper', 65 , 20);

    // Line
    // doc.line(50, 250, 100, 250);
    doc.setFontSize(10);
    doc.text('Add: Khushhal Khan Meena in front of Dawat University', 50, 40);
    doc.text('Kabul, Afghanistan', 80 , 45);
    doc.text('Email: Marketing @sabacent.org', 70 , 50);
    doc.text('Phone # 0703141414', 78 , 55);

    doc.text('Contractor:', 10 , 65);
    doc.text('Subject of Contract: Broadcasting of Spots', 10 , 70);
    doc.text('This contract is between NAWA RADIO 103.1FM as vender and ( raman ) as a client.', 10 , 75);
    doc.text('The contract is based on: 2/18/2019 up to 3/29/2019', 10 , 80);

    doc.setFontSize(14);
    doc.text('Both parties\' responsibilities are as follow:', 10 , 90);
    doc.setFontSize(10);
    doc.text('1. Broadcasting of(Spots) in NAWA Radio.', 15 , 100);
    doc.text('2. Radio airtimes should be in Flat time.', 15 , 105);
    doc.text('3. Programs status : ( Active )', 15, 110);
    doc.text('4. The broadcasting will be provided by', 15, 115);
    doc.text('The broadcasting cost of one month will be ( 123 )', 15, 120);

    doc.setFontSize(14);
    doc.text('Both parties\' responsibilities are as follow:', 10 , 130);
    doc.setFontSize(10);
    doc.text('1 - Customer', 15 , 140);
    doc.text('­ Payment of amount. before the starting of broadcasting.', 15 , 150);
    doc.text('- If client once approve the program format it will be his/her responsibility even if any mistakes were there.', 15 , 155);
    doc.text('- Provision of the schedule.', 15 , 160);
    doc.text('- The programs should not be against National benefits and Radio Nawa policies.', 15 , 165);
    // doc.setFontSize(14);
    doc.text('2 - NAWA RADIO', 15 , 175);
    // doc.setFontSize(10);
    doc.text('- Broadcasting of (Spots) Audio programs in Flat times as per the approved schedule.', 15 , 185);
    doc.text('Note: NAWA RADIO has no legal responsibility for the subjects and contents of the programs and advertisements.', 15 , 195);

    doc.text('Both parties are agreed to terms and conditions in the contract stated above.', 10 , 215);
    // doc.setFontSize(14);
    doc.text('NAWA RADIO', 10 , 225);

    doc.text('Representative Name: ', 10 , 235);
    doc.text('Signature: ', 10 , 240);
    doc.text('Date: 3/29/2019', 10 , 245);

    doc.text('Customer\'s Name: raman', 100 , 235);
    doc.text('Signature:', 100 , 240);

    doc.save('AgreementDoc.pdf');
  });
  img.src = '../../../../../assets/img/agreement-logo.png';



    // let printContents, popupWin;
    // printContents = document.getElementById('resultTable').innerHTML;
    // popupWin = window.open('', '_blank', '');
    // popupWin.document.open();
    // popupWin.document.write(`
    //       <html>
    //         <head>
    //           <title></title>
    //           <style>
    //           //........Customized style.......
    //           </style>
    //         </head>
    //     <body onload="window.print();window.close()">${printContents}</body>
    //       </html>`);
    // popupWin.document.close();
    // popupWin.document.execCommand('SaveAs', true, 'fileName.pdf')

  }

  sendChat() {
    this.ChatModel = {
      EntityId: 1,
      Message: this.sampleMessage,
      SourceEntityTypeId: FileSourceEntityTypes.ProjectDetail,
      UserName: ''
    };
      this.activitiesService.AddMessage(this.ChatModel).subscribe(data => {
      });
  }
}

interface IChatModel {
SourceEntityTypeId: number;
EntityId: number;
Message: string;
UserName: string;
}
