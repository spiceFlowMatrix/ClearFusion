import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { GLOBAL } from 'src/app/shared/global';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { QualityModel } from '../../../contracts/model/contract-details.model';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { MatDialog } from '@angular/material';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';

@Component({
  selector: 'app-quality-details',
  templateUrl: './quality-details.component.html',
  styleUrls: ['./quality-details.component.scss']
})
export class QualityDetailsComponent implements OnInit {
  @Input() qualityId: number;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deleteQuality = new EventEmitter<any>();
  @Output() addQuality = new EventEmitter<any>();
  @Output() updateQuality = new EventEmitter<any>();
  qualityDetailsForm;
  archiveButton = false;
  qualityDetailsLoaderFlag = false;
  qualityDetail: QualityModel = {};
  // tslint:disable-next-line:max-line-length
  constructor(public dialog: MatDialog, public toastr: ToastrService, private commonLoaderService: CommonLoaderService, private qualityService: MasterPageServiceService, private appurl: AppUrlService) { }
  ngOnInit() {
  }
  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    if (this.qualityId !== 0 && this.qualityId !== undefined) {
      this.archiveButton = true;
      this.GetQualityById(this.qualityId);
    } else {
      this.archiveButton = false;
    }
  }

  initForm() {
    this.qualityDetailsForm = new FormGroup({
      qualityName: new FormControl('', [Validators.required])
    });
  }

  ResetFormOnAddNewQuality() {
    this.qualityDetail = {};
    this.qualityDetailsForm.reset();
    this.qualityId = 0;
  }

  GetQualityById(id: number) {
    this.qualityDetailsLoaderFlag = true;
    this.qualityService.GetById(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetQualityById, id).subscribe(result => {
      if (result.StatusCode === 200) {
        this.qualityDetailsLoaderFlag = false;
        this.qualityDetail = result.data.qualityById;
        this.qualityDetailsForm = new FormGroup({
          qualityName: new FormControl(this.qualityDetail.QualityName, [Validators.required])
        });
      } else {
        this.qualityDetailsLoaderFlag = false;
        this.toastr.error('Some error occured.Please try again later.');
      }
    },
    error => {
      this.qualityDetailsLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onChange(value) {
    this.qualityDetail.QualityName = value;
    if (this.qualityId === 0 || this.qualityId === undefined || this.qualityId === null) {
      this.CreateQuality();
    } else {
      this.EditQuality();
    }
  }

  CreateQuality() {
    this.qualityDetailsForm.disable();
    this.qualityService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddQuality, this.qualityDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.qualityDetailsForm.enable();
        this.qualityDetail = result.data.qualityById;
        this.qualityId = result.data.qualityById.QualityId;
        this.addQuality.emit(this.qualityDetail);
        this.archiveButton = true;
      } else {
        this.toastr.error(result.Message);
        this.qualityDetailsForm.enable();
      }
    },
    error => {
      this.qualityDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  EditQuality() {
    this.qualityDetailsForm.disable();
    this.qualityService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddQuality, this.qualityDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.qualityDetailsForm.enable();
        this.qualityDetail = result.data.qualityById;
        this.updateQuality.emit(this.qualityDetail);
      }  else {
        this.qualityDetailsForm.enable();
        this.toastr.error(result.Message);
      }
    },
    error => {
      this.qualityDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  DeleteQuality(id) {
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
    this.qualityDetailsForm.disable();
    this.qualityService.Delete(this.appurl.getApiUrl() + GLOBAL.API_Contract_DeleteQuality, id).subscribe(result => {
      if (result.StatusCode === 200) {
        this.qualityDetailsForm.enable();
        this.toastr.success(result.Message);
        dialogRef.componentInstance.onCancelPopup();
        this.deleteQuality.emit({ id: id });
      this.qualityId = 0;
      this.qualityDetail = {};
      } else {
        this.qualityDetailsForm.enable();
        this.toastr.error(result.Message);
      }
      dialogRef.componentInstance.isLoading = false;
    },
    error => {
      this.qualityDetailsForm.enable();
      dialogRef.componentInstance.isLoading = false;
      this.toastr.error('Some error occured. Please try again later');
    });
    });
  }

  CreateQualityonAddNew() {
    this.qualityDetail = {};
    this.qualityService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddQuality, this.qualityDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.qualityDetail = result.data.qualityById;
        this.qualityId = result.data.qualityById.QualityId;
        this.addQuality.emit(this.qualityDetail);
        this.archiveButton = true;
      } else {
        this.toastr.error(result.Message);
      }
    },
    error => {
      this.toastr.error('Some error occured. Please try again later');
    });
  }
}
