import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  OnChanges,
  HostListener
} from '@angular/core';
import {
  TimeCategoryModel,
  MediumModel,
  ApproveContractModel,
  MediaCategoryModel,
  NatureModel,
  LanguageModel,
  CurrencyModel,
  ContractDetailsModel,
  ActivityTypeModel,
  QualityModel
} from '../model/contract-details.model';
import { ContractsService } from '../service/contracts.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import {
  FormGroup,
  Validators,
  FormControl,
  FormBuilder
} from '@angular/forms';
import {
  ClientDetailsModel,
  ClientNameModel
} from '../../clients/model/client.model';
import { UnitRateModel } from '../../master-pages/model/mastrer-pages.model';
import { ToastrService } from 'ngx-toastr';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { ContractApprovalComponent } from '../contract-approval/contract-approval.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Observable } from 'rxjs/internal/Observable';
import { MatDialog } from '@angular/material/dialog';
import { MatAutocomplete } from '@angular/material/autocomplete';

@Component({
  selector: 'app-contract-details',
  templateUrl: './contract-details.component.html',
  styleUrls: ['./contract-details.component.scss']
})
export class ContractDetailsComponent implements OnInit, OnChanges {
  formErrors = {
    clientName: '',
    currency: '',
    startDate: '',
    endDate: '',
    language: '',
    medium: '',
    jobNature: '',
    timeCategory: '',
    mediaCategory: '',
    quality: '',
    activityType: ''
  };
  validationMessages = {
    clientName: {
      required: 'Client Name is required.'
    },
    currency: {
      required: 'Currency is required.'
    },
    startDate: {
      required: 'Start Date is required.'
    },
    endDate: {
      required: 'End Date is required.'
    },
    language: {
      required: 'Language is required.'
    },
    medium: {
      required: 'Medium is required.'
    },
    jobNature: {
      required: 'Nature is required.'
    },
    timeCategory: {
      required: 'Time Category is required.'
    },
    mediaCategory: {
      required: 'Media Category is required.'
    },
    quality: {
      required: 'Quality is required.'
    },
    activityType: {
      required: 'Activity Type is required.'
    }
  };

  approveContractModel: ApproveContractModel = {};
  contractDetails: ContractDetailsModel = {};
  contractDetailsModel: ContractDetailsModel = {};
  validateForm = false;
  clientId: any;
  timecategories: TimeCategoryModel[];
  natures: NatureModel[];
  activitytypes: ActivityTypeModel[];
  mediacategories: MediaCategoryModel[];
  mediums: MediumModel[];
  languages: LanguageModel[];
  currencies: CurrencyModel[] = [];
  contractDetailsForm;
  clientNameModel: ClientNameModel[] = [];
  clientList: ClientDetailsModel[];
  unitRateModel: UnitRateModel = {};
  qualityModel: QualityModel[];
  viewContract: boolean;
  declineContract = true;
  approveContract = true;
  saveContractBool: boolean;
  saveContractBtn = false;
  selectedcontractId: number;
  confirmApproval = false;
  confirmDecline = false;
  types: string;
  hideSaveLoader = false;
  archiveButton = false;
  displayModal = '';
  contractType = '';
  length?: number;
  flag = false;
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  contractDetailLoaderFlag = false;
  unitRateLoader = false;
  confirmApprovalLoader = false;
  disableControls = false;
  isApproveRejectAllowed = false;
  pageId = ApplicationPages.Assets;
  cId: any;
  id: string;
  filteredOptions: Observable<any[]>;

  @Input() contractId: any;
  @Input() isEditingAllowed: boolean;
  @Output() deleteContract = new EventEmitter<any>();
  @Output() addContractList = new EventEmitter<any>();
  @Output() updateContractList = new EventEmitter<any>();
  @Output() hideDetailPanel = new EventEmitter<any>();

  @ViewChild(MatAutocomplete) matAutocomplete: MatAutocomplete;

  constructor(
    public dialog: MatDialog,
    private localStorageService: LocalStorageService,
    private toastr: ToastrService,
    private contractService: ContractsService,
    private appurl: AppUrlService,
  ) {
    this.getScreenSize();
  }

  ngOnChanges(): void {
    if (this.contractId !== 0 && this.contractId !== undefined) {
      this.archiveButton = true;
      this.GetContractById(this.contractId);
    } else {
      this.archiveButton = false;
    }
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

  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }

  initForm() {
    this.contractDetailsForm = new FormGroup({
      contractCode: new FormControl(''),
      clientId: new FormControl(''),
      clientName: new FormControl('', [Validators.required]),
      unitRate: new FormControl(''),
      currency: new FormControl('', [Validators.required]),
      startDate: new FormControl('', [Validators.required]),
      endDate: new FormControl('', [Validators.required]),
      language: new FormControl('', [Validators.required]),
      medium: new FormControl('', [Validators.required]),
      jobNature: new FormControl('', [Validators.required]),
      timeCategory: new FormControl('', [Validators.required]),
      // mediaCategory: new FormControl('', [Validators.required]),
      quality: new FormControl('', [Validators.required]),
      activityType: new FormControl('', [Validators.required])
    });
  }

  ngOnInit() {
    this.initForm();
    this.MasterPageValues();
    this.selectedcontractId = 0;
    this.getClientList();
    this.isApproveRejectAllowed = this.localStorageService.IsApproveRejectAllowed(
      this.pageId
    );
    // this.onValueChanged();
  }

  getClientList() {
    this.contractService
      .GetClientList(this.appurl.getApiUrl() + GLOBAL.API_Client_GetAllClient)
      .subscribe(data => {
        this.clientList = data.data.ClientDetails;
        data.data.ClientDetails.forEach(element => {
          this.clientNameModel.push({
            ClientId: element.ClientId,
            ClientName: element.ClientName
          });
        });
      });
  }

  GetContractById(cId) {
    this.saveContractBtn = false;
    this.hideSaveLoader = false;
    if (cId === 0) {
      this.SetDefaultValues();
      this.onValueChanged();
    } else {
      this.contractDetailLoaderFlag = true;
      // this.commonLoaderService.showLoader();
      this.contractDetailsModel = {};
      // tslint:disable-next-line:max-line-length
      this.contractService
        .GetContractById(
          this.appurl.getApiUrl() + GLOBAL.API_Contract_GetContractDetails,
          cId
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.selectedcontractId = cId;
              this.contractDetailsModel = data.data.contractDetailsModel;
              // setTimeout(() => {
              this.contractDetailsForm.controls['clientName'].setValue(
                this.contractDetailsModel.ClientId
              );
              this.contractDetailsForm.controls['unitRate'].setValue(
                this.contractDetailsModel.UnitRate
              );
              this.contractDetailsForm.controls['currency'].setValue(
                this.contractDetailsModel.CurrencyId
              );
              this.contractDetailsForm.controls['startDate'].setValue(
                this.contractDetailsModel.StartDate
              );
              this.contractDetailsForm.controls['endDate'].setValue(
                this.contractDetailsModel.EndDate
              );
              this.contractDetailsForm.controls['language'].setValue(
                this.contractDetailsModel.LanguageId
              );
              this.contractDetailsForm.controls['medium'].setValue(
                this.contractDetailsModel.MediumId
              );
              this.contractDetailsForm.controls['jobNature'].setValue(
                this.contractDetailsModel.NatureId
              );
              this.contractDetailsForm.controls['timeCategory'].setValue(
                this.contractDetailsModel.TimeCategoryId
              );
              this.contractDetailsForm.controls['quality'].setValue(
                this.contractDetailsModel.QualityId
              );
              this.contractDetailsForm.controls['activityType'].setValue(
                this.contractDetailsModel.ActivityTypeId
              );
              // tslint:disable-next-line:max-line-length
              if (
                this.contractDetailsModel.UnitRate !== undefined &&
                this.contractDetailsModel.UnitRate !== 0 &&
                this.contractDetailsModel.UnitRate !== null
              ) {
                this.validateForm = true;
              } else {
                this.validateForm = false;
              }
              this.onValueChanged();
              // this.commonLoaderService.hideLoader();
              this.contractDetailLoaderFlag = false;
              this.unitRateModel.ActivityTypeId = this.contractDetailsModel.ActivityTypeId;
              this.unitRateModel.CurrencyId = this.contractDetailsModel.CurrencyId;
              this.unitRateModel.LanguageId = this.contractDetailsModel.LanguageId;
              // this.unitRateModel.MediaCategoryId = this.contractDetails.MediaCategoryId;
              this.unitRateModel.MediumId = this.contractDetailsModel.MediumId;
              this.unitRateModel.NatureId = this.contractDetailsModel.NatureId;
              this.unitRateModel.QualityId = this.contractDetailsModel.QualityId;
              this.unitRateModel.TimeCategoryId = this.contractDetailsModel.TimeCategoryId;
              this.unitRateModel.UnitRate = this.contractDetailsModel.UnitRate;
              this.unitRateModel.UnitRateId = this.contractDetailsModel.UnitRateId;
              if (this.contractDetailsModel.IsApproved === true) {
                this.disableControls = true;
                this.viewContract = true;
                this.approveContract = false;
                this.declineContract = false;
                this.confirmApproval = true;
                this.confirmDecline = false;
                this.contractDetailsForm.disable();
              } else if (this.contractDetailsModel.IsDeclined === true) {
                this.disableControls = true;
                this.viewContract = true;
                this.approveContract = false;
                this.declineContract = false;
                this.confirmDecline = true;
                this.confirmApproval = false;
                this.contractDetailsForm.disable();
              } else {
                this.disableControls = false;
                this.viewContract = false;
                this.approveContract = true;
                this.declineContract = true;
                this.confirmApproval = false;
                this.confirmDecline = false;
                this.contractDetailsForm.enable();
              }
              this.contractType = '';
            } else {
              this.disableControls = false;
              // this.commonLoaderService.hideLoader();
              this.contractDetailLoaderFlag = false;
              this.toastr.error(data.Message);
            }

            // this.onValueChanged();
          },
          error => {
            // this.commonLoaderService.hideLoader();
            this.contractDetailLoaderFlag = false;
            this.toastr.error('Some error occured. Please try again later');
          }
        );
    }
  }

  MasterPageValues() {
    this.contractService
      .GetMasterPagesList(
        this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMasterPagesValues
      )
      .subscribe(data => {
        this.natures = data.data.Natures;
        this.mediums = data.data.Mediums;
        this.mediacategories = data.data.MediaCategories;
        this.timecategories = data.data.TimeCategories;
        this.languages = data.data.Languages;
        this.currencies = data.data.Currencies;
        this.activitytypes = data.data.ActivityTypes;
        this.qualityModel = data.data.Qualities;
        this.SetDefaultValues();
      });
  }

  SetDefaultValues() {
    this.unitRateModel = {};
    this.contractDetailsModel = {};
    this.setValuefordropdown();
    this.unitRateModel.CurrencyId = this.currencies[0].CurrencyId;
    this.contractDetailsModel.CurrencyId = this.currencies[0].CurrencyId;
    this.unitRateModel.LanguageId = this.languages[0].LanguageId;
    this.contractDetailsModel.LanguageId = this.languages[0].LanguageId;
    this.unitRateModel.MediumId = this.mediums[0].MediumId;
    this.contractDetailsModel.MediumId = this.mediums[0].MediumId;
    this.unitRateModel.NatureId = this.natures[0].NatureId;
    this.contractDetailsModel.NatureId = this.natures[0].NatureId;
    this.unitRateModel.TimeCategoryId = this.timecategories[0].TimeCategoryId;
    this.contractDetailsModel.TimeCategoryId = this.timecategories[0].TimeCategoryId;
    this.unitRateModel.QualityId = this.qualityModel[0].QualityId;
    this.contractDetailsModel.QualityId = this.qualityModel[0].QualityId;
  }

  setValuefordropdown() {
    this.contractDetailsForm.controls['currency'].setValue(
      this.currencies[0].CurrencyId
    );
    this.contractDetailsForm.controls['language'].setValue(
      this.languages[0].LanguageId
    );
    this.contractDetailsForm.controls['medium'].setValue(
      this.mediums[0].MediumId
    );
    this.contractDetailsForm.controls['jobNature'].setValue(
      this.natures[0].NatureId
    );
    this.contractDetailsForm.controls['timeCategory'].setValue(
      this.timecategories[0].TimeCategoryId
    );
    this.contractDetailsForm.controls['quality'].setValue(
      this.qualityModel[0].QualityId
    );
  }

  selectionChanged(ev, type) {
    if (ev === 'activity') {
      this.unitRateModel.ActivityTypeId = type.value;
      this.contractDetailsModel.ActivityTypeId = type.value;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.ActivityTypeId !== 0 ||
        this.contractDetailsModel.ActivityTypeId !== null ||
        this.contractDetailsModel.ActivityTypeId !== undefined
      ) {
        this.onControllerValueChanged('activityType', type.value);
      }
    }
    if (ev === 'currency') {
      this.unitRateModel.CurrencyId = type;
      this.contractDetailsModel.CurrencyId = type;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.CurrencyId !== 0 ||
        this.contractDetailsModel.CurrencyId !== null ||
        this.contractDetailsModel.CurrencyId !== undefined
      ) {
        this.onControllerValueChanged(ev, type);
      }
    }

    if (ev === 'language') {
      this.unitRateModel.LanguageId = type;
      this.contractDetailsModel.LanguageId = type;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.LanguageId !== 0 ||
        this.contractDetailsModel.LanguageId !== null ||
        this.contractDetailsModel.LanguageId !== undefined
      ) {
        this.onControllerValueChanged(ev, type);
      }
    }

    if (ev === 'medium') {
      this.unitRateModel.MediumId = type;
      this.contractDetailsModel.MediumId = type;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.MediumId !== 0 ||
        this.contractDetailsModel.MediumId !== null ||
        this.contractDetailsModel.MediumId !== undefined
      ) {
        this.onControllerValueChanged(ev, type);
      }
    }

    if (ev === 'jobNature') {
      this.unitRateModel.NatureId = type;
      this.contractDetailsModel.NatureId = type;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.NatureId !== 0 ||
        this.contractDetailsModel.NatureId !== null ||
        this.contractDetailsModel.NatureId !== undefined
      ) {
        this.onControllerValueChanged(ev, type);
      }
    }

    if (ev === 'timeCategory') {
      this.unitRateModel.TimeCategoryId = type;
      this.contractDetailsModel.TimeCategoryId = type;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.TimeCategoryId !== 0 ||
        this.contractDetailsModel.TimeCategoryId !== null ||
        this.contractDetailsModel.TimeCategoryId !== undefined
      ) {
        this.onControllerValueChanged(ev, type);
      }
    }

    if (ev === 'quality') {
      this.unitRateModel.QualityId = type;
      this.contractDetailsModel.QualityId = type;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.QualityId !== 0 ||
        this.contractDetailsModel.QualityId !== null ||
        this.contractDetailsModel.QualityId !== undefined
      ) {
        this.onControllerValueChanged(ev, type);
      }
    }

    if (ev === 'startDate') {
      // tslint:disable-next-line:max-line-length
      this.unitRateModel.StartDate = new Date(
        new Date(type).getFullYear(),
        new Date(type).getMonth(),
        new Date(type).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      );
      this.contractDetailsModel.StartDate = this.unitRateModel.StartDate;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.StartDate !== '' ||
        this.contractDetailsModel.StartDate !== null ||
        this.contractDetailsModel.StartDate !== undefined
      ) {
        this.onControllerValueChanged(ev, type);
      }
    }

    if (ev === 'endDate') {
      // tslint:disable-next-line:max-line-length
      this.unitRateModel.EndDate = new Date(
        new Date(type).getFullYear(),
        new Date(type).getMonth(),
        new Date(type).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      );
      this.contractDetailsModel.EndDate = this.unitRateModel.EndDate;
      this.contractDetails.EndDate = type;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.EndDate !== '' ||
        this.contractDetailsModel.EndDate !== null ||
        this.contractDetailsModel.EndDate !== undefined
      ) {
        this.onControllerValueChanged(ev, type);
      }
    }

    if (ev === 'clientName') {
      const id = type;
      // this.clientNameModel.find(c => c.ClientName === type).ClientId;
      this.unitRateModel.ClientId = id;
      this.contractDetailsModel.ClientId = id;
      // tslint:disable-next-line:max-line-length
      if (
        this.contractDetailsModel.ClientId !== 0 ||
        this.contractDetailsModel.ClientId !== null ||
        this.contractDetailsModel.ClientId !== undefined
      ) {
        this.onControllerValueChanged(ev, type);
      }
    }

    if (this.contractDetailsModel.ActivityTypeId === 2) {
      if (
        this.contractDetailsModel.QualityId === undefined ||
        this.contractDetailsModel.QualityId === null ||
        this.contractDetailsModel.NatureId === 0 ||
        this.contractDetailsModel.NatureId === undefined ||
        this.contractDetailsModel.NatureId === null ||
        this.contractDetailsModel.MediumId === 0 ||
        this.contractDetailsModel.MediumId === undefined ||
        this.contractDetailsModel.MediumId === null ||
        this.contractDetailsModel.CurrencyId === 0 ||
        this.contractDetailsModel.CurrencyId === undefined ||
        this.contractDetailsModel.CurrencyId === null ||
        this.contractDetailsModel.ClientId === undefined ||
        this.contractDetailsModel.ClientId === 0 ||
        this.contractDetailsModel.ClientId === null ||
        this.contractDetailsModel.StartDate === undefined ||
        this.contractDetailsModel.StartDate === '' ||
        this.contractDetailsModel.StartDate === null ||
        this.contractDetailsModel.EndDate === undefined ||
        this.contractDetailsModel.EndDate === '' ||
        this.contractDetailsModel.EndDate === null
      ) {
        this.validateForm = false;
      } else {
        this.unitRateModel.ActivityTypeId = 2;
        this.validateForm = true;
      }
    }

    if (this.contractDetailsModel.ActivityTypeId === 1) {
      if (
        this.contractDetailsModel.TimeCategoryId === 0 ||
        this.contractDetailsModel.TimeCategoryId === undefined ||
        this.contractDetailsModel.TimeCategoryId === null ||
        this.contractDetailsModel.MediumId === 0 ||
        this.contractDetailsModel.MediumId === undefined ||
        this.contractDetailsModel.MediumId === null ||
        this.contractDetailsModel.CurrencyId === 0 ||
        this.contractDetailsModel.CurrencyId === undefined ||
        this.contractDetailsModel.CurrencyId === null ||
        this.contractDetailsModel.ClientId === undefined ||
        this.contractDetailsModel.ClientId === 0 ||
        this.contractDetailsModel.ClientId === null ||
        this.contractDetailsModel.StartDate === undefined ||
        this.contractDetailsModel.StartDate === '' ||
        this.contractDetailsModel.StartDate === null ||
        this.contractDetailsModel.EndDate === undefined ||
        this.contractDetailsModel.EndDate === '' ||
        this.contractDetailsModel.EndDate === null
      ) {
        this.validateForm = false;
      } else {
        this.unitRateModel.ActivityTypeId = 1;
        this.validateForm = true;
      }
    }

    if (this.validateForm === true) {
      // this.commonLoaderService.showLoader();
      this.unitRateLoader = true;
      // tslint:disable-next-line:max-line-length
      this.contractService
        .GetUnitRateDetailsByActivityId(
          this.appurl.getApiUrl() + GLOBAL.API_Contract_GetUnitRateByActivity,
          this.unitRateModel
        )
        .subscribe(data => {
          if (data.StatusCode === 200) {
            this.unitRateModel = data.data.UnitRateByActivityId;
            this.contractDetailsForm.controls['unitRate'].setValue(
              data.data.UnitRateByActivityId.UnitRates
            );
            // this.commonLoaderService.hideLoader();
            this.unitRateLoader = false;
            // this.contractDetailsForm = new FormGroup({
            //   unitRate: new FormControl(data.data.UnitRateByActivityId.UnitRates)
            // });
            this.contractDetailsModel.UnitRateId =
              data.data.UnitRateByActivityId.UnitRateId;
            this.contractDetailsModel.UnitRate =
              data.data.UnitRateByActivityId.UnitRates;
          }
          if (data.StatusCode === 120) {
            // this.commonLoaderService.hideLoader();
            this.unitRateLoader = false;
            // this.toastr.error(data.Message);
            this.contractDetailsForm.controls['unitRate'].setValue();
            this.contractDetailsModel.UnitRateId = 0;
            this.contractDetailsModel.UnitRate = 0;
          }
          // tslint:disable-next-line:max-line-length
          if (
            this.contractDetailsModel.ContractId !== 0 &&
            this.contractDetailsModel.ContractId !== null &&
            this.contractDetailsModel.ContractId !== undefined
          ) {
            this.updateContract();
          }
        });
    }

    // tslint:disable-next-line:max-line-length
    if (
      this.contractDetailsModel.ContractId === 0 ||
      this.contractDetailsModel.ContractId === null ||
      this.contractDetailsModel.ContractId === undefined
    ) {
      // this.approveContract = true;
      // this.declineContract = true;
    }
  }
  onControllerValueChanged(data?: any, id?: any) {
    if (!this.contractDetailsForm) {
      return;
    }
    const form = this.contractDetailsForm;
    for (const field in this.formErrors) {
      if (this.formErrors.hasOwnProperty(field)) {
        // clear previous error message (if any)
        if (field === data && id !== undefined && id !== 0 && id !== null) {
          this.formErrors[field] = '';
        }
      }
    }
  }

  onValueChanged(data?: any) {
    if (!this.contractDetailsForm) {
      return;
    }
    const form = this.contractDetailsForm;
    for (const field in this.formErrors) {
      if (this.formErrors.hasOwnProperty(field)) {
        // clear previous error message (if any)
        this.formErrors[field] = '';
        const control = form.get(field);
        if (control && !control.valid) {
          const messages = this.validationMessages[field];
          for (const key in control.errors) {
            if (control.errors.hasOwnProperty(key)) {
              if (this.unitRateModel.ActivityTypeId === 2) {
                if (field === 'timeCategory') {
                  this.formErrors[field] += ' ';
                } else {
                  this.formErrors[field] += messages[key] + ' ';
                }
              } else if (this.unitRateModel.ActivityTypeId === 1) {
                if (field === 'quality' || field === 'jobNature') {
                  this.formErrors[field] += ' ';
                } else {
                  this.formErrors[field] += messages[key] + ' ';
                }
              } else {
                this.formErrors[field] += messages[key] + ' ';
              }
            }
          }
        }
      }
    }
  }

  updateContract() {
       // tslint:disable-next-line:max-line-length
    this.contractService
    .SaveContract(
      this.appurl.getApiUrl() + GLOBAL.API_Contract_SaveContract,
      this.contractDetailsModel
    )
    .subscribe(data => {
      this.updateContractList.emit(this.contractDetailsModel);
      // this.displayModal = 'block';
    });
  }

  DeleteContract(id) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });

    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText =
      Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText =
      Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => {
    });

    dialogRef.componentInstance.confirmDelete.subscribe(res => {
      dialogRef.componentInstance.isLoading = true;
      this.contractService
        .DeleteContract(
          this.appurl.getApiUrl() + GLOBAL.API_Contract_DeleteContract,
          id
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.toastr.success(data.Message);
              this.length = data.data.jobListTotalCount;
              dialogRef.componentInstance.onCancelPopup();
              this.deleteContract.emit({ id: id });
            } else {
              this.toastr.error('Some error occured. Please try again later');
            }
            dialogRef.componentInstance.isLoading = false;
          },
          error => {
            this.toastr.error('Some error occured. Please try again later');
            dialogRef.componentInstance.isLoading = false;
          }
        );
    });
  }

  saveContract() {
    const id = this.selectedcontractId;
    this.contractDetailsModel.Type = this.contractType;
    if (this.contractDetailsModel.StartDate > this.contractDetailsModel.EndDate) {
      this.toastr.error('Start Date can not be greater than End Date');
    } else {
      this.saveContractBtn = false;
    this.hideSaveLoader = true;
    // tslint:disable-next-line:max-line-length
    this.contractService
      .SaveContract(
        this.appurl.getApiUrl() + GLOBAL.API_Contract_SaveContract,
        this.contractDetailsModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success(data.Message);
            this.hideSaveLoader = false;
            this.contractDetailsModel = data.data.contractDetailsModel;

            // tslint:disable-next-line:max-line-length
            if (
              this.contractDetailsModel.UnitRate !== undefined &&
              this.contractDetailsModel.UnitRate !== 0 &&
              this.contractDetailsModel.UnitRate !== null
            ) {
              this.validateForm = true;
            } else {
              this.validateForm = false;
            }
            this.selectedcontractId = this.contractDetailsModel.ContractId;
            this.addContractList.emit(this.contractDetailsModel);
            // this.approveContractModel.ContractId = id;

            // this.contractDetailsModel.ContractId;
            // tslint:disable-next-line:max-line-length
            // this.contractService.ApproveContract(this.appurl.getApiUrl() + GLOBAL.API_Contract_ApproveContract, this.approveContractModel).subscribe(data1 => {

            // if (this.contractType === 'Approve') {
            //   this.viewContract = true;
            //   this.confirmApproval = true;
            //   this.confirmDecline = false;
            //   this.approveContract = false;
            //   this.declineContract = false;
            // }
            // if (this.contractType === 'Rejected') {
            //   this.viewContract = true;
            //   this.confirmDecline = true;
            //   this.confirmApproval = false;
            //   this.approveContract = false;
            //   this.declineContract = false;
            // }
            // this.contractDetailsForm.disable();
            this.approveContract = true;
            this.declineContract = true;
          } else {
            this.saveContractBtn = true;
            this.toastr.error(data.Message);
            this.hideSaveLoader = false;
          }
          this.displayModal = 'none';
          this.contractType = '';
          this.archiveButton = true;
          // this.contractDetailsForm.controls['clientName'].disable();
          // this.nextLibAvailable=true;
          // // tslint:disable-next-line:forin
          // for (let control in this.contractDetailsForm.controls) {
          //   this.contractDetailsForm.controls[control].disable();
          // }
          // this.contractDetailsForm.disable();
          // });
        },
        error => {
          this.hideSaveLoader = false;
          this.saveContractBtn = true;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
    }
  }

  AddNewContract() {
    this.saveContractBtn = true;
    this.validateForm = false;
    this.contractDetailsForm.enable();
    this.contractType = '';
    this.viewContract = false;
    this.approveContract = false;
    this.declineContract = false;
    this.confirmApproval = false;
    this.confirmDecline = false;
    this.contractDetailsModel = {};
    this.unitRateModel = {};
    this.contractDetailsForm.reset();
    this.selectedcontractId = 0;
    this.contractDetailsModel = {};
    this.archiveButton = false;
    setTimeout(() => {
      this.SetDefaultValues();
    }, 400);
    // this.onValueChanged();
  }

  closeModal() {
    this.contractType = '';
    this.displayModal = 'none';
  }

  FinalizeContract(type) {
    // tslint:disable-next-line:max-line-length
    if (
      this.contractDetailsForm.value.unitRate !== null &&
      this.contractDetailsForm.value.unitRate !== undefined &&
      this.contractDetailsForm.value.unitRate !== 0
    ) {
      this.contractType = type;
      if (this.validateForm === true) {
        this.displayModal = 'block';
      }
    } else {
      this.toastr.error('Unit Rate value can not be empty');
    }
  }

  confirmApprove(data) {
    if (data === 'true') {
 // tslint:disable-next-line:max-line-length
 this.contractService.SaveContract(this.appurl.getApiUrl() + GLOBAL.API_Contract_ApproveContract, this.approveContractModel).subscribe(data1 => {
  if (data1.StatusCode === 200) {
    this.contractDetailsModel = data1.data.contractDetails;
    if (this.contractDetailsModel.IsApproved === true) {
      this.toastr.success(data1.Message);
      this.contractDetailsForm.disable();
      this.confirmApproval = true;
      this.approveContract = false;
      this.declineContract = false;
      this.confirmDecline = false;
      this.viewContract = true;
    } else if (this.contractDetailsModel.IsDeclined === true) {
      this.disableControls = true;
      this.toastr.success(data1.Message);
      this.contractDetailsForm.disable();
      this.confirmApproval = false;
      this.approveContract = false;
      this.declineContract = false;
      this.confirmDecline = true;
      this.viewContract = true;
    } else {
      this.disableControls = false;
      this.confirmApproval = false;
      this.approveContract = true;
      this.declineContract = true;
      this.confirmDecline = false;
      this.viewContract = false;
      this.toastr.error('Some error occured. Please try again later.');
      this.contractDetailsForm.enable();
    }
  } else {
    this.disableControls = false;
    this.confirmApproval = false;
    this.approveContract = true;
    this.declineContract = true;
    this.confirmDecline = false;
    this.viewContract = false;
    this.toastr.error('Some error occured. Please try again later.');
    this.contractDetailsForm.enable();
  }
},
error => {
  this.disableControls = false;
  this.confirmApproval = false;
  this.approveContract = true;
  this.declineContract = true;
  this.confirmDecline = false;
  this.viewContract = false;
  this.toastr.error('Some error occured. Please try again later.');
  this.contractDetailsForm.enable();
  });
    } else {
      this.toastr.error('Some error occured. Please try again later');
    }

  }

  ConfirmSave() {
    this.onValueChanged();
    const form = this.contractDetailsForm;
    // tslint:disable-next-line:max-line-length
    if (
      (this.selectedcontractId === 0 ||
        this.selectedcontractId === undefined ||
        this.selectedcontractId === null) &&
      form.status === 'VALID'
    ) {
      this.saveContract();
    } else if (form.status === 'VALID') {
      this.updateContract();
    }
    // tslint:disable-next-line:max-line-length
  }

  DisplayFirstEntryOfFilteredList(data) {
    if (data!= undefined) {
      this.contractDetailsForm.controls['clientName'].setValue(data.ClientId);
      this.contractDetailsForm.controls['unitRate'].setValue(data.UnitRate);
      this.contractDetailsForm.controls['currency'].setValue(data.CurrencyId);
      this.contractDetailsForm.controls['startDate'].setValue(data.StartDate);
      this.contractDetailsForm.controls['endDate'].setValue(data.EndDate);
      this.contractDetailsForm.controls['language'].setValue(data.LanguageId);
      this.contractDetailsForm.controls['medium'].setValue(data.MediumId);
      this.contractDetailsForm.controls['jobNature'].setValue(data.NatureId);
      this.contractDetailsForm.controls['timeCategory'].setValue(
        data.TimeCategoryId
      );
      this.contractDetailsForm.controls['quality'].setValue(data.QualityId);
      this.contractDetailsForm.controls['activityType'].setValue(
        data.ActivityTypeId
      );
    }
    
  }

  openApproveContractDialog(type) {
    // tslint:disable-next-line:max-line-length
    if (
      this.contractDetailsForm.value.unitRate !== null &&
      this.contractDetailsForm.value.unitRate !== undefined &&
      this.contractDetailsForm.value.unitRate !== 0
    ) {
      this.contractType = type;
      if (this.validateForm === true) {
        this.approveContractModel.Type = this.contractType;
        this.approveContractModel.ContractId = this.selectedcontractId;
        const dialogRef = this.dialog.open(ContractApprovalComponent, {
          width: '350px',
          data: {
            data: this.approveContractModel
          }
        });

        dialogRef.componentInstance.onListRefresh.subscribe(data => {
          // do something
          this.confirmApprove(data);
          dialogRef.componentInstance.onCancelPopup();
        });

        dialogRef.afterClosed().subscribe(result => {
        });
      }
    } else {
      this.toastr.error('Unit Rate value can not be empty');
    }
  }
}
