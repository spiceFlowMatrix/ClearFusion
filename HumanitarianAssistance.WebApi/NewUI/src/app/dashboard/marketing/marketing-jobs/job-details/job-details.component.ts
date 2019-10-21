import { Component, OnInit, HostListener, OnChanges } from '@angular/core';
import {
  MarketingJobDetailModel,
  PhaseDetailsModel,
  ContractsListByClient,
  InvoiceModel
} from '../model/marketing-jobs.model';
import { MarketingJobsService } from '../service/marketing-jobs.service';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { DatePipe } from '@angular/common';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import * as jsPDF from 'jspdf';

@Component({
  selector: 'app-job-details',
  templateUrl: './job-details.component.html',
  styleUrls: ['./job-details.component.scss']
})
export class JobDetailsComponent implements OnInit, OnChanges {
  //#region "variable"
  contractsList: any[];
  contractsListByClient: ContractsListByClient[];
  contractsListByClient1: ContractsListByClient[];
  jobDetails: MarketingJobDetailModel = {};
  id: number;
  jobDetailsForm: FormGroup;
  unitRate?: number;
  finalPrice?: number;
  finalRate?: number;
  discount?: number;
  discountPercent?: number;
  validateForm = false;
  archiveButton = false;
  selectedJobId?: number;
  displayModal?: string;
  type?: string;
  approved = false;
  declined = false;
  approve = false;
  decline = false;
  approveButton = false;
  approvedJobFlag = false;
  checkApprove = false;

  downloadAgreement = true;
  whatTime: any;
  currencyCode = '';
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  showPdfDiv = false;
  jobDetailsLoaderFlag = false;
  jobDetails1LoaderFlag = false;
  downloadAgreementLoader = false;
  approveJobLoader = false;
  isApproveRejectAllowed = false;
  isAgreeDisagreeAllowed = false;
  disableInvoiceBtn = false;
  exportPdfBtn = false;
  printInvoiceFlag = false;
  disableInvoiceGenerationBtn = false;
  pageId = ApplicationPages.Assets;
  invoiceDetails: InvoiceModel = {};
  genrateInvoiceSpinnerFlag = false;
  approveInvoiceSpinnerFlag = false;
  rejectInvoiceSpinnerFlag = false;

  formErrors = {
    jobname: '',
    endDate: '',
    contractname: '',
    minutes: '',
    rate: '',
    total: ''
  };
  validationMessages = {
    jobname: {
      required: 'Job Name is required.'
    },
    endDate: {
      required: 'End Date is required.'
    },
    contractname: {
      required: 'Contract Name is required.'
    },
    minutes: {
      required: 'Minutes are required.'
    },
    rate: {
      required: 'Rate is required.'
    },
    total: {
      required: 'Total is required.'
    }
  };

  //#endregion

  constructor(
    private datePipe: DatePipe,
    public marketingJobService: MarketingJobsService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private localStorageService: LocalStorageService
  ) {
    this.id = +this.route.snapshot.paramMap.get('id');
    this.getScreenSize();
  }

  ngOnInit() {
    this.initForm();
    this.getContractsByClient();
    this.fetchInvoice();
    this.getContracts();
    if (this.id !== 0 && this.id !== undefined) {
      this.archiveButton = true;
      this.GetJobById(this.id);
    } else {
      this.archiveButton = false;
    }

    this.setPagePermission();
  }

  ngOnChanges(): void {
    this.initForm();
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;
    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 111 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  setPagePermission() {
    this.isApproveRejectAllowed = this.localStorageService.IsApproveRejectAllowed(
      this.pageId
    );
    this.isAgreeDisagreeAllowed = this.localStorageService.IsAgreeDisagreeAllowed(
      this.pageId
    );
  }

  initForm() {
    this.jobDetailsForm = new FormGroup({
      jobname: new FormControl('', [Validators.required]),
      endDate: new FormControl('', [Validators.required]),
      contractname: new FormControl('', [Validators.required]),
      minutes: new FormControl('', [Validators.required]),
      rate: new FormControl('', [Validators.required]),
      total: new FormControl('', [Validators.required]),
      clientname: new FormControl('', [Validators.required])
    });
  }

  ApproveJobs() {
    this.approveJobLoader = true;
    this.marketingJobService.ApproveJob(this.id).subscribe(
      (data: IResponseData) => {
        if (data.statusCode === 200) {
          this.toastr.success(data.message);
          this.approveJobLoader = false;
          this.approvedJobFlag = false;
        } else {
          this.approveJobLoader = false;
          this.approvedJobFlag = true;
          this.toastr.error('Some error occured. Please try again later.');
        }
      },
      error => {
        this.approveJobLoader = false;
        this.approvedJobFlag = true;
        this.toastr.error('Some error occured. Please try again later');
      }
    );
  }

  onSave() {
    this.jobDetails.Discount = this.discount;
    this.jobDetails.DiscountPercent = this.discountPercent;
    this.jobDetails.UnitRate = this.unitRate;
    this.jobDetails.TotalPrice = this.finalPrice;
    this.jobDetails.FinalRate = this.finalRate;
    this.jobDetails.JobName = this.jobDetailsForm.controls['jobname'].value;
    this.jobDetails.ContractId = this.jobDetailsForm.controls[
      'contractname'
    ].value;
    this.jobDetails.Minutes = this.jobDetailsForm.controls['minutes'].value;
    this.jobDetails.EndDate = this.jobDetailsForm.controls['endDate'].value;
    this.jobDetailsForm.disable();
    this.marketingJobService.AddJobDetail(this.jobDetails).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success(response.message);
        } else {
          this.toastr.error('Some error occured. Please try again later.');
        }
        this.jobDetailsForm.enable();
      },
      error => {
        this.jobDetailsForm.enable();
        this.toastr.error('Some error occured. Please try again later');
      }
    );
  }

  onChange(value) {
    this.marketingJobService
      .GetContractByClient(value)
      .subscribe((data: IResponseData) => {
        if (data.statusCode === 200) {
          this.contractsListByClient1 = data.data;
        }
      });
  }

  AgreementAccepted() {
    this.marketingJobService
      .AcceptAgreement(this.jobDetails.JobId)
      .subscribe((data: IResponseData) => {
        if (data.statusCode === 200) {
          this.toastr.success(data.message);
          if (data.data.IsAgreementApproved === true) {
            this.approveButton = true;
            this.downloadAgreement = false;
          }
        } else {
          this.toastr.error(data.message);
          this.approveButton = false;
          this.downloadAgreement = true;
        }
      });
  }

  generateAgreementDocPdf() {
    const job = this.jobDetails;
    const startDate = this.datePipe.transform(job.StartDate, 'yyyy-MM-dd');
    const endDate = this.datePipe.transform(job.EndDate, 'yyyy-MM-dd');
    const doc = new jsPDF();

    doc.setFontSize(10);
    const img = new Image();
    img.src = './assets/img/agreement-logo-small.png';
    img.addEventListener('load', function() {
      doc.addImage(img, 'png', 150, 5);

      doc.setFontSize(14);

      doc.text('NAWA RADIO', 85, 10);
      doc.text('Marketing Department', 75, 15);
      doc.text('Broadcasting Agreement Paper', 65, 20);

      // horizontal line
      doc.setLineWidth(0.2);
      doc.line(10, 30, 200, 30);
      doc.setLineWidth(0.2);
      doc.line(10, 60, 200, 60);

      // vertical line
      doc.setLineWidth(0.2);
      doc.line(10, 30, 10, 60);
      doc.setLineWidth(0.2);
      doc.line(200, 30, 200, 60);

      // horizontal line
      doc.setLineWidth(0.2);
      doc.line(10, 205, 200, 205);

      doc.setFontSize(10);
      doc.text('Add: Khushhal Khan Meena in front of Dawat University', 50, 40);
      doc.text('Kabul, Afghanistan', 80, 45);
      doc.text('Email: Marketing @sabacent.org', 70, 50);
      doc.text('Phone # 0703141414', 78, 55);

      doc.setFontType('bold');
      doc.text('Contractor:', 10, 65);
      doc.setFontType('bold');
      doc.text('Subject of Contract:', 10, 70);
      doc.setFontType('normal');
      doc.text('Broadcasting of Spots', 45, 70);
      doc.text(
        'This contract is between NAWA RADIO 103.1FM as vender and',
        10,
        75
      );
      doc.setFontType('bold');
      doc.text('( ' + job.ClientName + ' )', 113, 75);
      doc.setFontType('normal');
      doc.text('as a client.', 130, 75);
      doc.text('The contract is based on:', 10, 80);
      doc.setFontType('bold');
      doc.text('' + startDate + '', 52, 80);
      doc.setFontType('normal');
      doc.text('up to', 73, 80);
      doc.setFontType('bold');
      doc.text('' + endDate + '', 83, 80);

      doc.setFontSize(12);
      doc.text('Both parties\' responsibilities are as follows:', 10, 90);
      doc.setFontType('normal');
      doc.setFontSize(10);
      doc.text('1. Broadcasting of(Spots) in NAWA Radio.', 15, 100);
      doc.text('2. Radio airtimes should be in Flat time.', 15, 105);
      doc.text('3. Programs status : ( Active )', 15, 110);
      doc.text('4. The broadcasting will be provided by', 15, 115);
      doc.text('The broadcasting cost of one month will be', 15, 120);
      doc.setFontType('bold');
      doc.text('( ' + job.UnitRate + ' )', 85, 120);
      doc.setFontSize(12);
      doc.text('Both parties\' responsibilities are as follows:', 10, 130);
      doc.setFontSize(10);
      doc.text('1 - Customer:', 15, 140);
      doc.setFontType('normal');
      doc.text(
        '­ Payment of amount. before the starting of broadcasting.',
        15,
        150
      );
      doc.text(
        '- If client once approve the program format it will be his/her responsibility even if any mistakes were there.',
        15,
        155
      );
      doc.text('- Provision of the schedule.', 15, 160);
      doc.text(
        '- The programs should not be against National benefits and Radio Nawa policies.',
        15,
        165
      );
      doc.setFontType('bold');
      doc.text('2 - NAWA RADIO:', 15, 175);
      doc.setFontType('normal');
      doc.text(
        '- Broadcasting of (Spots) Audio programs in Flat times as per the approved schedule.',
        15,
        185
      );
      doc.setFontType('bold');
      doc.setFontSize(12);
      doc.text('Note:', 10, 195);
      doc.setFontType('normal');
      doc.setFontSize(10);
      doc.text(
        'NAWA RADIO has no legal responsibility for the subjects and contents of the programs and advertisements.',
        23,
        195
      );

      doc.text(
        'Both parties have agreed to terms and conditions in the contract stated above.',
        10,
        215
      );
      doc.setFontSize(12);
      doc.setFontType('bold');
      doc.text('NAWA RADIO', 10, 225);
      doc.text('Representative Name: ', 10, 235);
      doc.text('Signature: ', 10, 245);
      doc.text('Date:', 10, 255);
      doc.setFontType('normal');
      doc.text(' 3/29/2019', 22, 255);
      doc.setFontType('bold');
      doc.text('Customer\'s Name:', 100, 235);
      doc.setFontType('normal');
      doc.text('' + job.ClientName + '', 140, 235);
      doc.setFontType('bold');
      doc.text('Signature:', 100, 245);

      doc.save('AgreementDoc.pdf');
    });
  }

  GetJobById(id: number) {
    this.jobDetailsLoaderFlag = true;
    this.jobDetails1LoaderFlag = true;

    this.finalPrice = null;
    this.finalRate = null;
    this.discount = null;
    this.discountPercent = null;

    this.marketingJobService.GetJobById(id).subscribe(
      (data: IResponseData) => {
        if (data.statusCode === 200) {
          this.jobDetails = data.data;
          this.jobDetails.EndDate = StaticUtilities.setLocalDate(
            data.data.EndDate
          );

          if (this.jobDetails.IsAgreementApproved === true) {
            this.approveButton = true;
            this.downloadAgreement = false;
          } else {
            this.approveButton = false;
            this.downloadAgreement = true;
          }

          if (this.jobDetails.IsApproved === true) {
            this.approved = true;
            this.declined = false;
            this.approve = false;
            this.decline = false;
            this.approvedJobFlag = false;
          }
          if (this.jobDetails.IsApproved === false) {
            this.approved = false;
            this.declined = true;
            this.approve = false;
            this.decline = false;
            this.approvedJobFlag = true;
          }

          const currentDate = new Date(
            new Date().getFullYear(),
            new Date().getMonth(),
            new Date().getDate()
          );

          if (currentDate > this.jobDetails.EndDate) {
            this.disableInvoiceBtn = false;
            this.disableInvoiceGenerationBtn = false;
            this.exportPdfBtn = false;
          } else {
            this.disableInvoiceBtn = true;
            this.disableInvoiceGenerationBtn = true;
            this.exportPdfBtn = true;
          }
          this.selectedJobId = this.jobDetails.JobId;
          this.onChange(this.jobDetails.ClientId);
          this.jobDetailsForm.controls['jobname'].setValue(
            this.jobDetails.JobName
          );
          this.jobDetailsForm.controls['endDate'].setValue(
            this.jobDetails.EndDate
          );
          this.jobDetailsForm.controls['contractname'].setValue(
            this.jobDetails.ContractId
          );
          this.jobDetailsForm.controls['minutes'].setValue(
            this.jobDetails.Minutes
          );
          this.jobDetailsForm.controls['rate'].setValue(
            this.jobDetails.FinalRate
          );
          this.jobDetailsForm.controls['total'].setValue(
            this.jobDetails.TotalPrice
          );
          this.jobDetailsForm.controls['clientname'].setValue(
            this.jobDetails.ClientId
          );

          this.finalPrice = this.jobDetails.TotalPrice;
          this.finalRate = this.jobDetails.FinalRate;
          this.discount = this.jobDetails.Discount;
          this.discountPercent = this.jobDetails.DiscountPercent;
          this.currencyCode = this.jobDetails.CurrencyCode;
          this.unitRate = this.jobDetails.UnitRate;
        } else {
          // this.commonLoaderService.hideLoader();
          this.toastr.error('Some error occured. Please try again later.');
        }
        this.jobDetailsLoaderFlag = false;
        this.jobDetails1LoaderFlag = false;
      },
      error => {
        // this.commonLoaderService.hideLoader();
        this.jobDetailsLoaderFlag = false;
        this.jobDetails1LoaderFlag = false;
        this.toastr.error('Some error occured. Please try again later');
      }
    );
  }
  getContracts() {
    this.marketingJobService
      .GetContractList()
      .subscribe((data: IResponseData) => {
        if (data.statusCode) {
          this.contractsList = data.data;
        }
      });
  }

  getContractsByClient() {
    this.marketingJobService
      .GetContractListByClient()
      .subscribe((data: IResponseData) => {
        this.contractsListByClient = data.data;
      });
  }

  onJobDetailUpdate(ev, data) {
    if (ev === 'jobname') {
      this.jobDetails.JobName = data;
      if (
        this.jobDetails.JobName !== '' ||
        this.jobDetails.JobName !== undefined ||
        this.jobDetails.JobName !== null
      ) {
        this.onControllerValueChanged(ev, data);
      }
    }
    if (ev === 'endDate') {
      this.jobDetails.EndDate = data;
      this.onControllerValueChanged(ev, data);
    }
    if (ev === 'contractname') {
      this.jobDetails.ContractId = data;
      this.onControllerValueChanged(ev, data);
      this.onContractChange(data);
    }
    if (ev === 'minutes') {
      this.jobDetails.Minutes = data;
      this.onControllerValueChanged(ev, data);
      this.calculateJobPrice(ev, data);
    }
    if (ev === 'rate') {
      this.jobDetails.FinalRate = data;
      this.onControllerValueChanged(ev, data);
      this.calculateJobPrice(ev, data);
    }
    if (ev === 'total') {
      this.jobDetails.TotalPrice = data;
      this.onControllerValueChanged(ev, data);
    }

    if (
      this.jobDetails.JobId === 0 ||
      this.jobDetails.JobId === null ||
      this.jobDetails.JobId === undefined
    ) {
      this.approve = true;
      this.decline = true;
    } else {
    }
  }

  onContractChange(ev) {
    const str = this.contractsListByClient.find(x => x.ContractId === ev);
    this.unitRate = str.UnitRate;
    this.jobDetails.UnitRate = this.unitRate;
    if (
      this.jobDetails.Minutes !== 0 &&
      this.jobDetails.Minutes !== undefined &&
      this.jobDetails.Minutes !== null
    ) {
      this.finalPrice = this.jobDetails.Minutes * this.unitRate;
      this.jobDetailsForm.controls['total'].setValue(
        this.jobDetails.Minutes * this.jobDetails.FinalRate
      );
      this.discount = this.jobDetails.ActualPrice - this.finalPrice;
      this.discountPercent =
        (this.discount / this.jobDetails.ActualPrice) * 100;
    }
    this.finalRate = this.unitRate;
    this.jobDetails.ActualPrice = this.unitRate * this.jobDetails.Minutes;

    this.jobDetailsForm.controls['rate'].setValue(this.unitRate);
    this.onControllerValueChanged('rate', this.unitRate);
    this.jobDetails.FinalRate = this.unitRate;
  }

  calculateJobPrice(ev, value) {
    if (ev === 'minutes') {
      this.jobDetails.Minutes = value;
    }
    if (ev === 'rate') {
      this.jobDetails.FinalRate = value;
    }
    this.jobDetails.ActualPrice = this.unitRate * this.jobDetails.Minutes;
    if (
      this.jobDetails.Minutes !== 0 &&
      this.jobDetails.Minutes !== undefined &&
      this.jobDetails.Minutes !== null
    ) {
      this.jobDetails.TotalPrice =
        this.jobDetails.Minutes * this.jobDetails.FinalRate;
      this.jobDetails.Discount =
        this.jobDetails.ActualPrice - this.jobDetails.TotalPrice;
      this.jobDetails.DiscountPercent =
        (this.jobDetails.Discount / this.jobDetails.ActualPrice) * 100;
      this.jobDetailsForm.controls['total'].setValue(
        this.jobDetails.TotalPrice
      );
    }
    this.onControllerValueChanged('total', this.jobDetails.TotalPrice);
    if (this.jobDetails.TotalPrice !== 0) {
      this.finalPrice = this.jobDetails.TotalPrice;
      this.finalRate = this.jobDetails.FinalRate;
      this.discount = this.jobDetails.Discount;
      this.discountPercent = this.jobDetails.DiscountPercent;
    }
  }

  onValueChanged(data?: any) {
    if (!this.jobDetailsForm) {
      return;
    }
    const form = this.jobDetailsForm;
    for (const field in this.formErrors) {
      if (this.formErrors.hasOwnProperty(field)) {
        // clear previous error message (if any)
        this.formErrors[field] = '';
        const control = form.get(field);
        if (control && !control.valid) {
          const messages = this.validationMessages[field];
          for (const key in control.errors) {
            if (control.errors.hasOwnProperty(key)) {
              this.formErrors[field] += messages[key] + ' ';
            }
          }
        }
      }
    }
  }

  onControllerValueChanged(data?: any, id?: any) {
    if (!this.jobDetailsForm) {
      return;
    }
    const form = this.jobDetailsForm;
    for (const field in this.formErrors) {
      if (this.formErrors.hasOwnProperty(field)) {
        // clear previous error message (if any)
        if (field === data && id !== '' && id !== undefined && id !== null) {
          this.formErrors[field] = '';
        } else if (
          field === data &&
          id === '' &&
          id === 0 &&
          id === undefined &&
          id === null
        ) {
          this.formErrors[field] = '';
          const control = form.get(field);
          if (control && !control.valid) {
            const messages = this.validationMessages[field];
            for (const key in control.errors) {
              if (control.errors.hasOwnProperty(key)) {
                this.formErrors[field] += messages[key] + ' ';
              }
            }
          }
        }
      }
    }
  }

  fetchInvoice() {
    this.invoiceDetails = {};
    this.marketingJobService.FetchInvoice(this.id).subscribe(
      (data: IResponseData) => {
        if (data.statusCode === 200) {
          if (data !== undefined) {
            this.invoiceDetails = {
              ClientName: data.data.ClientName,
              CurrencyCode: data.data.CurrencyCode,
              EndDate: StaticUtilities.setLocalDate(data.data.EndDate),
              FinalPrice: data.data.FinalPrice,
              InvoiceId: data.data.InvoiceId,
              IsApproved: data.data.IsApproved,
              IsScheduleExist: data.data.IsScheduleExist,
              JobId: data.data.JobId,
              JobName: data.data.JobName,
              JobRate: data.data.JobRate,
              TotalMinutes: data.data.TotalMinutes,
              TotalRunningMinutes: data.data.TotalRunningMinutes
            };
            this.disableInvoiceGenerationBtn = true;
          }
        }
        if (data.statusCode === 120) {
          this.invoiceDetails = {};
          this.printInvoiceFlag = true;
          this.disableInvoiceBtn = true;
        } else {
          this.printInvoiceFlag = false;
        }
      },
      error => {
        this.toastr.error('Some error occured. Please try again later');
        this.disableInvoiceBtn = true;
      }
    );
  }
  // Checker() {
  //   const currentDate = new Date(
  //     new Date().getFullYear(),
  //     new Date().getMonth(),
  //     new Date().getDate()
  //   );
  //   if (currentDate >= this.invoiceDetails.EndDate) {
  //     this.disableInvoiceBtn = false;
  //     this.disableInvoiceGenerationBtn = false;
  //     this.exportPdfBtn = false;
  //   } else {
  //     this.disableInvoiceBtn = true;
  //     this.disableInvoiceGenerationBtn = true;
  //     this.exportPdfBtn = true;
  //   }
  // }
  generateInvoice() {
    this.invoiceDetails = {};
    this.genrateInvoiceSpinnerFlag = true;
    this.marketingJobService.GenerateInvoice(this.id).subscribe(
      (data: IResponseData) => {
        if (data.statusCode === 200) {
          if (data !== undefined) {
            this.invoiceDetails = data.data;
            this.invoiceDetails.EndDate = StaticUtilities.setLocalDate(
              data.data.EndDate
            );
             this.disableInvoiceGenerationBtn = true;
             this.disableInvoiceBtn = false;
          }
        }
        if (data.statusCode === 120) {
          this.toastr.error(data.message);
        } else {
        }
        this.genrateInvoiceSpinnerFlag = false;
      },
      error => {
        this.genrateInvoiceSpinnerFlag = false;
        this.toastr.error('Some error occured. Please try again later');
      }
    );
  }

  generateInvoicePdf() {
    let job: InvoiceModel = {};

    if (this.printInvoiceFlag === true) {
      job = {
        JobName: this.jobDetails.JobName,
        ClientName: this.jobDetails.ClientName,
        JobRate: this.jobDetails.FinalPrice,
        TotalMinutes: this.jobDetails.Minutes,
        TotalRunningMinutes: 0,
        FinalPrice: 0,
        CurrencyCode: this.jobDetails.CurrencyCode
      };
    } else {
      job = {
        InvoiceId: this.invoiceDetails.InvoiceId,
        JobName: this.invoiceDetails.JobName,
        ClientName: this.invoiceDetails.ClientName,
        JobRate: this.invoiceDetails.JobRate,
        TotalMinutes: this.invoiceDetails.TotalMinutes,
        TotalRunningMinutes: this.invoiceDetails.TotalRunningMinutes,
        FinalPrice: this.invoiceDetails.FinalPrice,
        CurrencyCode: this.jobDetails.CurrencyCode
      };
    }

    const todayDate =
      new Date().getDate() +
      '-' +
      (new Date().getMonth() + 1) +
      '-' +
      new Date().getFullYear();
    const doc = new jsPDF();
    doc.setFontSize(10);
    const img = new Image();
    img.src = './assets/img/agreement-logo-small.png';
    img.addEventListener('load', function() {
      // start red border
      doc.setDrawColor(255, 0, 0);
      doc.setLineWidth(0.2);
      doc.line(5, 5, 5, 292);
      doc.line(5, 5, 205, 5);
      doc.line(205, 292, 5, 292);
      doc.line(205, 5, 205, 292);
      // end red border
      doc.addImage(img, 'png', 170, 10);

      doc.setFontType('bold');
      doc.setFontSize(14);
      doc.text('Radio Nawa', 50, 25);
      doc.setFontSize(10);
      doc.text('Invoice Number:', 150, 50);
      doc.text('' + job.InvoiceId + '', 185, 50);
      doc.text('Org:', 15, 50);
      doc.text('Title:', 15, 60);
      doc.text('' + job.JobName + '', 25, 60);
      doc.text('Invoice Date:', 150, 60);
      doc.text('' + todayDate + '', 175, 60);
      doc.setFontSize(12);

      doc.setFontSize(10);
      // start black outlining for invoice content
      doc.setDrawColor(0, 0, 0);
      doc.setLineWidth(0.2);
      doc.line(10, 70, 10, 272);
      doc.line(10, 70, 200, 70);
      doc.line(200, 272, 10, 272);
      doc.line(200, 70, 200, 272);
      // end black outlining of invoice content

      // start table inward lining
      doc.line(60, 70, 60, 272);
      doc.line(95, 70, 95, 272);
      doc.line(130, 70, 130, 272);
      doc.line(165, 70, 165, 272);
      // end table inward lining

      // start table heades
      doc.setFontType('bold');
      doc.setFontSize(14);
      doc.text('Description', 22, 78);
      doc.text('Quantity', 68, 78);
      doc.text('Duration(min)', 97, 78);
      doc.text('Unit Price', 136, 78);
      doc.text('Amount', 173, 78);
      // end table headers

      // start table values
      doc.setFontType('normal');
      doc.setFontSize(12);
      doc.text('' + job.TotalRunningMinutes + '', 110, 100);
      doc.text('' + job.JobRate + '', 143, 100);
      doc.text('' + job.FinalPrice + '', 177, 100);

      // end table values

      doc.setFontType('bold');
      doc.setFontSize(14);
      doc.text('Total', 30, 265);
      doc.text('' + job.FinalPrice + '', 177, 265);
      doc.setFontSize(10);
      doc.text('Approved By:', 13, 285);
      doc.text('Received By:', 145, 285);

      doc.line(10, 85, 200, 85);
      doc.line(200, 257, 10, 257);
      doc.setFontSize(24);
      doc.text('INVOICE', 85, 55);
      doc.setFontType('normal');
      doc.setFontSize(10);
      doc.setFontType('bold');
      doc.setFontType('normal');
      doc.save('Invoice.pdf');
    });
  }
  RejectInvoice() {
    this.rejectInvoiceSpinnerFlag = true;
    this.invoiceDetails = {};
    this.marketingJobService.RemoveInvoice(this.id).subscribe(
      (data: IResponseData) => {
        if (data.statusCode === 200) {
          this.rejectInvoiceSpinnerFlag = false;
          this.disableInvoiceGenerationBtn = false;
          this.disableInvoiceBtn = true;
          this.toastr.success('Invoice Rejected successfully');
        } else {
          this.rejectInvoiceSpinnerFlag = false;
          this.toastr.success('Unable To Rejected Invoice');
        }
      },
      error => {
        this.approveInvoiceSpinnerFlag = false;
        this.toastr.error('Some error occured. Please try again later');
      }
    );
  }
  ApproveInvoice() {
    this.approveInvoiceSpinnerFlag = true;
    this.marketingJobService.ApproveInvoice(this.id).subscribe(
      (data: IResponseData) => {
        if (data.statusCode === 200) {
           this.disableInvoiceBtn = true;
          // this.exportPdfBtn = false;
          this.toastr.success(data.message);
        }
        if (data.statusCode === 120) {
          this.toastr.error(data.message);
        } else {
          // this.toastr.error('Some error occured. Please try again later');
        }
        this.approveInvoiceSpinnerFlag = false;
      },
      error => {
        this.approveInvoiceSpinnerFlag = false;
        this.toastr.error('Some error occured. Please try again later');
      }
    );
  }
}
