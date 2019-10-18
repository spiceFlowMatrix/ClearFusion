import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MediumModel } from '../../../contracts/model/contract-details.model';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { GLOBAL } from 'src/app/shared/global';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';

@Component({
  selector: 'app-medium-details',
  templateUrl: './medium-details.component.html',
  styleUrls: ['./medium-details.component.scss']
})
export class MediumDetailsComponent implements OnInit {
  @Input() mediumId: number;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deleteMedium = new EventEmitter<any>();
  @Output() addMedium = new EventEmitter<any>();
  @Output() updateMedium = new EventEmitter<any>();
  mediumDetailsForm;
  archiveButton = false;
  mediumDetail: MediumModel = {};
  mediumDetailsLoaderFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor(public dialog: MatDialog, public toastr: ToastrService, private commonLoaderService: CommonLoaderService, private mediumService: MasterPageServiceService, private appurl: AppUrlService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    // this.GetCategory();
    if (this.mediumId !== 0 && this.mediumId !== undefined) {
      this.archiveButton = true;
      this.GetMediumById(this.mediumId);
    } else {
      this.archiveButton = false;
    }
  }

  initForm() {
    this.mediumDetailsForm = new FormGroup({
      mediumName: new FormControl('', [Validators.required])
    });
  }

  ResetFormOnAddNewMedium() {
    this.mediumDetail = {};
    this.mediumDetailsForm.reset();
    this.mediumId = 0;
  }

  ngOnInit() {
  }

  GetMediumById(id) {
    // this.commonLoaderService.showLoader();
    this.mediumDetailsLoaderFlag = true;
    // tslint:disable-next-line:max-line-length
    this.mediumService.GetById(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMediumById, id).subscribe(result => {
      if (result.StatusCode === 200) {
        // this.commonLoaderService.hideLoader();
        this.mediumDetailsLoaderFlag = false;
        this.mediumDetail = result.data.mediumById;
        this.mediumDetailsForm = new FormGroup({
          mediumName: new FormControl(this.mediumDetail.MediumName, [Validators.required])
        });
      } else {
        this.mediumDetailsLoaderFlag = false;
        // this.commonLoaderService.hideLoader();
        this.toastr.error('Some error occured.Please try again later.');
      }
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.mediumDetailsLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onChange(value) {
    this.mediumDetail.MediumName = value;
    if (this.mediumId === 0 || this.mediumId === undefined || this.mediumId === null) {
      this.CreateMedium();
    } else {
      this.EditMedium();
    }
  }

  CreateMedium() {
    this.mediumDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.mediumService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddMedium, this.mediumDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.mediumDetailsForm.enable();
        this.mediumDetail = result.data.mediumById;
        this.mediumId  = result.data.mediumById.MediumId;
        this.addMedium.emit(this.mediumDetail);
        this.archiveButton = true;
      } else {
        this.toastr.error(result.Message);
        this.mediumDetailsForm.enable();
      }
    },
    error => {
      this.mediumDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  EditMedium() {
    this.mediumDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.mediumService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddMedium, this.mediumDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.mediumDetailsForm.enable();
        this.mediumDetail = result.data.mediumById;
      this.updateMedium.emit(this.mediumDetail);
      } else {
        this.toastr.error(result.Message);
        this.mediumDetailsForm.enable();
      }
    },
    error => {
      this.mediumDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  DeleteMedium(id) {
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
      this.mediumDetailsForm.disable();
      // tslint:disable-next-line:max-line-length
      this.mediumService.Delete(this.appurl.getApiUrl() + GLOBAL.API_Contract_DeleteMedium, id).subscribe(result => {
        if (result.StatusCode === 200) {
          this.mediumDetailsForm.enable();
          this.toastr.success(result.Message);
          dialogRef.componentInstance.onCancelPopup();
          this.deleteMedium.emit({ id: id });
          this.mediumDetail = {};
          this.mediumId  = 0;
        } else {
          this.mediumDetailsForm.enable();
          this.toastr.error(result.Message);
        }
        dialogRef.componentInstance.isLoading = false;
      },
      error => {
        this.mediumDetailsForm.enable();
        dialogRef.componentInstance.isLoading = false;
        this.toastr.error('Some error occured. Please try again later');
      });
    });
  }

  CreateMediumonAddNew() {
    this.mediumDetail = {};
    // tslint:disable-next-line:max-line-length
    this.mediumService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddMedium, this.mediumDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
      this.mediumDetail = result.data.mediumById;
      this.mediumId  = result.data.mediumById.MediumId;
      this.addMedium.emit(this.mediumDetail);
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
