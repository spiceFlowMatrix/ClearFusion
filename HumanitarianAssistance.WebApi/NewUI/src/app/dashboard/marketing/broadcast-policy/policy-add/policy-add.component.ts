import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { MediumModel, MediaCategoryModel, LanguageModel } from '../../contracts/model/contract-details.model';
import { PolicyModel } from '../model/policy-model';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { BroadcastPolicyService } from '../service/broadcast-policy.service';
import { GLOBAL } from 'src/app/shared/global';
import { ContractsService } from '../../contracts/service/contracts.service';
import { ToastrService } from 'ngx-toastr';
import { ProducerModel } from '../../master-pages/model/mastrer-pages.model';

@Component({
  selector: 'app-policy-add',
  templateUrl: './policy-add.component.html',
  styleUrls: ['./policy-add.component.scss']
})
export class PolicyAddComponent implements OnInit {
  onListRefresh = new EventEmitter();
  policyDetailsForm: FormGroup;
  policyDetails: PolicyModel = {};
  mediums: MediumModel[];
  mediaCategories: MediaCategoryModel[];
  languages: LanguageModel[];
  producers: ProducerModel[];
  addPolicyLoaderFlag = false;
  addPolicyLoader = false;
  // tslint:disable-next-line:max-line-length
  constructor(private toastr: ToastrService, private contractService: ContractsService, @Inject(MAT_DIALOG_DATA) public data: DataSources, private appurl: AppUrlService,
  public dialogRef: MatDialogRef<PolicyAddComponent>, private policyService: BroadcastPolicyService) { }

  ngOnInit() {
    this.initForm();
    this.MasterPageValues();
  }

  policyListRefresh(data) {
    this.onListRefresh.emit(data);
  }

  initForm() {
    this.policyDetailsForm = new FormGroup({
      policyName: new FormControl('', [(Validators.required)]),
      mediumname: new FormControl('', [(Validators.required)]),
      categoryname: new FormControl('', [(Validators.required)]),
      languagename: new FormControl('', [(Validators.required)]),
      producername: new FormControl('', [(Validators.required)]),
      description: new FormControl('')
    });
  }

  MasterPageValues() {
    this.addPolicyLoaderFlag = true;
    this.contractService
      .GetMasterPagesList(
        this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMasterPagesValues
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.mediums = data.data.Mediums;
          this.mediaCategories = data.data.MediaCategories;
          this.languages = data.data.Languages;
          this.producers = data.data.Producers;
        } else {
          this.toastr.error('Some error occured. Please try again later');
        }
        this.addPolicyLoaderFlag = false;
      },
      error => {
        this.addPolicyLoaderFlag = false;
        this.toastr.error('Some error occured. Please try again later');
      });
  }

  onCancelPopup(): void {
    this.dialogRef.close();
  }

  onSubmit(data): void {
    if (this.policyDetailsForm.valid) {
       this.addPolicyLoader = true;
       this.policyDetails.PolicyName = this.policyDetailsForm.controls.policyName.value;
       this.policyDetails.MediumId = this.policyDetailsForm.controls.mediumname.value;
       this.policyDetails.MediaCategoryId = this.policyDetailsForm.controls.categoryname.value;
       this.policyDetails.LanguageId = this.policyDetailsForm.controls.languagename.value;
       this.policyDetails.Description = this.policyDetailsForm.controls.description.value;
       this.policyDetails.ProducerId = this.policyDetailsForm.controls.producername.value;
       this.policyService
       .AddPolicy(
         this.appurl.getApiUrl() + GLOBAL.API_Policy_AddNewPolicy, this.policyDetails
       )
       .subscribe(data1 => {
         if (data1.StatusCode === 200) {
            this.addPolicyLoader = false;
            this.policyDetails = data1.data.policyDetails;
            this.onCancelPopup();
            this.toastr.success(data1.Message);
            this.policyListRefresh(data1);
         } else {
          this.addPolicyLoader = false;
          this.toastr.error(data1.Message);
         }
       },
       error => {
         this.addPolicyLoader = false;
         this.toastr.error('Some error occured. Please try again later');
       });
    }
  }
}

interface DataSources {
  data: any;
}
