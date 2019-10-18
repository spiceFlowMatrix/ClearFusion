import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { GLOBAL } from 'src/app/shared/global';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { TimeCategoryModel } from '../../../contracts/model/contract-details.model';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { MatDialog } from '@angular/material';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
@Component({
  selector: 'app-time-category-details',
  templateUrl: './time-category-details.component.html',
  styleUrls: ['./time-category-details.component.scss']
})
export class TimeCategoryDetailsComponent implements OnInit {
  @Input() timeCategoryId: number;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deleteTimeCategory = new EventEmitter<any>();
  @Output() addTimeCategory = new EventEmitter<any>();
  @Output() updateTimeCategory = new EventEmitter<any>();
  timeCategoryDetailsForm;
  archiveButton = false;
  timeCategoryDetail: TimeCategoryModel = {};
  timeCategoryDetailsLoaderFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor(public dialog: MatDialog, public toastr: ToastrService, private commonLoaderService: CommonLoaderService, private timeCategoryService: MasterPageServiceService, private appurl: AppUrlService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    // this.GetCategory();
    if (this.timeCategoryId !== 0 && this.timeCategoryId !== undefined) {
      this.archiveButton = true;
      this.GetTimeCategoryById(this.timeCategoryId);
    } else {
      this.archiveButton = false;
    }
  }

  initForm() {
    this.timeCategoryDetailsForm = new FormGroup({
      timeCategoryName: new FormControl('', [Validators.required])
    });
  }

  ResetFormOnAddNewTimeCategory() {
    this.timeCategoryDetail = {};
    this.timeCategoryDetailsForm.reset();
    this.timeCategoryId = 0;
  }

  ngOnInit() {
  }

  GetTimeCategoryById(id) {
    // this.commonLoaderService.showLoader();
    this.timeCategoryDetailsLoaderFlag = true;
    // tslint:disable-next-line:max-line-length
    this.timeCategoryService.GetById(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetTimeCategoryById, id).subscribe(result => {
      if (result.StatusCode === 200) {
        // this.commonLoaderService.hideLoader();
        this.timeCategoryDetailsLoaderFlag = false;
        this.timeCategoryDetail = result.data.timeCatergoryById;
        this.timeCategoryDetailsForm = new FormGroup({
          timeCategoryName: new FormControl(this.timeCategoryDetail.TimeCategoryName, [Validators.required])
        });
      } else {
        // this.commonLoaderService.hideLoader();
        this.timeCategoryDetailsLoaderFlag = false;
        this.toastr.error('Some error occured.Please try again later.');
      }
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.timeCategoryDetailsLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onChange(value) {
    this.timeCategoryDetail.TimeCategoryName = value;
    if (this.timeCategoryId === 0 || this.timeCategoryId === undefined || this.timeCategoryId === null) {
      this.CreateTimeCategory();
    } else {
      this.EditTimeCategory();
    }
  }

  CreateTimeCategory() {
    this.timeCategoryDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.timeCategoryService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddTimeCategory, this.timeCategoryDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.timeCategoryDetailsForm.enable();
        this.timeCategoryDetail = result.data.timeCatergoryById;
      this.timeCategoryId = this.timeCategoryDetail.TimeCategoryId;
      this.addTimeCategory.emit(this.timeCategoryDetail);
      this.archiveButton = true;
      } else {
        this.toastr.error(result.Message);
        this.timeCategoryDetailsForm.enable();
      }
    },
    error => {
      this.timeCategoryDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  EditTimeCategory() {
    this.timeCategoryDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.timeCategoryService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddTimeCategory, this.timeCategoryDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.timeCategoryDetailsForm.enable();
        this.timeCategoryDetail = result.data.timeCatergoryById;
        this.updateTimeCategory.emit(this.timeCategoryDetail);
      this.archiveButton = true;
      } else {
        this.toastr.error(result.Message);
        this.timeCategoryDetailsForm.enable();
      }
    },
    error => {
      this.timeCategoryDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  DeleteTimeCategory(id) {
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
    this.timeCategoryDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.timeCategoryService.Delete(this.appurl.getApiUrl() + GLOBAL.API_Contract_DeleteTimeCategory, id).subscribe(result => {
      if (result.StatusCode === 200) {
        this.timeCategoryDetailsForm.enable();
        this.toastr.success(result.Message);
        dialogRef.componentInstance.onCancelPopup();
        this.deleteTimeCategory.emit({ id: id });
        this.timeCategoryDetail = {};
        this.timeCategoryId = 0;
      } else {
        this.timeCategoryDetailsForm.enable();
        this.toastr.error(result.Message);
      }
      dialogRef.componentInstance.isLoading = false;
    },
    error => {
      this.timeCategoryDetailsForm.enable();
      dialogRef.componentInstance.isLoading = false;
      this.toastr.error('Some error occured. Please try again later');
    });
    });
  }

  CreateTimeCategoryonAddNew() {
    this.timeCategoryDetail = {};
    // tslint:disable-next-line:max-line-length
    this.timeCategoryService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddTimeCategory, this.timeCategoryDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.timeCategoryDetail = result.data.timeCatergoryById;
      this.timeCategoryId = this.timeCategoryDetail.TimeCategoryId;
      this.addTimeCategory.emit(this.timeCategoryDetail);
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
