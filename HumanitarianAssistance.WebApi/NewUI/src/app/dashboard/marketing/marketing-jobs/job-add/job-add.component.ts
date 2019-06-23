import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MarketingJobDetailModel, ContractsListByClient } from '../model/marketing-jobs.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MarketingJobsService } from '../service/marketing-jobs.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { DataSources } from './job-detail.model';

@Component({
  selector: 'app-job-add',
  templateUrl: './job-add.component.html',
  styleUrls: ['./job-add.component.scss']
})
export class JobAddComponent implements OnInit {
  onListRefresh = new EventEmitter();
  unitRate?: number;
  jobDetails: MarketingJobDetailModel = {};
  contractsListByClient: ContractsListByClient[];
  jobFilterOption: any[];
  jobListLoaderFlag = false;
  addJobLoaderFlag = false;
  addJobLoader = false;
  jobDetailsForm: FormGroup;
  length:  number;
  constructor(
    private toastr: ToastrService,
    private marketingJobService: MarketingJobsService,
    private appurl: AppUrlService,
    @Inject(MAT_DIALOG_DATA) public data: DataSources,
    public dialogRef: MatDialogRef<JobAddComponent>) { }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.jobDetailsForm = new FormGroup({
      jobname: new FormControl('', [Validators.required]),
      endDate: new FormControl('', [Validators.required]),
      contractname: new FormControl('', [Validators.required]),
      minutes: new FormControl('', [Validators.required, Validators.min(0)]),
      rate: new FormControl('', [Validators.required, Validators.min(0)]),
      total: new FormControl('', [Validators.required, Validators.min(0)]),
      unitRate: new FormControl('', [Validators.required, Validators.min(0)]),
      discount:  new FormControl('', [Validators.required]),
      discountPercent:  new FormControl('', [Validators.required])
    });
  }

  jobListRefresh() {
    this.onListRefresh.emit();
  }

  calculateJobPrice(ev, value) {
    if (ev === 'minutes') {
      this.jobDetails.Minutes = value;
    }
    if (ev === 'rate') {
      this.jobDetails.FinalRate = value;
    }
    this.jobDetails.ActualPrice = this.unitRate * this.jobDetails.Minutes;
    // tslint:disable-next-line:max-line-length
    if (this.jobDetails.Minutes !== undefined &&  this.jobDetails.Minutes !== null && this.jobDetails.UnitRate !== 0 && this.jobDetails.UnitRate !== undefined &&  this.jobDetails.Minutes !== null) {
      this.jobDetails.TotalPrice = this.jobDetails.Minutes * this.jobDetails.FinalRate;
    this.jobDetails.Discount = this.jobDetails.ActualPrice - this.jobDetails.TotalPrice;
    this.jobDetails.DiscountPercent = (this.jobDetails.Discount / this.jobDetails.ActualPrice) * 100;
    if ( this.jobDetails.TotalPrice !== 0 && this.jobDetails.TotalPrice !== undefined &&  this.jobDetails.TotalPrice !== null) {
      this.jobDetailsForm.controls['discount'].setValue(this.jobDetails.Discount);
      this.jobDetailsForm.controls['discountPercent'].setValue(parseFloat(this.jobDetails.DiscountPercent.toFixed(2)));
      this.jobDetailsForm.controls['total'].setValue(this.jobDetails.TotalPrice);
    }
    }
  }
  onContractChange(data) {
    this.unitRate = 0;
    const str = this.data.contractsList.find(x => x.ContractId === data);
    this.unitRate = str.UnitRate;
    this.jobDetails.FinalRate =  this.unitRate;
    this.jobDetails.UnitRate =  this.unitRate;
    this.jobDetailsForm.controls['unitRate'].setValue(this.unitRate);
    this.jobDetailsForm.controls['rate'].setValue(this.unitRate);
  }

  onCancelPopup(): void {
    this.dialogRef.close();
  }

  onSubmit(data): void {
    this.addJobLoader = true;
    if (this.jobDetailsForm.valid) {
      this.jobDetails.ContractId = this.jobDetailsForm.controls.contractname.value;
      this.jobDetails.Discount = this.jobDetailsForm.controls.discount.value;
      this.jobDetails.DiscountPercent = this.jobDetailsForm.controls.discountPercent.value;
      this.jobDetails.JobName = this.jobDetailsForm.controls.jobname.value;
      this.jobDetails.EndDate = this.jobDetailsForm.controls.endDate.value;
      this.jobDetails.Minutes = this.jobDetailsForm.controls.minutes.value;
      this.jobDetails.FinalRate = this.jobDetailsForm.controls.rate.value;
      this.jobDetails.FinalPrice = this.jobDetailsForm.controls.total.value;
      this.jobDetails.UnitRate = this.jobDetailsForm.controls.unitRate.value;
      this.marketingJobService
     .AddJobDetail(this.jobDetails
      ).subscribe((response: IResponseData) => {
       if (response.statusCode === 200) {
         this.onCancelPopup();
         this.jobListRefresh();
        this.toastr.success(response.message);
        this.jobDetails = response.data;
        this.jobDetails.count = response.total;
       // this.jobsList.push(this.jobDetails);
       this.length = this.jobDetails.count;
       this.jobDetails = {};
       this.jobDetailsForm.reset();
       } else {
        this.toastr.error(response.message);
       }
       this.addJobLoader = false;
      },
      error => {
        this.addJobLoader = false;
        this.toastr.error('Some error occured. Please try again later');
      });
    }
  }
}


