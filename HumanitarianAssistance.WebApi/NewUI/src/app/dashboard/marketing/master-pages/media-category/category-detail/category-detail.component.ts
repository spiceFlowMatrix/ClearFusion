import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MediaCategoryModel } from '../../model/mastrer-pages.model';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.scss']
})
export class CategoryDetailComponent implements OnInit {
  @Input() mediaCategoryId: number;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deleteMediaCategory = new EventEmitter<any>();
  @Output() addMediaCategory = new EventEmitter<any>();
  @Output() updateMediaCategory = new EventEmitter<any>();
  mediaCategoryDetailsForm;
  archiveButton = false;
  mediaCategoryDetail: MediaCategoryModel = {};
  mediaCategoryDetailFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor(public dialog: MatDialog, public toastr: ToastrService, private commonLoaderService: CommonLoaderService, private mediaCategoryService: MasterPageServiceService, private appurl: AppUrlService) { }

  ngOnInit() {
  }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    // this.GetCategory();
    if (this.mediaCategoryId !== 0 && this.mediaCategoryId !== undefined) {
      this.archiveButton = true;
      this.GetMediaCategoryById(this.mediaCategoryId);
    } else {
       this.archiveButton = false;
    }
  }

  initForm() {
    this.mediaCategoryDetailsForm = new FormGroup({
      mediaCategoryName: new FormControl('', [Validators.required])
    });
  }

  ResetFormOnAddNewMediaCategory() {
    this.mediaCategoryId = 0;
    this.mediaCategoryDetail = {};
    this.mediaCategoryDetailsForm.reset();
  }

  GetMediaCategoryById(id) {
    this.mediaCategoryDetailFlag = true;
    // tslint:disable-next-line:max-line-length
    this.mediaCategoryService.GetMediaCategoryById(id).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.mediaCategoryDetail = result.data;
        this.mediaCategoryDetailsForm = new FormGroup({
          mediaCategoryName: new FormControl(this.mediaCategoryDetail.CategoryName, [Validators.required])
        });
      } else {
        this.toastr.error('Some error occured.Please try again later.');
      }
      this.mediaCategoryDetailFlag = false;
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.mediaCategoryDetailFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onChange(value) {
    this.mediaCategoryDetail.CategoryName = value;
    if (this.mediaCategoryId === 0 || this.mediaCategoryId === undefined || this.mediaCategoryId === null) {
      this.CreateMediaCategory();
    } else {
      this.EditMediaCategory();
    }
  }

  CreateMediaCategory() {
    this.mediaCategoryDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.mediaCategoryService.AddMediaCategory(this.mediaCategoryDetail).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.toastr.success(result.message);
        this.mediaCategoryDetail = result.data;
        this.mediaCategoryId = result.data.MediaCategoryId;
        this.addMediaCategory.emit(this.mediaCategoryDetail);
        this.archiveButton = true;
      } else {
        this.toastr.error(result.message);
      }
      this.mediaCategoryDetailsForm.enable();
    },
    error => {
      this.mediaCategoryDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  EditMediaCategory() {
    this.mediaCategoryDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.mediaCategoryService.AddMediaCategory(this.mediaCategoryDetail).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.toastr.success(result.message);
        this.mediaCategoryDetail = result.data;
        this.updateMediaCategory.emit(this.mediaCategoryDetail);
      }  else {
        this.toastr.error(result.message);
      }
      this.mediaCategoryDetailsForm.enable();
    },
    error => {
      this.mediaCategoryDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  CreateMediaCategoryonAddNew() {
    this.mediaCategoryDetail = {};
    // tslint:disable-next-line:max-line-length
    this.mediaCategoryService.AddMediaCategory(this.mediaCategoryDetail).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.toastr.success(result.message);
        this.mediaCategoryDetail = result.data;
        this.mediaCategoryId = result.data.MediaCategoryId;
        this.addMediaCategory.emit(this.mediaCategoryDetail);
        this.archiveButton = true;
      } else {
        this.toastr.error(result.message);
      }
    },
    error => {
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  DeleteMediaCategory(id) {
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
    this.mediaCategoryDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.mediaCategoryService.DeleteMediaCategory(id).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.toastr.success(result.message);
        this.mediaCategoryId = 0;
        this.mediaCategoryDetail = {};
        dialogRef.componentInstance.onCancelPopup();
        this.deleteMediaCategory.emit({ id: id });
      } else {
        this.toastr.error(result.message);
      }
      this.mediaCategoryDetailsForm.enable();
      dialogRef.componentInstance.isLoading = false;
    },
    error => {
      this.mediaCategoryDetailsForm.enable();
      dialogRef.componentInstance.isLoading = false;
      this.toastr.error('Some error occured. Please try again later');
    });
    });
  }
}
