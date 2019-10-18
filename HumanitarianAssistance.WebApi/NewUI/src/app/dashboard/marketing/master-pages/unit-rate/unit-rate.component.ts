import { Component, OnInit, Input, EventEmitter, Output, HostListener } from '@angular/core';
import {
  TimeCategoryModel,
  MediumModel,
  QualityModel,
  ActivityTypeModel,
  NatureModel,
  CurrencyModel,
  MediaCategoryModel
} from '../../contracts/model/contract-details.model';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ContractsService } from '../../contracts/service/contracts.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MasterPageServiceService } from '../service/master-page-service.service';
import { UnitRateModel } from '../model/mastrer-pages.model';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { MatDialog } from '@angular/material/dialog';
@Component({
  selector: 'app-unit-rate',
  templateUrl: './unit-rate.component.html',
  styleUrls: ['./unit-rate.component.scss']
})
export class UnitRateComponent implements OnInit {
  unitRateDetails: UnitRateModel = {};
  unitRateForm; // These are variables
  activity;
  unitRate;
  currency;
  medium;
  timeCategory;
  nature;
  quality;
  scrollStyles: any;
  screenHeight: any;
  screenWidth: any;
  selectedUnitRateId: number;
  selectedActivityTypeId: number;
  timecategories: TimeCategoryModel[];
  natures: NatureModel[];
  mediums: MediumModel[];
  qualities: QualityModel[];
  activities: ActivityTypeModel[];
  currencies: CurrencyModel[];
  mediaCategories: MediaCategoryModel[];
  validateForm: boolean;
  controlName: string;
  activityType: number;
  archiveUnitRate = false;
  unitRateDetailsLoader = false;
  @Input() routeId: number;
  @Input() isEditingAllowed: boolean;
  @Output() deleteUnitRate = new EventEmitter<any>();
  @Output() addunitRateList = new EventEmitter<any>();
  @Output() testFunc = new EventEmitter<{}>();
  @Output() updateUnitRateListById = new EventEmitter<any>();
  @Output() hideDetailPanel = new EventEmitter<any>();
  formErrors = {
    'activity': '',
    'quality': '',
    'nature': '',
    'timeCategory': '',
    'medium': '',
    'currency': '',
    'unitRate': ''
  };
  validationMessages = {
    'activity': {
      'required': 'Activity is required.'
    },
    'quality': {
      'required': 'Quality is required.'
    },
    'nature': {
      'required': 'Nature is required.'
    },
    'timeCategory': {
      'required': 'Time Category is required.'
    },
    'medium': {
      'required': 'Medium is required.'
    },
    'currency': {
      'required': 'Currency is required.'
    },
    'unitRate': {
      'required': 'Unit Rate is required.'
    }
  };
  // tslint:disable-next-line:max-line-length
  constructor(
    public dialog: MatDialog,
    public toastr: ToastrService,
    private contractService: ContractsService,
    private appurl: AppUrlService,
    private masterPageService: MasterPageServiceService,
  ) {
    this.getScreenSize();
  }
  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    if (this.routeId !== 0 && this.routeId !== undefined) {
      this.archiveUnitRate = true;
      this.GetUnitRateById(this.routeId);
    } else {
      this.archiveUnitRate = false;
    }
  }

  initForm() {
    this.unitRateForm = new FormGroup({
      activity: new FormControl('', [Validators.required]),
      unitRate: new FormControl('', [Validators.required]),
      currency: new FormControl('', [Validators.required]),
      timeCategory: new FormControl('', [Validators.required]),
      nature: new FormControl('', [Validators.required]),
      quality: new FormControl('', [Validators.required]),
      medium: new FormControl('', [Validators.required]),
      unitRateId: new FormControl(),
      mediaCategory: new FormControl()
    });
  }

  ngOnInit() {
    this.initForm();
    this.MasterPageValues();
  }


   //#region "Dynamic Scroll"
   @HostListener('window:resize', ['$event'])
   getScreenSize(event?) {
       this.screenHeight = window.innerHeight;
       this.screenWidth = window.innerWidth;
       this.scrollStyles = {
         'overflow-y': 'auto',
         'height': this.screenHeight - 110 + 'px',
         'overflow-x': 'hidden'
         };
   }
   //#endregion

  Validate(evt) {
    if (
      (evt.which !== 8 && evt.which !== 0 && evt.which < 48) ||
      evt.which > 57
    ) {
      evt.preventDefault();
    }
  }

  GetUnitRateById(id) {
    // this.commonLoaderService.showLoader();
    this.unitRateDetailsLoader = true;
    if (id === 0) {
      this.SetDefaultValues();
      this.onValueChanged();
      this.selectedUnitRateId = 0;
    } else {
      this.initForm();
      this.unitRateDetails = {};
      this.routeId = id;
      this.masterPageService
        .GetById(
          this.appurl.getApiUrl() + GLOBAL.API_Contract_GetUnitRateById,
          id
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.unitRateForm.controls['activity'].setValue(
                data.data.rateDetailsById.ActivityTypeId
              );
              this.unitRateForm.controls['unitRate'].setValue(
                data.data.rateDetailsById.UnitRates
              );
              this.unitRateForm.controls['currency'].setValue(
                data.data.rateDetailsById.CurrencyId
              );
              this.unitRateForm.controls['timeCategory'].setValue(
                data.data.rateDetailsById.TimeCategoryId
              );
              this.unitRateForm.controls['nature'].setValue(
                data.data.rateDetailsById.NatureId
              );
              this.unitRateForm.controls['quality'].setValue(
                data.data.rateDetailsById.QualityId
              );
              this.unitRateForm.controls['medium'].setValue(
                data.data.rateDetailsById.MediumId
              );
              this.unitRateForm.controls['unitRateId'].setValue(
                data.data.rateDetailsById.UnitRateId
              );
              this.unitRateForm.controls['mediaCategory'].setValue(
                data.data.rateDetailsById.MediaCategoryId
              );
              // this.commonLoaderService.hideLoader();
              this.unitRateDetailsLoader = false;
              this.selectedUnitRateId = data.data.rateDetailsById.UnitRateId;
              this.selectedActivityTypeId = data.data.rateDetailsById.ActivityTypeId;
              // this.onChange('activity', this.selectedActivityTypeId);
              this.unitRateDetails = data.data.rateDetailsById;
              this.onValueChanged();
            } else {
              // this.commonLoaderService.hideLoader();
              this.unitRateDetailsLoader = false;
              this.toastr.error('Some error occured. Please try again later');
            }
          },
          error => {
            // this.commonLoaderService.hideLoader();
            this.unitRateDetailsLoader = false;
            this.toastr.error('Some error occured. Please try again later');
          });
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
        this.timecategories = data.data.TimeCategories;
        this.qualities = data.data.Qualities;
        this.activities = data.data.ActivityTypes;
        this.currencies = data.data.Currencies;
        this.mediaCategories = data.data.MediaCategories;
        this.SetDefaultValues();
      });
  }

  SetDefaultValues() {
    this.unitRateForm.controls['activity'].setValue(
      this.activities[0].ActivityTypeId
    );
    this.unitRateForm.controls['currency'].setValue(
      this.currencies[0].CurrencyId
    );
    this.unitRateForm.controls['timeCategory'].setValue(
      this.timecategories[0].TimeCategoryId
    );
    this.unitRateForm.controls['nature'].setValue(this.natures[0].NatureId);
    this.unitRateForm.controls['quality'].setValue(this.qualities[0].QualityId);
    this.unitRateForm.controls['medium'].setValue(this.mediums[0].MediumId);
    this.unitRateForm.controls['mediaCategory'].setValue(
      this.mediaCategories[0].MediaCategoryId
    );
    this.activityType = this.activities[0].ActivityTypeId;
    this.unitRateDetails.ActivityTypeId = this.activities[0].ActivityTypeId;
    this.unitRateDetails.CurrencyId = this.currencies[0].CurrencyId;
    this.unitRateDetails.TimeCategoryId = this.timecategories[0].TimeCategoryId;
    this.unitRateDetails.NatureId = this.natures[0].NatureId;
    this.unitRateDetails.QualityId = this.qualities[0].QualityId;
    this.unitRateDetails.MediumId = this.mediums[0].MediumId;
    this.unitRateDetails.MediaCategoryId = this.mediaCategories[0].MediaCategoryId;
  }

  ValidateFields(ev, data) {
    if (data === 1) {
      this.activityType = 1;
    } else if (data === 2) {
      this.activityType = 2;
    }
    if (ev === 'activity') {
      this.unitRateDetails.ActivityTypeId = data;
    }
    Object.keys(this.unitRateForm.controls).map((controlName) => {
      if (this.activityType === 2) {
        if (controlName === 'timeCategory') {
          return;
        } else {
          // tslint:disable-next-line:no-unused-expression
          this.unitRateForm.get(controlName).markAsTouched({ onlySelf: true }) ||
            this.unitRateForm.get(controlName).markAsDirty({ onlySelf: true });
        }
      }

      if (this.activityType === 1) {
        if (controlName === 'quality' || controlName === 'nature') {
          return;
        } else {
          // tslint:disable-next-line:no-unused-expression
          this.unitRateForm.get(controlName).markAsTouched({ onlySelf: true }) ||
            this.unitRateForm.get(controlName).markAsDirty({ onlySelf: true });
        }
      }
    });
    if (ev === 'activity') {
      this.unitRateDetails.ActivityTypeId = data;
    }
    if (ev === 'medium') {
      this.unitRateDetails.MediumId = data;
    }
    if (ev === 'currency') {
      this.unitRateDetails.CurrencyId = data;
    }
    if (ev === 'nature') {
      this.unitRateDetails.NatureId = data;
    }
    if (ev === 'quality') {
      this.unitRateDetails.QualityId = data;
    }
    if (ev === 'timeCategory') {
      this.unitRateDetails.TimeCategoryId = data;
    }
    if (ev === 'unitRate') {
      this.unitRateDetails.UnitRates = data;
    }
    if (ev === 'mediaCategory') {
      this.unitRateDetails.MediaCategoryId = data;
    }

    if (this.activityType === 1) { // Broadcasting
      if (this.unitRateDetails.TimeCategoryId === 0
         || this.unitRateDetails.TimeCategoryId === undefined
         || this.unitRateDetails.TimeCategoryId === null
         || this.unitRateDetails.CurrencyId === 0
         || this.unitRateDetails.CurrencyId === undefined
         || this.unitRateDetails.CurrencyId === null
         || this.unitRateDetails.MediumId === 0
         || this.unitRateDetails.MediumId === undefined
         || this.unitRateDetails.MediumId === null
         || this.unitRateDetails.UnitRates === 0
         || this.unitRateDetails.UnitRates === undefined
         || this.unitRateDetails.UnitRates === null) {
        this.validateForm = false;
      } else {
        this.validateForm = true;
      }
    }
    if (this.activityType === 2) { // production
      if (this.unitRateDetails.QualityId === 0
         || this.unitRateDetails.QualityId === undefined
         || this.unitRateDetails.QualityId === null
         || this.unitRateDetails.ActivityTypeId === 0
         || this.unitRateDetails.ActivityTypeId === undefined
         || this.unitRateDetails.ActivityTypeId === null
         || this.unitRateDetails.CurrencyId === 0
         || this.unitRateDetails.CurrencyId === undefined
         || this.unitRateDetails.CurrencyId === null
         || this.unitRateDetails.MediumId === 0
         || this.unitRateDetails.MediumId === undefined
         || this.unitRateDetails.MediumId === null
         || this.unitRateDetails.UnitRates === 0
         || this.unitRateDetails.UnitRates === undefined
         || this.unitRateDetails.UnitRates === null
         || this.unitRateDetails.NatureId === 0
         || this.unitRateDetails.NatureId === undefined
         || this.unitRateDetails.NatureId === null) {
        this.validateForm = false;
      } else {
        this.validateForm = true;
      }
    }
    this.onValueChanged();
    if (this.unitRateDetails.UnitRateId === 0) {
      this.unitRateDetails.UnitRateId = this.selectedUnitRateId;
    }
    if (this.validateForm === true) {
      if (this.selectedUnitRateId === 0 || this.selectedUnitRateId === undefined || this.selectedUnitRateId === null) {
        this.addNewUnitRate(this.unitRateDetails);
      } else {
        this.editUnitRate(this.unitRateDetails);
      }
      // tslint:disable-next-line:max-line-length
    } else {
      return;
    }
  }

  editUnitRate(model) {
    this.unitRateForm.disable();
    this.masterPageService
      .Add(
        this.appurl.getApiUrl() + GLOBAL.API_Contract_AddUnitRate,
        model
      )
      .subscribe(
        result => {
          if (result.StatusCode === 200) {
            this.unitRateForm.enable();
            this.toastr.success(result.Message);
            this.unitRateDetails = result.data.unitRateDetailsById;
            this.unitRateDetails.ActivityName =
              result.data.unitRateDetailsById.ActivityTypes.ActivityName;
            // this.unitRateDetails.UnitRate = result.data.unitRateDetails.UnitRates;
            this.updateUnitRateListById.emit(this.unitRateDetails);
          } else {
            this.unitRateForm.enable();
            this.toastr.error(result.Message);
          }
        },
        error => {
          this.unitRateForm.enable();
          this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  addNewUnitRate(model) {
    this.unitRateForm.disable();
    this.masterPageService
      .Add(
        this.appurl.getApiUrl() + GLOBAL.API_Contract_AddUnitRate,
        model
      )
      .subscribe(
        result => {
          if (result.StatusCode === 200) {
            this.toastr.success(result.Message);
            this.unitRateForm.enable();
            this.archiveUnitRate = true;
            this.unitRateDetails = result.data.unitRateDetails;
            this.unitRateDetails.UnitRateId =
              result.data.unitRateDetails.UnitRateId;
            this.unitRateDetails.UnitRates =
              result.data.unitRateDetails.UnitRates;
            this.unitRateDetails.ActivityName =
              result.data.unitRateDetails.ActivityName;
            this.testFunc.emit(this.unitRateDetails);
          } else {
            this.unitRateForm.enable();
            this.archiveUnitRate = true;
            this.toastr.error(result.Message);
          }
        },
        error => {
          this.unitRateForm.enable();
          this.archiveUnitRate = true;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  DeleteUnitRate(id) {
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
    this.unitRateForm.disable();
    this.contractService
      .DeleteUnitRate(
        this.appurl.getApiUrl() + GLOBAL.API_Contract_DeleteUnitRate,
        id
      )
      .subscribe(
        result => {
          if (result.StatusCode === 200) {
          this.deleteUnitRate.emit({ id: id });
          this.toastr.success(result.Message);
          this.unitRateForm.enable();
          dialogRef.componentInstance.onCancelPopup();
          } else {
            this.toastr.error('Some error occured. Please try again later');
            this.unitRateForm.enable();
          }
        },
        error => {
          this.unitRateForm.enable();
          this.toastr.error('Some error occured. Please try again later');
        }
      );
    });
  }

  AdditionOfUnitRate() {
    this.unitRateDetails = {};
    this.unitRateForm.reset();
    this.selectedUnitRateId = 0;
    setTimeout(() => {
      this.SetDefaultValues();
    }, 400);
  }

  // onChange(ev, data) {

  // }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  onValueChanged(data?: any) {
    if (!this.unitRateForm) {
      return;
    }
    const form = this.unitRateForm;
    for (const field in this.formErrors) {
      if (this.formErrors.hasOwnProperty(field)) {
        // clear previous error message (if any)
        this.formErrors[field] = '';
        const control = form.get(field);
        if (control && !control.valid) {
          const messages = this.validationMessages[field];
          for (const key in control.errors) {
            if (control.errors.hasOwnProperty(key)) {
              if (this.activityType === 2) {
                if (field === 'timeCategory') {
                  this.formErrors[field] += ' ';
                } else {
                  this.formErrors[field] += messages[key] + ' ';
                }
              } else if (this.activityType === 1) {
                if (field === 'quality' || field === 'nature') {
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
}
